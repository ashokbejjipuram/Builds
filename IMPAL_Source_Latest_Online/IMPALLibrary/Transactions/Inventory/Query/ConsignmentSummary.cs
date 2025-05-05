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


namespace IMPALLibrary
{

    #region This class is used Consignment Summary info in View mode functionalities
    public class ConsignmentSummary
    {
        public ConsignmentSummary()
        { }

        public List<ConsignmentItemCode> GetConsignmentItemCode(string strPart_Number, string strBranchCode)
        {
            List<ConsignmentItemCode> objGetItemCode = new List<ConsignmentItemCode>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select distinct a.item_code,a.supplier_part_number from item_master a WITH (NOLOCK) inner join consignment b WITH (NOLOCK) on a.item_code = b.item_code and b.Branch_Code = '" + strBranchCode + "' and a.supplier_part_number like '" + strPart_Number + "%'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ConsignmentItemCode objTrans = new ConsignmentItemCode();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objGetItemCode.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objGetItemCode;
        }

        public List<ConsignmentItemCode> GetConsignmentItemCodeAllBranches(string strPart_Number, string strBranchCode)
        {
            List<ConsignmentItemCode> objGetItemCode = new List<ConsignmentItemCode>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select Item_Code, Supplier_Part_Number from item_master WITH (NOLOCK) Where Supplier_Part_Number like '" + strPart_Number + "%'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ConsignmentItemCode objTrans = new ConsignmentItemCode();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objGetItemCode.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objGetItemCode;
        }

        public ConsignmentHeaderSummary GetConsignmentHeaderSummary(string strItem_Code, string strBranch_Code)
        {
            // List<ConsignmentHeaderSummary> objGetItemHeader = new List<ConsignmentHeaderSummary>();
            ConsignmentHeaderSummary objTrans = new ConsignmentHeaderSummary();

            Int32 iRowCount = 0;
            if (string.IsNullOrEmpty(strItem_Code))
                strItem_Code = "";
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                objTrans.Balance_Quantity = "";
                objTrans.Branch_Code = "";
                objTrans.FOC_LS_Balance_Quantity = "";
                objTrans.FOC_OS_Balance_Quantity = "";
                objTrans.Item_Code = "";
                objTrans.Long_Description = "";
                objTrans.LS_Balance_Quantity = "";
                objTrans.OS_Balance_Quantity = "";
                objTrans.Short_Description = "";
                objTrans.Supplier_Part_Number = "";
                objTrans.Branch_Code = strBranch_Code;

                if (strItem_Code.Length > 0)
                {
                    sSQL = "select distinct a.item_code,a.supplier_part_number from item_master a WITH (NOLOCK), consignment b WITH (NOLOCK) where b.Branch_Code = '" + strBranch_Code + "' and a.item_code = b.item_code and a.item_code like '" + strItem_Code + "%'";

                }
                else
                {
                    sSQL = "select distinct a.item_code,a.supplier_part_number from item_master a WITH (NOLOCK), consignment b WITH (NOLOCK) where b.Branch_Code = '" + strBranch_Code + "' and a.item_code = b.item_code and a.item_code = '" + strItem_Code + "'";

                }
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {

                        objTrans.Item_Code = reader["item_code"].ToString();
                        strItem_Code = reader["item_code"].ToString();
                    }
                }

