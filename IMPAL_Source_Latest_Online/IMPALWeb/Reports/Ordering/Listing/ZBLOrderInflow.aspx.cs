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
    public partial class ZBLOrderInflow : System.Web.UI.Page
    {
        string strBranchCode = default(string);

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
                    if (crZBLOrderInflow != null)
                    {
                        crZBLOrderInflow.Dispose();
                        crZBLOrderInflow = null;
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
            if (crZBLOrderInflow != null)
            {
                crZBLOrderInflow.Dispose();
                crZBLOrderInflow = null;
            }
        }
        protected void crZBLOrderInflow_Unload(object sender, EventArgs e)
        {
            if (crZBLOrderInflow != null)
            {
                crZBLOrderInflow.Dispose();
                crZBLOrderInflow = null;
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
                    string strBranchCodeField = default(string);
                    string strPODate = default(string);

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;
                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strPODate = "{purchase_order_header.PO_date}";
                    strBranchCodeField = "{purchase_order_header.branch_code}";

                    if (strBranchCode == "CRP")
                        strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate;
                    else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                        strSelectionFormula = strPODate + ">=" + strCryFromDate + " and " + strPODate + "<=" + strCryToDate + " and " + strBranchCodeField + " ='" + strBranchCode + "'";

                    crZBLOrderInflow.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crZBLOrderInflow.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crZBLOrderInflow.RecordSelectionFormula = strSelectionFormula;
                    crZBLOrderInflow.GenerateReport();
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