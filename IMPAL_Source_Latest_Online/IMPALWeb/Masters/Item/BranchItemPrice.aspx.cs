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
    public partial class BranchItemPrice : System.Web.UI.Page
    {
        BranchItemPrices BIP = new BranchItemPrices();
        private string strBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BranchCode"] != null)
            {
                strBranchCode = (string)Session["BranchCode"];
            }

            if (!IsPostBack)
            {
                FirstGridViewRow();

                Branches oBranches = new Branches();
                IMPALLibrary.Branch oBranchDetails = oBranches.GetBranchDetails(strBranchCode);
                if (oBranchDetails != null)
                {
                    ddlZone.Items.Add(new ListItem(oBranchDetails.Zone, oBranchDetails.ZoneCode));
                    ddlState.Items.Add(new ListItem(oBranchDetails.State, oBranchDetails.StateCode));
                    ddlBranch.Items.Add(new ListItem(oBranchDetails.BranchName, oBranchDetails.BranchCode));
                }
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
                DropDownList ddlPurchaseDiscount = (DropDownList)grdrDropDownRow.FindControl("ddlPurchaseDiscount");
                TextBox txtListLessDiscount = (TextBox)grdrDropDownRow.FindControl("txtListLessDiscount");
                DropDownList ddlCoupon = (DropDownList)grdrDropDownRow.FindControl("ddlCoupon");
                TextBox txtAfterCoupon = (TextBox)grdrDropDownRow.FindControl("txtAfterCoupon");
                DropDownList ddlCDPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlCDPercentage");
                TextBox txtCDAmount = (TextBox)grdrDropDownRow.FindControl("txtCDAmount");
                TextBox txtAfterCD = (TextBox)grdrDropDownRow.FindControl("txtAfterCD");
                DropDownList ddlWCPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlWCPercentage");
                TextBox txtWCAmount = (TextBox)grdrDropDownRow.FindControl("txtWCAmount");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtMRP = (TextBox)grdrDropDownRow.FindControl("txtMRP");

                ddlSupplierPartNo.Visible = false;
                txtCurrentSearch.Visible = true;
                btnSearch.Text = "Search";
                txtCurrentSearch.Text = "";

                txtItemCode.Text = "";
                txtListPrice.Text = "";
                ddlPurchaseDiscount.SelectedIndex = 0;
                txtListLessDiscount.Text = "0.00";
                ddlCoupon.SelectedIndex = 0;
                txtAfterCoupon.Text = "0.00";
                ddlCDPercentage.SelectedIndex = 0;
                txtCDAmount.Text = "0.00";
                txtAfterCD.Text = "0.00";
                ddlWCPercentage.SelectedIndex = 0;
                txtWCAmount.Text = "0.00";
                txtCostPrice.Text = "";
                txtMRP.Text = "";

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
                Log.WriteException(typeof(BranchItemPrice), exp);
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
                DropDownList ddlPurchaseDiscount = (DropDownList)grdrDropDownRow.FindControl("ddlPurchaseDiscount");
                TextBox txtListLessDiscount = (TextBox)grdrDropDownRow.FindControl("txtListLessDiscount");
                DropDownList ddlCoupon = (DropDownList)grdrDropDownRow.FindControl("ddlCoupon");
                TextBox txtAfterCoupon = (TextBox)grdrDropDownRow.FindControl("txtAfterCoupon");
                DropDownList ddlCDPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlCDPercentage");
                TextBox txtCDAmount = (TextBox)grdrDropDownRow.FindControl("txtCDAmount");
                TextBox txtAfterCD = (TextBox)grdrDropDownRow.FindControl("txtAfterCD");
                DropDownList ddlWCPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlWCPercentage");
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
                    ddlPurchaseDiscount.SelectedIndex = 0;
                    txtListLessDiscount.Text = "0.00";
                    ddlCoupon.SelectedIndex = 0;
                    txtAfterCoupon.Text = "0.00";
                    ddlCDPercentage.SelectedIndex = 0;
                    txtCDAmount.Text = "0.00";
                    txtAfterCD.Text = "0.00";
                    ddlWCPercentage.SelectedIndex = 0;
                    txtWCAmount.Text = "0.00";
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
                Log.WriteException(typeof(BranchItemPrice), exp);
            }
        }

        private bool CheckExisting(string partNumber)
        {
            foreach (GridViewRow gr in grdItemDetails.Rows)
            {
                TextBox txtItemCode = (TextBox)gr.Cells[3].FindControl("txtItemCode");

                if (partNumber == txtItemCode.Text)
                {
                    return false;
                }
            }

            return true;
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
                    ddlSupplierName.DataSource = suppliers.GetAllSuppliersBIP();
                    ddlSupplierName.DataValueField = "SupplierCode";
                    ddlSupplierName.DataTextField = "SupplierName";
                    ddlSupplierName.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BranchItemPrice), exp);
            }
        }

        protected void grdItemDetails_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();

                    if (grdItemDetails.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grdItemDetails.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            var strSNo = (TextBox)grdItemDetails.Rows[i].FindControl("txtSNo");
                            var strSupplierName = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlSupplierName");
                            var strddlSupplierPartNo = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlSupplierPartNo");
                            var strSupplierPartNo = (TextBox)grdItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                            var strListPrice = (TextBox)grdItemDetails.Rows[i].FindControl("txtListPrice");
                            var strPurchaseDiscount = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlPurchaseDiscount");
                            var strListLessDiscount = (TextBox)grdItemDetails.Rows[i].FindControl("txtListLessDiscount");
                            var strCoupon = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlCoupon");
                            var strAfterCoupon = (TextBox)grdItemDetails.Rows[i].FindControl("txtAfterCoupon");
                            var strCDPercentage = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlCDPercentage");
                            var strCDAmount = (TextBox)grdItemDetails.Rows[i].FindControl("txtCDAmount");
                            var strAfterCD = (TextBox)grdItemDetails.Rows[i].FindControl("txtAfterCD");
                            var strWCPercentage = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlWCPercentage");
                            var strWCAmount = (TextBox)grdItemDetails.Rows[i].FindControl("txtWCAmount");
                            var strCostPrice = (TextBox)grdItemDetails.Rows[i].FindControl("txtCostPrice");
                            var strMRP = (TextBox)grdItemDetails.Rows[i].FindControl("txtMRP");
                            var strItemCode = (TextBox)grdItemDetails.Rows[i].FindControl("txtItemCode");

                            dr["SNo"] = strSNo.Text;
                            dr["Supplier_Name"] = strSupplierName.SelectedValue;
                            dr["Part_Number"] = strSupplierPartNo.Text;
                            dr["ListPrice"] = strListPrice.Text;
                            dr["PurchaseDiscount"] = strPurchaseDiscount.Text;
                            dr["ListLessDiscount"] = strListLessDiscount.Text;
                            dr["Coupon"] = strCoupon.SelectedValue;
                            dr["AfterCoupon"] = strAfterCoupon.Text;
                            dr["CDPercentage"] = strCDPercentage.SelectedValue;
                            dr["CDAmount"] = strCDAmount.Text;
                            dr["AfterCD"] = strAfterCD.Text;
                            dr["WareHousePercentage"] = strWCPercentage.SelectedValue;
                            dr["WareHouseAmount"] = strWCAmount.Text;
                            dr["CostPrice"] = strCostPrice.Text;
                            dr["MRP"] = strMRP.Text;
                            dr["Item_Code"] = strItemCode.Text;
                            dt.Rows.Add(dr);
                        }
                    }

                    dt.Rows.RemoveAt(e.RowIndex);
                    grdItemDetails.DataSource = dt;
                    grdItemDetails.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlSupplierName = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlSupplierName");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlSupplierPartNo");
                            TextBox txtSupplierPartNo = (TextBox)grdItemDetails.Rows[i].FindControl("txtSupplierPartNo");
                            Button btnSearch = (Button)grdItemDetails.Rows[i].FindControl("btnSearch");
                            TextBox txtListPrice = (TextBox)grdItemDetails.Rows[i].FindControl("txtListPrice");
                            DropDownList ddlPurchaseDiscount = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlPurchaseDiscount");
                            TextBox txtListLessDiscount = (TextBox)grdItemDetails.Rows[i].FindControl("txtListLessDiscount");
                            DropDownList ddlCoupon = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlCoupon");
                            TextBox txtAfterCoupon = (TextBox)grdItemDetails.Rows[i].FindControl("txtAfterCoupon");
                            DropDownList ddlCDPercentage = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlCDPercentage");
                            TextBox txtCDAmount = (TextBox)grdItemDetails.Rows[i].FindControl("txtCDAmount");
                            TextBox txtAfterCD = (TextBox)grdItemDetails.Rows[i].FindControl("txtAfterCD");
                            DropDownList ddlWCPercentage = (DropDownList)grdItemDetails.Rows[i].FindControl("ddlWCPercentage");
                            TextBox txtWCAmount = (TextBox)grdItemDetails.Rows[i].FindControl("txtWCAmount");
                            TextBox txtCostPrice = (TextBox)grdItemDetails.Rows[i].FindControl("txtCostPrice");
                            TextBox txtMRP = (TextBox)grdItemDetails.Rows[i].FindControl("txtMRP");
                            TextBox txtItemCode = (TextBox)grdItemDetails.Rows[i].FindControl("txtItemCode");
                            HiddenField hdnCostPrice = (HiddenField)grdItemDetails.Rows[i].FindControl("hdnCostPrice");

                            List<BranchItemPriceDiscDtls> lstDiscDtls = new List<BranchItemPriceDiscDtls>();
                            lstDiscDtls = BIP.GetPurchaseDiscountDetails(Session["BranchCode"].ToString(), dt.Rows[i]["Supplier_Name"].ToString(), dt.Rows[i]["Part_Number"].ToString());
                            ddlPurchaseDiscount.DataSource = lstDiscDtls;
                            ddlPurchaseDiscount.DataTextField = "DiscountDesc";
                            ddlPurchaseDiscount.DataValueField = "DiscountPercentage";
                            ddlPurchaseDiscount.DataBind();
                            lstDiscDtls = null;

                            lstDiscDtls = BIP.GetCouponDetails(Session["BranchCode"].ToString(), dt.Rows[i]["Supplier_Name"].ToString(), dt.Rows[i]["Part_Number"].ToString());
                            ddlCoupon.DataSource = lstDiscDtls;
                            ddlCoupon.DataTextField = "DiscountDesc";
                            ddlCoupon.DataValueField = "DiscountPercentage";
                            ddlCoupon.DataBind();
                            lstDiscDtls = null;

                            lstDiscDtls = BIP.GetCDPercentageDetails(Session["BranchCode"].ToString(), dt.Rows[i]["Supplier_Name"].ToString(), dt.Rows[i]["Part_Number"].ToString());
                            ddlCDPercentage.DataSource = lstDiscDtls;
                            ddlCDPercentage.DataTextField = "DiscountDesc";
                            ddlCDPercentage.DataValueField = "DiscountPercentage";
                            ddlCDPercentage.DataBind();
                            lstDiscDtls = null;

                            lstDiscDtls = BIP.GetWCPercentageDetails(Session["BranchCode"].ToString(), dt.Rows[i]["Supplier_Name"].ToString(), dt.Rows[i]["Part_Number"].ToString());
                            ddlWCPercentage.DataSource = lstDiscDtls;
                            ddlWCPercentage.DataTextField = "DiscountDesc";
                            ddlWCPercentage.DataValueField = "DiscountPercentage";
                            ddlWCPercentage.DataBind();
                            lstDiscDtls = null;

                            ddlSupplierName.SelectedValue = dt.Rows[i]["Supplier_Name"].ToString();
                            txtSupplierPartNo.Text = dt.Rows[i]["Part_Number"].ToString();
                            txtListPrice.Text = dt.Rows[i]["ListPrice"].ToString();
                            ddlPurchaseDiscount.SelectedValue = dt.Rows[i]["PurchaseDiscount"].ToString();
                            txtListLessDiscount.Text = dt.Rows[i]["ListLessDiscount"].ToString();
                            ddlCoupon.SelectedValue = dt.Rows[i]["Coupon"].ToString();
                            txtAfterCoupon.Text = dt.Rows[i]["AfterCoupon"].ToString();
                            ddlCDPercentage.SelectedValue = dt.Rows[i]["CDPercentage"].ToString();
                            txtCDAmount.Text = dt.Rows[i]["CDAmount"].ToString();
                            txtAfterCD.Text = dt.Rows[i]["AfterCD"].ToString();
                            ddlWCPercentage.SelectedValue = dt.Rows[i]["WareHousePercentage"].ToString();
                            txtWCAmount.Text = dt.Rows[i]["WareHouseAmount"].ToString();
                            txtCostPrice.Text = dt.Rows[i]["CostPrice"].ToString();
                            txtMRP.Text = dt.Rows[i]["MRP"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                            hdnCostPrice.Value = dt.Rows[i]["hdnCostPrice"].ToString();
                        }
                    }

                    ViewState["CurrentTable"] = dt;
                    ViewState["GridRowCount"] = dt.Rows.Count.ToString();

                    if (dt.Rows.Count == 0)
                    {
                        FirstGridViewRow();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BranchItemPrice), exp);
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
                Log.WriteException(typeof(BranchItemPrice), exp);
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
                            DropDownList ddlPurchaseDiscount = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlPurchaseDiscount");
                            TextBox txtListLessDiscount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtListLessDiscount");
                            DropDownList ddlCoupon = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlCoupon");
                            TextBox txtAfterCoupon = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtAfterCoupon");
                            DropDownList ddlCDPercentage = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlCDPercentage");
                            TextBox txtCDAmount = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtCDAmount");
                            TextBox txtAfterCD = (TextBox)grdItemDetails.Rows[iNoofRows - 1].FindControl("txtAfterCD");
                            DropDownList ddlWCPercentage = (DropDownList)grdItemDetails.Rows[iNoofRows - 1].FindControl("ddlWCPercentage");
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

                            dtCurrentTable.Rows[i - 1]["ListPrice"] = FourDecimalConversion(txtListPrice.Text);
                            dtCurrentTable.Rows[i - 1]["PurchaseDiscount"] = FourDecimalConversion(ddlPurchaseDiscount.SelectedValue);
                            dtCurrentTable.Rows[i - 1]["ListLessDiscount"] = FourDecimalConversion(txtListLessDiscount.Text);
                            dtCurrentTable.Rows[i - 1]["Coupon"] = FourDecimalConversion(ddlCoupon.SelectedValue);
                            dtCurrentTable.Rows[i - 1]["AfterCoupon"] = FourDecimalConversion(txtAfterCoupon.Text);
                            dtCurrentTable.Rows[i - 1]["CDPercentage"] = TwoDecimalConversion(ddlCDPercentage.SelectedValue);
                            dtCurrentTable.Rows[i - 1]["CDAmount"] = FourDecimalConversion(txtCDAmount.Text);
                            dtCurrentTable.Rows[i - 1]["AfterCD"] = FourDecimalConversion(txtAfterCD.Text);
                            dtCurrentTable.Rows[i - 1]["WareHousePercentage"] = FourDecimalConversion(ddlWCPercentage.SelectedValue);
                            dtCurrentTable.Rows[i - 1]["WareHouseAmount"] = FourDecimalConversion(txtWCAmount.Text);
                            dtCurrentTable.Rows[i - 1]["CostPrice"] = FourDecimalConversion(txtCostPrice.Text);
                            dtCurrentTable.Rows[i - 1]["MRP"] = FourDecimalConversion(txtMRP.Text);
                            dtCurrentTable.Rows[i - 1]["hdnCostPrice"] = FourDecimalConversion(hdnCostPrice.Value);
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
                        DropDownList ddlPurchaseDiscount = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlPurchaseDiscount");
                        TextBox txtListLessDiscount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtListLessDiscount");
                        DropDownList ddlCoupon = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlCoupon");
                        TextBox txtAfterCoupon = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtAfterCoupon");
                        DropDownList ddlCDPercentage = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlCDPercentage");
                        TextBox txtCDAmount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCDAmount");
                        TextBox txtAfterCD = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtAfterCD");
                        DropDownList ddlWCPercentage = (DropDownList)grdItemDetails.Rows[i - 1].FindControl("ddlWCPercentage");
                        TextBox txtWCAmount = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtWCAmount");
                        TextBox txtCostPrice = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtCostPrice");
                        TextBox txtMRP = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtMRP");
                        TextBox txtItemCode = (TextBox)grdItemDetails.Rows[i - 1].FindControl("txtItemCode");
                        HiddenField hdnCostPrice = (HiddenField)grdItemDetails.Rows[i - 1].FindControl("hdnCostPrice");

                        List<BranchItemPriceDiscDtls> lstDiscDtls = new List<BranchItemPriceDiscDtls>();
                        lstDiscDtls = BIP.GetPurchaseDiscountDetails(Session["BranchCode"].ToString(), dt.Rows[i - 1]["Supplier_Name"].ToString(), dt.Rows[i - 1]["Part_Number"].ToString());
                        ddlPurchaseDiscount.DataSource = lstDiscDtls;
                        ddlPurchaseDiscount.DataTextField = "DiscountDesc";
                        ddlPurchaseDiscount.DataValueField = "DiscountPercentage";
                        ddlPurchaseDiscount.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetCouponDetails(Session["BranchCode"].ToString(), dt.Rows[i - 1]["Supplier_Name"].ToString(), dt.Rows[i - 1]["Part_Number"].ToString());
                        ddlCoupon.DataSource = lstDiscDtls;
                        ddlCoupon.DataTextField = "DiscountDesc";
                        ddlCoupon.DataValueField = "DiscountPercentage";
                        ddlCoupon.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetCDPercentageDetails(Session["BranchCode"].ToString(), dt.Rows[i - 1]["Supplier_Name"].ToString(), dt.Rows[i - 1]["Part_Number"].ToString());
                        ddlCDPercentage.DataSource = lstDiscDtls;
                        ddlCDPercentage.DataTextField = "DiscountDesc";
                        ddlCDPercentage.DataValueField = "DiscountPercentage";
                        ddlCDPercentage.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetWCPercentageDetails(Session["BranchCode"].ToString(), dt.Rows[i - 1]["Supplier_Name"].ToString(), dt.Rows[i - 1]["Part_Number"].ToString());
                        ddlWCPercentage.DataSource = lstDiscDtls;
                        ddlWCPercentage.DataTextField = "DiscountDesc";
                        ddlWCPercentage.DataValueField = "DiscountPercentage";
                        ddlWCPercentage.DataBind();
                        lstDiscDtls = null;

                        ddlSupplierName.SelectedValue = dt.Rows[i - 1]["Supplier_Name"].ToString();
                        txtSupplierPartNo.Text = dt.Rows[i - 1]["Part_Number"].ToString();
                        txtListPrice.Text = dt.Rows[i - 1]["ListPrice"].ToString();
                        ddlPurchaseDiscount.SelectedValue = dt.Rows[i - 1]["PurchaseDiscount"].ToString();
                        txtListLessDiscount.Text = dt.Rows[i - 1]["ListLessDiscount"].ToString();
                        ddlCoupon.SelectedValue = dt.Rows[i - 1]["Coupon"].ToString();
                        txtAfterCoupon.Text = dt.Rows[i - 1]["AfterCoupon"].ToString();
                        ddlCDPercentage.SelectedValue = dt.Rows[i - 1]["CDPercentage"].ToString();
                        txtCDAmount.Text = dt.Rows[i - 1]["CDAmount"].ToString();
                        txtAfterCD.Text = dt.Rows[i - 1]["AfterCD"].ToString();
                        ddlWCPercentage.SelectedValue = dt.Rows[i - 1]["WareHousePercentage"].ToString();
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

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlSupplierPartNo = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSupplierPartNo.Parent.Parent);
                DropDownList ddlSupplierName = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierName");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtItemCode");
                TextBox txtSupplierPartno = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtListPrice = (TextBox)grdrDropDownRow.FindControl("txtListPrice");
                DropDownList ddlPurchaseDiscount = (DropDownList)grdrDropDownRow.FindControl("ddlPurchaseDiscount");
                DropDownList ddlCoupon = (DropDownList)grdrDropDownRow.FindControl("ddlCoupon");
                TextBox txtListLessDiscount = (TextBox)grdrDropDownRow.FindControl("txtListLessDiscount");
                TextBox txtAfterCoupon = (TextBox)grdrDropDownRow.FindControl("txtAfterCoupon");
                DropDownList ddlCDPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlCDPercentage");
                TextBox txtCDAmount = (TextBox)grdrDropDownRow.FindControl("txtCDAmount");
                TextBox txtAfterCD = (TextBox)grdrDropDownRow.FindControl("txtAfterCD");
                DropDownList ddlWCPercentage = (DropDownList)grdrDropDownRow.FindControl("ddlWCPercentage");
                TextBox txtWCAmount = (TextBox)grdrDropDownRow.FindControl("txtWCAmount");
                TextBox txtCostPrice = (TextBox)grdrDropDownRow.FindControl("txtCostPrice");
                TextBox txtMRP = (TextBox)grdrDropDownRow.FindControl("txtMRP");

                if (ddlSupplierPartNo.SelectedIndex > 0)
                {
                    bool isExisting = CheckExisting(ddlSupplierPartNo.SelectedValue);

                    if (isExisting)
                    {
                        txtItemCode.Text = ddlSupplierPartNo.SelectedValue;
                        txtSupplierPartno.Text = ddlSupplierPartNo.SelectedItem.Text;

                        List<Branch_ItemPrice> BranchItemPriceDtls = new List<Branch_ItemPrice>();

                        BranchItemPriceDtls = BIP.GetBranchItemPriceList(ddlSupplierName.SelectedValue, txtSupplierPartno.Text, Session["BranchCode"].ToString());

                        if (BranchItemPriceDtls.Count > 0)
                        {
                            txtListPrice.Text = FourDecimalConversion(BranchItemPriceDtls[0].listPrice.ToString());
                            txtCostPrice.Text = FourDecimalConversion(BranchItemPriceDtls[0].costPrice.ToString());
                            txtMRP.Text = FourDecimalConversion(BranchItemPriceDtls[0].sellingPrice.ToString());
                        }
                        else
                        {
                            txtListPrice.Text = "";
                            txtCostPrice.Text = "";
                            txtMRP.Text = "";
                        }

                        txtListLessDiscount.Text = "0.00";
                        txtAfterCoupon.Text = "0.00";
                        txtCDAmount.Text = "0.00";
                        txtAfterCD.Text = "0.00";
                        txtWCAmount.Text = "0.00";

                        List<BranchItemPriceDiscDtls> lstDiscDtls = new List<BranchItemPriceDiscDtls>();
                        lstDiscDtls = BIP.GetPurchaseDiscountDetails(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue, ddlSupplierPartNo.SelectedItem.Text);
                        ddlPurchaseDiscount.DataSource = lstDiscDtls;
                        ddlPurchaseDiscount.DataTextField = "DiscountDesc";
                        ddlPurchaseDiscount.DataValueField = "DiscountPercentage";
                        ddlPurchaseDiscount.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetCouponDetails(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue, ddlSupplierPartNo.SelectedItem.Text);
                        ddlCoupon.DataSource = lstDiscDtls;
                        ddlCoupon.DataTextField = "DiscountDesc";
                        ddlCoupon.DataValueField = "DiscountPercentage";
                        ddlCoupon.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetCDPercentageDetails(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue, ddlSupplierPartNo.SelectedItem.Text);
                        ddlCDPercentage.DataSource = lstDiscDtls;
                        ddlCDPercentage.DataTextField = "DiscountDesc";
                        ddlCDPercentage.DataValueField = "DiscountPercentage";
                        ddlCDPercentage.DataBind();
                        lstDiscDtls = null;

                        lstDiscDtls = BIP.GetWCPercentageDetails(Session["BranchCode"].ToString(), ddlSupplierName.SelectedValue, ddlSupplierPartNo.SelectedItem.Text);
                        ddlWCPercentage.DataSource = lstDiscDtls;
                        ddlWCPercentage.DataTextField = "DiscountDesc";
                        ddlWCPercentage.DataValueField = "DiscountPercentage";
                        ddlWCPercentage.DataBind();
                        lstDiscDtls = null;

                        txtListPrice.Focus();
                    }
                    else
                    {
                        ddlSupplierPartNo.SelectedIndex = 0;
                        txtItemCode.Text = "";
                        txtSupplierPartno.Text = "";
                        ddlPurchaseDiscount.SelectedIndex = 0;
                        txtListPrice.Text = "";
                        txtListLessDiscount.Text = "0.00";
                        ddlCoupon.SelectedIndex = 0;
                        txtAfterCoupon.Text = "0.00";
                        ddlCDPercentage.SelectedIndex = 0;
                        txtCDAmount.Text = "0.00";
                        txtAfterCD.Text = "0.00";
                        ddlWCPercentage.SelectedIndex = 0;
                        txtWCAmount.Text = "0.00";
                        txtCostPrice.Text = "";
                        txtMRP.Text = "";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Record already exists...');", true);
                        ddlSupplierPartNo.Focus();
                    }
                }
                else
                {
                    txtItemCode.Text = "";
                    txtSupplierPartno.Text = "";
                    ddlPurchaseDiscount.SelectedIndex = 0;
                    txtListPrice.Text = "";
                    txtListLessDiscount.Text = "0.00";
                    ddlCoupon.SelectedIndex = 0;
                    txtAfterCoupon.Text = "0.00";
                    ddlCDPercentage.SelectedIndex = 0;
                    txtCDAmount.Text = "0.00";
                    txtAfterCD.Text = "0.00";
                    ddlWCPercentage.SelectedIndex = 0;
                    txtWCAmount.Text = "0.00";
                    txtCostPrice.Text = "";
                    txtMRP.Text = "";
                }                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BranchItemPrice), exp);
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
                int i = 1;

                foreach (GridViewRow gr in grdItemDetails.Rows)
                {
                    DropDownList ddlSupplierName = (DropDownList)gr.FindControl("ddlSupplierName");                    
                    TextBox SupplierPartno = (TextBox)gr.FindControl("txtSupplierPartNo");
                    DropDownList ddlSupplierPartno = (DropDownList)gr.FindControl("ddlSupplierPartNo");
                    TextBox ListPrice = (TextBox)gr.FindControl("txtListPrice");                                      
                    TextBox Mrp = (TextBox)gr.FindControl("txtMRP");
                    TextBox Itemcode = (TextBox)gr.FindControl("txtItemCode");
                    HiddenField hdnCostPrice = (HiddenField)gr.FindControl("hdnCostPrice");

                    if (i == grdItemDetails.Rows.Count)
                    {
                        TextBox ListLessDiscount = (TextBox)gr.FindControl("txtListLessDiscount");
                        TextBox AfterCoupon = (TextBox)gr.FindControl("txtAfterCoupon");
                        TextBox CDAmount = (TextBox)gr.FindControl("txtCDAmount");
                        TextBox AfterCD = (TextBox)gr.FindControl("txtAfterCD");
                        TextBox WCAmount = (TextBox)gr.FindControl("txtWCAmount");
                        TextBox CostPrice = (TextBox)gr.FindControl("txtCostPrice");

                        HiddenField hdnListLessDiscount = (HiddenField)gr.FindControl("hdnListLessDiscount");
                        HiddenField hdnAfterCoupon = (HiddenField)gr.FindControl("hdnAfterCoupon");
                        HiddenField hdnCDAmount = (HiddenField)gr.FindControl("hdnCDAmount");
                        HiddenField hdnAfterCD = (HiddenField)gr.FindControl("hdnAfterCD");
                        HiddenField hdnWCAmount = (HiddenField)gr.FindControl("hdnWCAmount");


                        ListLessDiscount.Text = hdnListLessDiscount.Value;
                        AfterCoupon.Text = hdnAfterCoupon.Value;
                        CDAmount.Text = hdnCDAmount.Value;
                        AfterCD.Text = hdnAfterCD.Value;
                        WCAmount.Text = hdnWCAmount.Value;
                        CostPrice.Text = hdnCostPrice.Value;

                        if (Mrp.Text == "") Mrp.Text = "0";
                    }

                    result = BIP.AddNewBranchItemPrice(zone, state, branch, ddlSupplierName.SelectedValue, Itemcode.Text, SupplierPartno.Text, Convert.ToDecimal(ListPrice.Text), Convert.ToDecimal(hdnCostPrice.Value), Convert.ToDecimal(Mrp.Text));

                    i++;
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
                    btnSubmit.Visible = false;
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

        private string FourDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.0000";
            else
                return string.Format("{0:0.0000}", Convert.ToDecimal(strValue));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("BranchItemPrice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
    }
}