#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.Sales;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion 

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class CustomerLineInvoiceSales : System.Web.UI.Page
    {
        private string strCustomerCode = default(string);
        private string status = default(string);
        private string strBranchCode = default(string);

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
                    if (crCustLineInvSales != null)
                    {
                        crCustLineInvSales.Dispose();
                        crCustLineInvSales = null;
                    }

                    fnPopulateFromLine();
                    fnPopulateToLine();
                    fnPopulateReportType();
                    fnPopulateCustomer();
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    CustomerInfo.Visible = false;
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
            if (crCustLineInvSales != null)
            {
                crCustLineInvSales.Dispose();
                crCustLineInvSales = null;
            }
        }
        protected void crCustLineInvSales_Unload(object sender, EventArgs e)
        {
            if (crCustLineInvSales != null)
            {
                crCustLineInvSales.Dispose();
                crCustLineInvSales = null;
            }
        }

        #region Populate FromLine
        /// <summary>
        /// To populate From Line dropdown
        /// </summary>
        protected void fnPopulateFromLine()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Suppliers supp = new IMPALLibrary.Suppliers();
                List<Supplier> lstsupplier = new List<Supplier>();
                lstsupplier = supp.GetAllSuppliers();
                ddlFromLine.DataSource = lstsupplier;
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

        #region Populate To line
        /// <summary>
        /// To Populate To line
        /// </summary>
        protected void fnPopulateToLine()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Suppliers supp1 = new IMPALLibrary.Suppliers();
                List<Supplier> lstsupplier = new List<Supplier>();
                lstsupplier = supp1.GetAllSuppliers();
                ddlToLine.DataSource = lstsupplier;
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

        #region Populate Report Type
        /// <summary>
        /// To Populate Report Type
        /// </summary>
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("CustSalesReportType");
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

        #region Populate Customer
        /// <summary>
        /// To Populate Customer Name
        /// </summary>
        protected void fnPopulateCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.Sales.SalesOrderHeaders sales = new IMPALLibrary.Masters.Sales.SalesOrderHeaders();
                List<SalesOrderHeader> lstCustomer = new List<SalesOrderHeader>();
                lstCustomer = sales.GetSalesOrder(strBranchCode);
                cboCustomer.DataSource = lstCustomer;
                cboCustomer.DataTextField = "customer_name";
                cboCustomer.DataValueField = "customer_code";
                cboCustomer.DataBind();
                cboCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Customer Info
        /// <summary>
        /// To Populate Customer Information in Table from Db
        /// </summary>
        /// <param name="strCustomerCode"></param>
        public void fnPopulateCustInfo(string strBranchCode, string strCustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                if (!string.IsNullOrEmpty(strCustomerCode))
                {
                    CustomerInfo.Visible = true;
                    string sSQL = default(string);
                    sSQL = "select status ,address1,address2,address3,address4,location from customer_master WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and customer_code ='" + strCustomerCode + "'";
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
                    CustomerInfo.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Customer Onselected Index Change
        protected void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fnPopulateCustomer();
            fnPopulateCustInfo(strBranchCode, cboCustomer.SelectedValue);

        }
        #endregion

        #region Generate Selection Formula
        public void GenerateAndExportReport(string fileType)
        {
            #region Declaration
            string strFromDate = default(string);
            string strToDate = default(string);
            string strFromLineCode = default(string);
            string strToLineCode = default(string);
            string strCustomer = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strItemCodeField = default(string);
            string strBranchCodeField = default(string);
            string strDateField = default(string);
            string strCustomerCodeField = default(string);
            string strSelectionFormula = default(string);
            strItemCodeField = "mid({V_SalesReports.item_code},1,3)";
            strBranchCodeField = "{V_SalesReports.branch_code}";
            strDateField = "{V_SalesReports.Document_date}";
            strCustomerCodeField = "{V_SalesReports.Customer_Code}";
            #endregion

            #region Selection Formula Formation
            strFromLineCode = ddlFromLine.Text;
            strToLineCode = ddlToLine.Text;
            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            if (cboCustomer.Text != "0")
                strCustomer = cboCustomer.Text;
            else
                strCustomer = string.Empty;

            if (!string.IsNullOrEmpty(strFromDate))
                strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            if (!string.IsNullOrEmpty(strToDate))
                strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (ddlReportType.SelectedIndex == 0)//if Report is Detail Report
            {
                if (string.IsNullOrEmpty(strCustomer) || strFromLineCode == "0")//FromLine or Customer Field is null
                {
                    if (strBranchCode != "CRP")
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + "='" + strBranchCode + "'";
                    else
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;

                }
                if (string.IsNullOrEmpty(strCustomer) && strFromLineCode != "0")//Customer Field is null and FromLine not null
                {
                    if (strBranchCode != "CRP")
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + "='" + strBranchCode + "'" +
                                            " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'";
                    else
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate +
                                             " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'";

                }

                if (strFromLineCode != "0" && !string.IsNullOrEmpty(strCustomer))//CustomerField and FromLine is not null
                {
                    if (strBranchCode != "CRP")
                        if (strCustomer == "0")
                        {
                            strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + "='" + strBranchCode + "'" +
                                                 " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'";

                        }
                        else
                        {
                            strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strBranchCodeField + "='" + strBranchCode + "'" +
                                                 " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'" +
                                                 " and " + strCustomerCodeField + "='" + strCustomer + "'";
                        }
                    else
                        if (strCustomer == "0")
                    {
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'";

                    }
                    else
                    {
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate + " and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "'" +
                                         " and " + strCustomerCodeField + "='" + strCustomer + "'";
                    }
                }

                if (strFromLineCode == "0" && !string.IsNullOrEmpty(strCustomer))//FromLine is null and CustomerField is not null
                {
                    if (strBranchCode != "CRP")
                        if (strCustomer == "0")
                        {
                            strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate +
                                                 " and " + strBranchCodeField + "='" + strBranchCode + "'";
                        }
                        else
                        {
                            strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate +
                                                 " and " + strBranchCodeField + "='" + strBranchCode + "'" + " and " + strCustomerCodeField + "='" + strCustomer + "'";
                        }
                    else
                        if (strCustomer == "0")
                    {
                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate;
                    }
                    else
                    {

                        strSelectionFormula = strDateField + ">=" + strCryFromDate + " and " + strDateField + "<=" + strCryToDate +
                                             " and " + strCustomerCodeField + "='" + strCustomer + "'";

                    }
                }

                crCustLineInvSales.ReportName = "CustLineInvSales";
            }

            if (ddlReportType.SelectedIndex == 1 || ddlReportType.SelectedIndex == 2) //if Report is Customer Summary or Line/CustomerSummary
            {
                int intPrevFromYear = default(int);
                int intPrevToYear = default(int);
                int intIsLeapYear = default(int);
                int intIsToDateDay = default(int);
                string strPrevDay = default(string);
                string strPrevFromDay = default(string);
                string strPrevToDay = default(string);

                intPrevFromYear = Convert.ToInt32(strFromDate.Substring(6)) - 1;
                intPrevToYear = Convert.ToInt32(strToDate.Substring(6)) - 1;

                intIsLeapYear = intPrevFromYear % 4;
                string strDateDay = strToDate.Substring(0, 2);
                intIsToDateDay = Convert.ToInt32(strToDate.Substring(0, 2));

                if (intIsLeapYear > 0 && intIsToDateDay == 29)
                {
                    strPrevDay = Convert.ToString(intIsToDateDay - 1);
                }
                else
                {
                    strPrevDay = intIsToDateDay.ToString();
                    if (intIsToDateDay <= 9)
                        strPrevDay = "0" + strPrevDay;
                }

                strPrevFromDay = "Date (" + Convert.ToString(intPrevFromYear) + "," + strFromDate.Split(new char[] { '/' })[1] + "," + strFromDate.Split(new char[] { '/' })[0] + ")";

                if (intIsLeapYear == 0)
                {
                    strPrevToDay = "Date (" + Convert.ToString(intPrevToYear) + "," + strToDate.Split(new char[] { '/' })[1] + "," + strToDate.Split(new char[] { '/' })[0] + ")";
                }
                else
                {
                    strPrevToDay = "Date (" + Convert.ToString(intPrevToYear) + "," + strToDate.Split(new char[] { '/' })[1] + "," + strPrevDay + ")";
                }

                if (strBranchCode != "CRP")
                {
                    if (strFromLineCode == "0" && strToLineCode == "0" && string.IsNullOrEmpty(strCustomer))//FromLine, Toline & CustomerField are null
                        strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode == "0" && !string.IsNullOrEmpty(strCustomer) && strToLineCode == "0")
                        if (strCustomer == "0")
                        {
                            strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";
                        }
                        else
                        {
                            strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strCustomerCodeField + " = '" + strCustomer + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";
                        }

                    else if (strFromLineCode != "0" && string.IsNullOrEmpty(strCustomer) && strToLineCode == "0")
                        strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strItemCodeField + " = '" + strFromLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode == "0" && string.IsNullOrEmpty(strCustomer) && strToLineCode != "0")
                        strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strItemCodeField + " = '" + strToLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode != "0" && strToLineCode != "0" && string.IsNullOrEmpty(strCustomer))
                        strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode != "0" && strToLineCode != "0" && !string.IsNullOrEmpty(strCustomer))
                        strSelectionFormula = "(" + strBranchCodeField + "= '" + strBranchCode + "' and " + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "' and " + strCustomerCodeField + "= '" + strCustomer + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";
                }

                else
                {
                    if (strFromLineCode == "0" && strToLineCode == "0" && string.IsNullOrEmpty(strCustomer))
                        strSelectionFormula = "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")";

                    else if (strFromLineCode == "0" && strToLineCode == "0" && !string.IsNullOrEmpty(strCustomer))
                        if (strCustomer == "0")
                        {
                            strSelectionFormula = "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")";
                        }
                        else
                        {
                            strSelectionFormula = "(" + strCustomerCodeField + " = '" + strCustomer + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";
                        }

                    else if (strFromLineCode != "0" && string.IsNullOrEmpty(strCustomer) && strToLineCode == "0")
                        strSelectionFormula = "(" + strItemCodeField + " = '" + strFromLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode == "0" && string.IsNullOrEmpty(strCustomer) && strToLineCode != "0")
                        strSelectionFormula = "(" + strItemCodeField + " = '" + strToLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode != "0" && strToLineCode != "0" && string.IsNullOrEmpty(strCustomer))
                        strSelectionFormula = "(" + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";

                    else if (strFromLineCode != "0" && strToLineCode != "0" && !string.IsNullOrEmpty(strCustomer))
                        strSelectionFormula = "(" + strItemCodeField + " in '" + strFromLineCode + "'" + " to '" + strToLineCode + "' and " + strCustomerCodeField + "= '" + strCustomer + "' and " + "(" + "(" + strDateField + " >= " + strPrevFromDay + " and " + strDateField + " <= " + strPrevToDay + ")" + "  OR " + "(" + strDateField + " >= " + strCryFromDate + " and " + strDateField + " <= " + strCryToDate + ")" + ")" + ")";
                }

                if (ddlReportType.SelectedIndex == 1)
                {
                    crCustLineInvSales.ReportName = "CustLineInvSalesSummary";
                }
                else
                {
                    crCustLineInvSales.ReportName = "CustLineInvSalesLineSummary";
                }
            }
            #endregion

            crCustLineInvSales.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crCustLineInvSales.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crCustLineInvSales.RecordSelectionFormula = strSelectionFormula;
            crCustLineInvSales.GenerateReportAndExport(fileType);
        }
        #endregion

        #region Button Click

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
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CustomerLineInvoiceSales.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }  
}








               
                   







                                               
                    

                      












                    
                