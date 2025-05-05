using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesListing_item_wise_ : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crSTDN_Inward != null)
                    {
                        crSTDN_Inward.Dispose();
                        crSTDN_Inward = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    PopulateReportType();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crSTDN_Inward != null)
            {
                crSTDN_Inward.Dispose();
                crSTDN_Inward = null;
            }
        }
        protected void crSTDN_Inward_Unload(object sender, EventArgs e)
        {
            if (crSTDN_Inward != null)
            {
                crSTDN_Inward.Dispose();
                crSTDN_Inward = null;
            }
        }

        #region PopulateReportType
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-STDNIn/Out");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Button Click

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
        #endregion

        #region Generate Selection Formula
        public void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            //string strBranchCode=default(string);
            string strSelectionFormula = default(string);

            if (ddlReportType.SelectedValue == "Inward")
                crSTDN_Inward.ReportName = "STDN_Inward";
            else
                crSTDN_Inward.ReportName = "STDN_Outward";
            
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            string strSTDN_BranchCode = default(string);
            string strSTDN_DateField = default(string);

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strSTDN_BranchCode = "{STDN_HEADER.Branch_Code}";
            strSTDN_DateField = "{STDN_HEADER.STDN_date}";

            if (ddlReportType.SelectedValue == "Inward")
                strSelectionFormula = "NOT ISNULL({STDN_Detail.Received_Quantity}) and {STDN_Detail.Received_Quantity}>0 and {STDN_Header.To_Branch_Code}={STDN_Header.Branch_Code}";
            else
                strSelectionFormula = "NOT ISNULL({STDN_Detail.Despatched_Quantity}) and {STDN_Detail.Despatched_Quantity}>0 and {STDN_Header.From_Branch_Code}={STDN_Header.Branch_Code}";

            if (strBranchCode == "CRP")
                strSelectionFormula = strSelectionFormula + " and " + strSTDN_DateField + ">=" + strCryFromDate + "and" + strSTDN_DateField + "<=" + strCryToDate;
            else
                strSelectionFormula = strSelectionFormula + " and " + strSTDN_DateField + ">=" + strCryFromDate + "and" + strSTDN_DateField + "<=" + strCryToDate + " and " + strSTDN_BranchCode + "='" + strBranchCode + "'";

            crSTDN_Inward.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSTDN_Inward.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSTDN_Inward.RecordSelectionFormula = strSelectionFormula;
            crSTDN_Inward.GenerateReportAndExportA4(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (crSTDN_Inward != null)
                {
                    crSTDN_Inward.Dispose();
                    crSTDN_Inward = null;
                }

                Server.ClearError();
                Response.Redirect("STDNInwardOutward.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}