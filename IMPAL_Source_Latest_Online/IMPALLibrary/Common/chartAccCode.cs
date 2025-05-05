using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for chartAccCode
/// </summary>
namespace IMPALLibrary
{
    public class chartAccCode
    {
        public chartAccCode()
        {
        }

        public List<Glmain> GetGlmain(string Filter)
        {
            List<Glmain> GlMainList = new List<Glmain>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = string.Empty;
                if (!string.IsNullOrEmpty(Filter))
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) where Gl_Main_Code in " + Filter + " order by gl_main_description";
                else
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) order by gl_main_description";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlMainList.Add(new Glmain(reader["gl_main_code"].ToString(), reader["gl_main_description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlMainList;
        }

        public List<Glmain> GetGlmainOthers(string Filter)
        {
            List<Glmain> GlMainList = new List<Glmain>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = string.Empty;
                if (!string.IsNullOrEmpty(Filter))
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) where Gl_Main_Code in " + Filter + " order by gl_main_description";
                else
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) order by gl_main_description";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlMainList.Add(new Glmain(reader["gl_main_code"].ToString(), reader["gl_main_description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlMainList;
        }

        public List<GlSub> GetGlSub(string GlMainValue)
        {
            List<GlSub> GlSubList = new List<GlSub>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208")
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' order by Gl_Sub_Code";
                }
                else
                {

                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' and GL_Sub_Code not in ('0061','0062','0063','0064','0209','0210','0211','0212') order by Gl_Sub_Description";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlSubList.Add(new GlSub(reader["Gl_Sub_Code"].ToString(), reader["Gl_Sub_Description"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlSubList;
        }

        public List<GlSub> GetGlSubOthers(string GlMainValue)
        {
            List<GlSub> GlSubList = new List<GlSub>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208")
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' and Gl_Sub_Code='0140' order by Gl_Sub_Code";
                }
                else
                {

                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' and GL_Sub_Code not in ('0061','0062','0063','0064','0209','0210','0211','0212') order by Gl_Sub_Description";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlSubList.Add(new GlSub(reader["Gl_Sub_Code"].ToString(), reader["Gl_Sub_Description"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlSubList;
        }

        public List<Glmain> GetGlmainGSTOptions(string Filter)
        {
            List<Glmain> GlMainList = new List<Glmain>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = string.Empty;

                if (Filter == "('GSTOPTIONS')")
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) where GL_Main_Code not in ('033','093','082','090') order by gl_main_description";
                else
                    sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) order by gl_main_description";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlMainList.Add(new Glmain(reader["gl_main_code"].ToString(), reader["gl_main_description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlMainList;
        }

        public List<Glmain> GetGlmainGSTOptionsOthers(string Filter)
        {
            List<Glmain> GlMainList = new List<Glmain>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = string.Empty;
                sSQL = "select distinct gl_main_code,gl_main_description from gl_master WITH (NOLOCK) order by gl_main_description";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlMainList.Add(new Glmain(reader["gl_main_code"].ToString(), reader["gl_main_description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlMainList;
        }

        public List<GlSub> GetGlSubGSTOptions(string GlMainValue)
        {
            List<GlSub> GlSubList = new List<GlSub>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208")
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' order by Gl_Sub_Code";
                }
                else
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' order by Gl_Sub_Description";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlSubList.Add(new GlSub(reader["Gl_Sub_Code"].ToString(), reader["Gl_Sub_Description"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlSubList;
        }

        public List<GlSub> GetGlSubGSTOptionsOthers(string GlMainValue)
        {
            List<GlSub> GlSubList = new List<GlSub>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208")
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' order by Gl_Sub_Code";
                }
                else
                {
                    sSQL = "select distinct Gl_Sub_Code,Gl_Sub_Description from Gl_Sub_Master WITH (NOLOCK) where Gl_Main_Code='" + GlMainValue + "' order by Gl_Sub_Description";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlSubList.Add(new GlSub(reader["Gl_Sub_Code"].ToString(), reader["Gl_Sub_Description"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlSubList;
        }

        public List<GlAccount> GetGlAccount(string GlMainValue, string GlSubValue, string BranchCode, string AutoTODstatus)
        {
            List<GlAccount> GlAccountList = new List<GlAccount>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208" && GlSubValue == "0140")
                {
                    sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                           " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                           " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Branch_Code='" + BranchCode + "' and b.Description not like '%CONTROL ACCOUNT%' Order by a.GL_Account_Code";
                }
                else
                {
                    if (BranchCode.ToUpper() != "CRP")
                    {
                        if (GlMainValue == "033" && GlSubValue == "0220")
                        {
                            sSQL = "select distinct a.GL_Account_Code,b.Description + ' | ' + c.Location Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                                   " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                                   " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Branch_Code='" + BranchCode + "'" +
                                   " inner join Customer_Master c WITH (NOLOCK) on c.Branch_Code=a.Branch_Code and c.Customer_Code=a.GL_Account_Code Order by b.Description + ' | ' + c.Location";
                        }
                        else
                        {
                            if (AutoTODstatus == "1")
                                sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                                   " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                                   " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Branch_Code='" + BranchCode + "'" +
                                   " left outer join TOD_Target_Master t WITH (NOLOCK) on t.Supplier_Code=Substring(b.Description,1,3) and t.Branch_Code=a.Branch_Code where t.Branch_Code is null Order by b.Description";
                            else

                                sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                                       " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                                       " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Branch_Code='" + BranchCode + "' Order by b.Description";
                        }
                    }
                    else
                    {
                        sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) Gl_Account_Master b WITH (NOLOCK) on " +
                               " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                               " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' Order by b.Description";
                    }
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlAccountList.Add(new GlAccount(reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlAccountList;
        }

        public List<GlAccount> GetGlAccountOthers(string GlMainValue, string GlSubValue, string BranchCode)
        {
            List<GlAccount> GlAccountList = new List<GlAccount>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208" && GlSubValue == "0140")
                {
                    sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                         " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                         " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.GL_Account_Code='3801CRP' and a.Branch_Code='" + BranchCode + "' and b.Description not like '%CONTROL ACCOUNT%' Order by a.GL_Account_Code";
                }
                else
                {
                    if (BranchCode.ToUpper() != "CRP")
                    {
                        sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                         " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                         " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Branch_Code='" + BranchCode + "' Order by b.Description";
                    }
                    else
                    {
                        sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) Gl_Account_Master b WITH (NOLOCK) on " +
                         " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and b.Description<>'' and b.Description is not null and" +
                         " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' Order by b.Description";
                    }
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlAccountList.Add(new GlAccount(reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlAccountList;
        }

        public List<GlAccount> GetGlAccountVendor(string GlMainValue, string GlSubValue, string BranchCode)
        {
            List<GlAccount> GlAccountList = new List<GlAccount>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on " +
                 " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and " +
                 " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' Order by b.Description";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlAccountList.Add(new GlAccount(reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlAccountList;
        }
        

        public List<GlAccount> GetGlAccount(string GlMainValue, string GlSubValue)
        {
            List<GlAccount> GlAccountList = new List<GlAccount>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;
                sSQL = "select distinct a.GL_Account_Code,b.Description from Chart_Of_Account a WITH (NOLOCK) Gl_Account_Master b WITH (NOLOCK) on " +
                 " a.Gl_Account_Code=b.Gl_Account_Code and a.gl_main_code = b.gl_main_code and a.gl_sub_code = b.gl_sub_code and " +
                 " a.Gl_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlAccountList.Add(new GlAccount(reader["GL_Account_Code"].ToString(), reader["Description"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return GlAccountList;
        }

        public List<GlBranch> GetGlBranch(string GlMainValue, string GlSubValue, string GlAccountValue)
        {
            List<GlBranch> GlBranchList = new List<GlBranch>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = "select distinct a.Branch_Code,b.Branch_Name from Chart_Of_Account a WITH (NOLOCK) inner join Branch_master b WITH (NOLOCK) on a.Branch_Code=b.Branch_Code " +
                "and a.GL_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Gl_Account_Code='" + GlAccountValue + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlBranchList.Add(new GlBranch(reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlBranchList;
        }

        public List<GlBranch> GetGlBranchOthers(string GlMainValue, string GlSubValue, string GlAccountValue, string GlBranchCode)
        {
            List<GlBranch> GlBranchList = new List<GlBranch>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                if (GlMainValue == "208" && GlSubValue == "0140" && GlAccountValue == "3801CRP")
                    sSQL = "select distinct a.Branch_Code,b.Branch_Name from Chart_Of_Account a WITH (NOLOCK) inner join Branch_master b WITH (NOLOCK) on a.Branch_Code=b.Branch_Code " +
                           "and a.GL_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Gl_Account_Code='" + GlAccountValue + "' and a.Branch_Code='" + GlBranchCode + "'";
                else
                    sSQL = "select distinct a.Branch_Code,b.Branch_Name from Chart_Of_Account a WITH (NOLOCK) inner join Branch_master b WITH (NOLOCK) on a.Branch_Code=b.Branch_Code " +
                           "and a.GL_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.Gl_Account_Code='" + GlAccountValue + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        GlBranchList.Add(new GlBranch(reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return GlBranchList;
        }

        public ChartDetails GetChartDetails(string GlMainValue, string GlSubValue, string GlAccountValue, string GlBranch)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            ChartDetails ChartDetails = null;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = "select distinct a.Chart_of_Account_Code,d.GL_Classification_Description,e.GL_Group_Description from " +
                "Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on a.GL_Account_Code=b.GL_Account_Code " +
                "inner join Branch_master c WITH (NOLOCK) on a.Branch_Code=c.Branch_Code " +
                "inner join gl_classification d WITH (NOLOCK) on a.Gl_Classification_Code=d.Gl_Classification_Code " +
                "inner join gl_group e WITH (NOLOCK) on a.GL_Group_Code=e.GL_Group_Code " +
                "inner join gl_sub_master f WITH (NOLOCK) on a.GL_Sub_Code=f.GL_Sub_Code " +
                "inner join gl_master g WITH (NOLOCK) on a.GL_Main_Code=g.GL_Main_Code " +
                "where a.GL_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.GL_Account_Code='" + GlAccountValue + "' and a.Branch_Code='" + GlBranch + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        ChartDetails = new ChartDetails(reader["Chart_of_Account_Code"].ToString(), reader["GL_Classification_Description"].ToString(), reader["GL_Group_Description"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return ChartDetails;
        }

        public ChartDetails GetChartDetailsOthers(string GlMainValue, string GlSubValue, string GlAccountValue, string GlBranch)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            ChartDetails ChartDetails = null;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = "select distinct a.Chart_of_Account_Code,d.GL_Classification_Description,e.GL_Group_Description from " +
                "Chart_Of_Account a WITH (NOLOCK) inner join Gl_Account_Master b WITH (NOLOCK) on a.GL_Account_Code=b.GL_Account_Code " +
                "inner join Branch_master c WITH (NOLOCK) on a.Branch_Code=c.Branch_Code " +
                "inner join gl_classification d WITH (NOLOCK) on a.Gl_Classification_Code=d.Gl_Classification_Code " +
                "inner join gl_group e WITH (NOLOCK) on a.GL_Group_Code=e.GL_Group_Code " +
                "inner join gl_sub_master f WITH (NOLOCK) on a.GL_Sub_Code=f.GL_Sub_Code " +
                "inner join gl_master g WITH (NOLOCK) on a.GL_Main_Code=g.GL_Main_Code " +
                "where a.GL_Main_Code='" + GlMainValue + "' and a.GL_Sub_Code='" + GlSubValue + "' and a.GL_Account_Code='" + GlAccountValue + "' and a.Branch_Code='" + GlBranch + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        ChartDetails = new ChartDetails(reader["Chart_of_Account_Code"].ToString(), reader["GL_Classification_Description"].ToString(), reader["GL_Group_Description"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return ChartDetails;
        }
    }

    public class GlBranch
    {
        public GlBranch(string BankCode, string BankName)
        {
            _BankCode = BankCode;
            _BankName = BankName;

        }

        private string _BankCode;
        private string _BankName;

        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

    }

    public class Glmain
    {
        public Glmain(string GlMainCode, string GlMainName)
        {
            _GlMainCode = GlMainCode;
            _GlMainName = GlMainName;

        }

        private string _GlMainCode;
        private string _GlMainName;

        public string GlMainCode
        {
            get { return _GlMainCode; }
            set { _GlMainCode = value; }
        }
        public string GlMainName
        {
            get { return _GlMainName; }
            set { _GlMainName = value; }
        }

    }

    public class GlSub
    {
        public GlSub(string GlSubCode, string GlSubName)
        {
            _GlSubCode = GlSubCode;
            _GlSubName = GlSubName;

        }

        private string _GlSubCode;
        private string _GlSubName;

        public string GlSubCode
        {
            get { return _GlSubCode; }
            set { _GlSubCode = value; }
        }
        public string GlSubName
        {
            get { return _GlSubName; }
            set { _GlSubName = value; }
        }

    }

    public class GlAccount
    {
        public GlAccount(string GlAccountCode, string GlAccountName)
        {
            _GlAccountCode = GlAccountCode;
            _GlAccountName = GlAccountName;

        }

        private string _GlAccountCode;
        private string _GlAccountName;

        public string GlAccountCode
        {
            get { return _GlAccountCode; }
            set { _GlAccountCode = value; }
        }
        public string GlAccountName
        {
            get { return _GlAccountName; }
            set { _GlAccountName = value; }
        }

    }

    public class ChartDetails
    {
        public ChartDetails(string ChartCode, string GlClassification, string GlGroup)
        {
            _ChartCode = ChartCode;
            _GlClassification = GlClassification;
            _GlGroup = GlGroup;

        }

        private string _ChartCode;
        private string _GlClassification;
        private string _GlGroup;

        public string ChartCode
        {
            get { return _ChartCode; }
            set { _ChartCode = value; }
        }
        public string GlClassification
        {
            get { return _GlClassification; }
            set { _GlClassification = value; }
        }

        public string GlGroup
        {
            get { return _GlGroup; }
            set { _GlGroup = value; }
        }

    }

}


