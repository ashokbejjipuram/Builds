using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;
using IMPALLibrary;
using IMPALLibrary.Masters;

public partial class Service : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetPartNumber(string prefixText, int count)
    {
        Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        List<string> PartNumberList = new List<string>();
        try
        {
            List<SupplierDetails> supplier = (List<SupplierDetails>)HttpContext.Current.Session["SupplierDetails"];
            List<SupplierDetails> PartnumberList = null;
            if (!prefixText.Equals("%"))
                PartnumberList = supplier.Where(p => p.Supplier_Part_Number.StartsWith(prefixText, true, new CultureInfo("en-US"))).ToList();
            else
                PartnumberList = supplier;
            if (PartnumberList.Count > 0)
                PartNumberList = PartnumberList.Select(c => c.Supplier_Part_Number).ToList();
        }
        catch (Exception exp)
        {
            Log.WriteException(Source, exp);
        }
        return PartNumberList;
    }
}
