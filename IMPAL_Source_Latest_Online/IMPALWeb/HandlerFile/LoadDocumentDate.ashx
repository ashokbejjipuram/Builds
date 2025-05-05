<%@ WebHandler Language="C#" Class="LoadDocumentDate" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary;
using IMPALLibrary.Transactions.Finance;
public class LoadDocumentDate : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string BranchCode = context.Request.QueryString["BranchCode"];
        string DocumentNumber = context.Request.QueryString["DocumentNumber"];
        string CustBrCode = context.Request.QueryString["CustBrCode"];
        string CustBranchInd = context.Request.QueryString["CustBranchInd"];
        DebitCredit objDebit = new DebitCredit();
        RefDocDate Refdate = new RefDocDate();
        Refdate = objDebit.GetReferenceDocNumberDate(BranchCode, DocumentNumber, CustBrCode, CustBranchInd);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(Refdate);
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