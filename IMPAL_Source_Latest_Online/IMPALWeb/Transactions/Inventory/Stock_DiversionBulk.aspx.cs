using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory
{
    public partial class Stock_DiversionBulk : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                
                if (!IsPostBack)
                {
                    lblError.Text = "";
                    BindTransactionType();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp);                
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Server.ClearError();
                Response.Redirect("Stock_DiversionBulk.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp);
            }
        }
       
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            int RowCount = 0;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    StockDiversionItems objSection = new StockDiversionItems();
                    objSection.Supplier_Line = ddlSupplierName.SelectedValue;
                    objSection.From_TransactionType = ddlFromTransactionType.SelectedValue;
                    objSection.To_TransactionType = ddlToTransactionType.SelectedValue;                    

                    StockDiversion objSaveStockAdjustment = new StockDiversion();
                    RowCount = objSaveStockAdjustment.UpdStockDiversionBulk(objSection, Session["BranchCode"].ToString());
                    BtnSubmit.Enabled = false;
                    ddlSupplierName.Enabled = false;
                    ddlFromTransactionType.Enabled = false;
                    ddlToTransactionType.Enabled = false;

                    if (RowCount > 0)
                    {
                        lblError.Text = "Updated Successfully.";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblError.Text = "No Records Exists.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp);
            }
        }

        private void AddSelect(DropDownList ddl)
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = " ";
                li.Value = "0";
                ddl.Items.Insert(0, li);
                ddl.SelectedValue = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp); 
            }
        }

        private void BindTransactionType()
        {
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.ddlTransactionType";
                    obj.TypeName = "IMPALLibrary.StockDiversion";
                    obj.SelectMethod = "GetAllStockTransactionTypes";
                    obj.DataBind();
                    ddlToTransactionType.DataSource = obj;
                    ddlToTransactionType.DataTextField = "Transaction_Type_Description";
                    ddlToTransactionType.DataValueField = "Transaction_Type_Code";
                    ddlToTransactionType.DataBind();
                    AddSelect(ddlToTransactionType);

                    ddlFromTransactionType.DataSource = obj;
                    ddlFromTransactionType.DataTextField = "Transaction_Type_Description";
                    ddlFromTransactionType.DataValueField = "Transaction_Type_Code";
                    ddlFromTransactionType.DataBind();
                    AddSelect(ddlFromTransactionType);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Stock_DiversionBulk), exp); 
            }
        }               
    }     
}