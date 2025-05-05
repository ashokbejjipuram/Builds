using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;   
using System.Collections;

namespace IMPALLibrary
{
    public class Classifications
    {

        public List<Classification> GetAllClassifications()
        {
            List<Classification> ClassificationLst = new List<Classification>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Code,Description,Isnull(Minimum_Value,0)as Minimum_Value,Isnull(Maximum_Value,0) as Maximum_Value From Insurance_Classification";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    string min = string.Format("{0:F}", reader["Minimum_Value"]);
                    string max = string.Format("{0:F}", reader["Maximum_Value"]);
                 // ClassificationLst.Add(new Classification(reader["Code"].ToString(), reader["Description"].ToString(),reader["Minimum_Value"].ToString(),reader["Maximum_Value"].ToString()));
                    ClassificationLst.Add(new Classification(reader["Code"].ToString(), reader["Description"].ToString(), min, max));
                }
            }

            return ClassificationLst;
        }

        public void AddNewClassification(string Description, double MinimumValue, double MaximumValue)
        {
             Database ImpalDB = DataAccess.GetDatabase();

             // Create command to execute the stored procedure and add the parameters.
             DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddInsuranceClassification");
             ImpalDB.AddInParameter(cmd, "@Description", DbType.String, Description.Trim());
             double min = Convert.ToDouble(MinimumValue);
             double max = Convert.ToDouble(MaximumValue);
             ImpalDB.AddInParameter(cmd, "@Minimum_Value", DbType.Double, min);
             ImpalDB.AddInParameter(cmd, "@Maximum_Value", DbType.Double , max );
             cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
             ImpalDB.ExecuteNonQuery(cmd);

        }

        public void UpdateClassification(string Code, string Description, double MinimumValue, double MaximumValue)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdInsuranceClassification");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Code", DbType.String, Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Description", DbType.String, Description.Trim());
            ImpalDB.AddInParameter(cmd, "@Minimum_Value", DbType.String, Convert.ToDouble(MinimumValue));
            ImpalDB.AddInParameter(cmd, "@Maximum_Value", DbType.String, Convert.ToDouble(MaximumValue));
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
    //SELECT Code,Description FROM Insurance_Documents
    public class Classification
    {
        public Classification(string Code, string Description, string MinimumValue, string MaximumValue)
        {
            _Code = Code;
            _Description = Description;
            _MinimumValue = MinimumValue;
            _MaximumValue = MaximumValue;
        }

        private string _Code;
        private string _Description;
        private string _MinimumValue;
        private string _MaximumValue;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string MinimumValue
        {
            get { return _MinimumValue; }
            set { _MinimumValue = value; }
        }
        public string MaximumValue
        {
            get { return _MaximumValue; }
            set { _MaximumValue = value; }
        }


    }
}