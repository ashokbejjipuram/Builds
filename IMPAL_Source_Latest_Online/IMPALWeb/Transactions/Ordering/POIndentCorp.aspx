<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="POIndentCorp.aspx.cs"
    Inherits="IMPALWeb.Ordering.POIndentCorp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript">
        function ConfirmSub() {
            var Id = "ctl00_CPHDetails_";
            var PO_Number = document.getElementById(Id + "ddlWorkSheetList").value;

            if (PO_Number != "" && PO_Number != "0") {
                var Available_Balance = document.getElementById(Id + "hdnAvailableBalance").value;
                var Branch_PO_Value = document.getElementById(Id + "hdnBranchPOValue").value;
                var diff = Number(Branch_PO_Value) >= Number(Available_Balance);
                if (diff) {
                    if (confirm("Ordering Amount Exceeds than Alloted Amount.Get a Permission with Ordering Dept. Do you want to Proceed further ?"))
                        document.getElementById(Id + "confirm_value").value = "Yes";
                }
                else
                    document.getElementById(Id + "confirm_value").value = "No";
            }
            else {
                alert("WorkSheet Number is Not Available");
                document.getElementById(Id + "confirm_value").value = "No";
            }
        }
    </script>

    <script src="../../Javascript/DirectPOcommon.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle subFormTitleExtender350">
                            Central CyberWarehouse [Corp]</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="LblBrCode" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlBrCodeList" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlBrCodeList_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="LblLinecode" runat="server" SkinID="LabelNormal" Text="Line Code"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlLineCodeList" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlLineCodeList_SelectedIndexChanged" TabIndex="2">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFromDt" SkinID="LabelNormal" runat="server" Text="From Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFromDt" runat="server" AutoPostBack="true" SkinID="TextBoxCalendarExtenderNormal"
                                        TabIndex="3" OnTextChanged="txtFromDt_TextChanged" onblur="return CheckValidDate(this.id, true,'From Date');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDt" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToDt" SkinID="LabelNormal" runat="server" Text="To Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtToDt" runat="server" AutoPostBack="true" SkinID="TextBoxCalendarExtenderNormal"
                                        TabIndex="4" OnTextChanged="txtToDt_TextChanged" onblur="return CheckValidDate(this.id, true,'To Date');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtToDt" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="LblWorkSheet" runat="server" SkinID="LabelNormal" Text="WorkSheet No#"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlWorkSheetList" runat="server" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlWorkSheetList_SelectedIndexChanged" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlBrCodeList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlLineCodeList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlWorkSheetList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtFromDt" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtToDt" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <asp:UpdatePanel ID="UpdatePanelTranBtn" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Process" SkinID="ButtonNormal" TabIndex="6"
                            OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" TabIndex="7"
                            OnClick="btnReset_Click" />
                        <asp:HiddenField ID="hdnAvailableBalance" runat="server" />
                        <asp:HiddenField ID="hdnBranchPOValue" runat="server" />
                        <asp:HiddenField ID="confirm_value" runat="server" Value="No" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
