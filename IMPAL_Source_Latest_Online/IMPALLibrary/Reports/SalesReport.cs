using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary
{
    public class SalesReport
    {
        public List<string> GetInvoiceSTDN(string strInvoiceType, string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            if (!string.IsNullOrEmpty(strInvoiceType))
            {
                if (strInvoiceType.Equals("D"))
                    strQuery = "SELECT DOCUMENT_NUMBER FROM SALES_ORDER_HEADER WHERE CONVERT(NVARCHAR,DOCUMENT_DATE,103)= '" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "' and isnull(Status,'A') = 'A' and Branch_Code = '" + strBranchCode + "' order by Document_Number desc";
                else
                    strQuery = "SELECT STDN_NUMBER FROM STDN_HEADER WHERE STDN_NUMBER LIKE '%S' AND BRANCH_CODE = '" + strBranchCode + "' and isnull(Status,'A') = 'A' and Approval_Status='A' order by STDN_Number desc";
                
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        lstInvoiceSTDN.Add(reader[0].ToString());
                    }
                }

                if (lstInvoiceSTDN.Count <= 0)
                    lstInvoiceSTDN.Add("");
            }

            return lstInvoiceSTDN;
        }

        public List<string> GetSTDNQRdocs(string strInvoiceType, string strBranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            if (!string.IsNullOrEmpty(strInvoiceType))
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");                
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, strInvoiceType);
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
            }

            return lstInvoiceSTDN;
        }

        public List<Item> GetSalesQRdocs(string strInvoiceType, string strBranchCode)
        {
            List<Item> lstInvoice = new List<Item>();
            Item objItem = new Item();
            Database ImpalDB = DataAccess.GetDatabase();
            if (!string.IsNullOrEmpty(strInvoiceType))
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getVinvoice_EinvDocs_QR");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Invoice_Type", DbType.String, strInvoiceType);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objItem = new Item();
                        objItem.ItemDesc = reader[0].ToString();
                        objItem.ItemCode = reader[1].ToString();
                        lstInvoice.Add(objItem);                        
                    }
                }

                if (lstInvoice.Count <= 0)
                {
                    objItem = new Item();
                    objItem.ItemDesc = "";
                    objItem.ItemCode = "";
                    lstInvoice.Add(objItem);
                }
            }

            return lstInvoice;
        }

        public DataSet GetEinvoicingDetails(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_Data");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsQR(string BranchCode, string DocumentNumber, string InvoiceType)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = null;

            if (InvoiceType == "D")
                cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_Data_QR");
            else
                cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_STDN_Data_QR");

            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public DataSet GetEinvoicingDetailsOldQR(string BranchCode, string DocumentNumber, string InvoiceType)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DataSet ds = new DataSet();
            DbCommand cmd = null;

            if (InvoiceType == "D")
                cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_Data_QRDuplicate");
            else
                cmd = ImpalDB.GetStoredProcCommand("usp_GetVinvoice_STDN_Data_QRDuplicate");

            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@doc_no", DbType.String, DocumentNumber);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);
            cmd = null;

            return ds;
        }

        public List<string> GetInvoiceProforma(string strInvoiceType, string BranchCode)
        {
            List<string> lstInvoiceSTDN = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            if (!string.IsNullOrEmpty(strInvoiceType))
            {
                strQuery = "Select Document_Number From Proforma_Invoice_Header Where Branch_Code='" + BranchCode + "' and isnull(Status,'A') = 'A' order by Document_Number desc";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        lstInvoiceSTDN.Add(reader[0].ToString());
                    }
                }

                if (lstInvoiceSTDN.Count <= 0)
                    lstInvoiceSTDN.Add("");
            }

            return lstInvoiceSTDN;
        }

        public string GetIndicatorFromSerialNumber(string strInvoiceSTDNNumber, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            if (!string.IsNullOrEmpty(strInvoiceSTDNNumber)) 
            {
                string strQuery = "Select Indicator from serial_number where document_number = '" + strInvoiceSTDNNumber + "' and Branch_Code = '" + strBranchCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objValue = ImpalDB.ExecuteScalar(cmd1);
                if (objValue != null)
                    return Convert.ToString(objValue);
            }

            return default(string);
        }

        public string GetEWayBillInd(string BranchCode, string DocumentNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string result = "";
            string sSQL = "";
            string CustCode = "";
            string DocDate = "";

            //string strQuery = "Select Customer_Code, Convert(varchar(10),Document_Date,103) Document_Date from Sales_Order_Header Where Branch_Code='" + BranchCode + "' and Document_Number = '" + DocumentNumber + "'";
            //DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            //cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;

            //using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            //{
            //    while (reader.Read())
            //    {
            //        CustCode = reader[0].ToString();
            //        DocDate = reader[1].ToString();
            //    }
            //}

            //sSQL = "Select Ind from (Select case when Doc_Sale_Value>=50000 or Day_Sale_Value>=50000 then 'Customer' else 'N' end Ind from ";
            //sSQL = sSQL + "(Select distinct a.Customer_Code, a.Document_Date, a.Doc_Sale_Value, case when a.Day_Sale_Value<50000 then 0 ";
            //sSQL = sSQL + "when a.Day_Sale_Value>=50000 and (LR_Number='' or LR_Number='0' or LR_Number IS NULL) then 50000 ";
            //sSQL = sSQL + "when a.Day_Sale_Value>=50000 and LR_Number<>'' and LR_Number<>'0' and LR_Number IS NOT NULL then 0 else 0 end Day_Sale_Value from ";
            //sSQL = sSQL + "(Select Customer_Code,Convert(Date,Document_Date,103) Document_Date,ISNULL(SUM(case when Document_Number='" + DocumentNumber + "' and Order_Value<50000 then 0 ";
            //sSQL = sSQL + "when Document_Number='" + DocumentNumber + "' and Order_Value>=50000 and (LR_Number='' or LR_Number='0' or LR_Number IS NULL) then 50000 ";
            //sSQL = sSQL + "when Document_Number='" + DocumentNumber + "' and Order_Value>=50000 and LR_Number<>'' and LR_Number<>'0' and LR_Number IS NOT NULL then 0 else 0 end),0) Doc_Sale_Value, ";
            //sSQL = sSQL + "ISNULL(SUM(Order_Value),0) Day_Sale_Value from Sales_Order_Header WITH (NOLOCK) where Branch_Code='" + BranchCode + "' and Convert(Date,Document_Date,103)=Convert(Date,'" + DocDate + "',103) and ";
            //sSQL = sSQL + "Status='A' and Customer_Code='" + CustCode + "' Group By Customer_Code,Convert(Date,Document_Date,103)) a ";
            //sSQL = sSQL + "inner join Sales_Order_Header s WITH (NOLOCK) on s.Branch_Code='" + BranchCode + "' and s.Status='A' and s.Customer_Code=a.Customer_Code and Convert(Date,s.Document_Date,103)=a.Document_Date) b) c Where Ind='Customer'";
            //DbCommand cmdOrdVal = ImpalDB.GetSqlStringCommand(sSQL);
            //cmdOrdVal.CommandTimeout = ConnectionTimeOut.TimeOut;

            //using (IDataReader reader = ImpalDB.ExecuteReader(cmdOrdVal))
            //{
            //    while (reader.Read())
            //    {
            //        result = reader[0].ToString();
            //    }
            //}

            return result;
        }

        public string GetSTDNeWayBillInd(string BranchCode, string STDNNumber)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string result = "";
            string sSQL = "";
            string ToBranch = "";
            string STDNDate = "";

            //string strQuery = "Select To_Branch_Code, Convert(varchar(10),STDN_Date,103) STDN_Date from STDN_Header Where From_Branch_Code='" + BranchCode + "' and STDN_Number = '" + STDNNumber + "'";
            //DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            //cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;

            //using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            //{
            //    while (reader.Read())
            //    {
            //        ToBranch = reader[0].ToString();
            //        STDNDate = reader[1].ToString();
            //    }
            //}

            //sSQL = "Select Ind from (Select case when Doc_Sale_Value>=50000 or Day_Sale_Value>=50000 then 'Branch' else 'N' end Ind from ";
            //sSQL = sSQL + "(Select distinct a.To_Branch_Code, a.STDN_Date, a.Doc_Sale_Value, case when a.Day_Sale_Value<50000 then 0 ";
            //sSQL = sSQL + "when a.Day_Sale_Value>=50000 and (LR_Number='' or LR_Number='0' or LR_Number IS NULL) then 50000 ";
            //sSQL = sSQL + "when a.Day_Sale_Value>=50000 and LR_Number<>'' and LR_Number<>'0' and LR_Number IS NOT NULL then 0 else 0 end Day_Sale_Value from ";
            //sSQL = sSQL + "(Select To_Branch_Code,Convert(Date,STDN_Date,103) STDN_Date,ISNULL(SUM(case when STDN_Number='" + STDNNumber + "' and STDN_Value<50000 then 0 ";
            //sSQL = sSQL + "when STDN_Number='" + STDNNumber + "' and STDN_Value>=50000 and (LR_Number='' or LR_Number='0' or LR_Number IS NULL) then 50000 ";
            //sSQL = sSQL + "when STDN_Number='" + STDNNumber + "' and STDN_Value>=50000 and LR_Number<>'' and LR_Number<>'0' and LR_Number IS NOT NULL then 0 else 0 end),0) Doc_Sale_Value, ";
            //sSQL = sSQL + "ISNULL(SUM(STDN_Value),0) Day_Sale_Value from STDN_Header WITH (NOLOCK) where From_Branch_Code='" + BranchCode + "' and Convert(Date,STDN_Date,103)=Convert(Date,'" + STDNDate + "',103) and ";
            //sSQL = sSQL + "ISNULL(Status,'A')='A' and To_Branch_Code='" + ToBranch + "' Group By To_Branch_Code,Convert(Date,STDN_Date,103)) a ";
            //sSQL = sSQL + "inner join STDN_Header s WITH (NOLOCK) on s.From_Branch_Code='" + BranchCode + "' and ISNULL(s.Status,'A')='A' and s.To_Branch_Code=a.To_Branch_Code and Convert(Date,s.STDN_Date,103)=a.STDN_Date) b) c Where Ind='Branch'";
            //DbCommand cmdOrdVal = ImpalDB.GetSqlStringCommand(sSQL);
            //cmdOrdVal.CommandTimeout = ConnectionTimeOut.TimeOut;

            //using (IDataReader reader = ImpalDB.ExecuteReader(cmdOrdVal))
            //{
            //    while (reader.Read())
            //    {
            //        result = reader[0].ToString();
            //    }
            //}

            return result;
        }
    }
}