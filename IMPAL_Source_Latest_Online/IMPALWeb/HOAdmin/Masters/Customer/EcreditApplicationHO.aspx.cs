using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Globalization;
using System.Web.Services;

namespace IMPALWeb
{
    public partial class ECreditApplicationHO : System.Web.UI.Page
    {
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        StateMasters States = new StateMasters();        
        Branches Branch = new Branches();
        Towns Towns = new Towns();
        CustomerDetails customer = new CustomerDetails();
        List<CustomerFields> custDetails;
        ECreditFormCustTransactions ecrdtTrans = new ECreditFormCustTransactions();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    pnlEcreditForm.Visible = true;
                    txtApplicationDate.Enabled = false;

                    div1.Attributes.Add("style", "display:none");
                    div2.Attributes.Add("style", "display:none");
                    div3.Attributes.Add("style", "display:none");
                    div4.Attributes.Add("style", "display:none");
                    div5.Attributes.Add("style", "display:none");
                    div6.Attributes.Add("style", "display:none");

                    LoadBranchDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplication(Session["UserID"].ToString()), ddlBranch, "BranchCode", "BranchName", true, "");
                    btnSubmit.Attributes.Add("OnClick", "return fnSubmitFormHO('2')");
                    btnReject.Attributes.Add("OnClick", "return fnEcreditFormReject();");

                    ddlApplicationNo.Items.Clear();
                    ddlApplicationNo.DataSource = (object)ecrdtTrans.GetEcreditApplicationNos("", Session["UserID"].ToString());
                    ddlApplicationNo.DataTextField = "ItemDesc";
                    ddlApplicationNo.DataValueField = "ItemCode";
                    ddlApplicationNo.DataBind();

                    btnSubmit.Visible = false;
                    btnReject.Visible = false;
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

