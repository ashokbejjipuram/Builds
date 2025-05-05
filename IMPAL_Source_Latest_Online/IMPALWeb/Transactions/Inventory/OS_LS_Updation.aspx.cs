using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.Transactions.Inventory
{
    public partial class OS_LSUpdation : System.Web.UI.Page
    {
        #region event methods

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (!IsPostBack)
                {
                    BindInward_STDNVlaue("G");
                    AddSelect(ddlSupplierPartNo);
                    AddSelect(ddlSerialNo);
                    BtnSubmit.Enabled = false;
                    TRSerialNo.Visible = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void ddlUpdationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindInward_STDNVlaue(ddlUpdationType.SelectedValue);
                BtnSubmit.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void ddlInward_STDNNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInward_STDNNo.SelectedItem.Value == "0")
                {
                    if (ddlSerialNo.Visible == true)
                    {
                        ddlSerialNo.SelectedValue = "0";
                        ddlSerialNo.Enabled = false;
                    }

                    ddlSupplierPartNo.SelectedValue = "0";
                    ddlOS_LS.SelectedValue = "0";
                    ddlSupplierPartNo.Enabled = false;
                    ddlOS_LS.Enabled = false;
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    BindItemCode_SerialNo(ddlUpdationType.SelectedValue);

                    if (ddlSerialNo.Visible == true)
                    {
                        ddlSerialNo.SelectedValue = "0";
                        ddlSerialNo.Enabled = true;
                    }
                    else
                    {
                        ddlSupplierPartNo.Enabled = true;
                    }

                    ddlOS_LS.Enabled = false;
                    BtnSubmit.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void ddlSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSerialNo.SelectedItem.Value == "0")
                {
                    ddlSupplierPartNo.SelectedValue = "0";
                    ddlOS_LS.SelectedValue = "0";
                    ddlSupplierPartNo.Enabled = false;
                    ddlOS_LS.Enabled = false;
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                        obj.TypeName = "IMPALLibrary.OSLSUpdation";
                        obj.SelectMethod = "GetItemCode";
                        obj.SelectParameters.Add("strInwardNumber", ddlInward_STDNNo.SelectedValue);
                        obj.SelectParameters.Add("strSTDN_number", ddlInward_STDNNo.SelectedValue);
                        obj.SelectParameters.Add("strSerialNo", ddlSerialNo.SelectedValue);
                        obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                        obj.DataBind();
                        ddlSupplierPartNo.DataSource = obj;
                        ddlSupplierPartNo.DataTextField = "Supplier_Part_Number";
                        ddlSupplierPartNo.DataValueField = "Item_Code";
                        ddlSupplierPartNo.DataBind();
                        AddSelect(ddlSupplierPartNo);
                        ddlSupplierPartNo.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void ddlSupplierPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplierPartNo.SelectedItem.Value == "0")
                {
                    ddlOS_LS.SelectedValue = "0";
                    ddlOS_LS.Enabled = false;
                    BtnSubmit.Enabled = false;
                }
                else
                {
                    if (ddlUpdationType.SelectedValue == "S")
                    {
                        using (ObjectDataSource obj = new ObjectDataSource())
                        {
                            obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                            obj.TypeName = "IMPALLibrary.OSLSUpdation";
                            obj.SelectMethod = "ChkItemCode";
                            obj.SelectParameters.Add("strUpdationType", ddlUpdationType.SelectedValue);
                            obj.SelectParameters.Add("strInwardNumber", ddlInward_STDNNo.SelectedValue);
                            obj.SelectParameters.Add("strSerialNo", ddlSerialNo.SelectedValue);
                            obj.SelectParameters.Add("strItemCode", ddlSupplierPartNo.SelectedItem.Value);
                            obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                            obj.DataBind();

                            ConsignmentHeaderSummary objSection = new ConsignmentHeaderSummary();
                            object[] objCustomerSections = new object[0];
                            objCustomerSections = (object[])obj.Select();
                            objSection = (ConsignmentHeaderSummary)objCustomerSections[0];

                            if (objSection.iRowCount > 0)
                            {
                                ChkAndBindOSLS();

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Item already released from consignment.');", true);
                                ddlSerialNo.SelectedValue = "0";
                                ddlSupplierPartNo.SelectedValue = "0";
                            }
                        }
                    }
                    else
                    {
                        ChkAndBindOSLS();
                        BtnSubmit.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                OSLSUpdation objOSLSUpdate = new OSLSUpdation();
                int updRowCount = objOSLSUpdate.upd_OSLSUpdation(ddlUpdationType.SelectedValue, ddlInward_STDNNo.SelectedValue, ddlSupplierPartNo.SelectedItem.Value, ddlOS_LS.SelectedValue, Session["BranchCode"].ToString());

                ddlUpdationType.SelectedValue = "G";
                BindInward_STDNVlaue("G");
                ddlInward_STDNNo.SelectedValue = "0";
                if (ddlSerialNo.Visible == true)
                {
                    ddlSerialNo.SelectedValue = "0";
                    ddlSerialNo.Enabled = false;
                }

                ddlSupplierPartNo.SelectedValue = "0";
                ddlOS_LS.SelectedValue = "0";
                ddlSupplierPartNo.Enabled = false;
                ddlOS_LS.Enabled = false;
                BtnSubmit.Enabled = false;
                lblError.Text = "Updated Successfully";
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlInward_STDNNo.SelectedValue = "0";
                ddlSupplierPartNo.SelectedValue = "0";
                ddlSupplierPartNo.Enabled = false;
                BtnSubmit.Enabled = false;

                if (ddlSerialNo.Visible == true)
                {
                    ddlSerialNo.SelectedValue = "0";
                    ddlSerialNo.Enabled = false;
                }

                ddlOS_LS.SelectedValue = "0";
                ddlOS_LS.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        #endregion

        #region user methods

        private void ChkAndBindOSLS()
        {
            try
            {
                using (ObjectDataSource obj1 = new ObjectDataSource())
                {

                    obj1.DataObjectTypeName = "IMPALLibrary.OSLSSerialNo";
                    obj1.TypeName = "IMPALLibrary.OSLSUpdation";
                    obj1.SelectMethod = "GetLSOS";
                    obj1.SelectParameters.Add("strUpdationType", ddlUpdationType.SelectedValue);
                    obj1.SelectParameters.Add("strInwardNumber", ddlInward_STDNNo.SelectedValue);
                    obj1.SelectParameters.Add("strItemCode", ddlSupplierPartNo.SelectedItem.Value);
                    obj1.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                    if (ddlUpdationType.SelectedValue == "S")
                    {
                        obj1.SelectParameters.Add("strSerialNo", ddlSerialNo.SelectedValue);
                    }
                    else
                    {
                        obj1.SelectParameters.Add("strSerialNo", "0");
                    }

                    obj1.DataBind();

                    OSLSSerialNo objSection1 = new OSLSSerialNo();
                    object[] objCustomerSections1 = new object[0];
                    objCustomerSections1 = (object[])obj1.Select();
                    objSection1 = (OSLSSerialNo)objCustomerSections1[0];
                    if ((objSection1.OSLS_SerialNumber is object))
                    {
                        ddlOS_LS.SelectedValue = objSection1.OSLS_SerialNumber;
                    }
                }

                BtnSubmit.Enabled = true;
                ddlOS_LS.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }

        }

        private void AddSelect(DropDownList ddl)
        {
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
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        private void BindInward_STDNVlaue(string strUpdationType)
        {
            try
            {
                ddlInward_STDNNo.Items.Clear();

                if (strUpdationType == "G")
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {

                        obj.DataObjectTypeName = "IMPALLibrary.InwardNumber";
                        obj.TypeName = "IMPALLibrary.OSLSUpdation";
                        obj.SelectMethod = "GetInwardNumber";
                        obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                        obj.DataBind();
                        ddlInward_STDNNo.DataSource = obj;
                        ddlInward_STDNNo.DataTextField = "Inward_Number";
                        ddlInward_STDNNo.DataValueField = "Inward_Number";
                        ddlInward_STDNNo.DataBind();
                        AddSelect(ddlInward_STDNNo);
                    }

                    ddlSerialNo.Visible = false;
                    lblSerialNo.Visible = false;
                    TRSerialNo.Visible = false;

                    ddlSupplierPartNo.SelectedValue = "0";
                    ddlOS_LS.SelectedValue = "0";
                    ddlSupplierPartNo.Enabled = false;
                    ddlOS_LS.Enabled = false;
                }
                else
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        obj.DataObjectTypeName = "IMPALLibrary.GoodsInTransitTransferSTDNNumber";
                        obj.TypeName = "IMPALLibrary.OSLSUpdation";
                        obj.SelectMethod = "GetSTDNNumber";
                        obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                        obj.DataBind();
                        ddlInward_STDNNo.DataSource = obj;
                        ddlInward_STDNNo.DataTextField = "STDNNumber";
                        ddlInward_STDNNo.DataValueField = "STDNNumber";
                        ddlInward_STDNNo.DataBind();
                        AddSelect(ddlInward_STDNNo);
                    }

                    ddlSerialNo.Visible = true;
                    lblSerialNo.Visible = true;
                    TRSerialNo.Visible = true;

                    ddlSerialNo.SelectedValue = "0";
                    ddlSupplierPartNo.SelectedValue = "0";
                    ddlOS_LS.SelectedValue = "0";
                    ddlSerialNo.Enabled = false;
                    ddlSupplierPartNo.Enabled = false;
                    ddlOS_LS.Enabled = false;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        private void BindItemCode_SerialNo(string strUpdationType)
        {
            try
            {
                if (strUpdationType == "G")
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        obj.DataObjectTypeName = "IMPALLibrary.ConsignmentItemCode";
                        obj.TypeName = "IMPALLibrary.OSLSUpdation";
                        obj.SelectMethod = "GetItemCode";
                        obj.SelectParameters.Add("strInwardNumber", ddlInward_STDNNo.SelectedValue);
                        obj.SelectParameters.Add("strSTDN_number", ddlInward_STDNNo.SelectedValue);
                        obj.SelectParameters.Add("strSerialNo", "00");
                        obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                        obj.DataBind();
                        ddlSupplierPartNo.DataSource = obj;
                        ddlSupplierPartNo.DataTextField = "Supplier_Part_Number";
                        ddlSupplierPartNo.DataValueField = "Item_Code";
                        ddlSupplierPartNo.DataBind();
                        AddSelect(ddlSupplierPartNo);
                    }
                }
                else
                {
                    using (ObjectDataSource obj = new ObjectDataSource())
                    {
                        obj.DataObjectTypeName = "IMPALLibrary.OSLSSerialNo";
                        obj.TypeName = "IMPALLibrary.OSLSUpdation";
                        obj.SelectMethod = "GetSerialNumber";
                        obj.SelectParameters.Add("strSTDN_number", ddlInward_STDNNo.SelectedValue);
                        obj.SelectParameters.Add("strBranchCode", Session["BranchCode"].ToString());
                        obj.DataBind();
                        ddlSerialNo.DataSource = obj;
                        ddlSerialNo.DataTextField = "OSLS_SerialNumber";
                        ddlSerialNo.DataValueField = "OSLS_SerialNumber";
                        ddlSerialNo.DataBind();
                        AddSelect(ddlSerialNo);
                    }

                    ddlSupplierPartNo.SelectedValue = "0";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(OS_LSUpdation), exp);
            }
        }

        #endregion
    }
}