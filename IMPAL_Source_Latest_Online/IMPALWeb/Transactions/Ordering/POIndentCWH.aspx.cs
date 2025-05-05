using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
using System.Data;
using System.Globalization;
using log4net;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;


namespace IMPALWeb
{
    public partial class POIndentCWH : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private const string serverDownloadFolder = "Downloads\\WorkSheet";
        private string filePath = string.Empty;
        private string fileName = string.Empty;
        private string strPONumber = default(string);
        private string Indicator = default(string);
        private string strPONumberField = default(string);

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCWH), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!Page.IsPostBack)
                {
                    if (Session["BranchCode"] != null)
                        strBranchCode = Session["BranchCode"].ToString();
                    fnPopulateIndentType();
                    btnReportPDF.Visible = false;
                    btnReportExcel.Visible = false;
                    btnReportRTF.Visible = false;

                    divItemDetailsExcel.Attributes.Add("style", "display:none");
                }

                BtnSubmit.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnPopulateIndentType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("POIndentCWH");
                ddlIndentType.DataSource = oList;
                ddlIndentType.DataValueField = "DisplayValue";
                ddlIndentType.DataTextField = "DisplayText";
                ddlIndentType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlIndentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlIndentType.SelectedIndex > 0)
                {
                    IMPALLibrary.Transactions.POIndentCWHTran ponumb = new IMPALLibrary.Transactions.POIndentCWHTran();
                    ddlIndentNumber.DataSource = ponumb.GetIndentNumber(ddlIndentType.SelectedValue, strBranchCode);
                    ddlIndentNumber.DataTextField = "IndentNumber";
                    ddlIndentNumber.DataValueField = "IndentNumber";
                    ddlIndentNumber.DataBind();
                }
                else
                {
                    ddlIndentNumber.DataSource = null;
                    ddlIndentNumber.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public DataTable ConstructColumn(DataTable objPOItemTable)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataColumn dtColPartNumber = new DataColumn("PartNumber", typeof(string));
                DataColumn dtColItemDesc = new DataColumn("ItemDesc", typeof(string));
                DataColumn dtColStockOnHand = new DataColumn("StockOnHand", typeof(string));
                DataColumn dtColPendingOrderQty = new DataColumn("PendingOrderQty", typeof(string));
                DataColumn dtColDocOnHand = new DataColumn("DocOnHand", typeof(string));
                DataColumn dtColAvgSales = new DataColumn("AvgSales", typeof(string));
                DataColumn dtColCurMonthSales = new DataColumn("CurMonthSales", typeof(string));
                DataColumn dtColToOrderQty = new DataColumn("ToOrderQty", typeof(string));
                DataColumn dtColToOrderQtyAddl = new DataColumn("ToOrderQtyAddl", typeof(string));
                DataColumn dtColPackQty = new DataColumn("PackQty", typeof(string));
                DataColumn dtColAcceptedQty = new DataColumn("AcceptedQty", typeof(string));
                DataColumn dtColItemCode = new DataColumn("ItemCode", typeof(string));
                DataColumn dtColVehTypeDesc = new DataColumn("VehTypeDesc", typeof(string));

                objPOItemTable.Columns.Add(dtColPartNumber);
                objPOItemTable.Columns.Add(dtColItemDesc);
                objPOItemTable.Columns.Add(dtColStockOnHand);
                objPOItemTable.Columns.Add(dtColPendingOrderQty);
                objPOItemTable.Columns.Add(dtColDocOnHand);
                objPOItemTable.Columns.Add(dtColAvgSales);
                objPOItemTable.Columns.Add(dtColCurMonthSales);
                objPOItemTable.Columns.Add(dtColToOrderQty);
                objPOItemTable.Columns.Add(dtColToOrderQtyAddl);
                objPOItemTable.Columns.Add(dtColPackQty);
                objPOItemTable.Columns.Add(dtColAcceptedQty);
                objPOItemTable.Columns.Add(dtColItemCode);
                objPOItemTable.Columns.Add(dtColVehTypeDesc);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objPOItemTable;
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;
                POIndentCWHTran objPOIndent = new POIndentCWHTran();

                fileName = objPOIndent.GetIndentExcelFileName(strBranchCode, ddlIndentNumber.SelectedValue, ddlIndentType.SelectedValue);

                if (fileName == "")
                {
                    BtnSubmit.Visible = false;
                    divItemDetailsExcel.Visible = false;
                    FileStatusMsg.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('Data doesnt exists. Please regenerate New Worksheet and Process the same.');", true);
                    return;
                }

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    if (btnFileUpload.HasFile)
                    {
                        filePath = @"D:\Downloads\WorkSheet";

                        string fileName = btnFileUpload.FileName;

                        if (File.Exists(filePath + "\\" + fileName))
                            File.Delete(filePath + "\\" + fileName);

                        btnFileUpload.SaveAs(filePath + "\\" + fileName);

                        if (File.Exists(filePath + "\\" + fileName))
                        {
                            string myexceldataquery = "Select branch_code,Indent_Number,Supplier_Part_Number,To_Order_Qty,ABCFMS_Status,Accepted_Qty from [sheet1$]";
                            string ExcelConnectionString = "";
                            string sqlTotQuery = "";
                            int AcceptedQty = 0;

                            if (fileName.ToString().EndsWith("s"))
                                ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;OLE DB Services=-4;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                            else
                                ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;OLE DB Services=-4;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                            OleDbConnection OledbConn = new OleDbConnection(ExcelConnectionString);
                            OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                            OledbConn.Open();
                            OleDbDataReader dr = OledbCmd.ExecuteReader();
                            while (dr.Read())
                            {
                                if (dr[5].ToString().Trim() == "")
                                    AcceptedQty = 0;
                                else
                                    AcceptedQty = Convert.ToInt32(dr[5].ToString());

                                if (AcceptedQty != 0)
                                {
                                    Database ImpalDb = DataAccess.GetDatabase();
                                    sqlTotQuery = "Usp_UpdTempWorkSheetExcel '" + dr[0] + "','" + dr[1] + "','" + dr[2] + "'," + dr[3] + ",'" + dr[4] + "','" + fileName + "'," + AcceptedQty + ",'" + ddlIndentType.SelectedValue + "'";
                                    DbCommand cmd = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                                    ImpalDb.ExecuteNonQuery(cmd);
                                }
                            }

                            OledbConn.Close();

                            if (fileName == "" || fileName == null)
                            {
                                FileStatusMsg.Text = "<font style='color: red' size='4'><b>WorkSheet Excel File Doesn't Exist. Please Regenerate the Same</b></font>";
                                divItemDetailsExcel.Attributes.Add("style", "display:inline");
                                BtnSubmit.Enabled = false;
                                ddlIndentNumber.Enabled = true;
                                ddlIndentType.Enabled = true;
                            }
                            else
                            {
                                if (File.Exists(filePath + "\\" + fileName))
                                {
                                    FileStatusMsg.Text = "<font style='color: Green' size='4'><b>WorkSheet Excel File Has been Uploaded. Please Process the Same.</b></font>";
                                    divItemDetailsExcel.Attributes.Add("style", "display:inline");
                                    btnFileUpload.Visible = false;
                                    btnUploadExcel.Visible = false;
                                    ddlIndentNumber.Enabled = false;
                                    ddlIndentType.Enabled = false;
                                    lblDate.Visible = false;
                                    BtnSubmit.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sResult = string.Empty;
                
                filePath = @"D:\Downloads\WorkSheet";

                if (ddlIndentNumber.SelectedValue != "0" && ddlIndentType.SelectedValue != "0")
                {
                    DataTable objPODataTbl = new DataTable();
                    DataRow dtRowItem;

                    List<POIndentDetail> objPartItems = new List<POIndentDetail>();
                    POIndentCWHTran objPOIndent = new POIndentCWHTran();

                    fileName = objPOIndent.GetIndentExcelFileName(strBranchCode, ddlIndentNumber.SelectedValue, ddlIndentType.SelectedValue);

                    if (fileName == "")
                    {
                        BtnSubmit.Visible = false;
                        divItemDetailsExcel.Visible = false;
                        FileStatusMsg.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('Data doesnt exists. Please regenerate New Worksheet and Process the same.');", true);
                        return;
                    }

                    string AvgSales = string.Empty;

                    string[] SupplierCode = ddlIndentNumber.SelectedItem.Text.Split('/');

                    objPartItems = objPOIndent.ListIndentItems(ddlIndentNumber.SelectedItem.Text, ddlIndentType.SelectedValue.ToString(), strBranchCode);

                    objPODataTbl = ConstructColumn(objPODataTbl);

                    for (int i = 0; i <= objPartItems.Count - 1; i++)
                    {
                        dtRowItem = objPODataTbl.NewRow();
                        objPODataTbl.Rows.Add(dtRowItem);

                        objPODataTbl.Rows[i]["PartNumber"] = string.IsNullOrEmpty(objPartItems[i].PartNumber) ? "" : objPartItems[i].PartNumber.ToString();
                        objPODataTbl.Rows[i]["ItemDesc"] = string.IsNullOrEmpty(objPartItems[i].ItemDesc) ? "" : objPartItems[i].ItemDesc.ToString();
                        objPODataTbl.Rows[i]["StockOnHand"] = string.IsNullOrEmpty(objPartItems[i].StockOnHand) ? "" : objPartItems[i].StockOnHand.ToString();
                        objPODataTbl.Rows[i]["PendingOrderQty"] = string.IsNullOrEmpty(objPartItems[i].PendingOrderQty) ? "" : objPartItems[i].PendingOrderQty.ToString();
                        objPODataTbl.Rows[i]["DocOnHand"] = string.IsNullOrEmpty(objPartItems[i].DocOnHand) ? "" : objPartItems[i].DocOnHand.ToString();

                        AvgSales = string.IsNullOrEmpty(objPartItems[i].AvgSales.ToString()) ? "" : objPartItems[i].AvgSales.ToString();
                        
                        objPODataTbl.Rows[i]["AvgSales"] = int.Parse(AvgSales.ToString(), NumberStyles.Number);
                        objPODataTbl.Rows[i]["CurMonthSales"] = string.IsNullOrEmpty(objPartItems[i].CurMonthSales) ? "" : objPartItems[i].CurMonthSales.ToString();
                        objPODataTbl.Rows[i]["ToOrderQty"] = string.IsNullOrEmpty(objPartItems[i].ToOrderQty) ? "0" : objPartItems[i].ToOrderQty.ToString();
                        objPODataTbl.Rows[i]["PackQty"] = string.IsNullOrEmpty(objPartItems[i].PackQty) ? "" : objPartItems[i].PackQty.ToString();
                        objPODataTbl.Rows[i]["ItemCode"] = string.IsNullOrEmpty(objPartItems[i].ItemCode) ? "" : objPartItems[i].ItemCode.ToString();
                        objPODataTbl.Rows[i]["AcceptedQty"] = string.IsNullOrEmpty(objPartItems[i].AcceptedQty) ? "" : objPartItems[i].AcceptedQty.ToString();
                        objPODataTbl.Rows[i]["ToOrderQtyAddl"] = string.IsNullOrEmpty(objPartItems[i].ToOrderQtyAddl) ? "" : objPartItems[i].ToOrderQtyAddl.ToString();
                    }

                    sResult = objPOIndent.SubmitPOIndentCWH(objPODataTbl, ddlIndentNumber.SelectedItem.Text, ddlIndentType.SelectedValue.ToString(), strBranchCode);

                    if (sResult == "0")
                    {
                        ViewState["CurrentTable"] = "";

                        if (File.Exists(filePath + "\\" + fileName))
                            File.Delete(filePath + "\\" + fileName);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Process completed');", true);
                        ddlIndentNumber.Enabled = false;
                        ddlIndentType.Enabled = false;
                        BtnSubmit.Visible = false;
                        btnReportPDF.Visible = true;
                        btnReportExcel.Visible = true;
                        btnReportRTF.Visible = true;
                    }
                    else if (sResult != "0")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + sResult + "');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("POIndentCWH.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void ResetForm()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlIndentNumber.SelectedIndex = 0;
                ddlIndentType.SelectedIndex = 0;
                BtnSubmit.Enabled = false;
                BtnSubmit.Visible = true;
                btnReportPDF.Visible = false;
                btnReportExcel.Visible = false;
                btnReportRTF.Visible = false;
                ddlIndentNumber.Enabled = true;
                ddlIndentType.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlIndentNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlIndentNumber.SelectedIndex > 0)
                {
                    divItemDetailsExcel.Attributes.Add("style", "display:block");
                }
                else
                {
                    divItemDetailsExcel.Attributes.Add("style", "display:none");
                    BtnSubmit.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }

        protected void GenerateAndExportReport(string fileType)
        {
            crPurchaseOrderWorkSheet.ReportName = "Purchase_worksheet";

            string strSelectionFormula = default(string);
            strPONumber = ddlIndentNumber.SelectedValue;

            string strBranchCodeField = default(string);
            Database ImpalDB = DataAccess.GetDatabase();

            strBranchCodeField = "{Purchase_Order_Header.branch_code}";
            strPONumberField = "{Purchase_Order_Header.PO_number}";

            strSelectionFormula = strPONumberField + "=" + " " + "'" + strPONumber + "' and " + strBranchCodeField + "='" + strBranchCode + "'";

            crPurchaseOrderWorkSheet.RecordSelectionFormula = strSelectionFormula;
            crPurchaseOrderWorkSheet.GenerateReportAndExportA4(fileType);
        }
    }
}
