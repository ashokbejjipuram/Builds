#region Namespace
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
#endregion

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class SalesManClassifiedOutstanding : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        private string status = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crSMClassifiedOutstanding_Report != null)
                    {
                        crSMClassifiedOutstanding_Report.Dispose();
                        crSMClassifiedOutstanding_Report = null;
                    }

                    ddlBranch.Text = strBranchCode;
                    if (strBranchCode != "CRP")
                        ddlBranch.Enabled = false;
                    else
                        ddlBranch.Enabled = true;
                    fnPopulateBranch();
                    fnPopulateSalesMan();
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
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
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
            if (crSMClassifiedOutstanding_Report != null)
            {
                crSMClassifiedOutstanding_Report.Dispose();
                crSMClassifiedOutstanding_Report = null;
            }
        }
        protected void crSMClassifiedOutstanding_Report_Unload(object sender, EventArgs e)
        {
            if (crSMClassifiedOutstanding_Report != null)
            {
                crSMClassifiedOutstanding_Report.Dispose();
                crSMClassifiedOutstanding_Report = null;
            }
        }

        #region populate Branch
        public void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateBranch", "Inside fnPopulateBranch");
            try
            {
            IMPALLibrary.Branches brnch = new IMPALLibrary.Branches();
            ddlBranch.DataSource = brnch.GetCorpBranch();
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate From Customer
        public void fnPopulateSalesMan()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string queryType = "Corporate";
                string strDefault = null;
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails cust = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                if (ddlBranch.SelectedValue == "CRP")
                {
                    cboSalesMan.DataSource = cust.GetSalesman(ddlBranch.Text);
                }
                else
                {
                    cboSalesMan.DataSource = cust.GetSalesman(ddlBranch.Text);
                }
                cboSalesMan.DataTextField = "Name";
                cboSalesMan.DataValueField = "Code";
                cboSalesMan.DataBind();
                cboSalesMan.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void cboSalesMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

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
                    crSMClassifiedOutstanding_Report.ReportName = "SalesManClassifiedOutstanding_Report";
                    fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
            try
            {
                string strDate = default(string);
                string strDate1 = default(string);
                string strSalesMan = default(string);
                string strBranch = default(string);
                string strTown = default(string);
                int intProcStatus = default(int);
                string strSelectionFormula = default(string);

                strDate = txtDate.Text;
                strDate1 = txtDate.Text;
                strSalesMan = cboSalesMan.SelectedValue;
                strBranch = ddlBranch.SelectedValue;

                if (strSalesMan == "0")
                    strSalesMan = "";
                if ((strSalesMan == "") && (strDate == ""))
                {
                    strSelectionFormula = null;
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                    string strSalesCode = default(string);
                    string pstrBranchCode = default(string);

                    strSalesCode = "{cust_aging_salesman.Sales_Man_code}";
                    pstrBranchCode = "{cust_aging_salesman.branch_code}";

                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_calcos_Salesman");
                    ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(dbcmd, "@salesman", DbType.String, strSalesMan);
                    ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                    if (strBranchCode != "CRP")
                    {
                        if (strSalesMan == "")
                            strSelectionFormula = pstrBranchCode + " ='" + strBranchCode + "'";
                        else
                            strSelectionFormula = strSalesCode + " = '" + strSalesMan + "' and " + pstrBranchCode + " ='" + strBranchCode + "'";
                    }
                    else
                    {
                        if (strSalesMan == "")
                            strSelectionFormula = "";
                        else
                            strSelectionFormula = strSalesCode + " = '" + strSalesMan + "'";
                    }
                }

                crSMClassifiedOutstanding_Report.RecordSelectionFormula = strSelectionFormula;
                crSMClassifiedOutstanding_Report.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Branch Selected Index change
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strBranchCode == "CRP")
            {
                fnPopulateSalesMan();
            }
        }
        #endregion
    }
}
