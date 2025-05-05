using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Data;
using System.Data.Common;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


namespace IMPALWeb.Masters.SLB
{
    public partial class SLBDetails : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        protected void Page_Load(object sender, EventArgs e)
        {
            strBranchCode = Session["BranchCode"].ToString();
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_SBLDetail.ShowFooter = false;
                int EditButtonIndex = GV_SBLDetail.Columns.Count - 1;
                GV_SBLDetail.Columns[EditButtonIndex].Visible = false;
            }
            if (!IsPostBack)
            {
                fnPopulateSLB(ddlSLB);
                fnPopulateBranch(ddlBranch);
                if (strBranchCode != "CRP")
                {
                    ddlBranch.SelectedValue = strBranchCode;
                    ddlBranch.Enabled = false;
                }
                else
                    ddlBranch.Enabled = true;

                fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
            }

        }

        protected void fnPopulatePurchaseDiscount(DropDownList ddlDropDown, string strItemCode)
        {
            SLBs objSLB = new SLBs();
            try
            {
                ddlDropDown.DataSource = objSLB.GetPurchaseDiscount(strItemCode);
                ddlDropDown.DataTextField = "Purchase_Discount";
                ddlDropDown.DataValueField = "Purchase_Discount";
                ddlDropDown.DataBind();
                ddlDropDown.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected void LoadDataControl(string strPartNumber, bool strReset)
        {
            try
            {
                fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, strPartNumber);
      
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string strPartNumber;
            try
            {
                strPartNumber = txtSupplierPartNo.Text.Trim();
                LoadDataControl(strPartNumber, false);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

        }

        protected void fnPopulateSLB(DropDownList ddlDropDown)
        {
            SLBs objSLB = new SLBs();
            try
            {
                ddlDropDown.DataSource = objSLB.GetSLBDetails(strBranchCode);
                ddlDropDown.DataTextField = "Description";
                ddlDropDown.DataValueField = "SLBCode";
                ddlDropDown.DataBind();
                ddlDropDown.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected void fnPopulateBranch(DropDownList ddlDropDown)
        {
            Branches objBranches = new Branches();
            try
            {
                ddlDropDown.DataSource = objBranches.GetAllLinewiseBranches(string.Empty, "OnlyBranch", Session["BranchCode"].ToString());
                ddlDropDown.DataTextField = "BranchName";
                ddlDropDown.DataValueField = "BranchCode";
                ddlDropDown.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objBranches = null;
            }
        }

        protected void fnBindGrid(string strBranch, string strSLB, string SupPartNo)
        {            
            SLBs objSLB = new SLBs();
            try
            {
                GV_SBLDetail.DataSource = objSLB.GetSLB(strBranch, strSLB, SupPartNo);
                GV_SBLDetail.DataBind();

            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected void ddlSLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
        }

        protected void GV_SBLDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SLBs objSLB = new SLBs();
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                objSLB.SLBCode = ((DropDownList)GV_SBLDetail.FooterRow.FindControl("ddlNewSLB")).SelectedValue;
                objSLB.ItemCode = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtItemCode")).Text;
                objSLB.BranchCode = ((DropDownList)GV_SBLDetail.FooterRow.FindControl("ddlNewBranch")).SelectedValue;

                objSLB.NewFDOValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddNewFDO")).Text;
                objSLB.NewLRValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddNewLRTransfer")).Text;
                objSLB.NewOSValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddNewOS")).Text;
                objSLB.NewLSValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddNewLS")).Text;

                objSLB.OldFDOValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddOldFDO")).Text;
                objSLB.OldLRValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddOldLRTransfer")).Text;
                objSLB.OldOSValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddOldOS")).Text;
                objSLB.OldLSValue = ((TextBox)GV_SBLDetail.FooterRow.FindControl("txtAddNewLS")).Text;

                objSLB.InsertSLBDetails();

                fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
            }
        }

        protected void GV_SBLDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SBLDetail.PageIndex = e.NewPageIndex;
            fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);

        }
        protected void GV_SBLDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_SBLDetail.EditIndex = -1;
            fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
        }
        protected void GV_SBLDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var ddNewSLB = (DropDownList)e.Row.FindControl("ddlNewSLB");
                fnPopulateSLB(ddNewSLB);

                var ddNewBranch = (DropDownList)e.Row.FindControl("ddlNewBranch");
                fnPopulateBranch(ddNewBranch);
            }

        }

        protected void ItemPopUp_ImageClicked(object sender, EventArgs e)
        {
            try
            {
                var txtNewItemCode = (TextBox)GV_SBLDetail.FooterRow.FindControl("txtItemCode");
                txtNewItemCode.Text = Session["ItemCode"].ToString();
                Session["SupplierPartNumber"] = "";
                Session["ItemCode"] = "";
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void GV_SBLDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_SBLDetail.EditIndex = e.NewEditIndex;
            fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);

        }
        protected void GV_SBLDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SLBs objSLB = new SLBs();
            GridViewRow row = (GridViewRow)GV_SBLDetail.Rows[e.RowIndex];
            try
            {

                objSLB.SLBCode = ((Label)row.FindControl("lblSLBCode")).Text;
                objSLB.ItemCode = ((Label)row.FindControl("lblItemCode")).Text;
                objSLB.BranchCode = ((Label)row.FindControl("lblBranchCode")).Text;

                objSLB.NewFDOValue = ((TextBox)row.FindControl("txtNewFDO")).Text;
                objSLB.NewLRValue = ((TextBox)row.FindControl("txtNewLRTransfer")).Text;
                objSLB.NewOSValue = ((TextBox)row.FindControl("txtNewOS")).Text;
                objSLB.NewLSValue = ((TextBox)row.FindControl("txtNewLS")).Text;

                objSLB.OldFDOValue = ((TextBox)row.FindControl("txtOldFDO")).Text;
                objSLB.OldLRValue = ((TextBox)row.FindControl("txtOldLRTransfer")).Text;
                objSLB.OldOSValue = ((TextBox)row.FindControl("txtOldOS")).Text;
                objSLB.OldLSValue = ((TextBox)row.FindControl("txtOldLS")).Text;

                objSLB.PurchaseDiscount = string.Empty;
                objSLB.Status = string.Empty;

                objSLB.UpdateSLBDetails();

                GV_SBLDetail.EditIndex = -1;
                fnBindGrid(ddlBranch.SelectedValue, ddlSLB.SelectedValue, txtSupplierPartNo.Text);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            finally
            {
                objSLB = null;
                row = null;
            }

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                //string strDdlSLB = ddlSLB.SelectedValue.ToString();
                //string strDdlBracnh = ddlBranch.SelectedValue.ToString();
                Server.Execute("SLBDetailsReport.aspx");
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
                Response.Redirect("SLBDetails.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
