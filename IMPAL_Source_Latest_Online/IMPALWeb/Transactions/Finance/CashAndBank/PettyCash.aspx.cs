using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;
using IMPALWeb.UserControls;

namespace IMPALWeb.Finance
{
    public partial class Fin_PettyCash : System.Web.UI.Page
    {
        private string strBranchCode;

        #region Event Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {                    
                    txtBranch.Text = fnGetBranch(strBranchCode);
                    txtBranch.Enabled = false;
                    txtPettyCashDate.Enabled = false;
                    ddlPettyCashNumber.Visible = false;
                    txtPettyCashNumber.Visible = true;
                    BtnSubmit.Visible = false;
                    lblNoOfTransactions.Visible = true;
                    txtNoOfTransactions.Visible = true;
                    txtPettyCashDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    grdPettyTransaction.Visible = false;
                    ddlPettyCashNumber.Enabled = true;
                }

                if (grdPettyTransaction.Visible == false)
                {
                    BtnSubmit.Visible = false;
                    btnTransactionDetails.Visible = true;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    btnTransactionDetails.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string fnGetBranch(string strBranchCode)
        {
            PettyCashTransactions pettycashTransactions = new PettyCashTransactions();
            try
            {
                return (string)Session["BranchName"];
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankPayment), exp);
            }

