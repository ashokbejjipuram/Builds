#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary.Masters.CustomerDetails;
using IMPALLibrary.Masters;
#endregion

namespace IMPALWeb.Reports.Sales.SalesStatement
{
    public partial class TurnoverDiscount : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        #endregion

        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];
                if (!IsPostBack)
                {
                    if (crTurnoverDiscount != null)
                    {
                        crTurnoverDiscount.Dispose();
                        crTurnoverDiscount = null;
                    }

                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = txtFromDate.Text;
                    PopulateReportType();
                    GetCustomerList();
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crTurnoverDiscount != null)
            {
                crTurnoverDiscount.Dispose();
                crTurnoverDiscount = null;
            }
        }
        protected void crTurnoverDiscount_Unload(object sender, EventArgs e)
        {
            if (crTurnoverDiscount != null)
            {
                crTurnoverDiscount.Dispose();
                crTurnoverDiscount = null;
            }
        }

        #region btnReport_Click
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
            PanelHeaderDtls.Enabled = false;

            btnReportPDF.Attributes.Add("style", "display:inline");
            btnReportExcel.Attributes.Add("style", "display:inline");
            btnReportRTF.Attributes.Add("style", "display:inline");
            btnBack.Attributes.Add("style", "display:inline");
            btnReport.Attributes.Add("style", "display:none");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
        }
        #endregion

        #region CallCrystalReport
        private void GenerateAndExportReport(string fileType)
        {
            switch (ddlReportType.SelectedValue)
            {
                case "Report":
                    crTurnoverDiscount.ReportName = "TDPartWise";
                    break;
                case "Customer":
                    crTurnoverDiscount.ReportName = "TDCustomerWise";
                    break;
                case "SLBWise":
                    crTurnoverDiscount.ReportName = "TDSLBWise";
                    break;
            }

            string strSelectionFormula = null;
            string strDateQuery = "{V_tod.document_date}";
            string strBranchQuery = "{V_tod.Branch_code}";
            string strSupplierQuery = "mid({V_tod.Item_code},1,3)";
            string strCustomerQuery = "{V_tod.Customer_Code}";
            string strPartNumQuery = "{Item_master.supplier_part_number}";

            string strFromDate = Convert.ToDateTime(hidFromDate.Value).ToString("yyyy,MM,dd");
            string strToDate = Convert.ToDateTime(hidToDate.Value).ToString("yyyy,MM,dd");
            string strDateCompare = strDateQuery + " >= Date (" + strFromDate + ") and "
                                   + strDateQuery + " <= Date (" + strToDate + ")";

            strSelectionFormula = strDateCompare;
            if (ddlCustomer.SelectedIndex > 0)
                strSelectionFormula = strSelectionFormula + " and " + strCustomerQuery + " = '" + ddlCustomer.SelectedValue + "'";
            if (ddlLineCode.SelectedIndex > 0)
            {
                strSelectionFormula = strSelectionFormula + " and " + strSupplierQuery + " = '" + ddlLineCode.SelectedValue + "'";
                if (lstItem.Items.Count > 0)
                {
                    string strItemCodes = null;
                    foreach (ListItem lst in lstItem.Items)
                    {
                        if (string.IsNullOrEmpty(strItemCodes))
                            strItemCodes = "'" + lst.Text + "'";
                        else
                            strItemCodes = strItemCodes + ",'" + lst.Text + "'";
                    }
                    strSelectionFormula = strSelectionFormula + " and " + strPartNumQuery + " in [" + strItemCodes + "]";
                }
            }
            if (!strBranchCode.Equals("CRP"))
                strSelectionFormula = strSelectionFormula + " and " + strBranchQuery + " = '" + strBranchCode + "'";

            crTurnoverDiscount.CrystalFormulaFields.Add("From_Date", "'" + txtFromDate.Text + "'");
            crTurnoverDiscount.CrystalFormulaFields.Add("To_Date", "'" + txtToDate.Text + "'");
            crTurnoverDiscount.RecordSelectionFormula = strSelectionFormula;
            crTurnoverDiscount.GenerateReportAndExportHO(fileType);
        }
        #endregion

        #region PopulateReportType
        /// <summary>
        /// Populates the dropdown with Report Types from XML
        /// </summary>
        protected void PopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oLib = new ImpalLibrary();
                List<DropDownListValue> lstValues = new List<DropDownListValue>();
                lstValues = oLib.GetDropDownListValues("ReportType-TurnOverDiscount");
                ddlReportType.DataSource = lstValues;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region GetCustomerList
        public void GetCustomerList()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                List<CustomerDtls> lstCompletion = null;
                CustomerDetails oCustomerDtls = new CustomerDetails();
                CustomerDtls oCustomer = new CustomerDtls();
                lstCompletion = oCustomerDtls.GetCustomers(strBranchCode, null);
                ddlCustomer.DataSource = lstCompletion;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Code";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, string.Empty);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ListBox Events
        protected void btnNext_Click(object sender, EventArgs e)
        {
            bool blnFlag = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(ddlItemCodes.SelectedValue))
                {
                    string strText = ddlItemCodes.SelectedItem.Text;
                    foreach (ListItem lst in lstItem.Items)
                    {
                        if (strText.Equals(lst.Value))
                        {
                            blnFlag = true;
                            break;
                        }
                    }
                    if (!blnFlag)
                        lstItem.Items.Add(new ListItem(strText, strText));
                }

                ddlItemCodes.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                Source = null;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int intcount;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                for (intcount = 0; ;)
                {
                    if (intcount < lstItem.Items.Count)
                    {
                        if (lstItem.Items[intcount].Selected)
                        {
                            lstItem.Items.Remove(lstItem.Items[intcount].Text);
                        }
                        else
                            intcount++;
                    }
                    else
                    {
                        intcount = 0;
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                Source = null;
            }
        }
        #endregion

        #region LoadSupplierPartNum
        protected void LoadSupplierPartNum()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ItemMasters objItemMaster = new ItemMasters();
                ddlItemCodes.DataSource = objItemMaster.GetSupplierPartNumber(ddlLineCode.SelectedValue);
                ddlItemCodes.DataTextField = "Supplierpartno";
                ddlItemCodes.DataValueField = "itemcode";
                ddlItemCodes.DataBind();
                ddlItemCodes.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region ddlLineCode_IndexChanged
        protected void ddlLineCode_IndexChanged(object sender, EventArgs e)
        {
            LoadSupplierPartNum();
            for (int count = 0; count < lstItem.Items.Count; count++)
            {
                lstItem.Items.Clear();
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("TurnoverDiscount.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
    }
}
