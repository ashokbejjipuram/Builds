#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion 

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class DailyReport : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crDailyReport != null)
                    {
                        crDailyReport.Dispose();
                        crDailyReport = null;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crDailyReport != null)
            {
                crDailyReport.Dispose();
                crDailyReport = null;
            }
        }
        protected void crDailyReport_Unload(object sender, EventArgs e)
        {
            if (crDailyReport != null)
            {
                crDailyReport.Dispose();
                crDailyReport = null;
            }
        }

        #region Generate Selection Formula
        /// <summary>
        /// Method to generate selection formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strBranchCodeField = default(string);
                string strDateField = default(string);
                string strSelectionFormula = default(string);
                string strFromDate = default(string);
                string strCryFromDate = default(string);
                string strReportName = default(string);
                int intProcStatus = default(int);
                #endregion

                #region Selection Formula Formation
                strFromDate = DateTime.Today.ToString("dd/MM/yyyy");
                strBranchCodeField = "{daily_report.branch_code}";
                strDateField = "{daily_report.Report_Date}";
                strReportName = "DailyReport";
                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_adddailyreceipt");
                ImpalDB.AddInParameter(dbcmd, "@branch_code", DbType.String, strBranchCode.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                if (strBranchCode == "CRP")
                {
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryFromDate;
                }
                else
                {
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryFromDate + " and " + strBranchCodeField + "='" + strBranchCode + "'";
                }
                #endregion

                crDailyReport.ReportName = strReportName;
                crDailyReport.RecordSelectionFormula = strSelectionFormula;
                crDailyReport.GenerateReport();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Report Button Click
        /// <summary>
        /// Report Button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Entering btnReport_Click");
            try
            {
                GenerateSelectionFormula();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
