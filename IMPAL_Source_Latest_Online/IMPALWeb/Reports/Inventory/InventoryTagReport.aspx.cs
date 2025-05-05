using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using System.Data.Common;
using IMPALLibrary;
using System.Data;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace IMPALWeb.Reports.Inventory
{
    public partial class InventoryTagReport : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    populatetagtypedll();
                    populatereporttypedll();
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
        protected void crInventoryTagReport_Unload(object sender, EventArgs e)
        {
        }

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

        #region populatetagtypedll
        public void populatetagtypedll()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "populatetagtypedll", "Entering populatetagtypedll");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("TagType");
                ddlTagType.DataSource = oList;
                ddlTagType.DataValueField = "DisplayValue";
                ddlTagType.DataTextField = "DisplayText";
                ddlTagType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populatereporttypedll
        public void populatereporttypedll()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "populatereporttypedll", "Entering populatereporttypedll");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType");
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

        #region ddlsuppcode_SelectedIndexChanged
        protected void ddlsuppcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            user.SupplierLine = ddlsuppcode.SelectedValue;
            user.SupplierDesc = ddlsuppcode.SelectedItem.Text;
        }
        #endregion

        #region user_SearchImageClicked
        protected void user_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "user_SearchImageClicked", "Entering user_SearchImageClicked");
            try
            {
                txtItemCode.Text = Session["ItemCode"].ToString();
                txtSuppPartNo.Text = Session["SupplierPartNumber"].ToString();
                Session["ItemCode"] = "";
                Session["SupplierPartNumber"] = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(strBranchCode))
                {
                    string strLineCode = ddlsuppcode.SelectedValue;
                    string strItemCode = txtItemCode.Text;
                    string strTagtype = ddlTagType.SelectedValue;
                    string strFromDate = DateTime.Now.ToString("yyyyMMdd");
                    DataSet ds = new DataSet();

                    StockAdjustment objSaveStockAdjustment = new StockAdjustment();

                    if (ddlReportType.SelectedValue == "Report")
                        ds = objSaveStockAdjustment.AddinventoryTag(strBranchCode, strLineCode, strItemCode, strTagtype, "G", "XL");
                    else
                        ds = objSaveStockAdjustment.AddinventoryTag(strBranchCode, strLineCode, strItemCode, strTagtype, "A", "XL");

                    string filename = strBranchCode + "_TagReport_" + strFromDate + ".xls";

                    string strBranchName = (string)Session["BranchName"];

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    Response.ContentType = "application/ms-excel";
                    Response.Write("<table Border='1' style='font-family:arial;font-size:14px'><tr>");

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
                if (exp.Message != "Thread was being aborted.")
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
            string strLineCode = ddlsuppcode.SelectedValue;
            string strItemCode = txtItemCode.Text;
            string strTagtype = ddlTagType.SelectedValue;
            string strFromDate = DateTime.Now.ToString("yyyyMMdd");
            DataSet ds = new DataSet();

            StockAdjustment objSaveStockAdjustment = new StockAdjustment();

            if (ddlReportType.SelectedValue == "Report")
                ds = objSaveStockAdjustment.AddinventoryTag(strBranchCode, strLineCode, strItemCode, strTagtype, "G", "PDF");
            else
                ds = objSaveStockAdjustment.AddinventoryTag(strBranchCode, strLineCode, strItemCode, strTagtype, "A", "PDF");

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
            string heading = strBranchName + " - Inventory Tag for the Day " + string.Format("{0:dd/MM/yyyy}", strFromDate);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strBranchCode + "_TagReport_" + DateTime.Now.ToString("ddMMyyyy_hhmmsstt") + ".pdf");
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
                Response.Redirect("InventoryTagReport.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}