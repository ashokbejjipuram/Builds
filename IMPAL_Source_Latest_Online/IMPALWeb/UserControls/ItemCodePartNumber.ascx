<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemCodePartNumber.ascx.cs"
    Inherits="IMPALWeb.UserControls.ItemCodePartNumber" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div class="modalIconHolder">
    <asp:ImageButton ID="imgPopup" runat="server" ImageUrl="~/images/iHelpScreen.png"
        OnClick="imgPopup_Click" SkinID="ImageButtonSearchPopup" />
    <asp:Button ID="imgPopupHidden" Style="display: none" runat="server" />
    <ajax:ModalPopupExtender ID="MPESupplier" runat="server" TargetControlID="imgPopupHidden"
        PopupControlID="divPopup" DropShadow="false" BackgroundCssClass="modalPopupBackground">
    </ajax:ModalPopupExtender>
</div>
<div id="divPopup" class="divPopup" runat="server" style="display: none">
    <div class="modalImageExit">
        <asp:ImageButton ID="imgBtnPopupExit" runat="server" SkinID="ImageButtonSearchPopup"
            ImageUrl="~/images/iCloseGray.png" OnClick="imgBtnPopupExit_Click" />
    </div>
    <div class="modalExtenderTitle">
        Supplier wise Item code Query</div>
    <table id="tablediv" runat="server" class="modalExtenderTable">
        <tr id="UCTRRowSupplier" runat="server">
            <td class="label">
                <asp:Label ID="lblSupplier" SkinID="LabelNormalPopup" Text="Supplier" runat="server"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtSupplier" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:DropDownList ID="drpSupplierLine" SkinID="DropDownListNormalPopup" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSuppPartNo" Text="Supplier Part Number" SkinID="LabelNormalPopup"
                    runat="server"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtUCSuppPartNo" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:TextBox ID="txtSupplierPartNumber" SkinID="TextBoxNormalPopup" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAppSegment" SkinID="LabelNormalPopup" Text="Application Segment"
                    runat="server"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtAppSegment" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblVehicleType" SkinID="LabelNormalPopup" Text="Vehicle Type" runat="server"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtVehicleType" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr id="UCTRRowPackage" runat="server">
            <td class="label">
                <asp:Label ID="lblPackQnt" SkinID="LabelNormalPopup" Text="Packing Quantity" runat="server"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtPackQnt" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemCode" SkinID="LabelNormalPopup" Text="Item Code" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtItemCode" SkinID="TextBoxNormalPopup" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:HiddenField ID="hdnPartNoItemCode" runat="server"></asp:HiddenField>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAutoComple" runat="server" Style="display: none;" OnClick="btnAutoComple_Click" />
    <div class="modalExtenderButtons">
        <div class="modalExtenderButtonsHolder">
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormalPopup"
                OnClick="BtnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormalPopup"
                OnClick="btnReset_Click" />
        </div>
    </div>
</div>
