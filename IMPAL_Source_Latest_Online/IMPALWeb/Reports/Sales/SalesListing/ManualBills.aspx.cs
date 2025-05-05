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

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class ManualBills : System.Web.UI.Page
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
                if (crystockvalue != null)
                {
                    crystockvalue.Dispose();
                    crystockvalue = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crystockvalue != null)
            {
                crystockvalue.Dispose();
                crystockvalue = null;
            }
        }
        protected void crystockvalue_Unload(object sender, EventArgs e)
        {
            if (crystockvalue != null)
            {
                crystockvalue.Dispose();
                crystockvalue = null;
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

                    strBranchCodeField = "right({sales_order_header.document_number},3)";
                    strDateField = "{sales_order_header.document_date}";
                    strReportName = "impal-reports-manbills";

                    if (strBranchCode == "CRP")
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                    else if (strBranchCode != "CRP" && !string.IsNullOrEmpty(strBranchCode))
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + " ='" + strBranchCode + "'";

                    crystockvalue.ReportName = strReportName;
                    crystockvalue.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crystockvalue.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crystockvalue.RecordSelectionFormula = strSelectionFormula;
                    crystockvalue.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
