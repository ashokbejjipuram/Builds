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


namespace IMPALWeb.Reports.Finance.Stock
{
    public partial class InwardInsuranceStatement : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crInwardInsuranceStatement != null)
                    {
                        crInwardInsuranceStatement.Dispose();
                        crInwardInsuranceStatement = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateInsuranceType();
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
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
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
            if (crInwardInsuranceStatement != null)
            {
                crInwardInsuranceStatement.Dispose();
                crInwardInsuranceStatement = null;
            }
        }
        protected void crInwardInsuranceStatement_Unload(object sender, EventArgs e)
        {
            if (crInwardInsuranceStatement != null)
            {
                crInwardInsuranceStatement.Dispose();
                crInwardInsuranceStatement = null;
            }
        }

        #region Populate Insurance Type
        public void fnPopulateInsuranceType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInsuranceType", "Entering fnPopulateInsuranceType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-InwardInsurance");
                ddlInsurance.DataSource = oList;
                ddlInsurance.DataValueField = "DisplayValue";
                ddlInsurance.DataTextField = "DisplayText";
                ddlInsurance.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
            if (ddlInsurance.SelectedValue == "Y")
                crInwardInsuranceStatement.ReportName = "InwardInsuranceStatement";
            else
                crInwardInsuranceStatement.ReportName = "InwardNonInsuranceStatement";

            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strInsurance = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            strInsurance = ddlInsurance.SelectedValue;
            string strInwardBrcode = default(string);
            string strInwardDate = default(string);
            string strInwardIndicator = default(string);

            if (strInsurance == "Y")
            {
                strInwardBrcode = "{Inward_Insurance.branch_code}";
                strInwardDate = "{Inward_Insurance.Inward_Date}";
                strInwardIndicator = "{Inward_Insurance.Insurance_Indicator}";
            }
            else
            {
                strInwardBrcode = "{Inward_Header.branch_code}";
                strInwardDate = "{Inward_Header.Inward_Date}";
                strInwardIndicator = "{supplier_master.Insurance_Indicator}";
            }

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (strBranchCode != "CRP")
                strSelectionFormula = strInwardDate + ">=" + strCryFromDate + "and" + strInwardDate + "<=" + strCryToDate + " and " + strInwardBrcode + " ='" + strBranchCode + "' and " + strInwardIndicator + "='" + strInsurance + "'";
            else
                strSelectionFormula = strInwardDate + ">=" + strCryFromDate + "and" + strInwardDate + "<=" + strCryToDate + " and " + strInwardIndicator + "='" + strInsurance + "'";

            if (strInsurance == "N")
                strSelectionFormula = strSelectionFormula + " and {supplier_master.insurance_indicator} = 'N' and Not isnull({Inward_Header.date_opened})";

            crInwardInsuranceStatement.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crInwardInsuranceStatement.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crInwardInsuranceStatement.RecordSelectionFormula = strSelectionFormula;
            crInwardInsuranceStatement.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("InwardInsuranceStatement.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}