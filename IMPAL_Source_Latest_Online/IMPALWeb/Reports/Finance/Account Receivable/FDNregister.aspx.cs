#region Namespace Declaration
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class FDNregister : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Stock Value Page Init Method");
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
            //Log.WriteLog(Source, "Page_Load", "Stock Value Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crFDNregister != null)
                    {
                        crFDNregister.Dispose();
                        crFDNregister = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            if (crFDNregister != null)
            {
                crFDNregister.Dispose();
                crFDNregister = null;
            }
        }
        protected void crFDNregister_Unload(object sender, EventArgs e)
        {
            if (crFDNregister != null)
            {
                crFDNregister.Dispose();
                crFDNregister = null;
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string selectionformula = string.Empty;
                    string strselDocDate = "{debit_credit_Note_Header.Document_date}";
                    string strselBranchCode = "{debit_credit_Note_Header.Branch_code}=";

                    string date1 = txtFromDate.Text.ToString();
                    string date2 = txtToDate.Text.ToString();
                    Database ImpalDB = DataAccess.GetDatabase();

                    var strFromDate = txtFromDate.Text.Split('/');
                    var strToDate = txtToDate.Text.Split('/');

                    string Dfrom_Param = string.Format("Date({0},{1},{2})", strFromDate[2], strFromDate[1], strFromDate[0]);
                    string Dto_Param = string.Format("Date({0},{1},{2})", strToDate[2], strToDate[1], strToDate[0]);

                    if (strBranchCode == "CRP")
                    { selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param; }
                    else
                    { selectionformula = strselDocDate + ">=" + Dfrom_Param + " and " + strselDocDate + "<=" + Dto_Param + " and " + strselBranchCode + "'" + strBranchCode + "'"; }

                    crFDNregister.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crFDNregister.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crFDNregister.RecordSelectionFormula = selectionformula;
                    crFDNregister.ReportName = "impal_fdnregister";
                    crFDNregister.GenerateReport();
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
