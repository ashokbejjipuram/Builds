#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class BalanceSheetHO : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Balance Sheet Page Init Method"); 
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "Balance Sheet Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                {
                    strBranchCode = (string)Session["BranchCode"];
                }

                if (!IsPostBack)
                {
                    if (strBranchCode.Equals("CRP"))
                    {
                        //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        //txtToDate.Text = txtFromDate.Text;

                        txtFromDate.Text = DateTime.Now.ToString("01/04/2019");
                        txtToDate.Text = DateTime.Now.ToString("31/03/2020");

                        chkZone.Checked = false;
                        chkState.Checked = false;
                        chkBranch.Checked = false;

                        Zones oZone = new Zones();
                        ddlZone.DataSource = oZone.GetAllZones();
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, "--All--");
                        ddlZone.Enabled = true;

                        ddlState.Items.Insert(0, "--All--");
                        ddlBranch.Items.Insert(0, "--All--");
                    }
                    else
                    {
                        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtToDate.Text = txtFromDate.Text;

                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                        {
                            ddlZone.Items.Add(oBranchDetails.Zone);
                            ddlState.Items.Add(oBranchDetails.State);
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                        }
                    }

                    LoadReportType();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBalanceSheet != null)
            {
                crBalanceSheet.Dispose();
                crBalanceSheet = null;
            }
        }
        protected void crystalReport_Unload(object sender, EventArgs e)
        {
            if (crBalanceSheet != null)
            {
                crBalanceSheet.Dispose();
                crBalanceSheet = null;
            }
        }

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                ddlBranch.SelectedIndex = 0;
                chkZone.Checked = false;
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
                chkZone.Checked = false;
                chkState.Checked = false;
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
                    ddlBranch.DataSource = oBranch.GetAllBranch();
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region Populate Report Type
        protected void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "Balance Sheet Report type Populate Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlreportType.DataSource = oCommon.GetDropDownListValues("ReportType-DtlSumm");
                ddlreportType.DataTextField = "DisplayText";
                ddlreportType.DataValueField = "DisplayValue";
                ddlreportType.DataBind();
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
                    string strFromDate = txtFromDate.Text;
                    string strToDate = txtToDate.Text;
                    string str_head = "";
                    string filename = "";
                    btnReport.Text = "Back";

                    if (ddlreportType.SelectedValue == "Detail")
                        filename = string.Format("{0:yyyyMMdd}", strToDate) + "_BS_Det";
                    else
                        filename = string.Format("{0:yyyyMMdd}", strToDate) + "_BS_Sum";

                    if (ddlBranch.SelectedValue != "--All--")
                        filename = filename + "_" + ddlBranch.SelectedValue.ToString();
                    else if (ddlState.SelectedValue != "--All--" && ddlBranch.SelectedValue == "--All--")
                        filename = filename + "_" + ddlState.SelectedItem.Text.ToString();
                    else if (ddlZone.SelectedValue != "--All--" && ddlState.SelectedValue == "--All--" && ddlBranch.SelectedValue == "--All--")
                        filename = filename + "_" + ddlZone.SelectedItem.Text.ToString();
                    else
                        filename = filename + "_CRP";

                    filename = filename + ".xls";

                    Database ImpalDB = DataAccess.GetDatabaseBackUp();
                    DataSet ds = new DataSet();
                    DbCommand cmd;

                    if (ddlreportType.SelectedValue == "Detail")
                        cmd = ImpalDB.GetStoredProcCommand("usp_balsheetdetail_report");
                    else
                        cmd = ImpalDB.GetStoredProcCommand("usp_balsheetsummary_report");

                    ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.String, ddlZone.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@State_Code", DbType.String, ddlState.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, ddlBranch.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@Login_Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@From_Date", DbType.String, strFromDate);
                    ImpalDB.AddInParameter(cmd, "@To_Date", DbType.String, strToDate);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    string strBranchName = (string)Session["UserName"];

                    if (ddlreportType.SelectedValue == "Detail")
                        str_head = "<center><b><font size='6'>Balance Sheet Details for the period ended " + txtToDate.Text + "</font></b><br><br></center>";
                    else
                        str_head = "<center><b><font size='6'>Balance Sheet Summary for the period ended " + txtToDate.Text + "</font></b><br><br></center>";

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    Response.ContentType = "application/ms-excel";
                    Response.Write(str_head);
                    Response.Write("<table border='1' style='font-family:arial;font-size:14px'>");
                    if (ddlZone.SelectedIndex > 0)
                    {
                        Response.Write("<tr><td><b>Zone Name : " + ddlZone.SelectedItem.Text + "</b></td></tr>");
                    }
                    if (ddlState.SelectedIndex > 0)
                    {
                        Response.Write("<tr><td><b>State Name : " + ddlState.SelectedItem.Text + "</b></td></tr>");
                    }
                    if (ddlBranch.SelectedIndex > 0)
                    {
                        Response.Write("<tr><td><b>Branch Name : " + ddlBranch.SelectedItem.Text + "</b></td></tr>");
                    }

                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Response.Write("<tr>");
                        DataRow row = ds.Tables[0].Rows[i];
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            Response.Write("<td>" + row[j].ToString() + "</td>");
                        }

                        Response.Write("</tr>");
                    }

                    Response.Write("</table>");
                }
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
