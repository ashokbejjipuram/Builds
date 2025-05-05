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
    public partial class HoPaymentReport : System.Web.UI.Page
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
                    if (crHoPaymentReport != null)
                    {
                        crHoPaymentReport.Dispose();
                    }

                    if (Session["HoCCWHNumber"] != null && Session["HoCCWHNumber"].ToString() != "")
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
            if (crHoPaymentReport != null)
            {
                crHoPaymentReport.Dispose();
                crHoPaymentReport = null;
            }
        }
        protected void crHoPaymentReport_Unload(object sender, EventArgs e)
        {
            if (crHoPaymentReport != null)
            {
                crHoPaymentReport.Dispose();
                crHoPaymentReport = null;
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
                if (Session["HoCCWHNumber"] != null && Session["HoCCWHNumber"].ToString() != "")
                {
                    strField = "{Corporate_Payment_Detail.ccwh_Number}";
                    strValue = Session["HoCCWHNumber"].ToString();                    
                    
                    crHoPaymentReport.ReportName = "HoPaymentReport";
                    crHoPaymentReport.RecordSelectionFormula = strField + "= " + "'" + strValue + "'";
                    crHoPaymentReport.GenerateReport();
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
        //            crHoPaymentReport.ReportName = strReportName;
        //            crHoPaymentReport.RecordSelectionFormula = strSelectionFormula;
        //            crHoPaymentReport.GenerateReport();
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
                Session["HoCCWHNumber"] = "";
                Session["Confirmstat"] = null;
                Session["InvFromDate"] = "";
                Session["InvToDate"] = "";
                Server.ClearError();
                if (Session["PrevURL"] != null && Session["PrevURL"].ToString() != "")
                    Response.Redirect("~/Transactions/Finance/Payable/" + Session["PrevURL"], false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
