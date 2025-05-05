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

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class RoadPermitIssued : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {

                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crRoadPermitIssued != null)
                    {
                        crRoadPermitIssued.Dispose();
                        crRoadPermitIssued = null;
                    }

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
            if (crRoadPermitIssued != null)
            {
                crRoadPermitIssued.Dispose();
                crRoadPermitIssued = null;
            }
        }
        protected void crRoadPermitIssued_Unload(object sender, EventArgs e)
        {
            if (crRoadPermitIssued != null)
            {
                crRoadPermitIssued.Dispose();
                crRoadPermitIssued = null;
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
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    string strSelectionFormula = default(string);

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;
                    string strBranchCodeField = default(string);
                    string strDateOpened = default(string);

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strDateOpened = "{inward_header.date_opened}";
                    strBranchCodeField = "{inward_header.branch_code}";

                    if (strBranchCode == "CRP")
                        strSelectionFormula = strDateOpened + ">=" + strCryFromDate + " and " + strDateOpened + "<=" + strCryToDate;
                    else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                        strSelectionFormula = strDateOpened + ">=" + strCryFromDate + " and " + strDateOpened + "<=" + strCryToDate + " and " + strBranchCodeField + " ='" + strBranchCode + "'";

                    crRoadPermitIssued.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crRoadPermitIssued.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crRoadPermitIssued.RecordSelectionFormula = strSelectionFormula;
                    crRoadPermitIssued.GenerateReport();
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