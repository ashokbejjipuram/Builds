<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Sales-LineWiseSales.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.Sales_LineWiseSales" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
       }
    </script>

    <div class="reportFormTitle">
        Linewise Sales</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblFrmline" runat="server" Text="From Line"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlfrmline" runat="server" TabIndex="1"
                        DataSourceID="ODSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblToline" runat="server" Text="To Line"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlToline" runat="server" TabIndex="2"
                        DataSourceID="ODToSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODToSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblfromdate" Text="From Date" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtFromDate" TabIndex="3"
                        runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblToDate" Text="To Date" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtToDate" TabIndex="4" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlReportType" TabIndex="5" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                TabIndex="6" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crSalesLineWiseSales" runat="server" OnUnload="crSalesLineWiseSales_Unload" />
    </div>
</asp:Content>
