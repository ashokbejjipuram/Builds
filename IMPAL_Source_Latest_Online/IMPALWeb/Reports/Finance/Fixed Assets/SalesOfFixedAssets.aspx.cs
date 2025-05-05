#region Namespace
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
    public partial class SalesOfFixedAssets : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page init
        /// <summary>
        /// To initialize page
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
                    if (crsalesoffixedasset != null)
                    {
                        crsalesoffixedasset.Dispose();
                        crsalesoffixedasset = null;
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
            if (crsalesoffixedasset != null)
            {
                crsalesoffixedasset.Dispose();
                crsalesoffixedasset = null;
            }
        }
        protected void crsalesoffixedasset_Unload(object sender, EventArgs e)
        {
            if (crsalesoffixedasset != null)
            {
                crsalesoffixedasset.Dispose();
                crsalesoffixedasset = null;
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
                    string strDateofSale = "{Fixed_Assets_Master.Date_of_Sale}";
                    string strBranchCode = "{Fixed_Assets_Master.Branch_code}";
                    string date1 = txtFromDate.Text;
                    string date2 = txtToDate.Text;
                    string FromDate = string.Empty;
                    string ToDate = string.Empty;

                    FromDate = "Date (" + DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    ToDate = "Date (" + DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (sessionvalue == "CRP")
                    {
                        strselectionformula = strDateofSale + ">=" + FromDate + "and " + strDateofSale + "<=" + ToDate;
                    }
                    else if (sessionvalue != "CRP")
                    {
                        strselectionformula = strDateofSale + ">=" + FromDate + "and " + strDateofSale + "<=" + ToDate + "and " + strBranchCode + "='" + sessionvalue + "'";
                    }

                    crsalesoffixedasset.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crsalesoffixedasset.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crsalesoffixedasset.RecordSelectionFormula = strselectionformula;
                    crsalesoffixedasset.ReportName = "Fixed_Assets_Sales";
                    crsalesoffixedasset.GenerateReport();
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
