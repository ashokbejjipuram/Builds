using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;

namespace IMPALWeb.Reports.Sales.SalesListing
{
    public partial class SalesListing_item_wise_1 : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        string strLineCode = default(string);

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
                    if (crSalesListingItemwise_Report != null)
                    {
                        crSalesListingItemwise_Report.Dispose();
                        crSalesListingItemwise_Report = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    PopulateReportType();
                    PopulateLineCode();
                    PopulatePartNo();

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        public void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("ReportType-SalesListing_ItemWise");
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crSalesListingItemwise_Report != null)
            {
                crSalesListingItemwise_Report.Dispose();
                crSalesListingItemwise_Report = null;
            }
        }
        protected void crSalesListingItemwise_Report_Unload(object sender, EventArgs e)
        {
            if (crSalesListingItemwise_Report != null)
            {
                crSalesListingItemwise_Report.Dispose();
                crSalesListingItemwise_Report = null;
            }
        }

        #region Populate Line Code
        public void PopulateLineCode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Suppliers supp = new IMPALLibrary.Suppliers();
                ddlLineCode.DataSource = supp.GetAllSuppliers();
                ddlLineCode.DataTextField = "SupplierName";
                ddlLineCode.DataValueField = "SupplierCode";
                ddlLineCode.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulatePartNo
        public void PopulatePartNo()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strLineCode = ddlLineCode.SelectedValue;
                IMPALLibrary.Masters.ItemMasters partno = new IMPALLibrary.Masters.ItemMasters();
                ddlPartNo.DataSource = partno.GetSupplierPartNumber(strLineCode);
                ddlPartNo.DataTextField = "Supplierpartno";
                ddlPartNo.DataValueField = "Supplierpartno";
                ddlPartNo.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula
        public void GenerateAndExportReport(string fileType)
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            string strCryFromDate = default(string);
            string strCryToDate = default(string);
            string strSelectionFormula = default(string);

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;
            strLineCode = ddlLineCode.SelectedValue;

            string strV_DocDate = default(string);
            string strV_BranchCode = default(string);
            string strV_ItemCode = default(string);

            strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
            strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            strV_DocDate = "{V_SalesReports.Document_date}";
            strV_BranchCode = "{V_SalesReports.branch_code}";
            strV_ItemCode = "{V_SalesReports.Supplier_Code}";

            if (ddlLineCode.SelectedIndex == 0)
            {
                strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'";
            }

            else
            {
                if (strBranchCode != "CRP")
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_BranchCode + "='" + strBranchCode + "'" + " and " + strV_ItemCode + "='" + strLineCode + "'";
                else
                    strSelectionFormula = strV_DocDate + ">=" + strCryFromDate + " and " + strV_DocDate + "<=" + strCryToDate + " and " + strV_ItemCode + "='" + strLineCode + "'";
            }

            crSalesListingItemwise_Report.ReportName = fnGetReportName(ddlReportType.SelectedValue);
            crSalesListingItemwise_Report.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSalesListingItemwise_Report.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSalesListingItemwise_Report.RecordSelectionFormula = strSelectionFormula;

            if (lstItem.Items.Count > 0)
                crSalesListingItemwise_Report.GenerateReportAndExport(fileType);
            else
                crSalesListingItemwise_Report.GenerateReportAndExportA4(fileType);
        }
        #endregion
        protected void btNextItem_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                bool blnFlag = false;
                if (!string.IsNullOrEmpty(ddlPartNo.SelectedValue))
                {
                    string strText = ddlPartNo.SelectedItem.Text;
                    foreach (ListItem lst in lstItem.Items)
                    {
                        if (strText.Equals(lst.Value))
                        {
                            blnFlag = true;
                            break;
                        }
                    }
                    if (!blnFlag)
                        lstItem.Items.Add(new ListItem(strText, strText));
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btRemove_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                int intcount;
                for (intcount = 0; ; )
                {
                    if (intcount < lstItem.Items.Count)
                    {
                        if (lstItem.Items[intcount].Selected)
                        {
                            lstItem.Items.Remove(lstItem.Items[intcount].Text);
                        }
                        else
                            intcount++;
                    }
                    else
                    {
                        intcount = 0;
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public string fnGetReportName(string strReportType)
        {
            string rptName = string.Empty;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (lstItem.Items.Count > 0)
                    rptName = "SalesListingItemwise_Part1";
                else if (ddlReportType.SelectedValue == "Report")
                    rptName = "SalesListingItemwise_Report";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return rptName;
        }

        protected void ddlLineCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePartNo();
            for (int count = 0; count < lstItem.Items.Count; count++)
            {
                lstItem.Items.Clear();
            }
        }

        #region Generate Button Click

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                PanelHeaderDtls.Enabled = false;

                if (lstItem.Items.Count > 0)
                {
                    int i = 0;

                    foreach (ListItem lst in lstItem.Items)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                        DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_addsaleslistingitemwise");
                        ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(dbcmd, "@Line_Code", DbType.String, ddlLineCode.SelectedValue);
                        ImpalDB.AddInParameter(dbcmd, "@Sup_Part", DbType.String, lst.Value);
                        ImpalDB.AddInParameter(dbcmd, "@Cnt", DbType.String, i);
                        dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(dbcmd);

                        i++;
                    }
                }

                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("SalesListing_Itemwise.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}