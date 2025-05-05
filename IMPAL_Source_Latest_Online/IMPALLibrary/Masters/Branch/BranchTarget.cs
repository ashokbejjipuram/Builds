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
    public class BranchTargets
    {
        public List<BranchTarget> GetAllBranchTarget()
        {
            List<BranchTarget> lstBrTarget = new List<BranchTarget>();

            Database ImpalDB = DataAccess.GetDatabase();

            //string sSQL = "Select a.Branch_Code,b.Branch_Name,a.Supplier_Line_Code,c.Supplier_Name,a.Year,a.target_amount,a.actual_amount " +
            //        " from Branch_Target a Inner join Branch_Master b on a.Branch_Code = b.Branch_Code " +
            //        " inner join Supplier_Master c on substring(a.Supplier_Line_Code,1,3) = c.Supplier_Code ";

            string sSQL = "  Select a.Supplier_Line_Code,b.Branch_Name,a.Year,a.target_amount,a.actual_amount" +
                       " from Branch_Target a Inner join Branch_Master b on a.Branch_Code = b.Branch_Code" +
                      " inner join Supplier_Master c on substring(a.Supplier_Line_Code,1,3) = c.Supplier_Code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstBrTarget.Add(new BranchTarget(reader[0].ToString(), reader[1].ToString(),
                            reader[2].ToString(),reader[3].ToString(), reader[4].ToString()));
                }
            }
            return lstBrTarget;
        }

        public List<BranchTarget> GetAllLine()
        {
            List<BranchTarget> lstLineTarget = new List<BranchTarget>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select distinct Supplier_Line_Code from Supplier_Line_Master order by Supplier_Line_Code";
            lstLineTarget.Insert(0, new BranchTarget(string.Empty));
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstLineTarget.Add(new BranchTarget(reader[0].ToString()));
                }
            }
            return lstLineTarget;
        }
        public void AddNewBranchTargets(string SupplierCode,string BranchName, string Year, string TargetAmount)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranchtarget");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchName.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Year", DbType.String, Year.Trim());
            ImpalDB.AddInParameter(cmd, "@Target_Amount", DbType.String, TargetAmount.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateBranchTarget(string SupplierCode, string BranchCode, string Year, string TargetAmount)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updbranchtarget");
            //ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Substring(0, 3).Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Year", DbType.String, Year.Trim());
            ImpalDB.AddInParameter(cmd, "@Target_Amount", DbType.String, TargetAmount.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }

    public class BranchTarget
    {
        public BranchTarget(string SupplierCode, string BranchName, string Year, string TargetAmount, string ActualAmount)
        {
            _SupplierCode = SupplierCode;
            _BranchName = BranchName;
            _Year = Year;
            _TargetAmount = TargetAmount;
            _ActualAmount = ActualAmount;
        }

       
        public BranchTarget()
        { }
        public BranchTarget(string SupplierCode)
        { 
            _SupplierCode = SupplierCode; 
        }
                private string _SupplierCode;
                private string _SupplierName;
                private string _BranchCode;
                private string _BranchName;
                private string _Year;
                private string _TargetAmount;
                private string _ActualAmount;
        
                public string SupplierCode
                {
                    get { return _SupplierCode; }
                    set { _SupplierCode = value; }
                }
                public string SupplierName
                {
                    get { return _SupplierName; }
                    set { _SupplierName = value; }
                }
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
                public string Year
                {
                    get { return _Year; }
                    set { _Year = value; }
                }
                public string TargetAmount
                {
                    get { return _TargetAmount; }
                    set { _TargetAmount = value; }
                }
                public string ActualAmount
                {
                    get { return _ActualAmount; }
                    set { _ActualAmount = value; }
                }
        
    }
}