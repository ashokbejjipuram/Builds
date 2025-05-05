<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="HOSFLPayment.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.HOSFLPayment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HoSFLPaymentInvDetails.ascx" TagName="HoSFLPaymentInvDetails"
    TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";
        
        function CalculateTotal() {
            debugger;
            var invoiceamount = 0;
            var totamount = 0;

            var grd = document.getElementById(CtrlIdPrefix + "grvHOSFLPaymentDetails");
            document.getElementById(CtrlIdPrefix + "txtCalculatedAmount").value = 0;

            if ('<%=Session["hdnTotVal"]%>' != "") {
                totamount = '<%=Session["hdnTotVal"]%>';
            }

            for (i = 1; i < grd.rows.length - 1; i++) {
                var browserName = navigator.appName;

                if (browserName == 'Netscape') {
                    var node1 = grd.rows[i].cells[3].childNodes[1];
                    var node2 = grd.rows[i].cells[1].childNodes[1];
                    var node3 = grd.rows[i].cells[0].childNodes[1];
                }
                else {
                    var node1 = grd.rows[i].cells[3].childNodes[0];
                    var node2 = grd.rows[i].cells[1].childNodes[0];
                    var node3 = grd.rows[i].cells[0].childNodes[0];
                }

                if (node2.childNodes[0] != undefined && node2.childNodes[0].type == "checkbox")
                    if (node2.childNodes[0].checked == true) {
                    if (node1 != undefined && node1.type == "text") {
                        if (!isNaN(node1.value) && node1.value != "")
                            invoiceamount += parseFloat(node1.value);
                    }
                    node3.disabled = false;
                }
                else
                    node3.disabled = true;
            }
            document.getElementById(CtrlIdPrefix + "txtCalculatedAmount").value = parseFloat(Math.round(invoiceamount * 100) / 100).toFixed(2);
            document.getElementById(CtrlIdPrefix + "txtTotalAmount").value = (parseFloat(totamount) + parseFloat(Math.round(invoiceamount * 100) / 100)).toFixed(2);
            PageMethods.SetSessionTotvalue((parseFloat(totamount) + parseFloat(Math.round(invoiceamount * 100) / 100)).toFixed(2));
        }

        function CalculateTotalHO(id) {
            debugger;
            var invoiceamount = 0;
            var cdamount = 0;
            var invoiceNumbers = "'";
            var Cnt = 0;

            var grdid = id.split("grvHOSFLPaymentInvoiceDetails");
            var grd = document.getElementById(grdid[0] + "grvHOSFLPaymentInvoiceDetails");

            for (i = 1; i < grd.rows.length - 1; i++) {
                var browserName = navigator.appName;

                if (browserName == 'Netscape') {
                    var node1 = grd.rows[i].cells[4].childNodes[1];
                    var node2 = grd.rows[i].cells[0].childNodes[1];
                    var node3 = grd.rows[i].cells[5].childNodes[1];
                    var node4 = grd.rows[i].cells[2].childNodes[1];
                }
                else {
                    var node1 = grd.rows[i].cells[4].childNodes[0];
                    var node2 = grd.rows[i].cells[0].childNodes[0];
                    var node3 = grd.rows[i].cells[5].childNodes[0];
                    var node4 = grd.rows[i].cells[2].childNodes[0];
                }

                if (node2.childNodes[0] != undefined && node2.childNodes[0].type == "checkbox")
                    if (node2.childNodes[0].checked == true) {
                    if (node1 != undefined && node1.type == "text") {
                        if (!isNaN(node1.value) && node1.value != "") {
                            invoiceamount += parseFloat(node1.value);
                            cdamount += parseFloat(node3.value);
                            invoiceNumbers += node4.value + "','";
                            Cnt += 1;
                        }
                    }
                    else
                        Cnt -= 1;
                }
            }

            document.getElementById(grdid[0] + "txtAdjTotal").value = parseFloat(Math.round(invoiceamount * 100) / 100).toFixed(2);
            document.getElementById(grdid[0] + "txtCDTotal").value = parseFloat(Math.round(cdamount * 100) / 100).toFixed(2);
            document.getElementById(grdid[0] + "hdnCnt").value = Cnt;
            document.getElementById(grdid[0] + "hdnInvoiceNumbers").value = invoiceNumbers + "'";
        }

        function showHOSFLPaymentChild(id) {
            var branchid = id.replace("editHOSFLPaymentDet_btnEdit", "txtBranch");
            var amountid = id.replace("editHOSFLPaymentDet_btnEdit", "txtInvoiceAmount");
            var cdamtid = id.replace("editHOSFLPaymentDet_btnEdit", "txtCDAmount");
            var branch = document.getElementById(branchid).value;
            var amount = Math.round((document.getElementById(amountid).value) * 100) / 100;
            var cdamt = Math.round((document.getElementById(cdamtid).value) * 100) / 100;
            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier").value;
            var fromdate = document.getElementById(CtrlIdPrefix + "txtInvoiceFromDate").value;
            var todate = document.getElementById(CtrlIdPrefix + "txtInvoiceToDate").value;

            PageMethods.SetSessionvalues(branch, amount, cdamt, supplier, fromdate, todate);
        }

        function ValidateClick(id) {
            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
            if (supplier.value == "" || supplier.value == "0") {
                alert("Supplier should not be null");
                supplier.focus();
                return false;
            }

            var zone = document.getElementById(CtrlIdPrefix + "ddlZone");
            if (zone.value == "" || zone.value == "0") {
                alert("Zone should not be null");
                zone.focus();
                return false;
            }

            var FromDt = document.getElementById(CtrlIdPrefix + "txtInvoiceFromDate");
            if (FromDt.value == "" || FromDt.value == "0") {
                alert("Invoice From Date should not be null");
                FromDt.focus();
                return false;
            }

            var status = fnIsDate(FromDt.value);

            if (!status) {
                FromDt.value = "";
                FromDt.focus();
                return false;
            }

            var ToDt = document.getElementById(CtrlIdPrefix + "txtInvoiceToDate");
            if (FromDt.value == "" || FromDt.value == "0") {
                alert("Invoice To Date should not be null");
                ToDt.focus();
                return false;
            }

            var status = fnIsDate(ToDt.value);

            if (!status) {
                ToDt.value = "";
                ToDt.focus();
                return false;
            }

            if (id == 2 || id == 3) {
                var zoneamount = document.getElementById(CtrlIdPrefix + "txtCalculatedAmount");
                if (zoneamount.value == "0" || zoneamount.value == "") {
                    alert("Payment Amount Should Not Less then Zero.");
                    return false;
                }

                ConfirmProcess(id);
            }
        }

        function ConfirmProcess(id) {
            if (id == 3) {
                alert("All the Zones have been done...Submitting Now");
                document.getElementById(CtrlIdPrefix + "HdnConfirmvalue").value = "No";
                return true;
            }
            else {
                var confirm_value = document.getElementById(CtrlIdPrefix + "HdnConfirmvalue");
                if (confirm("Do You Want To Proceed Another Zone ?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        HEADOFFICE SFL PAYMENT</div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblZone" runat="server" Text="Zone" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlZone" runat="server" DataSourceID="ODS_Zone" DataTextField="ZoneName"
                                    DataValueField="ZoneCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Calculated Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCalculatedAmount" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTotalAmount" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceFromDate" runat="server" Text="Invoice - From Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceInvoiceFromDate" PopupButtonID="imgInvoiceFromDate"
                                    Format="dd/MM/yyyy" runat="server" TargetControlID="txtInvoiceFromDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceToDate" runat="server" Text="Invoice - To Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceInvoiceToDate" PopupButtonID="imgInvoiceToDate"
                                    Format="dd/MM/yyyy" runat="server" TargetControlID="txtInvoiceToDate" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClick" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS</div>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvHOSFLPaymentDetails" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" SkinID="GridViewTransaction">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <uc2:HoSFLPaymentInvDetails runat="server" DefaultBranch="true" ID="editHOSFLPaymentDet"
                                        OnSearchImageClicked="ucHoSFLPaymentInvDetails_SearchImageClicked" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Selected">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelected" runat="server" Style="width: 30px !important;" SkinID="GridViewTextBoxSmall"
                                        OnClick="CalculateTotal();" Checked='<%# Bind("Selected") %>'></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBranch" runat="server" SkinID="GridViewTextBoxSmall" Text='<%# Bind("BranchCode") %>'
                                        Enabled="false"> </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("InvoiceAmount") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CD Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCDAmount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("CDAmount") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnClick" runat="server" ValidationGroup="btnClick" SkinID="ButtonNormal"
                            Enabled="true" CausesValidation="true" Text="Click" OnClick="btnClick_Click"
                            OnClientClick="javaScript:return ValidateClick(1);" />
                        <asp:Button ID="btnProcess" runat="server" ValidationGroup="btnProcess" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Process" OnClick="btnProcess_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="btnClick" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />                            
                        <asp:HiddenField ID="HdnConfirmvalue" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSFSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Zone" runat="server" SelectMethod="LoadZoneMaster"
        TypeName="IMPALLibrary.Payable" DataObjectTypeName="IMPALLibrary.Payable"></asp:ObjectDataSource>
</asp:Content>