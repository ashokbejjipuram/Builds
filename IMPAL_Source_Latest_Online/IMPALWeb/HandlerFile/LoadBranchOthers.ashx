<%@ WebHandler Language="C#" Class="LoadBranchOthers" %>

using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;

public class LoadBranchOthers : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string GlMain = context.Request.QueryString["GlMain"];
        string GlSub = context.Request.QueryString["GlSub"];
        string GlAcc = context.Request.QueryString["GlAcc"];

        string BranchCode;

        if (context.Session["BranchCodeOthers"] != null)
            BranchCode = context.Session["BranchCodeOthers"].ToString();
        else
            BranchCode = context.Session["BranchCode"].ToString();

        chartAccCode acc = new chartAccCode();

        List<GlBranch> GlBranList = acc.GetGlBranchOthers(GlMain, GlSub, GlAcc, BranchCode);
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