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
    public partial class CreditLimitMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    CommonDashboard obj = new CommonDashboard();
                   
                    DashCreLimitMonitor.ReportName = "Credit Limit Monitoring";
                    List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                    ////ReportParameter p1 = new ReportParameter("Limit", ZCode.ToString());
                    ////lstReportParameter.Add(p1);
                    DashCreLimitMonitor.lstReportParameter = lstReportParameter;
                    DashCreLimitMonitor.ReportPath = System.Configuration.ConfigurationManager.AppSettings["CreditLimitZone"].ToString();
                    DashCreLimitMonitor.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
