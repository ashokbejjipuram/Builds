using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions;
using System.Windows;

namespace IMPALWeb.Ordering
{
    public partial class POIndentCorpDummy : System.Web.UI.Page
    {
        ItemLocationDetails Item = new ItemLocationDetails();
        string strPONumber;
        string strBranchCode;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(POIndentCorpDummy), exp);
            }
        }

        Int32 Branch_count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                strPONumber = Request.QueryString["StrPONumber"];
                strBranchCode = Request.QueryString["StrBranchCode"];

                LblCanBillValue.Text = Request.QueryString["Available_Balance"] == "0.0000" ? string.Empty : Request.QueryString["Available_Balance"];
                LblAllotedAmtValue.Text = Request.QueryString["Branch_Amount"] == "0.0000" ? string.Empty : Request.QueryString["Branch_Amount"];

                if (!IsPostBack)
                {
                    grvItemDetails.ShowFooter = false;
                    int EditButtonIndex = grvItemDetails.Columns.Count - 1;
                    grvItemDetails.Columns[EditButtonIndex].Visible = false;

                    BindHeadDetails();
                    BindItemDetails();

                    if (grvItemDetails.Rows.Count > 0)
                        grvItemDetails.PageIndex = 0;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void HOQtyUpdate()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string sqlstr = string.Empty;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = null;

                foreach (GridViewRow row in grvItemDetails.Rows)
                {
                    var OrderQty = (Label)row.FindControl("lblOrderQty");
                    var StdnQty = (TextBox)row.FindControl("txtSTDNQuantity");
                    var StdnBr = (Label)row.FindControl("lblSTDNBranch");
                    var HoQty = (TextBox)row.FindControl("txtEditHOAccepted");
                    var PartNo = (Label)row.FindControl("lblSuppPartNo");
                    var ItemCode = (HiddenField)row.FindControl("lblItemCode");
                    var SuppPartNo = (Label)row.FindControl("lblSuppPartNo");
                    int oQty = 0;
                    int sQty = 0;
                    int OrdQty = 0;

                    if (HoQty != null)
                        oQty = Convert.ToInt32(HoQty.Text);
                    if (StdnQty != null)
                        sQty = Convert.ToInt32(StdnQty.Text);
                    if (OrderQty != null)
                        OrdQty = Convert.ToInt32(OrderQty.Text);

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string sqlquery = string.Empty;
                        string sqlquery1 = string.Empty;
                        if (OrdQty > 0 && oQty > 0 && sQty == 0)
                        {
                            sqlquery = "update ccwh_dummy1 set to_order_qty = " + Convert.ToInt32(StdnQty.Text) + ",indent_qty = " + Convert.ToInt32(StdnQty.Text) + "";
                            sqlquery = sqlquery + " where order_qty > 0 and indent_qty > 0 and item_code = '" + ItemCode.Value.ToString() + "' and po_number ='" + strPONumber + "'";
                        }
                        else if (OrdQty == 0 && oQty > 0 && sQty > 0)
                        {
                            sqlquery = "update ccwh_dummy1 set stdn_qty  = isnull(stdn_qty,0) + " + (Convert.ToInt32(StdnQty.Text)) + "";
                            sqlquery = sqlquery + ",ho_br_qty =  " + (Convert.ToInt32(HoQty.Text)) + " where order_qty > 0 and indent_qty > 0 and item_code = '" + ItemCode.Value.ToString() + "' and po_number ='" + strPONumber + "'";

                            sqlquery1 = "update ccwh_dummy1 set ho_br_qty = " + (Convert.ToInt32(HoQty.Text)) + " , ho_br_code = '" + StdnBr.Text.ToString() + "' where stdn_qty = " + (Convert.ToInt32(StdnQty.Text)) + " and indent_qty = 0 and item_code = '" + ItemCode.Value.ToString() + "' and po_number ='" + strPONumber + "'";
                        }
                        else if (OrdQty > 0 && oQty == 0)
                        {
                            sqlquery = "update ccwh_dummy1 set order_qty = 0 ,INdent_qty = " + Convert.ToInt32(StdnQty.Text) + "";
                            sqlquery = sqlquery + ",ho_br_qty = " + (Convert.ToInt32(HoQty.Text)) + " where order_qty > 0 and indent_qty > 0 and item_code = '" + ItemCode.ToString() + "' and po_number ='" + strPONumber + "'";

                            sqlquery1 = "update ccwh_dummy1 set ho_br_qty = " + (Convert.ToInt32(HoQty.Text)) + ", ho_br_code ='" + StdnBr.Text.ToString() + "' where stdn_qty = " + Convert.ToInt32(StdnQty.Text) + " and indent_qty = 0 and item_code = '" + ItemCode.Value.ToString() + "' and po_number ='" + strPONumber + "'";
                        }
                        if (sqlquery.Length > 0)
                        {
                            cmd = ImpalDB.GetSqlStringCommand(sqlquery);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                        }

                        if (sqlquery1.Length > 0)
                        {
                            cmd = ImpalDB.GetSqlStringCommand(sqlquery1);
                            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                            ImpalDB.ExecuteNonQuery(cmd);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void BindHeadDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string sqlstr = string.Empty;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                sqlstr = string.Empty;
                sqlstr = "select count(1) from purchase_order_Detail where po_Number = '" + strPONumber + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                LblTotalItemValue.Text = Convert.ToString(ImpalDB.ExecuteScalar(cmd)) ?? string.Empty;

                sqlstr = string.Empty;
                sqlstr = "select count(1) from purchase_order_Schedule where po_Number ='" + strPONumber + "' and status IN('R','S') ";
                cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                LblRecItemsValue.Text = Convert.ToString(ImpalDB.ExecuteScalar(cmd)) ?? string.Empty;

                sqlstr = string.Empty;
                sqlstr = "select count(1) from ccwh_dummy1 where order_qty > 0 and Po_number ='" + strPONumber + "'";
                cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                LblToBeOrderedValue.Text = Convert.ToString(ImpalDB.ExecuteScalar(cmd)) ?? string.Empty;

                LblPONumberValue.Text = strPONumber;

                sqlstr = string.Empty;
                sqlstr = "select case when substring(reference_number,1,6) = 'ABCFMS' then 'ABCFMS WORKSHEET' ";
                sqlstr = sqlstr + " when reference_number like 'WRK%' then 'NORMAL WORKSHEET' else ' WORKSHEET ' ";
                sqlstr = sqlstr + " end from purchase_Order_header where po_Number = '" + strPONumber + "'";

                cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                LblPONumbersheet.Text = Convert.ToString(ImpalDB.ExecuteScalar(cmd));
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void BindItemDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string sqlstr = string.Empty;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    sqlstr = string.Empty;
                    sqlstr = "select count(1) from branch_Percentage Where Supplier_code = '" + strPONumber.Substring(10, 3) + "' and branch_code = '" + strPONumber.Substring(14, 3) + "' and Permit_Status ='Z'";
                    DbCommand cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Branch_count = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                    if (Branch_count > 0)
                    {
                        sqlstr = string.Empty;
                        sqlstr = "update Purchase_Order_Header Set Status = 'Z',Downup_status = Null,DateStamp = getdate() Where po_number ='" + strPONumber + "'";
                        cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);

                        Response.Write("<script>alert('This Order Locked Due To Ho Norms')</script>");
                        Server.ClearError();
                        Response.Redirect("POIndentCorp.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }

                    cmd = ImpalDB.GetStoredProcCommand("Usp_CCWH_Temp_Tables");
                    ImpalDB.AddInParameter(cmd, "@Branch_code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@Po_Number", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    cmd = ImpalDB.GetStoredProcCommand("Usp_ccwh_worksheet");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    sqlstr = string.Empty;
                    sqlstr = "delete from ccwh_dummy1";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    sqlstr = string.Empty;
                    sqlstr = "Insert into ccwh_dummy1(po_number,Supplier_part_Number,item_code,order_qty,stdn_qty,stdn_branch,indent_qty,";
                    sqlstr = sqlstr + " stk_on_hand,pending_order,avg_sales,currmonth_sales,pack_qty,percentage)";
                    sqlstr = sqlstr + " select po_number,supplier_part_number,item_code, (isnull(to_order_qty,0) + isnull(stdn_quantity,0)) Order_Qty,";
                    sqlstr = sqlstr + " stdn_quantity,stdn_branch,isnull(indent_qty,0) Indent_quantity,total_stock,";
                    sqlstr = sqlstr + " (pending_order_stk + pending_order_stu + pending_order_oth+pending_order_ind) as pendingorder,";
                    sqlstr = sqlstr + " (Avrg_sales_stk + Avrg_sales_stu + Avrg_Sales_Oth) as averagesales, ";
                    sqlstr = sqlstr + " (Curr_Sales_Stk +  Curr_Sales_STU + Curr_Sales_Oth) as currentmthsales,Packing_Quantity,percentage from ccwh_item_worksheet ";
                    sqlstr = sqlstr + " where To_Order_Qty > 0 and po_number = '" + strPONumber + "' and branch_code ='" + strPONumber.Substring(14, 3) + "' order by order_qty Desc,supplier_part_number Asc";

                    //DataTable dt = new DataTable();
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    ImpalDB.ExecuteNonQuery(cmd);

                    cmd = ImpalDB.GetStoredProcCommand("usp_ccwh_Dummy_STDN");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    sqlstr = string.Empty;
                    sqlstr = "select po_number,a.supplier_part_number,b.item_short_description,a.item_code,";
                    sqlstr = sqlstr + " stk_on_hand,pending_order,avg_sales,currmonth_sales,order_qty,stdn_qty,indent_qty,ho_qty,ho_br_code,";
                    sqlstr = sqlstr + " ho_br_qty,stdn_branch,isnull(cost_price,0) cost_price1,a.original_receipt_date,aging,";
                    sqlstr = sqlstr + " Packing_quantity,Consignment_Sr_No,inward_Number from ccwh_dummy1 a inner join item_master b";
                    sqlstr = sqlstr + " on a.item_code = b.item_code and a.supplier_part_number = b.supplier_part_number";
                    sqlstr = sqlstr + " inner join Branch_item_Price c on b.item_code = c.item_code"; //and b.branch_code = c.branch_code";
                    sqlstr = sqlstr + " where a.po_number = '" + strPONumber + "'";
                    sqlstr = sqlstr + " order by a.supplier_part_number,order_qty desc,priority,stdn_Branch asc";

                    DataTable dr = new DataTable();
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                    {
                        dr.Load(reader);
                    }

                    grvItemDetails.DataSource = dr;
                    grvItemDetails.DataBind();
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        //protected void grvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        //    try
        //    {
        //        grvItemDetails.PageIndex = e.NewPageIndex;
        //        BindItemDetails();  
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException(Source, exp);
        //    }
        //}

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("POIndentCorp.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    HOQtyUpdate();
                    string sqlstr = string.Empty;
                    Database ImpalDB = DataAccess.GetDatabase();
                    DbCommand cmd = null;

                    sqlstr = "select count(1) as cnt  from ccwh_item_worksheet inner join ccwh_detail_Temp ON ccwh_item_worksheet.Supplier_Part_Number = ccwh_detail_Temp.ccwh_Supplier_Part_Number";
                    sqlstr = sqlstr + " and ccwh_item_worksheet.po_number = ccwh_detail_Temp.ccwh_branch_po_Number and ccwh_item_worksheet.po_number = '" + strPONumber + "'";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Int32 Cnt1 = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                    int d = 0;

                    if (Cnt1 == 0)
                    {
                        foreach (GridViewRow row in grvItemDetails.Rows)
                        {
                            var OrderQty = (Label)row.FindControl("lblOrderQty");
                            var StdnQty = (TextBox)row.FindControl("txtSTDNQuantity");
                            var StdnBr = (Label)row.FindControl("lblSTDNBranch");
                            var HoQty = (TextBox)row.FindControl("txtEditHOAccepted");
                            var PartNo = (Label)row.FindControl("lblSuppPartNo");
                            var ItemCode = (HiddenField)row.FindControl("lblItemCode");
                            var SuppPartNo = (Label)row.FindControl("lblSuppPartNo");
                            var grn_no = (HiddenField)row.FindControl("lblgrnno");
                            var gr_sr_no = (HiddenField)row.FindControl("lblgrsrno");

                            string sqlquery = string.Empty;
                            int oQty = 0;
                            int sQty = 0;
                            int OrdQty = 0;
                            string vgrn_no = string.Empty;
                            int vgr_sr_no = 0;

                            if (HoQty != null)
                                oQty = Convert.ToInt32(HoQty.Text);
                            if (StdnQty != null)
                                sQty = Convert.ToInt32(StdnQty.Text);
                            if (OrderQty != null)
                                OrdQty = Convert.ToInt32(OrderQty.Text);
                            if (grn_no != null)
                                vgrn_no = grn_no.Value.ToString();
                            if (gr_sr_no != null)
                                vgr_sr_no = Convert.ToInt32(gr_sr_no.Value);

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                if (oQty > 0 && OrdQty > 0)
                                {
                                    d = d + 1;
                                    sqlquery = "Exec usp_ccwh_detail_Temp_Update '" + strPONumber.ToString() + "'," + d + ",'" + SuppPartNo.Text.ToString() + "', '" + ItemCode.Value.ToString() + "'," + OrderQty.Text + "," + HoQty.Text + "," + HoQty.Text + ",0,'" + StdnBr.Text.ToString() + "',NULL,'" + vgrn_no + "'," + vgr_sr_no + "";
                                }
                                else if (oQty > 0 && OrdQty == 0)
                                {
                                    d = d + 1;
                                    sqlquery = "insert into ccwh_detail_temp (CCWH_Branch_Po_Number,CCWH_Serial_Number,CCWH_Supplier_Part_Number,CCWH_Item_code,CCWH_Branch_po_Quantity,CCWH_Po_Quantity,CCWH_Indent_Quantity,CCWH_STDN_Quantity,CCWH_Stdn_branch_code,CCWH_Status,CCWH_Date_Stamp,Inward_Number,Consignment_Sr_No) Values ('";
                                    sqlquery = sqlquery + strPONumber.ToString() + "'," + d + ",'" + SuppPartNo.Text.ToString() + "', '" + ItemCode.Value.ToString() + "'," + OrderQty.Text + "," + OrderQty.Text + "," + OrderQty.Text + "," + HoQty.Text + ",'" + StdnBr.Text.ToString() + "',NULL,getdate(),'" + vgrn_no + "'," + vgr_sr_no + ")";

                                    //sqlquery = sqlquery + strPONumber.ToString() + "'," + d + ",'" + SuppPartNo.Text.ToString() + "', '" + ItemCode.Value.ToString() + "'," + OrderQty.Text + "," + OrderQty.Text + "," + HoQty.Text + "," + StdnQty.Text + ",'" + StdnBr.Text.ToString() + "',NULL,getdate(),'" + vgrn_no + "'," + vgr_sr_no + ")";
                                }

                                if (sqlquery.Length > 0)
                                {
                                    cmd = ImpalDB.GetSqlStringCommand(sqlquery);
                                    ImpalDB.ExecuteNonQuery(cmd);

                                    sqlquery = string.Empty;
                                    sqlquery = "select isnull(List_Price,0) List_price,isnull(Discount,0) Discount ,isnull(Supplier_Line_code,0) Supplier_Line_code from purchase_order_detail where po_number = '" + strPONumber.ToString() + "' and item_code in (select item_code from item_master where supplier_part_number ='" + SuppPartNo.Text.ToString() + "')"; //and branch_code ='" + strBranchCode.ToString() + "'

                                    Decimal List_Price = 0;
                                    Decimal Discount = 0;
                                    string supplier_line_code = string.Empty;
                                    cmd = ImpalDB.GetSqlStringCommand(sqlquery);
                                    using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                                    {
                                        while (reader.Read())
                                        {
                                            List_Price = Convert.ToDecimal(reader[0]);
                                            Discount = Convert.ToDecimal(reader[1]);
                                            supplier_line_code = reader[2].ToString();
                                        }
                                    }

                                    sqlquery = string.Empty;
                                    sqlquery = "update CCWH_Detail_Temp set CCWH_Po_List_Value = " + List_Price + " ,CCWH_Po_Discount = " + Discount + ", CCWH_Supplier_Line_Code = '" + supplier_line_code + "'";
                                    sqlquery = sqlquery + " where CCWH_Branch_po_Number='" + strPONumber.ToString() + "' and ccwh_supplier_part_number ='" + SuppPartNo.Text.ToString() + "'";
                                    cmd = ImpalDB.GetSqlStringCommand(sqlquery);
                                    ImpalDB.ExecuteNonQuery(cmd);
                                }
                            }
                        }
                    }

                    sqlstr = string.Empty;
                    sqlstr = "select Max(Original_Receipt_Date) Original_Receipt_Date from ccwh_item_worksheet";
                    sqlstr = sqlstr + " where To_Order_Qty > 0 and po_number ='" + strPONumber + "'";
                    sqlstr = sqlstr + " group by po_number,branch_code";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                    string vRecDt = Convert.ToString(ImpalDB.ExecuteScalar(cmd));
                    vRecDt = (vRecDt.Length == 0 || vRecDt == null) ? DateTime.Now.ToString() : vRecDt;

                    sqlstr = string.Empty;
                    sqlstr = "select count(1) from ccwh_detail_temp where CCWH_Branch_Po_Number ='" + strPONumber + "'";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Int32 Cnt2 = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                    if (Cnt2 > 0)
                    {

                        sqlstr = string.Empty;
                        sqlstr = "insert into ccwh_header_Temp (ccwh_date,ccwh_branch_code,ccwh_branch_po_number,ccwh_branch_po_date,ccwh_downup_status,ccwh_date_stamp) ";
                        sqlstr = sqlstr + "	values ('" + vRecDt + "','" + strBranchCode + "','" + strPONumber + "','" + vRecDt + "',Null,getdate())";
                        cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                        ImpalDB.ExecuteNonQuery(cmd);

                        sqlstr = string.Empty;
                        sqlstr = "Select transaction_type_code,po_list_value,discount,po_value,po_date from purchase_order_header where po_number = '" + strPONumber + "'";
                        cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;

                        string vTranType = string.Empty;
                        decimal po_list_value = 0;
                        decimal discount = 0;
                        decimal po_value = 0;
                        string po_date = string.Empty;

                        using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                        {
                            while (reader.Read())
                            {
                                vTranType = reader[0].ToString();
                                po_list_value = Convert.ToDecimal(reader[1].ToString());
                                discount = Convert.ToDecimal(reader[2]);
                                po_value = Convert.ToDecimal(reader[3]);
                                po_date = reader[4].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(vTranType))
                        {
                            sqlstr = string.Empty;
                            sqlstr = "update CCWH_Header_Temp set ccwh_Transaction_type_code = '" + vTranType + "',CCWH_Branch_Po_List_Value = " + po_list_value;
                            sqlstr = sqlstr + ", CCWH_Branch_Po_Value = " + po_value + ", CCWH_Branch_po_Discount =" + discount + ", CCWH_Date = '" + po_date + "' where CCWH_Branch_Po_Number = '" + strPONumber + "'";
                            cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                            ImpalDB.ExecuteNonQuery(cmd);
                        }

                        cmd = ImpalDB.GetStoredProcCommand("usp_ccwh_updQty");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }

                    sqlstr = string.Empty;
                    sqlstr = "select isnull(count(1),0) from ccwh_detail_temp where isnull(CCWH_STDN_Quantity,0) > 0 and CCWH_Branch_Po_Number ='" + strPONumber + "'";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    Int32 Cnt3 = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

                    if (Cnt3 > 0)
                    {
                        cmd = ImpalDB.GetStoredProcCommand("usp_CCWH_CyberWarehouse_process_new");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Indentno", DbType.String, strPONumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);

                        cmd = ImpalDB.GetStoredProcCommand("usp_CCWH_StockTransferUpdate_STDN");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd);
                    }

                    cmd = ImpalDB.GetStoredProcCommand("usp_CCWH_getdetails_new");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@ponumber", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    cmd = ImpalDB.GetStoredProcCommand("usp_CCWH_Order_Stdn");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    cmd = ImpalDB.GetStoredProcCommand("usp_ccwh_item_worksheet_update");
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(cmd, "@po_number", DbType.String, strPONumber);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);

                    sqlstr = string.Empty;
                    sqlstr = "Delete from ccwh_dummy1 where po_Number ='" + strPONumber + "'";
                    cmd = ImpalDB.GetSqlStringCommand(sqlstr);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmd);
                    scope.Complete();
                }

                Server.ClearError();
                string message = "Process done!";
                string url = "POIndentCorp.aspx";
                string script = "{ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += url;
                script += "'; }";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", script, true);
                //Response.Redirect("POIndentCorp.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void grvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var StdnBr = (Label)e.Row.FindControl("lblSTDNBranch");
                    if (StdnBr.Text == "")
                    {
                        int i = 0;
                        while (i < 12)
                        {
                            e.Row.Cells[i].BorderStyle = BorderStyle.Solid;
                            e.Row.Cells[i].BorderColor = System.Drawing.Color.Red;
                            e.Row.Cells[10].Text = "-Nil-";
                            i = i + 1;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void txtEditHOAccepted_TextChanged(object sender, EventArgs e)
        {

        }
    }
}