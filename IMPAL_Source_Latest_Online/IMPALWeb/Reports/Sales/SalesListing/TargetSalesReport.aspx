<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="TargetSalesReport.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.TargetSalesReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');
            return validatemonthyear(ddlMonthYear);
            }
    </script>

    <div class="reportFormTitle">
        Target Sales Report</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblMonthYear" runat="server" Text="Month Year"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlMonthYear" runat="server" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" OnClick="btnReport_Click"
                TabIndex="2" OnClientClick="javaScript:return fnValidate();" Text="Generate Report" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport runat="server" ID="crTargetSales" OnUnload="crTargetSales_Unload" ReportName="TargetSales" />
    </div>
</asp:Content>
