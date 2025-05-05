#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using IMPALLibrary.Transactions;
using System.Globalization;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class Branch_Item_Surplus_List : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        SupplierItemSearch supp = new SupplierItemSearch();
        static List<SupplierDetails> SupplierDetails;
        Suppliers objsupplier = new Suppliers();
        private string m_SupplierType = string.Empty;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (strBranchCode.Equals("CRP"))
                    {
                        Zones oZone = new Zones();
                        ddlZone.DataSource = oZone.GetAllZones();
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, string.Empty);
                    }
                    else
                    {
                        Branches oBranches = new Branches();
                        fnPopulateZone();
                    }
                    Suppliers oSuppliers = new Suppliers();
                    ddlSupplier.DataSource = oSuppliers.GetAllSuppliersSurplus(strBranchCode);
                    ddlSupplier.DataBind();
                    ddlSupplier.Items.Insert(1, "ALL");

                    string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                    string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                    string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);

                    SupplierDetails = supp.GetSupplierDetailsSurplus(ddlSupplier.SelectedValue, SupplierType == "0" ? "1" : "2", ddlBranch.SelectedValue);
                    Session["ItemPartSupplierline"] = ddlSupplier.SelectedValue;
                    Session["SupplierDetails"] = SupplierDetails;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnAutoComple_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
                var result = SupplierDetails.Find(p => p.Supplier_Part_Number == txtSupplierPartNumber.Text);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void fnPopulateZone()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Zones zn = new IMPALLibrary.Zones();
                ddlZone.DataSource = zn.GetAllZones();
                ddlZone.DataTextField = "ZoneName";
                ddlZone.DataValueField = "ZoneCode";
                ddlZone.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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
                    ddlState.Items.Insert(0, new ListItem("", "0"));
                }
                else
                    ddlState.Enabled = false;

                string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
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
                    ddlBranch.Items.Insert(0, new ListItem("", "0"));
                }
                else
                    ddlBranch.Enabled = false;

                string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlSupplier.Items.Clear();
                if (ddlBranch.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Suppliers osuppliers = new Suppliers();
                    ddlSupplier.DataSource = osuppliers.GetSupplierBasedBranch(ddlBranch.SelectedValue);
                    ddlSupplier.DataBind();
                }
                else
                    ddlSupplier.Enabled = false;

                string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        public string SupplierType
        {
            get
            {
                if (ViewState["SupplierType"] != null)
                { m_SupplierType = Convert.ToString(ViewState["SupplierType"]); };
                return m_SupplierType;
            }
            set { ViewState["SupplierType"] = value; }

        }

        #region ddlSupplier_OnSelectedIndexChanged
        protected void ddlSupplier_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string PartNumberClientID = "ctl00_CPHDetails_txtSupplierPartNumber";
                string AutoCompleteBtnClientID = "ctl00_CPHDetails_btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);

                SupplierDetails = supp.GetSupplierDetailsSurplus(ddlSupplier.SelectedValue, SupplierType == "0" ? "1" : "2", ddlBranch.SelectedValue);
                Session["ItemPartSupplierline"] = ddlSupplier.SelectedValue;
                Session["SupplierDetails"] = SupplierDetails;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
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

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();

                string str_head = "";
                btnReport.Text = "Back";
                string filename = string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)) + "SurPlus_" + strBranchCode + ".xls";

                SalesTransactions salesItem = new SalesTransactions();

                ds = salesItem.GetSurplusDetails(ddlZone.SelectedValue, ddlState.SelectedValue, ddlBranch.SelectedValue, ddlSupplier.SelectedValue, txtSupplierPartNumber.Text);
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Surplus Report for the Day " + System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'>");

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
