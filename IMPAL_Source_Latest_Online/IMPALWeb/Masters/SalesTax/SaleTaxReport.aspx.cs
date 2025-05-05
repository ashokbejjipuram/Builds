#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion


namespace IMPALWeb.Masters.SalesTax
{
    public partial class SaleTaxReport : System.Web.UI.Page
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
                    //crSalesTaxReport.RecordSelectionFormula = "";
                    //crSalesTaxReport.GenerateReport();
                    fnGenerateSelectionFormula();
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
                Response.Redirect("SaleTax.aspx", false);
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
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
            try
            {
                string strSelectionFormula = default(string);
                string strSalesCode = default(string);
                //string strSalesBranch = default(string);

                strSalesCode = "{sales_tax_master.Sales_Tax_Code}";

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpSTDesc");

                string strSalesTaxCode = SourceDDl.SelectedValue;

                if (strSalesTaxCode == "ALL")
                    strSelectionFormula = null;
                else
                    strSelectionFormula = strSalesCode + "= " + strSalesTaxCode;

                crSalesTaxReport.RecordSelectionFormula = strSelectionFormula;
                crSalesTaxReport.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

    }
}
