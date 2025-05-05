using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.SLB
{
    public partial class gpstatementforlines : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

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
                    if (crslbgpstateforlines != null)
                    {
                        crslbgpstateforlines.Dispose();
                        crslbgpstateforlines = null;
                    }

                    if (Convert.ToString(Session["BranchCode"]) != "CRP")
                    {
                        GetZoneBranch(strBranchCode);
                        fnPopulateLineCode();
                    }
                    else
                        fnPopulateZone();
                }

                //PopulateBranchCode();
                //fnPopulateLineCode();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crslbgpstateforlines != null)
            {
                crslbgpstateforlines.Dispose();
                crslbgpstateforlines = null;
            }
        }
        protected void crslbgpstateforlines_Unload(object sender, EventArgs e)
        {
            if (crslbgpstateforlines != null)
            {
                crslbgpstateforlines.Dispose();
                crslbgpstateforlines = null;
            }
        }

        #region GetZoneBranch
        protected void GetZoneBranch(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Branches objBranch = new Branches();
                List<Branch> objBra = objBranch.GetBranchAndZone(strBranchCode);
                ddlZone.DataSource = objBra;
                ddlbranchcode.DataSource = objBra;
                ddlZone.DataTextField = "ZoneName";
                ddlZone.DataValueField = "ZoneCode";

                ddlbranchcode.DataTextField = "BranchName";
                ddlbranchcode.DataValueField = "BranchCode";

                ddlZone.DataBind();
                ddlbranchcode.DataBind();

                ddlbranchcode.Enabled = false;
                ddlZone.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateZone
        protected void fnPopulateZone()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Zones oZone = new Zones();
                ddlZone.DataSource = oZone.GetAllZones();
                ddlZone.DataTextField = "ZoneName";
                ddlZone.DataValueField = "ZoneCode";
                ddlZone.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlZone_OnSelectedIndexChanged
        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            PopulateBranchCode();
        }
        #endregion

        #region PopulateBranchCode
        protected void PopulateBranchCode()
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Branches obranch = new Branches();
                //ddlbranchcode.DataSource = obranch.SLBItembranchcode(ddlZone.SelectedValue);
                ddlbranchcode.DataSource = obranch.GetBranchBasedonZoneState(ddlZone.SelectedValue);
                ddlbranchcode.DataTextField = "BranchName";
                ddlbranchcode.DataValueField = "BranchCode";
                ddlbranchcode.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlbranchcode_OnSelectedIndexChanged
        protected void ddlbranchcode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulateLineCode();
        }
        #endregion

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
                // if (objlinecode != null)
                //  objlinecode = null;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Generate Report
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;

                        string strbranchmastercode = "{Branch_Master.branch_code}";
                        string SLBItemcalc = "{SLB_Item_calculation.branch_code}";
                        string SLBLinecode = "mid({SLB_Item_calculation.Supplier_line_code},1,3)";
                        string SLBZonecode = "{Zone_Master.Zone_Code}";
                        if (strBranchCode != "CRP")
                        {
                            if (ddllinecode.SelectedValue != "")
                            {
                                if (ddlbranchcode.SelectedValue != "")
                                {

                                    strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + " ' and " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + " ' and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + " ' and " + SLBZonecode + "=" + ddlZone.SelectedValue;
                                }
                                else
                                {
                                    strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + " ' and " + SLBItemcalc + "=" + "'" + strBranchCode + " ' and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                }
                            }
                            else
                            {
                                if (ddlbranchcode.SelectedValue != "")
                                {
                                    strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + " ' and " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + " ' or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                }
                                else
                                {
                                    strSelectionFormula = strbranchmastercode + " ='" + strBranchCode + " ' or " + SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + " ' or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                }
                            }
                        }
                        else
                        {
                            if (ddllinecode.SelectedValue != "")
                            {
                                if (ddlbranchcode.SelectedValue != "")
                                {
                                    strSelectionFormula = SLBItemcalc + "=" + "'" + ddlbranchcode.SelectedValue + " ' and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + " ' and " + SLBZonecode + "=" + ddlZone.SelectedValue;
                                }
                                else
                                {
                                    strSelectionFormula = strbranchmastercode + "=" + "'" + strBranchCode + " ' and " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                }
                            }
                            else
                            {
                                if (ddlbranchcode.SelectedValue != "")
                                {
                                    strSelectionFormula = strbranchmastercode + "=" + "'" + ddlbranchcode.SelectedValue + " ' or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";
                                }
                                else
                                {
                                    strSelectionFormula = strbranchmastercode + "=" + "'" + ddlbranchcode.SelectedValue + " ' or " + SLBLinecode + "=" + " '" + ddllinecode.SelectedValue + "'";

                                }

                            }

                        }

                        crslbgpstateforlines.ReportName = "Impal-report-GPstatement";
                        crslbgpstateforlines.RecordSelectionFormula = strSelectionFormula;
                        crslbgpstateforlines.GenerateReport();
                    }
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