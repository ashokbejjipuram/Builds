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
    public partial class PaymentReportHOLocal : System.Web.UI.Page
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

        #region Report Button Click

        protected void btnreport_Click(object sender, EventArgs e)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //Code to Hide / Show filters
                Main mainMaster = (Main)Page.Master;
                if (mainMaster.ShowHideFilters(btnreport, reportFiltersTable, reportViewerHolder))
                {

                    string strSupplierCode = default(string);
                    string strBranchCode = default(string);
                    string selectionFormula = default(string);

                    string strSupplier = default(string);
                    string strBranch = default(string);
                    string strInvoiceDate = default(string);
                    strSupplierCode = ddlSupplierCode.SelectedValue;
                    strBranchCode = ddlBranch.SelectedValue;


                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strSupplier = "{Corporate_Payment_Detail.Supplier_Code}";
                    strBranch = "{Corporate_Payment_Detail.Branch_code}";
                    strInvoiceDate = "{Corporate_Payment_Detail.Invoice_Date}";


                    if (strSupplierCode != "0" && strBranchCode != "0")
                        strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strBranch + "='" + strBranchCode + "' and " + strInvoiceDate + ">=" + strCryFromDate + "and " + strInvoiceDate + "<=" + strCryToDate;
                    else if (strSupplierCode != "0" && strBranchCode == "0")
                        strSelectionFormula = strSupplier + "='" + strSupplierCode + "' and " + strInvoiceDate + ">=" + strCryFromDate + " and " + strInvoiceDate + "<=" + strCryToDate;
                    else if (strSupplierCode == "0" && strBranchCode != "0")
                        strSelectionFormula = strBranch + "='" + strBranchCode + "' and " + strInvoiceDate + ">=" + strCryFromDate + "and " + strInvoiceDate + "<=" + strCryToDate;
                    else if (strSupplierCode == "0" && strBranchCode == "0")
                        strSelectionFormula = strSupplier + "<>'' and " + strInvoiceDate + ">=" + strCryFromDate + "and " + strInvoiceDate + "<=" + strCryToDate;

                    crHOLocal.ReportName = ddlReportType.SelectedValue;
                    crHOLocal.CrystalFormulaFields.Add("fromdate", "\"" + txtFromDate.Text + "\"");
                    crHOLocal.CrystalFormulaFields.Add("todate", "\"" + txtToDate.Text + "\"");                    
                    crHOLocal.RecordSelectionFormula = strSelectionFormula;
                    crHOLocal.GenerateReport();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
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

    }
}
