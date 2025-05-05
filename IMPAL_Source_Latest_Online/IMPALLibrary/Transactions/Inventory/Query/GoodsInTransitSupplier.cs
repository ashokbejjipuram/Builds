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
    public class GoodsInTransitSupplier
    {
        public GoodsInTransitSupplier()
        {

        }
        public List<Supplier> GetSupplier(string strBranchCode)
        {
            List<Supplier> objInwardNumbers = new List<Supplier>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct a.supplier_code, b.supplier_name from inward_header a,supplier_master b where a.Branch_Code = '" + strBranchCode + "' and b.supplier_code = a.supplier_code order by Supplier_Code";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Supplier objTrans = new Supplier();
                        objTrans.SupplierCode = reader["supplier_code"].ToString();
                        objTrans.SupplierName = reader["supplier_name"].ToString();
                        objInwardNumbers.Add(objTrans);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitSupplier), exp); 
            }
            return objInwardNumbers;
        }

        public List<GoodsInTransitSupplierItemDetail> GetSupplierItemDetails(string strSupplierCode, string strBranchCode)
        {
            List<GoodsInTransitSupplierItemDetail> objSupplierItems = new List<GoodsInTransitSupplierItemDetail>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL,sSQLRowcount;
                sSQL = "select a.inward_number as inward_number ,convert(nvarchar,a.inward_date,103) as inward_date,a.lr_number as lr_number, convert(nvarchar,a.lr_date,103) as lr_date,a.place_of_despatch,a.carrier, c.item_code,c.supplier_part_number, b.document_quantity, isnull(item_price,0) as item_price";
                sSQL = sSQL + " from inward_header a, inward_detail b ,item_master c where a.Branch_Code = '" + strBranchCode + "' and a.supplier_code='" + strSupplierCode + "'and a.inward_number = b.inward_number  and b.item_code=c.item_code and isnull(a.status,'A') = 'A' and b.actual_quantity is  null";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        GoodsInTransitSupplierItemDetail objTrans = new GoodsInTransitSupplierItemDetail();
                        objTrans.InvoiceDate = reader["inward_date"].ToString();
                        objTrans.InvoiceNumber = reader["inward_number"].ToString();

                        objTrans.ItemCode = reader["item_code"].ToString();
                        objTrans.LRDate = reader["lr_date"].ToString();
                        objTrans.LRNumber = reader["lr_number"].ToString();
                        objTrans.Quantity = reader["document_quantity"].ToString();
                        objTrans.SupplierPartNumber = reader["supplier_part_number"].ToString();
                        objTrans.Value = reader["item_price"].ToString();

                        objSupplierItems.Add(objTrans);
                    }
                }

              
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransitSupplier), exp); 
            }
            return objSupplierItems;
        }

        public GoodsInTransitSupplierCount GetSupplierItemCount(string strSupplierCode, string strBranchCode)
        {
            GoodsInTransitSupplierCount objTrans = new GoodsInTransitSupplierCount();
            
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL, sSQLRowcount;
                sSQL = "select 1 as R_Count from inward_header a, inward_detail b ,item_master c where a.Branch_Code = '" + strBranchCode + "' and a.supplier_code='" + strSupplierCode + "'and a.inward_number = b.inward_number  and b.item_code=c.item_code and isnull(a.status,'A') = 'A' and b.actual_quantity is null";
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
                Log.WriteException(typeof(GoodsInTransitSupplier), exp); 
            }
              return objTrans;
        }
       
    }
 

    #region This class is used GoodsInTransitSupplierItemDetail custom
        
    
    public class GoodsInTransitSupplierItemDetail
    {
        public GoodsInTransitSupplierItemDetail()
        {
        }        
        public string InvoiceNumber { set; get; }
        public string InvoiceDate { set; get; }
        public string LRNumber { set; get; }
        public string LRDate { set; get; }
        public string ItemCode { set; get; }
        public string SupplierPartNumber { set; get; }
        public string Quantity { set; get; }
        public string Value { set; get; }

    }

    public class GoodsInTransitSupplierCount
    {
        public GoodsInTransitSupplierCount()
        {
        }
        public int RowCount { set; get; }
    }
    #endregion

}
