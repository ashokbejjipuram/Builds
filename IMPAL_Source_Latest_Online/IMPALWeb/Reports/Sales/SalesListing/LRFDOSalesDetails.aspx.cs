using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using System.Data.Common;
using System.Data; 

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class LRFDOSalesDetails : System.Web.UI.Page
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
                if (crLRFDOSalesDetails != null)
                {
                    crLRFDOSalesDetails.Dispose();
                    crLRFDOSalesDetails = null;
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (strBranchCode == "CRP")
                {
                    fnPopulateZone();
                    fnPopulateBranch(ddlZone.SelectedValue);
                }
                else
                {
                    fnPopulateDefaultZoneAndBranch();
                    ddlZone.Enabled = false;
                    ddlBranch.Enabled = false; 
                }
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crLRFDOSalesDetails != null)
            {
                crLRFDOSalesDetails.Dispose();
                crLRFDOSalesDetails = null;
            }
        }
        protected void crLRFDOSalesDetails_Unload(object sender, EventArgs e)
        {
            if (crLRFDOSalesDetails != null)
            {
                crLRFDOSalesDetails.Dispose();
                crLRFDOSalesDetails = null;
            }
        }
        private void fnPopulateDefaultZoneAndBranch()
        {
            Branches objBranch = new Branches();
            List<Branch> lbr=objBranch.GetBranchAndZone(strBranchCode);
            ddlBranch.DataSource = lbr;
            ddlZone.DataSource = lbr;

            ddlZone.DataTextField = "ZoneName";
            ddlZone.DataValueField = "ZoneCode";
            ddlZone.DataBind();

            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }

        private void fnPopulateZone()
        {
            Zones zne = new Zones();
            ddlZone.DataSource = zne.GetAllZones();
            ddlZone.DataTextField = "ZoneName";
            ddlZone.DataValueField = "ZoneCode";
            ddlZone.DataBind();
            if (zne != null)
                zne = null;
        }

        private void fnPopulateBranch(string strZoneCode)
        {
            Branches objBranch = new Branches();
            ddlBranch.DataSource=objBranch.GetBranchBasedOnZone(strZoneCode);
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
            if (objBranch != null)
                objBranch = null; 
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
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string strBranchCodeField = default(string);
                    string strTransField = default(string);
                    string strZoneField = default(string);
                    string strDateField = default(string);
                    string strReportName = default(string);

                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strBranchCodeField = "{branch_master.branch_code}";
                    strDateField = "{Sales_Order_Header.Document_date}";
                    strTransField = "{Sales_Order_Header.Transaction_Type_Code}";
                    strZoneField = "{Zone_master.Zone_code}";

                    strReportName = "impal_LRSalesdetails";

                    if ((string.IsNullOrEmpty(ddlZone.SelectedValue) || ddlZone.SelectedValue == "0") && (string.IsNullOrEmpty(ddlBranch.SelectedValue) || ddlBranch.SelectedValue == "0"))
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                    else if ((!string.IsNullOrEmpty(ddlZone.SelectedValue) && ddlZone.SelectedValue != "0") && (string.IsNullOrEmpty(ddlBranch.SelectedValue) || ddlBranch.SelectedValue == "0"))
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strZoneField + "=" + ddlZone.SelectedValue;
                    else if ((!string.IsNullOrEmpty(ddlZone.SelectedValue) && ddlZone.SelectedValue != "0") && (!string.IsNullOrEmpty(ddlBranch.SelectedValue) && ddlBranch.SelectedValue != "0"))
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strZoneField + "=" + ddlZone.SelectedValue + " and " + strBranchCodeField + " = '" + ddlBranch.SelectedValue + "'";

                    strSelectionFormula = strSelectionFormula + " and " + strTransField + " in ('371','461','471')"; 

                    crLRFDOSalesDetails.ReportName = strReportName;
                    crLRFDOSalesDetails.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crLRFDOSalesDetails.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crLRFDOSalesDetails.RecordSelectionFormula = strSelectionFormula;
                    crLRFDOSalesDetails.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strBranchCode =="CRP") 
                fnPopulateBranch(ddlZone.SelectedValue);  
        }
    }
}
