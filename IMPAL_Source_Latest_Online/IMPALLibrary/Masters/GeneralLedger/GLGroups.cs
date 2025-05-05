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
    public class GLGroups
    {
        public void AddNewGLGroup(string GLGroupCode, string GLGroupDesc, string GLCLCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addGLGroup");

            ImpalDB.AddInParameter(cmd, "@GL_Group_Code", DbType.String, GLGroupCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Gl_Classification_Code", DbType.String, GLCLCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Group_Description", DbType.String, GLGroupDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateGLGroup(string GLCode, string Description , string ClassificationDesc)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updGLGroup");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@GL_Group_Code", DbType.String, GLCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Group_Description", DbType.String, Description.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<GLGroup> GetAllGroup()
        {
            List<GLGroup> GLGroups = new List<GLGroup>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " SELECT [GL_Group_Code], [GL_Group_Description]  , [dbo].[GL_Group].[Gl_Classification_Code], " +
                        " [Gl_Classification_Description] " +
                        " FROM  [dbo].[GL_Group], [dbo].[GL_Classification] where  [dbo].[GL_Group].[Gl_Classification_Code] " +
                        " = [dbo].[GL_Classification].[Gl_Classification_Code] " +
                        " Order By GL_Group_Code,GL_Group_Description, Gl_Classification_Description ";

            GLGroups.Add(new GLGroup("-1", "", "", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GLGroups.Add(new GLGroup(reader["GL_Group_Code"].ToString(), reader["GL_Group_Description"].ToString(), reader["Gl_Classification_Code"].ToString(), reader["Gl_Classification_Description"].ToString()));
                   
                }

            }
            
            return GLGroups;
        }

        public List<GLGroup> GetAllGroup(string Filter)
        {
            List<GLGroup> GLGroups = new List<GLGroup>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = " SELECT   [GL_Group_Code], [GL_Group_Description]  , [dbo].[GL_Group].[Gl_Classification_Code], " +
                        " [Gl_Classification_Description] " +
                        " FROM  [dbo].[GL_Group], [dbo].[GL_Classification] where  [dbo].[GL_Group].[Gl_Classification_Code] " +
                        " = [dbo].[GL_Classification].[Gl_Classification_Code]";
            if (Filter != "ALL")
            {
                sSQL = sSQL + " and [dbo].[GL_Classification].[Gl_Classification_Code] =" + Filter + "";
            }

            sSQL = sSQL + " Order By GL_Group_Code,GL_Group_Description, Gl_Classification_Description ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GLGroups.Add(new GLGroup(reader["GL_Group_Code"].ToString(), reader["GL_Group_Description"].ToString(), reader["Gl_Classification_Code"].ToString(), reader["Gl_Classification_Description"].ToString()));

                }
            }

            return GLGroups;
        }
        public List<GLGroup> GetAllGroupEmpty(string Filter)
        {
            List<GLGroup> GLGroups = new List<GLGroup>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = " SELECT   [GL_Group_Code], [GL_Group_Description]  , [dbo].[GL_Group].[Gl_Classification_Code], " +
                        " [Gl_Classification_Description] " +
                        " FROM  [dbo].[GL_Group], [dbo].[GL_Classification] where  [dbo].[GL_Group].[Gl_Classification_Code] " +
                        " = [dbo].[GL_Classification].[Gl_Classification_Code]";

            if (Filter != "ALL")
            {
                sSQL = sSQL + " and [dbo].[GL_Classification].[Gl_Classification_Code] =" + Filter + "";
            }
            sSQL = sSQL + " Order By GL_Group_Code,GL_Group_Description, Gl_Classification_Description ";

            GLGroups.Add(new GLGroup("-1", "", "", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GLGroups.Add(new GLGroup(reader["GL_Group_Code"].ToString(), reader["GL_Group_Description"].ToString(), reader["Gl_Classification_Code"].ToString(), reader["Gl_Classification_Description"].ToString()));
                }
            }

            return GLGroups;
        }

        public List<GlClassification> GetGlClassification()
        {
            List<GlClassification> GLGroups = new List<GlClassification>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select distinct [Gl_Classification_Description],[Gl_Classification_Code] from  [dbo].[GL_Classification]";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {

                while (reader.Read())
                {
                    GLGroups.Add(new GlClassification(reader["Gl_Classification_Description"].ToString(), reader["Gl_Classification_Code"].ToString()));

                }

            }

            return GLGroups;
        }

       
    }

    public class GLGroup 
    {
        public GLGroup(string GLcode, string Description, string CLCLCode,string GLDesc )
        {
            _Glcode = GLcode;
            _Description = Description;
            _ClassificationCode = CLCLCode;
            _ClassificationDesc = GLDesc;
        }
        public GLGroup()
        {
           
        }
        private string _Glcode;
        private string _Description;
        private string _ClassificationCode;
        private string _ClassificationDesc;
        

        public string GLCode 
        {
            get { return _Glcode; } 
            set { _Glcode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string ClassificationCode
        {
            get { return _ClassificationCode; }
            set { _ClassificationCode = value; }
        }


        public string ClassificationDesc
        {
            get { return _ClassificationDesc; }
            set { _ClassificationDesc = value; }
        }



    }

    public class GlClassification
    {
        public GlClassification(string GlClassificationDesc, string GlClassificationCode)
        {
            _GlClassificationDesc = GlClassificationDesc;
            _GlClassificationCode = GlClassificationCode;

        }

        private string _GlClassificationDesc;
        private string _GlClassificationCode;

        public string GlClassificationDesc
        {
            get { return _GlClassificationDesc; }
            set { _GlClassificationDesc = value; }
        }
        public string GlClassificationCode
        {
            get { return _GlClassificationCode; }
            set { _GlClassificationCode = value; }
        }

    }
}
