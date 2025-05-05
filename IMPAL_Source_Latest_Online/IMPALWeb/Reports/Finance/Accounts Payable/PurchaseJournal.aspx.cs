#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace IMPALWeb.Reports.Finance.Accounts_Payable
{
    public partial class PurchaseJournal : System.Web.UI.Page
    {
        #region Global Declaration
        private string strBranchCode = default(string);
        private string strBranchCodeField = default(string);
        private string strDateField = default(string);
        private string strReportName = default(string);
        #endregion

        #region Page Init
        /// <summary>
        /// Page init event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
                if (!IsPostBack)
                    Session.Remove("CrystalReport");

                if (Session["CrystalReport"] != null)
                    crPurchaseJournal.GenerateReportHO();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page Load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crPurchaseJournal != null)
                    {
                        crPurchaseJournal.Dispose();
                        crPurchaseJournal = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    fnPopulateReportType();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crPurchaseJournal != null)
            {
                crPurchaseJournal.Dispose();
                crPurchaseJournal = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void crPurchaseJournal_Unload(object sender, EventArgs e)
        {
            if (crPurchaseJournal != null)
            {
                crPurchaseJournal.Dispose();
                crPurchaseJournal = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Populate Report Type
        /// <summary>
        /// Populate Report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entering fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("PurchaseJourReportType");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion 

        #region Button Click
        /// <summary>
        /// Report Button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    int intProcStatus = default(int);
                    string strSelectionFormula = default(string);

                    Session.Remove("CrystalReport");
                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    strBranchCodeField = "{branch_master.branch_code}";
                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (ddlReportType.SelectedIndex == 0)
                    {
                        strDateField = "{Inward_Detail.Original_receipt_Date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='PO' and {inward_detail.serial_number} =1";
                        strReportName = "PurchaseJournal";
                    }
                    else if (ddlReportType.SelectedIndex == 1)
                    {
                        strDateField = "{Inward_Detail.Original_receipt_Date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='PO' and {inward_detail.serial_number} =1";
                        strReportName = "PurchaseJournalSummary";
                    }
                    else if (ddlReportType.SelectedIndex == 2)
                    {
                        strDateField = "{Debit_credit_note_header.Document_date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='DA' and {Debit_Credit_Note_Header.Dr_Cr_Indicator} = 'DA' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'642' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'742'";
                        strReportName = "CreditNoteDetail";
                    }
                    else if (ddlReportType.SelectedIndex == 3)
                    {
                        strDateField = "{General_ledger_detail.Document_date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='DA' and {Debit_Credit_Note_Header.Dr_Cr_Indicator} = 'DA' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'642' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'742'";
                        strReportName = "CreditNoteSummary";
                    }
                    else if (ddlReportType.SelectedIndex == 4)
                    {
                        strDateField = "{Debit_credit_note_header.Document_date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='DA' and {Debit_Credit_Note_Header.Dr_Cr_Indicator} = 'CA' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'642' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'742'";
                        strReportName = "DebitNoteDetail";
                    }
                    else
                    {
                        strDateField = "{General_ledger_detail.Document_date}";
                        strSelectionFormula = "{General_Ledger_Detail.Amount}<>0 and {General_Ledger_Detail.Indicator} ='DA' and {Debit_Credit_Note_Header.Dr_Cr_Indicator} = 'CA' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'642' and {Debit_Credit_Note_Header.Transaction_Type_Code}<>'742'";
                        strReportName = "DebitNoteSummary";
                    }

                    if (strBranchCode == "CRP")
                        strSelectionFormula = strSelectionFormula + " and " + strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryToDate;
                    else
                        strSelectionFormula = strSelectionFormula + " and " + strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryToDate + "and " + strBranchCodeField + "='" + strBranchCode + "'";

                    crPurchaseJournal.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crPurchaseJournal.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crPurchaseJournal.ReportName = strReportName;
                    crPurchaseJournal.RecordSelectionFormula = strSelectionFormula;
                    crPurchaseJournal.GenerateReportHO();
                }
                else
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crPurchaseJournal != null)
                    {
                        crPurchaseJournal.Dispose();
                        crPurchaseJournal = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
