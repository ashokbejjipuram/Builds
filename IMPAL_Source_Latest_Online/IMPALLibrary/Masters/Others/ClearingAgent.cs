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

    public class ClearingAgentMaster
    {
        public void AddNewClearingAgentMaster(string BranchCode, string AgentCode, string AgentName, string Address, string Phone, string Fax, string EMail, string ContactPerson, string Remarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddClearingAgent");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Agent_Name", DbType.String, AgentName.Trim());
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, Address.Trim());
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, Phone.Trim());
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, Fax.Trim());
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, EMail.Trim());
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, ContactPerson.Trim());
            ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, Remarks.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateClearingAgentMaster(string BranchCode, string AgentCode, string AgentName, string Address, string Phone, string Fax, string EMail, string ContactPerson, string Remarks)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdClearingAgent");
            ImpalDB.AddInParameter(cmd, "@Agent_Code", DbType.String, AgentCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Agent_Name", DbType.String, AgentName.Trim());
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode.Trim());
            if (string.IsNullOrEmpty(Address))
                Address = "";
            ImpalDB.AddInParameter(cmd, "@Address", DbType.String, Address.Trim());
            if (string.IsNullOrEmpty(Phone))
                Phone = "";
            ImpalDB.AddInParameter(cmd, "@Phone", DbType.String, Phone.Trim());
            if (string.IsNullOrEmpty(Fax))
                Fax = "";
            ImpalDB.AddInParameter(cmd, "@Fax", DbType.String, Fax.Trim());
            if (string.IsNullOrEmpty(EMail))
                EMail = "";
            ImpalDB.AddInParameter(cmd, "@Email", DbType.String, EMail.Trim());
            if (string.IsNullOrEmpty(ContactPerson))
                ContactPerson = "";
            ImpalDB.AddInParameter(cmd, "@Contact_Person", DbType.String, ContactPerson.Trim());
            if (string.IsNullOrEmpty(Remarks))
                Remarks = "";
            ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, Remarks.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<ClearingAgent> GetAllClearingAgent(string strBranchCode)
        {
            List<ClearingAgent> objClearingAgent = new List<ClearingAgent>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "SELECT Branch_Code,Clearing_Agent_Code,Agent_Name,Address,Phone,Fax,Email,Contact_Person,Remarks FROM CLEARING_AGENT ORDER BY CLEARING_AGENT_CODE";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objClearingAgent.Add(new ClearingAgent(reader["Branch_Code"].ToString(), reader["Clearing_Agent_Code"].ToString(),
                        reader["Agent_Name"].ToString(), reader["Address"].ToString(), reader["Phone"].ToString(),
                        reader["Fax"].ToString(), reader["Email"].ToString(), reader["Contact_Person"].ToString(),
                        reader["Remarks"].ToString()));
                }
            }
            return objClearingAgent;
        }

        public List<ClearingAgentBranch> GetBranchList(string StrBranchCode)
        {
            List<ClearingAgentBranch> objBranchList = new List<ClearingAgentBranch>();

            string sSql = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER WHERE STATUS='A' ORDER BY BRANCH_NAME";
            //string sSql = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MASTER WHERE BRANCH_CODE = '" + StrBranchCode + "' ORDER BY BRANCH_CODE";

            Database ImpalDB = DataAccess.GetDatabase();

            ClearingAgentBranch BranchList = new ClearingAgentBranch();
            BranchList.BrName = "-- Select --";
            BranchList.BrCode = "0";
            objBranchList.Add(BranchList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    BranchList = new ClearingAgentBranch();
                    BranchList.BrCode = reader[0].ToString();
                    BranchList.BrName = reader[1].ToString();
                    objBranchList.Add(BranchList);
                }
            }
            return objBranchList;
        }
    }

    public class ClearingAgent
    {
        public string BranchCode { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string ContactPerson { get; set; }
        public string Remarks { get; set; }

        public ClearingAgent() { }

        public ClearingAgent(string pBranchCode,string pAgentCode, string pAgentName, string pAddress, string pPhone, string pFax, string pEMail, string pContactPerson, string pRemarks)
        {
            BranchCode = pBranchCode;
            AgentCode = pAgentCode;
            AgentName = pAgentName;
            Address = pAddress;
            Phone = pPhone;
            Fax = pFax;
            EMail = pEMail;
            ContactPerson = pContactPerson;
            Remarks = pRemarks;
        }
    }

    public class ClearingAgentBranch
    {
        public string BrCode { get; set; }
        public string BrName { get; set; }
    }
}
