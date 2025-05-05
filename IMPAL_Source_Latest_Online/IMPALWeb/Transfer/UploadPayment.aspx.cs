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
using System.IO;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion Namespace

namespace IMPALWeb.Transfer
{
    public partial class UploadPayment : System.Web.UI.Page
    {
        #region Private Variables

        private const string serverDownloadFolder = "Downloads";

        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();

        private string filePath;

        private string branchCode;

        #endregion Private Variables

        #region Public Variables

        IMPALLibrary.Upload upload = new IMPALLibrary.Upload();

        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        #endregion Public Variables

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion Events

        #region Userdefined Methods

        protected void btnRegularUpload_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnRegularUpload.Visible = false;
                btnFileUpload.Enabled = false;
                string sStatus = string.Empty;

                if (btnFileUpload.HasFile)
                {
                    ltrProgressBar.Text = "<div style=\"display:block\"></div>";

                    filePath = @"D:\Downloads\HoPayment";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(filePath + "\\" + fileName) && fileName.Substring(8, 3).ToUpper().Contains("CRP"))
                    {
                        branchCode = Session["BranchCode"].ToString();

                        Session["UploadPaymentDetails"] = upload.UploadPaymentDataFromTextFile(filePath, fileName, branchCode);
                        FileStatusMsg.Text = Session["UploadPaymentDetails"].ToString();                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("UploadPayment.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        #endregion Userdefined Methods
    }
}