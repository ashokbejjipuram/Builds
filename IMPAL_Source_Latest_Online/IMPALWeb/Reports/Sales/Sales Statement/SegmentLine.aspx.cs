#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class SegmentLine : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        ImpalLibrary oLib = new ImpalLibrary();
        #endregion

        #region Page_Init
        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crSegmentLine != null)
                    {
                        crSegmentLine.Dispose();
                        crSegmentLine = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    PopulateReportType();
                    LoadApplSegmentDDL();
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
            if (crSegmentLine != null)
            {
                crSegmentLine.Dispose();
                crSegmentLine = null;
            }
        }
        protected void crSegmentLine_Unload(object sender, EventArgs e)
        {
            if (crSegmentLine != null)
            {
                crSegmentLine.Dispose();
                crSegmentLine = null;
            }
        }

        #region LoadApplSegmentDDL
        /// <summary>
        /// Method to LoadApplSegmentDDL 
        /// </summary>
        private void LoadApplSegmentDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ApplicationSegments oApplSeg = new ApplicationSegments();
                ddlSegmentType.DataSource = oApplSeg.GetAllNewApplicationSegments(strBranchCode);
                ddlSegmentType.DataBind();
                ddlSegmentType.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        /// <summary>
        /// Method to populate Report Type
        /// </summary>
        private void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-SegmentLineSales");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        /// <summary>
        /// Report Button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlReportType.SelectedValue.Equals("MatrixReport"))
                    {
                        //crSegmentLine.ReportName = "SegmentLine-Matrix";
                        CallExcelReport();
                    }
                    else
                        CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void CallExcelReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                DataSet ds = new DataSet();

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                string str_head = "";
                btnReport.Text = "Back";
                string filename = "MatrixReport" + string.Format("{0:yyyyMMdd}", strFromDate) + ".xls";

                SalesTransactions salesItem = new SalesTransactions();

                ds = salesItem.GetSegmentSalesMatrixDetails(Session["BranchCode"].ToString(), strFromDate, strToDate);
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>" + strBranchName + " - Matrix Report from " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " to " + string.Format("{0:dd/MM/yyyy}", strToDate) + "</font></b><br><br></center>";

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

        #region CallCrystalReport
        /// <summary>
        /// Method to call crystal report
        /// </summary>
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    string strSelectionFormula = null;
                    string strDateQuery = "{V_SalesReports_Segment.document_date}";
                    string strBranchQuery = "{V_SalesReports_Segment.Branch_code}";
                    string strApplSegQuery = "{v_new_segment_master.Application_Segment_Code}";
                    string strSupplierQuery = "{V_SalesReports_Segment.Item_Code}";

                    if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    {
                        string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                        string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                        string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                               + strDateQuery + " <= Date (" + strToDate + ")";
                        if (ddlSegmentType.SelectedIndex > 0)
                        {
                            if (ddlLineCode.SelectedIndex > 0)
                                strSelectionFormula = strDateCompare + " and " + strApplSegQuery + " = '" + ddlSegmentType.SelectedValue
                                    + "' and " + strSupplierQuery + " = '" + ddlLineCode.SelectedValue + "'";
                            else
                                strSelectionFormula = strDateCompare + " and " + strApplSegQuery + " = '" + ddlSegmentType.SelectedValue + "'";
                        }
                        else if (ddlLineCode.SelectedIndex > 0)
                            strSelectionFormula = strDateCompare + " and " + strSupplierQuery + " = '" + ddlLineCode.SelectedValue + "'";
                        else
                            strSelectionFormula = strDateCompare;
                        if (!strBranchCode.Equals("CRP"))
                            strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                    }

                    crSegmentLine.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crSegmentLine.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crSegmentLine.RecordSelectionFormula = strSelectionFormula;
                    crSegmentLine.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}