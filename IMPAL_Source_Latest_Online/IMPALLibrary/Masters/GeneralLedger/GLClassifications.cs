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
    public class GLClassifications
    {


        public List<GlClassification> GetGlClassification_Master()
        {
            List<GlClassification> GLClassification = new List<GlClassification>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " Select Gl_Classification_Code, GL_Classification_Description" +
            " from dbo.GL_Classification order by Gl_Classification_Code, GL_Classification_Description";
            GLClassification.Add(new GlClassification(" ", "-1"));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {

                while (reader.Read())
                {
                    GLClassification.Add(new GlClassification(reader["GL_Classification_Description"].ToString(), reader["Gl_Classification_Code"].ToString()));

                }

            }

            return GLClassification;
        }
       
        public List<GLClassification> GetAllGLClassifications()
        {
            List<GLClassification> GLClassification = new List<GLClassification>();

            Database ImpalDB = DataAccess.GetDatabase();
            
            string sSQL = "Select Gl_Classification_Code, GL_Classification_Description" +
            " from dbo.GL_Classification order by Gl_Classification_Code, GL_Classification_Description";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    GLClassification.Add(new GLClassification(reader[0].ToString(), reader[1].ToString()));


                }

            }

            return GLClassification;
        }

        public void AddNewGLClassifications(string GLCLDescription)
        {
             Database ImpalDB = DataAccess.GetDatabase();

             // Create command to execute the stored procedure and add the parameters.
             DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Addglclassification");
             ImpalDB.AddInParameter(cmd, "@GL_Classification_Description", DbType.String, GLCLDescription.Trim());
             cmd.CommandTimeout = ConnectionTimeOut.TimeOut; 
             ImpalDB.ExecuteNonQuery(cmd);

        }

        public void UpdateGLClassification(string GLCLCode, string GLCLDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdGLClassification");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Gl_Classification_Code", DbType.String, GLCLCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Classification_Description", DbType.String, GLCLDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class GLClassification
    {
        public GLClassification(string GLCLCode, string GLCLDescription)
        {
            _GlClcode = GLCLCode;
            _GLCLDescription = GLCLDescription;
          
        }

        private string _GlClcode;
        private string _GLCLDescription;
     


        public string GLCLCode
        {
            get { return _GlClcode; }
            set { _GlClcode = value; }
        }
        public string GLCLDescription
        {
            get { return _GLCLDescription; }
            set { _GLCLDescription = value; }
        }

    }
}