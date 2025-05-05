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
    public partial class Login : System.Web.UI.Page

    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {   
                ShowHideThemeOption();
                BuildButton.Text  = (@System.Reflection.Assembly.GetExecutingAssembly().GetName().Version).ToString();

                HttpCookie aCookie;
                string cookieName;
                int limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }

            txtUserId.Focus();
            txtUserId.Attributes.Add("autocomplete", "off");
        }

        protected void btSignIn_Click(object sender, EventArgs e)
        {
            string sUserId = txtUserId.Text.Trim();
            string sPassword = txtPassword.Text.Trim();

            if (!(sUserId == "" || sUserId == null))
            {
                lblMessage1.Text = "";

                Users objUsers = new Users();
                UserInfo userinfo = new UserInfo();
                int Count = objUsers.GetUserId(sUserId);

                if (Count > 0)
                {
                    if (!(sPassword == "" || sPassword == null))
                    {
                        //objUsers.UpdateEncodedPasswords();
                        userinfo = objUsers.GetUserInfo(sUserId, sPassword);                        

                        if (userinfo.RoleID > 0)
                        {
                            //Create form authentication ticket
                            Session["UserName"] = userinfo.UserName;
                            Session["UserID"] = userinfo.UserID;
                            Session["BranchCode"] = userinfo.BranchCode;
                            Session["RoleCode"] = userinfo.RoleCode;
                            Session["RoleName"] = userinfo.RoleName;
                            Session["BranchName"] = userinfo.BranchName;
                            //objUsers.Updateqtyinitial(Session["BranchCode"].ToString());
                            CreateFormAuthenctionTicket(sUserId);
                        }
                        else
                        {
                            lblMessage.Text = "Please enter a valid password";
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please enter a Password";
                        txtPassword.Focus();
                    }
                }
                else
                {
                    txtUserId.Text = "";
                    lblMessage1.Text = "Please enter a valid UserId";
                    txtUserId.Focus();
                }
            }
            else
            {
                lblMessage1.Text = "Please enter an UserId";
                txtUserId.Focus();
            }
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
        private void CreateFormAuthenctionTicket(string UserName)
        {
            //FormsAuthenticationTicket tkt ;
            string cookiestr;
            HttpCookie ck;
            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, UserName, DateTime.Now,
            DateTime.Now.AddMinutes(30), false, "your custom data");
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            //if (chkPersistCookie.Checked)
            //    ck.Expires = tkt.Expiration;	
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);

            Session["IPAdress"] = GetIPAddress();

            if (Session["UserID"] != null)
            {
                Session["ServiceProvider"] = Dns.GetHostName();

                var host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Session["IPAdress1"] = ip.ToString();
                    }
                }

                Users objUsers = new Users();
                objUsers.UpdateUserIPAddressDetails(Session["BranchCode"].ToString(), Session["UserID"].ToString(), Session["ServiceProvider"].ToString(),
                                                    Session["IPAdress"].ToString(), Session["IPAdress1"].ToString(), ck.Path, "A");
            }

            //if (Request.QueryString["ReturnUrl"] == null)
                Response.Redirect("Home.aspx", true);
            //else
            //    Response.Redirect(Request.QueryString["ReturnURL"], true);
        }

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

        /// To change the Site's Style Sheet Theme based on the Login page theme buttons
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
