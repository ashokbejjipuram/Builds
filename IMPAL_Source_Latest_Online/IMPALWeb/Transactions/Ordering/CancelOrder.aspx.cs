#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Transactions.Ordering
{
    public partial class CancelOrder : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CancelOrder), exp);
            }
        }

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region radType_SelectedIndexChanged
        protected void radType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                divPartNo.Style.Add("display", "none");
                lblPartNo.Style.Add("display", "none");
                ddlOrderNo.Style.Add("display", "none");
                lblOrderNo.Style.Add("display", "none");
                divOrderDetails.Style.Add("display", "none");
                btnUpdate.Style.Add("display", "none");
                divMessage.Style.Add("display", "none");
                if (ddlSupplier.SelectedIndex > 0)
                {
                    switch (radType.SelectedValue)
                    {
                        case "PartNo":
                            divPartNo.Style.Add("display", "");
                            lblPartNo.Style.Add("display", "");
                            BindPartNoDDL();
                            break;
                        case "OrderNo":
                            ddlOrderNo.Style.Add("display", "");
                            lblOrderNo.Style.Add("display", "");
                            BindOrderNoDDL();
                            break;
                        case "Month":
                            BindOrderDetails(null, null);
                            break;
                    }
                    ddlSupplier.Enabled = false;
                }
                else
                    radType.ClearSelection();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Bind PartNo and OrderNo DDLs
        private void BindPartNoDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Cancellation objCancel = new IMPALLibrary.Cancellation();
                List<IMPALLibrary.CancelOrderProp> lstPartNo = objCancel.GetPartNo(ddlSupplier.SelectedValue, Session["BranchCode"].ToString());
                ddlPartNo.DataSource = lstPartNo;
                ddlPartNo.DataTextField = "PartNum";
                ddlPartNo.DataValueField = "ItemCode";
                ddlPartNo.DataBind();
                ddlPartNo.Items.Insert(0, string.Empty);
                ddlPartNo.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        private void BindOrderNoDDL()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Cancellation objCancel = new IMPALLibrary.Cancellation();
                List<IMPALLibrary.CancelOrderProp> lstOrderNo = objCancel.GetOrderNo(ddlSupplier.SelectedValue, Session["BranchCode"].ToString());
                ddlOrderNo.DataSource = lstOrderNo;
                ddlOrderNo.DataTextField = "OrderNum";
                ddlOrderNo.DataValueField = "OrderNum";
                ddlOrderNo.DataBind();
                ddlOrderNo.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region BindOrderDetails
        private void BindOrderDetails(string PartNo, string OrderNo)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.Cancellation objCancel = new IMPALLibrary.Cancellation();
                List<IMPALLibrary.CancelOrderProp> lstOrder = objCancel.GetOrderDetails(radType.SelectedValue, ddlSupplier.SelectedValue, PartNo, OrderNo, Session["BranchCode"].ToString());
                if (lstOrder.Count > 0)
                {
                    rOrderDetails.DataSource = lstOrder;
                    rOrderDetails.DataBind();
                    btnUpdate.Style.Add("display", "");
                    divOrderDetails.Style.Add("display", "");
                    divMessage.Style.Add("display", "none");
                }
                else
                {
                    btnUpdate.Style.Add("display", "none");
                    divOrderDetails.Style.Add("display", "none");
                    divMessage.Style.Add("display", "");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlPartNo_IndexChanged
        protected void ddlPartNo_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlPartNo.SelectedIndex > 0)
                {
                    BindOrderDetails(ddlPartNo.SelectedValue, null);
                }
                else
                {
                    divMessage.Style.Add("display", "none");
                }
                ddlOrderNo.ClearSelection();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlOrderNo_IndexChanged
        protected void ddlOrderNo_IndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlOrderNo.SelectedIndex > 0)
                    BindOrderDetails(null, ddlOrderNo.SelectedValue);
                ddlPartNo.ClearSelection();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region btnUpdate_OnClick
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('hello')", true); 

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<IMPALLibrary.CancelOrderProp> lstOrderDetails = null;
                for (int Count = 0; Count < rOrderDetails.Items.Count; Count++)
                {
                    CheckBox chkSelect = (CheckBox)rOrderDetails.Items[Count].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        if (lstOrderDetails == null)
                            lstOrderDetails = new List<IMPALLibrary.CancelOrderProp>();
                        IMPALLibrary.CancelOrderProp oProp = new IMPALLibrary.CancelOrderProp();
                        HiddenField hidSerialNum = (HiddenField)rOrderDetails.Items[Count].FindControl("hidSerialNum");
                        if (string.IsNullOrEmpty(hidSerialNum.Value))
                            oProp.SerialNum = "0";
                        else
                            oProp.SerialNum = hidSerialNum.Value;
                        Label lblOrderNo = (Label)rOrderDetails.Items[Count].FindControl("lblOrderNo");
                        oProp.OrderNum = lblOrderNo.Text;
                        Label lblOrderDate = (Label)rOrderDetails.Items[Count].FindControl("lblOrderDate");
                        oProp.OrderDate = lblOrderDate.Text;
                        Label lblPartNum = (Label)rOrderDetails.Items[Count].FindControl("lblPartNum");
                        oProp.PartNum = lblPartNum.Text;
                        Label lblPOQty = (Label)rOrderDetails.Items[Count].FindControl("lblPOQty");
                        if (string.IsNullOrEmpty(lblPOQty.Text))
                            oProp.OrderQty = "0";
                        else
                            oProp.OrderQty = lblPOQty.Text;
                        Label lblRecdQty = (Label)rOrderDetails.Items[Count].FindControl("lblRecdQty");
                        if (string.IsNullOrEmpty(lblRecdQty.Text))
                            oProp.ReceivedQty = "0";
                        else
                            oProp.ReceivedQty = lblRecdQty.Text;
                        TextBox txtBalQty = (TextBox)rOrderDetails.Items[Count].FindControl("txtBalQty");
                        if (string.IsNullOrEmpty(txtBalQty.Text))
                            oProp.BalanceQty = "0";
                        else
                            oProp.BalanceQty = txtBalQty.Text;
                        lstOrderDetails.Add(oProp);
                    }
                }
                if (lstOrderDetails != null)
                {
                    IMPALLibrary.Cancellation objCancel = new IMPALLibrary.Cancellation();
                    Int32 intReturnCount = objCancel.UpdateOrderDetails(lstOrderDetails);
                    if (intReturnCount > 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Updated Successfully')", true);
                    //string display = "Updated Successfully";
                    //display = "alert('Updated Successfully');";
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalertsss", display, true);
                }

                #region Bind Repeater
                switch (radType.SelectedValue)
                {
                    case "PartNo":
                        BindOrderDetails(ddlPartNo.SelectedValue, null);
                        break;
                    case "OrderNo":
                        BindOrderDetails(null, ddlOrderNo.SelectedValue);
                        break;
                    case "Month":
                        BindOrderDetails(null, null);
                        break;
                }
                #endregion

                string display = "Updated Successfully";
                display = "alert('Updated Successfully');";
                ClientScript.RegisterStartupScript(this.GetType(), "myalertsss", display, true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion


        #region btnReset_OnClick
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                radType.ClearSelection();
                ddlSupplier.Enabled = true;
                divPartNo.Style.Add("display", "none");
                lblPartNo.Style.Add("display", "none");
                ddlOrderNo.Style.Add("display", "none");
                lblOrderNo.Style.Add("display", "none");
                divOrderDetails.Style.Add("display", "none");
                btnUpdate.Style.Add("display", "none");
                divMessage.Style.Add("display", "none");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
