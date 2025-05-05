using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using IMPALWeb.Dashboard;

namespace IMPALWeb.UserControls
{
    public partial class SSRSReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void GenerateReport()
        {
            CommonDashboard objCommonDashboard = new CommonDashboard();
            try
            {
                ReportViewer1.ShowToolBar = false;
                ReportViewer1.Reset();
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                objCommonDashboard.RunReport(ReportViewer1.ServerReport, lstReportParameter, Reporturl, ReportPath);
                btnExport.Visible = true;
                drp.Visible = true;
                ReportViewer1.ShowToolBar = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                objCommonDashboard = null;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            CommonDashboard objCommonDashboard = new CommonDashboard();
            try
            {
                objCommonDashboard.Export(ReportViewer1.ServerReport, drp.SelectedValue, ReportName);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string Reporturl { get; set; }

        public string ReportPath { get; set; }

        public string ReportName { get; set; }

        public List<ReportParameter> lstReportParameter { get; set; }
    }
}