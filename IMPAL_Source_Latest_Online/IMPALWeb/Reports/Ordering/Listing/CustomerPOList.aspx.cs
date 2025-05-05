#region Namespace
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
#endregion

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class CustomerPOList : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        #region Page Init
        /// <summary>
        /// Page Init event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region Page Load
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    if (crCustomerPO != null)
                    {
                        crCustomerPO.Dispose();
                        crCustomerPO = null;
                    }
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
            if (crCustomerPO != null)
            {
                crCustomerPO.Dispose();
                crCustomerPO = null;
            }
        }
        protected void crCustomerPO_Unload(object sender, EventArgs e)
        {
            if (crCustomerPO != null)
            {
                crCustomerPO.Dispose();
                crCustomerPO = null;
            }
        }

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                string strCryFromDate = default(string);
                string strCryToDate = default(string);
                string strBranchCodeField = default(string);
                string strDateField = default(string);
                string strReportName = default(string);
                //string strBranchCode=default(string);
                string strSelectionFormula = default(string);
                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strBranchCodeField = "{purchase_order_header.Branch_code}";
                strDateField = "{purchase_order_header.po_date}";
                strReportName = "CustomerPOList";
                if (strBranchCode == "CRP")
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + " ='" + strBranchCode + "'";

                crCustomerPO.ReportName = strReportName;
                crCustomerPO.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crCustomerPO.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crCustomerPO.RecordSelectionFormula = strSelectionFormula;
                crCustomerPO.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Button Click
        /// <summary>
        /// Report Button click
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