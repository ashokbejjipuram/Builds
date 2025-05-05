using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using System.Web.Script.Services;
using System.Web.Services;

namespace IMPALWeb.UserControls
{
    public partial class ItemCodePartNumber : System.Web.UI.UserControl
    {
        private string m_SupplierLine = string.Empty;
        private string m_SupplierPartnumber = string.Empty;
        private string m_itemcode = string.Empty;
        private string m_Mode = string.Empty;
        private string m_Disable = string.Empty;
        private string m_SupplierType = string.Empty;
        private string g_Itemcode = string.Empty;
        private string m_SupplierDesc = string.Empty;

        public string ItemCode
        {
            get
            {
                if (ViewState["PartNoItemCode"] != null)
                { g_Itemcode = Convert.ToString(ViewState["PartNoItemCode"]); };

                return g_Itemcode;
            }
            set
            {

                ViewState["PartNoItemCode"] = value;

            }
        }

        public string SupplierLine
        {
            get
            {
                if (ViewState["SupplierLine"] != null)
                { m_SupplierLine = Convert.ToString(ViewState["SupplierLine"]); };
                return m_SupplierLine;
            }
            set { ViewState["SupplierLine"] = value; }
        }

        public string SupplierDesc
        {
            get
            {
                if (ViewState["SupplierDesc"] != null)
                { m_SupplierDesc = Convert.ToString(ViewState["SupplierDesc"]); };
                return m_SupplierDesc;
            }
            set { ViewState["SupplierDesc"] = value; }
        }

        public string Mode
        {
            get
            {
                if (ViewState["Mode"] != null)
                { m_Mode = Convert.ToString(ViewState["Mode"]); };
                return m_Mode;
            }
            set { ViewState["Mode"] = value; }
        }

        public string Disable
        {
            get
            {
                if (ViewState["Disable"] != null)
                { m_Disable = Convert.ToString(ViewState["Disable"]); };
                return m_Disable;
            }
            set { ViewState["Disable"] = value; }
        }

        public string SupplierPartnumber
        {
            get
            {
                if (ViewState["SupplierPartnumber"] != null)
                { m_SupplierPartnumber = Convert.ToString(ViewState["SupplierPartnumber"]); };
                return m_SupplierPartnumber;
            }
            set { ViewState["SupplierPartnumber"] = value; }

        }

        public string SupplierType
        {
            get
            {
                if (ViewState["SupplierType"] != null)
                { m_SupplierType = Convert.ToString(ViewState["SupplierType"]); };
                return m_SupplierType;
            }
            set { ViewState["SupplierType"] = value; }

        }

        public event EventHandler SearchImageClicked;

