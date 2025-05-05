<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Quarterly_Comparison-Line_Branch.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.OOOH.Quarterly_Comparison_Line_Branch" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validatenull() {
            var frommonth = document.getElementById('<%=ddfrommonth.ClientID%>');
            var tomonth = document.getElementById('<%=ddtomonth.ClientID%>');
            var year = document.getElementById('<%=txtyear.ClientID%>');
            return validatemonthyear1(frommonth, tomonth, year);
        } 
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Quarterly Comparison-Line/Branch
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblfrommonth" runat="server" Text="From Month" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddfrommonth" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblyear" runat="server" Text="Year" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtyear" TabIndex="2" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="lbltomonth" runat="server" Text="To Month" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddtomonth" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="4" OnClientClick="javaScript:return validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crlinebranch" OnUnload="crlinebranch_Unload" />
    </div>
</asp:Content>
