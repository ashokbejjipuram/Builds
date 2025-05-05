<%@ Page Title="PO Indent-CWH" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BulkStdnUploadHO.aspx.cs" Inherits="IMPALWeb.BulkStdnUploadHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <style type="text/css">
        .headstyle
        {
            background-color: #666;
            color: #ffffff;
            padding: 5px 5px 5px 5px;
            border: none;
            font-family: Tahoma, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            text-transform: capitalize;
            text-align: center;
        }
        .FixedHeader
        {
            position: absolute;
            font-weight: bold;
        }
    </style>

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

		function CheckExcelformat() {
		    var Filename = document.getElementById(CtrlIdPrefix + "btnFileUpload");

		    if (Filename.value == "" || Filename.value == null) {
		        alert('Please select an Excel file');
		        Filename.focus();
		        return false;
		    }
		}
    </script>
    <div id="DivTop" runat="server" style="width: 100%">
        <asp:UpdatePanel ID="UpdPanelHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="subFormTitle subFormTitleExtender350">
                    BULK STDN UPLOAD - HO</div>
                <div id="divItemDetailsExcel" style="text-align: center" runat="server">
                    <div class="subFormTitle">
                        File Upload</div>
                    <table class="subFormTable">
                        <tr id="row1" runat="server">
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:FileUpload runat="server" ID="btnFileUpload" />
                            </td>
                            <td>
                                <asp:Button ID="btnUploadExcel" runat="server" Text="Upload File" SkinID="ButtonNormal"
                                    OnClick="btnUploadExcel_Click" OnClientClick="return CheckExcelformat();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label runat="server" ID="FileStatusMsg"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />                        
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnUploadExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
