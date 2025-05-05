<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProformaInvoiceReprint.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.Sales_Statement.ProformaInvoiceReprint" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="ContentProformaInvoiceReprint" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlPONumber = document.getElementById('<%=ddlInvoiceProforma.ClientID%>');
            var oPONumberVal = ddlPONumber.value.trim();
            if (oPONumberVal == null || oPONumberVal == "") {
                alert("Invoice Number should not be null");
                ddlPONumber.focus();
                return false;
            }

            window.document.forms[0].target = '_blank';
        }        
    </script>
    <div class="reportButtons" runat="server" id="DivBackbtn" style="display:none">
        <asp:Button ID="btnBack" runat="server" SkinID="ButtonViewReport" Text="Back" OnClick="btnBack_Click" />
    </div>
    <div class="reportFormTitle">
        Proforma Invoice - Reprint</div>
    <div class="reportFilters" runat="server" id="divSelection">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblInvoiceType" Text="Invoice Type" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlInvoiceType" SkinID="DropDownListNormal" TabIndex="1" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblInvoiceProforma" SkinID="LabelNormal" Text="Invoice Number" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlInvoiceProforma" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons" runat="server" id="divButton" style="float: left">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="cryProformaInvoiceReprint" runat="server" OnUnload="cryProformaInvoiceReprint_Unload" />
    </div>
</asp:Content>
