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
    public class GLAccMasters
    {
        public void AddNewGLAccMaster(string GLMasterCode, string GLMasterDesc, string GLSubMasterCode, string GLSubMasterDesc, string GLAccMasterCode, string GLAccMasterDesc)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addglaccount");
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, GLMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, GLSubMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Gl_Account_Code", DbType.String, GLAccMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Description", DbType.String, GLAccMasterDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateGLAccGroup(string GLMasterCode, string GLMasterDesc, string GLSubMasterCode, string GLSubMasterDesc, string GLAccMasterCode, string GLAccMasterDesc)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updglaccount");
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, GLMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, GLSubMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Gl_Account_Code", DbType.String, GLAccMasterCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Description", DbType.String, GLAccMasterDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<GLAccMaster> GetAllGlAccGroup()
        {
            List<GLAccMaster> GlAccMasterGroup = new List<GLAccMaster>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = " select a.GL_Main_Code,a.GL_Main_Description,b.GL_Sub_Code,b.GL_Sub_Description , c.GL_Account_Code , c.Description " +
            " from GL_Master a Inner join GL_Sub_Master b on a.GL_Main_Code = b.GL_Main_Code " +
            " Inner Join GL_Account_Master c  on (a.GL_Main_Code =c.GL_Main_Code and b.GL_Sub_Code = c.GL_Sub_Code ) ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GlAccMasterGroup.Add(new GLAccMaster(reader["GL_Main_Code"].ToString(), reader["GL_Main_Description"].ToString(), reader["GL_Sub_Code"].ToString(),
                        reader["GL_Sub_Description"].ToString(), reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                }
            }
            return GlAccMasterGroup;
        }


        public List<GLAccMaster> GetAllGlAccSubGroup(string glmainCode)
        {
            List<GLAccMaster> GlAccSubGroup = new List<GLAccMaster>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "";
            if (glmainCode != null)
            {
                sSQL = " Select b.GL_Sub_Code,b.GL_Sub_Description " +
                      "from GL_Master a Inner join GL_Sub_Master b on a.GL_Main_Code = b.GL_Main_Code where b.GL_Main_Code = '" + glmainCode + "'";
            }
            else
            {
                sSQL = " Select b.GL_Sub_Code,b.GL_Sub_Description " +
                      "from GL_Master a Inner join GL_Sub_Master b on a.GL_Main_Code = b.GL_Main_Code ";
            }
            GlAccSubGroup.Add(new GLAccMaster("-1", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GlAccSubGroup.Add(new GLAccMaster(reader["GL_Sub_Code"].ToString(), reader["GL_Sub_Description"].ToString()));
                }
            }
            return GlAccSubGroup;
        }

        public List<GLAcc> GetAllGlAccSubGrp(string glclassification, string glgroup, string glmain, string glsub)
        {
            List<GLAcc> GlAccSubGroup = new List<GLAcc>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "";
            if (glsub != "")
            {
                //sSQL = " select GL_Account_Code , Description  from GL_Account_Master Where GL_Sub_Code = '" + glmainCode + "'";

                sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a,Gl_Account_Master b ";
                sSQL = sSQL + "where a.Gl_Account_Code=b.Gl_Account_Code and a.GL_Classification_Code= '" + glclassification + "'";
                sSQL = sSQL + "and a.GL_Group_Code='" + glgroup + "' and a.Gl_Main_Code='" + glmain + "' and ";
                sSQL = sSQL + "a.GL_Sub_Code='" + glsub + "'";

            }
            else
            {
                sSQL = " select GL_Account_Code , Description  from GL_Account_Master ";
                GlAccSubGroup.Add(new GLAcc("-1", ""));
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    GlAccSubGroup.Add(new GLAcc(reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                }
            }
            return GlAccSubGroup;
        }


        public bool AccSubMaster_FindExists(string gl_main_code, string gl_sub_code, string gl_account_code)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int BranchCode = 0;

            string ssql = "select 1 from Gl_Account_Master where gl_main_code ='" + gl_main_code + "' and gl_sub_code = '" + gl_sub_code
                    + "' and gl_account_code ='" + gl_account_code + "'";

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
    }

    public class GLAccMaster
    {
        public GLAccMaster(string GLMasterCode, string GLMasterDesc, string GLSubMasterCode, string GLSubMasterDesc, string GLAccMasterCode, string GLAccMasterDesc)
        {
            _GlMasterCode = GLMasterCode;
            _GlMasterDesc = GLMasterDesc;
            _GlSubMasterCode = GLSubMasterCode;
            _GlSubMasterDesc = GLSubMasterDesc;
            _GlAccMasterCode = GLAccMasterCode;
            _GlAccMasterDesc = GLAccMasterDesc;
            
        }
        public GLAccMaster(string GLSubMasterCode, string GLSubMasterDesc)
        {
            _GlSubMasterCode = GLSubMasterCode;
            _GlSubMasterDesc = GLSubMasterDesc;
                        
        }
        public GLAccMaster()
        {

        }
        private string _GlMasterCode;
        private string _GlMasterDesc;
        private string _GlSubMasterCode;
        private string _GlSubMasterDesc;
        private string _GlAccMasterCode;
        private string _GlAccMasterDesc;

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
        public string GLAccMasterCode
        {
            get { return _GlAccMasterCode; }
            set { _GlAccMasterCode = value; }
        }
        public string GLAccMasterDesc
        {
            get { return _GlAccMasterDesc; }
            set { _GlAccMasterDesc = value; }
        }

    }
    public class GLAcc
    {
        public GLAcc(string GLAccCode, string GLAccDesc)
        {
            _GlAccCode = GLAccCode;
            _GlAccDesc = GLAccDesc;
        }
        public GLAcc() { }
        private string _GlAccCode;
        private string _GlAccDesc;
        public string GLAccCode
        {
            get { return _GlAccCode; }
            set { _GlAccCode = value; }
        }
        public string GLAccDesc
        {
            get { return _GlAccDesc; }
            set { _GlAccDesc = value; }
        }
    }
}
