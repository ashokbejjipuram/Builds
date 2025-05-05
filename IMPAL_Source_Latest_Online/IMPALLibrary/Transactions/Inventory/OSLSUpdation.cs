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
    public class OSLSUpdation
    {
        public OSLSUpdation()
        { }
        
        public List<GoodsInTransitTransferSTDNNumber> GetSTDNNumber(string strBranchCode)
        {
            List<GoodsInTransitTransferSTDNNumber> objSTDNNumbers = new List<GoodsInTransitTransferSTDNNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct STDN_Number from STDN_detail Where SUBSTRING(stdn_number, charindex('/', stdn_number, charindex('/', stdn_number, charindex('/', stdn_number)+1))+1, 3)= '" + strBranchCode + "' order by stdn_number desc";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        GoodsInTransitTransferSTDNNumber objTrans = new GoodsInTransitTransferSTDNNumber();
                        objTrans.STDNNumber = reader["STDN_Number"].ToString();
                        objTrans.STDNNumber = reader["STDN_Number"].ToString();
                        objSTDNNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objSTDNNumbers;
        }

        public List<InwardNumber> GetInwardNumber(string strBranchCode)
        {
            List<InwardNumber> objInwardNumbers = new List<InwardNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct Inward_Number from inward_detail i inner join Accounting_Period a on SUBSTRING(i.Inward_Number, charindex('/', i.Inward_Number, charindex('/', i.Inward_Number, charindex('/', i.Inward_Number)+1))+1, 3)= '" + strBranchCode + "' and GETDATE() between a.Start_Date and a.End_Date and substring(i.Inward_Number,1,4)>=substring(a.description,1,4)-1 order by Inward_Number desc";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        InwardNumber objTrans = new InwardNumber();
                        objTrans.Inward_Number = reader["Inward_Number"].ToString();
                        objTrans.Inward_Number = reader["Inward_Number"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objInwardNumbers;
        }

        public List<OSLSSerialNo> GetSerialNumber(string strSTDN_number, string strBranchCode)
        {
            List<OSLSSerialNo> objInwardNumbers = new List<OSLSSerialNo>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct serial_Number from STDN_detail where SUBSTRING(STDN_number, charindex('/', STDN_number, charindex('/', STDN_number, charindex('/', STDN_number)+1))+1, 3)= '" + strBranchCode + "' and STDN_number ='" + strSTDN_number + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        OSLSSerialNo objTrans = new OSLSSerialNo();
                        objTrans.OSLS_SerialNumber = reader["serial_Number"].ToString();
                        objTrans.OSLS_SerialNumber = reader["serial_Number"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objInwardNumbers;
        }

        public List<ConsignmentItemCode> GetItemCode(string strInwardNumber, string strSTDN_number, string strSerialNo, string strBranchCode)
        {
            List<ConsignmentItemCode> objInwardNumbers = new List<ConsignmentItemCode>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                if (strSerialNo != "00")
                {
                    sSQL = "select distinct a.item_code,b.supplier_part_number from STDN_detail a,Item_master b where SUBSTRING(a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number)+1))+1, 3)= '" + strBranchCode + "' and a.item_code =b.item_code and a.STDN_number = '" + strSTDN_number + "' and a.Serial_Number = '" + strSerialNo + "'";
                }
                else
                {
                    sSQL = "select distinct a.item_code,b.supplier_part_number from inward_detail a,Item_master b where SUBSTRING(a.Inward_Number, charindex('/', a.Inward_Number, charindex('/', a.Inward_Number, charindex('/', a.Inward_Number)+1))+1, 3)= '" + strBranchCode + "' and a.item_code =b.item_code and a.inward_number ='" + strInwardNumber + "'";
                }
                DbCommand cmd3 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd3.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd3))
                {
                    while (reader.Read())
                    {
                        ConsignmentItemCode objTrans = new ConsignmentItemCode();
                        objTrans.Item_Code = reader["item_code"].ToString();
                        objTrans.Supplier_Part_Number = reader["supplier_part_number"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objInwardNumbers;
        }
        
        public ConsignmentHeaderSummary ChkItemCode(string strUpdationType, string strInwardNumber, string strSerialNo, string strItemCode, string strBranchCode)
        {
            ConsignmentHeaderSummary objRowCount = new ConsignmentHeaderSummary();
            try
            {
               
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL="";
                if (strUpdationType == "S")
                {
                    sSQL = "select 1 as TotalCount from Consignment where Branch_Code= '" + strBranchCode + "' and item_code = '" + strItemCode + "' and status = 'A' and inward_number ='" + strInwardNumber + "' and serial_number =1";
                }
                DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd2))
                {
                    while (reader.Read())
                    {
                        objRowCount.iRowCount = Convert.ToInt32(reader["TotalCount"]);
                    }
                }
                
                 
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objRowCount;
        }

        public OSLSSerialNo GetLSOS(string strUpdationType, string strInwardNumber, string strItemCode, string strSerialNo, string strBranchCode)
        {

            OSLSSerialNo objTrans = new OSLSSerialNo();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                if (strUpdationType == "G")
                {
                    sSQL = "select OS_LS_Indicator from Inward_Detail where SUBSTRING(Inward_Number, charindex('/', Inward_Number, charindex('/', Inward_Number, charindex('/', Inward_Number)+1))+1, 3)= '" + strBranchCode + "' and Inward_Number = '" + strInwardNumber + "' and Item_Code = '" + strItemCode + "'";
                }
                else
                {
                    sSQL = "select Os_Ls_Indicator from Consignment where Branch_Code = '" + strBranchCode + "' and item_code = '" + strItemCode + "' and status = 'A' and inward_number ='" + strInwardNumber + "' and serial_number =1";
                }
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        objTrans.OSLS_SerialNumber = reader["OS_LS_Indicator"].ToString();                        
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp); 
            }
            return objTrans;
        }

        public int upd_OSLSUpdation(string strUpdationType, string strInwardNumber, string strItemCode, string strOSLS, string strBranchCode)
        {

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd;
            int iRowCount = 0;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (strUpdationType == "G")
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_updosls");
                    }
                    else
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_updosls1");
                    }
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
					ImpalDB.AddInParameter(cmd, "@Inward_Number", DbType.String, strInwardNumber);
                    ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode);
                    ImpalDB.AddInParameter(cmd, "@OS_LS", DbType.String, strOSLS);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    iRowCount = ImpalDB.ExecuteNonQuery(cmd);

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OSLSUpdation), exp);
            }

            return iRowCount;
        }

       
    }

    public class OSLSSerialNo
    {
        public OSLSSerialNo() { }

        public string OSLS_SerialNumber { set; get; }
    }
}
