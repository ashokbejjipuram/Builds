<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChartAccount.ascx.cs"
    Inherits="IMPALWeb.UserControls.ChartAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div class="modalIconHolder">
    <asp:ImageButton ID="imgPopup" runat="server" SkinID="ImageButtonSearchPopup" ImageUrl="~/images/iHelpScreen.png"
        OnClientClick="LoadMain(this);" />
    <ajax:ModalPopupExtender ID="MPESupplier" runat="server" TargetControlID="imgPopup"
        PopupControlID="divpopup" DropShadow="false" BackgroundCssClass="modalPopupBackground">
    </ajax:ModalPopupExtender>
</div>
<div id="divpopup" class="divPopup" runat="server" style="display: none">
    <div class="modalImageExit">
        <asp:ImageButton ID="imgBtnPopupExit" runat="server" OnClientClick="return ClosePopup(this);"
            SkinID="ImageButtonSearchPopup" ImageUrl="~/images/iCloseGray.png" />
    </div>
    <div class="modalExtenderTitle">
        ChartAccount Details</div>
    <div>
        <table class="modalExtenderTable">
            <tr>
                <td class="label">
                    <asp:Label ID="lblGlMain" SkinID="LabelNormalPopup" Text="GL main" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <select id="drpGlMain" runat="server" class="dropDownListNormalPopup" onchange="LoadSub(this);">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblGlSub" SkinID="LabelNormalPopup" Text="GL Sub" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <select id="drpGlSub" runat="server" class="dropDownListNormalPopup" onchange="LoadAcc(this);">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblGlAccount" SkinID="LabelNormalPopup" Text="GL Account" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <select id="drpGlAccount" runat="server" class="dropDownListNormalPopup" onchange="LoadBranch(this);">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblBranch" SkinID="LabelNormalPopup" Text="Branch" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <select id="drpBranch" runat="server" class="dropDownListNormalPopup" onchange="LoadChartAccountCode(this);">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblGlGroup" SkinID="LabelNormalPopup" Text="GL Group" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:Label ID="txtGlGroup" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblGlClass" SkinID="LabelNormalPopup" Text="GL Classfication" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:Label ID="txtGlclass" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblChatAccCode" SkinID="LabelNormalPopup" Text="Chart Of Account Code"
                        runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:Label ID="txtChatAccCode" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
         <div id="divCustomerDetails" runat="server">
        <div class="modalExtenderTitle">
            Customer Information</div>
        <table class="modalExtenderTable">
            <tr>
                <td class="label">
                    <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="lblAddress1" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblAddress2" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="lblAddress3" runat="server" SkinID="LabelNormal" Text="Address3"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblAddress4" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="lblLocation" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
        <div class="modalExtenderButtons">
            <div class="modalExtenderButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" OnClientClick="return SubmitChart(this);"
                    Text="Submit" SkinID="ButtonNormalPopup" OnClick="BtnSubmit_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormalPopup"
                    OnClientClick="return ResetPopup(this);" />
            </div>
        </div>
    </div>
   
    <asp:HiddenField ID="AshxPath" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hddFilter" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hddDefaultBranch" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hddBranch" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hddChartAccount" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hddDse" runat="server"></asp:HiddenField>
</div>
