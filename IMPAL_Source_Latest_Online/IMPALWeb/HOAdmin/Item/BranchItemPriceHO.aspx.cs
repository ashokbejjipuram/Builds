using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using IMPALLibrary.Transactions;

namespace IMPALWeb.HOAdmin.Item
{
    public partial class BranchItemPriceHO : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Zones oZone = new Zones();
                ddlZone.DataSource = oZone.GetAllZones();
                ddlZone.DataBind();
                ddlZone.Items.Insert(0, "--All--");
                ddlZone.Enabled = true;

                ddlState.Items.Insert(0, "--All--");
                ddlBranch.Items.Insert(0, "--All--");

                FirstGridViewRow();
            }

            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();            
        }

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                chkZone.Checked = false;
                chkState.Checked = false;
                chkBranch.Checked = false;

                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStatesOnline(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetAllStatesOnline();
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();
                chkZone.Checked = false;
                chkState.Checked = false;
                chkBranch.Checked = false;

                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchStateOnline(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchNew();
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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
                TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");

                ddlSupplierPartNo.Visible = false;
                txtCurrentSearch.Visible = true;
                btnSearch.Text = "Search";
                txtItemCode.Text = "";
                txtCurrentSearch.Text = "";
                txtListPrice.Text = "";

                if (ddlSupplierName.SelectedIndex != 0)
                {
                    txtCurrentSearch.Text = "";
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
                Log.WriteException(typeof(BranchItemPriceHO), exp);
            }
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
                TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");                
                TextBox txtPurchaseDiscount = (TextBox)grdrDropDownRow.FindControl("txtPurchaseDiscount");
                TextBox txtListLessDiscount = (TextBox)grdrDropDownRow.FindControl("txtListLessDiscount");
                TextBox txtCoupon = (TextBox)grdrDropDownRow.FindControl("txtCoupon");
                TextBox txtAfterCoupon = (TextBox)grdrDropDownRow.FindControl("txtAfterCoupon");
                TextBox txtCDPercentage = (TextBox)grdrDropDownRow.FindControl("txtCDPercentage");
                TextBox txtCDAmount = (TextBox)grdrDropDownRow.FindControl("txtCDAmount");
                TextBox txtAfterCD = (TextBox)grdrDropDownRow.FindControl("txtAfterCD");
                TextBox txtWCPercentage = (TextBox)grdrDropDownRow.FindControl("txtWCPercentage");
                TextBox txtWCAmount = (TextBox)grdrDropDownRow.FindControl("txtWCAmount");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtMRP = (TextBox)grdrDropDownRow.FindControl("txtMRP");

                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNo.Visible = false;
                    txtCurrentSearch.Visible = true;
                    btnSearch.Text = "Search";
                    txtItemCode.Text = "";
                    txtCurrentSearch.Text = "";
                    txtListPrice.Text = "";
                    txtPurchaseDiscount.Text = "";
                    txtListLessDiscount.Text = "";
                    txtCoupon.Text = "";
                    txtAfterCoupon.Text = "";
                    txtCDPercentage.Text = "";
                    txtCDAmount.Text = "";
                    txtAfterCD.Text = "";
                    txtWCPercentage.Text = "";
                    txtWCAmount.Text = "";
                    txtCostPrice.Text = "";
                    txtMRP.Text = "";
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        SalesTransactions salesItem = new SalesTransactions();
                        List<CustomerSalesReqItem> lstSuppliers = new List<CustomerSalesReqItem>();
                        ListItem firstListItem = new ListItem();

                        lstSuppliers = salesItem.GetCustomerSalesReqItems(ddlSupplierName.SelectedItem.Value, txtCurrentSearch.Text);

                        ddlSupplierPartNo.DataSource = lstSuppliers;
                        ddlSupplierPartNo.Items.Add(firstListItem);
                        ddlSupplierPartNo.DataTextField = "SupplierPartNumber";
                        ddlSupplierPartNo.DataValueField = "ItemCode";
                        ddlSupplierPartNo.DataBind();
                        ddlSupplierPartNo.Visible = true;
                        txtCurrentSearch.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void grdItemDetails_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlSupplierName = (DropDownList)e.Row.Cells[1].FindControl("ddlSupplierName");

                    SalesItem objItem = new SalesItem();
                    Suppliers suppliers = new Suppliers();
                    ddlSupplierName.DataSource = suppliers.GetAllSuppliers();
                    ddlSupplierName.DataValueField = "SupplierCode";
                    ddlSupplierName.DataTextField = "SupplierName";
                    ddlSupplierName.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Part_Number", typeof(string)));
            dt.Columns.Add(new DataColumn("ListPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("PurchaseDiscount", typeof(string)));
            dt.Columns.Add(new DataColumn("ListLessDiscount", typeof(string)));
            dt.Columns.Add(new DataColumn("Coupon", typeof(string)));
            dt.Columns.Add(new DataColumn("AfterCoupon", typeof(string)));
            dt.Columns.Add(new DataColumn("CDPercentage", typeof(string)));
            dt.Columns.Add(new DataColumn("CDAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("AfterCD", typeof(string)));
            dt.Columns.Add(new DataColumn("WareHousePercentage", typeof(string)));
            dt.Columns.Add(new DataColumn("WareHouseAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("CostPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnCostPrice", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Supplier_Name"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["Part_Number"] = string.Empty;
                dr["ListPrice"] = string.Empty;
                dr["PurchaseDiscount"] = string.Empty;
                dr["ListLessDiscount"] = string.Empty;
                dr["Coupon"] = string.Empty;
                dr["AfterCoupon"] = string.Empty;
                dr["CDPercentage"] = string.Empty;
                dr["CDAmount"] = string.Empty;
                dr["AfterCD"] = string.Empty;
                dr["WareHousePercentage"] = string.Empty;
                dr["WareHouseAmount"] = string.Empty;
                dr["CostPrice"] = string.Empty;
                dr["MRP"] = string.Empty;
                dr["hdnCostPrice"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdItemDetails.DataSource = dt;
            ViewState["GridRowCount"] = dt.Rows.Count.ToString();
            grdItemDetails.DataBind();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
                UpdPanelGrid.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
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
                            DropDownList ddlSupplierName = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlSupplierName");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlSupplierPartNo");
                            TextBox txtSupplierPartNo = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtSupplierPartNo");
                            Button btnSearch = (Button)grdItemDetails.Rows[iNoofRows - 1].FindControl("btnSearch");
                            TextBox txtListPrice = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtListPrice");
                            TextBox txtPurchaseDiscount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtPurchaseDiscount");
                            TextBox txtListLessDiscount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtListLessDiscount");
                            TextBox txtCoupon = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtCoupon");
                            TextBox txtAfterCoupon = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtAfterCoupon");
                            TextBox txtCDPercentage = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtCDPercentage");
                            TextBox txtCDAmount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtCDAmount");
                            TextBox txtAfterCD = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtAfterCD");
                            TextBox txtWCPercentage = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtWCPercentage");
                            TextBox txtWCAmount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtWCAmount");
                            TextBox txtCostPrice = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtCostPrice");
                            TextBox txtMRP = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtMRP");
                            TextBox txtItemCode = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtItemCode");
                            HiddenField hdnCostPrice = (HiddenField)grdItemDetails.Rows[i - 1].FindControl("hdnCostPrice");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["Supplier_Name"] = ddlSupplierName.SelectedValue;

                            if (ddlSupplierPartNo.SelectedItem != null)
                            {
                                dtCurrentTable.Rows[i - 1]["Item_Code"] = ddlSupplierPartNo.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["Part_Number"] = ddlSupplierPartNo.SelectedItem.Text;
                            }

                            dtCurrentTable.Rows[i - 1]["ListPrice"] = TwoDecimalConversion(txtListPrice.Text);
                            dtCurrentTable.Rows[i - 1]["PurchaseDiscount"] = TwoDecimalConversion(txtPurchaseDiscount.Text);
                            dtCurrentTable.Rows[i - 1]["ListLessDiscount"] = TwoDecimalConversion(txtListLessDiscount.Text);
                            dtCurrentTable.Rows[i - 1]["Coupon"] = TwoDecimalConversion(txtCoupon.Text);
                            dtCurrentTable.Rows[i - 1]["AfterCoupon"] = TwoDecimalConversion(txtAfterCoupon.Text);
                            dtCurrentTable.Rows[i - 1]["CDPercentage"] = TwoDecimalConversion(txtCDPercentage.Text);
                            dtCurrentTable.Rows[i - 1]["CDAmount"] = TwoDecimalConversion(txtCDAmount.Text);
                            dtCurrentTable.Rows[i - 1]["AfterCD"] = TwoDecimalConversion(txtAfterCD.Text);
                            dtCurrentTable.Rows[i - 1]["WareHousePercentage"] = TwoDecimalConversion(txtWCPercentage.Text);
                            dtCurrentTable.Rows[i - 1]["WareHouseAmount"] = TwoDecimalConversion(txtWCAmount.Text);
                            dtCurrentTable.Rows[i - 1]["CostPrice"] = TwoDecimalConversion(txtCostPrice.Text);
                            dtCurrentTable.Rows[i - 1]["MRP"] = TwoDecimalConversion(txtMRP.Text);
                            dtCurrentTable.Rows[i - 1]["hdnCostPrice"] = TwoDecimalConversion(hdnCostPrice.Value);
                            rowIndex++;
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    grdItemDetails.DataSource = dtCurrentTable;
                    grdItemDetails.DataBind();
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
                        DropDownList ddlSupplierName = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlSupplierName");
                        DropDownList ddlSupplierPartNo = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlSupplierPartNo");
                        TextBox txtSupplierPartNo = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtSupplierPartNo");
                        Button btnSearch = (Button)grdItemDetails.Rows[i - 1].FindControl("btnSearch");
                        TextBox txtListPrice = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtListPrice");
                        TextBox txtPurchaseDiscount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtPurchaseDiscount");
                        TextBox txtListLessDiscount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtListLessDiscount");
                        TextBox txtCoupon = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCoupon");
                        TextBox txtAfterCoupon = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtAfterCoupon");
                        TextBox txtCDPercentage = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCDPercentage");
                        TextBox txtCDAmount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCDAmount");
                        TextBox txtAfterCD = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtAfterCD");
                        TextBox txtWCPercentage = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtWCPercentage");
                        TextBox txtWCAmount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtWCAmount");
                        TextBox txtCostPrice = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCostPrice");
                        TextBox txtMRP = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtMRP");
                        TextBox txtItemCode = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtItemCode");
                        HiddenField hdnCostPrice = (HiddenField)grdItemDetails.Rows[i - 1].FindControl("hdnCostPrice");

                        ddlSupplierName.SelectedValue = dt.Rows[i - 1]["Supplier_Name"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i - 1]["Part_Number"].ToString();
                        txtListPrice.Text = dt.Rows[i - 1]["ListPrice"].ToString();
                        txtPurchaseDiscount.Text = dt.Rows[i - 1]["PurchaseDiscount"].ToString();
                        txtListLessDiscount.Text = dt.Rows[i - 1]["ListLessDiscount"].ToString();
                        txtCoupon.Text = dt.Rows[i - 1]["Coupon"].ToString();
                        txtAfterCoupon.Text = dt.Rows[i - 1]["AfterCoupon"].ToString();
                        txtCDPercentage.Text = dt.Rows[i - 1]["CDPercentage"].ToString();
                        txtCDAmount.Text = dt.Rows[i - 1]["CDAmount"].ToString();
                        txtAfterCD.Text = dt.Rows[i - 1]["AfterCD"].ToString();
                        txtWCPercentage.Text = TwoDecimalConversion(dt.Rows[i - 1]["WareHousePercentage"].ToString());
                        txtWCAmount.Text = dt.Rows[i - 1]["WareHouseAmount"].ToString();
                        txtCostPrice.Text = dt.Rows[i - 1]["CostPrice"].ToString();
                        txtMRP.Text = dt.Rows[i - 1]["MRP"].ToString();
                        txtItemCode.Text = dt.Rows[i - 1]["Item_Code"].ToString();
                        hdnCostPrice.Value = dt.Rows[i - 1]["hdnCostPrice"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private void HideDllItemCodeDropDown()
        {
            for (int index = 0; index < grdItemDetails.Rows.Count; index++)
            {
                if (index != grdItemDetails.Rows.Count - 1)
                {
                    grdItemDetails.Rows[index].Enabled = false;
                    ((Button)grdItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Visible = false;
                    ((DropDownList)grdItemDetails.Rows[index].Cells[1].FindControl("ddlSupplierPartNo")).Visible = false;
                    ((TextBox)grdItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                }
                else
                {
                    grdItemDetails.Rows[index].Enabled = true;
                    ((TextBox)grdItemDetails.Rows[index].Cells[1].FindControl("txtSupplierPartNo")).Enabled = false;
                    ((Button)grdItemDetails.Rows[index].Cells[1].FindControl("btnSearch")).Enabled = false;
                }
            }
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSupplierPartNo = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSupplierPartNo.Parent.Parent);
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                TextBox txtSupplierPartno = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");

                if (ddlSupplierPartNo.SelectedIndex > 0)
                {
                    txtItemCode.Text = ddlSupplierPartNo.SelectedValue;
                    txtSupplierPartno.Text = ddlSupplierPartNo.SelectedItem.Text;

                }
                else
                {
                    txtItemCode.Text = "";
                    txtSupplierPartno.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CustomerSalesReq), exp);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                var zone = ddlZone.SelectedValue;
                var state = ddlState.SelectedValue;
                var branch = ddlBranch.SelectedValue;
                
                int result = 0;

                foreach (GridViewRow gr in grdItemDetails.Rows)
                {
                    DropDownList ddlSupplierName = (DropDownList)gr.FindControl("ddlSupplierName");                    
                    TextBox SupplierPartno = (TextBox)gr.FindControl("txtSupplierPartNo");
                    DropDownList ddlSupplierPartno = (DropDownList)gr.FindControl("ddlSupplierPartNo");
                    TextBox lPrice = (TextBox)gr.FindControl("txtListPrice");
                    HiddenField cPrice = (HiddenField)gr.FindControl("hdnCostPrice");
                    TextBox Mrp = (TextBox)gr.FindControl("txtMRP");
                    TextBox Itemcode = (TextBox)gr.FindControl("txtItemCode");

                    BranchItemPrices BIP = new BranchItemPrices();
                    result = BIP.AddNewBranchItemPriceHO(zone, state, branch, ddlSupplierName.SelectedValue, Itemcode.Text, SupplierPartno.Text, Convert.ToDecimal(lPrice.Text), Convert.ToDecimal(cPrice.Value), Convert.ToDecimal(Mrp.Text));
                }

                if (result == 1)
                {
                    chkZone.Enabled = false;
                    chkState.Enabled = false;
                    chkBranch.Enabled = false;
                    ddlZone.Enabled = false;
                    ddlState.Enabled = false;
                    ddlBranch.Enabled = false;                    
                    grdItemDetails.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnReset.Enabled = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Branch Item Price Details Updated Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("BranchItemPriceHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
    }
}