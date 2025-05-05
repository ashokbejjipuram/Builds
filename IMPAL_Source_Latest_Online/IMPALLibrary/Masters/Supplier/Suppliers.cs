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
    public class Suppliers
    {
        #region GetAllSuppliers
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct Supplier_Code, Supplier_Name From Supplier_Master WITH (NOLOCK) Order By Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersDispLoc(int Ind)
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            sSQL = "Select Distinct s.Supplier_Code, s.Supplier_Name From Supplier_Master s WITH(NOLOCK) left outer join CHE_SBI_Street_SupplyLines c WITH(NOLOCK) on c.Supplier_Code = s.Supplier_Code ";

            if (Ind == 0)
                sSQL = sSQL + "Where c.Supplier_Code IS NULL ";
            else if (Ind == 1)
                sSQL = sSQL + "Where c.Supplier_Code IS NOT NULL and c.Status='A'";

            sSQL = sSQL + "Order By s.Supplier_Name";

            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersBIP()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct s.Supplier_Code, s.Supplier_Name From Supplier_Master s WITH (NOLOCK) inner join Suppliers_BIP s1 WITH (NOLOCK) on s.Supplier_Code=s1.Supplier_Code Order By s.Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersAutoGRN()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct s.Supplier_Code, s.Supplier_Name From Supplier_Master s WITH (NOLOCK) inner join Supplier_Customer_Master s1 WITH (NOLOCK) on s.Supplier_Code=s1.Supplier_Code Order By s.Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersWorkSheet
        public List<Supplier> GetAllSuppliersWorkSheet()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetAllSuppliersWorkSheet");            
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            SupplierBranches.Add(new Supplier("0", string.Empty));
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersWorkSheetSegment()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct Supplier_Code, Supplier_Name From Supplier_Segment_Master WITH (NOLOCK) Order By Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersLucas
        public List<Supplier> GetAllSuppliersLucas()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct Supplier_Code, Supplier_Name From Supplier_Master Where Supplier_Code like '33%' Order By Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersAcc
        public List<Supplier> GetAllSuppliersAcc()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct Supplier_Code, Supplier_Name From Supplier_Master Order By Supplier_Name";
            SupplierBranches.Add(new Supplier("0", "--ALL--"));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersGST
        public List<Supplier> GetAllSuppliersGST()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct Supplier_Code, Supplier_Name From Supplier_Master WITH (NOLOCK) Where isnull(Oldest_Pending_Invoice,'A') <>'I' Order By Supplier_Name";
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersManualGRN(string BranchCode)
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct sm.Supplier_Code, sm.Supplier_Name From Supplier_Master sm WITH (NOLOCK) left outer join supplier_customer_master sd WITH (NOLOCK) on sm.Supplier_Code = sd.Supplier_Code and sd.Status = 'A' and sd.Impal_Branch_Code='" + BranchCode + "' Where isnull(Oldest_Pending_Invoice,'A') <> 'I' and sd.Supplier_Code is null Order By sm.Supplier_Name";
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<Supplier> GetAllSuppliersGSTAutoGRN()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct sm.Supplier_Code, sm.Supplier_Name From Supplier_Master sm WITH (NOLOCK) INNER JOIN supplier_customer_master s WITH (NOLOCK) on sm.Supplier_Code=s.Supplier_Code and isnull(sm.Oldest_Pending_Invoice,'A') <> 'I' and s.Status = 'A' Order By Supplier_Name";
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersIndHex
        public List<Supplier> GetAllSuppliersIndHex()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Distinct s.Supplier_Code, s.Supplier_Name From Supplier_Master s inner join Indl_Hex_Part_Numbers i on s.Supplier_Code=i.Supplier_Code Order By Supplier_Name";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSFSuppliers

        public List<Supplier> GetAllSFSuppliers()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "SELECT DISTINCT SUPPLIER_CODE,SUPPLIER_NAME  FROM SUPPLIER_MASTER WHERE SUPPLIER_CODE in ('320','410','411','412','413') ORDER BY SUPPLIER_NAME";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        public List<Supplier> GetAllSuppliersSurplus(string BranchCode)
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "SELECT DISTINCT b.SUPPLIER_CODE,a.SUPPLIER_NAME  FROM SUPPLIER_MASTER a WITH (NOLOCK) inner join Branches_Surplus b WITH (NOLOCK) on a.SUPPLIER_CODE=b.SUPPLIER_CODE where b.Branch_Code = '" + BranchCode + "' ORDER BY a.SUPPLIER_NAME";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        #region GetAllSuppliers

        public List<Supplier> GetAllSuppliersBMS()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select Supplier_Code,Supplier_Name from Supplier_Master where BMS_Indicator is not null and substring(supplier_code,1,2) in ('16','26','33','41','45','54','52')";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region GetAllSuppliersInvocieCN

        public List<Supplier> GetAllSuppliersInvoiceCN()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "Select distinct Supplier_Code,Supplier_Name from Supplier_Master Order by Supplier_Code Asc";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }
        #endregion

        #region To Get All Supplier Code with Out Default Value Zero
        
        public List<Supplier> GetAllSupplierLines()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Supplier_Code, Supplier_Name from Supplier_Master WITH (NOLOCK) order by Supplier_Name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        #endregion

        #region GetApplSegBasedSuppliers
        public List<Supplier> GetApplSegBasedSuppliers(string ApplSegCode)
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT DISTINCT A.SUPPLIER_CODE,D.SUPPLIER_NAME " +
                "FROM SUPPLIER_LINE_MASTER A, APPLICATION_SEGMENT_MASTER B, ITEM_MASTER C, SUPPLIER_MASTER D " +
                "WHERE C.SUPPLIER_LINE_CODE = A.SUPPLIER_LINE_CODE AND " +
                "SUBSTRING(A.SUPPLIER_LINE_CODE,1,3) = D.SUPPLIER_CODE " +
                "AND C.APPLICATION_SEGMENT_CODE = '" + ApplSegCode + "' " +
                "ORDER BY D.SUPPLIER_NAME";
            SupplierBranches.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
            }
            return SupplierBranches;
        }
        #endregion

        #region line wise suppliers
        public List<Supplier> GetAllLinewiseSuppliers(string branchCode)
        {
            string sSQL = string.Empty;

            List<Supplier> LinewiseSuppliers = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            sSQL = "select distinct supplier_code,supplier_name from supplier_master Order by supplier_code";            
            LinewiseSuppliers.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    LinewiseSuppliers.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return LinewiseSuppliers;
        }
        #endregion

        #region line wise suppliers
        public List<Supplier> GetAllLinewiseSuppliersGST(string branchCode, string Indicator)
        {
            string sSQL = string.Empty;

            List<Supplier> LinewiseSuppliers = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            if (Indicator == "O")
            {
                sSQL = "select distinct supplier_code,supplier_name from supplier_master where supplier_code not in ('160','220','390') Order by supplier_code";
            }
            else
            {
                sSQL = "select distinct supplier_code,supplier_name from supplier_master where supplier_code in ('160','220','390') Order by supplier_code";
            }
            LinewiseSuppliers.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    LinewiseSuppliers.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return LinewiseSuppliers;
        }
        #endregion

        #region get supplier for branch
        public List<Supplier> GetSupplierline(string strBranchcode)
        {
            List<Supplier> LinewiseSuppliers = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.supplier_code,a.supplier_name from supplier_master a,Line_wise_sales b where a.supplier_code=substring(b.supplier_line_code,1,3) and b.Branch_Code = '" + strBranchcode + "' order by a.supplier_name ";
            LinewiseSuppliers.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    LinewiseSuppliers.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return LinewiseSuppliers;
        }
        #endregion

        #region To get list of all Line based Supplier
        //Used by StockSheetList.aspx
        public List<Supplier> GetLineBasedSupplier(string strBranchcode)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            //Adds empty item to the DDL
            lstSupplier.Add(new Supplier("0", string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "SELECT DISTINCT A.SUPPLIER_CODE,B.SUPPLIER_NAME FROM SUPPLIER_LINE_MASTER A, " +
                            "SUPPLIER_MASTER B WHERE SUBSTRING(A.SUPPLIER_LINE_CODE,1,3)=B.SUPPLIER_CODE AND " +
                            "A.SUPPLIER_LINE_CODE IN (SELECT SUBSTRING(ITEM_CODE,1,7) FROM ITEM_WISE_SALES WHERE BRANCH_CODE = '" + strBranchcode + "') " +
                            "ORDER BY B.SUPPLIER_NAME";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstSupplier.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstSupplier;

        }
        #endregion

        #region To get list of all Inventory based Supplier
        //Used by StockVariance.aspx
        public List<Supplier> GetInventoryBasedSupplier(string strBranchCode)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier.Add(new Supplier("0", string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "Select a.Supplier_Code, a.Supplier_Name From Supplier_Master a WITH (NOLOCK) Inner Join Inventory_Tag b WITH (NOLOCK) On b.Branch_Code = '" + strBranchCode + "'  and a.Supplier_Code = b.Supplier_Code Group by a.Supplier_Code, a.Supplier_Name Order by a.Supplier_Name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstSupplier.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return lstSupplier;
        }
        #endregion

        public List<Supplier> GetSalesBasedSuppliers(string strBranchCode)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier.Add(new Supplier("0", string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "select distinct a.Supplier_Code,a.Supplier_Name from Supplier_Master a WITH (NOLOCK) inner join sales_order_detail b WITH (NOLOCK) on substring(a.supplier_code,1,2) = substring(b.supplier_line_code,1,2) and substring(Document_Number,10,3)= '" + strBranchCode + "' order by supplier_name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstSupplier.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return lstSupplier;
        }

        public List<Supplier> GetSupplierBasedBranch(string BranchCode)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier.Add(new Supplier("0", string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct b.Supplier_Code,s.supplier_name from Branches_Surplus b WITH (NOLOCK) inner join Supplier_Master s WITH (NOLOCK) " +
             " on b.Branch_Code = '" + BranchCode + "' and b.supplier_code=s.Supplier_Code order by b.supplier_code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstSupplier.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return lstSupplier;
        }

        public int AddNewSupplierInvoicesUpload(string SupplierLine)
        {
            int result = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchitemprice_Admin");
                ImpalDB.AddInParameter(cmd, "@Supplier_Line", DbType.String, SupplierLine.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                Log.WriteException(typeof(Suppliers), exp);
            }

            return result;
        }

        #region line code for suppliers
        public List<Supplier> GetLineCodes(string strBranchCode)
        {
            List<Supplier> LineCode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.Supplier_Code,b.supplier_name from Purchase_Order_Header a,supplier_master b where a.supplier_code = b.supplier_code and a.Branch_Code = '" + strBranchCode + "' order by b.supplier_name ";
            LineCode.Add(new Supplier("", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    LineCode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return LineCode;
        }
        #endregion

        #region Getlinewiseorder
        public List<Supplier> Getlinewiseorder(string strBranchCode)
        {
            List<Supplier> linewiseorder = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            String squery = "select distinct a.Supplier_Code,b.Supplier_Name from Purchase_Order_Header a,Supplier_Master b where a.Supplier_Code = b.Supplier_Code and a.Branch_Code = '" + strBranchCode + "' order by supplier_name";
            linewiseorder.Add(new Supplier("0", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(squery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    linewiseorder.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return linewiseorder;
        }

        public List<Supplier> GetlinewiseorderCRP()
        {
            List<Supplier> linewiseorder = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            String squery = "select distinct Supplier_Code,Supplier_Name from Supplier_Master order by supplier_name";
            linewiseorder.Add(new Supplier("0", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(squery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    linewiseorder.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return linewiseorder;
        }
        #endregion

        #region get supplier code
        public List<Supplier> GetSuppliercode()
        {

            List<Supplier> suppliercode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            //string sQuery = "select distinct a.supplier_code,b.supplier_name from supplier_line_master a,supplier_master b where a.supplier_code = b.supplier_code order by b.supplier_name";
            string sQuery = "select supplier_code, supplier_name from supplier_master order by supplier_name";
            suppliercode.Add(new Supplier("0", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    suppliercode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));


                }

            }
            return suppliercode;

        }
        #endregion

        #region get supplier code
        public List<Supplier> GetSuppliercodewithOutDefault()
        {
            List<Supplier> suppliercode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct supplier_code,supplier_name from supplier_master order by supplier_code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    suppliercode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return suppliercode;
        }
        #endregion

        #region get supplier code
        public List<Supplier> GetSupplierForTODGeneration(string BranchCode)
        {
            List<Supplier> suppliercode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "Select distinct t.Supplier_Code Supplier_Code,s.Supplier_Name from TOD_Target_Master t WITH (NOLOCK) inner join Supplier_Master s WITH (NOLOCK) on t.Branch_Code='" + BranchCode + "' and t.Supplier_Code=s.Supplier_Code Order By Supplier_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    suppliercode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }

            }

            return suppliercode;
        }
        #endregion

        #region Item Based Suppliers
        public List<Supplier> ItemBasedSupplier(string branchcode)
        {

            List<Supplier> suppliercode = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct supplier_code,supplier_name from supplier_master,Item_Wise_Sales where substring(item_code,1,3)=supplier_code and Branch_Code = '" + branchcode + "' order by supplier_name";
            suppliercode.Add(new Supplier("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    suppliercode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return suppliercode;
        }
        #endregion

        #region
        public List<Supplier> ConsignmentBasedSupplier(string suppliercode, string strBranchCode)
        {

            List<Supplier> lstsupplier = new List<Supplier>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery =string.Empty;
            if(strBranchCode != "CRP")
           sQuery  = "select distinct Supplier_part_number from V_Consignment where substring(item_code,1,3) = '" + suppliercode + "' and branch_code = '" + strBranchCode + "' order by Supplier_part_number";
            else
                sQuery = "select distinct Supplier_part_number from V_Consignment where substring(item_code,1,3) = '" + suppliercode + "' order by Supplier_part_number";

            lstsupplier.Insert(0, new Supplier(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstsupplier.Add(new Supplier(reader[0].ToString()));
                }
            }
            return lstsupplier;
        }
        #endregion


        #region line code for SLB
        public List<Supplier> GetLineCodeforSLB(string strBranchCode)
        {

            List<Supplier> LineCode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "select distinct a.supplier_line_code,b.Short_Description from SLB_Item_calculation a,Supplier_line_master b where a.Supplier_line_code=b.supplier_line_code and a.Branch_Code = " + "'" + ddlbranchcode.value + "'" + "";
            string sSQL = "select distinct a.supplier_line_code,b.Short_Description from SLB_Item_calculation a,Supplier_line_master b where a.Supplier_line_code=b.supplier_line_code and a.Branch_Code = " + "'" + strBranchCode + "'" + "";
            LineCode.Add(new Supplier("", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    LineCode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));


                }

            }

            return LineCode;
        }
        #endregion


        //Linecode for SLB Summary/Details

        #region line code for SLB summary/details
        public List<Supplier> GetLineCodeforSLBSummary(string strBranchCode)
        {

            List<Supplier> LineCode = new List<Supplier>();

            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "select distinct a.supplier_line_code,b.Short_Description from SLB_Item_calculation a,Supplier_line_master b where a.Supplier_line_code=b.supplier_line_code and a.Branch_Code = " + "'" + ddlbranchcode.value + "'" + "";
            string sSQL = "select distinct c.supplier_code,c.supplier_name,a.supplier_line_code,b.Short_Description from SLB_Item_calculation a,Supplier_line_master b,supplier_master c where a.Supplier_line_code=b.supplier_line_code and substring(a.supplier_line_code,1,3)=c.supplier_code and a.Branch_Code = " + "'" + strBranchCode + "'" + "";
            LineCode.Add(new Supplier("", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    LineCode.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));


                }

            }

            return LineCode;
        }
        #endregion


        #region To Get All the Supplier Fields
        public List<SupplierDetail> ViewSupplier(string Supplier_Code)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSupplier");
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, Supplier_Code.Trim());
            List<SupplierDetail> SupplierList = new List<SupplierDetail>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    SupplierDetail SupplierFields = new SupplierDetail();
                    SupplierFields.Supplier_Code = objReader["Supplier_Code"].ToString();
                    SupplierFields.Supplier_Name = objReader["Supplier_Name"].ToString();
                    SupplierFields.Chart_of_Account_Code = objReader["Chart_of_Account_Code"].ToString();
                    SupplierFields.Party_Type_Code = objReader["Party_Type_Code"].ToString();
                    SupplierFields.Address = objReader["Address"].ToString();
                    SupplierFields.Pincode = objReader["Pincode"].ToString();
                    SupplierFields.Alpha_Code = objReader["Alpha_Code"].ToString();
                    SupplierFields.Phone = objReader["Phone"].ToString();
                    SupplierFields.Fax = objReader["Fax"].ToString();
                    SupplierFields.Email = objReader["Email"].ToString();
                    SupplierFields.Telex = objReader["Telex"].ToString();
                    SupplierFields.Contact_Person = objReader["Contact_Person"].ToString();
                    SupplierFields.Contact_Designation = objReader["Contact_Designation"].ToString();
                    SupplierFields.Carrier = objReader["Carrier"].ToString();
                    SupplierFields.Destination = objReader["Destination"].ToString();
                    SupplierFields.Local_Sales_Tax_Number = objReader["Local_Sales_Tax_Number"].ToString();
                    SupplierFields.Central_Sales_Tax_Number = objReader["Central_Sales_Tax_Number"].ToString();
                    //SupplierFields.BMS_Indicator = objReader["BMS_Indicator"].ToString();
                    //SupplierFields.Group_Company_Indicator = objReader["Group_Company_Indicator"].ToString();
                    SupplierFields.Purchase_Upto_Previous_Year = objReader["Purchase_Upto_Previous_Year"].ToString();
                    SupplierFields.Purchase_During_Previous_Year = objReader["Purchase_During_Previous_Year"].ToString();
                    SupplierFields.Purchase_During_Current_Year = objReader["Purchase_During_Current_Year"].ToString();
                    SupplierFields.Outstanding_Amount = objReader["Outstanding_Amount"].ToString();
                    SupplierFields.Oldest_Pending_Invoice = objReader["Oldest_Pending_Invoice"].ToString();
                    if (objReader["Insurance_Indicator"] != null)
                    {
                        if (objReader["Insurance_Indicator"].ToString() == "Y")
                            SupplierFields.Insurance_Indicator = true;
                        else
                            SupplierFields.Insurance_Indicator = false;
                    }
                    if (objReader["BMS_Indicator"] != null)
                    {
                        if (objReader["BMS_Indicator"].ToString() == "Y")
                            SupplierFields.BMS_Indicator = true;
                        else
                            SupplierFields.BMS_Indicator = false;
                    }

                    if (objReader["Group_Company_Indicator"] != null)
                    {
                        if (objReader["Group_Company_Indicator"].ToString() == "Y")
                            SupplierFields.Group_Company_Indicator = true;
                        else
                            SupplierFields.Group_Company_Indicator = false;
                    }

                    SupplierFields.place = objReader["place"].ToString();
                    SupplierFields.Auto_Locking = objReader["Auto_Locking"].ToString();
                    SupplierFields.Optimum_Stock_value = objReader["Optimum_Stock_value"].ToString();

                    SupplierList.Add(SupplierFields);

                }
            }

            return SupplierList;
        }
        #endregion


        #region To Add New Supplier
        public string AddNewSupplier(SupplierDetail SupplierDetails)
        {
            string SupplierCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSupplier");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierDetails.Supplier_Code);
            ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, SupplierDetails.Chart_of_Account_Code);
            ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, SupplierDetails.Party_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Name", DbType.String, SupplierDetails.Supplier_Name);
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, SupplierDetails.Address);
            ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, SupplierDetails.Pincode);
            ImpalDB.AddInParameter(cmd, "@Alpha_Code", DbType.String, SupplierDetails.Alpha_Code);
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, SupplierDetails.Phone);
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, SupplierDetails.Fax);
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, SupplierDetails.Email);
            ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, SupplierDetails.Telex);
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, SupplierDetails.Contact_Person);
            ImpalDB.AddInParameter(cmd, "@Contact_Designation", DbType.String, SupplierDetails.Contact_Designation);
            ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, SupplierDetails.Carrier);
            ImpalDB.AddInParameter(cmd, "@BMS_Indicator", DbType.String, SupplierDetails.BMS_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@Destination", DbType.String, SupplierDetails.Destination);
            ImpalDB.AddInParameter(cmd, "@Group_Company_Indicator", DbType.String, SupplierDetails.Group_Company_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@Local_Sales_Tax_Number", DbType.String, SupplierDetails.Local_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@Central_Sales_Tax_Number", DbType.String, SupplierDetails.Central_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@Insurance_Indicator", DbType.String, SupplierDetails.Insurance_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@optimum_stock_value", DbType.String, SupplierDetails.Local_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@auto_locking", DbType.String, SupplierDetails.Central_Sales_Tax_Number);
            SupplierCode = ImpalDB.ExecuteScalar(cmd).ToString();

            return SupplierCode;
        }
        #endregion

        #region To Update the Existing supplier
        public void UpdateSupplier(SupplierDetail SupplierDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdSupplier");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierDetails.Supplier_Code);
            ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, SupplierDetails.Chart_of_Account_Code);
            //ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, SupplierDetails.Party_Type_Code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Name", DbType.String, SupplierDetails.Supplier_Name);
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, SupplierDetails.Address);
            ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, SupplierDetails.Pincode);
            ImpalDB.AddInParameter(cmd, "@Alpha_Code", DbType.String, SupplierDetails.Alpha_Code);
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, SupplierDetails.Phone);
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, SupplierDetails.Fax);
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, SupplierDetails.Email);
            ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, SupplierDetails.Telex);
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, SupplierDetails.Contact_Person);
            ImpalDB.AddInParameter(cmd, "@Contact_Designation", DbType.String, SupplierDetails.Contact_Designation);
            ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, SupplierDetails.Carrier);
            ImpalDB.AddInParameter(cmd, "@BMS_Indicator", DbType.String, SupplierDetails.BMS_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@Destination", DbType.String, SupplierDetails.Destination);
            ImpalDB.AddInParameter(cmd, "@Group_Company_Indicator", DbType.String, SupplierDetails.Group_Company_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@Local_Sales_Tax_Number", DbType.String, SupplierDetails.Local_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@Central_Sales_Tax_Number", DbType.String, SupplierDetails.Central_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@Insurance_Indicator", DbType.String, SupplierDetails.Insurance_Indicator == true ? "Y" : "N");
            ImpalDB.AddInParameter(cmd, "@optstkvalue", DbType.String, SupplierDetails.Local_Sales_Tax_Number);
            ImpalDB.AddInParameter(cmd, "@autolocking", DbType.String, SupplierDetails.Central_Sales_Tax_Number);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        #endregion

        #region To Get All Supplier Line
        
        public List<SupplierLineCode> GetAllSuppliersLine()
        {
            List<SupplierLineCode> SupplierLineCodeList = new List<SupplierLineCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select supplier_line_code,Short_Description,Long_Description from supplier_line_master  order by Long_Description";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    SupplierLineCodeList.Add(new SupplierLineCode(reader[0].ToString(), reader[2].ToString()));
                }
            }
            return SupplierLineCodeList;
        }

        #endregion

        #region To Get All Plant Code and Description
        
        public List<PlantCode> GetAllPlant()
        {
            List<PlantCode> PlantCodeList = new List<PlantCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Depot_Code,Depot_Long_Name from Depot_Master order by depot_long_name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    PlantCodeList.Add(new PlantCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return PlantCodeList;
        }

        #endregion

        #region To Get All Product Code and Description
        
        public List<ProductCode> GetAllProduct()
        {
            List<ProductCode> ProductCodeList = new List<ProductCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select supplier_product_code,supplier_Product_name,supplier_product_short_name from supplier_product_master order by supplier_product_short_name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    ProductCodeList.Add(new ProductCode(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }
            }
            return ProductCodeList;
        }

        public List<ProductCode> GetAllProduct(string Suppliercode)
        {
            List<ProductCode> ProductCodeList = new List<ProductCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select supplier_product_code,supplier_Product_name,supplier_product_short_name from supplier_product_master  where supplier_code='" + Suppliercode + "' order by supplier_product_short_name";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                //SupplierBranches.Add(new Supplier("0","-- Select --"));
                while (reader.Read())
                {
                    ProductCodeList.Add(new ProductCode(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }
            }
            return ProductCodeList;
        }

        #endregion




        #region To Get All the Supplier Line Fields
        
        public List<SupplierLineDetails> ViewSupplierLine(string SupplierLine)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Getsupplierline");
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierLine.Trim());
            List<SupplierLineDetails> SupplierList = new List<SupplierLineDetails>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    SupplierLineDetails SupplierFields = new SupplierLineDetails();
                    SupplierFields.supplier_plant_code = objReader["supplier_plant_code"].ToString();
                    SupplierFields.short_description = objReader["short_description"].ToString();
                    SupplierFields.long_description = objReader["long_description"].ToString();
                    SupplierFields.ed_value = objReader["ed_value"].ToString();
                    SupplierFields.ed_indicator = objReader["ed_indicator"].ToString();
                    SupplierFields.order_pattern = objReader["order_pattern"].ToString();
                    SupplierFields.price_revision_days = objReader["price_revision_days"].ToString();
                    SupplierFields.Stock_Verification_Times = objReader["Stock_Verification_Times"].ToString();
                    SupplierFields.Stock_Verification_Duration = objReader["Stock_Verification_Duration"].ToString();
                    SupplierFields.purchase_discount = objReader["purchase_discount"].ToString();
                    SupplierFields.excise_duty_discount = objReader["excise_duty_discount"].ToString();
                    SupplierFields.minimum_items_per_day = objReader["minimum_items_per_day"].ToString();
                    SupplierFields.stock_holding_pattern = objReader["stock_holding_pattern"].ToString();
                    SupplierFields.depot_surcharge = objReader["depot_surcharge"].ToString();
                    SupplierFields.freight_surcharge = objReader["freight_surcharge"].ToString();
                    SupplierFields.additional_discount1 = objReader["additional_discount1"].ToString();
                    SupplierFields.additional_discount2 = objReader["additional_discount2"].ToString();
                    SupplierFields.additional_discount3 = objReader["additional_discount3"].ToString();
                    SupplierFields.additional_discount4 = objReader["additional_discount4"].ToString();
                    SupplierFields.additional_discount5 = objReader["additional_discount5"].ToString();
                    SupplierFields.stock_verification_first_month = objReader["stock_verification_first_month"].ToString();
                    SupplierFields.supplier_line_code = objReader["supplier_line_code"].ToString();
                    SupplierFields.supplier_product_code = objReader["supplier_product_code"].ToString();
                    SupplierFields.Supplier_Name = objReader["Supplier_Name"].ToString();
                    SupplierFields.supplier_code = objReader["supplier_code"].ToString();
                    SupplierFields.supplier_product_short_name = objReader["supplier_product_short_name"].ToString();
                    //SupplierFields.Depot_Payment = objReader["Depot_Payment"].ToString();
                    SupplierFields.Supplier_Classification = objReader["Supplier_Classification"].ToString();
                    SupplierFields.Aging = objReader["Aging"].ToString();
                    SupplierFields.Stock_Transfer_Percentage = objReader["Stock_Transfer_Percentage"].ToString();
                    SupplierFields.DealerDiscount = objReader["dealers_discount"].ToString();


                    if (objReader["Depot_Payment"] != null)
                    {
                        if (objReader["Depot_Payment"].ToString() == "D")
                            SupplierFields.Depot_Payment = true;
                        else
                            SupplierFields.Depot_Payment = false;
                    }


                    SupplierList.Add(SupplierFields);

                }
            }

            return SupplierList;
        }
        #endregion


        #region To Add New Supplier New Supplier Line
        public void AddNewSupplierLine(SupplierLineDetails SupplierDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSupplierLine");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierDetails.supplier_code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Product_Code", DbType.String, SupplierDetails.supplier_product_code);
            ImpalDB.AddInParameter(cmd, "@Supplier_Plant_Code", DbType.String, SupplierDetails.supplier_plant_code);
            ImpalDB.AddInParameter(cmd, "@Short_Description", DbType.String, SupplierDetails.short_description);
            ImpalDB.AddInParameter(cmd, "@Long_Description", DbType.String, SupplierDetails.long_description);
            ImpalDB.AddInParameter(cmd, "@ED_Indicator", DbType.String, SupplierDetails.ed_indicator);
            ImpalDB.AddInParameter(cmd, "@ED_Value", DbType.String, SupplierDetails.ed_value);
            ImpalDB.AddInParameter(cmd, "@Order_Pattern", DbType.String, SupplierDetails.order_pattern);
            ImpalDB.AddInParameter(cmd, "@price_Revision_Days", DbType.String, SupplierDetails.price_revision_days);
            ImpalDB.AddInParameter(cmd, "@Stock_Verification_Times", DbType.String, SupplierDetails.Stock_Verification_Times);
            ImpalDB.AddInParameter(cmd, "@Stock_Verification_Duration", DbType.String, SupplierDetails.Stock_Verification_Duration);
            ImpalDB.AddInParameter(cmd, "@Purchase_Discount", DbType.String, SupplierDetails.purchase_discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Discount", DbType.String, SupplierDetails.excise_duty_discount);
            ImpalDB.AddInParameter(cmd, "@Minimum_Items_Per_Day", DbType.String, SupplierDetails.minimum_items_per_day);
            ImpalDB.AddInParameter(cmd, "@stock_holding_pattern", DbType.String, SupplierDetails.stock_holding_pattern);
            ImpalDB.AddInParameter(cmd, "@depot_surcharge", DbType.String, SupplierDetails.depot_surcharge);
            ImpalDB.AddInParameter(cmd, "@freight_surcharge", DbType.String, SupplierDetails.freight_surcharge);
            ImpalDB.AddInParameter(cmd, "@Depot_Payment", DbType.String, SupplierDetails.Depot_Payment);
            ImpalDB.AddInParameter(cmd, "@additional_discount1", DbType.String, SupplierDetails.additional_discount1);
            ImpalDB.AddInParameter(cmd, "@additional_discount2", DbType.String, SupplierDetails.additional_discount2);
            ImpalDB.AddInParameter(cmd, "@additional_discount3", DbType.String, SupplierDetails.additional_discount3);
            ImpalDB.AddInParameter(cmd, "@additional_discount4", DbType.String, SupplierDetails.additional_discount4);
            ImpalDB.AddInParameter(cmd, "@additional_discount5", DbType.String, SupplierDetails.additional_discount5);
            ImpalDB.AddInParameter(cmd, "@stock_verification_first_month", DbType.String, SupplierDetails.stock_verification_first_month);
            ImpalDB.AddInParameter(cmd, "@dealers_discount", DbType.String, SupplierDetails.DealerDiscount);
            ImpalDB.AddInParameter(cmd, "@Supplier_Classification", DbType.String, SupplierDetails.Supplier_Classification);
            ImpalDB.AddInParameter(cmd, "@Aging", DbType.String, SupplierDetails.Aging);
            ImpalDB.AddInParameter(cmd, "@Stock_Transfer_Percentage", DbType.String, SupplierDetails.Stock_Transfer_Percentage);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }
        #endregion


        #region To Update Supplier Line
        public void UpdateSupplierLine(SupplierLineDetails SupplierDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdSupplierLine");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierDetails.supplier_line_code);
            //ImpalDB.AddInParameter(cmd, "@Supplier_Product_Code", DbType.String, SupplierDetails.supplier_product_code);
            //ImpalDB.AddInParameter(cmd, "@Supplier_Plant_Code", DbType.String, SupplierDetails.supplier_plant_code);
            ImpalDB.AddInParameter(cmd, "@Short_Description", DbType.String, SupplierDetails.short_description);
            ImpalDB.AddInParameter(cmd, "@Long_Description", DbType.String, SupplierDetails.long_description);
            ImpalDB.AddInParameter(cmd, "@ED_Indicator", DbType.String, SupplierDetails.ed_indicator);
            ImpalDB.AddInParameter(cmd, "@ED_Value", DbType.String, SupplierDetails.ed_value);
            ImpalDB.AddInParameter(cmd, "@Order_Pattern", DbType.String, SupplierDetails.order_pattern);
            ImpalDB.AddInParameter(cmd, "@price_Revision_Days", DbType.String, SupplierDetails.price_revision_days);
            ImpalDB.AddInParameter(cmd, "@Stock_Verification_Times", DbType.String, SupplierDetails.Stock_Verification_Times);
            ImpalDB.AddInParameter(cmd, "@Stock_Verification_Duration", DbType.String, SupplierDetails.Stock_Verification_Duration);
            ImpalDB.AddInParameter(cmd, "@Purchase_Discount", DbType.String, SupplierDetails.purchase_discount);
            ImpalDB.AddInParameter(cmd, "@Excise_Duty_Discount", DbType.String, SupplierDetails.excise_duty_discount);
            ImpalDB.AddInParameter(cmd, "@Minimum_Items_Per_Day", DbType.String, SupplierDetails.minimum_items_per_day);
            ImpalDB.AddInParameter(cmd, "@stock_holding_pattern", DbType.String, SupplierDetails.stock_holding_pattern);
            ImpalDB.AddInParameter(cmd, "@depot_surcharge", DbType.String, SupplierDetails.depot_surcharge);
            ImpalDB.AddInParameter(cmd, "@freight_surcharge", DbType.String, SupplierDetails.freight_surcharge);
            ImpalDB.AddInParameter(cmd, "@Depot_Payment", DbType.String, SupplierDetails.Depot_Payment);
            ImpalDB.AddInParameter(cmd, "@additional_discount1", DbType.String, SupplierDetails.additional_discount1);
            ImpalDB.AddInParameter(cmd, "@additional_discount2", DbType.String, SupplierDetails.additional_discount2);
            ImpalDB.AddInParameter(cmd, "@additional_discount3", DbType.String, SupplierDetails.additional_discount3);
            ImpalDB.AddInParameter(cmd, "@additional_discount4", DbType.String, SupplierDetails.additional_discount4);
            ImpalDB.AddInParameter(cmd, "@additional_discount5", DbType.String, SupplierDetails.additional_discount5);
            ImpalDB.AddInParameter(cmd, "@stock_verification_first_month", DbType.String, SupplierDetails.stock_verification_first_month);
            ImpalDB.AddInParameter(cmd, "@dealers_discount", DbType.String, SupplierDetails.DealerDiscount);
            ImpalDB.AddInParameter(cmd, "@Supplier_Classification", DbType.String, SupplierDetails.Supplier_Classification);
            ImpalDB.AddInParameter(cmd, "@Aging", DbType.String, SupplierDetails.Aging);
            ImpalDB.AddInParameter(cmd, "@Stock_Transfer_Percentage", DbType.String, SupplierDetails.Stock_Transfer_Percentage);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }
        #endregion




    }

    public class Supplier
    {
        public Supplier(string SupplierCode, string SupplierName)
        {
            _SupplierCode = SupplierCode;
            _SupplierName = SupplierName;
        }
        public Supplier()
        {

        }
        public Supplier(string SupplierCode)
        {
            _SupplierCode = SupplierCode;
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

    public class SupplierLineCode
    {
        public SupplierLineCode(string Supplier_Line_Code, string Long_Description)
        {
            _Supplier_Line_Code = Supplier_Line_Code;
            _Long_Description = Long_Description;

        }

        public SupplierLineCode(string Supplier_Line_Code, string Short_Description, string Long_Description)
        {
            _Supplier_Line_Code = Supplier_Line_Code;
            _Long_Description = Long_Description;
            _Short_Description = Short_Description;

        }

        private string _Supplier_Line_Code;
        private string _Long_Description;
        private string _Short_Description;

        public string Supplier_Line_Code
        {
            get { return _Supplier_Line_Code; }
            set { _Supplier_Line_Code = value; }
        }

        public string Long_Description
        {
            get { return _Long_Description; }
            set { _Long_Description = value; }
        }

        public string Short_Description
        {
            get { return _Short_Description; }
            set { _Short_Description = value; }
        }
    }


    public class PlantCode
    {
        public PlantCode(string PlantCodevalue, string PlantDesc)
        {
            _PlantCodevalue = PlantCodevalue;
            _PlantDesc = PlantDesc;

        }

        private string _PlantCodevalue;
        private string _PlantDesc;

        public string PlantCodevalue
        {
            get { return _PlantCodevalue; }
            set { _PlantCodevalue = value; }
        }

        public string PlantDesc
        {
            get { return _PlantDesc; }
            set { _PlantDesc = value; }
        }


    }

    public class ProductCode
    {
        public ProductCode(string ProductCodevalue, string ProductDesc, string ProductShortDesc)
        {
            _ProductCodevalue = ProductCodevalue;
            _ProductDesc = ProductDesc;
            _ProductShortDesc = ProductShortDesc;

        }

        private string _ProductCodevalue;
        private string _ProductDesc;
        private string _ProductShortDesc;

        public string ProductCodevalue
        {
            get { return _ProductCodevalue; }
            set { _ProductCodevalue = value; }
        }

        public string ProductDesc
        {
            get { return _ProductDesc; }
            set { _ProductDesc = value; }
        }

        public string ProductShortDesc
        {
            get { return _ProductShortDesc; }
            set { _ProductShortDesc = value; }
        }


    }

    #region Supplier Fields
    public class SupplierDetail
    {
        public SupplierDetail()
        {

        }

        private string _Supplier_Code;
        private string _Supplier_Name;
        private string _Chart_of_Account_Code;
        private string _Party_Type_Code;
        private string _Address;
        private string _Pincode;
        private string _Alpha_Code;
        private string _Phone;
        private string _Fax;
        private string _Email;
        private string _Telex;
        private string _Contact_Person;
        private string _Contact_Designation;
        private string _Carrier;
        private string _Destination;
        private string _Local_Sales_Tax_Number;
        private string _Central_Sales_Tax_Number;
        private bool _BMS_Indicator;
        private string _Purchase_Upto_Previous_Year;
        private string _Purchase_During_Previous_Year;
        private string _Purchase_During_Current_Year;
        private string _Outstanding_Amount;
        private string _Oldest_Pending_Invoice;
        private bool _Insurance_Indicator;
        private string _place;
        private string _Auto_Locking;
        private string _Optimum_Stock_value;
        private bool _Group_Company_Indicator;


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

        public string Chart_of_Account_Code
        {
            get { return _Chart_of_Account_Code; }
            set { _Chart_of_Account_Code = value; }
        }

        public string Party_Type_Code
        {
            get { return _Party_Type_Code; }
            set { _Party_Type_Code = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }

        public string Alpha_Code
        {
            get { return _Alpha_Code; }
            set { _Alpha_Code = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Telex
        {
            get { return _Telex; }
            set { _Telex = value; }
        }

        public string Contact_Person
        {
            get { return _Contact_Person; }
            set { _Contact_Person = value; }
        }

        public string Contact_Designation
        {
            get { return _Contact_Designation; }
            set { _Contact_Designation = value; }
        }

        public string Carrier
        {
            get { return _Carrier; }
            set { _Carrier = value; }
        }

        public string Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }

        public string Local_Sales_Tax_Number
        {
            get { return _Local_Sales_Tax_Number; }
            set { _Local_Sales_Tax_Number = value; }
        }

        public string Central_Sales_Tax_Number
        {
            get { return _Central_Sales_Tax_Number; }
            set { _Central_Sales_Tax_Number = value; }
        }

        public bool BMS_Indicator
        {
            get { return _BMS_Indicator; }
            set { _BMS_Indicator = value; }
        }

        public string Purchase_Upto_Previous_Year
        {
            get { return _Purchase_Upto_Previous_Year; }
            set { _Purchase_Upto_Previous_Year = value; }
        }

        public string Purchase_During_Previous_Year
        {
            get { return _Purchase_During_Previous_Year; }
            set { _Purchase_During_Previous_Year = value; }
        }

        public string Purchase_During_Current_Year
        {
            get { return _Purchase_During_Current_Year; }
            set { _Purchase_During_Current_Year = value; }
        }

        public string Outstanding_Amount
        {
            get { return _Outstanding_Amount; }
            set { _Outstanding_Amount = value; }
        }

        public string Oldest_Pending_Invoice
        {
            get { return _Oldest_Pending_Invoice; }
            set { _Oldest_Pending_Invoice = value; }
        }

        public bool Insurance_Indicator
        {
            get { return _Insurance_Indicator; }
            set { _Insurance_Indicator = value; }
        }

        public string place
        {
            get { return _place; }
            set { _place = value; }
        }

        public string Auto_Locking
        {
            get { return _Auto_Locking; }
            set { _Auto_Locking = value; }
        }

        public string Optimum_Stock_value
        {
            get { return _Optimum_Stock_value; }
            set { _Optimum_Stock_value = value; }
        }

        public bool Group_Company_Indicator
        {
            get { return _Group_Company_Indicator; }
            set { _Group_Company_Indicator = value; }
        }


    }
    #endregion


    #region Supplier Fields
    public class SupplierLineDetails
    {
        public SupplierLineDetails()
        {

        }

        private string _supplier_plant_code;
        private string _short_description;
        private string _long_description;
        private string _ed_indicator;
        private string _ed_value;
        private string _order_pattern;
        private string _price_revision_days;
        private string _Stock_Verification_Times;
        private string _Stock_Verification_Duration;
        private string _purchase_discount;
        private string _excise_duty_discount;
        private string _minimum_items_per_day;
        private string _stock_holding_pattern;
        private string _depot_surcharge;
        private string _freight_surcharge;
        private string _additional_discount1;
        private string _additional_discount2;
        private string _additional_discount3;
        private string _additional_discount4;
        private string _additional_discount5;
        private string _stock_verification_first_month;
        private string _supplier_line_code;
        private string _supplier_code;
        private string _supplier_product_code;
        private string _Supplier_Name;
        private string _supplier_product_short_name;
        private bool _Depot_Payment;
        private string _Supplier_Classification;
        private string _Aging;
        private string _Stock_Transfer_Percentage;
        private string _DealerDiscount;

        public string DealerDiscount
        {
            get { return _DealerDiscount; }
            set { _DealerDiscount = value; }
        }

        public string supplier_plant_code
        {
            get { return _supplier_plant_code; }
            set { _supplier_plant_code = value; }
        }

        public string short_description
        {
            get { return _short_description; }
            set { _short_description = value; }
        }

        public string long_description
        {
            get { return _long_description; }
            set { _long_description = value; }
        }

        public string ed_indicator
        {
            get { return _ed_indicator; }
            set { _ed_indicator = value; }
        }

        public string ed_value
        {
            get { return _ed_value; }
            set { _ed_value = value; }
        }

        public string order_pattern
        {
            get { return _order_pattern; }
            set { _order_pattern = value; }
        }

        public string price_revision_days
        {
            get { return _price_revision_days; }
            set { _price_revision_days = value; }
        }

        public string Stock_Verification_Times
        {
            get { return _Stock_Verification_Times; }
            set { _Stock_Verification_Times = value; }
        }

        public string Stock_Verification_Duration
        {
            get { return _Stock_Verification_Duration; }
            set { _Stock_Verification_Duration = value; }
        }

        public string purchase_discount
        {
            get { return _purchase_discount; }
            set { _purchase_discount = value; }
        }

        public string excise_duty_discount
        {
            get { return _excise_duty_discount; }
            set { _excise_duty_discount = value; }
        }

        public string minimum_items_per_day
        {
            get { return _minimum_items_per_day; }
            set { _minimum_items_per_day = value; }
        }

        public string stock_holding_pattern
        {
            get { return _stock_holding_pattern; }
            set { _stock_holding_pattern = value; }
        }

        public string depot_surcharge
        {
            get { return _depot_surcharge; }
            set { _depot_surcharge = value; }
        }

        public string freight_surcharge
        {
            get { return _freight_surcharge; }
            set { _freight_surcharge = value; }
        }

        public string additional_discount1
        {
            get { return _additional_discount1; }
            set { _additional_discount1 = value; }
        }

        public string additional_discount2
        {
            get { return _additional_discount2; }
            set { _additional_discount2 = value; }
        }

        public string additional_discount3
        {
            get { return _additional_discount3; }
            set { _additional_discount3 = value; }
        }

        public string additional_discount4
        {
            get { return _additional_discount4; }
            set { _additional_discount4 = value; }
        }

        public string additional_discount5
        {
            get { return _additional_discount5; }
            set { _additional_discount5 = value; }
        }

        public string stock_verification_first_month
        {
            get { return _stock_verification_first_month; }
            set { _stock_verification_first_month = value; }
        }

        public string supplier_line_code
        {
            get { return _supplier_line_code; }
            set { _supplier_line_code = value; }
        }

        public string supplier_code
        {
            get { return _supplier_code; }
            set { _supplier_code = value; }
        }

        public string supplier_product_code
        {
            get { return _supplier_product_code; }
            set { _supplier_product_code = value; }
        }

        public string Supplier_Name
        {
            get { return _Supplier_Name; }
            set { _Supplier_Name = value; }
        }

        public string supplier_product_short_name
        {
            get { return _supplier_product_short_name; }
            set { _supplier_product_short_name = value; }
        }

        public bool Depot_Payment
        {
            get { return _Depot_Payment; }
            set { _Depot_Payment = value; }
        }

        public string Supplier_Classification
        {
            get { return _Supplier_Classification; }
            set { _Supplier_Classification = value; }
        }

        public string Aging
        {
            get { return _Aging; }
            set { _Aging = value; }
        }

        public string Stock_Transfer_Percentage
        {
            get { return _Stock_Transfer_Percentage; }
            set { _Stock_Transfer_Percentage = value; }
        }


    }
    #endregion
}
