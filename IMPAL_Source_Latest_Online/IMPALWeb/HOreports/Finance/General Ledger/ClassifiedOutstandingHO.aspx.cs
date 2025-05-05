#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Collections;
using IMPALLibrary.Transactions;
#endregion
namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class ClassifiedOutstandingHO : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        string strReportType = default(string);
        ArrayList myList = new ArrayList();

        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    Zones oZone = new Zones();
                    ddlZone.DataSource = oZone.GetAllZones();
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, "--All--");
                    ddlZone.Enabled = true;

                    ddlState.Items.Insert(0, "--All--");
                    ddlBranch.Items.Insert(0, "--All--");

                    var startDate = DateTime.Now;
                    var startofMonth = new DateTime(startDate.Year, startDate.Month, 1);
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    reportViewerHolder.Visible = false;
                }

                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                reportViewerHolder.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();

                if (ddlZone.SelectedIndex > 0)
                {
                    chkZone.Checked = true;
                    chkZone.Enabled = false;
                }
                else
                {
                    chkZone.Checked = false;
                    chkZone.Enabled = true;
                }

                chkState.Checked = false;
                chkBranch.Checked = false;

                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStatesOnline(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetAllStatesOnline();
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();

                if (ddlState.SelectedIndex > 0)
                {
                    chkState.Checked = true;
                    chkState.Enabled = false;
                }
                else
                {
                    chkState.Checked = false;
                    chkState.Enabled = true;
                }

                chkBranch.Checked = false;

                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchStateOnline(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchNew();
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    chkBranch.Checked = true;
                    chkBranch.Enabled = false;
                }
                else
                {
                    chkBranch.Checked = false;
                    chkBranch.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);

                if (btnReport.Text == "Back")
                {
                    if (chkBranch.Checked || chkState.Checked || chkZone.Checked)
                    {
                        if (chkZone.Checked && !chkState.Checked && !chkBranch.Checked && ddlZone.SelectedIndex > 0)
                        {
                            strReportType = "ZoneWise";
                        }
                        else if (chkZone.Checked && chkState.Checked && !chkBranch.Checked && ddlZone.SelectedIndex > 0)
                        {
                            strReportType = "StateWise";
                        }
                        else if (chkZone.Checked && chkState.Checked && chkBranch.Checked && ddlZone.SelectedIndex > 0)
                        {
                            strReportType = "BranchWise";
                        }
                        else if (chkZone.Checked && !chkState.Checked && !chkBranch.Checked && ddlZone.SelectedIndex == 0)
                        {
                            strReportType = "AllZone";
                        }
                        else if (!chkZone.Checked && chkState.Checked && !chkBranch.Checked && ddlZone.SelectedIndex == 0)
                        {
                            strReportType = "AllState";
                        }
                        else if (!chkZone.Checked && !chkState.Checked && chkBranch.Checked && ddlZone.SelectedIndex == 0)
                        {
                            strReportType = "AllBranch";
                        }
                    }
                    else
                    {
                        strReportType = "All";
                    }

                    BindReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region Grid

        private void BindReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                string strdate = txtToDate.Text.ToString();

                DataSet ds = new DataSet();
                string str_head = "";
                btnReport.Text = "Back";
                string filename = "ClassifiedOS_" + strReportType + "_" + strdate.Replace("/", "") + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetClassifiedOutstandingReportHO(strReportType, ddlZone.SelectedValue, ddlState.SelectedValue, ddlBranch.SelectedValue, strdate);
                string strBranchName = salesItem.GetBranchName(Session["BranchCode"].ToString());
                str_head = "<center><b><font size='6'>Classified Outstanding Report as on " + string.Format("{0:dd/MM/yyyy}", strdate) + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1'><tr>");

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                }
                Response.Write("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Response.Write("<tr>");
                    DataRow row = ds.Tables[0].Rows[i];
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {

                        Response.Write("<td>" + row[j] + "</td>");
                    }
                    Response.Write("</tr>");
                }

                Response.Write("</table>");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                Response.Flush();
                Response.End();
                Response.Close();
            }
        }
        #endregion
    }
}