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
    public class Banks
    {

        public string BankCode { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string ChartOfAccountCode { get; set; }
        public string BankBranchCode { get; set;}


        public List<Bank> GetAllBanks()
        {
            List<Bank> BankLst = new List<Bank>();
            BankLst.Add(new Bank(string.Empty,string.Empty));

            Database ImpalDB = DataAccess.GetDatabase();
            
            string sSQL = "Select Bank_Code,Bank_Name from Bank_Master";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BankLst.Add(new Bank(reader["Bank_code"].ToString(), reader["Bank_Name"].ToString()));
                }
            }

            return BankLst;
        }
        public void AddNewBanks(string BankName)
        {
             Database ImpalDB = DataAccess.GetDatabase();

             // Create command to execute the stored procedure and add the parameters.
             DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbank");
             ImpalDB.AddInParameter(cmd, "@Bank_Name", DbType.String, BankName.Trim());
             cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
             ImpalDB.ExecuteNonQuery(cmd);

        }

        public void UpdateBank(string BankCode, string BankName)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updbank");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Bank_Code", DbType.String, BankCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Bank_Name", DbType.String, BankName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public DataSet GetBankBranchDetails(string strBank)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = "SELECT BBM.Bank_Branch_Code,BBM.Bank_Code,BBM.Address,BBM.Chart_of_Account_Code,BBM.Phone,BBM.Fax,BBM.Contact_Person,BBM.Email,BBM.Account_Number,BBM.Branch_Name,BM.Bank_Name from  Bank_Branch_Master BBM inner join Bank_Master BM on BBM.Bank_Code=BM.Bank_Code ";
            if (!string.IsNullOrEmpty(strBank) && ! strBank.Equals("ALL"))
            {
                strQuery += " Where BBM.Bank_Code=" + strBank;  
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);
            return ds;
   
        }

        public void AddBankBranch()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbankbranch");

            ImpalDB.AddInParameter(cmd, "@Bank_Code", DbType.Int32, BankCode);
            ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, BranchName.Trim());
            ImpalDB.AddInParameter(cmd, "@Account_Number", DbType.String, AccountNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, Address.Trim());
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, Phone.Trim());
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, Fax.Trim());
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, ContactPerson.Trim());
            ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, ChartOfAccountCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateBankBranch()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updbankbranch");

            ImpalDB.AddInParameter(cmd, "@Bank_Code", DbType.Int32, BankCode);
            ImpalDB.AddInParameter(cmd, "@Bank_Branch_Code", DbType.Int32, BankBranchCode);
            ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, BranchName.Trim());
            ImpalDB.AddInParameter(cmd, "@Account_Number", DbType.String, AccountNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, Address.Trim());
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, Phone.Trim());
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, Fax.Trim());
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, ContactPerson.Trim());
            ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, ChartOfAccountCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class Bank
    {
        public Bank(string BankCode, string BankName)
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
}