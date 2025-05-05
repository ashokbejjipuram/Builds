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
using System.Threading;

#endregion Namespace

namespace IMPALLibrary
{
    public class Download
    {
        #region Private Variables

        private string sSQLTableNames;

        //private string tableName;

        //private int tableColumnsCount;

        private string systemSPSQLQuery;

        private string tableSPSQLQuery;

        private string invSQLQuery;

        private int columnCount = 0;

        private string[] columnType = new string[1000];

        private string downloadTextLine;

        private DataSet dataSetBranchTables = new DataSet();

        private DataSet dataSetBranches = new DataSet();

        #endregion Private Variables

        #region Public Variables

        #endregion Public Variables

        #region Constructor
        public Download() { }

        #endregion Constructor

        #region User Defined Methods

        public DataSet DownloadBranchTableDetails(string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                Database ImpalDB = DataAccess.GetDatabase();

                sSQLTableNames = "SELECT TABLE_NAME, cnt FROM Branch.dbo.v_column_cnt,Impal.dbo.tables " +
                                 "where tables.tables=v_column_cnt.table_name and " +
                                 "tables.tables not in('Consignment_Allbranches','Consignment_AvgSales') ORDER BY seq_no";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQLTableNames);
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

        public DataSet DownloadPaymentBranchTableDetails(string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                Database ImpalDB = DataAccess.GetDatabase();

                sSQLTableNames = "SELECT TABLE_NAME, cnt FROM v_column_cnt,tables " +
                                 "where tables.tables=v_column_cnt.table_name " +
                                 "and tables.seq_no in (64,39) ORDER BY seq_no";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQLTableNames);
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

        public DataSet DownloadPaymentToBranchTableDetails(string branchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                Database ImpalDB = DataAccess.GetDatabase();

                sSQLTableNames = "SELECT TABLE_NAME, cnt FROM v_column_cnt,tables " +
                                 "where tables.tables=v_column_cnt.table_name " +
                                 "and tables.seq_no in (132,133,134) ORDER BY seq_no";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQLTableNames);
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

        public DataSet DownloadBranchDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                sSQLTableNames = "Select Distinct BranchCode from Users Where BranchCode not in ('CRP') ORDER BY BranchCode";
                //sSQLTableNames = "Select Distinct BranchCode from Users Where BranchCode in ('AGR') ORDER BY BranchCode";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQLTableNames);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (DataSet reader = ImpalDB.ExecuteDataSet(cmd))
                {
                    dataSetBranches = reader;
                }

