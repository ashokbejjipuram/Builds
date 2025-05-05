#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.Web.Services;

#endregion Namespace

namespace IMPALWeb
{
    public partial class Main : System.Web.UI.MasterPage
    {
        #region Events
        public int AccessCnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] == null)
                    LogoutUser();

                string Hidepath =  "";

                //this.AddKeepAlive();

                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                string path = url.Substring(url.LastIndexOf("/") + 1).ToUpper();

                if (Request.UrlReferrer != null)
                {
                    string UrlHide = Request.UrlReferrer.ToString();
                    Hidepath = UrlHide.Substring(UrlHide.LastIndexOf("?") + 1);
                }

                string Hidepath1 = url.Substring(url.LastIndexOf("?") + 1);

                if (!IsPostBack)
                {
                    //if (Session["BranchCode"] == null)
                    //    LogoutUser();

                    lblBranchName.Text = "<b>Branch:</b> " + (string)Session["BranchName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>UserName:</b> " + (string)Session["UserName"];
                    lblLoggedTime.Text = (string)Session["LoginTime"];

                    //Invalid path issue for Js files fixed using ResolveClientURL() and DataBind() method
                    Page.Header.DataBind();                    
                }

                //To Load the Accordion Menu with Header Content and the TreeView Menu
                LoadAccordionTreeViewMenu();

                if (!IsPostBack)
                {
                    Session["AutoTODsuppliers"] = null;
                    ClearCache();
                }

                if (AccessCnt != 0)
                {
                    Server.ClearError();
                    Response.Redirect("~/Home.aspx?ConfirmMsg=InvAccess", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (path == "HOME.ASPX" || path == "HOME.ASPX?CONFIRMMSG=INVTRANS" || path == "HOME.ASPX?CONFIRMMSG=INVACCESS")
                {
                    menuPanel.Style["Display"] = "";
                }
                else
                {
                    if (!(Session["UserName"] == null || Session["UserName"].ToString() == ""))
                    {
                        if ((string)Session["UserName"].ToString().ToUpper() == "CORPORATE" && url.ToUpper().Contains("/TRANSACTIONS/"))
                        {                            
                            Server.ClearError();
                            Response.Redirect("~/Home.aspx?ConfirmMsg=InvTrans", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }

                    menuPanel.Style["Display"] = "none";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        //private void AddKeepAlive()
        //{
        //    int int_MilliSecondsTimeOut = (this.Session.Timeout * 60000) - 30000;
        //    string str_Script = @"
        //    <script type='text/javascript'>
        //    //Number of Reconnects
        //    var count=0;
        //    //Maximum reconnects setting
        //    var max = 5;
        //    function Reconnect(){

        //    count++;
        //    if (count < max)
        //    {
        //    window.status = 'Link to Server Refreshed ' + count.toString()+' time(s)' ;

        //    var img = new Image(1,1);

        //    img.src = '/KB/session/Reconnect.aspx';

        //    }
        //    }

        //    window.setInterval('Reconnect()'," + int_MilliSecondsTimeOut.ToString() + @"); //Set to length required

        //    </script>

        //    ";

        //    this.Page.RegisterClientScriptBlock("Reconnect", str_Script);

        //}

        /// Image button method
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// Log out button click event.
        protected void btLogout_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                // to logout the logged in user and redirect to login page
                LogoutUser();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// Help button click event
        protected void btHelp_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #endregion Events

        #region Userdefined Methods
        /// To Load the Accordion Menu with Header Content and the TreeView Menu
        private void LoadAccordionTreeViewMenu()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                for (int i = 0; i < SiteMap.RootNode.ChildNodes.Count; i++)
                {
                    SiteMapNode siteMapNode = (SiteMapNode)SiteMap.RootNode.ChildNodes[i];
                    
                    // Based on Roles and ShowMenu, display the menu headers.
                    // Hiding Menu headers, hides whole Menu items below that menu header
                    string roleMenuHeader = "";
                    for (int roleCount = 0; roleCount < siteMapNode.Roles.Count; roleCount++)
                    {
                        roleMenuHeader += siteMapNode.Roles[roleCount].ToString();
                        if (roleCount < siteMapNode.Roles.Count - 1)
                            roleMenuHeader += " , ";
                    }

                    // Make an pane for each nodes under the root
                    AccordionPane accordionPane = new AccordionPane();

                    accordionPane.ID = "Pane" + i;

                    // Insert a panel and an hyperlink in the pane
                    Panel pnlHeader = new Panel();
                    HyperLink hlHeader = new HyperLink();
                    Image image = new Image();

                    hlHeader.NavigateUrl = SiteMap.RootNode.ChildNodes[i].Url.ToString();
                    hlHeader.Text = SiteMap.RootNode.ChildNodes[i].Title.ToString();
                    image.ID = "img" + i;

                    hlHeader.Text = string.Format("<span class=\"{0}\" /></span><span>{1}</span>",
                                                                                        hlHeader.Text,
                                                                                        hlHeader.Text);

                    pnlHeader.Controls.Add(hlHeader);

                    accordionPane.HeaderContainer.Controls.Add(pnlHeader);

                    pnlHeader.Visible = false;
                    accordionPane.HeaderContainer.Attributes.Add("style", "display:none");

                    // Fixed Issue for displaying menu headers based roles.
                    if (Session["RoleCode"] != null)
                    {
                        if (roleMenuHeader.Contains(Session["RoleCode"].ToString()) && siteMapNode["ShowMenu"] == "true")
                        {
                            pnlHeader.Visible = true;
                            accordionPane.HeaderContainer.Attributes.Add("style", "display:block");
                        }
                    }

                    // If this is the current page, make it underline
                    if (siteMapNode.Url == Page.Request.Path)
                        hlHeader.Font.Underline = true;

                    if (siteMapNode.HasChildNodes)
                    {
                        // The node has children, so make a TreeView with it.
                        TreeView treeSubNodeAccord = new TreeView();

                        //Tree View properties
                        treeSubNodeAccord.ShowLines = true;
                        treeSubNodeAccord.NodeIndent = 10;
                        treeSubNodeAccord.ImageSet = TreeViewImageSet.Msdn;

                        //CSS Class Styling for Tree View
                        treeSubNodeAccord.CssClass = "treeviewMain";
                        treeSubNodeAccord.NodeStyle.CssClass = "treeviewNodeStyle";
                        treeSubNodeAccord.RootNodeStyle.CssClass = "treeviewRootNodeStyle";
                        treeSubNodeAccord.ParentNodeStyle.CssClass = "treeviewParentNodeStyle";
                        treeSubNodeAccord.LeafNodeStyle.CssClass = "treeviewLeafNodeStyle";
                        treeSubNodeAccord.HoverNodeStyle.CssClass = "treeviewHoverNodeStyle";
                        treeSubNodeAccord.SelectedNodeStyle.CssClass = "treeviewSelectedNodeStyle";

                        TreeNodeStyle treeNodeStyleLevel1 = new TreeNodeStyle();
                        treeNodeStyleLevel1.CssClass = "treeviewTreeNodeStyleLevel1";

                        TreeNodeStyle treeNodeStyleLevel2 = new TreeNodeStyle();
                        treeNodeStyleLevel2.CssClass = "treeviewTreeNodeStyleLevel2";

                        TreeNodeStyle treeNodeStyleLevel3 = new TreeNodeStyle();
                        treeNodeStyleLevel3.CssClass = "treeviewTreeNodeStyleLevel3";

                        TreeNodeStyle treeNodeStyleLevel4 = new TreeNodeStyle();
                        treeNodeStyleLevel4.CssClass = "treeviewTreeNodeStyleLevel4";

                        TreeNodeStyle treeNodeStyleLevel5 = new TreeNodeStyle();
                        treeNodeStyleLevel5.CssClass = "treeviewTreeNodeStyleLevel5";

                        treeSubNodeAccord.LevelStyles.Add(treeNodeStyleLevel1);
                        treeSubNodeAccord.LevelStyles.Add(treeNodeStyleLevel2);
                        treeSubNodeAccord.LevelStyles.Add(treeNodeStyleLevel3);
                        treeSubNodeAccord.LevelStyles.Add(treeNodeStyleLevel4);
                        treeSubNodeAccord.LevelStyles.Add(treeNodeStyleLevel5);

                        treeSubNodeAccord.ID = "treeViewMainMenu" + i;

                        if (AddChildren(siteMapNode, treeSubNodeAccord, null)
                                        || siteMapNode.Url == Page.Request.Path)
                        {
                            myAccordion.SelectedIndex = i;
                        }

                        treeSubNodeAccord.CollapseAll();
                        if (treeSubNodeAccord.SelectedNode != null)
                            ExpandToRoot(treeSubNodeAccord.SelectedNode);

                        // Fixed Issue for displaying menu headers based roles.
                        if (Session["RoleCode"] != null)
                        {
                            if (roleMenuHeader.Contains(Session["RoleCode"].ToString())
                                                    && siteMapNode["ShowMenu"] == "true")
                            {
                                accordionPane.ContentContainer.Controls.Add(treeSubNodeAccord);
                            }
                        }
                    }

                    //Add accordion pane to my accordion control for Accordion TreeView Menu.
                    myAccordion.Panes.Add(accordionPane);
                    //Session["Apane"] = myAccordion;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        //private void LoadTreeFromSession()
        //{
        //    Accordion accordion = new Accordion();
        //    accordion = (Accordion)Session["Apane"];
        //    Control ctrl = accordion.FindControl("MyAccordion");
        //    string[] paneArray = new string[ctrl.Controls.Count - 1];

        //    for (int i = 1; i < ctrl.Controls.Count; i++)
        //    {
        //        if (ctrl.Controls[i].ID.Contains("Pane"))
        //        {
        //            paneArray[i - 1] = ctrl.Controls[i].ID;
        //        }
        //    }

        //    foreach (string s in paneArray)
        //    {
        //        AccordionPane accordionPane = new AccordionPane();
        //        accordionPane = (AccordionPane)accordion.FindControl(s);
        //        myAccordion.Panes.Add(accordionPane);
        //    }

        //    Session["Apane"] = null;
        //    Session["Apane"] = myAccordion;
        //    myAccordion = null;
        //}

        private void ExpandToRoot(TreeNode node)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                node.Expand();
                if (node.Parent != null)
                {
                    ExpandToRoot(node.Parent);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// To Load the Child nodes for TreeView Menu
        protected virtual bool AddChildren(SiteMapNode node, TreeView tree, TreeNode currentTreeNode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            bool result = false;
            try
            {
                foreach (SiteMapNode child in node.ChildNodes)
                {
                    TreeNode treeNode = new TreeNode(child.Title);
                    if (child.Url.ToString() != string.Empty)
                        treeNode.NavigateUrl = child.Url;
                    else
                        treeNode.SelectAction = TreeNodeSelectAction.Expand;

                    if (child.Url == Page.Request.Path)
                    {
                        treeNode.Selected = true;
                        result = true;
                        Page.Title = child.Description.ToString();
                    }

                    // You can define additionnal properties in your sitemap nodes
                    // We use this features to use icons in TreeNodes.
                    treeNode.ImageUrl = child["ImageUrl"];

                    // Based on Roles, display the menu items.
                    string roleMenu = "";
                    for (int roleCount = 0; roleCount < child.Roles.Count; roleCount++)
                    {
                        roleMenu += child.Roles[roleCount].ToString();
                        if (roleCount < child.Roles.Count - 1)
                            roleMenu += " , ";
                    }

                    if (child.HasChildNodes)
                    {
                        foreach (SiteMapNode child1 in child.ChildNodes)
                        {
                            string roleMenu1 = "";
                            for (int roleCount1 = 0; roleCount1 < child1.Roles.Count; roleCount1++)
                            {
                                roleMenu1 += child1.Roles[roleCount1].ToString();
                                if (roleCount1 < child1.Roles.Count - 1)
                                    roleMenu1 += " , ";
                            }

                            if (child1.Url.ToUpper() == Page.Request.Path.ToUpper())
                            {
                                if (Session["RoleCode"] != null)
                                {
                                    if (!(roleMenu1.Contains(Session["RoleCode"].ToString())))
                                    {
                                        AccessCnt++;
                                        return false;
                                    }
                                }
                            }

                            if (child1.HasChildNodes)
                            {
                                foreach (SiteMapNode child2 in child1.ChildNodes)
                                {
                                    string roleMenu2 = "";
                                    for (int roleCount2 = 0; roleCount2 < child2.Roles.Count; roleCount2++)
                                    {
                                        roleMenu2 += child2.Roles[roleCount2].ToString();
                                        if (roleCount2 < child2.Roles.Count - 1)
                                            roleMenu2 += " , ";
                                    }

                                    if (child2.Url.ToUpper() == Page.Request.Path.ToUpper())
                                    {
                                        if (Session["RoleCode"] != null)
                                        {
                                            if (!(roleMenu2.Contains(Session["RoleCode"].ToString())))
                                            {
                                                AccessCnt++;
                                                return false;
                                            }
                                        }
                                    }

                                    if (child2.HasChildNodes)
                                    {
                                        foreach (SiteMapNode child3 in child2.ChildNodes)
                                        {
                                            string roleMenu3 = "";
                                            for (int roleCount3 = 0; roleCount3 < child3.Roles.Count; roleCount3++)
                                            {
                                                roleMenu3 += child2.Roles[roleCount3].ToString();
                                                if (roleCount3 < child3.Roles.Count - 1)
                                                    roleMenu3 += " , ";
                                            }

                                            if (child3.Url.ToUpper() == Page.Request.Path.ToUpper())
                                            {
                                                if (Session["RoleCode"] != null)
                                                {
                                                    if (!(roleMenu3.Contains(Session["RoleCode"].ToString())))
                                                    {
                                                        AccessCnt++;
                                                        return false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (child.Url.ToUpper() == Page.Request.Path.ToUpper())
                        {
                            if (Session["RoleCode"] != null)
                            {
                                if (!(roleMenu.Contains(Session["RoleCode"].ToString())))
                                {
                                    AccessCnt++;
                                    return false;
                                }
                            }
                        }
                    }

                    if (roleMenu != string.Empty)
                    {
                        if (Session["RoleCode"] != null)
                        {
                            if (roleMenu.Contains(Session["RoleCode"].ToString()))
                            {
                                // To show (or) hide the menu option on the Left side menu tree.
                                if (child["ShowMenu"].ToString().Equals("true"))
                                {
                                    if (currentTreeNode == null)
                                        tree.Nodes.Add(treeNode);
                                    else
                                        currentTreeNode.ChildNodes.Add(treeNode);

                                    if (child.HasChildNodes)
                                        result |= AddChildren(child, tree, treeNode);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return result;
        }        

        /// Clear cache method
        private void ClearCache()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Server.ClearError();
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Clear();
                    Response.ClearContent();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// User Log out method
        private void LogoutUser()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();
                Server.ClearError();
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// Method to save the previous page URL in session to handle browser back button click.
        public void SavePreviousPageURL()
        {
            if (Session["CurrentPageURL"] != null)
            {
                Session["PreviousPageURL"] = Session["CurrentPageURL"];
            }

            Session["CurrentPageURL"] = Request.Url;
        }

        /// To show / hide filter based report button text
        public bool ShowHideFilters(Button btnReport, System.Web.UI.HtmlControls.HtmlControl reportFilters,
                                        System.Web.UI.HtmlControls.HtmlControl reportViewer)
        {
            if (btnReport.Text == "Back")
            {
                //reportFilters.Visible = true;
                reportFilters.Style["Display"] = "";
                reportViewer.Visible = false;
                btnReport.Text = "Generate Report";
                Session["ViewState"] = null;
                ViewState.Clear();
                Server.ClearError();

                Uri uriAddress = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

                if (!(uriAddress.ToString().ToLower().Contains("http://localhost") || uriAddress.ToString().ToLower().Contains("http://impalser") || uriAddress.ToString().ToLower().Contains("http://ashok-pc") || uriAddress.ToString().ToLower().Contains("http://awstesting")))
                {
                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"), false);
                }
                else
                {
                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri, false);
                }
                
                Context.ApplicationInstance.CompleteRequest();
                return false;
            }
            else
            {
                //reportFilters.Visible = false;
                reportFilters.Style["Display"] = "none";
                reportViewer.Visible = true;
                btnReport.Text = "Back";
                return true;
            }
        }

        #endregion Userdefined Methods
    }
}
