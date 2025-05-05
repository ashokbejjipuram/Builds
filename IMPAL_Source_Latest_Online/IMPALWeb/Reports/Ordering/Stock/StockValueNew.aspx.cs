#region Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IMPALLibrary;
using ClosedXML.Excel;
using System.IO;
using System.Collections;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class StockValueNew : System.Web.UI.Page
    {
        string strBranchcode = string.Empty;
        private static string strReportName = default(string);
        int size = 0;
        ArrayList myList = new ArrayList();

        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                    reportViewerHolder.Visible = false;

                if (Session["BranchCode"] != null)
                    strBranchcode = (string)Session["BranchCode"];
                if (strBranchcode != "CRP")
                {
                    ddlbranchcode.SelectedValue = strBranchcode.ToString();
                    ddlbranchcode.Enabled = false;
                }
                else
                {
                    ddlbranchcode.Enabled = true;
                }

            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Button_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    strBranchcode = ddlbranchcode.SelectedValue;
                    if (ddlbranchcode.SelectedValue == "0")
                    {
                        strBranchcode = string.Empty;
                    }
                    strReportName = "Stock_Value";
                    BindReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region Grid

        private void BindReport()
        {
            DataTable dt;
            if (ViewState["ReportData"] == null)
            {
                ReportsData reportsData = new ReportsData();
                List<ProcParam> lstparam = new List<ProcParam>();
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, strBranchcode));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_StockValue]", lstparam);
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdStockValue.PageSize = size;
            }

            grdStockValue.DataSource = dt;
            grdStockValue.DataBind();
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
                grdStockValue.PageSize = size;
                BindReport();
            }
        }
        protected void grdStockValue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStockValue.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void grdStockValue_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        { 
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns(strReportName); 
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
            grdStockValue.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdStockValue, strReportName);
        }
        #endregion
        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}
