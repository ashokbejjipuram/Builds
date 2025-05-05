#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class LineWise : System.Web.UI.Page
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
                    if (crLineWise != null)
                    {
                        crLineWise.Dispose();
                        crLineWise = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    Towns oTowns = new Towns();
                    if (strBranchCode.Equals("CRP"))
                        ddlTownCode.DataSource = oTowns.GetAllTowns(null);
                    else
                        ddlTownCode.DataSource = oTowns.GetAllTowns(strBranchCode);
                    ddlTownCode.DataBind();
                    ddlTownCode.Items.Insert(0, string.Empty);
                    ddlLineCode.Items.Insert(0, string.Empty);
                    PopulateReportType();
                    UpdateAutomobileItems();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crLineWise != null)
            {
                crLineWise.Dispose();
                crLineWise = null;
            }
        }
        protected void crLineWise_Unload(object sender, EventArgs e)
        {
            if (crLineWise != null)
            {
                crLineWise.Dispose();
                crLineWise = null;
            }
        }

        private void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlTownCode.SelectedIndex > 0)
                {
                    ddlReportType.Items.Clear();
                    ddlReportType.Items.Add("Town Wise");
                }
                else
                {
                    ddlReportType.Items.Clear();
                    PopulateReportType();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
            if (ddlReportType.SelectedValue.Equals("Value"))
                crLineWise.ReportName = "LineWise";
            else if (ddlReportType.SelectedValue.Equals("Quantity"))
                crLineWise.ReportName = "LineWise-Qty";
            else if (ddlReportType.SelectedValue.Equals("Details"))
                crLineWise.ReportName = "LineWise-Dtls";
            else
                crLineWise.ReportName = "LineWise-TownWise";

            string strSelectionFormula = null;
            string strDocFromDate = null;
            string strDocToDate = null;
            string strToDate_PrevYear = null;
            string strDocDateCompare = null;

            string strDocDateQuery = "{V_SalesReports.Document_Date}";
            string strSupplierQuery = "{V_SalesReports.supplier_name} = '" + ddlLineCode.SelectedItem.Text + "'";
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

            #region Dates Setting
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

            if (iIsLeapYear == 0)
                strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + dtToDate.Day + ")";
            else
                strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + iPrevDay + ")";

            strDocDateCompare = "((" + strDocDateQuery + " >= " + strDocFromDate + " and " + strDocDateQuery + " <= Date(" + strToDate + ")) OR ("
                              + strDocDateQuery + " >= " + strDocToDate + " and " + strDocDateQuery + " <= " + strToDate_PrevYear + "))";
            #endregion

            #region Selection Formula formation
            if (ddlLineCode.SelectedIndex == 0)
            {
                if (ddlTownCode.SelectedIndex == 0)
                    strSelectionFormula = strDocDateCompare;
                else
                    strSelectionFormula = strDocDateCompare + " and " + strTownQuery;
            }
            else if (ddlLineCode.SelectedIndex > 0)
            {
                string str = "160/410/390/630/450";
                if (str.Contains(ddlLineCode.SelectedValue))
                {
                    string strSuppQuery = GetSupplierValues();
                    if (ddlTownCode.SelectedIndex == 0)
                        strSelectionFormula = strSuppQuery + " and " + strDocDateCompare;
                    else
                        strSelectionFormula = strSuppQuery + " and " + strDocDateCompare + " and " + strTownQuery;
                }
                else
                {
                    if (ddlTownCode.SelectedIndex == 0)
                        strSelectionFormula = "(" + strSupplierQuery + " and " + strDocDateCompare + ")";
                    else
                        strSelectionFormula = "(" + strSupplierQuery + " and " + strDocDateCompare + ") and " + strTownQuery;
                }
            }

            if (strBranchCode != "CRP")
            {
                strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
            }

            #endregion

            crLineWise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crLineWise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crLineWise.RecordSelectionFormula = strSelectionFormula;
            crLineWise.GenerateReportAndExport(fileType);
        }
        #endregion

        #region GetSupplierValues
        private string GetSupplierValues()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strQuery = null;
            try
            {
                strQuery = "({V_SalesReports.supplier_name} = ";
                if (ddlLineCode.SelectedValue.Equals("160"))
                    strQuery = strQuery + "'161-BRAKES INDIA LIMITED-Non Friction' or {V_SalesReports.supplier_name}='160-BRAKES INDIA LIMITED-Friction')";
                else if (ddlLineCode.SelectedValue.Equals("410"))
                    strQuery = strQuery + "'410-SF AUTO HEX' or {V_SalesReports.supplier_name}='415-SF INDL HEX')";
                else if (ddlLineCode.SelectedValue.Equals("390") || ddlLineCode.SelectedValue.Equals("630"))
                    strQuery = strQuery + "'390-RANE (MADRAS) LIMITED')";
                else if (ddlLineCode.SelectedValue.Equals("450"))
                    strQuery = strQuery + "'450-WABCO USP ITEMS' or {V_SalesReports.supplier_name}='451-WABCO NON USP ITEMS')";
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            return strQuery;
        }
        #endregion

        #region PopulateReportType
        private void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-ValQtyDtl");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region UpdateAutomobileItems
        private void UpdateAutomobileItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Vehicles oVehicles = new Vehicles();
                oVehicles.UpdateAutomobileItem();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlTownCode_IndexChanged(object sender, EventArgs e)
        {
            LoadReportType();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LineWise.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}