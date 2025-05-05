using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace IMPALLibrary
{
    public class Cancellation
    {
        #region GetOrderDetails
        public List<CancelOrderProp> GetOrderDetails(string QueryType, string SupplierCode, string PartNo, string OrderNo, string strBranchCode)
        {
            List<CancelOrderProp> lstOrder = new List<CancelOrderProp>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            switch (QueryType)
            {
                case "PartNo":
                    sQuery = "SELECT B.PO_NUMBER, B.ITEM_CODE, CONVERT(VARCHAR,A.PO_DATE,103) PO_DATE, ISNULL(C.RECEIVED_QUANTITY,0) " +
                             "AS RECEIVED_QUANTITY, (B.PO_QUANTITY - ISNULL(C.RECEIVED_QUANTITY,0)) AS BAL_QTY,B.PO_QUANTITY, B.SERIAL_NUMBER " +
                             "FROM PURCHASE_ORDER_HEADER A WITH (NOLOCK) INNER JOIN PURCHASE_ORDER_DETAIL B WITH (NOLOCK) ON A.BRANCH_CODE = '" + strBranchCode + "' " +
                             "AND A.SUPPLIER_CODE = '" + SupplierCode + "' AND B.ITEM_CODE ='" + PartNo + "' AND A.PO_NUMBER = B.PO_NUMBER AND ISNULL(B.STATUS,'A') = 'A' AND ISNULL(A.STATUS,'A') ='A' " +
                             "INNER JOIN PURCHASE_ORDER_SCHEDULE C WITH (NOLOCK) ON B.PO_NUMBER = C.PO_NUMBER AND B.SERIAL_NUMBER = C.SERIAL_NUMBER AND ISNULL(C.STATUS,'A') = 'A' " +
                             "INNER JOIN ITEM_MASTER D WITH (NOLOCK) ON B.ITEM_CODE = D.ITEM_CODE ORDER BY PO_DATE DESC";
                    break;
                case "OrderNo":
                    sQuery = "SELECT B.PO_NUMBER, D.SUPPLIER_PART_NUMBER, CONVERT(VARCHAR,B.DATESTAMP,103) PO_DATE, C.RECEIVED_QUANTITY, " +
                             "(B.PO_QUANTITY - ISNULL(C.RECEIVED_QUANTITY,0)) AS BAL_QTY,B.PO_QUANTITY, B.SERIAL_NUMBER " +
                             "FROM PURCHASE_ORDER_HEADER A WITH (NOLOCK) INNER JOIN PURCHASE_ORDER_DETAIL B WITH (NOLOCK) ON A.BRANCH_CODE = '" + strBranchCode + "' " +
                             "AND A.SUPPLIER_CODE = '" + SupplierCode + "' AND A.PO_NUMBER ='" + OrderNo + "' AND A.PO_NUMBER = B.PO_NUMBER AND ISNULL(B.STATUS,'A') = 'A' AND ISNULL(A.STATUS,'A') ='A' " +
                             "INNER JOIN PURCHASE_ORDER_SCHEDULE C WITH (NOLOCK) ON B.PO_NUMBER = C.PO_NUMBER AND B.SERIAL_NUMBER = C.SERIAL_NUMBER AND ISNULL(C.STATUS,'A') = 'A' " +
                             "INNER JOIN ITEM_MASTER D WITH (NOLOCK) ON B.ITEM_CODE = D.ITEM_CODE ORDER BY PO_DATE DESC";
                    break;
                case "Month":
                    sQuery = "SELECT B.PO_NUMBER, D.SUPPLIER_PART_NUMBER, CONVERT(VARCHAR,B.DATESTAMP,103) PO_DATE, C.RECEIVED_QUANTITY, " +
                             "(B.PO_QUANTITY - ISNULL(C.RECEIVED_QUANTITY,0)) AS BAL_QTY,B.PO_QUANTITY, B.SERIAL_NUMBER " +
                             "FROM PURCHASE_ORDER_HEADER A WITH (NOLOCK) INNER JOIN PURCHASE_ORDER_DETAIL B WITH (NOLOCK) ON A.BRANCH_CODE = '" + strBranchCode + "' " +
                             "AND A.SUPPLIER_CODE = '" + SupplierCode + "' AND A.PO_NUMBER = B.PO_NUMBER AND ISNULL(B.STATUS,'A') = 'A' AND ISNULL(A.STATUS,'A') ='A' " +
                             "INNER JOIN PURCHASE_ORDER_SCHEDULE C WITH (NOLOCK) ON B.PO_NUMBER = C.PO_NUMBER AND B.SERIAL_NUMBER = C.SERIAL_NUMBER AND ISNULL(C.STATUS,'A') = 'A' " +
                             "INNER JOIN ITEM_MASTER D WITH (NOLOCK) ON B.ITEM_CODE = D.ITEM_CODE ORDER BY PO_DATE DESC";
                    break;
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CancelOrderProp oProp = new CancelOrderProp();
                    oProp.OrderNum = reader["PO_NUMBER"].ToString();
                    if (QueryType.Equals("PartNo"))
                        oProp.PartNum = reader["ITEM_CODE"].ToString();
                    else
                        oProp.PartNum = reader["SUPPLIER_PART_NUMBER"].ToString();
                    oProp.OrderDate = reader["PO_DATE"].ToString();
                    oProp.ReceivedQty = reader["RECEIVED_QUANTITY"].ToString();
                    oProp.BalanceQty = reader["BAL_QTY"].ToString();
                    oProp.OrderQty = reader["PO_QUANTITY"].ToString();
                    oProp.SerialNum = reader["SERIAL_NUMBER"].ToString();
                    lstOrder.Add(oProp);
                }
            }
            return lstOrder;
        }
        #endregion

        #region GetPartNo
        public List<CancelOrderProp> GetPartNo(string SupplierCode, string BranchCode)
        {
            List<CancelOrderProp> lstOrder = new List<CancelOrderProp>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            sQuery = "Select distinct i.Supplier_Part_Number, i.Item_Code FROM Purchase_Order_Header P1 WITH (NOLOCK) INNER JOIN Purchase_Order_Detail P2 WITH (NOLOCK) On p1.Branch_Code = '" + BranchCode + "' AND ISNULL(p1.Status,'A') = 'A' AND p1.Supplier_Code ='" + SupplierCode + "' " +
                     "AND p1.Transaction_Type_Code in ('201','451') and p1.PO_Number = p2.PO_Number INNER JOIN Item_Master i WITH(NOLOCK) on p2.Item_Code = i.Item_Code Order By i.Supplier_Part_Number";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CancelOrderProp oProp = new CancelOrderProp();
                    oProp.PartNum = reader["SUPPLIER_PART_NUMBER"].ToString();
                    oProp.ItemCode = reader["ITEM_CODE"].ToString();
                    lstOrder.Add(oProp);
                }
            }
            return lstOrder;
        }
        #endregion

        #region GetOrderNo
        public List<CancelOrderProp> GetOrderNo(string SupplierCode, string BranchCode)
        {
            List<CancelOrderProp> lstOrder = new List<CancelOrderProp>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            sQuery = "SELECT PO_NUMBER FROM PURCHASE_ORDER_HEADER WITH (NOLOCK) WHERE Branch_Code = '" + BranchCode + "' AND ISNULL(STATUS,'A') = 'A' AND SUPPLIER_CODE ='" + SupplierCode + "' AND TRANSACTION_TYPE_CODE in ('201','451') " +
                     "AND ISNULL(Reference_Number,'')<>'SUPPLIMENTARY ORDER FOR AUTO GRN' AND ISNULL(Approval_Reference_Number,'')<> 'SUPPLIMENTARY ORDER FOR AUTO GRN' ORDER BY PO_NUMBER DESC";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CancelOrderProp oProp = new CancelOrderProp();
                    oProp.OrderNum = reader["PO_NUMBER"].ToString();
                    lstOrder.Add(oProp);
                }
            }
            return lstOrder;
        }
        #endregion

        #region UpdateOrderDetails
        public Int32 UpdateOrderDetails(List<CancelOrderProp> OrderList)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int intCount = 0;
            for (int i = 0; i < OrderList.Count; i++)
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_ABCSTATUSUPDATE");
                ImpalDB.AddInParameter(cmd, "@Po_Number", DbType.String, OrderList[i].OrderNum);
                ImpalDB.AddInParameter(cmd, "@item_code", DbType.String, OrderList[i].PartNum);
                ImpalDB.AddInParameter(cmd, "@Po_Qty", DbType.Int16, Convert.ToInt16(OrderList[i].OrderQty));
                ImpalDB.AddInParameter(cmd, "@Recd_Qty", DbType.Int16, Convert.ToInt16(OrderList[i].ReceivedQty));
                ImpalDB.AddInParameter(cmd, "@Bal_Qty", DbType.Int16, Convert.ToInt16(OrderList[i].BalanceQty));
                ImpalDB.AddInParameter(cmd, "@Sr_Number", DbType.Int16, Convert.ToInt16(OrderList[i].SerialNum));
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Int32 intRowAffected = ImpalDB.ExecuteNonQuery(cmd);
                if (intRowAffected > 0)
                    intCount++;
            }
            return intCount;
        }
        #endregion
    }

    public class CancelOrderProp
    {
        public CancelOrderProp() { }

        private string _SerialNum;
        public string SerialNum
        {
            get { return _SerialNum; }
            set { _SerialNum = value; }
        }

        private string _ItemCode;
        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        private string _OrderNum;
        public string OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }

        private string _PartNum;
        public string PartNum
        {
            get { return _PartNum; }
            set { _PartNum = value; }
        }

        private string _OrderDate;
        public string OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        private string _ReceivedQty;
        public string ReceivedQty
        {
            get { return _ReceivedQty; }
            set { _ReceivedQty = value; }
        }

        private string _BalanceQty;
        public string BalanceQty
        {
            get { return _BalanceQty; }
            set { _BalanceQty = value; }
        }

        private string _OrderQty;
        public string OrderQty
        {
            get { return _OrderQty; }
            set { _OrderQty = value; }
        }
    }
}
