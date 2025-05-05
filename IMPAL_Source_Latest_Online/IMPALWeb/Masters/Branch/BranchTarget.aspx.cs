using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace IMPALWeb
{
    public partial class BranchTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            try
            {
                if ((string)Session["RoleCode"] == "BEDP")
                {
                    gv_Target.ShowFooter = false;
                    int EditButtonIndex = gv_Target.Columns.Count - 1;
                    gv_Target.Columns[EditButtonIndex].Visible = false;
                }
                if (!IsPostBack)
                {
                    fnBindGrid();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        protected void fnBindGrid()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            IMPALLibrary.BranchTargets objTarget = new IMPALLibrary.BranchTargets();
            try
            {
               
                gv_Target.DataSource = objTarget.GetAllBranchTarget();
                gv_Target.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                objTarget = null;
            }
        }

                    
        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("BranchTargetReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void gv_Target_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            IMPALLibrary.BranchTargets objTarget = new IMPALLibrary.BranchTargets();
            try
            {
                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    string SupplierCode = ((DropDownList)gv_Target.FooterRow.FindControl("ddlLineCode")).SelectedValue;
                    string BranchName = ((DropDownList)gv_Target.FooterRow.FindControl("ddlBranch")).SelectedValue;
                    string Year = ((TextBox)gv_Target.FooterRow.FindControl("txtYear")).Text;
                    string TargetAmount = ((TextBox)gv_Target.FooterRow.FindControl("txtTarget")).Text;
                    objTarget.AddNewBranchTargets(SupplierCode, BranchName, Year, TargetAmount);
                    fnBindGrid();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                objTarget = null;
            }
        }

        protected void gv_Target_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            try
            {
                gv_Target.PageIndex = e.NewPageIndex;
                fnBindGrid();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void gv_Target_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            try
            {
                gv_Target.EditIndex = -1;
                fnBindGrid();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void gv_Target_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            try
            {
                gv_Target.EditIndex = e.NewEditIndex;
                fnBindGrid();
                string TotalAmt =((TextBox) gv_Target.Rows[e.NewEditIndex].Cells[3].FindControl("txtTargetAmount")).Text;
                string updatedTotalAmt= String.Format("{0:0.00}", string.IsNullOrEmpty(TotalAmt) ? 0.00 : Convert.ToDouble(TotalAmt));
               ((TextBox)gv_Target.Rows[e.NewEditIndex].Cells[3].FindControl("txtTargetAmount")).Text = updatedTotalAmt;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void gv_Target_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(Source, "fnBindGrid()", "Master Page Branch->Target");
            try
            {
                string BranchCode = string.Empty;
                IMPALLibrary.BranchTargets objTarget = new IMPALLibrary.BranchTargets();
                GridViewRow row = (GridViewRow)gv_Target.Rows[e.RowIndex];
                string SupplierCode = ((Label)row.FindControl("lblLineCode")).Text;
                string BranchName = ((Label)row.FindControl("lblBranch")).Text;
                Database ImpalDB = DataAccess.GetDatabase();

                using (DbCommand sqlCmd = ImpalDB.GetSqlStringCommand("select branch_code from branch_master where Branch_Name='" + BranchName + "'"))
                {
                    sqlCmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    BranchCode = ImpalDB.ExecuteScalar(sqlCmd).ToString();
                }

                string Year = ((Label)row.FindControl("lblYear")).Text;
                string TargetAmount = ((TextBox)row.FindControl("txtTargetAmount")).Text;
                objTarget.UpdateBranchTarget(SupplierCode, BranchCode, Year, TargetAmount);
                gv_Target.EditIndex = -1;
                fnBindGrid();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void gv_Target_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Target_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var txtGrdAmount = (Label)e.Row.FindControl("lblTarget");
                    txtGrdAmount.Text = FormatString(txtGrdAmount.Text);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private string FormatString(string grdValue)
        {
            string result = string.Empty;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                result = String.Format("{0:0.00}", string.IsNullOrEmpty(grdValue) ? 0.00 : Convert.ToDouble(grdValue));
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return result;
        } 
    }
}

