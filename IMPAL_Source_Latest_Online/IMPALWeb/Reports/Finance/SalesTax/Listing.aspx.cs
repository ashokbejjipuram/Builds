#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common; 
#endregion

namespace IMPALWeb.Reports.Finance.SalesTax
{
    public partial class Listing : System.Web.UI.Page
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
                    if (crListing != null)
                    {
                        crListing.Dispose();
                        crListing = null;
                    }

                    LoadReportType();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
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
            if (crListing != null)
            {
                crListing.Dispose();
                crListing = null;
            }
        }
        protected void crListing_Unload(object sender, EventArgs e)
        {
            if (crListing != null)
            {
                crListing.Dispose();
                crListing = null;
            }
        }

        protected void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();

                int branchcnt = oCommon.GetDelhiBranches(Session["BranchCode"].ToString());
                if (branchcnt > 0)
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("ReportType-STLisitng-Delhi");
                else
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("ReportType-STLisitng");

                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

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
                    string strSelectionFormula = null;
                    string strDateQuery = null;
                    string strBranchQuery = null;
                    string strFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null).ToString("yyyy,MM,dd");
                    string strToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null).ToString("yyyy,MM,dd");
                    string strsel1 = string.Empty;

                    if (ddlReportType.SelectedValue == "Report")
                    {
                        crListing.ReportName = "STListing";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.SalesTaxSummary(strBranchCode, txtFromDate.Text, txtToDate.Text);

                        strDateQuery = "{sales_order_header.document_date}";
                        strBranchQuery = "left({sales_order_header.Customer_Code},3)";
                        strsel1 = "{sales_order_header.Status}<>'I'";
                    }
                    else if (ddlReportType.SelectedValue == "VAT")
                    {
                        crListing.ReportName = "STListing_VAT";

                        IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                        oFinance.SalesTaxSummary(strBranchCode, txtFromDate.Text, txtToDate.Text);

                        strDateQuery = "{serial_number.Document_date}";
                        strBranchQuery = "{serial_number.Branch_Code}";
                    }
                    else
                    {
                        if (ddlReportType.SelectedValue == "DVAT")
                        {
                            crListing.ReportName = "STListing_DVAT";

                            IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                            oFinance.VATSalesDetails(strBranchCode, txtFromDate.Text, txtToDate.Text);
                        }
                        else if (ddlReportType.SelectedValue == "DVATD")
                        {
                            crListing.ReportName = "STListing_DVATD";

                            IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                            oFinance.VATSalesDetails(strBranchCode, txtFromDate.Text, txtToDate.Text);
                        }
                        else if (ddlReportType.SelectedValue == "VATC")
                        {
                            crListing.ReportName = "STListing_VATC";

                            IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                            oFinance.VATSalesDetails(strBranchCode, txtFromDate.Text, txtToDate.Text);
                        }
                        else if (ddlReportType.SelectedValue == "CNS")
                        {
                            crListing.ReportName = "STListing_CNS";

                            IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                            oFinance.VATSalesDetailsCN(strBranchCode, txtFromDate.Text, txtToDate.Text);
                        }

                        strDateQuery = "{vat09_detail.document_date}";
                        strBranchQuery = "{vat09_detail.branch_code}";
                    }

                    strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                           + strDateQuery + " <= Date (" + strToDate + ")";

                    if (!strBranchCode.Equals("CRP"))
                        strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

                    if (ddlReportType.SelectedValue == "Report")
                    {
                        strSelectionFormula = strSelectionFormula + " and " + strsel1;
                    }

                    crListing.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crListing.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crListing.RecordSelectionFormula = strSelectionFormula;
                    crListing.GenerateReportHO();
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
