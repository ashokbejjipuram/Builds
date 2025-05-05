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
    public class ClassificationDocuments
    {

        public List<ClassificationDocument> GetAllClassificationDocuments()
        {
            List<ClassificationDocument> DocumentLst = new List<ClassificationDocument>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select a.Document_Code ,b.Description , a.Classification_Code ,c.Description as Classification,a.Status  " +
                     " from Classification_documents a Inner join Insurance_Documents b " +
                     " on a.Document_Code = b.Code Inner join Insurance_Classification c on " +
                     " a.Classification_Code = c.Code ";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    DocumentLst.Add(new ClassificationDocument(reader["Classification_Code"].ToString(),reader["Classification"].ToString(),
                    reader["Document_Code"].ToString(), reader["Description"].ToString(), reader["Status"].ToString()));
                }
            }
            return DocumentLst;
        }

        public void AddNewClassificationDocuments(string classificationCode, string classification, string documentCode, string documents, string status)
        {
             Database ImpalDB = DataAccess.GetDatabase();

             // Create command to execute the stored procedure and add the parameters.
             DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addClassificationDocuments");
             ImpalDB.AddInParameter(cmd, "@Document_Code", DbType.Int32 , documentCode.Trim());
             ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.Int32, classificationCode.Trim());
             ImpalDB.AddInParameter(cmd, "@status", DbType.String, status.Trim());
             cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
             ImpalDB.ExecuteNonQuery(cmd);
  
        }

        public void UpdateClassificationDocuments(string classificationCode, string classification, string documentCode, string documents, string status)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updClassificationDocuments");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Document_Code", DbType.String, documentCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.String, classificationCode.Trim());
            ImpalDB.AddInParameter(cmd, "@status", DbType.String, status.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
    //SELECT Code,Description FROM Insurance_Documents
    public class ClassificationDocument
    {
        public ClassificationDocument(string classificationCode, string classification, string documentCode, string documents, string status)
        {
            _classificationCode = classificationCode;
            _classification = classification;
            _documentCode = documentCode;
            _documents = documents;
            _status = status;
        }

        private string _classificationCode;
        private string _classification;
        private string _documentCode;
        private string _documents;
        private string _status;

        public string classificationCode
        {
            get { return _classificationCode; }
            set { _classificationCode = value; }
        }
        public string classification
        {
            get { return _classification; }
            set { _classification = value; }
        }
        public string documentCode
        {
            get { return _documentCode; }
            set { _documentCode = value; }
        }
        public string documents
        {
            get { return _documents; }
            set { _documents = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }



    }
}