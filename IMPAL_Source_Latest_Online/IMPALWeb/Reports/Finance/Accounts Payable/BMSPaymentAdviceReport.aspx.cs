#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Diagnostics;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
#endregion

namespace IMPALWeb.Reports.Finance.Accounts_Payable
{
    public partial class BMSPaymentAdviceReport : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    if (crBMSPaymentAdviceReport != null)
                    {
                        crBMSPaymentAdviceReport.Dispose();
                        crBMSPaymentAdviceReport = null;
                    }
                    if (Session["BMSCCWHNumber"] != null && Session["BMSCCWHNumber"].ToString() != "")
                    {
                        divSelection.Visible = false;
                        DivBackbtn.Attributes.Add("style", "display:block");
                        fnGenerateReport();
                    }
                    else
                    {
                        divSelection.Visible = true;
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
            if (crBMSPaymentAdviceReport != null)
            {
                crBMSPaymentAdviceReport.Dispose();
                crBMSPaymentAdviceReport = null;
            }
        }
        protected void crBMSPaymentAdviceReport_Unload(object sender, EventArgs e)
        {
            if (crBMSPaymentAdviceReport != null)
            {
                crBMSPaymentAdviceReport.Dispose();
                crBMSPaymentAdviceReport = null;
            }
        }

        #region fnGenerateReport
        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            string strField = default(string);
            string strValue = default(string);
            try
            {
                if (Session["BMSCCWHNumber"] != null && Session["BMSCCWHNumber"].ToString() != "")
                {
                    strField = "{Bms_Header.CCWH_Number}";
                    strValue = Session["BMSCCWHNumber"].ToString();                    
                    
                    crBMSPaymentAdviceReport.ReportName = "BMSPaymentAdviceReport";
                    crBMSPaymentAdviceReport.RecordSelectionFormula = strField + "= " + "'" + strValue + "'";
                    crBMSPaymentAdviceReport.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        //#region Generate Selection Formula
        //public void GenerateSelectionFormula()
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
        //    try
        //    {
        //        string strSelectionFormula = default(string);
        //        string strField = default(string);
        //        string strValue = default(string);
        //        string strReportName = default(string);

        //        strField = "{Corporate_Payment_Detail.ccwh_Number}";
        //        strValue = ddlCCWHNumber.SelectedValue;
        //        strSelectionFormula = strField + "= " + "'" + strValue + "'";
        //        strReportName = GetReportName();
        //        if (!string.IsNullOrEmpty(strReportName))
        //        {
        //            crBMSPaymentAdviceReport.ReportName = strReportName;
        //            crBMSPaymentAdviceReport.RecordSelectionFormula = strSelectionFormula;
        //            crBMSPaymentAdviceReport.GenerateReport();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException(Source, exp);
        //    }

        //}
        //#endregion
        //#region ReportButton Click
        //protected void btnReport_Click(object sender, EventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    try
        //    {
        //        Main mainmaster = (Main)Page.Master;
        //        mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
        //        if (btnReport.Text == "Back")
        //        {
        //            GenerateSelectionFormula();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException(Source, exp);
        //    }
        //}
        //#endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                if (Session["BMSCCWHNumber"] != null && Session["BMSCCWHNumber"].ToString() != "")
                    Response.Redirect("~/Transactions/Finance/Payable/BMSPaymentAdvice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                Session["BMSCCWHNumber"] = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
