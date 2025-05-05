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
    public partial class OSGovtTrans : System.Web.UI.Page
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
                    List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                    //ReportParameter p1 = new ReportParameter("Zone_Code", ZCode.ToString());
                    // lstReportParameter.Add(p1);
                    DashGovtTran.lstReportParameter = lstReportParameter;
                    DashGovtTran.ReportName = "OutStanding GovtTrans";
                    DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingGovtTrans"].ToString();
                    //DashGovtTran.ReportPath = @"/Finance Dashboard/Outstanding/OutStandingGovtTrans";
                    DashGovtTran.GenerateReport();
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
        //    int ZCode = 0;
        //    try
        //    {
        //        Main mainmaster = (Main)Page.Master;
        //        mainmaster.ShowHideFilters(btnReport, dashReportTable, dashReportHolder);
        //        if (btnReport.Text == "Back")
        //        {                  

        //            //switch (ddlReportList.SelectedValue.Trim())
        //            //{
        //            //    case "SOUTH":
        //            //        {
        //            //            ZCode = 1;                                
        //            //            break;
        //            //        }
        //            //    case "NORTH":
        //            //        {
        //            //            ZCode = 2;
        //            //            break;                                
        //            //        }
        //            //    case "EAST":
        //            //        {
        //            //            ZCode = 3;
        //            //            break;                                
        //            //        }
        //            //    case "WEST":
        //            //        {
        //            //            ZCode = 4;
        //            //            break;
        //            //        }                       
        //            //}
        //            // string DatePeriod = txtDatePick.Text.ToString();
        //            //DashGovtTran.Reporturl = "http://dotnetonline/ReportServer";
        //            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
        //            //ReportParameter p1 = new ReportParameter("Zone_Code", ZCode.ToString());
        //           // lstReportParameter.Add(p1);
        //            DashGovtTran.lstReportParameter = lstReportParameter;
        //            DashGovtTran.ReportName = "OutStanding GovtTrans";
        //            DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingGovtTrans"].ToString();
        //            //DashGovtTran.ReportPath = @"/Finance Dashboard/Outstanding/OutStandingGovtTrans";
        //            DashGovtTran.GenerateReport();
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
