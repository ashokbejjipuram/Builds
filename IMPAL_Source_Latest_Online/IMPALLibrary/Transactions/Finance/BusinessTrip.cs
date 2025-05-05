using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Transactions;

namespace IMPALLibrary
{
    public class BusinessTrip
    {
        public List<MiscHeader> GetDocumentNumber(string strBranch)
        {
            List<MiscHeader> lstDocumentNumber = new List<MiscHeader>();

            MiscHeader objDocumentNumber = new MiscHeader();
            objDocumentNumber.DocumentNumber = "0";
            objDocumentNumber.DocumentNumber = "-- Select --";
            lstDocumentNumber.Add(objDocumentNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string year = System.DateTime.Now.Year.ToString() + "%";
            string prevYear = Convert.ToString(DateTime.Today.Year - 1) + "%";

            string sSQL = "Select Document_Number from supplier_invoice_header ";
            sSQL = sSQL + " Where substring(branch_code,1,3) = '" + strBranch + "' and isnull(status,'A') in ('A','P') and jv_number like '" + year + "' or jv_number like '" + prevYear + "' order by JV_Number desc";
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))//(CommandType.StoredProcedure, "usp_GetCustomer"))
            {
                while (reader.Read())
                {
                    objDocumentNumber = new MiscHeader();
                    objDocumentNumber.DocumentNumber = reader[0].ToString();
                    lstDocumentNumber.Add(objDocumentNumber);
                }
            }

            return lstDocumentNumber;
        }

