<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ZoneLineComparison.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.StockComparison.ZoneLineComparison" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="a" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validatenull() {
        
            var frommonth = document.getElementById('<%=ddfrommonth.ClientID%>');
            var tomonth = document.getElementById('<%=ddtomonth.ClientID%>');
            var year = document.getElementById('<%=txtyear.ClientID%>');
            return validatemonthyear1(frommonth,tomonth,year);
        } 
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Zone/Line Stock Comparison
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
        <uc1:CrystalReport runat="server" ID="crzoneline" OnUnload="crzoneline_Unload" />
    </div>
</asp:Content>
