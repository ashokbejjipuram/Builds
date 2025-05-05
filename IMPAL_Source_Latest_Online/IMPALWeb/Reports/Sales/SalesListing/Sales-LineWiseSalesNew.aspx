<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Sales_LineWiseSalesNew.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.Sales_LineWiseSalesNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle">
        Linewise Sales
    </div>
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
                    <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlReportType" TabIndex="5" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
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
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblToDate" Text="To Date" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtToDate" TabIndex="4" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                TabIndex="6" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <table id="Table1" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label" style="display: none">
                    <asp:Label ID="Label2" SkinID="LabelNormal" runat="server" Text="Rows per page"></asp:Label>
                </td>
                <td class="inputcontrols" style="display: none">
                    <asp:DropDownList ID="ddlpagelimit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpagelimit_SelectedIndexChanged" SkinID="DropDownListNormal">
                        <asp:ListItem Text="100" Value="100" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label" style="display: none">
                    <asp:Label ID="Label3" SkinID="LabelNormal" runat="server" Text="Total No of Records"></asp:Label>

                </td>
                <td class="inputcontrols" style="display: none">
                    <asp:Label ID="lblTotalRecords" SkinID="LabelNormal" runat="server" Text="0" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnExport" SkinID="ButtonExportReport" Text="Export Report" runat="server"
                        OnClick="btnExport_Click" CssClass="buttonViewReport reportViewerHolder" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdLineWiseSales" runat="server" Width="100%" AutoGenerateColumns="true" OnRowDataBound="grdLineWiseSales_RowDataBound"
            AllowPaging="true" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdLineWiseSales_PageIndexChanging"
            PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No Records found.">
            <HeaderStyle BackColor="#003399" ForeColor="White"></HeaderStyle>
            <AlternatingRowStyle BackColor="#FFFFCC" />
            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" Position="Top" PageButtonCount="5" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
</asp:Content>