        public List<MiscHeader> GetMiscHeader(string strJVNumber, string strBranch)
        {
            List<MiscHeader> lstDocumentNumber = new List<MiscHeader>();

            MiscHeader objDocumentNumber = new MiscHeader();

            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = " Select Top 10 JVH.JV_Number, convert(nvarchar,JVH.JV_Date,103) document_date, ";
            sSQL = sSQL + " JVH.Reference_Document_Number , convert(nvarchar,JVH.Reference_Document_Date,103) reference_document_date,  ";
            sSQL = sSQL + " JVH.Reference_Document_Type, JVH.Narration, AP.description ";
            sSQL = sSQL + " From JV_Header JVH inner join Accounting_Period AP on JVH.JV_date between Start_date and End_date ";

            if (strJVNumber == "")
                sSQL = sSQL + " Where substring(JVH.branch_code,1,3) = '" + strBranch + "' and isnull(JVH.status,'A') in ('A') and JVH.JVNumber like '2013%' Order By JVDate desc";
            else
                sSQL = sSQL + " Where JVH.JV_Number= '" + strJVNumber + "'";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))//(CommandType.StoredProcedure, "usp_GetCustomer"))
            {
                while (reader.Read())
                {
                    objDocumentNumber = new MiscHeader();

                    objDocumentNumber.DocumentNumber = reader["JV_number"].ToString();
                    objDocumentNumber.DocumentDate = reader["document_date"].ToString();
                    objDocumentNumber.SupplierInvoiceNumber = reader["Reference_Document_Number"].ToString();
                    objDocumentNumber.SupplierInvoiceDate = reader["Reference_Document_Date"].ToString();
                    lstDocumentNumber.Add(objDocumentNumber);
                }
            }

            return lstDocumentNumber;
        }

        public List<MiscDetail> GetDocumentDetail(string strJVNumber)
        {
            List<MiscDetail> lstDocumentDetail = new List<MiscDetail>();

            MiscDetail objDocumentDetail = new MiscDetail();
            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = " Select JVH.JV_Number, JVD.Debit_Credit_Indicator, CAST(ROUND(JVD.Amount,2) AS NUMERIC(12,2)) Amount, ";
            sSQL = sSQL + " JVD.Serial_Number,JVD.Chart_of_Account_Code,JVD.Remarks, convert(nvarchar,JVD.Reference_Date,103) Reference_Date, ";
            sSQL = sSQL + " JVD.Document_Type, JVD.Reference_Number, GLA.Description ";
            sSQL = sSQL + "  from JV_header  JVH ";
            sSQL = sSQL + " Inner Join JV_Detail JVD On JVH.JV_Number = JVD.JV_Number ";
            sSQL = sSQL + " Left Outer Join GL_Account_Master GLA On substring(JVD.chart_of_account_Code, 4,3) = GLA.gl_main_code ";
            sSQL = sSQL + " and substring(JVD.chart_of_account_Code, 7,4) = GLA.gl_sub_code and substring(JVD.chart_of_account_Code, 11,7) = GLA.gl_account_code ";
            sSQL = sSQL + " Where JVH.JV_Number= '" + strJVNumber + "'";


            int count = 0;
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))//(CommandType.StoredProcedure, "usp_GetCustomer"))
            {
                while (reader.Read())
                {
                    count = count + 1;
                    objDocumentDetail = new MiscDetail();

                    objDocumentDetail.Chart_of_Account_Code = reader["Chart_of_Account_Code"].ToString();
                    objDocumentDetail.Description = reader["Description"].ToString();
                    objDocumentDetail.Discount = reader["Remarks"].ToString();
                    objDocumentDetail.Quantity = reader["Reference_Date"].ToString();
                    objDocumentDetail.Rate = reader["Document_Type"].ToString();
                    objDocumentDetail.UnitOfMeasurement = reader["Reference_Number"].ToString();
                    lstDocumentDetail.Add(objDocumentDetail);
                }
            }

            return lstDocumentDetail;
        }

        public string AddNewBusinessTripEntry(ref BusinessTripHeader businessTripHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            DataSet ds;
            string BusinessTripNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    foreach (BusinessTripDetail businessTripDetail in businessTripHeader.Items)
                    {
                        DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_Addbusinesstrip");

                        if (BusinessTripNumber != "")
                        {
                            ImpalDb.AddInParameter(cmd, "@Business_trip_number", DbType.String, BusinessTripNumber);
                        }
                        else
                        {
                            ImpalDb.AddInParameter(cmd, "@Business_trip_number", DbType.String, businessTripHeader.BusinessTripNumber);
                        }

                        ImpalDb.AddInParameter(cmd, "@Business_trip_date", DbType.String, businessTripHeader.BusinessTripDate);
                        ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, businessTripHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd, "@Name_of_the_employee", DbType.String, businessTripHeader.EmployeeName);
                        ImpalDb.AddInParameter(cmd, "@Date_of_travel", DbType.String, businessTripHeader.DateofTravel);
                        ImpalDb.AddInParameter(cmd, "@Travelling_from", DbType.String, businessTripHeader.TravellingFrom);
                        ImpalDb.AddInParameter(cmd, "@Travelling_to", DbType.String, businessTripHeader.TravellingTo);
                        ImpalDb.AddInParameter(cmd, "@Expected_date_of_arrival", DbType.String, businessTripHeader.ExpectedDateofArrival);
                        ImpalDb.AddInParameter(cmd, "@Date_of_Actual_arrival", DbType.String, businessTripHeader.DateofActualArrival);
                        ImpalDb.AddInParameter(cmd, "@Date_of_joining", DbType.String, businessTripHeader.DateofJoining);
                        ImpalDb.AddInParameter(cmd, "@Number_of_nights", DbType.Int16, businessTripHeader.NumberofNights);
                        ImpalDb.AddInParameter(cmd, "@Advance_paid", DbType.Decimal, businessTripHeader.AdvancePaid);
                        ImpalDb.AddInParameter(cmd, "@Remarks", DbType.String, businessTripHeader.Remarks);

                        ImpalDb.AddInParameter(cmd, "@Chart_of_account_code", DbType.String, businessTripDetail.Chart_of_Account_Code);
                        ImpalDb.AddInParameter(cmd, "@Amount_spent", DbType.String, businessTripDetail.Amount);
                        ImpalDb.AddInParameter(cmd, "@remarks1", DbType.String, businessTripDetail.Remarks);

                        ds = new DataSet();
                        ds = ImpalDb.ExecuteDataSet(cmd);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            BusinessTripNumber = ds.Tables[0].Rows[0][0].ToString();
                        }

                    }


                    if (BusinessTripNumber != "")
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_addglbt1");
                        ImpalDb.AddInParameter(cmd1, "@doc_no", DbType.String, BusinessTripNumber);
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, businessTripHeader.BranchCode);
                        result = ImpalDb.ExecuteNonQuery(cmd1);
                    }

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return BusinessTripNumber;

        }


        public List<Employee> LoadEmployee(string branchCode)
        {
            List<Employee> Employees = new List<Employee>();

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "Select Employee_Number,Employee_name from Employee";
            sSQL = sSQL + " Where branch_code = '" + branchCode + "' order by Employee_Number";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    Employees.Add(new Employee(reader["Employee_Number"].ToString(), reader["Employee_name"].ToString()));
                }
            }

            return Employees;
        }
    }

    public class BusinessTripHeader
    {
        public string BusinessTripNumber { get; set; }
        public string BusinessTripDate { get; set; }
        public string EmployeeName { get; set; }
        public string DateofTravel { get; set; }
        public string TravellingFrom { get; set; }
        public string TravellingTo { get; set; }
        public string ExpectedDateofArrival { get; set; }
        public string DateofActualArrival { get; set; }
        public string DateofJoining { get; set; }
        public string NumberofNights { get; set; }
        public string AdvancePaid { get; set; }
        public string Remarks { get; set; }
        public string BranchCode { get; set; }
        public List<BusinessTripDetail> Items { get; set; }

    }

    public class BusinessTripDetail
    {
        public string Chart_of_Account_Code { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
    }

    public class Employee
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }

        public Employee(string _EmployeeNumber, string _EmployeeName)
        {
            EmployeeNumber = _EmployeeNumber;
            EmployeeName = _EmployeeName;

        }
    }


}
