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


namespace IMPALLibrary.Transactions
{
    public class InwardEntity
    {
        public string InwardNumber { get; set; }
        public string InwardDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string SupplierCode { get; set; }
        public string BranchCode { get; set; }

        public string DCNumber { get; set; }
        public string DCDate { get; set; }

        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public string Carrier { get; set; }
        public string PlaceOfDespatch { get; set; }
        public string Weight { get; set; }
        public string NoOfCases { get; set; }
        public string RoadPermitNumber { get; set; }
        public string RoadPermitDate { get; set; }

        public string FreightIndicatorCode { get; set; }
        public string FreightAmount { get; set; }
        public string FreightTax { get; set; }
        public string Insurance { get; set; }
        public string PostalCharges { get; set; }
        public string CouponCharges { get; set; }

        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string ReceivedDate { get; set; }

        public string TaxValue { get; set; }
        public string TaxValue1 { get; set; }

        public string SupplierGSTIN { get; set; }
        public string SGSTValue { get; set; }
        public string CGSTValue { get; set; }
        public string IGSTValue { get; set; }
        public string UTGSTValue { get; set; }
        public string InvoiceValue { get; set; }
        public string TCSValue { get; set; }
        public string SupplyPlantCode { get; set; }
        public string SuppliersCount { get; set; }
        public string OSIndicator { get; set; }
        public string ReasonForReturn { get; set; }
        public string Status { get; set; }
        public string ExcessShortageStatus { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string PO_Number { get; set; }
        public string PO_Date { get; set; }
        public string PoPendingApprovalStatus { get; set; }

        public List<InwardItem> Items { get; set; }

        public List<InwardExcessItem> ExcessItems { get; set; }
    }

    public class InwardItem
    {
        public string TransactionTypeCode { get; set; }
        public string BrachCode { get; set; }
        public string InvoiceDate { get; set; }
        public string SNO { get; set; }
        public string CCWHNO { get; set; }
        public string ItemCode { get; set; }
        public string SerialNo { get; set; }
        public string PONumber { get; set; }
        public string POQuantity { get; set; }
        public string ReceivedQuantity { get; set; }
        public string Quantity { get; set; }
        public string BalanceQuantity { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ListPrice { get; set; }
        public string CostPricePerQty { get; set; }
        public string CostPrice { get; set; }
        public string MRP { get; set; }
        public string PurchaseDiscount { get; set; }
        public string Coupon { get; set; }
        public string ListLessDiscount { get; set; }
        public string EDIndicator { get; set; }
        public string EDValue { get; set; }
        public string ItemLocation { get; set; }
        public string ItemTaxPercentage { get; set; }
        public string ItemTaxValue { get; set; }
        public string OSLSIndicator { get; set; }
        public string HSN { get; set; }
        public string ItemDescription { get; set; }
    }

    public class InwardExcessItem
    {
        public string TransactionTypeCode { get; set; }
        public string BrachCode { get; set; }
        public string InvoiceDate { get; set; }
        public string SNO { get; set; }
        public string CCWHNO { get; set; }
        public string ItemCode { get; set; }
        public string SerialNo { get; set; }
        public string PONumber { get; set; }
        public string POQuantity { get; set; }
        public string ReceivedQuantity { get; set; }
        public string Quantity { get; set; }
        public string BalanceQuantity { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ListPrice { get; set; }
        public string CostPricePerQty { get; set; }
        public string CostPrice { get; set; }
        public string MRP { get; set; }
        public string PurchaseDiscount { get; set; }
        public string Coupon { get; set; }
        public string ListLessDiscount { get; set; }
        public string EDIndicator { get; set; }
        public string EDValue { get; set; }
        public string ItemLocation { get; set; }
        public string ItemTaxPercentage { get; set; }
        public string ItemTaxValue { get; set; }
        public string OSLSIndicator { get; set; }
        public string HSN { get; set; }
    }

    public class CCWH
    {
        public string CCWHNumber { get; set; }
        public string PONumber { get; set; }
    }

    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
    }

    public class InwardTransactions
    {
        public int AddNewInwardEntry(ref InwardEntity inwardEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string inwardNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addinwardentry1_GST");
                    ImpalDB.AddInParameter(cmd, "@Inward_Date", DbType.String, inwardEntity.InwardDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, inwardEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, inwardEntity.SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, inwardEntity.DCNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, inwardEntity.DCDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, inwardEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, inwardEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, inwardEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Place_Of_Despatch", DbType.String, inwardEntity.PlaceOfDespatch.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.Weight.Trim()))
                        inwardEntity.Weight = "0";

                    ImpalDB.AddInParameter(cmd, "@Consignment_Weight", DbType.Decimal, Convert.ToDecimal(inwardEntity.Weight.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.NoOfCases.Trim()))
                        inwardEntity.NoOfCases = "0";

                    ImpalDB.AddInParameter(cmd, "@Number_Of_Cases", DbType.Int32, Convert.ToInt64(inwardEntity.NoOfCases.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, inwardEntity.RoadPermitNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, inwardEntity.RoadPermitDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.FreightAmount.Trim()))
                        inwardEntity.FreightAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.FreightAmount.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightTax.Trim()))
                        inwardEntity.FreightTax = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Tax", DbType.String, Convert.ToDecimal(inwardEntity.FreightTax.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.Insurance.Trim()))
                        inwardEntity.Insurance = "0";
                    ImpalDB.AddInParameter(cmd, "@Insurance_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.Insurance.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.PostalCharges.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Postal_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.PostalCharges.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.CouponCharges.Trim()))
                        inwardEntity.CouponCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Coupon_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.CouponCharges.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, inwardEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, inwardEntity.InvoiceDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date_Received", DbType.String, inwardEntity.ReceivedDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.InvoiceValue.Trim()))
                        inwardEntity.InvoiceValue = "0";
                    ImpalDB.AddInParameter(cmd, "@Inward_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.InvoiceValue.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, inwardEntity.Status);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, null);
                    ImpalDB.AddInParameter(cmd, "@depot_code", DbType.String, inwardEntity.SupplyPlantCode.Trim());

                    if (inwardEntity.OSIndicator.Trim() == "L")
                    {
                        if (string.IsNullOrEmpty(inwardEntity.SGSTValue.Trim()))
                            inwardEntity.SGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.UTGSTValue.Trim()))
                            inwardEntity.UTGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.CGSTValue.Trim()))
                            inwardEntity.CGSTValue = "0";

                        if (inwardEntity.BranchCode.ToUpper() == "CHG")
                            inwardEntity.TaxValue = inwardEntity.UTGSTValue;
                        else
                            inwardEntity.TaxValue = inwardEntity.SGSTValue;

                        inwardEntity.TaxValue1 = inwardEntity.CGSTValue;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inwardEntity.IGSTValue.Trim()))
                            inwardEntity.IGSTValue = "0";

