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
    public partial class ItemRateReport : System.Web.UI.Page
    {

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    crItemRate.RecordSelectionFormula = "";
                    crItemRate.GenerateReport();
                    //fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page init
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
                Response.Redirect("ItemRate.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        //#region Generate Selection Formula
        //public void fnGenerateSelectionFormula()
        //{
        //    String strRateItemCode = default(string);
        //    string strItemRate = default(string);
        //    string strSelectionFormula = default(string);

        //    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
        //    DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpItemRate");

        //    strItemRate = SourceDDl.SelectedValue;
        //    strRateItemCode = "{Rate_Master.item_code}";

        //    if (strItemRate == "ALL")
        //        strSelectionFormula = null;
        //    else 
        //        strSelectionFormula = strRateItemCode + "=" + " '" + strItemRate + "'";
        //    crItemRate.RecordSelectionFormula = strSelectionFormula;
        //    crItemRate.GenerateReport();
        //}
        //#endregion
    }
}
