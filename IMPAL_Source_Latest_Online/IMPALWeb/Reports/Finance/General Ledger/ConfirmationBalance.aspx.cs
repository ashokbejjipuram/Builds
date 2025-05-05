#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using System.Data.Common;
using System.Data;
#endregion
namespace IMPALWeb.Reports.Finance.General_Ledger
{
    public partial class ConfirmationBalance : System.Web.UI.Page
    {
        #region Declaration
        string sessionvalue = string.Empty;
        #endregion

        #region Page Init
        /// <summary>
        /// To initialize page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        /// <summary>
        /// To Load page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {

                if (Session["BranchCode"] != null)
                    sessionvalue = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crconfirmationbalance != null)
                    {
                        crconfirmationbalance.Dispose();
                        crconfirmationbalance = null;
                    }

                    PopulateCustomerType();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    PopulateCustomer();
                }
                if (ddlCustomerSupplier.SelectedValue == "S")
                    divCustomerAddress.Visible = false;
                else if ((ddlFromCustomer.SelectedValue == string.Empty && ddlToCustomer.SelectedValue == string.Empty))
                    divCustomerAddress.Visible = false;
                else
                    divCustomerAddress.Visible = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crconfirmationbalance != null)
            {
                crconfirmationbalance.Dispose();
                crconfirmationbalance = null;
            }
        }
        protected void crconfirmationbalance_Unload(object sender, EventArgs e)
        {
            if (crconfirmationbalance != null)
            {
                crconfirmationbalance.Dispose();
                crconfirmationbalance = null;
            }
        }

        #region PopulateCustomerType
        /// <summary>
        /// To populate dropdown ddlCustomerSupplier
        /// </summary>
        protected void PopulateCustomerType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateCustomerType", "Entering PopulateCustomerType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                List<DropDownListValue> oList = new List<DropDownListValue>();
                oList = oCommon.GetDropDownListValues("CustomerSupplier");
                ddlCustomerSupplier.DataSource = oList;
                ddlCustomerSupplier.DataValueField = "DisplayValue";
                ddlCustomerSupplier.DataTextField = "DisplayText";
                ddlCustomerSupplier.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region ddlCustomerSupplier_SelectedIndexChanged
        /// <summary>
        /// To handle selection changed event for customer supplier dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCustomerSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ddlCustomerSupplier_SelectedIndexChanged", "Entering ddlCustomerSupplier_SelectedIndexChanged");
            try
            {
                if (ddlCustomerSupplier.SelectedValue == "S")
                    PopulateSupplier();
                else if (ddlCustomerSupplier.SelectedValue == "C")
                    PopulateCustomer();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region PopulateSupplier
        /// <summary>
        /// To populate supplier list in fromcustomer and tocustomer dropdowns
        /// </summary>
        protected void PopulateSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateSupplier", "Entering PopulateSupplier");
            try
            {
                IMPALLibrary.Suppliers suppliers = new IMPALLibrary.Suppliers();
                List<IMPALLibrary.Supplier> lstSupplier = new List<IMPALLibrary.Supplier>();
                lstSupplier = suppliers.GetAllSuppliers();
                lstSupplier.RemoveAt(0);
                lstSupplier.Insert(0, new IMPALLibrary.Supplier("", ""));
                ddlFromCustomer.DataSource = lstSupplier;
                ddlFromCustomer.DataTextField = "SupplierName";
                ddlFromCustomer.DataValueField = "SupplierCode";
                ddlFromCustomer.DataBind();
                ddlToCustomer.DataSource = lstSupplier;
                ddlToCustomer.DataTextField = "SupplierName";
                ddlToCustomer.DataValueField = "SupplierCode";
                ddlToCustomer.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region PopulateCustomer
        /// <summary>
        /// To Populate customer names in fromcustomer and tocustomer dropdowns
        /// </summary>
        protected void PopulateCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "PopulateCustomer", "Entering PopulateCustomer");
            try
            {
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails customerdetails = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                List<IMPALLibrary.Masters.CustomerDetails.CustomerDtls> lstcustomer = new List<IMPALLibrary.Masters.CustomerDetails.CustomerDtls>();
                if (sessionvalue != "CRP")
                    lstcustomer = customerdetails.GetCustomers(sessionvalue, null);
                else
                    lstcustomer = customerdetails.GetCustomers();
                lstcustomer.RemoveAt(0);
                lstcustomer.Insert(0, new IMPALLibrary.Masters.CustomerDetails.CustomerDtls("", ""));
                ddlFromCustomer.DataSource = lstcustomer;
                ddlFromCustomer.DataTextField = "Name";
                ddlFromCustomer.DataValueField = "Code";
                ddlFromCustomer.DataBind();
                ddlToCustomer.DataSource = lstcustomer;
                ddlToCustomer.DataTextField = "Name";
                ddlToCustomer.DataValueField = "Code";
                ddlToCustomer.DataBind();
                ddlFromCustomer.SelectedIndex = 0;
                ddlToCustomer.SelectedIndex = 0;

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlFromCustomer_SelectedIndexChanged1
        /// <summary>
        /// To handle 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlFromCustomer_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "ddlFromCustomer_SelectedIndexChanged1", "Entering ddlFromCustomer_SelectedIndexChanged1");
            try
            {
                DropDownList ddlcustomer = (DropDownList)sender;
                txtcustcode.Text = "";
                txtaddress1.Text = "";
                txtaddress2.Text = "";
                txtaddress3.Text = "";
                txtaddress4.Text = "";
                txtlocation.Text = "";
                if (ddlCustomerSupplier.SelectedValue == "C")
                {
                    IMPALLibrary.Masters.CustomerDetails.CustomerDetails customerdetails = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                    IMPALLibrary.Masters.CustomerDetails.CustomerDtls customeraddress = new IMPALLibrary.Masters.CustomerDetails.CustomerDtls();
                    if (ddlFromCustomer.SelectedValue == string.Empty && ddlToCustomer.SelectedValue == string.Empty)
                    {
                        divCustomerAddress.Visible = false;
                    }
                    else if (ddlFromCustomer.SelectedValue == string.Empty)
                    {
                        customeraddress = customerdetails.GetDetails(sessionvalue, ddlToCustomer.SelectedValue);
                        txtcustcode.Text = ddlToCustomer.SelectedValue;
                        divCustomerAddress.Visible = true;
                    }
                    else if (ddlToCustomer.SelectedValue == string.Empty)
                    {
                        customeraddress = customerdetails.GetDetails(sessionvalue, ddlFromCustomer.SelectedValue);
                        txtcustcode.Text = ddlFromCustomer.SelectedValue;
                        divCustomerAddress.Visible = true;
                    }
                    else
                    {
                        customeraddress = customerdetails.GetDetails(sessionvalue, ddlcustomer.SelectedValue);
                        txtcustcode.Text = ddlcustomer.SelectedValue;
                        divCustomerAddress.Visible = true;
                    }


                    //if (ddlFromCustomer.SelectedValue != string.Empty || ddlToCustomer.SelectedValue != string.Empty)
                    //{
                    //    if (ddlFromCustomer.SelectedValue == string.Empty)
                    //    {
                    //        customeraddress = customerdetails.GetDetails(ddlToCustomer.SelectedValue);
                    //        divCustomerAddress.Visible = true;
                    //    }
                    //    else if (ddlToCustomer.SelectedValue == string.Empty)
                    //    {
                    //        customeraddress = customerdetails.GetDetails(ddlFromCustomer.SelectedValue);
                    //        divCustomerAddress.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        customeraddress = customerdetails.GetDetails(ddlcustomer.SelectedValue);
                    //        divCustomerAddress.Visible = true;
                    //    }
                    //}
                    //else if (ddlFromCustomer.SelectedValue == string.Empty)
                    //{
                    //    customeraddress = customerdetails.GetDetails(ddlToCustomer.SelectedValue);
                    //    divCustomerAddress.Visible = true;
                    //}
                    //else if (ddlToCustomer.SelectedValue == string.Empty)
                    //{
                    //    customeraddress = customerdetails.GetDetails(ddlFromCustomer.SelectedValue);
                    //    divCustomerAddress.Visible = true;
                    //}



                    txtaddress1.Text = customeraddress.Address1;
                    txtaddress2.Text = customeraddress.Address2;
                    txtaddress3.Text = customeraddress.Address3;
                    txtaddress4.Text = customeraddress.Address4;
                    txtlocation.Text = customeraddress.Location;


                }
                //if (ddlcustomer.ID == "ddlFromCustomer")
                //    ddlToCustomer.SelectedValue = "";
                //if ((ddlFromCustomer.SelectedValue == string.Empty && ddlToCustomer.SelectedValue == string.Empty))
                //    divCustomerAddress.Visible = false;
            }

            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
        #endregion

        #region Generate report

        protected void btnReportPDF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".pdf");
        }
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".xls");
        }

        protected void btnReportRTF_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            GenerateAndExportReport(".doc");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                PanelHeaderDtls.Enabled = false;
                btnReportPDF.Attributes.Add("style", "display:inline");
                //btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");

                string Date = txtDate.Text;

                DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                string StrFromcust = ddlFromCustomer.SelectedValue;
                string StrTocust = ddlToCustomer.SelectedValue;
                Database ImpalDB = DataAccess.GetDatabaseBackUp();

                if (ddlCustomerSupplier.SelectedValue == "C")
                {
                    DbCommand cmd1 = ImpalDB.GetStoredProcCommand("usp_calcustos1");
                    ImpalDB.AddInParameter(cmd1, "@month_year", DbType.DateTime, date1);
                    ImpalDB.AddInParameter(cmd1, "@from_Cus", DbType.String, StrFromcust);
                    ImpalDB.AddInParameter(cmd1, "@To_Cus", DbType.String, StrTocust);
                    ImpalDB.AddInParameter(cmd1, "@Branch_Code", DbType.String, sessionvalue);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd1);
                }
                else if (ddlCustomerSupplier.SelectedValue == "S")
                {
                    DbCommand cmd2 = ImpalDB.GetStoredProcCommand("usp_calsupos1");
                    ImpalDB.AddInParameter(cmd2, "@month_year", DbType.DateTime, date1);
                    ImpalDB.AddInParameter(cmd2, "@from_sup", DbType.String, StrFromcust);
                    ImpalDB.AddInParameter(cmd2, "@To_sup", DbType.String, StrTocust);
                    ImpalDB.AddInParameter(cmd2, "@Branch_Code", DbType.String, sessionvalue);
                    cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd2);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void GenerateAndExportReport(string fileType)
        {
            string strSelectionFormula = string.Empty;
            string StrRptName = string.Empty;
            string StrCode = string.Empty;
            string StrMonthYear = string.Empty;
            string strBranchCode = string.Empty;
            string StrFromcust = ddlFromCustomer.SelectedValue;
            string StrTocust = ddlToCustomer.SelectedValue;

            if (ddlCustomerSupplier.SelectedValue == "C")
            {
                StrRptName = "Impal_confirm_bal";
                StrCode = "{outstanding.customer_code}";
                StrMonthYear = "{outstanding.month_year}";
                strBranchCode = "{outstanding.branch_code}";
            }
            else if (ddlCustomerSupplier.SelectedValue == "S")
            {
                StrRptName = "Impal_confirm_supp";
                StrCode = "{outstanding_supp.supplier_code}";
                StrMonthYear = "{outstanding_supp.month_year}";
                strBranchCode = "{outstanding_supp.branch_code}";
            }

            string Date = txtDate.Text;

            string DateField = "Date (" + DateTime.ParseExact(Date, "dd/MM/yyyy", null).ToString("yyyy,MM,dd") + ")";

            if (StrFromcust == "" && StrTocust == "" && Date != "")
            {
                strSelectionFormula = StrMonthYear + " = " + DateField;
            }
            else if (StrFromcust != "" && StrTocust != "" && Date != "")
            {
                strSelectionFormula = StrCode + " in '" + StrFromcust + "' to '" + StrTocust + "' and " + StrMonthYear + " = " + DateField;
            }

            strSelectionFormula = strSelectionFormula + " and " + strBranchCode + "='" + sessionvalue + "'";

            crconfirmationbalance.RecordSelectionFormula = strSelectionFormula;
            crconfirmationbalance.ReportName = StrRptName;
            crconfirmationbalance.GenerateReportAndExportHO(fileType);
        }
        #endregion
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("ConfirmationBalance.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
