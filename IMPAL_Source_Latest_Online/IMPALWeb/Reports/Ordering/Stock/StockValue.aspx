<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockValue.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.StockValue" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Stock Value
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" TabIndex="1" ID="ddlbranchcode" runat="server"
                        DataSourceID="ODBranch" DataTextField="BranchName" DataValueField="BranchCode">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button TabIndex="2" SkinID="ButtonViewReport" ID="btnReport" runat="server"
                Text="Generate Report" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crystockvalue" OnUnload="crystockvalue_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="GetAllBranch" TypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
</asp:Content>
