#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using IMPALLibrary;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
#endregion

namespace IMPALWeb.Reports.Sales.Sales_Comparison
{
    public partial class Linewisetop20 : System.Web.UI.Page
    {
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

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    if (crlinewisetop != null)
                    {
                        crlinewisetop.Dispose();
                        crlinewisetop = null;
                    }

                    fnpopulatelineselect();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
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
            if (crlinewisetop != null)
            {
                crlinewisetop.Dispose();
                crlinewisetop = null;
            }
        }
        protected void crlinewisetop_Unload(object sender, EventArgs e)
        {
            if (crlinewisetop != null)
            {
                crlinewisetop.Dispose();
                crlinewisetop = null;
            }
        }

        #region Populate Line Select Dropdown
        private void fnpopulatelineselect()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlSelect.DataSource = oCommon.GetDropDownListValues("LineWiseSelect");
                ddlSelect.DataTextField = "DisplayText";
                ddlSelect.DataValueField = "DisplayValue";
                ddlSelect.DataBind();
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
                    string sessionvalue = string.Empty;
                    if (Session["BranchCode"] != null)
                    {
                        sessionvalue = (string)Session["BranchCode"];
                    }
                    string selectionformula = string.Empty;
                    string sel1 = "Mid({linewisetop20V.item_code},1,3)";
                    string sel2 = "Mid({linewisetop20q.item_code},1,3)";
                    string sel3 = "{linewisetop20v.Branch_code}";
                    string sel4 = "{linewisetop20q.Branch_code}";

                    Database ImpalDB = DataAccess.GetDatabase();
                    string strFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                    string strToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

                    if (ddlSelect.SelectedIndex == 0)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetLineWisetop20");
                        ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                        ImpalDB.AddInParameter(cmd, "@fromdate", DbType.String, strFromDate);
                        ImpalDB.AddInParameter(cmd, "@todate", DbType.String, strToDate);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);

                        if (sessionvalue == "CRP")
                        {
                            if (ddlSupplier.SelectedIndex != 0)
                            {
                                selectionformula = sel2 + "= '" + ddlSupplier.SelectedValue + "'";
                            }
                        }
                        else
                        {
                            if (ddlSupplier.SelectedIndex != 0)
                            {
                                selectionformula = sel4 + "='" + sessionvalue + "' and " + sel2 + "='" + ddlSupplier.SelectedValue + "'";
                            }
                            else
                            {
                                selectionformula = sel4 + "='" + sessionvalue + "'";
                            }
                        }
                        crlinewisetop.ReportName = "line-wise-top20-q";
                    }
                    else
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetLineWisetop20v");
                        ImpalDB.AddInParameter(cmd, "@branch_code", DbType.String, sessionvalue);
                        ImpalDB.AddInParameter(cmd, "@fromdate", DbType.String, strFromDate);
                        ImpalDB.AddInParameter(cmd, "@todate", DbType.String, strToDate);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);

                        if (sessionvalue == "CRP")
                        {
                            if (ddlSupplier.SelectedIndex != 0)
                            {
                                selectionformula = sel1 + "= '" + ddlSupplier.SelectedValue + "'";
                            }
                        }
                        else
                        {
                            if (ddlSupplier.SelectedIndex != 0)
                            {
                                selectionformula = sel3 + "='" + sessionvalue + "' and " + sel1 + "='" + ddlSupplier.SelectedValue + "'";
                            }
                            else
                            {
                                selectionformula = sel3 + "='" + sessionvalue + "'";
                            }
                        }

                        crlinewisetop.ReportName = "line-wise-top20-V";
                    }

                    crlinewisetop.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crlinewisetop.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crlinewisetop.RecordSelectionFormula = selectionformula;
                    crlinewisetop.GenerateReport();
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
