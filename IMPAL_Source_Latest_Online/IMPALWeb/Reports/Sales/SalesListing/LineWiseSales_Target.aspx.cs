using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class LineWiseSales_Target : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();

            if (!IsPostBack)
            {
                if (crLineWiseSalesTarget != null)
                {
                    crLineWiseSalesTarget.Dispose();
                    crLineWiseSalesTarget = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crLineWiseSalesTarget != null)
            {
                crLineWiseSalesTarget.Dispose();
                crLineWiseSalesTarget = null;
            }
        }
        protected void crLineWiseSalesTarget_Unload(object sender, EventArgs e)
        {
            if (crLineWiseSalesTarget != null)
            {
                crLineWiseSalesTarget.Dispose();
                crLineWiseSalesTarget = null;
            }
        }

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

        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            //string strBranchCode=default(string);
            string strSelectionFormula = default(string);
            int intPrevFromYear = default(int);
            int intPrevToYear = default(int);
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string strBranchCodeField = default(string);
            string strDateField = default(string);
            string strSupplierName = default(string);
            string strReportName = default(string);
            string strTempFromDate = default(string);
            string strTempToDate = default(string);
            string strTempToDatePrint = default(string);

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            intPrevFromYear = Convert.ToInt32(strFromDate.Substring(6)) - 1;
            intPrevToYear = Convert.ToInt32(strToDate.Substring(6)) - 1;

            if (Convert.ToInt32(strFromDate.Split(new char[] { '/' })[1]) < 4)
                strTempFromDate = "Date (" + Convert.ToString(Convert.ToInt32(strFromDate.Substring(6)) - 1) + ",04,01)";
            else
                strTempFromDate = "Date (" + strFromDate.Substring(6) + ",04,01)";

            if (Convert.ToInt32(strToDate.Split(new char[] { '/' })[1]) < 4)
                strTempToDate = "Date (" + Convert.ToString(intPrevToYear - 1) + ",04,01)";
            else
                strTempToDate = "Date (" + Convert.ToString(intPrevToYear) + ",04,01)";

            strTempToDatePrint = "Date (" + Convert.ToString(intPrevToYear) + "," + strToDate.Split(new char[] { '/' })[1] + "," + strToDate.Split(new char[] { '/' })[0] + ")";

            strBranchCodeField = "{V_Salessalesman.branch_code}";
            strDateField = "CDate ({V_Salessalesman.Document_Date})";
            strSupplierName = "mid({V_Salessalesman.supplier_name},1,3)";
            strReportName = "LineWiseSalesWithAreaAndTarget";

            if (string.IsNullOrEmpty(ddlLinecode.SelectedValue) || ddlLinecode.SelectedValue.ToString() == "0")
                strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";
            else
                strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strSupplierName + "= '" + ddlLinecode.SelectedValue + "' and ((" + strDateField + " >= " + strTempFromDate + " and " + strDateField + " <= " + strCryToDate + ") OR (" + strDateField + " >= " + strTempToDate + " and " + strDateField + " <= " + strTempToDatePrint + ")))";

            crLineWiseSalesTarget.ReportName = strReportName;
            crLineWiseSalesTarget.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crLineWiseSalesTarget.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crLineWiseSalesTarget.RecordSelectionFormula = strSelectionFormula;
            crLineWiseSalesTarget.GenerateReportAndExport(fileType);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LineWiseSales_Target.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
