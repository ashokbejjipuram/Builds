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


namespace IMPALLibrary.Masters.PONumber
{
    public class PONumbers
    {
        #region GetAllPONumbers


        public List<PONumber> GetPONumbers(string BranchCode)
        {
            List<PONumber> POrderNumber = new List<PONumber>();
            POrderNumber.Add(new PONumber(string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct PO_Number from STDN_Request_Header where PO_Number is not null and From_Branch_Code = '" + BranchCode + "' order by PO_number DESC";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {

                while (reader.Read())
                {
                    POrderNumber.Add(new PONumber(reader[0].ToString()));
                }
            }
            return POrderNumber;
        }
        #endregion
        // This query is used to fetch po numbers for  Cyber wh pages
        #region To getPO number based on branch
        public List<PONumber> GetPONumber(string Branch)
        {
            List<PONumber> POrderNumber = new List<PONumber>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select distinct PO_Number from STDN_Request_Header";
            if (Branch != "CRP" && Branch != string.Empty)
            {
                sSQL = sSQL + " Where From_Branch_Code = '" + Branch + "'";
                sSQL = sSQL + " and PO_Number is not null order by PO_number DESC";
            }
            POrderNumber.Add(new PONumber(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {

                while (reader.Read())
                {
                    POrderNumber.Add(new PONumber(reader[0].ToString()));
                }
            }
            return POrderNumber;
        }
        #endregion
        public List<PONumber> GetPONumberDateRange(string Branch)
        {
            List<PONumber> POrderNumber = new List<PONumber>();
            Database ImpalDB = DataAccess.GetDatabase();

            string start_Dt = string.Empty;
            string end_Dt = string.Empty;

            string strQuery1 = "select convert(varchar(10),start_Date,103),convert(varchar(10),End_Date,103) from Accounting_Period";
            strQuery1 = strQuery1 + " where convert(datetime,getdate(),103) between convert(datetime,start_date,103) and convert(datetime,end_date,103)";
            strQuery1 = strQuery1 + "order by accounting_period_code desc";
            DbCommand cmdP1 = ImpalDB.GetSqlStringCommand(strQuery1);
            cmdP1.CommandTimeout = ConnectionTimeOut.TimeOut;
            IDataReader reader = ImpalDB.ExecuteReader(cmdP1);
            while (reader.Read())
            {
                start_Dt = reader[0].ToString();
                end_Dt = reader[1].ToString();
            }

            string sSQL = "select distinct PO_Number from CCWH_Stdn_Request_Header";
            if (Branch != "CRP" && Branch != string.Empty)
            {
                sSQL = sSQL + " Where From_Branch_Code = '" + Branch + "'";
                sSQL = sSQL + " and convert(datetime,request_Date,103) between";
                sSQL = sSQL + " convert(datetime, '" + start_Dt + "',103) and convert(datetime,'" + end_Dt + "',103)";
                sSQL = sSQL + " and PO_Number is not null order by PO_number DESC";
            }
            POrderNumber.Add(new PONumber(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader POreader = ImpalDB.ExecuteReader(cmd1))
            {

                while (POreader.Read())
                {
                    POrderNumber.Add(new PONumber(POreader[0].ToString()));
                }
            }
            return POrderNumber;
        }

        public List<PONumber> GetCCWH_NoReprint(string Branch)
        {
            List<PONumber> POrderNumber = new List<PONumber>();
            Database ImpalDB = DataAccess.GetDatabase();

            string start_Dt = string.Empty;
            string end_Dt = string.Empty;

            string strQuery1 = "select convert(varchar(10),start_Date,103),convert(varchar(10),End_Date,103) from Accounting_Period";
            strQuery1 = strQuery1 + " where convert(datetime,getdate(),103) between convert(datetime,start_date,103) and convert(datetime,end_date,103)";
            strQuery1 = strQuery1 + "order by accounting_period_code desc";
            DbCommand cmdP1 = ImpalDB.GetSqlStringCommand(strQuery1);
            cmdP1.CommandTimeout = ConnectionTimeOut.TimeOut;
            IDataReader reader = ImpalDB.ExecuteReader(cmdP1);
            while (reader.Read())
            {
                start_Dt = reader[0].ToString();
                end_Dt = reader[1].ToString();
            }

            string sqlquery = string.Empty;

            if (Branch != "CRP")
            {
                sqlquery = "select distinct CCWH_No from purchase_order_header where substring(po_number,15,3) = '" + Branch + "' ";
                sqlquery = sqlquery + " and convert(datetime,ccwh_Date,103) between convert(datetime,'" + start_Dt + "',103) and convert(datetime,'" + end_Dt + "',103) and isnull(ccwh_Status,'A') <> 'I'";
                sqlquery = sqlquery + " order by ccwh_No desc";
            }
            else
            {
                sqlquery = "select distinct ccwh_No from Purchase_order_header where substring(po_number,15,3)='" + Branch + "'";
                sqlquery = sqlquery + " and convert(datetime,ccwh_Date,103) between convert(datetime,'" + start_Dt + "',103) and convert(datetime,'" + end_Dt + "',103) ";
                sqlquery = sqlquery + " order by ccwh_No desc";
            }

            POrderNumber.Add(new PONumber(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sqlquery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader POreader = ImpalDB.ExecuteReader(cmd1))
            {

                while (POreader.Read())
                {
                    POrderNumber.Add(new PONumber(POreader[0].ToString()));
                }
            }
            return POrderNumber;
        }
        //To get PO NUMBER for PurchaseOrderReprint.aspx
        #region PO NUMBER
        public List<ClsPONumber> GetPONumber(string strPOType, string strReportType, string strBranchCode)
        {
            List<ClsPONumber> POnumber = new List<ClsPONumber>();
            string sSQL = default(string);

            Database ImpalDB = DataAccess.GetDatabase();
            switch (strReportType)
            {
                case "1": //Report
                    {
                        switch (strPOType)
                        {
                            case "D":
                                {
                                    sSQL = "select PO_Number from Purchase_Order_Header where transaction_type_code <> '451' and reference_number not like '%ABCFMS%' and reference_number<>'WORKSHEET-DPO'" +
                                           "and isnull(status,'A') = 'A' and branch_code = '" + strBranchCode + "' and po_date is not null order by po_date desc";

                                    break;
                                }
                            case "I":
                                {
                                    sSQL = "select PO_Number from Purchase_Order_Header where transaction_type_code='451' and isnull(status,'A') = 'A' and branch_code = '" + strBranchCode + "' order by po_Date desc, po_number desc";

                                    break;
                                }
                            case "W":
                                {
                                    sSQL = "select PO_Number from Purchase_Order_Header where Reference_Number like '%ABCFMS%' and branch_code = '" + strBranchCode + "' and upper(isnull(status,'A')) = 'A' and isnull(PO_Value,0)>0 order by indent_Date desc";

                                    break;
                                }
                            case "N":
                                {
                                    sSQL = "select PO_Number from Purchase_Order_Header where transaction_type_code = '201' and reference_number='WORKSHEET-DPO' and isnull(status,'A') = 'A' and branch_code = '" + strBranchCode + "' order by Indent_Date desc";

                                    break;
                                }
                            case "E":
                                {
                                    sSQL = "select PO_Number from Purchase_Order_Header where transaction_type_code = '201' and (reference_number='EXTRA PURCHASE ORDER' or reference_number like 'EPO%') and isnull(status,'A') = 'A' and branch_code = '" + strBranchCode + "' order by Indent_Date desc";

                                    break;
                                }
                        }

                        break;
                    }
                case "2": //Worksheet Reprint
                    {
                        sSQL = "select PO_Number from Purchase_Order_Header where PO_Number in (select distinct(Indent_Number) from Item_Worksheet_ABCFMS union all select distinct(Indent_Number) from Item_Worksheet_ABCFMS_Nil) and isnull(status,'A') = 'A' and branch_code = '" + strBranchCode + "' order by indent_Date desc";

                        break;
                    }
            }

            POnumber.Add(new ClsPONumber(""));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    POnumber.Add(new ClsPONumber(reader[0].ToString()));
                }
            }
            return POnumber;
        }
        #endregion
    }


  public class PONumber
    {
      public PONumber(string PONumber)
      {
          _PONumber = PONumber;
      }
      private string _PONumber;

      public string PONumbers
      {
          get { return _PONumber; }
          set { _PONumber = value; }
      }
    }
  public class ClsPONumber
  {
      public ClsPONumber(string strPONumber)
      {
          PONumber = strPONumber;
      }
      public string PONumber { get; set; }
  }
}
