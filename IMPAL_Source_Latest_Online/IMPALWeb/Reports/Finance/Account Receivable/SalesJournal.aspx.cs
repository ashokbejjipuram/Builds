#region Namespace Declaration
using System;
using System.Collections.Generic;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;
#endregion

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class SalesJournal : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
       
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Sales Journal Page Init Method");
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("CrystalReport");
                }
                if (Session["CrystalReport"] != null)
                    crSalesJournal.GenerateReportHO();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "Sales Journal Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crSalesJournal != null)
                    {
                        crSalesJournal.Dispose();
                        crSalesJournal = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    fnLoadReportType();
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
            if (crSalesJournal != null)
            {
                crSalesJournal.Dispose();
                crSalesJournal = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void crSalesJournal_Unload(object sender, EventArgs e)
        {
            if (crSalesJournal != null)
            {
                crSalesJournal.Dispose();
                crSalesJournal = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Report type Dropdown Populate Method
        protected void fnLoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnLoadReportType()", "Sales Journal Report type Dropdown Populate Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();              

                int branchcnt = oCommon.GetUPBranches(Session["BranchCode"].ToString());
                if (branchcnt > 0)
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("Finance-SalesJournalUP");                
                else
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("Finance-SalesJournal");

                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Button Click
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
                    string strselDate = string.Empty;
                    string strselDocNumber = string.Empty;
                    string strsel1 = string.Empty;

                    if (ddlReportType.SelectedValue == "Sales Journal Detail")
                    {
                        crSalesJournal.ReportName = "impal_listing_sales_journal_New";
                        strselDate = "{Sales_Journal_View.Document_Date}";
                        strselDocNumber = "{Sales_Journal_View.Branch_Code}=";
                        strsel1 = "{Sales_Journal_View.Status}<>'I'";
                    }
                    else if (ddlReportType.SelectedValue == "Sales Journal Summary")
                    {
                        crSalesJournal.ReportName = "impal_listing_sales_journal_summ";
                        strselDate = "{Sales_Order_Header.Document_date}";
                        strselDocNumber = "{branch_master.branch_code}=";
                        strsel1 = "{Sales_Order_Header.Status}<>'I'";
                    }
                    else if (ddlReportType.SelectedValue == "CreditNote Detail")
                    {
                        crSalesJournal.ReportName = "impal_CNjournal";
                        strselDate = "{debit_credit_note_header.Document_date}";
                        strselDocNumber = "{debit_credit_note_header.Branch_code}=";
                    }
                    else if (ddlReportType.SelectedValue == "CreditNote Summary")
                    {
                        crSalesJournal.ReportName = "impal_CNjournal_summ";
                        strselDate = "{General_ledger_detail.Document_date}";
                        strselDocNumber = "{branch_master.branch_code}=";
                    }
                    else if (ddlReportType.SelectedValue == "DebitNote Detail")
                    {
                        crSalesJournal.ReportName = "impal_DNjournal";
                        strselDate = "{debit_credit_note_header.Document_date}";
                        strselDocNumber = "{debit_credit_note_header.Branch_code}=";
                    }
                    else if (ddlReportType.SelectedValue == "DebitNote Summary")
                    {
                        crSalesJournal.ReportName = "impal_DNjournal_summ";
                        strselDate = "{General_ledger_detail.Document_date}";
                        strselDocNumber = "{branch_master.branch_code}=";
                    }
                    else if (ddlReportType.SelectedValue == "Sales Journal Detail - UPstate")
                    {
                        crSalesJournal.ReportName = "impal_listing_sales_journal_New_UpState";
                        strselDate = "{Sales_Journal_View_UpState.Document_Date}";
                        strselDocNumber = "{Sales_Journal_View_UpState.Branch_Code}=";
                        strsel1 = "{Sales_Journal_View_UpState.Status}<>'I'";
                    }

                    string date1 = txtFromDate.Text.ToString();
                    string date2 = txtToDate.Text.ToString();
                    var strFromDate = txtFromDate.Text.Split('/');
                    var strToDate = txtToDate.Text.Split('/');
                    string Dfrom_Param = string.Format("Date({0},{1},{2})", strFromDate[2], strFromDate[1], strFromDate[0]);
                    string Dto_Param = string.Format("Date({0},{1},{2})", strToDate[2], strToDate[1], strToDate[0]);

                    Database ImpalDB = DataAccess.GetDatabase();

                    if (strBranchCode == "CRP")
                    { 
                        selectionformula = strselDate + ">=" + Dfrom_Param + " and " + strselDate + "<=" + Dto_Param; 
                    }
                    else
                    { 
                        selectionformula = strselDate + ">=" + Dfrom_Param + " and " + strselDate + "<=" + Dto_Param + " and " + strselDocNumber + "'" + strBranchCode + "'"; 
                    }

                    if (ddlReportType.SelectedValue == "Sales Journal Detail" || ddlReportType.SelectedValue == "Sales Journal Detail - UPstate"
                        || ddlReportType.SelectedValue == "Sales Journal Summary")
                    {
                        selectionformula = selectionformula + " and " + strsel1;
                    }

                    crSalesJournal.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crSalesJournal.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crSalesJournal.RecordSelectionFormula = selectionformula;

                    if (ddlReportType.SelectedValue == "DebitNote Summary" || ddlReportType.SelectedValue == "CreditNote Summary")
                        crSalesJournal.GenerateReportHO();
                    else
                        crSalesJournal.GenerateReportHO();
                }
                else
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crSalesJournal != null)
                    {
                        crSalesJournal.Dispose();
                        crSalesJournal = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
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
