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
    public class Locations
    {
        public void AddNewLocations(string FALocationName, string BrCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addfalocation");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BrCode.Trim());
            ImpalDB.AddInParameter(cmd, "@FA_Location_Name", DbType.String, FALocationName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateLocation(string FALocationCode, string FALocationName, string BrName)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updfalocation");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@FA_Location_Code", DbType.String, FALocationCode.Trim());
            ImpalDB.AddInParameter(cmd, "@FA_Location_Name", DbType.String, FALocationName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<Location> GetAllLocations()
        {
            List<Location> LocationList = new List<Location>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " SELECT   A.FA_Location_Code, A.FA_Location_Name , B.Branch_Code, B.Branch_Name FROM  Fixed_Assets_Location A, Branch_Master B where  A.Branch_code = B.Branch_Code" +
                " Order By  A.FA_Location_Code, A.FA_Location_Name , B.Branch_Code, B.Branch_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    LocationList.Add(new Location(reader["FA_Location_Code"].ToString(), reader["FA_Location_Name"].ToString(), reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                }
            }

            return LocationList;
        }
       
    }

    public class Location
    {
        public Location(string FALocationCode, string FALocationName, string BrCode, string BrName)
        {
            _FALocationCode = FALocationCode;
            _FALocationName = FALocationName;
            _BrCode = BrCode;
            _BrName = BrName;
        }
        private string _FALocationCode;
        private string _FALocationName;
        private string _BrCode;
        private string _BrName;


        public string FALocationCode 
        {
            get { return _FALocationCode; }
            set { _FALocationCode = value; }
        }
        public string FALocationName
        {
            get { return _FALocationName; }
            set { _FALocationName = value; }
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
