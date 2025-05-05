using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using System.Web;
using System.Collections;
using System.Diagnostics;

namespace IMPALLibrary.Common
{
    public class ImpalCrystalReports
    {
        private ReportDocument ImpalReport = new ReportDocument();
        private ConnectionInfo connectininfo = new ConnectionInfo();
        private TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
        private Tables tables = null;
        private Queue reportQueue = new Queue();
        Exception expobj = new Exception();

        private ConnectionInfo SetDBConnection()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            string ConnectinString = ImpalDB.ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectinString);

            ConnectionInfo connectininfo = new ConnectionInfo();

            connectininfo.ServerName = ConfigurationManager.AppSettings["CryRptServerName"].ToString();
            connectininfo.DatabaseName = ConfigurationManager.AppSettings["CryRptDataBaseName"].ToString();
            connectininfo.UserID = ConfigurationManager.AppSettings["CryRptUserName"].ToString();
            connectininfo.Password = ConfigurationManager.AppSettings["CryRptPwd"].ToString();

            return connectininfo;
        }

        private ConnectionInfo SetDBConnectionHO()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            string ConnectinString = ImpalDB.ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectinString);

            ConnectionInfo connectininfo = new ConnectionInfo();

            connectininfo.ServerName = ConfigurationManager.AppSettings["CryRptServerName"].ToString();
            connectininfo.DatabaseName = ConfigurationManager.AppSettings["CryRptDataBaseNameHO"].ToString();
            connectininfo.UserID = ConfigurationManager.AppSettings["CryRptUserName"].ToString();
            connectininfo.Password = ConfigurationManager.AppSettings["CryRptPwd"].ToString();

            return connectininfo;
        }

        public void GetCurrentReport(string ReportName, string ReportSelctionFormula, Dictionary<string, string> CrystalFormulas)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Reportsize = 39;

            try
            {
                ImpalReport.Dispose();
                ImpalReport.Close();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();
                }

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalReport.Load(ReportName);
                    scope.Complete();
                }

                reportQueue.Enqueue(ImpalReport);

                ImpalReport.DataSourceConnections.Clear();

                connectininfo = SetDBConnection();

                tables = ImpalReport.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectininfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

                for (int i = 0; i <= CrystalFormulas.Count - 1; i++)
                {
                    ImpalReport.DataDefinition.FormulaFields[CrystalFormulas.ElementAt(i).Key.ToString()].Text = CrystalFormulas.ElementAt(i).Value.ToString();
                }

                ImpalReport.PrintOptions.PrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();
                ImpalReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)Reportsize;
                ImpalReport.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource.Auto);
                ImpalReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait;

                ImpalReport.RecordSelectionFormula = ReportSelctionFormula;
                HttpContext.Current.Session["CrystalReport"] = ImpalReport;
            }
            catch (Exception exp)
            {
                if (reportQueue.Count > 0)
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Log.WriteException(Source, exp);
            }
            finally
            {
                //Log.WriteLog(Source, "Load Report", "Report load Count is " + reportQueue.Count.ToString());

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                if (reportQueue.Count == 0)
                {
                    Process iisReset = new Process();
                    iisReset.StartInfo.FileName = "iisreset.exe";
                    iisReset.StartInfo.RedirectStandardOutput = true;
                    iisReset.StartInfo.UseShellExecute = false;
                    iisReset.Start();
                    iisReset.WaitForExit();
                }
            }
        }

        public void GetCurrentReportA4(string ReportName, string ReportSelctionFormula, Dictionary<string, string> CrystalFormulas)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                ImpalReport.Dispose();
                ImpalReport.Close();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();
                }

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalReport.Load(ReportName);
                    scope.Complete();
                }

                reportQueue.Enqueue(ImpalReport);

                ImpalReport.DataSourceConnections.Clear();

                connectininfo = SetDBConnection();

                tables = ImpalReport.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectininfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

                for (int i = 0; i <= CrystalFormulas.Count - 1; i++)
                {
                    ImpalReport.DataDefinition.FormulaFields[CrystalFormulas.ElementAt(i).Key.ToString()].Text = CrystalFormulas.ElementAt(i).Value.ToString();
                }

                ImpalReport.RecordSelectionFormula = ReportSelctionFormula;
                HttpContext.Current.Session["CrystalReport"] = ImpalReport;
            }
            catch (Exception exp)
            {
                if (reportQueue.Count > 0)
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Log.WriteException(Source, exp);
            }
            finally
            {
                //Log.WriteLog(Source, "Load Report", "Report load Count is " + reportQueue.Count.ToString());

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                if (reportQueue.Count == 0)
                {
                    Process iisReset = new Process();
                    iisReset.StartInfo.FileName = "iisreset.exe";
                    iisReset.StartInfo.RedirectStandardOutput = true;
                    iisReset.StartInfo.UseShellExecute = false;
                    iisReset.Start();
                    iisReset.WaitForExit();
                }
            }
        }

        public void GetCurrentReportHO(string ReportName, string ReportSelctionFormula, Dictionary<string, string> CrystalFormulas)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ImpalReport.Dispose();
                ImpalReport.Close();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();
                }

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalReport.Load(ReportName);
                    scope.Complete();
                }

                reportQueue.Enqueue(ImpalReport);

                connectininfo = SetDBConnectionHO();

                tables = ImpalReport.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectininfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

                for (int i = 0; i <= CrystalFormulas.Count - 1; i++)
                {
                    ImpalReport.DataDefinition.FormulaFields[CrystalFormulas.ElementAt(i).Key.ToString()].Text = CrystalFormulas.ElementAt(i).Value.ToString();
                }

                ImpalReport.RecordSelectionFormula = ReportSelctionFormula;
                HttpContext.Current.Session["CrystalReport"] = ImpalReport;
            }
            catch (Exception exp)
            {
                if (reportQueue.Count > 0)
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Log.WriteException(Source, exp);
            }
            finally
            {
                //Log.WriteLog(Source, "Load Report", "Report load Count is " + reportQueue.Count.ToString());

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    expobj = new Exception("Report load Count exceeds 75");
                    Log.WriteException(Source, expobj);
                }

                if (reportQueue.Count == 0)
                {
                    Process iisReset = new Process();
                    iisReset.StartInfo.FileName = "iisreset.exe";
                    iisReset.StartInfo.RedirectStandardOutput = true;
                    iisReset.StartInfo.UseShellExecute = false;
                    iisReset.Start();
                    iisReset.WaitForExit();
                }
            }
        }

        public void GetCurrentReportA4HO(string ReportName, string ReportSelctionFormula, Dictionary<string, string> CrystalFormulas)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                ImpalReport.Dispose();
                ImpalReport.Close();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();
                }

                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    ImpalReport.Load(ReportName);
                    scope.Complete();
                }

                reportQueue.Enqueue(ImpalReport);

                connectininfo = SetDBConnectionHO();

                tables = ImpalReport.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectininfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

                for (int i = 0; i <= CrystalFormulas.Count - 1; i++)
                {
                    ImpalReport.DataDefinition.FormulaFields[CrystalFormulas.ElementAt(i).Key.ToString()].Text = CrystalFormulas.ElementAt(i).Value.ToString();
                }

                ImpalReport.RecordSelectionFormula = ReportSelctionFormula;
                HttpContext.Current.Session["CrystalReport"] = ImpalReport;
            }
            catch (Exception exp)
            {
                if (reportQueue.Count > 0)
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                ImpalReport.Dispose();
                ImpalReport.Close();
                ImpalReport = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Log.WriteException(Source, exp);
            }
            finally
            {
                //Log.WriteLog(Source, "Load Report", "Report load Count is " + reportQueue.Count.ToString());

                if (reportQueue.Count > 75)
                {
                    ((ReportDocument)reportQueue.Dequeue()).Dispose();

                    ImpalReport.Dispose();
                    ImpalReport.Close();
                    ImpalReport = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                if (reportQueue.Count == 0)
                {
                    Process iisReset = new Process();
                    iisReset.StartInfo.FileName = "iisreset.exe";
                    iisReset.StartInfo.RedirectStandardOutput = true;
                    iisReset.StartInfo.UseShellExecute = false;
                    iisReset.Start();
                    iisReset.WaitForExit();
                }
            }
        }
    }
}