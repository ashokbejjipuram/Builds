using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using IMPALLibrary.Common;


namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class PaymentReportBms : System.Web.UI.Page
    {
        string pstrBranchCode = default(string);

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    pstrBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    fnPopulateReportType();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

      

        #region Report Button Click

        protected void btnreport_Click(object sender, EventArgs e)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);            

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //Code to Hide / Show filters
                Main mainMaster = (Main)Page.Master;
                if (mainMaster.ShowHideFilters(btnreport, reportFiltersTable, reportViewerHolder))
                {
                    Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("Bms_Header_Temp_Process");
                    ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, pstrBranchCode);
                    ImpalDB.AddInParameter(dbcmd, "@From_date", DbType.String, strFromDate.Trim());
                    ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strToDate.Trim());
                    ImpalDB.ExecuteNonQuery(dbcmd);

                    string strBMSDate = default(string);
                    string strTransactionDate = default(string);

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

                    strBMSDate = "{bms_Header_Temp.Bms_Date}";
                    strTransactionDate = "{bms_Header.Transaction_Date}";

                    if (ddlReportType.SelectedIndex == 0)
                    {
                        if (strFromDate != "" && strToDate != "")
                            strSelectionFormula = strBMSDate + ">=" + strCryFromDate + "and" + strBMSDate + "<=" + strCryToDate;
                    }
                    else
                    {
                        if (strFromDate != "" && strToDate != "")
                            strSelectionFormula = strTransactionDate + ">=" + strCryFromDate + "and" + strTransactionDate + "<=" + strCryToDate;
                    }
                    
                    crBMS.ReportName = ddlReportType.SelectedValue;
                    crBMS.CrystalFormulaFields.Add("fromdate", "\"" + txtFromDate.Text + "\"");
                    crBMS.CrystalFormulaFields.Add("todate", "\"" + txtToDate.Text + "\"");                    
                    crBMS.RecordSelectionFormula = strSelectionFormula;
                    crBMS.GenerateReport();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Populate Report Type
        public void fnPopulateReportType()
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
           
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("PymtReportBMS");
                ddlReportType.DataSource = oList;
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion
    }
}
