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
    public partial class UploadWorkSheet : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        private const string serverDownloadFolder = "Downloads\\WorkSheet";

        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();

        private string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString();

        private bool isDownloadPathAvailable = false;

        private const string branchCodeHO = "COR";

        private string fileName;

        private string sqlTotQuery;

        private string erritem = "";

        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "Statement Of Account Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (strBranchCode != "GUW")
                {
                    btnFileUpload.Enabled = false;
                    btnUpload.Enabled = false;
                    btnReset.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
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

        protected void UploadFileData(bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (serverPathMode.Contains("true"))
                    filePath = HttpContext.Current.Server.MapPath(@"~/" + serverDownloadFolder);

                isDownloadPathAvailable = uitility.CheckDirectoryExists(filePath);
                string SQLQuery = "";
                string Cnt;

                if (isDownloadPathAvailable)
                {
                    if (btnFileUpload.HasFile)
                    {
                        Session["UploadDetails"] = "";

                        fileName = btnFileUpload.FileName;

                        if (File.Exists(Path.Combine(filePath, fileName)))
                        {
                            SQLQuery = "Exec Usp_AddWorkSheet '" + filePath + "','" + fileName + "'";
                            Database ImpalDb = DataAccess.GetDatabase();
                            DbCommand cmd = ImpalDb.GetSqlStringCommand(SQLQuery);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            Cnt = ImpalDb.ExecuteScalar(cmd).ToString();

                            if (Cnt == "0")
                            {
                                Session["UploadDetails"] = "<font style='color:red' size='2'>WorkSheet Uploaded Successfully</font>";
                            }
                            else
                            {
                                Session["UploadDetails"] = "<font style='color:red' size='2'>Error in the Data</font>";
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                        }

                        btnUpload.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        } 

        private void ShowSuccessMessage(string message)
        {
            lblUploadMessage.Visible = true;
            lblUploadMessage.Text = "<br /><br /><center style='font-size:13px;'><b>" + message + "</center>";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnUpload.Enabled = true;
                lblUploadMessage.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }
    }
}

