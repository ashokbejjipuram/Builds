#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.Insurance
{
    public partial class ClassificationReport : System.Web.UI.Page
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
            //Log.WriteLog(source, "Page_Load", "Inside Page Load");
            try{
            if (!IsPostBack)
            {
                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                crClassification.ReportName = "Classification";
                crClassification.RecordSelectionFormula = "";
                crClassification.GenerateReport();
                if (crClassification.Visible == false)
                    crClassification.Visible = true;
            }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);

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
                Response.Redirect("Classification.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #endregion
    }
}
