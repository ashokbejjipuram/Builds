using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class GoodsInTransit_Supplier : System.Web.UI.Page
    {
        #region Event Medthods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindSupplier();
                    ListItem li = new ListItem();
                    li.Text = Session["BranchCode"].ToString();
                    li.Value = Session["BranchCode"].ToString();
                    ddlBranches.Items.Insert(0, li);
                    ddlBranches.SelectedValue = "0";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);                    
            }
          
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSupplier.SelectedValue = "0";
                BindGridItems();
                lblNoRecord.Text = "";
                ddlSupplier.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);  
            }

        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridItems();

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);  
            }
        }

        protected void grdGoodsInTransitSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdGoodsInTransitSupplier.PageIndex = e.NewPageIndex;
                BindGridItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);  
            }

        }

        #endregion

        #region User Medthods
        private void BindSupplier()
        {
            try
            {                

                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.Supplier";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitSupplier";
                    obj.SelectMethod = "GetSupplier";
                    obj.DataBind();
                    ddlSupplier.DataSource = obj;
                    ddlSupplier.DataTextField = "SupplierName";
                    ddlSupplier.DataValueField = "SupplierCode";
                    ddlSupplier.DataBind();
                    AddSelect(ddlSupplier);
                    ddlSupplier.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);  
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
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);           
            }           
        }
 
        private void BindGridItems()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitSupplierCount";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitSupplier";
                    obj.SelectMethod = "GetSupplierItemCount";
                    obj.SelectParameters.Add("strSupplierCode", ddlSupplier.SelectedValue);
                    obj.DataBind();

                    GoodsInTransitSupplierCount objSection = new GoodsInTransitSupplierCount();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (GoodsInTransitSupplierCount)objCustomerSections[0];

                    if (objSection.RowCount > 0)
                    {
                        lblNoRecord.Text = "";
                    }
                    else
                    {
                        lblNoRecord.Text = "No records to query";
                    }

                    using (ObjectDataSource obj1 = new ObjectDataSource())
                    {
                        obj1.DataObjectTypeName = "IMPALLibrary.GoodsInTransitSupplierItemDetail";
                        obj1.TypeName = "IMPALLibrary.GoodsInTransitSupplier";
                        obj1.SelectMethod = "GetSupplierItemDetails";
                        obj1.SelectParameters.Add("strSupplierCode", ddlSupplier.SelectedValue);
                        obj1.DataBind();

                        grdGoodsInTransitSupplier.DataSource = obj1;
                        grdGoodsInTransitSupplier.DataBind();
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Supplier), exp);  
            }

        }

        #endregion

    }
}
