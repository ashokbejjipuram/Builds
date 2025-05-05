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
    public class StateMasters
    {
        public void AddNewState(string StateCode,string StateName,string ZoneCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddState");

            ImpalDB.AddInParameter(cmd, "@Zone_code", DbType.String, ZoneCode.Trim());
            ImpalDB.AddInParameter(cmd, "@State_name", DbType.String, StateName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }

        public void UpdateState(string StateCode, string StateName, string ZoneCode, string ZoneName)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdState");

            ImpalDB.AddInParameter(cmd, "@State_Code", DbType.String, StateCode.Trim());
            ImpalDB.AddInParameter(cmd, "@State_Name", DbType.String, StateName.Trim());
            ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.String, ZoneCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<StateMaster> GetAllStates()
        {
            List<StateMaster> StateList = new List<StateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select  a.State_code, a.State_Name,b.Zone_code, b.Zone_name from State_Master a WITH (NOLOCK) Inner Join Zone_Master b WITH (NOLOCK) " +
                          "on a.Zone_Code = b.Zone_Code Order By a.State_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    StateList.Add(new StateMaster(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                }

                return StateList;
            }
        }

        public List<StateMaster> GetAllStatesOnline()
        {
            List<StateMaster> StateList = new List<StateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.State_code, a.State_Name,b.Zone_code, b.Zone_name from State_Master a WITH (NOLOCK) Inner Join Zone_Master b WITH (NOLOCK) " +
                          "on a.Zone_Code = b.Zone_Code Inner Join Branch_Master c WITH (NOLOCK) On c.State_Code=a.State_Code inner join Users u WITH (NOLOCK) on c.Branch_Code=u.BranchCode Order By a.State_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    StateList.Add(new StateMaster(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                }


                return StateList;
            }
        }

        public List<StateMaster> GetZoneBasedStates(int ZoneCode)
        {
            List<StateMaster> lstStates = new List<StateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct state_code,state_name from state_master WITH (NOLOCK) where zone_code = " + ZoneCode + " order by state_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                    lstStates.Add(new StateMaster(reader[0].ToString(), reader[1].ToString(), ZoneCode.ToString(), null));
                return lstStates;
            }
        }

        public List<StateMaster> GetZoneBasedStatesOnline(int ZoneCode)
        {
            List<StateMaster> lstStates = new List<StateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select distinct s.state_code,state_name from state_master s WITH (NOLOCK) inner join branch_master b WITH (NOLOCK) on s.zone_code = " + ZoneCode + " and s.state_code=b.state_code ";
            sQuery = sQuery + "inner join users u WITH (NOLOCK) on b.branch_code=u.branchcode order by state_name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                    lstStates.Add(new StateMaster(reader[0].ToString(), reader[1].ToString(), ZoneCode.ToString(), null));
                return lstStates;
            }
        }

        public int GetCurrentState(string BranchCode)
        {
            int StateCode = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string StSQL = "Select b.State_Code from Branch_Master a WITH (NOLOCK) inner join State_Master b WITH (NOLOCK) on a.Branch_Code='" + BranchCode + "' and a.State_Code=b.State_Code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(StSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            StateCode = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            return StateCode;
        }

        public int GetCustomerState(string BranchCode, string CustomerCode)
        {
            int StateCode = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string StSQL = "Select b.Sales_During_Previous_Year from Branch_Master a WITH (NOLOCK) inner join Customer_Master b WITH (NOLOCK) on a.Branch_Code='" + BranchCode + "' and b.Customer_Code='" + CustomerCode + "' and a.Branch_Code=b.Branch_Code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(StSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            StateCode = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            return StateCode;
        }

    }
    public class StateMaster
    { 
        public StateMaster(string StateCode,string StateName,string ZoneCode, string ZoneName)
        {
            _StateCode = StateCode;
            _StateName = StateName;
            _ZoneCode = ZoneCode;
            _ZoneName = ZoneName;
        }
        public StateMaster()
        {
        }
        private string _StateCode;
        private string _StateName;
        private string _ZoneCode;
        private string _ZoneName;

        public string StateCode {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string StateName {
            get { return _StateName; }
            set { _StateName = value; }
        }
        public string ZoneCode
        {
            get { return _ZoneCode; }
            set { _ZoneCode = value; }
        }
        public string ZoneName {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }
    }
}
