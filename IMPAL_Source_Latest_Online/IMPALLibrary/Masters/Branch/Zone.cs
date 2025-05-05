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
    public class Zones
    {
        public List<Zone> GetAllZones()
        {
            List<Zone> lstZone = null;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Zone_Code, Zone_Name from Zone_Master Order by Zone_code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstZone == null)
                    lstZone = new List<Zone>();
                //lstZone.Add(new Zone("0",""));   
                while (reader.Read())
                {
                    lstZone.Add(new Zone(reader[0].ToString(), reader[1].ToString()));
                }
                return lstZone;
            }
        }
        public void AddNewZone(string ZoneName)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addzone");
            ImpalDB.AddInParameter(cmd, "@Zone_name", DbType.String, ZoneName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateZone(string ZoneCode, string ZoneName)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updzone");
            ImpalDB.AddInParameter(cmd, "@Zone_code", DbType.String, ZoneCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Zone_Name", DbType.String, ZoneName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class Zone
    {
        public Zone(string ZoneCode, string ZoneName)
        {
            _ZoneCode = ZoneCode;
            _ZoneName = ZoneName;
        }
        private string _ZoneCode;
        private string _ZoneName;

        public string ZoneCode {
            get { return _ZoneCode; }
            set { _ZoneCode = value; }
        }
        public string ZoneName {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }
    }

}
