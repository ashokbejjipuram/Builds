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
    public class GRNEntity
    {
        public string BranchCode { get; set; }
        public string InwardNumber { get; set; }
        public string InwardDate { get; set; }
        public string InwardValue { get; set; }
        public string TransactionTypeCode { get; set; }
        public string SupplierCode { get; set; }
        public string OSLSindicator { get; set; }
        public string HeaderTaxValue { get; set; }
        public string InsValue { get; set; }
        public string DiscValue { get; set; }
        public string PackageStatus { get; set; }
        public string PackageOpenDate { get; set; }
        public string LRTransfer { get; set; }
        public string LocalPurchaseTax { get; set; }
        public string Remarks { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string ClearingAgentNo { get; set; }
        public string CheckPostName { get; set; }
        public string ClearenceDate { get; set; }
        public string ClearenceAmount { get; set; }
        public string RoadPermitNo { get; set; }
        public string RoadPermitDate { get; set; }
        public string WarehouseNo { get; set; }
        public string WarehouseDate { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ApprovalLevel { get; set; }                  
        public List<GRNItem> Items { get; set; }

    }
    public class GRNItem
    {
        public string SNo { get; set; }
        public string SupplierPartNo { get; set; }
        public string POQty { get; set; }
        public string ReceivedQty { get; set; }
        public string InwardQty { get; set; }
        public string AcceptedQty { get; set; }
        public string Indicator { get; set; }
        public string Shortage { get; set; }
        public string ItemLocation { get; set; }        
        public string HandlingCharges { get; set; }
    }

    public class GRNTransactions
    {
        public GRNEntity GetInwardInfoForGRNEntry(string InwardNumber, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetGRNEntryInformation");
			ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@InwardNo", DbType.String, InwardNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            GRNEntity grnEntity = new GRNEntity();
            GRNItem grnItem = null;

            grnEntity.Items = new List<GRNItem>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    grnEntity.BranchCode = dr["Branch_Code"].ToString();
                    grnEntity.InwardNumber = dr["Inward_Number"].ToString();
                    grnEntity.InwardDate = dr["Inward_Date"].ToString();
                    grnEntity.TransactionTypeCode = dr["Transaction_Type_Code"].ToString();
                    grnEntity.SupplierCode = dr["Supplier_Code"].ToString();
                    grnEntity.PackageStatus = dr["Packing_Status"].ToString();
                    grnEntity.PackageOpenDate = dr["Date_Opened"].ToString();
                    grnEntity.LRTransfer = dr["LR_Transfer_Indicator"].ToString();
                    grnEntity.LocalPurchaseTax = dr["Local_Purchase_Tax"].ToString();
                    grnEntity.Remarks = dr["Remarks"].ToString();
                    grnEntity.InvoiceNumber = dr["Invoice_Number"].ToString();
                    grnEntity.InvoiceDate = dr["Invoice_Date"].ToString();
                    grnEntity.ClearingAgentNo = dr["Clearing_Agent_Code"].ToString();
                    grnEntity.CheckPostName = dr["Clearing_Reference"].ToString();
                    grnEntity.ClearenceDate = dr["Clearing_Reference_Date"].ToString();
                    grnEntity.ClearenceAmount = dr["Clearing_Amount"].ToString();
                    grnEntity.WarehouseNo = dr["WarehouseNo"].ToString();
                    grnEntity.WarehouseDate = dr["WarehouseDate"].ToString();
                    grnEntity.OSLSindicator = dr["OS_LS_indicator"].ToString();
                    grnEntity.InwardValue = dr["Inward_Value"].ToString();
                    grnEntity.HeaderTaxValue = dr["HeaderTax_Value"].ToString();
                    grnEntity.InsValue = dr["Ins_Value"].ToString();
                    grnEntity.DiscValue = dr["Disc_Value"].ToString();
                }
            }
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow reader in ds.Tables[1].Rows)
                    {
                        grnItem = new GRNItem();
                        grnItem.SNo = Convert.ToString(reader["Serial_Number"]);
                        grnItem.SupplierPartNo = Convert.ToString(reader["SUPPARTNO"]);
                        grnItem.POQty = Convert.ToString(reader["po_quantity"]);
                        grnItem.ReceivedQty = Convert.ToString(reader["RCVD_Qty"]);
                        grnItem.InwardQty = Convert.ToString(reader["InwardQty"]);
                        grnItem.AcceptedQty = Convert.ToString(reader["AcceptedQty"]);
                        grnItem.Indicator = Convert.ToString(reader["Indicator"]);
                        grnItem.Shortage = Convert.ToString(reader["ShortageQty"]);
                        grnItem.ItemLocation = Convert.ToString(reader["Location_Code"]);
                        grnItem.HandlingCharges = Convert.ToString(reader["HandlingCharges"]);
                        grnEntity.Items.Add(grnItem);
                    }
                }
            }

            return grnEntity;
        }

        public int ClearingAgentExists(string InwardNum, string ClearingAgentCode, string strBranchCode)
        {
            int Cnt = 0;
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetSqlStringCommand("IF EXISTS(select 1 from inward_header WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "' and clearing_agent_code ='" + ClearingAgentCode + "' and Inward_Number<>'" + InwardNum + "') Select 1 Else Select 0");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return Cnt;
        }

        public List<Item> GetInwardEntriesForGRNEDP(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardEntriesListForGRN");
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

        public List<Item> GetInwardEntriesForManager(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardListForManager");
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

        public List<Item> GetInwardEntriesForWareHouse(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetInwardListForWareHouse");
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

        public int UpdateWareHouseGRNEntry(ref GRNEntity grnEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updgrnentry1_WareHouse");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, grnEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@packing_status", DbType.String, grnEntity.PackageStatus.Trim());
                    ImpalDB.AddInParameter(cmd, "@date_opened", DbType.String, grnEntity.PackageOpenDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_transfer_indicator", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, grnEntity.Remarks.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, grnEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, grnEntity.InvoiceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearingAgentNo.Trim()))
                        grnEntity.ClearingAgentNo = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_agent_code", DbType.Int64, grnEntity.ClearingAgentNo.Trim());

                    ImpalDB.AddInParameter(cmd, "@clearing_reference", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@clearing_reference_date", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@clearing_amount", DbType.Decimal, 0);
                    ImpalDB.AddInParameter(cmd, "@RoadPermitNo", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@RoadPermitDate", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@Warehouse_Status", DbType.String, "W");
                    ImpalDB.AddInParameter(cmd, "@Approval_Status", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@Approval_Remarks", DbType.String, "");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    result = ImpalDB.ExecuteNonQuery(cmd);

                    foreach (GRNItem grnItem in grnEntity.Items)
                    {
                        DbCommand cmdItems = ImpalDB.GetStoredProcCommand("usp_Updgrnentry2_WareHouse");
                        ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Inward_Number", DbType.String, grnEntity.InwardNumber);
                        ImpalDB.AddInParameter(cmdItems, "@serial_number", DbType.Int32, Convert.ToInt64(grnItem.SNo.ToString()));

                        if (string.IsNullOrEmpty(grnItem.AcceptedQty.Trim()))
                            grnItem.AcceptedQty = "0";
                        ImpalDB.AddInParameter(cmdItems, "@actual_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.AcceptedQty.ToString()));
                        
                        if (grnEntity.PackageStatus == "G")
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Int16, 0);
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.Shortage));
                        }

                        ImpalDB.AddInParameter(cmdItems, "@location_code", DbType.String, grnItem.ItemLocation);
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_No", DbType.String, grnEntity.WarehouseNo.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_Date", DbType.String, grnEntity.WarehouseDate.ToString());                        
                        cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItems);
                    }

                    grnEntity.ErrorCode = "0";
                    grnEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return result;
        }

        public DataSet GetEinvoicingShortageDebitNoteDetails(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_ShortageDebitNote_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public int UpdateGRNFinalEntryEDP(ref GRNEntity grnEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updgrnentry1_WareHouse");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, grnEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@packing_status", DbType.String, grnEntity.PackageStatus.Trim());
                    ImpalDB.AddInParameter(cmd, "@date_opened", DbType.String, grnEntity.PackageOpenDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_transfer_indicator", DbType.String, grnEntity.LRTransfer.Trim());
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, grnEntity.Remarks.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, grnEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, grnEntity.InvoiceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearingAgentNo.Trim()))
                        grnEntity.ClearingAgentNo = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_agent_code", DbType.Int64, grnEntity.ClearingAgentNo.Trim());

                    ImpalDB.AddInParameter(cmd, "@clearing_reference", DbType.String, grnEntity.CheckPostName.Trim());
                    ImpalDB.AddInParameter(cmd, "@clearing_reference_date", DbType.String, grnEntity.ClearenceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearenceAmount.Trim()))
                        grnEntity.ClearenceAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_amount", DbType.Decimal, Convert.ToDecimal(grnEntity.ClearenceAmount.Trim()));

                    ImpalDB.AddInParameter(cmd, "@RoadPermitNo", DbType.String, grnEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@RoadPermitDate", DbType.String, grnEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Warehouse_Status", DbType.String, "C");
                    ImpalDB.AddInParameter(cmd, "@Approval_Status", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@Approval_Remarks", DbType.String, "");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    result = ImpalDB.ExecuteNonQuery(cmd);

                    foreach (GRNItem grnItem in grnEntity.Items)
                    {
                        DbCommand cmdItems = ImpalDB.GetStoredProcCommand("usp_Updgrnentry2_WareHouse");
                        ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Inward_Number", DbType.String, grnEntity.InwardNumber);
                        ImpalDB.AddInParameter(cmdItems, "@serial_number", DbType.Int32, Convert.ToInt64(grnItem.SNo.ToString()));

                        if (string.IsNullOrEmpty(grnItem.AcceptedQty.Trim()))
                            grnItem.AcceptedQty = "0";
                        ImpalDB.AddInParameter(cmdItems, "@actual_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.AcceptedQty.ToString()));
                        
                        if (grnEntity.PackageStatus == "G")
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Int16, 0);
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.Shortage));
                        }

                        ImpalDB.AddInParameter(cmdItems, "@location_code", DbType.String, grnItem.ItemLocation);
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_No", DbType.String, grnEntity.WarehouseNo.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_Date", DbType.String, grnEntity.WarehouseDate.ToString());                        
                        cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItems);
                    }

                    grnEntity.ErrorCode = "0";
                    grnEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return result;
        }

        public int UpdateGRNFinalEntry(ref GRNEntity grnEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updgrnentry1_New_GST");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, grnEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@packing_status", DbType.String, grnEntity.PackageStatus.Trim());
                    ImpalDB.AddInParameter(cmd, "@date_opened", DbType.String, grnEntity.PackageOpenDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_transfer_indicator", DbType.String, grnEntity.LRTransfer.Trim());
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, grnEntity.Remarks.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, grnEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, grnEntity.InvoiceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearingAgentNo.Trim()))
                        grnEntity.ClearingAgentNo = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_agent_code", DbType.Int64, grnEntity.ClearingAgentNo.Trim());

                    ImpalDB.AddInParameter(cmd, "@clearing_reference", DbType.String, grnEntity.CheckPostName.Trim());
                    ImpalDB.AddInParameter(cmd, "@clearing_reference_date", DbType.String, grnEntity.ClearenceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearenceAmount.Trim()))
                        grnEntity.ClearenceAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_amount", DbType.Decimal, Convert.ToDecimal(grnEntity.ClearenceAmount.Trim()));

                    ImpalDB.AddInParameter(cmd, "@RoadPermitNo", DbType.String, grnEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@RoadPermitDate", DbType.String, grnEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@ApprovalLevel", DbType.String, grnEntity.ApprovalLevel.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    DbCommand cmdItems;

                    foreach (GRNItem grnItem in grnEntity.Items)
                    {
                        cmdItems = ImpalDB.GetStoredProcCommand("usp_Updgrnentry2_New_GST");
                        ImpalDB.AddInParameter(cmdItems, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Inward_Number", DbType.String, grnEntity.InwardNumber);
                        ImpalDB.AddInParameter(cmdItems, "@serial_number", DbType.Int32, Convert.ToInt64(grnItem.SNo.ToString()));

                        if (string.IsNullOrEmpty(grnItem.AcceptedQty.Trim()))
                            grnItem.AcceptedQty = "0";
                        ImpalDB.AddInParameter(cmdItems, "@actual_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.AcceptedQty.ToString()));

                        ImpalDB.AddInParameter(cmdItems, "@os_ls_indicator", DbType.String, grnItem.Indicator);
                        if (grnEntity.PackageStatus == "G")
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Int16, 0);
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmdItems, "@defective_quantity", DbType.Decimal, Convert.ToDecimal(grnItem.Shortage));
                        }

                        ImpalDB.AddInParameter(cmdItems, "@location_code", DbType.String, grnItem.ItemLocation);
                        ImpalDB.AddInParameter(cmdItems, "@list_price", DbType.Decimal, Convert.ToDecimal("0"));
                        ImpalDB.AddInParameter(cmdItems, "@sales_tax_percentage", DbType.Decimal, Convert.ToDecimal("0"));
                        ImpalDB.AddInParameter(cmdItems, "@other_charges", DbType.Decimal, Convert.ToDecimal("0"));
                        ImpalDB.AddInParameter(cmdItems, "@handling_charges_percentage", DbType.Decimal, Convert.ToDecimal(grnItem.HandlingCharges));
                        ImpalDB.AddInParameter(cmdItems, "@rebate", DbType.Decimal, Convert.ToDecimal("0"));
                        ImpalDB.AddInParameter(cmdItems, "@ST_Amt", DbType.Decimal, Convert.ToDecimal("0"));
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_No", DbType.String, grnEntity.WarehouseNo.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@Warehouse_Date", DbType.String, grnEntity.WarehouseDate.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@supplier_Code", DbType.String, grnEntity.SupplierCode.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@transtype", DbType.String, grnEntity.TransactionTypeCode.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@inward_Date", DbType.String, grnEntity.InwardDate.ToString());
                        ImpalDB.AddInParameter(cmdItems, "@inward_Value", DbType.Decimal, Convert.ToDecimal(grnEntity.InwardValue.ToString()));
                        ImpalDB.AddInParameter(cmdItems, "@Invoice_Number", DbType.String, grnEntity.InvoiceNumber.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@Invoice_Date", DbType.String, grnEntity.InvoiceDate.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@date_opened", DbType.String, grnEntity.PackageOpenDate.Trim());
                        ImpalDB.AddInParameter(cmdItems, "@os_ls_indicator1", DbType.String, grnEntity.OSLSindicator.ToString());                        
                        ImpalDB.AddInParameter(cmdItems, "@Header_Tax_Amt", DbType.Decimal, Convert.ToDecimal(grnEntity.HeaderTaxValue.ToString()));
                        ImpalDB.AddInParameter(cmdItems, "@ins_Value", DbType.Decimal, Convert.ToDecimal(grnEntity.InsValue.ToString()));
                        ImpalDB.AddInParameter(cmdItems, "@Disc_Value", DbType.Decimal, Convert.ToDecimal(grnEntity.DiscValue.ToString()));
                        cmdItems.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdItems);
                        cmdItems = null;
                    }

                    if (grnEntity.TransactionTypeCode.Trim() != "171")
                    {
                        DbCommand cmdStatus = ImpalDB.GetStoredProcCommand("usp_inwardQuantity_AllStatus");
                        ImpalDB.AddInParameter(cmdStatus, "@Inward_Number", DbType.String, grnEntity.InwardNumber);
                        ImpalDB.AddInParameter(cmdStatus, "@Branch_code", DbType.String, grnEntity.BranchCode);
                        cmdStatus.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdStatus);
                    }

                    if (Convert.ToDecimal(grnEntity.LocalPurchaseTax) > 0)
                    {
                        DbCommand cmdGlGr1 = ImpalDB.GetStoredProcCommand("usp_addglgr1_GST");
                        ImpalDB.AddInParameter(cmdGlGr1, "@doc_no", DbType.String, grnEntity.InwardNumber);
                        ImpalDB.AddInParameter(cmdGlGr1, "@Branch_Code", DbType.String, grnEntity.BranchCode);
                        cmdGlGr1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdGlGr1);
                    }

                    grnEntity.ErrorCode = "0";
                    grnEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return result;
        }

        public int UpdateGRNFinalEntryRejection(ref GRNEntity grnEntity, string ApprovalRemarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updgrnentry1_WareHouse");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, grnEntity.BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, grnEntity.InwardNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@packing_status", DbType.String, grnEntity.PackageStatus.Trim());
                    ImpalDB.AddInParameter(cmd, "@date_opened", DbType.String, grnEntity.PackageOpenDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@LR_transfer_indicator", DbType.String, grnEntity.LRTransfer.Trim());
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, grnEntity.Remarks.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Number", DbType.String, grnEntity.InvoiceNumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@Invoice_Date", DbType.String, grnEntity.InvoiceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearingAgentNo.Trim()))
                        grnEntity.ClearingAgentNo = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_agent_code", DbType.Int64, grnEntity.ClearingAgentNo.Trim());

                    ImpalDB.AddInParameter(cmd, "@clearing_reference", DbType.String, grnEntity.CheckPostName.Trim());
                    ImpalDB.AddInParameter(cmd, "@clearing_reference_date", DbType.String, grnEntity.ClearenceDate.Trim());

                    if (string.IsNullOrEmpty(grnEntity.ClearenceAmount.Trim()))
                        grnEntity.ClearenceAmount = "0";
                    ImpalDB.AddInParameter(cmd, "@clearing_amount", DbType.Decimal, Convert.ToDecimal(grnEntity.ClearenceAmount.Trim()));

                    ImpalDB.AddInParameter(cmd, "@RoadPermitNo", DbType.String, grnEntity.RoadPermitNo.Trim());
                    ImpalDB.AddInParameter(cmd, "@RoadPermitDate", DbType.String, grnEntity.RoadPermitDate.Trim());
                    ImpalDB.AddInParameter(cmd, "@Warehouse_Status", DbType.String, "P");
                    ImpalDB.AddInParameter(cmd, "@Approval_Status", DbType.String, "R");
                    ImpalDB.AddInParameter(cmd, "@Approval_Remarks", DbType.String, ApprovalRemarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    result = ImpalDB.ExecuteNonQuery(cmd);

                    grnEntity.ErrorCode = "0";
                    grnEntity.ErrorMsg = "";
                    result = 1;

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return result;
        }

        public string GetBranchName(string strBranchCode)
        {
            string strBranchName = string.Empty;

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_name from branch_master WITH (NOLOCK) where branch_code = '" + strBranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    strBranchName = reader[0].ToString();
                }
            }

            return strBranchName;
        }

        public DataSet GetGRNeditlistDetails(string BranchCode, string SupplierCode, string FromDate, string ToDate)
        {

            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_report_GRNeditlist");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, SupplierCode.Trim());
            ImpalDB.AddInParameter(cmd, "@FromDate", DbType.String, FromDate.Trim());
            ImpalDB.AddInParameter(cmd, "@ToDate", DbType.String, ToDate.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }
    }
}
