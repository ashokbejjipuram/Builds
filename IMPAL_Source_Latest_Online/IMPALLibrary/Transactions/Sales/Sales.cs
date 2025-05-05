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
using System.Data.SqlClient;

namespace IMPALLibrary.Transactions
{
    public class SalesEntity
    {
        public string SalesInvoiceNumber { get; set; }
        public string SalesInvoiceDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerActualName { get; set; }
        public string SalesManCode { get; set; }
        public string SalesManName { get; set; }
        public string BranchCode { get; set; }
        public string CashDiscountCode { get; set; }
        public double OrderValue { get; set; }
        public string CustomerPONumber { get; set; }
        public string CustomerPODate { get; set; }
        public double CourierCharge { get; set; }
        public double InsuranceChargePerc { get; set; }
        public string RefDocumentNumber { get; set; }
        public string RefDocumentDate { get; set; }
        public string Indicator { get; set; }
        public string LRTransfer { get; set; }
        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public string MarkingNumber { get; set; }
        public string Carrier { get; set; }
        public string NumberOfCases { get; set; }
        public string Weight { get; set; }
        public string FreightIndicatorCode { get; set; }
        public double FreightAmount { get; set; }
        public string Remarks { get; set; }
        public double Discount { get; set; }
        public double InsuranceCharges { get; set; }
        public string ReceiptLocalOutstation { get; set; }
        public string ModeOfReceipt { get; set; }
        public string ChequeDraftNumber { get; set; }
        public string ChequeDraftDate { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public double AmountReceived { get; set; }
        public double BalanceAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTown { get; set; }
        public string LocalOutstation { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string SalesTaxValue { get; set; }
        public string Status { get; set; }
        public string CustSalesReqNumber { get; set; }
        public string CostToCostCouponInd { get; set; }

        public string ShippingName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingAddress4 { get; set; }
        public string ShippingLocation { get; set; }
        public string ShippingGSTIN { get; set; }
        public string ShippingState { get; set; }
        public string DispatchLocationInd { get; set; }

        public List<SalesItem> Items { get; set; }
    }

    public class TransactionType
    {
        public string TransactionTypeDesc { get; set; }
        public string TransactionTypeCode { get; set; }
    }
    public class SalesMan
    {
        public string SalesManCode { get; set; }
        public string SalesManName { get; set; }
    }
    public class CashDiscount
    {
        public string CashDiscountDesc { get; set; }
        public string CashDiscountCode { get; set; }
    }
    public class SalesItem
    {
        public string SNO { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemSupplierPartNumber { get; set; }

        public string OsLsIndicator { get; set; }
        public string SlbCode { get; set; }
        public string SlbDesc { get; set; }
        public string PackingQuantity { get; set; }
        public string ReceivedQuantity { get; set; }
        public string Quantity { get; set; }
        public string AvialableQuantity { get; set; }
        public string OriginalReqQty { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ListPrice { get; set; }
        public string CostPrice { get; set; }
        public string ValueType { get; set; }
        public double SLBNetValuePrice { get; set; }
        public string DepotCode { get; set; }
        public string DepotLongName { get; set; }
        public string PurchaseDiscount { get; set; }
        public string CouponIndicator { get; set; }
        public string ItemDiscount { get; set; }
        public string EDIndicator { get; set; }
        public string EDValue { get; set; }
        public string SalesTaxCode { get; set; }
        public string SalesTaxDescription { get; set; }
        public string SalesTaxPercentage { get; set; }
        public string SalesTaxText { get; set; }
        public string ItemSaleTaxValue { get; set; }
        public string SellingPrice { get; set; }
        public string GrossProfit { get; set; }
        public string SaleValue { get; set; }
        public string MinOrdQuantity { get; set; }

        public string OSLSIndDesc { get; set; }
        public string SalesTaxPerDesc { get; set; }
        public string ProductGroupCode { get; set; }
        public string ItemDescription { get; set; }

        public string DispatchLocationInd { get; set; }
        public string DispatchLocationCount { get; set; }
    }

    public class CustomerSalesReqEntity
    {
        public string CustomerSalesReqNumber { get; set; }
        public string CustomerSalesReqNumVal { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerActualName { get; set; }
        public string BranchCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<CustomerSalesReqItem> Items { get; set; }
    }

    public class CustomerSalesReqItem
    {
        public string SNO { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string PackingQuantity { get; set; }
        public string Quantity { get; set; }
        public string ValidDays { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ListPrice { get; set; }
        public string MinOrdQuantity { get; set; }
    }

    public class SalesTransactions
    {
        public int AddNewSalesEntry(SalesEntity SalesEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            return 1;
        }

        public List<SalesEntity> GetSalesInvoiceNumber(string strBranchCode)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesEntity objSalesEntity = new SalesEntity();
                    objSalesEntity.SalesInvoiceNumber = "0";
                    objSalesEntity.SalesInvoiceNumber = "-- Select --";
                    lstSalesEntity.Add(objSalesEntity);

                    Database ImpalDB = DataAccess.GetDatabase();
                    //string sSQL = "Select distinct s1.Document_Number from Sales_Order_Header s inner join v_invoice v on s.Document_Number=v.Document_Number ";
                    //sSQL = sSQL + "and s.Branch_Code = '" + strBranchCode + "' and s.Branch_Code=v.Branch_Code and isnull(s.status,'A') in ('A','P') and convert(date,s.Document_date,103)=convert(date,GETDATE(),103) ";
                    //sSQL = sSQL + "inner join Sales_Order_Detail s1 on s1.document_number=v.Document_Number and s1.Item_Code=v.Item_Code Group By s1.document_number having SUM(isnull(Return_Quantity,0))=0 order by s1.Document_Number desc";

                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSalesInvoiceListForCancellation");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSalesEntity = new SalesEntity();
                            objSalesEntity.SalesInvoiceNumber = reader[0].ToString();
                            lstSalesEntity.Add(objSalesEntity);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesEntity;
        }

        public List<SalesEntity> GetPackingSlipInvoiceNumber(string strBranch, string InvoiceNumber)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesEntity objSalesEntity = new SalesEntity();
                    objSalesEntity.SalesInvoiceNumber = "0";
                    objSalesEntity.SalesInvoiceNumber = "-- Select --";
                    lstSalesEntity.Add(objSalesEntity);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select distinct s.Document_Number from Sales_Order_Header s WITH (NOLOCK) inner join Sales_Order_Detail s1 WITH (NOLOCK) on s.Document_Number=s1.Document_Number ";
                    sSQL = sSQL + "and s.Document_Number = '" + InvoiceNumber + "' and s.Branch_Code = '" + strBranch + "' and isnull(s.status,'A') in ('A','P') Group By s.document_number";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSalesEntity = new SalesEntity();
                            objSalesEntity.SalesInvoiceNumber = reader[0].ToString();
                            lstSalesEntity.Add(objSalesEntity);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesEntity;
        }

        public List<SalesEntity> GetProformaInvoiceNumber(string strBranchCode)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesEntity objSalesEntity = new SalesEntity();
                    objSalesEntity.SalesInvoiceNumber = "0";
                    objSalesEntity.SalesInvoiceNumber = "-- Select --";
                    lstSalesEntity.Add(objSalesEntity);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select distinct s1.Document_Number from Proforma_Invoice_Header s WITH (NOLOCK) inner join v_invoice v WITH (NOLOCK) on s.Document_Number=v.Document_Number ";
                    sSQL = sSQL + "and s.Branch_Code = '" + strBranchCode + "' and s.Branch_Code=v.Branch_Code and isnull(s.status,'A') in ('A','P') and convert(date,s.Document_date,103)=convert(date,GETDATE(),103) ";
                    sSQL = sSQL + "inner join Proforma_Invoice_Detail s1 WITH (NOLOCK) on s1.document_number=v.Document_Number and s1.Item_Code=v.Item_Code Group By s1.document_number having SUM(isnull(Return_Quantity,0))=0 order by s1.Document_Number desc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSalesEntity = new SalesEntity();
                            objSalesEntity.SalesInvoiceNumber = reader[0].ToString();
                            lstSalesEntity.Add(objSalesEntity);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesEntity;
        }

        public List<SalesEntity> GetSalesInvoiceHeader(string strSalesInvoiceNumer, string strBranchCode)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesEntity objSalesEntity = new SalesEntity();

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = " Select 'V' Indidator, SOH.document_number document_number, SOH.Transaction_Type_Code,TM.Transaction_Type_Description, convert(nvarchar,SOH.Document_Date,103) document_date, SOH.Customer_Code, CM.Customer_Name,  ";
                    sSQL = sSQL + " SOH.sales_man_code,SMM.sales_man_name, isnull(LR_Transfer,'N') LR_Transfer, SOH.LR_Number,convert(nvarchar,LR_Date,103) LR_date,  ";
                    sSQL = sSQL + " SOH.Reference_Document_Number , convert(nvarchar,SOH.Reference_Document_Date,103) reference_document_date,  SOH.Remarks, ";
                    sSQL = sSQL + " SOH.cash_discount_code, CD.cash_discount_description, isnull(SOH.Order_Value,0) order_value, ";
                    sSQL = sSQL + " SOH.Marking_Number,SOH.Number_Of_Cases,SOH.Weight, SOH.Carrier, SOH.Freight_Indicator,   ";
                    sSQL = sSQL + " isnull(SOH.Freight_Amount,0) freight_amount,isnull(SOH.Courier_Charges,0) courier_charges, ";
                    sSQL = sSQL + " isnull(SOH.Insurance_Charges,0) Insurance_charges, SOH.Customer_PO_Number,Convert(nvarchar,SOH.Customer_PO_Date,103) Customer_PO_Date, SOH.Sales_Taxes, SOH.Status, ";
                    sSQL = sSQL + " SOR.Mode_of_Receipt,SOR.Cheque_Draft_Number,SOR.Cheque_Draft_Date,SOR.Bank_Name,SOR.Branch_Name,SOR.LOCAL_OUTSTATION From Sales_order_header SOH WITH (NOLOCK) ";
                    sSQL = sSQL + " Inner Join Transaction_Type_Master TM WITH (NOLOCK) On SOH.Transaction_Type_Code =TM.Transaction_Type_Code ";
                    sSQL = sSQL + " Inner Join Customer_Master CM WITH (NOLOCK) On SOH.Branch_Code=CM.Branch_Code and SOH.Customer_Code = CM.Customer_Code ";
                    sSQL = sSQL + " Left Outer Join sales_man_master SMM WITH (NOLOCK) On CM.Branch_Code=SMM.Branch_Code and SOH.sales_man_code = SMM.sales_man_code  ";
                    sSQL = sSQL + " Left Outer Join cash_discount CD WITH (NOLOCK) On SMM.Branch_Code=CD.Branch_Code and SOH.cash_discount_code = CD.cash_discount_code ";
                    sSQL = sSQL + " Left Outer Join Sales_Order_Receipt_Details SOR WITH (NOLOCK) On SOR.Branch_Code=SOH.Branch_Code and SOR.Document_Number = SOH.Document_Number ";
                    if (strSalesInvoiceNumer == "")
                        sSQL = sSQL + " Where SOH.Branch_Code = '" + strBranchCode + "' and isnull(SOH.status,'A') in ('A') and CONVERT(NVARCHAR,SOH.DOCUMENT_DATE,103) = '" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'  Order By Document_date desc";
                    else
                        sSQL = sSQL + " Where SOH.document_number= '" + strSalesInvoiceNumer + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSalesEntity = new SalesEntity();
                            objSalesEntity.Indicator = reader["Indidator"].ToString();
                            objSalesEntity.SalesInvoiceNumber = reader["document_number"].ToString();
                            objSalesEntity.TransactionTypeCode = reader["Transaction_Type_Code"].ToString();
                            objSalesEntity.SalesInvoiceDate = reader["document_date"].ToString();
                            objSalesEntity.CustomerCode = reader["Customer_Code"].ToString();
                            objSalesEntity.CustomerActualName = reader["Customer_Name"].ToString();
                            objSalesEntity.SalesManCode = reader["sales_man_code"].ToString();
                            objSalesEntity.SalesManName = reader["sales_man_name"].ToString();
                            objSalesEntity.LRTransfer = reader["LR_Transfer"].ToString();
                            objSalesEntity.LRNumber = reader["LR_Number"].ToString();
                            objSalesEntity.LRDate = reader["LR_date"].ToString();
                            objSalesEntity.CashDiscountCode = reader["cash_discount_code"].ToString();
                            objSalesEntity.OrderValue = Convert.ToDouble(reader["order_value"].ToString());
                            objSalesEntity.MarkingNumber = reader["Marking_Number"].ToString();
                            objSalesEntity.NumberOfCases = reader["Number_Of_Cases"].ToString();
                            objSalesEntity.Weight = reader["Weight"].ToString();
                            objSalesEntity.Carrier = reader["Carrier"].ToString();
                            objSalesEntity.FreightIndicatorCode = reader["Freight_Indicator"].ToString();
                            objSalesEntity.FreightAmount = Convert.ToDouble(reader["freight_amount"].ToString());
                            objSalesEntity.CourierCharge = Convert.ToDouble(reader["courier_charges"].ToString());
                            objSalesEntity.InsuranceCharges = Convert.ToDouble(reader["Insurance_charges"].ToString());
                            objSalesEntity.CustomerPONumber = reader["Customer_PO_Number"].ToString();
                            objSalesEntity.CustomerPODate = reader["Customer_PO_Date"].ToString();
                            objSalesEntity.SalesTaxValue = reader["Sales_Taxes"].ToString();
                            objSalesEntity.Status = reader["Status"].ToString();
                            objSalesEntity.ModeOfReceipt = reader["Mode_of_Receipt"].ToString();
                            objSalesEntity.ChequeDraftNumber = reader["Cheque_Draft_Number"].ToString();
                            objSalesEntity.ChequeDraftDate = reader["Cheque_Draft_Date"].ToString();
                            objSalesEntity.BankName = reader["Bank_Name"].ToString();
                            objSalesEntity.BankBranchName = reader["Branch_Name"].ToString();
                            objSalesEntity.LocalOutstation = reader["LOCAL_OUTSTATION"].ToString();

                            lstSalesEntity.Add(objSalesEntity);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesEntity;
        }

        public List<SalesEntity> GetProformaInvoiceHeader(string strSalesInvoiceNumer, string strBranchCode)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesEntity objSalesEntity = new SalesEntity();

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = " Select Top 10 SN.Indicator Indidator, SOH.document_number document_number, SOH.Transaction_Type_Code,TM.Transaction_Type_Description, convert(nvarchar,SOH.Document_Date,103) document_date, SOH.Customer_Code, CM.Customer_Name,  ";
                    sSQL = sSQL + " SOH.sales_man_code,SMM.sales_man_name, isnull(LR_Transfer,'N') LR_Transfer, SOH.LR_Number,convert(nvarchar,LR_Date,103) LR_date,  ";
                    sSQL = sSQL + " SOH.Reference_Document_Number , convert(nvarchar,SOH.Reference_Document_Date,103) reference_document_date,  SOH.Remarks, ";
                    sSQL = sSQL + " SOH.cash_discount_code, CD.cash_discount_description, isnull(SOH.Order_Value,0) order_value, ";
                    sSQL = sSQL + " SOH.Marking_Number,SOH.Number_Of_Cases,SOH.Weight, SOH.Carrier, SOH.Freight_Indicator,   ";
                    sSQL = sSQL + " isnull(SOH.Freight_Amount,0) freight_amount,isnull(SOH.Courier_Charges,0) courier_charges, ";
                    sSQL = sSQL + " isnull(SOH.Insurance_Charges,0) Insurance_charges, SOH.Customer_PO_Number,Convert(nvarchar,SOH.Customer_PO_Date,103) Customer_PO_Date, SOH.Sales_Taxes, SOH.Status, ";
                    sSQL = sSQL + " SOR.Mode_of_Receipt,SOR.Cheque_Draft_Number,SOR.Cheque_Draft_Date,SOR.Bank_Name,SOR.Branch_Name,SOR.LOCAL_OUTSTATION From Proforma_Invoice_Header SOH WITH (NOLOCK) ";
                    sSQL = sSQL + " Inner Join Transaction_Type_Master TM WITH (NOLOCK) On SOH.Transaction_Type_Code =TM.Transaction_Type_Code ";
                    sSQL = sSQL + " Inner Join Customer_Master CM WITH (NOLOCK) On SOH.Branch_Code=CM.Branch_Code and SOH.Customer_Code = CM.Customer_Code ";
                    sSQL = sSQL + " Inner Join serial_number SN WITH (NOLOCK) On CM.Branch_Code=SN.Branch_Code and SOH.document_number = SN.document_number ";
                    sSQL = sSQL + " Left Outer Join sales_man_master SMM WITH (NOLOCK) On SN.Branch_Code=SMM.Branch_Code and SOH.sales_man_code = SMM.sales_man_code  ";
                    sSQL = sSQL + " Left Outer Join cash_discount CD WITH (NOLOCK) On SMM.Branch_Code=CD.Branch_Code and SOH.cash_discount_code = CD.cash_discount_code ";
                    sSQL = sSQL + " Left Outer Join Sales_Order_Receipt_Details SOR WITH (NOLOCK) On SOR.Branch_Code=SOH.Branch_Code and SOR.Document_Number = SOH.Document_Number ";
                    if (strSalesInvoiceNumer == "")
                        sSQL = sSQL + " Where SOH.Branch_Code = '" + strBranchCode + "' and isnull(SOH.status,'A') in ('A') and CONVERT(NVARCHAR,SOH.DOCUMENT_DATE,103) = '" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'  Order By Document_date desc";
                    else
                        sSQL = sSQL + " Where SOH.document_number= '" + strSalesInvoiceNumer + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSalesEntity = new SalesEntity();
                            objSalesEntity.Indicator = reader["Indidator"].ToString();
                            objSalesEntity.SalesInvoiceNumber = reader["document_number"].ToString();
                            objSalesEntity.TransactionTypeCode = reader["Transaction_Type_Code"].ToString();
                            objSalesEntity.SalesInvoiceDate = reader["document_date"].ToString();
                            objSalesEntity.CustomerCode = reader["Customer_Code"].ToString();
                            objSalesEntity.CustomerActualName = reader["Customer_Name"].ToString();
                            objSalesEntity.SalesManCode = reader["sales_man_code"].ToString();
                            objSalesEntity.SalesManName = reader["sales_man_name"].ToString();
                            objSalesEntity.LRTransfer = reader["LR_Transfer"].ToString();
                            objSalesEntity.LRNumber = reader["LR_Number"].ToString();
                            objSalesEntity.LRDate = reader["LR_date"].ToString();
                            objSalesEntity.CashDiscountCode = reader["cash_discount_code"].ToString();
                            objSalesEntity.OrderValue = Convert.ToDouble(reader["order_value"].ToString());
                            objSalesEntity.MarkingNumber = reader["Marking_Number"].ToString();
                            objSalesEntity.NumberOfCases = reader["Number_Of_Cases"].ToString();
                            objSalesEntity.Weight = reader["Weight"].ToString();
                            objSalesEntity.Carrier = reader["Carrier"].ToString();
                            objSalesEntity.FreightIndicatorCode = reader["Freight_Indicator"].ToString();
                            objSalesEntity.FreightAmount = Convert.ToDouble(reader["freight_amount"].ToString());
                            objSalesEntity.CourierCharge = Convert.ToDouble(reader["courier_charges"].ToString());
                            objSalesEntity.InsuranceCharges = Convert.ToDouble(reader["Insurance_charges"].ToString());
                            objSalesEntity.CustomerPONumber = reader["Customer_PO_Number"].ToString();
                            objSalesEntity.CustomerPODate = reader["Customer_PO_Date"].ToString();
                            objSalesEntity.SalesTaxValue = reader["Sales_Taxes"].ToString();
                            objSalesEntity.Status = reader["Status"].ToString();
                            objSalesEntity.ModeOfReceipt = reader["Mode_of_Receipt"].ToString();
                            objSalesEntity.ChequeDraftNumber = reader["Cheque_Draft_Number"].ToString();
                            objSalesEntity.ChequeDraftDate = reader["Cheque_Draft_Date"].ToString();
                            objSalesEntity.BankName = reader["Bank_Name"].ToString();
                            objSalesEntity.BankBranchName = reader["Branch_Name"].ToString();
                            objSalesEntity.LocalOutstation = reader["LOCAL_OUTSTATION"].ToString();
                            lstSalesEntity.Add(objSalesEntity);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesEntity;
        }

        public List<SalesItem> GetSalesInvoiceDetail(string strSalesInvoiceNumer, string strBranchCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select SOH.Document_Number, SM.Supplier_Name, SOD.Item_Code, SOD.Supplier_Line_Code, IM.Supplier_Part_Number, SOD.Item_Quantity, IsNull(SOD.List_Price,0) List_Price, ";
                    sSQL = sSQL + "SOD.Indicator, sod.SLB_Code, SLM.SLB_Description, IsNull(SOD.SLB_Value,0) SLB_Value, SOD.STATUS, SOD.Discount, SUM(Sales_Tax_Percentage) SalesTaxPercentage ";
                    sSQL = sSQL + "from sales_order_header SOH WITH (NOLOCK) Inner Join Sales_Order_Detail SOD WITH (NOLOCK) On SOH.Document_Number = SOD.Document_Number ";
                    sSQL = sSQL + "Inner Join Sales_Order_st STax WITH (NOLOCK) On STax.Item_Code = SOD.Item_Code  AND SOD.Document_Number = Stax.Document_Number ";
                    sSQL = sSQL + "Inner Join Item_Master IM WITH (NOLOCK) On SOD.Item_Code = IM.Item_Code Inner Join Supplier_Master SM On substring(SOD.supplier_line_code,1,3) = SM.Supplier_Code ";
                    sSQL = sSQL + "Left Outer Join SLB_Master SLM WITH (NOLOCK) On SOD.SLB_Code = SLM.SLB_Code Where SOH.Branch_Code = '" + strBranchCode + "' and SOH.document_number= '" + strSalesInvoiceNumer + "' ";
                    sSQL = sSQL + "Group By SOH.Document_Number, SM.Supplier_Name, SOD.Item_Code, SOD.Supplier_Line_Code, IM.Supplier_Part_Number,  SOD.Item_Quantity, IsNull(SOD.List_Price, 0),";
                    sSQL = sSQL + "SOD.Indicator, sod.SLB_Code, SLM.SLB_Description, IsNull(SOD.SLB_Value, 0), SOD.STATUS, SOD.Discount";

                    int count = 0;
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();
                            objSalesItem.SNO = count.ToString();
                            objSalesItem.SupplierCode = reader["Supplier_Line_Code"].ToString();
                            objSalesItem.SupplierName = reader["Supplier_Name"].ToString();
                            objSalesItem.ItemSupplierPartNumber = reader["Supplier_Part_Number"].ToString();
                            objSalesItem.ItemCode = reader["Item_Code"].ToString();
                            objSalesItem.Quantity = reader["Item_Quantity"].ToString();
                            objSalesItem.ListPrice = reader["List_Price"].ToString();
                            objSalesItem.OsLsIndicator = reader["Indicator"].ToString();
                            objSalesItem.SlbCode = reader["SLB_Code"].ToString();
                            objSalesItem.SlbDesc = reader["SLB_Description"].ToString();
                            objSalesItem.SLBNetValuePrice = Convert.ToDouble(reader["SLB_Value"].ToString());
                            objSalesItem.ItemDiscount = reader["Discount"].ToString();
                            objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public List<SalesItem> GetProformaInvoiceDetail(string strSalesInvoiceNumer, string strBranchCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select SOH.Document_Number, SM.Supplier_Name, SOD.Item_Code, SOD.Supplier_Line_Code, IM.Supplier_Part_Number, SOD.Item_Quantity, IsNull(SOD.List_Price,0) List_Price, ";
                    sSQL = sSQL + "SOD.Indicator, sod.SLB_Code, SLM.SLB_Description, IsNull(SOD.SLB_Value,0) SLB_Value, SOD.STATUS, SOD.Discount, Sum(Sales_Tax_Percentage) SalesTaxPercentage ";
                    sSQL = sSQL + "from Proforma_Invoice_Header SOH WITH (NOLOCK) Inner Join Proforma_Invoice_Detail SOD WITH (NOLOCK) On SOH.Document_Number = SOD.Document_Number ";
                    sSQL = sSQL + "inner join Proforma_Invoice_ST STax WITH (NOLOCK) Where STax.Item_Code = IM.Item_Code AND SOD.Document_Number = Stax.Document_Number ";
                    sSQL = sSQL + "Inner Join Item_Master IM WITH (NOLOCK) On SOD.Item_Code = IM.Item_Code Inner Join Supplier_Master SM On substring(SOD.supplier_line_code,1,3) = SM.Supplier_Code ";
                    sSQL = sSQL + "Inner Join SLB_Master SLM WITH (NOLOCK) On SOD.SLB_Code = SLM.SLB_Code Where SOH.Branch_Code = '" + strBranchCode + "' and SOH.document_number= '" + strSalesInvoiceNumer + "' ";
                    sSQL = sSQL + "Group By SOH.Document_Number, SM.Supplier_Name, SOD.Item_Code, SOD.Supplier_Line_Code, IM.Supplier_Part_Number,  SOD.Item_Quantity, IsNull(SOD.List_Price, 0), ";
                    sSQL = sSQL + "SOD.Indicator, sod.SLB_Code, SLM.SLB_Description, IsNull(SOD.SLB_Value, 0), SOD.STATUS, SOD.Discount";

                    int count = 0;
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();
                            objSalesItem.SNO = count.ToString();
                            objSalesItem.SupplierCode = reader["Supplier_Line_Code"].ToString();
                            objSalesItem.SupplierName = reader["Supplier_Name"].ToString();
                            objSalesItem.ItemSupplierPartNumber = reader["Supplier_Part_Number"].ToString();
                            objSalesItem.ItemCode = reader["Item_Code"].ToString();
                            objSalesItem.Quantity = reader["Item_Quantity"].ToString();
                            objSalesItem.ListPrice = reader["List_Price"].ToString();
                            objSalesItem.OsLsIndicator = reader["Indicator"].ToString();
                            objSalesItem.SlbCode = reader["SLB_Code"].ToString();
                            objSalesItem.SlbDesc = reader["SLB_Description"].ToString();
                            objSalesItem.SLBNetValuePrice = Convert.ToDouble(reader["SLB_Value"].ToString());
                            objSalesItem.ItemDiscount = reader["Discount"].ToString();
                            objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public List<SalesItem> GetFDOInwardItemDetail(string strInwardNumber, string strBranchCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select a.Reference_Document, SM.Supplier_Name, b.supplier_part_Number,a.Item_code,a.Actual_quantity,isnull(d.list_price,0) List_price,c.Customer_code,a.Os_Ls_Indicator,c.Transaction_type_code,c.branch_code  ";
                    sSQL = sSQL + "from inward_detail a WITH (NOLOCK),Item_master b WITH (NOLOCK), Inward_header C WITH (NOLOCK), Branch_Item_Price d WITH (NOLOCK), Supplier_Master SM WITH (NOLOCK) ";
                    sSQL = sSQL + "Where c.Branch_Code== '" + strBranchCode + "' and a.Item_code = d.Item_code and b.Item_code = d.Item_code and c.Branch_Code = d.Branch_Code and ";
                    sSQL = sSQL + "a.inward_Number = c.Inward_Number and a.item_Code = b.item_Code AND substring(a.supplier_line_code,1,3) = SM.Supplier_Code and a.inward_number = '" + strInwardNumber + "'";

                    int count = 0;
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();
                            objSalesItem.SNO = count.ToString();
                            objSalesItem.SupplierName = reader["Supplier_Name"].ToString();
                            objSalesItem.ItemSupplierPartNumber = reader["Supplier_Part_Number"].ToString();
                            objSalesItem.ItemCode = reader["Item_Code"].ToString();
                            objSalesItem.Quantity = reader["Actual_quantity"].ToString();
                            objSalesItem.ListPrice = reader["List_Price"].ToString();
                            objSalesItem.OsLsIndicator = reader["Os_Ls_Indicator"].ToString();
                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public List<TransactionType> GetTransactionType()
        {
            List<TransactionType> lstTransactionType = new List<TransactionType>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    TransactionType objTransactionType = new TransactionType();
                    objTransactionType.TransactionTypeCode = "0";
                    objTransactionType.TransactionTypeDesc = "-- Select --";
                    lstTransactionType.Add(objTransactionType);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select Transaction_Type_code,Transaction_Type_Description from Transaction_Type_Master WITH (NOLOCK) ";
                    sSQL = sSQL + "Where Transaction_Type_Code in ('101','041','111','171','361','421','431','441','451','481') order by Transaction_Type_Description";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objTransactionType = new TransactionType();
                            objTransactionType.TransactionTypeCode = reader[0].ToString();
                            objTransactionType.TransactionTypeDesc = reader[1].ToString();
                            lstTransactionType.Add(objTransactionType);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstTransactionType;
        }

        public List<TransactionType> GetProformaTransactionType()
        {
            List<TransactionType> lstTransactionType = new List<TransactionType>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    TransactionType objTransactionType = new TransactionType();
                    objTransactionType.TransactionTypeCode = "0";
                    objTransactionType.TransactionTypeDesc = "-- Select --";
                    lstTransactionType.Add(objTransactionType);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select Transaction_Type_code,Transaction_Type_Description from Transaction_Type_Master WITH (NOLOCK) ";
                    sSQL = sSQL + "Where Transaction_Type_Code in ('111','421') order by Transaction_Type_Description";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objTransactionType = new TransactionType();
                            objTransactionType.TransactionTypeCode = reader[0].ToString();
                            objTransactionType.TransactionTypeDesc = reader[1].ToString();
                            lstTransactionType.Add(objTransactionType);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }


            return lstTransactionType;
        }

        public List<SlabState> GetAllStatesShipping(string BranchCode)
        {
            List<SlabState> objStateList = new List<SlabState>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SlabState StateList = new SlabState();
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "SELECT DISTINCT s.State_Code,State_Name FROM State_Master S WITH (NOLOCK) LEFT OUTER JOIN Branch_Master B WITH (NOLOCK) on B.State_Code=S.State_Code";
                    if (BranchCode != "0")
                    {
                        sSQL = sSQL + " Where B.Branch_Code='" + BranchCode + "'";
                    }
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            StateList = new SlabState();
                            StateList.StateCode = reader[0].ToString();
                            StateList.StateName = reader[1].ToString();
                            objStateList.Add(StateList);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return objStateList;
        }

        public List<SalesMan> GetBranchSalesMan(string strBranchCode)
        {
            List<SalesMan> lstBranchSalesMan = new List<SalesMan>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesMan objBranchSalesMan = new SalesMan();
                    objBranchSalesMan.SalesManCode = "0";
                    objBranchSalesMan.SalesManName = "-- Select --";
                    lstBranchSalesMan.Add(objBranchSalesMan);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select Sales_Man_code,Sales_Man_Name from Sales_Man_Master WITH (NOLOCK) where branch_code  = '" + strBranchCode + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objBranchSalesMan = new SalesMan();
                            objBranchSalesMan.SalesManCode = reader[0].ToString();
                            objBranchSalesMan.SalesManName = reader[1].ToString();
                            lstBranchSalesMan.Add(objBranchSalesMan);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstBranchSalesMan;
        }

        public List<CashDiscount> GetCashDiscount(string strBranchCode)
        {
            List<CashDiscount> lstCashDiscount = new List<CashDiscount>();
            CashDiscount objCashDiscount = new CashDiscount();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase(); //CD_Percentage
                    string sSQL = "Select Cash_Discount_Code,CAST(ROUND(CD_Percentage,2) AS NUMERIC(12,2)) from Cash_Discount WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and Supplier_Line_Code='Y'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objCashDiscount = new CashDiscount();
                            objCashDiscount.CashDiscountCode = reader[0].ToString();
                            objCashDiscount.CashDiscountDesc = reader[1].ToString();
                            lstCashDiscount.Add(objCashDiscount);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstCashDiscount;
        }

        public List<SalesItem> GetItems(string strSupplier, string strPartNumber, string strTransactionType, string strBranchCode)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objItem = new SalesItem();
                    objItem.ItemCode = "0";
                    objItem.SupplierPartNumber = "-- Select --";
                    lstItems.Add(objItem);

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSalesItem");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSupplier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, strPartNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, strTransactionType.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.SupplierPartNumber = reader[0].ToString();
                            objItem.ItemCode = reader[1].ToString();

                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<SalesItem> GetProformaItems(string strSupplier, string strPartNumber, string strTransactionType)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objItem = new SalesItem();
                    objItem.ItemCode = "0";
                    objItem.SupplierPartNumber = "-- Select --";
                    lstItems.Add(objItem);

                    Database ImpalDB = DataAccess.GetDatabase();

                    string sSQL;
                    sSQL = "Select distinct Supplier_Part_Number,Item_Code from item_master WITH (NOLOCK) where substring(supplier_line_code,1,3) =" + "'" + strSupplier + "' and supplier_part_number like " + "'" + strPartNumber + "%' Order by Supplier_Part_Number asc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.SupplierPartNumber = reader[0].ToString();
                            objItem.ItemCode = reader[1].ToString();

                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<CustomerSalesReqItem> GetCustomerSalesReqItems(string strSupplier, string strPartNumber)
        {
            List<CustomerSalesReqItem> lstItems = new List<CustomerSalesReqItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    CustomerSalesReqItem objItem = new CustomerSalesReqItem();
                    objItem.ItemCode = "0";
                    objItem.SupplierPartNumber = "-- Select --";
                    lstItems.Add(objItem);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL; sSQL = "Select distinct Supplier_Part_Number,Item_Code from item_master WITH (NOLOCK) Where substring(supplier_line_code,1,3) = '" + strSupplier + "' and supplier_part_number like '" + strPartNumber + "%' Order by Supplier_Part_Number asc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new CustomerSalesReqItem();
                            objItem.SupplierPartNumber = reader[0].ToString();
                            objItem.ItemCode = reader[1].ToString();

                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<CustomerSalesReqItem> GetCustomerSalesReqItemsIndHexReg(string strSupplier, string strPartNumber)
        {
            List<CustomerSalesReqItem> lstItems = new List<CustomerSalesReqItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    CustomerSalesReqItem objItem = new CustomerSalesReqItem();
                    objItem.ItemCode = "0";
                    objItem.SupplierPartNumber = "-- Select --";
                    lstItems.Add(objItem);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL; sSQL = "Select distinct i.Supplier_Part_Number,i.Item_Code from item_master i WITH (NOLOCK) LEFT join Indl_Hex_Part_Numbers i1 WITH (NOLOCK) ";
                    sSQL = sSQL + "on substring(i.Item_Code,1,3)=i1.Supplier_Code and i.Supplier_Part_Number=i1.Supplier_Part_Number ";
                    sSQL = sSQL + "WHERE substring(i.supplier_line_code,1,3) ='" + strSupplier + "' and i.Supplier_Part_Number like '" + strPartNumber + "%' ";
                    sSQL = sSQL + "and i1.supplier_part_number is null Order by i.Supplier_Part_Number asc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new CustomerSalesReqItem();
                            objItem.SupplierPartNumber = reader[0].ToString();
                            objItem.ItemCode = reader[1].ToString();

                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<CustomerSalesReqItem> GetCustomerSalesReqItemsIndHex(string strSupplier, string strPartNumber)
        {
            List<CustomerSalesReqItem> lstItems = new List<CustomerSalesReqItem>();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    CustomerSalesReqItem objItem = new CustomerSalesReqItem();
                    objItem.ItemCode = "0";
                    objItem.SupplierPartNumber = "-- Select --";
                    lstItems.Add(objItem);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL; sSQL = "Select distinct i.Supplier_Part_Number,i.Item_Code from item_master i WITH (NOLOCK) LEFT join Indl_Hex_Part_Numbers i1 WITH (NOLOCK) ";
                    sSQL = sSQL + "on substring(i.Item_Code,1,3)=i1.Supplier_Code and i.Supplier_Part_Number=i1.Supplier_Part_Number ";
                    sSQL = sSQL + "WHERE substring(i.supplier_line_code,1,3) ='" + strSupplier + "' and i.Supplier_Part_Number like '" + strPartNumber + "%' ";
                    sSQL = sSQL + "and i1.supplier_part_number is not null Order by i.Supplier_Part_Number asc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new CustomerSalesReqItem();
                            objItem.SupplierPartNumber = reader[0].ToString();
                            objItem.ItemCode = reader[1].ToString();

                            lstItems.Add(objItem);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<SalesItem> GetItemPrice(string strItemCode, string strBranchCode)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            SalesItem objItem = new SalesItem();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select b.OS_LS_Indicator, b.List_Price, b.cost_price, a.Packing_Quantity, b.ED_Indicator, b.depot_code, dm.depot_long_name, a.Product_Group_Code, 0 Coupon_Charges from Consignment b WITH (NOLOCK) ";
                    sSQL = sSQL + "Inner join item_master a WITH (NOLOCK) On a.item_code = b.item_code Left Outer Join depot_master dm WITH (NOLOCK) On dm.depot_code = b.depot_code ";
                    sSQL = sSQL + "Where b.item_code = " + "'" + strItemCode + "'" + " and b.Status = 'A' and b.branch_code = '" + strBranchCode + "' order by b.original_receipt_date";// order by b.original_receipt_date";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.OsLsIndicator = reader[0].ToString();
                            objItem.ListPrice = reader[1].ToString();
                            objItem.CostPrice = reader[2].ToString();
                            objItem.PackingQuantity = reader[3].ToString();
                            objItem.EDIndicator = reader[4].ToString();
                            objItem.DepotCode = reader[5].ToString();
                            objItem.DepotLongName = reader[6].ToString();
                            objItem.ItemCode = strItemCode.ToString();
                            objItem.ProductGroupCode = reader[7].ToString();
                            objItem.PurchaseDiscount = reader[8].ToString();
                            lstItems.Add(objItem);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<SalesItem> GetProformaItemPrice(string strItemCode, string strBranchCode)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            SalesItem objItem = new SalesItem();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select 'O', b.List_Price, b.cost_price, a.Packing_Quantity, '', '', '', a.Product_Group_Code from item_master a WITH (NOLOCK) ";
                    sSQL = sSQL + "Inner join Branch_Item_Price b WITH (NOLOCK) On a.item_code = b.item_code and b.item_code = " + "'" + strItemCode + "'" + " and b.branch_code = '" + strBranchCode + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.OsLsIndicator = reader[0].ToString();
                            objItem.ListPrice = reader[1].ToString();
                            objItem.CostPrice = reader[2].ToString();
                            objItem.PackingQuantity = reader[3].ToString();
                            objItem.EDIndicator = reader[4].ToString();
                            objItem.DepotCode = reader[5].ToString();
                            objItem.DepotLongName = reader[6].ToString();
                            objItem.ItemCode = strItemCode.ToString();
                            objItem.ProductGroupCode = reader[7].ToString();
                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<SalesItem> GetItemQty(string strItemCode, string strBranchCode, string TransactionType)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            SalesItem objItem = new SalesItem();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemQuantityDetail");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, TransactionType.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.Quantity = reader[0].ToString();
                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public List<SalesItem> GetSLB(string strCustomerCode, string strItemCode, string strBranchCode, string TownCode)
        {
            List<SalesItem> lstSLB = new List<SalesItem>();
            string sSQL = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {

                    SalesItem objSLB = new SalesItem();
                    objSLB.SlbCode = "0";
                    objSLB.SlbDesc = "-- Select --";
                    lstSLB.Add(objSLB);

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSLB_SalesRequest_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, TownCode.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSLB = new SalesItem();
                            objSLB.SlbCode = reader[0].ToString();
                            objSLB.SlbDesc = reader[1].ToString();
                            lstSLB.Add(objSLB);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSLB;
        }

        public string GetSupplierCouponIndicator(string strSupplierCode, string strBranchCode)
        {
            string couponInd = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("Select isnull(Telex,'') from Supplier_Master WITH (NOLOCK) where Supplier_Code = '" + strSupplierCode + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            couponInd = reader[0].ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }
            return couponInd;
        }

        public string GetCrystalRptPrintBtnIndicator(string strReportName)
        {
            string Prtstatus = "I";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (strReportName != null)
                    {
                        Database ImpalDB = DataAccess.GetDatabase();
                        DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Status from CrystalReport_List WITH (NOLOCK) where ReportName = '" + strReportName + "'");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        Prtstatus = ImpalDB.ExecuteScalar(cmd).ToString();
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return Prtstatus;
        }

        public void UpdRptPrtExecCount(string strBranchCode, string strUserid, string strReportName)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (strReportName != null)
                    {
                        Database ImpalDB = DataAccess.GetDatabase();
                        DbCommand cmd = ImpalDB.GetSqlStringCommand("Insert into RptPrt_ExecCount_Daily values ('" + strBranchCode + "', '" + strUserid + "', '" + strReportName + "', GETDATE())");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }
        }

        public List<SalesItem> GetSLBSalesReq(string strCustomerCode, string strItemCode, string strBranchCode, string TownCode)
        {
            List<SalesItem> lstSLB = new List<SalesItem>();
            string sSQL = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSLB = new SalesItem();
                    objSLB.SlbCode = "0";
                    objSLB.SlbDesc = "-- Select --";
                    lstSLB.Add(objSLB);

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSLB_SalesRequest_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, TownCode.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objSLB = new SalesItem();
                            objSLB.SlbCode = reader[0].ToString();
                            objSLB.SlbDesc = reader[1].ToString();
                            lstSLB.Add(objSLB);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSLB;
        }

        public string GetBranchItemPrice(string strItemCode, string strBranchCode)
        {
            string britempr = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Top 1 List_Price from branch_item_price WITH (NOLOCK) where branch_code = '" + strBranchCode + "' and item_Code = '" + strItemCode + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            britempr = reader[0].ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return britempr;
        }

        public List<SalesItem> GetBranchItemPriceBO(string strItemCode, string strBranchCode)
        {
            List<SalesItem> lstItems = new List<SalesItem>();
            SalesItem objItem = new SalesItem();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Top 1 b.List_Price,i.Packing_Quantity,i.Minimum_Order_Quantity,i.Item_Short_Description from branch_item_price b WITH (NOLOCK) inner join Item_Master i WITH (NOLOCK) on b.branch_code =  '" + strBranchCode + "' and b.item_Code = '" + strItemCode + "' and b.Item_Code=i.Item_Code");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objItem = new SalesItem();
                            objItem.ListPrice = reader[0].ToString();
                            objItem.PackingQuantity = reader[1].ToString();
                            objItem.MinOrdQuantity = reader[2].ToString();
                            objItem.ItemDescription = reader[3].ToString();
                            lstItems.Add(objItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstItems;
        }

        public int CheckHSNCodeItem(string strItemCode, string strBranchCode)
        {
            int HSNcode = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("select case when len(isnull(Item_Type_Code,0))<= 3 then 0 else len(Item_Type_Code) end from Item_Master WITH (NOLOCK) where Item_Code='" + strItemCode + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    HSNcode = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return HSNcode;
        }

        public double GetSalesTax(string strCustomerCode, string strItemCode, string strBranchCode, string SLBIndicator, string Transactiontype, string ProductGroupCode)
        {
            double taxPercentage = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetTaxPercentageFor_Sales_GST");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.Int16, 0);
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, Transactiontype.Trim());
                    ImpalDB.AddInParameter(cmd, "@Original_Trans_Code", DbType.String, Transactiontype.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    taxPercentage = double.Parse(ImpalDB.ExecuteScalar(cmd).ToString());
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return taxPercentage;
        }

        public double GetAddlSalesTaxGST(string strCustomerCode, string strItemCode, string strBranch, string SLBIndicator, string Transactiontype, string ProductGroupCode)
        {
            double taxPercentage = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetAddlTaxPercentageFor_Sales_GST");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranch.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.String, ProductGroupCode);
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, Transactiontype.Trim());
                    ImpalDB.AddInParameter(cmd, "@Original_Trans_Code", DbType.String, Transactiontype.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    taxPercentage = double.Parse(ImpalDB.ExecuteScalar(cmd).ToString());

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return taxPercentage;
        }

        public double GetItemSellingPrice(string strCustomerCode, string strItemCode, string strBranchCode, int Qty, double BranchListPrice, double SLBNetValuePrice, string Indicator, string TransactionType, int SLBCode)
        {
            double ItemSellingPrice = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getlistprice");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@LIST_PRICE", DbType.Double, BranchListPrice);
                    ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.Double, SLBNetValuePrice);
                    ImpalDB.AddInParameter(cmd, "@slb_code", DbType.String, SLBCode);
                    ImpalDB.AddInParameter(cmd, "@transaction_Type_code", DbType.String, TransactionType.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            ItemSellingPrice = double.Parse(reader[0].ToString());
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ItemSellingPrice;
        }


        public double GetFDOItemSellingPrice(string strCustomerCode, string strItemCode, string strBranchCode, int Qty, double BranchListPrice, double SLBNetValuePrice, string Indicator, string TransactionType, int SLBCode)
        {
            double ItemSellingPrice = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getlistprice_E1");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@LIST_PRICE", DbType.Double, BranchListPrice);
                    ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.Double, SLBNetValuePrice);
                    ImpalDB.AddInParameter(cmd, "@slb_code", DbType.String, SLBCode);
                    ImpalDB.AddInParameter(cmd, "@transaction_Type_code", DbType.String, TransactionType.Trim());

                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            ItemSellingPrice = double.Parse(reader[0].ToString());

                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ItemSellingPrice;
        }

        public double GetSLBNetValuePrice(string strCustomerCode, string strItemCode, string strBranchCode, int Qty, double BranchListPrice, string Indicator, string TransactionType, int SLBCode)
        {
            double SLBNetValuePrice = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSLBPrice");
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@quantity", DbType.Int32, Qty);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@BranchListPrice", DbType.Double, BranchListPrice);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, TransactionType.Trim());
                    ImpalDB.AddInParameter(cmd, "@slb_code", DbType.String, SLBCode);

                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            SLBNetValuePrice = double.Parse(reader[0].ToString());
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return SLBNetValuePrice;
        }

        public double GetCouponValue(string strBranchCode, string strCustomerCode, string strSupplierCode, string strItemCode, int Qty, string TransactionType)
        {
            double couponValue = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCouponValue");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@quantity", DbType.Int32, Qty);
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, TransactionType.Trim());

                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            couponValue = double.Parse(reader[0].ToString());
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return couponValue;
        }

        public DataSet AddNewSalesInvoiceEntry(ref SalesEntity saleEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            DataSet ds = new DataSet();

            string SalesInvoiceNumber = "";
            DbCommand cmd = null;
            int count = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSalesOrderHeader_GST");
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, saleEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, saleEntity.CustomerCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, saleEntity.SalesManCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, saleEntity.RefDocumentNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, "");//saleEntity.RefDocumentDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, saleEntity.Remarks.Trim());
                    ImpalDB.AddInParameter(cmd, "@Cash_Discount_Code", DbType.String, saleEntity.CashDiscountCode);
                    ImpalDB.AddInParameter(cmd, "@Order_Value", DbType.Double, saleEntity.OrderValue);
                    ImpalDB.AddInParameter(cmd, "@LR_Transfer", DbType.String, saleEntity.LRTransfer.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, saleEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, saleEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Marking_Number", DbType.String, saleEntity.MarkingNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Number_Of_Cases", DbType.String, saleEntity.NumberOfCases);
                    if (string.IsNullOrEmpty(saleEntity.Weight.Trim())) saleEntity.Weight = "0";
                    ImpalDB.AddInParameter(cmd, "@Weight", DbType.Decimal, Convert.ToDecimal(saleEntity.Weight.Trim()));
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, saleEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Freight_Indicator", DbType.String, saleEntity.FreightIndicatorCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(saleEntity.FreightAmount));
                    ImpalDB.AddInParameter(cmd, "@Courier_Charges", DbType.Decimal, Convert.ToDecimal(saleEntity.CourierCharge));
                    ImpalDB.AddInParameter(cmd, "@Insurance_Charges", DbType.Decimal, Convert.ToDecimal(saleEntity.InsuranceCharges));
                    ImpalDB.AddInParameter(cmd, "@Mode_Of_Receipt", DbType.String, saleEntity.ModeOfReceipt.Trim());
                    ImpalDB.AddInParameter(cmd, "@Cheque_Draft_Number", DbType.String, saleEntity.ChequeDraftNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Cheque_Draft_Date", DbType.String, saleEntity.ChequeDraftDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Bank_Name", DbType.String, saleEntity.BankName.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, saleEntity.BankBranchName.Trim());
                    ImpalDB.AddInParameter(cmd, "@Amount_Received", DbType.Double, saleEntity.AmountReceived);
                    ImpalDB.AddInParameter(cmd, "@Balance_Amount", DbType.Double, saleEntity.BalanceAmount);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, saleEntity.CustomerName);
                    ImpalDB.AddInParameter(cmd, "@Customer_Town", DbType.String, saleEntity.CustomerTown);
                    ImpalDB.AddInParameter(cmd, "@Insindicator", DbType.String, ""); // Not Used on SP
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "V");
                    ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, saleEntity.ReceiptLocalOutstation.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_PO", DbType.String, saleEntity.CustomerPONumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Customer_PO_Date", DbType.String, saleEntity.CustomerPODate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    SalesInvoiceNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    cmd = null;

                    if (SalesInvoiceNumber != "")
                    {
                        foreach (SalesItem saleItem in saleEntity.Items)
                        {
                            count = count + 1;

                            cmd = ImpalDB.GetStoredProcCommand("usp_AddSalesOrderDetail_GST");
                            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, SalesInvoiceNumber);
                            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, saleEntity.TransactionTypeCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, saleEntity.CustomerCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@Discount", DbType.Decimal, Convert.ToDecimal(saleItem.ItemDiscount));
                            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, saleItem.ItemCode);
                            ImpalDB.AddInParameter(cmd, "@Item_Quantity", DbType.Int32, saleItem.Quantity);
                            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, saleItem.ListPrice);
                            ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, saleItem.OsLsIndicator);//+ inwardItem.ItemCode);
                            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, saleItem.SlbCode);
                            ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.String, saleItem.SLBNetValuePrice);
                            ImpalDB.AddInParameter(cmd, "@valuetype", DbType.String, saleItem.ValueType); //Need to verify based on OSLS Indicator and TransactionType
                            ImpalDB.AddInParameter(cmd, "@CostToCostCoupon", DbType.String, saleEntity.CostToCostCouponInd);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                            cmd = null;

                            if (!(saleEntity.TransactionTypeCode.Trim() == "041" || saleEntity.TransactionTypeCode.Trim() == "141" || saleEntity.TransactionTypeCode.Trim() == "071" || saleEntity.TransactionTypeCode.Trim() == "171" || saleEntity.TransactionTypeCode.Trim() == "421" || saleEntity.TransactionTypeCode.Trim() == "431" || saleEntity.TransactionTypeCode.Trim() == "441" || saleEntity.TransactionTypeCode.Trim() == "461"))
                            {
                                cmd = ImpalDB.GetStoredProcCommand("usp_UpdBackOrder");
                                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                                ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, saleEntity.CustomerCode.Trim());
                                ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, saleEntity.CustSalesReqNumber);
                                ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, SalesInvoiceNumber);
                                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, saleItem.ItemCode);
                                ImpalDB.AddInParameter(cmd, "@Stock", DbType.Int32, saleItem.AvialableQuantity);
                                ImpalDB.AddInParameter(cmd, "@Supplied_Qty", DbType.Int32, saleItem.Quantity);
                                ImpalDB.AddInParameter(cmd, "@Document_Value", DbType.String, saleEntity.OrderValue);
                                ImpalDB.AddInParameter(cmd, "@Status", DbType.String, "A");
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                cmd = null;
                            }
                        }
                    
                        cmd = ImpalDB.GetStoredProcCommand("usp_addinvrec_GST");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, SalesInvoiceNumber);
                        ImpalDB.AddInParameter(cmd, "@ind", DbType.String, saleEntity.Indicator.Trim());
                        ImpalDB.AddInParameter(cmd, "@ShippingName", DbType.String, saleEntity.ShippingName);
                        ImpalDB.AddInParameter(cmd, "@ShippingAddress1", DbType.String, saleEntity.ShippingAddress1);
                        ImpalDB.AddInParameter(cmd, "@ShippingAddress2", DbType.String, saleEntity.ShippingAddress2);
                        ImpalDB.AddInParameter(cmd, "@ShippingAddress4", DbType.String, saleEntity.ShippingAddress4);
                        ImpalDB.AddInParameter(cmd, "@ShippingGSTIN", DbType.String, saleEntity.ShippingGSTIN);
                        ImpalDB.AddInParameter(cmd, "@ShippingLocation", DbType.String, saleEntity.ShippingLocation);
                        ImpalDB.AddInParameter(cmd, "@ShippingState", DbType.String, saleEntity.ShippingState);
                        ImpalDB.AddInParameter(cmd, "@DispLocInd", DbType.String, saleEntity.DispatchLocationInd);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);                        
                        cmd = null;

                        //if (!(saleEntity.TransactionTypeCode.Trim() == "071" || saleEntity.TransactionTypeCode.Trim() == "171")) //&& replaced
                        //{
                            cmd = ImpalDB.GetStoredProcCommand("usp_addglso1_GST");
                            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, SalesInvoiceNumber);
                            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                            cmd = null;
                        //}

                        if ((saleEntity.TransactionTypeCode.Trim() == "001") || (saleEntity.TransactionTypeCode.Trim() == "101"))
                        {
                            cmd = ImpalDB.GetStoredProcCommand("usp_addglso4_GST");
                            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, SalesInvoiceNumber);
                            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                            cmd = null;
                        }

                        cmd = ImpalDB.GetStoredProcCommand("usp_getCustGST_Status");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, saleEntity.CustomerCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Doc_No", DbType.String, SalesInvoiceNumber.Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmd);
                        cmd = null;
                    }

                    saleEntity.SalesInvoiceNumber = SalesInvoiceNumber;
                    saleEntity.ErrorCode = "0";
                    saleEntity.ErrorMsg = "";

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = 0;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public int AddNewProformaInvoice(ref SalesEntity saleEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            string SalesInvoiceNumber = "";
            string SalesManReqNumber = "";
            string indicator = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    int count = 0;

                    foreach (SalesItem saleItem in saleEntity.Items)
                    {
                        count = count + 1;

                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddProformaInvoice");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, SalesInvoiceNumber);
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, saleEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, saleEntity.CustomerCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, saleEntity.SalesManCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, SalesManReqNumber);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, "");//saleEntity.RefDocumentDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, saleEntity.Remarks.Trim());
                        ImpalDB.AddInParameter(cmd, "@Cash_Discount_Code", DbType.String, saleEntity.CashDiscountCode);
                        ImpalDB.AddInParameter(cmd, "@Order_Value", DbType.Double, saleEntity.OrderValue);
                        ImpalDB.AddInParameter(cmd, "@LR_Transfer", DbType.String, saleEntity.LRTransfer.Trim());
                        ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, saleEntity.LRNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, saleEntity.LRDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Marking_Number", DbType.String, saleEntity.MarkingNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Number_Of_Cases", DbType.String, saleEntity.NumberOfCases);
                        if (string.IsNullOrEmpty(saleEntity.Weight.Trim())) saleEntity.Weight = "0";
                        ImpalDB.AddInParameter(cmd, "@Weight", DbType.Decimal, Convert.ToDecimal(saleEntity.Weight.Trim()));
                        ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, saleEntity.Carrier.Trim());
                        ImpalDB.AddInParameter(cmd, "@Freight_Indicator", DbType.String, saleEntity.FreightIndicatorCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(saleEntity.FreightAmount));
                        ImpalDB.AddInParameter(cmd, "@Courier_Charges", DbType.Decimal, Convert.ToDecimal(saleEntity.CourierCharge));
                        if (saleItem.ItemDiscount == "") saleItem.ItemDiscount = "0";
                        ImpalDB.AddInParameter(cmd, "@Discount", DbType.Decimal, Convert.ToDecimal(saleItem.ItemDiscount));
                        ImpalDB.AddInParameter(cmd, "@Insurance_Charges", DbType.Decimal, Convert.ToDecimal(saleEntity.InsuranceCharges));
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, saleItem.ItemCode);
                        ImpalDB.AddInParameter(cmd, "@Item_Quantity", DbType.Int32, saleItem.Quantity);
                        ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, saleItem.ListPrice);
                        ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, saleItem.OsLsIndicator);//+ inwardItem.ItemCode);
                        ImpalDB.AddInParameter(cmd, "@Mode_Of_Receipt", DbType.String, saleEntity.ModeOfReceipt.Trim());
                        ImpalDB.AddInParameter(cmd, "@Cheque_Draft_Number", DbType.String, saleEntity.ChequeDraftNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Cheque_Draft_Date", DbType.String, saleEntity.ChequeDraftDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Bank_Name", DbType.String, saleEntity.BankName.Trim());
                        ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, saleEntity.BankBranchName.Trim());
                        ImpalDB.AddInParameter(cmd, "@Amount_Received", DbType.Double, saleEntity.AmountReceived);
                        ImpalDB.AddInParameter(cmd, "@Balance_Amount", DbType.Double, saleEntity.BalanceAmount);
                        ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, saleEntity.CustomerName);
                        ImpalDB.AddInParameter(cmd, "@Customer_Town", DbType.String, saleEntity.CustomerTown);
                        ImpalDB.AddInParameter(cmd, "@Insindicator", DbType.String, ""); // Not Used on SP
                        if (count < saleEntity.Items.Count) indicator = "N";
                        else indicator = "V";
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, indicator);
                        ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, saleItem.SlbCode);
                        ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.String, saleItem.SLBNetValuePrice);
                        ImpalDB.AddInParameter(cmd, "@valuetype", DbType.String, saleItem.ValueType); //Need to verify based on OSLS Indicator and TransactionType
                        ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, saleEntity.ReceiptLocalOutstation.Trim());
                        ImpalDB.AddInParameter(cmd, "@Customer_PO", DbType.String, saleEntity.CustomerPONumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Customer_PO_Date", DbType.String, saleEntity.CustomerPODate.Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        SalesInvoiceNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    if (SalesInvoiceNumber != "")
                    {
                        DbCommand cmdInv = ImpalDB.GetStoredProcCommand("usp_addinvrec_Proforma");
                        ImpalDB.AddInParameter(cmdInv, "@Branch_Code", DbType.String, saleEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdInv, "@Document_Number", DbType.String, SalesInvoiceNumber);
                        ImpalDB.AddInParameter(cmdInv, "@ind", DbType.String, saleEntity.Indicator.Trim());
                        ImpalDB.AddInParameter(cmdInv, "@ShippingName", DbType.String, saleEntity.ShippingName);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingAddress1", DbType.String, saleEntity.ShippingAddress1);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingAddress2", DbType.String, saleEntity.ShippingAddress2);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingAddress4", DbType.String, saleEntity.ShippingAddress4);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingGSTIN", DbType.String, saleEntity.ShippingGSTIN);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingLocation", DbType.String, saleEntity.ShippingLocation);
                        ImpalDB.AddInParameter(cmdInv, "@ShippingState", DbType.String, saleEntity.ShippingState);
                        cmdInv.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdInv);
                    }

                    saleEntity.SalesInvoiceNumber = SalesInvoiceNumber;
                    saleEntity.ErrorCode = "0";
                    saleEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public DataSet CancelSalesInvoice(string BranchCode, string CustomerCode, string SalesInvoiceNumber, string TransactionCode, string CustSalesReqNumber, string Status, string Remarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (SalesInvoiceNumber != "")
                    {
                        DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_UpdSalesOrderheaderstatus_GST");
                        ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Customer_Code", DbType.String, CustomerCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Document_Number", DbType.String, SalesInvoiceNumber);                        
                        ImpalDB.AddInParameter(cmdHeader, "@Transaction_Type_Code", DbType.String, TransactionCode);
                        ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, Status);
                        ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, Remarks);
                        cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdHeader);

                        //DbCommand cmdMis = ImpalDB.GetStoredProcCommand("usp_updmissale");
                        //ImpalDB.AddInParameter(cmdMis, "@Branch_Code", DbType.String, BranchCode);
                        //ImpalDB.AddInParameter(cmdMis, "@Document_Number", DbType.String, SalesInvoiceNumber);                        
                        //ImpalDB.AddInParameter(cmdMis, "@Indicator", DbType.String, "R");
                        //cmdMis.CommandTimeout = ConnectionTimeOut.TimeOut;
                        //ImpalDB.ExecuteNonQuery(cmdMis);

                        //DbCommand cmdAvg = ImpalDB.GetStoredProcCommand("usp_updavgsale");
                        //ImpalDB.AddInParameter(cmdAvg, "@Branch_Code", DbType.String, BranchCode);
                        //ImpalDB.AddInParameter(cmdAvg, "@Document_Number", DbType.String, SalesInvoiceNumber);                        
                        //ImpalDB.AddInParameter(cmdAvg, "@Transaction_Type_Code", DbType.String, TransactionCode);
                        //cmdAvg.CommandTimeout = ConnectionTimeOut.TimeOut;
                        //ImpalDB.ExecuteNonQuery(cmdAvg);

                        if (!(TransactionCode == "041" || TransactionCode == "141" || TransactionCode == "071" || TransactionCode == "171" || TransactionCode == "421" || TransactionCode == "431" || TransactionCode == "441"))
                        {
                            DbCommand cmdBO = ImpalDB.GetStoredProcCommand("usp_UpdBackOrder");
                            ImpalDB.AddInParameter(cmdBO, "@Branch_Code", DbType.String, BranchCode);
                            ImpalDB.AddInParameter(cmdBO, "@Customer_Code", DbType.String, CustomerCode);
                            ImpalDB.AddInParameter(cmdBO, "@Request_Number", DbType.String, CustSalesReqNumber);
                            ImpalDB.AddInParameter(cmdBO, "@Invoice_Number", DbType.String, SalesInvoiceNumber);
                            ImpalDB.AddInParameter(cmdBO, "@Item_Code", DbType.String, "");
                            ImpalDB.AddInParameter(cmdBO, "@Stock", DbType.String, null);
                            ImpalDB.AddInParameter(cmdBO, "@Supplied_Qty", DbType.Int32, 0);
                            ImpalDB.AddInParameter(cmdBO, "@Document_Value", DbType.String, "");
                            ImpalDB.AddInParameter(cmdBO, "@Status", DbType.String, Status);
                            cmdBO.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdBO);
                        }

                        DbCommand cmdInvData = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_Data_Cancel");
                        ImpalDB.AddInParameter(cmdInvData, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmdInvData, "@Customer_Code", DbType.String, CustomerCode);
                        ImpalDB.AddInParameter(cmdInvData, "@doc_no", DbType.String, SalesInvoiceNumber);
                        cmdInvData.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmdInvData);
                    }

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public List<CustomerSalesReqEntity> GetCustomerSalesReqNumber(string strBranchCode, string CustomerCode)
        {
            List<CustomerSalesReqEntity> lstCustSalesEntity = new List<CustomerSalesReqEntity>();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    CustomerSalesReqEntity objCustSalesEntity = new CustomerSalesReqEntity();
                    objCustSalesEntity.CustomerSalesReqNumVal = "0";
                    objCustSalesEntity.CustomerSalesReqNumber = "-- Select --";
                    lstCustSalesEntity.Add(objCustSalesEntity);

                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "Select distinct Request_Number from Back_Order WITH (NOLOCK) Where Branch_Code = '" + strBranchCode + "' and Customer_Code = '" + CustomerCode + "' and isnull(status,'A') in ('A','P') order by Request_Number desc";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            objCustSalesEntity = new CustomerSalesReqEntity();
                            objCustSalesEntity.CustomerSalesReqNumVal = reader[0].ToString();
                            objCustSalesEntity.CustomerSalesReqNumber = reader[0].ToString();
                            lstCustSalesEntity.Add(objCustSalesEntity);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstCustSalesEntity;
        }

        public List<SalesItem> GetCustomerSalesReqTaxDetails(string strSalesReqNumber, string strTransactionType, string strCashDiscountPer, string OSLSFilter, string TaxFilter, string strBranchCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    int count = 0;
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSales_BackOrder_Tax_Details_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strSalesReqNumber);
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, strTransactionType);
                    ImpalDB.AddInParameter(cmd, "@OSLSFilter", DbType.String, OSLSFilter);
                    ImpalDB.AddInParameter(cmd, "@TaxFilter", DbType.String, TaxFilter);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();

                            //if (OSLSFilter == "Y" && TaxFilter == "")
                            //{
                            //    objSalesItem.OsLsIndicator = reader["Os_Ls_Indicator"].ToString();
                            //    objSalesItem.OSLSIndDesc = reader["OSLSIndDesc"].ToString();
                            //}
                            //else if (OSLSFilter == "" && TaxFilter == "Y")
                            //{
                                objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                                objSalesItem.SalesTaxPerDesc = reader["SalesTaxPerDesc"].ToString();
                            //}

                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public List<SalesItem> GetCustomerSalesReqDetails(string strSalesReqNumber, string strTransactionType, string strCashDiscountPer, string OSLSFilter, string TaxFilter, string strBranchCode, string TownCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    int count = 0;
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSales_BackOrder_Details_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strSalesReqNumber);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, TownCode);
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, strTransactionType);
                    ImpalDB.AddInParameter(cmd, "@CashDiscountPer", DbType.String, strCashDiscountPer);
                    ImpalDB.AddInParameter(cmd, "@OSLSFilter", DbType.String, OSLSFilter);
                    ImpalDB.AddInParameter(cmd, "@TaxFilter", DbType.String, TaxFilter);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();

                            if (OSLSFilter == "Y" && TaxFilter == "")
                            {
                                objSalesItem.OsLsIndicator = reader["Os_Ls_Indicator"].ToString();
                                objSalesItem.OSLSIndDesc = reader["OSLSIndDesc"].ToString();
                            }
                            else if (OSLSFilter == "" && TaxFilter == "Y")
                            {
                                objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                                objSalesItem.SalesTaxPerDesc = reader["SalesTaxPerDesc"].ToString();
                            }
                            else
                            {
                                objSalesItem.SNO = count.ToString();
                                objSalesItem.SupplierCode = reader["Supplier_Code"].ToString();
                                objSalesItem.SupplierName = reader["Supplier_Name"].ToString();
                                objSalesItem.ItemSupplierPartNumber = reader["Supplier_Part_Number"].ToString();
                                objSalesItem.ItemCode = reader["Item_Code"].ToString();
                                objSalesItem.Quantity = reader["Req_Qty"].ToString();
                                objSalesItem.AvialableQuantity = reader["Balance_Quantity"].ToString();
                                objSalesItem.OriginalReqQty = reader["Req_Qty"].ToString();
                                objSalesItem.ListPrice = reader["List_Price"].ToString();
                                objSalesItem.CostPrice = reader["Cost_Price"].ToString();
                                objSalesItem.OsLsIndicator = reader["Os_Ls_Indicator"].ToString();
                                objSalesItem.SlbCode = reader["SLB_Code"].ToString();
                                objSalesItem.SlbDesc = reader["SLB_Description"].ToString();
                                objSalesItem.SLBNetValuePrice = Convert.ToDouble(reader["SLB_Value"].ToString());
                                objSalesItem.ItemDiscount = reader["Discount"].ToString();
                                objSalesItem.SalesTaxText = reader["SalesTaxText"].ToString();
                                objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                                objSalesItem.SellingPrice = reader["SellingPrice"].ToString();
                                objSalesItem.GrossProfit = reader["Gross_Profit"].ToString();
                                objSalesItem.SaleValue = reader["SaleValue"].ToString();
                                objSalesItem.ProductGroupCode = reader["Product_Group_Code"].ToString();
                                objSalesItem.CouponIndicator = reader["Coupon_Indicator"].ToString();
                                objSalesItem.DispatchLocationInd = reader["DispatchLoc_Ind"].ToString();
                                objSalesItem.DispatchLocationCount = reader["DispatchLoc_Cnt"].ToString();
                            }

                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public List<SalesItem> GetCustomerSalesReqItemDetails(string strSalesReqNumber, string strTransactionType, string strCashDiscountPer, string strItemCode, string strSLBCode, string strBranchCode, string strCoupon, string TownCode)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    SalesItem objSalesItem = new SalesItem();
                    Database ImpalDB = DataAccess.GetDatabase();
                    int count = 0;
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSales_BackOrder_Item_Details_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strSalesReqNumber);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, TownCode);
                    ImpalDB.AddInParameter(cmd, "@TransactionType", DbType.String, strTransactionType);
                    ImpalDB.AddInParameter(cmd, "@CashDiscountPer", DbType.String, strCashDiscountPer);
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode);
                    ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, strSLBCode);
                    ImpalDB.AddInParameter(cmd, "@Coupon", DbType.String, strCoupon);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            count = count + 1;
                            objSalesItem = new SalesItem();
                            objSalesItem.SNO = count.ToString();
                            objSalesItem.SupplierCode = reader["Supplier_Code"].ToString();
                            objSalesItem.SupplierName = reader["Supplier_Name"].ToString();
                            objSalesItem.ItemSupplierPartNumber = reader["Supplier_Part_Number"].ToString();
                            objSalesItem.ItemCode = reader["Item_Code"].ToString();
                            objSalesItem.Quantity = reader["Req_Qty"].ToString();
                            objSalesItem.AvialableQuantity = reader["Balance_Quantity"].ToString();
                            objSalesItem.OriginalReqQty = reader["Req_Qty"].ToString();
                            objSalesItem.ListPrice = reader["List_Price"].ToString();
                            objSalesItem.CostPrice = reader["Cost_Price"].ToString();
                            objSalesItem.OsLsIndicator = reader["Os_Ls_Indicator"].ToString();
                            objSalesItem.SlbCode = reader["SLB_Code"].ToString();
                            objSalesItem.SlbDesc = reader["SLB_Description"].ToString();
                            objSalesItem.SLBNetValuePrice = Convert.ToDouble(reader["SLB_Value"].ToString());
                            objSalesItem.ItemDiscount = reader["Discount"].ToString();
                            objSalesItem.SalesTaxText = reader["SalesTaxText"].ToString();
                            objSalesItem.SalesTaxPercentage = reader["SalesTaxPercentage"].ToString();
                            objSalesItem.SellingPrice = reader["SellingPrice"].ToString();
                            objSalesItem.GrossProfit = reader["Gross_Profit"].ToString();
                            objSalesItem.SaleValue = reader["SaleValue"].ToString();
                            objSalesItem.CouponIndicator = reader["Coupon_Indicator"].ToString();
                            lstSalesItem.Add(objSalesItem);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return lstSalesItem;
        }

        public int AddNewCustomerReqEntry(ref CustomerSalesReqEntity CustomerReqEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            string CustomerSalesReqNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (CustomerSalesReqItem CustomerReqItem in CustomerReqEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddBackOrder");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, CustomerReqEntity.BranchCode);
                        ImpalDB.AddInParameter(cmd, "@Req_Number", DbType.String, CustomerSalesReqNumber);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerReqEntity.CustomerCode.Trim());                        
                        ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, CustomerReqItem.SupplierCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int64, CustomerReqItem.SNO);
                        ImpalDB.AddInParameter(cmd, "@Supplier_PartNumber", DbType.String, CustomerReqItem.SupplierPartNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, CustomerReqItem.ItemCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Req_Quantity", DbType.Int64, CustomerReqItem.Quantity);
                        ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, CustomerReqItem.ListPrice);
                        ImpalDB.AddInParameter(cmd, "@Valid_Days", DbType.Int64, CustomerReqItem.ValidDays);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        CustomerSalesReqNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    CustomerReqEntity.CustomerSalesReqNumber = CustomerSalesReqNumber;
                    CustomerReqEntity.ErrorCode = "0";
                    CustomerReqEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public int AddEinvoiceDetails(string Branch_Code, string Document_Number, string Ack_No, string Ack_Date, string BDO_AckNo,
                                      string IRN_Number, string QR_Code, string Ewaybill_No, string Ewaybill_Date, string Remarks,
                                      string Signed_Invoice, string Signed_QRCode, byte[] imageData, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddEinvoiceDetails");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, Document_Number);
                    ImpalDB.AddInParameter(cmd, "@Ack_No", DbType.String, Ack_No);
                    ImpalDB.AddInParameter(cmd, "@Ack_Date", DbType.String, Ack_Date);
                    ImpalDB.AddInParameter(cmd, "@BDO_AckNo", DbType.String, BDO_AckNo);
                    ImpalDB.AddInParameter(cmd, "@IRN_Number", DbType.String, IRN_Number);
                    ImpalDB.AddInParameter(cmd, "@QR_Code", DbType.String, QR_Code);
                    ImpalDB.AddInParameter(cmd, "@Ewaybill_No", DbType.String, Ewaybill_No);
                    ImpalDB.AddInParameter(cmd, "@Ewaybill_Date", DbType.String, Ewaybill_Date);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, Remarks);
                    ImpalDB.AddInParameter(cmd, "@SignedInvoice", DbType.String, Signed_Invoice);
                    ImpalDB.AddInParameter(cmd, "@SignedQRCode", DbType.String, Signed_QRCode);
                    ImpalDB.AddInParameter(cmd, "@imageData", DbType.Binary, imageData);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public int AddEinvoiceIRNDetails(string Branch_Code, string Document_Number, string Ack_No, string Ack_Date, string BDO_AckNo,
                                         string IRN_Number, string QR_Code, string Ewaybill_No, string Ewaybill_Date, string Remarks,
                                         string Signed_Invoice, string Signed_QRCode, byte[] imageData, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddEinvoiceDetails");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, Document_Number);
                    ImpalDB.AddInParameter(cmd, "@Ack_No", DbType.String, Ack_No);
                    ImpalDB.AddInParameter(cmd, "@Ack_Date", DbType.String, Ack_Date);
                    ImpalDB.AddInParameter(cmd, "@BDO_AckNo", DbType.String, BDO_AckNo);
                    ImpalDB.AddInParameter(cmd, "@IRN_Number", DbType.String, IRN_Number);
                    ImpalDB.AddInParameter(cmd, "@QR_Code", DbType.String, QR_Code);
                    ImpalDB.AddInParameter(cmd, "@Ewaybill_No", DbType.String, Ewaybill_No);
                    ImpalDB.AddInParameter(cmd, "@Ewaybill_Date", DbType.String, Ewaybill_Date);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, Remarks);
                    ImpalDB.AddInParameter(cmd, "@SignedInvoice", DbType.String, Signed_Invoice);
                    ImpalDB.AddInParameter(cmd, "@SignedQRCode", DbType.String, Signed_QRCode);
                    ImpalDB.AddInParameter(cmd, "@imageData", DbType.Binary, imageData);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public int updEinvoiceIRNDetails(string Branch_Code, string Document_Number, byte[] imageData)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updEinvoiceDetails");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, Document_Number);
                    ImpalDB.AddInParameter(cmd, "@imageData", DbType.Binary, imageData);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public int AddEinvoiceCancelDetails(string Branch_Code, string Document_Number, string IRN_Number, string Cancel_Date)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {

                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddEinvoiceDetails_Cancel");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, Document_Number);
                    ImpalDB.AddInParameter(cmd, "@IRN_Number", DbType.String, IRN_Number);
                    ImpalDB.AddInParameter(cmd, "@Cancel_Date", DbType.String, Cancel_Date);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return result;
        }

        public decimal GetItemRate(string strItemCode)
        {
            decimal rate = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = " select isnull(rate,0) from Item_Master WITH (NOLOCK) where Item_Code = '" + strItemCode + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            rate = Convert.ToDecimal(reader[0]);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return rate;
        }

        public DataSet GetSalesJournalExcelDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_SalesJournalGST_Details");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetSalesTaxListingExcelDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {

                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_SalesTaxListingGST_Details");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetSalesTaxGSTReportDetails(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_SalesTaxGST_Reports");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetMissingQRCodeDocs(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetMissingQRCodes");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetGSTInwardRegisterDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetInwardRegister_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetGSTOutwardRegisterDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetOutwardRegister_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetGSTInwardRegisterDetailsHO(string zone, string state, string branch, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetInwardRegister_Report_HO");
                    ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, zone.Trim());
                    ImpalDB.AddInParameter(cmd, "@State", DbType.String, state.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, branch.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetGSTOutwardRegisterDetailsHO(string zone, string state, string branch, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetOutwardRegister_Report_HO");
                    ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, zone.Trim());
                    ImpalDB.AddInParameter(cmd, "@State", DbType.String, state.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, branch.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetOrderingReportDetails(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetOrdering_Reports_HO");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }
            return ds;
        }

        public DataSet GetInventoryReportDetails(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetInventory_Reports_HO");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetOutstandingReportDetails(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetOutstanding_Reports_HO");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1);
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2);
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

		public DataSet GetChequeReturnCollectionDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetChequeReturnCollection_Report_HO");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1);
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetClassifiedOutstandingReportHO(string ReportType, string zone, string state, string Branch, string date)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_rpt_ClassifiedOS_HO");
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    ImpalDB.AddInParameter(cmd, "@ZoneCode", DbType.String, zone);
                    ImpalDB.AddInParameter(cmd, "@StateCode", DbType.String, state);
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, Branch);
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, date);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }
        public DataSet GetMiscellaneousReportDetails(string BranchCode, string SupplierCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetMiscellaneous_Reports");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetMiscellaneousReportDetailsHO(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetMiscellaneous_Reports_HO");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetDealerCountDetailsReport(string BranchCode, string Date1, string Date2, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDealerCountDetails_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetCreditLimitHistoryDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCreditLimit_History_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetStockLedgerReport(string BranchCode, string SupplierCode, string SupplierPartNumber, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetStockLedger_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierPartNumber", DbType.String, SupplierPartNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetNewSegmentSalesReport(string BranchCode, string Date1, string Date2, string Segment, string SupplierCode, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetNewSegment_Sales_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    ImpalDB.AddInParameter(cmd, "@Segment", DbType.String, Segment.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetNewTBReportDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_NewTB_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetSalesTaxSummaryExcelDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_SalesTaxSummaryGST_Details");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetPurchaseJournalExcelDetails(string BranchCode, string Date1, string Date2)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_PurchaseJournalGST_Details");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, Date1.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, Date2.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetSalesManDetails(string BranchCode, string Date)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_salesman_report_KRA");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@date", DbType.String, Date.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetGRNEditListDetails(string BranchCode, string SupplierCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            string sSQL = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    sSQL = "Select Branch_Code,Branch_Name,Supplier_Code,Supplier_Name,Inward_Number GRN_Number,Convert(Varchar(10), Date_Opened, 103) GRN_Date,Invoice_Number, Convert(Varchar(10), Invoice_Date, 103) Invoice_Date, Inward_Value Invoice_Value from V_GrnListing WITH (NOLOCK) where convert(date,date_opened,103) between convert(date,'" + FromDate + "',103) and convert(date,'" + ToDate + "',103) ";

                    if (BranchCode.ToUpper() != "CRP")
                    {
                        sSQL = sSQL + "and Branch_Code = '" + BranchCode + "' ";
                    }

                    if (SupplierCode != "0")
                    {
                        sSQL = sSQL + "and Supplier_Code = '" + SupplierCode + "' ";
                    }

                    sSQL = sSQL + "Order By Branch_Code, Supplier_Code, Date_Opened";

                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetCancelledBillsDetails(string BranchCode, string FromDate, string ToDate, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCancelledBills");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetEWayBillReport(string BranchCode, string FromDate, string ToDate, string InvoiceType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_EWaybill_Report");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@InvoiceType", DbType.String, InvoiceType.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetPaymentPatternDetails(string BranchCode, int AccPeriod, string CustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_PaymentPattern_Details");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@AccPeriod", DbType.Int32, AccPeriod);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.Int32, CustomerCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetPendingDocsWeeklyPlanDetails(string BranchCode, string Date, string FromCustCode, string ToCustCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_PendingDoc_Weekly_Plan_Details");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date", DbType.String, Date);
                    ImpalDB.AddInParameter(cmd, "@From_CustCode", DbType.Int32, FromCustCode);
                    ImpalDB.AddInParameter(cmd, "@To_CustCode", DbType.Int32, ToCustCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetMiniWorkSheetDetails(string BranchCode, string SupplierCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Mini_WorkSheet_Report");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetTargetLineSales(string BranchCode, string Date)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_TargetLineSales");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date", DbType.String, Date.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesEntity), exp);
            }

            return ds;
        }

        public DataSet GetTargetTownSales(string BranchCode, string Date)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_TargetTownSales");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date", DbType.String, Date.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public DataSet GetMonthlyVisionDetails(string BranchCode, string Date)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_vision_for_branch");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date", DbType.String, Date.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }
            return ds;
        }

        public DataSet GetSalesManDealerDetails(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_salesman_dealer_report");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@fromdate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@todate", DbType.String, ToDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }
            return ds;
        }

        public DataSet GetSalesInvCheckDetails(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_inv_valcheck");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@fromdate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@todate", DbType.String, ToDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public DataSet GetGRNValueCheckDetails(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_inw_valcheck");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@fromdate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@todate", DbType.String, ToDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public string GetBranchName(string strBranchCode)
        {
            string strBranchName = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "select branch_name from branch_master where branch_code = '" + strBranchCode + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            strBranchName = reader[0].ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return strBranchName;
        }

        public string GetOsLsTaxIndicator(string strBranchCode)
        {
            string strOsLsTaxIndicator = string.Empty;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = "select isnull(Telex,0) from branch_master WITH (NOLOCK) where branch_code = '" + strBranchCode + "'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            strOsLsTaxIndicator = reader[0].ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return strOsLsTaxIndicator;
        }

        public DataSet GetSalesManDealerHeaderDetails(string strBranchCode, string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();                    
                    DbCommand cmd = ImpalDB.GetSqlStringCommand("select distinct(Sales_Man_Name) from v_Salesman_dealer WITH (NOLOCK) where branch_code='" + strBranchCode + "' and sales_man_name <> 'Others' and convert(date,document_date,101) between convert(date,convert(date,'" + FromDate + "',103),101) and convert(date,convert(date,'" + ToDate + "',103),101) order by sales_man_name");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public DataSet GetSegmentSalesMatrixDetails(string BranchCode, string FromDate, string ToDate)
        {

            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_SegmentSales_Matrix_Report");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public DataSet GetSurplusDetails(string ZoneCode, string StateCode, string BranchCode, string SupplierCode, string SupplierPartNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_report_zonebranchsurplus");
                    ImpalDB.AddInParameter(cmd, "@ZoneCode", DbType.String, ZoneCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@StateCode", DbType.String, StateCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierPartNo.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }

        public DataSet GetSupplierDespatchDetails(string BranchCode, string SupplierCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Upd_SupplierDespatchDetails");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return ds;
        }
    }
}