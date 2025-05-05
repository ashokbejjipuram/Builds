<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceReprintQRDuplicate.aspx.cs"
    Inherits="IMPALWeb.Reports.QR_Codes.SalesInvoiceReprintQRDuplicate" %>

<asp:Content ID="ContentSalesInvoiceReprint" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlPONumber = document.getElementById('<%=ddlInvoice.ClientID%>');
            var oPONumberVal = ddlPONumber.value.trim();
            if (oPONumberVal == null || oPONumberVal == "") {
                alert("Invoice Number should not be null");
                ddlPONumber.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Sales Invoice QR Code - Duplicate
    </div>
    <div class="reportFilters" runat="server" id="divSelection">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        Enabled="false" SkinID="DropDownListNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_IndexChanged" />
                </td>
                <td class="label">
                    <asp:Label ID="lblInvoiceType" Text="Invoice Type" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlInvoiceType" SkinID="DropDownListNormal" TabIndex="1" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceType_IndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblInvoice" SkinID="LabelNormal" Text="Invoice/STDN Number" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlInvoice" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons" runat="server" id="divButton" style="float: left">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
</asp:Content>
