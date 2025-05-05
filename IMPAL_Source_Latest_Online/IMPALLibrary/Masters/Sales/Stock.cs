#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data; 
#endregion

namespace IMPALLibrary.Masters
{
    public class Stock
    {
        public bool InsertDatesForCorp(string FromDate, string ToDate, string BranchCode)
        {
            bool blSuccess = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int intInsert = 0;
                Database ImpalDB = DataAccess.GetDatabase();
                // Create command to execute the stored procedure and add the parameters.
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_STOCKVARIANCE");
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                //ImpalDB.AddInParameter(cmd, "@dat2", DbType.String, BranchName);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intInsert = ImpalDB.ExecuteNonQuery(cmd);
                //if (intInsert > 0)
                blSuccess = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return blSuccess;
        }

        #region InsertPendingDocs
        public bool InsertPendingDocs(string FromDate, string ToDate, string BranchName)
        {
            bool blSuccess = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int intInsert = 0;
                Database ImpalDB = DataAccess.GetDatabase();
                // Create command to execute the stored procedure and add the parameters.
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_PENDINGDOCS");
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, BranchName);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intInsert = ImpalDB.ExecuteNonQuery(cmd);
                //if (intInsert > 0)
                blSuccess = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return blSuccess;
        }
        #endregion

        #region StockLedgerTransaction
        public void StockLedgerTransaction(string MonthYear, string BranchCode, string SupplierCode, string ItemCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_STOCK_MAIN");
                ImpalDB.AddInParameter(cmd, "@m_yr", DbType.String, MonthYear);
                ImpalDB.AddInParameter(cmd, "@brnch", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@supplier_code", DbType.String, SupplierCode);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        public List<ProductGroup> GetSegmentStockProductList(string BranchCode, string SupplierCode)
        {
            List<ProductGroup> ProductGroupLst = new List<ProductGroup>();            
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            strQuery = "Select distinct Product_Description, Supplier_Code From New_Segment_Master";
            if (SupplierCode != "0")
                strQuery = strQuery + " Where Supplier_Code ='" + SupplierCode + "'";
            strQuery = strQuery + " order by Supplier_Code, Product_Description";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductGroupLst.Add(new ProductGroup(reader["Product_Description"].ToString(), reader["Product_Description"].ToString()));
                }
            }

            return ProductGroupLst;
        }

        public List<ApplicationSegment> GetSegmentStockSegmentList(string BranchCode, string SupplierCode, string ProductDescription)
        {
            List<ApplicationSegment> ApplnSegLst = new List<ApplicationSegment>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = default(string);
            strQuery = "Select distinct Application_Segment_Code, Application_Segment_Description, Supplier_Code  From New_Segment_Master";
            if (SupplierCode != "0")
                strQuery = strQuery + " Where Supplier_Code ='" + SupplierCode + "'";
            if (ProductDescription != "" && SupplierCode == "0")
                strQuery = strQuery + " Where Product_Description ='" + ProductDescription + "'";
            if (ProductDescription != "" && SupplierCode != "0")
                strQuery = strQuery + " and Product_Description ='" + ProductDescription + "'";
            strQuery = strQuery + " order by Supplier_Code, Application_Segment_Description";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ApplnSegLst.Add(new ApplicationSegment(reader["Application_Segment_Code"].ToString(), reader["Application_Segment_Description"].ToString()));
                }
            }

            return ApplnSegLst;
        }

        public List<VehilcleType> GetSegmentStockVehilcleTypes(string BranchCode, string SupplierCode, string ProductDescription, string Segment)
        {
            List<VehilcleType> VehicleTypeLst = new List<VehilcleType>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "Select distinct Vehicle_Type, Supplier_Code From New_Segment_Master";
            if (SupplierCode != "0")
                strQuery = strQuery + " Where Supplier_Code ='" + SupplierCode + "'";
            if (ProductDescription != "" && SupplierCode == "0")
                strQuery = strQuery + " Where Product_Description ='" + ProductDescription + "'";
            if (ProductDescription != "" && SupplierCode != "0")
                strQuery = strQuery + " and Product_Description ='" + ProductDescription + "'";

            if (Segment != "" && (ProductDescription == "" || SupplierCode == "0"))
                strQuery = strQuery + " Where Application_Segment_Code ='" + Segment + "'";
            else
                strQuery = strQuery + " and Application_Segment_Code ='" + Segment + "'";

            strQuery = strQuery + " order by Supplier_Code, Vehicle_Type";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    VehicleTypeLst.Add(new VehilcleType(reader["Vehicle_Type"].ToString(), reader["Vehicle_Type"].ToString()));
                }
            }

            return VehicleTypeLst;
        }

        public List<ApplicationSegment> GetSegmentStockSegmentListNew()
        {
            List<ApplicationSegment> ApplnSegLst = new List<ApplicationSegment>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = default(string);
            strQuery = "Select distinct Application_Segment_Description From New_Segment_Master Where Application_Segment_Description<>'' and Application_Segment_Description IS NOT NULL order by Application_Segment_Description";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ApplnSegLst.Add(new ApplicationSegment(reader["Application_Segment_Description"].ToString(), reader["Application_Segment_Description"].ToString()));
                }
            }

            return ApplnSegLst;
        }

        public List<Supplier> GetSegmentSupplierCodes(string Segment)
        {
            List<Supplier> suppCodeList = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "Select distinct n.Supplier_Code, s.Supplier_Name From New_Segment_Master n inner join Supplier_Master s on n.Supplier_Code=s.Supplier_Code";

            if (Segment != "")
                strQuery = strQuery + " Where n.Application_Segment_Description ='" + Segment + "'";

            strQuery = strQuery + " order by n.Supplier_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    suppCodeList.Add(new Supplier(reader["Supplier_Code"].ToString(), reader["Supplier_Name"].ToString()));
                }
            }

            return suppCodeList;
        }

        public List<Supplier> GetAllSupplierCodes()
        {
            List<Supplier> suppCodeList = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "Select distinct Supplier_Code, Supplier_Name From Supplier_Master Order By Supplier_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    suppCodeList.Add(new Supplier(reader["Supplier_Code"].ToString(), reader["Supplier_Name"].ToString()));
                }
            }

            return suppCodeList;
        }
    }
}
