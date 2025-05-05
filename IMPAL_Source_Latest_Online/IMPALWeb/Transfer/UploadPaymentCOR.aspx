<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="UploadPaymentCOR.aspx.cs" Inherits="IMPALWeb.Transfer.UploadPaymentCOR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntUpload" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divUpload" runat="server">
                    <asp:Literal ID="ltrProgressBar" runat="server"></asp:Literal>
                    <div class="subFormTitle subFormTitleExtender350">
                        Corporate UPLOAD For Payment</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDownloadDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDownloadDate" runat="server" SkinID="TextBoxDisabled" contentEditable="false"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnDownloadDate" ImageUrl="~/Images/Calendar.png" alt="Calendar"
                                    runat="server" SkinID="ImageButtonCalendar" />
                                <ajaxToolkit:CalendarExtender ID="calExtDownloadDate" EnableViewState="true" PopupPosition="BottomLeft"
                                    PopupButtonID="imgBtnDownloadDate" TargetControlID="txtDownloadDate" Format="dd/MM/yyyy"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                                <asp:Button ID="ResetButton" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="ResetButton_Click"
                                    Visible="false" />
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <table class="subFormTable">
                        <tr>
                            <td>
                                <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                                <asp:Label ID="lblUploadError" runat="server" Text="" SkinID="Error" Visible="false"></asp:Label>
                            </td>
                        </tr>
                </div>
                <div class="transactionButtons" id="Transbuttons" runat="server">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnRegularUpload" runat="server" Text="Regular" SkinID="ButtonNormal"
                            OnClick="btnRegularUpload_Click" OnClientClick="javaScript:return Validate();" />
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function Validate() {
            var txtDate = document.getElementById('<%=txtDownloadDate.ClientID%>');
            return CheckDateRequired(txtDate);
        }
    </script>

</asp:Content>
