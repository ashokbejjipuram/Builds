<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceReprintOld.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.Sales_Statement.SalesInvoiceReprintOld" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="ContentSalesInvoiceReprint" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtInvoiceNum = document.getElementById('<%=txtInvoiceNum.ClientID%>');
            var btnReportTxt = document.getElementById('<%=btnReport.ClientID%>');
            var oPONumberVal = txtInvoiceNum.value.trim();
            if (btnReportTxt.value.trim() != "Back") {
                if (oPONumberVal == null || oPONumberVal == "") {
                    alert("Invoice Number should not be null");
                    txtInvoiceNum.focus();
                    return false;
                }
            }

            window.document.forms[0].target = '_blank';
        }
    </script>
    <div class="reportFormTitle">
        Sales Invoice - Reprint Old
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters" runat="server" id="divSelection">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInvoiceNum" SkinID="LabelNormal" Text="Invoice Number" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceNum" SkinID="TextBoxNormal" TabIndex="2" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons" runat="server" id="divButton" style="float: left">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                    TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport ID="crySalesInvoiceReprint" OnUnload="crySalesInvoiceReprint_Unload" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
