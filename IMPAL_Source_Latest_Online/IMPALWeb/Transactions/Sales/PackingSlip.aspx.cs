using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;
using System.Configuration;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class PackingSlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DefineBranch();
                    ddlSalesInvoiceNumber.Visible = false;
                    txtSalesInvoiceNumber.Visible = true;
                    ddlSalesInvoiceNumber.SelectedIndex = -1;
                }

                imgEditToggle.Attributes.Add("OnClick", "return ValidationEdit(1);");
                BtnReport.Attributes.Add("OnClick", "return ValidationEdit(2);");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSalesInvoiceNumber(ddlBranch.SelectedValue, txtSalesInvoiceNumber.Text);

                if (ddlSalesInvoiceNumber.Items.Count > 1)
                {
                    LoadTransactionType();
                    LoadCustomers();
                    LoadSalesMan();
                    LoadCashDiscount();

                    ddlSalesInvoiceNumber.SelectedIndex = -1;
                    ddlSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Visible = false;
                    imgEditToggle.Visible = false;
                }
                else
                {
                    ddlSalesInvoiceNumber.SelectedIndex = -1;
                    ddlSalesInvoiceNumber.Visible = false;
                    txtSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Text = "";
                    imgEditToggle.Visible = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number does not exists.');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                txtLRNumber.Enabled = false;
                txtLRDate.Enabled = false;
                txtCarrier.Enabled = false;
                txtMarkingNumber.Enabled = false;
                txtWeight.Enabled = false;
                txtNoOfCases.Enabled = false;

                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmdtemps = ImpalDB.GetStoredProcCommand("usp_GetSalesInvoice_PackingSlip");
                ImpalDB.AddInParameter(cmdtemps, "@Branch_Code", DbType.String, Session["BranchCode"].ToString());
                ImpalDB.AddInParameter(cmdtemps, "@Document_Number", DbType.String, ddlSalesInvoiceNumber.SelectedValue.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@LR_Number", DbType.String, txtLRNumber.Text.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@LR_Date", DbType.String, txtLRDate.Text.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@Carrier", DbType.String, txtCarrier.Text.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@Marking_Number", DbType.String, txtMarkingNumber.Text.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@Weight", DbType.String, txtWeight.Text.ToString());
                ImpalDB.AddInParameter(cmdtemps, "@NoOfCases", DbType.String, txtNoOfCases.Text.ToString());
                cmdtemps.CommandTimeout = ConnectionTimeOut.TimeOut;
                var cnt = ImpalDB.ExecuteScalar(cmdtemps).ToString();

                if (Convert.ToInt32(cnt) >= 1)
                {
                    string strSelectionFormula = default(string);
                    string strInvoiceNumber = default(string);
                    string strBranch = default(string);
                    strBranch = "{v_invoice.Branch_Code}";
                    strInvoiceNumber = "{v_invoice.document_number}";
                    strSelectionFormula = strBranch + "= '" + ddlBranch.SelectedValue + "' and " + strInvoiceNumber + "= '" + ddlSalesInvoiceNumber.SelectedValue + "'";
                    crPackingSlip.ReportName = "PackingSlip";
                    crPackingSlip.RecordSelectionFormula = strSelectionFormula;
                    crPackingSlip.GenerateReportAndExportA4();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Data is not available');", true);
                }                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
            }
        }

        private void LoadCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetAllCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
        }

        private void LoadTransactionType()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<TransactionType> lstTransactionType = new List<TransactionType>();
            lstTransactionType = salesTrans.GetTransactionType();
            ddlTransactionType.DataSource = lstTransactionType;
            ddlTransactionType.DataTextField = "TransactionTypeDesc";
            ddlTransactionType.DataValueField = "TransactionTypeCode";
            ddlTransactionType.DataBind();
        }

        private void LoadSalesInvoiceNumber(string strBranch, string InvoiceNumber)
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            lstSalesEntity = salesTrans.GetPackingSlipInvoiceNumber(strBranch, InvoiceNumber);
            ddlSalesInvoiceNumber.DataSource = lstSalesEntity;
            ddlSalesInvoiceNumber.DataTextField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataValueField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataBind();
        }

        private void LoadCashDiscount()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<CashDiscount> lstCashDiscount = new List<CashDiscount>();
            lstCashDiscount = salesTrans.GetCashDiscount(ddlBranch.SelectedValue);
            ddlCashDiscount.DataSource = lstCashDiscount;
            ddlCashDiscount.DataTextField = "CashDiscountDesc";
            ddlCashDiscount.DataValueField = "CashDiscountCode";
            ddlCashDiscount.DataBind();
        }

        private void LoadSalesMan()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<SalesMan> lstSaleMan = new List<SalesMan>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();
            lstSaleMan = salesTrans.GetBranchSalesMan(strBranch);//Session["BranchCode"].ToString());
            ddlSalesMan.DataSource = lstSaleMan;
            ddlSalesMan.DataTextField = "SalesManName";
            ddlSalesMan.DataValueField = "SalesManCode";
            ddlSalesMan.DataBind();
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("PackingSlip.aspx", false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
            }
        }

        protected void ddlSalesInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSalesInvoiceNumber.SelectedValue != "-- Select --")
                {
                    SalesTransactions salesTrans = new SalesTransactions();
                    List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
                    lstSalesEntity = salesTrans.GetSalesInvoiceHeader(ddlSalesInvoiceNumber.SelectedValue.ToString(), "");
                    if (lstSalesEntity.Count > 0)
                    {
                        for (int i = 0; i <= lstSalesEntity.Count - 1; i++)
                        {
                            ddlTransactionType.SelectedValue = lstSalesEntity[i].TransactionTypeCode;
                            txtSalesInvoiceDate.Text = lstSalesEntity[i].SalesInvoiceDate;
                            ddlCustomerName.SelectedValue = lstSalesEntity[i].CustomerCode;
                            ddlSalesMan.SelectedValue = lstSalesEntity[i].SalesManCode;
                            ddlCashDiscount.Text = lstSalesEntity[i].CashDiscountCode;
                            txtTotalValue.Text = TwoDecimalConversion(lstSalesEntity[i].OrderValue.ToString());
                            txtLRNumber.Text = lstSalesEntity[i].LRNumber;
                            txtLRDate.Text = lstSalesEntity[i].LRDate;
                            txtMarkingNumber.Text = lstSalesEntity[i].MarkingNumber;
                            txtNoOfCases.Text = lstSalesEntity[i].NumberOfCases.ToString();
                            txtWeight.Text = lstSalesEntity[i].Weight;
                            txtCarrier.Text = lstSalesEntity[i].Carrier.ToString();
                        }

                        string strTransValue = ddlTransactionType.SelectedValue.ToString();

                        if (ddlCustomerName.SelectedValue != "")
                        {
                            LoadCustomerDetails();
                            List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                            if (lstCustomers.Count > 0)
                            {
                                for (int i = 0; i < lstCustomers.Count; i++)
                                {
                                    if (lstCustomers[i].Customer_Code == ddlCustomerName.SelectedValue.ToString())
                                    {
                                        txtCustomerCode.Text = ddlCustomerName.SelectedValue.ToString();
                                        txtAddress1.Text = lstCustomers[i].address1.ToString();
                                        txtAddress2.Text = lstCustomers[i].address2.ToString();
                                        txtAddress4.Text = lstCustomers[i].address3.ToString();
                                        txtGSTIN.Text = lstCustomers[i].GSTIN.ToString();
                                        txtLocation.Text = lstCustomers[i].Location.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BtnReset_Click(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PackingSlip), exp);
            }
        }

        private void LoadCustomerDetails()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetCustomerDetails(strBranch, ddlCustomerName.SelectedValue);
            ViewState["CustomerDetails"] = lstCustomers;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private void DefineBranch()
        {
            if (Session["RoleCode"].ToString().ToUpper() == "CORP")
            {
                ddlBranch.SelectedValue = "0";
                ddlBranch.Enabled = true;
            }
            else
            {
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                ddlBranch.Enabled = false;
            }
        }
    }
}