<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Stock Value - Aging.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.Stock_Value___Aging" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Stock Value - Aging
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblsuppliercode" runat="server" SkinID="LabelNormal" Text="Supplier Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList runat="server" ID="ddlSupplierCode" DataSourceID="ODSuppliercode"
                        DataTextField="SupplierName" SkinID="DropDownListNormal" TabIndex="1" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblrpttype" Text="Report Type" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReporttype" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="craging" />
    </div>
    <asp:ObjectDataSource ID="ODSuppliercode" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
