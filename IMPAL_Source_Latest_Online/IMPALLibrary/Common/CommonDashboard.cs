using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Collections.Generic;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Net;
using System.Data.SqlClient;

namespace IMPALWeb.Dashboard
{
    public class CommonDashboard
    {
        public void Export(ServerReport serverReport, string type,string fileName)
        {
            try
            {
                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Warning[] warnings;
                byte[] bytes = serverReport.Render(type, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = mimeType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + fileNameExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch
            {                
                throw;
            }
           
        }
        public void RunReport(ServerReport serverReport, List<ReportParameter> lstReportParameter, string ReportServerUrl, string ReportPath)
        {
            try
            {
                serverReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["SERVERURL"].ToString());
                serverReport.ReportPath = ReportPath;
                //IReportServerCredentials irsc = new CustomReportCredentials();
                serverReport.ReportServerCredentials = new MyReportServerCredentials();
                DataSourceCredentials dataSourceCredentials = new DataSourceCredentials();
                dataSourceCredentials.Name = "impal";
                //ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["IMPALDatabase"];
                //string connectionString = connection.ConnectionString;
                //SqlConnectionStringBuilder str = new SqlConnectionStringBuilder(connectionString);
                //dataSourceCredentials.UserId = str.UserID;
                //dataSourceCredentials.Password = str.Password;
                //DataSourceCredentials[] dsCredentials = new DataSourceCredentials[] { dataSourceCredentials };
                //serverReport.SetDataSourceCredentials(dsCredentials);

                serverReport.SetParameters(lstReportParameter);
                serverReport.Refresh();
            }
            catch
            {                
                throw;
            }            
        }

        public List<Zone> GetAllZones()
        {

            List<Zone> lstZone = null;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Zone_Code, Zone_Name from Zone_Master Order by Zone_code";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    if (lstZone == null)
                        lstZone = new List<Zone>();
                    lstZone.Add(new Zone("-1", "All"));
                    while (reader.Read())
                    {
                        lstZone.Add(new Zone(reader[0].ToString(), reader[1].ToString()));
                    }
                    return lstZone;
                }
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public List<StateMaster> GetZoneBasedStates(int ZoneCode)
        {
            List<StateMaster> lstStates = new List<StateMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            if (ZoneCode == -1)
            {
                sQuery = "SELECT DISTINCT STATE_CODE,STATE_NAME FROM STATE_MASTER";
            }
            else
            {
                sQuery = "SELECT DISTINCT STATE_CODE,STATE_NAME FROM STATE_MASTER WHERE ZONE_CODE = " + ZoneCode;
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                lstStates.Add(new StateMaster("-1", "All", "-1", null));
                while (reader.Read())
                    lstStates.Add(new StateMaster(reader[0].ToString(), reader[1].ToString(), ZoneCode.ToString(), null));
                return lstStates;
            }
        }


        public List<Branch> GetStateBasedBranch(int ZoneID, int StateCode)
        {
            List<Branch> lstBranch = new List<Branch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            if (ZoneID == -1 && StateCode == -1)
            {
                sQuery = "SELECT DISTINCT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER B INNER JOIN State_Master S  ON B.State_Code = S.State_Code INNER JOIN Zone_Master Z ON Z.Zone_Code = S.Zone_Code ";
            }
            else if(ZoneID != -1 && StateCode == -1)
                sQuery = "SELECT DISTINCT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER B INNER JOIN State_Master S  ON B.State_Code = S.State_Code INNER JOIN Zone_Master Z ON Z.Zone_Code = S.Zone_Code AND Z.Zone_Code = " + ZoneID;
            else if(ZoneID != -1 && StateCode != -1)
                sQuery = "SELECT DISTINCT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER B INNER JOIN State_Master S  ON B.State_Code = S.State_Code AND S.State_Code = " + StateCode + " INNER JOIN Zone_Master Z ON Z.Zone_Code = S.Zone_Code AND Z.Zone_Code = " + ZoneID;
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                lstBranch.Add(new Branch("-1", "All"));
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
        public Branch(string ZoneCode, string ZoneName, string BranchCode, string BranchName)
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


    public class StateMaster
    {
        public StateMaster(string StateCode, string StateName, string ZoneCode, string ZoneName)
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

        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
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

    public class Zone
    {
        public Zone(string ZoneCode, string ZoneName)
        {
            _ZoneCode = ZoneCode;
            _ZoneName = ZoneName;
        }
        private string _ZoneCode;
        private string _ZoneName;

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
    [Serializable]
    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                string userName = System.Configuration.ConfigurationManager.AppSettings["SSRSUSERNAME"].ToString();

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password = System.Configuration.ConfigurationManager.AppSettings["SSRSPASSWORD"].ToString();

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain = System.Configuration.ConfigurationManager.AppSettings["SSRSDOMAIN"].ToString();

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing domain from web.config file");

                return new NetworkCredential(userName, password, domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            // Not using form credentials
            return false;
        }
    }
}
