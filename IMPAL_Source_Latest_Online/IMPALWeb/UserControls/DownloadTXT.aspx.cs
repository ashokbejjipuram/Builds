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
using System.Net;

namespace IMPALWeb.UserControls
{
    public partial class DownloadTXT : System.Web.UI.Page
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
                //Based upon id get file name from database or from some where else. I am considering direct file name
                string filePathFromDatabase = Request.QueryString["FileName"];
                DownloadFile(filePathFromDatabase);
            }
        }

        private void DownloadFile(string filename)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string filePath = Server.MapPath(@"\Downloads\" + string.Format("{0:yyyyMM}", DateTime.Now));

            System.IO.FileInfo file = new System.IO.FileInfo(filePath + "\\" + filename);
            if (file.Exists)
            {
                try
                {
                    WebClient req = new WebClient();
                    Response.Clear();
                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.ContentType = "text/plain";
                    byte[] data = req.DownloadData(filePath + "\\" + filename);
                    Response.BinaryWrite(data);
                    Response.End();
                }
                catch (Exception exp)
                {
                    //Log.WriteException(Source, exp);
                }
                finally
                {
                    //if (File.Exists(filePath + "\\" + filename))
                    //    File.Delete(filePath + "\\" + filename);
                }
            }
        }
    }
}