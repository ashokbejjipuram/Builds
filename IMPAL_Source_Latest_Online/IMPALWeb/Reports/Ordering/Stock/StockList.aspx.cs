#region Namespace Declaration
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IMPALLibrary;
using IMPALLibrary.Common;
using System.Web.UI.WebControls;
using System.Web.UI;
#endregion

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class StockList : System.Web.UI.Page
    {
        string strBranchCode = string.Empty;

        #region Page init
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                if (Session["BranchCode"] != null)
                    strBranchCode = (string)Session["BranchCode"];

                if (!IsPostBack)
                {
                    if (crstocklist != null)
                    {
                        crstocklist.Dispose();
                        crstocklist = null;
                    }

                    fnPopulateBranches();
                    fnPopulateReportType();
                    ddlAging.Items.Clear();
                    ddlAging.Enabled = false;
                    if (strBranchCode != "CRP")
                    {
                        ddlBranchCode.SelectedValue = strBranchCode.ToString();
                        ddlBranchCode.Enabled = false;
                    }
                    else
                    {
                        ddlBranchCode.Enabled = true;
                    }
                    fnPopulateSuppliers(ddlBranchCode.SelectedValue);
                }

                if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                {
                    ddlSupplierCode.AutoPostBack = true;
                }
                else
                {
                    ddlSupplierCode.AutoPostBack = false;
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
            if (crstocklist != null)
            {
                crstocklist.Dispose();
                crstocklist = null;
            }
        }
        protected void crstocklist_Unload(object sender, EventArgs e)
        {
            if (crstocklist != null)
            {
                crstocklist.Dispose();
                crstocklist = null;
            }
        }

        #region Populate Report Type and List Dropdown
        protected void fnPopulateReportType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();

                if (strBranchCode == "MGT")
                    ddlList.DataSource = oCommon.GetDropDownListValues("ListMGT");
                else
                    ddlList.DataSource = oCommon.GetDropDownListValues("List");

                ddlList.DataTextField = "DisplayText";
                ddlList.DataValueField = "DisplayValue";
                ddlList.DataBind();

                ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-List2");
                ddlReportType.DataTextField = "DisplayText";
                ddlReportType.DataValueField = "DisplayValue";
                ddlReportType.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Suppliers Dropdown
        protected void fnPopulateSuppliers(string branchcode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Suppliers objSup = new Suppliers();
                ddlSupplierCode.DataSource = objSup.GetAllLinewiseSuppliers(branchcode);
                ddlSupplierCode.DataValueField = "SupplierCode";
                ddlSupplierCode.DataTextField = "SupplierName";
                ddlSupplierCode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Populate Branches Dropdown
        protected void fnPopulateBranches()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Branches objBranch = new Branches();
                ddlBranchCode.DataSource = objBranch.GetAllBranch();
                ddlBranchCode.DataValueField = "BranchCode";
                ddlBranchCode.DataTextField = "BranchName";
                ddlBranchCode.DataBind();
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region List Dropdown Index changed Method
        protected void ddlList_OnSelectIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImpalLibrary oCommon = new ImpalLibrary();
                ddlSupplierCode.Enabled = true;
                ddlSupplierCode.SelectedIndex = 0;
                //if (ddlList.SelectedIndex == 3)
                //{
                //    ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-List1");
                //    ddlReportType.DataTextField = "DisplayText";
                //    ddlReportType.DataValueField = "DisplayValue";
                //    ddlReportType.DataBind();
                //}
                ddlAging.Items.Clear();
                ddlAging.Enabled = false;

                if (ddlList.SelectedIndex == 3)
                {
                    ddlAging.DataSource = oCommon.GetDropDownListValues("Aging_PartNo");
                    ddlAging.DataTextField = "DisplayText";
                    ddlAging.DataValueField = "DisplayValue";
                    ddlAging.DataBind();
                    ddlAging.Items.Insert(0, new ListItem("--All--", String.Empty));
                    ddlAging.Enabled = true;


                    ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-List3");
                    ddlReportType.DataTextField = "DisplayText";
                    ddlReportType.DataValueField = "DisplayValue";
                    ddlReportType.DataBind();
                }
                else
                {
                    ddlReportType.DataSource = oCommon.GetDropDownListValues("RptType-List2");
                    ddlReportType.DataTextField = "DisplayText";
                    ddlReportType.DataValueField = "DisplayValue";
                    ddlReportType.DataBind();
                }

                //if (ddlList.SelectedIndex == 4)
                //{
                //    ddlSupplierCode.Enabled = false;
                //}
                //if (ddlList.SelectedIndex == 1)
                //{
                //    SegmentStock.Attributes.Add("style", "display:table-row");
                //}
                //else
                SegmentStock.Attributes.Add("style", "display:none");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion


        #region Report button click
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
                fnGenerateReport();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Report Has been Generated Successfully. Please Click on Below Buttons to Export in Respective Formats');", true);
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void fnGenerateReport()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlReportType.SelectedValue == "0")
                {
                    if (ddlList.SelectedIndex == 0)
                    {
                        crstocklist.ReportName = "StockListnew";
                    }
                    else if (ddlList.SelectedIndex.Equals(1))
                    {
                        crstocklist.ReportName = "stock_list";
                    }
                    else if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                    {
                        if (ddlList.SelectedIndex == 2)
                            crstocklist.ReportName = "SegmentStockList_New";
                        else if (ddlList.SelectedIndex == 4)
                            crstocklist.ReportName = "SegmentStockList_JLN_New";
                        else
                            crstocklist.ReportName = "SegmentStockList_UKD_New";
                    }
                }
                else
                {
                    if (ddlReportType.SelectedValue == "1")
                    {
                        crstocklist.ReportName = "PartNo_Aging";
                    }
                    else if (ddlReportType.SelectedValue == "2")
                    {
                        crstocklist.ReportName = "PartNo_Aging";
                    }
                    else if (ddlReportType.SelectedValue == "3")
                    {
                        crstocklist.ReportName = "PartNo_Aging";
                    }

                    if (ddlAging.SelectedIndex == 1)
                    {
                        crstocklist.ReportName = "PartNo_Aging_less3";
                    }
                    else if (ddlAging.SelectedIndex == 2)
                    {
                        crstocklist.ReportName = "PartNo_Aging_3to6";
                    }
                    else if (ddlAging.SelectedIndex == 3)
                    {
                        crstocklist.ReportName = "PartNo_Aging_6to12";
                    }
                    else if (ddlAging.SelectedIndex == 4)
                    {
                        crstocklist.ReportName = "PartNo_Aging_above12";
                    }
                }

                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = null;
                //int timediff = 0;

                //cmd = ImpalDB.GetSqlStringCommand("select top 1 Datediff(ss, datestamp, GETDATE()) from Rpt_ExecCount_Daily WITH (NOLOCK) where BranchCode = '" + Session["BranchCode"].ToString() + "' and reportname = '" + crstocklist.ReportName + "' order by datestamp desc");
                //cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                //timediff = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                //if (timediff > 0 && timediff <= 600)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('You Are Again Generating this Report With in no Time. Please Wait for 10 Minutes');", true);
                //    btnReportPDF.Attributes.Add("style", "display:none");
                //    btnReportExcel.Attributes.Add("style", "display:none");
                //    btnReportRTF.Attributes.Add("style", "display:none");
                //    btnBack.Attributes.Add("style", "display:inline");
                //    btnReport.Attributes.Add("style", "display:none");
                //    return;
                //}
                //else
                //{
                    ReportsData reportsDt = new ReportsData();
                    reportsDt.UpdRptExecCount(Session["BranchCode"].ToString(), Session["UserID"].ToString(), crstocklist.ReportName);
                //}

                if (ddlReportType.SelectedValue == "0")
                {
                    int intsuppcode = ddlSupplierCode.SelectedIndex;
                    string strsuppcode = ddlSupplierCode.SelectedValue.ToString();
                    int intbranchcode = ddlBranchCode.SelectedIndex;
                    string strBranchCode = ddlBranchCode.SelectedValue.ToString();
                    string productdesc = ddlProduct.SelectedValue.ToString();
                    string segmentdesc = ddlSegment.SelectedValue.ToString();
                    string vehicledesc = ddlVehicle.SelectedValue.ToString();

                    if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("stocklist");
                        if (!strsuppcode.Equals("0"))
                        {
                            ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                            ImpalDB.AddInParameter(cmd, "@Supplier", DbType.String, strsuppcode);
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedIndex.ToString());
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedIndex.ToString());
                        }

                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }
                    else if (ddlList.SelectedIndex == 0)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("stocklist");
                        if (!strsuppcode.Equals("0"))
                        {
                            ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                            ImpalDB.AddInParameter(cmd, "@Supplier", DbType.String, strsuppcode);
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedIndex.ToString());
                        }
                        else
                        {
                            ImpalDB.AddInParameter(cmd, "@branch", DbType.String, strBranchCode);
                            ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, ddlList.SelectedIndex.ToString());
                        }

                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }
                }
                else
                {
                    string selectionformula = string.Empty;

                    if (ddlReportType.SelectedValue == "1")
                    {
                        //crstocklist.ReportName = "PartNo_Aging";
                        string selcon1 = "' and {V_PartNo_Aging.Indicator} <> 'si'";
                        string strbrch = "{V_PartNo_Aging.branch_code}";
                        string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                        if (ddlSupplierCode.SelectedIndex != 0)
                        {
                            selectionformula = strbrch + "='" + strBranchCode + selcon1 + strSupplierCode;
                        }
                        else
                        {
                            selectionformula = strbrch + "='" + strBranchCode + selcon1;
                        }
                    }
                    else if (ddlReportType.SelectedValue == "2")
                    {
                        //crstocklist.ReportName = "PartNo_Aging";
                        string selcon2 = "' and {V_PartNo_Aging.Indicator} = 'si'";
                        string strbrch = "{V_PartNo_Aging.branch_code}";
                        string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                        if (ddlSupplierCode.SelectedIndex != 0)
                        {
                            selectionformula = strbrch + "='" + strBranchCode + selcon2 + strSupplierCode;
                        }
                        else
                        {
                            selectionformula = strbrch + "='" + strBranchCode + selcon2;
                        }
                    }
                    else if (ddlReportType.SelectedValue == "3")
                    {
                        //crstocklist.ReportName = "PartNo_Aging";
                        string strbrch = "{V_PartNo_Aging.branch_code}";
                        string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                        if (ddlSupplierCode.SelectedIndex != 0)
                        {
                            selectionformula = strbrch + "='" + strBranchCode + "'" + strSupplierCode;
                        }
                        else
                        {
                            selectionformula = strbrch + "='" + strBranchCode + "'";
                        }
                    }

                    //selectionformula = selectionformula + " and {Product_Branch_Sales_Tax.party_type_code} = 'DLRREG' and {Product_Branch_Sales_Tax.os_ls_indicator} = 'L'";
                    //selectionformula = selectionformula + " and {Product_Branch_Sales_Tax.status} = 'A' and {Sales_Tax_Master.Sales_Tax_Indicator} = 'L'";

                    if (ddlAging.SelectedIndex == 1)
                    {
                        //crstocklist.ReportName = "PartNo_Aging_less3";
                        //selectionformula = selectionformula + " and {V_PartNo_Aging.less3} <> 0";
                    }
                    else if (ddlAging.SelectedIndex == 2)
                    {
                        //crstocklist.ReportName = "PartNo_Aging_3to6";
                        //selectionformula = selectionformula + " and {V_PartNo_Aging.3to6}<> 0";
                    }
                    else if (ddlAging.SelectedIndex == 3)
                    {
                        //crstocklist.ReportName = "PartNo_Aging_6to12";
                        //selectionformula = selectionformula + " and {V_PartNo_Aging.6to12}<> 0";
                    }
                    else if (ddlAging.SelectedIndex == 4)
                    {
                        //crstocklist.ReportName = "PartNo_Aging_above12";
                        //selectionformula = selectionformula + " and ({V_PartNo_Aging.12to24}<> 0 or {V_PartNo_Aging.above24}<> 0)";
                    }

                    //crstocklist.RecordSelectionFormula = selectionformula;
                    //crstocklist.GenerateReport();
                }

                btnReportPDF.Attributes.Add("style", "display:inline");
                btnReportExcel.Attributes.Add("style", "display:inline");
                btnReportRTF.Attributes.Add("style", "display:inline");
                btnBack.Attributes.Add("style", "display:inline");
                btnReport.Attributes.Add("style", "display:none");
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        protected void GenerateAndExportReport(string fileType)
        {
            string sel1 = "{V_Stock_list_Superceeded.Supplier_Code}";
            string sel2 = "{V_Stock_list2_Superceeded.Supplier_Code}";
            string sel3 = "{V_Stock_list2_Superceeded.Product_Description}";
            string sel4 = "{V_Stock_list2_Superceeded.Application_Segment_Code}";
            string sel5 = "{V_Stock_list2_Superceeded.Vehicle_Type}";
            string sel6 = "{V_Stock_list2_Superceeded.branch_code}=";
            string sel7 = "{V_Stock_list_Superceeded.branch_code}=";
            string selectionformula = string.Empty;

            int intsuppcode = ddlSupplierCode.SelectedIndex;
            string strsuppcode = ddlSupplierCode.SelectedValue.ToString();
            int intbranchcode = ddlBranchCode.SelectedIndex;
            string strBranchCode = ddlBranchCode.SelectedValue.ToString();
            string productdesc = ddlProduct.SelectedValue.ToString();
            string segmentdesc = ddlSegment.SelectedValue.ToString();
            string vehicledesc = ddlVehicle.SelectedValue.ToString();

            if (ddlReportType.SelectedValue == "0")
            {
                if (ddlList.SelectedIndex == 0)
                {
                    crstocklist.ReportName = "StockListnew";

                    if (intsuppcode != 0)
                    {
                        selectionformula = sel6 + "'" + strBranchCode + "' and " + sel2 + "='" + strsuppcode + "'";
                    }
                    else
                    {
                        selectionformula = sel6 + "'" + strBranchCode + "'";
                    }

                    crstocklist.RecordSelectionFormula = selectionformula;
                    crstocklist.GenerateReportAndExportA4(fileType);
                }
                else if (ddlList.SelectedIndex.Equals(1))
                {
                    crstocklist.ReportName = "stock_list";

                    if (intsuppcode != 0)
                    {
                        selectionformula = sel7 + "'" + strBranchCode + "' and " + sel1 + "='" + strsuppcode + "'";
                    }
                    else
                    {
                        selectionformula = sel7 + "'" + strBranchCode + "'";
                    }

                    crstocklist.RecordSelectionFormula = selectionformula;
                    crstocklist.GenerateReportAndExportA4(fileType);
                }
                else if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                {
                    if (ddlList.SelectedIndex == 2)
                        crstocklist.ReportName = "SegmentStockList_New";
                    else if (ddlList.SelectedIndex == 4)
                        crstocklist.ReportName = "SegmentStockList_JLN_New";
                    else
                        crstocklist.ReportName = "SegmentStockList_UKD_New";

                    if (intsuppcode != 0)
                    {
                        selectionformula = sel6 + "'" + strBranchCode + "' and " + sel2 + "='" + strsuppcode + "'";
                    }
                    else
                    {
                        selectionformula = sel6 + "'" + strBranchCode + "'";
                    }

                    if (productdesc != "")
                    {
                        selectionformula = selectionformula + " and " + sel3 + "='" + productdesc + "'";
                    }
                    if (segmentdesc != "")
                    {
                        selectionformula = selectionformula + " and " + sel4 + "='" + segmentdesc + "'";
                    }
                    if (vehicledesc != "")
                    {
                        selectionformula = selectionformula + " and " + sel5 + "='" + vehicledesc + "'";
                    }

                    crstocklist.RecordSelectionFormula = selectionformula;
                    crstocklist.GenerateReportAndExport(fileType);
                }
            }
            else
            {
                if (ddlReportType.SelectedValue == "1")
                {
                    crstocklist.ReportName = "PartNo_Aging";
                    string selcon1 = "' and {V_PartNo_Aging.Indicator} <> 'si'";
                    string strbrch = "{V_PartNo_Aging.branch_code}";
                    string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                    if (ddlSupplierCode.SelectedIndex != 0)
                    {
                        selectionformula = strbrch + "='" + strBranchCode + selcon1 + strSupplierCode;
                    }
                    else
                    {
                        selectionformula = strbrch + "='" + strBranchCode + selcon1;
                    }
                }
                else if (ddlReportType.SelectedValue == "2")
                {
                    crstocklist.ReportName = "PartNo_Aging";
                    string selcon2 = "' and {V_PartNo_Aging.Indicator} = 'si'";
                    string strbrch = "{V_PartNo_Aging.branch_code}";
                    string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                    if (ddlSupplierCode.SelectedIndex != 0)
                    {
                        selectionformula = strbrch + "='" + strBranchCode + selcon2 + strSupplierCode;
                    }
                    else
                    {
                        selectionformula = strbrch + "='" + strBranchCode + selcon2;
                    }
                }
                else if (ddlReportType.SelectedValue == "3")
                {
                    crstocklist.ReportName = "PartNo_Aging";
                    string strbrch = "{V_PartNo_Aging.branch_code}";
                    string strSupplierCode = " and {V_PartNo_Aging.supplier_code}='" + ddlSupplierCode.SelectedValue + "'";

                    if (ddlSupplierCode.SelectedIndex != 0)
                    {
                        selectionformula = strbrch + "='" + strBranchCode + "'" + strSupplierCode;
                    }
                    else
                    {
                        selectionformula = strbrch + "='" + strBranchCode + "'";
                    }
                }

                if (ddlAging.SelectedIndex == 1)
                {
                    crstocklist.ReportName = "PartNo_Aging_less3";
                    selectionformula = selectionformula + " and {V_PartNo_Aging.less3} <> 0";
                }
                else if (ddlAging.SelectedIndex == 2)
                {
                    crstocklist.ReportName = "PartNo_Aging_3to6";
                    selectionformula = selectionformula + " and {V_PartNo_Aging.3to6}<> 0";
                }
                else if (ddlAging.SelectedIndex == 3)
                {
                    crstocklist.ReportName = "PartNo_Aging_6to12";
                    selectionformula = selectionformula + " and {V_PartNo_Aging.6to12}<> 0";
                }
                else if (ddlAging.SelectedIndex == 4)
                {
                    crstocklist.ReportName = "PartNo_Aging_above12";
                    selectionformula = selectionformula + " and ({V_PartNo_Aging.12to24}<> 0 or {V_PartNo_Aging.above24}<> 0)";
                }

                crstocklist.RecordSelectionFormula = selectionformula;
                crstocklist.GenerateReportAndExportA4(fileType);
            }            
        }

        #region BranchCode Index Changed Method
        protected void ddlBranchCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulateSuppliers(ddlBranchCode.SelectedValue);
        }
        #endregion

        protected void ddlSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                {
                    IMPALLibrary.Masters.Stock oStock = new IMPALLibrary.Masters.Stock();
                    ddlProduct.DataSource = oStock.GetSegmentStockProductList(ddlBranchCode.SelectedValue ,ddlSupplierCode.SelectedValue);
                    ddlProduct.DataTextField = "ProductGroupCode";
                    ddlProduct.DataValueField = "ProductGroupDescription";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, "");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                {
                    IMPALLibrary.Masters.Stock oStock = new IMPALLibrary.Masters.Stock();
                    ddlSegment.DataSource = oStock.GetSegmentStockSegmentList(ddlBranchCode.SelectedValue, ddlSupplierCode.SelectedValue, ddlProduct.SelectedValue);
                    ddlSegment.DataTextField = "ApplnSegmentDescription";
                    ddlSegment.DataValueField = "ApplicationSegmentCode";
                    ddlSegment.DataBind();
                    ddlSegment.Items.Insert(0, "");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ddlList.SelectedIndex == 2 || ddlList.SelectedIndex == 4 || ddlList.SelectedIndex == 5)
                {
                    IMPALLibrary.Masters.Stock oStock = new IMPALLibrary.Masters.Stock();
                    ddlVehicle.DataSource = oStock.GetSegmentStockVehilcleTypes(ddlBranchCode.SelectedValue, ddlSupplierCode.SelectedValue, ddlProduct.SelectedValue, ddlSegment.SelectedValue);
                    ddlVehicle.DataTextField = "VehicleTypeDescription";
                    ddlVehicle.DataValueField = "VehicleTypeDescription";
                    ddlVehicle.DataBind();
                    ddlVehicle.Items.Insert(0, "");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("StockList.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
