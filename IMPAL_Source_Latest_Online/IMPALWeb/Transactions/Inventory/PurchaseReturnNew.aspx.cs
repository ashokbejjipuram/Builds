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

namespace IMPALWeb
{
    public partial class PurchaseReturnNew : System.Web.UI.Page
    {
        string PurchaseReturnNumber = string.Empty;
        EinvAuthGen einvGen = new EinvAuthGen();
        StockTransferTransactions stTransactions = new StockTransferTransactions();

        #region Page Init
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
        #endregion

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
                    
                    txtPurchaseReturnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    FirstGridViewRow();
                    ddlPurchaseReturnNumber.Visible = false;
                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();
                    DefineBranch();
                    ddlInwardNumber.Visible = false;
                    BtnReport.Enabled = false;
                    BtnReport.Visible = false;
                    lblMessage.Text = "";
                    ddlBranch.CssClass = "dropDownListDisabled";
                    ChkStatus.Value = "0";

                    BtnSubmit.Attributes.Add("Style", "display:none");
                    grvItemDetails.Enabled = false;
                    lblTotalPurchaseReturnVal.Text = "0";
                }

                BtnSubmit.Attributes.Add("OnClick", "return PurchaseReturnEntrySubmit('E');");
                imgEditToggle1.Attributes.Add("OnClick", "return ValidateInwardNumber();");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
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
                imgEditToggle.Visible = false;
                imgEditToggle1.Visible = false;

                ddlPurchaseReturnNumber.Visible = true;
                txtPurchaseReturnNumber.Visible = false;

                txtInwardNumber.Enabled = false;
                ddlRemarks.Enabled = false;
                txtPurchaseReturnNumber.Text = string.Empty;
                txtPurchaseReturnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                FirstGridViewRow();
                BtnReport.Enabled = false;

