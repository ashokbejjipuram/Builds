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
using IMPALLibrary.Masters.CustomerDetails;
using System.Transactions;

namespace IMPALLibrary.Transactions.Finance
{
    public class DebitCredit
    {
        public DebitCredit()
        {
        }

        public List<DocumentNumber> GetAllDocumentNumber(string BranchCode)
        {
            List<DocumentNumber> DocumentNumberList = new List<DocumentNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct Document_Number from Debit_Credit_Note_Header_new where Branch_code = '" + BranchCode + "' and year(document_date)>=year(getdate())-3 and Dr_Cr_Indicator in ('DR','CR') and RIGHT(Substring(Document_Number,1,4),2)>= RIGHT(YEAR(GETDATE())-2, 2) order by Document_Number desc");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentNumberList.Add(new DocumentNumber(reader["Document_Number"].ToString()));
                }
            }

            return DocumentNumberList;
        }

        public List<DocumentNumber> GetAllSupplierDocumentNumber(string BranchCode)
        {
            List<DocumentNumber> DocumentNumberList = new List<DocumentNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Document_Number from Debit_Credit_Note_Header where Branch_code = '" + BranchCode + "' and year(document_date)>=year(getdate())-3 and Dr_Cr_Indicator in ('DA','CA') and Right(Document_Number,2) in ('CA','DA') and supplier_Code is not null order by document_date desc");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentNumberList.Add(new DocumentNumber(reader["Document_Number"].ToString()));
                }
            }

            return DocumentNumberList;
        }

        public List<DocumentNumber> GetAllSupplierDocumentNumberCOR(string BranchCode)
        {
            List<DocumentNumber> DocumentNumberList = new List<DocumentNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Document_Number from Debit_Credit_Note_Header where Branch_code = '" + BranchCode + "' and year(document_date)>=year(getdate())-3 and Dr_Cr_Indicator in ('DA','CA') and supplier_code is not null and tr_branch_Code is null and customer_Code is null order by document_Number desc");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentNumberList.Add(new DocumentNumber(reader["Document_Number"].ToString()));
                }
            }

            return DocumentNumberList;
        }

        public List<DocumentNumber> GetAllCustBranchDocumentNumber(string BranchCode)
        {
            List<DocumentNumber> DocumentNumberList = new List<DocumentNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Document_Number from Debit_Credit_Note_Header where Branch_code = '" + BranchCode + "' and year(document_date)>=year(getdate())-3 and Dr_Cr_Indicator in ('DA','CA') and Right(Document_Number,2) in ('CA','DA') and supplier_Code is null order by document_Number desc");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentNumberList.Add(new DocumentNumber(reader["Document_Number"].ToString()));
                }
            }

            return DocumentNumberList;
        }

        public List<DocumentNumber> GetAllCustBranchDocumentNumberCOR(string BranchCode)
        {
            List<DocumentNumber> DocumentNumberList = new List<DocumentNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Document_Number from Debit_Credit_Note_Header where Branch_code = '" + BranchCode + "' and year(document_date)>=year(getdate())-3 and Dr_Cr_Indicator in ('DA','CA') and Right(Document_Number,2) in ('CA','DA') and ((supplier_code is null and tr_branch_Code is null and customer_Code is not null) or (supplier_code is null and tr_branch_Code is not null and customer_Code is null)) order by document_date desc");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DocumentNumberList.Add(new DocumentNumber(reader["Document_Number"].ToString()));
                }
            }

            return DocumentNumberList;
        }

        public List<TransactionType> GetTransactionType(string TransactionCode)
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;
            if (string.IsNullOrEmpty(TransactionCode))
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('751','741') order by Transaction_Type_code");
            else
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('" + TransactionCode + "') order by Transaction_Type_code");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<TransactionType> GetCustomerBranchTransactionType(string TransactionCode, string CustBranchInd, int AccountingPeriodCode)
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            if (AccountingPeriodCode == 0)
            {
                if (TransactionCode.Equals("CA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('651','653','658','751','752','753','754','755','756','757','758') order by Transaction_Type_Description");//'658','758'
                else if (TransactionCode.Equals("DA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('741','742') order by Transaction_Type_Description");
                else if (TransactionCode.Equals("CA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('751') order by Transaction_Type_Description");
                else if (TransactionCode.Equals("DA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('741') order by Transaction_Type_Description");
            }
            else if (AccountingPeriodCode == 1)
            {
                if (TransactionCode.Equals("CA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('651','653','658','751','753','758') order by Transaction_Type_Description"); //'752','754','755','756','757'
                else if (TransactionCode.Equals("DA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('741','742') order by Transaction_Type_Description");
                else if (TransactionCode.Equals("CA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('751') order by Transaction_Type_Description");
                else if (TransactionCode.Equals("DA", StringComparison.OrdinalIgnoreCase) && CustBranchInd.Equals("Branch", StringComparison.OrdinalIgnoreCase))
                    cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('741') order by Transaction_Type_Description");
            }
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<TransactionType> GetCustomerBranchTransactionTypeView()
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_code,upper(Transaction_Type_Description) Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_code in ('651','653','658','741','742','751','752','753','754','755','756','757','758') order by Transaction_Type_Description");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<TransactionType> GetTransactionType(string BranchCode, string DebitCreditNoteType)
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            if (DebitCreditNoteType.Equals("CA"))
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_Code,Transaction_Type_Description from Transaction_Type_Master Where Transaction_Type_Code in (751,761,762,763,764,765,766,767,768,769,770) Order by Transaction_Type_Code");
            else
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_Code,Transaction_Type_Description from Transaction_Type_Master Where Transaction_Type_Code in (741,761,762,763,764,765,766,767,768,769,770) Order by Transaction_Type_Code");

            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<TransactionType> GetTransactionTypeView(string BranchCode, string DebitCreditNoteType)
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_Code,Transaction_Type_Description from Transaction_Type_Master Where Transaction_Type_Code in (741,751,761,762,763,764,765,766,767,768,769,770) Order by Transaction_Type_Code");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<TransactionType> GetTransactionTypeCOR(string BranchCode, string DebitCreditNoteType)
        {
            List<TransactionType> TransactionTypeList = new List<TransactionType>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;

            if (DebitCreditNoteType.Equals("CA"))
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_Code,Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_Code in (751,758,765) Order by Transaction_Type_Code");
            else
                cmd = ImpalDB.GetSqlStringCommand("Select Transaction_Type_Code,Transaction_Type_Description from Transaction_Type_Master where Transaction_Type_Code in (741,758,765) Order by Transaction_Type_Code");

            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TransactionTypeList.Add(new TransactionType(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return TransactionTypeList;
        }

        public List<SalesTaxCode> GetSalesTaxCode()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct sales_tax_code,Sales_Tax_Percentage from sales_tax_Master");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code>1000");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeSGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code in (1001,1005,1015,1023)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeCGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code in (1002,1006,1016,1024)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeIGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code in (1003,1007,1017,1025)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return SalesTaxCodeList;
        }

        public List<SalesTaxCode> GetSalesTaxCodeUTGST()
        {
            List<SalesTaxCode> SalesTaxCodeList = new List<SalesTaxCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            SalesTaxCodeList.Add(new SalesTaxCode("", "0"));
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select distinct cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' , cast(sales_tax_code as varchar) + ' - ' + cast(Sales_Tax_Percentage as varchar) + '%' from sales_tax_Master where sales_tax_code in (1004,1008,1018,1026)");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SalesTaxCodeList.Add(new SalesTaxCode(reader[0].ToString(), reader[1].ToString()));
                }
            }

            return SalesTaxCodeList;
        }

        public String GetSalesTaxPer(string SalesTaxCode)
        {
            string _SalesTaxCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Sales_Tax_Percentage from sales_tax_Master where sales_tax_code='" + SalesTaxCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            _SalesTaxCode = ImpalDB.ExecuteScalar(cmd).ToString();
            return _SalesTaxCode;
        }

        public String GetBranchName(string BranchCode)
        {
            string _BranchCode = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select branch_name from branch_master where branch_code='" + BranchCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            _BranchCode = ImpalDB.ExecuteScalar(cmd).ToString();
            return _BranchCode;
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
        public List<Item> GetSupplierPlant(string SupplierCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string Qry = "Select Gst_Supplier_Code, Supplier_Name from gst_supplier_master where Supplier_Code='" + SupplierCode + "'";
            List<Item> DepotList = new List<Item>();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Qry);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                Item objItem = new Item();
                objItem.ItemDesc = "";
                objItem.ItemCode = "";
                DepotList.Add(objItem);

                while (reader.Read())
                {
                    objItem = new Item();
                    objItem.ItemDesc = reader["Supplier_Name"].ToString();
                    objItem.ItemCode = reader["Gst_Supplier_Code"].ToString();
                    DepotList.Add(objItem);
                }
            }

            return DepotList;
        }
        public string SupplyPlantInterStateStatus(string SupplierCode, string SupplyPlantCode, string BranchCode)
        {
            string Indicator = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select case when g.State_Code=b.State_Code then 'L' else 'O' end from Gst_Supplier_Master g inner join Branch_Master b";
            sSQL = sSQL + " on g.Supplier_Code='" + SupplierCode + "' and g.Gst_Supplier_Code='" + SupplyPlantCode + "' and b.Branch_Code='" + BranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Indicator = ImpalDB.ExecuteScalar(cmd).ToString();

            return Indicator;
        }

        public List<CustomerDtls> GetCustomer(string BranchCode)
        {
            List<CustomerDtls> lstCustomer = null;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = null;
            if (!BranchCode.Equals("CRP", StringComparison.OrdinalIgnoreCase))
                cmd = ImpalDB.GetSqlStringCommand("SELECT CUSTOMER_CODE,CUSTOMER_NAME  + ' | ' + Location FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE BRANCH_CODE = '" + BranchCode + "' and CUSTOMER_CODE is not null ORDER BY CUSTOMER_NAME");
            else
                cmd = ImpalDB.GetSqlStringCommand("SELECT CUSTOMER_CODE,CUSTOMER_NAME  + ' | ' + Location FROM CUSTOMER_MASTER WITH (NOLOCK) WHERE CUSTOMER_CODE is not null ORDER BY CUSTOMER_NAME");
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

        public DebitCreditNote ViewDebitCreditNote(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDebitCredit_NoteHeader");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            DebitCreditNote Note = new DebitCreditNote();
            List<DebitCreditAdviceFields> DebitCreditList = new List<DebitCreditAdviceFields>();
            List<DebitCreditNoteDetails> DebitCreditListDetails = new List<DebitCreditNoteDetails>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    DebitCreditAdviceFields DebitCreditFields = new DebitCreditAdviceFields();
                    DebitCreditFields.Document_Number = objReader["Document_Number"].ToString();
                    DebitCreditFields.Transaction_Type_Code = objReader["Transaction_Type_Code"].ToString();
                    DebitCreditFields.Transaction_Type_Description = objReader["Transaction_Type_Description"].ToString();
                    DebitCreditFields.Dr_Cr_Indicator = objReader["Dr_Cr_Indicator"].ToString();
                    DebitCreditFields.Document_Date = objReader["Document_Date"].ToString();
                    DebitCreditFields.Branch_Code = objReader["branch_name"].ToString();
                    DebitCreditFields.Indicator_Code = objReader["Indicator_Code"].ToString();
                    DebitCreditFields.Reference_Document_Number = objReader["Reference_Document_Number"].ToString();
                    DebitCreditFields.Remarks = objReader["Remarks"].ToString();
                    DebitCreditFields.Value = objReader["Adjustment_Value"].ToString();
                    DebitCreditFields.Reference_Document_Date = objReader["Reference_Document_Date"].ToString();
                    DebitCreditFields.Accountingperiod = objReader["Accountingperiod"].ToString();
                    DebitCreditFields.Indicator = objReader["Indicator"].ToString();
                    DebitCreditFields.Indicator_Name = objReader["Indicator_Name"].ToString();
                    DebitCreditList.Add(DebitCreditFields);
                }

                objReader.NextResult();
                while (objReader.Read())
                {
                    DebitCreditNoteDetails NoteDetails = new DebitCreditNoteDetails();
                    NoteDetails.RowNumber = Convert.ToInt32(objReader["RowNumber"]);
                    NoteDetails.ChartOfAccount = objReader["Chart_of_Account_Code"].ToString();
                    NoteDetails.Amount = objReader["value"].ToString();
                    NoteDetails.Remarks = objReader["Remarks"].ToString();
                    DebitCreditListDetails.Add(NoteDetails);
                }
            }

            Note.ListDebitCreditNote = DebitCreditList;
            Note.ListDebitCreditNoteDetails = DebitCreditListDetails;

            return Note;
        }

        public DebitCreditNoteGST ViewDebitCreditNoteGST(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDebitCredit_NoteHeader_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            DebitCreditNoteGST Note = new DebitCreditNoteGST();
            List<DebitCreditAdviceFieldsGST> DebitCreditListGST = new List<DebitCreditAdviceFieldsGST>();
            List<DebitCreditNoteDetailsGST> DebitCreditListDetailsGST = new List<DebitCreditNoteDetailsGST>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    DebitCreditAdviceFieldsGST DebitCreditFieldsGST = new DebitCreditAdviceFieldsGST();
                    DebitCreditFieldsGST.Document_Number = objReader["Document_Number"].ToString();
                    DebitCreditFieldsGST.Transaction_Type_Code = objReader["Transaction_Type_Code"].ToString();
                    DebitCreditFieldsGST.Transaction_Type_Description = objReader["Transaction_Type_Description"].ToString();
                    DebitCreditFieldsGST.Dr_Cr_Indicator = objReader["Dr_Cr_Indicator"].ToString();
                    DebitCreditFieldsGST.Document_Date = objReader["Document_Date"].ToString();
                    DebitCreditFieldsGST.Branch_Code = objReader["branch_name"].ToString();
                    DebitCreditFieldsGST.Indicator_Code = objReader["Indicator_Code"].ToString();
                    DebitCreditFieldsGST.tr_Branch_Code = objReader["Plant_Code"].ToString();
                    DebitCreditFieldsGST.Reference_Document_Number = objReader["Reference_Document_Number"].ToString();
                    DebitCreditFieldsGST.Remarks = objReader["Remarks"].ToString();
                    DebitCreditFieldsGST.Value = objReader["Adjustment_Value"].ToString();
                    DebitCreditFieldsGST.Tax_Value = objReader["Tax_Amount"].ToString();
                    DebitCreditFieldsGST.Reference_Document_Date = objReader["Reference_Document_Date"].ToString();
                    DebitCreditFieldsGST.Accountingperiod = objReader["Accountingperiod"].ToString();
                    DebitCreditFieldsGST.Indicator = objReader["Indicator"].ToString();
                    DebitCreditFieldsGST.Indicator_Name = objReader["Indicator_Name"].ToString();
                    DebitCreditListGST.Add(DebitCreditFieldsGST);
                }

                objReader.NextResult();
                while (objReader.Read())
                {
                    DebitCreditNoteDetailsGST NoteDetailsGST = new DebitCreditNoteDetailsGST();
                    NoteDetailsGST.Serial_Number = objReader[1].ToString();
                    NoteDetailsGST.Amount = objReader[6].ToString();
                    NoteDetailsGST.Remarks = objReader[7].ToString();
                    NoteDetailsGST.Chart_of_Account_Code = objReader[9].ToString();
                    NoteDetailsGST.SGST_Code = objReader[10].ToString();
                    NoteDetailsGST.SGST_Per = objReader[11].ToString();
                    NoteDetailsGST.SGST_Amt = objReader[12].ToString();
                    NoteDetailsGST.CGST_Code = objReader[13].ToString();
                    NoteDetailsGST.CGST_Per = objReader[14].ToString();
                    NoteDetailsGST.CGST_Amt = objReader[15].ToString();
                    NoteDetailsGST.IGST_Code = objReader[16].ToString();
                    NoteDetailsGST.IGST_Per = objReader[17].ToString();
                    NoteDetailsGST.IGST_Amt = objReader[18].ToString();
                    NoteDetailsGST.UTGST_Code = objReader[19].ToString();
                    NoteDetailsGST.UTGST_Per = objReader[20].ToString();
                    NoteDetailsGST.UTGST_Amt = objReader[21].ToString();
                    DebitCreditListDetailsGST.Add(NoteDetailsGST);
                }
            }

            Note.ListDebitCreditNoteGST = DebitCreditListGST;
            Note.ListDebitCreditNoteDetailsGST = DebitCreditListDetailsGST;

            return Note;
        }

        public DebitCreditNote ViewDebitCreditNoteCustBranch(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDebitCredit_NoteHeader");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            DebitCreditNote Note = new DebitCreditNote();
            List<DebitCreditAdviceFields> DebitCreditList = new List<DebitCreditAdviceFields>();
            List<DebitCreditNoteDetailsCustBranch> DebitCreditListDetails = new List<DebitCreditNoteDetailsCustBranch>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    DebitCreditAdviceFields DebitCreditFields = new DebitCreditAdviceFields();
                    DebitCreditFields.Document_Number = objReader["Document_Number"].ToString();
                    DebitCreditFields.Transaction_Type_Code = objReader["Transaction_Type_Code"].ToString();
                    DebitCreditFields.Transaction_Type_Description = objReader["Transaction_Type_Description"].ToString();
                    DebitCreditFields.Dr_Cr_Indicator = objReader["Dr_Cr_Indicator"].ToString();
                    DebitCreditFields.Document_Date = objReader["Document_Date"].ToString();
                    DebitCreditFields.Branch_Code = objReader["branch_name"].ToString();
                    DebitCreditFields.Indicator_Code = objReader["Indicator_Code"].ToString();
                    DebitCreditFields.Reference_Document_Number = objReader["Reference_Document_Number"].ToString();
                    DebitCreditFields.Remarks = objReader["Remarks"].ToString();
                    DebitCreditFields.Value = objReader["Adjustment_Value"].ToString();
                    DebitCreditFields.Reference_Document_Date = objReader["Reference_Document_Date"].ToString();
                    DebitCreditFields.Accountingperiod = objReader["Accountingperiod"].ToString();
                    DebitCreditFields.Indicator = objReader["Indicator"].ToString();
                    DebitCreditFields.Indicator_Name = objReader["Indicator_Name"].ToString();
                    DebitCreditList.Add(DebitCreditFields);
                }

                objReader.NextResult();
                while (objReader.Read())
                {
                    DebitCreditNoteDetailsCustBranch NoteDetails = new DebitCreditNoteDetailsCustBranch();
                    NoteDetails.Document_Number = objReader[0].ToString();
                    NoteDetails.Serial_Number = objReader[1].ToString();
                    NoteDetails.Dr_Cr_Indicator = objReader[2].ToString();
                    NoteDetails.Item_Code = objReader[3].ToString();
                    NoteDetails.Return_Document_Quantity = objReader[4].ToString();
                    NoteDetails.Return_Actual_Quantity = objReader[5].ToString();
                    NoteDetails.Value = objReader[6].ToString();
                    NoteDetails.Remarks = objReader[7].ToString();
                    NoteDetails.Item_Short_Description = objReader[8].ToString();
                    NoteDetails.Chart_OF_Account_Code = objReader[9].ToString();
                    NoteDetails.ST_Code = objReader[10].ToString();
                    NoteDetails.St_percent = objReader[11].ToString();
                    NoteDetails.st_amount = objReader[12].ToString();
                    NoteDetails.sc_percent = objReader[13].ToString();
                    NoteDetails.sc_amount = objReader[14].ToString();
                    NoteDetails.asc_percent = objReader[15].ToString();
                    NoteDetails.asc_amount = objReader[16].ToString();
                    NoteDetails.tot_percent = objReader[17].ToString();
                    NoteDetails.tot_amount = objReader[18].ToString();
                    NoteDetails.Part_Number = objReader[19].ToString();
                    DebitCreditListDetails.Add(NoteDetails);
                }
            }

            Note.ListDebitCreditNote = DebitCreditList;
            Note.ListDebitCreditNoteDetailsCustBranch = DebitCreditListDetails;

            return Note;
        }

        public DebitCreditNoteGST ViewDebitCreditNoteCustBranchGST(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetDebitCredit_NoteHeader_GST");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            DebitCreditNoteGST Note = new DebitCreditNoteGST();
            List<DebitCreditAdviceFieldsGST> DebitCreditListGST = new List<DebitCreditAdviceFieldsGST>();
            List<DebitCreditNoteDetailsCustBranchGST> DebitCreditListDetailsGST = new List<DebitCreditNoteDetailsCustBranchGST>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    DebitCreditAdviceFieldsGST DebitCreditFieldsGST = new DebitCreditAdviceFieldsGST();
                    DebitCreditFieldsGST.Document_Number = objReader["Document_Number"].ToString();
                    DebitCreditFieldsGST.Transaction_Type_Code = objReader["Transaction_Type_Code"].ToString();
                    DebitCreditFieldsGST.Transaction_Type_Description = objReader["Transaction_Type_Description"].ToString();
                    DebitCreditFieldsGST.Dr_Cr_Indicator = objReader["Dr_Cr_Indicator"].ToString();
                    DebitCreditFieldsGST.Document_Date = objReader["Document_Date"].ToString();
                    DebitCreditFieldsGST.Branch_Code = objReader["branch_name"].ToString();
                    DebitCreditFieldsGST.Indicator_Code = objReader["Indicator_Code"].ToString();
                    DebitCreditFieldsGST.Reference_Document_Number = objReader["Reference_Document_Number"].ToString();
                    DebitCreditFieldsGST.Remarks = objReader["Remarks"].ToString();
                    DebitCreditFieldsGST.Value = objReader["Adjustment_Value"].ToString();
                    DebitCreditFieldsGST.Tax_Value = objReader["Tax_Amount"].ToString();
                    DebitCreditFieldsGST.Reference_Document_Date = objReader["Reference_Document_Date"].ToString();
                    DebitCreditFieldsGST.Accountingperiod = objReader["Accountingperiod"].ToString();
                    DebitCreditFieldsGST.Indicator = objReader["Indicator"].ToString();
                    DebitCreditFieldsGST.Indicator_Name = objReader["Indicator_Name"].ToString();
                    DebitCreditListGST.Add(DebitCreditFieldsGST);
                }

                objReader.NextResult();
                while (objReader.Read())
                {
                    DebitCreditNoteDetailsCustBranchGST NoteDetailsGST = new DebitCreditNoteDetailsCustBranchGST();
                    NoteDetailsGST.Document_Number = objReader[0].ToString();
                    NoteDetailsGST.Serial_Number = objReader[1].ToString();
                    NoteDetailsGST.Dr_Cr_Indicator = objReader[2].ToString();
                    NoteDetailsGST.Item_Code = objReader[3].ToString();
                    NoteDetailsGST.Return_Document_Quantity = objReader[4].ToString();
                    NoteDetailsGST.Return_Actual_Quantity = objReader[5].ToString();
                    NoteDetailsGST.Value = objReader[6].ToString();
                    NoteDetailsGST.Remarks = objReader[7].ToString();
                    NoteDetailsGST.Item_Short_Description = objReader[8].ToString();
                    NoteDetailsGST.Chart_OF_Account_Code = objReader[9].ToString();
                    NoteDetailsGST.SGST_Code = objReader[10].ToString();
                    NoteDetailsGST.SGST_Percent = objReader[11].ToString();
                    NoteDetailsGST.SGST_Amount = objReader[12].ToString();
                    NoteDetailsGST.CGST_Code = objReader[13].ToString();
                    NoteDetailsGST.CGST_Percent = objReader[14].ToString();
                    NoteDetailsGST.CGST_Amount = objReader[15].ToString();
                    NoteDetailsGST.IGST_Code = objReader[16].ToString();
                    NoteDetailsGST.IGST_Percent = objReader[17].ToString();
                    NoteDetailsGST.IGST_Amount = objReader[18].ToString();
                    NoteDetailsGST.UTGST_Code = objReader[19].ToString();
                    NoteDetailsGST.UTGST_Percent = objReader[20].ToString();
                    NoteDetailsGST.UTGST_Amount = objReader[21].ToString();
                    NoteDetailsGST.Part_Number = objReader[22].ToString();
                    DebitCreditListDetailsGST.Add(NoteDetailsGST);
                }
            }

            Note.ListDebitCreditNoteGST = DebitCreditListGST;
            Note.ListDebitCreditNoteDetailsCustBranchGST = DebitCreditListDetailsGST;

            return Note;
        }

        public string AddNewDebitCreditNoteSupplier(ref DebitCreditNoteEntityGST DrCrNoteEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string DocNumber = string.Empty;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (DebitCreditNoteDetailsGST DebitCreditItem in DrCrNoteEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCreditAdvice_GST");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, (DocNumber == string.Empty ? DrCrNoteEntity.Document_Number : DocNumber));
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, DrCrNoteEntity.Transaction_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, DrCrNoteEntity.Dr_Cr_Indicator);
                        ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, DrCrNoteEntity.Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, DrCrNoteEntity.Supplier_Code);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, DrCrNoteEntity.Customer_Code);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, DrCrNoteEntity.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, DrCrNoteEntity.Value);
                        ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, DrCrNoteEntity.GSTValue);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, DrCrNoteEntity.Reference_Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, DebitCreditItem.Item_Code);
                        ImpalDB.AddInParameter(cmd, "@Return_Document_Quantity", DbType.String, DebitCreditItem.Return_Document_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Return_Actual_Quantity", DbType.String, DebitCreditItem.Return_Actual_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Value", DbType.String, DebitCreditItem.Amount);
                        ImpalDB.AddInParameter(cmd, "@Remarks_Detail", DbType.String, DebitCreditItem.Remarks_Detail);
                        ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, DebitCreditItem.Chart_of_Account_Code);                        
                        ImpalDB.AddInParameter(cmd, "@tr_branch_code", DbType.String, DrCrNoteEntity.tr_Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd, "@ST_Code", DbType.String, DebitCreditItem.SGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ST_Per", DbType.String, DebitCreditItem.SGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ST_Amt", DbType.String, DebitCreditItem.SGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@SC_Code", DbType.String, DebitCreditItem.CGST_Code);
                        ImpalDB.AddInParameter(cmd, "@SC_Per", DbType.String, DebitCreditItem.CGST_Per);
                        ImpalDB.AddInParameter(cmd, "@SC_Amt", DbType.String, DebitCreditItem.CGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@ASC_Code", DbType.String, DebitCreditItem.IGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ASC_Per", DbType.String, DebitCreditItem.IGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ASC_Amt", DbType.String, DebitCreditItem.IGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@TOT_Code", DbType.String, DebitCreditItem.UTGST_Code);
                        ImpalDB.AddInParameter(cmd, "@TOT_Per", DbType.String, DebitCreditItem.UTGST_Per);
                        ImpalDB.AddInParameter(cmd, "@Tot_Amt", DbType.String, DebitCreditItem.UTGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@Serial_number", DbType.String, "0");
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Selected_Ind", DbType.String, DrCrNoteEntity.DrCrSelectedInd);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    if (DocNumber != "")
                    {
                        DbCommand cmd1 = ImpalDB.GetStoredProcCommand("Usp_addgldaca1_GST");
                        ImpalDB.AddInParameter(cmd1, "@Doc_No", DbType.String, DocNumber);
                        ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd1, "@RoundOffCA", DbType.String, DrCrNoteEntity.Roundoff);
                        ImpalDB.AddInParameter(cmd1, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd1, "@Dr_Cr_Selected_Ind", DbType.String, DrCrNoteEntity.DrCrSelectedInd);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd1);
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                DocNumber = "";
                throw exp;
            }
            return DocNumber;
        }

        public string AddNewDebitCreditNoteCustBranch(ref DebitCreditNoteEntityGST DrCrNoteEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string DocNumber = string.Empty;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (DebitCreditNoteDetailsGST DebitCreditItem in DrCrNoteEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCreditAdvice_GST");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, (DocNumber == string.Empty ? DrCrNoteEntity.Document_Number : DocNumber));
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, DrCrNoteEntity.Transaction_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, DrCrNoteEntity.Dr_Cr_Indicator);
                        ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, DrCrNoteEntity.Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, DrCrNoteEntity.Supplier_Code);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, DrCrNoteEntity.Customer_Code);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, DrCrNoteEntity.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, DrCrNoteEntity.Value);
                        ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, DrCrNoteEntity.GSTValue);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, DrCrNoteEntity.Reference_Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, DebitCreditItem.Item_Code);
                        ImpalDB.AddInParameter(cmd, "@Return_Document_Quantity", DbType.String, DebitCreditItem.Return_Document_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Return_Actual_Quantity", DbType.String, DebitCreditItem.Return_Actual_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Value", DbType.String, DebitCreditItem.Amount);
                        ImpalDB.AddInParameter(cmd, "@Remarks_Detail", DbType.String, DebitCreditItem.Remarks_Detail);
                        ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, DebitCreditItem.Chart_of_Account_Code);                        
                        ImpalDB.AddInParameter(cmd, "@tr_branch_code", DbType.String, DrCrNoteEntity.tr_Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd, "@ST_Code", DbType.String, DebitCreditItem.SGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ST_Per", DbType.String, DebitCreditItem.SGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ST_Amt", DbType.String, DebitCreditItem.SGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@SC_Code", DbType.String, DebitCreditItem.CGST_Code);
                        ImpalDB.AddInParameter(cmd, "@SC_Per", DbType.String, DebitCreditItem.CGST_Per);
                        ImpalDB.AddInParameter(cmd, "@SC_Amt", DbType.String, DebitCreditItem.CGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@ASC_Code", DbType.String, DebitCreditItem.IGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ASC_Per", DbType.String, DebitCreditItem.IGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ASC_Amt", DbType.String, DebitCreditItem.IGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@TOT_Code", DbType.String, DebitCreditItem.UTGST_Code);
                        ImpalDB.AddInParameter(cmd, "@TOT_Per", DbType.String, DebitCreditItem.UTGST_Per);
                        ImpalDB.AddInParameter(cmd, "@Tot_Amt", DbType.String, DebitCreditItem.UTGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@Serial_number", DbType.String, "0");
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Selected_Ind", DbType.String, "");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    }

                    if (DocNumber != "")
                    {
                        if (DrCrNoteEntity.Indicator == "Customer")
                        {
                            DbCommand cmd3 = ImpalDB.GetStoredProcCommand("usp_updcustos_daca");
                            ImpalDB.AddInParameter(cmd3, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                            ImpalDB.AddInParameter(cmd3, "@Doc_No", DbType.String, DocNumber);
                            cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd3);
                        }

                        DbCommand cmd5 = ImpalDB.GetStoredProcCommand("Usp_addgldaca1_GST");                        
                        ImpalDB.AddInParameter(cmd5, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd5, "@Doc_No", DbType.String, DocNumber);
                        ImpalDB.AddInParameter(cmd5, "@RoundOffCA", DbType.String, DrCrNoteEntity.Roundoff);
                        ImpalDB.AddInParameter(cmd5, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd5, "@Dr_Cr_Selected_Ind", DbType.String, "");
                        cmd5.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd5);
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                DocNumber = "";
                throw exp;
            }
            return DocNumber;
        }

        public DataSet GetEinvoicingDetailsDrCrCust(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_DrCr_Cust_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsDrCrBranch(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_DrCr_Branch_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsDrCrSupplier(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_DrCr_Supplier_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public string AddNewDebitCreditNoteCustBranchCA(ref DebitCreditNoteEntityGST DrCrNoteEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string DocNumber = string.Empty;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (DebitCreditNoteDetailsGST DebitCreditItem in DrCrNoteEntity.Items)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCreditAdvice_GST");
                        ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, (DocNumber == string.Empty ? DrCrNoteEntity.Document_Number : DocNumber));
                        ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, DrCrNoteEntity.Transaction_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, DrCrNoteEntity.Dr_Cr_Indicator);
                        ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, DrCrNoteEntity.Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, DrCrNoteEntity.Supplier_Code);
                        ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, DrCrNoteEntity.Customer_Code);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                        ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, DrCrNoteEntity.Remarks);
                        ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, DrCrNoteEntity.TotalValue);
                        ImpalDB.AddInParameter(cmd, "@Tax_Value", DbType.String, DrCrNoteEntity.GSTValue);
                        ImpalDB.AddInParameter(cmd, "@Reference_Document_Date", DbType.String, DrCrNoteEntity.Reference_Document_Date);
                        ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, DebitCreditItem.Item_Code);
                        ImpalDB.AddInParameter(cmd, "@Return_Document_Quantity", DbType.String, DebitCreditItem.Return_Document_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Return_Actual_Quantity", DbType.String, DebitCreditItem.Return_Actual_Quantity);
                        ImpalDB.AddInParameter(cmd, "@Value", DbType.String, DebitCreditItem.Amount);
                        ImpalDB.AddInParameter(cmd, "@Remarks_Detail", DbType.String, DebitCreditItem.Remarks_Detail);
                        ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, DebitCreditItem.Chart_of_Account_Code);                        
                        ImpalDB.AddInParameter(cmd, "@tr_branch_code", DbType.String, DrCrNoteEntity.tr_Branch_Code);
                        ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd, "@ST_Code", DbType.String, DebitCreditItem.SGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ST_Per", DbType.String, DebitCreditItem.SGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ST_Amt", DbType.String, DebitCreditItem.SGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@SC_Code", DbType.String, DebitCreditItem.CGST_Code);
                        ImpalDB.AddInParameter(cmd, "@SC_Per", DbType.String, DebitCreditItem.CGST_Per);
                        ImpalDB.AddInParameter(cmd, "@SC_Amt", DbType.String, DebitCreditItem.CGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@ASC_Code", DbType.String, DebitCreditItem.IGST_Code);
                        ImpalDB.AddInParameter(cmd, "@ASC_Per", DbType.String, DebitCreditItem.IGST_Per);
                        ImpalDB.AddInParameter(cmd, "@ASC_Amt", DbType.String, DebitCreditItem.IGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@TOT_Code", DbType.String, DebitCreditItem.UTGST_Code);
                        ImpalDB.AddInParameter(cmd, "@TOT_Per", DbType.String, DebitCreditItem.UTGST_Per);
                        ImpalDB.AddInParameter(cmd, "@Tot_Amt", DbType.String, DebitCreditItem.UTGST_Amt);
                        ImpalDB.AddInParameter(cmd, "@Serial_number", DbType.String, DebitCreditItem.Serial_Number);
                        ImpalDB.AddInParameter(cmd, "@Dr_Cr_Selected_Ind", DbType.String, "");
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        DocNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                        if (!(DrCrNoteEntity.Transaction_Type_Code == "658" || DrCrNoteEntity.Transaction_Type_Code == "758" || DrCrNoteEntity.Transaction_Type_Code == "656" || DrCrNoteEntity.Transaction_Type_Code == "756" || DrCrNoteEntity.Transaction_Type_Code == "653"))
                        {
                            if (DrCrNoteEntity.Indicator == "Customer")
                            {
                                DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_UpdSalescredit_GST");
                                ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                                ImpalDB.AddInParameter(cmd1, "@Document_Number", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                                ImpalDB.AddInParameter(cmd1, "@Return_Quantity", DbType.String, DebitCreditItem.Return_Actual_Quantity);
                                ImpalDB.AddInParameter(cmd1, "@Item_code", DbType.String, DebitCreditItem.Item_Code);
                                ImpalDB.AddInParameter(cmd1, "@Transaction_Type_Code", DbType.String, DrCrNoteEntity.Transaction_Type_Code);
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd1);
                            }
                            else if (DrCrNoteEntity.Indicator == "Branch")
                            {
                                DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_AddConstdn_GST");
                                ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                                ImpalDB.AddInParameter(cmd2, "@Inward_Number", DbType.String, DocNumber);
                                ImpalDB.AddInParameter(cmd2, "@Issue_Document_Number", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                                ImpalDB.AddInParameter(cmd2, "@Return_Quantity", DbType.String, DebitCreditItem.Return_Actual_Quantity);
                                ImpalDB.AddInParameter(cmd2, "@Item_code", DbType.String, DebitCreditItem.Item_Code);
                                ImpalDB.AddInParameter(cmd2, "@Transaction_Type_Code", DbType.String, DrCrNoteEntity.Transaction_Type_Code);                                
                                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd2);
                            }
                        }
                    }

                    if (DocNumber != "")
                    {
                        if (DrCrNoteEntity.Indicator == "Customer")
                        {
                            DbCommand cmd3 = ImpalDB.GetStoredProcCommand("usp_updcustos_daca");
                            ImpalDB.AddInParameter(cmd3, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                            ImpalDB.AddInParameter(cmd3, "@Doc_No", DbType.String, DocNumber);
                            cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd3);
                        }
                        if (DrCrNoteEntity.Indicator == "Branch")
                        {
                            DbCommand cmd4 = ImpalDB.GetStoredProcCommand("usp_updstdn_daca");
                            ImpalDB.AddInParameter(cmd4, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                            ImpalDB.AddInParameter(cmd4, "@Doc_No", DbType.String, DocNumber);
                            ImpalDB.AddInParameter(cmd4, "@STDN_No", DbType.String, DrCrNoteEntity.Reference_Document_Number);
                            ImpalDB.AddInParameter(cmd4, "@Remarks", DbType.String, DrCrNoteEntity.Remarks);
                            cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd4);
                        }

                        DbCommand cmd5 = ImpalDB.GetStoredProcCommand("Usp_addgldaca1_new_GST");                        
                        ImpalDB.AddInParameter(cmd5, "@Branch_Code", DbType.String, DrCrNoteEntity.Branch_Code);
                        ImpalDB.AddInParameter(cmd5, "@Doc_No", DbType.String, DocNumber);
                        ImpalDB.AddInParameter(cmd5, "@TurnOverValue", DbType.String, DrCrNoteEntity.Value);
                        ImpalDB.AddInParameter(cmd5, "@GSTValue", DbType.String, DrCrNoteEntity.GSTValue);
                        ImpalDB.AddInParameter(cmd5, "@Indicator", DbType.String, DrCrNoteEntity.Indicator);
                        ImpalDB.AddInParameter(cmd5, "@DocStatus", DbType.String, DrCrNoteEntity.DocExists);
                        ImpalDB.AddInParameter(cmd5, "@RoundOffCA", DbType.String, DrCrNoteEntity.Roundoff);
                        cmd5.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd5);
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                DocNumber = "";
                throw exp;
            }
            return DocNumber;
        }

        public int ValidateCA(string BranchCode, string DocumentNumber, string Code, string CustBranchInd)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_validateDebitCredit");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
            ImpalDB.AddInParameter(cmd, "@Code", DbType.String, Code);
            ImpalDB.AddInParameter(cmd, "@CustBranchInd", DbType.String, CustBranchInd);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int Result = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd).ToString());
            return Result;
        }

        public RefDocDate GetReferenceDocNumberDate(string BranchCode, string DocumentNumber, string CustBrCode, string CustBranchInd)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("GetReferenceDocNumber");
			ImpalDB.AddInParameter(cmd, "@Branchcode", DbType.String, BranchCode);																   
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber);
            ImpalDB.AddInParameter(cmd, "@CustBrCode", DbType.String, CustBrCode);
            ImpalDB.AddInParameter(cmd, "@CustBranchInd", DbType.String, CustBranchInd);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            RefDocDate refdate = new RefDocDate();
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    refdate.RefCode = objReader["RefCode"].ToString();
                    refdate.RefDate = objReader["RefDate"].ToString();
                }
            }
            return refdate;
        }

        public List<string> GetInvoiceDebitCreditNoteQR(string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, "S");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstInvoiceSTDN.Add(reader[0].ToString());
                }
            }

            if (lstInvoiceSTDN.Count <= 0)
                lstInvoiceSTDN.Add("");

            return lstInvoiceSTDN;
        }

        public DataSet GetEinvoicingDetailsDebitCreditNote(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_DebitCredit_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsDebitCreditNoteOld(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_DebitCredit_Data_Old");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public int CheckDocumentNumber(string Document_number, string AccountCode, string BranchCode, string DrIndicator)
        {
            int Count = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select start_date, end_date from accounting_period where accounting_period_code ='" + AccountCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    StartDate = reader[0].ToString();
                    EndDate = reader[1].ToString();
                }
            }

            sSQL = "select 1 from debit_credit_note_header where case when len(document_number)=15 then substring(Document_Number,4,5) else substring(Document_Number,6,5) end ='" + Document_number + "' and " +
                 "case when len(document_number)=15 then substring(Document_Number,14,2) else substring(Document_Number,16,2) end = '" + DrIndicator + "' and " +
                 "document_date >= '" + StartDate + "' and " +
                 "document_date <= '" + EndDate + "' and branch_code = '" + BranchCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            Count = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd1));
            return Count;
        }

		public int CheckAutoTODstatus(string Branch_Code, string Supplier_Code)
        {
            int Count = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("select count(1) from TOD_Target_Master where Branch_Code ='" + Branch_Code + "' and Supplier_Code='" + Supplier_Code + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Count = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            return Count;
        }
		
        public string AddCreditNoteTOD(ref TODGenerationEntity todGenerationEntity, string MonthYear)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string DocNumber = string.Empty;
            string InvoiceNumber = string.Empty;
            string InvoiceDate = string.Empty;
            string CustomerCode = string.Empty;
            DbCommand cmd = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    //foreach (TODGenerationItem todGenerationItem in todGenerationEntity.Items)
                    //{
                    //    cmd = ImpalDB.GetStoredProcCommand("usp_updDebitCredit_TODdetails");
                    //    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, todGenerationEntity.BranchCode);
                    //    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, todGenerationEntity.SupplierCode);
                    //    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, todGenerationEntity.CustomerCode);
                    //    ImpalDB.AddInParameter(cmd, "@Reference_Document_Number", DbType.String, todGenerationItem.InvoiceNumber);
                    //    ImpalDB.AddInParameter(cmd, "@Month_Year", DbType.String, MonthYear);
                    //    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    //    ImpalDB.ExecuteNonQuery(cmd);

                    //    InvoiceNumber = todGenerationItem.InvoiceNumber;
                    //    InvoiceDate = todGenerationItem.InvoiceDate;
                    //}

                    //cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_AddDebitCredit_TOD");
                    ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, "658");
                    ImpalDB.AddInParameter(cmd, "@Dr_Cr_Indicator", DbType.String, "DA");
                    ImpalDB.AddInParameter(cmd, "@Document_Date", DbType.String, todGenerationEntity.DocumentDate);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, todGenerationEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, todGenerationEntity.SupplierCode);
                    ImpalDB.AddInParameter(cmd, "@Supplier_Plant", DbType.String, todGenerationEntity.SupplyPlant);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, todGenerationEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.String, todGenerationEntity.SLBValue);
                    ImpalDB.AddInParameter(cmd, "@TOD_Type", DbType.String, todGenerationEntity.TODType);
                    ImpalDB.AddInParameter(cmd, "@Adjustment_Value", DbType.String, todGenerationEntity.TotalTODValue);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, "Auto TOD Credit Note - " + todGenerationEntity.CustomerName);
                    ImpalDB.AddInParameter(cmd, "@Chart_Of_Account_Code", DbType.String, "10000103200000001" + todGenerationEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Month_Year", DbType.String, MonthYear);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    DocNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_updcustos_TOD_daca");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, todGenerationEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, todGenerationEntity.CustomerCode);
                    ImpalDB.AddInParameter(cmd, "@Doc_No", DbType.String, DocNumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    cmd = null;

                    cmd = ImpalDB.GetStoredProcCommand("usp_addgldaca_TOD");
                    ImpalDB.AddInParameter(cmd, "@Doc_No", DbType.String, DocNumber);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, todGenerationEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, todGenerationEntity.TODType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    cmd = null;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                DocNumber = "";
                throw exp;
            }
            return DocNumber;
        }

        public List<CreditSalesTaxDetails> GetCATaxDetails(string BranchCode, string DocumentNumber, string CustBranchInd)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCreditData");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@CustBranchInd", DbType.String, CustBranchInd.Trim());
            List<CreditSalesTaxDetails> DebitCreditList = new List<CreditSalesTaxDetails>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    CreditSalesTaxDetails DebitCreditFields = new CreditSalesTaxDetails();
                    DebitCreditFields.supplier_part_number = objReader["supplier_part_number"].ToString();
                    DebitCreditFields.Item_Quantity = objReader["Item_Quantity"].ToString();
                    DebitCreditFields.value = objReader["value"].ToString();
                    DebitCreditFields.return_quantity = objReader["return_quantity"].ToString();
                    DebitCreditFields.item_code = objReader["item_code"].ToString();
                    DebitCreditFields.VatSalesPer = objReader["VatSalesPer"].ToString();
                    DebitCreditFields.VatSalesCode = objReader["VatSalesCode"].ToString();
                    DebitCreditFields.StSalesPer = objReader["StSalesPer"].ToString();
                    DebitCreditFields.StSalesCode = objReader["StSalesCode"].ToString();
                    DebitCreditFields.SCSalesPer = objReader["SCSalesPer"].ToString();
                    DebitCreditFields.SCSalesCode = objReader["SCSalesCode"].ToString();
                    DebitCreditFields.ASCSalesCode = objReader["ASCSalesCode"].ToString();
                    DebitCreditFields.ASCSalesPer = objReader["ASCSalesPer"].ToString();
                    DebitCreditFields.RSTSalesPer = objReader["RSTSalesPer"].ToString();
                    DebitCreditFields.RSTSalesCode = objReader["RSTSalesCode"].ToString();
                    DebitCreditFields.TOTSalesPer = objReader["TOTSalesPer"].ToString();
                    DebitCreditFields.TOTSalesCode = objReader["TOTSalesCode"].ToString();
                    DebitCreditFields.CESSSalesPer = objReader["CESSSalesPer"].ToString();
                    DebitCreditFields.CESSSalesCode = objReader["CESSSalesCode"].ToString();
                    DebitCreditFields.CouponCharges = objReader["CouponCharges"].ToString();
                    DebitCreditList.Add(DebitCreditFields);
                }
            }

            return DebitCreditList;
        }

        public List<CreditSalesTaxDetailsGST> GetCATaxDetailsGST(string BranchCode,string DocumentNumber, string TransactionType, string CustBranchInd, string GSTIN)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetCreditData_GST");
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Transaction_Type", DbType.String, TransactionType.Trim());
            ImpalDB.AddInParameter(cmd, "@CustBranchInd", DbType.String, CustBranchInd.Trim());
            ImpalDB.AddInParameter(cmd, "@CustomerGSTIN", DbType.String, GSTIN.Trim());
            List<CreditSalesTaxDetailsGST> DebitCreditListGST = new List<CreditSalesTaxDetailsGST>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    CreditSalesTaxDetailsGST DebitCreditFieldsGST = new CreditSalesTaxDetailsGST();
                    DebitCreditFieldsGST.supplier_part_number = objReader["supplier_part_number"].ToString();
                    DebitCreditFieldsGST.Item_Quantity = objReader["Item_Quantity"].ToString();
                    DebitCreditFieldsGST.value = objReader["value"].ToString();
                    DebitCreditFieldsGST.return_quantity = objReader["return_quantity"].ToString();
                    DebitCreditFieldsGST.item_code = objReader["item_code"].ToString();
                    DebitCreditFieldsGST.SGSTSalesPer = objReader["SGSTSalesPer"].ToString();
                    DebitCreditFieldsGST.SGSTSalesCode = objReader["SGSTSalesCode"].ToString();
                    DebitCreditFieldsGST.CGSTSalesPer = objReader["CGSTSalesPer"].ToString();
                    DebitCreditFieldsGST.CGSTSalesCode = objReader["CGSTSalesCode"].ToString();
                    DebitCreditFieldsGST.IGSTSalesPer = objReader["IGSTSalesPer"].ToString();
                    DebitCreditFieldsGST.IGSTSalesCode = objReader["IGSTSalesCode"].ToString();
                    DebitCreditFieldsGST.UTGSTSalesPer = objReader["UTGSTSalesPer"].ToString();
                    DebitCreditFieldsGST.UTGSTSalesCode = objReader["UTGSTSalesCode"].ToString();
                    DebitCreditFieldsGST.CouponCharges = objReader["CouponCharges"].ToString();
                    DebitCreditFieldsGST.Serial_Number = objReader["Serial_Number"].ToString();
                    DebitCreditListGST.Add(DebitCreditFieldsGST);
                }
            }

            return DebitCreditListGST;
        }
    }

    public class DocumentNumber
    {
        private string _Document_Number;

        public DocumentNumber(string Document_Number)
        {
            _Document_Number = Document_Number;
        }

        public string Document_Number
        {
            get { return _Document_Number; }
            set { _Document_Number = value; }
        }
    }

    public class TransactionType
    {
        public TransactionType(string Transaction_Type_code, string Transaction_Type_Description)
        {
            _Transaction_Type_code = Transaction_Type_code;
            _Transaction_Type_Description = Transaction_Type_Description;
        }

        private string _Transaction_Type_code;
        private string _Transaction_Type_Description;

        public string Transaction_Type_code
        {
            get { return _Transaction_Type_code; }
            set { _Transaction_Type_code = value; }
        }

        public string Transaction_Type_Description
        {
            get { return _Transaction_Type_Description; }
            set { _Transaction_Type_Description = value; }
        }
    }

    public class DebitCreditAdviceFields
    {
        public string Document_Number { get; set; }
        public string Accountingperiod { get; set; }
        public string Document_Date { get; set; }
        public string Branch_Code { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Indicator { get; set; }
        public string Indicator_Code { get; set; }
        public string Indicator_Name { get; set; }
        public string Supplier_Code { get; set; }
        public string Transaction_Type_Code { get; set; }
        public string Transaction_Type_Description { get; set; }
        public string Reference_Document_Number { get; set; }
        public string Reference_Document_Date { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Customer_Code { get; set; }
        public string tr_Branch_Code { get; set; }
    }

    public class DebitCreditNoteEntity
    {
        public string Document_Number { get; set; }
        public string Accountingperiod { get; set; }
        public string Document_Date { get; set; }
        public string Branch_Code { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Indicator { get; set; }
        public string Supplier_Code { get; set; }
        public string Transaction_Type_Code { get; set; }
        public string Reference_Document_Number { get; set; }
        public string Reference_Document_Date { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public string tr_Branch_Code { get; set; }

        public string Customer_Code { get; set; }

        public List<DebitCreditNoteDetails> Items { get; set; }
    }

    public class DebitCreditNoteDetails
    {
        public string Chart_of_Account_Code { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }

        public string Item_Code { get; set; }
        public string Return_Document_Quantity { get; set; }
        public string Return_Actual_Quantity { get; set; }
        public string Remarks_Detail { get; set; }
        public string ST_Code { get; set; }
        public string ST_Per { get; set; }
        public string ST_Amt { get; set; }
        public string SC_Code { get; set; }
        public string SC_Per { get; set; }
        public string SC_Amt { get; set; }
        public string ASC_Code { get; set; }
        public string ASC_Per { get; set; }
        public string ASC_Amt { get; set; }
        public string TOT_Code { get; set; }
        public string TOT_Per { get; set; }
        public string Tot_Amt { get; set; }

        public string ChartOfAccount { get; set; }
        public int RowNumber { get; set; }
    }

    public class DebitCreditNote
    {
        public DebitCreditNote()
        {
        }

        public List<DebitCreditAdviceFields> ListDebitCreditNote { get; set; }

        public List<DebitCreditNoteDetails> ListDebitCreditNoteDetails { get; set; }

        public List<DebitCreditNoteDetailsCustBranch> ListDebitCreditNoteDetailsCustBranch { get; set; }
    }

    public class DebitCreditAdviceFieldsGST
    {
        public string Document_Number { get; set; }
        public string Accountingperiod { get; set; }
        public string Document_Date { get; set; }
        public string Branch_Code { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Indicator { get; set; }
        public string Indicator_Code { get; set; }
        public string Indicator_Name { get; set; }
        public string Supplier_Code { get; set; }
        public string Transaction_Type_Code { get; set; }
        public string Transaction_Type_Description { get; set; }
        public string Reference_Document_Number { get; set; }
        public string Reference_Document_Date { get; set; }
        public string Value { get; set; }
        public string Tax_Value { get; set; }
        public string Remarks { get; set; }
        public string Chart_of_Account_Code { get; set; }
        public string Customer_Code { get; set; }
        public string tr_Branch_Code { get; set; }
    }

    public class DebitCreditNoteEntityGST
    {
        public string Document_Number { get; set; }
        public string Accountingperiod { get; set; }
        public string Document_Date { get; set; }
        public string Branch_Code { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Indicator { get; set; }
        public string Supplier_Code { get; set; }
        public string Transaction_Type_Code { get; set; }      
        public string Reference_Document_Number { get; set; }
        public string Reference_Document_Date { get; set; }
        public string TotalValue { get; set; }
        public string Value { get; set; }
        public string GSTValue { get; set; }
        public string Remarks { get; set; }
        public string tr_Branch_Code { get; set; }
        public string Roundoff { get; set; }
        public string DocExists { get; set; }
        public string DrCrSelectedInd { get; set; }

        public string Customer_Code { get; set; }

        public List<DebitCreditNoteDetailsGST> Items { get; set; }
    }

    public class DebitCreditNoteDetailsGST
    {
        public string Chart_of_Account_Code { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string Document_Number { get; set; }
        public string Serial_Number { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Item_Code { get; set; }
        public string Return_Document_Quantity { get; set; }
        public string Return_Actual_Quantity { get; set; }
        public string Remarks_Detail { get; set; }
        public string SGST_Code { get; set; }
        public string SGST_Per { get; set; }
        public string SGST_Amt { get; set; }
        public string CGST_Code { get; set; }
        public string CGST_Per { get; set; }
        public string CGST_Amt { get; set; }
        public string IGST_Code { get; set; }
        public string IGST_Per { get; set; }
        public string IGST_Amt { get; set; }
        public string UTGST_Code { get; set; }
        public string UTGST_Per { get; set; }
        public string UTGST_Amt { get; set; }

        public string ChartOfAccount { get; set; }
        public int RowNumber { get; set; }
    }

    public class DebitCreditNoteGST
    {
        public DebitCreditNoteGST()
        {
        }

        public List<DebitCreditAdviceFieldsGST> ListDebitCreditNoteGST { get; set; }

        public List<DebitCreditNoteDetailsGST> ListDebitCreditNoteDetailsGST { get; set; }

        public List<DebitCreditNoteDetailsCustBranchGST> ListDebitCreditNoteDetailsCustBranchGST { get; set; }
    }
    

    public class SalesTaxCode
    {
        public SalesTaxCode(string sales_tax_code, string Sales_Tax_Percentage)
        {
            _sales_tax_code = sales_tax_code;
            _Sales_Tax_Percentage = Sales_Tax_Percentage;
        }

        private string _sales_tax_code;
        private string _Sales_Tax_Percentage;

        public string sales_tax_code
        {
            get { return _sales_tax_code; }
            set { _sales_tax_code = value; }
        }

        public string Sales_Tax_Percentage
        {
            get { return _Sales_Tax_Percentage; }
            set { _Sales_Tax_Percentage = value; }
        }
    }

    public class CreditSalesTaxDetails
    {
        public CreditSalesTaxDetails()
        {
        }

        private string _supplier_part_number;
        private string _Item_Quantity;
        private string _value;
        private string _return_quantity;
        private string _item_code;
        private string _VatSalesPer;
        private string _VatSalesCode;
        private string _StSalesPer;
        private string _StSalesCode;
        private string _SCSalesPer;
        private string _SCSalesCode;
        private string _ASCSalesCode;
        private string _ASCSalesPer;
        private string _RSTSalesPer;
        private string _RSTSalesCode;
        private string _TOTSalesPer;
        private string _TOTSalesCode;
        private string _CESSSalesPer;
        private string _CESSSalesCode;
        private string _CouponCharges;

        public string supplier_part_number
        {
            get { return _supplier_part_number; }
            set { _supplier_part_number = value; }
        }

        public string Item_Quantity
        {
            get { return _Item_Quantity; }
            set { _Item_Quantity = value; }
        }

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string return_quantity
        {
            get { return _return_quantity; }
            set { _return_quantity = value; }
        }

        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }

        public string VatSalesPer
        {
            get { return _VatSalesPer; }
            set { _VatSalesPer = value; }
        }

        public string VatSalesCode
        {
            get { return _VatSalesCode; }
            set { _VatSalesCode = value; }
        }

        public string StSalesPer
        {
            get { return _StSalesPer; }
            set { _StSalesPer = value; }
        }

        public string StSalesCode
        {
            get { return _StSalesCode; }
            set { _StSalesCode = value; }
        }

        public string SCSalesPer
        {
            get { return _SCSalesPer; }
            set { _SCSalesPer = value; }
        }

        public string SCSalesCode
        {
            get { return _SCSalesCode; }
            set { _SCSalesCode = value; }
        }

        public string ASCSalesPer
        {
            get { return _ASCSalesPer; }
            set { _ASCSalesPer = value; }
        }

        public string ASCSalesCode
        {
            get { return _ASCSalesCode; }
            set { _ASCSalesCode = value; }
        }

        public string RSTSalesPer
        {
            get { return _RSTSalesPer; }
            set { _RSTSalesPer = value; }
        }

        public string RSTSalesCode
        {
            get { return _RSTSalesCode; }
            set { _RSTSalesCode = value; }
        }

        public string TOTSalesPer
        {
            get { return _TOTSalesPer; }
            set { _TOTSalesPer = value; }
        }

        public string TOTSalesCode
        {
            get { return _TOTSalesCode; }
            set { _TOTSalesCode = value; }
        }

        public string CESSSalesPer
        {
            get { return _CESSSalesPer; }
            set { _CESSSalesPer = value; }
        }

        public string CESSSalesCode
        {
            get { return _CESSSalesCode; }
            set { _CESSSalesCode = value; }
        }

        public string CouponCharges
        {
            get { return _CouponCharges; }
            set { _CouponCharges = value; }
        }
    }

    public class CreditSalesTaxDetailsGST
    {
        public CreditSalesTaxDetailsGST()
        {
        }

        private string _supplier_part_number;
        private string _Item_Quantity;
        private string _value;
        private string _return_quantity;
        private string _item_code;
        private string _Serial_Number;
        private string _SGSTSalesPer;
        private string _SGSTSalesCode;
        private string _CGSTSalesPer;
        private string _CGSTSalesCode;
        private string _IGSTSalesPer;
        private string _IGSTSalesCode;
        private string _UTGSTSalesCode;
        private string _UTGSTSalesPer;
        private string _CouponCharges;

        public string supplier_part_number
        {
            get { return _supplier_part_number; }
            set { _supplier_part_number = value; }
        }

        public string Item_Quantity
        {
            get { return _Item_Quantity; }
            set { _Item_Quantity = value; }
        }

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string return_quantity
        {
            get { return _return_quantity; }
            set { _return_quantity = value; }
        }

        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }

        public string Serial_Number
        {
            get { return _Serial_Number; }
            set { _Serial_Number = value; }
        }

        public string SGSTSalesPer
        {
            get { return _SGSTSalesPer; }
            set { _SGSTSalesPer = value; }
        }

        public string SGSTSalesCode
        {
            get { return _SGSTSalesCode; }
            set { _SGSTSalesCode = value; }
        }

        public string CGSTSalesPer
        {
            get { return _CGSTSalesPer; }
            set { _CGSTSalesPer = value; }
        }

        public string CGSTSalesCode
        {
            get { return _CGSTSalesCode; }
            set { _CGSTSalesCode = value; }
        }

        public string IGSTSalesPer
        {
            get { return _IGSTSalesPer; }
            set { _IGSTSalesPer = value; }
        }

        public string IGSTSalesCode
        {
            get { return _IGSTSalesCode; }
            set { _IGSTSalesCode = value; }
        }

        public string UTGSTSalesPer
        {
            get { return _UTGSTSalesPer; }
            set { _UTGSTSalesPer = value; }
        }

        public string UTGSTSalesCode
        {
            get { return _UTGSTSalesCode; }
            set { _UTGSTSalesCode = value; }
        }

        public string CouponCharges
        {
            get { return _CouponCharges; }
            set { _CouponCharges = value; }
        }
    }

    public class DebitCreditNoteDetailsCustBranch
    {
        public string Document_Number { get; set; }
        public string Serial_Number { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Item_Code { get; set; }
        public string Return_Document_Quantity { get; set; }
        public string Return_Actual_Quantity { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public string Item_Short_Description { get; set; }
        public string Chart_OF_Account_Code { get; set; }
        public string ST_Code { get; set; }
        public string St_percent { get; set; }
        public string st_amount { get; set; }
        public string sc_percent { get; set; }
        public string sc_amount { get; set; }
        public string asc_percent { get; set; }
        public string asc_amount { get; set; }
        public string tot_percent { get; set; }
        public string tot_amount { get; set; }
        public string Part_Number { get; set; }
    }

    public class DebitCreditNoteDetailsCustBranchGST
    {
        public string Document_Number { get; set; }
        public string Serial_Number { get; set; }
        public string Dr_Cr_Indicator { get; set; }
        public string Item_Code { get; set; }
        public string Return_Document_Quantity { get; set; }
        public string Return_Actual_Quantity { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public string Item_Short_Description { get; set; }
        public string Chart_OF_Account_Code { get; set; }
        public string SGST_Code { get; set; }
        public string SGST_Percent { get; set; }
        public string SGST_Amount { get; set; }
        public string CGST_Code { get; set; }
        public string CGST_Percent { get; set; }
        public string CGST_Amount { get; set; }
        public string IGST_Code { get; set; }
        public string IGST_Percent { get; set; }
        public string IGST_Amount { get; set; }
        public string UTGST_Code { get; set; }
        public string UTGST_Percent { get; set; }
        public string UTGST_Amount { get; set; }
        public string Part_Number { get; set; }
    }

    public class RefDocDate
    {
        public RefDocDate()
        {
        }

        private string _RefCode;
        private string _RefDate;

        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        public string RefDate
        {
            get { return _RefDate; }
            set { _RefDate = value; }
        }
    }
}
