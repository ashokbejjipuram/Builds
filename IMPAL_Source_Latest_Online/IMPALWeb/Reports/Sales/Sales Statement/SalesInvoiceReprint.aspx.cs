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
    public partial class SalesInvoiceReprint : System.Web.UI.Page
    {
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

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("STDNNumber");
                    Session["STDNNumber"] = null;

                    Session.Remove("SalesInvoiceNumber");
                    Session["SalesInvoiceNumber"] = null;

                    if (crySalesInvoiceReprint != null)
                    {
                        crySalesInvoiceReprint.Dispose();
                        crySalesInvoiceReprint = null;
                    }

                    fnPopulateInvoiceType();
                    fnPopulateInvoiceSTDN(ddlInvoiceType.SelectedValue);
                    divSelection.Visible = true;
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
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }
        protected void crySalesInvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");

            string strSelectionFormula = default(string);
            string strBrnchField = default(string);
            string strBrnchValue = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strBrnchValue = Session["BranchCode"].ToString();
            strValue = ddlInvoiceSTDN.SelectedValue;

            //SalesReport srpt = new SalesReport();

            if (ddlInvoiceType.SelectedValue.Equals("D"))
            {
                strBrnchField = "{V_Invoice.Branch_Code}";
                strField = "{V_Invoice.Document_number}";
                strReportName = "po_pp_invoice_invGST";

                //hdnEwayBillInd.Value = srpt.GetEWayBillInd(strBrnchValue, strValue);
            }
            else
            {
                strBrnchField = "{V_Invoice_STDN.Branch_Code}";
                strField = "{V_Invoice_STDN.Document_number}";
                strReportName = "po_pp_invoice_stdnGST";

                //hdnEwayBillInd.Value = srpt.GetSTDNeWayBillInd(strBrnchValue, strValue);
            }

            strSelectionFormula = strBrnchField + "='" + strBrnchValue + "' and " + strField + "= " + "'" + strValue + "'";

            if (!string.IsNullOrEmpty(strReportName))
            {
                crySalesInvoiceReprint.ReportName = strReportName;
                crySalesInvoiceReprint.RecordSelectionFormula = strSelectionFormula;

                if (ddlInvoiceType.SelectedValue.Equals("D"))
                    crySalesInvoiceReprint.GenerateReportAndExportInvoiceA4(strValue.Replace("/", "-"), 1);
                else
                    crySalesInvoiceReprint.GenerateReportAndExportInvoiceA4(strValue.Replace("/", "-"), 2);
            }
        }
        #endregion

        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Main mainmaster = (Main)Page.Master;
            mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
            if (btnReport.Text == "Back")
            {
                GenerateSelectionFormula();
            }
        }
        #endregion


        #region fnPopulateInvoiceType
        protected void fnPopulateInvoiceType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInvoiceType()", "Entering fnPopulateInvoiceType()");

            string strFileName = "InvoiceType";
            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strFileName);
                ddlInvoiceType.DataSource = drop;
                ddlInvoiceType.DataValueField = "DisplayValue";
                ddlInvoiceType.DataTextField = "DisplayText";
                ddlInvoiceType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateInvoiceSTDN
        protected void fnPopulateInvoiceSTDN(string strInvoiceType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInvoiceSTDN()", "Entering fnPopulateInvoiceSTDN()");
            try
            {
                SalesReport srpt = new SalesReport();
                if (string.IsNullOrEmpty(strInvoiceType))
                    ddlInvoiceSTDN.Items.Clear();
                else
                {
                    ddlInvoiceSTDN.DataSource = srpt.GetInvoiceSTDN(strInvoiceType, Session["BranchCode"].ToString());
                    ddlInvoiceSTDN.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlInvoiceType_SelectedIndexChanged
        protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ddlInvoiceType_SelectedIndexChanged()", "Entering ddlInvoiceType_SelectedIndexChanged()");
            try
            {
                fnPopulateInvoiceSTDN(ddlInvoiceType.SelectedValue);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("SalesInvoiceReprint.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
