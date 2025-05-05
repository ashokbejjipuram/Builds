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
    public partial class CollectionAging : System.Web.UI.Page
    {
        //private string strCustomerCode = default(string);
        string strBranchCode = default(string);
        private string status = default(string);

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
                    if (crCollectionAging_Report != null)
                    {
                        crCollectionAging_Report.Dispose();
                        crCollectionAging_Report = null;
                    }

                    CustInfo.Visible = false;
                    //txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.Text = strBranchCode;
                    if (strBranchCode != "CRP")
                        ddlBranch.Enabled = false;
                    else
                        ddlBranch.Enabled = true;
                    fnPopulateBranch();
                    fnPopulateFromCustomer();
                    fnPopulateToCustomer();
                    fnPopulateReportType();
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
            if (crCollectionAging_Report != null)
            {
                crCollectionAging_Report.Dispose();
                crCollectionAging_Report = null;
            }
        }
        protected void crCollectionAging_Report_Unload(object sender, EventArgs e)
        {
            if (crCollectionAging_Report != null)
            {
                crCollectionAging_Report.Dispose();
                crCollectionAging_Report = null;
            }
        }

        #region populate Branch
        public void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateBranch", "Inside fnPopulateBranch");
            try
            {
                IMPALLibrary.Branches brnch = new IMPALLibrary.Branches();
                ddlBranch.DataSource = brnch.GetCorpBranch();
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate From Customer
        public void fnPopulateFromCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateFromCustomer", "Inside fnPopulateFromCustomer");
            try
            {
                string queryType = "Corporate";
                string strDefault = null;
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails cust = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                if (ddlBranch.SelectedValue == "CRP")
                {
                    ddlFromCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, queryType);
                }
                else
                {
                    ddlFromCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, strDefault);
                }
                ddlFromCustomer.DataTextField = "Name";
                ddlFromCustomer.DataValueField = "Code";
                ddlFromCustomer.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate To Customer
        public void fnPopulateToCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateToCustomer", "Inside fnPopulateToCustomer");
            try
            {
                string queryType = "Corporate";
                string strDefault = null;
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails cust = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                if (ddlBranch.SelectedValue == "CRP")
                {
                    ddlToCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, queryType);
                }
                else
                {
                    ddlToCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, strDefault);
                }
                ddlToCustomer.DataTextField = "Name";
                ddlToCustomer.DataValueField = "Code";
                ddlToCustomer.DataBind();
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
                oList = oCommon.GetDropDownListValues("ReportType-CollectionAging");
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

        #region Populate Customer Information
        public void fnPopulateCustInfo(string strBranchCode, string strCustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateCustInfo", "Inside fnPopulateCustInfo");
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                if (!string.IsNullOrEmpty(strCustomerCode))
                {
                    CustInfo.Visible = true;
                    string sSQL = default(string);
                    sSQL = "select status ,address1,address2,address3,address4,location from customer_master WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and customer_code ='" + strCustomerCode + "'";
                    DbCommand cmdP = ImpalDB.GetSqlStringCommand(sSQL);
                    cmdP.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmdP))
                    {
                        while (reader.Read())
                        {
                            status = (string)reader[0];
                            txtAddress1.Text = (string)reader[1];
                            txtAddress2.Text = (string)reader[2];
                            txtAddress3.Text = (string)reader[3];
                            txtAddress4.Text = (string)reader[4];
                            txtLocation.Text = (string)reader[5];

                        }
                        txtCustCode.Text = strCustomerCode;
                    }
                }
                else
                    CustInfo.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlFromCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            astBranch.Visible = false;
            if (ddlFromCustomer.SelectedIndex == 0 && ddlToCustomer.SelectedIndex != 0)
            {
                CustInfo.Visible = true;
                fnPopulateCustInfo(strBranchCode, ddlToCustomer.SelectedValue);
                astFromCust.Visible = false;
                astToCust.Visible = false;
                astDate.Visible = true;
            }
            else if (ddlFromCustomer.SelectedIndex == 0)
            {
                astFromCust.Visible = false;
                astToCust.Visible = false;
                astDate.Visible = false;
                CustInfo.Visible = false;
                ddlToCustomer.SelectedIndex = 0;
                txtDate.Text = "";
                ddlReportType.SelectedIndex = 0;
            }
            else
            {
                astFromCust.Visible = true;
                astToCust.Visible = true;
                astDate.Visible = true;
                CustInfo.Visible = true;
                //fnPopulateFromCustomer();
                fnPopulateCustInfo(strBranchCode, ddlFromCustomer.SelectedValue);
            }

            //fnPopulateFromCustomer();
            //fnPopulateCustInfo(ddlFromCustomer.SelectedValue);
        }

        protected void ddlToCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fnPopulateToCustomer();
            if (ddlToCustomer.SelectedIndex == 0 && ddlFromCustomer.SelectedIndex != 0)
                fnPopulateCustInfo(strBranchCode, ddlFromCustomer.SelectedValue);
            else if (ddlToCustomer.SelectedIndex == 0)
            {
                CustInfo.Visible = false;
                txtDate.Text = "";
            }
            else
                fnPopulateCustInfo(strBranchCode, ddlToCustomer.SelectedValue);
        }

        #region Generate Button Click

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
            fnGenerateSelectionFormula();

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        protected void GenerateAndExportReport(string fileType)
        {
            string strCustCode = default(string);
            string strBrCode = default(string);
            string strSelectionFormula = default(string);

            if (ddlReportType.SelectedValue == "Report")
            {
                crCollectionAging_Report.ReportName = "CollectionAging_Report";
                strCustCode = "{collection_aging.customer_code}";
                strBrCode = "{collection_aging.branch_code}";
            }
            else
            {
                crCollectionAging_Report.ReportName = "CollectionAging_Documentwise";
                strCustCode = "{Collection_temp.cust_code}";
                strBrCode = "{Collection_temp.branch_code}";
            }

            strSelectionFormula = strBrCode + " ='" + strBranchCode + "'";

            crCollectionAging_Report.CrystalFormulaFields.Add("AccDesc", "'" + txtDate.Text + "'");
            crCollectionAging_Report.RecordSelectionFormula = strSelectionFormula;
            crCollectionAging_Report.GenerateReportAndExportHO(fileType);
        }

        #region Generate Selection Formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromCustomer = default(string);
                string strToCustomer = default(string);
                string strBranch = default(string);
                string strDate = default(string);

                strDate = txtDate.Text;
                strFromCustomer = ddlFromCustomer.SelectedValue;
                strToCustomer = ddlToCustomer.SelectedValue;
                strBranch = ddlBranch.SelectedValue;

                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_collection_temp");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch);
                ImpalDB.AddInParameter(dbcmd, "@frcustomer", DbType.String, strFromCustomer);
                ImpalDB.AddInParameter(dbcmd, "@tocustomer", DbType.String, strToCustomer);
                ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(dbcmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CollectionAging.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}