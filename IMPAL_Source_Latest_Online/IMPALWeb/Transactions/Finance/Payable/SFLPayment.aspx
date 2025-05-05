<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="SFLPayment.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.SFLPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntUpload" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

        function ValidationSubmit() {
            var btnUpload = document.getElementById(CtrlIdPrefix + "btnFileUpload");
            if (btnUpload.value == "" || btnUpload.value == null) {
                alert("Please Select a File");
                btnUpload.focus();
                return false;
            }
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
            <ContentTemplate>
                <div id="divUpload" runat="server">
                    <div class="reportFormTitle reportFormTitleExtender350">
                        SFL Payment</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:FileUpload runat="server" ID="btnFileUpload" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" colspan="4">
                                <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" colspan="4">
                                <asp:Label ID="lblErrorMessage" runat="server" Text="" SkinID="LabelNormal" Visible="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnUpload" runat="server" Text="SFL Corp Upload" SkinID="ButtonNormalBig"
                                OnClientClick="javaScript:return ValidationSubmit();" OnClick="btnUpload_Click" />
                            <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                                SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersBMS"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
</asp:Content>
