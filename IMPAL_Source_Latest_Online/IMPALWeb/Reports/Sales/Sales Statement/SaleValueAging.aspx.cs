#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
#endregion 

namespace IMPALWeb.Reports.Sales.Sales_Statement
{
    public partial class SaleValueAging : System.Web.UI.Page
    {
        string sessionbrchcode = string.Empty;

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
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionbrchcode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crSaleValue != null)
                    {
                        crSaleValue.Dispose();
                        crSaleValue = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    IMPALLibrary.Suppliers supp = new IMPALLibrary.Suppliers();
                    List<IMPALLibrary.Supplier> lstSupplier = new List<IMPALLibrary.Supplier>();
                    lstSupplier = supp.GetAllSuppliers();
                    ddlSupplier.DataSource = lstSupplier;
                    ddlSupplier.DataTextField = "SupplierName";
                    ddlSupplier.DataValueField = "SupplierCode";
                    ddlSupplier.DataBind();

                    ImpalLibrary oCommon = new ImpalLibrary();
                    List<DropDownListValue> oList = new List<DropDownListValue>();
                    oList = oCommon.GetDropDownListValues("RepType-SaleValueAging");
                    ddlSaleType.DataSource = oList;
                    ddlSaleType.DataTextField = "DisplayText";
                    ddlSaleType.DataValueField = "DisplayValue";
                    ddlSaleType.DataBind();

                    List<DropDownListValue> oList1 = new List<DropDownListValue>();
                    oList1 = oCommon.GetDropDownListValues("RepType-StockValueAging2");
                    ddlReportType.DataSource = oList1;
                    ddlReportType.DataTextField = "DisplayText";
                    ddlReportType.DataValueField = "DisplayValue";
                    ddlReportType.DataBind();
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
            if (crSaleValue != null)
            {
                crSaleValue.Dispose();
                crSaleValue = null;
            }
        }
        protected void crSaleValue_Unload(object sender, EventArgs e)
        {
            if (crSaleValue != null)
            {
                crSaleValue.Dispose();
                crSaleValue = null;
            }
        }

        #region Generate Selection Formula
        /// <summary>
        /// Method to generate selection formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                #region Declaration
                string selectionformula = string.Empty;
                string strselbranch = string.Empty;
                string strselsupplier = string.Empty;
                string strseldate = string.Empty;
                string strseldocnumber = string.Empty;
                string strselissuedocno = string.Empty;

                if (ddlSaleType.SelectedValue == "T")
                {
                    strselbranch = "{branch_master.branch_code}=";
                    strselsupplier = "{Supplier_Master.supplier_code}=";
                    strseldate = "{V_SalesReports_STDN.document_date}";
                    strseldocnumber = "Right({V_SalesReports_STDN.inward_number},2)";
                    strselissuedocno = "Length({V_SalesReports_STDN.Document_Number})=";
                }
                else
                {
                    strselbranch = "{branch_master.branch_code}=";
                    strselsupplier = "{Supplier_Master.supplier_code}=";
                    strseldate = "{V_SalesReports_aging.document_date}";
                    strseldocnumber = "Right({V_SalesReports_aging.inward_number},2)";
                    strselissuedocno = "Length({V_SalesReports_aging.Document_Number})=";
                }

                string[] strFromDate = txtFromDate.Text.Split('/');
                string[] strToDate = txtToDate.Text.Split('/');

                string Dfrom_param = "Date (" + strFromDate[2].Trim() + "," + strFromDate[1].Trim() + "," + strFromDate[0].Trim() + ")";
                string Dto_param = "Date (" + strToDate[2].Trim() + "," + strToDate[1].Trim() + "," + strToDate[0].Trim() + ")";

                #endregion
                #region SelectionFormula

                if (ddlSupplier.SelectedIndex == 0)
                {
                    if (ddlSaleType.SelectedValue == "T")
                    {
                        if (sessionbrchcode != "CRP")
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strseldocnumber + "='SI' and " + strselbranch + "'" + sessionbrchcode + "'";
                        }
                        else
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strseldocnumber + "='SI'";
                        }
                    }
                    else
                    {
                        if (sessionbrchcode != "CRP")
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strselbranch + "'" + sessionbrchcode + "'";
                        }
                        else
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param;
                        }
                    }
                }
                else
                {
                    if (ddlSaleType.SelectedValue == "T")
                    {
                        if (sessionbrchcode != "CRP")
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strseldocnumber + "'SI' and " + strselbranch + "'" + sessionbrchcode + "' and " + strselsupplier + "'" + ddlSupplier.SelectedValue + "'";
                        }
                        else
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strseldocnumber + "'SI' and " + strselsupplier + "'" + ddlSupplier.SelectedValue + "'";
                        }
                    }
                    else
                    {
                        if (sessionbrchcode != "CRP")
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strselissuedocno + "18 and " + strselbranch + "'" + sessionbrchcode + "' and " + strselsupplier + "'" + ddlSupplier.SelectedValue + "'";
                        }
                        else
                        {
                            selectionformula = strseldate + " >=" + Dfrom_param + " and " + strseldate + " <= " + Dto_param + " and " + strselissuedocno + "18 and " + strselsupplier + "'" + ddlSupplier.SelectedValue + "'";
                        }
                    }

                }
                #endregion

                crSaleValue.RecordSelectionFormula = selectionformula;
                crSaleValue.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Generate Button click
        /// <summary>
        /// Generate Report Button click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);

                if (btnReport.Text == "Back")
                {
                    #region ReportName
                    if (ddlReportType.SelectedValue == "0")
                    {
                        if (ddlSaleType.SelectedValue == "T")
                        {
                            crSaleValue.ReportName = "aging_salevalue_STDN";
                        }
                        else
                        {
                            crSaleValue.ReportName = "aging_salevalue";
                        }
                    }
                    else if (ddlReportType.SelectedValue == "1")
                    {
                        crSaleValue.ReportName = "aging_salevalue-branchwise";
                    }
                    else if (ddlReportType.SelectedValue == "2")
                    {
                        crSaleValue.ReportName = "aging_salevalue-supplierwise";
                    }
                    #endregion

                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion 
    }
}