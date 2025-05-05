#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
#endregion 

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class FundRemittanceStmt : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Text = "Back";
                btnReportPDF.Visible = false;
                DataSet ds = new DataSet();

                string str_head = "";
                string filename = "FundRemittance_" + txtFromDate.Text.Replace("/", "") + "_" + DateTime.Now.ToString("hhmmsstt") + ".xls";

                CashAndBankTransactions cbtrans = new CashAndBankTransactions();

                ds = cbtrans.GetFundRemittanceDetails(strBranchCode, txtFromDate.Text, txtToDate.Text, "E");
                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Fund Remittance Statement of " + strBranchName + " for the Period " + string.Format("{0:dd/MM/yyyy}", txtFromDate.Text) + " - " + string.Format("{0:dd/MM/yyyy}", txtToDate.Text) + "</font></b><br><br></center>";

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
                IMPALLibrary.Log.WriteException(Source, exp);
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

            CashAndBankTransactions cbtrans = new CashAndBankTransactions();
            ds = cbtrans.GetFundRemittanceDetails(strBranchCode, txtFromDate.Text, txtToDate.Text, "P");
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
            string heading = strBranchName + " - Fund Remittance Statement for the Period " + string.Format("{0:dd/MM/yyyy}", txtFromDate.Text) + " - " + string.Format("{0:dd/MM/yyyy}", txtToDate.Text);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strBranchCode + "_FundRemittance_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
            Response.End();
            Response.Flush();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("FundsRemittanceStmt.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
