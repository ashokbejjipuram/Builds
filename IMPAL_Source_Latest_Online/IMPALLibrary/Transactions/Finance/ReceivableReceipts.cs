using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Collections;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters;

namespace IMPALLibrary
{
    public class ReceiptEntity
    {
        public string BranchCode { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string AccountingPeriod { get; set; }
        public string AccountingPeriodDesc { get; set; }
        public string CustomerCode { get; set; }
        public string PaymentType { get; set; }
        public string Amount { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeBank { get; set; }
        public string ChequeBranch { get; set; }
        public string TempReceiptNumber { get; set; }
        public string TempReceiptDate { get; set; }
        public string LocalOrOutstation { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Location { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string HORefNo { get; set; }
        public string Remarks { get; set; }
        public string AdvanceAmount { get; set; }
        public string AdvanceChequeSlipNumber { get; set; }
        public string ClearedStatus { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<ReceiptItem> Items { get; set; }
    }

    public class ReceiptItem
    {
        public string SNO { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceDocumentNumber { get; set; }
        public string ReferenceDocumentNumber1 { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentValue { get; set; }
        public string CollectionAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string PaymentIndicator { get; set; }
    }

    public class TODGenerationEntity
    {
        public string BranchCode { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string AccountingPeriod { get; set; }
        public string AccountingPeriodDesc { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplyPlant { get; set; }
        public string SLBValue { get; set; }
        public string SLBPercentage { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Location { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TODType { get; set; }
        public string TODNumber { get; set; }
        public string TotalTODValue { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<TODGenerationItem> Items { get; set; }
    }

    public class TODGenerationItem
    {
        public string TODNumber { get; set; }
        public string SNO { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string ListValue { get; set; }
        public string TODValue { get; set; }
        public string SGSTPer { get; set; }
        public string SGSTCode { get; set; }
        public string SGSTVal { get; set; }
        public string CGSTPer { get; set; }
        public string CGSTCode { get; set; }
        public string CGSTVal { get; set; }
        public string IGSTPer { get; set; }
        public string IGSTCode { get; set; }
        public string IGSTVal { get; set; }
        public string ItemTotalTODValue { get; set; }
        public string IGSTInd { get; set; }
    }

    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
    }

    public class ReceiptTransactions
    {
        public List<IMPALLibrary.Transactions.Item> GetReceiptsList(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetReceiptsList");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<IMPALLibrary.Transactions.Item> ReceiptList = new List<IMPALLibrary.Transactions.Item>();
            IMPALLibrary.Transactions.Item objItem = new IMPALLibrary.Transactions.Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ReceiptList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new IMPALLibrary.Transactions.Item();
                    objItem.ItemDesc = reader["Receipt_Number"].ToString();
                    objItem.ItemCode = reader["Receipt_Number"].ToString();
                    ReceiptList.Add(objItem);
                }
            }
            return ReceiptList;
        }

        public List<IMPALLibrary.Transactions.Item> GetReceiptsListForCancellation(string BranchCode, string ReceiptNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetReceiptsList_ForCancellation");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Receipt_Number", DbType.String, ReceiptNumber);

            List<IMPALLibrary.Transactions.Item> ReceiptList = new List<IMPALLibrary.Transactions.Item>();
            IMPALLibrary.Transactions.Item objItem = new IMPALLibrary.Transactions.Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ReceiptList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new IMPALLibrary.Transactions.Item();
                    objItem.ItemDesc = reader["Receipt_Number"].ToString();
                    objItem.ItemCode = reader["Receipt_Number"].ToString();
                    ReceiptList.Add(objItem);
                }
            }
            return ReceiptList;
        }

        public ReceiptEntity GetReceiptsDetailsByNumber(string strBranchCode, string ReceiptNumber, string Status)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetReceiptsDetailsByNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Receipt_Number", DbType.String, ReceiptNumber);
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, Status);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            ReceiptEntity receiptEntity = new ReceiptEntity();
            ReceiptItem receiptItem = null;
            DataRow dr;

            if (ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    receiptEntity.ReceiptNumber = dr["Receipt_Number"].ToString();
                    receiptEntity.ReceiptDate = dr["Receipt_Date"].ToString();
                    receiptEntity.CustomerCode = dr["Customer_Code"].ToString();
                    receiptEntity.PaymentType = dr["Payment_Type"].ToString();
                    receiptEntity.ChequeNumber = dr["Cheque_Number"].ToString();
                    receiptEntity.ChequeDate = dr["Cheque_Date"].ToString();
                    receiptEntity.Amount = TwoDecimalConversion(dr["Cheque_Amount"].ToString());
                    receiptEntity.ChequeBank = dr["Cheque_Bank"].ToString();
                    receiptEntity.ChequeBranch = dr["Cheque_Branch"].ToString();
                    receiptEntity.TempReceiptNumber = dr["Temp_Receipt_Number"].ToString();
                    receiptEntity.TempReceiptDate = dr["Temp_Receipt_Date"].ToString();
                    receiptEntity.Remarks = dr["Remarks"].ToString();
                    receiptEntity.AccountingPeriod = dr["Accounting_Period_code"].ToString();
                    receiptEntity.AccountingPeriodDesc = dr["Accounting_Period_Desc"].ToString();
                    receiptEntity.LocalOrOutstation = dr["Local_Outstation"].ToString();
                    receiptEntity.Address1 = dr["Address1"].ToString();
                    receiptEntity.Address2 = dr["Address2"].ToString();
                    receiptEntity.Address3 = dr["Address3"].ToString();
                    receiptEntity.Address4 = dr["Address4"].ToString();
                    receiptEntity.Location = dr["Location"].ToString();
                    receiptEntity.AdvanceAmount = dr["Advance_Amount"].ToString();
                    receiptEntity.AdvanceChequeSlipNumber = dr["Advance_ChequeSlip_Number"].ToString();

                    receiptEntity.Items = new List<ReceiptItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        receiptItem = new ReceiptItem();

                        receiptItem.SNO = drItem["Serial_Number"].ToString();
                        receiptItem.ReferenceType = drItem["Reference_Type"].ToString();
                        receiptItem.ReferenceDocumentNumber = drItem["Reference_Document_Number"].ToString();
                        receiptItem.DocumentDate = drItem["Reference_Document_Date"].ToString();
                        receiptItem.DocumentValue = TwoDecimalConversion(drItem["Invoice_Value"].ToString());
                        receiptItem.CollectionAmount = TwoDecimalConversion(drItem["Collection_Amount"].ToString());
                        receiptItem.BalanceAmount = TwoDecimalConversion(drItem["BalanceAmount"].ToString());
                        receiptItem.PaymentIndicator = drItem["Remarks"].ToString();

                        receiptEntity.Items.Add(receiptItem);
                    }
                }
            }

            return receiptEntity;
        }

        public ReceiptEntity GetReceiptsDetailsByNumberChqReturn(string strBranchCode, string strCustomerCode, string strReceiptNumber, string strIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetReceiptsDetailsByNumber_ChqReturn");            
			ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Receipt_Number", DbType.String, strReceiptNumber);
            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, strIndicator);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            ReceiptEntity receiptEntity = new ReceiptEntity();
            ReceiptItem receiptItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    receiptEntity.ReceiptNumber = dr["Receipt_Number"].ToString();
                    receiptEntity.ReceiptDate = dr["Receipt_Date"].ToString();
                    receiptEntity.CustomerCode = dr["Customer_Code"].ToString();
                    receiptEntity.PaymentType = dr["Payment_Type"].ToString();
                    receiptEntity.ChequeNumber = dr["Cheque_Number"].ToString();
                    receiptEntity.ChequeDate = dr["Cheque_Date"].ToString();
                    receiptEntity.Amount = TwoDecimalConversion(dr["Cheque_Amount"].ToString());
                    receiptEntity.ChequeBank = dr["Cheque_Bank"].ToString();
                    receiptEntity.ChequeBranch = dr["Cheque_Branch"].ToString();
                    receiptEntity.TempReceiptNumber = dr["Temp_Receipt_Number"].ToString();
                    receiptEntity.TempReceiptDate = dr["Temp_Receipt_Date"].ToString();
                    receiptEntity.Remarks = dr["Remarks"].ToString();
                    receiptEntity.AccountingPeriod = dr["Accounting_Period_code"].ToString();
                    receiptEntity.AccountingPeriodDesc = dr["Accounting_Period_Desc"].ToString();
                    receiptEntity.LocalOrOutstation = dr["Local_Outstation"].ToString();
                    receiptEntity.Address1 = dr["Address1"].ToString();
                    receiptEntity.Address2 = dr["Address2"].ToString();
                    receiptEntity.Address3 = dr["Address3"].ToString();
                    receiptEntity.Address4 = dr["Address4"].ToString();
                    receiptEntity.Location = dr["Location"].ToString();

                    receiptEntity.Items = new List<ReceiptItem>();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        receiptItem = new ReceiptItem();

                        receiptItem.SNO = drItem["Serial_Number"].ToString();
                        receiptItem.ReferenceType = drItem["Reference_Type"].ToString();
                        receiptItem.ReferenceDocumentNumber = drItem["Reference_Document_Number"].ToString();
                        receiptItem.DocumentDate = drItem["Reference_Document_Date"].ToString();
                        receiptItem.DocumentValue = TwoDecimalConversion(drItem["Invoice_Value"].ToString());
                        receiptItem.CollectionAmount = TwoDecimalConversion(drItem["Collection_Amount"].ToString());
                        receiptItem.BalanceAmount = TwoDecimalConversion(drItem["BalanceAmount"].ToString());
                        receiptItem.PaymentIndicator = drItem["Remarks"].ToString();

                        receiptEntity.Items.Add(receiptItem);
                    }
                }
            }

