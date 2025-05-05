<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ipl-depotbilling.aspx.cs"
    Inherits="IMPALWeb.Reports.SLB.ipl_depotbilling" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        IPL DEPOT BILLING
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lbldepot" Text="Depot" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddldepot" runat="server" TabIndex="1" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblcode" Text="Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlcode" runat="server" TabIndex="2" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="3" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="cripldepotbilling" runat="server" OnUnload="cripldepotbilling_Unload" />
    </div>
</asp:Content>
