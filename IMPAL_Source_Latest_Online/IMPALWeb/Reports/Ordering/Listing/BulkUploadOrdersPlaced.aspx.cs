#region Namespace
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Masters;
using IMPALLibrary.Transactions;
#endregion

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class BulkUploadOrdersPlaced : System.Web.UI.Page
    {
        #region Declaration
        string strBranchCode = string.Empty;
        DataTable dt = new DataTable();
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
                    txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Init
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
        protected void Page_Unload(object sender, EventArgs e)
        {
        }

        private void AddSelect(DropDownList ddl)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ListItem li = new ListItem();
                li.Text = "";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        #region btnReport_Click
        protected void btnReportExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();
                DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails_BulkUploadHO_Reprint(txtDate.Text);

                string filename = "HoSupplier_BulkUpload_Reprint_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls";
                XLWorkbook wbook = new XLWorkbook();

                for (int k = 0; k < ds.Tables.Count; k++)
                {
                    dt = ds.Tables[k];
                    dt.TableName = "Sheet" + (k + 1);
                    var sheet1 = wbook.Worksheets.Add(dt);
                    sheet1.Table("Table1").ShowAutoFilter = false;
                }

                try
                {
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = false; //true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wbook.SaveAs(MyMemoryStream);
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
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
            finally
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "alert('Report Has been Downloaded Successfully');", true);
            }
        }       
        #endregion
    }
}