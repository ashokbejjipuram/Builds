using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using IMPALLibrary.Transactions;

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class ConsignmentStockOut : System.Web.UI.Page
    {
        private string pstrBranchCode = default(string);
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fnPopulateSupplier();

            }
            if (Session["BranchCode"] != null)
                pstrBranchCode = Session["BranchCode"].ToString();

        }

        protected void fnPopulateSupplier()
        {
            Suppliers objsup = new Suppliers();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlSupplier.DataSource = objsup.GetAllSuppliers();
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierCode";
                ddlSupplier.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                objsup = null;
                Source = null;
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string SupplierCode = default(string);
                DataSet ds = new DataSet();

                strFromDate = txtFromDate.Text;
                SupplierCode = ddlSupplier.SelectedValue;

                string str_head = "";
                btnReport.Text = "Back";
                string filename = "Cosignment_Stock Out-" + string.Format("{0:ddMMyyyy}", strFromDate) + ".xls";

                StockTranRequest stockItem = new StockTranRequest();
                ds = stockItem.GetConsStockOutDetails(SupplierCode, strFromDate, pstrBranchCode);

                SalesTransactions salesItemBr = new SalesTransactions();
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Report for the Day " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " of " + strBranchName + "</font></b><br><br></center>";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.ContentType = "application/ms-excel";
                Response.Write(str_head);
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'>");

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Response.Write("<tr>");
                    DataRow row = ds.Tables[0].Rows[i];
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j == 1)
                            Response.Write("<td>'" + row[j] + "'</td>");
                        else
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
    }
}
