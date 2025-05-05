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
  public class DepotList
  {
   public List<Depot> GetAllDepots()
    {
        List<Depot> lstDepot = new List<Depot>();
        Database ImpalDB = DataAccess.GetDatabase();

        string sSQL = "Select Depot_Code,Depot_Short_Name,Depot_Long_Name from Depot_Master order by Depot_Code";
        DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
        using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstDepot.Add(new Depot(reader["Depot_Code"].ToString(), reader["Depot_Short_Name"].ToString(), reader["Depot_Long_Name"].ToString()));
                }
            }
            return lstDepot;
        }
   public void AddNewDepot(string DepotCode, string DepotShortName, string DepotLongName)
   {
       Database ImpalDB = DataAccess.GetDatabase();
       DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addDepot");
       ImpalDB.AddInParameter(cmd, "@Depot_Code ", DbType.String, DepotCode.Trim());
       ImpalDB.AddInParameter(cmd, "@Depot_Short_Name", DbType.String, DepotShortName.Trim());
       ImpalDB.AddInParameter(cmd, "@Depot_Long_Name", DbType.String, DepotLongName.Trim());
       cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
       ImpalDB.ExecuteNonQuery(cmd);
   }
   public void UpdateDepot(string DepotCode, string DepotShortName, string DepotLongName)
   {
       Database ImpalDB = DataAccess.GetDatabase();
       DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updDepot");
       ImpalDB.AddInParameter(cmd, "@Depot_Code ", DbType.String, DepotCode.Trim());
       ImpalDB.AddInParameter(cmd, "@Depot_Short_Name", DbType.String, DepotShortName.Trim());
       ImpalDB.AddInParameter(cmd, "@Depot_Long_Name", DbType.String, DepotLongName.Trim());
       cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
       ImpalDB.ExecuteNonQuery(cmd);
   }
  }
  
    public class Depot {
        public Depot(string DepotCode, string DepotShortName, string DepotLongName)
        {
            _DepotCode = DepotCode;
            _DepotShortName = DepotShortName;
            _DepotLongName = DepotLongName;
        }
        private string _DepotCode;
        private string _DepotShortName;
        private string _DepotLongName;
        
         public string DepotCode {
             get {return _DepotCode;}
             set {_DepotCode = value;}
         }
         public string DepotShortName
         {
            get {return _DepotShortName; }
            set {_DepotShortName = value;}
        }
        public string DepotLongName {
            get {return _DepotLongName; }
            set {_DepotLongName = value;}
        }

    }
}


