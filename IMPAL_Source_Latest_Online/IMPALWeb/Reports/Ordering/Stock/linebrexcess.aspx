<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="linebrexcess.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.linebrexcess" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Line/Branch Excess-inv
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lbllinecode" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlinecode" runat="server" AutoPostBack="True" DataSourceID="ODLinecode"
                        DataTextField="SupplierName" DataValueField="SupplierCode" OnSelectedIndexChanged="drpdown_selidxchanged"
                        SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblbranchcode" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddbranchcode" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblmonthyear" runat="server" SkinID="LabelNormal" Text="Month Year"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddmonthyear" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" OnClick="btnReport_Click"
                TabIndex="4" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crlinebrexcess" ReportName="Impal_linebrexcess" OnUnload="Impal_linebrexcess_Unload" runat="server" />
    </div>
    <asp:ObjectDataSource ID="ODLinecode" runat="server" SelectMethod="GetSupplierline"
        TypeName="IMPALLibrary.Suppliers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchcode" ControlID="ddbranchcode" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
