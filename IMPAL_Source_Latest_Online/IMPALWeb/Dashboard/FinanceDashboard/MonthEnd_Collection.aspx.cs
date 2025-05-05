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
    public partial class MonthEnd_Collection : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CommonDashboard obj = new CommonDashboard();
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport1, dashReportTable, dashReportHolder1);
                if (btnReport1.Text == "Back")
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    switch (ddlReportList1.SelectedValue.Trim())
                    {
                        case "REMIITANCE":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_Remittance");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                DashMonthEndCollection1.ReportName = "MONTH END REMIITANCE";
                                //DashMonthEndCollection1.Reporturl = "http://dotnetonline/ReportServer";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashMonthEndCollection1.lstReportParameter = lstReportParameter;
                                DashMonthEndCollection1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["MONTHENDREMIITANCE"].ToString();
                                DashMonthEndCollection1.GenerateReport();
                                break;
                            }
                        case "COLLECTION ZONE WISE":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_MonthEndCollectionZonewise");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                DashMonthEndCollection1.ReportName = "MONTH END COLLECTION ZONE WISE";
                                //DashMonthEndCollection1.Reporturl = "http://dotnetonline/ReportServer";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashMonthEndCollection1.lstReportParameter = lstReportParameter;
                                DashMonthEndCollection1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["MONTHENDCOLLECTIONZONEWISE"].ToString();
                                DashMonthEndCollection1.GenerateReport();
                                break;
                            }
                        case "CUMULATIVE COLLECTION %":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_MonthEndCumulativeCollection");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                DashMonthEndCollection1.ReportName = "MONTH END CUMULATIVE COLLECTION %";
                                //DashMonthEndCollection1.Reporturl = "http://dotnetonline/ReportServer";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashMonthEndCollection1.lstReportParameter = lstReportParameter;
                                DashMonthEndCollection1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["MONTHENDCUMULATIVECOLLECTION%"].ToString();
                                DashMonthEndCollection1.GenerateReport();
                                break;
                            }
                        case "MONTH END COLLECTION ASC":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_MonthEndCollectionZonewise");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                DashMonthEndCollection1.ReportName = "MONTH END COLLECTION ASC";
                                //DashMonthEndCollection1.Reporturl = "http://dotnetonline/ReportServer";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashMonthEndCollection1.lstReportParameter = lstReportParameter;
                                DashMonthEndCollection1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["MONTHENDCOLLECTIONASC"].ToString();
                                DashMonthEndCollection1.GenerateReport();
                                break;
                            }
                        case "CUMULATIVE COLLECTION ASC":
                            {
                                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Dash_MonthEndCumulativeCollection");
                                ImpalDB.AddInParameter(cmd, "@Date_period", DbType.String, txtDatePick.Text);
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDB.ExecuteNonQuery(cmd);
                                DashMonthEndCollection1.ReportName = "MONTH END CUMULATIVE COLLECTION ASC";
                                //DashMonthEndCollection1.Reporturl = "http://dotnetonline/ReportServer";
                                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                                DashMonthEndCollection1.lstReportParameter = lstReportParameter;
                                DashMonthEndCollection1.ReportPath = System.Configuration.ConfigurationManager.AppSettings["MONTHENDCUMULATIVECOLLECTIONASC"].ToString();
                                DashMonthEndCollection1.GenerateReport();
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

      
    }
}
