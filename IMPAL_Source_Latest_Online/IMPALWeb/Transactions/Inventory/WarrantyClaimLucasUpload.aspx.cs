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

namespace IMPALWeb
{
    public partial class WarrantyClaimLucasUpload : System.Web.UI.Page
    {
        private string serverPathMode = System.Configuration.ConfigurationManager.AppSettings["ServerPathMode"].ToString();
        private string filePath = string.Empty;
        private string fileName = string.Empty;
        OleDbConnection OledbConn;
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(WarrantyClaimLucasUpload), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
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
                    filePath = @"D:\Downloads\WarrantyClaimLucas";

                    string filePathDest = @"D:\Downloads\WarrantyClaimLucas\Old";

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
                        sqlTotQuery = "Insert into Warranty_Claim_Lucas_Upload_Data_BackUp Select * from Warranty_Claim_Lucas_Upload_Data; Truncate table Warranty_Claim_Lucas_Upload_Data";
                        DbCommand cmd1 = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                        cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDb.ExecuteNonQuery(cmd1);
                        cmd1 = null;

                        while (dr.Read())
                        {
                            sqlTotQuery = "Insert into Warranty_Claim_Lucas_Upload_Data ([Card-Code],[Job-No],[Job-Date],[Rec-Type],[Qty],[Registration],[Engine-No],[Chassis-No],[Vehicle-Make],[Unit-Type]," +
                                "[Prod-Year-Month],[Dealer-Code],[Part-No],[Defect-Code],[Cause-Code],[Customer-Code],[Kilometers],[Date-of-Commission],[Date-of-Failure],[Date-Of-Complaint],[Labour-Hour]," +
                                "[Material-Cost],[Labour-Cost],[Handling-Cost],[Total_cost],[Claim-Settlement],[Return-Year-Month],[Repo-Week],[Depo-Code],[Prod-Year-Week],[Prefail],[Trep1],[Trep2],[Lisopdt]," +
                                "[Liscldt],[F1],[deler_jobno],[Lab_inv_no],[Lab_inv_date],[lab_inv_value],[Lab_SAC],[Lab_SGST_Perc],[Lab_SGST_Val],[Lab_CGST_Perc],[Lab_CGST_Val],[Lab_IGST_Perc],[Lab_IGST_Val]," +
                                "[Mat_inv_no],[Mat_inv_date],[Mat_HSN],[Mat_SGST_Perc],[Mat_SGST_Val],[Mat_CGST_Perc],[Mat_CGST_Val],[Mat_IGST_Perc],[Mat_IGST_Val],[Hld_inv_no],[Hld_inv_date],[Hld_SAC],[Hld_SGST_Perc]," +
                                "[Hld_SGST_Val],[Hld_CGST_Perc],[Hld_CGST_Val],[Hld_IGST_Perc],[Hld_IGST_Val],[Lab_inv_tot_value],[Mat_inv_tot_value],[Hld_inv_tot_value],[ltvs_plant_code])" +
                                "values ('" + dr[0] + "','" + dr[1] + "',Convert(varchar(8),'" + dr[2] + "',103),'" + dr[3] + "','" + dr[4] + "','" + dr[5] + "','" + dr[6] + "','" + dr[7] + "','" + dr[8] + "','" + dr[9] + "','" +
                                dr[10] + "','" + dr[11] + "','" + dr[12] + "','" + dr[13] + "','" + dr[14] + "','" + dr[15] + "','" + dr[16] + "','" + dr[17] + "','" + dr[18] + "','" + dr[19] + "','" +
                                dr[20] + "','" + dr[21] + "','" + dr[22] + "','" + dr[23] + "','" + dr[24] + "','" + dr[25] + "','" + dr[26] + "','" + dr[27] + "','" + dr[28] + "','" + dr[29] + "','" +
                                dr[30] + "','" + dr[31] + "','" + dr[32] + "','" + dr[33] + "','" + dr[34] + "','" + dr[35] + "','" + dr[36] + "','" + dr[37] + "',Convert(varchar(8),'" + dr[38] + "',103),'" + dr[39] + "','" +
                                dr[40] + "','" + dr[41] + "','" + dr[42] + "','" + dr[43] + "','" + dr[44] + "','" + dr[45] + "','" + dr[46] + "','" + dr[47] + "',Convert(varchar(8),'" + dr[48] + "',103),'" + dr[49] + "','" +
                                dr[50] + "','" + dr[51] + "','" + dr[52] + "','" + dr[53] + "','" + dr[54] + "','" + dr[55] + "','" + dr[56] + "',Convert(varchar(8),'" + dr[57] + "',103),'" + dr[58] + "','" + dr[59] + "','" +
                                dr[60] + "','" + dr[61] + "','" + dr[62] + "','" + dr[63] + "','" + dr[64] + "','" + dr[65] + "','" + dr[66] + "','" + dr[67] + "','" + dr[68] + "')";
                            cmd1 = ImpalDb.GetSqlStringCommand(sqlTotQuery);
                            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDb.ExecuteNonQuery(cmd1);
                        }

                        OledbConn.Close();

                        if (fileName == "" || fileName == null)
                        {
                            FileStatusMsg.Text = "<font style='color: red' size='3'><b>File Doesn't Exist. Please Check the Same</b></font>";
                            divItemDetailsExcel.Attributes.Add("style", "display:inline");
                        }
                        else
                        {
                            if (File.Exists(filePath + "\\" + fileName))
                            {
                                FileStatusMsg.Text = "<font style='color: Red' size='4'><b>Warranty Claim Lucas File Has been Uploaded.</b></font>";
                                row1.Attributes.Add("style", "display:none");
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("WarrantyClaimLucasUpload.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
