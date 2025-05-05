using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using System.Data.Common;
using System.Data;
using System.IO;
using IMPALLibrary.Transactions;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace IMPALWeb.Reports.Ordering.Stock
{
    public partial class WorksheetSegment : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private const string serverDownloadFolder = "Downloads\\WorkSheetSegment";
        private bool isDownloadPathAvailable = false;
        IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSupplier.Attributes.Add("OnChange", "return ValidSupllier();");
                fnPopulateSupplier();
                fnPopulateSupplierPartNumber("");
                fnPopulateReportName();
                ddlReport.Attributes.Add("OnChange", "fnReportType();");
            }

            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
        }

        protected void fnPopulateReportName()
        {
            string strSubstring = default(string);

            if (ddlSupplier.SelectedValue != "0")
            {
                strSubstring = ddlSupplier.SelectedValue.Substring(0, 2);
            }

            ImpalLibrary lib = new ImpalLibrary();
            List<DropDownListValue> lstdrop = new List<DropDownListValue>();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                lstdrop = lib.GetDropDownListValues("WorkSheetSegmnt");
                ddlReport.DataSource = lstdrop;
                ddlReport.DataValueField = "DisplayValue";
                ddlReport.DataTextField = "DisplayText";
                ddlReport.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                lib = null;
                lstdrop = null;
                Source = null;
            }
        }

        public void fnReportABCFMSWorksheet()
        {
            string strSupplierValue = ddlSupplier.SelectedValue;
            string strSupplierItemCode = ddlItemCodes.SelectedValue;

            IMPALLibrary.Transactions.InwardTransactions inwardTransactions = new IMPALLibrary.Transactions.InwardTransactions();
            string ItemPriceCnt = inwardTransactions.CheckBranchItemPriceABCFMSsegment(strSupplierValue, strBranchCode);

            if (ItemPriceCnt.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # " + ItemPriceCnt.Trim() + " missing in Branch Item Price Master. Please add the same and Process Again');", true);
                return;
            }

            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand dbcmdItem = ImpalDB.GetStoredProcCommand("usp_AddWorkSheetIndent_Segment_ABCFMS");
            ImpalDB.AddInParameter(dbcmdItem, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmdItem, "@Supplier_Code", DbType.String, strSupplierValue);
            dbcmdItem.CommandTimeout = ConnectionTimeOut.TimeOut; ;
            string strPONumber = Convert.ToString((object)ImpalDB.ExecuteScalar(dbcmdItem));

            fnWorksheetExcel("ABCFMS", strPONumber);
        }

        public void fnReportABCFMSNilWorksheet()
        {
            string strSupplierValue = ddlSupplier.SelectedValue;
            string strSupplierItemCode = ddlItemCodes.SelectedValue;

            IMPALLibrary.Transactions.InwardTransactions inwardTransactions = new IMPALLibrary.Transactions.InwardTransactions();
            string ItemPriceCnt = inwardTransactions.CheckBranchItemPriceNILABCFMSsegment(strSupplierValue, strBranchCode);

            if (ItemPriceCnt.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Part # " + ItemPriceCnt.Trim() + " missing in Branch Item Price Master. Please add the same and Process Again');", true);
                return;
            }

            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();
            DbCommand dbcmdItem = ImpalDB.GetStoredProcCommand("usp_AddWorkSheetIndent_ABCFMS_Nil");
            ImpalDB.AddInParameter(dbcmdItem, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmdItem, "@Supplier_Code", DbType.String, strSupplierValue);
            dbcmdItem.CommandTimeout = ConnectionTimeOut.TimeOut;
            string strPONumber = Convert.ToString((object)ImpalDB.ExecuteScalar(dbcmdItem));

            fnWorksheetExcel("NILABCFMS", strPONumber);
        }

        public void fnWorksheetExcel(string Indicator, string PONumber)
        {
            string strSupplierValue = ddlSupplier.SelectedValue;
            string filePath = @"D:\Downloads\WorkSheetSegment";

            isDownloadPathAvailable = uitility.CheckDirectoryExists(filePath);
            Microsoft.Practices.EnterpriseLibrary.Data.Database ImpalDB = DataAccess.GetDatabase();

            string filename = "WorkSheet_Segment_" + ddlSupplier.SelectedValue.ToString() + "_" + strBranchCode + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + ".xls";

            if (Indicator.Substring(0, 2).ToString() == "NI")
                filename = "Nil" + filename;
            else if (Indicator.Substring(0, 2).ToString() == "SF")
                filename = "SF" + filename;
            else if (Indicator.Substring(0, 2).ToString() == "SC")
                filename = "SCH" + filename;
            else if (Indicator.Substring(0, 2).ToString() == "SC")
                filename = "ABC" + filename;

            if (File.Exists(filePath + "\\" + filename))
                File.Delete(filePath + "\\" + filename);

            //object missing = System.Reflection.Missing.Value;
            //Microsoft.Office.Interop.Excel.Application xl = null;
            //Microsoft.Office.Interop.Excel._Workbook wb = null;
            //Microsoft.Office.Interop.Excel._Worksheet ws = null;
            //xl = new Microsoft.Office.Interop.Excel.Application();
            //xl.SheetsInNewWorkbook = 1;
            //xl.Visible = false;
            //wb = (_Workbook)(xl.Workbooks.Add(missing));
            //ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1);
            //ws.Columns.NumberFormat = "@";
            //ws.Cells.NumberFormat = "@";

            DataSet ds = new DataSet();
            DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_CreateWorkSheetExcel_Segment");
            ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
            ImpalDB.AddInParameter(dbcmd, "@Supplier_Code", DbType.String, strSupplierValue);
            ImpalDB.AddInParameter(dbcmd, "@PONumber", DbType.String, PONumber);
            ImpalDB.AddInParameter(dbcmd, "@FileName", DbType.String, filename);
            ImpalDB.AddInParameter(dbcmd, "@IndentType", DbType.String, Indicator);
            dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ds = ImpalDB.ExecuteDataSet(dbcmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                ArrayList root = new ArrayList();
                List<Dictionary<string, object>> table;
                Dictionary<string, object> data;

                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    table = new List<Dictionary<string, object>>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        data = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            data.Add(col.ColumnName, dr[col]);
                        }
                        table.Add(data);
                    }
                    root.Add(table);
                }

                hdnJSonExcelData.Value = serializer.Serialize(root);

                //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //{
                //    ws.Cells[1, i + 1] = ds.Tables[0].Columns[i].ColumnName;
                //}

                //for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                //{
                //    int s = i1 + 1;
                //    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                //    {
                //        ws.Cells[s + 1, j + 1] = ds.Tables[0].Rows[i1].ItemArray[j].ToString();
                //    }
                //}

                ////CommonDataMembers.ExportDataSetToExcel(ds, filename);

                ////ws.Cells.Font.Name = "Draft 12cpi";
                ////ws.Cells.Font.Size = "9";
                ////ws.PageSetup.TopMargin = 0;
                ////ws.PageSetup.BottomMargin = 0;
                ////ws.PageSetup.RightMargin = 0;
                ////ws.PageSetup.LeftMargin = 0;
                ////ws.Columns.AutoFit();            

                //wb.SaveAs(filePath + "\\" + filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                //wb.Close(true, missing, missing);
                //xl.Quit();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            btnReport.Attributes.Add("style", "display:none");
            btnBack.Attributes.Add("style", "display:inline");
            PanelHeaderDtls.Enabled = false;

            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                switch (ddlReport.SelectedValue)
                {
                    case "ABC FMS Worksheet":
                        {
                            fnReportABCFMSWorksheet();
                            break;
                        }
                    case "ABC FMS Nil Stock":
                        {
                            fnReportABCFMSNilWorksheet();
                            break;
                        }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void fnPopulateSupplierPartNumber(string strPartNumber)
        {
            ItemMasters objItemMaster = new ItemMasters();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlItemCodes.DataSource = objItemMaster.GetSupplierPartNumberWorkSheet(ddlSupplier.SelectedValue, strBranchCode, strPartNumber);
                ddlItemCodes.DataTextField = "Supplierpartno";
                ddlItemCodes.DataValueField = "itemcode";
                ddlItemCodes.DataBind();

                ddlItemCodes.SelectedIndex = 0;

                txtSupplierPart.Text = string.Empty;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                objItemMaster = null;
                Source = null;
            }
        }

        protected void fnPopulateSupplier()
        {
            Suppliers objsup = new Suppliers();
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlSupplier.DataSource = objsup.GetAllSuppliersWorkSheetSegment();
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierCode";
                ddlSupplier.DataBind();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                objsup = null;
                Source = null;
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplier.SelectedValue == "182" || ddlSupplier.SelectedValue == "230" ||
                        ddlSupplier.SelectedValue == "210" || ddlSupplier.SelectedValue == "300" ||
                        ddlSupplier.SelectedValue == "620" || ddlSupplier.SelectedValue == "790" ||
                        ddlSupplier.SelectedValue == "830" || ddlSupplier.SelectedValue == "360" ||
                        ddlSupplier.SelectedValue == "400")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Worksheet Cannot be Processed for this Supplier');", true);
                ddlSupplier.SelectedValue = "0";
            }

            fnPopulateSupplierPartNumber("");
            fnPopulateReportName();
            for (int count = 0; count < lstItem.Items.Count; count++)
            {
                lstItem.Items.Clear();
            }
        }

        protected void ddlItemCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSupplierPart.Text = ddlItemCodes.SelectedValue;
        }

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
                txtSupplierPart.Text = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                        break;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {
                Source = null;
            }
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("WorksheetSegment.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
