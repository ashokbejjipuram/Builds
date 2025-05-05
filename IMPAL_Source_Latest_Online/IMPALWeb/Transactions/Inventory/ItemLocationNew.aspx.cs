using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary;
using IMPALLibrary.Transactions;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace IMPALWeb.Transactions.Inventory
{
    public partial class ItemLocationNew : System.Web.UI.Page
    {
        ItemLocationDetails ItemLoc = new ItemLocationDetails();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtSupplierPartNo.Visible = true;
                    ddlSupplierPartNo.Items.Clear();
                    ddlSupplierPartNo.Visible = false;

                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnReport.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void ddlSupplierName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplierName.SelectedIndex > 0)
                {
                    BtnReport.Visible = true;                    
                }
                else
                {
                    BtnReport.Visible = false;
                }

                txtSupplierPartNo.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnReport.Visible = false;

                if (btnSearch.Text == "Reset")
                {                    
                    txtSupplierPartNo.Text = "";
                    ddlSupplierPartNo.Items.Clear();
                    ddlSupplierPartNo.Visible = false;
                    txtSupplierPartNo.Visible = true;
                    txtLocation.Text = "";
                    btnSearch.Text = "Search";
                }
                else
                {
                    if (txtSupplierPartNo != null)
                    {
                        StockTransferTransactions stTransactions = new StockTransferTransactions();
                        ddlSupplierPartNo.DataSource = (object)stTransactions.GetItemLocationSupplierPartNos(ddlSupplierName.SelectedValue, txtSupplierPartNo.Text, ddlBranch.SelectedValue.ToString());
                        ddlSupplierPartNo.DataTextField = "ItemDesc";
                        ddlSupplierPartNo.DataValueField = "ItemCode";
                        ddlSupplierPartNo.DataBind();

                        ddlSupplierPartNo.Visible = true;
                        txtSupplierPartNo.Visible = false;
                        btnSearch.Text = "Reset";
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void ddlSupplierPartNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StockTransferTransactions stockTransferTransactions = new StockTransferTransactions();
                string itmLoc = stockTransferTransactions.GetItemLocation(ddlBranch.SelectedValue, ddlSupplierPartNo.SelectedValue);
                
                if (itmLoc != "0")
                {
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = true;
                    BtnReport.Visible = false;
                    txtLocation.Text = itmLoc;
                }
                else
                {
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    BtnReport.Visible = false;
                    txtLocation.Text = "";
                }

                txtLocation.Focus();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(StockTransferEntryEDP), exp);
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("ItemLocationNew.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "ItemLocation_" + ddlSupplierName.SelectedValue + "_" + ddlBranch.SelectedValue + ".xls";

                ViewState["JSonExcelData"] = "";
                Database ImpalDB = DataAccess.GetDatabase();

                DataSet ds = new DataSet();
                DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_CreateItemLocationExcel");
                ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, ddlBranch.SelectedValue);
                ImpalDB.AddInParameter(dbcmd, "@Supplier_Code", DbType.String, ddlSupplierName.SelectedValue);
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

                    hdnViewState.Value = serializer.Serialize(root);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnReport.Visible = false;

                btnSearch.Visible = false;
                ddlSupplierName.Enabled = false;
                ddlSupplierPartNo.Enabled = false;
                txtLocation.Enabled = false;

                ItemLoc.AddNewItemLocationNew(ddlSupplierPartNo.SelectedValue, ddlBranch.SelectedValue, txtLocation.Text);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item Location has been added successfully');", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Visible = false;
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnReport.Visible = false;

                ddlSupplierName.Enabled = false;
                ddlSupplierPartNo.Enabled = false;
                txtLocation.Enabled = false;

                ItemLoc.UpdateItemLocationNew(ddlSupplierPartNo.SelectedValue, ddlBranch.SelectedValue, txtLocation.Text);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Item Location has been updated successfully');", true);

            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(ItemLocationNew), exp);
            }
        }

        private void GetAllSuppliers()
        {
            Suppliers suppliers = new Suppliers();
            List<Supplier> lstSuppliers = suppliers.GetAllSuppliers();
        }
    }
}
