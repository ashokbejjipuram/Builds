<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CashBook.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.CashBook" Async="true" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
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
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        Cash Book
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReportPDF" />
                <asp:PostBackTrigger ControlID="btnReportExcel" />
                <asp:PostBackTrigger ControlID="btnReportRTF" />
                <asp:PostBackTrigger ControlID="crCashBook_Report" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="PanelHeaderDtls" runat="server">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="1"></asp:TextBox>
                                <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="2"></asp:TextBox>
                                <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label" style="display:none">
                                <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols" style="display:none">
                                <asp:DropDownList ID="ddlTransactionType" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div class="reportButtons">
                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();"
                        Text="Generate Report" SkinID="ButtonViewReport" TabIndex="11" />
                    <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                    <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                    <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                    <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
                </div>
                <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                    <UC:CrystalReport ID="crCashBook_Report" runat="server" OnUnload="crCashBook_Report_Unload" ReportName="CashBook_Report" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
