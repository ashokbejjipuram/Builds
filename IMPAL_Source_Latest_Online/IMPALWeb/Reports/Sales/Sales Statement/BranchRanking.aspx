<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchRanking.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesStatement.BranchRanking" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlMonthYear = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            if (ddlMonthYear.value == "-1") {
                alert("Select Accounting Period");
                ddlMonthYear.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Branch Ranking
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccPeriod" runat="server" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                OnClientClick="javaScript:return Validate();" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc:CrystalReport ID="crSalesBranchRanking" runat="server" ReportName="BranchRanking" OnUnload="crSalesBranchRanking_Unload" />
    </div>
</asp:Content>
