<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Monthly.aspx.cs"
    Inherits="IMPALWeb.Reports.Gross_Profit.Monthly" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlMonYr = document.getElementById('<%=ddlMonYr.ClientID%>');
            if (ddlMonYr.value == null || ddlMonYr.value == "-1") {
                alert("Month Year should not be null");
                ddlMonYr.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender250">
        Gross Profit Statement - Monthly</div>
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
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="2" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="3" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crMonthly_BrGP" runat="server" OnUnload="crMonthly_BrGP_Unload" ReportName="Monthly_BrGP" />
    </div>
</asp:Content>
