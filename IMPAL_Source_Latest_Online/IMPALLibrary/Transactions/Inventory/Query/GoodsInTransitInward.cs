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

    #region This class is used GoodsInTransitInward details info in View mode functionalities
    public class GoodsInTransitInwards
     {
         public GoodsInTransitInwards()
         {         
         }

         public List<InwardNumber> GetInwardNumber(string strBranchCode)
         {
             List<InwardNumber> objInwardNumbers = new List<InwardNumber>();
             try
             {
                 Database ImpalDB = DataAccess.GetDatabase();
                 string sSQL;
                 sSQL = "select distinct a.inward_number as inward_number from inward_header a,inward_detail b where a.Branch_Code = '" + strBranchCode + "' and a.inward_number=b.inward_number and isnull(a.status,'A') = 'A' and actual_quantity is null order by inward_number desc";
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
                 Log.WriteException(typeof(GoodsInTransitInwards), exp); 
             }
             return objInwardNumbers;
         }

         public GoodsInTransitInward GetGoodsInTransitInward(string strInwardNumber, string strBranchCode)
         {
              GoodsInTransitInward objTrans = new GoodsInTransitInward();
              string strSupplierCode ="";
             try
             {
                 Database ImpalDB = DataAccess.GetDatabase();
                 string sSQL;
                 objTrans.BranchCode = "";
                 objTrans.Carrier = "";
                 objTrans.InwardNumber = "";
                 objTrans.InwardDate = "";
                 objTrans.LRDate = "";
                 objTrans.LRNumber = "";
                 objTrans.PlaceOfDespatch = "";
                 objTrans.Supplier = "";

                 sSQL = "select   a.inward_number,convert(nvarchar,inward_date,103) as inward_date, supplier_code, branch_code,convert(nvarchar,lr_date,103) as lr_date,lr_number,place_of_despatch, carrier from inward_header a, inward_detail b where a.Branch_Code = '" + strBranchCode + "' and a.inward_number = b.inward_number and a.inward_number='" + strInwardNumber + "'";
                 DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                 cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                 using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                 {
                     while (reader.Read())
                     {

                         objTrans.BranchCode = reader["branch_code"].ToString();
                         objTrans.Carrier = reader["carrier"].ToString();
                         objTrans.InwardNumber = reader["inward_number"].ToString();
                         objTrans.InwardDate = reader["inward_date"].ToString();
                         objTrans.LRDate = reader["lr_date"].ToString();
                         objTrans.LRNumber = reader["lr_number"].ToString();
                         objTrans.PlaceOfDespatch = reader["place_of_despatch"].ToString();
                         strSupplierCode = reader["supplier_code"].ToString(); 
                         
                     }
                 }

                 sSQL = "Select supplier_name from supplier_master where supplier_code ='" + strSupplierCode + "'";
                 DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                 cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                 using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                 {
                     while (reader.Read())
                     {
                         objTrans.Supplier = reader["supplier_name"].ToString();
                     }
                 }

                  
             }
             catch (Exception exp)
             {
                 Log.WriteException(typeof(GoodsInTransitInwards), exp); 
             }
             return objTrans;
         }

         public List<GoodsInTransitInwardItemDetail> GetGoodsInTransitInwardItemDetails(string strInwardNumber, string strBranchCode)
         {
             List<GoodsInTransitInwardItemDetail> objGetItemCode = new List<GoodsInTransitInwardItemDetail>();
             try
             {
                 Database ImpalDB = DataAccess.GetDatabase();
                 string sSQL = "select c.item_code,c.supplier_part_number, b.document_quantity, isnull(item_price,0) as item_price from inward_header a, inward_detail b ,item_master c where a.Branch_Code = '" + strBranchCode + "' and a.inward_number='" + strInwardNumber + "'and a.inward_number = b.inward_number  and b.item_code=c.item_code and b.actual_quantity is  null";
                 DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                 cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                 using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                 {
                     while (reader.Read())
                     {
                         GoodsInTransitInwardItemDetail objTrans = new GoodsInTransitInwardItemDetail();
                         objTrans.ItemCode = reader["item_code"].ToString();
                         objTrans.Quantity = reader["document_quantity"].ToString();
                         objTrans.SupplierPartNumber = reader["supplier_part_number"].ToString();
                         objTrans.ItemValue = reader["item_price"].ToString();
                         
                         objGetItemCode.Add(objTrans);
                     }
                 }
             }
             catch (Exception exp)
             {
                 Log.WriteException(typeof(GoodsInTransitInwards), exp); 
             }
             return objGetItemCode;
         }

     }


    #endregion


    #region This class is used GoodsInTransitInward custom
    public class InwardNumber
    {
        public InwardNumber()
        {
        }

        public string Inward_Number { set; get; }
        
    }
    #endregion


    #region This class is used GoodsInTransitInward custom
    public class GoodsInTransitInward
    {
        public GoodsInTransitInward(){
        }
 
        public string InwardNumber  { set; get; }
        public string InwardDate { set; get; }
        public string Supplier { set; get; }
        public string BranchCode  { set; get; }
        public string LRNumber  { set; get; }
        public string LRDate  { set; get; }

        public string PlaceOfDespatch  { set; get; }
        public string Carrier { set; get; }
    }
    #endregion

    #region This class is used GoodsInTransitInward custom
    public class GoodsInTransitInwardItemDetail
    {
        public GoodsInTransitInwardItemDetail()
        {
        }

        public string ItemCode  { set; get; }
        public string SupplierPartNumber  { set; get; }
        public string Quantity { set; get; }
        public string ItemValue { set; get; }
        
    }
    #endregion

}
