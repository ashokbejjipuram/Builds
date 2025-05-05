<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchReconcilationHO.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.General_Ledger.BranchReconcilationHO" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function FnValidate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');

            if (ddlBranch.value == "" || ddlBranch.value == "0") {
                alert("Please Select Branch!");
                ddlBranch.focus();
                return false;
            }

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');

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
        }

        function CheckDates(txtFromDate, txtToDate) {
            var oFromDateVal = txtFromDate.value.trim();
            var oToDateVal = txtToDate.value.trim();
            if (fnIsDate(oFromDateVal) == false) {
                txtFromDate.focus();
                return false;
            }
            else if (fnIsDate(oToDateVal) == false) {
                txtToDate.focus();
                return false;
            }
            else {
                var oSysDate = new Date();
                var oFromDate = oFromDateVal.split("/");
                var oToDate = oToDateVal.split("/");
                var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
                var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
                if (oSysDate < new Date(oFromDateFormatted)) {
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
                    alert("To Date should be greater than From Date");
                    txtToDate.value = "";
                    txtToDate.focus();
                    return false;
                }
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
        Branch - Reconsilation
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upBrReconsilation" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReportPDF" />
                <asp:PostBackTrigger ControlID="btnReportExcel" />
                <asp:PostBackTrigger ControlID="btnReportRTF" />
                <asp:PostBackTrigger ControlID="crBranchReconcilationHO" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="PanelHeaderDtls" runat="server">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable1" class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAccPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlAccPeriod" TabIndex="1" runat="server" SkinID="DropDownListNormal"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAccPeriod_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                                                TabIndex="1" Enabled="false" SkinID="DropDownListNormal" />
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtFromDate" SkinID="TextBoxDisabled" TabIndex="1" runat="server" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div class="reportButtons">
                    <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                        SkinID="ButtonViewReport" OnClientClick="javaScript:return FnValidate();" TabIndex="3" />
                    <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                    <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                    <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                        OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                    <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
                </div>
                <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                    <uc:CrystalReport ID="crBranchReconcilationHO" runat="server" ReportName="CustomerOutstanding" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
