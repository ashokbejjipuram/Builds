<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="StockSheetList.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.StockSheetList" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlFromLine = document.getElementById('<%=ddlFromLine.ClientID%>');
            var ddlToLine = document.getElementById('<%=ddlToLine.ClientID%>');
            if (ddlFromLine.value != "" && ddlToLine.value < ddlFromLine.value) {
                alert("To Description should be greater");
                ddlToLine.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Stock Sheet List</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromLine" Text="From Line" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- Dropdown populated from Suppliers.cs--%>
                    <asp:DropDownList ID="ddlFromLine" runat="server" TabIndex="1" DataSourceID="ODLine"
                        DataTextField="SupplierName" DataValueField="SupplierCode" SkinID="DropDownListNormal" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetLineBasedSupplier"
                        TypeName="IMPALLibrary.Suppliers" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToLine" Text="To Line" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToLine" runat="server" TabIndex="2" DataSourceID="ODLine"
                        DataTextField="SupplierName" DataValueField="SupplierCode" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="3" OnClientClick="javaScript:return Validate()" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crStockSheetList" runat="server" OnUnload="crStockSheetList_Unload" EnableViewState="true" ReportName="StockSheetList" />
    </div>
</asp:Content>
