<%@ WebHandler Language="C#" Class="LoadCashPer" %>

using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections;
using IMPALLibrary.Transactions.Finance;

public class LoadCashPer : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string CustomerCode = context.Request.QueryString["CustomerCode"];
        string Indicator = context.Request.QueryString["Indicator"];
        string NoOfDays = context.Request.QueryString["NoOfDays"];
        string DelayValue = context.Request.QueryString["DelayValue"];
        string Status = context.Request.QueryString["Status"];
        string DocumentDate = context.Request.QueryString["DocumentDate"].ToString();
        string BranchCode = context.Session["BranchCode"].ToString();
        int No_Of_Days = int.Parse(NoOfDays) - int.Parse(DelayValue);
        
        CashDiscountDts acc = new CashDiscountDts();
        List<CashPercentage> CashPerCollection = acc.GetCashPercentage(CustomerCode, Indicator, BranchCode, No_Of_Days, Status, DocumentDate);
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
        new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(CashPerCollection);
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