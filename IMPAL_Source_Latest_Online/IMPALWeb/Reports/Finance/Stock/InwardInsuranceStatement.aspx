<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InwardInsuranceStatement.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Stock.InwardInsuranceStatement" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Inward Insurance/Non-Insurance Statement
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crInwardInsuranceStatement" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInsurance" runat="server" Text="Insurance" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlInsurance" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="4" OnClientClick="javaScript:return fnValidate();"
                    SkinID="ButtonViewReport" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crInwardInsuranceStatement" runat="server" ReportName="InwardInsuranceStatement" OnUnload="crInwardInsuranceStatement_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
