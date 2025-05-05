using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALWeb.UserControls;


namespace IMPALWeb.Masters.Bank
{
    public partial class Branch : System.Web.UI.Page
    {
        //GLMasters Glmaster = new GLMasters();
        //GLGroups GlGroup = new GLGroups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_Branch.ShowFooter = false;
                int EditButtonIndex = GV_Branch.Columns.Count - 1;
                GV_Branch.Columns[EditButtonIndex].Visible = false;
            }

            if (!IsPostBack)
            {
                fnPopulateBank();
                fnBindGrid();
            }
        }

        protected void fnBindGrid()
        {
            Banks objBank = new Banks();
            try
            {
                GV_Branch.DataSource = objBank.GetBankBranchDetails(ddlBank.SelectedValue);
                GV_Branch.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objBank = null;
            }
        }

        protected void fnPopulateBank(DropDownList ddlDropDown)
        {
            Banks objBank = new Banks();
            try
            {
                ddlDropDown.DataSource = objBank.GetAllBanks();
                ddlDropDown.DataTextField = "BankName";
                ddlDropDown.DataValueField = "BankCode";
                ddlDropDown.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objBank = null;
            }
        }

        protected void fnPopulateBank()
        {
            Banks objBank = new Banks();
            try
            {
                ddlBank.DataSource = objBank.GetAllBanks();
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankCode";
                ddlBank.DataBind();
                //ddlBank.Items.Insert(0, "ALL");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objBank = null;
            }


        }


        protected void GV_Branch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Banks objBank = new Banks();
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                objBank.BankCode = ((DropDownList)GV_Branch.FooterRow.FindControl("ddlBankName")).SelectedValue;
                objBank.BranchName = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewName")).Text;
                objBank.AccountNumber = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewAccNo")).Text;

                objBank.Address = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewAddress")).Text;
                objBank.Phone = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewPhone")).Text;
                objBank.Fax = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewFax")).Text;

                objBank.ContactPerson = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewContactPerson")).Text;
                objBank.ChartOfAccountCode = ((TextBox)GV_Branch.FooterRow.FindControl("txtNewChartOfAccount")).Text;

                objBank.AddBankBranch();

                fnBindGrid();
            }
        }


        protected void GV_Branch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Branch.PageIndex = e.NewPageIndex;
            fnBindGrid();
        }
        protected void GV_Branch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_Branch.EditIndex = -1;
            fnBindGrid();
        }
        protected void GV_Branch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var ddBankName = (DropDownList)e.Row.FindControl("ddlBankName");
                fnPopulateBank(ddBankName);
            }

        }
        protected void GV_Branch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_Branch.EditIndex = e.NewEditIndex;
            fnBindGrid();
        }
        protected void GV_Branch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Banks objBank = new Banks();
            GridViewRow row = (GridViewRow)GV_Branch.Rows[e.RowIndex];
            objBank.BankCode = "0";
            objBank.BankBranchCode = ((Label)row.FindControl("lblCode")).Text;
            objBank.BranchName = ((TextBox)row.FindControl("txtName")).Text;
            objBank.AccountNumber = ((TextBox)row.FindControl("txtAccNo")).Text;
            objBank.Address = ((TextBox)row.FindControl("txtAddress")).Text;
            objBank.Phone = ((TextBox)row.FindControl("txtPhone")).Text;
            objBank.Fax = ((TextBox)row.FindControl("txtFax")).Text;
            objBank.ContactPerson = ((TextBox)row.FindControl("txtContactPerson")).Text;
            objBank.ChartOfAccountCode = ((TextBox)row.FindControl("txtChartOfAccount")).Text;

            objBank.UpdateBankBranch();

            GV_Branch.EditIndex = -1;
            fnBindGrid();
        }
        protected void drpGlGroupDesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindGrid();
        }

        protected void CABranch_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;                        
            try
            {
                var txtNewChartOfAccount = (TextBox)GV_Branch.FooterRow.FindControl("txtNewChartOfAccount");
                txtNewChartOfAccount.Text = Session["ChatAccCode"].ToString();
                Session["ChatAccCode"] = "";

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void CAEditBranch_SearchImageClicked(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;            
            try
            {   GridViewRow gvr = (GridViewRow)((ChartAccount)sender).Parent.Parent;
                var txtChartOfAccount = (TextBox)gvr.FindControl("txtChartOfAccount");
                txtChartOfAccount.Text = Session["ChatAccCode"].ToString();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.Execute("BranchReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
