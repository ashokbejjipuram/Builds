using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPLLib = IMPALLibrary.Transactions;
using IMPALLibrary.Masters.Others;
using System.Data;
using System.Globalization;
using log4net;


namespace IMPALWeb.Transactions.Finance.Payable
{
    public partial class ChequeSlip : System.Web.UI.Page
    {
        private string strBranchCode;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ChequeSlip), exp);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!Page.IsPostBack)
                {
                    btnBack.Visible = false;

                    if (crChequeSlip != null)
                    {
                        crChequeSlip.Dispose();
                        crChequeSlip = null;
                    }

                    txtBranchName.Text = fnGetBranch(strBranchCode);
                    // txtBranchName.Text = Session["BranchCode"].ToString();
                    txtChequeSlipDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtChequeDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //ImgChequeDt.Enabled = false;
                    ddlChequeSlipNo.Visible = false;
                    txtChequeSlipNo.Visible = true;
                    txtChequeSlipNo.Enabled = false;
                    txtChartofAccount.Enabled = false;
                    // txtAmount.Enabled = true;
                    txtCalculatedAmount.Enabled = false;
                    txtCalculatedAmount.Text = "0.00";
                    LoadCashDicount();
                    LoadSupplierLine();
                    ConstructColumn(true);
                    //txtAmount.Attributes.Add("onkeypress", "return ValidateAmount(event,this)");
                    BtnSubmit.Attributes.Add("OnClick", "return ValidateChequeSlip();");
                    btnReport.Visible = false;
                    btnReportExcel.Visible = false;
                    ChkHeader.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crChequeSlip != null)
            {
                crChequeSlip.Dispose();
                crChequeSlip = null;
            }
        }
        protected void crChequeSlip_Unload(object sender, EventArgs e)
        {
            if (crChequeSlip != null)
            {
                crChequeSlip.Dispose();
                crChequeSlip = null;
            }
        }

        private string fnGetBranch(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CashAndBankTransactions ChequeslipTransactions = new CashAndBankTransactions();
            try
            {
                return (string)Session["BranchName"];
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return (string)Session["BranchName"];
        }


        public void LoadChequeSlipNumber()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();

                ddlChequeSlipNo.DataSource = objChequeSlip.LoadChequeSlip(strBranchCode);
                ddlChequeSlipNo.DataTextField = "ChequeSlipNo";
                ddlChequeSlipNo.DataValueField = "ChequeSlipCode";
                ddlChequeSlipNo.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        public void LoadSupplierLine()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPLLib.StockTranRequest objStockTranReq = new IMPLLib.StockTranRequest();
                ddlSupplier.DataSource = objStockTranReq.GetSupplierList();
                ddlSupplier.DataTextField = "SupName";
                ddlSupplier.DataValueField = "SupCode";
                ddlSupplier.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }


        public void LoadCashDicount()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                CashDiscountMaster objCashDiscountMst = new CashDiscountMaster();
                ddlCashDiscount.DataSource = objCashDiscountMst.GetCashDiscountCheque(strBranchCode);
                ddlCashDiscount.DataTextField = "CashDiscName";
                ddlCashDiscount.DataValueField = "CashDiscPer";
                ddlCashDiscount.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        protected void ucChartofAccount_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChartofAccount.Text = Session["ChatAccCode"].ToString();
                IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                IMPLLib.ChequeSlipBankDetails objBankDtl = new IMPLLib.ChequeSlipBankDetails();
                objBankDtl = objChequeSlip.GetChequeSlipBankDetails(txtChartofAccount.Text);
                //((TextBox)updDocumentDetail.ContentTemplateContainer.FindControl("txtBank")).Text = objBankDtl.BankName;
                //((TextBox)updDocumentDetail.ContentTemplateContainer.FindControl("txtBranch")).Text = objBankDtl.Address;
                txtBank.Text = objBankDtl.BankName;
                txtBranch.Text = objBankDtl.Address;
                //if (Session["ChatDescription"].ToString() == "CASH ON HAND")
                //{
                //    txtChequeDate.Text = "";
                //    txtChequeNo.Text = "";
                //    txtChequeDate.Enabled = false;
                //    txtChequeNo.Enabled = false;
                //    ImgChequeDt.Enabled = false;
                //}
                //else
                //{
                //    txtChequeDate.Enabled = true;
                //    txtChequeNo.Enabled = true;
                //    ImgChequeDt.Enabled = true;
                //}
                updPanelPartTwo.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChequeSlipNo.Visible = false;
                ddlChequeSlipNo.Visible = true;
                BtnSubmit.Enabled = false;
                //updGridLineItem.Update();
                DisableViewMode();
                grdChequeDtl.Visible = false;
                btnReport.Visible = true;
                btnReportExcel.Visible = true;
                LoadChequeSlipNumber();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void LoadChequeSlipItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //ddlRefType.Enabled = false;
                IMPLLib.ChequeSlip objChequeSlipItems = new IMPLLib.ChequeSlip();

                List<IMPLLib.ChequeSlipDetail> objChequeSlipPO = new List<IMPLLib.ChequeSlipDetail>();
                txtCalculatedAmount.Text = "0.00";
                if (!string.IsNullOrEmpty(txtFromdateDate.Text) && !string.IsNullOrEmpty(txtToDate.Text) && ddlRefType.SelectedValue != "0")
                {
                    //objChequeSlipPO = objChequeSlipItems.LoadChequeSlipPO(Session["BranchCode"].ToString(), ddlSupplier.SelectedValue.ToString(), DateTime.ParseExact(txtFromdateDate.Text,"dd/MM/yyyy",null), DateTime.ParseExact(txtToDate.Text,"dd/MM/yyyy",null));
                    objChequeSlipPO = objChequeSlipItems.LoadChequeSlipPO(Session["BranchCode"].ToString(), ddlSupplier.SelectedValue.ToString(), txtFromdateDate.Text, txtToDate.Text);
                }
                else
                {
                    //if (grdChequeDtl.DataSource != null)

                    grdChequeDtl.DataSource = null;
                    grdChequeDtl.DataBind();
                    return;
                }
                if (objChequeSlipPO.Count == 0)
                {
                    grdChequeDtl.DataSource = null;
                    grdChequeDtl.DataBind();
                    ChkHeader.Visible = false;
                }
                else
                {
                    txtRemarks.Enabled = false;
                    txtChequeNo.Enabled = false;
                    ddlSupplier.Enabled = false;
                    txtFromdateDate.Enabled = false;
                    txtToDate.Enabled = false;
                    ddlRefType.Enabled = false;
                    ChkHeader.Visible = true;
                    //ddlCashDiscount.Enabled = false;

                    ConstructColumn(false);
                    DataTable objPODataTbl = new DataTable();
                    DataRow dtRowItem;
                    objPODataTbl = (DataTable)ViewState["CurrentTable"];
                    objPODataTbl.Rows.RemoveAt(0);

                    for (int i = 0; i <= objChequeSlipPO.Count - 1; i++)
                    {
                        dtRowItem = objPODataTbl.NewRow();
                        objPODataTbl.Rows.Add(dtRowItem);

                        objPODataTbl.Rows[i]["Indicator"] = string.IsNullOrEmpty(objChequeSlipPO[i].Indicator) ? "" : objChequeSlipPO[i].Indicator.ToString();
                        objPODataTbl.Rows[i]["BranchCode"] = string.IsNullOrEmpty(objChequeSlipPO[i].BranchCode) ? "" : objChequeSlipPO[i].BranchCode.ToString();
                        objPODataTbl.Rows[i]["InvoiceNo"] = string.IsNullOrEmpty(objChequeSlipPO[i].InvoiceNo) ? "" : objChequeSlipPO[i].InvoiceNo.ToString();
                        objPODataTbl.Rows[i]["InvoiceDate"] = string.IsNullOrEmpty(objChequeSlipPO[i].InvoiceDate) ? "" : objChequeSlipPO[i].InvoiceDate.ToString();
                        objPODataTbl.Rows[i]["InvoiceValue"] = string.IsNullOrEmpty(objChequeSlipPO[i].InvoiceValue) ? "" : Convert.ToDouble(objChequeSlipPO[i].InvoiceValue).ToString("0.00");
                        objPODataTbl.Rows[i]["CDValue"] = string.IsNullOrEmpty(objChequeSlipPO[i].CDValue) ? "" : objChequeSlipPO[i].CDValue.ToString();
                        objPODataTbl.Rows[i]["TotalValue"] = string.IsNullOrEmpty(objChequeSlipPO[i].TotalValue) ? "" : objChequeSlipPO[i].TotalValue.ToString();
                    }

                    grdChequeDtl.DataSource = objPODataTbl;
                    grdChequeDtl.DataBind();
                    ViewState["CurrentTable"] = objPODataTbl;
                    SetOnBlurEvent();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        public void ConstructColumn(bool bFlag)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataTable objPODataTbl = new DataTable();
                DataRow dtRowItem;

                DataColumn dtColIsSelected = new DataColumn("IsSelected", typeof(string));
                DataColumn dtColIndicator = new DataColumn("Indicator", typeof(string));
                DataColumn dtColBranchCode = new DataColumn("BranchCode", typeof(string));
                DataColumn dtColInvoiceNo = new DataColumn("InvoiceNo", typeof(string));
                DataColumn dtColInvoiceDate = new DataColumn("InvoiceDate", typeof(string));
                DataColumn dtColInvoiceValue = new DataColumn("InvoiceValue", typeof(string));
                DataColumn dtColCDValue = new DataColumn("CDValue", typeof(string));
                DataColumn dtColTotalValue = new DataColumn("TotalValue", typeof(string));

                objPODataTbl.Columns.Add(dtColIsSelected);
                objPODataTbl.Columns.Add(dtColIndicator);
                objPODataTbl.Columns.Add(dtColBranchCode);
                objPODataTbl.Columns.Add(dtColInvoiceNo);
                objPODataTbl.Columns.Add(dtColInvoiceDate);
                objPODataTbl.Columns.Add(dtColInvoiceValue);
                objPODataTbl.Columns.Add(dtColCDValue);
                objPODataTbl.Columns.Add(dtColTotalValue);

                dtRowItem = objPODataTbl.NewRow();
                objPODataTbl.Rows.Add(dtRowItem);

                ViewState["CurrentTable"] = objPODataTbl;

                if (bFlag == true)
                {
                    grdChequeDtl.DataSource = objPODataTbl;
                    grdChequeDtl.DataBind();
                }
                grdChequeDtl.Rows[0].Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        private void SaveChequeSlip()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPLLib.ChequeSlipHeader objChequeSlipHdr = new IMPLLib.ChequeSlipHeader();
                IMPLLib.ChequeSlipDetail objChequeSlipDtl = new IMPLLib.ChequeSlipDetail();
                IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                DataTable objPODataTbl = new DataTable();
                string sResult = null;

                objChequeSlipHdr.ChequeSlipDate = txtChequeSlipDate.Text;
                objChequeSlipHdr.ChequeSlipNo = txtChequeSlipNo.Text;
                objChequeSlipHdr.BranchCode = strBranchCode;
                objChequeSlipHdr.Remarks = txtRemarks.Text;
                objChequeSlipHdr.TotalAmount = txtAmount.Text;
                objChequeSlipHdr.ChartofAccount = txtChartofAccount.Text;
                objChequeSlipHdr.ChequeNo = txtChequeNo.Text;
                objChequeSlipHdr.ChequeDate = txtChequeDate.Text;
                objChequeSlipHdr.Bank = txtBank.Text;
                objChequeSlipHdr.Branch = txtBranch.Text;
                objChequeSlipHdr.SupCode = ddlSupplier.SelectedValue.ToString();
                if (string.IsNullOrEmpty(txtCalculatedAmount.Text))
                {
                    objChequeSlipHdr.CalcAmount = 0;
                }
                else
                {
                    objChequeSlipHdr.CalcAmount = Convert.ToDecimal(txtCalculatedAmount.Text);
                }
                objChequeSlipHdr.RefDocFromDate = txtFromdateDate.Text;
                hdnFromDate.Value = txtFromdateDate.Text;
                hdnToDate.Value = txtToDate.Text;
                objChequeSlipHdr.RefDocToDate = txtToDate.Text;
                objChequeSlipHdr.RefType = ddlRefType.SelectedValue.ToString();
                objChequeSlipHdr.CashDiscCode = Convert.ToInt32(Convert.ToDouble(ddlCashDiscount.SelectedValue));
                if (ddlCashDiscount.SelectedValue == "0")
                {
                    objChequeSlipHdr.CashDiscPer = 0;
                }
                else
                {
                    objChequeSlipHdr.CashDiscPer = Convert.ToInt32(Convert.ToDouble(ddlCashDiscount.SelectedValue));
                }


                objPODataTbl = (DataTable)ViewState["CurrentTable"];


                for (int linti = 0; linti <= grdChequeDtl.Rows.Count - 1; linti++)
                {
                    objPODataTbl.Rows[linti]["IsSelected"] = "0";

                    if (((CheckBox)grdChequeDtl.Rows[linti].FindControl("chkSelected")).Checked == true)
                    {
                        objPODataTbl.Rows[linti]["IsSelected"] = "1";

                        if (string.IsNullOrEmpty(((TextBox)grdChequeDtl.Rows[linti].FindControl("txtCDValue")).Text) || (((TextBox)grdChequeDtl.Rows[linti].FindControl("txtCDValue")).Text == "0"))
                        {
                            objPODataTbl.Rows[linti]["CDValue"] = Convert.ToDecimal(0);
                        }
                        else
                        {
                            objPODataTbl.Rows[linti]["CDValue"] = Convert.ToDecimal(((TextBox)grdChequeDtl.Rows[linti].FindControl("txtCDValue")).Text);
                        }

                        if (string.IsNullOrEmpty(((TextBox)grdChequeDtl.Rows[linti].FindControl("txtTotalValue")).Text) || (((TextBox)grdChequeDtl.Rows[linti].FindControl("txtTotalValue")).Text == "0"))
                        {
                            objPODataTbl.Rows[linti]["TotalValue"] = Convert.ToDecimal(0);
                        }
                        else
                        {
                            objPODataTbl.Rows[linti]["TotalValue"] = Convert.ToDecimal(((TextBox)grdChequeDtl.Rows[linti].FindControl("txtTotalValue")).Text);
                        }
                    }
                }

                ViewState["CurrentTable"] = objPODataTbl;

                sResult = objChequeSlip.SubmitChequeSlip(objChequeSlipHdr, objPODataTbl);
                if (!string.IsNullOrEmpty(sResult))
                {
                    ViewState["CurrentTable"] = "";
                    grdChequeDtl.DataSource = objPODataTbl;
                    grdChequeDtl.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Process completed');", true);
                    txtChequeSlipNo.Text = sResult;
                    txtAmount.Text = txtCalculatedAmount.Text;
                    txtChequeSlipNo.Enabled = false;
                    btnReport.Visible = true;
                    btnReportExcel.Visible = true;
                    BtnSubmit.Enabled = false;
                    ViewChequeSlip(sResult);
                    DisableViewMode();
                    updDocumentDetail.Update();
                    updPanelPartTwo.Update();
                    updPanelPartOne.Update();
                    updGridLineItem.Update();
                }
                else if (sResult != "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + sResult + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //LoadChequeSlipItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ChkHeader.Visible = true;
                ChkHeader.Enabled = true;
                grdChequeDtl.Visible = true;
                LoadChequeSlipItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlCashDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //LoadChequeSlipItems();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("ChequeSlip.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grdChequeDtl.Visible = true;
                SaveChequeSlip();
                LoadChequeSlipNumber();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlChequeSlipNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ViewChequeSlip(ddlChequeSlipNo.SelectedItem.Text.ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        private void ViewChequeSlip(string strChequeSlipNo)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPLLib.ChequeSlip objChequeSlip = new IMPLLib.ChequeSlip();
                IMPLLib.ChequeSlipHeader objChequeSlipHdr = new IMPLLib.ChequeSlipHeader();

                //objChequeSlipHdr = objChequeSlip.LoadCheckSlipViewRec(ddlChequeSlipNo.SelectedItem.Text.ToString());
                objChequeSlipHdr = objChequeSlip.LoadCheckSlipViewRec(strChequeSlipNo, Session["BranchCode"].ToString());

                LoadCashDicount();

                ConstructColumn(false);

                if (objChequeSlipHdr != null)
                {
                    DateTime dtChequeSlip = (DateTime.Parse(objChequeSlipHdr.ChequeSlipDate.ToString()));
                    txtChequeSlipDate.Text = dtChequeSlip.ToString("dd/MM/yyyy");
                    txtBranchName.Text = fnGetBranch(strBranchCode);
                    txtRemarks.Text = objChequeSlipHdr.Remarks;
                    double calcamt = Math.Round(Convert.ToDouble(objChequeSlipHdr.CalcAmount.ToString()), 2);
                    txtAmount.Text = calcamt.ToString("0.00");
                    txtChartofAccount.Text = objChequeSlipHdr.ChartofAccount;
                    txtChequeNo.Text = objChequeSlipHdr.ChequeNo;
                    DateTime dtchedate = (DateTime.Parse(objChequeSlipHdr.ChequeDate.ToString()));
                    txtChequeDate.Text = dtchedate.ToString("dd/MM/yyyy");

                    IMPLLib.ChequeSlipBankDetails objBankDtl = new IMPLLib.ChequeSlipBankDetails();
                    objBankDtl = objChequeSlip.GetChequeSlipBankDetails(txtChartofAccount.Text);
                    txtBank.Text = objBankDtl.BankName;
                    txtBranch.Text = objBankDtl.Address;
                    ddlSupplier.SelectedValue = objChequeSlipHdr.SupCode;
                    txtFromdateDate.Text = hdnFromDate.Value;
                    txtToDate.Text = hdnToDate.Value;

                    txtCalculatedAmount.Text = calcamt.ToString("0.00");
                    ddlRefType.SelectedIndex = 1;
                    // ddlCashDiscount.SelectedValue = Convert.ToString(objChequeSlipHdr.CashDiscCode);
                }

                DataTable objPODataTbl = new DataTable();
                DataRow dtRowItem;

                objPODataTbl = (DataTable)ViewState["CurrentTable"];

                int linti = 0;

                foreach (IMPLLib.ChequeSlipDetail objChequeSlipDetail in objChequeSlipHdr.ChequeSlipItems)
                {
                    if (linti != 0)
                    {
                        dtRowItem = objPODataTbl.NewRow();

                        objPODataTbl.Rows.Add(dtRowItem);
                    }

                    objPODataTbl.Rows[linti]["Indicator"] = string.IsNullOrEmpty(objChequeSlipDetail.Indicator) ? "" : objChequeSlipDetail.Indicator.ToString();
                    objPODataTbl.Rows[linti]["BranchCode"] = string.IsNullOrEmpty(objChequeSlipDetail.BranchCode) ? "" : objChequeSlipDetail.BranchCode.ToString();
                    objPODataTbl.Rows[linti]["InvoiceNo"] = string.IsNullOrEmpty(objChequeSlipDetail.InvoiceNo) ? "" : objChequeSlipDetail.InvoiceNo.ToString();
                    objPODataTbl.Rows[linti]["InvoiceDate"] = string.IsNullOrEmpty(objChequeSlipDetail.InvoiceDate) ? "" : objChequeSlipDetail.InvoiceDate.ToString();
                    objPODataTbl.Rows[linti]["InvoiceValue"] = string.IsNullOrEmpty(objChequeSlipDetail.InvoiceValue) ? "" : objChequeSlipDetail.InvoiceValue.ToString();
                    objPODataTbl.Rows[linti]["CDValue"] = string.IsNullOrEmpty(objChequeSlipDetail.CDValue) ? "" : objChequeSlipDetail.CDValue.ToString();
                    objPODataTbl.Rows[linti]["TotalValue"] = string.IsNullOrEmpty(objChequeSlipDetail.TotalValue) ? "" : objChequeSlipDetail.TotalValue.ToString();

                    linti++;
                }

                ViewState["CurrentTable"] = objPODataTbl;

                grdChequeDtl.DataSource = objPODataTbl;
                grdChequeDtl.DataBind();
                linti = 0;
                foreach (IMPLLib.ChequeSlipDetail objChequeSlipDetail in objChequeSlipHdr.ChequeSlipItems)
                {
                    grdChequeDtl.Rows[linti].Cells[1].Text = string.IsNullOrEmpty(objChequeSlipDetail.Indicator) ? "" : objChequeSlipDetail.Indicator.ToString();
                    grdChequeDtl.Rows[linti].Cells[2].Text = string.IsNullOrEmpty(objChequeSlipDetail.BranchCode) ? "" : objChequeSlipDetail.BranchCode.ToString();
                    grdChequeDtl.Rows[linti].Cells[3].Text = string.IsNullOrEmpty(objChequeSlipDetail.InvoiceNo) ? "" : objChequeSlipDetail.InvoiceNo.ToString();

                    DateTime invdate = DateTime.Parse(string.IsNullOrEmpty(objChequeSlipDetail.InvoiceDate) ? "" : objChequeSlipDetail.InvoiceDate.ToString());

                    grdChequeDtl.Rows[linti].Cells[4].Text = invdate.ToString("dd/MM/yyyy");

                    double cdval = Math.Round(Convert.ToDouble(string.IsNullOrEmpty(objChequeSlipDetail.CDValue) ? "" : objChequeSlipDetail.CDValue.ToString()), 2);
                    double totVal = Math.Round(Convert.ToDouble(string.IsNullOrEmpty(objChequeSlipDetail.TotalValue) ? "" : objChequeSlipDetail.TotalValue.ToString()), 2);
                    double invVal = Math.Round(Convert.ToDouble(string.IsNullOrEmpty(objChequeSlipDetail.InvoiceValue) ? "" : objChequeSlipDetail.InvoiceValue.ToString()), 2);

                    grdChequeDtl.Rows[linti].Cells[5].Text = invVal.ToString("0.00");

                    ((TextBox)grdChequeDtl.Rows[linti].FindControl("txtCDValue")).Text = cdval.ToString("0.00");
                    ((TextBox)grdChequeDtl.Rows[linti].FindControl("txtTotalValue")).Text = totVal.ToString("0.00");
                    ((CheckBox)grdChequeDtl.Rows[linti].FindControl("chkSelected")).Checked = true;
                    linti++;
                }

                updDocumentDetail.Update();
                updPanelPartTwo.Update();
                updPanelPartOne.Update();
                updGridLineItem.Update();
                DisableViewMode();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableViewMode()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChequeSlipDate.Enabled = false;
                txtRemarks.Enabled = false;
                txtChequeSlipNo.Enabled = false;
                txtChartofAccount.Enabled = false;
                txtAmount.Enabled = false;
                txtCalculatedAmount.Enabled = false;
                txtChequeNo.Enabled = false;
                txtChequeDate.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ddlSupplier.Enabled = false;
                txtFromdateDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlRefType.Enabled = false;
                grdChequeDtl.Enabled = false;
                BtnSubmit.Enabled = false;
                ddlCashDiscount.Enabled = false;
                imgEditToggle.Visible = false;
                ucChartofAccount.Visible = false;
                ChkHeader.Enabled = false;
                //ImgChequeDt.Visible = false;
                //ImgFromDt.Visible = false;
                //ImgToDt.Visible = false;
                updPanelPartTwo.Update();
                updDocumentDetail.Update();
                updPanelPartOne.Update();
                updGridLineItem.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ResetForm()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtChequeSlipDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlChequeSlipNo.Visible = false;
                txtChequeSlipNo.Text = "";
                txtChequeSlipNo.Enabled = false;
                txtChequeSlipNo.Visible = true;
                txtChartofAccount.Enabled = false;
                //txtAmount.Enabled = true;
                // txtAmount.Text = "";
                txtCalculatedAmount.Enabled = false;
                txtCalculatedAmount.Text = "0.00";
                txtAmount.Text = "";
                txtChequeNo.Text = "";
                // txtChequeDate.Text = "";
                txtChequeDate.Enabled = false;
                txtBank.Text = "";
                txtBranch.Text = "";
                LoadSupplierLine();
                txtFromdateDate.Text = "";
                txtToDate.Text = "";
                ddlRefType.Enabled = true;
                ddlRefType.SelectedIndex = 0;
                LoadCashDicount();
                ConstructColumn(true);
                grdChequeDtl.DataSource = null;
                grdChequeDtl.DataBind();
                ddlSupplier.Enabled = true;
                BtnSubmit.Enabled = true;
                btnReport.Visible = false;
                btnReportExcel.Visible = false;
                ddlCashDiscount.Enabled = true;
                imgEditToggle.Visible = true;
                //ImgChequeDt.Visible = true;
                //ImgFromDt.Visible = true;
                //ImgToDt.Visible = true;
                ucChartofAccount.Visible = true;
                txtRemarks.Text = "";
                txtRemarks.Enabled = true;
                txtChartofAccount.Text = "";
                txtChequeNo.Enabled = true;
                //txtChequeDate.Enabled = true;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                txtFromdateDate.Enabled = true;
                txtToDate.Enabled = true;
                grdChequeDtl.Enabled = true;
                ChkHeader.Visible = false;
                updPanelPartTwo.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        
        protected void grdChequeDtl_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (Convert.ToInt32(e.Row.RowIndex) > 0)
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        int index = Convert.ToInt32(e.Row.RowIndex);
            //        ((CheckBox)grdChequeDtl.Rows[index-1].FindControl("chkSelected")).Attributes.Add("Click", "void CalculateDiscAmount('" + ddlCashDiscount.ClientID.ToString() + "','" + grdChequeDtl.Rows[index-1].FindControl("txtCDValue").ClientID.ToString() + "','" + grdChequeDtl.Rows[index-1].FindControl("txtTotalValue").ClientID.ToString() + "','" + txtCalculatedAmount.ClientID.ToString() + "','" + ((CheckBox)grdChequeDtl.Rows[index-1].FindControl("chkSelected")).Checked.ToString() + "');");
            //    }
            //}
        }

        protected void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                //if (ddlCashDiscount.SelectedIndex == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Please select cash discount percentage');", true);
                //    return;
                //}


                double cdper;
                double cdvalue;
                int NoOfRowsChecked = 0;

                if (ddlCashDiscount.SelectedValue != "0.00")
                {
                    cdper = Convert.ToDouble(ddlCashDiscount.SelectedValue, null);
                }
                else
                {
                    cdper = 0;
                }

                if (grdChequeDtl.Rows[gr.RowIndex].Cells[1].Text == "PO")
                {

                    cdvalue = ((Convert.ToDouble(grdChequeDtl.Rows[gr.RowIndex].Cells[5].Text, null) * cdper) / 100);
                }

                else
                {
                    cdvalue = Convert.ToDouble("0.00");
                }

                if (chk.Checked == true)
                {
                    ((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtCDValue")).Text = Math.Round(cdvalue, 2).ToString("0.00");
                    ((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtTotalValue")).Text = Math.Round((Convert.ToDouble(grdChequeDtl.Rows[gr.RowIndex].Cells[5].Text, null) - cdvalue), 2).ToString("0.00");
                    txtCalculatedAmount.Text = Math.Round((Convert.ToDouble(txtCalculatedAmount.Text) + Convert.ToDouble(((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtTotalValue")).Text)), 2).ToString("0.00");
                    NoOfRowsChecked += 1;
                }
                else
                {
                    string invval = grdChequeDtl.Rows[gr.RowIndex].Cells[5].Text;
                    string cdval = ((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtCDValue")).Text;

                    double val = Convert.ToDouble(invval) - Convert.ToDouble(cdval);

                    txtCalculatedAmount.Text = Math.Round((Convert.ToDouble(txtCalculatedAmount.Text) - val), 2).ToString("0.00");
                    ((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtCDValue")).Text = "0.00";
                    ((TextBox)grdChequeDtl.Rows[gr.RowIndex].FindControl("txtTotalValue")).Text = "0.00"; //grdChequeDtl.Rows[gr.RowIndex].Cells[5].Text.ToString();
                }
                if (grdChequeDtl.Rows.Count == NoOfRowsChecked)
                {
                    ChkHeader.Checked = true;
                }
                else
                {
                    ChkHeader.Checked = false;
                    //txtCalculatedAmount.Text = "0.00";
                }
                //updDocumentDetail.Update();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtCDValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetOnBlurEvent()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                for (int linti = 0; linti <= grdChequeDtl.Rows.Count - 1; linti++)
                {
                    TextBox txtCdValue = (TextBox)grdChequeDtl.Rows[linti].FindControl("txtCDValue");
                    TextBox txtTotalValue = (TextBox)grdChequeDtl.Rows[linti].FindControl("txtTotalValue");
                    CheckBox chkCheckVal = (CheckBox)grdChequeDtl.Rows[linti].FindControl("chkSelected");

                    var invoiceVal = grdChequeDtl.Rows[linti].Cells[5].Text.ToString();

                    //RowId/CD Value control client id/Checbox flag value/Total Value/Total Value Client Id/Invoice Value
                    //txtCdValue.Attributes.Add("OnChange", "return CdValueOnBlur('" + linti + "','" + txtCdValue.ClientID.ToString() + "','" + chkCheckVal.ClientID.ToString() + "','" + txtTotalValue.ClientID.ToString() + "','" + txtCalculatedAmount.ClientID.ToString() + "','" + invoiceVal + "');");
                    //txtTotalValue.Attributes.Add("OnChange", "return CdValueOnBlur('" + linti + "','" + txtCdValue.ClientID.ToString() + "','" + chkCheckVal.ClientID.ToString() + "','" + txtTotalValue.ClientID.ToString() + "','" + txtCalculatedAmount.ClientID.ToString() + "','" + invoiceVal + "');");
                    //chkCheckVal.Attributes.Add("Click", "return CdValueOnBlur('" + linti + "','" + txtCdValue.ClientID.ToString() + "','" + chkCheckVal.ClientID.ToString() + "','" + txtTotalValue.ClientID.ToString() + "','" + txtCalculatedAmount.ClientID.ToString() + "','" + invoiceVal + "');");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region Generate Report Button Click

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            GenerateAndExportReport(".xls");
        }
        protected void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = default(string);
            string strChequeSlipNo = default(string);
            string strChequeNumber = default(string);

            updPanelPartOne.Visible = false;
            updPanelPartTwo.Visible = false;
            updDocumentDetail.Visible = false;
            updGridLineItem.Visible = false;
            btnBack.Visible = true;

            if (ddlChequeSlipNo.SelectedValue.ToString() != "" && ddlChequeSlipNo.SelectedValue.ToString() != null)
                strChequeNumber = ddlChequeSlipNo.SelectedValue;
            else
                strChequeNumber = txtChequeNo.Text;

            if ((string)Session["RoleCode"] == "BEDP")
            {
                crChequeSlip.ReportName = "ChequeSlipReport_Branch";
            }
            else
            {
                crChequeSlip.ReportName = "ChequeSlipReport_COR";
            }

            strChequeSlipNo = "{Cheque_Slip_Header.Cheque_Slip_Number}";

            strSelectionFormula = strChequeSlipNo + "= '" + strChequeNumber + "'";

            crChequeSlip.RecordSelectionFormula = strSelectionFormula;
            crChequeSlip.GenerateReportAndExportA4(fileType);
        }
        #endregion
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Back Button Clicked");
            try
            {
                Server.ClearError();
                Response.Redirect("ChequeSlip.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        protected void ChkHeader_CheckedChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ChkHeader_CheckedChanged", "Select All button is checked");
            try
            {
                double cdper;
                double cdvalue;
                int NoOfRowsChecked = 0;

                if (ChkHeader.Checked)
                {
                    foreach (GridViewRow gvr in grdChequeDtl.Rows)
                    {
                        if (ddlCashDiscount.SelectedValue != "0.00")
                        {
                            cdper = Convert.ToDouble(ddlCashDiscount.SelectedValue, null);
                        }
                        else
                        {
                            cdper = 0;
                        }

                        if (grdChequeDtl.Rows[gvr.RowIndex].Cells[1].Text == "PO")
                        {

                            cdvalue = ((Convert.ToDouble(grdChequeDtl.Rows[gvr.RowIndex].Cells[5].Text, null) * cdper) / 100);
                        }
                        else
                        {
                            cdvalue = Convert.ToDouble("0.00");
                        }
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("chkSelected");
                            ChkSelected.Checked = true;
                            ((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtCDValue")).Text = Math.Round(cdvalue, 2).ToString("0.00");
                            ((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtTotalValue")).Text = Math.Round((Convert.ToDouble(grdChequeDtl.Rows[gvr.RowIndex].Cells[5].Text, null) - cdvalue), 2).ToString("0.00");
                            txtCalculatedAmount.Text = Math.Round((Convert.ToDouble(txtCalculatedAmount.Text) + Convert.ToDouble(((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtTotalValue")).Text)), 2).ToString("0.00");
                            NoOfRowsChecked += 1;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow gvr in grdChequeDtl.Rows)
                    {
                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox ChkSelected = (CheckBox)gvr.FindControl("chkSelected");
                            ChkSelected.Checked = false;
                            string invval = grdChequeDtl.Rows[gvr.RowIndex].Cells[5].Text;
                            string cdval = ((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtCDValue")).Text;

                            double val = Convert.ToDouble(invval) - Convert.ToDouble(cdval);

                            txtCalculatedAmount.Text = Math.Round((Convert.ToDouble(txtCalculatedAmount.Text) - val), 2).ToString("0.00");
                            ((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtCDValue")).Text = "0.00";
                            ((TextBox)grdChequeDtl.Rows[gvr.RowIndex].FindControl("txtTotalValue")).Text = "0.00";
                        }
                    }

                }
                //updDocumentDetail.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
