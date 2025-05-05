#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Finance.GSTReports
{
    public partial class GSTOutwardRegisterHO : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Sales Journal Page Init Method");
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
            //Log.WriteLog(Source, "Page_Load", "Sales Journal Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    Zones oZone = new Zones();
                    ddlZone.DataSource = oZone.GetAllZones();
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, "--All--");
                    ddlZone.Enabled = true;

                    ddlState.Items.Insert(0, "--All--");
                    ddlBranch.Items.Insert(0, "--All--");

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
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
        }

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
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
                    ddlState.Enabled = false;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetAllStatesOnline();
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");;
                }

                ddlState_OnSelectedIndexChanged(ddlState, EventArgs.Empty);
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
                    ddlBranch.Enabled = false;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchGST();
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string date1 = txtFromDate.Text.ToString();
                string date2 = txtToDate.Text.ToString();
                string zone = ddlZone.SelectedValue;
                string state = ddlState.SelectedValue;
                string branch = ddlBranch.SelectedValue;
                string filename = string.Empty;
                DataSet ds = new DataSet();
                //string str_head = "";
                btnReport.Text = "Back";                

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetGSTOutwardRegisterDetailsHO(zone, state, branch, date1, date2);
                string strBranchName = (string)Session["BranchName"];
                //str_head = "<center><b><font size='6'>GST Inward Register for the Period " + string.Format("{0:dd/MM/yyyy}", date1) + " to " + string.Format("{0:dd/MM/yyyy}", date2) + " of " + strBranchName + "</font></b><br><br></center>";

                if (zone == "--All--" && state == "--All--" && branch == "--All--")
                    filename = ds.Tables[1].Rows[0]["GSTIN"].ToString() + "_OutWard_" + ds.Tables[1].Rows[0]["MonthYear"].ToString() + ".xls";
                else if (zone != "--All--" && state == "--All--" && branch == "--All--")
                    filename = ddlZone.SelectedItem.Text + "_OutWard_" + ds.Tables[1].Rows[0]["MonthYear"].ToString() + ".xls";
                else if (zone != "--All--" && state != "--All--" && branch == "--All--")
                    filename = ddlState.SelectedItem.Text + "_OutWard_" + ds.Tables[1].Rows[0]["MonthYear"].ToString() + ".xls";
                else if (zone != "--All--" && state != "--All--" && branch != "--All--")
                    filename = ddlBranch.SelectedItem.Text + "_OutWard_" + ds.Tables[1].Rows[0]["MonthYear"].ToString() + ".xls";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                //Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr>");

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th style='background-color:red;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
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
