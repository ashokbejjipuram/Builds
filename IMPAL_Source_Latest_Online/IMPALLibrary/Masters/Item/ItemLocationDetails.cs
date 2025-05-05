using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary
{
    public class ItemLocationDetails
    {

        public ItemLocationDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<SupplierPartnumber> GetSupplierPartNumber(string SupplierLine)
        {
            List<SupplierPartnumber> SupplierPartNumberList = new List<SupplierPartnumber>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select Supplier_Part_Number from Item_Master where Supplier_Line_Code ='" + SupplierLine + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    SupplierPartNumberList.Add(new SupplierPartnumber(sdr["Supplier_Part_Number"].ToString()));
                }
            }
            return SupplierPartNumberList;
        }

        public List<SupplierPartnumber> GetSupplierPartNumberAutoComplete(string Partnumber)
        {
            List<SupplierPartnumber> SupplierPartNumberList = new List<SupplierPartnumber>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select supplier_part_number,item_code from Item_Master where supplier_part_number is not null and supplier_part_number like '" + Partnumber + "%' order by supplier_part_number";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    SupplierPartNumberList.Add(new SupplierPartnumber(sdr["Supplier_Part_Number"].ToString(), sdr["item_code"].ToString()));
                }
            }
            return SupplierPartNumberList;
        }

        public List<SupplierCode> GetSupplierCode()
        {
            List<SupplierCode> SupplierLineList = new List<SupplierCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select Supplier_Code, Supplier_Name from Supplier_Master order by Supplier_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    SupplierLineList.Add(new SupplierCode(sdr["Supplier_Code"].ToString(), sdr["Supplier_Name"].ToString()));
                }
            }
            return SupplierLineList;
        }

        public List<SupplierLine> GetSupplierLine()
        {
            List<SupplierLine> SupplierLineList = new List<SupplierLine>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select Supplier_Line_Code ,Short_Description from Supplier_Line_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    SupplierLineList.Add(new SupplierLine(sdr["Supplier_Line_Code"].ToString(), sdr["Short_Description"].ToString()));
                }
            }
            return SupplierLineList;
        }

        public List<ItemDetails> GetItemLocDetails(string SupplierLine, string PartNumber, string StrBranchCode)
        {
            List<ItemDetails> ItemDetailsLineList = new List<ItemDetails>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            if (string.IsNullOrEmpty(PartNumber))
            {
                sSQL = "select a.item_code,b.Supplier_Part_Number,a.Location_Code,a.Branch_Code,a.Aisle,a.Row,a.Bin,0 " +
                "as Balance_Quantity,999999 as Maximum_Quantity from Item_Location a inner join " +
                "Item_Master b on a.Item_Code = b.Item_Code where SUBSTRING(a.item_code ,1,3) = '" + SupplierLine + "' and a.Branch_Code = '" + StrBranchCode + "'";                
            }
            else
            {
                sSQL = "select a.item_code,b.Supplier_Part_Number,a.Location_Code,a.Branch_Code,a.Aisle,a.Row,a.Bin,0 " +
                "as Balance_Quantity,999999 as Maximum_Quantity from Item_Location a inner join " +
                "Item_Master b on a.Item_Code = b.Item_Code where SUBSTRING(a.item_code ,1,3) = '" + SupplierLine + "' and " +
                "b.Supplier_Part_Number like '" + PartNumber + "%' and a.Branch_Code = '" + StrBranchCode + "'";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    ItemDetailsLineList.Add(new ItemDetails(sdr["item_code"].ToString(), sdr["Supplier_Part_Number"].ToString(), sdr["Location_Code"].ToString(), sdr["Branch_Code"].ToString(), sdr["Aisle"].ToString(), sdr["Row"].ToString(), sdr["Bin"].ToString(), sdr["Balance_Quantity"].ToString(), sdr["Maximum_Quantity"].ToString()));
                }
            }
            return ItemDetailsLineList;
        }

        public List<GlBranch> GetGlBranch(string BranchCode)
        {
            List<GlBranch> GlBranchList = new List<GlBranch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct Branch_Code,Branch_Name from Branch_master Where Branch_Code='" + BranchCode  + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    GlBranchList.Add(new GlBranch(sdr["Branch_Code"].ToString(), sdr["Branch_Name"].ToString()));
                }
            }

            return GlBranchList;
        }

        public void AddNewItemLocation(string Item_Code, string Branch_Code, string Aisle, string Row, string Bin, string Maximum_Quantity)
        {
            int _Maximum_Quantity = Convert.ToInt32(Maximum_Quantity.Trim());
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Additemlocation");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, Item_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Aisle", DbType.String, Aisle.Trim());
            ImpalDB.AddInParameter(cmd, "@Row", DbType.String, Row.Trim());
            ImpalDB.AddInParameter(cmd, "@Bin", DbType.String, Bin.Trim());
            ImpalDB.AddInParameter(cmd, "@Maximum_Quantity", DbType.Int32, _Maximum_Quantity);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateItemLocation(string Item_Code, string Branch_Code, string Aisle, string Row, string Bin, string Maximum_Quantity)
        {
            int _Maximum_Quantity = Convert.ToInt32(Maximum_Quantity.Trim());
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upditemlocation");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, Item_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Aisle", DbType.String, Aisle.Trim());
            ImpalDB.AddInParameter(cmd, "@Row", DbType.String, Row.Trim());
            ImpalDB.AddInParameter(cmd, "@Bin", DbType.String, Bin.Trim());
            ImpalDB.AddInParameter(cmd, "@Maximum_Quantity", DbType.Int32, _Maximum_Quantity);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void AddNewItemLocationNew(string Item_Code, string Branch_Code, string Location_Code)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Additemlocation_New");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, Item_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Location_Code", DbType.String, Location_Code.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateItemLocationNew(string Item_Code, string Branch_Code, string Location_Code)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upditemlocation_New");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, Item_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Location_Code", DbType.String, Location_Code.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public int CheckItemLocation(string Item_Code, string Aisle, string Row, string Bin, string Branch_code)
        {
            int Count = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            sSQL = "select 1 from Item_location where Item_Code ='" + Item_Code + "' and Aisle ='" + Aisle + "' and Row ='" + Row + "' and Bin ='" + Bin + "' and Branch_code='" + Branch_code + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            Count = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));
            return Count;
        }
    }

    public class SupplierLine
    {
        public SupplierLine(string Supplier_Line_Code, string Short_Description)
        {
            _Supplier_Line_Code = Supplier_Line_Code;
            _Short_Description = Short_Description;
        }

        private string _Supplier_Line_Code;
        private string _Short_Description;

        public string Supplier_Line_Code
        {
            get { return _Supplier_Line_Code; }
            set { _Supplier_Line_Code = value; }
        }
        public string Short_Description
        {
            get { return _Short_Description; }
            set { _Short_Description = value; }
        }
    }

    public class SupplierCode
    {
        public SupplierCode(string Supplier_Code, string Supplier_Name)
        {
            _Supplier_Code = Supplier_Code;
            _Supplier_Name = Supplier_Name;
        }

        private string _Supplier_Code;
        private string _Supplier_Name;

        public string Supplier_Code
        {
            get { return _Supplier_Code; }
            set { _Supplier_Code = value; }
        }
        public string Supplier_Name
        {
            get { return _Supplier_Name; }
            set { _Supplier_Name = value; }
        }
    }


    public class ItemDetails
    {
        public ItemDetails(string item_code, string part_no, string Location_Code, string Branch_Code, string Aisle, string Row, string Bin, string Balance_Quantity, string Maximum_Quantity)
        {
            _item_code = item_code;
            _part_no = part_no;
            _Location_Code = Location_Code;
            _Branch_Code = Branch_Code;
            _Row = Row;
            _Aisle = Aisle;
            _Bin = Bin;
            _Balance_Quantity = Balance_Quantity;
            _Maximum_Quantity = Maximum_Quantity;

        }

        private string _item_code;
        private string _part_no;
        private string _Location_Code;
        private string _Branch_Code;
        private string _Row;
        private string _Aisle;
        private string _Bin;
        private string _Balance_Quantity;
        private string _Maximum_Quantity;

        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string part_no
        {
            get { return _part_no; }
            set { _part_no = value; }
        }
        public string Location_Code
        {
            get { return _Location_Code; }
            set { _Location_Code = value; }
        }

        public string Branch_Code
        {
            get { return _Branch_Code; }
            set { _Branch_Code = value; }
        }

        public string Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        public string Aisle
        {
            get { return _Aisle; }
            set { _Aisle = value; }
        }

        public string Bin
        {
            get { return _Bin; }
            set { _Bin = value; }
        }

        public string Balance_Quantity
        {
            get { return _Balance_Quantity; }
            set { _Balance_Quantity = value; }
        }

        public string Maximum_Quantity
        {
            get { return _Maximum_Quantity; }
            set { _Maximum_Quantity = value; }
        }

    }

    public class AllBranch
    {
        public AllBranch(string BankCode, string BankName)
        {
            _BankCode = BankCode;
            _BankName = BankName;

        }

        private string _BankCode;
        private string _BankName;

        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

    }

    public class SupplierPartnumber
    {
        public SupplierPartnumber(string PartNumber)
        {
            _PartNumber = PartNumber;

        }

        public SupplierPartnumber(string PartNumber, string ItemCode)
        {
            _PartNumber = PartNumber;
            _ItemCode = ItemCode;

        }

        private string _PartNumber;
        private string _ItemCode;

        public string PartNumber
        {
            get { return _PartNumber; }
            set { _PartNumber = value; }
        }

        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
    }


}
