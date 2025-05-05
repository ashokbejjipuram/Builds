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

namespace IMPALLibrary
{
    #region This class is used for Branch Master database get/ins/update methods.
    [Serializable]
    public class Branch_Master
    {
        public Branch_Master()
        { 
        
        }

        public BranchMasterItems GetBranchDetails(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            BranchMasterItems objTrans = new BranchMasterItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetBranch");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Branch_Code = strBranchCode.ToString();
                        objTrans.Address = reader["Address"].ToString();
                        double strAmount=0.00D;
                        if (reader["Area_in_Square_Feet"].ToString() != "")
                        {
                            strAmount = Math.Round((Convert.ToDouble(reader["Area_in_Square_Feet"].ToString())), 2);
                            objTrans.Area_in_Square_Feet = string.Format((Convert.ToString(strAmount)), "#.00");
                        }
                        else
                            objTrans.Area_in_Square_Feet = "0.00";

                        strAmount = 0;
                       
                        objTrans.Branch_Accountant = reader["Branch_Accountant"].ToString();
                        objTrans.Branch_Carrier = reader["Branch_Carrier"].ToString();
                        objTrans.Branch_Name = reader["Branch_Name"].ToString();
                        objTrans.Branch_Destination = reader["Branch_Destination"].ToString();
                        objTrans.Branch_Manager = reader["Branch_Manager"].ToString();
                        objTrans.Central_sales_tax_number = reader["Central_sales_tax_number"].ToString();
                        objTrans.CESS_number = reader["CESS_number"].ToString();
                        objTrans.Classification_Code = reader["Classification_Code"].ToString();
                        objTrans.EDP_In_charge = reader["EDP_In_charge"].ToString();
                        objTrans.Email = reader["Email"].ToString();
                        objTrans.End_Date = reader["End_Date"].ToString();
                        objTrans.Fax = reader["Fax"].ToString();
                        objTrans.Local_sales_tax_number = reader["Local_sales_tax_number"].ToString();
                        objTrans.Min_Stock_Age_for_STDN = reader["Min_Stock_Age_for_STDN"].ToString();
                        if (reader["Min_Stock_Value_for_STDN"].ToString() != "")
                            strAmount = Math.Round((Convert.ToDouble(reader["Min_Stock_Value_for_STDN"].ToString())), 2);
                        else
                            strAmount = 0.00D; 
                        objTrans.Min_Stock_Value_for_STDN = string.Format((Convert.ToString(strAmount)), "#.00");
                        strAmount = 0;

                        if (reader["Monthly_Rent"].ToString() != "")
                            strAmount = Math.Round((Convert.ToDouble(reader["Monthly_Rent"].ToString())), 2);
                        else
                            strAmount = 0; 
                        objTrans.Monthly_Rent = string.Format((Convert.ToString(strAmount)), "#.00"); 

                        objTrans.Opening_Date = reader["Opening_Date"].ToString();
                        objTrans.OS_Cancellation_days = reader["OS_Cancellation_days"].ToString();
                        objTrans.OS_Lock = reader["OS_Lock"].ToString();
                        objTrans.Phone = reader["Phone"].ToString();
                        objTrans.Rental_Advance = reader["Rental_Advance"].ToString();
                        objTrans.Rental_Contract_End_Date = reader["Rental_Contract_End_Date"].ToString();
                        objTrans.Rental_Contract_Start_Date = reader["Rental_Contract_Start_Date"].ToString();
                        objTrans.Reporting_State_Code = reader["Reporting_State_Code"].ToString();

                        if (reader["Road_Permit_Indicator"].ToString() == "" || reader["Road_Permit_Indicator"].ToString() == "F")
                            objTrans.Road_Permit_Indicator = false;
                        else
                            objTrans.Road_Permit_Indicator = true;

                        objTrans.Status = reader["Status"].ToString();
                        objTrans.State = reader["State_Code"].ToString();
                        objTrans.Telex = reader["Telex"].ToString();
                        objTrans.Termination_Notice_Period = reader["Termination_Notice_Period"].ToString();
                        objTrans.tin = reader["tin"].ToString();
                        
                    }

                }

                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objTrans;
        }

        public int ChkInsBranchCode(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select 1 from Branch_master where Branch_code = '" + strBranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                iRowCount = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd));
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return iRowCount;        }

        public int InsBranchDetails(BranchMasterItems objInsValue)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;


            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addbranch");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objInsValue.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, objInsValue.Branch_Name);
                    ImpalDB.AddInParameter(cmd, "@Address", DbType.String, objInsValue.Address);

                    if (objInsValue.State == "")
                        ImpalDB.AddInParameter(cmd, "@State_Code", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@State_Code", DbType.Int32, Convert.ToInt32(objInsValue.State));

                    ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, objInsValue.Phone);
                    ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, objInsValue.Telex);
                    ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, objInsValue.Fax);
                    ImpalDB.AddInParameter(cmd, "@Email", DbType.String, objInsValue.Email);
                    ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.String, objInsValue.Classification_Code);
                    ImpalDB.AddInParameter(cmd, "@Branch_Manager", DbType.String, objInsValue.Branch_Manager);
                    ImpalDB.AddInParameter(cmd, "@Branch_Accountant", DbType.String, objInsValue.Branch_Accountant);
                    ImpalDB.AddInParameter(cmd, "@EDP_In_charge", DbType.String, objInsValue.EDP_In_charge);
                    ImpalDB.AddInParameter(cmd, "@Opening_Date", DbType.String, objInsValue.Opening_Date);
                    ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, objInsValue.End_Date);

                    if (objInsValue.Area_in_Square_Feet == "")
                        ImpalDB.AddInParameter(cmd, "@Area_in_Square_Feet", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Area_in_Square_Feet", DbType.Currency, Convert.ToDecimal(objInsValue.Area_in_Square_Feet));

                    if (objInsValue.Monthly_Rent == "")
                        ImpalDB.AddInParameter(cmd, "@Monthly_Rent", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Monthly_Rent", DbType.Currency, Convert.ToDecimal(objInsValue.Monthly_Rent));

                    if (objInsValue.Rental_Advance == "")
                        ImpalDB.AddInParameter(cmd, "@Rental_Advance", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Rental_Advance", DbType.Currency, Convert.ToDecimal(objInsValue.Rental_Advance));

                    ImpalDB.AddInParameter(cmd, "@Rental_Contract_Start_Date", DbType.String, objInsValue.Rental_Contract_Start_Date);
                    ImpalDB.AddInParameter(cmd, "@Rental_Contract_End_Date", DbType.String, objInsValue.Rental_Contract_End_Date);
                    ImpalDB.AddInParameter(cmd, "@Branch_Destination", DbType.String, objInsValue.Branch_Destination);
                    ImpalDB.AddInParameter(cmd, "@Branch_Carrier", DbType.String, objInsValue.Branch_Carrier);
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Indicator", DbType.String, objInsValue.Road_Permit_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, objInsValue.Status);

                    if (objInsValue.Termination_Notice_Period == "")
                        ImpalDB.AddInParameter(cmd, "@Termination_Notice_Period", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Termination_Notice_Period", DbType.Int32, Convert.ToInt32(objInsValue.Termination_Notice_Period));

                    if (objInsValue.Min_Stock_Age_for_STDN == "")
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Age_for_STDN", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Age_for_STDN", DbType.Int32, Convert.ToInt32(objInsValue.Min_Stock_Age_for_STDN));

                    if (objInsValue.Min_Stock_Value_for_STDN == "")
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Value_for_STDN", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Value_for_STDN", DbType.Currency, Convert.ToDecimal(objInsValue.Min_Stock_Value_for_STDN));

                    ImpalDB.AddInParameter(cmd, "@Local_sales_tax_number", DbType.String, objInsValue.Local_sales_tax_number);
                    ImpalDB.AddInParameter(cmd, "@Central_sales_tax_number", DbType.String, objInsValue.Central_sales_tax_number);
                    ImpalDB.AddInParameter(cmd, "@CESS_number", DbType.String, objInsValue.CESS_number);


                    if (objInsValue.Reporting_State_Code == "")
                        ImpalDB.AddInParameter(cmd, "@Reporting_State_Code", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Reporting_State_Code", DbType.Int32, Convert.ToInt32(objInsValue.Reporting_State_Code));

                    ImpalDB.AddInParameter(cmd, "@OS_lock", DbType.String, objInsValue.OS_Lock);

                    if (objInsValue.OS_Cancellation_days == "")
                        ImpalDB.AddInParameter(cmd, "@OS_Cancellation_days", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@OS_Cancellation_days", DbType.Int32, Convert.ToInt32(objInsValue.OS_Cancellation_days));

                    ImpalDB.AddInParameter(cmd, "@tin", DbType.String, objInsValue.tin);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return iRowCount;
        }

        public int UpdBranchDetails(BranchMasterItems objInsValue)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updbranch");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objInsValue.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Branch_Name", DbType.String, objInsValue.Branch_Name);
                    ImpalDB.AddInParameter(cmd, "@Address", DbType.String, objInsValue.Address);

                    if (objInsValue.State == "")
                        ImpalDB.AddInParameter(cmd, "@State_Code", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@State_Code", DbType.Int32, Convert.ToInt32(objInsValue.State));

                    ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, objInsValue.Phone);
                    ImpalDB.AddInParameter(cmd, "@Telex", DbType.String, objInsValue.Telex);
                    ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, objInsValue.Fax);
                    ImpalDB.AddInParameter(cmd, "@Email", DbType.String, objInsValue.Email);
                    ImpalDB.AddInParameter(cmd, "@Classification_Code", DbType.String, objInsValue.Classification_Code);
                    ImpalDB.AddInParameter(cmd, "@Branch_Manager", DbType.String, objInsValue.Branch_Manager);
                    ImpalDB.AddInParameter(cmd, "@Branch_Accountant", DbType.String, objInsValue.Branch_Accountant);
                    ImpalDB.AddInParameter(cmd, "@EDP_In_charge", DbType.String, objInsValue.EDP_In_charge);
                    ImpalDB.AddInParameter(cmd, "@Opening_Date", DbType.String, objInsValue.Opening_Date);
                    ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, objInsValue.End_Date);

                    if (objInsValue.Area_in_Square_Feet == "")
                        ImpalDB.AddInParameter(cmd, "@Area_in_Square_Feet", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Area_in_Square_Feet", DbType.Currency, Convert.ToDecimal(objInsValue.Area_in_Square_Feet));

                    if (objInsValue.Monthly_Rent == "")
                        ImpalDB.AddInParameter(cmd, "@Monthly_Rent", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Monthly_Rent", DbType.Currency, Convert.ToDecimal(objInsValue.Monthly_Rent));

                    if (objInsValue.Rental_Advance == "")
                        ImpalDB.AddInParameter(cmd, "@Rental_Advance", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Rental_Advance", DbType.Currency, Convert.ToDecimal(objInsValue.Rental_Advance));

                    ImpalDB.AddInParameter(cmd, "@Rental_Contract_Start_Date", DbType.String, objInsValue.Rental_Contract_Start_Date);
                    ImpalDB.AddInParameter(cmd, "@Rental_Contract_End_Date", DbType.String, objInsValue.Rental_Contract_End_Date);
                    ImpalDB.AddInParameter(cmd, "@Branch_Destination", DbType.String, objInsValue.Branch_Destination);
                    ImpalDB.AddInParameter(cmd, "@Branch_Carrier", DbType.String, objInsValue.Branch_Carrier);
                    ImpalDB.AddInParameter(cmd, "@Road_Permit_Indicator", DbType.String, objInsValue.Road_Permit_Indicator);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, objInsValue.Status);

                    if (objInsValue.Termination_Notice_Period == "")
                        ImpalDB.AddInParameter(cmd, "@Termination_Notice_Period", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Termination_Notice_Period", DbType.Int32, Convert.ToInt32(objInsValue.Termination_Notice_Period));

                    if (objInsValue.Min_Stock_Age_for_STDN == "")
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Age_for_STDN", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Age_for_STDN", DbType.Int32, Convert.ToInt32(objInsValue.Min_Stock_Age_for_STDN));

                    if (objInsValue.Min_Stock_Value_for_STDN == "")
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Value_for_STDN", DbType.Currency, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Min_Stock_Value_for_STDN", DbType.Currency, Convert.ToDecimal(objInsValue.Min_Stock_Value_for_STDN));

                    ImpalDB.AddInParameter(cmd, "@Local_sales_tax_number", DbType.String, objInsValue.Local_sales_tax_number);
                    ImpalDB.AddInParameter(cmd, "@Central_sales_tax_number", DbType.String, objInsValue.Central_sales_tax_number);
                    ImpalDB.AddInParameter(cmd, "@CESS_number", DbType.String, objInsValue.CESS_number);


                    if (objInsValue.Reporting_State_Code == "")
                        ImpalDB.AddInParameter(cmd, "@Reporting_State_Code", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@Reporting_State_Code", DbType.Int32, Convert.ToInt32(objInsValue.Reporting_State_Code));

                    ImpalDB.AddInParameter(cmd, "@OS_lock", DbType.String, objInsValue.OS_Lock);

                    if (objInsValue.OS_Cancellation_days == "")
                        ImpalDB.AddInParameter(cmd, "@OS_Cancellation_days", DbType.Int32, System.DBNull.Value);
                    else
                        ImpalDB.AddInParameter(cmd, "@OS_Cancellation_days", DbType.Int32, Convert.ToInt32(objInsValue.OS_Cancellation_days));

                    ImpalDB.AddInParameter(cmd, "@tin", DbType.String, objInsValue.tin);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return iRowCount;
        }
    }
    #endregion


    #region This class is used for Branch Master Custom-DataType
    [Serializable]
    public class BranchMasterItems
    {
        public BranchMasterItems()
        { }
        public string Branch_Code {get; set;}
        public string Branch_Name {get; set;}
        public string Address {get; set;}
        public string Phone {get; set;}
        public string Fax {get; set;}
        public string Telex {get; set;}
        public string Email {get; set;}
        public string Classification_Code {get; set;}
        public string Branch_Manager {get; set;}
        public string Branch_Accountant {get; set;}
        public string EDP_In_charge {get; set;}
        public string Opening_Date {get; set;}
        public string End_Date {get; set;}
        public string Area_in_Square_Feet {get; set;}
        public string Monthly_Rent {get; set;}
        public string Rental_Advance {get; set;}        
        public string Rental_Contract_Start_Date {get; set;}
        public string Rental_Contract_End_Date {get; set;}
        public string Branch_Destination {get; set;}
        public string Branch_Carrier {get; set;}
        public bool Road_Permit_Indicator {get; set;}
        public string Status {get; set;}
        public string State { get; set; }
        public string Termination_Notice_Period {get; set;}
        public string Min_Stock_Age_for_STDN {get; set;}
        public string Min_Stock_Value_for_STDN {get; set;}
        public string Local_sales_tax_number {get; set;}
        public string Central_sales_tax_number {get; set;}
        public string CESS_number {get; set;}
        public string Reporting_State_Code {get; set;}
        public string OS_Lock {get; set;}
        public string OS_Cancellation_days {get; set;}
        public string tin {get; set;}


    }
    #endregion
}
