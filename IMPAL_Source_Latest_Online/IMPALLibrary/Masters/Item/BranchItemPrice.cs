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
    public class BranchItemPrices
    {

        public BranchItemPrices()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public List<BIPSupplierLine> GetSuppliers()
        {
            List<BIPSupplierLine> SupplierLineList = new List<BIPSupplierLine>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select Supplier_Code ,Supplier_Name from Supplier_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    SupplierLineList.Add(new BIPSupplierLine(sdr["Supplier_Code"].ToString(), sdr["Supplier_Name"].ToString()));
                }
            }
            return SupplierLineList;
        }

        public List<Branch_ItemPrice> GetBranchItemPriceList(string SupplierLine, string PartNumber, string StrBranchCode)
        {
            List<Branch_ItemPrice> BranchItemPriceList = new List<Branch_ItemPrice>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            if (string.IsNullOrEmpty(PartNumber))
            {
                sSQL = "select b.Supplier_Name,a.Item_Code,c.Supplier_Part_Number ,a.Branch_Code,a.List_Price,a.Cost_Price,a.Selling_Price from Branch_Item_Price a " + 
                       " Inner Join Supplier_Master  b on Substring(a.Item_Code,1,3) = b.supplier_code " + 
                       " Inner Join Item_Master c on a.Item_Code = c.Item_Code " +
                       " Where SUBSTRING(a.item_code ,1,3) = '" + SupplierLine + "' and a.Branch_Code = '" + StrBranchCode + "'";
            }
            else
            {
                sSQL = "select b.Supplier_Name,a.Item_Code,c.Supplier_Part_Number ,a.Branch_Code,a.List_Price,a.Cost_Price,a.Selling_Price from Branch_Item_Price a " +
                       " Inner Join Supplier_Master  b on Substring(a.Item_Code,1,3) = b.supplier_code " +
                       " Inner Join Item_Master c on a.Item_Code = c.Item_Code " +
                       " Where SUBSTRING(a.item_code ,1,3) = '" + SupplierLine + "' and " +
                        "c.Supplier_Part_Number like '" + PartNumber + "%' and a.Branch_Code = '" + StrBranchCode + "'";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader sdr = ImpalDB.ExecuteReader(cmd1))
            {
                while (sdr.Read())
                {
                    BranchItemPriceList.Add(new Branch_ItemPrice(sdr["Supplier_Name"].ToString(), sdr["item_code"].ToString(), sdr["Supplier_Part_Number"].ToString(), sdr["Branch_Code"].ToString(), sdr["List_Price"].ToString(), sdr["Cost_Price"].ToString(), sdr["Selling_Price"].ToString()));
                }
            }
            return BranchItemPriceList;
        }

        public List<BranchItemPriceDiscDtls> GetPurchaseDiscountDetails(string strBranchCode, string strSupplierLine, string strSupplierPartNo)
        {
            List<BranchItemPriceDiscDtls> lstDiscount = new List<BranchItemPriceDiscDtls>();
            BranchItemPriceDiscDtls objDiscount = new BranchItemPriceDiscDtls();
            objDiscount.DiscountDesc = "0";
            objDiscount.DiscountPercentage = "0";
            lstDiscount.Add(objDiscount);

            Database ImpalDB = DataAccess.GetDatabase(); //CD_Percentage
            string sSQL = "Select distinct Purchase_Discount from Purchase_Discount_Master WITH (NOLOCK) where Supplier_Code='" + strSupplierLine + "' Order by Purchase_Discount";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objDiscount = new BranchItemPriceDiscDtls();
                    objDiscount.DiscountDesc = FourDecimalConversion(reader[0].ToString());
                    objDiscount.DiscountPercentage = FourDecimalConversion(reader[0].ToString());
                    lstDiscount.Add(objDiscount);
                }
            }

            return lstDiscount;
        }

        public List<BranchItemPriceDiscDtls> GetCouponDetails(string strBranchCode, string strSupplierLine, string strSupplierPartNo)
        {
            List<BranchItemPriceDiscDtls> lstDiscount = new List<BranchItemPriceDiscDtls>();
            BranchItemPriceDiscDtls objDiscount = new BranchItemPriceDiscDtls();
            objDiscount.DiscountDesc = "0";
            objDiscount.DiscountPercentage = "0";
            lstDiscount.Add(objDiscount);

            Database ImpalDB = DataAccess.GetDatabase(); //CD_Percentage
            string sSQL = "Select distinct Coupon_Amount from Coupon_Master_Inward WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and Supplier_Code='" + strSupplierLine + "' and Supplier_Part_Number='" + strSupplierPartNo + "' Order by Coupon_Amount";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objDiscount = new BranchItemPriceDiscDtls();
                    objDiscount.DiscountDesc = FourDecimalConversion(reader[0].ToString());
                    objDiscount.DiscountPercentage = FourDecimalConversion(reader[0].ToString());
                    lstDiscount.Add(objDiscount);
                }
            }

            return lstDiscount;
        }        

        public List<BranchItemPriceDiscDtls> GetCDPercentageDetails(string strBranchCode, string strSupplierLine, string strSupplierPartNo)
        {
            List<BranchItemPriceDiscDtls> lstDiscount = new List<BranchItemPriceDiscDtls>();
            BranchItemPriceDiscDtls objDiscount = new BranchItemPriceDiscDtls();
            objDiscount.DiscountDesc = "0";
            objDiscount.DiscountPercentage = "0";
            lstDiscount.Add(objDiscount);

            Database ImpalDB = DataAccess.GetDatabase(); //CD_Percentage
            string sSQL = "Select * from [dbo].[udf_CDpercentage]()";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objDiscount = new BranchItemPriceDiscDtls();
                    objDiscount.DiscountDesc = TwoDecimalConversion(reader[0].ToString());
                    objDiscount.DiscountPercentage = TwoDecimalConversion(reader[0].ToString());
                    lstDiscount.Add(objDiscount);
                }
            }

            return lstDiscount;
        }

        public List<BranchItemPriceDiscDtls> GetWCPercentageDetails(string strBranchCode, string strSupplierLine, string strSupplierPartNo)
        {
            List<BranchItemPriceDiscDtls> lstDiscount = new List<BranchItemPriceDiscDtls>();
            BranchItemPriceDiscDtls objDiscount = new BranchItemPriceDiscDtls();
            objDiscount.DiscountDesc = "0";
            objDiscount.DiscountPercentage = "0";
            lstDiscount.Add(objDiscount);

            Database ImpalDB = DataAccess.GetDatabase(); //CD_Percentage
            string sSQL = "Select distinct Depot_Charges from Purchase_Discount_Master WITH (NOLOCK) where Supplier_Code='" + strSupplierLine + "' and Depot_Charges<>0 Order by Depot_Charges";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objDiscount = new BranchItemPriceDiscDtls();
                    objDiscount.DiscountDesc = FourDecimalConversion(reader[0].ToString());
                    objDiscount.DiscountPercentage = FourDecimalConversion(reader[0].ToString());
                    lstDiscount.Add(objDiscount);
                }
            }

            return lstDiscount;
        }

        public void AddNewBranchItemPriceOld(string supplierLine, string item_code, string PartNo, string Branch_Code, string listPrice, string costPrice, string sellingPrice)
        {
            //int _Maximum_Quantity = Convert.ToInt16(Maximum_Quantity.Trim());
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchitemprice_Old");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, item_code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, listPrice.Trim());
            ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.String, costPrice.Trim());
            ImpalDB.AddInParameter(cmd, "@Selling_Price", DbType.String, sellingPrice.Trim());
            //ImpalDB.AddInParameter(cmd, "@Maximum_Quantity", DbType.Int16, _Maximum_Quantity);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);              
        }

        public int AddNewBranchItemPriceHO(string Zone, string State, string Branch, string supplierLine, string item_code, string PartNo, decimal listPrice, decimal costPrice, decimal MRP)
        {
            int result = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchitemprice_Admin");
                ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, Zone.Trim());
                ImpalDB.AddInParameter(cmd, "@State", DbType.String, State.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, Branch.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Line", DbType.String, supplierLine.Trim());
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, item_code.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Part_No", DbType.String, PartNo.Trim());
                ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Decimal, listPrice);
                ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.Decimal, costPrice);
                ImpalDB.AddInParameter(cmd, "@MRP", DbType.Decimal, MRP);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(BranchItemPrices), exp);
            }

            return result;
        }

        public int AddNewBranchItemPrice(string Zone, string State, string Branch, string supplierLine, string item_code, string PartNo, decimal listPrice, decimal costPrice, decimal MRP)
        {
            int result = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchitemprice");
                ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, Zone.Trim());
                ImpalDB.AddInParameter(cmd, "@State", DbType.String, State.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, Branch.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Line", DbType.String, supplierLine.Trim());
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, item_code.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Part_No", DbType.String, PartNo.Trim());
                ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Decimal, listPrice);
                ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.Decimal, costPrice);
                ImpalDB.AddInParameter(cmd, "@MRP", DbType.Decimal, MRP);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(BranchItemPrices), exp);
            }

            return result;
        }

        public int AddNewBranchItemPriceBulkUploadHO(string Zone, string State, string Branch)
        {
            int result = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchitemprice_Bulk_Admin");
                ImpalDB.AddInParameter(cmd, "@Zone", DbType.String, Zone.Trim());
                ImpalDB.AddInParameter(cmd, "@State", DbType.String, State.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, Branch.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(BranchItemPrices), exp);
            }

            return result;
        }

        public void UpdateBranchItemPrice(string supplierLine, string item_code, string PartNo, string Branch_Code, string listPrice, string costPrice, string sellingPrice)
        {
            //int _Maximum_Quantity = Convert.ToInt16(Maximum_Quantity.Trim());
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updbranchitemprice");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, item_code.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Branch_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@List_Price", DbType.String, listPrice.Trim());
            ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.String, costPrice.Trim());
            ImpalDB.AddInParameter(cmd, "@Selling_Price", DbType.String, sellingPrice.Trim());
            //ImpalDB.AddInParameter(cmd, "@Maximum_Quantity", DbType.Int16, _Maximum_Quantity);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string FourDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.0000";
            else
                return string.Format("{0:0.0000}", Convert.ToDecimal(strValue));
        }
    }

    public class Branch_ItemPrice
    { 
         public Branch_ItemPrice(string supplierLine,string item_code, string PartNo, string Branch_Code, string listPrice, string costPrice, string sellingPrice)
        {
            _item_code = item_code;
            _supplierLine = supplierLine;
            _Branch_Code = Branch_Code;
            _PartNo = PartNo;
            _listPrice = listPrice;
            _costPrice = costPrice;
            _sellingPrice = sellingPrice;
        }

         private string _item_code;
         private string _supplierLine;
         private string _Branch_Code;
         private string _PartNo;
         private string _listPrice;
         private string _costPrice;
         private string _sellingPrice;
        
        public string item_code
         {
             get { return _item_code; }
             set { _item_code = value; }
         }
        public string supplierLine
        {
            get { return _supplierLine; }
            set { _supplierLine = value; }
        }
          public string Branch_Code
        {
            get { return _Branch_Code; }
            set { _Branch_Code = value; }
        }
          public string PartNo
          {
              get { return _PartNo; }
              set { _PartNo = value; }
          }
          public string listPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }
          public string costPrice
        {
            get { return _costPrice; }
            set { _costPrice = value; }
        }
          public string sellingPrice
        {
            get { return _sellingPrice; }
            set { _sellingPrice = value; }
        }
    }

    public class BranchItemPriceDiscDtls
    {
        public string DiscountDesc { get; set; }
        public string DiscountPercentage { get; set; }
    }

    public class BIPSupplierLine
    {
        public BIPSupplierLine(string SupplierCode, string SupplierName)
        {
            _SupplierCode = SupplierCode;
            _SupplierName = SupplierName;

        }

        private string _SupplierCode;
        private string _SupplierName;

        public string SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }

    }


    //public class ItemDetails
    //{
    //    public ItemDetails(string item_code, string Location_Code, string Branch_Code, string Aisle, string Row, string Bin, string Balance_Quantity, string Maximum_Quantity)
    //    {
    //        _item_code = item_code;
    //        _Location_Code = Location_Code;
    //        _Branch_Code = Branch_Code;
    //        _Row = Row;
    //        _Aisle = Aisle;
    //        _Bin = Bin;
    //        _Balance_Quantity = Balance_Quantity;
    //        _Maximum_Quantity = Maximum_Quantity;

    //    }

    //    private string _item_code;
    //    private string _Location_Code;
    //    private string _Branch_Code;
    //    private string _Row;
    //    private string _Aisle;
    //    private string _Bin;
    //    private string _Balance_Quantity;
    //    private string _Maximum_Quantity;

    //    public string item_code
    //    {
    //        get { return _item_code; }
    //        set { _item_code = value; }
    //    }
    //    public string Location_Code
    //    {
    //        get { return _Location_Code; }
    //        set { _Location_Code = value; }
    //    }

    //    public string Branch_Code
    //    {
    //        get { return _Branch_Code; }
    //        set { _Branch_Code = value; }
    //    }

    //    public string Row
    //    {
    //        get { return _Row; }
    //        set { _Row = value; }
    //    }

    //    public string Aisle
    //    {
    //        get { return _Aisle; }
    //        set { _Aisle = value; }
    //    }

    //    public string Bin
    //    {
    //        get { return _Bin; }
    //        set { _Bin = value; }
    //    }

    //    public string Balance_Quantity
    //    {
    //        get { return _Balance_Quantity; }
    //        set { _Balance_Quantity = value; }
    //    }

    //    public string Maximum_Quantity
    //    {
    //        get { return _Maximum_Quantity; }
    //        set { _Maximum_Quantity = value; }
    //    }

    //}

    //public class AllBranch
    //{
    //    public AllBranch(string BankCode, string BankName)
    //    {
    //        _BankCode = BankCode;
    //        _BankName = BankName;

    //    }

    //    private string _BankCode;
    //    private string _BankName;

    //    public string BankCode
    //    {
    //        get { return _BankCode; }
    //        set { _BankCode = value; }
    //    }
    //    public string BankName
    //    {
    //        get { return _BankName; }
    //        set { _BankName = value; }
    //    }

    //}

    //public class SupplierPartnumber
    //{
    //    public SupplierPartnumber(string PartNumber)
    //    {
    //        _PartNumber = PartNumber;

    //    }

    //    private string _PartNumber;

    //    public string PartNumber
    //    {
    //        get { return _PartNumber; }
    //        set { _PartNumber = value; }
    //    }
    //}

}
