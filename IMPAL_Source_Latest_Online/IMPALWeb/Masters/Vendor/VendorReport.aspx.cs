#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.Customer
{
    public partial class CustomerReport : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("CrystalReport");
                    strBranchCode = Session["BranchCode"].ToString();
                    fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
        public void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
            try
            {
                if (Session["CrystalReport"] != null)
                    crCustomer.GenerateReport();
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
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Session.Remove("CrystalReport");
                Server.ClearError();
                Response.Redirect("Customer.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate selection formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Generating SelectionFormula");
            try
            {
                string strSelectionFomrula = default(string);
                string strCutMasterCustCode = default(string);
                string pstrBranchCode = default(string);

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpCustomerCode");

                string strCustCode = SourceDDl.SelectedValue;
                strCutMasterCustCode = "{customer_master.customer_code}";
                pstrBranchCode = "{branch_master.branch_code}";

                strSelectionFomrula = strCutMasterCustCode + "=" + " '" + strCustCode + "'";
                if (strBranchCode != "CRP")
                {
                    if (strCustCode != "")
                        strSelectionFomrula = pstrBranchCode + " ='" + strBranchCode + "' and " + strCutMasterCustCode + "=" + " '" + strCustCode + "'";
                    else
                        strSelectionFomrula = null;
                }
                else if (strBranchCode == "CRP")
                {
                    if (strCustCode != "")
                        strSelectionFomrula = strCutMasterCustCode + "=" + " '" + strCustCode + "'";
                    else
                        strSelectionFomrula = null;
                }
                crCustomer.RecordSelectionFormula = strSelectionFomrula;
                crCustomer.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
