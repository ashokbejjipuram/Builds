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
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters;
using IMPALLibrary;
namespace IMPALLibrary
{
    public class ReceivableInvoice
    {
        public class DateItem
        {
            public string DateItemCode { get; set; }
            public string DateItemDesc { get; set; }
        }

        public List<AccountingPeriod> GetAccountingPeriod()
        {
            List<AccountingPeriod> lstAccPeriod = new List<AccountingPeriod>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT DISTINCT ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD WHERE" +
                         " ACCOUNTING_PERIOD_CODE > -1 ORDER BY ACCOUNTING_PERIOD_CODE DESC";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstAccPeriod.Add(new AccountingPeriod(reader[0].ToString(), reader[1].ToString()));
            }
            return lstAccPeriod;
        }

        public int GetPreviousAccountingPeriodStatus(string UserID, string ScreenName)
        {
            int AccPeriodStatus = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select CHARINDEX('" + ScreenName + "', PrevFinYearStatus) from Users where UserId='" + UserID + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            AccPeriodStatus = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd1).ToString());

            return AccPeriodStatus;
        }

        public DateItem GetPreviousDateStatus(string UserID, string ScreenName)
        {
            DateItem objItem = new DateItem();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "select CHARINDEX('" + ScreenName + "', PrevDateStatus), Convert(varchar(10),PrevDate,103) from Users where UserId='" + UserID + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objItem.DateItemCode = reader[0].ToString();
                    objItem.DateItemDesc = reader[1].ToString();
                }
            }

            return objItem;
        }

        public String GetDocumentDate(string AcountingPeriodCode)
        {
            string DocumentDate = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select convert(nvarchar,End_Date,103) from Accounting_Period where Accounting_Period_Code ='" + AcountingPeriodCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DocumentDate = ImpalDB.ExecuteScalar(cmd).ToString();
            return DocumentDate;
        }

        public Customer GetCustomerInfoByCustomerCode(string strCustomerCode, string strBranchCode)
        {            
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Customer_Code,Customer_Name,Location,address1,address2,address3,address4,Local_Sales_Tax_Number [GSTIN],case when isnull(Cash_Discount_Collection_Days,0) = 0 then 'N' else 'S' end Status FROM Customer_Master WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "' and Customer_Code = '" + strCustomerCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                    objCustomer.Customer_Name = reader["Customer_Name"].ToString();
                    objCustomer.Location = reader["Location"].ToString();
                    objCustomer.address1 = reader["address1"].ToString();
                    objCustomer.address2 = reader["address2"].ToString();
                    objCustomer.address3 = reader["address3"].ToString();
                    objCustomer.address4 = reader["address4"].ToString();
                    objCustomer.GSTIN = reader["GSTIN"].ToString();
                    objCustomer.Status = reader["Status"].ToString();
                }
            }

            return objCustomer;
        }

        public Customer GetCustomerInfoChqDishonorByCustomerCode(string strCustomerCode, string strBranchCode)
        {
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustDetails_WithChqDishonorStatus");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                    objCustomer.Customer_Name = reader["Customer_Name"].ToString();
                    objCustomer.Location = reader["Location"].ToString();
                    objCustomer.address1 = reader["address1"].ToString();
                    objCustomer.address2 = reader["address2"].ToString();
                    objCustomer.address3 = reader["address3"].ToString();
                    objCustomer.address4 = reader["address4"].ToString();
                    objCustomer.GSTIN = reader["GSTIN"].ToString();
                    objCustomer.Status = reader["Status"].ToString();
                    objCustomer.ChqDishonorCnt = Convert.ToInt32(reader["ChqDishonorCnt"].ToString());
                    objCustomer.ChqDishonorInd = Convert.ToInt32(reader["ChqDishonorInd"].ToString());
                }
            }

            return objCustomer;
        }

        public CustomerOutstandingDetails GetCustomerOutstandingReceipts(string BranchCode, string FromCustCode)
        {
            CustomerOutstandingDetails CustOsDetails = null;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calcos_Receipts");
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
                    CustOsDetails.Above180MonthName = reader[17].ToString();
                    CustOsDetails.Above90MonthName = reader[18].ToString();
                    CustOsDetails.Above60MonthName = reader[19].ToString();
                    CustOsDetails.Above30MonthName = reader[20].ToString();
                    CustOsDetails.CurBalMonthName = reader[21].ToString();
                    CustOsDetails.ChqDishonorCnt = Convert.ToInt32(reader[22].ToString());
                    CustOsDetails.ChqDishonorInd = Convert.ToInt32(reader[23].ToString());
                }
            }

            return CustOsDetails;
        }

        public Customer GetCustomerInfoByCustomerCodeOthers(string strCustomerCode, string strBranchCode)
        {
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Customer_Code,Customer_Name,Location,address1,address2,address3,address4,Local_Sales_Tax_Number [GSTIN],case when isnull(Cash_Discount_Collection_Days,0) = 0 then 'N' else 'S' end Status FROM Customer_Master WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "' and Customer_Code = '" + strCustomerCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                    objCustomer.Customer_Name = reader["Customer_Name"].ToString();
                    objCustomer.Location = reader["Location"].ToString();
                    objCustomer.address1 = reader["address1"].ToString();
                    objCustomer.address2 = reader["address2"].ToString();
                    objCustomer.address3 = reader["address3"].ToString();
                    objCustomer.address4 = reader["address4"].ToString();
                    objCustomer.GSTIN = reader["GSTIN"].ToString();
                    objCustomer.Status = reader["Status"].ToString();
                }
            }

            return objCustomer;
        }

        public int GetInterStateStatusGST(string ToBranch, string FromBranch)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select COUNT(distinct State_Code) From Branch_Master where Branch_Code in ('" + ToBranch + "','" + FromBranch + "')";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }

        public int GetCustGSTValueStatus(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetGST_Value_Status_DrCr_Cust");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }


        public int GetSuppGSTValueStatus(string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetGST_Value_Status_DrCr_Supp");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int result = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));

            return result;
        }

        public List<SLB> GetTODSLBDetails(string strBranchCode, string strCustomerCode, string strSupplierCode, string strSLBType)
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct TOD_Slb_Value [SLB_Code],TOD_Slb_Value [SLB_Description] from TOD_Target_Master where Branch_Code='" + strBranchCode + "' and Supplier_Name='" + strSupplierCode + "' and TOD_Indicator='" + strSLBType + "' and TOD_Slb_Percentage > 0"; // and Customer_Code='" + strCustomerCode + "'
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(TwoDecimalConversion(reader["SLB_Code"].ToString()), TwoDecimalConversion(reader["SLB_Description"].ToString())));
                }
            }

            return SlbLst;
        }

        public List<Item_wise_sale> GetTODMonthYearGOGO(string strBranchCode)
        {
            List<Item_wise_sale> lstMonthYear = new List<Item_wise_sale>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetTODmonthYearGOGO");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
            {
                while (reader.Read())
                {
                    lstMonthYear.Add(new Item_wise_sale(reader[0].ToString()));
                }
            }

            return lstMonthYear;
        }

        public List<SLB> GetTODSLBPercentage(string strBranchCode, string strCustomerCode, string strSupplierCode, string strSupplierName, string strSLBType, string strSLBValue)
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "select distinct TOD_Slb_Percentage [SLB_Code],TOD_Slb_Percentage [SLB_Description] from TOD_Target_Master where Branch_Code='" + strBranchCode + "' and Supplier_Name='" + strSupplierCode + "' and TOD_Indicator='" + strSLBType + "' and TOD_Slb_Value=" + strSLBValue; // and Customer_Code='" + strCustomerCode + "'

            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetTODSLBPercentage");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@Supplier_Code", DbType.String, strSupplierCode);
            ImpalDB.AddInParameter(dbcmd, "@Supplier_Name", DbType.String, strSupplierName);
            ImpalDB.AddInParameter(dbcmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(dbcmd, "@TOD_Indicator", DbType.String, strSLBType);
            ImpalDB.AddInParameter(dbcmd, "@TOD_Slb_Value", DbType.String, strSLBValue);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(TwoDecimalConversion(reader["SLB_Code"].ToString()), TwoDecimalConversion(reader["SLB_Description"].ToString())));
                }
            }

            return SlbLst;
        }

        public List<SLB> GetTODSLBPercentageGOGO(string strSupplierCode, string strBranchCode, string strCustomerCode, string strMarketLocation, string strSLBType, string strSLBValue, string MonthYear, string CDindicator)
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetTODSLBPercentage_GOGO");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@Supplier_Code", DbType.String, strSupplierCode);
            ImpalDB.AddInParameter(dbcmd, "@Customer_Code", DbType.String, strCustomerCode);
            ImpalDB.AddInParameter(dbcmd, "@Monthly_Yearly", DbType.String, strSLBType);
            ImpalDB.AddInParameter(dbcmd, "@Local_Outstation", DbType.String, strSLBValue);
            ImpalDB.AddInParameter(dbcmd, "@Market_Location", DbType.String, strMarketLocation);
            ImpalDB.AddInParameter(dbcmd, "@MonthYear", DbType.String, MonthYear);
            ImpalDB.AddInParameter(dbcmd, "@CD_Indicator", DbType.String, CDindicator);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(TwoDecimalConversion(reader["TOD_Percentage"].ToString()), TwoDecimalConversion(reader["TOD_Percentage"].ToString())));
                }
            }

            return SlbLst;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}
