<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Upload.aspx.cs" Inherits="IMPALWeb.Transfer.Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntUpload" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function Validate() {
            var txtDate = document.getElementById('<%=txtDownloadDate.ClientID%>');
            if (txtDate.value == "" || txtDate.value == null){
                alert("Please Select a Date");
                return false;
            }
        
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
                        Upload Branch Data</div>
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
                        </tr>
                    </table>
                    <div id="divItemDetailsExcel" style="text-align: center" runat="server">
                        <div class="subFormTitle">
                            File Upload</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:FileUpload runat="server" ID="btnFileUpload" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" SkinID="ButtonNormal"
                                        OnClick="btnUploadFile_Click" OnClientClick="javaScript:return Validate();" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <asp:Label runat="server" ID="FileStatusMsg"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="transactionButtons" id="Transbuttons" runat="server">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnUploadFile" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
