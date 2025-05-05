#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion 

namespace IMPALWeb.Reports.Finance.Accounts_Payable
{
    public partial class ChequeSlipReport : System.Web.UI.Page
    {
        string strChequeNumber = default(string);
        //string strChequeDate = default(string);
        string strChequeSlipNumber = default(string);

        #region Page Init
        /// <summary>
        /// Page Init Event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page Load");
            try
            {
                if (!IsPostBack)
                {
                    if (crChequeSlip != null)
                    {
                        crChequeSlip.Dispose();
                        crChequeSlip = null;
                    }

                    //PreviousPage.Visible = false;
                    //Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                    //DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("ddlChequeSlipNo");
                    //strChequeNumber = SourceDDl.SelectedValue;
                    //TextBox SourceText = (TextBox)placeHolder.FindControl("txtChequeSlipNo");
                    //strChequeSlipNumber = SourceText.Text;
                    // TextBox SourceText1 = (TextBox)placeHolder.FindControl("txtChequeDate");
                    //strChequeDate = SourceText1.Text;
                    strChequeNumber = Session["ChequeSlipNumber"].ToString();
                    GenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crChequeSlip != null)
            {
                crChequeSlip.Dispose();
                crChequeSlip = null;
            }
        }
        protected void crChequeSlip_Unload(object sender, EventArgs e)
        {
            if (crChequeSlip != null)
            {
                crChequeSlip.Dispose();
                crChequeSlip = null;
            }
        }

        #region Generation of Report
        /// <summary>
        /// Method to select reports for generation based on Indent Type
        /// </summary>
        protected void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "GenerateSelectionFormula()", "Inside GenerateSelectionFormula()");
            try
            {
                string strSelectionFormula = default(string);
                string strChequeSlipNo = default(string);
               
                //string strChequeSlipDate = default(string);
                //string strCryDate = default(string);

                strChequeSlipNo = "{Cheque_Slip_Header.Cheque_Slip_Number}";
                //strChequeSlipDate = "{Cheque_Slip_Header.Cheque_Slip_date}";
                //if(!string.IsNullOrEmpty(strChequeDate))
                //{
                //strCryDate = "Date (" + DateTime.ParseExact(strChequeDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                //}
                if (!string.IsNullOrEmpty(strChequeNumber)&& strChequeNumber!="0")
                {
                    //strChequeNumber = txtChequeSlipNo.Text;
                    //if (SourceDDl.SelectedValue != "0")
                    //    strChequeNumber = SourceDDl.SelectedValue;
                    //string strChequeDate = txtChequeDate.Text;
                    strSelectionFormula = strChequeSlipNo + "= '" + strChequeNumber + "'";
                }
                else 
                {
                    strSelectionFormula = strChequeSlipNo + "= '" + strChequeSlipNumber + "'";
                }
                //else
                //{
                //    strSelectionFormula = strChequeSlipDate + "= " + strCryDate;
                //}
                if ((string)Session["RoleCode"] == "BEDP")
                {
                    crChequeSlip.ReportName = "ChequeSlipReport_Branch";
                }
                else
                {
                    crChequeSlip.ReportName = "ChequeSlipReport_COR";
                }
                crChequeSlip.RecordSelectionFormula = strSelectionFormula;
                crChequeSlip.GenerateReport();               
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Back Button Click
        /// <summary>
        /// Back Button Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("~/Transactions/Finance/Payable/ChequeSlip.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #endregion
    }
}
