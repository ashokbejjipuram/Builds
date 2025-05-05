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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Common;
using System.Web.Services;
using WinSCP;

namespace IMPALWeb
{
    public partial class SalesInvoiceEntry : System.Web.UI.Page
    {
        SalesReport srpt = new SalesReport();
        EinvAuthGen einvGen = new EinvAuthGen();
        SendSMS sendsms = new SendSMS();
        double CanBillUpTo = 0;
        string alertmsg = string.Empty;
        int GroupCompIndication = 0;
        SalesTransactions salesTrans = new SalesTransactions();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (crySalesInvoiceReprint != null)
                    {
                        crySalesInvoiceReprint.Dispose();
                        crySalesInvoiceReprint = null;
                    }

                    hdnScreenMode.Value = "A";
                    ddlSalesInvoiceNumber.Visible = false;
                    BtnReport.Visible = false;
                    txtCourierCharges.Text = "0";
                    txtInsuranceCharges.Text = "0";
                    ddlCustomerName.Enabled = false;
                    ddlSalesReqNumber.Enabled = false;
                    chkActive.Visible = false;
                    hdnCustOSLSStatus.Value = "A";
                    hdnCalamityCess.Value = "A";
                    hdnCostToCostCoupon.Value = "A";
                    ddlCashDiscount.Enabled = false;
                    hdnCDstatus.Value = "0";
                    txtSalesInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    BtnSubmit.Attributes.Add("OnClick", "return SalesInvoiceEntrySubmit();");
                    BtnSubmit.Visible = false;
                    BtnReset.Attributes.Add("OnClick", "return fnReset();");
                    FirstGridViewRow();
                    //ddlCashDiscount.Attributes.Add("OnChange", "return ChangeCashDiscount();");                                
                    LoadTransactionType();
                    ddlVindicator.Enabled = false;
                    LoadShippingAddressStates(Session["BranchCode"].ToString());
                    hdnStateCode.Value = ddlShippingState.SelectedValue;

                    txtAdvReceiptNumber.Attributes.Add("style", "display:none");
                    lblAdvReceiptNumber.Attributes.Add("style", "display:none");
                    txtAdvReceiptDate.Attributes.Add("style", "display:none");
                    lblAdvReceiptDate.Attributes.Add("style", "display:none");
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    BtnReportOs.Enabled = false;
                    ShipAdd1.Visible = false;
                    ShipAdd2.Visible = false;
                }

                if (Session["BranchCode"].ToString().ToUpper() == "MGT")
                    BtnReportOs.Visible = true;
                else
                    BtnReportOs.Visible = false;