                sSQL = "select item_short_description,supplier_part_number,item_long_description from item_master WITH (NOLOCK) where item_code = '" + strItem_Code.ToString() + "'";
                DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd2))
                {
                    while (reader.Read())
                    {
                        objTrans.Short_Description = reader["item_short_description"].ToString();
                        objTrans.Long_Description = reader["item_long_description"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                    }
                }

                sSQL = "select isnull(sum(balance_quantity),0) as BalanceQty from consignment WITH (NOLOCK) where item_code = '" + strItem_Code.ToString() + "' and Branch_code ='" + strBranch_Code + "' and status = 'A'";
                DbCommand cmd3 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd3))
                {
                    while (reader.Read())
                    {
                        objTrans.Balance_Quantity = reader["BalanceQty"].ToString();
                    }
                }

                sSQL = "select sum(case when os_ls_indicator = 'O' then isnull(balance_quantity,0) else 0 end) as OS_BalanceQty,sum(case when os_ls_indicator = 'L' then isnull(balance_quantity,0) else 0 end) as LS_BalanceQty ";
                sSQL = sSQL + "from consignment WITH (NOLOCK) where Status = 'A' and os_ls_indicator = 'O' and item_code='" + strItem_Code + "' and transaction_Type_Code not in ('171','071') and branch_Code = '" + strBranch_Code + "'";
                DbCommand cmd4 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd4))
                {
                    while (reader.Read())
                    {
                        objTrans.OS_Balance_Quantity = reader["OS_BalanceQty"].ToString();
                        objTrans.LS_Balance_Quantity = reader["LS_BalanceQty"].ToString();
                    }
                }

                sSQL = "select sum(case when os_ls_indicator = 'O' then isnull(balance_quantity,0) else 0 end) as FOC_OS_Balance_Qty,sum(case when os_ls_indicator = 'L' then isnull(balance_quantity,0) else 0 end) as FOC_LS_Balance_Qty ";
                sSQL = sSQL + "from consignment WITH (NOLOCK) where Status = 'A' and os_ls_indicator = 'O' and item_code='" + strItem_Code + "' and transaction_Type_Code in ('171','071') and branch_Code = '" + strBranch_Code + "'";
                DbCommand cmd5 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd5.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd5))
                {
                    while (reader.Read())
                    {
                        objTrans.FOC_OS_Balance_Quantity = reader["FOC_OS_Balance_Qty"].ToString();
                        objTrans.FOC_LS_Balance_Quantity = reader["FOC_LS_Balance_Qty"].ToString();
                    }
                }
       
                sSQL = "select 1 inward_number from consignment WITH (NOLOCK) where status = 'A' and item_code='" + strItem_Code + "' and branch_Code = '" + strBranch_Code + "'";
                DbCommand cmd6 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd6.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd6))
                {
                    while (reader.Read())
                    {
                        iRowCount += 1;
                        if ((Int32)reader["inward_number"] > 0)
                        {
                            objTrans.iRowCount = iRowCount;
                        }
                        else
                        {
                            objTrans.iRowCount = 0;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objTrans;
        }

        public List<ConsignmentItemSummary> GetConsignmentItemDetailSummary(string strItem_Code, string strBranch_Code, string strResetFlag)
        {
            List<ConsignmentItemSummary> objGetBalanceDetails = new List<ConsignmentItemSummary>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQLListPrice = "select inward_number,convert(nvarchar,inward_date,103) as inward_date, isnull(list_price,0) as list_price,isnull(cost_price,0) as cost_price,ED_indicator, isnull(ED_value,0) as ED_value,transaction_type_code,os_ls_indicator,GRN_Quantity,Balance_Quantity,Issue_Document_number, ";
                sSQLListPrice = sSQLListPrice + "convert(nvarchar,issue_document_date,103) as issue_document_date,location, convert(nvarchar,original_Receipt_Date,103) as original_Receipt_Date from consignment WITH (NOLOCK) where status = 'A' and item_code ='" + strItem_Code + "'and branch_code ='" + strBranch_Code + "' order by convert(nvarchar,Original_Receipt_Date,102) asc";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQLListPrice);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader readersSQLListPrice = ImpalDB.ExecuteReader(cmd1))
                {
                    while (readersSQLListPrice.Read())
                    {
                        ConsignmentItemSummary objTrans = new ConsignmentItemSummary();

                        objTrans.Balance_Quantity = readersSQLListPrice["Balance_Quantity"].ToString();
                        objTrans.Cost_Price = readersSQLListPrice["cost_price"].ToString();
                        objTrans.ED_Ind = readersSQLListPrice["ED_indicator"].ToString();
                        objTrans.ED_Value = readersSQLListPrice["ED_value"].ToString();
                        objTrans.GRN_Date = readersSQLListPrice["inward_date"].ToString();
                        objTrans.GRN_Number = readersSQLListPrice["inward_number"].ToString();
                        objTrans.GRN_Quantity = readersSQLListPrice["GRN_Quantity"].ToString();
                        objTrans.List_Price = readersSQLListPrice["list_price"].ToString();
                        objTrans.Original_Receipt_Date = readersSQLListPrice["original_Receipt_Date"].ToString();
                        objTrans.OS_LS = readersSQLListPrice["os_ls_indicator"].ToString();
                        objTrans.Tr_Type = readersSQLListPrice["transaction_type_code"].ToString();
                        objGetBalanceDetails.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objGetBalanceDetails;
        }


        public ConsignmentHeaderSummary GetConsignmentHeaderCons_Details(string strItem_Code, string strBranch_Code)
        {
            ConsignmentHeaderSummary objTrans = new ConsignmentHeaderSummary();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                objTrans.Balance_Quantity = "";
                objTrans.Branch_Code = "";
                objTrans.Item_Code = "";
                objTrans.Long_Description = "";
                objTrans.Short_Description = "";
                objTrans.Supplier_Part_Number = "";

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_GetConsignmentBalanceDetails_Header");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(dbcmd, "@strItem_Code", DbType.String, strItem_Code);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Branch_Code = reader["Branch_Code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objTrans.Short_Description = reader["item_short_description"].ToString();
                        objTrans.Long_Description = reader["item_long_description"].ToString();
                        objTrans.Balance_Quantity = reader["BalanceQty"].ToString();
                        objTrans.iRowCount = Convert.ToInt16(reader["InwardCount"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }

            return objTrans;
        }

        public List<ConsignmentItemSummary> GetConsignmentItemDetailCons_Details(string strItem_Code, string strBranch_Code, string strResetFlag)
        {
            List<ConsignmentItemSummary> objGetBalanceDetails = new List<ConsignmentItemSummary>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetConsignmentBalanceDetails_Footer");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItem_Code);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader readersSQLListPrice = ImpalDB.ExecuteReader(cmd))
                {
                    while (readersSQLListPrice.Read())
                    {
                        ConsignmentItemSummary objTrans = new ConsignmentItemSummary();
                        objTrans.Balance_Quantity = "";
                        objTrans.Cost_Price = readersSQLListPrice["cost_price"].ToString();
                        objTrans.ED_Ind = readersSQLListPrice["ED_indicator"].ToString();
                        objTrans.ED_Value = readersSQLListPrice["ED_value"].ToString();
                        objTrans.GRN_Date = readersSQLListPrice["inward_date"].ToString();
                        objTrans.GRN_Number = readersSQLListPrice["inward_number"].ToString();
                        objTrans.GRN_Quantity = readersSQLListPrice["GRN_Quantity"].ToString();
                        objTrans.List_Price = readersSQLListPrice["list_price"].ToString();
                        objTrans.Original_Receipt_Date = readersSQLListPrice["original_Receipt_Date"].ToString();
                        objTrans.OS_LS = readersSQLListPrice["os_ls_indicator"].ToString();
                        objTrans.Tr_Type = readersSQLListPrice["transaction_type_code"].ToString();
                        objTrans.Billed_Quantity = readersSQLListPrice["Issue_Quantity"].ToString();
                        objTrans.Invoice_Number = readersSQLListPrice["Issue_Document_number"].ToString();
                        objTrans.Invoice_Date = readersSQLListPrice["issue_document_date"].ToString();
                        objTrans.Location = readersSQLListPrice["location"].ToString();

                        objGetBalanceDetails.Add(objTrans);
                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objGetBalanceDetails;
        }

        public SurplusHeaderSummary GetAllBranchStockHeader_Details(string strItem_Code, string strBranch_Code)
        {
            SurplusHeaderSummary objTrans = new SurplusHeaderSummary();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                objTrans.Balance_Quantity = "";
                objTrans.Branch_Code = "";
                objTrans.Item_Code = "";
                objTrans.Long_Description = "";
                objTrans.Short_Description = "";
                objTrans.Supplier_Part_Number = "";

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_GetAllBranchStockHeader_Details");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(dbcmd, "@Item_Code", DbType.String, strItem_Code);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Branch_Code = reader["Branch_Code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objTrans.Short_Description = reader["item_short_description"].ToString();
                        objTrans.Vehicle_Application = reader["Vehicle_Type"].ToString();
                        objTrans.Product_Description = reader["Product_Description"].ToString();
                        objTrans.Application_Segment_Description = reader["Application_Segment_Description"].ToString();
                        objTrans.Balance_Quantity = reader["BalanceQty"].ToString();
                        objTrans.iRowCount = Convert.ToInt16(reader["InwardCount"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }

            return objTrans;
        }

        public List<SurplusItemSummary> GetAllBranchStockItem_Details(string strItem_Code, string strBranch_Code)
        {
            List<SurplusItemSummary> objGetBalanceDetails = new List<SurplusItemSummary>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetAllBranchStockItem_Details");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItem_Code);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader readersSQLListPrice = ImpalDB.ExecuteReader(cmd))
                {
                    while (readersSQLListPrice.Read())
                    {
                        SurplusItemSummary objTrans = new SurplusItemSummary();                        
                        
                        objTrans.Branch_Name = readersSQLListPrice["Branch_Name"].ToString();
                        objTrans.Less_than_Months = readersSQLListPrice["Less_than_Months"].ToString();
                        objTrans.Original_Receipt_Date_LessThanMonths = readersSQLListPrice["Original_Receipt_Date_LessThanMonths"].ToString();
                        objTrans.More_than_Months = readersSQLListPrice["More_than_Months"].ToString();
                        objTrans.Original_Receipt_Date_MoreThanMonths = readersSQLListPrice["Original_Receipt_Date_MoreThanMonths"].ToString();
                        objTrans.Total_Quantity = readersSQLListPrice["Total_Quantity"].ToString();
                        objTrans.List_Price = readersSQLListPrice["List_Price"].ToString();
                        objTrans.Cost_Price = readersSQLListPrice["Cost_Price"].ToString();

                        objGetBalanceDetails.Add(objTrans);
                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentSummary), exp);
            }
            return objGetBalanceDetails;
        }

    }
    #endregion

    #region This class is used for Consignment Summary Custom-DataType
    public class ConsignmentHeaderSummary
    {
        public ConsignmentHeaderSummary()
        { }

        public Int32 iRowCount { set; get; }
        public string Item_Code { set; get; }
        public string Branch_Code { set; get; }
        public string Balance_Quantity { set; get; }
        public string OS_Balance_Quantity { set; get; }
        public string FOC_OS_Balance_Quantity { set; get; }
        public string Short_Description { set; get; }
        public string Long_Description { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string LS_Balance_Quantity { set; get; }
        public string FOC_LS_Balance_Quantity { set; get; }
    }


    public class ConsignmentItemSummary
    {
        public ConsignmentItemSummary()
        { }

        public Int32 rowCount { set; get; }
        public string GRN_Number { set; get; }
        public string GRN_Date { set; get; }
        public string List_Price { set; get; }
        public string Cost_Price { set; get; }

        public string ED_Ind { set; get; }
        public string ED_Value { set; get; }
        public string Tr_Type { set; get; }
        public string OS_LS { set; get; }
        public string GRN_Quantity { set; get; }
        public string Balance_Quantity { set; get; }
        public string Original_Receipt_Date { set; get; }

        /* Following fields are applicble only Consignment Details */
        public string Billed_Quantity { set; get; }
        public string Invoice_Number { set; get; }
        public string Invoice_Date { set; get; }
        public string Location { set; get; }

    }

    public class SurplusHeaderSummary
    {
        public SurplusHeaderSummary()
        { }

        public Int32 iRowCount { set; get; }
        public string Item_Code { set; get; }
        public string Branch_Code { set; get; }
        public string Balance_Quantity { set; get; }
        public string OS_Balance_Quantity { set; get; }
        public string FOC_OS_Balance_Quantity { set; get; }
        public string Short_Description { set; get; }
        public string Long_Description { set; get; }
        public string Product_Description { set; get; }
        public string Vehicle_Application { set; get; }
        public string Application_Segment_Description { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string LS_Balance_Quantity { set; get; }
        public string FOC_LS_Balance_Quantity { set; get; }
    }

    public class SurplusItemSummary
    {
        public SurplusItemSummary()
        { }

        public Int32 rowCount { set; get; }
        public string Branch_Name { set; get; }
        public string Less_than_Months { set; get; }
        public string Original_Receipt_Date_LessThanMonths { set; get; }
        public string More_than_Months { set; get; }
        public string Original_Receipt_Date_MoreThanMonths { set; get; }
        public string Total_Quantity { set; get; }
        public string List_Price { set; get; }
        public string Cost_Price { set; get; }

    }



    #endregion

}
