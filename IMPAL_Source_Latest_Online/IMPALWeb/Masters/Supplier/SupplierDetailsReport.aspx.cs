#region Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion
namespace IMPALWeb.Masters.Supplier
{
    public partial class SupplierDetailsReport : System.Web.UI.Page
    {
        #region Page_Init
        /// <summary>
        /// To initialize page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
#endregion
        #region Page_Load
        /// <summary>
        /// To load page.Report is generated during page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack && PreviousPage !=null)
                {
                    crSupplierDetails.ReportName = "Impal_Supplier";
                    string strSupplierCode = "{Supplier_Master.Supplier_Code}";

                    Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    DropDownList SourceText = (DropDownList)placeHolder.FindControl("drpSupplierCode");
                    //TextBox SourceText = (TextBox)placeHolder.FindControl("txtCode");
                    string strSupplierCodeVal = SourceText.SelectedValue;
                   
                    // string strSupplierCodeVal = (string)Request.QueryString["SupplierCode"];
                    string strSelectionFormula = string.Empty;
                    if (strSupplierCodeVal != string.Empty)
                    {
                        strSupplierCodeVal = strSupplierCodeVal.Substring(0, 3);
                        strSelectionFormula = strSupplierCode + "=" + " '" + strSupplierCodeVal + "'";
                    }
                    crSupplierDetails.RecordSelectionFormula = strSelectionFormula;
                    crSupplierDetails.GenerateReport();
                    if (crSupplierDetails.Visible == false)
                        crSupplierDetails.Visible = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }


        }
#endregion
        #region Back Button Click
        /// <summary>
        /// To redirect to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("SupplierDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}
