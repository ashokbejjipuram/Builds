using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPLib = IMPALLibrary;
using IMPALLibrary;
namespace IMPALWeb.Masters.Others
{
    public partial class PartyType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                grdPartyType.ShowFooter = false;
                int EditButtonIndex = grdPartyType.Columns.Count - 1;
                grdPartyType.Columns[EditButtonIndex].Visible = false;
            }
        }

        //protected void ucChartAccount_SearchImageClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //GridViewRow gvr = (GridViewRow)((ChartOfAccount)sender).Parent.Parent;
        //        //TextBox txtgrdChartOfAccount = (TextBox)gvr.FindControl("txtCreditAccount");
        //        //txtgrdChartOfAccount.Text = Session["ChatAccCode"].ToString();

        //    }
        //    catch (Exception exp)
        //    {

        //    }

        //}

        protected void ODSPartyType_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["PartyTypeCode"] = ((TextBox)grdPartyType.FooterRow.FindControl("txtPartyCode")).Text.Trim();
            e.InputParameters["PartyTypeDesc"] = ((TextBox)grdPartyType.FooterRow.FindControl("txtPartyDesc")).Text.Trim();
            e.InputParameters["PartyTypeDbAccount"] = ((TextBox)grdPartyType.FooterRow.FindControl("txtDebitAccount")).Text.Trim();
            e.InputParameters["PartyTypeCrAccount"] = ((TextBox)grdPartyType.FooterRow.FindControl("txtCreditAccount")).Text.Trim();
        }

        protected void grdPartyType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IMPLib.PartyTypeMaster objPartyTypes = new IMPLib.PartyTypeMaster();

            if (e.CommandName == "Insert" && Page.IsValid)
                if (objPartyTypes.ValidatePartyTypeCode(((TextBox)grdPartyType.FooterRow.FindControl("txtPartyCode")).Text.Trim()) == true)
                {
                    ODSPartyType.Insert();
                }
                else
                {
                    ((RequiredFieldValidator)grdPartyType.FooterRow.FindControl("RequiredFieldPartyCode")).ErrorMessage = "Party Code already exists";
                    ((RequiredFieldValidator)grdPartyType.FooterRow.FindControl("RequiredFieldPartyCode")).Validate();
                    ((RequiredFieldValidator)grdPartyType.FooterRow.FindControl("RequiredFieldPartyCode")).IsValid = false;
                }
        }


        protected void ucDebitChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                var txtDebitAccount = (TextBox)grdPartyType.FooterRow.FindControl("txtDebitAccount");
                txtDebitAccount.Text = Session["ChatAccCode"].ToString();
            }
            catch (Exception)
            {

            }

        }

        protected void ucCreditChartAccount_SearchImageClicked(object sender, EventArgs e)
        {
            try
            {
                var txtCreditAccount = (TextBox)grdPartyType.FooterRow.FindControl("txtCreditAccount");
                txtCreditAccount.Text = Session["ChatAccCode"].ToString();
            }
            catch (Exception)
            {

            }

        }

        #region report button click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("PartyTypeReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
