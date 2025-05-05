<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkUploadOrdersPlaced.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.BulkUploadOrdersPlaced" MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(id) {
            var txtDate = document.getElementById('<%=txtDate.ClientID%>');

            if (document.getElementById("ctl00_CPHDetails_btnReportExcel").value != "Back") {
                var hidDate = document.getElementById('<%=hidDate.ClientID%>');
                return ValidateDateAutoGRN(txtDate, hidDate);
            }
            else {
                txtDate.value = "";
                document.getElementById("ctl00_CPHDetails_btnReportExcel").value = "Report";
                return false;
            }
        }

        function ValidateDateAutoGRN(txtDate, hidDate) {
            var oDateVal = txtDate.value.trim();
            if (oDateVal == null || oDateVal == "") {
                alert("Date should not be null!");
                txtDate.focus();
                return false;
            }

            var oDate = oDateVal.split("/");
            var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];
            if (hidDate != null)
                hidDate.value = oDateFormatted;

            document.getElementById("ctl00_CPHDetails_btnReportExcel").value = "Back";
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Bulk Upload Orders Placed
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
                                <asp:Label ID="lblDate" Text="Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                                <asp:HiddenField ID="hidDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtDate"
                                    Format="dd/MM/yyyy" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReportExcel" runat="server" Text="Supplier Excel" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return Validate(id);" OnClick="btnReportExcel_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
