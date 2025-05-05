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
    public class GLFixedAssetGroups
    {

        public List<GLFixedAssetGroup> GetAllGLFixedGroups()
        {
            List<GLFixedAssetGroup> GLFixedGroups = new List<GLFixedAssetGroup>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select FA_Group_Code , FA_Group_Description from Fixed_Assets_Group Order by FA_Group_Code ASC";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    GLFixedGroups.Add(new GLFixedAssetGroup(reader["FA_Group_Code"].ToString(), reader["FA_Group_Description"].ToString()));
                }
            }

            return GLFixedGroups;
        }

        public List<GLFixedAssetSubGroup> GetAllGLFixedSubGroups(string FaGroupCode)
        {
            List<GLFixedAssetSubGroup> GLFixedSubGroups = new List<GLFixedAssetSubGroup>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = string.Empty;
            if (!string.IsNullOrEmpty(FaGroupCode))
                sSQL = "Select FA_Sub_Group_Code,FA_Sub_group_Description from Fixed_Assets_Sub_Group where FA_Group_Code= " + FaGroupCode + "";
            else
                sSQL = "select FA_Sub_Group_Code,FA_Sub_Group_Description from Fixed_Assets_Sub_Group";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    GLFixedSubGroups.Add(new GLFixedAssetSubGroup(reader["FA_Sub_Group_Code"].ToString(), reader["FA_Sub_Group_Description"].ToString()));
                }
            }

            return GLFixedSubGroups;
        }

        public void AddNewGLFixedGroups(string GLCLDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addFAGroup");
            ImpalDB.AddInParameter(cmd, "@FA_Group_Description", DbType.String, GLCLDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);



        }

        public void UpdateGLFixedGroup(string GLCLCode, string GLCLDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updFAGroup");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@FA_Group_Code", DbType.String, GLCLCode.Trim());
            ImpalDB.AddInParameter(cmd, "@FA_Group_Description", DbType.String, GLCLDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<SubGroupDetails> ViewFixedAssetSubGroup(string GroupCode, string SunGroupCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetFixedAssetSubGroup");
            ImpalDB.AddInParameter(cmd, "@FA_Group_Code", DbType.String, GroupCode.Trim());
            ImpalDB.AddInParameter(cmd, "@FA_Sub_Group_Code", DbType.String, SunGroupCode.Trim());
            List<SubGroupDetails> SubGroupDetailsList = new List<SubGroupDetails>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    SubGroupDetails SubGroupDetails = new SubGroupDetails();
                    SubGroupDetails.FA_Group_Code = objReader["FA_Group_Code"].ToString();
                    SubGroupDetails.FA_Sub_Group_Code = objReader["FA_Sub_Group_Code"].ToString();
                    SubGroupDetails.FA_Sub_Group_Description = objReader["FA_Sub_Group_Description"].ToString();
                    SubGroupDetails.Depreciation_Percentage = objReader["Depreciation_Percentage"].ToString();
                    SubGroupDetails.Gl_Main_Code = objReader["Gl_Main_Code"].ToString();
                    SubGroupDetails.Gl_Sub_Code = objReader["Gl_Sub_Code"].ToString();
                    SubGroupDetails.Gl_Account_Code = objReader["Gl_Account_Code"].ToString();
                    SubGroupDetails.IT_Depreciation_Percentage = objReader["IT_Depreciation_Percentage"].ToString();
                    SubGroupDetails.GL_Main_Description = objReader["GL_Main_Description"].ToString();
                    SubGroupDetails.Gl_Sub_Description = objReader["Gl_Sub_Description"].ToString();
                    SubGroupDetails.Description = objReader["Description"].ToString();
                    SubGroupDetailsList.Add(SubGroupDetails);

                }
            }

            return SubGroupDetailsList;
        }

        public void AddNewFixedAssetSubGroup(SubGroupDetails SubGroupDts)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddFASubGroup");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@FA_Group_Code", DbType.String, SubGroupDts.FA_Group_Code);
            ImpalDB.AddInParameter(cmd, "@FA_Sub_Group_Description", DbType.String, SubGroupDts.FA_Sub_Group_Description);
            ImpalDB.AddInParameter(cmd, "@Depreciation_Percentage", DbType.String, SubGroupDts.Depreciation_Percentage);
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, SubGroupDts.Gl_Main_Code);
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, SubGroupDts.Gl_Sub_Code);
            ImpalDB.AddInParameter(cmd, "@GL_Account_Code", DbType.String, SubGroupDts.Gl_Account_Code);
            ImpalDB.AddInParameter(cmd, "@IT_Depreciation_Percentage", DbType.String, SubGroupDts.IT_Depreciation_Percentage);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }

        public void UpdateFixedAssetSubGroup(SubGroupDetails SubGroupDts)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdFASubGroup");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@FA_Group_Code", DbType.String, SubGroupDts.FA_Group_Code);
            ImpalDB.AddInParameter(cmd, "@FA_Sub_Group_Code", DbType.String, SubGroupDts.FA_Sub_Group_Code);
            ImpalDB.AddInParameter(cmd, "@FA_Sub_Group_Description", DbType.String, SubGroupDts.FA_Sub_Group_Description);
            ImpalDB.AddInParameter(cmd, "@Depreciation_Percentage", DbType.String, SubGroupDts.Depreciation_Percentage);
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, SubGroupDts.Gl_Main_Code);
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, SubGroupDts.Gl_Sub_Code);
            ImpalDB.AddInParameter(cmd, "@GL_Account_Code", DbType.String, SubGroupDts.Gl_Account_Code);
            ImpalDB.AddInParameter(cmd, "@IT_Depreciation_Percentage", DbType.String, SubGroupDts.IT_Depreciation_Percentage);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);


        }
    }

    public class GLFixedAssetGroup
    {
        public GLFixedAssetGroup(string GLCLCode, string GLCLDescription)
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

    public class GLFixedAssetSubGroup
    {
        public GLFixedAssetSubGroup(string GLCLSubCode, string GLCLSubDescription)
        {
            _GLCLSubCode = GLCLSubCode;
            _GLCLSubDescription = GLCLSubDescription;

        }

        private string _GLCLSubCode;
        private string _GLCLSubDescription;



        public string GLCLSubCode
        {
            get { return _GLCLSubCode; }
            set { _GLCLSubCode = value; }
        }
        public string GLCLSubDescription
        {
            get { return _GLCLSubDescription; }
            set { _GLCLSubDescription = value; }
        }

    }

    public class SubGroupDetails
    {
        public SubGroupDetails()
        {

        }

        private string _FA_Group_Code;
        private string _FA_Sub_Group_Code;
        private string _FA_Sub_Group_Description;
        private string _Depreciation_Percentage;
        private string _Gl_Main_Code;
        private string _Gl_Sub_Code;
        private string _Gl_Account_Code;
        private string _IT_Depreciation_Percentage;
        private string _GL_Main_Description;
        private string _Gl_Sub_Description;
        private string _Description;

        public string FA_Group_Code
        {
            get { return _FA_Group_Code; }
            set { _FA_Group_Code = value; }
        }

        public string FA_Sub_Group_Code
        {
            get { return _FA_Sub_Group_Code; }
            set { _FA_Sub_Group_Code = value; }
        }

        public string FA_Sub_Group_Description
        {
            get { return _FA_Sub_Group_Description; }
            set { _FA_Sub_Group_Description = value; }
        }

        public string Depreciation_Percentage
        {
            get { return _Depreciation_Percentage; }
            set { _Depreciation_Percentage = value; }
        }

        public string Gl_Main_Code
        {
            get { return _Gl_Main_Code; }
            set { _Gl_Main_Code = value; }
        }

        public string Gl_Sub_Code
        {
            get { return _Gl_Sub_Code; }
            set { _Gl_Sub_Code = value; }
        }

        public string Gl_Account_Code
        {
            get { return _Gl_Account_Code; }
            set { _Gl_Account_Code = value; }
        }

        public string IT_Depreciation_Percentage
        {
            get { return _IT_Depreciation_Percentage; }
            set { _IT_Depreciation_Percentage = value; }
        }

        public string GL_Main_Description
        {
            get { return _GL_Main_Description; }
            set { _GL_Main_Description = value; }
        }

        public string Gl_Sub_Description
        {
            get { return _Gl_Sub_Description; }
            set { _Gl_Sub_Description = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    }





}