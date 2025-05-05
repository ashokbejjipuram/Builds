<%@ WebHandler Language="C#" Class="LoadGlSub" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;
public class LoadGlSub : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string GlMain = context.Request.QueryString["GlMain"];
        string Filter = context.Request.QueryString["Filter"];
        chartAccCode acc = new chartAccCode();
        List<GlSub> GlBranch;

        if (Filter == "('GSTOPTIONS')")
            GlBranch = acc.GetGlSubGSTOptions(GlMain);
        else
            GlBranch = acc.GetGlSub(GlMain);

        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(GlBranch);
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