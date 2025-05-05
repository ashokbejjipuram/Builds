using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb
{
    public partial class Bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Bank.ShowFooter = false;
                int EditButtonIndex = GV_Bank.Columns.Count - 1;
                GV_Bank.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void GV_Bank_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Bank_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_Bank_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_Bank_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataBank.Insert();

        }

        protected void ODSBank_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox BankName = (TextBox)GV_Bank.FooterRow.FindControl("txtNewBankName");
            e.InputParameters["BankName"] = BankName.Text;
        }



        protected void ODSBank_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
        }

        protected void GV_Bank_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("BankReport.aspx");
                //Server.Execute("BankReport.aspx"); 
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
