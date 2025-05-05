<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SalesBreakup.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.SalesBreakup" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateMonthYear() {
            var drp = document.getElementById('<%=ddlMonthYear.ClientID%>');
            if (drp.value == null || drp.value == "" || drp.value == "0") {
                alert("Month Year should not be null");
                return false;
            }
        }

    </script>

    <div class="reportFormTitle">
        Sales Breakup
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblMonthYear" runat="server" SkinID="LabelNormal" Text="Month Year"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="2" OnClick="btnReport_Click" OnClientClick="javaScript:return validateMonthYear();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crSalesBreakup" OnUnload="crSalesBreakup_Unload" />
    </div>
</asp:Content>
