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
    public class ApplicationSegments
    {
        public List<ApplicationSegment> GetAllApplicationSegments()
        {
            List<ApplicationSegment> ApplnSegLst = new List<ApplicationSegment>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Application_Segment_Code,Appln_Segment_Description  from Application_Segment_Master Order by Application_Segment_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ApplnSegLst.Add(new ApplicationSegment(reader["Application_Segment_Code"].ToString(), reader["Appln_Segment_Description"].ToString()));
                }
            }

            return ApplnSegLst;
        }

        public List<ApplicationSegment> GetAllNewApplicationSegments(string strBranchCode)
        {
            List<ApplicationSegment> ApplnSegLst = new List<ApplicationSegment>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select distinct Application_Segment_Code,Application_Segment_Description from New_Segment_Master Order by Application_Segment_Description";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ApplnSegLst.Add(new ApplicationSegment(reader["Application_Segment_Code"].ToString(), reader["Application_Segment_Description"].ToString()));
                }
            }

            return ApplnSegLst;
        }

        public bool FindExists(string ApplicationCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int BranchCode = 0;
            string ssql = "select 1 from application_segment_master where Application_Segment_Code='"+ApplicationCode+"'";

            using (DbCommand sqlCmd = ImpalDB.GetSqlStringCommand(ssql))
            {

                BranchCode = (int)ImpalDB.ExecuteScalar(sqlCmd);
            }
            if (BranchCode == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void AddNewApplicationSegment(string ApplicationSegmentCode, string ApplnSegmentDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addapplnsegmentmaster");
            ImpalDB.AddInParameter(cmd, "@Application_Segment_Code", DbType.String, ApplicationSegmentCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Appln_Segment_Description", DbType.String, ApplnSegmentDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateItemType(string ApplicationSegmentCode, string ApplnSegmentDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updapplnsegmentmaster");
            ImpalDB.AddInParameter(cmd, "@Application_Segment_Code", DbType.String, ApplicationSegmentCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Appln_Segment_Description", DbType.String, ApplnSegmentDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class ApplicationSegment
    {

        public ApplicationSegment(string ApplicationSegmentCode, string ApplnSegmentDescription)
        {
            _ApplicationSegmentCode = ApplicationSegmentCode;
            _ApplnSegmentDescription = ApplnSegmentDescription;
        }
        private string _ApplicationSegmentCode;
        private string _ApplnSegmentDescription;

        public string ApplicationSegmentCode
        {
            get { return _ApplicationSegmentCode; }
            set { _ApplicationSegmentCode = value; }
        }
        public string ApplnSegmentDescription
        {
            get { return _ApplnSegmentDescription; }
            set { _ApplnSegmentDescription = value; }
        }

    }
}
