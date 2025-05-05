using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using IMPALLibrary;
using log4net;
using System.Collections;
using ClosedXML.Excel;
using System.IO;

namespace IMPALWeb.Ordering
{
    public partial class Ord_DirectPurchaseOrderHO : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        string strOrdBranchCode;

        #region system event methods

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(Ord_DirectPurchaseOrderHO), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (ViewState["strOrdBranchCode"] != null)
                    strOrdBranchCode = ViewState["strOrdBranchCode"].ToString();

                if (!IsPostBack)
                {
                    strOrdBranchCode = Session["BranchCode"].ToString();
                    ViewState["strOrdBranchCode"] = strOrdBranchCode.ToString();

                    ddlOrdBranchName.Visible = true;
                    BtnSubmit.Enabled = false;
                    ddlOrdTransactionType.Enabled = false;
                    ddlOrdSupplier.Enabled = false;
                    BtnBranchExcelFile.Visible = false;
                    BtnSupplierExcelFile.Visible = false;

                    InitializeControl();

                    ddlOrdSupplier.Attributes.Add("OnChange", "return true;");
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "EnableDataChanges();", true);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrd_PONumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strDirectPONumber = ddlOrd_PONumber.SelectedValue;
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.DirectPOView";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "Get_DirectPOHeaderViewMode";
                    obj.SelectParameters.Add("strPO_Number", strDirectPONumber);
                    obj.SelectParameters.Add("strIndicator", "H");
                    obj.SelectParameters.Add("strBranchCode", ddlOrdBranchName.SelectedValue);
                    obj.DataBind();

                    DirectPOView objSection = new DirectPOView();
                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();
                    objSection = (DirectPOView)objCustomerSections[0];
                    if ((objSection.PO_Number is object))
                    {
                        if (Convert.ToDecimal(objSection.PO_Value)<=0)
                        {                            
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('This PO # " + objSection.PO_Number + " is Having Zero PO Value.');", true);
                            ResetToEmpty();
                            BtnSubmit.Enabled = false;
                            GridView1.Enabled = false;
                            return;
                        }
                        else
                        {
                            BtnSubmit.Enabled = true;
                            GridView1.Enabled = true;
                        }

                        ddlOrd_PONumber.SelectedValue = objSection.PO_Number.ToString();
                        ddlOrdTransactionType.SelectedValue = objSection.Transaction_Type_Code.ToString();

                        if (objSection.Customer_Code.ToString() != "")
                        {
                            lblCustomer.Visible = true;
                            ddlOrdCustomer.Visible = true;
                            BindAllCustomers(strOrdBranchCode);
                            ddlOrdCustomer.SelectedValue = objSection.Customer_Code.ToString();
                        }
                        else
                        {
                            lblCustomer.Visible = false;
                            ddlOrdCustomer.Visible = false;
                        }

                        //txtOrdBranch.Text = objSection.Branch_Name.ToString();
                        ddlOrdSupplier.SelectedValue = objSection.Supplier_Code.ToString();
                        txtOrdIndentDate.Text = objSection.po_date.ToString();
                        txtPOType.Text = objSection.PO_Type.ToString();
                        txtPOValue.Text = TwoDecimalConversion(objSection.PO_Value.ToString());
                        txtOrdCustomerCode.Text = objSection.Customer_Code.ToString();
                        txtOrdCustomerAddress1.Text = objSection.Address1.ToString();
                        txtOrdCustomerAddress2.Text = objSection.Address2.ToString();
                        // txtOrdTin_Number.Text = objSection.Tin_No.ToString();
                        txtOrdAddress3.Text = objSection.Address3.ToString();
                        txtOrdCustomerAddress4.Text = objSection.Address4.ToString();
                        txtOrdCustomerLocation.Text = objSection.Location.ToString();
                        DisableControls(false);
                        BindGridItems();
                    }
                    else
                    {
                        ResetToEmpty();
                    }

                    BtnSubmit.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ImgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ImgButtonQuery.Visible = false;
                BindOrdPO_Number();
                ddlOrd_PONumber.Visible = true;
                ddlOrdTransactionType.Enabled = false;
                ddlOrdSupplier.Enabled = false;
                BtnSubmit.Enabled = false;
                txtOrd_PONumber.Visible = false;
                GridView1.ShowFooter = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrdSupplier_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlOrdTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if ((ddlOrdTransactionType.SelectedValue == "361") || (ddlOrdTransactionType.SelectedValue == "461") || (ddlOrdTransactionType.SelectedValue == "451") || (ddlOrdTransactionType.SelectedValue == "361") || (ddlOrdTransactionType.SelectedValue == "461") || (ddlOrdTransactionType.SelectedValue == "451"))
                {
                    lblCustomer.Visible = true;
                    ddlOrdCustomer.Visible = true;
                    ddlOrdCustomer.Enabled = true;
                    ddlOrdCustomer.SelectedValue = "0";
                }
                else
                {
                    lblCustomer.Visible = false;
                    ddlOrdCustomer.Visible = false;
                }

