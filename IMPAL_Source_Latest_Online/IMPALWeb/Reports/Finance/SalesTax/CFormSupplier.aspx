<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CFormSupplier.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.SalesTax.CFormSupplier" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            var ddlFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var ddlToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var oFromDateVal = ddlFromDate.value.trim();
            var oToDateVal = ddlToDate.value.trim();
            if (validateacc(ddlAccPeriod) == false) {
                return false;
            }
            if (ddlFromDate.value.trim() == null || ddlFromDate.value.trim() == "")
             {
                 alert("FromDate Should not be null");
                 ddlFromDate.focus();
                 return false;
             }
             else if (ddlFromDate.value.trim() != null || ddlFromDate.value.trim() != "" || ddlToDate.value.trim()!="")
             var oSysDate = new Date();
             var oFromDate = oFromDateVal.split("/");
             var oToDate = oToDateVal.split("/");
             var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
             var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
             if (oSysDate < new Date(oFromDateFormatted)) {
                 alert("From Date should not be greater than System Date");
                 ddlFromDate.value = "";
                 ddlFromDate.focus();
                 return false;
             }
             else if (oSysDate < new Date(oToDateFormatted)) {
                 alert("To Date should not be greater than System Date");
                 ddlToDate.value = "";
                 ddlToDate.focus();
                 return false;
             }
             else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
                 alert("To Date should be greater than From Date");
                 ddlToDate.value = "";
                 ddlToDate.focus();
                 return false;
             }                        
        }
    </script>

    <div class="reportFormTitle">
        CForm - Supplier Wise</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccPeriod" TabIndex="1" runat="server" SkinID="DropDownListNormal"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAccPeriod_IndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplierName" Text="Supplier Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="4" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>                
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
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
                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidToDate" runat="server" />
                    <%--Stores value in MM/dd/yyyy format--%>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" TabIndex="6" SkinID="ButtonViewReport"></asp:Button>
            <%--<asp:Button ID="btnReset" runat="server" OnClientClick="javaScript:return Reset();"
                                    Text="Reset" TabIndex="7" SkinID="ButtonViewReport"></asp:Button>--%>
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="rptCrystal" runat="server" OnUnload="rptCrystal_Unload" ReportName="CForm-Supplier" />
    </div>
</asp:Content>
