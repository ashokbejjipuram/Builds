<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpeningStock.aspx.cs" Inherits="IMPALWeb.Reports.Inventory.OpeningStock"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlStation = document.getElementById('<%=ddlStation.ClientID%>');
            ValidateIndicator(ddlStation);
            
        }
    </script>

    <div class="reportFormTitle">
        Opening Stock</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblStation" Text="Station" SkinID="LabelNormal" runat="server">OS/LS</asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlStation" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="2" SkinID="ButtonViewReport"></asp:Button>
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crOpeningStock" runat="server" OnUnload="crOpeningStock_Unload" ReportName="OpeningStock" />
    </div>
</asp:Content>
