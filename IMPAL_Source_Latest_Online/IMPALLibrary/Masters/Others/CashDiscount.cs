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


namespace IMPALLibrary.Masters.Others
{
    public class CashDiscountMaster
    {
        public CustomerSlabMaster objCustomerSlabMaster = new CustomerSlabMaster();

        public void AddNewCashDiscountMaster(string CashDiscCode, string CashDiscDesc, string Norms, string Indicator, string DiscountPer, string CustomerDueDays, string SupDueDays, string LineItemCode, string LineItemDesc, string CalcIndicator, string PurchaseIndicator, string PaymentIndicator, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            if (Indicator == "C")
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addcashdiscount1");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Cash_Discount_Description", DbType.String, CashDiscDesc.Trim());
                ImpalDB.AddInParameter(cmd, "@CD_Percentage", DbType.String, DiscountPer.Trim());
                ImpalDB.AddInParameter(cmd, "@Number_of_Days", DbType.String, CustomerDueDays.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Dealer_Indicator", DbType.String, Indicator.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            else if (Indicator != "C")
            {
                DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addcashdiscount");
                ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, BranchCode.Trim());
                ImpalDB.AddInParameter(cmd1, "@Cash_Discount_Description", DbType.String, CashDiscDesc.Trim());
                ImpalDB.AddInParameter(cmd1, "@Supplier_Line_Code", DbType.String, LineItemCode.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Percentage", DbType.String, DiscountPer.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Calculation_Indicator", DbType.String, CalcIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Number_of_Days", DbType.String, CustomerDueDays.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Indicator", DbType.String, PurchaseIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Payment_Indicator", DbType.String, PaymentIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Supplier_Dealer_Indicator", DbType.String, Indicator.Trim());
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd1);
            }
        }


        public void UpdateCashDiscountMaster(string BranchCode, string CashDiscCode, string CashDiscDesc, string Norms, string Indicator, string DiscountPer, string CustomerDueDays, string SupDueDays, string LineItemCode, string LineItemDesc, string CalcIndicator, string PurchaseIndicator, string PaymentIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            if (Indicator == "C")
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updcashdiscount1");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Cash_Discount_Code", DbType.String, CashDiscCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Cash_Discount_Description", DbType.String, CashDiscDesc.Trim());
                ImpalDB.AddInParameter(cmd, "@CD_Percentage", DbType.String, DiscountPer.Trim());
                ImpalDB.AddInParameter(cmd, "@Number_of_Days", DbType.String, CustomerDueDays.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Dealer_Indicator", DbType.String, Indicator.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            else if (Indicator != "C")
            {
                DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_Updcashdiscount");
                ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, BranchCode.Trim());
                ImpalDB.AddInParameter(cmd1, "@Cash_Discount_Code", DbType.String, CashDiscCode.Trim());
                ImpalDB.AddInParameter(cmd1, "@Cash_Discount_Description", DbType.String, CashDiscDesc.Trim());
                ImpalDB.AddInParameter(cmd1, "@Supplier_Line_Code", DbType.String, LineItemCode.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Percentage", DbType.String, DiscountPer.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Calculation_Indicator", DbType.String, CalcIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Number_of_Days", DbType.String, CustomerDueDays.Trim());
                ImpalDB.AddInParameter(cmd1, "@CD_Indicator", DbType.String, PurchaseIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Payment_Indicator", DbType.String, PaymentIndicator.Trim());
                ImpalDB.AddInParameter(cmd1, "@Supplier_Dealer_Indicator", DbType.String, Indicator.Trim());
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd1);
            }
        }


        public List<CashDiscountCode> GetCashDiscountCode()
        {
            List<CashDiscountCode> objCashDiscountCodeList = new List<CashDiscountCode>();

            string sSql = "SELECT CASH_DISCOUNT_CODE,CASH_DISCOUNT_DESCRIPTION FROM CASH_DISCOUNT ORDER BY CASH_DISCOUNT_CODE";

            Database ImpalDB = DataAccess.GetDatabase();

            CashDiscountCode CashDiscountCodeList = new CashDiscountCode();
            CashDiscountCodeList.CashDiscName = "-- Select --";
            CashDiscountCodeList.CashDiscCode = "0";
            objCashDiscountCodeList.Add(CashDiscountCodeList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    CashDiscountCodeList = new CashDiscountCode();
                    CashDiscountCodeList.CashDiscCode = reader[0].ToString();
                    CashDiscountCodeList.CashDiscName = reader[1].ToString();
                    objCashDiscountCodeList.Add(CashDiscountCodeList);
                }
            }
            return objCashDiscountCodeList;
        }

        public List<CashDiscountSupLine> GetSupLine()
        {

            List<CashDiscountSupLine> objSupplyLineCodeList = new List<CashDiscountSupLine>();

            string sSql = "SELECT SUPPLIER_CODE FROM SUPPLIER_MASTER Order by Supplier_Code";

            Database ImpalDB = DataAccess.GetDatabase();

            CashDiscountSupLine SupplyLineCodeList = new CashDiscountSupLine();
            SupplyLineCodeList.SupplierLineName = "";
            SupplyLineCodeList.SupplierLineCode = "0";
            objSupplyLineCodeList.Add(SupplyLineCodeList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplyLineCodeList = new CashDiscountSupLine();
                    SupplyLineCodeList.SupplierLineCode = reader[0].ToString();
                    SupplyLineCodeList.SupplierLineName = reader[0].ToString();
                    objSupplyLineCodeList.Add(SupplyLineCodeList);
                }
            }
            return objSupplyLineCodeList;
        }

        public CashDiscount GetCashDicount(string BranchCode, string pCashDiscountCode)
        {

            CashDiscount objCashDiscount=null;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Getcashdiscount");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Cash_Discount_Code", DbType.String, pCashDiscountCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                objCashDiscount = new CashDiscount();
                objCashDiscount.CashDiscDesc = ds.Tables[0].Rows[0]["Cash_Discount_Description"].ToString();
                objCashDiscount.LineItemCode = ds.Tables[0].Rows[0]["Supplier_Line_Code"].ToString();
                objCashDiscount.DiscountPer = ds.Tables[0].Rows[0]["CD_Percentage"].ToString();
                objCashDiscount.Indicator = ds.Tables[0].Rows[0]["CD_Calculation_Indicator"].ToString();
                objCashDiscount.SupDueDays = ds.Tables[0].Rows[0]["Number_of_Days"].ToString();
                objCashDiscount.CalcIndicator = ds.Tables[0].Rows[0]["CD_Indicator"].ToString();
                objCashDiscount.PurchaseIndicator = ds.Tables[0].Rows[0]["Payment_Indicator"].ToString();
                objCashDiscount.PaymentIndicator = ds.Tables[0].Rows[0]["Supplier_Dealer_Indicator"].ToString();
            }

            return objCashDiscount;
        }
        /// <summary>
        /// This method is to get Cash discount in  Chequeslip.aspx in Finance\Payable
        /// </summary>
        /// <returns></returns>
        public List<CashDiscountCode> GetCashDiscountCheque(string strBranchCode)
        {
            List<CashDiscountCode> objCashDiscountCodeList = new List<CashDiscountCode>();

            string sSql = "Select cash_discount_code, Cash_Discount_Description, CD_Percentage from cash_discount where branch_code = '" + strBranchCode + "' order by Cash_Discount_Code";

            Database ImpalDB = DataAccess.GetDatabase();

            CashDiscountCode CashDiscountCodeList = new CashDiscountCode();

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    CashDiscountCodeList = new CashDiscountCode();
                    CashDiscountCodeList.CashDiscCode = reader[0].ToString();
                    CashDiscountCodeList.CashDiscName = reader[1].ToString();
                    CashDiscountCodeList.CashDiscPer = reader[2].ToString();
                    objCashDiscountCodeList.Add(CashDiscountCodeList);
                }
            }
            return objCashDiscountCodeList;
        }

    }


    public class CashDiscount
    {
        public string CashDiscCode { get; set; }
        public string CashDiscDesc { get; set; }
       // public string CashDiscPer { get; set; }
        public string Norms { get; set; }
        public string Indicator { get; set; }
        public string DiscountPer { get; set; }
        public string CustomerDueDays { get; set; }
        public string SupDueDays { get; set; }
        public string LineItemCode { get; set; }
        public string LineItemDesc { get; set; }
        public string CalcIndicator { get; set; }
        public string PurchaseIndicator { get; set; }
        public string PaymentIndicator { get; set; }


        public CashDiscount() { }

        public CashDiscount(string pCashDiscCode, string pCashDiscDesc, string pNorms, string pIndicator, string pDiscountPer, string pCustomerDueDays, string pSupDueDays, string pLineItemCode, string pLineItemDesc, string pCalcIndicator, string pPurchaseIndicator, string pPaymentIndicator)
        {
            CashDiscCode = pCashDiscCode;
            CashDiscDesc = pCashDiscDesc;
            Norms = pNorms;
            Indicator = pIndicator;
            DiscountPer = pDiscountPer;
            CustomerDueDays = pCustomerDueDays;
            SupDueDays = pSupDueDays;
            LineItemCode = pLineItemCode;
            LineItemDesc = pLineItemDesc;
            CalcIndicator = pCalcIndicator;
            PurchaseIndicator = pPurchaseIndicator;
            PaymentIndicator = pPaymentIndicator;
        }


        public CashDiscount(string pCashDiscDesc, string pLineItemCode, string pDiscountPer, string pIndicator, string pSupDueDays, string pCalcIndicator, string pPurchaseIndicator, string pPaymentIndicator)
        {
            CashDiscDesc = pCashDiscDesc;
            LineItemCode = pLineItemCode;
            DiscountPer = pDiscountPer;
            Indicator = pIndicator;
            SupDueDays = pSupDueDays;
            CalcIndicator = pCalcIndicator;
            PurchaseIndicator = pPurchaseIndicator;
            PaymentIndicator = pPaymentIndicator;
        }

    }


    public class CashDiscountCode
    {
        public string CashDiscCode { get; set; }
        public string CashDiscName { get; set; }
        public string CashDiscPer { get; set; }
    }


    public class LineItem
    {
        public string LineItemCode { get; set; }
        public string LineItemDesc { get; set; }
    }


    public class CashDiscountSupLine
    {
        public string SupplierLineCode { get; set; }
        public string SupplierLineName { get; set; }
    }

}
