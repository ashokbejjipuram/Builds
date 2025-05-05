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

namespace IMPALLibrary.Masters
{
    public class ItemMasters
    {
        public List<ItemMaster> GetAllItemCode()
        {
            List<ItemMaster> ItemMaster = new List<ItemMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select item_code from Item_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ItemMaster.Add(new ItemMaster(reader["item_Code"].ToString(), ""));
                    //.Add(new ItemMaster(reader["item_Code"].ToString()));
                }
            }

            return ItemMaster;
        }

        #region Itemcode for SLB
        public List<ItemMaster> GetItemCode(string strBranchCode)
        {
            List<ItemMaster> code = new List<ItemMaster>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select distinct a.item_code, b.Supplier_Part_Number from slb_item_calculation a,Item_master b where a.item_code=b.item_code and a.Branch_Code = '" + strBranchCode + "' order by Supplier_part_number";
            code.Add(new ItemMaster("", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    code.Add(new ItemMaster(reader["item_code"].ToString(), reader["Supplier_Part_Number"].ToString()));
                }
            }

            return code;
        }
        #endregion

        public List<ItemMaster> GetSupplierPartNumber(string strSupplier)
        {
            List<ItemMaster> code = new List<ItemMaster>();
            code.Add(new ItemMaster("", ""));

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct item_code,supplier_part_number from item_master where substring(Supplier_Line_Code,1,3)='" + strSupplier + "' and supplier_part_number is not null order by supplier_part_number";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    code.Add(new ItemMaster(reader["item_code"].ToString(), reader["Supplier_Part_Number"].ToString()));
                }
            }

            return code;
        }

        public List<ItemMaster> GetSupplierPartNumberWorkSheet(string strSupplier, string strBranch, string strPartNumber)
        {
            List<ItemMaster> code = new List<ItemMaster>();            
            code.Add(new ItemMaster("", ""));

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDPOworksheet_PartNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSupplier);
            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, strPartNumber);            
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


        public List<ItemMaster> GetSupplierPartNumberEPO(string strBranch, string strIndentNumber)
        {
            List<ItemMaster> code = new List<ItemMaster>();
            code.Add(new ItemMaster("", ""));

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetEPOworksheet_PartNumber");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
            ImpalDB.AddInParameter(cmd, "@Indent_Number", DbType.String, strIndentNumber);
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


        public List<ItemMaster> GetAllItem()
        {
            List<ItemMaster> code = new List<ItemMaster>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Item_code,supplier_part_number from Item_Master where supplier_part_number is not null and Item_code is not null";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    code.Add(new ItemMaster(reader["item_code"].ToString(), reader["Supplier_Part_Number"].ToString()));
                }
            }

            return code;
        }

        public String Getitemcode()
        {
            string ItemCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select top 1 item_code from Item_Master where supplier_part_number is not null";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            ItemCode = ImpalDB.ExecuteScalar(cmd).ToString();
            return ItemCode;
        }

        public String Getitemcode(string PartNumber)
        {
            string ItemCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select top 1 item_code from Item_Master  where supplier_part_number='" + PartNumber + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            ItemCode = ImpalDB.ExecuteScalar(cmd).ToString();
            return ItemCode;
        }

        public List<ItemMaster> GetAllItemCode(string strSupplierLine)
        {
            List<ItemMaster> code = new List<ItemMaster>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select distinct item_code,Supplier_Part_Number from Item_master where Supplier_Line_Code  = '" + strSupplierLine + "' order by Supplier_Part_Number";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                code.Add(new ItemMaster("", ""));
                while (reader.Read())
                {
                    code.Add(new ItemMaster(reader["item_code"].ToString(), reader["Supplier_Part_Number"].ToString()));
                }
            }

            return code;
        }



        #region To Get All the Item Fields
        
        public List<ItemFields> ViewItem(string itemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Getitem");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, itemCode.Trim());
            List<ItemFields> ItemList = new List<ItemFields>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    ItemFields ItemFields = new ItemFields();
                    ItemFields.Item_Code = objReader["Item_Code"].ToString();
                    ItemFields.Application_Segment_Code = objReader["Application_Segment_Code"].ToString();
                    ItemFields.Vehicle_Type_Code = objReader["Vehicle_Type_Code"].ToString();
                    ItemFields.Supplier_Line_Code = objReader["Supplier_Line_Code"].ToString();
                    ItemFields.Item_Short_Description = objReader["Item_Short_Description"].ToString();
                    ItemFields.Item_Long_Description = objReader["Item_Long_Description"].ToString();
                    ItemFields.Supplier_Part_Number = objReader["Supplier_Part_Number"].ToString();
                    ItemFields.Item_Type_Code = objReader["Item_Type_Code"].ToString();
                    ItemFields.Product_Group_Code = objReader["Product_Group_Code"].ToString();
                    ItemFields.Purchase_Discount = objReader["Purchase_Discount"].ToString();
                    ItemFields.Excise_Duty_Discount = objReader["Excise_Duty_Discount"].ToString();
                    ItemFields.Excise_Duty_Indicator = objReader["Excise_Duty_Indicator"].ToString();
                    ItemFields.Excise_Duty_Value = objReader["Excise_Duty_Value"].ToString();
                    ItemFields.Packing_Quantity = objReader["Packing_Quantity"].ToString();
                    ItemFields.Unit_of_Measurement = objReader["Unit_of_Measurement"].ToString();
                    ItemFields.ABC_Classification = objReader["ABC_Classification"].ToString();
                    ItemFields.FSN_Classification = objReader["FSN_Classification"].ToString();
                    ItemFields.Rate_Indicator = objReader["Rate_Indicator"].ToString();
                    ItemFields.Rate = objReader["Rate"].ToString();
                    ItemFields.Minimum_Order_Quantity = objReader["Minimum_Order_Quantity"].ToString();
                    ItemFields.Maximum_Order_Quantity = objReader["Maximum_Order_Quantity"].ToString();
                    ItemFields.Economic_Batch_Quantity = objReader["Economic_Batch_Quantity"].ToString();
                    ItemFields.Safety_stock = objReader["Safety_stock"].ToString();
                    ItemFields.Minimum_Lead_Time = objReader["Minimum_Lead_Time"].ToString();
                    ItemFields.Maximum_Lead_Time = objReader["Maximum_Lead_Time"].ToString();
                    ItemFields.Vehicle_Type_Description = objReader["Vehicle_Type_Description"].ToString();
                    ItemFields.Product_Group_Description = objReader["Product_Group_Description"].ToString();
                    ItemFields.Status = objReader["Status"].ToString();

                    ItemList.Add(ItemFields);

                }
            }

            return ItemList;
        }
        #endregion


        #region To Add New Items
        
        public void AddNewItem(ItemFields ItemFields)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddItem");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Application_Segment_Code", DbType.String, ItemFields.Application_Segment_Code);
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Code", DbType.String, ItemFields.Vehicle_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, ItemFields.Supplier_Line_Code);
            ImpalDB.AddInParameter(cmd, "@Item_Short_Description", DbType.String, ItemFields.Item_Short_Description);
            ImpalDB.AddInParameter(cmd, "@Item_Long_Description", DbType.String, ItemFields.Item_Long_Description);
            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, ItemFields.Supplier_Part_Number);
            ImpalDB.AddInParameter(cmd, "@Item_Type_Code", DbType.String, ItemFields.Item_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.String, ItemFields.Product_Group_Code);
            ImpalDB.AddInParameter(cmd, "@Purchase_Discount", DbType.String, ItemFields.Purchase_Discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Discount", DbType.String, ItemFields.Excise_Duty_Discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Indicator", DbType.String, ItemFields.Excise_Duty_Indicator);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Value", DbType.String, ItemFields.Excise_Duty_Value);
            ImpalDB.AddInParameter(cmd, "@Packing_Quantity", DbType.String, ItemFields.Packing_Quantity);
            ImpalDB.AddInParameter(cmd, "@Unit_of_Measurement", DbType.String, ItemFields.Unit_of_Measurement);
            ImpalDB.AddInParameter(cmd, "@ABC_Classification", DbType.String, ItemFields.ABC_Classification);
            ImpalDB.AddInParameter(cmd, "@FSN_Classification", DbType.String, ItemFields.FSN_Classification);
            ImpalDB.AddInParameter(cmd, "@Rate_Indicator", DbType.String, ItemFields.Rate_Indicator);
            ImpalDB.AddInParameter(cmd, "@Rate", DbType.String, ItemFields.Rate);
            ImpalDB.AddInParameter(cmd, "@Minimum_Order_Quantity", DbType.String, ItemFields.Minimum_Order_Quantity);
            ImpalDB.AddInParameter(cmd, "@Maximum_Order_Quantity", DbType.String, ItemFields.Maximum_Order_Quantity);
            ImpalDB.AddInParameter(cmd, "@Economic_Batch_Quantity", DbType.String, ItemFields.Economic_Batch_Quantity);
            ImpalDB.AddInParameter(cmd, "@Safety_stock", DbType.String, ItemFields.Safety_stock);
            ImpalDB.AddInParameter(cmd, "@Minimum_Lead_Time", DbType.String, ItemFields.Minimum_Lead_Time);
            ImpalDB.AddInParameter(cmd, "@Maximum_Lead_Time", DbType.String, ItemFields.Maximum_Lead_Time);
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, ItemFields.Status);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }
        #endregion


        #region To Update Items
        
        public void UpdateItem(ItemFields ItemFields)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdItem");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemFields.Item_Code);
            ImpalDB.AddInParameter(cmd, "@Application_Segment_Code", DbType.String, ItemFields.Application_Segment_Code);
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Code", DbType.String, ItemFields.Vehicle_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, ItemFields.Supplier_Line_Code);
            ImpalDB.AddInParameter(cmd, "@Item_Short_Description", DbType.String, ItemFields.Item_Short_Description);
            ImpalDB.AddInParameter(cmd, "@Item_Long_Description", DbType.String, ItemFields.Item_Long_Description);
            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, ItemFields.Supplier_Part_Number);
            ImpalDB.AddInParameter(cmd, "@Item_Type_Code", DbType.String, ItemFields.Item_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.String, ItemFields.Product_Group_Code);
            ImpalDB.AddInParameter(cmd, "@Purchase_Discount", DbType.String, ItemFields.Purchase_Discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Discount", DbType.String, ItemFields.Excise_Duty_Discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Indicator", DbType.String, ItemFields.Excise_Duty_Indicator);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Value", DbType.String, ItemFields.Excise_Duty_Value);
            ImpalDB.AddInParameter(cmd, "@Packing_Quantity", DbType.String, ItemFields.Packing_Quantity);
            ImpalDB.AddInParameter(cmd, "@Unit_of_Measurement", DbType.String, ItemFields.Unit_of_Measurement);
            ImpalDB.AddInParameter(cmd, "@ABC_Classification", DbType.String, ItemFields.ABC_Classification);
            ImpalDB.AddInParameter(cmd, "@FSN_Classification", DbType.String, ItemFields.FSN_Classification);
            ImpalDB.AddInParameter(cmd, "@Rate_Indicator", DbType.String, ItemFields.Rate_Indicator);
            ImpalDB.AddInParameter(cmd, "@Rate", DbType.String, ItemFields.Rate);
            ImpalDB.AddInParameter(cmd, "@Minimum_Order_Quantity", DbType.String, ItemFields.Minimum_Order_Quantity);
            ImpalDB.AddInParameter(cmd, "@Maximum_Order_Quantity", DbType.String, ItemFields.Maximum_Order_Quantity);
            ImpalDB.AddInParameter(cmd, "@Economic_Batch_Quantity", DbType.String, ItemFields.Economic_Batch_Quantity);
            ImpalDB.AddInParameter(cmd, "@Safety_stock", DbType.String, ItemFields.Safety_stock);
            ImpalDB.AddInParameter(cmd, "@Minimum_Lead_Time", DbType.String, ItemFields.Minimum_Lead_Time);
            ImpalDB.AddInParameter(cmd, "@Maximum_Lead_Time", DbType.String, ItemFields.Maximum_Lead_Time);
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, ItemFields.Status);
            ImpalDB.AddInParameter(cmd, "@updstatus", DbType.String, ItemFields.Status);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }
        #endregion

        public Discount GetDiscount(string SupplierLineCode)
        {
            Discount code = new Discount();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select purchase_discount,Excise_Duty_Discount,ED_Value,ED_Indicator   from supplier_line_master where supplier_line_code ='" + SupplierLineCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    code.PurDisCount = reader["purchase_discount"].ToString();
                    code.EDDiscount = reader["Excise_Duty_Discount"].ToString();
                    code.EDDValue = reader["ED_Value"].ToString();
                    code.EDDInd = reader["ED_Indicator"].ToString();
                }
            }

            return code;
        }
    }
    public class ItemMaster
    {

        string _itemcode;
        string _Supplierpartno;

        public ItemMaster(string itemcode)
        {
            _itemcode = itemcode;
        }

        public ItemMaster(string itemcode, string Supplierpartno)
        {
            _itemcode = itemcode;
            _Supplierpartno = Supplierpartno;
        }

        public string itemcode
        {
            get { return _itemcode; }
            set { _itemcode = value; }
        }
        public string Supplierpartno
        {
            get { return _Supplierpartno; }
            set { _Supplierpartno = value; }

        }
    }

    public class Items
    {
        #region GetItems
        /// <summary>
        /// Gets the list of all items based on Supplier Code
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems(string SupplierCode)
        {
            List<Item> lstItems = new List<Item>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT ITEM_CODE,SUPPLIER_PART_NUMBER,ITEM_SHORT_DESCRIPTION FROM ITEM_MASTER "
                            + "WHERE SUBSTRING(SUPPLIER_LINE_CODE,1,3) = '" + SupplierCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstItems.Add(new Item(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }
            }
            return lstItems;
        }

        public List<Item> GetItemsSurplus(string SupplierCode, string StrBranchCode)
        {
            List<Item> lstItems = new List<Item>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT SUPPLIER_PART_NUMBER,DESCRIPTION FROM Branches_Surplus "
                            + "WHERE SUPPLIER_CODE = '" + SupplierCode + "' AND BRANCH_CODE = '" + StrBranchCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstItems.Add(new Item(reader[0].ToString(), reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstItems;
        }
        #endregion
    }

    public class Item
    {
        private string _Code;
        private string _Name;
        private string _SupplierPartNumber;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string SupplierPartNumber
        {
            get { return _SupplierPartNumber; }
            set { _SupplierPartNumber = value; }
        }

        public Item()
        {
        }

        public Item(string Code, string Name, string SupplierPartNumber)
        {
            _Code = Code;
            _Name = Name;
            _SupplierPartNumber = SupplierPartNumber;
        }
    }

    public class ItemFields
    {
        public ItemFields()
        {

        }

        private string _Item_Code;
        private string _Application_Segment_Code;
        private string _Vehicle_Type_Code;
        private string _Supplier_Line_Code;
        private string _Item_Short_Description;
        private string _Item_Long_Description;
        private string _Supplier_Part_Number;
        private string _Item_Type_Code;
        private string _Product_Group_Code;
        private string _Purchase_Discount;
        private string _Excise_Duty_Discount;
        private string _Excise_Duty_Indicator;
        private string _Excise_Duty_Value;
        private string _Packing_Quantity;
        private string _Unit_of_Measurement;
        private string _ABC_Classification;
        private string _FSN_Classification;
        private string _Rate_Indicator;
        private string _Rate;
        private string _Minimum_Order_Quantity;
        private string _Maximum_Order_Quantity;
        private string _Economic_Batch_Quantity;
        private string _Safety_stock;
        private string _Minimum_Lead_Time;
        private string _Maximum_Lead_Time;
        private string _Appln_Segment_Description;
        private string _Vehicle_Type_Description;
        private string _Product_Group_Description;
        private string _Status;

        public string Item_Code
        {
            get { return _Item_Code; }
            set { _Item_Code = value; }
        }

        public string Application_Segment_Code
        {
            get { return _Application_Segment_Code; }
            set { _Application_Segment_Code = value; }
        }

        public string Vehicle_Type_Code
        {
            get { return _Vehicle_Type_Code; }
            set { _Vehicle_Type_Code = value; }
        }

        public string Supplier_Line_Code
        {
            get { return _Supplier_Line_Code; }
            set { _Supplier_Line_Code = value; }
        }

        public string Item_Short_Description
        {
            get { return _Item_Short_Description; }
            set { _Item_Short_Description = value; }
        }

        public string Item_Long_Description
        {
            get { return _Item_Long_Description; }
            set { _Item_Long_Description = value; }
        }

        public string Supplier_Part_Number
        {
            get { return _Supplier_Part_Number; }
            set { _Supplier_Part_Number = value; }
        }

        public string Item_Type_Code
        {
            get { return _Item_Type_Code; }
            set { _Item_Type_Code = value; }
        }

        public string Product_Group_Code
        {
            get { return _Product_Group_Code; }
            set { _Product_Group_Code = value; }
        }

        public string Purchase_Discount
        {
            get { return _Purchase_Discount; }
            set { _Purchase_Discount = value; }
        }

        public string Excise_Duty_Discount
        {
            get { return _Excise_Duty_Discount; }
            set { _Excise_Duty_Discount = value; }
        }

        public string Excise_Duty_Indicator
        {
            get { return _Excise_Duty_Indicator; }
            set { _Excise_Duty_Indicator = value; }
        }

        public string Excise_Duty_Value
        {
            get { return _Excise_Duty_Value; }
            set { _Excise_Duty_Value = value; }
        }

        public string Packing_Quantity
        {
            get { return _Packing_Quantity; }
            set { _Packing_Quantity = value; }
        }

        public string Unit_of_Measurement
        {
            get { return _Unit_of_Measurement; }
            set { _Unit_of_Measurement = value; }
        }

        public string ABC_Classification
        {
            get { return _ABC_Classification; }
            set { _ABC_Classification = value; }
        }

        public string FSN_Classification
        {
            get { return _FSN_Classification; }
            set { _FSN_Classification = value; }
        }

        public string Rate_Indicator
        {
            get { return _Rate_Indicator; }
            set { _Rate_Indicator = value; }
        }

        public string Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        public string Minimum_Order_Quantity
        {
            get { return _Minimum_Order_Quantity; }
            set { _Minimum_Order_Quantity = value; }
        }

        public string Maximum_Order_Quantity
        {
            get { return _Maximum_Order_Quantity; }
            set { _Maximum_Order_Quantity = value; }
        }

        public string Economic_Batch_Quantity
        {
            get { return _Economic_Batch_Quantity; }
            set { _Economic_Batch_Quantity = value; }
        }

        public string Safety_stock
        {
            get { return _Safety_stock; }
            set { _Safety_stock = value; }
        }

        public string Minimum_Lead_Time
        {
            get { return _Minimum_Lead_Time; }
            set { _Minimum_Lead_Time = value; }
        }

        public string Maximum_Lead_Time
        {
            get { return _Maximum_Lead_Time; }
            set { _Maximum_Lead_Time = value; }
        }

        public string Appln_Segment_Description
        {
            get { return _Appln_Segment_Description; }
            set { _Appln_Segment_Description = value; }
        }

        public string Vehicle_Type_Description
        {
            get { return _Vehicle_Type_Description; }
            set { _Vehicle_Type_Description = value; }
        }

        public string Product_Group_Description
        {
            get { return _Product_Group_Description; }
            set { _Product_Group_Description = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

    }

    public class Discount
    {

        public Discount()
        {

        }


        private string _PurDisCount;
        private string _EDDiscount;
        private string _EDDValue;
        private string _EDDInd;

        public string PurDisCount
        {
            get { return _PurDisCount; }
            set { _PurDisCount = value; }
        }

        public string EDDiscount
        {
            get { return _EDDiscount; }
            set { _EDDiscount = value; }
        }

        public string EDDValue
        {
            get { return _EDDValue; }
            set { _EDDValue = value; }
        }

        public string EDDInd
        {
            get { return _EDDInd; }
            set { _EDDInd = value; }
        }

    }

}
