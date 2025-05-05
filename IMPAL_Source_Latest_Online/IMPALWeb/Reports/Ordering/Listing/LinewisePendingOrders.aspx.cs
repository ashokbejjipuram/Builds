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
    public partial class LinewisePendingOrders : System.Web.UI.Page
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
                    if (crLinewisePendingOrders != null)
                    {
                        crLinewisePendingOrders.Dispose();
                        crLinewisePendingOrders = null;
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
            if (crLinewisePendingOrders != null)
            {
                crLinewisePendingOrders.Dispose();
                crLinewisePendingOrders = null;
            }
        }
        protected void crLinewisePendingOrders_Unload(object sender, EventArgs e)
        {
            if (crLinewisePendingOrders != null)
            {
                crLinewisePendingOrders.Dispose();
                crLinewisePendingOrders = null;
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
                oList = oCommon.GetDropDownListValues("ReportType-LineWisePending");
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

        #region Selection Date Line
        public void SelectiondateLine(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            string strSuppCode = ddlSupplierCode.Text;

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text; strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            string strPODate = default(string);
            string strPONumber = default(string);
            string strBrCode = default(string);
            string strTrans = default(string);

            strPODate = "{Po_Order_Pending_Detail_View.PO_date}";
            strPONumber = "{Po_Order_Pending_Detail_View.Supplier_Code}";
            strBrCode = "{Po_Order_Pending_Detail_View.branch_code}";
            strTrans = "{Po_Order_Pending_Detail_View.Transaction_Type_Code}";

            if (ddlReportType.SelectedValue == "Aging")
            {
                if (strBranchCode == "CRP")
                    strSelectionFormula = strPONumber + "=" + " '" + strSuppCode + "'";
                else
                    strSelectionFormula = strPONumber + "=" + " '" + strSuppCode + "'" + " and " + strBrCode + "='" + strBranchCode + "'";
            }
            else
            {
                if (strBranchCode == "CRP")
                    strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate + " and " + strPONumber + "=" + " '" + strSuppCode + "'";
                else
                    strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate + " and " + strPONumber + "=" + " '" + strSuppCode + "'" + " and " + strBrCode + "='" + strBranchCode + "'";
            }

            if (ddlReportType.SelectedValue == "Customer")
                strSelectionFormula = strSelectionFormula + " and " + strTrans + " IN ['451','201'] and Not ISNULL({Po_Order_Pending_Detail_View.Customer_Code})";

            crLinewisePendingOrders.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crLinewisePendingOrders.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crLinewisePendingOrders.RecordSelectionFormula = strSelectionFormula;
            crLinewisePendingOrders.GenerateReportAndExport(fileType);
        }
        #endregion

        #region Selection Date
        public void SelectionDate(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            string strSuppCode = ddlSupplierCode.Text;

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            string strPODate = default(string);
            string strBrCode = default(string);
            string strTrans = default(string);

            strPODate = "{Po_Order_Pending_Detail_View.PO_date}";
            strBrCode = "{Po_Order_Pending_Detail_View.branch_code}";
            strTrans = "{Po_Order_Pending_Detail_View.Transaction_Type_Code}";

            if (ddlReportType.SelectedValue == "Aging")
            {
                if (strBranchCode == "CRP")
                    strSelectionFormula = "";
                else
                    strSelectionFormula = strBrCode + "='" + strBranchCode + "'";
            }
            else
            {
                if (strBranchCode == "CRP")
                    strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate;
                else
                    strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate + " and " + strBrCode + "='" + strBranchCode + "'";
            }

            if (ddlReportType.SelectedValue == "Customer")
                strSelectionFormula = strSelectionFormula + " and " + strTrans + " IN ['451', '201'] and Not ISNULL({ Po_Order_Pending_Detail_View.Customer_Code})";

            crLinewisePendingOrders.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crLinewisePendingOrders.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crLinewisePendingOrders.RecordSelectionFormula = strSelectionFormula;
            crLinewisePendingOrders.GenerateReportAndExport(fileType);
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

        protected void GenerateAndExportReport(string fileType)
        {
            if (ddlReportType.SelectedValue == "Detail")
                crLinewisePendingOrders.ReportName = "LinewisePendingOrders";
            else if (ddlReportType.SelectedValue == "Line Summary")
                crLinewisePendingOrders.ReportName = "LinewisePendingOrders_LnSumm";
            else if (ddlReportType.SelectedValue == "Part Summary")
                crLinewisePendingOrders.ReportName = "LinewisePendingOrders_PtSumm";
            else if (ddlReportType.SelectedValue == "Customer")
                crLinewisePendingOrders.ReportName = "LinewisePendingOrders_Customer";
            else if (ddlReportType.SelectedValue == "Aging")
                crLinewisePendingOrders.ReportName = "LinewisePendingOrders_Aging";

            if ((txtFromDate.Text != "") && (txtToDate.Text != "") && (ddlSupplierCode.SelectedValue != "0"))
                SelectiondateLine(fileType);
            else
                SelectionDate(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LinewisePendingOrders.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}