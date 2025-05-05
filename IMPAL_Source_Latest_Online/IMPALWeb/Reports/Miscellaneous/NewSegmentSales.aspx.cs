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
    public partial class NewSegmentSales : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        IMPALLibrary.Masters.Stock oStock = new IMPALLibrary.Masters.Stock();
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
                    LoadSegments();
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
                ddlReportType.DataSource = oCommon.GetDropDownListValues("NewSegmentSales-Report");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void LoadSegments()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {                
                ddlSegment.DataSource = oStock.GetSegmentStockSegmentListNew();
                ddlSegment.DataTextField = "ApplnSegmentDescription";
                ddlSegment.DataValueField = "ApplicationSegmentCode";
                ddlSegment.DataBind();
                ddlSegment.Items.Insert(0, "--All--");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlSupplier.Items.Clear();

                if (ddlSegment.SelectedIndex > 0)
                {
                    ddlSupplier.DataSource = oStock.GetSegmentSupplierCodes(ddlSegment.SelectedValue);
                    ddlSupplier.DataTextField = "SupplierName";
                    ddlSupplier.DataValueField = "SupplierCode";
                    ddlSupplier.DataBind();
                }
                else
                {
                    ddlSupplier.DataSource = oStock.GetAllSupplierCodes();
                    ddlSupplier.DataTextField = "SupplierName";
                    ddlSupplier.DataValueField = "SupplierCode";
                    ddlSupplier.DataBind();
                }

                ddlSupplier.Items.Insert(0, "--All--");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Enabled = false;
                string date1 = txtFromDate.Text.ToString();
                string date2 = txtToDate.Text.ToString();

                DataSet ds = new DataSet();
                string str_head = "";
                btnReport.Text = "Back";
                string filename = "NewSegment_" + ddlReportType.SelectedItem.Text + "_" + date1.Replace("/", "") + " to " + date2.Replace("/", "") + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetNewSegmentSalesReport(Session["BranchCode"].ToString(), date1, date2, ddlSegment.SelectedValue, ddlSupplier.SelectedValue, ddlReportType.SelectedValue);
                string strBranchName = salesItem.GetBranchName(Session["BranchCode"].ToString());
                str_head = "<center><b><font size='6'>" + ddlReportType.SelectedItem.Text + " for the Period " + string.Format("{0:dd/MM/yyyy}", date1) + " to " + string.Format("{0:dd/MM/yyyy}", date2) + " of " + strBranchName + "</font></b><br><br></center>";

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
    }
}
