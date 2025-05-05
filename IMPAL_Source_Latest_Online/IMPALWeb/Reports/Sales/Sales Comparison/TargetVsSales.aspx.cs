#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class TargetVsSales : System.Web.UI.Page
    {
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

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crtarget != null)
                    {
                        crtarget.Dispose();
                        crtarget = null;
                    }

                    fnPopulateReportType();
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
            if (crtarget != null)
            {
                crtarget.Dispose();
                crtarget = null;
            }
        }
        protected void crtarget_Unload(object sender, EventArgs e)
        {
            if (crtarget != null)
            {
                crtarget.Dispose();
                crtarget = null;
            }
        }

        public void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<string> lst = new List<string>();
                lst.Add("Line Wise");
                lst.Add("Town Wise");
                ddlReportType.DataSource = lst;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

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
                    string strFromDate = default(string);
                    DataSet ds = new DataSet();

                    strFromDate = txtDate.Text;

                    string str_head = "";
                    btnReport.Text = "Back";
                    string filename = "TargetVs" + ddlReportType.Text + " Sales" + string.Format("{0:yyyyMMdd}", strFromDate) + ".xls";

                    SalesTransactions salesItem = new SalesTransactions();

                    if (ddlReportType.SelectedIndex == 0)
                        ds = salesItem.GetTargetLineSales(Session["BranchCode"].ToString(), strFromDate);
                    else
                        ds = salesItem.GetTargetTownSales(Session["BranchCode"].ToString(), strFromDate);

                    string strBranchName = (string)Session["BranchName"];
                    str_head = "<center><b><font size='6'>Target Vs " + ddlReportType.Text + " Sales for the Day " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " of " + strBranchName + "</font></b><br><br></center>";

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
