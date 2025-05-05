#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Transactions;
using System.IO;

#endregion Namespace

namespace IMPALLibrary
{
    public class Upload
    {
        #region Private Variables

        private string file;

        private int branchCount = 0;

        private int columnCount = 0;

        private string branchCodeValue;

        private string branchNameValue;

        private string sqlQueryCRP;

        private string textLine;

        private char[] filters = { '|' };

        private string[] textLineArray = new string[1000];

        private string tableName;

        private string tableSPSQLQuery;

        private string sqlQueryColumns;

        private DataSet dataSetBranchTables = new DataSet();

        private int totalBranchCount = 0;

        private int retstatus = 0;

        private string branchUploadDetailsDataRowInfo = "<tr><td>{0}</td><td>{1}</td></tr>";

        private string branchUploadDetailsErrorInfo = "<tr><td>{0}</td><td>{1}</td></tr>";

        private string branchUploadDetails = "";
        private string branchUploadErrDetails = "";
        private string erritem = "";

        public string[] detailInfo;

        #endregion Private Variables

        #region Public Variables

        Database ImpalDB = DataAccess.GetDatabase();

        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        #endregion Public Variables

        #region Constructor
        public Upload() { }

        #endregion Constructor

        #region User Defined Methods

        /// <summary>
        /// To get the Brach details for Upload
        /// </summary>
        /// <param name="branchCode"></param>
        /// <param name="isHOProcess"></param>
        /// <returns></returns>
        public DataSet UploadBranchDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                sqlQueryCRP = "Select distinct branch_code,Branch_Name FROM Branch_Master b inner join users u on b.Branch_Code=u.BranchCode and ISNULL(b.status,'A') = 'A' and u.password is not null";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sqlQueryCRP);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (DataSet reader = ImpalDB.ExecuteDataSet(cmd))
                {
                    dataSetBranchTables = reader;
                }
                return dataSetBranchTables;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return null;
            }
        }

        public int UploadBranchDetailsCount(string branchCode, bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                sqlQueryCRP = "Select count(branch_code) FROM Branch_Master WHERE ISNULL(status,'A') = 'A' ";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sqlQueryCRP);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader readerBranchCount = ImpalDB.ExecuteReader(cmd))
                {
                    while (readerBranchCount.Read())
                    {
                        totalBranchCount = readerBranchCount.GetInt32(0);
                    }
                }
                return totalBranchCount;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return 0;
            }
        }

        public DataSet UploadPaymentCORDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                sqlQueryCRP = "Select Branch_Code,Branch_Name FROM Branch_Master WHERE ISNULL(status,'A') = 'A'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sqlQueryCRP);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (DataSet reader = ImpalDB.ExecuteDataSet(cmd))
                {
                    dataSetBranchTables = reader;
                }
                return dataSetBranchTables;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return null;
            }
        }

        /// <summary>
        /// Method to upload Individual Branch Data
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateCode"></param>
        /// <param name="branchCode"></param>
        /// <param name="branchCodeInd"></param>
        /// <param name="isHOProcess"></param>
        /// <returns></returns>
        public string UploadBranchDataFromTextFile(string filePath, string fileName, string branchCode, string DownloadDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Excstatus = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    branchUploadDetails = branchUploadDetails + "Text file - " + fileName + " exists";

                    StreamReader streamReader = new StreamReader(Path.Combine(filePath, fileName));
                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        if (textLine.Trim() != string.Empty)
                        {
                            // To split the fields based on filter and store them in an array
                            textLineArray = uitility.TextLineSplitter(textLine, filters);

                            // To check whether new table starts
                            if (textLineArray[0] == "@TABLE")
                            {
                                tableName = textLineArray[1].ToString();

                                if (tableName == "Consignment")
                                {
                                    tableSPSQLQuery = "Update Parameter set last_updated_date = getdate() " +
                                                        " where parameter_code = 10 and branch_code = '" + branchCode + "'";
                                    DbCommand cmd = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                    ImpalDB.ExecuteNonQuery(cmd);
                                }

                                // To get the column count for the current table
                                tableSPSQLQuery = "SELECT cnt FROM Branch.dbo.v_column_cnt Where Table_Name = '" + tableName + "'";
                                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                columnCount = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd1));
                            }
                            else //Manipulate the records for the Current table
                            {
                                // To check whether the retrieved string line has all the columns of the table
                                if (columnCount == textLineArray.Length)
                                {
                                    if (fileName.Substring(8, 3).Contains("CRP") && (tableName.ToUpper() == "General_Ledger_Detail".ToUpper()
                                                   || tableName.ToUpper() == "Journal_Voucher_Header".ToUpper()
                                                   || tableName.ToUpper() == "Journal_Voucher_Detail".ToUpper()))
                                        sqlQueryColumns = "usp_DownUp_Pymt_" + tableName + " ";
                                    else
                                        sqlQueryColumns = "usp_DownUp_" + tableName + " ";

                                    foreach (string splitField in textLineArray)
                                    {
                                        string splitFieldAssign = splitField == "" ? "0" : splitField;
                                        sqlQueryColumns = sqlQueryColumns + string.Format(" {0},", splitFieldAssign);
                                    }

                                    sqlQueryColumns = sqlQueryColumns + "'UP','O',"  + string.Format("'{0}'", branchCode);

                                    erritem = "<br><font color='red'> " + sqlQueryColumns + "</font>";

                                    try
                                    {
                                        Excstatus = 0;
                                        DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sqlQueryColumns);
                                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                        using (IDataReader readerBranchTable = ImpalDB.ExecuteReader(cmd2)) { }
                                    }
                                    catch (Exception exp)
                                    {
                                        Excstatus = 1;
                                        branchUploadDetails = branchUploadDetails + "<br>" + erritem + "<br><br><font color='red'>" + exp.Message + "</font>";
                                        streamReader.Close();
                                        streamReader.Dispose();
                                    }
                                }
                            }
                        }
                    }

                    if (Excstatus == 0)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }

                    branchUploadDetails = branchUploadDetails + "<font color='green'><b> - Successfully Completed</b></font><br><br>";

                    scope.Complete();
                    return branchUploadDetails;
                }
            }
            catch (Exception exp)
            {
                //return branchUploadDetails;
                //Log.WriteException(Source, exp);
                throw new Exception(branchUploadDetails);
            }
        }

        public string UploadHOorderDataFromTextFile(string filePath, string fileName, string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Excstatus = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    branchUploadDetails = branchUploadDetails + "Text file - " + fileName + " exists";

                    StreamReader streamReader = new StreamReader(Path.Combine(filePath, fileName));
                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        if (textLine.Trim() != string.Empty)
                        {
                            // To split the fields based on filter and store them in an array
                            textLineArray = uitility.TextLineSplitter(textLine, filters);

                            // To check whether new table starts
                            if (textLineArray[0] == "@TABLE")
                            {
                                tableName = textLineArray[1].ToString();
                                tableSPSQLQuery = "SELECT cnt FROM Branch.dbo.v_column_cnt Where Table_Name = '" + tableName + "'";
                                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                columnCount = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd1));
                            }
                            else //Manipulate the records for the Current table
                            {
                                if (columnCount == textLineArray.Length)
                                {
                                    sqlQueryColumns = "usp_DownUp_" + tableName + " ";

                                    foreach (string splitField in textLineArray)
                                    {
                                        string splitFieldAssign = splitField == "" ? "0" : splitField;
                                        sqlQueryColumns = sqlQueryColumns + string.Format(" {0},", splitFieldAssign);
                                    }

                                    sqlQueryColumns = sqlQueryColumns + " 'UP','O'";

                                    erritem = "<br><font color='red'> " + sqlQueryColumns + "</font>";

                                    try
                                    {
                                        Excstatus = 0;
                                        DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sqlQueryColumns);
                                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                        using (IDataReader readerBranchTable = ImpalDB.ExecuteReader(cmd2)) { }
                                    }
                                    catch (Exception exp)
                                    {
                                        Excstatus = 1;
                                        branchUploadDetails = branchUploadDetails + "<br>" + erritem + "<br><br><font color='red'>" + exp.Message + "</font>";
                                        streamReader.Close();
                                        streamReader.Dispose();
                                    }
                                }
                            }
                        }
                    }

                    if (Excstatus == 0)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }

                    branchUploadDetails = branchUploadDetails + "<font color='green'><b> - Successfully Completed</b></font><br><br>";

                    scope.Complete();
                    return branchUploadDetails;
                }
            }
            catch (Exception exp)
            {
                //return branchUploadDetails;
                //Log.WriteException(Source, exp);
                throw new Exception(branchUploadDetails);
            }
        }

        public string UploadPaymentDataFromTextFile(string filePath, string fileName, string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Excstatus = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    branchUploadDetails = branchUploadDetails + "Text file - " + fileName + " exists";

                    StreamReader streamReader = new StreamReader(Path.Combine(filePath, fileName));
                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        if (textLine.Trim() != string.Empty)
                        {
                            // To split the fields based on filter and store them in an array
                            textLineArray = uitility.TextLineSplitter(textLine, filters);

                            // To check whether new table starts
                            if (textLineArray[0] == "@TABLE")
                            {
                                tableName = textLineArray[1].ToString();

                                tableSPSQLQuery = "SELECT cnt FROM Branch.dbo.v_column_cnt Where Table_Name = '" + tableName + "'";
                                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                columnCount = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd1));
                            }
                            else //Manipulate the records for the Current table
                            {
                                if (columnCount == textLineArray.Length)
                                {
                                    if (tableName.ToUpper() == "GENERAL_LEDGER_DETAIL" || tableName.ToUpper() == "JOURNAL_VOUCHER_HEADER" || tableName.ToUpper() == "JOURNAL_VOUCHER_DETAIL")
                                        sqlQueryColumns = "usp_DownUp_Pymt_" + tableName + " ";

                                    foreach (string splitField in textLineArray)
                                    {
                                        string splitFieldAssign = splitField == "" ? "0" : splitField;
                                        sqlQueryColumns = sqlQueryColumns + string.Format(" {0},", splitFieldAssign);
                                    }

                                    if (tableName.ToUpper() == "GENERAL_LEDGER_DETAIL" || tableName.ToUpper() == "JOURNAL_VOUCHER_HEADER" || tableName.ToUpper() == "JOURNAL_VOUCHER_DETAIL")
                                        sqlQueryColumns = sqlQueryColumns + string.Format("'{0}'", branchCode) + ",'UP','O'";

                                    erritem = "<br><font color='red'> " + sqlQueryColumns + "</font>";

                                    try
                                    {
                                        Excstatus = 0;
                                        DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sqlQueryColumns);
                                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                        using (IDataReader readerBranchTable = ImpalDB.ExecuteReader(cmd2)) { }
                                    }
                                    catch (Exception exp)
                                    {
                                        Excstatus = 1;
                                        branchUploadDetails = branchUploadDetails + "<br>" + erritem + "<br><br><font color='red'>" + exp.Message + "</font>";
                                        streamReader.Close();
                                        streamReader.Dispose();
                                    }
                                }
                            }
                        }
                    }

                    if (Excstatus == 0)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }

                    branchUploadDetails = branchUploadDetails + "<font color='green'><b> - Successfully Completed</b></font><br><br>";

                    scope.Complete();
                    return branchUploadDetails;
                }
            }
            catch (Exception exp)
            {
                //return branchUploadDetails;
                //Log.WriteException(Source, exp);
                throw new Exception(branchUploadDetails);
            }
        }

        public string UploadPaymentCORDataFromTextFile(string filePath, string fileName, string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                file = Path.Combine(filePath, fileName);

                if (uitility.CheckFileExists(file))
                {
                    branchUploadDetails = branchUploadDetails +
                                            string.Format(branchUploadDetailsDataRowInfo,
                                            string.Format("Text file - {0} exists", fileName),
                                            "");

                    StreamReader streamReader = new StreamReader(file);
                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        if (textLine.Trim() != string.Empty)
                        {
                            // To split the fields based on filter and store them in an array
                            textLineArray = uitility.TextLineSplitter(textLine, filters);

                            // To check whether new table starts
                            if (textLineArray[0] == "@TABLE")
                            {
                                branchUploadDetails = branchUploadDetails +
                                                        string.Format(branchUploadDetailsDataRowInfo,
                                                        "",
                                                        "");

                                tableName = textLineArray[1].ToString();

                                if (tableName.ToUpper() == "INWARD_HEADER" || tableName == "DEBIT_CREDIT_NOTE_HEADER")
                                {
                                    tableSPSQLQuery = "sp_columns " + tableName;

                                    DbCommand cmd = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                    using (IDataReader readerBranchTable = ImpalDB.ExecuteReader(cmd)) { }

                                    // To get the column count for the current table
                                    tableSPSQLQuery = "SELECT cnt FROM v_column_cnt Where Table_Name = '" + tableName + "'";
                                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                    using (IDataReader readerTableCount = ImpalDB.ExecuteReader(cmd1))
                                    {
                                        while (readerTableCount.Read())
                                        {
                                            columnCount = readerTableCount.GetInt32(0);
                                        }
                                    }

                                    branchUploadDetails = branchUploadDetails +
                                                            string.Format(branchUploadDetailsDataRowInfo,
                                                            string.Format("Table - {0}", tableName),
                                                            string.Format("Columns: {0}", columnCount));
                                }
                            }
                            else //Manipulate the records for the Current table
                            {
                                if (tableName.ToUpper() == "INWARD_HEADER" || tableName.ToUpper() == "DEBIT_CREDIT_NOTE_HEADER")
                                {
                                    sqlQueryColumns = "Usp_Corporate_Supplier_Payment ";

                                    if (tableName.ToUpper() == "INWARD_HEADER")
                                    {
                                        decimal inward_Amt;
                                        decimal purchase_Amt;
                                        decimal invoice_Amt;

                                        if (textLineArray[4].ToString() == "")
                                            inward_Amt = 0;
                                        else
                                            inward_Amt = Convert.ToDecimal(textLineArray[4].ToString());

                                        if (textLineArray[5].ToString() == "")
                                            purchase_Amt = 0;
                                        else
                                            purchase_Amt = Convert.ToDecimal(textLineArray[5].ToString());

                                        invoice_Amt = inward_Amt + purchase_Amt;

                                        sqlQueryColumns = sqlQueryColumns + textLineArray[7] + "," + textLineArray[0] + "," + textLineArray[1] + "," + invoice_Amt;
                                        sqlQueryColumns = sqlQueryColumns + ",NULL,NULL,NULL,'PO'," + textLineArray[6] + "," + textLineArray[7] + "," + textLineArray[2] + "," + textLineArray[3] + ",NULL,NULL,NULL,NULL,NULL," + textLineArray[8];
                                    }
                                    else
                                    {
                                        decimal debit_Amt;
                                        if (textLineArray[4].ToString().ToUpper() == "DA")
                                            debit_Amt = Convert.ToDecimal(textLineArray[5].ToString()) * -1;
                                        else
                                            debit_Amt = Convert.ToDecimal(textLineArray[5].ToString());

                                        sqlQueryColumns = sqlQueryColumns + textLineArray[7] + "," + textLineArray[0] + "," + textLineArray[1] + ",NULL," + textLineArray[2] + ",";
                                        sqlQueryColumns = sqlQueryColumns + textLineArray[3] + "," + debit_Amt + ",'" + textLineArray[4] + "'," + textLineArray[6] + "," + textLineArray[7];
                                        sqlQueryColumns = sqlQueryColumns + ",NULL,NULL,NULL,NULL," + textLineArray[0] + "," + textLineArray[1] + "," + debit_Amt + "," + textLineArray[8];
                                    }

                                    branchUploadDetails = branchUploadDetails +
                                                        string.Format(branchUploadDetailsDataRowInfo,
                                                        string.Format("Table - {0}", tableName),
                                                        string.Format("Parameters: {0}", sqlQueryColumns));

                                    erritem = "<br><font color='red'>" + sqlQueryColumns + "</font>";

                                    DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sqlQueryColumns);
                                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                    using (IDataReader readerBranchTable = ImpalDB.ExecuteReader(cmd2)) { }
                                }
                            }
                        }
                    }

                    streamReader.Close();
                }

                return branchUploadDetails;
            }
            catch (Exception exp)
            {
                branchUploadDetails = erritem;
                return branchUploadDetails;
                Log.WriteException(Source, exp);
            }
        }
        #endregion User Defined Methods
    }
}
