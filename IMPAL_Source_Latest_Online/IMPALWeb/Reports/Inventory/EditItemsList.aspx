<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditItemsList.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.EditItemsList" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlStation = document.getElementById('<%=ddlStation.ClientID%>');
            if (ddlStation.value == "") {
                alert("Indicator should not be null");
                ddlStation.focus();
                return false;
            }
            else {
                var ddlFromLine = document.getElementById('<%=ddlFromLine.ClientID%>');
                var ddlToLine = document.getElementById('<%=ddlToLine.ClientID%>');
                if (ddlFromLine.value != " " && ddlToLine.value < ddlFromLine.value) {
                    alert("To Line should be greater");
                    ddlToLine.focus();
                    return false;
                }
                var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
                var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
                var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
                var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
                return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);
               
            }
        }
    </script>

    <div class="reportFormTitle">
        Edit List for the Items Entered</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblStation" Text="OS/LS" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlStation" runat="server" TabIndex="1" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="3" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidToDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromLine" Text="From Line" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <%-- Dropdown populated from Suppliers.cs--%>
                    <asp:DropDownList ID="ddlFromLine" runat="server" TabIndex="4" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToLine" Text="To Line" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToLine" runat="server" TabIndex="5" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                OnClientClick="javaScript:return Validate();" TabIndex="6" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crStockAdjustment" runat="server" OnUnload="crStockAdjustment_Unload" ReportName="StockAdjustment" />
    </div>
</asp:Content>
