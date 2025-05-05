<%@ WebHandler Language="C#" Class="LoadBranch" %>

using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;

public class LoadBranch : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string GlMain = context.Request.QueryString["GlMain"];
        string GlSub = context.Request.QueryString["GlSub"];
        string GlAcc = context.Request.QueryString["GlAcc"];
        chartAccCode acc = new chartAccCode();
        List<GlBranch> GlBranList = acc.GetGlBranch(GlMain, GlSub, GlAcc);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(GlBranList);
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