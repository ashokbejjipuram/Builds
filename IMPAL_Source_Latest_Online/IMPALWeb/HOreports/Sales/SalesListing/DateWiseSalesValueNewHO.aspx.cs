using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using System.IO;
using ClosedXML.Excel;
using System.Collections;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class DateWiseSalesValueNewHO : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        int size = 0; 
        string strFromDate = default(string);
        string strToDate = default(string);
        string strReportType = default(string);
        ArrayList myList = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    var startDate = DateTime.Now;
                    var startofMonth = new DateTime(startDate.Year, startDate.Month, 1);
                    txtFromDate.Text = startofMonth.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    reportViewerHolder.Visible = false;
                    btnExport.Visible = false;
                }

                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (btnReport.Text == "Generate Report")
                {
                    btnReport.Text = "Back";
                    reportViewerHolder.Visible = true;
                    btnExport.Visible = true;

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    BindReport();
                    PnlreportFilters.Enabled = false;
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("DateWiseSalesValueNewHO.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #region Grid

        private void BindReport()
        {
            DataTable dt;
            if (ViewState["ReportData"] == null)
            { 
                ReportsData reportsData = new ReportsData();
                List<ProcParam> lstparam = new List<ProcParam>();
                lstparam.Add(new ProcParam("@ReportType", DbType.String, strReportType));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, ddlBranch.SelectedValue));
                lstparam.Add(new ProcParam("@From_Date", DbType.String, strFromDate));
                lstparam.Add(new ProcParam("@To_Date", DbType.String, strToDate));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_Datewisesalesvalue_New_HO]", lstparam); 
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdHODatewiseValue.PageSize = size;
            }
            
            grdHODatewiseValue.DataSource = dt;
            grdHODatewiseValue.DataBind();
            ViewState["ReportData"] = dt;
            int rowcount = dt.Rows.Count;
            lblTotalRecords.Text = rowcount.ToString();
            if (rowcount == 0)
                btnExport.Visible = false;
        }

        protected void grdHODatewiseValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHODatewiseValue.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void ddlpagelimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdHODatewiseValue.PageSize = size;
                BindReport();
            }
        }

        protected void grdHODatewiseValue_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("DateWiseSalesValue_HO");
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    l_type = e.Row.Cells[i].Text.Trim();
                    myList.Add(Array.Exists(array, element => element == l_type));
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal result;
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if ((bool)myList[i])
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        if (decimal.TryParse(e.Row.Cells[i].Text, out result))
                        {
                            e.Row.Cells[i].Text = result.ToString("0.00");
                        }
                    }
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            grdHODatewiseValue.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdHODatewiseValue, "DateWiseSales_HO"); 
        }     

        #endregion
    }
}
