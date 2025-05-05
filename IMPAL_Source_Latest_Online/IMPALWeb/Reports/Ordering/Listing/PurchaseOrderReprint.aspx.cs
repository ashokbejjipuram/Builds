#region Namespace
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
#endregion

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class PurchaseOrderReprint : System.Web.UI.Page
    {
        #region GlobalDeclaration
        private string strBranchCode = default(string);
        private string strPONumber = default(string);
        private string Indicator = default(string);
        private string strPONumberField = default(string);
        string strReportName = default(string);
        #endregion

        #region Page Init
        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    if (crPurchaseOrderReprint != null)
                    {
                        crPurchaseOrderReprint.Dispose();
                        crPurchaseOrderReprint = null;
                    }

                    fnPopulateReportType();
                    fnPopulatePOType();
                    fnPopulatePONumber(ddlPOType.SelectedValue, ddlReportType.SelectedValue);
                    divSelection.Visible = true;

                    if (Session["PONumber"] != null)
                    {
                        ddlPONumber.SelectedValue = Session["PONumber"].ToString();
                        ddlPONumber.Enabled = false;
                        ddlPOType.Enabled = false;
                        ddlReportType.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
                        //fnPopulateReportForSubmission();
                        //divSelection.Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crPurchaseOrderReprint != null)
            {
                crPurchaseOrderReprint.Dispose();
                crPurchaseOrderReprint = null;
            }
        }
        protected void crPurchaseOrderReprint_Unload(object sender, EventArgs e)
        {
            if (crPurchaseOrderReprint != null)
            {
                crPurchaseOrderReprint.Dispose();
                crPurchaseOrderReprint = null;
            }
        }

        protected void fnPopulateReportForSubmission()
        {
            string strSelectionFormula = default(string);
            string strBranchCodeField = default(string);
            string strPONumberField = default(string);
            string strBranchCode = default(string);
            string PO_NUMBER = default(string);
            PO_NUMBER = Session["PONumber"].ToString();
            strBranchCode = Session["BranchCode"].ToString();

            strBranchCodeField = "{Purchase_Order_Header.branch_code}";
            strPONumberField = "{Purchase_Order_Header.PO_number}";
            if (strBranchCode == "CRP")
                strSelectionFormula = strPONumberField + "=" + " " + "'" + PO_NUMBER + "'";
            else
                strSelectionFormula = strPONumberField + "=" + " " + "'" + PO_NUMBER + "' and " +
                                      strBranchCodeField + "='" + strBranchCode + "'";

            crPurchaseOrderReprint.ReportName = "Purchase_Direct";
            crPurchaseOrderReprint.RecordSelectionFormula = strSelectionFormula;
            crPurchaseOrderReprint.GenerateReportAndExportA4(".pdf");
        }

        #region Populate Report Type
        /// <summary>
        /// To Populate Report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entering fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("POReportType");
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

        #region Populate PO Types
        /// <summary>
        /// To Populate PO Type
        /// </summary>
        protected void fnPopulatePOType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulatePOType", "Entering fnPopulatePOType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("POType");
                ddlPOType.DataSource = oList;
                ddlPOType.DataValueField = "DisplayValue";
                ddlPOType.DataTextField = "DisplayText";
                ddlPOType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate PO Number
        /// <summary>
        /// To Populate PO Number based on PO Type and Report Type selection
        /// </summary>
        /// <param name="strPOType"></param>
        /// <param name="strReportType"></param>
        protected void fnPopulatePONumber(string strPOType, string strReportType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulatePONumber", "Entering fnPopulatePONumber");
            try
            {
                string strBranchCode = (string)Session["BranchCode"];

                IMPALLibrary.Masters.PONumber.PONumbers ponumb = new IMPALLibrary.Masters.PONumber.PONumbers();
                ddlPONumber.DataSource = ponumb.GetPONumber(strPOType, strReportType, strBranchCode);
                ddlPONumber.DataTextField = "PONumber";
                ddlPONumber.DataValueField = "PONumber";
                ddlPONumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ReportType Selected Index Change
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ddlReportType_SelectedIndexChanged", "Entering ddlReportType_SelectedIndexChanged");
            try
            {
                if (ddlReportType.SelectedIndex == 1)
                {
                    ddlPOType.Enabled = false;
                    ddlPOType.SelectedIndex = 2;
                    fnPopulatePOType();
                    fnPopulatePONumber(ddlPOType.SelectedValue, ddlReportType.SelectedValue);
                }
                else
                {
                    ddlPOType.SelectedIndex = 0;
                    ddlPOType.Enabled = true;
                    fnPopulatePOType();
                    fnPopulatePONumber(ddlPOType.SelectedValue, ddlReportType.SelectedValue);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region PO Type Selected Index Change
        protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulatePONumber(ddlPOType.SelectedValue, ddlReportType.SelectedValue);
        }
        #endregion

        #region Button Click

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
        protected void GenerateAndExportReport(string fileType)
        {
            if (ddlPOType.SelectedIndex == 0)
                crPurchaseOrderReprint.ReportName = "Purchase_Direct";
            else if (ddlPOType.SelectedIndex == 1)
                crPurchaseOrderReprint.ReportName = "Purchase_Indent";
            else if (ddlPOType.SelectedIndex == 4)
                crPurchaseOrderReprint.ReportName = "Purchase_worksheetEPO";
            else
                crPurchaseOrderReprint.ReportName = "Purchase_worksheet";

            string strSelectionFormula = default(string);
            strPONumber = ddlPONumber.Text;

            string strBranchCodeField = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            strBranchCodeField = "{Purchase_Order_Header.branch_code}";
            strPONumberField = "{Purchase_Order_Header.PO_number}";

            strSelectionFormula = strPONumberField + "=" + " " + "'" + strPONumber + "' and " + strBranchCodeField + "='" + strBranchCode + "'";

            crPurchaseOrderReprint.RecordSelectionFormula = strSelectionFormula;
            crPurchaseOrderReprint.GenerateReportAndExportA4(fileType);
        }
        #endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();

                if (Session["PONumber"] != null && Session["PONumber"].ToString() != "")
                    Response.Redirect("~/Transactions/Ordering/DirectPurchaseOrder.aspx", false);
                else
                    Response.Redirect("PurchaseOrderReprint.aspx", false);

                Context.ApplicationInstance.CompleteRequest();

                Session.Remove("PONumber");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}