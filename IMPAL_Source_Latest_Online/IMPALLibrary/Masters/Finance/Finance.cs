#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common; 
#endregion

namespace IMPALLibrary.Masters
{
    #region Finance APIs
    /// <summary>
    /// Contains the Finance related APIs
    /// </summary>
    public class Finance
    {
        #region GetMonthYear     
        public List<FinanceProp> GetMonthYear(string tablename, string Branch)
        {
            List<FinanceProp> lstMonthYear = new List<FinanceProp>();
            lstMonthYear.Add(new FinanceProp());
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            switch (tablename)
            {
                case "V_PURCHASEBREAK":
                    sQuery = "SELECT DISTINCT SUBSTRING(INVOICE_DATE,4,7) MONTHYEAR, SUBSTRING(INVOICE_DATE,4,2) MON, " +
                                   "SUBSTRING(INVOICE_DATE,7,4) YR FROM V_PURCHASEBREAK ORDER BY YR DESC, MON DESC";
                    break;
                case "month_year":
                    sQuery = "select distinct(Month_year),SUBSTRING(MONTH_YEAR,1,2) AS MONTHS, SUBSTRING(MONTH_YEAR,3,4) AS YEARS from Line_wise_sales ORDER BY YEARS DESC, MONTHS DESC";
                    break;
                case "sales_order_header":
                        sQuery =  "select distinct SUBSTRING(convert(nvarchar,document_date,103),4,7) as monthyear,";
                        sQuery=sQuery+"SUBSTRING(convert(nvarchar,document_date,103),4,2) as month,SUBSTRING(convert(nvarchar,document_date,103),7,4) as year";
                        sQuery = sQuery + " from sales_order_header where substring(document_number,16,3) = '" + Branch + "' order by year desc,month desc";
                     break;
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                    lstMonthYear.Add(new FinanceProp(reader[0].ToString()));
            }
            return lstMonthYear;
        }
        #endregion
        
        public void SalesTaxSummary(string BranchCode, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_ST_SUMMARY");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void VATSalesDetails(string BranchCode, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_VatSales_Detail");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void VATSalesDetailsCN(string BranchCode, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_VatSales_Detail_CN");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void LocalStatePOTax(string FromDate, string ToDate, string BranchCode, string Indicator)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_purchase_Vat31");
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);                
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void LocalStateInward(string FromDate, string ToDate, string BranchCode, string Indicator)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_Stdn_Vat31");
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region UpdateCSTActDetails
        
