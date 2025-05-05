using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Transactions;
using System.Configuration;

namespace IMPALWeb
{
    public partial class InwardEntry : System.Web.UI.Page
    {
        //private string ScreenMode = "A";//"A--Add Mode,E--Edit Mode,V--View Mode"
        Decimal sumCostPrice = 0;
        Decimal sumListPrice = 0;
        Decimal sumCoupon = 0;
        Decimal sumItemTaxValue = 0;
        string inwardStatusDuringEdit = string.Empty;
        string inwardStatusDuringAdd = string.Empty;
        InwardTransactions inwardTransactions = new InwardTransactions();
        Suppliers suppliers = new Suppliers();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;

                    txtSupplierGSTIN.Text = "";
                    txtSupplierGSTIN.Enabled = false;

                    hdnScreenMode.Value = "A";
                    hdnSecondSales.Value = "0";
                    txtInwardDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtSupplierGSTIN.Attributes.Add("autocomplete", "off");
                    txtDcDate.Text = string.Empty;
                    txtLRDate.Text = string.Empty;
                    txtRoadPermitDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtReceivedDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlOSIndicator.Enabled = false;

                    txtSGSTValue.Text = "0.00";
                    txtCGSTValue.Text = "0.00";
                    txtIGSTValue.Text = "0.00";
                    txtUTGSTValue.Text = "0.00";
                    txtTCSValue.Text = "0.00";

                    FirstGridViewRow();

                    ddlInwardNumber.Visible = false;

                    hdnSelItemCode.Value = "";

                    if (Session["BranchCode"].ToString().ToUpper() == "CHG")
                    {
                        lblSGSTValue.Attributes.Add("Style", "display:none");
                        txtSGSTValue.Attributes.Add("Style", "display:none");
                        lblUTGSTValue.Attributes.Add("Style", "display:inline");
                        txtUTGSTValue.Attributes.Add("Style", "display:inline");
                    }
                    else
                    {
                        lblSGSTValue.Attributes.Add("Style", "display:inline");
                        txtSGSTValue.Attributes.Add("Style", "display:inline");
                        lblUTGSTValue.Attributes.Add("Style", "display:none");
                        txtUTGSTValue.Attributes.Add("Style", "display:none");
                    }
                    
                    ddlTransactionType.Enabled = true;
                    ddlSupplierName.Enabled = true;

                    ddlSupplyPlant.Items.Add(new ListItem("--Select--", "0"));

                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();

                    txtSGSTValue.Enabled = false;
                    txtUTGSTValue.Enabled = false;
                    txtCGSTValue.Enabled = false;
                    txtIGSTValue.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return InwardEntrySubmit();");
                ddlTransactionType.Attributes.Add("OnChange", "funTransTYpeChange();");
                ddlOSIndicator.Attributes.Add("OnChange", "funOSIndicatorChange();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");

                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                    hdnFooterCoupon.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    hdnFooterTaxPrice.Value = grvItemDetails.FooterRow.Cells[14].ClientID;                    
                }                

                trRoadPermitDetails.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    ddlInwardNumber.SelectedValue = "0";
                    ddlInwardNumber.Visible = true;
                    txtSupplierGSTIN.Enabled = true;
                    txtInwardNumber.Visible = false;

                    ddlTransactionType.Enabled = false;
                    ddlSupplierName.Enabled = false;
                    ddlBranch.Enabled = false;
                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlInwardNumber.Visible = false;
                    txtSupplierGSTIN.Enabled = false;
                    txtInwardNumber.Visible = true;

