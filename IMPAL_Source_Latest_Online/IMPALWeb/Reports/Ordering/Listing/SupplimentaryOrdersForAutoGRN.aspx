<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplimentaryOrdersForAutoGRN.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.SupplimentaryOrdersForAutoGRN" MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(id) {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');

            if (document.getElementById("ctl00_CPHDetails_btnReportExcel").value != "Back") {
                var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
                var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
                return ValidateDateAutoGRN(txtFromDate, txtToDate, hidFromDate, hidToDate);
            }
            else {
                txtFromDate.value = "";
                txtToDate.value = "";
                document.getElementById("ctl00_CPHDetails_btnReportExcel").value = "Report";
                return false;
            }
        }

        function ValidateDateAutoGRN(txtFromDate, txtToDate, hidFromDate, hidToDate) {
            var oFromDateVal = txtFromDate.value.trim();
            var oToDateVal = txtToDate.value.trim();
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
            if (CheckDates(txtFromDate, txtToDate) == false)
                return false;
            else {
                var oFromDate = oFromDateVal.split("/");
                var oToDate = oToDateVal.split("/");
                var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
                var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
                if (hidFromDate != null)
                    hidFromDate.value = oFromDateFormatted;
                if (hidToDate != null)
                    hidToDate.value = oToDateFormatted;
                
                document.getElementById("ctl00_CPHDetails_btnReportExcel").value = "Back";
            }
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Supplimentary Orders For Auto GRN
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportExcel" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" Text="From Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                                <asp:HiddenField ID="hidFromDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="label">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                                <asp:HiddenField ID="hidToDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                                    Format="dd/MM/yyyy" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReportExcel" runat="server" Text="Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return Validate(id);" OnClick="btnReportExcel_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