                return dataSetBranches;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return null;
            }
        }

        public bool DownloadBranchData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                StreamWriter streamWriter;
                streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                streamWriter.WriteLine("@TABLE|" + tableName);
                systemSPSQLQuery = "sp_columns " + tableName;
                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_DownUp_" + tableName + " NULL";
                for (int loopCount = 2; loopCount <= tableColumnsCount; loopCount++)
                {
                    tableSPSQLQuery = tableSPSQLQuery + ", NULL";
                }
                tableSPSQLQuery = tableSPSQLQuery + ",'DN', 'S'";

                //if (tableName.ToUpper().Equals("CONSIGNMENT"))
                    tableSPSQLQuery = tableSPSQLQuery + "," + branchCode;

                DbCommand cmdSP = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmdSP.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmdSP))
                { 
                    while (recordsReader.Read())
                    {
                        if (columnType[0] == "datetime" || columnType[0] == "varchar")
                            downloadTextLine = "'" + recordsReader.GetValue(0).ToString() + "'";
                        else
                            downloadTextLine = recordsReader.GetValue(0).ToString();

                        for (columnCount = 1; columnCount < recordsReader.FieldCount; columnCount++)
                        {
                            if (columnType[columnCount] == "datetime" || columnType[columnCount] == "varchar")
                                downloadTextLine = downloadTextLine + "|'" + recordsReader.GetValue(columnCount).ToString() + "'";
                            else
                                downloadTextLine = downloadTextLine + "|" + recordsReader.GetValue(columnCount).ToString();
                        }

                        streamWriter.WriteLine(downloadTextLine);
                    }
                }

                streamWriter.Flush();
                streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool DownloadUpdateBranchData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //StreamWriter streamWriter;
                //streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                //streamWriter.WriteLine("@TABLE|" + tableName);
                systemSPSQLQuery = "sp_columns " + tableName;

                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_DownUp_" + tableName + " NULL";
                for (int loopCount = 2; loopCount <= tableColumnsCount; loopCount++)
                {
                    tableSPSQLQuery = tableSPSQLQuery + ", NULL";
                }
                tableSPSQLQuery = tableSPSQLQuery + ",'DN', 'U'";

                //if (tableName.ToUpper().Equals("CONSIGNMENT"))
                tableSPSQLQuery = tableSPSQLQuery + "," + branchCode;

                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmd1))
                {

                }

                //streamWriter.Flush();
                //streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool DownloadPaymentCorporateData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                StreamWriter streamWriter;
                streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                streamWriter.WriteLine("@TABLE|" + tableName);
                systemSPSQLQuery = "sp_columns " + tableName;
                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_Pymt_DownUp_" + tableName + " NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'DN', 'S'";

                DbCommand cmdSP = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmdSP.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmdSP))
                {
                    while (recordsReader.Read())
                    {
                        if (columnType[0] == "datetime" || columnType[0] == "varchar")
                            downloadTextLine = "'" + recordsReader.GetValue(0).ToString() + "'";
                        else
                            downloadTextLine = recordsReader.GetValue(0).ToString();

                        for (columnCount = 1; columnCount < recordsReader.FieldCount; columnCount++)
                        {
                            if (columnType[columnCount] == "datetime" || columnType[columnCount] == "varchar")
                                downloadTextLine = downloadTextLine + "|'" + recordsReader.GetValue(columnCount).ToString() + "'";
                            else
                                downloadTextLine = downloadTextLine + "|" + recordsReader.GetValue(columnCount).ToString();
                        }

                        streamWriter.WriteLine(downloadTextLine);
                    }
                }

                streamWriter.Flush();
                streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }
        

        public bool DownloadPaymentUpdateCorporateData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //StreamWriter streamWriter;
                //streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                //streamWriter.WriteLine("@TABLE|" + tableName);
                systemSPSQLQuery = "sp_columns " + tableName;

                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_Pymt_DownUp_" + tableName + " NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'DN','U'";

                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmd1))
                {

                }

                //streamWriter.Flush();
                //streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool DownloadPaymentToBranchCorporateData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                StreamWriter streamWriter;
                streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                streamWriter.WriteLine("@TABLE|" + tableName.Substring(0,tableName.Length-4));
                systemSPSQLQuery = "sp_columns " + tableName;
                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_DownUp_" + tableName + " NULL";
                for (int loopCount = 2; loopCount <= tableColumnsCount; loopCount++)
                {
                    tableSPSQLQuery = tableSPSQLQuery + ", NULL";
                }
                tableSPSQLQuery = tableSPSQLQuery + ",'DN', 'S'";

                DbCommand cmdSP = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmdSP.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmdSP))
                {
                    while (recordsReader.Read())
                    {
                        if (columnType[0] == "datetime" || columnType[0] == "varchar")
                            downloadTextLine = "'" + recordsReader.GetValue(0).ToString() + "'";
                        else
                            downloadTextLine = recordsReader.GetValue(0).ToString();

                        for (columnCount = 1; columnCount < recordsReader.FieldCount; columnCount++)
                        {
                            if (columnType[columnCount] == "datetime" || columnType[columnCount] == "varchar")
                                downloadTextLine = downloadTextLine + "|'" + recordsReader.GetValue(columnCount).ToString() + "'";
                            else
                                downloadTextLine = downloadTextLine + "|" + recordsReader.GetValue(columnCount).ToString();
                        }

                        streamWriter.WriteLine(downloadTextLine);
                    }
                }

                streamWriter.Flush();
                streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool DownloadPaymentToBranchUpdateCorporateData(string filePath, string branchCode, string tableName, int tableColumnsCount)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //StreamWriter streamWriter;
                //streamWriter = File.AppendText(filePath);
                Database ImpalDB = DataAccess.GetDatabase();
                columnType = new string[1000];
                columnCount = 0;

                //streamWriter.WriteLine("@TABLE|" + tableName);
                systemSPSQLQuery = "sp_columns " + tableName;

                DbCommand cmd = ImpalDB.GetSqlStringCommand(systemSPSQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader subReader = ImpalDB.ExecuteReader(cmd))
                {
                    while (subReader.Read())
                    {
                        columnType[columnCount] = subReader.GetString(5);
                        columnCount++;
                    }
                }

                tableSPSQLQuery = "usp_DownUp_" + tableName + " NULL";
                for (int loopCount = 2; loopCount <= tableColumnsCount; loopCount++)
                {
                    tableSPSQLQuery = tableSPSQLQuery + ", NULL";
                }
                tableSPSQLQuery = tableSPSQLQuery + ",'DN', 'U'";

                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(tableSPSQLQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader recordsReader = ImpalDB.ExecuteReader(cmd1))
                {

                }

                //streamWriter.Flush();
                //streamWriter.Close();
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public void UpdateInvTables(string BranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();

                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand("INSERT INTO V_Invoice_All SELECT v1.* FROM V_Invoice v1 left join V_Invoice_All v2 on v1.Document_Number = v2.Document_Number where v1.Branch_Code= '" + BranchCode + "' and v2.Document_Number is null");
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    DbCommand cmd2 = ImpalDB.GetSqlStringCommand("Delete From v_invoice Where Branch_Code = '" + BranchCode + "'");
                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd2);

                    DbCommand cmd3 = ImpalDB.GetSqlStringCommand("INSERT INTO V_Invoice_ST_All SELECT v1.* FROM V_Invoice_ST v1 left join V_Invoice_ST_All v2 on v1.Document_Number = v2.Document_Number where v1.Branch_Code= '" + BranchCode + "' and v2.Document_Number is null");
                    cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd3);

                    DbCommand cmd4 = ImpalDB.GetSqlStringCommand("Delete From v_invoice_St Where Branch_Code = '" + BranchCode + "'");
                    cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd4);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void UpdateDownupStatus()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();

                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Update_Downup_Status");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);                    

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion User Defined Methods
    }
}
