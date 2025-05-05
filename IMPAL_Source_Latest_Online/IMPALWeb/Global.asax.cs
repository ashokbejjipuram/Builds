using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace IMPALWeb
{    
    public class Global : System.Web.HttpApplication
    {       
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Log.WriteLog(Source, "Application_Start", "Application started");

            //string[] filePaths = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath(@"\App_Themes\Styles\CascadePrt"));
            //foreach (string filePath in filePaths)
            //{
            //    try
            //    {
            //        File.Delete(filePath);
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source,"Session_Start", "Session started");            
            Session["LoginTime"] = "<b>Login Time</b> : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }        

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Exception exc = Server.GetLastError();
            string hostUrl = string.Empty;

            Uri uriAddress = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

            if (!(uriAddress.ToString().ToLower().Contains("http://localhost") || uriAddress.ToString().ToLower().Contains("http://impalser")))
            {
                Uri uriAddress1 = new Uri(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"));
                hostUrl = uriAddress1.ToString();
            }
            else
            {
                hostUrl = uriAddress.ToString();
            }

            Exception excUrl = new Exception(hostUrl);            

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                }
            }

            Application["ExceptionURL"] = excUrl;
            Application["ExceptionMesage"] = exc;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Session_End", "Logged out");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            //string[] filePaths = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath(@"\App_Themes\Styles\CascadePrt"));
            //foreach (string filePath in filePaths)
            //{
            //    try
            //    {
            //        File.Delete(filePath);                    
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}

            Log.WriteLog(Source, "Application_End", "Application End");
        }
    }
}