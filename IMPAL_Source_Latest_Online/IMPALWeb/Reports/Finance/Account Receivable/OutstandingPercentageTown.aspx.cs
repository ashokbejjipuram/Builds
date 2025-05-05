#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion
namespace IMPALWeb.Reports.Finance.Account_Receivable
{
    public partial class OutstandingPercentageTown : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page Init
        /// <summary>
        /// To Initialize page
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
        /// To Load page
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
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (croutstandingtown != null)
                    {
                        croutstandingtown.Dispose();
                        croutstandingtown = null;
                    }

                    loadbranch();
                    loadtown(sessionvalue);
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
            if (croutstandingtown != null)
            {
                croutstandingtown.Dispose();
                croutstandingtown = null;
            }
        }
        protected void croutstandingtown_Unload(object sender, EventArgs e)
        {
            if (croutstandingtown != null)
            {
                croutstandingtown.Dispose();
                croutstandingtown = null;
            }
        }

        #region Generate Report
        /// <summary>
        /// To Generate Report
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
                    croutstandingtown.RecordSelectionFormula = GenerateSelectionFormula();
                    croutstandingtown.ReportName = "impal_outstandingpercenttown";
                    croutstandingtown.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Load branch
        /// <summary>
        /// To Load branch values in dropdown list
        /// </summary>
        protected void loadbranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "loadbranch", "Entering loadbranch");
            try
            {
                IMPALLibrary.Branches branches = new IMPALLibrary.Branches();
                List<IMPALLibrary.Branch> lstbranch = new List<IMPALLibrary.Branch>();
                lstbranch = branches.GetCorpBranch();
                lstbranch.RemoveAt(0);
                ddlBranch.DataSource = lstbranch;
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
                if (sessionvalue != "CRP")
                {
                    ddlBranch.SelectedValue = sessionvalue;
                    //ddlBranch.Enabled = false;
                }
                //else
                //{ ddlBranch.Enabled = true; }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Load Town
        /// <summary>
        /// To load town names according to selected branch
        /// </summary>
        /// <param name="branchcode"></param>
        protected void loadtown(string branchcode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "loadtown", "Entering loadtown");
            try
            {
                if (sessionvalue != null)
                {
                    IMPALLibrary.Towns towns = new IMPALLibrary.Towns();
                    List<IMPALLibrary.Town> lstfromtown = new List<IMPALLibrary.Town>();

                    lstfromtown = towns.GetAllTowns(branchcode);
                    ddlFromTown.DataSource = lstfromtown;
                    ddlFromTown.DataValueField = "TownName";
                    ddlFromTown.DataTextField = "TownName";
                    ddlFromTown.DataBind();

                    ddlToTown.DataSource = lstfromtown;
                    ddlToTown.DataValueField = "TownName";
                    ddlToTown.DataTextField = "TownName";
                    ddlToTown.DataBind();
                    //if (branchcode != "0")
                    //{
                    //    ddlFromTown.Enabled = true;
                    //    ddlToTown.Enabled = true;
                    //}
                    //else
                    //{
                    //    ddlFromTown.Enabled = false;
                    //    ddlToTown.Enabled = false;
                    //}
                }
                // "select Town_Name from Town_master where branch_code="&"'" & Request.form("cbobranch")&"' order by Town_name"
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlBranch_SelectedIndexChanged
        /// <summary>
        /// To load values for town dropdown 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadtown(ddlBranch.SelectedValue);
        }
        #endregion

        #region SElection formula
        /// <summary>
        /// To Generate Selection Formula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected string GenerateSelectionFormula()
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "GenerateSelectionFormula", "Entering GenerateSelectionFormula");
             string strselectionformula = string.Empty;
            try
            {
            string strcusttownplace = "{cust_town.place}";
            string strcusttowncode = "{cust_town.Branch_Code}";
           
            string strBranchCode = ddlBranch.SelectedValue;
            string strFromTown = ddlFromTown.SelectedValue;
            string strToTown = ddlToTown.SelectedValue;
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_ospercenttown1_new");
            ImpalDB.AddInParameter(cmd, "@Town1", DbType.String, ddlFromTown.SelectedValue);
            ImpalDB.AddInParameter(cmd, "@Town2", DbType.String, ddlToTown.SelectedValue);
            ImpalDB.AddInParameter(cmd, "@Branch", DbType.String, strBranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

            if (sessionvalue == "CRP")
            {
                strselectionformula = strcusttownplace + " >= '" + strFromTown + "' or " + strcusttownplace + " <='" + strToTown + "'";
            }
            else if (sessionvalue != "CRP" && (ddlFromTown.SelectedValue != "" || ddlFromTown.SelectedValue != null || ddlFromTown.SelectedValue != string.Empty))
            {
                strselectionformula = strcusttowncode + "= '" + sessionvalue + "' and (" + strcusttownplace + " >='" + strFromTown + "' or " + strcusttownplace + " <='" + strToTown + "' )";
            }
            else if (sessionvalue != "CRP" && (ddlFromTown.SelectedValue == "" || ddlFromTown.SelectedValue == null))
            {
                strselectionformula = strcusttowncode + "= '" + sessionvalue + "'";
            }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return strselectionformula;
        }
        #endregion

    }
}
