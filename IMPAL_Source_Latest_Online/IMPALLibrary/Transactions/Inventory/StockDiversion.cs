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
    public class StockDiversion
    {
        public StockDiversion()
        { }

        public List<DiversionNumber> GetDiversionNumber(string strBranchCode)
        {
            List<DiversionNumber> objDiversionNumbers = new List<DiversionNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select Diversion_Number from Stock_Diversion where SUBSTRING(Diversion_Number, charindex('/', Diversion_Number, charindex('/', Diversion_Number, charindex('/', Diversion_Number)+1))+1, 3)= '" + strBranchCode + "' and substring(Diversion_Number,0,5) in (year(GETDATE()), year(DateAdd(yyyy, -1, GetDate()))) order by Diversion_Number desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;             
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        DiversionNumber objTrans = new DiversionNumber();
                        objTrans.Diversion_Number = reader["Diversion_Number"].ToString();

                        objDiversionNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return objDiversionNumbers;
        }

        public StockDiversionItems GetStockDiversion(  string strDiversion_number, string strBranchCode)
        {             
            StockDiversionItems objTrans = new StockDiversionItems();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;

                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetStockDiversion");
				ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Diversion_Number", DbType.String, strDiversion_number);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Diversion_Number = strDiversion_number;
                        objTrans.Diversion_date = reader["Diversion_Date"].ToString();
                        objTrans.From_TransactionType = reader["From_Transaction_Type"].ToString();
                        objTrans.To_TransactionType = reader["To_Transaction_Type"].ToString();
                        objTrans.Inward_Number = reader["Inward_Number"].ToString();
                        objTrans.Item_Code = reader["Item_Code"].ToString();
                        objTrans.Quantity = Convert.ToInt32(reader["Quantity"]);
                        objTrans.Reason = reader["Reason"].ToString();                        
                    }
                }

                sSQL = "  SELECT Supplier_Part_Number FROM Item_Master where item_code = '" + objTrans.Item_Code + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        objTrans.Supplier_Part_Number = reader["Supplier_Part_Number"].ToString();                        
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return objTrans;
        }

        public List<ddlTransactionType> GetAllStockTransactionTypes()
        {
            List<ddlTransactionType> ddlTransactionTypes = new List<ddlTransactionType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "SELECT [Transaction_Type_Code], [Transaction_Type_Description] FROM transaction_type_master Where Transaction_Type_Code in (041,141,101,111,171,201,361,421,431,441,451,481) ORDER BY Transaction_Type_Code";
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
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            
            return ddlTransactionTypes;
        }

        public List<ddlTransactionType> GetFromStockTransactionTypes()
        {
            List<ddlTransactionType> ddlTransactionTypes = new List<ddlTransactionType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                
                string sSQL = "";
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getFromTransactions_StockDiversion");
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
                Log.WriteException(typeof(StockDiversion), exp);
            }

            return ddlTransactionTypes;
        }

        public List<ddlTransactionType> GetToStockTransactionTypes()
        {
            List<ddlTransactionType> ddlTransactionTypes = new List<ddlTransactionType>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_getToTransactions_StockDiversion");
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
                Log.WriteException(typeof(StockDiversion), exp);
            }

            return ddlTransactionTypes;
        }

        public List<ConsignmentItemCode> GetItemCode(string strTransactionType, string strSupplierPartNumber, string strBranchCode)
        {
            List<ConsignmentItemCode> objItemCodes = new List<ConsignmentItemCode>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct a.item_code,supplier_part_number from consignment a, item_master b where a.item_Code = b.item_Code and a.status ='A' and a.branch_code = '" + strBranchCode + "' and Transaction_Type_Code='" + strTransactionType + "' and supplier_part_number like'" + strSupplierPartNumber + "%' order by supplier_part_number ";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ConsignmentItemCode objTrans = new ConsignmentItemCode();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objItemCodes.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return objItemCodes;
        }

        public List<InwardNumber> GetInwardNumber(string strTransactionType, string strItemcode,string strBranchCode)
        {
            List<InwardNumber> objInwardNumbers = new List<InwardNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "";
               
                if ( (strTransactionType != "361") && (strTransactionType  != "371") && (strTransactionType != "461") && ( strTransactionType  != "471" )  )
                {
                    sSQL = "select distinct Inward_Number from Consignment where branch_code='" + strBranchCode + "' and status = 'A' and Transaction_Type_Code='"+ strTransactionType + "' and item_code = '" + strItemcode + "' order by Inward_Number DESC" ;
                }
                else
                {
                    sSQL = "select Inward_Number from Inward_header where branch_code='" + strBranchCode + "'  and Transaction_Type_Code='" + strTransactionType + "' and item_code = '" + strItemcode + "' and inward_number not in (select distinct inward_number from consignment where branch_code='" + strBranchCode + "' and Transaction_Type_Code='" +strTransactionType + "')and inward_number not in (select distinct reference_document_number from sales_requisition_header where substring(reference_document_number,12,3)='" + strBranchCode + "' and Transaction_Type_Code='" + strTransactionType + "')";
                }
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;                             
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        InwardNumber objTrans = new InwardNumber();
                        objTrans.Inward_Number = reader["inward_number"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return objInwardNumbers;
        }

        public List<OSLSSerialNo> GetSerialNumber(string strTransactionType, string strItemcode, string strInwardNumber, string strBranchCode)
        {
            List<OSLSSerialNo> objInwardNumbers = new List<OSLSSerialNo>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "";

                if ((strTransactionType != "361") && (strTransactionType != "371") && (strTransactionType != "461") && (strTransactionType != "471"))
                {
                    sSQL = "select DISTINCT supplier_part_number,a.Item_Code,serial_number from Consignment a,item_master b where a.Branch_Code = '" + strBranchCode + "' and  a.item_Code = b.item_Code and Inward_Number='" + strInwardNumber + "' and b.Item_Code='" + strItemcode + "' and a.status ='A'";
                }
                else
                {
                    sSQL = "Select DISTINCT supplier_part_number,a.Item_Code,serial_number from Inward_Detail a,item_master b where";
					sSQL = sSQL + " SUBSTRING(Inward_Number, charindex('/', Inward_Number, charindex('/', Inward_Number, charindex('/', Inward_Number)+1))+1, 3)= '" + strBranchCode + "' and ";
					sSQL = sSQL + " a.item_Code = b.item_Code and Inward_Number='" + strInwardNumber +"' and a.Item_Code not in (select Item_Code from Stock_Diversion"; 
					sSQL = sSQL + " where SUBSTRING(Diversion_Number, charindex('/', Diversion_Number, charindex('/', Diversion_Number, charindex('/', Diversion_Number)+1))+1, 3)= '" + strBranchCode + "'";
					sSQL = sSQL + " and Inward_number='" + strInwardNumber + "')";
                }
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        OSLSSerialNo objTrans = new OSLSSerialNo();
                        objTrans.OSLS_SerialNumber = reader["serial_number"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return objInwardNumbers;
        }

        public Quantity GetQuantiy(string strTransactionType, string strSupplierPartNumber,string strItemCode,string strInwardNumber, string strSerialNo , string strBranchCode)
        {
            Quantity objTrans = new Quantity();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "";

                if ((strTransactionType != "361") && (strTransactionType != "371") && (strTransactionType != "461") && (strTransactionType != "471"))
                {
                    sSQL = " select isnull(Balance_Quantity,0) as Balance_Quantity from consignment where Branch_code = '" + strBranchCode + "' and Status = 'A' and Inward_Number='" + strInwardNumber + "'and Transaction_Type_Code='" + strTransactionType + "' and Item_Code='" + strItemCode + "' and Serial_Number = '" + strSerialNo + "'";
                }
                else
                {
                    sSQL = "select actual_quantity as Balance_Quantity from inward_detail a,Inward_Header b where b.Branch_code = '" + strBranchCode + "' and a.Inward_Number =b.Inward_Number and a.Inward_Number='" + strInwardNumber + "'and b.Transaction_Type_Code='" + strTransactionType + "' and Item_Code='" + strItemCode + "'";
                }
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.Quantity_No = reader["Balance_Quantity"].ToString();                     
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }

            return objTrans;
                   
        }

        public int GetItemCount(string strTransactionType, string strBranchCode)
        {
            int ItemCount = 0;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = null;
                 if (strTransactionType != "361" && strTransactionType  != "371" && strTransactionType != "461" && strTransactionType  != "471")   
                 {
                     cmd= ImpalDB.GetSqlStringCommand("select 1 as ItemCount from consignment where branch_code='" + strBranchCode+  "' and Transaction_Type_Code='" + strTransactionType + "'");
                 }
                else
                 {
                     cmd = ImpalDB.GetSqlStringCommand("select 1 as ItemCount from inward_header where branch_code='" + strBranchCode + "' and Transaction_Type_Code='" + strTransactionType + "'");
                 }
                 
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ItemCount = Convert.ToInt32(reader["ItemCount"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }

            return ItemCount;
        }

        public int GetBalanceItemCount(string strTransactionType, string strItemCode, string strBranchCode)
        {
            int ItemCount = -1;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;


                if( strTransactionType != "361" && strTransactionType  != "371" && strTransactionType !="461" && strTransactionType  != "471"  )
                {
                    sSQL="select Balance_Quantity from consignment where Branch_code = '" + strBranchCode + "' and Item_Code='" + strItemCode + "'and Transaction_Type_Code='" + strTransactionType + "' and Inward_Number not in (select Inward_Number from Stock_Diversion where Item_Code='" + strItemCode + "')";
                }
                else
                {
                    sSQL = "select Balance_Quantity from consignment where Branch_code = '" + strBranchCode + "' and Item_Code='" + strItemCode + "'and Transaction_Type_Code='" + strTransactionType + "' and Inward_Number not in (select Inward_Number from Stock_Diversion where Item_Code='" + strItemCode + "')";
                }
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ItemCount = Convert.ToInt32(reader["Balance_Quantity"].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp); 
            }
            return ItemCount;
        }


        public string InsStockDiversion(StockDiversionItems objValue, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;
            string strDiversionNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_AddStockDiversion");
                    ImpalDB.AddInParameter(cmd, "@Diversion_Number", DbType.String, "");
                    ImpalDB.AddInParameter(cmd, "@Diversion_Date", DbType.String, objValue.Diversion_date);
                    ImpalDB.AddInParameter(cmd, "@FromTransactionType", DbType.String, objValue.From_TransactionType);
                    ImpalDB.AddInParameter(cmd, "@ToTransactionType", DbType.String, objValue.To_TransactionType);
                    ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, objValue.Inward_Number);
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, objValue.Item_Code);
                    ImpalDB.AddInParameter(cmd, "@Quantity", DbType.Int32, Convert.ToInt32(objValue.Quantity));
                    ImpalDB.AddInParameter(cmd, "@Reason", DbType.String, objValue.Reason);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@SLNO", DbType.Int32, Convert.ToInt32(objValue.Serial_No));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    strDiversionNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp);
            }

            return strDiversionNumber;
        }


        public int UpdStockDiversion(StockDiversionItems objValue, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;
            int iRowCount = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_UpdStockDiversion");
					ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@DiversionNumber", DbType.String, objValue.Diversion_Number);
                    ImpalDB.AddInParameter(cmd, "@Reason", DbType.String, objValue.Reason);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp);
            }

            return iRowCount;
        }

        public int UpdStockDiversionBulk(StockDiversionItems objValue, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;
            int iRowCount = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    cmd = ImpalDB.GetStoredProcCommand("usp_UpdStockDiversion_Bulk");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@SupplierCode", DbType.String, objValue.Supplier_Line);
                    ImpalDB.AddInParameter(cmd, "@FromTransaction", DbType.String, objValue.From_TransactionType);
                    ImpalDB.AddInParameter(cmd, "@ToTransaction", DbType.String, objValue.To_TransactionType);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockDiversion), exp);
            }

            return iRowCount;
        }
    }

    public class DiversionNumber
    {
        public DiversionNumber()
        { }

        public string Diversion_Number { set; get; }
    }

    public class Quantity
    {
        public Quantity()
        { }

        public string Quantity_No { set; get; }
        //public int Balance_Quantity_No { set; get; }
        //public int Item_Count { set; get; }
    }

    public class StockDiversionItems
    {
        public StockDiversionItems()
        { }

        public string Diversion_Number { set; get; }
        public string Item_Code { set; get; }
        public string Diversion_date { set; get; }
        public string From_TransactionType { set; get; }
        public string To_TransactionType { set; get; }
        public string Supplier_Line { set; get; }
        public string Supplier_Part_Number  { set; get; }
        public string Inward_Number { set; get; }
        public int Serial_No { set; get; }
        public int Quantity { set; get; }
        public string Reason { set; get; }
        
    }
    
}
