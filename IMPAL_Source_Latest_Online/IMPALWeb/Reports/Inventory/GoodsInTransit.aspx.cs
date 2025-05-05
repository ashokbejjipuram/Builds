#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using IMPALLibrary.Transactions;
using System.Globalization;

#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class SupplierDespatchDetails : System.Web.UI.Page
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
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
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
        }

        #endregion

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    DataSet ds = new DataSet();

                    string str_head = "";
                    btnReport.Text = "Back";
                    string filename = string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)) + "GoodsInTransit.xls";

                    SalesTransactions salesItem = new SalesTransactions();

                    ds = salesItem.GetSupplierDespatchDetails(strBranchCode, ddlSuppLine.SelectedValue);
                    string strBranchName = (string)Session["BranchName"];
                    str_head = "<center><b><font size='6'>Goods In Transit Details Report for the Day " + System.DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " of " + strBranchName + "</font></b><br><br></center>";

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
