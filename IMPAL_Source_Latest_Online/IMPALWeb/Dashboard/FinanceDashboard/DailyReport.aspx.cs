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
    public partial class DailyReport : System.Web.UI.Page
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
                    DashDailyReport.lstReportParameter = lstReportParameter;
                    DashDailyReport.ReportName = "Finance_collection";
                    DashDailyReport.ReportPath = System.Configuration.ConfigurationManager.AppSettings["Financecollection"].ToString();// @"/Finance Dashboard/Daily Report/Finance_collection";
                    DashDailyReport.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
      
        #endregion
    }
}
