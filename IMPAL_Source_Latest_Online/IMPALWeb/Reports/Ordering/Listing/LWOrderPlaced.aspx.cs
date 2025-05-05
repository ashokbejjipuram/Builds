using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class LWOrderPlaced : System.Web.UI.Page
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
                    if (crLinewiseOrederPlaced != null)
                    {
                        crLinewiseOrederPlaced.Dispose();
                        crLinewiseOrederPlaced = null;
                    }

                    PopulateReportType();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
            if (crLinewiseOrederPlaced != null)
            {
                crLinewiseOrederPlaced.Dispose();
                crLinewiseOrederPlaced = null;
            }
        }
        protected void crLinewiseOrederPlaced_Unload(object sender, EventArgs e)
        {
            if (crLinewiseOrederPlaced != null)
            {
                crLinewiseOrederPlaced.Dispose();
                crLinewiseOrederPlaced = null;
            }
        }

        #region PopulateReportType
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateReportType", "Entering PopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-LineWise");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
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
            string strReportType = ddlReportType.Text;
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            string strSuppFrom = ddlFromLine.SelectedValue;
            string strSuppTo = ddlToLine.SelectedValue;

            if (strReportType.Equals("Report"))
                crLinewiseOrederPlaced.ReportName = "LinewiseOrederPlaced";
            else if (strReportType.Equals("Supplementary"))
                crLinewiseOrederPlaced.ReportName = "LinewiseOrederPlaced_Suppl";
            else if (strReportType.Equals("Branch Summary"))
                crLinewiseOrederPlaced.ReportName = "LinewiseOrederPlaced_BrSumm";
            else if (strReportType.Equals("Line Summary"))
                crLinewiseOrederPlaced.ReportName = "LinewiseOrederPlaced_lnSumm";
            else if (strReportType.Equals("Loss Of Sale"))
                crLinewiseOrederPlaced.ReportName = "LinewiseOrederPlaced_LossOfSale";

            if (strSuppFrom == "0")
                strSuppFrom = "";
            if (strSuppTo == "0")
                strSuppTo = "";

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            strCryFromDate = "Date(" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date(" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            string strCode = default(string);
            string strSupplierCode = default(string);
            string strDate = default(string);

            if (ddlReportType.SelectedValue == "Loss Of Sale")
            {
                strSupplierCode = "{Loss_Of_Sale.Supplier_code}";
                strDate = "{Loss_Of_sale.DateStamp}";
                strCode = "mid({Loss_Of_Sale.Customer_code},1,3)";
            }
            else
            {
                strSupplierCode = "{Purchase_Order_Header.Supplier_Code}";
                strDate = "{Purchase_Order_Header.Indent_Date}";
                strCode = "{Purchase_Order_Header.Branch_Code}";
            }
            if ((strSuppFrom == "") && (strSuppTo == "") && (strFromDate == "") && (strToDate == ""))
                strSelectionFormula = null;
            else if (strBranchCode == "CRP")
            {
                if (strSuppFrom != "" && strSuppTo == "" && strFromDate == "" && strToDate == "")
                    strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "'";
                else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate == "" && strToDate == ""))
                    strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "'";
                else if ((strSuppFrom == "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                else if ((strSuppFrom != "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strSupplierCode + "'" + strSuppFrom + "' and " + strDate + ">=" + strCryFromDate + "and " + strDate + "<=" + strCryToDate;
                else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "'and " + strDate + ">=" + strCryFromDate + "and" + strDate + "<=" + strCryToDate;
            }
            else
            {
                if (strSuppFrom != "" && strSuppTo == "" && strFromDate == "" && strToDate == "")
                    strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "' and " + strCode + "='" + strBranchCode + "'";
                else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate == "" && strToDate == ""))
                    strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "'and " + strCode + "='" + strBranchCode + "'";
                else if ((strSuppFrom == "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strCode + "='" + strBranchCode + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                else if ((strSuppFrom != "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate + "and " + strCode + "='" + strBranchCode + "'";
                else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate != "" && strToDate != ""))
                    strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate + " and " + strCode + "='" + strBranchCode + "'";
            }

            if (ddlReportType.SelectedValue == "Supplementary")
            {
                strSelectionFormula = strSelectionFormula + " and (ISNULL({Purchase_Order_Header.Status}) or {Purchase_Order_Header.Status} = 'A')";
            }
            else if (ddlReportType.SelectedValue == "Branch Summary" || ddlReportType.SelectedValue == "Line Summary")
            {
                strSelectionFormula = strSelectionFormula + " and (ISNULL({Purchase_Order_Detail.Status}) or {Purchase_Order_Detail.Status} = 'A')";
            }

            crLinewiseOrederPlaced.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crLinewiseOrederPlaced.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crLinewiseOrederPlaced.RecordSelectionFormula = strSelectionFormula;
            crLinewiseOrederPlaced.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LWOrderPlaced.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}