#region NameSpace Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class LinewiseinQty : System.Web.UI.Page
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crlinewise != null)
                    {
                        crlinewise.Dispose();
                        crlinewise = null;
                    }

                    fnPopulateAccountingPeriod();
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
            if (crlinewise != null)
            {
                crlinewise.Dispose();
                crlinewise = null;
            }
        }
        protected void crlinewise_Unload(object sender, EventArgs e)
        {
            if (crlinewise != null)
            {
                crlinewise.Dispose();
                crlinewise = null;
            }
        }

        #region Populate Accounting Period Dropdown
        private void fnPopulateAccountingPeriod()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods accperiod = new AccountingPeriods();
                ddlYear.DataSource = accperiod.GetAccountingPeriod(20, null, sessionvalue);
                ddlYear.DataTextField = "Desc";
                ddlYear.DataValueField = "AccPeriodCode";
                ddlYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string sel1 = "{line_wise_sales.Accounting_Period_Code}=";
                    string sel2 = "mid({line_wise_sales.supplier_line_code},1,3)='";
                    string sel3 = " {line_wise_sales.branch_code}='";
                    string selectionformula = string.Empty;

                    string strsupplier = ddlLinecode.SelectedValue;
                    int intsupplier = ddlLinecode.SelectedIndex;
                    string stryear = ddlYear.SelectedValue;
                    int intyear = ddlYear.SelectedIndex;

                    #region Selection Formula Logic
                    if (sessionvalue != "CRP")
                    {
                        if (intyear != 0)
                        {
                            if (intsupplier != 0)
                            {
                                selectionformula = sel1 + stryear + " and " + sel2 + strsupplier + "'" + " and " + sel3 + sessionvalue + "'";
                            }
                            else
                            {
                                selectionformula = sel1 + stryear + " and " + sel3 + sessionvalue + "'";
                            }
                        }
                        else
                        {
                            if (intsupplier != 0)
                            {
                                selectionformula = sel2 + strsupplier + "'" + "and" + sel3 + sessionvalue + "'";
                            }
                            else
                            {
                                selectionformula = sel3 + sessionvalue + "'";
                            }

                        }

                    }

                    else
                    {
                        if (intyear != 0)
                        {
                            if (intsupplier != 0)
                            {
                                selectionformula = sel1 + stryear + " and " + sel2 + strsupplier + "'";

                            }
                            else
                            {
                                selectionformula = sel1 + stryear;
                            }
                        }
                        else
                        {
                            if (intsupplier != 0)
                            {
                                selectionformula = sel2 + strsupplier + "'";

                            }
                            else
                            {
                                selectionformula = null;
                            }

                        }

                    }
                    #endregion

                    crlinewise.RecordSelectionFormula = selectionformula;
                    crlinewise.GenerateReport();
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
