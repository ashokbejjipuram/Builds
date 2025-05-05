using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace IMPALWeb
{
    public partial class Depot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
               gvDepot.ShowFooter = false;
               int EditButtonIndex = gvDepot.Columns.Count - 1;
               gvDepot.Columns[EditButtonIndex].Visible = false;
            }
            if (!IsPostBack)
            {
                fnBindGrid();
            }
        }

        protected void fnBindGrid()
        {
            IMPALLibrary.DepotList objDepot = new IMPALLibrary.DepotList();
            gvDepot.DataSource = objDepot.GetAllDepots();
            gvDepot.DataBind();
        }

      
        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("DepotReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        protected void gvDepot_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDepot.EditIndex = -1;
            fnBindGrid();
        }

        protected void gvDepot_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepot.PageIndex = e.NewPageIndex;
            fnBindGrid();
        }

        protected void gvDepot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IMPALLibrary.DepotList objDepot = new IMPALLibrary.DepotList();
            if (e.CommandName == "Insert" && Page.IsValid)
            {
                string DepotCode = ((TextBox)gvDepot.FooterRow.FindControl("txtDepot")).Text;
                string DepotShortName = ((TextBox)gvDepot.FooterRow.FindControl("txtAddShort")).Text;
                string DepotLongName = ((TextBox)gvDepot.FooterRow.FindControl("txtAddlong")).Text;
                Database ImpalDB = DataAccess.GetDatabase();
                object BranchCode = string.Empty;
                using (DbCommand sqlCmd = ImpalDB.GetSqlStringCommand("Select Depot_Code from Depot_Master where depot_code='" + DepotCode.Trim() + "'"))
                {
                    sqlCmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    BranchCode = ImpalDB.ExecuteScalar(sqlCmd);
                   
                }

                if (BranchCode == null)
                {
                    objDepot.AddNewDepot(DepotCode, DepotShortName, DepotLongName);
                    fnBindGrid();
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Depot Code Already Exists');", true);
                }
              
              
            }
        }

        protected void gvDepot_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDepot.EditIndex = e.NewEditIndex;
            fnBindGrid();
        }

        protected void gvDepot_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            IMPALLibrary.DepotList objDepot = new IMPALLibrary.DepotList();
            GridViewRow row = (GridViewRow)gvDepot.Rows[e.RowIndex];
            string Depot = ((Label)row.FindControl("lblCode")).Text;
            string ShortDescription = ((TextBox)row.FindControl("txtEditShort")).Text;
            string LongDescription = ((TextBox)row.FindControl("txtEditLong")).Text;

            objDepot.UpdateDepot(Depot, ShortDescription, LongDescription);
            gvDepot.EditIndex=-1;
            fnBindGrid();
        }


    }
}
