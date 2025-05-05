<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SalesJournalGST.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.SalesJournalGST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(e) {

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDatesGST(txtFromDate, txtToDate);
        }

        function ValidateDatesGST(txtFromDate, txtToDate) {
            var oFromDateVal = txtFromDate.value.trim();
            var oToDateVal = txtToDate.value.trim();
            var oSysDate = new Date();
            var oFromDate = oFromDateVal.split("/");
            var oToDate = oToDateVal.split("/");
            var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
            var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];

            if (oFromDateVal == null || oFromDateVal == "") {
                alert("From Date should not be null!");
                txtFromDate.focus();
                return false;
            }
            else if (oToDateVal == null || oToDateVal == "") {
                alert("To Date should not be null!");
                txtToDate.focus();
                return false;
            }
            else if (oFromDateVal != null && fnIsDate(txtFromDate.value) == false) {
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;

            }
            else if (oToDateVal != null && fnIsDate(txtToDate.value) == false) {
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oFromDateFormatted)) {
                alert("From Date should not be greater than System Date");
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oToDateFormatted)) {
                alert("To Date should not be greater than System Date");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
                alert("To Date should be greater than or equal to From Date");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (new Date(oFromDateFormatted) <= new Date('6/30/2017')) {
                alert("From Date should be greater than or equal to 1st July 2017");
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;
            }
            else if (new Date(oToDateFormatted) <= new Date('6/30/2017')) {
                alert("To Date should be greater than or equal to 1st July 2017");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Sales Journal - GST
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" SkinID="LabelNormal" Text="From date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblTodate" runat="server" SkinID="LabelNormal" Text="To date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" SkinID="LabelNormal" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormalBig" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="4" OnClientClick="javaScript:return Validate(this);" OnClick="btnReport_Click" />
        </div>
    </div>
</asp:Content>
