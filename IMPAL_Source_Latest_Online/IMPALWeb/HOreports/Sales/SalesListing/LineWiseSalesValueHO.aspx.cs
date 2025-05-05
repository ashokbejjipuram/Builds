using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using System.IO;
using ClosedXML.Excel;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class LineWiseSalesValueHO : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        int size = 0; 
        string strFromDate = default(string);
        string strToDate = default(string);
        string strReportType = default(string);
        ArrayList myList = new ArrayList();

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
                    txtFromDate.Text = startofMonth.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    reportViewerHolder.Visible = false;
                    btnExport.Visible = false;
                }

                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
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

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

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
                    PnlreportFilters.Enabled = false;
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("LineWiseSalesValueHO.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
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
            DataTable dt;
            if (ViewState["ReportData"] == null)
            { 
                ReportsData reportsData = new ReportsData();
                List<ProcParam> lstparam = new List<ProcParam>();
                lstparam.Add(new ProcParam("@ReportType", DbType.String, strReportType));
                lstparam.Add(new ProcParam("@ZoneCode", DbType.String, ddlZone.SelectedValue));
                lstparam.Add(new ProcParam("@StateCode", DbType.String, ddlState.SelectedValue));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, ddlBranch.SelectedValue));
                lstparam.Add(new ProcParam("@From_Date", DbType.String, strFromDate));
                lstparam.Add(new ProcParam("@To_Date", DbType.String, strToDate));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_LineWisesalesvalue_HO]", lstparam); 
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdHOLineWiseValue.PageSize = size;
            }

            grdHOLineWiseValue.DataSource = dt;
            grdHOLineWiseValue.DataBind();
            ViewState["ReportData"] = dt;
            int rowcount = dt.Rows.Count;
            lblTotalRecords.Text = rowcount.ToString();
            if (rowcount == 0)
                btnExport.Visible = false;
        }

        protected void grdHOLineWiseValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHOLineWiseValue.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void ddlpagelimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdHOLineWiseValue.PageSize = size;
                BindReport();
            }
        }

        protected void grdHOLineWiseValue_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("LineWiseSalesValue_HO");
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            grdHOLineWiseValue.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdHOLineWiseValue, "LineWiseSales_HO"); 
        }     

        #endregion
    }
}
