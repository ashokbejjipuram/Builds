<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesListing_Itemwise.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.SalesListing_item_wise_1" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

    <div class="reportFormTitle">
        Sales Listing(Item wise)
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crSalesListingItemwise_Report" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLineCode" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlLineCode" runat="server" SkinID="DropDownListNormal" TabIndex="1"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLineCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPartNo" runat="server" Text="Part Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlPartNo" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportType" runat="server" SkinID="LabelNormal" Text="Report Type"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="8">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="4"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:ListBox ID="lstItem" runat="server" SkinID="ListBoxNormal" SelectionMode="Multiple"
                                    Rows="5" TabIndex="5"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="reportButtons">
                                <asp:Button ID="btNextItem" runat="server" Text="Next Item" SkinID="ButtonNormal"
                                    TabIndex="6" OnClick="btNextItem_Click" />
                                <asp:Button ID="btRemove" runat="server" SkinID="ButtonNormal" TabIndex="7" OnClick="btRemove_Click"
                                    Text="Remove" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                    TabIndex="9" OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crSalesListingItemwise_Report" runat="server" ReportName="SalesListingItemwise_Report" OnUnload="crSalesListingItemwise_Report_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
