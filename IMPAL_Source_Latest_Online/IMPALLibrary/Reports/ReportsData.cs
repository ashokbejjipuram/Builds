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
    public class ReportsData
    {
        #region GetDatatable
        public DataTable GetTableData(string query)
        { 
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(query);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet dataSet = ImpalDB.ExecuteDataSet(cmd1); 
            return dataSet.Tables[0];
        }

        public void UpdRptExecCount(string strBranchCode, string strUserid, string strReportName)
        {
            if (strReportName != null)
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetSqlStringCommand("Insert into Rpt_ExecCount_Daily values ('" + strBranchCode + "', '" + strUserid + "', '" + strReportName + "', GETDATE())");
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
        }

        public void UpdMailSentStatus(string strBranchCode, string strCustomerCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Update outstanding_Mails Set Sent_Status='S', datestamp=GETDATE() Where Branch_Code='" + strBranchCode + "' and Customer_Code='" + strCustomerCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdSentMailsStatusOnError()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Insert into outstanding_Mails_Temp select * from outstanding WITH (NOLOCK) Where Sent_Status IS NOT NULL");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public DataTable GetTableData(string Proc, List<ProcParam> ParamList)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();

            DbCommand cmd = ImpalDB.GetStoredProcCommand(Proc);
            foreach (var param in ParamList)
            {
                ImpalDB.AddInParameter(cmd, param.name, param.dbType, param.val);
            } 
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(cmd);

            return ds.Tables[0];
        }

        #endregion
    }
}
