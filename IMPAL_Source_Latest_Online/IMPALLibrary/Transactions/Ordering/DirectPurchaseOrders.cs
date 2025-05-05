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
    #region This class is used Direct PO View / Add mode functionalities

    public class DirectPurchaseOrders
    {
        public DirectPurchaseOrders() { }

        public List<ddlCustomerType> GetAllCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlCustomerType> ddlCustomerTypes = new List<ddlCustomerType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Customer_Code,Customer_Name,Status [Customer_Status],Location from customer_Master WITH (NOLOCK) where branch_code = '" + strBranchCode + "' and Status='A' order by Customer_Name";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlCustomerType objCustomer = new ddlCustomerType();
                        objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                        objCustomer.Customer_Name = reader["Customer_Name"].ToString() + "-" + reader["Location"].ToString();
                        ddlCustomerTypes.Add(objCustomer);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlCustomerTypes;
        }

        public List<ddlCustomerType> GetAllCustomersEPO(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlCustomerType> ddlCustomerTypes = new List<ddlCustomerType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Customer_Code,Customer_Name,Status [Customer_Status],Location from customer_Master WITH (NOLOCK) where branch_code = '" + strBranchCode + "' and Status='A' order by Customer_Name";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlCustomerType objCustomer = new ddlCustomerType();
                        objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                        objCustomer.Customer_Name = reader["Customer_Name"].ToString() + " || " + reader["Location"].ToString() + " || " + reader["Customer_Code"].ToString();
                        ddlCustomerTypes.Add(objCustomer);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlCustomerTypes;
        }

        public List<ddlCustomerType> GetAllIndentRegularCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlCustomerType> ddlCustomerTypes = new List<ddlCustomerType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Customer_Code,Customer_Name,Status [Customer_Status],Location from customer_Master WITH (NOLOCK) where branch_code = '" + strBranchCode + "'  and Destination not in ('DLRSTU','DLRGOV','DLRCFD') and Status='A' order by Customer_Name";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlCustomerType objCustomer = new ddlCustomerType();
                        objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                        objCustomer.Customer_Name = reader["Customer_Name"].ToString() + "-" + reader["Location"].ToString();
                        ddlCustomerTypes.Add(objCustomer);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlCustomerTypes;
        }

        public List<ddlCustomerType> GetAllIndentSTUCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlCustomerType> ddlCustomerTypes = new List<ddlCustomerType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Customer_Code,Customer_Name,Status [Customer_Status],Location from customer_Master WITH (NOLOCK) where branch_code = '" + strBranchCode + "' and Destination in ('DLRSTU','DLRGOV','DLRCFD') and Status='A' order by Customer_Name";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlCustomerType objCustomer = new ddlCustomerType();
                        objCustomer.Customer_Code = reader["Customer_Code"].ToString();
                        objCustomer.Customer_Name = reader["Customer_Name"].ToString() + "-" + reader["Location"].ToString();
                        ddlCustomerTypes.Add(objCustomer);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlCustomerTypes;
        }

        public List<ddlTransactionType> GetAllOrdTransactionTypes()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlTransactionType> ddlTransactionTypes = new List<ddlTransactionType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                //string sSQL = "SELECT [Transaction_Type_Code], [Transaction_Type_Description] FROM transaction_type_master where Transaction_Type_Code in ('201','321','331','341','361','371','411','421','431','441','451','461','471') ORDER BY Transaction_Type_Code ";
                string sSQL = "SELECT Transaction_Type_Code, Transaction_Type_Description FROM transaction_type_master where Transaction_Type_Code in ('201','451') ORDER BY Transaction_Type_Code ";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlTransactionType objTrans = new ddlTransactionType();
                        objTrans.Transaction_Type_Code = reader[0].ToString();
                        objTrans.Transaction_Type_Description = reader[1].ToString();
                        ddlTransactionTypes.Add(objTrans);
                        // ddlcustomers.Add(new ddlcustomer(reader[0].ToString(), reader[1].ToString()));
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlTransactionTypes;
        }

        public List<ddlSupplierType> GetAllOrdSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlSupplierType> ddlSupplierTypes = new List<ddlSupplierType>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "SELECT supplier_code, supplier_name FROM supplier_master ORDER BY supplier_name ";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlSupplierType objSupplier = new ddlSupplierType();
                        objSupplier.Supplier_Code = reader[0].ToString();
                        objSupplier.Supplier_Name = reader[1].ToString();
                        ddlSupplierTypes.Add(objSupplier);
                        // ddlcustomers.Add(new ddlcustomer(reader[0].ToString(), reader[1].ToString()));
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlSupplierTypes;
        }

        public List<Branch> GetAllOrdBranches()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<Branch> ddlBranches = new List<Branch>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct BM.Branch_Code, BM.Branch_Name from Purchase_Order_Header POH, Branch_Master BM ";
                sSQL = sSQL + " where POH.Branch_Code = BM.Branch_Code order by Branch_Code ";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Branch objBranch = new Branch();
                        objBranch.BranchCode = reader["Branch_Code"].ToString();
                        objBranch.BranchName = reader["Branch_Name"].ToString();
                        ddlBranches.Add(objBranch);
                        // ddlcustomers.Add(new ddlcustomer(reader[0].ToString(), reader[1].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlBranches;
        }

        public List<ddlOrdPO_NumberType> GetAllOrdPO_Number(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlOrdPO_NumberType> ddlOrdPO_Numbers = new List<ddlOrdPO_NumberType>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPurchaseOrderDetails_HO");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlOrdPO_NumberType objOrdPO_Number = new ddlOrdPO_NumberType();
                        objOrdPO_Number.PO_Number = reader[0].ToString();
                        objOrdPO_Number.PO_Number = reader[0].ToString();
                        ddlOrdPO_Numbers.Add(objOrdPO_Number);
                        // ddlcustomers.Add(new ddlcustomer(reader[0].ToString(), reader[1].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlOrdPO_Numbers;
        }

        public List<ddlOrdPO_NumberType> GetAllOrdPO_Number_Reprint(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlOrdPO_NumberType> ddlOrdPO_Numbers = new List<ddlOrdPO_NumberType>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPurchaseOrderDetails_Reprint_HO");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlOrdPO_NumberType objOrdPO_Number = new ddlOrdPO_NumberType();
                        objOrdPO_Number.PO_Number = reader[0].ToString();
                        objOrdPO_Number.PO_Number = reader[0].ToString();
                        ddlOrdPO_Numbers.Add(objOrdPO_Number);
                        // ddlcustomers.Add(new ddlcustomer(reader[0].ToString(), reader[1].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlOrdPO_Numbers;
        }

        public List<ddlOrdPO_NumberType> GetIndentPO_Number(string strBranchCode, string POIndentFrDt, string POIndentToDt, string strLineCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlOrdPO_NumberType> ddlOrdPO_Number = new List<ddlOrdPO_NumberType>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "SELECT PO_Number FROM purchase_order_header PO INNER JOIN supplier_master S ON S.supplier_code = PO.supplier_code ";
                sSQL = sSQL + " where S.Supplier_name = '" + strLineCode + "'";
                sSQL = sSQL + " AND PO_date IS Not NULL AND Status = 'A' and Branch_Code = '" + strBranchCode + "'";
                sSQL = sSQL + " AND PO_Date >= convert(datetime, '" + POIndentFrDt + "',103)-1";
                sSQL = sSQL + " AND PO_Date <= convert(datetime, '" + POIndentToDt + "',103)+1 ORDER BY PO_Number desc";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlOrdPO_NumberType objOrdPO_Number = new ddlOrdPO_NumberType();
                        objOrdPO_Number.PO_Number = reader[0].ToString();
                        ddlOrdPO_Number.Add(objOrdPO_Number);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlOrdPO_Number;
        }

        public SupplierOrdLineDetails GetOrdSupplierLineDetails(string ItemCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            SupplierOrdLineDetails objOrdSupplierDetails = new SupplierOrdLineDetails();
            try
            {
                objOrdSupplierDetails.Appln_Segment_Description = "";
                objOrdSupplierDetails.Item_Code = "";
                objOrdSupplierDetails.Supp_Short_description = "";
                objOrdSupplierDetails.Vehicle_Type_Description = "";

                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = " select isnull(appln_segment_description,'') as  App_Segment_Desc from item_master a,application_segment_master b where a.application_segment_code = b.application_segment_code and a.item_code = '" + ItemCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        objOrdSupplierDetails.Appln_Segment_Description = reader["App_Segment_Desc"].ToString();
                    }
                }

                sSQL = " select short_description from item_master a,supplier_line_master b where a.supplier_line_code = b.supplier_line_code and a.item_code ='" + ItemCode + "'";
                DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd2))
                {
                    while (reader.Read())
                    {
                        objOrdSupplierDetails.Supp_Short_description = reader["short_description"].ToString();
                    }
                }

                sSQL = "select Vehicle_Type_Description from Item_master a, vehicle_type_master b where a.vehicle_type_code = b.vehicle_type_code and a.item_code = '" + ItemCode + "'";
                DbCommand cmd3 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd3))
                {
                    while (reader.Read())
                    {
                        objOrdSupplierDetails.Vehicle_Type_Description = reader["Vehicle_Type_Description"].ToString();
                    }
                }

                sSQL = "select supplier_part_number from item_master where item_code = '" + ItemCode + "'";
                DbCommand cmd4 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd4.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd4))
                {
                    while (reader.Read())
                    {
                        objOrdSupplierDetails.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objOrdSupplierDetails;
        }

        public SupplierOrdPartNumberDetails GetSupplierOrdPartNumberDetails(string strSuplierPartNo, string strSuplierCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            SupplierOrdPartNumberDetails objOrdPartNumber = new SupplierOrdPartNumberDetails();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = " select isnull(Application_Segment_code,'') as  Application_Segment_code ,isnull(Vehicle_Type_Code,'') as Vehicle_Type_Code, isnull(packing_quantity,'') packing_quantity , isNull(item_code,'') as item_code  from Item_master where supplier_part_number = '" + strSuplierPartNo + "' and  substring(supplier_line_code,1,3) ='" + strSuplierCode + "'  order by supplier_part_number";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objOrdPartNumber.Application_Segment_Code = reader["Application_Segment_code"].ToString();
                        objOrdPartNumber.Vehicle_Type_Code = reader["Vehicle_Type_Code"].ToString();
                        objOrdPartNumber.Packing_Quantity = reader["packing_quantity"].ToString();
                        objOrdPartNumber.Item_Code = reader["item_code"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objOrdPartNumber;
        }

        public List<ddlOrdPartNumberType> GetSupplierOrdPartNumberSearch(string strBranchCode, string strSuplierPartNo, string strSuplierCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<ddlOrdPartNumberType> ddlOrdPartNumbers = new List<ddlOrdPartNumberType>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetItemDetails_PO");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, strSuplierCode);
                ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, strSuplierPartNo);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ddlOrdPartNumberType objOrdPartNumber = new ddlOrdPartNumberType();
                        objOrdPartNumber.Supplier_Part_Number = reader["Supplier_Part_Number"].ToString();
                        objOrdPartNumber.Item_Code = reader["Item_Code"].ToString();
                        ddlOrdPartNumbers.Add(objOrdPartNumber);
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ddlOrdPartNumbers;
        }

        public DirectPOItems GetOrdDirectPO_ItemsCode(string strSupplier_Part_Number)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DirectPOItems objOrd_CustSection = new DirectPOItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL1 = "select item_code from item_master where Supplier_Part_Number = '" + strSupplier_Part_Number + "' order by Supplier_Part_Number";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL1);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objOrd_CustSection.Item_Code = reader["item_code"].ToString();
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objOrd_CustSection;
        }

        public CustomerSectionFields FillOrdDirectPO_CustomerSection(string strCustomer_Code, string strBranch_Code)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CustomerSectionFields objOrd_CustSection = new CustomerSectionFields();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string sSQL = "select Customer_Code, Customer_name, Address1, Address2, Address3, Address4, Location, Tin from Customer_master WITH (NOLOCK) WHERE branch_code = '" + strBranch_Code + "' AND customer_code = '" + strCustomer_Code + "' AND isnull(status,'A') = 'A'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objOrd_CustSection.Customer_Code = reader["Customer_Code"].ToString();
                        objOrd_CustSection.Customer_name = reader["Customer_name"].ToString();
                        objOrd_CustSection.Address1 = reader["Address1"].ToString();
                        objOrd_CustSection.Address2 = reader["Address2"].ToString();
                        objOrd_CustSection.Address3 = reader["Address3"].ToString();
                        objOrd_CustSection.Address4 = reader["Address4"].ToString();
                        objOrd_CustSection.Location = reader["Location"].ToString();
                        objOrd_CustSection.Tin_No = reader["Tin"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objOrd_CustSection;
        }

        public void UpdOrdDirectPO_Number(string strOrdDirectPONumber, string strStatus, string strBranchCode, string strTransactionTypeCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_UpdPoHeaderStatus");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@PONumber", DbType.String, strOrdDirectPONumber);
                    ImpalDB.AddInParameter(cmd, "@TransType_Code", DbType.String, strTransactionTypeCode);
                    ImpalDB.AddInParameter(cmd, "@Status", DbType.String, strStatus);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void UpdCCWH_PO_Number(string strBranchCode, string strCCWHPONumber)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_CCWH_GetDetails_New_461");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@PONumber", DbType.String, strCCWHPONumber);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public DataSet GetDSOrdDirectPO_Items(string strPO_Number, string Indicator, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet DS = new DataSet();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndent_OutDandS");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strPO_Number);
                ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, 1);
                ImpalDB.AddInParameter(cmd, "@Schedule_Number", DbType.Int32, 0);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                DS = ImpalDB.ExecuteDataSet(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return DS;
        }

        public List<DirectPOItems> GetOrdDirectPO_Items(string strPO_Number, string Indicator, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<DirectPOItems> grdViewItemDatas = new List<DirectPOItems>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndent_OutDandS");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strPO_Number);
                ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, 1);
                ImpalDB.AddInParameter(cmd, "@Schedule_Number", DbType.Int32, 0);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        DirectPOItems objSupplier = new DirectPOItems();
                        objSupplier.Application_Segment_Code = reader["Application_Segment_Code"].ToString();
                        objSupplier.Vehicle_Type_Code = reader["Vehicle_Type_Code"].ToString();
                        objSupplier.Packing_Quantity = reader["Packing_Quantity"].ToString();
                        objSupplier.Item_Code = reader["Item_Code"].ToString();
                        objSupplier.OrderItem_PO_Quantity = Convert.ToInt32(reader["PO_Quantity"].ToString());
                        objSupplier.OrderItem_Status = reader["Ord_Status"].ToString();
                        objSupplier.Supplier_Part_Number = reader["Supplier_Part_Number"].ToString();
                        objSupplier.Schedule_Date = reader["Schedule_Date"].ToString();
                        objSupplier.Schedule_PO_Quantity = Convert.ToInt32(reader["sche_Indent_Quantity"].ToString());
                        objSupplier.Valid_Days = Convert.ToInt32(reader["Valid_Days"].ToString());
                        objSupplier.Schedule_Status = reader["Sche_Status"].ToString();
                        objSupplier.Indent_Branch = reader["Sche_Indent_Branch"].ToString();

                        grdViewItemDatas.Add(objSupplier);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return grdViewItemDatas;
        }

        public List<DirectPOItems> GetIndentBranchesZoneWise(string strItemCode, int ZoneCode, string strBranchCode)
        {
            List<DirectPOItems> lstBranches = new List<DirectPOItems>();
            string sSQL = string.Empty;

            DirectPOItems objBranches = new DirectPOItems();
            objBranches.Indent_Branch = "0";
            objBranches.Indent_BranchName = "-- Select --";
            lstBranches.Add(objBranches);

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndentBranches_STDN");
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.Int16, ZoneCode);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objBranches = new DirectPOItems();
                    objBranches.Indent_Branch = reader[0].ToString();
                    objBranches.Indent_BranchName = reader[1].ToString();
                    lstBranches.Add(objBranches);
                }
            }

            return lstBranches;
        }

        public int GetIndentBranchesStock(string strBranchcode, string strItemCode, int ZoneCode)
        {
            string sSQL = string.Empty;
            int stock = 0;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndentBranchstock_STDN");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchcode.Trim());
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.Int16, ZoneCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            stock = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());

            return stock;
        }

        public List<DirectPOItems> GetOrdDirectPO_Items_HO(string strPO_Number, string Indicator, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<DirectPOItems> grdViewItemDatas = new List<DirectPOItems>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndent_OutDandS_HO");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strPO_Number);
                ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, 1);
                ImpalDB.AddInParameter(cmd, "@Schedule_Number", DbType.Int32, 0);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        DirectPOItems objSupplier = new DirectPOItems();
                        objSupplier.Application_Segment_Code = reader["Application_Segment_Code"].ToString();
                        objSupplier.Vehicle_Type_Code = reader["Vehicle_Type_Code"].ToString();
                        objSupplier.Packing_Quantity = reader["Packing_Quantity"].ToString();
                        objSupplier.Item_Code = reader["Item_Code"].ToString();
                        objSupplier.OrderItem_PO_Quantity = Convert.ToInt32(reader["PO_Quantity"].ToString());
                        objSupplier.OrderItem_Status = reader["Ord_Status"].ToString();
                        objSupplier.Supplier_Part_Number = reader["Supplier_Part_Number"].ToString();
                        objSupplier.Schedule_Date = reader["Schedule_Date"].ToString();
                        objSupplier.Schedule_PO_Quantity = Convert.ToInt32(reader["sche_Indent_Quantity"].ToString());
                        objSupplier.Valid_Days = Convert.ToInt32(reader["Valid_Days"].ToString());
                        objSupplier.Schedule_Status = reader["Sche_Status"].ToString();
                        objSupplier.Indent_Branch = reader["Sche_Indent_Branch"].ToString();

                        grdViewItemDatas.Add(objSupplier);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return grdViewItemDatas;
        }

        public string Ins_DirectPOHeaderAndItems(DirectPOHeader objValue, CustomerSectionFields objCustomer, DataTable objItemArrays)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strOutPO_Number = "";
            int SNo = 1;

            DirectPOHeader objPO_Number = new DirectPOHeader();
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (objItemArrays.Rows.Count > 0)
                    {
                        for (int i = 0; i < objItemArrays.Rows.Count; i++)
                        {
                            if (objItemArrays.Rows[i]["Item_Code"].ToString() != "")
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddDirectPurchaseOrder");
                                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strOutPO_Number.ToString());
                                ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, objValue.Transaction_Type_Code);
                                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, objValue.Branch_Code);
                                ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, objValue.Supplier_Code);
                                ImpalDB.AddInParameter(cmd, "@Reference_Number", DbType.String, objValue.Reference_Number);
                                ImpalDB.AddInParameter(cmd, "@Reference_Date", DbType.String, objValue.Reference_Date);
                                ImpalDB.AddInParameter(cmd, "@Road_Permit_Number", DbType.String, objValue.Road_Permit_Number);
                                ImpalDB.AddInParameter(cmd, "@Road_Permit_Date", DbType.String, objValue.Road_Permit_Date);
                                ImpalDB.AddInParameter(cmd, "@Carrier", DbType.String, objValue.Carrier);
                                ImpalDB.AddInParameter(cmd, "@Destination", DbType.String, objValue.Destination);
                                ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, objValue.Remarks);
                                ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, SNo);
                                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, objItemArrays.Rows[i]["Item_Code"].ToString());
                                ImpalDB.AddInParameter(cmd, "@Item_PO_Quantity", DbType.Int32, Convert.ToInt32(objItemArrays.Rows[i]["OrderItem_PO_Quantity"]));
                                ImpalDB.AddInParameter(cmd, "@Item_Status", DbType.String, objItemArrays.Rows[i]["OrderItem_Status"].ToString());
                                ImpalDB.AddInParameter(cmd, "@Schedule_PO_Quantity", DbType.Int32, Convert.ToInt32(objItemArrays.Rows[i]["OrderItem_PO_Quantity"]));
                                ImpalDB.AddInParameter(cmd, "@Schedule_Date", DbType.String, objItemArrays.Rows[i]["Schedule_Date"].ToString());
                                ImpalDB.AddInParameter(cmd, "@Valid_Days", DbType.Int32, 0); // Convert.ToInt32(objItemArrays.Rows[i]["Valid_Days"]));
                                ImpalDB.AddInParameter(cmd, "@Indent_Branch", DbType.String, objValue.Branch_Code); //objItemArrays.Rows[i]["Indent_Branch"].ToString());
                                ImpalDB.AddInParameter(cmd, "@Schedule_Status", DbType.String, objItemArrays.Rows[i]["Schedule_status"].ToString());
                                ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, objItemArrays.Rows[i]["Item_Code"].ToString().Substring(0, 6));
                                ImpalDB.AddInParameter(cmd, "@OrderStatus", DbType.String, objItemArrays.Rows[i]["OrderItem_Status"].ToString());
                                ImpalDB.AddInParameter(cmd, "@customercode", DbType.String, objCustomer.Customer_Code);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                strOutPO_Number = ImpalDB.ExecuteScalar(cmd).ToString();
                                SNo++;

                                cmd = null;
                            }
                        }

                        //string[] branches = { "BGL", "CAL", "MGT", "GUW", "MDU", "CBE", "ATR" };

                        //if (((branches.Contains(objValue.Branch_Code.ToUpper()) && objValue.Transaction_Type_Code == "451") || objValue.Transaction_Type_Code == "421" || objValue.Transaction_Type_Code == "441")
                        //    || objValue.Branch_Code.ToUpper() == "CA1" || (objValue.Branch_Code.ToUpper() == "CHE" && (objValue.Supplier_Code == "410" || objValue.Supplier_Code == "320"))
                        //    || objValue.Branch_Code.ToUpper() == "MGT" || objValue.Branch_Code.ToUpper() == "CAL")
                        //{
                        //    string sSQL = "update purchase_order_header set ccwh_no='" + strOutPO_Number + "',ccwh_date='" + DateTime.Today.ToString() + "' where branch_code = '" + objValue.Branch_Code + "' and po_number='" + strOutPO_Number + "'";
                        //    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                        //    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        //    ImpalDB.ExecuteNonQuery(cmd1);
                        //}

                        DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_UpdPurchaseOrderHeader");
                        ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, objValue.Branch_Code);
                        ImpalDB.AddInParameter(cmd1, "@PO_Number", DbType.String, strOutPO_Number.ToString());
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd1);

                        cmd1 = null;
                    }                    

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return strOutPO_Number;
        }

        public string Ins_DirectPOHeaderAndItemsHO(DirectPOHeaderHO poEntity)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string result = "0";
            DirectPOHeaderHO objPO_Number = new DirectPOHeaderHO();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    foreach (DirectPOItemsHO poItem in poEntity.Items)
                    {
                        if (poItem.Item_Code.ToString() != "")
                        {
                            cmd = ImpalDB.GetStoredProcCommand("usp_AddPurchaseOrderSTDN_HO");
                            ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, poEntity.PO_Number.ToString());
                            ImpalDB.AddInParameter(cmd, "@PO_Date", DbType.String, poEntity.PO_Indent_Date.ToString());
                            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, poEntity.Transaction_Type_Code);
                            ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int16, poItem.Serial_Number);
                            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, poEntity.Branch_Code);
                            ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, poEntity.Supplier_Code);
                            ImpalDB.AddInParameter(cmd, "@Supplier_Part_Number", DbType.String, poItem.Supplier_Part_Number.ToString());
                            ImpalDB.AddInParameter(cmd, "@PO_Serial_Number", DbType.String, poItem.OrderItem_PO_SNo.ToString());
                            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, poItem.Item_Code.ToString());
                            ImpalDB.AddInParameter(cmd, "@PO_Qty", DbType.String, poItem.OrderItem_PO_Quantity.ToString());
                            ImpalDB.AddInParameter(cmd, "@Customer_Code", DbType.String, poEntity.customercode);
                            ImpalDB.AddInParameter(cmd, "@STDNBranchItemList", DbType.String, poItem.IndentBranchDataCollection);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                        }
                    }

                    cmd = null;

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_UpdPurchaseOrderSTDN_HO");
                    ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, poEntity.Branch_Code);
                    ImpalDB.AddInParameter(cmd1, "@PO_Number", DbType.String, poEntity.PO_Number.ToString());                    
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    cmd1 = null;

                    poEntity.ErrorCode = "0";
                    poEntity.ErrorMessage = "";
                    result = "0";
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = "1";
                Log.WriteException(Source, exp);
            }

            return result;
        }

        public DataSet GetPO_StdnSupplierDetails(string BranchCode, string PONumber, string Supplier, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetPOSTDNDetails_report");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, PONumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier", DbType.String, Supplier.Trim());
            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetPO_StdnSupplierDetails_BulkUploadHO(string fileName, string strdate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetPOdetails_Report_BulkUpload");
            ImpalDB.AddInParameter(cmd, "@File_Name", DbType.String, fileName.Trim());
            ImpalDB.AddInParameter(cmd, "@Date", DbType.String, strdate.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetPO_StdnSupplierDetails_BulkUploadHO_Reprint(string strdate)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetPOdetails_Report_BulkUpload_Reprint");
            ImpalDB.AddInParameter(cmd, "@Date", DbType.String, strdate.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetPO_StdnSupplierDetailsReprint(string BranchCode, string PONumber, string Supplier, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetPOSTDNDetails_report_Reprint");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, PONumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Supplier", DbType.String, Supplier.Trim());
            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds;
        }

        public DataSet GetLineWiseOrdersPlacedDetails(string SupplierCode, string BranchCode, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetLineWiseOrdersPlacedDetails");
                ImpalDB.AddInParameter(cmd, "@Supplier_Code", DbType.String, SupplierCode.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate.Trim());
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ds = ImpalDB.ExecuteDataSet(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ds;
        }

        public List<Branch> GetBranches()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<Branch> ddlBranches = new List<Branch>();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select Branch_Code,branch_name from branch_master Where Branch_Code not in ('COR','CRP') and Status='A' order by branch_Name";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Branch objBranch = new Branch();
                        objBranch.BranchCode = reader["Branch_Code"].ToString();
                        objBranch.BranchName = reader["Branch_Name"].ToString();
                        ddlBranches.Add(objBranch);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
			
            return ddlBranches;
        }

        public DirectPOView Get_DirectPOHeaderViewMode(string strPO_Number, string strIndicator, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DirectPOView objPO_Number = new DirectPOView();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetIndent_OutDandS");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@PO_Number", DbType.String, strPO_Number);
                ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.Int32, 0);
                ImpalDB.AddInParameter(cmd, "@Schedule_Number", DbType.Int32, 0);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, strIndicator);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objPO_Number.PO_Number = reader["PO_Number"].ToString();
                        objPO_Number.Indent_Date = reader["Indent_Date"].ToString();
                        objPO_Number.Branch_Code = reader["Branch_Code"].ToString();
                        objPO_Number.Customer_Code = reader["Customer_Code"].ToString();
                        objPO_Number.Supplier_Code = reader["Supplier_Code"].ToString();
                        objPO_Number.Reference_Number = reader["Reference_Number"].ToString();
                        objPO_Number.Reference_Date = reader["Reference_Date"].ToString();
                        objPO_Number.Road_Permit_Number = reader["Road_Permit_Number"].ToString();
                        objPO_Number.Road_Permit_Date = reader["Road_Permit_Date"].ToString();
                        objPO_Number.Carrier = reader["Carrier"].ToString();
                        objPO_Number.Destination = reader["Destination"].ToString();
                        objPO_Number.Remarks = reader["Remarks"].ToString();
                        objPO_Number.Transaction_Type_Code = reader["Transaction_Type_Code"].ToString();
                        objPO_Number.Transaction_Type_Description = reader["Transaction_Type_Description"].ToString();
                        objPO_Number.Branch_Name = reader["Branch_Name"].ToString();
                        objPO_Number.supplier_name = reader["supplier_name"].ToString();
                        objPO_Number.po_date = reader["po_date"].ToString();
                        objPO_Number.orderstatus = reader["orderstatus"].ToString();
                        objPO_Number.Address1 = reader["Address1"].ToString();
                        objPO_Number.Address2 = reader["Address2"].ToString();
                        objPO_Number.Address3 = reader["Address3"].ToString();
                        objPO_Number.Address4 = reader["Address4"].ToString();
                        objPO_Number.Location = reader["Location"].ToString();
                        objPO_Number.Customer_Code = reader["Customer_Code"].ToString();
                        objPO_Number.PO_Type = reader["PO_Type"].ToString();
                        objPO_Number.PO_Value = reader["PO_Value"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objPO_Number;
        }
    }

    #endregion

    #region This class is used for PartNumber Custom-DataType
    public class ddlOrdPartNumberType
    {
        public string Supplier_Part_Number { set; get; }
        public string Item_Code { set; get; }
        // public string OrderStatus { set; get; }
    }
    #endregion

    #region This class is used for Items Master Custom-DataType
    public class DirectPOItems
    {
        // public int Serial_Number { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string Application_Segment_Code { set; get; }
        public string Vehicle_Type_Code { set; get; }
        public string Packing_Quantity { set; get; }
        public string Item_Code { set; get; }
        public int OrderItem_PO_Quantity { set; get; }
        public string OrderItem_Status { set; get; }
        public int Schedule_PO_Quantity { set; get; }
        public string Schedule_Date { set; get; }
        public int Valid_Days { set; get; }
        public string Indent_Branch { set; get; }
        public string Indent_BranchName { set; get; }
        public string Schedule_Status { set; get; }
        //public string Supplier_Line_Code { set; get; }
        public string OrderStatus { set; get; }
        public string IndentBranchDataCollection { set; get; }
    }
    #endregion

    #region This class is used for DirectPOHeader Custom-DataType
    public class DirectPOHeader
    {
        public string PO_Number { set; get; }
        public string PO_Indent_Date { set; get; }
        public string Transaction_Type_Code { set; get; }
        public string Branch_Code { set; get; }
        public string Supplier_Code { set; get; }
        public string Reference_Number { set; get; }
        public string Reference_Date { set; get; }
        public string Road_Permit_Number { set; get; }
        public string Road_Permit_Date { set; get; }
        public string Carrier { set; get; }
        public string Destination { set; get; }
        public string Remarks { set; get; }
        public string customercode { set; get; }

        public string ErrorMessage { set; get; }
        public string ErrorCode { set; get; }
    }
    #endregion

    #region This class is used for Items Master Custom-DataType
    public class DirectPOItemsHO
    {
        public int Serial_Number { set; get; }
        public string Supplier_Part_Number { set; get; }
        public string Packing_Quantity { set; get; }
        public string Item_Code { set; get; }
        public int OrderItem_PO_Quantity { set; get; }
        public int OrderItem_PO_SNo { set; get; }
        public string IndentBranchDataCollection { set; get; }
    }
    #endregion

    #region This class is used for DirectPOHeader Custom-DataType
    public class DirectPOHeaderHO
    {
        public string PO_Number { set; get; }
        public string PO_Indent_Date { set; get; }
        public string Transaction_Type_Code { set; get; }
        public string Branch_Code { set; get; }
        public string Supplier_Code { set; get; }
        public string customercode { set; get; }

        public string ErrorMessage { set; get; }
        public string ErrorCode { set; get; }

        public List<DirectPOItemsHO> Items { get; set; }
    }
    #endregion

    #region This class is used for DirectPOView(ItemsAndHeader) Custom-DataType
    public class DirectPOView
    {

        public string PO_Number { set; get; }
        public string Indent_Date { set; get; }
        public string Transaction_Type_Code { set; get; }
        public string Branch_Code { set; get; }
        public string Supplier_Code { set; get; }
        public string Reference_Number { set; get; }
        public string Reference_Date { set; get; }
        public string Road_Permit_Number { set; get; }
        public string Road_Permit_Date { set; get; }
        public string Carrier { set; get; }
        public string Destination { set; get; }
        public string Remarks { set; get; }
        public string Transaction_Type_Description { set; get; }
        public string Branch_Name { set; get; }
        public string Customer_Name { set; get; }
        public string supplier_name { set; get; }
        public string po_indent_date { set; get; }
        public string po_date { set; get; }
        public string orderstatus { set; get; }
        public string Address1 { set; get; }
        public string Address2 { set; get; }
        public string Address3 { set; get; }
        public string Tin_No { set; get; }
        public string Address4 { set; get; }
        public string Location { set; get; }
        public string Customer_Code { set; get; }
        public string PO_Type { set; get; }
        public string PO_Value { set; get; }
    }

    public class ddlTransactionType
    {
        public string Transaction_Type_Code { get; set; }
        public string Transaction_Type_Description { get; set; }
    }

    public class ddlSalesManType
    {
        public string Sales_Man_code { get; set; }
        public string Sales_Man_Name { get; set; }
    }

    public class ddlCustomerType
    {
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Location { get; set; }
    }

    public class ddlInvoiceNumberType
    {
        public string Document_Number { get; set; }
        public string Document_Number_Value { get; set; }
    }

    public class ddlSupplierType
    {
        public string Supplier_Code { get; set; }
        public string Supplier_Name { get; set; }
    }

    public class CustomerSectionFields
    {
        public string Customer_Code { get; set; }
        public string Customer_name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Location { get; set; }
        public string Tin_No { get; set; }
    }

    public class ddlOrdPO_NumberType
    {
        public string PO_Number { get; set; }
        // public string Supplier_Name { get; set; }
    }

    public class OrdBranchName
    {
        public string Order_BranchName { get; set; }
        public string Order_Branch_Code { get; set; }
        public string Road_Permit_Indicator { get; set; }
    }

    public class SupplierOrdPartNumberDetails
    {
        public string Application_Segment_Code { get; set; }
        public string Vehicle_Type_Code { get; set; }
        public string Packing_Quantity { get; set; }
        public string Item_Code { get; set; }
    }

    public class SupplierOrdLineDetails
    {
        public string Supplier_Part_Number { get; set; }
        public string Appln_Segment_Description { get; set; }
        public string Vehicle_Type_Description { get; set; }
        public string Supp_Short_description { get; set; }
        public string Item_Code { get; set; }
    }
    #endregion
}