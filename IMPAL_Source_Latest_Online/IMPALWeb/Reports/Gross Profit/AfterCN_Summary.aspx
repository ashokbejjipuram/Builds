<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AfterCN(Summary).aspx.cs"
    Inherits="IMPALWeb.Reports.Gross_Profit.AfterCN_Summary_" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlMonYr = document.getElementById('<%=ddlMonYr.ClientID%>');
            if (ddlMonYr.value == null || ddlMonYr.value == "") {
                alert("Month Year should not be null");
                ddlMonYr.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender250">
        Gross Profit Summary Report</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblMonYr" runat="server" Text="Month Year" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonYr" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="2" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crAfterCnSummary" runat="server" OnUnload="crAfterCnSummary_Unload" ReportName="AfterCnSummary" />
    </div>
</asp:Content>
