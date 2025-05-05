using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;

namespace IMPALWeb.Masters.Branch
{
    public partial class NeighboringBranches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                lblNoRecord.Text = "";
                if (!IsPostBack)
                {
                    BindBranchName();
                    //if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                    //{
                        BtnSubmit.Enabled = false;
                    //}
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);              
            }
            
        }

        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
               // Neighboring_Branch obj = new Neighboring_Branch();
                BindNeighbouringBranch();
                if ((bool)!(Session["RoleCode"].ToString().Equals("CORP")))
                {
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    BtnSubmit.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindNeighbouringBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.NeighboringBranchItems";
                    obj.TypeName = "IMPALLibrary.Neighboring_Branch";
                    obj.SelectMethod = "GetNeignboringBranchDetails";
                    obj.SelectParameters.Add("strBranchCode", ddlBranchName.SelectedValue);
                    obj.DataBind();
                    grdNeighboringBranch.DataSource = obj;
                    grdNeighboringBranch.DataBind();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        
        }

        private void BindBranchName()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {                   
                    obj.DataObjectTypeName = "IMPALLibrary.Branch";
                    obj.TypeName = "IMPALLibrary.Branches";
                    obj.SelectMethod = "GetAllBranches";                    
                    obj.DataBind();
                    ddlBranchName.DataSource = obj;
                    ddlBranchName.DataTextField = "BranchName";
                    ddlBranchName.DataValueField = "BranchCode";
                    ddlBranchName.DataBind();
                    AddSelect(ddlBranchName);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            } 
        }

        private void AddSelect(DropDownList ddl)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
          
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;
            try
            {
                 List<NeighboringBranchItems> objInsValues = new List<NeighboringBranchItems>();
                 Neighboring_Branch DBobj = new Neighboring_Branch();
                for (int i = 0; i < grdNeighboringBranch.Rows.Count;i++ )
                {
                    NeighboringBranchItems objValue = new NeighboringBranchItems();

                    var txt =  new TextBox();
                    var hdn = new HiddenField();
                    txt = (TextBox) grdNeighboringBranch.Rows[i].FindControl("txtNeighboring");
                    hdn = (HiddenField)grdNeighboringBranch.Rows[i].FindControl("hdn");
                    objValue.Neighboring_Branch_Name = txt.Text.Trim();
                    objValue.Neighboring_Branch_Code = hdn.Value;

                    txt = (TextBox)grdNeighboringBranch.Rows[i].FindControl("txtPriority");
                    objValue.Priority = Convert.ToInt32(txt.Text.Trim());

                    txt = (TextBox)grdNeighboringBranch.Rows[i].FindControl("txtFreight");
                    objValue.Freight_Percent = Convert.ToDouble(txt.Text.Trim());

                    objInsValues.Add(objValue);                   

                }

               iRowCount =  DBobj.InsBranchDetails(objInsValues, ddlBranchName.SelectedValue);

               if (iRowCount > 0)
                {
                   // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Updated Sucessfully.');", true);

                    lblNoRecord.Text = "Updated Sucessfully.";
                    BtnSubmit.Enabled = false;
                }
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
                lblNoRecord.Text = "";
                ddlBranchName.SelectedValue = "0";
                BtnSubmit.Enabled = true;
                BindNeighbouringBranch();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        #region Report Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnReport_Click", "Inside btnReport_Click");
            try
            {
                Server.Execute("NeighboringReport.aspx");
                //string strBracnh = ddlBranchName.SelectedValue.ToString();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void grdNeighboringBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                grdNeighboringBranch.PageIndex = e.NewPageIndex;
                BindNeighbouringBranch(); ;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
