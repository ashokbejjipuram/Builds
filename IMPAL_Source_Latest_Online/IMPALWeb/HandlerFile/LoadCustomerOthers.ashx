<%@ WebHandler Language="C#" Class="LoadCustomerOthers" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;
using IMPALLibrary.Masters;

public class LoadCustomerOthers : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string CustomerCode = context.Request.QueryString["GlAcc"];
        string BranchCode = context.Session["BranchCode"].ToString();
        ReceivableInvoice objReceivableInvoice = new ReceivableInvoice();        
        Customer CustomerDts = objReceivableInvoice.GetCustomerInfoByCustomerCodeOthers(CustomerCode, BranchCode);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(CustomerDts);
        context.Response.Write(sJSON);
        context.Response.ContentType = "application/json";
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}