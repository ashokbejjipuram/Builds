#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class StockVariance : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
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
                    if (crStockVariance != null)
                    {
                        crStockVariance.Dispose();
                        crStockVariance = null;
                    }

                    DefineBranch();
                    PopulateReportType();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crStockVariance != null)
            {
                crStockVariance.Dispose();
                crStockVariance = null;
            }
        }
        protected void crStockVariance_Unload(object sender, EventArgs e)
        {
            if (crStockVariance != null)
            {
                crStockVariance.Dispose();
                crStockVariance = null;
            }
        }

        #region Populate Report Type
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("StkVar_ReportType-DtlSumm");
                ddlReportType.DataSource = oList;
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

        #region GenerateSelectionFormula
        public void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = null;

            string str = ddlReportType.Text;
            if (str.Equals("Detail"))
                crStockVariance.ReportName = "StockVariance";
            else
                crStockVariance.ReportName = "StockVarianceSummary";

            if (!string.IsNullOrEmpty(strBranchCode))
            {
                string strFromDate = null;
                string strToDate = null;
                if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                {
                    ImpalLibrary oCommon = new ImpalLibrary();
                    strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                    strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");

                    string strTagDate = "{view_stockvariance.tag_date}";
                    string strDateCompare = strTagDate + " >= Date (" + strFromDate + ") and " + strTagDate + " <= Date (" + strToDate + ")";
                    string strBranchQuery = " and {view_stockvariance.Branch_Code} = '" + strBranchCode + "'";

                    //string strSupplierQuery = " and {view_stockvariance.Supplier_Code} = '" + ddlSupplier.SelectedValue + "'";

                    if (strBranchCode.Equals("CRP"))
                        strSelectionFormula = strDateCompare; //+ strSupplierQuery;
                    else
                        strSelectionFormula = strDateCompare + strBranchQuery; //+strSupplierQuery;
                }
            }

            crStockVariance.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crStockVariance.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crStockVariance.RecordSelectionFormula = strSelectionFormula;

            if (str.Equals("Detail"))
                crStockVariance.GenerateReportAndExport(fileType);
            else
                crStockVariance.GenerateReportAndExportA4(fileType);
        }
        #endregion

        private void DefineBranch()
        {
            if (Session["RoleCode"].ToString() != null && Session["RoleCode"].ToString() != "")
            {
                if (Session["RoleCode"].ToString().ToUpper() == "CORP")
                {
                    ddlBranch.SelectedValue = "0";//Session["BranchCode"].ToString();
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                }
            }
        }

        #region Generate Report
        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("StockVariance.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
