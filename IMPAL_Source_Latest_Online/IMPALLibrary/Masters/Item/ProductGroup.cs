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
    public class ProductGroups
    { //Product_Group_Code, Product_Group_Description
        public List<ProductGroup> GetAllProductGroups()
        {
            List<ProductGroup> ProductGroupLst = new List<ProductGroup>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Product_Group_Code, Product_Group_Description from Product_Group_Master Order by Product_Group_Code asc";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ProductGroupLst.Add(new ProductGroup(reader["Product_Group_Code"].ToString(), reader["Product_Group_Description"].ToString()));
                }
            }

            return ProductGroupLst;
        }
        public void AddNewProductGroup(string ProductGroupDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addproductmaster");
            ImpalDB.AddInParameter(cmd, "@Product_Group_Description", DbType.String, ProductGroupDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateProductGroup(string ProductGroupCode, string ProductGroupDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updproductmaster");

            ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.String, ProductGroupCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Product_Group_Description", DbType.String, ProductGroupDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
    public class ProductGroup
    {
        public ProductGroup(string ProductGroupCode, string ProductGroupDescription)
        {
            _ProductGroupCode = ProductGroupCode;
            _ProductGroupDescription = ProductGroupDescription;
        }
        private string _ProductGroupCode;
        private string _ProductGroupDescription;

        public string ProductGroupCode
        {
            get { return _ProductGroupCode; }
            set { _ProductGroupCode = value; }
        }
        public string ProductGroupDescription
        {
            get { return _ProductGroupDescription; }
            set { _ProductGroupDescription = value; }
        }

    }

}


