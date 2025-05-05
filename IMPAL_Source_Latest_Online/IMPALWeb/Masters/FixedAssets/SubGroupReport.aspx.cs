#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.FixedAssets
{
    public partial class SubGroupReport : System.Web.UI.Page
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
                Response.Redirect("SubGroup.aspx", false);
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
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
            try
            {
                string strSelectionFormula = default(string);
                string strFASubGroup = default(string);

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpAssertGroup");

                string strSubGroupCode = SourceDDl.SelectedValue;
                strFASubGroup = "{Fixed_Assets_Sub_Group.FA_Sub_Group_Code}";

                if (strBranchCode == "CRP")
                    strSelectionFormula = null;
                else
                {
                    if (strSubGroupCode == "" || strSubGroupCode == "ALL")
                        strSelectionFormula = null;
                    else
                        strSelectionFormula = strFASubGroup + "=" + " " + Convert.ToInt16(strSubGroupCode);
                }
                crFASubGroup.RecordSelectionFormula = strSelectionFormula;
                crFASubGroup.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
