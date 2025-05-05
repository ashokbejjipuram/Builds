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

namespace IMPALLibrary.Masters
{
    [Serializable]
    public class Customer
    {
        public string Customer_Code { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Party_Type_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Branch_Code { get; set; }
        public string Town_Code { get; set; }

        public string Alpha_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Telex { get; set; }
        public string Contact_Person { get; set; }
        public string Contact_Designation { get; set; }
        public string Carrier { get; set; }        
        public string Freight_Indicator { get; set; }
        public string Destination { get; set; }
        //public string Group_Company_Indicator { get; set; }
        public string Local_Sales_Tax_Number { get; set; }
        public string Central_Sales_Tax_Number { get; set; }
        public string Sales_Upto_Previous_Year { get; set; }
        public string Sales_During_Previous_Year { get; set; }
        public string Sales_During_Current_Year { get; set; }
        public string Outstanding_Amount { get; set; }
        public string Oldest_Pending_Invoice { get; set; }
        public string Credit_Limit { get; set; }
        public string Cash_Credit_Limit_Indicator { get; set; }
        public string Cash_Discount_Collection_Days { get; set; }
        public string Customer_Bank_Name { get; set; }
        public string Customer_Bank_Branch { get; set; }
        public string Customer_Bank_Address { get; set; }
        public string Status { get; set; }
        public string CDType { get; set; }
        public string CalamityCess { get; set; }
        public string CostToCostCoupon { get; set; }

        //public string Oldest_Pending_Invoice { get; set; }
        //public string Insurance_Indicator { get; set; }
        //public string Place { get; set; }
        public string datestamp { get; set; }
        public string downup_status { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public string Location { get; set; }
        public string TIN { get; set; }
        public string GSTIN { get; set; }
        public string CustOSLSStatus { get; set; }
        public string State_Code { get; set; }
        public string Sales_Man_Code { get; set; }
        public string Sales_Man_Name { get; set; }
        public string OSCreditLimiStatus { get; set; }
        public string GroupCompanyStatus { get; set; }
        public int ChqDishonorCnt { get; set; }
        public int ChqDishonorInd { get; set; }

        public List<ShippingAddressDtls> ShippingAddress { get; set; }
    }
    [Serializable]

    public class ShippingAddressDtls
    {
        public ShippingAddressDtls(string Indicator, string Address)
        {
            _Indicator = Indicator;
            _Address = Address;
        }

        private string _Indicator;
        private string _Address;

        public string Indicator
        {
            get { return _Indicator; }
            set { _Indicator = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
    }    

    public class Customers
    {
        public List<Customer> GetAllCustomers(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master WITH (NOLOCK)";
            sSQL = sSQL + " where branch_code = '" + strBranchCode + "' and Status='A' order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetAllCustomersComboBox(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master WITH (NOLOCK)";
            sSQL = sSQL + " where branch_code = '" + strBranchCode + "' and Status='A' order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetAllCustomersExisting(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master WITH (NOLOCK)";
            sSQL = sSQL + " where branch_code = '" + strBranchCode + "' order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetAllCustomersPDCRegister(string strBranchCode, string strAccPeriod)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select p.Customer_Code,c.Customer_Name + ' | ' + Location as 'Customer_Name' from PDC_Register p WITH (NOLOCK) inner join Customer_Master c WITH (NOLOCK) on p.Branch_Code='" + strBranchCode + "'";
            sSQL = sSQL + " and Accounting_Period=" + strAccPeriod + " and p.Branch_Code=c.Branch_Code and p.Customer_Code=c.Customer_Code and p.Status='A' group by p.Customer_Code,c.Customer_Name + ' | ' + Location order by c.Customer_Name + ' | ' + Location";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetAllCustomersForReceipts(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustomersListForReceipts");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetGovtCustomers(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master WITH (NOLOCK)";
            sSQL = sSQL + " where branch_code = '" + strBranchCode + "' and Destination in ('DLRSTU','DLRGOV','DLRCFD') and Status='A' order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetExportCustomers(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master WITH (NOLOCK)";
            sSQL = sSQL + " where branch_code = '" + strBranchCode + "' and Destination ='DLRFOM' and Status='A' order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetCostToCostCustomers(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select distinct Customer_Code,Customer_Name + ' | ' + Location as 'Customer_Name' from Customer_Master_CostToCost WITH (NOLOCK)";
            sSQL = sSQL + " where Branch_Code = '" + strBranchCode + "' and Status='A' Order by Customer_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetCustomerDetails(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustomerDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    objCustomer.address1 = reader[2].ToString();
                    objCustomer.address2 = reader[3].ToString();
                    objCustomer.address3 = reader[4].ToString();
                    objCustomer.address4 = reader[5].ToString();
                    objCustomer.Location = reader[6].ToString();
                    objCustomer.TIN = reader[7].ToString();
                    objCustomer.Outstanding_Amount = reader[8].ToString();
                    objCustomer.Credit_Limit = reader[9].ToString();
                    objCustomer.Sales_Man_Code = reader[10].ToString();
                    objCustomer.Sales_Man_Name = reader[11].ToString();
                    objCustomer.Party_Type_Code = reader[12].ToString();
                    objCustomer.Town_Code = reader[13].ToString();
                    objCustomer.GSTIN = reader[14].ToString();
                    objCustomer.CustOSLSStatus = reader[15].ToString();
                    objCustomer.CDType = reader[16].ToString();
                    objCustomer.CalamityCess = reader[17].ToString();
                    objCustomer.CostToCostCoupon = reader[18].ToString();
                    objCustomer.Phone = reader[19].ToString();
                    objCustomer.State_Code = reader[20].ToString();

                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetCustomerDetailsWithShippingAddress(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustomerDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, strCustomerCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    objCustomer.address1 = reader[2].ToString();
                    objCustomer.address2 = reader[3].ToString();
                    objCustomer.address3 = reader[4].ToString();
                    objCustomer.address4 = reader[5].ToString();
                    objCustomer.Location = reader[6].ToString();
                    objCustomer.TIN = reader[7].ToString();
                    objCustomer.Outstanding_Amount = reader[8].ToString();
                    objCustomer.Credit_Limit = reader[9].ToString();
                    objCustomer.Sales_Man_Code = reader[10].ToString();
                    objCustomer.Sales_Man_Name = reader[11].ToString();
                    objCustomer.Party_Type_Code = reader[12].ToString();
                    objCustomer.Town_Code = reader[13].ToString();
                    objCustomer.GSTIN = reader[14].ToString();
                    objCustomer.CustOSLSStatus = reader[15].ToString();
                    objCustomer.CDType = reader[16].ToString();
                    objCustomer.CalamityCess = reader[17].ToString();
                    objCustomer.CostToCostCoupon = reader[18].ToString();
                    objCustomer.Phone = reader[19].ToString();
                    objCustomer.State_Code = reader[20].ToString();
                    objCustomer.Carrier = reader[21].ToString();
                    objCustomer.Freight_Indicator = reader[22].ToString();
                }

                reader.NextResult();

                List<ShippingAddressDtls> shippingAddress = new List<ShippingAddressDtls>();
                shippingAddress.Add(new ShippingAddressDtls("0", "--Select--"));

                while (reader.Read())
                {
                    shippingAddress.Add(new ShippingAddressDtls (reader["Indicator"].ToString(), reader["Shipping_Address"].ToString()));                    
                }

                objCustomer.ShippingAddress = shippingAddress;

                lstCustomers.Add(objCustomer);
            }

            return lstCustomers;
        }

        public string CanCustomerOSCreditLimitsReq(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_calcos_sales_request");
            ImpalDB.AddInParameter(cmdCustomerOS, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmdCustomerOS, "@Customer_Code", DbType.String, strCustomerCode);
            cmdCustomerOS.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCustomerOS))
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() == "ERROR")
                    {
                        return "ERROR";
                    }
                    else
                    {
                        return "OK";
                    }
                }
            }
            return "OK";
        }

