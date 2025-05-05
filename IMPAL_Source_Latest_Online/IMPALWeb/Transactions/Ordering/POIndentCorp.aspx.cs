using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IMPALLibrary;
using log4net;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions;

namespace IMPALWeb.Ordering
{
    public partial class POIndentCorp : System.Web.UI.Page
    {
        string strOrdBranchCode;
        string strLineCode;
        string strPONumber;
        private string Available_Balance = string.Empty;
        string Branch_PO_Value;
        private string Branch_Amount = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCorp), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ViewState["strOrdBranchCode"] != null)
                    strOrdBranchCode = ViewState["strOrdBranchCode"].ToString();

                if (ViewState["strPONumber"] != null)
                    strPONumber = ViewState["strPONumber"].ToString();

                if (ViewState["strLineCode"] != null)
                    strLineCode = ViewState["strLineCode"].ToString();

                strOrdBranchCode = ddlBrCodeList.SelectedValue; //Session["BranchCode"].ToString();
                ViewState["strOrdBranchCode"] = ddlBrCodeList.SelectedValue; //strOrdBranchCode.ToString();
                ddlBrCodeList.Visible = true;

                if (!IsPostBack)
                {
                    //BtnSubmit.Enabled = false;
                    InitializeControl();
                    BindOrdSupplier();
                    BtnSubmit.Attributes.Add("onclick", "return ConfirmSub();");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBrCodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strOrdBranchCode = ddlBrCodeList.SelectedValue;
                ViewState["strOrdBranchCode"] = strOrdBranchCode.ToString();
                BindOrdSupplier();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlLineCodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strLineCode = ddlLineCodeList.SelectedValue;
                ViewState["strLineCode"] = strLineCode.ToString();
                BindWorkSheetList();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlWorkSheetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strPONumber = ddlWorkSheetList.SelectedValue;
                ViewState["strPONumber"] = strPONumber.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void MGTorCLT()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DirectPurchaseOrders ccWHupdate = new DirectPurchaseOrders();
                if (strPONumber.Substring(14, 3) == "MGT" || strPONumber.Substring(14, 3) == "CLT" || strPONumber.Substring(14, 3) == "EKM" || strPONumber.Substring(14, 3) == "PAT" || strPONumber.Substring(14, 3) == "GUW")
                {
                    ccWHupdate.UpdCCWH_PO_Number(ddlBrCodeList.SelectedValue, strPONumber.ToString());

                    Server.ClearError();
                    string message = "Process done!";
                    string url = "POIndentCorp.aspx";
                    string script = "{ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                    //Response.Write("<script>alert('Process Done')</script>"); 
                    //Response.Redirect("POIndentCorp.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void MGTorCLTNotEq()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DirectPurchaseOrders ccWHupdate = new DirectPurchaseOrders();
                if (strPONumber.Substring(14, 3) != "MGT" || strPONumber.Substring(14, 3) != "CLT" || strPONumber.Substring(14, 3) != "EKM" || strPONumber.Substring(14, 3) != "PAT" || strPONumber.Substring(14, 3) != "GUW")
                {
                    if (strPONumber.Substring(17, 3) == "461")
                    {
                        ccWHupdate.UpdCCWH_PO_Number(ddlBrCodeList.SelectedValue, strPONumber.ToString());

                        Server.ClearError();
                        string message = "Process done!";
                        string url = "POIndentCorp.aspx";
                        string script = "{ alert('";
                        script += message;
                        script += "');";
                        script += "window.location = '";
                        script += url;
                        script += "'; }";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                        //Server.ClearError();
                        //Response.Redirect("POIndentCorp.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
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
                string confirmValue = confirm_value.Value.ToString();

                if (confirmValue == "Yes")
                {
                    MGTorCLT();
                    MGTorCLTNotEq();

                    Available_Balance = ViewState["Available_Balance"].ToString();
                    Branch_Amount = ViewState["Branch_Amount"].ToString();
                    Response.Redirect("POIndentCorpDummy.aspx?StrPONumber=" + strPONumber.ToString() + "&StrBranchCode=" + strOrdBranchCode.ToString() + "&Available_Balance=" + Available_Balance + "&Branch_Amount=" + Branch_Amount, false);
                }
                else
                {
                    Server.ClearError();
                    Response.Redirect("POIndentCorp.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("POIndentCorp.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void InitializeControl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DirectPurchaseOrders supplier = new DirectPurchaseOrders();
                ddlBrCodeList.DataSource = supplier.GetBranches();
                ddlBrCodeList.DataTextField = "BranchName";
                ddlBrCodeList.DataValueField = "BranchCode";
                ddlBrCodeList.DataBind();
                ddlBrCodeList.SelectedValue = strOrdBranchCode;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindOrdSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers supplier = new Suppliers();
                ddlLineCodeList.DataSource = supplier.GetAllLinewiseSuppliers(strOrdBranchCode.ToString());
                ddlLineCodeList.DataTextField = "SupplierName";
                ddlLineCodeList.DataValueField = "SupplierName";
                ddlLineCodeList.DataBind();
                if (ddlLineCodeList.Items.Count > 0)
                {
                    ddlLineCodeList.SelectedIndex = 0;
                    strLineCode = ddlLineCodeList.SelectedValue;
                    ViewState["strLineCode"] = strLineCode.ToString();
                }

                BindWorkSheetList();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindWorkSheetList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DirectPurchaseOrders PONo = new DirectPurchaseOrders();
                ddlWorkSheetList.DataSource = PONo.GetIndentPO_Number(strOrdBranchCode.ToString(), txtFromDt.Text.ToString(), txtToDt.Text.ToString(), strLineCode);
                ddlWorkSheetList.DataTextField = "PO_Number";
                ddlWorkSheetList.DataValueField = "PO_Number";
                ddlWorkSheetList.DataBind();
                ddlWorkSheetList.ClearSelection();

                if (ddlWorkSheetList.Items.Count > 0)
                {
                    ddlWorkSheetList.SelectedIndex = 0;
                    strPONumber = ddlWorkSheetList.SelectedValue;
                    ViewState["strPONumber"] = strPONumber.ToString();
                }

                if (strPONumber != null)
                {
                    // string Branch_PO_Value = string.Empty;
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPos_Amount");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strOrdBranchCode);
                    ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                    ImpalDB.AddInParameter(cmd, "@Avail_Balance", DbType.Currency, 0);
                    ImpalDB.AddInParameter(cmd, "@po_value", DbType.Currency, 0);
                    ImpalDB.AddInParameter(cmd, "@Branch_Amount", DbType.Currency, 0);
                    ImpalDB.AddInParameter(cmd, "@Line_code", DbType.String, strPONumber.Substring(10, 3));
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            ViewState["Available_Balance"] = reader[0].ToString();
                            ViewState["Branch_PO_Value"] = reader[1].ToString();
                            ViewState["Branch_Amount"] = reader[2].ToString();
                        }
                        Available_Balance = ViewState["Available_Balance"].ToString();
                        Branch_PO_Value = ViewState["Branch_PO_Value"].ToString();
                        hdnAvailableBalance.Value = Available_Balance;
                        hdnBranchPOValue.Value = Branch_PO_Value;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtFromDt_TextChanged(object sender, EventArgs e)
        {
            BindWorkSheetList();
            strPONumber = ddlWorkSheetList.SelectedValue;
        }

        protected void txtToDt_TextChanged(object sender, EventArgs e)
        {
            BindWorkSheetList();
        }
    }
}