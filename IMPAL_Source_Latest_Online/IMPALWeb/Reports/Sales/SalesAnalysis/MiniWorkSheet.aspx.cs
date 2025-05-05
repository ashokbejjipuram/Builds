#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
using System.Data;
using IMPALLibrary.Transactions;
using System.Globalization;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class MiniWorkSheet : System.Web.UI.Page
    {
        private string pstrBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BranchCode"] != null)
                pstrBranchCode = Session["BranchCode"].ToString();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();

                string str_head = "";
                btnReport.Text = "Back";
                string filename = string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)) + "_MiniWorkSheet_" + ddlLineCode.SelectedValue.ToString() + ".xls";

                SalesTransactions salesItem = new SalesTransactions();

                ds = salesItem.GetMiniWorkSheetDetails(pstrBranchCode, ddlLineCode.SelectedValue.ToString());
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Mini WorkSheet for the Day " + System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr><td colspan=5><b>'" + ddlLineCode.SelectedItem.Text.ToString() + "'</b></td></tr><tr>");

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
    }
}
