#region Namespace Declaration
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.Common;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
#endregion

namespace IMPALWeb.Reports.Ordering.OOOH
{
    public partial class Quarterly_Comparison_Line_Branch : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crlinebranch != null)
                    {
                        crlinebranch.Dispose();
                        crlinebranch = null;
                    }

                    XmlDocument docs = new XmlDocument();
                    docs.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));
                    docs.SelectSingleNode("//root/rootitem[@Type='From Month']");
                    XmlNodeList xlist = (docs.SelectNodes("root/rootnode[@Type='Month']/rootitem"));

                    foreach (XmlNode xNode in xlist)
                    {
                        ddfrommonth.Items.Add(new ListItem(xNode.Attributes["Description"].Value.ToString(), xNode.Attributes["Value"].Value.ToString()));
                        ddtomonth.Items.Add(new ListItem(xNode.Attributes["Description"].Value.ToString(), xNode.Attributes["Value"].Value.ToString()));

                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crlinebranch != null)
            {
                crlinebranch.Dispose();
                crlinebranch = null;
            }
        }
        protected void crlinebranch_Unload(object sender, EventArgs e)
        {
            if (crlinebranch != null)
            {
                crlinebranch.Dispose();
                crlinebranch = null;
            }
        }

        #region Report Button Click

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string sel1 = "{line_wise_sales2.frommonth}=";
                    string sel2 = "{line_wise_sales2.branch_code}=";
                    string selectionformula = string.Empty;
                    string value3 = string.Empty;

                    string frommonthyear = ddfrommonth.SelectedValue.ToString() + txtyear.Text;
                    string tomonthyear = ddtomonth.SelectedValue.ToString() + txtyear.Text;

                    if (Convert.ToInt32(ddtomonth.SelectedValue.Trim()) < 10)
                    {
                        value3 = "0" + (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }
                    else
                    {
                        value3 = (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }

                    if (Convert.ToInt32(ddtomonth.SelectedValue.Trim()) - 1 < 10)
                    {
                        value3 = "0" + (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }
                    else
                    {
                        value3 = (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addlinewise_sales1");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@month_year1", DbType.String, frommonthyear);
                    ImpalDB.AddInParameter(cmd, "@month_year2", DbType.String, value3);
                    ImpalDB.AddInParameter(cmd, "@month_year3", DbType.String, tomonthyear);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (sessionvalue == "CRP")
                    {
                        selectionformula = sel1 + "'" + frommonthyear + "'";
                    }
                    else
                    {
                        selectionformula = sel1 + "'" + frommonthyear + "' and " + sel2 + "'" + sessionvalue + "'";
                    }
                    crlinebranch.RecordSelectionFormula = selectionformula;
                    crlinebranch.ReportName = "Impal_QtrOOOHline";
                    crlinebranch.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}