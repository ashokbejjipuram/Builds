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
    public class AreaManagers
    {
        public List<AreaManager> GetAllAreaManagers()
        {
            List<AreaManager> lstAreaManager = new List<AreaManager>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Area_Manager_Code, Area_Manager_Name ,Operating_Branch," +
                   "Start_Date,End_Date,Previous_Manager,Previous_Manager_Start_Date, Previous_Manager_End_Date " +
                   "From Area_Manager Order by Area_Manager_Code ";

            lstAreaManager.Add(new AreaManager("-1", ""));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstAreaManager.Add(new AreaManager(reader["Area_Manager_Code"].ToString(), reader["Area_Manager_Name"].ToString(), reader["Operating_Branch"].ToString(), reader["Start_Date"].ToString(), reader["End_Date"].ToString(), reader["Previous_Manager"].ToString(), reader["Previous_Manager_Start_Date"].ToString(), reader["Previous_Manager_End_Date"].ToString()));
                }
            }
            return lstAreaManager;
        }

        public List<AreaManager> GetAreaManagers()
        {
            List<AreaManager> lstAreaManager = new List<AreaManager>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Area_Manager_code, Area_Manager_Name from Area_Manager order by Area_Manager_Name";
            lstAreaManager.Add(new AreaManager("0", string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstAreaManager.Add(new AreaManager(reader["Area_Manager_Code"].ToString(), reader["Area_Manager_Name"].ToString()));
                }
            }
            return lstAreaManager;
        }
        public void AddNewAreaManagers(string AreaManagerCode, string AreaManagerName, string OperatingBranch, string StartDate, string EndDate, string PreviousManager, string PreviousManagerStartDate, string PreviousManagerEndDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addareaman");
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Code", DbType.String, AreaManagerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Name", DbType.String, AreaManagerName.Trim());
            ImpalDB.AddInParameter(cmd, "@Operating_Branch", DbType.String, OperatingBranch.Trim());
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate.Trim());
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate.Trim());
            ImpalDB.AddInParameter(cmd, "@Previous_Manager", DbType.String, PreviousManager.Trim());
            ImpalDB.AddInParameter(cmd, "@Previous_Manager_Start_Date", DbType.String, PreviousManagerStartDate.Trim());
            ImpalDB.AddInParameter(cmd, "@Previous_Manager_End_Date", DbType.String, PreviousManagerEndDate.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateAreaManagers(string AreaManagerCode, string AreaManagerName, string OperatingBranch, string StartDate, string EndDate, string PreviousManager, string PreviousManagerStartDate, string PreviousManagerEndDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updareaman");
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Code", DbType.String, AreaManagerCode);
            ImpalDB.AddInParameter(cmd, "@Area_Manager_Name", DbType.String, AreaManagerName);
            ImpalDB.AddInParameter(cmd, "@Operating_Branch", DbType.String, OperatingBranch);
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate);
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate);
            ImpalDB.AddInParameter(cmd, "@Previous_Manager", DbType.String, PreviousManager);
            ImpalDB.AddInParameter(cmd, "@Previous_Manager_Start_Date", DbType.String, PreviousManagerStartDate);
            ImpalDB.AddInParameter(cmd, "@Previous_Manager_End_Date", DbType.String, PreviousManagerEndDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

    }
    public class AreaManager
    {
        public AreaManager(string AreaManagerCode, string AreaManagerName, string OperatingBranch, string StartDate, string EndDate, string PreviousManager, string PreviousManagerStartDate, string PreviousManagerEndDate)
        {
            _AreaManagerCode = AreaManagerCode;
            _AreaManagerName = AreaManagerName;
            _OperatingBranch = OperatingBranch;
            _StartDate = StartDate;
            _EndDate = EndDate;
            _PreviousManager = PreviousManager;
            _PreviousManagerStartDate = PreviousManagerStartDate;
            _PreviousManagerEndDate = PreviousManagerEndDate;

        }
        public AreaManager(string AreaManagerCode, string AreaManagerName)
        {
            _AreaManagerCode = AreaManagerCode;
            _AreaManagerName = AreaManagerName;
        }
        private string _AreaManagerCode;
        private string _AreaManagerName;
        private string _OperatingBranch;
        private string _StartDate;
        private string _EndDate;
        private string _PreviousManager;
        private string _PreviousManagerStartDate;
        private string _PreviousManagerEndDate;

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
        public string PreviousManager
        {
            get { return _PreviousManager; }
            set { _PreviousManager = value; }
        }
        public string PreviousManagerStartDate
        {
            get { return _PreviousManagerStartDate; }
            set { _PreviousManagerStartDate = value; }
        }
        public string PreviousManagerEndDate
        {
            get { return _PreviousManagerEndDate; }
            set { _PreviousManagerEndDate = value; }
        }
    }
}









