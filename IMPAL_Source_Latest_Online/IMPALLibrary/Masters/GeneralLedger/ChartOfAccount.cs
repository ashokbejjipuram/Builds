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
    public class ChartOfAccounts
    {
        public void AddNewChartOfAccount(string GL_Classification_Code, string Gl_Sub_Description, string Gl_Main_Description,
                    string Gl_Group_Description,string GL_Account_Code)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addchartofaccount_New");

            ImpalDB.AddInParameter(cmd, "@GL_Account_Code", DbType.String, GL_Account_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Sub_Code", DbType.String, Gl_Sub_Description.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Main_Code", DbType.String, Gl_Main_Description.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Group_Code", DbType.String, Gl_Group_Description.Trim());
            ImpalDB.AddInParameter(cmd, "@GL_Classification_Code", DbType.String, GL_Classification_Code.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
      
        public List<DropGlClassfication> GetAllDrpChartOfAccount()
        {
            List<DropGlClassfication> ChartOfAccountList = new List<DropGlClassfication>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select Gl_Classification_Code,GL_Classification_Description  from GL_Classification ";

            ChartOfAccountList.Add(new DropGlClassfication("-1", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ChartOfAccountList.Add(new DropGlClassfication(reader["GL_Classification_Code"].ToString(), reader["GL_Classification_Description"].ToString()));
                }
            }
            return ChartOfAccountList;
        }
        public List<DropGlBranchClassification> GetAllDrpChartOfAccountBranch(string classification, string group, string main, string subcode)
        {
            List<DropGlBranchClassification> ChartOfAccountBranchList = new List<DropGlBranchClassification>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select distinct a.Branch_Code,b.Branch_Name from Chart_Of_Account a,Branch_Master b where a.Branch_Code=b.Branch_Code and a.GL_Classification_Code="
            + "'" + classification + "' and a.GL_Group_Code='" + group + "' and a.Gl_Main_Code='" + main + "' and a.GL_Sub_Code= '" + subcode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ChartOfAccountBranchList.Add(new DropGlBranchClassification(reader["Branch_Code"].ToString(), reader["Branch_Name"].ToString()));
                }
            }
            return ChartOfAccountBranchList;
        }


        public List<ChartOfAccount> GetAllChartOfAccount()
        {
            List<ChartOfAccount> ChartOfAccountList = new List<ChartOfAccount>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.GL_Classification_Code,b.GL_Classification_Description , a.GL_Account_Code /*,f.Description */, a.Branch_Code, a.Chart_of_Account_Code," +
            " c.GL_Group_Code,c.Gl_Group_Description,a.GL_Main_Code , d.Gl_Main_Description , e.GL_Sub_Code,e.Gl_Sub_Description " +
            "  from Chart_Of_Account a  left Join Gl_Account_Master f on " +
	        " (a.GL_Main_Code =f.GL_Main_Code and a.GL_Sub_Code = f.GL_Sub_Code and a.GL_Sub_Code = f.GL_Sub_Code ) "+
	        "    inner Join GL_Classification b on b.GL_Classification_Code=a.GL_Classification_Code " +
	        "    Inner join Gl_Group c On (a.GL_Group_Code = c.GL_Group_Code and a.Gl_Classification_Code = c.Gl_Classification_Code ) "+
            "    Inner join Gl_Master d on (a.GL_Group_Code = d.GL_Group_Code and a.GL_Main_Code  = d.GL_Main_Code ) "+
            "    Inner Join Gl_Sub_Master e on (a.GL_Main_Code = e.GL_Main_Code and a.GL_Sub_Code = e.GL_Sub_Code ) " +
            "    Order by a.Gl_Classification_Code ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ChartOfAccountList.Add(new ChartOfAccount(reader["GL_Classification_Code"].ToString(), reader["GL_Classification_Description"].ToString(), reader["Branch_Code"].ToString(),/* reader["Description"].ToString(),*/
                        reader["Gl_Sub_Description"].ToString(), reader["Gl_Main_Description"].ToString(), reader["Gl_Group_Description"].ToString(), reader["Chart_of_Account_Code"].ToString(),
                        reader["GL_Account_Code"].ToString()));
                }
            }
            return ChartOfAccountList;
        }

        public List<ChartOfAccount> GetAllChartOfAccount(string Filter)
        {
            List<ChartOfAccount> ChartOfAccountList = new List<ChartOfAccount>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct a.GL_Classification_Code,b.GL_Classification_Description , a.GL_Account_Code /*,f.Description */, a.Branch_Code, a.Chart_of_Account_Code," +
            " c.GL_Group_Code,c.Gl_Group_Description,a.GL_Main_Code , d.Gl_Main_Description , e.GL_Sub_Code,e.Gl_Sub_Description " +
            "  from Chart_Of_Account a  left Join Gl_Account_Master f on " +
            " (a.GL_Main_Code =f.GL_Main_Code and a.GL_Sub_Code = f.GL_Sub_Code and a.GL_Sub_Code = f.GL_Sub_Code ) " +
            "    inner Join GL_Classification b on b.GL_Classification_Code=a.GL_Classification_Code " +
            "    Inner join Gl_Group c On (a.GL_Group_Code = c.GL_Group_Code and a.Gl_Classification_Code = c.Gl_Classification_Code ) " +
            "    Inner join Gl_Master d on (a.GL_Group_Code = d.GL_Group_Code and a.GL_Main_Code  = d.GL_Main_Code ) " +
            "    Inner Join Gl_Sub_Master e on (a.GL_Main_Code = e.GL_Main_Code and a.GL_Sub_Code = e.GL_Sub_Code ) ";
            
            if (Filter != "")
                sSQL = sSQL + "and a.GL_Classification_Code =" + Filter + "";

            sSQL = sSQL + "  Order by a.Gl_Classification_Code ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ChartOfAccountList.Add(new ChartOfAccount(reader["GL_Classification_Code"].ToString(), reader["GL_Classification_Description"].ToString(), reader["Branch_Code"].ToString(),/* reader["Description"].ToString(),*/
                        reader["Gl_Sub_Description"].ToString(), reader["Gl_Main_Description"].ToString(), reader["Gl_Group_Description"].ToString(), reader["Chart_of_Account_Code"].ToString(),
                        reader["GL_Account_Code"].ToString()));
                }
            }
            return ChartOfAccountList;
        }
    }
    public class DropGlBranchClassification
    { 
        public DropGlBranchClassification(string BranchCode,string BranchName)
        {
        _BranchCode = BranchCode;
        _BranchName = BranchName;
        }
        public DropGlBranchClassification() { }
       private string _BranchCode;
       private string _BranchName;

       public string BranchCode
       {
           get { return _BranchCode; }
           set { _BranchCode = value; }
       }
       public string BranchName
       {
           get { return _BranchName; }
           set { _BranchName = value; }
       }
    }

    public class DropGlClassfication
    {
        public DropGlClassfication( string GL_Classification_Code,string GL_Classification_Description)
        {
            _GL_Classification_Code = GL_Classification_Code;
            _GL_Classification_Description = GL_Classification_Description;
        }
        public DropGlClassfication() { }

        private string _GL_Classification_Code;
        private string _GL_Classification_Description;
        public string GL_Classification_Description
        {
            get { return _GL_Classification_Description; }
            set { _GL_Classification_Description = value; }
        }
        public string GL_Classification_Code
        {
            get { return _GL_Classification_Code; }
            set { _GL_Classification_Code = value; }
        }
    
    }
    public class ChartOfAccount
    {
        public ChartOfAccount(string GL_Classification_Code,string GL_Classification_Description, string Branch_Code, /*string Description,*/ 
                    string Gl_Sub_Description, string Gl_Main_Description, 
                    string Gl_Group_Description, string Chart_of_Account_Code,string GL_Account_Code)
        {
            _GL_Classification_Description = GL_Classification_Description;
            _Branch_Code = Branch_Code;
            _Description = Description;
            _Gl_Sub_Description = Gl_Sub_Description;
            _Gl_Main_Description = Gl_Main_Description;
            _Gl_Group_Description = Gl_Group_Description;
            _Chart_of_Account_Code = Chart_of_Account_Code;
            _GL_Classification_Code = GL_Classification_Code;
            _GL_Account_Code = GL_Account_Code;
        }
        //public ChartOfAccount(string GL_Classification_Code, string Branch_Code, string GL_Account_Code, string GL_Sub_Code, string GL_Main_Code, string GL_Group_Code, string Chart_of_Account_Code)
        //{
        //    _Chart_of_Account_Code = Chart_of_Account_Code;
        //    _Branch_Code = Branch_Code;
        //    _GL_Account_Code = GL_Account_Code;
        //    _GL_Sub_Code = GL_Sub_Code;
        //    _GL_Main_Code = GL_Main_Code;
        //    _GL_Group_Code = GL_Group_Code;
        //    _GL_Classification_Code = GL_Classification_Code;
        //}
        public ChartOfAccount()
        {

        }

        private string  _GL_Classification_Code;
        private string  _GL_Classification_Description;
        private string  _Branch_Code ;
        private string  _Description ;
        private string  _Gl_Sub_Description ;
        private string  _Gl_Main_Description;
        private string  _Gl_Group_Description;
        private string  _Chart_of_Account_Code;
        
        private string  _GL_Account_Code;

        /*private string _Chart_of_Account_Code;
        private string _Branch_Code;
        private string _GL_Account_Code;
        private string _GL_Sub_Code;
        private string _GL_Main_Code;
        private string _GL_Group_Code;
        private string _GL_Classification_Code; */
        
        public string Chart_of_Account_Code
        {
            get { return _Chart_of_Account_Code; }
            set { _Chart_of_Account_Code = value; }
        }
        public string Branch_Code
        {
            get { return _Branch_Code; }
            set { _Branch_Code = value; }
        }

        public string GL_Classification_Description
        {
            get { return _GL_Classification_Description; }
            set { _GL_Classification_Description = value; }
        }
        public string GL_Classification_Code
        {
            get { return _GL_Classification_Code; }
            set { _GL_Classification_Code = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Gl_Sub_Description
        {
            get { return _Gl_Sub_Description; }
            set { _Gl_Sub_Description = value; }
        }
        public string Gl_Main_Description
        {
            get { return _Gl_Main_Description; }
            set { _Gl_Main_Description = value; }
        }
        public string Gl_Group_Description
        {
            get { return _Gl_Group_Description; }
            set { _Gl_Group_Description = value; }
        }

        public string GL_Account_Code
        {
            get { return _GL_Account_Code; }
            set { _GL_Account_Code = value; }
        }
        //public string GL_Sub_Code
        //{
        //    get { return _GL_Sub_Code; }
        //    set { _GL_Sub_Code = value; }
        //}
        //public string GL_Main_Code
        //{
        //    get { return _GL_Main_Code; }
        //    set { _GL_Main_Code = value; }
        //}
        //public string GL_Group_Code
        //{
        //    get { return _GL_Group_Code; }
        //    set { _GL_Group_Code = value; }
        //}
    
    
    }
}