        public string CanCustomerOSCreditLimits(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_calcos_sales");
            ImpalDB.AddInParameter(cmdCustomerOS, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmdCustomerOS, "@Customer_Code", DbType.String, strCustomerCode);
            cmdCustomerOS.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCustomerOS))
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() == "ERROR")
                    {
                        return "ERROR";
                    }
                    else
                    {
                        return "OK";
                    }
                }
            }
            return "OK";
        }

        public List<Customer> GetAllVendors(string strBranchCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            objCustomer.Customer_Code = "0";
            objCustomer.Customer_Name = "-- Select --";
            lstCustomers.Add(objCustomer);

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Vendor_Code,Vendor_Name + ' | ' + Location from Vendor_Master WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "' and Status='A' order by Vendor_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer = new Customer();
                    objCustomer.Customer_Code = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public Customer GetVendorInfoByVendorCode(string strBranchCode, string strVendorCode)
        {
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVendorDetails");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Vendor_Code", DbType.String, strVendorCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objCustomer.Customer_Code = reader["Vendor_Code"].ToString();
                    objCustomer.Customer_Name = reader["Vendor_Name"].ToString();
                    objCustomer.Location = reader["Location"].ToString();
                    objCustomer.address1 = reader["address1"].ToString();
                    objCustomer.address2 = reader["address2"].ToString();
                    objCustomer.address3 = reader["address3"].ToString();
                    objCustomer.address4 = reader["address4"].ToString();
                    objCustomer.GSTIN = reader["GSTIN"].ToString();
                    objCustomer.Phone = reader["Phone"].ToString();
                    objCustomer.CustOSLSStatus = reader["OSLSstatus"].ToString();
                }
            }

            return objCustomer;
        }

        public string CanCustomerOSDaysCreditLimits(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();

            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_outstanding");
            ImpalDB.AddInParameter(cmdCustomerOS, "@Branch_Code", DbType.String, strBranchCode);	
            ImpalDB.AddInParameter(cmdCustomerOS, "@Customer_Code", DbType.String, strCustomerCode);																										
            cmdCustomerOS.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCustomerOS))
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() == "ERROR")
                    {
                        return "ERROR";
                    }
                    else
                    {
                        return "OK";
                    }
                }
            }

            return "OK";
        }

        public List<Customer> GetCustomerOSCreditLimitsReq(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_calcos_sales_request");
            ImpalDB.AddInParameter(cmdCustomerOS, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmdCustomerOS, "@Customer_Code", DbType.String, strCustomerCode);
            cmdCustomerOS.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCustomerOS))
            {
                while (reader.Read())
                {
                    objCustomer.OSCreditLimiStatus = reader[0].ToString();
                    objCustomer.Customer_Name = reader[1].ToString();
                    objCustomer.Outstanding_Amount = reader[2].ToString();
                    objCustomer.Credit_Limit = reader[3].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetCustomerOSCreditLimits(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();

            //DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_calcos_sales");

            DbCommand cmdCustomerOS = ImpalDB.GetStoredProcCommand("usp_calcos_sales_New");
            ImpalDB.AddInParameter(cmdCustomerOS, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmdCustomerOS, "@Customer_Code", DbType.String, strCustomerCode);
            cmdCustomerOS.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCustomerOS))
            {
                while (reader.Read())
                {
                    objCustomer.OSCreditLimiStatus = reader[0].ToString();
                    objCustomer.Customer_Code = reader[1].ToString();
                    objCustomer.Customer_Name = reader[2].ToString();
                    objCustomer.Outstanding_Amount = reader[3].ToString();
                    objCustomer.Credit_Limit = reader[4].ToString();
                    objCustomer.GroupCompanyStatus = reader[5].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }

        public List<Customer> GetCustomerOSCreditLimitsOutstanding(string strBranchCode, string strCustomerCode)
        {
            List<Customer> lstCustomers = new List<Customer>();
            Customer objCustomer = new Customer();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Customer_Name, isnull(os_amt,0),isnull(cr_limit,0) from cust_aging_outstanding WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "' and Customer_Code = '" + strCustomerCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {

                    objCustomer.Customer_Name = reader[0].ToString();
                    objCustomer.Outstanding_Amount = reader[1].ToString();
                    objCustomer.Credit_Limit = reader[2].ToString();
                    lstCustomers.Add(objCustomer);
                }
            }

            return lstCustomers;
        }
    }
}