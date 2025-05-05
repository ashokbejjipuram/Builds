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
    public class Towns
    {
        public void AddNewTowns(string TownName, string BrCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addtown");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Town_Name", DbType.String, TownName.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BrCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateTown(string Towncode, string TownName,string BrName)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updtown");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, Towncode.Trim());
            ImpalDB.AddInParameter(cmd, "@Town_Name", DbType.String, TownName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<Town> GetAllTowns()
        {
            List<Town> TownList = new List<Town>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " SELECT   [Town_Code], [Town_Name]  , [dbo].[Branch_Master].[Branch_Code], [Branch_Name] " +
                " FROM  [dbo].[Town_Master], [dbo].[Branch_Master] where  [dbo].[Town_Master].[Branch_code] " +
                " = [dbo].[Branch_Master].[Branch_Code] Order By [Town_Code], [Town_Name]  , [dbo].[Branch_Master].[Branch_Code], [Branch_Name] ";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TownList.Add(new Town(reader["Town_Code"].ToString(), reader["Town_Name"].ToString(), reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                }
            }

            return TownList;
        }

        public List<Town> GetAllTownsBranch(string BranchCode)
        {
            List<Town> TownList = new List<Town>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "SELECT b.Town_Code, b.Town_Name, a.Branch_Code, a.Branch_Name FROM Branch_Master a WITH (NOLOCK) inner join Town_Master b WITH (NOLOCK) " +
                          "on a.Branch_Code='" + BranchCode + "' and a.Branch_code=b.Branch_Code Order By b.Town_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TownList.Add(new Town(reader["Town_Code"].ToString(), reader["Town_Name"].ToString(), reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                }
            }

            return TownList;
        }

        /// <summary>
        /// Get the list of towns based on Branch Code and Customer Code
        /// </summary>
        /// <param name="BranchCode"></param>
        /// <returns></returns>
        public List<Town> GetBranchBasedTowns(string BranchCode)
        {
            List<Town> lstTowns = new List<Town>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            if (string.IsNullOrEmpty(BranchCode))//For Corporate User
                sQuery = "SELECT DISTINCT TOWN_CODE, TOWN_NAME FROM TOWN_MASTER WITH (NOLOCK) ORDER BY TOWN_NAME";
            else
                sQuery = "SELECT DISTINCT T.TOWN_CODE,T.TOWN_NAME FROM TOWN_MASTER T WITH (NOLOCK) INNER JOIN CUSTOMER_MASTER C WITH (NOLOCK) ON C.Branch_Code = '" + BranchCode + "' and C.BRANCH_CODE=T.BRANCH_CODE and T.TOWN_CODE=C.TOWN_CODE ORDER BY TOWN_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstTowns == null)
                    lstTowns = new List<Town>();
                while (reader.Read())
                    lstTowns.Add(new Town(reader[0].ToString(), reader[1].ToString()));
                return lstTowns;
            }
        }

        #region GetAllTowns
        /// <summary>
        /// Get the list of towns based on Branch Code
        /// </summary>
        /// <returns></returns>
        public List<Town> GetAllTowns(string BranchCode)
        {
            List<Town> lstTowns = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            if (string.IsNullOrEmpty(BranchCode))//For Corporate User
                sQuery = "SELECT DISTINCT TOWN_CODE, TOWN_NAME FROM TOWN_MASTER WITH (NOLOCK) ORDER BY TOWN_NAME";
            else
                sQuery = "SELECT DISTINCT TOWN_CODE,TOWN_NAME FROM TOWN_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '" + BranchCode + "' ORDER BY TOWN_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstTowns == null)
                    lstTowns = new List<Town>();
                    lstTowns.Add(new Town("0", string.Empty));
                while (reader.Read())
                    lstTowns.Add(new Town(reader[0].ToString(), reader[1].ToString()));
                //return lstTowns;
            }
            return lstTowns;
        }
        #endregion

    }

    public class Town
    {
        public Town(string Towncode, string TownName, string BrCode,string BrName )
        {
            _Towncode = Towncode;
            _TownName = TownName;
            _BrCode = BrCode;
            _BrName = BrName;
        }

        public Town(string Towncode, string TownName)
        {
            _Towncode = Towncode;
            _TownName = TownName;
        }

        public Town()
        {
            _Towncode = Towncode;
            _TownName = TownName;
        }

        private string _Towncode;
        private string _TownName;
        private string _BrCode;
        private string _BrName;


        public string Towncode 
        {
            get { return _Towncode; }
            set { _Towncode = value; }
        }
        public string TownName
        {
            get { return _TownName; }
            set { _TownName = value; }
        }

        public string BrCode
        {
            get { return _BrCode; }
            set { _BrCode = value; }
        }

        public string BrName
        {
            get { return _BrName; }
            set { _BrName = value; }
        }



    }
}
