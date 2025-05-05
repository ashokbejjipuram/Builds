#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using AjaxControlToolkit;
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Miscellaneous
{
    public partial class DataTransferToBI : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;
        string selectionformula = string.Empty;
        string strselAccCode = string.Empty;
        SendDataToBI sendBIData = new SendDataToBI();

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Init", "Statement Of Account Page Init Method");
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "Page_Load", "Statement Of Account Page Load Method");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    int Cnt = UpdateDataTransferStatus();

                    if (Cnt == 0)
                    {
                        btnReport.Visible = true;
                        btnReportSalesData.Visible = false;

                        Cnt = UpdateSalesDataTransferStatus();

                        if (Cnt == 1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Stock Data is Already Transferred for Current Date. Please Transfer the Sales Data');", true);
                            btnReportSalesData.Visible = true;
                            btnReport.Visible = false;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Data is Already Transferred for Current Date. Please do Process on Next Day.');", true);
                        btnReport.Visible = false;
                        btnReportSalesData.Visible = false;
                        return;
                    }
                }                
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void Page_Unload(object sender, EventArgs e)
        {           
        }

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int Cnt = UpdateDataTransferStatus();

                if (Cnt == 0)
                {
                    sendBIData.SendingDataToBI(Session["UserID"].ToString(), 1);

                    btnReport.Visible = false;
                    btnReportSalesData.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Data Has been Transferred Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Data is Already Transferred for Current Date. Please do Process on Next Day.');", true);
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnReportSalesData_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                sendBIData.SendingDataToBI(Session["UserID"].ToString(), 2);

                btnReport.Visible = false;
                btnReportSalesData.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Sales Data Has been Transferred Successfully.');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #endregion

        public int UpdateDataTransferStatus()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int Cnt = 0;

            try
            {
                DbCommand cmd = ImpalDB.GetSqlStringCommand("Select COUNT(1) from BI_Data_Transfer_Status WITH (NOLOCK) Where convert(date,datestamp,103)=convert(date,GETDATE(),103) and Stock_Update='A' and Sales_Update='A'");
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SendDataToBI), exp);
            }

            return Cnt;
        }

        public int UpdateSalesDataTransferStatus()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int Cnt = 0;

            try
            {
                DbCommand cmd = ImpalDB.GetSqlStringCommand("Select COUNT(1) from BI_Data_Transfer_Status WITH (NOLOCK) Where convert(date,datestamp,103)=convert(date,GETDATE(),103) and Stock_Update='A' and Sales_Update IS NULL");
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SendDataToBI), exp);
            }

            return Cnt;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("DataTransferToBI.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}