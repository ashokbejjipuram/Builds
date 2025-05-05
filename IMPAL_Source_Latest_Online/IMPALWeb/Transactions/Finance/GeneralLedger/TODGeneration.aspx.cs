using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IMPALLibrary;
using IMPALLibrary.Transactions;
using IMPALLibrary.Masters;
using IMPALWeb.UserControls;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions.Finance;
using System.Drawing;
using IMPALLibrary.Masters.Sales;
using Newtonsoft.Json;

namespace IMPALWeb
{
    public partial class TODGeneration : System.Web.UI.Page
    {
        Suppliers objSupplier = new Suppliers();
        DebitCredit objDebitCredit = new DebitCredit();
        public string IGSTIndicator = "0";
        ReceivableInvoice receivableReceipt = new ReceivableInvoice();
        ReceiptTransactions receiptTransactions = new ReceiptTransactions();
        ImpalLibrary oCommon = new ImpalLibrary();
        CashDiscountDts objCashDiscount = new CashDiscountDts();
        EinvAuthGen einvGen = new EinvAuthGen();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdnScreenMode.Value = "A";

                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;

                    ChkStatus.Value = "0";
                    SetDefaultValues();
                    ddlDocumentNumber.Visible = false;
                    ddlAccountingPeriod.Visible = true;
                    txtAccountPeriod.Visible = false;
                    //FirstGridViewRow(string.Empty);
                    FreezeButtons(true);

                    PnlDateRangeOthers.Visible = false;
                    PnlDateRangeGogo.Visible = false;

                    BtnSubmit.Visible = false;
                    btnReset.Enabled = true;
                    BtnReport.Enabled = true;

                    grvItemDetails.Visible = false;

                    LoadDropDownLists<IMPALLibrary.Supplier>(objSupplier.GetSupplierForTODGeneration(Session["BranchCode"].ToString()), ddlSupplier, "SupplierCode", "SupplierName", true, "-- Select --");
                }

