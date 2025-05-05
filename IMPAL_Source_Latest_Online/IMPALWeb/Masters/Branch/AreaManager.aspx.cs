using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;



namespace IMPALWeb
{
    public partial class AreaManager : System.Web.UI.Page
    {
        List<DropDownListValue> oList = new List<DropDownListValue>();
        ImpalLibrary oCommon = new ImpalLibrary();
        AreaManagers AM = new AreaManagers();
               

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                GV_AM.ShowFooter = false;
                int EditButtonIndex = GV_AM.Columns.Count - 1;
                GV_AM.Columns[EditButtonIndex].Visible = false;
            }
        }
        protected void GV_AM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GV_AM_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GV_AM_DataBinding(object sender, EventArgs e)
        {
            // GV_GLGroup.Columns[0].Visible = false;
        }

        protected void GV_AM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" 
            // and the validation controls indicate valid data...
            if (e.CommandName == "Insert" && Page.IsValid)
                ObjectDataAM.Insert();

        }

        protected void ODSAM_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

            TextBox Code = (TextBox)GV_AM.FooterRow.FindControl("txtNewAMCode");
            TextBox Name = (TextBox)GV_AM.FooterRow.FindControl("txtNewAMName");
            DropDownList OpBranch = (DropDownList)GV_AM.FooterRow.FindControl("ddlOPBranch");
            TextBox sDate = (TextBox)GV_AM.FooterRow.FindControl("txtNewStDate");
            TextBox eDate = (TextBox)GV_AM.FooterRow.FindControl("txtNewEDate");
            TextBox PMName = (TextBox)GV_AM.FooterRow.FindControl("txtNewPreviousManager");
            TextBox PMsDate = (TextBox)GV_AM.FooterRow.FindControl("txtNewPMStDate");
            TextBox PMeDate = (TextBox)GV_AM.FooterRow.FindControl("txtNewPMeDate");

            e.InputParameters["AreaManagerCode"] = Code.Text;
            e.InputParameters["AreaManagerName"] = Name.Text;
            e.InputParameters["OperatingBranch"] = OpBranch.SelectedValue;
            e.InputParameters["StartDate"] = sDate.Text;
            e.InputParameters["EndDate"] = eDate.Text;
            e.InputParameters["PreviousManager"] = PMName.Text;
            e.InputParameters["PreviousManagerStartDate"] = PMsDate.Text;
            e.InputParameters["PreviousManagerEndDate"] = PMeDate.Text;

           

        }



        protected void ODSAM_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
           
        }

        protected void GV_AM_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GV_AM.Rows[e.RowIndex];
            TextBox Code = (TextBox)row.FindControl("txtAMCode");
            TextBox Name = (TextBox)row.FindControl("txtAMName");
            DropDownList OpBranch = (DropDownList)row.FindControl("ddlOPBranch");
            TextBox sDate = (TextBox)row.FindControl("txtstDate");
            TextBox eDate = (TextBox)row.FindControl("txteDate");
            TextBox PMName = (TextBox)row.FindControl("txtPreviousManager");
            TextBox PMsDate = (TextBox)row.FindControl("txtPMStDate");
            TextBox PMeDate = (TextBox)row.FindControl("txtPMeDate");
           
            ObjectDataAM.UpdateParameters.Add("AreaManagerCode", Code.Text);
            ObjectDataAM.UpdateParameters.Add("AreaManagerName", Name.Text);
            ObjectDataAM.UpdateParameters.Add("OperatingBranch", OpBranch.SelectedValue);
            ObjectDataAM.UpdateParameters.Add("StartDate", sDate.Text);
            ObjectDataAM.UpdateParameters.Add("EndDate",eDate.Text);
            ObjectDataAM.UpdateParameters.Add("PreviousManager",  PMName.Text);
            ObjectDataAM.UpdateParameters.Add("PreviousManagerStartDate", PMsDate.Text);
            ObjectDataAM.UpdateParameters.Add("PreviousManagerEndDate", PMeDate.Text);

          
          //  AM.UpdateAreaManagers(Code.Text, Name.Text, OpBranch.SelectedValue, sDate.Text, eDate.Text, PMName.Text, PMsDate.Text, PMeDate.Text);
            
        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("AMReport.aspx");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void GV_AM_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
