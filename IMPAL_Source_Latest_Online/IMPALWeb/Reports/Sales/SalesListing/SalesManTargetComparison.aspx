<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="SalesManTargetComparison.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.SalesManTargetComparison" %>

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

    <div class="subFormTitle subFormTitleExtender350">
        Sales Man Target Sales Comparison</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblfromdate" Text="From Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                        runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblTransType" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlTransType" SkinID="DropDownListNormal" runat="server" TabIndex="3"
                        DataSourceID="ODTransactionType" DataTextField="TransactionTypeDescription" DataValueField="TransactionTypeCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODTransactionType" runat="server" SelectMethod="GetAllTransactionItems"
                        TypeName="IMPALLibrary.Transaction"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lblSuplinecode" runat="server" SkinID="LabelNormal" Text="Supplier Line Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSuplinecode" SkinID="DropDownListNormal" runat="server"
                        TabIndex="4" DataSourceID="ODToSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODToSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" SkinID="ButtonViewReport" runat="server" TabIndex="5"
                Text="Generate Report" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crSalesManTargetComparison" runat="server" OnUnload="crSalesManTargetComparison_Unload" />
    </div>
</asp:Content>
