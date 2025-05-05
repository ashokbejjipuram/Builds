#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb.Reports.Inventory
{
    public partial class GoodsInwardRegister : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
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
                    if (crGoodsInwardRegister != null)
                    {
                        crGoodsInwardRegister.Dispose();
                        crGoodsInwardRegister = null;
                    }

                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
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
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crGoodsInwardRegister != null)
            {
                crGoodsInwardRegister.Dispose();
                crGoodsInwardRegister = null;
            }
        }
        protected void crGoodsInwardRegister_Unload(object sender, EventArgs e)
        {
            if (crGoodsInwardRegister != null)
            {
                crGoodsInwardRegister.Dispose();
                crGoodsInwardRegister = null;
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

        public void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = null;
            string strFromDate = null;
            string strToDate = null;
            string strInwardDate = "{inward_header.inward_date}";

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
                strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
                string strDateCompareQuery = strInwardDate + " >= Date (" + strFromDate + ") and "
                                           + strInwardDate + " <= Date (" + strToDate + ")";

                if (strBranchCode.Equals("CRP"))
                {
                    strSelectionFormula = strDateCompareQuery;
                }
                else
                {
                    strSelectionFormula = strDateCompareQuery + " and {inward_header.Branch_Code} = '" + strBranchCode + "'";
                }

                crGoodsInwardRegister.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
                crGoodsInwardRegister.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
                crGoodsInwardRegister.RecordSelectionFormula = strSelectionFormula;
                crGoodsInwardRegister.GenerateReportAndExportHO(fileType);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("GoodsInwardRegister.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}