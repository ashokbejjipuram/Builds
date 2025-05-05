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
    public partial class BranchItemPriceBulkUploadHO : System.Web.UI.Page
    {
        private string strBranchCode = default(string);
        private string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString();
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Zones oZone = new Zones();
                ddlZone.DataSource = oZone.GetAllZones();
                ddlZone.DataBind();
                ddlZone.Items.Insert(0, "--All--");
                ddlZone.Enabled = true;

                ddlState.Items.Insert(0, "--All--");
                ddlBranch.Items.Insert(0, "--All--");

                hdnRowCount.Value = "0";
                hdnMissingPartNos.Value = "0";
            }

            if (Session["BranchCode"] != null)
                strBranchCode = Session["BranchCode"].ToString();            
        }

        protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlState.Items.Clear();
                chkZone.Checked = false;
                chkState.Checked = false;
                chkBranch.Checked = false;

                if (ddlZone.SelectedIndex > 0)
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetZoneBasedStatesOnline(Convert.ToInt16(ddlZone.SelectedValue));
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlState.Enabled = true;
                    StateMasters oStateMaster = new StateMasters();
                    ddlState.DataSource = oStateMaster.GetAllStatesOnline();
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
        }

        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlBranch.Items.Clear();
                chkZone.Checked = false;
                chkState.Checked = false;
                chkBranch.Checked = false;

                if (ddlState.SelectedIndex > 0)
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchStateOnline(ddlState.SelectedValue);
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
                else
                {
                    ddlBranch.Enabled = true;
                    Branches oBranch = new Branches();
                    ddlBranch.DataSource = oBranch.GetAllBranchNew();
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, "--All--");
                }
            }
            catch (Exception exp)
            {
                IMPALLibrary.Log.WriteException(Source, exp);
            }
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
                    filePath = @"D:\Downloads\BranchItemPriceHO";

                    Session["UploadDetails"] = "";

                    string fileName = btnFileUpload.FileName;

                    if (File.Exists(filePath + "\\" + fileName))
                        File.Delete(filePath + "\\" + fileName);

                    btnFileUpload.SaveAs(filePath + "\\" + fileName);

                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        UploadBIPDetails(filePath, fileName);
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

        protected void UploadBIPDetails(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Database ImpalDb = DataAccess.GetDatabase();

            string ExcelConnectionString = "";
            string sqlTempQuery = "";
            string SheetName = "";
            DbCommand cmd1;

            try
            {
                sqlTempQuery = "truncate table Temp_Branch_Item_Price";
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

                string myexceldataquery = "Select Supplier_Line,PartNo,ListPrice,PurchaseDisc,Coupon,CashDiscount,WHChargPercentage,MRP from [" + SheetName + "]";
                OleDbConnection OledbConn = new OleDbConnection(ExcelConnectionString);
                OleDbCommand OledbCmd = new OleDbCommand(myexceldataquery, OledbConn);
                OledbConn.Open();
                OleDbDataReader dr = OledbCmd.ExecuteReader();
                while (dr.Read())
                {
                    sqlTempQuery = "Insert into Temp_Branch_Item_Price (Supplier_Code,Supplier_Part_No,List_Price,PurDisc_Percentage,Coupon,CD_Percentage,WC_Percentage,MRP) " +
                                   "values ('" + dr[0] + "','" + dr[1] + "'," + dr[2] + "," + dr[3] + "," + dr[4] + "," + dr[5] + "," + dr[6] + "," + dr[7] + ")";

                    cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                    cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDb.ExecuteNonQuery(cmd1);
                }

                cmd1 = null;
                OledbConn.Close();

                sqlTempQuery = "Exec Usp_UpdTemp_Branch_Item_Price";
                cmd1 = ImpalDb.GetSqlStringCommand(sqlTempQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDb.ExecuteDataSet(cmd1);
                cmd1 = null;

                hdnRowCount.Value = ds.Tables[0].Rows.Count.ToString();
                hdnMissingPartNos.Value = ds.Tables[1].Rows[0]["Cnt"].ToString();

                if (Convert.ToInt16(hdnMissingPartNos.Value) <= 0)
                {
                    Session["UploadDetails"] = "<font style='color:green' size='5'><b>Branch Item Price Data has been Uploaded Successfully. Please Submit the File</b></font>";
                    btnSubmit.Enabled = true;
                }
                else
                {
                    if (Convert.ToInt16(hdnRowCount.Value) == Convert.ToInt16(hdnMissingPartNos.Value))
                    {
                        Session["UploadDetails"] = "<font style='color:red' size='5'><b>All the Part Nos Exist in the File are New Addition and to be added in Item Master. Please Check the file Once</b></font>";
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        Session["UploadDetails"] = "<font style='color:red' size='5'><b>Few Part Nos Exists in the File are New Addition and to be added in Item Master. You Can Proceed with Existing Data</b></font>";
                        btnSubmit.Enabled = true;
                    }
                }

                tbluploadFile.Visible = false;
            }
            catch (Exception exp)
            {
                Session["UploadDetails"] = "Error in the Data";
                throw new Exception(exp.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                var zone = ddlZone.SelectedValue;
                var state = ddlState.SelectedValue;
                var branch = ddlBranch.SelectedValue;

                BranchItemPrices BIP = new BranchItemPrices();
                int result = BIP.AddNewBranchItemPriceBulkUploadHO(zone, state, branch);

                if (result == 1)
                {
                    chkZone.Enabled = false;
                    chkState.Enabled = false;
                    chkBranch.Enabled = false;
                    ddlZone.Enabled = false;
                    ddlState.Enabled = false;
                    ddlBranch.Enabled = false; 
                    btnSubmit.Enabled = false;
                    btnReset.Enabled = true;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Branch Item Price Details Updated Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Error in Data');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
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
                Response.Redirect("BranchItemPriceBulkUploadHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }
    }
}