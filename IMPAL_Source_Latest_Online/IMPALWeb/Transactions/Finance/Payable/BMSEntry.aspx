<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BMSEntry.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.BMSEntry" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";
        
        function ValidationSubmit() {

            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
            if (supplier.value == "" || supplier.value == "0") {
                alert("Supplier should not be null");
                supplier.focus();
                return false;
            }

            var bmsnumber = document.getElementById(CtrlIdPrefix + "txtBMSNumber");
            if (bmsnumber.value == "") {
                alert("BMS Number should not be null");
                bmsnumber.focus();
                return false;
            }

            var bmsdate = document.getElementById(CtrlIdPrefix + "txtBMSDate");
            if (bmsdate.value == "") {
                alert("BMS Date should not be null");
                bmsdate.focus();
                return false;
            }

            var bmsduedate = document.getElementById(CtrlIdPrefix + "txtBMSDueDate");
            if (bmsduedate.value == "") {
                alert("BMS Due Date should not be null");
                bmsduedate.focus();
                return false;
            }

            var bmsamount = document.getElementById(CtrlIdPrefix + "txtBMSAmount");
            if (bmsamount.value == "") {
                alert("BMS Amount should not be null");
                bmsamount.focus();
                return false;
            }

            var bankname = document.getElementById(CtrlIdPrefix + "txtBankName");
            if (bankname.value == "") {
                alert("Bank Name should not be null");
                bankname.focus();
                return false;
            }
        }

        function checkDateBMSDueDate(id) {

            var idDate = document.getElementById(id).value;

            if (idDate != '') {
                var status = fnIsDate(idDate);

                if (!status) {
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
                else {

                    var idFromDate = document.getElementById("ctl00_CPHDetails_txtBMSDate").value

                    var toDate = new Date();
                    toDate = convertDate(idDate);

                    var frmDate = new Date();
                    frmDate = convertDate(idFromDate);

                    if (toDate < frmDate) {
                        document.getElementById(id).value = "";
                        alert("BMS Due Date should be greater than or equal to BMS Date");
                    }
                }
            }
        }

        function convertDate(id) {
            var date = id.split("/");
            var day = parseInt(date[0]);
            var month = parseInt(date[1] - 1);
            var year = parseInt(date[2]);

            var validationDate = new Date(year, month, day);

            return validationDate;
        }

        function checkDateBMSDate(id) {

            var idDate = document.getElementById(id).value;

            if (idDate != '') {
                var status = fnIsDate(idDate);

                if (!status) {
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
                else {

                    var toDate = new Date();
                    toDate = convertDate(idDate);

                    var date = new Date();
                    date.setHours(0);
                    date.setMinutes(0);
                    date.setSeconds(0);
                    date.setMilliseconds(0);

                    if (toDate > date) {
                        document.getElementById(id).value = "";
                        alert("BMS Due Date should be less than or equal to System Date");
                    }
                }
            }
        }
        
    </script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        BMS ENTRY</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblHOBMSNumber" runat="server" Text="HO BMS Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtHOBMSNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                                <asp:DropDownList ID="ddlHOBMSNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlHOBMSNumber_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
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
                                <asp:Label ID="lblBMSNumber" runat="server" Text="BMS Number" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBMSNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBMSDate" runat="server" Text="BMS Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBMSDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return checkDateBMSDate(this.id);"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceBMSDate" PopupButtonID="imgBMSDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtBMSDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBMSDueDate" runat="server" Text="BMS Due Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBMSDueDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return checkDateBMSDueDate(this.id);"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceBMSDueDate" PopupButtonID="imgBMSDueDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtBMSDueDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBMSAmount" runat="server" Text="BMS Amount" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBMSAmount" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankName" runat="server" Text="Bank Name" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBankName" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="label" align="center">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="label" >
                                <asp:Label ID="lblIsExists" Text="" runat="server" SkinID="Error" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                        CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                        SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetSuppliers"
        TypeName="IMPALLibrary.Payable" DataObjectTypeName="IMPALLibrary.Payable"></asp:ObjectDataSource>
</asp:Content>
