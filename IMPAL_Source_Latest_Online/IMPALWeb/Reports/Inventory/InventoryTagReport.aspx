<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="InventoryTagReport.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.InventoryTagReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function Disablebtns() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
        }
    </script>
    <div class="reportFormTitle">
        Inventory Tag Report
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblsppcode" runat="server" Text="Supplier Code" SkinID="LabelNormal" />
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlsuppcode" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource1"
                        DataTextField="SupplierName" DataValueField="SupplierCode" SkinID="DropDownListNormal"
                        OnSelectedIndexChanged="ddlsuppcode_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lblSuppPartNo" runat="server" Text="Supplier Part Number" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtSuppPartNo" runat="server" Height="16px" Enabled="false" SkinID="TextBoxNormal"></asp:TextBox>
                    <User:ItemCodePartNumber ID="user" runat="server" Mode="2" OnSearchImageClicked="user_SearchImageClicked"
                        Disable="false" />
                </td>
                <td class="label">
                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtItemCode" runat="server" SkinID="TextBoxNormal" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblTagType" runat="server" Text="Tag Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlTagType" runat="server" Width="160px" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Excel Report" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button SkinID="ButtonViewReport" ID="btnReportPDF" runat="server" Text="PDF Report"
                TabIndex="3" OnClick="btnReportPDF_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
