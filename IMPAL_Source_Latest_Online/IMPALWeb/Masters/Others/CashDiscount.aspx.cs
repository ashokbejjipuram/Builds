
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPLib = IMPALLibrary.Masters.Others;
using IMPALLibrary;

namespace IMPALWeb.Masters.Others
{
    public partial class CashDiscount : System.Web.UI.Page
    {
        IMPLib.CashDiscount objCashDiscount = new IMPLib.CashDiscount();
        IMPLib.CashDiscountMaster objCashDiscountMst = new IMPLib.CashDiscountMaster();
        #region Page_Load
        /// <summary>
/// To load page
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    cashDiscountFormView.DefaultMode = FormViewMode.Insert;
                    LoadCashDicount();
                    LoadSupLine();
                    //Enable_DisableControls();
                    if ((string)Session["RoleCode"] == "CORP")
                    {
                        cashDiscountFormView.DefaultMode = FormViewMode.Insert;
                        PnlCustomer.Enabled = true;
                        BtnSubmit.Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).SelectedIndex = 0;
                        ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Enabled = false;
                        RadioButtonList rdIndicator = ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator"));
                        if (rdIndicator.SelectedValue == "C")
                        {
                            CustomValidator cCustValSupDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValSupDueDays"));
                            cCustValSupDueDays.Enabled = false;
                            CustomValidator cCustValLineItem = ((CustomValidator)cashDiscountFormView.FindControl("CustValLineItem"));
                            cCustValLineItem.Enabled = false;
                            CustomValidator cCustValCustDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValCustDueDays"));
                            cCustValCustDueDays.Enabled = true;
                        }
                        else
                        {
                            CustomValidator cCustValSupDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValSupDueDays"));
                            cCustValSupDueDays.Enabled = true;
                            CustomValidator cCustValLineItem = ((CustomValidator)cashDiscountFormView.FindControl("CustValLineItem"));
                            cCustValLineItem.Enabled = true;
                            CustomValidator cCustValCustDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValCustDueDays"));
                            cCustValCustDueDays.Enabled = false;
                        }
                    }
                    else if ((string)Session["RoleCode"] == "BEDP")
                    {
                        //cashDiscountFormView.DefaultMode = FormViewMode.ReadOnly;
                        //PnlCustomer.Enabled = false;
                        BtnSubmit.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {

                Log.WriteException(Source, exp);
            }
        }

        #endregion
        #region Page Init
        /// <summary>
        /// To initialize page
        /// </summary>
        protected void Page_Init()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
            try
            {
                cashDiscountFormView.EditItemTemplate = cashDiscountFormView.ItemTemplate;
                cashDiscountFormView.InsertItemTemplate = cashDiscountFormView.ItemTemplate;
            }
            catch (Exception exp)
            {

                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region BindCashDiscout
        /// <summary>
        /// To bind Cash discount
        /// </summary>
        private void BindCashDiscout()
        {
            cashDiscountFormView.DefaultMode = FormViewMode.ReadOnly;
            IMPLib.CashDiscountMaster objCashDiscountMst = new IMPLib.CashDiscountMaster();
            IMPLib.CashDiscount objCashDisc = new IMPLib.CashDiscount();
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "BindCashDiscout", "Inside BindCashDiscout");
            try
            {
                objCashDisc = objCashDiscountMst.GetCashDicount(Session["BranchCode"].ToString(), drpDiscCode.SelectedValue.ToString());

            if (objCashDisc != null)
            {

                cashDiscountFormView.DefaultMode = FormViewMode.Edit;
                PnlCustomer.Enabled = true;


                ((TextBox)cashDiscountFormView.FindControl("txtCashCode")).Text = drpDiscCode.SelectedValue.ToString();
                ((TextBox)cashDiscountFormView.FindControl("txtNorms")).Text = objCashDisc.CashDiscDesc;

                if (objCashDisc.PaymentIndicator == "S")
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).SelectedIndex = 0;
                else if (objCashDisc.PaymentIndicator == "C")
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).Items[0].Enabled = false;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).SelectedIndex = 1;

                ((TextBox)cashDiscountFormView.FindControl("txtDiscount")).Text = objCashDisc.DiscountPer;

                if (objCashDisc.PaymentIndicator == "C")
                {
                    ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Text = objCashDisc.SupDueDays;
                }

