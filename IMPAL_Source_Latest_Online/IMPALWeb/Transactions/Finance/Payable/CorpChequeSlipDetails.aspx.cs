using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class CorpChequeSlipDetails : System.Web.UI.Page
    {
        string pstrBranchCode = default(string);
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();

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
                    fnLoadBanks();
                    fnPopulateReportType();
                    DateWise.Attributes.Add("style", "display:block");
                    ChequeWise.Attributes.Add("style", "display:none");
                    btnReset.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }

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

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlReportType.SelectedValue == "DateWise")
                {
                    DateWise.Attributes.Add("style", "display:block");
                    ChequeWise.Attributes.Add("style", "display:none");
                }
                else
                {
                    DateWise.Attributes.Add("style", "display:none");
                    ChequeWise.Attributes.Add("style", "display:block");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlBankName.SelectedIndex != 0)
                {
                    ddlBankName.Enabled = false;
                }
                else
                {
                    ddlBankName.Enabled = true;                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(typeof(BMSPayment), ex);
            }
        }

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
                DateWise.Attributes.Add("style", "display:none");
                ChequeWise.Attributes.Add("style", "display:none");
                btnReset.Visible = false;
                //Code to Hide / Show filters
                Main mainMaster = (Main)Page.Master;
                if (mainMaster.ShowHideFilters(btnreport, reportFiltersTable, reportViewerHolder))
                {
                    string strChequeDate = default(string);
                    string strBankCode = default(string);
                    string SelBankCode = default(string);

                    strCryFromDate = "Date (" + DateTime.ParseExact(strFromDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    strCryToDate = "Date (" + DateTime.ParseExact(strToDate, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";
                    SelBankCode = ddlBankName.SelectedValue.ToString();

                    strChequeDate = "{corp_bank_transaction.Cheque_Date}";
                    strBankCode = "{Corp_bank_transaction.Bank_Code}";

                    if (strCryFromDate != "" && strCryToDate != "")
                        strSelectionFormula = strChequeDate + ">=" + strCryFromDate + "and " + strChequeDate + "<=" + strCryToDate;

                    if (SelBankCode != "0")
                        strSelectionFormula = strSelectionFormula + " and " + strBankCode + " = " + SelBankCode;

                    crCorpChequeSlipDetails.ReportName = "CorpChequeSlipDetails" + ddlReportType.SelectedValue;
                    crCorpChequeSlipDetails.RecordSelectionFormula = strSelectionFormula;
                    crCorpChequeSlipDetails.GenerateReport();

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
        #endregion

        #region Populate Banks
        public void fnLoadBanks()
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<BankNames> lstBankNames = new List<BankNames>();
                lstBankNames = objPayable.GetBankNamesCorp();
                ddlBankName.DataSource = lstBankNames;
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataValueField = "BankCode";
                ddlBankName.DataBind();
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
                oList = oCommon.GetDropDownListValues("CorpChequeSlipDetails");
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBankName.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 0;
                ddlBankName.Enabled = true;
                DateWise.Attributes.Add("style", "display:block");
                ChequeWise.Attributes.Add("style", "display:none");
            }
            catch (Exception ex)
            {
                Log.WriteException(source, ex);
            }
        }
    }
}
