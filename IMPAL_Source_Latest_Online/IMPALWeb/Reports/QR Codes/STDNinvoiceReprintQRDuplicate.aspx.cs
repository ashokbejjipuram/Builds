#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Net;
using System.IO;
using IMPALLibrary.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Common;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
#endregion

namespace IMPALWeb.Reports.QR_Codes
{
    public partial class STDNinvoiceReprintQRDuplicate : System.Web.UI.Page
    {
        SalesReport srpt = new SalesReport();
        EinvAuthGen einvGen = new EinvAuthGen();

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
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
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    //fnPopulateInvoiceType();

                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchesHOAdmin();
                    ddlBranch.DataBind();
                    ddlBranch.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void ddlBranch_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    fnPopulateInvoiceSTDN(ddlInvoiceType.SelectedValue);
                    divSelection.Visible = true;
                }
                else
                    ddlInvoiceSTDN.Items.Clear();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlInvoiceType_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    fnPopulateInvoiceSTDN(ddlInvoiceType.SelectedValue);
                    divSelection.Visible = true;
                }
                else
                    ddlInvoiceSTDN.Items.Clear();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        #region ReportButton Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = srpt.GetEinvoicingDetailsOldQR(ddlBranch.SelectedValue, ddlInvoiceSTDN.SelectedValue, ddlInvoiceType.SelectedValue);

                GenerateJSON objGenJsonData = new GenerateJSON();

                string JSONData = "{\r\n    \"supplier_gstin\":\"" + ds.Tables[0].Rows[0]["Supplier_GSTIN"] + "\",\r\n    \"doc_no\":\"" + ds.Tables[0].Rows[0]["Document_Number"] + "\"," + "\r\n    \"doc_date\":\"" + ds.Tables[0].Rows[0]["Document_Date"] + "\",\r\n    \"doc_type\":\"" + ds.Tables[0].Rows[0]["Document_Type"] + "\"\r\n}";

                einvGen.EinvoiceAuthentication(JSONData, ddlBranch.SelectedValue, ddlInvoiceSTDN.SelectedValue, "1", "GETIRN", ds.Tables[0].Rows[0]["Supplier_GSTIN"].ToString(), ds.Tables[0].Rows[0]["Document_Type"].ToString(), ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-","/"), ds);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('QR Code Has been Generated Successfully for STDN Number " + ddlInvoiceSTDN.SelectedValue + "');", true);

                ddlInvoiceSTDN.DataSource = srpt.GetSTDNQRdocs(ddlInvoiceType.SelectedValue, ddlBranch.SelectedValue);
                ddlInvoiceSTDN.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateInvoiceType
        protected void fnPopulateInvoiceType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInvoiceType()", "Entering fnPopulateInvoiceType()");

            string strFileName = "InvoiceType";
            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strFileName);
                ddlInvoiceType.DataSource = drop;
                ddlInvoiceType.DataValueField = "DisplayValue";
                ddlInvoiceType.DataTextField = "DisplayText";
                ddlInvoiceType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region fnPopulateInvoiceSTDN
        protected void fnPopulateInvoiceSTDN(string strInvoiceType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateInvoiceSTDN()", "Entering fnPopulateInvoiceSTDN()");
            try
            {
                if (string.IsNullOrEmpty(strInvoiceType))
                    ddlInvoiceSTDN.Items.Clear();
                else
                {
                    ddlInvoiceSTDN.DataSource = srpt.GetSTDNQRdocs(strInvoiceType, ddlBranch.SelectedValue);
                    ddlInvoiceSTDN.DataBind();
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