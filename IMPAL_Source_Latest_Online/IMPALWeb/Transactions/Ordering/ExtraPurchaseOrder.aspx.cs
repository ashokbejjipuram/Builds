using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
using System.Data;
using System.Globalization;
using log4net;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using IMPALLibrary.Masters;

namespace IMPALWeb
{
    public partial class ExtraPurchaseOrder : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private string strPONumber = default(string);
        private string strPONumberField = default(string);
        DataTable dt = new DataTable();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ExtraPurchaseOrder), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!Page.IsPostBack)
                {
                    if (Session["BranchCode"] != null)
                        strBranchCode = Session["BranchCode"].ToString();
                    fnPopulateIndentType();

                    IMPALLibrary.Transactions.POIndentCWHTran ponumb = new IMPALLibrary.Transactions.POIndentCWHTran();
                    ddlIndentNumber.DataSource = ponumb.GetIndentNumberEPO(ddlIndentType.SelectedValue, strBranchCode);
                    ddlIndentNumber.DataTextField = "IndentNumber";
                    ddlIndentNumber.DataValueField = "IndentNumber";
                    ddlIndentNumber.DataBind();
                    BindEPOtype();
                    BindAllCustomers(strBranchCode);

                    lblSupplierPartNo.Attributes.Add("style", "display:none");
                    ddlItemCode.Attributes.Add("style", "display:none");
                    btnAddItem.Attributes.Add("style", "display:none");

                    BtnSubmit.Enabled = false;
                    btnReportPDF.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportRTF.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnPopulateIndentType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ExtraPurchaseOrder");
                ddlIndentType.DataSource = oList;
                ddlIndentType.DataValueField = "DisplayValue";
                ddlIndentType.DataTextField = "DisplayText";
                ddlIndentType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlIndentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlIndentType.SelectedIndex > 0)
                {
                    IMPALLibrary.Transactions.POIndentCWHTran ponumb = new IMPALLibrary.Transactions.POIndentCWHTran();
                    ddlIndentNumber.DataSource = ponumb.GetIndentNumberEPO(ddlIndentType.SelectedValue, strBranchCode);
                    ddlIndentNumber.DataTextField = "IndentNumber";
                    ddlIndentNumber.DataValueField = "IndentNumber";
                    ddlIndentNumber.DataBind();
                }
                else
                {
                    ddlIndentNumber.DataSource = null;
                    ddlIndentNumber.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindAllCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlCustomerType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllCustomersEPO";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    ddlCustomer.DataSource = obj;
                    ddlCustomer.DataTextField = "Customer_Name";
                    ddlCustomer.DataValueField = "Customer_Code";
                    ddlCustomer.DataBind();

                    ddlCustomer.Items.Insert(0, "");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindEPOtype()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlEPOtype.DataSource = oCommon.GetDropDownListValues("EPOType");
                ddlEPOtype.DataTextField = "DisplayText";
                ddlEPOtype.DataValueField = "DisplayValue";
                ddlEPOtype.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlEPOtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlEPOtype.SelectedValue == "Dealer-Back to Back")
                {
                    Label2.Attributes.Add("style", "display:none");
                    ddlEPOsubType.Attributes.Add("style", "display:none");
                    Label3.Attributes.Add("style", "display:inline");
                    ddlCustomer.Attributes.Add("style", "display:inline");

                    ddlEPOsubType.Items.Clear();
                }
                else
                {
                    Label2.Attributes.Add("style", "display:inline");
                    ddlEPOsubType.Attributes.Add("style", "display:inline");
                    Label3.Attributes.Add("style", "display:none");
                    ddlCustomer.Attributes.Add("style", "display:none");
                }

                FirstGridViewRow();
                BindEPOsubtypes();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindEPOsubtypes()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Button btnAddDpoItem = (Button)grdPOIndent.FooterRow.FindControl("btnAddDpoItem");

                if (ddlEPOtype.SelectedValue == "Stock" || ddlEPOtype.SelectedValue == "MOQ/Shipper")
                {
                    lblSupplierPartNo.Attributes.Add("style", "display:inline");
                    ddlItemCode.Attributes.Add("style", "display:inline");
                    btnAddItem.Attributes.Add("style", "display:inline");

                    if (ddlEPOtype.SelectedValue == "Stock")
                    {
                        ddlEPOsubType.Enabled = true;

                        if (grdPOIndent.FooterRow != null)
                            btnAddDpoItem.Visible = true;
                    }
                    else
                    {
                        ddlEPOsubType.Enabled = false;

                        if (grdPOIndent.FooterRow != null)
                            btnAddDpoItem.Visible = false;
                    }
                }
                else
                {
                    lblSupplierPartNo.Attributes.Add("style", "display:none");
                    ddlItemCode.Attributes.Add("style", "display:none");
                    btnAddItem.Attributes.Add("style", "display:none");

                    ddlEPOsubType.Enabled = true;

                    if (grdPOIndent.FooterRow != null)
                        btnAddDpoItem.Visible = true;
                }

                string strSubType = string.Empty;

                if (ddlEPOtype.SelectedValue == "Stock")
                    strSubType = "EPOsubType-Stock";
                else if (ddlEPOtype.SelectedValue == "Scheme-HO" || ddlEPOtype.SelectedValue == "Scheme-Branch-State")
                    strSubType = "EPOsubType-Scheme";
                else if (ddlEPOtype.SelectedValue == "New Part Number")
                    strSubType = "EPOsubType-NewPart";

                ImpalLibrary oCommon = new ImpalLibrary();
                ddlEPOsubType.DataSource = oCommon.GetDropDownListValues(strSubType);
                ddlEPOsubType.DataTextField = "DisplayText";
                ddlEPOsubType.DataValueField = "DisplayValue";
                ddlEPOsubType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public DataTable ConstructColumn(DataTable objPOItemTable)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataColumn dtColPartNumber = new DataColumn("PartNumber", typeof(string));
                DataColumn dtColItemDesc = new DataColumn("ItemDesc", typeof(string));
                DataColumn dtColVehAppln = new DataColumn("VehAppln", typeof(string));
                DataColumn dtColStockOnHand = new DataColumn("StockOnHand", typeof(string));
                DataColumn dtColPreviousReqQty = new DataColumn("PreviousReqQty", typeof(string));
                DataColumn dtColDocOnHand = new DataColumn("DocOnHand", typeof(string));
                DataColumn dtColAvgSales = new DataColumn("AvgSales", typeof(string));
                DataColumn dtColCurMonthSales = new DataColumn("CurMonthSales", typeof(string));
                DataColumn dtColToOrderQty = new DataColumn("ToOrderQty", typeof(string));
                DataColumn dtColToOrderQtyAddl = new DataColumn("ToOrderQtyAddl", typeof(string));
                DataColumn dtColPackQty = new DataColumn("PackQty", typeof(string));
                DataColumn dtColPoQty = new DataColumn("PoQty", typeof(string));
                DataColumn dtColItemCode = new DataColumn("ItemCode", typeof(string));
                DataColumn dtColVehTypeDesc = new DataColumn("VehTypeDesc", typeof(string));
                DataColumn dtColExtraQty = new DataColumn("ExtraQty", typeof(string));

                objPOItemTable.Columns.Add(dtColPartNumber);
                objPOItemTable.Columns.Add(dtColItemDesc);
                objPOItemTable.Columns.Add(dtColVehAppln);
                objPOItemTable.Columns.Add(dtColStockOnHand);
                objPOItemTable.Columns.Add(dtColPreviousReqQty);
                objPOItemTable.Columns.Add(dtColDocOnHand);
                objPOItemTable.Columns.Add(dtColAvgSales);
                objPOItemTable.Columns.Add(dtColCurMonthSales);
                objPOItemTable.Columns.Add(dtColToOrderQty);
                objPOItemTable.Columns.Add(dtColToOrderQtyAddl);
                objPOItemTable.Columns.Add(dtColPackQty);
                objPOItemTable.Columns.Add(dtColPoQty);
                objPOItemTable.Columns.Add(dtColItemCode);
                objPOItemTable.Columns.Add(dtColVehTypeDesc);
                objPOItemTable.Columns.Add(dtColExtraQty);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objPOItemTable;
        }

        public void FirstGridViewRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("PartNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
                dt.Columns.Add(new DataColumn("VehAppln", typeof(string)));
                dt.Columns.Add(new DataColumn("StockOnHand", typeof(string)));
                dt.Columns.Add(new DataColumn("PreviousReqQty", typeof(string)));
                dt.Columns.Add(new DataColumn("DocOnHand", typeof(string)));
                dt.Columns.Add(new DataColumn("AvgSales", typeof(string)));
                dt.Columns.Add(new DataColumn("CurMonthSales", typeof(string)));
                dt.Columns.Add(new DataColumn("ToOrderQty", typeof(string)));
                dt.Columns.Add(new DataColumn("ToOrderQtyAddl", typeof(string)));
                dt.Columns.Add(new DataColumn("PackQty", typeof(string)));
                dt.Columns.Add(new DataColumn("PoQty", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("VehTypeDesc", typeof(string)));
                dt.Columns.Add(new DataColumn("ExtraQty", typeof(string)));
                dt.Columns.Add(new DataColumn("Indicator", typeof(string)));
                dt.Columns.Add(new DataColumn("SurplusQty", typeof(string)));

                DataRow dr = null;
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["PartNumber"] = string.Empty;
                    dr["ItemDesc"] = string.Empty;
                    dr["VehAppln"] = string.Empty;
                    dr["StockOnHand"] = string.Empty;
                    dr["PreviousReqQty"] = string.Empty;
                    dr["DocOnHand"] = string.Empty;
                    dr["AvgSales"] = string.Empty;
                    dr["CurMonthSales"] = string.Empty;
                    dr["ToOrderQty"] = string.Empty;
                    dr["ToOrderQtyAddl"] = string.Empty;
                    dr["PackQty"] = string.Empty;
                    dr["PoQty"] = string.Empty;
                    dr["ItemCode"] = string.Empty;
                    dr["VehTypeDesc"] = string.Empty;
                    dr["ExtraQty"] = string.Empty;
                    dr["Indicator"] = string.Empty;
                    dr["SurplusQty"] = string.Empty;
                    dt.Rows.Add(dr);
                }

                ViewState["CurrentTable"] = dt;
                grdPOIndent.DataSource = dt;
                grdPOIndent.DataBind();
                ViewState["GridRowCount"] = "0";
                grdPOIndent.Rows[0].Cells.Clear();
                grdPOIndent.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                grdPOIndent.Rows[0].Cells[0].ColumnSpan = 13;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnPopulateSupplierPartNumber()
        {
            ItemMasters objItemMaster = new ItemMasters();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlItemCode.DataSource = objItemMaster.GetSupplierPartNumberEPO(strBranchCode, ddlIndentNumber.SelectedValue);
                ddlItemCode.DataTextField = "Supplierpartno";
                ddlItemCode.DataValueField = "itemcode";
                ddlItemCode.DataBind();

                ddlItemCode.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                objItemMaster = null;
                Source = null;
            }
        }

        protected void ddlIndentNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlEPOtype.SelectedIndex = 0;
                ddlEPOsubType.Items.Clear();

                if (ddlIndentNumber.SelectedIndex > 0)
                {
                    fnPopulateSupplierPartNumber();
                    BtnSubmit.Enabled = true;
                }
                else
                {
                    ddlItemCode.Items.Clear();
                    BtnSubmit.Enabled = false;
                }

                FirstGridViewRow();

                if (grdPOIndent.FooterRow != null)
                {
                    Button btnAddDpoItem = (Button)grdPOIndent.FooterRow.FindControl("btnAddDpoItem");
                    btnAddDpoItem.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnAddItem_OnClick(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlItemCode.SelectedIndex > 0)
                {
                    bool isExisting = CheckExisting(ddlItemCode.SelectedItem.Text);

                    if (isExisting)
                    {
                        int ItemPriceCnt = 0;
                        IMPALLibrary.Transactions.InwardTransactions inwardTransactions = new IMPALLibrary.Transactions.InwardTransactions();
                        ItemPriceCnt = inwardTransactions.CheckBranchItemPrice(ddlItemCode.SelectedValue, strBranchCode);

                        if (ItemPriceCnt <= 0)
                        {                            
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # " + ddlItemCode.SelectedItem.Text + " is missing in Branch Item Price Master. Please add the same');", true);
                            ddlItemCode.SelectedIndex = 0;
                            return;
                        }

                        LoadGridIndent();
                    }
                    else
                    {
                        ddlItemCode.SelectedIndex = 0;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # already exists.');", true);
                        return;
                    }
                }
                else
                {
                    BtnSubmit.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnAddDpoItem_OnClick(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlItemCode.SelectedIndex = 0;
                ddlItemCode.Enabled = false;
                btnAddItem.Enabled = false;
                LoadGridDPO();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddlSupplierPartNo = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSupplierPartNo.Parent.Parent);
                TextBox txtSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                TextBox txtItemDesc = (TextBox)grdrDropDownRow.FindControl("txtItemDesc");
                TextBox txtVehicleAppln = (TextBox)grdrDropDownRow.FindControl("txtVehicleAppln");
                Label lblPackQty = (Label)grdrDropDownRow.FindControl("lblPackQty");
                Label lblStockOnHand = (Label)grdrDropDownRow.FindControl("lblStockOnHand");
                Label lblPreviousReqQty = (Label)grdrDropDownRow.FindControl("lblPreviousReqQty");
                Label lblDocOnHand = (Label)grdrDropDownRow.FindControl("lblDocOnHand");
                Label lblAvgSales = (Label)grdrDropDownRow.FindControl("lblAvgSales");
                Label lblCurMonthSales = (Label)grdrDropDownRow.FindControl("lblCurMonthSales");
                Label lblPoQty = (Label)grdrDropDownRow.FindControl("lblPoQty");
                Label lblItemCode = (Label)grdrDropDownRow.FindControl("lblItemCode");
                TextBox txtExtraQty = (TextBox)grdrDropDownRow.FindControl("txtExtraQty");
                HiddenField hdnSurplusQty = (HiddenField)grdrDropDownRow.FindControl("hdnSurplusQty");

                if (ddlSupplierPartNo.SelectedIndex > 0)
                {
                    bool isExisting = CheckExistingDPO(ddlSupplierPartNo.SelectedItem.Text);

                    if (isExisting)
                    {
                        int ItemPriceCnt = 0;
                        IMPALLibrary.Transactions.InwardTransactions inwardTransactions = new IMPALLibrary.Transactions.InwardTransactions();
                        ItemPriceCnt = inwardTransactions.CheckBranchItemPrice(ddlSupplierPartNo.SelectedValue, strBranchCode);

                        if (ItemPriceCnt <= 0)
                        {                            
                            txtItemDesc.Text = "";
                            txtVehicleAppln.Text = "";
                            lblPackQty.Text = "";
                            lblStockOnHand.Text = "";
                            lblPreviousReqQty.Text = "";
                            lblDocOnHand.Text = "";
                            lblAvgSales.Text = "";
                            lblCurMonthSales.Text = "";
                            lblPoQty.Text = "";
                            lblItemCode.Text = "";
                            txtExtraQty.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # " + ddlSupplierPartNo.SelectedItem.Text + " is missing in Branch Item Price Master. Please add the same');", true);
                            ddlSupplierPartNo.SelectedIndex = 0;
                            return;
                        }

                        POIndentCWHTran objPOIndent = new POIndentCWHTran();
                        List<POIndentDetail> objPartItems = objPOIndent.ListDPOitemsExtraPurchase(ddlIndentNumber.SelectedValue, ddlIndentType.SelectedValue.ToString(), strBranchCode, ddlSupplierPartNo.SelectedValue);

                        if (objPartItems[0].SurplusQty == "0")
                        {
                            txtSupplierPartNo.Text = objPartItems[0].PartNumber;
                            txtItemDesc.Text = objPartItems[0].ItemDesc;
                            txtVehicleAppln.Text = objPartItems[0].VehTypeDesc;
                            lblPackQty.Text = objPartItems[0].PackQty;
                            lblStockOnHand.Text = objPartItems[0].StockOnHand;
                            lblPreviousReqQty.Text = objPartItems[0].PreviousReqQty;
                            lblDocOnHand.Text = objPartItems[0].DocOnHand;
                            lblAvgSales.Text = objPartItems[0].AvgSales;
                            lblCurMonthSales.Text = objPartItems[0].CurMonthSales;
                            lblPoQty.Text = objPartItems[0].PendingOrderQty;
                            lblItemCode.Text = objPartItems[0].ItemCode;
                            txtExtraQty.Text = objPartItems[0].ExtraQty;
                        }
                        else
                        {
                            ddlSupplierPartNo.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # is having Surplus Qty and hence you cannot place the order.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ddlSupplierPartNo.SelectedIndex = 0;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # already exists.');", true);
                        txtSupplierPartNo.Text = "";
                        txtItemDesc.Text = "";
                        txtVehicleAppln.Text = "";
                        lblPackQty.Text = "";
                        lblStockOnHand.Text = "";
                        lblPreviousReqQty.Text = "";
                        lblDocOnHand.Text = "";
                        lblAvgSales.Text = "";
                        lblCurMonthSales.Text = "";
                        lblPoQty.Text = "";
                        lblItemCode.Text = "";
                        txtExtraQty.Text = "";
                        return;
                    }
                }
                else
                {
                    txtSupplierPartNo.Text = "";
                    txtItemDesc.Text = "";
                    txtVehicleAppln.Text = "";
                    lblPackQty.Text = "";
                    lblStockOnHand.Text = "";
                    lblPreviousReqQty.Text = "";
                    lblDocOnHand.Text = "";
                    lblAvgSales.Text = "";
                    lblCurMonthSales.Text = "";
                    lblPoQty.Text = "";
                    lblItemCode.Text = "";
                    txtExtraQty.Text = "";

                    BtnSubmit.Enabled = false;
                }

                ddlItemCode.Enabled = true;
                btnAddItem.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private bool CheckExisting(string partNumber)
        {
            foreach (GridViewRow gr in grdPOIndent.Rows)
            {
                TextBox txtSupplierPartNo = (TextBox)gr.Cells[0].FindControl("txtSupplierPartNo");

                if (partNumber == txtSupplierPartNo.Text)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckExistingDPO(string partNumber)
        {
            foreach (GridViewRow gr in grdPOIndent.Rows)
            {
                TextBox txtSupplierPartNo = (TextBox)gr.Cells[0].FindControl("txtSupplierPartNo");
                DropDownList ddlSupplierPartNo = (DropDownList)gr.Cells[0].FindControl("ddlSupplierPartNo");

                if (!ddlSupplierPartNo.Visible)
                {
                    if (partNumber == txtSupplierPartNo.Text)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        protected void grdPOIndent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.RowIndex >= 0)
                {
                    dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();

                    if (grdPOIndent.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grdPOIndent.Rows.Count; i++)
                        {
                            TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[i].FindControl("txtSupplierPartNo");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[i].FindControl("ddlSupplierPartNo");
                            Button btnSearch = (Button)grdPOIndent.Rows[i].FindControl("btnSearch");
                            TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[i].FindControl("txtItemDesc");
                            TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[i].FindControl("txtVehicleAppln");
                            Label lblPackQty = (Label)grdPOIndent.Rows[i].FindControl("lblPackQty");
                            Label lblStockOnHand = (Label)grdPOIndent.Rows[i].FindControl("lblStockOnHand");
                            Label lblPreviousReqQty = (Label)grdPOIndent.Rows[i].FindControl("lblPreviousReqQty");
                            Label lblDocOnHand = (Label)grdPOIndent.Rows[i].FindControl("lblDocOnHand");
                            Label lblAvgSales = (Label)grdPOIndent.Rows[i].FindControl("lblAvgSales");
                            Label lblCurMonthSales = (Label)grdPOIndent.Rows[i].FindControl("lblCurMonthSales");
                            Label lblPoQty = (Label)grdPOIndent.Rows[i].FindControl("lblPoQty");
                            Label lblItemCode = (Label)grdPOIndent.Rows[i].FindControl("lblItemCode");
                            TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[i].FindControl("txtExtraQty");
                            HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[i].FindControl("hdnIndicator");
                            HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[i].FindControl("hdnSurplusQty");

                            DataRow dr = dt.NewRow();

                            dr["PartNumber"] = txtSupplierPartNo.Text;
                            dr["ItemDesc"] = txtItemDesc.Text;
                            dr["VehAppln"] = txtVehicleAppln.Text;
                            dr["PackQty"] = lblPackQty.Text;
                            dr["StockOnHand"] = lblStockOnHand.Text;
                            dr["PreviousReqQty"] = lblPreviousReqQty.Text;
                            dr["DocOnHand"] = lblDocOnHand.Text;
                            dr["AvgSales"] = lblAvgSales.Text;
                            dr["CurMonthSales"] = lblCurMonthSales.Text;
                            dr["PoQty"] = lblPoQty.Text;
                            dr["ItemCode"] = lblItemCode.Text;
                            dr["ExtraQty"] = txtExtraQty.Text;
                            dr["Indicator"] = hdnIndicator.Value;
                            dr["SurplusQty"] = hdnSurplusQty.Value;

                            dt.Rows.Add(dr);
                        }

                        dt.Rows.RemoveAt(e.RowIndex);
                        ViewState["CurrentTable"] = dt;

                        if (dt.Rows.Count > 0)
                        {
                            grdPOIndent.DataSource = ViewState["CurrentTable"];
                            grdPOIndent.DataBind();
                            SetPreviousData();
                        }
                        else
                            FirstGridViewRow();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void LoadGridIndent()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
                        }
                        else
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtSupplierPartNo");
                                DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                                Button btnSearch = (Button)grdPOIndent.Rows[rowIndex].FindControl("btnSearch");
                                TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtItemDesc");
                                TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtVehicleAppln");
                                Label lblPackQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPackQty");
                                Label lblStockOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblStockOnHand");
                                Label lblPreviousReqQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPreviousReqQty");
                                Label lblDocOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblDocOnHand");
                                Label lblAvgSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblAvgSales");
                                Label lblCurMonthSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblCurMonthSales");
                                Label lblPoQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPoQty");
                                Label lblItemCode = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblItemCode");
                                TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtExtraQty");
                                HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnIndicator");
                                HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnSurplusQty");

                                drCurrentRow = dtCurrentTable.NewRow();

                                dtCurrentTable.Rows[i - 1]["PartNumber"] = txtSupplierPartNo.Text;
                                dtCurrentTable.Rows[i - 1]["ItemDesc"] = txtItemDesc.Text;
                                dtCurrentTable.Rows[i - 1]["VehAppln"] = txtVehicleAppln.Text;
                                dtCurrentTable.Rows[i - 1]["PackQty"] = lblPackQty.Text;
                                dtCurrentTable.Rows[i - 1]["StockOnHand"] = lblStockOnHand.Text;
                                dtCurrentTable.Rows[i - 1]["PreviousReqQty"] = lblPreviousReqQty.Text;
                                dtCurrentTable.Rows[i - 1]["DocOnHand"] = lblDocOnHand.Text;
                                dtCurrentTable.Rows[i - 1]["AvgSales"] = lblAvgSales.Text;
                                dtCurrentTable.Rows[i - 1]["CurMonthSales"] = lblCurMonthSales.Text;
                                dtCurrentTable.Rows[i - 1]["PoQty"] = lblPoQty.Text;
                                dtCurrentTable.Rows[i - 1]["ItemCode"] = lblItemCode.Text;
                                dtCurrentTable.Rows[i - 1]["ExtraQty"] = txtExtraQty.Text;
                                dtCurrentTable.Rows[i - 1]["Indicator"] = hdnIndicator.Value;
                                dtCurrentTable.Rows[i - 1]["SurplusQty"] = hdnSurplusQty.Value;

                                rowIndex++;
                            }
                        }

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();

                        grdPOIndent.DataSource = dtCurrentTable;
                        grdPOIndent.DataBind();
                    }
                    else
                        BtnSubmit.Enabled = false;
                }
                else
                {
                    Response.Write("ViewState is null");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            SetPreviousDataIndent();
        }

        public void LoadGridDPO()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
                        }
                        else
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtSupplierPartNo");
                                DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                                Button btnSearch = (Button)grdPOIndent.Rows[rowIndex].FindControl("btnSearch");
                                TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtItemDesc");
                                TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtVehicleAppln");
                                Label lblPackQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPackQty");
                                Label lblStockOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblStockOnHand");
                                Label lblPreviousReqQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPreviousReqQty");
                                Label lblDocOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblDocOnHand");
                                Label lblAvgSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblAvgSales");
                                Label lblCurMonthSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblCurMonthSales");
                                Label lblPoQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPoQty");
                                Label lblItemCode = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblItemCode");
                                TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtExtraQty");
                                HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnIndicator");
                                HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnSurplusQty");

                                drCurrentRow = dtCurrentTable.NewRow();

                                dtCurrentTable.Rows[i - 1]["PartNumber"] = txtSupplierPartNo.Text;
                                dtCurrentTable.Rows[i - 1]["ItemDesc"] = txtItemDesc.Text;
                                dtCurrentTable.Rows[i - 1]["VehAppln"] = txtVehicleAppln.Text;
                                dtCurrentTable.Rows[i - 1]["PackQty"] = lblPackQty.Text;
                                dtCurrentTable.Rows[i - 1]["StockOnHand"] = lblStockOnHand.Text;
                                dtCurrentTable.Rows[i - 1]["PreviousReqQty"] = lblPreviousReqQty.Text;
                                dtCurrentTable.Rows[i - 1]["DocOnHand"] = lblDocOnHand.Text;
                                dtCurrentTable.Rows[i - 1]["AvgSales"] = lblAvgSales.Text;
                                dtCurrentTable.Rows[i - 1]["CurMonthSales"] = lblCurMonthSales.Text;
                                dtCurrentTable.Rows[i - 1]["PoQty"] = lblPoQty.Text;
                                dtCurrentTable.Rows[i - 1]["ItemCode"] = lblItemCode.Text;
                                dtCurrentTable.Rows[i - 1]["ExtraQty"] = txtExtraQty.Text;
                                dtCurrentTable.Rows[i - 1]["Indicator"] = hdnIndicator.Value;
                                dtCurrentTable.Rows[i - 1]["SurplusQty"] = hdnSurplusQty.Value;

                                rowIndex++;
                            }
                        }

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;
                        ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();

                        grdPOIndent.DataSource = dtCurrentTable;
                        grdPOIndent.DataBind();
                    }
                    else
                        BtnSubmit.Enabled = false;
                }
                else
                {
                    Response.Write("ViewState is null");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            SetPreviousDataDPO();
        }

        private void SetPreviousDataIndent()
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
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtSupplierPartNo");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                            Button btnSearch = (Button)grdPOIndent.Rows[rowIndex].FindControl("btnSearch");
                            TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtItemDesc");
                            TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtVehicleAppln");
                            Label lblPackQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPackQty");
                            Label lblStockOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblStockOnHand");
                            Label lblPreviousReqQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPreviousReqQty");
                            Label lblDocOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblDocOnHand");
                            Label lblAvgSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblAvgSales");
                            Label lblCurMonthSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblCurMonthSales");
                            Label lblPoQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPoQty");
                            Label lblItemCode = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblItemCode");
                            TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtExtraQty");
                            HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnIndicator");
                            HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnSurplusQty");

                            if (i == dtCurrentTable.Rows.Count - 1)
                            {
                                POIndentCWHTran objPOIndent = new POIndentCWHTran();
                                List<POIndentDetail> objPartItems = objPOIndent.ListIndentItemsExtraPurchase(ddlIndentNumber.SelectedValue, ddlIndentType.SelectedValue.ToString(), strBranchCode, ddlItemCode.SelectedValue);

                                if (objPartItems[0].SurplusQty == "0")
                                {
                                    txtSupplierPartNo.Text = objPartItems[0].PartNumber;
                                    txtItemDesc.Text = objPartItems[0].ItemDesc;
                                    txtVehicleAppln.Text = objPartItems[0].VehTypeDesc;
                                    lblPackQty.Text = objPartItems[0].PackQty;
                                    lblStockOnHand.Text = objPartItems[0].StockOnHand;
                                    lblPreviousReqQty.Text = objPartItems[0].PreviousReqQty;
                                    lblDocOnHand.Text = objPartItems[0].DocOnHand;
                                    lblAvgSales.Text = objPartItems[0].AvgSales;
                                    lblCurMonthSales.Text = objPartItems[0].CurMonthSales;
                                    lblPoQty.Text = objPartItems[0].PendingOrderQty;
                                    lblItemCode.Text = objPartItems[0].ItemCode;
                                    txtExtraQty.Text = objPartItems[0].ExtraQty;
                                    hdnIndicator.Value = "I";
                                }
                                else
                                {
                                    ddlSupplierPartNo.SelectedIndex = 0;
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # is having Surplus Qty and hence you cannot place the order.');", true);
                                    return;
                                }
                            }
                            else
                            {
                                txtSupplierPartNo.Text = dtCurrentTable.Rows[i]["PartNumber"].ToString();
                                txtItemDesc.Text = dtCurrentTable.Rows[i]["ItemDesc"].ToString();
                                txtVehicleAppln.Text = dtCurrentTable.Rows[i]["VehAppln"].ToString();
                                lblPackQty.Text = dtCurrentTable.Rows[i]["PackQty"].ToString();
                                lblStockOnHand.Text = dtCurrentTable.Rows[i]["StockOnHand"].ToString();
                                lblPreviousReqQty.Text = dtCurrentTable.Rows[i]["PreviousReqQty"].ToString();
                                lblDocOnHand.Text = dtCurrentTable.Rows[i]["DocOnHand"].ToString();
                                lblAvgSales.Text = dtCurrentTable.Rows[i]["AvgSales"].ToString();
                                lblCurMonthSales.Text = dtCurrentTable.Rows[i]["CurMonthSales"].ToString();
                                lblPoQty.Text = dtCurrentTable.Rows[i]["PoQty"].ToString();
                                lblItemCode.Text = dtCurrentTable.Rows[i]["ItemCode"].ToString();
                                txtExtraQty.Text = dtCurrentTable.Rows[i]["ExtraQty"].ToString();
                                hdnIndicator.Value = dtCurrentTable.Rows[i]["Indicator"].ToString();
                                hdnSurplusQty.Value = dtCurrentTable.Rows[i]["SurplusQty"].ToString();
                            }

                            if (hdnIndicator.Value == "D")
                            {
                                ddlSupplierPartNo.Visible = false;

                                if (i == dtCurrentTable.Rows.Count - 1)
                                {
                                    txtSupplierPartNo.Enabled = true;
                                    btnSearch.Visible = true;
                                }
                                else
                                {
                                    txtSupplierPartNo.Enabled = false;
                                    btnSearch.Visible = false;
                                }
                            }
                            else
                            {
                                ddlSupplierPartNo.Visible = false;
                                txtSupplierPartNo.Enabled = false;
                                btnSearch.Visible = false;
                            }

                            rowIndex++;
                        }
                    }
                }
            }
        }

        private void SetPreviousDataDPO()
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
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtSupplierPartNo");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                            Button btnSearch = (Button)grdPOIndent.Rows[rowIndex].FindControl("btnSearch");
                            TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtItemDesc");
                            TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtVehicleAppln");
                            Label lblPackQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPackQty");
                            Label lblStockOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblStockOnHand");
                            Label lblPreviousReqQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPreviousReqQty");
                            Label lblDocOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblDocOnHand");
                            Label lblAvgSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblAvgSales");
                            Label lblCurMonthSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblCurMonthSales");
                            Label lblPoQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPoQty");
                            Label lblItemCode = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblItemCode");
                            TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtExtraQty");
                            HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnIndicator");
                            HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnSurplusQty");

                            if (i == dtCurrentTable.Rows.Count - 1)
                            {
                                hdnIndicator.Value = "D";
                            }
                            else
                            {
                                txtSupplierPartNo.Text = dtCurrentTable.Rows[i]["PartNumber"].ToString();
                                txtItemDesc.Text = dtCurrentTable.Rows[i]["ItemDesc"].ToString();
                                txtVehicleAppln.Text = dtCurrentTable.Rows[i]["VehAppln"].ToString();
                                lblPackQty.Text = dtCurrentTable.Rows[i]["PackQty"].ToString();
                                lblStockOnHand.Text = dtCurrentTable.Rows[i]["StockOnHand"].ToString();
                                lblPreviousReqQty.Text = dtCurrentTable.Rows[i]["PreviousReqQty"].ToString();
                                lblDocOnHand.Text = dtCurrentTable.Rows[i]["DocOnHand"].ToString();
                                lblAvgSales.Text = dtCurrentTable.Rows[i]["AvgSales"].ToString();
                                lblCurMonthSales.Text = dtCurrentTable.Rows[i]["CurMonthSales"].ToString();
                                lblPoQty.Text = dtCurrentTable.Rows[i]["PoQty"].ToString();
                                lblItemCode.Text = dtCurrentTable.Rows[i]["ItemCode"].ToString();
                                txtExtraQty.Text = dtCurrentTable.Rows[i]["ExtraQty"].ToString();
                                hdnIndicator.Value = dtCurrentTable.Rows[i]["Indicator"].ToString();
                                hdnSurplusQty.Value = dtCurrentTable.Rows[i]["SurplusQty"].ToString();
                            }

                            if (hdnIndicator.Value == "D")
                            {
                                ddlSupplierPartNo.Visible = false;

                                if (i == dtCurrentTable.Rows.Count - 1)
                                {
                                    txtSupplierPartNo.Enabled = true;
                                    btnSearch.Visible = true;
                                }
                                else
                                {
                                    txtSupplierPartNo.Enabled = false;
                                    btnSearch.Visible = false;
                                }
                            }
                            else
                            {
                                ddlSupplierPartNo.Visible = false;
                                txtSupplierPartNo.Enabled = false;
                                btnSearch.Visible = false;
                            }

                            rowIndex++;
                        }
                    }
                }
            }
        }

        private void SetPreviousData()
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
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtSupplierPartNo = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtSupplierPartNo");
                            DropDownList ddlSupplierPartNo = (DropDownList)grdPOIndent.Rows[rowIndex].FindControl("ddlSupplierPartNo");
                            Button btnSearch = (Button)grdPOIndent.Rows[rowIndex].FindControl("btnSearch");
                            TextBox txtItemDesc = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtItemDesc");
                            TextBox txtVehicleAppln = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtVehicleAppln");
                            Label lblPackQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPackQty");
                            Label lblStockOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblStockOnHand");
                            Label lblPreviousReqQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPreviousReqQty");
                            Label lblDocOnHand = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblDocOnHand");
                            Label lblAvgSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblAvgSales");
                            Label lblCurMonthSales = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblCurMonthSales");
                            Label lblPoQty = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblPoQty");
                            Label lblItemCode = (Label)grdPOIndent.Rows[rowIndex].FindControl("lblItemCode");
                            TextBox txtExtraQty = (TextBox)grdPOIndent.Rows[rowIndex].FindControl("txtExtraQty");
                            HiddenField hdnIndicator = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnIndicator");
                            HiddenField hdnSurplusQty = (HiddenField)grdPOIndent.Rows[rowIndex].FindControl("hdnSurplusQty");

                            txtSupplierPartNo.Text = dtCurrentTable.Rows[i]["PartNumber"].ToString();
                            txtItemDesc.Text = dtCurrentTable.Rows[i]["ItemDesc"].ToString();
                            txtVehicleAppln.Text = dtCurrentTable.Rows[i]["VehAppln"].ToString();
                            lblPackQty.Text = dtCurrentTable.Rows[i]["PackQty"].ToString();
                            lblStockOnHand.Text = dtCurrentTable.Rows[i]["StockOnHand"].ToString();
                            lblPreviousReqQty.Text = dtCurrentTable.Rows[i]["PreviousReqQty"].ToString();
                            lblDocOnHand.Text = dtCurrentTable.Rows[i]["DocOnHand"].ToString();
                            lblAvgSales.Text = dtCurrentTable.Rows[i]["AvgSales"].ToString();
                            lblCurMonthSales.Text = dtCurrentTable.Rows[i]["CurMonthSales"].ToString();
                            lblPoQty.Text = dtCurrentTable.Rows[i]["PoQty"].ToString();
                            lblItemCode.Text = dtCurrentTable.Rows[i]["ItemCode"].ToString();
                            txtExtraQty.Text = dtCurrentTable.Rows[i]["ExtraQty"].ToString();
                            hdnIndicator.Value = dtCurrentTable.Rows[i]["Indicator"].ToString();
                            hdnSurplusQty.Value = dtCurrentTable.Rows[i]["SurplusQty"].ToString();

                            ddlSupplierPartNo.Visible = false;
                            txtSupplierPartNo.Enabled = false;
                            btnSearch.Visible = false;

                            rowIndex++;
                        }
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Button btnSearch = (Button)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtSupplierPartNo");
                DropDownList ddlSupplierPartNo = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierPartNo");
                TextBox txtItemDesc = (TextBox)grdrDropDownRow.FindControl("txtItemDesc");
                TextBox txtVehicleAppln = (TextBox)grdrDropDownRow.FindControl("txtVehicleAppln");
                Label lblPackQty = (Label)grdrDropDownRow.FindControl("lblPackQty");
                Label lblStockOnHand = (Label)grdrDropDownRow.FindControl("lblStockOnHand");
                Label lblPreviousReqQty = (Label)grdrDropDownRow.FindControl("lblPreviousReqQty");
                Label lblDocOnHand = (Label)grdrDropDownRow.FindControl("lblDocOnHand");
                Label lblAvgSales = (Label)grdrDropDownRow.FindControl("lblAvgSales");
                Label lblCurMonthSales = (Label)grdrDropDownRow.FindControl("lblCurMonthSales");
                Label lblPoQty = (Label)grdrDropDownRow.FindControl("lblPoQty");
                Label lblItemCode = (Label)grdrDropDownRow.FindControl("lblItemCode");
                TextBox txtExtraQty = (TextBox)grdrDropDownRow.FindControl("txtExtraQty");

                if (btnSearch.Text == "Reset")
                {
                    txtSupplierPartNo.Text = "";
                    txtItemDesc.Text = "";
                    txtVehicleAppln.Text = "";
                    lblPackQty.Text = "";
                    lblStockOnHand.Text = "";
                    lblPreviousReqQty.Text = "";
                    lblDocOnHand.Text = "";
                    lblAvgSales.Text = "";
                    lblCurMonthSales.Text = "";
                    lblPoQty.Text = "";
                    lblItemCode.Text = "";
                    txtExtraQty.Text = "";

                    txtSupplierPartNo.Visible = true;
                    ddlSupplierPartNo.Visible = false;
                    btnSearch.Text = "Search";
                }
                else
                {
                    POIndentCWHTran objPOIndent = new POIndentCWHTran();
                    List<ItemMaster> objPartItems = objPOIndent.GetDPOSupplierPartNumberEPO(strBranchCode, ddlIndentNumber.SelectedValue.Substring(10, 3), txtSupplierPartNo.Text, ddlIndentNumber.SelectedValue, ddlEPOtype.SelectedValue);

                    ddlSupplierPartNo.DataSource = (object)objPartItems;
                    ddlSupplierPartNo.DataTextField = "Supplierpartno";
                    ddlSupplierPartNo.DataValueField = "itemcode";
                    ddlSupplierPartNo.DataBind();

                    txtSupplierPartNo.Visible = false;
                    ddlSupplierPartNo.Visible = true;
                    btnSearch.Text = "Reset";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sResult = string.Empty;

                IMPALLibrary.Transactions.POIndentCWH objPOIndentCWH = new IMPALLibrary.Transactions.POIndentCWH();
                objPOIndentCWH.Items = new List<POIndentDetail>();

                POIndentDetail objPartItems = null;
                int SNo = 0;

                foreach (GridViewRow gr in grdPOIndent.Rows)
                {
                    SNo += 1;

                    TextBox txtSupplierPartNo = (TextBox)gr.FindControl("txtSupplierPartNo");
                    DropDownList ddlSupplierPartNo = (DropDownList)gr.FindControl("ddlSupplierPartNo");
                    Button btnSearch = (Button)gr.FindControl("btnSearch");
                    TextBox txtItemDesc = (TextBox)gr.FindControl("txtItemDesc");
                    TextBox txtVehicleAppln = (TextBox)gr.FindControl("txtVehicleAppln");
                    Label lblPackQty = (Label)gr.FindControl("lblPackQty");
                    Label lblStockOnHand = (Label)gr.FindControl("lblStockOnHand");
                    Label lblPreviousReqQty = (Label)gr.FindControl("lblPreviousReqQty");
                    Label lblDocOnHand = (Label)gr.FindControl("lblDocOnHand");
                    Label lblAvgSales = (Label)gr.FindControl("lblAvgSales");
                    Label lblCurMonthSales = (Label)gr.FindControl("lblCurMonthSales");
                    Label lblPoQty = (Label)gr.FindControl("lblPoQty");
                    Label lblItemCode = (Label)gr.FindControl("lblItemCode");
                    TextBox txtExtraQty = (TextBox)gr.FindControl("txtExtraQty");
                    HiddenField hdnIndicator = (HiddenField)gr.FindControl("hdnIndicator");

                    objPartItems = new POIndentDetail();
                    objPartItems.PartNumber = txtSupplierPartNo.Text;
                    objPartItems.SerialNumber = SNo.ToString();
                    objPartItems.ItemDesc = txtItemDesc.Text;
                    objPartItems.VehTypeDesc = txtVehicleAppln.Text;
                    objPartItems.PackQty = lblPackQty.Text;
                    objPartItems.StockOnHand = lblStockOnHand.Text;
                    objPartItems.PreviousReqQty = lblPreviousReqQty.Text;
                    objPartItems.DocOnHand = lblDocOnHand.Text;
                    objPartItems.AvgSales = lblAvgSales.Text;
                    objPartItems.CurMonthSales = lblCurMonthSales.Text;
                    objPartItems.PendingOrderQty = lblPoQty.Text;
                    objPartItems.ItemCode = lblItemCode.Text;
                    objPartItems.ExtraQty = txtExtraQty.Text;
                    objPartItems.Indicator = hdnIndicator.Value;

                    objPOIndentCWH.Items.Add(objPartItems);
                }

                string Remarks = string.Empty;

                if (ddlEPOtype.SelectedValue == "MOQ/Shipper")
                    Remarks = ddlEPOtype.SelectedValue;
                else if (ddlEPOtype.SelectedValue == "Dealer-Back to Back")
                    Remarks = ddlEPOtype.SelectedValue + "-" + ddlCustomer.SelectedItem.Text;
                else
                    Remarks = ddlEPOtype.SelectedValue + "-" + ddlEPOsubType.SelectedItem.Text;

                POIndentCWHTran objPOIndent = new POIndentCWHTran();
                sResult = objPOIndent.SubmitPOIndentEPO(ddlIndentNumber.SelectedValue, ddlIndentType.SelectedValue.ToString(), strBranchCode, ddlIndentNumber.SelectedValue.Substring(10, 3), ddlEPOtype.SelectedValue, Remarks, objPOIndentCWH);

                if (sResult == "0")
                {
                    ViewState["CurrentTable"] = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Process completed');", true);
                    ddlIndentNumber.Enabled = false;
                    ddlEPOtype.Enabled = false;
                    ddlEPOsubType.Enabled = false;
                    ddlItemCode.Enabled = false;
                    btnAddItem.Visible = false;
                    grdPOIndent.Enabled = false;
                    ddlIndentType.Enabled = false;
                    BtnSubmit.Visible = false;
                    btnReportPDF.Visible = true;
                    btnReportExcel.Visible = true;
                    btnReportRTF.Visible = true;
                }
                else if (sResult != "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + sResult + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("ExtraPurchaseOrder.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }

        protected void GenerateAndExportReport(string fileType)
        {
            crPurchaseOrderWorkSheet.ReportName = "Purchase_worksheetEPO";

            string strSelectionFormula = default(string);
            strPONumber = ddlIndentNumber.SelectedValue.Substring(0, 3) + "E" + ddlIndentNumber.SelectedValue.Substring(4, 16);

            string strBranchCodeField = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            strBranchCodeField = "{Purchase_Order_Header.branch_code}";
            strPONumberField = "{Purchase_Order_Header.PO_number}";

            strSelectionFormula = strPONumberField + "=" + " " + "'" + strPONumber + "' and " + strBranchCodeField + "='" + strBranchCode + "'";

            crPurchaseOrderWorkSheet.RecordSelectionFormula = strSelectionFormula;
            crPurchaseOrderWorkSheet.GenerateReportAndExportA4(fileType);
        }
    }
}
