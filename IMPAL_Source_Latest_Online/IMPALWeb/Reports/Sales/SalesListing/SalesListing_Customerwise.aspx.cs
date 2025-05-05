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

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesListing_Customer_wise_ : System.Web.UI.Page
    {
        private string strCustomerCode = default(string);
        string strBranchCode = default(string);
        private string status = default(string);

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
                    if (crSalesListing_Custwise != null)
                    {
                        crSalesListing_Custwise.Dispose();
                        crSalesListing_Custwise = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    CustInfo.Visible = false;
                    PopulateCustomer();
                    PopulateFromLine();
                    PopulateToLine();
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
            if (crSalesListing_Custwise != null)
            {
                crSalesListing_Custwise.Dispose();
                crSalesListing_Custwise = null;
            }
        }
        protected void crSalesListing_Custwise_Unload(object sender, EventArgs e)
        {
            if (crSalesListing_Custwise != null)
            {
                crSalesListing_Custwise.Dispose();
                crSalesListing_Custwise = null;
            }
        }

        #region populate Customer
        public void PopulateCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Sales.SalesOrderHeaders salesOrder = new IMPALLibrary.Masters.Sales.SalesOrderHeaders();
                List<IMPALLibrary.Masters.Sales.SalesOrderHeader> lstCustomer = new List<IMPALLibrary.Masters.Sales.SalesOrderHeader>();
                lstCustomer = salesOrder.GetSalesOrder(strBranchCode);
                //lstCustomer.Insert(0, new IMPALLibrary.Masters.Sales.SalesOrderHeader("","",""));
                cboCustomer.DataSource = lstCustomer;
                cboCustomer.DataTextField = "Customer_Name";
                cboCustomer.DataValueField = "Customer_code";
                cboCustomer.DataBind();
                cboCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate From Line Code
        public void PopulateFromLine()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Suppliers supp = new IMPALLibrary.Suppliers();
                List<IMPALLibrary.Supplier> lstSupplier = new List<IMPALLibrary.Supplier>();
                lstSupplier = supp.GetAllSuppliers();
                lstSupplier.RemoveAt(0);
                lstSupplier.Insert(0, new IMPALLibrary.Supplier(""));
                ddlFromLine.DataSource = lstSupplier;
                ddlFromLine.DataTextField = "SupplierName";
                ddlFromLine.DataValueField = "SupplierCode";
                ddlFromLine.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate To Line Code
        public void PopulateToLine()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Suppliers supp = new IMPALLibrary.Suppliers();
                List<IMPALLibrary.Supplier> lstSupplier = new List<IMPALLibrary.Supplier>();
                lstSupplier = supp.GetAllSuppliers();
                //lstSupplier.Insert(0, new IMPALLibrary.Supplier(""));
                ddlToLine.DataSource = lstSupplier;
                ddlToLine.DataTextField = "SupplierName";
                ddlToLine.DataValueField = "SupplierCode";
                ddlToLine.DataBind();
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
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                if (!string.IsNullOrEmpty(strCustomerCode))
                {
                    CustInfo.Visible = true;
                    string sSQL = default(string);
                    sSQL = "select status,ISNULL(address1,''),ISNULL(address2,''),ISNULL(address3,''),ISNULL(address4,''),ISNULL(location,'') from customer_master WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and customer_code ='" + strCustomerCode + "'";
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
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

        protected void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PopulateCustomer();
            fnPopulateCustInfo(strBranchCode, cboCustomer.SelectedValue);
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
            PanelHeaderDtls.Enabled = false;

            //Database ImpalDB = DataAccess.GetDatabase();
            //DbCommand cmd = null;
            //int timediff = 0;

            //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = 'SalesListing_Custwise' order by datestamp desc");
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
                reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), "SalesListing_Custwise");
            //}

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
            string strFromLine = default(string);
            string strToLine = default(string);
            string strCustomer = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            strFromLine = ddlFromLine.SelectedValue;
            strToLine = ddlToLine.SelectedValue;
            strCustomer = cboCustomer.SelectedValue;

            if (strFromLine == "0")
                strFromLine = "";
            if (strToLine == "0")
                strToLine = "";
            if (strCustomer == "0")
                strCustomer = "";

            string strV_DocDate = default(string);
            string strV_BranchCode = default(string);
            string strV_LineCode = default(string);
            string strV_CustomerCode = default(string);

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            strV_DocDate = "{V_SalesReports.Document_date}";
            strV_BranchCode = "{V_SalesReports.branch_code}";
            strV_LineCode = "{V_SalesReports.Supplier_Code}";
            strV_CustomerCode = "{V_SalesReports.Customer_Code}";

            if ((strFromLine == "") && (strCustomer == ""))
            {
                if (strBranchCode != "CRP")
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'";
                else
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate;
            }

            else if ((strFromLine != "") && (strCustomer == ""))
            {
                if (strBranchCode != "CRP")
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'" + " and " + strV_LineCode + " in '" + strFromLine + "'" + " to '" + strToLine + "'";
                else
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_LineCode + " in '" + strFromLine + "'" + " to '" + strToLine + "'";
            }
            else if ((strFromLine != "") && (strCustomer != ""))
            {
                if (strBranchCode != "CRP")
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'" + " and " + strV_LineCode + " in '" + strFromLine + "'" + " to '" + strToLine + "'" + " and " + strV_CustomerCode + "='" + strCustomer + "'";
                else
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_LineCode + " in '" + strFromLine + "'" + " to '" + strToLine + "'" + " and " + strV_CustomerCode + "='" + strCustomer + "'";
            }
            else if ((strFromLine == "") && (strCustomer != ""))
            {
                if (strBranchCode != "CRP")
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'" + " and " + strV_CustomerCode + "='" + strCustomer + "'";
                else
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_CustomerCode + "='" + strCustomer + "'";
            }

            crSalesListing_Custwise.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSalesListing_Custwise.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSalesListing_Custwise.RecordSelectionFormula = strSelectionFormula;
            crSalesListing_Custwise.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void ddlFromLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromLine.SelectedIndex != 0)
                astToLine.Visible = true;
            else
                astToLine.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("SalesListing_Customerwise.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}