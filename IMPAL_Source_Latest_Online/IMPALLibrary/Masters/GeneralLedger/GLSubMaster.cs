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
    public class GLSubMasters
    {
        public void AddNewGLSubMaster(string GLMasterCode, string GLSubMasterCode, string GLSubMasterDesc)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addGLsub");
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, GLMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, GLSubMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Description", DbType.String, GLSubMasterDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateGLSubGroup(string GLMasterCode, string GLMasterDesc, string GLSubMasterCode, string GLSubMasterDesc)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdGLsub");
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, GLMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, GLSubMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Description", DbType.String, GLSubMasterDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<GLSubMaster> GetAllGlSubGroup()
        {
            List<GLSubMaster> GlSubMasterGroup = new List<GLSubMaster>();
            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "SELECT GL_Main_Code , GL_Sub_Code, GL_Sub_Description from GL_Sub_Master Order by GL_Main_Code ";
            string sSQL = "select a.GL_Main_Code,a.GL_Main_Description,b.GL_Sub_Code,b.GL_Sub_Description from GL_Master a Inner join GL_Sub_Master b ";
            sSQL = sSQL + " on a.GL_Main_Code = b.GL_Main_Code ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GlSubMasterGroup.Add(new GLSubMaster(reader["GL_Main_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Sub_Code"].ToString(), reader["GL_Sub_Description"].ToString()));
                }
            }
            return GlSubMasterGroup;
        }
        public List<GLSubMaster> GetAllGlSubGroup(string Filter)
        {
            List<GLSubMaster> GlSubMasterGroup = new List<GLSubMaster>();
            Database ImpalDB = DataAccess.GetDatabase();

            //string sSQL = "SELECT GL_Main_Code , GL_Sub_Code, GL_Sub_Description from GL_Sub_Master Order by GL_Main_Code ";
            string sSQL = "select a.GL_Main_Code,a.GL_Main_Description,b.GL_Sub_Code,b.GL_Sub_Description from GL_Master a Inner join GL_Sub_Master b ";
            sSQL = sSQL + " on a.GL_Main_Code = b.GL_Main_Code ";
            if (Filter != "ALL")
                sSQL = sSQL + "and a.GL_Main_Code =" + Filter + "";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GlSubMasterGroup.Add(new GLSubMaster(reader["GL_Main_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Sub_Code"].ToString(), reader["GL_Sub_Description"].ToString()));
                }
            }
            return GlSubMasterGroup;
        }
        // "Select GL_Main_Code,GL_Main_Description from GL_Master"
    }
    public class GLSubMaster
    {
        public GLSubMaster(string GLMasterCode, string GLMasterDesc, string GLSubMasterCode, string GLSubMasterDesc)
        {
            _GlMasterCode = GLMasterCode;
            _GlMasterDesc = GLMasterDesc;
            _GlSubMasterCode = GLSubMasterCode;
            _GlSubMasterDesc = GLSubMasterDesc;
            
        }
        public GLSubMaster()
        {

        }
        private string _GlMasterCode;
        private string _GlMasterDesc;
        private string _GlSubMasterCode;
        private string _GlSubMasterDesc;
        

        public string GLMasterCode
        {
            get { return _GlMasterCode; }
            set { _GlMasterCode = value; }
        }
        public string GLMasterDesc
        {
            get { return _GlMasterDesc; }
            set { _GlMasterDesc = value; }
        }
        public string GLSubMasterCode
        {
            get { return _GlSubMasterCode; }
            set { _GlSubMasterCode = value; }
        }
        public string GLSubMasterDesc {
            get { return _GlSubMasterDesc; }
            set { _GlSubMasterDesc = value; }
        }

    }
}
