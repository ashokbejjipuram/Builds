using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary;
using IMPALWeb.UserControls;

namespace IMPALWeb.Security
{
    public partial class Parameter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ParamterInfo paramterInfo = new ParamterInfo();

                IMPALLibrary.Parameter parameter = new IMPALLibrary.Parameter();
                paramterInfo = parameter.GetCurrentYear();

                if (!(paramterInfo.FromDate == "" && paramterInfo.FromDate == ""))
                {
                    txtFromDate.Text = Convert.ToDateTime(paramterInfo.FromDate).ToString("dd/MM/yyyy");
                    txtToDate.Text = Convert.ToDateTime(paramterInfo.ToDate).ToString("dd/MM/yyyy");
                    lblError.Text = "Already Exists...";
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    BtnSubmit.Enabled = true;
                    txtFromDate.Text = "01/04/" + System.DateTime.Now.Year;
                    txtToDate.Text = "31/03/" + System.DateTime.Now.AddYears(1).Year;
                    lblError.Text = "";
                }



            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                ParamterInfo paramterInfo = new ParamterInfo();

                paramterInfo.FromDate = txtFromDate.Text;
                paramterInfo.ToDate = txtToDate.Text;

                IMPALLibrary.Parameter parameter = new IMPALLibrary.Parameter();
                int result = parameter.AddNewParameterEntry(ref paramterInfo);
                lblError.Text = "Successfully Created the Accounting Period";
                lblError.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

    }
}