        private void LoadBranchDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
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
                    DDlDropDown.Items.Insert(0, new ListItem("--ALL--", ""));
                }
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
                if (ddlBranch.SelectedIndex>0)
                {
                    ddlApplicationNo.Items.Clear();
                    ddlApplicationNo.DataSource = (object)ecrdtTrans.GetEcreditApplicationNos(ddlBranch.SelectedValue, Session["UserID"].ToString());
                    ddlApplicationNo.DataTextField = "ItemDesc";
                    ddlApplicationNo.DataValueField = "ItemCode";
                    ddlApplicationNo.DataBind();
                }
                else
                {
                    ddlApplicationNo.Items.Clear();
                    ddlApplicationNo_SelectedIndexChanged(ddlApplicationNo, EventArgs.Empty);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        protected void ddlApplicationNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlApplicationNo.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = false;
                    string BranchCode = ddlBranch.SelectedIndex == 0 ? ddlApplicationNo.SelectedValue.Substring(14, 3) : ddlBranch.SelectedValue;
                    
                    List<ECreditApplicationCust> lstEcreditEntity = new List<ECreditApplicationCust>();
                    lstEcreditEntity = ecrdtTrans.GetEcreditApplicationDetails(BranchCode, ddlApplicationNo.SelectedValue.ToString());

                    if (lstEcreditEntity.Count > 0)
                    {
                        //ddlApplicationNo.Enabled = false;
                        ddlCustomerCode.Enabled = false;
                        LoadDropDownLists<CustomerDtls>(customer.GetDistrictMaster(BranchCode), ddlDealerDistrict, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlDealerState, "StateCode", "StateName", true, "");
                        LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(BranchCode), ddlDealerTown, "Towncode", "TownName", true, "");
                        LoadDropDownLists<CustomerFields>(customer.GetSalesmanEcredit(BranchCode), ddlAssignedSMRR, "Salesman_Code", "Salesman", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlParentBranch, "BranchCode", "BranchName", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(BranchCode), ddlParentCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlMigrateBranch, "BranchCode", "BranchName", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(BranchCode), ddlMigrateCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(BranchCode), ddlCustomerCode, "Code", "Name", true, "");
                        //LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerCreditIndicator"), ddlCrLimitIndicator, "DisplayValue", "DisplayText", true, "");

                        txtApplicationDate.Text = lstEcreditEntity[0].Form_Date;
                        ddlCustomerCode.SelectedValue = lstEcreditEntity[0].Customer_Code;
                        txtCustCode.Text = ddlCustomerCode.SelectedValue;
                        txtCustName.Text = lstEcreditEntity[0].Customer_Name;
                        hdnIndicator.Value = lstEcreditEntity[0].Indicator;

                        if (hdnIndicator.Value == "N")
                        {
                            DealerIndicator.Text = "New Dealer";
                            DealerIndicator.Attributes.Add("style", "color:green; font-weight: bold; height: 25px; width: 200px; font-size: 18px; text-align: center;");
                        }
                        else
                        {
                            DealerIndicator.Text = "Existing Dealer";
                            DealerIndicator.Attributes.Add("style", "color:red; font-weight: bold; height: 25px; width: 200px; font-size: 18px; text-align: center;");
                        }

                        txtAdd1.Text = lstEcreditEntity[0].Customer_Address1;
                        txtAdd2.Text = lstEcreditEntity[0].Customer_Address2;
                        txtAdd3.Text = lstEcreditEntity[0].Customer_Address3;
                        txtAdd4.Text = lstEcreditEntity[0].Customer_Address4;
                        txtPropName.Text = lstEcreditEntity[0].Proprietor_Name;
                        txtPropMobile.Text = lstEcreditEntity[0].Proprietor_Mobile;

                        DataTable dt = ecrdtTrans.GetECrdeitApplnSisDetailsGroup(BranchCode, ddlCustomerCode.SelectedValue);

                        if (lstEcreditEntity[0].Group_Company_Codes.IndexOf(",") > 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                grvGroupDealers.DataSource = dt;
                                grvGroupDealers.DataBind();

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DropDownList ddlSisCustBranch = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCustBranch");
                                    DropDownList ddlSisCust = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCust");
                                    TextBox txtSisCustCode = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCode");
                                    TextBox txtSisCustCrLimit = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCrLimit");

                                    LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlSisCustBranch, "BranchCode", "BranchName", true, "");
                                    ddlSisCustBranch.SelectedValue = dt.Rows[i]["Dealer_Branch"].ToString();

                                    LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlSisCustBranch.SelectedValue), ddlSisCust, "Code", "Name", true, "");
                                    ddlSisCust.SelectedValue = dt.Rows[i]["Dealer_Name"].ToString();

                                    txtSisCustCode.Text = dt.Rows[i]["Dealer_Code"].ToString();
                                    txtSisCustCrLimit.Text = IndianMoneyConversion(dt.Rows[i]["Credit_Limit"].ToString());
                                }
                            }
                        }

                        ddlParentBranch.SelectedValue = lstEcreditEntity[0].Migration_From_Branch_Code;
                        ddlParentCustomer.SelectedValue = lstEcreditEntity[0].Migration_From_Customer_Code;
                        ddlMigrateBranch.SelectedValue = lstEcreditEntity[0].Branch_Code;
                        ddlMigrateCustomer.SelectedValue = lstEcreditEntity[0].Customer_Code;
                        txtYearofEstbl.Text = lstEcreditEntity[0].Year_Of_Establishment;
                        ddlDealerState.SelectedValue = lstEcreditEntity[0].State_Code;
                        ddlDealerDistrict.SelectedValue = lstEcreditEntity[0].District_Code;
                        ddlDealerTown.SelectedValue = lstEcreditEntity[0].Town_Code;
                        ddlTownClassification.SelectedValue = lstEcreditEntity[0].Town_Classification;
                        txtContactPersonName.Text = lstEcreditEntity[0].Contact_Person;
                        txtContactPersonMobNo.Text = lstEcreditEntity[0].Contact_Person_Mobile;
                        txtLocation.Text = lstEcreditEntity[0].Location;
                        txtEmailid.Text = lstEcreditEntity[0].Email;
                        txtPinCode.Text = lstEcreditEntity[0].PinCode;
                        txtGSTIN.Text = lstEcreditEntity[0].GSTIN_No;
                        ddlFirmType.SelectedValue = lstEcreditEntity[0].Type_of_Company;
                        ddlRegnType.SelectedValue = lstEcreditEntity[0].Type_of_Registration;
                        txtAnnlTunOver.Text = IndianMoneyConversion(lstEcreditEntity[0].Turnover_Value);
                        txtLineSaleTurnOver.Text = IndianMoneyConversion(lstEcreditEntity[0].Impal_Turnover_Value);
                        ddlGSTINlocation.SelectedValue = lstEcreditEntity[0].GSTIN_Location;
                        txtOverAllStockValue.Text = IndianMoneyConversion(lstEcreditEntity[0].Stock_Value);
                        ddlDealerServicedBy.SelectedValue = lstEcreditEntity[0].Representative_Type;
                        txtDistance.Text = lstEcreditEntity[0].Distance_From_Branch;
                        txtDistanceSM.Text = lstEcreditEntity[0].Distance_from_RR;
                        ddlDayTravelOS.SelectedValue = lstEcreditEntity[0].Travel_Classificaion;
                        ddlAssignedSMRR.SelectedValue = lstEcreditEntity[0].Salesman_code;
                        txtDealerTarget.Text = TwoDecimalConversion(lstEcreditEntity[0].Dealer_Target);
                        ddlVisitPlan.SelectedValue = lstEcreditEntity[0].Period_of_Visit;
                        ddlDealerClassification.SelectedValue = lstEcreditEntity[0].Classification;
                        ddlDealerSegment.SelectedValue = lstEcreditEntity[0].Segment;
                        ddlDealersMultipleTown.SelectedValue = lstEcreditEntity[0].MultipleTown;
                        ddlDealerDealingGroups.SelectedValue = lstEcreditEntity[0].Dealing_with_Other_Group_Co;
                        txtTransporterName.Text = lstEcreditEntity[0].Transporter_Name;
                        txtCashPurchase.Text = IndianMoneyConversion(lstEcreditEntity[0].Cash_Purchase_Value);
                        txtOsAmt.Text = IndianMoneyConversion(lstEcreditEntity[0].Outstanding_Amount);
                        txtExistingCrLimit.Text = IndianMoneyConversion(lstEcreditEntity[0].Existing_Credit_Limit);
                        txtEnhCrLimtReq.Text = IndianMoneyConversion(lstEcreditEntity[0].Proposed_Credit_Limit);
                        ddlCrLimitIndicator.SelectedValue = lstEcreditEntity[0].Cash_Credit_Limit_Indicator;
                        ddlValidityIndicator.SelectedValue = lstEcreditEntity[0].Cr_Limit_Validity_Ind;
                        txtCrlimitDueDate.Text = lstEcreditEntity[0].Cr_Limit_Due_Date;
                        ddlFreightIndicator.SelectedValue = lstEcreditEntity[0].FreightIndicator;
                        txtFirstCrLimitReq.Text = IndianMoneyConversion(lstEcreditEntity[0].First_Time_Credit_Limit_Request);
                        txtBankAccountNo.Text = lstEcreditEntity[0].Bank_AccountNo;
                        txtBankName.Text = lstEcreditEntity[0].Bank_Name;
                        txtBankBranch.Text = lstEcreditEntity[0].Bank_Branch;
                        txtIFSCcode.Text = lstEcreditEntity[0].IFSC_Code;
                        txtAccountName.Text = lstEcreditEntity[0].Name_of_Account_Holder;
                        txtCarNoExpDate.Text = lstEcreditEntity[0].Debit_Credit_Card_Number;
                        ddlPaymentMode.SelectedValue = lstEcreditEntity[0].Payment_Mode;
                        txtCrLimitApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); //lstEcreditEntity[0].HO_Approval_Date;
                        txtApprovedCrLimit.Text = IndianMoneyConversion(lstEcreditEntity[0].Proposed_Credit_Limit);
                        txtCustomerCode.Text = lstEcreditEntity[0].Customer_Code;
                        ddlReasonForClosure.SelectedValue = lstEcreditEntity[0].Reason_For_closure;
                        txtWriteOffAmout.Text = IndianMoneyConversion(lstEcreditEntity[0].Written_Off_Amount);
                        hdnAccPeriodCode.Value = lstEcreditEntity[0].Accounting_Period_Code;

                        txtApprovedCrLimit.Attributes.Add("OnChange", "ApprovedCrLimitValidation();");
                        txtOsAmt.Enabled = false;
                        txtExistingCrLimit.Enabled = false;
                        txtEnhCrLimtReq.Enabled = false;
                        ddlCrLimitIndicator.Enabled = false;

                        if (ddlReasonForClosure.SelectedIndex <= 0)
                        {
                            div3.Attributes.Add("style", "display:inline");
                            div5.Attributes.Add("style", "display:inline");
                            link5.Attributes.Remove("onclick");
                            link5.Attributes.Add("onclick", "return false;");

                            Panel1.Enabled = false;
                            Panel2.Enabled = false;
                            txtEnhCrLimtReq.Enabled = false;
                            ddlValidityIndicator.Enabled = true;
                            txtCrlimitDueDate.Enabled = true;

                            if (ddlValidityIndicator.SelectedValue == "T")
                                txtCrlimitDueDate.Enabled = true;
                            else
                                txtCrlimitDueDate.Enabled = false;

                            //Panel3.Enabled = false;
                            Panel4.Enabled = false;
                            Panel6.Enabled = false;
                        }
                        else
                        {
                            div6.Attributes.Add("style", "display:inline");
                            link6.Attributes.Remove("onclick");
                            link6.Attributes.Add("onclick", "return false;");

                            Panel1.Enabled = false;
                            Panel2.Enabled = false;
                            txtEnhCrLimtReq.Enabled = false;
                            ddlValidityIndicator.Enabled = true;

                            if (ddlValidityIndicator.SelectedValue == "T")
                                txtCrlimitDueDate.Enabled = true;
                            else
                                txtCrlimitDueDate.Enabled = false;

                            //Panel3.Enabled = false;
                            Panel4.Enabled = false;
                            Panel5.Enabled = false;
                        }

                        List<EcreditPaymentPattern> lstPymtPatternDtls = new List<EcreditPaymentPattern>();
                        lstPymtPatternDtls = ecrdtTrans.GetPaymentPatternDetails(BranchCode, ddlCustomerCode.SelectedValue);
                        grvPaymentPattern.DataSource = lstPymtPatternDtls;
                        grvPaymentPattern.DataBind();

                        for (int i = 1; i <= lstPymtPatternDtls.Count; i++)
                        {
                            Label lblOsMonth = (Label)grvPaymentPattern.Rows[i - 1].Cells[0].FindControl("lblOsMonth");
                            Label lblOsCrLimit = (Label)grvPaymentPattern.Rows[i - 1].Cells[1].FindControl("lblOsCrLimit");
                            Label lblOsTotal = (Label)grvPaymentPattern.Rows[i - 1].Cells[2].FindControl("lblOsTotal");
                            Label lblOsCurBal = (Label)grvPaymentPattern.Rows[i - 1].Cells[3].FindControl("lblOsCurBal");
                            Label lblOsAbove30 = (Label)grvPaymentPattern.Rows[i - 1].Cells[4].FindControl("lblOsAbove30");
                            Label lblOsAbove60 = (Label)grvPaymentPattern.Rows[i - 1].Cells[5].FindControl("lblOsAbove60");
                            Label lblOsAbove90 = (Label)grvPaymentPattern.Rows[i - 1].Cells[6].FindControl("lblOsAbove90");
                            Label lblOsAbove180 = (Label)grvPaymentPattern.Rows[i - 1].Cells[7].FindControl("lblOsAbove180");
                            Label lblOsCollPer = (Label)grvPaymentPattern.Rows[i - 1].Cells[8].FindControl("lblOsCollPer");

                            lblOsMonth.Text = lstPymtPatternDtls[i - 1].Month.ToString();
                            lblOsCrLimit.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Credit_Limit.ToString());
                            lblOsTotal.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Total.ToString());
                            lblOsCurBal.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].CurBal.ToString());
                            lblOsAbove30.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Above30.ToString());
                            lblOsAbove60.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Above60.ToString());
                            lblOsAbove90.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Above90.ToString());
                            lblOsAbove180.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].Above180.ToString());
                            lblOsCollPer.Text = IndianMoneyConversion(lstPymtPatternDtls[i - 1].CollPercentage.ToString());

                            if (i == lstPymtPatternDtls.Count)
                                lblOsChequeReturns.Text = lstPymtPatternDtls[0].NoOfChqReturns.ToString();
                        }

                        btnSubmit.Visible = true;
                        btnReject.Visible = true;
                    }
                    else
                    {
                        Server.ClearError();
                        Response.Redirect("ECreditApplicationHO.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("ECreditApplicationHO.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvGroupDealers_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //DropDownList ddlSisCust = (DropDownList)e.Row.Cells[0].FindControl("ddlSisCust");
                        //TextBox txtSisCustCode = (TextBox)e.Row.Cells[0].FindControl("txtSisCustCode");
                        //TextBox txtSisCustCrLimit = (TextBox)e.Row.Cells[0].FindControl("txtSisCustCrLimit");

                        //LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), ddlSisCust, "Code", "Name", true, "");
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvPaymentPattern_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SalesInvoiceEntry), exp);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                div1.Attributes.Add("style", "display:inline");
                div2.Attributes.Add("style", "display:inline");
                div3.Attributes.Add("style", "display:inline");
                div4.Attributes.Add("style", "display:inline");
                div5.Attributes.Add("style", "display:inline");
                div6.Attributes.Add("style", "display:inline");

                pnlEcreditForm.Enabled = false;							   
                btnSubmit.Visible = false;
                btnPrint1.Visible = false;
				pnlEcreditSel.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "javascript:fnprint()", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
            btnReject.Visible = false;

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ECreditApplicationCust ecreditAppln = new ECreditApplicationCust();
                ecreditAppln.Branch_Code = ddlBranch.SelectedIndex == 0 ? ddlApplicationNo.SelectedValue.Substring(14, 3) : ddlBranch.SelectedValue;
                ecreditAppln.Form_Number = ddlApplicationNo.SelectedValue;
                ecreditAppln.Customer_Code = ddlCustomerCode.SelectedValue;
                ecreditAppln.Customer_Name = txtCustName.Text;
                ecreditAppln.Location = txtLocation.Text;
                ecreditAppln.Existing_Credit_Limit = txtExistingCrLimit.Text;
                ecreditAppln.Proposed_Credit_Limit = txtEnhCrLimtReq.Text;
                ecreditAppln.Cr_Limit_Validity_Ind = ddlValidityIndicator.SelectedValue;
                ecreditAppln.Cr_Limit_Due_Date = txtCrlimitDueDate.Text;
                ecreditAppln.Approved_Credit_Limit = txtApprovedCrLimit.Text;
                ecreditAppln.Reason_For_closure = ddlReasonForClosure.SelectedValue;
                ecreditAppln.Written_Off_Amount = txtWriteOffAmout.Text;
                ecreditAppln.Userid = Session["UserID"].ToString();

                DataSet ds = ecrdtTrans.UpdECrdeitApplicationForm(ecreditAppln);

                Panel1.Enabled = false;
                Panel2.Enabled = false;
                Panel3.Enabled = false;
                Panel4.Enabled = false;
                Panel5.Enabled = false;
                Panel6.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlApplicationNo.Enabled = false;

                if (ecreditAppln.ErrorCode == "0")
                {
                    string Emailid = ds.Tables[0].Rows[0]["Email"].ToString();
                    string cc = ds.Tables[0].Rows[0]["cc"].ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('E-Credit Application Has Been Approved Succesfully.');", true);

                    SendMails(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Branch_Name"].ToString(), hdnIndicator.Value, ds.Tables[0].Rows[0]["Customer_Details"].ToString(), ddlApplicationNo.SelectedValue, Emailid, cc, ds.Tables[0].Rows[0]["Approver_Name"].ToString(), ds.Tables[0].Rows[0]["Approver_Designation"].ToString(), IndianMoneyConversion(txtExistingCrLimit.Text), IndianMoneyConversion(txtApprovedCrLimit.Text), "");

                    PageReset();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Data Error');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }        

        protected void SendMails(string Branch_Code, string Branch_Name, string Indicator, string CustomerDetails, string FormNumber, string Emailid, string cc, string ApproverName, string ApproverDesignation, string OldLimit, string NewLimit, string Remarks)
        {
            MailAddress from = new MailAddress("ecreditform@impal.net", "E-Credit Application Form");
            MailAddress to = new MailAddress(Emailid);
            string BodyMessage = "";

            if (Indicator == "R")
                BodyMessage = "<font face='Arial'>Dear " + Branch_Name + " Branch,<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been Rejected by <b>Mr." + ApproverName + " - " + ApproverDesignation + "</b> with the Remarks: <b>" + Remarks + "</b> regarding Enhancement of Credit Limit for the Customer <b>" + CustomerDetails + "</b> from Old Limit of <b>Rs." + OldLimit + "</b> to New Limit of <b>Rs." + NewLimit + "</b>.";
            else if (Indicator == "E")
                BodyMessage = "<font face='Arial'>Dear " + Branch_Name + " Branch,<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been Approved by <b>Mr." + ApproverName + " - " + ApproverDesignation + "</b> regarding Enhancement of Credit Limit for the Existing Customer <b>" + CustomerDetails + "</b> from Old Limit of <b>Rs." + OldLimit + "</b> to New Limit of <b>Rs." + NewLimit + "</b>.";
            else
                BodyMessage = "<font face='Arial'>Dear " + Branch_Name + " Branch,<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been Approved by <b>Mr." + ApproverName + " - " + ApproverDesignation + "</b> regarding Sanction of Credit Limit of <b>Rs." + NewLimit + "</b> for the New Customer <b>" + CustomerDetails + "</b>.";

            BodyMessage = BodyMessage + "<br><br>Please Check and Proceed accordingly.<br><br>This is an auto system generated Mail.<br><br>Regards,<br>IMPAL IT Team</font>";

            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to))
            {
                if (Indicator == "R")
                    mm.Subject = "E-CREDIT FORM # " + FormNumber + " - REJECTION " + DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
                else if (Indicator == "E")
                    mm.Subject = "E-CREDIT FORM # " + FormNumber + " - EXISTING DEALER " + DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
                else
                    mm.Subject = "E-CREDIT FORM # " + FormNumber + " - NEW DEALER " + DateTime.Now.ToString("dd/MM/yyyy h:mm tt");

                mm.Body = BodyMessage;
                mm.IsBodyHtml = true;

                if (cc != "")
                {
                    string[] CCId = cc.Split(',');
                    foreach (string CCEmail in CCId)
                    {
                        mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                    }
                }

                mm.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["MailApiHost"].ToString();
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["MailApiUsername"].ToString(), ConfigurationManager.AppSettings["MailApiPassword"].ToString());
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailApiPort"].ToString());
                smtp.EnableSsl = false;
                smtp.Send(mm);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                Panel5.Enabled = false;

                ECreditApplicationCust ecreditAppln = new ECreditApplicationCust();
                ecreditAppln.Branch_Code = ddlBranch.SelectedIndex == 0 ? ddlApplicationNo.SelectedValue.Substring(14, 3) : ddlBranch.SelectedValue;
                ecreditAppln.Form_Number = ddlApplicationNo.SelectedValue;
                ecreditAppln.Customer_Code = ddlCustomerCode.SelectedValue;
                ecreditAppln.Customer_Name = txtCustName.Text;
                ecreditAppln.Location = txtLocation.Text;
                ecreditAppln.Existing_Credit_Limit = txtExistingCrLimit.Text;
                ecreditAppln.Cr_Limit_Validity_Ind = ddlValidityIndicator.SelectedValue;
                ecreditAppln.Cr_Limit_Due_Date = txtCrlimitDueDate.Text;
                ecreditAppln.Approved_Credit_Limit = txtApprovedCrLimit.Text;
                ecreditAppln.Userid = Session["UserID"].ToString();
                ecreditAppln.Remarks = Session["EcreditFormRemarks"].ToString();

                DataSet ds = ecrdtTrans.UpdECrdeitApplicationFormReject(ecreditAppln);

                string Emailid = ds.Tables[0].Rows[0]["Email"].ToString();
                string cc = ""; // ds.Tables[0].Rows[0]["cc"].ToString();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('E Credit Form has been Rejected Sucessfully');", true);

                SendMails(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Branch_Name"].ToString(), "R", ds.Tables[0].Rows[0]["Customer_Details"].ToString(), ddlApplicationNo.SelectedValue, Emailid, cc, ds.Tables[0].Rows[0]["Approver_Name"].ToString(), ds.Tables[0].Rows[0]["Approver_Designation"].ToString(), IndianMoneyConversion(txtExistingCrLimit.Text), IndianMoneyConversion(txtApprovedCrLimit.Text), Session["EcreditFormRemarks"].ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        [WebMethod]
        public static void SetSessionRemarks(string Remarks)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Page objp = new Page();
                objp.Session["EcreditFormRemarks"] = Remarks;
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

        private string IndianMoneyConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0";
            else
            {
                decimal parsed = decimal.Parse(strValue, CultureInfo.InvariantCulture);
                CultureInfo format = new CultureInfo("hi-IN");
                return string.Format(format, "{0:#,#}", parsed);
            }
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("ECreditApplicationHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void PageReset()
        {
            ddlCustomerCode.Items.Clear();
            ddlDealerDistrict.Items.Clear();
            ddlDealerState.Items.Clear();
            ddlDealerTown.Items.Clear();
            ddlAssignedSMRR.Items.Clear();
            ddlParentBranch.Items.Clear();
            ddlParentCustomer.Items.Clear();
            ddlMigrateBranch.Items.Clear();
            ddlMigrateCustomer.Items.Clear();
            ddlTownClassification.Items.Clear();

            txtApplicationDate.Text = "";
            txtCustCode.Text = "";
            txtCustName.Text = "";
            hdnIndicator.Value = "";
            DealerIndicator.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtAdd3.Text = "";
            txtAdd4.Text = "";
            txtPropName.Text = "";
            txtPropMobile.Text = "";

            grvGroupDealers.DataSource = null;
            grvGroupDealers.DataBind();

            txtYearofEstbl.Text = "";            
            txtContactPersonName.Text = "";
            txtContactPersonMobNo.Text = "";
            txtLocation.Text = "";
            txtEmailid.Text = "";
            txtPinCode.Text = "";
            txtGSTIN.Text = "";
            ddlFirmType.SelectedIndex = 0;
            ddlRegnType.SelectedIndex = 0;
            txtAnnlTunOver.Text = "";
            txtLineSaleTurnOver.Text = "";
            ddlGSTINlocation.SelectedIndex = 0;
            txtOverAllStockValue.Text = "";
            ddlDealerServicedBy.SelectedIndex = 0;
            txtDistance.Text = "";
            txtDistanceSM.Text = "";
            ddlDayTravelOS.SelectedIndex = 0;
            txtDealerTarget.Text = "";
            ddlVisitPlan.SelectedIndex = 0;
            ddlDealerClassification.SelectedIndex = 0;
            ddlDealerSegment.SelectedIndex = 0;
            ddlDealersMultipleTown.SelectedIndex = 0;
            ddlDealerDealingGroups.SelectedIndex = 0;
            txtTransporterName.Text = "";
            txtCashPurchase.Text = "";
            txtExistingCrLimit.Text = "";
            txtEnhCrLimtReq.Text = "";
            ddlCrLimitIndicator.SelectedIndex = 0;
            ddlValidityIndicator.SelectedIndex = 0;
            txtCrlimitDueDate.Text = "";
            ddlFreightIndicator.SelectedIndex = 0;
            txtFirstCrLimitReq.Text = "";
            txtBankAccountNo.Text = "";
            txtBankName.Text = "";
            txtBankBranch.Text = "";
            txtIFSCcode.Text = "";
            txtAccountName.Text = "";
            txtCarNoExpDate.Text = "";
            ddlPaymentMode.SelectedIndex = 0;
            txtCrLimitApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtApprovedCrLimit.Text = "";
            txtCustomerCode.Text = "";
            ddlReasonForClosure.SelectedIndex = 0;
            txtWriteOffAmout.Text = "";
            hdnAccPeriodCode.Value = "";

            ddlApplicationNo.Items.Clear();
            ddlApplicationNo.DataSource = (object)ecrdtTrans.GetEcreditApplicationNos("", Session["UserID"].ToString());
            ddlApplicationNo.DataTextField = "ItemDesc";
            ddlApplicationNo.DataValueField = "ItemCode";
            ddlApplicationNo.DataBind();
            ddlValidityIndicator.Enabled = true;
            txtApprovedCrLimit.Enabled = true;

            div1.Attributes.Add("style", "display:none");
            div2.Attributes.Add("style", "display:none");
            div3.Attributes.Add("style", "display:none");
            div4.Attributes.Add("style", "display:none");
            div5.Attributes.Add("style", "display:none");
            div6.Attributes.Add("style", "display:none");
        }
    }
}