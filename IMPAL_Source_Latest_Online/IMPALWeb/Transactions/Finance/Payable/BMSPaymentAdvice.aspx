<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BMSPaymentAdvice.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.BMSPaymentAdvice" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">

        function checkDateToDate(id) {

            var idDate = document.getElementById(id).value;

            if (idDate != '') {
                var status = fnIsDate(idDate);

                if (!status) {
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
                else {


                    var idFromDate = document.getElementById("ctl00_CPHDetails_txtfromdate").value

                    var toDate = new Date();
                    toDate = convertDate(idDate);

                    var frmDate = new Date();
                    frmDate = convertDate(idFromDate);

                    var date = new Date();
                    date.setHours(0);
                    date.setMinutes(0);
                    date.setSeconds(0);
                    date.setMilliseconds(0);

                    if (toDate < frmDate) {
                        document.getElementById(id).value = "";
                        alert("To Date should be greater than or equal to From Date");
                    }

                    if (toDate > date) {
                        document.getElementById(id).value = "";
                        alert("To Date should not be greater than System Date");
                    }
                }
            }
        }

        function checkDateFromDate(id) {
            document.getElementById("ctl00_CPHDetails_txtToDate").value = '';

            var idDate = document.getElementById(id).value;

            if (idDate != '') {
                var status = fnIsDate(idDate);

                if (!status) {
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
                else {

                    var date = new Date();
                    date.setHours(0);
                    date.setMinutes(0);
                    date.setSeconds(0);
                    date.setMilliseconds(0);

                    var toDate = new Date();
                    toDate = convertDate(idDate);

                    if (toDate > date) {
                        document.getElementById(id).value = "";
                        alert("From Date should not be greater than System Date");
                    }
                }
            }
        }

        function convertDate(id) {
            var date = id.split("/");

            var dayy = date[0];
            dayy.indexOf('0') == 0 ? dayy = dayy.replace('0', '') : dayy;
            var day = parseInt(dayy);
            var month = parseInt(date[1] - 1);
            var year = parseInt(date[2]);

            var validationDate = new Date(year, month, day);

            return validationDate;
        }

        function CalculateTotal() {

            var bmsamount = 0;
            var cdamount = 0;
            var totalamount = 0;
            var node5;
            var node6;
            var node7;

            var grd = document.getElementById('ctl00_CPHDetails_grvItemDetails');
            document.getElementById("ctl00_CPHDetails_txtTotalAmount").value = 0;

            for (i = 1; i < grd.rows.length-1; i++) {
                var node1 = grd.rows[i].cells[4].childNodes[0];
                var node2 = grd.rows[i].cells[0].childNodes[0];
                var node3 = grd.rows[i].cells[6].childNodes[0];
                var node4 = grd.rows[i].cells[7].childNodes[0];
                node5 = grd.rows[parseInt(grd.rows.length) - 1].cells[4].childNodes[0];
                node6 = grd.rows[parseInt(grd.rows.length) - 1].cells[6].childNodes[0];
                node7 = grd.rows[parseInt(grd.rows.length) - 1].cells[7].childNodes[0];

                var browserName = navigator.appName;
                if (browserName == 'Netscape') {
                    var node1 = grd.rows[i].cells[4].childNodes[1];
                    var node2 = grd.rows[i].cells[0].childNodes[1];
                    var node3 = grd.rows[i].cells[6].childNodes[1];
                    var node4 = grd.rows[i].cells[7].childNodes[1];
                    node5 = grd.rows[parseInt(grd.rows.length) - 1].cells[4].childNodes[1];
                    node6 = grd.rows[parseInt(grd.rows.length) - 1].cells[6].childNodes[1];
                    node7 = grd.rows[parseInt(grd.rows.length) - 1].cells[7].childNodes[1];
                }

                if (node2.childNodes[0] != undefined && node2.childNodes[0].type == "checkbox")
                    if (node2.childNodes[0].checked == true) {

                    if (node1 != undefined && node1.type == "text") {
                        if (!isNaN(node1.value) && node1.value != "")
                            bmsamount += parseFloat(node1.value);
                    }

                    if (node3 != undefined && node3.type == "text") {
                        if (!isNaN(node3.value) && node3.value != "")
                            cdamount += parseFloat(node3.value);
                    }

                    if (node4 != undefined && node4.type == "text") {
                        if (!isNaN(node4.value) && node4.value != "")
                            totalamount += parseFloat(node4.value);
                    }
                }
            }

            document.getElementById("ctl00_CPHDetails_txtTotalAmount").value = parseFloat(Math.round(totalamount * 100) / 100).toFixed(2);
            document.getElementById(node5.id).value = parseFloat(Math.round(bmsamount * 100) / 100).toFixed(2);
            document.getElementById(node6.id).value = parseFloat(Math.round(cdamount * 100) / 100).toFixed(2);
            document.getElementById(node7.id).value = parseFloat(Math.round(totalamount * 100) / 100).toFixed(2);
        }

        function ValidationSubmit() {

            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
            if (supplier.value == "" || supplier.value == "0") {
                alert("Supplier Code should not be null");
                supplier.focus();
                return false;
            }

            var fromdate = document.getElementById(CtrlIdPrefix + "txtfromdate");
            if (fromdate.value == "") {
                alert("From Date should not be null");
                fromdate.focus();
                return false;
            }

            var todate = document.getElementById(CtrlIdPrefix + "txtToDate");
            if (todate.value == "") {
                alert("To Date should not be null");
                todate.focus();
                return false;
            }
        }

        function ValidationProcess() {
            ValidationSubmit();

            var totalamount = document.getElementById(CtrlIdPrefix + "txtTotalAmount");
            if (parseFloat(totalamount.value) <= 0) {
                alert("Bms Couldn't Generate for Nil / Negative Payment");
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        BMS PAYMENT ADVICE SLIP</div>
                    <table class="subFormTable" id="idHeader" runat="server">
                        <tr>
                            <td colspan="4" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierCode" runat="server" Text="Supplier Code" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTotalAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="BMS From Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtfromdate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                    onblur="return checkDateFromDate(this.id);"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtfromdate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="BMS To Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                    onblur="return checkDateToDate(this.id);"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnDetails" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS</div>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        SkinID="GridViewTransaction">
                        <Columns>
                            <asp:TemplateField HeaderText="Selected">
                                <ItemTemplate>
                                    <asp:CheckBox ID="txtSelected" runat="server" Style="width: 30px !important;" SkinID="GridViewTextBoxSmall"
                                        OnClick="CalculateTotal();" Checked='<%# Bind("Selected") %>'></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BMS Number #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBMSNumber" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("BMSNumber") %>'
                                        Enabled="false"> </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BMS Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBMSDate" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("BMSDate") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BMS Due Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBMSDueDate" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("BMSDueDate") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BMS Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBMSAmount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("BMSAmount") %>'
                                        Enabled="false" TabIndex="0"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalBMSAmount" runat="server" SkinID="GridViewTextBox" Text="0"
                                        Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Of Days">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNoofDays" runat="server" SkinID="GridViewTextBox" Style="width: 80px !important;"
                                        Text='<%# Bind("NoofDays") %>' Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CD Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCDValue" runat="server" Text='<%# Bind("CDAmount") %>' SkinID="GridViewTextBox"
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalCDValue" runat="server" SkinID="GridViewTextBox" Text="0"
                                        Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalValue" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("BMSValue") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalValue" runat="server" SkinID="GridViewTextBox" Text="0"
                                        Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnDetails" runat="server" ValidationGroup="btnDetails" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Details" OnClick="btnDetails_Click" />
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetSuppliers"
        TypeName="IMPALLibrary.Payable" DataObjectTypeName="IMPALLibrary.Payable"></asp:ObjectDataSource>
</asp:Content>
