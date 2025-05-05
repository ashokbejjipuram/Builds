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
using System.Collections;

namespace IMPALWeb
{
    public partial class AutoInwardEntry : System.Web.UI.Page
    {
        //private string ScreenMode = "A";//"A--Add Mode,E--Edit Mode,V--View Mode"
        Decimal sumCostPrice = 0;
        Decimal sumListPrice = 0;
        Decimal sumCoupon = 0;
        Decimal sumItemTaxValue = 0;
        string inwardStatusDuringEdit = string.Empty;
        InwardTransactions inwardTransactions = new InwardTransactions();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
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

                    hdnScreenMode.Value = "A";
                    hdnSecondSales.Value = "0";
                    txtInwardDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    divExcessItemDetails.Visible = false;
                    BtnProcessPO.Visible = false;

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
                    ddlTransactionType.SelectedValue = "201";

                    ddlSupplyPlant.Items.Add(new ListItem("--Select--", "0"));

                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();

                    txtSGSTValue.Enabled = false;
                    txtUTGSTValue.Enabled = false;
                    txtCGSTValue.Enabled = false;
                    txtIGSTValue.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return AutoInwardEntrySubmit();");
                ddlTransactionType.Attributes.Add("OnChange", "funTransTYpeChange();");
                ddlOSIndicator.Attributes.Add("OnChange", "funOSIndicatorChange();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");

                if (grvItemDetails.FooterRow != null)
                {
                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                    hdnFooterCoupon.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    hdnFooterTaxPrice.Value = grvItemDetails.FooterRow.Cells[14].ClientID;
                }

                trRoadPermitDetails.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("AutoInwardEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string InvoiceNo = inwardTransactions.CheckInvoiceExists(ddlSupplierName.SelectedValue, txtInwardDate.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, Session["BranchCode"].ToString());

                if (InvoiceNo != "")
                {
                    txtInvoiceNo.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number already exists...');", true);
                    return;
                }

                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void ddlSupplierName_OnDataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                if (ddl != null)
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        li.Attributes["title"] = li.Text;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplierName.SelectedIndex > 0)
                {
                    ddlInvoiceNumber.DataSource = (object)GetAutoGRNSupplyInvoices();
                    ddlInvoiceNumber.DataTextField = "ItemDesc";
                    ddlInvoiceNumber.DataValueField = "ItemCode";
                    ddlInvoiceNumber.DataBind();
                }
                else
                {
                    ddlInvoiceNumber.DataSource = null;
                    ddlInvoiceNumber.DataBind();
                }

