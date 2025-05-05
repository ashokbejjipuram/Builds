using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace IMPALWeb.Dashboard.SalesDashboard
{
    public partial class SalesbyLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ssrsSalesbyLine.ReportPath = System.Configuration.ConfigurationManager.AppSettings["SalesbyLine"].ToString();
                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                ssrsSalesbyLine.lstReportParameter = lstReportParameter;
                ssrsSalesbyLine.GenerateReport();
            }
        }
    }
}
