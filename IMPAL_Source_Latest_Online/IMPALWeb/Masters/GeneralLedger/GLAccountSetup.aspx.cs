using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;


namespace IMPALWeb
{
    public partial class GLAccountSetup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!IsPostBack)
                {
                    ddlNumber.Visible = false;
                    ddlSerialNo.Visible = false;

                    ddlDGLSub.Enabled = false;
                    ddlCGLSub.Enabled = false;
                    ddlDGLAccount.Enabled = false;
                    ddlCGLAccount.Enabled = false;
                    LoadTransactionType();
                    LoadDGLMain(false);
                    LoadCGLMain(false);
                    BtnSubmit.Attributes.Add("OnClick", "return ValidationSubmit();");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        private void LoadTransactionType()
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstTransactionType = new List<IMPALLibrary.GLAccountHeader>();
            lstTransactionType = glAccountSetups.GetTransactionType();
            ddlTransactionType.DataSource = lstTransactionType;
            ddlTransactionType.DataTextField = "Transaction_Type_Description";
            ddlTransactionType.DataValueField = "Transaction_Type_Code";
            ddlTransactionType.DataBind();

        }

        protected void imgEditToggle_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadNumber();
                ddlNumber.Visible = true;
                txtNumber.Visible = false;
                imgEditToggle.Visible = false;
                DisableOnEditMode();
                BtnSubmit.Enabled = false;
                ddlSerialNo.Visible = true;
                txtSerialNo.Visible = false;

                ddlTransactionType.Items.Clear();
                ddlDGLMain.Items.Clear();
                ddlCGLMain.Items.Clear();
                ddlDGLSub.Items.Clear();
                ddlCGLSub.Items.Clear();
                ddlDGLAccount.Items.Clear();
                ddlCGLAccount.Items.Clear();
                txtDescription.Text = "";

                ddlSerialNo.Items.Clear();
                ddlSerialNo.Enabled = true;



            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadNumber()
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstControlNumber = new List<IMPALLibrary.GLAccountHeader>();
            lstControlNumber = glAccountSetups.GetControlNumber();
            ddlNumber.DataSource = lstControlNumber;
            ddlNumber.DataTextField = "transaction_type_description";
            ddlNumber.DataValueField = "control_number";
            ddlNumber.DataBind();

        }

        private void LoadSerialNumber(string controlNumber)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstSerialNumber = new List<IMPALLibrary.GLAccountHeader>();
            lstSerialNumber = glAccountSetups.GetSerialNumber(controlNumber);
            ddlSerialNo.DataSource = lstSerialNumber;
            ddlSerialNo.DataTextField = "Serial_Number";
            ddlSerialNo.DataValueField = "Serial_Number";
            ddlSerialNo.DataBind();

        }


        private void LoadDGLMain(bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstDGLMain = new List<IMPALLibrary.GLAccountHeader>();
            lstDGLMain = glAccountSetups.GetDGLMain(Assign);
            ddlDGLMain.DataSource = lstDGLMain;
            ddlDGLMain.DataTextField = "Debit_GL_Main_Description";
            ddlDGLMain.DataValueField = "Debit_GL_Main_Code";
            ddlDGLMain.DataBind();

        }

        private void LoadCGLMain(bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstCGLMain = new List<IMPALLibrary.GLAccountHeader>();
            lstCGLMain = glAccountSetups.GetCGLMain(Assign);
            ddlCGLMain.DataSource = lstCGLMain;
            ddlCGLMain.DataTextField = "GL_Main_Description";
            ddlCGLMain.DataValueField = "GL_Main_Code";
            ddlCGLMain.DataBind();

        }

        private void LoadDGLSub(string strDGLMain, bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstDGLSub = new List<IMPALLibrary.GLAccountHeader>();
            lstDGLSub = glAccountSetups.GetDGLSub(strDGLMain, Assign);
            ddlDGLSub.DataSource = lstDGLSub;
            ddlDGLSub.DataTextField = "Debit_GL_Sub_Description";
            ddlDGLSub.DataValueField = "Debit_GL_Sub_Code";
            ddlDGLSub.DataBind();

        }

        private void LoadCGLSub(string strCGLMain, bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstCGLSub = new List<IMPALLibrary.GLAccountHeader>();
            lstCGLSub = glAccountSetups.GetCGLSub(strCGLMain, Assign);
            ddlCGLSub.DataSource = lstCGLSub;
            ddlCGLSub.DataTextField = "GL_Sub_Description";
            ddlCGLSub.DataValueField = "GL_Sub_Code";
            ddlCGLSub.DataBind();

        }

        private void LoadDGLAccountCode(string strDGLMain, string strDGLSub, string strTransType, bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstDGLAccountCode = new List<IMPALLibrary.GLAccountHeader>();
            lstDGLAccountCode = glAccountSetups.GetDGLAccountCode(strDGLMain, strDGLSub, strTransType, Assign);
            ddlDGLAccount.DataSource = lstDGLAccountCode;
            ddlDGLAccount.DataTextField = "Debit_AccountCode_Description";
            ddlDGLAccount.DataValueField = "Debit_GL_Account_Code";
            ddlDGLAccount.DataBind();

        }

        private void LoadCGLAccountCode(string strCGLMain, string strCGLSub, bool Assign)
        {
            IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

            List<IMPALLibrary.GLAccountHeader> lstCGLAccountCode = new List<IMPALLibrary.GLAccountHeader>();
            lstCGLAccountCode = glAccountSetups.GetCGLAccountCode(strCGLMain, strCGLSub, Assign);
            ddlCGLAccount.DataSource = lstCGLAccountCode;
            ddlCGLAccount.DataTextField = "Description";
            ddlCGLAccount.DataValueField = "GL_Account_Code";
            ddlCGLAccount.DataBind();

        }

        private void DisableOnEditMode()
        {
            ddlSerialNo.Enabled = false;
            ddlTransactionType.Enabled = false;
            txtDescription.Enabled = false;
            ddlDGLMain.Enabled = false;
            ddlCGLMain.Enabled = false;
            ddlDGLSub.Enabled = false;
            ddlCGLSub.Enabled = false;
            ddlDGLAccount.Enabled = false;
            ddlCGLAccount.Enabled = false;

        }

        protected void ddlNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string controlNumber = string.Empty;
                controlNumber = ddlNumber.SelectedValue.ToString();

                LoadSerialNumber(controlNumber);
                ddlSerialNo.Enabled = true;

                ddlTransactionType.Enabled = false;
                ddlTransactionType.Items.Clear();
                txtDescription.Enabled = false;
                txtDescription.Text = "";

                ddlDGLMain.Enabled = false;
                ddlCGLMain.Enabled = false;
                ddlDGLSub.Enabled = false;
                ddlCGLSub.Enabled = false;
                ddlDGLAccount.Enabled = false;
                ddlCGLAccount.Enabled = false;

                ddlDGLMain.Items.Clear();
                ddlCGLMain.Items.Clear();
                ddlDGLSub.Items.Clear();
                ddlCGLSub.Items.Clear();
                ddlDGLAccount.Items.Clear();
                ddlCGLAccount.Items.Clear();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();

                List<IMPALLibrary.GLAccountHeader> lstAccountHeader = new List<IMPALLibrary.GLAccountHeader>();
                lstAccountHeader = glAccountSetups.GetAccountHeader(ddlNumber.SelectedValue.ToString(), ddlSerialNo.SelectedValue.ToString());

                if (lstAccountHeader.Count > 0)
                {
                    ddlNumber.SelectedValue = lstAccountHeader[0].Control_Number;
                    ddlSerialNo.SelectedValue = lstAccountHeader[0].Serial_Number;
                    LoadTransactionType();

                    ddlTransactionType.SelectedValue = lstAccountHeader[0].Transaction_Type_Code;
                    txtDescription.Text = lstAccountHeader[0].Control_Description;

                    if (!(lstAccountHeader[0].Debit_GL_Main_Code == "0" || lstAccountHeader[0].Debit_GL_Main_Code == ""))
                    {
                        LoadDGLMain(true);
                        ddlDGLMain.SelectedValue = lstAccountHeader[0].Debit_GL_Main_Code;
                    }
                    else
                    {
                        ddlDGLMain.Items.Clear();
                    }

                    if (!(lstAccountHeader[0].GL_Main_Code == "0" || lstAccountHeader[0].GL_Main_Code == ""))
                    {
                        LoadCGLMain(true);
                        ddlCGLMain.SelectedValue = lstAccountHeader[0].GL_Main_Code;
                    }
                    else
                    {
                        ddlDGLMain.Items.Clear();
                    }


                    if (!(ddlDGLMain.SelectedValue == "0" || ddlDGLMain.SelectedValue == ""))
                    {
                        LoadDGLSub(ddlDGLMain.SelectedValue, true);
                        ddlDGLSub.SelectedValue = lstAccountHeader[0].Debit_GL_Sub_Code;
                    }

                    if (!(ddlCGLMain.SelectedValue == "0" || ddlCGLMain.SelectedValue == ""))
                    {
                        LoadCGLSub(ddlCGLMain.SelectedValue, true);
                        ddlCGLSub.SelectedValue = lstAccountHeader[0].GL_Sub_Code;
                    }




                    if (!(ddlDGLMain.SelectedValue == "0" || ddlDGLMain.SelectedValue == ""))
                    {
                        LoadDGLAccountCode(ddlDGLMain.SelectedValue, ddlDGLSub.SelectedValue, ddlTransactionType.SelectedValue, true);
                        ddlDGLAccount.SelectedValue = lstAccountHeader[0].Debit_GL_Account_Code;
                    }

                    if (!(ddlCGLMain.SelectedValue == "0" || ddlCGLMain.SelectedValue == ""))
                    {
                        LoadCGLAccountCode(ddlCGLMain.SelectedValue, ddlCGLSub.SelectedValue, true);
                        ddlCGLAccount.SelectedValue = lstAccountHeader[0].GL_Account_Code;
                    }

                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void ddlDGLMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadDGLSub(ddlDGLMain.SelectedValue, false);
                LoadDGLAccountCode(ddlDGLMain.SelectedValue, ddlDGLSub.SelectedValue, "", false);
                ddlDGLSub.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlDGLSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadDGLAccountCode(ddlDGLMain.SelectedValue, ddlDGLSub.SelectedValue, "", false);
                ddlDGLAccount.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void ddlCGLMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadCGLSub(ddlCGLMain.SelectedValue, false);
                LoadCGLAccountCode(ddlCGLMain.SelectedValue, ddlCGLSub.SelectedValue, false);
                ddlCGLSub.Enabled = true;


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void ddlCGLSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                LoadCGLAccountCode(ddlCGLMain.SelectedValue, ddlCGLSub.SelectedValue, false);
                ddlCGLAccount.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                GLAccountHeader glAccountHeader = new GLAccountHeader();


                glAccountHeader.Transaction_Type_Code = ddlTransactionType.SelectedValue;
                glAccountHeader.Control_Description = txtDescription.Text;
                glAccountHeader.Debit_GL_Main_Code = ddlDGLMain.SelectedValue;
                glAccountHeader.GL_Main_Code = ddlCGLMain.SelectedValue;
                glAccountHeader.Debit_GL_Sub_Code = ddlDGLSub.SelectedValue;
                glAccountHeader.GL_Sub_Code = ddlCGLSub.SelectedValue;
                glAccountHeader.Debit_GL_Account_Code = ddlDGLAccount.SelectedValue;
                glAccountHeader.GL_Account_Code = ddlCGLAccount.SelectedValue;

                IMPALLibrary.GLAccountSetups glAccountSetups = new IMPALLibrary.GLAccountSetups();
                glAccountHeader = glAccountSetups.AddNewGLAccountEntry(ref glAccountHeader);
                //txtNumber.Text = ddlNumber.SelectedValue;
                //txtSerialNo.Text = ddlSerialNo.SelectedValue;
                txtNumber.Text = glAccountHeader.Control_Number;
                txtSerialNo.Text = glAccountHeader.Serial_Number;
                btnReport.Visible = false;
                BtnSubmit.Enabled = false;

                ddlTransactionType.Enabled = false;
                txtDescription.Enabled = false;

                ddlDGLMain.Enabled = false;
                ddlCGLMain.Enabled = false;
                ddlDGLSub.Enabled = false;
                ddlCGLSub.Enabled = false;
                ddlDGLAccount.Enabled = false;
                ddlCGLAccount.Enabled = false;

                if (glAccountHeader.Debit_GL_Main_Code == "0" || glAccountHeader.Debit_GL_Main_Code == "")
                {
                    ddlDGLMain.Items.Clear();
                }

                if (glAccountHeader.GL_Main_Code == "0" || glAccountHeader.GL_Main_Code == "")
                {
                    ddlCGLMain.Items.Clear();
                }


                //updateSupplier.Update();
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

                BtnSubmit.Enabled = true;

                txtNumber.Visible = true;
                ddlNumber.Visible = false;
                txtNumber.Text = "";

                imgEditToggle.Visible = true;

                LoadDGLMain(false);
                LoadCGLMain(false);
                LoadTransactionType();

                ddlDGLSub.Items.Clear();
                ddlCGLSub.Items.Clear();
                ddlDGLAccount.Items.Clear();
                ddlCGLAccount.Items.Clear();


                ddlDGLSub.Enabled = false;
                ddlCGLSub.Enabled = false;
                ddlDGLAccount.Enabled = false;
                ddlCGLAccount.Enabled = false;

                ddlTransactionType.SelectedValue = "0";
                txtDescription.Text = "";

                ddlSerialNo.Visible = false;
                txtSerialNo.Visible = true;
                txtSerialNo.Text = "";

                ddlTransactionType.Enabled = true;
                txtDescription.Enabled = true;
                txtSerialNo.Enabled = true;
                ddlDGLMain.Enabled = true;
                ddlCGLMain.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                Server.Execute("GLAccountSetupReport.aspx");

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }



    }
}
