#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion
namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class LiabilityList_Branchwise : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crLiabilityList_Branchwise != null)
                    {
                        crLiabilityList_Branchwise.Dispose();
                        crLiabilityList_Branchwise = null;
                    }                    

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crLiabilityList_Branchwise != null)
            {
                crLiabilityList_Branchwise.Dispose();
                crLiabilityList_Branchwise = null;
            }
        }
        protected void crLiabilityList_Branchwise_Unload(object sender, EventArgs e)
        {
            if (crLiabilityList_Branchwise != null)
            {
                crLiabilityList_Branchwise.Dispose();
                crLiabilityList_Branchwise = null;
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    string strSelectionFormula = default(string);

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string strDateRecieved = default(string);
                    //string strInwardDate = default(string);
                    string strInwardNumber = default(string);

                    strDateRecieved = "{LiabilityList.Date_Received}";
                    //strInwardDate = "{LiabilityList.Inward_Date}";
                    strInwardNumber = "right({LiabilityList.Inward_Number},3)";
                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    if (strBranchCode != "CRP")
                        strSelectionFormula = strDateRecieved + ">=" + strCryFromDate + "and " + strDateRecieved + "<=" + strCryToDate + " and " + strInwardNumber + " ='" + strBranchCode + "'";
                    else
                        strSelectionFormula = strDateRecieved + ">=" + strCryFromDate + "and " + strDateRecieved + "<=" + strCryToDate;

                    crLiabilityList_Branchwise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crLiabilityList_Branchwise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crLiabilityList_Branchwise.RecordSelectionFormula = strSelectionFormula;
                    crLiabilityList_Branchwise.GenerateReport();
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
