#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common; 
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class ORCStatement : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
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
                    if (crORCStatement != null)
                    {
                        crORCStatement.Dispose();
                        crORCStatement = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    PopulateReportType();
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
            if (crORCStatement != null)
            {
                crORCStatement.Dispose();
                crORCStatement = null;
            }
        }
        protected void crORCStatement_Unload(object sender, EventArgs e)
        {
            if (crORCStatement != null)
            {
                crORCStatement.Dispose();
                crORCStatement = null;
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
                    crORCStatement.ReportName = ddlReportType.SelectedValue;
                    CallCrystalReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    string strSelectionFormula = null;
                    string strDateQuery = "{v_invoice.document_Date}";
                    string strSupplierQuery = "mid({v_invoice.item_code},1,3)";

                    string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                    string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                    strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                           + strDateQuery + " <= Date (" + strToDate + ") and "
                                           + strSupplierQuery + " = '" + ddlSupplierName.SelectedValue + "'";

                    crORCStatement.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crORCStatement.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crORCStatement.RecordSelectionFormula = strSelectionFormula;
                    crORCStatement.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        /// <summary>
        /// Populates the dropdown with Report Types from XML
        /// </summary>
        protected void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-ORCStatement");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
