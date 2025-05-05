<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LinewisePurchaseOrder.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.LinewisePurchaseOrder" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            if (txtFromDate.value == "" && txtToDate.value == "") {
                return true;
            }
            else {
                return ValidateOrderDate(txtFromDate, txtToDate);
            }

       }
    </script>

    <div class="reportFormTitle">
        Line Wise Purchase Order</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblBranchCode" runat="server" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlBranchCode" runat="server" TabIndex="1"
                        DataSourceID="ODBranch" DataTextField="BranchName" DataValueField="BranchCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="Orderbranchcodes"
                        TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblLineCode" runat="server" Text="Line Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlLineCode" runat="server" TabIndex="2"
                        DataSourceID="ODSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetLineCodes"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtFromDate" runat="server"
                        TabIndex="3"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtToDate" runat="server"
                        TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" TabIndex="5"
                Text="Generate Report" OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport runat="server" ID="crLinewisePurchaseOrder" OnUnload="crLinewisePurchaseOrder_Unload" ReportName="LinewisePurchaseOrder" />
    </div>
</asp:Content>
