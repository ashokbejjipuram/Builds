<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesManPerformance.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.SalesManPerformance" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);
        }
    </script>

    <div class="reportFormTitle">
        Sales Man Performance</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" Text="From Date"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" Text="To Date" runat="server"></asp:Label><span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidToDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" Text="Sales Man" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- Dropdown populated from Suppliers.cs--%>
                    <asp:DropDownList ID="ddlSalesMen" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataTextField="Name" DataValueField="Code" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="4" DataTextField="DisplayText"
                        DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="5" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crPerformance" runat="server" OnUnload="crPerformance_Unload" />
    </div>
</asp:Content>
