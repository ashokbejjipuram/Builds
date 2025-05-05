#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.Others
{
    public partial class CashDiscountReport : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region page init
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

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    fnGenerateSelectionFormula();
                    if (crCashDiscount.Visible == false)
                        crCashDiscount.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Generate selection formula
        protected void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside Generate Selection Formula");
            try
            {
                string strSelectionFormula = default(string);
                string strDiscountCodeField = default(string);
                //string strBranchCodeField = default(string);
                string strReportName = default(string);

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpDiscCode");

                string strDiscountCode = SourceDDl.SelectedValue;
                strReportName = "CashDiscountMaster";
                strDiscountCodeField = "{cash_discount.cash_discount_code}";
                //strBranchCodeField = "{bank_branch_master.branch_name}";
                if (string.IsNullOrEmpty(strDiscountCode) && strDiscountCode.Equals("0"))
                    strSelectionFormula = "";

                if (!string.IsNullOrEmpty(strDiscountCode) && !strDiscountCode.Equals("0"))
                    strSelectionFormula = strDiscountCodeField + " = " + strDiscountCode;
                //if(!strBranchCode.Equals("CRP"))
                //    strSelectionFormula = strSelectionFormula + " and " + strBranchCodeField + "= '" + strBranchCode + "'";

                crCashDiscount.ReportName = strReportName;
                crCashDiscount.RecordSelectionFormula = strSelectionFormula;
                crCashDiscount.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region back button click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Inside btnBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("CashDiscount.aspx", false);
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
