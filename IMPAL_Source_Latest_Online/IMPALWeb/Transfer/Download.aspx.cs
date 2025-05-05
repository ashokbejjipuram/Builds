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
using System.Threading;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;

#endregion Namespace


namespace IMPALWeb.Transfer
{
    public partial class Download : System.Web.UI.Page
    {
        #region Private Variables

        private const string serverDownloadFolder = "Downloads";

        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();

        private string filePath = System.Configuration.ConfigurationManager.AppSettings["DownloadPath"].ToString();

        private string downloadResult;

        private DataSet dataSetBranchTables = new DataSet();

        private string tableName;

        private int tableColumnsCount;

        private string invSQLQuery;

        private int threadCount = 0;

        private int totalThreadCount = 100;

        private int timerCount = 0;

        private string downloadRowInfo = "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>";

        private string downloadRowInfoData = "";

        private StringBuilder messageString = new StringBuilder();

        private string hostUrl;

        private Uri uriAddress;

        private Uri uriAddress1;

        #endregion Private Variables

        #region Public Variables

        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();
        IMPALLibrary.Download download = new IMPALLibrary.Download();

        #endregion Public Variables

        #region Events
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Download button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ltrProgressBar.Text = "<div id=\"divShadowForBar\" style=\"display:block\"></div>";

                Session["DownloadDetails"] = "";
                Session["DownloadTableDetails"] = "";

                // To create the file first
                CreateFileForBranchData();

                Session["DownloadStatus"] = 0;

                Session["TotalDownloadCount"] = 0;

                // To create the data for the download file
                Thread downloadThread = new Thread(new System.Threading.ThreadStart(DownloadBranchTables));
                downloadThread.IsBackground = true;
                downloadThread.Start();
                Session["Thread"] = downloadThread;

                btnDownload.Enabled = false;
                tmrDownload.Enabled = true;                
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        /// <summary>
        /// Time eevent for Ajax Progress Bar update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tmrDownload_Tick(object sender, EventArgs e)
        {
            double percentage = 0;
            timerCount = Convert.ToInt32(Session["DownloadStatus"]);
            totalThreadCount = Convert.ToInt32(Session["TotalDownloadCount"]);
            filePath = HttpContext.Current.Server.MapPath(@"~/" + serverDownloadFolder); 
            uriAddress = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            uriAddress1 = new Uri(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"));

            if (totalThreadCount != 0)
                percentage = (timerCount / totalThreadCount) * 100;

            percentage = System.Math.Round(percentage);

            if (percentage == 100)
            {
                tmrDownload.Enabled = false;
                btnDownload.Enabled = true;
                ltrProgressBar.Visible = false;

                string folderPath = string.Format("{0:yyyyMM}", DateTime.Now);
                string fileNamePrefix = string.Format("{0:yyyyMMdd}", DateTime.Now) + (string)Session["BranchCode"] + ".txt";

                filePath = Path.Combine(filePath, folderPath);

                if (!(uriAddress.ToString().ToLower().Contains("http://localhost") || uriAddress.ToString().ToLower().Contains("http://impalser")))
                {
                    hostUrl = uriAddress1.GetLeftPart(UriPartial.Authority);
                }
                else
                {
                    hostUrl = uriAddress.GetLeftPart(UriPartial.Authority);

                    if (uriAddress.ToString().ToLower().Contains("http://impalser"))
                        hostUrl = hostUrl + "/ImpalWeb";
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadTXTFile('" + hostUrl + ("/UserControls/DownloadTXT.aspx?FileName=" + fileNamePrefix) + "');", true);

                ShowSuccessMessage(Session["DownloadDetails"].ToString());
            }

            ltrProgressBar.Text = string.Format("<div id=\"divShadowForBar\" style=\"display:block\"></div><div id=\"divAjaxProgressBarLoader\" style=\"display:block\">" +
                                                            "<table id=\"tblProgressBar\" width=\"" +
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

            //ltrProgressBar.Text = ltrProgressBar.Text + "<br><br><br>" + percentage.ToString() + "<br>" + totalThreadCount.ToString() + "<br>" + timerCount.ToString()+
            //                                            "<br><br>" + Session["TotalDownloadCount"].ToString() +
            //                                             "<br><br>" + Session["DownloadDetails"].ToString();
        }

        #endregion Events

        #region Userdefined Methods
        /// <summary>
        /// Method to create the text file for the Branch Data
        /// </summary>
        protected void CreateFileForBranchData()
        {
            if (serverPathMode.Contains("true"))
            {
                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "1", "File to be written to server", "");
                
                filePath = HttpContext.Current.Server.MapPath(@"~/" + serverDownloadFolder);                

                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "2", string.Format("Filepath: {0}", filePath), "");
            }
            else
            {
                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "1", "File to be written to local (client machine) path", "");
                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "2", string.Format("Filepath: {0}", filePath), "");
            }