                if (Session["BranchCode"].ToString().ToUpper() == "CHE")
                    tdSecondaryDispLoc.Attributes.Add("style", "display:inline");
                else
                    tdSecondaryDispLoc.Attributes.Add("style", "display:none");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }
        protected void crySalesInvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (crySalesInvoiceReprint != null)
            {
                crySalesInvoiceReprint.Dispose();
                crySalesInvoiceReprint = null;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransactionType.SelectedValue.ToString() == "461" && txtRefDocument.Text != "")
                {
                    BindFDOItemDetail(txtRefDocument.Text);
                }
                else
                {
                    AddNewRow();
                }

                DisableOnEditMode();
                upHeader.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSalesReqNumber.Items.Clear();
                ddlCashDiscount.Enabled = false;
                hdnCDstatus.Value = "0";
                ddlCashDiscount.SelectedIndex = 0;

                if (ddlCustomerName.SelectedIndex > 0)
                {
                    LoadCustomerDetails();
                    List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                    if (lstCustomers.Count > 0)
                    {
                        txtCustomerCode.Text = ddlCustomerName.SelectedValue.ToString();
                        txtAddress1.Text = lstCustomers[0].address1.ToString();
                        txtAddress2.Text = lstCustomers[0].address2.ToString();
                        txtAddress4.Text = lstCustomers[0].address4.ToString();
                        txtGSTIN.Text = lstCustomers[0].GSTIN.ToString();
                        hdnCustOSLSStatus.Value = lstCustomers[0].CustOSLSStatus.ToString();
                        hdnCalamityCess.Value = lstCustomers[0].CalamityCess.ToString();
                        hdnCostToCostCoupon.Value = lstCustomers[0].CostToCostCoupon.ToString();
                        txtHdnCustTownCode.Text = lstCustomers[0].Town_Code.ToString();
                        txtLocation.Text = lstCustomers[0].Location.ToString();
                        txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[0].Credit_Limit.ToString());
                        txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[0].Outstanding_Amount.ToString());

                        txtShippingName.Text = ddlCustomerName.SelectedItem.Text;
                        txtShippingAddress1.Text = lstCustomers[0].address1.ToString();
                        txtShippingAddress2.Text = lstCustomers[0].address2.ToString();
                        txtShippingAddress4.Text = lstCustomers[0].address4.ToString();
                        txtShippingGSTIN.Text = lstCustomers[0].GSTIN.ToString();
                        txtShippingLocation.Text = lstCustomers[0].Location.ToString();
                        ddlShippingState.SelectedValue = lstCustomers[0].State_Code.ToString();
                        txtCarrier.Text = lstCustomers[0].Carrier.ToString();
                        ddlFreightIndicator.SelectedValue = lstCustomers[0].Freight_Indicator.ToString();

                        txtCashBillCustomer.Text = lstCustomers[0].Customer_Name.ToString();
                        txtCashBillCustomerTown.Text = lstCustomers[0].Location.ToString();
                        txtPhone.Text = lstCustomers[0].Phone.ToString();

                        if (lstCustomers[0].Sales_Man_Code == "" || lstCustomers[0].Sales_Man_Code == null)
                        {
                            ddlSalesMan.SelectedValue = "0";
                            ddlShippingAddress.Items.Clear();
                            ShipAdd1.Visible = false;
                            ShipAdd2.Visible = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Salesman Need to Map with the Customer');", true);
                            return;
                        }
                        else
                        {
                            try
                            {
                                ddlSalesMan.SelectedValue = lstCustomers[0].Sales_Man_Code;
                            }
                            catch (Exception exp)
                            {
                                ddlSalesMan.SelectedValue = "0";
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Salesman Need to Map with the Customer');", true);
                                return;
                            }                            

                            if (!(ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "071"
                               || ddlTransactionType.SelectedValue.ToString() == "171")) //--- Cash Sales, Cash Sales Manual, Free of Cost, Free of Cost Manual
                            {
                                LoadLimits(ddlBranch.SelectedValue.ToString(), ddlCustomerName.SelectedValue.ToString());

                                if (CanBillUpTo <= 0)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + alertmsg + "');", true);
                                    return;
                                }
                            }
                            else
                                BtnSubmit.Visible = true;

                            if (lblHeaderMessage.Text != "")
                                return;                            

                            if (lstCustomers[0].GSTIN == "" || lstCustomers[0].GSTIN == null)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IMPAL", "alert('GSTIN is Not Available for the Customer');", true);
                                return;
                            }

                            if (!(ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "071"
                                || ddlTransactionType.SelectedValue.ToString() == "171" || ddlTransactionType.SelectedValue.ToString() == "421" || ddlTransactionType.SelectedValue.ToString() == "431"
                                || ddlTransactionType.SelectedValue.ToString() == "441" || ddlTransactionType.SelectedValue.ToString() == "461" || ddlTransactionType.SelectedValue.ToString() == "001"
                                || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "361"))
                            {
                                List<CustomerSalesReqEntity> lstCustSalesEntity = new List<CustomerSalesReqEntity>();
                                lstCustSalesEntity = salesTrans.GetCustomerSalesReqNumber(ddlBranch.SelectedValue, ddlCustomerName.SelectedValue);

                                if (lstCustSalesEntity.Count <= 1)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Please Create Customer Sales Request');", true);
                                    return;
                                }
                                else
                                {
                                    ddlSalesReqNumber.DataSource = lstCustSalesEntity;
                                    ddlSalesReqNumber.DataTextField = "CustomerSalesReqNumber";
                                    ddlSalesReqNumber.DataValueField = "CustomerSalesReqNumVal";
                                    ddlSalesReqNumber.DataBind();
                                }

                                ddlSalesReqNumber.Enabled = true;
                            }
                            else
                            {
                                ddlSalesReqNumber.Enabled = false;
                            }

                            if (ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || lstCustomers[0].CDType.ToString() == "Y") // || Session["BranchCode"].ToString().ToUpper() == "MGT"
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.Trim() == "071" || ddlTransactionType.SelectedValue.Trim() == "171" || ddlTransactionType.SelectedValue.ToString() == "481")
                                    ddlCashDiscount.Enabled = false;
                                else
                                    ddlCashDiscount.Enabled = true;

                                hdnCDstatus.Value = "1";
                                ddlCashDiscount.AutoPostBack = false;
                                //ddlCashDiscount.Attributes.Add("OnChange", "return ChangeCashDiscount();");
                            }
                            else
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.Trim() == "071" || ddlTransactionType.SelectedValue.Trim() == "171" || ddlTransactionType.SelectedValue.ToString() == "481")
                                {
                                    ddlCashDiscount.Enabled = false;
                                    hdnCDstatus.Value = "0";
                                }
                                else
                                {
                                    if (txtCustomerOutStanding.Text != "")
                                    {
                                        if (Convert.ToDecimal(txtCustomerOutStanding.Text.ToString()) < 0)
                                        {
                                            ddlCashDiscount.Enabled = true;
                                            hdnCDstatus.Value = "1";
                                        }
                                        else
                                        {
                                            ddlCashDiscount.Enabled = false;
                                            hdnCDstatus.Value = "0";
                                        }
                                    }
                                }

                                //ddlCashDiscount.AutoPostBack = true;
                                //ddlCashDiscount.Attributes.Remove("OnChange");
                                //ddlCashDiscount.AutoPostBack = false;
                                //ddlCashDiscount.Attributes.Add("OnChange", "return ChangeCashDiscount();");
                            }

                            if (lstCustomers[0].ShippingAddress.Count > 1)
                            {
                                ddlShippingAddress.DataSource = lstCustomers[0].ShippingAddress;
                                ddlShippingAddress.DataTextField = "Address";
                                ddlShippingAddress.DataValueField = "Indicator";
                                ddlShippingAddress.DataBind();

                                ShipAdd1.Visible = true;
                                ShipAdd2.Visible = true;

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Dealer is Having Different Shipping Addresses and You can Select the Same if Needed.');", true);
                            }
                            else
                            {
                                ddlShippingAddress.Items.Clear();
                                ShipAdd1.Visible = false;
                                ShipAdd2.Visible = false;
                            }

                            ddlCashDiscount.SelectedValue = "1";
                        }
                    }
                    else
                    {
                        ddlSalesMan.SelectedIndex = 0;
                        ddlCashDiscount.SelectedIndex = 0;
                        ddlVindicator.SelectedIndex = 0;
                        txtCustomerPONo.Text = "";
                        txtCustomerPODate.Text = "";
                        txtCustomerCode.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtAddress4.Text = "";
                        txtGSTIN.Text = "";
                        hdnCustOSLSStatus.Value = "";
                        hdnCalamityCess.Value = "";
                        hdnCostToCostCoupon.Value = "";
                        txtLocation.Text = "";
                        txtCustomerCreditLimit.Text = "";
                        txtCustomerOutStanding.Text = "";
                        txtCanBillUpTo.Text = "";
                        ddlCashDiscount.Enabled = false;
                        hdnCDstatus.Value = "0";

                        txtShippingName.Text = "";
                        txtShippingAddress1.Text = "";
                        txtShippingAddress2.Text = "";
                        txtShippingAddress4.Text = "";
                        txtShippingGSTIN.Text = "";
                        txtShippingLocation.Text = "";

                        ddlShippingAddress.Items.Clear();
                        ShipAdd1.Visible = false;
                        ShipAdd2.Visible = false;

                        FirstGridViewRow();
                    }
                }
                else
                {
                    ddlSalesMan.SelectedIndex = 0;
                    ddlCashDiscount.SelectedIndex = 0;
                    ddlVindicator.SelectedIndex = 0;
                    txtCustomerPONo.Text = "";
                    txtCustomerPODate.Text = "";
                    txtCustomerCode.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtAddress4.Text = "";
                    txtGSTIN.Text = "";
                    hdnCustOSLSStatus.Value = "";
                    hdnCalamityCess.Value = "";
                    hdnCostToCostCoupon.Value = "";
                    txtLocation.Text = "";
                    txtCustomerCreditLimit.Text = "";
                    txtCustomerOutStanding.Text = "";
                    txtCanBillUpTo.Text = "";
                    ddlCashDiscount.Enabled = false;
                    hdnCDstatus.Value = "0";

                    txtShippingName.Text = "";
                    txtShippingAddress1.Text = "";
                    txtShippingAddress2.Text = "";
                    txtShippingAddress4.Text = "";
                    txtShippingGSTIN.Text = "";
                    txtShippingLocation.Text = "";

                    ddlShippingAddress.Items.Clear();
                    ShipAdd1.Visible = false;
                    ShipAdd2.Visible = false;

                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlShippingAddress_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlShippingAddress.SelectedIndex == 0)
                {
                    txtShippingName.Text = ddlCustomerName.SelectedItem.Text;
                    txtShippingAddress1.Text = txtAddress1.Text;
                    txtShippingAddress2.Text = txtAddress2.Text;
                    txtShippingAddress4.Text = txtAddress4.Text;
                    txtShippingGSTIN.Text = txtGSTIN.Text;
                    txtShippingLocation.Text = txtLocation.Text;
                }
                else
                {
                    string[] ShippingAddress = ddlShippingAddress.SelectedItem.Text.Split(new string[] { " ||| " }, StringSplitOptions.None);

                    txtShippingName.Text = ShippingAddress[0].ToString().Trim();
                    txtShippingAddress1.Text = ShippingAddress[1].ToString().Trim();
                    txtShippingAddress2.Text = ShippingAddress[2].ToString().Trim();
                    txtShippingAddress4.Text = ShippingAddress[3].ToString().Trim();
                    txtShippingGSTIN.Text = ShippingAddress[6].ToString().Trim();
                    txtShippingLocation.Text = ShippingAddress[4].ToString().Trim();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlSalesReqNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlSalesReqNumber.SelectedIndex > 0)
                {
                    List<SalesItem> lstSalesItem = new List<SalesItem>();

                    //string strOsLsTaxFilter = salesTrans.GetOsLsTaxIndicator(Session["BranchCode"].ToString());

                    ////if (strOsLsTaxFilter.ToString() == "OsLsTaxInd" || strOsLsTaxFilter.ToString() == "OsLsInd")
                    ////{
                    ////    PanelOsLsFilter.Attributes.Add("style", "display:inline");
                    ////    lstSalesItem = salesTrans.GetCustomerSalesReqDetails(ddlSalesReqNumber.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlCashDiscount.SelectedItem.Text, "Y", "");
                    ////    ddlOsLsFilter.DataSource = lstSalesItem;                  
                    ////    ddlOsLsFilter.DataTextField = "OSLSIndDesc";
                    ////    ddlOsLsFilter.DataValueField = "OsLsIndicator";                        
                    ////    ddlOsLsFilter.DataBind();
                    ////    ddlOsLsFilter.Items.Insert(0, new ListItem("ALL", "A"));
                    ////}
                    ////else
                    ////    PanelOsLsFilter.Attributes.Add("style", "display:none");

                    ////if (strOsLsTaxFilter.ToString() == "OsLsTaxInd" || strOsLsTaxFilter.ToString() == "TaxInd")

                    //if (strOsLsTaxFilter.ToString() == "TaxInd")
                    //{
                    PanelTaxFilter.Attributes.Add("style", "display:none");
                    lstSalesItem = salesTrans.GetCustomerSalesReqTaxDetails(ddlSalesReqNumber.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlCashDiscount.SelectedItem.Text, "", "Y", Session["BranchCode"].ToString());
                    ddlTaxFilter.DataSource = lstSalesItem;
                    ddlTaxFilter.DataTextField = "SalesTaxPerDesc";
                    ddlTaxFilter.DataValueField = "SalesTaxPercentage";
                    ddlTaxFilter.DataBind();
                    //ddlTaxFilter.Items.Insert(0, new ListItem("ALL", "A"));
                    //}
                    //else
                    //    PanelTaxFilter.Attributes.Add("style", "display:none");

                    LoadGridItems("A", ddlTaxFilter.SelectedValue);

                    //LoadGridItems("A", "A");                    

                    //if (ddlTaxFilter.Items.Count > 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Sales Request has Multiple Tax. Submit the Invoice First, Take Print and Repeat the Same Process for All the Taxes of This Sales Request.');", true);
                    //}
                }
                else
                {
                    ddlCustomerName_OnSelectedIndexChanged(ddlCustomerName, EventArgs.Empty);
                    txtTotalValue.Text = "";
                    PanelOsLsFilter.Attributes.Add("style", "display:none");
                    PanelTaxFilter.Attributes.Add("style", "display:none");
                    FirstGridViewRow();
                    ddlCustomerName.Enabled = true;
                }

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                    btnAdd.Visible = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlOsLsFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadGridItems(ddlOsLsFilter.SelectedValue, ddlTaxFilter.SelectedValue);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlTaxFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadGridItems(ddlOsLsFilter.SelectedValue, ddlTaxFilter.SelectedValue);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadGridItems(string OSLSFilter, string TaxFilter)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                
                List<SalesItem> lstSalesItem = new List<SalesItem>();
                lstSalesItem = salesTrans.GetCustomerSalesReqDetails(ddlSalesReqNumber.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlCashDiscount.SelectedItem.Text, OSLSFilter, TaxFilter, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);

                if (lstSalesItem.Count > 0)
                {
                    grvItemDetails.DataSource = lstSalesItem;
                    grvItemDetails.DataBind();
                    hdnDispLocInd.Value = lstSalesItem[0].DispatchLocationInd;
                    hdnDispLocCnt.Value = lstSalesItem[0].DispatchLocationCount;

                    if (lstSalesItem[0].DispatchLocationInd == "2")
                    {
                        ddlSecondaryDispLoc.SelectedValue = "S";
                        ddlSecondaryDispLoc.Enabled = false;
                    }

                    for (int i = 1; i <= lstSalesItem.Count; i++)
                    {
                        int SLBCode = 0;
                        double BranchListPrice = 0;
                        double dblSLBNetValuePrice = 0;
                        double dblItemSellingPrice = 0;
                        double CouponCharges = 0;

                        TextBox txtSno = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("txtSno");
                        DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                        TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                        DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                        Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                        TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                        TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                        TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                        DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                        TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                        TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtCostPrice");
                        TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                        TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                        TextBox txtOrgCoupon = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtOrgCoupon");
                        HiddenField txtHdnCouponInd = (HiddenField)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtHdnCouponInd");
                        TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                        TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                        HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");
                        HiddenField txtHdnReqOrderQty = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtHdnReqOrderQty");
                        HiddenField txtProductGroupCode = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtProductGroupCode");
                        HiddenField txtAddlTax = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtAddlTax");
                        LinkButton delete = (LinkButton)grvItemDetails.Rows[i - 1].Cells[10].FindControl("lnkDelete");

                        lblSupplier.Text = lstSalesItem[i - 1].SupplierName.ToString();
                        ddlSupplierName.Visible = false;
                        txtSupplierPartNo.Text = lstSalesItem[i - 1].ItemSupplierPartNumber.ToString();
                        txtItemCode.Text = lstSalesItem[i - 1].ItemCode.ToString();
                        txtItemCode.Visible = true;
                        ddlItemCode.Visible = false;
                        btnSearch.Visible = false;
                        ddlSLB.Visible = true;
                        txtSLB.Visible = false;
                        delete.Visible = true;
                        txtSupplierPartNo.Enabled = false;
                        grvItemDetails.Enabled = true;

                        txtHdnReqOrderQty.Value = lstSalesItem[i - 1].OriginalReqQty.ToString();
                        txtQuantity.Text = lstSalesItem[i - 1].Quantity.ToString();
                        txtCanOrderQty.Text = lstSalesItem[i - 1].AvialableQuantity.ToString();
                        txtOSLS.Text = lstSalesItem[i - 1].OsLsIndicator.ToString();

                        txtQuantity.Attributes.Add("OnChange", "SalesInvoiceQtyChangeGST(" + txtSno.Text + ",'" + txtCanOrderQty.ClientID + "','" + txtHdnReqOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + ddlSLB.ClientID + "','" + txtItemSaleValue.ClientID + "','" + txtDiscount.ClientID + "')");

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            txtDiscount.Attributes.Add("OnChange", "SalesInvoiceCouponChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                txtDiscount.Attributes.Add("OnChange", "SalesInvoiceDiscountChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                            else
                                txtDiscount.Attributes.Add("OnChange", "SalesInvoiceCouponChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                        }

                        List<SalesItem> lstSLB = new List<SalesItem>();
                        lstSLB = salesTrans.GetSLBSalesReq(ddlCustomerName.SelectedItem.Value, lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                        ddlSLB.DataSource = lstSLB;
                        ddlSLB.DataTextField = "SlbDesc";
                        ddlSLB.DataValueField = "SlbCode";

                        if (lstSLB.Count <= 2)
                        {
                            ddlSLB.SelectedValue = lstSalesItem[i - 1].SlbCode.ToString();
                            ddlSLB.Enabled = false;
                        }
                        else
                            ddlSLB.Enabled = true;

                        ddlSLB.DataBind();

                        ddlItemCode.Items.Add(new ListItem(lstSalesItem[i - 1].ItemSupplierPartNumber.ToString(), lstSalesItem[i - 1].ItemCode.ToString()));

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            //CouponCharges = salesTrans.GetCouponValue(Session["BranchCode"].ToString(), ddlCustomerName.SelectedItem.Value, ddlSupplierName.SelectedValue.ToString(), txtItemCode.Text, txtQuantity.Text, ddlTransactionType.SelectedValue.ToString());

                            if (txtDiscount.Text.Trim() == "" || txtDiscount.Text.Trim() == "0.00" || txtDiscount.Text.Trim() == "0")
                                txtDiscount.Text = "0.00"; //TwoDecimalConversion(CouponCharges.ToString());

                            //txtDiscount.Text = TwoDecimalConversion(lstSalesItem[i - 1].ItemDiscount.ToString());
                            txtOrgCoupon.Text = TwoDecimalConversion(lstSalesItem[i - 1].ItemDiscount.ToString());
                            txtHdnCouponInd.Value = lstSalesItem[i - 1].CouponIndicator.ToString();

                            if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                            {
                                txtDiscount.Text = "0.00";
                                txtOrgCoupon.Text = "0.00";
                            }

                            if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                            {
                                txtDiscount.Text = txtOrgCoupon.Text;
                                txtDiscount.Enabled = true;
                            }
                            else
                                txtDiscount.Enabled = false;
                        }
                        else
                        {
                            if (!(ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "481"))
                            {
                                //CouponCharges = salesTrans.GetCouponValue(Session["BranchCode"].ToString(), ddlCustomerName.SelectedItem.Value, ddlSupplierName.SelectedValue.ToString(), txtItemCode.Text, txtQuantity.Text, ddlTransactionType.SelectedValue.ToString());

                                if (txtDiscount.Text.Trim() == "" || txtDiscount.Text.Trim() == "0.00" || txtDiscount.Text.Trim() == "0")
                                    txtDiscount.Text = "0.00"; //TwoDecimalConversion(CouponCharges.ToString());

                                //txtDiscount.Text = TwoDecimalConversion(lstSalesItem[i - 1].ItemDiscount.ToString());
                                txtOrgCoupon.Text = TwoDecimalConversion(lstSalesItem[i - 1].ItemDiscount.ToString());
                                txtHdnCouponInd.Value = lstSalesItem[i - 1].CouponIndicator.ToString();

                                if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                                {
                                    txtDiscount.Text = "0.00";
                                    txtOrgCoupon.Text = "0.00";
                                }

                                if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                                {
                                    txtDiscount.Text = txtOrgCoupon.Text;
                                    txtDiscount.Enabled = true;
                                }
                                else
                                    txtDiscount.Enabled = false;
                            }
                            else
                                txtDiscount.Enabled = true;
                        }

                        if (ddlTransactionType.SelectedValue.ToString() == "481")
                        {
                            ddlSLB.SelectedIndex = 0;
                            txtSLBNetValue.Text = "0.00";
                            ddlSLB.Enabled = false;
                            ddlSLB_OnSelectedIndexChanged(ddlSLB, EventArgs.Empty);
                        }
                        else
                        {
                            if (ddlSLB.SelectedValue != "0")
                            {
                                SLBCode = Convert.ToInt32(lstSalesItem[i - 1].SlbCode.ToString());

                                if (i < lstSalesItem.Count)
                                    txtSLB.Text = lstSalesItem[i - 1].SlbDesc.ToString();

                                if (ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "481") //For Distress & Distress Manual;
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[i - 1].CostPrice.ToString());
                                else
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[i - 1].ListPrice.ToString());

                                if (txtBranchListPrice.Text != "")
                                    BranchListPrice = double.Parse(txtBranchListPrice.Text.ToString());

                                txtSLBNetValue.Text = TwoDecimalConversion(lstSalesItem[i - 1].SLBNetValuePrice.ToString());

                                List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                                if (lstCustomers[0].Party_Type_Code.ToUpper() == "DLREXP" || lstSalesItem[i - 1].SalesTaxText.ToLower() == "second sales" || lstSalesItem[i - 1].SalesTaxText.ToLower() == "agricultural implements")
                                {
                                    txtSalesTax.Text = "S";
                                }
                                else
                                {
                                    txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[i - 1].SalesTaxPercentage.ToString());
                                }

                                txtGrossProfit.Text = TwoDecimalConversion(lstSalesItem[i - 1].GrossProfit.ToString());
                                txtCostPrice.Text = TwoDecimalConversion(lstSalesItem[i - 1].CostPrice.ToString());

                                dblSLBNetValuePrice = Convert.ToDouble(TwoDecimalConversion(lstSalesItem[i - 1].SLBNetValuePrice.ToString()));
                                dblItemSellingPrice = Convert.ToDouble(TwoDecimalConversion(lstSalesItem[i - 1].SellingPrice.ToString()));
                                txtItemSaleValue.Value = lstSalesItem[i - 1].SaleValue.ToString();
                                txtProductGroupCode.Value = lstSalesItem[i - 1].ProductGroupCode.ToString();
                            }
                            else
                            {
                                txtSLB.Text = "";
                                txtBranchListPrice.Text = "";
                                txtSLBNetValue.Text = "";
                                txtSalesTax.Text = "";
                                txtAddlTax.Value = "";
                                txtGrossProfit.Text = "";
                                txtCostPrice.Text = "";
                                dblSLBNetValuePrice = 0;
                                dblItemSellingPrice = 0;
                                txtItemSaleValue.Value = "0";
                                txtProductGroupCode.Value = "0";
                            }
                        }
                    }

                    if (Convert.ToInt16(hdnDispLocCnt.Value) > 1)
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Request # is also having GP Road dispatching. Please Check and Raise a new Invoice for the same after this Billing.');", true);

                    List<Customer> lstCustomers1 = (List<Customer>)ViewState["CustomerDetails"];

                    if (lstCustomers1[0].Party_Type_Code.ToUpper() == "DLREXP")
                    {
                        lblMessage1.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>Export Customer</b></font></span>";
                        PanelTaxFilter.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        PanelTaxFilter.Attributes.Add("style", "display:inline");
                        if (hdnCustOSLSStatus.Value == "L")
                        {
                            if (hdnCalamityCess.Value == "Y")
                                lblMessage1.Text = "<font color='red' size='6'><b>CALAMITY CESS/SGST/UTGST/CGST Customer</b></font>";
                            else
                                lblMessage1.Text = "<font color='red' size='6'><b>SGST/UTGST/CGST Customer</b></font>";
                        }
                        else
                        {
                            lblMessage1.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6' style='text-decoartion: blink;'><b>IGST Customer</b></font></span>";
                        }
                    }

                    if (grvItemDetails.FooterRow != null)
                    {
                        Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                        btnAdd.Enabled = false;
                        btnAdd.Visible = false;
                    }

                    if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                    {
                        grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                    }
                    else
                    {
                        if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                            grvItemDetails.HeaderRow.Cells[8].Text = "Discount";
                        else
                            grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                    }

                    CalculateTotalValue();
                    ddlCustomerName.Enabled = false;
                }
                else
                {
                    grvItemDetails.DataSource = null;
                    grvItemDetails.DataBind();
                    ddlCustomerName.Enabled = true;
                    ddlCashDiscount.SelectedIndex = 0;
                    ddlCashDiscount.Enabled = false;
                    ddlTaxFilter.Items.Clear();
                    hdnCDstatus.Value = "0";
                }

                hdnRowCnt.Value = lstSalesItem.Count.ToString();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void ServerActionforHeader()
        {

            string strTransValue = ddlTransactionType.SelectedValue.ToString();

            if (strTransValue == "101" || strTransValue == "001")
            {
                panelReceipt.Attributes.Add("style", "display:inline");
                txtCashBillCustomer.Enabled = true;
                txtCashBillCustomerTown.Enabled = true;
                ddlModeOfREceipt.Attributes.Add("OnChange", "return ChangeModeOfREceipt();");
            }
            else
            {
                panelReceipt.Attributes.Add("style", "display:none");
                ddlModeOfREceipt.Attributes.Remove("OnChange");
            }

            //if (strTransValue == "361" || strTransValue == "371" || strTransValue == "461" || strTransValue == "471")
            //{
            //    txtRefDocument.Enabled = true;
            //}
            //else
            //{
            //    txtRefDocument.Enabled = false;
            //}

            if (strTransValue == "321" || strTransValue == "331" || strTransValue == "341" || strTransValue == "421" || strTransValue == "431" || strTransValue == "441")
            {
                LoadGovtCustomers();
            }
            else if (strTransValue == "361")
            {
                LoadExportCustomers();
            }
            else if (strTransValue == "481")
            {
                LoadCostToCostCustomers();
            }
            else
            {
                LoadCustomers();
            }

            LoadSalesMan();

            if (!(ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.Trim() == "071" || ddlTransactionType.SelectedValue.Trim() == "171" || ddlTransactionType.SelectedValue.ToString() == "481"))
                LoadCashDiscount();
            else
                ddlCashDiscount.Items.Insert(0, new ListItem("0.00", "1"));
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Visible = false;

                if (!chkActive.Checked)
                    SubmitAction();
                else
                    DeActiveSalesInvoice();
            }
            catch (Exception exp)
            {
                string errBranch = string.Empty;

                if (!ddlSalesInvoiceNumber.Visible)
                {
                    if (Session["SalesInvoiceNumber"] != null)
                        errBranch = ddlBranch.SelectedValue + "_" + Session["SalesInvoiceNumber"].ToString();
                    else
                        errBranch = ddlBranch.SelectedValue;
                }
                else
                    errBranch = ddlBranch.SelectedValue + "_" + ddlSalesInvoiceNumber.SelectedValue;

                Exception obj = new Exception();
                obj = new Exception(errBranch + "_" + exp);

                if (obj.ToString().Contains("EinvoiceAuthentication"))
                {
                    Log.WriteExceptionCustom(typeof(SalesInvoiceEntry), obj);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Has been Generated Successfully Without QR Code as there is Some issue with BDO / NIC Portal. Please inform HO');", true);
                }
                else if (obj.ToString().Contains("SendingSMStoCustomers"))
                {
                    Log.WriteExceptionCustom(typeof(SalesInvoiceEntry), obj);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Has been Generated Successfully With QR Code but there is Some issue with SMS Portal. Please inform HO');", true);
                }
                else if (obj.ToString().Contains("Some Excess Supply Quantity"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Could not be Generated as there is Some Excess Supply Quantity Data Issue. Please contact HO');", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + exp.Message.ToString().Replace("'","").Replace("\r\n", " ") + "');", true);
                    //throw exp;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + exp.Message.ToString().Replace("'", "").Replace("\r\n", " ") + "');", true);
                    //throw exp;
                }
            }
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["InvoiceRemarks"] = Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DeActiveSalesInvoice()
        {
            //int result = salesItem.CancelSalesInvoice(ddlBranch.SelectedValue.ToString(), ddlSalesInvoiceNumber.SelectedItem.Text.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlSalesReqNumber.SelectedValue.ToString(), "I");

            DataSet ds = new DataSet();
            ds = salesTrans.CancelSalesInvoice(ddlBranch.SelectedValue.ToString(), ddlCustomerName.SelectedValue.ToString(), ddlSalesInvoiceNumber.SelectedItem.Text.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlSalesReqNumber.SelectedValue.ToString(), "I", Session["InvoiceRemarks"].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                string JSONData = "{\r\n    \"supplier_gstin\":\"" + ds.Tables[0].Rows[0]["GSTIN"] + "\",\r\n    \"doc_no\":\"" + ds.Tables[0].Rows[0]["Document_Number"] + "\"," + "\r\n    \"irn_no\":\"" + ds.Tables[0].Rows[0]["IRN_Number"] + "\"," +
                                  "\r\n    \"doc_date\":\"" + ds.Tables[0].Rows[0]["Document_Date"] + "\",\r\n    \"reason\":\"" + ds.Tables[0].Rows[0]["Reason"] + "\",\r\n    \"remark\":\"" + ds.Tables[0].Rows[0]["Remarks"] + "\"\r\n}";

                einvGen.EinvoiceAuthentication(JSONData, ddlBranch.SelectedValue.ToString(), ddlSalesInvoiceNumber.SelectedItem.Text.ToString(), "2", "CANIRN", ds.Tables[0].Rows[0]["GSTIN"].ToString(), ds.Tables[0].Rows[0]["Document_Type"].ToString(), ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), ds);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["Phone"].ToString().Length >= 10)
                    if (ds.Tables[1].Rows[0]["Phone"].ToString() != "9999999999")
                        sendsms.SendingSMStoCustomers(ddlBranch.SelectedValue, ds.Tables[1].Rows[0]["Phone"].ToString(), ds.Tables[1].Rows[0]["SMS"].ToString(), ds.Tables[1].Rows[0]["Template_Id"].ToString());
            }

            MoveCloudFileToCancelFolder(ddlSalesInvoiceNumber.SelectedValue.Replace("/", "-"));

            Server.ClearError();
            Response.Redirect("SalesInvoice.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void SubmitAction()
        {
            SalesEntity SalesInvoiceEntity = new SalesEntity();
            SalesInvoiceEntity.Items = new List<SalesItem>();
            SalesInvoiceEntity.SalesInvoiceNumber = "";// txtSalesInvoiceNumber.Text;
            SalesInvoiceEntity.SalesInvoiceDate = txtSalesInvoiceDate.Text;
            SalesInvoiceEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
            SalesInvoiceEntity.CustomerCode = ddlCustomerName.SelectedValue.ToString();
            SalesInvoiceEntity.CustSalesReqNumber = ddlSalesReqNumber.SelectedValue.ToString();
            SalesInvoiceEntity.CustomerPONumber = txtCustomerPONo.Text;
            SalesInvoiceEntity.CustomerPODate = txtCustomerPODate.Text;
            SalesInvoiceEntity.SalesManCode = ddlSalesMan.SelectedValue.ToString();
            SalesInvoiceEntity.RefDocumentNumber = txtRefDocument.Text.ToString();
            SalesInvoiceEntity.Remarks = txtRemarks.Text;
            SalesInvoiceEntity.CashDiscountCode = ddlCashDiscount.SelectedValue.ToString();

            Double OrderValue = 0;
            if (txtTotalValue.Text != "")
            {
                OrderValue = Convert.ToDouble(TwoDecimalConversion(txtTotalValue.Text.ToString()));
            }

            SalesInvoiceEntity.OrderValue = OrderValue;
            SalesInvoiceEntity.LRTransfer = "N";//rdLRTransfer.SelectedValue.ToString();
            SalesInvoiceEntity.Carrier = txtCarrier.Text;
            SalesInvoiceEntity.LRNumber = txtLRNumber.Text;
            SalesInvoiceEntity.LRDate = txtLRDate.Text;
            SalesInvoiceEntity.MarkingNumber = txtCaseMarking.Text;
            SalesInvoiceEntity.NumberOfCases = txtNoOfCases.Text.ToString();
            SalesInvoiceEntity.Weight = txtWeight.Text;
            SalesInvoiceEntity.FreightIndicatorCode = ddlFreightIndicator.SelectedValue;

            double dbFreightAmout;
            if (txtFreightAmount.Text != "")
                dbFreightAmout = double.Parse(txtFreightAmount.Text);
            else
                dbFreightAmout = 0;

            SalesInvoiceEntity.FreightAmount = dbFreightAmout;

            SalesInvoiceEntity.BranchCode = ddlBranch.SelectedValue.ToString(); //Session["BranchCode"].ToString();//
            SalesInvoiceEntity.CostToCostCouponInd = hdnCostToCostCoupon.Value;

            double iCourierCharge;
            if (txtCourierCharges.Text != "")
                iCourierCharge = double.Parse(txtCourierCharges.Text);
            else
                iCourierCharge = 0;

            double iInsuranceCharges;
            if (txtInsuranceCharges.Text != "")
                iInsuranceCharges = double.Parse(txtInsuranceCharges.Text);
            else
                iInsuranceCharges = 0;

            SalesInvoiceEntity.CourierCharge = iCourierCharge;
            SalesInvoiceEntity.InsuranceChargePerc = iInsuranceCharges;
            SalesInvoiceEntity.InsuranceCharges = iInsuranceCharges;
            SalesInvoiceEntity.CustomerName = txtCashBillCustomer.Text; //ddlCustomerName.SelectedItem.Text;
            SalesInvoiceEntity.CustomerTown = txtAddress4.Text;
            SalesInvoiceEntity.Indicator = ddlVindicator.SelectedValue; //ddlVindicator.SelectedValue;
            SalesInvoiceEntity.Remarks = txtRemarks.Text;
            SalesInvoiceEntity.ModeOfReceipt = ddlModeOfREceipt.SelectedValue;
            SalesInvoiceEntity.ChequeDraftNumber = txtChequeDraftNo.Text;
            SalesInvoiceEntity.ChequeDraftDate = txtChequeDraftDt.Text;
            SalesInvoiceEntity.BankName = txtBank.Text;
            SalesInvoiceEntity.BankBranchName = txtBankBranch.Text;
            SalesInvoiceEntity.ReceiptLocalOutstation = ddlLocalOutstation.SelectedValue;
            SalesInvoiceEntity.DispatchLocationInd = ddlSecondaryDispLoc.SelectedValue;

            if (txtTotalValue.Text.ToString() != "")
            {
                SalesInvoiceEntity.AmountReceived = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtTotalValue.Text), 0));
            }

            SalesInvoiceEntity.BalanceAmount = 0;

            SalesInvoiceEntity.ShippingName = txtShippingName.Text;
            SalesInvoiceEntity.ShippingAddress1 = txtShippingAddress1.Text == "" ? txtAddress1.Text : txtShippingAddress1.Text;
            SalesInvoiceEntity.ShippingAddress2 = txtShippingAddress2.Text == "" ? txtAddress2.Text : txtShippingAddress2.Text;
            SalesInvoiceEntity.ShippingAddress4 = txtShippingAddress4.Text == "" ? txtAddress4.Text : txtShippingAddress4.Text;
            SalesInvoiceEntity.ShippingGSTIN = txtShippingGSTIN.Text == "" ? txtGSTIN.Text : txtShippingGSTIN.Text;
            SalesInvoiceEntity.ShippingLocation = txtShippingLocation.Text == "" ? txtLocation.Text : txtShippingLocation.Text;
            SalesInvoiceEntity.ShippingState = ddlShippingState.SelectedItem.Text;

            int iNoofRows = 0;
            int SNo = 0;
            SalesItem SalesInvoiceItem = null;
            SalesInvoiceItem = new SalesItem();

            if (ddlSalesReqNumber.SelectedIndex <= 0)
            {
                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        string filter = "Item_Code <> ''"; //textBox1.Text=ur input   
                        string filter1 = "Supplier_Item <>''";
                        DataView view = new DataView(dt);
                        view.RowFilter = filter;
                        view.RowFilter = filter1;
                        dt = view.ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dt.Rows.Count; i++)
                            {
                                SalesInvoiceItem = new SalesItem();
                                SNo += 1;

                                string Oslsind, ValueType = "";
                                Oslsind = dt.Rows[i - 1]["OS"].ToString(); ///txtOSLS.Text;     

                                if (Oslsind == "O")
                                    ValueType = "OS";
                                else if (Oslsind == "L")
                                    ValueType = "LS";

                                if (SalesInvoiceEntity.TransactionTypeCode == "361" || SalesInvoiceEntity.TransactionTypeCode == "461")
                                    ValueType = "FD";
                                if (SalesInvoiceEntity.TransactionTypeCode == "471" || SalesInvoiceEntity.TransactionTypeCode == "371")
                                    ValueType = "LR";

                                SalesInvoiceItem.ValueType = ValueType;
                                SalesInvoiceItem.ItemCode = dt.Rows[i - 1]["Item_Code"].ToString();
                                SalesInvoiceItem.Quantity = dt.Rows[i - 1]["Qty"].ToString();
                                SalesInvoiceItem.OsLsIndicator = dt.Rows[i - 1]["OS"].ToString();
                                SalesInvoiceItem.ListPrice = dt.Rows[i - 1]["Branch_ListPrice"].ToString();
                                SalesInvoiceItem.SlbCode = dt.Rows[i - 1]["SLB"].ToString();

                                if (dt.Rows[i - 1]["SLB_NetValue"].ToString() == "" || dt.Rows[i - 1]["SLB_NetValue"] == null)
                                    SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble("0");
                                else
                                    SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble(dt.Rows[i - 1]["SLB_NetValue"].ToString());

                                if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                                {
                                    SalesInvoiceItem.ItemDiscount = dt.Rows[i - 1]["Coupon"].ToString();
                                }
                                else
                                {
                                    if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                        SalesInvoiceItem.ItemDiscount = dt.Rows[i - 1]["Discount"].ToString();
                                    else
                                        SalesInvoiceItem.ItemDiscount = dt.Rows[i - 1]["Coupon"].ToString();
                                }

                                SalesInvoiceItem.SNO = SNo.ToString();
                                SalesInvoiceEntity.Items.Add(SalesInvoiceItem);
                            }
                        }
                    }
                }
            }

            if (SNo < grvItemDetails.Rows.Count)
            {
                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    SalesInvoiceItem = new SalesItem();

                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[0].FindControl("ddlSupplierName");
                    if (ddlSupplierName.Visible || ddlTransactionType.SelectedValue.ToString() == "461" || ddlSalesReqNumber.SelectedIndex > 0)
                    {
                        SNo += 1;
                        TextBox lblSupplier = (TextBox)gr.Cells[0].FindControl("lblSupplier");
                        DropDownList ddlItemCode = (DropDownList)gr.Cells[1].FindControl("ddlItemCode");
                        TextBox txtItemCode = (TextBox)gr.Cells[1].FindControl("txtItemCode");
                        Button btnSearch = (Button)gr.Cells[1].FindControl("btnSearch");
                        TextBox txtOSLS = (TextBox)gr.Cells[2].FindControl("txtOSLS");
                        DropDownList ddlSLB = (DropDownList)gr.Cells[3].FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)gr.Cells[3].FindControl("txtSLB");
                        TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                        TextBox txtCanOrderQty = (TextBox)gr.Cells[4].FindControl("txtCanOrderQty");
                        TextBox txtBranchListPrice = (TextBox)gr.Cells[5].FindControl("txtBranchListPrice");
                        TextBox txtCostPrice = (TextBox)gr.Cells[5].FindControl("txtCostPrice");
                        TextBox txtSLBNetValue = (TextBox)gr.Cells[6].FindControl("txtSLBNetValue");
                        TextBox txtDiscount = (TextBox)gr.Cells[7].FindControl("txtDiscount");
                        TextBox txtSalesTax = (TextBox)gr.Cells[8].FindControl("txtSalesTax");
                        TextBox txtGrossProfit = (TextBox)gr.Cells[9].FindControl("txtGrossProfit");

                        string Oslsind, ValueType = "";
                        Oslsind = txtOSLS.Text;

                        if (Oslsind == "O")
                            ValueType = "OS";
                        else if (Oslsind == "L")
                            ValueType = "LS";

                        if (SalesInvoiceEntity.TransactionTypeCode == "361" || SalesInvoiceEntity.TransactionTypeCode == "461")
                            ValueType = "FD";
                        if (SalesInvoiceEntity.TransactionTypeCode == "471" || SalesInvoiceEntity.TransactionTypeCode == "371")
                            ValueType = "LR";

                        SalesInvoiceItem.ValueType = ValueType;

                        if (ddlItemCode.Visible)
                            SalesInvoiceItem.ItemCode = ddlItemCode.SelectedValue.ToString();
                        else
                            SalesInvoiceItem.ItemCode = txtItemCode.Text.ToString();

                        SalesInvoiceItem.AvialableQuantity = txtCanOrderQty.Text;
                        SalesInvoiceItem.Quantity = txtQuantity.Text;
                        SalesInvoiceItem.OsLsIndicator = txtOSLS.Text;
                        SalesInvoiceItem.ListPrice = txtBranchListPrice.Text;
                        SalesInvoiceItem.SlbCode = ddlSLB.SelectedValue.ToString();

                        if (txtSLBNetValue.Text == "" || txtSLBNetValue == null)
                            SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble("0");
                        else
                            SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble(txtSLBNetValue.Text);

                        if (txtDiscount.Text == "")
                            txtDiscount.Text = "0";

                        SalesInvoiceItem.ItemDiscount = txtDiscount.Text;  //Need to verify for Distress Type
                        SalesInvoiceItem.SNO = SNo.ToString();
                        SalesInvoiceEntity.Items.Add(SalesInvoiceItem);
                    }
                }
            }

            BtnSubmit.Visible = false;
            grvItemDetails.Enabled = false;
            imgEditToggle.Visible = false;
            upHeader.Update();
            ddlTaxFilter.Enabled = false;
            ddlSalesReqNumber.Enabled = false;
            UpdPanelGrid.Update();
            PanelShippingDtls.Enabled = false;
            PanelHeaderDtls.Enabled = false;
            panelReceipt.Enabled = false;

            DataSet ds = salesTrans.AddNewSalesInvoiceEntry(ref SalesInvoiceEntity);
            int result = 0;

            if ((SalesInvoiceEntity.ErrorMsg == string.Empty) && (SalesInvoiceEntity.ErrorCode == "0"))
            {
                txtSalesInvoiceNumber.Text = SalesInvoiceEntity.SalesInvoiceNumber;
                Session["SalesInvoiceNumber"] = SalesInvoiceEntity.SalesInvoiceNumber;
                result = Convert.ToInt16(ds.Tables[0].Rows[0]["Result"]);

                BtnReset.Enabled = true;
                BtnReset.Visible = true;
                BtnReport.Visible = true;

                DataSet Datasetresult = srpt.GetEinvoicingDetails(ddlBranch.SelectedValue.ToString(), SalesInvoiceEntity.SalesInvoiceNumber);
                GenerateJSON objGenJsonData = new GenerateJSON();

                if (result == 1)
                {
                    string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                    einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, SalesInvoiceEntity.SalesInvoiceNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Has been Generated Successfully with B2B/B2C QR Code. Please Click On Report Button To Take the Print');", true);
                }
                else
                {
                    einvGen.EinvoiceAuthenticationB2C(objGenJsonData.GenerateInvoiceJSONDataB2C(Datasetresult, ddlBranch.SelectedValue), ddlBranch.SelectedValue, SalesInvoiceEntity.SalesInvoiceNumber, "1", "NOIRN");

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Has been Generated Successfully with B2C QR Code. Please Click On Report Button To Take the Print');", true);
                }                

                if (ds.Tables[0].Rows[0]["Phone"].ToString().Length >= 10)
                    if (ds.Tables[0].Rows[0]["Phone"].ToString() != "9999999999")
                        sendsms.SendingSMStoCustomers(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Phone"].ToString(), ds.Tables[0].Rows[0]["SMS"].ToString(), ds.Tables[0].Rows[0]["Template_Id"].ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + SalesInvoiceEntity.ErrorMsg + "');", true);
            }
        }

        public void MoveCloudFileToCancelFolder(string InvoiceNumber)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            SessionOptions sessionOptions = new SessionOptions();

            sessionOptions.Protocol = Protocol.Scp;
            sessionOptions.HostName = ConfigurationManager.AppSettings["CloudStorage_HostName"].ToString();
            sessionOptions.UserName = ConfigurationManager.AppSettings["CloudStorage_UserName"].ToString();
            sessionOptions.Password = ConfigurationManager.AppSettings["CloudStorage_Password"].ToString();
            sessionOptions.SshHostKeyFingerprint = ConfigurationManager.AppSettings["CloudStorage_FingerPrint"].ToString(); //256 bit

            Session session = new Session();

            try
            {
                var CurrentDate = DateTime.Now;

                string filePath = InvoiceNumber + ".pdf";
                string RemoteFolder = "/www/wwwroot/" + Session["BranchCode"].ToString() + "/Invoice/" + CurrentDate.Year.ToString().PadLeft(4, '0') + "/" + CurrentDate.Month.ToString().PadLeft(2, '0') + "/" + CurrentDate.Day.ToString().PadLeft(2, '0') + "/";

                session.Open(sessionOptions);

                if (session.FileExists(RemoteFolder + filePath))
                    session.MoveFile(RemoteFolder + filePath, RemoteFolder + "/Cancelled/" + filePath);

                session.Close();
                session.Dispose();
                session = null;
            }
            catch (Exception Exp)
            {
                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                    session.Dispose();
                    session = null;
                }
            }
        }

        private List<Supplier> GetAllSuppliers()
        {
            SalesItem objItem = new SalesItem();
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliers();
            return lstSuppliers;
        }

        private List<Supplier> GetAllSuppliersDispLoc(int DispLocInd)
        {
            SalesItem objItem = new SalesItem();
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliersDispLoc(DispLocInd);
            return lstSuppliers;
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Part_Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("CanOrderQty", typeof(string)));
            dt.Columns.Add(new DataColumn("OS", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB_Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch_ListPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("Cost_Price", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB_NetValue", typeof(string)));

            if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
            {
                dt.Columns.Add(new DataColumn("Coupon", typeof(string)));
            }
            else
            {
                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                    dt.Columns.Add(new DataColumn("Discount", typeof(string)));
                else
                    dt.Columns.Add(new DataColumn("Coupon", typeof(string)));
            }

            dt.Columns.Add(new DataColumn("Original_Coupon", typeof(string)));
            dt.Columns.Add(new DataColumn("Coupon_Indicator", typeof(string)));
            dt.Columns.Add(new DataColumn("SalesTax", typeof(string)));
            dt.Columns.Add(new DataColumn("Gross_Profit", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_SaleValue", typeof(string)));
            dt.Columns.Add(new DataColumn("Original_Req_Qty", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Supplier_Name"] = string.Empty;
                dr["Supplier_Item"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["Part_Number"] = string.Empty;
                dr["Qty"] = string.Empty;
                dr["CanOrderQty"] = string.Empty;
                dr["OS"] = string.Empty;
                dr["SLB"] = string.Empty;
                dr["SLB_Item"] = string.Empty;
                dr["Branch_ListPrice"] = string.Empty;
                dr["Cost_Price"] = string.Empty;
                dr["SLB_NetValue"] = string.Empty;

                if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                {
                    dr["Coupon"] = string.Empty;
                }
                else
                {
                    if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                        dr["Discount"] = string.Empty;
                    else
                        dr["Coupon"] = string.Empty;
                }

                dr["Original_Coupon"] = string.Empty;
                dr["Coupon_Indicator"] = string.Empty;
                dr["SalesTax"] = string.Empty;
                dr["Gross_Profit"] = string.Empty;
                dr["Item_SaleValue"] = string.Empty;
                dr["Original_Req_Qty"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
            {
                grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
            }
            else
            {
                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                    grvItemDetails.HeaderRow.Cells[8].Text = "Discount";
                else
                    grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
            }

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 13;
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        private void BindGrid()
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");

                if (Session["BranchCode"].ToString().ToUpper() == "CHE")
                    ddlSupplierName.DataSource = GetAllSuppliersDispLoc(ddlSecondaryDispLoc.SelectedIndex);
                else
                    ddlSupplierName.DataSource = GetAllSuppliers();

                ddlSupplierName.DataValueField = "SupplierCode";
                ddlSupplierName.DataTextField = "SupplierName";
                ddlSupplierName.DataBind();

                TextBox txtItemCode = (TextBox)gr.Cells[2].FindControl("txtItemCode");
                TextBox txtSupplierPartNo = (TextBox)gr.Cells[6].FindControl("txtSupplierPartNo");
                TextBox txtListPrice = (TextBox)gr.Cells[7].FindControl("txtListPrice");
                TextBox txtCostPrice = (TextBox)gr.Cells[8].FindControl("txtCostPrice");
                TextBox txtPurDiscount = (TextBox)gr.Cells[9].FindControl("txtPurDiscount");
                TextBox txtDiscount = (TextBox)gr.Cells[10].FindControl("txtDiscount");
                TextBox txtEDIndicator = (TextBox)gr.Cells[11].FindControl("txtEDIndicator");
                TextBox txtEDValue = (TextBox)gr.Cells[12].FindControl("txtEDValue");
            }
        }

        private void LoadTransactionType()
        {
            List<TransactionType> lstTransactionType = new List<TransactionType>();
            lstTransactionType = salesTrans.GetTransactionType();
            ddlTransactionType.DataSource = lstTransactionType;
            ddlTransactionType.DataTextField = "TransactionTypeDesc";
            ddlTransactionType.DataValueField = "TransactionTypeCode";
            ddlTransactionType.DataBind();
        }

        private void LoadSalesInvoiceNumber(string strBranch)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            lstSalesEntity = salesTrans.GetSalesInvoiceNumber(strBranch);
            ddlSalesInvoiceNumber.DataSource = lstSalesEntity;
            ddlSalesInvoiceNumber.DataTextField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataValueField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataBind();
        }

        private void LoadCashDiscount()
        {
            List<CashDiscount> lstCashDiscount = new List<CashDiscount>();
            lstCashDiscount = salesTrans.GetCashDiscount(Session["BranchCode"].ToString());
            ddlCashDiscount.DataSource = lstCashDiscount;
            ddlCashDiscount.DataTextField = "CashDiscountDesc";
            ddlCashDiscount.DataValueField = "CashDiscountCode";
            ddlCashDiscount.DataBind();
        }

        private void LoadSalesMan()
        {
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

        private void LoadLimits(string strBranchCode, string CustomerCode)
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            //string CanCustomerRaise;

            CanBillUpTo = 0;

            //if (Session["BranchCode"].ToString().ToUpper() == "MGT")
            //    CanCustomerRaise = customers.CanCustomerOSDaysCreditLimits(strBranchCode, CustomerCode);
            //else
            //    CanCustomerRaise = customers.CanCustomerOSCreditLimits(strBranchCode, CustomerCode);

            //if (Session["BranchCode"].ToString().ToUpper() == "MGT")
            //lstCustomers = customers.GetCustomerOSCreditLimitsOutstanding(strBranchCode, CustomerCode);
            //else

            lstCustomers = customers.GetCustomerOSCreditLimits(strBranchCode, CustomerCode);

            if (lstCustomers.Count > 0)
            {
                txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[0].Credit_Limit.ToString());
                txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[0].Outstanding_Amount.ToString());
                CanBillUpTo = Convert.ToDouble(lstCustomers[0].Credit_Limit) - Convert.ToDouble(lstCustomers[0].Outstanding_Amount);
            }

            if (lstCustomers[0].OSCreditLimiStatus.ToString() == "ERROR" && lstCustomers[0].GroupCompanyStatus.ToString() == "0")
            {
                GroupCompIndication = 0;
                lblHeaderMessage.Text = "Outstanding Amount Exceeds";
                alertmsg = "There is No Credit Limit / Can Bill Amount for this Customer and You cannot proceed with billing.";

                if (CanBillUpTo <= 0)
                    return;

                txtCanBillUpTo.Text = "0";
                ddlSalesReqNumber.Enabled = false;
                FreezeOrUnFreezeButtons(false);
                BtnSubmit.Visible = false;
            }
            else if (lstCustomers[0].OSCreditLimiStatus.ToString() == "ERROR" && lstCustomers[0].GroupCompanyStatus.ToString() == "1")
            {
                GroupCompIndication = 1;
                lblHeaderMessage.Text = "Outstanding Amount Exceeds for Group Company Dealer - " + lstCustomers[0].Customer_Name + " || " + lstCustomers[0].Customer_Code;
                alertmsg = "There is No Credit Limit / Can Bill Amount for Group Company Customer " + lstCustomers[0].Customer_Name + " || " + lstCustomers[0].Customer_Code + " and You cannot proceed with billing.";

                if (CanBillUpTo <= 0)
                    return;

                txtCanBillUpTo.Text = "0";
                ddlSalesReqNumber.Enabled = false;
                FreezeOrUnFreezeButtons(false);
                BtnSubmit.Visible = false;
            }
            else
            {
                if (CanBillUpTo <= 0)
                {
                    lblHeaderMessage.Text = "Outstanding Amount Exceeds";
                    alertmsg = "There is No Credit Limit / Can Bill Amount for this Customer and You cannot proceed with billing.";
                    ddlSalesReqNumber.Enabled = false;
                    FreezeOrUnFreezeButtons(false);
                    BtnSubmit.Visible = false;
                    return;
                }
                else
                {
                    lblHeaderMessage.Text = "";
                    txtCanBillUpTo.Text = TwoDecimalConversion(CanBillUpTo.ToString());
                    ddlSalesReqNumber.Enabled = true;
                    FreezeOrUnFreezeButtons(true);
                    BtnSubmit.Visible = true;
                }
            }
        }

        private void LoadShippingAddressStates(string BranchCode)
        {
            List<IMPALLibrary.SlabState> StateList = new List<IMPALLibrary.SlabState>();
            StateList = salesTrans.GetAllStatesShipping(BranchCode);
            ddlShippingState.DataSource = StateList;
            ddlShippingState.DataTextField = "StateName";
            ddlShippingState.DataValueField = "StateCode";
            ddlShippingState.DataBind();
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            if (grvItemDetails.FooterRow != null)
            {
                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                btnAdd.Enabled = Fzflag;
            }

            ddlSalesReqNumber.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
            BtnReportOs.Enabled = !Fzflag;
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

        private void LoadGovtCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetGovtCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
        }

        private void LoadExportCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetExportCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
        }

        private void LoadCostToCostCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetCostToCostCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
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

            lstCustomers = customers.GetCustomerDetailsWithShippingAddress(strBranch, ddlCustomerName.SelectedValue);
            ViewState["CustomerDetails"] = lstCustomers;
        }

        private void AddNewRow()
        {
            int rowIndex = 0;
            int iNoofRows = 0;

            if (ViewState["GridRowCount"] != null)
            {
                iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
            }

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (Convert.ToInt64(ViewState["GridRowCount"]) == 0)
                    {
                        dtCurrentTable.Rows.Clear();
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["SNo"] = 1;
                    }
                    else
                    {
                        for (int i = iNoofRows; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[0].FindControl("ddlSupplierName");
                            TextBox lblSupplier = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[0].FindControl("lblSupplier");
                            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("ddlItemCode");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("txtItemCode");
                            Button btnSearch = (Button)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("btnSearch");
                            TextBox txtOSLS = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[2].FindControl("txtOSLS");
                            DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("ddlSLB");
                            TextBox txtSLB = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("txtSLB");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtQuantity");
                            TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtCanOrderQty");
                            TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txtBranchListPrice");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txtCostPrice");
                            TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[6].FindControl("txtSLBNetValue");
                            TextBox txtDiscount = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtDiscount");
                            TextBox txtOrgCoupon = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtOrgCoupon");
                            HiddenField txtHdnCouponInd = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtHdnCouponInd");
                            TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[8].FindControl("txtSalesTax");
                            TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtGrossProfit");
                            HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtItemSaleValue");
                            HiddenField txtHdnReqOrderQty = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtHdnReqOrderQty");
                            HiddenField txtAddlTax = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtAddlTax");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            if (ddlSupplierName.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = ddlSupplierName.SelectedValue;//txtCCWHNo.Text;
                                dtCurrentTable.Rows[i - 1]["Supplier_Item"] = ddlSupplierName.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = lblSupplier.Text.ToString();
                            }

                            if (ddlItemCode.SelectedItem != null)
                            {
                                dtCurrentTable.Rows[i - 1]["Item_Code"] = ddlItemCode.SelectedValue; //txtItemCode.Text;
                                dtCurrentTable.Rows[i - 1]["Part_Number"] = ddlItemCode.SelectedItem.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["Qty"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["CanOrderQty"] = txtCanOrderQty.Text;
                            dtCurrentTable.Rows[i - 1]["Original_Req_Qty"] = txtHdnReqOrderQty.Value;

                            dtCurrentTable.Rows[i - 1]["OS"] = txtOSLS.Text;
                            if (ddlSLB.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["SLB"] = ddlSLB.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["SLB_Item"] = ddlSLB.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["SLB_Item"] = txtSLB.Text.ToString();
                            }
                            dtCurrentTable.Rows[i - 1]["Branch_ListPrice"] = txtBranchListPrice.Text;

                            dtCurrentTable.Rows[i - 1]["Cost_Price"] = txtCostPrice.Text;
                            dtCurrentTable.Rows[i - 1]["SLB_NetValue"] = txtSLBNetValue.Text;

                            if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                            {
                                dtCurrentTable.Rows[i - 1]["Coupon"] = txtDiscount.Text;
                            }
                            else
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                    dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;
                                else
                                    dtCurrentTable.Rows[i - 1]["Coupon"] = txtDiscount.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["Original_Coupon"] = txtOrgCoupon.Text;
                            dtCurrentTable.Rows[i - 1]["Coupon_Indicator"] = txtHdnCouponInd.Value;
                            dtCurrentTable.Rows[i - 1]["SalesTax"] = txtSalesTax.Text;
                            dtCurrentTable.Rows[i - 1]["Gross_Profit"] = txtGrossProfit.Text;
                            dtCurrentTable.Rows[i - 1]["Item_SaleValue"] = txtItemSaleValue.Value;
                            rowIndex++;
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();
                    grvItemDetails.DataSource = dtCurrentTable;
                    grvItemDetails.DataBind();

                    if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                    {
                        grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                    }
                    else
                    {
                        if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                            grvItemDetails.HeaderRow.Cells[8].Text = "Discount";
                        else
                            grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                    }
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
            HideDllItemCodeDropDown();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                int iNoofRows = 0;
                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }

                if (dt.Rows.Count > 1)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {

                        DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                        TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                        DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                        Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                        TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                        TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                        TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                        DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                        TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                        TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtCostPrice");
                        TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                        TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                        TextBox txtOrgCoupon = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtOrgCoupon");
                        HiddenField txtHdnCouponInd = (HiddenField)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtHdnCouponInd");
                        TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                        TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                        HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");
                        HiddenField txtHdnReqOrderQty = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtHdnReqOrderQty");
                        HiddenField txtAddlTax = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtAddlTax");

                        txtSupplierPartNo.Text = dt.Rows[i - 1]["Part_Number"].ToString();
                        lblSupplier.Text = dt.Rows[i - 1]["Supplier_Item"].ToString();
                        txtItemCode.Text = dt.Rows[i - 1]["Item_Code"].ToString();
                        txtItemCode.Visible = true;
                        txtQuantity.Text = dt.Rows[i - 1]["Qty"].ToString();
                        txtCanOrderQty.Text = dt.Rows[i - 1]["CanOrderQty"].ToString();
                        txtHdnReqOrderQty.Value = dt.Rows[i - 1]["Original_Req_Qty"].ToString();
                        txtOSLS.Text = dt.Rows[i - 1]["OS"].ToString();
                        txtBranchListPrice.Text = dt.Rows[i - 1]["Branch_ListPrice"].ToString();
                        txtCostPrice.Text = dt.Rows[i - 1]["Cost_Price"].ToString();
                        txtSLBNetValue.Text = dt.Rows[i - 1]["SLB_NetValue"].ToString();

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            txtDiscount.Text = dt.Rows[i - 1]["Coupon"].ToString();
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                txtDiscount.Text = dt.Rows[i - 1]["Discount"].ToString();
                            else
                                txtDiscount.Text = dt.Rows[i - 1]["Coupon"].ToString();
                        }

                        txtOrgCoupon.Text = dt.Rows[i - 1]["Original_Coupon"].ToString();
                        txtHdnCouponInd.Value = dt.Rows[i - 1]["Coupon_Indicator"].ToString();
                        txtSalesTax.Text = dt.Rows[i - 1]["SalesTax"].ToString();
                        txtGrossProfit.Text = dt.Rows[i - 1]["Gross_Profit"].ToString();
                        txtItemSaleValue.Value = dt.Rows[i - 1]["Item_SaleValue"].ToString();
                        txtSLB.Text = dt.Rows[i - 1]["SLB_Item"].ToString();
                        //ddlSLB.SelectedValue = dt.Rows[i - 1]["SLB"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private void HideDllItemCodeDropDown()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSupplierName");
                TextBox lblSupplier = (TextBox)grvItemDetails.Rows[index].Cells[0].FindControl("lblSupplier");
                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlItemCode");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtItemCode");
                Button btnSearch = (Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch");
                DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[index].Cells[3].FindControl("ddlSLB");

                if (index != grvItemDetails.Rows.Count - 1)
                {
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSupplierName")).Visible = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("lblSupplier")).Visible = true;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlItemCode")).Visible = false;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSLB")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Visible = true;
                    //((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Text = ddlSLB.SelectedItem.Text;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCostPrice")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtOSLS")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCanOrderQty")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtDiscount")).Enabled = false;
                }
                else
                {
                    ((TextBox)grvItemDetails.Rows[index].Cells[0].FindControl("lblSupplier")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCostPrice")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSLB")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCanOrderQty")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtDiscount")).Enabled = false;
                }
            }

            txtHdnGridCtrls.Text = sb.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSearch = (Button)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtCurrentSearch = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName"); ;
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                TextBox txtSLB = (TextBox)grdrDropDownRow.FindControl("txtSLB");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdrDropDownRow.FindControl("txtHdnReqOrderQty");
                HiddenField txtAddlTax = (HiddenField)grdrDropDownRow.FindControl("txtAddlTax");

                if (btnSearch.Text == "Reset")
                {
                    ddlItemCode.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtOSLS.Text = "";
                    txtQuantity.Text = "";
                    txtCanOrderQty.Text = "";
                    ddlSLB.SelectedValue = "0";
                    ddlSLB.Enabled = false;
                    txtSLB.Text = "";
                    txtBranchListPrice.Text = "";
                    txtCostPrice.Text = "";
                    txtSLBNetValue.Text = "";
                    txtDiscount.Text = "";
                    txtSalesTax.Text = "";
                    txtAddlTax.Value = "";
                    txtGrossProfit.Text = "";
                    txtItemSaleValue.Value = "";
                    txtHdnReqOrderQty.Value = "";
                    CalculateTotalValue();
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        List<SalesItem> lstSuppliers = new List<SalesItem>();
                        ListItem firstListItem = new ListItem();

                        lstSuppliers = salesTrans.GetItems(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text, ddlTransactionType.SelectedValue.ToString(), Session["BranchCode"].ToString());
                        ddlItemCode.DataSource = lstSuppliers;
                        ddlItemCode.Items.Add(firstListItem);
                        ddlItemCode.DataTextField = "SupplierPartNumber"; //"supplier_part_number";
                        ddlItemCode.DataValueField = "ItemCode";
                        ddlItemCode.DataBind();
                        ddlItemCode.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlCurrentDropDownList = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
                Button btnSearch = (Button)grdrDropDownRow.FindControl("btnSearch");
                TextBox txtCurrentSearch = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName");
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                TextBox txtSLB = (TextBox)grdrDropDownRow.FindControl("txtSLB");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtOrgCoupon = (TextBox)grdrDropDownRow.FindControl("txtOrgCoupon");
                HiddenField txtHdnCouponInd = (HiddenField)grdrDropDownRow.FindControl("txtHdnCouponInd");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdrDropDownRow.FindControl("txtHdnReqOrderQty");
                HiddenField txtAddlTax = (HiddenField)grdrDropDownRow.FindControl("txtAddlTax");

                ddlItemCode.Visible = false;
                txtCurrentSearch.Visible = true;
                btnSearch.Text = "Search";
                txtItemCode.Text = "";
                txtCurrentSearch.Text = "";
                txtOSLS.Text = "";
                txtQuantity.Text = "";
                txtCanOrderQty.Text = "";
                ddlSLB.SelectedValue = "0";
                ddlSLB.Enabled = false;
                txtSLB.Text = "";
                txtBranchListPrice.Text = "";
                txtCostPrice.Text = "";
                txtSLBNetValue.Text = "";
                txtDiscount.Text = "";
                txtSalesTax.Text = "";
                txtAddlTax.Value = "";
                txtGrossProfit.Text = "";
                txtItemSaleValue.Value = "";
                txtHdnReqOrderQty.Value = "";

                //CalculateTotalValue();

                if (ddlSupplierName.SelectedIndex != 0)
                {
                    btnSearch.Enabled = true;
                    txtCurrentSearch.Enabled = true;
                    txtCurrentSearch.Focus();
                    
                    string CouponIndicator = salesTrans.GetSupplierCouponIndicator(ddlSupplierName.SelectedValue.ToString(), Session["BranchCode"].ToString());
                    txtHdnCouponInd.Value = CouponIndicator;
                }
                else
                {
                    btnSearch.Enabled = false;
                    txtCurrentSearch.Enabled = false;
                    txtHdnCouponInd.Value = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlItemCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlItemCode = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlItemCode.Parent.Parent);
                TextBox txtSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtOrgCoupon = (TextBox)grdrDropDownRow.FindControl("txtOrgCoupon");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                HiddenField txtProductGroupCode = (HiddenField)grdrDropDownRow.FindControl("txtProductGroupCode");
                HiddenField txtAddlTax = (HiddenField)grdrDropDownRow.FindControl("txtAddlTax");

                if (ddlItemCode.SelectedItem.Value != "0")
                {
                    int HSNCode = salesTrans.CheckHSNCodeItem(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                    if (HSNCode > 0)
                    {
                        bool isExisting = CheckExisting(ddlItemCode.SelectedValue);
                        double lstAddlSalesTax = 0;

                        if (isExisting)
                        {
                            double lstMultipleSalesTax = salesTrans.GetSalesTax(ddlCustomerName.SelectedItem.Value, ddlItemCode.SelectedValue, Session["BranchCode"].ToString(), "", ddlTransactionType.SelectedValue.ToString(), txtProductGroupCode.Value);

                            if (hdnCalamityCess.Value == "Y")
                                lstAddlSalesTax = salesTrans.GetAddlSalesTaxGST(ddlCustomerName.SelectedItem.Value, ddlItemCode.SelectedValue, Session["BranchCode"].ToString(), "", ddlTransactionType.SelectedValue.ToString(), txtProductGroupCode.Value);

                            //bool isSingleTax = true; //CheckMultipleTax(TwoDecimalConversion(lstMultipleSalesTax.ToString()));

                            //if (isSingleTax)
                            //{
                                txtSalesTax.Text = TwoDecimalConversion(lstMultipleSalesTax.ToString());
                                txtAddlTax.Value = TwoDecimalConversion(lstAddlSalesTax.ToString());

                                lblMessage.Text = "";
                                lblMessage1.Text = "";

                                string BrItemPrice = "";
                                BrItemPrice = salesTrans.GetBranchItemPrice(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                                if (BrItemPrice != "")
                                {
                                    txtQuantity.Enabled = true;
                                    txtItemCode.Text = ddlItemCode.SelectedItem.Value;
                                    txtSupplierPartNo.Text = ddlItemCode.SelectedItem.Text;
                                    string strTransactionType = ddlTransactionType.SelectedItem.Value;

                                    List<SalesItem> lstItemQty = new List<SalesItem>();
                                    lstItemQty = salesTrans.GetItemQty(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString(), strTransactionType);

                                    if (lstItemQty.Count > 0)
                                    {
                                        txtQuantity.Text = "";
                                        txtCanOrderQty.Text = lstItemQty[0].Quantity.ToString();

                                        if (txtCanOrderQty.Text == "0")
                                        {
                                            ddlItemCode.SelectedValue = "0";
                                            txtOSLS.Text = "";
                                            txtBranchListPrice.Text = "";
                                            txtQuantity.Text = "";
                                            ddlSLB.SelectedValue = "0";
                                            txtCostPrice.Text = "";
                                            txtCanOrderQty.Text = "";
                                            txtItemCode.Text = "";
                                            txtProductGroupCode.Value = "";
                                            txtDiscount.Text = "";
                                            txtOrgCoupon.Text = "";

                                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This item is not available in this transaction type');", true);
                                            //salesItem.UpdateLossofSale();
                                        }
                                        else
                                        {
                                            List<SalesItem> lstItemPrice = new List<SalesItem>();
                                            lstItemPrice = salesTrans.GetItemPrice(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                                            if (string.IsNullOrEmpty(lstItemPrice[0].ListPrice.ToString()) || lstItemPrice[0].ListPrice.ToString() == "0.00")
                                                txtBranchListPrice.Text = "";
                                            else
                                            {
                                                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                                {
                                                    txtQuantity.AutoPostBack = true;
                                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString());
                                                }
                                                else
                                                {
                                                    txtQuantity.AutoPostBack = false;
                                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].ListPrice.ToString());
                                                }
                                            }

                                            txtOSLS.Text = lstItemPrice[0].OsLsIndicator.ToString();
                                            txtCostPrice.Text = TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString());
                                            lblPackingQuantity.Text = lstItemPrice[0].PackingQuantity.ToString();
                                            lblDepotName.Text = lstItemPrice[0].DepotLongName;
                                            txtProductGroupCode.Value = lstItemPrice[0].ProductGroupCode;
                                            txtDiscount.Text = TwoDecimalConversion(lstItemPrice[0].PurchaseDiscount.ToString());

                                            if (Convert.ToDecimal(lstItemPrice[0].PurchaseDiscount) != 0)
                                                txtDiscount.Enabled = true;
                                            else
                                                txtDiscount.Enabled = false;

                                            txtOrgCoupon.Text = TwoDecimalConversion(lstItemPrice[0].PurchaseDiscount.ToString());

                                            lblMessage.Text = "Quantity Available = " + txtCanOrderQty.Text + " , Packing Quantity = " + lblPackingQuantity.Text + " Plant = " + lblDepotName.Text;

                                            List<Customer> lstCustomers1 = (List<Customer>)ViewState["CustomerDetails"];

                                            if (lstCustomers1[0].Party_Type_Code.ToUpper() == "DLREXP")
                                            {
                                                lblMessage1.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>Export Customer</b></font></span>";
                                                PanelTaxFilter.Attributes.Add("style", "display:none");
                                            }
                                            else
                                            {
                                                PanelTaxFilter.Attributes.Add("style", "display:inline");
                                                if (hdnCustOSLSStatus.Value == "L")
                                                {
                                                    if (hdnCalamityCess.Value == "Y")
                                                        lblMessage.Text = lblMessage.Text + "   <span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>CALAMITY CESS/SGST/UTGST/CGST Customer</b></font>";
                                                    else
                                                        lblMessage.Text = lblMessage.Text + "   <span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>SGST/UTGST/CGST Customer</b></font>";
                                                }
                                                else
                                                {
                                                    lblMessage.Text = lblMessage.Text + "   <span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6' style='text-decoartion: blink;'><b>IGST Customer</b></font></span>";
                                                }
                                            }

                                            lblDepotName.Text = "";
                                            lblPackingQuantity.Text = "";
                                            txtQuantity.Enabled = true;
                                            txtQuantity.Focus();
                                            List<SalesItem> lstSLB = new List<SalesItem>();
                                            lstSLB = salesTrans.GetSLB(ddlCustomerName.SelectedItem.Value, ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                                            ddlSLB.DataSource = lstSLB;
                                            ddlSLB.DataTextField = "SlbDesc";
                                            ddlSLB.DataValueField = "SlbCode";
                                            ddlSLB.DataBind();
                                        }
                                    }
                                }
                                else
                                {
                                    txtQuantity.Enabled = false;
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item is not available in Branch Item Price');", true);
                                }
                            //}
                            //else
                            //{
                            //    txtQuantity.Enabled = false;
                            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Multiple Sales Tax Billing is not Possible in GST. Please Remove this Part Number');", true);
                            //}
                        }
                        else
                        {
                            txtQuantity.Enabled = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Record already exists...');", true);
                        }
                    }
                    else
                    {
                        txtQuantity.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HSN Code is not available for this Item');", true);
                    }
                }
                else
                {
                    txtOSLS.Text = "";
                    txtBranchListPrice.Text = "";
                    txtQuantity.Text = "";
                    ddlSLB.SelectedValue = "0";
                    txtCostPrice.Text = "";
                    txtCanOrderQty.Text = "";
                    txtItemCode.Text = "";
                    txtProductGroupCode.Value = "";
                    txtDiscount.Text = "";
                    txtOrgCoupon.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlSLB_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSLB = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSLB.Parent.Parent);
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName");
                TextBox lblSupplier = (TextBox)grdrDropDownRow.FindControl("lblSupplier");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtOrgCoupon = (TextBox)grdrDropDownRow.FindControl("txtOrgCoupon");
                HiddenField txtHdnCouponInd = (HiddenField)grdrDropDownRow.FindControl("txtHdnCouponInd");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtProductGroupCode = (HiddenField)grdrDropDownRow.FindControl("txtProductGroupCode");
                HiddenField txtAddlTax = (HiddenField)grdrDropDownRow.FindControl("txtAddlTax");

                foreach (GridViewRow row in grvItemDetails.Rows)
                {
                    //Finding Dropdown control  
                    Control ctrl = row.FindControl("ddlSLB") as DropDownList;
                    if (ctrl != null)
                    {
                        DropDownList ddl1 = (DropDownList)ctrl;
                        //Comparing ClientID of the dropdown with sender
                        if (ddlSLB.ClientID == ddl1.ClientID)
                        {
                            txthdSlab.Text = ddl1.SelectedItem.Text;
                            break;
                        }
                    }
                }

                List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                string SuppCode = string.Empty;

                if (ddlSupplierName.SelectedValue.ToString() == "0")
                    SuppCode = lblSupplier.Text.Substring(0, 3);
                else
                    SuppCode = ddlSupplierName.SelectedValue.ToString();
                
                string CouponIndicator = salesTrans.GetSupplierCouponIndicator(SuppCode, Session["BranchCode"].ToString());
                txtHdnCouponInd.Value = CouponIndicator;

                if (ddlSalesReqNumber.SelectedIndex <= 0)
                {
                    double dblSLBNetValuePrice = 0;
                    double CouponCharges = 0;
                    decimal CouponValue = 0;
                    int Qty = 0;
                    if (txtQuantity.Text != "")
                    {
                        Qty = int.Parse(txtQuantity.Text);
                        double BranchListPrice = 0;
                        if (txtBranchListPrice.Text != "")
                            BranchListPrice = double.Parse(txtBranchListPrice.Text.ToString());
                        string Indicator = "";
                        if (txtOSLS.Text == "L") Indicator = "LS";
                        if (txtOSLS.Text == "O") Indicator = "OS";
                        int SLBCode = 0;
                        if (ddlSLB.SelectedItem.Value != "")
                            SLBCode = int.Parse(ddlSLB.SelectedItem.Value);

                        string strTransactionType, strItemCode;
                        strTransactionType = ddlTransactionType.SelectedItem.Value; //201";// "361";// "011";

                        if (strTransactionType == "461")
                            strItemCode = txtItemCode.Text;
                        else
                            strItemCode = ddlItemCode.SelectedItem.Value;

                        if (SLBCode > 0)
                            dblSLBNetValuePrice = salesTrans.GetSLBNetValuePrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, Indicator, strTransactionType, SLBCode);

                        txtSLBNetValue.Text = dblSLBNetValuePrice.ToString();

                        double dblItemSellingPrice = 0, dblGrossProfit = 0, dblCostPrice = 0;
                        if (ddlTransactionType.SelectedValue.ToString() == "461") //FDO
                            dblItemSellingPrice = salesTrans.GetFDOItemSellingPrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, dblSLBNetValuePrice, Indicator, strTransactionType, SLBCode);
                        else
                            dblItemSellingPrice = salesTrans.GetItemSellingPrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, dblSLBNetValuePrice, Indicator, strTransactionType, SLBCode);

                        if (txtCostPrice.Text != "")
                            dblCostPrice = double.Parse(txtCostPrice.Text.ToString());

                        if (dblItemSellingPrice == 0)
                            dblGrossProfit = 0;
                        else
                            dblGrossProfit = (((dblItemSellingPrice - dblCostPrice) / dblItemSellingPrice) * 100);

                        txtGrossProfit.Text = TwoDecimalConversion(dblGrossProfit.ToString());

                        double SaleValue = 0, dblCashDiscountPer = 0, dblCourierCharge = 0, dblInsuranceAmt = 0, AmountReceived;

                        if (ddlCashDiscount.SelectedValue != "")
                        {
                            dblCashDiscountPer = Convert.ToDouble(ddlCashDiscount.SelectedItem.Text);
                        }

                        if (SLBCode <= 50)
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                            {
                                SaleValue = BranchListPrice;
                            }
                            else
                            {
                                SaleValue = BranchListPrice + (BranchListPrice * (dblSLBNetValuePrice / 100));
                            }
                        }
                        else if (SLBCode > 50 && SLBCode <= 90)
                        {
                            SaleValue = dblSLBNetValuePrice;
                        }
                        else if (SLBCode > 90 && SLBCode <= 99)
                        {
                            SaleValue = dblItemSellingPrice;
                            txtBranchListPrice.Text = TwoDecimalConversion(dblItemSellingPrice.ToString());
                        }

                        string transactionType = ddlTransactionType.SelectedValue.ToString();
                        //decimal rate = salesTrans.GetItemRate(txtItemCode.Text);

                        if (!(transactionType == "141" || transactionType == "041" || transactionType == "481"))
                        {
                            CouponCharges = salesTrans.GetCouponValue(Session["BranchCode"].ToString(), ddlCustomerName.SelectedItem.Value, ddlSupplierName.SelectedValue.ToString(), strItemCode, Qty, strTransactionType);

                            if (txtDiscount.Text.Trim() == "" || txtDiscount.Text.Trim() == "0.00" || txtDiscount.Text.Trim() == "0")
                                txtDiscount.Text = "0.00"; //TwoDecimalConversion(CouponCharges.ToString());

                            txtOrgCoupon.Text = TwoDecimalConversion(CouponCharges.ToString());

                            if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                            {
                                txtDiscount.Text = "0.00";
                                txtOrgCoupon.Text = "0.00";
                            }

                            if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                            {
                                txtDiscount.Enabled = true;
                            }
                            else
                                txtDiscount.Enabled = false;
                        }
                        else
                            txtDiscount.Enabled = true;

                        decimal rate = Convert.ToDecimal(txtDiscount.Text.ToString());

                        if (!(transactionType == "141" || transactionType == "041" || transactionType == "481"))
                        {
                            CouponValue = CouponValue + Convert.ToDecimal(rate);
                            SaleValue = (SaleValue - Convert.ToDouble(CouponValue)) * (1 - (dblCashDiscountPer / 100));
                        }
                        else
                        {
                            SaleValue = (SaleValue * (1 + (dblCashDiscountPer / 100))) * (1 - (dblCashDiscountPer / 100));
                        }

                        if (hdnCalamityCess.Value == "Y")
                        {
                            SaleValue = SaleValue * (1 + (Convert.ToDouble(txtSalesTax.Text) / 100)) * (1 + (Convert.ToDouble(txtAddlTax.Value) / 100));
                        }
                        else
                        {
                            SaleValue = SaleValue * (1 + (Convert.ToDouble(txtSalesTax.Text) / 100));
                        }

                        if (txtCourierCharges.Text != "")
                        {
                            dblCourierCharge = Convert.ToDouble(txtCourierCharges.Text);
                        }
                        if (txtInsuranceCharges.Text != "")
                        {
                            dblInsuranceAmt = Convert.ToDouble(txtInsuranceCharges.Text);
                        }
                        ///SALEVALUE – CASH  DISCOUNT + TAX VALUE + COURIER CHARGES + INSURANCE CHARGES + POSTAGE CHARGES – COUPON CHARGES.
                        if (ddlTransactionType.SelectedValue.ToString() == "461") ///FDO
                        {
                            txtSalesTax.Text = "S";
                            txtItemSaleValue.Value = SaleValue.ToString();
                            AmountReceived = (SaleValue) * Qty;

                            double GrandAmountRececived = 0;
                            if (txtTotalValue.Text != "" || txtTotalValue.Text == "0.00")
                            {
                                GrandAmountRececived = Convert.ToDouble(txtTotalValue.Text);
                            }

                            GrandAmountRececived = GrandAmountRececived + AmountReceived + dblCourierCharge + dblInsuranceAmt;
                        }
                        else
                        {
                            txtItemSaleValue.Value = SaleValue.ToString();
                            AmountReceived = (SaleValue);
                        }

                        CalculateTotalValue();
                    }
                    else
                    {
                        lblMessage.Text = "Order Quantity should not be Zero or empty";
                    }
                }
                else
                {
                    List<SalesItem> lstSalesItem = new List<SalesItem>();

                    if (ddlSLB.SelectedValue.ToString() != "0" || ddlTransactionType.SelectedValue.ToString() == "481")
                    {
                        lstSalesItem = salesTrans.GetCustomerSalesReqItemDetails(ddlSalesReqNumber.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlCashDiscount.SelectedItem.Text, ddlItemCode.SelectedValue.ToString(), ddlSLB.SelectedValue.ToString(), Session["BranchCode"].ToString(), txtDiscount.Text.ToString(), txtHdnCustTownCode.Text);

                        if (hdnCostToCostCoupon.Value == "Y")
                        {
                            txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                            txtCostPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                            txtSLBNetValue.Text = "0.00";
                            //txtOrgCoupon.Text = TwoDecimalConversion(lstSalesItem[0].ItemDiscount.ToString());
                            txtHdnCouponInd.Value = lstSalesItem[0].CouponIndicator.ToString();

                            if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                            {
                                txtDiscount.Text = "0.00";
                                txtOrgCoupon.Text = "0.00";
                            }

                            if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                            {
                                txtDiscount.Enabled = true;
                            }
                            else
                                txtDiscount.Enabled = false;

                            if (lstCustomers[0].Party_Type_Code.ToUpper() == "DLREXP" || lstSalesItem[0].SalesTaxText.ToLower() == "second sales" || lstSalesItem[0].SalesTaxText.ToLower() == "agricultural implements")
                            {
                                txtSalesTax.Text = "S";
                            }
                            else
                            {
                                txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[0].SalesTaxPercentage.ToString());
                            }

                            txtGrossProfit.Text = TwoDecimalConversion(lstSalesItem[0].GrossProfit.ToString());
                            txtItemSaleValue.Value = lstSalesItem[0].SaleValue.ToString();
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "481")
                            {
                                txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                                txtCostPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                                txtSLBNetValue.Text = "0.00";
                                txtOrgCoupon.Text = "";
                                txtHdnCouponInd.Value = "";

                                if (txtOrgCoupon.Text == "")
                                    txtOrgCoupon.Text = "0.00";

                                txtDiscount.Enabled = true;

                                if (lstCustomers[0].Party_Type_Code.ToUpper() == "DLREXP" || lstSalesItem[0].SalesTaxText.ToLower() == "second sales" || lstSalesItem[0].SalesTaxText.ToLower() == "agricultural implements")
                                {
                                    txtSalesTax.Text = "S";
                                }
                                else
                                {
                                    txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[0].SalesTaxPercentage.ToString());
                                }

                                txtGrossProfit.Text = TwoDecimalConversion(lstSalesItem[0].GrossProfit.ToString());
                                txtItemSaleValue.Value = lstSalesItem[0].SaleValue.ToString();
                            }
                            else
                            {
                                txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[0].ListPrice.ToString());
                                txtCostPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                                txtSLBNetValue.Text = TwoDecimalConversion(lstSalesItem[0].SLBNetValuePrice.ToString());
                                //txtOrgCoupon.Text = TwoDecimalConversion(lstSalesItem[0].ItemDiscount.ToString());
                                txtHdnCouponInd.Value = lstSalesItem[0].CouponIndicator.ToString();

                                if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                                {
                                    txtDiscount.Text = "0.00";
                                    txtOrgCoupon.Text = "0.00";
                                }

                                if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                                {
                                    txtDiscount.Enabled = true;
                                }
                                else
                                    txtDiscount.Enabled = false;

                                if (lstCustomers[0].Party_Type_Code.ToUpper() == "DLREXP" || lstSalesItem[0].SalesTaxText.ToLower() == "second sales" || lstSalesItem[0].SalesTaxText.ToLower() == "agricultural implements")
                                {
                                    txtSalesTax.Text = "S";
                                }
                                else
                                {
                                    txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[0].SalesTaxPercentage.ToString());
                                }

                                txtGrossProfit.Text = TwoDecimalConversion(lstSalesItem[0].GrossProfit.ToString());
                                txtItemSaleValue.Value = lstSalesItem[0].SaleValue.ToString();
                            }
                        }
                    }
                    else
                    {
                        txtBranchListPrice.Text = "";
                        txtCostPrice.Text = "";
                        txtSLBNetValue.Text = "";
                        txtSalesTax.Text = "";
                        txtAddlTax.Value = "";
                        txtGrossProfit.Text = "";
                        txtItemSaleValue.Value = "0";
                    }

                    CalculateTotalValue();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void grvItemDetails_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "V")
                    return;

                if (IsPostBack)
                {
                    string transactionType = ddlTransactionType.SelectedValue.ToString();

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlSupplierName = (DropDownList)e.Row.Cells[1].FindControl("ddlSupplierName");

                        if (Session["BranchCode"].ToString().ToUpper() == "CHE")
                            ddlSupplierName.DataSource = GetAllSuppliersDispLoc(ddlSecondaryDispLoc.SelectedIndex);
                        else
                            ddlSupplierName.DataSource = GetAllSuppliers();

                        ddlSupplierName.DataValueField = "SupplierCode";
                        ddlSupplierName.DataTextField = "SupplierName";
                        ddlSupplierName.DataBind();

                        TextBox txtItemCode = (TextBox)e.Row.Cells[1].FindControl("txtItemCode");
                        TextBox txtCanOrderQty = (TextBox)e.Row.FindControl("txtCanOrderQty");
                        HiddenField txtItemSaleValue = (HiddenField)e.Row.FindControl("txtItemSaleValue");
                        TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                        TextBox txtDiscount = (TextBox)e.Row.FindControl("txtDiscount");
                        TextBox txtOrgCoupon = (TextBox)e.Row.FindControl("txtOrgCoupon");
                        DropDownList ddlSLB = (DropDownList)e.Row.FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)e.Row.FindControl("txtSLB");
                        HiddenField txtHdnReqOrderQty = (HiddenField)e.Row.FindControl("txtHdnReqOrderQty");

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            ddlSLB.Enabled = false;
                            txtOrgCoupon.Visible = true;
                        }
                        else
                        {
                            if (transactionType == "141" || transactionType == "041" || transactionType == "481")
                            {
                                //txtDiscount.Enabled = true;
                                ddlSLB.Enabled = false;
                                txtOrgCoupon.Visible = false;
                            }
                            else
                            {
                                //txtDiscount.Enabled = true;
                                ddlSLB.Enabled = true;
                                txtOrgCoupon.Visible = true;
                            }
                        }

                        ddlSLB.Attributes.Add("OnChange", "SalesInvoiceValidationHeader();");

                        if (ddlSalesReqNumber.SelectedIndex <= 0)
                        {
                            txtQuantity.Attributes.Add("OnChange", "SalesInvoiceQtyChangeGST(" + hdnRowCnt.Value + ",'" + txtCanOrderQty.ClientID + "','" + txtHdnReqOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + ddlSLB.ClientID + "','" + txtItemSaleValue.ClientID + "','" + txtDiscount.ClientID + "')");

                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                txtDiscount.Attributes.Add("OnChange", "SalesInvoiceDiscountChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                            else
                                txtDiscount.Attributes.Add("OnChange", "SalesInvoiceCouponChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string SixDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.000000";
            else
                return string.Format("{0:0.000000}", Convert.ToDecimal(strValue));
        }

        private string DecimalToIntConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0";
            else
                return string.Format("{0:0}", Convert.ToDecimal(strValue));
        }

        protected void ddlSalesInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSalesInvoiceNumber.SelectedValue != "-- Select --")
                {
                    List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
                    lstSalesEntity = salesTrans.GetSalesInvoiceHeader(ddlSalesInvoiceNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());
                    if (lstSalesEntity.Count > 0)
                    {
                        for (int i = 0; i <= lstSalesEntity.Count - 1; i++)
                        {
                            ddlTransactionType.SelectedValue = lstSalesEntity[i].TransactionTypeCode;
                            txtSalesInvoiceDate.Text = lstSalesEntity[i].SalesInvoiceDate;
                            ddlCustomerName.SelectedValue = lstSalesEntity[i].CustomerCode;
                            ddlSalesMan.SelectedValue = lstSalesEntity[i].SalesManCode;
                            rdLRTransfer.SelectedValue = "N";//lstSalesEntity[i].LRTransfer;
                            txtLRNumber.Text = lstSalesEntity[i].LRNumber;
                            txtLRDate.Text = lstSalesEntity[i].LRDate;
                            ddlCashDiscount.Text = lstSalesEntity[i].CashDiscountCode;
                            txtTotalValue.Text = TwoDecimalConversion(lstSalesEntity[i].OrderValue.ToString());
                            ddlVindicator.SelectedValue = lstSalesEntity[i].Indicator;
                            txtCaseMarking.Text = lstSalesEntity[i].MarkingNumber;
                            txtNoOfCases.Text = lstSalesEntity[i].NumberOfCases.ToString();
                            txtWeight.Text = lstSalesEntity[i].Weight;
                            txtCarrier.Text = lstSalesEntity[i].Carrier.ToString();
                            if (lstSalesEntity[i].FreightIndicatorCode != "")
                                ddlFreightIndicator.SelectedValue = lstSalesEntity[i].FreightIndicatorCode;

                            txtFreightAmount.Text = lstSalesEntity[i].FreightAmount.ToString();
                            txtCourierCharges.Text = lstSalesEntity[i].CourierCharge.ToString();
                            txtInsuranceCharges.Text = lstSalesEntity[i].InsuranceCharges.ToString();
                            txtCustomerPONo.Text = lstSalesEntity[i].CustomerPONumber;
                            txtCustomerPODate.Text = lstSalesEntity[i].CustomerPODate.ToString();
                            string TransValue = ddlTransactionType.SelectedValue.ToString();

                            if (TransValue == "101" || TransValue == "001")
                            {
                                txtBank.Text = lstSalesEntity[i].BankName.ToString();
                                ddlModeOfREceipt.SelectedValue = lstSalesEntity[i].ModeOfReceipt.ToString();
                                txtChequeDraftNo.Text = lstSalesEntity[i].ChequeDraftNumber.ToString();
                                txtChequeDraftDt.Text = lstSalesEntity[i].ChequeDraftDate.ToString();
                                txtBankBranch.Text = lstSalesEntity[i].BankBranchName.ToString();
                                ddlLocalOutstation.SelectedValue = lstSalesEntity[i].LocalOutstation.ToString();
                            }

                            if (TransValue == "171")
                            {
                                txtTotalValue.Visible = false;
                            }

                            if (System.DateTime.Now.ToString("dd/MM/yyyy") == txtSalesInvoiceDate.Text)
                            {
                                chkActive.Visible = true;
                                BtnSubmit.Visible = true;
                            }
                            else
                            {
                                chkActive.Visible = false;
                                BtnSubmit.Visible = false;
                            }
                        }

                        string strTransValue = ddlTransactionType.SelectedValue.ToString();

                        if (strTransValue == "101" || strTransValue == "001")
                        {
                            panelReceipt.Attributes.Add("style", "display:inline");
                            txtChequeDraftNo.Enabled = false;
                            txtChequeDraftDt.Enabled = false;
                            txtBank.Enabled = false;
                            txtBankBranch.Enabled = false;
                            ddlModeOfREceipt.Enabled = false;
                            ddlLocalOutstation.Enabled = false;
                        }
                        else
                        {
                            panelReceipt.Attributes.Add("style", "display:none");
                        }

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
                                        hdnCustOSLSStatus.Value = lstCustomers[i].CustOSLSStatus.ToString();
                                        hdnCalamityCess.Value = lstCustomers[i].CalamityCess.ToString();
                                        hdnCostToCostCoupon.Value = lstCustomers[i].CostToCostCoupon.ToString();
                                        txtLocation.Text = lstCustomers[i].Location.ToString();

                                        txtShippingName.Text = ddlCustomerName.SelectedItem.Text;
                                        txtShippingAddress1.Text = lstCustomers[i].address1.ToString();
                                        txtShippingAddress2.Text = lstCustomers[i].address2.ToString();
                                        txtShippingAddress4.Text = lstCustomers[i].address3.ToString();
                                        txtShippingGSTIN.Text = lstCustomers[i].GSTIN.ToString();
                                        txtShippingLocation.Text = lstCustomers[i].Location.ToString();

                                        txtHdnCustTownCode.Text = lstCustomers[0].Town_Code.ToString();
                                        txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[i].Credit_Limit.ToString());
                                        txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[i].Outstanding_Amount.ToString());
                                        txtCashBillCustomer.Text = lstCustomers[i].Customer_Name.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    List<SalesItem> lstSalesItem = new List<SalesItem>();
                    lstSalesItem = salesTrans.GetSalesInvoiceDetail(ddlSalesInvoiceNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    if (lstSalesEntity.Count > 0)
                    {
                        grvItemDetails.DataSource = lstSalesItem;
                        grvItemDetails.DataBind();

                        for (int i = 1; i <= lstSalesItem.Count; i++)
                        {
                            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                            TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                            Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                            TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                            TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                            DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                            TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                            TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtCostPrice");
                            TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                            TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                            TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                            TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                            HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");
                            LinkButton delete = (LinkButton)grvItemDetails.Rows[i - 1].Cells[10].FindControl("lnkDelete");

                            lblSupplier.Text = lstSalesItem[i - 1].SupplierName.ToString();
                            ddlSupplierName.Visible = false;
                            txtSupplierPartNo.Text = lstSalesItem[i - 1].ItemSupplierPartNumber.ToString();
                            txtItemCode.Text = lstSalesItem[i - 1].ItemCode.ToString();
                            txtItemCode.Visible = true;
                            ddlItemCode.Visible = false;
                            btnSearch.Visible = false;
                            txtCanOrderQty.Visible = false;
                            ddlSLB.Visible = false;
                            txtSLB.Visible = true;
                            delete.Visible = false;

                            txtQuantity.Text = lstSalesItem[i - 1].Quantity.ToString();
                            txtOSLS.Text = lstSalesItem[i - 1].OsLsIndicator.ToString();
                            txtSLB.Text = lstSalesItem[i - 1].SlbDesc.ToString();
                            //txtBranchListPrice.Text = lstSalesItem[i - 1].ListPrice.ToString();
                            txtSLBNetValue.Text = lstSalesItem[i - 1].SLBNetValuePrice.ToString();
                            txtDiscount.Text = lstSalesItem[i - 1].ItemDiscount.ToString();
                            txtSalesTax.Text = lstSalesItem[i - 1].SalesTaxPercentage.ToString();
                            
                            List<SalesItem> lstItemPrice = new List<SalesItem>();
                            double dblItemSellingPrice = 0, dblCostPrice = 0;
                            lstItemPrice = salesTrans.GetItemPrice(lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString());

                            if (lstItemPrice.Count() > 0)
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "481") //For Distress & Distress Manual;
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString());
                                else
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].ListPrice.ToString());

                                if (ddlTransactionType.SelectedValue.ToString() == "461") //FDO
                                    dblItemSellingPrice = salesTrans.GetFDOItemSellingPrice(ddlCustomerName.SelectedValue.ToString(), lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), Convert.ToInt16(lstSalesItem[i - 1].Quantity), Convert.ToDouble(txtBranchListPrice.Text), Convert.ToDouble(lstSalesItem[i - 1].SLBNetValuePrice), lstSalesItem[i - 1].OsLsIndicator.ToString(), ddlTransactionType.SelectedValue.ToString(), Convert.ToInt16(lstSalesItem[i - 1].SlbCode));
                                else
                                    dblItemSellingPrice = salesTrans.GetItemSellingPrice(ddlCustomerName.SelectedValue.ToString(), lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), Convert.ToInt16(lstSalesItem[i - 1].Quantity), Convert.ToDouble(txtBranchListPrice.Text), Convert.ToDouble(lstSalesItem[i - 1].SLBNetValuePrice), lstSalesItem[i - 1].OsLsIndicator.ToString(), ddlTransactionType.SelectedValue.ToString(), Convert.ToInt16(lstSalesItem[i - 1].SlbCode));

                                dblCostPrice = Convert.ToDouble(TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString()));
                                if (dblItemSellingPrice == 0)
                                    txtGrossProfit.Text = "0";
                                else
                                    txtGrossProfit.Text = TwoDecimalConversion(Convert.ToString((((dblItemSellingPrice - dblCostPrice) / dblItemSellingPrice) * 100)));
                            }
                            else
                            {
                                txtBranchListPrice.Text = "0";
                                txtGrossProfit.Text = "0";
                            }
                        }
                    }
                }
                else
                {
                    BtnReset_Click(this, null);
                    chkActive.Visible = false;
                    BtnSubmit.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {

            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    Server.ClearError();
                    Response.Redirect("SalesInvoice.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else if (hdnScreenMode.Value == "V")
                {
                    ddlSalesInvoiceNumber_SelectedIndexChanged(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void BtnReportOs_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CustomerCode"] = ddlCustomerName.SelectedValue;
                Server.ClearError();
                Response.Redirect("OutstandingDaysReport.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            BtnReport.Visible = false;
            upHeader.Visible = false;
            UpdPanelGrid.Visible = false;
            btnBack.Visible = true;

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSelectionFormula = default(string);
            string strBrnchField = default(string);
            string strBrnchValue = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strBrnchField = "{V_Invoice.Branch_Code}";
            strBrnchValue = ddlBranch.SelectedValue;
            strField = "{V_Invoice.Document_number}";
            strValue = txtSalesInvoiceNumber.Text;
            strSelectionFormula = strBrnchField + "='" + strBrnchValue + "' and " + strField + "='" + strValue + "'";
            strReportName = "po_pp_invoice_invGST";

            //SalesReport srpt = new SalesReport();
            //hdnEwayBillInd.Value = srpt.GetEWayBillInd(strBrnchValue, strValue);                

            crySalesInvoiceReprint.ReportName = strReportName;
            crySalesInvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            crySalesInvoiceReprint.GenerateReportAndExportInvoiceA4(strValue.Replace("/", "-"), 1);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("SalesInvoice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBranch.SelectedValue != "" && ddlBranch.SelectedValue != "0")
                    LoadSalesInvoiceNumber(ddlBranch.SelectedValue);
                LoadCustomers();
                LoadSalesMan();
                FreezeOrUnFreezeButtons(false);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "V";
                    LoadSalesInvoiceNumber(ddlBranch.SelectedValue);
                    LoadCustomers();
                    LoadSalesMan();
                    LoadCashDiscount();

                    ddlSalesInvoiceNumber.SelectedIndex = -1;
                    ddlSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Visible = false;
                    imgEditToggle.Visible = false;
                    FirstGridViewRow();
                }
                else if (hdnScreenMode.Value == "V")
                {
                    hdnScreenMode.Value = "A";
                    ddlSalesInvoiceNumber.Visible = false;
                    txtSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Text = string.Empty;
                    txtSalesInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlTransactionType.SelectedValue = "0";
                    ddlCustomerName.SelectedValue = "0";
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString(); //ConfigurationManager.AppSettings["BranchCode"].ToString();
                    txtSalesInvoiceDate.Text = string.Empty;
                    ddlSalesMan.SelectedValue = "0";
                    txtLRNumber.Text = string.Empty; ;
                    txtLRDate.Text = string.Empty; ;
                    ddlCashDiscount.Text = string.Empty;
                    txtTotalValue.Text = string.Empty;
                    txtCaseMarking.Text = string.Empty;
                    txtNoOfCases.Text = string.Empty;
                    txtWeight.Text = string.Empty;
                    txtCarrier.Text = string.Empty;
                    ddlFreightIndicator.SelectedValue = "0";
                    txtFreightAmount.Text = string.Empty;
                    txtCourierCharges.Text = string.Empty;
                    txtInsuranceCharges.Text = string.Empty;
                    txtCustomerPONo.Text = string.Empty;
                    txtCustomerPODate.Text = string.Empty;
                }

                BtnSubmit.Attributes.Add("OnClick", "return SalesInvoiceEntrySubmit();");

                FreezeOrUnFreezeButtons(false);
                DisableOnEditMode();
                grvItemDetails.Enabled = false;
                BtnSubmit.Enabled = false;
                upHeader.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        private void DisableOnEditMode()
        {
            //--- Sales Invoice
            ddlSecondaryDispLoc.Enabled = false;
            ddlTransactionType.Enabled = false;
            ddlCustomerName.Enabled = false;
            ddlSalesReqNumber.Enabled = false;
            rdLRTransfer.Enabled = false;
            txtCourierCharges.Enabled = false;
            txtInsuranceCharges.Enabled = false;
            txtCustomerPONo.Enabled = false;
            txtCustomerPODate.Enabled = false;
            ddlCashDiscount.Enabled = false;
            hdnCDstatus.Value = "0";
            ddlSalesMan.Enabled = false;
            txtRefDocument.Enabled = false;
            ddlVindicator.Enabled = false;
            BtnReportOs.Enabled = false;

            //--- Customer Information
            txtCustomerCode.Enabled = false;
            txtAddress1.Enabled = false;
            txtAddress2.Enabled = false;
            txtAddress4.Enabled = false;
            txtGSTIN.Enabled = false;
            txtLocation.Enabled = false;
            txtCustomerCreditLimit.Enabled = false;
            txtCustomerOutStanding.Enabled = false;
            txtCanBillUpTo.Enabled = false;

            //--- Carrier Information
            txtLRNumber.Enabled = false;
            txtCaseMarking.Enabled = false;
            ddlFreightIndicator.Enabled = false;
            txtLRDate.Enabled = false;
            txtNoOfCases.Enabled = false;
            txtFreightAmount.Enabled = false;
            txtCarrier.Enabled = false;
            txtWeight.Enabled = false;
            txtRemarks.Enabled = false;

            //-- Receipt Details

            if (ddlTransactionType.SelectedValue == "101" || ddlTransactionType.SelectedValue == "001")
            {
                txtChequeDraftNo.Enabled = false;
                txtChequeDraftDt.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlModeOfREceipt.Enabled = false;
                ddlLocalOutstation.Enabled = false;
            }
        }

        private void BindFDOItemDetail(string InwardNumber)
        {
            List<SalesItem> lstSalesItem = new List<SalesItem>();
            lstSalesItem = salesTrans.GetFDOInwardItemDetail(InwardNumber, Session["BranchCode"].ToString());
            if (lstSalesItem.Count > 0)
            {
                grvItemDetails.DataSource = lstSalesItem;
                grvItemDetails.DataBind();
                hdnRowCnt.Value = lstSalesItem.Count.ToString();

                for (int i = 1; i <= lstSalesItem.Count; i++)
                {
                    DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                    TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                    DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                    Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                    TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                    TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                    DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                    TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                    TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                    TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtCostPrice");
                    TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                    TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                    TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                    //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].Cells[1].FindControl("txtEDIndicator");
                    TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                    HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");

                    List<SalesItem> lstSLB = new List<SalesItem>();
                    lstSLB = salesTrans.GetSLB(ddlCustomerName.SelectedItem.Value, lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                    ddlSLB.DataSource = lstSLB;
                    ddlSLB.DataTextField = "SlbDesc"; //"supplier_part_number";
                    ddlSLB.DataValueField = "SlbCode";
                    ddlSLB.DataBind();

                    lblSupplier.Text = lstSalesItem[i - 1].SupplierName.ToString();
                    ddlSupplierName.Visible = false;
                    txtSupplierPartNo.Text = lstSalesItem[i - 1].ItemSupplierPartNumber.ToString();
                    txtItemCode.Text = lstSalesItem[i - 1].ItemCode.ToString();
                    txtItemCode.Visible = true;
                    ddlItemCode.Visible = false;
                    btnSearch.Visible = false;
                    txtCanOrderQty.Visible = false;
                    ddlSLB.Visible = true;
                    txtSLB.Visible = false;

                    txtQuantity.Text = lstSalesItem[i - 1].Quantity.ToString();
                    txtOSLS.Text = lstSalesItem[i - 1].OsLsIndicator.ToString();
                    txtBranchListPrice.Text = lstSalesItem[i - 1].ListPrice.ToString();
                    txtGrossProfit.Text = "0";
                }
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQuantity = (TextBox)sender;
                GridViewRow grdRow = ((GridViewRow)txtQuantity.Parent.Parent);
                DropDownList ddlSupplierName = (DropDownList)grdRow.FindControl("ddlSupplierName");
                DropDownList ddlItemCode = (DropDownList)grdRow.FindControl("ddlItemCode");
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
                DropDownList ddlSLB = (DropDownList)grdRow.FindControl("ddlSLB");
                TextBox txtSLBNetValue = (TextBox)grdRow.FindControl("txtSLBNetValue");
                TextBox txtSalesTax = (TextBox)grdRow.FindControl("txtSalesTax");
                HiddenField txtAddlTax = (HiddenField)grdRow.FindControl("txtAddlTax");
                TextBox txtCostPrice = (TextBox)grdRow.FindControl("txtCostPrice");
                TextBox txtGrossProfit = (TextBox)grdRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdRow.FindControl("txtHdnReqOrderQty");
                TextBox txtDiscount = (TextBox)grdRow.FindControl("txtDiscount");
                TextBox txtOrgCoupon = (TextBox)grdRow.FindControl("txtOrgCoupon");
                HiddenField txtHdnCouponInd = (HiddenField)grdRow.FindControl("txtHdnCouponInd");

                if (txtQuantity.Text != null && txtQuantity.Text != "")
                {
                    if (Convert.ToInt64(txtQuantity.Text.ToString()) > 0)
                    {
                        if (!(ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "071" || ddlTransactionType.SelectedValue.ToString() == "171" || ddlTransactionType.SelectedValue.ToString() == "421" || ddlTransactionType.SelectedValue.ToString() == "431" || ddlTransactionType.SelectedValue.ToString() == "441" || ddlTransactionType.SelectedValue.ToString() == "461" || ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "361"))
                        {
                            if (string.IsNullOrEmpty(txtHdnReqOrderQty.Value))
                                txtHdnReqOrderQty.Value = "0";

                            if (Convert.ToInt64(txtQuantity.Text.ToString()) > Convert.ToInt64(txtHdnReqOrderQty.Value))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Quantity Should not exceed Required Quantity');", true);
                                txtQuantity.Text = txtHdnReqOrderQty.Value;
                            }
                        }
                        else
                        {
                            ddlSLB.Enabled = true;

                            if (ddlSLB.Items.Count == 2)
                            {
                                ddlSLB.SelectedIndex = 1;
                                ddlSLB_OnSelectedIndexChanged(ddlSLB, EventArgs.Empty);
                            }

                            if (ddlSLB.SelectedIndex == 0)
                                ddlSLB.Focus();
                        }
                    }
                    else
                    {
                        if (ddlSLB.Items.Count <= 2)
                            ddlSLB.Enabled = false;
                        else
                            ddlSLB.Enabled = true;
                    }

                    string transactionType = ddlTransactionType.SelectedValue.ToString();
                    string strItemCode;
                    double CouponCharges = 0;
                    int Qty = int.Parse(txtQuantity.Text);

                    if (transactionType == "461")
                        strItemCode = txtItemCode.Text;
                    else
                        strItemCode = ddlItemCode.SelectedItem.Value;

                    if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                    {
                        CouponCharges = salesTrans.GetCouponValue(Session["BranchCode"].ToString(), ddlCustomerName.SelectedItem.Value, ddlSupplierName.SelectedValue.ToString(), strItemCode, Qty, transactionType);

                        if (txtDiscount.Text.Trim() == "" || txtDiscount.Text.Trim() == "0.00" || txtDiscount.Text.Trim() == "0")
                            txtDiscount.Text = "0.00"; //TwoDecimalConversion(CouponCharges.ToString());

                        txtOrgCoupon.Text = TwoDecimalConversion(CouponCharges.ToString());

                        if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                        {
                            txtDiscount.Text = "0.00";
                            txtOrgCoupon.Text = "0.00";
                        }

                        if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                        {
                            txtDiscount.Text = txtOrgCoupon.Text;
                            txtDiscount.Enabled = true;
                        }
                        else
                            txtDiscount.Enabled = false;
                    }
                    else
                    {
                        if (!(transactionType == "141" || transactionType == "041" || transactionType == "481"))
                        {
                            CouponCharges = salesTrans.GetCouponValue(Session["BranchCode"].ToString(), ddlCustomerName.SelectedItem.Value, ddlSupplierName.SelectedValue.ToString(), strItemCode, Qty, transactionType);

                            if (txtDiscount.Text.Trim() == "" || txtDiscount.Text.Trim() == "0.00" || txtDiscount.Text.Trim() == "0")
                                txtDiscount.Text = "0.00"; //TwoDecimalConversion(CouponCharges.ToString());

                            txtOrgCoupon.Text = TwoDecimalConversion(CouponCharges.ToString());

                            if (txtOrgCoupon.Text == "" || txtOrgCoupon.Text == "0.00")
                            {
                                txtDiscount.Text = "0.00";
                                txtOrgCoupon.Text = "0.00";
                            }

                            if (Convert.ToDecimal(txtOrgCoupon.Text.ToString()) > 0) //&& txtHdnCouponInd.Value == "Y")
                            {
                                txtDiscount.Text = txtOrgCoupon.Text;
                                txtDiscount.Enabled = true;
                            }
                            else
                                txtDiscount.Enabled = false;
                        }
                        else
                            txtDiscount.Enabled = true;
                    }

                    CalculateTotalValue();
                }
                else
                {
                    if (ddlSLB.Items.Count <= 2)
                        ddlSLB.Enabled = false;
                    else
                        ddlSLB.Enabled = true;

                    txtSLBNetValue.Text = "";
                    txtSalesTax.Text = "";
                    txtAddlTax.Value = "";
                    txtCostPrice.Text = "";
                    txtGrossProfit.Text = "";
                    txtItemSaleValue.Value = "";
                    txtDiscount.Text = "";
                    txtOrgCoupon.Text = "";
                    txtDiscount.Enabled = false;
                }

                if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                {
                    ddlSLB.SelectedIndex = 0;
                    txtSLBNetValue.Text = "";
                    ddlSLB.Enabled = false;
                    ddlSLB_OnSelectedIndexChanged(ddlSLB, EventArgs.Empty);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (!(ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141"))
            {
                TextBox txtDiscount = (TextBox)sender;
                GridViewRow grdRow = ((GridViewRow)txtDiscount.Parent.Parent);
                DropDownList ddlSLB = (DropDownList)grdRow.FindControl("ddlSLB");

                ddlSLB_OnSelectedIndexChanged(ddlSLB, EventArgs.Empty);
            }

            CalculateTotalValue();
        }

        protected void grvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.Rows.Clear();
                dt.AcceptChanges();

                if (grvItemDetails.Rows.Count >= 1)
                {
                    for (int i = 0; i < grvItemDetails.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        var strSNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSNo");
                        var strSupplierName = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierName");
                        var strSupplier = (TextBox)grvItemDetails.Rows[i].FindControl("lblSupplier");
                        var strItemCode = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemCode");
                        var strddlItemCode = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlItemCode");
                        var strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        var strOSLS = (TextBox)grvItemDetails.Rows[i].FindControl("txtOSLS");
                        var strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        var strCanOrderQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtCanOrderQty");
                        var strddlSLB = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSLB");
                        var strSLB = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLB");
                        var strBranchListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtBranchListPrice");
                        var strCostPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtCostPrice");
                        var strSLBNetValue = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLBNetValue");
                        var strDiscount = (TextBox)grvItemDetails.Rows[i].FindControl("txtDiscount");
                        var strOrgCoupon = (TextBox)grvItemDetails.Rows[i].FindControl("txtOrgCoupon");
                        var strCouponInd = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnCouponInd");
                        var strSalesTax = (TextBox)grvItemDetails.Rows[i].FindControl("txtSalesTax");
                        var strAddlTax = (HiddenField)grvItemDetails.Rows[i].FindControl("txtAddlTax");
                        var strGrossProfit = (TextBox)grvItemDetails.Rows[i].FindControl("txtGrossProfit");
                        var strItemSaleValue = (HiddenField)grvItemDetails.Rows[i].FindControl("txtItemSaleValue");
                        var strOriginalReqQty = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnReqOrderQty");

                        dr["SNo"] = strSNo.Text;

                        if (strSupplier.Text != "")
                        {
                            List<Supplier> supplier = new List<Supplier>();

                            if (Session["BranchCode"].ToString().ToUpper() == "CHE")
                                supplier = GetAllSuppliersDispLoc(ddlSecondaryDispLoc.SelectedIndex);
                            else
                                supplier = GetAllSuppliers();

                            supplier = supplier.Where(a => a.SupplierName == strSupplier.Text).ToList();

                            if (supplier.Count > 0)
                            {
                                dr["Supplier_Name"] = supplier[0].SupplierCode;
                            }
                        }
                        else
                        {
                            dr["Supplier_Name"] = strSupplierName.SelectedValue;
                        }

                        dr["Supplier_Item"] = strSupplier.Text;
                        dr["Item_Code"] = strItemCode.Text;
                        dr["Part_Number"] = strSupplierPartNo.Text;
                        dr["Qty"] = strQty.Text;
                        dr["CanOrderQty"] = strCanOrderQty.Text;
                        dr["OS"] = strOSLS.Text;

                        if (strSLB.Text != "")
                        {
                            List<SalesItem> lstSLB = new List<SalesItem>();
                            lstSLB = salesTrans.GetSLB(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                            strddlSLB.DataSource = lstSLB;
                            strddlSLB.DataTextField = "SlbDesc"; //"supplier_part_number";
                            strddlSLB.DataValueField = "SlbCode";
                            strddlSLB.DataBind();

                            lstSLB = lstSLB.Where(a => a.SlbDesc == strSLB.Text).ToList();

                            if (lstSLB.Count > 0)
                            {
                                dr["SLB"] = lstSLB[0].SlbCode;
                            }
                        }
                        else
                        {
                            dr["SLB"] = strddlSLB.SelectedValue;
                        }

                        dr["SLB_Item"] = strSLB.Text;
                        dr["Branch_ListPrice"] = strBranchListPrice.Text;
                        dr["Cost_Price"] = strCostPrice.Text;
                        dr["SLB_NetValue"] = strSLBNetValue.Text;

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            dr["Coupon"] = strDiscount.Text;
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                dr["Discount"] = strDiscount.Text;
                            else
                                dr["Coupon"] = strDiscount.Text;
                        }

                        dr["Original_Coupon"] = strOrgCoupon.Text;
                        dr["Coupon_Indicator"] = strCouponInd.Value;
                        dr["SalesTax"] = strSalesTax.Text;
                        dr["Gross_Profit"] = strGrossProfit.Text;
                        dr["Item_SaleValue"] = strItemSaleValue.Value;
                        dr["Original_Req_Qty"] = strOriginalReqQty.Value;
                        dt.Rows.Add(dr);
                    }
                }

                dt.Rows.RemoveAt(e.RowIndex);
                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();

                if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                {
                    grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                }
                else
                {
                    if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                        grvItemDetails.HeaderRow.Cells[8].Text = "Discount";
                    else
                        grvItemDetails.HeaderRow.Cells[8].Text = "Coupon/Available";
                }

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList strSupplierName = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierName");
                        TextBox strSupplier = (TextBox)grvItemDetails.Rows[i].FindControl("lblSupplier");
                        TextBox strItemCode = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemCode");
                        DropDownList strddlItemCode = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlItemCode");
                        TextBox strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        TextBox strOSLS = (TextBox)grvItemDetails.Rows[i].FindControl("txtOSLS");
                        TextBox strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        TextBox strCanOrderQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtCanOrderQty");
                        DropDownList strddlSLB = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSLB");
                        TextBox strSLB = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLB");
                        TextBox strBranchListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtBranchListPrice");
                        TextBox strCostPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtCostPrice");
                        TextBox strSLBNetValue = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLBNetValue");
                        TextBox strDiscount = (TextBox)grvItemDetails.Rows[i].FindControl("txtDiscount");
                        TextBox strOrgCoupon = (TextBox)grvItemDetails.Rows[i].FindControl("txtOrgCoupon");
                        HiddenField strCouponInd = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnCouponInd");
                        TextBox strSalesTax = (TextBox)grvItemDetails.Rows[i].FindControl("txtSalesTax");
                        HiddenField txtAddlTax = (HiddenField)grvItemDetails.Rows[i].FindControl("txtAddlTax");
                        TextBox strGrossProfit = (TextBox)grvItemDetails.Rows[i].FindControl("txtGrossProfit");
                        HiddenField strItemSaleValue = (HiddenField)grvItemDetails.Rows[i].FindControl("txtItemSaleValue");
                        HiddenField strOriginalReqQty = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnReqOrderQty");
                        Button btnSearch = (Button)grvItemDetails.Rows[i].FindControl("btnSearch");
                        LinkButton delete = (LinkButton)grvItemDetails.Rows[i].FindControl("lnkDelete");
                        Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");

                        strQty.Attributes.Add("OnChange", "SalesInvoiceQtyChangeGST(" + i + ",'" + strCanOrderQty.ClientID + "','" + strOriginalReqQty.ClientID + "','" + strQty.ClientID + "','" + strddlSLB.ClientID + "','" + strItemSaleValue.ClientID + "','" + strDiscount.ClientID + "');");

                        if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                            strDiscount.Attributes.Add("OnChange", "SalesInvoiceDiscountChange('" + strCanOrderQty.ClientID + "','" + strQty.ClientID + "','" + strItemSaleValue.ClientID + "');");

                        if (Session["BranchCode"].ToString().ToUpper() == "CHE")
                            strSupplierName.DataSource = GetAllSuppliersDispLoc(ddlSecondaryDispLoc.SelectedIndex);
                        else
                            strSupplierName.DataSource = GetAllSuppliers();

                        strSupplierName.DataValueField = "SupplierCode";
                        strSupplierName.DataTextField = "SupplierName";
                        strSupplierName.DataBind();

                        strSLB.Text = dt.Rows[i]["SLB_Item"].ToString();
                        strSupplierName.SelectedValue = dt.Rows[i]["Supplier_Name"].ToString();
                        strSupplier.Text = dt.Rows[i]["Supplier_Item"].ToString();
                        strItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                        strSupplierPartNo.Text = dt.Rows[i]["Part_Number"].ToString();
                        strQty.Text = dt.Rows[i]["Qty"].ToString();
                        strCanOrderQty.Text = dt.Rows[i]["CanOrderQty"].ToString();
                        strOSLS.Text = dt.Rows[i]["OS"].ToString();
                        strBranchListPrice.Text = dt.Rows[i]["Branch_ListPrice"].ToString();
                        strCostPrice.Text = dt.Rows[i]["Cost_Price"].ToString();
                        strSLBNetValue.Text = dt.Rows[i]["SLB_NetValue"].ToString();

                        if (ddlTransactionType.SelectedValue.ToString() == "481" && hdnCostToCostCoupon.Value == "Y")
                        {
                            strDiscount.Text = dt.Rows[i]["Coupon"].ToString();
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "481")
                                strDiscount.Text = dt.Rows[i]["Discount"].ToString();
                            else
                                strDiscount.Text = dt.Rows[i]["Coupon"].ToString();
                        }

                        strOrgCoupon.Text = dt.Rows[i]["Original_Coupon"].ToString();
                        strCouponInd.Value = dt.Rows[i]["Coupon_Indicator"].ToString();
                        strSalesTax.Text = dt.Rows[i]["SalesTax"].ToString();
                        strGrossProfit.Text = dt.Rows[i]["Gross_Profit"].ToString();
                        strItemSaleValue.Value = dt.Rows[i]["Item_SaleValue"].ToString();
                        strOriginalReqQty.Value = dt.Rows[i]["Original_Req_Qty"].ToString();
                        strCostPrice.Visible = false;
                        strSupplier.Visible = false;

                        if (strOrgCoupon.Text == "" || strOrgCoupon.Text == "0.00")
                        {
                            strDiscount.Text = "0.00";
                            strOrgCoupon.Text = "0.00";
                        }

                        if (Convert.ToDecimal(strOrgCoupon.Text.ToString()) > 0 && strCouponInd.Value == "Y")
                            strDiscount.Enabled = true;
                        else
                            strDiscount.Enabled = false;

                        if (ddlSalesReqNumber.SelectedIndex > 0)
                        {
                            strSupplierName.Visible = false;
                            strSupplier.Visible = true;
                            strItemCode.Visible = true;
                            strddlItemCode.Visible = false;
                            btnSearch.Visible = false;
                            
                            List<SalesItem> lstSLB = new List<SalesItem>();
                            lstSLB = salesTrans.GetSLBSalesReq(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                            strddlSLB.DataSource = lstSLB;
                            strddlSLB.DataTextField = "SlbDesc";
                            strddlSLB.DataValueField = "SlbCode";
                            strddlSLB.SelectedValue = dt.Rows[i]["SLB"].ToString();
                            strddlSLB.DataBind();

                            strddlItemCode.Items.Add(new ListItem(dt.Rows[i]["Part_Number"].ToString(), dt.Rows[i]["Item_Code"].ToString()));

                            strddlSLB.Visible = true;
                            strSLB.Visible = false;
                            delete.Visible = true;
                            strSupplierPartNo.Enabled = false;
                            grvItemDetails.Enabled = true;
                            btnAdd.Enabled = false;
                            btnAdd.Visible = false;
                        }
                        else
                        {
                            if (dt.Rows[i]["Item_Code"].ToString() != "")
                            {
                                if (dt.Rows.Count == 1 || i == (dt.Rows.Count - 1))
                                {
                                    btnSearch.Text = "Reset";
                                    strddlItemCode.Visible = true;
                                    strSupplierPartNo.Visible = false;
                                    strCostPrice.Visible = false;
                                    strSupplier.Visible = false;
                                    
                                    List<SalesItem> lstSuppliers = new List<SalesItem>();
                                    ListItem firstListItem = new ListItem();

                                    lstSuppliers = salesTrans.GetItems(strSupplierName.SelectedItem.Value, "", ddlTransactionType.SelectedValue.ToString(), Session["BranchCode"].ToString());
                                    strddlItemCode.DataSource = lstSuppliers;
                                    strddlItemCode.Items.Add(firstListItem);
                                    strddlItemCode.DataTextField = "SupplierPartNumber";
                                    strddlItemCode.DataValueField = "ItemCode";
                                    strddlItemCode.DataBind();

                                    string strSupplierPartNumber = dt.Rows[i]["Part_Number"].ToString();

                                    lstSuppliers = lstSuppliers.Where(a => a.SupplierPartNumber == strSupplierPartNumber).ToList();
                                    if (lstSuppliers.Count > 0)
                                    {
                                        strddlItemCode.SelectedValue = lstSuppliers[0].ItemCode;
                                    }
                                    strddlItemCode.SelectedItem.Text = strSupplierPartNumber;

                                    List<SalesItem> lstSLB = new List<SalesItem>();
                                    lstSLB = salesTrans.GetSLB(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                                    strddlSLB.DataSource = lstSLB;
                                    strddlSLB.DataTextField = "SlbDesc";
                                    strddlSLB.DataValueField = "SlbCode";
                                    strddlSLB.DataBind();

                                    if (dt.Rows[i]["SLB"].ToString() != "")
                                    {
                                        strddlSLB.SelectedValue = dt.Rows[i]["SLB"].ToString();
                                        strddlSLB.Enabled = true;
                                    }
                                }
                                else
                                {
                                    btnSearch.Visible = false;
                                    strSupplierName.Visible = false;
                                    strddlItemCode.Visible = false;
                                    strddlSLB.Visible = false;
                                    strSupplierPartNo.Enabled = false;
                                    strDiscount.Enabled = false;
                                    strSLB.Visible = true;
                                    strQty.Enabled = false;
                                    strSupplier.Visible = true;
                                }
                            }
                        }
                    }
                }

                hdnRowCnt.Value = dt.Rows.Count.ToString();
                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count.ToString();

                if (dt.Rows.Count == 0 && ddlSalesReqNumber.SelectedIndex <= 0)
                {
                    FirstGridViewRow();
                }

                CalculateTotalValue();
                lblMessage.Text = "";
                lblMessage1.Text = "";
                lblPackingQuantity.Text = "";
                lblDepotName.Text = "";
            }
        }

        //protected void ddlModeOfREceipt_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (ddlModeOfREceipt.SelectedValue.ToString() == "M")
        //    {
        //        txtChequeDraftNo.Enabled = false;
        //        txtChequeDraftDt.Enabled = false;
        //        txtBank.Enabled = false;
        //        txtBankBranch.Enabled = false;
        //        txtChequeDraftNo.Text = "";
        //        txtChequeDraftDt.Text = "";
        //        txtBank.Text = "";
        //        txtBankBranch.Text = "";
        //    }
        //    else
        //    {
        //        txtChequeDraftNo.Enabled = true;
        //        txtChequeDraftDt.Enabled = true;
        //        txtBank.Enabled = true;
        //        txtBankBranch.Enabled = true;
        //    }
        //}

        private void CalculateTotalValue()
        {
            decimal totalValue = 0;
            decimal CouponValue = 0;

            if (ddlTransactionType.SelectedValue.Trim() == "071" || ddlTransactionType.SelectedValue.Trim() == "171")
            {
                txtTotalValue.Text = "0.00";
            }
            else
            {
                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    if (gr.Cells.Count > 1)
                    {
                        HiddenField txtItemSaleValue = (HiddenField)gr.Cells[9].FindControl("txtItemSaleValue");
                        TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                        TextBox txtItemCode = (TextBox)gr.Cells[1].FindControl("txtItemCode");
                        TextBox txtDiscount = (TextBox)gr.Cells[7].FindControl("txtDiscount");
                        TextBox txtBranchListPrice = (TextBox)gr.Cells[5].FindControl("txtBranchListPrice");

                        //if (txtItemCode.Text != "")
                        //{
                        //    decimal rate = salesTrans.GetItemRate(txtItemCode.Text);
                        //    CouponValue = CouponValue + Convert.ToDecimal(rate * Convert.ToDecimal(txtQuantity.Text));
                        //}

                        if (txtItemSaleValue != null)
                        {
                            if (txtItemSaleValue.Value != "")
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041")
                                {
                                    if (!(txtDiscount.Text == "" || txtBranchListPrice.Text == ""))
                                    {
                                        if (Convert.ToDecimal(txtDiscount.Text) != 0)
                                        {
                                            totalValue = totalValue + ((Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text)) * (1 + (Convert.ToDecimal(txtDiscount.Text) / 100)));
                                            txtTotalValue.Text = Convert.ToString(totalValue);
                                        }
                                        else
                                        {
                                            totalValue = totalValue + (Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text)) + (((Convert.ToDecimal(txtBranchListPrice.Text) * Convert.ToDecimal(txtQuantity.Text))) * (Convert.ToDecimal(txtDiscount.Text) / 100));
                                            txtTotalValue.Text = Convert.ToString(totalValue);
                                        }
                                    }
                                    else
                                    {
                                        totalValue = totalValue + Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text);
                                        txtTotalValue.Text = Convert.ToString(totalValue);
                                    }
                                }
                                else
                                {
                                    totalValue = totalValue + Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text);
                                    txtTotalValue.Text = Convert.ToString(totalValue);
                                }
                            }
                            else
                            {
                                totalValue = totalValue + 0;
                                txtTotalValue.Text = Convert.ToString(totalValue);
                            }
                        }
                        else
                        {
                            totalValue = totalValue + 0;
                            txtTotalValue.Text = Convert.ToString(totalValue);
                        }
                    }
                    else
                    {
                        txtTotalValue.Text = "";
                        txtCanBillUpTo.Text = "";
                    }
                }
            }

            if (txtTotalValue.Text != "")
            {
                decimal courierCharges = txtCourierCharges.Text == "" ? 0 : Convert.ToDecimal(txtCourierCharges.Text);
                decimal freightCharges = txtFreightAmount.Text == "" ? 0 : Convert.ToDecimal(txtFreightAmount.Text);
                decimal insuranceCharges = txtInsuranceCharges.Text == "" ? 0 : Convert.ToDecimal(txtInsuranceCharges.Text);
                insuranceCharges = (totalValue * insuranceCharges / 100);
                totalValue = totalValue + courierCharges + freightCharges + insuranceCharges - CouponValue;
                txtTotalValue.Text = TwoDecimalConversion(Convert.ToString(totalValue));

                if (!(ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "071" || ddlTransactionType.SelectedValue.ToString() == "171")) //--- Cash Sales, Cash Sales Manual, Free of Cost, Free of Cost Manual
                {
                    txtCanBillUpTo.Text = TwoDecimalConversion(Convert.ToString(Convert.ToDecimal(txtCustomerCreditLimit.Text) - (Convert.ToDecimal(txtCustomerOutStanding.Text)) - totalValue));
                }
                else
                {
                    txtCanBillUpTo.Text = "0";
                    txtTotalValue.Visible = true;
                }
            }

            if (hdnCDstatus.Value == "1")
            {
                if (!(ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.Trim() == "071" || ddlTransactionType.SelectedValue.Trim() == "171" || ddlTransactionType.SelectedValue.ToString() == "481"))
                    ddlCashDiscount.Enabled = true;
                else
                    ddlCashDiscount.Enabled = false;
            }
            else
                ddlCashDiscount.Enabled = false;

            upHeader.Update();

            if (!(ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "071" || ddlTransactionType.SelectedValue.ToString() == "171")) //--- Cash Sales, Cash Sales Manual, Free of Cost, Free of Cost Manual
            {
                double TotalVal = 0;
                double CanBill = 0;

                //TotalVal = Convert.ToDouble(txtTotalValue.Text);

                if (txtCanBillUpTo.Text == "")
                    txtCanBillUpTo.Text = "0.00";

                CanBill = Convert.ToDouble(txtCanBillUpTo.Text);
                decimal CanbillUptoAmt = Convert.ToDecimal(txtCustomerCreditLimit.Text) - Convert.ToDecimal(txtCustomerOutStanding.Text);

                if (Session["BranchCode"].ToString().ToUpper() == "MGT")
                {
                    if (CanBill < 0)
                    {
                        BtnSubmit.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Total Value Exceeds the Can Bill Upto Amount');", true);
                    }
                    else
                        BtnSubmit.Enabled = true;
                }
                else
                {
                    if ((Convert.ToDecimal(ddlCashDiscount.SelectedItem.Text) > 0 && Convert.ToDecimal(txtCustomerOutStanding.Text) < 0)
                        || (ddlTransactionType.SelectedValue.ToString() == "421" || ddlTransactionType.SelectedValue.ToString() == "431"
                        || ddlTransactionType.SelectedValue.ToString() == "441"))
                    {
                        if (CanbillUptoAmt < 0)
                            CanbillUptoAmt = (-1) * CanbillUptoAmt;

                        if ((Convert.ToDecimal(txtTotalValue.Text) <= Convert.ToDecimal(CanbillUptoAmt)) || (ddlTransactionType.SelectedValue.ToString() == "421" || ddlTransactionType.SelectedValue.ToString() == "431"
                        || ddlTransactionType.SelectedValue.ToString() == "441"))
                        {
                            BtnSubmit.Enabled = true;

                            if (grvItemDetails.FooterRow != null)
                            {
                                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                                btnAdd.Enabled = true;
                            }
                        }
                        else
                        {
                            BtnSubmit.Enabled = false;

                            if (grvItemDetails.FooterRow != null)
                            {
                                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                                btnAdd.Enabled = false;
                            }

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Total Value Exceeds the Credit Balance of Rs. " + (-1 * Convert.ToDecimal(txtCustomerOutStanding.Text)) + ". Please Remove an Item to allow the Cash Discount');", true);
                        }
                    }
                    else
                    {
                        if (CanBill < 0)
                        {
                            BtnSubmit.Enabled = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Total Value Exceeds the Can Bill Upto Amount');", true);
                        }
                        else
                            BtnSubmit.Enabled = true;
                    }
                }
            }
        }

        private bool CheckExisting(string partNumber)
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    TextBox txtItemCode = (TextBox)gr.Cells[3].FindControl("txtItemCode");
                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");
                    if (!(ddlSupplierName.Visible))
                    {
                        if (partNumber == txtItemCode.Text)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CheckMultipleTax(string SalesTax)
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    TextBox txtSalesTax = (TextBox)gr.Cells[10].FindControl("txtSalesTax");
                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");
                    if (!(ddlSupplierName.Visible))
                    {
                        if (SalesTax != txtSalesTax.Text)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTransactionType.Enabled = false;
            ServerActionforHeader();
            ddlCustomerName.Enabled = true;
            ddlShippingState.Items.Clear();
            LoadShippingAddressStates("0");
            ddlShippingState.SelectedValue = hdnStateCode.Value;
            FirstGridViewRow();
        }

        protected void ddlCashDiscount_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCashDiscount.SelectedIndex == 0)
            {
                ddlCashDiscount.Enabled = true;
                hdnCDstatus.Value = "1";
            }
            else
            {
                ddlCashDiscount.Enabled = false;
                hdnCDstatus.Value = "0";
            }

            if (ddlSalesReqNumber.SelectedIndex > 0)
                ddlSalesReqNumber_OnSelectedIndexChanged(ddlSalesReqNumber, EventArgs.Empty);
            else
            {
                FirstGridViewRow();
                ddlSalesReqNumber.Focus();
            }
        }

        protected void rdAdvanceIndicator_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdAdvanceIndicator.SelectedValue.ToString() == "N")
            {
                txtAdvReceiptNumber.Attributes.Add("style", "display:none");
                lblAdvReceiptNumber.Attributes.Add("style", "display:none");
                txtAdvReceiptDate.Attributes.Add("style", "display:none");
                lblAdvReceiptDate.Attributes.Add("style", "display:none");
            }
            else
            {
                txtAdvReceiptNumber.Attributes.Add("style", "display:inline");
                lblAdvReceiptNumber.Attributes.Add("style", "display:inline");
                txtAdvReceiptDate.Attributes.Add("style", "display:inline");
                lblAdvReceiptDate.Attributes.Add("style", "display:inline");
            }
        }
    }
}