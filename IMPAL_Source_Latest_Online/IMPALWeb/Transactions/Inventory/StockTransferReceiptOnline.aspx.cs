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
    public partial class StockTransferReceiptOnline : System.Web.UI.Page
    {
        [WebMethod]
        public static void CheckRefNo(string RefNo)
        {
            string refNo = "";
            Page objp = new Page();
            StockTransferTransactions stkrefNo = new StockTransferTransactions();
            refNo = stkrefNo.CheckRefNoExists(RefNo, objp.Session["BranchCode"].ToString());

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
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdnScreenMode.Value = "A";
                    hdnReceivedStatus.Value = "";

                    ddlSTDNNumberOnline.Enabled = true;
                    txtStockTransferFromDate.Text = "";
                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlStockTransferReceiptNumber.Visible = false;
                    txtIGSTValue.Text = "0.00";
                    hdninterStateStatus.Value = "0";
                    hdnSelItemCode.Value = "";

                    GetHOSTDNEntriesOnline();                   

                    FreezeOrUnFreezeButtons(true);                    
                    LoadSTDNReceiptEntriesOnline();
                    DefineBranch();
                }

                BtnSubmit.Attributes.Add("OnClick", "return StockTransferReceiptSubmit('W');");

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[12].ClientID;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSTDNNumberOnline.Enabled = false;

                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    //ddlStockTransferReceiptNumber.SelectedIndex = 0;
                    ddlStockTransferReceiptNumber.Visible = true;
                    txtStockTransferReceiptNumber.Visible = false;
                    ddlStockTransferReceiptNumber.DataBind();
                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlStockTransferReceiptNumber.Visible = false;
                    txtStockTransferReceiptNumber.Visible = true;

                    txtStockTransferReceiptNumber.Text = string.Empty;
                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlFromBranch.SelectedIndex = 0;
                    txtInvoiceValue.Text = "";
                    txtIGSTValue.Text = "";
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                    FirstGridViewRow();
                }

                BtnSubmit.Attributes.Add("OnClick", "return StockTransferReceiptSubmit('W');");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                if (grvItemDetails.FooterRow != null)
                {
                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[12].ClientID;
                }

                FreezeOrUnFreezeButtons(true);

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        private void GetHOSTDNEntriesOnline()
        {
            StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
            List<IMPALLibrary.Transactions.Item> HOSTDNs = stockTransferTransactions.GetHOSTDNDespatchEntriesEDP(Session["BranchCode"].ToString());

            if (HOSTDNs.Count > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HO STDNs are in pending for STDN Outward to Other Branches. Please Check and Complete the same.');", true);
                ddlSTDNNumberOnline.Enabled = false;
                BtnSubmit.Visible = false;
                return;
            }
            else
            {
                ddlSTDNNumberOnline.Enabled = true;
                BtnSubmit.Visible = true;
            }
        }

        private void LoadSTDNReceiptEntriesOnline()
        {
            StockTransferReceiptTransactions stTransactions = new StockTransferReceiptTransactions();
            ddlSTDNNumberOnline.DataSource = (object)stTransactions.GetSTDNReceiptEntriesOnline(Session["BranchCode"].ToString());
            ddlSTDNNumberOnline.DataTextField = "ItemCode";
            ddlSTDNNumberOnline.DataValueField = "ItemCode";
            ddlSTDNNumberOnline.DataBind();
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferReceiptOnline.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void ddlStockTransferReceiptNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStockTransferReceiptNumber.SelectedIndex > 0)
                {
                    StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
                    StockTransferReceiptEntity stockTransferReceiptEntity = stockTransferReceiptTransactions.GetSTDNReceiptDetailsByNumber(Session["BranchCode"].ToString(), ddlStockTransferReceiptNumber.SelectedValue.ToString());

                    txtStockTransferReceiptNumber.Text = stockTransferReceiptEntity.StockTransferReceiptNumber;
                    txtStockTransferDate.Text = stockTransferReceiptEntity.StockTransferReceiptDate;
                    ddlTransactionType.SelectedValue = stockTransferReceiptEntity.TransactionTypeCode;
                    ddlBranch.SelectedValue = stockTransferReceiptEntity.BranchCode;
                    ddlFromBranch.SelectedValue = stockTransferReceiptEntity.FromBranch;
                    txtIGSTValue.Text = FourDecimalConversion(stockTransferReceiptEntity.IGSTValue);
                    txtInvoiceValue.Text = FourDecimalConversion(stockTransferReceiptEntity.InvoiceValue);

                    txtLRNumber.Text = stockTransferReceiptEntity.LRNumber;
                    txtLRDate.Text = stockTransferReceiptEntity.LRDate;
                    txtCarrier.Text = stockTransferReceiptEntity.Carrier;
                    txtDestination.Text = stockTransferReceiptEntity.Destination;
                    txtRoadPermitNo.Text = stockTransferReceiptEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = stockTransferReceiptEntity.RoadPermitDate;
                    hdnReceivedStatus.Value = stockTransferReceiptEntity.ReceivedStatus;

                    BindExistingRows(stockTransferReceiptEntity.Items);                    

                    btnReset.Enabled = true;
                    BtnSubmit.Enabled = false;
                    txtLRNumber.Enabled = false;
                    txtLRDate.Enabled = false;
                    txtCarrier.Enabled = false;
                    txtDestination.Enabled = false;
                    txtRoadPermitNo.Enabled = false;
                    txtRoadPermitDate.Enabled = false;
                    txtWareHouseNo.Enabled = false;
                    txtWarehouseDate.Enabled = false;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }
                }
                else
                {
                    hdnScreenMode.Value = "E";
                    ddlStockTransferReceiptNumber.SelectedValue = "0";
                    ddlStockTransferReceiptNumber.Visible = true;
                    txtStockTransferReceiptNumber.Visible = false;
                    ddlStockTransferReceiptNumber.DataBind();
                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                }

                if (hdnReceivedStatus.Value == "A")
                    BtnSubmit.Visible = true;
                else
                    BtnSubmit.Visible = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void ddlSTDNNumberOnline_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSTDNNumberOnline.SelectedIndex > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
                    int STDNinterStateStatus = stockTransferReceiptTransactions.GetSTDNinterStateStatus(ddlBranch.SelectedValue, ddlSTDNNumberOnline.SelectedValue.Substring(11, 3));
                    hdninterStateStatus.Value = STDNinterStateStatus.ToString();  

                    imgEditToggle.Visible = false;
                    
                    StockTransferReceiptEntity stockTransferReceiptEntity = stockTransferReceiptTransactions.GetSTDNDetailsOnline(ddlSTDNNumberOnline.SelectedValue.Substring(11, 3), ddlSTDNNumberOnline.SelectedValue.ToString());

                    hdnSTDNDate.Value = stockTransferReceiptEntity.StockTransferReceiptDate;
                    txtStockTransferFromDate.Text = stockTransferReceiptEntity.StockTransferReceiptDate;
                    txtStockTransferReceiptNumber.Text = "";
                    txtStockTransferDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlTransactionType.SelectedValue = stockTransferReceiptEntity.TransactionTypeCode;
                    ddlBranch.SelectedValue = stockTransferReceiptEntity.BranchCode;
                    ddlFromBranch.SelectedValue = stockTransferReceiptEntity.FromBranch;
                    txtIGSTValue.Text = FourDecimalConversion(stockTransferReceiptEntity.IGSTValue);
                    txtInvoiceValue.Text = FourDecimalConversion(stockTransferReceiptEntity.InvoiceValue);
                    txtLRNumber.Text = stockTransferReceiptEntity.LRNumber;
                    txtLRDate.Text = stockTransferReceiptEntity.LRDate;
                    txtCarrier.Text = stockTransferReceiptEntity.Carrier;
                    txtDestination.Text = stockTransferReceiptEntity.Destination;
                    txtRoadPermitNo.Text = stockTransferReceiptEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = stockTransferReceiptEntity.RoadPermitDate;

                    BindExistingRowsDuringEdit(stockTransferReceiptEntity.Items);

                    btnReset.Enabled = true;
                    BtnSubmit.Enabled = true;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        TextBox txtSupplierName = (TextBox)gr.FindControl("txtSupplierName");
                        TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                        TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                        TextBox txtOriginalReceiptDate = (TextBox)gr.FindControl("txtOriginalReceiptDate");
                        TextBox txtListPrice = (TextBox)gr.FindControl("txtListPrice");
                        TextBox txtReceivedQuantity = (TextBox)gr.FindControl("txtReceivedQuantity");
                        TextBox txtAcceptedQuantity = (TextBox)gr.FindControl("txtAcceptedQuantity");
                        TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                        TextBox txtTotalCostPrice = (TextBox)gr.FindControl("txtTotalCostPrice");
                        TextBox txtOSLSIndicator = (TextBox)gr.FindControl("txtOSLSIndicator");
                        TextBox txtGSTPercentage = (TextBox)gr.FindControl("txtGSTPercentage");
                        TextBox txtInvoiceNumber = (TextBox)gr.FindControl("txtInvoiceNumber");

                        txtAcceptedQuantity.Attributes.Add("OnChange", "return funAcceptedQtyValidation('" + txtReceivedQuantity.ClientID + "','" + txtAcceptedQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtTotalCostPrice.ClientID + "');");

                        txtSupplierName.Enabled = false;
                        txtItemLocation.Enabled = true;
                        txtSupplierPartNo.Enabled = false;
                        txtOriginalReceiptDate.Enabled = false;
                        txtReceivedQuantity.Enabled = false;
                        txtAcceptedQuantity.Enabled = true;
                        txtListPrice.Enabled = false;
                        txtCstPricePerQty.Enabled = false;
                        txtTotalCostPrice.Enabled = false;
                        txtOSLSIndicator.Enabled = false;
                        txtGSTPercentage.Enabled = false;
                        txtInvoiceNumber.Enabled = true;
                        txtInvoiceNumber.Enabled = true;

                        sb.Append("" + txtSupplierPartNo.ClientID + "," + txtOriginalReceiptDate.ClientID + "," + txtCstPricePerQty.ClientID + "," + txtReceivedQuantity.ClientID + "," + txtAcceptedQuantity.ClientID + "," + txtItemLocation.ClientID + ",");
                    }

                    txtHdnGridCtrls.Text = sb.ToString();
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("StockTransferReceiptOnline.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Enabled = false;
                SubmitHeaderAndItems();
                ShowSummaryInFooter();
                ddlSTDNNumberOnline.Enabled = false;
                STDNReceiptPanel.Enabled = false;
                pnlCI.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                List<CCWH> obj = new List<CCWH>();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptOnline), exp);
            }
        }

        private void SubmitHeaderAndItems()
        {
            StockTransferReceiptEntity stockTransferReceiptEntity = new StockTransferReceiptEntity();
            stockTransferReceiptEntity.Items = new List<StockTransferReceiptItem>();

            stockTransferReceiptEntity.BranchCode = ddlBranch.SelectedValue.ToString();

            if (ddlStockTransferReceiptNumber.Visible)
                stockTransferReceiptEntity.StockTransferReceiptNumber = ddlStockTransferReceiptNumber.SelectedValue.ToString();
            else
                stockTransferReceiptEntity.StockTransferReceiptNumber = txtStockTransferReceiptNumber.Text.ToString();

            stockTransferReceiptEntity.StockTransferReceiptDate = txtStockTransferDate.Text.ToString();
            stockTransferReceiptEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
            stockTransferReceiptEntity.FromBranch = ddlFromBranch.SelectedValue.ToString();
            stockTransferReceiptEntity.InvoiceValue = txtInvoiceValue.Text.ToString();
            stockTransferReceiptEntity.IGSTValue = txtIGSTValue.Text.ToString();
            stockTransferReceiptEntity.WareHouseNo = txtWareHouseNo.Text.ToString();
            stockTransferReceiptEntity.WarehouseDate = txtWarehouseDate.Text.ToString();
            stockTransferReceiptEntity.LRNumber = txtLRNumber.Text.ToString();
            stockTransferReceiptEntity.LRDate = txtLRDate.Text.ToString();
            stockTransferReceiptEntity.Carrier = txtCarrier.Text.ToString();
            stockTransferReceiptEntity.Destination = txtDestination.Text.ToString();
            stockTransferReceiptEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
            stockTransferReceiptEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();
            stockTransferReceiptEntity.RefDocNo = ddlSTDNNumberOnline.SelectedValue;
            stockTransferReceiptEntity.RefDocDate = txtStockTransferFromDate.Text;

            StockTransferReceiptItem stockTransferReceiptItem = null;
            int SNo = 0;
            decimal dmlTotal = 0;

            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                stockTransferReceiptItem = new StockTransferReceiptItem();
                SNo += 1;

                TextBox txtSupplierName = (TextBox)gr.FindControl("txtSupplierName");
                TextBox txtItemLocation = (TextBox)gr.FindControl("txtItemLocation");
                TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                TextBox txtOriginalReceiptDate = (TextBox)gr.FindControl("txtOriginalReceiptDate");
                TextBox txtReceivedQuantity = (TextBox)gr.FindControl("txtReceivedQuantity");
                TextBox txtAcceptedQuantity = (TextBox)gr.FindControl("txtAcceptedQuantity");
                TextBox txtListPrice = (TextBox)gr.FindControl("txtListPrice");
                TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                TextBox txtTotalCostPrice = (TextBox)gr.FindControl("txtTotalCostPrice");
                TextBox txtOSLSIndicator = (TextBox)gr.FindControl("txtOSLSIndicator");
                TextBox txtInvoiceNumber = (TextBox)gr.FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)gr.FindControl("txtInvoiceDate");
                TextBox hdnConsInwardNo = (TextBox)gr.FindControl("hdnConsInwardNo");
                TextBox hdnConsSerialNo = (TextBox)gr.FindControl("hdnConsSerialNo");

                dmlTotal += Convert.ToDecimal(txtCstPricePerQty.Text);

                stockTransferReceiptItem.SNo = SNo.ToString();
                stockTransferReceiptItem.ItemLocation = txtItemLocation.Text.ToString();
                stockTransferReceiptItem.ItemCode = txtItemCode.Text.ToString();
                stockTransferReceiptItem.SupplierPartNo = txtSupplierPartNo.Text.ToString();
                stockTransferReceiptItem.ReceivedQuantity = txtReceivedQuantity.Text;
                stockTransferReceiptItem.AcceptedQuantity = txtAcceptedQuantity.Text;
                stockTransferReceiptItem.CostPricePerQty = txtCstPricePerQty.Text;
                stockTransferReceiptItem.ListPrice = txtListPrice.Text;
                stockTransferReceiptItem.STDNValue = txtTotalCostPrice.Text;
                stockTransferReceiptItem.OriginalReceiptDate = txtOriginalReceiptDate.Text;
                stockTransferReceiptItem.InvoiceNumber = txtInvoiceNumber.Text;
                stockTransferReceiptItem.InvoiceDate = txtInvoiceDate.Text;
                stockTransferReceiptItem.OSLSIndicator = txtOSLSIndicator.Text;
                stockTransferReceiptItem.ConsInwardNo = hdnConsInwardNo.Text;
                stockTransferReceiptItem.ConsSerialNo = hdnConsSerialNo.Text;

                stockTransferReceiptEntity.Items.Add(stockTransferReceiptItem);
            }

            StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
            if (hdnScreenMode.Value == "A")
            {
                stockTransferReceiptTransactions.AddNewSTReceiptEntryOnline(ref stockTransferReceiptEntity);
                if ((stockTransferReceiptEntity.ErrorMsg == string.Empty) && (stockTransferReceiptEntity.ErrorCode == "0"))
                {
                    txtStockTransferReceiptNumber.Text = stockTransferReceiptEntity.StockTransferReceiptNumber;
                    FreezeOrUnFreezeButtons(false);
                    btnReset.Enabled = true;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }

                    UpdpanelTop.Update();
                    UpdPanelGrid.Update();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer receipt entry details has been saved successfully.');", true);
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
            dt.Columns.Add(new DataColumn("ProductGroupCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsInwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsSerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));            

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
                dr["ProductGroupCode"] = string.Empty;
                dr["Col13"] = string.Empty;
                dr["ConsInwardNo"] = string.Empty;
                dr["ConsSerialNo"] = string.Empty;
                dr["Col14"] = string.Empty;                
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 16;
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
            dt.Columns.Add(new DataColumn("ProductGroupCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsInwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsSerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));            

            DataRow dr = null;
            for (int i = 0; i < lstStockTransferItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstStockTransferItem[i].SNo.ToString();
                dr["Col1"] = lstStockTransferItem[i].SupplierLineCode.ToString();
                dr["Col2"] = lstStockTransferItem[i].SupplierPartNo.ToString();
                dr["Col3"] = lstStockTransferItem[i].ItemCode.ToString();
                dr["Col4"] = FourDecimalConversion(lstStockTransferItem[i].CostPricePerQty.ToString());
                dr["Col5"] = lstStockTransferItem[i].OriginalReceiptDate.ToString();
                dr["Col6"] = FourDecimalConversion(lstStockTransferItem[i].ListPrice.ToString());
                dr["Col7"] = lstStockTransferItem[i].ReceivedQuantity.ToString();
                dr["Col8"] = "";
                dr["Col9"] = lstStockTransferItem[i].ItemLocation.ToString();
                dr["Col10"] = FourDecimalConversion(lstStockTransferItem[i].TotalCostPrice.ToString());
                dr["Col11"] = lstStockTransferItem[i].OSLSIndicator.ToString();
                dr["Col12"] = FourDecimalConversion(lstStockTransferItem[i].GSTPercentage.ToString());
                dr["ProductGroupCode"] = lstStockTransferItem[i].ProductGroupCode.ToString();
                dr["Col13"] = "";
                dr["ConsInwardNo"] = lstStockTransferItem[i].ConsInwardNo.ToString();
                dr["ConsSerialNo"] = lstStockTransferItem[i].ConsSerialNo.ToString();
                dr["Col14"] = "";
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCnt.Value = dt.Rows.Count.ToString();

            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();
            SetPreviousData();

            ShowSummaryInFooter();
        }

        private void BindExistingRows(List<StockTransferReceiptItem> lstStockTransferItem)
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
            dt.Columns.Add(new DataColumn("ProductGroupCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsInwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ConsSerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));            

            DataRow dr = null;
            for (int i = 0; i < lstStockTransferItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstStockTransferItem[i].SNo.ToString();
                dr["Col1"] = lstStockTransferItem[i].SupplierLineCode.ToString();
                dr["Col2"] = lstStockTransferItem[i].SupplierPartNo.ToString();
                dr["Col3"] = lstStockTransferItem[i].ItemCode.ToString();
                dr["Col4"] = FourDecimalConversion(lstStockTransferItem[i].CostPricePerQty.ToString());
                dr["Col5"] = lstStockTransferItem[i].OriginalReceiptDate.ToString();
                dr["Col6"] = FourDecimalConversion(lstStockTransferItem[i].CostPricePerQty.ToString());
                dr["Col7"] = lstStockTransferItem[i].ReceivedQuantity.ToString();
                dr["Col8"] = lstStockTransferItem[i].AcceptedQuantity.ToString();
                dr["Col9"] = lstStockTransferItem[i].ItemLocation.ToString();
                dr["Col10"] = FourDecimalConversion(lstStockTransferItem[i].TotalCostPrice.ToString());
                dr["Col11"] = lstStockTransferItem[i].OSLSIndicator.ToString();
                dr["Col12"] = FourDecimalConversion(lstStockTransferItem[i].GSTPercentage.ToString());
                dr["ProductGroupCode"] = lstStockTransferItem[i].ProductGroupCode.ToString();
                dr["Col13"] = "";
                dr["ConsInwardNo"] = lstStockTransferItem[i].ConsInwardNo.ToString();
                dr["ConsSerialNo"] = lstStockTransferItem[i].ConsSerialNo.ToString();
                dr["Col14"] = "";
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCnt.Value = dt.Rows.Count.ToString();

            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();
            SetPreviousData();

            ShowSummaryInFooter();
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
                        TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                        TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        TextBox txtListPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtListPrice");
                        TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                        TextBox txtOriginalReceiptDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOriginalReceiptDate");
                        TextBox txtReceivedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtReceivedQuantity");
                        TextBox txtAcceptedQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAcceptedQuantity");
                        TextBox txtTotalCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtTotalCostPrice");
                        TextBox txtOSLSIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtOSLSIndicator");
                        TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                        HiddenField hdnTaxGroupCode = (HiddenField)grvItemDetails.Rows[rowIndex].FindControl("hdnTaxGroupCode");
                        TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                        TextBox hdnConsInwardNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("hdnConsInwardNo");
                        TextBox hdnConsSerialNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("hdnConsSerialNo");
                        TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                        txtSupplierName.Text = dt.Rows[i]["Col1"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i]["Col2"].ToString();
                        txtItemCode.Text = dt.Rows[i]["Col3"].ToString();
                        txtCstPricePerQty.Text = FourDecimalConversion(dt.Rows[i]["Col4"].ToString());
                        txtOriginalReceiptDate.Text = dt.Rows[i]["Col5"].ToString();
                        txtListPrice.Text = FourDecimalConversion(dt.Rows[i]["Col6"].ToString());
                        txtReceivedQuantity.Text = dt.Rows[i]["Col7"].ToString();
                        txtAcceptedQuantity.Text = dt.Rows[i]["Col8"].ToString();
                        txtItemLocation.Text = dt.Rows[i]["Col9"].ToString();
                        txtTotalCostPrice.Text = FourDecimalConversion(dt.Rows[i]["Col10"].ToString());
                        txtOSLSIndicator.Text = dt.Rows[i]["Col11"].ToString();
                        txtGSTPercentage.Text = FourDecimalConversion(dt.Rows[i]["Col12"].ToString());
                        hdnTaxGroupCode.Value = dt.Rows[i]["ProductGroupCode"].ToString();
                        txtInvoiceNumber.Text = dt.Rows[i]["Col13"].ToString();
                        hdnConsInwardNo.Text = dt.Rows[i]["ConsInwardNo"].ToString();
                        hdnConsSerialNo.Text = dt.Rows[i]["ConsSerialNo"].ToString();
                        txtInvoiceDate.Text = dt.Rows[i]["Col14"].ToString();                        

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
                grvItemDetails.FooterRow.Cells[10].Text = TwoDecimalConversion(sumCostPrice.ToString());
                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;

                grvItemDetails.FooterRow.Cells[12].Text = TwoDecimalConversion(sumTaxValue.ToString());
                hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[12].ClientID;
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
                return "0";
            else
                return string.Format("{0:0}", Convert.ToDecimal(strValue));
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            txtStockTransferReceiptNumber.Enabled = !Fzflag;
            txtStockTransferDate.Enabled = !Fzflag;
            ddlTransactionType.Enabled = !Fzflag;
            ddlFromBranch.Enabled = !Fzflag;
            txtInvoiceValue.Enabled = !Fzflag;
            txtIGSTValue.Enabled = !Fzflag;
            btnReset.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
            DivHeader.Disabled = !Fzflag;
            imgEditToggle.Enabled = Fzflag;
            UpdpanelTop.Update();
            UpdPanelGrid.Update();
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