<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DebitCreditNoteNew.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.DebitCreditNoteNew" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtDocumentNo = document.getElementById('<%=txtDocumentNo.ClientID%>');
            var btnReportTxt = document.getElementById('<%=btnReport.ClientID%>');
            var oPONumberVal = txtDocumentNo.value.trim();
            if (btnReportTxt.value.trim() != "Back") {
                if (oPONumberVal == null || oPONumberVal == "") {
                    alert("Document Number should not be null");
                    txtDocumentNo.focus();
                    return false;
                }
            }

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="subFormTitle subFormTitleExtender300">
        Debit Credit Note Report - With QR
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblInvoiceNum" SkinID="LabelNormal" Text="Dr/Cr Note Number" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtDocumentNo" SkinID="TextBoxNormal" TabIndex="2" runat="server">
                    </asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal">
                        <asp:ListItem Text="New" Value="0"></asp:ListItem>
                        <asp:ListItem Text="General" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Doc-Wise" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Cust-Wise" Value="3"></asp:ListItem>                        
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" TabIndex="4" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crDebitCreditNew" OnUnload="crDebitCreditNew_Unload" />
    </div>
</asp:Content>
