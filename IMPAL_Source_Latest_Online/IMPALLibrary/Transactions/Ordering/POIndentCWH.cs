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
using IMPALLibrary.Masters;

namespace IMPALLibrary.Transactions
{
    public class POIndentCWH
    {
        public string IndentNumber { get; set; }
        public string IndentType { get; set; }

        public List<POIndentDetail> Items { get; set; }
    }

    public class POIndentDetail
    {
        public string PartNumber { get; set; }
        public string ItemDesc { get; set; }
        public string StockOnHand { get; set; }
        public string PendingOrderQty { get; set; }
        public string DocOnHand { get; set; }
        public string AvgSales { get; set; }
        public string CurMonthSales { get; set; }
        public string ToOrderQty { get; set; }
        public string PackQty { get; set; }
        public string PreviousReqQty { get; set; }
        public string AcceptedQty { get; set; }
        public string ItemCode { get; set; }
        public string VehTypeDesc { get; set; }
        public string ToOrderQtyAddl { get; set; }
        public string SurplusQty { get; set; }
        public string ExtraQty { get; set; }
        public string Indicator { get; set; }
        public string SerialNumber { get; set; }
    }
    public class IndentDetails
    {
        public string POIndent { get; set; }
        public string IndentNumber { get; set; }
    }

    public class ClsIndentNumber
    {
        public ClsIndentNumber(string strIndentNumber)
        {
            IndentNumber = strIndentNumber;
        }
        public string IndentNumber { get; set; }
    }

    public class POIndentCWHTran
    {
        public const string ABCFMSTBL = "V_ITEMWORKSHEET_ABCFMS_VEHICLE";
        public const string NILABCFMSTBL = "V_ITEMWORKSHEET_ABCFMS_NIL_VEHICLE";
        public const string ABCFMSTBLSUBBRCH = "V_ITEMWORKSHEET_ABCFMS_VEHICLE_SUBBRANCH";
        public const string NILABCFMSTBLSUBBRCH = "V_ITEMWORKSHEET_ABCFMS_NIL_VEHICLE_SUBBRANCH";
        public const string ABCFMSTBLDPO = "V_ITEMWORKSHEET_ABCFMS_DPO_VEHICLE";
        public const string DUMMYABCFMS = "DUMMY1_ABCFMS";
        public const string DUMMYABCFMSNIL = "DUMMY1_ABCFMS_NIL";
        public const string ITEM_WORKSHEET_ABCFMS = "ITEM_WORKSHEET_ABCFMS";
        public const string ITEM_WORKSHEET_ABCFMS_NIL = "ITEM_WORKSHEET_ABCFMS_NIL";
        public const string ITEM_WORKSHEET_DPO = "Worksheet_Temp_DPO";
        public const string ITEM_WORKSHEET_EPO = "Item_WorkSheet_EPO";

        //To Get Indent Numbers based on Indent Type
        #region GetIndentNumbers
        public List<ClsIndentNumber> GetIndentNumber(string strIndentType, string strBranchCode)
        {
            List<ClsIndentNumber> IndentNumber = new List<ClsIndentNumber>();
            string sSQL = default(string);

            Database ImpalDB = DataAccess.GetDatabase();
            switch (strIndentType)
            {
                case "SFFSN":
                    sSQL = "select po_number from Purchase_Order_Header WITH (NOLOCK) where isnull(status,'A') = 'A'"
                           + " and Branch_Code = '" + strBranchCode + "' and Reference_Number='SFFMS'" //and Supplier_Code like '41%'" /*substring(Reference_Number,1,3) in ('WRK','ABC','NIL') */ 
                           + " and PO_Indent_Date is Null and PO_Date is Null order by indent_Date desc";
                    break;
                case "NILABCFMS":
                    sSQL = "select po_number, Reference_Number from Purchase_Order_Header WITH (NOLOCK) where isnull(status,'A') = 'A'"
                           + " and Branch_Code = '" + strBranchCode + "' and Reference_Number='NILABCFMS'"
                           + " and PO_Value is null and PO_List_Value is null and PO_Indent_Date is Null and PO_Date is Null order by indent_Date desc";
                    break;
                case "ABCFMS":
                    sSQL = "select po_number, Reference_Number  from Purchase_Order_Header WITH (NOLOCK) where isnull(status,'A') = 'A'"
                           + " and Branch_Code = '" + strBranchCode + "' and Reference_Number='ABCFMS'"
                           + " and PO_Value is null and PO_List_Value is null and PO_Indent_Date is Null and PO_Date is Null order by indent_Date desc";
                    break;
                case "SCHEME":
                    sSQL = "select po_number, Reference_Number from Purchase_Order_Header WITH (NOLOCK) where isnull(status,'A') = 'A'"
                           + " and Branch_Code = '" + strBranchCode + "' and Reference_Number='SCHEME' "
                           + " and PO_Indent_Date is Null and PO_Date is Null order by indent_Date desc";
                    break;
                case "DPO":
                    sSQL = "select po_number, Reference_Number from Purchase_Order_Header WITH (NOLOCK) where isnull(status,'A') = 'A'"
                           + " and Branch_Code = '" + strBranchCode + "' and Reference_Number='WORKSHEET-DPO'"
                           + " and PO_Value is null and PO_List_Value is null and PO_Indent_Date is Null and PO_Date is Null order by indent_Date desc";
                    break;
            } IndentNumber.Add(new ClsIndentNumber(""));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IndentNumber.Add(new ClsIndentNumber(reader[0].ToString()));
                }
            }