            return (string)Session["BranchName"];
        }

        private void fnPopulatePettyCashNumber(string strBranchCode)
        {
            PettyCashTransactions pettycashTransactions = new PettyCashTransactions();

            try
            {
                ddlPettyCashNumber.DataSource = pettycashTransactions.GetPettyCahshNumber(strBranchCode);
                ddlPettyCashNumber.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(CashAndBankPayment), exp);
            }
        }

        private void DisplayGridColumns()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSubmit.Visible = false;
                btnTransactionDetails.Visible = false;
                txtPettyCashNumber.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtDescription_Name.Enabled = false;
                grdPettyTransaction.Visible = true;
                //btnReport.Enabled = false;
                btnReport.Visible = true;

                if (ddlPettyCashNumber.SelectedValue == "0")
                {
                    txtPettyCashNumber.Text = "";
                    txtNoOfTransactions.Text = "";
                    txtTransactionAmount.Text = "";
                    txtDescription_Name.Text = "";
                    //btnReport.Enabled = false;
                    btnReport.Visible = false;
                    FirstGridViewRow();
                }

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
                txtPettyCashNumber.Visible = false;
                ddlPettyCashNumber.Visible = true;
                BtnSubmit.Enabled = false;
                imgEditToggle.Visible = false;
                txtDescription_Name.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                grdPettyTransaction.Visible = false;
                btnTransactionDetails.Visible = false;
                btnReport.Visible = true;
                fnPopulatePettyCashNumber(strBranchCode);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlPettyCashNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PettyCashTransactions pettycashTransactions = new PettyCashTransactions();
                PettyCashEntity pettycashEntity = pettycashTransactions.GetPettyCashDetails(ddlPettyCashNumber.SelectedValue, Session["BranchCode"].ToString());

                txtPettyCashNumber.Text = ddlPettyCashNumber.SelectedValue.ToString();
                txtPettyCashDate.Text = pettycashEntity.PettyCashDate;
                txtTransactionAmount.Text = string.Format("{0:0.00}", Convert.ToDecimal(pettycashEntity.TotalAmount));

                if (pettycashEntity.Items == null)
                    txtNoOfTransactions.Text = "";
                else
                    txtNoOfTransactions.Text = pettycashEntity.Items.Count().ToString();

                txtDescription_Name.Text = pettycashEntity.DescriptionName;

                grdPettyTransaction.DataSource = (object)pettycashEntity.Items;
                grdPettyTransaction.DataBind();
                DisplayGridColumns();

                foreach (GridViewRow gr in grdPettyTransaction.Rows)
                {
                    gr.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }
        }

        protected void btnTransactionDetails_Click(object sender, EventArgs e)
        {
            try
            {
                // btnTransactionDetails.Enabled = false;
                btnTransactionDetails.Visible = false;
                ImgNoOfTransaction.Visible = false;
                lblNoOfTransactions.Visible = false;
                txtNoOfTransactions.Visible = false;

                txtDescription_Name.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                txtTransactionAmount.Enabled = false;
                btnReport.Visible = false;
                grdPettyTransaction.Visible = true;

                //BtnSubmit.Enabled = true;
                BtnSubmit.Visible = true;
                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            PettyCashEntity pettycashEntity = new PettyCashEntity();
            pettycashEntity.Items = new List<PettyCashItem>();

            pettycashEntity.PettyCashNumber = txtPettyCashNumber.Text;
            pettycashEntity.PettyCashDate = txtPettyCashDate.Text;
            pettycashEntity.BranchCode = Session["BranchCode"].ToString();
            pettycashEntity.DescriptionName = txtDescription_Name.Text;
            pettycashEntity.TotalAmount = txtTransactionAmount.Text == "" ? "0.00" : txtTransactionAmount.Text;

            PettyCashItem pettycashItem = null;
            int intCount = 0;
            int intVisible = 0;
            foreach (GridViewRow grvRow in grdPettyTransaction.Rows)
            {
                pettycashItem = new PettyCashItem();

                if (!string.IsNullOrEmpty(((TextBox)grvRow.Cells[1].FindControl("txtChartOfAccount")).Text))
                {
                    intCount += 1;
                    pettycashItem.Serial_Number = intCount.ToString();
                    pettycashItem.Chart_of_Account_Code = ((TextBox)grvRow.Cells[1].FindControl("txtChartOfAccount")).Text;
                    pettycashItem.Amount = ((TextBox)grvRow.Cells[2].FindControl("txtAmount")).Text == "" ? "0.00" : ((TextBox)grvRow.Cells[2].FindControl("txtAmount")).Text;
                    pettycashItem.Remarks = ((TextBox)grvRow.Cells[3].FindControl("txtRemarks")).Text;

                    pettycashEntity.Items.Add(pettycashItem);
                }
                else
                    grdPettyTransaction.Rows[intVisible].Visible = false;

                intVisible += 1;
            }

            PettyCashTransactions pettycashTransactions = new PettyCashTransactions();
            int result = pettycashTransactions.AddPettyCashDetails(ref pettycashEntity);

            if ((pettycashEntity.ErrorMsg == string.Empty) && (pettycashEntity.ErrorCode == "0"))
            {
                txtPettyCashNumber.Text = pettycashEntity.PettyCashNumber;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Petty Cash details are successfully inserted');", true);

                //BtnSubmit.Enabled = false;
                BtnSubmit.Visible = false;
                btnTransactionDetails.Visible = false;
                txtPettyCashNumber.Enabled = false;
                txtNoOfTransactions.Enabled = false;
                txtTransactionAmount.Enabled = false;
                txtDescription_Name.Enabled = false;
                grdPettyTransaction.Enabled = false;
                ddlPettyCashNumber.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + pettycashEntity.ErrorMsg + "');", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("PettyCash.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.WriteException(Source, ex);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                Server.Execute("PettyCashPageReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }

        }

        protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                TextBox txtgrdChartOfAccount = (TextBox)gvr.FindControl("txtChartOfAccount");
                bool IsAlready = false;

                if (grdPettyTransaction.Rows.Count >= 1)
                {
                    for (int i = 0; i < grdPettyTransaction.Rows.Count; i++)
                    {
                        var txt_ChartAcc = (TextBox)grdPettyTransaction.Rows[i].FindControl("txtChartOfAccount");

                        if (txt_ChartAcc.Text == Session["ChatAccCode"].ToString())
                        {
                            txtgrdChartOfAccount.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Chart of Account should not be same');", true);
                            IsAlready = true;
                            break;
                        }
                    }
                }
                if (!IsAlready)
                {
                    txtgrdChartOfAccount.Text = Session["ChatAccCode"].ToString();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }

        }

        protected void grdPettyTransaction_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                // grdPettyTransaction.PageIndex = e.NewSelectedIndex;
                // BindValue("False");
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }

        }


        protected void grdPettyTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var txtS_No = (TextBox)e.Row.FindControl("txtS_No");
                    var txtChartOfAccount = (TextBox)e.Row.FindControl("txtChartOfAccount");
                    var txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    var txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                    if (ddlPettyCashNumber.SelectedIndex.ToString() != "0")
                    {
                        txtS_No.Enabled = false;
                        txtChartOfAccount.Enabled = false;
                        txtAmount.Enabled = false;
                        txtRemarks.Enabled = false;
                    }
                    else
                    {
                        txtS_No.Enabled = false;
                        txtChartOfAccount.Enabled = false;
                        txtAmount.Enabled = true;
                        txtRemarks.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Fin_PettyCash), exp);
            }
        }

        #endregion

        #region User Methods
        private void FirstGridViewRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("S_No", typeof(int)));
                dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

                DataRow dr = null;

                if (txtNoOfTransactions.Text.Trim() != "")
                {
                    for (int i = 0; i < Convert.ToInt32(txtNoOfTransactions.Text.Trim()); i++)
                    {
                        dr = dt.NewRow();
                        // dr["S_No"] = 0;
                        dr["Chart_of_Account_Code"] = string.Empty;
                        dr["Amount"] = string.Empty;
                        dr["Remarks"] = string.Empty;
                        dt.Rows.Add(dr);
                    }
                }
                grdPettyTransaction.DataSource = dt;
                grdPettyTransaction.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        private void AddSelect(DropDownList ddl)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        #endregion


    }

}
