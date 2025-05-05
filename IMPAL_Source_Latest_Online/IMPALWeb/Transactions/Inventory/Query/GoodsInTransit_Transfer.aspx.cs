using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
 

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class GoodsInTransit_Transfer : System.Web.UI.Page
    {

        #region Event Medthods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindSTDN();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);                 
            }
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSTDN.SelectedValue = "0";
                BindGridItems();
                lblNoRecord.Text = "";
                ddlSTDN.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
            }
        }



        protected void ddlSTDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitTransferSTDN";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitTransfer";
                    obj.SelectMethod = "GetSTDNHeader";
                    obj.SelectParameters.Add("strSTDN_Number", ddlSTDN.SelectedValue);
                    obj.DataBind();

                    GoodsInTransitTransferSTDN objSection = new GoodsInTransitTransferSTDN();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (GoodsInTransitTransferSTDN)objCustomerSections[0];
                    if ((objSection.STDNNumber is object))
                    {
                        txtFromBranch.Text = objSection.FromBranch.ToString();
                        txtToBranch.Text = objSection.ToBranch.ToString();
                        txtSTDNDate.Text = objSection.STDNDate.ToString();
                        txtLRDate.Text = objSection.LRDate.ToString();
                        txtLRNumber.Text = objSection.LRNumber.ToString();
                        ddlSTDN.SelectedValue = objSection.STDNNumber.ToString();
                    }

                }
                BindGridItems();


            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
            }
        }

        protected void grdGoodsInTransitTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdGoodsInTransitTransfer.PageIndex = e.NewPageIndex;
                BindGridItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
            }

        }
         #endregion

        #region User Medthods
        private void BindSTDN()
        {
            try
            {                

                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitTransferSTDNNumber";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitTransfer";
                    obj.SelectMethod = "GetTransferSTDNNumber";
                    obj.DataBind();
                    ddlSTDN.DataSource = obj;
                    ddlSTDN.DataTextField = "STDNNumber";
                    ddlSTDN.DataValueField = "STDNNumber";
                    ddlSTDN.DataBind();
                    AddSelect(ddlSTDN);
                    ddlSTDN.Focus();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
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
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
            }
           
        }
 
        private void BindGridItems()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitSupplierCount";
                    obj.TypeName = "IMPALLibrary.GoodsInTransitTransfer";
                    obj.SelectMethod = "GetTransferItemCount";
                    obj.SelectParameters.Add("strSTDN_Number", ddlSTDN.SelectedValue);
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
                        obj1.DataObjectTypeName = "IMPALLibrary.GoodsInTransitTransferItemDetail";
                        obj1.TypeName = "IMPALLibrary.GoodsInTransitTransfer";
                        obj1.SelectMethod = "GetTransferItemDetails";
                        obj1.SelectParameters.Add("strSTDN_Number", ddlSTDN.SelectedValue);
                        obj1.DataBind();

                        grdGoodsInTransitTransfer.DataSource = obj1;
                        grdGoodsInTransitTransfer.DataBind();

                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(GoodsInTransit_Transfer), exp);   
            }

        }
        #endregion
  
    }
}
