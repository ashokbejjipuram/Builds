using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class CancelledOrderDetails : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crCancelledOrderDetails_HO != null)
                    {
                        crCancelledOrderDetails_HO.Dispose();
                        crCancelledOrderDetails_HO = null;
                    }                    

                    PopulateReportType();
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crCancelledOrderDetails_HO != null)
            {
                crCancelledOrderDetails_HO.Dispose();
                crCancelledOrderDetails_HO = null;
            }
        }
        protected void crCancelledOrderDetails_HO_Unload(object sender, EventArgs e)
        {
            if (crCancelledOrderDetails_HO != null)
            {
                crCancelledOrderDetails_HO.Dispose();
                crCancelledOrderDetails_HO = null;
            }
        }

        #region PopulateReportType
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateReportType", "Entering PopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-Cancel");
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

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlReportType.SelectedValue == "CANCEL By H.O")
                        crCancelledOrderDetails_HO.ReportName = "CancelledOrderDetails_HO";
                    else if (ddlReportType.SelectedValue == "CANCEL By Branch")
                        crCancelledOrderDetails_HO.ReportName = "CancelledOrderDetails_Branch";

                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strReportType = ddlReportType.Text;
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            int intProcStatus = default(int);

            string strSuppFrom = ddlFromLineCd.SelectedValue;
            string strSuppTo = ddlToLineCd.SelectedValue;

            if (strSuppFrom == "0")
                strSuppFrom = "";
            else if (strSuppTo == "0")
                strSuppTo = "";

            bool blnFlag = false;
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula", "Entering GenerateSelectionFormula");
            try
            {
                strCryFromDate = "Date(" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                strCryToDate = "Date(" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                string strCode = default(string);
                string strSupplierCode = default(string);
                string strDate = default(string);

                strSupplierCode = "{Purchase_Order_Header.Supplier_Code}";
                strDate = "{Purchase_Order_Header.PO_Date}";
                strCode = "{Purchase_Order_Header.Branch_Code}";

                if (strBranchCode == "CRP")
                {
                    if (strSuppFrom != "" && strSuppTo == "" && strFromDate == "" && strToDate == "")
                        strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "'";
                    else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate == "" && strToDate == ""))
                        strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "'";
                    else if ((strSuppFrom == "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                    else if ((strSuppFrom != "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strSupplierCode + "'" + strSuppFrom + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                    else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                    else if (strFromDate == "" && strToDate == "")
                        strSelectionFormula = strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                }
                else
                {
                    if (strSuppFrom != "" && strSuppTo == "" && strFromDate == "" && strToDate == "")
                        strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "' and " + strCode + "='" + strBranchCode + "'";
                    else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate == "" && strToDate == ""))
                        strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "' and " + strCode + "='" + strBranchCode + "'";
                    else if ((strSuppFrom == "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strCode + "='" + strBranchCode + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                    else if ((strSuppFrom != "" && strSuppTo == "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strSupplierCode + "='" + strSuppFrom + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate + " and " + strCode + "='" + strBranchCode + "'";
                    else if ((strSuppFrom != "" && strSuppTo != "") && (strFromDate != "" && strToDate != ""))
                        strSelectionFormula = strSupplierCode + ">='" + strSuppFrom + "' and " + strSupplierCode + "<='" + strSuppTo + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate + " and " + strCode + "='" + strBranchCode + "'";
                    else if (strFromDate != "" && strToDate != "")
                        strSelectionFormula = strCode + "='" + strBranchCode + "' and " + strDate + ">=" + strCryFromDate + " and " + strDate + "<=" + strCryToDate;
                }

                if (strSelectionFormula == "" || strSelectionFormula == default(string))
                {
                    if (ddlReportType.SelectedValue == "CANCEL By H.O")
                        strSelectionFormula = strSelectionFormula + "{Purchase_order_Schedule.Status} ='I'";
                    else if (ddlReportType.SelectedValue == "CANCEL By Branch")
                        strSelectionFormula = strSelectionFormula + "{Purchase_order_Schedule.Status} ='C'";
                }
                else
                {
                    if (ddlReportType.SelectedValue == "CANCEL By H.O")
                        strSelectionFormula = strSelectionFormula + " and {Purchase_order_Schedule.Status} ='I'";
                    else if (ddlReportType.SelectedValue == "CANCEL By Branch")
                        strSelectionFormula = strSelectionFormula + " and {Purchase_order_Schedule.Status} ='C'";
                }

                crCancelledOrderDetails_HO.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crCancelledOrderDetails_HO.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crCancelledOrderDetails_HO.RecordSelectionFormula = strSelectionFormula;
                crCancelledOrderDetails_HO.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
