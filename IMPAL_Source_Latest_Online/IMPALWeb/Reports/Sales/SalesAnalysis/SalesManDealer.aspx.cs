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

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesManDealer : System.Web.UI.Page
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
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (Session["BranchCode"] != null)
                pstrBranchCode = Session["BranchCode"].ToString();

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strFromDate = default(string);
                string strToDate = default(string);
                string str_head = default(string);

                DataSet ds = new DataSet();
                DataSet dsCol = new DataSet();

                strFromDate = txtFromDate.Text;
                strToDate = txtToDate.Text;
                string filename = "Salesman_Dealer" + ".xls";
                btnReport.Text = "Back";

                SalesTransactions salesItem = new SalesTransactions();

                ds = salesItem.GetSalesManDealerDetails(pstrBranchCode, strFromDate, strToDate);
                dsCol = salesItem.GetSalesManDealerHeaderDetails(pstrBranchCode, strFromDate, strToDate);

                if (dsCol.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                    string strBranchName = (string)Session["BranchName"];
                    str_head = "<center><b><font size='6'>Report of the branch " + strBranchName + " from " + string.Format("{0:dd/MM/yyyy}", strFromDate) + "  -  " + string.Format("{0:dd/MM/yyyy}", strToDate) + "</font></b><br><br></center>";

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    Response.ContentType = "application/ms-excel";

                    str_head = str_head + "<table border='1' style='font-family:arial;font-size:14px'><tr style='background-color:#336699;color:white'>";
                    str_head = str_head + "<th>Date</th><th>Supplier</th>";

                    for (int i = 0; i < dsCol.Tables[0].Rows.Count; i++)
                    {
                        str_head = str_head + "<th colspan='2'>" + dsCol.Tables[0].Rows[i].ItemArray[0] + "</th>";
                    }

                    str_head = str_head + "</tr>";
                    Response.Write(str_head);
                    str_head = default(string);

                    str_head = "<tr style='background-color:#336699;color:white'><th></th><th></th>";
                    for (int i = 0; i < dsCol.Tables[0].Rows.Count; i++)
                    {
                        str_head = str_head + "<th>Amount</th><th>Count</th>";
                    }

                    str_head = str_head + "</tr>";
                    Response.Write(str_head);

                    if (ds.Tables.Count > 0)
                    {
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Records not found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Records not found');", true);
                }
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
