<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ShortageDebitNote.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.ShortageDebitNote" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtDocumentNo = document.getElementById('<%=txtDocumentNo.ClientID%>');
            var btnReport = document.getElementById('<%=btnReport.ClientID%>');
            var docNumber = txtDocumentNo.value.trim();

            if (docNumber == null || docNumber == "") {
                alert("Document Number should not be null");
                txtDocumentNo.focus();
                return false;
            }
            
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        Supplier Shortage Debit Note
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblInvoiceNum" SkinID="LabelNormal" Text="Shortage Debit Note Number" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtDocumentNo" SkinID="TextBoxNormal" TabIndex="2" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="reportButtons">
        <asp:Button ID="btnReport" Text="Generate Report" runat="server" TabIndex="4" SkinID="ButtonViewReport"
            OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();" />
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crShortageDebitNote" OnUnload="crShortageDebitNote_Unload" />
    </div>
</asp:Content>
