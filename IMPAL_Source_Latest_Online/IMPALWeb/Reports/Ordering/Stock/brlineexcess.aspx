<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="brlineexcess.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.brlineexcess" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Branch\Line-Excess Inv
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblbranchcode" runat="server" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranchCode" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                        TabIndex="1" OnSelectedIndexChanged="brdd_indexchanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lbllinecode" runat="server" Text="Line Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlLineCode" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblmonthyear" runat="server" Text="Month Year"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" SkinID="ButtonViewReport"
                TabIndex="4" Text="Generate Report" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crbrlineexcess" runat="server" OnUnload="crbrlineexcess_Unload" ReportName="Impal_brlineexcess" />
    </div>
</asp:Content>
