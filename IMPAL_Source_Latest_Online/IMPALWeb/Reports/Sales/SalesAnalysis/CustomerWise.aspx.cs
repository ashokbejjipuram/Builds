#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Common;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Reports.Sales.SalesAnalysis
{
    public partial class CustomerWise : System.Web.UI.Page
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
                    if (crCustomerWise != null)
                    {
                        crCustomerWise.Dispose();
                        crCustomerWise = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    GetCustomerList();
                    PopulateReportType();

                    //TextBox txtCustomerName = cbCustomerName.FindControl("cbCustomerName_TextBox") as TextBox;
                    //if (txtCustomerName != null)
                    //    txtCustomerName.Attributes.Add("OnChange", "cbCustomerName_onBlur(this);");
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
            if (crCustomerWise != null)
            {
                crCustomerWise.Dispose();
                crCustomerWise = null;
            }
        }
        protected void crCustomerWise_Unload(object sender, EventArgs e)
        {
            if (crCustomerWise != null)
            {
                crCustomerWise.Dispose();
                crCustomerWise = null;
            }
        }

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                cbCustomerName.DataSource = lstCompletion;
                cbCustomerName.DataTextField = "Name";
                cbCustomerName.DataValueField = "Code";
                cbCustomerName.DataBind();
                cbCustomerName.SelectedIndex = 0;
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
            if (!string.IsNullOrEmpty(strBranchCode))
            {
                string strSelectionFormula = null;
                string strDocFromDate = null;
                string strDocToDate = null;
                string strToDate_PrevYear = null;
                string strDocDateCompare = null;
                string strDocDateQuery = "{V_SalesReports.Document_Date}";
                string strCustomerQuery = "{V_SalesReports.customer_code} = '" + cbCustomerName.SelectedValue + "'";
                string strBranchQuery = "{V_SalesReports.branch_code}";

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

                if (iIsLeapYear == 0)
                    strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + dtToDate.Day + ")";
                else
                    strToDate_PrevYear = "Date (" + iToDatePrevYear + "," + dtToDate.Month + "," + iPrevDay + ")";

                strDocDateCompare = "((" + strDocDateQuery + " >= " + strDocFromDate + " and " + strDocDateQuery + " <= Date(" + strToDate + ")) OR ("
                                  + strDocDateQuery + " >= " + strDocToDate + " and " + strDocDateQuery + " <= " + strToDate_PrevYear + "))";

                string txtCustomerName = cbCustomerName.SelectedValue.ToString();
                if (txtCustomerName == "0")
                {
                    strSelectionFormula = strDocDateCompare;
                    cbCustomerName.ClearSelection();
                }
                else
                    strSelectionFormula = "(" + strCustomerQuery + " and " + strDocDateCompare + ")";

                if (strBranchCode != "CRP")
                {
                    strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";
                }

                if (ddlReportType.SelectedValue.Equals("Report"))
                    crCustomerWise.ReportName = "CustomerWise";
                else
                    crCustomerWise.ReportName = "CustomerWiseSummary";

                crCustomerWise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crCustomerWise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crCustomerWise.RecordSelectionFormula = strSelectionFormula;
                crCustomerWise.GenerateReportAndExport(fileType);
            }
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

        #region GetCustomerDetails
        private void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = cbCustomerName.SelectedValue;
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void cbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerDetails(cbCustomerName.SelectedValue);
            divCustomerInfo.Style.Add("display", "block");
        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CustomerWise.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
