<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnnexureE1.aspx.cs" Inherits="IMPALWeb.Reports.Finance.SalesTax.AnnexureE1"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

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
        Annexure to E1 Form</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplierName" Text="Supplier Name" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
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
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" TabIndex="4" SkinID="ButtonViewReport"></asp:Button>
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="rptCrystal" runat="server" ReportName="AnnexureE1" OnUnload="rptCrystal_Unload" />
    </div>
</asp:Content>
