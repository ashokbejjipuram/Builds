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

    public class CustomerSLBS
    {
        public void AddNewSLBGroup(string CustomerCode, string SupplierLineCode, string SLBCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();


            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSlbDetail");
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_code", DbType.String, SupplierLineCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, SLBCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateSLBGroup(string Custcode, string CustName, string SupplierLineCode, string SupplierLine, string SLBCode, string SLBDesc)
        //    string CustomerCode,string  string SupplierLineCode, string SLBCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updslbdetail");

            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, Custcode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_code", DbType.String, SupplierLineCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, SLBCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<CustomerSLB> GetSlbGroup(string strBranchCode)
        {
            List<CustomerSLB> CustomerSLBS = new List<CustomerSLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select b.Customer_Name, b.Customer_Code , c.Short_Description, a.Supplier_Line_Code , d.SLB_Description , a.SLB_Code " +
                " from SLB_Detail a WITH (NOLOCK) inner join Customer_Master b WITH (NOLOCK) on a.Customer_Code = b.Customer_Code " +
                " inner join Supplier_Line_Master c WITH (NOLOCK) on a.Supplier_Line_Code = c.Supplier_Line_Code " +
                " Inner join SLB_Master d WITH (NOLOCK) on a.SLB_Code = d.SLB_Code where b.Branch_Code = '" + strBranchCode + "'";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
           using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
           {
                while (reader.Read())
                {
                    CustomerSLBS.Add(new CustomerSLB(reader["Customer_Code"].ToString(), reader["Customer_Name"].ToString(), reader["Supplier_Line_Code"].ToString(), reader["Short_Description"].ToString(), reader["SLB_Code"].ToString(),reader["SLB_Description"].ToString()));
                }
            }

            return CustomerSLBS;
        }

       
    }

    public class CustomerSLB 
    {
        public CustomerSLB(string Custcode, string CustName, string SupplierLineCode, string SupplierLine, string SLBCode, string SLBDesc)
        {
            _Custcode = Custcode;
            _CustName = CustName;
            _SupplierLineCode = SupplierLineCode;
            _SupplierLine = SupplierLine;
            _SLBCode = SLBCode;
            _SLBDesc = SLBDesc;
        }
        public CustomerSLB()
        {
           
        }
        private string _Custcode;
        private string _CustName;
        private string _SupplierLineCode;
        private string _SupplierLine;
        private string _SLBCode;
        private string _SLBDesc;

        public string Custcode 
        {
            get { return _Custcode; }
            set { _Custcode = value; }
        }
        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }

        public string SupplierLineCode
        {
            get { return _SupplierLineCode; }
            set { _SupplierLineCode = value; }
        }

        public string SupplierLine
        {
            get { return _SupplierLine; }
            set { _SupplierLine = value; }
        }
        public string SLBCode
        {
            get { return _SLBCode; }
            set { _SLBCode = value; }
        }
        public string SLBDesc
        {
            get { return _SLBDesc; }
            set { _SLBDesc = value; }
        }


    }
    public class CustomerNames {
        public List<CustomerLst> GetCustomerList(string strBranchCode)
        {
            List<CustomerLst> CustomerNames = new List<CustomerLst>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select Customer_Name, Customer_Code from Customer_Master WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "'";
            CustomerNames.Add(new CustomerLst("-1", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    CustomerNames.Add(new CustomerLst(reader["Customer_Code"].ToString(),reader["Customer_Name"].ToString() ));
                }
            }

            return CustomerNames;
        }
    }
    public class CustomerLst { 
            public CustomerLst(string CustomerCode, string CustomerName)
        {
            _CustomerCode = CustomerCode;
            _CustomerName = CustomerName;
        }
        public CustomerLst()
        {
           
        }
        private string _CustomerCode;
        private string _CustomerName;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

    }

    public class SupplierLineNames
    {
        public List<SupplierLineLst> GetSupplierLineList()
        {
            List<SupplierLineLst> SupplierLineNames = new List<SupplierLineLst>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select Supplier_Line_Code , Short_Description from Supplier_Line_Master ";
            SupplierLineNames.Add(new SupplierLineLst("-1", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierLineNames.Add(new SupplierLineLst(reader["Supplier_Line_Code"].ToString(), reader["Short_Description"].ToString()));
                }
            }
            return SupplierLineNames;
        }
    }
    public class SupplierLineLst
    {
        public SupplierLineLst(string SupplierLineCode, string SupplierShortName)
        {
            _SupplierLineCode = SupplierLineCode;
            _SupplierShortName = SupplierShortName;
        }
        public SupplierLineLst()
        {

        }
        private string _SupplierLineCode;
        private string _SupplierShortName;
        public string SupplierLineCode
        {
            get { return _SupplierLineCode; }
            set { _SupplierLineCode = value; }
        }
        public string SupplierShortName
        {
            get { return _SupplierShortName; }
            set { _SupplierShortName = value; }
        }

    }

    public class SLBNames
    {
        public List<SLBLst> GetSLBNameList()
        {
            List<SLBLst> SLBNames = new List<SLBLst>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " select SLB_Code, SLB_Description from SLB_Master ";
            SLBNames.Add(new SLBLst("-1", ""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SLBNames.Add(new SLBLst(reader["SLB_Code"].ToString(), reader["SLB_Description"].ToString()));
                }
            }
            return SLBNames;
        }
    }
    public class SLBLst
    {
        public SLBLst(string SLB_Code, string SLB_Description)
        {
            _SLB_Code = SLB_Code;
            _SLB_Description = SLB_Description;
        }
        public SLBLst()
        {

        }
        private string _SLB_Code;
        private string _SLB_Description;
        public string SLB_Code
        {
            get { return _SLB_Code; }
            set { _SLB_Code = value; }
        }
        public string SLB_Description
        {
            get { return _SLB_Description; }
            set { _SLB_Description = value; }
        }

    }

}
