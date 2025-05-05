<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockValueAging.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.Stock_Value_Aging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function Disablebtns() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
        }
    </script>
    <div class="reportFormTitle">
        Stock Value - Aging
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblsuppliercode" runat="server" SkinID="LabelNormal" Text="Supplier Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList runat="server" ID="ddlSupplierCode" DataSourceID="ODSuppliercode"
                        DataTextField="SupplierName" SkinID="DropDownListNormal" TabIndex="1" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblrpttype" Text="Report Type" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReporttype" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Excel Report"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button SkinID="ButtonViewReport" ID="btnReportPDF" runat="server" Text="PDF Report"
                TabIndex="3" OnClick="btnReportPDF_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <asp:ObjectDataSource ID="ODSuppliercode" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
