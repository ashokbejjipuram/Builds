using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IMPALLibrary;

using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;


namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class OrderLockedDetailsCorp : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    if (OrderLockCorp != null)
                    {
                        OrderLockCorp.Dispose();
                        OrderLockCorp = null;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (OrderLockCorp != null)
            {
                OrderLockCorp.Dispose();
                OrderLockCorp = null;
            }
        }
        protected void OrderLockCorp_Unload(object sender, EventArgs e)
        {
            if (OrderLockCorp != null)
            {
                OrderLockCorp.Dispose();
                OrderLockCorp = null;
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string sqlquery = string.Empty;
                    string selection_formula = string.Empty;

                    string strPODate = "{Purchase_Order_Header.PO_date}";
                    string strFrombranchcode = "{Purchase_Order_Header.branch_code}";
                    string rptName = "BranchWise_Ponumber_Cancelled";

                    var strFromDate = txtFromDate.Text.Split('/');
                    var strToDate = txtToDate.Text.Split('/');

                    string Dfrom_Param = string.Format("Date({0},{1},{2})", strFromDate[2], strFromDate[1], strFromDate[0]);
                    string Dto_Param = string.Format("Date({0},{1},{2})", strToDate[2], strToDate[1], strToDate[0]);

                    if (strBranchCode.Equals("CRP"))
                    {
                        selection_formula = strPODate + ">=" + Dfrom_Param + " and " + strPODate + "<=" + Dto_Param;
                    }
                    else
                    {
                        selection_formula = strPODate + ">=" + Dfrom_Param + " and " + strPODate + "<=" + Dto_Param + " and " + strFrombranchcode + "='" + strBranchCode + "'";
                    }

                    selection_formula = selection_formula + " and {purchase_Order_Header.Status} ='Z'";

                    OrderLockCorp.ReportName = rptName;
                    OrderLockCorp.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    OrderLockCorp.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    OrderLockCorp.RecordSelectionFormula = selection_formula;
                    OrderLockCorp.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
