﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace IMPALWeb
{
    public partial class Reconnect : System.Web.UI.Page
    {

        protected string WindowStatusText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Refresh this page 60 seconds before session timeout, effectively resetting the session timeout counter.
                MetaRefresh.Attributes["content"] = Convert.ToString((Session.Timeout * 60) - 60) + ";url=Reconnect.aspx?q=" + DateTime.Now.Ticks;
                WindowStatusText = "Last refresh " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }
        }
    }
}