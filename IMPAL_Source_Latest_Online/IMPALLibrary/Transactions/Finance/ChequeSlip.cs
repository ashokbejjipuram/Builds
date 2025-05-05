using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace IMPALLibrary.Transactions
{
    public class ChequeSlip
    {       
        public List<ChequeSlipNumber> LoadChequeSlip(string BranchCode)
        {
            List<ChequeSlipNumber> objChequeSlipDetail = new List<ChequeSlipNumber>();

            Database ImpalDB = DataAccess.GetDatabase();
            string lstrSql = "select Cheque_Slip_Number from Cheque_Slip_Header where Branch_Code = '" + BranchCode + "' and year(Cheque_Slip_Date)>year(getdate())-3 order by Cheque_Slip_Number desc, Cheque_Slip_Date desc";
            ChequeSlipNumber ChequeSlipList = new ChequeSlipNumber();
            ChequeSlipList.ChequeSlipCode = "0";
            ChequeSlipList.ChequeSlipNo = "";
            objChequeSlipDetail.Add(ChequeSlipList);
            DbCommand cmd = ImpalDB.GetSqlStringCommand(lstrSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ChequeSlipList = new ChequeSlipNumber();
                    ChequeSlipList.ChequeSlipCode = reader[0].ToString();
                    ChequeSlipList.ChequeSlipNo = reader[0].ToString();
                    objChequeSlipDetail.Add(ChequeSlipList);
                }
            }

            return objChequeSlipDetail;
        }

        public List<ChequeSlipDetail> LoadChequeSlipPO(string strBranch, string strSupLineNo, string dtInvFromDate, string dtInvToDate)
        {
            List<ChequeSlipDetail> objChequeSlipPOItems = new List<ChequeSlipDetail>();
            Database ImpalDB = DataAccess.GetDatabase();

            ChequeSlipDetail ChequeSlipPOItem = new ChequeSlipDetail();

            string lstrSql = null;

            lstrSql = "SELECT INVOICE_NUMBER,INWARD_VALUE, CONVERT(VARCHAR(10),INVOICE_DATE,103), INDICATOR"
                       + " FROM V_LIABILITYLIST WHERE CONVERT(DATETIME,INVOICE_DATE,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                       + " AND CONVERT(DATETIME,INVOICE_DATE,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                       + " AND ISNULL(STATUS,'A') = 'A' AND INVOICE_NUMBER NOT IN (SELECT REFERENCE_DOCUMENT_NUMBER FROM CHEQUE_SLIP_DETAIL "
                       + " WHERE SUBSTRING(CHART_OF_ACCOUNT_CODE,15,3) = '" + strSupLineNo + " ') AND SUPPLIER_CODE = '" + strSupLineNo + "' ORDER BY INVOICE_DATE ASC ";

            DbCommand cmd = ImpalDB.GetSqlStringCommand(lstrSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ChequeSlipPOItem = new ChequeSlipDetail();
                    ChequeSlipPOItem.IsSelected = "F";                    
                    ChequeSlipPOItem.Indicator = reader["INDICATOR"].ToString();
                    ChequeSlipPOItem.BranchCode = strBranch;
                    ChequeSlipPOItem.InvoiceNo = reader["INVOICE_NUMBER"].ToString();
                    if (reader["INDICATOR"].ToString() == "DA")
                    {
                        ChequeSlipPOItem.InvoiceValue = (Convert.ToDecimal(reader["INWARD_VALUE"]) * -1).ToString();
                    }
                    else
                    {
                        ChequeSlipPOItem.InvoiceValue = reader["INWARD_VALUE"].ToString();
                    }
                    ChequeSlipPOItem.InvoiceDate = reader[2].ToString();
                    ChequeSlipPOItem.CDValue = "0.00";
                    ChequeSlipPOItem.TotalValue = "0.00";
                    objChequeSlipPOItems.Add(ChequeSlipPOItem);
                }
            }

            return objChequeSlipPOItems;
        }

        public List<ChequeSlipDetailCOR> LoadChequeSlipPOCOR(string strBranch, string strSupLineNo, string dtInvFromDate, string dtInvToDate, string RefType)
        {
            List<ChequeSlipDetailCOR> objChequeSlipPOItems = new List<ChequeSlipDetailCOR>();
            Database ImpalDB = DataAccess.GetDatabase();

            ChequeSlipDetailCOR ChequeSlipPOItem = new ChequeSlipDetailCOR();

            string lstrSql = null;

            if (RefType == "BM")
            {
                lstrSql = "SELECT HO_BMS_NUMBER, BMS_AMOUNT, CONVERT(VARCHAR(10),HO_BMS_DATE,103), '' INDICATOR"
                         + " FROM BMS_HEADER WHERE CONVERT(DATETIME,BMS_date,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                         + " AND CONVERT(DATETIME,BMS_date,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                         + " AND ISNULL(BMS_STATUS,'A') <> 'P' AND SUPPLIER_CODE = '" + strSupLineNo + "' ORDER BY HO_BMS_DATE ASC ";
            }
            else if (RefType == "SV")
            {
                lstrSql = "SELECT INVOICE_NUMBER,INWARD_VALUE, CONVERT(VARCHAR(10),INVOICE_DATE,103), '' INDICATOR"
                         + " FROM INWARD_HEADER WHERE CONVERT(DATETIME,INVOICE_DATE,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                         + " AND CONVERT(DATETIME,INVOICE_DATE,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                         + " AND ISNULL(STATUS,'A') = 'A' AND SUPPLIER_CODE = '" + strSupLineNo + "' AND BRANCH_CODE = '" + strBranch + "' ORDER BY INVOICE_DATE ASC ";
            }
            else if (RefType == "IV")
            {
                lstrSql = "SELECT DOCUMENT_NUMBER,NET_AMOUNT, CONVERT(VARCHAR(10),DOCUMENT_DATE,103), '' INDICATOR"
                         + " FROM SUPPLIER_INVOICE_HEADER WHERE CONVERT(DATETIME,DOCUMENT_DATE,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                         + " AND CONVERT(DATETIME,DOCUMENT_DATE,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                         + " AND ISNULL(STATUS,'A') <> 'P' AND SUPPLIER_CODE = '" + strSupLineNo + "' AND BRANCH_CODE = '" + strBranch + "' ORDER BY DOCUMENT_DATE ASC ";
            }
            else if (RefType == "DN")
            {
                lstrSql = "SELECT DOCUMENT_NUMBER,ADJUSTMENT_VALUE, CONVERT(VARCHAR(10),DOCUMENT_DATE,103), '' INDICATOR"
                         + " FROM DEBIT_CREDIT_NOTE_HEADER WHERE CONVERT(DATETIME,DOCUMENT_DATE,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                         + " AND CONVERT(DATETIME,DOCUMENT_DATE,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                         + " AND DR_CR_INDICATOR IN ('DN','DR') AND ISNULL(STATUS,'A') <> 'P' AND SUPPLIER_CODE = '" + strSupLineNo + "' AND BRANCH_CODE = '" + strBranch + "' ORDER BY DOCUMENT_DATE ASC ";
            }
            else if (RefType == "CN")
            {
                lstrSql = "SELECT DOCUMENT_NUMBER,ADJUSTMENT_VALUE, CONVERT(VARCHAR(10),DOCUMENT_DATE,103), '' INDICATOR"
                         + " FROM DEBIT_CREDIT_NOTE_HEADER WHERE CONVERT(DATETIME,DOCUMENT_DATE,103) >= CONVERT(DATETIME,'" + dtInvFromDate + " ',103)"
                         + " AND CONVERT(DATETIME,DOCUMENT_DATE,103) <= CONVERT(DATETIME,'" + dtInvToDate + "',103) "
                         + " AND DR_CR_INDICATOR IN ('CN','CR') AND ISNULL(STATUS,'A') <> 'P' AND SUPPLIER_CODE = '" + strSupLineNo + "' AND BRANCH_CODE = '" + strBranch + "' ORDER BY DOCUMENT_DATE ASC ";
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(lstrSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ChequeSlipPOItem = new ChequeSlipDetailCOR();
                    ChequeSlipPOItem.Sel = "F";
                    ChequeSlipPOItem.Ind = reader[3].ToString();
                    ChequeSlipPOItem.BrchCode = strBranch;
                    ChequeSlipPOItem.InvNo = reader[0].ToString();
                    if (reader[3].ToString() == "DA")
                    {
                        ChequeSlipPOItem.InvVal = (Convert.ToDecimal(reader[1]) * -1).ToString();
                    }
                    else
                    {
                        ChequeSlipPOItem.InvVal = reader[1].ToString();
                    }
                    ChequeSlipPOItem.InvDt = reader[2].ToString();
                    ChequeSlipPOItem.CDVal = "0.00";
                    ChequeSlipPOItem.TotVal = "0.00";
                    objChequeSlipPOItems.Add(ChequeSlipPOItem);
                }
            }

            return objChequeSlipPOItems;
        }

        public string SubmitChequeSlip(ChequeSlipHeader objChequeSlipHdr, DataTable objPOTbl)
        {
            string lstrSql = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string strChequeSlipNumber = string.Empty;
            decimal dInvValue = 0;
            decimal dCdValue = 0;
            int Sno = 1;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addchequeslip1");
                    ImpalDB.AddInParameter(cmd, "@Cheque_Slip_Date", DbType.String, objChequeSlipHdr.ChequeSlipDate);
                    ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, objChequeSlipHdr.SupCode);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objChequeSlipHdr.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@cacode", DbType.String, objChequeSlipHdr.ChartofAccount);
                    ImpalDB.AddInParameter(cmd, "@chequeno", DbType.String, objChequeSlipHdr.ChequeNo);
                    ImpalDB.AddInParameter(cmd, "@chequedate", DbType.String, objChequeSlipHdr.ChequeDate);
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, objChequeSlipHdr.Remarks);
                    ImpalDB.AddInParameter(cmd, "@chequebank", DbType.String, objChequeSlipHdr.Bank);
                    ImpalDB.AddInParameter(cmd, "@chequebranch", DbType.String, objChequeSlipHdr.Branch);
                    ImpalDB.AddInParameter(cmd, "@chequeamt", DbType.Decimal, objChequeSlipHdr.CalcAmount);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    strChequeSlipNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    for (int linti = 0; linti <= objPOTbl.Rows.Count - 1; linti++)
                    {
                        if (objPOTbl.Rows[linti]["IsSelected"].ToString() == "1")
                        {
                            DbCommand cmddtl = ImpalDB.GetStoredProcCommand("usp_addchequeslip2");
                            ImpalDB.AddInParameter(cmddtl, "@Cheque_Slip_Number", DbType.String, strChequeSlipNumber);
                            ImpalDB.AddInParameter(cmddtl, "@Serial_Number", DbType.Int32, Sno++);
                            ImpalDB.AddInParameter(cmddtl, "@Reference_Document_Number", DbType.String, objPOTbl.Rows[linti]["InvoiceNo"].ToString());
                            ImpalDB.AddInParameter(cmddtl, "@Reference_Type", DbType.String, objChequeSlipHdr.RefType);
                            ImpalDB.AddInParameter(cmddtl, "@Amount", DbType.Decimal, objPOTbl.Rows[linti]["TotalValue"].ToString());
                            ImpalDB.AddInParameter(cmddtl, "@Reference_Document_Date", DbType.String, objPOTbl.Rows[linti]["InvoiceDate"].ToString());

                            dInvValue = Convert.ToDecimal(objPOTbl.Rows[linti]["InvoiceValue"].ToString());
                            ImpalDB.AddInParameter(cmddtl, "@Document_Value", DbType.Decimal, dInvValue);

                            dCdValue = Convert.ToDecimal(objPOTbl.Rows[linti]["CDValue"].ToString());
                            ImpalDB.AddInParameter(cmddtl, "@cd_value", DbType.Decimal, dCdValue);
                            ImpalDB.AddInParameter(cmddtl, "@branch_code", DbType.String, objChequeSlipHdr.BranchCode);
                            ImpalDB.AddInParameter(cmddtl, "@cd_percentage", DbType.Int32, objChequeSlipHdr.CashDiscCode);
                            cmddtl.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmddtl);

                            DbCommand cmdlib = ImpalDB.GetStoredProcCommand("usp_Update_LiabilityList");
                            ImpalDB.AddInParameter(cmdlib, "@branch_code", DbType.String, objChequeSlipHdr.BranchCode);
                            ImpalDB.AddInParameter(cmdlib, "@Document_Number", DbType.String, objPOTbl.Rows[linti]["InvoiceNo"].ToString());
                            ImpalDB.AddInParameter(cmdlib, "@Reference_Type", DbType.String, objChequeSlipHdr.RefType);
                            cmdlib.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmdlib);
                        }
                    }

                    DbCommand cmdcql = ImpalDB.GetStoredProcCommand("usp_addglcq1");
                    ImpalDB.AddInParameter(cmdcql, "@doc_no", DbType.String, strChequeSlipNumber);
                    ImpalDB.AddInParameter(cmdcql, "@Branch_Code", DbType.String, objChequeSlipHdr.BranchCode);
                    cmdcql.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdcql);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                strChequeSlipNumber = "";
                throw exp;
            }

            return strChequeSlipNumber;
        }


        public ChequeSlipBankDetails GetChequeSlipBankDetails(string strChartofAccount)
        {
            string lstrSql = "SELECT A.BANK_NAME,B.ADDRESS FROM BANK_MASTER A, BANK_BRANCH_MASTER B "
                            + " WHERE A.BANK_CODE = B.BANK_CODE AND B.CHART_OF_ACCOUNT_CODE = '" +  strChartofAccount + "'";

            Database ImpalDB = DataAccess.GetDatabase();

            ChequeSlipBankDetails ChequeSlipBankDetail = new ChequeSlipBankDetails();
            DbCommand cmd = ImpalDB.GetSqlStringCommand(lstrSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ChequeSlipBankDetail= new ChequeSlipBankDetails();
                    ChequeSlipBankDetail.BankName = reader[0].ToString();
                    ChequeSlipBankDetail.Address = reader[1].ToString();
                }
            }

            return ChequeSlipBankDetail;
        }

        public ChequeSlipHeader LoadCheckSlipViewRec(string strChequeSlipNo, string strBranchCode)
        {
            string lstrSql = string.Empty;
            
            ChequeSlipHeader objChequeSlipHdr = new ChequeSlipHeader();
            objChequeSlipHdr.ChequeSlipItems = new List<ChequeSlipDetail>();
            ChequeSlipDetail ChequeSlipPOItem = new ChequeSlipDetail();

            Database ImpalDB = DataAccess.GetDatabase();

            lstrSql = "SELECT * FROM CHEQUE_SLIP_HEADER WHERE CHEQUE_SLIP_NUMBER = '" + strChequeSlipNo + "' and BRANCH_CODE = '" + strBranchCode + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(lstrSql);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objChequeSlipHdr.ChequeSlipNo = reader["Cheque_Slip_Number"].ToString();
                    objChequeSlipHdr.ChequeSlipDate = reader["Cheque_Slip_Date"].ToString();
                    objChequeSlipHdr.SupCode = reader["Supplier_Line_Code"].ToString();
                    objChequeSlipHdr.BranchCode = reader["Branch_Code"].ToString();

                    ChequeSlipBankDetails objBankDtl = new ChequeSlipBankDetails();
                    objBankDtl = GetChequeSlipBankDetails(reader["Chart_of_Account_Code"].ToString());

                    objChequeSlipHdr.Bank = objBankDtl.BankName;
                    objChequeSlipHdr.Branch = objBankDtl.Address;
                    objChequeSlipHdr.ChartofAccount = reader["Chart_of_Account_Code"].ToString();
                    objChequeSlipHdr.ChequeNo = reader["Cheque_Number"].ToString();
                    objChequeSlipHdr.ChequeDate = reader["Cheque_Date"].ToString();
                    objChequeSlipHdr.CalcAmount = Convert.ToDecimal(reader["Cheque_Amount"].ToString());
                    objChequeSlipHdr.Remarks = reader["Remarks"].ToString();
                }
            }

            lstrSql = "select v.indicator,c.Reference_Document_Number,c.Reference_Document_Date,c.Document_Value,c.CD_Value,c.Amount,c.CD_Percentage from Cheque_Slip_Detail c inner join v_liabilitylist v";
            lstrSql = lstrSql +  " on c.Reference_Document_Number = v.invoice_number and c.Cheque_Slip_Number= '" + strChequeSlipNo + "'";
            DbCommand cmdr = ImpalDB.GetSqlStringCommand(lstrSql);
            cmdr.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmdr))
            {
                while (reader.Read())
                {
                    ChequeSlipPOItem = new ChequeSlipDetail();
                    ChequeSlipPOItem.IsSelected = "F";
                    ChequeSlipPOItem.Indicator = reader["indicator"].ToString();;
                    ChequeSlipPOItem.BranchCode = objChequeSlipHdr.BranchCode;
                    ChequeSlipPOItem.InvoiceNo = reader["Reference_Document_Number"].ToString();
                    ChequeSlipPOItem.InvoiceValue = reader["Document_Value"].ToString();
                    ChequeSlipPOItem.InvoiceDate = reader["Reference_Document_Date"].ToString();
                    ChequeSlipPOItem.CDValue = reader["CD_Value"].ToString();
                    ChequeSlipPOItem.TotalValue = reader["Amount"].ToString();
                    objChequeSlipHdr.ChequeSlipItems.Add(ChequeSlipPOItem);
                    objChequeSlipHdr.CashDiscCode = Convert.ToInt32(reader["CD_Percentage"]);
                }
            }

            return objChequeSlipHdr;
        }
    }

    public class ChequeSlipNumber
    {
        public string ChequeSlipCode { get; set; }
        public string ChequeSlipNo { get; set; }
    }

    public class ChequeSlipBankDetails
    {
        public string BankName { get; set; }
        public string Address { get; set; }
    }

    public class ChequeSlipHeader
    {
        public string ChequeSlipNo { get; set; }
        public string ChequeSlipDate { get; set; }
        public string BranchCode { get; set; }
        public string Remarks { get; set; }
        public string TotalAmount { get; set; }
        public string ChartofAccount { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string SupCode { get; set; }
        public decimal CalcAmount { get; set; }
        public string RefDocFromDate { get; set; }
        public string RefDocToDate { get; set; }
        public string RefType { get; set; }
        public int CashDiscCode { get; set; }
        public int CashDiscPer { get; set; }
        public List<ChequeSlipDetail> ChequeSlipItems { get; set; }
    }

    public class ChequeSlipDetail
    {
        public string IsSelected { get; set; }
        public string Indicator { get; set; }
        public string BranchCode { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceValue { get; set; }
        public string CDValue { get; set; }
        public string TotalValue { get; set; }
    }

    public class ChequeSlipDetailCOR
    {
        public string _IsSelected;
        public string _Indicator;
        public string _BranchCode;
        public string _InvoiceNo;
        public string _InvoiceDate;
        public string _InvoiceValue;
        public string _CDValue;
        public string _TotalValue;

        public ChequeSlipDetailCOR(string Sel, string Ind, string BrchCode, string InvNo, string InvDt, string InvVal, string CDVal, string TotVal)
        {
            _IsSelected = Sel;
            _Indicator = Ind;
            _BranchCode = BrchCode;
            _InvoiceNo = InvNo;
            _InvoiceDate = InvDt;
            _InvoiceValue = InvVal;
            _CDValue = CDVal;
            _TotalValue = TotVal;
        }
        public ChequeSlipDetailCOR()
        {

        }

        public string Sel
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }
        public string Ind
        {
            get { return _Indicator; }
            set { _Indicator = value; }
        }
        public string BrchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public string InvNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public string InvDt
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        public string InvVal
        {
            get { return _InvoiceValue; }
            set { _InvoiceValue = value; }
        }
        public string CDVal
        {
            get { return _CDValue; }
            set { _CDValue = value; }
        }
        public string TotVal
        {
            get { return _TotalValue; }
            set { _TotalValue = value; }
        }
    }
}
