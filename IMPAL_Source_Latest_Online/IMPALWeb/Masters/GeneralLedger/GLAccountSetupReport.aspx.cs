using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;


namespace IMPALWeb.Masters.GeneralLedger
{
    public partial class GLAccountSetupReport : System.Web.UI.Page
    {
        #region Page_Init
        /// <summary>
        /// To initialize page.
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
        /// To Load Page.Report is generated during Page Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack && PreviousPage != null)
                {
                    crGLAccountSetup.ReportName = "AccountSetup";
                    string strCode = "{Account_Setup.control_number}";
                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("ddlNumber");
                    //DropDownList SourceDDl = (DropDownList)PreviousPage.Controls[0].FindControl("ddlNumber");
                    string strCodeVal = SourceDDl.SelectedValue;
                    string BranchCode = (string)Session["BranchCode"];

                    //string InsuranceBranchCode = "Insurance_claims.br_code";
                    //string strCodeVal = (string)Request.QueryString["Code"];
                    string strSelectionFormula = string.Empty;
                    if (strCodeVal != string.Empty && strCodeVal != "ALL" && strCodeVal!="0")
                    {
                       
                            strSelectionFormula = strCode + "=" + strCodeVal ;
                       
                       
                    }
                    crGLAccountSetup.RecordSelectionFormula = strSelectionFormula;
                    crGLAccountSetup.GenerateReport();
                    if (crGLAccountSetup.Visible == false)
                        crGLAccountSetup.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("GLAccountSetup.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
