#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using System.Data.Common;
using System.Data;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Miscellaneous
{
    public partial class DealerCountDetails : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    LoadReportType();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void LoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("DealerCountDetails-Reports");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            //Database ImpalDB = DataAccess.GetDatabase();
            //DbCommand cmd = null;
            //int timediff = 0;

            //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = 'Misc-" + ddlReportType.SelectedValue + "' order by datestamp desc");
            //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            //timediff = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            //if (timediff > 0 && timediff <= 600)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('You Are Again Generating this Report With in no Time. Please Wait for 10 Minutes');", true);
            //    ddlSupplier.Enabled = false;
            //    ddlSupplier.SelectedIndex = 0;
            //    txtFromDate.Enabled = false;
            //    txtToDate.Enabled = false;
            //    ddlReportType.Enabled = false;
            //    btnReport.Attributes.Add("style", "display:none");
            //    btnBack.Attributes.Add("style", "display:inline");
            //    return;
            //}
            //else
            //{
                ReportsData reportsDt = new ReportsData();
                reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), "Misc-" + ddlReportType.SelectedValue);
            //}

            btnReport.Attributes.Add("style", "display:none");
            btnBack.Attributes.Add("style", "display:inline");

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                string date1 = txtFromDate.Text.ToString();
                string date2 = txtToDate.Text.ToString();

                DataSet ds = new DataSet();
                string str_head = "";
                string filename = "New_Dealer_Count_" + ddlReportType.SelectedItem.Text + "_" + date1.Replace("/", "") + " to " + date2.Replace("/", "") + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetDealerCountDetailsReport(Session["BranchCode"].ToString(), date1, date2, ddlReportType.SelectedValue);
                string strBranchName = salesItem.GetBranchName(Session["BranchCode"].ToString());
                str_head = "<center><b><font size='6'>New Dealer Count " + ddlReportType.SelectedItem.Text + " for the Period " + string.Format("{0:dd/MM/yyyy}", date1) + " to " + string.Format("{0:dd/MM/yyyy}", date2) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1'><tr>");

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                }
                Response.Write("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Response.Write("<tr>");
                    DataRow row = ds.Tables[0].Rows[i];
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {

                        Response.Write("<td>" + row[j] + "</td>");
                    }
                    Response.Write("</tr>");
                }

                Response.Write("</table>");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                Response.Flush();
                Response.End();
                Response.Close();
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("DealerCountDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
