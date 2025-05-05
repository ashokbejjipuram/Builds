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
    public class SalesManTargets
    {
        public void AddNewSalesManTarget(string SalesManCode, string SupplierLinecode, string CustomerCode, string Year, string TargetAmt, string Actual, string Expenses)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSalesmantarget");

            ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, SalesManCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierLinecode.Trim());
            ImpalDB.AddInParameter(cmd, "@Year", DbType.Int32 , Year.Trim());
            ImpalDB.AddInParameter(cmd, "@Target_Amount", DbType.Currency, Convert.ToDouble(TargetAmt));
            ImpalDB.AddInParameter(cmd, "@Actual_Achieved", DbType.Currency, Convert.ToDouble(Actual));
            ImpalDB.AddInParameter(cmd, "@Salesman_Expenses", DbType.Currency, Convert.ToDouble(Expenses));
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateSalesManTarget(string SalesManCode, string SupplierLinecode, string CustomerCode, string Year, string TargetAmt, string Actual, string Expenses)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            ////// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updsalesmantarget");
            ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, SalesManCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierLinecode.Trim());
            ImpalDB.AddInParameter(cmd, "@Year", DbType.Int32, Year.Trim());
            ImpalDB.AddInParameter(cmd, "@Target_Amount", DbType.Currency, Convert.ToDouble(TargetAmt));
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<SalesManTarget> GetAllSalesManTarget(string Filter)
        {
            List<SalesManTarget> SalesManTargetList = new List<SalesManTarget>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select a.Sales_Man_Code ,d.Sales_Man_Name,b.Short_Description,b.Supplier_Line_Code , c.Customer_Code,c.Customer_Name,a.Year, a.Target_Amount , " +
                    " a.Actual_Achieved,a.Salesman_Expenses from Sales_Man_Target a WITH (NOLOCK) Inner join Supplier_Line_Master b WITH (NOLOCK) on " +
                    " a.Supplier_Line_Code = b.Supplier_Line_Code Inner Join Customer_Master c WITH (NOLOCK) on a.Branch_Code=c.Branch_Code and a.Customer_Code = c.Customer_Code " +
                    " Inner Join Sales_Man_Master d WITH (NOLOCK) on c.Branch_Code=d.Branch_Code and c.Sales_Man_Code = d.Sales_Man_Code ";
            if (Filter != "ALL")
                sSQL = sSQL + " WHERE a.Sales_Man_Code ='" + Filter + "'";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesManTargetList.Add(new SalesManTarget(reader["Sales_Man_Code"].ToString(), reader["Sales_Man_Name"].ToString(), reader["Short_Description"].ToString(), reader["Supplier_Line_Code"].ToString(),
                            reader["Customer_Code"].ToString(), reader["Customer_Name"].ToString(), reader["Year"].ToString(), reader["Target_Amount"].ToString(), reader["Actual_Achieved"].ToString(), reader["Salesman_Expenses"].ToString()));
                }
            
            }

            return SalesManTargetList;
        }

        public List<SalesManTarget> GetAllSalesManTarget()
        {
            List<SalesManTarget> SalesManTargetList = new List<SalesManTarget>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select a.Sales_Man_Code ,d.Sales_Man_Name,b.Short_Description,b.Supplier_Line_Code , c.Customer_Code,c.Customer_Name,a.Year, a.Target_Amount , " +
                " a.Actual_Achieved,a.Salesman_Expenses from Sales_Man_Target a WITH (NOLOCK) Inner join Supplier_Line_Master b WITH (NOLOCK) on " +
                " a.Supplier_Line_Code = b.Supplier_Line_Code Inner Join Customer_Master c WITH (NOLOCK) on a.Branch_Code=c.Branch_Code and a.Customer_Code = c.Customer_Code " +
                " Inner Join Sales_Man_Master d WITH (NOLOCK) on c.Branch_Code=d.Branch_Code and a.Sales_Man_Code = d.Sales_Man_Code ";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesManTargetList.Add(new SalesManTarget(reader["Sales_Man_Code"].ToString(), reader["Sales_Man_Name"].ToString(), reader["Short_Description"].ToString(), reader["Supplier_Line_Code"].ToString(),
                        reader["Customer_Code"].ToString(), reader["Customer_Name"].ToString(), reader["Year"].ToString(), reader["Target_Amount"].ToString(), reader["Actual_Achieved"].ToString(), reader["Salesman_Expenses"].ToString()));
                }
            }
            return SalesManTargetList;
        }
        
    }
    public class SalesManTarget
    {
        public SalesManTarget(string SalesManCode, string SalesManName, string SupplierName, string SupplierLinecode, string CustomerCode, string CustomerName, string Year, string TargetAmt, string Actual, string Expenses)
        {
            _SalesManCode = SalesManCode;
            _SalesManName = SalesManName;
            _SupplierLinecode = SupplierLinecode;
            _SupplierName = SupplierName;
            _CustomerCode = CustomerCode;
            _CustomerName = CustomerName;
            _Year = Year;
            _TargetAmt = TargetAmt;
            _Actual = Actual;
            _Expenses = Expenses;
        }
        public SalesManTarget()
        {

        }

        private string _SalesManCode;
        private string _SalesManName;
        private string _SupplierLinecode;
        private string _SupplierName;
        private string _CustomerCode;
        private string _CustomerName;
        private string _Year;
        private string _TargetAmt;
        private string _Actual;
        private string _Expenses;

        public string SalesManCode
        {
            get { return _SalesManCode; }
            set { _SalesManCode = value; }
        }
        public string SalesManName
        {
            get { return _SalesManName; }
            set { _SalesManName = value; }
        }
        public string SupplierLinecode
        {
            get { return _SupplierLinecode; }
            set { _SupplierLinecode = value; }
        }
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
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
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public string Actual
        {
            get { return _Actual; }
            set { _Actual = value; }
        }
        public string TargetAmt
        {
            get { return _TargetAmt; }
            set { _TargetAmt = value; }
        }
        public string Expenses
        {
            get { return _Expenses; }
            set { _Expenses = value; }
        }
        
    }

    public class SalesmanSupplierLines
    {
        public List<SMSupplierLine> GetItemSuppliers()
        {
            List<SMSupplierLine> SupplierLines = new List<SMSupplierLine>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Supplier_Line_Code,Short_Description from Supplier_Line_Master ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierLines.Add(new SMSupplierLine(reader["Supplier_Line_Code"].ToString(), reader["Short_Description"].ToString()));
                }
            }
            return SupplierLines;
        }
    }
    public class SMSupplierLine
    {
        public SMSupplierLine(string Code, string Name)
        {
            _Code = Code;
            _Name = Name;
        }
        public SMSupplierLine()
        {

        }
        private string _Code;
        private string _Name;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }

   
  
}
