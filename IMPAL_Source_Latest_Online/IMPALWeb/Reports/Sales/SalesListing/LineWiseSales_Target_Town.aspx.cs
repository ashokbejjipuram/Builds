using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class LineWiseSales_Target_Town : System.Web.UI.Page
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

                hdnTownCodes.Value = "";

                if (strBranchCode == "MGT")
                    ddlTownCode.AutoPostBack = true;
                else
                    ddlTownCode.AutoPostBack = false;

                GetTownlist();

                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        public void GetTownlist()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<IMPALLibrary.Town> lstTowns = null;
                Towns oTowns = new Towns();
                lstTowns = oTowns.GetBranchBasedTowns(strBranchCode);
                ddlTownCode.DataSource = lstTowns;
                ddlTownCode.DataBind();
                ddlTownCode.Items.Insert(0, string.Empty);

                if (strBranchCode == "MGT")
                {                    
                    ddlTownCode.Items.Insert(1, new ListItem("DELHI WHOLE SALE - MGT", "DELHI WHOLE SALE - MGT"));
                    ddlTownCode.Items.Insert(2, new ListItem("DELHI RETAIL - MGT", "DELHI RETAIL - MGT"));
                    ddlTownCode.Items.Insert(3, new ListItem("DELHI- GURGAON - MGT", "DELHI- GURGAON - MGT"));
                    ddlTownCode.Items.Insert(4, new ListItem("DELHI- WESTREN UP - MGT", "DELHI- WESTREN UP - MGT"));
                    ddlTownCode.Items.Insert(5, new ListItem("DELHI-HALDWANI - MGT", "DELHI-HALDWANI - MGT"));
                    ddlTownCode.Items.Insert(6, new ListItem("DAILY REPORTS ALL TOWNS - MGT", "DAILY REPORTS ALL TOWNS - MGT"));
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlTownCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (strBranchCode == "MGT")
                {
                    if (ddlTownCode.SelectedIndex >= 1 && ddlTownCode.SelectedIndex <= 6)
                        PopulateFiveTownCodesMGT();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
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
            string strDate = default(string);
            string strCryDate = default(string);
            //string strBranchCode=default(string);
            string strSelectionFormula = default(string);
            int intPrevYear = default(int);
            strDate = txtDate.Text;

            string strBranchCodeField = default(string);
            string strDateField = default(string);
            string strSupplierName = default(string);
            string strTown = default(string);
            string strReportName = default(string);
            string strTempDate = default(string);
            string strTempDate1 = default(string);
            string strTempDatePrint = default(string);

            intPrevYear = Convert.ToInt32(strDate.Substring(6)) - 1;

            if (Convert.ToInt32(strDate.Split(new char[] { '/' })[1]) < 4)
                strTempDate = "Date (" + Convert.ToString(Convert.ToInt32(strDate.Substring(6)) - 1) + ",04,01)";
            else
                strTempDate = "Date (" + strDate.Substring(6) + ",04,01)";

            if (Convert.ToInt32(strDate.Split(new char[] { '/' })[1]) < 4)
                strTempDate1 = "Date (" + Convert.ToString(intPrevYear - 1) + ",04,01)";
            else
                strTempDate1 = "Date (" + Convert.ToString(intPrevYear) + ",04,01)";

            strCryDate = "Date (" + DateTime.ParseExact(strDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strTempDatePrint = "Date (" + Convert.ToString(intPrevYear) + "," + strDate.Split(new char[] { '/' })[1] + "," + strDate.Split(new char[] { '/' })[0] + ")";

            strBranchCodeField = "{V_Salessalesman.branch_code}";
            strDateField = "CDate ({V_Salessalesman.Document_Date})";
            strSupplierName = "mid({V_Salessalesman.supplier_name},1,3)";
            strTown = "{Town_Master.Town_Code}";

            if (string.IsNullOrEmpty(ddlLinecode.SelectedValue) || ddlLinecode.SelectedValue.ToString() == "0")
                strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and ((" + strDateField + " >= " + strTempDate + " and " + strDateField + " <= " + strCryDate + ") OR (" + strDateField + " >= " + strTempDate1 + " and " + strDateField + " <= " + strTempDatePrint + ")))";
            else
                strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strSupplierName + "= '" + ddlLinecode.SelectedValue + "' and ((" + strDateField + " >= " + strTempDate + " and " + strDateField + " <= " + strCryDate + ") OR (" + strDateField + " >= " + strTempDate1 + " and " + strDateField + " <= " + strTempDatePrint + ")))";

            if (strBranchCode != "MGT")
            {
                if (ddlTownCode.SelectedIndex > 0)
                {
                    strSelectionFormula = strSelectionFormula + " and (" + strTown + "=" + ddlTownCode.SelectedValue + ")";
                }
            }
            else
            {
                if (ddlTownCode.SelectedIndex >= 1 && ddlTownCode.SelectedIndex <= 6)
                {
                    strSelectionFormula = strSelectionFormula + " and (" + strTown + " in " + hdnTownCodes.Value + ")";
                }
                else if (ddlTownCode.SelectedIndex > 6)
                {
                    strSelectionFormula = strSelectionFormula + " and (" + strTown + "=" + ddlTownCode.SelectedValue + ")";
                }
            }

            if (ddlTownCode.SelectedIndex > 0)
                strReportName = "LineWiseSalesWithAreaAndTarget_Town";
            else
                strReportName = "LineWiseSalesWithAreaAndTarget_TownWise";

            crLineWiseSalesTarget.ReportName = strReportName;
            crLineWiseSalesTarget.CrystalFormulaFields.Add("Date", "'" + txtDate.Text + "'");
            crLineWiseSalesTarget.CrystalFormulaFields.Add("Town", "'" + ddlTownCode.SelectedItem.Text + "'");
            crLineWiseSalesTarget.RecordSelectionFormula = strSelectionFormula;
            crLineWiseSalesTarget.GenerateReportAndExport(fileType);
        }

        protected void PopulateFiveTownCodesMGT()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("LinewWiseTargetMGT");
                int i = ddlTownCode.SelectedIndex;
                hdnTownCodes.Value = lstValues[i].DisplayValue;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LineWiseSales_Target_Town.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
