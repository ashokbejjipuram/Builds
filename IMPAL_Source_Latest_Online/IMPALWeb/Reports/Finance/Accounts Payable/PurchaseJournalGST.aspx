<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PurchaseJournalGST.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Accounts_Payable.PurchaseJournalGST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
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
        Purchase Journal - GST
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" ID="lblFromDate" Text="From Date" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" SkinID="LabelNormal" Text="To Date" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlReportType" runat="server" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                OnClick="btnReport_Click" TabIndex="4" SkinID="ButtonViewReport" />
        </div>
    </div>
</asp:Content>
