using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;

namespace IMPALLibrary
{
    public class Roles
    {
        public void AddNewRole(string RoleName, string RoleCode)
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             try
             {
                 Database ImpalDB = DataAccess.GetDatabase();
                 DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addNewRole");
                 ImpalDB.AddInParameter(cmd, "@RoleName", DbType.String, RoleName.Trim());
                 ImpalDB.AddInParameter(cmd, "@RoleCode", DbType.String, RoleCode.Trim());
                 cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                 ImpalDB.ExecuteNonQuery(cmd);
             }
             catch (Exception exp)
             {
                 Log.WriteException(Source, exp);
             }

        }
        public void UpdateRole(int RoleID, string RoleName, bool Active, string RoleCode)
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             try
             {
                Database ImpalDB = DataAccess.GetDatabase();


                // Create command to execute the stored procedure and add the parameters.
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdateRoles");

                //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
                ImpalDB.AddInParameter(cmd, "@RoleID", DbType.Int16,RoleID  );
                ImpalDB.AddInParameter(cmd, "@RoleName", DbType.String, RoleName.Trim());
                ImpalDB.AddInParameter(cmd, "@Active", DbType.Boolean, Active);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
             }
             catch (Exception exp)
             {
                 Log.WriteException(Source, exp);
             }
        }
        public List<RoleInfo> GetAllRoles()
        {
            List<RoleInfo> RoleInfos = new List<RoleInfo>();

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select RoleID, RoleName, RoleCode  from userrole  order by RoleName, RoleCode";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        RoleInfos.Add(new RoleInfo((int)reader["RoleID"], reader["RoleName"].ToString(), reader["RoleCode"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return RoleInfos;
        }



      

    }


    public class RoleInfo
    {

        private int _RoleID;
        private string _RoleName = "";
        private string _RoleCode = "";
        private bool _Active;

        public RoleInfo()
        { }

        public RoleInfo(int RoleID, string RoleName, string RoleCode, bool Active)
        {
            _RoleID = RoleID;
            _RoleName = RoleName;
            _RoleCode = RoleCode;
            _Active = Active;


        }
        public RoleInfo(int RoleID, string RoleName, string RoleCode )
        {
            _RoleID = RoleID;
            _RoleName = RoleName;
            _RoleCode = RoleCode;

        }





        public int RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }

        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                _RoleName = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return _RoleCode;
            }
            set
            {
                _RoleCode = value;
            }
        }

        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }

  



    }
    
}
