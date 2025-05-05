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
    public class Branches
    {
        #region To get list of all branches
        public List<Branch> GetAllBranch()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' order by branch_name";
            
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {                   
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));                    
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchECreditApplicationParent()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode not in ('COR','CRP') order by branch_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }


        public List<Branch> GetAllBranchECreditApplication(string userid)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode not in ('COR','CRP')" +
                          "inner join Ecredit_Application_Authority_Matrix e with (nolock) on e.Branch_Codes like '%' + b.Branch_Code + '%' and e.userid='" + userid + "' order by b.branch_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchNew()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' order by branch_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranches(string zonecode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "";

            if (zonecode == "--All--")
                sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' order by branch_name";
            else
                sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' inner join State_Master s with (nolock) on s.State_Code=b.State_Code and s.Zone_Code=" + zonecode + " order by branch_name";

            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchGST()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' order by branch_name";

            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchStateOnline(string StateCode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.branch_code,b.branch_name from branch_master b with (nolock) inner join Users u with (nolock) on b.Branch_Code=u.BranchCode and u.Password is not null and u.BranchCode<>'CRP' and b.State_Code=" + StateCode + " order by branch_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranches()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_code,branch_name from branch_master with (nolock) Where Branch_Code not in ('COR','CRP') and Status='A' order by branch_name";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchesHO()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_code,branch_name from branch_master with (nolock) Where Branch_Code not in ('CRP') and Status='A' order by branch_name";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetCostToCostSuppliers(string strBranchCode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "select distinct GST_Supplier_Code,Supplier_Name from GST_Supplier_Master inner jon Where Supplier_Code in ('990')"; // Union All select distinct branch_code,branch_name from branch_master order by Supplier_Code";
            //string sSQL = "select distinct GST_Supplier_Code,Supplier_Name from GST_Supplier_Master g inner join Branch_Master b On b.Branch_Code='" + strBranchCode + "' and g.Supplier_Code in ('990') and g.State_Code=b.State_Code and b.Termination_Notice_Period=2";
            string sSQL = "select distinct GST_Supplier_Code,Supplier_Name from GST_Supplier_Master g with (nolock) inner join Branch_Master b with (nolock) On b.Branch_Code='" + strBranchCode + "' and g.Supplier_Code in ('990') and b.Termination_Notice_Period=2";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;

        }
		
		public List<Branch> GetAllBranchesAcc()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_code,branch_name from branch_master with (nolock) Where Status='A' order by branch_name";
            Branchlist.Add(new Branch("0", "--ALL--"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return Branchlist;

        }

        public List<Branch> GetAllBranchesCRP()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Branch_Code,branch_name from branch_master with (nolock) Where Status='A' order by branch_code";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchesAdmin()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct u.BranchCode, b.Branch_Name from Users u with (nolock) inner join Branch_Master b with (nolock) on u.BranchCode not in ('CRP') and u.BranchCode=b.Branch_Code order by u.BranchCode";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchesHOAdmin()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct u.BranchCode, b.Branch_Name from Users u with (nolock) inner join Branch_Master b with (nolock) on u.BranchCode not in ('CRP','COR') and u.BranchCode=b.Branch_Code order by u.BranchCode";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchesDrCrHOAdmin()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct u.BranchCode, b.Branch_Name from Users u inner join Branch_Master b on u.BranchCode not in ('CRP') and u.BranchCode=b.Branch_Code order by u.BranchCode";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranchesCBPayment(string strBranchCode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct u.BranchCode, b.Branch_Name from Users u with (nolock) inner join Branch_Master b with (nolock) on u.BranchCode not in ('COR','CRP','" + strBranchCode + "') and u.BranchCode=b.Branch_Code order by u.BranchCode";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetAllBranches_Area()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_code,branch_name from branch_master with (nolock) Where Status='A' order by branch_name";
            Branchlist.Add(new Branch("", ""));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return Branchlist;

        }

        public List<Branch> GetChqIssueBranchesVendorBooking(string strBranchCode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select b.Cheque_Issue_Authority_Branch, bm.Branch_Name from Cheque_Issue_Authority_Branches b WITH (NOLOCK) inner join Branch_Master bm WITH (NOLOCK) on b.Sub_Branches like '%" + strBranchCode + "%' and b.Cheque_Issue_Authority_Branch=bm.Branch_Code and b.Status='A' Order By SNo";
            //Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }

        public List<Branch> GetChqIssueBranchesInterBranch(string strBranchCode)
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (strBranchCode == "COR")
                sSQL = "select distinct u.BranchCode, b.Branch_Name from Users u with (nolock) inner join Branch_Master b with (nolock) on u.BranchCode not in ('COR','CRP','" + strBranchCode + "') and u.BranchCode=b.Branch_Code order by u.BranchCode";
            else
                sSQL = "select distinct b.Branch_Code,b.Branch_Name from Cheque_Issue_Authority_Branches c with (nolock) inner join Branch_Master b with (nolock) on c.Cheque_Issue_Authority_Branch like '%" + strBranchCode + "%' and c.Sub_Branches like '%' + b.Branch_Code + '%' and b.Branch_Code<>'" + strBranchCode + "' order by b.Branch_Name";

            //Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Branchlist;
        }
        #endregion

        #region To get list of branch Name based on branch Code
        public Branch GetBranchName(string strBranchCode)
        {
            Branch objBranchName = new Branch();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "SELECT branch_name, isnull(Road_Permit_Indicator,0) Road_Permit_Indicator FROM branch_master WHERE branch_code ='" + strBranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        objBranchName.BranchName = reader["branch_name"].ToString();
                        objBranchName.Road_Permit_Indicator = reader["Road_Permit_Indicator"].ToString();
                    }

                }


            }
            catch (Exception exp)
            {
                throw exp;
            }
            return objBranchName;
        }
        #endregion

        #region To get list of line wise branches


        public List<Branch> GetAllLinewiseBranches(string supplierCode, string QueryType, string BranchCode)
        {
            List<Branch> LinewiseSuppliers = new List<Branch>();
            string sSQL = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            switch (QueryType)
            {
                case "WithLine":
                    sSQL = "select distinct b.Branch_Code,b.branch_name from Line_wise_sales a,Branch_master b where a.branch_code =b.branch_code and " +
                   " substring(a.Supplier_line_code,1,3) = '" + supplierCode + "' and b.Branch_Code='" + BranchCode + "'";
                    break;
                //This query to added to support the SLB Details in the Master screen  
                case "OnlyBranch":
                    sSQL = "SELECT distinct Branch_code,branch_Name from Branch_Master where Branch_Code='" + BranchCode + "'";
                    break;
                default:
                    sSQL = "select distinct b.Branch_Code,b.Branch_Name from Line_Wise_Sales a,Branch_Master b where a.Branch_Code = b.Branch_Code and b.Branch_Code='" + BranchCode + "' order by Branch_name";
                    break;
            }

            //LinewiseSuppliers.Add(new Branch("0",string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    LinewiseSuppliers.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return LinewiseSuppliers;
        }
        #endregion

    
        #region GetStateBasedBranch
        /// <summary>
        /// Gets the list of all branches based on State
        /// </summary>
        /// <param name="StateCode"></param>
        /// <returns></returns>
        public List<Branch> GetStateBasedBranch(string StateCode)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER WHERE STATE_CODE = " + StateCode;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstBranch;
        }
        #endregion

        #region GetStateBasedBranch
        /// <summary>
        /// Gets the list of all branches based on State
        /// </summary>
        /// <param name="StateCode"></param>
        /// <returns></returns>
        public List<Branch> GetBranch(string StateCode)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER WHERE STATE_CODE = " + StateCode;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstBranch;
        }
        #endregion

        #region GetStateBasedBranch
        /// <summary>
        /// Gets the State and Zone Details of a Branch
        /// </summary>
        /// <param name="StateCode"></param>
        /// <returns></returns>
        public Branch GetBranchDetails(string BranchCode)
        {
            Branch oBranch = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT B.BRANCH_NAME, S.STATE_NAME, Z.ZONE_NAME, B.BRANCH_CODE, S.STATE_CODE, Z.ZONE_CODE FROM BRANCH_MASTER B WITH (NOLOCK) " +
                            "INNER JOIN STATE_MASTER S WITH (NOLOCK) ON B.BRANCH_CODE = '" + BranchCode + "' AND S.STATE_CODE = B.STATE_CODE " +
                            "INNER JOIN ZONE_MASTER Z WITH (NOLOCK) ON Z.ZONE_CODE = S.ZONE_CODE";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    oBranch = new Branch();
                    oBranch.BranchName = reader[0].ToString();
                    oBranch.State = reader[1].ToString();
                    oBranch.Zone = reader[2].ToString();
                    oBranch.BranchCode = reader[3].ToString();
                    oBranch.StateCode = reader[4].ToString();
                    oBranch.ZoneCode = reader[5].ToString();                    
                }
            }
            return oBranch;
        }
        #endregion

        public List<Branch> GetBranchBasedOnZone(string strZoneCode)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT bch.branch_code,bch.Branch_Name FROM Branch_Master bch with (nolock) inner join State_Master sta with (nolock) on bch.State_Code=sta.State_Code "
                            + "inner join Zone_Master zne with (nolock) on sta.Zone_Code=zne.Zone_Code WHERE zne.Zone_Code=" + strZoneCode;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                lstBranch.Add(new Branch("0", ""));   
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString()));      
                }
            }
            return lstBranch;
        }
        public List<Branch> GetCorpBranch()
        {
            List<Branch> Branchlist = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select branch_code,branch_name from branch_master with (nolock) Where Status='A' order by branch_name";
            Branchlist.Add(new Branch("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Branchlist.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return Branchlist;

        }
        public List<Branch> GetBranchAndZone(string strBranchCode)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT Zn.Zone_Code,Zn.Zone_Name,Br.Branch_Code,br.Branch_Name from Zone_Master Zn with (nolock) inner join State_Master st with (nolock) on zn.Zone_Code=st.Zone_Code inner join Branch_Master br on br.State_Code=st.State_Code"
                                + " where br.Branch_Code='" + strBranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                }
            }
            return lstBranch;
        }

        public List<Branch> GetBranchBasedonZoneState(string strZone)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select a.Branch_Code,a.Branch_Name from Branch_Master a with (nolock),State_Master b with (nolock),Zone_Master c with (nolock) where a.State_Code=b.State_Code and b.Zone_Code=c.Zone_Code and c.Zone_Code =" + strZone;
            lstBranch.Add(new Branch(" ",string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstBranch;
        }
        #region To get branch codes
        public List<Branch> Orderbranchcodes()
        {
            List<Branch> Orderbranchcodes = new List<Branch>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.Branch_code, b.Branch_Name from Item_wise_sales a,Branch_master b where a.Branch_Code= b.Branch_Code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                Orderbranchcodes.Add(new Branch("", ""));
                while (reader.Read())
                {
                    Orderbranchcodes.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Orderbranchcodes;
        }

        public List<Branch> OrderbranchcodesCRP()
        {
            List<Branch> Orderbranchcodes = new List<Branch>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct b.Branch_code, b.Branch_Name from Users a inner join Branch_master b On a.BranchCode= b.Branch_Code and a.BranchCode not in ('COR','CRP') order by b.Branch_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                Orderbranchcodes.Add(new Branch("", ""));
                while (reader.Read())
                {
                    Orderbranchcodes.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return Orderbranchcodes;
        }
        #endregion

        #region To get branchcode for SLB
        public List<Branch> SLBbranchcode()
        {

            List<Branch> SLBbranchcode = new List<Branch>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.Branch_Code,b.branch_name from SLB_Item_calculation a,branch_master b where a.Branch_Code=b.branch_code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                SLBbranchcode.Add(new Branch("", ""));
                while (reader.Read())
                {

                    SLBbranchcode.Add(new Branch(reader[0].ToString(), reader[1].ToString()));

                }

            }

            return SLBbranchcode;
        }
        #endregion

        public List<Branch> GetBranchFromChartofAccount()
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct a.Branch_Code,b.Branch_Name from Chart_Of_Account a with (nolock),Branch_Master b with (nolock) where a.Branch_Code=b.Branch_Code";
            lstBranch.Add(new Branch(" ",string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBranch.Add(new Branch(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstBranch;
        }

             
    }
    public class Branch
    {

        public Branch(string BranchCode, string BranchName)
        {
            _BranchCode = BranchCode;
            _BranchName = BranchName;
        }

        public Branch(string BranchCode, string BranchName, string Road_Permit_Indicator)
        {
            _BranchCode = BranchCode;
            _BranchName = BranchName;
            _Road_Permit_Indicator = Road_Permit_Indicator;

        }
        public Branch(string ZoneCode,string ZoneName,string BranchCode, string BranchName)
        {
            _BranchCode = BranchCode;
            _BranchName = BranchName;
            _ZoneCode = ZoneCode;
            _ZoneName = ZoneName; 

        }
        
        public Branch()
        {

        }

        private string _BranchCode;
        private string _BranchName;
        private string _Road_Permit_Indicator;
        private string _State;
        private string _Zone;
        private string _ZoneCode;
        private string _ZoneName;
		private string _StateCode;

        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }				  

        public string BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }

        public string Road_Permit_Indicator
        {
            get { return _Road_Permit_Indicator; }
            set { _Road_Permit_Indicator = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string Zone
        {
            get { return _Zone; }
            set { _Zone = value; }
        }

        public string ZoneCode
        {
            get { return _ZoneCode; }
            set { _ZoneCode = value; }
        }

        public string ZoneName
        {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }

    }
}
