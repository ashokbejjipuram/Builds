using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALLibrary.Masters.CustomerDetails
{
    public class CustomerDetails
    {
        public List<CustomerDtls> GetCustomers(string BranchCode, string QueryType)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            switch (QueryType)
            {
                case "SalesOrder":
                    sQuery = "SELECT DISTINCT S.CUSTOMER_CODE, C.CUSTOMER_NAME, C.STATUS " +
                        "FROM SALES_ORDER_HEADER SALES S WITH (NOLOCK) INNER JOIN CUSTOMER_MASTER C WITH (NOLOCK) " +
                        "ON C.BRANCH_CODE = '" + BranchCode + "' AND S.BRANCH_CODE=C.BRANCH_CODE AND S.CUSTOMER_CODE = C.CUSTOMER_CODE " +
                        "ORDER BY C.CUSTOMER_NAME";
                    break;
                case "Corporate":
                    sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) ORDER BY CUSTOMER_NAME";
                    break;
                case "StatementOfAccount":
                    sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                                 + BranchCode + "' and status='A' ORDER BY CUSTOMER_CODE";
                    break;
                default:
                    sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                                 + BranchCode + "' ORDER BY CUSTOMER_NAME"; // AND CUSTOMER_NAME LIKE '" + AutoCompleteName + "%'
                    break;
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                lstCustomer.Add(new CustomerDtls("0", string.Empty));
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstCustomer;
            }
        }

        public List<CustomerDtls> GetCustomersByTown(string BranchCode, int TownCode, string QueryType)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            if (TownCode != 0)
                sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                     + BranchCode + "' and Town_Code=" + TownCode + " ORDER BY CUSTOMER_NAME";
            else
                sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                     + BranchCode + "' ORDER BY CUSTOMER_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                lstCustomer.Add(new CustomerDtls("0", string.Empty));
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstCustomer;
            }
        }

        public List<CustomerDtls> GetCustomerDetails(string BranchCode, string AutoCompleteName)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            //if (string.IsNullOrEmpty(BranchCode))//For Corporate User
            //    sQuery = "SELECT DISTINCT TOWN_CODE, TOWN_NAME FROM TOWN_MASTER ORDER BY TOWN_NAME";
            //else
            if (AutoCompleteName != "Dummy")
                sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                             + BranchCode + "' AND CUSTOMER_NAME LIKE '" + AutoCompleteName + "%' ORDER BY CUSTOMER_NAME";
            else
                sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME, STATUS FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '"
                         + BranchCode + "' ORDER BY CUSTOMER_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstCustomer;
            }
        }

        public CustomerDtls GetDetails(string BranchCode, string CustomerCode)
        {
            CustomerDtls oCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "Select Status, Address1, Address2, Address3, Address4, Location, Phone From Customer_Master WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Customer_Code = '" + CustomerCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (oCustomer == null)
                    oCustomer = new CustomerDtls();
                while (reader.Read())
                {
                    oCustomer.Status = reader[0].ToString();
                    oCustomer.Address1 = reader[1].ToString();
                    oCustomer.Address2 = reader[2].ToString();
                    oCustomer.Address3 = reader[3].ToString();
                    oCustomer.Address4 = reader[4].ToString();
                    oCustomer.Location = reader[5].ToString();
                    oCustomer.Phone = reader[6].ToString();
                }

                return oCustomer;
            }
        }

        public List<CustomerDtls> GetCustomers()
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME FROM CUSTOMER_MASTER WITH (NOLOCK) ORDER BY CUSTOMER_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString()));
                return lstCustomer;
            }
        }

        public string GetCustomersIndHex(string BranchCode, string CustomerCode)
        {
            string Cnt = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            sQuery = "SELECT isnull(Convert(varchar,(SELECT isnull(Indicator,0) From Indl_Hex_Customer_Master WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Customer_Code = '" + CustomerCode + "' GROUP BY Indicator), 0),0)";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Cnt = ImpalDB.ExecuteScalar(cmd).ToString();

            return Cnt;
        }

        public List<CustomerDtls> GetCustomerswithLocation(string strBranchCode)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION AS 'CUSTOMER_NAME' FROM CUSTOMER_MASTER WITH (NOLOCK) Where Branch_Code='" + strBranchCode + "' ORDER BY CUSTOMER_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString()));
                return lstCustomer;
            }
        }

        public List<CustomerDtls> GetCustomerswithLocationGrpDlrs(string strBranchCode, string strCustCode)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT CUSTOMER_CODE, CUSTOMER_NAME + ' | ' + LOCATION AS 'CUSTOMER_NAME' FROM CUSTOMER_MASTER WITH (NOLOCK) Where Branch_Code='" + strBranchCode + "' AND CUSTOMER_CODE<>'" + strCustCode + "' ORDER BY CUSTOMER_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString()));
                return lstCustomer;
            }
        }

        public List<CustomerDtls> GetDistrictMaster(string strBranchCode)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "Select District_Code, District_Name From District_Master d WITH (NOLOCK) inner join Branch_Master b WITH (NOLOCK) on b.Branch_Code='" + strBranchCode + "' and b.State_Code=d.State_Code Order by District_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstCustomer == null)
                    lstCustomer = new List<CustomerDtls>();
                while (reader.Read())
                    lstCustomer.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString()));
                return lstCustomer;
            }
        }

        public List<CustomerFields> GetSalesman(string branchCode)
        {

            List<CustomerFields> lstSalesman = new List<CustomerFields>();
            CustomerFields objSalesMan = new CustomerFields();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT SALES_MAN_CODE,SALES_MAN_NAME  FROM SALES_MAN_MASTER WHERE SALES_MAN_NAME IS NOT NULL AND STATUS = 'A' AND BRANCH_CODE = '" + branchCode + "' ORDER BY SALES_MAN_NAME  ";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objSalesMan = new CustomerFields();
                    objSalesMan.Salesman_Code = reader[0].ToString();
                    objSalesMan.Salesman = reader[1].ToString();
                    lstSalesman.Add(objSalesMan);
                }
                return lstSalesman;
            }
        }

        public List<CustomerFields> GetSalesmanEcredit(string branchCode)
        {

            List<CustomerFields> lstSalesman = new List<CustomerFields>();
            CustomerFields objSalesMan = new CustomerFields();

            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT SALES_MAN_CODE,SALES_MAN_NAME + '-' + SALES_MAN_CODE SALES_MAN_NAME FROM SALES_MAN_MASTER WHERE SALES_MAN_NAME IS NOT NULL AND STATUS = 'A' AND BRANCH_CODE = '" + branchCode + "' ORDER BY SALES_MAN_NAME  ";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objSalesMan = new CustomerFields();
                    objSalesMan.Salesman_Code = reader[0].ToString();
                    objSalesMan.Salesman = reader[1].ToString();
                    lstSalesman.Add(objSalesMan);
                }
                return lstSalesman;
            }
        }

        public List<CustomerDtls> GetSalesMan(string BranchCode)
        {
            List<CustomerDtls> lstSalesMan = new List<CustomerDtls>();
            lstSalesMan.Add(new CustomerDtls("0", string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT SALES_MAN_CODE,SALES_MAN_NAME  FROM SALES_MAN_MASTER WITH (NOLOCK) WHERE SALES_MAN_NAME IS NOT NULL AND STATUS = 'A' AND BRANCH_CODE = '" + BranchCode + "' ORDER BY SALES_MAN_NAME";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                    lstSalesMan.Add(new CustomerDtls(reader[0].ToString(), reader[1].ToString()));
            }
            return lstSalesMan;
        }

        public List<CustomerType> GetAllCustomerType()
        {
            List<CustomerType> CustomerTypeList = new List<CustomerType>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = string.Empty;
            sSQL = "SELECT Party_Type_Code, Party_Type_Description FROM Party_Type_Master WITH (NOLOCK) where (Party_Type_Code is not null and Party_Type_Code <>'') and Debit_Chart_Of_Account='1' and Credit_Chart_Of_Account='1' order by party_type_code";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {

                    CustomerTypeList.Add(new CustomerType(reader["Party_Type_Code"].ToString(), reader["Party_Type_Description"].ToString()));
                }
            }

            return CustomerTypeList;
        }

        public string AddNewCustomer(CustomerFields CustDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string customerCode = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddCustomer");
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, CustDetails.Customer_Name);
                    ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, CustDetails.Chart_of_Account_Code);
                    ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, CustDetails.Party_Type_Code);
                    ImpalDB.AddInParameter(cmd, "@Customer_Classification", DbType.String, CustDetails.Customer_Classification);
                    ImpalDB.AddInParameter(cmd, "@Customer_Segment", DbType.String, CustDetails.Customer_Segment);
                    ImpalDB.AddInParameter(cmd, "@Address1", DbType.String, CustDetails.Address1);
                    ImpalDB.AddInParameter(cmd, "@Address2", DbType.String, CustDetails.Address2);
                    ImpalDB.AddInParameter(cmd, "@Address3", DbType.String, CustDetails.Address3);
                    ImpalDB.AddInParameter(cmd, "@Address4", DbType.String, CustDetails.Address4);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, CustDetails.location);
                    ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, CustDetails.Pincode);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, CustDetails.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, CustDetails.Town_Code);
                    ImpalDB.AddInParameter(cmd, "@Alpha_Code", DbType.String, CustDetails.Alpha_Code);
                    ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, CustDetails.Phone);
                    ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, CustDetails.Local_Outstation);
                    ImpalDB.AddInParameter(cmd, "@Email", DbType.String, CustDetails.Email);
                    ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, CustDetails.Telex);
                    ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, CustDetails.Contact_Person);
                    ImpalDB.AddInParameter(cmd, "@Contact_Designation", DbType.String, CustDetails.Contact_Person_Mobile);
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, CustDetails.Carrier);
                    ImpalDB.AddInParameter(cmd, "@Destination", DbType.String, CustDetails.Destination);
                    ImpalDB.AddInParameter(cmd, "@Local_Sales_Tax_Number", DbType.String, CustDetails.Local_Sales_Tax_Number);
                    ImpalDB.AddInParameter(cmd, "@Central_Sales_Tax_Number", DbType.String, CustDetails.Central_Sales_Tax_Number);
                    ImpalDB.AddInParameter(cmd, "@Outstanding_Amount", DbType.Double, CustDetails.Outstanding_Amount);
                    ImpalDB.AddInParameter(cmd, "@Credit_Limit", DbType.Double, CustDetails.Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Cash_Credit_Limit_Indicator", DbType.String, CustDetails.Cash_Credit_Limit_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Cash_Discount_Collection_Days", DbType.Int16, null);
                    ImpalDB.AddInParameter(cmd, "@customer_bank_name", DbType.String, CustDetails.customer_bank_name);
                    ImpalDB.AddInParameter(cmd, "@customer_bank_branch", DbType.String, CustDetails.customer_bank_branch);
                    ImpalDB.AddInParameter(cmd, "@IFSC_Code", DbType.String, CustDetails.IFSC_Code);
                    ImpalDB.AddInParameter(cmd, "@status", DbType.String, CustDetails.status);
                    ImpalDB.AddInParameter(cmd, "@tinnumber", DbType.String, CustDetails.TinNumber);
                    ImpalDB.AddInParameter(cmd, "@BranchStateCode", DbType.String, CustDetails.BranchState_Code);
                    ImpalDB.AddInParameter(cmd, "@CustomerStateCode", DbType.String, CustDetails.CustomerState_Code);
                    //ImpalDB.ExecuteNonQuery(cmd);

                    DataSet ds = new DataSet();
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        customerCode = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (customerCode != "")
                    {
                        DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addCustomerSalesman");
                        ImpalDB.AddInParameter(cmd1, "@Customer_Code", DbType.String, customerCode);
                        ImpalDB.AddInParameter(cmd1, "@Customer_Name", DbType.String, CustDetails.Customer_Name);
                        ImpalDB.AddInParameter(cmd1, "@Town_Code", DbType.Int16, CustDetails.Town_Code);
                        ImpalDB.AddInParameter(cmd1, "@Sales_Man_Code", DbType.String, CustDetails.Salesman_Code);
                        ImpalDB.AddInParameter(cmd1, "@Sales_Man_Name", DbType.String, CustDetails.Salesman);
                        ImpalDB.AddInParameter(cmd1, "@Branch_code", DbType.String, CustDetails.Branch_Code);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd1);
                    }

                    if (customerCode != "")
                    {
                        DbCommand cmdslb = ImpalDB.GetStoredProcCommand("Usp_AddCustomerSLB");
                        ImpalDB.AddInParameter(cmdslb, "@CustomerCode", DbType.String, customerCode);
                        cmdslb.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmdslb);

                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerCode;
        }

        public void UpdateCustomer(CustomerFields CustDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    // Create command to execute the stored procedure and add the parameters.
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdCustomer");
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustDetails.Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, CustDetails.Customer_Name);
                    ImpalDB.AddInParameter(cmd, "@Customer_Classification", DbType.String, CustDetails.Customer_Classification);
                    ImpalDB.AddInParameter(cmd, "@Customer_Segment", DbType.String, CustDetails.Customer_Segment);
                    ImpalDB.AddInParameter(cmd, "@Address1", DbType.String, CustDetails.Address1);
                    ImpalDB.AddInParameter(cmd, "@Address2", DbType.String, CustDetails.Address2);
                    ImpalDB.AddInParameter(cmd, "@Address3", DbType.String, CustDetails.Address3);
                    ImpalDB.AddInParameter(cmd, "@Address4", DbType.String, CustDetails.Address4);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, CustDetails.location);
                    ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, CustDetails.Pincode);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, CustDetails.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, CustDetails.Town_Code);
                    //ImpalDB.AddInParameter(cmd, "@Alpha_Code", DbType.String, CustDetails.Alpha_Code);
                    ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, CustDetails.Phone);
                    ImpalDB.AddInParameter(cmd, "@Local_Outstation", DbType.String, CustDetails.Local_Outstation);
                    ImpalDB.AddInParameter(cmd, "@Email", DbType.String, CustDetails.Email);
                    ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, CustDetails.Telex);
                    ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, CustDetails.Contact_Person);
                    ImpalDB.AddInParameter(cmd, "@Contact_Designation", DbType.String, CustDetails.Contact_Person_Mobile);
                    ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, CustDetails.Carrier);
                    ImpalDB.AddInParameter(cmd, "@Destination", DbType.String, CustDetails.Destination);
                    ImpalDB.AddInParameter(cmd, "@Local_Sales_Tax_Number", DbType.String, CustDetails.Local_Sales_Tax_Number);
                    ImpalDB.AddInParameter(cmd, "@Central_Sales_Tax_Number", DbType.String, CustDetails.Central_Sales_Tax_Number);
                    //ImpalDB.AddInParameter(cmd, "@Outstanding_Amount", DbType.Double, CustDetails.Outstanding_Amount);
                    ImpalDB.AddInParameter(cmd, "@Credit_Limit", DbType.Double, CustDetails.Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Cash_Credit_Limit_Indicator", DbType.String, CustDetails.Cash_Credit_Limit_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Cash_Discount_Collection_Days", DbType.String, CustDetails.Collection_Days);
                    ImpalDB.AddInParameter(cmd, "@customer_bank_name", DbType.String, CustDetails.customer_bank_name);
                    ImpalDB.AddInParameter(cmd, "@customer_bank_branch", DbType.String, CustDetails.customer_bank_branch);
                    ImpalDB.AddInParameter(cmd, "@IFSC_Code", DbType.String, CustDetails.IFSC_Code);
                    ImpalDB.AddInParameter(cmd, "@status", DbType.String, CustDetails.status);
                    ImpalDB.AddInParameter(cmd, "@tinno", DbType.String, CustDetails.TinNumber);
                    ImpalDB.AddInParameter(cmd, "@BranchStateCode", DbType.String, CustDetails.BranchState_Code);
                    ImpalDB.AddInParameter(cmd, "@CustomerStateCode", DbType.String, CustDetails.CustomerState_Code);
                    ImpalDB.AddInParameter(cmd, "@Validity_Indicator", DbType.String, CustDetails.Validity_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Old_Credit_Limit", DbType.Double, CustDetails.Old_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Validity_Date", DbType.String, CustDetails.Credit_Limit_Due_Date);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_addCustomerSalesman");
                    ImpalDB.AddInParameter(cmd1, "@Customer_Code", DbType.String, CustDetails.Customer_Code);
                    ImpalDB.AddInParameter(cmd1, "@Customer_Name", DbType.String, CustDetails.Customer_Name);
                    ImpalDB.AddInParameter(cmd1, "@Town_Code", DbType.Int16, CustDetails.Town_Code);
                    ImpalDB.AddInParameter(cmd1, "@Sales_Man_Code", DbType.String, CustDetails.Salesman_Code);
                    ImpalDB.AddInParameter(cmd1, "@Sales_Man_Name", DbType.String, CustDetails.Salesman);
                    ImpalDB.AddInParameter(cmd1, "@Branch_code", DbType.String, CustDetails.Branch_Code);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCustomerHoAdmin(string strBranchCode, string strCustCode, string strOldCreditLimit, string strCreditLimit, string strValidityInd, string strValidityDate)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    // Create command to execute the stored procedure and add the parameters.
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdCustomer_HoAdmin");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustCode);
                    ImpalDB.AddInParameter(cmd, "@Credit_Limit", DbType.Double, strCreditLimit);                    
                    ImpalDB.AddInParameter(cmd, "@Old_Credit_Limit", DbType.Double, strOldCreditLimit);
                    ImpalDB.AddInParameter(cmd, "@Validity_Indicator", DbType.String, strValidityInd);
                    ImpalDB.AddInParameter(cmd, "@Validity_Date", DbType.String, strValidityDate);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCustomerTCSExemptionStatus(string strBranchCode, string strCustCode, string strCustName, string Location, string ExemptionInd)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdTCS_Exemption");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, strCustName);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, Location);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ExemptionInd);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCustomerStatus(string strBranchCode, string strCustCode, string strCustName, string Location, string Status)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdCustomer_Status");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, strCustName);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, Location);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, Status);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCustomerTCSExemptionStatus(string strBranchCode, string strCustCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string Status = string.Empty;

            string sQuery = "Select COUNT(1) From Customer_Master_TCS_Exemption WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and Customer_Code='" + strCustCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Status = ImpalDB.ExecuteScalar(cmd).ToString();

            if (Status == "0")
                Status = "N";
            else
                Status = "Y";

            return Status;
        }

        public string GetCustomerStatus(string strBranchCode, string strCustCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string Status = string.Empty;

            string sQuery = "Select ISNULL(Status,'') From Customer_Master WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and Customer_Code='" + strCustCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Status = ImpalDB.ExecuteScalar(cmd).ToString();

            return Status;
        }

        public string GetTempBillingLimitStatus(string strBranchCode, string strCustCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            string Status = string.Empty;

            string sQuery = "Select COUNT(1) From Customer_Master_Temp_Billing WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and Customer_Code='" + strCustCode + "' and Status='Y'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Status = ImpalDB.ExecuteScalar(cmd).ToString();

            if (Status == "0")
                Status = "N";
            else
                Status = "Y";

            return Status;
        }

        public List<CustomerFields> GetDealerNEFTDetails(string BankRefNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            List<CustomerFields> NEFTdetails = new List<CustomerFields>();

            string sQuery = "Select To_Branch_Code,convert(varchar(10),NEFT_Date,103) NEFT_Date, Amount, Remarks From Main_Cash_Details_HO_Branch WITH (NOLOCK) where Bank_Ref_No = '" + BankRefNo + "' and Receipt_Number is null and Status is null and Receipt_Amount is null and Receipt_Type is null";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                CustomerFields NEFT = new CustomerFields();

                while (objReader.Read())
                {
                    NEFT.Branch_Code = objReader[0].ToString();
                    NEFT.NEFT_Date = objReader[1].ToString();
                    NEFT.NEFT_Amount = objReader[2].ToString();
                    NEFT.NEFT_Remarks = objReader[3].ToString();

                    NEFTdetails.Add(NEFT);
                }
            }

            return NEFTdetails;
        }

        public void UpdateDealerNEFTDetails(string strBranchCode, string strBankRefNo, string strCustCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    // Create command to execute the stored procedure and add the parameters.
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdNEFT_Details");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@BankRef_No", DbType.String, strBankRefNo);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerFields> ViewCustomer(string BranchCode, string CustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustomer");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode.Trim());
            List<CustomerFields> custDetails = new List<CustomerFields>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    CustomerFields cust = new CustomerFields();
                    cust.Customer_Code = objReader["Customer_Code"].ToString();
                    cust.Customer_Name = objReader["Customer_Name"].ToString();
                    cust.Chart_of_Account_Code = objReader["Chart_of_Account_Code"].ToString();
                    cust.Party_Type_Code = objReader["Party_Type_Code"].ToString();
                    //cust.Classification_Segment = objReader["Classification_Segment"].ToString();
                    cust.Customer_Classification = objReader["Customer_Classification"].ToString();
                    cust.Customer_Segment = objReader["Customer_Segment"].ToString();
                    cust.Pincode = objReader["Pincode"].ToString();
                    cust.Branch_Code = objReader["Branch_Code"].ToString();
                    cust.Town_Code = objReader["Town_Code"].ToString();
                    cust.District_Code = objReader["District_Code"].ToString();
                    cust.Alpha_Code = objReader["Alpha_Code"].ToString();
                    cust.Phone = objReader["Phone"].ToString();
                    cust.Local_Outstation = objReader["Local_Outstation"].ToString();
                    cust.Email = objReader["Email"].ToString();
                    cust.Telex = objReader["Telex"].ToString();
                    cust.Proprietor_Name = objReader["Proprietor_Name"].ToString();
                    cust.Proprietor_Mobile = objReader["Proprietor_Mobile"].ToString();
                    cust.Contact_Person = objReader["Contact_Person"].ToString();
                    cust.Contact_Person_Mobile = objReader["Contact_Person_Mobile"].ToString();
                    cust.Carrier = objReader["Carrier"].ToString();
                    cust.Destination = objReader["Destination"].ToString();
                    cust.Firm_Type = objReader["Firm_Type"].ToString();
                    cust.Registration_Type = objReader["Registration_Type"].ToString();
                    cust.Local_Sales_Tax_Number = objReader["Local_Sales_Tax_Number"].ToString();
                    cust.Central_Sales_Tax_Number = objReader["Central_Sales_Tax_Number"].ToString();
                    cust.Outstanding_Amount = Convert.ToDouble(objReader["Outstanding_Amount"]);
                    cust.Credit_Limit = Convert.ToDouble(objReader["Credit_Limit"]);
                    cust.Cash_Credit_Limit_Indicator = objReader["Cash_Credit_Limit_Indicator"].ToString();
                    //cust.Collection_Days = objReader["Collection_Days"].ToString();
                    cust.customer_bank_name = objReader["customer_bank_name"].ToString();
                    cust.customer_bank_branch = objReader["customer_bank_branch"].ToString();
                    cust.IFSC_Code = objReader["IFSC_Code"].ToString();
                    cust.status = objReader["status"].ToString();
                    cust.Branch_Name = objReader["Branch_Name"].ToString();
                    cust.Town_Name = objReader["Town_Name"].ToString();
                    cust.Party_Type_Description = objReader["Party_Type_Description"].ToString();
                    cust.Address1 = objReader["address1"].ToString();
                    cust.Address2 = objReader["address2"].ToString();
                    cust.Address3 = objReader["address3"].ToString();
                    cust.Address4 = objReader["address4"].ToString();
                    cust.location = objReader["location"].ToString();
                    cust.TinNumber = objReader["tin"].ToString();
                    cust.CustOSLSStatus = objReader["CustOSLSStatus"].ToString();
                    cust.Salesman_Code = objReader["Sales_Man_Code"].ToString();
                    cust.Salesman = objReader["Sales_Man_Name"].ToString();
                    cust.Date_Of_Creation = objReader["Date_Of_Creation"].ToString();                    

                    custDetails.Add(cust);

                }
            }

            return custDetails;
        }
    }
    public class CustomerDtls
    {
        public CustomerDtls()
        {
        }

        private string _Code;
        private string _Name;
        private string _Status;
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private string _Address4;
        private string _Location;
        private string _GSTINNo;
        private string _PinCode;
        private string _Phone;
        private string _CrLimit;

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
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public string Address4
        {
            get { return _Address4; }
            set { _Address4 = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string GSTINNo
        {
            get { return _GSTINNo; }
            set { _GSTINNo = value; }
        }
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string CrLimit
        {
            get { return _CrLimit; }
            set { _CrLimit = value; }
        }
        public CustomerDtls(string Code, string Name, string Status)
        {
            _Code = Code;
            _Name = Name;
            _Status = Status;
        }

        public CustomerDtls(string Code, string Name)
        {
            _Code = Code;
            _Name = Name;
        }
    }

    public class CustomerFields
    {
        public CustomerFields()
        {

        }

        private string _Customer_Code;
        private string _Customer_Name;
        private string _Chart_of_Account_Code;
        private string _Party_Type_Code;
        private string _Classification_Segment;
        private string _Pincode;
        private string _Branch_Code;
        private string _Town_Code;
        private string _District_Code;
        private string _Alpha_Code;
        private string _Phone;
        private string _Fax;
        private string _Email;
        private string _Telex;
        private string _Proprietor_Name;
        private string _Proprietor_Mobile;
        private string _Contact_Person;
        private string _Contact_Person_Mobile;
        private string _Carrier;
        private string _Destination;
        private string _Firm_Type;
        private string _Registration_Type;
        private string _Local_Sales_Tax_Number;
        private string _Central_Sales_Tax_Number;
        private double _Outstanding_Amount;
        private double _Credit_Limit;
        private string _Cash_Credit_Limit_Indicator;
        private string _Collection_Days;
        private string _customer_bank_AccNo;
        private string _customer_bank_name;
        private string _customer_bank_branch;
        private string _IFSC_Code;
        private string _status;
        private string _Branch_Name;
        private string _Town_Name;
        private string _Party_Type_Description;
        private string _address1;
        private string _address2;
        private string _address3;
        private string _address4;
        private string _location;
        private string _TinNumber;
        private string _Salesman;
        private string _Salesman_Code;
        private string _BranchState_Code;
        private string _CustomerState_Code;
        private string _CustOSLSStatus;
        private string _Validity_Indicator;
        private double _Old_Credit_Limit;
        private string _Credit_Limit_Due_Date;
        private string _Date_Of_Creation;
        private string _Customer_Classification;
        private string _Customer_Segment;
        private string _Local_Outstation;
        private string _Dealer_Target;

        private string _NEFT_RefNo;
        private string _NEFT_Date;
        private string _NEFT_Amount;
        private string _NEFT_Remarks;

        public string Customer_Code
        {
            get { return _Customer_Code; }
            set { _Customer_Code = value; }
        }

        public string Customer_Name
        {
            get { return _Customer_Name; }
            set { _Customer_Name = value; }
        }

        public string Chart_of_Account_Code
        {
            get { return _Chart_of_Account_Code; }
            set { _Chart_of_Account_Code = value; }
        }

        public string Party_Type_Code
        {
            get { return _Party_Type_Code; }
            set { _Party_Type_Code = value; }
        }

        public string Classification_Segment
        {
            get { return _Classification_Segment; }
            set { _Classification_Segment = value; }
        }

        public string Customer_Classification
        {
            get { return _Customer_Classification; }
            set { _Customer_Classification = value; }
        }

        public string Customer_Segment
        {
            get { return _Customer_Segment; }
            set { _Customer_Segment = value; }
        }

        public string Local_Outstation
        {
            get { return _Local_Outstation; }
            set { _Local_Outstation = value; }
        }        

        public string Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }

        public string Branch_Code
        {
            get { return _Branch_Code; }
            set { _Branch_Code = value; }
        }

        public string Town_Code
        {
            get { return _Town_Code; }
            set { _Town_Code = value; }
        }

        public string District_Code
        {
            get { return _District_Code; }
            set { _District_Code = value; }
        }

        public string Alpha_Code
        {
            get { return _Alpha_Code; }
            set { _Alpha_Code = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Telex
        {
            get { return _Telex; }
            set { _Telex = value; }
        }

        public string Proprietor_Name
        {
            get { return _Proprietor_Name; }
            set { _Proprietor_Name = value; }
        }

        public string Proprietor_Mobile
        {
            get { return _Proprietor_Mobile; }
            set { _Proprietor_Mobile = value; }
        }

        public string Contact_Person
        {
            get { return _Contact_Person; }
            set { _Contact_Person = value; }
        }

        public string Contact_Person_Mobile
        {
            get { return _Contact_Person_Mobile; }
            set { _Contact_Person_Mobile = value; }
        }

        public string Carrier
        {
            get { return _Carrier; }
            set { _Carrier = value; }
        }

        public string Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }

        public string Firm_Type
        {
            get { return _Firm_Type; }
            set { _Firm_Type = value; }
        }

        public string Registration_Type
        {
            get { return _Registration_Type; }
            set { _Registration_Type = value; }
        }

        public string Local_Sales_Tax_Number
        {
            get { return _Local_Sales_Tax_Number; }
            set { _Local_Sales_Tax_Number = value; }
        }

        public string Central_Sales_Tax_Number
        {
            get { return _Central_Sales_Tax_Number; }
            set { _Central_Sales_Tax_Number = value; }
        }

        public double Outstanding_Amount
        {
            get { return _Outstanding_Amount; }
            set { _Outstanding_Amount = value; }
        }

        public double Credit_Limit
        {
            get { return _Credit_Limit; }
            set { _Credit_Limit = value; }
        }

        public string Cash_Credit_Limit_Indicator
        {
            get { return _Cash_Credit_Limit_Indicator; }
            set { _Cash_Credit_Limit_Indicator = value; }
        }

        public string Collection_Days
        {
            get { return _Collection_Days; }
            set { _Collection_Days = value; }
        }

        public string customer_bank_AccNo
        {
            get { return _customer_bank_AccNo; }
            set { _customer_bank_AccNo = value; }
        }

        public string customer_bank_name
        {
            get { return _customer_bank_name; }
            set { _customer_bank_name = value; }
        }

        public string customer_bank_branch
        {
            get { return _customer_bank_branch; }
            set { _customer_bank_branch = value; }
        }

        public string IFSC_Code
        {
            get { return _IFSC_Code; }
            set { _IFSC_Code = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Branch_Name
        {
            get { return _Branch_Name; }
            set { _Branch_Name = value; }
        }

        public string Town_Name
        {
            get { return _Town_Name; }
            set { _Town_Name = value; }
        }

        public string Party_Type_Description
        {
            get { return _Party_Type_Description; }
            set { _Party_Type_Description = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }

        public string Address4
        {
            get { return _address4; }
            set { _address4 = value; }
        }

        public string location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string TinNumber
        {
            get { return _TinNumber; }
            set { _TinNumber = value; }
        }

        public string Salesman
        {
            get { return _Salesman; }
            set { _Salesman = value; }
        }

        public string Salesman_Code
        {
            get { return _Salesman_Code; }
            set { _Salesman_Code = value; }
        }

        public string BranchState_Code
        {
            get { return _BranchState_Code; }
            set { _BranchState_Code = value; }
        }

        public string CustomerState_Code
        {
            get { return _CustomerState_Code; }
            set { _CustomerState_Code = value; }
        }

        public string CustOSLSStatus
        {
            get { return _CustOSLSStatus; }
            set { _CustOSLSStatus = value; }
        }

        public string Validity_Indicator
        {
            get { return _Validity_Indicator; }
            set { _Validity_Indicator = value; }
        }        

        public double Old_Credit_Limit
        {
            get { return _Old_Credit_Limit; }
            set { _Old_Credit_Limit = value; }
        }

        public string Credit_Limit_Due_Date
        {
            get { return _Credit_Limit_Due_Date; }
            set { _Credit_Limit_Due_Date = value; }
        }

        public string Date_Of_Creation
        {
            get { return _Date_Of_Creation; }
            set { _Date_Of_Creation = value; }
        }

        public string Dealer_Target
        {
            get { return _Dealer_Target; }
            set { _Dealer_Target = value; }
        }

        public string NEFT_RefNo
        {
            get { return _NEFT_RefNo; }
            set { _NEFT_RefNo = value; }
        }

        public string NEFT_Date
        {
            get { return _NEFT_Date; }
            set { _NEFT_Date = value; }
        }

        public string NEFT_Amount
        {
            get { return _NEFT_Amount; }
            set { _NEFT_Amount = value; }
        }

        public string NEFT_Remarks
        {
            get { return _NEFT_Remarks; }
            set { _NEFT_Remarks = value; }
        }        
    }

    public class CustomerType
    {
        public CustomerType(string CustomerTypeCode, string CustomerTypeDesc)
        {
            _CustomerTypeDesc = CustomerTypeDesc;
            _CustomerTypeCode = CustomerTypeCode;

        }



        private string _CustomerTypeDesc;
        private string _CustomerTypeCode;



        public string CustomerTypeDesc
        {
            get { return _CustomerTypeDesc; }
            set { _CustomerTypeDesc = value; }
        }
        public string CustomerTypeCode
        {
            get { return _CustomerTypeCode; }
            set { _CustomerTypeCode = value; }
        }

    }
}