                    ddlTransactionType.Enabled = true;
                    ddlSupplierName.Enabled = true;
                    ddlBranch.Enabled = true;
                    txtInvoiceValue.Enabled = true;
                }

                txtSupplierGSTIN.Text = string.Empty;
                txtInwardNumber.Text = string.Empty;
                txtInwardDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ddlTransactionType.SelectedValue = "0";
                ddlSupplierName.SelectedValue = "0";

                txtDCNumber.Text = string.Empty;
                txtDcDate.Text = string.Empty;

                txtLRNumber.Text = string.Empty;
                txtLRDate.Text = string.Empty;
                txtCarrier.Text = string.Empty;
                txtPlaceOfDespatch.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtNoOfCases.Text = string.Empty;
                txtRoadPermitNo.Text = string.Empty;
                txtRoadPermitDate.Text = string.Empty;

                txtFreightAmount.Text = string.Empty;
                txtFreightTax.Text = string.Empty;
                txtInsurance.Text = string.Empty;
                txtPostalCharges.Text = string.Empty;
                txtCouponCharges.Text = string.Empty;

                txtInvoiceNo.Text = string.Empty;
                txtInvoiceDate.Text = string.Empty;
                txtReceivedDate.Text = string.Empty;
                txtSGSTValue.Text = string.Empty;
                txtInvoiceValue.Text = string.Empty;
                ddlOSIndicator.SelectedValue = "O";
                ddlReasonForReturn.SelectedValue = "0";
                FirstGridViewRow();

                BtnSubmit.Attributes.Add("OnClick", "return InwardEntrySubmit();");

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                
                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                    hdnFooterCoupon.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    hdnFooterTaxPrice.Value = grvItemDetails.FooterRow.Cells[14].ClientID;
                }

                FreezeOrUnFreezeButtons(true);

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("InwardEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
          
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("InwardEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {                
                string InvoiceNo = "";
                
                if (hdnScreenMode.Value.ToUpper() == "A")
                {
                    InvoiceNo = inwardTransactions.CheckInvoiceExists(ddlSupplierName.SelectedValue, txtInwardDate.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, Session["BranchCode"].ToString());

                    if (InvoiceNo != "")
                    {
                        txtInvoiceNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number already exists...');", true);

                        FirstGridViewRow();

                        if (grvItemDetails.FooterRow != null)
                        {
                            Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                            if (btnAddRow != null)
                                btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                        }
                    }
                    else
                    {
                        AddNewRowForGrid();
                    }
                }
                else if (hdnScreenMode.Value.ToUpper() == "E")
                {
                    InvoiceNo = inwardTransactions.CheckInvoiceExistsEdit(ddlSupplierName.SelectedValue, txtInwardDate.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, Session["BranchCode"].ToString(), ddlInwardNumber.SelectedValue);

                    if (InvoiceNo != "")
                    {
                        txtInvoiceNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number already exists...');", true);
                        return;
                    }

                    AddNewRowForGrid();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        private void AddNewRowForGrid()
        {
            AddNewRow();

            if (grvItemDetails.FooterRow != null)
            {
                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
            }
        }

        protected void ddlCCWHNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;

                DropDownList selCCWHNo = (DropDownList)grvItemDetails.Rows[row.RowIndex].FindControl("ddlCCWHNo");
                int rowIndex = row.RowIndex;

                TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                txtListPrice.Text = TwoDecimalConversion("");

                TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");
                txtPOQuantity.Text = DecimalToIntConversion("");

                TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");
                txtRcvdQty.Text = DecimalToIntConversion("");

                TextBox txtBalanceQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");
                txtBalanceQty.Text = DecimalToIntConversion("");

                TextBox txtQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");
                txtQty.Text = DecimalToIntConversion("");

                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                txtItemCode.Text = "";

                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                txtCstPricePerQty.Text = TwoDecimalConversion("");

                TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                txtCostPrice.Text = "0.00";

                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");
                txtItemCoupon.Text = TwoDecimalConversion("");

                TextBox txtListLessDiscount = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                txtListLessDiscount.Text = TwoDecimalConversion("");

                //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                //txtEDIndicator.Text = "";

                //TextBox txtEDValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");
                //txtEDValue.Text = TwoDecimalConversion("");

                TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                txtItemLocation.Text = "";

                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                txtTaxPercentage.Text = "";

                TextBox txtItemTaxValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");
                txtItemTaxValue.Text = "";

                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[row.RowIndex].FindControl("ddlItemCode");
                
                ddlItemCode.DataSource = (object)GetItemCode(selCCWHNo.SelectedValue, ddlInwardNumber.SelectedValue);
                ddlItemCode.DataTextField = "ItemDesc";
                ddlItemCode.DataValueField = "ItemCode";
                ddlItemCode.DataBind();

                if (selCCWHNo.SelectedValue == "0")
                {
                    return;
                }

                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[row.RowIndex].FindControl("txtSupplierPartNo");

                TextBox txtCCWHNo = null;
                TextBox txtSerialNo = null;
                TextBox txtPONumber = null;
                DropDownList ddlCCWHNo = null;
                string[] listitemArr;
                string strArray = "";

                foreach (ListItem li in ddlItemCode.Items)
                {
                    if (li.Value.ToString() == "")
                        continue;

                    foreach (GridViewRow gvr in grvItemDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            //txtExistingItem = (TextBox)gvr.FindControl("txtItemCode");

                            txtCCWHNo = (TextBox)gvr.FindControl("txtCCWHNo");
                            txtSerialNo = (TextBox)gvr.FindControl("txtSerialNo");
                            txtPONumber = (TextBox)gvr.FindControl("txtPONumber");
                            ddlCCWHNo = (DropDownList)gvr.FindControl("ddlCCWHNo");
                            listitemArr = li.Value.Split('_');

                            if (txtSerialNo.Text == listitemArr[1] && txtPONumber.Text == listitemArr[2] && (selCCWHNo.SelectedValue == txtCCWHNo.Text || selCCWHNo.SelectedValue == ddlCCWHNo.SelectedValue))
                            {
                                strArray += li.Value.ToString() + "|";
                            }
                        }
                    }
                }

                string[] strArr = strArray.Split('|');

                foreach (string str in strArr)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;

                    ListItem li = ddlItemCode.Items.FindByValue(str);
                    ddlItemCode.Items.Remove(li);
                }

                txtSupplierPartNo.Visible = false;
                ddlItemCode.Visible = true;

                //newItemCode.AutoPostBack = true;
                //ddlItemCode.Attributes.Add("OnChange", "return ChkDuplicateItems('" + ddlItemCode.ClientID + "');");

                ShowSummaryInFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void ddlItemCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                DropDownList selectedCCWHNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCCWHNo");
                DropDownList selectedItemCode = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlItemCode");                

                if (selectedItemCode.SelectedIndex == 0 || selectedItemCode.SelectedValue == "")
                {
                    TextBox txtListPrice1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                    txtListPrice1.Text = "";

                    TextBox txtPOQuantity1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");
                    txtPOQuantity1.Text = "";

                    TextBox txtRcvdQty1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");
                    txtRcvdQty1.Text = "";

                    TextBox txtBalanceQty1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");
                    txtBalanceQty1.Text = "";

                    TextBox txtQty1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");
                    txtQty1.Text = "";

                    TextBox txtItemCode1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                    txtItemCode1.Text = "";

                    TextBox txtCstPricePerQty1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                    txtCstPricePerQty1.Text = "";

                    TextBox txtCostPrice1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                    txtCostPrice1.Text = "0.00";

                    TextBox txtCoupon1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");
                    txtCoupon1.Text = "";

                    TextBox txtListLessDiscount1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                    txtListLessDiscount1.Text = "";

                    //TextBox txtEDIndicator1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                    //txtEDIndicator1.Text = "";

                    //TextBox txtEDValue1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");
                    //txtEDValue1.Text = TwoDecimalConversion("");

                    TextBox txtItemLocation1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                    txtItemLocation1.Text = "";

                    TextBox txtTaxPercentage1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                    txtTaxPercentage1.Text = "";

                    TextBox txtItemTaxValue1 = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");
                    txtItemTaxValue1.Text = "";

                    return;
                }
                else
                {
                    InwardItem inwardItem = new InwardItem();
                    string[] itemCodeArr = selectedItemCode.SelectedValue.Split('_');
                    inwardItem.ItemCode = itemCodeArr[0].ToString();

                    int ItemCnt = 0;
                    ItemCnt = inwardTransactions.InwardItemsLocation(inwardItem.ItemCode, ddlBranch.SelectedValue.ToString());

                    if (ItemCnt <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward item code " + inwardItem.ItemCode + " is missing in Item_Location table');", true);
                        selectedItemCode.SelectedIndex = 0;
                    }
                    else
                    {
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                        txtSupplierPartNo.Text = selectedItemCode.SelectedValue;

                        if (ddlTransactionType.SelectedValue != "171")
                        {
                            if (itemCodeArr.Count() > 1)
                            {
                                inwardItem.SerialNo = itemCodeArr[1];
                                inwardItem.PONumber = itemCodeArr[2];
                            }
                        }
                        else
                        {
                            inwardItem.SerialNo = "0";
                            inwardItem.PONumber = "0";
                        }

                        inwardItem.TransactionTypeCode = ddlTransactionType.SelectedValue;
                        inwardItem.BrachCode = Session["BranchCode"].ToString();
                        inwardItem.InvoiceDate = txtInvoiceDate.Text.ToString();
                        inwardItem.OSLSIndicator = ddlOSIndicator.SelectedValue;

                        inwardItem = inwardTransactions.GetItemInformation(inwardItem);

                        TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");
                        txtPOQuantity.Text = DecimalToIntConversion(inwardItem.POQuantity.ToString());

                        TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");
                        txtRcvdQty.Text = DecimalToIntConversion(inwardItem.ReceivedQuantity.ToString());

                        TextBox txtBalanceQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");
                        txtBalanceQty.Text = DecimalToIntConversion(inwardItem.BalanceQuantity.ToString());

                        TextBox txtQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");
                        txtQty.Text = DecimalToIntConversion(inwardItem.Quantity.ToString());
                        txtQty.Focus();

                        HiddenField txtExistingQty = (HiddenField)grvItemDetails.Rows[rowIndex].FindControl("txtExistingQty");

                        if (txtRcvdQty.Text == "0")
                            txtExistingQty.Value = "0";
                        else
                            txtExistingQty.Value = txtRcvdQty.Text;

                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        txtItemCode.Text = Convert.ToString(inwardItem.ItemCode);

                        TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSerialNo");
                        txtSerialNo.Text = Convert.ToString(inwardItem.SerialNo);

                        TextBox txtPONumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPONumber");
                        txtPONumber.Text = Convert.ToString(inwardItem.PONumber);

                        if (ddlTransactionType.SelectedValue == "171" && Convert.ToDecimal(txtInvoiceValue.Text) == 0)
                        {
                            TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                            txtListPrice.Text = "0.00";

                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            txtCstPricePerQty.Text = "0.00";
                        }
                        else
                        {
                            TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                            txtListPrice.Text = FourDecimalConversion(inwardItem.ListPrice.ToString());

                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            txtCstPricePerQty.Text = FourDecimalConversion(inwardItem.CostPricePerQty.ToString());
                        }

                        TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                        txtCostPrice.Text = "0.00";

                        TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");
                        txtItemCoupon.Text = TwoDecimalConversion(inwardItem.Coupon.ToString());

                        TextBox txtListLessDiscount = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                        txtListLessDiscount.Text = TwoDecimalConversion(inwardItem.ListLessDiscount.ToString());

                        //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                        //txtEDIndicator.Text = inwardItem.EDIndicator.ToString();

                        //TextBox txtEDValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");
                        //txtEDValue.Text = TwoDecimalConversion(inwardItem.EDValue.ToString());

                        TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        txtItemLocation.Text = inwardItem.ItemLocation.ToString();

                        TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                        //if (ddlTransactionType.SelectedValue != "171")
                        //{
                        txtTaxPercentage.Text = inwardItem.ItemTaxPercentage.ToString();
                        //}
                        //else
                        //{
                        //    txtTaxPercentage.Text = "0.00";
                        //}

                        TextBox txtItemTaxValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");
                        txtItemTaxValue.Text = "";

                        txtQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + inwardItem.CostPricePerQty.ToString() + "','" + txtExistingQty.ClientID + "');");
                        txtTaxPercentage.Attributes.Add("OnChange", "return funCalculateTaxValue('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + inwardItem.CostPricePerQty.ToString() + "','" + txtExistingQty.ClientID + "');");
                        txtItemCoupon.Attributes.Add("OnChange", "return funCalculateCoupon('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtItemCoupon.ClientID + "','" + inwardItem.CostPricePerQty.ToString() + "','" + txtExistingQty.ClientID + "');");

                        ShowSummaryInFooter();

                        ScriptManager.RegisterStartupScript(this.Page.Master, this.Master.GetType(), "IMPAL", "funSetFocusOnQty('" + txtQty.ClientID + "');", true);

                        //txtQty.Focus();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void ddlInwardNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInwardNumber.SelectedValue != "0")
                {
                    InwardEntity inwardEntity = inwardTransactions.GetInwardDetails(ddlInwardNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    if (inwardEntity != null)
                    {
                        txtInwardNumber.Text = inwardEntity.InwardNumber;
                        txtInwardDate.Text = inwardEntity.InwardDate;
                        ddlTransactionType.SelectedValue = inwardEntity.TransactionTypeCode;

                        ddlSupplierName.DataSourceID = null;

                        ddlSupplierName.DataSource = suppliers.GetAllSuppliers();
                        ddlSupplierName.DataValueField = "SupplierCode";
                        ddlSupplierName.DataTextField = "SupplierName";
                        ddlSupplierName.DataBind();

                        ddlSupplierName.SelectedValue = inwardEntity.SupplierCode;
                        ddlBranch.SelectedValue = inwardEntity.BranchCode;
                        txtSupplierGSTIN.Text = inwardEntity.SupplierGSTIN;

                        txtDCNumber.Text = inwardEntity.DCNumber;
                        txtDcDate.Text = inwardEntity.DCDate;

                        txtLRNumber.Text = inwardEntity.LRNumber;
                        txtLRDate.Text = inwardEntity.LRDate;
                        txtCarrier.Text = inwardEntity.Carrier;
                        txtPlaceOfDespatch.Text = inwardEntity.PlaceOfDespatch;
                        txtWeight.Text = inwardEntity.Weight;
                        txtNoOfCases.Text = inwardEntity.NoOfCases;
                        txtRoadPermitNo.Text = inwardEntity.RoadPermitNumber;
                        txtRoadPermitDate.Text = inwardEntity.RoadPermitDate;

                        txtFreightAmount.Text = inwardEntity.FreightAmount;
                        txtFreightTax.Text = inwardEntity.FreightTax;
                        txtInsurance.Text = inwardEntity.Insurance;
                        txtPostalCharges.Text = inwardEntity.PostalCharges;
                        txtCouponCharges.Text = inwardEntity.CouponCharges;

                        txtInvoiceNo.Text = inwardEntity.InvoiceNumber;
                        txtInvoiceDate.Text = inwardEntity.InvoiceDate;
                        txtReceivedDate.Text = inwardEntity.ReceivedDate;

                        txtSGSTValue.Text = inwardEntity.SGSTValue;
                        txtCGSTValue.Text = inwardEntity.CGSTValue;
                        txtIGSTValue.Text = inwardEntity.IGSTValue;
                        txtUTGSTValue.Text = inwardEntity.UTGSTValue;
                        txtTCSValue.Text = inwardEntity.TCSValue;

                        txtInvoiceValue.Text = inwardEntity.InvoiceValue;
                        hdnDocStatus.Value = inwardEntity.Status;

                        ddlSupplyPlant.DataSource = (object)GetSupplyplantsEdit(inwardEntity.SupplyPlantCode);
                        ddlSupplyPlant.DataTextField = "ItemDesc";
                        ddlSupplyPlant.DataValueField = "ItemCode";
                        ddlSupplyPlant.DataBind();
                        ddlSupplyPlant.SelectedValue = inwardEntity.SupplyPlantCode;

                        if (inwardEntity.Status == "E")
                        {
                            txtInvoiceValue.Enabled = true;

                            if (inwardEntity.OSIndicator.ToUpper() == "O")
                            {
                                txtSGSTValue.Enabled = false;
                                txtUTGSTValue.Enabled = false;
                                txtCGSTValue.Enabled = false;
                                txtIGSTValue.Enabled = true;
                            }
                            else
                            {
                                txtSGSTValue.Enabled = true;
                                txtUTGSTValue.Enabled = true;
                                txtCGSTValue.Enabled = true;
                                txtIGSTValue.Enabled = false;
                            }

                            //if (inwardEntity.OSIndicator.ToUpper() == "L")
                            //{
                            //    txtSGSTValue.Enabled = true;
                            //    txtCGSTValue.Enabled = true;
                            //    txtIGSTValue.Enabled = false;
                            //    txtUTGSTValue.Enabled = false;
                            //}
                            //else
                            //{
                            //    txtSGSTValue.Enabled = false;
                            //    txtCGSTValue.Enabled = false;
                            //    txtIGSTValue.Enabled = true;
                            //    txtUTGSTValue.Enabled = true;
                            //}
                        }
                        else
                        {
                            txtInvoiceValue.Enabled = false;
                            txtSGSTValue.Enabled = false;
                            txtCGSTValue.Enabled = false;
                            txtIGSTValue.Enabled = true;
                            txtUTGSTValue.Enabled = true;
                        }

                        ddlOSIndicator.SelectedValue = inwardEntity.OSIndicator.ToUpper();
                        inwardStatusDuringEdit = inwardEntity.Status;

                        string FreightChargesStatus = inwardTransactions.GetFreightChargesStatus(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                        if (FreightChargesStatus == "1")
                        {
                            txtFreightAmount.Enabled = true;
                            txtFreightTax.Enabled = true;
                        }
                        else
                        {
                            txtFreightAmount.Text = "";
                            txtFreightTax.Text = "";
                            txtFreightAmount.Enabled = false;
                            txtFreightTax.Enabled = false;
                        }

                        BindExistingRowsDuringEdit(inwardEntity.Items);
                        ddlTransactionType.Enabled = false;
                        ddlSupplierName.Enabled = false;
                        ddlBranch.Enabled = false;

                        FreezeOrUnFreezeButtons(true);
                        UpdpanelTop.Update();
                        UpdPanelGrid.Update();

                        string ItemCodes = string.Empty;
                        ItemCodes = inwardTransactions.InwardItemsLocationEdit(ddlInwardNumber.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString());

                        Button btnAddRow = null;

                        if (grvItemDetails.FooterRow != null)
                            btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");

                        if (ItemCodes != "" && ItemCodes != null)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward item codes " + ItemCodes + " are missing in Item_Location table');", true);

                            if (btnAddRow != null)
                                btnAddRow.Enabled = false;

                            BtnSubmit.Enabled = false;
                        }
                        else
                        {
                            if (btnAddRow != null)
                                btnAddRow.Enabled = true;

                            BtnSubmit.Enabled = true;
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
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string InvoiceNo = inwardTransactions.CheckInvoiceExistsEdit(ddlSupplierName.SelectedValue, txtInwardDate.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, Session["BranchCode"].ToString(), ddlInwardNumber.SelectedValue);

                if (InvoiceNo != "")
                {
                    txtInvoiceNo.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number already exists');", true);
                    return;
                }

                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void txtSupplierGSTIN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierGSTIN.Text.Trim() != "")
                {
                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();
                }
                else
                {
                    ddlSupplyPlant.Items.Clear();
                    ddlSupplyPlant.Items.Add(new ListItem("--Select--", "0"));
                }

                hdnSecondSales.Value = "0";

                if (ddlSupplierName.SelectedIndex == 0)
                    txtSupplierGSTIN.Enabled = false;
                else
                    txtSupplierGSTIN.Enabled = true;

                if (!ddlInwardNumber.Visible)
                {
                    FirstGridViewRow();

                    if (grvItemDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                        if (btnAddRow != null)
                            btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplierName.SelectedIndex == 0)
                    txtSupplierGSTIN.Enabled = false;
                else
                    txtSupplierGSTIN.Enabled = true;

                txtSupplierGSTIN.Text = "";
                ddlSupplyPlant.Items.Clear();
                ddlSupplyPlant.Items.Add(new ListItem("--Select--", "0"));

                if (!ddlInwardNumber.Visible)
                {
                    FirstGridViewRow();

                    if (grvItemDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                        if (btnAddRow != null)
                            btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                    }
                }
                
                string FreightChargesStatus = inwardTransactions.GetFreightChargesStatus(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                if (FreightChargesStatus == "1")
                {
                    txtFreightAmount.Enabled = true;
                    txtFreightTax.Enabled = true;
                }
                else
                {
                    txtFreightAmount.Text = "";
                    txtFreightTax.Text = "";
                    txtFreightAmount.Enabled = false;
                    txtFreightTax.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void ddlTransactionType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                ddlSupplierName.Items.Clear();
                ddlSupplierName.DataSourceID = null;                

                if (ddlTransactionType.SelectedValue == "171")
                {
                    ddlSupplierName.DataSource = suppliers.GetAllSuppliersGST();
                    ddlSupplierName.DataValueField = "SupplierCode";
                    ddlSupplierName.DataTextField = "SupplierName";
                    ddlSupplierName.DataBind();
                }
                else
                {
                    ddlSupplierName.DataSource = suppliers.GetAllSuppliersManualGRN(ddlBranch.SelectedValue);
                    ddlSupplierName.DataValueField = "SupplierCode";
                    ddlSupplierName.DataTextField = "SupplierName";
                    ddlSupplierName.DataBind();
                }

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }
        protected void ddlSupplyPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplyPlant.SelectedIndex > 0)
                {
                    string OsLsIndicator = inwardTransactions.SupplyPlantInterStateStatus(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                    ddlOSIndicator.SelectedValue = OsLsIndicator;

                    txtSGSTValue.Text = "0.00";
                    txtUTGSTValue.Text = "0.00";
                    txtCGSTValue.Text = "0.00";
                    txtIGSTValue.Text = "0.00";
                    txtTCSValue.Text = "0.00";

                    if (OsLsIndicator == "O")
                    {
                        txtSGSTValue.Enabled = false;
                        txtUTGSTValue.Enabled = false;
                        txtCGSTValue.Enabled = false;
                        txtIGSTValue.Enabled = true;
                    }
                    else
                    {
                        txtSGSTValue.Enabled = true;
                        txtUTGSTValue.Enabled = true;
                        txtCGSTValue.Enabled = true;
                        txtIGSTValue.Enabled = false;
                    }
                }

                if (hdnScreenMode.Value == "A" && ddlInwardNumber.Visible == false)
                {
                    FirstGridViewRow();

                    if (grvItemDetails.FooterRow != null)
                    {
                        Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                        if (btnAddRow != null)
                            btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    List<CCWH> obj = new List<CCWH>();
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlCCWH = (DropDownList)e.Row.FindControl("ddlCCWHNo");
                        DropDownList ddlItemCode = (DropDownList)e.Row.FindControl("ddlItemCode");

                        if (ddlTransactionType.SelectedValue == "171")//Free of cost
                        {

                            CCWH ccwh = new CCWH();
                            ccwh.PONumber = "0";
                            ccwh.CCWHNumber = "--Not Required--";
                            obj.Add(ccwh);

                            ddlCCWH.DataTextField = "CCWHNumber";
                            ddlCCWH.DataValueField = "PONumber";
                            ddlCCWH.DataSource = obj;
                            ddlCCWH.DataBind();
                            ddlCCWH.Enabled = false;

                            ddlItemCode.DataSource = (object)GetFOCItemCodes();
                            ddlItemCode.DataTextField = "ItemDesc";
                            ddlItemCode.DataValueField = "ItemCode";
                            ddlItemCode.DataBind();

                            //grvItemDetails.Columns[3].Visible = false;
                            //grvItemDetails.Columns[4].Visible = false;
                            //grvItemDetails.Columns[5].Visible = false;
                            //grvItemDetails.Columns[7].Visible = false;
                            //grvItemDetails.Columns[8].Visible = false;
                            //grvItemDetails.Columns[9].Visible = false;
                            //grvItemDetails.Columns[10].Visible = false;
                            //grvItemDetails.Columns[11].Visible = false;
                            //grvItemDetails.Columns[12].Visible = false;
                            //grvItemDetails.Columns[13].Visible = false;
                            //grvItemDetails.Columns[14].Visible = false;
                            //grvItemDetails.Columns[15].Visible = false;
                        }
                        else
                        {
                            if ((ddlSupplierName.SelectedValue != "0") && (ddlBranch.SelectedValue != "0") && (ddlTransactionType.SelectedValue != "0"))
                                obj = GetCCWHNo(ddlSupplierName.SelectedValue, ddlBranch.SelectedValue, ddlTransactionType.SelectedValue);

                            ddlCCWH.DataTextField = "CCWHNumber";
                            ddlCCWH.DataValueField = "PONumber";
                            ddlCCWH.DataSource = obj;
                            ddlCCWH.DataBind();
                            ddlCCWH.Enabled = true;

                            grvItemDetails.Columns[3].Visible = true;
                            grvItemDetails.Columns[4].Visible = true;
                            grvItemDetails.Columns[5].Visible = true;
                            grvItemDetails.Columns[7].Visible = true;
                            grvItemDetails.Columns[8].Visible = true;
                            grvItemDetails.Columns[9].Visible = true;
                            grvItemDetails.Columns[10].Visible = true;
                            grvItemDetails.Columns[11].Visible = true;
                            grvItemDetails.Columns[12].Visible = true;
                            grvItemDetails.Columns[13].Visible = true;
                            grvItemDetails.Columns[14].Visible = true;
                            grvItemDetails.Columns[15].Visible = true;
                        }

                        //if (inwardStatusDuringAdd == "" && inwardStatusDuringEdit == "E")
                        //{
                            //e.Row.Cells[16].Enabled = false;
                        //}

                        if (inwardStatusDuringAdd == "E")
                        {
                            //if (e.Row.RowIndex + 1 == Convert.ToInt16(hdnRowCnt.Value))
                                e.Row.Cells[16].Enabled = true;
                           // else
                                //e.Row.Cells[16].Enabled = false;
                        }
                        
                        //ddlItemCode.Attributes.Add("OnChange", "return ChkDuplicateItems('" + ddlItemCode.ClientID + "');");
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        protected void grvItemDetails_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["CurrentTable"] as DataTable;
                FillLastRowInfoInGrid(ref dt);
                dt.Rows[index].Delete();
                inwardStatusDuringEdit = hdnScreenMode.Value;

                if (dt.Rows.Count > 0)
                {
                    int NewSerialNo = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[0] = NewSerialNo.ToString();
                        NewSerialNo += 1;
                    }

                    int LastRow = dt.Rows.Count - 1;

                    if (
                        (string.IsNullOrEmpty(dt.Rows[LastRow][1].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][2].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][3].ToString())) &&
                        (string.IsNullOrEmpty(dt.Rows[LastRow][4].ToString())))
                    {
                        dt.Rows[LastRow].Delete();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        ViewState["CurrentTable"] = dt;
                        ViewState["GridRowCount"] = dt.Rows.Count;
                        hdnRowCnt.Value = dt.Rows.Count.ToString();

                        grvItemDetails.DataSource = dt;
                        grvItemDetails.DataBind();

                        SetPreviousData();
                        HideDllItemCodeDropDownForDisplayOnly(inwardStatusDuringEdit);
                        ShowSummaryInFooter();
                    }
                    else
                    {
                        FirstGridViewRow();
                    }
                }
                else
                {
                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        private void RemoveDuplicates()
        {
            TextBox txtExistingItem = null;
            string strArray = "";
            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[grvItemDetails.Rows.Count - 1].FindControl("ddlItemCode");

            foreach (GridViewRow gvr in grvItemDetails.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    txtExistingItem = (TextBox)gvr.FindControl("txtItemCode");
                    //if (txtExistingItem.Text.Trim() == li.Value.ToString())
                    //{
                    strArray += txtExistingItem.Text.Trim() + "|";
                    //}
                }
            }

            string[] strArr = strArray.Split('|');

            foreach (string str in strArr)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                ListItem li = ddlItemCode.Items.FindByValue(str);
                ddlItemCode.Items.Remove(li);
            }

            ShowSummaryInFooter();

        }

        private List<IMPALLibrary.Transactions.Item> GetItemCode(string PoNumber, string InwardNumber)
        {
            return inwardTransactions.GetItemCode(PoNumber, InwardNumber, Session["BranchCode"].ToString());
        }

        private void SubmitHeaderAndItems()
        {
            InwardEntity inwardEntity = new InwardEntity();
            inwardEntity.Items = new List<InwardItem>();

            if (ddlInwardNumber.Visible == true)
                inwardEntity.InwardNumber = ddlInwardNumber.SelectedValue;
            else
                inwardEntity.InwardNumber = txtInwardNumber.Text;

            inwardEntity.InwardDate = txtInwardDate.Text;
            inwardEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
            inwardEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
            inwardEntity.BranchCode = ddlBranch.SelectedValue.ToString();

            inwardEntity.DCNumber = txtDCNumber.Text;
            inwardEntity.DCDate = txtDcDate.Text;

            inwardEntity.LRNumber = txtLRNumber.Text;
            inwardEntity.LRDate = txtLRDate.Text;
            inwardEntity.Carrier = txtCarrier.Text;
            inwardEntity.PlaceOfDespatch = txtPlaceOfDespatch.Text;
            inwardEntity.Weight = txtWeight.Text;
            inwardEntity.NoOfCases = txtNoOfCases.Text;
            inwardEntity.RoadPermitNumber = txtRoadPermitNo.Text;
            inwardEntity.RoadPermitDate = txtRoadPermitDate.Text;

            inwardEntity.FreightAmount = txtFreightAmount.Text;
            inwardEntity.FreightTax = txtFreightTax.Text;
            inwardEntity.Insurance = txtInsurance.Text;
            inwardEntity.PostalCharges = txtPostalCharges.Text;
            inwardEntity.CouponCharges = txtCouponCharges.Text;

            inwardEntity.InvoiceNumber = txtInvoiceNo.Text;
            inwardEntity.InvoiceDate = txtInvoiceDate.Text;
            inwardEntity.ReceivedDate = txtReceivedDate.Text;
            inwardEntity.SGSTValue = txtSGSTValue.Text;
            inwardEntity.CGSTValue = txtCGSTValue.Text;
            inwardEntity.IGSTValue = txtIGSTValue.Text;
            inwardEntity.UTGSTValue = txtUTGSTValue.Text;
            inwardEntity.InvoiceValue = txtInvoiceValue.Text;
            inwardEntity.TCSValue = txtTCSValue.Text;
            inwardEntity.SupplyPlantCode = ddlSupplyPlant.SelectedValue;
            inwardEntity.OSIndicator = ddlOSIndicator.SelectedValue;
            inwardEntity.ReasonForReturn = ddlReasonForReturn.SelectedValue;
            inwardEntity.Status = hdnScreenMode.Value;

            InwardItem inwardItem = null;
            int SNo = 0;
            decimal dmlTotal = 0;
            decimal dmlTotalCoupon = 0;
            decimal dmlTotalTax = 0;

            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                inwardItem = new InwardItem();
                SNo += 1;

                DropDownList ddlCCWHNo = (DropDownList)gr.FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)gr.FindControl("txtCCWHNo");

                DropDownList ddlItemCode = (DropDownList)gr.FindControl("ddlItemCode");
                TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");

                TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");

                TextBox txtPOQuantity = (TextBox)gr.FindControl("txtPOQuantity");
                TextBox txtRcvdQty = (TextBox)gr.FindControl("txtRcvdQty");
                TextBox txtBalanceQty = (TextBox)gr.FindControl("txtBalanceQty");
                TextBox txtQty = (TextBox)gr.FindControl("txtQty");

                TextBox txtListPrice = (TextBox)gr.FindControl("txtListPrice");
                TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");

                dmlTotal += Convert.ToDecimal(txtCostPrice.Text);

                TextBox txtItemCoupon = (TextBox)gr.FindControl("txtItemCoupon");

                TextBox txtListLessDiscount = (TextBox)gr.FindControl("txtListLessDiscount");
                TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                TextBox txtSerialNo = (TextBox)gr.FindControl("txtSerialNo");
                TextBox txtPONumber = (TextBox)gr.FindControl("txtPONumber");
                TextBox txtTaxPercentage = (TextBox)gr.FindControl("txtTaxPercentage");
                TextBox txtItemTaxValue = (TextBox)gr.FindControl("txtItemTaxValue");

                //if (ddlCCWHNo.Visible)
                //    inwardItem.CCWHNO = ddlCCWHNo.SelectedValue.ToString();
                //else
                //    inwardItem.CCWHNO = txtCCWHNo.Text.ToString();

                if (txtItemCode.Text.ToString() != "" && txtItemCode.Text.ToString() != null)
                    inwardItem.ItemCode = txtItemCode.Text.ToString();
                else
                {
                    string[] itemCodeArr = ddlItemCode.SelectedValue.Split('_');
                    inwardItem.ItemCode = itemCodeArr[0].ToString();
                }

                inwardItem.ItemLocation = txtItemLocation.Text;
                inwardItem.POQuantity = txtPOQuantity.Text;
                inwardItem.ReceivedQuantity = txtRcvdQty.Text;
                inwardItem.BalanceQuantity = txtBalanceQty.Text;
                inwardItem.Quantity = txtQty.Text;

                if (ddlItemCode.Visible)
                    inwardItem.SupplierPartNumber = ddlItemCode.SelectedItem.Text;
                else
                    inwardItem.SupplierPartNumber = txtSupplierPartNo.Text;

                inwardItem.ListPrice = txtListPrice.Text;
                inwardItem.CostPrice = txtCostPrice.Text;
                inwardItem.Coupon = txtItemCoupon.Text;

                inwardItem.ListLessDiscount = txtListLessDiscount.Text;
                inwardItem.SNO = SNo.ToString();
                inwardItem.SerialNo = txtSerialNo.Text;
                inwardItem.PONumber = txtPONumber.Text;
                inwardItem.ItemTaxPercentage = txtTaxPercentage.Text;

                dmlTotalCoupon += Convert.ToDecimal(txtItemCoupon.Text);

                dmlTotalTax += Convert.ToDecimal(txtItemTaxValue.Text);

                inwardEntity.Items.Add(inwardItem);
            }

            if (grvItemDetails.FooterRow != null)
            {
                grvItemDetails.FooterRow.Cells[9].Text = dmlTotal.ToString();
                grvItemDetails.FooterRow.Cells[10].Text = dmlTotalCoupon.ToString();
                grvItemDetails.FooterRow.Cells[14].Text = dmlTotalTax.ToString();
            }
            
            if (ddlInwardNumber.Visible == false)
            {
                int result = inwardTransactions.AddNewInwardEntry(ref inwardEntity);
                if ((inwardEntity.ErrorMsg == string.Empty) && (inwardEntity.ErrorCode == "0"))
                {
                    txtInwardNumber.Text = inwardEntity.InwardNumber;
                    FreezeOrUnFreezeButtons(false);

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward entry details has been saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + inwardEntity.ErrorMsg + "');", true);
                }
            }
            else
            {
                inwardEntity.InwardNumber = ddlInwardNumber.SelectedValue.ToString();
                int result = inwardTransactions.UpdateInwardEntry(ref inwardEntity);
                ddlInwardNumber.SelectedValue = inwardEntity.InwardNumber;
                if ((inwardEntity.ErrorMsg == string.Empty) && (inwardEntity.ErrorCode == "0"))
                {
                    FreezeOrUnFreezeButtons(false);

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward entry details has been updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + inwardEntity.ErrorMsg + "');", true);
                }
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));
            dt.Columns.Add(new DataColumn("Col9", typeof(string)));
            dt.Columns.Add(new DataColumn("Col10", typeof(string)));
            dt.Columns.Add(new DataColumn("Col11", typeof(string)));
            dt.Columns.Add(new DataColumn("Col12", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));
            dt.Columns.Add(new DataColumn("Col15", typeof(string)));
            dt.Columns.Add(new DataColumn("SerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("PONumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ExistingQty", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Col1"] = string.Empty;
                dr["Col2"] = string.Empty;
                dr["Col3"] = string.Empty;
                dr["Col4"] = string.Empty;
                dr["Col5"] = string.Empty;
                dr["Col6"] = string.Empty;
                dr["Col7"] = string.Empty;
                dr["Col8"] = string.Empty;
                dr["Col9"] = string.Empty;
                dr["Col10"] = string.Empty;
                dr["Col11"] = string.Empty;
                dr["Col12"] = string.Empty;
                dr["Col13"] = string.Empty;
                dr["Col14"] = string.Empty;
                dr["Col15"] = string.Empty;
                dr["SerialNo"] = string.Empty;
                dr["PONumber"] = string.Empty;
                dr["ExistingQty"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 19;
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        private void FillLastRowInfoInGrid(ref DataTable dt)
        {
            int rowIndex = grvItemDetails.Rows.Count - 1;

            DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlCCWHNo");
            TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCCWHNo");

            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlItemCode");
            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

            TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");

            TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");

            TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");

            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");

            TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");

            TextBox txtBalanceQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");
            //txtRcvdQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtRcvdQty.ClientID + "','" + txtPOQuantity.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "');");

            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
            TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSerialNo");
            TextBox txtPONumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPONumber");

            TextBox txtListLessDiscount = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
            //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
            //TextBox txtEDValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");
            TextBox txtQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");

            TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
            TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
            TextBox txtItemTaxValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");


            if ((ddlCCWHNo.Visible) && (ddlItemCode.Visible))
            {

                if ((ddlCCWHNo.SelectedValue.ToString() != "0") && (ddlItemCode.SelectedValue.ToString() != "") && (txtRcvdQty.Text != "" || txtRcvdQty.Text != "0"))
                {
                    dt.Rows[rowIndex]["Col1"] = ddlCCWHNo.SelectedValue.ToString();
                    dt.Rows[rowIndex]["Col2"] = ddlItemCode.SelectedItem.Text;                    
                    dt.Rows[rowIndex]["Col3"] = txtPOQuantity.Text;
                    dt.Rows[rowIndex]["Col4"] = txtRcvdQty.Text;
                    dt.Rows[rowIndex]["Col5"] = txtBalanceQty.Text;                    
                    dt.Rows[rowIndex]["Col6"] = txtQty.Text;
                    dt.Rows[rowIndex]["Col7"] = txtListPrice.Text;
                    dt.Rows[rowIndex]["Col8"] = txtCstPricePerQty.Text;
                    dt.Rows[rowIndex]["Col9"] = txtCostPrice.Text;
                    dt.Rows[rowIndex]["Col10"] = txtItemCoupon.Text;
                    dt.Rows[rowIndex]["Col11"] = txtListLessDiscount.Text;
                    dt.Rows[rowIndex]["Col12"] = txtItemLocation.Text;
                    dt.Rows[rowIndex]["Col13"] = txtTaxPercentage.Text;
                    dt.Rows[rowIndex]["Col14"] = txtItemTaxValue.Text;

                    string[] listitemArr = ddlItemCode.SelectedValue.Split('_');
                    dt.Rows[rowIndex]["Col15"] = listitemArr[0];
                    dt.Rows[rowIndex]["SerialNo"] = listitemArr[1];
                    dt.Rows[rowIndex]["PONumber"] = listitemArr[2];
                }
            }
        }

        private void BindExistingRowsDuringEdit(List<InwardItem> lstInwardItem)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));
            dt.Columns.Add(new DataColumn("Col9", typeof(string)));
            dt.Columns.Add(new DataColumn("Col10", typeof(string)));
            dt.Columns.Add(new DataColumn("Col11", typeof(string)));
            dt.Columns.Add(new DataColumn("Col12", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));
            dt.Columns.Add(new DataColumn("Col15", typeof(string)));
            dt.Columns.Add(new DataColumn("SerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("PONumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ExistingQty", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < lstInwardItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstInwardItem[i].SNO.ToString();
                dr["Col1"] = lstInwardItem[i].CCWHNO.ToString();
                dr["Col2"] = lstInwardItem[i].SupplierPartNumber.ToString();
                dr["Col3"] = lstInwardItem[i].POQuantity.ToString();
                dr["Col4"] = lstInwardItem[i].ReceivedQuantity.ToString();
                dr["Col5"] = lstInwardItem[i].BalanceQuantity.ToString();                
                dr["Col6"] = lstInwardItem[i].Quantity.ToString();
                dr["ExistingQty"] = lstInwardItem[i].Quantity.ToString();
                dr["Col7"] = lstInwardItem[i].ListPrice.ToString();
                dr["Col8"] = lstInwardItem[i].CostPricePerQty.ToString();
                dr["Col9"] = lstInwardItem[i].CostPrice.ToString();
                dr["Col10"] = lstInwardItem[i].Coupon.ToString();
                dr["Col11"] = lstInwardItem[i].ListLessDiscount.ToString();
                dr["Col12"] = lstInwardItem[i].ItemLocation.ToString();
                dr["Col13"] = lstInwardItem[i].ItemTaxPercentage.ToString();
                dr["Col14"] = lstInwardItem[i].ItemTaxValue.ToString();
                dr["Col15"] = lstInwardItem[i].ItemCode.ToString();

                if (lstInwardItem[i].SerialNo != null)
                {
                    dr["SerialNo"] = lstInwardItem[i].SerialNo.ToString();
                    dr["PONumber"] = lstInwardItem[i].PONumber.ToString();
                }

                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCnt.Value = dt.Rows.Count.ToString();

            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();
            SetPreviousData();
            HideDllItemCodeDropDownForDisplayOnlyEdit(inwardStatusDuringEdit);

            ShowSummaryInFooter();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

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
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList DdlCCWHNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlCCWHNo");
                            TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCCWHNo");

                            DropDownList DdlItemCode = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlItemCode");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                            TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");

                            TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");
                            TextBox txtBalanceQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                            TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSerialNo");
                            TextBox txtPONumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPONumber");

                            TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                            TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");

                            TextBox txtListLessDiscount = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                            //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                            //TextBox txtEDValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");
                            TextBox txtQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");

                            TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                            TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                            TextBox txtItemTaxValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            if (DdlCCWHNo.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Col1"] = DdlCCWHNo.SelectedValue;//txtCCWHNo.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Col1"] = txtCCWHNo.Text.ToString();
                            }

                            if (DdlItemCode.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Col2"] = DdlItemCode.SelectedItem.Text;//.SelectedValue;//txtCCWHNo.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Col2"] = txtSupplierPartNo.Text.ToString();
                            }

                            dtCurrentTable.Rows[i - 1]["Col3"] = txtPOQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Col4"] = txtRcvdQty.Text;
                            dtCurrentTable.Rows[i - 1]["Col5"] = (Convert.ToInt64(txtPOQuantity.Text) - Convert.ToInt64(txtRcvdQty.Text)).ToString();
                            dtCurrentTable.Rows[i - 1]["SerialNo"] = txtSerialNo.Text;
                            dtCurrentTable.Rows[i - 1]["PONumber"] = txtPONumber.Text;
                            dtCurrentTable.Rows[i - 1]["Col6"] = txtQty.Text;
                            dtCurrentTable.Rows[i - 1]["Col7"] = txtListPrice.Text;
                            dtCurrentTable.Rows[i - 1]["Col8"] = txtCstPricePerQty.Text;
                            dtCurrentTable.Rows[i - 1]["Col9"] = (Convert.ToInt64(txtQty.Text) * Convert.ToDecimal(txtCstPricePerQty.Text)).ToString();
                            dtCurrentTable.Rows[i - 1]["Col10"] = txtItemCoupon.Text;
                            dtCurrentTable.Rows[i - 1]["Col11"] = txtListLessDiscount.Text;
                            dtCurrentTable.Rows[i - 1]["Col12"] = txtItemLocation.Text;
                            dtCurrentTable.Rows[i - 1]["Col13"] = txtTaxPercentage.Text;
                            dtCurrentTable.Rows[i - 1]["Col14"] = txtItemTaxValue.Text;
                            dtCurrentTable.Rows[i - 1]["Col15"] = txtItemCode.Text;
                            
                            rowIndex++;
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();
                    inwardStatusDuringAdd = hdnScreenMode.Value;

                    grvItemDetails.DataSource = dtCurrentTable;
                    grvItemDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
            HideDllItemCodeDropDown();
            ShowSummaryInFooter();
        }

        private void HideDllItemCodeDropDown()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtCCWHNo");
                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlItemCode");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSupplierPartNo");
                TextBox txtQty = (TextBox)grvItemDetails.Rows[index].FindControl("txtQty");
                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCoupon");
                TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemLocation");
                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[index].FindControl("txtTaxPercentage");

                if (index != grvItemDetails.Rows.Count - 1)
                {
                    ddlCCWHNo.Visible = false;
                    ddlItemCode.Visible = false;

                    txtCCWHNo.Visible = true;
                    txtSupplierPartNo.Visible = true;
                    txtQty.Enabled = false;
                    txtItemCoupon.Enabled = false;
                    txtItemLocation.Enabled = false;
                    txtTaxPercentage.Enabled = false;

                    sb.Append(txtQty.ClientID + ",");
                }
                else
                {
                    txtCCWHNo.Visible = false;
                    txtSupplierPartNo.Visible = false;

                    ddlCCWHNo.Visible = true;
                    ddlItemCode.Visible = true;
                    txtQty.Enabled = true;
                    txtItemCoupon.Enabled = true;
                    txtItemLocation.Enabled = true;
                    txtTaxPercentage.Enabled = true;

                    sb.Append("" + ddlCCWHNo.ClientID + "," + ddlItemCode.ClientID + "," + txtQty.ClientID + ",");

                    if (ddlTransactionType.SelectedValue == "171")
                    {
                        RemoveDuplicates();
                    }
                }
            }

            txtHdnGridCtrls.Text = sb.ToString();
        }

        private void HideDllItemCodeDropDownForDisplayOnly(string status)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string ddlItemVal = string.Empty;
            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtCCWHNo");

                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlItemCode");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCode");
                TextBox txtQty = (TextBox)grvItemDetails.Rows[index].FindControl("txtQty");
                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCoupon");
                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[index].FindControl("txtTaxPercentage");

                TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSerialNo");
                TextBox txtPONumber = (TextBox)grvItemDetails.Rows[index].FindControl("txtPONumber");

                if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                {
                    //if (index == grvItemDetails.Rows.Count - 1)
                    //{
                    //    ddlCCWHNo.Visible = true;
                    //    ddlCCWHNo.SelectedIndex = ddlCCWHNo.Items.IndexOf(ddlCCWHNo.Items.FindByValue(txtCCWHNo.Text));
                    //    txtCCWHNo.Visible = false;

                    //    string[] strArr = ddlItemVal.Split('|');

                    //    foreach (string str in strArr)
                    //    {
                    //        if (string.IsNullOrEmpty(str))
                    //            continue;

                    //        ListItem li = ddlItemCode.Items.FindByValue(str);
                    //        ddlItemCode.Items.Remove(li);
                    //    }

                    //    if (ddlTransactionType.SelectedValue == "171")
                    //        ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(ddlItemCode.Items.FindByValue(txtItemCode.Text));
                    //    else
                    //        ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(ddlItemCode.Items.FindByValue(txtItemCode.Text + "_" + txtSerialNo.Text + "_" + txtPONumber.Text));

                    //    ddlItemCode.Visible = true;
                    //    txtSupplierPartNo.Visible = false;
                    //    txtQty.Enabled = true;
                    //    txtItemCoupon.Enabled = true;
                    //    txtTaxPercentage.Enabled = true;
                    //}
                    //else
                    //{
                        if (ddlTransactionType.SelectedValue == "171")
                            ddlItemVal += txtItemCode.Text + "|";
                        else
                            ddlItemVal += txtItemCode.Text + "_" + txtSerialNo.Text + "_" + txtPONumber.Text + "|";                            

                        ddlCCWHNo.Visible = false;
                        ddlItemCode.Visible = false;
                        txtCCWHNo.Visible = true;
                        txtSupplierPartNo.Visible = true;
                        txtQty.Enabled = true;
                        txtItemCoupon.Enabled = false;
                        txtTaxPercentage.Enabled = false;
                    //}
                    sb.Append(txtQty.ClientID + ",");
                }
            }

            txtHdnGridCtrls.Text = sb.ToString();
        }

        private void HideDllItemCodeDropDownForDisplayOnlyEdit(string status)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string ddlItemVal = string.Empty;
            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtCCWHNo");

                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlItemCode");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCode");
                TextBox txtQty = (TextBox)grvItemDetails.Rows[index].FindControl("txtQty");
                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCoupon");
                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[index].FindControl("txtTaxPercentage");

                TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSerialNo");
                TextBox txtPONumber = (TextBox)grvItemDetails.Rows[index].FindControl("txtPONumber");

                if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                {
                    //if (index == grvItemDetails.Rows.Count - 1)
                    //{
                    //    ddlCCWHNo.Visible = true;
                    //    ddlCCWHNo.SelectedIndex = ddlCCWHNo.Items.IndexOf(ddlCCWHNo.Items.FindByValue(txtCCWHNo.Text));
                    //    txtCCWHNo.Visible = false;

                    //    string[] strArr = ddlItemVal.Split('|');

                    //    foreach (string str in strArr)
                    //    {
                    //        if (string.IsNullOrEmpty(str))
                    //            continue;

                    //        ListItem li = ddlItemCode.Items.FindByValue(str);
                    //        ddlItemCode.Items.Remove(li);
                    //    }

                    //    if (ddlTransactionType.SelectedValue == "171")
                    //        ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(ddlItemCode.Items.FindByValue(txtItemCode.Text));
                    //    else
                    //        ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(ddlItemCode.Items.FindByValue(txtItemCode.Text + "_" + txtSerialNo.Text + "_" + txtPONumber.Text));

                    //    ddlItemCode.Visible = true;
                    //    txtSupplierPartNo.Visible = false;
                    //    txtQty.Enabled = true;
                    //    txtItemCoupon.Enabled = true;
                    //    txtTaxPercentage.Enabled = true;
                    //}
                    //else
                    //{
                        if (ddlTransactionType.SelectedValue == "171")
                            ddlItemVal += txtItemCode.Text + "|";
                        else
                            ddlItemVal += txtItemCode.Text + "_" + txtSerialNo.Text + "_" + txtPONumber.Text + "|";

                        ddlCCWHNo.Visible = false;
                        ddlItemCode.Visible = false;
                        txtCCWHNo.Visible = true;
                        txtSupplierPartNo.Visible = true;
                        txtQty.Enabled = true;
                        txtItemCoupon.Enabled = false;
                        txtTaxPercentage.Enabled = false;
                    //}
                    sb.Append(txtQty.ClientID + ",");
                }
            }

            txtHdnGridCtrls.Text = sb.ToString();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlCCWHNo");
                        TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCCWHNo");

                        DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlItemCode");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                        TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");

                        TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");

                        TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");

                        TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                        TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");

                        TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");

                        TextBox txtBalanceQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");

                        TextBox txtQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQty");
                        HiddenField txtExistingQty = (HiddenField)grvItemDetails.Rows[rowIndex].FindControl("txtExistingQty");

                        //txtRcvdQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtRcvdQty.ClientID + "','" + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "');");
                        txtQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "','" + txtExistingQty.ClientID + "');");
                        txtItemCoupon.Attributes.Add("OnChange", "return funCalculateCoupon('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtItemCoupon.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "','" + txtExistingQty.ClientID + "');");
                        
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSerialNo");
                        TextBox txtPONumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPONumber");

                        TextBox txtListLessDiscount = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                        //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                        //TextBox txtEDValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtEDValue");

                        TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                        TextBox txtItemTaxValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");

                        txtTaxPercentage.Attributes.Add("OnChange", "return funCalculateTaxValue('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "','" + txtExistingQty.ClientID + "');");

                        txtCCWHNo.Text = dt.Rows[i]["Col1"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i]["Col2"].ToString();
                        txtPOQuantity.Text = dt.Rows[i]["Col3"].ToString();
                        txtRcvdQty.Text = dt.Rows[i]["Col4"].ToString();
                        txtBalanceQty.Text = dt.Rows[i]["Col5"].ToString();
                        txtQty.Text = dt.Rows[i]["Col6"].ToString();

                        if (txtRcvdQty.Text != "0")
                            txtExistingQty.Value = dt.Rows[i]["ExistingQty"].ToString();
                        else
                            txtExistingQty.Value = "0";

                        txtListPrice.Text = dt.Rows[i]["Col7"].ToString();
                        txtCstPricePerQty.Text = dt.Rows[i]["Col8"].ToString();
                        txtCostPrice.Text = dt.Rows[i]["Col9"].ToString();
                        txtItemCoupon.Text = dt.Rows[i]["Col10"].ToString();
                        txtListLessDiscount.Text = dt.Rows[i]["Col11"].ToString();
                        txtItemLocation.Text = dt.Rows[i]["Col12"].ToString();
                        txtTaxPercentage.Text = dt.Rows[i]["Col13"].ToString();
                        txtItemTaxValue.Text = dt.Rows[i]["Col14"].ToString();
                        txtItemCode.Text = dt.Rows[i]["Col15"].ToString();
                        txtSerialNo.Text = dt.Rows[i]["SerialNo"].ToString();
                        txtPONumber.Text = dt.Rows[i]["PONumber"].ToString();

                        List<CCWH> obj = new List<CCWH>();
                        if (ddlTransactionType.SelectedValue == "171")//Free of cost
                        {
                            CCWH ccwh = new CCWH();
                            ccwh.PONumber = "0";
                            ccwh.CCWHNumber = "--Not Required--";
                            obj.Add(ccwh);

                            ddlCCWHNo.DataTextField = "CCWHNumber";
                            ddlCCWHNo.DataValueField = "PONumber";
                            ddlCCWHNo.DataSource = obj;
                            ddlCCWHNo.DataBind();
                            ddlCCWHNo.Enabled = false;
                        }
                        else
                        {
                            if ((ddlSupplierName.SelectedValue != "0") && (ddlBranch.SelectedValue != "0") && (ddlTransactionType.SelectedValue != "0"))
                                obj = GetCCWHNo(ddlSupplierName.SelectedValue, ddlBranch.SelectedValue, ddlTransactionType.SelectedValue);

                            ddlCCWHNo.DataSource = obj;
                            ddlCCWHNo.DataBind();
                            ddlCCWHNo.Enabled = true;
                        }
                        
                        if (txtCCWHNo.Text != null && txtCCWHNo.Text != "" && txtCCWHNo.Text != "0")
                        {
                            ddlItemCode.DataSource = (object)GetItemCode(txtCCWHNo.Text, ddlInwardNumber.SelectedValue);
                            ddlItemCode.DataTextField = "ItemDesc";
                            ddlItemCode.DataValueField = "ItemCode";
                            ddlItemCode.DataBind();
                            ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(ddlItemCode.Items.FindByValue(dt.Rows[i]["Col15"].ToString() + "_" + dt.Rows[i]["SerialNo"].ToString() + "_" + dt.Rows[i]["PONumber"].ToString()));
                        }

                        rowIndex++;
                    }
                }
            }
        }

        private List<CCWH> GetCCWHNo(string SupplierCode, string BranchCode, string TransactionType)
        {
            List<CCWH> obj = inwardTransactions.GetCCWHNumbers(ddlSupplierName.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), hdnScreenMode.Value);
            return obj;
        }

        private void ShowSummaryInFooter()
        {
            foreach (GridViewRow gvr in grvItemDetails.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtListPrice = (TextBox)gvr.FindControl("txtListPrice");
                    TextBox txtCostPrice = (TextBox)gvr.FindControl("txtCostPrice");
                    TextBox txtItemCoupon = (TextBox)gvr.FindControl("txtItemCoupon");
                    TextBox txtItemTaxValue = (TextBox)gvr.FindControl("txtItemTaxValue");

                    if (string.IsNullOrEmpty(txtListPrice.Text))
                        txtListPrice.Text = "0";
                    if (string.IsNullOrEmpty(txtCostPrice.Text))
                        txtCostPrice.Text = "0";
                    if (string.IsNullOrEmpty(txtItemCoupon.Text))
                        txtItemCoupon.Text = "0";
                    if (string.IsNullOrEmpty(txtItemTaxValue.Text))
                        txtItemTaxValue.Text = "0";

                    sumListPrice += Convert.ToDecimal(txtListPrice.Text);
                    sumCostPrice += Convert.ToDecimal(txtCostPrice.Text);
                    sumCoupon += Convert.ToDecimal(txtItemCoupon.Text);
                    sumItemTaxValue += Convert.ToDecimal(txtItemTaxValue.Text);
                }
            }

            if (grvItemDetails.FooterRow != null)
            {
                grvItemDetails.FooterRow.Cells[7].Text = "";

                //if (ddlTransactionType.SelectedValue == "171")
                //{
                //    grvItemDetails.FooterRow.Cells[9].Text = "0.00";
                //    grvItemDetails.FooterRow.Cells[10].Text = "0.00";
                //    grvItemDetails.FooterRow.Cells[14].Text = "0.00";
                //}
                //else
                //{
                    grvItemDetails.FooterRow.Cells[9].Text = sumCostPrice.ToString();
                    grvItemDetails.FooterRow.Cells[10].Text = sumCoupon.ToString();
                    grvItemDetails.FooterRow.Cells[14].Text = sumItemTaxValue.ToString();
                //}

                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                hdnFooterCoupon.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                hdnFooterTaxPrice.Value = grvItemDetails.FooterRow.Cells[14].ClientID;

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                {
                    btnAddRow.Attributes.Remove("OnClick");
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                }
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string FourDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.0000";
            else
                return string.Format("{0:0.0000}", Convert.ToDecimal(strValue));
        }

        private string DecimalToIntConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "";
            else
                return string.Format("{0:0}", Convert.ToDecimal(strValue));
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            if (grvItemDetails.FooterRow != null)
            {
                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");

                if (btnAdd != null)
                    btnAdd.Enabled = Fzflag;
            }

            BtnSubmit.Enabled = Fzflag;
            imgEditToggle.Enabled = Fzflag;
            DivHeader.Disabled = !Fzflag;
            InwardPanel.Enabled = Fzflag;

            if ((inwardStatusDuringEdit == "A") && (hdnScreenMode.Value == "E"))
            {
                BtnSubmit.Enabled = false;
                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    gr.Enabled = false;
                }
            }

            UpdpanelTop.Update();
            UpdPanelGrid.Update();
        }

        private void GetAllItemDetailsForFreeOfCost(GridViewRow CurrentRow)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddlItemCode = (DropDownList)CurrentRow.FindControl("ddlItemCode");
                TextBox txtSupplierPartNo = (TextBox)CurrentRow.FindControl("txtSupplierPartNo");

                ddlItemCode.DataSource = (object)GetFOCItemCodes();
                ddlItemCode.DataTextField = "ItemDesc";
                ddlItemCode.DataValueField = "ItemCode";
                ddlItemCode.DataBind();

                TextBox txtExistingItem = null;
                string strArray = "";

                foreach (ListItem li in ddlItemCode.Items)
                {
                    if (li.Value.ToString() == "")
                        continue;

                    foreach (GridViewRow gvr in grvItemDetails.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            txtExistingItem = (TextBox)gvr.FindControl("txtItemCode");
                            if (txtExistingItem.Text.Trim() == li.Value.ToString())
                            {
                                strArray += li.Value.ToString() + "|";
                            }
                        }
                    }
                }
                string[] strArr = strArray.Split('|');

                foreach (string str in strArr)
                {
                    if (string.IsNullOrEmpty(str))
                        continue;

                    ListItem li = ddlItemCode.Items.FindByValue(str);
                    ddlItemCode.Items.Remove(li);
                }
                
                ddlItemCode.Visible = true;

                ShowSummaryInFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetFOCItemCodes()
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetFOCItemCodes(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString());
            return obj;
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetSupplierDepotInward(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString(), txtSupplierGSTIN.Text);
            return obj;
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplantsEdit(string PlantCode)
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetSupplierDepotInwardEdit(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString(), txtSupplierGSTIN.Text, PlantCode);
            return obj;
        }
    }
}
