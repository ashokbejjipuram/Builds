#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion
namespace IMPALWeb.Reports.Finance.Fixed_Assets
{
    public partial class VehicleMaintenenceStatement : System.Web.UI.Page
    {
        #region Declaration
        string SessionValue = string.Empty;
        #endregion

        #region Page init
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    SessionValue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crvehiclemaintenece != null)
                    {
                        crvehiclemaintenece.Dispose();
                        crvehiclemaintenece = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
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
            if (crvehiclemaintenece != null)
            {
                crvehiclemaintenece.Dispose();
                crvehiclemaintenece = null;
            }
        }
        protected void crvehiclemaintenece_Unload(object sender, EventArgs e)
        {
            if (crvehiclemaintenece != null)
            {
                crvehiclemaintenece.Dispose();
                crvehiclemaintenece = null;
            }
        }
        #region Button Report Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strSelectionFormula = string.Empty;
                    string strDateofMaintenence = "{Fixed_Assets_Maintenance.Date_of_Maintenance}";
                    string strBranchCode = "{Fixed_Assets_master.Branch_code}";
                    string Date1 = txtFromDate.Text;
                    string Date2 = txtToDate.Text;
                    string FromDate = string.Empty;
                    string ToDate = string.Empty;

                    FromDate = "Date (" + DateTime.ParseExact(Date1, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    ToDate = "Date (" + DateTime.ParseExact(Date2, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (SessionValue == "CRP")
                    {
                        strSelectionFormula = strDateofMaintenence + ">=" + FromDate + "and " + strDateofMaintenence + "<=" + ToDate;
                    }
                    else if (SessionValue != "CRP")
                    {
                        strSelectionFormula = strDateofMaintenence + ">=" + FromDate + "and " + strDateofMaintenence + "<=" + ToDate + "and " + strBranchCode + "='" + SessionValue + "'";
                    }

                    crvehiclemaintenece.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crvehiclemaintenece.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crvehiclemaintenece.RecordSelectionFormula = strSelectionFormula;
                    crvehiclemaintenece.ReportName = "Vehicle_maintenance";
                    crvehiclemaintenece.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}

