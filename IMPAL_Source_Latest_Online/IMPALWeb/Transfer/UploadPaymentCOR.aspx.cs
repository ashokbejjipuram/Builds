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
    public partial class UploadPaymentCOR : System.Web.UI.Page
    {
        #region Private Variables

        private const string serverDownloadFolder = "Downloads-Pymt";

        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();

        private string filePath = System.Configuration.ConfigurationManager.AppSettings["DownloadPath"].ToString();

        private string fileName;

        private string uploadResult;

        private int threadCount = 0;

        private int status = 0;

        //private int updstatus = 0;

        private int totalThreadCount = 100;

        private int timerCount = 0;

        private bool isDownloadPathAvailable = false;

        private const string branchCodeCorprate = "CRP";

        private string branchCode;

        private int branchCount = 0;

        private DataSet dataSetBranchTables = new DataSet();

        private bool isHOProcess = false;

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

        protected void btnRegularUpload_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                UploadFileData(false);
                ShowSuccessMessage(Session["UploadDetails"].ToString());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("Upload.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void tmrUpload_Tick(object sender, EventArgs e)
        {
            double percentage = 0;
            timerCount = Convert.ToInt32(Session["UploadStatus"]);
            totalThreadCount = Convert.ToInt32(Session["TotalUploadCount"]);

            if (timerCount == 0 && totalThreadCount == 0)
            {
                btnRegularUpload.Enabled = true;
                //tmrUpload.Enabled = false;
                ltrProgressBar.Visible = false;
            }

            else
            {
                if (totalThreadCount != 0)
                    percentage = (timerCount / totalThreadCount) * 100;
                percentage = System.Math.Round(percentage);

                if (percentage == 100)
                {
                    btnRegularUpload.Enabled = true;
                    //tmrUpload.Enabled = false;
                    ltrProgressBar.Visible = false;
                    ShowSuccessMessage(Session["UploadDetails"].ToString());
                }

                ltrProgressBar.Text = string.Format("<div style=\"display:block\"></div><div id=\"divAjaxProgressBarLoader\" style=\"display:block\">" +
                                                                    "<table width='600' id=\"tblProgressBar\" width=\"" +
                                                                    "{0}" +
                                                                    "px\" border=\"2px\">" +
                                                                    "<tr><td width=\"" +
                                                                    "{1}" +
                                                                    "px\">" +
                                                                    "<table id=\"progress\" width=\"{2}\"><tr><td class=\"progress\" width=\"{3}\"></td></tr></table>" +
                                                                    "</td></tr>" +
                                                                    "<tr><td class=\"progressPercent\">" +
                                                                    "{4} %" +
                                                                    "</td></tr></table></div>",
                                                                    totalThreadCount.ToString(),
                                                                    totalThreadCount.ToString(),
                                                                    timerCount.ToString(),
                                                                    timerCount.ToString(),
                                                                    timerCount.ToString());
            }
        }

        #endregion Events

        #region Userdefined Methods
        protected void UploadFileData(bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (CheckDownloadFolderPath())
                {
                    ltrProgressBar.Text = "<div style=\"display:block\"></div>";

                    Session["UploadDetails"] = "";
                    Session["UploadTableDetails"] = "";

                    int BrhCnt = 0;

                    dataSetBranchTables = upload.UploadPaymentCORDetails();

                    totalThreadCount = dataSetBranchTables.Tables[0].Rows.Count;

                    Session["TotalUploadCount"] = dataSetBranchTables.Tables[0].Rows.Count;

                    foreach (DataTable table in dataSetBranchTables.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            branchCode = row[0].ToString();

                            fileName = string.Format("{0}.txt", uitility.DateFormatYYYYMMDD(txtDownloadDate.Text) + branchCode);

                            if (File.Exists(Path.Combine(filePath, fileName)))
                            {
                                Session["UploadTableDetails"] = upload.UploadPaymentCORDataFromTextFile(filePath, fileName, branchCode);

                                BrhCnt = BrhCnt + 1;
                            }
                        }
                    }

                    btnRegularUpload.Enabled = false;
                    
                    if (BrhCnt == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                        btnRegularUpload.Enabled = true;
                    }                    
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected bool CheckDownloadFolderPath()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (serverPathMode.Contains("true"))
                    filePath = HttpContext.Current.Server.MapPath(@"~/" + serverDownloadFolder);

                filePath = Path.Combine(filePath, uitility.DateFormatYYYYMM(txtDownloadDate.Text));

                return isDownloadPathAvailable = uitility.CheckDirectoryExists(filePath);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void CheckProgressBar()
        {
            int i = 0;
            while (i < 100)
            {
                Thread.Sleep(500);
                i = i + 1;
                Session["DownloadStatus"] = i;
            }
            Thread objThread = (Thread)Session["Thread"];
            if (objThread != null)
                objThread.Abort();
        }

        private void ShowSuccessMessage(string message)
        {
            lblUploadError.Visible = false;
            lblUploadMessage.Visible = true;
            Transbuttons.Visible = false;
            imgBtnDownloadDate.Enabled = false;
            txtDownloadDate.ReadOnly = true;
            ResetButton.Visible = true;
            lblUploadMessage.Text = string.Format("<table style='width:400px;font-family:arial;font-size:10px;' border='0'><tr><td width='25%'>{0}</td><td width='75%'>{1}</td></tr></table><br />" +
                                                  "<table style='width:400px;font-family:arial;font-size:10px;' border='0'><tr><td width='25%'>{2}</td><td width='75%'>{3}</td></tr></table><br />",
                                                        "<b>Upload Details</b>",
                                                        message,
                                                        "<b>Upload Table Details</b>",
                                                        Session["UploadTableDetails"].ToString());
        }

        private void ShowErrorMessage(string message)
        {
            lblUploadMessage.Visible = false;
            lblUploadError.Visible = true;
            lblUploadError.Text = message;
        }

        #endregion Userdefined Methods
    }
}
