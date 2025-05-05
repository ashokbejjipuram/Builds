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
using System.Web.Services;

namespace IMPALWeb
{
    public partial class CostToCostInward : System.Web.UI.Page
    {
        [WebMethod]
        public static void CheckRefNo(string RefNo)
        {
            string refNo = "";
            Page objp = new Page();
            StockTransferTransactions stkrefNo = new StockTransferTransactions();
            refNo = stkrefNo.CheckRefNoExistsCostToCost(RefNo, objp.Session["BranchCode"].ToString());

            if (refNo != "")
            {
                throw new Exception();
            }
        }

        Decimal sumCostPrice = 0;
        Decimal sumTaxValue = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdnScreenMode.Value = "A";

                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlCostToCostInwardNumber.Visible = false;
                    txtIGSTValue.Text = "0.00";
                    hdninterStateStatus.Value = "0";
                    hdnSelItemCode.Value = "";
                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                }

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

                if (ddlCostToCostInwardNumber.SelectedIndex > 0)
                {
                    txtSGSTValue.Enabled = false;
                    txtUTGSTValue.Enabled = false;
                    txtCGSTValue.Enabled = false;
                    txtIGSTValue.Enabled = false;
                }
                else
                {
                    txtSGSTValue.Enabled = false;
                    txtUTGSTValue.Enabled = false;
                    txtCGSTValue.Enabled = false;
                    txtIGSTValue.Enabled = true;
                }
                
                BtnSubmit.Attributes.Add("OnClick", "return CostToCostInwardSubmit();");

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[8].ClientID;
                hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Visible = false;

                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";                    
                    ddlCostToCostInwardNumber.Visible = true;
                    txtCostToCostInwardNumber.Visible = false;
                    //ddlCostToCostInwardNumber.SelectedIndex = 0;
                    ddlCostToCostInwardNumber.DataBind();
                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                    ddlTransactionType.Enabled = false;
                    ddlFromBranch.Enabled = false;
                    txtInvoiceValue.Enabled = false;
                    txtSGSTValue.Enabled = false;
                    txtUTGSTValue.Enabled = false;
                    txtCGSTValue.Enabled = false;
                    txtIGSTValue.Enabled = false;
                    txtCCWHNo.Enabled = false;
                    txtRefStockTransfeDate.Enabled = false;
                    txtLRNumber.Enabled = false;
                    txtLRDate.Enabled = false;
                    txtCarrier.Enabled = false;
                    txtDestination.Enabled = false;
                    txtRoadPermitNo.Enabled = false;
                    txtRoadPermitDate.Enabled = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlCostToCostInwardNumber.Visible = false;
                    txtCostToCostInwardNumber.Visible = true;

