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


namespace IMPALLibrary
{
    public class CustomerSlabMaster
    {
        public void AddNewCustomerSlabMaster(string StateCode, string PartyTypeCode, string SlabCode, string SupplierLineCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("");
            ImpalDB.AddInParameter(cmd, "", DbType.String, StateCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, PartyTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, SlabCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, SupplierLineCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public void UpdateCustomerSlabMaster(string StateCode, string PartyTypeCode, string SlabCode, string SupplierLineCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("");
            ImpalDB.AddInParameter(cmd, "", DbType.String, StateCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, PartyTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, SlabCode.Trim());
            ImpalDB.AddInParameter(cmd, "", DbType.String, SupplierLineCode.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        
        public List<SlabState> GetStateList()
        {
            List<SlabState> objStateList = new List<SlabState>();

            string sSql = "SELECT DISTINCT STATE_CODE,STATE_NAME FROM STATE_MASTER ORDER BY STATE_NAME";

            Database ImpalDB = DataAccess.GetDatabase();

            SlabState StateList = new SlabState();
            StateList.StateCode = "0";
            StateList.StateName = "-- Select --";
            objStateList.Add(StateList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    StateList = new SlabState();
                    StateList.StateCode = reader[0].ToString();
                    StateList.StateName = reader[1].ToString();
                    objStateList.Add(StateList);
                }
            }
            return objStateList;
        }
        public List<SlabPartyType> GetPartyTypeList()
        {
            List<SlabPartyType> objSlabPartyTypeList = new List<SlabPartyType>();

            string sSql = "SELECT DISTINCT PARTY_TYPE_CODE,PARTY_TYPE_DESCRIPTION FROM PARTY_TYPE_MASTER ORDER BY PARTY_TYPE_DESCRIPTION";

            Database ImpalDB = DataAccess.GetDatabase();

            SlabPartyType SlabPartyTypeList = new SlabPartyType();
            SlabPartyTypeList.PartyTypeCode = "0";
            SlabPartyTypeList.PartyTypeName = "-- Select --";
            objSlabPartyTypeList.Add(SlabPartyTypeList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlabPartyTypeList = new SlabPartyType();
                    SlabPartyTypeList.PartyTypeCode = reader[0].ToString();
                    SlabPartyTypeList.PartyTypeName = reader[1].ToString();
                    objSlabPartyTypeList.Add(SlabPartyTypeList);
                }
            }
            return objSlabPartyTypeList;
        }
        public List<Slab> GetSlabList()
        {
            List<Slab> objSlabList = new List<Slab>();

            string sSql = "SELECT DISTINCT SLB_CODE,SLB_DESCRIPTION FROM SLB_MASTER ORDER BY SLB_CODE";

            Database ImpalDB = DataAccess.GetDatabase();

            Slab SlabList = new Slab();
            SlabList.SlabCode = "0";
            SlabList.SlabName = "-- Select --";
            objSlabList.Add(SlabList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlabList = new Slab();
                    SlabList.SlabCode = reader[0].ToString();
                    SlabList.SlabName = reader[1].ToString();
                    objSlabList.Add(SlabList);
                }
            }
            return objSlabList;
        }
        public List<CustomerSlabSupLine> GetSupplierLineList()
        {
            List<CustomerSlabSupLine> objSupplierLineList = new List<CustomerSlabSupLine>();

            string sSql = "SELECT DISTINCT SUPPLIER_CODE,SUPPLIER_NAME + ' | ' + SUPPLIER_CODE FROM SUPPLIER_MASTER  ORDER BY SUPPLIER_NAME";

            Database ImpalDB = DataAccess.GetDatabase();

            CustomerSlabSupLine SupplierLineList = new CustomerSlabSupLine();
            SupplierLineList.SupplierLineCode = "0";
            SupplierLineList.SupplierLineName = "-- Select --";
            objSupplierLineList.Add(SupplierLineList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SupplierLineList = new CustomerSlabSupLine();
                    SupplierLineList.SupplierLineCode = reader[0].ToString();
                    SupplierLineList.SupplierLineName = reader[1].ToString();
                    objSupplierLineList.Add(SupplierLineList);
                }
            }
            return objSupplierLineList;
        }
    }

    public class CustomerSlab
    {
        public string State { get; set; }
        public string PartyType { get; set; }
        public string Slab { get; set; }
        public string SupplierLine { get; set; }


        public CustomerSlab() { }

        public CustomerSlab(string pState, string pPartyType, string pSlab, string pSupplierLine)
        {
            State = pState;
            PartyType = pPartyType;
            Slab = pSlab;
            SupplierLine = pSupplierLine;
        }
    }
    public class SlabState
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
    public class SlabPartyType
    {
        public string PartyTypeCode { get; set; }
        public string PartyTypeName { get; set; }
    }
    public class Slab
    {
        public string SlabCode { get; set; }
        public string SlabName { get; set; }
    }
    public class CustomerSlabSupLine
    {
        public string SupplierLineCode { get; set; }
        public string SupplierLineName { get; set; }
    }

}
