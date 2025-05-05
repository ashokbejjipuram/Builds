using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace IMPALLibrary
{
    public class SalesManMaster
    {
        public void AddNewSalesMan(string SalesManCode, string SalesManName, string Designation, string Branch, string StartDate, string EndDate, string Status, string BrCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addsalesman");

            //string SDate = StartDate.ToString("dd/MM/yyyy");
            //string EDate = EndDate.ToString("dd/MM/yyyy");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, SalesManCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Man_Name", DbType.String, SalesManName.Trim());
            ImpalDB.AddInParameter(cmd, "@Designation", DbType.String, Designation.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BrCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate.Trim());
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate.Trim());
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, Status.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateSalesMan(string SalesManCode, string SalesManName, string Designation, string Branch, string StartDate, string EndDate, string Status, string BrCode)
        {            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updsalesman");
            //string SDate = StartDate.ToString("dd/MM/yyyy");
            //string EDate = EndDate.ToString("dd/MM/yyyy");
            
            ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, SalesManCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Man_Name", DbType.String, SalesManName.Trim());
            ImpalDB.AddInParameter(cmd, "@Designation", DbType.String, Designation.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BrCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, StartDate);
            ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, EndDate);
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String, Status.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<SalesMans> GetAllSalesMan(string Filter, string strBranchCode)
        {
            List<SalesMans> SalesManList = new List<SalesMans>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            sSQL = "Select Sales_Man_Code , Sales_Man_Name,Designation,Branch_name,Start_Date,Sales_Man_Master.End_Date ,Sales_Man_Master.status,Sales_Man_Master.Branch_Code " +
                " from Sales_Man_Master Inner Join Branch_Master on Sales_Man_Master.Branch_Code = Branch_Master.Branch_Code AND Sales_Man_Master.Branch_Code = '" + strBranchCode + "'";
            if (Filter != "ALL")
                sSQL = sSQL + " Where Sales_Man_Code ='" + Filter + "' and Sales_Man_Name is not Null";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesManList.Add(new SalesMans(reader["Sales_Man_Code"].ToString(), reader["Sales_Man_Name"].ToString(), reader["Designation"].ToString(), reader["Branch_name"].ToString(), reader["Start_Date"].ToString(), reader["End_Date"].ToString(), reader["Status"].ToString(), reader["Branch_code"].ToString()));
                }
            }
            return SalesManList;
        }

        public List<SalesMans> GetAllSalesMan(string strBranchCode)
        {
            List<SalesMans> SalesManList = new List<SalesMans>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = string.Empty;
                sSQL = "Select Sales_Man_Code , Sales_Man_Name ,Designation,Branch_name,Start_Date,Sales_Man_Master.End_Date ,Sales_Man_Master.status,Sales_Man_Master.Branch_Code " +
                " from Sales_Man_Master Inner Join Branch_Master on Sales_Man_Master.Branch_Code = Branch_Master.Branch_Code Where Sales_Man_Name is Not Null AND Sales_Man_Master.Branch_Code = '" + strBranchCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesManList.Add(new SalesMans(reader["Sales_Man_Code"].ToString(), reader["Sales_Man_Name"].ToString(), reader["Designation"].ToString(), reader["Branch_name"].ToString(), reader["Start_Date"].ToString(), reader["End_Date"].ToString(), reader["Status"].ToString(), reader["Branch_code"].ToString()));
                }
            }
            return SalesManList;
        }
    }

    public class SalesMans
    {
        public SalesMans(string SalesManCode, string SalesManName, string Designation, string Branch, string StartDate, string EndDate, string Status, string BrCode)
        {
            _SalesManCode = SalesManCode;
            _SalesManName = SalesManName;
            _Designation = Designation;
            _Branch = Branch;
            _StartDate = StartDate;
            _EndDate = EndDate;
            _Status = Status;
            _BrCode = BrCode;
        }
        public SalesMans()
        {

        }
        
        private string _SalesManCode;
        private string _SalesManName;
        private string _Designation;
        private string _Branch;
        private string _StartDate;
        private string _EndDate;
        private string _Status;
        private string _BrCode;

        public string SalesManCode
        {
            get { return _SalesManCode; }
            set { _SalesManCode = value; }
        }
        public string SalesManName
        {
            get { return _SalesManName; }
            set { 
                _SalesManName = value; 
            }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }
        public string BrCode
        {
            get { return _BrCode; }
            set { _BrCode = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