                if (objCashDisc.PaymentIndicator != "C")
                {
                    ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Text = objCashDisc.SupDueDays;
                    ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Enabled = false;
                }

                ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).SelectedIndex = 0;
                ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).Enabled = false;


                if ((string)Session["RoleCode"] == "CORP")
                {
                    if (objCashDisc.Indicator == "L")
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 0;
                    else
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 1;
                }
                else
                {
                    if (objCashDisc.Indicator == "L")
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 0;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[1].Enabled = false;
                    }
                    else
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 1;
                    }
                }


                if ((string)Session["RoleCode"] == "CORP")
                {
                    if (objCashDisc.CalcIndicator == "L")
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 0;
                    else
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 1;
                }
                else
                {
                    if (objCashDisc.CalcIndicator == "L")
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 0;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[1].Enabled = false;
                    }
                    else
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 1;
                    }
                }



                if ((string)Session["RoleCode"] == "CORP")
                {
                    if (objCashDisc.PurchaseIndicator == "H")
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 0;
                    else
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 1;
                }
                else
                {
                    if (objCashDisc.PurchaseIndicator == "H")
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 0;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[1].Enabled = false;
                    }
                    else
                    {
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 1;
                    }
                }

            }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion  
        #region LoadCashDiscount
        /// <summary>
        /// To load cash discount
        /// </summary>
        public void LoadCashDicount()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "LoadCashDicount", "Inside LoadCashDicount");
            try
            {
            IMPLib.CashDiscountMaster objCashDiscountMst = new IMPLib.CashDiscountMaster();
            drpDiscCode.DataSource = objCashDiscountMst.GetCashDiscountCode();
            drpDiscCode.DataTextField = "CashDiscName";
            drpDiscCode.DataValueField = "CashDiscCode";
            drpDiscCode.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region LoadSupLine
        /// <summary>
        /// To load ddl for supplier line
        /// </summary>
        public void LoadSupLine()
        {
             Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
             //Log.WriteLog(source, "LoadSupLine", "Inside LoadSupLine");
            try
            {
            IMPLib.CashDiscountMaster objCashDiscountMst = new IMPLib.CashDiscountMaster();
            var ddlLineItemCtl = (DropDownList)cashDiscountFormView.FindControl("ddlLineItem");
            ddlLineItemCtl.DataSource = objCashDiscountMst.GetSupLine();
            ddlLineItemCtl.DataTextField = "SupplierLineName";
            ddlLineItemCtl.DataValueField = "SupplierLineCode";
            ddlLineItemCtl.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region btnSearch_Click
        /// <summary>
        /// To search 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnSearch_Click", "Inside btnSearch_Click");
            try
            {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                cashDiscountFormView.DefaultMode = FormViewMode.ReadOnly;
                PnlCustomer.Enabled = false;
                BtnSubmit.Enabled = false;
            }
            else if ((string)Session["RoleCode"] == "CORP")
            {
                cashDiscountFormView.DefaultMode = FormViewMode.Insert;
                PnlCustomer.Enabled = true;
                BtnSubmit.Enabled = true;

            }
            BindCashDiscout();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        #region BtnSubmit_Click
        /// <summary>
        /// Button submit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "BtnSubmit_Click", "Inside BtnSubmit_Click");
            try
            {

            if (cashDiscountFormView.DefaultMode == FormViewMode.Insert)
            {
                var txtNorms = ((TextBox)cashDiscountFormView.FindControl("txtNorms"));
                var rdoIndicator = ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator"));
                var txtDisc = ((TextBox)cashDiscountFormView.FindControl("txtDiscount"));
                var txtCusDue = ((TextBox)cashDiscountFormView.FindControl("txtDueDays"));
                var txtSupDue = ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays"));
                var drpLineItem = ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem"));
                var rdoCalcIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator"));
                var rdoPurIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator"));
                var rdoPayIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator"));

                objCashDiscountMst.AddNewCashDiscountMaster("", txtNorms.Text, txtNorms.Text, rdoIndicator.SelectedValue,
                                    txtDisc.Text, txtCusDue.Text, txtSupDue.Text, drpLineItem.SelectedValue.ToString(), drpLineItem.SelectedItem.Text,
                                    rdoCalcIndi.SelectedValue, rdoPurIndi.SelectedValue, rdoPayIndi.SelectedValue, Session["BranchCode"].ToString());

            }
            else if (cashDiscountFormView.DefaultMode == FormViewMode.Edit)
            {

                var txtCashCode = ((TextBox)cashDiscountFormView.FindControl("txtCashCode"));
                var txtNorms = ((TextBox)cashDiscountFormView.FindControl("txtNorms"));
                var rdoIndicator = ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator"));
                var txtDisc = ((TextBox)cashDiscountFormView.FindControl("txtDiscount"));
                var txtCusDue = ((TextBox)cashDiscountFormView.FindControl("txtDueDays"));
                var txtSupDue = ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays"));
                var drpLineItem = ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem"));
                var rdoCalcIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator"));
                var rdoPurIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator"));
                var rdoPayIndi = ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator"));

                objCashDiscountMst.UpdateCashDiscountMaster(Session["BranchCode"].ToString(), txtCashCode.Text, txtNorms.Text, txtNorms.Text, rdoIndicator.SelectedValue,
                                    txtDisc.Text, txtCusDue.Text, txtSupDue.Text, drpLineItem.SelectedValue.ToString(), drpLineItem.SelectedItem.Text,
                                    rdoCalcIndi.SelectedValue, rdoPurIndi.SelectedValue, rdoPayIndi.SelectedValue);

            }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                //var DiscountCode = drpDiscCode.SelectedValue;
                Server.Execute("CashDiscountReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region rdIndicator_SelectedIndexChanged
        protected void rdIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {

            Enable_DisableControls();
             RadioButtonList rdIndicator = ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator"));
             if (rdIndicator.SelectedValue == "C")
             {
                 CustomValidator cCustValSupDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValSupDueDays"));
                 cCustValSupDueDays.Enabled = false;
                 CustomValidator cCustValLineItem = ((CustomValidator)cashDiscountFormView.FindControl("CustValLineItem"));
                 cCustValLineItem.Enabled = false;
                 CustomValidator cCustValCustDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValCustDueDays"));
                 cCustValCustDueDays.Enabled = true;
             }
             else
             {
                 CustomValidator cCustValSupDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValSupDueDays"));
                 cCustValSupDueDays.Enabled = true;
                 CustomValidator cCustValLineItem = ((CustomValidator)cashDiscountFormView.FindControl("CustValLineItem"));
                 cCustValLineItem.Enabled = true;
                 CustomValidator cCustValCustDueDays = ((CustomValidator)cashDiscountFormView.FindControl("CustValCustDueDays"));
                 cCustValCustDueDays.Enabled = false;
             }

        }
        #endregion
        #region btnReset_Click
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReset_Click", "Inside btnReset_Click");
           
            try
            {
                drpDiscCode.SelectedIndex = 0;
                ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).SelectedIndex = 0;
                ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).SelectedIndex = 0;
                ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 0;
                ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 0;
                ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 0;
                Enable_DisableControls();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion
        #region Enable_DisableControls
        public void Enable_DisableControls()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Enable_DisableControls", "Inside Enable_DisableControls");
            try
            {

                ((TextBox)cashDiscountFormView.FindControl("txtCashCode")).Text = "";
                ((TextBox)cashDiscountFormView.FindControl("txtNorms")).Text = "";
                ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Text = "";
                ((TextBox)cashDiscountFormView.FindControl("txtDiscount")).Text = "";
                ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Text = "";

                ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).Items[0].Enabled = true;
                ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator")).Items[1].Enabled = true;

                if ((string)Session["RoleCode"] == "BEDP")
                {
                    ((TextBox)cashDiscountFormView.FindControl("txtNorms")).Enabled = false;
                    ((TextBox)cashDiscountFormView.FindControl("txtCashCode")).Enabled = false;
                    ((TextBox)cashDiscountFormView.FindControl("txtDiscount")).Enabled = false;
                    ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Enabled = false;
                    ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Enabled = false;
                    //((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).SelectedIndex = 0;
                    ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).Enabled = true;
                    //((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).SelectedIndex = 0;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[0].Enabled = true;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[1].Enabled = true;
                    //((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).SelectedIndex = 0;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[0].Enabled = true;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[1].Enabled = true;
                    //((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).SelectedIndex = 0;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[0].Enabled = true;
                    ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[1].Enabled = true;

                }
                else if ((string)Session["RoleCode"] == "CORP")
                {
                    RadioButtonList rdIndicator = ((RadioButtonList)cashDiscountFormView.FindControl("rdIndicator"));
                    if (rdIndicator.SelectedValue == "C")
                    {
                        ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Enabled = true;
                        ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Enabled = false;
                        ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[1].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[1].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[0].Enabled = false;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[1].Enabled = false;
                    }
                    else
                    {
                        ((TextBox)cashDiscountFormView.FindControl("txtDueDays")).Enabled = false;
                        ((TextBox)cashDiscountFormView.FindControl("txtSupDueDays")).Enabled = true;
                        ((DropDownList)cashDiscountFormView.FindControl("ddlLineItem")).Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[0].Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlCalcIndicator")).Items[1].Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[0].Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPurchaseIndicator")).Items[1].Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[0].Enabled = true;
                        ((RadioButtonList)cashDiscountFormView.FindControl("rdlPaymentIndicator")).Items[1].Enabled = true;
                    }
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
