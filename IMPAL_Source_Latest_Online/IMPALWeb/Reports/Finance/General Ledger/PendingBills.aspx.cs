#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class PendingBills : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;       

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Pending Bills Page Init Method");
            try
            {
                if (!IsPostBack)
                    Session.Remove("CrystalReport");
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
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crPendingBills != null)
                    {
                        crPendingBills.Dispose();
                        crPendingBills = null;
                    }

                    divCustomerInfo.Visible = false;
                    fnPopulateCustomer();
                    fnPopulateReportType();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            if (crPendingBills != null)
            {
                crPendingBills.Dispose();
                crPendingBills = null;
            }
        }
        protected void crPendingBills_Unload(object sender, EventArgs e)
        {
            if (crPendingBills != null)
            {
                crPendingBills.Dispose();
                crPendingBills = null;
            }
        }

        #region Function to Populate from and to customer
        public void fnPopulateCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateCustomer()", "Pending Bills Customer Details Populate Method");
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                if (strBranchCode != "CRP")
                {
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                }
                else
                {
                    lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "Corporate");
                }

                cboFromCustomer.DataSource = lstCompletion;
                cboFromCustomer.DataTextField = "Name";
                cboFromCustomer.DataValueField = "Code";
                cboFromCustomer.DataBind();
                cboToCustomer.DataSource = lstCompletion;
                cboToCustomer.DataTextField = "Name";
                cboToCustomer.DataValueField = "Code";
                cboToCustomer.DataBind();
                cboFromCustomer.SelectedIndex = 0;
                cboToCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report type
        public void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnPopulateReportType()", "Pending Bills Report type Populate Method");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("PendingBills");
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

        #region Generate report
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
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            if (ddlReportType.SelectedValue == "Weekly Plan")
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                try
                {
                    DataSet ds = new DataSet();

                    string str_head = "";
                    btnReport.Text = "Back";
                    string filename = "Pending_Doc_Weekly_Plan_" + string.Format("{0:yyyyMMdd}", txtDate.Text) + ".xls";

                    SalesTransactions salesItem = new SalesTransactions();

                    ds = salesItem.GetPendingDocsWeeklyPlanDetails(Session["BranchCode"].ToString(), txtDate.Text, cboFromCustomer.SelectedValue, cboToCustomer.SelectedValue);
                    string strBranchName = (string)Session["BranchName"];
                    str_head = "<center><b><font size='6'>Pending Documents - Weekly Plan Report for " + txtDate.Text + " of " + strBranchName + "</font></b></center></font>";

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    Response.ContentType = "application/ms-excel";
                    Response.Write(str_head);
                    Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr>");

                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                    }
                    Response.Write("</tr>");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Response.Write("<tr>");
                        DataRow row = ds.Tables[0].Rows[i];
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {

                            Response.Write("<td>" + row[j] + "</td>");
                        }
                        Response.Write("</tr>");
                    }

                    Response.Write("</table>");
                }
                catch (Exception exp)
                {
                    IMPALLibrary.Log.WriteException(Source, exp);
                }
                finally
                {
                    Response.Flush();
                    Response.End();
                    Response.Close();
                }
            }
            else
            {
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromCustomer = cboFromCustomer.SelectedValue;
                string strToCustomer = cboToCustomer.SelectedValue;
                string strDate = txtDate.Text;
                if (ddlReportType.SelectedValue == "Debit Report" || ddlReportType.SelectedValue == "Debit/Credit Report")
                {
                    if (ddlReportType.SelectedValue == "Debit Report")
                        crPendingBills.ReportName = "impal-report-state-acc-pending";
                    else
                        crPendingBills.ReportName = "Impal-report-State-Acc-pending_both";

                    SelectionConfirmBal(strFromCustomer, strToCustomer, strDate);
                    crPendingBills.GenerateReportAndExportA4HO(fileType);
                }
                else if (ddlReportType.SelectedValue == "Pending Documents")
                {
                    crPendingBills.ReportName = "pending_documents1";
                    crPendingBills.GenerateReportAndExportA4HO(fileType);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Selection formula for DebitReport and Debit/CreditReport
        public void SelectionConfirmBal(string strFromCustomer,string strToCustomer,string strDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strselDocdate = "{general_ledger_detail.document_date}";
                string strselCusCode = "{outstanding.customer_code}";
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
              
                if (strFromCustomer == "0")
                { strFromCustomer = string.Empty; }
                if (strToCustomer == "0")
                { strToCustomer = string.Empty; }

                string[] strSplitdate = strDate.Split('/');
                string Dfrom_Param = string.Format("Date( {0},{1},{2})", strSplitdate[2], strSplitdate[1], strSplitdate[0]);
                string strDate2 = strSplitdate[1] + "/" + strSplitdate[0] + "/" + strSplitdate[2];

                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_calcustos1");
                ImpalDB.AddInParameter(cmd, "@month_year", DbType.String, strDate2);
                ImpalDB.AddInParameter(cmd, "@from_Cus", DbType.String, strFromCustomer.Trim());
                ImpalDB.AddInParameter(cmd, "@To_Cus", DbType.String, strToCustomer.Trim());
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);

                if (!string.IsNullOrEmpty(strDate) && string.IsNullOrEmpty(strFromCustomer) && string.IsNullOrEmpty(strToCustomer))
                {
                    crPendingBills.RecordSelectionFormula = strselDocdate + "<=" + Dfrom_Param;
                   
                }
                else if (!string.IsNullOrEmpty(strDate) && !string.IsNullOrEmpty(strFromCustomer) && !string.IsNullOrEmpty(strToCustomer))
                {
                    crPendingBills.RecordSelectionFormula = strselCusCode + " in '" + strFromCustomer + "' to '" + strToCustomer + "' and " + strselDocdate + "<=" + Dfrom_Param;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

       #endregion 

        #region From Customer and To Customer Dropdown Changed Event
        protected void ddlChangedEvent(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList DrpDwn = (DropDownList)sender;
                string DrpDwnID = DrpDwn.ID;

                if (DrpDwn.SelectedValue == string.Empty || DrpDwn.SelectedValue == null || DrpDwn.SelectedValue == "")
                {
                    DrpDwn.SelectedValue = "0";
                }

                if (DrpDwn.SelectedValue != "0")
                {
                    if (DrpDwn.ID == "cboFromCustomer")
                    {
                        GetCustomerDetails(DrpDwn.SelectedValue, "From");
                        divCustomerInfo.Visible = true;
                    }
                    else if (DrpDwn.ID == "cboToCustomer")
                    {
                        GetCustomerDetails(DrpDwn.SelectedValue, "To");
                        divCustomerInfo.Visible = true;
                    }
                }

                else if ( DrpDwn.SelectedValue == "0")
                {
                    if (DrpDwn.ID == "cboFromCustomer")
                    {
                        if (cboToCustomer.SelectedValue == "0")
                        {
                            divCustomerInfo.Visible = false;
                        }
                        else
                        {
                            GetCustomerDetails(cboToCustomer.SelectedValue, "To");
                            divCustomerInfo.Visible = true;
                        }

                    }
                    else
                    {
                        if (cboFromCustomer.SelectedValue == "0")
                        {
                            divCustomerInfo.Visible = false;
                        }
                        else
                        {
                            GetCustomerDetails(cboFromCustomer.SelectedValue, "From");
                            divCustomerInfo.Visible = true;
                        }
                    }
                }
                else
                {
                    divCustomerInfo.Visible = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Get Customer Details Method
        private void GetCustomerDetails(string CustomerCode,string type)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                if (type == "From")
                {
                    txtCustomerCode.Text = cboFromCustomer.SelectedValue;
                }
                else
                {
                    txtCustomerCode.Text = cboToCustomer.SelectedValue;
                }
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("PendingBills.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}