using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALWeb.UserControls;
using System.Data;

namespace IMPALWeb
{
    public partial class SalesManTarget : System.Web.UI.Page
    {
        // ItemRateMasters IR = new ItemRateMasters();
        SalesManTargets SMT = new SalesManTargets();
        SalesmanSupplierLines SupplierLines = new SalesmanSupplierLines();
        SalesManMaster SMan = new SalesManMaster();
        CustomerNames Cust = new CustomerNames();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                //GV_GLMaster.DataSource = SMT.GetAllSalesManTarget(); // IR.GetAllItemRates(drpSalesMan.SelectedValue);
                //GV_GLMaster.DataSource = SMT.GetAllSalesManTarget(drpSalesMan1.SelectedValue);
                //BM.GetAllBudgetMasters(drpBY.SelectedValue);
                //GV_GLMaster.DataBind();


                if (SMT.GetAllSalesManTarget(drpSalesMan1.SelectedValue).Count == 0)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add(new DataColumn("SalesManCode", typeof(string)));
                    dt.Columns.Add(new DataColumn("SalesManName", typeof(string)));
                    dt.Columns.Add(new DataColumn("SupplierLinecode", typeof(string)));
                    dt.Columns.Add(new DataColumn("SupplierName", typeof(string)));
                    dt.Columns.Add(new DataColumn("CustomerCode", typeof(string)));
                    dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
                    dt.Columns.Add(new DataColumn("Year", typeof(string)));
                    dt.Columns.Add(new DataColumn("Actual", typeof(string)));
                    dt.Columns.Add(new DataColumn("TargetAmt", typeof(string)));
                    dt.Columns.Add(new DataColumn("Expenses", typeof(string)));

                    DataRow dr = null;

                    dr = dt.NewRow();
                    dr["SalesManCode"] = "";
                    dr["SalesManName"] = "";
                    dr["SupplierLinecode"] = "";
                    dr["SupplierName"] = "";
                    dr["CustomerCode"] = "";
                    dr["CustomerName"] = "";
                    dr["Year"] = "";
                    dr["Actual"] = "";
                    dr["TargetAmt"] = "";
                    dr["Expenses"] = "";

                    dt.Rows.Add(dr);
                    
                    GV_GLMaster.DataSource = dt;
                    GV_GLMaster.DataBind();
                    //GV_GLMaster.AutoGenerateColumns = false;
                    
                    int columnCount = GV_GLMaster.Rows[0].Cells.Count;
                    GV_GLMaster.Rows[0].Cells.Clear();
                    GV_GLMaster.Rows[0].Cells.Add(new TableCell());
                    GV_GLMaster.Rows[0].Cells[0].ColumnSpan = columnCount;
                    //GV_GLMaster.Rows[0].Cells[0].Text = "No Records Found.";
                    Session["DefaultRow"] = "True";
                }
                else
                {
                    Session["DefaultRow"] = null;
                    GV_GLMaster.DataSource = SMT.GetAllSalesManTarget(drpSalesMan1.SelectedValue);                    
                    GV_GLMaster.DataBind();
                    //GV_GLMaster.AutoGenerateColumns = true;
                   
                }




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
                //drpSalesMan1.DataSource = SupplierLines.GetItemSuppliers();
                //drpSalesMan1.DataTextField = "Name";
                //drpSalesMan1.DataValueField = "Code";
                //drpSalesMan1.DataBind();
                //drpSalesMan1.Items.Insert(0, "ALL");

