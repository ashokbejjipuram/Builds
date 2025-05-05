#region Namespace
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
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Statement
{
    public partial class PurchaseReturnReprint : System.Web.UI.Page
    {
        #region Page Init
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
                if (!IsPostBack)
                {
                    if (cryPurchaseReturnReprint != null)
                    {
                        cryPurchaseReturnReprint.Dispose();
                        cryPurchaseReturnReprint = null;
                    }

                    if (Session["PurchaseReturnNumber"] != null && Session["PurchaseReturnNumber"].ToString() != "")
                    {
                        divSelection.Visible = false;
                        DivBackbtn.Attributes.Add("style", "display:block");
                        fnGenerateReport();
                    }
                    //else
                    //{
                    //    fnPopulateInvoiceType();
                    //    fnPopulateInvoiceSTDN(ddlInvoiceType.SelectedValue);
                    //    divSelection.Visible = true;
                    //}
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
            if (cryPurchaseReturnReprint != null)
            {
                cryPurchaseReturnReprint.Dispose();
                cryPurchaseReturnReprint = null;
            }
        }
        protected void cryPurchaseReturnReprint_Unload(object sender, EventArgs e)
        {
            if (cryPurchaseReturnReprint != null)
            {
                cryPurchaseReturnReprint.Dispose();
                cryPurchaseReturnReprint = null;
            }
        }

        #region fnGenerateReport
        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            if (Session["PurchaseReturnNumber"] != null && Session["PurchaseReturnNumber"].ToString() != "")
            {
                strField = "{Purchase_Return_Header.Purchase_Return_Number}";
                strValue = Session["PurchaseReturnNumber"].ToString();
                strSelectionFormula = strField + "= " + "'" + strValue + "'";
                strReportName = "po_pp_invoice_PurchaseReturn";

                cryPurchaseReturnReprint.ReportName = strReportName;
                cryPurchaseReturnReprint.RecordSelectionFormula = strSelectionFormula;
                cryPurchaseReturnReprint.GenerateReportAndExportA4();
            }
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strField = "{Purchase_Return_Header.Purchase_Return_number}";
            strReportName = "po_pp_invoice_PurchaseReturn";
            strValue = ddlInvoice.SelectedValue;
            strSelectionFormula = strField + "= " + "'" + strValue + "'";

            cryPurchaseReturnReprint.ReportName = strReportName;
            cryPurchaseReturnReprint.RecordSelectionFormula = strSelectionFormula;
            cryPurchaseReturnReprint.GenerateReportAndExportA4();
        }
        #endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                if (Session["PurchaseReturnNumber"] != null && Session["PurchaseReturnNumber"].ToString() != "")
                    Response.Redirect("~/Transactions/Inventory/PurchaseReturnEDP.aspx", false);
                
                Context.ApplicationInstance.CompleteRequest();
                Session["PurchaseReturnNumber"] = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
