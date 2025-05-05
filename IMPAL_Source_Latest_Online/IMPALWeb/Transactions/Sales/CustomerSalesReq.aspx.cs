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
    public partial class CustomerSalesReq : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        string strBranchCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    ddlBranch.SelectedValue = strBranchCode;
                    ddlBranch.Enabled = false;
                    HdnIndHexCust.Value = "0";
                    LoadCustomers();
                    FirstGridViewRow();
                    cmbCustomerName.Focus();
                }
            }
            catch (Exception exp)
            {                
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
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

            lstCustomers = customers.GetAllCustomersComboBox(strBranch);
            cmbCustomerName.DataSource = lstCustomers;
            cmbCustomerName.DataTextField = "Customer_Name";
            cmbCustomerName.DataValueField = "Customer_Code";
            cmbCustomerName.DataBind();
        }

        private void GetCustomerDetails(string BranchCode, string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails oDtls = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                IMPALLibrary.Masters.CustomerDetails.CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(BranchCode, CustomerCode);
                txtCustomerCode.Text = cmbCustomerName.SelectedValue;
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
                txtPhone.Text = oCustomer.Phone;

                LoadLimits(strBranchCode, CustomerCode);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void LoadLimits(string strBranchCode, string CustomerCode)
        {
            Customers customers = new Customers();
            List<Customer> lstCustomers = new List<Customer>();
            //string CanCustomerRaise;
            double CanBillUpTo = 0;

            //if (Session["BranchCode"].ToString().ToUpper() == "MGT")
            //    CanCustomerRaise = customers.CanCustomerOSDaysCreditLimits(strBranchCode, CustomerCode);
            //else
            //    CanCustomerRaise = customers.CanCustomerOSCreditLimitsReq(strBranchCode, CustomerCode);

            //if (Session["BranchCode"].ToString().ToUpper() == "MGT")
            //    lstCustomers = customers.GetCustomerOSCreditLimitsOutstanding(strBranchCode, CustomerCode);
            //else
            lstCustomers = customers.GetCustomerOSCreditLimitsReq(strBranchCode, CustomerCode);

            if (lstCustomers.Count > 0)
            {
                txtCustomerCreditLimit.Text = TwoDecimalConversion(lstCustomers[0].Credit_Limit.ToString());
                txtCustomerOutStanding.Text = TwoDecimalConversion(lstCustomers[0].Outstanding_Amount.ToString());
                CanBillUpTo = Convert.ToDouble(lstCustomers[0].Credit_Limit) - Convert.ToDouble(lstCustomers[0].Outstanding_Amount);                
            }

            if (lstCustomers[0].OSCreditLimiStatus.ToString() == "ERROR")
            {
                txtCanBillUpTo.Text = "0";
            }
            else
            {
                txtCanBillUpTo.Text = TwoDecimalConversion(CanBillUpTo.ToString());
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
                UpdPanelGrid.Update();

                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                btnAdd.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void cmbCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetCustomerDetails(strBranchCode, cmbCustomerName.SelectedValue);

                IMPALLibrary.Masters.CustomerDetails.CustomerDetails oDtls = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                HdnIndHexCust.Value = oDtls.GetCustomersIndHex(ddlBranch.SelectedValue, cmbCustomerName.SelectedValue);

                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                if (cmbCustomerName.SelectedValue.ToString() == "0")
                {
                    divCustomerInfo.Style.Add("display", "none");                    
                    btnAdd.Enabled = false;
                    cmbCustomerName.Enabled = true;                    
                }
                else
                {
                    divCustomerInfo.Style.Add("display", "block");
                    btnAdd.Enabled = true;
                    cmbCustomerName.Enabled = false;
                    btnAdd.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerSalesReqEntity CustomerReqEntity = new CustomerSalesReqEntity();
                CustomerReqEntity.Items = new List<CustomerSalesReqItem>();

                CustomerReqEntity.CustomerSalesReqNumber = "";
                CustomerReqEntity.BranchCode = ddlBranch.SelectedValue.ToString();
                CustomerReqEntity.CustomerCode = cmbCustomerName.SelectedValue.ToString();

                string SuppPartNo = string.Empty;
                CustomerSalesReqItem CustomerReqItem = new CustomerSalesReqItem();
                int iNoofRows = 0;
                int SNo = 0;

                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        string filter = "Item_Code <> ''";
                        string filter1 = "Supplier_Line <>''";
                        DataView view = new DataView(dt);
                        view.RowFilter = filter;
                        view.RowFilter = filter1;
                        dt = view.ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dt.Rows.Count; i++)
                            {
                                CustomerReqItem = new CustomerSalesReqItem();
                                SNo += 1;

                                CustomerReqItem.SupplierCode = dt.Rows[i - 1]["Supplier_Line"].ToString().Substring(0,3);
                                CustomerReqItem.SupplierPartNumber = dt.Rows[i - 1]["Part_Number"].ToString();
                                CustomerReqItem.ItemCode = dt.Rows[i - 1]["Item_Code"].ToString();
                                CustomerReqItem.PackingQuantity = dt.Rows[i - 1]["Packing_Qty"].ToString();
                                CustomerReqItem.Quantity = dt.Rows[i - 1]["Req_Qty"].ToString();
                                CustomerReqItem.ValidDays = dt.Rows[i - 1]["Valid_Days"].ToString();
                                CustomerReqItem.ListPrice = TwoDecimalConversion(dt.Rows[i - 1]["List_Price"].ToString());
                                CustomerReqItem.MinOrdQuantity = dt.Rows[i - 1]["Min_Ord_Qty"].ToString();
                                CustomerReqItem.SNO = SNo.ToString();
                                CustomerReqEntity.Items.Add(CustomerReqItem);
                                SuppPartNo = dt.Rows[i - 1]["Part_Number"].ToString();
                            }
                        }
                    }
                }

                foreach (GridViewRow gr in grvItemDetails.Rows)
                {
                    CustomerReqItem = new CustomerSalesReqItem();

                    DropDownList ddlSupplierName = (DropDownList)gr.Cells[0].FindControl("ddlSupplierName");
                    if (ddlSupplierName.Visible)
                    {
                        SNo += 1;
                        TextBox lblSupplier = (TextBox)gr.Cells[0].FindControl("lblSupplier");
                        DropDownList ddlSupplierPartNo = (DropDownList)gr.Cells[1].FindControl("ddlSupplierPartNo");
                        TextBox txtItemCode = (TextBox)gr.Cells[1].FindControl("txtItemCode");
                        Button btnSearch = (Button)gr.Cells[1].FindControl("btnSearch");
                        TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                        TextBox txtValidDays = (TextBox)gr.Cells[5].FindControl("txtValidDays");
                        TextBox txtListPrice = (TextBox)gr.Cells[6].FindControl("txtListPrice");

                        CustomerReqItem.SupplierCode = ddlSupplierName.SelectedValue.ToString().Substring(0, 3);
                        CustomerReqItem.SupplierPartNumber = ddlSupplierPartNo.SelectedItem.Text;
                        CustomerReqItem.ItemCode = ddlSupplierPartNo.SelectedValue.ToString();
                        CustomerReqItem.Quantity = txtQuantity.Text;
                        CustomerReqItem.ValidDays = txtValidDays.Text;
                        CustomerReqItem.ListPrice = TwoDecimalConversion(txtListPrice.Text);
                        CustomerReqItem.SNO = SNo.ToString();
                        btnSearch.Visible = false;

                        if (SuppPartNo != ddlSupplierPartNo.SelectedItem.Text)
                            CustomerReqEntity.Items.Add(CustomerReqItem);
                    }
                }

                BtnSubmit.Enabled = false;
                grvItemDetails.Enabled = false;
                cmbCustomerName.Enabled = false;
                upHeader.Update();
                UpdPanelGrid.Update();

                SalesTransactions salesTransactions = new SalesTransactions();
                int result = salesTransactions.AddNewCustomerReqEntry(ref CustomerReqEntity);

                if ((CustomerReqEntity.ErrorMsg == string.Empty) && (CustomerReqEntity.ErrorCode == "0"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Details Submitted Successfully');", true);
                    txtCustomerReqNumber.Text = CustomerReqEntity.CustomerSalesReqNumber;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + CustomerReqEntity.ErrorMsg + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        private List<Supplier> GetAllSuppliers()
        {
            SalesItem objItem = new SalesItem();            
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliers();
            return lstSuppliers;
        }

        private List<Supplier> GetAllSuppliersIndHex()
        {
            SalesItem objItem = new SalesItem();
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliersIndHex();
            return lstSuppliers;
        } 

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Line", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Part_Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Packing_Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Req_Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Valid_Days", typeof(string)));
            dt.Columns.Add(new DataColumn("List_Price", typeof(string)));
            dt.Columns.Add(new DataColumn("Min_Ord_Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_Desc", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Supplier_Name"] = string.Empty;
                dr["Supplier_Line"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["Part_Number"] = string.Empty;
                dr["Packing_Qty"] = string.Empty;
                dr["Req_Qty"] = string.Empty;
                dr["Valid_Days"] = string.Empty;
                dr["List_Price"] = string.Empty;
                dr["Min_Ord_Qty"] = string.Empty;
                dr["Item_Desc"] = string.Empty;
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

        private void BindGrid()
        {
            foreach (GridViewRow gr in grvItemDetails.Rows)
            {
                DropDownList ddlSupplierName = (DropDownList)gr.Cells[1].FindControl("ddlSupplierName");

                if (HdnIndHexCust.Value == "0" || HdnIndHexCust.Value == "N")
                    ddlSupplierName.DataSource = GetAllSuppliers();
                else
                    ddlSupplierName.DataSource = GetAllSuppliersIndHex();

                ddlSupplierName.DataValueField = "SupplierCode";
                ddlSupplierName.DataTextField = "SupplierName";
                ddlSupplierName.DataBind();

                TextBox txtItemCode = (TextBox)gr.Cells[4].FindControl("txtItemCode");
                TextBox txtSupplierPartNo = (TextBox)gr.Cells[2].FindControl("txtSupplierPartNo");
            }
        }

        private void FreezeOrUnFreezeButtons(bool Fzflag)
        {
            Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
            btnAdd.Enabled = Fzflag;
            BtnSubmit.Enabled = Fzflag;
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
                            DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("ddlSupplierPartNo");
                            TextBox txtItemCode = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("txtItemCode");
                            Button btnSearch = (Button)grvItemDetails.Rows[iNoofRows - 1].Cells[1].FindControl("btnSearch");
                            TextBox txtPackingQuantity = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("txtPackingQuantity");
                            TextBox txtQuantity = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[3].FindControl("txtQuantity");
                            TextBox txtValidDays = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtValidDays");
                            TextBox txtListPrice = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txtListPrice");
                            TextBox txtMinOrdQty = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[6].FindControl("txtMinOrdQty");
                            TextBox txtItemDescription = (TextBox)grvItemDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtItemDescription");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            if (ddlSupplierName.Visible)
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = ddlSupplierName.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["Supplier_Line"] = ddlSupplierName.SelectedItem.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i - 1]["Supplier_Name"] = lblSupplier.Text.ToString();
                            }

                            if (ddlSupplierPartNo.SelectedItem != null)
                            {
                                dtCurrentTable.Rows[i - 1]["Item_Code"] = ddlSupplierPartNo.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["Part_Number"] = ddlSupplierPartNo.SelectedItem.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["Packing_Qty"] = txtPackingQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Req_Qty"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Valid_Days"] = txtValidDays.Text;
                            dtCurrentTable.Rows[i - 1]["List_Price"] = TwoDecimalConversion(txtListPrice.Text);
                            dtCurrentTable.Rows[i - 1]["Min_Ord_Qty"] = txtMinOrdQty.Text;
                            dtCurrentTable.Rows[i - 1]["Item_Desc"] = txtItemDescription.Text;
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
                        DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[i - 1].Cells[1].FindControl("ddlSupplierPartNo");
                        TextBox txtItemCode = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtItemCode");
                        TextBox txtSupplierPartNo = (TextBox)grvItemDetails.Rows[i - 1].Cells[1].FindControl("txtSupplierPartNo");
                        Button btnSearch = (Button)grvItemDetails.Rows[i - 1].Cells[1].FindControl("btnSearch");
                        TextBox txtPackingQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtPackingQuantity");
                        TextBox txtQuantity = (TextBox)grvItemDetails.Rows[i - 1].Cells[3].FindControl("txtQuantity");
                        TextBox txtValidDays = (TextBox)grvItemDetails.Rows[i - 1].Cells[4].FindControl("txtValidDays");
                        TextBox txtListPrice = (TextBox)grvItemDetails.Rows[i - 1].Cells[5].FindControl("txtListPrice");
                        TextBox txtMinOrdQty = (TextBox)grvItemDetails.Rows[i - 1].Cells[6].FindControl("txtMinOrdQty");
                        TextBox txtItemDescription = (TextBox)grvItemDetails.Rows[i - 1].Cells[7].FindControl("txtItemDescription");

                        txtSupplierPartNo.Text = dt.Rows[i - 1]["Part_Number"].ToString();
                        lblSupplier.Text = dt.Rows[i - 1]["Supplier_Line"].ToString();
                        txtItemCode.Text = dt.Rows[i - 1]["Item_Code"].ToString();
                        txtItemCode.Visible = true;
                        txtPackingQuantity.Text = dt.Rows[i - 1]["Packing_Qty"].ToString();
                        txtQuantity.Text = dt.Rows[i - 1]["Req_Qty"].ToString();
                        txtValidDays.Text = dt.Rows[i - 1]["Valid_Days"].ToString();
                        txtListPrice.Text = TwoDecimalConversion(dt.Rows[i - 1]["List_Price"].ToString());
                        txtMinOrdQty.Text = dt.Rows[i - 1]["Min_Ord_Qty"].ToString();
                        txtItemDescription.Text = dt.Rows[i - 1]["Item_Desc"].ToString();
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
                DropDownList ddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlSupplierPartNo");
                TextBox txtItemCode = (TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtItemCode");
                Button btnSearch = (Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch");

                if (index != grvItemDetails.Rows.Count - 1)
                {
                    ((DropDownList)grvItemDetails.Rows[index].Cells[0].FindControl("ddlSupplierName")).Visible = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("lblSupplier")).Visible = true;
                    ((DropDownList)grvItemDetails.Rows[index].Cells[1].FindControl("ddlSupplierPartNo")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtValidDays")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                }
                else
                {
                    ((TextBox)grvItemDetails.Rows[index].Cells[0].FindControl("lblSupplier")).Visible = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((Button)grvItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtQuantity")).Enabled = false;
                    ((TextBox)grvItemDetails.Rows[index].Cells[1].FindControl("txtValidDays")).Enabled = false;
                }
            }
            txtHdnGridCtrls.Text = sb.ToString();
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
                DropDownList ddlSupplierPartNo = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierPartNo");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtValidDays = (TextBox)grdrDropDownRow.FindControl("txtValidDays");
                TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");
                TextBox txtPackingQuantity = (TextBox)grdrDropDownRow.FindControl("txtPackingQuantity");
                TextBox txtMinOrdQty = (TextBox)grdrDropDownRow.FindControl("txtMinOrdQty");

                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNo.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtPackingQuantity.Text = "";                    
                    txtQuantity.Text = "";
                    txtValidDays.Text = "";
                    txtListPrice.Text = "";
                    txtMinOrdQty.Text = "";
                    txtQuantity.Enabled = false;
                    txtValidDays.Enabled = false;
                    txtItemCode.Focus();
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        SalesTransactions salesItem = new SalesTransactions();
                        List<CustomerSalesReqItem> lstSuppliers = new List<CustomerSalesReqItem>();
                        ListItem firstListItem = new ListItem();

                        if (HdnIndHexCust.Value == "0")
                            lstSuppliers = salesItem.GetCustomerSalesReqItems(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text);
                        else  if (HdnIndHexCust.Value == "N")
                            lstSuppliers = salesItem.GetCustomerSalesReqItemsIndHexReg(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text);
                        else
                            lstSuppliers = salesItem.GetCustomerSalesReqItemsIndHex(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text);
                        
                        ddlSupplierPartNo.DataSource = lstSuppliers;
                        ddlSupplierPartNo.Items.Add(firstListItem);
                        ddlSupplierPartNo.DataTextField = "SupplierPartNumber"; //"supplier_part_number";
                        ddlSupplierPartNo.DataValueField = "ItemCode";
                        ddlSupplierPartNo.DataBind();
                        ddlSupplierPartNo.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                        ddlSupplierPartNo.Focus();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSupplierPartNo = (DropDownList)sender;

                if (ddlSupplierPartNo.SelectedItem.Value != "0")
                {
                    bool isExisting = CheckExisting(ddlSupplierPartNo.SelectedValue);

                    if (isExisting)
                    {
                        SalesTransactions salesItem = new SalesTransactions();
                        List<SalesItem> lstItemPrice = new List<SalesItem>();

                        GridViewRow grdrDropDownRow = ((GridViewRow)ddlSupplierPartNo.Parent.Parent);
                        TextBox txtSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                        TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                        TextBox txtPackingQuantity = (TextBox)grdrDropDownRow.FindControl("txtPackingQuantity");
                        TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                        TextBox txtValidDays = (TextBox)grdrDropDownRow.FindControl("txtValidDays");
                        TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");
                        TextBox txtMinOrdQty = (TextBox)grdrDropDownRow.FindControl("txtMinOrdQty");
                        TextBox txtItemDescription = (TextBox)grdrDropDownRow.FindControl("txtItemDescription");

                        int HSNCode = salesItem.CheckHSNCodeItem(ddlSupplierPartNo.SelectedItem.Value, Session["BranchCode"].ToString());

                        if (HSNCode > 0)
                        {
                            lstItemPrice = salesItem.GetBranchItemPriceBO(ddlSupplierPartNo.SelectedItem.Value, Session["BranchCode"].ToString());

                            if (lstItemPrice.Count > 0)
                            {
                                txtSupplierPartNo.Text = ddlSupplierPartNo.SelectedItem.Text;
                                txtItemCode.Text = ddlSupplierPartNo.SelectedItem.Value;
                                txtPackingQuantity.Text = lstItemPrice[0].PackingQuantity;
                                txtListPrice.Text = TwoDecimalConversion(lstItemPrice[0].ListPrice.ToString());
                                txtMinOrdQty.Text = lstItemPrice[0].MinOrdQuantity;
                                txtItemDescription.Text = lstItemPrice[0].ItemDescription;
                                txtQuantity.Enabled = true;
                                txtValidDays.Text = "30";
                                txtValidDays.Enabled = true;
                                txtQuantity.Focus();
                            }
                            else
                            {
                                txtQuantity.Enabled = false;
                                txtValidDays.Text = "";
                                txtValidDays.Enabled = false;

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item is not available in Branch Item Price');", true);
                            }
                        }
                        else
                        {
                            txtQuantity.Enabled = false;
                            txtValidDays.Text = "";
                            txtValidDays.Enabled = false;

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('HSN Code is not available for this Item');", true);
                        }
                    }
                    else
                    {
                        GridViewRow grdrDropDownRow = ((GridViewRow)ddlSupplierPartNo.Parent.Parent);
                        TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                        TextBox txtValidDays = (TextBox)grdrDropDownRow.FindControl("txtValidDays");
                        txtQuantity.Text = "";
                        txtValidDays.Text = "";
                        txtQuantity.Enabled = false;
                        txtValidDays.Enabled = false;
                        
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Record already exists...');", true);
                        ddlSupplierPartNo.Focus();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
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
                DropDownList ddlSupplierPartNo = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierPartNo");
                TextBox txtQuantity = (TextBox)grdrDropDownRow.FindControl("txtQuantity");
                TextBox txtValidDays = (TextBox)grdrDropDownRow.FindControl("txtValidDays");
                TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");

                ddlSupplierPartNo.Visible = false;
                txtCurrentSearch.Visible = true;
                btnSearch.Text = "Search";
                txtItemCode.Text = "";
                txtCurrentSearch.Text = "";
                txtQuantity.Text = "";
                txtValidDays.Text = "";
                txtListPrice.Text = "";

                Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                btnAdd.Focus();

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
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void grvItemDetails_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlSupplierName = (DropDownList)e.Row.Cells[1].FindControl("ddlSupplierName");

                        if (HdnIndHexCust.Value == "0" || HdnIndHexCust.Value == "N")
                            ddlSupplierName.DataSource = GetAllSuppliers();
                        else
                            ddlSupplierName.DataSource = GetAllSuppliersIndHex();

                        ddlSupplierName.DataValueField = "SupplierCode";
                        ddlSupplierName.DataTextField = "SupplierName";
                        ddlSupplierName.DataBind();
                        ddlSupplierName.Focus();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                HdnIndHexCust.Value = "0";
                Server.ClearError();
                Response.Redirect("CustomerSalesReq.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
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
                        var strddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierPartNo");
                        var strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        var strPackingQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtPackingQuantity");
                        var strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        var strValidDays = (TextBox)grvItemDetails.Rows[i].FindControl("txtValidDays");
                        var strListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtListPrice");
                        var strMinOrdQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtMinOrdQty");
                        var strItemDescription = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemDescription");

                        dr["SNo"] = strSNo.Text;

                        if (strSupplier.Text != "")
                        {
                            List<Supplier> supplier = new List<Supplier>();

                            if (HdnIndHexCust.Value == "0" || HdnIndHexCust.Value == "N")
                                supplier = GetAllSuppliers();
                            else
                                supplier = GetAllSuppliersIndHex();

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

                        dr["Supplier_Line"] = strSupplier.Text;
                        dr["Item_Code"] = strItemCode.Text;
                        dr["Part_Number"] = strSupplierPartNo.Text;
                        dr["Packing_Qty"] = strPackingQty.Text;
                        dr["Req_Qty"] = strQty.Text;
                        dr["Valid_Days"] = strValidDays.Text;
                        dr["List_Price"] = TwoDecimalConversion(strListPrice.Text);
                        dr["Min_Ord_Qty"] = strMinOrdQty.Text;
                        dr["Item_Desc"] = strItemDescription.Text;
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
                        DropDownList strddlSupplierPartNo = (DropDownList)grvItemDetails.Rows[i].FindControl("ddlSupplierPartNo");
                        TextBox strSupplierPartNo = (TextBox)grvItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                        TextBox strPackingQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtPackingQuantity");
                        TextBox strQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtQuantity");
                        TextBox strValidDays = (TextBox)grvItemDetails.Rows[i].FindControl("txtValidDays");
                        TextBox strListPrice = (TextBox)grvItemDetails.Rows[i].FindControl("txtListPrice");
                        TextBox strMinOrdQty = (TextBox)grvItemDetails.Rows[i].FindControl("txtMinOrdQty");
                        Button btnSearch = (Button)grvItemDetails.Rows[i].FindControl("btnSearch");
                        TextBox strItemDescription = (TextBox)grvItemDetails.Rows[i].FindControl("txtItemDescription");

                        if (HdnIndHexCust.Value == "0" || HdnIndHexCust.Value == "N")
                            strSupplierName.DataSource = GetAllSuppliers();
                        else
                            strSupplierName.DataSource = GetAllSuppliersIndHex();

                        strSupplierName.DataValueField = "SupplierCode";
                        strSupplierName.DataTextField = "SupplierName";
                        strSupplierName.DataBind();

                        strSupplierName.SelectedValue = dt.Rows[i]["Supplier_Name"].ToString();
                        strSupplier.Text = dt.Rows[i]["Supplier_Line"].ToString();
                        strItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                        strSupplierPartNo.Text = dt.Rows[i]["Part_Number"].ToString();
                        strPackingQty.Text = dt.Rows[i]["Packing_Qty"].ToString();
                        strQty.Text = dt.Rows[i]["Req_Qty"].ToString();
                        strValidDays.Text = dt.Rows[i]["Valid_Days"].ToString();
                        strListPrice.Text = TwoDecimalConversion(dt.Rows[i]["List_Price"].ToString());
                        strMinOrdQty.Text = dt.Rows[i]["Min_Ord_Qty"].ToString();
                        strItemDescription.Text = dt.Rows[i]["Item_Desc"].ToString();
                        strSupplier.Visible = false;

                        if (dt.Rows.Count > 1 && i < dt.Rows.Count - 1)
                        {
                            btnSearch.Visible = false;
                            strddlSupplierPartNo.Visible = false;
                            strSupplier.Visible = true;
                            strSupplierPartNo.Visible = true;                            
                            strSupplierName.Visible = false;
                            strSupplierPartNo.Enabled = false;
                            strQty.Enabled = false;
                            strValidDays.Enabled = false;
                        }
                        else
                        {
                            if (dt.Rows[i]["Item_Code"].ToString() != "")
                            {
                                btnSearch.Text = "Reset";
                                strddlSupplierPartNo.Visible = true;
                                strSupplierPartNo.Visible = false;
                                strSupplier.Visible = false;

                                SalesTransactions salesItem = new SalesTransactions();
                                List<CustomerSalesReqItem> lstSuppliers = new List<CustomerSalesReqItem>();
                                ListItem firstListItem = new ListItem();


                                if (HdnIndHexCust.Value == "0")
                                    lstSuppliers = salesItem.GetCustomerSalesReqItems(strSupplierName.SelectedItem.Value, strSupplierPartNo.Text);
                                else if (HdnIndHexCust.Value == "N")
                                    lstSuppliers = salesItem.GetCustomerSalesReqItemsIndHexReg(strSupplierName.SelectedItem.Value, strSupplierPartNo.Text);
                                else
                                    lstSuppliers = salesItem.GetCustomerSalesReqItemsIndHex(strSupplierName.SelectedItem.Value, strSupplierPartNo.Text);
                                
                                strddlSupplierPartNo.DataSource = lstSuppliers;
                                strddlSupplierPartNo.Items.Add(firstListItem);
                                strddlSupplierPartNo.DataTextField = "SupplierPartNumber";
                                strddlSupplierPartNo.DataValueField = "ItemCode";
                                strddlSupplierPartNo.DataBind();

                                string strSupplierPartNumber = dt.Rows[i]["Part_Number"].ToString();

                                lstSuppliers = lstSuppliers.Where(a => a.SupplierPartNumber == strSupplierPartNumber).ToList();
                                if (lstSuppliers.Count > 0)
                                {
                                    strddlSupplierPartNo.SelectedValue = lstSuppliers[0].ItemCode;
                                }

                                strddlSupplierPartNo.SelectedItem.Text = strSupplierPartNumber;
                            }
                            else
                            {
                                btnSearch.Text = "Search";
                            }
                        }
                    }
                }

                hdnRowCnt.Value = dt.Rows.Count.ToString();
                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count.ToString();

                if (dt.Rows.Count == 0)
                {
                    FirstGridViewRow();
                }
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQuantity = (TextBox)sender;
                GridViewRow grdRow = ((GridViewRow)txtQuantity.Parent.Parent);
                TextBox txtValidDays = (TextBox)grdRow.FindControl("txtValidDays");

                if (txtQuantity.Text != null && txtQuantity.Text != "" && txtQuantity.Text != "0")
                {
                    txtValidDays.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void txtValidDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtValidDays = (TextBox)sender;

                if (txtValidDays.Text != null && txtValidDays.Text != "" && txtValidDays.Text != "0")
                {
                    Button btnAdd = (Button)grvItemDetails.FooterRow.FindControl("btnAdd");
                    btnAdd.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
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

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }
    }
}