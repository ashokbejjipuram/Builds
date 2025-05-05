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
    public partial class SalesInvoiceReprintQR : System.Web.UI.Page
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
                    ddlInvoice.Items.Clear();
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
                    ddlInvoice.Items.Clear();
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
                string[] GSTINStatus = ddlInvoice.SelectedValue.Split(new string[] { "_" }, StringSplitOptions.None);

                DataSet Datasetresult = srpt.GetEinvoicingDetailsQR(ddlBranch.SelectedValue, GSTINStatus[0].ToString(), ddlInvoiceType.SelectedValue);

                GenerateJSON objGenJsonData = new GenerateJSON();

                if (GSTINStatus[1].ToString().Contains("UNREG"))
                {
                    einvGen.EinvoiceAuthenticationB2C(objGenJsonData.GenerateInvoiceJSONDataB2C(Datasetresult, ddlBranch.SelectedValue), ddlBranch.SelectedValue, GSTINStatus[0].ToString(), "1", "NOIRN");

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('B2C QR Code Has been Generated Successfully for Invoice Number " + GSTINStatus[0].ToString() + "');", true);
                }
                else
                {
                    string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                    einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, GSTINStatus[0].ToString(), "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('QR Code Has been Generated Successfully for Invoice Number " + GSTINStatus[0].ToString() + "');", true);                    
                }                
                
                ddlInvoice.DataSource = srpt.GetSalesQRdocs(ddlInvoiceType.SelectedValue, ddlBranch.SelectedValue);
                ddlInvoice.DataBind();
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
                    ddlInvoice.Items.Clear();
                else
                {
                    ddlInvoice.DataSource = (object)srpt.GetSalesQRdocs(strInvoiceType, ddlBranch.SelectedValue);
                    ddlInvoice.DataTextField = "ItemDesc";
                    ddlInvoice.DataValueField = "ItemCode";
                    ddlInvoice.DataBind();
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