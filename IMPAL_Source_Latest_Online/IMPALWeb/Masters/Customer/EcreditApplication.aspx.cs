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
    public partial class ECreditApplication : System.Web.UI.Page
    {
        ImpalLibrary objImpalLibrary = new ImpalLibrary();
        StateMasters States = new StateMasters();
        Branches Branch = new Branches();
        Towns Towns = new Towns();
        CustomerDetails customer = new CustomerDetails();
        ECreditFormCustTransactions ecrdtTrans = new ECreditFormCustTransactions();
        List<ECreditApplicationCust> lstEcreditEntity = new List<ECreditApplicationCust>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    pnlEcreditForm.Visible = false;
                    txtApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtApplicationDate.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlMigrateCustomer.Enabled = false;

                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = false;
                    divtrans.Visible = false;
                    btnWithDraw.Visible = false;

                    btnPrevious.Attributes.Add("OnClick", "return fnSubmitForm('1')");
                    btnNext.Attributes.Add("OnClick", "return fnSubmitForm('1')");
                    btnSubmit.Attributes.Add("OnClick", "return fnSubmitForm('2')");
                    btnWithDraw.Attributes.Add("OnClick", "return fnEcreditFormWithDraw();");

                    if (rbType.SelectedIndex < 0)
                    {
                        btnPrint.Visible = false;
                        btnPrint1.Visible = false;
                    }
                    else
                    {
                        btnPrint.Visible = true;
                        btnPrint1.Visible = true;
                    }                    
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

        protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (rbType.SelectedIndex >= 0)
                {

                    LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlBranch, "BranchCode", "BranchName", true, "");
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                    if (rbType.SelectedIndex < 2)
                    {
                        hdnStateCode.Value = States.GetCurrentState(ddlBranch.SelectedValue).ToString();

                        LoadDropDownLists<CustomerDtls>(customer.GetDistrictMaster(Session["BranchCode"].ToString()), ddlDealerDistrict, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlDealerState, "StateCode", "StateName", true, "");
                        LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(ddlBranch.SelectedValue), ddlDealerTown, "Towncode", "TownName", true, "");
                        LoadDropDownLists<CustomerFields>(customer.GetSalesmanEcredit(ddlBranch.SelectedValue), ddlAssignedSMRR, "Salesman_Code", "Salesman", true, "");
                        //LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), ddlSis1, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlParentBranch, "BranchCode", "BranchName", true, "");
                        //LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), ddlParentCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlMigrateBranch, "BranchCode", "BranchName", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), ddlMigrateCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlBranch.SelectedValue), ddlCustomerCode, "Code", "Name", true, "");
                        //LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerCreditIndicator"), ddlCrLimitIndicator, "DisplayValue", "DisplayText", true, "");
                    }

                    switch (rbType.SelectedValue)
                    {
                        case "N":
                            pnlEcreditForm.Visible = true;
                            divdealerCode.Visible = false;
                            divdealerCode1.Visible = false;
                            divtrans.Visible = true;
                            ddlReasonForClosure.Enabled = false;
                            txtWriteOffAmout.Enabled = false;
                            txtExistingCrLimit.Enabled = false;
                            txtEnhCrLimtReq.Enabled = false;
                            ddlCrLimitIndicator.SelectedValue = "CD";
                            ddlCrLimitIndicator.Enabled = false;
                            Panel1.Enabled = true;
                            ddlApplicationNo.Visible = false;
                            txtApplicationNo.Visible = true;
                            break;
                        case "E":
                            pnlEcreditForm.Visible = true;
                            divdealerCode.Visible = true;
                            divdealerCode1.Visible = true;
                            divtrans.Visible = true;
                            txtExistingCrLimit.Enabled = true;
                            txtEnhCrLimtReq.Enabled = true;
                            Panel1.Enabled = false;
                            ddlApplicationNo.Visible = false;
                            txtApplicationNo.Visible = true;
                            LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(Session["BranchCode"].ToString()), ddlCustomerCode, "Code", "Name", true, "");
                            break;
                        case "W":
                            Panel1.Enabled = false;
                            Panel2.Enabled = false;
                            Panel3.Enabled = false;
                            Panel4.Enabled = false;
                            Panel1.Visible = true;
                            Panel2.Visible = true;
                            Panel3.Visible = true;
                            Panel4.Visible = true;
                            btnSubmit.Visible = false;
                            btnPrevious.Visible = false;
                            btnNext.Visible = false;
                            pnlEcreditForm.Visible = true;
                            divdealerCode.Visible = true;
                            divdealerCode1.Visible = true;
                            divtrans.Visible = true;
                            txtExistingCrLimit.Enabled = true;
                            txtEnhCrLimtReq.Enabled = true;
                            Panel1.Enabled = false;
                            txtApplicationDate.Text = "";
                            ddlApplicationNo.Visible = true;
                            txtApplicationNo.Visible = false;
                            btnWithDraw.Visible = false;
                            btnPrint.Visible = false;
                            btnPrint1.Visible = false;

                            ddlApplicationNo.DataSource = (object)ecrdtTrans.GetEcreditApplicationNosWithDrawal(Session["BranchCode"].ToString());
                            ddlApplicationNo.DataTextField = "ItemDesc";
                            ddlApplicationNo.DataValueField = "ItemCode";
                            ddlApplicationNo.DataBind();

                            break;
                        case "R":
                            Panel1.Enabled = false;
                            Panel2.Enabled = false;
                            Panel3.Enabled = false;
                            Panel4.Enabled = false;
                            Panel1.Visible = true;
                            Panel2.Visible = true;
                            Panel3.Visible = true;
                            Panel4.Visible = true;
                            btnSubmit.Visible = false;
                            btnPrevious.Visible = false;
                            btnNext.Visible = false;
                            pnlEcreditForm.Visible = true;
                            divdealerCode.Visible = true;
                            divdealerCode1.Visible = true;
                            divtrans.Visible = true;
                            txtExistingCrLimit.Enabled = true;
                            txtEnhCrLimtReq.Enabled = true;
                            Panel1.Enabled = false;
                            txtApplicationDate.Text = "";
                            ddlApplicationNo.Visible = true;
                            txtApplicationNo.Visible = false;

                            ddlApplicationNo.DataSource = (object)ecrdtTrans.GetEcreditApplicationNosReprint(Session["BranchCode"].ToString());
                            ddlApplicationNo.DataTextField = "ItemDesc";
                            ddlApplicationNo.DataValueField = "ItemCode";
                            ddlApplicationNo.DataBind();

                            break;
                    }

                    FirstGridViewRow();

                    btnPrevious.Enabled = false;
                    btnPrevious.Visible = false;

                    if (rbType.SelectedValue != "W")
                    {
                        btnPrint.Visible = true;
                        btnPrint1.Visible = true;
                    }

                    ddlParentBranch.Items.Remove(ddlParentBranch.Items.FindByValue(ddlBranch.SelectedValue));
                    ddlMigrateBranch.SelectedValue = ddlBranch.SelectedValue;
                    ddlMigrateBranch.Enabled = false;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnPrint1.Visible = false;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    divtrans.Visible = false;
                }

                rbType.Enabled = false;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Dealer_Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("Dealer_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Dealer_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit_Limit", typeof(string)));

            DataRow dr = null;
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["SNo"] = i + 1;
                dr["Dealer_Branch"] = string.Empty;
                dr["Dealer_Name"] = string.Empty;
                dr["Dealer_Code"] = string.Empty;
                dr["Credit_Limit"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grvGroupDealers.DataSource = dt;
            ViewState["GridRowCount"] = dt.Rows.Count.ToString();
            grvGroupDealers.DataBind();
        }

        protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lstEcreditEntity = ecrdtTrans.GetPendingRequestDetails(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);

                if (lstEcreditEntity.Count <= 0)
                {
                    lstEcreditEntity = null;

                    lstEcreditEntity = ecrdtTrans.GetCustDetailsEcreditForm(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);

                    if (lstEcreditEntity.Count > 0)
                    {
                        ddlCustomerCode.SelectedValue = lstEcreditEntity[0].Customer_Code;
                        txtCustCode.Text = ddlCustomerCode.SelectedValue;
                        txtCustName.Text = lstEcreditEntity[0].Customer_Name;
                        txtAdd1.Text = lstEcreditEntity[0].Customer_Address1;
                        txtAdd2.Text = lstEcreditEntity[0].Customer_Address2;
                        txtAdd3.Text = lstEcreditEntity[0].Customer_Address3;
                        txtAdd4.Text = lstEcreditEntity[0].Customer_Address4;
                        txtPropName.Text = lstEcreditEntity[0].Proprietor_Name;
                        txtPropMobile.Text = lstEcreditEntity[0].Proprietor_Mobile;

                        DataTable dt = ecrdtTrans.GetECrdeitApplnSisDetailsGroup(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);

                        if (lstEcreditEntity[0].Group_Company_Codes.IndexOf(",") > 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = dt;
                                ViewState["GridRowCount"] = dt.Rows.Count.ToString();
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
                        hdnCrLimitInd.Value = lstEcreditEntity[0].Cash_Credit_Limit_Indicator;
                        hdnDealerClassification.Value = lstEcreditEntity[0].Classification;
                        hdnDealerSegment.Value = lstEcreditEntity[0].Segment;
                        hdnDealersMultipleTown.Value = lstEcreditEntity[0].MultipleTown;
                        hdnDealerDealingGroups.Value = lstEcreditEntity[0].Dealing_with_Other_Group_Co;
                        txtTransporterName.Text = lstEcreditEntity[0].Transporter_Name;
                        txtCashPurchase.Text = IndianMoneyConversion(lstEcreditEntity[0].Cash_Purchase_Value);
                        txtOsAmt.Text = IndianMoneyConversion(lstEcreditEntity[0].Outstanding_Amount);
                        txtExistingCrLimit.Text = IndianMoneyConversion(lstEcreditEntity[0].Existing_Credit_Limit);
                        txtEnhCrLimtReq.Text = IndianMoneyConversion(lstEcreditEntity[0].Proposed_Credit_Limit);
                        ddlValidityIndicator.SelectedValue = lstEcreditEntity[0].Cr_Limit_Validity_Ind;
                        txtCrlimitDueDate.Text = lstEcreditEntity[0].Cr_Limit_Due_Date;

                        if (lstEcreditEntity[0].CDenabled == "Y")
                            chkCDEnabled.Checked = true;
                        else
                            chkCDEnabled.Checked = false;

                        hdnFreightIndicator.Value = lstEcreditEntity[0].FreightIndicator;
                        txtFirstCrLimitReq.Text = IndianMoneyConversion(lstEcreditEntity[0].First_Time_Credit_Limit_Request);
                        txtBankAccountNo.Text = lstEcreditEntity[0].Bank_AccountNo;
                        txtBankName.Text = lstEcreditEntity[0].Bank_Name;
                        txtBankBranch.Text = lstEcreditEntity[0].Bank_Branch;
                        txtIFSCcode.Text = lstEcreditEntity[0].IFSC_Code;
                        txtAccountName.Text = lstEcreditEntity[0].Name_of_Account_Holder;
                        txtCarNoExpDate.Text = lstEcreditEntity[0].Debit_Credit_Card_Number;
                        hdnPaymentMode.Value = lstEcreditEntity[0].Payment_Mode;
                        txtCrLimitApprovalDate.Text = "";
                        txtApprovedCrLimit.Text = "";
                        txtCustomerCode.Text = lstEcreditEntity[0].Customer_Code;
                        hdnReasonForClosure.Value = lstEcreditEntity[0].Reason_For_closure;
                        txtWriteOffAmout.Text = IndianMoneyConversion(lstEcreditEntity[0].Written_Off_Amount);
                        txtExistingCrLimit.Enabled = false;
                        txtOsAmt.Enabled = false;

                        List<EcreditPaymentPattern> lstPymtPatternDtls = new List<EcreditPaymentPattern>();
                        lstPymtPatternDtls = ecrdtTrans.GetPaymentPatternDetails(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);
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

                        Panel1.Enabled = true;
                    }
                    else
                    {
                        Panel1.Enabled = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('ECredit Form Request # " + lstEcreditEntity[0].Form_Number + " dated " + lstEcreditEntity[0].Form_Date + " is pending for Approval at Approver end regarding Enhancement of Credit Limit from Old Limit of Rs." + IndianMoneyConversion(lstEcreditEntity[0].Existing_Credit_Limit) + " to New Limit of Rs." + IndianMoneyConversion(lstEcreditEntity[0].Proposed_Credit_Limit) + ". Please do follow up');", true);
                    return;
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
                    List<ECreditApplicationCust> lstEcreditEntity = new List<ECreditApplicationCust>();
                    lstEcreditEntity = ecrdtTrans.GetEcreditApplicationDetails(ddlBranch.SelectedValue, ddlApplicationNo.SelectedValue.ToString());

                    if (lstEcreditEntity.Count > 0)
                    {
                        ddlCustomerCode.Enabled = false;
                        LoadDropDownLists<CustomerDtls>(customer.GetDistrictMaster(ddlBranch.SelectedValue), ddlDealerDistrict, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.StateMaster>(States.GetAllStates(), ddlDealerState, "StateCode", "StateName", true, "");
                        LoadDropDownLists<IMPALLibrary.Town>(Towns.GetAllTownsBranch(ddlBranch.SelectedValue), ddlDealerTown, "Towncode", "TownName", true, "");
                        LoadDropDownLists<CustomerFields>(customer.GetSalesmanEcredit(ddlBranch.SelectedValue), ddlAssignedSMRR, "Salesman_Code", "Salesman", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlParentBranch, "BranchCode", "BranchName", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlBranch.SelectedValue), ddlParentCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlMigrateBranch, "BranchCode", "BranchName", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlBranch.SelectedValue), ddlMigrateCustomer, "Code", "Name", true, "");
                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlBranch.SelectedValue), ddlCustomerCode, "Code", "Name", true, "");
                        //LoadDropDownLists<IMPALLibrary.Common.DropDownListValue>(objImpalLibrary.GetDropDownListValues("CustomerCreditIndicator"), ddlCrLimitIndicator, "DisplayValue", "DisplayText", true, "");

                        txtApplicationDate.Text = lstEcreditEntity[0].Form_Date;
                        ddlCustomerCode.SelectedValue = lstEcreditEntity[0].Customer_Code;
                        txtCustCode.Text = ddlCustomerCode.SelectedValue;
                        txtCustName.Text = lstEcreditEntity[0].Customer_Name;
                        txtAdd1.Text = lstEcreditEntity[0].Customer_Address1;
                        txtAdd2.Text = lstEcreditEntity[0].Customer_Address2;
                        txtAdd3.Text = lstEcreditEntity[0].Customer_Address3;
                        txtAdd4.Text = lstEcreditEntity[0].Customer_Address4;
                        txtPropName.Text = lstEcreditEntity[0].Proprietor_Name;
                        txtPropMobile.Text = lstEcreditEntity[0].Proprietor_Mobile;

                        DataTable dt = ecrdtTrans.GetECrdeitApplnSisDetailsGroup(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);

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
                        txtCrLimitApprovalDate.Text = lstEcreditEntity[0].HO_Approval_Date;
                        txtApprovedCrLimit.Text = IndianMoneyConversion(lstEcreditEntity[0].Approved_Credit_Limit);
                        txtCustomerCode.Text = lstEcreditEntity[0].Customer_Code;
                        ddlReasonForClosure.SelectedValue = lstEcreditEntity[0].Reason_For_closure;
                        txtWriteOffAmout.Text = IndianMoneyConversion(lstEcreditEntity[0].Written_Off_Amount);
                        hdnAccPeriodCode.Value = lstEcreditEntity[0].Accounting_Period_Code;
                        lblHOApprovalRemarks.Text = lstEcreditEntity[0].HO_Approval_Status_Remarks;

                        txtApprovedCrLimit.Attributes.Add("OnChange", "ApprovedCrLimitValidation();");
                        txtExistingCrLimit.Enabled = false;
                        txtEnhCrLimtReq.Enabled = false;
                        ddlCrLimitIndicator.Enabled = false;
                        Panel1.Enabled = false;
                        Panel2.Enabled = false;
                        Panel3.Enabled = false;
                        Panel4.Enabled = false;
                        btnSubmit.Visible = false;

                        List<EcreditPaymentPattern> lstPymtPatternDtls = new List<EcreditPaymentPattern>();
                        lstPymtPatternDtls = ecrdtTrans.GetPaymentPatternDetails(ddlBranch.SelectedValue, ddlCustomerCode.SelectedValue);
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

                        if (rbType.SelectedValue == "W")
                        {
                            btnWithDraw.Visible = true;
                        }
                    }
                    else
                    {
                        btnWithDraw.Visible = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Application Details Not Available.');", true);
                        return;
                    }
                }
                else
                {
                    btnWithDraw.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Please Select an Application Number.');", true);
                    return;
                }
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
                AddNewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSisCustBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddlSisCustBranch = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSisCustBranch.Parent.Parent);
                DropDownList ddlSisCust = (DropDownList)grdrDropDownRow.FindControl("ddlSisCust");

                if (ddlSisCustBranch.SelectedIndex > 0)
                {
                    LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocationGrpDlrs(ddlSisCustBranch.SelectedValue, txtCustCode.Text), ddlSisCust, "Code", "Name", true, "");
                }
                else
                    ddlSisCustBranch.Items.Clear();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSisCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddlSisCust = (DropDownList)sender;
                GridViewRow grdrDropDownRow = ((GridViewRow)ddlSisCust.Parent.Parent);
                DropDownList ddlSisCustBranch = (DropDownList)grdrDropDownRow.FindControl("ddlSisCustBranch");
                TextBox txtSisCustCode = (TextBox)grdrDropDownRow.FindControl("txtSisCustCode");
                TextBox txtSisCustCrLimit = (TextBox)grdrDropDownRow.FindControl("txtSisCustCrLimit");

                bool isExisting = CheckExisting(ddlSisCust.SelectedValue);

                if (isExisting)
                {
                    ECreditFormCustTransactions oDtls = new ECreditFormCustTransactions();
                    CustomerDtls oCustomer = null;
                    oCustomer = oDtls.GetECrdeitApplnSisDetails(ddlSisCustBranch.SelectedValue, ddlSisCust.SelectedValue);
                    txtSisCustCode.Text = oCustomer.Code;
                    txtSisCustCrLimit.Text = IndianMoneyConversion(oCustomer.CrLimit);
                }
                else
                {
                    ddlSisCustBranch.SelectedIndex = 0;
                    ddlSisCust.SelectedIndex = 0;
                    txtSisCustCode.Text = "";
                    txtSisCustCrLimit.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Group Dealer already exists');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlParentBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlParentBranch.SelectedIndex > 0)
                {
                    LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlParentBranch.SelectedValue), ddlParentCustomer, "Code", "Name", true, "");
                }
                else
                {
                    ddlParentCustomer.Items.Clear();
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
                    if (grvGroupDealers.DataSource != null)
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            DropDownList ddlSisCustBranch = (DropDownList)e.Row.FindControl("ddlSisCustBranch");
                            LoadDropDownLists<IMPALLibrary.Branch>(Branch.GetAllBranchECreditApplicationParent(), ddlSisCustBranch, "BranchCode", "BranchName", true, "");
                        }
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

        protected void grvGroupDealers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (e.RowIndex >= 0)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    dt.Rows.Clear();
                    dt.AcceptChanges();

                    if (grvGroupDealers.Rows.Count >= 1)
                    {
                        for (int i = 0; i < grvGroupDealers.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            var ddlSisCustBranch = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCustBranch");
                            var ddlSisCust = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCust");
                            var txtSisCustCode = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCode");
                            var txtSisCustCrLimit = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCrLimit");

                            dr["Dealer_Branch"] = ddlSisCustBranch.SelectedValue;
                            dr["Dealer_Name"] = ddlSisCust.SelectedValue;
                            dr["Dealer_Code"] = txtSisCustCode.Text;
                            dr["Credit_Limit"] = txtSisCustCrLimit.Text;
                            dt.Rows.Add(dr);
                        }
                    }

                    dt.Rows.RemoveAt(e.RowIndex);
                    grvGroupDealers.DataSource = dt;
                    grvGroupDealers.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        FirstGridViewRow();
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlSisCustBranch = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCustBranch");
                            DropDownList ddlSisCust = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCust");
                            TextBox txtSisCustCode = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCode");
                            TextBox txtSisCustCrLimit = (TextBox)grvGroupDealers.Rows[i].FindControl("txtSisCustCrLimit");

                            ddlSisCustBranch.SelectedValue = dt.Rows[i]["Dealer_Branch"].ToString();

                            LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlSisCustBranch.SelectedValue), ddlSisCust, "Code", "Name", true, "");
                            ddlSisCust.SelectedValue = dt.Rows[i]["Dealer_Name"].ToString();

                            txtSisCustCode.Text = dt.Rows[i]["Dealer_Code"].ToString();
                            txtSisCustCrLimit.Text = IndianMoneyConversion(dt.Rows[i]["Credit_Limit"].ToString());

                            ddlSisCustBranch.Enabled = false;
                            ddlSisCust.Enabled = false;
                        }

                        ViewState["CurrentTable"] = dt;
                        ViewState["GridRowCount"] = dt.Rows.Count.ToString();                        
                    }                                       
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private bool CheckExisting(string GroupDealer)
        {
            if (grvGroupDealers.Rows.Count > 1)
            {
                for (int i = 0; i < grvGroupDealers.Rows.Count - 1; i++)
                {
                    DropDownList ddlSisCust = (DropDownList)grvGroupDealers.Rows[i].FindControl("ddlSisCust");

                    if (GroupDealer == ddlSisCust.SelectedValue)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void AddNewRow()
        {
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
                            DropDownList ddlSisCustBranch = (DropDownList)grvGroupDealers.Rows[iNoofRows - 1].FindControl("ddlSisCustBranch");
                            DropDownList ddlSisCust = (DropDownList)grvGroupDealers.Rows[iNoofRows - 1].FindControl("ddlSisCust");
                            TextBox txtSisCustCode = (TextBox)grvGroupDealers.Rows[iNoofRows - 1].FindControl("txtSisCustCode");
                            TextBox txtSisCustCrLimit = (TextBox)grvGroupDealers.Rows[iNoofRows - 1].FindControl("txtSisCustCrLimit");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["SNo"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["Dealer_Branch"] = ddlSisCustBranch.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Dealer_Name"] = ddlSisCust.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Dealer_Code"] = ddlSisCust.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Credit_Limit"] = IndianMoneyConversion(txtSisCustCrLimit.Text);
                        }
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    grvGroupDealers.DataSource = dtCurrentTable;
                    grvGroupDealers.DataBind();
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
                        DropDownList ddlSisCustBranch = (DropDownList)grvGroupDealers.Rows[i - 1].FindControl("ddlSisCustBranch");
                        DropDownList ddlSisCust = (DropDownList)grvGroupDealers.Rows[i - 1].FindControl("ddlSisCust");
                        TextBox txtSisCustCode = (TextBox)grvGroupDealers.Rows[i - 1].FindControl("txtSisCustCode");
                        TextBox txtSisCustCrLimit = (TextBox)grvGroupDealers.Rows[i - 1].FindControl("txtSisCustCrLimit");

                        ddlSisCustBranch.SelectedValue = dt.Rows[i - 1]["Dealer_Branch"].ToString();

                        LoadDropDownLists<CustomerDtls>(customer.GetCustomerswithLocation(ddlSisCustBranch.SelectedValue), ddlSisCust, "Code", "Name", true, "");
                        ddlSisCust.SelectedValue = dt.Rows[i - 1]["Dealer_Name"].ToString();

                        txtSisCustCode.Text = dt.Rows[i - 1]["Dealer_Code"].ToString();
                        txtSisCustCrLimit.Text = IndianMoneyConversion(dt.Rows[i - 1]["Credit_Limit"].ToString());

                        ddlSisCustBranch.Enabled = false;
                        ddlSisCust.Enabled = false;
                        rowIndex++;                        
                    }
                }
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (Panel1.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnPrevious.Visible = false;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;
                }
                else if (Panel2.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = false;
                    btnPrevious.Visible = false;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;

                    hdnDealerClassification.Value = ddlDealerClassification.SelectedValue;
                    hdnDealerSegment.Value = ddlDealerSegment.SelectedValue;
                    hdnDealersMultipleTown.Value = ddlDealersMultipleTown.SelectedValue;
                    hdnDealerDealingGroups.Value = ddlDealerDealingGroups.SelectedValue;
                }
                else if (Panel3.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;

                    if (ddlCustomerCode.SelectedIndex > 0)
                    {
                        ddlDealerClassification.SelectedValue = hdnDealerClassification.Value;
                        ddlDealerSegment.SelectedValue = hdnDealerSegment.Value;
                        ddlCrLimitIndicator.SelectedValue = hdnCrLimitInd.Value;                        
                        ddlDealersMultipleTown.SelectedValue = hdnDealersMultipleTown.Value;
                        ddlDealerDealingGroups.SelectedValue = hdnDealerDealingGroups.Value;
                        hdnFreightIndicator.Value = ddlFreightIndicator.SelectedValue;
                        hdnCrLimitInd.Value = ddlCrLimitIndicator.SelectedValue;
                        hdnPaymentMode.Value = ddlPaymentMode.SelectedValue;
                        hdnValidityIndicator.Value = ddlValidityIndicator.SelectedValue;
                    }
                }
                else if (Panel4.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;

                    if (ddlCustomerCode.SelectedIndex > 0)
                    {
                        ddlPaymentMode.SelectedValue = hdnPaymentMode.Value;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (Panel1.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;

                    if (ddlCustomerCode.SelectedIndex > 0)
                    {
                        ddlDealerClassification.SelectedValue = hdnDealerClassification.Value;
                        ddlDealerSegment.SelectedValue = hdnDealerSegment.Value;                        
                        ddlDealersMultipleTown.SelectedValue = hdnDealersMultipleTown.Value;
                        ddlDealerDealingGroups.SelectedValue = hdnDealerDealingGroups.Value;                        
                    }
                }
                else if (Panel2.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel4.Visible = false;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    btnNext.Enabled = true;
                    btnNext.Visible = true;

                    if (ddlCustomerCode.SelectedIndex > 0)
                    {
                        ddlCrLimitIndicator.SelectedValue = hdnCrLimitInd.Value;
                        ddlValidityIndicator.SelectedValue = hdnValidityIndicator.Value;
                        ddlFreightIndicator.SelectedValue = hdnFreightIndicator.Value;
                        ddlPaymentMode.SelectedValue = hdnPaymentMode.Value;
                    }
                }
                else if (Panel3.Visible)
                {
                    ddlCustomerCode.Enabled = false;
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = true;
                    btnPrevious.Enabled = true;
                    btnPrevious.Visible = true;
                    btnNext.Enabled = false;
                    btnNext.Visible = false;

                    if (ddlCustomerCode.SelectedIndex > 0)
                    {
                        ddlReasonForClosure.SelectedValue = hdnReasonForClosure.Value;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Panel1.Visible = true;
                Panel2.Visible = true;
                Panel3.Visible = true;
                Panel4.Visible = true;
                pnlEcreditForm.Enabled = false;
                btnSubmit.Visible = false;
                btnPrevious.Visible = false;
                btnNext.Visible = false;
                btnPrint1.Visible = false;
                pnlEcreditSel.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "javascript:fnprint()", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnWithDraw_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ddlApplicationNo.Enabled = false;
                btnSubmit.Visible = false;
                btnWithDraw.Visible = false;

                ECreditApplicationCust ecreditAppln = new ECreditApplicationCust();
                ecreditAppln.Branch_Code = ddlBranch.SelectedIndex == 0 ? ddlApplicationNo.SelectedValue.Substring(14, 3) : ddlBranch.SelectedValue;
                ecreditAppln.Form_Number = ddlApplicationNo.SelectedValue;
                ecreditAppln.Customer_Code = ddlCustomerCode.SelectedValue;
                ecreditAppln.Userid = Session["UserID"].ToString();
                ecreditAppln.Remarks = Session["EcreditFormWithDrawRemarks"].ToString();

                ecrdtTrans.UpdECrdeitApplicationFormWithDraw(ecreditAppln);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('E Credit Form has been WithDrawn Sucessfully');", true);
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
                objp.Session["EcreditFormWithDrawRemarks"] = Remarks;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string groupDealers = "";

            try
            {
                ECreditApplicationCust ecreditAppln = new ECreditApplicationCust();

                foreach (GridViewRow gr in grvGroupDealers.Rows)
                {
                    TextBox txtSisCustCode = (TextBox)gr.FindControl("txtSisCustCode");
                    groupDealers = groupDealers + txtSisCustCode.Text + ",";
                }

                if (groupDealers != "")
                    groupDealers = groupDealers.Substring(0, groupDealers.Length - 1);

                ecreditAppln.Indicator = rbType.SelectedValue;
                ecreditAppln.Branch_Code = ddlBranch.SelectedValue;
                ecreditAppln.Customer_Code = ddlCustomerCode.SelectedValue;
                ecreditAppln.Customer_Name = txtCustName.Text;
                ecreditAppln.Customer_Address1 = txtAdd1.Text;
                ecreditAppln.Customer_Address2 = txtAdd2.Text;
                ecreditAppln.Customer_Address3 = txtAdd3.Text;
                ecreditAppln.Customer_Address4 = txtAdd4.Text;
                ecreditAppln.Proprietor_Name = txtPropName.Text;
                ecreditAppln.Proprietor_Mobile = txtPropMobile.Text;
                ecreditAppln.Contact_Person = txtContactPersonName.Text;
                ecreditAppln.Contact_Person_Mobile = txtContactPersonMobNo.Text;
                ecreditAppln.Group_Company_Codes = groupDealers;
                ecreditAppln.Migration_From_Branch_Code = ddlParentBranch.SelectedValue;
                ecreditAppln.Migration_From_Customer_Code = ddlParentCustomer.SelectedValue;
                ecreditAppln.Year_Of_Establishment = txtYearofEstbl.Text;
                ecreditAppln.State_Code = ddlDealerState.SelectedValue;
                ecreditAppln.District_Code = ddlDealerDistrict.SelectedValue;
                ecreditAppln.Town_Code = ddlDealerTown.SelectedValue;
                ecreditAppln.Town_Classification = ddlTownClassification.SelectedValue;
                ecreditAppln.Phone_No = txtContactPersonName.Text;
                ecreditAppln.Mobile_No = txtContactPersonMobNo.Text;
                ecreditAppln.Location = txtLocation.Text;
                ecreditAppln.Email = txtEmailid.Text;
                ecreditAppln.PinCode = txtPinCode.Text;
                ecreditAppln.GSTIN_No = txtGSTIN.Text;
                ecreditAppln.Type_of_Company = ddlFirmType.SelectedValue;
                ecreditAppln.Type_of_Registration = ddlRegnType.SelectedValue;
                ecreditAppln.Turnover_Value = txtAnnlTunOver.Text;
                ecreditAppln.Impal_Turnover_Value = txtLineSaleTurnOver.Text;
                ecreditAppln.GSTIN_Location = ddlGSTINlocation.SelectedValue;
                ecreditAppln.Stock_Value = txtOverAllStockValue.Text;
                ecreditAppln.Representative_Type = ddlDealerServicedBy.SelectedValue;
                ecreditAppln.Distance_From_Branch = txtDistance.Text;
                ecreditAppln.Distance_from_RR = txtDistanceSM.Text;
                ecreditAppln.Travel_Classificaion = ddlDayTravelOS.SelectedValue;
                ecreditAppln.Salesman_code = ddlAssignedSMRR.SelectedValue;
                ecreditAppln.Dealer_Target = txtDealerTarget.Text;
                ecreditAppln.Period_of_Visit = ddlVisitPlan.SelectedValue;
                ecreditAppln.Classification = ddlDealerClassification.SelectedValue == "" ? hdnDealerClassification.Value : ddlDealerClassification.SelectedValue;
                ecreditAppln.Segment = ddlDealerSegment.SelectedValue == "" ? hdnDealerSegment.Value : ddlDealerSegment.SelectedValue;
                ecreditAppln.Supplier_code_From_Direct_Suppliers = txtKeyLine1.Text + "," + txtKeyLine2.Text + "," + txtKeyLine3.Text + "," + txtKeyLine4.Text + "," + txtKeyLine5.Text + "," + txtKeyLine6.Text;
                ecreditAppln.MultipleTown = ddlDealersMultipleTown.SelectedValue == "" ? hdnDealersMultipleTown.Value : ddlDealersMultipleTown.SelectedValue;
                ecreditAppln.Dealing_with_Other_Group_Co = ddlDealerDealingGroups.SelectedValue == "" ? hdnDealerDealingGroups.Value : ddlDealerDealingGroups.SelectedValue;
                ecreditAppln.ASC_Line_Codes = txtLinesASC1.Text + "," + txtLinesASC2.Text + "," + txtLinesASC3.Text + "," + txtLinesASC4.Text;
                ecreditAppln.Additonal_Dealer_Info = txtAddlInfo.Text;
                ecreditAppln.Transporter_Name = txtTransporterName.Text;
                ecreditAppln.Remarks = txtAddlInfo1.Text;
                ecreditAppln.ASD_Line_Codes = txtOtherAuthDetails.Text;
                ecreditAppln.Cash_Purchase_Value = txtCashPurchase.Text;
                ecreditAppln.Expected_Supplier_Codes = txtExpSalesDtls1.Text + "," + txtExpSalesDtls2.Text + "," + txtExpSalesDtls3.Text + "," + txtExpSalesDtls4.Text + "," + txtExpSalesDtls5.Text + "," + txtExpSalesDtls6.Text + "," + txtExpSalesDtls7.Text + "," + txtExpSalesDtls8.Text + "," + txtExpSalesDtls9.Text;
                ecreditAppln.Outstanding_Amount = txtOsAmt.Text;
                ecreditAppln.Existing_Credit_Limit = txtExistingCrLimit.Text;
                ecreditAppln.Proposed_Credit_Limit = txtEnhCrLimtReq.Text;
                ecreditAppln.Cash_Credit_Limit_Indicator = ddlCrLimitIndicator.SelectedValue == "" ? hdnCrLimitInd.Value : ddlCrLimitIndicator.SelectedValue;
                ecreditAppln.Cr_Limit_Validity_Ind = ddlValidityIndicator.SelectedValue;
                ecreditAppln.Cr_Limit_Due_Date = txtCrlimitDueDate.Text;
                ecreditAppln.CDenabled = chkCDEnabled.Checked ? "Y" : "N";
                ecreditAppln.FreightIndicator = ddlFreightIndicator.SelectedValue;
                ecreditAppln.First_Time_Credit_Limit_Request = txtFirstCrLimitReq.Text;
                ecreditAppln.Bank_AccountNo = txtBankAccountNo.Text;
                ecreditAppln.Bank_Name = txtBankName.Text;
                ecreditAppln.Bank_Branch = txtBankBranch.Text;
                ecreditAppln.IFSC_Code = txtIFSCcode.Text;
                ecreditAppln.Name_of_Account_Holder = txtAccountName.Text;
                ecreditAppln.Debit_Credit_Card_Number = txtCarNoExpDate.Text;
                ecreditAppln.Expiry_Date = "";
                ecreditAppln.Payment_Mode = ddlPaymentMode.SelectedValue == "" ? hdnPaymentMode.Value : ddlPaymentMode.SelectedValue;
                ecreditAppln.Branch_Approval_Status = "A";
                ecreditAppln.Branch_Approval_Date = "";
                ecreditAppln.HO_Approval_Status = "A";
                ecreditAppln.HO_Approval_Date = txtCrLimitApprovalDate.Text;
                ecreditAppln.Approved_Credit_Limit = txtApprovedCrLimit.Text;
                ecreditAppln.Date_of_Closure = "";
                ecreditAppln.Reason_For_closure = ddlReasonForClosure.SelectedValue == "" ? hdnReasonForClosure.Value : ddlReasonForClosure.SelectedValue;
                ecreditAppln.Written_Off_Amount = txtWriteOffAmout.Text;
                ecreditAppln.Status = "A";
                ecreditAppln.Userid = Session["UserID"].ToString();

                DataSet ds = ecrdtTrans.AddECrdeitApplicationForm(ecreditAppln);

                if (ecreditAppln.ErrorCode == "0")
                {
                    txtApplicationNo.Text = ds.Tables[0].Rows[0]["Form_Number"].ToString();

                    if (rbType.SelectedValue == "N")
                        txtCustomerCode.Text = ds.Tables[0].Rows[0]["Customer_Code"].ToString();

                    string Emailid = ds.Tables[0].Rows[0]["Emailid"].ToString();
                    string cc = ds.Tables[0].Rows[0]["cc"].ToString();

                    if (txtExistingCrLimit.Text == "")
                        txtExistingCrLimit.Text = "0";

                    if (txtEnhCrLimtReq.Text == "")
                        txtEnhCrLimtReq.Text = "0";

                    Panel1.Visible = true;
                    Panel2.Visible = true;
                    Panel3.Visible = true;
                    Panel4.Visible = true;

                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    btnPrevious.Visible = false;
                    btnNext.Visible = false;

                    if (ddlReasonForClosure.SelectedIndex > 0)
                    {
                        SendMails(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Branch_Name"].ToString(), "C", ds.Tables[0].Rows[0]["Customer_Details"].ToString(), txtApplicationNo.Text, Emailid, ds.Tables[0].Rows[0]["Authority_Designation"].ToString(), cc, IndianMoneyConversion(txtExistingCrLimit.Text), IndianMoneyConversion(txtEnhCrLimtReq.Text), IndianMoneyConversion(txtWriteOffAmout.Text));

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Details have been updated and E - Credit Application # is raised by sending an Auto Mail to Authorized personnel for the Approval of Dealer Business Closure.');", true);
                    }
                    else if (Convert.ToDecimal(txtExistingCrLimit.Text) == Convert.ToDecimal(txtEnhCrLimtReq.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Details are Saved Successfully.');", true);
                    }
                    else if (Convert.ToDecimal(txtExistingCrLimit.Text) > Convert.ToDecimal(txtEnhCrLimtReq.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Details are Saved Successfully and Credit Limit has been decreased as you updated.');", true);
                    }
                    else
                    {
                        SendMails(ddlBranch.SelectedValue, ds.Tables[0].Rows[0]["Branch_Name"].ToString(), ds.Tables[0].Rows[0]["Indicator"].ToString(), ds.Tables[0].Rows[0]["Customer_Details"].ToString(), txtApplicationNo.Text, Emailid, ds.Tables[0].Rows[0]["Authority_Designation"].ToString(), cc, IndianMoneyConversion(txtExistingCrLimit.Text), IndianMoneyConversion(txtEnhCrLimtReq.Text), "0");

                        if (rbType.SelectedValue == "N")
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('New Customer has been Added and E - Credit Application # is raised by sending an Auto Mail to Authorized personnel for the sanction of New Credit Limit.');", true);
                        else
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Details have been updated and E - Credit Application # is raised by sending an Auto Mail to Authorized personnel for the Approval of Credit Limit Enhancement.');", true);
                    }
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

        protected void SendMails(string Branch_Code, string Branch_Name, string Indicator, string CustomerDetails, string FormNumber, string Emailid, string Designation, string cc, string OldLimit, string NewLimit, string WriteOffAmount)
        {
            MailAddress from = new MailAddress("ecreditform@impal.net", "E-Credit Application Form");
            MailAddress to = new MailAddress(Emailid);
            string BodyMessage = "";
            cc = "ecreditform@impal.net";

            if (Indicator == "C")
                BodyMessage = "<font face='Arial'>Dear " + Designation + ",<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been raised at <b>" + Branch_Name + "</b> Branch for your Approval regarding Closure of the Dealer Business of <b>" + CustomerDetails + "</b> with WriteOff Amount of <b>Rs." + WriteOffAmount + "</b>.";
            else if (Indicator == "E")
                BodyMessage = "<font face='Arial'>Dear " + Designation + ",<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been raised at <b>" + Branch_Name + "</b> Branch for your Approval regarding Enhancement of Credit Limit for the Existing Customer <b>" + CustomerDetails + "</b> from Old Limit of <b>Rs." + OldLimit + "</b> to New Limit of <b>Rs." + NewLimit + "</b>.";
            else
                BodyMessage = "<font face='Arial'>Dear " + Designation + ",<br><br>The E-Credit Application # <b>" + FormNumber + "</b> has been raised at <b>" + Branch_Name + "</b> Branch for your Approval regarding Sanction of Credit Limit of <b>Rs." + NewLimit + "</b> for the New Customer <b>" + CustomerDetails + "</b>.";

            BodyMessage = BodyMessage + "<br><br>Please login to <a href='https://www.impalspares.com'>www.impalspares.com</a> using your login credentials and approve the request form.<br><br>This is an auto system generated Mail.<br><br>Regards,<br>IMPAL IT Team</font>";

            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to))
            {
                if (Indicator == "C")
                    mm.Subject = "E-CREDIT FORM # " + FormNumber + " - CLOSURE OF BUSINESS " + DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
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
                rbType.ClearSelection();
                Server.ClearError();
                Response.Redirect("ECreditApplication.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}