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
using System.Data;
using ClosedXML.Excel;
using System.IO;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class GeneralLedgerHoLive : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        private ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();
        DataTable dt = new DataTable();
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
                    PopulateReportType();
                    LoadAccPeriodDDL();
                    LoadGLClassification();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    cbGLAcc.Enabled = false;
                    ddlAccPeriod_SelectedIndexChanged(ddlAccPeriod, EventArgs.Empty);

                    Zones oZone = new Zones();
                    ddlZone.DataSource = oZone.GetAllZones();
                    ddlZone.DataBind();
                    ddlZone.Items.Insert(0, "--All--");
                    ddlZone.Enabled = true;

                    ddlState.Items.Insert(0, "--All--");
                    ddlBranch.Items.Insert(0, "--All--");                    
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                ddlBranch.Items.Clear();

                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStatesOnline(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlState.Items.Insert(0, "--All--");
                }

                ddlBranch.Items.Insert(0, "--All--");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();

                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchStateOnline(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

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
                if (Session["RoleCode"].ToString().ToUpper() == "EAUD")
                {
                    List<string> lstAccountingPeriod = new List<string>();

                    int PrevFinYearStatus = objReceivableInvoice.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "GeneralLedgerReport");

                    if (PrevFinYearStatus > 0)
                    {
                        AccountingPeriods oAcc = new AccountingPeriods();
                        ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(0, "EXTERNALAUDITPREV", strBranchCode);
                        ddlAccPeriod.DataTextField = "Desc";
                        ddlAccPeriod.DataValueField = "AccPeriodCode";
                        ddlAccPeriod.DataBind();

                    }
                    else
                    {
                        AccountingPeriods oAcc = new AccountingPeriods();
                        ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(0, "EXTERNALAUDIT", strBranchCode);
                        ddlAccPeriod.DataTextField = "Desc";
                        ddlAccPeriod.DataValueField = "AccPeriodCode";
                        ddlAccPeriod.DataBind();
                    }
                }
                else
                {
                    AccountingPeriods oAcc = new AccountingPeriods();
                    ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(0, "GLHOADM", strBranchCode);
                    ddlAccPeriod.DataTextField = "Desc";
                    ddlAccPeriod.DataValueField = "AccPeriodCode";
                    ddlAccPeriod.DataBind();
                }
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
                ddlGLClassification.DataSource = oFinance.GetGLClassificationHO();
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
                ddlGLGroup.DataSource = oFinance.GetGLGroupHO(ClassificationCode, strBranchCode);
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

            if (ddlAccPeriod.SelectedIndex == 0 && ddlAccPeriod.Items.Count > 1)
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
                ddlGLMain.DataSource = oFinance.GetGLMainHO(ddlGLClassification.SelectedValue, ddlGLGroup.SelectedValue, strBranchCode);
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
                ddlGLSub.DataSource = oFinance.GetGLSubHO(oFinanceProp, strBranchCode);
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
                ddlBranch.Enabled = false;

                if (ddlGLSub.SelectedIndex > 0)
                {
                    IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                    oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
                    oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
                    oFinanceProp.MainCode = ddlGLMain.SelectedValue;
                    oFinanceProp.SubCode = ddlGLSub.SelectedValue;
                    oFinanceProp.BranchCode = ddlBranch.SelectedValue;

                    if (!ddlGLSub.SelectedValue.Equals("0220"))
                    {
                        LoadGLAccount(oFinanceProp);
                        cbGLAcc.Enabled = true;
                    }
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
                cbGLAcc.DataSource = oFinance.GetGLAccountHO(FinanceProp);
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
                ddlBranch.Enabled = true;

                if (strBranchCode.Equals("CRP"))
                {
                    if (ddlGLSub.SelectedIndex > 0)
                    {
                        IMPALLibrary.Masters.FinanceProp oFinanceProp = new IMPALLibrary.Masters.FinanceProp();
                        oFinanceProp.ClassificationCode = ddlGLClassification.SelectedValue;
                        oFinanceProp.GroupCode = ddlGLGroup.SelectedValue;
                        oFinanceProp.MainCode = ddlGLMain.SelectedValue;
                        oFinanceProp.SubCode = ddlGLSub.SelectedValue;
                    }
                }

                //if (ddlGLSub.SelectedValue.Equals("0220"))
                //{
                //    //GetCustomerDetails(cbGLAcc.SelectedValue);
                //    //divCustomerInfo.Style.Add("display", "block");
                //}
                //else
                divCustomerInfo.Style.Add("display", "none");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                DataSet ds = new DataSet();
                ReportsData reportsDt = new ReportsData();
                reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), "GLreportHO");

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

                    oProp.BranchCode = ddlBranch.SelectedValue;

                    oProp.FromDate = txtFromDate.Text;
                    oProp.ToDate = txtToDate.Text;
                    ds = oFinance.CalculateGeneralLedgerHoLive(oProp, ddlZone.SelectedValue, ddlState.SelectedValue, ddlBranch.SelectedValue);
                }

                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                string strFromDate = default(string);
                string strToDate = default(string);
                string strBrchCode = default(string);              

                strBrchCode = ddlBranch.SelectedValue;

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                string filename = "GL_Report_" + string.Format("{0:yyyyMMdd}", strFromDate) + "-" + string.Format("{0:yyyyMMdd}", strToDate) + ".xls";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ExportDataSetToExcel(ds, filename);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
            }
        }

        protected void ExportDataSetToExcel(DataSet ds, string strReportName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(ds);

                dt = ds.Tables[0];
                var sheet1 = wb.Worksheets.Add(dt);
                sheet1.Table("Table1").ShowAutoFilter = false;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false; //true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + strReportName);

                try
                {
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        MyMemoryStream.Close();
                        MyMemoryStream.Dispose();
                    }
                }
                catch (Exception exp)
                {
                    if (exp.Message != "Thread was being aborted.")
                        IMPALLibrary.Log.WriteException(Source, exp);
                }
                finally
                {
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    HttpContext.Current.Response.Close();

                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("GeneralLedgerHOLive.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GeneralLedgerHoLive), exp);
            }
        }
    }
}
