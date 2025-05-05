using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory.Query
{
    public partial class ConsignmentBalance : System.Web.UI.Page
    {
        #region Event Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                if (!IsPostBack)
                {
                    BindCustomer();
                    divheading1.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);                 
            }
        }


        protected void LoadDataControl(string strPartNumber,bool strReset)
        {
            try
            {
                lblNoRecord.Text = "";
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ConsignmentBalanceDetails";
                    obj.TypeName = "IMPALLibrary.ConsignmentDetails";
                    obj.SelectMethod = "GetConsignmentBalance";
                    obj.SelectParameters.Add("strPart_Number", strPartNumber);
                    obj.SelectParameters.Add("strBranch_Code", Session["BranchCode"].ToString());
                    obj.DataBind();

                    grdConsignmentBalance.DataSource = obj;
                    grdConsignmentBalance.DataBind();

                    if (grdConsignmentBalance.Rows.Count <= 0)
                    {
                        if (strReset == false)
                        {
                            lblNoRecord.Text = "No Records found for the search condition.";
                        }

                        return;
                    }

                    using (ObjectDataSource obj1 = new ObjectDataSource())
                    {
                        obj1.DataObjectTypeName = "IMPALLibrary.ConsignmentBalanceDetails";
                        obj1.TypeName = "IMPALLibrary.ConsignmentDetails";
                        obj1.SelectMethod = "GetConsignmentBalanceAlternate";
                        obj1.SelectParameters.Add("strPart_Number", strPartNumber);
                        obj1.SelectParameters.Add("strBranch_Code", Session["BranchCode"].ToString());
                        obj1.DataBind();

                        grdConsignmentBalanceAlt.DataSource = obj1;
                        grdConsignmentBalanceAlt.DataBind();

                        if (grdConsignmentBalanceAlt.Rows.Count <= 0)
                        {
                            grdConsignmentBalanceAlt.Visible = false;
                            divheading1.Visible = false;
                        }
                        else
                        {
                            grdConsignmentBalanceAlt.Visible = true;
                            divheading1.Visible = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);
            }        
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string strPartNumber;
            try
            {
                strPartNumber = txtSupplierPartNo.Text.Trim();
                LoadDataControl(strPartNumber, false);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);    
            }
          
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("ConsignmentBalance.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);    
            }
            
        }

        protected void grdConsignmentBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string strPartNumber;
            try
            {
                grdConsignmentBalance.PageIndex = e.NewPageIndex;
                strPartNumber = txtSupplierPartNo.Text.Trim();
                LoadDataControl(strPartNumber, false);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);
            } 
        }
        #endregion

        #region User Methods
        private void BindCustomer()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlCustomerType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllCustomers";
                    obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    obj.DataBind();
                   
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ConsignmentBalance), exp);
            }
        }
        private void AddSelect(DropDownList ddl)
        {
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
                Log.WriteException(typeof(ConsignmentBalance), exp);
            }

        }

        #endregion
    }
}
