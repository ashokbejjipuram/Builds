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
using System.Configuration;

namespace IMPALWeb
{
    public partial class StatementPaymentUpload : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private string filePath = string.Empty;
        private string fileName = string.Empty;
        private bool isDownloadPathAvailable = false;
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();
        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StatementPaymentUpload), exp);
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
                }

                BtnSubmit.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public DataTable ConstructColumn(DataTable objPaymetTable)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataColumn dtColPBranchCode = new DataColumn("Branch_Code", typeof(string));
                DataColumn dtColSupplierCode = new DataColumn("Supplier_Code", typeof(string));
                DataColumn dtColInvoiceNumber = new DataColumn("Invoice_Number", typeof(string));
                DataColumn dtColInvoiceDate = new DataColumn("Invoice_Date", typeof(string));
                DataColumn dtColAmount = new DataColumn("Amount", typeof(string));
                DataColumn dtColIndicator = new DataColumn("Indicator", typeof(string));
                DataColumn dtColStatus = new DataColumn("Status", typeof(string));
                DataColumn dtColInwardNumber = new DataColumn("Inward_Number", typeof(string));
                DataColumn dtColInwardDate = new DataColumn("Inward_Date", typeof(string));

                objPaymetTable.Columns.Add(dtColPBranchCode);
                objPaymetTable.Columns.Add(dtColSupplierCode);
                objPaymetTable.Columns.Add(dtColInvoiceNumber);
                objPaymetTable.Columns.Add(dtColInvoiceDate);
                objPaymetTable.Columns.Add(dtColAmount);
                objPaymetTable.Columns.Add(dtColIndicator);
                objPaymetTable.Columns.Add(dtColStatus);
                objPaymetTable.Columns.Add(dtColInwardNumber);
                objPaymetTable.Columns.Add(dtColInwardDate);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return objPaymetTable;
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;

                if (btnFileUpload.HasFile)
                {
                    filePath = @"D:\Downloads\SupplierPayment";

                    string filePathDest = @"D:\Downloads\SupplierPayment\Old";

                    int a = 0;

                    string fileName = btnFileUpload.FileName;

                    string dest = Path.Combine(filePathDest, fileName);

                    if (File.Exists(filePath + "\\" + fileName))
                    {
                        while (File.Exists(dest))
                        {
                            a++;
                            dest = Path.Combine(filePathDest, fileName.Replace(".xls", " (" + a + ").xls"));
                        }

                        File.Copy(Path.Combine(filePath, fileName), dest);
                    }

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    ViewState["fileName"] = filePath + "\\" + fileName;

                    if (File.Exists(filePath + "\\" + fileName))
                    {
                        string myexceldataquery = "Select Branch_Code,Supplier_Code,Invoice_Number,Invoice_Date,Invoice_Amount,Pymt_Indicator,Pymt_Status,Inward_Number,Inward_Date from [sheet1$]";
                        string ExcelConnectionString = "";
                        string sqlTotQuery = "";

                        if (fileName.ToString().EndsWith("s"))
                            ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                        else
                            ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                        OleDbConnection OledbConn = new OleDbConnection(ExcelConnectionString);
                        OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                        OledbConn.Open();
                        OleDbDataReader dr = OledbCmd.ExecuteReader();

                        Database ImpalDb = DataAccess.GetDatabase();
                        sqlTotQuery = "Truncate table Corporate_Payment_Detail_Statementwise";
                        DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd1);
                        cmd1 = null;

                        using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["IMPALDatabase"].ConnectionString))
                        {
                            sqlcon.Open();

                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlcon))
                            {
                                bulkCopy.ColumnMappings.Add("Branch_Code", "Branch_Code");
                                bulkCopy.ColumnMappings.Add("Supplier_Code", "Supplier_Code");
                                bulkCopy.ColumnMappings.Add("Invoice_Number", "Invoice_Number");
                                bulkCopy.ColumnMappings.Add("Invoice_Date", "Invoice_Date");
                                bulkCopy.ColumnMappings.Add("Invoice_Amount", "Invoice_Value");
                                bulkCopy.ColumnMappings.Add("Pymt_Indicator", "Pymt_Indicator");
                                bulkCopy.ColumnMappings.Add("Pymt_Status", "Pymt_Status");
                                bulkCopy.ColumnMappings.Add("Inward_Number", "Inward_Number");
                                bulkCopy.ColumnMappings.Add("Inward_Date", "Inward_Date");
                                bulkCopy.DestinationTableName = "Corporate_Payment_Detail_Statementwise";
                                bulkCopy.WriteToServer(dr);
                            }
                        }

                        dr.Close();
                        dr.Dispose();

                        //while (dr.Read())
                        //{
                        //    sqlTotQuery = "Insert into Corporate_Payment_Detail_Statementwise values (,'" + dr[1] + "','" + dr[2] + "',convert(date,'" + dr[3] + "',103)," + dr[4] + ",'" + dr[5] + "','" + dr[6] + "','" + dr[7] + "',convert(date,'" + dr[8] + "',103),GETDATE(),'" + fileName + "')";
                        //    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        //    ImpalDb.ExecuteNonQuery(cmd1);
                        //    cmd1 = null;
                        //}

                        OledbConn.Close();

                        if (fileName == "" || fileName == null)
                        {
                            FileStatusMsg.Text = "<font style='color: red' size='3'><b>Statement Payment Excel File Doesn't Exist. Please Regenerate the Same</b></font>";
                            divItemDetailsExcel.Attributes.Add("style", "display:inline");
                            BtnSubmit.Enabled = false;
                        }
                        else
                        {
                            if (File.Exists(filePath + "\\" + fileName))
                            {
                                DataSet ds = new DataSet();
                                cmd1 = ImpalDb.GetStoredProcCommand("usp_CheckInv_Corporate_Payment_Supplier");
                                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ds = ImpalDb.ExecuteDataSet(cmd1);
                                cmd1 = null;

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    FileStatusMsg.Text = "<font style='color: Red' size='4'><b>Statement Payment Excel File Has been Uploaded. Please Check the Duplicate Invoices which cannot be processed.</b></font>";

                                    string filename = "Duplicate_" + fileName;

                                    string filePath1 = @"D:\Downloads\SupplierPayment\Download";

                                    isDownloadPathAvailable = uitility.CheckDirectoryExists(filePath1);
                                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

                                    if (File.Exists(filePath1 + "\\" + filename))
                                        File.Delete(filePath1 + "\\" + filename);

                                    object missing = System.Reflection.Missing.Value;
                                    Microsoft.Office.Interop.Excel.Application xl = null;
                                    Microsoft.Office.Interop.Excel._Workbook wb = null;
                                    Microsoft.Office.Interop.Excel._Worksheet ws = null;
                                    xl = new Microsoft.Office.Interop.Excel.Application();
                                    xl.SheetsInNewWorkbook = 1;
                                    xl.Visible = false;
                                    wb = (Microsoft.Office.Interop.Excel._Workbook)(xl.Workbooks.Add(missing));
                                    ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1);
                                    ws.Columns.NumberFormat = "@";
                                    ws.Cells.NumberFormat = "@";

                                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                    {
                                        ws.Cells[1, i + 1] = ds.Tables[0].Columns[i].ColumnName;
                                    }

                                    for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                                    {
                                        int s = i1 + 1;
                                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                        {
                                            ws.Cells[s + 1, j + 1] = ds.Tables[0].Rows[i1].ItemArray[j].ToString();
                                        }
                                    }

                                    wb.SaveAs(filePath1 + "\\" + filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                                    wb.Close(true, missing, missing);
                                    xl.Quit();

                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
                                }
                                else
                                {
                                    FileStatusMsg.Text = "<font style='color: Green' size='3'><b>Statement Payment Excel File Has been Uploaded. Please Submit and Process the Same.</b></font>";
                                }

                                divItemDetailsExcel.Attributes.Add("style", "display:inline");
                                row1.Attributes.Add("style", "display:none");
                                btnFileUpload.Visible = false;
                                btnUploadExcel.Visible = false;
                                BtnSubmit.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
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

                if (File.Exists(ViewState["fileName"].ToString()))
                {
                    DataTable objPymtDataTbl = new DataTable();

                    sResult = objPayable.SubmitStatementPayment(strBranchCode);

                    if (sResult == "0")
                    {
                        ViewState["fileName"] = "";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Payment Upload Process completed');", true);
                        BtnSubmit.Visible = false;
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
            Response.Redirect("StatementPaymentUpload.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
