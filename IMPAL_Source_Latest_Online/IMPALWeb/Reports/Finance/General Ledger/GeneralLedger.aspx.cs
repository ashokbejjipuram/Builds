#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class GeneralLedger : System.Web.UI.Page
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
                    btnBack.Attributes.Add("style", "display:none");

                    if (rptCrystal != null)
                    {
                        rptCrystal.Dispose();
                        rptCrystal = null;
                    }

                    PopulateReportType();
                    LoadAccPeriodDDL();
                    LoadGLClassification();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    cbGLAcc.Enabled = false;
                    ddlAccPeriod_SelectedIndexChanged(ddlAccPeriod, EventArgs.Empty);

                    if (!(strBranchCode.Equals("CRP")))
                    {
                        IMPALLibrary.Branches oBranches = new IMPALLibrary.Branches();
                        IMPALLibrary.Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                            ddlBranch.Items.Add(new ListItem(oBranchDetails.BranchName, strBranchCode));
                    }
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
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
            }
        }
        protected void rptCrystal_Unload(object sender, EventArgs e)
        {
            if (rptCrystal != null)
            {
                rptCrystal.Dispose();
                rptCrystal = null;
            }
        }

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                    IMPALLibrary.Masters.FinanceProp oProp = new IMPALLibrary.Masters.FinanceProp();
                    oProp.AccPeriod = Convert.ToInt16(ddlAccPeriod.SelectedValue);

                    if (ddlGLClassification.SelectedIndex > 0)
                        oProp.ClassificationCode = ddlGLClassification.SelectedValue;
                    else
                        oProp.ClassificationCode = string.Empty;

                    if (ddlGLGroup.SelectedIndex > 0)
                        oProp.GroupCode = ddlGLGroup.SelectedValue;
                    else
                        oProp.GroupCode = string.Empty;

                    if (ddlGLMain.SelectedIndex > 0)
                        oProp.MainCode = ddlGLMain.SelectedValue;
                    else
                        oProp.MainCode = string.Empty;

                    if (ddlGLSub.SelectedIndex > 0)
                        oProp.SubCode = ddlGLSub.SelectedValue;
                    else
                        oProp.SubCode = string.Empty;

                    if (cbGLAcc.SelectedIndex > 0)
                        oProp.AccCode = cbGLAcc.SelectedValue;
                    else
                        oProp.AccCode = string.Empty;

                    if (strBranchCode.Equals("CRP"))
                    {
                        if (ddlBranch.SelectedIndex > 0)
                            oProp.BranchCode = ddlBranch.SelectedValue;
                        else
                        {
                            ddlBranch.SelectedValue = "CRP";
                            oProp.BranchCode = "CRP";
                        }
                    }
                    else
                        oProp.BranchCode = strBranchCode;

                    oProp.FromDate = txtFromDate.Text;
                    oProp.ToDate = txtToDate.Text;
                    oFinance.CalculateGeneralLedger(oProp);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        protected void PopulateReportType()
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

        #region LoadAccPeriodDDL
        private void LoadAccPeriodDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(30, null, strBranchCode);
                ddlAccPeriod.DataTextField = "Desc";
                ddlAccPeriod.DataValueField = "AccPeriodCode";
                ddlAccPeriod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLClassification
        private void LoadGLClassification()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                ddlGLClassification.DataSource = oFinance.GetGLClassification();
                ddlGLClassification.DataTextField = "ClassificationDesc";
                ddlGLClassification.DataValueField = "ClassificationCode";
                ddlGLClassification.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLGroup
        protected void GLClassification_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlGLGroup.Items.Clear();
                ddlGLGroup.Enabled = false;
                ddlGLMain.Items.Clear();
                ddlGLMain.Enabled = false;
                ddlGLSub.Items.Clear();
                ddlGLSub.Enabled = false;
                cbGLAcc.Items.Clear();
                cbGLAcc.Enabled = false;
                //ddlBranch.Items.Clear();
                ddlBranch.Enabled = false;
                if (ddlGLClassification.SelectedIndex > 0)
                {
                    LoadGLGroup(ddlGLClassification.SelectedValue);
                    ddlGLGroup.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadGLGroup(string ClassificationCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                ddlGLGroup.DataSource = oFinance.GetGLGroup(ClassificationCode, strBranchCode);
                ddlGLGroup.DataTextField = "GroupDesc";
                ddlGLGroup.DataValueField = "GroupCode";
                ddlGLGroup.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlAccPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParamterInfo paramterInfo = new ParamterInfo();
            IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
            paramterInfo = oFinance.GetAccountPeriodDates(ddlAccPeriod.SelectedValue);

            txtFromDate.Text = paramterInfo.FromDate;

            if (ddlAccPeriod.SelectedIndex == 0)
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            else
                txtToDate.Text = paramterInfo.ToDate;
        }

        #region LoadGLMain
        protected void ddlGLGroup_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlGLMain.Items.Clear();
                ddlGLMain.Enabled = false;
                ddlGLSub.Items.Clear();
                ddlGLSub.Enabled = false;
                cbGLAcc.Items.Clear();
                cbGLAcc.Enabled = false;
                //ddlBranch.Items.Clear();
                ddlBranch.Enabled = false;
                if (ddlGLGroup.SelectedIndex > 0)
                {
                    LoadGLMain();
                    ddlGLMain.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadGLMain()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                ddlGLMain.DataSource = oFinance.GetGLMain(ddlGLClassification.SelectedValue, ddlGLGroup.SelectedValue, strBranchCode);
                ddlGLMain.DataTextField = "MainDesc";
                ddlGLMain.DataValueField = "MainCode";
                ddlGLMain.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLSub
        protected void ddlGLMain_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlGLSub.Items.Clear();
                ddlGLSub.Enabled = false;
                cbGLAcc.Items.Clear();
                cbGLAcc.Enabled = false;
                //ddlBranch.Items.Clear();
                ddlBranch.Enabled = false;
                if (ddlGLMain.SelectedIndex > 0)
                {
                    LoadGLSub();
                    ddlGLSub.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadGLSub()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
                oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
                oFinanceProp.MainCode = ddlGLMain.SelectedValue;
                ddlGLSub.DataSource = oFinance.GetGLSub(oFinanceProp, strBranchCode);
                ddlGLSub.DataTextField = "SubDesc";
                ddlGLSub.DataValueField = "SubCode";
                ddlGLSub.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLAccount
        protected void ddlGLSub_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                cbGLAcc.Items.Clear();
                cbGLAcc.Enabled = false;
                //ddlBranch.Items.Clear();
                ddlBranch.Enabled = false;
                if (ddlGLSub.SelectedIndex > 0)
                {
                    IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                    oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
                    oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
                    oFinanceProp.MainCode = ddlGLMain.SelectedValue;
                    oFinanceProp.SubCode = ddlGLSub.SelectedValue;
                    oFinanceProp.BranchCode = ddlBranch.SelectedValue;
                    LoadGLAccount(oFinanceProp);
                    cbGLAcc.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadGLAccount(IMPALLibrary.Masters.FinanceProp FinanceProp)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                cbGLAcc.DataSource = oFinance.GetGLAccount(FinanceProp);
                cbGLAcc.DataTextField = "AccDesc";
                cbGLAcc.DataValueField = "AccCode";
                cbGLAcc.DataBind();
                cbGLAcc.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLBranch
        //protected void ddlGLAcc_IndexChanged(object sender, EventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    try
        //    {
        //        ddlBranch.Items.Clear();
        //        ddlBranch.Enabled = false;
        //        if (ddlGLSub.SelectedIndex > 0)
        //        {
        //            IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
        //            oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
        //            oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
        //            oFinanceProp.MainCode = ddlGLMain.SelectedValue;
        //            oFinanceProp.SubCode = ddlGLSub.SelectedValue;
        //            LoadGLBranch(oFinanceProp);
        //            ddlBranch.Enabled = true;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        IMPALLibrary.Log.WriteException(Source, exp);
        //    }
        //}

        private void LoadGLBranch(IMPALLibrary.Masters.FinanceProp FinanceProp)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (strBranchCode.Equals("CRP"))
                {
                    IMPALLibrary.Masters.Finance oFinance = new IMPALLibrary.Masters.Finance();
                    ddlBranch.DataSource = oFinance.GetGLBranch(FinanceProp);
                    ddlBranch.DataTextField = "BranchDesc";
                    ddlBranch.DataValueField = "BranchCode";
                    ddlBranch.DataBind();
                }
                else
                {
                    IMPALLibrary.Branches oBranches = new IMPALLibrary.Branches();
                    IMPALLibrary.Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                    if (oBranchDetails != null)
                        ddlBranch.Items.Add(oBranchDetails.BranchName);
                }
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
                txtCustomerCode.Text = cbGLAcc.SelectedValue;
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

        protected void cbGLAcc_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (strBranchCode.Equals("CRP"))
                {
                    //ddlBranch.Items.Clear();
                    ddlBranch.Enabled = false;
                    if (ddlGLSub.SelectedIndex > 0)
                    {
                        IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                        oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
                        oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
                        oFinanceProp.MainCode = ddlGLMain.SelectedValue;
                        oFinanceProp.SubCode = ddlGLSub.SelectedValue;
                        LoadGLBranch(oFinanceProp);
                        ddlBranch.Enabled = true;
                    }
                }

                if (ddlGLSub.SelectedValue.Equals("0220"))
                {
                    GetCustomerDetails(cbGLAcc.SelectedValue);
                    divCustomerInfo.Style.Add("display", "block");
                }
                else
                    divCustomerInfo.Style.Add("display", "none");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #endregion

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
            //Database ImpalDB = DataAccess.GetDatabase();
            //DbCommand cmd = null;
            //int timediff = 0;

            //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = 'GL-Report' order by datestamp desc");
            //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            //timediff = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            //if (timediff > 0 && timediff <= 1200)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('You Are Again Generating this Report With in no Time. Please Wait for 20 Minutes');", true);
            //    btnReport.Attributes.Add("style", "display:none");
            //    btnBack.Attributes.Add("style", "display:inline");
            //    return;
            //}
            //else
            //{
                ReportsData reportsDt = new ReportsData();
                reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), "GL-Report");
                CallCrystalReport();
            //}

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }

        protected void GenerateAndExportReport(string fileType)
        {
            btnReportPDF.Attributes.Add("style", "display:none");
            btnReportExcel.Attributes.Add("style", "display:none");
            btnReportRTF.Attributes.Add("style", "display:none");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            switch (ddlReportType.SelectedValue)
            {
                case "Report":
                    rptCrystal.ReportName = "GL-Report";
                    break;
                case "Summary":
                    rptCrystal.ReportName = "GL-Summary";
                    break;
            }

            if (ddlReportType.SelectedValue == "Report")
                rptCrystal.RecordSelectionFormula = "{V_General_Ledger_Detail.Report_Branch}='" + strBranchCode + "' and {V_General_Ledger_Detail.Branch_Code}='" + strBranchCode + "'";
            else
                rptCrystal.RecordSelectionFormula = "{V_General_Ledger_Detail_Summ.Report_Branch}='" + strBranchCode + "' and {V_General_Ledger_Detail_Summ.Branch_Code}='" + strBranchCode + "'";

            rptCrystal.GenerateReportAndExportHO(fileType);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("GeneralLedgerBackUp.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GeneralLedgerBackUp), exp);
            }
        }
    }
}
