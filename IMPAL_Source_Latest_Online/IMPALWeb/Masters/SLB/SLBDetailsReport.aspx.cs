#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion
namespace IMPALWeb.Masters.SLB
{
    public partial class SLBDetailsReport : System.Web.UI.Page
    {
        string strBranchCode = default(string);

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
                    strBranchCode = Session["BranchCode"].ToString();
                    fnGenerateSelectionFormula();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page init
        public void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Inside Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Back nutton click
        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Inside btnBack_Click");
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
        #endregion

        #region Generate Selection formula
        public void fnGenerateSelectionFormula()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
            try
            {
                string strSLBCode = default(string);
                string strBranchCode = default(string);
                string strSupPartNo = default(string);
                string strSelectionFormula = default(string);

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("ddlSLB");
                DropDownList SourceDDl1 = (DropDownList)placeHolder.FindControl("ddlBranch");
                TextBox SuppPartNo = (TextBox)placeHolder.FindControl("txtSupplierPartNo");

                string strDDLSLB = SourceDDl.SelectedValue;
                string StrDDLBranch = SourceDDl1.SelectedValue;
                string strSupPart = SuppPartNo.Text;

                strSLBCode = "{SLB_Master.SLB_Code}";
                strBranchCode = "{SLB_Item_Detail.Branch_Code}";
                strSupPartNo = "{Item_Master.Supplier_Part_Number}";

                if (strBranchCode == "CRP")
                {
                    if (strDDLSLB == "" && StrDDLBranch == "")
                        strSelectionFormula = null;
                    else if (strDDLSLB != "" && StrDDLBranch == "")
                        strSelectionFormula = strSLBCode + "=" + " " + Convert.ToInt16(strDDLSLB);
                    else if (strDDLSLB == "" && StrDDLBranch != "")
                        strSelectionFormula = strBranchCode + "= '" + StrDDLBranch + "'";
                    else if (strDDLSLB != "" && StrDDLBranch != "")
                        strSelectionFormula = strSLBCode + "=" + " " + Convert.ToInt16(strDDLSLB) + " and " + strBranchCode + "= '" + StrDDLBranch + "'";
                }
                else
                {
                    if (strDDLSLB == "")
                        strSelectionFormula = null;
                    else
                        strSelectionFormula = strSLBCode + "=" + " " + Convert.ToInt16(strDDLSLB) + " and " + strBranchCode + "= '" + StrDDLBranch + "'";
                }

                if (strSupPart != "")
                {
                    if (strSelectionFormula == null)
                        strSelectionFormula = strSupPartNo + "= '" + strSupPart + "'";
                    else
                        strSelectionFormula = strSelectionFormula + " and " + strSupPartNo + "= '" + strSupPart + "'";
                }

                crSLBDetailReport.RecordSelectionFormula = strSelectionFormula;
                crSLBDetailReport.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}
