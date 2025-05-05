#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using IMPALLibrary;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Ordering.StockComparison
{
    public partial class ZoneLineComparison : System.Web.UI.Page
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

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crzoneline != null)
                    {
                        crzoneline.Dispose();
                        crzoneline = null;
                    }

                    fnPopulateMonth();
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
            if (crzoneline != null)
            {
                crzoneline.Dispose();
                crzoneline = null;
            }
        }
        protected void crzoneline_Unload(object sender, EventArgs e)
        {
            if (crzoneline != null)
            {
                crzoneline.Dispose();
                crzoneline = null;
            }
        }

        #region Populate From and To month Dropdown Population
        private void fnPopulateMonth()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
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
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    string selectionformula = string.Empty;
                    string strsel1 = "{line_wise_sales1.frommonth}=";
                    string strsel2 = "{line_wise_sales1.branch_code}=";
                    string value3 = string.Empty;
                    string frommonthyear = ddfrommonth.SelectedValue.ToString() + txtyear.Text;
                    string tomonthyear = ddtomonth.SelectedValue.ToString() + txtyear.Text;

                    if (Convert.ToInt32(ddtomonth.SelectedValue.Trim()) > 9)
                    {
                        value3 = (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }
                    else
                    {
                        value3 = "0" + (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }

                    if (Convert.ToInt32(ddtomonth.SelectedValue.Trim()) - 1 > 9)
                    {
                        value3 = (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }
                    else
                    {
                        value3 = "0" + (Convert.ToInt32(ddtomonth.SelectedValue) - 1).ToString() + txtyear.Text;
                    }

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addlinewise_sales");
                    ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd, "@month_year1", DbType.String, frommonthyear);
                    ImpalDB.AddInParameter(cmd, "@month_year3", DbType.String, tomonthyear);
                    ImpalDB.AddInParameter(cmd, "@month_year2", DbType.String, value3);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    if (sessionvalue == "CRP")
                    {
                        selectionformula = strsel1 + "'" + frommonthyear + "'";
                    }
                    else
                    {
                        selectionformula = strsel1 + "'" + frommonthyear + "' and " + strsel2 + "'" + sessionvalue + "'";

                    }

                    crzoneline.RecordSelectionFormula = selectionformula;
                    crzoneline.ReportName = "zoneline_stockcomp1";
                    crzoneline.GenerateReport();
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