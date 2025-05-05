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
    public partial class OutstandIndus : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //if (!IsPostBack)
                //{
                //    Session.Remove("SSRSReport");
                //}
                //if (Session["SSRSReport"] != null)
                //    DashGovtTran.GenerateReport();
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
                    CommonDashboard obj = new CommonDashboard();
                    int ZCode = 3;
                    // string DatePeriod = txtDatePick.Text.ToString();
                    //DashIndus.Reporturl = "http://dotnetonline/ReportServer";
                    DashIndus.ReportName = "OutStanding Indus Dealer";
                    List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                    ReportParameter p1 = new ReportParameter("Limit", ZCode.ToString());
                    lstReportParameter.Add(p1);
                    DashIndus.lstReportParameter = lstReportParameter;
                    DashIndus.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingIndusDealer"].ToString();
                    //DashIndus.ReportPath = @"/Finance Dashboard/Outstanding/OutStandingIndusDealer";
                    DashIndus.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
        //protected void btnReport_Click(object sender, EventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    CommonDashboard obj = new CommonDashboard();
        //    int ZCode = 3;
        //    try
        //    {
        //        Main mainmaster = (Main)Page.Master;
        //        mainmaster.ShowHideFilters(btnReport, dashReportTable, dashReportHolder);
        //        if (btnReport.Text == "Back")
        //        {
        //            // string DatePeriod = txtDatePick.Text.ToString();
        //            //DashIndus.Reporturl = "http://dotnetonline/ReportServer";
        //            DashIndus.ReportName = "OutStanding Indus Dealer";
        //            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
        //            ReportParameter p1 = new ReportParameter("Limit", ZCode.ToString());
        //            lstReportParameter.Add(p1);
        //            DashIndus.lstReportParameter = lstReportParameter;
        //            DashIndus.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingIndusDealer"].ToString();
        //            //DashIndus.ReportPath = @"/Finance Dashboard/Outstanding/OutStandingIndusDealer";
        //            DashIndus.GenerateReport();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException(Source, exp);
        //    }
        //}
        #endregion
    }
}
