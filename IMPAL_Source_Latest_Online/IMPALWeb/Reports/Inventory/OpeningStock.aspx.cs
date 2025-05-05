#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class OpeningStock : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crOpeningStock != null)
                    {
                        crOpeningStock.Dispose();
                        crOpeningStock = null;
                    }

                    PopulateStationddl();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crOpeningStock != null)
            {
                crOpeningStock.Dispose();
                crOpeningStock = null;
            }
        }
        protected void crOpeningStock_Unload(object sender, EventArgs e)
        {
            if (crOpeningStock != null)
            {
                crOpeningStock.Dispose();
                crOpeningStock = null;
            }
        }

        #region PopulateStationddl
        public void PopulateStationddl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("Station");
                ddlStation.DataSource = oList;
                ddlStation.DataValueField = "DisplayValue";
                ddlStation.DataTextField = "DisplayText";
                ddlStation.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;

                        if (strBranchCode.Equals("CRP"))
                            strSelectionFormula = "{consignment.OS_LS_Indicator} = '" + ddlStation.SelectedValue + "'";
                        else
                            strSelectionFormula = "{Consignment.Branch_Code} = '" + strBranchCode + "' and {consignment.OS_LS_Indicator} = '" + ddlStation.SelectedValue + "'";

                        crOpeningStock.RecordSelectionFormula = strSelectionFormula;
                        crOpeningStock.GenerateReport();
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
