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

namespace IMPALLibrary.Masters.Sales
{
    public class Salesbranches
    {
        public List<Salesbranch> Getsalesbranches(string strBranchCode)
        {
            List<Salesbranch> lstmonthyear = new List<Salesbranch>();
            Database ImpalDB = DataAccess.GetDatabase();
            string Ssql = "select distinct Month_Year, SUBSTRING(MONTH_YEAR,1,2) AS MONTHS, SUBSTRING(MONTH_YEAR,3,4) AS YEARS  from v_salesbranch where Branch_Code = '" + strBranchCode + "' ORDER BY YEARS DESC, MONTHS DESC";
           
            lstmonthyear.Add(new Salesbranch(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstmonthyear.Add(new Salesbranch(reader[0].ToString()));
                }
            }
            return lstmonthyear;
        }

        #region RankBranch
        public int RankBranch(string BranchCode, int PeriodCode)
        {
            int intInsert = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_ADDBRANCH_RANKING1_DESC");
            ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@accounting_period_code", DbType.Int16, PeriodCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            intInsert = ImpalDB.ExecuteNonQuery(cmd);
            return intInsert;
        }
        #endregion

        #region GetCustomerOutstanding
        public CustomerOutstandingDetails GetCustomerOutstanding(string BranchCode, string FromCustCode)
        {
            CustomerOutstandingDetails CustOsDetails = null;
            
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calcos_cust");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, FromCustCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CustOsDetails = new CustomerOutstandingDetails();
                    CustOsDetails.Status = reader[0].ToString();
                    CustOsDetails.Address1 = reader[1].ToString();
                    CustOsDetails.Address2 = reader[2].ToString();
                    CustOsDetails.Address3 = reader[3].ToString();
                    CustOsDetails.Address4 = reader[4].ToString();
                    CustOsDetails.Location = reader[5].ToString();
                    CustOsDetails.GSTINNo = reader[6].ToString();
                    CustOsDetails.PinCode = reader[7].ToString();
                    CustOsDetails.Phone = reader[8].ToString();
                    CustOsDetails.Credit_Limit = reader[9].ToString();
                    CustOsDetails.Outstanding = reader[10].ToString();
                    CustOsDetails.Above180 = reader[11].ToString();
                    CustOsDetails.Above90 = reader[12].ToString();
                    CustOsDetails.Above60 = reader[13].ToString();
                    CustOsDetails.Above30 = reader[14].ToString();
                    CustOsDetails.CurBal = reader[15].ToString();
                    CustOsDetails.Cr_Bal = reader[16].ToString();
                    CustOsDetails.Message = reader[17].ToString();
                }
            }

