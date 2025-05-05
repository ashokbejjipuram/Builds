using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALLibrary.Masters.VendorDetails
{
    public class VendorDetails
    {
        public List<VendorDtls> GetVendors(string BranchCode, string QueryType)
        {
            List<VendorDtls> lstVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            switch (QueryType)
            {
                case "SalesOrder":
                    sQuery = "SELECT DISTINCT SALES.Vendor_Code, CUST.Vendor_Name, CUST.STATUS " +
                        "FROM SALES_ORDER_HEADER SALES,Vendor_Master CUST " +
                        "WHERE SALES.Vendor_Code = CUST.Vendor_Code " +
                        "ORDER BY CUST.Vendor_Name";
                    break;
                case "Corporate":
                    sQuery = "SELECT Vendor_Code, Vendor_Name, STATUS FROM Vendor_Master  ORDER BY Vendor_Name";
                    break;
                case "StatementOfAccount":
                    sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                                 + BranchCode + "' and status='A' ORDER BY Vendor_Code";
                    break;
                default:
                    sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                                 + BranchCode + "' ORDER BY Vendor_Name"; // AND Vendor_Name LIKE '" + AutoCompleteName + "%'
                    break;
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstVendor == null)
                    lstVendor = new List<VendorDtls>();
                    lstVendor.Add(new VendorDtls("0", string.Empty));
                while (reader.Read())
                    lstVendor.Add(new VendorDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstVendor;
            }
        }

        public List<VendorDtls> GetVendorsByTown(string BranchCode, int TownCode, string QueryType)
        {
            List<VendorDtls> lstVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            if (TownCode!=0)
                    sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                         + BranchCode + "' and Town_Code=" + TownCode + " ORDER BY Vendor_Name";
            else
                    sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                         + BranchCode + "' ORDER BY Vendor_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstVendor == null)
                    lstVendor = new List<VendorDtls>();
                lstVendor.Add(new VendorDtls("0", string.Empty));
                while (reader.Read())
                    lstVendor.Add(new VendorDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstVendor;
            }
        }

        public List<VendorDtls> GetVendorDetails(string BranchCode, string AutoCompleteName)
        {
            List<VendorDtls> lstVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            //if (string.IsNullOrEmpty(BranchCode))//For Corporate User
            //    sQuery = "SELECT DISTINCT TOWN_CODE, TOWN_NAME FROM TOWN_MASTER ORDER BY TOWN_NAME";
            //else
            if (AutoCompleteName != "Dummy")
                sQuery = "SELECT Vendor_Code, Vendor_Name, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                             + BranchCode + "' AND Vendor_Name LIKE '" + AutoCompleteName + "%' ORDER BY Vendor_Name";
            else
                sQuery = "SELECT Vendor_Code, Vendor_Name, STATUS FROM Vendor_Master WHERE BRANCH_CODE = '"
                         + BranchCode + "' ORDER BY Vendor_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstVendor == null)
                    lstVendor = new List<VendorDtls>();
                while (reader.Read())
                    lstVendor.Add(new VendorDtls(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                return lstVendor;
            }
        }

        public VendorDtls GetDetails(string Code)
        {
            VendorDtls oVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT STATUS, ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, LOCATION FROM Vendor_Master WHERE Vendor_Code = '" + Code + "'";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (oVendor == null)
                    oVendor = new VendorDtls();
                while (reader.Read())
                {
                    oVendor.Status = reader[0].ToString();
                    oVendor.Address1 = reader[1].ToString();
                    oVendor.Address2 = reader[2].ToString();
                    oVendor.Address3 = reader[3].ToString();
                    oVendor.Address4 = reader[4].ToString();
                    oVendor.Location = reader[5].ToString();
                }
                return oVendor;
            }
        }

        public List<VendorDtls> GetVendorswithLocation(string strBranchCode)
        {
            List<VendorDtls> lstVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION AS 'Vendor_Name' FROM Vendor_Master Where Branch_Code='" + strBranchCode + "' and ISNULL(Status,'A') in ('A','I') ORDER BY Vendor_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstVendor == null)
                    lstVendor = new List<VendorDtls>();
                while (reader.Read())
                    lstVendor.Add(new VendorDtls(reader[0].ToString(), reader[1].ToString()));
                return lstVendor;
            }
        }


        public List<VendorDtls> GetAllVendors(string strBranchCode)
        {
            List<VendorDtls> lstVendor = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;

            sQuery = "SELECT Vendor_Code, Vendor_Name + ' | ' + LOCATION AS 'Vendor_Name' FROM Vendor_Master Where Branch_Code='" + strBranchCode + "' ORDER BY Vendor_Name";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                if (lstVendor == null)
                    lstVendor = new List<VendorDtls>();
                while (reader.Read())
                    lstVendor.Add(new VendorDtls(reader[0].ToString(), reader[1].ToString()));
                return lstVendor;
            }
        }

        public string AddNewVendor(VendorFields CustDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string VendorCode = "";

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddVendor");
            ImpalDB.AddInParameter(cmd, "@Vendor_Name", DbType.String, CustDetails.Vendor_Name);
            ImpalDB.AddInParameter(cmd, "@Chart_of_Account_Code", DbType.String, CustDetails.Chart_of_Account_Code);
            ImpalDB.AddInParameter(cmd, "@Address1", DbType.String, CustDetails.Address1);
            ImpalDB.AddInParameter(cmd, "@Address2", DbType.String, CustDetails.Address2);
            ImpalDB.AddInParameter(cmd, "@Address3", DbType.String, CustDetails.Address3);
            ImpalDB.AddInParameter(cmd, "@Address4", DbType.String, CustDetails.Address4);
            ImpalDB.AddInParameter(cmd, "@Location", DbType.String, CustDetails.location);
            ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, CustDetails.Pincode);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, CustDetails.Branch_Code);
            ImpalDB.AddInParameter(cmd, "@Vendor_Alpha_Code", DbType.String, CustDetails.Vendor_Alpha_Code);
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, CustDetails.Phone);
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, CustDetails.Email);
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, CustDetails.Contact_Person);
            ImpalDB.AddInParameter(cmd, "@GSTIN", DbType.String, CustDetails.GSTIN);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_name", DbType.String, CustDetails.Vendor_bank_name);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_branch", DbType.String, CustDetails.Vendor_bank_branch);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_address", DbType.String, CustDetails.Vendor_bank_address);
            ImpalDB.AddInParameter(cmd, "@status", DbType.String, CustDetails.status);

            DataSet ds = new DataSet();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                VendorCode = ds.Tables[0].Rows[0][0].ToString();
            }

            return VendorCode;        
        }       

        public void UpdateVendor(VendorFields CustDetails)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdVendor");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, CustDetails.Branch_Code);
            ImpalDB.AddInParameter(cmd, "@Vendor_Code", DbType.String, CustDetails.Vendor_Code);
            ImpalDB.AddInParameter(cmd, "@Vendor_Name", DbType.String, CustDetails.Vendor_Name);
            ImpalDB.AddInParameter(cmd, "@Address1", DbType.String, CustDetails.Address1);
            ImpalDB.AddInParameter(cmd, "@Address2", DbType.String, CustDetails.Address2);
            ImpalDB.AddInParameter(cmd, "@Address3", DbType.String, CustDetails.Address3);
            ImpalDB.AddInParameter(cmd, "@Address4", DbType.String, CustDetails.Address4);
            ImpalDB.AddInParameter(cmd, "@Location", DbType.String, CustDetails.location);
            ImpalDB.AddInParameter(cmd, "@Pincode", DbType.String, CustDetails.Pincode);
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, CustDetails.Phone);
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, CustDetails.Email);
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, CustDetails.Contact_Person);
            ImpalDB.AddInParameter(cmd, "@GSTIN", DbType.String, CustDetails.GSTIN);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_name", DbType.String, CustDetails.Vendor_bank_name);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_branch", DbType.String, CustDetails.Vendor_bank_branch);
            ImpalDB.AddInParameter(cmd, "@Vendor_bank_address", DbType.String, CustDetails.Vendor_bank_address);
            ImpalDB.AddInParameter(cmd, "@status", DbType.String, CustDetails.status);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<VendorFields> ViewVendor(string BranchCode ,string VendorCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetVendor");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Vendor_Code", DbType.String, VendorCode.Trim());
            List<VendorFields> custDetails = new List<VendorFields>();
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader objReader = ImpalDB.ExecuteReader(cmd))
            {
                while (objReader.Read())
                {
                    VendorFields cust = new VendorFields();
                    cust.Vendor_Code = objReader["Vendor_Code"].ToString();
                    cust.Vendor_Name = objReader["Vendor_Name"].ToString();
                    cust.Chart_of_Account_Code = objReader["Chart_of_Account_Code"].ToString();
                    cust.Pincode = objReader["Pincode"].ToString();
                    cust.Branch_Code = objReader["Branch_Code"].ToString();
                    cust.Vendor_Alpha_Code = objReader["Vendor_Alpha_Code"].ToString();
                    cust.Phone = objReader["Phone"].ToString();
                    cust.Email = objReader["Email"].ToString();
                    cust.Contact_Person = objReader["Contact_Person"].ToString();
                    cust.GSTIN = objReader["GSTIN"].ToString();
                    cust.Vendor_bank_name = objReader["Vendor_bank_name"].ToString();
                    cust.Vendor_bank_branch = objReader["Vendor_bank_branch"].ToString();
                    cust.Vendor_bank_address = objReader["Vendor_bank_address"].ToString();
                    cust.status = objReader["status"].ToString();
                    cust.Branch_Name = objReader["Branch_Name"].ToString();
                    cust.Address1 = objReader["address1"].ToString();
                    cust.Address2 = objReader["address2"].ToString();
                    cust.Address3 = objReader["address3"].ToString();
                    cust.Address4 = objReader["address4"].ToString();
                    cust.location = objReader["location"].ToString();

                    custDetails.Add(cust);
                }
            }

            return custDetails;
        }
    }
    public class VendorDtls
    {
        public VendorDtls()
        {
        }

        private string _Code;
        private string _Name;
        private string _Status;
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private string _Address4;
        private string _Location;


        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public string Address4
        {
            get { return _Address4; }
            set { _Address4 = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public VendorDtls(string Code, string Name, string Status)
        {
            _Code = Code;
            _Name = Name;
            _Status = Status;
        }
        public VendorDtls(string Code, string Name)
        {
            _Code = Code;
            _Name = Name;
        }
    }

    public class VendorFields
    {
        public VendorFields()
        {

        }

        private string _Vendor_Code;
        private string _Vendor_Name;
        private string _Chart_of_Account_Code;
        private string _Address;
        private string _Pincode;
        private string _Branch_Code;
        private string _Town_Code;
        private string _Vendor_Alpha_Code;
        private string _Phone;
        private string _Email;
        private string _Contact_Person;
        private string _GSTIN;
        private string _Vendor_bank_name;
        private string _Vendor_bank_branch;
        private string _Vendor_bank_address;
        private string _status;
        private string _Branch_Name;
        private string _Town_Name;
        private string _address1;
        private string _address2;
        private string _address3;
        private string _address4;
        private string _location;

        public string Vendor_Code
        {
            get { return _Vendor_Code; }
            set { _Vendor_Code = value; }
        }

        public string Vendor_Name
        {
            get { return _Vendor_Name; }
            set { _Vendor_Name = value; }
        }

        public string Chart_of_Account_Code
        {
            get { return _Chart_of_Account_Code; }
            set { _Chart_of_Account_Code = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }

        public string Branch_Code
        {
            get { return _Branch_Code; }
            set { _Branch_Code = value; }
        }

        public string Town_Code
        {
            get { return _Town_Code; }
            set { _Town_Code = value; }
        }

        public string Vendor_Alpha_Code
        {
            get { return _Vendor_Alpha_Code; }
            set { _Vendor_Alpha_Code = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Contact_Person
        {
            get { return _Contact_Person; }
            set { _Contact_Person = value; }
        }

        public string GSTIN
        {
            get { return _GSTIN; }
            set { _GSTIN = value; }
        }

        public string Vendor_bank_name
        {
            get { return _Vendor_bank_name; }
            set { _Vendor_bank_name = value; }
        }

        public string Vendor_bank_branch
        {
            get { return _Vendor_bank_branch; }
            set { _Vendor_bank_branch = value; }
        }

        public string Vendor_bank_address
        {
            get { return _Vendor_bank_address; }
            set { _Vendor_bank_address = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Branch_Name
        {
            get { return _Branch_Name; }
            set { _Branch_Name = value; }
        }

        public string Town_Name
        {
            get { return _Town_Name; }
            set { _Town_Name = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }

        public string Address4
        {
            get { return _address4; }
            set { _address4 = value; }
        }

        public string location
        {
            get { return _location; }
            set { _location = value; }
        }
    }

    public class VendorType
    {
        public VendorType(string VendorTypeCode, string VendorTypeDesc)
        {
            _VendorTypeDesc = VendorTypeDesc;
            _VendorTypeCode = VendorTypeCode;
        }

        private string _VendorTypeDesc;
        private string _VendorTypeCode;

        public string VendorTypeDesc
        {
            get { return _VendorTypeDesc; }
            set { _VendorTypeDesc = value; }
        }
        public string VendorTypeCode
        {
            get { return _VendorTypeCode; }
            set { _VendorTypeCode = value; }
        }
    }
}