                    txtCostToCostInwardNumber.Text = string.Empty;
                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlTransactionType.SelectedValue = "0";
                    ddlFromBranch.SelectedValue = "0";
                    txtInvoiceValue.Text = "";
                    txtIGSTValue.Text = "";
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                    FirstGridViewRow();
                }

                BtnSubmit.Attributes.Add("OnClick", "return CostToCostInwardSubmit();");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");

                if (grvItemDetails.FooterRow != null)
                {
                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[6].ClientID;
                    hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[8].ClientID;
                }

                FreezeOrUnFreezeButtons(true);

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSearch = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtCurrentSearch = (TextBox)grdRow.FindControl("txtSupplierPartNo");
                TextBox txtItemLocation = (TextBox)grdRow.FindControl("txtItemLocation");
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdRow.FindControl("ddlSupplierName"); ;
                DropDownList ddlSupplierPartNo = (DropDownList)grdRow.FindControl("ddlSupplierPartNo");
                TextBox txtOriginalReceiptDate = (TextBox)grdRow.FindControl("txtOriginalReceiptDate");                
                TextBox txtCstPricePerQty = (TextBox)grdRow.FindControl("txtCstPricePerQty");
                TextBox txtReceivedQuantity = (TextBox)grdRow.FindControl("txtReceivedQuantity");
                TextBox txtTotalCostPrice = (TextBox)grdRow.FindControl("txtTotalCostPrice");
                TextBox txtOSLSIndicator = (TextBox)grdRow.FindControl("txtOSLSIndicator");
                TextBox txtGSTPercentage = (TextBox)grdRow.FindControl("txtGSTPercentage");
                TextBox txtInvoiceNumber = (TextBox)grdRow.FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)grdRow.FindControl("txtInvoiceDate");

                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNo.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemLocation.Text = "";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtOriginalReceiptDate.Text = "";
                    txtCstPricePerQty.Text = "";
                    txtReceivedQuantity.Text = "";
                    txtTotalCostPrice.Text = "";
                    txtOSLSIndicator.Text = "";
                    txtGSTPercentage.Text = "";
                    txtInvoiceNumber.Text = "";
                    txtInvoiceDate.Text = "";
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        StockTransferTransactions stTransactions = new StockTransferTransactions();
                        ddlSupplierPartNo.DataSource = (object)stTransactions.GetItemListBySupplierReceipt(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text, ddlBranch.SelectedValue.ToString());
                        ddlSupplierPartNo.DataTextField = "ItemDesc";
                        ddlSupplierPartNo.DataValueField = "ItemCode";
                        ddlSupplierPartNo.DataBind();

                        TextBox txtExistingItemCode = null;
                        TextBox txtExistingItem = null;
                        string strArray = "";

                        foreach (ListItem li in ddlSupplierPartNo.Items)
                        {
                            if (li.Value.ToString() == "0")
                                continue;

                            foreach (GridViewRow gvr in grvItemDetails.Rows)
                            {
                                if (gvr.RowType == DataControlRowType.DataRow)
                                {
                                    txtExistingItemCode = (TextBox)gvr.Cells[5].FindControl("txtItemCode");
                                    if (txtExistingItemCode.Text.ToString() != "" && txtExistingItemCode.Text.ToString() != null)
                                    {
                                        txtExistingItem = (TextBox)gvr.Cells[4].FindControl("txtSupplierPartNo");
                                        if (txtExistingItem.Text.Trim() == li.Value.ToString())
                                        {
                                            strArray += li.Value.ToString() + "|";
                                        }
                                    }
                                }
                            }
                        }
                        //string[] strArr = strArray.Split('|');

                        //foreach (string str in strArr)
                        //{
                        //    if (string.IsNullOrEmpty(str))
                        //        continue;

                        //    ListItem li = ddlSupplierPartNo.Items.FindByValue(str);
                        //    ddlSupplierPartNo.Items.Remove(li);
                        //}

                        ddlSupplierPartNo.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("CostToCostInward.Aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                string supplierCode = ddl.SelectedValue.ToString();
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                if (supplierCode == "0")
                    return;

                txtSupplierPartNo.Focus();
                ShowSummaryInFooter();                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                if (ddl.SelectedValue.ToString() == "0")
                    return;

                string selSupplierPartNo = ddl.SelectedValue.ToString();
                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");

                StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                string[] result = stockTransferTransactions.GetCostToCostItemDetails(ddlSupplierName.SelectedValue, selSupplierPartNo, ddlBranch.SelectedValue.ToString(), ddlFromBranch.SelectedItem.Text.Substring(0, 3), ddlFromBranch.SelectedValue.ToString(), "I");

                if (result != null)
                {
                    TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");

                    TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                    TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                    TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                    TextBox txtTotalCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTotalCostPrice");
                    TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                    HiddenField hdnTaxGroupCode = (HiddenField)grvItemDetails.Rows[rowIndex].FindControl("hdnTaxGroupCode");
                        
                    if (Convert.ToInt16(result[5].ToString()) > 1 && (result[2].ToString() == "0.00%"))
                    {
                        txtReceivedQuantity.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GST% is not Available For this item. Please Check the same');", true);
                    }
                    else
                    {
                        txtItemLocation.Text = result[4].ToString();
                        txtItemCode.Text = result[0].ToString();
                        txtCstPricePerQty.Text = TwoDecimalConversion(result[1].ToString());
                        hdnTaxGroupCode.Value = result[6].ToString();
                        txtReceivedQuantity.Text = "";
                        txtReceivedQuantity.Enabled = true;
                        txtTotalCostPrice.Text = "";
                        txtGSTPercentage.Text = result[2].ToString();                        
                        txtCstPricePerQty.Attributes.Add("OnChange", "return funSTReceiptValidation('" + txtReceivedQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtTotalCostPrice.ClientID + "');");
                        txtReceivedQuantity.Attributes.Add("OnChange", "return funSTReceiptValidation('" + txtReceivedQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtTotalCostPrice.ClientID + "');");
                        txtGSTPercentage.Attributes.Add("OnChange", "return funSTReceiptValidation('" + txtReceivedQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtTotalCostPrice.ClientID + "','" + txtGSTPercentage.ClientID + "');");

                        txtOriginalReceiptDate.Focus();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void ddlCostToCostInwardNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCostToCostInwardNumber.SelectedIndex > 0)
                {
                    StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
                    StockTransferReceiptEntity stockTransferReceiptEntity = stockTransferReceiptTransactions.GetCostToCostDetailsByNumber(Session["BranchCode"].ToString(), ddlCostToCostInwardNumber.SelectedValue.ToString());

                    txtCostToCostInwardNumber.Text = stockTransferReceiptEntity.StockTransferReceiptNumber;
                    txtStockTransferDate.Text = stockTransferReceiptEntity.StockTransferReceiptDate;
                    ddlTransactionType.SelectedValue = stockTransferReceiptEntity.TransactionTypeCode;
                    ddlBranch.SelectedValue = stockTransferReceiptEntity.BranchCode;
                    ddlFromBranch.SelectedValue = stockTransferReceiptEntity.FromBranch;
                    txtSGSTValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.SGSTValue);
                    txtCGSTValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.CGSTValue);
                    txtIGSTValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.IGSTValue);
                    txtUTGSTValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.UTGSTValue);
                    txtInvoiceValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.InvoiceValue);

                    txtLRNumber.Text = stockTransferReceiptEntity.LRNumber;
                    txtLRDate.Text = stockTransferReceiptEntity.LRDate;
                    txtCarrier.Text = stockTransferReceiptEntity.Carrier;
                    txtDestination.Text = stockTransferReceiptEntity.Destination;
                    txtRoadPermitNo.Text = stockTransferReceiptEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = stockTransferReceiptEntity.RoadPermitDate;
                    txtCCWHNo.Text = stockTransferReceiptEntity.RefDocNo;
                    txtRefStockTransfeDate.Text = stockTransferReceiptEntity.RefDocDate;

                    BindExistingRowsDuringEdit(stockTransferReceiptEntity.Items);

                    btnReset.Enabled = true;
                    BtnSubmit.Enabled = false;

                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    btnAddRow.Enabled = false;
                    btnAddRow.Visible = false;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }
                }
                else
                {
                    hdnScreenMode.Value = "E";
                    ddlCostToCostInwardNumber.SelectedValue = "0";
                    ddlCostToCostInwardNumber.Visible = true;
                    txtCostToCostInwardNumber.Visible = false;
                    ddlCostToCostInwardNumber.DataBind();
                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void ddlFromBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFromBranch.SelectedIndex > 0)
                {
                    StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
                    int STDNinterStateStatus = stockTransferReceiptTransactions.GetCOSTinterStateStatus(ddlBranch.SelectedValue, ddlFromBranch.SelectedItem.Text.Substring(0, 3), ddlFromBranch.SelectedValue);

                    hdninterStateStatus.Value = STDNinterStateStatus.ToString();
                }
                else
                {
                    hdninterStateStatus.Value = "0";
                }

                if (hdninterStateStatus.Value != "0")
                {
                    txtSGSTValue.Text = "0.00";
                    txtUTGSTValue.Text = "0.00";
                    txtCGSTValue.Text = "0.00";
                    txtIGSTValue.Text = "0.00";

                    if (hdninterStateStatus.Value == "2")
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

                FirstGridViewRow();

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                if (btnAddRow != null)
                    btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Visible = false;
                SubmitHeaderAndItems();
                ShowSummaryInFooter();
                STDNReceiptPanel.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
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
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                List<CCWH> obj = new List<CCWH>();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((ddlCostToCostInwardNumber.Visible) && ddlCostToCostInwardNumber.SelectedIndex > 0)
                    {
                        Button btnSearch = (Button)e.Row.FindControl("btnSearch");
                        btnSearch.Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
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

                    ViewState["CurrentTable"] = dt;
                    ViewState["GridRowCount"] = dt.Rows.Count;
                    hdnRowCnt.Value = dt.Rows.Count.ToString();

                    grvItemDetails.DataSource = dt;
                    grvItemDetails.DataBind();

                    SetPreviousData();
                    HideDllItemCodeDropDownForDisplayOnly();
                    ShowSummaryInFooter();
                }
                else
                {
                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CostToCostInward), exp);
            }
        }

        private void FillLastRowInfoInGrid(ref DataTable dt)
        {
            int rowIndex = grvItemDetails.Rows.Count - 1;

            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
            TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");

            DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

            TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
            TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
            TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
            TextBox txtTotalCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTotalCostPrice");

            DropDownList ddlOSLSIndicator = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlOSLSIndicator");
            TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");

            TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
            TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
            TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

            if ((ddlSupplierName.Visible) && (ddlSupplierPartNo.Visible))
            {
                if ((ddlSupplierPartNo.SelectedValue.ToString() != "0") && (ddlSupplierPartNo.SelectedValue.ToString() != "0") && (txtReceivedQuantity.Text != "") && (txtReceivedQuantity.Text != "0"))
                {
                    dt.Rows[rowIndex]["Col1"] = ddlSupplierName.SelectedItem.Text;
                    dt.Rows[rowIndex]["Col2"] = ddlSupplierPartNo.SelectedItem.Text;
                    dt.Rows[rowIndex]["Col3"] = txtItemLocation.Text;
                    dt.Rows[rowIndex]["Col4"] = txtItemCode.Text;
                    dt.Rows[rowIndex]["Col5"] = txtCstPricePerQty.Text;
                    dt.Rows[rowIndex]["Col6"] = txtOriginalReceiptDate.Text;
                    dt.Rows[rowIndex]["Col7"] = txtReceivedQuantity.Text;
                    dt.Rows[rowIndex]["Col8"] = txtTotalCostPrice.Text;
                    dt.Rows[rowIndex]["Col9"] = ddlOSLSIndicator.SelectedItem.Text;
                    dt.Rows[rowIndex]["Col10"] = txtGSTPercentage.Text;
                    dt.Rows[rowIndex]["Col11"] = txtInvoiceNumber.Text;
                    dt.Rows[rowIndex]["Col12"] = txtInvoiceDate.Text;
                }
            }
        }

        private void SubmitHeaderAndItems()
        {
            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            stockTransferReceiptEntity.BranchCode = ddlBranch.SelectedValue.ToString();

            if (ddlCostToCostInwardNumber.Visible)
                stockTransferReceiptEntity.StockTransferReceiptNumber = ddlCostToCostInwardNumber.SelectedValue.ToString();
            else
                stockTransferReceiptEntity.StockTransferReceiptNumber = txtCostToCostInwardNumber.Text.ToString();

            stockTransferReceiptEntity.StockTransferReceiptDate = txtStockTransferDate.Text.ToString();
            stockTransferReceiptEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
            stockTransferReceiptEntity.GSTSupplierCode = ddlFromBranch.SelectedValue.ToString();
            stockTransferReceiptEntity.FromBranch = ddlFromBranch.SelectedItem.Text.Substring(0, 3);
            stockTransferReceiptEntity.InvoiceValue = txtInvoiceValue.Text.ToString();
            stockTransferReceiptEntity.SGSTValue = txtSGSTValue.Text;
            stockTransferReceiptEntity.CGSTValue = txtCGSTValue.Text;
            stockTransferReceiptEntity.IGSTValue = txtIGSTValue.Text;
            stockTransferReceiptEntity.UTGSTValue = txtUTGSTValue.Text;
            stockTransferReceiptEntity.LRNumber = txtLRNumber.Text.ToString();
            stockTransferReceiptEntity.LRDate = txtLRDate.Text.ToString();
            stockTransferReceiptEntity.Carrier = txtCarrier.Text.ToString();
            stockTransferReceiptEntity.Destination = txtDestination.Text.ToString();
            stockTransferReceiptEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
            stockTransferReceiptEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();
            stockTransferReceiptEntity.RefDocNo = txtCCWHNo.Text.ToString();
            stockTransferReceiptEntity.RefDocDate = txtRefStockTransfeDate.Text.ToString();
            stockTransferReceiptEntity.interStateStatus = hdninterStateStatus.Value;

            StockTransferReceiptItem stockTransferReceiptItem = null;
            int SNo = 0;
            decimal dmlTotal = 0;

            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                stockTransferReceiptItem = new StockTransferReceiptItem();
                SNo += 1;

                DropDownList ddlSupplierName = (DropDownList)gr.FindControl("ddlSupplierName");
                TextBox txtSupplierName = (TextBox)gr.FindControl("txtSupplierName");

                TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                TextBox txtOriginalReceiptDate = (TextBox)gr.FindControl("txtOriginalReceiptDate");
                TextBox txtReceivedQuantity = (TextBox)gr.FindControl("txtReceivedQuantity");
                TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                TextBox txtTotalCostPrice = (TextBox)gr.FindControl("txtTotalCostPrice");

                DropDownList ddlOSLSIndicator = (DropDownList)gr.FindControl("ddlOSLSIndicator");
                TextBox txtOSLSIndicator = (TextBox)gr.FindControl("txtOSLSIndicator");
                TextBox txtGSTPercentage = (TextBox)gr.FindControl("txtGSTPercentage");
                TextBox txtInvoiceNumber = (TextBox)gr.FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)gr.FindControl("txtInvoiceDate");

                dmlTotal += Convert.ToDecimal(txtCstPricePerQty.Text);

                stockTransferReceiptItem.SNo = SNo.ToString();
                stockTransferReceiptItem.ItemLocation = txtItemLocation.Text.ToString();
                stockTransferReceiptItem.ItemCode = txtItemCode.Text.ToString();
                stockTransferReceiptItem.ReceivedQuantity = txtReceivedQuantity.Text;
                stockTransferReceiptItem.CostPricePerQty = txtCstPricePerQty.Text;
                stockTransferReceiptItem.STDNValue = txtTotalCostPrice.Text;
                stockTransferReceiptItem.OriginalReceiptDate = txtOriginalReceiptDate.Text;

                if (txtGSTPercentage.Text.Contains("%"))
                {
                    txtGSTPercentage.Text = txtGSTPercentage.Text.Replace("%", "");
                }

                stockTransferReceiptItem.GSTPercentage = txtGSTPercentage.Text;
                stockTransferReceiptItem.InvoiceNumber = txtInvoiceNumber.Text;
                stockTransferReceiptItem.InvoiceDate = txtInvoiceDate.Text;

                if (ddlOSLSIndicator.Visible)
                {
                    stockTransferReceiptItem.OSLSIndicator = ddlOSLSIndicator.SelectedItem.Text;
                }
                else
                {
                    stockTransferReceiptItem.OSLSIndicator = txtOSLSIndicator.Text;
                }

                stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
            }

            StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
            if (hdnScreenMode.Value == "A")
            {
                stockTransferReceiptTransactions.AddNewCostToCostInwardEntry(ref stockTransferReceiptEntity);
                if ((stockTransferReceiptEntity.ErrorMsg == string.Empty) && (stockTransferReceiptEntity.ErrorCode == "0"))
                {
                    txtCostToCostInwardNumber.Text = stockTransferReceiptEntity.StockTransferReceiptNumber;
                    FreezeOrUnFreezeButtons(false);
                    btnReset.Enabled = true;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }

                    UpdpanelTop.Update();
                    UpdPanelGrid.Update();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Cost to Cost Inward entry has been saved successfully.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + stockTransferReceiptEntity.ErrorMsg + "');", true);
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
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 13;
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        private void BindExistingRowsDuringEdit(List<StockTransferReceiptItem> lstStockTransferItem)
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

            DataRow dr = null;
            for (int i = 0; i < lstStockTransferItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstStockTransferItem[i].SNo.ToString();
                dr["Col1"] = lstStockTransferItem[i].SupplierLineCode.ToString();
                dr["Col2"] = lstStockTransferItem[i].SupplierPartNo.ToString();
                dr["Col3"] = lstStockTransferItem[i].ItemLocation.ToString();
                dr["Col4"] = lstStockTransferItem[i].ItemCode.ToString();
                dr["Col5"] = TwoDecimalConversion(lstStockTransferItem[i].CostPricePerQty.ToString());
                dr["Col6"] = lstStockTransferItem[i].OriginalReceiptDate.ToString();
                dr["Col7"] = lstStockTransferItem[i].ReceivedQuantity.ToString();
                dr["Col8"] = TwoDecimalConversion(lstStockTransferItem[i].TotalCostPrice.ToString());
                dr["Col9"] = lstStockTransferItem[i].OSLSIndicator.ToString();
                dr["Col10"] = TwoDecimalConversion(lstStockTransferItem[i].GSTPercentage.ToString());
                dr["Col11"] = lstStockTransferItem[i].InvoiceNumber.ToString();
                dr["Col12"] = lstStockTransferItem[i].InvoiceDate.ToString();
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCnt.Value = dt.Rows.Count.ToString();

            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();
            SetPreviousData();
            HideDllItemCodeDropDownForDisplayOnly();

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
                            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                            TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");

                            DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                            TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
                            TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                            TextBox txtTotalCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTotalCostPrice");

                            DropDownList ddlOSLSIndicator = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlOSLSIndicator");
                            TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");

                            TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                            TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                            TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            if (ddlSupplierName.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Col1"] = ddlSupplierName.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Col1"] = txtSupplierName.Text.ToString();
                            }

                            if (ddlSupplierPartNo.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Col2"] = ddlSupplierPartNo.SelectedValue;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Col2"] = txtSupplierPartNo.Text.ToString();
                            }

                            dtCurrentTable.Rows[i - 1]["Col3"] = txtItemLocation.Text;
                            dtCurrentTable.Rows[i - 1]["Col4"] = txtItemCode.Text;
                            dtCurrentTable.Rows[i - 1]["Col5"] = txtCstPricePerQty.Text;
                            dtCurrentTable.Rows[i - 1]["Col6"] = txtOriginalReceiptDate.Text;

                            dtCurrentTable.Rows[i - 1]["Col7"] = txtReceivedQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Col8"] = txtTotalCostPrice.Text;

                            if (ddlOSLSIndicator.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Col9"] = ddlOSLSIndicator.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Col9"] = txtOSLSIndicator.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["Col10"] = txtGSTPercentage.Text;
                            dtCurrentTable.Rows[i - 1]["Col11"] = txtInvoiceNumber.Text;
                            dtCurrentTable.Rows[i - 1]["Col12"] = txtInvoiceDate.Text;

                            rowIndex++;
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();

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

            for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
            {
                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");

                DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                Button btnSearch = (Button)grvItemDetails.Rows[rowIndex].FindControl("btnSearch");
                TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
                DropDownList ddlOSLSIndicator = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlOSLSIndicator");
                TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");
                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                if (rowIndex != grvItemDetails.Rows.Count - 1)
                {
                    ddlSupplierName.Visible = false;
                    ddlSupplierPartNo.Visible = false;
                    ddlOSLSIndicator.Visible = false;
                    btnSearch.Visible = false;
                    txtSupplierName.Enabled = false;
                    txtItemLocation.Enabled = false;
                    txtSupplierPartNo.Enabled = false;
                    txtCstPricePerQty.Enabled = false;
                    txtReceivedQuantity.Enabled = false;
                    txtOriginalReceiptDate.Enabled = false;
                    txtOSLSIndicator.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                }
                else
                {
                    txtSupplierName.Visible = false;
                    txtOSLSIndicator.Visible = false;
                }

                sb.Append("" + ddlSupplierName.ClientID + "," + txtSupplierPartNo.ClientID + "," + ddlSupplierPartNo.ClientID + "," + txtItemCode.ClientID + "," + txtOriginalReceiptDate.ClientID + "," + txtCstPricePerQty.ClientID + "," + txtReceivedQuantity.ClientID + "," + txtInvoiceNumber.ClientID + "," + txtInvoiceDate.ClientID + ",");
            }
            txtHdnGridCtrls.Text = sb.ToString();
        }

        private void HideDllItemCodeDropDownForDisplayOnly()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
            {
                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
                DropDownList ddlOSLSIndicator = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlOSLSIndicator");
                TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");
                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                {
                    ddlSupplierName.Visible = false;
                    ddlSupplierPartNo.Visible = false;
                    ddlOSLSIndicator.Visible = false;

                    txtSupplierName.Visible = true;
                    txtSupplierPartNo.Visible = true;
                    txtOSLSIndicator.Visible = true;

                    txtSupplierName.Enabled = false;
                    txtSupplierPartNo.Enabled = false;
                    txtItemLocation.Enabled = false;
                    txtCstPricePerQty.Enabled = false;
                    txtReceivedQuantity.Enabled = false;
                    txtOriginalReceiptDate.Enabled = false;
                    txtOSLSIndicator.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                }

                sb.Append("" + ddlSupplierName.ClientID + "," + txtSupplierPartNo.ClientID + "," + ddlSupplierPartNo.ClientID + "," + txtItemCode.ClientID + "," + txtOriginalReceiptDate.ClientID + "," + txtCstPricePerQty.ClientID + "," + txtReceivedQuantity.ClientID + "," + txtInvoiceNumber.ClientID + "," + txtInvoiceDate.ClientID + ",");
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
                        DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                        TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");

                        DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                        TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                        TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
                        TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                        TextBox txtTotalCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTotalCostPrice");
                        DropDownList ddlOSLSIndicator = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlOSLSIndicator");
                        TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");
                        TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                        TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                        TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                        txtSupplierName.Text = dt.Rows[i]["Col1"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i]["Col2"].ToString();
                        txtItemLocation.Text = dt.Rows[i]["Col3"].ToString();
                        txtItemCode.Text = dt.Rows[i]["Col4"].ToString();
                        txtCstPricePerQty.Text = dt.Rows[i]["Col5"].ToString();
                        txtOriginalReceiptDate.Text = dt.Rows[i]["Col6"].ToString();
                        txtReceivedQuantity.Text = dt.Rows[i]["Col7"].ToString();
                        txtTotalCostPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col8"].ToString());
                        txtOSLSIndicator.Text = dt.Rows[i]["Col9"].ToString();
                        txtGSTPercentage.Text = dt.Rows[i]["Col10"].ToString();
                        txtInvoiceNumber.Text = dt.Rows[i]["Col11"].ToString();
                        txtInvoiceDate.Text = dt.Rows[i]["Col12"].ToString();

                        rowIndex++;
                    }

                }
            }
        }

        private void ShowSummaryInFooter()
        {
            foreach (GridViewRow gvr in grvItemDetails.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtTotalCostPrice = (TextBox)gvr.FindControl("txtTotalCostPrice");
                    TextBox txtGSTPercentage = (TextBox)gvr.FindControl("txtGSTPercentage");

                    if (txtGSTPercentage.Text.Contains("%"))
                    {
                        txtGSTPercentage.Text = txtGSTPercentage.Text.Replace("%", "");
                    }

                    if (string.IsNullOrEmpty(txtTotalCostPrice.Text))
                        txtTotalCostPrice.Text = "0";
                    if (string.IsNullOrEmpty(txtGSTPercentage.Text))
                        txtGSTPercentage.Text = "0";

                    sumCostPrice += Convert.ToDecimal(txtTotalCostPrice.Text);
                    sumTaxValue += (Convert.ToDecimal(txtTotalCostPrice.Text) * Convert.ToDecimal(txtGSTPercentage.Text))/100;
                }
            }

            if (grvItemDetails.FooterRow != null)
            {
                grvItemDetails.FooterRow.Cells[8].Text = TwoDecimalConversion(sumCostPrice.ToString());
                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[6].ClientID;

                grvItemDetails.FooterRow.Cells[10].Text = TwoDecimalConversion(sumTaxValue.ToString());
                hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[8].ClientID;
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string DecimalToIntConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0";
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

            foreach (GridViewRow gvr in grvItemDetails.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    //ImageButton ImgOriginalReceiptDate = (ImageButton)gvr.FindControl("ImgOriginalReceiptDate");
                    //ImageButton ImgRefStockTransfeDate = (ImageButton)gvr.FindControl("ImgRefStockTransfeDate");

                    //if (ImgOriginalReceiptDate != null)
                    //    ImgOriginalReceiptDate.Enabled = Fzflag;

                    //if (ImgRefStockTransfeDate != null)
                    //    ImgRefStockTransfeDate.Enabled = Fzflag;
                }
            }

            btnReset.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
            DivHeader.Disabled = !Fzflag;
            imgEditToggle.Enabled = Fzflag;
            UpdpanelTop.Update();
            UpdPanelGrid.Update();

        }
    }   
}
