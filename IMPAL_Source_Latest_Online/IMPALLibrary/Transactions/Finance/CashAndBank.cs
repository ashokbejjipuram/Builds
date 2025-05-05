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
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Transactions.Finance;

#endregion Namespace

namespace IMPALLibrary
{
    public class CashAndBankEntity
    {
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BranchCode { get; set; }
        public string Remarks { get; set; }
        public string ChartOfAccountCode { get; set; }
        public string ReceiptPaymentIndicator { get; set; }
        public string CashChequeIndicator { get; set; }
        public string TransactionAmount { get; set; }
        public string Accounting_Period { get; set; }
        public string VendorNumber { get; set; }

        public string Cheque_Number { get; set; }
        public string Cheque_Date { get; set; }
        public string Cheque_Bank { get; set; }
        public string Cheque_Branch { get; set; }
        public string Local_Outstation { get; set; }
        public string Ref_Date { get; set; }        
        public string RefNo { get; set; }
        public string PaymentBranch { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public string Location { get; set; }
        public string Indicator { get; set; }
        public string BankCharges { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
       
        public List<CashAndBankItem> Items { get; set; }
    }

    public class CashAndBankItem
    {
        public string Serial_Number { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string Branch { get; set; }
        public string BankRefNo { get; set; }
        public string NeftDate { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceDocumentNumber { get; set; }
        public string ReferenceDocumentNumber1 { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentValue { get; set; }
        public string CollectionAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string PaymentIndicator { get; set; }
    }

    public class VendorBookingEntity
    {
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string BranchCode { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Location { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string GSTValue { get; set; }
        public string RoundingOffValue { get; set; }
        public string TDStype { get; set; }
        public string TDSvalue { get; set; }
        public string GSTINNumber { get; set; }
        public string Narration { get; set; }
        public string Status { get; set; }
        public string AccountingPeriodCode { get; set; }
        public string RCMstatus { get; set; }
        public string PaymentBranch { get; set; }
        public string PaymentDueDate { get; set; }

        public string PaymentNumber { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }

        public string PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string AuthorityMatrixStatus { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeBank { get; set; }
        public string ChequeBranch { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }

        public List<VendorBookingDetail> Items { get; set; }
    }

    public class VendorBookingDetail
    {
        public string SerialNumber { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string Dr_Cr { get; set; }
        public string Amount { get; set; }
        public string SGST_Code { get; set; }
        public string SGST_Per { get; set; }
        public string SGST_Amt { get; set; }
        public string CGST_Code { get; set; }
        public string CGST_Per { get; set; }
        public string CGST_Amt { get; set; }
        public string IGST_Code { get; set; }
        public string IGST_Per { get; set; }
        public string IGST_Amt { get; set; }
        public string UTGST_Code { get; set; }
        public string UTGST_Per { get; set; }
        public string UTGST_Amt { get; set; }
        public string OtherTax_Code { get; set; }
        public string OtherTax_Per { get; set; }
        public string OtherTax_Amt { get; set; }

        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string TDSAmount { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string PaymentAmount { get; set; }
    }

    public class CashAndBankTransactions
    {
        public List<string> GetTransactionNumber(string strBranchCode, string Indicator)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Transaction_Number from Main_Cash_Header WITH (NOLOCK) where Substring(Transaction_Number,1,4)>=YEAR(GETDATE())-2 and ";
                if (Indicator == "P")
                    sSQL = sSQL + "Receipt_Payment_Indicator <> 'R'";
                else if (Indicator == "R")
                    sSQL = sSQL + "Receipt_Payment_Indicator = 'R'";

                sSQL = sSQL + " and  Branch_Code = '" + strBranchCode + "' and Ref_no IS NULL and Remarks not like '%HOUPD' Order by Transaction_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public List<string> GetTransactionNumberUpload(string strBranchCode, string Indicator)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Transaction_Number from Main_Cash_Header WITH (NOLOCK) where Substring(Transaction_Number,1,4)>=YEAR(GETDATE())-2 and ";
                if (Indicator == "P")
                    sSQL = sSQL + "Receipt_Payment_Indicator <> 'R'";
                else if (Indicator == "R")
                    sSQL = sSQL + "Receipt_Payment_Indicator = 'R'";

                sSQL = sSQL + " and  Branch_Code = '" + strBranchCode + "' and Ref_no IS NULL and Remarks like '%-HOUPD' Order by Transaction_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public List<string> GetTransactionNumberUploadAndRegular(string strBranchCode, string Indicator)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Transaction_Number from Main_Cash_Header WITH (NOLOCK) where Substring(Transaction_Number,1,4)>=YEAR(GETDATE())-2 and ";
                if (Indicator == "P")
                    sSQL = sSQL + "Receipt_Payment_Indicator <> 'R'";
                else if (Indicator == "R")
                    sSQL = sSQL + "Receipt_Payment_Indicator = 'R'";

                sSQL = sSQL + " and  Branch_Code = '" + strBranchCode + "' and Ref_no IS NULL and Remarks like '%-HOREGUPD' Order by Transaction_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public List<string> GetTransactionNumberNEFT(string strBranchCode, string Indicator)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Transaction_Number from Main_Cash_Header WITH (NOLOCK) where Substring(Transaction_Number,1,4)>=YEAR(GETDATE())-2 and ";
                if (Indicator == "P")
                    sSQL = sSQL + "Receipt_Payment_Indicator <> 'R'";
                else if (Indicator == "R")
                    sSQL = sSQL + "Receipt_Payment_Indicator = 'R'";

                sSQL = sSQL + " and  Branch_Code = '" + strBranchCode + "' and Ref_no IS NULL and Remarks like '%-NEFTHOUPD' Order by Transaction_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public List<string> GetTransactionNumberChqReturn(string strBranchCode, string Indicator, string TransType)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Transaction_Number from Main_Cash_Header WITH (NOLOCK) where Substring(Transaction_Number,1,4)>=YEAR(GETDATE())-2 and ";
                if (Indicator == "P")
                    sSQL = sSQL + "Receipt_Payment_Indicator <> 'R'";
                else if (Indicator == "R")
                    sSQL = sSQL + "Receipt_Payment_Indicator = 'R'";

                sSQL = sSQL + " and  Branch_Code = '" + strBranchCode + "' and Ref_no IS NOT NULL Order by Transaction_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public List<string> GetTransactionForCancellation(string strBranchCode, string strTransactionNumber)
        {
            List<String> lstTransactionNo = new List<string>();
            lstTransactionNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetChequeSlipList_ForCancellation");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Transaction_Number", DbType.String, strTransactionNumber);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTransactionNo.Add(Convert.ToString(reader["Transaction_Number"]));
                    }
                }

                return lstTransactionNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public string GetBranchName(string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select branch_name from branch_master where branch_code='" + strBranchCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objBranch = ImpalDB.ExecuteScalar(cmd1);
                return Convert.ToString(objBranch);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public DataSet GetOutstandingDetails(string strBranchCode, string strFromCustCode, string strToCustCode, string strDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_calcos_report");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@frcustomer", DbType.String, strFromCustCode);
            ImpalDB.AddInParameter(dbcmd, "@tocustomer", DbType.String, strToCustCode);
            ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public DataSet GetOutstandingDetailsBackUp(string strBranchCode, string strFromCustCode, string strToCustCode, string strDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_calcos_report");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@frcustomer", DbType.String, strFromCustCode);
            ImpalDB.AddInParameter(dbcmd, "@tocustomer", DbType.String, strToCustCode);
            ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public DataSet GetNEFTdetailsHO(string strBranchCode, string strFromDate, string strToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetNEFTdetailsHO");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@From_Date", DbType.String, strFromDate);
            ImpalDB.AddInParameter(dbcmd, "@To_Date", DbType.String, strToDate);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public DataSet GetPDCRegisterReport(string strBranchCode, string strFromDate, string strToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetPDCRegister_Report");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@From_Date", DbType.String, strFromDate);
            ImpalDB.AddInParameter(dbcmd, "@To_Date", DbType.String, strToDate);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public List<AccountingPeriod> GetAccountingPeriod()
        {
            List<AccountingPeriod> lstAccPeriod = new List<AccountingPeriod>();
            Database ImpalDB = DataAccess.GetDatabase();

            lstAccPeriod.Add(new AccountingPeriod("", ""));

            //string sQuery = "SELECT  DISTINCT ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD WHERE GETDATE() BETWEEN " +
            //               " CASE DATEPART(MM,GETDATE()) WHEN 4 THEN DATEADD(YY,-1,START_DATE)ELSE  START_DATE END " +
            //               " AND CASE DATEPART(MM,GETDATE()) WHEN 4 THEN DATEADD(YY,1,END_DATE) ELSE  END_DATE  END " +
            //               " ORDER BY ACCOUNTING_PERIOD_CODE DESC ";


            string sQuery = "SELECT  DISTINCT ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD WHERE '03/31/2020' BETWEEN START_DATE AND END_DATE ORDER BY ACCOUNTING_PERIOD_CODE DESC";
            DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd2))
            {
                while (reader.Read())
                    lstAccPeriod.Add(new AccountingPeriod(reader[0].ToString(), reader[1].ToString()));
            }

            return lstAccPeriod;
        }

        public DataSet GetVendorBookingDetails(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select * from V_VendorBooking_Details_Report where Branch_Code='" + BranchCode + "' and Convert(date,Document_Date,103) between Convert(date,'" + FromDate + "',103) and Convert(date,'" + ToDate + "',103)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetTempReceiptDetailsBMapp(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetTempReceipt_Details_App");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
            ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetVendorTDSReportHO(string zone, string state, string branch, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVendor_TDS_Report");
            ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, zone.Trim());
            ImpalDB.AddInParameter(cmd, "@State", DbType.String, state.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, branch.Trim());
            ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate);
            ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetFundRemittanceDetails(string BranchCode, string FromDate, string ToDate, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetFundRemittanceDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
            ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
            ImpalDB.AddInParameter(cmd, "@Ind", DbType.String, Indicator);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetSTDNInwardOutwardJournalDetails(string strBranchCode, string FromDate, string ToDate, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_STDNInwardJournalSummaryDetails");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@From_Date", DbType.String, FromDate);
            ImpalDB.AddInParameter(dbcmd, "@To_Date", DbType.String, ToDate);
            ImpalDB.AddInParameter(dbcmd, "@Indicator", DbType.String, Indicator);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public String GetDocumentDate(string AcountingPeriodCode)
        {
            string DocumentDate = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select convert(nvarchar,End_Date,103) from Accounting_Period where Accounting_Period_Code ='" + AcountingPeriodCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DocumentDate = ImpalDB.ExecuteScalar(cmd).ToString();
            return DocumentDate;
        }

        public CashAndBankEntity GetCashBankDetails(string BranchCode,string strTransactionNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCashAndBankDetailsByNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Transaction_number", DbType.String, strTransactionNo);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.TransactionDate = dr["Transaction_date"].ToString();
                    cashandbankEntity.BranchCode = dr["Branch_Code"].ToString();
                    cashandbankEntity.Remarks = dr["Remarks"].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(dr["Transaction_Amount"].ToString());
                    cashandbankEntity.ChartOfAccountCode = dr["Chart_of_account_Code"].ToString();
                    cashandbankEntity.ReceiptPaymentIndicator = dr["Receipt_Payment_Indicator"].ToString();
                    cashandbankEntity.CashChequeIndicator = dr["Cash_Cheque_Indicator"].ToString();
                    cashandbankEntity.Cheque_Number = dr["Cheque_Number"].ToString();
                    cashandbankEntity.Cheque_Date = dr["Cheque_Date"].ToString();
                    cashandbankEntity.Cheque_Bank = dr["Cheque_Bank"].ToString();
                    cashandbankEntity.Cheque_Branch = dr["Cheque_Branch"].ToString();
                    cashandbankEntity.Local_Outstation = dr["Local_Outstation"].ToString();
                    cashandbankEntity.Ref_Date = dr["Ref_Date"].ToString();
                    cashandbankEntity.Accounting_Period = dr["Accounting_Period_Code"].ToString();

                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        cashandbankItem = new CashAndBankItem();

                        cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                        cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                        cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                        cashandbankItem.Remarks = drItem["Remarks"].ToString();                        

                        cashandbankEntity.Items.Add(cashandbankItem);
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankNEFTDetails(string BranchCode, string strTransactionNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCashAndBankNEFTdetailsByNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Transaction_number", DbType.String, strTransactionNo);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.TransactionDate = dr["Transaction_date"].ToString();
                    cashandbankEntity.BranchCode = dr["Branch_Code"].ToString();
                    cashandbankEntity.Remarks = dr["Remarks"].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(dr["Transaction_Amount"].ToString());
                    cashandbankEntity.ChartOfAccountCode = dr["Chart_of_account_Code"].ToString();
                    cashandbankEntity.ReceiptPaymentIndicator = dr["Receipt_Payment_Indicator"].ToString();
                    cashandbankEntity.CashChequeIndicator = dr["Cash_Cheque_Indicator"].ToString();
                    cashandbankEntity.Cheque_Number = dr["Cheque_Number"].ToString();
                    cashandbankEntity.Cheque_Date = dr["Cheque_Date"].ToString();
                    cashandbankEntity.Cheque_Bank = dr["Cheque_Bank"].ToString();
                    cashandbankEntity.Cheque_Branch = dr["Cheque_Branch"].ToString();
                    cashandbankEntity.Local_Outstation = dr["Local_Outstation"].ToString();
                    cashandbankEntity.Ref_Date = dr["Ref_Date"].ToString();
                    cashandbankEntity.Accounting_Period = dr["Accounting_Period_Code"].ToString();

                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        cashandbankItem = new CashAndBankItem();

                        cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                        cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                        cashandbankItem.Branch = drItem["Branch_Code"].ToString();
                        cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                        cashandbankItem.BankRefNo = drItem["Ref_Number"].ToString();
                        cashandbankItem.Remarks = drItem["Remarks"].ToString();
                        cashandbankItem.NeftDate = drItem["Date"].ToString();

                        cashandbankEntity.Items.Add(cashandbankItem);
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankDetailsCancellation(string BranchCode, string strTransactionNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCashAndBankDetailsByNumber_Cancellation");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Transaction_number", DbType.String, strTransactionNo);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.TransactionDate = dr["Transaction_date"].ToString();
                    cashandbankEntity.BranchCode = dr["Branch_Code"].ToString();
                    cashandbankEntity.Remarks = dr["Remarks"].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(dr["Transaction_Amount"].ToString());
                    cashandbankEntity.ChartOfAccountCode = dr["Chart_of_account_Code"].ToString();
                    cashandbankEntity.ReceiptPaymentIndicator = dr["Receipt_Payment_Indicator"].ToString();
                    cashandbankEntity.CashChequeIndicator = dr["Cash_Cheque_Indicator"].ToString();
                    cashandbankEntity.Cheque_Number = dr["Cheque_Number"].ToString();
                    cashandbankEntity.Cheque_Date = dr["Cheque_Date"].ToString();
                    cashandbankEntity.Cheque_Bank = dr["Cheque_Bank"].ToString();
                    cashandbankEntity.Cheque_Branch = dr["Cheque_Branch"].ToString();
                    cashandbankEntity.Local_Outstation = dr["Local_Outstation"].ToString();
                    cashandbankEntity.Ref_Date = dr["Ref_Date"].ToString();
                    cashandbankEntity.Accounting_Period = dr["description"].ToString();

                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        cashandbankItem = new CashAndBankItem();

                        cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                        cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                        cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                        cashandbankItem.Remarks = drItem["Remarks"].ToString();

                        cashandbankEntity.Items.Add(cashandbankItem);
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankDetailsChqReturn(string BranchCode, string strTransactionNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCashAndBankDetailsByNumber_ChqReturn");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Transaction_number", DbType.String, strTransactionNo);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.TransactionDate = dr["Transaction_date"].ToString();
                    cashandbankEntity.BranchCode = dr["Branch_Code"].ToString();
                    cashandbankEntity.Remarks = dr["Remarks"].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(dr["Transaction_Amount"].ToString());
                    cashandbankEntity.BankCharges = TwoDecimalConversion(dr["Bank_Charges"].ToString());
                    cashandbankEntity.ChartOfAccountCode = dr["Chart_of_account_Code"].ToString();
                    cashandbankEntity.ReceiptPaymentIndicator = dr["Receipt_Payment_Indicator"].ToString();
                    cashandbankEntity.CashChequeIndicator = dr["Cash_Cheque_Indicator"].ToString();
                    cashandbankEntity.Cheque_Number = dr["Cheque_Number"].ToString();
                    cashandbankEntity.Cheque_Date = dr["Cheque_Date"].ToString();
                    cashandbankEntity.Cheque_Bank = dr["Cheque_Bank"].ToString();
                    cashandbankEntity.Cheque_Branch = dr["Cheque_Branch"].ToString();
                    cashandbankEntity.Local_Outstation = dr["Local_Outstation"].ToString();
                    cashandbankEntity.RefNo = dr["Ref_No"].ToString();
                    cashandbankEntity.Ref_Date = dr["Ref_Date"].ToString();
                    cashandbankEntity.Accounting_Period = dr["Accounting_Period_Code"].ToString();
                    cashandbankEntity.CustomerCode = dr["Customer_Code"].ToString();
                    cashandbankEntity.CustomerName = dr["Customer_Name"].ToString();
                    cashandbankEntity.Location = dr["Location"].ToString();
                    cashandbankEntity.address1 = dr["address1"].ToString();
                    cashandbankEntity.address2 = dr["address2"].ToString();
                    cashandbankEntity.address3 = dr["address3"].ToString();
                    cashandbankEntity.address4 = dr["address4"].ToString();

                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        cashandbankItem = new CashAndBankItem();

                        cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                        cashandbankItem.ReferenceType = drItem["Reference_Type"].ToString();
                        cashandbankItem.ReferenceDocumentNumber = drItem["Reference_Document_Number"].ToString();
                        cashandbankItem.DocumentDate = drItem["Reference_Document_Date"].ToString();
                        cashandbankItem.DocumentValue = TwoDecimalConversion(drItem["Invoice_Value"].ToString());
                        cashandbankItem.CollectionAmount = TwoDecimalConversion(drItem["Collection_Amount"].ToString());
                        cashandbankItem.BalanceAmount = TwoDecimalConversion(drItem["BalanceAmount"].ToString());
                        cashandbankItem.PaymentIndicator = drItem["Remarks"].ToString();

                        cashandbankEntity.Items.Add(cashandbankItem);
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankReceiptDetailsExcel(string filePath, string fileName, string BranchCode, string Amount, int Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string SQLQuery = "Exec Usp_AddTemp_Receipt_CashAndBank_Excel '','','','','" + BranchCode + "'," + Amount + ",'" + filePath + "','" + fileName + "',''," + Indicator;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.ErrorCode = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                    cashandbankEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();                     
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[2].ToString());
                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (cashandbankEntity.ErrorCode == "0.00")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drItem in ds.Tables[1].Rows)
                        {
                            cashandbankItem = new CashAndBankItem();

                            cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                            cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                            cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                            cashandbankItem.Remarks = drItem["Remarks"].ToString();

                            cashandbankEntity.Items.Add(cashandbankItem);
                        }
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankReceiptDetailsExcelNEFT(string filePath, string fileName, string BranchCode, string Amount, string ChartofAcc, int Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string SQLQuery = "Exec Usp_AddTemp_Receipt_CashAndBank_NEFT_Excel '','','','','','" + BranchCode + "'," + Amount + ",'" + filePath + "','" + fileName + "','','" + ChartofAcc + "'," + Indicator;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;
            double result = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    if (Double.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString(), out result))
                    {
                        cashandbankEntity.ErrorCode = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                        cashandbankEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        cashandbankEntity.TransactionAmount = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[2].ToString());
                        cashandbankEntity.Items = new List<CashAndBankItem>();
                    }
                    else if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "ERROR1")
                    {
                        cashandbankEntity.ErrorCode = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        cashandbankEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    }
                    else if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "ERROR2")
                        cashandbankEntity.ErrorCode = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                if (cashandbankEntity.ErrorCode == "0.00")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drItem in ds.Tables[1].Rows)
                        {
                            cashandbankItem = new CashAndBankItem();

                            cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                            cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                            cashandbankItem.Branch = drItem["Branch_Code"].ToString();
                            cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                            cashandbankItem.BankRefNo = drItem["Ref_Number"].ToString();
                            cashandbankItem.Remarks = drItem["Remarks"].ToString();
                            cashandbankItem.NeftDate = drItem["Date"].ToString();

                            cashandbankEntity.Items.Add(cashandbankItem);
                        }
                    }
                }

            }
            return cashandbankEntity;
        }

        public CashAndBankEntity GetCashBankPaymentDetailsExcel(string filePath, string fileName, string BranchCode, string Amount, int Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string SQLQuery = "Exec Usp_AddTemp_Payment_CashAndBank_Excel '','','','','" + BranchCode + "'," + Amount + ",'" + filePath + "','" + fileName + "',''," + Indicator;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.ErrorCode = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                    cashandbankEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[2].ToString());
                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (cashandbankEntity.ErrorCode == "0.00")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drItem in ds.Tables[1].Rows)
                        {
                            cashandbankItem = new CashAndBankItem();

                            cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                            cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                            cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                            cashandbankItem.Remarks = drItem["Remarks"].ToString();

                            cashandbankEntity.Items.Add(cashandbankItem);
                        }
                    }
                }

            }
            return cashandbankEntity;
        }

        public DataSet GetDuplicateNEFTUploadDetails(string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_GetDuplicate_NEFT_Excel_Details");
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            return ds;
        }

        public CashAndBankEntity GetCashBankPaymentDetailsExcelNew(string filePath, string fileName, string BranchCode, string Amount, int Indicator, string GridTotal)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string SQLQuery = "Exec Usp_AddTemp_Payment_CashAndBank_Excel_New '','','','','" + BranchCode + "'," + Amount + "," + GridTotal + ",'" + filePath + "','" + fileName + "',''," + Indicator;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashAndBankEntity cashandbankEntity = new CashAndBankEntity();
            CashAndBankItem cashandbankItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    cashandbankEntity.ErrorCode = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                    cashandbankEntity.ErrorMsg = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                    cashandbankEntity.TransactionAmount = TwoDecimalConversion(ds.Tables[0].Rows[0].ItemArray[2].ToString());
                    cashandbankEntity.Items = new List<CashAndBankItem>();
                }

                if (cashandbankEntity.ErrorCode == "0.00")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drItem in ds.Tables[1].Rows)
                        {
                            cashandbankItem = new CashAndBankItem();

                            cashandbankItem.Serial_Number = drItem["Serial_Number"].ToString();
                            cashandbankItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                            cashandbankItem.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                            cashandbankItem.Remarks = drItem["Remarks"].ToString();

                            cashandbankEntity.Items.Add(cashandbankItem);
                        }
                    }
                }

            }
            return cashandbankEntity;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public int AddCashBankDetails(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
						ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        foreach (CashAndBankItem cbItem in cashandbankEntity.Items)
                        {
                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2");

                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cbItem.Serial_Number);                            
                            ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, cbItem.Chart_of_Account_Code);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cbItem.Amount);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cbItem.Remarks);
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);
                        }

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            if (cashandbankEntity.BranchCode.ToString().ToUpper() != "COR")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsNew(ref CashAndBankEntity cashandbankEntity, string ReceiptType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1_New");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, cashandbankEntity.CustomerCode);
                        ImpalDB.AddInParameter(cmdHeader, "@HO_Ref_Number", DbType.String, cashandbankEntity.RefNo);
                        ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, "A");
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        foreach (CashAndBankItem cbItem in cashandbankEntity.Items)
                        {
                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2");
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cbItem.Serial_Number);
                            ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, cbItem.Chart_of_Account_Code);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cbItem.Amount);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cbItem.Remarks);
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);
                        }

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            ImpalDB.AddInParameter(cmd1, "@HO_Ref_Number", DbType.String, cashandbankEntity.RefNo);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            if (cashandbankEntity.BranchCode.ToString().ToUpper() != "COR")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsInterBranch(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1_InterBranch");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        ImpalDB.AddInParameter(cmdHeader, "@Payment_Branch", DbType.String, cashandbankEntity.PaymentBranch);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        foreach (CashAndBankItem cbItem in cashandbankEntity.Items)
                        {
                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2");
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cbItem.Serial_Number);
                            ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, cbItem.Chart_of_Account_Code);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cbItem.Amount);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cbItem.Remarks);
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);
                        }

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            if (cashandbankEntity.BranchCode.ToString().ToUpper() != "COR")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddChequeReturnDetails(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1_ChqReturn");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, cashandbankEntity.CustomerCode);
                        ImpalDB.AddInParameter(cmdHeader, "@RefNo", DbType.String, cashandbankEntity.RefNo);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdHeader, "@Bank_Charges", DbType.Decimal, cashandbankEntity.BankCharges);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        ImpalDB.AddInParameter(cmdHeader, "@Indicator", DbType.String, cashandbankEntity.Indicator);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();
                        
                        DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2_ChqReturn");
                        ImpalDB.AddInParameter(cmdItem, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                        ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cashandbankEntity.Items[0].Serial_Number);
                        ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, cashandbankEntity.Items[0].Chart_of_Account_Code);
                        ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cashandbankEntity.Items[0].Amount);
                        ImpalDB.AddInParameter(cmdItem, "@Bank_Charges", DbType.Decimal, cashandbankEntity.BankCharges);
                        ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cashandbankEntity.Items[0].Remarks);
                        cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItem);

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1_ChqReturn");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            ImpalDB.AddInParameter(cmd1, "@Customer_Code", DbType.String, cashandbankEntity.CustomerCode);
                            ImpalDB.AddInParameter(cmd1, "@Cheque_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                            ImpalDB.AddInParameter(cmd1, "@Bank_Charges", DbType.Decimal, cashandbankEntity.BankCharges);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsExcel(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
						ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        foreach (CashAndBankItem cbItem in cashandbankEntity.Items)
                        {
                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2");
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cbItem.Serial_Number);
                            ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, cbItem.Chart_of_Account_Code);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cbItem.Amount);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cbItem.Remarks);
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);
                        }

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1_excel");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            if (cashandbankEntity.BranchCode.ToString().ToUpper() != "COR")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsExcelNew(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            int count = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1");
                    ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                    ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                    ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                    ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
					ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                    ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                    ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                    if (TransactionNumber != "")
                    {
                        DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2_New");
                        ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                        ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                        count = Convert.ToInt16(ImpalDB.ExecuteScalar(cmdItem).ToString());

                        if (count > 0)
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            if (cashandbankEntity.BranchCode.ToString().ToUpper() != "COR")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsHOBranch(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        foreach (CashAndBankItem cbItem in cashandbankEntity.Items)
                        {
                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2_HO");
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);                            
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                            ImpalDB.AddInParameter(cmdItem, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, cbItem.Serial_Number);                           
                            ImpalDB.AddInParameter(cmdItem, "@To_Branch", DbType.String, cbItem.Branch);
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, cbItem.Amount);
                            ImpalDB.AddInParameter(cmdItem, "@Ref_Number", DbType.String, cbItem.BankRefNo);
                            ImpalDB.AddInParameter(cmdItem, "@Ref_Date", DbType.String, cbItem.NeftDate);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, cbItem.Remarks);                            
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);
                        }

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1_HO");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int AddCashBankDetailsHOBranchExcel(ref CashAndBankEntity cashandbankEntity, string ScreenMode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (cashandbankEntity.Items.Count > 0)
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1");
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                        ImpalDB.AddInParameter(cmdHeader, "@Chart_Of_Account_Code", DbType.String, cashandbankEntity.ChartOfAccountCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, cashandbankEntity.Cheque_Number);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, cashandbankEntity.Cheque_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, cashandbankEntity.Cheque_Bank);
                        ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, cashandbankEntity.Cheque_Branch);
                        ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, cashandbankEntity.Ref_Date);
                        ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, cashandbankEntity.Local_Outstation);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                        DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2_HO_Excel");
                        ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                        ImpalDB.AddInParameter(cmdItem, "@Transaction_Date", DbType.String, cashandbankEntity.TransactionDate);
                        ImpalDB.AddInParameter(cmdItem, "@Transaction_Amount", DbType.Decimal, cashandbankEntity.TransactionAmount);
                        ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, cashandbankEntity.ReceiptPaymentIndicator);
                        ImpalDB.AddInParameter(cmdItem, "@Cash_Cheque_Indicator", DbType.String, cashandbankEntity.CashChequeIndicator);
                        ImpalDB.AddInParameter(cmdItem, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                        cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItem);

                        if (TransactionNumber != "")
                        {
                            DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1_HO");
                            ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);

                            cashandbankEntity.TransactionNumber = TransactionNumber;
                            cashandbankEntity.ErrorCode = "0";
                            cashandbankEntity.ErrorMsg = "";
                            result = 1;
                        }
                        else
                        {
                            cashandbankEntity.ErrorCode = "1";
                            cashandbankEntity.ErrorMsg = "Data Error";
                            result = -1;
                        }
                    }
                    else
                    {
                        cashandbankEntity.ErrorCode = "1";
                        cashandbankEntity.ErrorMsg = "Data Error";
                        result = -1;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public int UpdCashBankDetailsCancellation(ref CashAndBankEntity cashandbankEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updmaincash1_Cancel");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, cashandbankEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Transaction_Number", DbType.String, cashandbankEntity.TransactionNumber);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, cashandbankEntity.Remarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    cashandbankEntity.ErrorCode = "0";
                    cashandbankEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;
        }

        public List<string> GetVendorNumber(string strBranchCode)
        {
            List<String> lstVendorNo = new List<string>();
            lstVendorNo.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select Document_Number from Vendor_Invoice_Header WITH (NOLOCK) where Substring(Document_Number,1,4)>=YEAR(GETDATE())-2 ";
                sSQL = sSQL + "and Branch_Code = '" + strBranchCode + "' and Transaction_Number is NULL and Transaction_Number is NULL Order by Document_Number Desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstVendorNo.Add(Convert.ToString(reader["Document_Number"]));
                    }
                }

                return lstVendorNo;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public string CheckBankRefNoExists(string strBranchCode, string strBankRefNo, string strAccPeriodCode)
        {
            string BankRefNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Bank_Ref_No from Main_Cash_Details_HO_Branch WITH (NOLOCK) Where To_Branch_Code= '" + strBranchCode + "' and Bank_Ref_No = '" + strBankRefNo + "' and Accounting_Period = " + strAccPeriodCode;
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
             
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BankRefNo = reader[0].ToString();
                }
            }

            return BankRefNo;
        }

        public List<VendorBookingEntity> GetVendorDocuments(string strBranchCode)
        {
            List<VendorBookingEntity> lstvendorNumbers = new List<VendorBookingEntity>();

            VendorBookingEntity objvendorNumber = new VendorBookingEntity();
            objvendorNumber.DocumentNumber = "0";
            objvendorNumber.DocumentNumber = "-- Select --";
            lstvendorNumbers.Add(objvendorNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select Document_Number from Vendor_Invoice_Header WITH (NOLOCK) Where Branch_Code='" + strBranchCode + "' and Status ='A' and Approval_Status='A' Order by Document_Number Desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objvendorNumber = new VendorBookingEntity();
                    objvendorNumber.DocumentNumber = reader[0].ToString();
                    lstvendorNumbers.Add(objvendorNumber);
                }
            }

            return lstvendorNumbers;
        }

        public List<VendorBookingEntity> GetVendorDocumentsForApproval(string strBranchCode)
        {
            List<VendorBookingEntity> lstvendorNumbers = new List<VendorBookingEntity>();

            VendorBookingEntity objvendorNumber = new VendorBookingEntity();
            objvendorNumber.DocumentNumber = "0";
            objvendorNumber.DocumentNumber = "-- Select --";
            lstvendorNumbers.Add(objvendorNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select Document_Number from Vendor_Invoice_Header WITH (NOLOCK) Where Branch_Code='" + strBranchCode + "' and Document_Date>='11/2/2022' and Status ='A' and Paid_Amount IS NULL Order by Document_Number Desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objvendorNumber = new VendorBookingEntity();
                    objvendorNumber.DocumentNumber = reader[0].ToString();
                    lstvendorNumbers.Add(objvendorNumber);
                }
            }

            return lstvendorNumbers;
        }

        public List<VendorBookingEntity> GetVendorPaymentDocuments(string strBranchCode)
        {
            List<VendorBookingEntity> lstvendorNumbers = new List<VendorBookingEntity>();

            VendorBookingEntity objvendorNumber = new VendorBookingEntity();
            objvendorNumber.DocumentNumber = "0";
            objvendorNumber.DocumentNumber = "-- Select --";
            lstvendorNumbers.Add(objvendorNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select distinct Payment_Number from Vendor_Payment_Detail Where Branch_Code='" + strBranchCode + "' Order by Payment_Number Desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objvendorNumber = new VendorBookingEntity();
                    objvendorNumber.DocumentNumber = reader[0].ToString();
                    lstvendorNumbers.Add(objvendorNumber);
                }
            }

            return lstvendorNumbers;
        }

        public List<VendorBookingEntity> GetVendorPaymentDocumentsForApproval(string strBranchCode)
        {
            List<VendorBookingEntity> lstvendorNumbers = new List<VendorBookingEntity>();

            VendorBookingEntity objvendorNumber = new VendorBookingEntity();
            objvendorNumber.DocumentNumber = "0";
            objvendorNumber.DocumentNumber = "-- Select --";
            lstvendorNumbers.Add(objvendorNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select distinct Payment_Number from Vendor_Payment_Detail Where Branch_Code='" + strBranchCode + "' and Status ='A' and Approval_Status IS NULL and	Approval_Remarks IS NULL Order by Payment_Number Desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objvendorNumber = new VendorBookingEntity();
                    objvendorNumber.DocumentNumber = reader[0].ToString();
                    lstvendorNumbers.Add(objvendorNumber);
                }
            }

            return lstvendorNumbers;
        }

        public List<VendorBookingDetail> GetVendorDocumentDetails(string BranchCode, string FromBranchCode, string VendorCode)
        {
            List<VendorBookingDetail> vendorDetail = new List<VendorBookingDetail>();
            VendorBookingDetail vendorbookingDetail = null;

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select ROW_NUMBER() over(partition by Document_Number,Vendor_Code order by Vendor_Code,Document_Number) Serial_Number,Document_Number,convert(varchar(10),Document_Date,103) Document_Date,Invoice_Number,";
            sSQL = sSQL + "convert(varchar(10),Invoice_Date,103) Invoice_Date,isnull(Invoice_Value,0) Invoice_Value,isnull(TDS_Value,0) TDS_Value,isnull(Paid_Amount,0) Paid_Amount,(isnull(Invoice_Value,0)-isnull(TDS_Value,0)-isnull(Paid_Amount,0)) Payment_Amount,Remarks from ";
            sSQL = sSQL + "Vendor_Invoice_Header WITH (NOLOCK) Where Branch_Code='" + FromBranchCode + "' and Vendor_Code= '" + VendorCode + "' and Status ='A' and Approval_Status='A' and Payment_Branch='" + BranchCode + "' and isnull(Invoice_Value,0)-isnull(TDS_Value, 0) - isnull(Paid_Amount, 0) > 0 Order by Document_Date Desc";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    vendorbookingDetail = new VendorBookingDetail();
                    vendorbookingDetail.SerialNumber = dr["Serial_Number"].ToString();
                    vendorbookingDetail.DocumentNumber = dr["Document_Number"].ToString();
                    vendorbookingDetail.DocumentDate = dr["Document_Date"].ToString();
                    vendorbookingDetail.InvoiceNumber = dr["Invoice_Number"].ToString();
                    vendorbookingDetail.InvoiceDate = dr["Invoice_Date"].ToString();
                    vendorbookingDetail.InvoiceValue = TwoDecimalConversion(dr["Invoice_Value"].ToString());
                    vendorbookingDetail.TDSAmount = TwoDecimalConversion(dr["TDS_Value"].ToString());
                    vendorbookingDetail.PaidAmount = TwoDecimalConversion(dr["Paid_Amount"].ToString());
                    vendorbookingDetail.PaymentAmount = TwoDecimalConversion(dr["Payment_Amount"].ToString());
                    vendorbookingDetail.Remarks = dr["Remarks"].ToString();

                    vendorDetail.Add(vendorbookingDetail);
                    index++;
                }
            }

            return vendorDetail;
        }

        public string CheckGSTstatus(string BranchCode, string VendorCode)
        {
            string status = "";
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            sSQL = "select case when ISNUMERIC(substring(v.GSTIN,1,2))=0 then 'L' when len(v.GSTIN)=15 and ISNUMERIC(substring(v.GSTIN,1,2))=1 and substring(b.Local_Sales_Tax_Number,1,2)=substring(v.GSTIN,1,2)";
            sSQL = sSQL + " then 'L' else 'O' end Result From Vendor_Master v WITH(NOLOCK) inner join Branch_Master b WITH(NOLOCK) on b.Branch_Code = v.Branch_Code";
            sSQL = sSQL + " and v.Branch_Code = '" + BranchCode + "' and v.Vendor_Code = '" + VendorCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            status = ImpalDB.ExecuteScalar(cmd1).ToString();

            return status;
        }

        public List<SalesTaxCode> GetSalesTaxCodeGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code>1000");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeSGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where Sales_Tax_Description='SGST TAX' and Sales_Tax_Percentage>1 Order by Sales_Tax_Percentage");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeCGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where Sales_Tax_Description='CGST TAX' and Sales_Tax_Percentage>1 Order by Sales_Tax_Percentage");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeIGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where Sales_Tax_Description='IGST TAX' and Sales_Tax_Percentage>1 Order by Sales_Tax_Percentage");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeUTGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where Sales_Tax_Description='UTGST TAX' and Sales_Tax_Percentage>1 Order by Sales_Tax_Percentage");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public String GetSalesTaxPer(string SalesTaxCode)
        {
            string _SalesTaxCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Sales_Tax_Percentage from sales_tax_Master where sales_tax_code='" + SalesTaxCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            _SalesTaxCode = ImpalDB.ExecuteScalar(cmd).ToString();
            return _SalesTaxCode;
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

        public List<string> GetTDSType(string strBranchCode)
        {
            List<String> tdsType = new List<string>();
            tdsType.Add(string.Empty);
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string sSQL = "select description from chart_of_account a, GL_Account_Master b ";
                sSQL = sSQL + " Where branch_code = '" + strBranchCode + "' and  a.Chart_of_Account_Code like '4093020080%' and  b.gl_main_code = substring(a.chart_of_account_Code, 4,3)";
                sSQL = sSQL + " and b.gl_sub_code = substring(a.chart_of_account_Code, 7,4) and ";
                sSQL = sSQL + " b.gl_account_code = substring(a.chart_of_account_Code, 11,7) ";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        tdsType.Add(Convert.ToString(reader["description"]));
                    }
                }

                return tdsType;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public int CheckDocumentNumber(string VendorCode, string Document_Number, string AccountCode, string BranchCode)
        {
            int Count = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            sSQL = "select 1 from Vendor_Invoice_Header v WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on v.branch_code = '" + BranchCode + "' and v.Vendor_Code = '" + VendorCode + "' and ";
            sSQL = sSQL + "a.Accounting_Period_Code= '" + AccountCode + "'  and v.document_date between a.Start_Date and a.End_Date and ";
            sSQL = sSQL + "v.Invoice_Number = '" + Document_Number + "' and ISNULL(Status,'A')='A'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            Count = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd1));
            return Count;
        }

        public VendorBookingEntity GetVendorBookingHeaderandDetails(string BranchCode, string strDocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetVendorBookingHeaderAndDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, strDocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            VendorBookingEntity vendorbookingHeader = new VendorBookingEntity();
            VendorBookingDetail vendorbookingDetail = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    vendorbookingHeader.DocumentNumber = dr["Document_Number"].ToString();
                    vendorbookingHeader.DocumentDate = dr["Document_Date"].ToString();
                    vendorbookingHeader.BranchCode = dr["Branch_Code"].ToString();
                    vendorbookingHeader.VendorCode = dr["Vendor_Code"].ToString();
                    vendorbookingHeader.VendorName = dr["Vendor_name"].ToString();
                    vendorbookingHeader.Address1 = dr["address1"].ToString();
                    vendorbookingHeader.Address2 = dr["address2"].ToString();
                    vendorbookingHeader.Address3 = dr["address3"].ToString();
                    vendorbookingHeader.Address4 = dr["address4"].ToString();
                    vendorbookingHeader.Location = dr["Location"].ToString();
                    vendorbookingHeader.InvoiceNumber = dr["Invoice_Number"].ToString();
                    vendorbookingHeader.InvoiceDate = dr["Invoice_Date"].ToString();
                    vendorbookingHeader.InvoiceValue = TwoDecimalConversion(dr["Invoice_Value"].ToString());
                    vendorbookingHeader.GSTValue = TwoDecimalConversion(dr["Tax_Value"].ToString());
                    vendorbookingHeader.TDSvalue = TwoDecimalConversion(dr["TDS_Value"].ToString());
                    vendorbookingHeader.GSTINNumber = dr["GSTINNumber"].ToString();
                    vendorbookingHeader.TDStype = dr["TDS_Type"].ToString();
                    vendorbookingHeader.Narration = dr["Remarks"].ToString();
                    vendorbookingHeader.AccountingPeriodCode = dr["Accounting_Period_Code"].ToString();
                    vendorbookingHeader.RCMstatus = dr["RCM_Status"].ToString();
                    vendorbookingHeader.Status = dr["Status"].ToString();
                    vendorbookingHeader.PaymentBranch = dr["Payment_Branch"].ToString();
                    vendorbookingHeader.PaymentDueDate = dr["Payment_DueDate"].ToString();
                    vendorbookingHeader.PaymentAmount = dr["Paid_Amount"].ToString();
                    vendorbookingHeader.PaymentStatus = dr["Paid_Status"].ToString();
                    vendorbookingHeader.AuthorityMatrixStatus = dr["Auth_Matrix_Status"].ToString();

                    vendorbookingHeader.Items = new List<VendorBookingDetail>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        vendorbookingDetail = new VendorBookingDetail();

                        vendorbookingDetail.SerialNumber = drItem["Serial_Number"].ToString();
                        vendorbookingDetail.Chart_of_Account_Code = drItem["Chart_of_Account_Code"].ToString();
                        vendorbookingDetail.Description = drItem["Description"].ToString();
                        vendorbookingDetail.Remarks = drItem["Remarks"].ToString();
                        vendorbookingDetail.Amount = TwoDecimalConversion(drItem["Amount"].ToString());
                        vendorbookingDetail.SGST_Code = drItem["SGSTCode"].ToString();
                        vendorbookingDetail.SGST_Per = drItem["SGSTPer"].ToString();
                        vendorbookingDetail.SGST_Amt = drItem["SGSTAmt"].ToString();
                        vendorbookingDetail.UTGST_Code = drItem["UTGSTCode"].ToString();
                        vendorbookingDetail.UTGST_Per = drItem["UTGSTPer"].ToString();
                        vendorbookingDetail.UTGST_Amt = drItem["UTGSTAmt"].ToString();
                        vendorbookingDetail.CGST_Code = drItem["CGSTCode"].ToString();
                        vendorbookingDetail.CGST_Per = drItem["CGSTPer"].ToString();
                        vendorbookingDetail.CGST_Amt = drItem["CGSTAmt"].ToString();
                        vendorbookingDetail.IGST_Code = drItem["IGSTCode"].ToString();
                        vendorbookingDetail.IGST_Per = drItem["IGSTPer"].ToString();
                        vendorbookingDetail.IGST_Amt = drItem["IGSTAmt"].ToString();                        

                        vendorbookingHeader.Items.Add(vendorbookingDetail);
                    }
                }
            }

            return vendorbookingHeader;
        }

        public List<Item> GetVendorBookingEntriesOtherBranches(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetVendorBookingEntriesOtherBranches");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> VendorBookingEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            VendorBookingEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Vendor_Name"].ToString();
                    objItem.ItemCode = reader["Vendor_Code"].ToString();
                    VendorBookingEntryList.Add(objItem);
                }
            }

            return VendorBookingEntryList;
        }

        public VendorBookingEntity GetVendorPaymentHeaderandDetails(string BranchCode, string strDocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetVendorPaymentHeaderAndDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, strDocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            VendorBookingEntity vendorpaymentHeader = new VendorBookingEntity();
            VendorBookingDetail vendorpaymentDetail = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    vendorpaymentHeader.DocumentNumber = dr["Payment_Number"].ToString();
                    vendorpaymentHeader.DocumentDate = dr["Payment_Date"].ToString();
                    vendorpaymentHeader.BranchCode = dr["Branch_Code"].ToString();
                    vendorpaymentHeader.VendorCode = dr["Vendor_Code"].ToString();
                    vendorpaymentHeader.VendorName = dr["Vendor_name"].ToString();
                    vendorpaymentHeader.Location = dr["Location"].ToString();
                    vendorpaymentHeader.GSTINNumber = dr["GSTIN"].ToString();
                    vendorpaymentHeader.InvoiceValue = TwoDecimalConversion(dr["Payment_Amount"].ToString());
                    vendorpaymentHeader.Narration = dr["Payment_Remarks"].ToString();
                    vendorpaymentHeader.AccountingPeriodCode = dr["Accounting_Period_Code"].ToString();
                    vendorpaymentHeader.Chart_of_Account_Code = dr["Chart_of_Account_Code"].ToString();
                    vendorpaymentHeader.PaymentMode = dr["Payment_Mode"].ToString();
                    vendorpaymentHeader.ChequeNumber = dr["Cheque_Number"].ToString();
                    vendorpaymentHeader.ChequeDate = dr["Cheque_Date"].ToString();
                    vendorpaymentHeader.ChequeBank = dr["Cheque_Bank"].ToString();
                    vendorpaymentHeader.ChequeBranch = dr["Cheque_Branch"].ToString();

                    vendorpaymentHeader.Items = new List<VendorBookingDetail>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        vendorpaymentDetail = new VendorBookingDetail();

                        vendorpaymentDetail.DocumentNumber = drItem["Document_Number"].ToString();
                        vendorpaymentDetail.DocumentDate = drItem["Document_Date"].ToString();
                        vendorpaymentDetail.InvoiceNumber = drItem["Invoice_Number"].ToString();
                        vendorpaymentDetail.InvoiceDate = drItem["Invoice_Date"].ToString();
                        vendorpaymentDetail.InvoiceValue = TwoDecimalConversion(drItem["Invoice_Value"].ToString());
                        vendorpaymentDetail.TDSAmount = TwoDecimalConversion(drItem["TDS_Value"].ToString());
                        vendorpaymentDetail.PaidAmount = TwoDecimalConversion(drItem["Payment_Amount"].ToString());
                        vendorpaymentDetail.Remarks = drItem["Remarks"].ToString();

                        vendorpaymentHeader.Items.Add(vendorpaymentDetail);
                    }
                }
            }

            return vendorpaymentHeader;
        }

        public int AddNewVendorBookingEntry(ref VendorBookingEntity vendorbookingHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            string DocumentNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    DbCommand cmdHeader = ImpalDb.GetStoredProcCommand("usp_addVendorBooking_Header");
                    ImpalDb.AddInParameter(cmdHeader, "@Document_Number", DbType.String, DocumentNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Document_Date", DbType.String, vendorbookingHeader.DocumentDate);
                    ImpalDb.AddInParameter(cmdHeader, "@Transaction_Type_Code", DbType.String, vendorbookingHeader.TransactionTypeCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Vendor_Code", DbType.String, vendorbookingHeader.VendorCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Vendor_Name", DbType.String, vendorbookingHeader.VendorName);
                    ImpalDb.AddInParameter(cmdHeader, "@Place", DbType.String, vendorbookingHeader.Location);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Number", DbType.String, vendorbookingHeader.InvoiceNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Date", DbType.String, vendorbookingHeader.InvoiceDate);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Value", DbType.String, vendorbookingHeader.InvoiceValue);
                    ImpalDb.AddInParameter(cmdHeader, "@GST_Value", DbType.String, vendorbookingHeader.GSTValue);
                    ImpalDb.AddInParameter(cmdHeader, "@RoundingOff_Value", DbType.String, vendorbookingHeader.RoundingOffValue);
                    ImpalDb.AddInParameter(cmdHeader, "@TDS_Value", DbType.String, vendorbookingHeader.TDSvalue);
                    ImpalDb.AddInParameter(cmdHeader, "@TDS_Type", DbType.String, vendorbookingHeader.TDStype);
                    ImpalDb.AddInParameter(cmdHeader, "@GSTINNumber", DbType.String, vendorbookingHeader.GSTINNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Remarks", DbType.String, vendorbookingHeader.Narration);
                    ImpalDb.AddInParameter(cmdHeader, "@Indicator", DbType.String, "A");
                    ImpalDb.AddInParameter(cmdHeader, "@RCM_Status", DbType.String, vendorbookingHeader.RCMstatus);
					ImpalDb.AddInParameter(cmdHeader, "@Payment_Branch", DbType.String, vendorbookingHeader.PaymentBranch);
                    ImpalDb.AddInParameter(cmdHeader, "@Payment_DueDate", DbType.String, vendorbookingHeader.PaymentDueDate);																															 
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DocumentNumber = ImpalDb.ExecuteScalar(cmdHeader).ToString();

                    foreach (VendorBookingDetail vendorbookingDetail in vendorbookingHeader.Items)
                    {
                        DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_addVendorBooking_Detail");
                        ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd, "@Serial_Number", DbType.Int64, Convert.ToInt64(vendorbookingDetail.SerialNumber));
                        ImpalDb.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, vendorbookingDetail.Chart_of_Account_Code);
                        ImpalDb.AddInParameter(cmd, "@Description", DbType.String, vendorbookingDetail.Description);
                        ImpalDb.AddInParameter(cmd, "@Debit_Credit_Indicator", DbType.String, vendorbookingDetail.Dr_Cr);
                        ImpalDb.AddInParameter(cmd, "@Remarks", DbType.String, vendorbookingDetail.Remarks);
                        ImpalDb.AddInParameter(cmd, "@Amount", DbType.String, vendorbookingDetail.Amount);
                        ImpalDb.AddInParameter(cmd, "@SGST_Code", DbType.String, vendorbookingDetail.SGST_Code);
                        ImpalDb.AddInParameter(cmd, "@SGST_Per", DbType.String, vendorbookingDetail.SGST_Per);
                        ImpalDb.AddInParameter(cmd, "@SGST_Amt", DbType.String, vendorbookingDetail.SGST_Amt);
                        ImpalDb.AddInParameter(cmd, "@CGST_Code", DbType.String, vendorbookingDetail.CGST_Code);
                        ImpalDb.AddInParameter(cmd, "@CGST_Per", DbType.String, vendorbookingDetail.CGST_Per);
                        ImpalDb.AddInParameter(cmd, "@CGST_Amt", DbType.String, vendorbookingDetail.CGST_Amt);
                        ImpalDb.AddInParameter(cmd, "@IGST_Code", DbType.String, vendorbookingDetail.IGST_Code);
                        ImpalDb.AddInParameter(cmd, "@IGST_Per", DbType.String, vendorbookingDetail.IGST_Per);
                        ImpalDb.AddInParameter(cmd, "@IGST_Amt", DbType.String, vendorbookingDetail.IGST_Amt);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd);
                    }

                    vendorbookingHeader.DocumentNumber = DocumentNumber;
                    vendorbookingHeader.ErrorCode = "0";
                    vendorbookingHeader.ErrorMsg = "";
                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int AddNewVendorBookingEntryFinal(ref VendorBookingEntity vendorbookingHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            int Status = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    DbCommand cmdHeader = ImpalDb.GetStoredProcCommand("usp_updVendorBooking_Header");
                    ImpalDb.AddInParameter(cmdHeader, "@Document_Number", DbType.String, vendorbookingHeader.DocumentNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Document_Date", DbType.String, vendorbookingHeader.DocumentDate);
                    ImpalDb.AddInParameter(cmdHeader, "@Transaction_Type_Code", DbType.String, vendorbookingHeader.TransactionTypeCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Vendor_Code", DbType.String, vendorbookingHeader.VendorCode);
                    ImpalDb.AddInParameter(cmdHeader, "@Vendor_Name", DbType.String, vendorbookingHeader.VendorName);
                    ImpalDb.AddInParameter(cmdHeader, "@Place", DbType.String, vendorbookingHeader.Location);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Number", DbType.String, vendorbookingHeader.InvoiceNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Date", DbType.String, vendorbookingHeader.InvoiceDate);
                    ImpalDb.AddInParameter(cmdHeader, "@Invoice_Value", DbType.String, vendorbookingHeader.InvoiceValue);
                    ImpalDb.AddInParameter(cmdHeader, "@GST_Value", DbType.String, vendorbookingHeader.GSTValue);
                    ImpalDb.AddInParameter(cmdHeader, "@RoundingOff_Value", DbType.String, vendorbookingHeader.RoundingOffValue);
                    ImpalDb.AddInParameter(cmdHeader, "@TDS_Value", DbType.String, vendorbookingHeader.TDSvalue);
                    ImpalDb.AddInParameter(cmdHeader, "@TDS_Type", DbType.String, vendorbookingHeader.TDStype);
                    ImpalDb.AddInParameter(cmdHeader, "@GSTINNumber", DbType.String, vendorbookingHeader.GSTINNumber);
                    ImpalDb.AddInParameter(cmdHeader, "@Remarks", DbType.String, vendorbookingHeader.Narration);
                    ImpalDb.AddInParameter(cmdHeader, "@Indicator", DbType.String, "A");
                    ImpalDb.AddInParameter(cmdHeader, "@RCM_Status", DbType.String, vendorbookingHeader.RCMstatus);
                    ImpalDb.AddInParameter(cmdHeader, "@Payment_Branch", DbType.String, vendorbookingHeader.PaymentBranch);
                    ImpalDb.AddInParameter(cmdHeader, "@Payment_DueDate", DbType.String, vendorbookingHeader.PaymentDueDate);
                    ImpalDb.AddInParameter(cmdHeader, "@ApprovalLevel", DbType.String, vendorbookingHeader.ApprovalLevel);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Status = Convert.ToInt16(ImpalDb.ExecuteScalar(cmdHeader).ToString());

                    if (Status == 1)
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addglvb");
                        ImpalDb.AddInParameter(cmd1, "@doc_no", DbType.String, vendorbookingHeader.DocumentNumber);
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd1);
                    }
                    
                    vendorbookingHeader.ErrorCode = "0";
                    vendorbookingHeader.ErrorMsg = "";
                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int CancelVendorBookingEntry(string BranchCode, string VendorBookingNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Database ImpalDb = null;
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_CancelVendorBooking");
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, VendorBookingNumber);
                    ImpalDb.AddInParameter(cmd, "@Status", DbType.String, status);
                    ImpalDb.AddInParameter(cmd, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDb.AddInParameter(cmd, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int AddVendorPaymentEntry(ref VendorBookingEntity vendorbookingHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            string DocumentNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    foreach (VendorBookingDetail vendorbookingDetail in vendorbookingHeader.Items)
                    {
                        DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_AddVendorPayment");
                        ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd, "@Document_Date", DbType.String, vendorbookingHeader.PaymentDate);
                        ImpalDb.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, vendorbookingHeader.TransactionTypeCode);
                        ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd, "@Serial_Number", DbType.Int64, Convert.ToInt64(vendorbookingDetail.SerialNumber));
                        ImpalDb.AddInParameter(cmd, "@Vendor_Code", DbType.String, vendorbookingHeader.VendorCode);
                        ImpalDb.AddInParameter(cmd, "@Vendor_Name", DbType.String, vendorbookingHeader.VendorName);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Number", DbType.String, vendorbookingDetail.InvoiceNumber);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Date", DbType.String, vendorbookingDetail.InvoiceDate);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, vendorbookingDetail.InvoiceValue);
                        ImpalDb.AddInParameter(cmd, "@TDS_Amount", DbType.String, vendorbookingDetail.TDSAmount);
                        ImpalDb.AddInParameter(cmd, "@Payment_Amount", DbType.String, vendorbookingDetail.PaymentAmount);
                        ImpalDb.AddInParameter(cmd, "@Payment_Mode", DbType.String, vendorbookingHeader.PaymentMode);
                        ImpalDb.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, vendorbookingHeader.Chart_of_Account_Code);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Number", DbType.String, vendorbookingHeader.ChequeNumber);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Date", DbType.String, vendorbookingHeader.ChequeDate);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Bank", DbType.String, vendorbookingHeader.ChequeBank);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Branch", DbType.String, vendorbookingHeader.ChequeBranch);
                        ImpalDb.AddInParameter(cmd, "@Payment_Remarks", DbType.String, vendorbookingHeader.Narration);
						ImpalDb.AddInParameter(cmd, "@Payment_Branch", DbType.String, vendorbookingHeader.PaymentBranch);
                        ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, "P");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocumentNumber = ImpalDb.ExecuteScalar(cmd).ToString();
                    }

                    if (DocumentNumber != "")
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addglvp");
                        ImpalDb.AddInParameter(cmd1, "@doc_no", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        result = ImpalDb.ExecuteNonQuery(cmd1);
                    }

                    vendorbookingHeader.DocumentNumber = DocumentNumber;
                    vendorbookingHeader.ErrorCode = "0";
                    vendorbookingHeader.ErrorMsg = "";
                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int AddVendorPaymentEntryFinal(ref VendorBookingEntity vendorbookingHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            string DocumentNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    foreach (VendorBookingDetail vendorbookingDetail in vendorbookingHeader.Items)
                    {
                        DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_updVendorPayment");
                        ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd, "@Document_Date", DbType.String, vendorbookingHeader.PaymentDate);
                        ImpalDb.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, vendorbookingHeader.TransactionTypeCode);
                        ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd, "@Serial_Number", DbType.Int64, Convert.ToInt64(vendorbookingDetail.SerialNumber));
                        ImpalDb.AddInParameter(cmd, "@Vendor_Code", DbType.String, vendorbookingHeader.VendorCode);
                        ImpalDb.AddInParameter(cmd, "@Vendor_Name", DbType.String, vendorbookingHeader.VendorName);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Number", DbType.String, vendorbookingDetail.InvoiceNumber);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Date", DbType.String, vendorbookingDetail.InvoiceDate);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, vendorbookingDetail.InvoiceValue);
                        ImpalDb.AddInParameter(cmd, "@TDS_Amount", DbType.String, vendorbookingDetail.TDSAmount);
                        ImpalDb.AddInParameter(cmd, "@Payment_Amount", DbType.String, vendorbookingDetail.PaymentAmount);
                        ImpalDb.AddInParameter(cmd, "@Payment_Mode", DbType.String, vendorbookingHeader.PaymentMode);
                        ImpalDb.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, vendorbookingHeader.Chart_of_Account_Code);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Number", DbType.String, vendorbookingHeader.ChequeNumber);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Date", DbType.String, vendorbookingHeader.ChequeDate);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Bank", DbType.String, vendorbookingHeader.ChequeBank);
                        ImpalDb.AddInParameter(cmd, "@Cheque_Branch", DbType.String, vendorbookingHeader.ChequeBranch);
                        ImpalDb.AddInParameter(cmd, "@Payment_Remarks", DbType.String, vendorbookingHeader.Narration);
                        ImpalDb.AddInParameter(cmd, "@Payment_Branch", DbType.String, vendorbookingHeader.PaymentBranch);
                        ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, "P");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocumentNumber = ImpalDb.ExecuteScalar(cmd).ToString();
                    }

                    if (DocumentNumber != "")
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addglvp");
                        ImpalDb.AddInParameter(cmd1, "@doc_no", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, vendorbookingHeader.BranchCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        result = ImpalDb.ExecuteNonQuery(cmd1);
                    }

                    vendorbookingHeader.DocumentNumber = DocumentNumber;
                    vendorbookingHeader.ErrorCode = "0";
                    vendorbookingHeader.ErrorMsg = "";
                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int CancelVendorPaymentEntry(string BranchCode, string VendorBookingNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Database ImpalDb = null;
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_CancelVendorPayment");
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, VendorBookingNumber);
                    ImpalDb.AddInParameter(cmd, "@Status", DbType.String, status);
                    ImpalDb.AddInParameter(cmd, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDb.AddInParameter(cmd, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }
    }
}
