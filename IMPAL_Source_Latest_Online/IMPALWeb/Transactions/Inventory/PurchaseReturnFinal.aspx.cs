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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Common;
using System.Data.Common;

namespace IMPALWeb
{
    public partial class PurchaseReturnFinal : System.Web.UI.Page
    {
        string PurchaseReturnNumber = string.Empty;
        EinvAuthGen einvGen = new EinvAuthGen();

        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (cryPurchaseReturnReprint != null)
                    {
                        cryPurchaseReturnReprint.Dispose();
                        cryPurchaseReturnReprint = null;
                    }

                    hdnScreenMode.Value = "A";
                    txtPurchaseReturnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlPurchaseReturnNumber.Visible = false;
                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();
                    DefineBranch();
                    BtnReport.Enabled = false;
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    BtnCancel.Enabled = false;
                    lblMessage.Text = "";
                    if ((string)Session["RoleCode"] == "CORP")
                        ddlBranch.CssClass = "dropDownListNormal";
                    else
                        ddlBranch.CssClass = "dropDownListDisabled";

                }

                BtnSubmit.Attributes.Add("OnClick", "return PurchaseReturnEntrySubmit('M');");
                BtnReject.Attributes.Add("OnClick", "return funPurchaseReturnReject();");
                BtnCancel.Attributes.Add("OnClick", "return funPurchaseReturnCancel();");

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                btnAddRow.Visible = false;

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryPurchaseReturnReprint != null)
            {
                cryPurchaseReturnReprint.Dispose();
                cryPurchaseReturnReprint = null;
            }
        }
        protected void cryPurchaseReturnReprint_Unload(object sender, EventArgs e)
        {
            if (cryPurchaseReturnReprint != null)
            {
                cryPurchaseReturnReprint.Dispose();
                cryPurchaseReturnReprint = null;
            }
        }
        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    ddlPurchaseReturnNumber.SelectedValue = "0";
                    ddlPurchaseReturnNumber.Visible = true;
                    txtPurchaseReturnNumber.Visible = false;
                    ddlPurchaseReturnNumber.DataBind();

                    ddlTransactionType.Enabled = false;
                    ddlSupplierName.Enabled = false;
                    ddlSupplyPlant.Enabled = false;
                    txtRefDocumentNumber.Enabled = false;
                    txtRefDocumentDate.Enabled = false;

                    BtnReport.Enabled = false;

                    FirstGridViewRow();
                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlPurchaseReturnNumber.Visible = false;
                    txtPurchaseReturnNumber.Visible = true;

                    txtPurchaseReturnNumber.Text = string.Empty;
                    txtPurchaseReturnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    FirstGridViewRow();
                    BtnReport.Enabled = false;
                    BtnCancel.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return PurchaseReturnEntrySubmit('M');");
                BtnReject.Attributes.Add("OnClick", "return funPurchaseReturnReject();");
                BtnCancel.Attributes.Add("OnClick", "return funPurchaseReturnCancel();");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");                

                FreezeOrUnFreezeButtons(true);

                grvItemDetails.Enabled = false;
                BtnCancel.Enabled = false;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
                DropDownList ddlSupplierPartNo = (DropDownList)grdRow.FindControl("ddlSupplierPartNo");
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
                    txtQuantity.Text = "";
                    txtCstPricePerQty.Text = "";
                    txtCostPrice.Text = "";
                    txtGSTPercentage.Text = "";
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        StockTransferTransactions stTransactions = new StockTransferTransactions();
                        ddlSupplierPartNo.DataSource = (object)stTransactions.GetItemListBySupplierPartNo(ddlSupplierName.SelectedItem.Text.Substring(0, 3), txtCurrentSearch.Text, ddlBranch.SelectedValue.ToString());
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
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            UpdpanelTop.Visible = false;
            UpdPanelGrid.Visible = false;
            btnBack.Visible = true;

            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strField = "{Purchase_Return_Header.Purchase_Return_Number}";

            strValue = ddlPurchaseReturnNumber.SelectedValue;

            strSelectionFormula = strField + "= " + "'" + strValue + "'";
            strReportName = "po_pp_invoice_PurchaseReturn";

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

            cryPurchaseReturnReprint.ReportName = strReportName;
            cryPurchaseReturnReprint.RecordSelectionFormula = strSelectionFormula;
            cryPurchaseReturnReprint.GenerateReportAndExportA4();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("PurchaseReturnFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(CashDiscount), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("PurchaseReturnFinal.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private bool CheckMultipleTax(string SalesTax)
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    TextBox txtTaxPercentage = (TextBox)gr.Cells[6].FindControl("txtGSTPercentage");
                    if (txtTaxPercentage.Text != "")
                    {
                        if (SalesTax != txtTaxPercentage.Text.Substring(0, txtTaxPercentage.Text.Length - 1))
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
                txtRefDocumentNumber.Text = "";
                txtRefDocumentDate.Text = "";
                txtAddress.Text = "";
                txtGSTIN.Text = "";
                lblMessage.Text = "";

                if (ddlSupplierName.SelectedIndex > 0)
                {
                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();                    
                }
                else
                {
                    ddlSupplyPlant.Items.Clear();
                    ddlSupplyPlant.DataBind();
                }

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        protected void ddlSupplyPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplyPlant.SelectedIndex > 0)
                {
                    StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                    PurchaseReturnEntity purchaseReturnEntity = stockTransferTransactions.SupplyPlantAddressDetails(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                    txtAddress.Text = purchaseReturnEntity.Address;
                    txtGSTIN.Text = purchaseReturnEntity.GSTINNumber;
                    hdnOSLSindicator.Value = purchaseReturnEntity.OSLSindicator;
                }
                else
                {
                    txtAddress.Text = "";
                    txtGSTIN.Text = "";
                    hdnOSLSindicator.Value = "";
                }

                if (hdnOSLSindicator.Value == "L")
                {
                    lblMessage.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>Local Plant i.e SGST/UTGST/CGST</b></font></span>";
                }
                else
                {
                    lblMessage.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6' style='text-decoartion: blink;'><b>OutStation Plant i.e IGST</b></font></span>";
                }

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InwardEntry), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            InwardTransactions objTrans = new InwardTransactions();
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetSupplierDepot(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString());
            return obj;
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                if (ddl.SelectedItem.Text.ToString() == "0")
                    return;

                string selSupplierPartNo = ddl.SelectedItem.Text.ToString();
                
                StockTransferTransactions StockTransferTransactions = new StockTransferTransactions();
                int HSNCode = StockTransferTransactions.CheckHSNCodeItem(ddlSupplierName.SelectedValue, selSupplierPartNo);

                if (HSNCode > 0)
                {
                    string[] result = StockTransferTransactions.GetPurchaseReturnItemDetails(ddlSupplierName.SelectedValue, selSupplierPartNo, ddlBranch.SelectedValue.ToString(), hdnOSLSindicator.Value);

                    if (result != null)
                    {
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                        TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                        TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                        TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                        TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                        TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");

                        bool isSingleTax;

                        if (result[2].ToString().Substring(0, result[2].Length - 1) == "0")
                            isSingleTax = CheckMultipleTax(result[2].ToString().Substring(0, result[2].Length - 1));
                        else
                            isSingleTax = CheckMultipleTax(TwoDecimalConversion(result[2].ToString().Substring(0, result[2].Length - 1)));

                        if (result[2].ToString() == "0.00%")
                        {
                            txtQuantity.Enabled = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GST% is not Available For this item. Please Check the same');", true);
                        }
                        else
                        {
                            if (isSingleTax)
                            {
                                txtItemCode.Text = result[0].ToString();
                                txtQuantity.Text = "";
                                txtQuantity.Enabled = true;
                                txtAvailableQuantity.Text = result[3].ToString();
                                txtCstPricePerQty.Text = TwoDecimalConversion(result[1].ToString());
                                txtGSTPercentage.Text = result[2].ToString();
                                txtQuantity.Attributes.Add("OnChange", "return funQuantityValidation('" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "');");
                                txtQuantity.Focus();
                            }
                            else
                            {
                                txtQuantity.Enabled = false;
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Multiple Tax Purchase Return is not Possible in GST. Please Remove this Part Number');", true);
                            }
                        }
                    }
                }
                else
                {
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                    txtQuantity.Text = "";
                    txtQuantity.Enabled = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HSN Code is not available for this Item');", true);
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        protected void ddlPurchaseReturnNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPurchaseReturnNumber.SelectedValue != "0")
                {

                    StockTransferTransactions stocktransferTransactions = new StockTransferTransactions();
                    PurchaseReturnEntity purchaseReturnEntity = stocktransferTransactions.GetPurchaseReturnDetails(ddlBranch.SelectedValue, ddlPurchaseReturnNumber.SelectedValue.ToString());

                    ddlBranch.SelectedValue = purchaseReturnEntity.BranchCode;
                    txtPurchaseReturnNumber.Text = purchaseReturnEntity.PurchaseReturnNumber;
                    txtPurchaseReturnDate.Text = purchaseReturnEntity.PurchaseReturnDate;
                    ddlTransactionType.SelectedValue = purchaseReturnEntity.TransactionTypeCode;
                    ddlSupplierName.SelectedValue = purchaseReturnEntity.SupplierCode;

                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    ddlSupplyPlant.SelectedValue = purchaseReturnEntity.SupplierPlantCode;
                    txtRefDocumentNumber.Text = purchaseReturnEntity.RefDocNumber;
                    txtRefDocumentDate.Text = purchaseReturnEntity.RefDocDate;
                    txtAddress.Text = purchaseReturnEntity.Address;
                    txtGSTIN.Text = purchaseReturnEntity.GSTINNumber;

                    pnlStatus.Visible = false;

                    if (purchaseReturnEntity.ApprovalStatus == "A")
                    {
                        BtnReport.Visible = true;
                        BtnReport.Enabled = true;
                        BtnSubmit.Enabled = false;
                        BtnReject.Enabled = false;
                        BtnCancel.Enabled = true;
                        hdnScreenMode.Value = "E";
                    }
                    else
                    {
                        BtnReport.Visible = false;
                        BtnSubmit.Enabled = true;
                        BtnReject.Enabled = true;
                        BtnCancel.Enabled = false;
                        hdnScreenMode.Value = "A";
                    }

                    BindExistingRowsDuringEdit(purchaseReturnEntity.Items);
                }
                else
                {
                    pnlStatus.Visible = false;
                    txtPurchaseReturnDate.Text = "";
                    ddlTransactionType.SelectedValue = "0";
                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                    BtnCancel.Visible = false;
                    FirstGridViewRow();
                }

                BtnReport.Enabled = true;
                btnReset.Enabled = true;

                Button btnAddRow = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                btnAddRow.Visible = false;

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    gvr.Enabled = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.SelectedValue == "Inactive" && txtPurchaseReturnDate.Text == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    StockTransferTransactions tran = new StockTransferTransactions();
                    tran.EditPurchaseReturnEntry(ddlBranch.SelectedValue ,ddlPurchaseReturnNumber.SelectedValue, "I", "I", "Inactivated");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Number has been Inactivated Sucessfully');", true);
                    ddlPurchaseReturnNumber.Enabled = false;
                    ddlStatus.Enabled = false;
                    BtnCancel.Enabled = false;
                    BtnReport.Enabled = false;
                }
                else
                {
                    SubmitHeaderAndItems();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ddlStatus.Enabled = false;
                BtnReport.Enabled = false;
                BtnCancel.Enabled = false;
                ddlPurchaseReturnNumber.Enabled = false;
                StockTransferTransactions tran = new StockTransferTransactions();
                tran.EditPurchaseReturnEntry(ddlBranch.SelectedValue, ddlPurchaseReturnNumber.SelectedValue, "I", "R", Session["PurchaseReturnRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Number has been Rejected/Cancelled Sucessfully');", true);                                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                BtnCancel.Enabled = false;
                ddlPurchaseReturnNumber.Enabled = false;
                StockTransferTransactions tran = new StockTransferTransactions();

                DataSet ds = tran.EditPurchaseReturnEntry(ddlBranch.SelectedValue, ddlPurchaseReturnNumber.SelectedValue, "I", "I", Session["PurchaseReturnRemarks"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string JSONData = "{\"supplier_gstin\":\"" + ds.Tables[0].Rows[0]["GSTIN"] + "\",\"doc_no\":\"" + ds.Tables[0].Rows[0]["Document_Number"] + "\"," + "\"irn_no\":\"" + ds.Tables[0].Rows[0]["IRN_Number"] + "\"," +
                                      "\"doc_date\":\"" + ds.Tables[0].Rows[0]["Document_Date"] + "\",\"reason\":\"" + ds.Tables[0].Rows[0]["Reason"] + "\",\"remark\":\"" + ds.Tables[0].Rows[0]["Remarks"] + "\"}";

                    einvGen.EinvoiceAuthentication(JSONData, ddlBranch.SelectedValue.ToString(), ddlPurchaseReturnNumber.SelectedItem.Text.ToString(), "2", "CANIRN", ds.Tables[0].Rows[0]["GSTIN"].ToString(), ds.Tables[0].Rows[0]["Document_Type"].ToString(), ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), ds);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Entry has been Cancelled Sucessfully');", true);
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
                objp.Session["PurchaseReturnRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
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
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
                }
                else
                {
                    FirstGridViewRow();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private void FillLastRowInfoInGrid(ref DataTable dt)
        {
            try
            {
                int rowIndex = grvItemDetails.Rows.Count - 1;

                DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].Cells[5].FindControl("txtGSTPercentage");

                dt.Rows[rowIndex]["Col1"] = ddlSupplierPartNo.SelectedValue.ToString();
                dt.Rows[rowIndex]["Col2"] = txtItemCode.Text;
                dt.Rows[rowIndex]["Col3"] = txtQuantity.Text;
                dt.Rows[rowIndex]["Col4"] = txtAvailableQuantity.Text;
                dt.Rows[rowIndex]["Col5"] = txtCstPricePerQty.Text;
                dt.Rows[rowIndex]["Col6"] = txtCostPrice.Text;
                dt.Rows[rowIndex]["Col7"] = txtGSTPercentage.Text;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private void SubmitHeaderAndItems()
        {
            try
            {
                PurchaseReturnEntity purchaseReturnEntity = new PurchaseReturnEntity();
                purchaseReturnEntity.Items = new List<PurchaseReturnItem>();

                purchaseReturnEntity.BranchCode = ddlBranch.SelectedValue.ToString();

                if (ddlPurchaseReturnNumber.Visible)
                    purchaseReturnEntity.PurchaseReturnNumber = ddlPurchaseReturnNumber.SelectedValue.ToString();
                else
                    purchaseReturnEntity.PurchaseReturnNumber = txtPurchaseReturnNumber.Text.ToString();

                purchaseReturnEntity.PurchaseReturnDate = txtPurchaseReturnDate.Text.ToString();
                purchaseReturnEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                purchaseReturnEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
                purchaseReturnEntity.SupplierPlantCode = ddlSupplyPlant.SelectedValue.ToString();
                purchaseReturnEntity.RefDocNumber = txtRefDocumentNumber.Text.ToString();
                purchaseReturnEntity.RefDocDate = txtRefDocumentDate.Text.ToString();
                purchaseReturnEntity.OSLSindicator = hdnOSLSindicator.Value;
                purchaseReturnEntity.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];

                PurchaseReturnItem PurchaseReturnItem = null;
                int SNo = 0;
                decimal dmlTotal = 0;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    PurchaseReturnItem = new PurchaseReturnItem();
                    SNo += 1;

                    DropDownList ddlSupplierPartNo = (DropDownList)gr.FindControl("ddlSupplierPartNo");
                    TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                    TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");
                    TextBox txtGSTPercentage = (TextBox)gr.FindControl("txtGSTPercentage");

                    dmlTotal += Convert.ToDecimal(txtCostPrice.Text);

                    PurchaseReturnItem.SNo = SNo.ToString();

                    PurchaseReturnItem.SupplierLineCode = ddlSupplierName.SelectedValue.ToString();

                    if (ddlSupplierPartNo.Visible)
                        PurchaseReturnItem.SupplierPartNo = ddlSupplierPartNo.SelectedValue.ToString();
                    else
                        PurchaseReturnItem.SupplierPartNo = txtSupplierPartNo.Text.ToString();

                    PurchaseReturnItem.ItemCode = txtItemCode.Text.ToString();
                    PurchaseReturnItem.Quantity = txtQuantity.Text;
                    PurchaseReturnItem.CostPricePerQuantity = txtCstPricePerQty.Text;
                    PurchaseReturnItem.CostPrice = txtCostPrice.Text;
                    PurchaseReturnItem.GSTPercentage = txtGSTPercentage.Text;

                    purchaseReturnEntity.Items.Add(PurchaseReturnItem);
                }

                StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                if (hdnScreenMode.Value == "A")
                {
                    stockTransferTransactions.AddNewPurchaseReturn(ref purchaseReturnEntity, ddlStatus.SelectedValue);
                    if ((purchaseReturnEntity.ErrorMsg == string.Empty) && (purchaseReturnEntity.ErrorCode == "0"))
                    {
                        txtPurchaseReturnNumber.Text = purchaseReturnEntity.PurchaseReturnNumber;
                        PurchaseReturnNumber = purchaseReturnEntity.PurchaseReturnNumber;

                        DataSet Datasetresult = stockTransferTransactions.GetEinvoicingDetailsPurchaseReturn(ddlBranch.SelectedValue.ToString(), purchaseReturnEntity.PurchaseReturnNumber);

                        GenerateJSON objGenJsonData = new GenerateJSON();

                        string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                        einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, purchaseReturnEntity.PurchaseReturnNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                        FreezeOrUnFreezeButtons(false);
                        btnReset.Enabled = true;

                        foreach (GridViewRow gvr in grvItemDetails.Rows)
                        {
                            gvr.Enabled = false;
                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Entry Has been Generated Successfully with QR Code.');", true);
                        BtnReport.Enabled = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + purchaseReturnEntity.ErrorMsg + "');", true);
                        BtnReport.Enabled = false;
                    }
                }

                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                BtnCancel.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();

                grvItemDetails.Rows[0].Cells.Clear();
                grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                grvItemDetails.Rows[0].Cells[0].ColumnSpan = 8;
                ViewState["GridRowCount"] = "0";
                hdnRowCnt.Value = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private void BindExistingRowsDuringEdit(List<PurchaseReturnItem> lstPurchaseReturnItem)
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

                DataRow dr = null;
                for (int i = 0; i < lstPurchaseReturnItem.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["SNo"] = lstPurchaseReturnItem[i].SNo.ToString();
                    dr["Col1"] = lstPurchaseReturnItem[i].SupplierPartNo.ToString();
                    dr["Col2"] = lstPurchaseReturnItem[i].ItemCode.ToString();
                    dr["Col3"] = lstPurchaseReturnItem[i].Quantity.ToString();
                    dr["Col4"] = lstPurchaseReturnItem[i].AvailableQuantity.ToString();
                    dr["Col5"] = lstPurchaseReturnItem[i].CostPricePerQuantity.ToString();
                    dr["Col6"] = lstPurchaseReturnItem[i].CostPrice.ToString();
                    dr["Col7"] = lstPurchaseReturnItem[i].GSTPercentage.ToString();
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;
                hdnRowCnt.Value = dt.Rows.Count.ToString();

                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();
                grvItemDetails.Columns[4].Visible = false;
                SetPreviousData();
                HideDllItemCodeDropDownForDisplayOnly();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private void HideDllItemCodeDropDown()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
                {
                    DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                    Button btnSearch = (Button)grvItemDetails.Rows[rowIndex].FindControl("btnSearch");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                    
                    if (rowIndex != grvItemDetails.Rows.Count - 1)
                    {
                        ddlSupplierPartNo.Visible = false;
                        txtSupplierPartNo.Enabled = false;
                        txtQuantity.Enabled = false;
                        btnSearch.Enabled = false;
                        btnSearch.Visible = false;

                        sb.Append(txtQuantity.ClientID + ",");
                    }
                    else
                    {
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
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }

        private void HideDllItemCodeDropDownForDisplayOnly()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int rowIndex = 0; rowIndex < grvItemDetails.Rows.Count; rowIndex++)
                {
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                    DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                    Button btnSearch = (Button)grvItemDetails.Rows[rowIndex].FindControl("btnSearch");                   
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                    TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                    TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                    
                    if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                    {
                        if (rowIndex != grvItemDetails.Rows.Count - 1)
                        {
                            ddlSupplierPartNo.Visible = false;
                            txtSupplierPartNo.Enabled = false;
                            txtQuantity.Enabled = false;
                            btnSearch.Enabled = false;
                            btnSearch.Visible = false;
                        }
                        else
                        {
                            txtSupplierPartNo.Enabled = true;
                            txtQuantity.Enabled = true;
                            btnSearch.Enabled = true;

                            if (ddlPurchaseReturnNumber.Visible && ddlPurchaseReturnNumber.SelectedIndex > 0)
                                btnSearch.Visible = false;
                            else
                                btnSearch.Visible = true;

                            txtQuantity.Attributes.Add("OnChange", "return funSTQuantityValidation('" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "');");
                        }
                    }
                }

                txtHdnGridCtrls.Text = sb.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
                            DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                            TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                            TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].Cells[5].FindControl("txtGSTPercentage");

                            txtSupplierPartNo.Text = dt.Rows[i]["Col1"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Col2"].ToString();
                            txtQuantity.Text = dt.Rows[i]["Col3"].ToString();
                            txtAvailableQuantity.Text = dt.Rows[i]["Col4"].ToString();
                            txtCstPricePerQty.Text = dt.Rows[i]["Col5"].ToString();
                            txtCostPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col6"].ToString());
                            txtGSTPercentage.Text = dt.Rows[i]["Col7"].ToString();

                            rowIndex++;
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
            try
            {
                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("ButtonAdd");
                btnAdd.Enabled = Fzflag;
                btnReset.Enabled = Fzflag;
                BtnSubmit.Enabled = Fzflag;
                BtnCancel.Enabled = Fzflag;
                imgEditToggle.Enabled = Fzflag;
                pnlSTEntry.Enabled = Fzflag;
                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        ImageButton imgButton = (ImageButton)gvr.FindControl("ImgRefStockTransfeDate");
                        if (imgButton != null)
                            imgButton.Enabled = Fzflag;
                    }
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
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
                Log.WriteException(typeof(PurchaseReturnFinal), exp);
            }
        }


    }
}
