<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReprint.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.PurchaseOrderReprint" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnShowHideBtns() {
            var ddlPONumber = document.getElementById('<%=ddlPONumber.ClientID%>');

            if (ddlPONumber.value == null || ddlPONumber.value == "") {
                alert("PO Number should not be null");
                ddlPONumber.focus();
                return false;
            }

            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
            document.getElementById('<%=PanelHeaderDtls.ClientID%>').disabled = true;

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        Purchase Order-Reprint
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crPurchaseOrderReprint" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters" runat="server" id="divSelection">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="ddlReportType" AutoPostBack="true"
                                    runat="server" TabIndex="1" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblPOType" runat="server" Text="PO Type"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="ddlPOType" AutoPostBack="true"
                                    runat="server" TabIndex="2" OnSelectedIndexChanged="ddlPOType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblPONumber" runat="server" Text="PO Number"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="ddlPONumber" runat="server" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport runat="server" ID="crPurchaseOrderReprint" OnUnload="crPurchaseOrderReprint_Unload" ReportName="Purchase_Direct" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
