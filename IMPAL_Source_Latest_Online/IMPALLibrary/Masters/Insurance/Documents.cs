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
    public class Documents
    {

        public List<Document> GetAllDocuments()
        {
            List<Document> DocumentLst = new List<Document>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Code,Description FROM Insurance_Documents";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    DocumentLst.Add(new Document(reader["Code"].ToString(), reader["Description"].ToString()));
                }
            }

            return DocumentLst;
        }

        public void AddNewDocuments(string Description)
        {
             Database ImpalDB = DataAccess.GetDatabase();

             // Create command to execute the stored procedure and add the parameters.
             DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addInsuranceDocuments");
             ImpalDB.AddInParameter(cmd, "@Description", DbType.String, Description.Trim());
             cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
             ImpalDB.ExecuteNonQuery(cmd);

        }

        public void UpdateBank(string Code, string Description)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updInsuranceDocuments");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Code", DbType.String, Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Description", DbType.String, Description.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
    //SELECT Code,Description FROM Insurance_Documents
    public class Document
    {
        public Document(string Code, string Description)
        {
            _Code = Code;
            _Description = Description;
        }

        private string _Code;
        private string _Description;

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

    }
}