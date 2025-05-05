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
    public class BranchSalesTaxForms
    {
        public void AddNewBranchSalesTax(string BranchCode, string Serialnumber, string SupplierDealerIndicator, string ReceiveIssueIndicator, string STFormName)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_ADDSTform");
            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Dealer_Indicator", DbType.String, SupplierDealerIndicator.Trim());
            ImpalDB.AddInParameter(cmd, "@Receive_Issue_Indicator", DbType.String, ReceiveIssueIndicator.Trim());
            ImpalDB.AddInParameter(cmd, "@ST_Form_Name", DbType.String, STFormName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateBranchSalesTax(string BranchCode, string Serialnumber, string SupplierDealerIndicator, string ReceiveIssueIndicator, string STFormName)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updtSTform");
            
            ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.String, Serialnumber.Trim());
            ImpalDB.AddInParameter(cmd, "@ST_Form_Name", DbType.String, STFormName.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }

        public List<BranchSalesTax> GetAllBranchSalesTexes(string Filter)
        {
            List<BranchSalesTax> BranchSalesTaxList = new List<BranchSalesTax>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = " SELECT Supplier_Dealer_Indicator,Receive_Issue_Indicator,ST_Form_Name,a.Branch_Code,serial_number,Branch_name " +
                    " FROM branch_ST_forms b Inner Join branch_master a on a.branch_code =b.branch_code ";
                

            if (Filter != "ALL")
                sSQL = sSQL + " Where Serial_number ='" + Filter + "'";
            //    " WHERE a.branch_Code =@Branch_Code and Serial_Number = @Serial_Number " +
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BranchSalesTaxList.Add(new BranchSalesTax(reader["Branch_Code"].ToString(), reader["Serial_number"].ToString(), reader["Supplier_Dealer_Indicator"].ToString(), reader["Receive_Issue_Indicator"].ToString(), reader["ST_Form_Name"].ToString()));
                }
            }
            return BranchSalesTaxList;
        }

        public List<BranchSalesTax> GetAllBranchSalesTexes()
        {
            List<BranchSalesTax> BranchSalesTaxList = new List<BranchSalesTax>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = " SELECT Supplier_Dealer_Indicator,Receive_Issue_Indicator,ST_Form_Name,a.Branch_Code,serial_number,Branch_name " +
                    " FROM branch_ST_forms b Inner Join branch_master a on a.branch_code =b.branch_code "; ;
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BranchSalesTaxList.Add(new BranchSalesTax(reader["Branch_Code"].ToString(), reader["Serial_number"].ToString(), reader["Supplier_Dealer_Indicator"].ToString(), reader["Receive_Issue_Indicator"].ToString(), reader["ST_Form_Name"].ToString()));
                }
            }
            return BranchSalesTaxList;
        }
    }

    public class BranchSalesTax
    {
        public BranchSalesTax(string BranchCode, string Serialnumber, string SupplierDealerIndicator, string ReceiveIssueIndicator, string STFormName)
        {
            _BranchCode = BranchCode;
            _Serialnumber = Serialnumber;
            _SupplierDealerIndicator = SupplierDealerIndicator;
            _ReceiveIssueIndicator = ReceiveIssueIndicator;
            _STFormName = STFormName;

        }
        public BranchSalesTax()
        {

        }
        private string _BranchCode;
        private string _Serialnumber;
        private string _SupplierDealerIndicator;
        private string _ReceiveIssueIndicator;
        private string _STFormName;

        public string BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public string Serialnumber
        {
            get { return _Serialnumber; }
            set { _Serialnumber = value; }
        }
        public string SupplierDealerIndicator
        {
            get
            {
                if (_SupplierDealerIndicator == "S")
                    return "Supplier";
                else
                    return "Dealer";
            }
            set { _SupplierDealerIndicator = value; }
        }
        public string ReceiveIssueIndicator
        {
            get { 
                if (_ReceiveIssueIndicator == "R")
                        return "Receive";
                else
                        return "Issue";
            }
            set
            {  _ReceiveIssueIndicator = value;  }
        }
        public string STFormName
        {
            get { return _STFormName; }
            set { _STFormName = value; }
        }



    }
}
