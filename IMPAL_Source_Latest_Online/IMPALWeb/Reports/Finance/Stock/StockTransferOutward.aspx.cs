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

namespace IMPALWeb.Reports.Finance.Stock
{
    public partial class StockTransferOutward : System.Web.UI.Page
    {
        private string strBranchCode = default(string);

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
                    if (crstocktransferoutward_Detail != null)
                    {
                        crstocktransferoutward_Detail.Dispose();
                        crstocktransferoutward_Detail = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateReportType();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crstocktransferoutward_Detail != null)
            {
                crstocktransferoutward_Detail.Dispose();
                crstocktransferoutward_Detail = null;
            }
        }
        protected void crstocktransferoutward_Detail_Unload(object sender, EventArgs e)
        {
            if (crstocktransferoutward_Detail != null)
            {
                crstocktransferoutward_Detail.Dispose();
                crstocktransferoutward_Detail = null;
            }
        }

        #region Populate Report Type
        public void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Entering fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-StockTransfer");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Button Click
        protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "Summary")
            {
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:none");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");
            }
            else
            {
                btnReportPDF.Attributes.Add("style", "display:none");
                btnReportExcel.Attributes.Add("style", "display:none");
                btnReportRTF.Attributes.Add("style", "display:none");
                btnBack.Attributes.Add("style", "display:none");
                btnReport.Attributes.Add("style", "display:inline");
            }
        }

        #region Generate Button Click

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            if (ddlReportType.SelectedValue == "Detail")
            {
                GenerateAndExportReport(".pdf");
            }
            else if (ddlReportType.SelectedValue == "Summary")
            {
                if (crstocktransferoutward_Detail != null)
                {
                    crstocktransferoutward_Detail.Dispose();
                    crstocktransferoutward_Detail = null;
                }

                string strFromDate = default(string);
                DataSet ds = new DataSet();

                strFromDate = txtFromDate.Text;

                CashAndBankTransactions cbtrans = new CashAndBankTransactions();
                ds = cbtrans.GetSTDNInwardOutwardJournalDetails(strBranchCode, txtFromDate.Text, txtToDate.Text, "O");
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
                string heading = strBranchName + " - Stock Outward Journal Summary for the Period " + string.Format("{0:dd/MM/yyyy}", txtFromDate.Text) + " - " + string.Format("{0:dd/MM/yyyy}", txtToDate.Text);

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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strBranchCode + "_StockOutwardJournalSummary_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".pdf");
                Response.Clear();
                Response.BinaryWrite(mStream.ToArray());
                Response.End();
                Response.Flush();
            }
        }

        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            if (ddlReportType.SelectedValue == "Detail")
            {
                GenerateAndExportReport(".xls");
            }
            else if (ddlReportType.SelectedValue == "Summary")
            {
                if (crstocktransferoutward_Detail != null)
                {
                    crstocktransferoutward_Detail.Dispose();
                    crstocktransferoutward_Detail = null;
                }

                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                try
                {
                    btnReportPDF.Visible = false;
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    DataSet ds = new DataSet();

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    string str_head = "";
                    string filename = "StockOutwardJournalSummary_" + txtFromDate.Text.Replace("/", "") + "_" + DateTime.Now.ToString("hhmmsstt") + ".xls";

                    CashAndBankTransactions cbtrans = new CashAndBankTransactions();

                    ds = cbtrans.GetSTDNInwardOutwardJournalDetails(strBranchCode, strFromDate, strToDate, "O");
                    string strBranchName = (string)Session["BranchName"];
                    str_head = "<center><b><font size='6'>Stock Outward Journal Summary for the Period " + string.Format("{0:dd/MM/yyyy}", strFromDate) + " - " + string.Format("{0:dd/MM/yyyy}", strToDate) + "</font></b><br><br></center>";

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
        }
        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;

            if (ddlReportType.SelectedValue == "Detail")
            {
                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);

            if (ddlReportType.SelectedValue == "Detail")
                crstocktransferoutward_Detail.ReportName = "stocktransferoutward_Detail";

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string strSTDN_Brcode = default(string);
            string strSTDN_Date = default(string);

            strSTDN_Date = "{STDN_header.stdn_date}";
            strSTDN_Brcode = "{STDN_Header.Branch_code}";

            strCryFromDate = "Date(" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date(" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (strBranchCode == "CRP")
                strSelectionFormula = strSTDN_Date + ">=" + strCryFromDate + "and " + strSTDN_Date + "<=" + strCryToDate;
            else
                strSelectionFormula = strSTDN_Date + ">=" + strCryFromDate + "and " + strSTDN_Date + "<=" + strCryToDate + "and " + strSTDN_Brcode + "='" + strBranchCode + "'";

            strSelectionFormula = strSelectionFormula + " and {STDN_Header.From_Branch_code}={STDN_Header.Branch_code}";

            crstocktransferoutward_Detail.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crstocktransferoutward_Detail.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crstocktransferoutward_Detail.RecordSelectionFormula = strSelectionFormula;
            crstocktransferoutward_Detail.GenerateReportAndExport(fileType);
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("StockTransferOutward.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
