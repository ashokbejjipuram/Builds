using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using System.Web.Script.Services;
using System.Web.Services;

namespace IMPALWeb.Masters.Item
{
    public partial class ItemMasterNew : System.Web.UI.Page
    {
        List<ItemFields> ItemsFields;
        ApplicationSegments ObjAppSegment = new ApplicationSegments();
        ItemMasters ObjItemMaster = new ItemMasters();
        ItemTypes objItemType = new ItemTypes();
        ProductGroups objProduct = new ProductGroups();
        VehilcleTypes objVechile = new VehilcleTypes();
        Suppliers objsupplier = new Suppliers();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if ((string)Session["RoleCode"] == "BEDP")
                    {
                        ItemDetailFormView.DefaultMode = FormViewMode.ReadOnly;
                        //BindItemDetailFormView(drpSupplierPartNumber.SelectedValue);
                        //txtSupplierPartNumber.Text = ObjItemMaster.Getitemcode();
                        //BindItemDetailFormView(txtSupplierPartNumber.Text);
                        //drpSupplierPartNumber.Enabled = true;
                        txtSupplierPartNumber.Enabled = true;
                        btnSearch.Visible = false;
                        PnlItemMasterNew.Enabled = false;
                        BtnSubmit.Enabled = false;
                        BtnSubmit.Visible = false;
                        btnReset.Enabled = false;
                    }
                    else
                    {
                        ItemDetailFormView.DefaultMode = FormViewMode.Insert;
                        //drpSupplierPartNumber.Enabled = false;
                        txtSupplierPartNumber.Enabled = false;
                        PnlItemMasterNew.Enabled = true;
                        btnSearch.Visible = true;
                        //BtnSubmit.Enabled = true;
                        BtnSubmit.Visible = false;
                        btnReset.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Init()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ItemDetailFormView.EditItemTemplate = ItemDetailFormView.ItemTemplate;
                ItemDetailFormView.InsertItemTemplate = ItemDetailFormView.ItemTemplate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void ItemDetailFormView_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void drpSupplierPartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //BindItemDetailFormView(drpSupplierPartNumber.SelectedValue);
                //BindItemDetailFormView(txtSupplierPartNumber.Text);
                BindItemDetailFormView(hddItemCode.Value);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindItemDetailFormView(string ItemCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ItemsFields = new List<ItemFields>();
                ItemsFields = ObjItemMaster.ViewItem(ItemCode);
                if (ItemsFields.Count >= 0)
                {
                    ItemDetailFormView.DataSource = ItemsFields;
                    ItemDetailFormView.DataBind();
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ItemDetailFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void ItemDetailFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var drpSupplierLine = (DropDownList)ItemDetailFormView.FindControl("drpSupplierLine");
                //var drpItemType = (DropDownList)ItemDetailFormView.FindControl("drpItemType");
                var drpProductGrp = (DropDownList)ItemDetailFormView.FindControl("drpProductGrp");
                var drpAppSegment = (DropDownList)ItemDetailFormView.FindControl("drpAppSegment");
                var drpVehType = (DropDownList)ItemDetailFormView.FindControl("drpVehType");
                var drpFsnInd = (DropDownList)ItemDetailFormView.FindControl("drpFsnInd");
                var drpStatus = (DropDownList)ItemDetailFormView.FindControl("drpStatus");
                var drpAbcInd = (DropDownList)ItemDetailFormView.FindControl("drpAbcInd");

                var RdoIndPercent = (RadioButton)ItemDetailFormView.FindControl("RdoIndPercent");
                var RdoIndAmount = (RadioButton)ItemDetailFormView.FindControl("RdoIndAmount");
                var rdoRateIndYes = (RadioButton)ItemDetailFormView.FindControl("rdoRateIndYes");
                var rdoRateIndNo = (RadioButton)ItemDetailFormView.FindControl("rdoRateIndNo");
                var txtHSNCode = (TextBox)ItemDetailFormView.FindControl("txtHSNCode");
				
                LoadDropDownLists<IMPALLibrary.Supplier>(objsupplier.GetAllSupplierLines(), drpSupplierLine, "SupplierCode", "SupplierName", true, "");
                //LoadDropDownLists<IMPALLibrary.ItemType>(objItemType.GetAllItemTypes(), drpItemType, "ItemTypeCode", "ItemTypeDescription", true, "");
                LoadDropDownLists<IMPALLibrary.ProductGroup>(objProduct.GetAllProductGroups(), drpProductGrp, "ProductGroupCode", "ProductGroupDescription", true, "");
                LoadDropDownLists<IMPALLibrary.ApplicationSegment>(ObjAppSegment.GetAllApplicationSegments(), drpAppSegment, "ApplicationSegmentCode", "ApplnSegmentDescription", true, "");
                LoadDropDownLists<IMPALLibrary.VehilcleType>(objVechile.GetAllVehilcleTypes(), drpVehType, "VehicleTypeCode", "VehicleTypeDescription", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("ItemFSNIndicator"), drpFsnInd, "DisplayValue", "DisplayText", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("ItemStatus"), drpStatus, "DisplayValue", "DisplayText", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("ItemABCIndicator"), drpAbcInd, "DisplayValue", "DisplayText", true, "");

                if (ItemsFields != null)
                {
                    drpSupplierLine.SelectedValue = ItemsFields[0].Supplier_Line_Code;
                    //drpItemType.SelectedValue = ItemsFields[0].Item_Type_Code;
                    drpProductGrp.SelectedValue = ItemsFields[0].Product_Group_Code;
                    drpAppSegment.SelectedValue = ItemsFields[0].Application_Segment_Code;
                    drpVehType.SelectedValue = ItemsFields[0].Vehicle_Type_Code;
                    drpFsnInd.SelectedValue = ItemsFields[0].FSN_Classification;
                    drpStatus.SelectedValue = ItemsFields[0].Status;
                    drpAbcInd.SelectedValue = ItemsFields[0].ABC_Classification;

                    RdoIndPercent.Checked = ItemsFields[0].Excise_Duty_Indicator == "P" ? true : false;
                    RdoIndAmount.Checked = ItemsFields[0].Excise_Duty_Indicator == "A" ? true : false;
                    rdoRateIndYes.Checked = ItemsFields[0].Rate_Indicator == "Y" ? true : false;
                    rdoRateIndNo.Checked = ItemsFields[0].Rate_Indicator == "N" ? true : false;
                }

                if (ItemDetailFormView.CurrentMode == FormViewMode.Edit)
                {
                    //drpSupplier.Enabled = false;
                    //drpProduct.Enabled = false;
                    //drpPlant.Enabled = false;
                    //BtnSubmit.Text = "Update";
                    BtnSubmit.Visible = false;
                    drpSupplierLine.Enabled = false;
                }
                else if (ItemDetailFormView.CurrentMode == FormViewMode.Insert)
                {
                    //drpSupplier.Enabled = true;
                    //drpProduct.Enabled = true;
                    //drpPlant.Enabled = true;
                    //BtnSubmit.Text = "Add";
                    BtnSubmit.Visible = false;
                    drpSupplierLine.Enabled = true;
                }
                else
                {
                    BtnSubmit.Visible = false;
                    btnReset.Visible = false;
                    drpSupplierLine.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //drpSupplierPartNumber.Enabled = true;
                txtSupplierPartNumber.Enabled = true;
                btnSearch.Visible = false;
                PnlItemMasterNew.Enabled = true;
                btnReset.Enabled = true;
                //BtnSubmit.Enabled = true;
                BtnSubmit.Visible = false;
                var drpSupplierLine = (DropDownList)ItemDetailFormView.FindControl("drpSupplierLine");
                drpSupplierLine.Enabled = true;
                ItemDetailFormView.ChangeMode(FormViewMode.Edit);

                //BindItemDetailFormView(drpSupplierPartNumber.SelectedValue);
                //txtSupplierPartNumber.Text = ObjItemMaster.Getitemcode();
                //BindItemDetailFormView(txtSupplierPartNumber.Text);


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
                GetItemsDetails(true);
                if (ItemDetailFormView.CurrentMode == FormViewMode.Edit)
                {
                    //drpSupplierPartNumber.Enabled = false;
                    txtSupplierPartNumber.Enabled = false;
                    btnSearch.Visible = true;
                    ItemDetailFormView.ChangeMode(FormViewMode.Insert);
                    PnlItemMasterNew.Enabled = true;

                }
                if (ItemDetailFormView.CurrentMode == FormViewMode.Insert)
                {

                    //drpSupplierPartNumber.Enabled = false;
                    txtSupplierPartNumber.Enabled = false;
                    //BtnSubmit.Enabled = true;
                    BtnSubmit.Visible = false;
                    btnReset.Enabled = true;
                    btnSearch.Visible = true;
                    PnlItemMasterNew.Enabled = true;

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
            var txtPartNumber = (TextBox)ItemDetailFormView.FindControl("txtPartNumber");
            //var drpProduct = (DropDownList)ItemDetailFormView.FindControl("drpProduct");
            //var drpPlant = (DropDownList)ItemDetailFormView.FindControl("drpPlant");
            //string New_Supplier_Line = drpSupplier.SelectedValue + drpProduct.SelectedValue + drpPlant.SelectedValue;
            ListItem selectedListItem = null; //drpSupplierPartNumber.Items.FindByValue(New_Supplier_Line);
            try
            {
                if (ItemDetailFormView.CurrentMode == FormViewMode.Insert)
                {
                    if (selectedListItem == null)
                    {
                        ObjItemMaster.AddNewItem(GetItemsDetails(false));
                        //LoadDropDownLists<ItemMaster>(ObjItemMaster.GetItemCode(), drpSupplierPartNumber, "itemcode", "Supplierpartno", false, "");
                        ItemDetailFormView.ChangeMode(FormViewMode.Edit);
                        BindItemDetailFormView(ObjItemMaster.Getitemcode(txtPartNumber.Text));
                        PnlItemMasterNew.Enabled = false;
                        //BtnSubmit.Enabled = false;
                        BtnSubmit.Visible = false;
                        btnReset.Enabled = true;
                        btnSearch.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Code Already Exists');", true);
                    }

                }
                else
                {
                    if (ItemDetailFormView.CurrentMode == FormViewMode.Edit)
                    {
                        ObjItemMaster.UpdateItem(GetItemsDetails(false));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item  Updated Succesfully');", true);
                        ItemDetailFormView.ChangeMode(FormViewMode.Insert);
                        btnSearch.Visible = true;
                        //drpSupplierPartNumber.Enabled = false;
                        txtSupplierPartNumber.Enabled = false;

                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private ItemFields GetItemsDetails(bool Reset)
        {
            ItemFields ItemFields = new ItemFields();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                var txtLineCode = (TextBox)ItemDetailFormView.FindControl("txtItemCode");
                var txtPartNumber = (TextBox)ItemDetailFormView.FindControl("txtPartNumber");
                var txtShortName = (TextBox)ItemDetailFormView.FindControl("txtShortName");
                var txtLongDesc = (TextBox)ItemDetailFormView.FindControl("txtLongDesc");
                var txtPurchaseDis = (TextBox)ItemDetailFormView.FindControl("txtPurchaseDis");
                var txtEdDis = (TextBox)ItemDetailFormView.FindControl("txtEdDis");
                var txtHSNCode = (TextBox)ItemDetailFormView.FindControl("txtHSNCode");
                var txtEdValue = (TextBox)ItemDetailFormView.FindControl("txtEdValue");
                var txtUOM = (TextBox)ItemDetailFormView.FindControl("txtUOM");
                var txtPackQty = (TextBox)ItemDetailFormView.FindControl("txtPackQty");
                var txtValue = (TextBox)ItemDetailFormView.FindControl("txtValue");
                var txtMinQnt = (TextBox)ItemDetailFormView.FindControl("txtMinQnt");
                var txtMaxQnt = (TextBox)ItemDetailFormView.FindControl("txtMaxQnt");
                var txtEconomicQnt = (TextBox)ItemDetailFormView.FindControl("txtEconomicQnt");
                var txtSaftyStock = (TextBox)ItemDetailFormView.FindControl("txtSaftyStock");
                var txtMinLead = (TextBox)ItemDetailFormView.FindControl("txtMinLead");
                var txtMaxLead = (TextBox)ItemDetailFormView.FindControl("txtMaxLead");

                var drpSupplierLine = (DropDownList)ItemDetailFormView.FindControl("drpSupplierLine");
                //var drpItemType = (DropDownList)ItemDetailFormView.FindControl("drpItemType");
                var drpProductGrp = (DropDownList)ItemDetailFormView.FindControl("drpProductGrp");
                var drpAppSegment = (DropDownList)ItemDetailFormView.FindControl("drpAppSegment");
                var drpVehType = (DropDownList)ItemDetailFormView.FindControl("drpVehType");
                var drpAbcInd = (DropDownList)ItemDetailFormView.FindControl("drpAbcInd");
                var drpFsnInd = (DropDownList)ItemDetailFormView.FindControl("drpFsnInd");
                var drpStatus = (DropDownList)ItemDetailFormView.FindControl("drpStatus");

                var RdoIndPercent = (RadioButton)ItemDetailFormView.FindControl("RdoIndPercent");
                var RdoIndAmount = (RadioButton)ItemDetailFormView.FindControl("RdoIndAmount");
                var rdoRateIndYes = (RadioButton)ItemDetailFormView.FindControl("rdoRateIndYes");
                var rdoRateIndNo = (RadioButton)ItemDetailFormView.FindControl("rdoRateIndNo");

                if (Reset == false)
                {
                    //ItemFields.Item_Code = drpSupplierPartNumber.SelectedValue;
                    ItemFields.Item_Code = hddItemCode.Value;//txtSupplierPartNumber.Text;
                    ItemFields.Application_Segment_Code = drpAppSegment.SelectedValue;
                    ItemFields.Vehicle_Type_Code = drpVehType.SelectedValue;
                    ItemFields.Supplier_Line_Code = drpSupplierLine.SelectedValue;
                    ItemFields.Item_Short_Description = txtShortName.Text;
                    ItemFields.Item_Long_Description = txtLongDesc.Text;
                    ItemFields.Supplier_Part_Number = txtPartNumber.Text;
                    ItemFields.Item_Type_Code = txtHSNCode.Text;
                    ItemFields.Product_Group_Code = drpProductGrp.SelectedValue;
                    ItemFields.Purchase_Discount = txtPurchaseDis.Text;
                    ItemFields.Excise_Duty_Discount = txtEdDis.Text;
                    ItemFields.Excise_Duty_Indicator = RdoIndPercent.Checked ? "P" : "A";
                    ItemFields.Excise_Duty_Value = txtEdValue.Text;
                    ItemFields.Packing_Quantity = txtPackQty.Text;
                    ItemFields.Unit_of_Measurement = txtUOM.Text;
                    ItemFields.ABC_Classification = drpAbcInd.SelectedValue;
                    ItemFields.FSN_Classification = drpFsnInd.SelectedValue;
                    ItemFields.Rate_Indicator = rdoRateIndYes.Checked ? "Y" : "N";
                    ItemFields.Rate = txtValue.Text;
                    ItemFields.Minimum_Order_Quantity = txtMinQnt.Text;
                    ItemFields.Maximum_Order_Quantity = txtMaxQnt.Text;
                    ItemFields.Economic_Batch_Quantity = txtEconomicQnt.Text;
                    ItemFields.Safety_stock = txtSaftyStock.Text;
                    ItemFields.Minimum_Lead_Time = txtMinLead.Text;
                    ItemFields.Maximum_Lead_Time = txtMaxLead.Text;
                    ItemFields.Appln_Segment_Description = drpAppSegment.SelectedItem.Text;
                    ItemFields.Vehicle_Type_Description = drpVehType.SelectedItem.Text;
                    ItemFields.Product_Group_Description = drpProductGrp.SelectedItem.Text;
                    ItemFields.Status = drpStatus.SelectedItem.Text;


                }
                else
                {

                    drpSupplierLine.SelectedIndex = 0;
                    //drpItemType.SelectedIndex = 0;
                    drpProductGrp.SelectedIndex = 0;
                    drpAppSegment.SelectedIndex = 0;
                    drpVehType.SelectedIndex = 0;
                    drpAbcInd.SelectedIndex = 0;
                    drpFsnInd.SelectedIndex = 0;
                    drpStatus.SelectedIndex = 0;

                    txtLineCode.Text = "";
                    txtPartNumber.Text = "";
                    txtLongDesc.Text = "";
                    txtShortName.Text = "";
                    txtHSNCode.Text = "";
                    txtLongDesc.Text = "";
                    txtPurchaseDis.Text = "";
                    txtEdDis.Text = "";
                    txtEdValue.Text = "";
                    txtUOM.Text = "";
                    txtPackQty.Text = "";
                    txtValue.Text = "";
                    txtMinQnt.Text = "";
                    txtMaxQnt.Text = "";
                    txtEconomicQnt.Text = "";
                    txtSaftyStock.Text = "";
                    txtMinLead.Text = "";
                    txtMaxLead.Text = "";
                    txtSupplierPartNumber.Text = "";

                    RdoIndPercent.Checked = true;
                    RdoIndAmount.Checked = false;
                    rdoRateIndYes.Checked = true;
                    rdoRateIndNo.Checked = false;



                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return ItemFields;
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<string> ItemCodeList = new List<string>();
            try
            {
                ItemLocationDetails item = new ItemLocationDetails();

                List<SupplierPartnumber> PartnumberList = item.GetSupplierPartNumberAutoComplete(prefixText);
                foreach (var item1 in PartnumberList)
                {
                    string KeyValuePair = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item1.PartNumber, item1.ItemCode);
                    ItemCodeList.Add(KeyValuePair);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return ItemCodeList;
        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Discount Discount = new Discount();
                var drpSupplierLine = (DropDownList)ItemDetailFormView.FindControl("drpSupplierLine");
                var txtPurchaseDis = (TextBox)ItemDetailFormView.FindControl("txtPurchaseDis");
                var txtEdDis = (TextBox)ItemDetailFormView.FindControl("txtEdDis");
                var txtEdValue = (TextBox)ItemDetailFormView.FindControl("txtEdValue");
                var RdoIndPercent = (RadioButton)ItemDetailFormView.FindControl("RdoIndPercent");
                var RdoIndAmount = (RadioButton)ItemDetailFormView.FindControl("RdoIndAmount");
                Discount = ObjItemMaster.GetDiscount(drpSupplierLine.SelectedValue);

                if (Discount != null)
                {
                    RdoIndPercent.Checked = Discount.EDDInd == "P" ? true : false;
                    RdoIndAmount.Checked = Discount.EDDInd == "A" ? true : false;
                    txtPurchaseDis.Text = FormatString(Discount.PurDisCount);
                    txtEdDis.Text = FormatString(Discount.EDDiscount);
                    txtEdValue.Text = FormatString(Discount.EDDValue);
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        private string FormatString(string grdValue)
        {
            string result = string.Empty;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                result = String.Format("{0:0.00}", string.IsNullOrEmpty(grdValue) ? 0.00 : Convert.ToDouble(grdValue));
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return result;
        }

    }
}
