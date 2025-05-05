using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;

namespace IMPALWeb.Masters.SalesTax
{
    public partial class BranchProductAndPurchaseTax : System.Web.UI.Page
    {
        #region Event methods
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    BindGVBranchPurchaseTax();
 
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);   
            }
           
        } 

        protected void GV_BrnProductPurchaseTax_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GV_BrnProductPurchaseTax.PageIndex = e.NewPageIndex;
                BindGVBranchPurchaseTax();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }

        protected void GV_BrnProductPurchaseTax_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
              
                GV_BrnProductPurchaseTax.EditIndex = -1;
                GV_BrnProductPurchaseTax.ShowFooter = true;
                BindGVBranchPurchaseTax();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }
                  
        protected void GV_BrnProductPurchaseTax_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var ddlBranch = (DropDownList)e.Row.FindControl("ddlBranch");

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                        objBranch.TypeName = "IMPALLibrary.Branches";
                        objBranch.SelectMethod = "GetAllBranch";
                        objBranch.DataBind();
                        ddlBranch.DataSource = objBranch;
                        ddlBranch.DataTextField = "BranchName";
                        ddlBranch.DataValueField = "BranchCode";
                        ddlBranch.DataBind();
                        AddSelect(ddlBranch);                       
                    }

                    var ddlProduct = (DropDownList)e.Row.FindControl("ddlProduct");

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.ProductGroup";
                        objBranch.TypeName = "IMPALLibrary.ProductGroups";
                        objBranch.SelectMethod = "GetAllProductGroups";
                        objBranch.DataBind();
                        ddlProduct.DataSource = objBranch;
                        ddlProduct.DataTextField = "ProductGroupDescription";
                        ddlProduct.DataValueField = "ProductGroupCode";
                        ddlProduct.DataBind();
                        AddSelect(ddlProduct);                         
                    }
                    

                    var ddlSalesTax = (DropDownList)e.Row.FindControl("ddlSalesTax");

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.SalesTax";
                        objBranch.TypeName = "IMPALLibrary.SalesTaxes";
                        objBranch.SelectMethod = "GetAllSalesTexes";
                        objBranch.DataBind();
                        ddlSalesTax.DataSource = objBranch;
                        ddlSalesTax.DataTextField = "SalesTaxDescription";
                        ddlSalesTax.DataValueField = "SalesTaxCode";
                        ddlSalesTax.DataBind();
                        AddSelect(ddlSalesTax);
                    }

                    var ddlPartType = (DropDownList)e.Row.FindControl("ddlPartType");

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Masters.CustomerDetails.CustomerType";
                        objBranch.TypeName = "IMPALLibrary.Masters.CustomerDetails.CustomerDetails";
                        objBranch.SelectMethod = "GetAllCustomerType";
                        objBranch.DataBind();
                        ddlPartType.DataSource = objBranch;
                        ddlPartType.DataTextField = "CustomerTypeDesc";
                        ddlPartType.DataValueField = "CustomerTypeCode";
                        ddlPartType.DataBind();
                        AddSelect(ddlPartType);
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {                   
                    var ddlBranch = (DropDownList)e.Row.FindControl("ddlBranch");  
                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                        objBranch.TypeName = "IMPALLibrary.Branches";
                        objBranch.SelectMethod = "GetAllBranch";
                        objBranch.DataBind();
                        ddlBranch.DataSource = objBranch;
                        ddlBranch.DataTextField = "BranchName";
                        ddlBranch.DataValueField = "BranchCode";
                        ddlBranch.DataBind();
                        AddSelect(ddlBranch);                        
                    }

                    var ddlProduct = (DropDownList)e.Row.FindControl("ddlProduct");  

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.ProductGroup";
                        objBranch.TypeName = "IMPALLibrary.ProductGroups";
                        objBranch.SelectMethod = "GetAllProductGroups";
                        objBranch.DataBind();
                        ddlProduct.DataSource = objBranch;
                        ddlProduct.DataTextField = "ProductGroupDescription";
                        ddlProduct.DataValueField = "ProductGroupCode";
                        ddlProduct.DataBind();
                        AddSelect(ddlProduct);
                    }


                    var ddlSalesTax = (DropDownList)e.Row.FindControl("ddlSalesTax");  

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.SalesTax";
                        objBranch.TypeName = "IMPALLibrary.SalesTaxes";
                        objBranch.SelectMethod = "GetAllSalesTexes";
                        objBranch.DataBind();
                        ddlSalesTax.DataSource = objBranch;
                        ddlSalesTax.DataTextField = "SalesTaxDescription";
                        ddlSalesTax.DataValueField = "SalesTaxCode";
                        ddlSalesTax.DataBind();
                        AddSelect(ddlSalesTax);
                    }

                    var ddlPartType = (DropDownList)e.Row.FindControl("ddlPartType");                       

                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Masters.CustomerDetails.CustomerType";
                        objBranch.TypeName = "IMPALLibrary.Masters.CustomerDetails.CustomerDetails";
                        objBranch.SelectMethod = "GetAllCustomerType";
                        objBranch.DataBind();
                        ddlPartType.DataSource = objBranch;
                        ddlPartType.DataTextField = "CustomerTypeDesc";
                        ddlPartType.DataValueField = "CustomerTypeCode";
                        ddlPartType.DataBind();
                        AddSelect(ddlPartType);
                    }
                    BranchProductPurchaseTaxItems objSection = new BranchProductPurchaseTaxItems();
                    BranchProduct_PurchaseTax objDBIns = new BranchProduct_PurchaseTax();

                    var lblSerialNumber = (Label)e.Row.FindControl("lblSerialNumber");
                    objSection = objDBIns.getBranchProductPurchaseTaxSerialNo(lblSerialNumber.Text.Trim());

                    var ddl = (DropDownList)e.Row.FindControl("ddlBranch"); 
                    ddl.SelectedValue = objSection.GVBranchCode;

                    ddl = (DropDownList)e.Row.FindControl("ddlProduct"); 
                    ddl.SelectedValue = objSection.Product_Group_Code;

                    ddl = (DropDownList)e.Row.FindControl("ddlSalesTax"); 
                    ddl.SelectedValue = objSection.Sales_Tax_Code;

                    ddl = (DropDownList)e.Row.FindControl("ddlOSLS");
                    ddl.SelectedValue = objSection.OS_LS_Indicator;

                    ddl = (DropDownList)e.Row.FindControl("ddlPartType"); 
                    ddl.SelectedValue = objSection.Party_Type_Code;

                    ddl = (DropDownList)e.Row.FindControl("ddlStatus");
                    ddl.SelectedValue = objSection.Status;
                     
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }
           
        protected void GV_BrnProductPurchaseTax_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
           try
           {
               GV_BrnProductPurchaseTax.ShowFooter = false;
               GV_BrnProductPurchaseTax.EditIndex = e.NewEditIndex;
               BindGVBranchPurchaseTax();
           }
           catch (Exception exp)
           {
               Log.WriteException(Source, exp); 
           }
            
        }

        protected void GV_BrnProductPurchaseTax_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;
            try
            {
                GridViewRow row = GV_BrnProductPurchaseTax.Rows[e.RowIndex];
                BranchProductPurchaseTaxItems objInsVlaue = new BranchProductPurchaseTaxItems();

                objInsVlaue.SerialNumber = ((Label)row.Cells[1].FindControl("lblSerialNumber")).Text;
                objInsVlaue.GVBranchCode = ((DropDownList)row.Cells[1].FindControl("ddlBranch")).SelectedValue;
                objInsVlaue.Product_Group_Code = ((DropDownList)row.Cells[2].FindControl("ddlProduct")).SelectedValue;
                objInsVlaue.Sales_Tax_Code = ((DropDownList)row.Cells[3].FindControl("ddlSalesTax")).SelectedValue;
                // objInsVlaue.Sales_Tax_Percentage = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtPercentage")).Text;
                //  objInsVlaue.Sales_Tax_Indicator = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtIndicator")).Text;
                objInsVlaue.OS_LS_Indicator = ((DropDownList)row.Cells[6].FindControl("ddlOSLS")).SelectedValue;
                

                if (((DropDownList)row.Cells[7].FindControl("ddlPartType")).SelectedValue == "0")
                {
                    objInsVlaue.Party_Type_Code = "";
                }
                else
                {
                    objInsVlaue.Party_Type_Code = ((DropDownList)row.Cells[7].FindControl("ddlPartType")).SelectedValue;
                }

                objInsVlaue.Sales_Tax_Text = ((TextBox)row.Cells[8].FindControl("txtSaleTaxText")).Text;
                objInsVlaue.Form_Name_Text = ((TextBox)row.Cells[9].FindControl("txtFormNameText")).Text;
                objInsVlaue.Status = ((DropDownList)row.Cells[10].FindControl("ddlStatus")).SelectedValue;

                BranchProduct_PurchaseTax objDBIns = new BranchProduct_PurchaseTax();
               
                iRowCount = objDBIns.UpdBranchProductPurchaseTax(objInsVlaue);
                if (iRowCount >= 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Records updated Sucessfully.');", true);
                }
               
                GV_BrnProductPurchaseTax.EditIndex = -1;
                GV_BrnProductPurchaseTax.ShowFooter = true;
                BindGVBranchPurchaseTax();
                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }

        protected void ddlSalesTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                DropDownList strSaleTax = null;
                if (row.RowType == DataControlRowType.Footer)
                {
                    strSaleTax = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlSalesTax"));
                }
                else if (row.RowType == DataControlRowType.DataRow)
                {
                    strSaleTax = (DropDownList)GV_BrnProductPurchaseTax.Rows[row.RowIndex].FindControl("ddlSalesTax");
                }

                using (ObjectDataSource objBranch = new ObjectDataSource())
                {
                    objBranch.DataObjectTypeName = "IMPALLibrary.BranchProductPurchaseTaxItems";
                    objBranch.TypeName = "IMPALLibrary.BranchProduct_PurchaseTax";
                    objBranch.SelectMethod = "getPurchaseSalesIndicatorAndPercentage";
                    objBranch.SelectParameters.Add("strSaleTaxCode", strSaleTax.SelectedValue);
                    objBranch.DataBind();

                    BranchProductPurchaseTaxItems objSection = new BranchProductPurchaseTaxItems();
                    object[] objInsSections = new object[0];
                    objInsSections = (object[])objBranch.Select();
                    objSection = (BranchProductPurchaseTaxItems)objInsSections[0];

                    foreach (GridViewRow gvr in GV_BrnProductPurchaseTax.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow && (gvr.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                        {
                            var txtEditPercentage = ((TextBox)GV_BrnProductPurchaseTax.Rows[row.RowIndex].FindControl("txtPercentage"));
                            txtEditPercentage.Text = objSection.Sales_Tax_Percentage.ToString();

                            var txtEditIndicator = ((TextBox)GV_BrnProductPurchaseTax.Rows[row.RowIndex].FindControl("txtIndicator"));
                            txtEditIndicator.Text = objSection.Sales_Tax_Indicator.ToString();
                        }
                    }

                    if (row.RowType == DataControlRowType.Footer)
                    {
                        var txtPercentage = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtPercentage"));
                        txtPercentage.Text = objSection.Sales_Tax_Percentage.ToString();

                        var txtIndicator = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtIndicator"));
                        txtIndicator.Text = objSection.Sales_Tax_Indicator.ToString();

                    }

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
            int iRowCount = -1;
            try
            {
                BranchProductPurchaseTaxItems objInsVlaue = new BranchProductPurchaseTaxItems();

                objInsVlaue.GVBranchCode = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlBranch")).SelectedValue;
                objInsVlaue.Product_Group_Code = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlProduct")).SelectedValue;
                objInsVlaue.Sales_Tax_Code = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlSalesTax")).SelectedValue;
                // objInsVlaue.Sales_Tax_Percentage = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtPercentage")).Text;
                //  objInsVlaue.Sales_Tax_Indicator = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtIndicator")).Text;
                objInsVlaue.OS_LS_Indicator = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlOSLS")).SelectedValue;

                if (((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlPartType")).SelectedValue == "0")
                {
                    objInsVlaue.Party_Type_Code = "";
                }
                else
                {
                    objInsVlaue.Party_Type_Code = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlPartType")).SelectedValue;
                }


                
                objInsVlaue.Sales_Tax_Text = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtSaleTaxText")).Text;
                objInsVlaue.Form_Name_Text = ((TextBox)GV_BrnProductPurchaseTax.FooterRow.FindControl("txtFormNameText")).Text;
                objInsVlaue.Status = ((DropDownList)GV_BrnProductPurchaseTax.FooterRow.FindControl("ddlStatus")).SelectedValue;



                BranchProduct_PurchaseTax objDBIns = new BranchProduct_PurchaseTax();
                iRowCount = objDBIns.PurchaseCheckDuplicate(objInsVlaue);
                if (iRowCount == 0)
                {
                    iRowCount = -1;
                    iRowCount = objDBIns.InsBranchProductPurchaseTax(objInsVlaue);
                    if (iRowCount >= 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Records inserted Sucessfully.');", true);
                        BindGVBranchPurchaseTax();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Data already exists.');", true);
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
               // Response.Redirect("BranchProductAndPurchaseTaxReport.aspx");
                Server.Execute("BranchProductAndPurchaseTaxReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }

        }

        #endregion

        #region User methods

        private void BindGVBranchPurchaseTax()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strBranchCode = Session["BranchCode"].ToString();
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.BranchProductPurchaseTaxItems";
                    obj.TypeName = "IMPALLibrary.BranchProduct_PurchaseTax";
                    obj.SelectMethod = "GetBranchProductPurchaseTax";
                    // obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                    {
                        GV_BrnProductPurchaseTax.ShowFooter = false;
                        GV_BrnProductPurchaseTax.Columns[11].Visible = false;
                        GV_BrnProductPurchaseTax.Columns[12].Visible = false;
                        GV_BrnProductPurchaseTax.DataSource = obj;
                        GV_BrnProductPurchaseTax.DataBind();
                    }
                    else
                    {
                        GV_BrnProductPurchaseTax.DataSource = obj;
                        GV_BrnProductPurchaseTax.DataBind();
                    }
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

        #endregion
    }
}