            return CustOsDetails;
        }

        public CustomerOutstandingDetails GetCustomerOutstandingHO(string BranchCode, string FromCustCode)
        {
            CustomerOutstandingDetails CustOsDetails = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calcos_cust_HO");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, FromCustCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CustOsDetails = new CustomerOutstandingDetails();
                    CustOsDetails.Status = reader[0].ToString();
                    CustOsDetails.Address1 = reader[1].ToString();
                    CustOsDetails.Address2 = reader[2].ToString();
                    CustOsDetails.Address3 = reader[3].ToString();
                    CustOsDetails.Address4 = reader[4].ToString();
                    CustOsDetails.Location = reader[5].ToString();
                    CustOsDetails.GSTINNo = reader[6].ToString();
                    CustOsDetails.PinCode = reader[7].ToString();
                    CustOsDetails.Phone = reader[8].ToString();
                    CustOsDetails.Credit_Limit = reader[9].ToString();
                    CustOsDetails.Outstanding = reader[10].ToString();
                    CustOsDetails.Above180 = reader[11].ToString();
                    CustOsDetails.Above90 = reader[12].ToString();
                    CustOsDetails.Above60 = reader[13].ToString();
                    CustOsDetails.Above30 = reader[14].ToString();
                    CustOsDetails.CurBal = reader[15].ToString();
                    CustOsDetails.Cr_Bal = reader[16].ToString();
                    CustOsDetails.Message = reader[17].ToString();
                }
            }

            return CustOsDetails;
        }

        public CustomerOutstandingDetails GetCustomerDetails(string BranchCode, string FromCustCode)
        {
            CustomerOutstandingDetails CustOsDetails = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustomerDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, FromCustCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CustOsDetails = new CustomerOutstandingDetails();
                    CustOsDetails.Status = reader[0].ToString();
                    CustOsDetails.Address1 = reader[1].ToString();
                    CustOsDetails.Address2 = reader[2].ToString();
                    CustOsDetails.Address3 = reader[3].ToString();
                    CustOsDetails.Address4 = reader[4].ToString();
                    CustOsDetails.Location = reader[5].ToString();
                    CustOsDetails.GSTINNo = reader[6].ToString();
                    CustOsDetails.PinCode = reader[7].ToString();
                    CustOsDetails.Phone = reader[8].ToString();
                    CustOsDetails.Credit_Limit = reader[9].ToString();
                }
            }

            return CustOsDetails;
        }
        #endregion

        #region UpdateTODSales
        public void UpdateTODSales(string CustomerCode, string FromDate, string ToDate, string PlantCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_SFLTOD_NEW");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@customer_code", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@from_date", DbType.String, FromDate);
            ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, ToDate);
            ImpalDB.AddInParameter(cmd, "@plant_code", DbType.String, PlantCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        #endregion

        #region UpdateTODSales
        public void UpdateTODSocket(string CustomerCode, string FromDate, string ToDate, string PlantCode, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_sfltod_Socket");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@customer_code", DbType.String, CustomerCode);
            ImpalDB.AddInParameter(cmd, "@from_date", DbType.String, FromDate);
            ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, ToDate);
            ImpalDB.AddInParameter(cmd, "@plant_code", DbType.String, PlantCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        #endregion

        public DataSet GetSFLTODSocketDetails(string strBranchCode,string strFromDate,string strToDate)
        {
            Database ImpalDB = DataAccess.GetDatabaseBackUp();
            DataSet ds = new DataSet();
            string SQLQuery = string.Empty;

            SQLQuery = "select b.Branch_Name,s.Customer_Code,c.Customer_Name,'" + strFromDate + "' From_Date,'" + strToDate + "' To_Date,s.Supplier_Part_Number,";
            SQLQuery = SQLQuery + "s.Quantity,s.List_Price,s.Sale_Value,(isnull(s.Quantity,0)*isnull(s.Sale_Value,0)) Total_Value ";
            SQLQuery = SQLQuery + "from sfltod s WITH (NOLOCK) inner join branch_master b WITH (NOLOCK) on  b.Branch_Code = '" + strBranchCode + "' and ";
            SQLQuery = SQLQuery + "b.Branch_Code=s.Branch_Code inner join Customer_Master c WITH (NOLOCK) ";
            SQLQuery = SQLQuery + "on b.Branch_Code=c.Branch_Code and c.Customer_Code=s.Customer_Code";

            DbCommand cmd = ImpalDB.GetSqlStringCommand(SQLQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }
    }

    public class Salesbranch
    {
        private string _month_year;

        public Salesbranch(string month_year)
        {
            _month_year = month_year;
        }
        public Salesbranch()
        { }
        public string accounting_period_code
        {
            get { return _month_year; }
            set { _month_year = value; }
        }
    }

    public class CustomerOutstandingDetails
    {
        public string Status { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Location { get; set; }
        public string GSTINNo { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }

        public string Customer_Code { get; set; }
        public string Credit_Limit { get; set; }
        public string Outstanding { get; set; }
        public string Above180 { get; set; }
        public string Above150 { get; set; }
        public string Above120 { get; set; }
        public string Above90 { get; set; }
        public string Above60 { get; set; }
        public string Above30 { get; set; }
        public string CurBal { get; set; }
        public string Cr_Bal { get; set; }
        public string Above180MonthName { get; set; }
        public string Above150MonthName { get; set; }
        public string Above120MonthName { get; set; }
        public string Above90MonthName { get; set; }
        public string Above60MonthName { get; set; }
        public string Above30MonthName { get; set; }
        public string CurBalMonthName { get; set; }
        public int ChqDishonorCnt { get; set; }
        public int ChqDishonorInd { get; set; }
        public string Message { get; set; }
    }
}