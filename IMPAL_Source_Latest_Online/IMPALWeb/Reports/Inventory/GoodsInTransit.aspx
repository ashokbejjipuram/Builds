<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="GoodsInTransit.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.SupplierDespatchDetails" MasterPageFile="~/Main.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Goods In Transit Details</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSuppLine" Text="Supplier Line" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSuppLine" runat="server" TabIndex="1" DataSourceID="ODLine"
                        DataTextField="SupplierName" DataValueField="SupplierCode" SkinID="DropDownListNormal" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetLineBasedSupplier"
                        TypeName="IMPALLibrary.Suppliers" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                TabIndex="3" OnClientClick="javaScript:return Validate()" SkinID="ButtonViewReport" />
        </div>
    </div>
</asp:Content>
