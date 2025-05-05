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

    public class PartyTypeMaster
    {
        public void AddNewPartyTypeMaster(string PartyTypeCode, string PartyTypeDesc, string PartyTypeDbAccount, string PartyTypeCrAccount)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addPartyType");
            ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, PartyTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Party_Type_Description", DbType.String, PartyTypeDesc.Trim());
            ImpalDB.AddInParameter(cmd, "@Debit_Chart_of_Account", DbType.String, PartyTypeDbAccount.Trim());
            ImpalDB.AddInParameter(cmd, "@Credit_Chart_of_Account", DbType.String, PartyTypeCrAccount.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdatePartyTypeMaster(string PartyTypeCode, string PartyTypeDesc, string PartyTypeDbAccount, string PartyTypeCrAccount)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updPartyType");
            ImpalDB.AddInParameter(cmd, "@Party_Type_Code", DbType.String, PartyTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Party_Type_Description", DbType.String, PartyTypeDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<PartyType> GetAllPartyTypes()
        {
            List<PartyType> objPartyTypes = new List<PartyType>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Party_Type_Code,Party_Type_Description,Debit_Chart_of_account,Credit_Chart_of_account FROM PARTY_TYPE_MASTER";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objPartyTypes.Add(new PartyType(reader["Party_Type_Code"].ToString(), reader["Party_Type_Description"].ToString(),
                        reader["Debit_Chart_of_account"].ToString(), reader["Credit_Chart_of_account"].ToString()));
                }
            }
            return objPartyTypes;
        }


        public bool ValidatePartyTypeCode(string pPartyTypeCode)
        {
            PartyType objPartyTypes = new PartyType();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT 1 FROM PARTY_TYPE_MASTER WHERE PARTY_TYPE_CODE = '" + pPartyTypeCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() != "0")
                        return false;
                }
            }

            return true;
        }
    }

    public class PartyType
    {
        public string PartyTypeCode { get; set; }
        public string PartyTypeDesc { get; set; }
        public string PartyTypeDbAccount { get; set; }
        public string PartyTypeCrAccount { get; set; }

        public PartyType() { }

        public PartyType(string pPartyTypeCode, string pPartyTypeDesc, string pPartyTypeDbAccount, string pPartyTypeCrAccount)
        {
            PartyTypeCode = pPartyTypeCode;
            PartyTypeDesc = pPartyTypeDesc;
            PartyTypeDbAccount = pPartyTypeDbAccount;
            PartyTypeCrAccount = pPartyTypeCrAccount;
        }
    }
}
