using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.Inventory
{
    public partial class EditListforTransaction : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crListTransaction != null)
                    {
                        crListTransaction.Dispose();
                        crListTransaction = null;
                    }

                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
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
            if (crListTransaction != null)
            {
                crListTransaction.Dispose();
                crListTransaction = null;
            }
        }
        protected void crListTransaction_Unload(object sender, EventArgs e)
        {
            if (crListTransaction != null)
            {
                crListTransaction.Dispose();
                crListTransaction = null;
            }
        }

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
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        string strFromDate = null;
                        string strToDate = null;
                        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                        {
                            if (!ddltrancode.SelectedValue.Equals(""))
                            {
                                ImpalLibrary oCommon = new ImpalLibrary();
                                strFromDate = (Convert.ToDateTime(hidFromDate.Value)).ToString("yyyy,MM,dd");
                                strToDate = (Convert.ToDateTime(hidToDate.Value)).ToString("yyyy,MM,dd");
                                string strInwardDate = "{inward_header.inward_date}";
                                string strTranTypeCode = "{inward_header.Transaction_Type_Code}";
                                string strBranchQuery = "{inward_header.Branch_code}";
                                string strDateCompare = strInwardDate + " >= Date (" + strFromDate + ") and "
                                                        + strInwardDate + " <= Date (" + strToDate + ")";
                               
                                if (strBranchCode.Equals("CRP"))
                                {
                                    if (ddltrancode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strTranTypeCode + "=" + "'" + ddltrancode.SelectedValue + "'" + " and " + strDateCompare;
                                    }
                                    else
                                    {
                                        strSelectionFormula = strDateCompare;
                                    }
                                }
                                else
                                {
                                    if (ddltrancode.SelectedValue != "")
                                    {
                                        strSelectionFormula = strTranTypeCode + "=" + "'" + ddltrancode.SelectedValue + "'" + " and " + strDateCompare + "and" + strBranchQuery + "='" + strBranchCode + "'";
                                    }
                                    else
                                    {
                                        strSelectionFormula = strDateCompare + "and" + strBranchQuery + "='" + strBranchCode + "'";
                                    }
                                }
                            }
                        }

                        crListTransaction.ReportName = "impal_editlistfortran";
                        crListTransaction.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                        crListTransaction.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                        crListTransaction.RecordSelectionFormula = strSelectionFormula;
                        crListTransaction.GenerateReport();
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