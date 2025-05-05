using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using ClosedXML.Excel;
using System.IO;
using System.Collections;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class Sales_LineWiseSalesNew : System.Web.UI.Page
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
                fnPopulateReportName();

                reportViewerHolder.Visible = false;
            }
            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
            else
                Response.Redirect("~/Login.aspx", false);

        }

        protected void fnPopulateReportName()
        {
            string strFileName = "LineWiseSalesValueReport";
            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strFileName);
                ddlReportType.DataSource = drop;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
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
                    if (ddlReportType.SelectedValue == "Report")
                    { 
                        strReportName = "LineWiseSales_Report";
                    }
                    else if (ddlReportType.SelectedValue == "Summary")
                    { 
                        strReportName = "LineWiseSales_Summary";
                    }

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
                lstparam.Add(new ProcParam("@ReportType", DbType.String, ddlReportType.SelectedValue.ToString()));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, strBranchCode));
                lstparam.Add(new ProcParam("@FromLine", DbType.String, ddlfrmline.SelectedValue.ToString()));
                lstparam.Add(new ProcParam("@ToLine", DbType.String, ddlToline.SelectedValue.ToString()));
                lstparam.Add(new ProcParam("@From_Date", DbType.String,strFromDate));
                lstparam.Add(new ProcParam("@To_Date", DbType.String, strToDate));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_LineWiseSales]", lstparam);
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdLineWiseSales.PageSize = size;
            }

            grdLineWiseSales.DataSource = dt;
            grdLineWiseSales.DataBind();
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
                grdLineWiseSales.PageSize = size;
                BindReport();
            }
        }

        protected void grdLineWiseSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLineWiseSales.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void grdLineWiseSales_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("LineWiseSales");
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
            grdLineWiseSales.AllowPaging = false;
            this.BindReport(); 

           CommonDataMembers.ExportGridToExcel(grdLineWiseSales,strReportName); 
        }
         #endregion
    }
}
