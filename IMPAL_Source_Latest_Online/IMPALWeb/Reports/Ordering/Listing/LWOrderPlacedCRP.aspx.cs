using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary.Common;
using IMPALLibrary;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using ClosedXML.Excel;
using System.IO;

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class LWOrderPlacedCRP : System.Web.UI.Page
    {
        string strBranchCode = default(string);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
                if (Session["BranchCode"] != null)
                    strBranchCode = Session["BranchCode"].ToString();

                if (!IsPostBack)
                {
                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            PanelHeaderDtls.Enabled = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                btnReport.Text = "Back";               

                string filename = "LineWiseOrdersPlaced_" + txtFromDate.Text.Replace("/", "") + "_" + txtToDate.Text.Replace("/", "") + ".xls";

                DirectPurchaseOrders objPOdetails = new DirectPurchaseOrders();
                ds = objPOdetails.GetLineWiseOrdersPlacedDetails(ddlSupplierCode.SelectedValue, ddlBranchCode.SelectedValue, txtFromDate.Text, txtToDate.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ExportDataSetToExcel(ds, filename);                    
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            finally
            {               
            }
        }

        protected void ExportDataSetToExcel(DataSet ds, string strReportName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(ds);

                dt = ds.Tables[0];
                var sheet1 = wb.Worksheets.Add(dt);
                sheet1.Table("Table1").ShowAutoFilter = false;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false; //true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + strReportName);

                try
                {
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        MyMemoryStream.Close();
                        MyMemoryStream.Dispose();
                    }
                }
                catch (Exception exp)
                {
                    IMPALLibrary.Log.WriteException(Source, exp);
                }
                finally
                {
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //HttpContext.Current.Response.End();
                    HttpContext.Current.Response.Close();

                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                Server.ClearError();
                Response.Redirect("LWOrderPlacedCRP.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}