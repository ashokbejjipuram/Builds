using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Collections;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class DateWiseSalesValueNew : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private static string strReportName = default(string);
        int size = 0;
        string strFromDate = default(string);
        string strToDate = default(string);
        ArrayList myList = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                reportViewerHolder.Visible = false;
            }
            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    strReportName = "DateWiseSalesValue";
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;
                    BindReport(); 
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
                lstparam.Add(new ProcParam("@ReportType", DbType.String, ""));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, strBranchCode));                
                lstparam.Add(new ProcParam("@From_Date", DbType.String, strFromDate));
                lstparam.Add(new ProcParam("@To_Date", DbType.String, strToDate));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_DateWiseSalesValue]", lstparam);
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdDateWiseSalesValue.PageSize = size;
            }

            grdDateWiseSalesValue.DataSource = dt;
            grdDateWiseSalesValue.DataBind();
            ViewState["ReportData"] = dt;
            int rowcount = dt.Rows.Count;
            lblTotalRecords.Text = rowcount.ToString();
            if (rowcount == 0)
                btnExport.Visible = false;
        }

        protected void ddlpagelimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdDateWiseSalesValue.PageSize = size;
                BindReport();
            }
        }

        protected void grdDateWiseSalesValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDateWiseSalesValue.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void grdDateWiseSalesValue_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("DateWiseSalesValue");
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
        #endregion

        #region Export
        protected void btnExport_Click(object sender, EventArgs e)
        {
            grdDateWiseSalesValue.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdDateWiseSalesValue, strReportName);
        }
        #endregion
    }
}
