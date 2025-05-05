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
    public partial class ClaimClassification : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
        /// <summary>
        /// Page init event
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
                    fnGetClassificationcode();

                    if (crClassificationCode != null)
                    {
                        crClassificationCode.Dispose();
                        crClassificationCode = null;
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
            if (crClassificationCode != null)
            {
                crClassificationCode.Dispose();
                crClassificationCode = null;
            }
        }
        protected void crClassificationCode_Unload(object sender, EventArgs e)
        {
            if (crClassificationCode != null)
            {
                crClassificationCode.Dispose();
                crClassificationCode = null;
            }
        }

        #region Populate ClassificationCode
        /// <summary>
        /// Gets Classification Code
        /// </summary>
        public void fnGetClassificationcode()
        {
              Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
              //Log.WriteLog(source, "fnGetClassificationcode", "Entering fnGetClassificationcode");
            try
            {
            Insurances insurance = new Insurances();

            ddlClassificationCode.DataSource = insurance.GetClassificationCode(strBranchCode);
            ddlClassificationCode.DataTextField = "ClassificationCodes";
            ddlClassificationCode.DataValueField = "ClassificationCodes";
            ddlClassificationCode.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        /// <summary>
        /// Method to Generate Selection Formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Entering GenerateSelectionFormula()");
            try
            {
                #region Declaration
                string strClassificationCode = default(string);
                string strSelectionFormula = default(string);
                strClassificationCode = ddlClassificationCode.Text;
                string strClassificationCodeField = default(string);
                string strClassBranchCodeField = default(string);
                string strReportName = default(string);
                strClassificationCodeField = "{Insurance_Claims.Classification_Code}";
                strClassBranchCodeField = "{Insurance_Claims.Branch_Code}";
                strReportName = "ClaimClassification";
                #endregion

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                #region SelectionFormula Formation
                if (strClassificationCode == "")
                {
                    strSelectionFormula = "";
                }
                else if (strBranchCode == "CRP")
                {
                    strSelectionFormula = strClassificationCodeField + "=" + " " + strClassificationCode;
                }
                else
                {
                    strSelectionFormula = strClassificationCodeField + "=" + " " + strClassificationCode + "and" +
                                         strClassBranchCodeField + "= '" + strBranchCode + "'";
                }
                #endregion
                crClassificationCode.ReportName = strReportName;
                crClassificationCode.RecordSelectionFormula = strSelectionFormula;
                crClassificationCode.GenerateReport();
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
