using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary.Masters.Sales
{
    public class LineWiseSales
    {
        #region GetMonthYear
        /// <summary>
        /// Gets the Month Year data.
        /// </summary>
        /// <param name="BranchCode"></param>
        /// <returns></returns>
        public List<LineWiseSale> GetMonthYear(string BranchCode)
        {
            string sSQL = string.Empty;
            List<LineWiseSale> lstMonthYear = new List<LineWiseSale>(); ;
            lstMonthYear.Add(new LineWiseSale());
           
            Database ImpalDB = DataAccess.GetDatabase();
            if (string.IsNullOrEmpty(BranchCode)||BranchCode=="0")
                sSQL = "SELECT DISTINCT MONTH_YEAR, SUBSTRING(MONTH_YEAR,1,2) AS MONTHS, SUBSTRING(MONTH_YEAR,3,4) AS YEARS FROM LINE_WISE_SALES ORDER BY YEARS DESC, MONTHS DESC";
            else
                sSQL = "SELECT DISTINCT MONTH_YEAR, SUBSTRING(MONTH_YEAR,1,2) AS MONTHS, SUBSTRING(MONTH_YEAR,3,4) AS YEARS FROM LINE_WISE_SALES WHERE BRANCH_CODE= '"
                    + BranchCode + "' ORDER BY YEARS DESC, MONTHS DESC";

            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                    lstMonthYear.Add(new LineWiseSale(reader[0].ToString()));
                return lstMonthYear;
            } 
            
        }
        #endregion

     
    }
    public class LineWiseSale
    {
        public LineWiseSale()
        {
        }
        
        public LineWiseSale(string MonthYear)
        {
            _MonthYear = MonthYear;
        }

        private string _MonthYear;
        private string _Month;
        private string _Year;
        
        public string MonthYear
        {
            get { return _MonthYear; }
            set { _MonthYear = value; }
        }
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
    }
}
