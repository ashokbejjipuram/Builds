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
    public partial class CollectionReport : System.Web.UI.Page
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
                    if (crCollection_Report != null)
                    {
                        crCollection_Report.Dispose();
                        crCollection_Report = null;
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
            if (crCollection_Report != null)
            {
                crCollection_Report.Dispose();
                crCollection_Report = null;
            }
        }
        protected void crCollection_Report_Unload(object sender, EventArgs e)
        {
            if (crCollection_Report != null)
            {
                crCollection_Report.Dispose();
                crCollection_Report = null;
            }
        }

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
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            string strTransDate = default(string);
            string pstrBranchCode = default(string);

            strTransDate = "{general_ledger_detail.document_date}";
            pstrBranchCode = "{branch_master.Branch_code}";

            if (strBranchCode != "CRP")
                strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate + " and " + pstrBranchCode + " = '" + strBranchCode + "'";
            else
                strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate;

            crCollection_Report.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crCollection_Report.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crCollection_Report.RecordSelectionFormula = strSelectionFormula;
            crCollection_Report.GenerateReportAndExportHO(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CollectionReport.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}