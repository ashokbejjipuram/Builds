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
    public partial class GLGroupReport : System.Web.UI.Page
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
                    if (crGLGroup.Visible == false)
                        crGLGroup.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region Generate SelectionFormula
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
                string strGLClassificationCode = default(string);
                string strGLClassificationCodeField = default(string);
                string strReportName = default(string);
                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpGLClassification");
                //strGLClassificationCode = Request.QueryString["GLClassificationCode"];
                strGLClassificationCode = SourceDDl.SelectedValue;
                strGLClassificationCodeField = "{GL_Group.Gl_Classification_Code}";
                strReportName = "GLGroup";
                if (strGLClassificationCode == "ALL")
                {
                    strSelectionFormula = "";

                }
                else
                {
                    strSelectionFormula = strGLClassificationCodeField + "=" + " '" + strGLClassificationCode + "'";
                }
                crGLGroup.ReportName = strReportName;
                crGLGroup.RecordSelectionFormula = strSelectionFormula;
                crGLGroup.GenerateReport();
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
                Response.Redirect("GLGroup.aspx", false);
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
