using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using System.Data.Common;
using IMPALLibrary.Transactions;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace IMPALWeb.HOAdmin.Item
{
    public partial class SupplierInvoicesUpload : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString();
        OleDbConnection OledbConn;
        OleDbCommand OledbCmd;
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
            }

            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();            
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            DataTable pdtCashAndBankPaymentExcel = new DataTable();
            try
            {
                UploadFileData(false);
                ShowSuccessMessage(Session["UploadDetails"].ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ShowSuccessMessage(string message)
        {
            lblUploadMessage.Visible = true;
            lblUploadMessage.Text = "<br /><br /><center style='font-size:13px;'><b>" + message + "</center>";
        }

        protected void UploadFileData(bool isHOProcess)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string sStatus = string.Empty;

                if (btnFileUpload.HasFile)
                {
                    filePath = @"D:\Downloads\SupplierDespatch";

                    Session["UploadDetails"] = "";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        UploadSupplierInvoiceDetails(filePath, fileName);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('File doesnt exists');", true);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void UploadSupplierInvoiceDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Database ImpalDb = DataAccess.GetDatabase();

            string ExcelConnectionString = "";
            string sqlTempQuery = "";
            string SheetName = "";
            DbCommand cmd1;

            try
            {
                sqlTempQuery = "truncate table Supplier_Despatches";
                cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDb.ExecuteNonQuery(cmd1);
                cmd1 = null;

                if (fileName.ToString().EndsWith("s"))
                    ExcelConnectionString = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
                else
                    ExcelConnectionString = @"provider=Microsoft.ace.oledb.12.0;data source=" + filePath + "\\" + fileName + ";extended properties=" + "\"excel 12.0 xml;hdr=yes;\"";

                using (OleDbConnection conn = new OleDbConnection(ExcelConnectionString))
                {
                    conn.Open();
                    DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    SheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                    conn.Close();
                }

                string myexceldataquery = "Select * from [" + SheetName + "]";
                OledbConn = new OleDbConnection(ExcelConnectionString);
                OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                OledbConn.Open();
                OleDbDataReader dr = OledbCmd.ExecuteReader();
                int i = 0;

                string[] BICode = { "160", "162", "750", "840", "841" };                
                string[] LucasCode = { "330", "350", "770" };
                //string[] SFCode = { "320", "321", "322", "323", "410", "411", "412", "413", "414" };
                string[] WabcoCode = { "450" };
                string[] ContitechCode = { "470" };
                string[] ValeoCode = { "490" };
                string[] SKFCode = { "550", "555" };
                string[] ExedyCode = { "590" };
                string[] GoGoCode = { "980" };

                while (dr.Read())
                {
                    if (BICode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[10].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches values ('" + ddlSupplierLine.SelectedValue + "','" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "',Convert(varchar(10),'" + dr[5] + "',101)," + dr[6] + ",'" + dr[7] + "',Convert(varchar(10),'" + dr[8] + "',101)," + dr[9] + "," +
                                           "'" + dr[10] + "',NULL," + dr[12] + "," + dr[13] + ",'" + dr[14] + "'," + dr[15] + "," + dr[16] + "," + dr[17] + "," + dr[18] + "," + dr[19] + "," +
                                           "" + dr[20] + "," + dr[21] + "," + dr[22] + "," + dr[23] + "," + dr[24] + "," + dr[25] + "," + dr[26] + ",'" + dr[27] + "',Convert(varchar(10),'" + dr[28] + "',101),'" + dr[29] + "'," +
                                           "Convert(varchar(10),'" + dr[30] + "',101)," + dr[31] + "," + dr[32] + "," + dr[33] + "," + dr[34] + "," + dr[35] + ",'" + dr[36] + "','" + dr[37] + "')";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }
                        else
                        {
                            Exception obj = new Exception();
                            obj = new Exception("Supplier Part No # is not in the Text format to upload for Customer PO Number " + dr[7].ToString() + " and Line Item Number " + dr[9].ToString() + ".Please Check and Modify the Excel File accordingly.");
                            throw obj;
                        }
                    }
                    else if (LucasCode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[11].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches (Supplier_Code,Division,Plant,CustomerCode,CustomerCity,InvoiceNo,Invoicedate,Customerpono,Customerpodate," +
                                       "Lineitemno,Partno,PartDesc,Partquantity,UOM,PartMrp,PartListprice,DiscountPercentage,DiscountAmount,ItemTotalValue,ItemBaseValue,LrNo,LRDate,Ewaybillno,Ewaybilldate,TptName) " +
                                       "values ('" + ddlSupplierLine.SelectedValue + "','IMPAL',trim('" + dr[0] + "'),'" + dr[1] + "','" + dr[2] + "',trim('" + dr[3] + "'),trim(Convert(varchar(10),'" + dr[4] + "',101)),trim('" + dr[6] + "'),trim(Convert(varchar(10),'" + dr[7] + "',101))," +
                                       "" + (i + 1) + ",trim('" + dr[11] + "'),trim('" + dr[12] + "')," + dr[14] + ",'PCS'," + dr[18] + "," + dr[15] + ",0,0,0," + dr[16] + ",'" + dr[20] + "','" + dr[21] + "','" + dr[22] + "','" + dr[23] + "',trim('" + dr[19] + "'))";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);

                            i++;
                        }
                        else
                        {
                            Exception obj = new Exception();
                            obj = new Exception("Supplier Part No # is not in the Text format to upload for Customer PO Number " + dr[16].ToString() + ".Please Check and Modify the Excel File accordingly.");
                            throw obj;
                        }
                    }
                    else if (WabcoCode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[0].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches (Supplier_Code,Division,Plant,CustomerCode,CustomerCity,InvoiceNo,Invoicedate,Invoiceamount,Customerpono,Customerpodate,Lineitemno," +
                                           "Partno,PartDesc,Partquantity,CgstAmount,SgstAmount,IgstAmount,ItemTotalValue,ItemBaseValue,LrNo,LRDate,TptName) " +
                                           "values('" + ddlSupplierLine.SelectedValue + "','IMPAL','" + dr[3] + "','" + dr[0] + "','" + dr[1] + "-" + dr[2] + "','" + dr[17] + "',Replace('" + dr[18] + "','.','/'),0,Replace('" + dr[4] + "','.','/'),Replace('" + dr[5] + "','.','/')," + (i + 1) + "," +
                                           "'" + dr[6] + "','" + dr[7] + "'," + dr[11] + "," + dr[14] + "," + dr[16] + "," + dr[15] + ",0," + dr[12] + ",'" + dr[9] + "',Replace('" + dr[10] + "','.','/'),'" + dr[8] + "')";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }

                        i++;
                    }
                    else if (ContitechCode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[13].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches (Supplier_Code,Division,Plant,CustomerCode,CustomerCity,InvoiceNo,Invoicedate,Invoiceamount,Customerpono,Lineitemno," +
                                           "Partno,PartDesc,Partquantity,PartListprice,IgstPercentage,IgstAmount,CgstPercentage,CgstAmount,SgstPercentage,SgstAmount,ItemTotalValue,ItemBaseValue,Hsncode,LrNo,LRDate,TptName) " +
                                           "values('" + ddlSupplierLine.SelectedValue + "','IMPAL','" + dr[2] + "','" + dr[8] + "','" + dr[9] + "','" + dr[5] + "',convert(varchar(10),'" + dr[6] + "',103)," + dr[25] + ",'" + dr[28] + "'," + dr[0] + "," +
                                           "'" + dr[14] + "','" + dr[26] + "'," + dr[15] + "," + dr[17] + "," + dr[18] + "," + dr[19] + "," + dr[20] + "," + dr[21] + "," + dr[22] + "," + dr[23] + "," + dr[24] + "," + dr[16] + ",'" + dr[27] + "','" + dr[29] + "',convert(varchar(10),'" + dr[30] + "',103),'" + dr[31] + "')";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }

                        i++;
                    }
                    else if (ValeoCode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[10].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches values ('" + ddlSupplierLine.SelectedValue + "','IMPAL','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "',Convert(varchar(10),'" + dr[5] + "',101)," + dr[6] + ",'" + dr[7] + "',Convert(varchar(10),'" + dr[8] + "',101)," + dr[9] + "," +
                                           "'" + dr[10] + "',NULL," + dr[12] + "," + dr[13] + ",'" + dr[14] + "'," + dr[15] + "," + dr[16] + "," + dr[17] + "," + dr[18] + "," + dr[19] + "," +
                                           "" + dr[20] + "," + dr[21] + "," + dr[22] + "," + dr[23] + "," + dr[24] + "," + dr[25] + "," + dr[26] + ",'" + dr[27] + "',Convert(varchar(10),'" + dr[28] + "',101),'" + dr[29] + "'," +
                                           "Convert(varchar(10),'" + dr[30] + "',101)," + dr[31] + "," + dr[32] + "," + dr[33] + "," + dr[34] + "," + dr[35] + ",'" + dr[36] + "','" + dr[37] + "')";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }
                        else
                        {
                            if (dr[1].ToString() == "" && dr[10].ToString() == "")
                            {
                            }
                            else
                            {
                                Exception obj = new Exception();
                                obj = new Exception("Supplier Part No # is not in the Text format to upload for Customer PO Number " + dr[7].ToString() + " and Line Item Number " + dr[9].ToString() + ".Please Check and Modify the Excel File accordingly.");
                                throw obj;
                            }
                        }
                    }
                    else if (GoGoCode.Contains(ddlSupplierLine.SelectedValue))
                    {
                        if (dr[10].ToString() != "")
                        {
                            sqlTempQuery = "Insert into Supplier_Despatches (Supplier_Code,Division,Plant,CustomerCode,CustomerCity,InvoiceNo,Invoicedate,Invoiceamount,Customerpono,Customerpodate,Lineitemno,Partno,PartDesc,Hsncode,Partquantity,UOM,PartMrp,PartListprice,DiscountPercentage,DiscountAmount," +
                                           "CgstPercentage,CgstAmount,SgstPercentage,SgstAmount,IgstPercentage,IgstAmount,ItemTotalValue,ItemBaseValue,LrNo,LRDate,Ewaybillno,Ewaybilldate,TptGstNo,TptName) values ('" + ddlSupplierLine.SelectedValue + "','" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "','" + dr[5] + "'," + dr[6] + ",'" + dr[7] + "','" + dr[8] + "'," + dr[9] + "," +
                                           "'" + dr[10] + "',NULL," + dr[12] + "," + dr[13] + ",'" + dr[14] + "'," + dr[15] + "," + dr[16] + "," + dr[17] + "," + dr[18] + "," + dr[19] + "," + dr[20] + "," + dr[21] + "," + dr[22] + "," + dr[23] + "," + dr[24] + "," + dr[25] + "," +
                                           "" + dr[26] + ",'" + dr[27] + "','" + dr[28] + "','" + dr[29] + "','" + dr[30] + "','" + dr[36] + "','" + dr[37] + "')";

                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }

                        i++;
                    }
                }

                cmd1 = null;
                OledbConn.Close();

                cmd1 = ImpalDb.GetStoredProcCommand("Usp_UpdTemp_Supplier_Despatches");
                ImpalDb.AddInParameter(cmd1, "@Supplier_Code", DbType.String, ddlSupplierLine.SelectedValue);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDb.ExecuteDataSet(cmd1);
                cmd1 = null;

                int InsCnt = Convert.ToInt16(ds.Tables[0].Rows[0]["InsertCount"].ToString());
                int UplCnt = Convert.ToInt16(ds.Tables[0].Rows[0]["UploadCount"].ToString());

                if (InsCnt > 0)
                {
                    if (InsCnt == UplCnt)
                        Session["UploadDetails"] = "<font style='color:green' size='5'><b>All the Supplier Invoice Details have been Uploaded Successfully.</b></font>";
                    else
                        Session["UploadDetails"] = "<font style='color:green' size='5'><b>Supplier Invoice Details have been Uploaded Successfully and the Inserted Records are " + InsCnt + "</b></font>";
                }
                else
                {
                    Session["UploadDetails"] = "<font style='color:red' size='5'><b>No New Data Exists. Please Check the file Once</b></font>";
                }

                ddlSupplierLine.Enabled = false;
                btnFileUpload.Enabled = false;
                btnUploadExcel.Visible = false;
            }
            catch (Exception exp)
            {
                Session["UploadDetails"] = "Error in the Data";
                OledbConn.Close();
                throw new Exception(exp.Message);
            }
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("SupplierInvoicesUpload.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
    }
}