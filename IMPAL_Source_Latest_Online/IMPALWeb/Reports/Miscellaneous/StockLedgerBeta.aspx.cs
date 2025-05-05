#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using System.Data.Common;
using System.Data;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Miscellaneous
{
    public partial class StockLedgerBeta : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                    Session.Remove("CrystalReport");
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
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    Session.Remove("CrystalReport");
                    //fnPopulateMonthYear();
                }

                string[] branches = { "MDU", "EKM", "KTM", "MGT", "JR1", "JOD", "UDA", "CAL", "GUW", "PAT", "SBR", "IND" };

                if (branches.Contains(strBranchCode.ToUpper()))
                    btnReport.Visible = true;
                else
                    btnReport.Visible = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        //public void fnPopulateMonthYear()
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    //Log.WriteLog(Source, "fnPopulateMonthYear()", "Statement Of Account Populate Monthyear Method");
        //    try
        //    {
        //        IMPALLibrary.Masters.Item_wise_sales objItem = new IMPALLibrary.Masters.Item_wise_sales();
        //        ddlMonthYear.DataSource = objItem.GetMonthYearStockLedger(strBranchCode);
        //        ddlMonthYear.DataValueField = "month_year";
        //        ddlMonthYear.DataTextField = "month_year";
        //        ddlMonthYear.DataBind();
        //    }
        //    catch (Exception exp)
        //    {
        //        IMPALLibrary.Log.WriteException(Source, exp);
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNo.Visible = false;
                    btnSearch.Text = "Search";
                    txtSupplierPartNo.Visible = true;
                    txtSupplierPartNo.Text = "";
                }
                else
                {
                    StockTransferTransactions stTransactions = new StockTransferTransactions();
                    ddlSupplierPartNo.DataSource = (object)stTransactions.GetItemListBySupplierReceipt(ddlSupplier.SelectedItem.Value, txtSupplierPartNo.Text, "");
                    ddlSupplierPartNo.DataTextField = "ItemDesc";
                    ddlSupplierPartNo.DataValueField = "ItemCode";
                    ddlSupplierPartNo.DataBind();

                    ddlSupplierPartNo.Visible = true;
                    txtSupplierPartNo.Visible = false;
                    btnSearch.Text = "Reset";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockLedgerBeta), exp);
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    btnReport.Enabled = false;
                    //string MonthYear = ddlMonthYear.SelectedValue;
                    string date = txtDate.Text;

                    DataSet ds = new DataSet();
                    string str_head = "";
                    btnReport.Text = "Back";
                    string filename = "StockLedger_" + Session["BranchCode"].ToString() + "_" + date + ".xls";

                    SalesTransactions salesItem = new SalesTransactions();
                    ds = salesItem.GetStockLedgerReport(Session["BranchCode"].ToString(), ddlSupplier.SelectedValue, ddlSupplierPartNo.SelectedValue, txtDate.Text);
                    string strBranchName = salesItem.GetBranchName(Session["BranchCode"].ToString());
                    str_head = "<center><b><font size='6'> Stock Ledger for the Date " + date + " of " + strBranchName + "</font></b><br><br></center>";

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
                    Response.End();
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
