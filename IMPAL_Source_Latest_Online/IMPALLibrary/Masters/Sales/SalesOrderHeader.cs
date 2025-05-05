using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary.Masters.Sales
{
  public  class SalesOrderHeaders
    {
      public List<SalesOrderHeader> GetSalesOrder(string BranchCode)
      {
          string Ssql = string.Empty;
          List<SalesOrderHeader> lstSalesOrder = new List<SalesOrderHeader>();
          Database ImpalDB = DataAccess.GetDatabase();
          lstSalesOrder.Add(new SalesOrderHeader("0", string.Empty, string.Empty));
          if (BranchCode == "CRP")
          {
              Ssql = "select Customer_code, Customer_Name + ' | ' + Location, Status from Customer_Master WITH (NOLOCK) order by b.Customer_Name";
          }
          else
          {
              Ssql = "select Customer_code, Customer_Name + ' | ' + Location, Status from Customer_Master WITH (NOLOCK) Where Branch_Code = '" + BranchCode + "' order by Customer_Name";
          }

          DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
          cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
          using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
          {
              while (reader.Read())
              {
                  lstSalesOrder.Add(new SalesOrderHeader(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
              }

          }


          return lstSalesOrder;
      }
    }
  public  class SalesOrderHeader
    { 
      private string _customer_code;
      private string _customer_name;
      private string _status;

      public SalesOrderHeader(string customer_code, string customer_name, string status)
        {
            _customer_code = customer_code;
            _customer_name = customer_name;
            _status = status;
        }

        public SalesOrderHeader()
        {


        }

        public string customer_code
        {
            get { return _customer_code; }
            set { _customer_code = value; }
        }
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

    }
}
