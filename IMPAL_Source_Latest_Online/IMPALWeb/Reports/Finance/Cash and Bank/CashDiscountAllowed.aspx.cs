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

namespace IMPALWeb.Reports.Finance.CashAndBank
{
    public partial class CashDiscountAllowed : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crCashDiscountReport != null)
                    {
                        crCashDiscountReport.Dispose();
                        crCashDiscountReport = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateReportType();
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
            if (crCashDiscountReport != null)
            {
                crCashDiscountReport.Dispose();
                crCashDiscountReport = null;
            }
        }
        protected void crCashDiscountReport_Unload(object sender, EventArgs e)
        {
            if (crCashDiscountReport != null)
            {
                crCashDiscountReport.Dispose();
                crCashDiscountReport = null;
            }
        }

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

        #region Populate Report Type
        public void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Inside fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-CashDiscount");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
                    if (ddlReportType.SelectedValue == "Report")
                    {
                        crCashDiscountReport.ReportName = "CashDiscountReport";
                        fnGenerateSelectionFormula();
                    }
                    else
                    {
                        crCashDiscountReport.ReportName = "CashDiscountSummary";
                        fnGenerateSelectionFormula();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                string strCryFromDate = default(string);
                string strCryToDate = default(string);
                string strSelectionFormula = default(string);

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                string strDocNumber = default(string);
                string strBranch = default(string);
                string strDocDate = default(string);

                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                if (ddlReportType.SelectedValue == "Report")
                {
                    strBranch = "{Sales_Order_Header.Branch_Code}";
                    strDocDate = "{Sales_Order_Header.Document_Date}";
                }
                else
                {
                    strBranch = "{cdstatement.varchar}";
                    strDocDate = "{cdstatement.Date}";
                }

                if (strBranchCode != "CRP")
                    strSelectionFormula = strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate + " and " + strBranch + " ='" + strBranchCode + "'";
                else
                    strSelectionFormula = strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate;

                crCashDiscountReport.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crCashDiscountReport.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crCashDiscountReport.RecordSelectionFormula = strSelectionFormula;
                crCashDiscountReport.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
