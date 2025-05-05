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
    public class GoodsInTransitTransfer
    {
        public GoodsInTransitTransfer()
        {

        }

        public List<GoodsInTransitTransferSTDNNumber> GetTransferSTDNNumber(string strBranchCode)
        {
            List<GoodsInTransitTransferSTDNNumber> objSTDNNumbers = new List<GoodsInTransitTransferSTDNNumber>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct stdn_number, convert(nvarchar,stdn_date,103), from_branch_code, to_branch_code, lr_number, convert(nvarchar,lr_date,103) from stdn_header where SUBSTRING(stdn_number, charindex('/', stdn_number, charindex('/', stdn_number, charindex('/', stdn_number)+1))+1, 3)= '" + strBranchCode + "' and lr_number is not null order by stdn_number desc";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        GoodsInTransitTransferSTDNNumber objTrans = new GoodsInTransitTransferSTDNNumber();
                        objTrans.STDNNumber = reader["stdn_number"].ToString();
                        objTrans.STDNNumber = reader["stdn_number"].ToString();
                        objSTDNNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitTransfer), exp); 
            }
            return objSTDNNumbers;
        }

        public GoodsInTransitTransferSTDN GetSTDNHeader(string strSTDN_Number, string strBranchCode)
        {
            GoodsInTransitTransferSTDN objTrans = new GoodsInTransitTransferSTDN();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select  a.stdn_number,convert(nvarchar,stdn_date,103) as stdn_date, from_branch_code, to_branch_code, lr_number, convert(nvarchar,lr_date,103) as lr_date,item_code, stdn_quantity, received_quantity  from stdn_header a, stdn_detail b where a.stdn_number='" + strSTDN_Number + "' and SUBSTRING(a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number)+1))+1, 3)= '" + strBranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objTrans.STDNNumber = reader["stdn_number"].ToString();
                        objTrans.STDNDate = reader["stdn_date"].ToString();
                        objTrans.LRNumber = reader["lr_number"].ToString();
                        objTrans.LRDate = reader["lr_date"].ToString();
                        objTrans.FromBranch = reader["from_branch_code"].ToString();
                        objTrans.ToBranch = reader["to_branch_code"].ToString();
                        
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitTransfer), exp); 
            }
            return objTrans;
        }

        public List<GoodsInTransitTransferItemDetail> GetTransferItemDetails(string strSTDN_Number, string strBranchCode)
        {
            List<GoodsInTransitTransferItemDetail> objTransferItems = new List<GoodsInTransitTransferItemDetail>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select c.item_code,c.supplier_part_number, despatched_quantity, (despatched_quantity * cost_price) value from stdn_header a, stdn_detail b, item_master c, branch_item_price d where SUBSTRING(a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number)+1))+1, 3)= '" + strBranchCode + "' and a.stdn_number='" + strSTDN_Number + "' and  a.stdn_number = b.stdn_number and  (received_quantity is null or received_quantity = 0) and b.item_code=c.item_code and lr_number is not null and d.item_code = c.item_Code and d.branch_code = a.to_branch_Code";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        GoodsInTransitTransferItemDetail objTrans = new GoodsInTransitTransferItemDetail();
                        objTrans.ItemCode = reader["item_code"].ToString();
                        objTrans.SupplierPartNumber = reader["supplier_part_number"].ToString();
                        objTrans.Quantity = reader["despatched_quantity"].ToString();
                        objTrans.Value = reader["value"].ToString();
                        objTransferItems.Add(objTrans);
                    }
                }              
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitTransfer), exp); 
            }
            return objTransferItems;
        }

        public GoodsInTransitSupplierCount GetTransferItemCount(string strSTDN_Number, string strBranchCode)
        {
            GoodsInTransitSupplierCount objTrans = new GoodsInTransitSupplierCount();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL;
                sSQL = "select 1 as R_Count from stdn_header a, stdn_detail b where SUBSTRING(a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number, charindex('/', a.stdn_number)+1))+1, 3)= '" + strBranchCode + "' and a.stdn_number='" + strSTDN_Number + "'and a.stdn_number = b.stdn_number and (received_quantity is null or received_quantity = 0) and lr_number is not null";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {

                        objTrans.RowCount = Convert.ToInt32(reader["R_Count"]);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitTransfer), exp); 
            }
            return objTrans;
        }
       
    }
 

    #region This class is used GoodsInTransitSupplierItemDetail custom

    public class GoodsInTransitTransferSTDNNumber
    {
        public GoodsInTransitTransferSTDNNumber()
        {
        }
        public string STDNNumber { set; get; }
         
    }
    
   

    public class GoodsInTransitTransferSTDN
    {
        public GoodsInTransitTransferSTDN()
        {
        }
        public string STDNNumber  { set; get; }
        public string STDNDate  { set; get; }
        public string FromBranch  { set; get; }
        public string ToBranch { set; get; }
        public string LRNumber  { set; get; }
        public string LRDate  { set; get; }
    }

    public class GoodsInTransitTransferItemDetail
    {
        public GoodsInTransitTransferItemDetail()
        {
        }

        public string ItemCode { set; get; }
        public string SupplierPartNumber { set; get; }
        public string Quantity { set; get; }
        public string Value { set; get; }

    }
    #endregion

}
