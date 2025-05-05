#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.Bank
{
    public partial class BranchReport : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
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
            //Log.WriteLog(source, "Page_Load", "Inside Page Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    fnGenerateSelectionFormula();
                    if (crBranch.Visible == false)
                        crBranch.Visible = true;
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region SelectionFormula Formation
        /// <summary>
        /// Generate Selection Formula 
        /// </summary>
        protected void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Generating SelectionFormula");
            try
            {
                string strSelectionFormula = default(string);
                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("ddlBank");
                string strBankName = SourceDDl.SelectedValue;
                //string strBankName = Request.QueryString["BankName"];
                string strBankCodeField = default(string);
                //string strBranchCodeField = default(string);
                string strReportName = default(string);

                strReportName = "Branch";
                strBankCodeField = "{bank_branch_master.bank_code}";
                //strBranchCodeField = "{bank_branch_master.branch_name}";
                if (string.IsNullOrEmpty(strBankName) && strBankName.Equals("ALL"))
                    strSelectionFormula = "";

                if (!string.IsNullOrEmpty(strBankName) && !strBankName.Equals("ALL"))
                    strSelectionFormula = strBankCodeField + " = " + strBankName;
                //if(!strBranchCode.Equals("CRP"))
                //    strSelectionFormula = strSelectionFormula + " and " + strBranchCodeField + "= '" + strBranchCode + "'";

                crBranch.ReportName = strReportName;
                crBranch.RecordSelectionFormula = strSelectionFormula;
                crBranch.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Back Button Click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("Branch.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
