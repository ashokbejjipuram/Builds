using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary.Masters.Sales
{
    public class SalesMen
    {
        #region GetSalesMenNames
        /// <summary>
        /// Get the list of Sales Men based on Branch Code
        /// </summary>
        /// <returns></returns>
        public List<SalesMan> GetSalesMenNames(string BranchCode)
        {
            List<SalesMan> lstSalesMen = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT SALES_MAN_NAME ,SALES_MAN_CODE  FROM SALES_MAN_MASTER WHERE BRANCH_CODE = '"
                             + BranchCode + "' AND SALES_MAN_NAME IS NOT NULL AND SALES_MAN_NAME <> ' ' ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                if (lstSalesMen == null)
                    lstSalesMen = new List<SalesMan>();
                while (reader.Read())
                    lstSalesMen.Add(new SalesMan(reader[0].ToString(), reader[1].ToString()));
                return lstSalesMen;
            }
        }
        #endregion

        #region SendFilterDetailsToDB
        public void SendFilterDetailsToDB(string FromDate, string ToDate, string BranchName, string SalesManName)
        {
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_salesmanperformance");
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@TO_Date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchName);
                ImpalDB.AddInParameter(cmd, "@Sales_Man_Code", DbType.String, SalesManName);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        #endregion

        #region GetSalesMenNames
        /// <summary>
        /// Get the list of Sales Men 
        /// </summary>
        /// <returns></returns>
        public List<SalesMan> GetSalesMenNames()
        {
            List<SalesMan> lstSalesMen = null;
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT SALES_MAN_NAME ,SALES_MAN_CODE FROM SALES_MAN_MASTER WHERE "
                + "SALES_MAN_NAME IS NOT NULL AND SALES_MAN_NAME <> ' ' ORDER BY SALES_MAN_NAME";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                if (lstSalesMen == null)
                    lstSalesMen = new List<SalesMan>();
                while (reader.Read())
                    lstSalesMen.Add(new SalesMan(reader[0].ToString(), reader[1].ToString()));
                return lstSalesMen;
            }
        }
        #endregion
    }

    public class SalesMan
    {
        public SalesMan(string Name, string Code)
        {
            _Code = Code;
            _Name = Name;
        }
        public SalesMan()
        {

        }

        private string _Code;
        private string _Name;

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
    }
}
