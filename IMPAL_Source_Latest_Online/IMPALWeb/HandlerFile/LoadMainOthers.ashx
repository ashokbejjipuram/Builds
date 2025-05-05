<%@ WebHandler Language="C#" Class="LoadMainOthers" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;

public class LoadMainOthers : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string Filter = context.Request.QueryString["Filter"];
        chartAccCode acc = new chartAccCode();
        List<Glmain> GlBranch;

        if (Filter == "('GSTOPTIONS')")
            GlBranch = acc.GetGlmainGSTOptionsOthers(Filter);
        else
            GlBranch = acc.GetGlmainOthers(Filter);

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





