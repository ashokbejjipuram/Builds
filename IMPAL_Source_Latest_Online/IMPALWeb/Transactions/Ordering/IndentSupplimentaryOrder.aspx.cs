using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;
using log4net;
using IMPALLibrary.Common;

namespace IMPALWeb.Ordering
{
    public partial class IndentSupplimentaryOrder : System.Web.UI.Page
    {
        DataTable dt = new DataTable();      
         string strOrdBranchCode;

         protected void Page_Init(object sender, EventArgs e)
         {
             try
             {
             }
             catch (Exception exp)
             {
                 Log.WriteException(typeof(IndentSupplimentaryOrder), exp);
             }
         }
      
        #region system event methods


        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if(ViewState["strOrdBranchCode"] != null)
                       strOrdBranchCode =  ViewState["strOrdBranchCode"].ToString();

                if (!IsPostBack)
                {
                    ddlOrdBranchName.Visible = false;
                    txtOrdBranch.Visible = true;
                    strOrdBranchCode = Session["BranchCode"].ToString();
                    ViewState["strOrdBranchCode"] = strOrdBranchCode.ToString();
                    Session["PONumber"] = null;
                    Session.Remove("PONumber");                    

                    InitializeControl();

                    ddlOrdSupplier.Attributes.Add("OnChange", "return DirectPOValidSupllier();");
                    if (GridView1.FooterRow != null)
                    {
                        Button btnAddRow = (Button)GridView1.FooterRow.FindControl("btnAdd");
                        if (btnAddRow != null)
                        {
                            btnAddRow.Attributes.Add("OnClick", "return funDirectPOBtnAddRow();");
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        protected void ddlOrd_PONumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strDirectPONumber = ddlOrd_PONumber.SelectedValue;
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.DirectPOView";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "Get_DirectPOHeaderViewMode";
                    obj.SelectParameters.Add("strPO_Number", strDirectPONumber);
                    obj.SelectParameters.Add("strIndicator", "H");
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();

                    DirectPOView objSection = new DirectPOView();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (DirectPOView)objCustomerSections[0];
                    if ((objSection.PO_Number is object))
                    {
                        ddlOrd_PONumber.SelectedValue = objSection.PO_Number.ToString();
                        //txtOrdIndentDate.Text = objSection.Indent_Date.ToString();                        
                        ddlOrdTransactionType.SelectedValue = objSection.Transaction_Type_Code.ToString();

                        if (ddlOrdTransactionType.SelectedValue == "451" || ddlOrdTransactionType.SelectedValue == "452")
                        {
                            lblCustomer.Visible = true;
                            ddlOrdCustomer.Visible = true;
                            reqCustomer.Visible = true;
                            BindAllCustomers(strOrdBranchCode);
                            ddlOrdCustomer.SelectedValue = objSection.Customer_Code.ToString();
                        }
                        else
                        {
                            lblCustomer.Visible = false;
                            ddlOrdCustomer.Visible = false;
                            reqCustomer.Visible = false;
                        }

                        txtOrdBranch.Text = objSection.Branch_Name.ToString();
                        ddlOrdSupplier.SelectedValue = objSection.Supplier_Code.ToString();
                        txtOrdRefIndentNumber.Text = objSection.Reference_Number.ToString();
                        txtOrdRefIndentDate.Text = objSection.Reference_Date.ToString();
                        txtOrdIndentDate.Text = objSection.po_date.ToString();
                        txtOrdRdPermitNumber.Text = objSection.Road_Permit_Number.ToString();
                        txtOrdRdPermitDate.Text = objSection.Road_Permit_Date.ToString();
                        txtOrdCarrier.Text = objSection.Carrier.ToString();
                        txtOrdDestination.Text = objSection.Destination.ToString();
                        txtOrdCarrierInfoRemarks.Text = objSection.Remarks.ToString();
                        txtOrdCustomerCode.Text = objSection.Customer_Code.ToString();
                        txtOrdCustomerAddress1.Text = objSection.Address1.ToString();
                        txtOrdCustomerAddress2.Text = objSection.Address2.ToString();
                        // txtOrdTin_Number.Text = objSection.Tin_No.ToString();
                        txtOrdAddress3.Text = objSection.Address3.ToString();
                        txtOrdCustomerAddress4.Text = objSection.Address4.ToString();
                        txtOrdCustomerLocation.Text = objSection.Location.ToString();
                        DisableControls(false);
                        BindGridItems();
                    }
                    else
                    {
                        ResetToEmpty();                    
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }
               
        protected void ImgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindOrdPO_Number();
                ddlOrd_PONumber.Visible = true;
                ddlOrdStatus.Visible = true;
                lblStatus.Visible = true;
                //ImgRefIndDateCalendar.Enabled = true;
                //ImgRdPermitDate.Enabled = true;
                ddlOrdTransactionType.Enabled = false;
                ddlOrdSupplier.Enabled = false;
                BtnSubmit.Enabled = false;
                //  btnUpdate.Enabled = false;               
                ddlOrdStatus.Enabled = false;                 
                txtOrd_PONumber.Visible = false;
                GridView1.ShowFooter = false;              
                BindGridItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            } 
        }

        protected void ddlOrdSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //if (!(ddlOrdTransactionType.SelectedValue == "0" || ddlOrdSupplier.SelectedValue == "0"))
                //{
                //    if (ddlOrdSupplier.SelectedValue == "182" || ddlOrdSupplier.SelectedValue == "230" ||
                //        ddlOrdSupplier.SelectedValue == "210" || ddlOrdSupplier.SelectedValue == "300" ||
                //        ddlOrdSupplier.SelectedValue == "620" || ddlOrdSupplier.SelectedValue == "790" ||
                //        ddlOrdSupplier.SelectedValue == "830" || ddlOrdSupplier.SelectedValue == "360" ||
                //        ddlOrdSupplier.SelectedValue == "400")
                //    {
                //        if (!(txtOrdBranch.Text == "MORI GATE" || txtOrdBranch.Text == "CALCUTTA"))
                //        {
                //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Transaction Not Allowed For This Supplier');", true);
                //            ddlOrdSupplier.SelectedValue = "0";
                //        }
                //    }
                //    else
                //    {
                //        if (!(ddlOrdTransactionType.SelectedValue == "312" || ddlOrdTransactionType.SelectedValue == "421" || 
                //            ddlOrdTransactionType.SelectedValue == "361" || ddlOrdTransactionType.SelectedValue == "461" || 
                //            ddlOrdTransactionType.SelectedValue == "451" || ddlOrdTransactionType.SelectedValue == "471"))
                //        {
                //            if (!(ddlOrdSupplier.SelectedValue == "220" || ddlOrdSupplier.SelectedValue == "550" || 
                //                ddlOrdSupplier.SelectedValue == "390" || ddlOrdSupplier.SelectedValue == "391" || 
                //                ddlOrdSupplier.SelectedValue == "320" || ddlOrdSupplier.SelectedValue == "410" || 
                //                ddlOrdSupplier.SelectedValue == "392" || ddlOrdSupplier.SelectedValue == "630"))
                //            {
                //                if (!(txtOrdBranch.Text == "MORI GATE" || txtOrdBranch.Text == "CALCUTTA"))
                //                {
                //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Transaction Not Allowed For This Supplier');", true);
                //                    ddlOrdSupplier.SelectedValue = "0";
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrdTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlOrdTransactionType.SelectedValue == "451" || ddlOrdTransactionType.SelectedValue == "452") //ddlOrdTransactionType.SelectedValue == "361" || ddlOrdTransactionType.SelectedValue == "461" || 
                {
                    lblCustomer.Visible = true;
                    ddlOrdCustomer.Visible = true;
                    ddlOrdCustomer.Enabled = true;
                    reqCustomer.Visible = true;                    

                    if (ddlOrdTransactionType.Items[ddlOrdTransactionType.SelectedIndex].ToString() == "INDENT-STU")
                    {
                        BindOrdSupplier();
                        BindAllIndentSTUCustomers(strOrdBranchCode);                        
                    }
                    else
                    {
                        BindAllIndentRegularCustomers(strOrdBranchCode);
                        ddlOrdSupplier.Items.Clear();
                        ImpalLibrary oCommon = new ImpalLibrary();
                        ddlOrdSupplier.DataSource = oCommon.GetDropDownListValues("DirectPurchaseOrderHexSocket");
                        ddlOrdSupplier.DataTextField = "DisplayText";
                        ddlOrdSupplier.DataValueField = "DisplayValue";
                        ddlOrdSupplier.DataBind();
                    }
                }
                else
                {
                    lblCustomer.Visible = false;
                    ddlOrdCustomer.Visible = false;
                    reqCustomer.Visible = false;
                    BindAllCustomers(strOrdBranchCode);
                    BindOrdSupplier();
                }

