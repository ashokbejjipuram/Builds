using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class GoodsInTransit_Inward : System.Web.UI.Page
    {
        #region Event Medthods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindInwardNumber();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);                     
            }
        }


        protected void ddlInwardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

            //GetGoodsInTransitInward(strInwardNumber)

            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitInward";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitInwards";
                    obj.SelectMethod = "GetGoodsInTransitInward";
                    obj.SelectParameters.Add("strInwardNumber", ddlInwardNumber.SelectedValue);
                    obj.DataBind();

                    GoodsInTransitInward objSection = new GoodsInTransitInward();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (GoodsInTransitInward)objCustomerSections[0];
                    if ((objSection.InwardNumber is object))
                    {
                        txtBranchCode.Text = objSection.BranchCode;
                        txtCarrier.Text = objSection.Carrier;
                        txtInwardDate.Text = objSection.InwardDate;
                        txtLRDate.Text = objSection.LRDate;
                        txtLRNumber.Text = objSection.LRNumber;
                        txtLRDate.Text = objSection.LRDate;
                        txtPlaceOfDespatch.Text = objSection.PlaceOfDespatch;
                        txtSupplier.Text = objSection.Supplier;
                    }

                }
                BindGridItems();


            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
            }

        }

        protected void grdGoodsInTransitInward_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdGoodsInTransitInward.PageIndex = e.NewPageIndex;
                BindGridItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSupplier.Text = "";
                txtPlaceOfDespatch.Text = "";
                txtLRNumber.Text = "";
                txtLRDate.Text = "";
                txtInwardDate.Text = "";
                txtCarrier.Text = "";
                txtBranchCode.Text = "";
                ddlInwardNumber.SelectedValue = "0";
                BindGridItems();
                ddlInwardNumber.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
            }

        }
         #endregion
     
        #region User Medthods
        private void BindInwardNumber()
        {
            try
            {                

                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.InwardNumber";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitInwards";
                    obj.SelectMethod = "GetInwardNumber";
                    obj.DataBind();
                    ddlInwardNumber.DataSource = obj;
                    ddlInwardNumber.DataTextField = "Inward_Number";
                    ddlInwardNumber.DataValueField = "Inward_Number";
                    ddlInwardNumber.DataBind();
                    AddSelect(ddlInwardNumber);
                    ddlInwardNumber.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
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
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
            }
          
        }



        private void BindGridItems()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitInwardItemDetail";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitInwards";
                    obj.SelectMethod = "GetGoodsInTransitInwardItemDetails";
                    obj.SelectParameters.Add("strInwardNumber", ddlInwardNumber.SelectedValue);
                    obj.DataBind();

                    grdGoodsInTransitInward.DataSource = obj;
                    grdGoodsInTransitInward.DataBind();

                }

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Inward), exp);   
            }
        
        }
     
        #endregion
         
    }
}
