#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion
namespace IMPALWeb.Masters.Item
{
    public partial class BranchItemPriceReport : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    //crBranchItemPrice.RecordSelectionFormula = "";
                    //crBranchItemPrice.GenerateReport();
                    fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region page init
        public void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Back button click
        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Inside btnBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("BranchItemPrice.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void fnGenerateSelectionFormula()
        {
            string strItemCode = default(string);
            string strSelectionFormula = default(string);

            Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
            //DropDownList ItemCode = (DropDownList)placeHolder.FindControl("drpSupplierLine");
            HiddenField SupplierCode = (HiddenField)placeHolder.FindControl("HdnSupplierLine");

            string strSupplier_Code = SupplierCode.Value;
            strItemCode = "mid({Supplier_Master.Supplier_Name},1,3)";

            if (strSupplier_Code == "")
                strSelectionFormula = null;
            else
                strSelectionFormula = strItemCode + "= '" + strSupplier_Code + "'";

            crBranchItemPrice.RecordSelectionFormula = strSelectionFormula;
            crBranchItemPrice.GenerateReport();
        }
        #endregion
    }
}
