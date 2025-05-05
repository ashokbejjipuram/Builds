#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion
namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class TrialBalance : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page Initialization
        /// <summary>
        /// To Initialization  Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// To load page
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
                {
                    strBranchCode = (string)Session["BranchCode"];
                }

                if (!IsPostBack)
                {
                    if (crtrialbalance != null)
                    {
                        crtrialbalance.Dispose();
                        crtrialbalance = null;
                    }

                    if (strBranchCode.Equals("CRP"))
                    {
                        Database ImpalDB = DataAccess.GetDatabase();
                        string SSQL = "select TOP 1 From_Date, To_Date from Temp_TB_dates";
                        DbCommand cmd = ImpalDB.GetSqlStringCommand(SSQL);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                        using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                        {
                            while (reader.Read())
                            {
                                txtFromDate.Text = (string)reader[0];
                                txtToDate.Text = (string)reader[1];
                            }
                        }

                        Zones oZone = new Zones();
                        ddlZone.DataSource = oZone.GetAllZones();
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, string.Empty);
                        ddlZone.Enabled = true;
                    }
                    else
                    {
                        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtToDate.Text = txtFromDate.Text;

                        Branches oBranches = new Branches();
                        Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                        if (oBranchDetails != null)
                        {
                            ddlZone.Items.Add(oBranchDetails.Zone);
                            ddlState.Items.Add(oBranchDetails.State);
                            ddlBranch.Items.Add(oBranchDetails.BranchName);
                        }
                    }

                    LoadReportType();
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
            if (crtrialbalance != null)
            {
                crtrialbalance.Dispose();
                crtrialbalance = null;
            }
        }
        protected void crtrialbalance_Unload(object sender, EventArgs e)
        {
            if (crtrialbalance != null)
            {
                crtrialbalance.Dispose();
                crtrialbalance = null;
            }
        }

        #region LoadReportType
        /// <summary>
        /// To Load Report Type list in dropdown ddlReportType
        /// </summary>
        protected void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "LoadReportType", "Entering LoadReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("TrialBalanceBranch");
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

        #region ddlZone_OnSelectedIndexChanged
        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStates(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, string.Empty);
                }
                else
                    ddlState.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlState_OnSelectedIndexChanged
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
                    ddlBranch.DataSource = oBranch.GetStateBasedBranch(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, string.Empty);
                }
                else
                    ddlBranch.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                PanelHeaderDtls.Enabled = false;
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                string Date1 = txtFromDate.Text;
                string Date2 = txtToDate.Text;

                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmdTB = ImpalDB.GetStoredProcCommand("usp_trialbalance_branch");
                ImpalDB.AddInParameter(cmdTB, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmdTB, "@From_Date", DbType.String, Date1);
                ImpalDB.AddInParameter(cmdTB, "@To_Date", DbType.String, Date2);
                cmdTB.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmdTB);

                if (crtrialbalance.ReportName == "TB_Audit")
                {
                    DbCommand cmdTB1 = ImpalDB.GetStoredProcCommand("usp_trialbalgl_audit");
                    ImpalDB.AddInParameter(cmdTB1, "@Branch_Code", DbType.String, strBranchCode);
                    cmdTB1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdTB1);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strselectionstring = string.Empty;
            string strcomfield = "{Chart_of_account.Branch_Code}";

            if (ddlReportType.SelectedValue == "Report")
            {
                crtrialbalance.ReportName = "impal-reports-trialbalance";

            }
            else if (ddlReportType.SelectedValue == "AuditDetail")
            {
                crtrialbalance.ReportName = "TB_Audit";
            }

            if (strBranchCode != "CRP")
            {
                strselectionstring = strcomfield + " = '" + strBranchCode + "'";
            }

            crtrialbalance.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crtrialbalance.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crtrialbalance.RecordSelectionFormula = strselectionstring;
            crtrialbalance.GenerateReportAndExportHO(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("TrialBalance.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
