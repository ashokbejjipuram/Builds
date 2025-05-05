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


namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class InvoiceEditSlipReport : System.Web.UI.Page
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
            string strCryFromDate = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;

            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //Code to Hide / Show filters
                Main mainMaster = (Main)Page.Master;
                if (mainMaster.ShowHideFilters(btnreport, reportFiltersTable, reportViewerHolder))
                {
                    string strSupplierCode = default(string);
                    string strBranchCode = default(string);

                    string strSupplier = default(string);
                    string strBranch = default(string);
                    string strHoReferenceDate = default(string);
                    string strstatus = default(string);
                    strSupplierCode = ddlSupplierCode.SelectedValue;
                    strBranchCode = ddlBranch.SelectedValue;

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strSupplier = "{corporate_Payment_Detail.Supplier_code}";
                    strBranch = "{corporate_Payment_Detail.Branch_code}";
                    strHoReferenceDate = "{corporate_Payment_Detail.Ho_Reference_date}";
                    strstatus = "{corporate_Payment_Detail.status} <> 'I'";

                    if (strSupplierCode == "0" && strBranchCode == "0" && strFromDate != "")
                        strSelectionFormula = strHoReferenceDate + "=" + strCryFromDate;
                    else if (strSupplierCode != "0" && strBranchCode == "0" && strFromDate != "")
                        strSelectionFormula = strHoReferenceDate + "=" + strCryFromDate + " and " + strSupplier + "='" + strSupplierCode + "'";
                    else if (strSupplierCode == "0" && strBranchCode != "0" && strFromDate != "")
                        strSelectionFormula = strHoReferenceDate + "=" + strCryFromDate + " and " + strBranch + "='" + strBranchCode + "'";
                    else if (strSupplierCode != "0" && strBranchCode != "0" && strFromDate != "")
                        strSelectionFormula = strHoReferenceDate + "=" + strCryFromDate + " and " + strSupplier + "='" + strSupplierCode + "'" + " and " + strBranch + "='" + strBranchCode + "'";

                    strSelectionFormula = strSelectionFormula + " and " + strstatus;

                    crEditSlip.ReportName = "InvoiceEditSlipReport";
                    crEditSlip.RecordSelectionFormula = strSelectionFormula;
                    crEditSlip.GenerateReport();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion


    }
}
