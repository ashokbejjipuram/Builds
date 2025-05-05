<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="cost_slb_gpworking.aspx.cs"
    Inherits="IMPALWeb.Reports.SLB.cost_slb_gpworking" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        COST/SLB/GP Working
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" Text="Branch Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- <asp:DropDownList ID="ddlbranchcode" runat="server" TabIndex="1" SkinID="DropDownListNormal" />--%>
                    <asp:DropDownList ID="ddlbranchcode" runat="server" AutoPostBack="true" DataSourceID="ODbranchcode"
                        DataTextField="BranchName" DataValueField="BranchCode" SkinID="DropDownListNormal"
                        OnSelectedIndexChanged="ddlbranchcode_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODbranchcode" runat="server" SelectMethod="SLBbranchcode"
                        TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lbllinecode" Text="Line Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- <asp:DropDownList ID="ddllinecode" runat="server" TabIndex="1" SkinID="DropDownListNormal" />--%>
                    <asp:DropDownList ID="ddllinecode" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="3" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crslbgpworking" runat="server" OnUnload="crslbgpworking_Unload" />
    </div>
</asp:Content>
