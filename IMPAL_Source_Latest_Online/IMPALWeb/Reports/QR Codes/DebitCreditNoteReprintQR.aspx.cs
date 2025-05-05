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
using IMPALLibrary.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Transactions.Finance;

namespace IMPALWeb.Reports.QR_Codes
{
    public partial class DebitCreditNoteReprintQR : System.Web.UI.Page
    {
        DebitCredit objDebitCredit = new DebitCredit();
        EinvAuthGen einvGen = new EinvAuthGen();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchesDrCrHOAdmin();
                    ddlBranch.DataBind();
                    ddlBranch.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranch_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    fnPopulateInvoiceDebitCreditNote();
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet Datasetresult = objDebitCredit.GetEinvoicingDetailsDebitCreditNote(ddlBranch.SelectedValue, ddlInvoice.SelectedValue);

                GenerateJSON objGenJsonData = new GenerateJSON();

                string JSONData = JsonConvert.SerializeObject(objGenJsonData.GenerateInvoiceJSONData(Datasetresult, ddlBranch.SelectedValue), Formatting.Indented);

                einvGen.EinvoiceAuthentication(JSONData.Substring(3, JSONData.Length - 4), ddlBranch.SelectedValue, ddlInvoice.SelectedValue, "1", "GENIRN", Datasetresult.Tables[0].Rows[0]["Seller_GST"].ToString(), Datasetresult.Tables[0].Rows[0]["Doc_Type"].ToString(), Datasetresult.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/"), Datasetresult);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('QR Code Has been Generated Successfully for Debit Credit Note Number " + ddlInvoice.SelectedValue + "');", true);

                fnPopulateInvoiceDebitCreditNote();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnPopulateInvoiceDebitCreditNote()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlInvoice.Items.Clear();

                ddlInvoice.DataSource = objDebitCredit.GetInvoiceDebitCreditNoteQR(ddlBranch.SelectedValue);
                ddlInvoice.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
