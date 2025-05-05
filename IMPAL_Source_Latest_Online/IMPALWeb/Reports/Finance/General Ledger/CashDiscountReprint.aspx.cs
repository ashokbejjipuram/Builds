#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Statement
{
    public partial class CashDiscountReprint : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            
            try
            {
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (cryCashDiscountReprint != null)
                    {
                        cryCashDiscountReprint.Dispose();
                        cryCashDiscountReprint = null;
                    }

                    divSelection.Visible = true;
                    divSelection.Visible = true;
                    btnBack.Attributes.Add("style", "display:none");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (cryCashDiscountReprint != null)
            {
                cryCashDiscountReprint.Dispose();
                cryCashDiscountReprint = null;
            }
        }
        protected void cryCashDiscountReprint_Unload(object sender, EventArgs e)
        {
            if (cryCashDiscountReprint != null)
            {
                cryCashDiscountReprint.Dispose();
                cryCashDiscountReprint = null;
            }
        }

        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string strSelectionFormula = default(string);
            strSelectionFormula = "";
            string strReportName = "CashDiscount";
            string documentNumber = "";

            var FromDate = txtFromDate.Text.Split('/');
            var ToDate = txtToDate.Text.Split('/');

            string Dfrom_Param = string.Format("Date({0},{1},{2})", FromDate[2], FromDate[1], FromDate[0]);
            string Dto_Param = string.Format("Date({0},{1},{2})", ToDate[2], ToDate[1], ToDate[0]);

            strSelectionFormula = "{Cash_Discount_Cust.CD_CN_Date}>=" + Dfrom_Param + " and {Cash_Discount_Cust.CD_CN_Date}<=" + Dto_Param;

            strSelectionFormula = strSelectionFormula + " and {Cash_Discount_Cust.Branch_Code}='" + strBranchCode + "'";

            cryCashDiscountReprint.ReportName = strReportName;
            cryCashDiscountReprint.RecordSelectionFormula = strSelectionFormula;
            cryCashDiscountReprint.GenerateReportAndExportA4();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("CashDiscountReprint.aspx", false);
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
