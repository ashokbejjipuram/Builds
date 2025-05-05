<%@ Page Title="PO Indent-CWH" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="POIndentCWH.aspx.cs" Inherits="IMPALWeb.POIndentCWH" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <style type="text/css">
        .headstyle {
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

        .FixedHeader {
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

            if (Filename.value.slice(Filename.value.length - 5) == ".xlsx") {
                alert('Please select a Valid 97-2003 Format Excel file');
                return false;
            }

            //alert(Filename.value.slice(Filename.value.length - 4));
            if (Filename.value.slice(Filename.value.length - 4) != ".xls") {
                alert('Please select a Valid Excel file');
                return true;
            }
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=PanelHeaderDtls.ClientID%>').disabled = true;

            window.document.forms[0].target = '_blank';
        }
    </script>
    <div id="DivTop" runat="server" style="width: 100%">
        <asp:UpdatePanel ID="UpdPanelHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="PanelHeaderDtls" runat="server">
                    <div class="subFormTitle">
                        PO INDENT CWH
                    </div>
                    <div class="subFormTitle">
                        INDENT DETAILS
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblIndentType" runat="server" SkinID="LabelNormal" Text="Indent Type"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlIndentType" runat="server" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlIndentType_SelectedIndexChanged"
                                    AutoPostBack="true" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblIndentNumber" runat="server" SkinID="LabelNormal" Text="Indent Number"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlIndentNumber" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">&nbsp;
                            </td>
                            <td class="inputcontrols">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetailsExcel" style="text-align: center" runat="server">
                        <div class="subFormTitle">
                            File Upload
                        </div>
                        <table class="subFormTable">
                            <tr>
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
                </asp:Panel>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Process" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                        <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" />
                        <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" />
                        <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" />
                    </div>
                </div>
                <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                    <UC:CrystalReport runat="server" ID="crPurchaseOrderWorkSheet" ReportName="Purchase_worksheet" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlIndentType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnReportPDF" />
                <asp:PostBackTrigger ControlID="btnReportExcel" />
                <asp:PostBackTrigger ControlID="btnReportRTF" />
                <asp:PostBackTrigger ControlID="btnUploadExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
