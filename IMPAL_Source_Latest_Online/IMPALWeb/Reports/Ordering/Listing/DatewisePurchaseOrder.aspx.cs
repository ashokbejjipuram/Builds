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
    public partial class DatewisePurchaseOrderReport : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
        /// <summary>
        /// Page Init method
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
        /// Page load event
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
                    //ddlBranchCode.Text = strBranchCode;

                    if (crDatewisePurchaseOrder != null)
                    {
                        crDatewisePurchaseOrder.Dispose();
                        crDatewisePurchaseOrder = null;
                    }
                }
                if (strBranchCode != "CRP")
                {
                    ddlBranchCode.Text = strBranchCode;
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
            if (crDatewisePurchaseOrder != null)
            {
                crDatewisePurchaseOrder.Dispose();
                crDatewisePurchaseOrder = null;
            }
        }
        protected void crDatewisePurchaseOrder_Unload(object sender, EventArgs e)
        {
            if (crDatewisePurchaseOrder != null)
            {
                crDatewisePurchaseOrder.Dispose();
                crDatewisePurchaseOrder = null;
            }
        }

        #region Generate selection Formula 
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                string strBranchCode = default(string);
                string strLineCode = default(string);
                string strCryFromDate = default(string);
                string strCryToDate = default(string);
                string strBranchCodeField = default(string);
                string strSupplierCodeField = default(string);
                string strDateField = default(string);
                string strReportName = default(string);
                string strSelectionFormula = default(string);
                strBranchCode = ddlBranchCode.Text;
                strLineCode = ddlLineCode.Text;
                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                if (!string.IsNullOrEmpty(strFromDate))
                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                if (!string.IsNullOrEmpty(strToDate))
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                strBranchCodeField = "{Purchase_Order_Header.Branch_code}";
                strSupplierCodeField = "{Purchase_Order_Header.Supplier_Code}";
                strDateField = "{Purchase_Order_Header.PO_Date}";
                strReportName = "DatewisePurchaseOrder";
                if (strBranchCode == "" && strLineCode == "" && strFromDate == "" && strToDate == "")
                {
                    strSelectionFormula = "";
                }
                else if (strBranchCode != "" && strLineCode == "" && strFromDate == "" && strToDate == "")
                {
                    strSelectionFormula = strBranchCodeField + "='" + strBranchCode + "'";
                }
                else if (strBranchCode == "" && strLineCode != "" && strFromDate == "" && strToDate == "")
                {
                    strSelectionFormula = strSupplierCodeField + "='" + strLineCode + "'";
                }
                else if (strBranchCode != "" && strLineCode != "" && strFromDate == "" && strToDate == "")
                {
                    strSelectionFormula = strBranchCodeField + "='" + strBranchCode + "' and " +
                                          strSupplierCodeField + "='" + strLineCode + "'";
                }
                else if (strBranchCode == "" && strLineCode == "" && strFromDate != "" && strToDate != "")
                {
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " +
                                        strDateField + "<=" + strCryToDate;
                }
                else if (strBranchCode != "" && strLineCode == "" && strFromDate != "" && strToDate != "")
                {
                    strSelectionFormula = strBranchCodeField + "='" + strBranchCode + "' and " +
                                          strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                }
                else if (strBranchCode == "" && strLineCode != "" && strFromDate != "" && strToDate != "")
                {
                    strSelectionFormula = strSupplierCodeField + "='" + strLineCode + "' and " +
                                          strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                }
                else if (strBranchCode != "" && strLineCode != "" && strFromDate != "" && strToDate != "")
                {
                    strSelectionFormula = strBranchCodeField + ">='" + strBranchCode + "' and " + strSupplierCodeField + "='" + strLineCode +
                                          "' and " + strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                }

                crDatewisePurchaseOrder.ReportName = strReportName;
                crDatewisePurchaseOrder.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crDatewisePurchaseOrder.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crDatewisePurchaseOrder.RecordSelectionFormula = strSelectionFormula;
                crDatewisePurchaseOrder.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Button Click
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