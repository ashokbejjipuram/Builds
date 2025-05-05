#region Namespace Declaration
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class Stock_Value_Aging : System.Web.UI.Page
    {
        string sessionbrchcode = string.Empty;

        #region Page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
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
            try
            {
                if (Session["BranchCode"] != null)
                    sessionbrchcode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    fnPopulateReportType();
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

        #region Populate Report Type Dropdown
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReporttype.DataSource = oCommon.GetDropDownListValues("RepType-StockValueAging1");
                ddlReporttype.DataTextField = "DisplayText";
                ddlReporttype.DataValueField = "DisplayValue";
                ddlReporttype.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Text = "Back";
                btnReportPDF.Visible = false;
                string ReportType = string.Empty;
                string ReportInd = string.Empty;
                DataSet ds = new DataSet();

                Database ImpalDB = DataAccess.GetDatabase();

                string supplier = ddlSupplierCode.SelectedValue;

                if (ddlReporttype.SelectedValue == "0")
                {
                    ReportType = "A";
                }
                else if (ddlReporttype.SelectedValue == "1")
                {
                    ReportType = "B";
                    ReportInd = "Branch";
                }
                else if (ddlReporttype.SelectedValue == "2")
                {

                    ReportType = "S";
                    ReportInd = "Supplier";
                }

                string supp;

                if (supplier == "0")
                {
                    supp = "All_Lines";
                }
                else
                {
                    supp = supplier;
                }

                string str_head = "";
                string filename = "StockValue_Aging_" + supp + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".xls";

                StockTranRequest stockItem = new StockTranRequest();
                ds = stockItem.GetStockValueAgingDetails(sessionbrchcode, supplier, ReportType);

                string strBranchName = (string)Session["BranchName"];
                str_head = "<center><b><font size='6'>Stock Value Aging - " + ReportInd + " of " + strBranchName + "</font></b><br><br></center>";

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
            btnReport.Text = "Back";
            btnReportPDF.Visible = false;
            string ReportType = string.Empty;
            string ReportInd = string.Empty;
            DataSet ds = new DataSet();

            Database ImpalDB = DataAccess.GetDatabase();

            string supplier = ddlSupplierCode.SelectedValue;

            if (ddlReporttype.SelectedValue == "0")
            {
                ReportType = "A";
            }
            else if (ddlReporttype.SelectedValue == "1")
            {
                ReportType = "B";
                ReportInd = "Branch";
            }
            else if (ddlReporttype.SelectedValue == "2")
            {

                ReportType = "S";
                ReportInd = "Supplier";
            }

            string supp;

            if (supplier == "0")
            {
                supp = "All_Lines";
            }
            else
            {
                supp = supplier;
            }            

            StockTranRequest stockItem = new StockTranRequest();
            ds = stockItem.GetStockValueAgingDetails(sessionbrchcode, supplier, ReportType);
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
            string heading = strBranchName + " - Stock Value Aging - " + ReportInd;

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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + sessionbrchcode + "_StockValue_Aging_" + supp + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
            Response.End();
            Response.Flush();
        }
        #endregion

        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("StockValueAging.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
