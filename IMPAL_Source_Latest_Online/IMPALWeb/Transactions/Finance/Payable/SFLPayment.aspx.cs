using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class SFLPayment : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        private const string serverDownloadFolder = "Downloads";
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

                if (isDownloadPathAvailable)
                {
                    if (btnFileUpload.HasFile)
                    {
                        Session["UploadDetails"] = "";

                        fileName = btnFileUpload.FileName;

                        if (File.Exists(Path.Combine(filePath, fileName)))
                        {
                            UploadSFLPaymentDetails(filePath, fileName);
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

        protected void UploadSFLPaymentDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;            
            try
            {
                string SQLQuery = "";
                string file = Path.Combine(filePath, fileName);
                string Cnt;

                SQLQuery = "Exec Usp_Upd_Temp_SFL_BranchName '" + filePath + "','" + fileName+ "'";
                Database ImpalDb = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDb.GetSqlStringCommand(SQLQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = ImpalDb.ExecuteScalar(cmd).ToString();

                if (Cnt == "0")
                {
                    int UpdStatus = payableEntity.UpdateSFLPaymentDetails(strBranchCode, filePath, fileName);

                    if (UpdStatus == 1)
                        Session["UploadDetails"] = "<font style='color:Blue' size='2'>Difference in the Amount. Please Check the Updated File <font color='red'>" + Path.Combine(filePath, "Updated_" + fileName) + "</font></font>";
                    else
                    {
                        payableEntity.InsertSFLPaymentDetails();
                        Session["UploadDetails"] = "<font style='color:red' size='2'>SFL Corp and BMS Header Data Matches</font>";
                    }
                }
                else
                {
                    btnUpload.Enabled = false;
                    lblErrorMessage.Visible = true;
                    lblUploadMessage.Visible = false;
                    Session["UploadDetails"] = "<font style='color:red' size='2'>Branch Code is Null for Few Records.<br />Please Check the file" + Path.Combine(filePath, "Branch_NULL_" + fileName) + "</font>";
                }
            }
            catch (Exception exp)
            {
                Session["UploadDetails"] = "Error in the Data";
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
            try
            {
                btnUpload.Enabled = true;
                lblUploadMessage.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(OtherPayment), ex);
            }
        }
    }
}

