<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="UploadOrder.aspx.cs" Inherits="IMPALWeb.Transfer.UploadOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntUpload" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function ValidateFile() {
            var status = document.getElementById('<%=btnFileUpload.ClientID%>');
            if (status.value == "" || status.value == null){
                alert("Please Select a File");
                return false;
            }
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divUpload" runat="server">
                    <asp:Literal ID="ltrProgressBar" runat="server"></asp:Literal>
                    <div class="subFormTitle">
                        Upload - HO Order</div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="2">
                                <font size="4" color="red"><b>Please Keep the File in "D:\Downloads\HoOrder" Folder and Upload
                                    the Same.</b></font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:FileUpload runat="server" ID="btnFileUpload" />
                            </td>
                        </tr>
                    </table>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="3">
                                <asp:Label runat="server" ID="FileStatusMsg"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="transactionButtons" id="Transbuttons" runat="server">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnHOOrderUpload" runat="server" Text="Upload HO - Order" SkinID="ButtonNormalBig"
                            OnClick="btnHOOrderUpload_Click" OnClientClick="javaScript:return ValidateFile();" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnHOOrderUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