            return receiptEntity;
        }

        public List<IMPALLibrary.Transactions.Item> GetHOReceiptRefDetails(string BranchCode, string CustomerCode, string AccountingPeriod, string Amount)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetHOReceiptRefDetails");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            ImpalDB.AddInParameter(cmd, "@Amount", DbType.Decimal, Convert.ToDecimal(Amount));

            List<IMPALLibrary.Transactions.Item> ReceiptList = new List<IMPALLibrary.Transactions.Item>();
            IMPALLibrary.Transactions.Item objItem = new IMPALLibrary.Transactions.Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ReceiptList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new IMPALLibrary.Transactions.Item();
                    objItem.ItemDesc = reader["NEFT_Details"].ToString();
                    objItem.ItemCode = reader["HO_Ref_Number"].ToString();
                    ReceiptList.Add(objItem);
                }
            }
            return ReceiptList;
        }

		public List<IMPALLibrary.Transactions.Item> GetCacenlledReceiptDetails(string BranchCode, string CustomerCode, string AccountingPeriod, string ModeOfReceipt, string Amount, string ChequeNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCancelledReceiptDetails");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            ImpalDB.AddInParameter(cmd, "@ModeOfReceipt", DbType.String, ModeOfReceipt);
            ImpalDB.AddInParameter(cmd, "@Amount", DbType.Decimal, Convert.ToDecimal(Amount));
            ImpalDB.AddInParameter(cmd, "@ChequeNumber", DbType.String, ChequeNumber);

            List<IMPALLibrary.Transactions.Item> ReceiptList = new List<IMPALLibrary.Transactions.Item>();
            IMPALLibrary.Transactions.Item objItem = new IMPALLibrary.Transactions.Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ReceiptList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new IMPALLibrary.Transactions.Item();
                    objItem.ItemDesc = reader["Receipt_Number"].ToString();
                    objItem.ItemCode = reader["Receipt_Number"].ToString();
                    ReceiptList.Add(objItem);
                }
            }
            return ReceiptList;
        }
		
        public int GetExistingChequeEntryStatus(string BranchCode, string strCustomerCode, string strChequeNumber, string strChequeAmount, string AccountingPeriod)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int status;

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetChequeEntryStatus");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, strChequeNumber);
            ImpalDB.AddInParameter(cmd, "@Cheque_Amount", DbType.String, strChequeAmount);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            status = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return status;
        }

        public int GetExistingChequeEntryStatusCB(string BranchCode, string strCustomerCode, string strChequeNumber, string strChequeAmount, string AccountingPeriod)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int status;

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetChequeEntryStatus_CB");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, strChequeNumber);
            ImpalDB.AddInParameter(cmd, "@Cheque_Amount", DbType.String, strChequeAmount);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            status = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return status;
        }

        public List<IMPALLibrary.Transactions.Item> GetExistingChequeReceiptDetails(string BranchCode, string strCustomerCode, string strChequeNumber, string AccountingPeriod)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetChequeReceiptDetails");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, strChequeNumber);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            List<IMPALLibrary.Transactions.Item> ReceiptList = new List<IMPALLibrary.Transactions.Item>();
            IMPALLibrary.Transactions.Item objItem = new IMPALLibrary.Transactions.Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ReceiptList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new IMPALLibrary.Transactions.Item();
                    objItem.ItemDesc = reader["Receipt_Number"].ToString();
                    objItem.ItemCode = reader["Receipt_Number_Indicator"].ToString();
                    ReceiptList.Add(objItem);
                }
            }

            return ReceiptList;
        }

        public List<ReceiptItem> GetDocumentDetails(string strFromDate, string strToDate, string CustomerCode, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GET_FINANCE_RCPT_DOCUMENTS");
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
			ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);																		  
            ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, strFromDate);
            ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, strToDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            List<ReceiptItem> lstDetail = new List<ReceiptItem>();
            ReceiptItem receiptItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    receiptItem = new ReceiptItem();
                    receiptItem.SNO = index.ToString();
                    receiptItem.ReferenceType = dr["Document_Type"].ToString();
                    receiptItem.ReferenceDocumentNumber = dr["Document_Number"].ToString();
                    receiptItem.ReferenceDocumentNumber1 = dr["Document_Number1"].ToString();
                    receiptItem.DocumentDate = dr["Document_date"].ToString();
                    receiptItem.DocumentValue = TwoDecimalConversion(dr["Balance_Amount"].ToString());
                    receiptItem.CollectionAmount = TwoDecimalConversion(dr["Balance_Amount"].ToString());
                    receiptItem.BalanceAmount = "0.00";
                    receiptItem.PaymentIndicator = dr["remarks"].ToString();

                    lstDetail.Add(receiptItem);
                    index++;
                }

            }

            return lstDetail;
        }

        public List<TODGenerationItem> GetTODGenerationDocumentDetails(string BranchCode, string strAccPeriod, string strFromDate, string strToDate, string CustomerCode, string SupplierName, string SLBValue, string SLBPercentage, string SLBtype, string TODtype)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Get_TODGeneration_Documents");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, strAccPeriod);
            ImpalDB.AddInParameter(cmd, "@SupplierName", DbType.String, SupplierName);
            ImpalDB.AddInParameter(cmd, "@SLBValue", DbType.String, SLBValue);
            ImpalDB.AddInParameter(cmd, "@SLBPercentage", DbType.String, SLBPercentage);
            ImpalDB.AddInParameter(cmd, "@SLBType", DbType.String, SLBtype);
            ImpalDB.AddInParameter(cmd, "@TODtype", DbType.String, TODtype);
            ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, strFromDate);
            ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, strToDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            List<TODGenerationItem> lstDetail = new List<TODGenerationItem>();
            TODGenerationItem todgenerationItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    todgenerationItem = new TODGenerationItem();
                    todgenerationItem.SNO = index.ToString();
                    todgenerationItem.InvoiceNumber = dr["Invoice_Number"].ToString();
                    todgenerationItem.InvoiceDate = dr["Invoice_Date"].ToString();
                    todgenerationItem.InvoiceValue = TwoDecimalConversion(dr["Tot_Sale_Value"].ToString());
                    todgenerationItem.ListValue = TwoDecimalConversion(dr["Tot_List_Value"].ToString());
                    todgenerationItem.TODValue = TwoDecimalConversion(dr["TOD_Value"].ToString());
                    todgenerationItem.SGSTPer = TwoDecimalConversion(dr["SGSTPer"].ToString());
                    todgenerationItem.SGSTVal = TwoDecimalConversion(dr["SGSTVal"].ToString());
                    todgenerationItem.CGSTPer = TwoDecimalConversion(dr["CGSTPer"].ToString());
                    todgenerationItem.CGSTVal = TwoDecimalConversion(dr["CGSTVal"].ToString());
                    todgenerationItem.IGSTPer = TwoDecimalConversion(dr["IGSTPer"].ToString());
                    todgenerationItem.IGSTVal = TwoDecimalConversion(dr["IGSTVal"].ToString());
                    todgenerationItem.ItemTotalTODValue = TwoDecimalConversion(dr["Total_TOD_Value"].ToString());
                    todgenerationItem.IGSTInd = dr["IGSTInd"].ToString();
                    todgenerationItem.TODNumber = dr["TOD_Number"].ToString();

                    lstDetail.Add(todgenerationItem);
                    index++;
                }
            }

            return lstDetail;
        }

        public List<TODGenerationItem> GetTODGenerationDocumentDetailsGOGO(string BranchCode, string strAccPeriod, string strMontYear, string CustomerCode, string SupplierCode, string TODtype, string LocalOutstation, string TODpercentage, string CDindicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Get_TODGeneration_Documents_GOGO");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@CustomerCode", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, strAccPeriod);
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, LocalOutstation);
            ImpalDB.AddInParameter(cmd, "@TODtype", DbType.String, TODtype);
            ImpalDB.AddInParameter(cmd, "@MonthYear", DbType.String, strMontYear);
            ImpalDB.AddInParameter(cmd, "@TODPercentage", DbType.String, TODpercentage);
            ImpalDB.AddInParameter(cmd, "@CD_Indicator", DbType.String, CDindicator);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            List<TODGenerationItem> lstDetail = new List<TODGenerationItem>();
            TODGenerationItem todgenerationItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    todgenerationItem = new TODGenerationItem();
                    todgenerationItem.SNO = index.ToString();
                    todgenerationItem.InvoiceNumber = dr["Invoice_Number"].ToString();
                    todgenerationItem.InvoiceDate = dr["Invoice_Date"].ToString();
                    todgenerationItem.InvoiceValue = TwoDecimalConversion(dr["Tot_Sale_Value"].ToString());
                    todgenerationItem.ListValue = TwoDecimalConversion(dr["Tot_List_Value"].ToString());
                    todgenerationItem.TODValue = TwoDecimalConversion(dr["TOD_Value"].ToString());
                    todgenerationItem.SGSTPer = TwoDecimalConversion(dr["SGSTPer"].ToString());
                    todgenerationItem.SGSTVal = TwoDecimalConversion(dr["SGSTVal"].ToString());
                    todgenerationItem.CGSTPer = TwoDecimalConversion(dr["CGSTPer"].ToString());
                    todgenerationItem.CGSTVal = TwoDecimalConversion(dr["CGSTVal"].ToString());
                    todgenerationItem.IGSTPer = TwoDecimalConversion(dr["IGSTPer"].ToString());
                    todgenerationItem.IGSTVal = TwoDecimalConversion(dr["IGSTVal"].ToString());
                    todgenerationItem.ItemTotalTODValue = TwoDecimalConversion(dr["Total_TOD_Value"].ToString());
                    todgenerationItem.IGSTInd = dr["IGSTInd"].ToString();

                    lstDetail.Add(todgenerationItem);
                    index++;
                }
            }

            return lstDetail;
        }

        public void ResetTODdetails(string strBranchCode, string strCustomerCode, string strSupplierCode, string strTODnumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_reset_TODdetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSupplierCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);            
            ImpalDB.AddInParameter(cmd, "@TOD_Number", DbType.String, strTODnumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public DataSet AddNewReceiptEntry(ref ReceiptEntity receiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            int result = 0;
            string ReceiptNumber = "";
            string TransactionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_Addcollection1_New");
                    ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Receipt_Date", DbType.String, receiptEntity.ReceiptDate);
                    ImpalDB.AddInParameter(cmdHeader, "@Payment_Type", DbType.String, receiptEntity.PaymentType);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, receiptEntity.ChequeNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, receiptEntity.ChequeDate);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Amount", DbType.String, receiptEntity.Amount);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, receiptEntity.ChequeBank);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, receiptEntity.ChequeBranch);
                    ImpalDB.AddInParameter(cmdHeader, "@Temp_Receipt_Number", DbType.String, receiptEntity.TempReceiptNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Temp_Receipt_Date", DbType.String, receiptEntity.TempReceiptDate);
                    ImpalDB.AddInParameter(cmdHeader, "@Reference_Document_Number", DbType.String, "");
                    ImpalDB.AddInParameter(cmdHeader, "@Reference_Document_Date", DbType.String, "");
                    ImpalDB.AddInParameter(cmdHeader, "@Invoice_Value", DbType.Decimal, Convert.ToDecimal("0"));
                    ImpalDB.AddInParameter(cmdHeader, "@Collection_Amount", DbType.Decimal, Convert.ToDecimal("0"));
                    ImpalDB.AddInParameter(cmdHeader, "@Reference_Type", DbType.String, "");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, receiptEntity.LocalOrOutstation);
					ImpalDB.AddInParameter(cmdHeader, "@HO_Ref_Number", DbType.String, receiptEntity.HORefNo);
                    ImpalDB.AddInParameter(cmdHeader, "@Advance_Amount", DbType.Decimal, receiptEntity.AdvanceAmount);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ReceiptNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                    if (ReceiptNumber.Substring(0, 1) != "E")
                    {
                        foreach (ReceiptItem rcptItem in receiptEntity.Items)
                        {
                            DbCommand cmdItems = ImpalDB.GetStoredProcCommand("usp_Addcollection2_New");
                            ImpalDB.AddInParameter(cmdItems, "@Receipt_Number", DbType.String, ReceiptNumber);
                            ImpalDB.AddInParameter(cmdItems, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                            ImpalDB.AddInParameter(cmdItems, "@Receipt_Date", DbType.String, receiptEntity.ReceiptDate);
                            ImpalDB.AddInParameter(cmdItems, "@Payment_Type", DbType.String, receiptEntity.PaymentType);
                            ImpalDB.AddInParameter(cmdItems, "@Cheque_Number", DbType.String, receiptEntity.ChequeNumber);
                            ImpalDB.AddInParameter(cmdItems, "@Cheque_Date", DbType.String, receiptEntity.ChequeDate);
                            ImpalDB.AddInParameter(cmdItems, "@Cheque_Amount", DbType.String, receiptEntity.Amount);
                            ImpalDB.AddInParameter(cmdItems, "@Cheque_Bank", DbType.String, receiptEntity.ChequeBank);
                            ImpalDB.AddInParameter(cmdItems, "@Cheque_Branch", DbType.String, receiptEntity.ChequeBranch);
                            ImpalDB.AddInParameter(cmdItems, "@Temp_Receipt_Number", DbType.String, receiptEntity.TempReceiptNumber);
                            ImpalDB.AddInParameter(cmdItems, "@Temp_Receipt_Date", DbType.String, receiptEntity.TempReceiptDate);
                            ImpalDB.AddInParameter(cmdItems, "@Reference_Document_Number", DbType.String, rcptItem.ReferenceDocumentNumber);
                            ImpalDB.AddInParameter(cmdItems, "@Reference_Document_Date", DbType.String, rcptItem.DocumentDate);

                            if (string.IsNullOrEmpty(rcptItem.DocumentValue))
                                rcptItem.DocumentValue = "0";
                            ImpalDB.AddInParameter(cmdItems, "@Invoice_Value", DbType.Decimal, Convert.ToDecimal(rcptItem.DocumentValue));

                            if (string.IsNullOrEmpty(rcptItem.CollectionAmount))
                                rcptItem.CollectionAmount = "0";
                            ImpalDB.AddInParameter(cmdItems, "@Collection_Amount", DbType.Decimal, Convert.ToDecimal(rcptItem.CollectionAmount));

                            ImpalDB.AddInParameter(cmdItems, "@Reference_Type", DbType.String, rcptItem.ReferenceType);
                            ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                            ImpalDB.AddInParameter(cmdItems, "@Local_Outstation", DbType.String, receiptEntity.LocalOrOutstation);
							ImpalDB.AddInParameter(cmdItems, "@remarks", DbType.String, rcptItem.PaymentIndicator);
                            cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItems);

                            DbCommand cmdOutStanding = ImpalDB.GetStoredProcCommand("usp_Update_OutStanding");
                            ImpalDB.AddInParameter(cmdOutStanding, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                            ImpalDB.AddInParameter(cmdOutStanding, "@Document_Number", DbType.String, rcptItem.ReferenceDocumentNumber);
                            ImpalDB.AddInParameter(cmdOutStanding, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);

                            if (string.IsNullOrEmpty(rcptItem.CollectionAmount))
                                rcptItem.CollectionAmount = "0";
                            ImpalDB.AddInParameter(cmdOutStanding, "@Collection_Amount", DbType.Decimal, Convert.ToDecimal(rcptItem.CollectionAmount));

                            ImpalDB.AddInParameter(cmdOutStanding, "@Reference_Type", DbType.String, rcptItem.ReferenceType);
                            ImpalDB.AddInParameter(cmdOutStanding, "@Document_Number1", DbType.String, rcptItem.ReferenceDocumentNumber1);
                            ImpalDB.AddInParameter(cmdOutStanding, "@Serial_Number", DbType.String, rcptItem.SNO);
                            ImpalDB.AddInParameter(cmdOutStanding, "@remarks", DbType.String, rcptItem.PaymentIndicator);
                            cmdOutStanding.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdOutStanding);
                        }

                        DbCommand cmdGlRc = ImpalDB.GetStoredProcCommand("usp_addglrc1");
                        ImpalDB.AddInParameter(cmdGlRc, "@doc_no", DbType.String, ReceiptNumber);
                        ImpalDB.AddInParameter(cmdGlRc, "@Branch_Code", DbType.String, receiptEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdGlRc, "@HO_Ref_Number", DbType.String, receiptEntity.HORefNo);
                        ImpalDB.AddInParameter(cmdGlRc, "@Advance_Amount", DbType.String, receiptEntity.AdvanceAmount);
                        cmdGlRc.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdGlRc);

                        if (Convert.ToDecimal(receiptEntity.AdvanceAmount) > 0 && receiptEntity.PaymentType != "CA")
                        {
                            string PaymentType = string.Empty;
                            string Remarks = "AUTO CHEQUESLIP FOR ADVANCE PAYMENT RECEIVED - RECEIPT # " + ReceiptNumber;

                            if (receiptEntity.PaymentType == "CH")
                                PaymentType = "Q";
                            else if (receiptEntity.PaymentType == "DR")
                                PaymentType = "D";

                            cmdHeader = ImpalDB.GetStoredProcCommand("usp_addmaincash1_AutoChequeSlip");
                            ImpalDB.AddInParameter(cmdHeader, "@Transaction_Date", DbType.String, receiptEntity.ReceiptDate);
                            ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                            ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, Remarks);
                            ImpalDB.AddInParameter(cmdHeader, "@Receipt_Payment_Indicator", DbType.String, "R");
                            ImpalDB.AddInParameter(cmdHeader, "@Cash_Cheque_Indicator", DbType.String, PaymentType);
                            ImpalDB.AddInParameter(cmdHeader, "@Transaction_Amount", DbType.Decimal, receiptEntity.AdvanceAmount);
                            ImpalDB.AddInParameter(cmdHeader, "@Cheque_Number", DbType.String, receiptEntity.ChequeNumber);
                            ImpalDB.AddInParameter(cmdHeader, "@Cheque_Date", DbType.String, receiptEntity.ChequeDate);
                            ImpalDB.AddInParameter(cmdHeader, "@Cheque_Bank", DbType.String, receiptEntity.ChequeBank);
                            ImpalDB.AddInParameter(cmdHeader, "@Cheque_Branch", DbType.String, receiptEntity.ChequeBranch);
                            ImpalDB.AddInParameter(cmdHeader, "@Ref_Date", DbType.String, receiptEntity.ReceiptDate);
                            ImpalDB.AddInParameter(cmdHeader, "@Local_Outstation", DbType.String, receiptEntity.LocalOrOutstation);
                            ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                            ImpalDB.AddInParameter(cmdHeader, "@HO_Ref_Number", DbType.String, receiptEntity.HORefNo);
                            ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, "A");
                            cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                            TransactionNumber = ImpalDB.ExecuteScalar(cmdHeader).ToString();

                            DbCommand cmdItem = ImpalDB.GetStoredProcCommand("usp_addmaincash2");
                            ImpalDB.AddInParameter(cmdItem, "@Transaction_Number", DbType.String, TransactionNumber);
                            ImpalDB.AddInParameter(cmdItem, "@Serial_Number", DbType.Int32, 1);
                            ImpalDB.AddInParameter(cmdItem, "@Chart_of_Account_Code", DbType.String, "4070330220" + receiptEntity.CustomerCode + receiptEntity.BranchCode);
                            ImpalDB.AddInParameter(cmdItem, "@Receipt_Payment_Indicator", DbType.String, "R");
                            ImpalDB.AddInParameter(cmdItem, "@Amount", DbType.Decimal, receiptEntity.AdvanceAmount);
                            ImpalDB.AddInParameter(cmdItem, "@Remarks", DbType.String, Remarks);
                            cmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdItem);

                            if (TransactionNumber != "")
                            {
                                DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addglmc1");
                                ImpalDB.AddInParameter(cmd1, "@doc_no", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd1);

                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_updcustos_new");
                                ImpalDB.AddInParameter(cmd2, "@Doc_No", DbType.String, TransactionNumber);
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                                ImpalDB.AddInParameter(cmd2, "@Receipt_Number", DbType.String, ReceiptNumber);
                                ImpalDB.AddInParameter(cmd2, "@Transaction_Number", DbType.String, TransactionNumber);
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }
                        }

                        DbCommand cmdOsData = ImpalDB.GetStoredProcCommand("usp_GetOutstanding_Data");
                        ImpalDB.AddInParameter(cmdOsData, "@Branch_Code", DbType.String, receiptEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdOsData, "@Customer_Code", DbType.String, receiptEntity.CustomerCode.Trim());
                        ImpalDB.AddInParameter(cmdOsData, "@doc_no", DbType.String, ReceiptNumber);
                        cmdOsData.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmdOsData);

                        receiptEntity.ReceiptNumber = ReceiptNumber;
						receiptEntity.AdvanceChequeSlipNumber = TransactionNumber;														  
                        receiptEntity.ErrorCode = "0";
                        receiptEntity.ErrorMsg = "";
                    }
                    else
                    {
                        receiptEntity.ReceiptNumber = ReceiptNumber.Substring(1, ReceiptNumber.Length - 1);
                        receiptEntity.ErrorCode = "D";
                        receiptEntity.ErrorMsg = "D";
                    }                    

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                receiptEntity.ErrorCode = "1";
                receiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return ds;
        }

        public DataSet CancelReceiptEntry(ref ReceiptEntity receiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_updCollection_New");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Receipt_Number", DbType.String, receiptEntity.ReceiptNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Mode_of_Receipt", DbType.String, receiptEntity.PaymentType);
                    ImpalDB.AddInParameter(cmdHeader, "@Cheque_Amount", DbType.String, receiptEntity.Amount);
                    ImpalDB.AddInParameter(cmdHeader, "@NEFT_Remarks", DbType.String, receiptEntity.HORefNo);
                    ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, receiptEntity.Remarks);
                    ImpalDB.AddInParameter(cmdHeader, "@Advance_ChequeSlip_Number", DbType.String, receiptEntity.AdvanceChequeSlipNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Advance_Amount", DbType.Decimal, Convert.ToDecimal(receiptEntity.AdvanceAmount));
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);

                    foreach (ReceiptItem rcptItem in receiptEntity.Items)
                    {
                        DbCommand cmdOutStanding = ImpalDB.GetStoredProcCommand("usp_Update_OutStanding_Cancel");
                        ImpalDB.AddInParameter(cmdOutStanding, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdOutStanding, "@Document_Number", DbType.String, rcptItem.ReferenceDocumentNumber);
                        ImpalDB.AddInParameter(cmdOutStanding, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);

                        if (string.IsNullOrEmpty(rcptItem.CollectionAmount))
                            rcptItem.CollectionAmount = "0";
                        ImpalDB.AddInParameter(cmdOutStanding, "@Collection_Amount", DbType.Decimal, Convert.ToDecimal(rcptItem.CollectionAmount));

                        ImpalDB.AddInParameter(cmdOutStanding, "@Reference_Type", DbType.String, rcptItem.ReferenceType);
                        ImpalDB.AddInParameter(cmdOutStanding, "@Document_Number1", DbType.String, rcptItem.ReferenceDocumentNumber1);
                        ImpalDB.AddInParameter(cmdOutStanding, "@Serial_Number", DbType.String, rcptItem.SNO);
                        ImpalDB.AddInParameter(cmdOutStanding, "@remarks", DbType.String, rcptItem.PaymentIndicator);
                        cmdOutStanding.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdOutStanding);
                    }

                    DbCommand cmdGlRc = ImpalDB.GetStoredProcCommand("usp_addglrc1_Cancel");
                    ImpalDB.AddInParameter(cmdGlRc, "@doc_no", DbType.String, receiptEntity.ReceiptNumber);
                    ImpalDB.AddInParameter(cmdGlRc, "@Branch_Code", DbType.String, receiptEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmdGlRc, "@HO_Ref_Number", DbType.String, receiptEntity.HORefNo);
                    ImpalDB.AddInParameter(cmdGlRc, "@Amount", DbType.String, receiptEntity.Amount);
                    cmdGlRc.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdGlRc);

                    if (receiptEntity.AdvanceChequeSlipNumber != "" && Convert.ToDecimal(receiptEntity.AdvanceAmount) > 0)
                    {
                        DbCommand cmdAdvChqSlip = ImpalDB.GetStoredProcCommand("usp_updmaincash1_New");
                        ImpalDB.AddInParameter(cmdAdvChqSlip, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdAdvChqSlip, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                        ImpalDB.AddInParameter(cmdAdvChqSlip, "@Transaction_Number", DbType.String, receiptEntity.AdvanceChequeSlipNumber);
                        ImpalDB.AddInParameter(cmdAdvChqSlip, "@Advance_Amount", DbType.Decimal, Convert.ToDecimal(receiptEntity.AdvanceAmount));
                        cmdAdvChqSlip.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdAdvChqSlip);
                    }

                    receiptEntity.ReceiptNumber = receiptEntity.ReceiptNumber;
                    receiptEntity.ErrorCode = "0";
                    receiptEntity.ErrorMsg = "";

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                receiptEntity.ErrorCode = "1";
                receiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return ds;
        }

        public void AddNewPDCRegister(ref ReceiptEntity receiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddPDC_Register");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmd, "@Receipt_Date", DbType.String, receiptEntity.ReceiptDate);
                    ImpalDB.AddInParameter(cmd, "@Payment_Type", DbType.String, receiptEntity.PaymentType);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, receiptEntity.ChequeNumber);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Date", DbType.String, receiptEntity.ChequeDate);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Amount", DbType.String, receiptEntity.Amount);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Bank", DbType.String, receiptEntity.ChequeBank);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Branch", DbType.String, receiptEntity.ChequeBranch);
                    ImpalDB.AddInParameter(cmd, "@Cleared_Status", DbType.String, receiptEntity.ClearedStatus);
                    ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, receiptEntity.LocalOrOutstation);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, receiptEntity.Remarks);
                    ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, receiptEntity.AccountingPeriod);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    receiptEntity.ErrorCode = "0";
                    receiptEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                receiptEntity.ErrorCode = "1";
                receiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void UpdNewPDCRegister(ref ReceiptEntity receiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updPDC_Register");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, receiptEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, receiptEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmd, "@Receipt_Date", DbType.String, receiptEntity.ReceiptDate);
                    ImpalDB.AddInParameter(cmd, "@Payment_Type", DbType.String, receiptEntity.PaymentType);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, receiptEntity.ChequeNumber);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Date", DbType.String, receiptEntity.ChequeDate);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Amount", DbType.String, receiptEntity.Amount);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Bank", DbType.String, receiptEntity.ChequeBank);
                    ImpalDB.AddInParameter(cmd, "@Cheque_Branch", DbType.String, receiptEntity.ChequeBranch);
                    ImpalDB.AddInParameter(cmd, "@Cleared_Status", DbType.String, receiptEntity.ClearedStatus);
                    ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, receiptEntity.LocalOrOutstation);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, receiptEntity.Remarks);
                    ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, receiptEntity.AccountingPeriod);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    receiptEntity.ErrorCode = "0";
                    receiptEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                receiptEntity.ErrorCode = "1";
                receiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public int GetExistingPDCchequeEntryStatus(string BranchCode, string strCustomerCode, string strChequeNumber, string strChequeBank, string strChequeAmount, string strBank, string AccountingPeriod)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int status;

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPDC_ChequeEntryStatus");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, strChequeNumber);
            ImpalDB.AddInParameter(cmd, "@Cheque_Bank", DbType.String, strChequeBank);
            ImpalDB.AddInParameter(cmd, "@Cheque_Amount", DbType.String, strChequeAmount);
            ImpalDB.AddInParameter(cmd, "@Bank", DbType.String, strBank);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            status = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return status;
        }

        public DataSet GetExistingPDCchequeEntryDetails(string BranchCode, string strCustomerCode, string strChequeNumber, string strChequeBank, string strChequeAmount, string strBank, string AccountingPeriod)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet(); ;

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPDC_ChequeEntryDetails");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(cmd, "@Cheque_Number", DbType.String, strChequeNumber);
            ImpalDB.AddInParameter(cmd, "@AccountingPeriod", DbType.Int16, AccountingPeriod);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }
    }
}