            return IndentNumber;
        }

        public List<ClsIndentNumber> GetIndentNumberEPO(string strIndentType, string strBranchCode)
        {
            List<ClsIndentNumber> IndentNumber = new List<ClsIndentNumber>();
            string sSQL = default(string);

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndentOrders_EPO");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Indent_Type", DbType.String, strIndentType.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            IndentNumber.Add(new ClsIndentNumber(""));
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IndentNumber.Add(new ClsIndentNumber(reader[0].ToString()));
                }
            }

            return IndentNumber;
        }
        #endregion

        #region Get Indent numbers
        public List<IndentDetails> GetIndentList(string strBranchCode)
        {
            List<IndentDetails> objList = new List<IndentDetails>();
            string[] sIndentNo;

            string SqlIndentQry = "SELECT PO_NUMBER FROM PURCHASE_ORDER_HEADER WHERE UPPER(ISNULL(STATUS,'A')) = 'A'"
                                    + " AND BRANCH_CODE = '" + strBranchCode + "' AND SUBSTRING(REFERENCE_NUMBER,1,3) IN ('WRK','ABC','NIL')"
                                    + " AND PO_INDENT_DATE IS NULL AND PO_DATE IS NULL AND PO_NUMBER IN (SELECT DISTINCT(INDENT_NUMBER)"
                                    + " FROM ITEM_WORKSHEET_ABCFMS UNION ALL  SELECT DISTINCT(INDENT_NUMBER)"
                                    + " FROM ITEM_WORKSHEET_ABCFMS_NIL) ORDER BY INDENT_DATE DESC";

            Database ImpalDB = DataAccess.GetDatabase();

            IndentDetails IndentList = new IndentDetails();
            IndentList.POIndent = "";
            IndentList.IndentNumber = "0";
            objList.Add(IndentList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SqlIndentQry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IndentList = new IndentDetails();
                    IndentList.POIndent = reader[0].ToString();
                    sIndentNo = Regex.Split(reader[0].ToString(), "/");
                    IndentList.IndentNumber = sIndentNo[1].ToString();
                    objList.Add(IndentList);
                }
            }

            return objList;
        }
        #endregion

        #region Get FileName
        public string GetIndentExcelFileName(string strBranchCode, string strPONumber, string sIndentType)
        {
            string sIndentFileName = "";
            string sSqlIndentTbl = string.Empty;

            if (sIndentType == "ABCFMS")
            {
                sSqlIndentTbl = ITEM_WORKSHEET_ABCFMS;
            }
            else if (sIndentType == "NILABCFMS")
            {
                sSqlIndentTbl = ITEM_WORKSHEET_ABCFMS_NIL;
            }
            else if (sIndentType == "DPO")
            {
                sSqlIndentTbl = ITEM_WORKSHEET_DPO;
            }

            string SqlIndentQry = "SELECT DISTINCT ExcelFileName FROM " + sSqlIndentTbl + " WHERE BRANCH_CODE = '" + strBranchCode + "' AND INDENT_NUMBER = '" + strPONumber + "'";
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SqlIndentQry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    sIndentFileName = ImpalDB.ExecuteScalar(cmd).ToString();
                }
            }

            return sIndentFileName;
        }
        #endregion

        #region Update AcceptedQty
        public string UpdAcceptedQty(string strBranchCode, string strPONumber, string filePath, string fileName, string sIndentType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            string sIndentFileName = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_UpdTempWorkSheetExcel");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strPONumber.Trim());
                    ImpalDB.AddInParameter(cmd, "@FilePath", DbType.String, filePath.Trim());
                    ImpalDB.AddInParameter(cmd, "@FileName", DbType.String, fileName.Trim());
                    ImpalDB.AddInParameter(cmd, "@IndentType", DbType.String, sIndentType.Trim());
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    sIndentFileName = ImpalDB.ExecuteScalar(cmd).ToString();

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return sIndentFileName;
        }
        #endregion

        #region Get Indent items
        public List<POIndentDetail> ListIndentItems(string sIndentNo, string sIndentType, string strBranchCode)
        {
            List<POIndentDetail> objPartItems = new List<POIndentDetail>();
            string SqlIndentQry = string.Empty;
            string sSqlIndentTbl = string.Empty;

            if (sIndentType != "0")
            {
                if (sIndentType == "DPO")
                {
                    //sSqlIndentTbl = NILABCFMSTBL;
                    sSqlIndentTbl = ABCFMSTBLDPO;

                    SqlIndentQry = "SELECT SUPPLIER_PART_NUMBER, ITEM_SHORT_DESCRIPTION, STOCK, PO_QTY AS PENDINGORDER,"
                                 + " DOC_ON_HAND AS DOCONHAND, AVG_SALES AS AVERAGESALES, CURR_MONTH AS CURRENTMTHSALES,"
                                 + " CASE WHEN TO_ORDER_QTY<=0 THEN 0 ELSE TO_ORDER_QTY END TO_ORDER_QTY, PACKING_QUANTITY,ACCEPTED_QTY AS ACCEPTED,ITEM_CODE,"
                                 + " ABCFMS_STATUS FROM " + sSqlIndentTbl + " WHERE BRANCH_CODE='" + strBranchCode + "' AND INDENT_NUMBER = '" + sIndentNo + "'"
                                 + " ORDER BY SUPPLIER_PART_NUMBER";
                }
                else
                {
                    if (sIndentType == "ABCFMS")
                    {
                        //sSqlIndentTbl = ABCFMSTBL;
                        sSqlIndentTbl = ITEM_WORKSHEET_ABCFMS;
                    }
                    else if (sIndentType == "NILABCFMS")
                    {
                        //sSqlIndentTbl = NILABCFMSTBL;
                        sSqlIndentTbl = ITEM_WORKSHEET_ABCFMS_NIL;
                    }

                    SqlIndentQry = "SELECT SUPPLIER_PART_NUMBER, ITEM_SHORT_DESCRIPTION, STOCK, PO_QTY AS PENDINGORDER,"
                                 + " DOC_ON_HAND AS DOCONHAND, AVG_SALES AS AVERAGESALES, CURR_MONTH AS CURRENTMTHSALES,"
                                 + " CASE WHEN TO_ORDER_QTY<=0 THEN 0 ELSE TO_ORDER_QTY END TO_ORDER_QTY, PACKING_QUANTITY,ACCEPTED_QTY AS ACCEPTED,ITEM_CODE,"
                                 + " ABCFMS_STATUS FROM " + sSqlIndentTbl + " WHERE BRANCH_CODE='" + strBranchCode + "' AND INDENT_NUMBER = '" + sIndentNo + "'"
                                 + " AND (TO_ORDER_QTY > 0 OR STOCK > 0 OR PO_QTY>0 OR AVG_SALES>0 OR CURR_MONTH > 0) ORDER BY SUPPLIER_PART_NUMBER";
                }

                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetSqlStringCommand(SqlIndentQry);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        POIndentDetail objPOIndent = new POIndentDetail();

                        objPOIndent.PartNumber = reader["SUPPLIER_PART_NUMBER"].ToString();
                        objPOIndent.ItemDesc = reader["ITEM_SHORT_DESCRIPTION"].ToString();
                        objPOIndent.StockOnHand = reader["STOCK"].ToString();
                        objPOIndent.PendingOrderQty = reader["PENDINGORDER"].ToString();
                        objPOIndent.DocOnHand = reader["DOCONHAND"].ToString();
                        objPOIndent.AvgSales = reader["AVERAGESALES"].ToString();
                        objPOIndent.CurMonthSales = reader["CURRENTMTHSALES"].ToString();
                        if (reader["ABCFMS_STATUS"].ToString() == "AF" || reader["ABCFMS_STATUS"].ToString() == "BF" || reader["ABCFMS_STATUS"].ToString() == "CF")
                        {
                            objPOIndent.ToOrderQty = reader["TO_ORDER_QTY"].ToString();
                        }
                        else if (reader["ABCFMS_STATUS"].ToString() == "AM" || reader["ABCFMS_STATUS"].ToString() == "BM" || reader["ABCFMS_STATUS"].ToString() == "CM")
                        {
                            double dToOrderQty = 0;
                            dToOrderQty = Convert.ToDouble(reader["TO_ORDER_QTY"].ToString());
                            dToOrderQty = Math.Round((dToOrderQty * 0.75), 0);
                            objPOIndent.ToOrderQty = dToOrderQty.ToString();
                        }
                        else if (reader["ABCFMS_STATUS"].ToString() == "AS" || reader["ABCFMS_STATUS"].ToString() == "BS" || reader["ABCFMS_STATUS"].ToString() == "CS")
                        {
                            double dToOrderQty = 0;
                            dToOrderQty = Convert.ToDouble(reader["TO_ORDER_QTY"].ToString());
                            dToOrderQty = Math.Round((dToOrderQty * 0.51), 0);
                            objPOIndent.ToOrderQty = dToOrderQty.ToString();
                        }

                        objPOIndent.PackQty = reader["PACKING_QUANTITY"].ToString();
                        objPOIndent.AcceptedQty = reader["ACCEPTED"].ToString();
                        objPOIndent.ItemCode = reader["ITEM_CODE"].ToString();
                        objPOIndent.ToOrderQtyAddl = "0";

                        objPartItems.Add(objPOIndent);
                    }
                }
            }

            return objPartItems;
        }

        public List<POIndentDetail> ListIndentItemsExtraPurchase(string sIndentNo, string sIndentType, string strBranchCode, string strItemCode)
        {
            List<POIndentDetail> objPartItems = new List<POIndentDetail>();
            string SqlIndentQry = string.Empty;
            string sSqlIndentTbl = string.Empty;

            if (sIndentType != "0")
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetEPOworksheet_PartNumber_Details");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, sIndentNo.Trim());
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        POIndentDetail objPOIndent = new POIndentDetail();

                        objPOIndent.PartNumber = reader["Supplier_Part_Number"].ToString();
                        objPOIndent.ItemDesc = reader["Item_Short_Description"].ToString();
                        objPOIndent.VehTypeDesc = reader["Vehicle_Type"].ToString();
                        objPOIndent.PackQty = reader["Packing_Quantity"].ToString();
                        objPOIndent.PreviousReqQty = reader["Previous_Req_Quantity"].ToString();
                        objPOIndent.PendingOrderQty = reader["PO_Quantity"].ToString();                        
                        objPOIndent.StockOnHand = reader["Stock"].ToString();
                        objPOIndent.DocOnHand = reader["Doc_On_Hand"].ToString();
                        objPOIndent.AvgSales = reader["Avg_Sales"].ToString();
                        objPOIndent.CurMonthSales = reader["Curr_Month"].ToString();
                        objPOIndent.ItemCode = reader["Item_Code"].ToString();
                        objPOIndent.ExtraQty = "0";
                        objPOIndent.SurplusQty = reader["Surplus_Qty"].ToString();

                        objPartItems.Add(objPOIndent);
                    }
                }
            }

            return objPartItems;
        }

        public List<POIndentDetail> ListDPOitemsExtraPurchase(string sIndentNo, string sIndentType, string strBranchCode, string strItemCode)
        {
            List<POIndentDetail> objPartItems = new List<POIndentDetail>();
            string SqlIndentQry = string.Empty;
            string sSqlIndentTbl = string.Empty;

            if (sIndentType != "0")
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDPO_PartNumber_Details_EPO");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, sIndentNo.Trim());
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        POIndentDetail objPOIndent = new POIndentDetail();

                        objPOIndent.PartNumber = reader["Supplier_Part_Number"].ToString();
                        objPOIndent.ItemDesc = reader["Item_Short_Description"].ToString();
                        objPOIndent.VehTypeDesc = reader["Vehicle_Type"].ToString();
                        objPOIndent.PackQty = reader["Packing_Quantity"].ToString();                        
                        objPOIndent.PreviousReqQty = reader["Previous_Req_Quantity"].ToString();
                        objPOIndent.PendingOrderQty = reader["PO_Quantity"].ToString();
                        objPOIndent.StockOnHand = reader["Stock"].ToString();
                        objPOIndent.DocOnHand = reader["Doc_On_Hand"].ToString();
                        objPOIndent.AvgSales = reader["Avg_Sales"].ToString();
                        objPOIndent.CurMonthSales = reader["Curr_Month"].ToString();
                        objPOIndent.ItemCode = reader["Item_Code"].ToString();
                        objPOIndent.SurplusQty = reader["Surplus_Qty"].ToString();
                        objPOIndent.ExtraQty = "0";

                        objPartItems.Add(objPOIndent);
                    }
                }
            }

            return objPartItems;
        }

        public List<ItemMaster> GetDPOSupplierPartNumberEPO(string strBranch, string strSupplier, string strPartNumber, string strIndentNumber, string strEPOtype)
        {
            List<ItemMaster> code = new List<ItemMaster>();
            code.Add(new ItemMaster("", ""));

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDPO_PartNumbers_EPO");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSupplier);
            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, strPartNumber);
            ImpalDB.AddInParameter(cmd, "@Indent_Number", DbType.String, strIndentNumber);
            ImpalDB.AddInParameter(cmd, "@EPO_Type", DbType.String, strEPOtype);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    code.Add(new ItemMaster(reader["item_code"].ToString(), reader["Supplier_Part_Number"].ToString()));
                }
            }

            return code;
        }

        #endregion

        #region Get Indent items
        public List<POIndentDetail> ListIndentItemsSubBranch(string sIndentNo, string sIndentType, string SupplierCode, string strBranchCode)
        {
            List<POIndentDetail> objPartItems = new List<POIndentDetail>();
            string SqlIndentQry = string.Empty;
            string sSqlIndentTbl = string.Empty;
            string sSqlIndentTbl1 = string.Empty;

            if (sIndentType != "0")
            {
                if (sIndentType != "SCHEME")
                {
                    if (sIndentType != "NILABCFMS")
                    {
                        sSqlIndentTbl = ABCFMSTBL;
                        sSqlIndentTbl1 = ABCFMSTBLSUBBRCH;
                    }
                    else if (sIndentType == "NILABCFMS")
                    {
                        sSqlIndentTbl = NILABCFMSTBL;
                        sSqlIndentTbl1 = NILABCFMSTBLSUBBRCH;
                    }

                    SqlIndentQry = "SELECT case when v1.SUPPLIER_PART_NUMBER is null then v2.SUPPLIER_PART_NUMBER"
                                 + " else v1.SUPPLIER_PART_NUMBER end SUPPLIER_PART_NUMBER,"
                                 + " case when v1.ITEM_SHORT_DESCRIPTION is null then v2.ITEM_SHORT_DESCRIPTION"
                                 + " else v1.ITEM_SHORT_DESCRIPTION end ITEM_SHORT_DESCRIPTION,"
                                 + " case when v1.STOCK is null then v2.STOCK else v1.STOCK end STOCK,"
                                 + " case when v1.PO_QTY is null then v2.PO_QTY else v1.PO_QTY end AS PendINGORDER,"
                                 + " case when v1.DOC_ON_HAND is null then v2.DOC_ON_HAND else v1.DOC_ON_HAND end AS DOCONHAND,"
                                 + " case when v1.AVG_SALES is null then v2.AVG_SALES else v1.AVG_SALES end AS AVERAGESALES,"
                                 + " case when v1.CURR_MONTH is null then v2.CURR_MONTH else v1.CURR_MONTH end AS CURRENTMTHSALES,"
                                 + " case when ISNULL(v1.TO_ORDER_QTY,0)<0 then 0 else ISNULL(v1.TO_ORDER_QTY,0) end TO_ORDER_QTY,"
                                 + " case when v1.PACKING_QUANTITY is null then v2.PACKING_QUANTITY else v1.PACKING_QUANTITY end PACKING_QUANTITY,"
                                 + " 0 AS ACCEPTED,case when v1.VEHICLE_TYPE_DESCRIPTION is null then v2.VEHICLE_TYPE_DESCRIPTION"
                                 + " else v1.VEHICLE_TYPE_DESCRIPTION end VEHICLE_TYPE_DESCRIPTION,"
                                 + " case when v1.ITEM_CODE is null then v2.ITEM_CODE else v1.ITEM_CODE end ITEM_CODE,"
                                 + " case when v1.ABCFMS_STATUS is null then v2.ABCFMS_STATUS else v1.ABCFMS_STATUS end ABCFMS_STATUS,"
                                 + " case when ISNULL(v2.TO_ORDER_QTY,0)<0 then 0 else ISNULL(v2.TO_ORDER_QTY,0) end TO_ORDER_QTY_ADDL"
                                 + " FROM " + sSqlIndentTbl + " v1 FULL OUTER JOIN " + sSqlIndentTbl1 + " v2"
                                 + " ON v1.Branch_Code = '" + strBranchCode + "' AND v1.Branch_Code=v2.Branch_Code AND v1.INDENT_NUMBER = '" + sIndentNo + "' AND"
                                 + " SUBSTRING(v1.ITEM_CODE,1,3)= SUBSTRING(v2.ITEM_CODE,1,3) AND v1.Supplier_Part_Number=v2.Supplier_Part_Number"
                                 + " WHERE (v1.TO_ORDER_QTY > 0 OR v2.TO_ORDER_QTY > 0)"
                                 + " and (SUBSTRING(v1.Item_Code,1,3)='" + SupplierCode + "' or SUBSTRING(v2.Item_Code,1,3)='" + SupplierCode + "')"
                                 + " ORDER BY case when v1.VEHICLE_TYPE_DESCRIPTION is null then v2.VEHICLE_TYPE_DESCRIPTION else v1.VEHICLE_TYPE_DESCRIPTION end,"
                                 + " case when v1.SUPPLIER_PART_NUMBER is null then v2.SUPPLIER_PART_NUMBER else v1.SUPPLIER_PART_NUMBER end";
                }
                else
                {
                    sSqlIndentTbl = ABCFMSTBL;

                    SqlIndentQry = "SELECT case when v1.SUPPLIER_PART_NUMBER is null then v2.SUPPLIER_PART_NUMBER"
                                 + " else v1.SUPPLIER_PART_NUMBER end SUPPLIER_PART_NUMBER,"
                                 + " case when v1.ITEM_SHORT_DESCRIPTION is null then v2.ITEM_SHORT_DESCRIPTION"
                                 + " else v1.ITEM_SHORT_DESCRIPTION end ITEM_SHORT_DESCRIPTION,"
                                 + " case when v1.STOCK is null then v2.STOCK else v1.STOCK end STOCK,"
                                 + " case when v1.PO_QTY is null then v2.PO_QTY else v1.PO_QTY end AS PendINGORDER,"
                                 + " case when v1.DOC_ON_HAND is null then v2.DOC_ON_HAND else v1.DOC_ON_HAND end AS DOCONHAND,"
                                 + " case when v1.AVG_SALES is null then v2.AVG_SALES else v1.AVG_SALES end AS AVERAGESALES,"
                                 + " case when v1.CURR_MONTH is null then v2.CURR_MONTH else v1.CURR_MONTH end AS CURRENTMTHSALES,"
                                 + " case when ISNULL(v1.TO_ORDER_QTY,0)<0 then 0 else ISNULL(v1.TO_ORDER_QTY,0) end TO_ORDER_QTY,"
                                 + " case when v1.PACKING_QUANTITY is null then v2.PACKING_QUANTITY else v1.PACKING_QUANTITY end PACKING_QUANTITY,"
                                 + " 0 AS ACCEPTED,case when v1.VEHICLE_TYPE_DESCRIPTION is null then v2.VEHICLE_TYPE_DESCRIPTION"
                                 + " else v1.VEHICLE_TYPE_DESCRIPTION end VEHICLE_TYPE_DESCRIPTION,"
                                 + " case when v1.ITEM_CODE is null then v2.ITEM_CODE else v1.ITEM_CODE end ITEM_CODE,"
                                 + " case when v1.ABCFMS_STATUS is null then v2.ABCFMS_STATUS else v1.ABCFMS_STATUS end ABCFMS_STATUS,"
                                 + " case when ISNULL(v2.TO_ORDER_QTY,0)<0 then 0 else ISNULL(v2.TO_ORDER_QTY,0) end TO_ORDER_QTY_ADDL"
                                 + " FROM " + sSqlIndentTbl + " v1 FULL OUTER JOIN " + sSqlIndentTbl1 + " v2"
                                 + " ON v1.Branch_Code = '" + strBranchCode + "' AND v1.Branch_Code=v2.Branch_Code AND v1.INDENT_NUMBER = '" + sIndentNo + "' AND"
                                 + " SUBSTRING(v1.ITEM_CODE,1,3)= SUBSTRING(v2.ITEM_CODE,1,3) AND v1.Supplier_Part_Number=v2.Supplier_Part_Number"
                                 + " WHERE (SUBSTRING(v1.Item_Code,1,3)='" + SupplierCode + "' or SUBSTRING(v2.Item_Code,1,3)='" + SupplierCode + "')"
                                 + " ORDER BY case when v1.VEHICLE_TYPE_DESCRIPTION is null then v2.VEHICLE_TYPE_DESCRIPTION else v1.VEHICLE_TYPE_DESCRIPTION end,"
                                 + " case when v1.SUPPLIER_PART_NUMBER is null then v2.SUPPLIER_PART_NUMBER else v1.SUPPLIER_PART_NUMBER end";
                }

                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetSqlStringCommand(SqlIndentQry);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        POIndentDetail objPOIndent = new POIndentDetail();

                        objPOIndent.PartNumber = reader["SUPPLIER_PART_NUMBER"].ToString();
                        objPOIndent.ItemDesc = reader["ITEM_SHORT_DESCRIPTION"].ToString();
                        objPOIndent.StockOnHand = reader["STOCK"].ToString();
                        objPOIndent.PendingOrderQty = reader["PENDINGORDER"].ToString();
                        objPOIndent.DocOnHand = reader["DOCONHAND"].ToString();
                        objPOIndent.AvgSales = reader["AVERAGESALES"].ToString();
                        objPOIndent.CurMonthSales = reader["CURRENTMTHSALES"].ToString();
                        if (reader["ABCFMS_STATUS"].ToString() == "AF" || reader["ABCFMS_STATUS"].ToString() == "BF" || reader["ABCFMS_STATUS"].ToString() == "CF")
                        {
                            objPOIndent.ToOrderQty = reader["TO_ORDER_QTY"].ToString();
                        }
                        else if (reader["ABCFMS_STATUS"].ToString() == "AM" || reader["ABCFMS_STATUS"].ToString() == "BM" || reader["ABCFMS_STATUS"].ToString() == "CM")
                        {
                            double dToOrderQty = 0;
                            dToOrderQty = Convert.ToDouble(reader["TO_ORDER_QTY"].ToString());
                            dToOrderQty = Math.Round((dToOrderQty * 0.75), 0);
                            objPOIndent.ToOrderQty = dToOrderQty.ToString();
                        }
                        else if (reader["ABCFMS_STATUS"].ToString() == "AS" || reader["ABCFMS_STATUS"].ToString() == "BS" || reader["ABCFMS_STATUS"].ToString() == "CS")
                        {
                            double dToOrderQty = 0;
                            dToOrderQty = Convert.ToDouble(reader["TO_ORDER_QTY"].ToString());
                            dToOrderQty = Math.Round((dToOrderQty * 0.51), 0);
                            objPOIndent.ToOrderQty = dToOrderQty.ToString();
                        }

                        objPOIndent.PackQty = reader["PACKING_QUANTITY"].ToString();
                        objPOIndent.AcceptedQty = reader["ACCEPTED"].ToString();
                        objPOIndent.ItemCode = reader["ITEM_CODE"].ToString();
                        objPOIndent.VehTypeDesc = reader["VEHICLE_TYPE_DESCRIPTION"].ToString();
                        objPOIndent.ToOrderQtyAddl = reader["TO_ORDER_QTY_ADDL"].ToString();

                        objPartItems.Add(objPOIndent);
                    }
                }
            }
            return objPartItems;
        }
        #endregion

        public string SubmitPOIndentCWH(DataTable objIndentTbl, string sIndentNo, string sIndentType, string BranchCode)
        {
            string result = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    //Truncate dummy1_AbcFms
                    string sSql = string.Empty;
                    string sDummyabcfms = string.Empty;

                    if (sIndentType != "0")
                    {
                        if (sIndentType == "NILABCFMS")
                        {
                            sDummyabcfms = DUMMYABCFMSNIL;
                        }
                        else
                        {
                            sDummyabcfms = DUMMYABCFMS;
                        }
                    }

                    sSql = "Delete From " + sDummyabcfms + " Where Branch_Code='" + BranchCode + "' and Indent_Number='" + sIndentNo + "'";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmdC = ImpalDB.GetSqlStringCommand(sSql);
                    cmdC.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdC);

                    for (int linti = 0; linti <= objIndentTbl.Rows.Count - 1; linti++)
                    {
                        if (objIndentTbl.Rows[linti]["AcceptedQty"].ToString().Trim() != "0" && objIndentTbl.Rows[linti]["AcceptedQty"].ToString().Trim() != "")
                        {
                            sSql = "Insert into " + sDummyabcfms + " (Supplier_Part_Number,Item_Short_Description,stockonhand,"
                                   + " Doc_On_Hand,pendingorder,Avgsales,currmthsales,toorder,Acceptedqty, Item_Code, Branch_Code,remarks, Indent_Number) "
                                   + " values ('" + objIndentTbl.Rows[linti]["PartNumber"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["ItemDesc"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["StockOnHand"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["DocOnHand"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["PendingOrderQty"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["AvgSales"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["CurMonthSales"] + "',"
                                   + "'" + (string.IsNullOrEmpty(objIndentTbl.Rows[linti]["ToOrderQty"].ToString()) ? "0" : objIndentTbl.Rows[linti]["ToOrderQty"]) + "',"
                                   + "'" + objIndentTbl.Rows[linti]["AcceptedQty"] + "',"
                                   + "'" + objIndentTbl.Rows[linti]["ItemCode"] + "',"
                                   + "'" + BranchCode + "','','" + sIndentNo + "')";

                            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd1);
                        }
                    }

                    DbCommand cmd3 = ImpalDB.GetStoredProcCommand("usp_updpoqty");
                    ImpalDB.AddInParameter(cmd3, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd3, "@PO_Number", DbType.String, sIndentNo.Trim());
                    cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd3);

                    result = "0";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = "Error";
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return result;
        }

        public string SubmitPOIndentEPO(string sIndentNo, string sIndentType, string BranchCode, string SupplierCode, string EPOtype, string Remarks, POIndentCWH objPOIndentCWH)
        {
            string result = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    string sSql = string.Empty;

                    sSql = "Delete From " + ITEM_WORKSHEET_EPO + " Where Branch_Code='" + BranchCode + "' and Indent_Number='" + sIndentNo + "'";
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmdC = ImpalDB.GetSqlStringCommand(sSql);
                    cmdC.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdC);

                    foreach (POIndentDetail objPartItems in objPOIndentCWH.Items)
                    {
                        if (objPartItems.CurMonthSales == "")
                            objPartItems.CurMonthSales = "0";

                        if (objPartItems.AvgSales == "")
                            objPartItems.AvgSales = "0";

                        if (objPartItems.StockOnHand == "")
                            objPartItems.StockOnHand = "0";

                        sSql = "Insert into " + ITEM_WORKSHEET_EPO + " (Branch_Code, Supplier_Code, Indent_Number, Serial_Number, EPO_Type, Order_Type, Supplier_Part_Number, Item_Code, Curr_Month, Avg_Sales, Stock, Ordered_Qty) "
                                   + " values ('" + BranchCode + "','" + SupplierCode + "','" + sIndentNo + "'," + objPartItems.SerialNumber + ",'" + EPOtype + "','" + objPartItems.Indicator + "','" + objPartItems.PartNumber + "','" + objPartItems.ItemCode + "',"
                                   + "" + objPartItems.CurMonthSales + "," + objPartItems.AvgSales + "," + objPartItems.StockOnHand + "," + objPartItems.ExtraQty + ")";

                        DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd1);
                    }

                    DbCommand cmd3 = ImpalDB.GetStoredProcCommand("usp_updpoqty_EPO");
                    ImpalDB.AddInParameter(cmd3, "@Branch_Code", DbType.String, BranchCode.Trim());
                    ImpalDB.AddInParameter(cmd3, "@Supplier_Code", DbType.String, SupplierCode.Trim());
                    ImpalDB.AddInParameter(cmd3, "@PO_Number", DbType.String, sIndentNo.Trim());
                    ImpalDB.AddInParameter(cmd3, "@Remarks", DbType.String, Remarks.Trim());
                    cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd3);

                    result = "0";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = "Error";
                Log.WriteException(typeof(POIndentCWHTran), exp);
            }

            return result;
        }
    }
}