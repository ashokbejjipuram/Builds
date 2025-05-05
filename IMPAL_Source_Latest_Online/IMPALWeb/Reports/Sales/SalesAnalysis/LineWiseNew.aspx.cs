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
using System.Collections;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class LineWiseNew : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        private static string strReportName = default(string);
        int size = 0;
        static string strFromDate = default(string);
        static string strToDate = default(string);
        ArrayList myList = new ArrayList();
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

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    Towns oTowns = new Towns();
                    if (strBranchCode.Equals("CRP"))
                        ddlTownCode.DataSource = oTowns.GetAllTowns(null);
                    else
                        ddlTownCode.DataSource = oTowns.GetAllTowns(strBranchCode);
                    ddlTownCode.DataBind();
                    ddlTownCode.Items.Insert(0, string.Empty);
                    ddlLineCode.Items.Insert(0, string.Empty);
                    PopulateReportType();
                    UpdateAutomobileItems();

                    reportViewerHolder.Visible = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        private void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-ValQtyDtl");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        private void UpdateAutomobileItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Vehicles oVehicles = new Vehicles();
                oVehicles.UpdateAutomobileItem();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlTownCode.SelectedIndex > 0)
                {
                    ddlReportType.Items.Clear();
                    ddlReportType.Items.Add("Town Wise");
                }
                else
                {
                    ddlReportType.Items.Clear();
                    PopulateReportType();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    if (ddlReportType.SelectedValue.Equals("Value"))
                        strReportName = "LineWise";
                    else if (ddlReportType.SelectedValue.Equals("Quantity"))
                        strReportName = "LineWise-Qty";
                    else if (ddlReportType.SelectedValue.Equals("Details"))
                        strReportName = "LineWise-Dtls";
                    else
                        strReportName = "LineWise-TownWise";
                     

                    //DateTime frmDate = CommonDataMembers.GetFormatedDate(hidFromDate.Value);
                    //DateTime toDate = CommonDataMembers.GetFormatedDate(hidToDate.Value);
                    //strFromDate = "'" + frmDate.ToString("yyyy/MM/dd") + "'";
                    //strToDate = "'" + toDate.ToString("yyyy/MM/dd") + "'";
                    //strFromDate = "'" + frmDate.ToString("yyyy,MM,dd") + "'";
                    //strToDate = "'" + toDate.ToString("yyyy,MM,dd") + "'"; 

                    DateTime dtFromDate = Convert.ToDateTime(hidFromDate.Value);
                    DateTime dtToDate = Convert.ToDateTime(hidToDate.Value);  
                    strFromDate = "'" + dtFromDate.ToString("yyyy/MM/dd") + "'";
                    strToDate = "'" + dtToDate.ToString("yyyy/MM/dd") + "'";

                    BindReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlTownCode_IndexChanged(object sender, EventArgs e)
        {
            LoadReportType();
        }


        #region Grid

        private void BindReport()
        {
            DataTable dt;
            if (ViewState["ReportData"] == null)
            {
                ReportsData reportsData = new ReportsData();
                List<ProcParam> lstparam = new List<ProcParam>();
                lstparam.Add(new ProcParam("@ReportType", DbType.String, ddlReportType.SelectedValue));
                lstparam.Add(new ProcParam("@BranchCode", DbType.String, strBranchCode));
                lstparam.Add(new ProcParam("@From_Date", DbType.String, strFromDate));
                lstparam.Add(new ProcParam("@To_Date", DbType.String, strToDate));
                lstparam.Add(new ProcParam("@LineCode", DbType.String, ddlLineCode.SelectedValue));
                lstparam.Add(new ProcParam("@TownCode", DbType.String, ddlTownCode.SelectedValue));

                dt = reportsData.GetTableData("[dbo].[usp_rpt_LineWisePrevYears]", lstparam);
            }
            else
            {
                dt = (DataTable)ViewState["ReportData"];
            }

            if (ddlpagelimit.SelectedItem.Text != "0")
            {
                size = int.Parse(ddlpagelimit.SelectedItem.Value.ToString());
                grdLineWise.PageSize = size;
            }

            grdLineWise.DataSource = dt;
            grdLineWise.DataBind();
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
                grdLineWise.PageSize = size;
                BindReport();
            }
        }
        protected void grdLineWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLineWise.PageIndex = e.NewPageIndex;
            BindReport();
        }
        protected void grdLineWise_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string l_type = "";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string[] array = CommonDataMembers.GetNumericColumns("LineWise");
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
            grdLineWise.AllowPaging = false;
            this.BindReport();

            CommonDataMembers.ExportGridToExcel(grdLineWise, strReportName);
        }
        #endregion 
         
    }
}
