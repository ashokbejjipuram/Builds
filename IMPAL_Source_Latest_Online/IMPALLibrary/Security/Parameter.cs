using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;

namespace IMPALLibrary
{
    public class Parameter
    {
        public ParamterInfo GetCurrentYear()
        {
            //List<ParamterInfo> lstparameterInfo = new List<ParamterInfo>();
            ParamterInfo objparamterInfo = new ParamterInfo();
            Database ImpalDB = DataAccess.GetDatabase();

            string currentYear = System.DateTime.Now.Year + "-" + System.DateTime.Now.AddYears(1).Year;
            string sSQL = " Select Start_Date, End_Date from Accounting_Period ";
            sSQL = sSQL + "where description = '" + currentYear + "' and downup_status = 'D'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;            
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {   
                    objparamterInfo.FromDate = reader["Start_Date"].ToString();
                    objparamterInfo.ToDate = reader["End_Date"].ToString();
                    //lstparameterInfo.Add(objparamterInfo);
                }
            }

            return objparamterInfo;
        }

        public int AddNewParameterEntry(ref ParamterInfo parameterInfo)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddParameter");
                ImpalDB.AddInParameter(cmd, "@Start_Date", DbType.String, parameterInfo.FromDate);
                ImpalDB.AddInParameter(cmd, "@End_date", DbType.String, parameterInfo.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                result = ImpalDB.ExecuteNonQuery(cmd);

                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;

        }
    }

    public class ParamterInfo
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }



}


