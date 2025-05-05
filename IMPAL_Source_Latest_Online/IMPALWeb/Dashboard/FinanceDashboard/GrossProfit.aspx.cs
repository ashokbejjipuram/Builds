using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.Common;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Reporting.WebForms;
using Microsoft.Office.Interop.Word;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace IMPALWeb.Dashboard.FinanceDashboard
{
    public partial class GrossProfit : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("SSRSReport");
                }
                if (Session["SSRSReport"] != null)
                    DashGrossProfit.GenerateReport();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("SSRSReport");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion


        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CommonDashboard obj = new CommonDashboard();
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, dashReportTable, dashReportHolder);
                if (btnReport.Text == "Back")
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    switch (ddlReportList.SelectedValue.Trim())
                    {
                        case "ACC GP %":
                        {                            
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "ACCOUNTS GP %";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            lstReportParameter.Add(p1);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["ACCOUNTSGP"].ToString();
                            //DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPAccPer";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "ACC CUMU":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "ACCOUNTS GP CUMULATIVE";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            lstReportParameter.Add(p1);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["ACCOUNTSGPCUMULATIVE"].ToString();
                            //DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPAccCumm";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "ACC MTH":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "ACCOUNTS GP MONTH";
                           // DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            lstReportParameter.Add(p1);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["ACCOUNTSGPMONTH"].ToString();
                            // DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPAccMonth";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "INV GP %":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "INVOICE GP %";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            ReportParameter p2 = new ReportParameter("Zone_Code", "A");
                            lstReportParameter.Add(p1);
                            lstReportParameter.Add(p2);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["INVOICEGP"].ToString();
                            //DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPInvPer";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "SOUTH GP %":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "SOUTH GP %";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            ReportParameter p2 = new ReportParameter("Zone_Code", "S");
                            lstReportParameter.Add(p1);
                            lstReportParameter.Add(p2);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["SOUTHGP"].ToString();
                           // DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPInvPer";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "REST OF INDIA GP %":
                        {

                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "REST OF INDIA GP %";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            ReportParameter p2 = new ReportParameter("Zone_Code", "R");
                            lstReportParameter.Add(p1);
                            lstReportParameter.Add(p2);
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["RESTOFINDIAGP"].ToString();
                            //DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPInvPer";
                            DashGrossProfit.GenerateReport();
                            break;
                        }
                        case "INV GP MONTH":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "INVOICE GP MONTH";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            ReportParameter p2 = new ReportParameter("Zone_Code", "A");
                            lstReportParameter.Add(p1);
                            lstReportParameter.Add(p2);                          
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["INVOICEGPMONTH"].ToString();
                           // DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPInvVal";
                            DashGrossProfit.GenerateReport();
                            break;
                           
                        }
                        case "INV GP CUMM":
                        {
                            string DatePeriod = txtDatePick.Text.ToString();
                            DashGrossProfit.ReportName = "INVOICE GP CUMULATIVE";
                            //DashGrossProfit.Reporturl = "http://dotnetonline/ReportServer";
                            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                            ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
                            ReportParameter p2 = new ReportParameter("Zone_Code", "A");
                            lstReportParameter.Add(p1);
                            lstReportParameter.Add(p2);                          
                            DashGrossProfit.lstReportParameter = lstReportParameter;
                            DashGrossProfit.ReportPath = System.Configuration.ConfigurationManager.AppSettings["INVOICEGPCUMULATIVE"].ToString();
                            //DashGrossProfit.ReportPath = @"/Finance Dashboard/Gross Profit/Dash_GPInvCumm";
                            DashGrossProfit.GenerateReport();
                            break;
                           
                        }
                        case "ACC AND INV GP %":
                        {
                            break;
                        }

                    }

                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
