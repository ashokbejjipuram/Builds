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
    public class STFormCollection
    {
        public STFormCollection()
        {
        }

        public List<ReferenceNumber> GetReferenceNumber(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ReferenceNumber> objDiversionNumbers = new List<ReferenceNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select Reference_Number from Form_Issue_Receipt order by reference_number desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ReferenceNumber objTrans = new ReferenceNumber();
                        objTrans.Reference_Number = reader["Reference_Number"].ToString();
                        objDiversionNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objDiversionNumbers;
        }

        public List<STFormName> GetSTFormName()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<STFormName> objSTFormName = new List<STFormName>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select ST_Form_Name, Receive_Issue_Indicator,Supplier_Dealer_Indicator from branch_ST_forms order by ST_Form_Name";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        STFormName objTrans = new STFormName();
                        objTrans.ST_Form_Name = reader["ST_Form_Name"].ToString();
                        objSTFormName.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objSTFormName;
        }

        public List<STCustomDDL> GetSupplierCustomer(string strSupplierOrDealer, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<STCustomDDL> objSTFormName = new List<STCustomDDL>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;

                if (strSupplierOrDealer == "D")
                {
                    sSQL = "select distinct customer_code as ST_Value,customer_name as ST_Text from customer_master WITH (NOLOCK) where Branch_Code = '" + strBranchCode + "'order by customer_Name";
                }
                else
                {
                    sSQL = "select distinct supplier_line_code as ST_Value,Short_Description as ST_Text from supplier_line_master WITH (NOLOCK) order by Short_Description";
                }
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        STCustomDDL objTrans = new STCustomDDL();
                        objTrans.STCustomDDL_Text = reader["ST_Text"].ToString();
                        objTrans.STCustomDDL_Value = reader["ST_Value"].ToString();
                        objSTFormName.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objSTFormName;
        }

        public List<STCustomDDL> GetReferenceDocument(string strSupplierOrDealer, string strCustomerSupplierCode, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<STCustomDDL> objSTFormName = new List<STCustomDDL>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;

                if (strSupplierOrDealer == "D")
                {
                    sSQL = "select document_number as ST_Value from sales_order_header where customer_code = '" + strCustomerSupplierCode + "' and substring(sales_order_header.document_number,16,3) = '" + strBranchCode + "' order by document_number desc ";
                }
                else
                {
                    sSQL = "select distinct Invoice_Number as ST_Value,Inward_Number from inward_header where supplier_code = substring('" + strCustomerSupplierCode + "',1,3) and substring(Inward_header.inward_number,12,3) = '" + strBranchCode + "' order by Inward_Number desc";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        STCustomDDL objTrans = new STCustomDDL();
                        objTrans.STCustomDDL_Text = reader["ST_Value"].ToString();
                        objTrans.STCustomDDL_Value = reader["ST_Value"].ToString();
                        objSTFormName.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objSTFormName;
        }

        public STFormCollectionItems GetReceiveSupplierIndicator(string strFromName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            STFormCollectionItems objTrans = new STFormCollectionItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select Receive_Issue_Indicator,Supplier_Dealer_Indicator from branch_ST_forms where ST_Form_Name ='" + strFromName + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Receive_Issue_Indicator = reader["Receive_Issue_Indicator"].ToString();
                        objTrans.Supplier_Dealer_Indicator = reader["Supplier_Dealer_Indicator"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objTrans;
        }

        public STFormCollectionItems GetSTFormCollection(string strReference_Number)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            STFormCollectionItems objTrans = new STFormCollectionItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSTcoll");
                ImpalDB.AddInParameter(cmd, "@Reference_Number", DbType.String, strReference_Number);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Reference_Number = strReference_Number;
                        objTrans.ST_Form_Name = reader["Form_Name"].ToString();
                        objTrans.Branch_Code = reader["Branch_code"].ToString();
                        objTrans.Receipt_Date = reader["Receipt_Date"].ToString();
                        objTrans.Receive_Issue_Indicator = reader["Issue_Receive_Indicator"].ToString();
                        objTrans.Supplier_Dealer_Indicator = reader["Supplier_Dealer_Indicator"].ToString();
                        objTrans.Party_Code = reader["Party_Code"].ToString();
                        objTrans.Ref_Document_Number = reader["Reference_Document"].ToString();
                        objTrans.Form_No = reader["Form_Number"].ToString();
                        objTrans.Form_Date = reader["Form_Date"].ToString();
                        objTrans.Party_Code = reader["Party_Code"].ToString();
                        objTrans.Sup_Del_desc = reader["Sup_Del_desc"].ToString();

                    }

                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objTrans;
        }

        public string InsSTFormCollection(STFormCollectionItems objValue)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSTReferenceNumber = "";
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_AddSTcoll");
                    ImpalDB.AddInParameter(cmd, "@Reference_Number", DbType.String, strSTReferenceNumber);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objValue.Branch_Code);
                    ImpalDB.AddInParameter(cmd, "@Form_Name", DbType.String, objValue.ST_Form_Name);
                    ImpalDB.AddInParameter(cmd, "@Party_Code", DbType.String, objValue.Party_Code);
                    ImpalDB.AddInParameter(cmd, "@Reference_Document", DbType.String, objValue.Ref_Document_Number);
                    ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, objValue.Form_No);
                    ImpalDB.AddInParameter(cmd, "@Form_Date", DbType.String, objValue.Form_Date);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    strSTReferenceNumber = ImpalDB.ExecuteScalar(cmd).ToString();
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return strSTReferenceNumber;
        }

        public int UpdSTFormCollection(STFormCollectionItems objValue)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_updtSTcoll");
                    ImpalDB.AddInParameter(cmd, "@Reference_Number", DbType.String, objValue.Reference_Number);
                    ImpalDB.AddInParameter(cmd, "@Form_Number", DbType.String, objValue.Form_No);
                    ImpalDB.AddInParameter(cmd, "@Form_Date", DbType.String, objValue.Form_Date);
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
    



    public class ReferenceNumber
    {
        public ReferenceNumber()
        { }

        public string Reference_Number { set; get; }
    }

     public class STFormName
    {
         public STFormName()
        { }

        public string ST_Form_Name { set; get; }
    }

     public class STCustomDDL
     {
         public STCustomDDL()
         { }

         public string STCustomDDL_Value { set; get; }
         public string STCustomDDL_Text { set; get; }
     }
    
    public class STFormCollectionItems
    {
        public STFormCollectionItems() { }

        public string Reference_Number { set; get; }
        public string Branch_Code { set; get; }

        public string Receipt_Date { set; get; }

        public string ST_Form_Name { set; get; }
        public string Receive_Issue_Indicator { set; get; }
        public string Supplier_Dealer_Indicator { set; get; }
        public string Party_Code { set; get; }
        public string Customer_code { set; get; }
        public string Supplier_Line_Code { set; get; }
        public string Ref_Document_Number { set; get; }
        public string Invoice_Number { set; get; }
        public string Form_No { set; get; }
        public string Form_Date { set; get; }
        public string Sup_Del_desc { set; get; }

    }
}
