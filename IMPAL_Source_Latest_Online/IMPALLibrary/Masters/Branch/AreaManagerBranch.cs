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
    public class AreaManagerBranches
    {
        public List<AreaManagerBranch> GetAllAreaManagerBranches()
        {
            List<AreaManagerBranch> lstAreaManagerBranch = new List<AreaManagerBranch>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Usp_Viewareamanbranch ";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstAreaManagerBranch.Add(new AreaManagerBranch(reader["Area_Manager_Code"].ToString(), reader["Area_Manager_Name"].ToString(), reader["Branch_Manager"].ToString(), reader["branch_Name"].ToString(), reader["Start_Date"].ToString(), reader["End_Date"].ToString()));
                }
            }
            return lstAreaManagerBranch;
        }
        public void AddNewAreaManagerBranches(string AreaManagerCode, string AreaManagerName, string BranchManager, string OperatingBranch, string StartDate, string EndDate)
        
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addareamanbranch");
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Code", DbType.String, AreaManagerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, OperatingBranch.Trim());
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate.Trim());
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Manager", DbType.String, BranchManager.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateAreaManagerBranches(string AreaManagerCode, string AreaManagerName, string BranchManager, string OperatingBranch, string StartDate, string EndDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updareamanbranch");
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Code", DbType.String, AreaManagerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, OperatingBranch.Trim());
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate.Trim());
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Manager", DbType.String, BranchManager.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

    }
    public class AreaManagerBranch
    {
        public AreaManagerBranch(string AreaManagerCode, string AreaManagerName, string BranchManager, string OperatingBranch, string StartDate, string EndDate)
        {
            _AreaManagerCode = AreaManagerCode;
            _AreaManagerName = AreaManagerName;
            _BranchManager = BranchManager;
            _OperatingBranch = OperatingBranch;
            _StartDate = StartDate;
            _EndDate = EndDate;

        }
        private string _AreaManagerCode;
        private string _AreaManagerName;
        private string _BranchManager;
        private string _OperatingBranch;
        private string _StartDate;
        private string _EndDate;
        

        public string AreaManagerCode
        {
            get { return _AreaManagerCode; }
            set { _AreaManagerCode = value; }
        }
        public string AreaManagerName
        {
            get { return _AreaManagerName; }
            set { _AreaManagerName = value; }
        }
        public string BranchManager
        {
            get { return _BranchManager; }
            set { _BranchManager = value; }
        }
     
        public string OperatingBranch
        {
            get { return _OperatingBranch; }
            set { _OperatingBranch = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
    }
  
}









