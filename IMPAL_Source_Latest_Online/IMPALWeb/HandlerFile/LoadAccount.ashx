<%@ WebHandler Language="C#" Class="LoadAccount" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;
public class LoadAccount : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string GlMain = context.Request.QueryString["GlMain"];
        string GlSub = context.Request.QueryString["GlSub"];

        string BranchCode;
        string AutoTODstatus;

        if (context.Session["BranchCodeOthers"] != null)
            BranchCode = context.Session["BranchCodeOthers"].ToString();
        else
            BranchCode = context.Session["BranchCode"].ToString();

        if (context.Session["AutoTODsuppliers"] != null)
            AutoTODstatus = context.Session["AutoTODsuppliers"].ToString();
        else
            AutoTODstatus = "";

        chartAccCode acc = new chartAccCode();
        List<GlAccount> GlAccount = acc.GetGlAccount(GlMain, GlSub, BranchCode, AutoTODstatus);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(GlAccount);
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

