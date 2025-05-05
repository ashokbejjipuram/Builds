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
    public class BudgetMasters
    {
        public void AddNewBudgetMaster(string Budget_Year, string Chart_Of_Account_Code, string Budget_amount, string Approved_Amount)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddBudget");
            ImpalDB.AddInParameter(cmd, "@Budget_Year", DbType.String, Budget_Year);
            ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, Chart_Of_Account_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Budget_amount", DbType.Currency, Budget_amount.Trim());
            ImpalDB.AddInParameter(cmd, "@Approved_Amount", DbType.Currency, Approved_Amount.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateBudgetMaster(string Budget_Year, string Chart_Of_Account_Code, string Budget_amount, string Approved_Amount)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdBudget");

            ImpalDB.AddInParameter(cmd, "@Budget_Year", DbType.String, Budget_Year);
            ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, Chart_Of_Account_Code.Trim());
            ImpalDB.AddInParameter(cmd, "@Budget_amount", DbType.Currency, Budget_amount.Trim());
            ImpalDB.AddInParameter(cmd, "@Approved_Amount", DbType.Currency, Approved_Amount.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<BudgetMaster> GetAllBudgetMasters(string Filter)
        {
            List<BudgetMaster> BudgetMasters = new List<BudgetMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select Budget_Year,Chart_of_Account_Code,Budget_Amount,Approved_Amount ,Actual_Amount from Budget_Master ";

            if (Filter != "ALL")
                sSQL = sSQL + " WHERE Budget_Master.Budget_Year =" + Filter + "";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BudgetMasters.Add(new BudgetMaster(reader["Budget_Year"].ToString(), reader["Chart_of_Account_Code"].ToString(), reader["Budget_Amount"].ToString(), reader["Approved_Amount"].ToString(), reader["Actual_Amount"].ToString()));
                }
            }
            return BudgetMasters;
        }

        public List<BudgetMaster> GetChartofAccountCode()
        {
            List<BudgetMaster> BudgetMasters = new List<BudgetMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select distinct substring (budget_master.Chart_of_Account_Code,4,3),Gl_main_description";
            sSQL=sSQL+" from budget_master,GL_master where substring (budget_master.Chart_of_Account_Code,4,3)= Gl_master.Gl_main_code";

            BudgetMasters.Add(new BudgetMaster("0", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BudgetMasters.Add(new BudgetMaster(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return BudgetMasters;
        }

        public List<BudgetMaster> GetAllBudgetMasters()
        {
            List<BudgetMaster> BudgetMasters = new List<BudgetMaster>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Budget_Year,Chart_of_Account_Code,Budget_Amount,Approved_Amount ,Actual_Amount from Budget_Master ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BudgetMasters.Add(new BudgetMaster(reader["Budget_Year"].ToString(), reader["Chart_of_Account_Code"].ToString(), reader["Budget_Amount"].ToString(), reader["Approved_Amount"].ToString(), reader["Actual_Amount"].ToString()));
                }

            }

            return BudgetMasters;
        }

    }
    public class BudgetMaster
    {
        public BudgetMaster(string Budget_Year, string Chart_of_Account_Code, string Budget_Amount, string Approved_Amount, string Actual_Amount)
        {
            _Budget_Year = Budget_Year;
            _Chart_of_Account_Code = Chart_of_Account_Code;
            _Budget_Amount = Budget_Amount;
            _Approved_Amount = Approved_Amount;
            _Actual_Amount = Actual_Amount;
        }
        public BudgetMaster()
        {

        }
        private string _Budget_Year;
        private string _Chart_of_Account_Code;
        private string _Budget_Amount;
        private string _Approved_Amount;
        private string _Actual_Amount;
        private string _Gl_main_description;

        public BudgetMaster(string Chart_of_Account_Code, string Gl_main_description)
        {
            _Chart_of_Account_Code = Chart_of_Account_Code;
            _Gl_main_description = Gl_main_description;
        }

        public string Budget_Year
        {
            get { return _Budget_Year; }
            set { _Budget_Year = value; }
        }
        public string Gl_main_description
        {
            get { return _Gl_main_description; }
            set { _Gl_main_description = value; }
        }
        public string Chart_of_Account_Code
        {
            get { return _Chart_of_Account_Code; }
            set { _Chart_of_Account_Code = value; }
        }
        public string Budget_Amount
        {
            get { return _Budget_Amount; }
            set { _Budget_Amount = value; }
        }
        public string Approved_Amount
        {
            get { return _Approved_Amount; }
            set { _Approved_Amount = value; }
        }
        public string Actual_Amount
        {
            get { return _Actual_Amount; }
            set { _Actual_Amount = value; }
        }

    }

    public class BudgetYears
    {
        public List<BudgetYear> GetBudgetYears()
        {
            List<BudgetYear> BudgetYear = new List<BudgetYear>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Budget_Year from Budget_Master ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BudgetYear.Add(new BudgetYear(reader["Budget_Year"].ToString()));
                }
            }

            return BudgetYear;
        }
    }

    public class BudgetYear
    {
        public BudgetYear(string Budgetyear)
        {
            _BudgetYear = Budgetyear;
        }
        public BudgetYear()
        {

        }
        private string _BudgetYear;

        public string Budgetyear
        {
            get { return _BudgetYear; }
            set { _BudgetYear = value; }
        }
    }
}
