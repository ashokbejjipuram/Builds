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
    public class StockTransferEntity
    {
        public string BranchCode { get; set; }
        public string StockTransferNumber { get; set; }
        public string StockTransferDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string ToBranch { get; set; }
        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public string RefDocNo { get; set; }
        public string RefDocDate { get; set; }
        public string Carrier { get; set; }
        public string Destination { get; set; }
        public string RoadPermitNo { get; set; }
        public string RoadPermitDate { get; set; }        
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }
        public string Remarks { get; set; }
        public string ModeOfDespatch { get; set; }
        public string TempNumber { get; set; }
        public List<StockTransferItem> Items { get; set; }
    }

    public class StockTransferItem
    {
        public string SNo { get; set; }
        public string STDNMode { get; set; }
        public string SupplierLineCode { get; set; }
        public string SupplierPartNo { get; set; }
        public string ItemCode { get; set; }
        public string AvailableQty { get; set; }
        public string Quantity { get; set; }
        public string CostPricePerQuantity { get; set; }
        public string CostPrice { get; set; }
        public string VatItcReversal { get; set; }
        public string GSTPercentage { get; set; }
        public string GSTValue { get; set; }
    }

    public class StockTransferReceiptEntity
    {
        public string BranchCode { get; set; }
        public string StockTransferReceiptNumber { get; set; }
        public string StockTransferReceiptDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string FromBranch { get; set; }
        public string GSTSupplierCode { get; set; }
        public string InvoiceValue { get; set; }
        public string VatValue { get; set; }
        public string TaxValue { get; set; }
        public string TaxValue1 { get; set; }
        public string SGSTValue { get; set; }
        public string CGSTValue { get; set; }
        public string UTGSTValue { get; set; }
        public string IGSTValue { get; set; }
        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public string RefDocNo { get; set; }
        public string RefDocDate { get; set; }
        public string Carrier { get; set; }
        public string Destination { get; set; }
        public string RoadPermitNo { get; set; }
        public string RoadPermitDate { get; set; }
        public string WareHouseNo { get; set; }
        public string WarehouseDate { get; set; }
        public string ReceivedStatus { get; set; }
        public string interStateStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public List<StockTransferReceiptItem> Items { get; set; }
    }

    public class StockTransferReceiptItem
    {
        public string SNo { get; set; }
        public string SupplierLineCode { get; set; }
        public string SupplierPartNo { get; set; }
        public string ItemLocation { get; set; }
        public string ItemCode { get; set; }
        public string ListPrice { get; set; }
        public string CostPricePerQty { get; set; }
        public string TotalCostPrice { get; set; }
        public string OriginalReceiptDate { get; set; }
        public string ReceivedQuantity { get; set; }
        public string AcceptedQuantity { get; set; }
        public string OSLSIndicator { get; set; }
        public string HandlingCharges { get; set; }
        public string STDNValue { get; set; }
        public string GSTPercentage { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string ProductGroupCode { get; set; }
        public string ConsInwardNo { get; set; }
        public string ConsSerialNo { get; set; }
    }

    public class PurchaseReturnEntity
    {
        public string BranchCode { get; set; }
        public string PurchaseReturnNumber { get; set; }
        public string PurchaseReturnDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierPlantCode { get; set; }
        public string OSLSindicator { get; set; }
        public string RefDocNumber { get; set; }
        public string RefDocDate { get; set; }        
        public string Remarks { get; set; }
        public string Address { get; set; }
        public string GSTINNumber { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }
        public string InwardNumber { get; set; }
        public string InwardDate { get; set; }
        public string ReceivedDate { get; set; }

        public List<PurchaseReturnItem> Items { get; set; }
    }

    public class PurchaseReturnItem
    {
        public string SNo { get; set; }
        public string SupplierPartNo { get; set; }
        public string ItemCode { get; set; }
        public string SupplierLineCode { get; set; }
        public string Quantity { get; set; }
        public string InwardQuantity { get; set; }
        public string AvailableQuantity { get; set; }
        public string CostPricePerQuantity { get; set; }
        public string Discount { get; set; }
        public string CostPrice { get; set; }
        public string GSTPercentage { get; set; }
        public string ItemDescription { get; set; }
        public string InwardSerialNumber { get; set; }
        public string Count { get; set; }
    }

    public class WarrantyClaimLucasEntity
    {
        public string BranchCode { get; set; }
        public string WarrantyClaimNumber { get; set; }
        public string WarrantyClaimDate { get; set; }
        public string LabourHandlingNumber { get; set; }
        public string LabourHandlingDate { get; set; }
        public string LabourHandlingCharges { get; set; }
        public string TransactionTypeCode { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierPlantCode { get; set; }
        public string OSLSindicator { get; set; }
        public string RefDocNumber { get; set; }
        public string RefDocDate { get; set; }
        public string JobCardNumber { get; set; }
        public string JobCardDate { get; set; }
        public string ShipTo { get; set; }
        public string Remarks { get; set; }
        public string Address { get; set; }
        public string GSTINNumber { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalLevel { get; set; }
        public string TaxMessage { get; set; }

        public List<WarrantyClaimLucasItem> Items { get; set; }
    }

    public class WarrantyClaimLucasItem
    {
        public string SNo { get; set; }
        public string SupplierPartNo { get; set; }
        public string ItemCode { get; set; }
        public string SupplierLineCode { get; set; }
        public string Quantity { get; set; }
        public string AvailableQuantity { get; set; }
        public string CostPricePerQuantity { get; set; }
        public string Discount { get; set; }
        public string CostPrice { get; set; }
        public string GSTPercentage { get; set; }
    }

    public class StockTransferTransactions
    {

        public List<Item> GetItemListBySupplierReceipt(string SupplierCode, string SupplierPartNo, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemDetailsBySupplier");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierPartNo);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> ItemList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            ItemList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Supplier_Part_Number"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_Number"].ToString();
                    //objItem.ItemDesc = reader["Item_Code"].ToString();
                    ItemList.Add(objItem);
                }
            }
            return ItemList;
        }


        public List<Item> GetItemListBySupplier(string SupplierCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL;

            sSQL = "select rtrim(ltrim(IM.Supplier_Part_Number)) Supplier_Part_Number, IM.Item_Code from CONSIGNMENT Co INNER JOIN Item_Master IM ";
            sSQL = sSQL + "  ON IM.Item_Code=CO.Item_Code and CO.Balance_Quantity>0 and CO.Status='A' and CO.Transaction_Type_Code='201' and CO.Branch_Code='" + BranchCode + "'";
            sSQL = sSQL + "  and SUBSTRING(IM.Item_Code,1,3)='" + SupplierCode + "' Group By rtrim(ltrim(IM.Supplier_Part_Number)),IM.Item_Code Order by rtrim(ltrim(IM.Supplier_Part_Number))";

            List<Item> ItemList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            ItemList.Add(objItem);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Supplier_Part_Number"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_Number"].ToString();
                    //objItem.ItemDesc = reader["Item_Code"].ToString();
                    ItemList.Add(objItem);
                }
            }
            return ItemList;
        }

        public List<Item> GetItemListBySupplierPartNo(string SupplierCode, string strPartNumber, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL;

            sSQL = "select rtrim(ltrim(IM.Supplier_Part_Number)) Supplier_Part_Number, IM.Item_Code from CONSIGNMENT Co INNER JOIN Item_Master IM ";
            sSQL = sSQL + "  ON IM.Item_Code=CO.Item_Code and CO.Balance_Quantity>0 and CO.Status='A' and CO.Transaction_Type_Code='201' and CO.Branch_Code='" + BranchCode + "' and SUBSTRING(IM.Item_Code,1,3)='" + SupplierCode + "'";
            sSQL = sSQL + "  and IM.Supplier_Part_Number like " + "'" + strPartNumber + "%'  Group By rtrim(ltrim(IM.Supplier_Part_Number)),IM.Item_Code Order by rtrim(ltrim(IM.Supplier_Part_Number))";

            List<Item> ItemList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            ItemList.Add(objItem);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Item_Code"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_Number"].ToString();
                    //objItem.ItemDesc = reader["Item_Code"].ToString();
                    ItemList.Add(objItem);
                }
            }
            return ItemList;
        }

        public List<Item> GetInwardNoForPurchaseReturn(string BranchCode, string InwardNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardNumber_PurchaseReturn");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, InwardNumber);

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

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public InwardEntity GetInwardDetailsForPurchaseReturn(string InwardNumber, string BranchCode)
        {
            InwardEntity inwardEntity = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardEntryHeaderAndDetails_PurchaseReturn");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@InwardNumber", DbType.String, InwardNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                inwardEntity = new InwardEntity();
                //Status
                inwardEntity.InwardNumber = string.Empty;//reader[0].ToString();
                inwardEntity.InwardDate = ds.Tables[0].Rows[0]["Inward_Date"].ToString();
                inwardEntity.SupplierCode = ds.Tables[0].Rows[0]["Supplier_Code"].ToString();
                inwardEntity.DCNumber = ds.Tables[0].Rows[0]["Document_Number"].ToString();
                inwardEntity.DCDate = ds.Tables[0].Rows[0]["Document_Date"].ToString();
                inwardEntity.LRNumber = ds.Tables[0].Rows[0]["LR_Number"].ToString();
                inwardEntity.LRDate = ds.Tables[0].Rows[0]["LR_Date"].ToString();
                inwardEntity.Carrier = ds.Tables[0].Rows[0]["Carrier"].ToString();
                inwardEntity.PlaceOfDespatch = ds.Tables[0].Rows[0]["Place_of_Despatch"].ToString();
                inwardEntity.Weight = TwoDecimalConversion(ds.Tables[0].Rows[0]["Consignment_Weight"].ToString());
                inwardEntity.NoOfCases = ds.Tables[0].Rows[0]["Number_of_Cases"].ToString();
                inwardEntity.RoadPermitNumber = ds.Tables[0].Rows[0]["Road_Permit_Number"].ToString();
                inwardEntity.RoadPermitDate = ds.Tables[0].Rows[0]["Road_Permit_Date"].ToString();                
                inwardEntity.FreightAmount = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Amount"].ToString());
                inwardEntity.FreightTax = TwoDecimalConversion(ds.Tables[0].Rows[0]["Freight_Tax"].ToString());
                inwardEntity.Insurance = TwoDecimalConversion(ds.Tables[0].Rows[0]["Insurance_Amount"].ToString());
                inwardEntity.InvoiceNumber = ds.Tables[0].Rows[0]["Invoice_Number"].ToString();
                inwardEntity.InvoiceDate = ds.Tables[0].Rows[0]["Invoice_Date"].ToString();
                inwardEntity.ReceivedDate = ds.Tables[0].Rows[0]["Received_Date"].ToString();
                inwardEntity.InvoiceValue = TwoDecimalConversion(ds.Tables[0].Rows[0]["Inward_Value"].ToString());
                inwardEntity.SupplyPlantCode = ds.Tables[0].Rows[0]["Depot_code"].ToString();
                inwardEntity.PostalCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Postal_Charges"].ToString());
                inwardEntity.CouponCharges = TwoDecimalConversion(ds.Tables[0].Rows[0]["Coupon_Charges"].ToString());
                inwardEntity.Items = new List<InwardItem>();

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    InwardItem inwardItem = new InwardItem();
                    inwardItem.SNO = dr["SNo"].ToString();
                    //inwardItem.SerialNo = dr["SerialNo"].ToString();
                    inwardItem.SupplierPartNumber = dr["Supplier_Part_Number"].ToString();
                    inwardItem.ItemCode = dr["Item_Code"].ToString();
                    inwardItem.ItemDescription = dr["Item_Short_Description"].ToString();
                    inwardItem.ReceivedQuantity = dr["ReceivedQuantity"].ToString();
                    inwardItem.BalanceQuantity = dr["BalanceQuantity"].ToString();
                    inwardItem.Quantity = dr["Quantity"].ToString();
                    inwardItem.CostPricePerQty = TwoDecimalConversion(dr["CostPricePerQty"].ToString());
                    inwardItem.CostPrice = TwoDecimalConversion(dr["CostPrice"].ToString());
                    inwardItem.ItemTaxPercentage = TwoDecimalConversion(dr["Sales_Tax_Percentage"].ToString());
                    inwardEntity.Items.Add(inwardItem);
                }
            }

            return inwardEntity;
        }

        public int CheckHSNCodeItem(string SupplierCode, string SuppPartNo)
        {
            int HSNcode = 0;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select case when len(isnull(Item_Type_Code,0))<= 3 then 0 else len(Item_Type_Code) end from Item_Master where substring(Item_Code,1,3)='" + SupplierCode + "' and Supplier_Part_Number='" + SuppPartNo + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            HSNcode = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return HSNcode;
        }

        public int CheckBranchItemPrice(string BranchCode, string SupplierCode, string SuppPartNo)
        {
            int Cnt = 0;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("IF EXISTS(select 1 from Branch_Item_Price b inner join Item_Master i On b.branch_code='" + BranchCode + "' and substring(i.Item_Code,1,3)='" + SupplierCode + "' and i.Supplier_Part_Number='" + SuppPartNo + "' and b.Item_Code=i.Item_Code) Select 1 Else Select 0");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return Cnt;
        }

        public List<Item> GetItemLocationSupplierPartNos(string SupplierCode, string strPartNumber, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL;

            sSQL = "select rtrim(ltrim(IM.Supplier_Part_Number)) Supplier_Part_Number, IM.Item_Code from Item_Master IM LEFT OUTER JOIN Item_Location IL ";
            sSQL = sSQL + "ON IM.Item_Code=IL.Item_Code and IL.Branch_Code='" + BranchCode + "' Where SUBSTRING(IM.Item_Code,1,3)='" + SupplierCode + "' ";
            sSQL = sSQL + "and IM.Supplier_Part_Number like " + "'" + strPartNumber + "%' Order by rtrim(ltrim(IM.Supplier_Part_Number))";

            List<Item> ItemList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            ItemList.Add(objItem);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Item_Code"].ToString();
                    objItem.ItemDesc = reader["Supplier_Part_Number"].ToString();
                    //objItem.ItemDesc = reader["Item_Code"].ToString();
                    ItemList.Add(objItem);
                }
            }

            return ItemList;
        }

        public string GetItemLocation(string strBranchCode, string strItemCode)
        {
            string itemLocation = "0";

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("IF EXISTS (Select Location_Code from Item_Location where Branch_Code='" + strBranchCode + "' and Item_Code='" + strItemCode + "') " +
                                                        "Select Location_Code from Item_Location where Branch_Code='" + strBranchCode + "' and Item_Code='" + strItemCode + "' ELSE Select '0'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            itemLocation = ImpalDB.ExecuteScalar(cmd).ToString();

            return itemLocation;
        }

        public string[] GetItemDetails(string SupplierCode, string SupplierpartNo, string ItemCode, string BranchCode, string FromToBranchCode, string TransType)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_ST_ItemDetails_GST");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierpartNo);
            ImpalDB.AddInParameter(cmd, "@ItemCode", DbType.String, ItemCode);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@FromToBranchCode", DbType.String, FromToBranchCode);
            ImpalDB.AddInParameter(cmd, "@TransType", DbType.String, TransType);
            string[] strResult = new string[7];
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strResult[0] = reader["Item_Code"].ToString();
                    strResult[1] = reader["Cost_Price"].ToString();
                    strResult[2] = reader["GST_Percentage"].ToString();
                    strResult[3] = reader["Available_Qty"].ToString();
                    strResult[4] = reader["ItemLocation"].ToString();
                    strResult[5] = reader["Cnt"].ToString();
                    strResult[6] = reader["Product_Group_Code"].ToString();
                }
            }

            return strResult;
        }

        public string[] GetCostToCostItemDetails(string SupplierCode, string SupplierpartNo, string BranchCode, string GSTSupplierCode, string FromToBranchCode, string TransType)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_CostToCost_ItemDetails");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierpartNo);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@GSTSupplierCode", DbType.String, GSTSupplierCode);
            ImpalDB.AddInParameter(cmd, "@FromToBranchCode", DbType.String, FromToBranchCode);
            ImpalDB.AddInParameter(cmd, "@TransType", DbType.String, TransType);
            string[] strResult = new string[7];
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strResult[0] = reader["Item_Code"].ToString();
                    strResult[1] = reader["Cost_Price"].ToString();
                    strResult[2] = reader["GST_Percentage"].ToString();
                    strResult[3] = reader["Available_Qty"].ToString();
                    strResult[4] = reader["ItemLocation"].ToString();
                    strResult[5] = reader["Cnt"].ToString();
                    strResult[6] = reader["Product_Group_Code"].ToString();
                }
            }

            return strResult;
        }

        public string[] GetItemDetailsGST(string SupplierCode, string SupplierpartNo, string BranchCode, string FromToBranchCode, string TransType)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_ST_ItemDetails_GST");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierpartNo);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@FromToBranchCode", DbType.String, FromToBranchCode);
            ImpalDB.AddInParameter(cmd, "@TransType", DbType.String, TransType);
            string[] strResult = new string[7];
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strResult[0] = reader["Item_Code"].ToString();
                    strResult[1] = reader["Cost_Price"].ToString();
                    strResult[2] = reader["GST_Percentage"].ToString();
                    strResult[3] = reader["Available_Qty"].ToString();
                    strResult[4] = reader["ItemLocation"].ToString();
                    strResult[5] = reader["Cnt"].ToString();
                    strResult[6] = reader["Product_Group_Code"].ToString();
                }
            }

            return strResult;
        }

        public DataSet EditSTEntry(string BranchCode, string stdnNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd  = ImpalDB.GetStoredProcCommand("Usp_updStdnheader_Status");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@StdnNo", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmd, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmd, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, ApprovalRemarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    cmd = null;

                    if (ApprovalStatus.ToUpper() == "I")
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_addglSTDN_Cancel");
                        ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, stdnNumber);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code ", DbType.String, BranchCode);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                        cmd = null;

                        cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_STDN_Data_Cancel");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, stdnNumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmd);
                        cmd = null;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

            return ds;
        }

        public void EditSTEntryInWard(string BranchCode, string FromBranchCode, string stdnNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("Usp_updStdnheader_Inward_Status");
                    ImpalDB.AddInParameter(cmdHeader, "@BranchCode", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@FromBranchCode", DbType.String, FromBranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@StdnNo", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }

        public string CheckRefNoExists(string refDocNumber, string BranchCode)
        {
            string RefNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Reference_Document_Number from STDN_Header s WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on s.Branch_Code= '" + BranchCode + "' and ";
            sSQL = sSQL + "i.Reference_Document_Number ='" + refDocNumber + "' and i.STDN_Date between a.Start_Date and a.End_Date";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    RefNo = reader[0].ToString();
                }
            }

            return RefNo;
        }

        public string CheckRefNoExistsCostToCost(string refDocNumber, string BranchCode)
        {
            string RefNo = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Invoice_Number from Inward_Header i WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on i.Branch_Code= '" + BranchCode + "' and";
            sSQL = sSQL + " i.Invoice_Number ='" + refDocNumber + "' and i.Inward_Date between a.Start_Date and a.End_Date";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    RefNo = reader[0].ToString();
                }
            }

            return RefNo;
        }

        public void AddNewSTEntry(ref StockTransferEntity stockTransferEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    string stdnNumber = string.Empty;

                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatchEntry_Header");
                    ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferEntity.StockTransferDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_No", DbType.String, stockTransferEntity.RefDocNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, stockTransferEntity.RefDocDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    stdnNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    cmd = null;

                    foreach (StockTransferItem stItem in stockTransferEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatchEntry_Detail");
                        ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stdnNumber);
                        ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferEntity.StockTransferDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());

                        if (string.IsNullOrEmpty(stItem.Quantity.Trim()))
                            stItem.Quantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(stItem.Quantity.Trim()));

                        ImpalDB.AddInParameter(cmd, "@TaxPercentage", DbType.String, stItem.GSTPercentage.Replace("%","").Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd).ToString();
                        cmd = null;
                    }                    

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_UpdSTDNDespatch_Header");
                    ImpalDB.AddInParameter(cmd1, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd1, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmd1, "@STDN_Number", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmd1, "@Temp_Number", DbType.String, stockTransferEntity.TempNumber);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1).ToString();
                    cmd1 = null;

                    stockTransferEntity.StockTransferNumber = stdnNumber;
                    stockTransferEntity.ErrorCode = "0";
                    stockTransferEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferEntity.ErrorCode = "1";
                stockTransferEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public int AddNewSTEntryHOSTDN(ref StockTransferEntity stockTransferEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int IntrBrchStatus = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    string stdnNumber = string.Empty;

                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatchEntry_Header_HOSTDN");
                    ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferEntity.StockTransferDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_No", DbType.String, stockTransferEntity.RefDocNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, stockTransferEntity.RefDocDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    stdnNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    cmd = null;

                    foreach (StockTransferItem stItem in stockTransferEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatchEntry_Detail_HOSTDN");
                        ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stdnNumber);
                        ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferEntity.StockTransferDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());

                        if (string.IsNullOrEmpty(stItem.Quantity.Trim()))
                            stItem.Quantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(stItem.Quantity.Trim()));

                        ImpalDB.AddInParameter(cmd, "@TaxPercentage", DbType.String, stItem.GSTPercentage.Replace("%", "").Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd).ToString();
                        cmd = null;
                    }

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_UpdSTDNDespatch_Header");
                    ImpalDB.AddInParameter(cmd1, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd1, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmd1, "@STDN_Number", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmd1, "@Temp_Number", DbType.String, stockTransferEntity.TempNumber);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1).ToString();
                    cmd1 = null;

                    if (stockTransferEntity.Remarks.Trim() == "HO_PO_STDN_Approval")
                    {
                        DbCommand cmdAddglSO = ImpalDB.GetStoredProcCommand("usp_addglSTDNO1_GST");
                        ImpalDB.AddInParameter(cmdAddglSO, "@doc_no", DbType.String, stdnNumber);
                        ImpalDB.AddInParameter(cmdAddglSO, "@Branch_Code", DbType.String, stockTransferEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdAddglSO, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                        ImpalDB.AddInParameter(cmdAddglSO, "@Indicator", DbType.String, "1");
                        cmdAddglSO.CommandTimeout = ConnectionTimeOut.TimeOut;
                        IntrBrchStatus = Convert.ToInt16(ImpalDB.ExecuteScalar(cmdAddglSO).ToString());
                    }

                    stockTransferEntity.StockTransferNumber = stdnNumber;
                    stockTransferEntity.ErrorCode = "0";
                    stockTransferEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferEntity.ErrorCode = "1";
                stockTransferEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return IntrBrchStatus;
        }

        public void UpdateLRdetails(ref StockTransferEntity stockTransferEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_updSTDNDespatch_LRdetails");
                    ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stockTransferEntity.StockTransferNumber);
                    ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferEntity.RoadPermitDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    cmd = null;

                    stockTransferEntity.ErrorCode = "0";
                    stockTransferEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferEntity.ErrorCode = "1";
                stockTransferEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void UpdateHOSTDNstatus(string BranchCode, string HOSTDNnumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updHOSTDN_Status");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Temp_Number", DbType.String, HOSTDNnumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public int AddNewSTEntryFinal(ref StockTransferEntity stockTransferEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int cnt = 0;
            int Indicator = 0;
            int IntrBrchStatus = 0;

            try
            {
                DbCommand cmd = null;
                DbCommand cmdAddglSO = null;                

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatch_Final");
                    ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stockTransferEntity.StockTransferNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, stockTransferEntity.ApprovalLevel.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    IntrBrchStatus = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
                    cmd = null;

                    if (IntrBrchStatus != 1)
                    {
                        foreach (StockTransferItem stItem in stockTransferEntity.Items)
                        {
                            cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNDespatch_GST");
                            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stockTransferEntity.StockTransferNumber.Trim());
                            ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferEntity.StockTransferDate.Trim());
                            ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferEntity.TransactionTypeCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferEntity.BranchCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.String, stItem.SNo.Trim());

                            if (string.IsNullOrEmpty(stItem.Quantity.Trim()))
                                stItem.Quantity = "0";

                            ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(stItem.Quantity.Trim()));
                            ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.String, stItem.CostPricePerQuantity.Trim());
                            ImpalDB.AddInParameter(cmd, "@Tax_Percentage", DbType.String, stItem.GSTPercentage.Trim());
                            ImpalDB.AddInParameter(cmd, "@Cnt", DbType.Int16, IntrBrchStatus);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                            cmd = null;
                        }
                    }

                    if (stockTransferEntity.Remarks.Trim() == "HO_PO_STDN_Approval")
                    {
                        Indicator = 1;
                    }

                    cmdAddglSO = ImpalDB.GetStoredProcCommand("usp_addglSTDNO1_GST");
                    ImpalDB.AddInParameter(cmdAddglSO, "@doc_no", DbType.String, stockTransferEntity.StockTransferNumber);
                    ImpalDB.AddInParameter(cmdAddglSO, "@Branch_Code", DbType.String, stockTransferEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmdAddglSO, "@ToBranch", DbType.String, stockTransferEntity.ToBranch.Trim());
                    ImpalDB.AddInParameter(cmdAddglSO, "@Indicator", DbType.String, Indicator);
                    cmdAddglSO.CommandTimeout = ConnectionTimeOut.TimeOut;
                    cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmdAddglSO).ToString());

                    stockTransferEntity.StockTransferNumber = stockTransferEntity.StockTransferNumber;
                    stockTransferEntity.ErrorCode = cnt.ToString();
                    stockTransferEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferEntity.ErrorCode = "1";
                stockTransferEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }

            return IntrBrchStatus;
        }

        public List<Item> GetSTDNDespatchEntriesEDP(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNDespatchEntriesEDP");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }
            return InwardEntryList;
        }

        public DataSet GetEinvoicingSTDNDetails(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_STDN_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public List<Item> GetHOSTDNDespatchEntriesEDP(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetHOSTDNDespatchEntriesEDP");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }
            return InwardEntryList;
        }

        public List<Item> GetSTDNDespatchEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNDespatchEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }
            return InwardEntryList;
        }

        public List<Item> GetSTDNDespatchEntriesHO(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNDespatchEntries_HO");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number_Status"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }
            return InwardEntryList;
        }

        public string GetSTDNDespatchEntryStatus(string BranchCode)
        {
            string status = "0";

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSTDNDespatchEntryStatus");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            status = ImpalDB.ExecuteScalar(cmd).ToString();

            return status;
        }

        public string[] GetPurchaseReturnItemDetails(string SupplierCode, string SupplierpartNo, string BranchCode, string OSLSIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_PurchaseReturn_ItemDetails");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierpartNo);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@OSLSIndicator", DbType.String, OSLSIndicator);
            string[] strResult = new string[5];
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strResult[0] = reader["Item_Code"].ToString();
                    strResult[1] = reader["Cost_Price"].ToString();
                    strResult[2] = reader["GST_Percentage"].ToString();
                    strResult[3] = reader["Available_Qty"].ToString();
                    strResult[4] = reader["ItemLocation"].ToString();
                }
            }

            return strResult;
        }

        public List<Item> GetPurchaseReturnEntriesEDP(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetPurchaseReturnEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> PurchaseReturnEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            PurchaseReturnEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Purchase_Return_Number"].ToString();
                    objItem.ItemDesc = reader["Purchase_Return_Number"].ToString();
                    PurchaseReturnEntryList.Add(objItem);
                }
            }

            return PurchaseReturnEntryList;
        }

        public List<Item> GetPurchaseReturnEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetPurchaseReturnEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> PurchaseReturnEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            PurchaseReturnEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Purchase_Return_Number"].ToString();
                    objItem.ItemDesc = reader["Purchase_Return_Number"].ToString();
                    PurchaseReturnEntryList.Add(objItem);
                }
            }

            return PurchaseReturnEntryList;
        }

        public List<Item> GetWarrantyClaimEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetWarrantyClaimLucasEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> WarrantyClaimEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            WarrantyClaimEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Warranty_Claim_Number"].ToString();
                    objItem.ItemDesc = reader["Warranty_Claim_Number"].ToString();
                    WarrantyClaimEntryList.Add(objItem);
                }
            }

            return WarrantyClaimEntryList;
        }
        public List<Item> GetWarrantyClaimEntriesLabourPending(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetWarrantyClaimLucasEntriesLabourPending");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> WarrantyClaimEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            WarrantyClaimEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Warranty_Claim_Number"].ToString();
                    objItem.ItemDesc = reader["Warranty_Claim_Number"].ToString();
                    WarrantyClaimEntryList.Add(objItem);
                }
            }

            return WarrantyClaimEntryList;
        }
        public List<Item> GetWarrantyClaimEntriesHandlingPending(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetWarrantyClaimLucasEntriesHandlingPending");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> WarrantyClaimEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            WarrantyClaimEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Warranty_Claim_Number"].ToString();
                    objItem.ItemDesc = reader["Warranty_Claim_Number"].ToString();
                    WarrantyClaimEntryList.Add(objItem);
                }
            }

            return WarrantyClaimEntryList;
        }
        public List<Item> GetHandlingChargesEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetHandlingChargesEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> WarrantyClaimEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            WarrantyClaimEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Labour_Handling_Number"].ToString();
                    objItem.ItemDesc = reader["Labour_Handling_Number"].ToString();
                    WarrantyClaimEntryList.Add(objItem);
                }
            }

            return WarrantyClaimEntryList;
        }

        public List<Item> GetLabourChargesEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetLabourChargesEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> WarrantyClaimEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "0";
            WarrantyClaimEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["Labour_Handling_Number"].ToString();
                    objItem.ItemDesc = reader["Labour_Handling_Number"].ToString();
                    WarrantyClaimEntryList.Add(objItem);
                }
            }

            return WarrantyClaimEntryList;
        }

        public StockTransferEntity GetSTDNEntryDetailsByNumber(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDN_Despatch_Details_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, STDNNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferEntity stockTransferEntity = new StockTransferEntity();
            StockTransferItem stockTransferItem = null;

            stockTransferEntity.Items = new List<StockTransferItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferEntity.BranchCode = dr["From_Branch_Code"].ToString();
                stockTransferEntity.StockTransferNumber = dr["STDN_Number"].ToString();
                stockTransferEntity.StockTransferDate = dr["STDN_Date"].ToString();
                stockTransferEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferEntity.ToBranch = dr["to_Branch_Code"].ToString();
                stockTransferEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferEntity.Carrier = dr["Carrier"].ToString();
                stockTransferEntity.Destination = dr["Destination"].ToString();
                stockTransferEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();
                stockTransferEntity.RefDocNo = dr["Ref_Doc_No"].ToString();
                stockTransferEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                stockTransferEntity.ApprovalStatus = dr["Approval_Status"].ToString();
                stockTransferEntity.Remarks = dr["Remarks"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferItem = new StockTransferItem();

                    stockTransferItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    stockTransferItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferItem.AvailableQty = Convert.ToString(reader["AvailableQty"]);
                    stockTransferItem.Quantity = Convert.ToString(reader["Quantity"]);
                    stockTransferItem.CostPricePerQuantity = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferItem.CostPrice = Convert.ToString(reader["Total_Cost_Price"]);
                    stockTransferItem.GSTPercentage = Convert.ToString(reader["GST"]);
                    stockTransferItem.GSTValue = Convert.ToString(reader["GST_Value"]);

                    stockTransferEntity.Items.Add(stockTransferItem);
                }
            }

            return stockTransferEntity;
        }

        public StockTransferEntity GetHOSTDNEntryDetailsByNumber(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetHO_STDN_Despatch_Details_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, STDNNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferEntity stockTransferEntity = new StockTransferEntity();
            StockTransferItem stockTransferItem = null;

            stockTransferEntity.Items = new List<StockTransferItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferEntity.BranchCode = dr["From_Branch_Code"].ToString();
                stockTransferEntity.StockTransferNumber = dr["STDN_Number"].ToString();
                stockTransferEntity.StockTransferDate = dr["STDN_Date"].ToString();
                stockTransferEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferEntity.ToBranch = dr["to_Branch_Code"].ToString();
                stockTransferEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferEntity.Carrier = dr["Carrier"].ToString();
                stockTransferEntity.Destination = dr["Destination"].ToString();
                stockTransferEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();
                stockTransferEntity.RefDocNo = dr["Ref_Doc_No"].ToString();
                stockTransferEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                stockTransferEntity.ApprovalStatus = dr["Approval_Status"].ToString();
                stockTransferEntity.Remarks = dr["Remarks"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferItem = new StockTransferItem();

                    stockTransferItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    stockTransferItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferItem.AvailableQty = Convert.ToString(reader["AvailableQty"]);
                    stockTransferItem.Quantity = Convert.ToString(reader["Quantity"]);
                    stockTransferItem.CostPricePerQuantity = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferItem.CostPrice = Convert.ToString(reader["Total_Cost_Price"]);
                    stockTransferItem.GSTPercentage = Convert.ToString(reader["GST"]);
                    stockTransferItem.GSTValue = Convert.ToString(reader["GST_Value"]);

                    stockTransferEntity.Items.Add(stockTransferItem);
                }
            }

            return stockTransferEntity;
        }

        public DataSet EditPurchaseReturnEntry(string BranchCode, string PurchaseReturnNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("Usp_UpdPurchaseReturnHeader_Status");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@PurchaseReturnNo", DbType.String, PurchaseReturnNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);
                    cmdHeader = null;

                    if (ApprovalStatus.ToUpper() == "I")
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_PurchaseReturn_Data_Cancel");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, PurchaseReturnNumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmd);
                        cmd = null;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

            return ds;
        }

        public DataSet EditPurchaseReturnEntryNew(string BranchCode, string PurchaseReturnNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("Usp_UpdPurchaseReturnHeader_Status_New");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@PurchaseReturnNo", DbType.String, PurchaseReturnNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);
                    cmdHeader = null;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

            return ds;
        }

        public PurchaseReturnEntity GetPurchaseReturnDetails(string BranchCode, string PurchaseReturnNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetPurchase_Return_Details");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, PurchaseReturnNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            PurchaseReturnEntity purchaseReturnEntity = new PurchaseReturnEntity();
            PurchaseReturnItem purchaseReturnItem = null;

            purchaseReturnEntity.Items = new List<PurchaseReturnItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                purchaseReturnEntity.BranchCode = dr["Branch_Code"].ToString();
                purchaseReturnEntity.PurchaseReturnNumber = dr["Purchase_Return_Number"].ToString();
                purchaseReturnEntity.PurchaseReturnDate = dr["Purchase_Return_Date"].ToString();
                purchaseReturnEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                purchaseReturnEntity.SupplierCode = dr["Supplier_Code"].ToString();
                purchaseReturnEntity.SupplierPlantCode = dr["Supplier_Plant_Code"].ToString();
                purchaseReturnEntity.RefDocNumber = dr["Ref_Doc_No"].ToString();
                purchaseReturnEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                purchaseReturnEntity.Address = dr["Address"].ToString();
                purchaseReturnEntity.GSTINNumber = dr["GSTIN_Number"].ToString();
                purchaseReturnEntity.ApprovalStatus = dr["Approval_Status"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    purchaseReturnItem = new PurchaseReturnItem();

                    purchaseReturnItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    purchaseReturnItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    purchaseReturnItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    purchaseReturnItem.Quantity = Convert.ToString(reader["Quantity"]);
                    purchaseReturnItem.AvailableQuantity = Convert.ToString(reader["AvailableQuantity"]);
                    purchaseReturnItem.CostPricePerQuantity = Convert.ToString(reader["CostPricePerQty"]);
                    purchaseReturnItem.CostPrice = Convert.ToString(reader["Total_Cost_Price"]);
                    purchaseReturnItem.GSTPercentage = Convert.ToString(reader["GST"]);

                    purchaseReturnEntity.Items.Add(purchaseReturnItem);
                }
            }

            return purchaseReturnEntity;
        }

        public PurchaseReturnEntity GetPurchaseReturnDetailsNew(string BranchCode, string PurchaseReturnNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetPurchase_Return_Details_New");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, PurchaseReturnNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            PurchaseReturnEntity purchaseReturnEntity = new PurchaseReturnEntity();
            PurchaseReturnItem purchaseReturnItem = null;

            purchaseReturnEntity.Items = new List<PurchaseReturnItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                purchaseReturnEntity.BranchCode = dr["Branch_Code"].ToString();
                purchaseReturnEntity.PurchaseReturnNumber = dr["Purchase_Return_Number"].ToString();
                purchaseReturnEntity.PurchaseReturnDate = dr["Purchase_Return_Date"].ToString();
                purchaseReturnEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                purchaseReturnEntity.SupplierCode = dr["Supplier_Code"].ToString();
                purchaseReturnEntity.SupplierPlantCode = dr["Supplier_Plant_Code"].ToString();
                purchaseReturnEntity.InwardNumber = dr["Inward_Number"].ToString();
                purchaseReturnEntity.InwardDate = dr["Inward_Date"].ToString();
                purchaseReturnEntity.ReceivedDate = dr["Date_Received"].ToString();
                purchaseReturnEntity.RefDocNumber = dr["Ref_Doc_No"].ToString();
                purchaseReturnEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                purchaseReturnEntity.Remarks = dr["Remarks"].ToString();
                purchaseReturnEntity.Address = dr["Address"].ToString();
                purchaseReturnEntity.GSTINNumber = dr["GSTIN_Number"].ToString();
                purchaseReturnEntity.ApprovalStatus = dr["Approval_Status"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    purchaseReturnItem = new PurchaseReturnItem();

                    purchaseReturnItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    purchaseReturnItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    purchaseReturnItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    purchaseReturnItem.ItemDescription = Convert.ToString(reader["Item_Short_Description"]);
                    purchaseReturnItem.Quantity = Convert.ToString(reader["Quantity"]);
                    purchaseReturnItem.InwardQuantity = Convert.ToString(reader["InwardQuantity"]);
                    purchaseReturnItem.AvailableQuantity = Convert.ToString(reader["AvailableQuantity"]);
                    purchaseReturnItem.CostPricePerQuantity = Convert.ToString(reader["CostPricePerQty"]);
                    purchaseReturnItem.CostPrice = Convert.ToString(reader["Total_Cost_Price"]);
                    purchaseReturnItem.GSTPercentage = Convert.ToString(reader["GST"]);
                    purchaseReturnItem.InwardSerialNumber = Convert.ToString(reader["InwardSerialNumber"]);

                    purchaseReturnEntity.Items.Add(purchaseReturnItem);
                }
            }

            return purchaseReturnEntity;
        }

        public PurchaseReturnEntity SupplyPlantAddressDetails(string SupplierCode, string SupplyPlantCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            PurchaseReturnEntity purchaseReturnEntity = new PurchaseReturnEntity();

            string sSQL = "select g.Address, g.GSTIN_Number, case when g.State_Code=b.State_Code then 'L' else 'O' end [OSLSindicator] from Gst_Supplier_Master g inner join Branch_Master b";
            sSQL = sSQL + " on g.Supplier_Code='" + SupplierCode + "' and g.Gst_Supplier_Code='" + SupplyPlantCode + "' and b.Branch_Code='" + BranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    purchaseReturnEntity.Address = reader["Address"].ToString();
                    purchaseReturnEntity.GSTINNumber = reader["GSTIN_Number"].ToString();
                    purchaseReturnEntity.OSLSindicator = reader["OSLSindicator"].ToString();
                }
            }

            return purchaseReturnEntity;
        }

        public void AddNewPurchaseReturnEDP(ref PurchaseReturnEntity purchaseReturnEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    string PurchaseReturnNumber = string.Empty;

                    foreach (PurchaseReturnItem stItem in purchaseReturnEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddPurchaseReturn_EDP");
                        ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, purchaseReturnEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, PurchaseReturnNumber);
                        ImpalDB.AddInParameter(cmd, "@Purchase_Return_Date", DbType.String, purchaseReturnEntity.PurchaseReturnDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, purchaseReturnEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, purchaseReturnEntity.SupplierCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierPlantCode", DbType.String, purchaseReturnEntity.SupplierPlantCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, purchaseReturnEntity.OSLSindicator.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Number", DbType.String, purchaseReturnEntity.RefDocNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, purchaseReturnEntity.RefDocDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());

                        if (string.IsNullOrEmpty(stItem.Quantity.Trim()))
                            stItem.Quantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(stItem.Quantity.Trim()));

                        if (stItem.SNo == "1")
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "1");
                        else
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "0");

                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        PurchaseReturnNumber = ImpalDB.ExecuteScalar(cmd).ToString();                        
                    }

                    cmd = ImpalDB.GetStoredProcCommand("usp_updPurchaseReturn_Header");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, purchaseReturnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, PurchaseReturnNumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    purchaseReturnEntity.PurchaseReturnNumber = PurchaseReturnNumber;
                    purchaseReturnEntity.ErrorCode = "0";
                    purchaseReturnEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                purchaseReturnEntity.ErrorCode = "1";
                purchaseReturnEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void AddNewPurchaseReturnNew(ref PurchaseReturnEntity purchaseReturnEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    string PurchaseReturnNumber = string.Empty;

                    foreach (PurchaseReturnItem stItem in purchaseReturnEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddPurchaseReturn_New");
                        ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, purchaseReturnEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, PurchaseReturnNumber);
                        ImpalDB.AddInParameter(cmd, "@Purchase_Return_Date", DbType.String, purchaseReturnEntity.PurchaseReturnDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, purchaseReturnEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, purchaseReturnEntity.SupplierCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierPlantCode", DbType.String, purchaseReturnEntity.SupplierPlantCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, purchaseReturnEntity.OSLSindicator.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Number", DbType.String, purchaseReturnEntity.RefDocNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, purchaseReturnEntity.RefDocDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, purchaseReturnEntity.InwardNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Inward_SerialNumber", DbType.String, stItem.InwardSerialNumber.Trim());

                        if (string.IsNullOrEmpty(stItem.Quantity.Trim()))
                            stItem.Quantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(stItem.Quantity.Trim()));

                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, purchaseReturnEntity.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, stItem.SNo);
                        ImpalDB.AddInParameter(cmd, "@Count", DbType.String, stItem.Count);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        PurchaseReturnNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    purchaseReturnEntity.PurchaseReturnNumber = PurchaseReturnNumber;
                    purchaseReturnEntity.ErrorCode = "0";
                    purchaseReturnEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                purchaseReturnEntity.ErrorCode = "1";
                purchaseReturnEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void AddNewPurchaseReturn(ref PurchaseReturnEntity purchaseReturnEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int Status = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;
                    DbCommand cmdAddglSO = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_UpdPurchaseReturn");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, purchaseReturnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Purchase_Return_Number", DbType.String, purchaseReturnEntity.PurchaseReturnNumber);
                    ImpalDB.AddInParameter(cmd, "@Approval_Status", DbType.String, "A");
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, purchaseReturnEntity.ApprovalLevel.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Status = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

                    if (Status == 1)
                    {
                        cmdAddglSO = ImpalDB.GetStoredProcCommand("usp_addglpr1");
                        ImpalDB.AddInParameter(cmdAddglSO, "@doc_no", DbType.String, purchaseReturnEntity.PurchaseReturnNumber);
                        ImpalDB.AddInParameter(cmdAddglSO, "@Branch_Code", DbType.String, purchaseReturnEntity.BranchCode.Trim());
                        cmdAddglSO.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdAddglSO);
                    }

                    purchaseReturnEntity.PurchaseReturnNumber = purchaseReturnEntity.PurchaseReturnNumber;
                    purchaseReturnEntity.ErrorCode = "0";
                    purchaseReturnEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                purchaseReturnEntity.ErrorCode = "1";
                purchaseReturnEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }
        public WarrantyClaimLucasEntity GetWarrantyClaimLucasDetails(string BranchCode, string WarrantyClaimNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetWarranty_Claim_Details");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Warranty_Claim_Number", DbType.String, WarrantyClaimNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            WarrantyClaimLucasEntity warrantyclaimEntity = new WarrantyClaimLucasEntity();
            WarrantyClaimLucasItem warrantyclaimItem = null;

            warrantyclaimEntity.Items = new List<WarrantyClaimLucasItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                warrantyclaimEntity.BranchCode = dr["Branch_Code"].ToString();
                warrantyclaimEntity.WarrantyClaimNumber = dr["Warranty_Claim_Number"].ToString();
                warrantyclaimEntity.WarrantyClaimDate = dr["Warranty_Claim_Date"].ToString();
                warrantyclaimEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                warrantyclaimEntity.SupplierCode = dr["Supplier_Code"].ToString();
                warrantyclaimEntity.SupplierPlantCode = dr["Supplier_Plant_Code"].ToString();
                warrantyclaimEntity.RefDocNumber = dr["Ref_Doc_No"].ToString();
                warrantyclaimEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                warrantyclaimEntity.JobCardNumber = dr["Job_Card_Number"].ToString();
                warrantyclaimEntity.JobCardDate = dr["Job_Card_Date"].ToString();
                warrantyclaimEntity.ShipTo = dr["Ship_To"].ToString();
                warrantyclaimEntity.Address = dr["Address"].ToString();
                warrantyclaimEntity.GSTINNumber = dr["GSTIN_Number"].ToString();
                warrantyclaimEntity.ApprovalStatus = dr["Approval_Status"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    warrantyclaimItem = new WarrantyClaimLucasItem();

                    warrantyclaimItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    warrantyclaimItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    warrantyclaimItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    warrantyclaimItem.Quantity = Convert.ToString(reader["Quantity"]);
                    warrantyclaimItem.AvailableQuantity = Convert.ToString(reader["AvailableQuantity"]);
                    warrantyclaimItem.CostPricePerQuantity = Convert.ToString(reader["CostPricePerQty"]);
                    warrantyclaimItem.CostPrice = Convert.ToString(reader["Total_Cost_Price"]);
                    warrantyclaimItem.GSTPercentage = Convert.ToString(reader["GST"]);

                    warrantyclaimEntity.Items.Add(warrantyclaimItem);
                }
            }

            return warrantyclaimEntity;
        }
        public string[] GetWarrantyClaimItemDetails(string SupplierCode, string SupplierpartNo, string BranchCode, string OSLSIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_PurchaseReturn_ItemDetails");
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode);
            ImpalDB.AddInParameter(cmd, "@SupplierPartNo", DbType.String, SupplierpartNo);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@OSLSIndicator", DbType.String, OSLSIndicator);
            string[] strResult = new string[5];
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strResult[0] = reader["Item_Code"].ToString();
                    strResult[1] = reader["Cost_Price"].ToString();
                    strResult[2] = reader["GST_Percentage"].ToString();
                    strResult[3] = reader["Available_Qty"].ToString();
                    strResult[4] = reader["ItemLocation"].ToString();
                }
            }

            return strResult;
        }
        public WarrantyClaimLucasEntity WarrantyClaimSupplyPlantAddressDetails(string SupplierCode, string SupplyPlantCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            WarrantyClaimLucasEntity warrantyclaimEntity = new WarrantyClaimLucasEntity();

            string sSQL = "select g.Address, g.GSTIN_Number, case when g.State_Code=b.State_Code then 'L' else 'O' end [OSLSindicator] from Gst_Supplier_Master g inner join Branch_Master b";
            sSQL = sSQL + " on g.Supplier_Code='" + SupplierCode + "' and g.Gst_Supplier_Code='" + SupplyPlantCode + "' and b.Branch_Code='" + BranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    warrantyclaimEntity.Address = reader["Address"].ToString();
                    warrantyclaimEntity.GSTINNumber = reader["GSTIN_Number"].ToString();
                    warrantyclaimEntity.OSLSindicator = reader["OSLSindicator"].ToString();
                }
            }

            return warrantyclaimEntity;
        }

        public WarrantyClaimLucasEntity GetWarrantyClaimLucasLabourHandlingChargesDetails(string BranchCode, string LabourHandlingNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetWarranty_Claim_LabourHandlingCharges_Details");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Labour_Handling_Number", DbType.String, LabourHandlingNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            WarrantyClaimLucasEntity warrantyclaimEntity = new WarrantyClaimLucasEntity();
            WarrantyClaimLucasItem warrantyclaimItem = null;

            warrantyclaimEntity.Items = new List<WarrantyClaimLucasItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                DataRow dr1 = ds.Tables[1].Rows[0];

                warrantyclaimEntity.BranchCode = dr["Branch_Code"].ToString();
                warrantyclaimEntity.LabourHandlingNumber= dr["Labour_Handling_Number"].ToString();
                warrantyclaimEntity.LabourHandlingDate = dr["Labour_Handling_Date"].ToString();
                warrantyclaimEntity.WarrantyClaimNumber = dr["Warranty_Claim_Number"].ToString();
                warrantyclaimEntity.WarrantyClaimDate = dr["Warranty_Claim_Date"].ToString();
                warrantyclaimEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                warrantyclaimEntity.LabourHandlingCharges = dr["Labour_Handling_Value"].ToString();
                warrantyclaimEntity.SupplierCode = dr["Supplier_Code"].ToString();
                warrantyclaimEntity.SupplierPlantCode = dr["Supplier_Plant_Code"].ToString();
                warrantyclaimEntity.RefDocNumber = dr["Ref_Doc_No"].ToString();
                warrantyclaimEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                warrantyclaimEntity.JobCardNumber = dr["Job_Card_Number"].ToString();
                warrantyclaimEntity.JobCardDate = dr["Job_Card_Date"].ToString();
                warrantyclaimEntity.ShipTo = dr["Ship_To"].ToString();
                warrantyclaimEntity.Address = dr["Address"].ToString();
                warrantyclaimEntity.GSTINNumber = dr["GSTIN_Number"].ToString();
                warrantyclaimEntity.ApprovalStatus = dr["Approval_Status"].ToString();
                warrantyclaimEntity.TaxMessage = dr1["ST_Description"].ToString();
            }

            return warrantyclaimEntity;
        }

        public void AddNewWarrantyClaimLucas(ref WarrantyClaimLucasEntity warrantyclaimEntity, string value)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;
                    DbCommand cmdAddglSO = null;

                    string WarrantyClaimNumber = string.Empty;

                    foreach (WarrantyClaimLucasItem wcItem in warrantyclaimEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddWarrantyClaim_Lucas");
                        ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, warrantyclaimEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Warranty_Claim_Number", DbType.String, WarrantyClaimNumber);
                        ImpalDB.AddInParameter(cmd, "@Warranty_Claim_Date", DbType.String, warrantyclaimEntity.WarrantyClaimDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, warrantyclaimEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, warrantyclaimEntity.SupplierCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@SupplierPlantCode", DbType.String, warrantyclaimEntity.SupplierPlantCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, warrantyclaimEntity.OSLSindicator.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Number", DbType.String, warrantyclaimEntity.RefDocNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, warrantyclaimEntity.RefDocDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Job_Card_Number", DbType.String, warrantyclaimEntity.JobCardNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Job_Card_Date", DbType.String, warrantyclaimEntity.JobCardDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Ship_To", DbType.String, warrantyclaimEntity.ShipTo.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, wcItem.ItemCode.Trim());

                        if (string.IsNullOrEmpty(wcItem.Quantity.Trim()))
                            wcItem.Quantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Decimal, Convert.ToDecimal(wcItem.Quantity.Trim()));

                        if (wcItem.SNo == "1")
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "1");
                        else
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "0");

                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        WarrantyClaimNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    cmdAddglSO = ImpalDB.GetStoredProcCommand("usp_addglwc_lucas1");
                    ImpalDB.AddInParameter(cmdAddglSO, "@doc_no", DbType.String, WarrantyClaimNumber);
                    ImpalDB.AddInParameter(cmdAddglSO, "@Branch_Code", DbType.String, warrantyclaimEntity.BranchCode.Trim());
                    cmdAddglSO.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdAddglSO);

                    warrantyclaimEntity.WarrantyClaimNumber = WarrantyClaimNumber;
                    warrantyclaimEntity.ErrorCode = "0";
                    warrantyclaimEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                warrantyclaimEntity.ErrorCode = "1";
                warrantyclaimEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public DataSet EditWarrantyClaimEntry(string BranchCode, string WarrantyClaimNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("Usp_UpdWarrantyClaimLucas_Status");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@WarrantyClaimNo", DbType.String, WarrantyClaimNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);
                    cmdHeader = null;

                    if (ApprovalStatus.ToUpper() == "I")
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaim_Lucas_Data_Cancel");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, WarrantyClaimNumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmd);
                        cmd = null;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

            return ds;
        }

        public DataSet EditWarrantyClaimLabourHandlingEntry(string BranchCode, string LabourHandlingNumber, string status, string ApprovalStatus, string ApprovalRemarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("Usp_UpdWarrantyClaimLucas_LabourHandling_Status");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@LabourHandlingNo", DbType.String, LabourHandlingNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Status", DbType.String, status);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Status", DbType.String, ApprovalStatus);
                    ImpalDB.AddInParameter(cmdHeader, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);
                    cmdHeader = null;

                    if (ApprovalStatus.ToUpper() == "I")
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaim_Lucas_LabourHandling_Data_Cancel");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                        ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, LabourHandlingNumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDB.ExecuteDataSet(cmd);
                        cmd = null;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

            return ds;
        }

        public void AddNewWarrantyClaimLucasHandlingLabourCharges(ref WarrantyClaimLucasEntity warrantyclaimEntity, string value, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;
                    DbCommand cmdAddglSO = null;

                    string LabourHandlingNumber = string.Empty;

                    cmd = ImpalDB.GetStoredProcCommand("usp_AddWarrantyClaim_Lucas_HandlingLabour_Charges");
                    ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, warrantyclaimEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Labour_Handling_Number", DbType.String, LabourHandlingNumber);
                    ImpalDB.AddInParameter(cmd, "@Labour_Handling_Date", DbType.String, warrantyclaimEntity.LabourHandlingDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Warranty_Claim_Number", DbType.String, warrantyclaimEntity.WarrantyClaimNumber);
                    ImpalDB.AddInParameter(cmd, "@Handling_Labour_Charges", DbType.String, warrantyclaimEntity.LabourHandlingCharges);
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, warrantyclaimEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, warrantyclaimEntity.SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@SupplierPlantCode", DbType.String, warrantyclaimEntity.SupplierPlantCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Os_Ls_Indicator", DbType.String, warrantyclaimEntity.OSLSindicator.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_Number", DbType.String, warrantyclaimEntity.RefDocNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, warrantyclaimEntity.RefDocDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Job_Card_Number", DbType.String, warrantyclaimEntity.JobCardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Job_Card_Date", DbType.String, warrantyclaimEntity.JobCardDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ship_To", DbType.String, warrantyclaimEntity.ShipTo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, null);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    LabourHandlingNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    cmdAddglSO = ImpalDB.GetStoredProcCommand("usp_addglwc_lucas_miscellaneous1");
                    ImpalDB.AddInParameter(cmdAddglSO, "@doc_no", DbType.String, LabourHandlingNumber);
                    ImpalDB.AddInParameter(cmdAddglSO, "@Branch_Code", DbType.String, warrantyclaimEntity.BranchCode.Trim());
                    cmdAddglSO.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdAddglSO);

                    warrantyclaimEntity.LabourHandlingNumber = LabourHandlingNumber;
                    warrantyclaimEntity.ErrorCode = "0";
                    warrantyclaimEntity.ErrorMsg = "";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                warrantyclaimEntity.ErrorCode = "1";
                warrantyclaimEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public DataSet GetEinvoicingDetailsPurchaseReturn(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_PurchaseReturn_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsPurchaseReturnOld(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_PurchaseReturn_Data_Old");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsWarrantyClaimLucas(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaimLucas_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsWarrantyClaimLucasOld(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaimLucas_Data_Old");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsWarrantyClaimLucasHandlingLabourCharges(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaimLucas_HandlingLabourCharges_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsWarrantyClaimLucasHandlingLabourChargesOld(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_WarrantyClaimLucas_HandlingLabourCharges_Data_Old");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public List<string> GetInvoicePurchaseReturnQR(string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, "P");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstInvoiceSTDN.Add(reader[0].ToString());
                }
            }

            if (lstInvoiceSTDN.Count <= 0)
                lstInvoiceSTDN.Add("");

            return lstInvoiceSTDN;
        }

        public List<string> GetInvoiceWarrantyClaimLucasQR(string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, "WC");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstInvoiceSTDN.Add(reader[0].ToString());
                }
            }

            if (lstInvoiceSTDN.Count <= 0)
                lstInvoiceSTDN.Add("");

            return lstInvoiceSTDN;
        }

        public List<string> GetInvoiceWarrantyClaimLucasLabourChargesQR(string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, "WL");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstInvoiceSTDN.Add(reader[0].ToString());
                }
            }

            if (lstInvoiceSTDN.Count <= 0)
                lstInvoiceSTDN.Add("");

            return lstInvoiceSTDN;
        }

        public List<string> GetInvoiceWarrantyClaimLucasHandlingChargesQR(string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, "WH");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstInvoiceSTDN.Add(reader[0].ToString());
                }
            }

            if (lstInvoiceSTDN.Count <= 0)
                lstInvoiceSTDN.Add("");

            return lstInvoiceSTDN;
        }
    }


    public class StockTransferReceiptTransactions
    {
        public StockTransferReceiptEntity GetSTDNReceiptDetailsByNumberGST(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDN_Receipt_Details_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, STDNNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            StockTransferReceiptItem stockTransferReceiptItem = null;

            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferReceiptEntity.StockTransferReceiptNumber = dr["STDN_Number"].ToString();
                stockTransferReceiptEntity.StockTransferReceiptDate = dr["STDN_Date"].ToString();
                stockTransferReceiptEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferReceiptEntity.FromBranch = dr["From_Branch_Code"].ToString();
                stockTransferReceiptEntity.BranchCode = dr["To_Branch_Code"].ToString();
                stockTransferReceiptEntity.InvoiceValue = dr["Invoice_Value"].ToString();
                stockTransferReceiptEntity.IGSTValue = dr["GST_Value"].ToString();
                stockTransferReceiptEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferReceiptEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferReceiptEntity.Carrier = dr["Carrier"].ToString();
                stockTransferReceiptEntity.Destination = dr["Destination"].ToString();
                stockTransferReceiptEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferReceiptEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();
                stockTransferReceiptEntity.RefDocNo = dr["Ref_Doc_No"].ToString();
                stockTransferReceiptEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferReceiptItem = new StockTransferReceiptItem();

                    stockTransferReceiptItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    stockTransferReceiptItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferReceiptItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferReceiptItem.ItemLocation = Convert.ToString(reader["Item_Location"]);
                    stockTransferReceiptItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferReceiptItem.CostPricePerQty = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferReceiptItem.OriginalReceiptDate = Convert.ToString(reader["Original_Receipt_Date"]);
                    stockTransferReceiptItem.ReceivedQuantity = Convert.ToString(reader["Received_Quantity"]);
                    stockTransferReceiptItem.TotalCostPrice = Convert.ToString(reader["TotalCostPrice"]);
                    stockTransferReceiptItem.OSLSIndicator = Convert.ToString(reader["OS_LS_Indicator"]);
                    stockTransferReceiptItem.GSTPercentage = Convert.ToString(reader["Sales_Tax_Percentage"]);
                    stockTransferReceiptItem.InvoiceNumber = Convert.ToString(reader["Invoice_Number"]);
                    stockTransferReceiptItem.InvoiceDate = Convert.ToString(reader["Invoice_Date"]);
                    stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
                }
            }

            return stockTransferReceiptEntity;
        }

        public void AddNewSTReceiptEntryOnline(ref StockTransferReceiptEntity stockTransferReceiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    string stdnNumber = string.Empty;

                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNReceipt_Header_Online");
                    ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferReceiptEntity.StockTransferReceiptDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferReceiptEntity.TransactionTypeCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferReceiptEntity.FromBranch.Trim());
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Reference_STDN", DbType.String, stockTransferReceiptEntity.RefDocNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Reference_Date", DbType.String, stockTransferReceiptEntity.RefDocDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@STDN_Value", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, stockTransferReceiptEntity.IGSTValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferReceiptEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferReceiptEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferReceiptEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferReceiptEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferReceiptEntity.RoadPermitDate.Trim());
                    stdnNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    if (stdnNumber.Substring(0, 1) != "E")
                    {
                        foreach (StockTransferReceiptItem stItem in stockTransferReceiptEntity.Items)
                        {
                            cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNReceipt_Details_Online");
                            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stdnNumber);
                            ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferReceiptEntity.StockTransferReceiptDate.Trim());
                            ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferReceiptEntity.TransactionTypeCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferReceiptEntity.FromBranch.Trim());
                            ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@Item_Location", DbType.String, stItem.ItemLocation.Trim());
                            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());
                            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, stItem.SupplierPartNo.Trim());
                            ImpalDB.AddInParameter(cmd, "@Reference_STDN", DbType.String, stockTransferReceiptEntity.RefDocNo.Trim());
                            ImpalDB.AddInParameter(cmd, "@Reference_Date", DbType.String, stockTransferReceiptEntity.RefDocDate.Trim());
                            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, stItem.ListPrice.Trim());
                            ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.String, stItem.CostPricePerQty.Trim());
                            ImpalDB.AddInParameter(cmd, "@Original_Receipt_Date", DbType.String, stItem.OriginalReceiptDate.Trim());

                            if (string.IsNullOrEmpty(stItem.ReceivedQuantity.Trim()))
                                stItem.ReceivedQuantity = "0";

                            ImpalDB.AddInParameter(cmd, "@Received_Quantity", DbType.Decimal, Convert.ToDecimal(stItem.ReceivedQuantity.Trim()));
                            ImpalDB.AddInParameter(cmd, "@Accepted_Quantity", DbType.Decimal, Convert.ToDecimal(stItem.AcceptedQuantity.Trim()));

                            if (stItem.OSLSIndicator.Trim().ToUpper() == "OS")
                                ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, "O");
                            else
                                ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, "L");

                            ImpalDB.AddInParameter(cmd, "@Handling_Charges", DbType.String, "0.00");
                            ImpalDB.AddInParameter(cmd, "@STDN_Value", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());
                            ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, stItem.InvoiceNumber.Trim());
                            ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, stItem.InvoiceDate.Trim());
                            ImpalDB.AddInParameter(cmd, "@Warehouse_No", DbType.String, stockTransferReceiptEntity.WareHouseNo.Trim());
                            ImpalDB.AddInParameter(cmd, "@Warehouse_Date", DbType.String, stockTransferReceiptEntity.WarehouseDate.Trim());
                            ImpalDB.AddInParameter(cmd, "@Cons_Inward_Number", DbType.String, stItem.ConsInwardNo.Trim());
                            ImpalDB.AddInParameter(cmd, "@Cons_Serial_Number", DbType.String, stItem.ConsSerialNo.Trim());
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                        }

                        stockTransferReceiptEntity.StockTransferReceiptNumber = stdnNumber;
                        stockTransferReceiptEntity.ErrorCode = "0";
                        stockTransferReceiptEntity.ErrorMsg = "";
                    }
                    else
                    {
                        stockTransferReceiptEntity.StockTransferReceiptNumber = stdnNumber.Substring(1, stdnNumber.Length - 1);
                        stockTransferReceiptEntity.ErrorCode = "0";
                        stockTransferReceiptEntity.ErrorMsg = "";
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferReceiptEntity.ErrorCode = "1";
                stockTransferReceiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void AddNewSTReceiptEntryFinalEDP(ref StockTransferReceiptEntity stockTransferReceiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_UpdSTDNReceipt_Final_EDP");
                    ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stockTransferReceiptEntity.StockTransferReceiptNumber);
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@STDN_Value", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, stockTransferReceiptEntity.IGSTValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferReceiptEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferReceiptEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferReceiptEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferReceiptEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferReceiptEntity.RoadPermitDate.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd).ToString();

                    stockTransferReceiptEntity.ErrorCode = "0";
                    stockTransferReceiptEntity.ErrorMsg = "";

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferReceiptEntity.ErrorCode = "1";
                stockTransferReceiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void AddNewSTReceiptEntryFinal(ref StockTransferReceiptEntity stockTransferReceiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_UpdSTDNReceipt_Final");
                    ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stockTransferReceiptEntity.StockTransferReceiptNumber);
                    ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@STDN_Value", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, stockTransferReceiptEntity.IGSTValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferReceiptEntity.LRNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferReceiptEntity.LRDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferReceiptEntity.Carrier.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferReceiptEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferReceiptEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_No", DbType.String, stockTransferReceiptEntity.RefDocNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@Ref_Doc_Date", DbType.String, stockTransferReceiptEntity.RefDocDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, stockTransferReceiptEntity.ApprovalLevel.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd).ToString();

                    cmd = ImpalDB.GetStoredProcCommand("Usp_addglsi_GST");
                    ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, stockTransferReceiptEntity.StockTransferReceiptNumber);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@invoice_amt", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());
                    ImpalDB.AddInParameter(cmd, "@gst_amt", DbType.String, stockTransferReceiptEntity.IGSTValue.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    stockTransferReceiptEntity.ErrorCode = "0";
                    stockTransferReceiptEntity.ErrorMsg = "";

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferReceiptEntity.ErrorCode = "1";
                stockTransferReceiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public void AddNewCostToCostInwardEntry(ref StockTransferReceiptEntity stockTransferReceiptEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string stdnNumber = string.Empty;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = null;

                    foreach (StockTransferReceiptItem stItem in stockTransferReceiptEntity.Items)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_AddCostToCostInward");
                        ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, stdnNumber);
                        ImpalDB.AddInParameter(cmd, "@STDN_Date", DbType.String, stockTransferReceiptEntity.StockTransferReceiptDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, stockTransferReceiptEntity.TransactionTypeCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@GSTSupplierCode", DbType.String, stockTransferReceiptEntity.GSTSupplierCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@FromBranch", DbType.String, stockTransferReceiptEntity.FromBranch.Trim());
                        ImpalDB.AddInParameter(cmd, "@ToBranch", DbType.String, stockTransferReceiptEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Location", DbType.String, stItem.ItemLocation.Trim());
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, stItem.ItemCode.Trim());
                        ImpalDB.AddInParameter(cmd, "@Reference_STDN", DbType.String, stockTransferReceiptEntity.RefDocNo.Trim());
                        ImpalDB.AddInParameter(cmd, "@Reference_Date", DbType.String, stockTransferReceiptEntity.RefDocDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, stItem.CostPricePerQty.Trim());
                        ImpalDB.AddInParameter(cmd, "@Original_Receipt_Date", DbType.String, stItem.OriginalReceiptDate.Trim());

                        if (string.IsNullOrEmpty(stItem.ReceivedQuantity.Trim()))
                            stItem.ReceivedQuantity = "0";
                        ImpalDB.AddInParameter(cmd, "@Received_Quantity", DbType.Decimal, Convert.ToDecimal(stItem.ReceivedQuantity.Trim()));

                        if (stItem.OSLSIndicator.Trim().ToUpper() == "OS")
                            ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, "O");
                        else
                            ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, "L");

                        ImpalDB.AddInParameter(cmd, "@Tax_Percentage", DbType.String, stItem.GSTPercentage);
                        ImpalDB.AddInParameter(cmd, "@Handling_Charges", DbType.String, "0.00");
                        ImpalDB.AddInParameter(cmd, "@STDN_Value", DbType.String, stockTransferReceiptEntity.InvoiceValue.Trim());

                        if (stockTransferReceiptEntity.interStateStatus == "1")
                        {
                            if (string.IsNullOrEmpty(stockTransferReceiptEntity.SGSTValue.Trim()))
                                stockTransferReceiptEntity.SGSTValue = "0";
                            if (string.IsNullOrEmpty(stockTransferReceiptEntity.UTGSTValue.Trim()))
                                stockTransferReceiptEntity.UTGSTValue = "0";
                            if (string.IsNullOrEmpty(stockTransferReceiptEntity.CGSTValue.Trim()))
                                stockTransferReceiptEntity.CGSTValue = "0";

                            if (Convert.ToDouble(stockTransferReceiptEntity.SGSTValue.ToString()) > 0)
                            {
                                stockTransferReceiptEntity.TaxValue = stockTransferReceiptEntity.SGSTValue;
                            }
                            else if (Convert.ToDouble(stockTransferReceiptEntity.UTGSTValue.ToString()) > 0)
                            {
                                stockTransferReceiptEntity.TaxValue = stockTransferReceiptEntity.UTGSTValue;
                            }

                            stockTransferReceiptEntity.TaxValue1 = stockTransferReceiptEntity.CGSTValue;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(stockTransferReceiptEntity.IGSTValue.Trim()))
                                stockTransferReceiptEntity.IGSTValue = "0";

                            stockTransferReceiptEntity.TaxValue = stockTransferReceiptEntity.IGSTValue;
                            stockTransferReceiptEntity.TaxValue1 = "0";
                        }

                        ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, stockTransferReceiptEntity.TaxValue.Trim());
                        ImpalDB.AddInParameter(cmd, "@Tax_Value1", DbType.String, stockTransferReceiptEntity.TaxValue1.Trim());
                        ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, stItem.InvoiceNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, stItem.InvoiceDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@LR_Number", DbType.String, stockTransferReceiptEntity.LRNumber.Trim());
                        ImpalDB.AddInParameter(cmd, "@LR_Date", DbType.String, stockTransferReceiptEntity.LRDate.Trim());
                        ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, stockTransferReceiptEntity.Carrier.Trim());
                        ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, stockTransferReceiptEntity.RoadPermitNo.Trim());
                        ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, stockTransferReceiptEntity.RoadPermitDate.Trim());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        stdnNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    DbCommand cmdGlGr1 = ImpalDB.GetStoredProcCommand("usp_addglgr1_GST");
                    ImpalDB.AddInParameter(cmdGlGr1, "@doc_no", DbType.String, stdnNumber);
                    ImpalDB.AddInParameter(cmdGlGr1, "@Branch_Code", DbType.String, stockTransferReceiptEntity.BranchCode);
                    cmdGlGr1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdGlGr1);

                    stockTransferReceiptEntity.StockTransferReceiptNumber = stdnNumber;
                    stockTransferReceiptEntity.ErrorCode = "0";
                    stockTransferReceiptEntity.ErrorMsg = "";

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                stockTransferReceiptEntity.ErrorCode = "1";
                stockTransferReceiptEntity.ErrorMsg = exp.Message.ToString();
                throw exp;
            }
        }

        public List<Item> GetSTDNReceiptEntriesEDP(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNReceiptEntriesEDP");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }
            return InwardEntryList;
        }
        
        public List<Item> GetSTDNReceiptEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNReceiptEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {                
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }

            return InwardEntryList;        
        }

        public List<Item> GetCostToCostEntries(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetCostToCostEntries");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "";
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

        public List<Item> GetSTDNReceiptEntriesOnline(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDNReceiptEntriesOnline");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);

            List<Item> InwardEntryList = new List<Item>();
            Item objItem = new Item();
            objItem.ItemDesc = "";
            objItem.ItemCode = "";
            InwardEntryList.Add(objItem);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemCode = reader["STDN_Number"].ToString();
                    objItem.ItemDesc = reader["STDN_Number"].ToString();
                    InwardEntryList.Add(objItem);
                }
            }

            return InwardEntryList;
        }

        public StockTransferReceiptEntity GetSTDNReceiptDetailsByNumber(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDN_Receipt_Details_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, STDNNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            StockTransferReceiptItem stockTransferReceiptItem = null;

            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferReceiptEntity.StockTransferReceiptNumber = dr["STDN_Number"].ToString();
                stockTransferReceiptEntity.StockTransferReceiptDate = dr["STDN_Date"].ToString();
                stockTransferReceiptEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferReceiptEntity.FromBranch = dr["From_Branch_Code"].ToString();
                stockTransferReceiptEntity.BranchCode = dr["To_Branch_Code"].ToString();
                stockTransferReceiptEntity.InvoiceValue = dr["Invoice_Value"].ToString();
                stockTransferReceiptEntity.IGSTValue = dr["GST_Value"].ToString();
                stockTransferReceiptEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferReceiptEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferReceiptEntity.Carrier = dr["Carrier"].ToString();
                stockTransferReceiptEntity.Destination = dr["Destination"].ToString();
                stockTransferReceiptEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferReceiptEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();
                stockTransferReceiptEntity.RefDocNo = dr["Ref_Doc_No"].ToString();
                stockTransferReceiptEntity.RefDocDate = dr["Ref_Doc_Date"].ToString();
                stockTransferReceiptEntity.ReceivedStatus = dr["Received_Status"].ToString();
                stockTransferReceiptEntity.ApprovalStatus = dr["Approval_Status"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferReceiptItem = new StockTransferReceiptItem();

                    stockTransferReceiptItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    
                    stockTransferReceiptItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferReceiptItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferReceiptItem.ItemLocation = Convert.ToString(reader["Item_Location"]);
                    stockTransferReceiptItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferReceiptItem.ListPrice = Convert.ToString(reader["List_Price"]);
                    stockTransferReceiptItem.CostPricePerQty = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferReceiptItem.OriginalReceiptDate = Convert.ToString(reader["Original_Receipt_Date"]);
                    stockTransferReceiptItem.ReceivedQuantity = Convert.ToString(reader["Received_Quantity"]);
                    stockTransferReceiptItem.AcceptedQuantity = Convert.ToString(reader["Accepted_Quantity"]);
                    stockTransferReceiptItem.TotalCostPrice = Convert.ToString(reader["TotalCostPrice"]);
                    stockTransferReceiptItem.OSLSIndicator = Convert.ToString(reader["OS_LS_Indicator"]);
                    stockTransferReceiptItem.GSTPercentage = Convert.ToString(reader["Sales_Tax_Percentage"]);
                    stockTransferReceiptItem.InvoiceNumber = Convert.ToString(reader["Invoice_Number"]);
                    stockTransferReceiptItem.InvoiceDate = Convert.ToString(reader["Invoice_Date"]);
                    stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
                }
            }

            return stockTransferReceiptEntity;
        }

        public StockTransferReceiptEntity GetCostToCostDetailsByNumber(string BranchCode, string InwardNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetCostToCost_Details_ByNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, InwardNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            StockTransferReceiptItem stockTransferReceiptItem = null;

            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferReceiptEntity.StockTransferReceiptNumber = dr["Inward_Number"].ToString();
                stockTransferReceiptEntity.StockTransferReceiptDate = dr["Inward_Date"].ToString();
                stockTransferReceiptEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferReceiptEntity.FromBranch = dr["From_Branch_Code"].ToString();
                stockTransferReceiptEntity.BranchCode = dr["Branch_Code"].ToString();
                stockTransferReceiptEntity.InvoiceValue = dr["Invoice_Value"].ToString();
                stockTransferReceiptEntity.SGSTValue = dr["SGST_Value"].ToString();
                stockTransferReceiptEntity.CGSTValue = dr["CGST_Value"].ToString();
                stockTransferReceiptEntity.IGSTValue = dr["IGST_Value"].ToString();
                stockTransferReceiptEntity.UTGSTValue = dr["UTGST_Value"].ToString();
                stockTransferReceiptEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferReceiptEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferReceiptEntity.Carrier = dr["Carrier"].ToString();
                stockTransferReceiptEntity.Destination = dr["Destination"].ToString();
                stockTransferReceiptEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferReceiptEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();
                stockTransferReceiptEntity.RefDocNo = dr["Invoice_Number"].ToString();
                stockTransferReceiptEntity.RefDocDate = dr["Invoice_Date"].ToString();
                stockTransferReceiptEntity.ReceivedStatus = dr["Received_Status"].ToString();
                stockTransferReceiptEntity.ApprovalStatus = dr["Approval_Status"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferReceiptItem = new StockTransferReceiptItem();

                    stockTransferReceiptItem.SNo = Convert.ToString(reader["Serial_Number"]);
                    stockTransferReceiptItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferReceiptItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferReceiptItem.ItemLocation = Convert.ToString(reader["Item_Location"]);
                    stockTransferReceiptItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferReceiptItem.ListPrice = Convert.ToString(reader["List_Price"]);
                    stockTransferReceiptItem.CostPricePerQty = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferReceiptItem.OriginalReceiptDate = Convert.ToString(reader["Original_Receipt_Date"]);
                    stockTransferReceiptItem.ReceivedQuantity = Convert.ToString(reader["Received_Quantity"]);
                    stockTransferReceiptItem.AcceptedQuantity = Convert.ToString(reader["Accepted_Quantity"]);
                    stockTransferReceiptItem.TotalCostPrice = Convert.ToString(reader["TotalCostPrice"]);
                    stockTransferReceiptItem.OSLSIndicator = Convert.ToString(reader["OS_LS_Indicator"]);
                    stockTransferReceiptItem.GSTPercentage = Convert.ToString(reader["Sales_Tax_Percentage"]);
                    stockTransferReceiptItem.InvoiceNumber = Convert.ToString(reader["Invoice_Number"]);
                    stockTransferReceiptItem.InvoiceDate = Convert.ToString(reader["Invoice_Date"]);
                    stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
                }
            }

            return stockTransferReceiptEntity;
        }

        public StockTransferReceiptEntity GetSTDNDetailsOnline(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_GetSTDN_Receipt_Details_Online_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@STDN_Number", DbType.String, STDNNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            StockTransferReceiptItem stockTransferReceiptItem = null;

            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            if (ds.Tables.Count == 2)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                stockTransferReceiptEntity.StockTransferReceiptNumber = dr["STDN_Number"].ToString();
                stockTransferReceiptEntity.StockTransferReceiptDate = dr["STDN_Date"].ToString();
                stockTransferReceiptEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                stockTransferReceiptEntity.FromBranch = dr["From_Branch_Code"].ToString();
                stockTransferReceiptEntity.BranchCode = dr["To_Branch_Code"].ToString();
                stockTransferReceiptEntity.InvoiceValue = dr["Invoice_Value"].ToString();
                stockTransferReceiptEntity.IGSTValue = dr["GST_Value"].ToString();
                stockTransferReceiptEntity.LRNumber = dr["LR_Number"].ToString();
                stockTransferReceiptEntity.LRDate = dr["LR_Date"].ToString();
                stockTransferReceiptEntity.Carrier = dr["Carrier"].ToString();
                stockTransferReceiptEntity.Destination = dr["Destination"].ToString();
                stockTransferReceiptEntity.RoadPermitNo = dr["Road_Permit_Number"].ToString();
                stockTransferReceiptEntity.RoadPermitDate = dr["Road_Permit_Date"].ToString();

                foreach (DataRow reader in ds.Tables[1].Rows)
                {
                    stockTransferReceiptItem = new StockTransferReceiptItem();

                    stockTransferReceiptItem.SNo = Convert.ToString(reader["Serial_Number"]);                    
                    stockTransferReceiptItem.SupplierLineCode = Convert.ToString(reader["Supplier_name"]);
                    stockTransferReceiptItem.SupplierPartNo = Convert.ToString(reader["Supplier_Part_Number"]);
                    stockTransferReceiptItem.ItemLocation = Convert.ToString(reader["Item_Location"]);
                    stockTransferReceiptItem.ItemCode = Convert.ToString(reader["Item_Code"]);
                    stockTransferReceiptItem.CostPricePerQty = Convert.ToString(reader["CostPricePerQty"]);
                    stockTransferReceiptItem.OriginalReceiptDate = Convert.ToString(reader["Original_Receipt_Date"]);
                    stockTransferReceiptItem.ListPrice = Convert.ToString(reader["List_Price"]);
                    stockTransferReceiptItem.ReceivedQuantity = Convert.ToString(reader["Received_Quantity"]);
                    stockTransferReceiptItem.TotalCostPrice = Convert.ToString(reader["TotalCostPrice"]);
                    stockTransferReceiptItem.OSLSIndicator = Convert.ToString(reader["OS_LS_Indicator"]);
                    stockTransferReceiptItem.GSTPercentage = Convert.ToString(reader["Sales_Tax_Percentage"]);
                    stockTransferReceiptItem.InvoiceNumber = Convert.ToString(reader["Invoice_Number"]);
                    stockTransferReceiptItem.InvoiceDate = Convert.ToString(reader["Invoice_Date"]);
                    stockTransferReceiptItem.ProductGroupCode = Convert.ToString(reader["Product_Group_Code"]);
                    stockTransferReceiptItem.ConsInwardNo = Convert.ToString(reader["Consignment_Inward_Number"]);
                    stockTransferReceiptItem.ConsSerialNo = Convert.ToString(reader["Consignment_Serial_Number"]);
                    stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
                }
            }

            return stockTransferReceiptEntity;
        }

        public int GetSTDNinterStateStatus(string ToBranch, string FromBranch)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select COUNT(distinct State_Code) From Branch_Master where Branch_Code in ('" + ToBranch + "','" + FromBranch + "')";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }

        public int GetSTDNinterStateStatusGST(string ToBranch, string FromBranch)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select COUNT(distinct State_Code) From Branch_Master where Branch_Code in ('" + ToBranch + "','" + FromBranch + "')";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }

        public int GetCOSTinterStateStatus(string ToBranch, string Supplier, string FromBranch)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select Count(Distinct State_Code) from (Select State_Code From Branch_Master where Branch_Code = '" + ToBranch + "' union all Select State_Code From Gst_Supplier_Master where Supplier_Code='" + Supplier + "' and Gst_Supplier_Code ='" + FromBranch + "') a";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }
    }
}
