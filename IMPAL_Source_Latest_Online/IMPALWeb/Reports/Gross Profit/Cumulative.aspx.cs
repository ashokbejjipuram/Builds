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
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

using IMPALLibrary.Masters.Sales;
namespace IMPALWeb.Reports.Gross_Profit
{
    public partial class Cumulative : System.Web.UI.Page
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
                    Session.Remove("CrystalReport");
                    PopulateReportType();
                    PopulateMonthYear();
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
            if (!IsPostBack)
                Session.Remove("CrystalReport");
            if (Session["CrystalReport"] != null)
                crCumulative_BrGP.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report Type
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateReportType", "Entering PopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType_GPMontly");
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlReportType.SelectedValue == "Branch GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_BrGP";
                        GenerateSelectionFormula();
                    }
                    else if (ddlReportType.SelectedValue == "Line GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_LnGP";
                        GenerateSelectionFormula();
                    }
                    else if (ddlReportType.SelectedValue == "Town-Salesman GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_TownSalesmanGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Salesman-Town GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_SalesmanTownGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Customer-Line GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_CustLineGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Line-Customer GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_LineCustGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Town-Line GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_TownLineGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Line-Town GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_LineTownGP";
                        GenerateSelectionFormula1();
                    }
                    else if (ddlReportType.SelectedValue == "Line-Town-Customer GP")
                    {
                        crCumulative_BrGP.ReportName = "Monthly_LineTownCustGP";
                        GenerateSelectionFormula1();
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion


        #region Generate Selection Formula
        public void GenerateSelectionFormula()
        {
            string strGPInBrcode = default(string);
            string strGPInMonYr = default(string);
            string strMonyr = default(string);
            string strGPInAccPr = default(string);
            string strSelectionFormula = default(string);
            int intProcStatus = default(int);
            string strDesc = default(string);
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "GenerateSelectionFormula", "Entering GenerateSelectionFormula");
            try
            {
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

            strMonyr = ddlMonYr.SelectedValue;
            strGPInBrcode = "{GPProfitStatement_Invwise.brcode}";
            strGPInMonYr = "{GPProfitStatement_Invwise.month_year}";
            strGPInAccPr = "{GPProfitStatement_Invwise.Accounting_Period}";

            if (strBranchCode == "CRP")
            {
                string strQuery = "select Description from Accounting_Period where Accounting_Period_Code = '" + strMonyr + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                strDesc = (ImpalDB.ExecuteScalar(cmd1)).ToString();
            }
            else
                strDesc = strMonyr;
            if (strBranchCode != "CRP")
            {
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_subgpstatement_InvWise");
                ImpalDB.AddInParameter(dbcmd, "@month_year", DbType.String, strMonyr.Trim());
                ImpalDB.AddInParameter(dbcmd, "@brcode", DbType.String, strBranchCode.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);
            }

            if (strBranchCode == "CRP")
                strSelectionFormula = strGPInAccPr + "=" + strMonyr;
            else
                strSelectionFormula = strGPInMonYr + "=" + "'" + strMonyr + "'and" + strGPInBrcode + "=" + " '" + strBranchCode + "'";

            crCumulative_BrGP.CrystalFormulaFields.Add("AccDesc", "\"" + strDesc + "\"");
            crCumulative_BrGP.RecordSelectionFormula = strSelectionFormula;
            crCumulative_BrGP.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selcetion Formula1
        public void GenerateSelectionFormula1()
        {
            string strVGPBrcode = default(string);
            string strVGPMid1 = default(string);
            string strVGPMid2 = default(string);
            string strToMY = default(string);
            string strFrmMY = default(string);
            string strVGPAccPr = default(string);
            string strMonyr = default(string);
            string strDesc = default(string);
            string strSelectionFormula = default(string);
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "GenerateSelectionFormula1", "Entering GenerateSelectionFormula1");
            try
            {
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

            strMonyr = ddlMonYr.SelectedValue;

            strVGPBrcode = "{V_GP_Profitstatement.Branch_Code}";
            strVGPMid1 = "Mid({V_GP_Profitstatement.month_year},1,2)";
            strVGPMid2 = "Mid({V_GP_Profitstatement.month_year},3,6)";
            strToMY = strMonyr.Substring(2, 4);
            strFrmMY = strMonyr.Substring(0, 2);
            strVGPAccPr = "{V_GP_Profitstatement.Accounting_Period}";

            if (strBranchCode == "CRP")
            {
                string strQuery = "select Description from Accounting_Period where Accounting_Period_Code = '" + strMonyr + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                strDesc = (ImpalDB.ExecuteScalar(cmd1)).ToString();
            }
            else
                strDesc = strMonyr;

            if (strBranchCode == "CRP")
                strSelectionFormula = strVGPAccPr + "=" + strMonyr;
            else if (Convert.ToInt32(strFrmMY) < 4)
                strSelectionFormula = "((" + strVGPMid1 + ">='04' and " + strVGPMid1 + "<='12') or " + strVGPMid1 + "<='" + strFrmMY + "') and " + strVGPMid2 + "<='" + strToMY + "' and " + strVGPBrcode + "='" + strBranchCode + "'";
            else
                strSelectionFormula = strVGPMid1 + ">='04' and " + strVGPMid1 + "<='" + strFrmMY + "' and " + strVGPMid2 + "<='" + strToMY + "' and " + strVGPBrcode + "='" + strBranchCode + "'";

            crCumulative_BrGP.CrystalFormulaFields.Add("AccDesc", "\"" + strDesc + "\"");
            crCumulative_BrGP.RecordSelectionFormula = strSelectionFormula;
            crCumulative_BrGP.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Month Year
        public void PopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateMonthYear", "Entering PopulateMonthYear");
            try
            {
                if (strBranchCode != "CRP")
                {
                    int p, k, l, i, j;
                    string value = default(string);
                    string a = default(string);

                    List<string> lst = new List<string>();
                    int currMonth = DateTime.Now.Month;
                    int currYear = DateTime.Now.Year;

                    if (currMonth < 4)
                        p = 2;
                    else
                        p = 1;
                    for (j = currYear - p; j <= currYear; j++)
                    {
                        if (j == (currYear - p))
                        {
                            k = 4;
                            l = 12;
                        }
                        else if ((j == currYear - 1) && (p != 1))
                        {
                            k = 1;
                            l = 12;
                        }
                        else
                        {
                            k = 1;
                            l = currMonth;
                        }
                        for (i = k; i <= l; i++)
                        {
                            if ((i.ToString().Length) == 1)
                            {
                                a = "0" + i.ToString();
                                value = a + j.ToString();
                            }
                            else
                            {
                                value = i + j.ToString();
                            }
                            //value = a + j.ToString();
                            lst.Add(value);
                        }
                    }
                    ddlMonYr.DataSource = lst;
                    ddlMonYr.DataBind();
                }
                else
                {
                    IMPALLibrary.Masters.Sales.AccountingPeriods month = new AccountingPeriods();
                    List<IMPALLibrary.Masters.Sales.AccountingPeriod> lstMonth = new List<IMPALLibrary.Masters.Sales.AccountingPeriod>();
                    lstMonth = month.GetMonth();
                    ddlMonYr.DataSource = lstMonth;
                    ddlMonYr.DataTextField = "Desc";
                    ddlMonYr.DataValueField = "AccPeriodCode";
                    ddlMonYr.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
