<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="AnnualManager.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.StockComparison.AnnualManager" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validatenull() {
            var drp = document.getElementById('<%=ddlAccountingPeriod.ClientID%>');
            return validateacc(drp);
        }

    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Consolidated Branch/Area Managerwise
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccountingPeriod" runat="server" SkinID="LabelNormal" Text="Accounting period"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal"
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblAreaManager" runat="server" SkinID="LabelNormal" Text="Area Manager"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAreaManager" runat="server" SkinID="DropDownListNormal"
                        TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="4" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crAnnualManager" runat="server" OnUnload="crAnnualManager_Unload" />
    </div>
</asp:Content>
