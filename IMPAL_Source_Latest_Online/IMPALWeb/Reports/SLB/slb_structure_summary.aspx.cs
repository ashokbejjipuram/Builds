using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.SLB
{
    public partial class slb_structure_summary : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crslbsummary != null)
                    {
                        crslbsummary.Dispose();
                        crslbsummary = null;
                    }

                    ddllinecode.Enabled = false;
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crslbsummary != null)
            {
                crslbsummary.Dispose();
                crslbsummary = null;
            }
        }
        protected void crslbsummary_Unload(object sender, EventArgs e)
        {
            if (crslbsummary != null)
            {
                crslbsummary.Dispose();
                crslbsummary = null;
            }
        }

        #region fnPopulateLineCode
        protected void fnPopulateLineCode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers objlinecode = new Suppliers();
                ddllinecode.DataSource = objlinecode.GetLineCodeforSLBSummary(ddlbranchcode.SelectedValue);
                ddllinecode.DataTextField = "SupplierName";
                ddllinecode.DataValueField = "SupplierCode";
                ddllinecode.DataBind();
                if (objlinecode != null)
                    objlinecode = null;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region report_button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strSelectionFormula = null;
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strbranchmastercode = "{Branch_Master.branch_code}";
                        string SLBItemcalc = "{SLB_Item_calculation.branch_code}";
                        string SLBLinecode = "mid({SLB_Item_calculation.Supplier_line_code},1,3)";
                        if (!string.IsNullOrEmpty(ddlbranchcode.SelectedValue))
                        {
                            ImpalLibrary oCommon = new ImpalLibrary();
                            if (strBranchCode != "CRP")
                            {
                                if (ddllinecode.SelectedValue != "")
                                {
                                    if (ddlbranchcode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + "'and " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + "'and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                    else
                                    {
                                        strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + "'and " + SLBItemcalc + "=" + "'" + strBranchCode + "'and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";

                                    }

                                }
                                else if (ddllinecode.SelectedValue == "")
                                {
                                    if (ddlbranchcode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + "'and " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + "'or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                    else
                                    {
                                        strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + "'or " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + "'or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }

                                }
                            }
                            else
                            {
                                if (ddllinecode.SelectedValue != "")
                                {
                                    if (ddlbranchcode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strbranchmastercode + "=" + "'" + ddlbranchcode.SelectedValue + "'and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                    else
                                    {
                                        strSelectionFormula = strbranchmastercode + "=" + "'" + strBranchCode + "'and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                }
                                else if (ddllinecode.SelectedValue == "")
                                {
                                    if (ddlbranchcode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strbranchmastercode + "=" + "'" + ddlbranchcode.SelectedValue + "'or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                    else
                                    {
                                        strSelectionFormula = strbranchmastercode + "=" + "'" + ddlbranchcode.SelectedValue + "'or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (strBranchCode != "CRP")
                            {
                                strSelectionFormula = strbranchmastercode + "=" + "'" + strBranchCode + "'";
                            }
                        }
                    }
                    crslbsummary.ReportName = "Impal-report-SLBstruc-summary";
                    crslbsummary.RecordSelectionFormula = strSelectionFormula;
                    crslbsummary.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlbranchcode_SelectedIndexChanged
        protected void ddlbranchcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddllinecode.Enabled = true;
                fnPopulateLineCode();
            }


            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}