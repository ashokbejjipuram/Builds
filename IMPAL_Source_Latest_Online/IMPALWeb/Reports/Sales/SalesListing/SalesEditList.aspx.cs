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
    public partial class SalesEditList : System.Web.UI.Page
    {
        private string strCustomerCode = default(string);
        private string strBranchCode = default(string);
        private string status = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    strBranchCode = Session["BranchCode"].ToString();

                    if (crSalesEditList != null)
                    {
                        crSalesEditList.Dispose();
                        crSalesEditList = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    CustInfo.Visible = false;
                    PopulateCustomer();
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
            if (crSalesEditList != null)
            {
                crSalesEditList.Dispose();
                crSalesEditList = null;
            }
        }
        protected void crSalesEditList_Unload(object sender, EventArgs e)
        {
            if (crSalesEditList != null)
            {
                crSalesEditList.Dispose();
                crSalesEditList = null;
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
                //lstCustomer.Insert(0, new IMPALLibrary.Masters.Sales.SalesOrderHeader("", "", ""));
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
            if (cboCustomer.SelectedIndex == 0)
                CustInfo.Visible = false;
            else
            {
                fnPopulateCustInfo(strBranchCode, cboCustomer.SelectedValue);
                CustInfo.Visible = true;
            }
        }

        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                string strCustomer = default(string);
                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;
                strCustomer = cboCustomer.SelectedValue;
                strBranchCode = Session["BranchCode"].ToString();

                if ((strFromDate != "") && (strToDate != ""))
                {
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Usp_SalesEdit_List");
                    ImpalDB.AddInParameter(dbcmd, "@From_Date", DbType.String, strFromDate.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@To_Date", DbType.String, strToDate.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@Branch_code", DbType.String, strBranchCode);
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(dbcmd);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

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
            fnGenerateReport();

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
            string strCustomer = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            strCustomer = cboCustomer.SelectedValue;
            strBranchCode = Session["BranchCode"].ToString();

            string strDocDate = default(string);
            string strCustomerCode = default(string);
            string strBrCode = default(string);
            strDocDate = "{saleseditlist.document_date}";
            strCustomerCode = "{saleseditlist.Customer_Code}";
            strBrCode = "{saleseditlist.Branch_Code}";
            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (strBranchCode == "CRP")
            {
                if (strCustomer != "")
                    strSelectionFormula = strCustomerCode + "=" + "'" + strCustomer + "'" + " and " + strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate;
                else if (strCustomer == "")
                    strSelectionFormula = strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate;
            }
            else if (strBranchCode != "CRP")
            {
                if (cboCustomer.SelectedIndex != 0)
                    strSelectionFormula = strCustomerCode + "=" + "'" + strCustomer + "'" + " and " + strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate + " and " + strBrCode + "=" + "'" + strBranchCode + "'";
                else if (cboCustomer.SelectedIndex == 0)
                    strSelectionFormula = strDocDate + ">=" + strCryFromDate + "and " + strDocDate + "<=" + strCryToDate + " and " + strBrCode + "=" + "'" + strBranchCode + "'";
            }

            crSalesEditList.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSalesEditList.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSalesEditList.RecordSelectionFormula = strSelectionFormula;
            crSalesEditList.GenerateReportAndExport(fileType);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("SalesEditList.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
