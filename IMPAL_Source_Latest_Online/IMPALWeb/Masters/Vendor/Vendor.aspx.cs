using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.VendorDetails;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Masters.Vendor
{
    public partial class Vendor : System.Web.UI.Page
    {
        List<VendorFields> custDetails;
        VendorDetails vendor = new VendorDetails();
        Branches Branch = new Branches();
        Towns Towns = new Towns();
        StateMasters States = new StateMasters();
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    VendorFormView.DefaultMode = FormViewMode.Insert;
                    LoadDropDownLists<VendorDtls>(vendor.GetVendorswithLocation(Session["BranchCode"].ToString()), drpVendorCode, "Code", "Name", true, "");

                    VendorFormView.DefaultMode = FormViewMode.Insert;
                    drpVendorCode.Enabled = false;
                    PnlVendor.Enabled = true;
                    btnSearch.Visible = true;
                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;

                    var ddlLocalOutStaionList = (DropDownList)VendorFormView.FindControl("ddlLocalOutStaionList");
                    LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlLocalOutStaionList, "StateCode", "StateName", true, "");

                    var ddlBranch = (DropDownList)VendorFormView.FindControl("ddlBranch");
                    var hdnStateCode = (HiddenField)VendorFormView.FindControl("hdnStateCode");

                    if (ddlBranch.SelectedValue.ToUpper() == "")
                        hdnStateCode.Value = States.GetCurrentState(Session["BranchCode"].ToString()).ToString();
                    else
                        hdnStateCode.Value = States.GetCurrentState(ddlBranch.SelectedValue).ToString();

                    ddlLocalOutStaionList.SelectedValue = hdnStateCode.Value;
                    ddlLocalOutStaionList.Enabled = false;
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
                VendorFormView.EditItemTemplate = VendorFormView.ItemTemplate;
                VendorFormView.InsertItemTemplate = VendorFormView.ItemTemplate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void VendorFormView_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void drpVendorCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindVendorFormView(drpVendorCode.SelectedValue);                
                var branch = (DropDownList)VendorFormView.FindControl("ddlBranch");
                var ddlTown = (DropDownList)VendorFormView.FindControl("ddlTown");
                var ddlLocalOutStaionList = (DropDownList)VendorFormView.FindControl("ddlLocalOutStaionList");
                LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(branch.SelectedValue), ddlTown, "Towncode", "TownName", true, "");
                LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlLocalOutStaionList, "StateCode", "StateName", true, "");
  
                var ddlBranch = (DropDownList)VendorFormView.FindControl("ddlBranch");
                var hdnStateCode = (HiddenField)VendorFormView.FindControl("hdnStateCode");
                if (ddlBranch.SelectedValue.ToUpper() == "")
                    hdnStateCode.Value = States.GetCurrentState(Session["BranchCode"].ToString()).ToString();
                else
                    hdnStateCode.Value = States.GetCurrentState(ddlBranch.SelectedValue).ToString();
                ddlLocalOutStaionList.SelectedValue = hdnStateCode.Value;
                ddlLocalOutStaionList.Enabled = false;
                branch.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlLocalOrOutStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var ddlLocalOrOutStation = (DropDownList)VendorFormView.FindControl("ddlLocalOrOutStation");
                var ddlLocalOutStaionList = (DropDownList)VendorFormView.FindControl("ddlLocalOutStaionList");
                
                if (ddlLocalOrOutStation.SelectedValue == "O")
                {
                    ddlLocalOutStaionList.SelectedIndex = 0;
                    ddlLocalOutStaionList.Enabled = true;
                }
                else
                {
                    var hdnStateCode = (HiddenField)VendorFormView.FindControl("hdnStateCode");                
                    ddlLocalOutStaionList.SelectedValue = hdnStateCode.Value;
                    ddlLocalOutStaionList.Enabled = false;                    
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
                li.Text = "Select";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var ddlBranch = (DropDownList)VendorFormView.FindControl("ddlBranch");                
                var ddlSalesman = (DropDownList)VendorFormView.FindControl("ddlSalesman");
                var ddlTown = (DropDownList)VendorFormView.FindControl("ddlTown");
                LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(ddlBranch.SelectedValue), ddlTown, "Towncode", "TownName", true, "");
                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindVendorFormView(string VendorCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                custDetails = new List<VendorFields>();
                custDetails = vendor.ViewVendor(Session["BranchCode"].ToString(), VendorCode);
                VendorFormView.DataSource = custDetails;
                VendorFormView.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void VendorFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void VendorFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var ddlCustType = (DropDownList)VendorFormView.FindControl("ddlCustType");
                var ddlTown = (DropDownList)VendorFormView.FindControl("ddlTown");
                var ddlBranch = (DropDownList)VendorFormView.FindControl("ddlBranch");
                var ddlStatus = (DropDownList)VendorFormView.FindControl("ddlStatus");
                LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranch(), ddlBranch, "BranchCode", "BranchName", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("VendorStatus"), ddlStatus, "DisplayValue", "DisplayText", true, "");
                
                if (custDetails != null)
                {
                    ddlTown.SelectedValue = custDetails[0].Town_Code;
                    ddlBranch.SelectedValue = custDetails[0].Branch_Code;
                    ddlStatus.SelectedValue = custDetails[0].status;
                }

                if (VendorFormView.CurrentMode == FormViewMode.Edit)
                {
                    BtnSubmit.Text = "Update";
                }
                else if (VendorFormView.CurrentMode == FormViewMode.Insert)
                {

                    BtnSubmit.Text = "Add";
                }
                else
                {
                    BtnSubmit.Visible = false;
                    btnReset.Visible = false;
                    // btnReport.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void ddlSalesman_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                VendorFields cust = new VendorFields();


                var txtddlSalesmanCode = (TextBox)VendorFormView.FindControl("txtSalesmanCode");
                var ddlSalesman = (DropDownList)VendorFormView.FindControl("ddlSalesman");
                txtddlSalesmanCode.Text = ddlSalesman.SelectedValue;

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Vendor_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (VendorFormView.CurrentMode != FormViewMode.ReadOnly)
                {
                    var txtChartAc = (TextBox)VendorFormView.FindControl("txtChartAc");
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
                drpVendorCode.Enabled = true;
                btnSearch.Visible = false;
                VendorFormView.ChangeMode(FormViewMode.Edit);
                BtnSubmit.Text = "Update";
                if (drpVendorCode.SelectedIndex > 0)
                    BindVendorFormView(drpVendorCode.SelectedValue);
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
                Response.Redirect("Vendor.aspx", false);
                Context.ApplicationInstance.CompleteRequest(); 
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
                if (VendorFormView.CurrentMode == FormViewMode.Insert)
                {
                    string VendorCode = vendor.AddNewVendor(GetVendorDetails(false));
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Information Inserted Succesfully');", true);
                    var txtName = (TextBox)VendorFormView.FindControl("txtName");
                    VendorFormView.ChangeMode(FormViewMode.Edit);
                    BindVendorFormView(VendorCode);
                    PnlVendor.Enabled = false;
                    BtnSubmit.Enabled = false;
                    btnReset.Enabled = true;
                    //btnReport.Enabled = false;
                    btnSearch.Visible = false;
                }
                else
                {
                    if (VendorFormView.CurrentMode == FormViewMode.Edit)
                    {
                        vendor.UpdateVendor(GetVendorDetails(false));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Updated Succesfully');", true);
                        VendorFormView.ChangeMode(FormViewMode.Insert);
                        btnSearch.Visible = true;
                        drpVendorCode.Enabled = false;
                        drpVendorCode.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private VendorFields GetVendorDetails(bool Reset)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            VendorFields cust = new VendorFields();

            try
            {
                var txtCode = (TextBox)VendorFormView.FindControl("txtCode");
                var txtName = (TextBox)VendorFormView.FindControl("txtName");
                var ddlBranch = (DropDownList)VendorFormView.FindControl("ddlBranch");
                var txtAdd1 = (TextBox)VendorFormView.FindControl("txtAdd1");
                var txtAdd2 = (TextBox)VendorFormView.FindControl("txtAdd2");
                var txtAdd3 = (TextBox)VendorFormView.FindControl("txtAdd3");
                var txtAdd4 = (TextBox)VendorFormView.FindControl("txtAdd4");
                var txtLocation = (TextBox)VendorFormView.FindControl("txtLocation");
                var txtPinCode = (TextBox)VendorFormView.FindControl("txtPinCode");
                var ddlTown = (DropDownList)VendorFormView.FindControl("ddlTown");
                var txtPhone = (TextBox)VendorFormView.FindControl("txtPhone");
                var txtEmail = (TextBox)VendorFormView.FindControl("txtEmail");
                var txtContactPerson = (TextBox)VendorFormView.FindControl("txtContactPerson");
                var txtAlphacode = (TextBox)VendorFormView.FindControl("txtAlphacode");
                var txtGSTIN = (TextBox)VendorFormView.FindControl("txtGSTIN");
                var txtChartAc = (TextBox)VendorFormView.FindControl("txtChartAc");
                var ddlStatus = (DropDownList)VendorFormView.FindControl("ddlStatus");
                var txtBankName = (TextBox)VendorFormView.FindControl("txtBankName");
                var txtBranchName = (TextBox)VendorFormView.FindControl("txtBranchName");
                var txtBankAddress = (TextBox)VendorFormView.FindControl("txtBankAddress");
                var ddlLocalOutStaionList = (DropDownList)VendorFormView.FindControl("ddlLocalOutStaionList");

                if (Reset == false)
                {
                    cust.Vendor_Code = txtCode.Text;
                    cust.Vendor_Name = txtName.Text;
                    cust.Chart_of_Account_Code = txtChartAc.Text;
                    cust.Address = "";
                    cust.Pincode = txtPinCode.Text;
                    cust.Branch_Code = ddlBranch.SelectedValue;
                    cust.Town_Code = ddlTown.Text;
                    cust.Vendor_Alpha_Code = txtAlphacode.Text;
                    cust.Phone = txtPhone.Text;
                    cust.Email = txtEmail.Text;
                    cust.Contact_Person = txtContactPerson.Text;
                    cust.GSTIN = txtGSTIN.Text;
                    cust.Vendor_bank_name = txtBankName.Text;
                    cust.Vendor_bank_branch = txtBranchName.Text;
                    cust.Vendor_bank_address = txtBankAddress.Text;
                    cust.status = ddlStatus.SelectedValue;
                    cust.Branch_Name = ddlBranch.SelectedItem.ToString();
                    cust.Town_Name = ddlTown.SelectedItem.ToString();
                    cust.Address1 = txtAdd1.Text;
                    cust.Address2 = txtAdd2.Text;
                    cust.Address3 = txtAdd3.Text;
                    cust.Address4 = txtAdd4.Text;
                    cust.location = txtLocation.Text;
                }
                else
                {
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtChartAc.Text = "";
                    txtPinCode.Text = "";
                    txtAlphacode.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtContactPerson.Text = "";
                    txtGSTIN.Text = "";
                    txtBankName.Text = "";
                    txtBranchName.Text = "";
                    txtBankAddress.Text = "";
                    ddlStatus.SelectedIndex = 0;
                    ddlBranch.SelectedIndex = 0;
                    ddlTown.SelectedIndex = 0;
                    txtAdd1.Text = "";
                    txtAdd2.Text = "";
                    txtAdd3.Text = "";
                    txtAdd4.Text = "";
                    txtLocation.Text = "";
                    drpVendorCode.SelectedIndex = 0;
                    ddlLocalOutStaionList.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return cust;
        }

        #region Report Button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                //string strVendorCode = drpVendorCode.SelectedValue.ToString();
                Server.Execute("VendorReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
