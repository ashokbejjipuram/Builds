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
using WinSCP;

namespace IMPALWeb
{
    public partial class CrystalReportExportA4 : System.Web.UI.UserControl
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

        public void GenerateReportAndExportA4()
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    //CrystalRpt.GetCurrentReport(strReportPath, SelectionFormula, CrystalFormulaFields);

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

                    ExportingReport(ReportName);
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

        public void GenerateReportAndExportInvoiceA4(string InvoiceNumber, int Status)
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    //CrystalRpt.GetCurrentReport(strReportPath, SelectionFormula, CrystalFormulaFields);

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

                    ExportingReportInvoice(InvoiceNumber, Status);
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

        public void GenerateReportAndExportA4HO()
        {
            string SelectionFormula = RecordSelectionFormula;

            try
            {
                if (!string.IsNullOrEmpty(ReportName))
                {
                    string strReportPath = default(string);

                    reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                    //Getting the XML File Path from the WebConfig
                    Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                    //Getting the Report File path from Get
                    strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                    //CrystalRpt.GetCurrentReport(strReportPath, SelectionFormula, CrystalFormulaFields);

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

                    ExportingReport(ReportName);
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

        public void ExportingReport(string ReportName)
        {
            try
            {
               BinaryReader stream = new BinaryReader(ImpalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

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

        public void ExportingReportInvoice(string InvoiceNumber, int Status)
        {
            try
            {
                BinaryReader stream = new BinaryReader(ImpalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                byte[] bytes = stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length));

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
                Response.BinaryWrite(bytes);
                stream.Close();
                stream.Dispose();
                stream = null;

                if (Status == 1)
                {
                    var CurrentDate = DateTime.Now;
                    string fileName = InvoiceNumber + ".pdf";
                    string RemoteFolder = "/www/wwwroot/" + Session["BranchCode"].ToString() + "/Invoice/" + CurrentDate.Year.ToString().PadLeft(4, '0') + "/" + CurrentDate.Month.ToString().PadLeft(2, '0') + "/" + CurrentDate.Day.ToString().PadLeft(2, '0') + "/";

                    SessionOptions sessionOptions = new SessionOptions();
                    sessionOptions.Protocol = Protocol.Sftp;
                    sessionOptions.HostName = ConfigurationManager.AppSettings["CloudStorage_HostName"].ToString();
                    sessionOptions.UserName = ConfigurationManager.AppSettings["CloudStorage_UserName"].ToString();
                    sessionOptions.Password = ConfigurationManager.AppSettings["CloudStorage_Password"].ToString();
                    sessionOptions.SshHostKeyFingerprint = ConfigurationManager.AppSettings["CloudStorage_FingerPrint"].ToString(); //256 bit

                    Session session = new Session();

                    try
                    {
                        session.Open(sessionOptions);

                        if (!session.FileExists(RemoteFolder + fileName))
                        {
                            Stream stream1 = new MemoryStream(bytes);
                            session.PutFile(stream1, RemoteFolder + fileName);
                            stream1.Close();
                            stream1.Dispose();
                            stream1 = null;
                        }                       
                        
                        session.Close();
                        session.Dispose();
                        session = null;
                    }
                    catch (Exception Exp)
                    {
                        Log.WriteException(Source, Exp);
                    }
                    finally
                    {
                        if (session != null)
                        {
                            session.Close();
                            session.Dispose();
                            session = null;
                        }
                    }
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