        public void UpdateCSTActDetails(string BranchCode, string FromDate, string ToDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_ADDCST_ACT");
                ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@To_date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@brcode", DbType.String, BranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region UpdatePendingPos
        
        public void UpdatePendingPos(string AccountingPeriod, string NumOfYears, string BranchName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                // Create command to execute the stored procedure and add the parameters.
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_PENDING");
                ImpalDB.AddInParameter(cmd, "@Accounting_period", DbType.Int16, AccountingPeriod);
                ImpalDB.AddInParameter(cmd, "@num_years", DbType.Int16, NumOfYears);
                ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, BranchName);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region UpdatePurchaseReport
        
        public void UpdatePurchaseReport(string FromDate, string ToDate, string BranchName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_PURCHASE_REPORT");
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, FromDate);
                ImpalDB.AddInParameter(cmd, "@End_Date", DbType.String, ToDate);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchName);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        public ParamterInfo GetAccountPeriodDates(string AccountCode)
        {
            ParamterInfo objparamterInfo = new ParamterInfo();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Convert(varchar(10), start_date, 103), Convert(varchar(10), end_date, 103) from accounting_period where accounting_period_code ='" + AccountCode + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objparamterInfo.FromDate = reader[0].ToString();
                    objparamterInfo.ToDate = reader[1].ToString();
                }
            }

            return objparamterInfo;
        }

        public ParamterInfo GetAccountPeriodDatesReconHO(string Date)
        {
            ParamterInfo objparamterInfo = new ParamterInfo();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Convert(varchar(10), start_date, 103), Convert(varchar(10), end_date, 103) from accounting_period where convert(date,'" + Date + "',103) between Start_Date and End_Date");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objparamterInfo.FromDate = reader[0].ToString();
                    objparamterInfo.ToDate = reader[1].ToString();
                }
            }

            return objparamterInfo;
        }

        public ParamterInfo GetCurrentAccountPeriodDates()
        {
            ParamterInfo objparamterInfo = new ParamterInfo();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Convert(varchar(10), start_date, 103), Convert(varchar(10), end_date, 103) from accounting_period where GETDATE() between Start_Date and End_Date");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objparamterInfo.FromDate = reader[0].ToString();
                    objparamterInfo.ToDate = reader[1].ToString();
                }
            }

            return objparamterInfo;
        }

        #region GetGLClassification
        public List<FinanceProp> GetGLClassification()
        {
            List<FinanceProp> lstClassification = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstClassification = new List<FinanceProp>();
                lstClassification.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_CLASSIFICATION_CODE, B.GL_CLASSIFICATION_DESCRIPTION " +
                                "FROM CHART_OF_ACCOUNT A, GL_CLASSIFICATION B " +
                                "WHERE A.GL_CLASSIFICATION_CODE = B.GL_CLASSIFICATION_CODE";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.ClassificationCode = reader[0].ToString();
                        oProp.ClassificationDesc = reader[1].ToString();
                        lstClassification.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstClassification;
        }

        public List<FinanceProp> GetGLClassificationHO()
        {
            List<FinanceProp> lstClassification = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstClassification = new List<FinanceProp>();
                lstClassification.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_CLASSIFICATION_CODE, B.GL_CLASSIFICATION_DESCRIPTION " +
                                "FROM CHART_OF_ACCOUNT A, GL_CLASSIFICATION B " +
                                "WHERE A.GL_CLASSIFICATION_CODE = B.GL_CLASSIFICATION_CODE";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.ClassificationCode = reader[0].ToString();
                        oProp.ClassificationDesc = reader[1].ToString();
                        lstClassification.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstClassification;
        }
        #endregion

        #region GetGLGroup
        public List<FinanceProp> GetGLGroup(string ClassificationCode, string BranchCode)
        {
            List<FinanceProp> lstGroup = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstGroup = new List<FinanceProp>();
                lstGroup.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_GROUP_CODE, B.GL_GROUP_DESCRIPTION " +
                                "FROM CHART_OF_ACCOUNT A, GL_GROUP B " +
                                "WHERE A.GL_GROUP_CODE = B.GL_GROUP_CODE " +
                                "AND A.GL_CLASSIFICATION_CODE = '" + ClassificationCode + "' AND A.BRANCH_CODE = '" + BranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.GroupCode = reader[0].ToString();
                        oProp.GroupDesc = reader[1].ToString();
                        lstGroup.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstGroup;
        }

        public List<FinanceProp> GetGLGroupHO(string ClassificationCode, string BranchCode)
        {
            List<FinanceProp> lstGroup = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstGroup = new List<FinanceProp>();
                lstGroup.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_GROUP_CODE, B.GL_GROUP_DESCRIPTION FROM CHART_OF_ACCOUNT A, GL_GROUP B " +
                                "WHERE A.GL_GROUP_CODE = B.GL_GROUP_CODE AND A.GL_CLASSIFICATION_CODE = '" + ClassificationCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.GroupCode = reader[0].ToString();
                        oProp.GroupDesc = reader[1].ToString();
                        lstGroup.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstGroup;
        }
        #endregion

        #region GetGLMain
        public List<FinanceProp> GetGLMain(string ClassificationCode, string GroupCode, string BranchCode)
        {
            List<FinanceProp> lstMain = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstMain = new List<FinanceProp>();
                lstMain.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_MAIN_CODE, B.GL_MAIN_DESCRIPTION " +
                                "FROM CHART_OF_ACCOUNT A, GL_MASTER B " +
                                "WHERE A.GL_MAIN_CODE = B.GL_MAIN_CODE " +
                                "AND A.GL_CLASSIFICATION_CODE ='" + ClassificationCode + "' AND A.GL_GROUP_CODE='" + GroupCode + "' AND A.BRANCH_CODE = '" + BranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.MainCode = reader[0].ToString();
                        oProp.MainDesc = reader[1].ToString();
                        lstMain.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstMain;
        }

        public List<FinanceProp> GetGLMainHO(string ClassificationCode, string GroupCode, string BranchCode)
        {
            List<FinanceProp> lstMain = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstMain = new List<FinanceProp>();
                lstMain.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_MAIN_CODE, B.GL_MAIN_DESCRIPTION FROM CHART_OF_ACCOUNT A, GL_MASTER B WHERE A.GL_MAIN_CODE = B.GL_MAIN_CODE " +
                                "AND A.GL_CLASSIFICATION_CODE ='" + ClassificationCode + "' AND A.GL_GROUP_CODE='" + GroupCode + "' ORDER BY B.GL_MAIN_DESCRIPTION";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.MainCode = reader[0].ToString();
                        oProp.MainDesc = reader[1].ToString();
                        lstMain.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstMain;
        }
        #endregion

        #region GetGLSub
        public List<FinanceProp> GetGLSub(FinanceProp Prop, string BranchCode)
        {
            List<FinanceProp> lstSub = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstSub = new List<FinanceProp>();
                lstSub.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_SUB_CODE, B.GL_SUB_DESCRIPTION FROM CHART_OF_ACCOUNT A, GL_SUB_MASTER B WHERE A.GL_SUB_CODE = B.GL_SUB_CODE " +
                                "AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode + "' AND A.GL_GROUP_CODE='" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode + "' AND A.BRANCH_CODE = '" + BranchCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.SubCode = reader[0].ToString();
                        oProp.SubDesc = reader[1].ToString();
                        lstSub.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstSub;
        }

        public List<FinanceProp> GetGLSubHO(FinanceProp Prop, string BranchCode)
        {
            List<FinanceProp> lstSub = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstSub = new List<FinanceProp>();
                lstSub.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.GL_SUB_CODE, B.GL_SUB_DESCRIPTION FROM CHART_OF_ACCOUNT A, GL_SUB_MASTER B WHERE A.GL_SUB_CODE = B.GL_SUB_CODE " +
                                "AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode + "' AND A.GL_GROUP_CODE='" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode + "' ORDER BY B.GL_SUB_DESCRIPTION";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.SubCode = reader[0].ToString();
                        oProp.SubDesc = reader[1].ToString();
                        lstSub.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstSub;
        }
        #endregion

        #region GetGLAccount
        public List<FinanceProp> GetGLAccount(FinanceProp Prop)
        {
            List<FinanceProp> lstAccount = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstAccount = new List<FinanceProp>();
                lstAccount.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = string.Empty;

                if (Prop.SubCode.ToString() == "0220")
                {
                    sQuery = "SELECT DISTINCT A.GL_ACCOUNT_CODE, B.DESCRIPTION + ' | ' + C.LOCATION" +
                                    " FROM CHART_OF_ACCOUNT A WITH (NOLOCK), GL_ACCOUNT_MASTER B WITH (NOLOCK), CUSTOMER_MASTER C WITH (NOLOCK)" +
                                    " WHERE A.GL_ACCOUNT_CODE = B.GL_ACCOUNT_CODE AND A.GL_MAIN_CODE = B.GL_MAIN_CODE AND A.BRANCH_CODE=C.BRANCH_CODE AND B.GL_ACCOUNT_CODE = C.CUSTOMER_CODE" +
                                    " AND A.GL_SUB_CODE = B.GL_SUB_CODE AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode +
                                    "' AND A.GL_GROUP_CODE = '" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode +
                                    "' AND A.GL_SUB_CODE = '" + Prop.SubCode + "' AND B.DESCRIPTION<>'' AND B.DESCRIPTION is NOT NULL";
                }
                else
                {
                    sQuery = "SELECT DISTINCT A.GL_ACCOUNT_CODE, B.DESCRIPTION " +
                                    "FROM CHART_OF_ACCOUNT A WITH (NOLOCK), GL_ACCOUNT_MASTER B WITH (NOLOCK) " +
                                    "WHERE A.GL_ACCOUNT_CODE = B.GL_ACCOUNT_CODE AND A.GL_MAIN_CODE = B.GL_MAIN_CODE " +
                                    "AND A.GL_SUB_CODE = B.GL_SUB_CODE AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode +
                                    "' AND A.GL_GROUP_CODE = '" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode +
                                    "' AND A.GL_SUB_CODE = '" + Prop.SubCode + "' AND B.DESCRIPTION<>'' AND B.DESCRIPTION is NOT NULL";
                }

                if (!string.IsNullOrEmpty(Prop.BranchCode))
                    sQuery = sQuery + " AND A.BRANCH_CODE = '" + Prop.BranchCode + "'";
                
                if (Prop.SubCode.ToString() == "0220")
                {
                    sQuery = sQuery + " ORDER BY B.DESCRIPTION + ' | ' + C.LOCATION";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.AccCode = reader[0].ToString();
                        oProp.AccDesc = reader[1].ToString();
                        lstAccount.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstAccount;
        }

        public List<FinanceProp> GetGLAccountHO(FinanceProp Prop)
        {
            List<FinanceProp> lstAccount = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstAccount = new List<FinanceProp>();
                lstAccount.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = string.Empty;

                if (Prop.SubCode.ToString() == "0220")
                {
                    sQuery = "SELECT DISTINCT A.GL_ACCOUNT_CODE, B.DESCRIPTION + ' | ' + C.LOCATION" +
                                    " FROM CHART_OF_ACCOUNT A WITH (NOLOCK), GL_ACCOUNT_MASTER B WITH (NOLOCK), CUSTOMER_MASTER C WITH (NOLOCK)" +
                                    " WHERE A.GL_ACCOUNT_CODE = B.GL_ACCOUNT_CODE AND A.GL_MAIN_CODE = B.GL_MAIN_CODE AND A.BRANCH_CODE=C.BRANCH_CODE AND B.GL_ACCOUNT_CODE = C.CUSTOMER_CODE" +
                                    " AND A.GL_SUB_CODE = B.GL_SUB_CODE AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode +
                                    "' AND A.GL_GROUP_CODE = '" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode +
                                    "' AND A.GL_SUB_CODE = '" + Prop.SubCode + "' AND B.DESCRIPTION<>'' AND B.DESCRIPTION is NOT NULL";
                }
                else
                {
                    sQuery = "SELECT DISTINCT A.GL_ACCOUNT_CODE, B.DESCRIPTION " +
                                    "FROM CHART_OF_ACCOUNT A WITH (NOLOCK), GL_ACCOUNT_MASTER B WITH (NOLOCK) " +
                                    "WHERE A.GL_ACCOUNT_CODE = B.GL_ACCOUNT_CODE AND A.GL_MAIN_CODE = B.GL_MAIN_CODE " +
                                    "AND A.GL_SUB_CODE = B.GL_SUB_CODE AND A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode +
                                    "' AND A.GL_GROUP_CODE = '" + Prop.GroupCode + "' AND A.GL_MAIN_CODE = '" + Prop.MainCode +
                                    "' AND A.GL_SUB_CODE = '" + Prop.SubCode + "' AND B.DESCRIPTION<>'' AND B.DESCRIPTION is NOT NULL";
                }

                if (!string.IsNullOrEmpty(Prop.BranchCode))
                    sQuery = sQuery + " AND A.BRANCH_CODE = '" + Prop.BranchCode + "'";

                if (Prop.SubCode.ToString() == "0220")
                {
                    sQuery = sQuery + " ORDER BY B.DESCRIPTION + ' | ' + C.LOCATION";
                }

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.AccCode = reader[0].ToString();
                        oProp.AccDesc = reader[1].ToString();
                        lstAccount.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstAccount;
        }
        #endregion

        #region GetGLBranch
        public List<FinanceProp> GetGLBranch(FinanceProp Prop)
        {
            List<FinanceProp> lstAccount = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstAccount = new List<FinanceProp>();
                lstAccount.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT DISTINCT A.BRANCH_CODE, B.BRANCH_NAME " +
                                "FROM CHART_OF_ACCOUNT A, BRANCH_MASTER B " +
                                "WHERE A.BRANCH_CODE = B.BRANCH_CODE AND " +
                                "A.GL_CLASSIFICATION_CODE ='" + Prop.ClassificationCode + "' AND A.GL_GROUP_CODE = '" + Prop.GroupCode +
                                "' AND A.GL_MAIN_CODE = '" + Prop.MainCode + "' AND A.GL_SUB_CODE = '" + Prop.SubCode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.BranchCode = reader[0].ToString();
                        oProp.BranchDesc = reader[1].ToString();
                        lstAccount.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstAccount;
        }
        #endregion

        #region GetGLBranch
        public List<FinanceProp> GetGLBranchesHO()
        {
            List<FinanceProp> lstAccount = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstAccount = new List<FinanceProp>();
                lstAccount.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT A.BRANCH_CODE, B.BRANCH_NAME FROM CHART_OF_ACCOUNT A WITH (NOLOCK) INNER JOIN BRANCH_MASTER B WITH (NOLOCK) " +
                                "ON A.BRANCH_CODE = B.BRANCH_CODE INNER JOIN USERS U WITH (NOLOCK) ON B.BRANCH_CODE = U.BRANCHCODE GROUP BY A.BRANCH_CODE, B.BRANCH_NAME ORDER BY A.BRANCH_CODE";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.BranchCode = reader[0].ToString();
                        oProp.BranchDesc = reader[1].ToString();
                        lstAccount.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstAccount;
        }

        public List<FinanceProp> GetGLBranchesZone(int ZoneCode)
        {
            List<FinanceProp> lstAccount = null;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstAccount = new List<FinanceProp>();
                lstAccount.Add(new FinanceProp());
                Database ImpalDB = DataAccess.GetDatabase();
                string sQuery = "SELECT A.BRANCH_CODE, B.BRANCH_NAME FROM CHART_OF_ACCOUNT A WITH (NOLOCK) INNER JOIN BRANCH_MASTER B WITH (NOLOCK) " +
                                "ON A.BRANCH_CODE = B.BRANCH_CODE INNER JOIN USERS U WITH (NOLOCK) ON B.BRANCH_CODE = U.BRANCHCODE " +
                                "INNER JOIN STATE_MASTER S WITH (NOLOCK) ON S.STATE_CODE=B.STATE_CODE " +
                                "INNER JOIN ZONE_MASTER Z WITH(NOLOCK) ON Z.ZONE_CODE = S.ZONE_CODE and Z.ZONE_CODE = " + ZoneCode + " GROUP BY A.BRANCH_CODE, B.BRANCH_NAME ORDER BY A.BRANCH_CODE";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FinanceProp oProp = new FinanceProp();
                        oProp.BranchCode = reader[0].ToString();
                        oProp.BranchDesc = reader[1].ToString();
                        lstAccount.Add(oProp);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstAccount;
        }
        #endregion

        #region CalculateGeneralLedger

        public void CalculateGeneralLedger(FinanceProp Prop)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_CALGLOS");
                ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmd, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmd, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmd, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmd, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmd, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, Prop.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmd);

                DbCommand cmdSub = ImpalDB.GetStoredProcCommand("USP_CALGLOS_SUB");
                ImpalDB.AddInParameter(cmdSub, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmdSub, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmdSub, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmdSub, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmdSub, "@to_date", DbType.String, Prop.ToDate);
                cmdSub.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmdSub);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CalculateGeneralLedgerBackUp
        public void CalculateGeneralLedgerBackUp(FinanceProp Prop)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_CALGLOS");
                ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmd, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmd, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmd, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmd, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmd, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, Prop.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmd);

                DbCommand cmdSub = ImpalDB.GetStoredProcCommand("USP_CALGLOS_SUB");
                ImpalDB.AddInParameter(cmdSub, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmdSub, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmdSub, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmdSub, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmdSub, "@to_date", DbType.String, Prop.ToDate);
                cmdSub.CommandTimeout = ConnectionTimeOut.TimeOut;;
                ImpalDB.ExecuteNonQuery(cmdSub);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CalculateGeneralLedgerZone

        public void CalculateGeneralLedgerZone(FinanceProp Prop)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_CALGLOS_HO");
                ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmd, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmd, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmd, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmd, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmd, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, Prop.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);

                DbCommand cmdSub = ImpalDB.GetStoredProcCommand("USP_CALGLOS_SUB_HO");
                ImpalDB.AddInParameter(cmdSub, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmdSub, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmdSub, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmdSub, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmdSub, "@to_date", DbType.String, Prop.ToDate);
                cmdSub.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmdSub);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #endregion

        #region CalculateGeneralLedgerHO

        public void CalculateGeneralLedgerHO(FinanceProp Prop)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("USP_CALGLOS_HO");
                ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmd, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmd, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmd, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmd, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmd, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, Prop.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmd);

                DbCommand cmdSub = ImpalDB.GetStoredProcCommand("USP_CALGLOS_SUB_HO");
                ImpalDB.AddInParameter(cmdSub, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmdSub, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmdSub, "@GL_Account", DbType.String, Prop.AccCode);
                ImpalDB.AddInParameter(cmdSub, "@Branch_Code", DbType.String, Prop.BranchCode);
                ImpalDB.AddInParameter(cmdSub, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmdSub, "@to_date", DbType.String, Prop.ToDate);
                cmdSub.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ImpalDB.ExecuteNonQuery(cmdSub);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public DataSet CalculateGeneralLedgerHoLive(FinanceProp Prop, string ZoneCode, string StateCode, string BranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataSet ds = new DataSet();

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calglos_HO_Live");
                ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.String, ZoneCode);
                ImpalDB.AddInParameter(cmd, "@State_Code", DbType.String, StateCode);
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Accounting_Period", DbType.Int16, Prop.AccPeriod);
                ImpalDB.AddInParameter(cmd, "@GL_Classification", DbType.String, Prop.ClassificationCode);
                ImpalDB.AddInParameter(cmd, "@GL_Group", DbType.String, Prop.GroupCode);
                ImpalDB.AddInParameter(cmd, "@GL_Main", DbType.String, Prop.MainCode);
                ImpalDB.AddInParameter(cmd, "@GL_Sub", DbType.String, Prop.SubCode);
                ImpalDB.AddInParameter(cmd, "@GL_Account", DbType.String, Prop.AccCode);                
                ImpalDB.AddInParameter(cmd, "@From_date", DbType.String, Prop.FromDate);
                ImpalDB.AddInParameter(cmd, "@to_date", DbType.String, Prop.ToDate);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut; ;
                ds = ImpalDB.ExecuteDataSet(cmd);                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ds;
        }
        #endregion

        #region CalculateSupplierReconciliation
        
        public void CalculateSupplierReconciliation(FinanceProp Prop, string BranchCode, string Supplier)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string SuppCode = "";

                if (Supplier == "160")
                    SuppCode = "16','75','84";
                else if (Supplier == "410")
                    SuppCode = "41','32";
                else if (Supplier != "" && Supplier != "0")
                    SuppCode = Supplier.Substring(0, 2);

                Database ImpalDB = DataAccess.GetDatabase();
                //Modified for Cloud Migration
                string sDebitQry = string.Empty;
                if (BranchCode.ToUpper() != "CRP")
                    sDebitQry = "DELETE FROM SUPPLIER_RECON_GLCR where Serial_Number = '" + Prop.BranchCode + "'";
                else
					sDebitQry = "TRUNCATE TABLE SUPPLIER_RECON_GLCR";
                DbCommand cmdD = ImpalDB.GetSqlStringCommand(sDebitQry);
                cmdD.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmdD);
                string sQuery = "INSERT INTO SUPPLIER_RECON_GLCR SELECT SERIAL_NUMBER,TRANSACTION_TYPE, 'CHART_OF_ACCOUNT_CODE'= " +
                                "   CASE WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('32','41') THEN SUBSTRING(CHART_OF_ACCOUNT_CODE,1,14) +'410'+ SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) " +
                                "  WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('16','75','84') THEN SUBSTRING(CHART_OF_ACCOUNT_CODE,1,14) + '160'+SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) " +
                                "   ELSE CHART_OF_ACCOUNT_CODE    END,	" +
                                " DOCUMENT_NUMBER,DOCUMENT_DATE,DOCUMENT_REFERENCE_NUMBER,DOCUMENT_REFERENCE_DATE,DEBIT_CREDIT_INDICATOR,AMOUNT,REMARKS,DATESTAMP,DOWNUP_STATUS,INDICATOR,CUSTOMER_CODE," +
                                " 'SUPPLIER_CODE'= CASE WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('32','41') THEN 410 " +
                                "  WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('16','75','84') THEN 160 " +
                                "   ELSE SUBSTRING(CHART_OF_ACCOUNT_CODE,15,3)   END, DATEDIFF(DD,DOCUMENT_DATE,CONVERT(DATETIME,'" + Prop.ToDate + "',103)) " +
                                " FROM GENERAL_LEDGER_DETAIL WHERE SUBSTRING(SERIAL_NUMBER,1,4) >= 2007 " +
                                " AND SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) = '" + Prop.BranchCode + "' AND SUBSTRING(CHART_OF_ACCOUNT_CODE,7,4) = '0060' AND DEBIT_CREDIT_INDICATOR ='C' AND AMOUNT > 0 " +
                                " AND CONVERT(DATETIME,DOCUMENT_DATE,103) >=   CONVERT(DATETIME,'" + Prop.FromDate + "' ,103)  AND CONVERT(DATETIME,DOCUMENT_DATE,103) <=  CONVERT(DATETIME,'" + Prop.ToDate + "',103) ";
				if (BranchCode.ToUpper() != "CRP")
                    sQuery = sQuery + "AND SUBSTRING(SERIAL_NUMBER,15,3) = '" + Prop.BranchCode + "' AND SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) in ('" + SuppCode + "')";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
                //Modified for Cloud Migration
                string sDebitQuery1 = string.Empty;
                if (BranchCode.ToUpper() != "CRP")
                    sDebitQuery1 = "DELETE FROM SUPPLIER_RECON_GLDR where Serial_Number = '" + Prop.BranchCode + "'";
                else
                    sDebitQuery1 = "DELETE FROM SUPPLIER_RECON_GLDR";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sDebitQuery1);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut; 
                ImpalDB.ExecuteNonQuery(cmd1);

                string sDebitQuery = "INSERT INTO SUPPLIER_RECON_GLDR SELECT SERIAL_NUMBER,TRANSACTION_TYPE, 'CHART_OF_ACCOUNT_CODE'= " +
                            "   CASE WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('32','41') THEN SUBSTRING(CHART_OF_ACCOUNT_CODE,1,14) +'410'+ SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) " +
                            "  WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('16','75','84') THEN SUBSTRING(CHART_OF_ACCOUNT_CODE,1,14) + '160'+SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) " +
                            "   ELSE CHART_OF_ACCOUNT_CODE    END,	" +
                            " DOCUMENT_NUMBER,DOCUMENT_DATE,DOCUMENT_REFERENCE_NUMBER,DOCUMENT_REFERENCE_DATE,DEBIT_CREDIT_INDICATOR,AMOUNT,REMARKS,DATESTAMP,DOWNUP_STATUS,INDICATOR,CUSTOMER_CODE," +
                            " 'SUPPLIER_CODE'= CASE WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('32','41') THEN 410 " +
                            "  WHEN SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) IN ('16','75','84') THEN 160 " +
                            "   ELSE SUBSTRING(CHART_OF_ACCOUNT_CODE,15,3)   END , DATEDIFF(DD,DOCUMENT_DATE,CONVERT(DATETIME,'" + Prop.ToDate + "',103)) " +
                            " FROM GENERAL_LEDGER_DETAIL WHERE SUBSTRING(SERIAL_NUMBER,1,4) >= 2007 " +
                            " AND SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) = '" + Prop.BranchCode + "' AND SUBSTRING(CHART_OF_ACCOUNT_CODE,7,4) = '0060'  AND DEBIT_CREDIT_INDICATOR ='D' AND AMOUNT > 0 " +
                            " AND CONVERT(DATETIME,DOCUMENT_DATE,103) >=   CONVERT(DATETIME,'" + Prop.FromDate + "' ,103)  AND CONVERT(DATETIME,DOCUMENT_DATE,103) <=  CONVERT(DATETIME,'" + Prop.ToDate + "',103) ";
			if (BranchCode.ToUpper() != "CRP")
                    sQuery = sQuery + "AND SUBSTRING(CHART_OF_ACCOUNT_CODE,18,3) = '" + Prop.BranchCode + "'";

                sQuery = sQuery + "AND SUBSTRING(CHART_OF_ACCOUNT_CODE,15,2) in ('" + SuppCode + "')";

                DbCommand cmdDebit = ImpalDB.GetSqlStringCommand(sDebitQuery);
                cmdDebit.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmdDebit);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetBankBranches
        
        public List<FinanceProp> GetBankBranches(string strBank, string strBranch)
        {
            List<FinanceProp> lstBranches = new List<FinanceProp>();
            lstBranches.Add(new FinanceProp(string.Empty, string.Empty));
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            if (string.IsNullOrEmpty(strBank))
            {
                sQuery = "select Bank_Branch_Code,Branch_Name from Bank_master b,Bank_Branch_Master a where a.Bank_Code=b.Bank_Code ";
            }
            else
            {
                sQuery = "select Bank_Branch_Code,Branch_Name from Bank_master b,Bank_Branch_Master a where a.Bank_Code=b.Bank_Code and b.Bank_Code=" + strBank + " and substring(chart_of_Account_Code,18,3) = '" + strBranch + "' ";
            }
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                    lstBranches.Add(new FinanceProp(reader[0].ToString(), reader[1].ToString()));
            }
            return lstBranches;
        }
        #endregion

        #region GetChallanNumber
       
        public List<FinanceProp> GetChallanNumber(string strBranchCode, int AccPeriodCode)
        {
            List<FinanceProp> lstChallan = new List<FinanceProp>();
            lstChallan.Add(new FinanceProp());
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = string.Empty;
            //sQuery = "select Pay_in_Slip_Number from Pay_in_slip_header where month(pay_in_slip_date) = month(getdate()) and year(pay_in_slip_date) = year(getdate()) and Branch_Code = '" + strBranchCode +  "' order by pay_in_slip_number desc";
            //DbCommand cmd = ImpalDB.GetSqlStringCommand(sQuery);
            //cmd.CommandTimeout = ConnectionTimeOut.TimeOut; 

            DbCommand cmdCh = ImpalDB.GetStoredProcCommand("Usp_GetBankChallanNumber");
            ImpalDB.AddInParameter(cmdCh, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(cmdCh, "@Accounting_Period", DbType.Int16, AccPeriodCode);
            cmdCh.CommandTimeout = ConnectionTimeOut.TimeOut;

            using (IDataReader reader = ImpalDB.ExecuteReader(cmdCh))
            {
                while (reader.Read())
                    lstChallan.Add(new FinanceProp(reader[0].ToString()));
            }

            return lstChallan;
        }
        #endregion

        public List<string> GetCashDiscountDocumentNumber(string BranchCode)
        {
            List<string> lstDocumentNumber = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            strQuery = "Select distinct CD_CN_Number From Cash_Discount_Cust WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Approval_Status='A' and LEFT(CD_CN_Number,4)>=YEAR(GETDATE())-1 order by CD_CN_Number desc";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstDocumentNumber.Add(reader[0].ToString());
                }
            }

            if (lstDocumentNumber.Count <= 0)
                lstDocumentNumber.Add("");

            return lstDocumentNumber;
        }

        public List<string> GetCashDiscountNumberForApproval(string BranchCode)
        {
            List<string> lstDocumentNumber = new List<string>();
            string strQuery = default(string);
            Database ImpalDB = DataAccess.GetDatabase();
            lstDocumentNumber.Add("");
            strQuery = "Select distinct CD_CN_Number From Cash_Discount_Cust WITH (NOLOCK) Where Branch_Code='" + BranchCode + "' and Approval_Status is NULL and LEFT(CD_CN_Number,4)>=YEAR(GETDATE())-1 order by CD_CN_Number desc";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    lstDocumentNumber.Add(reader[0].ToString());
                }
            }                

            return lstDocumentNumber;
        }
    } 
    #endregion

    #region Properties
    /// <summary>
    /// Contains all the Finance related Properties
    /// </summary>
    public class FinanceProp
    {
        public FinanceProp()
        {
        }

        public FinanceProp(string MonthYear)
        {
            _MonthYear = MonthYear;
        }
        public FinanceProp(string Bank_Branch_Code, string BranchName)
        {
            _Bank_Branch_Code = Bank_Branch_Code;
            _Branch_Name = BranchName;

        }
        private string _MonthYear;
       
        public string MonthYear
        {
            get { return _MonthYear; }
            set { _MonthYear = value; }
        }
       
        private int _AccPeriod;
        public int AccPeriod
        {
            get { return _AccPeriod; }
            set { _AccPeriod = value; }
        }
        private string _FromDate;
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private string _ToDate;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private string _ClassificationCode;
        public string ClassificationCode
        {
            get { return _ClassificationCode; }
            set { _ClassificationCode = value; }
        }

        private string _ClassificationDesc;
        public string ClassificationDesc
        {
            get { return _ClassificationDesc; }
            set { _ClassificationDesc = value; }
        }

        private string _GroupCode;
        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        private string _GroupDesc;
        public string GroupDesc
        {
            get { return _GroupDesc; }
            set { _GroupDesc = value; }
        }

        private string _MainCode;
        public string MainCode
        {
            get { return _MainCode; }
            set { _MainCode = value; }
        }

        private string _MainDesc;
        public string MainDesc
        {
            get { return _MainDesc; }
            set { _MainDesc = value; }
        }

        private string _SubCode;
        public string SubCode
        {
            get { return _SubCode; }
            set { _SubCode = value; }
        }

        private string _SubDesc;
        public string SubDesc
        {
            get { return _SubDesc; }
            set { _SubDesc = value; }
        }

        private string _AccCode;
        public string AccCode
        {
            get { return _AccCode; }
            set { _AccCode = value; }
        }

        private string _AccDesc;
        public string AccDesc
        {
            get { return _AccDesc; }
            set { _AccDesc = value; }
        }

        private string _BranchCode;
        public string BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }

        private string _BranchDesc;
        public string BranchDesc
        {
            get { return _BranchDesc; }
            set { _BranchDesc = value; }
        }
        private string _Bank_Branch_Code;
        public string Bank_Branch_Code
        {
            get { return _Bank_Branch_Code; }
            set { _Bank_Branch_Code = value; }
        }
        private string _Branch_Name;
        public string BranchName
        {
            get { return _Branch_Name; }
            set { _Branch_Name = value; }
        }
        private string _ChallanNumb;
        public string ChallanNumb
        {
            get { return _ChallanNumb; }
            set { _ChallanNumb = value; }
        }
    } 
    #endregion
}
