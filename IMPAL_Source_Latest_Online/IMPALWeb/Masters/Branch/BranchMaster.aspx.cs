using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Masters.Branch
{
    public partial class BranchMaster : System.Web.UI.Page
    {
        
        #region system event methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lblNoRecord.Text = "";
                if (!IsPostBack)
                {
                    if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                    {
                        BranchFormView.ChangeMode(FormViewMode.ReadOnly);
                        BindBranchDetails(Session["BranchCode"].ToString());
                        btnReset.Enabled = false;
                        BtnUpdate.Visible = false;
                        BtnSubmit.Enabled = false;
                    }
                    else
                    {
                        BranchFormView.ChangeMode(FormViewMode.Insert);
                        BtnUpdate.Visible = false;
                        btnReport.Enabled = true;
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
            int iRowCount = -1;
            int iRowBranchCount = 0;
            string strBranchCode = "";
            try
            {
                if (BranchFormView.CurrentMode == FormViewMode.Insert)
                {



                    Branch_Master objIns = new Branch_Master();
                    BranchMasterItems objInsValue = new BranchMasterItems();

                    var txt = (TextBox)BranchFormView.FindControl("txtAddress");
                    objInsValue.Address = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtArea_sq_feet");
                    objInsValue.Area_in_Square_Feet = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtlblAccountsManager_Officer");
                    objInsValue.Branch_Accountant = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtPreferredCarrier");
                    objInsValue.Branch_Carrier = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtBranchName");
                    objInsValue.Branch_Name = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtBranchCode");
                    objInsValue.Branch_Code = txt.Text.Trim();
                    strBranchCode = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtBranchManager");
                    objInsValue.Branch_Manager = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtDestination");
                    objInsValue.Branch_Destination = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCentralSalesTaxNumber");
                    objInsValue.Central_sales_tax_number = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCESSNumber");
                    objInsValue.CESS_number = txt.Text.Trim();
                    var ddl = (DropDownList)BranchFormView.FindControl("ddlClassification");
                    objInsValue.Classification_Code = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtEDPin_charge");
                    objInsValue.EDP_In_charge = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtEmailid");
                    objInsValue.Email = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCloseDate");
                    objInsValue.End_Date = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtFax");
                    objInsValue.Fax = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtLocalST_RCNo");
                    objInsValue.Local_sales_tax_number = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtStockAging");
                    objInsValue.Min_Stock_Age_for_STDN = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtStockValue");
                    objInsValue.Min_Stock_Value_for_STDN = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtMonthlyRent");
                    objInsValue.Monthly_Rent = txt.Text;
                    txt = (TextBox)BranchFormView.FindControl("txtOpeningDate");
                    objInsValue.Opening_Date = txt.Text;
                    txt = (TextBox)BranchFormView.FindControl("txtO_SCalculationDays");
                    objInsValue.OS_Cancellation_days = txt.Text.Trim();
                    //txt = (TextBox)BranchFormView.FindControl("txtOSLocking");
                    //objInsValue.OS_Lock = txt.Text.Trim();
                    ddl = (DropDownList)BranchFormView.FindControl("ddlOSLocking");
                    objInsValue.OS_Lock = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtPhone");
                    objInsValue.Phone = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtRentalAdvance");
                    objInsValue.Rental_Advance = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtContractCompletionDate");
                    objInsValue.Rental_Contract_End_Date = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtContractStartDate");
                    objInsValue.Rental_Contract_Start_Date = txt.Text.Trim();
                    ddl = (DropDownList)BranchFormView.FindControl("ddlReportingSate");
                    objInsValue.Reporting_State_Code = ddl.SelectedValue;
                    var chkbox = (CheckBox)BranchFormView.FindControl("chkRoadPermitApplicable");
                    objInsValue.Road_Permit_Indicator = chkbox.Checked;
                    ddl = (DropDownList)BranchFormView.FindControl("ddlState");
                    objInsValue.State = ddl.SelectedValue;
                    ddl = (DropDownList)BranchFormView.FindControl("ddlStatus");
                    objInsValue.Status = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtTelex");
                    objInsValue.Telex = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtNoticePeriod");
                    objInsValue.Termination_Notice_Period = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtTINNumber");
                    objInsValue.tin = txt.Text.Trim();

                    iRowBranchCount = objIns.ChkInsBranchCode(strBranchCode);

                    if (iRowBranchCount > 0)
                         lblNoRecord.Text = "Record already Exist.";                       
                    else
                        iRowCount = objIns.InsBranchDetails(objInsValue);

                    if (iRowCount > 0)
                    {
                        txt = (TextBox)BranchFormView.FindControl("txtBranchCode");
                        BranchFormView.ChangeMode(FormViewMode.ReadOnly);
                        BindBranchDetails(txt.Text.ToString());
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);      
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;
            try
            {
                if (BranchFormView.CurrentMode == FormViewMode.Edit)
                {
                    Branch_Master objIns = new Branch_Master();
                    BranchMasterItems objInsValue = new BranchMasterItems();

                    var txt = (TextBox)BranchFormView.FindControl("txtAddress");
                    objInsValue.Address = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtArea_sq_feet");
                    objInsValue.Area_in_Square_Feet = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtlblAccountsManager_Officer");
                    objInsValue.Branch_Accountant = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtPreferredCarrier");
                    objInsValue.Branch_Carrier = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtBranchName");
                    objInsValue.Branch_Name = txt.Text.Trim();

                    var ddl = (DropDownList)BranchFormView.FindControl("ddlBranch");
                    objInsValue.Branch_Code = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtBranchManager");
                    objInsValue.Branch_Manager = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtDestination");
                    objInsValue.Branch_Destination = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCentralSalesTaxNumber");
                    objInsValue.Central_sales_tax_number = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCESSNumber");
                    objInsValue.CESS_number = txt.Text.Trim();
                    ddl = (DropDownList)BranchFormView.FindControl("ddlClassification");
                    objInsValue.Classification_Code = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtEDPin_charge");
                    objInsValue.EDP_In_charge = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtEmailid");
                    objInsValue.Email = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtCloseDate");
                    objInsValue.End_Date = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtFax");
                    objInsValue.Fax = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtLocalST_RCNo");
                    objInsValue.Local_sales_tax_number = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtStockAging");
                    objInsValue.Min_Stock_Age_for_STDN = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtStockValue");
                    objInsValue.Min_Stock_Value_for_STDN = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtMonthlyRent");
                    objInsValue.Monthly_Rent = txt.Text;
                    txt = (TextBox)BranchFormView.FindControl("txtOpeningDate");
                    objInsValue.Opening_Date = txt.Text;
                    txt = (TextBox)BranchFormView.FindControl("txtO_SCalculationDays");
                    objInsValue.OS_Cancellation_days = txt.Text.Trim();
                    //txt = (TextBox)BranchFormView.FindControl("txtOSLocking");
                    //objInsValue.OS_Lock = txt.Text.Trim();
                    ddl = (DropDownList)BranchFormView.FindControl("ddlOSLocking");
                    objInsValue.OS_Lock = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtPhone");
                    objInsValue.Phone = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtRentalAdvance");
                    objInsValue.Rental_Advance = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtContractCompletionDate");
                    objInsValue.Rental_Contract_End_Date = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtContractStartDate");
                    objInsValue.Rental_Contract_Start_Date = txt.Text.Trim();
                    ddl = (DropDownList)BranchFormView.FindControl("ddlReportingSate");
                    objInsValue.Reporting_State_Code = ddl.SelectedValue;
                    var chkbox = (CheckBox)BranchFormView.FindControl("chkRoadPermitApplicable");
                    objInsValue.Road_Permit_Indicator = chkbox.Checked;
                    ddl = (DropDownList)BranchFormView.FindControl("ddlState");
                    objInsValue.State = ddl.SelectedValue;
                    ddl = (DropDownList)BranchFormView.FindControl("ddlStatus");
                    objInsValue.Status = ddl.SelectedValue;
                    txt = (TextBox)BranchFormView.FindControl("txtTelex");
                    objInsValue.Telex = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtNoticePeriod");
                    objInsValue.Termination_Notice_Period = txt.Text.Trim();
                    txt = (TextBox)BranchFormView.FindControl("txtTINNumber");
                    objInsValue.tin = txt.Text.Trim();
                    iRowCount = objIns.UpdBranchDetails(objInsValue);

                    if (iRowCount > 0)
                    {
                       // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Updated Sucessfully.');", true);
                        lblNoRecord.Text = "Updated Sucessfully.";
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
                if (BranchFormView.CurrentMode == FormViewMode.Insert)
                {
                    if (((TextBox)BranchFormView.FindControl("txtBranchCode")).Visible)
                    {
                        if (((TextBox)BranchFormView.FindControl("txtBranchCode")).Text != "")
                        {
                            ClearControls();
                        }
                    }
                }
                else
                {
                    if (((DropDownList)BranchFormView.FindControl("ddlBranch")).Visible)
                    {
                        if (((DropDownList)BranchFormView.FindControl("ddlBranch")).SelectedIndex != 0)
                        {
                            ClearControls();
                        }
                    }
                }

                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
                BtnSubmit.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("BranchMasterReport.aspx");
                // var ddl = (DropDownList)BranchFormView.FindControl("ddlBranch");
                // string strBranch = ddl.SelectedValue;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (BranchFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    DropDownList ddlBranchReadOnly = (DropDownList)BranchFormView.FindControl("ddlBranch");
                    hdnBrnCurrentBranchCode.Value = ddlBranchReadOnly.SelectedValue;
                    BindBranchDetails(ddlBranchReadOnly.SelectedValue);
                    if (ddlBranchReadOnly.SelectedIndex == 0)
                        ClearControls(); 
                }
                else if (BranchFormView.CurrentMode == FormViewMode.Edit)
                {
                    DropDownList ddlBranchReadOnly = (DropDownList)BranchFormView.FindControl("ddlBranch");
                    hdnBrnCurrentBranchCode.Value = ddlBranchReadOnly.SelectedValue;
                    BindBranchDetails(ddlBranchReadOnly.SelectedValue);
                    if (ddlBranchReadOnly.SelectedIndex == 0)
                        ClearControls();
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
                if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                {
                    BranchFormView.ChangeMode(FormViewMode.ReadOnly);
                    BindBranchDetails(Session["BranchCode"].ToString());
                    if (((DropDownList)BranchFormView.FindControl("ddlBranch")).Visible)
                    {
                        if (((DropDownList)BranchFormView.FindControl("ddlBranch")).SelectedIndex == 0)
                        {
                            ClearControls();
                        }
                    }
                    BtnUpdate.Visible = false;
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    BranchFormView.ChangeMode(FormViewMode.Edit);
                    DropDownList ddlBranchReadOnly = (DropDownList)BranchFormView.FindControl("ddlBranch");
                //        hdnBrnCurrentBranchCode.Value = ddlBranchReadOnly.SelectedValue;
                    BindBranchDetails(Session["BranchCode"].ToString());
                    if (((DropDownList)BranchFormView.FindControl("ddlBranch")).Visible)
                    {
                        if (((DropDownList)BranchFormView.FindControl("ddlBranch")).SelectedIndex == 0)
                        {
                            ClearControls();
                        }
                    }
                    BtnUpdate.Visible = true;
                    BtnSubmit.Visible = false;
                    btnReport.Enabled = true;

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);      
            }
        }

       
        protected void BranchFormView_DataBound(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (BranchFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    DropDownList ddlBranchReadOnly = (DropDownList)BranchFormView.FindControl("ddlBranch");
                    BindBranchName(ddlBranchReadOnly);

                    DropDownList ddlStateReadOnly = (DropDownList)BranchFormView.FindControl("ddlState");
                    BindState(ddlStateReadOnly);

                    DropDownList ddlRptStateReadOnly = (DropDownList)BranchFormView.FindControl("ddlRptingState");
                    BindState(ddlRptStateReadOnly);

                    PopulateOSLocking();

                    //DropDownList ddlClassification = (DropDownList)BranchFormView.FindControl("ddlClassification");
                    //BindClassification(ddlClassification);

                    if (hdnBrnCurrentBranchCode.Value != "")
                        ddlBranchReadOnly.SelectedValue = hdnBrnCurrentBranchCode.Value;
                    else
                        ddlBranchReadOnly.SelectedValue = Session["BranchCode"].ToString();

                    if (hdnBrnCurrentStateCode.Value != "")
                        ddlStateReadOnly.SelectedValue = hdnBrnCurrentStateCode.Value;

                    if (hdnBrnCurrentReportingStateCode.Value != "")
                        ddlRptStateReadOnly.SelectedValue = hdnBrnCurrentReportingStateCode.Value;

                    //if (hdnBrnCurrentClassification.Value != "")
                    //    ddlClassification.SelectedValue = hdnBrnCurrentClassification.Value;
                }
                else if (BranchFormView.CurrentMode == FormViewMode.Insert)
                {

                    DropDownList ddlStateReadOnly = (DropDownList)BranchFormView.FindControl("ddlState");
                    BindState(ddlStateReadOnly);

                    DropDownList ddlReportingSateReadOnly = (DropDownList)BranchFormView.FindControl("ddlReportingSate");
                    BindState(ddlReportingSateReadOnly);

                    DropDownList ddlClassification = (DropDownList)BranchFormView.FindControl("ddlClassification");
                    BindClassification(ddlClassification);

                    PopulateOSLocking();
                }
                else if (BranchFormView.CurrentMode == FormViewMode.Edit)
                {
                    DropDownList ddlBranchReadOnly = (DropDownList)BranchFormView.FindControl("ddlBranch");
                    BindBranchName(ddlBranchReadOnly);

                    DropDownList ddlStateReadOnly = (DropDownList)BranchFormView.FindControl("ddlState");
                    BindState(ddlStateReadOnly);

                    DropDownList ddlReportingSateReadOnly = (DropDownList)BranchFormView.FindControl("ddlReportingSate");
                    BindState(ddlReportingSateReadOnly);

                    DropDownList ddlClassification = (DropDownList)BranchFormView.FindControl("ddlClassification");
                    BindClassification(ddlClassification);

                    PopulateOSLocking();

                    if (hdnBrnCurrentBranchCode.Value != "")
                        ddlBranchReadOnly.SelectedValue = hdnBrnCurrentBranchCode.Value;
                    else
                        ddlBranchReadOnly.SelectedValue = Session["BranchCode"].ToString();

                    if (hdnBrnCurrentStateCode.Value != "")
                        ddlStateReadOnly.SelectedValue = hdnBrnCurrentStateCode.Value;

                    if (hdnBrnCurrentReportingStateCode.Value != "")
                        ddlReportingSateReadOnly.SelectedValue = hdnBrnCurrentReportingStateCode.Value;

                    if (hdnBrnCurrentClassification.Value != "")
                        ddlClassification.SelectedValue = hdnBrnCurrentClassification.Value;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);      
            }
        }
         #endregion

        #region priavte methods

        private void BindBranchName(DropDownList ddlBranchReadOnly)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {                   
                    obj.DataObjectTypeName = "IMPALLibrary.Branch";
                    obj.TypeName = "IMPALLibrary.Branches";
                    obj.SelectMethod = "GetAllBranch";                    
                    obj.DataBind();
                    ddlBranchReadOnly.DataSource = obj;
                    ddlBranchReadOnly.DataTextField = "BranchName";
                    ddlBranchReadOnly.DataValueField = "BranchCode";
                    ddlBranchReadOnly.DataBind();
                    //AddSelect(ddlBranchReadOnly);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);      
            }
        }

        private void BindBranchDetails(string strBranchCode1)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //IMPALLibrary.Branch_Master objbm = new Branch_Master();
            //BranchMasterItems objMBItems = objbm.GetBranchDetails(strBranchCode1);
            //if (objMBItems != null)
            //{
            //    hdnBrnCurrentReportingStateCode.Value = objMBItems.Reporting_State_Code;
            //    hdnBrnCurrentStateCode.Value = objMBItems.State;
            //    hdnBrnCurrentBranchCode.Value = objMBItems.Branch_Code;
            //    hdnBrnCurrentClassification.Value = objMBItems.Classification_Code;
            //}
            //BranchFormView.DataSource = objMBItems;
            //BranchFormView.DataBind();


            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.BranchMasterItems";
                    obj.TypeName = "IMPALLibrary.Branch_Master";
                    obj.SelectMethod = "GetBranchDetails";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode1);
                    obj.DataBind();

                    BranchMasterItems objSection = new BranchMasterItems();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (BranchMasterItems)objCustomerSections[0];
                    if ((objSection.Reporting_State_Code is object))
                    {
                        hdnBrnCurrentReportingStateCode.Value = objSection.Reporting_State_Code;
                    }
                    if ((objSection.Reporting_State_Code is object))
                    {
                        hdnBrnCurrentStateCode.Value = objSection.State;
                    }

                    if ((objSection.Branch_Code is object))
                    {
                        hdnBrnCurrentBranchCode.Value = objSection.Branch_Code;
                    }

                    if ((objSection.Classification_Code is object))
                    {
                        hdnBrnCurrentClassification.Value = objSection.Classification_Code;
                        BranchFormView.DataSource = obj;
                        BranchFormView.DataBind();
                    }


                }

               
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindState(DropDownList ddlState)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.StateMaster";
                    obj.TypeName = "IMPALLibrary.StateMasters";
                    obj.SelectMethod = "GetAllStates";
                    obj.DataBind();
                    ddlState.DataSource = obj;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateCode";
                    ddlState.DataBind();
                    AddSelect(ddlState);                    
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);      
            }
        }

        private void BindClassification(DropDownList ddlClassification)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.BranchClassification";
                    obj.TypeName = "IMPALLibrary.BranchClassifications";
                    obj.SelectMethod = "GetAllBRClassifications";
                    obj.DataBind();
                    ddlClassification.DataSource = obj;
                    ddlClassification.DataTextField = "ClassificationDescription";
                    ddlClassification.DataValueField = "ClassificationCode";
                    ddlClassification.DataBind();
                    AddSelect(ddlClassification);
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

        private void ClearControls()
        {
            if (BranchFormView.CurrentMode == FormViewMode.Insert)
            {
                ((TextBox)BranchFormView.FindControl("txtBranchCode")).Text = "";
                ((TextBox)BranchFormView.FindControl("txtBranchName")).Text = "";
            }
            else if (BranchFormView.CurrentMode == FormViewMode.Edit)
            {
                ((DropDownList)BranchFormView.FindControl("ddlBranch")).SelectedIndex = 0;
                ((TextBox)BranchFormView.FindControl("txtBranchName")).Text = "";
            }
            else
            {
                ((DropDownList)BranchFormView.FindControl("ddlBranch")).SelectedIndex = 0;
                ((TextBox)BranchFormView.FindControl("txtName")).Text = "";
            }
            ((TextBox)BranchFormView.FindControl("txtAddress")).Text = "";
            ((DropDownList)BranchFormView.FindControl("ddlState")).SelectedIndex = 0;
            ((TextBox)BranchFormView.FindControl("txtPhone")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtFax")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtTelex")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtEmailid")).Text = "";
            if (BranchFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                ((TextBox)BranchFormView.FindControl("txtClassification")).Text = "";
                ((DropDownList)BranchFormView.FindControl("ddlRptingState")).SelectedIndex = 0;
            }
            else
            {
                ((DropDownList)BranchFormView.FindControl("ddlClassification")).SelectedIndex = 0;
                ((DropDownList)BranchFormView.FindControl("ddlReportingSate")).SelectedIndex = 0;
            }

            ((TextBox)BranchFormView.FindControl("txtBranchManager")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtlblAccountsManager_Officer")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtEDPin_charge")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtOpeningDate")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtArea_sq_feet")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtMonthlyRent")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtRentalAdvance")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtContractStartDate")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtContractCompletionDate")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtNoticePeriod")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtDestination")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtPreferredCarrier")).Text = "";
            ((CheckBox)BranchFormView.FindControl("chkRoadPermitApplicable")).Checked = false;
            if (BranchFormView.CurrentMode == FormViewMode.ReadOnly)
                ((TextBox)BranchFormView.FindControl("txtStatus")).Text = "";
            else
            ((DropDownList)BranchFormView.FindControl("ddlStatus")).SelectedIndex = 0;

            ((TextBox)BranchFormView.FindControl("txtCloseDate")).Text = "";
            ((DropDownList)BranchFormView.FindControl("ddlOSLocking")).SelectedIndex = 0;
            ((TextBox)BranchFormView.FindControl("txtO_SCalculationDays")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtStockAging")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtStockValue")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtLocalST_RCNo")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtCentralSalesTaxNumber")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtCESSNumber")).Text = "";
            ((TextBox)BranchFormView.FindControl("txtTINNumber")).Text = "";
        }

        #endregion

        #region Populate OS Locking
        public void PopulateOSLocking()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateOSLocking", "Entering PopulateOSLocking");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("OSLocking");
                ((DropDownList)BranchFormView.FindControl("ddlOSLocking")).DataSource = oList;
                ((DropDownList)BranchFormView.FindControl("ddlOSLocking")).DataValueField = "DisplayValue";
                ((DropDownList)BranchFormView.FindControl("ddlOSLocking")).DataTextField = "DisplayText";
                ((DropDownList)BranchFormView.FindControl("ddlOSLocking")).DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

    }
}
