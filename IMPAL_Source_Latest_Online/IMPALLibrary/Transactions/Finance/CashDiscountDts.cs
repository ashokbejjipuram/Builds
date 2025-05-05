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

namespace IMPALLibrary.Transactions.Finance
{
    public class CashDiscountDts
    {
        public CashDiscountDts()
        {
        }

        public List<CDDiscountDetails> GetCDDiscountDetails(string CustomerCode, string FromDate, string ToDate, string strBranchCode)
        {
            List<CDDiscountDetails> CDDiscountCollection = new List<CDDiscountDetails>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCDdealersdocumentDetails");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
                ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        CDDiscountDetails CDDiscountDetails = new CDDiscountDetails();
                        CDDiscountDetails.Document_Number = reader["Document_Number"].ToString();
                        CDDiscountDetails.Document_Date = reader["Document_Date"].ToString();
                        CDDiscountDetails.order_value = reader["order_value"].ToString();
                        CDDiscountDetails.Collection_Amount = reader["Collection_Amount"].ToString();
                        CDDiscountDetails.noofdays = reader["noofdays"].ToString();
                        CDDiscountCollection.Add(CDDiscountDetails);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return CDDiscountCollection;
        }

        public List<CashPercentage> GetCashPercentage(string CustomerCode, string Indicator, string BranchCode, int NoOfDays, string Status, string DocumentDate)
        {
            List<CashPercentage> CashPerCollection = new List<CashPercentage>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCDPercentage_CDCN");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
            ImpalDB.AddInParameter(cmd, "@NoOfDays", DbType.String, NoOfDays);
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, Status);
            ImpalDB.AddInParameter(cmd, "@DocumentDate", DbType.String, DocumentDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CashPercentage CashDiscountPercentage = new CashPercentage();
                    CashDiscountPercentage.cd_percentage = reader["cd_percentage"].ToString();
                    CashPerCollection.Add(CashDiscountPercentage);
                }
            }
            return CashPerCollection;
        }

        public List<CDAccStartEndDate> AccountingStartEnddate(string AccountPeriodCode)
        {
            List<CDAccStartEndDate> AccountingPeriodStateDate = new List<CDAccStartEndDate>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select convert(varchar(10),DATEADD(yy,-1,Start_Date),103),convert(varchar(10),End_Date,103) from accounting_period where accounting_period_code='" + AccountPeriodCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CDAccStartEndDate CDStartEndDates = new CDAccStartEndDate();
                    CDStartEndDates.Start_Date = reader[0].ToString();
                    CDStartEndDates.End_Date = reader[1].ToString();
                    AccountingPeriodStateDate.Add(CDStartEndDates);
                }
            }
            return AccountingPeriodStateDate;
        }

        public string GetReceiptNumber(string DrCrIndicator, string AccountingPeriod, string BranchCode, string CrYear)
        {
            string ReceiptNumber = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "SELECT case when Len(Last_number+1) < 5 then REPLICATE('0', 5-LEN(Last_number+1)) +  " +
                    "CAST(Last_number+1 as varchar) else CAST(Last_number+1 as varchar) end  FROM parameter where  " +
                    "Accounting_Period_Code='" + AccountingPeriod + "' and Branch_Code='" + BranchCode + "' and  Parameter_Code=30";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            string CrNumber = ImpalDB.ExecuteScalar(cmd).ToString();

            ReceiptNumber = CrYear + "/" + CrNumber + "/" + BranchCode + "/CD";
            return ReceiptNumber;
        }

        public string AddNewCashDiscount(ref CashDiscountEntity cashdiscountEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string DocumentNumber = "";
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (CashDiscountItem cdItem in cashdiscountEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCreditAdvice_cd_Entry1");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, cashdiscountEntity.Transaction_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, cashdiscountEntity.Dr_Cr_Indicator);
                        ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.DateTime, cashdiscountEntity.Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, cashdiscountEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, cashdiscountEntity.Customer_Code);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, cashdiscountEntity.Indicator);
                        ImpalDB.AddInParameter(cmd, "@AccPeriodCode", DbType.String, cashdiscountEntity.AccPeriodCode);
                        ImpalDB.AddInParameter(cmd, "@AccPeriodYear", DbType.String, cashdiscountEntity.AccPeriodYear);
                        ImpalDB.AddInParameter(cmd, "FromDate", DbType.String, cashdiscountEntity.FromDate);
                        ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, cashdiscountEntity.ToDate);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, cdItem.Reference_Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Document_Value", DbType.String, cdItem.Document_Value);
                        ImpalDB.AddInParameter(cmd, "@Collection_Amount", DbType.String, cdItem.Collection_Amount);
                        ImpalDB.AddInParameter(cmd, "@Days", DbType.String, cdItem.Days);
                        ImpalDB.AddInParameter(cmd, "@CD_Per", DbType.String, cdItem.CD_Per);
                        ImpalDB.AddInParameter(cmd, "@Delay_Days", DbType.String, cdItem.Delay_Days);
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, cdItem.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, cdItem.Adjustment_Value);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.DateTime, DateTime.ParseExact(cdItem.Reference_Document_Date, "dd/MM/yyyy", null));
                        ImpalDB.AddInParameter(cmd, "@Value", DbType.String, cdItem.Value);
                        ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, cdItem.Chart_Of_Account_Code);
                        ImpalDB.AddInParameter(cmd, "@Cnt", DbType.Int16, cdItem.Cnt);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocumentNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    cashdiscountEntity.ErrorCode = "0";
                    cashdiscountEntity.ErrorMsg = "";

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                cashdiscountEntity.ErrorCode = "1";
                cashdiscountEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return DocumentNumber;
        }

        public CashDiscountEntity GetCDDiscountDetailsCustomer(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCustomerCDdetails");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@DocumentNumber", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            CashDiscountEntity cdDiscountEntity = new CashDiscountEntity();
            CashDiscountItem cdDiscountItem = null;

            cdDiscountEntity.Items = new List<CashDiscountItem>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    cdDiscountEntity.Branch_Code = dr["Branch_Name"].ToString();
                    cdDiscountEntity.Customer_Code = dr["Customer_Code"].ToString();
                    cdDiscountEntity.Document_Number = dr["CD_CN_Number"].ToString();
                    cdDiscountEntity.DocumentDate = dr["Document_Date"].ToString();
                    cdDiscountEntity.AccPeriodCode = Convert.ToInt32(dr["Accounting_Period_Code"].ToString());
                    cdDiscountEntity.Value = TwoDecimalConversion(dr["Value"].ToString());
                    cdDiscountEntity.Indicator = dr["Indicator"].ToString();
                }
            }

            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow reader in ds.Tables[1].Rows)
                    {
                        cdDiscountItem = new CashDiscountItem();
                        cdDiscountItem.Reference_Document_Number = Convert.ToString(reader["Invoice_Number"]);
                        cdDiscountItem.Reference_Document_Date = Convert.ToString(reader["Invoice_Date"]);
                        cdDiscountItem.Value = TwoDecimalConversion(Convert.ToString(reader["Order_Value"]));
                        cdDiscountItem.Collection_Amount = TwoDecimalConversion(Convert.ToString(reader["Collected_Amount"]));
                        cdDiscountItem.Days = Convert.ToString(reader["Number_of_Days"]);
                        cdDiscountItem.CD_Per = Convert.ToString(reader["CD_Percent"]);
                        cdDiscountItem.Adjustment_Value = TwoDecimalConversion(Convert.ToString(reader["CD_Value"]));
                        cdDiscountItem.Delay_Days = Convert.ToString(reader["Delay_Days"]);
                        cdDiscountEntity.Items.Add(cdDiscountItem);
                    }
                }
            }

            return cdDiscountEntity;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public int AddNewCashDiscountFinal(ref CashDiscountEntity cashdiscountEntity, decimal CDtotItemValue)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (CashDiscountItem cdItem in cashdiscountEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCreditAdvice_cd");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, cashdiscountEntity.Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, cashdiscountEntity.Transaction_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, cashdiscountEntity.Dr_Cr_Indicator);
                        ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.DateTime, cashdiscountEntity.Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, cashdiscountEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, cashdiscountEntity.Customer_Code);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, cashdiscountEntity.Indicator);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, cdItem.Reference_Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Document_Value", DbType.String, cdItem.Document_Value);
                        ImpalDB.AddInParameter(cmd, "@Collection_Amount", DbType.String, cdItem.Collection_Amount);
                        ImpalDB.AddInParameter(cmd, "@Days", DbType.String, cdItem.Days);
                        ImpalDB.AddInParameter(cmd, "@CD_Per", DbType.String, cdItem.CD_Per);
                        ImpalDB.AddInParameter(cmd, "@Delay_Days", DbType.String, cdItem.Delay_Days);
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, cdItem.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, CDtotItemValue);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.DateTime, DateTime.ParseExact(cdItem.Reference_Document_Date, "dd/MM/yyyy", null));
                        ImpalDB.AddInParameter(cmd, "@Value", DbType.String, cdItem.Value);
                        ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, cdItem.Chart_Of_Account_Code);
                        ImpalDB.AddInParameter(cmd, "@Cnt", DbType.Int16, cdItem.Cnt);
                        ImpalDB.AddInParameter(cmd, "@AccPeriodCode", DbType.String, cashdiscountEntity.AccPeriodCode);
                        ImpalDB.AddInParameter(cmd, "@AccPeriodYear", DbType.String, cashdiscountEntity.AccPeriodYear);
                        ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, cashdiscountEntity.ApprovalLevel.Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }

                    DbCommand cmdDel = ImpalDB.GetSqlStringCommand("Delete From Cash_Discount_Cust Where Branch_Code = '" + cashdiscountEntity.Branch_Code + "' and CD_CN_Number = '" + cashdiscountEntity.Document_Number + "' and Customer_Code = '" + cashdiscountEntity.Customer_Code + "' and Approval_Status is NULL and Approval_Remarks is NULL");
                    cmdDel.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdDel);

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_updcustos_daca_CD");
                    ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, cashdiscountEntity.Branch_Code);
                    ImpalDB.AddInParameter(cmd1, "@Doc_No", DbType.String, cashdiscountEntity.Document_Number);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    DbCommand cmd3 = ImpalDB.GetStoredProcCommand("usp_addgldaca1_cd");
                    ImpalDB.AddInParameter(cmd3, "@Doc_No", DbType.String, cashdiscountEntity.Document_Number);
                    ImpalDB.AddInParameter(cmd3, "@Branch_Code", DbType.String, cashdiscountEntity.Branch_Code);
                    cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd3);

                    cashdiscountEntity.ErrorCode = "0";
                    cashdiscountEntity.ErrorMsg = "";

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                cashdiscountEntity.ErrorCode = "1";
                cashdiscountEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return result;
        }

        public void UpdCdEntry(string BranchCode, string CDNumber, string Remarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdCancelCD");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@CD_Number", DbType.String, CDNumber);
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

        public DataSet GetCashCdCustValue(string BranchCode, string DocumentNumber)
        {
            DataSet dt = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select invoice_number,convert(varchar,invoice_date,103) as invoice_date,order_value,collected_Amount,Number_of_days,cd_percent, " +
                   "cd_value,delay_days from Cash_Discount_Cust WITH (NOLOCK) Where Branch_Code= '" + BranchCode  + "' and CD_CN_Number='" + DocumentNumber + "' Order by Invoice_Number";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            dt = ImpalDB.ExecuteDataSet(cmd);
            return dt;
        }

        public DataSet GetCashCdCustValueFinal(string BranchCode, string DocumentNumber)
        {
            DataSet dt = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select invoice_number,convert(varchar,invoice_date,103) as invoice_date,order_value,collected_Amount,Number_of_days,cd_percent, " +
                   "cd_value,delay_days from Cash_Discount_Cust WITH (NOLOCK) Where Branch_Code= '" + BranchCode + "' and CD_CN_Number='" + DocumentNumber + "' Order by Invoice_Number";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            dt = ImpalDB.ExecuteDataSet(cmd);
            return dt;
        }
    }

    public class CDDiscountDetails
    {
        public CDDiscountDetails()
        {

        }
        public string Document_Number { get; set; }
        public string Document_Date { get; set; }
        public string order_value { get; set; }
        public string Collection_Amount { get; set; }
        public string noofdays { get; set; }
    }

    public class CDAccStartEndDate
    {
        public CDAccStartEndDate()
        {

        }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
    }

    public class CashPercentage
    {
        public CashPercentage()
        {
        }
        public string cd_percentage { get; set; }
    }

    public class CashDiscountEntity
    {
        public int AccPeriodCode { get; set; }
        public int AccPeriodYear { get; set; }
        public string Document_Number { get; set; }
        public string DocumentDate { get; set; }
        public string Transaction_Type_Code { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public DateTime Document_Date { get; set; }
        public string Branch_Code { get; set; }
        public string Customer_Code { get; set; }
        public string Indicator { get; set; }
        public string Value { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ApprovalLevel { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<CashDiscountItem> Items { get; set; }
    }

    public class CashDiscountItem
    {        
        public string Reference_Document_Number { get; set; }
        public string Document_Value { get; set; }
        public string Collection_Amount { get; set; }
        public string Days { get; set; }
        public string CD_Per { get; set; }
        public string Delay_Days { get; set; }
        public string Remarks { get; set; }
        public string Adjustment_Value { get; set; }
        public string Reference_Document_Date { get; set; }
        public string Value { get; set; }
        public string Chart_Of_Account_Code { get; set; }
        public int Cnt { get; set; }
        public string Serial_Number { get; set; }
    }
}
