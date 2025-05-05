<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineWiseNew.aspx.cs" Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.LineWiseNew"
    MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);
        }
    </script>

    <div class="reportFormTitle">
        Line wise with Previous Year
    </div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" Text="From Date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" Text="To Date" runat="server"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidToDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" Text="Line Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- Dropdown populated from Suppliers.cs--%>
                    <asp:DropDownList ID="ddlLineCode" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" Text="Town Code"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlTownCode" runat="server" TabIndex="4" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTownCode_IndexChanged" DataTextField="TownName" DataValueField="Towncode"
                        SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                </td>
                <td class="inputcontrols">
                    <%--<div id="divOther">--%>
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="5" DataTextField="DisplayText"
                        DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                    <%--</div>--%>
                    <%-- <div id="divTown" style="display: none" runat="server">
                                            <asp:DropDownList ID="ddlTownWise" runat="server" TabIndex="5" SkinID="DropDownListNormal">
                                                <asp:ListItem Text="Town Wise" Value="TownWise" />
                                            </asp:DropDownList>
                                        </div>--%>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="6" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <table id="Table1" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" SkinID="LabelNormal" runat="server" Text="Rows per page"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlpagelimit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpagelimit_SelectedIndexChanged" SkinID="DropDownListNormal">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Selected="True" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="Label3" SkinID="LabelNormal" runat="server" Text="Total No of Records"></asp:Label>

                </td>
                <td class="inputcontrols">
                    <asp:Label ID="lblTotalRecords" SkinID="LabelNormal" runat="server" Text="0" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnExport" SkinID="ButtonExportReport" Text="Export Report" runat="server"
                        OnClick="btnExport_Click" CssClass="buttonViewReport reportViewerHolder" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdLineWise" runat="server" Width="100%" AutoGenerateColumns="true" OnRowDataBound="grdLineWise_RowDataBound" 
            AllowPaging="true" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdLineWise_PageIndexChanging"
            PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No Records found.">
            <HeaderStyle BackColor="#003399" ForeColor="White"></HeaderStyle>
            <AlternatingRowStyle BackColor="#FFFFCC" />
            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" Position="Top" PageButtonCount="5" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
</asp:Content>
