#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace IMPALWeb.Reports.Finance.Cash_and_Bank
{
    public partial class ClassifiedOutstanding : System.Web.UI.Page
    {
        private string strCustomerCode = default(string);
        string strBranchCode = default(string);
        private string status = default(string);

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Inside Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    if (crClassifiedOutstanding_Report != null)
                    {
                        crClassifiedOutstanding_Report.Dispose();
                        crClassifiedOutstanding_Report = null;
                    }

                    CustInfo.Visible = false;
                    //txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ddlBranch.Text = strBranchCode;

                    if (strBranchCode != "CRP")
                        ddlBranch.Enabled = false;
                    else
                        ddlBranch.Enabled = true;

                    fnPopulateBranch();
                    fnPopulateFromCustomer();
                    fnPopulateToCustomer();
                    fnPopulateTown();
                    fnPopulateReportType();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
        protected void Page_Init(object sender, EventArgs e)
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crClassifiedOutstanding_Report != null)
            {
                crClassifiedOutstanding_Report.Dispose();
                crClassifiedOutstanding_Report = null;
            }
        }
        protected void crClassifiedOutstanding_Report_Unload(object sender, EventArgs e)
        {
            if (crClassifiedOutstanding_Report != null)
            {
                crClassifiedOutstanding_Report.Dispose();
                crClassifiedOutstanding_Report = null;
            }
        }

        #region populate Branch
        public void fnPopulateBranch()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateBranch", "Inside fnPopulateBranch");
            try
            {
                IMPALLibrary.Branches brnch = new IMPALLibrary.Branches();
                ddlBranch.DataSource = brnch.GetCorpBranch();
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate From Customer
        public void fnPopulateFromCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateFromCustomer", "Inside fnPopulateFromCustomer");
            try
            {
                string queryType = "Corporate";
                string strDefault = null;
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails cust = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                if (ddlBranch.SelectedValue == "CRP")
                {
                    cboFromCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, queryType);
                }
                else
                {
                    cboFromCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, strDefault);
                }
                cboFromCustomer.DataTextField = "Name";
                cboFromCustomer.DataValueField = "Code";
                cboFromCustomer.DataBind();
                cboFromCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate To Customer
        public void fnPopulateToCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateToCustomer", "Inside fnPopulateToCustomer");
            try
            {
                string queryType = "Corporate";
                string strDefault = null;
                IMPALLibrary.Masters.CustomerDetails.CustomerDetails cust = new IMPALLibrary.Masters.CustomerDetails.CustomerDetails();
                if (ddlBranch.SelectedValue == "CRP")
                {
                    cboToCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, queryType);
                }
                else
                {
                    cboToCustomer.DataSource = cust.GetCustomers(ddlBranch.Text, strDefault);
                }
                cboToCustomer.DataTextField = "Name";
                cboToCustomer.DataValueField = "Code";
                cboToCustomer.DataBind();
                cboToCustomer.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region populate Town
        public void fnPopulateTown()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateTown", "Inside fnPopulateTown");
            try
            {
                string strDefault = null;
                IMPALLibrary.Towns town = new IMPALLibrary.Towns();
                if (strBranchCode == "CRP")
                {
                    ddlTown.DataSource = town.GetAllTowns(strDefault);
                }
                else
                {
                    ddlTown.DataSource = town.GetAllTowns(strBranchCode);
                }
                ddlTown.DataTextField = "TownName";
                ddlTown.DataValueField = "Towncode";
                ddlTown.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Report Type
        public void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType", "Inside fnPopulateReportType");
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlReportType.DataSource = oCommon.GetDropDownListValues("ClassifiedOutstanding");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #endregion

        #region Populate Report Type1
        public void fnPopulateReportType1()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateReportType1", "Inside fnPopulateReportType1");
            try
            {
                List<string> lst1 = new List<string>();
                //lst1.Add("Report");
                //lst1.Add("Report-Above 90 Days");
                lst1.Add("Town Wise");
                ddlReportType.DataSource = lst1;
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #endregion

        #region Populate Customer Information
        public void fnPopulateCustInfo(string strBranchCode, string strCustomerCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnPopulateCustInfo", "Inside fnPopulateCustInfo");
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                if (!string.IsNullOrEmpty(strCustomerCode))
                {
                    CustInfo.Visible = true;
                    string sSQL = default(string);
                    sSQL = "select status ,address1,address2,address3,address4,location from customer_master WITH (NOLOCK) where Branch_Code='" + strBranchCode + "' and customer_code ='" + strCustomerCode + "'";
                    DbCommand cmdP = ImpalDB.GetSqlStringCommand(sSQL);
                    cmdP.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmdP))
                    {
                        while (reader.Read())
                        {
                            status = (string)reader[0];
                            txtAddress1.Text = (string)reader[1];
                            txtAddress2.Text = (string)reader[2];
                            txtAddress3.Text = (string)reader[3];
                            txtAddress4.Text = (string)reader[4];
                            txtLocation.Text = (string)reader[5];
                        }

                        txtCustCode.Text = strCustomerCode;
                    }
                }
                else
                    CustInfo.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        protected void cboFromCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            astHide.Visible = false;

            if (cboFromCustomer.SelectedIndex == 0 && cboToCustomer.SelectedIndex != 0)
            {
                CustInfo.Visible = true;
                fnPopulateCustInfo(strBranchCode, cboToCustomer.SelectedValue);
                astFromCust.Visible = false;
                astToCust.Visible = false;
                astDate.Visible = true;
            }
            else if (cboFromCustomer.SelectedIndex == 0)
            {
                astFromCust.Visible = false;
                astToCust.Visible = false;
                astDate.Visible = false;
                CustInfo.Visible = false;
                cboToCustomer.SelectedIndex = 0;
                txtDate.Text = "";
                ddlReportType.SelectedIndex = 0;
            }
            else
            {
                astFromCust.Visible = true;
                astToCust.Visible = true;
                astDate.Visible = true;
                CustInfo.Visible = true;
                //fnPopulateFromCustomer();
                fnPopulateCustInfo(strBranchCode, cboFromCustomer.SelectedValue);
            }
            //fnPopulateFromCustomer();
            //fnPopulateCustInfo(cboFromCustomer.SelectedValue);
        }

        protected void cboToCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fnPopulateToCustomer();
            if (cboToCustomer.SelectedIndex == 0 && cboFromCustomer.SelectedIndex != 0)
                fnPopulateCustInfo(strBranchCode, cboFromCustomer.SelectedValue);
            else if (cboToCustomer.SelectedIndex == 0)
            {
                CustInfo.Visible = false;
                astDate.Visible = false;
                txtDate.Text = "";
            }
            else
            {
                fnPopulateCustInfo(strBranchCode, cboToCustomer.SelectedValue);
                astDate.Visible = true;
            }
        }

        protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustInfo.Visible = false;
            if (ddlTown.SelectedIndex == 0)
                fnPopulateReportType();
            else
                fnPopulateReportType1();
        }

        #region Generate Button Click
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport, reportFiltersTable, reportViewerHolder);
                if (btnReport.Text == "Back")
                {
                    if (ddlReportType.SelectedValue == "Excel Report")
                    {
                        try
                        {                            
                            DataSet ds = new DataSet();

                            string str_head = "";
                            btnReport.Text = "Back";
                            string filename = "Outstanding" + string.Format("{0:ddMMyyyy}", txtDate.Text) + ".xls";

                            CashAndBankTransactions outStanding = new CashAndBankTransactions();

                            ds = outStanding.GetOutstandingDetails(Session["BranchCode"].ToString(), cboFromCustomer.SelectedValue, cboToCustomer.SelectedValue, txtDate.Text);
                            string strBranchName = (string)Session["BranchName"];
                            str_head = "<center><b><font size='6'>Outstanding Report for the Day " + string.Format("{0:dd/MM/yyyy}", txtDate.Text) + " of " + strBranchName + "</font></b><br><br></center>";

                            Response.Clear();
                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                            Response.ContentType = "application/ms-excel";
                            Response.Write(str_head);
                            Response.Write("<table border='1' style='font-family:arial;font-size:14px'>");

                            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                            {
                                Response.Write("<th style='background-color:#336699;color:white'>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
                            }

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Response.Write("<tr>");
                                DataRow row = ds.Tables[0].Rows[i];
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {

                                    Response.Write("<td>" + row[j] + "</td>");
                                }
                                Response.Write("</tr>");
                            }

                            Response.Write("</table>");
                        }
                        catch (Exception exp)
                        {
                            throw exp;
                        }
                        finally
                        {
                            Response.Flush();
                            Response.End();
                            Response.Close();
                        }
                    }
                    else if (ddlReportType.SelectedValue == "Report")
                    {
                        try
                        {
                            crClassifiedOutstanding_Report.ReportName = "ClassifiedOutstanding_Report";
                            fnGenerateSelectionFormula();
                        }
                        catch (Exception exp)
                        {
                            throw exp;
                        }
                    }
                    else if (ddlReportType.SelectedValue == "Report-Above 90 Days")
                    {
                        try
                        {
                            crClassifiedOutstanding_Report.ReportName = "ClassifiedOutstanding_Report90days";
                            fnGenerateSelectionFormula1();
                        }
                        catch (Exception exp)
                        {
                            throw exp;
                        }
                    }
                    else if (ddlReportType.SelectedValue == "Town Wise")
                    {
                        try
                        {
                            crClassifiedOutstanding_Report.ReportName = "ClassifiedOutstanding_Townwise";
                            fnGenerateSelectionFormula1();
                        }
                        catch (Exception exp)
                        {
                            throw exp;
                        }
                    }
                }
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
            try
            {
                string strDate = default(string);
                string strDate1 = default(string);
                string strFromCust = default(string);
                string strToCust = default(string);
                string strBranch = default(string);
                string strTown = default(string);
                int intProcStatus = default(int);
                string strSelectionFormula = default(string);

                strDate = txtDate.Text;
                strDate1 = txtDate.Text;
                strFromCust = cboFromCustomer.SelectedValue;
                strToCust = cboToCustomer.SelectedValue;
                strBranch = ddlBranch.SelectedValue;
                strTown = ddlTown.SelectedValue;

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                string strCustCode = default(string);
                string pstrBranchCode = default(string);
                string strTownCode = default(string);

                strCustCode = "{cust_aging.customer_code}";
                pstrBranchCode = "{cust_aging.branch_code}";
                strTownCode = "{customer_master.Town_Code}";

                if (strFromCust == "0" || strToCust == "0")
                {
                    string strQuery1 = "select min(customer_code),max(customer_code) from customer_master WITH (NOLOCK) where Branch_Code=" + "'" + strBranch + "'";
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery1);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                    {
                        while (reader.Read())
                        {
                            strFromCust = (string)reader[0];
                            strToCust = (string)reader[1];
                        }
                    }
                }

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_calcos");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch);
                ImpalDB.AddInParameter(dbcmd, "@frcustomer", DbType.String, strFromCust);
                ImpalDB.AddInParameter(dbcmd, "@tocustomer", DbType.String, strToCust);
                ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                if (strBranchCode != "CRP")
                {
                    if ((ddlTown.SelectedIndex != 0) && (ddlReportType.SelectedValue == "Town Wise"))
                        strSelectionFormula = "(" + strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "') and " + pstrBranchCode + " ='" + strBranchCode + "' and " + strTownCode + "=" + strTown;
                    else
                        strSelectionFormula = "(" + strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "') and " + pstrBranchCode + " ='" + strBranchCode + "'";
                }
                else
                {
                    if (ddlTown.SelectedIndex != 0)
                        strSelectionFormula = strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "' and " + strTownCode + "=" + strTown;
                    else
                        strSelectionFormula = strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "'";
                }

                crClassifiedOutstanding_Report.CrystalFormulaFields.Add("To_Date", "'" + txtDate.Text + "'");
                crClassifiedOutstanding_Report.RecordSelectionFormula = strSelectionFormula;
                crClassifiedOutstanding_Report.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Generate Selection Formula1
        public void fnGenerateSelectionFormula1()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "fnGenerateSelectionFormula1", "Inside fnGenerateSelectionFormula1");
            try
            {
                string strDate = default(string);
                string strDate1 = default(string);
                //string strCryDate = default(string);
                string strFromCust = default(string);
                string strToCust = default(string);
                string strBranch = default(string);
                string strTown = default(string);
                string strProc = default(string);
                int intProcStatus = default(int);
                string strSelectionFormula = default(string);

                strDate = txtDate.Text;
                strDate1 = txtDate.Text;
                strFromCust = cboFromCustomer.SelectedValue;
                strToCust = cboToCustomer.SelectedValue;
                strBranch = ddlBranch.SelectedValue;
                strTown = ddlTown.SelectedValue;

                Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
                string strCustCode = default(string);
                string pstrBranchCode = default(string);
                string strTownCode = default(string);

                if (ddlReportType.SelectedValue == "Report-Above 90 Days")
                {
                    strCustCode = "{cust_aging_120days.customer_code}";
                    pstrBranchCode = "{cust_aging_120days.branch_code}";
                    strTownCode = "{customer_master.Town_Code}";
                    strProc = "usp_calcos_180";
                }
                else if (ddlReportType.SelectedValue == "Town Wise")
                {
                    strCustCode = "{cust_aging.customer_code}";
                    pstrBranchCode = "{cust_aging.branch_code}";
                    strTownCode = "{town_master.town_code}";
                    strProc = "usp_calcos";
                }

                if (strFromCust == "0" || strToCust == "0")
                {
                    string strQuery1 = "select min(customer_code),max(customer_code) from customer_master WITH (NOLOCK) where Branch_Code=" + "'" + strBranch + "'";
                    DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery1);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                    {
                        while (reader.Read())
                        {
                            strFromCust = (string)reader[0];
                            strToCust = (string)reader[1];
                        }
                    }
                }

                DbCommand dbcmd = ImpalDB.GetStoredProcCommand(strProc);
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranch);
                ImpalDB.AddInParameter(dbcmd, "@frcustomer", DbType.String, strFromCust);
                ImpalDB.AddInParameter(dbcmd, "@tocustomer", DbType.String, strToCust);
                ImpalDB.AddInParameter(dbcmd, "@to_date", DbType.String, strDate.Trim());
                dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);

                if (strBranchCode != "CRP")
                {
                    if ((ddlTown.SelectedIndex != 0) && (ddlReportType.SelectedValue == "Town Wise"))
                        strSelectionFormula = "(" + strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "') and " + pstrBranchCode + " ='" + strBranchCode + "' and " + strTownCode + "=" + strTown;
                    else
                        strSelectionFormula = "(" + strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "') and " + pstrBranchCode + " ='" + strBranchCode + "'";
                }
                else
                    strSelectionFormula = strCustCode + ">= '" + strFromCust + "' or " + strCustCode + "<= '" + strToCust + "' and " + strTownCode + "=" + strTown;

                crClassifiedOutstanding_Report.CrystalFormulaFields.Add("To_Date", "'" + txtDate.Text + "'");
                crClassifiedOutstanding_Report.RecordSelectionFormula = strSelectionFormula;
                crClassifiedOutstanding_Report.GenerateReport();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Branch Selected Index change
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strBranchCode == "CRP")
            {
                fnPopulateFromCustomer();
                fnPopulateToCustomer();
            }
        }
        #endregion
    }
}
