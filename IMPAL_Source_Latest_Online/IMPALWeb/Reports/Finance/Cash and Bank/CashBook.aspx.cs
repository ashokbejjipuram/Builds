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
    public partial class CashBook : System.Web.UI.Page
    {
        string strBranchCode = default(string);

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
                    if (crCashBook_Report != null)
                    {
                        crCashBook_Report.Dispose();
                        crCashBook_Report = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateTransType();
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
            if (crCashBook_Report != null)
            {
                crCashBook_Report.Dispose();
                crCashBook_Report = null;
            }
        }
        protected void crCashBook_Report_Unload(object sender, EventArgs e)
        {
            if (crCashBook_Report != null)
            {
                crCashBook_Report.Dispose();
                crCashBook_Report = null;
            }
        }

        #region populate Transaction Type
        public void fnPopulateTransType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateTransType", "Inside fnPopulateTransType");
            try
            {
                IMPALLibrary.Transaction trans = new IMPALLibrary.Transaction();
                ddlTransactionType.DataSource = trans.GetAllTransactionItems();
                ddlTransactionType.DataTextField = "TransactionTypeDescription";
                ddlTransactionType.DataValueField = "TransactionTypeCode";
                ddlTransactionType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                int intProcStatus = default(int);
                int intProcStatus1 = default(int);

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_cbopenbal");
                ImpalDB.AddInParameter(dbcmd, "@from_date", DbType.String, strFromDate.Trim());
                ImpalDB.AddInParameter(dbcmd, "@end_date", DbType.String, strToDate.Trim());
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                DbCommand dbcmd1 = ImpalDB.GetStoredProcCommand("usp_cbopenbal1");
                ImpalDB.AddInParameter(dbcmd1, "@from_date", DbType.String, strFromDate.Trim());
                ImpalDB.AddInParameter(dbcmd1, "@end_date", DbType.String, strToDate.Trim());
                ImpalDB.AddInParameter(dbcmd1, "@Branch_Code", DbType.String, strBranchCode);
                dbcmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus1 = ImpalDB.ExecuteNonQuery(dbcmd1);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);            
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);
            string strTransType = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            strTransType = ddlTransactionType.SelectedValue;

            if (strTransType == "0")
                strTransType = "";

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            string strTransDate = default(string);
            string pstrBranchCode = default(string);
            string strTransTypeCode = default(string);
            string pstrCbBranchCode = default(string);

            strTransDate = "{V_Cashbank.Transaction_Date}";
            pstrBranchCode = "{V_Cashbank.Branch_Code}";
            strTransTypeCode = "{V_Cashbank.transaction_type_Code}";
            pstrCbBranchCode = "{cb_opening.Branch_Code}";

            if (strBranchCode != "CRP")
            {
                if (strTransType == "")
                    strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate + " and " + pstrBranchCode + " = '" + strBranchCode + "'";
                else
                    strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate + " and " + pstrBranchCode + " = '" + strBranchCode + "'" + " and" + strTransTypeCode + "=" + " '" + strTransType + "'";
            }
            else
            {
                if (strTransType == "")
                    strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate;
                else
                    strSelectionFormula = strTransDate + ">=" + strCryFromDate + " and " + strTransDate + "<=" + strCryToDate + " and" + strTransTypeCode + "=" + " '" + strTransType + "'";
            }

            strSelectionFormula = strSelectionFormula + " and " + pstrCbBranchCode + "=" + pstrBranchCode;

            crCashBook_Report.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crCashBook_Report.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crCashBook_Report.RecordSelectionFormula = strSelectionFormula;
            crCashBook_Report.GenerateReportAndExport(fileType);
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

            //Database ImpalDB = DataAccess.GetDatabase();
            //DbCommand cmd = null;
            //int timediff = 0;

            //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = 'CashBook_Report' order by datestamp desc");
            //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            //timediff = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            //if (timediff > 0 && timediff <= 600)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('You Are Again Generating this Report With in no Time. Please Wait for 10 Minutes');", true);
            //    btnReportPDF.Attributes.Add("style", "display:none");
            //    btnReportExcel.Attributes.Add("style", "display:none");
            //    btnBack.Attributes.Add("style", "display:inline");
            //    btnReport.Attributes.Add("style", "display:none");
            //    return;
            //}
            //else
            //{
                ReportsData reportsDt = new ReportsData();
                reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), "CashBook_Report");
            //}

            fnGenerateReport();

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CashBook.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}