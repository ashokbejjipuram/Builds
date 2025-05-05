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

namespace IMPALLibrary.Masters
{
    public class Item_wise_sales
    {
        public List<Item_wise_sale> GetItemwisesales(string strBranchCode)
        {
            List<Item_wise_sale> lstmonthyear = new List<Item_wise_sale>();
            Database ImpalDB = DataAccess.GetDatabase();
            string Ssql = "select distinct(Month_year) from Item_wise_Sales where Branch_Code = '" + strBranchCode + "'";

            lstmonthyear.Add(new Item_wise_sale(string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstmonthyear.Add(new Item_wise_sale(reader[0].ToString()));
                }
            }

            return lstmonthyear;
        }

        public List<Item_wise_sale> GetMonthYear(string strBranchCode)
        {
            List<Item_wise_sale> lstMonthYear = new List<Item_wise_sale>();
            Database ImpalDB = DataAccess.GetDatabase();
            //string Ssql = "select SUBSTRING(month_year,1,2) + substring(month_year,3,4) from Line_wise_Sales where substring(month_year,3,4) <= year(GETDATE())  and substring(month_year,3,4) > '2009' and Branch_Code = '" + strBranchCode + "'";
            //Ssql += " group by SUBSTRING(month_year,1,2) , substring(month_year,3,4) order by substring(month_year,3,4) desc ,SUBSTRING(month_year,1,2) desc";

            string Ssql = "select REPLACE(RIGHT(CONVERT(VARCHAR(10), StartDate, 103), 7),'/',''),ROW_NUMBER() over (order by StartDate desc) from (select CONVERT(DATE,DATEADD(MONTH, x.number-24, a.Start_Date),103) StartDate ";
            Ssql += " from master.dbo.spt_values x inner join Accounting_Period a on GETDATE() between dateadd(MM,-24,a.Start_Date) and a.End_Date and x.type = 'P' AND x.number <= DATEDIFF(MONTH, dateadd(MM,-24,a.Start_Date), GETDATE())) b";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstMonthYear.Add(new Item_wise_sale(reader[0].ToString()));
                }
            }

            return lstMonthYear;
        }

        public List<Item_wise_sale> GetMonthYearHOMails(string strBranchCode)
        {
            List<Item_wise_sale> lstMonthYear = new List<Item_wise_sale>();
            Database ImpalDB = DataAccess.GetDatabase();
            //string Ssql = "select SUBSTRING(month_year,1,2) + substring(month_year,3,4) from Line_wise_Sales where substring(month_year,3,4) <= year(GETDATE())  and substring(month_year,3,4) > '2009' and Branch_Code = '" + strBranchCode + "'";
            //Ssql += " group by SUBSTRING(month_year,1,2) , substring(month_year,3,4) order by substring(month_year,3,4) desc ,SUBSTRING(month_year,1,2) desc";

            string Ssql = "Select REPLACE(RIGHT(CONVERT(VARCHAR(10), DATEADD(day,-1,DATEADD(MM, DATEDIFF(MM,0,GETDATE()),0)), 103), 7),'/','')";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstMonthYear.Add(new Item_wise_sale(reader[0].ToString()));
                }
            }

            return lstMonthYear;
        }

        public List<Item_wise_sale> GetMonthYearStockLedger(string strBranchCode)
        {
            List<Item_wise_sale> lstMonthYear = new List<Item_wise_sale>();
            Database ImpalDB = DataAccess.GetDatabase();
            //string Ssql = "select SUBSTRING(month_year,1,2) + substring(month_year,3,4) from Line_wise_Sales where substring(month_year,3,4) <= year(GETDATE())  and substring(month_year,3,4) > '2009' and Branch_Code = '" + strBranchCode + "'";
            //Ssql += " group by SUBSTRING(month_year,1,2) , substring(month_year,3,4) order by substring(month_year,3,4) desc ,SUBSTRING(month_year,1,2) desc";

            string Ssql = "select REPLACE(RIGHT(CONVERT(VARCHAR(10), StartDate, 103), 7),'/',''),ROW_NUMBER() over (order by StartDate desc) from ";
            Ssql += "(select CONVERT(DATE,DATEADD(MONTH, x.number-case when a.Accounting_Period_Code<=32 then 0 else 12 * (a.Accounting_Period_Code-32) end, a.Start_Date),103) StartDate ";
            Ssql += "from master.dbo.spt_values x inner join Accounting_Period a on GETDATE() between dateadd(MM,case when a.Accounting_Period_Code<=32 then 0 else -12 * (a.Accounting_Period_Code-32) end,a.Start_Date) and a.End_Date ";
            Ssql += "and x.type = 'P' AND x.number <= DATEDIFF(MONTH, dateadd(MM,case when a.Accounting_Period_Code<=32 then 0 else -12 * (a.Accounting_Period_Code-32) end,a.Start_Date), GETDATE())) b";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(Ssql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstMonthYear.Add(new Item_wise_sale(reader[0].ToString()));
                }
            }

            return lstMonthYear;
        }
    }

    public class Item_wise_sale
    {
        private string _month_year;

        public Item_wise_sale(string month_year)
        {
            _month_year = month_year;

        }

        public Item_wise_sale()
        {


        }

        public string month_year
        {
            get { return _month_year; }
            set { _month_year = value; }
        }

    }
}