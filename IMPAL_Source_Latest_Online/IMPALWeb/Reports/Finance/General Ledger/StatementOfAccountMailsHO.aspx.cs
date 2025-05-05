#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using AjaxControlToolkit;
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class StatementOfAccountMailsHO : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        string selectionformula = string.Empty;
        string strselAccCode = string.Empty;        

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Statement Of Account Page Init Method");
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "Statement Of Account Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    lblFromCustomer.Text = "From Customer";
                    lblToCustomer.Text = "To Customer";

                    divCustomerInfo.Visible = false;
                    fnPopulateCustomerType();
                    fnPopulateReportType();
                    fnPopulateMonthYear();
                    cboFromCustomer.SelectedIndex = 0;
                    cboToCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {           
        }
        protected void crStatementOfAccountMailsHO_Unload(object sender, EventArgs e)
        {
        }

        #region To Populate Customer type Dropdown
        protected void fnPopulateCustomerType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateCustomerType()", "Statement Of Account Populate Customer Type Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlCustomerType.DataSource = oCommon.GetDropDownListValues("StatementOfAccountMailsHO");
                ddlCustomerType.DataTextField = "DisplayText";
                ddlCustomerType.DataValueField = "DisplayValue";
                ddlCustomerType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region To Populate Report type Dropdown
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "Statement Of Account Populate Populate Report type Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("StatementOfAccountMailsHO-RptType");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Function to Populate Month and year
        public void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateMonthYear()", "Statement Of Account Populate Monthyear Method");
            try
            {
                IMPALLibrary.Masters.Item_wise_sales objItem = new IMPALLibrary.Masters.Item_wise_sales();
                ddlMonthYear.DataSource = objItem.GetMonthYearHOMails(strBranchCode);
                ddlMonthYear.DataValueField = "month_year";
                ddlMonthYear.DataTextField = "month_year";
                ddlMonthYear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                string strMonYr = string.Empty;
                string strEndDate_DDMM = string.Empty;
                string strEndDate_MMDD = string.Empty;
                string strStartDate_YYMM = string.Empty;
                string strMonthYear = ddlMonthYear.SelectedValue;
                string strCustomerType = ddlCustomerType.SelectedValue;
                string strFromCustomer = cboFromCustomer.SelectedValue;
                string strToCustomer = cboToCustomer.SelectedValue;

                if (ddlMonthYear.SelectedValue != "")
                {
                    string strFormattedMonYr = string.Format("{0}/{1}", strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4));

                    strMonYr = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4)); //strMonYr
                    strEndDate_DDMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(2, 4));
                    strEndDate_MMDD = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))));
                    strStartDate_YYMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), "01");
                }

                fnCustomerTown(strMonYr, strCustomerType, strEndDate_DDMM, strFromCustomer, strToCustomer, strBranchCode, strMonthYear, strStartDate_YYMM, strEndDate_MMDD);
                PanelHeaderDtls.Enabled = false;
                
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");
                btnSendMails.Attributes.Add("style", "display:inline");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Data Has been Generated Successfully. Please Click on Send Mails Button to Send Emails to Dealers.');", true);                
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnSendMails_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            btnSendMails.Attributes.Add("style", "display:none");
            btnSendMails.Visible = false;
            btnBack.Attributes.Add("style", "display:inline");
            GenerateAndExportReport(".pdf");
        }
        #endregion
        protected void fnCustomerTown(string strMonYr, string strCustomerType, string strEndDate_DDMM, string strFromCustomer, string strToCustomer, string strBranchCode, string strMonthYear, string strStartDate_YYMM, string strEndDate_MMDD)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnCustomerTown()", "Statement Of Account Custmer and Townwise Method");
            try
            {
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calcustos_HO");
                ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strEndDate_DDMM);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strMonYr = string.Empty;
            string strEndDate_DDMM = string.Empty;
            string strEndDate_MMDD = string.Empty;
            string strStartDate_YYMM = string.Empty;
            string strMonthYear = ddlMonthYear.SelectedValue;
            string[] split = null;
            string[] split1 = null;
            string strFormattedStDate = null;
            string strFormattedEndDate = null;
            string strFormattedMonYr = string.Format("{0}/{1}", strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4));

            strMonYr = string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(0, 2), strMonthYear.Substring(2, 4)); //strMonYr
            strEndDate_DDMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))), strMonthYear.Substring(2, 4));
            strEndDate_MMDD = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), DateTime.DaysInMonth(Convert.ToInt32(strMonthYear.Substring(2, 4)), Convert.ToInt32(strMonthYear.Substring(0, 2))));
            strStartDate_YYMM = string.Format("{0}/{1}/{2}", strMonthYear.Substring(2, 4), strMonthYear.Substring(0, 2), "01");

            split = strStartDate_YYMM.Split('/'); //strStartDate_YYMM
            strFormattedStDate = string.Format("{0},{1},{2}", split[0], split[1], split[2]);
            split1 = strEndDate_MMDD.Split('/'); //strEndDate_MMDD
            strFormattedEndDate = string.Format("{0},{1},{2}", split1[0], split1[1], split1[2]);

            crStatementOfAccountMailsHO.ReportName = "Impal-report-State-Acc-custA4_Mails";

            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery1 = "select Branch_Code, Customer_Code, Customer_Name, EmailId, CcEmailId, LEFT(UPPER(DATENAME(MONTH, Month_Year)),3) + ' ' + DATENAME(YEAR, Month_Year), FORMAT (Month_Year, 'dd-MM-yyyy') from outstanding_Mails WITH (NOLOCK) Where Emailid is NOT NULL and ltrim(rtrim(Emailid))<>'' and Emailid like '%@%' and Sent_Status IS NULL Order by Branch_Code, Customer_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery1);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    selectionformula = "{general_ledger_detail.Branch_Code}='" + (string)reader[0] + "' and {general_ledger_detail.Customer_Code}='" + (string)reader[1] + "' " +
                        "and {general_ledger_detail.document_date} >= Date(" + strFormattedStDate + ") " +
                        "and {general_ledger_detail.document_date} <= Date(" + strFormattedEndDate + ") " +
                        "and {outstanding_Mails.Branch_Code}={general_ledger_detail.Branch_Code} and {outstanding_Mails.Customer_Code}={general_ledger_detail.Customer_Code}";
                    crStatementOfAccountMailsHO.RecordSelectionFormula = selectionformula;
                    crStatementOfAccountMailsHO.GenerateReportAndExportA4HOMails(fileType, (string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6]);
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Emails have been sent Successfully.');", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("StatementOfAccountMailsHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}