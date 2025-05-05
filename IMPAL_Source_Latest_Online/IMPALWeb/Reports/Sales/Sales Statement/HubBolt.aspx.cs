#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary; 
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class HubBolt : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        ImpalLibrary oLib = new ImpalLibrary();
        #endregion

        #region Page_Init
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
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crHubBolt != null)
                    {
                        crHubBolt.Dispose();
                        crHubBolt = null;
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crHubBolt != null)
            {
                crHubBolt.Dispose();
                crHubBolt = null;
            }
        }
        protected void crHubBolt_Unload(object sender, EventArgs e)
        {
            if (crHubBolt != null)
            {
                crHubBolt.Dispose();
                crHubBolt = null;
            }
        }

        private void LoadApplSegmentDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ApplicationSegments oApplSeg = new ApplicationSegments();
                ddlSegmentType.DataSource = oApplSeg.GetAllApplicationSegments();
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
        private void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-HubBoltSalesStmt");
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
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlReportType.SelectedValue.Equals("CustomerWise"))
                        crHubBolt.ReportName = "HubBolt-Customer";
                    else if (ddlReportType.SelectedValue.Equals("StockWise"))
                        crHubBolt.ReportName = "HubBolt-Stock";

                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    #region Declaration
                    string strSelectionFormula = null;
                    string strDateQuery = "{V_SalesReports.document_date}";
                    string strBranchQuery = "{V_SalesReports.branch_code}";
                    string strApplSegQuery = "{Item_Master.Application_Segment_Code}";
                    string strConsignmentBranchQuery = "{Consignment.Branch_Code}";
                    #endregion

                    #region Selction Formula Formation
                    if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    {
                        #region StockWise
                        if (ddlReportType.SelectedValue.Equals("StockWise"))
                        {
                            if (strBranchCode.Equals("CRP"))
                            {
                                if (ddlSegmentType.SelectedIndex > 0)
                                    strSelectionFormula = strApplSegQuery + " = '" + ddlSegmentType.SelectedValue + "'";
                            }
                            else
                            {
                                if (ddlSegmentType.SelectedIndex > 0)
                                    strSelectionFormula = strApplSegQuery + " = '" + ddlSegmentType.SelectedValue
                                        + "' and " + strConsignmentBranchQuery + " = '" + strBranchCode + "'";
                                else
                                    strSelectionFormula = strConsignmentBranchQuery + " = '" + strBranchCode + "'";
                            }
                        }
                        #endregion

                        #region Part# and CustomerWise
                        else
                        {
                            string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                            string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                            string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                                   + strDateQuery + " <= Date (" + strToDate + ")";
                            if (strBranchCode.Equals("CRP"))
                            {
                                if (ddlSegmentType.SelectedIndex > 0)
                                    strSelectionFormula = strDateCompare + " and " + strApplSegQuery + " = '" + ddlSegmentType.SelectedValue + "'";
                                else
                                    strSelectionFormula = strDateCompare;
                            }
                            else
                            {
                                if (ddlSegmentType.SelectedIndex > 0)
                                    strSelectionFormula = strDateCompare + " and " + strApplSegQuery + " = '" + ddlSegmentType.SelectedValue
                                        + "' and " + strBranchQuery + " = '" + strBranchCode + "'";
                                else
                                    strSelectionFormula = strDateCompare + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                            }
                        }
                        #endregion

                    }
                    #endregion

                    crHubBolt.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crHubBolt.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crHubBolt.RecordSelectionFormula = strSelectionFormula;
                    crHubBolt.GenerateReport();
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
