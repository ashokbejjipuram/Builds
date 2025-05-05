#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;
using System.Configuration;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;
using IMPALLibrary;
#endregion

namespace IMPALWeb
{
    public partial class OutstandingDaysReport : System.Web.UI.Page
    {
        string strCustomerCode = default(string);
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

        #region PageLoad
        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page Load");
            try
            {
                if (!IsPostBack)
                {
                    strCustomerCode = Session["CustomerCode"].ToString();
                    GenerateSelectionFormula();

                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);

            }

        }
        #endregion

        #region Generation of Report
        /// <summary>
        /// Method to select reports for generation based on Indent Type
        /// </summary>
        protected void GenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strSelectionFormula = default(string);
                string strCustCode = default(string);
                strCustCode = "{Os_temp_outstanding.cust_code}";
                strSelectionFormula = strCustCode + "= '" + strCustomerCode + "'";
                crOutstanding.ReportName = "OustandingDays";
                crOutstanding.RecordSelectionFormula = strSelectionFormula;
                crOutstanding.GenerateReport();
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
                Response.Redirect("SalesInvoice.aspx", false);
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

