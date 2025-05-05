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
    public partial class StockTransferReceiptFinal : System.Web.UI.Page
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
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
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

                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlStockTransferReceiptNumber.Visible = false;
                    txtIGSTValue.Text = "0.00";
                    hdninterStateStatus.Value = "0";
                    hdnSelItemCode.Value = "";
                    FreezeOrUnFreezeButtons(true);
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    DefineBranch();
                }

                BtnSubmit.Attributes.Add("OnClick", "return StockTransferReceiptSubmit('M');");
                BtnReject.Attributes.Add("OnClick", "return funSTDNinwardReject();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
                hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[12].ClientID;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    //hdnScreenMode.Value = "E";
                    //ddlStockTransferReceiptNumber.SelectedIndex = "0";
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

                BtnSubmit.Attributes.Add("OnClick", "return StockTransferReceiptSubmit('M');");
                BtnReject.Attributes.Add("OnClick", "return funSTDNinwardReject();");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                if (grvItemDetails.FooterRow != null)
                {
                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    hdnFooterTaxValue.Value = grvItemDetails.FooterRow.Cells[12].ClientID;
                }

                FreezeOrUnFreezeButtons(true);
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferReceiptFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferReceiptFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
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
                    txtIGSTValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.IGSTValue);
                    txtInvoiceValue.Text = TwoDecimalConversion(stockTransferReceiptEntity.InvoiceValue);

                    txtLRNumber.Text = stockTransferReceiptEntity.LRNumber;
                    txtLRDate.Text = stockTransferReceiptEntity.LRDate;
                    txtCarrier.Text = stockTransferReceiptEntity.Carrier;
                    txtDestination.Text = stockTransferReceiptEntity.Destination;
                    txtRoadPermitNo.Text = stockTransferReceiptEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = stockTransferReceiptEntity.RoadPermitDate;
                    hdnReceivedStatus.Value = stockTransferReceiptEntity.ReceivedStatus;
                    txtFromBranchSTDNNo.Text = stockTransferReceiptEntity.RefDocNo;
                    txtFromBranchSTDNDate.Text = stockTransferReceiptEntity.RefDocDate;

                    ddlTransactionType.Enabled = false;
                    ddlBranch.Enabled = false;
                    txtLRNumber.Enabled = false;
                    txtLRDate.Enabled = false;
                    txtCarrier.Enabled = false;
                    txtDestination.Enabled = false;
                    txtRoadPermitNo.Enabled = false;
                    txtRoadPermitDate.Enabled = false;

                    if (stockTransferReceiptEntity.ApprovalStatus == "A")
                    {
                        BtnSubmit.Enabled = false;
                        BtnReject.Enabled = false;
                        hdnScreenMode.Value = "E";
                    }
                    else
                    {
                        BtnSubmit.Enabled = true;
                        BtnReject.Enabled = true;
                        hdnScreenMode.Value = "A";
                    }

                    BindExistingRowsDuringEdit(stockTransferReceiptEntity.Items);

                    btnReset.Enabled = true;
                    btnCancel.Enabled = true;

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }
                }
                else
                {
                    hdnScreenMode.Value = "E";
                    //ddlStockTransferReceiptNumber.SelectedValue = "0";
                    ddlStockTransferReceiptNumber.Visible = true;
                    txtStockTransferReceiptNumber.Visible = false;
                    ddlStockTransferReceiptNumber.DataBind();
                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                    btnReset.Enabled = true;
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    btnCancel.Enabled = true;
                }

                if (hdnReceivedStatus.Value == "E")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = false;
                    BtnReject.Visible = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void ddlFromBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFromBranch.SelectedIndex > 0)
                {
                    StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
                    int STDNinterStateStatus = stockTransferReceiptTransactions.GetSTDNinterStateStatus(ddlBranch.SelectedValue, ddlFromBranch.SelectedValue);

                    hdninterStateStatus.Value = STDNinterStateStatus.ToString();                    
                }
                else
                {
                    hdninterStateStatus.Value = "0";
                }

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                SubmitHeaderAndItems();
                ShowSummaryInFooter();
                ddlStockTransferReceiptNumber.Enabled = false;
                STDNReceiptPanel.Enabled = false;
                pnlCI.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                ddlStockTransferReceiptNumber.Enabled = false;
                StockTransferTransactions tran = new StockTransferTransactions();
                tran.EditSTEntryInWard(Session["BranchCode"].ToString(), ddlFromBranch.SelectedValue, ddlStockTransferReceiptNumber.SelectedValue, "I", "R", Session["STDNinWardRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer Receipt Entry has been Rejected/Cancelled Sucessfully');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["STDNinWardRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                Log.WriteException(typeof(StockTransferReceiptFinal), exp);
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
            stockTransferReceiptEntity.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];
            stockTransferReceiptEntity.RefDocNo = txtFromBranchSTDNNo.Text;
            stockTransferReceiptEntity.RefDocDate = txtFromBranchSTDNDate.Text;

            StockTransferReceiptTransactions stockTransferReceiptTransactions = new StockTransferReceiptTransactions();
            if (hdnScreenMode.Value == "A")
            {
                stockTransferReceiptTransactions.AddNewSTReceiptEntryFinal(ref stockTransferReceiptEntity);
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

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer Receipt Entry details has been saved successfully.');", true);
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
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
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
                dr["Col13"] = string.Empty;
                dr["Col14"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 15;
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
            dt.Columns.Add(new DataColumn("Col13", typeof(string)));
            dt.Columns.Add(new DataColumn("Col14", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < lstStockTransferItem.Count; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = lstStockTransferItem[i].SNo.ToString();
                dr["Col1"] = lstStockTransferItem[i].SupplierLineCode.ToString();
                dr["Col2"] = lstStockTransferItem[i].SupplierPartNo.ToString();
                dr["Col3"] = lstStockTransferItem[i].ItemCode.ToString();
                dr["Col4"] = TwoDecimalConversion(lstStockTransferItem[i].CostPricePerQty.ToString());
                dr["Col5"] = lstStockTransferItem[i].OriginalReceiptDate.ToString();
                dr["Col6"] = TwoDecimalConversion(lstStockTransferItem[i].ListPrice.ToString());
                dr["Col7"] = lstStockTransferItem[i].ReceivedQuantity.ToString();
                dr["Col8"] = lstStockTransferItem[i].AcceptedQuantity.ToString();
                dr["Col9"] = lstStockTransferItem[i].ItemLocation.ToString();
                dr["Col10"] = TwoDecimalConversion(lstStockTransferItem[i].TotalCostPrice.ToString());
                dr["Col11"] = lstStockTransferItem[i].OSLSIndicator.ToString();
                dr["Col12"] = TwoDecimalConversion(lstStockTransferItem[i].GSTPercentage.ToString());
                dr["Col13"] = "";
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
                        TextBox txtInvoiceNumber = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceNumber");
                        TextBox txtInvoiceDate = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInvoiceDate");

                        txtSupplierName.Text = dt.Rows[i]["Col1"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i]["Col2"].ToString();
                        txtItemCode.Text = dt.Rows[i]["Col3"].ToString();
                        txtCstPricePerQty.Text = TwoDecimalConversion(dt.Rows[i]["Col4"].ToString());
                        txtOriginalReceiptDate.Text = dt.Rows[i]["Col5"].ToString();
                        txtListPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col6"].ToString());
                        txtReceivedQuantity.Text = dt.Rows[i]["Col7"].ToString();
                        txtAcceptedQuantity.Text = dt.Rows[i]["Col8"].ToString();
                        txtItemLocation.Text = dt.Rows[i]["Col9"].ToString();
                        txtTotalCostPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col10"].ToString());
                        txtOSLSIndicator.Text = dt.Rows[i]["Col11"].ToString();
                        txtGSTPercentage.Text = TwoDecimalConversion(dt.Rows[i]["Col12"].ToString());
                        txtInvoiceNumber.Text = dt.Rows[i]["Col13"].ToString();
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
            btnCancel.Enabled = Fzflag;
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