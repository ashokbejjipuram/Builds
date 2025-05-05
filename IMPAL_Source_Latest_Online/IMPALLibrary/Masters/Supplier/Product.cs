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
    public class Products
    {
        public List<Product> GetAllProducts()
        {
            List<Product> lstProducts = new List<Product>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = " Select a.Supplier_Code, a.Supplier_Product_Code,a.Supplier_Product_Short_Name,a.Supplier_Product_Name ,b.Supplier_Name " +
                            " From Supplier_Product_Master a Inner Join Supplier_Master b ON a.Supplier_Code = b.Supplier_Code " +
                            " Order by a.Supplier_Code Asc , a.Supplier_Product_Code DESC ";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstProducts.Add(new Product(reader["Supplier_Code"].ToString(), reader["Supplier_Product_Code"].ToString(), reader["Supplier_Product_Short_Name"].ToString(), reader["Supplier_Product_Name"].ToString(),reader["Supplier_Name"].ToString()));
                }
            }
            return lstProducts;
        }
        public bool FindExists(string SupplierCode, string SupplierProductCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int BranchCode = 0;
            string ssql = "Select 1 From Supplier_Product_Master a Inner Join Supplier_Master b ON a.Supplier_Code = b.Supplier_Code" +
                           " where a.Supplier_code='" + SupplierCode + "' and a.Supplier_Product_Code='" + SupplierProductCode + "'";

            using (DbCommand sqlCmd = ImpalDB.GetSqlStringCommand(ssql))
            {
                BranchCode = (int)ImpalDB.ExecuteScalar(sqlCmd);
            }

            if (BranchCode == 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public void AddNewProducts(string SupplierCode, string SupplierProductCode, string SupplierProductShortName, string SupplierProductName)
        {
                Database ImpalDB = DataAccess.GetDatabase();            
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addsupplierproduct");
                ImpalDB.AddInParameter(cmd, "@Supplier_Product_Code", DbType.String, SupplierProductCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Product_Short_Name", DbType.String, SupplierProductShortName.Trim());
                ImpalDB.AddInParameter(cmd, "@Supplier_Product_Name", DbType.String, SupplierProductName.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                
            
            
        }
        public void UpdateProducts(string SupplierCode, string SupplierProductCode, string SupplierProductShortName, string SupplierProductName , string SupplierName)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_updsupplierproduct");
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String,SupplierName.Substring(0,3).Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Product_Code", DbType.String, SupplierProductCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Product_Short_Name", DbType.String, SupplierProductShortName.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Product_Name", DbType.String, SupplierProductName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class Product
    {
      public Product(string SupplierCode, string SupplierProductCode, string SupplierProductShortName, string SupplierProductName,string SupplierName)
        {
            _SupplierCode = SupplierCode;
            _SupplierProductCode = SupplierProductCode;
            _SupplierProductShortName = SupplierProductShortName;
            _SupplierProductName = SupplierProductName;
            _SupplierName = SupplierName;
        }
                private string _SupplierCode;
                private string _SupplierName;
                private string _SupplierProductCode;
                private string _SupplierProductShortName;
                private string _SupplierProductName;

                public string SupplierCode
                {
                    get { return _SupplierCode; }
                    set { _SupplierCode = value; }
                }
                public string SupplierProductCode
                {
                    get { return _SupplierProductCode; }
                    set { _SupplierProductCode = value; }
                }
                public string SupplierProductShortName
                {
                    get { return _SupplierProductShortName; }
                    set { _SupplierProductShortName = value; }
                }
                public string SupplierProductName
                {
                    get { return _SupplierProductName; }
                    set { _SupplierProductName = value; }
                }
                public string SupplierName
                {
                    get { return _SupplierName; }
                    set { _SupplierName = value; }
                }
    }
}