                txtOrdCustomerAddress1.Text = "";
                txtOrdCustomerAddress2.Text = "";
                txtOrdCustomerAddress4.Text = "";
                txtOrdCustomerCode.Text = "";
                txtOrdAddress3.Text = "";
                txtOrdCustomerLocation.Text = "";

                BindGridItemsFooter();
                UpdatePanel1.Update();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                FillOrdCustomer();
                BindGridItemsFooter();
                if (GridView1.FooterRow != null)
                {
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlOrdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlOrdBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ViewState["CurrentTable"] = null;

                if (ddlOrdBranchName.SelectedIndex > 0)
                {
                    strOrdBranchCode = ddlOrdBranchName.SelectedValue;
                    ViewState["strOrdBranchCode"] = strOrdBranchCode.ToString();
                    BindOrdPO_Number();
                    ddlOrd_PONumber.SelectedIndex = 0;
                }

                ResetToEmpty();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranchesSouthZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grdRow = ((GridViewRow)ddl.Parent.Parent);
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtOrdItemCode");
                DropDownList ddlSouthBranches = (DropDownList)grdRow.FindControl("ddlBranchesSouthZone");
                TextBox txtQtySouthZone = (TextBox)grdRow.FindControl("txtQtySouthZone");

                if (ddlSouthBranches.SelectedIndex > 0)
                {
                    //DirectPurchaseOrders DirPO = new DirectPurchaseOrders();
                    //int stock = DirPO.GetIndentBranchesStock(ddlSouthBranches.SelectedValue, txtItemCode.Text, 1);

                    string[] stock = ddlSouthBranches.SelectedItem.Text.Split('-');
                    txtQtySouthZone.Text = stock[1].Trim().ToString();
                }
                else
                {
                    txtQtySouthZone.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranchesNorthZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grdRow = ((GridViewRow)ddl.Parent.Parent);
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtOrdItemCode");
                DropDownList ddlNorthBranches = (DropDownList)grdRow.FindControl("ddlBranchesNorthZone");
                TextBox txtQtyNorthZone = (TextBox)grdRow.FindControl("txtQtyNorthZone");

                if (ddlNorthBranches.SelectedIndex > 0)
                {
                    //DirectPurchaseOrders DirPO = new DirectPurchaseOrders();
                    //int stock = DirPO.GetIndentBranchesStock(ddlNorthBranches.SelectedValue, txtItemCode.Text, 2);

                    string[] stock = ddlNorthBranches.SelectedItem.Text.Split('-');
                    txtQtyNorthZone.Text = stock[1].Trim().ToString();
                }
                else
                {
                    txtQtyNorthZone.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranchesEastZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grdRow = ((GridViewRow)ddl.Parent.Parent);
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtOrdItemCode");
                DropDownList ddlEastBranches = (DropDownList)grdRow.FindControl("ddlBranchesEastZone");
                TextBox txtQtyEastZone = (TextBox)grdRow.FindControl("txtQtyEastZone");

                if (ddlEastBranches.SelectedIndex > 0)
                {
                    //DirectPurchaseOrders DirPO = new DirectPurchaseOrders();
                    //int stock = DirPO.GetIndentBranchesStock(ddlEastBranches.SelectedValue, txtItemCode.Text, 3);

                    string[] stock = ddlEastBranches.SelectedItem.Text.Split('-');
                    txtQtyEastZone.Text = stock[1].Trim().ToString();
                }
                else
                {
                    txtQtyEastZone.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ddlBranchesWestZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow grdRow = ((GridViewRow)ddl.Parent.Parent);
                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtOrdItemCode");
                DropDownList ddlWestBranches = (DropDownList)grdRow.FindControl("ddlBranchesWestZone");
                TextBox txtQtyWestZone = (TextBox)grdRow.FindControl("txtQtyWestZone");

                if (ddlWestBranches.SelectedIndex > 0)
                {
                    //DirectPurchaseOrders DirPO = new DirectPurchaseOrders();
                    //int stock = DirPO.GetIndentBranchesStock(ddlWestBranches.SelectedValue, txtItemCode.Text, 4);

                    string[] stock = ddlWestBranches.SelectedItem.Text.Split('-');
                    txtQtyWestZone.Text = stock[1].Trim().ToString();
                }
                else
                {
                    txtQtyWestZone.Text = "";
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            BtnSubmit.Enabled = false;
            DirectPOHeaderHO objValue = new DirectPOHeaderHO();
            objValue.Items = new List<DirectPOItemsHO>();
            CustomerSectionFields objCustomer = new CustomerSectionFields();

            try
            {
                if (Page.IsValid)
                {
                    objValue.Branch_Code = strOrdBranchCode.ToString();
                    objValue.PO_Indent_Date = txtOrdIndentDate.Text;
                    objValue.customercode = ddlOrdCustomer.SelectedValue;
                    objValue.Transaction_Type_Code = ddlOrdTransactionType.SelectedValue;
                    objValue.Supplier_Code = ddlOrdSupplier.SelectedValue;
                    objValue.PO_Number = ddlOrd_PONumber.SelectedValue;

                    DirectPOItemsHO objItems = null;
                    int SNo = 0;

                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        string indItems = "";
                        objItems = new DirectPOItemsHO();
                        CheckBox chkSelected = (CheckBox)gr.FindControl("chkSelected");

                        if (!chkSelected.Checked)
                            continue;

                        SNo += 1;

                        TextBox txtOrdSupplierPartNo = (TextBox)gr.FindControl("txtOrdSupplierPartNo");
                        HiddenField hdnSNo = (HiddenField)gr.FindControl("hdnSNo");
                        TextBox txtOrdItemCode = (TextBox)gr.FindControl("txtOrdItemCode");
                        TextBox txtOrdPackingQuantity = (TextBox)gr.FindControl("txtOrdPackingQuantity");
                        TextBox txtOrderItem_PO_Quantity = (TextBox)gr.FindControl("txtOrderItem_PO_Quantity");
                        ListBox ddlListIndentBranches = (ListBox)gr.FindControl("ddlListIndentBranches");

                        objItems.Serial_Number = SNo;
                        objItems.Supplier_Part_Number = txtOrdSupplierPartNo.Text;
                        objItems.OrderItem_PO_SNo = Convert.ToInt32(hdnSNo.Value);
                        objItems.Item_Code = txtOrdItemCode.Text;
                        objItems.Packing_Quantity = txtOrdPackingQuantity.Text;
                        objItems.OrderItem_PO_Quantity = Convert.ToInt32(txtOrderItem_PO_Quantity.Text);

                        foreach (ListItem lItem in ddlListIndentBranches.Items)
                        {
                            indItems = indItems + "|" + lItem.Value;
                        }

                        objItems.IndentBranchDataCollection = indItems;

                        objValue.Items.Add(objItems);
                    }

                    DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                    string result = objSaveHeaderAndItem.Ins_DirectPOHeaderAndItemsHO(objValue);

                    if ((objValue.ErrorMessage == string.Empty) && (objValue.ErrorCode == "0"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('PO Has been Processed Successfully. Please Click on the Report Buttons to download report accordingly');", true);

                        GridView1.Enabled = false;
                        BtnSubmit.Enabled = false;
                        ddlOrdBranchName.Enabled = false;
                        ddlOrd_PONumber.Enabled = false;
                        ImgButtonQuery.Visible = false;
                        BtnBranchExcelFile.Visible = true;
                        BtnSupplierExcelFile.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('" + objValue.ErrorMessage + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", "alert('Please Add atleast one Item details.');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        //protected void BtnSupplierExcelFile_Click(object sender, EventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
        //        ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "S");

        //        string filename = "HoSupplier_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

        //        Response.Clear();
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        //        Response.ContentType = "application/ms-excel";
        //        Response.Write("<table border='1'><tr>");

        //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        //        {
        //            Response.Write("<th>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
        //        }

        //        Response.Write("</tr>");

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            Response.Write("<tr>");
        //            DataRow row = ds.Tables[0].Rows[i];
        //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //            {

        //                Response.Write("<td>" + row[j] + "</td>");
        //            }
        //            Response.Write("</tr>");
        //        }

        //        Response.Write("</table>");
        //    }
        //    catch (Exception exp)
        //    {
        //        IMPALLibrary.Log.WriteException(Source, exp);
        //    }
        //    finally
        //    {
        //        Response.Flush();
        //        Response.End();
        //        Response.Close();
        //    }
        //}

        //protected void BtnBranchExcelFile_Click(object sender, EventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
        //        ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "B");

        //        string filename = "HoSTDN_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

        //        Response.Clear();
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        //        Response.ContentType = "application/ms-excel";
        //        Response.Write("<table border='1'><tr>");

        //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        //        {
        //            Response.Write("<th>" + ds.Tables[0].Columns[i].ColumnName + "</th>");
        //        }

        //        Response.Write("</tr>");

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            Response.Write("<tr>");
        //            DataRow row = ds.Tables[0].Rows[i];
        //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //            {

        //                Response.Write("<td>" + row[j] + "</td>");
        //            }
        //            Response.Write("</tr>");
        //        }

        //        Response.Write("</table>");
        //    }
        //    catch (Exception exp)
        //    {
        //        IMPALLibrary.Log.WriteException(Source, exp);
        //    }
        //    finally
        //    {
        //        Response.Flush();
        //        Response.End();
        //        Response.Close();
        //    }
        //}

        protected void BtnSupplierExcelFile_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();
                DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "S");

                string filename = "HoSupplier_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //ArrayList root = new ArrayList();
                    //List<Dictionary<string, object>> table;
                    //Dictionary<string, object> data;

                    //foreach (System.Data.DataTable dt in ds.Tables)
                    //{
                    //    table = new List<Dictionary<string, object>>();
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        data = new Dictionary<string, object>();
                    //        foreach (DataColumn col in dt.Columns)
                    //        {
                    //            data.Add(col.ColumnName, dr[col]);
                    //        }
                    //        table.Add(data);
                    //    }
                    //    root.Add(table);
                    //}

                    //hdnJSonExcelData.Value = serializer.Serialize(root);

                    ExportDataSetToExcel(ds, filename);

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
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

        protected void BtnBranchExcelFile_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DataSet ds = new DataSet();
                DirectPurchaseOrders objSaveHeaderAndItem = new DirectPurchaseOrders();
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetails(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "B");

                string filename = "HoSTDN_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //ArrayList root = new ArrayList();
                    //List<Dictionary<string, object>> table;
                    //Dictionary<string, object> data;

                    //foreach (System.Data.DataTable dt in ds.Tables)
                    //{
                    //    table = new List<Dictionary<string, object>>();
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        data = new Dictionary<string, object>();
                    //        foreach (DataColumn col in dt.Columns)
                    //        {
                    //            data.Add(col.ColumnName, dr[col]);
                    //        }
                    //        table.Add(data);
                    //    }
                    //    root.Add(table);
                    //}

                    //hdnJSonExcelData.Value = serializer.Serialize(root);

                    ExportDataSetToExcel(ds, filename);

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "javascript", "DownLoadExcelFile('" + filename + "');", true);
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Server.ClearError();
                Response.Redirect("DirectPurchaseOrderHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

        #endregion

        #region   User defined methods

        private void InitializeControl()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlOrd_PONumber.Visible = false;
                txtOrd_PONumber.Visible = true;
                lblCustomer.Visible = false;
                ddlOrdCustomer.Visible = false;
                //txtOrdIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrdIndentDate.Enabled = false;

                using (ObjectDataSource objBranch = new ObjectDataSource())
                {
                    objBranch.DataObjectTypeName = "IMPALLibrary.Branch";
                    objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    objBranch.SelectMethod = "GetAllOrdBranches";
                    objBranch.DataBind();
                    ddlOrdBranchName.DataSource = objBranch;
                    ddlOrdBranchName.DataTextField = "BranchName";
                    ddlOrdBranchName.DataValueField = "BranchCode";
                    ddlOrdBranchName.DataBind();
                    AddSelect(ddlOrdBranchName);
                    ddlOrdBranchName.SelectedValue = strOrdBranchCode;
                }

                BindOrdTransactionType();
                BindOrdSupplier();
                BindOrdPO_Number();
                BindAllCustomers(strOrdBranchCode);
                BindGridItemsFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindOrdPO_Number()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                //string strBranchCode = Session["BranchCode"].ToString();
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlOrdPO_NumberType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllOrdPO_Number";
                    obj.SelectParameters.Add("strBranchCode", strOrdBranchCode);
                    obj.DataBind();
                    ddlOrd_PONumber.DataSource = obj;
                    ddlOrd_PONumber.DataTextField = "PO_Number";
                    ddlOrd_PONumber.DataValueField = "PO_Number";
                    ddlOrd_PONumber.DataBind();

                    AddSelect(ddlOrd_PONumber);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindOrdTransactionType()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlTransactionType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllOrdTransactionTypes";
                    obj.DataBind();
                    ddlOrdTransactionType.DataSource = obj;
                    ddlOrdTransactionType.DataTextField = "Transaction_Type_Description";
                    ddlOrdTransactionType.DataValueField = "Transaction_Type_Code";
                    ddlOrdTransactionType.DataBind();

                    AddSelect(ddlOrdTransactionType);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindOrdSupplier()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {

                    obj.DataObjectTypeName = "IMPALLibrary.ddlTransactionType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllOrdSupplier";
                    obj.DataBind();
                    ddlOrdSupplier.DataSource = obj;
                    ddlOrdSupplier.DataTextField = "Supplier_Name";
                    ddlOrdSupplier.DataValueField = "Supplier_Code";
                    ddlOrdSupplier.DataBind();
                    AddSelect(ddlOrdSupplier);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void BindAllCustomers(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.ddlCustomerType";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "GetAllCustomers";
                    obj.SelectParameters.Add("strBranchCode", strBranchCode);
                    obj.DataBind();
                    ddlOrdCustomer.DataSource = obj;
                    ddlOrdCustomer.DataTextField = "Customer_Name";
                    ddlOrdCustomer.DataValueField = "Customer_Code";
                    ddlOrdCustomer.DataBind();

                    //  if (!IsPostBack)
                    //  {
                    AddSelect(ddlOrdCustomer);
                    //  }

                    if (ddlOrdCustomer.Visible == true)
                    {
                        if (ddlOrdCustomer.SelectedValue != "0")
                        {
                            FillOrdCustomer();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void FillOrdCustomer()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strCustomerCode = ddlOrdCustomer.SelectedValue;
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    obj.DataObjectTypeName = "IMPALLibrary.CustomerSectionFields";
                    obj.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                    obj.SelectMethod = "FillOrdDirectPO_CustomerSection";
                    obj.SelectParameters.Add("strBranch_Code", strOrdBranchCode);
                    obj.SelectParameters.Add("strCustomer_Code", strCustomerCode);

                    obj.DataBind();

                    CustomerSectionFields objSection = new CustomerSectionFields();

                    object[] objCustomerSections = new object[0];
                    objCustomerSections = (object[])obj.Select();

                    objSection = (CustomerSectionFields)objCustomerSections[0];

                    if (objSection.Customer_Code is object)
                    {
                        txtOrdCustomerCode.Text = objSection.Customer_Code.ToString();
                        txtOrdCustomerAddress1.Text = objSection.Address1.ToString();
                        txtOrdCustomerAddress2.Text = objSection.Address2.ToString();

                        txtOrdCustomerAddress4.Text = objSection.Address4.ToString();
                        txtOrdCustomerLocation.Text = objSection.Location.ToString();
                        // txtOrdTin_Number.Text = objSection.Tin_No.ToString();
                        txtOrdAddress3.Text = objSection.Address3.ToString();
                        DisableControls(true);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
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

        private void BindGridItems()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                string strPO_Number = ddlOrd_PONumber.SelectedValue;
                
                using (ObjectDataSource obj = new ObjectDataSource())
                {
                    dt.Rows.Clear();

                    dt.Columns.Add(new DataColumn("S_No", typeof(string)));
                    dt.Columns.Add(new DataColumn("Item_Value", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Part_Number", typeof(string)));
                    dt.Columns.Add(new DataColumn("Serial_Number", typeof(string)));
                    dt.Columns.Add(new DataColumn("Packing_Quantity", typeof(string)));
                    dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("OrderItem_PO_Quantity", typeof(string)));

                    DirectPurchaseOrders objDS = new DirectPurchaseOrders();
                    DataSet ds = new DataSet();
                    ds = objDS.GetDSOrdDirectPO_Items(ddlOrd_PONumber.SelectedValue, "D", ddlOrdBranchName.SelectedValue);

                    DataRow dr = null;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr["S_No"] = ds.Tables[0].Rows[i]["Serial_Number"].ToString();
                        dr["Item_Value"] = ds.Tables[0].Rows[i]["Item_Value"].ToString();
                        dr["Supplier_Part_Number"] = ds.Tables[0].Rows[i]["Supplier_Part_Number"].ToString();
                        dr["Serial_Number"] = ds.Tables[0].Rows[i]["Serial_Number"].ToString();
                        dr["Packing_Quantity"] = ds.Tables[0].Rows[i]["Packing_Quantity"].ToString();
                        dr["Item_Code"] = ds.Tables[0].Rows[i]["Item_Code"].ToString();
                        dr["OrderItem_PO_Quantity"] = ds.Tables[0].Rows[i]["PO_Quantity"].ToString();

                        dt.Rows.Add(dr);
                    }

                    ViewState["CurrentTable"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.Enabled = true;
                    GridView1.DataBind();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Item_Code"].ToString() != "")
                        {
                            DropDownList ddlBranchesSouthZone = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchesSouthZone");
                            DropDownList ddlBranchesNorthZone = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchesNorthZone");
                            DropDownList ddlBranchesWestZone = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchesWestZone");
                            DropDownList ddlBranchesEastZone = (DropDownList)GridView1.Rows[i].FindControl("ddlBranchesEastZone");

                            Button btnAddSouthZone = (Button)GridView1.Rows[i].FindControl("btnAddSouthZone");
                            Button btnAddNorthZone = (Button)GridView1.Rows[i].FindControl("btnAddNorthZone");
                            Button btnAddEastZone = (Button)GridView1.Rows[i].FindControl("btnAddEastZone");
                            Button btnAddWestZone = (Button)GridView1.Rows[i].FindControl("btnAddWestZone");
                            Button btnRemove = (Button)GridView1.Rows[i].FindControl("btnRemove");

                            btnAddSouthZone.Attributes.Add("OnClick", "return fnAddListitem(this.id);");
                            btnAddNorthZone.Attributes.Add("OnClick", "return fnAddListitem(this.id);");
                            btnAddEastZone.Attributes.Add("OnClick", "return fnAddListitem(this.id);");
                            btnAddWestZone.Attributes.Add("OnClick", "return fnAddListitem(this.id);");

                            btnRemove.Attributes.Add("OnClick", "return fnRemoveListitem(this.id);");

                            DirectPurchaseOrders DirPO = new DirectPurchaseOrders();
                            List<DirectPOItems> lstBranches = new List<DirectPOItems>();
                            lstBranches = DirPO.GetIndentBranchesZoneWise(ds.Tables[0].Rows[i]["Item_Code"].ToString(), 1, ddlOrdBranchName.SelectedValue);
                            ddlBranchesSouthZone.DataSource = lstBranches;
                            ddlBranchesSouthZone.DataValueField = "Indent_Branch";
                            ddlBranchesSouthZone.DataTextField = "Indent_BranchName";
                            ddlBranchesSouthZone.DataBind();

                            if (ddlBranchesSouthZone.Items.Count<=1)
                            {
                                btnAddSouthZone.Attributes.Add("style", "display:none");
                                ddlBranchesSouthZone.Enabled = false;
                                ddlBranchesSouthZone.Attributes.Add("style", "background-color:lightgrey");                                
                            }

                            lstBranches = DirPO.GetIndentBranchesZoneWise(ds.Tables[0].Rows[i]["Item_Code"].ToString(), 2, ddlOrdBranchName.SelectedValue);
                            ddlBranchesNorthZone.DataSource = lstBranches;
                            ddlBranchesNorthZone.DataValueField = "Indent_Branch";
                            ddlBranchesNorthZone.DataTextField = "Indent_BranchName";
                            ddlBranchesNorthZone.DataBind();

                            if (ddlBranchesNorthZone.Items.Count <= 1)
                            {
                                btnAddNorthZone.Attributes.Add("style", "display:none");
                                ddlBranchesNorthZone.Enabled = false;
                                ddlBranchesNorthZone.Attributes.Add("style", "background-color:lightgrey");
                            }

                            lstBranches = DirPO.GetIndentBranchesZoneWise(ds.Tables[0].Rows[i]["Item_Code"].ToString(), 3, ddlOrdBranchName.SelectedValue);
                            ddlBranchesEastZone.DataSource = lstBranches;
                            ddlBranchesEastZone.DataValueField = "Indent_Branch";
                            ddlBranchesEastZone.DataTextField = "Indent_BranchName";
                            ddlBranchesEastZone.DataBind();

                            if (ddlBranchesEastZone.Items.Count <= 1)
                            {
                                btnAddEastZone.Attributes.Add("style", "display:none");
                                ddlBranchesEastZone.Enabled = false;
                                ddlBranchesEastZone.Attributes.Add("style", "background-color:lightgrey");
                            }

                            lstBranches = DirPO.GetIndentBranchesZoneWise(ds.Tables[0].Rows[i]["Item_Code"].ToString(), 4, ddlOrdBranchName.SelectedValue);
                            ddlBranchesWestZone.DataSource = lstBranches;
                            ddlBranchesWestZone.DataValueField = "Indent_Branch";
                            ddlBranchesWestZone.DataTextField = "Indent_BranchName";
                            ddlBranchesWestZone.DataBind();

                            if (ddlBranchesWestZone.Items.Count <= 1)
                            {
                                btnAddWestZone.Attributes.Add("style", "display:none");
                                ddlBranchesWestZone.Enabled = false;
                                ddlBranchesWestZone.Attributes.Add("style", "background-color:lightgrey");
                            }
                        }
                    }

                    SetPreviousData();
                    hdnFreezeRowCnt.Value = "1";
                    hdnRowCnt.Value = "0";
                    // }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void SetPreviousData()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int rowIndex = 0;
            try
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        // System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Label lblItemValue = (Label)GridView1.Rows[rowIndex].FindControl("lblItemValue");
                            TextBox txtOrdSupplierPartNo = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdSupplierPartNo");
                            HiddenField hdnSNo = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdnSNo");
                            TextBox txtItemCode = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdItemCode");
                            TextBox txtOrdPackingQuantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrdPackingQuantity");
                            TextBox txtOrderItem_PO_Quantity = (TextBox)GridView1.Rows[rowIndex].FindControl("txtOrderItem_PO_Quantity");

                            lblItemValue.Text = TwoDecimalConversion(dt.Rows[i]["Item_Value"].ToString());
                            txtOrdSupplierPartNo.Text = dt.Rows[i]["Supplier_Part_Number"].ToString();
                            hdnSNo.Value = dt.Rows[i]["Serial_Number"].ToString();
                            txtOrdPackingQuantity.Text = dt.Rows[i]["Packing_Quantity"].ToString();
                            txtItemCode.Text = dt.Rows[i]["Item_Code"].ToString();
                            txtOrderItem_PO_Quantity.Text = dt.Rows[i]["OrderItem_PO_Quantity"].ToString();

                            rowIndex++;
                        }
                    }
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

        private void BindGridItemsFooter()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                FirstGridViewRow();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void FirstGridViewRow()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dt.Columns.Add(new DataColumn("S_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_Value", typeof(string)));
                dt.Columns.Add(new DataColumn("Supplier_Part_Number", typeof(string)));
                dt.Columns.Add(new DataColumn("Serial_Number", typeof(string)));
                dt.Columns.Add(new DataColumn("Packing_Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("OrderItem_PO_Quantity", typeof(string)));

                DataRow dr = null;

                dr = dt.NewRow();
                dr["S_No"] = string.Empty;
                dr["Item_Value"] = string.Empty;
                dr["Supplier_Part_Number"] = string.Empty;
                dr["Serial_Number"] = string.Empty;
                dr["Packing_Quantity"] = string.Empty;
                dr["Item_Code"] = string.Empty;
                dr["OrderItem_PO_Quantity"] = string.Empty;

                dt.Rows.Add(dr);

                ViewState["CurrentTable"] = dt;
                ViewState["GridRowCount"] = dt.Rows.Count;

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                GridView1.Rows[0].Cells[0].ColumnSpan = GridView1.Columns.Count - 1;
                GridView1.HeaderRow.Enabled = false;
                ViewState["GridRowCount"] = "0";
                hdnRowCnt.Value = "0";
                hdnFreezeRowCnt.Value = "0";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void DisableControls(bool setFlag)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                ddlOrdTransactionType.Enabled = setFlag;
                ddlOrdSupplier.Enabled = setFlag;
                ddlOrdCustomer.Enabled = setFlag;
                txtOrdCustomerCode.Enabled = false;
                txtOrdCustomerAddress1.Enabled = false;
                txtOrdCustomerAddress2.Enabled = false;
                txtOrdAddress3.Enabled = false;
                txtOrdCustomerAddress4.Enabled = false;
                txtOrdCustomerLocation.Enabled = false;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void ChkSelected_OnCheckedChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void ResetToEmpty()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtOrd_PONumber.Text = "";
                txtOrdIndentDate.Text = "";
                txtPOType.Text = "";
                txtPOValue.Text = "";
                txtOrdCustomerAddress1.Text = "";
                txtOrdCustomerAddress2.Text = "";
                txtOrdCustomerAddress4.Text = "";
                txtOrdAddress3.Text = "";
                txtOrdCustomerCode.Text = "";
                txtOrdCustomerLocation.Text = "";

                ddlOrdTransactionType.SelectedIndex = 0;

                if (ddlOrdCustomer.Visible == true)
                    ddlOrdCustomer.SelectedIndex = 0;

                ddlOrd_PONumber.SelectedIndex = 0;
                ddlOrdSupplier.SelectedIndex = 0;
                BindGridItemsFooter();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnAddSouthZone_Click(object sender, EventArgs e)
        {
            bool blnFlag = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Button btn = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btn.Parent.Parent);
                DropDownList ddlSouthBranches = (DropDownList)grdRow.FindControl("ddlBranchesSouthZone");
                ListBox ddlListIndentBranches = (ListBox)grdRow.FindControl("ddlListIndentBranches");
                TextBox txtQtySouthZone = (TextBox)grdRow.FindControl("txtQtySouthZone");

                string[] stock = ddlSouthBranches.SelectedItem.Text.Split('-');
                string strText = stock[0].ToString() + " - " + txtQtySouthZone.Text;
                foreach (ListItem lst in ddlListIndentBranches.Items)
                {
                    if (strText.Equals(lst.Value))
                    {
                        blnFlag = true;
                        break;
                    }
                }

                if (!blnFlag)
                    ddlListIndentBranches.Items.Add(new ListItem(strText, strText));

                ddlSouthBranches.SelectedIndex = 0;
                txtQtySouthZone.Text = "";
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

        protected void btnAddNorthZone_Click(object sender, EventArgs e)
        {
            bool blnFlag = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Button btn = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btn.Parent.Parent);
                DropDownList ddlBranchesNorthZone = (DropDownList)grdRow.FindControl("ddlBranchesNorthZone");
                ListBox ddlListIndentBranches = (ListBox)grdRow.FindControl("ddlListIndentBranches");
                TextBox txtQtyNorthZone = (TextBox)grdRow.FindControl("txtQtyNorthZone");

                string[] stock = ddlBranchesNorthZone.SelectedItem.Text.Split('-');
                string strText = stock[0].ToString() + " - " + txtQtyNorthZone.Text;
                foreach (ListItem lst in ddlListIndentBranches.Items)
                {
                    if (strText.Equals(lst.Value))
                    {
                        blnFlag = true;
                        break;
                    }
                }

                if (!blnFlag)
                    ddlListIndentBranches.Items.Add(new ListItem(strText, strText));

                ddlBranchesNorthZone.SelectedIndex = 0;
                txtQtyNorthZone.Text = "";
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

        protected void btnAddEastZone_Click(object sender, EventArgs e)
        {
            bool blnFlag = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Button btn = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btn.Parent.Parent);
                DropDownList ddlBranchesEastZone = (DropDownList)grdRow.FindControl("ddlBranchesEastZone");
                ListBox ddlListIndentBranches = (ListBox)grdRow.FindControl("ddlListIndentBranches");
                TextBox txtQtyEastZone = (TextBox)grdRow.FindControl("txtQtyEastZone");

                string[] stock = ddlBranchesEastZone.SelectedItem.Text.Split('-');
                string strText = stock[0].ToString() + " - " + txtQtyEastZone.Text;
                foreach (ListItem lst in ddlListIndentBranches.Items)
                {
                    if (strText.Equals(lst.Value))
                    {
                        blnFlag = true;
                        break;
                    }
                }

                if (!blnFlag)
                    ddlListIndentBranches.Items.Add(new ListItem(strText, strText));

                ddlBranchesEastZone.SelectedIndex = 0;
                txtQtyEastZone.Text = "";
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

        protected void btnAddWestZone_Click(object sender, EventArgs e)
        {
            bool blnFlag = false;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Button btn = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btn.Parent.Parent);
                DropDownList ddlBranchesWestZone = (DropDownList)grdRow.FindControl("ddlBranchesWestZone");
                ListBox ddlListIndentBranches = (ListBox)grdRow.FindControl("ddlListIndentBranches");
                TextBox txtQtyWestZone = (TextBox)grdRow.FindControl("txtQtyWestZone");

                string[] stock = ddlBranchesWestZone.SelectedItem.Text.Split('-');
                string strText = stock[0].ToString() + " - " + txtQtyWestZone.Text;
                foreach (ListItem lst in ddlListIndentBranches.Items)
                {
                    if (strText.Equals(lst.Value))
                    {
                        blnFlag = true;
                        break;
                    }
                }

                if (!blnFlag)
                    ddlListIndentBranches.Items.Add(new ListItem(strText, strText));

                ddlBranchesWestZone.SelectedIndex = 0;
                txtQtyWestZone.Text = "";
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
                Button btn = (Button)sender;
                GridViewRow grdRow = ((GridViewRow)btn.Parent.Parent);
                ListBox ddlListIndentBranches = (ListBox)grdRow.FindControl("ddlListIndentBranches");
                DropDownList ddlSouthBranches = (DropDownList)grdRow.FindControl("ddlBranchesSouthZone");
                TextBox txtQtySouthZone = (TextBox)grdRow.FindControl("txtQtySouthZone");
                DropDownList ddlNorthBranches = (DropDownList)grdRow.FindControl("ddlBranchesNorthZone");
                TextBox txtQtyNorthZone = (TextBox)grdRow.FindControl("txtQtyNorthZone");
                DropDownList ddlEastBranches = (DropDownList)grdRow.FindControl("ddlBranchesEastZone");
                TextBox txtQtyEastZone = (TextBox)grdRow.FindControl("txtQtyEastZone");
                DropDownList ddlWestBranches = (DropDownList)grdRow.FindControl("ddlBranchesWestZone");
                TextBox txtQtyWestZone = (TextBox)grdRow.FindControl("txtQtyWestZone");

                for (intcount = 0; ; )
                {
                    if (intcount < ddlListIndentBranches.Items.Count)
                    {
                        if (ddlListIndentBranches.Items[intcount].Selected)
                        {
                            ddlListIndentBranches.Items.Remove(ddlListIndentBranches.Items[intcount].Text);
                        }
                        else
                            intcount++;
                    }
                    else
                        break;
                }

                if (ddlSouthBranches.SelectedIndex > 0)
                {
                    ddlSouthBranches.SelectedIndex = 0;
                    txtQtySouthZone.Text = "";
                }

                if (ddlNorthBranches.SelectedIndex > 0)
                {
                    ddlNorthBranches.SelectedIndex = 0;
                    txtQtyNorthZone.Text = "";
                }

                if (ddlEastBranches.SelectedIndex > 0)
                {
                    ddlEastBranches.SelectedIndex = 0;
                    txtQtyEastZone.Text = "";
                }

                if (ddlWestBranches.SelectedIndex > 0)
                {
                    ddlWestBranches.SelectedIndex = 0;
                    txtQtyWestZone.Text = "";
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
        #endregion
    }
}