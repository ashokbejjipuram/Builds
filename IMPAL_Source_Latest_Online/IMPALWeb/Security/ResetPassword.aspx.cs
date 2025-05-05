using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMPALWeb.Security
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this screen is applicable only for corporate users
            string CurrentRole = (string)Session["RoleCode"];
            if (CurrentRole == "BEDP")
            {
                main.Visible = false;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                UpdatePassoword();
        }

        public void UpdatePassoword()
        {
            IMPALLibrary.Users objUsers = new IMPALLibrary.Users();
           // string BranchCode = (string)Session["BranchCode"];
            string BranchCode = (string)ddUserList.SelectedValue;
            string NewPassword = (string)txtNewPassword.Text;

            //Update the new password
            bool IsUpdated = objUsers.ResetPassword(NewPassword, BranchCode);

            if (IsUpdated) lblSucessMessage.Text = "Password Updated Successfully";

        }
    }
}
