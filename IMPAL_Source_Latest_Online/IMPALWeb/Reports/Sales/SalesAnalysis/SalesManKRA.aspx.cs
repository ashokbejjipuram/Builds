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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesManKRA : System.Web.UI.Page
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
                btnBack.Attributes.Add("style", "display:none");
            }

            if (Session["BranchCode"] != null)
                pstrBranchCode = Session["BranchCode"].ToString();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Text = "Back";
                btnReportPDF.Visible = false;
                string strFromDate = default(string);
                DataSet ds = new DataSet();

                strFromDate = txtFromDate.Text;

                string str_head = "";
                string filename = string.Format("{0:yyyyMMdd}", strFromDate) + ".xls";

                SalesTransactions salesItem = new SalesTransactions();
                ds = salesItem.GetSalesManDetails(pstrBranchCode, strFromDate);
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Sales Man KRA for the Day " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " of " + strBranchName + "</font></b><br><br></center>";

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

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            string strFromDate = default(string);
            DataSet ds = new DataSet();

            strFromDate = txtFromDate.Text;

            SalesTransactions salesItem = new SalesTransactions();
            ds = salesItem.GetSalesManDetails(pstrBranchCode, strFromDate);
            string strBranchName = (string)Session["BranchName"];

            string[] columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();
            int count = columnNames.Length;
            object[] array = new object[count];

            ds.Tables[0].Rows.Add(array);

            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mStream);
            int cols = ds.Tables[0].Columns.Count;
            int rows = ds.Tables[0].Rows.Count;
            string heading = strBranchName + " - Sales Man KRA for the Day " + string.Format("{0:dd/MM/yyyy}", strFromDate);

            pdfDoc.Open();
            PdfPTable pdfTable = new PdfPTable(ds.Tables[0].Columns.Count);
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

            Chunk chunkHead = new Chunk(heading);
            var para = new Paragraph(chunkHead);
            para.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(para);
            pdfDoc.Add(new Paragraph(Environment.NewLine));

            for (int i = 0; i < cols; i++)
            {
                Chunk cellCols = new Chunk(ds.Tables[0].Columns[i].ColumnName, FontFactory.GetFont(FontFactory.COURIER, 13, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#336699"))));
                var col = new Phrase(cellCols);                
                pdfTable.AddCell(new PdfPCell(col));
            }

            for (int k = 0; k < rows; k++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Chunk cellRows = new Chunk(ds.Tables[0].Rows[k][j].ToString(), FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
                    var row = new Phrase(cellRows);
                    pdfTable.AddCell(new PdfPCell(row));
                }
            }

            pdfDoc.Add(pdfTable);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + pstrBranchCode + "_SalesMan_KRA_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
            Response.End();
            Response.Flush();
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("SalesManKRA.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}