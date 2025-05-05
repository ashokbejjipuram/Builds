using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
using IMPALLibrary.Transactions;
using IMPALLibrary.Common;

namespace IMPALWeb
{
    public partial class EWayBillReport : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        protected void Page_Init(object sender, EventArgs e)
        {           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fnPopulateInvoiceType();
            }

            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
        }

        protected void fnPopulateInvoiceType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInvoiceType()", "Entering fnPopulateInvoiceType()");

            string strFileName = "InvoiceType";
            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strFileName);
                ddlInvoiceType.DataSource = drop;
                ddlInvoiceType.DataValueField = "DisplayValue";
                ddlInvoiceType.DataTextField = "DisplayText";
                ddlInvoiceType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                DataSet ds = new DataSet();

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;

                string str_head = "";
                string filename = "";
                string Ind = "";

                if (ddlInvoiceType.SelectedValue.Equals("D"))
                    Ind = "Sales";
                else
                    Ind = "STDN";

                filename = "EWayBillReport_" + Ind + "_" + string.Format("{0:yyyyMMdd}", strFromDate) + ".xls";
                btnReport.Text = "Back";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetEWayBillReport(strBranchCode, strFromDate, strToDate, ddlInvoiceType.SelectedValue);

                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>E-Way Bill Report " + Ind + " from " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " to " + string.Format("{0:dd/MM/yyyy}", strToDate) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr>");

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
    }
}
