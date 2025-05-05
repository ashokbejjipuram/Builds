#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters.Sales;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class BranchReconcilationHO : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page Init
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
                    LoadAccPeriodDDL();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    ddlAccPeriod_SelectedIndexChanged(ddlAccPeriod, EventArgs.Empty);

                    if (strBranchCode.Equals("CRP"))
                    {
                        ddlBranch.Enabled = true;
                        Branches oBranch = new Branches();
                        ddlBranch.DataSource = oBranch.GetAllBranchesCRP();
                        ddlBranch.DataBind();
                        ddlBranch.Text = strBranchCode;
                    }
                    else
                    {
                        ddlBranch.Enabled = false;
                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        private void LoadAccPeriodDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AccountingPeriods oAcc = new AccountingPeriods();
                ddlAccPeriod.DataSource = oAcc.GetAccountingPeriod(0, "GLHOADM", strBranchCode);
                ddlAccPeriod.DataTextField = "Desc";
                ddlAccPeriod.DataValueField = "AccPeriodCode";
                ddlAccPeriod.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
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

        #region btnReport_Click
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
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_CrpRecon");
                ImpalDB.AddInParameter(dbcmd, "@Branch_code", DbType.String, ddlBranch.SelectedItem.Value.ToString());
                ImpalDB.AddInParameter(dbcmd, "@From_Date", DbType.String, txtFromDate.Text);
                ImpalDB.AddInParameter(dbcmd, "@To_Date", DbType.String, txtToDate.Text);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(dbcmd);

                PanelHeaderDtls.Enabled = false;
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strBranch = "{Branch_Master.branch_code}";
            string strHOBranch = "{HOBRANCHRECON.frbr}";

            crBranchReconcilationHO.ReportName = "BranchReconcilationHO";
            crBranchReconcilationHO.CrystalFormulaFields.Add("BranchCode", "\"" + ddlBranch.SelectedItem.Text + "\"");
            crBranchReconcilationHO.RecordSelectionFormula = strBranch + "='" + ddlBranch.SelectedItem.Value + "' and " + strHOBranch + "='" + ddlBranch.SelectedItem.Value + "'";
            crBranchReconcilationHO.GenerateReportAndExportHO(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (crBranchReconcilationHO != null)
                {
                    crBranchReconcilationHO.Dispose();
                    crBranchReconcilationHO = null;
                }

                Server.ClearError();
                Response.Redirect("BranchReconcilationHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}