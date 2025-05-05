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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Common;
using System.Data.Common;

namespace IMPALWeb
{
    public partial class WarrantyClaimLucasLabourCharges : System.Web.UI.Page
    {
        EinvAuthGen einvGen = new EinvAuthGen();

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

                    if (cryLabourChargesReprint != null)
                    {
                        cryLabourChargesReprint.Dispose();
                        cryLabourChargesReprint = null;
                    }

                    hdnScreenMode.Value = "A";
                    txtLabourChargesDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlLabourChargesNumber.Visible = false;
                    FreezeOrUnFreezeButtons(true);
                    ddlTransactionType.Focus();
                    DefineBranch();
                    BtnReport.Enabled = false;
                    BtnReport.Visible = false;
                    lblMessage.Text = "";
                    ddlBranch.CssClass = "dropDownListDisabled";
                    grvItemDetails.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return LabourChargesEntrySubmit('E');");

                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryLabourChargesReprint != null)
            {
                cryLabourChargesReprint.Dispose();
                cryLabourChargesReprint = null;
            }
        }
        protected void cryLabourChargesReprint_Unload(object sender, EventArgs e)
        {
            if (cryLabourChargesReprint != null)
            {
                cryLabourChargesReprint.Dispose();
                cryLabourChargesReprint = null;
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    ddlLabourChargesNumber.SelectedValue = "0";
                    ddlLabourChargesNumber.Visible = true;
                    txtLabourChargesNumber.Visible = false;
                    ddlLabourChargesNumber.DataBind();
                    ddlWarrantyClaimNumber.Enabled = false;
                    txtWarrantyClaimDate.Enabled = false;
                    txtLabourCharges.Enabled = false;

                    ddlTransactionType.Enabled = false;
                    ddlSupplierName.Enabled = false;
                    ddlSupplyPlant.Enabled = false;
                    txtRefDocumentNumber.Enabled = false;
                    txtRefDocumentDate.Enabled = false;
                    txtJobCardNumber.Enabled = false;
                    txtJobCardDate.Enabled = false;
                    txtShipTo.Enabled = false;
                    lblMessage.Visible = true;

                    BtnReport.Enabled = false;
                    
                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlLabourChargesNumber.Visible = false;
                    txtLabourChargesNumber.Visible = true;

                    txtLabourChargesNumber.Text = string.Empty;
                    txtLabourChargesDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    BtnReport.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return LabourChargesEntrySubmit('E');");
                btnReset.Attributes.Add("OnClick", "return funReset();");
                imgEditToggle.Attributes.Add("OnClick", "return funEditToggle();");

                FreezeOrUnFreezeButtons(true);

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }
        protected void BtnReport_Click(object sender, EventArgs e)
        {
            btnBack.Visible = true;

            string strSelectionFormula = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strField = "{Warranty_Claim_Lucas_Miscellaneous_Header.Labour_Handling_Number}";

            if (ddlLabourChargesNumber.Visible)
                strValue = ddlLabourChargesNumber.SelectedValue;
            else
                strValue = txtLabourChargesNumber.Text;

            strSelectionFormula = strField + "= " + "'" + strValue + "'";
            strReportName = "WarrantyClaim_Lucas_LabourCharges";

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

            cryLabourChargesReprint.ReportName = strReportName;
            cryLabourChargesReprint.RecordSelectionFormula = strSelectionFormula;
            cryLabourChargesReprint.GenerateReportAndExportA4();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("WarrantyClaimLucasLabourCharges.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("WarrantyClaimLucasLabourCharges.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
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
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        protected void ddlSupplyPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplyPlant.SelectedIndex > 0)
                {
                    StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                    WarrantyClaimLucasEntity warrantyclaimLucas = stockTransferTransactions.WarrantyClaimSupplyPlantAddressDetails(ddlSupplierName.SelectedValue, ddlSupplyPlant.SelectedValue, Session["BranchCode"].ToString());

                    txtAddress.Text = warrantyclaimLucas.Address;
                    txtGSTIN.Text = warrantyclaimLucas.GSTINNumber;
                    hdnOSLSindicator.Value = warrantyclaimLucas.OSLSindicator;
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
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            InwardTransactions objTrans = new InwardTransactions();
            List<IMPALLibrary.Transactions.Item> obj = objTrans.GetSupplierDepot(ddlBranch.SelectedValue.ToString(), ddlSupplierName.SelectedValue.ToString());
            return obj;
        }
        protected void ddlLabourChargesNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLabourChargesNumber.SelectedValue != "0")
            {
                StockTransferTransactions stocktransferTransactions = new StockTransferTransactions();
                WarrantyClaimLucasEntity warrantyclaimLucas = stocktransferTransactions.GetWarrantyClaimLucasLabourHandlingChargesDetails(ddlBranch.SelectedValue, ddlLabourChargesNumber.SelectedValue.ToString());

                ddlBranch.SelectedValue = warrantyclaimLucas.BranchCode;
                ddlLabourChargesNumber.SelectedValue = warrantyclaimLucas.LabourHandlingNumber;
                txtLabourChargesDate.Text = warrantyclaimLucas.LabourHandlingDate;

                if (warrantyclaimLucas.WarrantyClaimNumber != "0")
                {
                    ddlWarrantyClaimNumber.SelectedItem.Text = warrantyclaimLucas.WarrantyClaimNumber;
                    txtWarrantyClaimDate.Text = warrantyclaimLucas.WarrantyClaimDate;
                }
                else
                {
                    ddlWarrantyClaimNumber.SelectedItem.Text = "";
                    txtWarrantyClaimDate.Text = "";
                }

                txtLabourCharges.Text = TwoDecimalConversion(warrantyclaimLucas.LabourHandlingCharges);
                ddlSupplierName.SelectedValue = warrantyclaimLucas.SupplierCode;

                ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                ddlSupplyPlant.DataTextField = "ItemDesc";
                ddlSupplyPlant.DataValueField = "ItemCode";
                ddlSupplyPlant.DataBind();

                ddlSupplyPlant.SelectedValue = warrantyclaimLucas.SupplierPlantCode;
                txtRefDocumentNumber.Text = warrantyclaimLucas.RefDocNumber;
                txtRefDocumentDate.Text = warrantyclaimLucas.RefDocDate;
                txtJobCardNumber.Text = warrantyclaimLucas.JobCardNumber;
                txtJobCardDate.Text = warrantyclaimLucas.JobCardDate;
                txtShipTo.Text = warrantyclaimLucas.ShipTo;
                txtAddress.Text = warrantyclaimLucas.Address;
                txtGSTIN.Text = warrantyclaimLucas.GSTINNumber;
                lblMessage.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>" + warrantyclaimLucas.TaxMessage + "</b></span";

                if (txtLabourChargesDate.Text == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    pnlStatus.Visible = true;
                    BtnSubmit.Visible = true;
                    BtnSubmit.Enabled = true;
                }
                else
                {
                    pnlStatus.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnSubmit.Enabled = false;
                }

                if (warrantyclaimLucas.ApprovalStatus == "A")
                {
                    BtnReport.Visible = true;
                    BtnReport.Enabled = true;
                }
                else
                {
                    BtnReport.Visible = false;
                }
            }
            else
            {
                Server.ClearError();
                Response.Redirect("WarrantyClaimLucasLabourCharges.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            btnReset.Enabled = true;
            BtnSubmit.Enabled = false;
            grvItemDetails.Visible = false;

            UpdpanelTop.Update();
            UpdPanelGrid.Update();
        }

        protected void ddlWarrantyClaimNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlWarrantyClaimNumber.SelectedValue != "0")
                {
                    StockTransferTransactions stocktransferTransactions = new StockTransferTransactions();
                    WarrantyClaimLucasEntity warrantyclaimLucas = stocktransferTransactions.GetWarrantyClaimLucasDetails(ddlBranch.SelectedValue, ddlWarrantyClaimNumber.SelectedValue.ToString());

                    ddlBranch.SelectedValue = warrantyclaimLucas.BranchCode;
                    txtWarrantyClaimDate.Text = warrantyclaimLucas.WarrantyClaimDate;
                    ddlSupplierName.SelectedValue = warrantyclaimLucas.SupplierCode;

                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    ddlSupplyPlant.SelectedValue = warrantyclaimLucas.SupplierPlantCode;

                    txtShipTo.Text = warrantyclaimLucas.ShipTo;
                    txtAddress.Text = warrantyclaimLucas.Address;
                    txtGSTIN.Text = warrantyclaimLucas.GSTINNumber;

                    BtnReport.Visible = false;
                    ddlSupplierName.Enabled = false;
                    ddlSupplyPlant.Enabled = false;
                    txtShipTo.Enabled = false;
                    BindExistingRowsDuringEdit(warrantyclaimLucas.Items);

                    ddlSupplyPlant_OnSelectedIndexChanged(ddlSupplyPlant, EventArgs.Empty);
                }
                else
                {
                    pnlStatus.Visible = false;
                    txtWarrantyClaimDate.Text = "";
                    ddlTransactionType.SelectedValue = "0";
                }

                BtnReport.Visible = false;
                btnReset.Enabled = true;
                BtnSubmit.Enabled = true;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.SelectedValue == "Inactive" && txtLabourChargesDate.Text == DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    StockTransferTransactions tran = new StockTransferTransactions();
                    DataSet ds = tran.EditWarrantyClaimLabourHandlingEntry(ddlBranch.SelectedValue ,ddlLabourChargesNumber.SelectedValue, "I", "I", "Wrong Entry");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string JSONData = "{\"supplier_gstin\":\"" + ds.Tables[0].Rows[0]["GSTIN"] + "\",\"doc_no\":\"" + ds.Tables[0].Rows[0]["Document_Number"] + "\"," + "\"irn_no\":\"" + ds.Tables[0].Rows[0]["IRN_Number"] + "\"," +
                                          "\"doc_date\":\"" + ds.Tables[0].Rows[0]["Document_Date"] + "\",\"reason\":\"" + ds.Tables[0].Rows[0]["Reason"] + "\",\"remark\":\"" + ds.Tables[0].Rows[0]["Remarks"] + "\"}";

                        einvGen.EinvoiceAuthentication(JSONData, ddlBranch.SelectedValue.ToString(), ddlLabourChargesNumber.SelectedItem.Text.ToString(), "2", "CANIRN", ds.Tables[0].Rows[0]["GSTIN"].ToString(), ds.Tables[0].Rows[0]["Document_Type"].ToString(), ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), ds);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Warranty Claim Labour Charges Number has been Inactivated Sucessfully');", true);
                    ddlLabourChargesNumber.Enabled = false;
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
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
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
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }
        private void SubmitHeaderAndItems()
        {
            try
            {
                BtnReport.Visible = false;
                WarrantyClaimLucasEntity warrantyclaimEntity = new WarrantyClaimLucasEntity();
                warrantyclaimEntity.Items = new List<WarrantyClaimLucasItem>();

                warrantyclaimEntity.BranchCode = ddlBranch.SelectedValue.ToString();

                if (ddlLabourChargesNumber.Visible)
                    warrantyclaimEntity.LabourHandlingNumber = ddlLabourChargesNumber.SelectedValue.ToString();
                else
                    warrantyclaimEntity.LabourHandlingNumber = txtLabourChargesNumber.Text.ToString();

                warrantyclaimEntity.LabourHandlingDate = txtLabourChargesDate.Text.ToString();
                warrantyclaimEntity.WarrantyClaimNumber = ddlWarrantyClaimNumber.SelectedValue;
                warrantyclaimEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                warrantyclaimEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
                warrantyclaimEntity.SupplierPlantCode = ddlSupplyPlant.SelectedValue.ToString();
                warrantyclaimEntity.LabourHandlingCharges = txtLabourCharges.Text;
                warrantyclaimEntity.RefDocNumber = txtRefDocumentNumber.Text.ToString();
                warrantyclaimEntity.RefDocDate = txtRefDocumentDate.Text.ToString();
                warrantyclaimEntity.JobCardNumber = txtJobCardNumber.Text.ToString();
                warrantyclaimEntity.JobCardDate = txtJobCardDate.Text.ToString();
                warrantyclaimEntity.OSLSindicator = hdnOSLSindicator.Value;
                warrantyclaimEntity.ShipTo = txtShipTo.Text;

                StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                if (hdnScreenMode.Value == "A")
                {
                    stockTransferTransactions.AddNewWarrantyClaimLucasHandlingLabourCharges(ref warrantyclaimEntity, ddlStatus.SelectedValue, ddlTransactionType.SelectedValue);
                    if ((warrantyclaimEntity.ErrorMsg == string.Empty) && (warrantyclaimEntity.ErrorCode == "0"))
                    {
                        txtLabourChargesNumber.Text = warrantyclaimEntity.LabourHandlingNumber;

                        DataSet Datasetresult = stockTransferTransactions.GetEinvoicingDetailsWarrantyClaimLucasHandlingLabourCharges(ddlBranch.SelectedValue.ToString(), warrantyclaimEntity.LabourHandlingNumber);

                        GenerateJSON objGenJsonData = new GenerateJSON();

                        string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                        einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, warrantyclaimEntity.LabourHandlingNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                        FreezeOrUnFreezeButtons(false);
                        btnReset.Enabled = true;
                        BtnReport.Visible = true;

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Labour Charges Entry Has been Generated Successfully with QR Code.');", true);
                        BtnReport.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + warrantyclaimEntity.ErrorMsg + "');", true);
                        BtnReport.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }

        private void BindExistingRowsDuringEdit(List<WarrantyClaimLucasItem> lstWarrantyClaimItem)
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
                for (int i = 0; i < lstWarrantyClaimItem.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["SNo"] = lstWarrantyClaimItem[i].SNo.ToString();
                    dr["Col1"] = lstWarrantyClaimItem[i].SupplierPartNo.ToString();
                    dr["Col2"] = lstWarrantyClaimItem[i].ItemCode.ToString();
                    dr["Col3"] = lstWarrantyClaimItem[i].Quantity.ToString();
                    dr["Col4"] = lstWarrantyClaimItem[i].AvailableQuantity.ToString();
                    dr["Col5"] = lstWarrantyClaimItem[i].CostPricePerQuantity.ToString();
                    dr["Col6"] = lstWarrantyClaimItem[i].CostPrice.ToString();
                    dr["Col7"] = lstWarrantyClaimItem[i].GSTPercentage.ToString();
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
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
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
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemCode");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtQuantity");
                    TextBox txtAvailableQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAvailableQuantity");
                    TextBox txtCstPricePerQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCstPricePerQty");
                    TextBox txtCostPrice = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtCostPrice");

                    if ((hdnScreenMode.Value == "E") || (hdnScreenMode.Value == "A"))
                    {
                        if (rowIndex != grvItemDetails.Rows.Count - 1)
                        {
                            txtSupplierPartNo.Enabled = false;
                            txtQuantity.Enabled = false;
                        }
                        else
                        {
                            txtSupplierPartNo.Enabled = true;
                            txtQuantity.Enabled = true;

                            txtQuantity.Attributes.Add("OnChange", "return funSTQuantityValidation('" + txtAvailableQuantity.ClientID + "','" + txtQuantity.ClientID + "','" + txtCstPricePerQty.ClientID + "','" + txtCostPrice.ClientID + "');");
                        }
                    }
                }

                txtHdnGridCtrls.Text = sb.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
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
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
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
                btnReset.Enabled = Fzflag;
                BtnSubmit.Enabled = Fzflag;
                imgEditToggle.Enabled = Fzflag;
                pnlSTEntry.Enabled = Fzflag;
                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
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
                Log.WriteException(typeof(WarrantyClaimLucasLabourCharges), exp);
            }
        }
    }
}