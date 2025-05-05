<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockListBranches.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.Stock_List_Branches" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        All Branches Stock
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblinecode" Text="Line Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList runat="server" ID="ddlinecode" DataSourceID="ODSuppliers" DataTextField="SupplierName"
                        DataValueField="SupplierCode" AutoPostBack="True" SkinID="DropDownListNormal"
                        TabIndex="1" OnSelectedIndexChanged="ddlinecode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblbranchcode" SkinID="LabelNormal" Text="Branch Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddbranchcode" runat="server" SkinID="DropDownListNormal" TabIndex="2"
                        DataSourceID="ODBranch" DataTextField="BranchName" DataValueField="BranchCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblpart" Text="Part #" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                        ID="cbopartno" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblaging" Text="Aging" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAging" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                        TabIndex="4">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="5" />
        </div>
    </div>
    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="GetCorpBranch" TypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crbranches" runat="server" OnUnload="crbranches_Unload" />
    </div>
</asp:Content>
