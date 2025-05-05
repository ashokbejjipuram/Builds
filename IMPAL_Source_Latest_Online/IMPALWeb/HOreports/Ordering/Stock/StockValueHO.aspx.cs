#region Declaration
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class StockValueHO : System.Web.UI.Page
    {
        string Branchcode = string.Empty;
        string strReportType = default(string);
        ArrayList myList = new ArrayList();
        int size = 0;

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

                    reportViewerHolder.Visible = false;
                    btnExport.Visible = false;
                }

                if (Session["BranchCode"] != null)
                    Branchcode = (string)Session["BranchCode"];
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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
                if (btnReport.Text == "Generate Report")
                {
                    btnReport.Text = "Back";
                    reportViewerHolder.Visible = true;
                    btnExport.Visible = true;

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
                        else
                        {
                            strReportType = "All";
                        }
                    }
                    else
                    {
                        strReportType = "All";
                    }

                    BindReport();
                    PnlreportFilters.Enabled = false;
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("StockValueHO.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region Grid
        private void BindReport()
        {
            DataTable dt;
            if (ViewState["ReportData"] == null)
            {
                ReportsData reportsData = new ReportsData();
                List<ProcParam> lstparam = new List<ProcParam>();
                lstparam.Add(new ProcParam("@ReportType", DbType.String, strReportType));
                lstparam.Add(new ProcParam("@ZoneCode", DbType.String, ddlZone.SelectedValue));
                lstparam.Add(new ProcParam("@StateCode", DbType.String, ddlState.SelectedValue));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, ddlBranch.SelectedValue));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_GetStockValue_HO]", lstparam);
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdStockValue.PageSize = size;
            }

            grdStockValue.DataSource = dt;
            grdStockValue.DataBind();
            ViewState["ReportData"] = dt;
            int rowcount = dt.Rows.Count;
            lblTotalRecords.Text = rowcount.ToString();
            if (rowcount == 0)
                btnExport.Visible = false;
        }

        protected void ddlpagelimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdStockValue.PageSize = size;
                BindReport();
            }
        }

        protected void grdStockValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStockValue.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void grdStockValue_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("StockValue_HO");
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    l_type = e.Row.Cells[i].Text.Trim();
                    myList.Add(Array.Exists(array, element => element == l_type));
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal result;
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if ((bool)myList[i])
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        if (decimal.TryParse(e.Row.Cells[i].Text, out result))
                        {
                            e.Row.Cells[i].Text = result.ToString("0.00");
                        }
                    }
                }
            }
        }
        #endregion

        #region Export
        protected void btnExport_Click(object sender, EventArgs e)
        {
            grdStockValue.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdStockValue, "StockValue_HO");
        }
        #endregion
    }
}