                        inwardEntity.TaxValue = inwardEntity.IGSTValue;
                        inwardEntity.TaxValue1 = "0";
                    }

                    ImpalDB.AddInParameter(cmd, "@Local_Purchase", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue.Trim()));
                    ImpalDB.AddInParameter(cmd, "@Local_Purchase1", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue1.Trim()));
                    ImpalDB.AddInParameter(cmd, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@TCSValue", DbType.Decimal, Convert.ToDecimal(inwardEntity.TCSValue.Trim()));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    inwardNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    DbCommand cmdItems;

                    foreach (InwardItem inwardItem in inwardEntity.Items)
                    {
                        cmdItems = ImpalDB.GetStoredProcCommand("usp_addinwardentry2_GST");
                        ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Inward_Number", DbType.String, inwardNumber);
                        ImpalDB.AddInParameter(cmdItems, "@Serial_Number", DbType.String, inwardItem.SNO.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@PO_Number", DbType.String, inwardItem.PONumber);
                        ImpalDB.AddInParameter(cmdItems, "@Item_Code", DbType.String, inwardItem.ItemCode);//+ inwardItem.ItemCode);
                        ImpalDB.AddInParameter(cmdItems, "@location_code", DbType.String, inwardItem.ItemLocation);

                        if (string.IsNullOrEmpty(inwardItem.Quantity.Trim()))
                            inwardItem.Quantity = "0";

                        ImpalDB.AddInParameter(cmdItems, "@Document_Quantity", DbType.Decimal, Convert.ToDecimal(inwardItem.Quantity));
                        ImpalDB.AddInParameter(cmdItems, "@PO_SerialNo", DbType.String, inwardItem.SerialNo.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Coupon", DbType.String, inwardItem.Coupon.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Tax_Percentage", DbType.String, inwardItem.ItemTaxPercentage.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                        cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItems);
                        cmdItems = null;
                    }

                    DbCommand cmdPO = ImpalDB.GetStoredProcCommand("Usp_InwardQuantity_Status");
                    ImpalDB.AddInParameter(cmdPO, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmdPO, "@Inward_Number", DbType.String, inwardNumber);
                    cmdPO.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdPO);

                    inwardEntity.InwardNumber = inwardNumber;
                    inwardEntity.ErrorCode = "0";
                    inwardEntity.ErrorMsg = "";
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

        public int AddNewAutoInwardEntry(ref InwardEntity inwardEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string inwardNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addinwardentry1_GST");
                    ImpalDB.AddInParameter(cmd, "@Inward_Date", DbType.String, inwardEntity.InwardDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, inwardEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, inwardEntity.SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, inwardEntity.DCNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, inwardEntity.DCDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, inwardEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, inwardEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, inwardEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Place_Of_Despatch", DbType.String, inwardEntity.PlaceOfDespatch.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.Weight.Trim()))
                        inwardEntity.Weight = "0";

                    ImpalDB.AddInParameter(cmd, "@Consignment_Weight", DbType.Decimal, Convert.ToDecimal(inwardEntity.Weight.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.NoOfCases.Trim()))
                        inwardEntity.NoOfCases = "0";

                    ImpalDB.AddInParameter(cmd, "@Number_Of_Cases", DbType.Int32, Convert.ToInt64(inwardEntity.NoOfCases.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, inwardEntity.RoadPermitNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, inwardEntity.RoadPermitDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.FreightAmount.Trim()))
                        inwardEntity.FreightAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.FreightAmount.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightTax.Trim()))
                        inwardEntity.FreightTax = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Tax", DbType.String, Convert.ToDecimal(inwardEntity.FreightTax.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.Insurance.Trim()))
                        inwardEntity.Insurance = "0";
                    ImpalDB.AddInParameter(cmd, "@Insurance_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.Insurance.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.PostalCharges.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Postal_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.PostalCharges.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.CouponCharges.Trim()))
                        inwardEntity.CouponCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Coupon_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.CouponCharges.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, inwardEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, inwardEntity.InvoiceDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Date_Received", DbType.String, inwardEntity.ReceivedDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.InvoiceValue.Trim()))
                        inwardEntity.InvoiceValue = "0";
                    ImpalDB.AddInParameter(cmd, "@Inward_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.InvoiceValue.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, inwardEntity.Status);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, "AUTO GRN");
                    ImpalDB.AddInParameter(cmd, "@depot_code", DbType.String, inwardEntity.SupplyPlantCode.Trim());

                    if (inwardEntity.OSIndicator.Trim() == "L")
                    {
                        if (string.IsNullOrEmpty(inwardEntity.SGSTValue.Trim()))
                            inwardEntity.SGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.UTGSTValue.Trim()))
                            inwardEntity.UTGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.CGSTValue.Trim()))
                            inwardEntity.CGSTValue = "0";

                        if (inwardEntity.BranchCode.ToUpper() == "CHG")
                            inwardEntity.TaxValue = inwardEntity.UTGSTValue;
                        else
                            inwardEntity.TaxValue = inwardEntity.SGSTValue;

                        inwardEntity.TaxValue1 = inwardEntity.CGSTValue;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inwardEntity.IGSTValue.Trim()))
                            inwardEntity.IGSTValue = "0";

                        inwardEntity.TaxValue = inwardEntity.IGSTValue;
                        inwardEntity.TaxValue1 = "0";
                    }

                    ImpalDB.AddInParameter(cmd, "@Local_Purchase", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue.Trim()));
                    ImpalDB.AddInParameter(cmd, "@Local_Purchase1", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue1.Trim()));
                    ImpalDB.AddInParameter(cmd, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@TCSValue", DbType.Decimal, Convert.ToDecimal(inwardEntity.TCSValue.Trim()));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    inwardNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    DbCommand cmdItems;

                    foreach (InwardItem inwardItem in inwardEntity.Items)
                    {
                        cmdItems = ImpalDB.GetStoredProcCommand("usp_addinwardentry2_AutoGRN");
                        ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Inward_Number", DbType.String, inwardNumber);
                        ImpalDB.AddInParameter(cmdItems, "@Serial_Number", DbType.String, inwardItem.SNO.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@PO_Number", DbType.String, inwardItem.PONumber);
                        ImpalDB.AddInParameter(cmdItems, "@Item_Code", DbType.String, inwardItem.ItemCode);//+ inwardItem.ItemCode);
                        ImpalDB.AddInParameter(cmdItems, "@location_code", DbType.String, inwardItem.ItemLocation);

                        if (string.IsNullOrEmpty(inwardItem.Quantity.Trim()))
                            inwardItem.Quantity = "0";

                        ImpalDB.AddInParameter(cmdItems, "@Document_Quantity", DbType.Decimal, Convert.ToDecimal(inwardItem.Quantity));
                        ImpalDB.AddInParameter(cmdItems, "@PO_SerialNo", DbType.String, inwardItem.SerialNo.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Sup_ln_Cod", DbType.String, inwardItem.ItemCode.Substring(0, 6));
                        ImpalDB.AddInParameter(cmdItems, "@item_price", DbType.String, inwardItem.ListPrice.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@cost_price", DbType.String, inwardItem.CostPricePerQty.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@MRP", DbType.String, inwardItem.MRP.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Coupon", DbType.String, inwardItem.Coupon.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Tax_Percentage", DbType.String, inwardItem.ItemTaxPercentage.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@HSN_Code", DbType.String, inwardItem.HSN.Trim());
                        cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItems);
                        cmdItems = null;                                                
                    }

                    DbCommand cmdPOStatus = ImpalDB.GetStoredProcCommand("Usp_InwardQuantity_Status");
                    ImpalDB.AddInParameter(cmdPOStatus, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmdPOStatus, "@Inward_Number", DbType.String, inwardNumber);
                    cmdPOStatus.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdPOStatus);

                    inwardEntity.InwardNumber = inwardNumber;
                    inwardEntity.ErrorCode = "0";
                    inwardEntity.ErrorMsg = "";
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

        public int AddNewPOforAutoInwardEntry(ref InwardEntity inwardEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            DbCommand cmdPO;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    string strOutPO_Number = "";
                    string strOutPO_Date = "";

                    foreach (InwardExcessItem inwardItem in inwardEntity.ExcessItems)
                    {
                        int BalQty = (-1) * Convert.ToInt32(inwardItem.BalanceQuantity);

                        cmdPO = ImpalDB.GetStoredProcCommand("usp_AddDirectPurchaseOrder_AutoGRN");
                        ImpalDB.AddInParameter(cmdPO, "@PO_Number", DbType.String, strOutPO_Number.ToString());
                        ImpalDB.AddInParameter(cmdPO, "@Transaction_Type_Code", DbType.String, inwardEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmdPO, "@Branch_Code", DbType.String, inwardEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdPO, "@Supplier_Code", DbType.String, inwardEntity.SupplierCode);
                        ImpalDB.AddInParameter(cmdPO, "@Reference_Number", DbType.String, "SUPPLIMENTARY ORDER FOR AUTO GRN");
                        ImpalDB.AddInParameter(cmdPO, "@Reference_Date", DbType.String, inwardEntity.InwardDate);
                        ImpalDB.AddInParameter(cmdPO, "@Road_Permit_Number", DbType.String, inwardEntity.RoadPermitNumber);
                        ImpalDB.AddInParameter(cmdPO, "@Road_Permit_Date", DbType.String, inwardEntity.RoadPermitDate);
                        ImpalDB.AddInParameter(cmdPO, "@Carrier", DbType.String, inwardEntity.Carrier);
                        ImpalDB.AddInParameter(cmdPO, "@Destination", DbType.String, inwardEntity.PlaceOfDespatch);
                        ImpalDB.AddInParameter(cmdPO, "@Invoice_Number", DbType.String, inwardEntity.InvoiceNumber);
                        ImpalDB.AddInParameter(cmdPO, "@Serial_Number", DbType.Int32, 0);
                        ImpalDB.AddInParameter(cmdPO, "@Item_Code", DbType.String, inwardItem.ItemCode.ToString());
                        ImpalDB.AddInParameter(cmdPO, "@Item_PO_Quantity", DbType.Int32, BalQty);
                        ImpalDB.AddInParameter(cmdPO, "@Item_Status", DbType.String, "A");
                        ImpalDB.AddInParameter(cmdPO, "@Schedule_PO_Quantity", DbType.Int32, BalQty);
                        ImpalDB.AddInParameter(cmdPO, "@Schedule_Date", DbType.String, DateTime.Now.ToString("MM/yyyy").ToString());
                        ImpalDB.AddInParameter(cmdPO, "@Valid_Days", DbType.Int32, 0);
                        ImpalDB.AddInParameter(cmdPO, "@Indent_Branch", DbType.String, inwardEntity.BranchCode);
                        ImpalDB.AddInParameter(cmdPO, "@Schedule_Status", DbType.String, "A");
                        ImpalDB.AddInParameter(cmdPO, "@Supplier_Line_Code", DbType.String, inwardItem.ItemCode.Substring(0, 6));
                        ImpalDB.AddInParameter(cmdPO, "@OrderStatus", DbType.String, "A");
                        ImpalDB.AddInParameter(cmdPO, "@customercode", DbType.String, "");
                        cmdPO.CommandTimeout = ConnectionTimeOut.TimeOut;
                        using (IDataReader reader = ImpalDB.ExecuteReader(cmdPO))
                        {
                            while (reader.Read())
                            {
                                strOutPO_Number = reader[0].ToString();
                                strOutPO_Date = reader[2].ToString();
                            }
                        }

                        cmdPO = null;
                    }

                    inwardEntity.PO_Number = strOutPO_Number;
                    inwardEntity.PO_Date = strOutPO_Date;
                    inwardEntity.ErrorCode = "0";
                    inwardEntity.ErrorMsg = "";
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

        public int UpdateInwardEntry(ref InwardEntity inwardEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updinwardentry1_GST");
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, inwardEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, inwardEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, inwardEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, inwardEntity.DCNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, inwardEntity.DCDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, inwardEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, inwardEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, inwardEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Place_of_Despatch", DbType.String, inwardEntity.PlaceOfDespatch.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.Weight.Trim()))
                        inwardEntity.Weight = "0";
                    ImpalDB.AddInParameter(cmd, "@Consignment_Weight", DbType.Decimal, Convert.ToDecimal(inwardEntity.Weight.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightAmount.Trim()))
                        inwardEntity.FreightAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.FreightAmount.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightTax.Trim()))
                        inwardEntity.FreightTax = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Tax", DbType.String, Convert.ToDecimal(inwardEntity.FreightTax.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.Insurance.Trim()))
                        inwardEntity.Insurance = "0";
                    ImpalDB.AddInParameter(cmd, "@Insurance", DbType.Decimal, Convert.ToDecimal(inwardEntity.Insurance.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.PostalCharges.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Postal_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.PostalCharges.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.NoOfCases.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Number_of_Cases", DbType.Decimal, Convert.ToInt64(inwardEntity.NoOfCases.Trim()));


                    ImpalDB.AddInParameter(cmd, "@Date_Received", DbType.String, inwardEntity.ReceivedDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.InvoiceValue.Trim()))
                        inwardEntity.InvoiceValue = "0";
                    ImpalDB.AddInParameter(cmd, "@Inward_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.InvoiceValue.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, inwardEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, inwardEntity.InvoiceDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, inwardEntity.RoadPermitNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, inwardEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, inwardEntity.Status);

                    if (string.IsNullOrEmpty(inwardEntity.CouponCharges.Trim()))
                        inwardEntity.CouponCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Coupon_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.CouponCharges.Trim()));

                    ImpalDB.AddInParameter(cmd, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@Depot_Code", DbType.String, inwardEntity.SupplyPlantCode.Trim());

                    if (inwardEntity.OSIndicator.Trim() == "L")
                    {
                        if (string.IsNullOrEmpty(inwardEntity.SGSTValue.Trim()))
                            inwardEntity.SGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.UTGSTValue.Trim()))
                            inwardEntity.UTGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.CGSTValue.Trim()))
                            inwardEntity.CGSTValue = "0";

                        if (inwardEntity.BranchCode.ToUpper() == "CHG")
                            inwardEntity.TaxValue = inwardEntity.UTGSTValue;
                        else
                            inwardEntity.TaxValue = inwardEntity.SGSTValue;

                        inwardEntity.TaxValue1 = inwardEntity.CGSTValue;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inwardEntity.IGSTValue.Trim()))
                            inwardEntity.IGSTValue = "0";

                        inwardEntity.TaxValue = inwardEntity.IGSTValue;
                        inwardEntity.TaxValue1 = "0";
                    }

                    ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue.Trim()));
                    ImpalDB.AddInParameter(cmd, "@Tax_Value1", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue1.Trim()));
                    ImpalDB.AddInParameter(cmd, "@TCSValue", DbType.Decimal, Convert.ToDecimal(inwardEntity.TCSValue.Trim()));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    foreach (InwardItem inwardItem in inwardEntity.Items)
                    {
                        DbCommand cmdUpdateInward = ImpalDB.GetStoredProcCommand("usp_Updinwardentry2_GST");
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Inward_Number", DbType.String, inwardEntity.InwardNumber.Trim());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Serial_Number", DbType.String, inwardItem.SNO.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@PO_Number", DbType.String, inwardItem.PONumber);
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Item_Code", DbType.String, inwardItem.ItemCode.ToString());//+ inwardItem.ItemCode);
                        ImpalDB.AddInParameter(cmdUpdateInward, "@location_code", DbType.String, inwardItem.ItemLocation);

                        if (string.IsNullOrEmpty(inwardItem.Quantity.Trim()))
                            inwardItem.Quantity = "0";

                        ImpalDB.AddInParameter(cmdUpdateInward, "@Document_Quantity", DbType.Decimal, Convert.ToDecimal(inwardItem.Quantity));
                        ImpalDB.AddInParameter(cmdUpdateInward, "@PO_SerialNo", DbType.String, inwardItem.SerialNo.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Coupon", DbType.String, inwardItem.Coupon.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Tax_Percentage", DbType.String, inwardItem.ItemTaxPercentage.ToString());                        
                        cmdUpdateInward.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdUpdateInward);
                    }

                    DbCommand cmdPOStatus = ImpalDB.GetStoredProcCommand("Usp_InwardQuantity_Status");
                    ImpalDB.AddInParameter(cmdPOStatus, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmdPOStatus, "@Inward_Number", DbType.String, inwardEntity.InwardNumber.Trim());
                    cmdPOStatus.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdPOStatus);

                    //inwardEntity.InwardNumber = InwardNumber;
                    inwardEntity.ErrorCode = "0";
                    inwardEntity.ErrorMsg = "";
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

        public int UpdateAutoInwardEntry(ref InwardEntity inwardEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updinwardentry1_GST");
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, inwardEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, inwardEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, inwardEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, inwardEntity.DCNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, inwardEntity.DCDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, inwardEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, inwardEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, inwardEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Place_of_Despatch", DbType.String, inwardEntity.PlaceOfDespatch.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.Weight.Trim()))
                        inwardEntity.Weight = "0";
                    ImpalDB.AddInParameter(cmd, "@Consignment_Weight", DbType.Decimal, Convert.ToDecimal(inwardEntity.Weight.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightAmount.Trim()))
                        inwardEntity.FreightAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Amount", DbType.Decimal, Convert.ToDecimal(inwardEntity.FreightAmount.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.FreightTax.Trim()))
                        inwardEntity.FreightTax = "0";
                    ImpalDB.AddInParameter(cmd, "@Freight_Tax", DbType.String, Convert.ToDecimal(inwardEntity.FreightTax.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.Insurance.Trim()))
                        inwardEntity.Insurance = "0";
                    ImpalDB.AddInParameter(cmd, "@Insurance", DbType.Decimal, Convert.ToDecimal(inwardEntity.Insurance.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.PostalCharges.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Postal_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.PostalCharges.Trim()));

                    if (string.IsNullOrEmpty(inwardEntity.NoOfCases.Trim()))
                        inwardEntity.PostalCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Number_of_Cases", DbType.Decimal, Convert.ToInt64(inwardEntity.NoOfCases.Trim()));


                    ImpalDB.AddInParameter(cmd, "@Date_Received", DbType.String, inwardEntity.ReceivedDate.Trim());

                    if (string.IsNullOrEmpty(inwardEntity.InvoiceValue.Trim()))
                        inwardEntity.InvoiceValue = "0";
                    ImpalDB.AddInParameter(cmd, "@Inward_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.InvoiceValue.Trim()));

                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, inwardEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, inwardEntity.InvoiceDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, inwardEntity.RoadPermitNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, inwardEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, inwardEntity.Status);

                    if (string.IsNullOrEmpty(inwardEntity.CouponCharges.Trim()))
                        inwardEntity.CouponCharges = "0";
                    ImpalDB.AddInParameter(cmd, "@Coupon_Charges", DbType.Decimal, Convert.ToDecimal(inwardEntity.CouponCharges.Trim()));

                    ImpalDB.AddInParameter(cmd, "@OSLS", DbType.String, inwardEntity.OSIndicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@Depot_Code", DbType.String, inwardEntity.SupplyPlantCode.Trim());

                    if (inwardEntity.OSIndicator.Trim() == "L")
                    {
                        if (string.IsNullOrEmpty(inwardEntity.SGSTValue.Trim()))
                            inwardEntity.SGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.UTGSTValue.Trim()))
                            inwardEntity.UTGSTValue = "0";
                        if (string.IsNullOrEmpty(inwardEntity.CGSTValue.Trim()))
                            inwardEntity.CGSTValue = "0";

                        if (inwardEntity.BranchCode.ToUpper() == "CHG")
                            inwardEntity.TaxValue = inwardEntity.UTGSTValue;
                        else
                            inwardEntity.TaxValue = inwardEntity.SGSTValue;

                        inwardEntity.TaxValue1 = inwardEntity.CGSTValue;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inwardEntity.IGSTValue.Trim()))
                            inwardEntity.IGSTValue = "0";

                        inwardEntity.TaxValue = inwardEntity.IGSTValue;
                        inwardEntity.TaxValue1 = "0";
                    }

                    ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue.Trim()));
                    ImpalDB.AddInParameter(cmd, "@Tax_Value1", DbType.Decimal, Convert.ToDecimal(inwardEntity.TaxValue1.Trim()));
                    ImpalDB.AddInParameter(cmd, "@TCSValue", DbType.Decimal, Convert.ToDecimal(inwardEntity.TCSValue.Trim()));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    foreach (InwardItem inwardItem in inwardEntity.Items)
                    {
                        DbCommand cmdUpdateInward = ImpalDB.GetStoredProcCommand("usp_Updinwardentry2_GST");
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Branch_Code", DbType.String, inwardEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Inward_Number", DbType.String, inwardEntity.InwardNumber.Trim());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Serial_Number", DbType.String, inwardItem.SNO.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@PO_Number", DbType.String, inwardItem.PONumber);
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Item_Code", DbType.String, inwardItem.ItemCode.ToString());//+ inwardItem.ItemCode);
                        ImpalDB.AddInParameter(cmdUpdateInward, "@location_code", DbType.String, inwardItem.ItemLocation);

                        if (string.IsNullOrEmpty(inwardItem.Quantity.Trim()))
                            inwardItem.Quantity = "0";

                        ImpalDB.AddInParameter(cmdUpdateInward, "@Document_Quantity", DbType.Decimal, Convert.ToDecimal(inwardItem.Quantity));
                        ImpalDB.AddInParameter(cmdUpdateInward, "@PO_SerialNo", DbType.String, inwardItem.SerialNo.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Coupon", DbType.String, inwardItem.Coupon.ToString());
                        ImpalDB.AddInParameter(cmdUpdateInward, "@Tax_Percentage", DbType.String, inwardItem.ItemTaxPercentage.ToString());
                        cmdUpdateInward.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdUpdateInward);
                    }

                    //inwardEntity.InwardNumber = InwardNumber;
                    inwardEntity.ErrorCode = "0";
                    inwardEntity.ErrorMsg = "";
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

        public string InwardItemsLocationEdit(string InwardNumber, string BranchCode)
        {
            string Itemcodes = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_inward_item_loc");
            ImpalDB.AddInParameter(cmd, "@InwardNumber", DbType.String, InwardNumber);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Itemcodes = ImpalDB.ExecuteScalar(cmd).ToString();

            return Itemcodes;
        }

        public string CheckNewItemsInward(string BranchCode, string SupplierCode, string InvoiceNumber)
        {
            string Itemcodes = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_inward_newitems");            
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, InvoiceNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Itemcodes = ImpalDB.ExecuteScalar(cmd).ToString();

            return Itemcodes;
        }

        public int InwardItemsLocation(string ItemCode, string BranchCode)
        {
            int Cnt = 0;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("IF EXISTS(select 1 from item_location WITH (NOLOCK) where branch_code='" + BranchCode + "' and item_code='" + ItemCode + "') Select 1 Else Select 0");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return Cnt;
        }

        public int CheckBranchItemPrice(string ItemCode, string BranchCode)
        {
            int Cnt = 0;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("IF EXISTS(select 1 from Branch_Item_Price WITH (NOLOCK) where branch_code='" + BranchCode + "' and item_code='" + ItemCode + "') Select 1 Else Select 0");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return Cnt;
        }

        public string CheckBranchItemPriceABCFMS(string SupplierCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("select STUFF((select ', ' + i.Supplier_Part_Number from Item_Worksheet_ABCFMS_Daily i WITH (NOLOCK) left outer join Branch_Item_Price b WITH (NOLOCK) on i.Item_Code=b.Item_Code " +
                            "and i.Branch_Code=b.Branch_Code where i.branch_code = '" + BranchCode + "' and i.Supplier_Code = '" + SupplierCode + "' and b.Item_Code is null order by ',' + i.Supplier_Part_Number for xml path('')),1,1,NULL)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            string Cnt = ImpalDB.ExecuteScalar(cmd).ToString();

            return Cnt;
        }

        public string CheckBranchItemPriceNILABCFMS(string SupplierCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("select STUFF((select ', ' + i.Supplier_Part_Number from Item_Worksheet_ABCFMS_Nil_Daily i WITH (NOLOCK) left outer join Branch_Item_Price b WITH (NOLOCK) on i.Item_Code=b.Item_Code " +
                            "and i.Branch_Code=b.Branch_Code where i.branch_code = '" + BranchCode + "' and i.Supplier_Code = '" + SupplierCode + "' and b.Item_Code is null order by ', ' + i.Supplier_Part_Number for xml path('')),1,1,NULL)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            string Cnt = ImpalDB.ExecuteScalar(cmd).ToString();

            return Cnt;
        }

        public string CheckBranchItemPriceABCFMSsegment(string SupplierCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("select STUFF((select ', ' + i.Supplier_Part_Number from Item_Worksheet_ABCFMS_Daily_Segment i WITH (NOLOCK) left outer join Branch_Item_Price b WITH (NOLOCK) on i.Item_Code=b.Item_Code " +
                            "and i.Branch_Code=b.Branch_Code where i.branch_code = '" + BranchCode + "' and i.Supplier_Code = '" + SupplierCode + "' and b.Item_Code is null order by ',' + i.Supplier_Part_Number for xml path('')),1,1,NULL)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            string Cnt = ImpalDB.ExecuteScalar(cmd).ToString();

            return Cnt;
        }

        public string CheckBranchItemPriceNILABCFMSsegment(string SupplierCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("select STUFF((select ', ' + i.Supplier_Part_Number from Item_Worksheet_ABCFMS_Nil_Daily i WITH (NOLOCK) left outer join Branch_Item_Price b WITH (NOLOCK) on i.Item_Code=b.Item_Code " +
                            "and i.Branch_Code=b.Branch_Code where i.branch_code = '" + BranchCode + "' and i.Supplier_Code = '" + SupplierCode + "' and b.Item_Code is null order by ', ' + i.Supplier_Part_Number for xml path('')),1,1,NULL)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            string Cnt = ImpalDB.ExecuteScalar(cmd).ToString();

            return Cnt;
        }

        public int CheckSecondSales(string SupplierCode, string strBranchCode)
        {
            int Cnt = 0;
            string sSQL="";
            Database ImpalDB = DataAccess.GetDatabase();

            sSQL = "select 1 from Product_Branch_Sales_Tax a WITH (NOLOCK) inner join Item_Master b WITH (NOLOCK)";
            sSQL = sSQL  + " on a.Product_Group_Code=b.Product_Group_Code and a.Status='A' and a.OS_LS_Indicator='L' and a.Sales_Tax_Text='Second Sales'";
            sSQL = sSQL  + " and a.Branch_Code = '" + strBranchCode + "' and substring(b.Item_Code,1,2)='" + SupplierCode + "'";
            
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return Cnt;
        }

        public string SupplyPlantInterStateStatus(string SupplierCode, string SupplyPlantCode, string BranchCode)
        {
            string Indicator = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select case when g.State_Code=b.State_Code then 'L' else 'O' end from Gst_Supplier_Master g WITH (NOLOCK) inner join Branch_Master b WITH (NOLOCK)";
            sSQL = sSQL + " on g.Supplier_Code='" + SupplierCode + "' and g.Gst_Supplier_Code='" + SupplyPlantCode + "' and b.Branch_Code='" + BranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Indicator = ImpalDB.ExecuteScalar(cmd).ToString();

            return Indicator;
        }

        public string GetFreightChargesStatus(string SupplierCode, string SupplyPlantCode, string BranchCode)
        {
            string Indicator = "0";
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Count(1) from Freight_Charges_Inward WITH (NOLOCK) where Supplier_Code='" + SupplierCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Indicator = ImpalDB.ExecuteScalar(cmd).ToString();

            return Indicator;
        }

        public InwardEntity GetInwardDetails(string InwardNumber, string BranchCode)
        {
            InwardEntity inwardEntity = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardEntryHeaderAndDetails_GST");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
			ImpalDB.AddInParameter(cmd, "@InwardNumber", DbType.String, InwardNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                inwardEntity = new InwardEntity();
                //Status
                inwardEntity.InwardNumber = string.Empty;//reader[0].ToString();
                inwardEntity.InwardDate = ds.Tables[0].Rows[0][3].ToString();
                inwardEntity.TransactionTypeCode = ds.Tables[0].Rows[0]["Transaction_Type_Code"].ToString();
                inwardEntity.SupplierCode = ds.Tables[0].Rows[0]["Supplier_Code"].ToString();
                inwardEntity.BranchCode = ds.Tables[0].Rows[0]["Branch_Code"].ToString();
                inwardEntity.SupplierGSTIN = ds.Tables[0].Rows[0]["GSTIN_Number"].ToString();
                inwardEntity.DCNumber = ds.Tables[0].Rows[0]["Document_Number"].ToString();
                inwardEntity.DCDate = ds.Tables[0].Rows[0][5].ToString();
                inwardEntity.LRNumber = ds.Tables[0].Rows[0]["LR_Number"].ToString();
                inwardEntity.LRDate = ds.Tables[0].Rows[0][7].ToString();
                inwardEntity.Carrier = ds.Tables[0].Rows[0]["Carrier"].ToString();
                inwardEntity.PlaceOfDespatch = ds.Tables[0].Rows[0]["Place_of_Despatch"].ToString();
                inwardEntity.Weight = TwoDecimalConversion(ds.Tables[0].Rows[0]["Consignment_Weight"].ToString());
                inwardEntity.NoOfCases = ds.Tables[0].Rows[0]["Number_of_Cases"].ToString();
                inwardEntity.RoadPermitNumber = ds.Tables[0].Rows[0]["Road_Permit_Number"].ToString();
                inwardEntity.RoadPermitDate = ds.Tables[0].Rows[0][21].ToString();
                inwardEntity.FreightAmount = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Amount"].ToString());
                inwardEntity.FreightTax = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Tax"].ToString());
                inwardEntity.Insurance = TwoDecimalConversion(ds.Tables[0].Rows[0]["Insurance_Amount"].ToString());
                inwardEntity.PostalCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Postal_Charges"].ToString());
                inwardEntity.CouponCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Coupon_Charges"].ToString());
                inwardEntity.InvoiceNumber = ds.Tables[0].Rows[0]["Invoice_Number"].ToString();
                inwardEntity.InvoiceDate = ds.Tables[0].Rows[0][19].ToString();
                inwardEntity.ReceivedDate = ds.Tables[0].Rows[0][16].ToString();                
                inwardEntity.InvoiceValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Inward_Value"].ToString());
                inwardEntity.SupplyPlantCode = ds.Tables[0].Rows[0]["Depot_code"].ToString();
                inwardEntity.OSIndicator = ds.Tables[0].Rows[0]["OS_LS_Indicator"].ToString();
                inwardEntity.TCSValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["TCS_Value"].ToString());

                if (ds.Tables[0].Rows[0]["OS_LS_Indicator"].ToString().ToUpper() == "L")
                {
                    if (ds.Tables[0].Rows[0]["Branch_Code"].ToString().ToUpper() == "CHG")
                    {
                        inwardEntity.SGSTValue = "0.00";
                        inwardEntity.UTGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());                        
                    }
                    else
                    {
                        inwardEntity.SGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());
                        inwardEntity.UTGSTValue = "0.00";
                    }

                    inwardEntity.CGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax1"].ToString());
                    inwardEntity.IGSTValue = "0.00";
                    
                }
                else
                {
                    inwardEntity.SGSTValue = "0.00";
                    inwardEntity.CGSTValue = "0.00";
                    inwardEntity.IGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());
                    inwardEntity.UTGSTValue = "0.00";
                }

                inwardEntity.ReasonForReturn = string.Empty;// reader[0].ToString();
                inwardEntity.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                inwardEntity.Items = new List<InwardItem>();

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    InwardItem inwardItem = new InwardItem();
                    inwardItem.SNO = dr["SNO"].ToString();
                    inwardItem.CCWHNO = dr["Reference_Document"].ToString();
                    inwardItem.ItemCode = dr["ItemCode"].ToString();
                    inwardItem.SerialNo = dr["Serial_Number"].ToString();
                    inwardItem.PONumber = dr["Reference_Document"].ToString();
                    inwardItem.POQuantity = dr["POQTY"].ToString();
                    inwardItem.ReceivedQuantity = dr["ReceivedQty"].ToString();
                    inwardItem.BalanceQuantity = dr["Bal_QTY"].ToString();
                    inwardItem.Quantity = dr["QTY"].ToString();
                    inwardItem.SupplierPartNumber = dr["SupPartNo"].ToString();
                    inwardItem.ListPrice = dr["ListPrice"].ToString();
                    inwardItem.CostPricePerQty = dr["CostPricePerQty"].ToString();
                    inwardItem.CostPrice = dr["CostPrice"].ToString();
                    inwardItem.Coupon = dr["Coupon"].ToString();
                    inwardItem.ListLessDiscount = dr["ListLessDiscount"].ToString();
                    inwardItem.ItemLocation = dr["ItemLocation"].ToString();
                    inwardItem.ItemTaxPercentage = dr["TaxPercentage"].ToString();
                    inwardItem.ItemTaxValue = dr["TaxValue"].ToString();
                    inwardEntity.Items.Add(inwardItem);
                }
            }

            return inwardEntity;
        }

        public InwardEntity GetInwardDetailsAutoGRN(string BranchCode, string SupplierCode, string InvoiceNumber)
        {
            InwardEntity inwardEntity = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardEntryHeaderAndDetails_AutoGRN");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, InvoiceNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                inwardEntity = new InwardEntity();
                //Status
                inwardEntity.InwardNumber = string.Empty;//reader[0].ToString();
                inwardEntity.InwardDate = ds.Tables[0].Rows[0][3].ToString();
                inwardEntity.TransactionTypeCode = ds.Tables[0].Rows[0]["Transaction_Type_Code"].ToString();
                inwardEntity.SupplierCode = ds.Tables[0].Rows[0]["Supplier_Code"].ToString();
                inwardEntity.BranchCode = ds.Tables[0].Rows[0]["Branch_Code"].ToString();
                inwardEntity.DCNumber = ds.Tables[0].Rows[0]["Document_Number"].ToString();
                inwardEntity.DCDate = ds.Tables[0].Rows[0][5].ToString();
                inwardEntity.LRNumber = ds.Tables[0].Rows[0]["LR_Number"].ToString();
                inwardEntity.LRDate = ds.Tables[0].Rows[0][7].ToString();
                inwardEntity.Carrier = ds.Tables[0].Rows[0]["Carrier"].ToString();
                inwardEntity.PlaceOfDespatch = ds.Tables[0].Rows[0]["Place_of_Despatch"].ToString();
                inwardEntity.Weight = TwoDecimalConversion(ds.Tables[0].Rows[0]["Consignment_Weight"].ToString());
                inwardEntity.NoOfCases = ds.Tables[0].Rows[0]["Number_of_Cases"].ToString();
                inwardEntity.RoadPermitNumber = ds.Tables[0].Rows[0]["Road_Permit_Number"].ToString();
                inwardEntity.RoadPermitDate = ds.Tables[0].Rows[0][21].ToString();                
                inwardEntity.FreightAmount = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Amount"].ToString());
                inwardEntity.FreightTax = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Tax"].ToString());
                inwardEntity.Insurance = TwoDecimalConversion(ds.Tables[0].Rows[0]["Insurance_Amount"].ToString());
                inwardEntity.PostalCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Postal_Charges"].ToString());
                inwardEntity.CouponCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Coupon_Charges"].ToString());
                inwardEntity.InvoiceNumber = ds.Tables[0].Rows[0]["Invoice_Number"].ToString();
                inwardEntity.InvoiceDate = ds.Tables[0].Rows[0][19].ToString();
                inwardEntity.ReceivedDate = ds.Tables[0].Rows[0][16].ToString();
                inwardEntity.InvoiceValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Inward_Value"].ToString());
                inwardEntity.SupplyPlantCode = ds.Tables[0].Rows[0]["Depot_code"].ToString();
                inwardEntity.OSIndicator = ds.Tables[0].Rows[0]["OS_LS_Indicator"].ToString();
                inwardEntity.TCSValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["TCS_Value"].ToString());
                inwardEntity.SuppliersCount = ds.Tables[0].Rows[0]["SuppCnt"].ToString();

                if (ds.Tables[0].Rows[0]["OS_LS_Indicator"].ToString().ToUpper() == "L")
                {
                    if (ds.Tables[0].Rows[0]["Branch_Code"].ToString().ToUpper() == "CHG")
                    {
                        inwardEntity.SGSTValue = "0.00";
                        inwardEntity.UTGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());
                    }
                    else
                    {
                        inwardEntity.SGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());
                        inwardEntity.UTGSTValue = "0.00";
                    }

                    inwardEntity.CGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax1"].ToString());
                    inwardEntity.IGSTValue = "0.00";

                }
                else
                {
                    inwardEntity.SGSTValue = "0.00";
                    inwardEntity.CGSTValue = "0.00";
                    inwardEntity.IGSTValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Local_Purchase_Tax"].ToString());
                    inwardEntity.UTGSTValue = "0.00";
                }

                inwardEntity.ReasonForReturn = string.Empty;// reader[0].ToString();
                inwardEntity.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                inwardEntity.ExcessShortageStatus = ds.Tables[1].Rows[0]["Cnt"].ToString();
                inwardEntity.PoPendingApprovalStatus = ds.Tables[4].Rows[0]["Cnt"].ToString();

                inwardEntity.Items = new List<InwardItem>();

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    InwardItem inwardItem = new InwardItem();
                    inwardItem.SNO = dr["SNO"].ToString();
                    inwardItem.CCWHNO = dr["Reference_Document"].ToString();
                    inwardItem.ItemCode = dr["ItemCode"].ToString();
                    inwardItem.SerialNo = dr["Serial_Number"].ToString();
                    inwardItem.PONumber = dr["Reference_Document"].ToString();
                    inwardItem.POQuantity = dr["POQTY"].ToString();
                    inwardItem.ReceivedQuantity = dr["ReceivedQty"].ToString();
                    inwardItem.BalanceQuantity = dr["Bal_QTY"].ToString();
                    inwardItem.Quantity = dr["QTY"].ToString();
                    inwardItem.SupplierPartNumber = dr["SupPartNo"].ToString();
                    inwardItem.ListPrice = dr["ListPrice"].ToString();
                    inwardItem.CostPricePerQty = dr["CostPricePerQty"].ToString();
                    inwardItem.CostPrice = dr["CostPrice"].ToString();
                    inwardItem.MRP = dr["MRP"].ToString();
                    inwardItem.Coupon = dr["Coupon"].ToString();
                    inwardItem.ListLessDiscount = dr["ListLessDiscount"].ToString();
                    inwardItem.ItemLocation = dr["ItemLocation"].ToString();
                    inwardItem.ItemTaxPercentage = dr["TaxPercentage"].ToString();
                    inwardItem.ItemTaxValue = dr["TaxValue"].ToString();
                    inwardItem.HSN = dr["Hsn_Code"].ToString();
                    inwardEntity.Items.Add(inwardItem);
                }

                inwardEntity.ExcessItems = new List<InwardExcessItem>();

                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    InwardExcessItem inwardExcessItem = new InwardExcessItem();
                    inwardExcessItem.SNO = dr["SNO"].ToString();
                    inwardExcessItem.CCWHNO = dr["Reference_Document"].ToString();
                    inwardExcessItem.ItemCode = dr["ItemCode"].ToString();
                    inwardExcessItem.SerialNo = dr["Serial_Number"].ToString();
                    inwardExcessItem.PONumber = dr["Reference_Document"].ToString();
                    inwardExcessItem.POQuantity = dr["POQTY"].ToString();
                    inwardExcessItem.ReceivedQuantity = dr["ReceivedQty"].ToString();
                    inwardExcessItem.BalanceQuantity = dr["Bal_QTY"].ToString();
                    inwardExcessItem.Quantity = dr["QTY"].ToString();
                    inwardExcessItem.SupplierPartNumber = dr["SupPartNo"].ToString();
                    inwardExcessItem.ListPrice = dr["ListPrice"].ToString();
                    inwardExcessItem.CostPricePerQty = dr["CostPricePerQty"].ToString();
                    inwardExcessItem.CostPrice = dr["CostPrice"].ToString();
                    inwardExcessItem.MRP = dr["MRP"].ToString();
                    inwardExcessItem.Coupon = dr["Coupon"].ToString();
                    inwardExcessItem.ListLessDiscount = dr["ListLessDiscount"].ToString();
                    inwardExcessItem.ItemLocation = dr["ItemLocation"].ToString();
                    inwardExcessItem.ItemTaxPercentage = dr["TaxPercentage"].ToString();
                    inwardExcessItem.ItemTaxValue = dr["TaxValue"].ToString();
                    inwardExcessItem.HSN = dr["Hsn_Code"].ToString();
                    inwardEntity.ExcessItems.Add(inwardExcessItem);
                }
            }

            return inwardEntity;
        }

        public DataSet GetMissingItemDetailsforPO(string BranchCode, string PONumber, string PODate, string SupplierCode, string InvoiceNumber, string InvoiceDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetMissingItemDetails_AutoGRN");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, PONumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@PO_Date", DbType.String, PODate.Trim());                    
                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, InvoiceDate.Trim());
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

        public DataSet GetSuppliemrayOrderDetailsforAutoGRN(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetSuppliementaryOrders_For_AutoGRN");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate.Trim());
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

        public DataSet GetUltimateIndentOrdersPlaced(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetUltimateIndentOrdersPlaced");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate.Trim());
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

        public DataSet GetPendingSTDNsPlaced(string BranchCode, string FromDate, string ToDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPendingSTDNsPlaced");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate.Trim());
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

        public List<CCWH> GetCCWHNumbers(string SupplierCode, string BranchCode, string TransactionType, string ScreenMode)
        {
            List<CCWH> objList = new List<CCWH>();
            List<GLMaster> GLMasters = new List<GLMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetCCWHNumbers");
            ImpalDB.AddInParameter(cmd, "@supplier", DbType.String, SupplierCode.Trim().Substring(0, 2));
            ImpalDB.AddInParameter(cmd, "@branchcode", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@transtype", DbType.String, TransactionType.Trim());
            ImpalDB.AddInParameter(cmd, "@ScreenMode", DbType.String, ScreenMode);

            CCWH ccwhItem = new CCWH();
            ccwhItem.CCWHNumber = "-- Select --";
            ccwhItem.PONumber = "0";
            objList.Add(ccwhItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ccwhItem = new CCWH();
                    ccwhItem.CCWHNumber = reader[0].ToString();
                    ccwhItem.PONumber = reader[1].ToString();
                    objList.Add(ccwhItem);
                }

            }

            return objList;
        }

        public List<Item> GetItemCode(string PoNumber, string InwardNumber, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemCodesFromCCWH");
            ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, PoNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, InwardNumber.Trim());
			ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);								  

            List<Item> PoItem = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "";
            PoItem.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["item_code"].ToString() + "_" + reader["Serial_Number"].ToString() + "_" + reader["PO_Number"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_number"].ToString();
                    PoItem.Add(objItem);
                }
            }
            return PoItem;
        }

        public List<Item> GetFOCItemCodes(string BranchCode, string SuppilerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            List<Item> PoItem = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            PoItem.Add(objItem);

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemCodesForFreeOfCost");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SuppilerCode.Trim());            
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["item_code"].ToString();// + "|" +reader["Supplier_Part_number"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_number"].ToString();
                    PoItem.Add(objItem);
                }
            }
            return PoItem;
        }

        public List<Item> GetInwardEntriesList(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardEntriesList");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Inward_Number"].ToString();
                    objItem.ItemDesc = reader["Inward_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }

            return InwardEntryList;
        }

        public List<Item> GetSupplierDepotInward(string BranchCode, string SupplierCode, string SupplierGSTIN)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select Gst_Supplier_Code, Supplier_Name + ' - ' + GSTIN_Number Supplier_Name from gst_supplier_master WITH (NOLOCK) where Supplier_Code='" + SupplierCode + "' and GSTIN_Number like '" + SupplierGSTIN + "%'";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem = new Item();
                objItem.ItemDesc = "-- Select --";
                objItem.ItemCode = "0";
                DepotList.Add(objItem);

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Supplier_Name"].ToString();
                    objItem.ItemCode = reader["Gst_Supplier_Code"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }

        public List<Item> GetSupplierDepotInwardEdit(string BranchCode, string SupplierCode, string SupplierGSTIN, string PlantCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select Gst_Supplier_Code, Supplier_Name + ' - ' + GSTIN_Number Supplier_Name from gst_supplier_master WITH (NOLOCK) where Supplier_Code='" + SupplierCode + "' and GSTIN_Number like '" + SupplierGSTIN + "%' and GST_Supplier_Code='" + PlantCode + "'";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem;

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Supplier_Name"].ToString();
                    objItem.ItemCode = reader["Gst_Supplier_Code"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }

        public List<Item> GetSupplierDepot(string BranchCode, string SupplierCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select Gst_Supplier_Code, Supplier_Name + ' - ' + GSTIN_Number Supplier_Name from gst_supplier_master WITH (NOLOCK) where Supplier_Code='" + SupplierCode + "'";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem = new Item();
                objItem.ItemDesc = "-- Select --";
                objItem.ItemCode = "0";
                DepotList.Add(objItem);

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Supplier_Name"].ToString();
                    objItem.ItemCode = reader["Gst_Supplier_Code"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }

        public List<Item> GetSupplierDepotAutoGRN(string BranchCode, string SupplierCode, string InvoiceNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select distinct sdm.Impal_Supplier_Plant_Code, sdm.Impal_Supplier_Plant_Name from Supplier_Despatch_Details sd WITH (NOLOCK) inner join Supplier_Customer_Master sm WITH (NOLOCK) on sm.Supplier_Customer_Code=sd.Supplier_Customer_Code inner join Supplier_Depot_Master sdm WITH (NOLOCK) " +
                         "On sd.Supplier_Code = sdm.Supplier_Code and sd.Supplier_Plant_Code = sdm.Supplier_Plant_Code and sm.Impal_Branch_Code = '" + BranchCode + "' and sd.Supplier_Code = '" + SupplierCode + "' and sd.Invoice_Number = '" + InvoiceNumber + "' " +
                         "inner join gst_supplier_master g WITH (NOLOCK) on g.Supplier_Code = sdm.Supplier_Code and g.GST_Supplier_Code = sdm.Impal_Supplier_Plant_Code";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem = new Item();

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Impal_Supplier_Plant_Name"].ToString();
                    objItem.ItemCode = reader["Impal_Supplier_Plant_Code"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }

        public List<Item> GetAutoGRNSupplierInvoices(string BranchCode, string SupplierCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "select distinct sd.Invoice_Number FROM Supplier_Despatch_Details sd WITH (NOLOCK) inner join Supplier_Customer_Master sm WITH (NOLOCK) on sm.Supplier_Customer_Code=sd.Supplier_Customer_Code inner join Supplier_Depot_Master sdm WITH (NOLOCK) " +
                         "On sd.Supplier_Code = sdm.Supplier_Code and sd.Supplier_Plant_Code = sdm.Supplier_Plant_Code and sm.Impal_Branch_Code = '" + BranchCode  + "' and sd.Supplier_Code = '"+ SupplierCode +"' " +
                         "left outer join Inward_Header ih WITH (NOLOCK) on ih.Branch_Code = sm.Impal_Branch_Code and ih.Supplier_Code = sd.Supplier_Code and ih.Invoice_Number = sd.Invoice_Number left outer join Accounting_Period a WITH (NOLOCK) on ih.Inward_Date between a.Start_Date and a.End_Date Where ih.Invoice_Number is null order by sd.Invoice_Number";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem = new Item();
                objItem.ItemDesc = "-- Select --";
                objItem.ItemCode = "0";
                DepotList.Add(objItem);

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Invoice_Number"].ToString();
                    objItem.ItemCode = reader["Invoice_Number"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }

        public InwardItem GetItemInformation(InwardItem inwardItem)
        {
            InwardItem InwardItemResult = new InwardItem();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemDetailsForInwardEntry");
            ImpalDB.AddInParameter(cmd, "@PO_NUMBER", DbType.String, inwardItem.PONumber.ToString());
            ImpalDB.AddInParameter(cmd, "@ITEM_CODE", DbType.String, inwardItem.ItemCode.ToString());
            ImpalDB.AddInParameter(cmd, "@SERIAL_NUMBER", DbType.String, inwardItem.SerialNo.ToString());
            ImpalDB.AddInParameter(cmd, "@BRANCH_CODE", DbType.String, inwardItem.BrachCode.ToString());
            ImpalDB.AddInParameter(cmd, "@TRAN_TYPE", DbType.String, inwardItem.TransactionTypeCode.ToString());
            ImpalDB.AddInParameter(cmd, "@INVOICE_DATE", DbType.String, inwardItem.InvoiceDate.ToString());
            ImpalDB.AddInParameter(cmd, "@OSLSINDICATOR", DbType.String, inwardItem.OSLSIndicator.ToString());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            InwardItemResult.PONumber = inwardItem.PONumber.ToString();
            InwardItemResult.ItemCode = inwardItem.ItemCode.ToString();

            if (inwardItem.SerialNo != null)
            {
                InwardItemResult.SerialNo = inwardItem.SerialNo.ToString();
            }

            InwardItemResult.BrachCode = inwardItem.BrachCode.ToString();
            InwardItemResult.InvoiceDate = inwardItem.InvoiceDate.ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                InwardItemResult.SupplierPartNumber = dr["SUPP_PART_NO"].ToString();
                InwardItemResult.POQuantity = dr["PO_QTY"].ToString();
                InwardItemResult.ReceivedQuantity = dr["RCVD_QTY"].ToString();
                InwardItemResult.BalanceQuantity = dr["DOC_QTY"].ToString();
                InwardItemResult.Quantity = "";
                InwardItemResult.ListPrice = dr["LIST_PRICE"].ToString();
                InwardItemResult.CostPricePerQty = dr["COST_PRICE"].ToString();
                InwardItemResult.CostPrice = dr["COST_PRICE"].ToString();
                InwardItemResult.Coupon = dr["COUPON"].ToString();
                InwardItemResult.ListLessDiscount = dr["LISTLESS_DISCOUNT"].ToString();
                InwardItemResult.EDIndicator = dr["EXCISE_IND"].ToString();
                InwardItemResult.EDValue = dr["EXCISE_VAL"].ToString();
                InwardItemResult.ItemLocation = dr["ITEMLOCATION"].ToString();
                InwardItemResult.ItemTaxPercentage = dr["TAXPERCENTAGE"].ToString();
            }
            return InwardItemResult;
        }

        public string CheckInvoiceExists(string strSupplierCode, string strInwardDate, string strInvoiceNo, string InvoiceDate, string strBranchCode)
        {
            string InvoiceNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select i.Invoice_Number from Inward_Header i WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on i.Branch_Code= '" + strBranchCode + "' and " +
                          "i.Supplier_Code = '" + strSupplierCode + "' and i.Invoice_Number ='" + strInvoiceNo + "' and i.Inward_Date between a.Start_Date and a.End_Date " +
                          "and Convert(date,'" + strInwardDate + "',103) between a.Start_Date and a.End_Date";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    InvoiceNo = reader[0].ToString();
                }
            }

            return InvoiceNo;
        }

        public string CheckInvoiceExistsEdit(string strSupplierCode, string strInwardDate, string strInvoiceNo, string InvoiceDate, string strBranchCode, string InwardNumber)
        {
            string InvoiceNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select i.Invoice_Number from Inward_Header i WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on i.Branch_Code= '" + strBranchCode + "' and " +
                          "i.Supplier_Code = '" + strSupplierCode + "' and i.Invoice_Number ='" + strInvoiceNo + "' and i.Inward_Number<>'" + InwardNumber + "' " +
                          "and i.Inward_Date between a.Start_Date and a.End_Date and Convert(date,'" + strInwardDate + "',103) between a.Start_Date and a.End_Date";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    InvoiceNo = reader[0].ToString();
                }
            }

            return InvoiceNo;
        }

        public string CheckInvoiceExistsAutoGRN(string strSupplierCode, string strInwardDate, string strInvoiceNo, string InvoiceDate, string strBranchCode, string InwardNumber)
        {
            string InvoiceNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select distinct sd.Invoice_Number FROM Supplier_Despatch_Details sd WITH (NOLOCK) inner join Supplier_Customer_Master sm WITH (NOLOCK) on sm.Supplier_Customer_Code=sd.Supplier_Customer_Code inner join Supplier_Depot_Master sdm WITH (NOLOCK) ";
            sSQL = sSQL + "On sd.Supplier_Code = sdm.Supplier_Code and sd.Supplier_Plant_Code = sdm.Supplier_Plant_Code and sm.Impal_Branch_Code= '" + strBranchCode + "' and sd.Supplier_Code = '" + strSupplierCode + "' and sd.Invoice_Number ='" + strInvoiceNo + "' ";
            sSQL = sSQL + "inner join Accounting_Period a WITH (NOLOCK) on a.Accounting_Period_Code=sd.Accounting_Period_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    InvoiceNo = reader[0].ToString();
                }
            }

            return InvoiceNo;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}
