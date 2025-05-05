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
    #region This class is used Consignment details info in View mode functionalities
    public class ConsignmentDetails
    {
        public ConsignmentDetails()
        { }


        public List<ConsignmentBalanceDetails> GetConsignmentBalance(string strPart_Number, string strBranch_Code)
        {
            List<ConsignmentBalanceDetails> objGetBalanceDetails = new List<ConsignmentBalanceDetails>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string strOSLSValue = string.Empty;

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetConsignmentBalance");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(dbcmd, "@strPart_Number", DbType.String, strPart_Number);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
                {
                    while (reader.Read())
                    {
                        ConsignmentBalanceDetails objTrans = new ConsignmentBalanceDetails();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Name = reader["Supplier_Name"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objTrans.Item_short_Description = reader["Item_short_Description"].ToString();
                        objTrans.LossOfSaleQty = "0";
                        objTrans.List_Price = reader["list_price"].ToString();
                        objTrans.OS_LS_indicator = reader["os_ls_indicator"].ToString();
                        objTrans.Balance_Quantity = reader["balance_quantity"].ToString();
                        objTrans.Location = reader["Location"].ToString();
                        objTrans.Balance_Quantity = reader["balance_quantity"].ToString();
                        objTrans.SLB = reader["SLB"].ToString();
                        objTrans.PO_Quantity = reader["PO_Quantity"].ToString();
                        objTrans.Document_Quantity = reader["Document_Quantity"].ToString();
                        objTrans.MRP = reader["MRP"].ToString();
                        objTrans.Application_Segment = reader["Application_Segment_Description"].ToString();
                        objTrans.Vehicle_Type = reader["Vehicle_Type"].ToString();
                        objTrans.Coupon = reader["Coupon"].ToString();
                        objTrans.Tax = reader["Tax_Percent"].ToString();
                        objTrans.OE_Reference = reader["OE_Reference"].ToString();

                        objGetBalanceDetails.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }

            return objGetBalanceDetails;
        }

        public List<ConsignmentBalanceDetails> GetConsignmentBalanceAlternate(string strPart_Number, string strBranch_Code)
        {
            List<ConsignmentBalanceDetails> objGetBalanceDetails = new List<ConsignmentBalanceDetails>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string strOSLSValue = string.Empty;

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_GetConsignmentBalanceAlternate");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch_Code);
                ImpalDB.AddInParameter(dbcmd, "@strPart_Number", DbType.String, strPart_Number);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(dbcmd))
                {
                    while (reader.Read())
                    {
                        ConsignmentBalanceDetails objTrans = new ConsignmentBalanceDetails();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Name = reader["Supplier_Name"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objTrans.Item_short_Description = reader["Item_short_Description"].ToString();
                        objTrans.LossOfSaleQty = "0";
                        objTrans.List_Price = reader["list_price"].ToString();
                        objTrans.OS_LS_indicator = reader["os_ls_indicator"].ToString();
                        objTrans.Balance_Quantity = reader["balance_quantity"].ToString();
                        objTrans.Location = reader["Location"].ToString();
                        objTrans.Balance_Quantity = reader["balance_quantity"].ToString();
                        objTrans.SLB = reader["SLB"].ToString();
                        objTrans.PO_Quantity = reader["PO_Quantity"].ToString();
                        objTrans.Document_Quantity = reader["Document_Quantity"].ToString();
                        objTrans.MRP = reader["MRP"].ToString();
                        objTrans.Application_Segment = reader["Application_Segment_Description"].ToString();
                        objTrans.Vehicle_Type = reader["Vehicle_Type"].ToString();
                        objTrans.Coupon = reader["Coupon"].ToString();
                        objTrans.Tax = reader["Tax_Percent"].ToString();
                        objTrans.AltPartNo = reader["AltPartNo"].ToString();
                        objTrans.SuperceededStatus = reader["SuperceededStatus"].ToString();
                        objTrans.OE_Reference = reader["OE_Reference"].ToString();

                        objGetBalanceDetails.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }

            return objGetBalanceDetails;
        }
    }
    #endregion  
     
    #region This class is used for Items Master Custom-DataType

    public class ConsignmentItemCode 
    {
        public string Item_Code { set; get; }
        public string Supplier_Part_Number { set; get; }  
    }

    public class ConsignmentBalanceDetails
    {
        public ConsignmentBalanceDetails()
        {}

        public string Item_Code { set; get; }
        public string Supplier_Name { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string Item_short_Description { set; get; }
        public string PO_Quantity { set; get; }

        public string List_Price { set; get; }
        public string OS_LS_indicator { set; get; }
        public string SLB { set; get; }        
        public string Balance_Quantity { set; get; }
        public string Document_Quantity { set; get; }
        public string LossOfSaleQty { set; get; }
        public string Location { set; get; }
        public string MRP { set; get; }
        public string Application_Segment { set; get; }
        public string Vehicle_Type { set; get; }
        public string Coupon { set; get; }
        public string Tax { set; get; }
        public string AltPartNo { set; get; }
        public string SuperceededStatus { set; get; }
        public string OE_Reference { set; get; }
    }
    #endregion


}
