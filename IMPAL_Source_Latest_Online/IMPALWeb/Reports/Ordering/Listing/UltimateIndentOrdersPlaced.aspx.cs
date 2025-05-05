#region Namespace
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class UltimateIndentOrdersPlaced : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
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
                    using (ObjectDataSource objBranch = new ObjectDataSource())
                    {
                        objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                        objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                        objBranch.SelectMethod = "GetAllOrdBranches";
                        objBranch.DataBind();
                        ddlOrdBranchName.DataSource = objBranch;
                        ddlOrdBranchName.DataTextField = "BranchName";
                        ddlOrdBranchName.DataValueField = "BranchCode";
                        ddlOrdBranchName.DataBind();
                        AddSelect(ddlOrdBranchName);
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
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
        protected void Page_Unload(object sender, EventArgs e)
        {
        }

        private void AddSelect(DropDownList ddl)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region btnReport_Click
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (btnReportExcel.Text == "Back")
                {
                    Server.ClearError();
                    Response.Redirect("UltimateIndentOrdersPlaced.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    DataSet ds = new DataSet();

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string str_head = "";
                    string filename = "UltimateIndentOrders_" + string.Format("{0:yyyyMMdd}", strToDate) + ".xls";

                    InwardTransactions inwardTransactions = new InwardTransactions();
                    ds = inwardTransactions.GetUltimateIndentOrdersPlaced(ddlOrdBranchName.SelectedValue, strFromDate, strToDate);
                    str_head = "<center><b><font size='6'>Ultimate Customer - Indent Orders from " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " to " + string.Format("{0:dd/MM/yyyy}", strToDate) + "</font></b><br><br></center>";

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    Response.ContentType = "application/ms-excel";
                    Response.Write(str_head);
                    Response.Write("<table border='1' style='font-family:arial;font-size:14px;'><tr>");

                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
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
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                Response.Flush();
                Response.End();
                Response.Close();
            }
        }       
        #endregion
    }
}