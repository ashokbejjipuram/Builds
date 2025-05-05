<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Part_number_sales_details.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Comparison.Part_number_sales_details" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript" src="../../../Javascript/Validation.js"></script>

    <script type="text/javascript">
     function validatenull() {
         var drp = document.getElementById("<%=ddlMonthYear.ClientID %>");
         return validatemonthyear(drp);
      }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Part Number Sales Details with Previous years
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblmonthyear" runat="server" Text="Month Year" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblsupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddsupplier" runat="server" DataSourceID="ODsuppliers" TabIndex="1"
                        SkinID="DropDownListNormal" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="2" OnClientClick="javaScript:return validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crpartnumber" OnUnload="crpartnumber_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODsuppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
