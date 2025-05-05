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
using IMPALLibrary.Common;
using Newtonsoft.Json;

namespace IMPALWeb
{
    public partial class GRNFinalEntry : System.Web.UI.Page
    {
        //private string ScreenMode = "A";//"A--Add Mode,E--Edit Mode,V--View Mode"

        EinvAuthGen einvGen = new EinvAuthGen();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
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

                    hdnScreenMode.Value = "E";
                    hdnInvoiceNo.Value = "";
                  
                    SetDefaultValues();
                    FirstGridViewRow();
                    FreezeButtons(true);

                    BtnSubmit.Enabled = false;
                    BtnReject.Enabled = false;
                }

                BtnSubmit.Attributes.Add("OnClick", "return funGrnSubmit()");
                BtnReject.Attributes.Add("OnClick", "return funGrnReject();");
                btnReset.Attributes.Add("OnClick", "return funGrnReset();");
                txtHdnGridCtrls.Attributes.Add("Style", "display:none");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }
                
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Enabled = false;
                GRNEntity grnEntity = new GRNEntity();
                grnEntity.Items = new List<GRNItem>();
                GRNItem grnItem = null;

                grnEntity.BranchCode = ddlBranch.SelectedValue.ToString();
                grnEntity.InwardNumber = ddlInwardNumber.SelectedValue.ToString();
                grnEntity.InwardDate = txtInwardDate.Text.ToString();
                grnEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                grnEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
                grnEntity.PackageStatus = ddlPackageStatus.SelectedValue.ToString();
                grnEntity.PackageOpenDate = txtPackageOpenDate.Text.ToString();
                grnEntity.LRTransfer = ddlLRTransfer.SelectedValue.ToString();
                grnEntity.LocalPurchaseTax = txtPurchaseTax.Text.ToString();
                grnEntity.Remarks = txtRemarks.Text.ToString();
                grnEntity.InvoiceNumber = txtInvoiceNo.Text;
                grnEntity.InvoiceDate = txtInvoiceDate.Text;
                grnEntity.ClearingAgentNo = txtClearingAgentNo.Text.ToString();
                grnEntity.CheckPostName = txtCheckPostName.Text.ToString();
                grnEntity.ClearenceDate = txtClearenceDate.Text.ToString();
                grnEntity.ClearenceAmount = txtClearenceAmount.Text.ToString();
                grnEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
                grnEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();
                grnEntity.ApprovalLevel = Session["UserName"].ToString() + "/" + Session["UserID"];
                grnEntity.OSLSindicator = hdnOSLSvalue.Value;
                grnEntity.InwardValue = hdnInwardvalue.Value;
                grnEntity.HeaderTaxValue = hdnHdrTaxvalue.Value;
                grnEntity.InsValue = hdnInsvalue.Value;
                grnEntity.DiscValue = hdnDiscvalue.Value;

                InwardTransactions inwardTransactions = new InwardTransactions();
                string InvoiceNo = inwardTransactions.CheckInvoiceExistsEdit(ddlSupplierName.SelectedValue, txtInwardDate.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, Session["BranchCode"].ToString(), ddlInwardNumber.SelectedValue);

                if (InvoiceNo != "")
                {
                    txtInvoiceNo.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Invoice Number already exists...');", true);
                    return;
                }

                int CAgentCnt = 0;
                int ShortageQty = 0;
                GRNTransactions grnTransactions = new GRNTransactions();
                CAgentCnt = grnTransactions.ClearingAgentExists(ddlInwardNumber.SelectedValue.ToString(), txtClearingAgentNo.Text.ToString(), Session["BranchCode"].ToString());

                if (CAgentCnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Clearing Agent Number is already exists');", true);
                    return;
                }                

                foreach (GridViewRow gvr in grvItemDetails.Rows)
                {
                    grnItem = new GRNItem();

                    TextBox txtSNo = (TextBox)gvr.FindControl("txtSNo");
                    grnItem.SNo = txtSNo.Text;

                    TextBox txtSupplierPartNo = (TextBox)gvr.FindControl("txtSupplierPartNo");
                    grnItem.SupplierPartNo = txtSupplierPartNo.Text;

                    TextBox txtPOQuantity = (TextBox)gvr.FindControl("txtPOQuantity");
                    grnItem.POQty = txtPOQuantity.Text;

                    TextBox txtRcvdQty = (TextBox)gvr.FindControl("txtRcvdQty");
                    grnItem.ReceivedQty = txtRcvdQty.Text;

                    TextBox txtInwardQty = (TextBox)gvr.FindControl("txtInwardQty");
                    grnItem.InwardQty = txtInwardQty.Text;

                    TextBox txtAcceptedQty = (TextBox)gvr.FindControl("txtAcceptedQty");
                    grnItem.AcceptedQty = txtAcceptedQty.Text;

                    TextBox txtIndicator = (TextBox)gvr.FindControl("txtIndicator");
                    grnItem.Indicator = txtIndicator.Text.ToUpper() == "OS" ? "O" : "L";

                    TextBox txtShortage = (TextBox)gvr.FindControl("txtShortage");
                    grnItem.Shortage = txtShortage.Text;

                    if (txtShortage.Text == "")
                        txtShortage.Text = "0";

                    ShortageQty = ShortageQty + Convert.ToInt32(txtShortage.Text);

                    TextBox txtItemLocation = (TextBox)gvr.FindControl("txtItemLocation");
                    grnItem.ItemLocation = txtItemLocation.Text;

                    grnEntity.WarehouseNo = txtWareHouseNo.Text;
                    grnEntity.WarehouseDate = txtWarehouseDate.Text;

                    TextBox txtHandlingCharges = (TextBox)gvr.FindControl("txtHandlingCharges");
                    grnItem.HandlingCharges = txtHandlingCharges.Text;                    

                    grnEntity.Items.Add(grnItem);
                }

                grnTransactions.UpdateGRNFinalEntry(ref grnEntity);

                if ((grnEntity.ErrorMsg == string.Empty) && (grnEntity.ErrorCode == "0"))
                {
                    FreezeButtons(false);

                    foreach (GridViewRow gr in grvItemDetails.Rows)
                    {
                        gr.Enabled = false;
                    }

                    btnReset.Enabled = true;

                    if (ShortageQty > 0)
                    {
                        DataSet Datasetresult = grnTransactions.GetEinvoicingShortageDebitNoteDetails(ddlBranch.SelectedValue.ToString(), grnEntity.InwardNumber);

                        GenerateJSON objGenJsonData = new GenerateJSON();

                        string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                        einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, Datasetresult.Tables[0].Rows[0]["Document_Number"].ToString().Trim(), "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GRN has been successfully Approved and Generated QR Code for Supplier Shortage Debit Note.');", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GRN Entry has been Approved successfully.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + grnEntity.ErrorMsg + "');", true);
                }

            }
            catch (Exception exp)
            {
               Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Enabled = false;
                BtnReject.Enabled = false;
                ddlInwardNumber.Enabled = false;

                GRNEntity grnEntity = new GRNEntity();
                grnEntity.BranchCode = ddlBranch.SelectedValue.ToString();
                grnEntity.InwardNumber = ddlInwardNumber.SelectedValue.ToString();
                grnEntity.InwardDate = txtInwardDate.Text.ToString();
                grnEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
                grnEntity.SupplierCode = ddlSupplierName.SelectedValue.ToString();
                grnEntity.PackageStatus = ddlPackageStatus.SelectedValue.ToString();
                grnEntity.PackageOpenDate = txtPackageOpenDate.Text.ToString();
                grnEntity.LRTransfer = ddlLRTransfer.SelectedValue.ToString();
                grnEntity.LocalPurchaseTax = txtPurchaseTax.Text.ToString();
                grnEntity.Remarks = txtRemarks.Text.ToString();
                grnEntity.InvoiceNumber = txtInvoiceNo.Text;
                grnEntity.InvoiceDate = txtInvoiceDate.Text;
                grnEntity.ClearingAgentNo = txtClearingAgentNo.Text.ToString();
                grnEntity.CheckPostName = txtCheckPostName.Text.ToString();
                grnEntity.ClearenceDate = txtClearenceDate.Text.ToString();
                grnEntity.ClearenceAmount = txtClearenceAmount.Text.ToString();
                grnEntity.RoadPermitNo = txtRoadPermitNo.Text.ToString();
                grnEntity.RoadPermitDate = txtRoadPermitDate.Text.ToString();

                GRNTransactions grnTransactions = new GRNTransactions();
                grnTransactions.UpdateGRNFinalEntryRejection(ref grnEntity, Session["GRNRemarks"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('GRN has been Rejected Sucessfully. Please inform WareHouse for Rechecking');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["GRNRemarks"] = objp.Session["UserName"] + "/" + objp.Session["UserID"] + " - " + Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("GRNFinalEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("GRNFinalEntry.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        protected void ddlInwardNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInwardNumber.SelectedValue != "0")
                {
                    GRNTransactions grnTransactions = new GRNTransactions();
                    GRNEntity grnEntity = grnTransactions.GetInwardInfoForGRNEntry(ddlInwardNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    ddlBranch.SelectedValue = grnEntity.BranchCode;
                    txtInwardDate.Text = grnEntity.InwardDate;
                    ddlTransactionType.SelectedValue = grnEntity.TransactionTypeCode;
                    ddlSupplierName.SelectedValue = grnEntity.SupplierCode;
                    ddlPackageStatus.SelectedValue = grnEntity.PackageStatus;
                    txtPackageOpenDate.Text = grnEntity.PackageOpenDate;
                    ddlLRTransfer.SelectedValue = grnEntity.LRTransfer;
                    txtPurchaseTax.Text = TwoDecimalConversion(grnEntity.LocalPurchaseTax);
                    txtRemarks.Text = grnEntity.Remarks;
                    txtInvoiceNo.Text = grnEntity.InvoiceNumber;
                    txtInvoiceDate.Text = grnEntity.InvoiceDate;
                    txtClearingAgentNo.Text = grnEntity.ClearingAgentNo;
                    txtCheckPostName.Text = grnEntity.CheckPostName;
                    txtClearenceDate.Text = grnEntity.ClearenceDate;
                    txtClearenceAmount.Text = TwoDecimalConversion(grnEntity.ClearenceAmount);
                    txtRoadPermitNo.Text = grnEntity.RoadPermitNo;
                    txtRoadPermitDate.Text = grnEntity.RoadPermitDate;
                    txtWareHouseNo.Text = grnEntity.WarehouseNo.ToString();
                    txtWarehouseDate.Text = grnEntity.WarehouseDate.ToString();

                    hdnInvoiceNo.Value = grnEntity.InvoiceNumber;
                    hdnOSLSvalue.Value = grnEntity.OSLSindicator;
                    hdnInwardvalue.Value = grnEntity.InwardValue;
                    hdnHdrTaxvalue.Value = grnEntity.HeaderTaxValue;
                    hdnInsvalue.Value = grnEntity.InsValue;
                    hdnDiscvalue.Value = grnEntity.DiscValue;

                    if (grnEntity.Items.Count > 0)
                        BindExistingRowsDuringEdit(grnEntity.Items);
                    else
                        FirstGridViewRow();

                    //foreach (GridViewRow gr in grvItemDetails.Rows)
                    //{
                    //    gr.Enabled = false;
                    //    gr.Cells[9].Enabled = true;
                    //}

                    if (txtRemarks.Text == "AUTO GRN")
                        BtnReject.Attributes.Add("style", "display:none;");
                    else
                        BtnReject.Attributes.Add("style", "background-color:Red; color:White; display:inline;");

                    FreezeButtons(true);

                    UpdpanelTop.Update();
                    UpdPanelGrid.Update();

                    hdnScreenMode.Value = "E";
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("GRNFinalEntry.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtInwardQty = (TextBox)e.Row.FindControl("txtInwardQty");
                    TextBox txtAcceptedQty = (TextBox)e.Row.FindControl("txtAcceptedQty");
                    TextBox txtShortage = (TextBox)e.Row.FindControl("txtShortage");
                    TextBox txtItemLocation = (TextBox)e.Row.FindControl("txtItemLocation");

                    if (txtAcceptedQty == null)
                        return;

                    txtWareHouseNo.Attributes.Add("disabled", "true");
                    txtWarehouseDate.Attributes.Add("disabled", "true");

                    txtAcceptedQty.Attributes.Add("OnChange", "return funGRNAcceptedQtyValidation('" + txtInwardQty.ClientID + "','" + txtAcceptedQty.ClientID + "','" + txtShortage.ClientID + "');");
                    txtHdnGridCtrls.Text = txtHdnGridCtrls.Text + "|" + txtAcceptedQty.ClientID + "," + txtShortage.ClientID + "," + txtWareHouseNo.ClientID + "," + txtWarehouseDate.ClientID + "," + txtItemLocation.ClientID;
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
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
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }

        protected void ddlBranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBranch.SelectedValue == "0")
                    return;

                ddlInwardNumber.DataBind();

                SetDefaultValues();
                FirstGridViewRow();
                FreezeButtons(true);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GRNFinalEntry), exp);
            }
        }     
        

        private void SetDefaultValues()
        {         
            txtInwardDate.Text = string.Empty;
            //txtPackageOpenDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ddlTransactionType.SelectedValue = "0";
            ddlSupplierName.SelectedValue = "0";
            //txtPackageOpenDate.Text = "";
            txtPurchaseTax.Text = "0.00";
            txtRemarks.Text = "";
            txtInvoiceNo.Text = "";
            hdnInvoiceNo.Value = "";
            txtInwardDate.Text = "";
            txtClearingAgentNo.Text = "";
            txtCheckPostName.Text = "";
            txtClearenceDate.Text = "";
            txtClearenceAmount.Text = "";

            txtRoadPermitNo.Text = "";
            txtRoadPermitDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
                        TextBox txtSNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSNo");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtSupplierPartNo");
                        TextBox txtPOQuantity = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtPOQuantity");
                        TextBox txtRcvdQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtRcvdQty");
                        TextBox txtInwardQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtInwardQty");
                        TextBox txtAcceptedQty = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtAcceptedQty");
                        TextBox txtIndicator = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtIndicator");
                        TextBox txtShortage = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtShortage");
                        TextBox txtItemLocation = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtItemLocation");
                        TextBox txtHandlingCharges = (TextBox)grvItemDetails.Rows[rowIndex].FindControl("txtHandlingCharges");

                        txtSNo.Text = dt.Rows[i]["Col0"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i]["Col1"].ToString();                        
                        txtPOQuantity.Text = dt.Rows[i]["Col2"].ToString();
                        txtRcvdQty.Text = dt.Rows[i]["Col3"].ToString();
                        txtInwardQty.Text = dt.Rows[i]["Col4"].ToString();
                        txtAcceptedQty.Text = dt.Rows[i]["Col5"].ToString();
                        txtIndicator.Text = dt.Rows[i]["Col6"].ToString();
                        txtShortage.Text = dt.Rows[i]["Col7"].ToString();
                        txtItemLocation.Text = dt.Rows[i]["Col8"].ToString();
                        txtHandlingCharges.Text = dt.Rows[i]["Col9"].ToString();
                        
                        rowIndex++;
                    }
                }
            }
        }
                
        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
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
                dr["Col0"] = string.Empty;
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
            grvItemDetails.Rows[0].Cells[0].ColumnSpan = 10;
            ViewState["GridRowCount"] = "0";
            hdnRowCnt.Value = "0";
        }

        private void BindExistingRowsDuringEdit(List<GRNItem> lstGRNItem)
        {
            DataTable dt = new DataTable();
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
            foreach (GRNItem grn in lstGRNItem)
            {
                dr = dt.NewRow();
                dr["Col0"] = grn.SNo.ToString();
                dr["Col1"] = grn.SupplierPartNo.ToString();
                dr["Col2"] = grn.POQty.ToString();
                dr["Col3"] = grn.ReceivedQty.ToString();
                dr["Col4"] = grn.InwardQty.ToString();
                dr["Col5"] = grn.AcceptedQty.ToString();
                dr["Col6"] = grn.Indicator.ToString();
                dr["Col7"] = grn.Shortage.ToString();
                dr["Col8"] = grn.ItemLocation.ToString();
                dr["Col9"] = grn.HandlingCharges.ToString();
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = dt.Rows.Count;
            hdnRowCnt.Value = dt.Rows.Count.ToString();

            grvItemDetails.DataSource = dt;
            grvItemDetails.DataBind();

            SetPreviousData();

        }

        private void FreezeButtons(bool Fzflag)
        {
            txtClearingAgentNo.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
            BtnReject.Enabled = Fzflag;
            btnReset.Enabled = Fzflag;
            btnCancel.Enabled = Fzflag;
            divGRN.Disabled = !Fzflag;
            ddlInwardNumber.Enabled = Fzflag;
            GRNPanel.Enabled = Fzflag;            
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
    }
}