            if (uitility.CheckDirectoryExists(filePath))
            {
                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "3", "File path exists on server", "");
                downloadResult = uitility.WriteTextFileToServer(filePath, (string)Session["BranchCode"], true);
            }
            else
            {
                Session["DownloadDetails"] = Session["DownloadDetails"].ToString() +
                                                string.Format(downloadRowInfo, "3", "File path does not exists on server<br> Download Failed", "");
            }
        }

        /// <summary>
        /// Method to download branch table details to Downloads folder
        /// </summary>
        protected void DownloadBranchTables()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType; 
            try
            {
                if (downloadResult.Contains(serverDownloadFolder))
                {
                    Session["DownloadDetails"] = Session["DownloadDetails"].ToString() + string.Format(downloadRowInfo, "4", "File created and Table details to downloaded taken", "");

                    dataSetBranchTables = download.DownloadBranchTableDetails((string)Session["BranchCode"]);

                    totalThreadCount = dataSetBranchTables.Tables[0].Rows.Count;

                    Session["TotalDownloadCount"] = dataSetBranchTables.Tables[0].Rows.Count;

                    ShowSuccessMessage(messageString.ToString());

                    foreach (DataTable table in dataSetBranchTables.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            tableName = row[0].ToString();
                            tableColumnsCount = Convert.ToInt32(row[1].ToString());

                            Thread.Sleep(50);

                            //threadCount = uitility.CalculateProgressBar(totalThreadCount, threadCount);
                            threadCount++;

                            Session["DownloadStatus"] = threadCount;

                            if (download.DownloadBranchData(downloadResult, (string)Session["BranchCode"], tableName, tableColumnsCount))
                            {
                                //messageString.Append(string.Format(downloadRowInfo, "",
                                //                                        string.Format("The table {0} successfully written to the file", tableName),
                                //                                        string.Format("Table Column Count: {0}", tableColumnsCount)));
                                Session["DownloadTableDetails"] = Session["DownloadTableDetails"].ToString() +
                                                                            string.Format(downloadRowInfo, "",
                                                                            string.Format("Upload table {0} - success", tableName),"");

                                download.DownloadUpdateBranchData(downloadResult, (string)Session["BranchCode"], tableName, tableColumnsCount);
                            }
                            else
                            {
                                //messageString.Append(string.Format(downloadRowInfo, "",
                                //                                        string.Format("The table {0} download failed", tableName),
                                //                                        string.Format("Table Column Count: {0}", tableColumnsCount)));
                                Session["DownloadTableDetails"] = Session["DownloadTableDetails"].ToString() +
                                                                            string.Format(downloadRowInfo, "",
                                                                            string.Format("Upload table {0} - failed", tableName),"");
                            }
                        }
                    }

                    if (Session["BranchCode"].ToString().ToUpper() != "COR" && Session["BranchCode"].ToString().ToUpper() != "CRP")
                    {
                        download.UpdateInvTables(Session["BranchCode"].ToString());
                    }
                }
                else
                {
                    Session["DownloadDetails"] = Session["DownloadDetails"].ToString() + string.Format(downloadRowInfo, "4", "File creation and table details fetching failed", "");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                ShowErrorMessage("File Download Failed");
            }
        }

        /// <summary>
        /// Method to check the progress bar
        /// </summary>
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

        /// <summary>
        /// Method to show success message on the page
        /// </summary>
        /// <param name="message"></param>
        private void ShowSuccessMessage(string message)
        {
            lblDownloadError.Visible = false;
            lblDownloadMessage.Visible = true;
            lblDownloadMessage.Text = string.Format("<table><tr><td>{0}</td></tr>{1}</table><br />" +
                                                    "<table><tr><td>{2}</td></tr>{3}</table><br />",
                                                    "","",//"Download Details",message,
                                                        "Downloaded Details",
                                                        Session["DownloadTableDetails"].ToString());
        }

        /// <summary>
        /// Method to show error message on the page
        /// </summary>
        /// <param name="message"></param>
        private void ShowErrorMessage(string message)
        {
            lblDownloadMessage.Visible = false;
            lblDownloadError.Visible = true;
            lblDownloadError.Text = message;
        }

        #endregion Userdefined Methods

    }
}
