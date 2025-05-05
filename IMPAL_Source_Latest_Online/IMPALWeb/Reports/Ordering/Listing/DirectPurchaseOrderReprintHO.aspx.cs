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

namespace IMPALWeb.Reports.Ordering.Listing
{
    public partial class DirectPurchaseOrderReprintHO : System.Web.UI.Page
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
                Log.WriteException(typeof(DirectPurchaseOrderReprintHO), exp);
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
                        txtOrdCustomerCode.Text = objSection.Customer_Code.ToString();
                        txtOrdCustomerAddress1.Text = objSection.Address1.ToString();
                        txtOrdCustomerAddress2.Text = objSection.Address2.ToString();
                        // txtOrdTin_Number.Text = objSection.Tin_No.ToString();
                        txtOrdAddress3.Text = objSection.Address3.ToString();
                        txtOrdCustomerAddress4.Text = objSection.Address4.ToString();
                        txtOrdCustomerLocation.Text = objSection.Location.ToString();
                        DisableControls(false);

                        BtnBranchExcelFile.Visible = true;
                        BtnSupplierExcelFile.Visible = true;
                    }
                    else
                    {
                        ResetToEmpty();
                        BtnBranchExcelFile.Visible = false;
                        BtnSupplierExcelFile.Visible = false;
                    }
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
                txtOrd_PONumber.Visible = false;
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
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetailsReprint(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "S");

                string filename = "HoSupplier_Reprint_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

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
                ds = objSaveHeaderAndItem.GetPO_StdnSupplierDetailsReprint(ddlOrdBranchName.SelectedValue, ddlOrd_PONumber.SelectedValue, ddlOrdSupplier.SelectedValue, "B");

                string filename = "HoSTDN_Reprint_" + ddlOrdSupplier.SelectedValue + "_" + ddlOrdBranchName.SelectedValue + "_" + txtOrdIndentDate.Text.Replace("/", "") + ".xls";

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
                    HttpContext.Current.Response.End();
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
                Response.Redirect("DirectPurchaseOrderReprintHO.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
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
                    obj.SelectMethod = "GetAllOrdPO_Number_Reprint";
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

        private void ResetToEmpty()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtOrd_PONumber.Text = "";
                txtOrdIndentDate.Text = "";
                txtPOType.Text = "";
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
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
    }
}