<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="slb_structure_detail.aspx.cs"
    Inherits="IMPALWeb.Reports.SLB.slb_structure_detail" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        SLB-Structure-Detail
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" Text="Branch Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%--<asp:DropDownList ID="ddlbranchcode" runat="server" TabIndex="1" SkinID="DropDownListNormal" />--%>
                    <asp:DropDownList ID="ddlbranchcode" runat="server" AutoPostBack="True" DataSourceID="ODbranchcode"
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
                    <asp:DropDownList ID="ddllinecode" runat="server" TabIndex="2" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="3" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crslbdetails" runat="server" OnUnload="crslbdetails_Unload" />
    </div>
</asp:Content>
