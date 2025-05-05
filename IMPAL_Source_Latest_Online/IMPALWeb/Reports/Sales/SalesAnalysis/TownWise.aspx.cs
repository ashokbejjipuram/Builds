#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class TownWise : System.Web.UI.Page
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
                    if (crTownWise != null)
                    {
                        crTownWise.Dispose();
                        crTownWise = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    GetTownlist();
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
            if (crTownWise != null)
            {
                crTownWise.Dispose();
                crTownWise = null;
            }
        }
        protected void crTownWise_Unload(object sender, EventArgs e)
        {
            if (crTownWise != null)
            {
                crTownWise.Dispose();
                crTownWise = null;
            }
        }

        #region btnReport_Click

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        #region CallCrystalReport
        private void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = null;
            string strDocFromDate = null;
            string strDocToDate = null;
            string strToDate_PrevYear = null;
            string strDocDateCompare = null;

            string strDocDateQuery = "{V_SalesReports.Document_Date}";
            //string strSupplierQuery = "{V_SalesReports.supplier_name} = '" + ddlLineCode.SelectedItem.Text + "'";
            string strBranchQuery = "{V_SalesReports.branch_code}";
            string strTownQuery = "{Town_Master.town_code} = " + ddlTownCode.SelectedValue;

            DateTime dtFromDate = Convert.ToDateTime(hidFromDate.Value);
            DateTime dtToDate = Convert.ToDateTime(hidToDate.Value);
            string strFromDate = dtFromDate.ToString("yyyy,MM,dd");
            string strToDate = dtToDate.ToString("yyyy,MM,dd");

            int iFromDatePrevYear = (dtFromDate.Year) - 1;
            int iToDatePrevYear = (dtToDate.Year) - 1;
            int iIsLeapYear = iToDatePrevYear % 4;
            int iToDateDay = dtToDate.Day;
            int iPrevDay = 0;

            if (iIsLeapYear > 0 && iToDateDay == 29)
                iPrevDay = iToDateDay - 1;
            else
                iPrevDay = iToDateDay;

            if (dtFromDate.Month < 4)
                dtFromDate = dtFromDate.AddYears(-1);
            if (dtToDate.Month < 4)
                iFromDatePrevYear = iFromDatePrevYear - 1;

            strDocFromDate = "Date (" + dtFromDate.Year + ",04,01)";
            strDocToDate = "Date (" + iFromDatePrevYear + ",04,01)";

            if (ddlReportType.SelectedValue.Equals("Report"))
                crTownWise.ReportName = "TownWise";
            else
                crTownWise.ReportName = "TownWiseSummary";

            if (iIsLeapYear == 0)
                strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + dtToDate.Day + ")";
            else
                strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + iPrevDay + ")";

            strDocDateCompare = "((" + strDocDateQuery + " >= " + strDocFromDate + " and " + strDocDateQuery + " <= Date(" + strToDate + ")) OR ("
                              + strDocDateQuery + " >= " + strDocToDate + " and " + strDocDateQuery + " <= " + strToDate_PrevYear + "))";

            if (ddlTownCode.SelectedIndex <= 0)
                strSelectionFormula = strDocDateCompare;
            else
                strSelectionFormula = strDocDateCompare + " and " + strTownQuery;

            if (strBranchCode != "CRP")
            {
                strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
            }

            crTownWise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crTownWise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crTownWise.RecordSelectionFormula = strSelectionFormula;
            crTownWise.GenerateReportAndExport(fileType);
        }
        #endregion

        #region GetTownlist
        /// <summary>
        /// Gets the list of Towns from DB
        /// </summary>
        public void GetTownlist()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<IMPALLibrary.Town> lstTowns = null;
                Towns oTowns = new Towns();
                if (strBranchCode.Equals("CRP"))
                    lstTowns = oTowns.GetBranchBasedTowns(null);
                else
                    lstTowns = oTowns.GetBranchBasedTowns(strBranchCode);
                ddlTownCode.DataSource = lstTowns;
                ddlTownCode.DataBind();
                ddlTownCode.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        protected void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-Std");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("TownWise.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}