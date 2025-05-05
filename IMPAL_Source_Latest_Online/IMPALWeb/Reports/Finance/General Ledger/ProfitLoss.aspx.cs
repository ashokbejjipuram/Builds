#region Namespace Declaration
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using IMPALLibrary.Common;
using System.Web.UI.WebControls;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class ProfitLoss : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init 
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "ProfitLoass Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "ProfitLoass Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crProfitLoss != null)
                    {
                        crProfitLoss.Dispose();
                        crProfitLoss = null;
                    }

                    fnPopulateReportType();

                    Database ImpalDB = DataAccess.GetDatabase();
                    string SSQL = "select TOP 1 From_Date, To_Date from Temp_TB_dates1";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(SSQL);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            txtFromDate.Text = (string)reader[0];
                            txtToDate.Text = (string)reader[1];
                        }
                    }

                    if (strBranchCode.Equals("CRP"))
                    {
                        Zones oZone = new Zones();
                        ddlZone.DataSource = oZone.GetAllZones();
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, string.Empty);
                        ddlZone.Enabled = true;
                    }
                    else
                    {
                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                        {
                            ddlZone.Items.Add(oBranchDetails.Zone);
                            ddlState.Items.Add(oBranchDetails.State);
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                        }
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
            if (crProfitLoss != null)
            {
                crProfitLoss.Dispose();
                crProfitLoss = null;
            }
        }
        protected void crProfitLoss_Unload(object sender, EventArgs e)
        {
            if (crProfitLoss != null)
            {
                crProfitLoss.Dispose();
                crProfitLoss = null;
            }
        }

        #region ddlZone_OnSelectedIndexChanged
        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStates(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, string.Empty);
                }
                else
                    ddlState.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlState_OnSelectedIndexChanged
        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();
                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetStateBasedBranch(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, string.Empty);
                }
                else
                    ddlBranch.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report Type
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "ProfitLoass Populate Report type Method"); 
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("ReportType-DtlSumm");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
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
                    string sessionBranch = string.Empty;
                    string selectionformula = string.Empty;
                    string strFromDate = txtFromDate.Text;
                    string strToDate = txtToDate.Text;
                    string str_head = "";
                    string filename = "";
                    btnReport.Text = "Back";

                    if (ddlReportType.SelectedValue == "Detail")
                        filename = string.Format("{0:yyyyMMdd}", strToDate) + "_PL_Det";
                    else
                        filename = string.Format("{0:yyyyMMdd}", strToDate) + "_PL_Sum";

                    if (ddlBranch.SelectedValue != "")
                        filename = filename + "_" + ddlBranch.SelectedValue.ToString();
                    else if (ddlState.SelectedValue != "" && ddlBranch.SelectedValue == "")
                        filename = filename + "_" + ddlState.SelectedItem.Text.ToString();
                    else if (ddlZone.SelectedValue != "" && ddlState.SelectedValue == "" && ddlBranch.SelectedValue == "")
                        filename = filename + "_" + ddlZone.SelectedItem.Text.ToString();
                    else
                        filename = filename + "_CRP";

                    filename = filename + ".xls";

                    Database ImpalDB = DataAccess.GetDatabaseBackUp();
                    DataSet ds = new DataSet();
                    DbCommand cmd;

                    if (ddlReportType.SelectedValue == "Detail")
                        cmd = ImpalDB.GetStoredProcCommand("usp_PLDetail_report");
                    else
                        cmd = ImpalDB.GetStoredProcCommand("usp_PLSummary_report");

                    ImpalDB.AddInParameter(cmd, "@Zone_Code", DbType.String, ddlZone.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@State_Code", DbType.String, ddlState.SelectedValue);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, ddlBranch.SelectedValue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ds = ImpalDB.ExecuteDataSet(cmd);

                    string strBranchName = (string)Session["UserName"];
                    str_head = "<center><b><font size='6'>Profit & Loss Account for the period ended " + txtToDate.Text + "</font></b><br><br></center>";

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
                            Response.Write("<td>" + row[j] + "</td>");
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

