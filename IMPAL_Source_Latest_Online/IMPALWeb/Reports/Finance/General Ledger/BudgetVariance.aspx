<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BudgetVariance.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.BudgetVariance" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {

            var FromGL = document.getElementById('<%=ddlFromGL.ClientID%>');
            var ToGL = document.getElementById('<%=ddlToGL.ClientID%>');
            if (ToGL.value.trim() < FromGL.value.trim()) {
                alert("To Description Should be Greater");
                return false;}
        }
    </script>

    <div class="reportFormTitle">
        Budget Variance
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromGL" runat="server" Text="From GL Main" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlFromGL" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblToGL" runat="server" Text="To GL Main" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToGL" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" TabIndex="3" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crBudgetVariance" OnUnload="crBudgetVariance_Unload" />
    </div>
</asp:Content>