                hdnSecondSales.Value = "0";

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void ddlInvoiceNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInvoiceNumber.SelectedIndex > 0)
                {
                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    InwardEntity inwardEntity = inwardTransactions.GetInwardDetailsAutoGRN(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue, ddlInvoiceNumber.SelectedValue);

                    if (inwardEntity != null)
                    {
                        if (inwardEntity.Items.Count == 0 && inwardEntity.ExcessItems.Count == 0)
                        {
                            grvItemDetails.DataSource = null;
                            grvItemDetails.DataBind();

                            divExcessItemDetails.Visible = false;
                            grvExcessItemDetails.DataSource = null;
                            grvExcessItemDetails.DataBind();
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('All the Part # in this Invoice are Under Approval Process from HO for Supplimentary Order. Please inform and Get Approval from HO.');", true);
                            return;
                        }

                        txtInwardDate.Text = inwardEntity.InwardDate;
                        ddlTransactionType.SelectedValue = inwardEntity.TransactionTypeCode;

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
                        hdnSupplierCnt.Value = inwardEntity.SuppliersCount;

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

                        BindExistingRowsDuringEdit(inwardEntity.Items);
                        ddlTransactionType.Enabled = false;
                        ddlSupplierName.Enabled = false;
                        ddlBranch.Enabled = false;

                        FreezeOrUnFreezeButtons(true);
                        UpdpanelTop.Update();

                        string ItemCodes = string.Empty;
                        ItemCodes = inwardTransactions.CheckNewItemsInward(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue, ddlInvoiceNumber.SelectedValue.ToString());

                        if (ItemCodes != "" && ItemCodes != null)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Auto Inward Supplier Part Numbers " + ItemCodes + " are missing in Item Master table. Please inform and Get Approval from HO.');", true);
                            BtnSubmit.Visible = false;
                            BtnSubmit.Enabled = false;
                            BtnProcessPO.Visible = false;
                        }
                        else
                        {
                            BindExistingRowsMissingItemsDuringEdit(inwardEntity.ExcessItems);
                            ddlTransactionType.Enabled = false;
                            ddlSupplierName.Enabled = false;
                            ddlBranch.Enabled = false;

                            if (inwardEntity.PoPendingApprovalStatus != "0")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Few of the Part # in this Invoice are Under Approval Process from HO for Supplimentary Order. Please inform and Get Approval from HO.');", true);
                                divExcessItemDetails.Visible = false;
                                BtnProcessPO.Visible = false;
                                BtnSubmit.Visible = false;
                                BtnSubmit.Enabled = false;
                            }
                            else if (inwardEntity.ExcessShortageStatus != "0")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This Invoice is Having Excess/Shortage Items. Please Get Supplimentary Order from HO for Missing Items and then Reprocess Auto GRN for this Invoice #.');", true);
                                divExcessItemDetails.Visible = true;
                                BtnProcessPO.Visible = true;
                                BtnSubmit.Visible = false;
                                BtnSubmit.Enabled = false;
                            }
                            else
                            {
                                if (hdnSupplierCnt.Value != "1")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This invoice is having Items for " + inwardEntity.SuppliersCount + " Suppliers. So after completing this GRN, Please Inward Other Supplier Items by selecting respective Supplier Name and Same Invoice Number.');", true);
                                }

