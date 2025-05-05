#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class FundsTransfer : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string strReportName = default(string);

        #region Page Init
        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
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
            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
            if (!IsPostBack)
            {
                if (crFundsTransfer != null)
                {
                    crFundsTransfer.Dispose();
                    crFundsTransfer = null;
                }

                fnPopulateReportType();
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crFundsTransfer != null)
            {
                crFundsTransfer.Dispose();
                crFundsTransfer = null;
            }
        }
        protected void crFundsTransfer_Unload(object sender, EventArgs e)
        {
            if (crFundsTransfer != null)
            {
                crFundsTransfer.Dispose();
                crFundsTransfer = null;
            }
        }

        #region Populate Report Type
        /// <summary>
        /// Populate report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Populating ReportsType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-Std");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        /// <summary>
        /// Method to generate selection formula  
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strFromDate = default(string);
                string strToDate = default(string);
                string strCryFromDate = default(string);
                string strCryToDate = default(string);
                //string strReportName = default(string);
                string strSelectionFormula = default(string);
                string strDateField = default(string);
                int intProcStatus = default(int);
                bool blnFlag = false;
                #endregion
                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;
                strDateField = "{v_fundstransfer.document_date}";
                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;

                crFundsTransfer.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crFundsTransfer.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crFundsTransfer.ReportName = strReportName;
                crFundsTransfer.RecordSelectionFormula = strSelectionFormula;
                crFundsTransfer.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

        #region Button Click
        /// <summary>
        /// Report Button Click
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
                    if (ddlReportType.SelectedValue == "Report")
                    {
                        strReportName = "FundsTransferReport";
                    }
                    else
                    {
                        strReportName = "FundsTransferSummary";
                    }
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