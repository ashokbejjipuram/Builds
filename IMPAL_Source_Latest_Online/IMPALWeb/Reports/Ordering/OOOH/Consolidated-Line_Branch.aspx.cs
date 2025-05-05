#region Namespace Declaration
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Ordering.OOOH
{
    public partial class Consolidated_Line_Branch : System.Web.UI.Page
    {
        string sessionvalue = string.Empty;

        #region Page init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crlinebranch != null)
                    {
                        crlinebranch.Dispose();
                        crlinebranch = null;
                    }

                    ddbranchcode.Enabled = false;
                    fnPopulateMonthYear();
                }
                if (IsPostBack)
                {
                    if (ddlinecode.SelectedIndex != 0)
                    {
                        ddbranchcode.Enabled = true;
                        fnPopulateBranch();
                    }
                    else
                    {
                        ddbranchcode.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crlinebranch != null)
            {
                crlinebranch.Dispose();
                crlinebranch = null;
            }
        }
        protected void crlinebranch_Unload(object sender, EventArgs e)
        {
            if (crlinebranch != null)
            {
                crlinebranch.Dispose();
                crlinebranch = null;
            }
        }

        #region Populate Branch Dropdown
        private void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Branches br = new Branches();
                ddbranchcode.DataSource = br.GetAllLinewiseBranches(ddlinecode.SelectedValue.ToString(), "WithLine", sessionvalue);
                ddbranchcode.DataTextField = "BranchName";
                ddbranchcode.DataValueField = "BranchCode";
                ddbranchcode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate MonthYear Dropdown
        private void fnPopulateMonthYear()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LineWiseSales l = new LineWiseSales();
                ddmonthyear.DataSource = l.GetMonthYear(ddlinecode.SelectedValue);
                ddmonthyear.DataTextField = "MonthYear";
                ddmonthyear.DataValueField = "MonthYear";
                ddmonthyear.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    string selectionformula = string.Empty;
                    string sel1 = "{Branch_Master.branch_code}=";
                    string sel2 = "mid({con_branch_line.Supplier_line_code},1,3)=";
                    string sel3 = "{con_branch_line.month_year}=";
                    Database ImpalDB = DataAccess.GetDatabase();

                    DbCommand cmd = ImpalDB.GetStoredProcCommand("updmis_oooh");
                    ImpalDB.AddInParameter(cmd, "@br", DbType.String, sessionvalue);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("Usp_Con_Branch_Line");
                    ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, sessionvalue);
                    ImpalDB.AddInParameter(cmd1, "@MONTH_YEAR", DbType.String, ddmonthyear.SelectedValue);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);

                    #region Report Button Click
                    if (sessionvalue != "CRP")
                    {
                        if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex == 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex == 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0)
                        {
                            selectionformula = sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0)
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "' and " + sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";

                        }


                    }
                    else
                    {
                        if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0)
                        {
                            selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0)
                        {
                            if (ddbranchcode.SelectedIndex == -1)
                            { selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "'"; }
                            else
                            {
                                selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "' and " + sel1 + "'" + sessionvalue + "'";
                            }
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0)
                        {
                            selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "' and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0)
                        {
                            selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "' and " + sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel1 + "'" + sessionvalue + "'";
                        }
                    }
                    #endregion

                    crlinebranch.ReportName = "Consoli_Line_wise";
                    crlinebranch.RecordSelectionFormula = selectionformula;
                    crlinebranch.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }

        }
        #endregion
    }
}