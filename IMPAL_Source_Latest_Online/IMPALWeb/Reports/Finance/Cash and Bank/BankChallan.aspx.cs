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
using IMPALLibrary.Masters.Sales;
using System.Transactions;
#endregion

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class BankChallan : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string strBank = default(string);

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
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
                    btnBack.Attributes.Add("style", "display:none");

                    if (crBankChallan != null)
                    {
                        crBankChallan.Dispose();
                        crBankChallan = null;
                    }

                    LoadAccountintPeriod();
                    fnPopulateReportType();
                    fnPopulateLocal();
                    fnPopulateBank();
                    lblChallan.Visible = false;
                    ddlChallanNo.Visible = false;
                    idSpan.Visible = false;
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
            if (crBankChallan != null)
            {
                crBankChallan.Dispose();
                crBankChallan = null;
            }
        }
        protected void crBankChallan_Unload(object sender, EventArgs e)
        {
            if (crBankChallan != null)
            {
                crBankChallan.Dispose();
                crBankChallan = null;
            }
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "BankChallan");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);

            List<AccountingPeriod> AccordingPeriod = objReceivableInvoice.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");
        }

        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
            }
            catch (Exception ex)
            {

                Log.WriteException(Source, ex);
            }
        }

        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;

            return FinYear.Trim();
        }

        #region Populate Report Type
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entering fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType");
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

        #region Populate Bank
        protected void fnPopulateBank()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateBank", "Entering fnPopulateBank");
            try
            {
                IMPALLibrary.Banks bank = new IMPALLibrary.Banks();
                List<IMPALLibrary.Bank> lstBank = new List<IMPALLibrary.Bank>();
                lstBank = bank.GetAllBanks();
                ddlBank.DataSource = lstBank;
                ddlBank.DataValueField = "BankCode";
                ddlBank.DataTextField = "BankName";
                ddlBank.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Branch
        protected void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateBranch", "Entering fnPopulateBranch");
            try
            {
                IMPALLibrary.Masters.Finance finance = new IMPALLibrary.Masters.Finance();
                List<IMPALLibrary.Masters.FinanceProp> lstBranch = new List<IMPALLibrary.Masters.FinanceProp>();
                lstBranch = finance.GetBankBranches(strBank, strBranchCode);
                ddlBranch.DataSource = lstBranch;
                ddlBranch.DataValueField = "Bank_Branch_Code";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Local
        protected void fnPopulateLocal()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateLocal", "Entering fnPopulateLocal");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("LocalOutstation");
                ddlLocalOutstatn.DataSource = oList;
                ddlLocalOutstatn.DataValueField = "DisplayValue";
                ddlLocalOutstatn.DataTextField = "DisplayText";
                ddlLocalOutstatn.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Challan Number
        protected void fnPopulateChallanNumb()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateChallanNumb", "Entering fnPopulateChallanNumb");
            try
            {
                IMPALLibrary.Masters.Finance finance = new IMPALLibrary.Masters.Finance();
                List<IMPALLibrary.Masters.FinanceProp> lstChallan = new List<IMPALLibrary.Masters.FinanceProp>();
                lstChallan = finance.GetChallanNumber(strBranchCode, Convert.ToInt16(ddlAccountingPeriod.SelectedValue));
                ddlChallanNo.DataSource = lstChallan;
                ddlChallanNo.DataValueField = "MonthYear";
                ddlChallanNo.DataTextField = "MonthYear";
                ddlChallanNo.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region On change of ReportType
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ddlReportType_SelectedIndexChanged", "Entering ddlReportType_SelectedIndexChanged");
            try
            {
                if (ddlReportType.SelectedValue == "Report")
                {
                    fnPopulateBank();
                    fnPopulateBranch();
                    lblChallan.Visible = false;
                    ddlChallanNo.Visible = false;
                    idSpan.Visible = false;
                    BankInfo.Visible = true;

                }
                else
                {
                    fnPopulateChallanNumb();
                    BankInfo.Visible = false;
                    lblChallan.Visible = true;
                    ddlChallanNo.Visible = true;
                    idSpan.Visible = true;

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Bank Selected Index Changed
        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBank = ddlBank.SelectedValue;
            fnPopulateBranch();
        }
        #endregion

        public void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strBank = default(string);
                string strBranch = default(string);
                string strLocal = default(string);
                string strIndicator = default(string);
                string strGroupInd = default(string);

                strBank = ddlBank.Text;
                strBranch = ddlBranch.Text;
                strLocal = ddlLocalOutstatn.Text;

                if (chkTransfer.Checked)
                    strIndicator = "Y";
                else
                    strIndicator = "N";

                if (chkGrouping.Checked)
                    strGroupInd = "Y";
                else
                    strGroupInd = "N";

                Database ImpalDB = DataAccess.GetDatabase();

                if (ddlReportType.SelectedValue != "Reprint")
                {
                    using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                    {
                        DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_AddPayInSlip");
                        ImpalDB.AddInParameter(dbcmd, "@branch_code", DbType.String, strBranchCode.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Accounting_Period_Code", DbType.String, ddlAccountingPeriod.SelectedValue.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Bank_Branch_Code", DbType.String, strBranch.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Bank_Code", DbType.String, strBank.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Transfer_Ind", DbType.String, strIndicator.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Group_Ind", DbType.String, strGroupInd.Trim());
                        ImpalDB.AddInParameter(dbcmd, "@Lo", DbType.String, strLocal.Trim());
                        dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(dbcmd);

                        scope.Complete();
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region Button Click
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
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            PanelHeaderDtls.Enabled = false;
            GenerateSelectionFormula();

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }

        public void GenerateAndExportReport(string fileType)
        {
            string strCurrentDate = default(string);
            string strBank = default(string);
            string strBranch = default(string);
            string strLocal = default(string);
            string strGroupInd = default(string);
            string strSelectionFormula = default(string);
            string strReportName = default(string);
            string strCryFromDate = default(string);
            string strDateField = default(string);
            string strBankCodeField = default(string);
            string strBankBranchCodeField = default(string);
            string strSlipNumberField = default(string);
            string strChallanNumb = default(string);
            string strSlipBranchCodeField = default(string);

            if (ddlAccountingPeriod.SelectedIndex == 0)
                strCurrentDate = DateTime.Today.ToString("dd/MM/yyyy");
            else
                strCurrentDate = DateTime.Now.ToString("31/03/" + DateTime.Today.Year.ToString());

            strCryFromDate = "Date (" + DateTime.ParseExact(strCurrentDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";            

            strBank = ddlBank.Text;
            strBranch = ddlBranch.Text;
            strLocal = ddlLocalOutstatn.Text;
            strChallanNumb = ddlChallanNo.Text;
            strReportName = "BankChallan";
            strDateField = "{Pay_In_slip_Header.Pay_in_slip_Date}";
            strBankCodeField = "{Pay_In_slip_Header.Bank_Code}";
            strBankBranchCodeField = "{Pay_In_slip_Header.Bank_Branch_Code}";
            strSlipBranchCodeField = "{pay_in_slip_header.branch_code}";
            strSlipNumberField = "{pay_in_slip_Header.Pay_In_Slip_Number}='";            

            if (ddlReportType.SelectedValue == "Report")
            {
                if (strBranchCode == "CRP")
                {
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryFromDate + "and " + strBankCodeField + "=" + strBank + "and " + strBankBranchCodeField + "=" + strBranch;
                }
                else
                {
                    strSelectionFormula = strDateField + ">=" + strCryFromDate + "and " + strDateField + "<=" + strCryFromDate + "and " + strBankCodeField + "=" + strBank + "and " + strBankBranchCodeField + "=" + strBranch + " and " + strSlipBranchCodeField + "='" + strBranchCode + "'";
                }
            }
            else
            {
                strSelectionFormula = strSlipNumberField + strChallanNumb + "'";
            }

            strSelectionFormula = strSelectionFormula + " and {Pay_In_Slip_Detail.payment_type}<>'D'";

            crBankChallan.ReportName = strReportName;
            crBankChallan.CrystalFormulaFields.Add("From_Date", "'" + strCurrentDate + "'");
            crBankChallan.CrystalFormulaFields.Add("To_Date", "'" + strCurrentDate + "'");
            crBankChallan.RecordSelectionFormula = strSelectionFormula;
            crBankChallan.GenerateReportAndExport(fileType);
        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("BankChallan.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}