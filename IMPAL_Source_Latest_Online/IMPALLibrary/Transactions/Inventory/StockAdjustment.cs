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
    public class StockAdjustment
    {
        public StockAdjustment()
        { }

        public List<TagNumber> GetTagNumber(string strBranchCode, string strTag_number)
        {
            List<TagNumber> objTagNumbers = new List<TagNumber>();
            TagNumber objTrans = new TagNumber();
            objTrans.Tag_Number = "";
            objTagNumbers.Add(objTrans);

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetInventoryTagNumber");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Tag_Number", DbType.String, strTag_number);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans = new TagNumber();
                        objTrans.Tag_Number = reader["Tag_Number"].ToString();
                        objTagNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockAdjustment), exp);
            }

            return objTagNumbers;
        }

        public StockAdjustmentItems GetStockAdjustmentDetails(string strBranchCode, string strTag_number)
        {
            //List<StockAdjustmentItems> objTagNumbers = new List<StockAdjustmentItems>();
            StockAdjustmentItems objTrans = new StockAdjustmentItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetInventoryTagDetails");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Tag_Number", DbType.String, strTag_number);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Tag_Number = strTag_number;
                        objTrans.Item_Code = reader["Item_Code"].ToString();
                        objTrans.Tag_Date = reader["tag_date"].ToString();
                        objTrans.Computer_Balance = reader["Computer_Balance"].ToString();
                        //objTrans.Entry_Date = reader["Entry_Date"].ToString();
                        //objTrans.Phy_verify = reader["Taken_By"].ToString();
                        //objTrans.Phy_Balance_Date = reader["Physical_Balance_Date"].ToString();
                        //objTrans.Approvedby = reader["Approved_By"].ToString();
                        //objTrans.Physical_Balance = reader["Physical_Balance"].ToString();
                        //objTrans.Remarks = reader["Remarks"].ToString();
                        objTrans.OS_LS_Indicator = reader["OS_LS_Indicator"].ToString();
                        objTrans.Supplier_Part_Number = reader["Supplier_Part_Number"].ToString();
                        objTrans.Location = reader["Location_Code"].ToString();
                        objTrans.Accounting_Period_Code = reader["Accounting_Period_Code"].ToString();
                        //objTrans.WH_Ref_No_Reason = reader["WH_Ref_no_Reason"].ToString(); 
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockAdjustment), exp); 
            }

            return objTrans;
        }

        public DataSet AddinventoryTag(string strBranchCode, string strLineCode, string strItemCode, string strTagtype, string Indicator, string strType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_addinventorytag");
                    ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@Line_Code", DbType.String, strLineCode.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@item_Code1", DbType.String, strItemCode.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@Tag_type", DbType.String, strTagtype.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@Indicator", DbType.String, Indicator.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@Type", DbType.String, strType.Trim());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(dbcmd);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockAdjustment), exp);
            }

            return ds;
        }

        public int UptStockAdjustNumber(StockAdjustmentItems objValue, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int iRowCount = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updstockadjustment");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
					ImpalDB.AddInParameter(cmd, "@Tag_Number", DbType.String, objValue.Tag_Number);
                    ImpalDB.AddInParameter(cmd, "@Tag_Date", DbType.String, objValue.Tag_Date);
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, objValue.Item_Code);
                    ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, objValue.Supplier_Part_Number);
                    ImpalDB.AddInParameter(cmd, "@taken_by", DbType.String, objValue.Phy_verify);
                    ImpalDB.AddInParameter(cmd, "@physical_balance_date", DbType.String, objValue.Phy_Balance_Date);
                    ImpalDB.AddInParameter(cmd, "@approved_by", DbType.String, objValue.Approvedby);
                    ImpalDB.AddInParameter(cmd, "@Computer_Balance", DbType.Decimal, Convert.ToDecimal(objValue.Computer_Balance));
                    ImpalDB.AddInParameter(cmd, "@physical_balance", DbType.Decimal, Convert.ToDecimal(objValue.Physical_Balance));
                    ImpalDB.AddInParameter(cmd, "@remarks", DbType.String, objValue.Remarks);
                    ImpalDB.AddInParameter(cmd, "@entry_date", DbType.String, objValue.Entry_Date);
                    ImpalDB.AddInParameter(cmd, "@Location", DbType.String, objValue.Location);
                    ImpalDB.AddInParameter(cmd, "@osls", DbType.String, objValue.OS_LS_Indicator);
                    ImpalDB.AddInParameter(cmd, "@whreason", DbType.String, objValue.WH_Ref_No_Reason);
                    ImpalDB.AddInParameter(cmd, "@InvoiceNumber", DbType.String, objValue.Invoice_Number);
                    ImpalDB.AddInParameter(cmd, "@InvoiceDate", DbType.String, objValue.Invoice_Date);
                    ImpalDB.AddInParameter(cmd, "@ap_code", DbType.Int16, objValue.Accounting_Period_Code);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockAdjustment), exp);
            }

            return iRowCount;
        }
    }

    public class TagNumber
    {
        public TagNumber()
        { }

        public string Tag_Number { set; get; }
    }

    public class StockAdjustmentItems
    {
        public StockAdjustmentItems()
        { }

        public string Tag_Number { set; get; }
        public string Item_Code { set; get; }
        public string Tag_Date { set; get; }
        public string Computer_Balance { set; get; }
        public string Entry_Date { set; get; }
        public string Phy_verify { set; get; }
        public string Phy_Balance_Date { set; get; }
        public string Approvedby { set; get; }
        public string Physical_Balance { set; get; }
        public string Remarks { set; get; }
        public string OS_LS_Indicator { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string Location { set; get; }
        public string WH_Ref_No_Reason { set; get; }
        public string Invoice_Number { set; get; }
        public string Invoice_Date { set; get; }
        public string Accounting_Period_Code { set; get; }
    }    
}
