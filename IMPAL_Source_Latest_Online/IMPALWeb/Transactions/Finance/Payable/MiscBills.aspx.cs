using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Transactions.Finance;
using IMPALLibrary.Masters.Sales;
using IMPALWeb.UserControls;

namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class MiscBills : System.Web.UI.Page
    {
        IMPALLibrary.Payable objPayable = new IMPALLibrary.Payable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    //BtnSubmit.Visible = false;
                    ddlInvoiceNumber.Visible = false;
                    LoadDropDownLists<SupplierLine>(objPayable.LoadSupplierLine(), ddlSupplierLine, "Supplier_Line_Code", "Short_Description", true, "--Select--");
                    LoadAccountintPeriod();
                    txtBranch.Text = Session["BranchName"].ToString();
                    hdnBranch.Value = Session["BranchCode"].ToString();
                    txtAccountingPeriod.Visible = false;
                    txtInvoiceDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    BtnSubmit.Attributes.Add("OnClick", "return funMisBillsSubmitValidation();");
                    //FirstGridViewRow();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
            }
            catch (Exception ex)
            {

                Log.WriteException(Source, ex);
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("S_No", typeof(int)));
            dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit_of_Measurement", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Discount", typeof(string)));
            dt.Columns.Add(new DataColumn("Net_Price", typeof(double)));

            DataRow dr = null;
            dr = dt.NewRow();
            dr["Chart_of_Account_Code"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Unit_of_Measurement"] = string.Empty;
            dr["Quantity"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Discount"] = string.Empty;
            dr["Net_Price"] = 0;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            ViewState["GridRowCount"] = "1";
            grvMiscDetails.DataSource = dt;
            grvMiscDetails.DataBind();
        }

        protected void ddlSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }

        }

        protected void imgEditToggle_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlSupplierLine.SelectedValue != "0")
                {
                    FirstGridViewRow();
                }
                else
                {
                    MiscHeader miscHeader = new MiscHeader();
                    IMPALLibrary.Payable payableEntity = new IMPALLibrary.Payable();
                    miscHeader.Items = new List<MiscDetail>();
                    MiscDetail miscDetail = null;
                    miscHeader.DocumentNumber = txtInvoiceNumber.Text;
                    miscHeader.DocumentDate = txtInvoiceDate.Text;
                    miscHeader.BranchCode = txtBranch.Text;
                    miscHeader.Remarks = txtRemarks.Text;
                    miscHeader.SupplierCode = ddlSupplierLine.SelectedValue;
                    miscHeader.SupplierInvoiceDate = txtReferenceDocumentDate.Text;
                    miscHeader.SupplierInvoiceNumber = txtReferenceDocumentNumber.Text;
                    miscHeader.SupplierName = txtSupplierName.Text;
                    miscHeader.SupplierPlace = txtSupplierPlace.Text;
                    miscHeader.SalesTaxAmount = txtSaleTaxAmount.Text;
                    miscHeader.OtherDeductions = txtOtherDeductions.Text;
                    miscHeader.OtherCharges = txtOtherCharges.Text;
                    miscHeader.InvoiceAmount = txtInvoiceAmount.Text;
                    miscHeader.ExciseDutyAmount = txtExciseDutyAmount.Text;
                    miscHeader.AdvanceAmount = txtAdvances.Text;

                    foreach (GridViewRow gr in grvMiscDetails.Rows)
                    {
                        miscDetail = new MiscDetail();
                        TextBox txtSNo = (TextBox)gr.Cells[0].FindControl("txtSNo");
                        TextBox txtChartOfAccount = (TextBox)gr.Cells[1].FindControl("txtChartOfAccount");
                        TextBox txtDescription = (TextBox)gr.Cells[2].FindControl("txtDescription");
                        TextBox txtUnit = (TextBox)gr.Cells[3].FindControl("txtUnit");
                        TextBox txtQuantity = (TextBox)gr.Cells[4].FindControl("txtQuantity");
                        TextBox txtRate = (TextBox)gr.Cells[5].FindControl("txtRate");
                        TextBox txtDiscount = (TextBox)gr.Cells[6].FindControl("txtDiscount");
                        TextBox txtNetPrice = (TextBox)gr.Cells[7].FindControl("txtNetPrice");
                        miscDetail.Chart_of_Account_Code = txtChartOfAccount.Text;
                        miscDetail.Description = txtDescription.Text;
                        miscDetail.UnitOfMeasurement = txtUnit.Text;
                        miscDetail.Quantity = txtQuantity.Text;
                        miscDetail.Rate = txtRate.Text;
                        miscDetail.Discount = txtDiscount.Text;

                        miscHeader.Items.Add(miscDetail);
                    }

                    string documentNumber = objPayable.AddNewMiscEntry(ref miscHeader);
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ResetMiscBills();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void ResetMiscBills()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("MiscBills.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exception)
            {
                Log.WriteException(Source, exception);
            }
        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                TextBox txtgrdChartOfAccount = (TextBox)gvr.FindControl("txtChartOfAccount");
                TextBox txtDescription = (TextBox)gvr.FindControl("txtDescription");
                txtgrdChartOfAccount.Text = Session["ChatAccCode"].ToString();
                txtDescription.Focus();

            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void LoadAccountintPeriod()
        {
            AccountingPeriods Acc = new AccountingPeriods();
            List<AccountingPeriod> lstAccountingPeriod = new List<AccountingPeriod>();

            if (DateTime.Today.Month == 4)
            {
                lstAccountingPeriod.Add(new AccountingPeriod("0", (DateTime.Today.Year - 1).ToString() + "-" + DateTime.Today.Year.ToString()));
            }

            string accountingPeriod = GetCurrentFinancialYear();
            lstAccountingPeriod.Add(new AccountingPeriod("1", accountingPeriod));

            //lstAccountingPeriod = Acc.GetAccountingPeriod(20, null);
            ddlAccountingPeriod.DataSource = lstAccountingPeriod;
            ddlAccountingPeriod.DataTextField = "Desc";
            ddlAccountingPeriod.DataValueField = "AccPeriodCode";
            ddlAccountingPeriod.DataBind();
        }

        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;

            return FinYear.Trim();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                AddNewRow();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        private void AddNewRow()
        {
            int rowIndex = 0;
            int iNoofRows = 0;

            if (ViewState["GridRowCount"] != null)
            {
                iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
            }

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (Convert.ToInt64(ViewState["GridRowCount"]) == 0)
                    {

                        dtCurrentTable.Rows.Clear();
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["S_No"] = 1;
                    }
                    else
                    {
                        for (int i = iNoofRows; i <= dtCurrentTable.Rows.Count; i++)
                        {

                            TextBox txtSNo = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[0].FindControl("txtSNo");
                            TextBox txtChartOfAccount = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[1].FindControl("txtChartOfAccount");
                            TextBox txtDescription = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[2].FindControl("txtDescription");
                            TextBox txtUnit = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[3].FindControl("txtUnit");
                            TextBox txtQuantity = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[4].FindControl("txtQuantity");
                            TextBox txtRate = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[5].FindControl("txtRate");
                            TextBox txtDiscount = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[6].FindControl("txtDiscount");
                            TextBox txtNetPrice = (TextBox)grvMiscDetails.Rows[iNoofRows - 1].Cells[7].FindControl("txtNetPrice");


                            dtCurrentTable.Rows[i - 1]["Chart_of_Account_Code"] = txtChartOfAccount.Text;
                            dtCurrentTable.Rows[i - 1]["Description"] = txtDescription.Text;
                            dtCurrentTable.Rows[i - 1]["Unit_of_Measurement"] = txtUnit.Text;
                            dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                            dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;
                            dtCurrentTable.Rows[i - 1]["Net_Price"] = txtNetPrice.Text;


                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["S_No"] = i + 1;
                            rowIndex++;
                        }
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    ViewState["GridRowCount"] = dtCurrentTable.Rows.Count.ToString();
                    hdnRowCnt.Value = dtCurrentTable.Rows.Count.ToString();

                    grvMiscDetails.DataSource = dtCurrentTable;
                    grvMiscDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                int iNoofRows = 0;
                if (ViewState["GridRowCount"] != null)
                {
                    iNoofRows = Convert.ToInt32(ViewState["GridRowCount"]);
                }


                if (dt.Rows.Count > 1)
                {

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        TextBox txtSNo = (TextBox)grvMiscDetails.Rows[i - 1].Cells[0].FindControl("txtSNo");
                        TextBox txtChartOfAccount = (TextBox)grvMiscDetails.Rows[i - 1].Cells[1].FindControl("txtChartOfAccount");
                        TextBox txtDescription = (TextBox)grvMiscDetails.Rows[i - 1].Cells[2].FindControl("txtDescription");
                        TextBox txtUnit = (TextBox)grvMiscDetails.Rows[i - 1].Cells[2].FindControl("txtUnit");
                        TextBox txtQuantity = (TextBox)grvMiscDetails.Rows[i - 1].Cells[3].FindControl("txtQuantity");
                        TextBox txtRate = (TextBox)grvMiscDetails.Rows[i - 1].Cells[4].FindControl("txtRate");
                        TextBox txtDiscount = (TextBox)grvMiscDetails.Rows[i - 1].Cells[5].FindControl("txtDiscount");
                        TextBox txtNetPrice = (TextBox)grvMiscDetails.Rows[i - 1].Cells[6].FindControl("txtNetPrice");


                        txtSNo.Text = dt.Rows[i - 1]["S_No"].ToString();
                        txtChartOfAccount.Text = dt.Rows[i - 1]["Chart_of_Account_Code"].ToString();
                        txtDescription.Text = dt.Rows[i - 1]["Description"].ToString();
                        txtDescription.Text = dt.Rows[i - 1]["Unit_of_Measurement"].ToString();
                        txtQuantity.Text = dt.Rows[i - 1]["Quantity"].ToString();
                        txtRate.Text = dt.Rows[i - 1]["Rate"].ToString();
                        txtDiscount.Text = dt.Rows[i - 1]["Discount"].ToString();
                        txtNetPrice.Text = dt.Rows[i - 1]["Net_Price"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
    }
}
