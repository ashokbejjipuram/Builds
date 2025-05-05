using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Masters.Customer
{
    public partial class Customer : System.Web.UI.Page
    {
        List<CustomerFields> custDetails;
        CustomerDetails customer = new CustomerDetails();
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
                    CustomerFormView.DefaultMode = FormViewMode.Insert;
                    LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), drpCustomerCode, "Code", "Name", true, "");

					CustomerFormView.DefaultMode = FormViewMode.Insert;
                    drpCustomerCode.Enabled = false;
                    PnlCustomer.Enabled = true;
                    btnSearch.Visible = true;
                    BtnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    BtnSubmit.Attributes.Remove("OnClick");

                    var ddlCustomerState = (DropDownList)CustomerFormView.FindControl("ddlCustomerState");
                    LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlCustomerState, "StateCode", "StateName", true, "");
					
					var ddlBranch = (DropDownList)CustomerFormView.FindControl("ddlBranch");
                    var hdnStateCode = (HiddenField)CustomerFormView.FindControl("hdnStateCode");

                    if (ddlBranch.SelectedValue.ToUpper() == "")
                        hdnStateCode.Value = States.GetCurrentState(Session["BranchCode"].ToString()).ToString();
                    else
                        hdnStateCode.Value = States.GetCurrentState(ddlBranch.SelectedValue).ToString();

                    ddlCustomerState.SelectedValue = hdnStateCode.Value;
                    //ddlCustomerState.Enabled = false;
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
                CustomerFormView.EditItemTemplate = CustomerFormView.ItemTemplate;
                CustomerFormView.InsertItemTemplate = CustomerFormView.ItemTemplate;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void CustomerFormView_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void drpCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BindCustomerFormView(Session["BranchCode"].ToString(), drpCustomerCode.SelectedValue);                
                var branch = (DropDownList)CustomerFormView.FindControl("ddlBranch");
                var ddlTown = (DropDownList)CustomerFormView.FindControl("ddlTown");
                var ddlCustomerState = (DropDownList)CustomerFormView.FindControl("ddlCustomerState");
                var ddlLocalOrOutStation = (DropDownList)CustomerFormView.FindControl("ddlLocalOrOutStation");
                LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(branch.SelectedValue), ddlTown, "Towncode", "TownName", true, "");
                LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlCustomerState, "StateCode", "StateName", true, "");
  
                var ddlBranch = (DropDownList)CustomerFormView.FindControl("ddlBranch");
                var hdnStateCode = (HiddenField)CustomerFormView.FindControl("hdnStateCode");
                var hdnCreditLimit = (HiddenField)CustomerFormView.FindControl("hdnCreditLimit");
                TextBox CrLimit = (TextBox)CustomerFormView.FindControl("txtCreditLimit");

                hdnCreditLimit.Value = CrLimit.Text;
                hdnStateCode.Value = States.GetCustomerState(ddlBranch.SelectedValue, drpCustomerCode.SelectedValue).ToString();
                ddlCustomerState.SelectedValue = hdnStateCode.Value;
                ddlCustomerState.Enabled = true;
                ddlLocalOrOutStation.SelectedValue = custDetails[0].Local_Outstation;
                branch.Enabled = false;

                if (drpCustomerCode.SelectedIndex == 0)
                    BtnSubmit.Attributes.Remove("OnClick");
                else
                {
                    BtnSubmit.Attributes.Remove("OnClick");
                    BtnSubmit.Attributes.Add("OnClick", "return CheckValidDateFields();");
                }
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
                var ddlLocalOrOutStation = (DropDownList)CustomerFormView.FindControl("ddlLocalOrOutStation");
                var ddlCustomerState = (DropDownList)CustomerFormView.FindControl("ddlCustomerState");

                var hdnStateCode = (HiddenField)CustomerFormView.FindControl("hdnStateCode");
                hdnStateCode.Value = States.GetCurrentState(Session["BranchCode"].ToString()).ToString();
                
                if (ddlLocalOrOutStation.SelectedValue == "L")
                {
                    ddlCustomerState.SelectedValue = hdnStateCode.Value;
                    ddlCustomerState.Enabled = false;
                }
                else
                {
                    ddlCustomerState.SelectedIndex = 0;
                    ddlCustomerState.Enabled = true;
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
                var ddlBranch = (DropDownList)CustomerFormView.FindControl("ddlBranch");                
                var ddlSalesman = (DropDownList)CustomerFormView.FindControl("ddlSalesman");
                var ddlTown = (DropDownList)CustomerFormView.FindControl("ddlTown");
                LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(ddlBranch.SelectedValue), ddlTown, "Towncode", "TownName", true, "");
                LoadDropDownLists<CustomerFields>(customer.GetSalesman(ddlBranch.SelectedValue), ddlSalesman, "Salesman_Code", "Salesman", true, "");
                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindCustomerFormView(string BranchCode, string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                custDetails = new List<CustomerFields>();
                custDetails = customer.ViewCustomer(BranchCode, CustomerCode);
                CustomerFormView.DataSource = custDetails;
                CustomerFormView.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void CustomerFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void CustomerFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var ddlCustType = (DropDownList)CustomerFormView.FindControl("ddlCustType");
                var ddlTown = (DropDownList)CustomerFormView.FindControl("ddlTown");
                var ddlBranch = (DropDownList)CustomerFormView.FindControl("ddlBranch");
                var ddlStatus = (DropDownList)CustomerFormView.FindControl("ddlStatus");
                var ddlCrLimitIndicator = (DropDownList)CustomerFormView.FindControl("ddlCrLimitIndicator");
                var ddlSalesman = (DropDownList)CustomerFormView.FindControl("ddlSalesman");
                var txtSalesmanCode = (TextBox)CustomerFormView.FindControl("txtSalesmanCode");
                var ddlLocalOrOutStation = (DropDownList)CustomerFormView.FindControl("ddlLocalOrOutStation");
                var ddlCustClassifiation = (DropDownList)CustomerFormView.FindControl("ddlCustClassifiation");
                var ddlCustSegment = (DropDownList)CustomerFormView.FindControl("ddlCustSegment");
                LoadDropDownLists<CustomerType>(customer.GetAllCustomerType(), ddlCustType, "CustomerTypeCode", "CustomerTypeDesc", true, "");                
                LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranch(), ddlBranch, "BranchCode", "BranchName", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerStatus"), ddlStatus, "DisplayValue", "DisplayText", true, "");
                LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerCreditIndicator"), ddlCrLimitIndicator, "DisplayValue", "DisplayText", true, "");
                
                if (custDetails != null)
                {
                    ddlCustType.SelectedValue = custDetails[0].Party_Type_Code;
                    ddlTown.SelectedValue = custDetails[0].Town_Code;
                    ddlBranch.SelectedValue = custDetails[0].Branch_Code;
                    ddlCustClassifiation.SelectedValue = custDetails[0].Customer_Classification;
                    ddlCustSegment.SelectedValue = custDetails[0].Customer_Segment;
                    LoadDropDownLists<CustomerFields>(customer.GetSalesman(ddlBranch.SelectedValue), ddlSalesman, "Salesman_Code", "Salesman", true, "");
                    ddlStatus.SelectedValue = custDetails[0].status;
                    ddlCrLimitIndicator.SelectedValue = custDetails[0].Cash_Credit_Limit_Indicator;
                    ddlSalesman.SelectedValue = custDetails[0].Salesman_Code;
                    txtSalesmanCode.Text = custDetails[0].Salesman_Code;
                    ddlLocalOrOutStation.SelectedValue = custDetails[0].CustOSLSStatus;
                }

                if (CustomerFormView.CurrentMode == FormViewMode.Edit)
                {
                    BtnSubmit.Text = "Update";
                }
                else if (CustomerFormView.CurrentMode == FormViewMode.Insert)
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
                CustomerFields cust = new CustomerFields();

                var txtddlSalesmanCode = (TextBox)CustomerFormView.FindControl("txtSalesmanCode");
                var ddlSalesman = (DropDownList)CustomerFormView.FindControl("ddlSalesman");
                txtddlSalesmanCode.Text = ddlSalesman.SelectedValue;
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
                if (CustomerFormView.CurrentMode != FormViewMode.ReadOnly)
                {
                    var txtChartAc = (TextBox)CustomerFormView.FindControl("txtChartAc");
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
                drpCustomerCode.Enabled = true;
                btnSearch.Visible = false;
                CustomerFormView.ChangeMode(FormViewMode.Edit);
                BtnSubmit.Text = "Update";

                if (drpCustomerCode.SelectedIndex > 0)
                {
                    BindCustomerFormView(Session["BranchCode"].ToString(), drpCustomerCode.SelectedValue);
                    BtnSubmit.Attributes.Add("OnClick", "return CheckValidDateFields();");
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
                Response.Redirect("Customer.aspx", false);
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
                if (CustomerFormView.CurrentMode == FormViewMode.Insert)
                {
                    string customerCode = customer.AddNewCustomer(GetCustomerDetails(false));
                    //LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(), drpCustomerCode, "Code", "Name", true, "");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Information Inserted Succesfully');", true);
                    var txtName = (TextBox)CustomerFormView.FindControl("txtName");
                    CustomerFormView.ChangeMode(FormViewMode.Edit);
                    BindCustomerFormView(Session["BranchCode"].ToString(), customerCode);
                    PnlCustomer.Enabled = false;
                    BtnSubmit.Enabled = false;
                    btnReset.Enabled = true;
                    //btnReport.Enabled = false;
                    btnSearch.Visible = false;
                }
                else
                {
                    if (CustomerFormView.CurrentMode == FormViewMode.Edit)
                    {
                        customer.UpdateCustomer(GetCustomerDetails(false));
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Updated Succesfully');", true);
                        CustomerFormView.ChangeMode(FormViewMode.Insert);
                        btnSearch.Visible = true;
                        drpCustomerCode.Enabled = false;
                        drpCustomerCode.SelectedIndex = 0;
                    }
                }


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
         }

        private CustomerFields GetCustomerDetails(bool Reset)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CustomerFields cust = new CustomerFields();

            try
            {
                var txtCode = (TextBox)CustomerFormView.FindControl("txtCode");
                var txtName = (TextBox)CustomerFormView.FindControl("txtName");
                var txtSalesmanCode = (TextBox)CustomerFormView.FindControl("txtSalesmanCode");
                var ddlBranch = (DropDownList)CustomerFormView.FindControl("ddlBranch");
                var ddlSalesman = (DropDownList)CustomerFormView.FindControl("ddlSalesman");
                var ddlCustType = (DropDownList)CustomerFormView.FindControl("ddlCustType");
                var txtAddOne = (TextBox)CustomerFormView.FindControl("txtAddOne");
                var txtAddTwo = (TextBox)CustomerFormView.FindControl("txtAddTwo");
                var txtAdd3 = (TextBox)CustomerFormView.FindControl("txtAdd3");
                var txtAdd4 = (TextBox)CustomerFormView.FindControl("txtAdd4");
                var txtLocation = (TextBox)CustomerFormView.FindControl("txtLocation");
                var txtPinCode = (TextBox)CustomerFormView.FindControl("txtPinCode");
                var ddlTown = (DropDownList)CustomerFormView.FindControl("ddlTown");
                var txtPhone = (TextBox)CustomerFormView.FindControl("txtPhone");
                var ddlLocalOrOutStation = (DropDownList)CustomerFormView.FindControl("ddlLocalOrOutStation");                
                var txtTelex = (TextBox)CustomerFormView.FindControl("txtTelex");
                var txtEmail = (TextBox)CustomerFormView.FindControl("txtEmail");
                var txtContactPerson = (TextBox)CustomerFormView.FindControl("txtContactPerson");
                var txtContactDesignation = (TextBox)CustomerFormView.FindControl("txtContactDesignation");
                var txtAlphacode = (TextBox)CustomerFormView.FindControl("txtAlphacode");
                var txtDateOfCreation = (TextBox)CustomerFormView.FindControl("txtDateOfCreation");
                var txtPreferredCarrier = (TextBox)CustomerFormView.FindControl("txtPreferredCarrier");
                var txtDestination = (TextBox)CustomerFormView.FindControl("txtDestination");
                var txtGSTIN = (TextBox)CustomerFormView.FindControl("txtGSTIN");
                var txtCentralST = (TextBox)CustomerFormView.FindControl("txtCentralST");
                var txtCreditLimit = (TextBox)CustomerFormView.FindControl("txtCreditLimit");
                var ddlCrLimitIndicator = (DropDownList)CustomerFormView.FindControl("ddlCrLimitIndicator");
                var txtOutstandingamount = (TextBox)CustomerFormView.FindControl("txtOutstandingamount");
                var txtChartAc = (TextBox)CustomerFormView.FindControl("txtChartAc");
                var ddlStatus = (DropDownList)CustomerFormView.FindControl("ddlStatus");
                var txtBankName = (TextBox)CustomerFormView.FindControl("txtBankName");
                var txtBranchName = (TextBox)CustomerFormView.FindControl("txtBranchName");
                var txtIFSCcode = (TextBox)CustomerFormView.FindControl("txtIFSCcode");
                var txtTinNumber = (TextBox)CustomerFormView.FindControl("txtTinNumber");
                var txtCollectiondays = (TextBox)CustomerFormView.FindControl("txtCollectiondays");
                var ddlCustomerState = (DropDownList)CustomerFormView.FindControl("ddlCustomerState");
                var hdnStateCode = (HiddenField)CustomerFormView.FindControl("hdnStateCode");
                var ddlValidityIndicator = (DropDownList)CustomerFormView.FindControl("ddlValidityIndicator");
                var hdnCreditLimit = (HiddenField)CustomerFormView.FindControl("hdnCreditLimit");
                var txtCrlimitDueDate = (TextBox)CustomerFormView.FindControl("txtCrlimitDueDate");
                var ddlCustClassifiation = (DropDownList)CustomerFormView.FindControl("ddlCustClassifiation");
                var ddlCustSegment = (DropDownList)CustomerFormView.FindControl("ddlCustSegment");

                if (Reset == false)
                {
                    cust.Customer_Code = txtCode.Text;
                    cust.Customer_Name = txtName.Text;
                    cust.Chart_of_Account_Code = txtChartAc.Text;
                    cust.Party_Type_Code = ddlCustType.SelectedValue;
                    cust.Salesman = ddlSalesman.SelectedItem.ToString();
                    cust.Salesman_Code = ddlSalesman.SelectedValue;
                    cust.Customer_Classification = ddlCustClassifiation.SelectedValue;
                    cust.Customer_Segment = ddlCustSegment.SelectedValue;
                    cust.Pincode = txtPinCode.Text;
                    cust.Branch_Code = ddlBranch.SelectedValue;
                    cust.Town_Code = ddlTown.Text;
                    cust.Alpha_Code = txtAlphacode.Text;
                    cust.Phone = txtPhone.Text;
                    cust.Local_Outstation = ddlLocalOrOutStation.SelectedValue;
                    cust.Email = txtEmail.Text;
                    cust.Telex = txtTelex.Text;
                    cust.Contact_Person = txtContactPerson.Text;
                    cust.Contact_Person_Mobile = txtContactDesignation.Text;
                    cust.Carrier = txtPreferredCarrier.Text;
                    cust.Destination = txtDestination.Text;
                    cust.Local_Sales_Tax_Number = txtGSTIN.Text;
                    cust.Central_Sales_Tax_Number = txtCentralST.Text;
                    cust.Outstanding_Amount = string.IsNullOrEmpty(txtOutstandingamount.Text) ? 0.00 : Convert.ToDouble(txtOutstandingamount.Text);
                    cust.Credit_Limit = string.IsNullOrEmpty(txtCreditLimit.Text) ? 0.00 : Convert.ToDouble(txtCreditLimit.Text);
                    cust.Cash_Credit_Limit_Indicator = ddlCrLimitIndicator.SelectedValue;
                    cust.Collection_Days = txtCollectiondays.Text;
                    cust.customer_bank_name = txtBankName.Text;
                    cust.customer_bank_branch = txtBranchName.Text;
                    cust.IFSC_Code = txtIFSCcode.Text;
                    cust.status = ddlStatus.SelectedValue;
                    cust.Branch_Name = ddlBranch.SelectedItem.ToString();
                    cust.Town_Name = ddlTown.SelectedItem.ToString();
                    cust.Party_Type_Description = ddlCustType.SelectedItem.ToString();
                    cust.Address1 = txtAddOne.Text;
                    cust.Address2 = txtAddTwo.Text;
                    cust.Address3 = txtAdd3.Text;
                    cust.Address4 = txtAdd4.Text;
                    cust.location = txtLocation.Text;
                    cust.TinNumber = txtTinNumber.Text;
                    cust.BranchState_Code = hdnStateCode.Value;
                    cust.CustomerState_Code = ddlCustomerState.SelectedValue;
                    cust.Validity_Indicator = ddlValidityIndicator.SelectedValue;
                    cust.Old_Credit_Limit = string.IsNullOrEmpty(hdnCreditLimit.Value) ? 0.00 : Convert.ToDouble(hdnCreditLimit.Value);
                    cust.Credit_Limit_Due_Date = txtCrlimitDueDate.Text;
                    cust.Date_Of_Creation = txtDateOfCreation.Text;
                }
                else
                {
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtChartAc.Text = "";
                    txtPinCode.Text = "";
                    txtAlphacode.Text = "";
                    ddlCustClassifiation.SelectedIndex = 0;
                    ddlCustSegment.SelectedIndex = 0;
                    txtPhone.Text = "";
                    ddlLocalOrOutStation.SelectedIndex = 0;
                    txtEmail.Text = "";
                    txtTelex.Text = "";
                    txtContactPerson.Text = "";
                    txtContactDesignation.Text = "";
                    txtPreferredCarrier.Text = "";
                    txtDestination.Text = "";
                    txtGSTIN.Text = "";
                    txtCentralST.Text = "";
                    txtOutstandingamount.Text = "";
                    txtCreditLimit.Text = "";
                    ddlCrLimitIndicator.SelectedIndex = 0;
                    txtCollectiondays.Text = "";
                    txtBankName.Text = "";
                    txtBranchName.Text = "";
                    txtIFSCcode.Text = "";
                    ddlStatus.SelectedIndex = 0;
                    ddlBranch.SelectedIndex = 0;
                    ddlTown.SelectedIndex = 0;
                    ddlSalesman.SelectedIndex = 0;
                    ddlCustType.SelectedIndex = 0;
                    txtAddOne.Text = "";
                    txtAddTwo.Text = "";
                    txtAdd3.Text = "";
                    txtAdd4.Text = "";
                    txtLocation.Text = "";
                    txtTinNumber.Text = "";
                    txtSalesmanCode.Text = "";
                    drpCustomerCode.SelectedIndex = 0;
                    ddlCustomerState.SelectedIndex = 0;
                    cust.Date_Of_Creation = "";
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
                //string strCustomerCode = drpCustomerCode.SelectedValue.ToString();
                Server.Execute("CustomerReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
