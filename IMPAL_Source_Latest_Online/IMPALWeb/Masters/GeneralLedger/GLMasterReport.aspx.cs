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
    public partial class GLMasterReport : System.Web.UI.Page
    {
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
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    fnGenerateSelectionFormula();

                    if (crGLMaster.Visible == false)
                        crGLMaster.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Generate Selection Formula
        /// <summary>
        /// To Generate Selection Formula
        /// </summary>
        protected void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Generating Selection Formula");
            try
            {
                string strSelectionFormula = default(string);
                string strGLGroupCode = default(string);
                string strGLGroupCodeField = default(string);
                string strReportName = default(string);
                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpGlGroupDesc");
                strGLGroupCode = SourceDDl.SelectedValue;
                //strGLGroupCode = Request.QueryString["GLGroupCode"];
                strGLGroupCodeField = "{GL_Master.GL_Group_Code}";
                strReportName = "GLMaster";
                if (strGLGroupCode == "ALL")
                {
                    strSelectionFormula = "";

                }
                else
                {
                    strSelectionFormula = strGLGroupCodeField + "=" + " '" + strGLGroupCode + "'";
                }
                crGLMaster.ReportName = strReportName;
                crGLMaster.RecordSelectionFormula = strSelectionFormula;
                crGLMaster.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Backbtn Click
        protected void btnGLMasterBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnGLMasterBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("GLMaster.aspx", false);
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
