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

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class SalesJournalGST : System.Web.UI.Page
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
                    fnLoadReportType();
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

        #region Report type Dropdown Populate Method
        protected void fnLoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnLoadReportType()", "Sales Journal Report type Dropdown Populate Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("Finance-SalesJournal_GST");
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

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string date1 = txtFromDate.Text.ToString();
                string date2 = txtToDate.Text.ToString();
                DataSet ds = new DataSet();
                string str_head = "";
                btnReport.Text = "Back";
                string filename = "SalesJournal-GST_" + string.Format("{0:yyyyMMdd}", date1) + " to " + string.Format("{0:yyyyMMdd}", date2) + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetSalesJournalExcelDetails(Session["BranchCode"].ToString(), date1, date2);
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Sales Journal Report for the Period " + string.Format("{0:dd/MM/yyyy}", date1) + " to " + string.Format("{0:dd/MM/yyyy}", date2) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr>");

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
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
