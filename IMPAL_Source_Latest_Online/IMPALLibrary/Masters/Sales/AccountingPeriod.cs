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

namespace IMPALLibrary.Masters.Sales
{
    public class AccountingPeriods
    {
        public List<AccountingPeriod> GetAccountingPeriod(int PeriodCode, string QueryType, string StrBranchCode)
        {
            List<AccountingPeriod> lstAccPeriod = new List<AccountingPeriod>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = null;
            switch (QueryType)
            {
                case "WithLine":
                    sQuery = "SELECT DISTINCT TOP 3 ACC.ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ACC, LINE_WISE_SALES LINE " +
                        "WHERE ACC.ACCOUNTING_PERIOD_CODE = LINE.ACCOUNTING_PERIOD_CODE AND BRANCH_CODE = '" + StrBranchCode + "' ORDER BY ACC.ACCOUNTING_PERIOD_CODE DESC";
                    break;
                case "BranchBanking":
                    sQuery = "SELECT DISTINCT TOP 3 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY DESCRIPTION DESC";
                    break;
                case "GLHOADM":
                    sQuery = "SELECT DISTINCT ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD WHERE ACCOUNTING_PERIOD_CODE>=29 ORDER BY DESCRIPTION DESC";
                    break;                
                case "GLHO":
                    sQuery = "SELECT DISTINCT TOP 3 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY DESCRIPTION DESC";
                    break;
                case "GLAUDIT":
                    sQuery = "SELECT DISTINCT TOP 3 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY DESCRIPTION DESC";
                    break;
                case "EXTERNALAUDIT":
                    sQuery = "SELECT TOP 1 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY DESCRIPTION DESC";
                    break;
                case "EXTERNALAUDITPREV":
                    sQuery = "SELECT TOP 2 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY DESCRIPTION DESC";
                    break;
                default:
                    sQuery = "SELECT DISTINCT TOP 3 ACCOUNTING_PERIOD_CODE, DESCRIPTION FROM ACCOUNTING_PERIOD ORDER BY ACCOUNTING_PERIOD_CODE DESC";
                    break;
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstAccPeriod.Add(new AccountingPeriod(reader[0].ToString(), reader[1].ToString()));
            }
            return lstAccPeriod;
        }

        #region GetMonth
        public List<AccountingPeriod> GetMonth()
        {
            List<AccountingPeriod> lstaccountingperiod = new List<AccountingPeriod>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSql = "select accounting_period_code, description from accounting_period";

            lstaccountingperiod.Add(new AccountingPeriod("-1", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstaccountingperiod.Add(new AccountingPeriod(reader[0].ToString(), reader[1].ToString()));
            }
            return lstaccountingperiod;
        }
        #endregion

        #region GetQuarterDates
        public List<AccountingPeriod> GetQuarterDates(int AccPeriodCode)
        {
            List<AccountingPeriod> lstquarterdates = new List<AccountingPeriod>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sSql = "with calendar as (select start_date,qtrsback=1 from Accounting_Period where Accounting_Period_Code=" + AccPeriodCode +
                          " union all select DATEADD(mm,3,start_date),qtrsback+1 from calendar where qtrsback<4) select CONVERT(varchar(10),start_date,103) from calendar";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstquarterdates.Add(new AccountingPeriod(reader[0].ToString(), reader[0].ToString()));
            }
            return lstquarterdates;
        }

        public string GetQuarterEndDate(string StartDate)
        {   
            string lstquarterenddate = string.Empty;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSql = "select Convert(varchar(10),DATEADD(s,-1,DATEADD(mm,DATEDIFF(m,0,convert(date,'" + StartDate + "',103))+3,0)),103)";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstquarterenddate = reader[0].ToString();
            }
            return lstquarterenddate;
        }
        
        #endregion
    }
    public class AccountingPeriod
    {
        private string _AccPeriodCode;
        private string _Desc;
        public AccountingPeriod(string AccPeriodCode, string Desc)
        {
            _AccPeriodCode = AccPeriodCode;
            _Desc = Desc;
        }

        public AccountingPeriod()
        {
        }

        public string AccPeriodCode
        {
            get { return _AccPeriodCode; }
            set { _AccPeriodCode = value; }
        }

        public string Desc
        {
            get { return _Desc; }
            set { _Desc = value; }
        }


    }
}
