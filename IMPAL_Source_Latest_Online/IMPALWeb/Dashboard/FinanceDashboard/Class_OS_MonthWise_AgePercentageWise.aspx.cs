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
    public partial class Class_OS_MonthWise_AgePercentageWise : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            
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
                    DashClassOSMonthwise.lstReportParameter = lstReportParameter;
                    DashClassOSMonthwise.ReportName = "OS_MonthWise_AgePercentage";
                    DashClassOSMonthwise.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OSMonthWiseAgePercentage"].ToString();               
                    DashClassOSMonthwise.GenerateReport();    
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
        //    try
        //    {
        //        Main mainmaster = (Main)Page.Master;
        //        mainmaster.ShowHideFilters(btnReport, dashReportTable, dashReportHolder);
        //        if (btnReport.Text == "Back")
        //        {   
        //            //string DatePeriod = txtDatePick.Text.ToString();
        //           // DashClassOSMonthwise.Reporturl = "http://dotnetonline/ReportServer";
        //            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
        //            //ReportParameter p1 = new ReportParameter("Date_period", DatePeriod);
        //            //lstReportParameter.Add(p1);
        //            DashClassOSMonthwise.lstReportParameter = lstReportParameter;
        //            DashClassOSMonthwise.ReportName = "OS_MonthWise_AgePercentage";
        //            DashClassOSMonthwise.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OS_MonthWise_AgePercentage"].ToString();
        //            //DashClassOSMonthwise.ReportPath = @"/Finance Dashboard/Outstanding/OutstandingZone";
        //            DashClassOSMonthwise.GenerateReport();                   
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        IMPALLibrary.Log.WriteException(Source, exp);
        //    }
        //}
        #endregion
    }
}
