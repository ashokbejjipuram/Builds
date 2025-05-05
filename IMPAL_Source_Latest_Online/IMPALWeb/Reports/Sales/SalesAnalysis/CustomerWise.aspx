<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWise.aspx.cs" Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.CustomerWise"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
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
        function Reset() {
            var today = GetTodaysDate();
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            txtFromDate.value = today;
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            txtToDate.value = today;
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            hidFromDate.value = "";
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            hidToDate.value = "";
            var cbCustomerName = document.getElementById('<%=cbCustomerName.ClientID%>');
            cbCustomerName.getElementsByTagName('input')[0].value = '';
            cbCustomerName.getElementsByTagName('input')[1].value = '-1';
            var ddlReportType = document.getElementById('<%=ddlReportType.ClientID%>');
            ddlReportType.options[0].selected = true;
            var divCustomerInfo = document.getElementById('<%=divCustomerInfo.ClientID%>');
            divCustomerInfo.style.display = 'none';
            return false;
        }

        function cbCustomerName_onBlur(txtCustomerName) {
            if (txtCustomerName != null) {
                var divCustomerInfo = document.getElementById('<%=divCustomerInfo.ClientID%>');
                if (txtCustomerName.value == "")
                    divCustomerInfo.style.display = 'none';
                else
                    divCustomerInfo.style.display = '';
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
        Customer with previous Year
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crCustomerWise" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable2" class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCustomerName" SkinID="LabelNormal" runat="server" Text="Customer Name"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="cbCustomerName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                                TabIndex="1" OnSelectedIndexChanged="cbCustomerName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromDate" SkinID="LabelNormal" runat="server" Text="From Date"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                TabIndex="2" />
                                            <asp:HiddenField ID="hidFromDate" runat="server" />
                                            <%--Stores value in MM/dd/yyyy format--%>
                                            <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                                                Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToDate" SkinID="LabelNormal" runat="server" Text="To Date"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                TabIndex="3" />
                                            <asp:HiddenField ID="hidToDate" runat="server" />
                                            <%--Stores value in MM/dd/yyyy format--%>
                                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                                                Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="4" DataTextField="DisplayText"
                                                DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCustomerInfo" style="display: none" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
                                    <table class="reportFiltersTable">
                                        <tr>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                    SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="5" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crCustomerWise" runat="server" OnUnload="crCustomerWise_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