                ddlOrdCustomer.SelectedIndex = 0;
                txtOrdCustomerAddress1.Text = "";
                txtOrdCustomerAddress2.Text = "";
                txtOrdCustomerAddress4.Text = "";
                txtOrdCustomerCode.Text = "";
                txtOrdAddress3.Text = "";
                txtOrdCustomerLocation.Text = "";

                if ((ddlOrdTransactionType.SelectedValue != "0") && (ddlOrdSupplier.SelectedValue != "0"))
                {
                    if (!(ddlOrdTransactionType.SelectedValue == "451" || ddlOrdTransactionType.SelectedValue == "452"))
                    {
                        if (ddlOrdSupplier.SelectedValue != "220" && ddlOrdSupplier.SelectedValue != "550" && ddlOrdSupplier.SelectedValue != "390" && ddlOrdSupplier.SelectedValue != "391" && ddlOrdSupplier.SelectedValue != "320" && ddlOrdSupplier.SelectedValue != "410" && ddlOrdSupplier.SelectedValue != "392" && ddlOrdSupplier.SelectedValue != "630")
                        {
                            if (!(txtOrdBranch.Text == "MORI GATE" || txtOrdBranch.Text == "CALCUTTA" || ddlOrdTransactionType.SelectedValue == "202"))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Transaction Not Allowed For This Supplier');", true);
                                ddlOrdSupplier.SelectedValue = "0";
                            }
                        }
                        else
                        {
                            //  ddlOrdSupplier.SelectedValue = "0";
                        }
                    }
                }
                BindGridItemsFooter();
                UpdatePanel1.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        protected void ddlOrdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                FillOrdCustomer();
                BindGridItemsFooter();
                if (GridView1.FooterRow != null)
                {
                    Button btnAddRow = (Button)GridView1.FooterRow.FindControl("btnAdd");
                    if (btnAddRow != null)
                    {
                        btnAddRow.Attributes.Add("OnClick", "return funDirectPOBtnAddRow();");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        protected void ddlOrdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSupplierPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            TextBox txtItemcode = null;
            TextBox txtCurrentSearch = null;
            DropDownList ddl = null;
            IMPALLibrary.Transactions.InwardTransactions inwardTransactions = new IMPALLibrary.Transactions.InwardTransactions();

            try
            {
                DropDownList ddlSelected = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddlSelected.NamingContainer;
                if (row.RowType == DataControlRowType.Footer)
                {

                }
                else if (row.RowType == DataControlRowType.DataRow)
                {
                    ddl = (DropDownList)GridView1.Rows[row.RowIndex].FindControl("ddlSupplierPartNumber");
                    TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrderItem_PO_Quantity");

                    bool isExisting = CheckExisting(ddl.SelectedValue);

                    if (isExisting)
                    {                        
                        txtItemcode = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrdItemCode");
                        txtCurrentSearch = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrdSupplierPartNo");
                        TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrdApplicationSegmentCode");
                        TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrdVehicleTypeCode");
                        TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrdPackingQuantity");
                        
                        Button btnAddRow = (Button)GridView1.FooterRow.FindControl("btnAdd");

                        int ItemPriceCnt = 0;
                        ItemPriceCnt = inwardTransactions.CheckBranchItemPrice(ddl.SelectedValue, strOrdBranchCode);

                        if (ItemPriceCnt <= 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # " + ddl.SelectedItem.Text + " is missing in Branch Item Price Master. Please add the same');", true);
                            txtCurrentSearch.Text = "";
                            BtnSubmit.Enabled = false;
                            btnAddRow.Enabled = false;
                            ddl.SelectedIndex = 0;
                            return;
                        }

                        BtnSubmit.Enabled = true;
                        btnAddRow.Enabled = true;
                        using (ObjectDataSource obj = new ObjectDataSource())
                        {
                            obj.DataObjectTypeName = "IMPALLibrary.SupplierOrdPartNumberDetails";
                            obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                            obj.SelectMethod = "GetSupplierOrdPartNumberDetails";
                            //obj.SelectParameters.Add("strSuplierPartNo", TypeCode.String, txtCurrentSearch.Text.Trim());

                            obj.SelectParameters.Add("strSuplierPartNo", TypeCode.String, ddl.SelectedItem.Text.Trim());
                            obj.SelectParameters.Add("strSuplierCode", TypeCode.String, ddlOrdSupplier.SelectedValue);
                            obj.DataBind();
                            SupplierOrdPartNumberDetails objSection = new SupplierOrdPartNumberDetails();
                            object[] objCustomerSections = new object[0];
                            objCustomerSections = (object[])obj.Select();
                            objSection = (SupplierOrdPartNumberDetails)objCustomerSections[0];

                            txtOrdApplicationSegmentCode.Text = objSection.Application_Segment_Code.ToString();
                            txtOrdVehicleTypeCode.Text = objSection.Vehicle_Type_Code.ToString();
                            txtOrdPackingQuantity.Text = objSection.Packing_Quantity.ToString();
                        }

                        if (ddl.SelectedValue != "0")
                            txtItemcode.Text = ddl.SelectedValue;
                        else
                        {
                            txtItemcode.Text = "";
                            txtOrdApplicationSegmentCode.Text = "";
                            txtOrdVehicleTypeCode.Text = "";
                            txtOrdPackingQuantity.Text = "";
                        }

                        ddl.Focus();
                        txtOrderItem_PO_Quantity.Focus();
                    }
                    else
                    {
                        ddl.SelectedIndex = 0;
                        txtOrderItem_PO_Quantity.Enabled = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # already exists...');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrdBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strOrdBranchCode = ddlOrdBranchName.SelectedValue;
                ViewState["strOrdBranchCode"] = strOrdBranchCode.ToString();
                BindOrdPO_Number();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AddNewRow("Add");
                if (GridView1.FooterRow != null)
                {
                    Button btnAddRow = (Button)GridView1.FooterRow.FindControl("btnAdd");
                    if (!(bool)(Session["RoleCode"].ToString().Equals("BEDP")))
                    {
                        btnAddRow.Enabled = false;
                    }
                    if (btnAddRow != null)
                    {
                        btnAddRow.Attributes.Add("OnClick", "return funDirectPOBtnAddRow();");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                Button btnSearch = (Button)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)btnSearch.Parent.Parent);
                TextBox txtCurrentSearch = (TextBox)grdrDropDownRow.FindControl("txtOrdSupplierPartNo");
                DropDownList ddlSupplierPartNumber = (DropDownList)grdrDropDownRow.FindControl("ddlSupplierPartNumber");

                TextBox txtOrdSupplierPartNo = (TextBox)grdrDropDownRow.FindControl("txtOrdSupplierPartNo");
                TextBox txtItemCode = (TextBox)grdrDropDownRow.FindControl("txtOrdItemCode");
                TextBox txtOrdApplicationSegmentCode = (TextBox)grdrDropDownRow.FindControl("txtOrdApplicationSegmentCode");
                TextBox txtOrdVehicleTypeCode = (TextBox)grdrDropDownRow.FindControl("txtOrdVehicleTypeCode");
                TextBox txtOrdPackingQuantity = (TextBox)grdrDropDownRow.FindControl("txtOrdPackingQuantity");
                TextBox txtOrderItem_PO_Quantity = (TextBox)grdrDropDownRow.FindControl("txtOrderItem_PO_Quantity");
                DropDownList ddlOrdItemStatus = (DropDownList)grdrDropDownRow.FindControl("ddlOrdItemStatus");
               // TextBox txtSchedule_Date = (TextBox)grdrDropDownRow.FindControl("txtSchedule_Date");
                TextBox txtValid_Days = (TextBox)grdrDropDownRow.FindControl("txtValid_Days");
                DropDownList ddlOrdScheduleStatus = (DropDownList)grdrDropDownRow.FindControl("ddlOrdScheduleStatus");
                DropDownList ddlIndentBranch = (DropDownList)grdrDropDownRow.FindControl("ddlIndentBranch");

                if (btnSearch.Text == "Reset")
                {
                    ddlSupplierPartNumber.Visible = false;
                    txtCurrentSearch.Visible = true;
                    txtCurrentSearch.Text = "";
                    txtItemCode.Text = "";
                    txtOrdApplicationSegmentCode.Text = "";
                    txtOrdVehicleTypeCode.Text = "";
                    txtOrdPackingQuantity.Text = "";
                    txtOrderItem_PO_Quantity.Text = "";
                    ddlOrdItemStatus.SelectedValue = "A";
                    txtValid_Days.Text = "0";
                    ddlOrdScheduleStatus.SelectedValue = "A";
                    ddlIndentBranch.Text = Session["BranchCode"].ToString();
                    btnSearch.Text = "Search";
                }
                else
                {
                    if (txtCurrentSearch != null)
                    {
                        using (ObjectDataSource obj = new ObjectDataSource())
                        {
                            if (ddlOrdSupplier.SelectedValue == "0")
                            {
                                AddSelect(ddlSupplierPartNumber);
                            }
                            else
                            {
                                obj.DataObjectTypeName = "IMPALLibrary.ddlOrdPartNumberType";
                                obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                                obj.SelectMethod = "GetSupplierOrdPartNumberSearch";
                                obj.SelectParameters.Add("strBranchCode", TypeCode.String, Session["BranchCode"].ToString());
                                obj.SelectParameters.Add("strSuplierPartNo", TypeCode.String, txtCurrentSearch.Text.Trim());
                                obj.SelectParameters.Add("strSuplierCode", TypeCode.String, ddlOrdSupplier.SelectedValue);
                                obj.DataBind();
                                ddlSupplierPartNumber.DataSource = obj;
                                ddlSupplierPartNumber.DataTextField = "Supplier_Part_Number";
                                ddlSupplierPartNumber.DataValueField = "Item_Code";
                                ddlSupplierPartNumber.DataBind();
                                AddSelect(ddlSupplierPartNumber);

                            }

                            txtOrderItem_PO_Quantity.Enabled = true;
                            ddlSupplierPartNumber.Visible = true;
                            txtCurrentSearch.Visible = false;
                            btnSearch.Text = "Reset";

                        }
                    }
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
            string PO_NUMBER = "";
            BtnSubmit.Enabled = false;
            DirectPOHeader objValue = new DirectPOHeader();
            CustomerSectionFields objCustomer = new CustomerSectionFields();

            try
            {
				string TransType = string.Empty;
                AddNewRow("BtnSubmit");

                if (Page.IsValid)
                {
                    dt = (DataTable)ViewState["CurrentTable"];
                    if (GridView1.Rows.Count >= 1)
                    {
                        if (ddlOrdTransactionType.SelectedValue == "452")
                            TransType = "451";
                        else
                            TransType = ddlOrdTransactionType.SelectedValue;

                        objValue.Branch_Code = strOrdBranchCode.ToString();
                        objValue.PO_Indent_Date = txtOrdIndentDate.Text;
                        objValue.Carrier = txtOrdCarrier.Text;
                        objValue.customercode = ddlOrdCustomer.SelectedValue;
                        objValue.Transaction_Type_Code = TransType;
                        objValue.Supplier_Code = ddlOrdSupplier.SelectedValue;
                        objValue.PO_Number = "";
                        objValue.Destination = txtOrdDestination.Text;

                        objValue.Reference_Date = txtOrdRefIndentDate.Text;
                        objValue.Reference_Number = txtOrdRefIndentNumber.Text;
                        objValue.Remarks = txtOrdCarrierInfoRemarks.Text;
                        objValue.Road_Permit_Date = txtOrdRdPermitDate.Text;
                        objValue.Road_Permit_Number = txtOrdRdPermitNumber.Text;

                        objCustomer.Address1 = txtOrdCustomerAddress1.Text;
                        objCustomer.Address2 = txtOrdCustomerAddress2.Text;
                        objCustomer.Address4 = txtOrdCustomerAddress4.Text;
                        objCustomer.Address3 = txtOrdAddress3.Text;
                        //objCustomer.Tin_No = txtOrdTin_Number.Text;
                        objCustomer.Customer_Code = txtOrdCustomerCode.Text;
                        objCustomer.Location = txtOrdCustomerLocation.Text;

                        DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                        PO_NUMBER = objSaveHeaderAndItem.Ins_DirectPOHeaderAndItems(objValue, objCustomer, (DataTable)ViewState["CurrentTable"]);
                        Session["PONumber"] = PO_NUMBER;

                        Server.ClearError();
                        Response.Redirect("~/Reports/Ordering/Listing/PurchaseOrderReprint.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Please Add atleast one Item details.');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("IndentSupplimentaryOrder.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    var ddl = (DropDownList)e.Row.FindControl("ddlSupplierPartNumber");
                    var ddlBranch = (DropDownList)e.Row.FindControl("ddlIndentBranch");
                    var txtItemCode = (TextBox)e.Row.FindControl("txtOrdItemCode");
                    var txtScheduleDate = (TextBox)e.Row.FindControl("txtSchedule_Date");


                    string strCurrentDate, strTempDate;
                    strCurrentDate = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                    string strDay = DateTime.Now.ToString("dd").ToString();
                    string strMonth = DateTime.Now.ToString("MM").ToString();
                    string strYear = DateTime.Now.ToString("yyyy").ToString();
                    Int32 intDay;
                    Int32 intMonth;
                    Int32 intYear;
                    intDay = Convert.ToInt32(strDay);
                    intMonth = Convert.ToInt32(strMonth);
                    intYear = Convert.ToInt32(strYear);

                    if (intDay > 19)
                    {
                        if (intMonth == 12)
                        {
                            txtScheduleDate.Text = "01/" + (intYear + 1).ToString();
                            strTempDate = "31/01/" + (intYear + 1).ToString();
                        }
                        else
                        {
                            if (intMonth < 9)
                            {
                                txtScheduleDate.Text = "0" + (intMonth + 1).ToString() + "/" + (intYear).ToString();
                                int daysInMonth = System.DateTime.DaysInMonth(intYear, intMonth + 1);
                                strTempDate = daysInMonth.ToString() + "/0" + (intMonth + 1).ToString() + "/" + (intYear).ToString();

                            }
                            else
                            {
                                txtScheduleDate.Text = (intMonth + 1).ToString() + "/" + (intYear).ToString();
                                int daysInMonth = System.DateTime.DaysInMonth(intYear, intMonth + 1);
                                strTempDate = daysInMonth.ToString() + "/" + (intMonth + 1).ToString() + "/" + (intYear).ToString();
                            }

                        }
                    }
                    else
                    {

                        if (intMonth < 10)
                        {
                            txtScheduleDate.Text = "0" + (intMonth).ToString() + "/" + (intYear).ToString();
                            int daysInMonth = System.DateTime.DaysInMonth(intYear, intMonth);
                            strTempDate = daysInMonth.ToString() + "/0" + (intMonth).ToString() + "/" + (intYear).ToString();

                        }
                        else
                        {
                            txtScheduleDate.Text = (intMonth).ToString() + "/" + (intYear).ToString();
                            int daysInMonth = System.DateTime.DaysInMonth(intYear, intMonth);
                            strTempDate = daysInMonth.ToString() + "/" + (intMonth).ToString() + "/" + (intYear).ToString();

                        }

                    }
                    string strMonthYear = strTempDate.ToString().Substring(3);
                    txtScheduleDate.Text = strMonthYear.ToString();
                    // Session["strTempDate"] = strTempDate.ToString();


                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                        objBranch.TypeName = "IMPALLibrary.Branches";
                        objBranch.SelectMethod = "GetAllBranch";
                        objBranch.DataBind();
                        ddlBranch.DataSource = objBranch;
                        ddlBranch.DataTextField = "BranchName";
                        ddlBranch.DataValueField = "BranchCode";
                        ddlBranch.DataBind();
                        AddSelect(ddlBranch);

                        if (strOrdBranchCode != null)
                        {
                            ddlBranch.SelectedValue = strOrdBranchCode.ToString();
                        }
                    }

                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.RowIndex >= 0)
                {
                    dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();


                    if (GridView1.Rows.Count >= 1)
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {

                    
                            DataRow dr = dt.NewRow();

                            DropDownList ddlSupplierPartNumber = (DropDownList)GridView1.Rows[i].FindControl("ddlSupplierPartNumber");
                            TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[i].FindControl("txtOrdSupplierPartNo");
                            TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[i].FindControl("txtOrdApplicationSegmentCode");
                            TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[i].FindControl("txtOrdVehicleTypeCode");
                            TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[i].FindControl("txtOrdPackingQuantity");
                            TextBox txtItemCode = (TextBox)GridView1.Rows[i].FindControl("txtOrdItemCode");
                            TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[i].FindControl("txtOrderItem_PO_Quantity");
                            DropDownList ddlOrdItemStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlOrdItemStatus");
                            TextBox txtSchedule_Date = (TextBox)GridView1.Rows[i].FindControl("txtSchedule_Date");
                            TextBox txtValid_Days = (TextBox)GridView1.Rows[i].FindControl("txtValid_Days");
                            DropDownList ddlOrdScheduleStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlOrdScheduleStatus");
                            DropDownList ddlIndentBranch = (DropDownList)GridView1.Rows[i].FindControl("ddlIndentBranch");
                            Button btnSearch = (Button)GridView1.Rows[i].FindControl("btnSearch");

                            if (ddlSupplierPartNumber.SelectedItem == null)
                            {
                                dr["Supplier_Part_Number"] = txtOrdSupplierPartNo.Text;
                                dr["Completed"] = "Yes";
                            }
                            else
                            {
                                dr["Supplier_Part_Number"] = ddlSupplierPartNumber.SelectedItem;
                                dr["Completed"] = "No";
                            }
                            
                            dr["Application_Segment_Code"] = txtOrdApplicationSegmentCode.Text;
                            dr["Vehicle_Type_Code"] = txtOrdVehicleTypeCode.Text;
                            dr["Packing_Quantity"] = txtOrdPackingQuantity.Text;
                            dr["Item_Code"] = txtItemCode.Text;
                            dr["OrderItem_PO_Quantity"] = txtOrderItem_PO_Quantity.Text;
                            dr["OrderItem_Status"] = ddlOrdItemStatus.SelectedValue;
                            dr["Schedule_Date"] = txtSchedule_Date.Text;
                            dr["Valid_Days"] = txtValid_Days.Text;
                            dr["Schedule_Status"] = ddlOrdScheduleStatus.SelectedValue;
                            dr["Indent_Branch"] = ddlIndentBranch.SelectedValue;
                            
                            

                            dt.Rows.Add(dr);
                        }

                    }


                    if (GridView1.ShowFooter == true && txtOrd_PONumber.Visible == true)
                    {
                        dt.Rows.RemoveAt(e.RowIndex);
                        ViewState["CurrentTable"] = dt;
                        GridView1.DataSource = ViewState["CurrentTable"];

                        if (dt.Rows.Count > 0 && txtOrd_PONumber.Visible == true)
                        {
                            GridView1.ShowFooter = true;
                            GridView1.DataBind();
                            SetPreviousDataAddMode(true);
                            //HideDllItemCodeDropDown(); 
                        }
                        else if (dt.Rows.Count > 0 && ddlOrd_PONumber.Visible == true)
                        {
                            GridView1.ShowFooter = false;
                            GridView1.DataBind();
                        }
                        else
                        {
                            dt = null;
                            dt = new DataTable();
                            ViewState["CurrentTable"] = dt;
                            BindGridItemsFooter();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Item Details canot be delete in the View mode or No item records.');", true);

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

     
       
        #endregion

        #region   User defined methods  

        private void InitializeControl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlOrd_PONumber.Visible = false;
                txtOrd_PONumber.Visible = true;
                lblCustomer.Visible = false;
                ddlOrdCustomer.Visible = false;
                reqCustomer.Visible = false;
                ddlOrdStatus.Visible = false;
                lblStatus.Visible = false;
                txtOrdIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrdIndentDate.Enabled = false;
               
                    
                    if ((string)Session["RoleCode"] == "BEDP")
                    {
                             using (ObjectDataSource obj = new ObjectDataSource())
                            {
                            obj.DataObjectTypeName = "IMPALLibrary.Branch";
                            obj.TypeName = "IMPALLibrary.Branches";
                            obj.SelectMethod = "GetBranchName";
                            obj.SelectParameters.Add("strBranchCode",strOrdBranchCode);
                            obj.DataBind();

                            Branch objSection = new Branch();
                            object[] objCustomerSections = new object[0];
                            objCustomerSections = (object[])obj.Select();
                            objSection = (Branch)objCustomerSections[0];
                            if ((objSection.BranchName is object))
                            {
                                txtOrdBranch.Text = objSection.BranchName.ToString();
                                txtOrdBranch.Enabled = false;
                            } 
                         }
                    }
                    else
                    {
                        using (ObjectDataSource objBranch = new ObjectDataSource())
                        {
                            objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                            objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                            objBranch.SelectMethod = "GetAllOrdBranches";
                            objBranch.DataBind();
                            ddlOrdBranchName.DataSource = objBranch;
                            ddlOrdBranchName.DataTextField = "BranchName";
                            ddlOrdBranchName.DataValueField = "BranchCode";
                            ddlOrdBranchName.DataBind();
                            AddSelect(ddlOrdBranchName);
                            ddlOrdBranchName.SelectedValue = strOrdBranchCode;
                        }                                                                       
                    }
                BindOrdTransactionType();
                BindOrdSupplier();
                BindOrdPO_Number();
                BindAllCustomers(strOrdBranchCode);
                BindGridItemsFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);    
            }
        }

        private void BindOrdPO_Number()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //string strBranchCode = Session["BranchCode"].ToString();
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.ddlOrdPO_NumberType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllOrdPO_Number";
                    obj.SelectParameters.Add("strBranchCode", strOrdBranchCode);
                    obj.DataBind();
                    ddlOrd_PONumber.DataSource = obj;
                    ddlOrd_PONumber.DataTextField = "PO_Number";
                    ddlOrd_PONumber.DataValueField = "PO_Number";
                    ddlOrd_PONumber.DataBind();

                    AddSelect(ddlOrd_PONumber);
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        private void BindOrdTransactionType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlOrdTransactionType.DataSource = oCommon.GetDropDownListValues("DirectPurchaseOrder");
                ddlOrdTransactionType.DataTextField = "DisplayText";
                ddlOrdTransactionType.DataValueField = "DisplayValue";
                ddlOrdTransactionType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindOrdSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlTransactionType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllOrdSupplier";
                    obj.DataBind();
                    ddlOrdSupplier.DataSource = obj;
                    ddlOrdSupplier.DataTextField = "Supplier_Name";
                    ddlOrdSupplier.DataValueField = "Supplier_Code";
                    ddlOrdSupplier.DataBind();
                    AddSelect(ddlOrdSupplier);
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
                    obj.SelectMethod = "GetAllCustomers";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    ddlOrdCustomer.DataSource = obj;
                    ddlOrdCustomer.DataTextField = "Customer_Name";
                    ddlOrdCustomer.DataValueField = "Customer_Code";
                    ddlOrdCustomer.DataBind();

                  //  if (!IsPostBack)
                  //  {
                        AddSelect(ddlOrdCustomer);
                  //  }

                    if (ddlOrdCustomer.Visible == true)
                    {
                        if (ddlOrdCustomer.SelectedValue != "0")
                        {
                            FillOrdCustomer();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

		private void BindAllIndentRegularCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlCustomerType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllIndentRegularCustomers";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    ddlOrdCustomer.DataSource = obj;
                    ddlOrdCustomer.DataTextField = "Customer_Name";
                    ddlOrdCustomer.DataValueField = "Customer_Code";
                    ddlOrdCustomer.DataBind();

                    //  if (!IsPostBack)
                    //  {
                    AddSelect(ddlOrdCustomer);
                    //  }

                    if (ddlOrdCustomer.Visible == true)
                    {
                        if (ddlOrdCustomer.SelectedValue != "0")
                        {
                            FillOrdCustomer();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindAllIndentSTUCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlCustomerType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllIndentSTUCustomers";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    ddlOrdCustomer.DataSource = obj;
                    ddlOrdCustomer.DataTextField = "Customer_Name";
                    ddlOrdCustomer.DataValueField = "Customer_Code";
                    ddlOrdCustomer.DataBind();

                    //  if (!IsPostBack)
                    //  {
                    AddSelect(ddlOrdCustomer);
                    //  }

                    if (ddlOrdCustomer.Visible == true)
                    {
                        if (ddlOrdCustomer.SelectedValue != "0")
                        {
                            FillOrdCustomer();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        private void FillOrdCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                string strCustomerCode = ddlOrdCustomer.SelectedValue;
                using (ObjectDataSource obj = new ObjectDataSource())
                {


                    obj.DataObjectTypeName = "IMPALLibrary.CustomerSectionFields";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "FillOrdDirectPO_CustomerSection";
                    obj.SelectParameters.Add("strBranch_Code", strOrdBranchCode);
                    obj.SelectParameters.Add("strCustomer_Code", strCustomerCode);

                    obj.DataBind();

                    CustomerSectionFields objSection = new CustomerSectionFields();

                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();

                    objSection = (CustomerSectionFields)objCustomerSections[0];

                    if (objSection.Customer_Code is object)
                    {
                        txtOrdCustomerCode.Text = objSection.Customer_Code.ToString();
                        txtOrdCustomerAddress1.Text = objSection.Address1.ToString();
                        txtOrdCustomerAddress2.Text = objSection.Address2.ToString();

                        txtOrdCustomerAddress4.Text = objSection.Address4.ToString();
                        txtOrdCustomerLocation.Text = objSection.Location.ToString();
                        // txtOrdTin_Number.Text = objSection.Tin_No.ToString();
                        txtOrdAddress3.Text = objSection.Address3.ToString();
                        DisableControls(true);
                       
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        private void AddSelect(DropDownList ddl)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
           
        }

        private void BindGridItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strPO_Number = ddlOrd_PONumber.SelectedValue;
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.DirectPOItems";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetOrdDirectPO_Items";
                    obj.SelectParameters.Add("strPO_Number", strPO_Number);
                    obj.SelectParameters.Add("Indicator", "D");
                    obj.DataBind();

                    if (txtOrd_PONumber.Visible == true)
                    {
                        GridView1.Columns[13].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[13].Visible = false;
                    }


                    dt.Rows.Clear();

                    dt.Columns.Add(new DataColumn("S_No", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Part_Number", typeof(string)));
                    dt.Columns.Add(new DataColumn("Application_Segment_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("Vehicle_Type_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("Packing_Quantity", typeof(string)));
                    dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("OrderItem_PO_Quantity", typeof(string)));
                    dt.Columns.Add(new DataColumn("OrderItem_Status", typeof(string)));
                    dt.Columns.Add(new DataColumn("Schedule_Date", typeof(string)));
                    dt.Columns.Add(new DataColumn("Valid_Days", typeof(string)));
                    dt.Columns.Add(new DataColumn("Schedule_Status", typeof(string)));
                    //   dt.Columns.Add(new DataColumn("Supplier_Line_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("Indent_Branch", typeof(string)));
                    dt.Columns.Add(new DataColumn("Completed", typeof(string)));

                    DirectPurchaseOrders objDS = new DirectPurchaseOrders();
                    DataSet ds = new DataSet();
                    ds = objDS.GetDSOrdDirectPO_Items(ddlOrd_PONumber.SelectedValue, "D", Session["BranchCode"].ToString());

                    DataRow dr = null;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr["S_No"] = i + 1;
                        dr["Supplier_Part_Number"] = ds.Tables[0].Rows[i]["Supplier_Part_Number"].ToString();
                        dr["Application_Segment_Code"] = ds.Tables[0].Rows[i]["Application_Segment_Code"].ToString();
                        dr["Vehicle_Type_Code"] = ds.Tables[0].Rows[i]["Vehicle_Type_Code"].ToString();
                        dr["Packing_Quantity"] = ds.Tables[0].Rows[i]["Packing_Quantity"].ToString();
                        dr["Item_Code"] = ds.Tables[0].Rows[i]["Item_Code"].ToString();
                        dr["OrderItem_PO_Quantity"] = ds.Tables[0].Rows[i]["PO_Quantity"].ToString();
                        dr["OrderItem_Status"] = ds.Tables[0].Rows[i]["Ord_Status"].ToString();
                        dr["Schedule_Date"] = ds.Tables[0].Rows[i]["Schedule_Date"].ToString();
                        dr["Valid_Days"] = ds.Tables[0].Rows[i]["Valid_Days"].ToString();
                        dr["Schedule_Status"] = ds.Tables[0].Rows[i]["Sche_Status"].ToString();
                        dr["Indent_Branch"] = ds.Tables[0].Rows[i]["Sche_Indent_Branch"].ToString();
                        dr["Completed"] = "";
                        dt.Rows.Add(dr);
                    }
                    ViewState["CurrentTable"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    SetPreviousData();
                    hdnFreezeRowCnt.Value = "1";
                    // }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        private void SetPreviousData()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int rowIndex = 0;
            try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        // System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                            Label lblSupplier_Part_Number = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSupplier_Part_Number");
                            Label lblItemCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdItemCode");
                            Label lblOrdApplicationSegmentCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdApplicationSegmentCode");
                            Label lblOrdVehicleTypeCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdVehicleTypeCode");
                            Label lblOrdPackingQuantity = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdPackingQuantity");
                            Label lblOrderItem_PO_Quantity = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_PO_Quantity");
                            Label lblOrderItem_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_Status");
                            Label lblSchedule_Date = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Date");
                            Label lblValid_Days = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdValid_Days");
                            Label lblSchedule_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Status");
                            Label lblIndent_Branch = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdIndent_Branch");
                            Button btnSearch = (Button)GridView1.Rows[rowIndex].FindControl("btnSearch");

                            DropDownList ddlSupplierPartNumber = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlSupplierPartNumber");
                            TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdSupplierPartNo");
                            TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                            TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdApplicationSegmentCode");
                            TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdVehicleTypeCode");
                            TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdPackingQuantity");
                            TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrderItem_PO_Quantity");
                            DropDownList ddlOrdItemStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdItemStatus");
                            TextBox txtSchedule_Date = (TextBox)GridView1.Rows[rowIndex].FindControl("txtSchedule_Date");
                            TextBox txtValid_Days = (TextBox)GridView1.Rows[rowIndex].FindControl("txtValid_Days");
                            DropDownList ddlOrdScheduleStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdScheduleStatus");
                            DropDownList ddlIndentBranch = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlIndentBranch");

                            btnSearch.Visible = false;
                            ddlSupplierPartNumber.Visible = false;
                            txtOrdSupplierPartNo.Visible = false;
                            txtOrdApplicationSegmentCode.Visible = false;
                            txtOrdVehicleTypeCode.Visible = false;
                            txtOrdPackingQuantity.Visible = false;
                            txtItemCode.Visible = false;
                            txtOrderItem_PO_Quantity.Visible = false;
                            ddlOrdItemStatus.Visible = false;
                            txtSchedule_Date.Visible = false;
                            txtValid_Days.Visible = false;
                            ddlOrdScheduleStatus.Visible = false;
                            ddlIndentBranch.Visible = false;

                            lblSupplier_Part_Number.Text = dt.Rows[i]["Supplier_Part_Number"].ToString();
                            lblOrdApplicationSegmentCode.Text = dt.Rows[i]["Application_Segment_Code"].ToString();
                            lblOrdVehicleTypeCode.Text = dt.Rows[i]["Vehicle_Type_Code"].ToString();
                            lblOrdPackingQuantity.Text = dt.Rows[i]["Packing_Quantity"].ToString();
                            lblItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                            lblOrderItem_PO_Quantity.Text = dt.Rows[i]["OrderItem_PO_Quantity"].ToString();
                            lblOrderItem_Status.Text = dt.Rows[i]["OrderItem_Status"].ToString();
                            lblSchedule_Date.Text = dt.Rows[i]["Schedule_Date"].ToString();
                            lblValid_Days.Text = dt.Rows[i]["Valid_Days"].ToString();
                            lblSchedule_Status.Text = dt.Rows[i]["Schedule_Status"].ToString();
                            lblIndent_Branch.Text = dt.Rows[i]["Indent_Branch"].ToString();


                            var PnlViewSupplierDetails = (Panel)GridView1.Rows[rowIndex].FindControl("PnlViewSupplierDetails");
                            var imgViewSupplierLine = (Image)GridView1.Rows[rowIndex].FindControl("imgViewSupplierLine");

                            var lblSupplier_Line_Value = (Label)PnlViewSupplierDetails.FindControl("lblSupplier_Line_Value");
                            var lblApplication_Segment_Value = (Label)PnlViewSupplierDetails.FindControl("lblApplication_Segment_Value");
                            var lblVehichleTypeDescriptionValue = (Label)PnlViewSupplierDetails.FindControl("lblVehichleTypeDescriptionValue");

                            if (lblItemCode.Text != "")
                            {
                                imgViewSupplierLine.Style.Add("display", "inline");

                                using (ObjectDataSource objBranch = new ObjectDataSource())
                                {
                                    objBranch.DataObjectTypeName = "IMPALLibrary.SupplierOrdLineDetails";
                                    objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                                    objBranch.SelectMethod = "GetOrdSupplierLineDetails";
                                    objBranch.SelectParameters.Add("ItemCode", lblItemCode.Text.Trim());
                                    objBranch.DataBind();

                                    SupplierOrdLineDetails objSection = new SupplierOrdLineDetails();
                                    object[] objCustomerSections = new object[0];
                                    objCustomerSections = (object[])objBranch.Select();
                                    objSection = (SupplierOrdLineDetails)objCustomerSections[0];

                                    lblSupplier_Line_Value.Text = objSection.Supp_Short_description.ToString();
                                    lblApplication_Segment_Value.Text = objSection.Appln_Segment_Description.ToString();
                                    lblVehichleTypeDescriptionValue.Text = objSection.Vehicle_Type_Description.ToString();
                                }
                            }
                            rowIndex++;
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
            
        }

        private void SetPreviousDataAddMode(bool isDelete)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            int rowIndex = 0;
            try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        // System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                            Label lblSupplier_Part_Number = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSupplier_Part_Number");
                            Label lblOrdApplicationSegmentCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdApplicationSegmentCode");
                            Label lblOrdVehicleTypeCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdVehicleTypeCode");
                            Label lblOrdPackingQuantity = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdPackingQuantity");
                            Label lblItemCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdItemCode");
                            Label lblOrderItem_PO_Quantity = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_PO_Quantity");
                            Label lblOrderItem_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_Status");
                            Label lblSchedule_Date = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Date");
                            Label lblValid_Days = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdValid_Days");
                            Label lblSchedule_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Status");
                            Label lblIndent_Branch = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdIndent_Branch");

                            DropDownList ddlSupplierPartNumber = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlSupplierPartNumber");
                            TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdSupplierPartNo");
                            TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdApplicationSegmentCode");
                            TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdVehicleTypeCode");
                            TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdPackingQuantity");
                            TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                            TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrderItem_PO_Quantity");
                            DropDownList ddlOrdItemStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdItemStatus");
                            TextBox txtSchedule_Date = (TextBox)GridView1.Rows[rowIndex].FindControl("txtSchedule_Date");
                            TextBox txtValid_Days = (TextBox)GridView1.Rows[rowIndex].FindControl("txtValid_Days");
                            DropDownList ddlOrdScheduleStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdScheduleStatus");
                            DropDownList ddlIndentBranch = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlIndentBranch");
                            Button btnSearch = (Button)GridView1.Rows[rowIndex].FindControl("btnSearch");

                            if (GridView1.FooterRow != null)
                            {                                
                                if (btnSearch != null)
                                {
                                    btnSearch.Attributes.Add("OnClick", "return funDirectPOBtnSearch();");
                                }
                            }

                            ddlSupplierPartNumber.Visible = false;
                            txtOrdApplicationSegmentCode.Visible = true;
                            txtOrdVehicleTypeCode.Visible = true;
                            txtOrdPackingQuantity.Visible = true;
                            txtItemCode.Visible = true;
                            txtOrdSupplierPartNo.Visible = true;
                            txtOrderItem_PO_Quantity.Visible = true;
                            ddlOrdItemStatus.Visible = true;
                            txtSchedule_Date.Visible = true;
                            txtValid_Days.Visible = true;
                            ddlOrdScheduleStatus.Visible = true;
                            ddlIndentBranch.Visible = true;

                            lblSupplier_Part_Number.Visible = false;
                            lblOrdApplicationSegmentCode.Visible = false;
                            lblOrdVehicleTypeCode.Visible = false;
                            lblOrdPackingQuantity.Visible = false;
                            lblItemCode.Visible = false;
                            lblOrderItem_PO_Quantity.Visible = false;
                            lblOrderItem_Status.Visible = false;
                            lblSchedule_Date.Visible = false;
                            lblValid_Days.Visible = false;
                            lblSchedule_Status.Visible = false;
                            lblIndent_Branch.Visible = false;

                            txtOrdSupplierPartNo.Text = dt.Rows[i]["Supplier_Part_Number"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                            txtOrderItem_PO_Quantity.Text = dt.Rows[i]["OrderItem_PO_Quantity"].ToString();
                            ddlOrdItemStatus.SelectedValue = dt.Rows[i]["OrderItem_Status"].ToString();
                            txtOrdApplicationSegmentCode.Text = dt.Rows[i]["Application_Segment_Code"].ToString();
                            txtOrdVehicleTypeCode.Text = dt.Rows[i]["Vehicle_Type_Code"].ToString();
                            txtOrdPackingQuantity.Text = dt.Rows[i]["Packing_Quantity"].ToString();
                            if (i > 0)
                            {
                                // txtSchedule_Date.Text = dt.Rows[i]["Schedule_Date"].ToString();
                            }

                            txtValid_Days.Text = dt.Rows[i]["Valid_Days"].ToString();
                            ddlOrdScheduleStatus.SelectedValue = dt.Rows[i]["Schedule_Status"].ToString();
                            ddlIndentBranch.Text = dt.Rows[i]["Indent_Branch"].ToString();


                            var PnlViewSupplierDetails = (Panel)GridView1.Rows[rowIndex].FindControl("PnlViewSupplierDetails");
                            var imgViewSupplierLine = (Image)GridView1.Rows[rowIndex].FindControl("imgViewSupplierLine");
                            var lblSupplier_Line_Value = (Label)PnlViewSupplierDetails.FindControl("lblSupplier_Line_Value");
                            var lblApplication_Segment_Value = (Label)PnlViewSupplierDetails.FindControl("lblApplication_Segment_Value");
                            var lblVehichleTypeDescriptionValue = (Label)PnlViewSupplierDetails.FindControl("lblVehichleTypeDescriptionValue");

                            imgViewSupplierLine.Style.Add("display", "none");

                            if (txtItemCode.Text != "")
                            {
                                using (ObjectDataSource objBranch = new ObjectDataSource())
                                {
                                    objBranch.DataObjectTypeName = "IMPALLibrary.SupplierOrdLineDetails";
                                    objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                                    objBranch.SelectMethod = "GetOrdSupplierLineDetails";
                                    objBranch.SelectParameters.Add("ItemCode", txtItemCode.Text.Trim());
                                    objBranch.DataBind();

                                    SupplierOrdLineDetails objSection = new SupplierOrdLineDetails();
                                    object[] objCustomerSections = new object[0];
                                    objCustomerSections = (object[])objBranch.Select();
                                    objSection = (SupplierOrdLineDetails)objCustomerSections[0];

                                    lblSupplier_Line_Value.Text = objSection.Supp_Short_description.ToString();
                                    lblApplication_Segment_Value.Text = objSection.Appln_Segment_Description.ToString();
                                    lblVehichleTypeDescriptionValue.Text = objSection.Vehicle_Type_Description.ToString();
                                }
                            }

                            if (isDelete)
                            {

                                if (dt.Rows[rowIndex]["Completed"].ToString() == "No")
                                {
                                    btnSearch.Text = "Reset";
                                    ddlSupplierPartNumber.Visible = true;
                                    txtOrdSupplierPartNo.Visible = false;
                                    using (ObjectDataSource obj = new ObjectDataSource())
                                    {
                                        obj.DataObjectTypeName = "IMPALLibrary.ddlOrdPartNumberType";
                                        obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                                        obj.SelectMethod = "GetSupplierOrdPartNumberSearch";
                                        obj.SelectParameters.Add("strBranchCode", TypeCode.String, Session["BranchCode"].ToString());
                                        obj.SelectParameters.Add("strSuplierPartNo", TypeCode.String, txtOrdSupplierPartNo.Text.Trim());
                                        obj.SelectParameters.Add("strSuplierCode", TypeCode.String, ddlOrdSupplier.SelectedValue);
                                        obj.DataBind();
                                        ddlSupplierPartNumber.DataSource = obj;
                                        ddlSupplierPartNumber.DataTextField = "Supplier_Part_Number";
                                        ddlSupplierPartNumber.DataValueField = "Item_Code";
                                        ddlSupplierPartNumber.DataBind();
                                        AddSelect(ddlSupplierPartNumber);

                                    }

                                    ddlSupplierPartNumber.SelectedItem.Text = dt.Rows[i]["Supplier_Part_Number"].ToString();


                                }
                                else
                                {
                                    if (txtOrdSupplierPartNo.Text != "")
                                    {
                                        txtOrdSupplierPartNo.Enabled = false;
                                        txtOrdApplicationSegmentCode.Enabled = false;
                                        txtOrdVehicleTypeCode.Enabled = false;
                                        txtOrdPackingQuantity.Enabled = false;
                                        txtItemCode.Enabled = false;
                                        txtOrderItem_PO_Quantity.Enabled = false;
                                        ddlOrdItemStatus.Enabled = false;
                                        txtSchedule_Date.Enabled = false;
                                        txtValid_Days.Enabled = false;
                                        ddlOrdScheduleStatus.Enabled = false;
                                        ddlIndentBranch.Enabled = false;
                                        btnSearch.Visible = false;
                                        imgViewSupplierLine.Style.Add("display", "inline");
                                    }
                                }
                            }

                            rowIndex++;
                        }

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
            
        }

        private void AddNewRow(string btnName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int rowIndex = 0;
            hdnFreezeRowCnt.Value = "1";
            try
            {
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
                            drCurrentRow["S_No"] = 1;
                            drCurrentRow["Valid_Days"] = 0;
                        }
                        else
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                drCurrentRow = dtCurrentTable.NewRow();
                                drCurrentRow["S_No"] = i + 1;
                                drCurrentRow["Valid_Days"] = 0;
                                Label lblSupplier_Part_Number = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSupplier_Part_Number");
                                Label lblOrdApplicationSegmentCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdApplicationSegmentCode");
                                Label lblOrdVehicleTypeCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdVehicleTypeCode");
                                Label lblOrdPackingQuantity = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdPackingQuantity");
                                Label lblItemCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdItemCode");
                                Label lblOrderItem_PO_Quantity = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_PO_Quantity");
                                Label lblOrderItem_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_Status");
                                Label lblSchedule_Date = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Date");
                                Label lblValid_Days = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdValid_Days");
                                Label lblSchedule_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Status");
                                Label lblIndent_Branch = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdIndent_Branch");

                                DropDownList ddlSupplierPartNumber = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlSupplierPartNumber");
                                TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdApplicationSegmentCode");
                                TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdVehicleTypeCode");
                                TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdPackingQuantity");
                                TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdSupplierPartNo");
                                TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");

                                TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrderItem_PO_Quantity");
                                DropDownList ddlOrdItemStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdItemStatus");
                                TextBox txtSchedule_Date = (TextBox)GridView1.Rows[rowIndex].FindControl("txtSchedule_Date");
                                TextBox txtValid_Days = (TextBox)GridView1.Rows[rowIndex].FindControl("txtValid_Days");
                                DropDownList ddlOrdScheduleStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdScheduleStatus");
                                DropDownList ddlIndentBranch = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlIndentBranch");

                                // ddlSupplierPartNumber.Visible = true;
                                txtItemCode.Visible = true;
                                txtOrderItem_PO_Quantity.Visible = true;
                                ddlOrdItemStatus.Visible = true;
                                txtSchedule_Date.Visible = true;
                                txtValid_Days.Visible = true;
                                ddlOrdScheduleStatus.Visible = true;
                                ddlIndentBranch.Visible = true;
                                lblSupplier_Part_Number.Visible = false;
                                lblItemCode.Visible = false;
                                lblOrderItem_PO_Quantity.Visible = false;
                                lblOrderItem_Status.Visible = false;
                                lblSchedule_Date.Visible = false;
                                lblValid_Days.Visible = false;
                                lblSchedule_Status.Visible = false;
                                lblIndent_Branch.Visible = false;

                                if (ddlSupplierPartNumber.SelectedIndex != -1)
                                {
                                    if (ddlSupplierPartNumber.SelectedItem.Text != "")
                                        dtCurrentTable.Rows[i - 1]["Supplier_Part_Number"] = ddlSupplierPartNumber.SelectedItem.Text;
                                    else
                                        dtCurrentTable.Rows[i - 1]["Supplier_Part_Number"] = txtOrdSupplierPartNo.Text;
                                }
                                else
                                {
                                    dtCurrentTable.Rows[i - 1]["Supplier_Part_Number"] = txtOrdSupplierPartNo.Text;
                                }

                                dtCurrentTable.Rows[i - 1]["Application_Segment_Code"] = txtOrdApplicationSegmentCode.Text;
                                dtCurrentTable.Rows[i - 1]["Vehicle_Type_Code"] = txtOrdVehicleTypeCode.Text;
                                dtCurrentTable.Rows[i - 1]["Packing_Quantity"] = txtOrdPackingQuantity.Text;
                                dtCurrentTable.Rows[i - 1]["Item_Code"] = txtItemCode.Text;
                                dtCurrentTable.Rows[i - 1]["OrderItem_PO_Quantity"] = txtOrderItem_PO_Quantity.Text;
                                dtCurrentTable.Rows[i - 1]["OrderItem_Status"] = ddlOrdItemStatus.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["Schedule_Date"] = txtSchedule_Date.Text;
                                // dtCurrentTable.Rows[i - 1]["Schedule_Date"] = txtScheduleDate.Text;                            
                                dtCurrentTable.Rows[i - 1]["Valid_Days"] = txtValid_Days.Text;
                                dtCurrentTable.Rows[i - 1]["Schedule_Status"] = ddlOrdScheduleStatus.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["Indent_Branch"] = ddlIndentBranch.SelectedValue;

                                rowIndex++;
                            }
                        }
                        if (btnName == "BtnSubmit")
                        {
                            ViewState["CurrentTable"] = dtCurrentTable;
                            ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                            hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();
                        }
                        else
                        {
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = dtCurrentTable;
                            ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                            hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();
                            GridView1.DataSource = dtCurrentTable;
                            GridView1.DataBind();
                        }
                        
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }
                SetPreviousDataAddMode(false);
                HideDllItemCodeDropDown();                 
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        private bool CheckExisting(string partNumber)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                DropDownList ddlSupplierPartNumber = (DropDownList)gr.Cells[1].FindControl("ddlSupplierPartNumber");
                TextBox txtOrdItemCode = (TextBox)gr.Cells[2].FindControl("txtOrdItemCode");

                if (!(ddlSupplierPartNumber.Visible))
                {
                    if (partNumber == txtOrdItemCode.Text)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void HideDllItemCodeDropDown()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                for (int rowIndex = 0; rowIndex < GridView1.Rows.Count; rowIndex++)
                {
                    Button btnSearch = (Button)GridView1.Rows[rowIndex].Cells[1].FindControl("btnSearch");

                    if (rowIndex != GridView1.Rows.Count - 1)
                    {
                        Image imgViewSupplierLine = (Image)GridView1.Rows[rowIndex].FindControl("imgViewSupplierLine");
                        Label lblSupplier_Part_Number = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSupplier_Part_Number");
                        Label lblOrdApplicationSegmentCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdApplicationSegmentCode");
                        Label lblOrdVehicleTypeCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdVehicleTypeCode");
                        Label lblOrdPackingQuantity = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdPackingQuantity");
                        Label lblItemCode = (Label)GridView1.Rows[rowIndex].FindControl("lblOrdItemCode");
                        Label lblOrderItem_PO_Quantity = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_PO_Quantity");
                        Label lblOrderItem_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdOrderItem_Status");
                        Label lblSchedule_Date = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Date");
                        Label lblValid_Days = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdValid_Days");
                        Label lblSchedule_Status = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdSchedule_Status");
                        Label lblIndent_Branch = (Label)GridView1.Rows[rowIndex].FindControl("lblgrdIndent_Branch");

                        DropDownList ddlSupplierPartNumber = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlSupplierPartNumber");
                        TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdSupplierPartNo");
                        TextBox txtOrdApplicationSegmentCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdApplicationSegmentCode");
                        TextBox txtOrdVehicleTypeCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdVehicleTypeCode");
                        TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdPackingQuantity");

                        TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                        TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrderItem_PO_Quantity");
                        DropDownList ddlOrdItemStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdItemStatus");
                        TextBox txtSchedule_Date = (TextBox)GridView1.Rows[rowIndex].FindControl("txtSchedule_Date");
                        TextBox txtValid_Days = (TextBox)GridView1.Rows[rowIndex].FindControl("txtValid_Days");
                        DropDownList ddlOrdScheduleStatus = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlOrdScheduleStatus");
                        DropDownList ddlIndentBranch = (DropDownList)GridView1.Rows[rowIndex].FindControl("ddlIndentBranch");

                        ddlSupplierPartNumber.Enabled = false;
                        txtOrdSupplierPartNo.Enabled = false;
                        txtOrdApplicationSegmentCode.Enabled = false;
                        txtOrdVehicleTypeCode.Enabled = false;
                        txtOrdPackingQuantity.Enabled = false;
                        txtItemCode.Enabled = false;
                        txtOrderItem_PO_Quantity.Enabled = false;
                        ddlOrdItemStatus.Enabled = false;
                        txtSchedule_Date.Enabled = false;
                        txtValid_Days.Enabled = false;
                        ddlOrdScheduleStatus.Enabled = false;
                        ddlIndentBranch.Enabled = false;
                        btnSearch.Visible = false;
                        imgViewSupplierLine.Style.Add("display", "inline");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);    
            }
          
        }

        private void BindGridItems(string strPO_Number)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.DirectPOItems";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetOrdDirectPO_Items";
                    obj.SelectParameters.Add("strPO_Number", strPO_Number);
                    obj.SelectParameters.Add("Indicator", "D");
                    obj.DataBind();
                   

                    GridView1.Columns[13].Visible = false;

                    DirectPurchaseOrders getObj = new DirectPurchaseOrders();
                    List<DirectPOItems> ItemData = new List<DirectPOItems>();
                    ItemData = getObj.GetOrdDirectPO_Items(strPO_Number, "D", Session["BranchCode"].ToString());
                    dt.Rows.Clear();
                    DataRow dr = null;
                    int iRowCount = 0;
                    for (int i = 0; i < ItemData.Count; i++)
                    {
                        iRowCount = i + 1;
                        dr = dt.NewRow();
                        dr["S_No"] = iRowCount.ToString();
                        dr["Supplier_Part_Number"] = ItemData[i].Supplier_Part_Number.ToString();
                        dr["Application_Segment_Code"] = ItemData[i].Application_Segment_Code.ToString();
                        dr["Vehicle_Type_Code"] = ItemData[i].Vehicle_Type_Code.ToString();
                        dr["Packing_Quantity"] = ItemData[i].Packing_Quantity.ToString();
                        dr["Item_Code"] = ItemData[i].Item_Code.ToString();
                        dr["OrderItem_PO_Quantity"] = ItemData[i].OrderItem_PO_Quantity.ToString();
                        dr["OrderItem_Status"] = ItemData[i].OrderItem_Status.ToString();
                        dr["Schedule_Date"] = ItemData[i].Schedule_Date.ToString();
                        dr["Valid_Days"] = ItemData[i].Valid_Days.ToString();
                        dr["Schedule_Status"] = ItemData[i].Schedule_Status.ToString();
                        dr["Indent_Branch"] = ItemData[i].Indent_Branch.ToString();
                        dt.Rows.Add(dr);
                    }

                    GridView1.ShowFooter = false;
                    ViewState["CurrentTable"] = dt;

                    if (!(bool)(Session["RoleCode"].ToString().Equals("BEDP")))
                    {
                        GridView1.ShowFooter = false;
                    } 
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    hdnFreezeRowCnt.Value = "1";
                    SetPreviousData();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        private void BindGridItemsFooter()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        }

        private void FirstGridViewRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dt.Columns.Add(new DataColumn("S_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Supplier_Part_Number", typeof(string)));
                dt.Columns.Add(new DataColumn("Application_Segment_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Vehicle_Type_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Packing_Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("OrderItem_PO_Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("OrderItem_Status", typeof(string)));
                dt.Columns.Add(new DataColumn("Schedule_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Valid_Days", typeof(string)));
                dt.Columns.Add(new DataColumn("Schedule_Status", typeof(string)));
                dt.Columns.Add(new DataColumn("Indent_Branch", typeof(string)));
                dt.Columns.Add(new DataColumn("Completed", typeof(string)));


                DataRow dr = null;

                dr = dt.NewRow();
                dr["S_No"] = string.Empty; ;
                dr["Supplier_Part_Number"] = string.Empty;
                dr["Application_Segment_Code"] = string.Empty;
                dr["Vehicle_Type_Code"] = string.Empty;
                dr["Packing_Quantity"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["OrderItem_PO_Quantity"] = string.Empty;
                dr["OrderItem_Status"] = string.Empty;
                dr["Schedule_Date"] = string.Empty;
                dr["Valid_Days"] = "0";
                dr["Schedule_Status"] = string.Empty;
                dr["Indent_Branch"] = string.Empty;
                dr["Completed"] = string.Empty;

                dt.Rows.Add(dr);

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;
                
                if (txtOrd_PONumber.Visible == true && GridView1.ShowFooter == true)
                {
                    GridView1.Columns[13].Visible = true;
                }
                else
                {
                    GridView1.Columns[13].Visible = false;
                }
                //  GridView1.Columns[10].Visible = true;

                if (GridView1.FooterRow != null)
                {
                    Button btnAddRow = (Button)GridView1.FooterRow.FindControl("btnAdd");
                    if (!(bool)(Session["RoleCode"].ToString().Equals("BEDP")))
                    {
                        btnAddRow.Enabled = false;
                    }
                    if (btnAddRow != null)
                    {
                        btnAddRow.Attributes.Add("OnClick", "return funDirectPOBtnAddRow();");
                    }
                }

                if (!(bool)(Session["RoleCode"].ToString().Equals("BEDP")))
                {
                    GridView1.ShowFooter = false;
                }

               
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                GridView1.Rows[0].Cells[0].ColumnSpan = GridView1.Columns.Count - 1;
                ViewState["GridRowCount"] = "0";
                hdnRowCnt.Value = "0";
                hdnFreezeRowCnt.Value = "0";
               

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        private void DisableControls(bool setFlag)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlOrdTransactionType.Enabled = setFlag;
                ddlOrdSupplier.Enabled = setFlag;
                ddlOrdCustomer.Enabled = setFlag;
                txtOrdRdPermitDate.Enabled = setFlag;
                txtOrdRdPermitNumber.Enabled = setFlag;
                txtOrdRefIndentDate.Enabled = setFlag;
                txtOrdRefIndentNumber.Enabled = setFlag;
                txtOrdCarrier.Enabled = setFlag;
                txtOrdCarrierInfoRemarks.Enabled = setFlag;
                txtOrdDestination.Enabled = setFlag;
                txtOrdCustomerCode.Enabled = false;
                txtOrdCustomerAddress1.Enabled = false;
                txtOrdCustomerAddress2.Enabled = false;
                txtOrdAddress3.Enabled = false;
                txtOrdCustomerAddress4.Enabled = false;
                txtOrdCustomerLocation.Enabled = false;

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }

        }

        private void ResetToEmpty()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtOrd_PONumber.Text = "";
                txtOrdRdPermitDate.Text = "";
                txtOrdRdPermitNumber.Text = "";
                txtOrdRefIndentDate.Text = "";
                txtOrdRefIndentNumber.Text = "";
                txtOrdCarrier.Text = "";
                txtOrdCarrierInfoRemarks.Text = "";

                txtOrdCustomerAddress1.Text = "";
                txtOrdCustomerAddress2.Text = "";
                txtOrdCustomerAddress4.Text = "";
                txtOrdAddress3.Text = "";
                txtOrdCustomerCode.Text = "";
                txtOrdCustomerLocation.Text = "";
                //txtOrdTin_Number.Text = "";

                ddlOrdTransactionType.SelectedIndex = 0;

                if (ddlOrdCustomer.Visible == true)
                    ddlOrdCustomer.SelectedIndex = 0;

                ddlOrd_PONumber.SelectedIndex = 0;
                ddlOrdSupplier.SelectedIndex = 0;
                BindGridItemsFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
        
        }
        #endregion

     
    }
}
