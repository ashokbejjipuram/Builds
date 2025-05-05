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
using System.Data.Common;
using IMPALLibrary.Common;
using Newtonsoft.Json;

namespace IMPALWeb
{
    public partial class StockTransferEntryEDP : System.Web.UI.Page
    {
        EinvAuthGen einvGen = new EinvAuthGen();
        StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Stock Value Page Init Method");
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        string STDNNumber = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (crySTDNinvoiceReprint != null)
                    {
                        crySTDNinvoiceReprint.Dispose();
                        crySTDNinvoiceReprint = null;
                    }

                    hdnScreenMode.Value = "A";
                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlStockTransferNumber.Visible = false;
                    hdnSelItemCode.Value = "";
                    FreezeOrUnFreezeButtons(true);
                    ddlToBranch.Focus();
                    DefineBranch();
                    BtnUpdate.Enabled = false;
                    BtnUpdate.Visible = false;
                    BtnReport.Enabled = false;
                    BtnReport.Visible = false;

                    LoadHOSTDNEntriesOnline();

                    if (ddlHOSTDNNumberOnline.Items.Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HO STDNs are in pending for STDN Outward to Other Branches. Please complete the same.');", true);
                        ddlHOSTDNNumberOnline.Focus();
                        BtnSubmit.Visible = false;
                        ddlToBranch.Enabled = false;

                        if (grvItemDetails.FooterRow != null)
                        {
                            Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                            if (btnAddRow != null)
                                btnAddRow.Visible = false;
                        }
                    }
                    else
                    {
                        if (grvItemDetails.FooterRow != null)
                        {
                            Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                            if (btnAddRow != null)
                                btnAddRow.Visible = true;
                        }

                        BtnSubmit.Visible = true;
                        ddlToBranch.Enabled = true;
                    }

                    if ((string)Session["RoleCode"] == "CORP")
                        ddlBranch.CssClass = "dropDownListNormal";
                    else
                        ddlBranch.CssClass = "dropDownListDisabled";

                    string EntryStatus = stockTransferTransactions.GetSTDNDespatchEntryStatus(Session["BranchCode"].ToString());

                    if (EntryStatus == "1")
                        BtnSubmit.Attributes.Add("Style", "display:inline");
                    else
                        BtnSubmit.Attributes.Add("Style", "display:none");
                }

                ddlToBranch.Attributes.Add("OnChange", "funSetDestination();");
                BtnSubmit.Attributes.Add("OnClick", "return StockTransferEntrySubmit('E');");

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");

