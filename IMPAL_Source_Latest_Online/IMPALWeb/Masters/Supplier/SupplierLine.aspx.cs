using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Masters.Supplier
{
    public partial class SupplierLine : System.Web.UI.Page
    {
        List<SupplierLineDetails> SupplierLineDetail;
        Suppliers ObjSupplier = new Suppliers();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {

                    LoadDropDownLists<IMPALLibrary.SupplierLineCode>(ObjSupplier.GetAllSuppliersLine(), drpSupplierLine, "Supplier_Line_Code", "Long_Description", true, "");
                    if ((string)Session["RoleCode"] == "BEDP")
                    {

                        SupplierLineFormView.DefaultMode = FormViewMode.ReadOnly;
                        //BindSupplierLineFormView(drpSupplierLine.SelectedValue);
                        drpSupplierLine.Enabled = true;
                        btnSearch.Visible = false;
                        PnlSupplier.Enabled = false;
                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = false;
                        //btnReport.Enabled = false;
                    }
                    else
                    {
                        SupplierLineFormView.DefaultMode = FormViewMode.Insert;
                        drpSupplierLine.Enabled = false;
                        PnlSupplier.Enabled = true;
                        btnSearch.Visible = true;
                        BtnSubmit.Enabled = true;
                        btnReset.Enabled = true;
                        //btnReport.Enabled = true;
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
                SupplierLineFormView.EditItemTemplate = SupplierLineFormView.ItemTemplate;
                SupplierLineFormView.InsertItemTemplate = SupplierLineFormView.ItemTemplate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void SupplierLineFormView_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindSupplierLineFormView(drpSupplierLine.SelectedValue);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void drpSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var drpProduct = (DropDownList)SupplierLineFormView.FindControl("drpProduct");
                var drpSupplier = (DropDownList)SupplierLineFormView.FindControl("drpSupplier");
                if (drpSupplier.SelectedIndex > 0)
                {
                    LoadDropDownLists<IMPALLibrary.ProductCode>(ObjSupplier.GetAllProduct(drpSupplier.SelectedValue), drpProduct, "ProductCodevalue", "ProductShortDesc", true, "");
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindSupplierLineFormView(string SupplierLineCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                SupplierLineDetail = new List<SupplierLineDetails>();
                SupplierLineDetail = ObjSupplier.ViewSupplierLine(SupplierLineCode);
                if (SupplierLineDetail.Count >= 0)
                {
                    SupplierLineFormView.DataSource = SupplierLineDetail;
                    SupplierLineFormView.DataBind();
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void SupplierLineFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void SupplierLineFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                var drpSupplier = (DropDownList)SupplierLineFormView.FindControl("drpSupplier");
                var drpProduct = (DropDownList)SupplierLineFormView.FindControl("drpProduct");
                var drpPlant = (DropDownList)SupplierLineFormView.FindControl("drpPlant");
                var drpEDInd = (DropDownList)SupplierLineFormView.FindControl("drpEDInd");
                var ChkDeportPayMent = (CheckBox)SupplierLineFormView.FindControl("ChkDeportPayMent");
                LoadDropDownLists<IMPALLibrary.Supplier>(ObjSupplier.GetSuppliercodewithOutDefault(), drpSupplier, "SupplierCode", "SupplierName", true, "");
                LoadDropDownLists<IMPALLibrary.ProductCode>(ObjSupplier.GetAllProduct(), drpProduct, "ProductCodevalue", "ProductShortDesc", true, "");
                LoadDropDownLists<IMPALLibrary.PlantCode>(ObjSupplier.GetAllPlant(), drpPlant, "PlantCodevalue", "PlantDesc", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("SupplierLineEDindicator"), drpEDInd, "DisplayValue", "DisplayText", true, "");
                if (SupplierLineDetail != null)
                {
                    drpSupplier.SelectedValue = SupplierLineDetail[0].supplier_code;
                    drpProduct.SelectedItem.Text = SupplierLineDetail[0].supplier_product_short_name;
                    drpPlant.SelectedValue = SupplierLineDetail[0].supplier_plant_code;
                    drpEDInd.SelectedValue = SupplierLineDetail[0].ed_indicator;
                    ChkDeportPayMent.Checked = SupplierLineDetail[0].Depot_Payment;
                }


                if (SupplierLineFormView.CurrentMode == FormViewMode.Edit)
                {
                    drpSupplier.Enabled = false;
                    drpProduct.Enabled = false;
                    drpPlant.Enabled = false;
                    BtnSubmit.Text = "Update";
                }
                else if (SupplierLineFormView.CurrentMode == FormViewMode.Insert)
                {
                    drpSupplier.Enabled = true;
                    drpProduct.Enabled = true;
                    drpPlant.Enabled = true;
                    BtnSubmit.Text = "Add";
                }
                else
                {
                    BtnSubmit.Visible = false;
                    btnReset.Visible = false;
                    //btnReport.Visible = false;
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
                drpSupplierLine.Enabled = true;
                btnSearch.Visible = false;
                PnlSupplier.Enabled = true;
                //btnReport.Enabled = true;
                btnReset.Enabled = true;
                BtnSubmit.Enabled = true;
                SupplierLineFormView.ChangeMode(FormViewMode.Edit);
                if (drpSupplierLine.SelectedIndex > 0)
                    BindSupplierLineFormView(drpSupplierLine.SelectedValue);


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
                GetCustomerDetails(true);
                if (SupplierLineFormView.CurrentMode == FormViewMode.Edit)
                {
                    drpSupplierLine.Enabled = false;
                    drpSupplierLine.SelectedIndex = 0;
                    btnSearch.Visible = true;
                    SupplierLineFormView.ChangeMode(FormViewMode.Insert);
                    PnlSupplier.Enabled = true;

                }
                if (SupplierLineFormView.CurrentMode == FormViewMode.Insert)
                {

                    drpSupplierLine.Enabled = false;
                    drpSupplierLine.SelectedIndex = 0;
                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    //btnReport.Enabled = true;
                    btnSearch.Visible = true;
                    PnlSupplier.Enabled = true;

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
            var drpSupplier = (DropDownList)SupplierLineFormView.FindControl("drpSupplier");
            var drpProduct = (DropDownList)SupplierLineFormView.FindControl("drpProduct");
            var drpPlant = (DropDownList)SupplierLineFormView.FindControl("drpPlant");
            string New_Supplier_Line = drpSupplier.SelectedValue + drpProduct.SelectedValue + drpPlant.SelectedValue;
            ListItem selectedListItem = drpSupplierLine.Items.FindByValue(New_Supplier_Line);
            try
            {
                if (SupplierLineFormView.CurrentMode == FormViewMode.Insert)
                {
                    if (selectedListItem == null)
                    {
                        ObjSupplier.AddNewSupplierLine(GetCustomerDetails(false));
                        LoadDropDownLists<IMPALLibrary.SupplierLineCode>(ObjSupplier.GetAllSuppliersLine(), drpSupplierLine, "Supplier_Line_Code", "Long_Description", true, "");
                        SupplierLineFormView.ChangeMode(FormViewMode.Edit);
                        BindSupplierLineFormView(New_Supplier_Line);
                        PnlSupplier.Enabled = false;
                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = true;
                        //btnReport.Enabled = false;
                        btnSearch.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Supplier Line Already Exists');", true);
                    }

                }
                else
                {
                    if (SupplierLineFormView.CurrentMode == FormViewMode.Edit)
                    {
                        ObjSupplier.UpdateSupplierLine(GetCustomerDetails(false));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Supplier Line Updated Succesfully');", true);
                        SupplierLineFormView.ChangeMode(FormViewMode.Insert);
                        btnSearch.Visible = true;
                        drpSupplierLine.Enabled = false;
                        drpSupplierLine.SelectedIndex = 0;

                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private SupplierLineDetails GetCustomerDetails(bool Reset)
        {
            SupplierLineDetails SupplierLineFields = new SupplierLineDetails();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                var txtLineCode = (TextBox)SupplierLineFormView.FindControl("txtLineCode");
                var drpSupplier = (DropDownList)SupplierLineFormView.FindControl("drpSupplier");
                var drpProduct = (DropDownList)SupplierLineFormView.FindControl("drpProduct");
                var drpPlant = (DropDownList)SupplierLineFormView.FindControl("drpPlant");
                var drpEDInd = (DropDownList)SupplierLineFormView.FindControl("drpEDInd");
                var txtShortDesc = (TextBox)SupplierLineFormView.FindControl("txtShortDesc");
                var txtLongDesc = (TextBox)SupplierLineFormView.FindControl("txtLongDesc");
                var txtEDValue = (TextBox)SupplierLineFormView.FindControl("txtEDValue");
                var txtPatternWeek = (TextBox)SupplierLineFormView.FindControl("txtPatternWeek");
                var txtGraceDay = (TextBox)SupplierLineFormView.FindControl("txtGraceDay");
                var txtInvDuration = (TextBox)SupplierLineFormView.FindControl("txtInvDuration");
                var txtIvnTimes = (TextBox)SupplierLineFormView.FindControl("txtIvnTimes");
                var txtMinItems = (TextBox)SupplierLineFormView.FindControl("txtMinItems");
                var txtIvnMonth = (TextBox)SupplierLineFormView.FindControl("txtIvnMonth");
                var txtHoldPattern = (TextBox)SupplierLineFormView.FindControl("txtHoldPattern");
                var txtDepotSurcharge = (TextBox)SupplierLineFormView.FindControl("txtDepotSurcharge");
                var ChkDeportPayMent = (CheckBox)SupplierLineFormView.FindControl("ChkDeportPayMent");
                var txtFreight = (TextBox)SupplierLineFormView.FindControl("txtFreight");
                var txtPurchaseDis = (TextBox)SupplierLineFormView.FindControl("txtPurchaseDis");
                var txtEDDis = (TextBox)SupplierLineFormView.FindControl("txtEDDis");
                var txtAddDis1 = (TextBox)SupplierLineFormView.FindControl("txtAddDis1");
                var txtAddDis2 = (TextBox)SupplierLineFormView.FindControl("txtAddDis2");
                var txtAddDis3 = (TextBox)SupplierLineFormView.FindControl("txtAddDis3");
                var txtAddDis4 = (TextBox)SupplierLineFormView.FindControl("txtAddDis4");
                var txtAddDis5 = (TextBox)SupplierLineFormView.FindControl("txtAddDis5");
                var txtDealerDis = (TextBox)SupplierLineFormView.FindControl("txtDealerDis");





                if (Reset == false)
                {
                    SupplierLineFields.supplier_plant_code = drpPlant.SelectedValue;
                    SupplierLineFields.short_description = txtShortDesc.Text;
                    SupplierLineFields.long_description = txtLongDesc.Text;
                    SupplierLineFields.ed_indicator = drpEDInd.SelectedValue;
                    SupplierLineFields.ed_value = txtEDValue.Text;
                    SupplierLineFields.order_pattern = txtPatternWeek.Text;
                    SupplierLineFields.price_revision_days = txtGraceDay.Text;
                    SupplierLineFields.Stock_Verification_Times = txtIvnTimes.Text;
                    SupplierLineFields.Stock_Verification_Duration = txtInvDuration.Text;
                    SupplierLineFields.purchase_discount = txtPurchaseDis.Text;
                    SupplierLineFields.excise_duty_discount = txtEDDis.Text;
                    SupplierLineFields.minimum_items_per_day = txtMinItems.Text;
                    SupplierLineFields.stock_holding_pattern = txtHoldPattern.Text;
                    SupplierLineFields.depot_surcharge = txtDepotSurcharge.Text;
                    SupplierLineFields.freight_surcharge = txtFreight.Text;
                    SupplierLineFields.additional_discount1 = txtAddDis1.Text;
                    SupplierLineFields.additional_discount2 = txtAddDis2.Text;
                    SupplierLineFields.additional_discount3 = txtAddDis3.Text;
                    SupplierLineFields.additional_discount4 = txtAddDis4.Text;
                    SupplierLineFields.additional_discount5 = txtAddDis5.Text;
                    SupplierLineFields.stock_verification_first_month = txtIvnMonth.Text;
                    SupplierLineFields.supplier_line_code = txtLineCode.Text;
                    SupplierLineFields.supplier_code = drpSupplier.SelectedValue;
                    SupplierLineFields.supplier_product_code = drpProduct.SelectedValue;
                    SupplierLineFields.Supplier_Name = drpProduct.SelectedItem.Text;
                    SupplierLineFields.supplier_product_short_name = drpProduct.SelectedItem.Text;
                    SupplierLineFields.Depot_Payment = ChkDeportPayMent.Checked;
                    SupplierLineFields.Supplier_Classification = "";
                    SupplierLineFields.Aging = "";
                    SupplierLineFields.DealerDiscount = txtDealerDis.Text;

                }
                else
                {
                    txtLineCode.Text = "";
                    drpSupplier.SelectedIndex = 0;
                    drpProduct.SelectedIndex = 0;
                    drpPlant.SelectedIndex = 0;
                    drpEDInd.SelectedIndex = 0;
                    txtShortDesc.Text = "";
                    txtLongDesc.Text = "";
                    txtEDValue.Text = "";
                    txtPatternWeek.Text = "";
                    txtGraceDay.Text = "";
                    txtInvDuration.Text = "";
                    txtIvnTimes.Text = "";
                    txtMinItems.Text = "";
                    txtIvnMonth.Text = "";
                    txtHoldPattern.Text = "";
                    txtDepotSurcharge.Text = "";
                    txtFreight.Text = "";
                    txtPurchaseDis.Text = "";
                    ChkDeportPayMent.Checked = false;
                    txtEDDis.Text = "";
                    txtAddDis1.Text = "";
                    txtAddDis2.Text = "";
                    txtAddDis3.Text = "";
                    txtAddDis4.Text = "";
                    txtAddDis5.Text = "";
                    txtDealerDis.Text = "";



                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return SupplierLineFields;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //var txtLineCode = (TextBox)SupplierLineFormView.FindControl("txtLineCode");
                //string LineCode = txtLineCode.Text;
                //Response.Redirect("SupplierLineReport.aspx?LineCode="+LineCode);
                Server.Execute("SupplierLineReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
    }
}
