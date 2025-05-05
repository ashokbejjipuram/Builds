#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Web.Security;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

#endregion Namespace

namespace IMPALWeb
{
    public partial class UpdPwds : System.Web.UI.Page

    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btSignIn_Click(object sender, EventArgs e)
        {
            Users objUsers = new Users();
            UserInfo userinfo = new UserInfo();
            userinfo = objUsers.UpdateEncodedPasswords();
        }
        
        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
        
        protected void txtPassword_Unload(object sender, EventArgs e)
        {

        }

        protected void btSignIn_Command(object sender, CommandEventArgs e)
        {

        }
        #endregion Events

        #region Userdefined Methods

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// To change the Site's Style Sheet Theme based on the UpdatePwds page theme buttons
        private void ChangeSiteStyleSheetTheme(string styleSheetThemeName)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");

                //Get the required section of the web.config file by using configuration object.
                PagesSection pages = (PagesSection)config.GetSection("system.web/pages");

                //Update the new values.
                pages.StyleSheetTheme = styleSheetThemeName.ToString();

                //save the changes by using Save() method of configuration object.
                if (!pages.SectionInformation.IsLocked)
                {
                    config.Save();
                    Server.ClearError();
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Write("<script>alert('Could not save Theme name in the configuration')</script>");
                }
            }
            catch (Exception)
            {
            }
        }

        /// Show (or) Hide Theme option for the Website
        private void ShowHideThemeOption()
        {
            try
            {
                bool showThemeOption = false;
                btnThemeWhite.Visible = false;
                btnThemeGray.Visible = false;

                showThemeOption = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowThemeOption"].ToString());

                if (showThemeOption)
                {
                    btnThemeWhite.Visible = true;
                    btnThemeGray.Visible = true;
                }
                else
                {
                    btnThemeWhite.Visible = false;
                    btnThemeGray.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion Userdefined Methods
    }
}
