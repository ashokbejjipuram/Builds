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

namespace IMPALLibrary
{
    public class ItemRateMasters
    {
        public void AddNewItemRate(string SupplierName, string ItemCode, string Partnumber, string BranchName, string NetPrice)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Addrateitemprice");

            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchName.Trim());
            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Currency, NetPrice.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

               
	    
	  	

        }
        public void UpdateItemRate(string SupplierName, string ItemCode, string Partnumber, string BranchName, string NetPrice)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            ////// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updrateitemprice");

            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchName.Trim());
            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Currency, NetPrice.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<ItemRateMaster> GetAllItemRates(string Filter, string StrBranchCode)
        {
            List<ItemRateMaster> ItemRateMasters = new List<ItemRateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            if (Filter != string.Empty)
            {
                sSQL = "select b.Supplier_Name,a.Item_Code,c.Supplier_Part_Number , a.Branch_code,a.Net_Price from Rate_Master a Inner join Supplier_Master b " +
                       " On substring(a.Item_Code,1,3) = b.Supplier_Code Inner join Item_Master c on a.Item_Code = c.Item_Code " +
                       " WHERE b.Supplier_Code ='" + Filter + "' and a.Branch_Code = '" + StrBranchCode + "'";
            }
            else
            {
                sSQL = "select b.Supplier_Name,a.Item_Code,c.Supplier_Part_Number , a.Branch_code,a.Net_Price from Rate_Master a Inner join Supplier_Master b " +
                        " On substring(a.Item_Code,1,3) = b.Supplier_Code Inner join Item_Master c on a.Item_Code = c.Item_Code WHERE a.Branch_Code = '" + StrBranchCode + "'";
            }

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ItemRateMasters.Add(new ItemRateMaster(reader["Supplier_Name"].ToString(), reader["Item_Code"].ToString(), reader["Supplier_Part_Number"].ToString(), reader["Branch_Code"].ToString(), reader["Net_Price"].ToString()));
                }
            }
            return ItemRateMasters;
        }

        public List<ItemRateMaster> GetAllItemRates(string StrBranchCode)
        {
            List<ItemRateMaster> ItemRateMasters = new List<ItemRateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select b.Supplier_Name,a.Item_Code,c.Supplier_Part_Number , a.Branch_code,a.Net_Price " +
                    " from Rate_Master a Inner join Supplier_Master b " +
                    " On substring(a.Item_Code,1,3) = b.Supplier_Code Inner join Item_Master c on a.Item_Code = c.Item_Code WHERE a.Branch_Code = '" + StrBranchCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ItemRateMasters.Add(new ItemRateMaster(reader["Supplier_Name"].ToString(), reader["Item_Code"].ToString(), reader["Supplier_Part_Number"].ToString(), reader["Branch_Code"].ToString(), reader["Net_Price"].ToString()));
                }
            }
            return ItemRateMasters;
        }

    }
    public class ItemRateMaster
    {
        public ItemRateMaster(string SupplierName, string ItemCode, string Partnumber,string BranchName, string NetPrice)
        {
            _SupplierName = SupplierName;
            _ItemCode = ItemCode;
            _Partnumber = Partnumber;
            _BranchName = BranchName;
            _NetPrice = NetPrice;
            
        }
        public ItemRateMaster()
        {

        }

        private string _SupplierName;
        private string _Partnumber;
        private string _ItemCode;
        private string _BranchName;
        private string _NetPrice;

        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        public string Partnumber
        {
            get { return _Partnumber; }
            set { _Partnumber = value; }
        }
        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        public string NetPrice
        {
            get { return _NetPrice; }
            set { _NetPrice = value; }
        }
        
    }

    public class SupplierLines
    {
        public List<ItemRateSupplierLine> GetItemSuppliers()
        {
            List<ItemRateSupplierLine> SupplierLines = new List<ItemRateSupplierLine>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Supplier_Code,Supplier_Name from Supplier_Master ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierLines.Add(new ItemRateSupplierLine(reader["Supplier_Code"].ToString(), reader["Supplier_Name"].ToString()));
                }
            }
            return SupplierLines;
        }
    }

    public class ItemRateSupplierLine
    {
        public ItemRateSupplierLine(string Code,string Name)
        {
            _Code = Code;
            _Name = Name;
        }
        public ItemRateSupplierLine()
        {

        }
        private string _Code;
        private string _Name;
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
    }
}
