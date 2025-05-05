<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="StockLedger.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesStatement.StockLedger" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="UC" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');
            if (ddlMonthYear.value == "") {
                alert("Select Month-Year!");
                ddlMonthYear.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Stock Ledger</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplierCode" runat="server" Text="Supplier Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplierCode" runat="server" DataSourceID="ODSuppliers"
                        AutoPostBack="true" DataTextField="SupplierName" DataValueField="SupplierCode"
                        SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlSupplierCode_IndexChanged">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplierPartNo" runat="server" Text="Supp.Part#" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtSupplierPartNo" runat="server" Enabled="false" SkinID="TextBoxNormal" />
                    <UC:ItemCodePartNumber runat="server" ID="ItemPopUp" Mode="2" Disable="false" OnSearchImageClicked="ItemPopUp_ImageClicked">
                    </UC:ItemCodePartNumber>
                </td>
                <td class="label">
                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code" SkinID="LabelNormal" />
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtItemCode" runat="server" SkinID="TextBoxDisabled" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblMonthYear" runat="server" Text="Month Year" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" DataTextField="Month_Year" SkinID="DropDownListNormal"
                        DataValueField="Month_Year">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crStockLedger" runat="server" OnUnload="crStockLedger_Unload" ReportName="StockLedger" />
    </div>
</asp:Content>
