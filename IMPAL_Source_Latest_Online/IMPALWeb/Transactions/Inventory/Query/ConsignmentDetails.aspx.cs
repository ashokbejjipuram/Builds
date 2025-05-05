using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class ConsignmentDetails : System.Web.UI.Page
    {

        private string strSupplierPartNumber = "";
        private string strSuppItemCode = "";
        private string strSelectedBranchCode = "";
        private bool boolSelectedBranchCode;
        TextBox txt, txtItemCode;
        DropDownList ddl;

        #region Event Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txt = (TextBox)ucSupplierPartNumber.FindControl("txtSupplierPartNumber");
                txtItemCode = (TextBox)ucSupplierPartNumber.FindControl("txtItemCode");
                strSuppItemCode = txtItemCode.Text;
                ddl = (DropDownList)ucSupplierPartNumber.FindControl("ddlSupplierPartNumber");

                if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                {
                    ddlBranchName.Enabled = false;
                    strSelectedBranchCode = Session["BranchCode"].ToString();
                    BindBranches();
                    ddlBranchName.SelectedValue = Session["BranchCode"].ToString();
                }
                else
                {
                    ddlBranchName.Enabled = true;
                }

                if (!IsPostBack)
                {
                    txt.Visible = true;
                    ddl.Visible = false;
                    boolSelectedBranchCode = false;
                    divItemDetails.Visible = false;
                    BindBranches();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Session["UserCtrolSupplierPartNumber"] = "";
            Session["UserCtrolSupplierItemCode"] = "";
        }

        protected void ucSupplierPartNumber_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                if (ddl.Visible == true)
                {
                    strSupplierPartNumber = Session["UserCtrolSupplierPartNumber"].ToString();
                    strSuppItemCode = Session["UserCtrolSupplierItemCode"].ToString();
                }

                BindSupplierPartNumber();
                ddl.Visible = true;
                txt.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);                
            }      
        }

        protected void ucSupplierPartNumber_SupplierddlChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl.Visible == true)
                {
                    strSupplierPartNumber = Session["UserCtrolSupplierPartNumber"].ToString();
                    strSuppItemCode = Session["UserCtrolSupplierItemCode"].ToString();
                }

                boolSelectedBranchCode = true;
                strSelectedBranchCode = ddlBranchName.SelectedValue;
                BindValue("False");

                if (lblNoRecord.Text != "No records to query")
                {
                    ddl.Visible = true;
                    txt.Visible = false;
                    divItemDetails.Visible = true;
                }
                else
                {
                    divItemDetails.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }        

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("ConsignmentDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }

        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                strSelectedBranchCode = ddlBranchName.SelectedValue;
                if (ddl.Visible == true)
                    strSuppItemCode = ddl.SelectedItem.Value;
                else
                    strSuppItemCode = txt.Text.Trim();
                boolSelectedBranchCode = true;
                BindValue("False");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }           
        }
             
        
        protected void grdConsignmentDeails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                boolSelectedBranchCode = true;
                strSelectedBranchCode = ddlBranchName.SelectedValue;
                grdConsignmentDeails.PageIndex = e.NewPageIndex;
                BindValue("False");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }       
        }                
        #endregion

        #region User Medthods
        private void BindBranches()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.Branch";
                    obj.TypeName = "IMPALLibrary.Branches";
                    obj.SelectMethod = "GetAllBranch";
                    obj.DataBind();
                    ddlBranchName.DataSource = obj;
                    ddlBranchName.DataTextField = "BranchName";
                    ddlBranchName.DataValueField = "BranchCode";
                    ddlBranchName.DataBind();
                   
                    ListItem li = new ListItem();
                    li.Text = "---Select All Branches---";
                    li.Value = "0";
                    ddlBranchName.Items.Remove(li);
                    li.Text = "";
                    li.Value = "0";
                    ddlBranchName.Items.Insert(0, li);
                    ddlBranchName.SelectedValue = "0";                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }

        private void BindValue(string strReset)
        {
            try
            {               
                int checkRowCount = 0;
                lblNoRecord.Text = "";
                BindSupplierPartNumber();
                if (!boolSelectedBranchCode)
                {
                    BindBranches();
                }

                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ConsignmentHeaderSummary";
                    obj.TypeName = "IMPALLibrary.ConsignmentSummary";
                    obj.SelectMethod = "GetConsignmentHeaderCons_Details";
                    obj.SelectParameters.Add("strItem_Code", strSuppItemCode);
                    obj.SelectParameters.Add("strBranch_Code", strSelectedBranchCode);
                    obj.DataBind();

                    ConsignmentHeaderSummary objSection = new ConsignmentHeaderSummary();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (ConsignmentHeaderSummary)objCustomerSections[0];
                    if ((objSection.Item_Code is object))
                    {
                        checkRowCount = objSection.iRowCount;

                        if (checkRowCount > 0)
                        {
                            if (objSection.Branch_Code.Length > 0)
                            {
                                ddlBranchName.SelectedValue = objSection.Branch_Code;
                            }

                            if (objSection.Supplier_Part_Number.Length > 0)
                            {
                                ddl.SelectedValue = objSection.Item_Code;
                            }

                            txtItemCode.Text = objSection.Item_Code;
                            txtBalanceQuantity.Text = Math.Round(Convert.ToDouble(objSection.Balance_Quantity), 2).ToString();
                            txtShortDescription.Text = objSection.Short_Description;
                            txtLongDescription.Text = objSection.Long_Description;
                        }
                        else
                        {
                            txtItemCode.Text = "";
                            txtBalanceQuantity.Text = "";
                            txtShortDescription.Text = "";
                            txtLongDescription.Text = "";
                            lblNoRecord.Text = "No records to query";
                        }

                        using (ObjectDataSource obj1 = new ObjectDataSource())
                        {
                            obj1.DataObjectTypeName = "IMPALLibrary.ConsignmentItemSummary";
                            obj1.TypeName = "IMPALLibrary.ConsignmentSummary";
                            obj1.SelectMethod = "GetConsignmentItemDetailCons_Details";
                            obj1.SelectParameters.Add("strItem_Code", strSuppItemCode);
                            obj1.SelectParameters.Add("strBranch_Code", strSelectedBranchCode);
                            obj1.SelectParameters.Add("strResetFlag", strReset);
                            obj1.DataBind();

                            grdConsignmentDeails.DataSource = obj1;
                            grdConsignmentDeails.DataBind();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }

        protected void BindSupplierPartNumber()
        {
            try
            {
                if (txt.Visible == true)
                {
                    strSupplierPartNumber = txt.Text.Trim();

                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                        obj.TypeName = "IMPALLibrary.ConsignmentSummary";
                        obj.SelectMethod = "GetConsignmentItemCode";
                        obj.SelectParameters.Add("strPart_Number", txt.Text.Trim());
                        obj.SelectParameters.Add("strBranchCode", strSelectedBranchCode);
                        obj.DataBind();
                        ddl.DataSource = obj;
                        ddl.DataTextField = "Supplier_Part_Number";
                        ddl.DataValueField = "item_code";
                        ddl.DataBind();
                        AddSelect(ddl);
                        if (ddl.Items.Count > 0 && txt.Text.Length > 0)
                        {
                            strSuppItemCode = ddl.SelectedItem.Value;
                        }
                    }
                    ddl.Focus();
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }

        private void AddSelect(DropDownList ddl)
        {
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
                Log.WriteException(typeof(ConsignmentDetails), exp);
            }
        }
        #endregion

    }
}
