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

namespace IMPALWeb
{
    public partial class CrystalReportA4 : System.Web.UI.UserControl
    {
        public Dictionary<string, string> CrystalFormulaFields = new Dictionary<string, string>();
        Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //ReportsData reportsDt = new ReportsData();
        public string ReportName { get; set; }
        public string RecordSelectionFormula { get; set; }
        private ReportDocument rptDoc;
        private ImpalLibrary Commonlib = new ImpalLibrary();
        private ImpalCrystalReports CrystalRpt = new ImpalCrystalReports();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                crvImpalReportsViewer.Dispose();
                crvImpalReportsViewer = null;

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

        public void GenerateReport()
        {
            if (rptDoc != null)
            {
                crvImpalReportsViewer.ReportSource = rptDoc;
                crvImpalReportsViewer.Visible = true;
            }
            else
            {
                string SelectionFormula = RecordSelectionFormula;
                string strReportPath = default(string);      

                crvImpalReportsViewer.ReportSource = null;
                crvImpalReportsViewer.Dispose();

                try
                {
                    if (!string.IsNullOrEmpty(ReportName))
                    {
                        //reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                        //Getting the XML File Path from the WebConfig
                        Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                        //Getting the Report File path from Get
                        strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                        rptDoc = CrystalRpt.GetCurrentReportA4(strReportPath, SelectionFormula, CrystalFormulaFields);

                        crvImpalReportsViewer.ToolPanelView = ToolPanelViewType.None;
                        crvImpalReportsViewer.HasToggleGroupTreeButton = false;
                        crvImpalReportsViewer.HasDrillUpButton = false;
                        //crvImpalReportsViewer.HasExportButton = false;
                        //crvImpalReportsViewer.HasPrintButton = true;
                        //crvImpalReportsViewer.HasGotoPageButton = false;
                        //crvImpalReportsViewer.HasPageNavigationButtons = false;
                        //crvImpalReportsViewer.HasSearchButton = false;
                        //crvImpalReportsViewer.HasZoomFactorList = false;
                        crvImpalReportsViewer.ReportSource = rptDoc;
                        crvImpalReportsViewer.Visible = true;
                    }
                    else
                        throw new Exception("Invalid ReportName");
                }
                catch (Exception Exp)
                {
                    Log.WriteException(Source, Exp);
                }
            }
        }

        public void GenerateReportHO()
        {
            if (rptDoc != null)
            {
                crvImpalReportsViewer.ReportSource = rptDoc;
                crvImpalReportsViewer.Visible = true;
            }
            else
            {
                string SelectionFormula = RecordSelectionFormula;
                string strReportPath = default(string);

                crvImpalReportsViewer.ReportSource = null;
                crvImpalReportsViewer.Dispose();

                try
                {
                    if (!string.IsNullOrEmpty(ReportName))
                    {
                        //reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), ReportName);

                        //Getting the XML File Path from the WebConfig
                        Commonlib.XMLFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ReportNameList"].ToString());

                        //Getting the Report File path from Get
                        strReportPath = Server.MapPath("~") + Commonlib.GetReportFileName(ReportName);

                        rptDoc = CrystalRpt.GetCurrentReportA4HO(strReportPath, SelectionFormula, CrystalFormulaFields);

                        crvImpalReportsViewer.ToolPanelView = ToolPanelViewType.None;
                        crvImpalReportsViewer.HasToggleGroupTreeButton = false;
                        crvImpalReportsViewer.HasDrillUpButton = false;
                        //crvImpalReportsViewer.HasExportButton = false;
                        //crvImpalReportsViewer.HasPrintButton = true;
                        //crvImpalReportsViewer.HasGotoPageButton = false;
                        //crvImpalReportsViewer.HasPageNavigationButtons = false;
                        //crvImpalReportsViewer.HasSearchButton = false;
                        //crvImpalReportsViewer.HasZoomFactorList = false;
                        crvImpalReportsViewer.ReportSource = rptDoc;
                        crvImpalReportsViewer.Visible = true;
                    }
                    else
                        throw new Exception("Invalid ReportName");
                }
                catch (Exception Exp)
                {
                    Log.WriteException(Source, Exp);
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crvImpalReportsViewer != null)
            {
                crvImpalReportsViewer.ReportSource = null;
                crvImpalReportsViewer.Dispose();
                crvImpalReportsViewer = null;
            }            
        }

        protected void crvImpalReportsViewer_Unload(object sender, EventArgs e)
        {
            if (crvImpalReportsViewer != null)
            {
                crvImpalReportsViewer.ReportSource = null;
                crvImpalReportsViewer.Dispose();
                crvImpalReportsViewer = null;
            }            
        }
    }
}
