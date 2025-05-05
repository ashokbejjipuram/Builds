using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Configuration;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Resources;
using System.IO;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System.Printing.Activation;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Printing;
using System.Management;
using IMPALLibrary.Transactions;
using System.Collections;
using CrystalDecisions.Web;
using System.Transactions;
using System.Data.SqlClient;
using System.Threading;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;

namespace IMPALWeb
{
    public partial class CrystalReportExport : System.Web.UI.UserControl
    {
        public Dictionary<string, string> CrystalFormulaFields = new Dictionary<string, string>();
        public string GUIDkey = string.Empty;
        Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        ReportsData reportsDt = new ReportsData();
        public string ReportName { get; set; }
        public string RecordSelectionFormula { get; set; }

        private ImpalLibrary Commonlib = new ImpalLibrary();
        private ReportDocument ImpalReport = new ReportDocument();
        private ConnectionInfo connectioninfo = new ConnectionInfo();
        private TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
        private CrystalDecisions.CrystalReports.Engine.Tables tables = null;
        private Queue reportQueue = new Queue();
        Exception expobj = new Exception();
        private int Reportsize = 39;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                }
                catch (Exception Exp)
                {
                    Log.WriteException(Source, Exp);
                }
            }
        }

        protected void crvImpalReports_Init(object sender, EventArgs e)
        {

        }

        private ConnectionInfo SetDBConnection()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            string ConnectinString = ImpalDB.ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectinString);

            ConnectionInfo connectioninfo = new ConnectionInfo();

            connectioninfo.ServerName = ConfigurationManager.AppSettings["CryRptServerName"].ToString();
            connectioninfo.DatabaseName = ConfigurationManager.AppSettings["CryRptDataBaseName"].ToString();
            connectioninfo.UserID = ConfigurationManager.AppSettings["CryRptUserName"].ToString();
            connectioninfo.Password = ConfigurationManager.AppSettings["CryRptPwd"].ToString();

            return connectioninfo;
        }

        private ConnectionInfo SetDBConnectionHO()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            string ConnectinString = ImpalDB.ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectinString);

            ConnectionInfo connectioninfo = new ConnectionInfo();

            connectioninfo.ServerName = ConfigurationManager.AppSettings["CryRptServerName"].ToString();
            connectioninfo.DatabaseName = ConfigurationManager.AppSettings["CryRptDataBaseNameHO"].ToString();
            connectioninfo.UserID = ConfigurationManager.AppSettings["CryRptUserName"].ToString();
            connectioninfo.Password = ConfigurationManager.AppSettings["CryRptPwd"].ToString();

            return connectioninfo;
        }

        public void GenerateReportAndExport(string fileType)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    if (!(ReportName == "SegmentStockList_New" || ReportName == "SegmentStockList_JLN_New" || ReportName == "SegmentStockList_UKD_New" || ReportName == "SalesListing_Custwise" || ReportName == "CashBook_Report"))
                        reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    ImpalReport.Dispose();
                    ImpalReport.Close();

                    ImpalReport.Load(strReportPath);
                    ImpalReport.DataSourceConnections.Clear();

                    connectioninfo = SetDBConnection();
                    tables = ImpalReport.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                    {
                        tableLogonInfo = table.LogOnInfo;
                        tableLogonInfo.ConnectionInfo = connectioninfo;
                        table.ApplyLogOnInfo(tableLogonInfo);
                    }

                    for (int i = 0; i <= CrystalFormulaFields.Count - 1; i++)
                    {
                        ImpalReport.DataDefinition.FormulaFields[CrystalFormulaFields.ElementAt(i).Key.ToString()].Text = CrystalFormulaFields.ElementAt(i).Value.ToString();
                    }

                    //ImpalReport.PrintOptions.PrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();
                    ImpalReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)Reportsize;
                    ImpalReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource.Auto);
                    ImpalReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait;

                    ImpalReport.RecordSelectionFormula = SelectionFormula;

                    ExportingReport(ReportName, fileType);
                }
                else
                    throw new Exception("Invalid ReportName");
            }
            catch (Exception Exp)
            {
                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }
            }
        }

        public void GenerateReportAndExportA4(string fileType)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    if (!(ReportName == "StockListnew" || ReportName == "stock_list" || ReportName == "PartNo_Aging" || ReportName == "PartNo_Aging_less3"
                        || ReportName == "PartNo_Aging_3to6" || ReportName == "PartNo_Aging_6to12" || ReportName == "PartNo_Aging_above12"))
                        reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    ImpalReport.Dispose();
                    ImpalReport.Close();

                    ImpalReport.Load(strReportPath);
                    ImpalReport.DataSourceConnections.Clear();

                    connectioninfo = SetDBConnection();
                    tables = ImpalReport.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                    {
                        tableLogonInfo = table.LogOnInfo;
                        tableLogonInfo.ConnectionInfo = connectioninfo;
                        table.ApplyLogOnInfo(tableLogonInfo);
                    }

                    for (int i = 0; i <= CrystalFormulaFields.Count - 1; i++)
                    {
                        ImpalReport.DataDefinition.FormulaFields[CrystalFormulaFields.ElementAt(i).Key.ToString()].Text = CrystalFormulaFields.ElementAt(i).Value.ToString();
                    }

                    ImpalReport.RecordSelectionFormula = SelectionFormula;

                    ExportingReport(ReportName, fileType);
                }
                else
                    throw new Exception("Invalid ReportName");
            }
            catch (Exception Exp)
            {
                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }
            }
        }

        public void GenerateReportAndExportHO(string fileType)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    if (!(ReportName == "GL-Report" || ReportName == "GLreportHO"))
                        reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    ImpalReport.Dispose();
                    ImpalReport.Close();

                    ImpalReport.Load(strReportPath);
                    ImpalReport.DataSourceConnections.Clear();

                    connectioninfo = SetDBConnectionHO();
                    tables = ImpalReport.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                    {
                        tableLogonInfo = table.LogOnInfo;
                        tableLogonInfo.ConnectionInfo = connectioninfo;
                        table.ApplyLogOnInfo(tableLogonInfo);
                    }

                    for (int i = 0; i <= CrystalFormulaFields.Count - 1; i++)
                    {
                        ImpalReport.DataDefinition.FormulaFields[CrystalFormulaFields.ElementAt(i).Key.ToString()].Text = CrystalFormulaFields.ElementAt(i).Value.ToString();
                    }

                    //ImpalReport.PrintOptions.PrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();
                    ImpalReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)Reportsize;
                    ImpalReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource.Auto);
                    ImpalReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait;

                    ImpalReport.RecordSelectionFormula = SelectionFormula;

                    ExportingReport(ReportName, fileType);
                }
                else
                    throw new Exception("Invalid ReportName");
            }
            catch (Exception Exp)
            {
                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }
            }
        }

        public void GenerateReportAndExportA4HO(string fileType)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    if (!(ReportName == "Impal-report-State-Acc-custA4" || ReportName == "Impal-report-State-Acc-townA4" || ReportName == "Impal-report-State-Acc-salesmanA4"))
                        reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    ImpalReport.Dispose();
                    ImpalReport.Close();

                    ImpalReport.Load(strReportPath);
                    ImpalReport.DataSourceConnections.Clear();

                    connectioninfo = SetDBConnectionHO();
                    tables = ImpalReport.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                    {
                        tableLogonInfo = table.LogOnInfo;
                        tableLogonInfo.ConnectionInfo = connectioninfo;
                        table.ApplyLogOnInfo(tableLogonInfo);
                    }

                    for (int i = 0; i <= CrystalFormulaFields.Count - 1; i++)
                    {
                        ImpalReport.DataDefinition.FormulaFields[CrystalFormulaFields.ElementAt(i).Key.ToString()].Text = CrystalFormulaFields.ElementAt(i).Value.ToString();
                    }

                    ImpalReport.RecordSelectionFormula = SelectionFormula;

                    ExportingReport(ReportName, fileType);
                }
                else
                    throw new Exception("Invalid ReportName");
            }
            catch (Exception Exp)
            {
                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }
            }
        }

        public void GenerateReportAndExportA4HOMails(string fileType, string BranchCode, string CustomerCode, string CustomerName, string EmailId, string CcEmailId, string MonthYear, string Date)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    ImpalReport.Dispose();
                    ImpalReport.Close();

                    ImpalReport.Load(strReportPath);
                    ImpalReport.DataSourceConnections.Clear();

                    connectioninfo = SetDBConnectionHO();
                    tables = ImpalReport.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                    {
                        tableLogonInfo = table.LogOnInfo;
                        tableLogonInfo.ConnectionInfo = connectioninfo;
                        table.ApplyLogOnInfo(tableLogonInfo);
                    }

                    for (int i = 0; i <= CrystalFormulaFields.Count - 1; i++)
                    {
                        ImpalReport.DataDefinition.FormulaFields[CrystalFormulaFields.ElementAt(i).Key.ToString()].Text = CrystalFormulaFields.ElementAt(i).Value.ToString();
                    }

                    ImpalReport.RecordSelectionFormula = SelectionFormula;
                    //ImpalReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\Downloads\StatementOFAccount\" + CustomerCode + fileType);

                    Stream stream = ImpalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                    string password = CustomerCode;
                    byte[] bytes;
                    PdfReader reader = new PdfReader(stream);
                    using (MemoryStream output = new MemoryStream())
                    {
                        PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                        bytes = output.ToArray();
                    }

                    string cc = CcEmailId;
                    MailAddress from = new MailAddress("dealersupport@impal.net", "Dealer Support - IMPAL");
                    MailAddress to = new MailAddress(EmailId);

                    string BodyMessage = "<font face='Arial'>Dear Sir/Madam,<br><br>Greetings from India Motor Parts & Accessories Limited (IMPAL)!!!!!" +
                        "<br><br>Please find attached your statement of Accounts with IMPAL as on " + Date + "." +
                        "<br><br>The attachment is password protected. Your dealer code is the password to open the attachment. Please refer to invoice for dealer code (Customer Code)." +
                        "<br><br>Password is case sensitive, please type all letters in CAPITAL." +
                        "<br><br>If the statement of account is correct and complete in all respects and there are no discrepancies, Kindly confirm the balance by way of return mail to the below email id:" +
                        "<br><a href='mailto:dealersupport@impal.net'>dealersupport@impal.net</a>" +
                        "<br><br>Incase if you find any discrepancy or mismatch in the statement of accounts, please respond with all details along with balance as per your books to the below email id:" +
                        "<br><a href='mailto:dealersupport@impal.net'>dealersupport@impal.net</a>" +
                        "<br><br>Kindly note that If no response is received for 1 week from the date of this mail, we will consider that the statement of accounts is accurate and balance is confirmed." +
                        "<br><br>Thanks and Regards,<br>IMPAL Team</font>";

                    using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to))
                    {
                        mm.Subject = "IMPAL – " + CustomerName + " - Statement of Accounts as on " + Date;
                        mm.Body = BodyMessage;
                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "StatementOfAccount-" + MonthYear +".pdf"));
                        mm.IsBodyHtml = true;
                        //string[] CCId = cc.Split(',');
                        //foreach (string CCEmail in CCId)
                        //{
                        //    mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                        //}
                        mm.Priority = MailPriority.High;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp3.impal.net";
                        smtp.UseDefaultCredentials = false;
                        NetworkCredential NetworkCred = new NetworkCredential("apikey", "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");
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.EnableSsl = false;
                        smtp.Send(mm);

                        reportsDt.UpdMailSentStatus(BranchCode, CustomerCode);
                    }
                }
                else
                    throw new Exception("Invalid ReportName");
            }
            catch (Exception Exp)
            {
                reportsDt.UpdSentMailsStatusOnError();

                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }                
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ImpalReport != null)
            {
                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;
            }            
        }

        protected override void OnUnload(EventArgs e)
        {
            if (ImpalReport != null)
            {
                ImpalReport.Close();
                ImpalReport.Dispose();
                ImpalReport = null;
            }

            base.OnUnload(e);
        }

        protected void crvImpalReportsViewer_Unload(object sender, EventArgs e)
        {
            if (ImpalReport != null)
            {
                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;
            }
        }

        public void ResetIIS()
        {
            Process iisReset = new Process();
            iisReset.StartInfo.FileName = "iisreset.exe";
            iisReset.StartInfo.RedirectStandardOutput = true;
            iisReset.StartInfo.UseShellExecute = false;
            iisReset.Start();
            iisReset.WaitForExit();
        }

        public void ExportingReport(string ReportName, string fileType)
        {
            try
            {
                GUIDkey = Session["BranchCode"].ToString().ToUpper() + "_" + ReportName + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + fileType;

                BinaryReader stream = null;

                if (fileType == ".pdf")
                {
                    stream = new BinaryReader(ImpalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

                    if (ImpalReport != null)
                    {
                        ImpalReport.Dispose();
                        ImpalReport.Close();
                        ImpalReport = null;
                        connectioninfo = null;
                        tableLogonInfo = null;
                    }

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
                    Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
                else if (fileType == ".xls")
                {
                    stream = new BinaryReader(ImpalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook));

                    if (ImpalReport != null)
                    {
                        ImpalReport.Dispose();
                        ImpalReport.Close();
                        ImpalReport = null;
                        connectioninfo = null;
                        tableLogonInfo = null;
                    }

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment; filename=" + GUIDkey);
                    Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
                    Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
                else if (fileType == ".doc")
                {
                    stream = new BinaryReader(ImpalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows));

                    if (ImpalReport != null)
                    {
                        ImpalReport.Dispose();
                        ImpalReport.Close();
                        ImpalReport = null;
                        connectioninfo = null;
                        tableLogonInfo = null;
                    }

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/msword";
                    Response.AddHeader("content-disposition", "attachment; filename=" + GUIDkey);
                    Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
                    Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
            catch (Exception Exp)
            {
                if (ImpalReport != null)
                {
                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;
                    connectioninfo = null;
                    tableLogonInfo = null;
                }

                Response.End();

                if (Exp.Message != "Thread was being aborted.")
                    Log.WriteException(Source, Exp);
            }
            finally
            {
                Response.Flush();
                Response.End();
                Response.Close();

                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}