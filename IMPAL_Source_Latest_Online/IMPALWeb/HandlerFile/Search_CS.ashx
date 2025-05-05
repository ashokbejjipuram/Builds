<%@ WebHandler Language="C#" Class="Search_CS" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using IMPALLibrary;
using IMPALLibrary.Masters;
using System.Linq;
using System.Globalization;



public class Search_CS : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        try
        {
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string prefixText = context.Request.QueryString["q"];
            StringBuilder sb = new StringBuilder();
            List<string> PartNumberList = new List<string>();
            List<SupplierDetails> supplier = (List<SupplierDetails>)context.Session["SupplierDetails"];
            List<SupplierDetails> PartnumberList = null;
            if (!prefixText.Equals("%"))
            {
                PartnumberList = supplier.Where(p => p.Supplier_Part_Number.StartsWith(prefixText, true, new CultureInfo("en-US"))).ToList();
            }
            else
                PartnumberList = supplier;
            if (PartnumberList.Count > 0)
            {
                foreach (var item in PartnumberList)
                {
                    sb.Append(string.Format("{0}|{1}", item.Supplier_Part_Number, Environment.NewLine));
                }
                context.Response.Write(sb.ToString());
            }
        }
        catch (Exception exp)
        {
            Log.WriteException(Source, exp);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}


   