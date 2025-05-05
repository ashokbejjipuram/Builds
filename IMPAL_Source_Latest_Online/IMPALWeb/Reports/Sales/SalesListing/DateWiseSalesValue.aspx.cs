using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class DateWiseSalesValue : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strBranchCode = Session["BranchCode"].ToString();

                if (cryDateWiseSalesValue != null)
                {
                    cryDateWiseSalesValue.Dispose();
                    cryDateWiseSalesValue = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryDateWiseSalesValue != null)
            {
                cryDateWiseSalesValue.Dispose();
                cryDateWiseSalesValue = null;
            }
        }
        protected void cryDateWiseSalesValue_Unload(object sender, EventArgs e)
        {
            if (cryDateWiseSalesValue != null)
            {
                cryDateWiseSalesValue.Dispose();
                cryDateWiseSalesValue = null;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    //string strBranchCode=default(string);
                    string strSelectionFormula = default(string);
                    bool blnFlag = false;
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string strBranchCodeField = default(string);
                    string strDateField = default(string);
                    string strReportName = default(string);

                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strBranchCodeField = "{linewisesales.varchar}";
                    strDateField = "{linewisesales.date}";
                    strReportName = "DateWiseSalesValue_new";

                    if (strBranchCode == "CRP")
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                    else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + " ='" + strBranchCode + "'";

                    cryDateWiseSalesValue.ReportName = strReportName;
                    cryDateWiseSalesValue.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    cryDateWiseSalesValue.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    cryDateWiseSalesValue.RecordSelectionFormula = strSelectionFormula;
                    cryDateWiseSalesValue.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
