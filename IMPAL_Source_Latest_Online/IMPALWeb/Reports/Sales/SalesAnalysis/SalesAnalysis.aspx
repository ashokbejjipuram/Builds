<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesAnalysis.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.SalesAnalysis" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');
            if (ddlMonthYear.value == "") {
                alert("Select Month-Year!");
                return false;
            }
        }
        function Reset() {
            var ddlTown = document.getElementById('<%=ddlTown.ClientID%>');
            ddlTown.options[0].selected = true;
            var ddlSupplier = document.getElementById('<%=ddlSupplier.ClientID%>');
            ddlSupplier.options[0].selected = true;
            var ddlCustomer = document.getElementById('<%=ddlCustomer.ClientID%>');
            ddlCustomer.options[0].selected = true;
            var ddlSalesMan = document.getElementById('<%=ddlSalesMan.ClientID%>');
            ddlSalesMan.options[0].selected = true;
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');
            ddlMonthYear.options[0].selected = true;
            var ddlReportType = document.getElementById('<%=ddlReportType.ClientID%>');
            ddlReportType.options[0].selected = true;
            return false;
        }
    </script>

    <div class="reportFormTitle">
        Sales Man Analysis</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblTown" SkinID="LabelNormal" runat="server" Text="Town"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlTown" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                        DataTextField="TownName" DataValueField="Towncode" />
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplier" SkinID="LabelNormal" Text="Supplier" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="2" SkinID="DropDownListNormal"
                        DataTextField="SupplierName" DataValueField="SupplierCode" />
                </td>
                <td class="label">
                    <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlCustomer" runat="server" TabIndex="3" SkinID="DropDownListNormal" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSalesMan" SkinID="LabelNormal" runat="server" Text="Sales Man"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSalesMan" runat="server" TabIndex="4" SkinID="DropDownListNormal"
                        DataTextField="Name" DataValueField="Code" />
                </td>
                <td class="label">
                    <asp:Label ID="lblMonthYear" SkinID="LabelNormal" runat="server" Text="Month-Year"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" TabIndex="5" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="6" DataTextField="DisplayText"
                        DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="7" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonViewReport" OnClientClick="javaScript:return Reset();"
                TabIndex="8" Style="display: none" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crSalesAnalysis" runat="server" OnUnload="crSalesAnalysis_Unload" />
    </div>
</asp:Content>
