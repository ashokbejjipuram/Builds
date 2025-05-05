using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class ItemType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_ItemType.ShowFooter = false;
                int EditButtonIndex = GV_ItemType.Columns.Count - 1;
                GV_ItemType.Columns[EditButtonIndex].Visible = false;
            }
        }



        protected void GV_ItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_ItemType_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_ItemType_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_ItemType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataIT.Insert();

        }

        protected void ODSGV_ItemType_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox ItemTypeDesc = (TextBox)GV_ItemType.FooterRow.FindControl("txtNewItemTypeDescription");

            e.InputParameters["ItemTypeDescription"] = ItemTypeDesc.Text;
        }

        protected void ODSItemType_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_ItemType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ItemTypeReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
