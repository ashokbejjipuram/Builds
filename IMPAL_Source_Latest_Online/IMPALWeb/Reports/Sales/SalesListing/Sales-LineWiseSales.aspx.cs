using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;  
namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class Sales_LineWiseSales : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strBranchCode = Session["BranchCode"].ToString();

                if (crSalesLineWiseSales != null)
                {
                    crSalesLineWiseSales.Dispose();
                    crSalesLineWiseSales = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fnPopulateReportName();
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crSalesLineWiseSales != null)
            {
                crSalesLineWiseSales.Dispose();
                crSalesLineWiseSales = null;
            }
        }
        protected void crSalesLineWiseSales_Unload(object sender, EventArgs e)
        {
            if (crSalesLineWiseSales != null)
            {
                crSalesLineWiseSales.Dispose();
                crSalesLineWiseSales = null;
            }
        }

        protected void fnPopulateReportName()
        {
            string strFileName = "LineWiseSalesValueReport";
            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strFileName);
                ddlReportType.DataSource = drop;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    string strSelectionFormula = default(string);
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;
                    string strBranchCodeField = default(string);
                    string strDateField = default(string);
                    string strSupplierField = default(string);
                    string strReportName = default(string);
                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (ddlReportType.SelectedValue == "Report")
                    {
                        strBranchCodeField = "{LineWiseSales_New.varchar}";
                        strDateField = "{LineWiseSales_New.date}";
                        strSupplierField = "mid({LineWiseSales_New.supplier_line_Code},1,3)";
                        strReportName = "LineWiseSales_Report";
                    }
                    else if (ddlReportType.SelectedValue == "Summary")
                    {
                        strBranchCodeField = "{LineWiseSales.varchar}";
                        strDateField = "{LineWiseSales.date}";
                        strSupplierField = "mid({LineWiseSales.supplier_line_Code},1,3)";
                        strReportName = "LineWiseSales_Summary";
                    }

                    if (strBranchCode == "CRP")
                    {
                        if (!string.IsNullOrEmpty(ddlfrmline.SelectedValue) && string.IsNullOrEmpty(ddlfrmline.SelectedValue) && string.IsNullOrEmpty(strCryFromDate) && string.IsNullOrEmpty(strCryToDate))
                            strSelectionFormula = strSupplierField + "='" + ddlfrmline.SelectedValue + "'";
                    }
                    else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                    {
                        strSelectionFormula = strBranchCodeField + "='" + strBranchCode + "' and " + strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                        if ((ddlfrmline.SelectedIndex > 0) && (ddlToline.SelectedIndex > 0))
                            strSelectionFormula = strSelectionFormula + " and " + strSupplierField + " in '" + ddlfrmline.SelectedValue + "' to '" + ddlToline.SelectedValue + "'";
                        else if ((ddlfrmline.SelectedIndex <= 0) && (ddlToline.SelectedIndex > 0))
                            strSelectionFormula = strSelectionFormula + " and " + strSupplierField + " in '" + ddlToline.SelectedValue + "'";
                        else if ((ddlfrmline.SelectedIndex > 0) && (ddlToline.SelectedIndex <= 0))
                            strSelectionFormula = strSelectionFormula + " and " + strSupplierField + " in '" + ddlfrmline.SelectedValue + "'";
                    }

                    crSalesLineWiseSales.ReportName = strReportName;
                    crSalesLineWiseSales.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crSalesLineWiseSales.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crSalesLineWiseSales.RecordSelectionFormula = strSelectionFormula;
                    crSalesLineWiseSales.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
