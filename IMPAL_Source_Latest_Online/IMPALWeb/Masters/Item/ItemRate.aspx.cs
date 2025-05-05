using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALWeb.UserControls;

namespace IMPALWeb
{
    public partial class ItemRate : System.Web.UI.Page
    {
        ItemRateMasters IR = new ItemRateMasters();
        SupplierLines SupplierLines = new SupplierLines();
        Branches Branch = new Branches();
        string strBranchCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BranchCode"] != null)
                strBranchCode = (string)Session["BranchCode"];

            if (!IsPostBack)
            {
                LoadDropDown();
                BindGrid();
            }

            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_GLMaster.ShowFooter = false;
                int EditButtonIndex = GV_GLMaster.Columns.Count - 1;
                GV_GLMaster.Columns[EditButtonIndex].Visible = false;
            }
        }

        private void BindGrid()
        {
            try
            {
                GV_GLMaster.DataSource = IR.GetAllItemRates(drpItemRate.SelectedValue, strBranchCode);
                GV_GLMaster.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadDropDown()
        {
            try
            {
                drpItemRate.DataSource = SupplierLines.GetItemSuppliers();
                drpItemRate.DataTextField = "Name";
                drpItemRate.DataValueField = "Code";
                drpItemRate.DataBind();
                drpItemRate.Items.Insert(0, string.Empty);
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    TextBox ItemCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewItemCode");
                    var drpFooterBranch = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpBranch");
                    TextBox NetAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewNetAmount");
                    TextBox Partno = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewPartNumber");
                    //TextBox AppAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewApprovedAmount");
                    IR.AddNewItemRate(null, ItemCode.Text, Partno.Text, drpFooterBranch.SelectedValue, NetAmount.Text);
                    BindGrid();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddGLGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write("testig");
        }

        #region Report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("ItemRateReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void GV_GLMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_GLMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_GLMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GV_GLMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }

        protected void GV_GLMaster_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                var User_SupplierLine = (ItemCodePartNumber)e.Row.FindControl("user");
                var drpFooterSupplier = (DropDownList)e.Row.FindControl("drpSupplierLine");

                //User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;
                //drpFooterSupplier.DataSource = SupplierLines.GetItemSuppliers();// Item.GetGlBranch();
                //drpFooterSupplier.DataTextField = "Name";
                //drpFooterSupplier.DataValueField = "Code";
                //drpFooterSupplier.DataBind();
                LoadDropDownLists<ItemRateSupplierLine>(SupplierLines.GetItemSuppliers(), drpFooterSupplier, "Code", "Name", true, "");
                User_SupplierLine.SupplierLine = drpFooterSupplier.SelectedValue;
                User_SupplierLine.SupplierDesc = drpFooterSupplier.SelectedItem.Text;
                //**** Branch Dropdown
                var drpFooterBranch = (DropDownList)e.Row.FindControl("drpBranch");
                LoadDropDownLists<Branch>(Branch.GetAllBranch(), drpFooterBranch, "BranchCode", "BranchName", true, "");

                //drpFooterBranch.DataSource =  
                //drpFooterBranch.DataTextField = "Name";
                //drpFooterBranch.DataValueField = "Code";
                //drpFooterBranch.DataBind();


            }
        }
        protected void GV_GLMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_GLMaster.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GV_GLMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GV_GLMaster.Rows[e.RowIndex];
            var Branch = (Label)row.FindControl("lblBranch");
            var ItemCode = (TextBox)row.FindControl("txtEditItemCode");
            var NetAmount = (TextBox)row.FindControl("txtNetAmount");

            //var caCode = (Label)row.FindControl("lblChartOfAccountCode");
            ////Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            //BM.UpdateBudgetMaster(BYear.Text, caCode.Text, BudAmount.Text, AppAmount.Text);
            IR.UpdateItemRate(null, ItemCode.Text, null, Branch.Text, NetAmount.Text);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }

        protected void drpItemRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var User_SupplierLine = (ItemCodePartNumber)GV_GLMaster.FooterRow.FindControl("user");
                var drpSupplierLine = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpSupplierLine");
                User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;
                User_SupplierLine.SupplierDesc = drpSupplierLine.SelectedItem.Text;



            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ucSupplierPartNumber_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {

                var txtItemCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewItemCode");
                var txtNewPartNumber = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewPartNumber");
                txtItemCode.Text = Session["ItemCode"].ToString();
                txtNewPartNumber.Text = Session["SupplierPartNumber"].ToString();

            }
            catch (Exception)
            {

            }

        }

        //protected void UserChart_SearchImageClicked(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        var txtNewChartOfAccountCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewChartOfAccountCode");
        //        txtNewChartOfAccountCode.Text = Session["ChatAccCode"].ToString();

        //    }
        //    catch (Exception exp)
        //    {

        //    }

        //}
        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
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
            catch (Exception)
            {

                throw;
            }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {

        }
    }


}
