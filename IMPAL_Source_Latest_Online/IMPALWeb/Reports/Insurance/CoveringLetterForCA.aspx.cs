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
using IMPALLibrary.Masters.Insurance;
#endregion

namespace IMPALWeb.Reports.Insurance
{
    public partial class CoveringLetterForCA : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        #region Page Init
        /// <summary>
        /// Page Init
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
                    fnGetClaimNumber();

                    if (crCoverLetterForCA != null)
                    {
                        crCoverLetterForCA.Dispose();
                        crCoverLetterForCA = null;
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
            if (crCoverLetterForCA != null)
            {
                crCoverLetterForCA.Dispose();
                crCoverLetterForCA = null;
            }
        }
        protected void crCoverLetterForCA_Unload(object sender, EventArgs e)
        {
            if (crCoverLetterForCA != null)
            {
                crCoverLetterForCA.Dispose();
                crCoverLetterForCA = null;
            }
        }

        #region Populate Claim Number
        /// <summary>
        /// Gets Claim Number
        /// </summary>
        public void fnGetClaimNumber()
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "fnGetClaimNumber", "Entering fnGetClaimNumber");
            try
            {
            Insurances insurance = new Insurances();

            ddlClaimBillNumber.DataSource = insurance.GetClaimNumber(strBranchCode);
            ddlClaimBillNumber.DataTextField = "ClaimNumber";
            ddlClaimBillNumber.DataValueField = "ClaimNumber";
            ddlClaimBillNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        /// <summary>
        /// Method to Generate Selection formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strClaimNumber = default(string);
                string strSelectionFormula = default(string);

                string strInsClaimRefField = default(string);
                string strInsClaimBranchField = default(string);
                string strReportName = default(string);
                strInsClaimRefField = "{Insurance_Claims.Claim_Reference_Number}";
                strInsClaimBranchField = "{Insurance_Claims.Branch_code}";
                strReportName = "CoverLetterForCA";
                #endregion
                #region SelectionFormula Formation
                strClaimNumber = ddlClaimBillNumber.Text;
                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                if (strBranchCode != "CRP")
                    strSelectionFormula = strInsClaimBranchField + " ='" + strBranchCode + "' and " + strInsClaimRefField + "=" + " '" + strClaimNumber + "'";
                else
                    strSelectionFormula = strInsClaimRefField + "=" + " '" + strClaimNumber + "'";
                #endregion
                crCoverLetterForCA.ReportName = strReportName;
                crCoverLetterForCA.RecordSelectionFormula = strSelectionFormula;
                crCoverLetterForCA.GenerateReport();
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
