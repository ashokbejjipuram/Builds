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

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class BI_MaruthiSales : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();
                if (!IsPostBack)
                {
                    if (crBI_MaruthiSales != null)
                    {
                        crBI_MaruthiSales.Dispose();
                        crBI_MaruthiSales = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crBI_MaruthiSales != null)
            {
                crBI_MaruthiSales.Dispose();
                crBI_MaruthiSales = null;
            }
        }
        protected void crBI_MaruthiSales_Unload(object sender, EventArgs e)
        {
            if (crBI_MaruthiSales != null)
            {
                crBI_MaruthiSales.Dispose();
                crBI_MaruthiSales = null;
            }
        }

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                    bool blnFlag = false;
                    string strFromDate = default(string);
                    string strToDate = default(string);
                    string strCryFromDate = default(string);
                    string strCryToDate = default(string);
                    string strSelectionFormula = default(string);
                    int intProcStatus = default(int);

                    strFromDate = txtFromDate.Text;
                    strToDate = txtToDate.Text;

                    strCryFromDate = DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                    strCryToDate = DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");

                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("BI_MARUTHI_SALES_MP");
                    ImpalDB.AddInParameter(dbcmd, "@br_code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(dbcmd, "@FROM_DAte", DbType.DateTime, strCryFromDate.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@TO_DAte", DbType.DateTime, strCryToDate.Trim());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                    strSelectionFormula = "{bimarsales.branch_Code}='" + strBranchCode + "'";

                    crBI_MaruthiSales.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                    crBI_MaruthiSales.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                    crBI_MaruthiSales.RecordSelectionFormula = strSelectionFormula;
                    crBI_MaruthiSales.GenerateReport();
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
