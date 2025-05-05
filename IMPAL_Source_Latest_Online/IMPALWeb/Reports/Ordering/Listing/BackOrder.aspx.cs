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
using IMPALLibrary.Masters.CustomerDetails;
using System.IO;

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class BackOrder : System.Web.UI.Page
    {
        string strBranchCode = default(string);

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
                    GetCustomerList();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    fnLoadReportType();
                    lblMessage.Text = "";
                    ddlReportType_SelectIndexChanged(ddlReportType, EventArgs.Empty);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Report type Dropdown Populate Method
        protected void fnLoadReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("Report-BackOrder");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region LoadGLGroup
        protected void ddlReportType_SelectIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                if (ddlReportType.SelectedValue.ToString() == "1")
                {
                    ddlSubReportType.DataSource = oCommon.GetDropDownListValues("ReportType-BackOrder");
                }
                else
                {
                    ddlSubReportType.DataSource = oCommon.GetDropDownListValues("ReportType-LossOfSale");
                }
                ddlSubReportType.DataTextField = "DisplayText";
                ddlSubReportType.DataValueField = "DisplayValue";
                ddlSubReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                ddlCustomerName.DataSource = lstCompletion;
                ddlCustomerName.DataTextField = "Name";
                ddlCustomerName.DataValueField = "Code";
                ddlCustomerName.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerDetails
        private void GetCustomerDetails(string CustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CustomerDetails oDtls = new CustomerDetails();
                CustomerDtls oCustomer = null;
                oCustomer = oDtls.GetDetails(strBranchCode, CustomerCode);
                txtCustomerCode.Text = ddlCustomerName.SelectedValue;
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
        #endregion

        protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerDetails(ddlCustomerName.SelectedValue);
            if (ddlCustomerName.SelectedIndex == 0)
                divCustomerInfo.Style.Add("display", "none");
            else
                divCustomerInfo.Style.Add("display", "block");
        }

        #region Report Button Click

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Text = "Back";

                DataSet ds = new DataSet();
                Database ImpalDB = DataAccess.GetDatabaseBackUp();
                DbCommand cmdBO = ImpalDB.GetStoredProcCommand("Usp_BackOrder_Report");
                ImpalDB.AddInParameter(cmdBO, "@Branch_Code", DbType.String, strBranchCode.Trim());
                ImpalDB.AddInParameter(cmdBO, "@FromDate", DbType.String, txtFromDate.Text);
                ImpalDB.AddInParameter(cmdBO, "@ToDate", DbType.String, txtToDate.Text);
                ImpalDB.AddInParameter(cmdBO, "@CustomerCode", DbType.String, ddlCustomerName.SelectedValue.ToString());
                ImpalDB.AddInParameter(cmdBO, "@SupplierCode", DbType.String, ddlSupplier.SelectedValue.ToString());
                ImpalDB.AddInParameter(cmdBO, "@ReportType", DbType.Int16, Convert.ToInt16(ddlSubReportType.SelectedValue.ToString()));
                ImpalDB.AddOutParameter(cmdBO, "@FileName", DbType.String, 1000);
                cmdBO.CommandTimeout = ConnectionTimeOut.TimeOut;
                ds = ImpalDB.ExecuteDataSet(cmdBO);

                string fileName = cmdBO.Parameters["@FileName"].Value.ToString();

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + "");
                Response.ContentType = "application/ms-excel";
                Response.Write("<table border='1' style='font-family:arial;font-size:14px'><tr>");
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                }
                Response.Write("</tr>");
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
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                Response.Flush();
                Response.End();
                Response.Close();
            }
        }
        #endregion        
    }
}
