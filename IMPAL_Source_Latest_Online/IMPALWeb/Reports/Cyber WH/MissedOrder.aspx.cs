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


namespace IMPALWeb.Reports.Cyber_WH
{
    public partial class MissedOrder : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        #region Page Init
        /// <summary>
        /// To Initialize page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {

            if (Session["BranchCode"] != null)
            {
                strBranchCode = (string)Session["BranchCode"];
            }
            if (!IsPostBack)
            {
                    if (crMissedOrder != null)
                    {
                        crMissedOrder.Dispose();
                        crMissedOrder = null;
                    }

                    loadPO(strBranchCode);
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
            if (crMissedOrder != null)
            {
                crMissedOrder.Dispose();
                crMissedOrder = null;
            }
        }
        protected void crMissedOrder_Unload(object sender, EventArgs e)
        {
            if (crMissedOrder != null)
            {
                crMissedOrder.Dispose();
                crMissedOrder = null;
            }
        }
        #region btnReport_Click
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
                    string strPOValue = ddlPONumber.Text;

                    string strPONumber = "{Purchase_Order_Header.PO_number}";
                    string strFrombranchcode = "{Purchase_Order_Header.branch_code}";
                    string rptName = "Impal-report-branch_transfer_missed_stmt_new";

                    if (strBranchCode.Equals("CRP"))
                        strSelectionFormula = strPONumber + "=" + " " + "'" + strPOValue + "'";
                    else
                        strSelectionFormula = strPONumber + "=" + " " + "'" + strPOValue + "'and " + strFrombranchcode + "='" + strBranchCode + "'";

                    crMissedOrder.ReportName = rptName;
                    crMissedOrder.RecordSelectionFormula = strSelectionFormula;
                    crMissedOrder.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region loadPO
        protected void loadPO(string branch)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "loadPO", "Entering loadPO");
            try
            {
            IMPALLibrary.Masters.PONumber.PONumbers ponumbers = new IMPALLibrary.Masters.PONumber.PONumbers();
            List<IMPALLibrary.Masters.PONumber.PONumber> lstPOnumber = new List<IMPALLibrary.Masters.PONumber.PONumber>();
            lstPOnumber = ponumbers.GetPONumber(branch);
            ddlPONumber.DataSource = lstPOnumber;
            ddlPONumber.DataValueField = "PONumbers";
            ddlPONumber.DataTextField = "PONumbers";
            ddlPONumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

    }
}


