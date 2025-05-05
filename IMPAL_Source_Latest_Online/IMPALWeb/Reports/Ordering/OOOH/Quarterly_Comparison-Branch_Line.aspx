<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Quarterly_Comparison-Branch_Line.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.OOOH.Quarterly_Comparison_Branch_Line" %>

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
        Quarterly Comparison-Branch/Line
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblfrommonth" runat="server" SkinID="LabelNormal" Text="From Month"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" TabIndex="1" ID="ddfrommonth" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblyear" SkinID="LabelNormal" runat="server" Text="Year"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtyear" TabIndex="2" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                </td>
                <td class="label">
                    <asp:Label ID="lbltomonth" runat="server" Text="To Month" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddtomonth" SkinID="DropDownListNormal" TabIndex="3" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                TabIndex="4" SkinID="ButtonViewReport" OnClientClick="javaScript:return validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crbranchline" OnUnload="crbranchline_Unload" />
    </div>
</asp:Content>
