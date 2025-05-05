using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class PartyDoc : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
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
                    if (crSalesPartyDocument != null)
                    {
                        crSalesPartyDocument.Dispose();
                        crSalesPartyDocument = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crSalesPartyDocument != null)
            {
                crSalesPartyDocument.Dispose();
                crSalesPartyDocument = null;
            }
        }
        protected void crSalesPartyDocument_Unload(object sender, EventArgs e)
        {
            if (crSalesPartyDocument != null)
            {
                crSalesPartyDocument.Dispose();
                crSalesPartyDocument = null;
            }
        }

        #region Generate Selection Formula
        /// <summary>
        /// Method to generate Selection formula 
        /// </summary>
        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    string strSelectionFormula = null;
                    string strFromDate = null;
                    string strToDate = null;
                    string strDocDateQuery = "{V_SalesReports.document_date}";
                    string strBranchQuery = "{V_SalesReports.Branch_code}";
                    if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    {
                        strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                        strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                        string strDateCompare = strDocDateQuery + " >= Date (" + strFromDate + ") and "
                                                + strDocDateQuery + " <= Date (" + strToDate + ")";

                        if (strBranchCode.Equals("CRP"))
                        {
                            strSelectionFormula = strDateCompare;
                        }
                        else
                        {
                            strSelectionFormula = strDateCompare + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                        }

                        crSalesPartyDocument.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                        crSalesPartyDocument.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                        crSalesPartyDocument.RecordSelectionFormula = strSelectionFormula;
                        crSalesPartyDocument.GenerateReport();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion 

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
                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
