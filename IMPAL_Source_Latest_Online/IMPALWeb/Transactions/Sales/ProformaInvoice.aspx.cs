using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;
using System.Configuration;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class ProformaInvoice : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnBack.Visible = false;

                    if (cryProformaInvoiceReprint != null)
                    {
                        cryProformaInvoiceReprint.Dispose();
                        cryProformaInvoiceReprint = null;
                    }

                    hdnScreenMode.Value = "A";
                    ddlSalesInvoiceNumber.Visible = false;
                    txtCourierCharges.Text = "0";
                    txtInsuranceCharges.Text = "0";
                    ddlCustomerName.Enabled = false;
                    ddlSalesReqNumber.Enabled = false;
                    BtnReport.Visible = false;
                    chkActive.Visible = false;
                    hdnCustOSLSStatus.Value = "A";
                    ddlCashDiscount.Enabled = true;
                    txtSalesInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    BtnSubmit.Attributes.Add("OnClick", "return SalesInvoiceEntrySubmit();");
                    BtnReset.Attributes.Add("OnClick", "return fnReset();");
                    FirstGridViewRow();
                    ddlFreightIndicator.SelectedValue = "2";
                    ddlCashDiscount.Attributes.Add("OnChange", "return ChangeCashDiscount();");                                
                    LoadTransactionType();
                    ddlVindicator.Enabled = false;
                    LoadShippingAddressStates(Session["BranchCode"].ToString());
                    hdnStateCode.Value = ddlShippingState.SelectedValue;                               
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    BtnReportOs.Enabled = false;
                }

                if (Session["BranchCode"].ToString().ToUpper() == "MGT")
                    BtnReportOs.Visible = true;
                else
                    BtnReportOs.Visible = false;
            }
            catch (Exception exp)
            {                
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryProformaInvoiceReprint != null)
            {
                cryProformaInvoiceReprint.Dispose();
                cryProformaInvoiceReprint = null;
            }
        }
        protected void cryProformaInvoiceReprint_Unload(object sender, EventArgs e)
        {
            if (cryProformaInvoiceReprint != null)
            {
                cryProformaInvoiceReprint.Dispose();
                cryProformaInvoiceReprint = null;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransactionType.SelectedValue.ToString() == "461" && txtRefDocument.Text != "")
                {
                    BindFDOItemDetail(txtRefDocument.Text);
                }
                else
                {
                    AddNewRow();
                }                
                DisableOnEditMode();
                upHeader.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSalesReqNumber.Items.Clear();

                if (ddlCustomerName.SelectedIndex > 0)
                {
                    LoadCustomerDetails();
                    List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                    if (lstCustomers.Count > 0)
                    {
                        txtCustomerCode.Text = ddlCustomerName.SelectedValue.ToString();
                        txtAddress1.Text = lstCustomers[0].address1.ToString();
                        txtAddress2.Text = lstCustomers[0].address2.ToString();
                        txtAddress4.Text = lstCustomers[0].address4.ToString();
                        txtGSTIN.Text = lstCustomers[0].GSTIN.ToString();
                        hdnCustOSLSStatus.Value = lstCustomers[0].CustOSLSStatus.ToString();
                        txtHdnCustTownCode.Text = lstCustomers[0].Town_Code.ToString();
                        txtLocation.Text = lstCustomers[0].Location.ToString();
                        txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[0].Credit_Limit.ToString());
                        txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[0].Outstanding_Amount.ToString());

                        txtShippingName.Text = ddlCustomerName.SelectedItem.Text;
                        txtShippingAddress1.Text = lstCustomers[0].address1.ToString();
                        txtShippingAddress2.Text = lstCustomers[0].address2.ToString();
                        txtShippingAddress4.Text = lstCustomers[0].address4.ToString();
                        txtShippingGSTIN.Text = lstCustomers[0].GSTIN.ToString();
                        txtShippingLocation.Text = lstCustomers[0].Location.ToString();

                        if (Session["BranchCode"].ToString().ToUpper() == "MGT")
                        {
                            ddlCashDiscount.Enabled = true;
                        }
                        else
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || lstCustomers[0].CDType.ToString() == "Y")
                                ddlCashDiscount.Enabled = true;
                            else
                                ddlCashDiscount.Enabled = false;
                        }

                        txtCashBillCustomer.Text = lstCustomers[0].Customer_Name.ToString();
                        txtCashBillCustomerTown.Text = lstCustomers[0].Location.ToString();

                        if (lstCustomers[0].Sales_Man_Code == "" || lstCustomers[0].Sales_Man_Code == null)
                        {
                            ddlSalesMan.SelectedValue = "0";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Salesman Need to Map with the Customer');", true);
                        }
                        else
                        {
                            ddlSalesMan.SelectedValue = lstCustomers[0].Sales_Man_Code;
                            ddlSalesReqNumber.Enabled = false;

                            if (lstCustomers[0].GSTIN == "" || lstCustomers[0].GSTIN == null)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IMPAL", "alert('GSTIN is Not Available for the Customer');", true);
                            }

                            ddlCashDiscount.SelectedValue = "1";
                        }
                    }
                    else
                    {
                        ddlSalesMan.SelectedIndex = 0;
                        ddlCashDiscount.SelectedIndex = 0;
                        ddlVindicator.SelectedIndex = 0;
                        txtCustomerPONo.Text = "";
                        txtCustomerPODate.Text = "";
                        txtCustomerCode.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtAddress4.Text = "";
                        txtGSTIN.Text = "";
                        hdnCustOSLSStatus.Value = "";
                        txtLocation.Text = "";
                        txtCustomerCreditLimit.Text = "";
                        txtCustomerOutStanding.Text = "";
                        txtCanBillUpTo.Text = "";

                        txtShippingName.Text = "";
                        txtShippingAddress1.Text = "";
                        txtShippingAddress2.Text = "";
                        txtShippingAddress4.Text = "";
                        txtShippingGSTIN.Text = "";
                        txtShippingLocation.Text = "";

                        FirstGridViewRow();
                    }
                }
                else
                {
                    ddlSalesMan.SelectedIndex = 0;
                    ddlCashDiscount.SelectedIndex = 0;
                    ddlVindicator.SelectedIndex = 0;
                    txtCustomerPONo.Text = "";
                    txtCustomerPODate.Text = "";
                    txtCustomerCode.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtAddress4.Text = "";
                    txtGSTIN.Text = "";
                    hdnCustOSLSStatus.Value = "";
                    txtLocation.Text = "";
                    txtCustomerCreditLimit.Text = "";
                    txtCustomerOutStanding.Text = "";
                    txtCanBillUpTo.Text = "";

                    txtShippingName.Text = "";
                    txtShippingAddress1.Text = "";
                    txtShippingAddress2.Text = "";
                    txtShippingAddress4.Text = "";
                    txtShippingGSTIN.Text = "";
                    txtShippingLocation.Text = "";

                    FirstGridViewRow();                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        private void ServerActionforHeader()
        {
            string strTransValue = ddlTransactionType.SelectedValue.ToString();

            panelReceipt.Attributes.Add("style", "display:none");

            if (strTransValue == "421")
            {
                LoadGovtCustomers();
            }
            else
            {
                LoadCustomers();                
            }

            LoadSalesMan();
            LoadCashDiscount();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false)
                {
                    BtnSubmit.Visible = false;
                    SubmitAction();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        private void SubmitAction()
        {
            SalesEntity SalesInvoiceEntity = new SalesEntity();
            SalesInvoiceEntity.Items = new List<SalesItem>();
            SalesInvoiceEntity.SalesInvoiceNumber = "";// txtSalesInvoiceNumber.Text;
            SalesInvoiceEntity.SalesInvoiceDate = txtSalesInvoiceDate.Text;
            SalesInvoiceEntity.TransactionTypeCode = ddlTransactionType.SelectedValue.ToString();
            SalesInvoiceEntity.CustomerCode = ddlCustomerName.SelectedValue.ToString();
            SalesInvoiceEntity.CustSalesReqNumber = ddlSalesReqNumber.SelectedValue.ToString();
            SalesInvoiceEntity.CustomerPONumber = txtCustomerPONo.Text;
            SalesInvoiceEntity.CustomerPODate = txtCustomerPODate.Text;
            SalesInvoiceEntity.SalesManCode = ddlSalesMan.SelectedValue.ToString();
            SalesInvoiceEntity.RefDocumentNumber = txtRefDocument.Text.ToString();
            SalesInvoiceEntity.Remarks = txtRemarks.Text;
            SalesInvoiceEntity.CashDiscountCode = ddlCashDiscount.SelectedValue.ToString();
            Double OrderValue = 0;
            if (txtTotalValue.Text != "")
            {
                OrderValue = Convert.ToDouble(TwoDecimalConversion(txtTotalValue.Text.ToString()));
            }
            SalesInvoiceEntity.OrderValue = OrderValue;
            SalesInvoiceEntity.LRTransfer = "N";//rdLRTransfer.SelectedValue.ToString();
            SalesInvoiceEntity.LRNumber = txtLRNumber.Text;
            SalesInvoiceEntity.LRDate = txtLRDate.Text;
            SalesInvoiceEntity.MarkingNumber = txtCaseMarking.Text;
            SalesInvoiceEntity.NumberOfCases = txtNoOfCases.Text.ToString();
            SalesInvoiceEntity.Weight = txtWeight.Text;
            SalesInvoiceEntity.Carrier = txtCarrier.Text;
            SalesInvoiceEntity.FreightIndicatorCode = ddlFreightIndicator.SelectedValue;
            double dbFreightAmout;
            if (txtFreightAmount.Text != "")
                dbFreightAmout = double.Parse(txtFreightAmount.Text);
            else
                dbFreightAmout = 0;
            SalesInvoiceEntity.FreightAmount = dbFreightAmout;
            SalesInvoiceEntity.BranchCode = ddlBranch.SelectedValue.ToString(); //Session["BranchCode"].ToString();//
            int iCourierCharge;
            if (txtCourierCharges.Text != "")
                iCourierCharge = int.Parse(txtCourierCharges.Text);
            else
                iCourierCharge = 0;

            int iInsuranceCharges;
            if (txtInsuranceCharges.Text != "")
                iInsuranceCharges = int.Parse(txtInsuranceCharges.Text);
            else
                iInsuranceCharges = 0;

            SalesInvoiceEntity.CourierCharge = iCourierCharge;
            SalesInvoiceEntity.InsuranceChargePerc = iInsuranceCharges;
            SalesInvoiceEntity.InsuranceCharges = iInsuranceCharges;
            SalesInvoiceEntity.CustomerName = txtCashBillCustomer.Text; //ddlCustomerName.SelectedItem.Text;
            SalesInvoiceEntity.CustomerTown = txtAddress4.Text;
            SalesInvoiceEntity.Indicator = ddlVindicator.SelectedValue; //ddlVindicator.SelectedValue;
            SalesInvoiceEntity.Remarks = txtRemarks.Text;
            SalesInvoiceEntity.ModeOfReceipt = ddlModeOfREceipt.SelectedValue;
            SalesInvoiceEntity.ChequeDraftNumber = txtChequeDraftNo.Text;
            SalesInvoiceEntity.ChequeDraftDate = txtChequeDraftDt.Text;
            SalesInvoiceEntity.BankName = txtBank.Text;
            SalesInvoiceEntity.BankBranchName = txtBankBranch.Text;
            SalesInvoiceEntity.ReceiptLocalOutstation = ddlLocalOutstation.SelectedValue;
            if (txtTotalValue.Text.ToString() != "")
            {
                SalesInvoiceEntity.AmountReceived = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtTotalValue.Text), 0));
            }
            SalesInvoiceEntity.BalanceAmount = 0;

            SalesInvoiceEntity.ShippingName = txtShippingName.Text;
            SalesInvoiceEntity.ShippingAddress1 = txtShippingAddress1.Text == "" ? txtAddress1.Text : txtShippingAddress1.Text;
            SalesInvoiceEntity.ShippingAddress2 = txtShippingAddress2.Text == "" ? txtAddress2.Text : txtShippingAddress2.Text;
            SalesInvoiceEntity.ShippingAddress4 = txtShippingAddress4.Text == "" ? txtAddress4.Text : txtShippingAddress4.Text;
            SalesInvoiceEntity.ShippingGSTIN = txtShippingGSTIN.Text == "" ? txtGSTIN.Text : txtShippingGSTIN.Text;
            SalesInvoiceEntity.ShippingLocation = txtShippingLocation.Text == "" ? txtLocation.Text : txtShippingLocation.Text;
            SalesInvoiceEntity.ShippingState = ddlShippingState.SelectedItem.Text;

            int iNoofRows = 0;
            int SNo = 0;
            SalesItem SalesInvoiceItem = null;
            SalesInvoiceItem = new SalesItem();

            if (ddlSalesReqNumber.SelectedIndex <= 0)
            {
                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        string filter = "Item_Code <> ''"; //textBox1.Text=ur input   
                        string filter1 = "Supplier_Item <>''";
                        DataView view = new DataView(dt);
                        view.RowFilter = filter;
                        view.RowFilter = filter1;
                        dt = view.ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dt.Rows.Count; i++)
                            {
                                SalesInvoiceItem = new SalesItem();
                                SNo += 1;

                                string Oslsind, ValueType = "";
                                Oslsind = dt.Rows[i - 1]["OS"].ToString(); ///txtOSLS.Text;     

                                if (Oslsind == "O")
                                    ValueType = "OS";
                                else if (Oslsind == "L")
                                    ValueType = "LS";

                                if (SalesInvoiceEntity.TransactionTypeCode == "361" || SalesInvoiceEntity.TransactionTypeCode == "461")
                                    ValueType = "FD";
                                if (SalesInvoiceEntity.TransactionTypeCode == "471" || SalesInvoiceEntity.TransactionTypeCode == "371")
                                    ValueType = "LR";

                                SalesInvoiceItem.ValueType = ValueType;
                                SalesInvoiceItem.ItemCode = dt.Rows[i - 1]["Item_Code"].ToString();
                                SalesInvoiceItem.Quantity = dt.Rows[i - 1]["Qty"].ToString();
                                SalesInvoiceItem.OsLsIndicator = dt.Rows[i - 1]["OS"].ToString();
                                SalesInvoiceItem.ListPrice = dt.Rows[i - 1]["Branch_ListPrice"].ToString();
                                SalesInvoiceItem.SlbCode = dt.Rows[i - 1]["SLB"].ToString();
                                SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble(dt.Rows[i - 1]["SLB_NetValue"].ToString());
                                SalesInvoiceItem.ItemDiscount = dt.Rows[i - 1]["Discount"].ToString();
                                SalesInvoiceItem.SNO = SNo.ToString();
                                SalesInvoiceEntity.Items.Add(SalesInvoiceItem);
                            }
                        }
                    }
                }
            }

            if (SNo < grvItemDetails.Rows.Count)
            {
                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    SalesInvoiceItem = new SalesItem();

                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[0].FindControl("ddlSupplierName");
                    if (ddlSupplierName.Visible || ddlTransactionType.SelectedValue.ToString() == "461" || ddlSalesReqNumber.SelectedIndex > 0)
                    {
                        SNo += 1;
                        TextBox lblSupplier = (TextBox)gr.Cells[0].FindControl("lblSupplier");
                        DropDownList ddlItemCode = (DropDownList)gr.Cells[1].FindControl("ddlItemCode");
                        TextBox txtItemCode = (TextBox)gr.Cells[1].FindControl("txtItemCode");
                        Button btnSearch = (Button)gr.Cells[1].FindControl("btnSearch");
                        TextBox txtOSLS = (TextBox)gr.Cells[2].FindControl("txtOSLS");
                        DropDownList ddlSLB = (DropDownList)gr.Cells[3].FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)gr.Cells[3].FindControl("txtSLB");
                        TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                        TextBox txtCanOrderQty = (TextBox)gr.Cells[4].FindControl("txtCanOrderQty");
                        TextBox txtBranchListPrice = (TextBox)gr.Cells[5].FindControl("txtBranchListPrice");
                        TextBox txthCostPrice = (TextBox)gr.Cells[5].FindControl("txthCostPrice");
                        TextBox txtSLBNetValue = (TextBox)gr.Cells[6].FindControl("txtSLBNetValue");
                        TextBox txtDiscount = (TextBox)gr.Cells[7].FindControl("txtDiscount");
                        TextBox txtSalesTax = (TextBox)gr.Cells[8].FindControl("txtSalesTax");
                        TextBox txtGrossProfit = (TextBox)gr.Cells[9].FindControl("txtGrossProfit");

                        string Oslsind, ValueType = "";
                        Oslsind = txtOSLS.Text;

                        if (Oslsind == "O")
                            ValueType = "OS";
                        else if (Oslsind == "L")
                            ValueType = "LS";

                        if (SalesInvoiceEntity.TransactionTypeCode == "361" || SalesInvoiceEntity.TransactionTypeCode == "461")
                            ValueType = "FD";
                        if (SalesInvoiceEntity.TransactionTypeCode == "471" || SalesInvoiceEntity.TransactionTypeCode == "371")
                            ValueType = "LR";

                        SalesInvoiceItem.ValueType = ValueType;

                        if (ddlItemCode.Visible)
                            SalesInvoiceItem.ItemCode = ddlItemCode.SelectedValue.ToString();
                        else
                            SalesInvoiceItem.ItemCode = txtItemCode.Text.ToString();

                        SalesInvoiceItem.AvialableQuantity = txtCanOrderQty.Text;
                        SalesInvoiceItem.Quantity = txtQuantity.Text;
                        SalesInvoiceItem.OsLsIndicator = txtOSLS.Text;
                        SalesInvoiceItem.ListPrice = txtBranchListPrice.Text;
                        SalesInvoiceItem.SlbCode = ddlSLB.SelectedValue.ToString();
                        SalesInvoiceItem.SLBNetValuePrice = Convert.ToDouble(txtSLBNetValue.Text);

                        if (txtDiscount.Text == "")
                            txtDiscount.Text = "0";
                        SalesInvoiceItem.ItemDiscount = txtDiscount.Text;  //Need to verify for Distress Type

                        SalesInvoiceItem.SNO = SNo.ToString();
                        SalesInvoiceEntity.Items.Add(SalesInvoiceItem);
                    }
                }
            }

            BtnSubmit.Enabled = false;
            grvItemDetails.Enabled = false;
            imgEditToggle.Visible = false;
            upHeader.Update();
            UpdPanelGrid.Update();
            BtnReport.Visible = true;

            SalesTransactions salesTransactions = new SalesTransactions();
            int result = salesTransactions.AddNewProformaInvoice(ref SalesInvoiceEntity);
            if ((SalesInvoiceEntity.ErrorMsg == string.Empty) && (SalesInvoiceEntity.ErrorCode == "0"))
            {
                txtSalesInvoiceNumber.Text = SalesInvoiceEntity.SalesInvoiceNumber;
                Session["ProformaInvoiceNumber"] = SalesInvoiceEntity.SalesInvoiceNumber;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Proforma Invoice Has been Generated Successfully. Please Click On Report Button To Take the Print');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + SalesInvoiceEntity.ErrorMsg + "');", true);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            BtnReport.Visible = false;
            upHeader.Visible = false;
            UpdPanelGrid.Visible = false;
            btnBack.Visible = true;

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strSelectionFormula = default(string);
            string strBrnchField = default(string);
            string strBrnchValue = default(string);
            string strField = default(string);
            string strValue = default(string);
            string strReportName = default(string);

            strBrnchField = "{V_Invoice.Branch_Code}";
            strBrnchValue = ddlBranch.SelectedValue;
            strField = "{V_Invoice.Document_number}";
            strValue = txtSalesInvoiceNumber.Text;
            strSelectionFormula = strBrnchField + "='" + strBrnchValue + "' and " + strField + "='" + strValue + "'";
            strReportName = "po_pp_invoiceProforma";

            //SalesReport srpt = new SalesReport();
            //hdnEwayBillInd.Value = srpt.GetEWayBillInd(strBrnchValue, strValue);                

            cryProformaInvoiceReprint.ReportName = strReportName;
            cryProformaInvoiceReprint.RecordSelectionFormula = strSelectionFormula;
            cryProformaInvoiceReprint.GenerateReportAndExportInvoiceA4(strValue.Replace("/", "-"), 2);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("ProformaInvoice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {

                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        private List<Supplier> GetAllSuppliers()
        {
            SalesItem objItem = new SalesItem();            
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliers();
            return lstSuppliers;
        }      

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Part_Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("CanOrderQty", typeof(string)));
            dt.Columns.Add(new DataColumn("OS", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB_Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch_ListPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("Cost_Price", typeof(string)));
            dt.Columns.Add(new DataColumn("SLB_NetValue", typeof(string)));
            dt.Columns.Add(new DataColumn("Discount", typeof(string)));
            dt.Columns.Add(new DataColumn("SalesTax", typeof(string)));
            dt.Columns.Add(new DataColumn("Gross_Profit", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_SaleValue", typeof(string)));
            dt.Columns.Add(new DataColumn("Original_Req_Qty", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Supplier_Name"] = string.Empty;
                dr["Supplier_Item"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["Part_Number"] = string.Empty;
                dr["Qty"] = string.Empty;
                dr["CanOrderQty"] = string.Empty;
                dr["OS"] = string.Empty;
                dr["SLB"] = string.Empty;
                dr["SLB_Item"] = string.Empty;
                dr["Branch_ListPrice"] = string.Empty;
                dr["Cost_Price"] = string.Empty;
                dr["SLB_NetValue"] = string.Empty;
                dr["Discount"] = string.Empty;
                dr["SalesTax"] = string.Empty;
                dr["Gross_Profit"] = string.Empty;
                dr["Item_SaleValue"] = string.Empty;
                dr["Original_Req_Qty"] = string.Empty;
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

        private void BindGrid()
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");
                ddlSupplierName.DataSource = GetAllSuppliers();
                ddlSupplierName.DataValueField = "SupplierCode";
                ddlSupplierName.DataTextField = "SupplierName";
                ddlSupplierName.DataBind();

                TextBox txtItemCode = (TextBox)gr.Cells[2].FindControl("txtItemCode");
                TextBox txtSupplierPartNo = (TextBox)gr.Cells[6].FindControl("txtSupplierPartNo");
                TextBox txtListPrice = (TextBox)gr.Cells[7].FindControl("txtListPrice");
                TextBox txtCostPrice = (TextBox)gr.Cells[8].FindControl("txtCostPrice");
                TextBox txtPurDiscount = (TextBox)gr.Cells[9].FindControl("txtPurDiscount");
                TextBox txtDiscount = (TextBox)gr.Cells[10].FindControl("txtDiscount");
                TextBox txtEDIndicator = (TextBox)gr.Cells[11].FindControl("txtEDIndicator");
                TextBox txtEDValue = (TextBox)gr.Cells[12].FindControl("txtEDValue");
            }
        }

        private void LoadTransactionType()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<TransactionType> lstTransactionType = new List<TransactionType>();
            lstTransactionType = salesTrans.GetProformaTransactionType();
            ddlTransactionType.DataSource = lstTransactionType;
            ddlTransactionType.DataTextField = "TransactionTypeDesc";
            ddlTransactionType.DataValueField = "TransactionTypeCode";
            ddlTransactionType.DataBind();
        }

        private void LoadSalesInvoiceNumber(string strBranch)
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            lstSalesEntity = salesTrans.GetProformaInvoiceNumber(strBranch);
            ddlSalesInvoiceNumber.DataSource = lstSalesEntity;
            ddlSalesInvoiceNumber.DataTextField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataValueField = "SalesInvoiceNumber";
            ddlSalesInvoiceNumber.DataBind();
        }

        private void LoadCashDiscount()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<CashDiscount> lstCashDiscount = new List<CashDiscount>();
            lstCashDiscount = salesTrans.GetCashDiscount(Session["BranchCode"].ToString());
            ddlCashDiscount.DataSource = lstCashDiscount;
            ddlCashDiscount.DataTextField = "CashDiscountDesc";
            ddlCashDiscount.DataValueField = "CashDiscountCode";
            ddlCashDiscount.DataBind();
        }

        private void LoadSalesMan()
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<SalesMan> lstSaleMan = new List<SalesMan>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();
            lstSaleMan = salesTrans.GetBranchSalesMan(strBranch);//Session["BranchCode"].ToString());
            ddlSalesMan.DataSource = lstSaleMan;
            ddlSalesMan.DataTextField = "SalesManName";
            ddlSalesMan.DataValueField = "SalesManCode";
            ddlSalesMan.DataBind();
        }

        private void LoadShippingAddressStates(string BranchCode)
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<IMPALLibrary.SlabState> StateList = new List<IMPALLibrary.SlabState>();
            StateList = salesTrans.GetAllStatesShipping(BranchCode);
            ddlShippingState.DataSource = StateList;
            ddlShippingState.DataTextField = "StateName";
            ddlShippingState.DataValueField = "StateCode";
            ddlShippingState.DataBind();

            //ddlShippingState.SelectedValue = ;
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            if (grvItemDetails.FooterRow != null)
            {
                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                btnAdd.Enabled = Fzflag;
            }

            ddlSalesReqNumber.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
            BtnReportOs.Enabled = !Fzflag;
        }

        private void LoadCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetAllCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
        }

        private void LoadGovtCustomers()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetGovtCustomers(strBranch);
            ddlCustomerName.DataSource = lstCustomers;
            ddlCustomerName.DataTextField = "Customer_Name";
            ddlCustomerName.DataValueField = "Customer_Code";
            ddlCustomerName.DataBind();
        }

        private void LoadCustomerDetails()
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            string strBranch;
            if (ddlBranch.SelectedValue.ToString() != "" && ddlBranch.SelectedValue.ToString() != "0")
                strBranch = ddlBranch.SelectedValue.ToString();
            else
                strBranch = Session["BranchCode"].ToString();

            lstCustomers = customers.GetCustomerDetails(strBranch,ddlCustomerName.SelectedValue);
            ViewState["CustomerDetails"] = lstCustomers;
        }


        private void AddNewRow()
        {
            int rowIndex = 0;
            int iNoofRows = 0;

            if (ViewState["GridRowCount"] != null)
            {
                iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
            }

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
                        for (int i = iNoofRows; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[0].FindControl("ddlSupplierName");
                            TextBox lblSupplier = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[0].FindControl("lblSupplier");
                            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("ddlItemCode");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("txtItemCode");
                            Button btnSearch = (Button)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("btnSearch");
                            TextBox txtOSLS = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[2].FindControl("txtOSLS");
                            DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("ddlSLB");
                            TextBox txtSLB = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("txtSLB");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtQuantity");
                            TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtCanOrderQty");                            
                            TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txtBranchListPrice");
                            TextBox txthCostPrice = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txthCostPrice");
                            TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[6].FindControl("txtSLBNetValue");
                            TextBox txtDiscount = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtDiscount");
                            TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[8].FindControl("txtSalesTax");
                            TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtGrossProfit");
                            HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtItemSaleValue");
                            HiddenField txtHdnReqOrderQty = (HiddenField)grvItemDetails.Rows[iNoofRows - 1].Cells[9].FindControl("txtHdnReqOrderQty");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            if (ddlSupplierName.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = ddlSupplierName.SelectedValue;//txtCCWHNo.Text;
                                dtCurrentTable.Rows[i - 1]["Supplier_Item"] = ddlSupplierName.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = lblSupplier.Text.ToString();
                            }

                            if (ddlItemCode.SelectedItem != null)
                            {
                                dtCurrentTable.Rows[i - 1]["Item_Code"] = ddlItemCode.SelectedValue; //txtItemCode.Text;
                                dtCurrentTable.Rows[i - 1]["Part_Number"] = ddlItemCode.SelectedItem.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["Qty"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["CanOrderQty"] = txtCanOrderQty.Text;
                            dtCurrentTable.Rows[i - 1]["Original_Req_Qty"] = txtHdnReqOrderQty.Value;

                            dtCurrentTable.Rows[i - 1]["OS"] = txtOSLS.Text;
                            if (ddlSLB.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["SLB"] = ddlSLB.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["SLB_Item"] = ddlSLB.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["SLB_Item"] = txtSLB.Text.ToString();
                            }
                            dtCurrentTable.Rows[i - 1]["Branch_ListPrice"] = txtBranchListPrice.Text;

                            dtCurrentTable.Rows[i - 1]["Cost_Price"] = txthCostPrice.Text;
                            dtCurrentTable.Rows[i - 1]["SLB_NetValue"] = txtSLBNetValue.Text;
                            dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                            dtCurrentTable.Rows[i - 1]["SalesTax"] = txtSalesTax.Text;
                            dtCurrentTable.Rows[i - 1]["Gross_Profit"] = txtGrossProfit.Text;
                            dtCurrentTable.Rows[i - 1]["Item_SaleValue"] = txtItemSaleValue.Value;
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
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                int iNoofRows = 0;
                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }

                if (dt.Rows.Count > 1)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {

                        DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                        TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                        DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                        Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                        TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                        TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");                        
                        TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                        DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                        TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                        TextBox txthCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txthCostPrice");
                        TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                        TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                        TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                        TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                        HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");
                        HiddenField txtHdnReqOrderQty = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtHdnReqOrderQty");
                        
                        txtSupplierPartNo.Text = dt.Rows[i - 1]["Part_Number"].ToString();
                        lblSupplier.Text = dt.Rows[i - 1]["Supplier_Item"].ToString();
                        txtItemCode.Text = dt.Rows[i - 1]["Item_Code"].ToString();
                        txtItemCode.Visible = true;
                        txtQuantity.Text = dt.Rows[i - 1]["Qty"].ToString();
                        txtCanOrderQty.Text = dt.Rows[i - 1]["CanOrderQty"].ToString();
                        txtHdnReqOrderQty.Value = dt.Rows[i - 1]["Original_Req_Qty"].ToString();
                        txtOSLS.Text = dt.Rows[i - 1]["OS"].ToString();
                        txtBranchListPrice.Text = dt.Rows[i - 1]["Branch_ListPrice"].ToString();
                        txthCostPrice.Text = dt.Rows[i - 1]["Cost_Price"].ToString();
                        txtSLBNetValue.Text = dt.Rows[i - 1]["SLB_NetValue"].ToString();
                        txtDiscount.Text = dt.Rows[i - 1]["Discount"].ToString();
                        txtSalesTax.Text = dt.Rows[i - 1]["SalesTax"].ToString();
                        txtGrossProfit.Text = dt.Rows[i - 1]["Gross_Profit"].ToString();
                        txtItemSaleValue.Value = dt.Rows[i - 1]["Item_SaleValue"].ToString();
                        txtSLB.Text = dt.Rows[i - 1]["SLB_Item"].ToString();
                        //ddlSLB.SelectedValue = dt.Rows[i - 1]["SLB"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private void HideDllItemCodeDropDown()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int index = 0; index < grvItemDetails.Rows.Count; index++)
            {
                DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSupplierName");
                TextBox lblSupplier = (TextBox)grvItemDetails.Rows[index].Cells[0].FindControl("lblSupplier");
                DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlItemCode");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtItemCode");
                Button btnSearch = (Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch");
                DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[index].Cells[3].FindControl("ddlSLB");

                if (index != grvItemDetails.Rows.Count - 1)
                {
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSupplierName")).Visible = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("lblSupplier")).Visible = true;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlItemCode")).Visible = false;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSLB")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Visible = true;
                    //((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Text = ddlSLB.SelectedItem.Text;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txthCostPrice")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtOSLS")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCanOrderQty")).Enabled = false;
                }
                else
                {
                    ((TextBox)grvItemDetails.Rows[index].Cells[0].FindControl("lblSupplier")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSLB")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txthCostPrice")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSLB")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtCanOrderQty")).Enabled = false;
                }
            }
            txtHdnGridCtrls.Text = sb.ToString();
        }


        protected void ddlCustomerName_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSearch = (Button)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtCurrentSearch = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName"); ;
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                TextBox txtSLB = (TextBox)grdrDropDownRow.FindControl("txtSLB");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                TextBox txthCostPrice = (TextBox)grdrDropDownRow.FindControl("txthCostPrice");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdrDropDownRow.FindControl("txtHdnReqOrderQty");

                if (btnSearch.Text == "Reset")
                {
                    ddlItemCode.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtOSLS.Text = "";
                    txtQuantity.Text = "";
                    txtCanOrderQty.Text = "";
                    ddlSLB.SelectedValue = "0";
                    ddlSLB.Enabled = false;
                    txtSLB.Text = "";
                    txtBranchListPrice.Text = "";
                    txthCostPrice.Text = "";
                    txtSLBNetValue.Text = "";
                    txtDiscount.Text = "";
                    txtSalesTax.Text = "";
                    txtGrossProfit.Text = "";
                    txtItemSaleValue.Value = "";
                    txtHdnReqOrderQty.Value = "";
                    CalculateTotalValue();
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        SalesTransactions salesItem = new SalesTransactions();
                        List<SalesItem> lstSuppliers = new List<SalesItem>();
                        ListItem firstListItem = new ListItem();

                        lstSuppliers = salesItem.GetProformaItems(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text, ddlTransactionType.SelectedValue.ToString());
                        ddlItemCode.DataSource = lstSuppliers;
                        ddlItemCode.Items.Add(firstListItem);
                        ddlItemCode.DataTextField = "SupplierPartNumber"; //"supplier_part_number";
                        ddlItemCode.DataValueField = "ItemCode";
                        ddlItemCode.DataBind();
                        ddlItemCode.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlCurrentDropDownList = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
                Button btnSearch = (Button)grdrDropDownRow.FindControl("btnSearch");
                TextBox txtCurrentSearch = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName");
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                TextBox txtSLB = (TextBox)grdrDropDownRow.FindControl("txtSLB");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                TextBox txthCostPrice = (TextBox)grdrDropDownRow.FindControl("txthCostPrice");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtDiscount = (TextBox)grdrDropDownRow.FindControl("txtDiscount");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdrDropDownRow.FindControl("txtHdnReqOrderQty");

                ddlItemCode.Visible = false;
                txtCurrentSearch.Visible = true;
                btnSearch.Text = "Search";
                txtItemCode.Text = "";
                txtCurrentSearch.Text = "";
                txtOSLS.Text = "";
                txtQuantity.Text = "";
                txtCanOrderQty.Text = "";
                ddlSLB.SelectedValue = "0";
                ddlSLB.Enabled = false;
                txtSLB.Text = "";
                txtBranchListPrice.Text = "";
                txthCostPrice.Text = "";
                txtSLBNetValue.Text = "";
                txtDiscount.Text = "";
                txtSalesTax.Text = "";
                txtGrossProfit.Text = "";
                txtItemSaleValue.Value = "";
                txtHdnReqOrderQty.Value = "";

                CalculateTotalValue();

                if (ddlSupplierName.SelectedIndex != 0)
                {
                    btnSearch.Enabled = true;
                    txtCurrentSearch.Enabled = true;
                    txtCurrentSearch.Focus();
                }
                else
                {
                    btnSearch.Enabled = false;
                    txtCurrentSearch.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void ddlItemCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlItemCode = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlItemCode.Parent.Parent);
                        
                if (ddlItemCode.SelectedItem.Value != "0")
                {
                    SalesTransactions salesItem = new SalesTransactions();
                    int HSNCode = salesItem.CheckHSNCodeItem(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                    if (HSNCode > 0)
                    {
                        bool isExisting = CheckExisting(ddlItemCode.SelectedValue);

                        if (isExisting)
                        {
                            TextBox txtSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                            TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                            TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                            TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                            DropDownList ddlSLB = (DropDownList)grdrDropDownRow.FindControl("ddlSLB");
                            TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                            TextBox txthCostPrice = (TextBox)grdrDropDownRow.FindControl("txthCostPrice");
                            TextBox txtCanOrderQty = (TextBox)grdrDropDownRow.FindControl("txtCanOrderQty");
                            TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                            HiddenField txtProductGroupCode = (HiddenField)grdrDropDownRow.FindControl("txtProductGroupCode");

                            double lstMultipleSalesTax = salesItem.GetSalesTax(ddlCustomerName.SelectedItem.Value, ddlItemCode.SelectedValue, Session["BranchCode"].ToString(), "", ddlTransactionType.SelectedValue.ToString(), "");

                            txtSalesTax.Text = TwoDecimalConversion(lstMultipleSalesTax.ToString());

                            lblMessage.Text = "";
                            lblMessage1.Text = "";

                            string BrItemPrice = "";
                            BrItemPrice = salesItem.GetBranchItemPrice(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                            if (BrItemPrice != "")
                            {
                                txtQuantity.Enabled = true;
                                txtItemCode.Text = ddlItemCode.SelectedItem.Value;
                                txtSupplierPartNo.Text = ddlItemCode.SelectedItem.Text;
                                string strTransactionType = ddlTransactionType.SelectedItem.Value;

                                List<SalesItem> lstItemPrice = new List<SalesItem>();
                                lstItemPrice = salesItem.GetProformaItemPrice(ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString());

                                if (string.IsNullOrEmpty(lstItemPrice[0].ListPrice.ToString()) || lstItemPrice[0].ListPrice.ToString() == "0.00")
                                    txtBranchListPrice.Text = "";
                                else
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].ListPrice.ToString());

                                txtOSLS.Text = lstItemPrice[0].OsLsIndicator.ToString();
                                txthCostPrice.Text = TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString());
                                lblPackingQuantity.Text = lstItemPrice[0].PackingQuantity.ToString();
                                txtProductGroupCode.Value = lstItemPrice[0].ProductGroupCode;

                                List<Customer> lstCustomers1 = (List<Customer>)ViewState["CustomerDetails"];

                                if (lstCustomers1[0].Party_Type_Code.ToUpper() == "DLREXP")
                                {
                                    lblMessage1.Text = "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>Export Customer</b></font></span>";
                                    PanelTaxFilter.Attributes.Add("style", "display:none");
                                }
                                else
                                {
                                    PanelTaxFilter.Attributes.Add("style", "display:inline");
                                    if (hdnCustOSLSStatus.Value == "L")
                                    {
                                        lblMessage.Text = lblMessage.Text + "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6'><b>SGST/UTGST/CGST Customer</b></font></span>";
                                    }
                                    else
                                    {
                                        lblMessage.Text = lblMessage.Text + "<span style='text-decoartion: blink;'>&nbsp;&nbsp;&nbsp;&nbsp;<font color='red' size='6' style='text-decoartion: blink;'><b>IGST Customer</b></font></span>";
                                    }
                                }

                                lblPackingQuantity.Text = "";
                                txtQuantity.Enabled = true;
                                txtQuantity.Focus();
                                List<SalesItem> lstSLB = new List<SalesItem>();
                                lstSLB = salesItem.GetSLB(ddlCustomerName.SelectedItem.Value, ddlItemCode.SelectedItem.Value, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                                ddlSLB.DataSource = lstSLB;
                                ddlSLB.DataTextField = "SlbDesc";
                                ddlSLB.DataValueField = "SlbCode";
                                ddlSLB.DataBind();
                            }
                            else
                            {
                                txtQuantity.Enabled = false;
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item is not available in Branch Item Price');", true);
                            }
                        }
                        else
                        {
                            TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                            txtQuantity.Enabled = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Record already exists...');", true);
                        }
                    }
                    else
                    {
                        TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                        txtQuantity.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HSN Code is not available for this Item');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void ddlSLB_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSLB = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSLB.Parent.Parent);
                TextBox txtOSLS = (TextBox)grdrDropDownRow.FindControl("txtOSLS");
                TextBox txtBranchListPrice = (TextBox)grdrDropDownRow.FindControl("txtBranchListPrice");
                DropDownList ddlItemCode = (DropDownList)grdrDropDownRow.FindControl("ddlItemCode");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtSLBNetValue = (TextBox)grdrDropDownRow.FindControl("txtSLBNetValue");
                TextBox txtSalesTax = (TextBox)grdrDropDownRow.FindControl("txtSalesTax");
                TextBox txthCostPrice = (TextBox)grdrDropDownRow.FindControl("txthCostPrice");
                TextBox txtGrossProfit = (TextBox)grdrDropDownRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdrDropDownRow.FindControl("txtItemSaleValue");
                HiddenField txtProductGroupCode = (HiddenField)grdrDropDownRow.FindControl("txtProductGroupCode");

                foreach (GridViewRow row in grvItemDetails.Rows)
                {
                    //Finding Dropdown control  
                    Control ctrl = row.FindControl("ddlSLB") as DropDownList;
                    if (ctrl != null)
                    {
                        DropDownList ddl1 = (DropDownList)ctrl;
                        //Comparing ClientID of the dropdown with sender
                        if (ddlSLB.ClientID == ddl1.ClientID)
                        {
                            txthdSlab.Text = ddl1.SelectedItem.Text;
                            break;
                        }
                    }
                }

                List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                if (ddlSalesReqNumber.SelectedIndex <= 0)
                {
                    SalesTransactions salesItem = new SalesTransactions();
                    double dblSLBNetValuePrice = 0;
                    decimal CouponValue = 0;
                    int Qty = 0;
                    if (txtQuantity.Text != "")
                    {
                        Qty = int.Parse(txtQuantity.Text);
                        double BranchListPrice = 0;
                        if (txtBranchListPrice.Text != "")
                            BranchListPrice = double.Parse(txtBranchListPrice.Text.ToString());
                        string Indicator = "";
                        if (txtOSLS.Text == "L") Indicator = "LS";
                        if (txtOSLS.Text == "O") Indicator = "OS";
                        int SLBCode = 0;
                        if (ddlSLB.SelectedItem.Value != "")
                            SLBCode = int.Parse(ddlSLB.SelectedItem.Value);

                        string strTransactionType, strItemCode;
                        strTransactionType = ddlTransactionType.SelectedItem.Value; //201";// "361";// "011";

                        if (strTransactionType == "461")
                            strItemCode = txtItemCode.Text;
                        else
                            strItemCode = ddlItemCode.SelectedItem.Value;

                        if (SLBCode > 0)
                            dblSLBNetValuePrice = salesItem.GetSLBNetValuePrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, Indicator, strTransactionType, SLBCode);

                        txtSLBNetValue.Text = dblSLBNetValuePrice.ToString();

                        double dblItemSellingPrice = 0, dblGrossProfit = 0, dblCostPrice = 0;
                        if (ddlTransactionType.SelectedValue.ToString() == "461") //FDO
                            dblItemSellingPrice = salesItem.GetFDOItemSellingPrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, dblSLBNetValuePrice, Indicator, strTransactionType, SLBCode);
                        else
                            dblItemSellingPrice = salesItem.GetItemSellingPrice(ddlCustomerName.SelectedItem.Value, strItemCode, Session["BranchCode"].ToString(), Qty, BranchListPrice, dblSLBNetValuePrice, Indicator, strTransactionType, SLBCode);

                        if (txthCostPrice.Text != "")
                            dblCostPrice = double.Parse(txthCostPrice.Text.ToString());

                        if (dblItemSellingPrice == 0)
                            dblGrossProfit = 0;
                        else
                            dblGrossProfit = (((dblItemSellingPrice - dblCostPrice) / dblItemSellingPrice) * 100);

                        txtGrossProfit.Text = dblGrossProfit.ToString();

                        double SaleValue = 0, dblCashDiscountPer = 0, dblCourierCharge = 0, dblInsuranceAmt = 0, AmountReceived;

                        if (ddlCashDiscount.SelectedValue != "")
                        {
                            dblCashDiscountPer = Convert.ToDouble(ddlCashDiscount.SelectedItem.Text);
                        }

                        SaleValue = 0;

                        if (SLBCode <= 50)
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141")
                            {
                                SaleValue = BranchListPrice;
                            }
                            else
                            {
                                SaleValue = BranchListPrice + (BranchListPrice * (dblSLBNetValuePrice / 100));
                            }
                        }
                        else if (SLBCode > 50 && SLBCode <= 90)
                        {
                            SaleValue = dblSLBNetValuePrice;
                        }
                        else if (SLBCode > 90 && SLBCode <= 99)
                        {
                            SaleValue = dblItemSellingPrice;
                            txtBranchListPrice.Text = dblItemSellingPrice.ToString();
                        }

                        decimal rate = salesItem.GetItemRate(txtItemCode.Text);
                        CouponValue = CouponValue + Convert.ToDecimal(rate * Qty);

                        SaleValue = SaleValue - (SaleValue * (dblCashDiscountPer / 100)) - Convert.ToDouble(CouponValue);

                        SaleValue = SaleValue * (1 + (Convert.ToDouble(txtSalesTax.Text) / 100));

                        if (txtCourierCharges.Text != "")
                        {
                            dblCourierCharge = Convert.ToDouble(txtCourierCharges.Text);
                        }
                        if (txtInsuranceCharges.Text != "")
                        {
                            dblInsuranceAmt = Convert.ToDouble(txtInsuranceCharges.Text);
                        }
                        ///SALEVALUE – CASH  DISCOUNT + TAX VALUE + COURIER CHARGES + INSURANCE CHARGES + POSTAGE CHARGES – COUPON CHARGES.
                        if (ddlTransactionType.SelectedValue.ToString() == "461") ///FDO
                        {
                            txtSalesTax.Text = "S";
                            txtItemSaleValue.Value = SaleValue.ToString();
                            AmountReceived = (SaleValue) * Qty;

                            double GrandAmountRececived = 0;
                            if (txtTotalValue.Text != "" || txtTotalValue.Text == "0.00")
                            {
                                GrandAmountRececived = Convert.ToDouble(txtTotalValue.Text);
                            }

                            GrandAmountRececived = GrandAmountRececived + AmountReceived + dblCourierCharge + dblInsuranceAmt;
                        }
                        else
                        {
                            txtItemSaleValue.Value = SaleValue.ToString();
                            AmountReceived = (SaleValue); 
                        }

                        CalculateTotalValue();
                    }
                    else
                    {
                        lblMessage.Text = "Order Quantity should not be Zero or empty";
                    }
                }
                else
                {
                    SalesTransactions salesTrans = new SalesTransactions();
                    List<SalesItem> lstSalesItem = new List<SalesItem>();

                    if (ddlSLB.SelectedValue.ToString() != "0")
                    {
                        lstSalesItem = salesTrans.GetCustomerSalesReqItemDetails(ddlSalesReqNumber.SelectedValue.ToString(), ddlTransactionType.SelectedValue.ToString(), ddlCashDiscount.SelectedItem.Text, ddlItemCode.SelectedValue.ToString(), ddlSLB.SelectedValue.ToString(), Session["BranchCode"].ToString(), "0", txtHdnCustTownCode.Text);

                        txtBranchListPrice.Text = TwoDecimalConversion(lstSalesItem[0].ListPrice.ToString());
                        txthCostPrice.Text = TwoDecimalConversion(lstSalesItem[0].CostPrice.ToString());
                        txtSLBNetValue.Text = TwoDecimalConversion(lstSalesItem[0].SLBNetValuePrice.ToString());

                        if (lstCustomers[0].Party_Type_Code.ToUpper() == "DLREXP" || lstSalesItem[0].SalesTaxText.ToLower() == "second sales" || lstSalesItem[0].SalesTaxText.ToLower() == "agricultural implements")
                        {
                            txtSalesTax.Text = "S";
                        }
                        else
                        {
                            txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[0].SalesTaxPercentage.ToString());
                        }

                        txtGrossProfit.Text = SixDecimalConversion(lstSalesItem[0].GrossProfit.ToString());
                        txtItemSaleValue.Value = lstSalesItem[0].SaleValue.ToString();
                    }
                    else
                    {
                        txtBranchListPrice.Text = "";
                        txthCostPrice.Text = "";
                        txtSLBNetValue.Text = "";
                        txtSalesTax.Text = "";
                        txtGrossProfit.Text = "";
                        txtItemSaleValue.Value = "0";
                    }

                    CalculateTotalValue();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void grvItemDetails_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "V")
                    return;

                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        string transactionType = ddlTransactionType.SelectedValue.ToString();
                        DropDownList ddlSupplierName = (DropDownList)e.Row.Cells[1].FindControl("ddlSupplierName");

                        ddlSupplierName.DataSource = GetAllSuppliers();
                        ddlSupplierName.DataValueField = "SupplierCode";
                        ddlSupplierName.DataTextField = "SupplierName";
                        ddlSupplierName.DataBind();
                     
                        TextBox txtItemCode = (TextBox)e.Row.Cells[1].FindControl("txtItemCode");
                        TextBox txtCanOrderQty = (TextBox)e.Row.FindControl("txtCanOrderQty");
                        HiddenField txtItemSaleValue = (HiddenField)e.Row.FindControl("txtItemSaleValue");
                        TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                        TextBox txtDiscount = (TextBox)e.Row.FindControl("txtDiscount");
                        DropDownList ddlSLB = (DropDownList)e.Row.FindControl("ddlSLB");
                        TextBox txtSLB = (TextBox)e.Row.FindControl("txtSLB");
                        HiddenField txtHdnReqOrderQty = (HiddenField)e.Row.FindControl("txtHdnReqOrderQty");

                        if (transactionType == "141" || transactionType == "041")
                        {
                            txtDiscount.Enabled = true;
                            ddlSLB.Enabled = false;
                        }
                        else
                        {
                            txtDiscount.Enabled = false;
                            ddlSLB.Enabled = true;
                        }
                        
                        ddlSLB.Attributes.Add("OnChange", "SalesInvoiceValidationHeader();");

                        if (ddlSalesReqNumber.SelectedIndex <= 0)
                        {
                            txtQuantity.Attributes.Add("OnChange", "SalesInvoiceQtyChangeGST(" + hdnRowCnt.Value + ",'" + txtCanOrderQty.ClientID + "','" + txtHdnReqOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + ddlSLB.ClientID + "','" + txtItemSaleValue.ClientID + "','" + txtDiscount.ClientID + "')");

                            if (ddlTransactionType.SelectedValue.ToString() == "041" || ddlTransactionType.SelectedValue.ToString() == "141")
                                txtDiscount.Attributes.Add("OnChange", "SalesInvoiceDiscountChange('" + txtCanOrderQty.ClientID + "','" + txtQuantity.ClientID + "','" + txtItemSaleValue.ClientID + "');");
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        private string SixDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.000000";
            else
                return string.Format("{0:0.000000}", Convert.ToDecimal(strValue));
        }

        private string DecimalToIntConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0";
            else
                return string.Format("{0:0}", Convert.ToDecimal(strValue));
        }

        protected void ddlSalesInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSalesInvoiceNumber.SelectedValue != "-- Select --")
                {
                    SalesTransactions salesTrans = new SalesTransactions();
                    List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
                    lstSalesEntity = salesTrans.GetProformaInvoiceHeader(ddlSalesInvoiceNumber.SelectedValue.ToString(), "");
                    if (lstSalesEntity.Count > 0)
                    {
                        for (int i = 0; i <= lstSalesEntity.Count - 1; i++)
                        {
                            ddlTransactionType.SelectedValue = lstSalesEntity[i].TransactionTypeCode;
                            txtSalesInvoiceDate.Text = lstSalesEntity[i].SalesInvoiceDate;
                            ddlCustomerName.SelectedValue = lstSalesEntity[i].CustomerCode;
                            ddlSalesMan.SelectedValue = lstSalesEntity[i].SalesManCode;
                            rdLRTransfer.SelectedValue = "N";//lstSalesEntity[i].LRTransfer;
                            txtLRNumber.Text = lstSalesEntity[i].LRNumber;
                            txtLRDate.Text = lstSalesEntity[i].LRDate;
                            ddlCashDiscount.Text = lstSalesEntity[i].CashDiscountCode;
                            txtTotalValue.Text = TwoDecimalConversion(lstSalesEntity[i].OrderValue.ToString());
                            ddlVindicator.SelectedValue = lstSalesEntity[i].Indicator;
                            txtCaseMarking.Text = lstSalesEntity[i].MarkingNumber;
                            txtNoOfCases.Text = lstSalesEntity[i].NumberOfCases.ToString();
                            txtWeight.Text = lstSalesEntity[i].Weight;
                            txtCarrier.Text = lstSalesEntity[i].Carrier.ToString();
                            if (lstSalesEntity[i].FreightIndicatorCode != "")
                                ddlFreightIndicator.SelectedValue = lstSalesEntity[i].FreightIndicatorCode;

                            txtFreightAmount.Text = lstSalesEntity[i].FreightAmount.ToString();
                            txtCourierCharges.Text = lstSalesEntity[i].CourierCharge.ToString();
                            txtInsuranceCharges.Text = lstSalesEntity[i].InsuranceCharges.ToString();
                            txtCustomerPONo.Text = lstSalesEntity[i].CustomerPONumber;
                            txtCustomerPODate.Text = lstSalesEntity[i].CustomerPODate.ToString();
                            string TransValue = ddlTransactionType.SelectedValue.ToString();

                            if (TransValue == "101" || TransValue == "001")
                            {
                                txtBank.Text = lstSalesEntity[i].BankName.ToString();
                                ddlModeOfREceipt.SelectedValue = lstSalesEntity[i].ModeOfReceipt.ToString();
                                txtChequeDraftNo.Text = lstSalesEntity[i].ChequeDraftNumber.ToString();
                                txtChequeDraftDt.Text = lstSalesEntity[i].ChequeDraftDate.ToString();
                                txtBankBranch.Text = lstSalesEntity[i].BankBranchName.ToString();
                                ddlLocalOutstation.SelectedValue = lstSalesEntity[i].LocalOutstation.ToString();
                            }

                            chkActive.Visible = false;
                        }

                        string strTransValue = ddlTransactionType.SelectedValue.ToString();

                        if (strTransValue == "101" || strTransValue == "001")
                        {
                            panelReceipt.Attributes.Add("style", "display:inline");
                            txtChequeDraftNo.Enabled = false;
                            txtChequeDraftDt.Enabled = false;
                            txtBank.Enabled = false;
                            txtBankBranch.Enabled = false;
                            ddlModeOfREceipt.Enabled = false;
                            ddlLocalOutstation.Enabled = false;
                        }
                        else
                        {
                            panelReceipt.Attributes.Add("style", "display:none");
                        }

                        if (ddlCustomerName.SelectedValue != "")
                        {
                            LoadCustomerDetails();
                            List<Customer> lstCustomers = (List<Customer>)ViewState["CustomerDetails"];

                            if (lstCustomers.Count > 0)
                            {
                                for (int i = 0; i < lstCustomers.Count; i++)
                                {
                                    if (lstCustomers[i].Customer_Code == ddlCustomerName.SelectedValue.ToString())
                                    {
                                        txtCustomerCode.Text = ddlCustomerName.SelectedValue.ToString();
                                        txtAddress1.Text = lstCustomers[i].address1.ToString();
                                        txtAddress2.Text = lstCustomers[i].address2.ToString();
                                        txtAddress4.Text = lstCustomers[i].address3.ToString();
                                        txtGSTIN.Text = lstCustomers[i].GSTIN.ToString();
                                        hdnCustOSLSStatus.Value = lstCustomers[i].CustOSLSStatus.ToString();
                                        txtLocation.Text = lstCustomers[i].Location.ToString();

                                        txtShippingName.Text = ddlCustomerName.SelectedItem.Text;
                                        txtShippingAddress1.Text = lstCustomers[i].address1.ToString();
                                        txtShippingAddress2.Text = lstCustomers[i].address2.ToString();
                                        txtShippingAddress4.Text = lstCustomers[i].address3.ToString();
                                        txtShippingGSTIN.Text = lstCustomers[i].GSTIN.ToString();
                                        txtShippingLocation.Text = lstCustomers[i].Location.ToString();

                                        txtHdnCustTownCode.Text = lstCustomers[0].Town_Code.ToString();
                                        txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[i].Credit_Limit.ToString());
                                        txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[i].Outstanding_Amount.ToString());
                                        txtCashBillCustomer.Text = lstCustomers[i].Customer_Name.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    List<SalesItem> lstSalesItem = new List<SalesItem>();
                    lstSalesItem = salesTrans.GetProformaInvoiceDetail(ddlSalesInvoiceNumber.SelectedValue.ToString(), Session["BranchCode"].ToString());

                    if (lstSalesEntity.Count > 0)
                    {
                        grvItemDetails.DataSource = lstSalesItem;
                        grvItemDetails.DataBind();

                        for (int i = 1; i <= lstSalesItem.Count; i++)
                        {
                            DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                            TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                            TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                            DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                            Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                            TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                            TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                            DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                            TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                            TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                            TextBox txthCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txthCostPrice");
                            TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                            TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                            TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                            TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                            HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");
                            LinkButton delete = (LinkButton)grvItemDetails.Rows[i - 1].Cells[10].FindControl("lnkDelete");

                            lblSupplier.Text = lstSalesItem[i - 1].SupplierName.ToString();
                            ddlSupplierName.Visible = false;
                            txtSupplierPartNo.Text = lstSalesItem[i - 1].ItemSupplierPartNumber.ToString();
                            txtItemCode.Text = lstSalesItem[i - 1].ItemCode.ToString();
                            txtItemCode.Visible = true;
                            ddlItemCode.Visible = false;
                            btnSearch.Visible = false;
                            txtCanOrderQty.Visible = false;
                            ddlSLB.Visible = false;
                            txtSLB.Visible = true;
                            delete.Visible = false;

                            txtQuantity.Text = lstSalesItem[i - 1].Quantity.ToString();
                            txtOSLS.Text = lstSalesItem[i - 1].OsLsIndicator.ToString();
                            txtSLB.Text = lstSalesItem[i - 1].SlbDesc.ToString();
                            //txtBranchListPrice.Text = lstSalesItem[i - 1].ListPrice.ToString();
                            txtSLBNetValue.Text = lstSalesItem[i - 1].SLBNetValuePrice.ToString();
                            txtDiscount.Text = lstSalesItem[i - 1].ItemDiscount.ToString();
                            txtSalesTax.Text = TwoDecimalConversion(lstSalesItem[i - 1].SalesTaxPercentage.ToString());

                            SalesTransactions salesItem = new SalesTransactions();
                            List<SalesItem> lstItemPrice = new List<SalesItem>();
                            double dblItemSellingPrice = 0, dblCostPrice = 0;
                            lstItemPrice = salesItem.GetProformaItemPrice(lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString());

                            if (lstItemPrice.Count() > 0)
                            {
                                if (ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041") //For Distress & Distress Manual;
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString());
                                else
                                    txtBranchListPrice.Text = TwoDecimalConversion(lstItemPrice[0].ListPrice.ToString());

                                if (ddlTransactionType.SelectedValue.ToString() == "461") //FDO
                                    dblItemSellingPrice = salesItem.GetFDOItemSellingPrice(ddlCustomerName.SelectedValue.ToString(), lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), Convert.ToInt16(lstSalesItem[i - 1].Quantity), Convert.ToDouble(txtBranchListPrice.Text), Convert.ToDouble(lstSalesItem[i - 1].SLBNetValuePrice), lstSalesItem[i - 1].OsLsIndicator.ToString(), ddlTransactionType.SelectedValue.ToString(), Convert.ToInt16(lstSalesItem[i - 1].SlbCode));
                                else
                                    dblItemSellingPrice = salesItem.GetItemSellingPrice(ddlCustomerName.SelectedValue.ToString(), lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), Convert.ToInt16(lstSalesItem[i - 1].Quantity), Convert.ToDouble(txtBranchListPrice.Text), Convert.ToDouble(lstSalesItem[i - 1].SLBNetValuePrice), lstSalesItem[i - 1].OsLsIndicator.ToString(), ddlTransactionType.SelectedValue.ToString(), Convert.ToInt16(lstSalesItem[i - 1].SlbCode));

                                dblCostPrice = Convert.ToDouble(TwoDecimalConversion(lstItemPrice[0].CostPrice.ToString()));
                                if (dblItemSellingPrice == 0)
                                    txtGrossProfit.Text = "0";
                                else
                                    txtGrossProfit.Text = TwoDecimalConversion(Convert.ToString((((dblItemSellingPrice - dblCostPrice) / dblItemSellingPrice) * 100)));
                            }
                            else
                            {
                                txtBranchListPrice.Text = "0";
                                txtGrossProfit.Text = "0";
                            }
                        }
                    }
                }
                else
                {
                    BtnReset_Click(this, null);
                    chkActive.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {

            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    Server.ClearError();
                    Response.Redirect("ProformaInvoice.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else if (hdnScreenMode.Value == "V")
                {
                    ddlSalesInvoiceNumber_SelectedIndexChanged(this, null);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void BtnReportOs_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CustomerCode"] = ddlCustomerName.SelectedValue;
                Server.ClearError();
                Response.Redirect("OutstandingDaysReport.aspx");
                Context.ApplicationInstance.CompleteRequest();                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBranch.SelectedValue != "" && ddlBranch.SelectedValue != "0")
                    LoadSalesInvoiceNumber(ddlBranch.SelectedValue);
                LoadCustomers();
                LoadSalesMan();
                FreezeOrUnFreezeButtons(false);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnScreenMode.Value == "A")
                {
                    hdnScreenMode.Value = "V";
                    LoadSalesInvoiceNumber(ddlBranch.SelectedValue);
                    LoadCustomers();
                    LoadSalesMan();
                    LoadCashDiscount();

                    ddlSalesInvoiceNumber.SelectedIndex = -1;
                    ddlSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Visible = false;
                    imgEditToggle.Visible = false;
                    FirstGridViewRow();
                }
                else if (hdnScreenMode.Value == "V")
                {
                    hdnScreenMode.Value = "A";
                    ddlSalesInvoiceNumber.Visible = false;
                    txtSalesInvoiceNumber.Visible = true;
                    txtSalesInvoiceNumber.Text = string.Empty;
                    txtSalesInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlTransactionType.SelectedValue = "0";
                    ddlCustomerName.SelectedValue = "0";
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString(); //ConfigurationManager.AppSettings["BranchCode"].ToString();
                    txtSalesInvoiceDate.Text = string.Empty;
                    ddlSalesMan.SelectedValue = "0";
                    txtLRNumber.Text = string.Empty; ;
                    txtLRDate.Text = string.Empty; ;
                    ddlCashDiscount.Text = string.Empty;
                    txtTotalValue.Text = string.Empty;
                    txtCaseMarking.Text = string.Empty;
                    txtNoOfCases.Text = string.Empty;
                    txtWeight.Text = string.Empty;
                    txtCarrier.Text = string.Empty;
                    ddlFreightIndicator.SelectedValue = "0";
                    txtFreightAmount.Text = string.Empty;
                    txtCourierCharges.Text = string.Empty;
                    txtInsuranceCharges.Text = string.Empty;
                    txtCustomerPONo.Text = string.Empty;
                    txtCustomerPODate.Text = string.Empty;
                }

                BtnSubmit.Attributes.Add("OnClick", "return SalesInvoiceEntrySubmit();");

                FreezeOrUnFreezeButtons(false);
                DisableOnEditMode();
                grvItemDetails.Enabled = false;
                BtnSubmit.Enabled = false;
                upHeader.Update();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        private void DisableOnEditMode()
        {
            //--- Sales Invoice
            ddlTransactionType.Enabled = false;
            ddlCustomerName.Enabled = false;
            ddlSalesReqNumber.Enabled = false;
            rdLRTransfer.Enabled = false;
            txtCourierCharges.Enabled = false;
            txtInsuranceCharges.Enabled = false;
            txtCustomerPONo.Enabled = false;
            txtCustomerPODate.Enabled = false;
            ddlCashDiscount.Enabled = false;
            ddlSalesMan.Enabled = false;
            txtRefDocument.Enabled = false;
            ddlVindicator.Enabled = false;
            BtnReportOs.Enabled = false;

            //--- Customer Information
            txtCustomerCode.Enabled = false;
            txtAddress1.Enabled = false;
            txtAddress2.Enabled = false;
            txtAddress4.Enabled = false;
            txtGSTIN.Enabled = false;
            txtLocation.Enabled = false;
            txtCustomerCreditLimit.Enabled = false;
            txtCustomerOutStanding.Enabled = false;
            txtCanBillUpTo.Enabled = false;

            //--- Carrier Information
            txtLRNumber.Enabled = false;
            txtCaseMarking.Enabled = false;
            ddlFreightIndicator.Enabled = false;
            txtLRDate.Enabled = false;
            txtNoOfCases.Enabled = false;
            txtFreightAmount.Enabled = false;
            txtCarrier.Enabled = false;
            txtWeight.Enabled = false;
            txtRemarks.Enabled = false;

            //-- Receipt Details

            if (ddlTransactionType.SelectedValue == "101" || ddlTransactionType.SelectedValue == "001")
            {
                txtChequeDraftNo.Enabled = false;
                txtChequeDraftDt.Enabled = false;
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                ddlModeOfREceipt.Enabled = false;
                ddlLocalOutstation.Enabled = false;
            }
        }

        private void BindFDOItemDetail(string InwardNumber)
        {
            SalesTransactions salesTrans = new SalesTransactions();
            List<SalesItem> lstSalesItem = new List<SalesItem>();
            lstSalesItem = salesTrans.GetFDOInwardItemDetail(InwardNumber, Session["BranchCode"].ToString());
            if (lstSalesItem.Count > 0)
            {
                grvItemDetails.DataSource = lstSalesItem;
                grvItemDetails.DataBind();
                hdnRowCnt.Value = lstSalesItem.Count.ToString();

                for (int i = 1; i <= lstSalesItem.Count; i++)
                {
                    DropDownList ddlSupplierName = (DropDownList)grvItemDetails.Rows[i - 1].Cells[0].FindControl("ddlSupplierName");
                    TextBox lblSupplier = (TextBox)grvItemDetails.Rows[i - 1].Cells[0].FindControl("lblSupplier");
                    TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                    DropDownList ddlItemCode = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlItemCode");
                    TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                    Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                    TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtQuantity");
                    TextBox txtCanOrderQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[2].FindControl("txtCanOrderQty");
                    TextBox txtOSLS = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtOSLS");
                    DropDownList ddlSLB = (DropDownList)grvItemDetails.Rows[i - 1].Cells[4].FindControl("ddlSLB");
                    TextBox txtSLB = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtSLB");
                    TextBox txtBranchListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtBranchListPrice");
                    TextBox txthCostPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txthCostPrice");
                    TextBox txtSLBNetValue = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtSLBNetValue");
                    TextBox txtDiscount = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtDiscount");
                    TextBox txtSalesTax = (TextBox)grvItemDetails.Rows[i - 1].Cells[8].FindControl("txtSalesTax");
                    //TextBox txtEDIndicator = (TextBox)grvItemDetails.Rows[rowIndex].Cells[1].FindControl("txtEDIndicator");
                    TextBox txtGrossProfit = (TextBox)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtGrossProfit");
                    HiddenField txtItemSaleValue = (HiddenField)grvItemDetails.Rows[i - 1].Cells[9].FindControl("txtItemSaleValue");

                    List<SalesItem> lstSLB = new List<SalesItem>();
                    lstSLB = salesTrans.GetSLB(ddlCustomerName.SelectedItem.Value, lstSalesItem[i - 1].ItemCode.ToString(), Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                    ddlSLB.DataSource = lstSLB;
                    ddlSLB.DataTextField = "SlbDesc"; //"supplier_part_number";
                    ddlSLB.DataValueField = "SlbCode";
                    ddlSLB.DataBind();

                    lblSupplier.Text = lstSalesItem[i - 1].SupplierName.ToString();
                    ddlSupplierName.Visible = false;
                    txtSupplierPartNo.Text = lstSalesItem[i - 1].ItemSupplierPartNumber.ToString();
                    txtItemCode.Text = lstSalesItem[i - 1].ItemCode.ToString();
                    txtItemCode.Visible = true;
                    ddlItemCode.Visible = false;
                    btnSearch.Visible = false;
                    txtCanOrderQty.Visible = false;
                    ddlSLB.Visible = true;
                    txtSLB.Visible = false;

                    txtQuantity.Text = lstSalesItem[i - 1].Quantity.ToString();
                    txtOSLS.Text = lstSalesItem[i - 1].OsLsIndicator.ToString();
                    txtBranchListPrice.Text = lstSalesItem[i - 1].ListPrice.ToString();
                    txtGrossProfit.Text = "0";
                }
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQuantity = (TextBox)sender;
                GridViewRow grdRow = ((GridViewRow)txtQuantity.Parent.Parent);
                DropDownList ddlSLB = (DropDownList)grdRow.FindControl("ddlSLB");
                TextBox txtSLBNetValue = (TextBox)grdRow.FindControl("txtSLBNetValue");
                TextBox txtSalesTax = (TextBox)grdRow.FindControl("txtSalesTax");
                TextBox txthCostPrice = (TextBox)grdRow.FindControl("txthCostPrice");
                TextBox txtGrossProfit = (TextBox)grdRow.FindControl("txtGrossProfit");
                HiddenField txtItemSaleValue = (HiddenField)grdRow.FindControl("txtItemSaleValue");
                HiddenField txtHdnReqOrderQty = (HiddenField)grdRow.FindControl("txtHdnReqOrderQty");

                if (txtQuantity.Text != null && txtQuantity.Text != "")
                {
                    if (Convert.ToInt64(txtQuantity.Text.ToString()) > 0)
                    {
                        ddlSLB.Enabled = true;

                        if (ddlSLB.Items.Count == 2)
                        {
                            ddlSLB.SelectedIndex = 1;
                            ddlSLB_OnSelectedIndexChanged(ddlSLB, EventArgs.Empty);
                        }

                        if (ddlSLB.SelectedIndex == 0)
                            ddlSLB.Focus();
                    }
                    else
                    {
                        if (ddlSLB.Items.Count <= 2)
                            ddlSLB.Enabled = false;
                        else
                            ddlSLB.Enabled = true;
                    }

                    CalculateTotalValue();
                }
                else
                {
                    if (ddlSLB.Items.Count <= 2)
                        ddlSLB.Enabled = false;
                    else
                        ddlSLB.Enabled = true;

                    txtSLBNetValue.Text = "";
                    txtSalesTax.Text = "";
                    txthCostPrice.Text = "";
                    txtGrossProfit.Text = "";
                    txtItemSaleValue.Value = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ProformaInvoice), exp);
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalValue();
        }

        protected void grvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.Rows.Clear();
                dt.AcceptChanges();

                if (grvItemDetails.Rows.Count >= 1)
                {
                    for (int i = 0; i < grvItemDetails.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        var strSNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSNo");
                        var strSupplierName = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierName");
                        var strSupplier = (TextBox)grvItemDetails.Rows[i].FindControl("lblSupplier");
                        var strItemCode = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemCode");
                        var strddlItemCode = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlItemCode");
                        var strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        var strOSLS = (TextBox)grvItemDetails.Rows[i].FindControl("txtOSLS");
                        var strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        var strCanOrderQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtCanOrderQty");
                        var strddlSLB = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSLB");
                        var strSLB = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLB");
                        var strBranchListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtBranchListPrice");
                        var strCostPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txthCostPrice");
                        var strSLBNetValue = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLBNetValue");
                        var strDiscount = (TextBox)grvItemDetails.Rows[i].FindControl("txtDiscount");
                        var strSalesTax = (TextBox)grvItemDetails.Rows[i].FindControl("txtSalesTax");
                        var strGrossProfit = (TextBox)grvItemDetails.Rows[i].FindControl("txtGrossProfit");
                        var strItemSaleValue = (HiddenField)grvItemDetails.Rows[i].FindControl("txtItemSaleValue");
                        var strOriginalReqQty = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnReqOrderQty");

                        dr["SNo"] = strSNo.Text;

                        if (strSupplier.Text != "")
                        {
                            List<Supplier> supplier = new List<Supplier>();
                            supplier = GetAllSuppliers();
                            supplier = supplier.Where(a => a.SupplierName == strSupplier.Text).ToList();

                            if (supplier.Count > 0)
                            {
                                dr["Supplier_Name"] = supplier[0].SupplierCode;
                            }
                        }
                        else
                        {
                            dr["Supplier_Name"] = strSupplierName.SelectedValue;
                        }

                        dr["Supplier_Item"] = strSupplier.Text;
                        dr["Item_Code"] = strItemCode.Text;
                        dr["Part_Number"] = strSupplierPartNo.Text;
                        dr["Qty"] = strQty.Text;
                        dr["CanOrderQty"] = strCanOrderQty.Text;
                        dr["OS"] = strOSLS.Text;

                        if (strSLB.Text != "")
                        {
                            SalesTransactions salesItem = new SalesTransactions();
                            List<SalesItem> lstSLB = new List<SalesItem>();
                            lstSLB = salesItem.GetSLB(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                            strddlSLB.DataSource = lstSLB;
                            strddlSLB.DataTextField = "SlbDesc"; //"supplier_part_number";
                            strddlSLB.DataValueField = "SlbCode";
                            strddlSLB.DataBind();

                            lstSLB = lstSLB.Where(a => a.SlbDesc == strSLB.Text).ToList();

                            if (lstSLB.Count > 0)
                            {
                                dr["SLB"] = lstSLB[0].SlbCode;
                            }
                        }
                        else
                        {
                            dr["SLB"] = strddlSLB.SelectedValue;
                        }

                        dr["SLB_Item"] = strSLB.Text;
                        dr["Branch_ListPrice"] = strBranchListPrice.Text;
                        dr["Cost_Price"] = strCostPrice.Text;
                        dr["SLB_NetValue"] = strSLBNetValue.Text;
                        dr["Discount"] = strDiscount.Text;
                        dr["SalesTax"] = strSalesTax.Text;
                        dr["Gross_Profit"] = strGrossProfit.Text;
                        dr["Item_SaleValue"] = strItemSaleValue.Value;
                        dr["Original_Req_Qty"] = strOriginalReqQty.Value;
                        dt.Rows.Add(dr);
                    }
                }

                dt.Rows.RemoveAt(e.RowIndex);
                grvItemDetails.DataSource = dt;
                grvItemDetails.DataBind();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList strSupplierName = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierName");
                        TextBox strSupplier = (TextBox)grvItemDetails.Rows[i].FindControl("lblSupplier");
                        TextBox strItemCode = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemCode");
                        DropDownList strddlItemCode = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlItemCode");
                        TextBox strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        TextBox strOSLS = (TextBox)grvItemDetails.Rows[i].FindControl("txtOSLS");
                        TextBox strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        TextBox strCanOrderQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtCanOrderQty");
                        DropDownList strddlSLB = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSLB");
                        TextBox strSLB = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLB");
                        TextBox strBranchListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtBranchListPrice");
                        TextBox strCostPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txthCostPrice");
                        TextBox strSLBNetValue = (TextBox)grvItemDetails.Rows[i].FindControl("txtSLBNetValue");
                        TextBox strDiscount = (TextBox)grvItemDetails.Rows[i].FindControl("txtDiscount");
                        TextBox strSalesTax = (TextBox)grvItemDetails.Rows[i].FindControl("txtSalesTax");
                        TextBox strGrossProfit = (TextBox)grvItemDetails.Rows[i].FindControl("txtGrossProfit");
                        HiddenField strItemSaleValue = (HiddenField)grvItemDetails.Rows[i].FindControl("txtItemSaleValue");
                        HiddenField strOriginalReqQty = (HiddenField)grvItemDetails.Rows[i].FindControl("txtHdnReqOrderQty");
                        Button btnSearch = (Button)grvItemDetails.Rows[i].FindControl("btnSearch");
                        LinkButton delete = (LinkButton)grvItemDetails.Rows[i].FindControl("lnkDelete");
                        Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");

                        strQty.Attributes.Add("OnChange", "SalesInvoiceQtyChangeGST(" + i + ",'" + strCanOrderQty.ClientID + "','" + strOriginalReqQty.ClientID + "','" + strQty.ClientID + "','" + strddlSLB.ClientID + "','" + strItemSaleValue.ClientID + "','" + strDiscount.ClientID + "');");

                        if (ddlTransactionType.SelectedValue.ToString() == "141")
                            strDiscount.Attributes.Add("OnChange", "SalesInvoiceDiscountChange('" + strCanOrderQty.ClientID + "','" + strQty.ClientID + "','" + strItemSaleValue.ClientID + "');");

                        strSupplierName.DataSource = GetAllSuppliers();
                        strSupplierName.DataValueField = "SupplierCode";
                        strSupplierName.DataTextField = "SupplierName";
                        strSupplierName.DataBind();

                        strSLB.Text = dt.Rows[i]["SLB_Item"].ToString();
                        strSupplierName.SelectedValue = dt.Rows[i]["Supplier_Name"].ToString();
                        strSupplier.Text = dt.Rows[i]["Supplier_Item"].ToString();
                        strItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                        strSupplierPartNo.Text = dt.Rows[i]["Part_Number"].ToString();
                        strQty.Text = dt.Rows[i]["Qty"].ToString();
                        strCanOrderQty.Text = dt.Rows[i]["CanOrderQty"].ToString();
                        strOSLS.Text = dt.Rows[i]["OS"].ToString();
                        strBranchListPrice.Text = dt.Rows[i]["Branch_ListPrice"].ToString();
                        strCostPrice.Text = dt.Rows[i]["Cost_Price"].ToString();
                        strSLBNetValue.Text = dt.Rows[i]["SLB_NetValue"].ToString();
                        strDiscount.Text = dt.Rows[i]["Discount"].ToString();
                        strSalesTax.Text = dt.Rows[i]["SalesTax"].ToString();
                        strGrossProfit.Text = dt.Rows[i]["Gross_Profit"].ToString();
                        strItemSaleValue.Value = dt.Rows[i]["Item_SaleValue"].ToString();
                        strOriginalReqQty.Value = dt.Rows[i]["Original_Req_Qty"].ToString();
                        strCostPrice.Visible = false;
                        strSupplier.Visible = false;

                        if (ddlSalesReqNumber.SelectedIndex > 0)
                        {
                            strSupplierName.Visible = false;
                            strSupplier.Visible = true;
                            strItemCode.Visible = true;
                            strddlItemCode.Visible = false;
                            btnSearch.Visible = false;

                            SalesTransactions salesItem = new SalesTransactions();
                            List<SalesItem> lstSLB = new List<SalesItem>();
                            lstSLB = salesItem.GetSLBSalesReq(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                            strddlSLB.DataSource = lstSLB;
                            strddlSLB.DataTextField = "SlbDesc";
                            strddlSLB.DataValueField = "SlbCode";
                            strddlSLB.SelectedValue = dt.Rows[i]["SLB"].ToString();
                            strddlSLB.DataBind();

                            strddlItemCode.Items.Add(new ListItem(dt.Rows[i]["Part_Number"].ToString(), dt.Rows[i]["Item_Code"].ToString()));

                            strddlSLB.Visible = true;
                            strSLB.Visible = false;
                            delete.Visible = true;
                            strSupplierPartNo.Enabled = false;
                            grvItemDetails.Enabled = true;
                            btnAdd.Enabled = false;
                        }
                        else
                        {
                            if (dt.Rows[i]["Item_Code"].ToString() != "")
                            {
                                if (dt.Rows.Count == 1 || i == (dt.Rows.Count-1))
                                {
                                    btnSearch.Text = "Reset";
                                    strddlItemCode.Visible = true;
                                    strSupplierPartNo.Visible = false;
                                    strCostPrice.Visible = false;
                                    strSupplier.Visible = false;

                                    SalesTransactions salesItem = new SalesTransactions();
                                    List<SalesItem> lstSuppliers = new List<SalesItem>();
                                    ListItem firstListItem = new ListItem();

                                    lstSuppliers = salesItem.GetProformaItems(strSupplierName.SelectedItem.Value, "", ddlTransactionType.SelectedValue.ToString());
                                    strddlItemCode.DataSource = lstSuppliers;
                                    strddlItemCode.Items.Add(firstListItem);
                                    strddlItemCode.DataTextField = "SupplierPartNumber";
                                    strddlItemCode.DataValueField = "ItemCode";
                                    strddlItemCode.DataBind();

                                    string strSupplierPartNumber = dt.Rows[i]["Part_Number"].ToString();

                                    lstSuppliers = lstSuppliers.Where(a => a.SupplierPartNumber == strSupplierPartNumber).ToList();
                                    if (lstSuppliers.Count > 0)
                                    {
                                        strddlItemCode.SelectedValue = lstSuppliers[0].ItemCode;
                                    }
                                    strddlItemCode.SelectedItem.Text = strSupplierPartNumber;

                                    List<SalesItem> lstSLB = new List<SalesItem>();
                                    lstSLB = salesItem.GetSLB(ddlCustomerName.SelectedItem.Value, strItemCode.Text, Session["BranchCode"].ToString(), txtHdnCustTownCode.Text);
                                    strddlSLB.DataSource = lstSLB;
                                    strddlSLB.DataTextField = "SlbDesc";
                                    strddlSLB.DataValueField = "SlbCode";
                                    strddlSLB.DataBind();

                                    if (dt.Rows[i]["SLB"].ToString() != "")
                                    {
                                        strddlSLB.SelectedValue = dt.Rows[i]["SLB"].ToString();
                                        strddlSLB.Enabled = true;
                                    }
                                }
                                else
                                {
                                    btnSearch.Visible = false;
                                    strSupplierName.Visible = false;
                                    strddlItemCode.Visible = false;
                                    strddlSLB.Visible = false;
                                    strSupplierPartNo.Enabled = false;
                                    strDiscount.Enabled = false;
                                    strSLB.Visible = true;
                                    strQty.Enabled = false;
                                    strSupplier.Visible = true;
                                }
                            }
                        }
                    }
                }

                hdnRowCnt.Value = dt.Rows.Count.ToString();
                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count.ToString();

                if (dt.Rows.Count == 0 && ddlSalesReqNumber.SelectedIndex <= 0)
                {
                    FirstGridViewRow();
                }

                if (ddlSalesReqNumber.SelectedIndex > 0)
                {
                    Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                    btnAdd.Enabled = false;
                }

                CalculateTotalValue();
                lblMessage.Text = "";
                lblMessage1.Text = "";
                lblPackingQuantity.Text = "";
            }
        }

        protected void ddlModeOfREceipt_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlModeOfREceipt.SelectedValue.ToString() == "M")
            {
                txtChequeDraftNo.Enabled = false;
                txtChequeDraftDt.Enabled = false;                
                txtBank.Enabled = false;
                txtBankBranch.Enabled = false;
                txtChequeDraftNo.Text = "";
                txtChequeDraftDt.Text = "";
                txtBank.Text = "";
                txtBankBranch.Text = "";
            }
            else
            {
                txtChequeDraftNo.Enabled = true;
                txtChequeDraftDt.Enabled = true;                
                txtBank.Enabled = true;
                txtBankBranch.Enabled = true;
            }
        }

        private void CalculateTotalValue()
        {
            decimal totalValue = 0;
            decimal CouponValue = 0;
            SalesTransactions salesTransactions = new SalesTransactions();

            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    HiddenField txtItemSaleValue = (HiddenField)gr.Cells[9].FindControl("txtItemSaleValue");
                    TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                    TextBox txtItemCode = (TextBox)gr.Cells[1].FindControl("txtItemCode");
                    TextBox txtDiscount = (TextBox)gr.Cells[7].FindControl("txtDiscount");
                    TextBox txtBranchListPrice = (TextBox)gr.Cells[5].FindControl("txtBranchListPrice");

                    if (txtItemSaleValue != null)
                    {
                        if (txtItemSaleValue.Value != "")
                        {
                            if (ddlTransactionType.SelectedValue.ToString() == "141" || ddlTransactionType.SelectedValue.ToString() == "041")
                            {
                                if (!(txtDiscount.Text == "" || txtBranchListPrice.Text == ""))
                                {
                                    totalValue = totalValue + Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text) + Convert.ToDecimal(txtBranchListPrice.Text) * Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(txtDiscount.Text) / 100);
                                    txtTotalValue.Text = Convert.ToString(totalValue);
                                }
                                else
                                {
                                    totalValue = totalValue + Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text);
                                    txtTotalValue.Text = Convert.ToString(totalValue);
                                }
                            }
                            else
                            {
                                totalValue = totalValue + Convert.ToDecimal(txtItemSaleValue.Value) * Convert.ToDecimal(txtQuantity.Text);
                                txtTotalValue.Text = Convert.ToString(totalValue);
                            }                            
                        }
                        else
                        {
                            totalValue = totalValue + 0;
                            txtTotalValue.Text = Convert.ToString(totalValue);
                        }
                    }
                    else
                    {
                        totalValue = totalValue + 0;
                        txtTotalValue.Text = Convert.ToString(totalValue);
                    }
                }
                else
                {
                    txtTotalValue.Text = "";
                    txtCanBillUpTo.Text = "";
                }
            }

            if (txtTotalValue.Text != "")
            {
                decimal courierCharges = txtCourierCharges.Text == "" ? 0 : Convert.ToDecimal(txtCourierCharges.Text);
                decimal freightCharges = txtFreightAmount.Text == "" ? 0 : Convert.ToDecimal(txtFreightAmount.Text);
                decimal insuranceCharges = txtInsuranceCharges.Text == "" ? 0 : Convert.ToDecimal(txtInsuranceCharges.Text);
                insuranceCharges = (totalValue * insuranceCharges / 100);
                totalValue = totalValue + courierCharges + freightCharges + insuranceCharges - CouponValue;
                txtTotalValue.Text = TwoDecimalConversion(Convert.ToString(totalValue));

                txtCanBillUpTo.Text = "0";
                txtTotalValue.Visible = true;
            }

            upHeader.Update();

            if (!(ddlTransactionType.SelectedValue.ToString() == "001" || ddlTransactionType.SelectedValue.ToString() == "101" || ddlTransactionType.SelectedValue.ToString() == "071" || ddlTransactionType.SelectedValue.ToString() == "171")) //--- Cash Sales, Cash Sales Manual, Free of Cost, Free of Cost Manual
            {
                double TotalVal = 0;
                double CanBill = 0;

                //TotalVal = Convert.ToDouble(txtTotalValue.Text);
                CanBill = Convert.ToDouble(txtCanBillUpTo.Text);

                if (CanBill < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Total Value Exceeds the Can Bill Upto Amount');", true);
                    BtnSubmit.Enabled = false;
                }
                else
                    BtnSubmit.Enabled = true;
            }
        }

        private bool CheckExisting(string partNumber)
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                if (gr.Cells.Count > 1)
                {
                    TextBox txtItemCode = (TextBox)gr.Cells[3].FindControl("txtItemCode");
                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");
                    if (!(ddlSupplierName.Visible))
                    {
                        if (partNumber == txtItemCode.Text)
                        {
                            return false;
                        }
                    }
                }                
            }

            return true;
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTransactionType.Enabled = false;
            ServerActionforHeader();
            ddlCustomerName.Enabled = true;
            ddlShippingState.Items.Clear();
            LoadShippingAddressStates("0");
            ddlShippingState.SelectedValue = hdnStateCode.Value;
        }
    }
}