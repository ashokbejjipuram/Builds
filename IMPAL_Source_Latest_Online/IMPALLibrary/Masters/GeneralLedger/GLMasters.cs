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
    public class GLMasters
    {
        public void AddNewGLMaster(string GLGroupCode, string Description, string BalanceIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addGLmaster");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, null);
            ImpalDB.AddInParameter(cmd, "@GL_Group_Code", DbType.String, GLGroupCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Main_Description", DbType.String, Description.Trim());
            ImpalDB.AddInParameter(cmd, "@Usual_Balance_Indicator", DbType.String, BalanceIndicator.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateGLMaster(string GLMasterCode, string GLGroupCode, string Description, string BalanceIndicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updGL");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, GLMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Main_Description", DbType.String, Description.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<GLMaster> GetAllGLMasters(string Filter)
        {
            List<GLMaster> GLMasters = new List<GLMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select GL_Main_Code,GL_Master.GL_Group_Code,GL_Main_Description,GL_Group_Description,Usual_Balance_Indicator " +
                           "from GL_Master,GL_Group where GL_Master.GL_Group_Code= GL_Group.GL_Group_Code  ";
            if (Filter != "ALL")
                sSQL = sSQL + "and GL_Master.GL_Group_Code=" + Filter + "";

            sSQL = sSQL + " order by GL_Main_Code,GL_Main_Description,GL_Group_Description";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GLMasters.Add(new GLMaster(reader["GL_Main_Code"].ToString(), reader["GL_Group_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Group_Description"].ToString(), reader["Usual_Balance_Indicator"].ToString()));
                }
            }
            return GLMasters;
        }

        public List<GLMaster> GetAllGLMastersEmpty(string Filter)
        {
            List<GLMaster> GLMasters = new List<GLMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select GL_Main_Code,GL_Master.GL_Group_Code,GL_Main_Description,GL_Group_Description,Usual_Balance_Indicator " +
                           "from GL_Master,GL_Group where GL_Master.GL_Group_Code= GL_Group.GL_Group_Code  ";
            if (Filter != "ALL")
                sSQL = sSQL + "and GL_Master.GL_Group_Code=" + Filter + "";

            sSQL = sSQL + " order by GL_Main_Code,GL_Main_Description,GL_Group_Description";
            GLMasters.Add(new GLMaster("", "-1", "", "", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GLMasters.Add(new GLMaster(reader["GL_Main_Code"].ToString(), reader["GL_Group_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Group_Description"].ToString(), reader["Usual_Balance_Indicator"].ToString()));
                }
            }
            return GLMasters;
        }



        public List<GLMaster> GetAllGLMasters()
        {
            List<GLMaster> GLMasters = new List<GLMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "Select GL_Main_Code,GL_Master.GL_Group_Code,GL_Main_Description,GL_Group_Description,Usual_Balance_Indicator " +
                           "from GL_Master,GL_Group where GL_Master.GL_Group_Code= GL_Group.GL_Group_Code  ";
            sSQL = sSQL + " order by GL_Main_Code,GL_Main_Description,GL_Group_Description";

            GLMasters.Add(new GLMaster("","-1","","",""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {

                    GLMasters.Add(new GLMaster(reader["GL_Main_Code"].ToString(), reader["GL_Group_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Group_Description"].ToString(), reader["Usual_Balance_Indicator"].ToString()));


                }

            }

            return GLMasters;
        }

    }

    public class GLMaster
    {
        public GLMaster(string GLMasterCode, string GLGroupCode, string Description, string GroupDescription, string BalanceIndicator)
        {
            _GLMasterCode = GLMasterCode;
            _GLGroupCode = GLGroupCode;
            _Description = Description;
            _GroupDescription = GroupDescription;
            _BalanceIndicator = BalanceIndicator;

        }
        public GLMaster()
        {

        }
        private string _GLMasterCode;
        private string _GLGroupCode;
        private string _Description;
        private string _GroupDescription;
        private string _BalanceIndicator;


        public string GLGroupCode
        {
            get { return _GLGroupCode; }
            set { _GLGroupCode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string GroupDescription
        {
            get { return _GroupDescription; }
            set { _GroupDescription = value; }
        }
        public string GLMasterCode
        {
            get { return _GLMasterCode; }
            set { _GLMasterCode = value; }
        }


        public string BalanceIndicator
        {
            get { return _BalanceIndicator; }
            set { _BalanceIndicator = value; }
        }



    }
}