                BtnGetDocuments.Attributes.Add("OnClick", "return funTODGenerationHeaderValidation();");
                BtnSubmit.Attributes.Add("OnClick", "return funTODGenerationSubmit();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
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
                //if (grvItemDetails.Rows.Count > 1)
                //{
                //    if (Session["BranchCode"].ToString() == "CHG")
                //    {
                //        if (grvItemDetails.HeaderRow != null)
                //        {
                //            grvItemDetails.HeaderRow.Cells[6].Text = "UTGST %";
                //            grvItemDetails.HeaderRow.Cells[7].Text = "UTGST Value";
                //        }
                //    }
                //    else
                //    {
                //        if (grvItemDetails.HeaderRow != null)
                //        {
                //            grvItemDetails.HeaderRow.Cells[6].Text = "SGST %";
                //            grvItemDetails.HeaderRow.Cells[7].Text = "SGST Value";
                //        }
                //    }

                //    if (IGSTIndicator == "0")
                //    {
                //        grvItemDetails.Columns[6].Visible = true;
                //        grvItemDetails.Columns[7].Visible = true;
                //        grvItemDetails.Columns[8].Visible = true;
                //        grvItemDetails.Columns[9].Visible = true;
                //        grvItemDetails.Columns[10].Visible = false;
                //        grvItemDetails.Columns[11].Visible = false;
                //    }
                //    else
                //    {
                //        grvItemDetails.Columns[6].Visible = false;
                //        grvItemDetails.Columns[7].Visible = false;
                //        grvItemDetails.Columns[8].Visible = false;
                //        grvItemDetails.Columns[9].Visible = false;
                //        grvItemDetails.Columns[10].Visible = true;
                //        grvItemDetails.Columns[11].Visible = true;
                //    }
                //}
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ChkSelected_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int NoOfRowsChecked = 0;

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkbox = (CheckBox)gvr.FindControl("ChkSelected");

                        if (chkbox.Checked)
                        {
                            NoOfRowsChecked += 1;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlDocumentNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnScreenMode.Value = "E";

                ReceiptEntity receiptEntity = receiptTransactions.GetReceiptsDetailsByNumber(Session["BranchCode"].ToString(), ddlDocumentNumber.SelectedValue, "A");

                ddlDocumentNumber.SelectedValue = receiptEntity.ReceiptNumber;
                txtDocumentNumber.Text = receiptEntity.ReceiptNumber;

                txtDocumentDate.Text = receiptEntity.ReceiptDate;
                txtAccountPeriod.Text = receiptEntity.AccountingPeriodDesc;
                //ddlAccountingPeriod.SelectedValue = receiptEntity.AccountingPeriod;
                ddlCustomer.SelectedValue = receiptEntity.CustomerCode;

                txtCode.Text = receiptEntity.CustomerCode;
                txtLocation.Text = receiptEntity.Location;
                txtAddress1.Text = receiptEntity.Address1;
                txtAddress2.Text = receiptEntity.Address2;
                txtAddress3.Text = receiptEntity.Address3;
                txtAddress4.Text = receiptEntity.Address4;

                txtFromDate.Text = receiptEntity.FromDate;
                txtToDate.Text = receiptEntity.ToDate;

                //BindExistingRows(receiptEntity.Items);

                grvItemDetails.DataSource = (object)receiptEntity.Items;
                grvItemDetails.DataBind();
                grvItemDetails.Columns[0].Visible = false;

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    gr.Enabled = false;
                }

                ddlAccountingPeriod.Enabled = false;
                ddlSupplier.Enabled = false;
                ddlCustomer.Enabled = false;
                ddlBranch.Enabled = false;
                BtnSubmit.Enabled = false;
                btnReset.Enabled = true;
                BtnReport.Enabled = true;

                BtnReport.Enabled = true;

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "E";
                    ddlDocumentNumber.SelectedValue = "0";
                    ddlDocumentNumber.Visible = true;
                    txtDocumentNumber.Visible = false;

                    ddlBranch.Enabled = false;
                    BtnReport.Enabled = false;
                    BtnSubmit.Enabled = false;

                    FirstGridViewRow(string.Empty);
                    txtDocumentNumber.Text = string.Empty;
                    txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlCustomer.SelectedValue = "0";
                    txtAccountPeriod.Visible = true;
                    ddlAccountingPeriod.Visible = false;

                    txtCode.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                    txtAddress3.Text = string.Empty;
                    txtAddress4.Text = string.Empty;

                    BtnSubmit.Attributes.Add("OnClick", "return funFYRcvReceiptSubmit();");

                    imgEditToggle.Visible = false;
                }
                else if (hdnScreenMode.Value == "E")
                {
                    hdnScreenMode.Value = "A";
                    ddlDocumentNumber.Visible = false;
                    txtDocumentNumber.Visible = true;

                    ddlBranch.Enabled = true;
                    ddlAccountingPeriod.Visible = true;
                    ddlAccountingPeriod.Enabled = true;
                    txtAccountPeriod.Visible = false;
                    ddlCustomer.Enabled = true;
                    ddlSupplier.Enabled = true;
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    BtnReport.Enabled = false;

                    FirstGridViewRow(string.Empty);
                    txtDocumentNumber.Text = string.Empty;
                    txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    //ddlAccountingPeriod.SelectedValue = "0";
                    ddlCustomer.SelectedValue = "0";

                    txtCode.Text = string.Empty;
                    txtLocation.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                    txtAddress3.Text = string.Empty;
                    txtAddress4.Text = string.Empty;

                    BtnSubmit.Attributes.Add("OnClick", "return funFYRcvReceiptSubmit();");

                    FreezeButtons(true);
                }

