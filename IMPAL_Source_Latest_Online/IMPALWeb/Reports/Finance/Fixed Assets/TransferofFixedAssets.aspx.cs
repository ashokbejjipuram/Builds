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
    public partial class TransferofFixedAssets : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page Initialization
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
                    if (crtransferoffixedasset != null)
                    {
                        crtransferoffixedasset.Dispose();
                        crtransferoffixedasset = null;
                    }

                    txtFromTransferDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToTransferDate.Text = txtFromTransferDate.Text;
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
            if (crtransferoffixedasset != null)
            {
                crtransferoffixedasset.Dispose();
                crtransferoffixedasset = null;
            }
        }
        protected void crtransferoffixedasset_Unload(object sender, EventArgs e)
        {
            if (crtransferoffixedasset != null)
            {
                crtransferoffixedasset.Dispose();
                crtransferoffixedasset = null;
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
                    string strDateofTransfer = "{Fixed_Assets_Transfer.Date_of_Transfer}";
                    string strOldBranchcode = "{Fixed_Assets_Transfer.Old_Branch_code}";
                    string Date1 = txtFromTransferDate.Text;
                    string Date2 = txtToTransferDate.Text;
                    string FromTransferDate = string.Empty;
                    string ToTransferDate = string.Empty;

                    FromTransferDate = "Date (" + DateTime.ParseExact(Date1, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    ToTransferDate = "Date (" + DateTime.ParseExact(Date2, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (sessionvalue == "CRP")
                    {
                        strselectionformula = strDateofTransfer + ">=" + FromTransferDate + "and " + strDateofTransfer + "<=" + ToTransferDate;

                    }
                    else if (sessionvalue != "CRP")
                    {
                        strselectionformula = strDateofTransfer + ">=" + FromTransferDate + "and " + strDateofTransfer + "<=" + ToTransferDate + "and " + strOldBranchcode + "='" + sessionvalue + "'";
                    }

                    crtransferoffixedasset.CrystalFormulaFields.Add("From_Date", "'" + txtFromTransferDate.Text + "'");
                    crtransferoffixedasset.CrystalFormulaFields.Add("To_Date", "'" + txtToTransferDate.Text + "'");
                    crtransferoffixedasset.RecordSelectionFormula = strselectionformula;
                    crtransferoffixedasset.ReportName = "Fixed_Assets_Transfer";
                    crtransferoffixedasset.GenerateReport();
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
