using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using System.Data.Common;
using System.Data;
using System.IO;
using IMPALLibrary.Transactions;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Diagnostics;

namespace IMPALWeb.UserControls
{
    public partial class DownloadReport : System.Web.UI.Page
    {
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0 && Request.QueryString["FileName"] != null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "alert('File Has been Downloaded Successfully');", true);

                //Based upon id get file name from database or from some where else. I am considering direct file name
                string filePathFromDatabase = Request.QueryString["FileName"];
                DownloadFile(filePathFromDatabase);
            }
        }

        private void DownloadFile(string filename)
        {
        }
    }
}