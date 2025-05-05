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
    public partial class NoofDaysSameOS : System.Web.UI.Page
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
                    switch (ddlReportList.SelectedValue.Trim())
                    {
                        case "Same Month OS Now":
                            {

                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_DashSameMonthOutstanding");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                //DashGovtTran.Reporturl = "http://dotnetonline/ReportServer";
                                DashGovtTran.ReportName = "SameMonthOutZone";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashGovtTran.lstReportParameter = lstReportParameter;
                                DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["SameMonthOutZone"].ToString();
                                //DashGovtTran.ReportPath = @"/Finance Dashboard/NoOfDays/SameMonthOutZone";
                                DashGovtTran.GenerateReport();
                                break;
                            }
                        case "Same Month OS With STU":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_DashSameMonthOutstandSTU");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                //DashGovtTran.Reporturl = "http://dotnetonline/ReportServer";
                                DashGovtTran.ReportName = "SameMonthOutZoneSTU";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashGovtTran.lstReportParameter = lstReportParameter;
                                DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["SameMonthOutZoneSTU"].ToString();
                               // DashGovtTran.ReportPath = @"/Finance Dashboard/NoOfDays/SameMonthOutZoneSTU";
                                DashGovtTran.GenerateReport();
                                break;
                            }
                        case "Same Month OS Without STU":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_DashSameMonthOutstandNonSTU");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                //DashGovtTran.Reporturl = "http://dotnetonline/ReportServer";
                                DashGovtTran.ReportName = "SameMonthOutZoneNonSTU";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashGovtTran.lstReportParameter = lstReportParameter;
                                DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["SameMonthOutZoneNonSTU"].ToString();
                                //DashGovtTran.ReportPath = @"/Finance Dashboard/NoOfDays/SameMonthOutZoneNonSTU";
                                DashGovtTran.GenerateReport();
                                break;
                            }
                        case "Zone Wise OS":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_DashSameMonthOutstandZone");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                //DashGovtTran.Reporturl = "http://dotnetonline/ReportServer";
                                DashGovtTran.ReportName = "outstandingSameMonthZone";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashGovtTran.lstReportParameter = lstReportParameter;
                                DashGovtTran.ReportPath = System.Configuration.ConfigurationManager.AppSettings["outstandingSameMonthZone"].ToString();
                                //DashGovtTran.ReportPath = @"/Finance Dashboard/NoOfDays/outstandingSameMonthZone";
                                DashGovtTran.GenerateReport();
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
