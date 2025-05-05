using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Transactions;
using System.IO;

namespace IMPALLibrary
{
    public class Payable
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

        public List<ChequeSlipHeader> GetChequeSlipNumber()
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();

            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();
            objChequeSlipNumber.ChequeSlipNumber = "0";
            objChequeSlipNumber.ChequeSlipNumber = "-- Select --";
            lstChequeSlipNumber.Add(objChequeSlipNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select Cheque_Slip_Number from Corporate_Payment_Detail Where Status ='P' group by Cheque_Slip_Number Order by Cheque_Slip_Number Desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();
                    objChequeSlipNumber.ChequeSlipNumber = reader[0].ToString();
                    lstChequeSlipNumber.Add(objChequeSlipNumber);
                }
            }

            return lstChequeSlipNumber;
        }

        public List<ChequeSlipHeader> GetHoCCWHNumbers()
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();

            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();
            objChequeSlipNumber.HOREFNumber = "0";
            objChequeSlipNumber.HOREFNumber = "-- Select --";
            lstChequeSlipNumber.Add(objChequeSlipNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select distinct c.ccwh_number from Corporate_Payment_Detail c WITH (NOLOCK) inner join Accounting_Period a WITH (NOLOCK) on c.ccwh_date between convert(date,a.Start_Date,103) and convert(date,a.End_Date,103) and c.ccwh_number is NOT NULL and a.Start_Date>DATEADD(yy,-1,GETDATE()) order by ccwh_number desc";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();
                    objChequeSlipNumber.HOREFNumber = reader[0].ToString();
                    lstChequeSlipNumber.Add(objChequeSlipNumber);
                }
            }

            return lstChequeSlipNumber;
        }

        public List<ChequeSlipHeader> GetALLChequeSlipNumber()
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();

            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();
            objChequeSlipNumber.ChequeSlipNumber = "0";
            objChequeSlipNumber.ChequeSlipNumber = "-- Select --";
            lstChequeSlipNumber.Add(objChequeSlipNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select cheque_slip_number from cheque_slip_header order by cheque_slip_Number Desc ";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();
                    objChequeSlipNumber.ChequeSlipNumber = reader[0].ToString();
                    lstChequeSlipNumber.Add(objChequeSlipNumber);
                }
            }
            return lstChequeSlipNumber;
        }

        public List<ChequeSlipHeader> GetALLChequeSlipNumberBMS()
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();

            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();
            objChequeSlipNumber.ChequeSlipNumber = "0";
            objChequeSlipNumber.ChequeSlipNumber = "-- Select --";
            lstChequeSlipNumber.Add(objChequeSlipNumber);

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select distinct a.cheque_slip_number from cheque_slip_header a inner join BMS_Header b on a.cheque_Slip_Number = b.Transaction_Number order by cheque_slip_Number Desc ";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();
                    objChequeSlipNumber.ChequeSlipNumber = reader[0].ToString();
                    lstChequeSlipNumber.Add(objChequeSlipNumber);
                }
            }
            return lstChequeSlipNumber;
        }

        public List<GroupConversionMonYear> GroupConMonYear()
        {
            List<GroupConversionMonYear> lstMonthYear = new List<GroupConversionMonYear>();
            Database ImpalDB = DataAccess.GetDatabase();
            string Ssql = "SELECT RIGHT(CONVERT(varchar,getdate(),101),4)+LEFT(CONVERT(varchar,getdate(),101),2), LEFT(CONVERT(varchar,getdate(),101),2)+RIGHT(CONVERT(varchar,getdate(),101),4) UNION ALL ";
            Ssql += "SELECT RIGHT(CONVERT(varchar,dateadd(month, -1, GETDATE()),101),4)+LEFT(CONVERT(varchar,dateadd(month, -1, GETDATE()),101),2), LEFT(CONVERT(varchar,dateadd(month, -1, GETDATE()),101),2)+RIGHT(CONVERT(varchar,dateadd(month, -1, GETDATE()),101),4)";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstMonthYear.Add(new GroupConversionMonYear(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return lstMonthYear;
        }

        public List<ChequeSlipHeader> GetChequeSlipHeader(string strChequeSlipNumber)
        {
            List<ChequeSlipHeader> lstChequeSlipNumber = new List<ChequeSlipHeader>();
            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();
            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = " select Cheque_Slip_Number,convert(varchar(10),Cheque_Slip_Date,103) cqsldt ,Supplier_Code,Branch_Code,Cheque_Number,convert(varchar(10),Cheque_Date,103) cqdate ,Cheque_Amount,ccwh_number from corporate_Payment_Detail where cheque_slip_Number = '" + strChequeSlipNumber + "' ";
            sSQL = sSQL + " group by Cheque_Slip_Number,Cheque_Slip_Date,Supplier_Code,Branch_Code,Cheque_Number,Cheque_Date,Cheque_Amount,ccwh_number ";


            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();

                    objChequeSlipNumber.ChequeSlipNumber = reader["Cheque_Slip_Number"].ToString();
                    objChequeSlipNumber.ChequeSlipDate = reader["cqsldt"].ToString();
                    objChequeSlipNumber.SupplierCode = reader["Supplier_Code"].ToString();
                    objChequeSlipNumber.BranchCode = reader["Branch_Code"].ToString();
                    objChequeSlipNumber.ChequeNumber = reader["Cheque_Number"].ToString();
                    objChequeSlipNumber.ChequeDate = reader["cqdate"].ToString();
                    objChequeSlipNumber.ChequeAmount = reader["Cheque_Amount"].ToString();
                    objChequeSlipNumber.HOREFNumber = reader["ccwh_number"].ToString();
                    lstChequeSlipNumber.Add(objChequeSlipNumber);
                }
            }

            string sSQL1 = " select Chart_of_Account_Code,Remarks from Cheque_Slip_Header Where Cheque_Slip_Number = '" + strChequeSlipNumber + "' and supplier_Code ='" + lstChequeSlipNumber[0].SupplierCode + "'";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL1))
            {
                while (reader.Read())
                {
                    lstChequeSlipNumber[0].Chart_of_Account_Code = reader["Chart_of_Account_Code"].ToString();
                    lstChequeSlipNumber[0].Remarks = reader["Remarks"].ToString();
                }
            }

            string sSQL2 = " Select a.bank_name,b.Address from bank_master a, bank_branch_master b where a.bank_code = b.bank_code and b.chart_of_account_code = '" + lstChequeSlipNumber[0].Chart_of_Account_Code + "'";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL2))
            {
                while (reader.Read())
                {
                    lstChequeSlipNumber[0].ChequeBank = reader["bank_name"].ToString();
                    lstChequeSlipNumber[0].ChequeBranch = reader["Address"].ToString();
                }
            }

            return lstChequeSlipNumber;
        }

        public List<ChequeSlipDetail> GetChequeSlipDetail(string strChequeSlipNumber)
        {
            List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();

            ChequeSlipDetail objChequeSlipDetail = new ChequeSlipDetail();
            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = " select Branch_code,sum(isnull(Invoice_Amount,0)) +  sum(isnull(Credit_Note_Amount,0)) Amt from Corporate_Payment_Detail where   Cheque_Slip_Number  ='" + strChequeSlipNumber + "'  group by branch_code";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipDetail = new ChequeSlipDetail();
                    objChequeSlipDetail.Branch = reader["Branch_code"].ToString();
                    objChequeSlipDetail.Amount = Convert.ToDecimal(Convert.ToString(reader["Amt"]));
                    lstChequeSlipDetail.Add(objChequeSlipDetail);
                }
            }
            return lstChequeSlipDetail;
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

        public string AddNewMiscEntry(ref MiscHeader miscHeader)
        {
            Database ImpalDb = null;
            int result = 0;
            DataSet ds;
            string DocumentNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();

                    int i = 0;
                    foreach (MiscDetail miscDetail in miscHeader.Items)
                    {
                        DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_addmiscbills");
                        ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, miscHeader.DocumentNumber);
                        ImpalDb.AddInParameter(cmd, "@Document_Date", DbType.String, miscHeader.DocumentDate);
                        ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, miscHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, miscHeader.SupplierCode);
                        ImpalDb.AddInParameter(cmd, "@Supplier_Invoice_Number", DbType.String, miscHeader.SupplierInvoiceNumber);
                        ImpalDb.AddInParameter(cmd, "@Supplier_Invoice_Date", DbType.String, miscHeader.SupplierInvoiceDate);
                        ImpalDb.AddInParameter(cmd, "@Remarks", DbType.String, miscHeader.Remarks);
                        ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, miscHeader.InvoiceAmount);
                        ImpalDb.AddInParameter(cmd, "@Excise_Duty_Amount", DbType.String, miscHeader.ExciseDutyAmount);
                        ImpalDb.AddInParameter(cmd, "@Sales_Tax_Amount", DbType.String, miscHeader.SalesTaxAmount);
                        ImpalDb.AddInParameter(cmd, "@Other_Charges", DbType.String, miscHeader.OtherCharges);
                        ImpalDb.AddInParameter(cmd, "@Other_Deductions", DbType.String, miscHeader.OtherDeductions);
                        ImpalDb.AddInParameter(cmd, "@Advance_Amount", DbType.String, miscHeader.AdvanceAmount);
                        ImpalDb.AddInParameter(cmd, "@Description", DbType.String, miscDetail.Description);
                        ImpalDb.AddInParameter(cmd, "@Quantity", DbType.String, miscDetail.Quantity);
                        ImpalDb.AddInParameter(cmd, "@Unit_of_Measurement", DbType.String, miscDetail.UnitOfMeasurement);
                        ImpalDb.AddInParameter(cmd, "@Rate", DbType.String, miscDetail.Rate);
                        ImpalDb.AddInParameter(cmd, "@Discount", DbType.String, miscDetail.Discount);
                        ImpalDb.AddInParameter(cmd, "@Supplier_Name", DbType.String, miscHeader.SupplierName);
                        ImpalDb.AddInParameter(cmd, "@supplier_Place", DbType.String, miscHeader.SupplierPlace);
                        ImpalDb.AddInParameter(cmd, "@chartofaccount", DbType.String, miscDetail.Chart_of_Account_Code);

                        i++;

                        if (miscHeader.Items.Count == i)
                        {
                            ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, "T");
                        }
                        else
                        {
                            ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, "");
                        }

                        //result = ImpalDb.ExecuteNonQuery(cmd);
                        ds = new DataSet();
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ds = ImpalDb.ExecuteDataSet(cmd);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DocumentNumber = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }

                    if (DocumentNumber != "")
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addglmb1");
                        ImpalDb.AddInParameter(cmd1, "@doc_no", DbType.String, DocumentNumber);
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, miscHeader.BranchCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
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

            return DocumentNumber;
        }

        #region BMS Payment

        public List<ChequeSlipHeader> GetBMSPayment(string checkSlipNumber)
        {
            Database Impaldb = DataAccess.GetDatabase();
            List<ChequeSlipHeader> lstChequeSlipHeader = new List<ChequeSlipHeader>();
            ChequeSlipHeader objChequeSlipNumber = new ChequeSlipHeader();

            string sql = "select distinct a.cheque_Slip_Number,convert(varchar(10),a.Cheque_Slip_date,103) Cheque_Slip_date,a.branch_code,a.chart_of_account_code,a.cheque_Number,a.remarks, ";
            sql = sql + " convert(varchar(10),a.cheque_date,103) cheque_date,a.cheque_Amount, b.ccwh_Number,b.cheque_Bank,b.Cheque_Branch from bms_Header b,cheque_Slip_Header a ";
            sql = sql + " Where a.cheque_Slip_Number = b.Transaction_Number and a.cheque_Slip_Number = '" + checkSlipNumber + "'";

            using (IDataReader reader = Impaldb.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    objChequeSlipNumber = new ChequeSlipHeader();

                    objChequeSlipNumber.ChequeSlipNumber = reader["cheque_Slip_Number"].ToString();
                    objChequeSlipNumber.ChequeSlipDate = reader["Cheque_Slip_date"].ToString();
                    objChequeSlipNumber.BranchCode = reader["branch_code"].ToString();
                    objChequeSlipNumber.ChequeNumber = reader["cheque_Number"].ToString();
                    objChequeSlipNumber.ChequeDate = reader["cheque_date"].ToString();
                    objChequeSlipNumber.ChequeAmount = reader["cheque_Amount"].ToString();
                    objChequeSlipNumber.HOREFNumber = reader["ccwh_Number"].ToString();
                    objChequeSlipNumber.Chart_of_Account_Code = reader["chart_of_account_code"].ToString();
                    objChequeSlipNumber.Remarks = reader["remarks"].ToString();
                    objChequeSlipNumber.ChequeBank = reader["cheque_Bank"].ToString();
                    objChequeSlipNumber.ChequeBranch = reader["Cheque_Branch"].ToString();
                    lstChequeSlipHeader.Add(objChequeSlipNumber);
                }
            }

            return lstChequeSlipHeader;
        }

        public List<BMSPaymentAdviceDetail> GetBMSPaymentDetails(string hhorefNumber)
        {

            Database Impaldb = DataAccess.GetDatabase();

            string sql = "select BMS_number,BMS_Amount,convert(varchar(10),bms_date,103) Bms_Date,convert(varchar(10),BMS_Due_Date,103) BMS_Due_Date, no_of_days,Cd_Amount,Bms_Value from BMS_header ";
            sql = sql + " where ccwh_Number = '" + hhorefNumber + "' order by Bms_Number ASC ";

            DbCommand cmd1 = Impaldb.GetSqlStringCommand(sql);
            DataSet ds = Impaldb.ExecuteDataSet(cmd1);

            List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
            BMSPaymentAdviceDetail advanceItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    advanceItem = new BMSPaymentAdviceDetail();
                    advanceItem.BMSNumber = dr["BMS_number"].ToString();
                    advanceItem.BMSDate = dr["Bms_Date"].ToString();
                    advanceItem.BMSDueDate = dr["BMS_Due_Date"].ToString();
                    advanceItem.BMSAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["BMS_Amount"])));
                    advanceItem.CDAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Cd_Amount"])));
                    advanceItem.BMSValue = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Bms_Value"])));
                    advanceItem.NoofDays = dr["no_of_days"].ToString();
                    advanceItem.TotalValue += TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["BMS_Amount"]) + Convert.ToDecimal(dr["Cd_Amount"])));
                    advanceItem.Selected = true;
                    lstDetail.Add(advanceItem);
                }
            }
            return lstDetail;
        }

        public List<BMSPaymentAdviceDetail> GetBMSPaymentDetailsForInsert(string hhorefNumber)
        {

            Database Impaldb = DataAccess.GetDatabase();

            string sql = "select BMS_number,BMS_Amount,convert(varchar(10),bms_date,103) Bms_Date,convert(varchar(10),BMS_Due_Date,103) BMS_Due_Date,no_of_days,Cd_Amount,Bms_Value,Rate_Of_Interest from BMS_header  ";
            sql = sql + " where ccwh_Number = '" + hhorefNumber + "' and isnull(BMS_status,'NULL') <> 'P' order by Bms_Number ASC ";

            DbCommand cmd1 = Impaldb.GetSqlStringCommand(sql);
            DataSet ds = Impaldb.ExecuteDataSet(cmd1);

            List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
            BMSPaymentAdviceDetail advanceItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    advanceItem = new BMSPaymentAdviceDetail();
                    advanceItem.BMSNumber = dr["BMS_number"].ToString();
                    advanceItem.BMSDate = dr["Bms_Date"].ToString();
                    advanceItem.BMSDueDate = dr["BMS_Due_Date"].ToString();
                    advanceItem.BMSAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["BMS_Amount"])));
                    advanceItem.CDAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Cd_Amount"])));
                    advanceItem.BMSValue = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Bms_Value"])));
                    advanceItem.NoofDays = dr["no_of_days"].ToString();
                    advanceItem.TotalValue += TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["BMS_Amount"]) + Convert.ToDecimal(dr["Cd_Amount"])));
                    advanceItem.RateofInterest = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Rate_Of_Interest"])));
                    advanceItem.Selected = true;
                    lstDetail.Add(advanceItem);
                }
            }
            return lstDetail;
        }

        public ChequeSlipHeader GetBMSPaymentBankDetailsForInsert(string chartofAccountCode)
        {
            Database Impaldb = DataAccess.GetDatabase();
            string sql = "Select a.bank_name,b.Address from bank_master a, bank_branch_master b where a.bank_code = b.bank_code ";
            sql = sql + " and b.chart_of_account_code = '" + chartofAccountCode + "'";

            ChequeSlipHeader chequeSlipHeader = new ChequeSlipHeader();
            using (IDataReader reader = Impaldb.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    chequeSlipHeader.ChequeBank = reader["bank_name"].ToString();
                    chequeSlipHeader.ChequeBranch = reader["Address"].ToString();
                }
            }

            return chequeSlipHeader;
        }

        public string AddNewChequeSlip(ref ChequeSlipHeader chequeSlipHeader)
        {

            int result = 0;
            string ChequeSlipNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_addchequeslip1");
                    ImpalDb.AddInParameter(cmd, "@Cheque_Slip_Date", DbType.String, chequeSlipHeader.ChequeSlipDate);
                    ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, chequeSlipHeader.SupplierCode);
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, chequeSlipHeader.BranchCode);
                    ImpalDb.AddInParameter(cmd, "@cacode", DbType.String, chequeSlipHeader.Chart_of_Account_Code);
                    ImpalDb.AddInParameter(cmd, "@chequeno", DbType.String, chequeSlipHeader.ChequeNumber);
                    ImpalDb.AddInParameter(cmd, "@chequedate", DbType.String, chequeSlipHeader.ChequeDate);
                    ImpalDb.AddInParameter(cmd, "@remarks", DbType.String, chequeSlipHeader.Remarks);
                    ImpalDb.AddInParameter(cmd, "@chequebank", DbType.String, chequeSlipHeader.ChequeBank);
                    ImpalDb.AddInParameter(cmd, "@chequebranch", DbType.String, chequeSlipHeader.ChequeBranch);
                    ImpalDb.AddInParameter(cmd, "@chequeamt", DbType.Decimal, Convert.ToDecimal(chequeSlipHeader.ChequeAmount));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ChequeSlipNumber = ImpalDb.ExecuteScalar(cmd).ToString();

                    foreach (ChequeSlipDetail chequeSlipDetail in chequeSlipHeader.Items)
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addchequeslip2_Bms");
                        ImpalDb.AddInParameter(cmd1, "@Cheque_Slip_Number", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd1, "@Serial_Number", DbType.Int32, chequeSlipDetail.SerialNo);
                        ImpalDb.AddInParameter(cmd1, "@Reference_Document_Number", DbType.String, chequeSlipDetail.ReferenceDocumentNumber);
                        ImpalDb.AddInParameter(cmd1, "@Reference_Type", DbType.String, "BMS");
                        ImpalDb.AddInParameter(cmd1, "@Amount", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.Amount));
                        ImpalDb.AddInParameter(cmd1, "@Reference_Document_Date", DbType.String, chequeSlipDetail.ReferenceDocumentDate);
                        ImpalDb.AddInParameter(cmd1, "@Document_Value", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.DocumentValue));
                        ImpalDb.AddInParameter(cmd1, "@cd_value", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.CDValue));
                        ImpalDb.AddInParameter(cmd1, "@branch_code", DbType.String, chequeSlipHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd1, "@cd_percentage", DbType.Int32, Convert.ToInt32(chequeSlipDetail.CDPercentage));
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd1);

                        DbCommand cmd2 = ImpalDb.GetStoredProcCommand("usp_updBmsHeader");
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Slip_Number", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd2, "@Serial_Number", DbType.Int32, chequeSlipDetail.SerialNo);
                        ImpalDb.AddInParameter(cmd2, "@Reference_Document_Number", DbType.String, chequeSlipDetail.ReferenceDocumentNumber);
                        ImpalDb.AddInParameter(cmd2, "@Reference_Type", DbType.String, "BMS");
                        ImpalDb.AddInParameter(cmd2, "@Amount", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.Amount));
                        ImpalDb.AddInParameter(cmd2, "@Reference_Document_Date", DbType.String, chequeSlipDetail.ReferenceDocumentDate);
                        ImpalDb.AddInParameter(cmd2, "@Document_Value", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.DocumentValue));
                        ImpalDb.AddInParameter(cmd2, "@cd_value", DbType.Decimal, Convert.ToDecimal(chequeSlipDetail.CDValue));
                        ImpalDb.AddInParameter(cmd2, "@branch_code", DbType.String, chequeSlipHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd2, "@cd_percentage", DbType.Int32, Convert.ToInt32(chequeSlipDetail.CDPercentage));
                        ImpalDb.AddInParameter(cmd2, "@cheque_bank", DbType.String, chequeSlipHeader.ChequeBank);
                        ImpalDb.AddInParameter(cmd2, "@cheque_Branch", DbType.String, chequeSlipHeader.ChequeBranch);
                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd2);
                    }

                    if (ChequeSlipNumber != "")
                    {
                        DbCommand cmd3 = ImpalDb.GetStoredProcCommand("usp_addglcq1");
                        ImpalDb.AddInParameter(cmd3, "@doc_no", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd3, "@Branch_Code", DbType.String, chequeSlipHeader.BranchCode);
                        cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                        result = ImpalDb.ExecuteNonQuery(cmd3);
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

            return ChequeSlipNumber;
        }

        #endregion BMS Payment

        #region BMS Payment Advice
        public List<BMSPaymentAdviceDetail> GetBMSPaymentAdvice(string supplierCode, string FromDate, string ToDate)
        {
            Database Impaldb = DataAccess.GetDatabase();
            DbCommand cmd = Impaldb.GetStoredProcCommand("UsP_Bms_Pymt_Advice");
            Impaldb.AddInParameter(cmd, "@Supplier_Code", DbType.String, supplierCode);
            Impaldb.AddInParameter(cmd, "@FromDate", DbType.String, FromDate);
            Impaldb.AddInParameter(cmd, "@toDate", DbType.String, ToDate);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Impaldb.ExecuteNonQuery(cmd);

            DbCommand cmd1 = Impaldb.GetSqlStringCommand("select bms_Number,convert(varchar(10),Bms_Date,103) Bms_Date ,convert(varchar(10),Bms_Due_Date,103) Bms_Due_Date ,isnull(Bms_Amount,0) Bms_Amount,isnull(cd_Amount,0) cd_Amount,isnull(Bms_Value,0) Bms_Value,no_of_Days from BMS_header_Pymt_Advice where convert(datetime,BMS_date,103) >=  convert(datetime," + "'" + FromDate + "'" + ",103)" + " and convert(datetime,BMS_date,103) <= convert(datetime," + "'" + ToDate + "'" + ",103) and supplier_code ='" + supplierCode + "'");
            DataSet ds = Impaldb.ExecuteDataSet(cmd1);

            List<BMSPaymentAdviceDetail> lstDetail = new List<BMSPaymentAdviceDetail>();
            BMSPaymentAdviceDetail advanceItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    advanceItem = new BMSPaymentAdviceDetail();
                    advanceItem.BMSNumber = dr["bms_Number"].ToString();
                    advanceItem.BMSDate = dr["Bms_Date"].ToString();
                    advanceItem.BMSDueDate = dr["Bms_Due_Date"].ToString();
                    advanceItem.BMSAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Bms_Amount"])));
                    advanceItem.CDAmount = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["cd_Amount"])));
                    advanceItem.BMSValue = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Bms_Value"])));
                    advanceItem.NoofDays = dr["no_of_Days"].ToString();
                    advanceItem.TotalValue += TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(dr["Bms_Amount"]) + Convert.ToDecimal(dr["cd_Amount"])));
                    advanceItem.Selected = true;
                    lstDetail.Add(advanceItem);
                }
            }

            return lstDetail;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public string UpdateBMSHeaderPymtAdvice(ref BMSPaymentAdviceDetails bmsPaymentAdviceDetails, string branchCode, string supplierCode)
        {

            string BMSCCWHNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();

                    foreach (BMSPaymentAdviceDetail bmsPaymentAdviceDetail in bmsPaymentAdviceDetails.bmsPaymentAdviceDetail)
                    {
                        if (bmsPaymentAdviceDetail.Selected == true)
                        {
                            DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_BMS_header_Pymt_Advice_Update");
                            ImpalDb.AddInParameter(cmd, "@Bms_Number", DbType.String, bmsPaymentAdviceDetail.BMSNumber);
                            ImpalDb.AddInParameter(cmd, "@Branch_code", DbType.String, "");
                            ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, "");
                            ImpalDb.AddInParameter(cmd, "@status", DbType.String, "N");
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd);
                        }
                    }

                    DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_BMS_header_Pymt_Advice_Update");
                    ImpalDb.AddInParameter(cmd1, "@Bms_Number", DbType.String, "");
                    ImpalDb.AddInParameter(cmd1, "@Branch_code", DbType.String, branchCode);
                    ImpalDb.AddInParameter(cmd1, "@Supplier_Code", DbType.String, supplierCode);
                    ImpalDb.AddInParameter(cmd1, "@status", DbType.String, "Y");
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    BMSCCWHNumber = ImpalDb.ExecuteScalar(cmd1).ToString();

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return BMSCCWHNumber;
        }
        #endregion BMS Payment Advice

        #region BMS

        public bool CheckExists(string bmsNumber)
        {
            bool IsExists = false;

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select count(isnull(bms_number,0)) from bms_header where bms_number = '" + bmsNumber + "'";

            DbCommand cmd = ImpalDb.GetSqlStringCommand(sSQL);
            object ds = ImpalDb.ExecuteScalar(cmd);

            if (Convert.ToInt32(ds) > 0)
            {
                IsExists = true;
            }
            return IsExists;
        }


        public List<BMS> GetHOBMSNumber()
        {
            List<BMS> lstBMSNumber = new List<BMS>();

            BMS objBMSNumber = new BMS();
            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select ho_bms_number from bms_header order by ho_bms_number desc ";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objBMSNumber = new BMS();
                    objBMSNumber.BMSNumber = reader[0].ToString();
                    lstBMSNumber.Add(objBMSNumber);
                }
            }

            return lstBMSNumber;
        }

        public string AddNewBMSEntry(ref BMS bms)
        {
            Database ImpalDb = null;
            DataSet ds;
            string DocumentNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_AddBMS");
                    ImpalDb.AddInParameter(cmd, "@BMS_Number", DbType.String, bms.BMSNumber);
                    ImpalDb.AddInParameter(cmd, "@BMS_Date", DbType.String, bms.BMSDate);
                    ImpalDb.AddInParameter(cmd, "@BMS_Due_Date", DbType.String, bms.BMSDueDate);
                    ImpalDb.AddInParameter(cmd, "@Bms_Amount", DbType.Decimal, bms.BMSAmount);
                    ImpalDb.AddInParameter(cmd, "@Bms_Bank_Name", DbType.String, bms.BMSBankName);
                    ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, bms.SupplierCode);
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, bms.BranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    //result = ImpalDb.ExecuteNonQuery(cmd);
                    ds = new DataSet();
                    ds = ImpalDb.ExecuteDataSet(cmd);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DocumentNumber = ds.Tables[0].Rows[0][0].ToString();
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DocumentNumber;
        }

        public int UpdateBMSEntry(ref BMS bms)
        {
            Database ImpalDb = null;
            int result = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_UpdBMS");
                    ImpalDb.AddInParameter(cmd, "@Ho_Bms_Number", DbType.String, bms.HOBMSNumber);
                    ImpalDb.AddInParameter(cmd, "@BMS_Number", DbType.String, bms.BMSNumber);
                    ImpalDb.AddInParameter(cmd, "@BMS_Date", DbType.String, bms.BMSDate);
                    ImpalDb.AddInParameter(cmd, "@BMS_Due_Date", DbType.String, bms.BMSDueDate);
                    ImpalDb.AddInParameter(cmd, "@Bms_Amount", DbType.Decimal, bms.BMSAmount);
                    ImpalDb.AddInParameter(cmd, "@Bms_Bank_Name", DbType.String, bms.BMSBankName);
                    ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, bms.SupplierCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    result = ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }
            return result;
        }

        public List<BMS> GetBMSHeader(string strBMSNumber)
        {
            List<BMS> lstBMSNumber = new List<BMS>();
            BMS objBMSNumber = new BMS();
            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = " select BMS_Number,Supplier_Code ,convert(varchar(10),BMS_Date,103) bms_date,convert(varchar(10),BMS_Due_Date,103) bms_due_date,BMS_Bank_Name,Bms_Amount, Bms_Status , Transaction_Number , convert(varchar(10),Transaction_Date,103) Transaction_Date from bms_header where ho_bms_number ='" + strBMSNumber + "'";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objBMSNumber = new BMS();
                    objBMSNumber.BMSNumber = reader["BMS_Number"].ToString();
                    objBMSNumber.BMSDate = reader["bms_date"].ToString();
                    objBMSNumber.SupplierCode = reader["Supplier_Code"].ToString();
                    objBMSNumber.BMSDueDate = reader["bms_due_date"].ToString();
                    objBMSNumber.BMSBankName = reader["BMS_Bank_Name"].ToString();
                    objBMSNumber.BMSAmount = Convert.ToDecimal(reader["Bms_Amount"]);
                    objBMSNumber.BMSStatus = reader["Bms_Status"].ToString();
                    objBMSNumber.TransactionNumber = reader["Transaction_Number"].ToString();
                    objBMSNumber.TransactionDate = reader["Transaction_Date"].ToString();
                    lstBMSNumber.Add(objBMSNumber);
                }
            }

            return lstBMSNumber;
        }

        #endregion BMS

        #region Update Payment Process Common
        public void UpdateHOPaymentProcess(ref PaymentProcessTempForBranches pymtProcessTemp, string branchCode, string SuppCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (PaymentProcessTempForBranch pymtProcess in pymtProcessTemp.paymentProcessTempForBranch)
                    {
                        if (pymtProcess.Selected == true)
                        {
                            Database ImpalDb = DataAccess.GetDatabase();
                            DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Temp");
                            ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, pymtProcess.Supplier);
                            ImpalDb.AddInParameter(cmd, "@Branch", DbType.String, pymtProcess.Branch);
                            ImpalDb.AddInParameter(cmd, "@BranchAmt", DbType.Decimal, pymtProcess.BranchAmount);
                            ImpalDb.AddInParameter(cmd, "@from_Date", DbType.String, pymtProcess.FromDate);
                            ImpalDb.AddInParameter(cmd, "@To_date", DbType.String, pymtProcess.ToDate);
                            ImpalDb.AddInParameter(cmd, "@Zone", DbType.String, pymtProcess.Zone);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public string SubmitStatementPayment(string BranchCode)
        {
            string result = string.Empty;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    string sSql = string.Empty;

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_ADD_Corporate_Supplier_InvoicePayment_Statement");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    result = "0";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = "Error";
                throw exp;
            }

            return result;
        }

        #region Payment Process Common
        public string SubmitHOPaymentProcess(ref PaymentProcessTempForBranches pymtProcessTemp, string branchCode, string SuppCode, string TotalAmt)
        {
            string HoCCWHNumber = "";

            if (TotalAmt == "")
                TotalAmt = "0";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    //foreach (PaymentProcessTempForBranch pymtProcess in pymtProcessTemp.paymentProcessTempForBranch)
                    //{
                    //    if (pymtProcess.Selected == true)
                    //    {
                    if (Convert.ToDouble(TotalAmt) > 0)
                    {
                        Database ImpalDb = DataAccess.GetDatabase();
                        DbCommand cmd = ImpalDb.GetSqlStringCommand("delete from payment_process_ZoneWise Where Select_Status = 'Z'");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd);

                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Conversion");
                        ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, branchCode);
                        ImpalDb.AddInParameter(cmd1, "@Supplier_Code", DbType.String, SuppCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        HoCCWHNumber = ImpalDb.ExecuteScalar(cmd1).ToString();
                    }
                    //    }
                    //}

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return HoCCWHNumber;
        }
        #endregion Payment Process Common

        #region Update Payment Process Common
        public void UpdateHOSFLPaymentProcess(ref PaymentProcessTempForBranches pymtProcessTemp, string branchCode, string SuppCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (PaymentProcessTempForBranch pymtProcess in pymtProcessTemp.paymentProcessTempForBranch)
                    {
                        if (pymtProcess.Selected == true)
                        {
                            Database ImpalDb = DataAccess.GetDatabase();
                            DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Temp");
                            ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, pymtProcess.Supplier);
                            ImpalDb.AddInParameter(cmd, "@Branch", DbType.String, pymtProcess.Branch);
                            ImpalDb.AddInParameter(cmd, "@BranchAmt", DbType.Decimal, pymtProcess.BranchAmount);
                            ImpalDb.AddInParameter(cmd, "@from_Date", DbType.String, pymtProcess.FromDate);
                            ImpalDb.AddInParameter(cmd, "@To_date", DbType.String, pymtProcess.ToDate);
                            ImpalDb.AddInParameter(cmd, "@Zone", DbType.String, pymtProcess.Zone);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Payment Process Common
        public string SubmitHOSFLPaymentProcess(ref PaymentProcessTempForBranches pymtProcessTemp, string branchCode, string SuppCode)
        {
            string HoCCWHNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (PaymentProcessTempForBranch pymtProcess in pymtProcessTemp.paymentProcessTempForBranch)
                    {
                        if (pymtProcess.Selected == true)
                        {
                            Database ImpalDb = DataAccess.GetDatabase();
                            DbCommand cmd = ImpalDb.GetSqlStringCommand("delete from payment_process_ZoneWise Where Select_Status = 'Z' and Branch_Code = '" + branchCode + "'");
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd);

                            DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_SFLPayment_Process_Conversion");
                            ImpalDb.AddInParameter(cmd1, "@Branch_Code", DbType.String, branchCode);
                            ImpalDb.AddInParameter(cmd1, "@Supplier_Code", DbType.String, SuppCode);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            HoCCWHNumber = ImpalDb.ExecuteScalar(cmd1).ToString();
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return HoCCWHNumber;
        }
        #endregion Payment Process Common

        #region Liability Payment

        public List<PaymentProcessTemp> LoadCorporatePaymentBranchTotalTemp(string supplier, string invoiceDate1, string invoiceDate2, string zoneCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Zone_Temp");
                    ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd, "@from_Date", DbType.String, invoiceDate1);
                    ImpalDb.AddInParameter(cmd, "@To_date", DbType.String, invoiceDate2);
                    ImpalDb.AddInParameter(cmd, "@Zone_Code", DbType.String, zoneCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_corporate_Pymt_BranchTotal_Temp");
                    ImpalDb.AddInParameter(cmd1, "@supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt1", DbType.String, invoiceDate1);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt2", DbType.String, invoiceDate2);
                    ImpalDb.AddInParameter(cmd1, "@zoneCode", DbType.String, zoneCode);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd1);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.BranchCode = dr["Branch_Code"].ToString();
                            pymtItem.InvoiceAmount = Convert.ToDecimal(dr["Column1"]);
                            pymtItem.CDAmount = Convert.ToDecimal(dr["Column2"]);
                            lstDetail.Add(pymtItem);
                        }

                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BankNames> GetBankNamesCorp()
        {
            List<BankNames> lstBankNames = new List<BankNames>();

            BankNames objBankNames = new BankNames();
            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select distinct Bank_code,Bank_Name from Bank_Master order by Bank_Name";
            lstBankNames.Add(new BankNames(0, ""));
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objBankNames = new BankNames();
                    objBankNames.BankCode = Convert.ToInt16(reader[0].ToString());
                    objBankNames.BankName = reader[1].ToString();
                    lstBankNames.Add(objBankNames);
                }
            }

            return lstBankNames;
        }

        public List<PaymentProcessTemp> LoadCorporateLiabilityPaymentBranchTotalTempInvoice(string supplier, string invoiceFromDate, string invoiceToDate, string branchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    //DbCommand cmd = ImpalDb.GetSqlStringCommand("truncate table payment_process_BranchWise");
                    //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    //ImpalDb.ExecuteNonQuery(cmd);

                    //string sql = "insert into Payment_Process_BranchWise Select * from Payment_Process_ZoneWise Where branch_code = '" + branchCode + "' and Supplier_Code = '" + supplier + "'";
                    //sql = sql + " and convert(datetime,Inward_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime ,Inward_date,103) <= convert(datetime, '" + invoiceToDate + "', 103) ";
                    //sql = sql + " and Select_Status ='Z'";
                    //DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sql);
                    //cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    //ImpalDb.ExecuteNonQuery(cmd1);

                    //string sqlquery = "select invoice_number,convert(varchar(10),invoice_date,103) invoice_date,isnull(invoice_amount,0) invoice_amount,Da_Ca_Indicator,isnull(Credit_Note_Amount,0) Credit_Note_Amount,isnull(Cash_Discount,0) cd_Amount from Payment_Process_ZoneWise Where Supplier_code='" + supplier + "' and convert(datetime,Inward_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime,Inward_date,103) <= convert(datetime,'" + invoiceToDate + "',103) and branch_code='" + branchCode + "' and Select_Status ='Z'";
                    //DbCommand cmd2 = ImpalDb.GetSqlStringCommand(sqlquery);
                    //cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    //DataSet ds = ImpalDb.ExecuteDataSet(cmd2);

                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Branch_Temp_InvWise");
                    ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd, "@invoicedt1", DbType.String, invoiceFromDate);
                    ImpalDb.AddInParameter(cmd, "@invoicedt2", DbType.String, invoiceToDate);
                    ImpalDb.AddInParameter(cmd, "@BranchCode", DbType.String, branchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.Selected = true;
                            pymtItem.InvoiceNo = dr["invoice_number"].ToString();
                            pymtItem.InvoiceDate = dr["invoice_date"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2);
                            }
                            else
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.Indicator = dr["Da_Ca_Indicator"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2) - Math.Round(Convert.ToDecimal(dr["cd_Amount"]), 2); ;
                            }
                            else
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.CDAmount = Math.Round(Convert.ToDecimal(dr["cd_Amount"]), 2);
                            lstDetail.Add(pymtItem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentProcessTemp> LoadCorporateHOPaymentBranchTotalTempInvoice(string supplier, string invoiceFromDate, string invoiceToDate, string branchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetSqlStringCommand("truncate table payment_process_BranchWise where Branch_Code = '" + branchCode + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    string sql = "insert into Payment_Process_BranchWise Select * from Payment_Process_ZoneWise Where branch_code = '" + branchCode + "' and Supplier_Code = '" + supplier + "'";
                    sql = sql + " and convert(datetime,Invoice_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime,Invoice_date,103) <= convert(datetime, '" + invoiceToDate + "', 103) ";
                    sql = sql + " and Select_Status ='Z'";
                    DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sql);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);

                    string sqlquery = "select invoice_number,convert(varchar(10),invoice_date,103) invoice_date,isnull(invoice_amount,0) invoice_amount,Da_Ca_Indicator,isnull(Credit_Note_Amount,0) Credit_Note_Amount,isnull(Cash_Discount,0) cd_Amount from Payment_Process_ZoneWise Where Supplier_code='" + supplier + "' and convert(datetime,Invoice_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime,Invoice_date,103) <= convert(datetime,'" + invoiceToDate + "',103) and branch_code='" + branchCode + "' and Select_Status ='Z'";
                    DbCommand cmd2 = ImpalDb.GetSqlStringCommand(sqlquery);
                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd2);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.Selected = true;
                            pymtItem.InvoiceNo = dr["invoice_number"].ToString();
                            pymtItem.InvoiceDate = dr["invoice_date"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2);
                            }
                            else
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.Indicator = dr["Da_Ca_Indicator"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2) - Math.Round(Convert.ToDecimal(dr["cd_Amount"]), 2); ;
                            }
                            else
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.CDAmount = Math.Round(Convert.ToDecimal(dr["cd_Amount"]), 2);
                            lstDetail.Add(pymtItem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentProcessTemp> LoadCorporateSFLPaymentBranchTotalTempInvoice(string supplier, string invoiceFromDate, string invoiceToDate, string branchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetSqlStringCommand("delete from payment_process_BranchWise where Branch_Code = '" + branchCode + "'");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    string sql = "insert into Payment_Process_BranchWise Select * from Payment_Process_ZoneWise Where branch_code = '" + branchCode + "' and Supplier_Code = '" + supplier + "'";
                    sql = sql + " and convert(datetime,Invoice_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime ,Invoice_date,103) <= convert(datetime, '" + invoiceToDate + "', 103) ";
                    sql = sql + " and Select_Status ='Z'";
                    DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sql);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);

                    string sqlquery = "update Payment_Process_ZoneWise set BMS_Date=case when zone_code <> 3 AND inward_date+37>invoice_Date+45 then inward_date+37 when zone_code <> 3 AND inward_date+37<=invoice_Date+45 then invoice_Date+45 when zone_code = 3 AND inward_date+45>invoice_Date+60 then inward_date+45 when zone_code = 3 AND inward_date+45<=invoice_Date+60 then invoice_Date+60 end Where Supplier_code='" + supplier + "' and convert(datetime,Invoice_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime,Invoice_date,103) <= convert(datetime,'" + invoiceToDate + "',103) and branch_code='" + branchCode + "' and Select_Status ='Z'";
                    DbCommand cmd2 = ImpalDb.GetSqlStringCommand(sqlquery);
                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd2);

                    string sqlquery1 = "select invoice_number,convert(varchar(10),invoice_date,103) invoice_date,isnull(invoice_amount,0) invoice_amount,Da_Ca_Indicator,isnull(Credit_Note_Amount,0) Credit_Note_Amount,";
                    sqlquery1 = sqlquery1 + " case when DATEDIFF(dd,GETDATE(),BMS_Date)<=0 then 0 else (isnull(invoice_amount,0)*0.09)*DATEDIFF(dd,GETDATE(),BMS_Date)/365 end CD_Amount,DATEDIFF(dd,GETDATE(),BMS_Date) CD_Days from Payment_Process_ZoneWise Where branch_code='" + branchCode + "' and Supplier_code='" + supplier + "'";
                    sqlquery1 = sqlquery1 + " and convert(datetime,Invoice_date,103) >= convert(datetime,'" + invoiceFromDate + "',103) and convert(datetime,Invoice_date,103) <= convert(datetime,'" + invoiceToDate + "',103) and Select_Status ='Z'";
                    DbCommand cmd3 = ImpalDb.GetSqlStringCommand(sqlquery1);
                    cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd3);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.Selected = true;
                            pymtItem.InvoiceNo = dr["invoice_number"].ToString();
                            pymtItem.InvoiceDate = dr["invoice_date"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2);
                            }
                            else
                            {
                                pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.Indicator = dr["Da_Ca_Indicator"].ToString();
                            if (Convert.ToInt32(dr["invoice_amount"]) > 0)
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["invoice_amount"]), 2) - Math.Round(Convert.ToDecimal(dr["cd_Amount"]), 2); ;
                            }
                            else
                            {
                                pymtItem.InvoiceValue = Math.Round(Convert.ToDecimal(dr["Credit_Note_Amount"]), 2);
                            }
                            pymtItem.CDAmount = Math.Round(Convert.ToDecimal(dr["CD_Amount"]), 2);
                            pymtItem.CDDays = Math.Round(Convert.ToDecimal(dr["CD_Days"]), 2);
                            lstDetail.Add(pymtItem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCorporatePaymentBranchTotalTempInvoice(string supplier, string invoiceNoCollection, string branchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    string sql = "";

                    if (invoiceNoCollection == "")
                    {
                        sql = "update payment_Process_ZoneWise set Select_Status ='B' Where Supplier_code = '" + supplier + "'";
                        sql = sql + " and branch_code ='" + branchCode + "'";
                    }
                    else
                    {
                        sql = "update payment_Process_ZoneWise set Select_Status ='B' Where Supplier_code = '" + supplier + "'";
                        sql = sql + " and invoice_number in (" + invoiceNoCollection + ") and branch_code ='" + branchCode + "'";
                    }

                    DbCommand cmd = ImpalDb.GetSqlStringCommand(sql);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Liability Payment

        #region HO Payment

        public List<PaymentProcessTemp> LoadCorporateHOPymtBranchTotalTemp(string supplier, string invoiceFromDate, string invoiceToDate, string zoneCode, string strBranchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();

                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Zone_HoPymt_Temp");
                    ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd, "@from_Date", DbType.String, invoiceFromDate);
                    ImpalDb.AddInParameter(cmd, "@To_date", DbType.String, invoiceToDate);
                    ImpalDb.AddInParameter(cmd, "@Zone_Code", DbType.String, zoneCode);
                    ImpalDb.AddInParameter(cmd, "@BR_Code", DbType.String, strBranchCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_corporate_HoPymt_BranchTotal_Temp");
                    ImpalDb.AddInParameter(cmd1, "@supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt1", DbType.String, invoiceFromDate);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt2", DbType.String, invoiceToDate);
                    ImpalDb.AddInParameter(cmd1, "@zoneCode", DbType.String, zoneCode);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd1);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.BranchCode = dr["Branch_Code"].ToString();
                            pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["Column1"]), 2);
                            pymtItem.CDAmount = Math.Round(Convert.ToDecimal(dr["Column2"]), 2);
                            lstDetail.Add(pymtItem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion HO Payment

        #region Liability Payment

        public List<PaymentProcessTemp> LoadCorporatePymtBranchTotalTemp(string supplier, string invoiceFromDate, string invoiceToDate, string zoneCode, string strBranchCode)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();

                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Payment_Process_Zone_Temp");
                    ImpalDb.AddInParameter(cmd, "@Supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd, "@from_Date", DbType.String, invoiceFromDate);
                    ImpalDb.AddInParameter(cmd, "@To_date", DbType.String, invoiceToDate);
                    ImpalDb.AddInParameter(cmd, "@Zone_Code", DbType.String, zoneCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    DbCommand cmd1 = ImpalDb.GetStoredProcCommand("Usp_corporate_Pymt_BranchTotal_Temp");
                    ImpalDb.AddInParameter(cmd1, "@supplier", DbType.String, supplier);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt1", DbType.String, invoiceFromDate);
                    ImpalDb.AddInParameter(cmd1, "@invoicedt2", DbType.String, invoiceToDate);
                    ImpalDb.AddInParameter(cmd1, "@zoneCode", DbType.String, zoneCode);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd1);

                    List<PaymentProcessTemp> lstDetail = new List<PaymentProcessTemp>();
                    PaymentProcessTemp pymtItem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pymtItem = new PaymentProcessTemp();
                            pymtItem.BranchCode = dr["Branch_Code"].ToString();
                            pymtItem.InvoiceAmount = Math.Round(Convert.ToDecimal(dr["Column1"]), 2);
                            pymtItem.CDAmount = Math.Round(Convert.ToDecimal(dr["Column2"]), 2);
                            lstDetail.Add(pymtItem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Liability Payment

        #region Group Co Conversion
        public void LoadCorporateSupplierPaymentGroupCompany()
        {
            int result = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Corporate_supplier_Payment_Group_Company");
                    //ImpalDb.AddInParameter(cmd, "@BrCode", DbType.String, supplier);
                    //ImpalDb.AddInParameter(cmd, "@Invoice_Number", DbType.String, invoiceDate1);
                    //ImpalDb.AddInParameter(cmd, "@Invoice_Date", DbType.String, invoiceDate2);
                    //ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@CN_Number", DbType.String, supplier);
                    //ImpalDb.AddInParameter(cmd, "@CN_Date", DbType.String, invoiceDate1);
                    //ImpalDb.AddInParameter(cmd, "@CN_Amount", DbType.String, invoiceDate2);
                    //ImpalDb.AddInParameter(cmd, "@CN_Indicator", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@supplier_Code", DbType.String, invoiceDate2);
                    //ImpalDb.AddInParameter(cmd, "@Branch", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Inward_Number", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Inward_Date", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Document_Number", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Document_Date", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, zoneCode);
                    //ImpalDb.AddInParameter(cmd, "@Adjustment_Value", DbType.String, zoneCode);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }
        }
        #endregion Group Co Conversion


        public List<InvoiceHeader> LoadInvoiceCNDetails(string BranchCode, string supplier, string invoiceNumber)
        {
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    string sqlquery = "select Ho_Reference_Number,Invoice_Number,convert(varchar(10),Invoice_Date,103) invoice_Date,case when isnull(invoice_amount,0)>0 and isnull(credit_note_amount,0)=0 then invoice_amount when isnull(credit_note_amount,0)>0 and isnull(invoice_amount,0)=0 then isnull(credit_note_amount,0) end,Da_Ca_Indicator,Status,Supplier_Code,Branch_Code from Corporate_Payment_Detail Where Branch_Code='" + BranchCode + "' and Supplier_Code = '" + supplier + "' and invoice_Number = '" + invoiceNumber + "' and status<>'I'";
                    DbCommand cmd = ImpalDb.GetSqlStringCommand(sqlquery);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DataSet ds = ImpalDb.ExecuteDataSet(cmd);

                    List<InvoiceHeader> lstDetail = new List<InvoiceHeader>();
                    InvoiceHeader InvCNitem = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            InvCNitem = new InvoiceHeader();
                            InvCNitem.HOREFNumber = dr[0].ToString();
                            InvCNitem.InvoiceNumber = dr[1].ToString();
                            InvCNitem.InvoiceDate = dr[2].ToString();
                            InvCNitem.InvoiceAmount = dr[3].ToString();
                            InvCNitem.Indicator = dr[4].ToString();
                            InvCNitem.Pymt_Status = dr[5].ToString();
                            InvCNitem.SupplierCode = supplier;
                            InvCNitem.BranchCode = dr[7].ToString();

                            lstDetail.Add(InvCNitem);
                        }
                    }

                    scope.Complete();
                    return lstDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Invoice Entry
        public string ADDCorporateSupplierInvoicePayment(ref InvoiceHeader invoiceHeader)
        {
            string HOREFNumber = "";
            int result = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_ADD_Corporate_Supplier_InvoicePayment");
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, invoiceHeader.BranchCode);
                    ImpalDb.AddInParameter(cmd, "@Supplier_code", DbType.String, invoiceHeader.SupplierCode);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Number", DbType.String, invoiceHeader.InvoiceNumber);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Date", DbType.String, invoiceHeader.InvoiceDate);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, invoiceHeader.InvoiceAmount);
                    ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, invoiceHeader.Indicator);
                    ImpalDb.AddInParameter(cmd, "@Pymt_Status", DbType.String, invoiceHeader.Pymt_Status);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    HOREFNumber = ImpalDb.ExecuteScalar(cmd).ToString();

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return HOREFNumber;
        }

        public void UpdateCorporateSupplierInvoicePayment(ref InvoiceHeader invoiceHeader)
        {

            int result = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {

                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_UPD_Corporate_Supplier_InvoicePayment");
                    ImpalDb.AddInParameter(cmd, "@Ho_Number", DbType.String, invoiceHeader.HOREFNumber);
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, invoiceHeader.BranchCode);
                    ImpalDb.AddInParameter(cmd, "@Supplier_code", DbType.String, invoiceHeader.SupplierCode);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Number", DbType.String, invoiceHeader.InvoiceNumber);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Date", DbType.String, invoiceHeader.InvoiceDate);
                    ImpalDb.AddInParameter(cmd, "@Invoice_Amount", DbType.String, invoiceHeader.InvoiceAmount);
                    ImpalDb.AddInParameter(cmd, "@Indicator", DbType.String, invoiceHeader.Indicator);
                    ImpalDb.AddInParameter(cmd, "@Pymt_Status", DbType.String, invoiceHeader.Pymt_Status);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }
        }
        #endregion Invoice Entry


        #region SFL Corp

        public int UpdateSFLPaymentDetails(string BranchCode, string filePath, string fileName)
        {
            int result = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_Upd_Temp_SFL_PaymentDetails");
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDb.AddInParameter(cmd, "@FilePath", DbType.String, filePath);
                    ImpalDb.AddInParameter(cmd, "@FileName", DbType.String, fileName);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    result = Convert.ToInt32(ImpalDb.ExecuteScalar(cmd));

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }

        public int InsertSFLPaymentDetails()
        {
            int result = 0;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("Usp_SFL_Corp_insert");
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd);

                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }

            return result;
        }
        #endregion

        #region Other Payment

        public List<HORNumber> LoadHOREFNumber(string supplierCode)
        {
            List<HORNumber> HOREFNumbers = new List<HORNumber>();

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select ccwh_number from corporate_Payment_Detail Where supplier_code ='" + supplierCode + "' and status <> 'P' and ccwh_number is not null group by ccwh_Number order by ccwh_Number desc";
            HOREFNumbers.Add(new HORNumber(string.Empty));
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    HOREFNumbers.Add(new HORNumber(reader["ccwh_Number"].ToString()));
                }
            }

            return HOREFNumbers;
        }


        public string GetOtherPaymentAmount(string hoRefNumber)
        {
            string Amount = "";

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select (sum(isnull(invoice_amount,0)) + sum(isnull(Credit_Note_Amount,0)) - sum(isnull(cash_discount,0))) Amount from Corporate_Payment_Detail where CCWH_Number ='" + hoRefNumber + "'";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    Amount = reader["Amount"].ToString();
                }
            }

            return Amount;
        }

        public void DeleteHoPymtTable(string strBranchCode)
        {
            Database ImpalDb = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDb.GetSqlStringCommand("truncate table Payment_Process_ZoneWise");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDb.ExecuteNonQuery(cmd);
        }

        public string AddNewChequeSlipForOtherPayment(ref ChequeSlipHeader chequeSlipHeader)
        {
            int result = 0;
            string ChequeSlipNumber = "";
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDb = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_addchequeslip1");
                    ImpalDb.AddInParameter(cmd, "@Cheque_Slip_Date", DbType.String, chequeSlipHeader.ChequeSlipDate);
                    ImpalDb.AddInParameter(cmd, "@Supplier_Code", DbType.String, chequeSlipHeader.SupplierCode);
                    ImpalDb.AddInParameter(cmd, "@Branch_Code", DbType.String, chequeSlipHeader.BranchCode);
                    ImpalDb.AddInParameter(cmd, "@cacode", DbType.String, chequeSlipHeader.Chart_of_Account_Code);
                    ImpalDb.AddInParameter(cmd, "@chequeno", DbType.String, chequeSlipHeader.ChequeNumber);
                    ImpalDb.AddInParameter(cmd, "@chequedate", DbType.String, chequeSlipHeader.ChequeDate);
                    ImpalDb.AddInParameter(cmd, "@remarks", DbType.String, chequeSlipHeader.Remarks);
                    ImpalDb.AddInParameter(cmd, "@chequebank", DbType.String, chequeSlipHeader.ChequeBank);
                    ImpalDb.AddInParameter(cmd, "@chequebranch", DbType.String, chequeSlipHeader.ChequeBranch);
                    ImpalDb.AddInParameter(cmd, "@chequeamt", DbType.Decimal, Convert.ToDecimal(chequeSlipHeader.ChequeAmount));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ChequeSlipNumber = ImpalDb.ExecuteScalar(cmd).ToString();

                    //List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();
                    //lstChequeSlipDetail = GetChequeSlipDetailForOtherPayment(chequeSlipHeader.HOREFNumber);

                    //foreach (ChequeSlipDetail chequeSlipDetail in lstChequeSlipDetail)
                    //{
                    if (ChequeSlipNumber != "")
                    {
                        DbCommand cmd1 = ImpalDb.GetStoredProcCommand("usp_addchequeslip2");
                        ImpalDb.AddInParameter(cmd1, "@Cheque_Slip_Number", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd1, "@Serial_Number", DbType.Int32, 0);
                        ImpalDb.AddInParameter(cmd1, "@Reference_Document_Number", DbType.String, chequeSlipHeader.HOREFNumber);
                        ImpalDb.AddInParameter(cmd1, "@Reference_Type", DbType.String, "");
                        ImpalDb.AddInParameter(cmd1, "@Amount", DbType.Decimal, 0.00);
                        ImpalDb.AddInParameter(cmd1, "@Reference_Document_Date", DbType.String, chequeSlipHeader.ChequeSlipDate);
                        ImpalDb.AddInParameter(cmd1, "@Document_Value", DbType.Decimal, 0.00);
                        ImpalDb.AddInParameter(cmd1, "@cd_value", DbType.Decimal, 0.00);
                        ImpalDb.AddInParameter(cmd1, "@branch_code", DbType.String, "");
                        ImpalDb.AddInParameter(cmd1, "@cd_percentage", DbType.Int32, Convert.ToInt32(0));
                        ImpalDb.AddInParameter(cmd1, "@Branch", DbType.String, chequeSlipHeader.BranchCode);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd1);
                        //}

                        DbCommand cmd2 = ImpalDb.GetStoredProcCommand("Usp_Update_Pymt_LiabilityList");
                        ImpalDb.AddInParameter(cmd2, "@Document_Number", DbType.String, chequeSlipHeader.HOREFNumber);
                        ImpalDb.AddInParameter(cmd2, "@Branch_Code", DbType.String, chequeSlipHeader.BranchCode);
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Slip_Number", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Slip_Date", DbType.String, chequeSlipHeader.ChequeSlipDate);
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Number", DbType.String, chequeSlipHeader.ChequeNumber);
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Date", DbType.String, chequeSlipHeader.ChequeDate);
                        ImpalDb.AddInParameter(cmd2, "@Cheque_Amount", DbType.Decimal, chequeSlipHeader.ChequeAmount);
                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd2);

                        DbCommand cmd3 = ImpalDb.GetStoredProcCommand("usp_addgl_Pymt_cq1");
                        ImpalDb.AddInParameter(cmd3, "@doc_no", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd3, "@Branch_Code", DbType.String, chequeSlipHeader.BranchCode);
                        cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd3);


                        //foreach (ChequeSlipDetail chequeSlipDetail in lstChequeSlipDetail)
                        //{
                        DbCommand cmd4 = ImpalDb.GetStoredProcCommand("Usp_AddGl_Pymt_Cq_Crp1");
                        ImpalDb.AddInParameter(cmd4, "@Doc_no", DbType.String, ChequeSlipNumber);
                        ImpalDb.AddInParameter(cmd4, "@Doc_dt", DbType.String, chequeSlipHeader.ChequeSlipDate);
                        ImpalDb.AddInParameter(cmd4, "@Ref_No", DbType.String, chequeSlipHeader.HOREFNumber);
                        ImpalDb.AddInParameter(cmd4, "@Ref_Amt", DbType.Decimal, 0.00);
                        ImpalDb.AddInParameter(cmd4, "@Branch_Code", DbType.String, "");
                        ImpalDb.AddInParameter(cmd4, "@Supplier_Code", DbType.String, chequeSlipHeader.SupplierCode);
                        ImpalDb.AddInParameter(cmd4, "@Remarks", DbType.String, chequeSlipHeader.Remarks);
                        ImpalDb.AddInParameter(cmd4, "@Branch_Code1", DbType.String, "CRP");
                        ImpalDb.AddInParameter(cmd4, "@cd_amount", DbType.Decimal, 0.00);
                        cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                        result = ImpalDb.ExecuteNonQuery(cmd4);
                        //}
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

            return ChequeSlipNumber;
        }

        public List<ChequeSlipDetail> GetChequeSlipDetailForOtherPaymentSearch(string strHOREFNumber)
        {
            List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();

            ChequeSlipDetail objChequeSlipDetail = new ChequeSlipDetail();
            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = " select Branch_code,sum(isnull(Invoice_Amount,0)) +  sum(isnull(Credit_Note_Amount,0)) Amt from Corporate_Payment_Detail where CCWH_Number  ='" + strHOREFNumber + "'  group by branch_code";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipDetail = new ChequeSlipDetail();
                    objChequeSlipDetail.Branch = reader["Branch_code"].ToString();
                    objChequeSlipDetail.Amount = Convert.ToDecimal(Convert.ToString(reader["Amt"]));
                    lstChequeSlipDetail.Add(objChequeSlipDetail);
                }
            }

            return lstChequeSlipDetail;
        }

        public List<ChequeSlipDetail> GetChequeSlipDetailForOtherPayment(string strHORefNumber)
        {
            List<ChequeSlipDetail> lstChequeSlipDetail = new List<ChequeSlipDetail>();

            ChequeSlipDetail objChequeSlipDetail = new ChequeSlipDetail();
            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = " select Branch_code,(sum(isnull(Invoice_Amount,0)) + sum(isnull(Credit_Note_Amount,0)) - sum(isnull(Cash_Discount,0))) Amt,sum(isnull(Cash_Discount,0)) cdamt from Corporate_Payment_Detail where CCWH_Number ='" + strHORefNumber + "' group by branch_code";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    objChequeSlipDetail = new ChequeSlipDetail();
                    objChequeSlipDetail.Branch = reader["Branch_code"].ToString();
                    objChequeSlipDetail.Amount = Convert.ToDecimal(Convert.ToString(reader["Amt"]));
                    objChequeSlipDetail.CDValue = Convert.ToDecimal(Convert.ToString(reader["cdamt"]));
                    lstChequeSlipDetail.Add(objChequeSlipDetail);
                }
            }

            return lstChequeSlipDetail;
        }

        #endregion Other Payment

        public List<SupplierLine> LoadSupplierLine()
        {
            List<SupplierLine> SupplierLines = new List<SupplierLine>();

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select Supplier_Line_Code,Short_Description from Supplier_Line_Master";

            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    SupplierLines.Add(new SupplierLine(reader["Supplier_Line_Code"].ToString(), reader["Short_Description"].ToString()));
                }
            }

            return SupplierLines;
        }

        public List<HORNumber> LoadHOREFNumber()
        {
            List<HORNumber> HOREFNumbers = new List<HORNumber>();

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select ccwh_Number from Bms_Header Where Bms_Status ='Y' group by CCWH_Number order by CCWH_Number";
            HOREFNumbers.Add(new HORNumber(string.Empty));
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    HOREFNumbers.Add(new HORNumber(reader["ccwh_Number"].ToString()));
                }
            }

            return HOREFNumbers;
        }

        public List<ZoneDetails> LoadZoneMaster()
        {
            List<ZoneDetails> ZoneDetail = new List<ZoneDetails>();

            Database ImpalDb = DataAccess.GetDatabase();

            //string sSQL = "select a.Zone_Code,a.Zone_Name from Zone_Master a ";
            //sSQL = sSQL + "Where a.zone_code not in (select zone_code from Payment_Process_ZoneWise b group by zone_code) ";

            DbCommand cmd = ImpalDb.GetStoredProcCommand("usp_Get_Zones_Pymt");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

            ZoneDetail.Add(new ZoneDetails("0", string.Empty));
            using (IDataReader reader = ImpalDb.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ZoneDetail.Add(new ZoneDetails(reader["Zone_Code"].ToString(), reader["Zone_Name"].ToString()));
                }
            }

            return ZoneDetail;
        }

        public int CheckZoneMaster()
        {
            int result = 0;

            Database ImpalDb = DataAccess.GetDatabase();

            string sSQL = "select count(Zone_Code) from Zone_Master Where zone_code not in ";
            sSQL = sSQL + "(select zone_code from Payment_Process_ZoneWise group by zone_code)";
            result = Convert.ToInt16(ImpalDb.ExecuteScalar(CommandType.Text, sSQL));

            return result;
        }

        public List<Supplier> GetSuppliers()
        {
            List<Supplier> SupplierBranches = new List<Supplier>();
            Database ImpalDb = DataAccess.GetDatabase();
            string sSQL = "select Supplier_Code,Supplier_Name  from Supplier_Master Where substring(supplier_code,1,2) in ( '32','41') ";
            //Adds empty item to the DDL
            SupplierBranches.Add(new Supplier("0", "-- Select --"));
            using (IDataReader reader = ImpalDb.ExecuteReader(CommandType.Text, sSQL))
            {
                while (reader.Read())
                {
                    SupplierBranches.Add(new Supplier(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SupplierBranches;
        }

        public List<StatementPaymentDetail> ListStatementPymtDetails()
        {
            List<StatementPaymentDetail> objPymtDetails = new List<StatementPaymentDetail>();
            string SqlQry = string.Empty;
            string sSqlPymtTbl = string.Empty;

            SqlQry = "Select Branch_Code,Supplier_Code,Invoice_Number,Invoice_Date,Invoice_Amount,Indicator,Status,Inward_Number,Inward_Date From Supplier_Payment_Statementwise";

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand(SqlQry);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    StatementPaymentDetail objPymtItems = new StatementPaymentDetail();

                    objPymtItems.BranchCode = reader["Branch_Code"].ToString();
                    objPymtItems.SupplierCode = reader["SupplierCode"].ToString();
                    objPymtItems.InvoiceNumber = reader["InvoiceNo"].ToString();
                    objPymtItems.InvoiceDate = reader["InvoiceDate"].ToString();
                    objPymtItems.InvoiceAmount = Convert.ToDecimal(reader["Amount"].ToString());
                    objPymtItems.Indicator = reader["Indicator"].ToString();
                    objPymtItems.Status = reader["Status"].ToString();
                    objPymtItems.InwardNumber = reader["Inward_Number"].ToString();
                    objPymtItems.InwardDate = reader["Inward_DateDate"].ToString();

                    objPymtDetails.Add(objPymtItems);
                }

                return objPymtDetails;
            }
        }
    }

    public class MiscHeader
    {
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public string SupplierInvoiceDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPlace { get; set; }
        public string InvoiceAmount { get; set; }
        public string ExciseDutyAmount { get; set; }
        public string SalesTaxAmount { get; set; }
        public string OtherCharges { get; set; }
        public string OtherDeductions { get; set; }
        public string AdvanceAmount { get; set; }
        public string Remarks { get; set; }
        public string BranchCode { get; set; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<MiscDetail> Items { get; set; }
    }

    public class MiscDetail
    {
        public string DocumentNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Description { get; set; }
        public string Rate { get; set; }
        public string Tax_Amount { get; set; }

        public string Discount { get; set; }
        public string Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }

    public class BMSPaymentAdviceDetails
    {
        public List<BMSPaymentAdviceDetail> bmsPaymentAdviceDetail { get; set; }
    }


    public class BMSPaymentAdviceDetail
    {
        public bool Selected { get; set; }
        public string BMSNumber { get; set; }
        public string BMSDate { get; set; }
        public string BMSDueDate { get; set; }
        public string BMSAmount { get; set; }
        public string CDAmount { get; set; }
        public string BMSValue { get; set; }
        public string NoofDays { get; set; }
        public string TotalValue { get; set; }
        public string RateofInterest { get; set; }
    }

    public class BMS
    {
        public string HOBMSNumber { get; set; }
        public string BMSNumber { get; set; }
        public string BMSDate { get; set; }
        public string BMSDueDate { get; set; }
        public decimal BMSAmount { get; set; }
        public string BMSBankName { get; set; }
        public string SupplierCode { get; set; }
        public string BranchCode { get; set; }
        public string BMSStatus { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
    }

    public class BankNames
    {
        public BankNames(int BankCode, string BankName)
        {
            _BankCode = BankCode;
            _BankName = BankName;
        }

        public BankNames()
        {

        }
        public BankNames(int BankCode)
        {
            _BankCode = BankCode;
        }

        private int _BankCode;
        private string _BankName;

        public int BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
    }


    public class ChequeSlipHeader
    {
        public string ChequeSlipNumber { get; set; }
        public string ChequeSlipDate { get; set; }
        public string SupplierLineCode { get; set; }
        public string BranchCode { get; set; }
        public string BankBranchCode { get; set; }
        public string BankCode { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeAmount { get; set; }
        public string ChequeBank { get; set; }
        public string ChequeBranch { get; set; }
        public string Remarks { get; set; }
        public string SupplierCode { get; set; }
        public string Branch { get; set; }
        public string HOREFNumber { get; set; }       
        public List<ChequeSlipDetail> Items { get; set; }
    }

    public class ChequeSlipDetail
    {
        public Int32 SerialNo { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string ReferenceDocumentNumber { get; set; }
        public string ReferenceDocumentDate { get; set; }
        public string ReferenceType { get; set; }
        public decimal Amount { get; set; }
        public string DocumentValue { get; set; }
        public decimal CDValue { get; set; }
        public decimal CDPercentage { get; set; }
        public string Branch { get; set; }
    }

    public class InvoiceHeader
    {
        public string HOREFNumber { get; set; }
        public string BranchCode { get; set; }
        public string SupplierCode { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceAmount { get; set; }
        public string Indicator { get; set; }
        public string Pymt_Status { get; set; }
    }

    public class PaymentProcessTemp
    {
        public bool Selected { get; set; }
        public string Indicator { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string BranchCode { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal CDAmount { get; set; }
        public decimal CDDays { get; set; }
        public decimal InvoiceValue { get; set; }
    }


    public class PaymentProcessTempForBranch
    {
        public bool Selected { get; set; }
        public string Supplier { get; set; }
        public string Branch { get; set; }
        public decimal BranchAmount { get; set; }
        public decimal CDAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Zone { get; set; }
    }

    public class StatementPaymentDetail
    {
        public string BranchCode { get; set; }
        public string SupplierCode { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string Status { get; set; }        
        public string Indicator { get; set; }
        public string InwardNumber { get; set; }
        public string InwardDate { get; set; }
    }

    public class PaymentProcessTempForBranches
    {
        public List<PaymentProcessTempForBranch> paymentProcessTempForBranch { get; set; }
    }

    public class HORNumber
    {
        public HORNumber(string HOREFNumber)
        {
            _HOREFNumber = HOREFNumber;
        }

        private string _HOREFNumber;

        public string HOREFNumber
        {
            get { return _HOREFNumber; }
            set { _HOREFNumber = value; }
        }
    }

    public class ZoneDetails
    {
        public ZoneDetails(string ZoneCode, string ZoneName)
        {
            _zoneCode = ZoneCode;
            _zoneName = ZoneName;
        }

        private string _zoneCode;
        private string _zoneName;

        public string ZoneCode
        {
            get { return _zoneCode; }
            set { _zoneCode = value; }
        }

        public string ZoneName
        {
            get { return _zoneName; }
            set { _zoneName = value; }
        }
    }

    public class GroupConversionMonYear
    {
        private string _year_month;
        private string _month_year;

        public GroupConversionMonYear(string year_month, string month_year)
        {
            _year_month = year_month;
            _month_year = month_year;            
        }

        public GroupConversionMonYear()
        {
        }

        public string year_month
        {
            get { return _year_month; }
            set { _year_month = value; }
        }

        public string month_year
        {
            get { return _month_year; }
            set { _month_year = value; }
        }
    }
}
