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
    public class BranchClassifications
    {
        public List<BranchClassification> GetAllBRClassifications()
        {
            List<BranchClassification> BrClassification = new List<BranchClassification>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Classification_Code,Classification_Description,Outstanding_Limit,Outstanding_Days from Branch_Classification " +
            "Order by Classification_Code ";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    string outstandAmt = string.Format("{0:F}", reader["Outstanding_Limit"]);
                    BrClassification.Add(new BranchClassification(reader["Classification_Code"].ToString(), reader["Classification_Description"].ToString(), outstandAmt, reader["Outstanding_Days"].ToString()));

                }
            }
            return BrClassification;
        }
        public void AddNewBRClassifications(string ClassificationCode, string ClassificationDescription, string OutstandingLimit, string OutstandingDays)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchclassification");
            ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.String, ClassificationCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Classification_Description", DbType.String, ClassificationDescription.Trim());
            ImpalDB.AddInParameter(cmd, "@Outstanding_Limit", DbType.String, OutstandingLimit.Trim());
            ImpalDB.AddInParameter(cmd, "@Outstanding_Days", DbType.String, OutstandingDays.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateBRClassifications(string ClassificationCode, string ClassificationDescription, string OutstandingLimit, string OutstandingDays)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updbranchclassification");
            ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.String, ClassificationCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Outstanding_Limit", DbType.String, OutstandingLimit.Trim());
            ImpalDB.AddInParameter(cmd, "@Outstanding_Days", DbType.String, OutstandingDays.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
    public class BranchClassification
    {
        public BranchClassification(string ClassificationCode, string ClassificationDescription, string OutstandingLimit, string OutstandingDays)
        { 
            _ClassificationCode = ClassificationCode;
            _ClassificationDescription = ClassificationDescription;
            _OutstandingLimit = OutstandingLimit;
            _OutstandingDays = OutstandingDays;
        }

        private string _ClassificationCode;
        private string _ClassificationDescription;
        private string _OutstandingLimit ;
        private string _OutstandingDays;

        public string ClassificationCode {
            get { return _ClassificationCode; }
            set { _ClassificationCode = value; }
        }
        public string ClassificationDescription {
            get { return _ClassificationDescription; }
            set { _ClassificationDescription = value; }
        }
        public string OutstandingLimit {
            get { return _OutstandingLimit; }
            set { _OutstandingLimit = value; }
        }
        public string OutstandingDays {
            get { return _OutstandingDays; }
            set { _OutstandingDays = value; }
        }

    }
}
