using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;


namespace IMPALWeb
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Product.ShowFooter = false;
                int EditButtonIndex = GV_Product.Columns.Count - 1;
                GV_Product.Columns[EditButtonIndex].Visible = false;
            }
        }
        protected void GV_Product_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Product_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }

        protected void GV_Product_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Product_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                Button btn = (Button)e.CommandSource;
                Products pds = new Products();
                DropDownList ddl = (DropDownList)btn.FindControl("ddlSupplierCode");
                TextBox txt = (TextBox)btn.FindControl("txtNewSPCode");
                if (pds.FindExists(ddl.SelectedValue,txt.Text.Trim()))
                {                 
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Product Code Already Exists');", true);
                }
                else{
                    ObjectDataSP.Insert();
                }

               
                
            }

        }

        protected void ODSSP_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox prodCode = (TextBox)GV_Product.FooterRow.FindControl("txtNewSPCode");
            TextBox ShortName = (TextBox)GV_Product.FooterRow.FindControl("txtNewShortName");
            DropDownList SuppCode = (DropDownList)GV_Product.FooterRow.FindControl("ddlSupplierCode");
            TextBox LongName = (TextBox)GV_Product.FooterRow.FindControl("txtNewLongName");
            //TextBox eDate = (TextBox)GV_AM.FooterRow.FindControl("txtEDate");
            //TextBox PMName = (TextBox)GV_AM.FooterRow.FindControl("txtPreviousManager");
            //TextBox PMsDate = (TextBox)GV_AM.FooterRow.FindControl("txtPMStDate");
            //TextBox PMeDate = (TextBox)GV_AM.FooterRow.FindControl("txtPMeDate");

            e.InputParameters["SupplierCode"] = SuppCode.SelectedValue;
            e.InputParameters["SupplierProductCode"] = prodCode.Text;
            e.InputParameters["SupplierProductShortName"] = ShortName.Text;
            e.InputParameters["SupplierProductName"] = LongName.Text;
            //e.InputParameters["EndDate"] = eDate.Text;
            //e.InputParameters["PreviousManager"] = PMName.Text;
            //e.InputParameters["PreviousManagerStartDate"] = PMsDate.Text;
            //e.InputParameters["PreviousManagerEndDate"] = PMeDate.Text;

        }



        protected void ODSSP_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //TextBox prodCode = (TextBox)GV_Product.FooterRow.FindControl("txtNewSPCode");
            //DropDownList SuppCode = (DropDownList)GV_Product.FooterRow.FindControl("ddlSupplierCode");

            //TextBox ShortName = (TextBox)GV_Product.FooterRow.FindControl("txtShortName");
            //TextBox LongName = (TextBox)GV_Product.FooterRow.FindControl("txtLongName");

            //e.InputParameters["SupplierCode"] = SuppCode.SelectedValue;
            //e.InputParameters["SupplierProductCode"] = prodCode.Text;
            //e.InputParameters["SupplierProductShortName"] = ShortName.Text;
            //e.InputParameters["SupplierProductName"] = LongName.Text;
        }

        protected void GV_Product_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ProductReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
