<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockValueNew.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.StockValueNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />

    <div class="reportFormTitle">
        Stock Value
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" TabIndex="1" ID="ddlbranchcode" runat="server"
                        DataSourceID="ODBranch" DataTextField="BranchName" DataValueField="BranchCode">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button TabIndex="2" SkinID="ButtonViewReport" ID="btnReport" runat="server"
                Text="Generate Report" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
       <%-- <uc1:CrystalReport runat="server" ID="crystockvalue" />--%>
        <table id="Table1" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" SkinID="LabelNormal" runat="server" Text="Rows per page"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlpagelimit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpagelimit_SelectedIndexChanged" SkinID="DropDownListNormal">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="70" Selected="True" Value="70"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="Label3" SkinID="LabelNormal" runat="server" Text="Total No of Records"></asp:Label>
            
                </td>   
                <td class="inputcontrols">
                    <asp:Label ID="lblTotalRecords" SkinID="LabelNormal" runat="server" Text="0" Font-Bold="true" ></asp:Label>
                </td> 
                <td>
                    <asp:Button ID="btnExport" SkinID="ButtonExportReport" Text="Export Report" runat="server"
                    OnClick="btnExport_Click" CssClass="buttonViewReport reportViewerHolder" />
                </td>
            </tr>
        </table> 
        <asp:GridView ID="grdStockValue" runat="server" Width="100%" AutoGenerateColumns="true" OnRowDataBound="grdStockValue_RowDataBound" 
            AllowPaging="true" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdStockValue_PageIndexChanging"
            PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No Records found." >
            <HeaderStyle BackColor="#003399" ForeColor="White"></HeaderStyle>
            <AlternatingRowStyle BackColor="#FFFFCC" />
            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" Position="Top" PageButtonCount="5" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="GetAllBranch" TypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource> 
</asp:Content>