        SupplierItemSearch supp = new SupplierItemSearch();
        static List<SupplierDetails> SupplierDetails;
        Suppliers objsupplier = new Suppliers();
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {

                //string scriptjs = "$(document).ready(function() {$('#" + txtSupplierPartNumber.ClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + txtSupplierPartNumber.ClientID + "').val(data[0]);$('#" + btnAutoComple.ClientID + "').click(); }});});";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);


            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                MPESupplier.Hide();
                if (Disable.ToUpper() != "TRUE")
                {
                    //Session["SupplierPartNumber"] = drpSuppPartNo.SelectedValue;
                    Session["SupplierPartNumber"] = txtSupplierPartNumber.Text;
                    Session["ItemCode"] = txtItemCode.Text;
                    onrest();
                    if (SearchImageClicked != null)
                        SearchImageClicked(this, EventArgs.Empty);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var btn = (Button)sender;
                string ClientID = btn.ClientID.Replace("btnReset", "");
                string PartNumberClientID = ClientID + "txtSupplierPartNumber";
                string AutoCompleteBtnClientID = ClientID + "btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
                MPESupplier.Show();
                onrest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void drpSuppPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                MPESupplier.Show();
                //var result = SupplierDetails.Find(p => p.Supplier_Part_Number == drpSuppPartNo.SelectedItem.Text);
                var result = SupplierDetails.Find(p => p.Supplier_Part_Number == txtSupplierPartNumber.Text);
                txtAppSegment.Text = result.Application_Segment_code;
                txtItemCode.Text = result.item_code;
                txtPackQnt.Text = result.packing_quantity;
                txtVehicleType.Text = result.Vehicle_Type_Code;

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void onrest()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                txtAppSegment.Text = "";
                txtItemCode.Text = "";
                txtPackQnt.Text = "";
                txtVehicleType.Text = "";
                //if (drpSuppPartNo.Items.Count > 0)
                //    drpSuppPartNo.SelectedIndex = 0;
                txtSupplierPartNumber.Text = "";
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void imgPopup_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //string script = "function StkItemSelectedHandler() {document.getElementById('" + btnAutoComple.ClientID + "').click()}";
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert", script, true);
            //AutoCompleteExtender1.OnClientItemSelected = "StkItemSelectedHandler";
            try
            {
                var btn = (ImageButton)sender;
                string ClientID = btn.ClientID.Replace("imgPopup", "");
                string PartNumberClientID = ClientID + "txtSupplierPartNumber";
                string AutoCompleteBtnClientID = ClientID + "btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
                BtnSubmit.Enabled = false;
                if (Mode == "4")
                {
                    hdnPartNoItemCode.Value = ItemCode.ToString();
                    if (hdnPartNoItemCode.Value != "")
                    {
                        using (ObjectDataSource objBranch = new ObjectDataSource())
                        {
                            objBranch.DataObjectTypeName = "IMPALLibrary.SupplierOrdLineDetails";
                            objBranch.TypeName = "IMPALLibrary.DirectPurchaseOrders";
                            objBranch.SelectMethod = "GetOrdSupplierLineDetails";
                            objBranch.SelectParameters.Add("ItemCode", hdnPartNoItemCode.Value);
                            objBranch.DataBind();

                            SupplierOrdLineDetails objSupplierLine = new SupplierOrdLineDetails();
                            object[] objSupplierLineSections = new object[0];
                            objSupplierLineSections = (object[])objBranch.Select();
                            objSupplierLine = (SupplierOrdLineDetails)objSupplierLineSections[0];

                            //drpSuppPartNo.Visible = false;
                            txtSupplierPartNumber.Visible = false;
                            drpSupplierLine.Visible = false;
                            txtSupplier.Visible = true;
                            UCTRRowPackage.Visible = false;
                            txtUCSuppPartNo.Visible = true;

                            txtItemCode.Text = g_Itemcode.ToString();
                            txtAppSegment.Text = objSupplierLine.Appln_Segment_Description.ToString();
                            txtVehicleType.Text = objSupplierLine.Vehicle_Type_Description.ToString();
                            txtUCSuppPartNo.Text = objSupplierLine.Supplier_Part_Number.ToString();
                            txtSupplier.Text = objSupplierLine.Supp_Short_description.ToString();


                            if (Disable.ToUpper() == "TRUE")
                            {
                                tablediv.Disabled = true;
                                btnReset.Visible = false;
                                BtnSubmit.Text = "Ok";
                            }

                        }
                        MPESupplier.Show();
                    }
                }
                else if (Mode != "3")
                {
                    drpSupplierLine.Visible = false;
                    txtSupplier.Visible = true;
                    //drpSuppPartNo.Visible = true;
                    txtSupplierPartNumber.Visible = true;
                    UCTRRowPackage.Visible = true;
                    txtUCSuppPartNo.Visible = false;
                    txtSupplier.Text = SupplierDesc;
                    Session["ItemPartSupplierline"] = SupplierLine;
                    SupplierDetails = supp.GetSupplierDetails(SupplierLine, Mode);
                    //LoadDropDownLists<IMPALLibrary.SupplierDetails>(SupplierDetails, drpSuppPartNo, "Supplier_Part_Number", "Supplier_Part_Number", true, "--Select PartNumber--");
                    MPESupplier.Show();

                    if (Disable.ToUpper() == "TRUE")
                    {
                        //drpSuppPartNo.SelectedValue = SupplierPartnumber;
                        //drpSuppPartNo_SelectedIndexChanged(this, new EventArgs());
                        txtSupplierPartNumber.Text = SupplierPartnumber;
                        btnAutoComple_Click(this, new EventArgs());
                        tablediv.Disabled = true;
                        btnReset.Visible = false;
                        BtnSubmit.Text = "Ok";
                    }

                }
                else
                {
                    drpSupplierLine.Visible = true;
                    txtSupplier.Visible = false;
                    //drpSuppPartNo.Visible = true;
                    txtSupplierPartNumber.Visible = true;
                    UCTRRowPackage.Visible = true;
                    txtUCSuppPartNo.Visible = false;
                    LoadDropDownLists<IMPALLibrary.Supplier>(SupplierType == "0" ? objsupplier.GetAllSupplierLines() : objsupplier.GetSuppliercodewithOutDefault(), drpSupplierLine, "SupplierCode", "SupplierName", false, "");
                    SupplierDetails = supp.GetSupplierDetails(drpSupplierLine.SelectedValue, SupplierType == "0" ? "1" : "2");
                    Session["ItemPartSupplierline"] = drpSupplierLine.SelectedValue;
                    //LoadDropDownLists<IMPALLibrary.SupplierDetails>(SupplierDetails, drpSuppPartNo, "Supplier_Part_Number", "Supplier_Part_Number", true, "--Select PartNumber--");
                    MPESupplier.Show();
                }
                Session["SupplierDetails"] = SupplierDetails;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

        protected void imgBtnPopupExit_Click(object sender, ImageClickEventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                MPESupplier.Hide();
                onrest();
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        private void LoadDropDownLists<T>(List<T> ListData, DropDownList DDlDropDown, string value_field, string text_field, bool bselect, string DefaultText)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                DDlDropDown.DataSource = ListData;
                DDlDropDown.DataTextField = text_field;
                DDlDropDown.DataValueField = value_field;
                DDlDropDown.DataBind();
                if (bselect.Equals(true))
                {
                    DDlDropDown.Items.Insert(0, DefaultText);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void drpSupplierLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var drpSupplierLine = (DropDownList)sender;
                string ClientID = drpSupplierLine.ClientID.Replace("drpSupplierLine", "");
                string PartNumberClientID = ClientID + "txtSupplierPartNumber";
                string AutoCompleteBtnClientID = ClientID + "btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
                MPESupplier.Show();
                SupplierDetails = supp.GetSupplierDetails(drpSupplierLine.SelectedValue, SupplierType == "0" ? "1" : "2");
                Session["ItemPartSupplierline"] = drpSupplierLine.SelectedValue;
                Session["SupplierDetails"] = SupplierDetails;
                onrest();

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        protected void btnAutoComple_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                var btn = (Button)sender;
                string ClientID = btn.ClientID.Replace("btnAutoComple", "");
                string PartNumberClientID = ClientID + "txtSupplierPartNumber";
                string AutoCompleteBtnClientID = ClientID + "btnAutoComple";
                string scriptjs = "$(document).ready(function() {$('#" + PartNumberClientID + "').autocomplete('" + Page.ResolveClientUrl("~/HandlerFile/Search_CS.ashx") + "').result(function(event, data, formatted) {if (data) {$('#" + PartNumberClientID + "').val(data[0]);$('#" + AutoCompleteBtnClientID + "').click(); }});});";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert434", scriptjs, true);
                MPESupplier.Show();
                var result = SupplierDetails.Find(p => p.Supplier_Part_Number == txtSupplierPartNumber.Text);
                txtAppSegment.Text = result.Application_Segment_code;
                txtItemCode.Text = result.item_code;
                txtPackQnt.Text = result.packing_quantity;
                txtVehicleType.Text = result.Vehicle_Type_Code;
                BtnSubmit.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                MPESupplier.Show();
                var result = SupplierDetails.Find(p => p.Supplier_Part_Number == txtSupplierPartNumber.Text);
                txtAppSegment.Text = result.Application_Segment_code;
                txtItemCode.Text = result.item_code;
                txtPackQnt.Text = result.packing_quantity;
                txtVehicleType.Text = result.Vehicle_Type_Code;
                BtnSubmit.Enabled = true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

    }


}