                    hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[7].ClientID;
                    hdnFooterGSTvalue.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                }

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crySTDNinvoiceReprint != null)
            {
                crySTDNinvoiceReprint.Dispose();
                crySTDNinvoiceReprint = null;
            }
        }

        protected void crySTDNinvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (crySTDNinvoiceReprint != null)
            {
                crySTDNinvoiceReprint.Dispose();
                crySTDNinvoiceReprint = null;
            }
        }

        private void LoadHOSTDNEntriesOnline()
        {
            ddlHOSTDNNumberOnline.DataSource = stockTransferTransactions.GetHOSTDNDespatchEntriesEDP(Session["BranchCode"].ToString());
            ddlHOSTDNNumberOnline.DataTextField = "ItemCode";
            ddlHOSTDNNumberOnline.DataValueField = "ItemCode";
            ddlHOSTDNNumberOnline.DataBind();
        }
        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                ddlHOSTDNNumberOnline.Enabled = false;

                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    //ddlStockTransferNumber.SelectedIndex = 0;
                    ddlStockTransferNumber.Visible = true;
                    txtStockTransferNumber.Visible = false;
                    ddlStockTransferNumber.DataBind();

                    ddlToBranch.SelectedIndex = 0;
                    BtnUpdate.Enabled = false;
                    BtnReport.Enabled = false;

                    FirstGridViewRow();

                    imgEditToggle.Visible = false;
                    BtnSubmit.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlStockTransferNumber.Visible = false;
                    txtStockTransferNumber.Visible = true;

                    txtStockTransferNumber.Text = string.Empty;
                    txtStockTransferDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlToBranch.SelectedIndex = 0;
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    txtLRNumber.Text = string.Empty;
                    txtLRDate.Text = string.Empty;
                    txtCarrier.Text = string.Empty;
                    txtDestination.Text = string.Empty;
                    txtRoadPermitNo.Text = string.Empty;
                    txtRoadPermitDate.Text = string.Empty;
                    FirstGridViewRow();
                    BtnUpdate.Enabled = false;
                    BtnReport.Enabled = false;
                    BtnSubmit.Visible = true;
                }

                BtnSubmit.Attributes.Add("OnClick", "return StockTransferEntrySubmit('E');");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                }

                FreezeOrUnFreezeButtons(true);

                ddlTransactionType.Enabled = false;
                ddlBranch.Enabled = false;
                ddlToBranch.Enabled = false;
                txtLRNumber.Enabled = false;
                txtLRDate.Enabled = false;
                txtCarrier.Enabled = false;
                txtDestination.Enabled = false;
                txtRoadPermitNo.Enabled = false;
                txtRoadPermitDate.Enabled = false;
                grvItemDetails.Enabled = false;
                btnReset.Enabled = true;
                BtnSubmit.Enabled = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSearch = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtCurrentSearch = (TextBox)grdRow.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdRow.FindControl("ddlSupplierName"); ;
                DropDownList ddlSupplierPartNo = (DropDownList)grdRow.FindControl("ddlSupplierPartNo");
                TextBox txtAvailQuantity = (TextBox)grdRow.FindControl("txtAvailableQuantity");
                TextBox txtQuantity = (TextBox)grdRow.FindControl("txtQuantity");
                TextBox txtCstPricePerQty = (TextBox)grdRow.FindControl("txtCstPricePerQty");
                TextBox txtCostPrice = (TextBox)grdRow.FindControl("txtCostPrice");
                TextBox txtGSTPercentage = (TextBox)grdRow.FindControl("txtGSTPercentage");

                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNo.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtAvailQuantity.Text = "";
                    txtQuantity.Text = "";
                    txtCstPricePerQty.Text = "";
                    txtCostPrice.Text = "";
                    txtGSTPercentage.Text = "";
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        ddlSupplierPartNo.DataSource = (object)stockTransferTransactions.GetItemListBySupplierPartNo(ddlSupplierName.SelectedItem.Text.Substring(0, 3), txtCurrentSearch.Text, ddlBranch.SelectedValue.ToString());
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
                        string[] strArr = strArray.Split('|');

                        foreach (string str in strArr)
                        {
                            if (string.IsNullOrEmpty(str))
                                continue;

                            ListItem li = ddlSupplierPartNo.Items.FindByValue(str);
                            ddlSupplierPartNo.Items.Remove(li);
                        }

                        ddlSupplierPartNo.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            UpdPanelGrid.Visible = false;
            btnBack.Visible = true;

            string strSelectionFormula = default(string);
            string strBrnchField = default(string);
            string strBrnchValue = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strBrnchValue = ddlBranch.SelectedValue;
            strValue = txtStockTransferNumber.Text;
            strBrnchField = "{V_Invoice_STDN.Branch_Code}";
            strField = "{V_Invoice_STDN.Document_number}";
            strReportName = "po_pp_invoice_stdnGST";

            strSelectionFormula = strBrnchField + "='" + strBrnchValue + "' and " + strField + "= " + "'" + strValue + "'";

            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmdtemps = ImpalDB.GetStoredProcCommand("usp_GetDocumentRePrint_Old");
            ImpalDB.AddInParameter(cmdtemps, "@Branch_Code", DbType.String, Session["BranchCode"].ToString());
            ImpalDB.AddInParameter(cmdtemps, "@Document_Number", DbType.String, strValue);
            ImpalDB.AddInParameter(cmdtemps, "@Indicator", DbType.String, "S");
            cmdtemps.CommandTimeout = ConnectionTimeOut.TimeOut;
            var signedQRCode = ImpalDB.ExecuteScalar(cmdtemps).ToString();

            if (signedQRCode.ToString() != "")
            {
                GenerateQRcode genQRcode = new GenerateQRcode();
                SalesTransactions salesTransactions = new SalesTransactions();
                byte[] imageData = genQRcode.GenerateInvQRCode(signedQRCode.ToString().Trim());

                salesTransactions.updEinvoiceIRNDetails(Session["BranchCode"].ToString(), strValue, imageData);
            }

            crySTDNinvoiceReprint.ReportName = strReportName;
            crySTDNinvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            crySTDNinvoiceReprint.GenerateReportAndExportA4();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferEntryEDP.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferEntryEDP.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    if (btnAddRow != null)
                        btnAddRow.Attributes.Add("OnClick", "return funBtnAddRow();");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private bool CheckMultipleTax(string SalesTax)
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    TextBox txtGSTPercentage = (TextBox)gr.Cells[10].FindControl("txtGSTPercentage");
                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[4].FindControl("ddlSupplierName");
                    if (!(ddlSupplierName.Visible))
                    {
                        if (SalesTax != txtGSTPercentage.Text.Substring(0, txtGSTPercentage.Text.Length - 1))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                string supplierCode = ddl.SelectedValue.ToString();

                if (supplierCode == "0")
                    return;

                TextBox txtSupplierPartNo = (TextBox)row.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                DropDownList ddlSupplierPartNo = (DropDownList)row.FindControl("ddlSupplierPartNo");
                TextBox txtAvailQuantity = (TextBox)row.FindControl("txtAvailableQuantity");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtCstPricePerQty = (TextBox)row.FindControl("txtCstPricePerQty");
                TextBox txtCostPrice = (TextBox)row.FindControl("txtCostPrice");
                TextBox txtGSTPercentage = (TextBox)row.FindControl("txtGSTPercentage");
                Button btnSearch = (Button)row.FindControl("btnSearch");

                txtSupplierPartNo.Visible = true;
                txtSupplierPartNo.Text = "";
                txtItemCode.Text = "";
                ddlSupplierPartNo.Items.Clear();
                ddlSupplierPartNo.Visible = false;
                txtAvailQuantity.Text = "";
                txtQuantity.Text = "";
                txtCstPricePerQty.Text = "";
                txtCostPrice.Text = "";
                txtGSTPercentage.Text = "";
                btnSearch.Text = "Search";
                txtSupplierPartNo.Focus();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }

        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                TextBox txtGSTValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTValue");

                if (ddl.SelectedValue.ToString() == "0")
                {
                    txtItemCode.Text = "";
                    txtAvailableQuantity.Text = "";
                    txtQuantity.Text = "";
                    txtCstPricePerQty.Text = "";
                    txtCostPrice.Text = "";
                    txtGSTPercentage.Text = "";
                    txtGSTValue.Text = "";
                }
                else
                {
                    string selSupplierPartNo = ddl.SelectedItem.Text.ToString();
                    string selItemCode = ddl.SelectedValue.ToString();
                    DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");


                    int HSNCode = stockTransferTransactions.CheckHSNCodeItem(ddlSupplierName.SelectedItem.Text.Substring(0, 3), selSupplierPartNo);

                    if (HSNCode > 0)
                    {
                        string[] result = stockTransferTransactions.GetItemDetails(ddlSupplierName.SelectedItem.Text.Substring(0, 3), selSupplierPartNo, selItemCode, ddlBranch.SelectedValue.ToString(), ddlToBranch.SelectedValue.ToString(), "O");

                        if (result != null)
                        {
                            //bool isSingleTax;

                            //if (result[2].ToString().Substring(0, result[2].Length - 1) == "0")
                            //    isSingleTax = CheckMultipleTax(result[2].ToString().Substring(0, result[2].Length - 1));
                            //else
                            //    isSingleTax = CheckMultipleTax(TwoDecimalConversion(result[2].ToString().Substring(0, result[2].Length - 1)));

                            if (Convert.ToInt16(result[5].ToString()) > 1 && (result[2].ToString() == "0.00%" || result[2].ToString() == "0.00" || result[2].ToString() == "0"))
                            {
                                txtQuantity.Enabled = false;
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GST% is not Available For this item. Please Check the same');", true);
                            }
                            else
                            {
                                //if (isSingleTax)
                                //{
                                txtItemCode.Text = result[0].ToString();
                                txtQuantity.Text = "";
                                txtQuantity.Enabled = true;
                                txtCstPricePerQty.Text = TwoDecimalConversion(result[1].ToString());
                                txtGSTPercentage.Text = result[2].ToString();
                                txtAvailableQuantity.Text = result[3].ToString();
                                txtQuantity.Attributes.Add("OnChange", "return funSTQuantityValidation('" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "','" + txtGSTPercentage.ClientID + "','" + txtGSTValue.ClientID + "');");
                                txtQuantity.Focus();
                                //}
                                //else
                                //{
                                //    txtQuantity.Enabled = false;
                                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Multiple Sales Tax STDN is not Possible in GST. Please Remove this Part Number');", true);
                                //}
                            }
                        }
                    }
                    else
                    {
                        txtQuantity.Text = "";
                        txtQuantity.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HSN Code is not available for this Item');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void ddlStockTransferNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStockTransferNumber.SelectedValue != "0")
                {
                    StockTransferEntity stockTransferEntity = stockTransferTransactions.GetSTDNEntryDetailsByNumber(Session["BranchCode"].ToString(), ddlStockTransferNumber.SelectedValue.ToString());

                    txtStockTransferNumber.Text = stockTransferEntity.StockTransferNumber;
                    txtStockTransferDate.Text = stockTransferEntity.StockTransferDate;                    

                    ddlTransactionType.SelectedValue = stockTransferEntity.TransactionTypeCode;
                    ddlBranch.SelectedValue = stockTransferEntity.BranchCode;
                    ddlToBranch.SelectedValue = stockTransferEntity.ToBranch;

                    txtLRNumber.Text = stockTransferEntity.LRNumber;
                    txtLRDate.Text = stockTransferEntity.LRDate;
                    txtCarrier.Text = stockTransferEntity.Carrier;
                    txtDestination.Text = stockTransferEntity.Destination;
                    txtCCWHNo.Text = stockTransferEntity.RefDocNo;
                    txtRefStockTransfeDate.Text = stockTransferEntity.RefDocDate;
                    txtRoadPermitNo.Text = stockTransferEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = stockTransferEntity.RoadPermitDate;

                    ddlTransactionType.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlToBranch.Enabled = false;

                    if (txtLRNumber.Text.Trim() == "")
                        txtLRNumber.Enabled = true;
                    else
                        txtLRNumber.Enabled = false;

                    if (txtLRDate.Text.Trim() == "")
                        txtLRDate.Enabled = true;
                    else
                        txtLRDate.Enabled = false;

                    if (txtCarrier.Text.Trim() == "")
                        txtCarrier.Enabled = true;
                    else
                        txtCarrier.Enabled = false;

                    if (txtRoadPermitNo.Text.Trim() == "")
                        txtRoadPermitNo.Enabled = true;
                    else
                        txtRoadPermitNo.Enabled = false;

                    if (txtRoadPermitDate.Text.Trim() == "")
                        txtRoadPermitDate.Enabled = true;
                    else
                        txtRoadPermitDate.Enabled = false;

                    if (txtLRNumber.Text.Trim() == "" || txtLRDate.Text.Trim() == "" || txtCarrier.Text.Trim() == "" || txtRoadPermitNo.Text.Trim() == "" || txtRoadPermitDate.Text.Trim() == "")
                        BtnUpdate.Visible = true;
                    else
                        BtnUpdate.Visible = false;

                    txtDestination.Enabled = false;
                    txtCCWHNo.Enabled = false;
                    txtRefStockTransfeDate.Enabled = false;
                    BtnSubmit.Visible = false;

                    if (stockTransferEntity.ApprovalStatus == "A")
                    {
                        BtnUpdate.Enabled = true;
                        BtnReport.Enabled = true;
                        BtnReport.Visible = true;
                    }
                    else
                    {
                        BtnUpdate.Visible = false;
                        BtnReport.Visible = false;
                    }

                    BindExistingRowsDuringEdit(stockTransferEntity.Items);
                }
                else
                {
                    txtStockTransferDate.Text = "";
                    txtLRNumber.Text = "";
                    txtLRDate.Text = "";
                    txtCarrier.Text = "";
                    txtDestination.Text = "";
                    txtRoadPermitNo.Text = "";
                    txtRoadPermitDate.Text = "";
                    ddlToBranch.SelectedIndex = 0;
                    FirstGridViewRow();
                }

                btnReset.Enabled = true;
                BtnSubmit.Enabled = false;

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    btnAddRow.Enabled = false;
                    btnAddRow.Visible = false;
                }

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    gvr.Enabled = false;
                    gvr.FindControl("btnSearch").Visible = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void ddlHOSTDNNumberOnline_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStockTransferNumber.SelectedValue != "0")
                {
                    StockTransferEntity stockTransferEntity = stockTransferTransactions.GetHOSTDNEntryDetailsByNumber(Session["BranchCode"].ToString(), ddlHOSTDNNumberOnline.SelectedValue.ToString());

                    imgEditToggle.Visible = false;

                    ddlTransactionType.SelectedValue = stockTransferEntity.TransactionTypeCode;
                    ddlBranch.SelectedValue = stockTransferEntity.BranchCode;
                    ddlToBranch.SelectedValue = stockTransferEntity.ToBranch;
                    txtCCWHNo.Text = stockTransferEntity.RefDocNo;
                    txtRefStockTransfeDate.Text = stockTransferEntity.RefDocDate;

                    txtHOStockTransferFromDate.Text = stockTransferEntity.StockTransferDate;
                    txtDestination.Text = stockTransferEntity.Destination;

                    ddlTransactionType.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlToBranch.Enabled = false;
                    txtCCWHNo.Enabled = false;
                    txtRefStockTransfeDate.Enabled = false;
                    txtDestination.Enabled = false;
                    txtLRNumber.Enabled = true;
                    txtLRDate.Enabled = true;
                    txtCarrier.Enabled = true;
                    txtRoadPermitNo.Enabled = true;
                    txtRoadPermitDate.Enabled = true;

                    BtnUpdate.Enabled = false;
                    BtnReport.Visible = false;

                    BindExistingRowsDuringEdit(stockTransferEntity.Items);
                    BtnSubmit.Visible = true;
                }
                else
                {
                    txtStockTransferDate.Text = "";
                    txtLRNumber.Text = "";
                    txtLRDate.Text = "";
                    txtCarrier.Text = "";
                    txtDestination.Text = "";
                    txtRoadPermitNo.Text = "";
                    txtRoadPermitDate.Text = "";
                    txtCCWHNo.Text = "";
                    txtRefStockTransfeDate.Text = "";
                    ddlToBranch.SelectedIndex = 0;
                    FirstGridViewRow();
                    BtnSubmit.Visible = false;
                }

                btnReset.Enabled = true;
                BtnSubmit.Enabled = true;

                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    btnAddRow.Enabled = false;
                    btnAddRow.Visible = false;
                }

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    gvr.Enabled = false;
                    gvr.FindControl("btnSearch").Visible = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.SelectedValue == "Inactive" && txtStockTransferDate.Text == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    DataSet ds = stockTransferTransactions.EditSTEntry(Session["BranchCode"].ToString(), ddlStockTransferNumber.SelectedValue, "I", "", "Cancelled");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string JSONData = "{\"supplier_gstin\":\"" + ds.Tables[0].Rows[0]["GSTIN"] + "\",\"doc_no\":\"" + ds.Tables[0].Rows[0]["Document_Number"] + "\"," + "\"irn_no\":\"" + ds.Tables[0].Rows[0]["IRN_Number"] + "\"," +
                                          "\"doc_date\":\"" + ds.Tables[0].Rows[0]["Document_Date"] + "\",\"reason\":\"" + ds.Tables[0].Rows[0]["Reason"] + "\",\"remark\":\"" + ds.Tables[0].Rows[0]["Remarks"] + "\"}";

                        einvGen.EinvoiceAuthentication(JSONData, ddlBranch.SelectedValue.ToString(), ddlStockTransferNumber.SelectedItem.Text.ToString(), "2", "CANIRN", ds.Tables[0].Rows[0]["GSTIN"].ToString(), ds.Tables[0].Rows[0]["Document_Type"].ToString(), ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), ds);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer Number has been Inactivated Sucessfully');", true);
                }
                else
                {
                    BtnSubmit.Enabled = false;
                    BtnSubmit.Visible = false;
                    SubmitHeaderAndItems();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BtnUpdate.Enabled = false;
                UpdateLRDetails();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryFinal), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                        txtQuantity.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
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
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void FillLastRowInfoInGrid(ref DataTable dt)
        {
            try
            {
                int rowIndex = grvItemDetails.Rows.Count - 1;

                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");

                DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");

                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");

                TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                TextBox txtGSTValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTValue");

                if (ddlSupplierName.Visible && txtSupplierName.Visible)
                {

                    if (txtQuantity.Text != "" && txtQuantity.Text != "0")
                    {
                        dt.Rows[rowIndex]["Col1"] = ddlSupplierName.SelectedItem.Text.ToString();
                        dt.Rows[rowIndex]["Col2"] = ddlSupplierPartNo.SelectedValue.ToString();
                        dt.Rows[rowIndex]["Col3"] = txtItemCode.Text;
                        dt.Rows[rowIndex]["Col4"] = txtAvailableQuantity.Text;
                        dt.Rows[rowIndex]["Col5"] = txtQuantity.Text;
                        dt.Rows[rowIndex]["Col6"] = txtCstPricePerQty.Text;
                        dt.Rows[rowIndex]["Col7"] = txtCostPrice.Text;
                        dt.Rows[rowIndex]["Col8"] = txtGSTPercentage.Text;
                        dt.Rows[rowIndex]["Col9"] = txtGSTValue.Text;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void SubmitHeaderAndItems()
        {
            try
            {
                BtnUpdate.Visible = false;
                BtnReport.Visible = false;
                StockTransferEntity stockTransferEntity = new StockTransferEntity();
                stockTransferEntity.Items = new List<StockTransferItem>();

                stockTransferEntity.BranchCode = ddlBranch.SelectedValue.ToString();

                if (ddlStockTransferNumber.Visible)
                    stockTransferEntity.StockTransferNumber = ddlStockTransferNumber.SelectedValue.ToString();
                else
                    stockTransferEntity.StockTransferNumber = txtStockTransferNumber.Text.ToString();

                stockTransferEntity.StockTransferDate = txtStockTransferDate.Text.ToString();

                stockTransferEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                stockTransferEntity.ToBranch = ddlToBranch.SelectedValue.ToString();

                stockTransferEntity.TempNumber = ddlHOSTDNNumberOnline.SelectedValue;
                stockTransferEntity.LRNumber = txtLRNumber.Text.ToString();
                stockTransferEntity.LRDate = txtLRDate.Text.ToString();
                stockTransferEntity.Carrier = txtCarrier.Text.ToString();
                stockTransferEntity.Destination = txtDestination.Text.ToString();
                stockTransferEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
                stockTransferEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();
                stockTransferEntity.RefDocNo = txtCCWHNo.Text.ToString();
                stockTransferEntity.RefDocDate = txtRefStockTransfeDate.Text.ToString();

                if (ddlHOSTDNNumberOnline.SelectedIndex > 0)
                    stockTransferEntity.Remarks = "HO_PO_STDN_Approval";
                else
                    stockTransferEntity.Remarks = "";

                StockTransferItem stockTransferItem = null;
                int SNo = 0;
                decimal dmlTotal = 0;
                int IntrBrchStatus = 1;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    stockTransferItem = new StockTransferItem();
                    SNo += 1;

                    DropDownList ddlSupplierName = (DropDownList)gr.FindControl("ddlSupplierName");
                    TextBox txtSupplierName = (TextBox)gr.FindControl("txtSupplierName");

                    DropDownList ddlSupplierPartNo = (DropDownList)gr.FindControl("ddlSupplierPartNo");
                    TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");

                    TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                    TextBox txtAvailableQuantity = (TextBox)gr.FindControl("txtAvailableQuantity");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");
                    TextBox txtGSTPercentage = (TextBox)gr.FindControl("txtGSTPercentage");

                    dmlTotal += Convert.ToDecimal(txtCostPrice.Text);

                    stockTransferItem.SNo = SNo.ToString();

                    if (ddlSupplierName.Visible)
                        stockTransferItem.SupplierLineCode = ddlSupplierName.SelectedItem.Text.Substring(0, 3).ToString();
                    else
                    {
                        string[] strSuppCode = txtSupplierName.Text.Split('-');
                        stockTransferItem.SupplierLineCode = strSuppCode[0].ToString();
                    }

                    if (ddlSupplierPartNo.Visible)
                        stockTransferItem.SupplierPartNo = ddlSupplierPartNo.SelectedValue.ToString();
                    else
                        stockTransferItem.SupplierPartNo = txtSupplierPartNo.Text.ToString();

                    stockTransferItem.ItemCode = txtItemCode.Text.ToString();
                    stockTransferItem.AvailableQty = txtAvailableQuantity.Text;
                    stockTransferItem.Quantity = txtQuantity.Text;
                    stockTransferItem.CostPricePerQuantity = txtCstPricePerQty.Text;
                    stockTransferItem.CostPrice = txtCostPrice.Text;
                    stockTransferItem.GSTPercentage = txtGSTPercentage.Text;

                    stockTransferEntity.Items.Add(stockTransferItem);
                }

                if (hdnScreenMode.Value == "A")
                {
                    if (ddlHOSTDNNumberOnline.SelectedIndex > 0)
                        IntrBrchStatus = stockTransferTransactions.AddNewSTEntryHOSTDN(ref stockTransferEntity, ddlStatus.SelectedValue);
                    else
                        stockTransferTransactions.AddNewSTEntry(ref stockTransferEntity, ddlStatus.SelectedValue);

                    if ((stockTransferEntity.ErrorMsg == string.Empty) && (stockTransferEntity.ErrorCode == "0"))
                    {
                        txtStockTransferNumber.Text = stockTransferEntity.StockTransferNumber;
                        STDNNumber = stockTransferEntity.StockTransferNumber;
                        ddlHOSTDNNumberOnline.Enabled = false;
                        FreezeOrUnFreezeButtons(false);
                        btnReset.Enabled = true;

                        foreach (GridViewRow gvr in grvItemDetails.Rows)
                        {
                            gvr.Enabled = false;
                        }

                        ShowSummaryInFooter();

                        if (ddlHOSTDNNumberOnline.SelectedIndex > 0)
                        {
                            if (IntrBrchStatus != 1)
                            {
                                DataSet Datasetresult = stockTransferTransactions.GetEinvoicingSTDNDetails(ddlBranch.SelectedValue.ToString(), stockTransferEntity.StockTransferNumber);

                                GenerateJSON objGenJsonData = new GenerateJSON();

                                string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                                einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, stockTransferEntity.StockTransferNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer OutWard Entry has been Completed successfully and Invoice Generated with QR Code.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer OutWard Entry has been Completed successfully and QR Code is not required for Intra State STDN.');", true);
                            }
                        }
                        else
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer entry details has been saved successfully. Please Get the Manager/Accountant Approval for Invoice Print.');", true);

                        BtnUpdate.Enabled = false;
                        BtnReport.Enabled = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + stockTransferEntity.ErrorMsg + "');", true);
                        BtnUpdate.Enabled = false;
                        BtnReport.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void UpdateLRDetails()
        {
            try
            {
                BtnUpdate.Visible = false;
                StockTransferEntity stockTransferEntity = new StockTransferEntity();
                stockTransferEntity.Items = new List<StockTransferItem>();

                stockTransferEntity.BranchCode = ddlBranch.SelectedValue.ToString();
                stockTransferEntity.StockTransferNumber = ddlStockTransferNumber.SelectedValue.ToString();
                stockTransferEntity.LRNumber = txtLRNumber.Text.ToString();
                stockTransferEntity.LRDate = txtLRDate.Text.ToString();
                stockTransferEntity.Carrier = txtCarrier.Text.ToString();
                stockTransferEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
                stockTransferEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();

                stockTransferTransactions.UpdateLRdetails(ref stockTransferEntity);
                if ((stockTransferEntity.ErrorMsg == string.Empty) && (stockTransferEntity.ErrorCode == "0"))
                {
                    FreezeOrUnFreezeButtons(false);
                    btnReset.Enabled = true;

                    foreach (GridViewRow gvr in grvItemDetails.Rows)
                    {
                        gvr.Enabled = false;
                    }

                    ShowSummaryInFooter();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Transfer LR details have been Updated successfully. Please Take the Invoice Print.');", true);
                    BtnUpdate.Enabled = false;
                    BtnReport.Visible = true;
                    BtnReport.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + stockTransferEntity.ErrorMsg + "');", true);
                    BtnUpdate.Enabled = false;
                    BtnReport.Visible = false;
                    BtnReport.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void FirstGridViewRow()
        {
            try
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
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();

                grvItemDetails.Rows[0].Cells.Clear();
                grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                grvItemDetails.Rows[0].Cells[0].ColumnSpan = 11;
                ViewState["GridRowCount"] = "0";
                hdnRowCnt.Value = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void BindExistingRowsDuringEdit(List<StockTransferItem> lstStockTransferItem)
        {
            try
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

                DataRow dr = null;
                for (int i = 0; i < lstStockTransferItem.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["SNo"] = lstStockTransferItem[i].SNo.ToString();
                    dr["Col1"] = lstStockTransferItem[i].SupplierLineCode.ToString();
                    dr["Col2"] = lstStockTransferItem[i].SupplierPartNo.ToString();
                    dr["Col3"] = lstStockTransferItem[i].ItemCode.ToString();
                    dr["Col4"] = lstStockTransferItem[i].AvailableQty.ToString();
                    dr["Col5"] = lstStockTransferItem[i].Quantity.ToString();
                    dr["Col6"] = lstStockTransferItem[i].CostPricePerQuantity.ToString();
                    dr["Col7"] = lstStockTransferItem[i].CostPrice.ToString();
                    dr["Col8"] = lstStockTransferItem[i].GSTPercentage.ToString();
                    dr["Col9"] = lstStockTransferItem[i].GSTValue.ToString();
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;
                hdnRowCnt.Value = dt.Rows.Count.ToString();

                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();
                SetPreviousData();
                HideDllItemCodeDropDownForDisplayOnly();

                if (lstStockTransferItem.Count > 0)
                    ShowSummaryInFooter();
                else
                {
                    stockTransferTransactions.UpdateHOSTDNstatus(Session["BranchCode"].ToString(), ddlHOSTDNNumberOnline.SelectedValue);
                    LoadHOSTDNEntriesOnline();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HO STDN # has been cancelled due to No Stock. Please Check and Proceed with Next HO STDN # if available.');", true);                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void AddNewRow()
        {
            try
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

                                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                                TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                                TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                                TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                                TextBox txtGSTValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTValue");

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

                                dtCurrentTable.Rows[i - 1]["Col2"] = txtSupplierPartNo.Text.ToString();
                                dtCurrentTable.Rows[i - 1]["Col3"] = txtItemCode.Text.ToString();
                                dtCurrentTable.Rows[i - 1]["Col4"] = txtAvailableQuantity.Text;
                                dtCurrentTable.Rows[i - 1]["Col5"] = txtQuantity.Text;
                                dtCurrentTable.Rows[i - 1]["Col6"] = txtCstPricePerQty.Text;
                                dtCurrentTable.Rows[i - 1]["Col7"] = txtCostPrice.Text;
                                dtCurrentTable.Rows[i - 1]["Col8"] = txtGSTPercentage.Text;
                                dtCurrentTable.Rows[i - 1]["Col9"] = txtGSTValue.Text;
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
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void HideDllItemCodeDropDown()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
                {
                    DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                    TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");
                    DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                    Button btnSearch = (Button)grvItemDetails.Rows[rowIndex].FindControl("btnSearch");
                    TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");

                    if (rowIndex != grvItemDetails.Rows.Count - 1)
                    {
                        ddlSupplierName.Visible = false;
                        ddlSupplierPartNo.Visible = false;
                        txtSupplierPartNo.Enabled = false;
                        txtQuantity.Enabled = false;
                        btnSearch.Enabled = false;
                        btnSearch.Visible = false;

                        sb.Append(txtQuantity.ClientID + ",");
                    }
                    else
                    {
                        txtSupplierName.Visible = false;
                        txtSupplierPartNo.Enabled = true;
                        txtQuantity.Enabled = true;
                        btnSearch.Enabled = true;
                        btnSearch.Visible = true;

                        sb.Append("" + ddlSupplierName.ClientID + "," + ddlSupplierPartNo.ClientID + "," + txtQuantity.ClientID + ",");
                    }
                }

                txtHdnGridCtrls.Text = sb.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void HideDllItemCodeDropDownForDisplayOnly()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
                {
                    DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierName");
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                    TextBox txtSupplierName = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierName");
                    DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                    Button btnSearch = (Button)grvItemDetails.Rows[rowIndex].FindControl("btnSearch");
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                    TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                    TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                    TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                    TextBox txtGSTValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTValue");
                    if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                    {
                        if (rowIndex != grvItemDetails.Rows.Count - 1)
                        {
                            ddlSupplierName.Visible = false;
                            ddlSupplierPartNo.Visible = false;
                            txtSupplierPartNo.Enabled = false;
                            txtQuantity.Enabled = false;
                            btnSearch.Enabled = false;
                            btnSearch.Visible = false;
                        }
                        else
                        {
                            txtSupplierName.Visible = false;
                            txtSupplierPartNo.Enabled = true;
                            txtQuantity.Enabled = true;
                            btnSearch.Enabled = true;
                            btnSearch.Visible = true;

                            ddlSupplierName.SelectedIndex = ddlSupplierName.Items.IndexOf(ddlSupplierName.Items.FindByText(txtSupplierName.Text));

                            txtQuantity.Attributes.Add("OnChange", "return funSTQuantityValidation('" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "','" + txtGSTPercentage.ClientID + "','" + txtGSTValue.ClientID + "');");
                        }
                    }
                }
                txtHdnGridCtrls.Text = sb.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void SetPreviousData()
        {
            try
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

                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");

                            TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                            TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");
                            TextBox txtGSTValue = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTValue");

                            txtSupplierName.Text = dt.Rows[i]["Col1"].ToString();
                            txtSupplierPartNo.Text = dt.Rows[i]["Col2"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Col3"].ToString();
                            txtAvailableQuantity.Text = dt.Rows[i]["Col4"].ToString();
                            txtQuantity.Text = dt.Rows[i]["Col5"].ToString();
                            txtCstPricePerQty.Text = dt.Rows[i]["Col6"].ToString();
                            txtCostPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col7"].ToString());
                            txtGSTPercentage.Text = dt.Rows[i]["Col8"].ToString();
                            txtGSTValue.Text = TwoDecimalConversion(dt.Rows[i]["Col9"].ToString());

                            rowIndex++;
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void ShowSummaryInFooter()
        {
            decimal sumCostPrice = 0;
            decimal sumGSTValue = 0;

            foreach (GridViewRow gvr in grvItemDetails.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtCostPrice = (TextBox)gvr.FindControl("txtCostPrice");
                    TextBox txtGSTValue = (TextBox)gvr.FindControl("txtGSTValue");

                    if (string.IsNullOrEmpty(txtCostPrice.Text))
                        txtCostPrice.Text = "0";

                    if (string.IsNullOrEmpty(txtGSTValue.Text))
                        txtGSTValue.Text = "0";

                    sumCostPrice += Convert.ToDecimal(txtCostPrice.Text);
                    sumGSTValue += Convert.ToDecimal(txtGSTValue.Text);
                }
            }

            grvItemDetails.FooterRow.Cells[7].Text = sumCostPrice.ToString();
            grvItemDetails.FooterRow.Cells[9].Text = sumGSTValue.ToString();
            hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[7].ClientID;
            hdnFooterGSTvalue.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
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
            try
            {
                if (grvItemDetails.FooterRow != null)
                {
                    Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                    btnAdd.Enabled = Fzflag;
                }

                btnReset.Enabled = Fzflag;
                BtnSubmit.Enabled = Fzflag;
                imgEditToggle.Enabled = Fzflag;
                pnlCI.Enabled = Fzflag;
                pnlSTEntry.Enabled = Fzflag;
                ddlTransactionType.Enabled = !Fzflag;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        private void DefineBranch()
        {
            try
            {
                if (Session["RoleCode"].ToString().ToUpper() == "CORP")
                {
                    ddlBranch.SelectedValue = "0";//Session["BranchCode"].ToString();
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }
    }
}
