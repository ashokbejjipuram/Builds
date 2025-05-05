using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Security
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Assign the Username to the username text box

            txtUserName.Text =  (string) Session["UserName"];
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid )
            UpdatePassoword();
        }

        protected void VarifyPassword(object sender, ServerValidateEventArgs e)
        {
            IMPALLibrary.Users objUsers = new IMPALLibrary.Users();
            string BranchCode = (string)Session["BranchCode"];
            string OldPassword = (string)txtOldPassword.Text;
            bool IsValid = objUsers.IsPasswordValid(BranchCode, OldPassword);
            if (IsValid) e.IsValid = true;
            else e.IsValid = false;
        }

        public void UpdatePassoword()
        {
            IMPALLibrary.Users objUsers = new IMPALLibrary.Users();
            string BranchCode = (string)Session["BranchCode"];
            string NewPassword = (string) txtNewPassword.Text;
           
           //Update the new password
            bool IsUpdated = objUsers.ResetPassword(NewPassword,BranchCode);

            if (IsUpdated) lblSucessMessage.Text = "Password Updated Successfully";
 
        }
    }
}
