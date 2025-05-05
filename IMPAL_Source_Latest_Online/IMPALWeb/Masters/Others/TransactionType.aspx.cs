using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Masters.Others
{
    public partial class TransactionType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                grdTranType.ShowFooter = false;
                int EditButtonIndex = grdTranType.Columns.Count - 1;
                grdTranType.Columns[EditButtonIndex].Visible = false;
            }
        }

        protected void ObjectDSTranType_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            TextBox TranTypeCode = (TextBox)grdTranType.FooterRow.FindControl("txtTranTypeCode");
            TextBox TranTypeDesc = (TextBox)grdTranType.FooterRow.FindControl("txtTranDesc");
            DropDownList Module = (DropDownList)grdTranType.FooterRow.FindControl("ddlModule");
            DropDownList ParamNumber = (DropDownList)grdTranType.FooterRow.FindControl("ddlParamNumber");
            RadioButton AffSales = (RadioButton)grdTranType.FooterRow.FindControl("rbAffSalesYes");
            RadioButton Indicator = (RadioButton)grdTranType.FooterRow.FindControl("rbIndiRevenue");

            e.InputParameters["TranTypeCode"] = TranTypeCode.Text.Trim();
            e.InputParameters["TranTypeDesc"] = TranTypeDesc.Text.Trim();
            e.InputParameters["TransTypeModuleCode"] = Module.SelectedValue.ToString();
            e.InputParameters["ParameterNumber"] = ParamNumber.SelectedValue.ToString();

            if (AffSales.Checked)
                e.InputParameters["AffectSales"] = "Y";
            else
                e.InputParameters["AffectSales"] = "N";

            if (Indicator.Checked)
                e.InputParameters["Indicator"] = "R";
            else
                e.InputParameters["Indicator"] = "E";
        }

        protected void grdTranType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDSTranType.Insert();
        }

        protected void grdTranType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void ObjectDSTranType_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("TransactionTypeReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
