
#region Namespace Declaration
using IMPALLibrary;
using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using IMPALLibrary.Masters.Sales;
#endregion

namespace IMPALWeb.Reports.Ordering.OOOH
{
    public partial class Consolidated_Branch_Line : System.Web.UI.Page
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
                    if (crbrline != null)
                    {
                        crbrline.Dispose();
                        crbrline = null;
                    }

                    fnPopulateBranch();

                    if (sessionvalue == "CRP")
                        ddbranchcode.Enabled = true;
                    else
                    {
                        ddbranchcode.Enabled = false;
                        ddbranchcode.SelectedValue = sessionvalue;
                    }

                    fnPopulateSupplierMonth();
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
            if (crbrline != null)
            {
                crbrline.Dispose();
                crbrline = null;
            }
        }
        protected void crbrline_Unload(object sender, EventArgs e)
        {
            if (crbrline != null)
            {
                crbrline.Dispose();
                crbrline = null;
            }
        }

        #region Populate Branch Dropdown
        protected void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Branches br = new IMPALLibrary.Branches();
                ddbranchcode.DataSource = br.GetAllBranch();
                ddbranchcode.DataValueField = "BranchCode";
                ddbranchcode.DataTextField = "BranchName";
                ddbranchcode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Branch Dropdown
        protected void fnPopulateSupplierMonth()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers s = new Suppliers();
                ddlinecode.DataSource = s.GetAllLinewiseSuppliers(ddbranchcode.SelectedValue);
                ddlinecode.DataTextField = "SupplierName";
                ddlinecode.DataValueField = "SupplierCode";
                ddlinecode.DataBind();
                LineWiseSales line = new LineWiseSales();
                ddmonthyear.DataSource = line.GetMonthYear(ddbranchcode.SelectedValue);
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
                    string sel1 = "{Branch_Master.branch_code}=";
                    string sel2 = "mid({OOOH_Line.Supplier_line_code},1,3)=";
                    string sel3 = "{OOOH_Line.Month_year}=";
                    string selectionformula = string.Empty;
                    crbrline.ReportName = "Impal_OOOH";

                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand("Select  replicate('0',(2 - len(month(getdate())))) + ltrim(str(month(getdate()))) +convert(varchar(4),year(getdate()))");
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    string date1 = (string)ImpalDB.ExecuteScalar(cmd1);

                    if (date1.CompareTo(ddmonthyear.SelectedValue.ToString()) == 0)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_oooh");
                        ImpalDB.AddInParameter(cmd, "@br", DbType.String, sessionvalue);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }

                    #region Report Slection Formula
                    if (sessionvalue != "CRP")
                    {
                        if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex == 0) //all
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0) //branch
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0) //monthyear
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex == 0) //line
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0) //monthyear & branch
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0) //line & branch
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0) //all input
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + " '" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0) //all input
                        {
                            selectionformula = sel1 + "'" + sessionvalue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + " '" + ddmonthyear.SelectedValue + "'";
                        }
                    }
                    else
                    {
                        if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex == 0) //all
                        {
                            selectionformula = string.Empty;
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0) //branch
                        {
                            selectionformula = sel1 + "'" + ddbranchcode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0) //monthyear
                        {
                            selectionformula = sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }

                        else if (ddlinecode.SelectedIndex == 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0) //monthyear & branch
                        {
                            selectionformula = sel1 + "'" + ddbranchcode.SelectedValue + "'" + " and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex == 0) //line & branch
                        {
                            selectionformula = sel1 + "'" + ddbranchcode.SelectedValue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex == 0 && ddmonthyear.SelectedIndex != 0) //all input
                        {
                            selectionformula = sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                        else if (ddlinecode.SelectedIndex != 0 && ddbranchcode.SelectedIndex != 0 && ddmonthyear.SelectedIndex != 0) //all input
                        {
                            selectionformula = sel1 + "'" + ddbranchcode.SelectedValue + "'" + " and " + sel2 + "'" + ddlinecode.SelectedValue + "' and " + sel3 + "'" + ddmonthyear.SelectedValue + "'";
                        }
                    }
                    #endregion

                    crbrline.RecordSelectionFormula = selectionformula;
                    crbrline.GenerateReport();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Branchcode Selected Index changed
        protected void ddbranchcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers s = new Suppliers();
                ddlinecode.DataSource = s.GetAllLinewiseSuppliers(ddbranchcode.SelectedValue);
                ddlinecode.DataTextField = "SupplierName";
                ddlinecode.DataValueField = "SupplierCode";
                ddlinecode.DataBind();

                LineWiseSales line = new LineWiseSales();
                ddmonthyear.DataSource = line.GetMonthYear(ddbranchcode.SelectedValue);
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
    }
}