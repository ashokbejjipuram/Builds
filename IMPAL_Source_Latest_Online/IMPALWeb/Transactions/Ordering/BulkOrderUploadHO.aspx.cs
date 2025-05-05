using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Common;
using IMPALLibrary.Transactions;
using System.Data;
using System.Globalization;
using log4net;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ClosedXML.Excel;

namespace IMPALWeb
{
    public partial class BulkOrderUploadHO : System.Web.UI.Page
    {
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private string filePath = string.Empty;
        private string fileName = string.Empty;
        DataTable dt = new DataTable();
        OleDbConnection OledbConn;
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(BulkOrderUploadHO), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                BtnSupplierExcelFile.Visible = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            
            try
            {
                string sStatus = string.Empty;                

                if (btnFileUpload.HasFile)
                {
                    filePath = @"D:\Downloads\BulkOrderHO";

                    string filePathDest = @"D:\Downloads\BulkOrderHO\Old";

                    int a = 0;

                    string fileName = btnFileUpload.FileName;

                    string dest = Path.Combine(filePathDest, fileName);

                    if (File.Exists(filePath + "\\" + fileName))
                    {
                        while (File.Exists(dest))
                        {
                            a++;
                            dest = Path.Combine(filePathDest, fileName.Replace(".xls", " (" + a + ").xls"));
                        }

                        File.Copy(Path.Combine(filePath, fileName), dest);
                    }

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(filePath + "\\" + fileName))
                    {
                        string myexceldataquery = "Select * from [sheet1$]";
                        string ExcelConnectionString = "";
                        string sqlTotQuery = "";

                        if (fileName.ToString().EndsWith("s"))
                            ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                        else
                            ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                        OledbConn = new OleDbConnection(ExcelConnectionString);
                        OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                        OledbConn.Open();
                        OleDbDataReader dr = OledbCmd.ExecuteReader();

                        Database ImpalDb = DataAccess.GetDatabase();
                        sqlTotQuery = "delete from Bulk_Order_Upload_HO where ExcelFileName='" + fileName + "'";
                        DbCommand cmd = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd);
                        cmd = null;

                        while (dr.Read())
                        {
                            sqlTotQuery = "Insert into Bulk_Order_Upload_HO values ('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "',NULL,'" + dr[3] + "',0,'" + fileName + "',GETDATE())";
                            cmd = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd);
                        }

                        OledbConn.Close();
                        cmd = null;

                        if (fileName == "" || fileName == null)
                        {
                            FileStatusMsg.Text = "<font style='color: red' size='3'><b>File Doesn't Exist. Please Check the Same</b></font>";
                            divItemDetailsExcel.Attributes.Add("style", "display:inline");
                        }
                        else
                        {
                            if (File.Exists(filePath + "\\" + fileName))
                            {
                                cmd = ImpalDb.GetStoredProcCommand("usp_AddDirectPurchaseOrder_BulkUpload_HO");
                                ImpalDb.AddInParameter(cmd, "@Excel_File_Name", DbType.String, fileName.Trim());
                                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                                ImpalDb.ExecuteNonQuery(cmd);
                                cmd = null;

                                FileStatusMsg.Text = "<font style='color: Green' size='4'><b>HO Bulk Order File Has been Uploaded.</b></font>";
                                row1.Attributes.Add("style", "display:none");
                                BtnSupplierExcelFile.Visible = true;
                                btnFileUpload.Visible = false;
                                btnUploadExcel.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                OledbConn.Close();
                throw new Exception(exp.Message);
            }
        }

        protected void BtnSupplierExcelFile_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();
                DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails_BulkUploadHO(btnFileUpload.FileName, DateTime.Now.ToString("dd/MM/yyyy"));

                string filename = "HoSupplier_BulkUpload_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("BulkOrderUploadHO.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
