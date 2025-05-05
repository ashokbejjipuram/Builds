#region Namespace Declaration
using IMPALLibrary;
using System;
using System.Collections.Generic;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{

    public partial class brlineexcess : System.Web.UI.Page
    {
        string sessionbrchcode = string.Empty;
        string selectionformula = string.Empty;
        string strbrchcode = string.Empty;

        #region Page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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
                    sessionbrchcode = (string)Session["BranchCode"];

                strbrchcode = sessionbrchcode;
                if (!IsPostBack)
                {
                    if (crbrlineexcess != null)
                    {
                        crbrlineexcess.Dispose();
                        crbrlineexcess = null;
                    }

                    fnPopulateSupplier();
                    fnPopulateMonthYear();
                    fnPopulateBranch();
                    if (sessionbrchcode != "CRP")
                    {
                        ddlBranchCode.SelectedValue = sessionbrchcode;
                        ddlBranchCode.Enabled = false;
                    }
                    else
                        ddlBranchCode.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crbrlineexcess != null)
            {
                crbrlineexcess.Dispose();
                crbrlineexcess = null;
            }
        }
        protected void crbrlineexcess_Unload(object sender, EventArgs e)
        {
            if (crbrlineexcess != null)
            {
                crbrlineexcess.Dispose();
                crbrlineexcess = null;
            }
        }

        #region Supplier Dropdown Populate Method
        protected void fnPopulateSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers objSup = new Suppliers();
                ddlLineCode.DataSource = objSup.GetAllLinewiseSuppliers(strbrchcode);
                ddlLineCode.DataValueField = "SupplierCode";
                ddlLineCode.DataTextField = "SupplierName";
                ddlLineCode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Method to Populate Month and year
        protected void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LineWiseSales objLine = new LineWiseSales();
                ddlMonthYear.DataSource = objLine.GetMonthYear(strbrchcode);
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Branch Dropdown Populate Method
        protected void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Branches br = new IMPALLibrary.Branches();
                ddlBranchCode.DataSource = br.GetAllBranch();
                ddlBranchCode.DataValueField = "BranchCode";
                ddlBranchCode.DataTextField = "BranchName";
                ddlBranchCode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region BranchDropdown Index changed
        protected void brdd_indexchanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlMonthYear.Enabled = true;
                ddlLineCode.Enabled = true;
                strbrchcode = ddlBranchCode.SelectedValue;
                Suppliers S = new Suppliers();
                ddlLineCode.DataSource = S.GetAllLinewiseSuppliers(strbrchcode);
                ddlLineCode.DataValueField = "SupplierCode";
                ddlLineCode.DataTextField = "SupplierName";
                ddlLineCode.DataBind();

                LineWiseSales l = new LineWiseSales();
                ddlMonthYear.DataSource = l.GetMonthYear(strbrchcode);
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string brchcode = ddlBranchCode.SelectedValue;
                    string linecode = ddlLineCode.SelectedValue;
                    string monthyear = ddlMonthYear.SelectedValue;
                    string selbranch = "{Branch_Master.branch_code}=";

                    if (sessionbrchcode != "CRP")
                    {
                        if ((ddlBranchCode.SelectedIndex == 0) && (ddlLineCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + sessionbrchcode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + sessionbrchcode + "' and {Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + "'" + sessionbrchcode + "' and {Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "' and {Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + sessionbrchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'" + " and {Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + "'" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'and {Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                    }
                    else if (sessionbrchcode == "CRP")
                    {
                        if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            if (ddlBranchCode.SelectedIndex == 0)
                            {
                                crbrlineexcess.RecordSelectionFormula = string.Empty;
                            }
                            else
                            { crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "'"; }

                        }
                        else if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'"; ;

                        }
                        else if ((ddlLineCode.SelectedIndex == 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + " ' and {Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex != 0) && (ddlMonthYear.SelectedIndex != 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = selbranch + " '" + brchcode + "' and {Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                        else if ((ddlLineCode.SelectedIndex != 0) && (ddlBranchCode.SelectedIndex == 0) && (ddlMonthYear.SelectedIndex == 0))
                        {
                            crbrlineexcess.RecordSelectionFormula = " mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";

                        }
                    }
                    crbrlineexcess.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}