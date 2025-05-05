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
    public partial class BranchReconcilation : System.Web.UI.Page
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
                    if (crBranchReconcilation != null)
                    {
                        crBranchReconcilation.Dispose();
                        crBranchReconcilation = null;
                    }

                    if (strBranchCode.Equals("CRP"))
                    {
                        ddlBranch.Enabled = true;
                        Branches oBranch = new Branches();
                        ddlBranch.DataSource = oBranch.GetAllBranchesCRP();
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, string.Empty);
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBranchReconcilation != null)
            {
                crBranchReconcilation.Dispose();
                crBranchReconcilation = null;
            }
        }
        protected void crBranchReconcilation_Unload(object sender, EventArgs e)
        {
            if (crBranchReconcilation != null)
            {
                crBranchReconcilation.Dispose();
                crBranchReconcilation = null;
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    crBranchReconcilation.ReportName = "BranchReconcilation";

                    string strBranch = "{Branch_Master.branch_code}";
                    string strHOBranch = "{HOBRANCHRECON.frbr}";

                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_CrpRecon");
                    ImpalDB.AddInParameter(dbcmd, "@Branch_code", DbType.String, ddlBranch.SelectedItem.Value.ToString());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(dbcmd);

                    crBranchReconcilation.CrystalFormulaFields.Add("BranchCode", "\"" + ddlBranch.SelectedItem.Text + "\"");
                    crBranchReconcilation.RecordSelectionFormula = strBranch + "='" + ddlBranch.SelectedItem.Value + "' and " + strHOBranch + "='" + ddlBranch.SelectedItem.Value + "'";
                    crBranchReconcilation.GenerateReport();
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
