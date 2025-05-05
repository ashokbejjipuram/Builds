#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters.Sales;
using System.Data;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class TODSales : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                    Session.Remove("CrystalReport");
                if (Session["CrystalReport"] != null)
                    crTOD.GenerateReportHO();
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
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crTOD != null)
                    {
                        crTOD.Dispose();
                        crTOD = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;

                    GetCustomerList();
                    PopulateReportType();
                    LoadSFLddl();
                    btnReport.Enabled = true;
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
            if (crTOD != null)
            {
                crTOD.Dispose();
                crTOD = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        protected void crTOD_Unload(object sender, EventArgs e)
        {
            if (crTOD != null)
            {
                crTOD.Dispose();
                crTOD = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, "SalesOrder");
                cbCustomerName.DataSource = lstCompletion;
                cbCustomerName.DataTextField = "Name";
                cbCustomerName.DataValueField = "Code";
                cbCustomerName.DataBind();
                cbCustomerName.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnReport_Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    Session.Remove("CrystalReport");
                    if (ddlReportType.SelectedValue.Equals("Detail"))
                    { crTOD.ReportName = "SFL-Detail"; }
                    else if (ddlReportType.SelectedValue.Equals("Summary"))
                    { crTOD.ReportName = "SFL-Summary"; }

                    CallCrystalReport();
                    btnReport.Visible = true;

                    if (ddlReportType.SelectedValue.Equals("Socket TOD Policy"))
                    {
                        SocketTODExcelReport();
                        btnReport.Enabled = false;
                    }
                }
                else
                {
                    Session["CrystalReport"] = null;
                    Session.Remove("CrystalReport");

                    if (crTOD != null)
                    {
                        crTOD.Dispose();
                        crTOD = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateReportType
        private void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-SFLTODSales");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void SocketTODExcelReport()
        {
            string strFromDate = default(string);
            string strToDate = default(string);
            DataSet ds = new DataSet();

            strFromDate = txtFromDate.Text;
            strToDate = txtToDate.Text;

            string str_head = "";
            btnReport.Text = "Back";
            string filename = string.Format("{0:yyyyMMdd}", DateTime.Now.ToString("dd/MM/yyyy")) + "_SFLTOD_Socket.xls";

            Salesbranches oSales = new Salesbranches();
            ds = oSales.GetSFLTODSocketDetails(Session["BranchCode"].ToString(), strFromDate, strToDate);
            string strBranchName = (string)Session["BranchName"];
            str_head = "<center><b><font size='6'>SFL Socket TOD Policy Report of " + strBranchName + "</font></b><br><br></center>";

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            Response.ContentType = "application/ms-excel";
            Response.Write(str_head);
            Response.Write("<table border='1' style='font-family:arial;font-size:14px'>");

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Response.Write("<tr>");
                DataRow row = ds.Tables[0].Rows[i];
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {

                    Response.Write("<td>" + row[j] + "</td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        #region GetCustomerDetails
        private void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = cbCustomerName.SelectedValue;
                txtAddress1.Text = oCustomer.Address1;
                txtAddress2.Text = oCustomer.Address2;
                txtAddress3.Text = oCustomer.Address3;
                txtAddress4.Text = oCustomer.Address4;
                txtLocation.Text = oCustomer.Location;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void cbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerDetails(cbCustomerName.SelectedValue);
            divCustomerInfo.Style.Add("display", "block");

        }
        #endregion

        #region LoadSFLddl
        public void LoadSFLddl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("Plants");
                ddlPlant.DataSource = oList;
                ddlPlant.DataValueField = "DisplayValue";
                ddlPlant.DataTextField = "DisplayText";
                ddlPlant.DataBind();
                ddlPlant.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region CallCrystalReport
        private void CallCrystalReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (!string.IsNullOrEmpty(strBranchCode))
                    {
                        string strSelectionFormula = null;
                        string strDateQuery = "{SFLTOD.Our_Invoice_date}";
                        string strBranchQuery = "{branch_master.branch_code}";
                        string strPlantQuery = "{SFLTOD.Plant_Code}";
                        string strCustomerQuery = "{SFLTOD.Customer_Code}";

                        Salesbranches oSales = new Salesbranches();

                        if (ddlReportType.SelectedValue.Equals("Socket TOD Policy"))
                        {
                            oSales.UpdateTODSocket(string.Empty, txtFromDate.Text, txtToDate.Text, ddlPlant.SelectedValue, strBranchCode);
                        }
                        else
                        {
                            oSales.UpdateTODSales(string.Empty, txtFromDate.Text, txtToDate.Text, ddlPlant.SelectedValue, strBranchCode);

                            string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                            string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                            string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                                   + strDateQuery + " <= Date (" + strToDate + ")";

                            strSelectionFormula = strDateCompare;
                            if (cbCustomerName.SelectedIndex > 0)
                                strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + cbCustomerName.SelectedValue + "'";
                            if (ddlPlant.SelectedIndex > 0)
                                strSelectionFormula = strSelectionFormula + " and " + strPlantQuery + " = '" + ddlPlant.SelectedValue + "'";
                            if (!strBranchCode.Equals("CRP"))
                                strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

                            crTOD.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                            crTOD.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                            crTOD.RecordSelectionFormula = strSelectionFormula;
                            crTOD.GenerateReportHO();
                        }
                    }
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
