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
    public partial class Collection_Outstanding : System.Web.UI.Page
    {
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_collectvsOS");
                    ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    switch (ddlReportList.SelectedValue.Trim())
                    {
                        case "BRANCH WISE":
                            {
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashCollectionOutstanding.lstReportParameter = lstReportParameter;
                                DashCollectionOutstanding.ReportName = "CollectionOS_Br";
                                DashCollectionOutstanding.ReportPath = System.Configuration.ConfigurationManager.AppSettings["CollectionOSBr"].ToString();
                                //DashCollectionOutstanding.ReportPath = @"/Finance Dashboard/Outstanding/Dash_collectvsOS_Br";
                                DashCollectionOutstanding.GenerateReport();
                                break;
                            }
                        case "CUSTOMER WISE":
                            {
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashCollectionOutstanding.lstReportParameter = lstReportParameter;
                                DashCollectionOutstanding.ReportName = "CollectionOS_Cust";
                                DashCollectionOutstanding.ReportPath = System.Configuration.ConfigurationManager.AppSettings["CollectionOSCust"].ToString();
                                //DashCollectionOutstanding.ReportPath = @"/Finance Dashboard/Outstanding/Dash_collectvsOS_Cust";
                                DashCollectionOutstanding.GenerateReport();
                                break;
                            }
                        case "TOWN WISE":
                            {
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashCollectionOutstanding.lstReportParameter = lstReportParameter;
                                DashCollectionOutstanding.ReportName = "CollectionOS_Town";
                                DashCollectionOutstanding.ReportPath = System.Configuration.ConfigurationManager.AppSettings["CollectionOSTown"].ToString();
                                //DashCollectionOutstanding.ReportPath = @"/Finance Dashboard/Outstanding/Dash_collectvsOS_Town";
                                DashCollectionOutstanding.GenerateReport();
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
