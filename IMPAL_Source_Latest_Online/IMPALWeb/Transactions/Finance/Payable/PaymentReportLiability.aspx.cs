using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary.Common;


namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class PaymentReportLiability : System.Web.UI.Page
    {
        string pstrBranchCode = default(string);
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    pstrBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crBMSLiabilty != null)
                    {
                        crBMSLiabilty.Dispose();
                        crBMSLiabilty = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateReportType();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBMSLiabilty != null)
            {
                crBMSLiabilty.Dispose();
                crBMSLiabilty = null;
            }
        }
        protected void crBMSLiabilty_Unload(object sender, EventArgs e)
        {
            if (crBMSLiabilty != null)
            {
                crBMSLiabilty.Dispose();
                crBMSLiabilty = null;
            }
        }

        #region Report Button Click

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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string strSupplierCode = default(string);
            string strBranchCode = default(string);

            string strSupplier = default(string);
            string strBranch = default(string);
            string strInwardDate = default(string);
            strSupplierCode = ddlSupplierCode.SelectedValue;
            strBranchCode = ddlBranch.SelectedValue;

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            strSupplier = "{Corporate_Payment_Detail.Supplier_Code}";
            strBranch = "{Corporate_Payment_Detail.Branch_code}";
            strInwardDate = "{Corporate_Payment_Detail.Inward_Date}";

            if (strSupplierCode != "0" && strBranchCode != "0")
                strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strBranch + "='" + strBranchCode + "' and " + strInwardDate + ">=" + strCryFromDate + "and " + strInwardDate + "<=" + strCryToDate;
            else if (strSupplierCode != "0" && strBranchCode == "0")
                strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strInwardDate + ">=" + strCryFromDate + " and " + strInwardDate + "<=" + strCryToDate;
            else if (strSupplierCode == "0" && strBranchCode != "0")
                strSelectionFormula = strBranch + "='" + strBranchCode + "' and " + strInwardDate + ">=" + strCryFromDate + "and " + strInwardDate + "<=" + strCryToDate;
            else if (strSupplierCode == "0" && strBranchCode == "0")
                strSelectionFormula = strSupplier + "<>'' and " + strInwardDate + ">=" + strCryFromDate + "and " + strInwardDate + "<=" + strCryToDate;

            if (strSelectionFormula != "")
                strSelectionFormula = strSelectionFormula + " and {Corporate_Payment_Detail.status} = 'A'";
            else
                strSelectionFormula = strSelectionFormula + "{Corporate_Payment_Detail.status} = 'A'";

            crBMSLiabilty.ReportName = ddlReportType.SelectedValue;
            crBMSLiabilty.CrystalFormulaFields.Add("fromdate", "\"" + txtFromDate.Text + "\"");
            crBMSLiabilty.CrystalFormulaFields.Add("todate", "\"" + txtToDate.Text + "\"");
            crBMSLiabilty.RecordSelectionFormula = strSelectionFormula;
            crBMSLiabilty.GenerateReportAndExportA4(fileType);
        }
        #endregion

        #region Populate Report Type
        public void fnPopulateReportType()
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("BMSPymtReportLiability");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("PaymentReportLiability.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
