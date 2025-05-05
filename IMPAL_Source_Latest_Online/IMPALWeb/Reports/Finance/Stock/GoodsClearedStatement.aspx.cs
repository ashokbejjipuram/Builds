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

namespace IMPALWeb.Reports.Finance.Stock
{
    public partial class GoodsClearedStatement : System.Web.UI.Page
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
                    if (crGoodClearedStatement != null)
                    {
                        crGoodClearedStatement.Dispose();
                        crGoodClearedStatement = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateStatementType();
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
            if (crGoodClearedStatement != null)
            {
                crGoodClearedStatement.Dispose();
                crGoodClearedStatement = null;
            }
        }
        protected void crGoodClearedStatement_Unload(object sender, EventArgs e)
        {
            if (crGoodClearedStatement != null)
            {
                crGoodClearedStatement.Dispose();
                crGoodClearedStatement = null;
            }
        }

        #region Populate Insurance Type
        public void fnPopulateStatementType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-GoodsCleared");
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

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);


            if (ddlReportType.SelectedValue == "Y")
                crGoodClearedStatement.ReportName = "GoodClearedStatement";
            else if (ddlReportType.SelectedValue == "N")
                crGoodClearedStatement.ReportName = "GoodNonClearedStatement";
            
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string strInwardBrCode = default(string);
            string strInwardDate = default(string);
            string strDateOpened = default(string);

            strInwardBrCode = "{Inward_Header.branch_code}";
            strInwardDate = "{Inward_Header.Inward_Date}";
            strDateOpened = "{Inward_Header.Date_Opened}";

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (strBranchCode == "CRP")
                strSelectionFormula = strInwardDate + ">=" + strCryFromDate + " and " + strInwardDate + "<=" + strCryToDate;
            else
            {
                strSelectionFormula = strInwardDate + ">=" + strCryFromDate + " and " + strInwardDate + "<=" + strCryToDate + " and " + strInwardBrCode + " ='" + strBranchCode + "'";

                if (ddlReportType.SelectedValue == "Y")
                    strSelectionFormula = strSelectionFormula + " and not isnull(" + strDateOpened + ")";
                else if (ddlReportType.SelectedValue == "N")
                    strSelectionFormula = strSelectionFormula + " and isnull(" + strDateOpened + ")";
            }

            crGoodClearedStatement.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crGoodClearedStatement.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crGoodClearedStatement.RecordSelectionFormula = strSelectionFormula;
            crGoodClearedStatement.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("GoodsClearedStatement.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}