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
    public partial class ProformaInvoiceReprint : System.Web.UI.Page
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
                    if (cryProformaInvoiceReprint != null)
                    {
                        cryProformaInvoiceReprint.Dispose();
                        cryProformaInvoiceReprint = null;
                    }

                    if (Session["ProformaInvoiceNumber"] != null && Session["ProformaInvoiceNumber"].ToString() != "")
                    {
                        divSelection.Visible = false;
                        DivBackbtn.Attributes.Add("style", "display:block");
                        fnGenerateReport();
                    }
                    else
                    {
                        fnPopulateInvoiceType();
                        fnPopulateInvoiceProforma(ddlInvoiceType.SelectedValue);
                        divSelection.Visible = true;
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
            if (cryProformaInvoiceReprint != null)
            {
                cryProformaInvoiceReprint.Dispose();
                cryProformaInvoiceReprint = null;
            }
        }
        protected void cryProformaInvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (cryProformaInvoiceReprint != null)
            {
                cryProformaInvoiceReprint.Dispose();
                cryProformaInvoiceReprint = null;
            }
        }

        #region fnGenerateReportGST
        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strField = "{V_Invoice.Document_number}";
            strValue = Session["ProformaInvoiceNumber"].ToString();
            strSelectionFormula = strField + "= " + "'" + strValue + "'";
            strReportName = "po_pp_invoiceProforma";

            cryProformaInvoiceReprint.ReportName = strReportName;
            cryProformaInvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            cryProformaInvoiceReprint.GenerateReportAndExportA4();
        }
        #endregion

        #region Insert in to V_invoice table
        protected bool InsertVinvoiceTable(string InvNum, string BranchCode)
        {
            bool blSuccess = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                if (!string.IsNullOrEmpty(InvNum))
                {
                    DbCommand cmdtemps = ImpalDB.GetStoredProcCommand("usp_GetProformatInvoice_RePrint");
                    ImpalDB.AddInParameter(cmdtemps, "@Branch_Code", DbType.String, BranchCode);
                    ImpalDB.AddInParameter(cmdtemps, "@Document_Number", DbType.String, InvNum);
                    cmdtemps.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdtemps);

                    blSuccess = true;

                }
                return blSuccess;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return blSuccess;
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            InsertVinvoiceTable(ddlInvoiceProforma.SelectedValue, Session["BranchCode"].ToString());

            strField = "{V_Invoice.Document_number}";
            strValue = ddlInvoiceProforma.SelectedValue;
            strSelectionFormula = strField + "= " + "'" + strValue + "'";
            strReportName = "po_pp_invoiceProforma";

            cryProformaInvoiceReprint.ReportName = strReportName;
            cryProformaInvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            cryProformaInvoiceReprint.GenerateReportAndExportA4();
        }
        #endregion

        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateInvoiceType
        protected void fnPopulateInvoiceType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strFileName = "ProformaInvoiceType";
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

        #region fnPopulateInvoiceProforma
        protected void fnPopulateInvoiceProforma(string strInvoiceType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                SalesReport srpt = new SalesReport();
                if (string.IsNullOrEmpty(strInvoiceType))
                    ddlInvoiceProforma.Items.Clear();

                ddlInvoiceProforma.DataSource = srpt.GetInvoiceProforma(strInvoiceType, Session["BranchCode"].ToString());
                ddlInvoiceProforma.DataBind();
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
            try
            {
                fnPopulateInvoiceProforma(ddlInvoiceType.SelectedValue);
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
            try
            {
                Server.ClearError();
                Response.Redirect("~/Transactions/sales/ProformaInvoice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                Session["ProformaInvoiceNumber"] = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}