                BtnSubmit.Attributes.Add("OnClick", "return PurchaseReturnEntrySubmit('E');");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                FreezeOrUnFreezeButtons(true);

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        protected void imgEditToggle1_Click(object sender, EventArgs e)
        {
            try
            {
                imgEditToggle1.Visible = false;
                imgEditToggle.Visible = false;

                List<IMPALLibrary.Transactions.Item> InwardNo = new List<IMPALLibrary.Transactions.Item>();

                InwardNo = stTransactions.GetInwardNoForPurchaseReturn(ddlBranch.SelectedValue.ToString(), txtInwardNumber.Text.Trim());
                ddlInwardNumber.DataSource = InwardNo;
                ddlInwardNumber.DataTextField = "ItemDesc";
                ddlInwardNumber.DataValueField = "ItemCode";
                ddlInwardNumber.DataBind();

                ddlInwardNumber.Visible = true;
                txtInwardNumber.Visible = false;

                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        protected void ddlInwardNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InwardEntity inwardEntity = stTransactions.GetInwardDetailsForPurchaseReturn(ddlInwardNumber.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString());

                if (inwardEntity != null)
                {
                    txtInwardDate.Text = inwardEntity.InwardDate;
                    txtRefDocumentNumber.Text = inwardEntity.InvoiceNumber;
                    txtRefDocumentDate.Text = inwardEntity.InvoiceDate;
                    txtReceivedDate.Text = inwardEntity.ReceivedDate;
                    ddlSupplierName.SelectedValue = inwardEntity.SupplierCode;

                    ddlSupplyPlant.DataSource = (object)GetSupplyplants(inwardEntity.SupplierCode);
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    ddlSupplyPlant.SelectedValue = inwardEntity.SupplyPlantCode;

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

                    if (inwardEntity.Items.Count > 0)
                    {
                        BindExistingRows(inwardEntity.Items);
                        BtnSubmit.Attributes.Add("Style", "display:inline");
                        grvItemDetails.Enabled = true;
                    }
                    else
                    {
                        BtnSubmit.Attributes.Add("Style", "display:none");
                        grvItemDetails.DataSource = null;
                        grvItemDetails.DataBind();
                        grvItemDetails.Enabled = false;
                    }                    

                    PurchaseReturnEntity purchaseReturnEntity = stTransactions.SupplyPlantAddressDetails(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                    txtAddress.Text = purchaseReturnEntity.Address;
                    txtGSTIN.Text = purchaseReturnEntity.GSTINNumber;
                    hdnOSLSindicator.Value = purchaseReturnEntity.OSLSindicator;

                    if (hdnOSLSindicator.Value == "L")
                    {
                        lblMessage.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='5'><b>Local Plant i.e SGST/UTGST/CGST</b></font></span>";
                    }
                    else
                    {
                        lblMessage.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='5' style='text-decoartion: blink;'><b>OutStation Plant i.e IGST</b></font></span>";
                    }

                    if (grvItemDetails.FooterRow != null)
                    {
                        hdnFooterCostPrice.Value = grvItemDetails.FooterRow.Cells[9].ClientID;
                        hdnFooterTaxPrice.Value = grvItemDetails.FooterRow.Cells[10].ClientID;
                    }
                }
                else
                {
                    BtnReset_Click(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
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

            if (ddlPurchaseReturnNumber.Visible)
                strValue = ddlPurchaseReturnNumber.SelectedValue;
            else
                strValue = txtPurchaseReturnNumber.Text;

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
                Response.Redirect("PurchaseReturnNew.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("PurchaseReturnNew.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
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

        protected void ddlSupplyPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplyPlant.SelectedIndex > 0)
                {
                    PurchaseReturnEntity purchaseReturnEntity = stTransactions.SupplyPlantAddressDetails(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

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

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants(string SupplierName)
        {
            InwardTransactions objTrans = new InwardTransactions();
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetSupplierDepot(ddlBranch.SelectedValue.ToString(), SupplierName);
            return obj;
        }

        protected void ddlPurchaseReturnNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPurchaseReturnNumber.SelectedValue != "0")
                {
                    PurchaseReturnEntity purchaseReturnEntity = stTransactions.GetPurchaseReturnDetailsNew(ddlBranch.SelectedValue, ddlPurchaseReturnNumber.SelectedValue.ToString());

                    ddlBranch.SelectedValue = purchaseReturnEntity.BranchCode;
                    txtPurchaseReturnNumber.Text = purchaseReturnEntity.PurchaseReturnNumber;
                    txtPurchaseReturnDate.Text = purchaseReturnEntity.PurchaseReturnDate;
                    ddlTransactionType.SelectedValue = purchaseReturnEntity.TransactionTypeCode;
                    txtInwardNumber.Text = purchaseReturnEntity.InwardNumber;
                    txtInwardDate.Text = purchaseReturnEntity.InwardDate;
                    txtReceivedDate.Text = purchaseReturnEntity.ReceivedDate;
                    ddlSupplierName.SelectedValue = purchaseReturnEntity.SupplierCode;

                    ddlSupplyPlant.DataSource = (object)GetSupplyplants(purchaseReturnEntity.SupplierCode);
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    ddlSupplyPlant.SelectedValue = purchaseReturnEntity.SupplierPlantCode;
                    txtRefDocumentNumber.Text = purchaseReturnEntity.RefDocNumber;
                    txtRefDocumentDate.Text = purchaseReturnEntity.RefDocDate;
                    txtAddress.Text = purchaseReturnEntity.Address;
                    txtGSTIN.Text = purchaseReturnEntity.GSTINNumber;
                    ddlRemarks.SelectedValue = purchaseReturnEntity.Remarks;

                    if (txtPurchaseReturnDate.Text == DateTime.Today.ToString("dd/MM/yyyy") && (purchaseReturnEntity.ApprovalStatus == string.Empty || purchaseReturnEntity.ApprovalStatus == ""))
                    {
                        pnlStatus.Visible = true;
                    }
                    else
                    {
                        pnlStatus.Visible = false;
                    }

                    if (purchaseReturnEntity.ApprovalStatus == "A")
                    {
                        BtnReport.Visible = true;
                        BtnReport.Enabled = true;
                    }
                    else
                    {
                        BtnReport.Visible = false;
                    }

                    BindExistingRowsView(purchaseReturnEntity.Items);
                }
                else
                {
                    pnlStatus.Visible = false;
                    txtPurchaseReturnDate.Text = "";
                    ddlTransactionType.SelectedValue = "0";
                    FirstGridViewRow();
                }

                BtnReport.Enabled = true;
                btnReset.Enabled = true;
                BtnSubmit.Enabled = false;                

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    gvr.Enabled = false;
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Attributes.Add("Style", "display:none");

                if (ddlStatus.SelectedValue == "Inactive" && txtPurchaseReturnDate.Text == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    DataSet ds = stTransactions.EditPurchaseReturnEntryNew(ddlBranch.SelectedValue, ddlPurchaseReturnNumber.SelectedValue, "I", "I", "EDP");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Number has been Inactivated Sucessfully');", true);
                    ddlPurchaseReturnNumber.Enabled = false;
                    ddlStatus.Enabled = false;
                    BtnReport.Enabled = false;
                    BtnReport.Visible = false;
                }
                else
                {
                    SubmitHeaderAndItems();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        private void SubmitHeaderAndItems()
        {
            try
            {
                BtnReport.Visible = false;
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
                purchaseReturnEntity.InwardNumber = ddlInwardNumber.SelectedValue.ToString();
                purchaseReturnEntity.RefDocNumber = txtRefDocumentNumber.Text.ToString();
                purchaseReturnEntity.RefDocDate = txtRefDocumentDate.Text.ToString();
                purchaseReturnEntity.OSLSindicator = hdnOSLSindicator.Value;
                purchaseReturnEntity.Remarks = ddlRemarks.SelectedValue;

                PurchaseReturnItem PurchaseReturnItem = null;
                int SNo = 0;
                decimal dmlTotal = 0;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    CheckBox chkSelected = (CheckBox)gr.FindControl("chkSelected");

                    if (!chkSelected.Checked)
                        continue;

                    PurchaseReturnItem = new PurchaseReturnItem();
                    SNo += 1;

                    HiddenField hdnInwardSno = (HiddenField)gr.FindControl("hdnInwardSno");
                    TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                    TextBox txtItemCode = (TextBox)gr.FindControl("txtItemCode");
                    TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                    TextBox txtCstPricePerQty = (TextBox)gr.FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)gr.FindControl("txtCostPrice");
                    TextBox txtGSTPercentage = (TextBox)gr.FindControl("txtGSTPercentage");

                    dmlTotal += Convert.ToDecimal(txtCostPrice.Text);
                    
                    if (SNo != Convert.ToInt16(ChkStatus.Value))
                        PurchaseReturnItem.SNo = SNo.ToString();
                    else
                        PurchaseReturnItem.SNo = "M";

                    PurchaseReturnItem.InwardSerialNumber = hdnInwardSno.Value;
                    PurchaseReturnItem.SupplierLineCode = ddlSupplierName.SelectedValue.ToString();
                    PurchaseReturnItem.SupplierPartNo = txtSupplierPartNo.Text.ToString();
                    PurchaseReturnItem.ItemCode = txtItemCode.Text.ToString();
                    PurchaseReturnItem.Quantity = txtQuantity.Text;
                    PurchaseReturnItem.CostPricePerQuantity = txtCstPricePerQty.Text;
                    PurchaseReturnItem.CostPrice = txtCostPrice.Text;
                    PurchaseReturnItem.GSTPercentage = txtGSTPercentage.Text;
                    PurchaseReturnItem.Count = ChkStatus.Value;

                    purchaseReturnEntity.Items.Add(PurchaseReturnItem);
                }

                stTransactions.AddNewPurchaseReturnNew(ref purchaseReturnEntity, ddlStatus.SelectedValue);
                if ((purchaseReturnEntity.ErrorMsg == string.Empty) && (purchaseReturnEntity.ErrorCode == "0"))
                {
                    txtPurchaseReturnNumber.Text = purchaseReturnEntity.PurchaseReturnNumber;
                    PurchaseReturnNumber = purchaseReturnEntity.PurchaseReturnNumber;
                    FreezeOrUnFreezeButtons(false);
                    btnReset.Enabled = true;

                    ddlRemarks.Enabled = false;
                    grvItemDetails.Enabled = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Purchase Return Entry Has been Saved Successfully. Please Get the Manager/Accountant Approval for Invoice Print');", true);
                    BtnReport.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + purchaseReturnEntity.ErrorMsg + "');", true);
                    BtnReport.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        private void FirstGridViewRow()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo", typeof(string)));
                dt.Columns.Add(new DataColumn("Col0", typeof(string)));
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
                    dr["Col0"] = string.Empty;
                    dr["Col1"] = string.Empty;
                    dr["Col2"] = string.Empty;
                    dr["Col3"] = string.Empty;
                    dr["Col4"] = string.Empty;
                    dr["Col5"] = string.Empty;
                    dr["Col6"] = string.Empty;
                    dr["Col7"] = string.Empty;
                    dr["Col8"] = string.Empty;
                    dr["Col8"] = string.Empty;
                    dr["Col9"] = string.Empty;
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
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        private void BindExistingRowsView(List<PurchaseReturnItem> lstPurchaseReturnItem)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo", typeof(string)));
                dt.Columns.Add(new DataColumn("Col0", typeof(string)));
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
                for (int i = 0; i < lstPurchaseReturnItem.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["SNo"] = lstPurchaseReturnItem[i].SNo.ToString();
                    dr["Col0"] = lstPurchaseReturnItem[i].InwardSerialNumber.ToString();
                    dr["Col1"] = lstPurchaseReturnItem[i].SupplierPartNo.ToString();
                    dr["Col2"] = lstPurchaseReturnItem[i].ItemCode.ToString();
                    dr["Col3"] = lstPurchaseReturnItem[i].ItemDescription.ToString();
                    dr["Col4"] = lstPurchaseReturnItem[i].InwardQuantity.ToString();
                    dr["Col5"] = lstPurchaseReturnItem[i].AvailableQuantity.ToString();
                    dr["Col6"] = lstPurchaseReturnItem[i].Quantity.ToString();
                    dr["Col7"] = lstPurchaseReturnItem[i].CostPricePerQuantity.ToString();
                    dr["Col8"] = lstPurchaseReturnItem[i].CostPrice.ToString();
                    dr["Col9"] = lstPurchaseReturnItem[i].GSTPercentage.ToString();
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;
                hdnRowCnt.Value = dt.Rows.Count.ToString();

                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();
                grvItemDetails.Columns[5].Visible = false;
                grvItemDetails.Columns[6].Visible = false;
                SetPreviousData();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        private void BindExistingRows(List<InwardItem> InwardItem)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo", typeof(string)));
                dt.Columns.Add(new DataColumn("Col0", typeof(string)));
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
                for (int i = 0; i < InwardItem.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["SNo"] = InwardItem[i].SNO.ToString();
                    dr["Col0"] = InwardItem[i].SerialNo.ToString();
                    dr["Col1"] = InwardItem[i].SupplierPartNumber.ToString();
                    dr["Col2"] = InwardItem[i].ItemCode.ToString();
                    dr["Col3"] = InwardItem[i].ItemDescription.ToString();
                    dr["Col4"] = InwardItem[i].ReceivedQuantity.ToString();
                    dr["Col5"] = InwardItem[i].BalanceQuantity.ToString();
                    dr["Col6"] = InwardItem[i].Quantity.ToString();
                    dr["Col7"] = InwardItem[i].CostPricePerQty.ToString();
                    dr["Col8"] = InwardItem[i].CostPrice.ToString();
                    dr["Col9"] = InwardItem[i].ItemTaxPercentage.ToString();
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;
                hdnRowCnt.Value = dt.Rows.Count.ToString();

                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();
                SetPreviousData();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
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
                            HiddenField hdnInwardSno = (HiddenField)grvItemDetails.Rows[rowIndex].FindControl("hdnInwardSno");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                            TextBox txtItemDescription = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemDescription");
                            TextBox txtInwardQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInwardQuantity");
                            TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                            TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                            TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");
                            TextBox txtGSTPercentage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtGSTPercentage");

                            hdnInwardSno.Value = dt.Rows[i]["Col0"].ToString();
                            txtSupplierPartNo.Text = dt.Rows[i]["Col1"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Col2"].ToString();
                            txtItemDescription.Text = dt.Rows[i]["Col3"].ToString();
                            txtInwardQuantity.Text = dt.Rows[i]["Col4"].ToString();
                            txtAvailableQuantity.Text = dt.Rows[i]["Col5"].ToString();
                            txtQuantity.Text = dt.Rows[i]["Col6"].ToString();
                            txtCstPricePerQty.Text = TwoDecimalConversion(dt.Rows[i]["Col7"].ToString());
                            txtCostPrice.Text = TwoDecimalConversion(dt.Rows[i]["Col8"].ToString());
                            txtGSTPercentage.Text = TwoDecimalConversion(dt.Rows[i]["Col9"].ToString());

                            txtQuantity.Attributes.Add("OnChange", "return funQuantityValidationNew('" + txtInwardQuantity.ClientID + "','" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "');");

                            txtQuantity.Enabled = false;

                            rowIndex++;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            try
            {
                btnReset.Enabled = Fzflag;
                BtnSubmit.Enabled = Fzflag;
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
                Log.WriteException(typeof(PurchaseReturnNew), exp);
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
                Log.WriteException(typeof(PurchaseReturnNew), exp);
            }
        }


    }
}
