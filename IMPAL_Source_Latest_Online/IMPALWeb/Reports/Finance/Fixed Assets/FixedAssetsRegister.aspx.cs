#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Reports.Fixed_Assets
{
    public partial class FixedAssetsRegister : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page init
        /// <summary>
        /// To Initialize page
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
        /// To load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crfixedassetsregister != null)
                    {
                        crfixedassetsregister.Dispose();
                        crfixedassetsregister = null;
                    }

                    loadgroup();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crfixedassetsregister != null)
            {
                crfixedassetsregister.Dispose();
                crfixedassetsregister = null;
            }
        }
        protected void crfixedassetsregister_Unload(object sender, EventArgs e)
        {
            if (crfixedassetsregister != null)
            {
                crfixedassetsregister.Dispose();
                crfixedassetsregister = null;
            }
        }

        #region btnReport_Click
        /// <summary>
        /// To generate report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strselectionformula = string.Empty;

                    string strFixedAssetsGroup = "{Fixed_Assets_Group.FA_Group_Description}";
                    string strFixedAssetsGroup1 = "{Fixed_Assets_Group.FA_Group_Description}";
                    string strFixedAssetsBranch = "{Fixed_Assets_Master.Branch_code}";

                    if (sessionvalue == "CRP")
                    {
                        strselectionformula = strFixedAssetsGroup + ">=" + " '" + ddlFromFAGroup.SelectedValue + "' and " + strFixedAssetsGroup1 + "<=" + " '" + ddlToFAGroup.SelectedValue + "'";
                    }
                    else
                    {
                        strselectionformula = strFixedAssetsGroup + ">=" + " '" + ddlFromFAGroup.SelectedValue + "' and " + strFixedAssetsGroup + "<=" + " '" + ddlToFAGroup.SelectedValue + "' and " + strFixedAssetsBranch + "=" + " '" + sessionvalue + "'";
                    }

                    crfixedassetsregister.RecordSelectionFormula = strselectionformula;
                    crfixedassetsregister.ReportName = "Fixed_Assets_Register";
                    crfixedassetsregister.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);


            }
        }
        #endregion

        #region Load Group
        /// <summary>
        /// To load group code values in dropdowns
        /// </summary>
        protected void loadgroup()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "loadgroup", "Entering loadgroup");
            try
            {
                IMPALLibrary.GLFixedAssetGroups Groups = new IMPALLibrary.GLFixedAssetGroups();

                List<IMPALLibrary.GLFixedAssetGroup> lstGroup = new List<IMPALLibrary.GLFixedAssetGroup>();
                lstGroup = Groups.GetAllGLFixedGroups();
                lstGroup.Insert(0, new IMPALLibrary.GLFixedAssetGroup("", ""));
                ddlFromFAGroup.DataSource = lstGroup;
                ddlFromFAGroup.DataValueField = "GLCLDescription";
                ddlFromFAGroup.DataTextField = "GLCLDescription";
                ddlFromFAGroup.DataBind();
                ddlToFAGroup.DataSource = lstGroup;
                ddlToFAGroup.DataValueField = "GLCLDescription";
                ddlToFAGroup.DataTextField = "GLCLDescription";
                ddlToFAGroup.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
