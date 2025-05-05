using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory
{
    public partial class Stock_Diversion : System.Web.UI.Page
    {
        #region Event Methods

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        { 

            try
            {
                lblError.Text = "";
                txtQuantity.Enabled = false;
                
                if (!IsPostBack)
                {
                    lblError.Text = "";
                    imgViewSupplierLine.Style.Add("display", "none");
                    BtnSubmit.Enabled = false;
                    trSerialNo.Visible = false;
                    ddlDiversionNumber.Visible = false;
                    txtDiversionNumber.Visible = true;
                    txtSupplierPartNumber.Visible = true;
                    ddlSupplierPartNumber.Visible = false;                    
                    ImagebtnSupplier.Visible = false;
                    txtDiversionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    BindTransactionType();
                    BindInwardNumber();
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp);                
            }
        }

     
        protected void ImgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtDiversionNumber.Visible = false;
                ddlDiversionNumber.Visible = true;
                trSerialNo.Visible = false;
               // BtnSubmit.Enabled = true;
                ddlInwardNumber.Enabled = false;
                ddlSerailNo.Enabled = false;
                txtQuantity.Enabled = false;                 
                txtReason.Enabled = true;
                BindDiversionNumber();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("Stock_Diversion.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp);
            }
        }
       
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            int RowCount = 0;
            string strDiversionNumber = "";
            try
            {
                if (txtDiversionNumber.Visible == true)
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        StockDiversionItems objSection = new StockDiversionItems();
                        objSection.Item_Code = txtItemCode.Text.Trim();
                        objSection.Diversion_date = txtDiversionDate.Text.Trim();
                        // objSection.Diversion_Number = txtPhysicalBalance.Text;
                        objSection.From_TransactionType = ddlFromTransactionType.SelectedValue;
                        objSection.To_TransactionType = ddlToTransactionType.SelectedValue;
                        objSection.Supplier_Part_Number = ddlSupplierPartNumber.SelectedValue;
                        objSection.Inward_Number = ddlInwardNumber.SelectedValue;
                        objSection.Serial_No = Convert.ToInt32(ddlSerailNo.SelectedValue);
                        objSection.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
                        objSection.Reason = txtReason.Text.Trim();


                        using (ObjectDataSource objGet = new ObjectDataSource())
                        {
                            objGet.DataObjectTypeName = "IMPALLibrary.Quantity";
                            objGet.TypeName = "IMPALLibrary.StockDiversion";
                            objGet.SelectMethod = "GetQuantiy";
                            objGet.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                            objGet.SelectParameters.Add("strSupplierPartNumber", ddlSupplierPartNumber.SelectedValue);
                            objGet.SelectParameters.Add("strItemcode", txtItemCode.Text.Trim());
                            objGet.SelectParameters.Add("strInwardNumber", ddlInwardNumber.SelectedValue);
                            objGet.SelectParameters.Add("strSerialNo", ddlSerailNo.SelectedValue);
                            objGet.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                            objGet.DataBind();

                            Quantity objGetSection = new Quantity();
                            object[] objGetCustomerSections = new object[0];
                            objGetCustomerSections = (object[])objGet.Select();
                            objGetSection = (Quantity)objGetCustomerSections[0];

                            if (Convert.ToInt32(objGetSection.Quantity_No) < Convert.ToInt32(txtQuantity.Text))
                            {
                                lblError.Text = "Quantity has been updated just now by other user, Please refresh and try again.";
                            }
                            else
                            {
                                StockDiversion objSaveStockAdjustment = new StockDiversion();
                                strDiversionNumber = objSaveStockAdjustment.InsStockDiversion(objSection, Session["BranchCode"].ToString());

                                txtDiversionNumber.Text = strDiversionNumber.ToString();
                                txtDiversionNumber.ReadOnly = true;
                                ImagebtnSupplier.Visible = false;
                                BtnSubmit.Enabled = false;
                                ddlInwardNumber.Enabled = false;
                                ddlSerailNo.Enabled = false;
                                txtQuantity.Enabled = false;
                                txtReason.Enabled = false;
                                lblError.Text = "Saved Successfully.";
                            }
                        }
                    }
                }
                else
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        StockDiversionItems objSection = new StockDiversionItems();
                        objSection.Diversion_Number = ddlDiversionNumber.SelectedValue;
                        objSection.Inward_Number = ddlInwardNumber.SelectedValue;
                        //objSection.Serial_No = Convert.ToInt32(ddlSerailNo.SelectedValue);                            
                        objSection.Reason = txtReason.Text.Trim();

                        StockDiversion objSaveStockAdjustment = new StockDiversion();
                        RowCount = objSaveStockAdjustment.UpdStockDiversion(objSection, Session["BranchCode"].ToString());
                        ImagebtnSupplier.Visible = false;
                        BtnSubmit.Enabled = false;
                        ddlInwardNumber.Enabled = false;
                        ddlSerailNo.Enabled = false;
                        txtQuantity.Enabled = false;
                        txtReason.Enabled = false;
                        lblError.Text = "Updated Successfully.";
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void ddlDiversionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
              
               
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.StockDiversionItems";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetStockDiversion";
                    obj.SelectParameters.Add("strDiversion_number", ddlDiversionNumber.SelectedValue);
                    obj.DataBind();

                    StockDiversionItems objSection = new StockDiversionItems();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (StockDiversionItems)objCustomerSections[0];
                    if ((objSection.Item_Code is object))
                    {

                        BindTransactionType();

                        txtDiversionDate.Text = objSection.Diversion_date;
                        txtItemCode.Text = objSection.Item_Code;
                        txtQuantity.Text = objSection.Quantity.ToString();
                        txtQuantity.Enabled = true;

                        txtSupplierPartNumber.Text = objSection.Supplier_Part_Number;
                        txtReason.Text = objSection.Reason;

                        BindSupplierLineDetails(txtItemCode.Text.Trim());

                        ListItem liFromTr = new ListItem();
                        ddlFromTransactionType.SelectedValue = objSection.From_TransactionType;
                        liFromTr.Value = ddlFromTransactionType.SelectedItem.Value;
                        liFromTr.Text = ddlFromTransactionType.SelectedItem.Text;
                        ddlFromTransactionType.Items.Clear();
                        ddlFromTransactionType.Items.Add(liFromTr);
                        ddlFromTransactionType.SelectedValue = objSection.From_TransactionType;


                        ListItem liToTr = new ListItem();
                        ddlToTransactionType.SelectedValue = objSection.To_TransactionType;
                        liToTr.Value = ddlToTransactionType.SelectedItem.Value;
                        liToTr.Text = ddlToTransactionType.SelectedItem.Text;
                        ddlToTransactionType.Items.Clear();
                        ddlToTransactionType.Items.Add(liToTr);
                        ddlToTransactionType.SelectedValue = objSection.To_TransactionType;

                        ddlSupplierPartNumber.Items.Clear();
                        ListItem liSP = new ListItem();
                        liSP.Value = objSection.Supplier_Part_Number;
                        liSP.Text = objSection.Supplier_Part_Number;
                        ddlSupplierPartNumber.Items.Add(liSP);
                        ddlSupplierPartNumber.SelectedValue = objSection.Supplier_Part_Number;

                        ddlInwardNumber.Items.Clear();
                        ListItem li = new ListItem();
                        li.Value = objSection.Inward_Number;
                        li.Text = objSection.Inward_Number;
                        ddlInwardNumber.Items.Add(li);
                        ddlInwardNumber.SelectedValue = objSection.Inward_Number;

                        BtnSubmit.Enabled = true;
                        trSerialNo.Visible = false;
                        ImagebtnSupplier.Visible = false;
                        ImgButtonQuery.Visible = false;
                        ddlInwardNumber.Enabled = true;
                        ddlSerailNo.Enabled = true;
                        txtReason.Enabled = true;
                        imgViewSupplierLine.Style.Add("display", "inline");


                        txtItemCode.ReadOnly = true;
                        txtQuantity.Enabled = false;
                        txtSupplierPartNumber.ReadOnly = true;

                    }
                    else
                    {

                        txtDiversionDate.Text = "";
                        txtItemCode.Text = "";
                        txtQuantity.Text = "";
                        txtSupplierPartNumber.Text = "";
                        txtReason.Text = "";
                        ddlToTransactionType.Items.Clear();
                        ddlFromTransactionType.Items.Clear();
                        ddlInwardNumber.Items.Clear();
                        imgViewSupplierLine.Style.Add("display", "none");
                        BtnSubmit.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void ImagebtnSupplier_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BindSupplierPartNumber();
                if (ddlSupplierPartNumber.Items.Count > 1)
                {
                    ddlSupplierPartNumber.Visible = true;
                    txtSupplierPartNumber.Visible = false;
                    txtReason.Enabled = true;
                }
                else
                    lblError.Text = "No item code found for search option";

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void ddlSupplierPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetBalanceItemCount";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    obj.SelectParameters.Add("strItemCode", ddlSupplierPartNumber.SelectedValue);
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();

                    int objSection; //= new Quantity();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (int)objCustomerSections[0];

                    if (objSection == -1)
                    {
                        lblError.Text = "No Records For this Item Code / Supplier Part #.";
                    }
                    
                    txtItemCode.Text = ddlSupplierPartNumber.SelectedValue;
                    trSerialNo.Visible = true;
                    BindInwardNumber();
                    BindSupplierLineDetails(txtItemCode.Text.Trim());
                    ddlSupplierPartNumber.Enabled = false;
                    ddlToTransactionType.Enabled = false;
                    ddlFromTransactionType.Enabled = false;
                    imgViewSupplierLine.Style.Add("display", "inline");                    

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }

        }

        protected void ddlSerailNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.Quantity";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetQuantiy";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    obj.SelectParameters.Add("strSupplierPartNumber", ddlSupplierPartNumber.SelectedValue);
                    obj.SelectParameters.Add("strItemcode", txtItemCode.Text.Trim());
                    obj.SelectParameters.Add("strInwardNumber", ddlInwardNumber.SelectedValue);
                    obj.SelectParameters.Add("strSerialNo", ddlSerailNo.SelectedValue);
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();

                    Quantity objSection = new Quantity();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (Quantity)objCustomerSections[0];
                    if ((objSection.Quantity_No is object))
                    {
                        txtQuantity.Text = objSection.Quantity_No;
                        HdnOrgQuantity.Value = objSection.Quantity_No;

                        BtnSubmit.Enabled = true;
                    }
                    else
                    {
                        txtQuantity.Text = "";

                    }
                    txtQuantity.Enabled = true;
                    
                }
                // txtQuantity.Text ="";                
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }

        }

        protected void ddlInwardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.OSLSSerialNo";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetSerialNumber";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    obj.SelectParameters.Add("strItemcode", txtItemCode.Text.Trim());
                    obj.SelectParameters.Add("strInwardNumber", ddlInwardNumber.SelectedValue);
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();
                    ddlSerailNo.DataSource = obj;
                    ddlSerailNo.DataTextField = "OSLS_SerialNumber";
                    ddlSerailNo.DataValueField = "OSLS_SerialNumber";
                    ddlSerailNo.DataBind();
                    AddSelect(ddlSerailNo);
                    txtQuantity.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void ddlFromTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ddlFromSelected;
                ddlFromSelected = ddlFromTransactionType.SelectedValue;
                BindTransactionType();
                ddlFromTransactionType.SelectedValue = ddlFromSelected;
                lblError.Text = "";
                funReset("FROMTRANS");
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetItemCount";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();

                    int objSection; //= new Quantity();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (int)objCustomerSections[0];

                    if (objSection == 0)
                    {
                        lblError.Text = "No ItemCode for this From Transaction type.";
                    }
                    else
                    {
                        // ImagebtnSupplier.Visible = true;
                        ListItem li = new ListItem();
                        li.Value = ddlFromTransactionType.SelectedValue;
                        li.Text = ddlFromTransactionType.SelectedItem.Text;
                        ddlToTransactionType.Items.Remove(li);

                        if (ddlToTransactionType.Items.Count > 0)
                        {
                            ddlToTransactionType.SelectedValue = "0";
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }


        }

        protected void ddlToTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlToTransactionType.SelectedValue != "0" && ddlFromTransactionType.SelectedValue != "0")
                {
                    ImagebtnSupplier.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }

        }

        #endregion

        #region User methods
        private void funReset(string strEvent)
        {
            try
            {
                 
                ImagebtnSupplier.Visible = false;
                ddlSupplierPartNumber.Visible = false;
                txtSupplierPartNumber.Visible = true;
                ddlDiversionNumber.Visible = false;
                txtDiversionNumber.Visible = true;
                ImgButtonQuery.Visible = true;
                ddlToTransactionType.Enabled = true;
                ddlFromTransactionType.Enabled = true;
                ddlSupplierPartNumber.Enabled = true;
             
                ddlInwardNumber.Enabled = true;
                ddlSerailNo.Enabled = true;
                txtQuantity.Enabled = true;
                BtnSubmit.Enabled = false;
                txtReason.Enabled = true;
                if (strEvent == "RESET")
                {
                    BindTransactionType();
                    ddlFromTransactionType.SelectedValue = "0";
                }

                ddlDiversionNumber.SelectedValue = "0";               
                ddlToTransactionType.SelectedValue = "0";
                ddlSerailNo.SelectedValue = "0";
                ddlInwardNumber.Items.Clear();
                AddSelect(ddlInwardNumber);
                txtSupplierPartNumber.Text = "";
                txtItemCode.Text = "";
                txtQuantity.Text = "";
                txtReason.Text = "";
                txtDiversionNumber.Text = "";
                txtDiversionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                 
                txtQuantity.Enabled = true;                
                txtSupplierPartNumber.ReadOnly = false;              
                imgViewSupplierLine.Style.Add("display", "none");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }


        private void AddSelect(DropDownList ddl)
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = " ";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }

        }

        private void BindTransactionType()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.ddlTransactionType";
                    obj.TypeName = "IMPALLibrary.StockDiversion";

                    obj.SelectMethod = "GetFromStockTransactionTypes";
                    obj.DataBind();
                    ddlFromTransactionType.DataSource = obj;
                    ddlFromTransactionType.DataTextField = "Transaction_Type_Description";
                    ddlFromTransactionType.DataValueField = "Transaction_Type_Code";
                    ddlFromTransactionType.DataBind();
                    AddSelect(ddlFromTransactionType);

                    obj.SelectMethod = "GetToStockTransactionTypes";
                    obj.DataBind();
                    ddlToTransactionType.DataSource = obj;
                    ddlToTransactionType.DataTextField = "Transaction_Type_Description";
                    ddlToTransactionType.DataValueField = "Transaction_Type_Code";
                    ddlToTransactionType.DataBind();
                    AddSelect(ddlToTransactionType);
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }

        }

        private void BindInwardNumber()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.InwardNumber";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetInwardNumber";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    //   obj.SelectParameters.Add("strItemcode", txtItemCode.Text.Trim());
                    obj.SelectParameters.Add("strItemcode", ddlSupplierPartNumber.SelectedValue);
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());

                    obj.DataBind();
                    ddlInwardNumber.DataSource = obj;
                    ddlInwardNumber.DataTextField = "Inward_Number";
                    ddlInwardNumber.DataValueField = "Inward_Number";
                    ddlInwardNumber.DataBind();
                    AddSelect(ddlInwardNumber);
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }


        private void BindDiversionNumber()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.DiversionNumber";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetDiversionNumber";
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();
                    ddlDiversionNumber.DataSource = obj;
                    ddlDiversionNumber.DataTextField = "Diversion_Number";
                    ddlDiversionNumber.DataValueField = "Diversion_Number";
                    ddlDiversionNumber.DataBind();
                    AddSelect(ddlDiversionNumber);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        protected void BindSupplierPartNumber()
        {
            string strSupplierPartNumber = "";
            try
            {
                if (txtSupplierPartNumber.Visible == true)
                {
                strSupplierPartNumber = txtSupplierPartNumber.Text.Trim();

                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetItemCode";
                    obj.SelectParameters.Add("strTransactionType", ddlFromTransactionType.SelectedValue);
                    obj.SelectParameters.Add("strSupplierPartNumber", txtSupplierPartNumber.Text.Trim());
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();
                    ddlSupplierPartNumber.DataSource = obj;
                    ddlSupplierPartNumber.DataTextField = "Supplier_Part_Number";
                    ddlSupplierPartNumber.DataValueField = "item_code";
                    ddlSupplierPartNumber.DataBind();
                    AddSelect(ddlSupplierPartNumber);

                }

                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        private void BindSupplierLineDetails(string strItemCode)
        {
            try
            {
                if (txtItemCode.Text != "")
                {
                    imgViewSupplierLine.CssClass = "ViewDetails";

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.SupplierOrdLineDetails";
                        objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                        objBranch.SelectMethod = "GetOrdSupplierLineDetails";
                        objBranch.SelectParameters.Add("ItemCode", strItemCode.ToString());
                        objBranch.DataBind();

                        SupplierOrdLineDetails objSupplierLine = new SupplierOrdLineDetails();
                        object[] objSupplierLineSections = new object[0];
                        objSupplierLineSections = (object[])objBranch.Select();
                        objSupplierLine = (SupplierOrdLineDetails)objSupplierLineSections[0];

                        lblSupplier_Line_Value.Text = objSupplierLine.Supp_Short_description.ToString();
                        lblApplication_Segment_Value.Text = objSupplierLine.Appln_Segment_Description.ToString();
                        lblVehichleTypeDescriptionValue.Text = objSupplierLine.Vehicle_Type_Description.ToString();
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_Diversion), exp); 
            }
        }

        #endregion
               
    } 
     
}
