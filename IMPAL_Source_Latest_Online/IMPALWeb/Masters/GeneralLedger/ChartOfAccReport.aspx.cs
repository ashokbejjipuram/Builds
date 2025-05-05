#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
#endregion

namespace IMPALWeb.Masters.GeneralLedger
{
    public partial class ChartOfAccReport : System.Web.UI.Page
    {
        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (!IsPostBack)
                {
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

        #region BAck button click
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "btnBack_Click", "Inside btnBack_Click");
            try
            {
                Server.ClearError();
                Response.Redirect("ChartOfAccountMaster.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

            #region Generate Selection Formula
            public void fnGenerateSelectionFormula()
            {
                Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
                //Log.WriteLog(source, "fnGenerateSelectionFormula", "Inside fnGenerateSelectionFormula");
                try
                {
                string strSelectionFormula = default(string);
                string strClassificationCode =default(string);

                Control placeHolder = PreviousPage.Controls[0].FindControl("CPHDetails");
                DropDownList SourceDDl = (DropDownList)placeHolder.FindControl("drpClassificationGrp");

                string strDdlClassification = SourceDDl.SelectedValue;
                strClassificationCode = "{Chart_of_Account.Gl_Classification_Code}";

                    if(strDdlClassification == "ALL")
                        strSelectionFormula = null;
                    else
                    strSelectionFormula = strClassificationCode + "= '" + strDdlClassification + "'";

                    crGLChartOfAccMaster.RecordSelectionFormula = strSelectionFormula;
                    crGLChartOfAccMaster.GenerateReport();
                    }
                catch (Exception exp)
                {
                Log.WriteException(Source, exp);
                }
            }
#endregion
        }
    }

