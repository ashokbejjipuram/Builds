<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockListGST.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.StockListGST" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Stock List - GST
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblList" SkinID="LabelNormal" runat="server" Text="List"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlList_OnSelectIndexChanged"
                        SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblbranchcode" SkinID="LabelNormal" runat="server" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranchCode" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                        TabIndex="2" OnSelectedIndexChanged="ddlBranchCode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblsuppliercode" runat="server" SkinID="LabelNormal" Text="Supplier Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplierCode" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblreporttype" runat="server" Text="ReportType" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="7">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" SkinID="ButtonViewReport" Text="Generate Report" runat="server"
                TabIndex="5" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crstocklistGST" EnableViewState="true" OnUnload="crstocklistGST_Unload" runat="server" />
    </div>
    <asp:ObjectDataSource ID="ODBranchCode" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSupplierCode" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
