<%@ WebHandler Language="C#" Class="LoadChartAccountOthers" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;

public class LoadChartAccountOthers : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string GlMain = context.Request.QueryString["GlMain"];
        string GlSub = context.Request.QueryString["GlSub"];
        string GlAcc = context.Request.QueryString["GlAcc"];
        string Branch = context.Request.QueryString["Branch"];
        chartAccCode acc = new chartAccCode();
        ChartDetails ChartDetails = acc.GetChartDetailsOthers(GlMain, GlSub, GlAcc, Branch);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(ChartDetails);
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