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
using System.Transactions;
using IMPALLibrary.Masters.CustomerDetails;

namespace IMPALLibrary.Masters
{
    [Serializable]
    public class ECreditApplicationCust
    {
        public string Branch_Code { get; set; }
        public string Form_Number { get; set; }
        public string Form_Date { get; set; }
        public string Indicator { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address1 { get; set; }
        public string Customer_Address2 { get; set; }
        public string Customer_Address3 { get; set; }
        public string Customer_Address4 { get; set; }
        public string Proprietor_Name { get; set; }
        public string Proprietor_Mobile { get; set; }
        public string Contact_Person { get; set; }
        public string Contact_Person_Mobile { get; set; }
        public string Group_Company_Codes { get; set; }
        public string Migration_From_Branch_Code { get; set; }
        public string Migration_From_Customer_Code { get; set; }
        public string Year_Of_Establishment { get; set; }
        public string State_Code { get; set; }
        public string District_Code { get; set; }
        public string Town_Code { get; set; }
        public string Town_Classification { get; set; }
        public string Phone_No { get; set; }
        public string Mobile_No { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string GSTIN_No { get; set; }
        public string Type_of_Company { get; set; }
        public string Type_of_Registration { get; set; }
        public string Turnover_Value { get; set; }
        public string GSTIN_Location { get; set; }
        public string Stock_Value { get; set; }
        public string Impal_Turnover_Value { get; set; }
        public string Representative_Type { get; set; }
        public string Distance_From_Branch { get; set; }
        public string Distance_from_RR { get; set; }
        public string Travel_Classificaion { get; set; }
        public string Salesman_code { get; set; }
        public string Dealer_Target { get; set; }
        public string Period_of_Visit { get; set; }
        public string Classification { get; set; }
        public string Segment { get; set; }
        public string MultipleTown { get; set; }
        public string Supplier_code_From_Direct_Suppliers { get; set; }
        public string Dealing_with_Other_Group_Co { get; set; }
        public string ASC_Line_Codes { get; set; }
        public string Additonal_Dealer_Info { get; set; }
        public string Transporter_Name { get; set; }
        public string Remarks { get; set; }
        public string ASD_Line_Codes { get; set; }
        public string Cash_Purchase_Value { get; set; }
        public string Expected_Supplier_Codes { get; set; }
        public string Outstanding_Amount { get; set; }
        public string Existing_Credit_Limit { get; set; }
        public string Proposed_Credit_Limit { get; set; }
        public string Cash_Credit_Limit_Indicator { get; set; }
        public string Cr_Limit_Validity_Ind { get; set; }
        public string Cr_Limit_Due_Date { get; set; }
        public string FreightIndicator { get; set; }
        public string First_Time_Credit_Limit_Request { get; set; }
        public string Bank_AccountNo { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string IFSC_Code { get; set; }
        public string Name_of_Account_Holder { get; set; }
        public string Debit_Credit_Card_Number { get; set; }
        public string Expiry_Date { get; set; }
        public string Payment_Mode { get; set; }
        public string Branch_Approval_Status { get; set; }
        public string Branch_Approval_Date { get; set; }
        public string HO_Approval_Status { get; set; }
        public string HO_Approval_Status_Remarks { get; set; }
        public string HO_Approval_Date { get; set; }
        public string Approved_Credit_Limit { get; set; }
        public string Date_of_Closure { get; set; }
        public string Reason_For_closure { get; set; }
        public string Written_Off_Amount { get; set; }
        public string Status { get; set; }
        public string Accounting_Period_Code { get; set; }
        public string Userid { get; set; }
        public string CDenabled { get; set; }
        public string Datestamp { get; set; }

        public string ErrorCode { get; set; }
    }

    public class EcreditItem
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
    }

    public class EcreditPaymentPattern
    {
        public string Month { get; set; }
        public string Credit_Limit { get; set; }
        public string Total { get; set; }
        public string CurBal { get; set; }
        public string Above30 { get; set; }
        public string Above60 { get; set; }
        public string Above90 { get; set; }
        public string Above180 { get; set; }
        public string CollPercentage { get; set; }
        public string NoOfChqReturns { get; set; }
    }

    public class ECreditFormCustTransactions
    {
        public DataSet AddECrdeitApplicationForm(ECreditApplicationCust ecreditAppln)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Add_ECredit_Application_Form");
                    ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, ecreditAppln.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ecreditAppln.Indicator);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, ecreditAppln.Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, ecreditAppln.Customer_Name);
                    ImpalDB.AddInParameter(cmd, "@Customer_Address1", DbType.String, ecreditAppln.Customer_Address1);
                    ImpalDB.AddInParameter(cmd, "@Customer_Address2", DbType.String, ecreditAppln.Customer_Address2);
                    ImpalDB.AddInParameter(cmd, "@Customer_Address3", DbType.String, ecreditAppln.Customer_Address3);
                    ImpalDB.AddInParameter(cmd, "@Customer_Address4", DbType.String, ecreditAppln.Customer_Address4);
                    ImpalDB.AddInParameter(cmd, "@Proprietor_Name", DbType.String, ecreditAppln.Proprietor_Name);
                    ImpalDB.AddInParameter(cmd, "@Proprietor_Mobile", DbType.String, ecreditAppln.Proprietor_Mobile);
                    ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, ecreditAppln.Contact_Person);
                    ImpalDB.AddInParameter(cmd, "@Contact_Person_Mobile", DbType.String, ecreditAppln.Contact_Person_Mobile);
                    ImpalDB.AddInParameter(cmd, "@Group_Company_Codes", DbType.String, ecreditAppln.Group_Company_Codes);
                    ImpalDB.AddInParameter(cmd, "@Migration_From_Branch_Code", DbType.String, ecreditAppln.Migration_From_Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Migration_From_Customer_Code", DbType.String, ecreditAppln.Migration_From_Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Year_Of_Establishment", DbType.String, ecreditAppln.Year_Of_Establishment);
                    ImpalDB.AddInParameter(cmd, "@State_Code", DbType.String, ecreditAppln.State_Code);
                    ImpalDB.AddInParameter(cmd, "@District_Code", DbType.String, ecreditAppln.District_Code);
                    ImpalDB.AddInParameter(cmd, "@Town_Code", DbType.String, ecreditAppln.Town_Code);
                    ImpalDB.AddInParameter(cmd, "@Town_Classification", DbType.String, ecreditAppln.Town_Classification);
                    ImpalDB.AddInParameter(cmd, "@Phone_No", DbType.String, ecreditAppln.Phone_No);
                    ImpalDB.AddInParameter(cmd, "@Mobile_No", DbType.String, ecreditAppln.Mobile_No);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, ecreditAppln.Location);
                    ImpalDB.AddInParameter(cmd, "@Email", DbType.String, ecreditAppln.Email);
                    ImpalDB.AddInParameter(cmd, "@PinCode", DbType.String, ecreditAppln.PinCode);
                    ImpalDB.AddInParameter(cmd, "@GSTIN_No", DbType.String, ecreditAppln.GSTIN_No);
                    ImpalDB.AddInParameter(cmd, "@Type_of_Company", DbType.String, ecreditAppln.Type_of_Company);
                    ImpalDB.AddInParameter(cmd, "@Type_of_Registration", DbType.String, ecreditAppln.Type_of_Registration);
                    ImpalDB.AddInParameter(cmd, "@GSTIN_Location", DbType.String, ecreditAppln.GSTIN_Location);
                    ImpalDB.AddInParameter(cmd, "@Turnover_Value", DbType.String, ecreditAppln.Turnover_Value);                    
                    ImpalDB.AddInParameter(cmd, "@Stock_Value", DbType.String, ecreditAppln.Stock_Value);
                    ImpalDB.AddInParameter(cmd, "@Impal_Turnover_Value", DbType.String, ecreditAppln.Impal_Turnover_Value);
                    ImpalDB.AddInParameter(cmd, "@Representative_Type", DbType.String, ecreditAppln.Representative_Type);
                    ImpalDB.AddInParameter(cmd, "@Distance_From_Branch", DbType.String, ecreditAppln.Distance_From_Branch);
                    ImpalDB.AddInParameter(cmd, "@Distance_from_RR", DbType.String, ecreditAppln.Distance_from_RR);
                    ImpalDB.AddInParameter(cmd, "@Travel_Classificaion", DbType.String, ecreditAppln.Travel_Classificaion);
                    ImpalDB.AddInParameter(cmd, "@Salesman_code", DbType.String, ecreditAppln.Salesman_code);
                    ImpalDB.AddInParameter(cmd, "@Dealer_Target", DbType.String, ecreditAppln.Dealer_Target);
                    ImpalDB.AddInParameter(cmd, "@Period_of_Visit", DbType.String, ecreditAppln.Period_of_Visit);
                    ImpalDB.AddInParameter(cmd, "@Dealer_Classification", DbType.String, ecreditAppln.Classification);
                    ImpalDB.AddInParameter(cmd, "@Dealer_Segment", DbType.String, ecreditAppln.Segment);
                    ImpalDB.AddInParameter(cmd, "@Supplier_code_From_Direct_Suppliers", DbType.String, ecreditAppln.Supplier_code_From_Direct_Suppliers);
                    ImpalDB.AddInParameter(cmd, "@Dealer_Multi_Towns", DbType.String, ecreditAppln.MultipleTown);
                    ImpalDB.AddInParameter(cmd, "@Dealing_with_Other_Group_Co", DbType.String, ecreditAppln.Dealing_with_Other_Group_Co);
                    ImpalDB.AddInParameter(cmd, "@ASC_Line_Codes", DbType.String, ecreditAppln.ASC_Line_Codes);
                    ImpalDB.AddInParameter(cmd, "@Additonal_Dealer_Info", DbType.String, ecreditAppln.Additonal_Dealer_Info);
                    ImpalDB.AddInParameter(cmd, "@Transporter_Name", DbType.String, ecreditAppln.Transporter_Name);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, ecreditAppln.Remarks);
                    ImpalDB.AddInParameter(cmd, "@ASD_Line_Codes", DbType.String, ecreditAppln.ASD_Line_Codes);
                    ImpalDB.AddInParameter(cmd, "@Cash_Purchase_Value", DbType.String, ecreditAppln.Cash_Purchase_Value);
                    ImpalDB.AddInParameter(cmd, "@Expected_Supplier_Codes", DbType.String, ecreditAppln.Expected_Supplier_Codes);
                    ImpalDB.AddInParameter(cmd, "@Outstanding_Amount", DbType.String, ecreditAppln.Outstanding_Amount);
                    ImpalDB.AddInParameter(cmd, "@Existing_Credit_Limit", DbType.String, ecreditAppln.Existing_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Proposed_Credit_Limit", DbType.String, ecreditAppln.Proposed_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Cash_Credit_Limit_Indicator", DbType.String, ecreditAppln.Cash_Credit_Limit_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Validity_Indicator", DbType.String, ecreditAppln.Cr_Limit_Validity_Ind);
                    ImpalDB.AddInParameter(cmd, "@Validity_Date", DbType.String, ecreditAppln.Cr_Limit_Due_Date);
                    ImpalDB.AddInParameter(cmd, "@CD_Enabled", DbType.String, ecreditAppln.CDenabled);
                    ImpalDB.AddInParameter(cmd, "@Freight_Indicator", DbType.String, ecreditAppln.FreightIndicator);
                    ImpalDB.AddInParameter(cmd, "@First_Time_Credit_Limit_Request", DbType.String, ecreditAppln.First_Time_Credit_Limit_Request);
                    ImpalDB.AddInParameter(cmd, "@Bank_AccountNo", DbType.String, ecreditAppln.Bank_AccountNo);
                    ImpalDB.AddInParameter(cmd, "@Bank_Name", DbType.String, ecreditAppln.Bank_Name);
                    ImpalDB.AddInParameter(cmd, "@Bank_Branch", DbType.String, ecreditAppln.Bank_Branch);
                    ImpalDB.AddInParameter(cmd, "@IFSC_Code", DbType.String, ecreditAppln.IFSC_Code);
                    ImpalDB.AddInParameter(cmd, "@Name_of_Account_Holder", DbType.String, ecreditAppln.Name_of_Account_Holder);
                    ImpalDB.AddInParameter(cmd, "@Debit_Credit_Card_Number", DbType.String, ecreditAppln.Debit_Credit_Card_Number);
                    ImpalDB.AddInParameter(cmd, "@Expiry_Date", DbType.String, ecreditAppln.Expiry_Date);
                    ImpalDB.AddInParameter(cmd, "@Payment_Mode", DbType.String, ecreditAppln.Payment_Mode);
                    ImpalDB.AddInParameter(cmd, "@Branch_Approval_Status", DbType.String, ecreditAppln.Branch_Approval_Status);
                    ImpalDB.AddInParameter(cmd, "@Branch_Approval_Date", DbType.String, ecreditAppln.Branch_Approval_Date);
                    ImpalDB.AddInParameter(cmd, "@HO_Approval_Status", DbType.String, ecreditAppln.HO_Approval_Status);
                    ImpalDB.AddInParameter(cmd, "@HO_Approval_Date", DbType.String, ecreditAppln.HO_Approval_Date);
                    ImpalDB.AddInParameter(cmd, "@Approved_Credit_Limit", DbType.String, ecreditAppln.Approved_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Date_of_Closure", DbType.String, ecreditAppln.Date_of_Closure);
                    ImpalDB.AddInParameter(cmd, "@Reason_For_closure", DbType.String, ecreditAppln.Reason_For_closure);
                    ImpalDB.AddInParameter(cmd, "@Written_OFF_Amount", DbType.String, ecreditAppln.Written_Off_Amount);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, ecreditAppln.Status);
                    ImpalDB.AddInParameter(cmd, "@Userid", DbType.String, ecreditAppln.Userid);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    scope.Complete();
                    ecreditAppln.ErrorCode = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet UpdECrdeitApplicationForm(ECreditApplicationCust ecreditAppln)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upd_ECredit_Application_Form");
                    ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, ecreditAppln.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, ecreditAppln.Form_Number);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, ecreditAppln.Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, ecreditAppln.Customer_Name);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, ecreditAppln.Location);
                    ImpalDB.AddInParameter(cmd, "@Existing_Credit_Limit", DbType.String, ecreditAppln.Existing_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Proposed_Credit_Limit", DbType.String, ecreditAppln.Proposed_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Validity_Indicator", DbType.String, ecreditAppln.Cr_Limit_Validity_Ind);
                    ImpalDB.AddInParameter(cmd, "@Validity_Date", DbType.String, ecreditAppln.Cr_Limit_Due_Date);
                    ImpalDB.AddInParameter(cmd, "@Approved_Credit_Limit", DbType.String, ecreditAppln.Approved_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Reason_For_closure", DbType.String, ecreditAppln.Reason_For_closure);
                    ImpalDB.AddInParameter(cmd, "@Written_Off_Amount", DbType.String, ecreditAppln.Written_Off_Amount);
                    ImpalDB.AddInParameter(cmd, "@Userid", DbType.String, ecreditAppln.Userid);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    scope.Complete();
                    ecreditAppln.ErrorCode = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet UpdECrdeitApplicationFormReject(ECreditApplicationCust ecreditAppln)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upd_ECredit_Application_Form_Reject");
                    ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, ecreditAppln.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, ecreditAppln.Form_Number);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, ecreditAppln.Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Customer_Name", DbType.String, ecreditAppln.Customer_Name);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, ecreditAppln.Location);
                    ImpalDB.AddInParameter(cmd, "@Existing_Credit_Limit", DbType.String, ecreditAppln.Existing_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Validity_Indicator", DbType.String, ecreditAppln.Cr_Limit_Validity_Ind);
                    ImpalDB.AddInParameter(cmd, "@Validity_Date", DbType.String, ecreditAppln.Cr_Limit_Due_Date);
                    ImpalDB.AddInParameter(cmd, "@Approved_Credit_Limit", DbType.String, ecreditAppln.Approved_Credit_Limit);
                    ImpalDB.AddInParameter(cmd, "@Userid", DbType.String, ecreditAppln.Userid);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, ecreditAppln.Remarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    scope.Complete();
                    ecreditAppln.ErrorCode = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public void UpdECrdeitApplicationFormWithDraw(ECreditApplicationCust ecreditAppln)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upd_ECredit_Application_Form_WithDraw");
                    ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, ecreditAppln.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, ecreditAppln.Form_Number);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, ecreditAppln.Customer_Code);
                    ImpalDB.AddInParameter(cmd, "@Userid", DbType.String, ecreditAppln.Userid);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, ecreditAppln.Remarks);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                    ecreditAppln.ErrorCode = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomerDtls GetECrdeitApplnSisDetails(string BranchCode, string CustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            CustomerDtls oCustomer = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSisterConcern_Cust_Details");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        oCustomer = new CustomerDtls();

                        while (reader.Read())
                        {
                            oCustomer.Code = reader[0].ToString();
                            oCustomer.CrLimit = reader[1].ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oCustomer;
        }

        public DataTable GetECrdeitApplnSisDetailsGroup(string BranchCode, string CustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSisterConcern_Group_Cust_Details");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }

        public List<EcreditPaymentPattern> GetPaymentPatternDetails(string BranchCode, string CustomerCode)
        {
            List<EcreditPaymentPattern> pymtPatternDtls = new List<EcreditPaymentPattern>();
            EcreditPaymentPattern objItem = new EcreditPaymentPattern();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetEcredit_PaymentPattern_Details");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objItem = new EcreditPaymentPattern();
                        objItem.Month = reader["MonthYear"].ToString();
                        objItem.Credit_Limit = reader["cr_limit"].ToString();
                        objItem.Total = reader["os_amt"].ToString();
                        objItem.CurBal = reader["curbal"].ToString();
                        objItem.Above30 = reader["above30"].ToString();
                        objItem.Above60 = reader["above60"].ToString();
                        objItem.Above90 = reader["above90"].ToString();
                        objItem.Above180 = reader["above180"].ToString();
                        objItem.CollPercentage = reader["collection_per"].ToString();
                        objItem.NoOfChqReturns = reader["ChequeReturns"].ToString();

                        pymtPatternDtls.Add(objItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pymtPatternDtls;
        }

        public List<EcreditItem> GetEcreditApplicationNos(string BranchCode, string Userid)
        {            
            List<EcreditItem> ApplicationNosList = new List<EcreditItem>();
            EcreditItem objItem = new EcreditItem();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ApplicationNosList.Add(objItem);
            string Qry = string.Empty;

            Database ImpalDB = DataAccess.GetDatabase();

            if (BranchCode == "")
                Qry = "Select distinct Branch_Code,Form_Number from ECredit_Application_Form WITH (NOLOCK) Where Approver_Userid='" + Userid + "' and Branch_Approval_Status is NULL and HO_Approval_Status is NULL and ISNULL(Status,'A')='A' Order by Branch_Code,Form_Number";
            else
                Qry = "Select distinct Branch_Code,Form_Number from ECredit_Application_Form WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Approver_Userid='" + Userid + "' and Branch_Approval_Status is NULL and HO_Approval_Status is NULL and ISNULL(Status,'A')='A' Order by Form_Number";

            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                EcreditItem ecrItem = new EcreditItem();

                while (reader.Read())
                {
                    ecrItem = new EcreditItem();
                    ecrItem.ItemDesc = reader["Form_Number"].ToString();
                    ecrItem.ItemCode = reader["Form_Number"].ToString();
                    ApplicationNosList.Add(ecrItem);
                }
            }

            return ApplicationNosList;
        }

        public List<EcreditItem> GetEcreditApplicationNosWithDrawal(string BranchCode)
        {
            List<EcreditItem> ApplicationNosList = new List<EcreditItem>();
            EcreditItem objItem = new EcreditItem();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ApplicationNosList.Add(objItem);

            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select distinct Form_Number from ECredit_Application_Form WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Branch_Approval_Status is null and HO_Approval_Status is null Order by Form_Number desc";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                EcreditItem ecrItem = new EcreditItem();

                while (reader.Read())
                {
                    ecrItem = new EcreditItem();
                    ecrItem.ItemDesc = reader["Form_Number"].ToString();
                    ecrItem.ItemCode = reader["Form_Number"].ToString();
                    ApplicationNosList.Add(ecrItem);
                }
            }

            return ApplicationNosList;
        }

        public List<EcreditItem> GetEcreditApplicationNosReprint(string BranchCode)
        {
            List<EcreditItem> ApplicationNosList = new List<EcreditItem>();
            EcreditItem objItem = new EcreditItem();
            objItem.ItemDesc = "-- Select --";
            objItem.ItemCode = "0";
            ApplicationNosList.Add(objItem);

            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select distinct Form_Number from ECredit_Application_Form WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' Order by Form_Number desc";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(Qry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                EcreditItem ecrItem = new EcreditItem();

                while (reader.Read())
                {
                    ecrItem = new EcreditItem();
                    ecrItem.ItemDesc = reader["Form_Number"].ToString();
                    ecrItem.ItemCode = reader["Form_Number"].ToString();
                    ApplicationNosList.Add(ecrItem);
                }
            }

            return ApplicationNosList;
        }

        public List<ECreditApplicationCust> GetPendingRequestDetails(string BranchCode, string CustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            List<ECreditApplicationCust> lstECreditApplicationEntity = new List<ECreditApplicationCust>();
            ECreditApplicationCust ecreditApplnDtls = new ECreditApplicationCust();

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustPendingRequest_Count");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ecreditApplnDtls = new ECreditApplicationCust();
                        ecreditApplnDtls.Form_Number = reader["Form_Number"].ToString();
                        ecreditApplnDtls.Form_Date = reader["Form_Date"].ToString();
                        ecreditApplnDtls.Existing_Credit_Limit = reader["Existing_Credit_Limit"].ToString();
                        ecreditApplnDtls.Proposed_Credit_Limit = reader["Proposed_Credit_Limit"].ToString();

                        lstECreditApplicationEntity.Add(ecreditApplnDtls);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return lstECreditApplicationEntity;
        }

        public List<ECreditApplicationCust> GetCustDetailsEcreditForm(string BranchCode, string CustomerCode)
        {
            List<ECreditApplicationCust> lstECreditApplicationEntity = new List<ECreditApplicationCust>();

            ECreditApplicationCust ecreditApplnDtls = new ECreditApplicationCust();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCustDetails_EcreditForm");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, CustomerCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ecreditApplnDtls = new ECreditApplicationCust();
                    ecreditApplnDtls.Branch_Code = reader["Branch_code"].ToString();
                    ecreditApplnDtls.Customer_Code = reader["Customer_Code"].ToString();
                    ecreditApplnDtls.Customer_Name = reader["Customer_Name"].ToString();
                    ecreditApplnDtls.Customer_Address1 = reader["Address1"].ToString();
                    ecreditApplnDtls.Customer_Address2 = reader["Address2"].ToString();
                    ecreditApplnDtls.Customer_Address3 = reader["Address3"].ToString();
                    ecreditApplnDtls.Customer_Address4 = reader["Address4"].ToString();
                    ecreditApplnDtls.Proprietor_Name = reader["Proprietor_Name"].ToString();
                    ecreditApplnDtls.Proprietor_Mobile = reader["Proprietor_Mobile"].ToString();
                    ecreditApplnDtls.Contact_Person = reader["Contact_Person"].ToString();
                    ecreditApplnDtls.Contact_Person_Mobile = reader["Contact_Person_Mobile"].ToString();
                    ecreditApplnDtls.Group_Company_Codes = reader["Group_Company_Codes"].ToString();
                    ecreditApplnDtls.Migration_From_Branch_Code = reader["Migration_From_Branch_Code"].ToString();
                    ecreditApplnDtls.Migration_From_Customer_Code = reader["Migration_From_Customer_Code"].ToString();
                    ecreditApplnDtls.Year_Of_Establishment = reader["Year_Of_Establishment"].ToString();
                    ecreditApplnDtls.State_Code = reader["State_Code"].ToString();
                    ecreditApplnDtls.District_Code = reader["District_Code"].ToString();
                    ecreditApplnDtls.Town_Code = reader["Town_Code"].ToString();
                    ecreditApplnDtls.Town_Classification = reader["Town_Classification"].ToString();
                    ecreditApplnDtls.Phone_No = reader["Phone"].ToString();
                    ecreditApplnDtls.Mobile_No = reader["Proprietor_Mobile"].ToString();
                    ecreditApplnDtls.Location = reader["Location"].ToString();
                    ecreditApplnDtls.Email = reader["Email"].ToString();
                    ecreditApplnDtls.CDenabled = reader["Telex"].ToString();
                    ecreditApplnDtls.PinCode = reader["PinCode"].ToString();
                    ecreditApplnDtls.GSTIN_No = reader["GSTIN_No"].ToString();
                    ecreditApplnDtls.Type_of_Company = reader["Firm_Type"].ToString();
                    ecreditApplnDtls.Type_of_Registration = reader["Registration_Type"].ToString();
                    ecreditApplnDtls.Turnover_Value = reader["Turnover_Value"].ToString();
                    ecreditApplnDtls.GSTIN_Location = reader["GSTIN_Location"].ToString();
                    ecreditApplnDtls.Stock_Value = reader["Stock_Value"].ToString();
                    ecreditApplnDtls.Impal_Turnover_Value = reader["Impal_Turnover_Value"].ToString();
                    ecreditApplnDtls.Representative_Type = reader["Representative_Type"].ToString();
                    ecreditApplnDtls.Distance_From_Branch = reader["Distance_From_Branch"].ToString();
                    ecreditApplnDtls.Distance_from_RR = reader["Distance_from_RR"].ToString();
                    ecreditApplnDtls.Travel_Classificaion = reader["Travel_Classificaion"].ToString();
                    ecreditApplnDtls.Salesman_code = reader["Sales_Man_Code"].ToString();
                    ecreditApplnDtls.Dealer_Target = reader["Dealer_Target"].ToString();
                    ecreditApplnDtls.Period_of_Visit = reader["Period_of_Visit"].ToString();
                    ecreditApplnDtls.Classification = reader["Customer_Classification"].ToString();
                    ecreditApplnDtls.Segment = reader["Customer_Segment"].ToString();
                    ecreditApplnDtls.MultipleTown = reader["Dealer_Multi_Towns"].ToString();
                    ecreditApplnDtls.Supplier_code_From_Direct_Suppliers = reader["Supplier_code_From_Direct_Suppliers"].ToString();
                    ecreditApplnDtls.Dealing_with_Other_Group_Co = reader["Dealing_with_Other_Group_Co"].ToString();
                    ecreditApplnDtls.Transporter_Name = reader["Transporter_Name"].ToString();
                    ecreditApplnDtls.ASC_Line_Codes = reader["ASC_Line_Codes"].ToString();
                    ecreditApplnDtls.Additonal_Dealer_Info = reader["Additonal_Dealer_Info"].ToString();
                    ecreditApplnDtls.Remarks = reader["Remarks"].ToString();
                    ecreditApplnDtls.ASD_Line_Codes = reader["ASD_Line_Codes"].ToString();
                    ecreditApplnDtls.Cash_Purchase_Value = reader["Cash_Purchase_Value"].ToString();
                    ecreditApplnDtls.Expected_Supplier_Codes = reader["Expected_Supplier_Codes"].ToString();
                    ecreditApplnDtls.Existing_Credit_Limit = reader["Existing_Credit_Limit"].ToString();
                    ecreditApplnDtls.Proposed_Credit_Limit = reader["Proposed_Credit_Limit"].ToString();
                    ecreditApplnDtls.Cash_Credit_Limit_Indicator = reader["Cash_Credit_Limit_Indicator"].ToString();
                    ecreditApplnDtls.Cr_Limit_Validity_Ind = reader["Validity_Indicator"].ToString();
                    ecreditApplnDtls.Cr_Limit_Due_Date = reader["Validity_Date"].ToString();
                    ecreditApplnDtls.FreightIndicator = reader["Freight_Indicator"].ToString();
                    ecreditApplnDtls.First_Time_Credit_Limit_Request = reader["First_Time_Credit_Limit_Request"].ToString();
                    ecreditApplnDtls.Outstanding_Amount = reader["Outstanding_Amount"].ToString();
                    ecreditApplnDtls.Bank_Name = reader["Bank_Name"].ToString();
                    ecreditApplnDtls.Bank_Branch = reader["Bank_Branch"].ToString();
                    ecreditApplnDtls.IFSC_Code = reader["IFSC_Code"].ToString();
                    ecreditApplnDtls.Name_of_Account_Holder = reader["Name_of_Account_Holder"].ToString();
                    ecreditApplnDtls.Debit_Credit_Card_Number = reader["Debit_Credit_Card_Number"].ToString();
                    ecreditApplnDtls.Expiry_Date = reader["Expiry_Date"].ToString();
                    ecreditApplnDtls.Payment_Mode = reader["Payment_Mode"].ToString();
                    ecreditApplnDtls.Branch_Approval_Status = reader["Branch_Approval_Status"].ToString();
                    ecreditApplnDtls.Branch_Approval_Date = reader["Branch_Approval_Date"].ToString();
                    ecreditApplnDtls.HO_Approval_Status = reader["HO_Approval_Status"].ToString();
                    ecreditApplnDtls.HO_Approval_Date = reader["HO_Approval_Date"].ToString();
                    ecreditApplnDtls.Approved_Credit_Limit = reader["Approved_Credit_Limit"].ToString();
                    ecreditApplnDtls.Date_of_Closure = reader["Date_of_Closure"].ToString();
                    ecreditApplnDtls.Reason_For_closure = reader["Reason_For_closure"].ToString();
                    ecreditApplnDtls.Written_Off_Amount = reader["Written_Off_Amount"].ToString();

                    lstECreditApplicationEntity.Add(ecreditApplnDtls);
                }
            }

            return lstECreditApplicationEntity;
        }

        public List<ECreditApplicationCust> GetEcreditApplicationDetails(string BranchCode, string ApplicationNo)
        {
            List<ECreditApplicationCust> lstECreditApplicationEntity = new List<ECreditApplicationCust>();

            ECreditApplicationCust ecreditApplnDtls = new ECreditApplicationCust();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetEcredit_Application_Details");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, ApplicationNo);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ecreditApplnDtls = new ECreditApplicationCust();
                    ecreditApplnDtls.Branch_Code = reader["Branch_code"].ToString();
                    ecreditApplnDtls.Form_Date = reader["Form_Date"].ToString();
                    ecreditApplnDtls.Indicator = reader["Indicator"].ToString();
                    ecreditApplnDtls.Customer_Code = reader["Customer_Code"].ToString();
                    ecreditApplnDtls.Customer_Name = reader["Customer_Name"].ToString();
                    ecreditApplnDtls.Customer_Address1 = reader["Customer_Address1"].ToString();
                    ecreditApplnDtls.Customer_Address2 = reader["Customer_Address2"].ToString();
                    ecreditApplnDtls.Customer_Address3 = reader["Customer_Address3"].ToString();
                    ecreditApplnDtls.Customer_Address4 = reader["Customer_Address4"].ToString();
                    ecreditApplnDtls.Proprietor_Name = reader["Proprietor_Name"].ToString();
                    ecreditApplnDtls.Proprietor_Mobile = reader["Proprietor_Mobile"].ToString();
                    ecreditApplnDtls.Contact_Person = reader["Contact_Person"].ToString();
                    ecreditApplnDtls.Contact_Person_Mobile = reader["Contact_Person_Mobile"].ToString();
                    ecreditApplnDtls.Group_Company_Codes = reader["Group_Company_Codes"].ToString();
                    ecreditApplnDtls.Migration_From_Branch_Code = reader["Migration_From_Branch_Code"].ToString();
                    ecreditApplnDtls.Migration_From_Customer_Code = reader["Migration_From_Customer_Code"].ToString();
                    ecreditApplnDtls.Year_Of_Establishment = reader["Year_Of_Establishment"].ToString();
                    ecreditApplnDtls.State_Code = reader["State_Code"].ToString();
                    ecreditApplnDtls.District_Code = reader["District_Code"].ToString();
                    ecreditApplnDtls.Town_Code = reader["Town_Code"].ToString();
                    ecreditApplnDtls.Town_Classification = reader["Town_Classification"].ToString();
                    ecreditApplnDtls.Phone_No = reader["Phone_No"].ToString();
                    ecreditApplnDtls.Mobile_No = reader["Mobile_No"].ToString();
                    ecreditApplnDtls.Location = reader["Location"].ToString();
                    ecreditApplnDtls.Email = reader["Email"].ToString();
                    ecreditApplnDtls.PinCode = reader["PinCode"].ToString();
                    ecreditApplnDtls.GSTIN_No = reader["GSTIN_No"].ToString();
                    ecreditApplnDtls.Type_of_Company = reader["Type_of_Company"].ToString();
                    ecreditApplnDtls.Type_of_Registration = reader["Type_of_Registration"].ToString();
                    ecreditApplnDtls.Turnover_Value = reader["Turnover_Value"].ToString();
                    ecreditApplnDtls.GSTIN_Location = reader["GSTIN_Location"].ToString();
                    ecreditApplnDtls.Stock_Value = reader["Stock_Value"].ToString();
                    ecreditApplnDtls.Impal_Turnover_Value = reader["Impal_Turnover_Value"].ToString();
                    ecreditApplnDtls.Representative_Type = reader["Representative_Type"].ToString();
                    ecreditApplnDtls.Distance_From_Branch = reader["Distance_From_Branch"].ToString();
                    ecreditApplnDtls.Distance_from_RR = reader["Distance_from_RR"].ToString();
                    ecreditApplnDtls.Travel_Classificaion = reader["Travel_Classificaion"].ToString();
                    ecreditApplnDtls.Salesman_code = reader["Salesman_code"].ToString();
                    ecreditApplnDtls.Dealer_Target = reader["Dealer_Target"].ToString();
                    ecreditApplnDtls.Period_of_Visit = reader["Period_of_Visit"].ToString();
                    ecreditApplnDtls.Classification = reader["Dealer_Classification"].ToString();
                    ecreditApplnDtls.Segment = reader["Dealer_Segment"].ToString();
                    ecreditApplnDtls.MultipleTown = reader["Dealer_Multi_Towns"].ToString();
                    ecreditApplnDtls.Supplier_code_From_Direct_Suppliers = reader["Supplier_code_From_Direct_Suppliers"].ToString();
                    ecreditApplnDtls.Dealing_with_Other_Group_Co = reader["Dealing_with_Other_Group_Co"].ToString();
                    ecreditApplnDtls.ASC_Line_Codes = reader["ASC_Line_Codes"].ToString();
                    ecreditApplnDtls.Additonal_Dealer_Info = reader["Additonal_Dealer_Info"].ToString();
                    ecreditApplnDtls.Transporter_Name = reader["Transporter_Name"].ToString();
                    ecreditApplnDtls.Remarks = reader["Remarks"].ToString();
                    ecreditApplnDtls.ASD_Line_Codes = reader["ASD_Line_Codes"].ToString();
                    ecreditApplnDtls.Cash_Purchase_Value = reader["Cash_Purchase_Value"].ToString();
                    ecreditApplnDtls.Expected_Supplier_Codes = reader["Expected_Supplier_Codes"].ToString();
                    ecreditApplnDtls.Outstanding_Amount = reader["Outstanding_Amount"].ToString();
                    ecreditApplnDtls.Existing_Credit_Limit = reader["Existing_Credit_Limit"].ToString();
                    ecreditApplnDtls.Proposed_Credit_Limit = reader["Proposed_Credit_Limit"].ToString();
                    ecreditApplnDtls.Cash_Credit_Limit_Indicator = reader["Cash_Credit_Limit_Indicator"].ToString();
                    ecreditApplnDtls.Cr_Limit_Validity_Ind = reader["Validity_Indicator"].ToString();
                    ecreditApplnDtls.Cr_Limit_Due_Date = reader["Validity_Date"].ToString();
                    ecreditApplnDtls.FreightIndicator = reader["Freight_Indicator"].ToString();
                    ecreditApplnDtls.First_Time_Credit_Limit_Request = reader["First_Time_Credit_Limit_Request"].ToString();
                    ecreditApplnDtls.Bank_Name = reader["Bank_Name"].ToString();
                    ecreditApplnDtls.Bank_Branch = reader["Bank_Branch"].ToString();
                    ecreditApplnDtls.IFSC_Code = reader["IFSC_Code"].ToString();
                    ecreditApplnDtls.Name_of_Account_Holder = reader["Name_of_Account_Holder"].ToString();
                    ecreditApplnDtls.Debit_Credit_Card_Number = reader["Debit_Credit_Card_Number"].ToString();
                    ecreditApplnDtls.Expiry_Date = reader["Expiry_Date"].ToString();
                    ecreditApplnDtls.Payment_Mode = reader["Payment_Mode"].ToString();
                    ecreditApplnDtls.Branch_Approval_Status = reader["Branch_Approval_Status"].ToString();
                    ecreditApplnDtls.Branch_Approval_Date = reader["Branch_Approval_Date"].ToString();
                    ecreditApplnDtls.HO_Approval_Status = reader["HO_Approval_Status"].ToString();
                    ecreditApplnDtls.HO_Approval_Date = reader["HO_Approval_Date"].ToString();
                    ecreditApplnDtls.Approved_Credit_Limit = reader["Approved_Credit_Limit"].ToString();
                    ecreditApplnDtls.Date_of_Closure = reader["Date_of_Closure"].ToString();
                    ecreditApplnDtls.Reason_For_closure = reader["Reason_For_closure"].ToString();
                    ecreditApplnDtls.Written_Off_Amount = reader["Written_Off_Amount"].ToString();
                    ecreditApplnDtls.HO_Approval_Status_Remarks = reader["HO_Approval_Status_Remarks"].ToString();

                    lstECreditApplicationEntity.Add(ecreditApplnDtls);
                }
            }

            return lstECreditApplicationEntity;
        }
    }
}
