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
using System.Text.RegularExpressions;

namespace IMPALLibrary.Transactions
{

    public class Branch
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
    }


    public class TranType
    {
        public string TranTypeCode { get; set; }
        public string TranTypeName { get; set; }
    }

    public class STDReqSupplier
    {
        public string SupCode { get; set; }
        public string SupName { get; set; }
    }

    public class RequestNumber
    {
        public string ReqNumber { get; set; }
    }


    public class StockTranHeader
    {
        public string RequestNumber { get; set; }
        public string IndentDate { get; set; }
        public string FromBranchCode { get; set; }
        public string ToBranchCode { get; set; }
        public string TranType { get; set; }
        public string Requestedby { get; set; }
        public string Remarks { get; set; }
        public string RoadPermit { get; set; }
        public string RoadPermitDate { get; set; }
    }


    public class StockTranDetails
    {
        public string SupplierLineCode { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ItemCode { get; set; }
        public string Quantity { get; set; }
        public string ItemRemarks { get; set; }
        public string Status { get; set; }
        public string Indicator { get; set; }
    }


    public class StockTranRequest
    {
        public List<Branch> GetBranchList(string strBranchCode)
        {
            List<Branch> objBranchList = new List<Branch>();

            string sSql = "SELECT BRANCH_CODE, BRANCH_NAME FROM BRANCH_MASTER WHERE BRANCH_CODE <> '" + strBranchCode + "'";

            Database ImpalDB = DataAccess.GetDatabase();

            Branch BranchList = new Branch();
            BranchList.BranchName = string.Empty;
            BranchList.BranchCode = "0";
            objBranchList.Add(BranchList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BranchList = new Branch();
                    BranchList.BranchName = reader[1].ToString();
                    BranchList.BranchCode = reader[0].ToString();
                    objBranchList.Add(BranchList);
                }
            }
            return objBranchList;
        }

        public List<TranType> GetTranType()
        {
            List<TranType> objTranList = new List<TranType>();

            string sSql = "SELECT TRANSACTION_TYPE_CODE, TRANSACTION_TYPE_DESCRIPTION FROM TRANSACTION_TYPE_MASTER WHERE TRANSACTION_TYPE_CODE IN ('031','051','131','151','251')";

            Database ImpalDB = DataAccess.GetDatabase();

            TranType TranList = new TranType();
            TranList.TranTypeName = string.Empty;
            TranList.TranTypeCode = "0";
            objTranList.Add(TranList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TranList = new TranType();
                    TranList.TranTypeName = reader[1].ToString();
                    TranList.TranTypeCode = reader[0].ToString();
                    objTranList.Add(TranList);
                }
            }
            return objTranList;
        }

        public List<STDReqSupplier> GetSupplierList()
        {
            List<STDReqSupplier> objSupplierList = new List<STDReqSupplier>();

            string sSql = "SELECT SUPPLIER_CODE, SUPPLIER_NAME FROM SUPPLIER_MASTER ORDER BY SUPPLIER_NAME";

            Database ImpalDB = DataAccess.GetDatabase();

            STDReqSupplier SupplierList = new STDReqSupplier();
            SupplierList.SupName = string.Empty;
            SupplierList.SupCode = "0";
            objSupplierList.Add(SupplierList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SupplierList = new STDReqSupplier();
                    SupplierList.SupName = reader[1].ToString();
                    SupplierList.SupCode = reader[0].ToString();
                    objSupplierList.Add(SupplierList);
                }
            }
            return objSupplierList;
        }


        public List<RequestNumber> GetRequestNumber(string strBranchCode)
        {
            List<RequestNumber> objRequestNumberList = new List<RequestNumber>();

            string sSql = "SELECT REQUEST_NUMBER FROM STDN_REQUEST_HEADER Where Branch_Code='" + strBranchCode + "' ORDER BY REQUEST_NUMBER Desc";

            Database ImpalDB = DataAccess.GetDatabase();

            RequestNumber RequestNumberList = new RequestNumber();
            RequestNumberList.ReqNumber = string.Empty;

            objRequestNumberList.Add(RequestNumberList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RequestNumberList = new RequestNumber();
                    RequestNumberList.ReqNumber = reader[0].ToString();
                    objRequestNumberList.Add(RequestNumberList);
                }
            }
            return objRequestNumberList;
        }

        public string SubmitStockTransfer(DataTable dtStockTran, StockTranHeader objStockTranReqHeader, string strBranchCode)
        {
            string lstrSql = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string strSTDReqNumber = string.Empty;

            try
            {
                for (int linti = 0; linti <= dtStockTran.Rows.Count - 1; linti++)
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSTDNRequest_New");
					ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    if (string.IsNullOrEmpty(strSTDReqNumber))
                    {
                        ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, objStockTranReqHeader.RequestNumber);
                    }
                    else
                    {
                        ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strSTDReqNumber);
                    }
                    ImpalDB.AddInParameter(cmd, "@From_Branch_Code", DbType.String, objStockTranReqHeader.FromBranchCode);
                    ImpalDB.AddInParameter(cmd, "@To_Branch_Code", DbType.String, objStockTranReqHeader.ToBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Requestedby", DbType.String, objStockTranReqHeader.Requestedby);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, objStockTranReqHeader.Remarks);
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, objStockTranReqHeader.RoadPermit);
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, objStockTranReqHeader.RoadPermitDate);
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, dtStockTran.Rows[linti]["ItemCode"].ToString());
                    ImpalDB.AddInParameter(cmd, "@Item_Quantity", DbType.Decimal, dtStockTran.Rows[linti]["Quantity"]);
                    ImpalDB.AddInParameter(cmd, "@Item_Remarks", DbType.String, dtStockTran.Rows[linti]["Remarks"].ToString());
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, dtStockTran.Rows[linti]["Status"].ToString());
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, objStockTranReqHeader.TranType);
                    ImpalDB.AddOutParameter(cmd, "@OutRequestNumber", DbType.String, 20);

                    if (linti == dtStockTran.Rows.Count - 1)
                    {
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "V");
                    }
                    else
                    {
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, "N");
                    }
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (string.IsNullOrEmpty(strSTDReqNumber))
                        strSTDReqNumber = cmd.Parameters["@OutRequestNumber"].Value.ToString();

                    cmd = null;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return strSTDReqNumber;
        }

        public void UpdateStockTransfer(DataTable dtStockTran, StockTranHeader objStockTranReqHeader, string strBranchCode)
        {
            string lstrSql = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string strSTDReqNumber = string.Empty;
            int iSlNo = 1;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmdHeader = ImpalDB.GetStoredProcCommand("usp_UpdSTDNRequesthead");
                    ImpalDB.AddInParameter(cmdHeader, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmdHeader, "@Request_Number", DbType.String, objStockTranReqHeader.RequestNumber);
                    ImpalDB.AddInParameter(cmdHeader, "@Requestedby", DbType.String, objStockTranReqHeader.Requestedby);
                    ImpalDB.AddInParameter(cmdHeader, "@Remarks", DbType.String, objStockTranReqHeader.Remarks);
                    ImpalDB.AddInParameter(cmdHeader, "@Road_Permit_Number", DbType.String, objStockTranReqHeader.RoadPermit);
                    ImpalDB.AddInParameter(cmdHeader, "@Road_Permit_Date", DbType.String, objStockTranReqHeader.RoadPermitDate);
                    cmdHeader.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdHeader);


                    for (int linti = 0; linti <= dtStockTran.Rows.Count - 1; linti++)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdSTDNRequestdetl");
                        ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, objStockTranReqHeader.RequestNumber);
                        ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.String, iSlNo);
                        ImpalDB.AddInParameter(cmd, "@Item_Quantity", DbType.Decimal, dtStockTran.Rows[linti]["Quantity"]);
                        ImpalDB.AddInParameter(cmd, "@Item_Remarks", DbType.String, dtStockTran.Rows[linti]["Remarks"].ToString());
                        ImpalDB.AddInParameter(cmd, "@Status", DbType.String, dtStockTran.Rows[linti]["Status"].ToString());
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                        iSlNo += 1;
                        cmd = null;
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public StockTranHeader GetStockTransReqHdr(string strRequestNumber, string strBranchCode)
        {
            string lstrSql = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string strSTDReqNumber = string.Empty;

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getSTDNrequesthead");
				ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strRequestNumber);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDB.ExecuteDataSet(cmd);
                StockTranHeader objStkReqHdr = new StockTranHeader();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objStkReqHdr.RequestNumber = ds.Tables[0].Rows[0]["Request_Number"].ToString();
                    objStkReqHdr.IndentDate = ds.Tables[0].Rows[0][1].ToString();
                    objStkReqHdr.FromBranchCode = ds.Tables[0].Rows[0]["From_Branch_Code"].ToString();
                    objStkReqHdr.ToBranchCode = ds.Tables[0].Rows[0]["To_Branch_Code"].ToString();
                    objStkReqHdr.Requestedby = ds.Tables[0].Rows[0]["Requested_By"].ToString();
                    objStkReqHdr.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    objStkReqHdr.RoadPermit = ds.Tables[0].Rows[0]["Road_Permit_Number"].ToString();
                    objStkReqHdr.RoadPermitDate = ds.Tables[0].Rows[0][7].ToString();
                    objStkReqHdr.TranType = ds.Tables[0].Rows[0]["transaction_type_code"].ToString();
                }

                return objStkReqHdr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<StockTranDetails> GetStockTransReqDetails(string strRequestNumber, string strBranchCode)
        {
            string lstrSql = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string strSTDReqNumber = string.Empty;

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getSTDNrequestdetl_New");
				ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Request_Number", DbType.String, strRequestNumber);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDB.ExecuteDataSet(cmd);
                
                List<StockTranDetails> objStkReqItem = new List<StockTranDetails>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    StockTranDetails objStkReqDetails = new StockTranDetails();
                    objStkReqDetails.SupplierLineCode = dr["Supplier_Code"].ToString();
                    objStkReqDetails.ItemCode = dr["Item_Code"].ToString();
                    objStkReqDetails.Quantity = dr["Quantity"].ToString();
                    objStkReqDetails.ItemRemarks = dr["Remarks"].ToString();
                    objStkReqDetails.Status = dr[8].ToString();
                    objStkReqDetails.SupplierPartNumber = dr["Supplier_Part_Number"].ToString();
                    objStkReqItem.Add(objStkReqDetails);
                }

                return objStkReqItem;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public DataSet GetConsStockOutDetails(string SuppCode, string Date, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_consignment_stockout_report");
			ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@supplier_code", DbType.String, SuppCode.Trim());
            ImpalDB.AddInParameter(cmd, "@date", DbType.String, Date.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetStockValueAgingDetails(string BranchCode, string SupplierCode, string ReportType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_StockValue_Aging");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Trim());
            ImpalDB.AddInParameter(cmd, "@ReportType", DbType.String, ReportType.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetWorkSheetStockPatternDetails(string strBranchCode, string SupplierCode)
        {

            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            string sSQL = "select 'z'+Item_Code Item_Code,'z'+Supplier_Part_Number Supplier_Part_Number,Item_Short_Description,Packing_Quantity,Doc_On_Hand,Stock,Po_Qty,To_Order_Qty,Less3mth,Above3mth,Total_Mths,Total_SaleQty,Unit_Cost,Avg_Sales,Cost_value,CostValue,ABC_Percent,ABCFMS_Status,Prev_Month1,Prev_Month2,Prev_Month3,Prev_Month1_sales,Prev_Month2_Sales,Prev_Month3_Sales,Curr_Month,branch_code,Indent_Number,Max_Qty,Min_Qty from Item_Worksheet_ABCFMS_Child where branch_code = '" + strBranchCode + "' and substring(item_code,1,3)='" + SupplierCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }
    }
}