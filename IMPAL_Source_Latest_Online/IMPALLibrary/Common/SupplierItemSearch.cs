using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;
namespace IMPALLibrary
{
    public class SupplierItemSearch
    {
        public SupplierItemSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<SupplierDetails> GetSupplierDetails(string SupplierLine)
        {
            List<SupplierDetails> SupplierDetailsList = new List<SupplierDetails>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;
                sSQL = "select Supplier_Part_Number, Application_Segment_code, Vehicle_Type_Code, packing_quantity, item_code from item_master where supplier_line_code ='" + SupplierLine + "' order by Supplier_Part_Number";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader sdr = ImpalDB.ExecuteReader(cmd))
                {
                    while (sdr.Read())
                    {

                        SupplierDetailsList.Add(new SupplierDetails(sdr["Supplier_Part_Number"].ToString(), sdr["Application_Segment_code"].ToString(), sdr["Vehicle_Type_Code"].ToString(), sdr["packing_quantity"].ToString(), sdr["item_code"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return SupplierDetailsList;
        }


        public List<SupplierDetails>GetSupplierDetails(string SupplierLine,string Mode)
        {
            List<SupplierDetails> SupplierDetailsList = new List<SupplierDetails>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;
                if (Mode == "1")
                    sSQL = "select Supplier_Part_Number, Application_Segment_code, Vehicle_Type_Code, packing_quantity, item_code from item_master WITH (NOLOCK) where supplier_line_code ='" + SupplierLine + "' order by Supplier_Part_Number";
                else
                    sSQL = "select Supplier_Part_Number, Application_Segment_code, Vehicle_Type_Code, packing_quantity, item_code from item_master WITH (NOLOCK) where SUBSTRING(supplier_line_code,1,3) ='" + SupplierLine + "' order by Supplier_Part_Number";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader sdr = ImpalDB.ExecuteReader(cmd))
                {
                    while (sdr.Read())
                    {

                        SupplierDetailsList.Add(new SupplierDetails(sdr["Supplier_Part_Number"].ToString(), sdr["Application_Segment_code"].ToString(), sdr["Vehicle_Type_Code"].ToString(), sdr["packing_quantity"].ToString(), sdr["item_code"].ToString()));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return SupplierDetailsList;
        }

        public List<SupplierDetails> GetSupplierDetailsSurplus(string SupplierLine, string Mode, string BranchCode)
        {
            List<SupplierDetails> SupplierDetailsList = new List<SupplierDetails>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = string.Empty;

                sSQL = "select distinct Supplier_Part_Number from Branches_Surplus";
                if (SupplierLine != "0" && BranchCode == "")
                    sSQL = sSQL + " Where Supplier_Code ='" + SupplierLine + "'";
                else if (SupplierLine == "0" && BranchCode != "")
                    sSQL = sSQL + " Where Branch_Code='" + BranchCode + "'";
                else if (SupplierLine != "0" && BranchCode != "")
                    sSQL = sSQL + " Where Supplier_Code ='" + SupplierLine + "' and Branch_Code='" + BranchCode + "'";

                sSQL = sSQL + " order by Supplier_Part_Number";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader sdr = ImpalDB.ExecuteReader(cmd))
                {
                    while (sdr.Read())
                    {

                        SupplierDetailsList.Add(new SupplierDetails(sdr["Supplier_Part_Number"].ToString(), "", "", "", ""));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return SupplierDetailsList;
        }

    }


    public class SupplierDetails
    {
        public SupplierDetails(string Supplier_Part_Number, string Application_Segment_code, string Vehicle_Type_Code, string packing_quantity, string item_code)
        {
            _Supplier_Part_Number = Supplier_Part_Number;
            _Application_Segment_code = Application_Segment_code;
            _Vehicle_Type_Code = Vehicle_Type_Code;
            _packing_quantity = packing_quantity;
            _item_code = item_code;

        }
        private string _Supplier_Part_Number;
        private string _Application_Segment_code;
        private string _Vehicle_Type_Code;
        private string _packing_quantity;
        private string _item_code;

        public string Supplier_Part_Number
        {
            get { return _Supplier_Part_Number; }
            set { _Supplier_Part_Number = value; }
        }

        public string Application_Segment_code
        {
            get { return _Application_Segment_code; }
            set { _Application_Segment_code = value; }
        }
        public string Vehicle_Type_Code
        {
            get { return _Vehicle_Type_Code; }
            set { _Vehicle_Type_Code = value; }
        }

        public string packing_quantity
        {
            get { return _packing_quantity; }
            set { _packing_quantity = value; }
        }

        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }

    }


}
