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
    public partial class Outstanabove2Lh : System.Web.UI.Page
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
            int Limit = 0;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, dashReportTable, dashReportHolder);
                if (btnReport.Text == "Back")
                {
                    Database ImpalDB = DataAccess.GetDatabase();

                    switch (ddlReportList.SelectedValue.Trim())
                    {
                        case "2 to 3 Lakhs":
                            {
                                Limit = 1;
                                DashOS2Lh.ReportName = "OutStanding2to3Lakhs";
                                break;
                            }
                        case "Above 3 Lakhs":
                            {
                                Limit = 2;
                                DashOS2Lh.ReportName = "OutStandingAbove3Lakhs";
                                break;
                            }
                    }
                    // string DatePeriod = txtDatePick.Text.ToString();
                    //DashOS2Lh.Reporturl = "http://dotnetonline/ReportServer";

                    List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                    ReportParameter p1 = new ReportParameter("Limit", Limit.ToString());
                    lstReportParameter.Add(p1);
                    DashOS2Lh.lstReportParameter = lstReportParameter;
                    DashOS2Lh.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingAbove3Lakhs"].ToString();
                    //DashOS2Lh.ReportPath = @"/Finance Dashboard/Outstanding/OutStandingabove2Lakhs";
                    DashOS2Lh.GenerateReport();


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
