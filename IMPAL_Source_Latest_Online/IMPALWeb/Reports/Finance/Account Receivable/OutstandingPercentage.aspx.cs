#region Namespace Declaration
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
#endregion

namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class OutstandingPercentage : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "OutstandingPercentage Page Init Method"); 
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
            //Log.WriteLog(Source, "Page_Load", "OutstandingPercentage Page Load Method"); 
            try
            {
                if (!IsPostBack)
                {
                    sessionvalue = (string)Session["BranchCode"];

                    if (crOutPercentage != null)
                    {
                        crOutPercentage.Dispose();
                        crOutPercentage = null;
                    }

                    fnPopulateBranch();
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
            if (crOutPercentage != null)
            {
                crOutPercentage.Dispose();
                crOutPercentage = null;
            }
        }
        protected void crOutPercentage_Unload(object sender, EventArgs e)
        {
            if (crOutPercentage != null)
            {
                crOutPercentage.Dispose();
                crOutPercentage = null;
            }
        }

        #region Branch Dropdown populate Method
        protected void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateBranch()", "OutstandingPercentage Branch Dropdown populate Method"); 
            try
            {
                IMPALLibrary.Branches objBranch = new IMPALLibrary.Branches();
                ddlFromBranch.DataSource = objBranch.GetCorpBranch();
                ddlFromBranch.DataValueField = "BranchCode";
                ddlFromBranch.DataTextField = "BranchName";
                ddlFromBranch.DataBind();
                ddlToBranch.DataSource = objBranch.GetCorpBranch();
                ddlToBranch.DataValueField = "BranchCode";
                ddlToBranch.DataTextField = "BranchName";
                ddlToBranch.DataBind();
                ddlFromBranch.SelectedValue = sessionvalue;
                ddlToBranch.SelectedValue = sessionvalue;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    if (Session["BranchCode"] != null)
                        sessionvalue = (string)Session["BranchCode"];

                    string selectionformula = string.Empty;
                    string strFromBranch = ddlFromBranch.SelectedValue;
                    string strToBranch = ddlToBranch.SelectedValue;
                    string strselBranch = "{cust_percent.Branch_code}";
                    crOutPercentage.ReportName = "impal_outstandingpercent";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_ospercent_new");
                    ImpalDB.AddInParameter(cmd, "@Branch1", DbType.String, strFromBranch);
                    ImpalDB.AddInParameter(cmd, "@Branch2", DbType.String, strToBranch);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    if (sessionvalue == "CRP")
                    {
                        selectionformula = strselBranch + " >='" + strFromBranch + "' or" + strselBranch + " <='" + strToBranch + "'";
                    }
                    else
                    {
                        selectionformula = strselBranch + " >='" + strFromBranch + "' or" + strselBranch + " <='" + strToBranch + "'";
                    }
                    crOutPercentage.RecordSelectionFormula = selectionformula;
                    crOutPercentage.GenerateReport();
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
