#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.GeneralLedger
{
    public partial class BudgetMasterReport : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    strBranchCode = Session["BranchCode"].ToString();
                    //crGLBudgetMaster.RecordSelectionFormula = "";
                    //crGLBudgetMaster.GenerateReport();
                    fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page init
        public void Page_Init(object sender, EventArgs e)
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

        #region Back button click
        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Inside btnBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("BudgetMaster.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Seldection formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
            try
            {
            string strSelectionFromula = default(string);
            string strBudgetYear = default(string);
            string strBudgetBranch = default(string);

            strBudgetYear = "{budget_master.Budget_Year}";
            strBudgetBranch = "{Chart_of_Account.Branch_Code}";
              
            Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
            DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpBY");

                string strDdlBudget = SourceDDl.SelectedValue;
                if (strBranchCode == "CRP")
                {
                    if (strDdlBudget != "ALL")
                        strSelectionFromula = strBudgetYear + "=" + strDdlBudget;
                    else
                        strSelectionFromula = null;
                }
                else
                {
                    if (strDdlBudget != "ALL")
                        strSelectionFromula = strBudgetYear + "=" + " " + strDdlBudget + " and " + strBudgetBranch + "='" + strBranchCode + "'";
                    else
                        strSelectionFromula = null;
                }

            crGLBudgetMaster.RecordSelectionFormula = strSelectionFromula;
            crGLBudgetMaster.GenerateReport();
        }
        catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
#endregion

    }
}