                drpSalesMan1.DataSource = SMan.GetAllSalesMan(Session["BranchCode"].ToString());
                drpSalesMan1.DataTextField = "SalesManName";
                drpSalesMan1.DataValueField = "SalesManCode";
                drpSalesMan1.DataBind();
                drpSalesMan1.Items.Insert(0, "ALL");

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
                    var Ccode = (Label)GV_GLMaster.FooterRow.FindControl("lblNewCustomerCode");
                    var SCode = (Label)GV_GLMaster.FooterRow.FindControl("lblNewSalesManCode");
                    var SuppCode = (Label)GV_GLMaster.FooterRow.FindControl("lblNewSupplierLine");
                    TextBox Year = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewYear");
                    var Target = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewTagetAmt");
                    var Actual = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewActualAmt");
                    var Expenses = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewExpenses");
                    //TextBox ItemCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewItemCode");
                    //var drpFooterBranch = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpBranch");
                    //TextBox NetAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewNetAmount");
                    //TextBox Partno = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewPartNumber");
                    //TextBox AppAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewApprovedAmount");
                    //IR.AddNewItemRate(null, ItemCode.Text, Partno.Text, drpFooterBranch.SelectedValue, NetAmount.Text);
                    SMT.AddNewSalesManTarget(SCode.Text, SuppCode.Text, Ccode.Text, Year.Text, Target.Text, Actual.Text, Expenses.Text);
                    BindGrid();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


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
                //   var User_SupplierLine = (ItemCodePartNumber)e.Row.FindControl("user");
                var drpFooterSupplier = (DropDownList)e.Row.FindControl("drpSupplierLine");

                // User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;

                drpFooterSupplier.DataSource = SupplierLines.GetItemSuppliers();// Item.GetGlBranch();
                drpFooterSupplier.DataTextField = "Name";
                drpFooterSupplier.DataValueField = "Code";
                drpFooterSupplier.DataBind();

                // User_SupplierLine.SupplierLine = drpFooterSupplier.SelectedValue;

                //**** SalesMan
                var drpFooterSalesMan = (DropDownList)e.Row.FindControl("drpSalesMan");
                LoadDropDownLists<SalesMans>(SMan.GetAllSalesMan(Session["BranchCode"].ToString()), drpFooterSalesMan, "SalesManCode", "SalesManName", true, "");

                var drpFooterCustomer = (DropDownList)e.Row.FindControl("drpCustomer");
                drpFooterCustomer.DataSource = Cust.GetCustomerList(Session["BranchCode"].ToString());  // Item.GetGlBranch();
                drpFooterCustomer.DataTextField = "CustomerName";
                drpFooterCustomer.DataValueField = "CustomerCode";
                drpFooterCustomer.DataBind();
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["DefaultRow"] != null)
                {
                    e.Row.Visible = false;
                }
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

            var Target = (TextBox)row.FindControl("txtTargetAmt");

            var Ccode = (Label)row.FindControl("lblCustomerCode");
            var SCode = (Label)row.FindControl("lblSalesManCode");
            var SuppCode = (Label)row.FindControl("lblSupplierLine");
            var Year = (Label)row.FindControl("lblYear");
            SMT.UpdateSalesManTarget(SCode.Text, SuppCode.Text, Ccode.Text, Year.Text, Target.Text, null, null);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }

        protected void drpSalesMan1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var drpSupplierLine = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpSupplierLine");
                var lblNewSupplierLine = (Label)GV_GLMaster.FooterRow.FindControl("lblNewSupplierLine");
                lblNewSupplierLine.Text = drpSupplierLine.SelectedValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void drpSalesMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //var User_SupplierLine = (ItemCodePartNumber)GV_GLMaster.FooterRow.FindControl("user");
                var lblNewSalesManCode = (Label)GV_GLMaster.FooterRow.FindControl("lblNewSalesManCode");
                DropDownList drpSalesMan = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpSalesMan");
                lblNewSalesManCode.Text = drpSalesMan.SelectedValue;
                //User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;
                //DropDownList CustCode = (DropDownList)GV_SLBGroup.FooterRow.FindControl("ddlCustomerName")
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //var User_SupplierLine = (ItemCodePartNumber)GV_GLMaster.FooterRow.FindControl("user");
                //User_SupplierLine.SupplierLine = drpSupplierLine.SelectedValue;
                var drpCustomer = (DropDownList)GV_GLMaster.FooterRow.FindControl("drpCustomer");
                var lblNewCustomerCode = (Label)GV_GLMaster.FooterRow.FindControl("lblNewCustomerCode");
                lblNewCustomerCode.Text = drpCustomer.SelectedValue;
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
                BindGrid();
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Server.Execute("SalesManTargetReport.aspx");
            //string SalesManCode = drpSalesMan1.SelectedValue;
            //Response.Redirect("SalesManTargetReport.aspx?Code=" + SalesManCode);
        }
    }


}
