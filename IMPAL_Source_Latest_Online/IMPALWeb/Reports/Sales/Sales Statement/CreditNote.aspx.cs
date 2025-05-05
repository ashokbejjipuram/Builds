using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class CreditNote : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        ImpalLibrary oLib = new ImpalLibrary();
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                    Session.Remove("CrystalReport");
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

                    if (crSalesCreditNote != null)
                    {
                        crSalesCreditNote.Dispose();
                        crSalesCreditNote = null;
                    }

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
            if (crSalesCreditNote != null)
            {
                crSalesCreditNote.Dispose();
                crSalesCreditNote = null;
            }
        }
        protected void crSalesCreditNote_Unload(object sender, EventArgs e)
        {
            if (crSalesCreditNote != null)
            {
                crSalesCreditNote.Dispose();
                crSalesCreditNote = null;
            }
        }

        #region btnReport_Click

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
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        private void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = null;
            string strDateQuery = "{debit_credit_note_header.document_date}";
            string strBranchQuery = "{debit_credit_note_header.branch_code}";

            string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
            string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
            strSelectionFormula = strDateQuery + " >= Date (" + strFromDate + ") and "
                                   + strDateQuery + " <= Date (" + strToDate + ")";

            if (strBranchCode != "CRP")
                strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

            crSalesCreditNote.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crSalesCreditNote.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crSalesCreditNote.RecordSelectionFormula = strSelectionFormula;
            crSalesCreditNote.GenerateReportAndExport(fileType);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("CreditNote.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}