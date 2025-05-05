<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Linewisetop20.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Comparison.Linewisetop20" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle ">
        Line Wise-Top 20
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                        runat="server"></asp:TextBox>
                    <a:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                    </a:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lbltodate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"></asp:TextBox>
                    <a:CalendarExtender ID="caltodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txttodate">
                    </a:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblselect" runat="server" Text="Quantity\Value" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSelect" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblsupplier" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList runat="server" ID="ddlSupplier" DataSourceID="ODsupplier" SkinID="DropDownListNormal"
                        TabIndex="4" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODsupplier" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                TabIndex="5" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crlinewisetop" OnUnload="crlinewisetop_Unload" />
    </div>
</asp:Content>
