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
    public partial class Upload : System.Web.UI.Page
    {
        #region Private Variables

        private string fileName;
        private string filePath;
        private string branchCode;
        private DataSet dataSetBranchTables = new DataSet();

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

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;
                btnFileUpload.Enabled = false;
                btnUploadFile.Enabled = false;
                calExtDownloadDate.Enabled = false;

                if (btnFileUpload.HasFile)
                {
                    ltrProgressBar.Text = "<div style=\"display:block\"></div>";
                    filePath = @"D:\Downloads\BranchDownload";
                    fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    ltrProgressBar.Text = "<div style=\"display:block\"></div>";

                    Session["UploadDetails"] = "";
                    Session["UploadTableDetails"] = "";
                    branchCode = fileName.Substring(8, 3);

                    fileName = string.Format("{0}.txt", uitility.DateFormatYYYYMMDD(txtDownloadDate.Text) + branchCode);

                    if (uitility.CheckFileExists(Path.Combine(filePath, fileName)))
                    {
                        Session["UploadTableDetails"] = upload.UploadBranchDataFromTextFile(filePath, fileName, branchCode, txtDownloadDate.Text);
                        FileStatusMsg.Text = Session["UploadTableDetails"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                }
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("Upload.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
