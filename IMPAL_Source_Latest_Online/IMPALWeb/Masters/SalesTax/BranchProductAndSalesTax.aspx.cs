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
    public partial class BranchProductAndSalesTax : System.Web.UI.Page
    {
        #region Event methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    BindGVBranchSalesTax();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
            
        }

        protected void GV_BrnProductSalesTax_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GV_BrnProductSalesTax.PageIndex = e.NewPageIndex;
                BindGVBranchSalesTax();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }
         
        protected void GV_BrnProductSalesTax_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GV_BrnProductSalesTax.EditIndex = -1;
                GV_BrnProductSalesTax.ShowFooter = true; 
                BindGVBranchSalesTax();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp); 
            }
        }
          
        protected void GV_BrnProductSalesTax_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    BranchProductSalesTaxItems objSection = new BranchProductSalesTaxItems();
                    BranchProduct_SalesTax objDBIns = new BranchProduct_SalesTax();

                    var lblSerialNumber = (Label)e.Row.FindControl("lblSerialNumber");
                    objSection = objDBIns.getBranchProductSalesTaxSerialNo(lblSerialNumber.Text.Trim());

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
 
        protected void GV_BrnProductSalesTax_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
           try
           {
               GV_BrnProductSalesTax.ShowFooter = false;               
               GV_BrnProductSalesTax.EditIndex = e.NewEditIndex;
               BindGVBranchSalesTax();
           }
           catch (Exception exp)
           {
               Log.WriteException(Source, exp); 
           }
            
        }

        protected void GV_BrnProductSalesTax_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;
            try
            {
                GridViewRow row = GV_BrnProductSalesTax.Rows[e.RowIndex];              
                BranchProductSalesTaxItems objInsVlaue = new BranchProductSalesTaxItems();

                objInsVlaue.SerialNumber = ((Label)row.Cells[1].FindControl("lblSerialNumber")).Text;
                objInsVlaue.GVBranchCode = ((DropDownList)row.Cells[1].FindControl("ddlBranch")).SelectedValue;
                objInsVlaue.Product_Group_Code = ((DropDownList)row.Cells[2].FindControl("ddlProduct")).SelectedValue;
                objInsVlaue.Sales_Tax_Code = ((DropDownList)row.Cells[3].FindControl("ddlSalesTax")).SelectedValue;
                // objInsVlaue.Sales_Tax_Percentage = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtPercentage")).Text;
                //  objInsVlaue.Sales_Tax_Indicator = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtIndicator")).Text;
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
                 
                BranchProduct_SalesTax objDBIns = new BranchProduct_SalesTax();
               
                iRowCount = objDBIns.UpdBranchProductSalesTax(objInsVlaue);
                if (iRowCount >= 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Records updated Sucessfully.');", true);
                }
                               
                GV_BrnProductSalesTax.EditIndex = -1;
                GV_BrnProductSalesTax.ShowFooter = true; 
                BindGVBranchSalesTax();
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
                    strSaleTax = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlSalesTax"));
                }
                else if (row.RowType == DataControlRowType.DataRow)
                {
                    strSaleTax = (DropDownList)GV_BrnProductSalesTax.Rows[row.RowIndex].FindControl("ddlSalesTax");
                }

                using (ObjectDataSource objBranch = new ObjectDataSource())
                {
                    objBranch.DataObjectTypeName = "IMPALLibrary.BranchProductSalesTaxItems";
                    objBranch.TypeName = "IMPALLibrary.BranchProduct_SalesTax";
                    objBranch.SelectMethod = "getSalesIndicatorAndPercentage";
                    objBranch.SelectParameters.Add("strSaleTaxCode", strSaleTax.SelectedValue);
                    objBranch.DataBind();

                    BranchProductSalesTaxItems objSection = new BranchProductSalesTaxItems();
                    object[] objInsSections = new object[0];
                    objInsSections = (object[])objBranch.Select();
                    objSection = (BranchProductSalesTaxItems)objInsSections[0];

                    foreach (GridViewRow gvr in GV_BrnProductSalesTax.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow && (gvr.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                        {
                            var txtEditPercentage = ((TextBox)GV_BrnProductSalesTax.Rows[row.RowIndex].FindControl("txtPercentage"));
                            txtEditPercentage.Text = objSection.Sales_Tax_Percentage.ToString();

                            var txtEditIndicator = ((TextBox)GV_BrnProductSalesTax.Rows[row.RowIndex].FindControl("txtIndicator"));
                            txtEditIndicator.Text = objSection.Sales_Tax_Indicator.ToString();
                        }
                    }

                    if (row.RowType == DataControlRowType.Footer)
                    {
                        var txtPercentage = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtPercentage"));
                        txtPercentage.Text = objSection.Sales_Tax_Percentage.ToString();

                        var txtIndicator = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtIndicator"));
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
                BranchProductSalesTaxItems objInsVlaue = new BranchProductSalesTaxItems();

                objInsVlaue.GVBranchCode = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlBranch")).SelectedValue;
                objInsVlaue.Product_Group_Code = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlProduct")).SelectedValue;
                objInsVlaue.Sales_Tax_Code = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlSalesTax")).SelectedValue;
                // objInsVlaue.Sales_Tax_Percentage = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtPercentage")).Text;
                //  objInsVlaue.Sales_Tax_Indicator = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtIndicator")).Text;
                objInsVlaue.OS_LS_Indicator = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlOSLS")).SelectedValue;

                if (((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlPartType")).SelectedValue == "0")
                {
                    objInsVlaue.Party_Type_Code = "";
                }
                else
                {                    
                    objInsVlaue.Party_Type_Code = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlPartType")).SelectedValue;
                }
                objInsVlaue.Sales_Tax_Text = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtSaleTaxText")).Text;
                objInsVlaue.Form_Name_Text = ((TextBox)GV_BrnProductSalesTax.FooterRow.FindControl("txtFormNameText")).Text;
                objInsVlaue.Status = ((DropDownList)GV_BrnProductSalesTax.FooterRow.FindControl("ddlStatus")).SelectedValue;



                BranchProduct_SalesTax objDBIns = new BranchProduct_SalesTax();
                iRowCount = objDBIns.CheckDuplicate(objInsVlaue);
                if (iRowCount == 0)
                {
                    iRowCount = -1;
                    iRowCount = objDBIns.InsBranchProductSalesTax(objInsVlaue);
                    if (iRowCount >= 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Records inserted Sucessfully.');", true);
                        BindGVBranchSalesTax();
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
            Server.Execute("BranchProductAndSalesTaxReport.aspx");
        }

        #endregion

        #region user defined methods
        private void BindGVBranchSalesTax()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strBranchCode = Session["BranchCode"].ToString();
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.BranchProductSalesTaxItems";
                    obj.TypeName = "IMPALLibrary.BranchProduct_SalesTax";
                    obj.SelectMethod = "GetBranchProductSalesTax";
                    // obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                    {
                        GV_BrnProductSalesTax.ShowFooter = false;
                        GV_BrnProductSalesTax.Columns[11].Visible = false;
                        GV_BrnProductSalesTax.Columns[12].Visible = false;
                        GV_BrnProductSalesTax.DataSource = obj;
                        GV_BrnProductSalesTax.DataBind();
                    }
                    else
                    {
                        GV_BrnProductSalesTax.DataSource = obj;
                        GV_BrnProductSalesTax.DataBind();
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
