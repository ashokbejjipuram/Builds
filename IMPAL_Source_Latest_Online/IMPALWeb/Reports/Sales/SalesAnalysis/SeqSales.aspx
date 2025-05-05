<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeqSales.aspx.cs" Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.SeqSales"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Sequential Sales</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccPeriod" TabIndex="1" runat="server" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" Text="Town"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- Dropdown populated from Customer\Town.cs--%>
                    <asp:DropDownList ID="ddlTownCode" runat="server" TabIndex="3" SkinID="DropDownListNormal" AutoPostBack="True"
                        DataTextField="TownName" DataValueField="Towncode" OnSelectedIndexChanged="ddlTownCode_SelectedIndexChanged" />
                </td>
                <td class="label">
                    <asp:Label ID="lblCustomerName" SkinID="LabelNormal" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="cbCustomerName" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                        SkinID="DropDownListNormal" TabIndex="1" OnSelectedIndexChanged="cbCustomerName_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" SkinID="LabelNormal" Text="Line Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlLineCode" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="4" DataTextField="DisplayText"
                        DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div id="divCustomerInfo" style="display: none" runat="server">
            <div class="reportFormTitle">
                Customer Information</div>
            <table class="reportFiltersTable">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="6" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crSeqSales" runat="server" OnUnload="crSeqSales_Unload" />
    </div>
</asp:Content>
