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
    public partial class StockLedger : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    if (Session["BranchCode"] != null)
                        strBranchCode = (string)Session["BranchCode"];

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
                Log.WriteException(typeof(StockLedger), exp);
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Enabled = false;
                //string MonthYear = ddlMonthYear.SelectedValue;
                string Fromdate = txtFromDate.Text;
                string ToDate = txtToDate.Text;

                DataSet ds = new DataSet();
                string str_head = "";
                btnReport.Text = "Back";
                string filename = "StockLedger_" + Session["BranchCode"].ToString() + "_" + Fromdate.Replace("/","") + "-" + ToDate.Replace("/", "") + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetStockLedgerReport(Session["BranchCode"].ToString(), ddlSupplier.SelectedValue, ddlSupplierPartNo.SelectedValue, Fromdate, ToDate);
                string strBranchName = salesItem.GetBranchName(Session["BranchCode"].ToString());
                str_head = "<center><b><font size='6'> Stock Ledger for the Period Between " + Fromdate + " - " + ToDate + " of " + strBranchName + "</font></b><br><br></center>";

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