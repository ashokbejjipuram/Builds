using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Data;

namespace IMPALWeb
{
    public partial class BudgetMaster : System.Web.UI.Page
    {
        BudgetMasters BM = new BudgetMasters();
        BudgetYears BudgetYear = new BudgetYears();
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
                //GV_GLMaster.DataSource = BM.GetAllBudgetMasters(drpBY.SelectedValue);
                //BM.GetAllBudgetMasters(drpBY.SelectedValue);
                //GV_GLMaster.DataBind();


                if (BM.GetAllBudgetMasters(drpBY.SelectedValue).Count == 0)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add(new DataColumn("Budget_Year", typeof(string)));
                    dt.Columns.Add(new DataColumn("Chart_of_Account_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("Budget_Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("Approved_Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("Actual_Amount", typeof(string)));

                    DataRow dr = null;

                    dr = dt.NewRow();
                    dr["Budget_Year"] = "";
                    dr["Chart_of_Account_Code"] = "";
                    dr["Budget_Amount"] = "";
                    dr["Approved_Amount"] = "";
                    dr["Actual_Amount"] = "";
                    dt.Rows.Add(dr);

                    GV_GLMaster.DataSource = dt;
                    GV_GLMaster.DataBind();
                    //GV_GLMaster.AutoGenerateColumns = false;

                    int columnCount = GV_GLMaster.Rows[0].Cells.Count;
                    GV_GLMaster.Rows[0].Cells.Clear();
                    GV_GLMaster.Rows[0].Cells.Add(new TableCell());
                    GV_GLMaster.Rows[0].Cells[0].ColumnSpan = columnCount;
                    //GV_GLMaster.Rows[0].Cells[0].Text = "No Records Found.";
                    Session["DefaultRowBudget"] = "True";
                }
                else
                {
                    Session["DefaultRowBudget"] = null;
                    GV_GLMaster.DataSource = BM.GetAllBudgetMasters(drpBY.SelectedValue);
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
                drpBY.DataSource = BudgetYear.GetBudgetYears();
                drpBY.DataTextField = "Budgetyear";
                drpBY.DataValueField = "Budgetyear";
                drpBY.DataBind();
                drpBY.Items.Insert(0, "ALL");
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void GV_GLMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Insert" && Page.IsValid)
            {
                TextBox BYear = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewBudgetYear");
                TextBox CaCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewChartOfAccountCode");
                TextBox BudAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewBudgetAmount");
                TextBox AppAmount = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewApprovedAmount");
                BM.AddNewBudgetMaster(BYear.Text, CaCode.Text, BudAmount.Text, AppAmount.Text);
                BindGrid();
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
                Server.Execute("BudgetMasterReport.aspx");
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
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    var ddGLGroup = (DropDownList)e.Row.FindControl("drpBY");
            //    ddGLGroup.DataSource = BudgetYear.GetBudgetYears();
            //    ddGLGroup.DataTextField = "Budget_Year";
            //    ddGLGroup.DataValueField = "Budget_Year";
            //    ddGLGroup.DataBind();
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["DefaultRowBudget"] != null)
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
            var BYear = (Label)row.FindControl("lblBudgetYear");
            var caCode = (Label)row.FindControl("lblChartOfAccountCode");
            var BudAmount = (TextBox)row.FindControl("txtBudgetAmount");
            var AppAmount = (TextBox)row.FindControl("txtApprovedAmount");

            //Glmaster.UpdateGLMaster(lblGLMasterCode.Text, null, txtDescription.Text, null);
            BM.UpdateBudgetMaster(BYear.Text, caCode.Text, BudAmount.Text, AppAmount.Text);
            GV_GLMaster.EditIndex = -1;
            BindGrid();
        }
        protected void drpBY_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }



        protected void UserChart_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {

                var txtNewChartOfAccountCode = (TextBox)GV_GLMaster.FooterRow.FindControl("txtNewChartOfAccountCode");
                txtNewChartOfAccountCode.Text = Session["ChatAccCode"].ToString();

            }
            catch (Exception)
            {

            }

        }
    }


}