                                divExcessItemDetails.Visible = false;
                                BtnProcessPO.Visible = false;
                                BtnSubmit.Visible = true;
                                BtnSubmit.Enabled = true;
                            }
                        }

                        if (ddlSupplierName.SelectedValue == "330" || ddlSupplierName.SelectedValue == "350" || ddlSupplierName.SelectedValue == "770")
                        {
                            if (inwardEntity.SGSTValue == "") inwardEntity.SGSTValue = "0";
                            if (inwardEntity.CGSTValue == "") inwardEntity.CGSTValue = "0";
                            if (inwardEntity.IGSTValue == "") inwardEntity.IGSTValue = "0";
                            if (inwardEntity.UTGSTValue == "") inwardEntity.UTGSTValue = "0";

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('This invoice is having " + inwardEntity.Items.Count + " Item(s) with Total Item Value of Rs. " + inwardEntity.InvoiceValue + "/- and Total GST Value of Rs. " + (Convert.ToDecimal(inwardEntity.SGSTValue) + Convert.ToDecimal(inwardEntity.CGSTValue) + Convert.ToDecimal(inwardEntity.IGSTValue) + Convert.ToDecimal(inwardEntity.UTGSTValue)) + "/-. Please Check and Proceed.');", true);
                        }
                    }
                }
                else
                {
                    ddlSupplyPlant.DataSource = null;
                    ddlSupplyPlant.DataBind();

                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
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
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        protected void grvExcessItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    List<CCWH> obj = new List<CCWH>();
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlCCWH = (DropDownList)e.Row.FindControl("ddlCCWHNo");

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

                            grvExcessItemDetails.Columns[3].Visible = true;
                            grvExcessItemDetails.Columns[4].Visible = true;
                            grvExcessItemDetails.Columns[5].Visible = true;
                            grvExcessItemDetails.Columns[7].Visible = true;
                            grvExcessItemDetails.Columns[8].Visible = true;
                            grvExcessItemDetails.Columns[9].Visible = true;
                            grvExcessItemDetails.Columns[10].Visible = true;
                            grvExcessItemDetails.Columns[11].Visible = true;
                            grvExcessItemDetails.Columns[12].Visible = true;
                            grvExcessItemDetails.Columns[13].Visible = true;
                            grvExcessItemDetails.Columns[14].Visible = true;
                            grvExcessItemDetails.Columns[15].Visible = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(AutoInwardEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetItemCode(string PoNumber, string InwardNumber)
        {
            return inwardTransactions.GetItemCode(PoNumber, InwardNumber, Session["BranchCode"].ToString());
        }

        private void SubmitHeaderAndItems()
        {
            InwardEntity inwardEntity = new InwardEntity();
            inwardEntity.Items = new List<InwardItem>();

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

                TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");

                TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");

                TextBox txtPOQuantity = (TextBox)gr.FindControl("txtPOQuantity");
                TextBox txtRcvdQty = (TextBox)gr.FindControl("txtRcvdQty");
                TextBox txtBalanceQty = (TextBox)gr.FindControl("txtBalanceQty");
                TextBox txtQty = (TextBox)gr.FindControl("txtQty");

                TextBox txtListPrice = (TextBox)gr.FindControl("txtListPrice");
                TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");
                TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");

                dmlTotal += Convert.ToDecimal(txtCostPrice.Text);

                TextBox txtItemCoupon = (TextBox)gr.FindControl("txtItemCoupon");

                TextBox txtListLessDiscount = (TextBox)gr.FindControl("txtListLessDiscount");
                TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                TextBox txtSerialNo = (TextBox)gr.FindControl("txtSerialNo");
                TextBox txtPONumber = (TextBox)gr.FindControl("txtPONumber");
                TextBox txtTaxPercentage = (TextBox)gr.FindControl("txtTaxPercentage");
                TextBox txtItemTaxValue = (TextBox)gr.FindControl("txtItemTaxValue");
                TextBox txtHSNCode = (TextBox)gr.FindControl("txtHSNCode");
                TextBox txtMRP = (TextBox)gr.FindControl("txtMRP");

                inwardItem.ItemCode = txtItemCode.Text.ToString();
                inwardItem.ItemLocation = txtItemLocation.Text;
                inwardItem.POQuantity = txtPOQuantity.Text;
                inwardItem.ReceivedQuantity = txtRcvdQty.Text;
                inwardItem.BalanceQuantity = txtBalanceQty.Text;
                inwardItem.Quantity = txtQty.Text;
                inwardItem.SupplierPartNumber = txtSupplierPartNo.Text;
                inwardItem.ListPrice = txtListPrice.Text;
                inwardItem.CostPrice = txtCostPrice.Text;
                inwardItem.CostPricePerQty = txtCstPricePerQty.Text;
                inwardItem.MRP = txtMRP.Text;
                inwardItem.Coupon = txtItemCoupon.Text;
                inwardItem.ListLessDiscount = txtListLessDiscount.Text;
                inwardItem.SNO = SNo.ToString();
                inwardItem.SerialNo = txtSerialNo.Text;
                inwardItem.PONumber = txtPONumber.Text;
                inwardItem.ItemTaxPercentage = txtTaxPercentage.Text;
                inwardItem.HSN = txtHSNCode.Text;

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
            
            int result = inwardTransactions.AddNewAutoInwardEntry(ref inwardEntity);

            if ((inwardEntity.ErrorMsg == string.Empty) && (inwardEntity.ErrorCode == "0"))
            {
                BtnSubmit.Visible = false;
                txtInwardNumber.Text = inwardEntity.InwardNumber;
                FreezeOrUnFreezeButtons(false);

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    gr.Enabled = false;
                }

                if (hdnSupplierCnt.Value != "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward Entry is Done successfully. Now please Inward " + hdnSupplierCnt.Value.Replace(inwardEntity.SupplierCode, "") + " Supplier Line Items by selecting Supplier Name and Same Invoice Number to Complete this Invoice');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Inward Entry is Done successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + inwardEntity.ErrorMsg + "');", true);
            }
        }

        private void GetAllSuppliers()
        {
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliers();
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
            dt.Columns.Add(new DataColumn("HSN", typeof(string)));

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
                dr["HSN"] = string.Empty;
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
            TextBox txtHSNCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtHSNCode");
            TextBox txtMRP = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtMRP");

            dt.Rows[rowIndex]["Col1"] = ddlCCWHNo.SelectedValue.ToString();
            dt.Rows[rowIndex]["Col2"] = txtItemCode.Text;
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
            dt.Rows[rowIndex]["HSN"] = txtHSNCode.Text;
            dt.Rows[rowIndex]["MRP"] = txtMRP.Text;

            string[] listitemArr = txtItemCode.Text.Split('_');
            dt.Rows[rowIndex]["Col15"] = listitemArr[0];
            dt.Rows[rowIndex]["SerialNo"] = listitemArr[1];
            dt.Rows[rowIndex]["PONumber"] = listitemArr[2];

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
            dt.Columns.Add(new DataColumn("HSN", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));

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
                dr["HSN"] = lstInwardItem[i].HSN.ToString();
                dr["MRP"] = lstInwardItem[i].MRP.ToString();

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
            HideDllItemCodeDropDownForDisplayOnly(inwardStatusDuringEdit);

            ShowSummaryInFooter();
        }

        private void BindExistingRowsMissingItemsDuringEdit(List<InwardExcessItem> lstInwardExcessItem)
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
            dt.Columns.Add(new DataColumn("HSN", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < lstInwardExcessItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstInwardExcessItem[i].SNO.ToString();
                dr["Col1"] = lstInwardExcessItem[i].CCWHNO.ToString();
                dr["Col2"] = lstInwardExcessItem[i].SupplierPartNumber.ToString();
                dr["Col3"] = lstInwardExcessItem[i].POQuantity.ToString();
                dr["Col4"] = lstInwardExcessItem[i].ReceivedQuantity.ToString();
                dr["Col5"] = lstInwardExcessItem[i].BalanceQuantity.ToString();
                dr["Col6"] = lstInwardExcessItem[i].Quantity.ToString();
                dr["ExistingQty"] = lstInwardExcessItem[i].Quantity.ToString();
                dr["Col7"] = lstInwardExcessItem[i].ListPrice.ToString();
                dr["Col8"] = lstInwardExcessItem[i].CostPricePerQty.ToString();
                dr["Col9"] = lstInwardExcessItem[i].CostPrice.ToString();
                dr["Col10"] = lstInwardExcessItem[i].Coupon.ToString();
                dr["Col11"] = lstInwardExcessItem[i].ListLessDiscount.ToString();
                dr["Col12"] = lstInwardExcessItem[i].ItemLocation.ToString();
                dr["Col13"] = lstInwardExcessItem[i].ItemTaxPercentage.ToString();
                dr["Col14"] = lstInwardExcessItem[i].ItemTaxValue.ToString();
                dr["Col15"] = lstInwardExcessItem[i].ItemCode.ToString();
                dr["HSN"] = lstInwardExcessItem[i].HSN.ToString();
                dr["MRP"] = lstInwardExcessItem[i].MRP.ToString();

                if (lstInwardExcessItem[i].SerialNo != null)
                {
                    dr["SerialNo"] = lstInwardExcessItem[i].SerialNo.ToString();
                    dr["PONumber"] = lstInwardExcessItem[i].PONumber.ToString();
                }

                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCntExcessItems.Value = dt.Rows.Count.ToString();

            grvExcessItemDetails.DataSource = dt;
            grvExcessItemDetails.DataBind();
            SetPreviousDataMissingItems();
            HideDllItemCodeDropDownForDisplayOnly(inwardStatusDuringEdit);

            ShowSummaryExcessItemsInFooter();
        }

        private void HideDllItemCodeDropDown()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlCCWHNo = (DropDownList)grvItemDetails.Rows[index].FindControl("ddlCCWHNo");
                TextBox txtCCWHNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtCCWHNo");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSupplierPartNo");
                TextBox txtQty = (TextBox)grvItemDetails.Rows[index].FindControl("txtQty");
                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCoupon");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCode");
                TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemLocation");
                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[index].FindControl("txtTaxPercentage");

                if (index != grvItemDetails.Rows.Count - 1)
                {
                    ddlCCWHNo.Visible = false;
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
                    txtQty.Enabled = true;
                    txtItemCoupon.Enabled = true;
                    txtItemLocation.Enabled = true;
                    txtTaxPercentage.Enabled = true;

                    sb.Append("" + ddlCCWHNo.ClientID + "," + txtItemCode.ClientID + "," + txtQty.ClientID + ",");
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
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCode");
                TextBox txtQty = (TextBox)grvItemDetails.Rows[index].FindControl("txtQty");
                TextBox txtItemCoupon = (TextBox)grvItemDetails.Rows[index].FindControl("txtItemCoupon");
                TextBox txtTaxPercentage = (TextBox)grvItemDetails.Rows[index].FindControl("txtTaxPercentage");
                TextBox txtHSNCode = (TextBox)grvItemDetails.Rows[index].FindControl("txtHSNCode");

                TextBox txtSerialNo = (TextBox)grvItemDetails.Rows[index].FindControl("txtSerialNo");
                TextBox txtPONumber = (TextBox)grvItemDetails.Rows[index].FindControl("txtPONumber");

                if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                {
                    ddlCCWHNo.Visible = false;
                    txtCCWHNo.Visible = true;
                    txtSupplierPartNo.Visible = true;
                    txtQty.Enabled = false;
                    txtItemCoupon.Enabled = false;
                    txtTaxPercentage.Enabled = false;

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
                        TextBox txtHSNCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtHSNCode");
                        TextBox txtMRP = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtMRP");

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
                        txtHSNCode.Text = dt.Rows[i]["HSN"].ToString();
                        txtMRP.Text = dt.Rows[i]["MRP"].ToString();

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

                        rowIndex++;
                    }
                }
            }
        }

        private void SetPreviousDataMissingItems()
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
                        DropDownList ddlCCWHNo = (DropDownList)grvExcessItemDetails.Rows[rowIndex].FindControl("ddlCCWHNo");
                        TextBox txtCCWHNo = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtCCWHNo");

                        TextBox txtSupplierPartNo = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                        TextBox txtPOQuantity = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");

                        TextBox txtRcvdQty = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");

                        TextBox txtListPrice = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtListPrice");

                        TextBox txtCstPricePerQty = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                        TextBox txtCostPrice = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtCostPrice");

                        TextBox txtItemCoupon = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtItemCoupon");

                        TextBox txtBalanceQty = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtBalanceQty");

                        TextBox txtQty = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtQty");
                        HiddenField txtExistingQty = (HiddenField)grvExcessItemDetails.Rows[rowIndex].FindControl("txtExistingQty");

                        //txtRcvdQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtRcvdQty.ClientID + "','" + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "');");
                        txtQty.Attributes.Add("OnChange", "return funReceivedQtyValidation('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtCostPrice.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "','" + txtExistingQty.ClientID + "');");
                        txtItemCoupon.Attributes.Add("OnChange", "return funCalculateCoupon('" + txtQty.ClientID + "','" + txtBalanceQty.ClientID + "','" + txtItemCoupon.ClientID + "','" + dt.Rows[i]["Col8"].ToString() + "','" + txtExistingQty.ClientID + "');");

                        TextBox txtItemCode = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        TextBox txtSerialNo = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtSerialNo");
                        TextBox txtPONumber = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtPONumber");

                        TextBox txtListLessDiscount = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtListLessDiscount");
                        //TextBox txtEDIndicator = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtEDIndicator");
                        //TextBox txtEDValue = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtEDValue");

                        TextBox txtItemLocation = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        TextBox txtTaxPercentage = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtTaxPercentage");
                        TextBox txtItemTaxValue = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtItemTaxValue");
                        TextBox txtHSNCode = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtHSNCode");
                        TextBox txtMRP = (TextBox)grvExcessItemDetails.Rows[rowIndex].FindControl("txtMRP");

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
                        txtHSNCode.Text = dt.Rows[i]["HSN"].ToString();
                        txtMRP.Text = dt.Rows[i]["MRP"].ToString();

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
            }
        }

        private void ShowSummaryExcessItemsInFooter()
        {
            sumListPrice = 0;
            sumCostPrice = 0;
            sumCoupon = 0;
            sumItemTaxValue = 0;

            foreach (GridViewRow gvr in grvExcessItemDetails.Rows)
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

            if (grvExcessItemDetails.FooterRow != null)
            {
                grvExcessItemDetails.FooterRow.Cells[7].Text = "";

                grvExcessItemDetails.FooterRow.Cells[9].Text = sumCostPrice.ToString();
                grvExcessItemDetails.FooterRow.Cells[10].Text = sumCoupon.ToString();
                grvExcessItemDetails.FooterRow.Cells[14].Text = sumItemTaxValue.ToString();
            }
        }

        protected void BtnProcessPO_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnProcessPO.Visible = false;
                InwardEntity inwardEntity = new InwardEntity();
                inwardEntity.ExcessItems = new List<InwardExcessItem>();
                InwardExcessItem inwardItem = null;
                int SNo = 0;
                
                inwardEntity.InwardDate = txtInwardDate.Text;
                inwardEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                inwardEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
                inwardEntity.BranchCode = ddlBranch.SelectedValue.ToString();
                inwardEntity.RoadPermitNumber = txtRoadPermitNo.Text;
                inwardEntity.RoadPermitDate = txtRoadPermitDate.Text;
                inwardEntity.Carrier = txtCarrier.Text;
                inwardEntity.PlaceOfDespatch = txtPlaceOfDespatch.Text;
                inwardEntity.InvoiceNumber = txtInvoiceNo.Text;

                foreach (GridViewRow gr in grvExcessItemDetails.Rows)
                {
                    inwardItem = new InwardExcessItem();
                    SNo += 1;

                    DropDownList ddlCCWHNo = (DropDownList)gr.FindControl("ddlCCWHNo");
                    TextBox txtCCWHNo = (TextBox)gr.FindControl("txtCCWHNo");
                    TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                    TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                    TextBox txtPOQuantity = (TextBox)gr.FindControl("txtPOQuantity");
                    TextBox txtRcvdQty = (TextBox)gr.FindControl("txtRcvdQty");
                    TextBox txtBalanceQty = (TextBox)gr.FindControl("txtBalanceQty");
                    TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                    TextBox txtListPrice = (TextBox)gr.FindControl("txtListPrice");
                    TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");
                    TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                    TextBox txtItemCoupon = (TextBox)gr.FindControl("txtItemCoupon");
                    TextBox txtListLessDiscount = (TextBox)gr.FindControl("txtListLessDiscount");
                    TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                    TextBox txtSerialNo = (TextBox)gr.FindControl("txtSerialNo");
                    TextBox txtPONumber = (TextBox)gr.FindControl("txtPONumber");
                    TextBox txtTaxPercentage = (TextBox)gr.FindControl("txtTaxPercentage");
                    TextBox txtItemTaxValue = (TextBox)gr.FindControl("txtItemTaxValue");
                    TextBox txtHSNCode = (TextBox)gr.FindControl("txtHSNCode");
                    TextBox txtMRP = (TextBox)gr.FindControl("txtMRP");

                    inwardItem.ItemCode = txtItemCode.Text.ToString();
                    inwardItem.ItemLocation = txtItemLocation.Text;
                    inwardItem.POQuantity = txtPOQuantity.Text;
                    inwardItem.ReceivedQuantity = txtRcvdQty.Text;
                    inwardItem.BalanceQuantity = txtBalanceQty.Text;
                    inwardItem.Quantity = txtQty.Text;
                    inwardItem.SupplierPartNumber = txtSupplierPartNo.Text;
                    inwardItem.ListPrice = txtListPrice.Text;
                    inwardItem.CostPrice = txtCostPrice.Text;
                    inwardItem.CostPricePerQty = txtCstPricePerQty.Text;
                    inwardItem.MRP = txtMRP.Text;
                    inwardItem.Coupon = txtItemCoupon.Text;
                    inwardItem.ListLessDiscount = txtListLessDiscount.Text;
                    inwardItem.SNO = SNo.ToString();
                    inwardItem.SerialNo = txtSerialNo.Text;
                    inwardItem.PONumber = txtPONumber.Text;
                    inwardItem.ItemTaxPercentage = txtTaxPercentage.Text;
                    inwardItem.HSN = txtHSNCode.Text;

                    inwardEntity.ExcessItems.Add(inwardItem);
                }

                int result = inwardTransactions.AddNewPOforAutoInwardEntry(ref inwardEntity);

                if (inwardEntity.ErrorCode == "0" && inwardEntity.ErrorMsg == "" && result == 1)
                {
                    string strPONumber = inwardEntity.PO_Number;
                    string strPODate = inwardEntity.PO_Date;
                    DownloadPODetails(strPONumber, strPODate);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Supplementary Order # " + inwardEntity.PO_Number + " Has been Processed successfully. Please download the file and Inform HO for Approval.');", true);                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + inwardEntity.ErrorMsg + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void DownloadPODetails(string strPONumber, string strPODate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {                                
                string filename = "Supplimentary_Order_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + ".xls";

                DataSet ds = new DataSet();
                ds = inwardTransactions.GetMissingItemDetailsforPO(Session["BranchCode"].ToString(), strPONumber, strPODate, ddlSupplierName.SelectedValue, ddlInvoiceNumber.SelectedValue, txtInvoiceDate.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    ArrayList root = new ArrayList();
                    List<Dictionary<string, object>> table;
                    Dictionary<string, object> data;

                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        table = new List<Dictionary<string, object>>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            data = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                data.Add(col.ColumnName, dr[col]);
                            }
                            table.Add(data);
                        }
                        root.Add(table);
                    }

                    hdnJSonExcelData.Value = serializer.Serialize(root);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                Session["strPONumber"] = null;
                Session["strPODate"] = null;
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
            BtnSubmit.Enabled = Fzflag;
            DivHeader.Disabled = !Fzflag;
            InwardPanel.Enabled = Fzflag;

            if ((inwardStatusDuringEdit == "A") && (hdnScreenMode.Value == "E"))
            {
                BtnSubmit.Visible = false;
                BtnSubmit.Enabled = false;
                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    gr.Enabled = false;
                }
            }

            UpdpanelTop.Update();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
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

        private List<IMPALLibrary.Transactions.Item> GetFOCItemCodes()
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetFOCItemCodes(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString());
            return obj;
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetSupplierDepotAutoGRN(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString(), ddlInvoiceNumber.SelectedValue);
            return obj;
        }

        private List<IMPALLibrary.Transactions.Item> GetAutoGRNSupplyInvoices()
        {
            List<IMPALLibrary.Transactions.Item> obj = inwardTransactions.GetAutoGRNSupplierInvoices(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString());
            return obj;
        }
    }
}