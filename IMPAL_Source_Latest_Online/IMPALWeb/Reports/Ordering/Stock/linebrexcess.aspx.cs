#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class linebrexcess : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page Init
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
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crlinebrexcess != null)
                    {
                        crlinebrexcess.Dispose();
                        crlinebrexcess = null;
                    }

                    ddbranchcode.Enabled = false;
                    fnPopulateMonthYear();
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
            if (crlinebrexcess != null)
            {
                crlinebrexcess.Dispose();
                crlinebrexcess = null;
            }
        }
        protected void crlinebrexcess_Unload(object sender, EventArgs e)
        {
            if (crlinebrexcess != null)
            {
                crlinebrexcess.Dispose();
                crlinebrexcess = null;
            }
        }

        #region Monthyear dropdown Populate Method
        protected void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LineWiseSales S = new LineWiseSales();
                ddmonthyear.DataSource = S.GetMonthYear(null);
                ddmonthyear.DataValueField = "MonthYear";
                ddmonthyear.DataTextField = "MonthYear";
                ddmonthyear.DataBind();
                ddmonthyear.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Line Code dropdown selected Index changed Method
        protected void drpdown_selidxchanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlinecode.SelectedValue != "0")
                {
                    ddbranchcode.Enabled = true;
                }
                else
                {
                    ddbranchcode.Enabled = false;
                }
                Branches b = new Branches();
                ddbranchcode.DataSource = b.GetAllLinewiseBranches(ddlinecode.SelectedValue.ToString(), "WithLine", sessionvalue);
                ddbranchcode.DataTextField = "BranchName";
                ddbranchcode.DataValueField = "BranchCode";
                ddbranchcode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string selectionformula = string.Empty;
                    string brchcode = ddbranchcode.SelectedValue;
                    string linecode = ddlinecode.SelectedValue;
                    string monthyear = ddmonthyear.SelectedValue;
                    string selbranch = "{Branch_Master.branch_code}";

                    if (sessionvalue != "CRP")
                    {
                        if ((ddbranchcode.SelectedValue == "0") && (ddlinecode.SelectedValue == "0") && (ddmonthyear.SelectedValue == "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = selbranch + " = '" + sessionvalue + "'";
                        }
                        else if ((ddbranchcode.SelectedValue == "") && (ddlinecode.SelectedValue == "0") && (ddmonthyear.SelectedValue == ""))
                        {
                            crlinebrexcess.RecordSelectionFormula = selbranch + " = '" + sessionvalue + "'";
                        }
                        else if ((ddbranchcode.SelectedValue == "0") && (ddlinecode.SelectedValue != "0") && (ddmonthyear.SelectedValue == ""))
                        {
                            crlinebrexcess.RecordSelectionFormula = selbranch + " = '" + sessionvalue + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue == "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = selbranch + " = '" + brchcode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + sessionvalue + "' " + " and {Line_wise_sales.month_year}= '" + monthyear + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + sessionvalue + "' and {Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + sessionvalue + "' and {Line_wise_sales.month_year}= '" + monthyear + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue == "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + sessionvalue + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            if (ddmonthyear.SelectedValue == "")
                            {
                                crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                            }
                            else
                            {
                                crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'" + " and {Line_wise_sales.month_year}= '" + monthyear + "'";
                            }
                        }
                    }
                    else if (sessionvalue == "CRP")
                    {
                        if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue != "") && (ddmonthyear.SelectedValue == ""))
                        {
                            crlinebrexcess.RecordSelectionFormula = " mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue == "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = selbranch + " = '" + brchcode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'";

                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue == "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + brchcode + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue == "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "' and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue != "0") && (ddbranchcode.SelectedValue != "0") && (ddmonthyear.SelectedValue != "0"))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{branch_master.branch_code} = '" + brchcode + "' and {Line_wise_sales.month_year}= '" + monthyear + "'" + " and mid({Line_wise_sales.Supplier_line_code},1,3) = '" + linecode + "'";
                        }
                        else if ((ddlinecode.SelectedValue == "0") && (ddbranchcode.SelectedValue == "") && ((ddmonthyear.SelectedValue != "0") && (ddmonthyear.SelectedValue != "")))
                        {
                            crlinebrexcess.RecordSelectionFormula = "{Line_wise_sales.month_year}= '" + monthyear + "'";
                        }
                    }
                    crlinebrexcess.ReportName = "Impal_linebrexcess";
                    crlinebrexcess.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlinecode_selectedindexchanged(object sender, EventArgs e)
        {

        }
    }
}