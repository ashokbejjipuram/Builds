using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Finance
{
    public partial class PettyCashPageReport : System.Web.UI.Page
    {
        string strPettyCashNumber = default(string);
        string strPettyCashNumber1 = default(string);
        string strPettyCashDate = default(string);

        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    PreviousPage.Visible = false;
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("ddlPettyCashNumber");
                    strPettyCashNumber = SourceDDl.SelectedValue;
                    TextBox SourceText = (TextBox)placeHolder.FindControl("txtPettyCashNumber");
                    strPettyCashNumber1 = SourceText.Text;
                    TextBox SourceText1 = (TextBox)placeHolder.FindControl("txtPettyCashDate");
                    strPettyCashDate = SourceText1.Text;
                    GenerateSelectionFormula();
                 }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }            

        }
        #region Generation of Report
        /// <summary>
        /// Method to select reports for generation based on Indent Type
        /// </summary>
        protected void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Inside GenerateSelectionFormula()");
            try
            {
                
                string strSelectionFormula = string.Empty;
                string strPettyCashNumberfield = "{Petty_cash_Header.Petty_Cash_Number}";
                string strPettyCashDateField = "{Petty_cash_Header.Petty_Cash_Date}";
                string strBranchCodeVal = (string)Session["BranchCode"];
                string strCryDate = "Date (" + DateTime.ParseExact(strPettyCashDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                if (strBranchCodeVal != string.Empty && strBranchCodeVal != "CRP")
                {                    
                    if (!string.IsNullOrEmpty(strPettyCashNumber1) && strPettyCashNumber != "0")
                    {
                        strSelectionFormula = strPettyCashNumberfield + " = '" +strPettyCashNumber1 + "'";
                    }
                    else
                    {
                        strSelectionFormula = strPettyCashDateField + " = " + strCryDate;
                    }
                }

                crPettyCashPageReport.ReportName = "PettyCashPageReport";
                crPettyCashPageReport.RecordSelectionFormula = strSelectionFormula;
                crPettyCashPageReport.GenerateReport();
                if (crPettyCashPageReport.Visible == false)
                    crPettyCashPageReport.Visible = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion 

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("PettyCash.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

           
        }
    }
}
