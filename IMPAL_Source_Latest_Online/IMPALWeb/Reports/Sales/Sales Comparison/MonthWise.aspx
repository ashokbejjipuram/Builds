<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="MonthWise.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Comparison.MonthWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validatenull() {
            var drp = document.getElementById('<%=ddaccountingperiod.ClientID%>');
            return validateacc(drp);
        }

    </script>

    <div class="reportFormTitle">
        Month Wise
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblcustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="cbocustomer" runat="server" DropDownStyle="DropDownList" TabIndex="1" SkinID="DropDownListNormal" MaxLength="0"
                        Style="display: inline;">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblsupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddsupplier" runat="server" TabIndex="2" SkinID="DropDownListNormal"
                        DataSourceID="ODsupplier" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblaccountingperiod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddaccountingperiod" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="4" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="5" OnClientClick="javaScript:return validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crmonthwise" OnUnload="crmonthwise_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODsupplier" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
