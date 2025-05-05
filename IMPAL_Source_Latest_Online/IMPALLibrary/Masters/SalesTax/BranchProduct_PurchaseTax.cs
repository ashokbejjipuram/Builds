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
    #region Branch Product PurchaseTax Methods
        public class BranchProduct_PurchaseTax
        {
            public BranchProduct_PurchaseTax()
            { 
            
            }

            public List<BranchProductPurchaseTaxItems> GetBranchProductPurchaseTax()
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                List<BranchProductPurchaseTaxItems> ProductSalesTax = new List<BranchProductPurchaseTaxItems>();

                try
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = string.Empty;
                    sSQL = "select PB.Serial_Number,BM.Branch_Name,PGM.Product_Group_Description,STM.Sales_Tax_Description,STM.Sales_Tax_Percentage,";
                    sSQL = sSQL + "STM.Sales_Tax_Indicator,OS_LS_Indicator,PTM.Party_Type_Description,Sales_Tax_Text,Form_Name_Text,PB.Status ";
                    sSQL = sSQL + " from Product_Branch_Purchase_Tax PB ,Sales_Tax_Master STM,Product_Group_Master PGM,Party_Type_Master PTM,Branch_Master BM ";
                    sSQL = sSQL + " where PB.Sales_Tax_Code = STM.Sales_Tax_Code and PB.Product_Group_Code = PGM.Product_Group_Code and PTM.Party_Type_Code = PB.Party_Type_Code ";
                    sSQL = sSQL + " and PB.Branch_Code = BM.Branch_Code order by Serial_Number asc";
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                    {
                        while (reader.Read())
                        {
                            BranchProductPurchaseTaxItems objValue = new BranchProductPurchaseTaxItems();
                            objValue.SerialNumber = reader["Serial_Number"].ToString();
                            objValue.GVBranchName = reader["Branch_Name"].ToString();
                            objValue.OS_LS_Indicator = reader["OS_LS_Indicator"].ToString();
                            objValue.Party_Type_Desc = reader["Party_Type_Description"].ToString();
                            objValue.Product_Group_Desc = reader["Product_Group_Description"].ToString();
                            objValue.Sales_Tax_Desc = reader["Sales_Tax_Description"].ToString();                             

                            double strAmount = Math.Round((Convert.ToDouble(reader["Sales_Tax_Percentage"].ToString())), 2);
                            objValue.Sales_Tax_Percentage = string.Format((Convert.ToString(strAmount)), "#.00");

                            objValue.Sales_Tax_Indicator = reader["Sales_Tax_Indicator"].ToString();
                            objValue.Status = reader["Status"].ToString();
                            objValue.Form_Name_Text = reader["Form_Name_Text"].ToString();
                            objValue.Sales_Tax_Text = reader["Sales_Tax_Text"].ToString();
                            ProductSalesTax.Add(objValue);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException(Source, exp);
                }
               
                return ProductSalesTax;
            }

            public BranchProductPurchaseTaxItems getPurchaseSalesIndicatorAndPercentage(string strSaleTaxCode)
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                BranchProductPurchaseTaxItems objValue = new BranchProductPurchaseTaxItems();
                try
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = string.Empty;
                    
                    sSQL = "Select Sales_Tax_Percentage,Sales_Tax_Indicator from Sales_Tax_Master where Sales_Tax_Code=" + Convert.ToInt32(strSaleTaxCode);
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                    {
                        while (reader.Read())
                        {
                           // objValue.Sales_Tax_Percentage = reader["Sales_Tax_Percentage"].ToString();
                            
                            double strAmount = Math.Round((Convert.ToDouble(reader["Sales_Tax_Percentage"].ToString())), 2);
                            objValue.Sales_Tax_Percentage = string.Format((Convert.ToString(strAmount)), "#.00");

                            objValue.Sales_Tax_Indicator = reader["Sales_Tax_Indicator"].ToString();
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException(Source, exp);
                }
                return objValue;
            }
            
            public int PurchaseCheckDuplicate(BranchProductPurchaseTaxItems objVlaue)
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                int iRowCount = -1;
                try
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    string sSQL = string.Empty;
                    sSQL = "select 1 as iRowCounts from Product_Branch_Sales_Tax  where Branch_Code = '" + objVlaue.GVBranchCode + "' and  Product_Group_Code = " + Convert.ToInt32(objVlaue.Product_Group_Code.Trim()) + " and sales_tax_code=" + Convert.ToInt32(objVlaue.Sales_Tax_Code.Trim()) +  " and OS_LS_Indicator = '" + objVlaue.OS_LS_Indicator +  "' and party_type_code = '" + objVlaue.Party_Type_Code +  "'";
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd1));
                }
                catch (Exception exp)
                {
                    Log.WriteException(Source, exp);
                }

                return iRowCount;
            }

            public int InsBranchProductPurchaseTax(BranchProductPurchaseTaxItems objVlaue)
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                int iRowCount = -1;

                try
                {
                    using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                    {
                        Database ImpalDB = DataAccess.GetDatabase();
                        //// Create command to execute the stored procedure and add the parameters.
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddProductBranchPurchasetax");
                        //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objVlaue.GVBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.Int32, Convert.ToInt32(objVlaue.Product_Group_Code));
                        ImpalDB.AddInParameter(cmd, "@Sales_Tax_Code", DbType.Int32, Convert.ToInt32(objVlaue.Sales_Tax_Code));
                        ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, objVlaue.OS_LS_Indicator);

                        ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, objVlaue.Party_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Sales_tax_text", DbType.String, objVlaue.Sales_Tax_Text);
                        ImpalDB.AddInParameter(cmd, "@Form_Name_Text", DbType.String, objVlaue.Form_Name_Text);
                        ImpalDB.AddInParameter(cmd, "@Status", DbType.String, objVlaue.Status);
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

            public int UpdBranchProductPurchaseTax(BranchProductPurchaseTaxItems objVlaue)
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                int iRowCount = -1;

                try
                {
                    using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                    {
                        Database ImpalDB = DataAccess.GetDatabase();
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdProductBranchPurchaseTax");
                        ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, Convert.ToInt32(objVlaue.SerialNumber));
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objVlaue.GVBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Product_Group_Code", DbType.Int32, Convert.ToInt32(objVlaue.Product_Group_Code));
                        ImpalDB.AddInParameter(cmd, "@Sales_Tax_Code", DbType.Int32, Convert.ToInt32(objVlaue.Sales_Tax_Code));
                        ImpalDB.AddInParameter(cmd, "@OS_LS_Indicator", DbType.String, objVlaue.OS_LS_Indicator);

                        ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, objVlaue.Party_Type_Code);
                        ImpalDB.AddInParameter(cmd, "@Sales_tax_text", DbType.String, objVlaue.Sales_Tax_Text);
                        ImpalDB.AddInParameter(cmd, "@Form_Name_Text", DbType.String, objVlaue.Form_Name_Text);
                        ImpalDB.AddInParameter(cmd, "@Status", DbType.String, objVlaue.Status);
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

            public BranchProductPurchaseTaxItems getBranchProductPurchaseTaxSerialNo(string strSerialNumber)
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                BranchProductPurchaseTaxItems objValue = new BranchProductPurchaseTaxItems();
                 
                try
                {
                   
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Getproductbranchpurchasetax");
                    ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, Convert.ToInt32(strSerialNumber));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                           
                           // objValue.SerialNumber = reader["Serial_Number"].ToString();
                            objValue.GVBranchCode = reader["Branch_Code"].ToString();
                            objValue.OS_LS_Indicator = reader["OS_LS_Indicator"].ToString();
                            objValue.Party_Type_Code = reader["Party_Type_code"].ToString();
                            objValue.Product_Group_Code = reader["Product_Group_Code"].ToString();
                            objValue.Sales_Tax_Code = reader["Sales_Tax_Code"].ToString();
                           // objValue.Sales_Tax_Percentage = reader["Sales_Tax_Percentage"].ToString();

                            double strAmount = Math.Round((Convert.ToDouble(reader["Sales_Tax_Percentage"].ToString())), 2);
                            objValue.Sales_Tax_Percentage = string.Format((Convert.ToString(strAmount)), "#.00");

                            objValue.Sales_Tax_Indicator = reader["Sales_Tax_Indicator"].ToString();
                            objValue.Status = reader["Status"].ToString();
                            objValue.Form_Name_Text = reader["Form_name_text"].ToString();
                            objValue.Sales_Tax_Text = reader["Sales_tax_text"].ToString();
                        }
                    }
                     
                }
                catch (Exception exp)
                {
                    Log.WriteException(Source, exp);
                }
                return objValue;
            }
        }
    #endregion

    #region Custom Data type
        public class BranchProductPurchaseTaxItems
    {
        public BranchProductPurchaseTaxItems()
        {             
        }
        public string SerialNumber{get; set; }
        public string GVBranchName { get; set; }
        public string GVBranchCode { get; set; }
        public string Product_Group_Desc { get; set; }
        public string Product_Group_Code { get; set; }
        public string Sales_Tax_Desc { get; set; }
        public string Sales_Tax_Code { get; set; }
        public string Sales_Tax_Indicator { get; set; }
        public string Sales_Tax_Percentage { get; set; }
        public string OS_LS_Indicator { get; set; }
        public string Party_Type_Desc { get; set; }
        public string Party_Type_Code { get; set; }
        public string Form_Name_Text { get; set; }
        public string Sales_Tax_Text { get; set; }
        public string Status { get; set; }

    }
    #endregion
}
