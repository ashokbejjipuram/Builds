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
    public partial class SupplierDetails : System.Web.UI.Page
    {
        List<SupplierDetail> SupplierDetail;

        Suppliers ObjSupplier = new Suppliers();

        CustomerDetails ObjCustomer = new CustomerDetails();

        ImpalLibrary objImpalLibrary = new ImpalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {

                    LoadDropDownLists<IMPALLibrary.Supplier>(ObjSupplier.GetSuppliercodewithOutDefault(), drpSupplierCode, "SupplierCode", "SupplierName", true, "");
                    if ((string)Session["RoleCode"] == "BEDP")
                    {

                        SupplierFormView.DefaultMode = FormViewMode.ReadOnly;
                        drpSupplierCode.Enabled = true;
                        btnSearch.Visible = false;
                        PnlSupplier.Enabled = false;
                        BtnSubmit.Enabled = false;
                        btnReset.Enabled = false;
                    }
                    else
                    {
                        SupplierFormView.DefaultMode = FormViewMode.Insert;
                        drpSupplierCode.Enabled = false;
                        PnlSupplier.Enabled = true;
                        btnSearch.Visible = true;
                        BtnSubmit.Enabled = true;
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
                SupplierFormView.EditItemTemplate = SupplierFormView.ItemTemplate;
                SupplierFormView.InsertItemTemplate = SupplierFormView.ItemTemplate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void SupplierFormView_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void drpSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindSupplierFormView(drpSupplierCode.SelectedValue);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindSupplierFormView(string SupplierCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                SupplierDetail = new List<SupplierDetail>();
                SupplierDetail = ObjSupplier.ViewSupplier(SupplierCode);
                SupplierFormView.DataSource = SupplierDetail;
                SupplierFormView.DataBind();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void SupplierFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void SupplierFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //if (SupplierFormView.CurrentMode != FormViewMode.ReadOnly)
                //{

                var drpAutoLock = (DropDownList)SupplierFormView.FindControl("drpAutoLock");
                var drpSupplierType = (DropDownList)SupplierFormView.FindControl("drpSupplierType");
                var RdoInsYes = (RadioButton)SupplierFormView.FindControl("RdoInsYes");
                var RdoInsNo = (RadioButton)SupplierFormView.FindControl("RdoInsNo");
                var chkBMS = (CheckBox)SupplierFormView.FindControl("chkAdPayMent");
                var chkAdPayMent = (CheckBox)SupplierFormView.FindControl("chkBMS");
                LoadDropDownLists<CustomerType>(ObjCustomer.GetAllCustomerType(), drpSupplierType, "CustomerTypeCode", "CustomerTypeDesc", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("SupplierAutoLocking"), drpAutoLock, "DisplayValue", "DisplayText", true, "");
                if (SupplierDetail != null)
                {
                    drpSupplierType.SelectedValue = SupplierDetail[0].Party_Type_Code;
                    drpAutoLock.SelectedValue = SupplierDetail[0].Auto_Locking;
                    chkBMS.Checked = SupplierDetail[0].BMS_Indicator;
                    chkAdPayMent.Checked = SupplierDetail[0].Group_Company_Indicator;
                    RdoInsYes.Checked = SupplierDetail[0].Insurance_Indicator == true ? true : false;
                    RdoInsNo.Checked = SupplierDetail[0].Insurance_Indicator == false ? true : false;
                }

                if (SupplierFormView.CurrentMode == FormViewMode.Edit)
                {

                    BtnSubmit.Text = "Update";
                }
                else if (SupplierFormView.CurrentMode == FormViewMode.Insert)
                {
                    BtnSubmit.Text = "Add";
                }
                else
                {
                    BtnSubmit.Visible = false;
                    btnReset.Visible = false;
                }
                //}
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void Customer_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (SupplierFormView.CurrentMode != FormViewMode.ReadOnly)
                {
                    var txtChartAc = (TextBox)SupplierFormView.FindControl("txtChartAc");
                    txtChartAc.Text = Session["ChatAccCode"].ToString();
                    Session["ChatAccCode"] = "";
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
                drpSupplierCode.Enabled = true;
                btnSearch.Visible = false;
                SupplierFormView.ChangeMode(FormViewMode.Edit);
                if (drpSupplierCode.SelectedIndex > 0)
                    BindSupplierFormView(drpSupplierCode.SelectedValue);
                var drpSupplierType = (DropDownList)SupplierFormView.FindControl("drpSupplierType");
                var txtCode = (TextBox)SupplierFormView.FindControl("txtCode");
                txtCode.Enabled = false;
                drpSupplierType.Enabled = false;

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
                var drpSupplierType = (DropDownList)SupplierFormView.FindControl("drpSupplierType");
                var txtCode = (TextBox)SupplierFormView.FindControl("txtCode");
                if (SupplierFormView.CurrentMode == FormViewMode.Edit)
                {
                    drpSupplierCode.Enabled = false;
                    drpSupplierCode.SelectedIndex = 0;
                    btnSearch.Visible = true;
                    GetCustomerDetails(true);
                    SupplierFormView.ChangeMode(FormViewMode.Insert);
                    PnlSupplier.Enabled = true;

                    txtCode.Enabled = true;
                    drpSupplierType.Enabled = true;
                }

                if (SupplierFormView.CurrentMode == FormViewMode.Insert)
                {
                    GetCustomerDetails(true);
                    drpSupplierCode.Enabled = false;
                    drpSupplierCode.SelectedIndex = 0;
                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    btnSearch.Visible = true;
                    PnlSupplier.Enabled = true;
                    txtCode.Enabled = true;
                    drpSupplierType.Enabled = true;

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
            var txtCode = (TextBox)SupplierFormView.FindControl("txtCode");
            ListItem selectedListItem = drpSupplierCode.Items.FindByValue(txtCode.Text);

            try
            {

                SupplierDetail = ObjSupplier.ViewSupplier(txtCode.Text);

                if (SupplierFormView.CurrentMode == FormViewMode.Insert)
                {
                    if (SupplierDetail.Count == 0)
                    {
                        string SupplierCode = string.Empty;
                        SupplierCode = ObjSupplier.AddNewSupplier(GetCustomerDetails(false));
                        LoadDropDownLists<IMPALLibrary.Supplier>(ObjSupplier.GetSuppliercodewithOutDefault(), drpSupplierCode, "SupplierCode", "SupplierName", true, "");
                        SupplierFormView.ChangeMode(FormViewMode.Edit);
                        if (!string.IsNullOrEmpty(SupplierCode))
                        {
                            BindSupplierFormView(SupplierCode);
                            PnlSupplier.Enabled = false;
                            BtnSubmit.Enabled = false;
                            btnReset.Enabled = true;
                            btnSearch.Visible = false;
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "Alert", "alert('Supplier Already Exists');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Supplier Code Already Exists');", true);
                    }

                }
                else
                {
                    if (SupplierFormView.CurrentMode == FormViewMode.Edit)
                    {
                        ObjSupplier.UpdateSupplier(GetCustomerDetails(false));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Supplier Updated Succesfully');", true);
                        SupplierFormView.ChangeMode(FormViewMode.Insert);
                        btnSearch.Visible = true;
                        drpSupplierCode.Enabled = false;
                        drpSupplierCode.SelectedIndex = 0;
                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private SupplierDetail GetCustomerDetails(bool Reset)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            SupplierDetail SupplierFields = new SupplierDetail();

            try
            {

                var txtCode = (TextBox)SupplierFormView.FindControl("txtCode");
                var txtName = (TextBox)SupplierFormView.FindControl("txtName");
                var drpSupplierType = (DropDownList)SupplierFormView.FindControl("drpSupplierType");
                var RdoInsYes = (RadioButton)SupplierFormView.FindControl("RdoInsYes");
                var RdoInsNo = (RadioButton)SupplierFormView.FindControl("RdoInsNo");
                var txtAdd = (TextBox)SupplierFormView.FindControl("txtAdd");
                var txtPinCode = (TextBox)SupplierFormView.FindControl("txtPinCode");
                var txtPhone = (TextBox)SupplierFormView.FindControl("txtPhone");
                var txtFax = (TextBox)SupplierFormView.FindControl("txtFax");
                var txtTelex = (TextBox)SupplierFormView.FindControl("txtTelex");
                var txtEmail = (TextBox)SupplierFormView.FindControl("txtEmail");
                var txtContactPerson = (TextBox)SupplierFormView.FindControl("txtContactPerson");
                var txtContactDesignation = (TextBox)SupplierFormView.FindControl("txtContactDesignation");
                var txtPreferredCarrier = (TextBox)SupplierFormView.FindControl("txtPreferredCarrier");
                var txtDestination = (TextBox)SupplierFormView.FindControl("txtDestination");
                var chkAdPayMent = (CheckBox)SupplierFormView.FindControl("chkAdPayMent");
                var chkBMS = (CheckBox)SupplierFormView.FindControl("chkBMS");
                var txtAlphacode = (TextBox)SupplierFormView.FindControl("txtAlphacode");
                var txtChartAc = (TextBox)SupplierFormView.FindControl("txtChartAc");
                var txtStockValue = (TextBox)SupplierFormView.FindControl("txtStockValue");
                var drpAutoLock = (DropDownList)SupplierFormView.FindControl("drpAutoLock");
                var txtSales = (TextBox)SupplierFormView.FindControl("txtSales");
                var txtCentraltax = (TextBox)SupplierFormView.FindControl("txtCentraltax");




                if (Reset == false)
                {
                    SupplierFields.Supplier_Code = txtCode.Text;
                    SupplierFields.Supplier_Name = txtName.Text;
                    SupplierFields.Chart_of_Account_Code = txtChartAc.Text;
                    SupplierFields.Party_Type_Code = drpSupplierType.SelectedValue;
                    SupplierFields.Address = txtAdd.Text;
                    SupplierFields.Pincode = txtPinCode.Text;
                    SupplierFields.Alpha_Code = txtAlphacode.Text;
                    SupplierFields.Phone = txtPhone.Text;
                    SupplierFields.Fax = txtFax.Text;
                    SupplierFields.Email = txtEmail.Text;
                    SupplierFields.Telex = txtTelex.Text;
                    SupplierFields.Contact_Person = txtContactPerson.Text;
                    SupplierFields.Contact_Designation = txtContactDesignation.Text;
                    SupplierFields.Carrier = txtPreferredCarrier.Text;
                    SupplierFields.Destination = txtDestination.Text;
                    SupplierFields.Local_Sales_Tax_Number = txtSales.Text;
                    SupplierFields.Central_Sales_Tax_Number = txtCentraltax.Text;
                    SupplierFields.Purchase_Upto_Previous_Year = "";
                    SupplierFields.Purchase_During_Previous_Year = "";
                    SupplierFields.Purchase_During_Current_Year = "";
                    SupplierFields.Outstanding_Amount = "";
                    SupplierFields.Oldest_Pending_Invoice = "";
                    SupplierFields.Insurance_Indicator = RdoInsYes.Checked;
                    SupplierFields.BMS_Indicator = chkBMS.Checked;
                    SupplierFields.Group_Company_Indicator = chkAdPayMent.Checked;
                    SupplierFields.place = "";
                    SupplierFields.Auto_Locking = drpAutoLock.SelectedValue;
                    SupplierFields.Optimum_Stock_value = txtStockValue.Text;

                }
                else
                {
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtChartAc.Text = "";
                    txtPinCode.Text = "";
                    txtAlphacode.Text = "";
                    txtPhone.Text = "";
                    txtFax.Text = "";
                    txtEmail.Text = "";
                    txtTelex.Text = "";
                    txtContactPerson.Text = "";
                    txtContactDesignation.Text = "";
                    txtPreferredCarrier.Text = "";
                    txtDestination.Text = "";
                    txtAlphacode.Text = "";
                    drpSupplierType.SelectedIndex = 0;
                    RdoInsYes.Checked = false;
                    RdoInsNo.Checked = false;
                    chkAdPayMent.Checked = false;
                    chkBMS.Checked = false;
                    txtStockValue.Text = "";
                    txtSales.Text = "";
                    txtCentraltax.Text = "";
                    txtAdd.Text = "";



                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return SupplierFields;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //var txtCode = (TextBox)SupplierFormView.FindControl("txtCode");
                //string LineCode = txtCode.Text;
                //Response.Redirect("SupplierDetailsReport.aspx?LineCode=" + LineCode);
                Server.Execute("SupplierDetailsReport.aspx");

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