                UpdpanelTop.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDocumentNumber.SelectedValue != "" && ddlDocumentNumber.SelectedValue != null)
                    Session["ReceiptNumber"] = ddlDocumentNumber.SelectedValue;
                else
                    Session["ReceiptNumber"] = txtDocumentNumber.Text;

                Session["ReceiptDate"] = txtDocumentDate.Text;
                Server.ClearError();
                Response.Redirect("ReceiptsReport.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Enabled = false;
                SubmitHeaderAndItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void BtnGetDocuments_Click(object sender, EventArgs e)
        {
            try
            {                
                BtnGetDocuments.Visible = false;

                List<TODGenerationItem> docDetails = new List<TODGenerationItem>();

                if (ddlSupplier.SelectedValue == "980")
                    docDetails = receiptTransactions.GetTODGenerationDocumentDetailsGOGO(Session["BranchCode"].ToString(), ddlAccountingPeriod.SelectedValue, ddlTODMonthsGOGO.SelectedValue, ddlCustomer.SelectedValue, ddlSupplier.SelectedValue, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue, ddlSLBPercentage.SelectedValue, ddlBeforeAfterCD.SelectedValue);
                else
                    docDetails = receiptTransactions.GetTODGenerationDocumentDetails(Session["BranchCode"].ToString(), ddlAccountingPeriod.SelectedValue, txtFromDate.Text, txtToDate.Text, ddlCustomer.SelectedValue, ddlSupplier.SelectedValue, ddlSLBValue.SelectedValue, ddlSLBPercentage.SelectedValue, ddlSLBType.SelectedValue, ddlTODtype.SelectedValue);

                grvItemDetails.Visible = true;
                ddlSLBPercentage.Enabled = false;

                if (docDetails.Count > 0)
                {
                    grvItemDetails.Enabled = true;
                    hdnRowCnt.Value = docDetails.Count.ToString();
                    IGSTIndicator = docDetails[0].IGSTInd;
                    hdnTODNumber.Value = docDetails[0].TODNumber;

                    grvItemDetails.DataSource = docDetails;
                    grvItemDetails.DataBind();

                    grvItemDetails.HeaderRow.Visible = true;
                    grvItemDetails.FooterRow.Visible = true;
                    BtnSubmit.Visible = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "EnableAllCheckboxes('ctl00_CPHDetails_grvItemDetails_ctl01_chkSelectAll');", true);
                }
                else
                {
                    grvItemDetails.Enabled = false;
                    BtnSubmit.Visible = false;
                    FirstGridViewRow("No Records Found.");
                    grvItemDetails.FooterRow.Visible = false;
                }

                ChkStatus.Value = docDetails.Count.ToString();

                ddlAccountingPeriod.Attributes.Add("disabled", "True");
                ddlCustomer.Attributes.Add("disabled", "True");
                ddlSupplier.Attributes.Add("disabled", "True");
                ddlSupplyPlant.Attributes.Add("disabled", "True");
                ddlBranch.Attributes.Add("disabled", "True");
                ddlSLBType.Attributes.Add("disabled", "True");
                ddlSLBValue.Attributes.Add("disabled", "True");
                ddlTODtype.Attributes.Add("disabled", "True");
                ddlTODMonthsGOGO.Attributes.Add("disabled", "True");
                ddlBeforeAfterCD.Attributes.Add("disabled", "True");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                //if (hdnTODNumber.Value != "")
                //{
                //    receiptTransactions.ResetTODdetails(Session["BranchCode"].ToString(), ddlCustomer.SelectedValue, ddlSupplier.SelectedValue, hdnTODNumber.Value);
                //}

                Server.ClearError();
                Response.Redirect("TODGeneration.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlCustomer_OnDataBound(object sender, EventArgs e)
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
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlSupplier.SelectedValue == "980")
                {
                    ddlSupplyPlant.DataSource = (object)GetSupplyplants();
                    ddlSupplyPlant.DataTextField = "ItemDesc";
                    ddlSupplyPlant.DataValueField = "ItemCode";
                    ddlSupplyPlant.DataBind();

                    PnlDateRangeOthers.Visible = false;
                    PnlDateRangeGogo.Visible = true;
                    Label6.Text = "Customer Location";
                    Label8.Text = "TOD Target Type";

                    if (ddlBranch.SelectedValue == "MGT")
                    {
                        ddlMarketLocation.Items.Clear();
                        ddlMarketLocation.DataBind();
                        ddlMarketLocation.DataSource = oCommon.GetDropDownListValues("TODMarketTypeSpecialGOGO");
                        ddlMarketLocation.DataTextField = "DisplayText";
                        ddlMarketLocation.DataValueField = "DisplayValue";
                        ddlMarketLocation.DataBind();
                        ddlMarketLocation.Enabled = true;
                    }
                    else
                    {
                        ddlMarketLocation.Items.Clear();
                        ddlMarketLocation.DataBind();
                        ddlMarketLocation.DataSource = oCommon.GetDropDownListValues("TODMarketTypeRegularGOGO");
                        ddlMarketLocation.DataTextField = "DisplayText";
                        ddlMarketLocation.DataValueField = "DisplayValue";
                        ddlMarketLocation.DataBind();
                        ddlMarketLocation.Enabled = false;
                    }

                    ddlSLBType.Items.Clear();
                    ddlSLBType.DataBind();
                    ddlSLBType.DataSource = oCommon.GetDropDownListValues("TODSLBTypeGOGO");
                    ddlSLBType.DataTextField = "DisplayText";
                    ddlSLBType.DataValueField = "DisplayValue";
                    ddlSLBType.DataBind();

                    ddlSLBValue.Items.Clear();
                    ddlSLBValue.DataBind();
                    ddlSLBValue.DataSource = oCommon.GetDropDownListValues("TODCustTypeGOGO");
                    ddlSLBValue.DataTextField = "DisplayText";
                    ddlSLBValue.DataValueField = "DisplayValue";
                    ddlSLBValue.DataBind();
                    ddlSLBValue.AutoPostBack = false;
                }
                else
                {
                    PnlDateRangeOthers.Visible = false;
                    PnlDateRangeGogo.Visible = false;
                    Label8.Text = "SLB Type";
                    Label6.Text = "SLB Value";
                    ddlSLBType.Items.Clear();
                    ddlSLBType.DataBind();
                    ddlSLBType.DataSource = oCommon.GetDropDownListValues("TODSLBType");
                    ddlSLBType.DataTextField = "DisplayText";
                    ddlSLBType.DataValueField = "DisplayValue";
                    ddlSLBType.DataBind();
                    ddlSLBValue.AutoPostBack = true;

                    ddlSLBValue.Items.Clear();
                    ddlSLBValue.DataBind();
                }
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private List<IMPALLibrary.Transactions.Item> GetSupplyplants()
        {
            List<IMPALLibrary.Transactions.Item> obj = objDebitCredit.GetSupplierPlant(ddlSupplier.SelectedValue.ToString());
            return obj;
        }

        protected void ddlAccountingPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlAccountingPeriod.Enabled = false;

                if (ddlAccountingPeriod.SelectedIndex == 0)
                    txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                else
                    txtDocumentDate.Text = objDebitCredit.GetDocumentDate(ddlAccountingPeriod.SelectedValue);

                LoadhdnStartEndDates(ddlAccountingPeriod.SelectedValue);

                //FirstGridViewRow(string.Empty);
                //FreezeButtons(true);
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.SelectedValue == "0")
                    return;

                Customer customer = receivableReceipt.GetCustomerInfoByCustomerCode(ddlCustomer.SelectedValue, Session["BranchCode"].ToString());
                txtCode.Text = customer.Customer_Code;
                txtLocation.Text = customer.Location;
                txtAddress1.Text = customer.address1;
                txtAddress2.Text = customer.address2;
                txtAddress3.Text = customer.address3;
                txtAddress4.Text = customer.address4;
                txtGSTINNo.Text = customer.GSTIN;

                if (ddlSupplier.SelectedValue == "980" && ddlTODMonthsGOGO.SelectedIndex > 0 && ddlBeforeAfterCD.SelectedIndex > 0)
                {
                    ddlSLBPercentage.DataSource = receivableReceipt.GetTODSLBPercentageGOGO(ddlSupplier.SelectedValue, ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlMarketLocation.SelectedValue, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue, ddlTODMonthsGOGO.SelectedValue, ddlBeforeAfterCD.SelectedValue);
                    ddlSLBPercentage.DataTextField = "Description";
                    ddlSLBPercentage.DataValueField = "SLBCode";
                    ddlSLBPercentage.DataBind();
                }
                else
                {
                    ddlSLBPercentage.Items.Clear();
                    ddlSLBPercentage.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlBeforeAfterCD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.SelectedValue == "0")
                    return;

                if (ddlSupplier.SelectedValue == "980" && ddlTODMonthsGOGO.SelectedIndex > 0 && ddlBeforeAfterCD.SelectedIndex > 0)
                {
                    ddlSLBPercentage.DataSource = receivableReceipt.GetTODSLBPercentageGOGO(ddlSupplier.SelectedValue, ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlMarketLocation.SelectedValue, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue, ddlTODMonthsGOGO.SelectedValue, ddlBeforeAfterCD.SelectedValue);
                    ddlSLBPercentage.DataTextField = "Description";
                    ddlSLBPercentage.DataValueField = "SLBCode";
                    ddlSLBPercentage.DataBind();
                }
                else
                {
                    ddlSLBPercentage.Items.Clear();
                    ddlSLBPercentage.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlTODMonthsGOGO_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCustomer.SelectedValue == "0")
                    return;

                if (ddlSupplier.SelectedValue == "980" && ddlTODMonthsGOGO.SelectedIndex > 0 && ddlBeforeAfterCD.SelectedIndex > 0)
                {
                    ddlSLBPercentage.DataSource = receivableReceipt.GetTODSLBPercentageGOGO(ddlSupplier.SelectedValue, ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlMarketLocation.SelectedValue, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue, ddlTODMonthsGOGO.SelectedValue, ddlBeforeAfterCD.SelectedValue);
                    ddlSLBPercentage.DataTextField = "Description";
                    ddlSLBPercentage.DataValueField = "SLBCode";
                    ddlSLBPercentage.DataBind();
                }
                else
                {
                    ddlSLBPercentage.Items.Clear();
                    ddlSLBPercentage.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }        

        protected void ddlMarketLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (ddlMarketLocation.SelectedValue == "0")
                        return;

                    if (ddlSupplier.SelectedValue == "980" && ddlTODMonthsGOGO.SelectedIndex > 0)
                    {
                        ddlSLBPercentage.DataSource = receivableReceipt.GetTODSLBPercentageGOGO(ddlSupplier.SelectedValue, ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlMarketLocation.SelectedValue, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue, ddlTODMonthsGOGO.SelectedValue, ddlBeforeAfterCD.SelectedValue);
                        ddlSLBPercentage.DataTextField = "Description";
                        ddlSLBPercentage.DataValueField = "SLBCode";
                        ddlSLBPercentage.DataBind();
                    }
                    else
                    {
                        ddlSLBPercentage.Items.Clear();
                        ddlSLBPercentage.DataBind();
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException(typeof(TODGeneration), exp);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlSLBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSLBType.SelectedValue == "")
                {
                    ddlSLBValue.Items.Clear();
                    ddlSLBValue.DataBind();
                }
                else
                {                    
                    if (ddlSupplier.SelectedValue == "980")
                    {
                        ddlTODMonthsGOGO.DataSource = receivableReceipt.GetTODMonthYearGOGO(ddlBranch.SelectedValue);
                        ddlTODMonthsGOGO.DataValueField = "month_year";
                        ddlTODMonthsGOGO.DataTextField = "month_year";
                        ddlTODMonthsGOGO.DataBind();
                        ddlTODMonthsGOGO.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    else
                    {
                        ddlSLBValue.DataSource = receivableReceipt.GetTODSLBDetails(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlSupplier.SelectedValue, ddlSLBType.SelectedValue);
                        ddlSLBValue.DataTextField = "Description";
                        ddlSLBValue.DataValueField = "SLBCode";
                        ddlSLBValue.DataBind();
                        ddlSLBValue.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        protected void ddlSLBValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSLBValue.SelectedValue == "")
                {
                    ddlSLBPercentage.Items.Clear();
                    ddlSLBPercentage.DataBind();
                }
                else
                {
                    if (ddlSupplier.SelectedValue != "980")
                    {
                        ddlSLBPercentage.DataSource = receivableReceipt.GetTODSLBPercentage(ddlBranch.SelectedValue, ddlCustomer.SelectedValue, ddlSupplier.SelectedValue, ddlSupplier.SelectedItem.Text, ddlSLBType.SelectedValue, ddlSLBValue.SelectedValue);
                        ddlSLBPercentage.DataTextField = "Description";
                        ddlSLBPercentage.DataValueField = "SLBCode";
                        ddlSLBPercentage.DataBind();
                    }
                }

                ddlSLBPercentage.Items.Insert(0, (new ListItem("--Select--", "")));

                if (ddlSLBPercentage.Items.Count > 1)
                    ddlSLBPercentage.Enabled = true;
                else
                    ddlSLBPercentage.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(TODGeneration), exp);
            }
        }

        private void SetDefaultValues()
        {
            txtDocumentNumber.Text = string.Empty;
            ddlDocumentNumber.SelectedValue = "0";
            txtDocumentDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ddlCustomer.SelectedValue = "0";

            txtCode.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtAddress4.Text = string.Empty;

            LoadAccountintPeriod();

            txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            List<string> lstAccountingPeriod = new List<string>();

            int PrevFinYearStatus = receivableReceipt.GetPreviousAccountingPeriodStatus(Session["UserID"].ToString(), "TODGeneration");

            if (PrevFinYearStatus > 0)
            {
                lstAccountingPeriod.Add((DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString());
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(accountingPeriod);
            txtDocumentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            List<AccountingPeriod> AccordingPeriod = receivableReceipt.GetAccountingPeriod();
            List<AccountingPeriod> FinYear = AccordingPeriod.Where(p => lstAccountingPeriod.Contains(p.Desc)).OrderByDescending(c => c.AccPeriodCode).ToList();
            LoadDropDownLists<AccountingPeriod>(FinYear, ddlAccountingPeriod, "AccPeriodCode", "Desc", false, "");

            LoadhdnStartEndDates(ddlAccountingPeriod.SelectedValue);
        }

        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;

            return FinYear.Trim();
        }

        private void LoadhdnStartEndDates(string AccPeriod)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DebitCredit objDebitCredit = new DebitCredit();

                List<CDAccStartEndDate> cdstartenddates = objCashDiscount.AccountingStartEnddate(AccPeriod);
                hdnStartdate.Value = cdstartenddates[0].Start_Date.ToString();
                hdnEnddate.Value = cdstartenddates[0].End_Date.ToString();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        private void FreezeButtons(bool Fzflag)
        {
            DivOuter.Disabled = !Fzflag;
            imgEditToggle.Enabled = Fzflag;
            BtnGetDocuments.Enabled = Fzflag;
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

        private bool ChkCurrencyDots(string StrCurrValue)
        {
            int count = StrCurrValue.Split('.').Length - 1;
            if (count > 1)
                return false;
            else
                return true;
        }

        private void FirstGridViewRow(string strNoRowsFoundMsg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceValue", typeof(string)));
            dt.Columns.Add(new DataColumn("ListValue", typeof(string)));
            dt.Columns.Add(new DataColumn("TODValue", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTPer", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTVal", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTPer", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTVal", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTPer", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTVal", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemTotalTODValue", typeof(string)));
            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["InvoiceNumber"] = string.Empty;
                dr["InvoiceDate"] = string.Empty;
                dr["InvoiceValue"] = string.Empty;
                dr["ListValue"] = string.Empty;
                dr["TODValue"] = string.Empty;
                dr["SGSTPer"] = string.Empty;
                dr["SGSTVal"] = string.Empty;
                dr["CGSTPer"] = string.Empty;
                dr["CGSTVal"] = string.Empty;
                dr["IGSTPer"] = string.Empty;
                dr["IGSTVal"] = string.Empty;
                dr["ItemTotalTODValue"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            grvItemDetails.Rows[0].Cells.Clear();
            grvItemDetails.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 12;
            grvItemDetails.Rows[0].Cells[0].Text = strNoRowsFoundMsg;
            grvItemDetails.Rows[0].Cells[0].CssClass = "EmptyRowStyle";
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        private void SubmitHeaderAndItems()
        {
            string DocumentNumber = string.Empty;
            BtnSubmit.Visible = false;

            TODGenerationEntity todGenerationEntity = new TODGenerationEntity();
            todGenerationEntity.Items = new List<TODGenerationItem>();

            todGenerationEntity.BranchCode = ddlBranch.SelectedValue;
            todGenerationEntity.AccountingPeriod = ddlAccountingPeriod.SelectedValue;
            todGenerationEntity.DocumentNumber = txtDocumentNumber.Text;
            todGenerationEntity.DocumentDate = txtDocumentDate.Text;
            todGenerationEntity.CustomerCode = ddlCustomer.SelectedValue;
            todGenerationEntity.CustomerName = ddlCustomer.SelectedItem.Text;
            todGenerationEntity.SupplierCode = ddlSupplier.SelectedValue;
            todGenerationEntity.SupplyPlant = ddlSupplyPlant.SelectedValue;
            todGenerationEntity.SLBValue = ddlSLBValue.SelectedValue;
            todGenerationEntity.SLBPercentage = ddlSLBPercentage.SelectedValue;
            todGenerationEntity.TODType = ddlTODtype.SelectedValue;
            todGenerationEntity.TODNumber = hdnTODNumber.Value;

            TextBox txtTotalTODValue = (TextBox)grvItemDetails.FooterRow.FindControl("txtTotalTODValue");
            todGenerationEntity.TotalTODValue = txtTotalTODValue.Text.ToString();

            TODGenerationItem todGenerationItem = null;
            int SNo = 0;

            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                CheckBox ChkSelected = (CheckBox)gr.FindControl("ChkSelected");

                if (!ChkSelected.Checked)
                    continue;

                todGenerationItem = new TODGenerationItem();
                SNo += 1;

                TextBox txtInvoiceNumber = (TextBox)gr.FindControl("txtInvoiceNumber");
                TextBox txtInvoiceDate = (TextBox)gr.FindControl("txtInvoiceDate");
                TextBox txtInvoiceValue = (TextBox)gr.FindControl("txtInvoiceValue");
                TextBox txtListValue = (TextBox)gr.FindControl("txtListValue");
                TextBox txtTODValue = (TextBox)gr.FindControl("txtTODValue");
                TextBox txtSGSTPercentage = (TextBox)gr.FindControl("txtSGSTPercentage");
                TextBox txtSGSTValue = (TextBox)gr.FindControl("txtSGSTValue");
                TextBox txtCGSTPercentage = (TextBox)gr.FindControl("txtCGSTPercentage");
                TextBox txtCGSTValue = (TextBox)gr.FindControl("txtCGSTValue");
                TextBox txtIGSTPercentage = (TextBox)gr.FindControl("txtIGSTPercentage");
                TextBox txtIGSTValue = (TextBox)gr.FindControl("txtIGSTValue");
                TextBox txtItemTotalTODValue = (TextBox)gr.FindControl("txtItemTotalTODValue");

                todGenerationItem.SNO = SNo.ToString();
                todGenerationItem.InvoiceNumber = txtInvoiceNumber.Text.Trim();
                todGenerationItem.InvoiceDate = txtInvoiceDate.Text.Trim();
                todGenerationItem.InvoiceValue = txtInvoiceValue.Text.ToString();
                todGenerationItem.ListValue = txtListValue.Text.ToString();
                todGenerationItem.TODValue = txtTODValue.Text.ToString();
                todGenerationItem.SGSTPer = txtSGSTPercentage.Text.ToString();
                todGenerationItem.SGSTVal = txtSGSTValue.Text.ToString();
                todGenerationItem.CGSTPer = txtCGSTPercentage.Text.ToString();
                todGenerationItem.CGSTVal = txtCGSTValue.Text.ToString();
                todGenerationItem.IGSTPer = txtIGSTPercentage.Text.ToString();
                todGenerationItem.IGSTVal = txtIGSTValue.Text.ToString();
                todGenerationItem.ItemTotalTODValue = txtItemTotalTODValue.Text.ToString();

                todGenerationEntity.Items.Add(todGenerationItem);
            }

            DocumentNumber = objDebitCredit.AddCreditNoteTOD(ref todGenerationEntity, ddlTODMonthsGOGO.SelectedValue);

            txtDocumentNumber.Text = DocumentNumber;
            grvItemDetails.Enabled = false;
            BtnGetDocuments.Enabled = false;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            ddlSLBType.Enabled = false;
            ddlSLBValue.Enabled = false;
            ddlSLBPercentage.Enabled = false;
            ddlBeforeAfterCD.Enabled = false;

            if (ddlTODtype.SelectedValue == "Y" && txtGSTINNo.Text.Substring(0, 2).ToUpper() != "UN")
            {
                DataSet Datasetresult = new DataSet();

                Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrCust(Session["BranchCode"].ToString(), DocumentNumber);

                GenerateJSON objGenJsonData = new GenerateJSON();

                string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, Session["BranchCode"].ToString()), Formatting.Indented);

                einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), Session["BranchCode"].ToString(), DocumentNumber, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('TOD Debit Credit Note Entry Has been Generated Successfully with QR Code.');", true);
            }
            else if (ddlTODtype.SelectedValue == "Y" && txtGSTINNo.Text.Substring(0, 2).ToUpper() == "UN")
            {
                DataSet Datasetresult = new DataSet();

                Datasetresult = objDebitCredit.GetEinvoicingDetailsDrCrCust(Session["BranchCode"].ToString(), DocumentNumber);

                GenerateJSON objGenJsonData = new GenerateJSON();

                einvGen.EinvoiceAuthenticationB2C(objGenJsonData.GenerateInvoiceJSONDataB2C(Datasetresult, ddlBranch.SelectedValue), ddlBranch.SelectedValue, DocumentNumber, "1", "NOIRN");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Has been Generated Successfully with B2C QR Code. Please Click On Report Button To Take the Print');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('TOD Credit Note Entry Has been Generated Successfully without QR Code..');", true);
        }
    }
}
