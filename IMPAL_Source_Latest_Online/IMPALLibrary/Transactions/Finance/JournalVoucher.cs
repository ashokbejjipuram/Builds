using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Transactions;

namespace IMPALLibrary
{
    public class JournalVoucher
    {
        public List<JVHeader> GetJVNumber(string strBranch, string AccPeriodCode)
        {
            List<JVHeader> lstJVNumber = new List<JVHeader>();

            JVHeader objJVNumber = new JVHeader();
            objJVNumber.JVNumber = "0";
            objJVNumber.JVNumber = "-- Select --";
            lstJVNumber.Add(objJVNumber);

            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetJVnumbers");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
            ImpalDB.AddInParameter(cmd, "@Acc_Period_Code", DbType.String, AccPeriodCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objJVNumber = new JVHeader();
                    objJVNumber.JVNumber = reader[0].ToString();
                    lstJVNumber.Add(objJVNumber);
                }
            }

            return lstJVNumber;
        }

        public List<JVHeader> GetJVNumberFinal(string strBranch)
        {
            List<JVHeader> lstJVNumber = new List<JVHeader>();

            JVHeader objJVNumber = new JVHeader();
            objJVNumber.JVNumber = "0";
            objJVNumber.JVNumber = "-- Select --";
            lstJVNumber.Add(objJVNumber);

            Database ImpalDB = DataAccess.GetDatabase();

            string year = System.DateTime.Now.Year.ToString() + "%";
            string prevYear = Convert.ToString(DateTime.Today.Year - 1) + "%";


            string sSQL = "Select jv_number from JV_Header WITH (NOLOCK) Where branch_code = '" + strBranch + "' and isnull(status,'A') in ('A','P')";
            sSQL = sSQL + " and (jv_number like '" + year + "' or jv_number like '" + prevYear + "') and Approval_Status is NULL order by JV_Number desc";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objJVNumber = new JVHeader();
                    objJVNumber.JVNumber = reader[0].ToString();
                    lstJVNumber.Add(objJVNumber);
                }
            }

            return lstJVNumber;
        }

        public List<JVHeader> GetJVHeader(string strJVNumber, string strBranch)
        {
            List<JVHeader> lstJVNumber = new List<JVHeader>();

            JVHeader objJVNumber = new JVHeader();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select JVH.JV_Number, convert(nvarchar,JVH.JV_Date,103) document_date, ";
            sSQL = sSQL + "JVH.Reference_Document_Number , convert(nvarchar,JVH.Reference_Document_Date,103) reference_document_date, ";
            sSQL = sSQL + "JVH.Reference_Document_Type, JVH.Narration, AP.description, JVH.Approval_Status ";
            sSQL = sSQL + "From JV_Header JVH WITH (NOLOCK) inner join Accounting_Period AP WITH (NOLOCK) on JVH.JV_date between Start_date and End_date ";
            sSQL = sSQL + "Where JVH.branch_code = '" + strBranch + "' and isnull(JVH.status,'A') in ('A') and JVH.JV_Number= '" + strJVNumber + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objJVNumber = new JVHeader();

                    objJVNumber.JVNumber = reader["JV_number"].ToString();
                    objJVNumber.JVDate = reader["document_date"].ToString();
                    objJVNumber.ReferenceDocNumber = reader["Reference_Document_Number"].ToString();
                    objJVNumber.ReferenceDocDate = reader["Reference_Document_Date"].ToString();
                    objJVNumber.ReferenceDocType = reader["Reference_Document_Type"].ToString();
                    objJVNumber.Narration = reader["Narration"].ToString();
                    objJVNumber.AccountingPeriod = reader["description"].ToString();
                    objJVNumber.ApprovalStatus = reader["Approval_Status"].ToString();
                    lstJVNumber.Add(objJVNumber);
                }
            }

            return lstJVNumber;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public List<JVDetail> GetJVDetail(string strJVNumber, string BranchCode)
        {
            List<JVDetail> lstJVDetail = new List<JVDetail>();

            JVDetail objJVDetail = new JVDetail();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " Select JVH.JV_Number, JVD.Debit_Credit_Indicator, ROUND(JVD.Amount,2) Amount,";
            sSQL = sSQL + " JVD.Serial_Number,JVD.Chart_of_Account_Code,JVD.Remarks, convert(nvarchar,JVD.Reference_Date,103) Reference_Date,";
            sSQL = sSQL + " JVD.Document_Type, JVD.Reference_Number, GLA.Description";
            sSQL = sSQL + " from JV_header JVH WITH (NOLOCK) Inner Join JV_Detail JVD WITH (NOLOCK) On JVH.JV_Number = JVD.JV_Number";
            sSQL = sSQL + " Left Outer Join GL_Account_Master GLA On substring(JVD.chart_of_account_Code, 4,3) = GLA.gl_main_code";
            sSQL = sSQL + " and substring(JVD.chart_of_account_Code, 7,4) = GLA.gl_sub_code and substring(JVD.chart_of_account_Code, 11,7) = GLA.gl_account_code";
            sSQL = sSQL + " Where JVH.JV_Number= '" + strJVNumber + "' and JVH.Branch_Code = '" + BranchCode + "'";
            int count = 0;
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    count = count + 1;
                    objJVDetail = new JVDetail();
                    objJVDetail.SerialNumber = reader["Serial_Number"].ToString();
                    objJVDetail.Chart_of_Account_Code = reader["Chart_of_Account_Code"].ToString();
                    objJVDetail.Description = reader["Description"].ToString();
                    objJVDetail.Remarks = reader["Remarks"].ToString();
                    objJVDetail.Ref_Doc_Date = reader["Reference_Date"].ToString();
                    objJVDetail.Ref_Doc_Type = reader["Document_Type"].ToString();
                    objJVDetail.Ref_Doc_Number = reader["Reference_Number"].ToString();
                    objJVDetail.Dr_Cr = reader["Debit_Credit_Indicator"].ToString();

                    if (objJVDetail.Dr_Cr == "D")
                    {
                        objJVDetail.Debit_Amount = TwoDecimalConversion(reader["Amount"].ToString());
                    }
                    else if (objJVDetail.Dr_Cr == "C")
                    {
                        objJVDetail.Credit_Amount = TwoDecimalConversion(reader["Amount"].ToString());
                    }
                   
                    lstJVDetail.Add(objJVDetail);
                }
            }

            return lstJVDetail;
        }

        public JVHeader GetJournalVoucherDetailsExcel(string BranchCode, int Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string SQLQuery = "Exec Usp_AddTemp_Journal_Voucher_excel '','','','','','','" + BranchCode + "',''," + Indicator;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            JVHeader jvEntity = new JVHeader();
            JVDetail jvItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    jvEntity.ErrorCode = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                    jvEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    jvEntity.Amount = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[2].ToString());
                    jvEntity.Items = new List<JVDetail>();
                }

                if (jvEntity.ErrorCode == "0.00")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drItem in ds.Tables[1].Rows)
                        {
                            jvItem = new JVDetail();

                            jvItem.SerialNumber = drItem["Serial_Number"].ToString();
                            jvItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                            jvItem.Description = drItem["Description"].ToString();
                            jvItem.Ref_Doc_Number = drItem["Ref_Doc_Number"].ToString();
                            jvItem.Ref_Doc_Date = drItem["Ref_Doc_Date"].ToString();
                            jvItem.Dr_Cr = drItem["Dr_Cr"].ToString();
                            jvItem.Debit_Amount = TwoDecimalConversion(drItem["Debit_Amount"].ToString());
                            jvItem.Credit_Amount = TwoDecimalConversion(drItem["Credit_Amount"].ToString());
                            jvItem.Remarks = drItem["Remarks"].ToString();

                            jvEntity.Items.Add(jvItem);
                        }
                    }
                }

            }

            return jvEntity;
        }

        public string AddNewJVEntry(ref JVHeader jvHeader)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string JVNumber = "";
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DataSet ds = new DataSet();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addjv1");
                    ImpalDB.AddInParameter(cmd, "@JV_Date", DbType.String, jvHeader.JVDate);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Type", DbType.String, jvHeader.ReferenceDocType);
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, jvHeader.ReferenceDocNumber);
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, jvHeader.ReferenceDocDate);
                    ImpalDB.AddInParameter(cmd, "@Narration", DbType.String, jvHeader.Narration);
                    ImpalDB.AddInParameter(cmd, "@indicator", DbType.String, "JV");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        JVNumber = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (JVNumber != "")
                    {
                        foreach (JVDetail jvDetail in jvHeader.Items)
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addjv2");
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                            ImpalDB.AddInParameter(cmd1, "@JV_Number", DbType.String, JVNumber);
                            ImpalDB.AddInParameter(cmd1, "@Serial_Number", DbType.Int32, jvDetail.SerialNumber);
                            ImpalDB.AddInParameter(cmd1, "@Chart_Of_Account_Code", DbType.String, jvDetail.Chart_of_Account_Code);
                            ImpalDB.AddInParameter(cmd1, "@Remarks", DbType.String, jvDetail.Remarks);
                            ImpalDB.AddInParameter(cmd1, "@Debit_Credit_Indicator", DbType.String, jvDetail.Dr_Cr);

                            if (jvDetail.Dr_Cr == "D")
                            {
                                ImpalDB.AddInParameter(cmd1, "@Amount", DbType.String, jvDetail.Debit_Amount);
                            }
                            else
                            {
                                ImpalDB.AddInParameter(cmd1, "@Amount", DbType.String, jvDetail.Credit_Amount);
                            }

                            ImpalDB.AddInParameter(cmd1, "@document_type", DbType.String, jvDetail.Ref_Doc_Type);
                            ImpalDB.AddInParameter(cmd1, "@reference_number", DbType.String, jvDetail.Ref_Doc_Number);
                            ImpalDB.AddInParameter(cmd1, "@reference_date", DbType.String, jvDetail.Ref_Doc_Date);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);
                        }
                    }

                    jvHeader.ErrorCode = "0";
                    jvHeader.ErrorMsg = "";

                    result = 1;
                    scope.Complete();
                } 
            }
            catch (Exception exp)
            {
                jvHeader.ErrorCode = "1";
                jvHeader.ErrorMsg = exp.Message.ToString();
                result = -1;
                Log.WriteException(Source, exp);
            }

            return JVNumber;
        }

        public void AddNewJVEntryFinal(ref JVHeader jvHeader)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updjv1");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@JV_Number", DbType.String, jvHeader.JVNumber);
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, jvHeader.ReferenceDocNumber);
                    ImpalDB.AddInParameter(cmd, "@Narration", DbType.String, jvHeader.Narration);
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, jvHeader.ApprovalLevel);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    foreach (JVDetail jvDetail in jvHeader.Items)
                    {
                        DbCommand cmdTemp1 = ImpalDB.GetStoredProcCommand("usp_updjv2");
                        ImpalDB.AddInParameter(cmdTemp1, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                        ImpalDB.AddInParameter(cmdTemp1, "@JV_Number", DbType.String, jvHeader.JVNumber);
                        ImpalDB.AddInParameter(cmdTemp1, "@Serial_Number", DbType.Int32, jvDetail.SerialNumber);
                        ImpalDB.AddInParameter(cmdTemp1, "@Chart_Of_Account_Code", DbType.String, jvDetail.Chart_of_Account_Code);
                        ImpalDB.AddInParameter(cmdTemp1, "@Remarks", DbType.String, jvDetail.Remarks);
                        ImpalDB.AddInParameter(cmdTemp1, "@Debit_Credit_Indicator", DbType.String, jvDetail.Dr_Cr);

                        if (jvDetail.Dr_Cr == "D")
                        {
                            ImpalDB.AddInParameter(cmdTemp1, "@Amount", DbType.String, jvDetail.Debit_Amount);
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmdTemp1, "@Amount", DbType.String, jvDetail.Credit_Amount);
                        }

                        ImpalDB.AddInParameter(cmdTemp1, "@document_type", DbType.String, jvDetail.Ref_Doc_Type);
                        ImpalDB.AddInParameter(cmdTemp1, "@reference_number", DbType.String, jvDetail.Ref_Doc_Number);
                        ImpalDB.AddInParameter(cmdTemp1, "@reference_date", DbType.String, jvDetail.Ref_Doc_Date);
                        cmdTemp1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdTemp1);
                    }                    

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_updcustos_jv");
                    ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                    ImpalDB.AddInParameter(cmd1, "@JV_Number", DbType.String, jvHeader.JVNumber);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_addgljv");
                    ImpalDB.AddInParameter(cmd2, "@Doc_no", DbType.String, jvHeader.JVNumber);
                    ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, jvHeader.BranchCode);
                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd2);

                    jvHeader.ErrorCode = "0";
                    jvHeader.ErrorMsg = "";

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                jvHeader.ErrorCode = "1";
                jvHeader.ErrorMsg = exp.Message.ToString();
                result = -1;
                Log.WriteException(Source, exp);
            }
        }

        public void UpdNewJVEntry(string BranchCode, string JVNumber, string Remarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdCancelJV");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@JV_Number", DbType.String, JVNumber);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, Remarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(Source, exp);
            }
        }

        public string GetDescription(string strChartofAccount, string strBranchCode)
        {

            string strDescription = string.Empty;          

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select description from chart_of_account a, GL_Account_Master b ";
            sSQL = sSQL + " Where branch_code = '" + strBranchCode + "' and  a.Chart_of_Account_Code = '" + strChartofAccount + "' and  b.gl_main_code = substring(a.chart_of_account_Code, 4,3)";
            sSQL = sSQL + " and b.gl_sub_code = substring(a.chart_of_account_Code, 7,4) and ";
            sSQL = sSQL + " b.gl_account_code = substring(a.chart_of_account_Code, 11,7) ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    strDescription = reader["description"].ToString();                    
                }
            }

            return strDescription;
        }
    }

    public class JVHeader
    {
        public string JVNumber { get; set; }
        public string JVDate { get; set; }
        public string AccountingPeriod { get; set; }
        public string ReferenceDocType { get; set; }
        public string ReferenceDocDate { get; set; }
        public string ReferenceDocNumber { get; set; }
        public string NoofTransactions { get; set; }
        public string Narration { get; set; }
        public string BranchCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }
        public string Amount { get; set; }

        public List<JVDetail> Items { get; set; }
    }

    public class JVDetail
    {

        public string SerialNumber { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string Ref_Doc_Date { get; set; }
        public string Ref_Doc_Type { get; set; }
        public string Ref_Doc_Number { get; set; }
        public string Dr_Cr { get; set; }
        public string Debit_Amount { get; set; }
        public string Credit_Amount { get; set; }
    }
}
