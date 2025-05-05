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
    public partial class ChequeDishonouredStatement : System.Web.UI.Page
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

                    if (crChequeDishonoured_Report != null)
                    {
                        crChequeDishonoured_Report.Dispose();
                        crChequeDishonoured_Report = null;
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
            if (crChequeDishonoured_Report != null)
            {
                crChequeDishonoured_Report.Dispose();
                crChequeDishonoured_Report = null;
            }
        }
        protected void crChequeDishonoured_Report_Unload(object sender, EventArgs e)
        {
            if (crChequeDishonoured_Report != null)
            {
                crChequeDishonoured_Report.Dispose();
                crChequeDishonoured_Report = null;
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
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                
                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_addchqdishonour");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(dbcmd, "@From_date", DbType.String, strFromDate.Trim());
                ImpalDB.AddInParameter(dbcmd, "@To_date", DbType.String, strToDate.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(dbcmd);

                PanelHeaderDtls.Enabled = false;
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #endregion
        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string pstrBranchCode = default(string);
            string strReturnDate = default(string);
            string strSelectionFormula = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);

            pstrBranchCode = "{cheque_dishonour.branch_code}";
            strReturnDate = "{cheque_dishonour.returndate}";

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (strBranchCode == "CRP")
                strSelectionFormula = strReturnDate + ">=" + strCryFromDate + "and " + strReturnDate + "<=" + strCryToDate;
            else
                strSelectionFormula = strReturnDate + ">=" + strCryFromDate + "and " + strReturnDate + "<=" + strCryToDate + " and " + pstrBranchCode + "=" + "'" + strBranchCode + "'";

            crChequeDishonoured_Report.RecordSelectionFormula = strSelectionFormula;
            crChequeDishonoured_Report.GenerateReportAndExportHO(fileType);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("ChequeDishonouredStatement.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}