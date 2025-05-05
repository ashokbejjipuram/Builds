<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="PendingBills.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.PendingBills" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtDate.ClientID%>');
            var FromCustomer = document.getElementById('<%=cboFromCustomer.ClientID%>_cboFromCustomer_TextBox');
            var ToCustomer = document.getElementById('<%=cboToCustomer.ClientID%>_cboToCustomer_TextBox');

            if ((FromCustomer.value.trim() == null || FromCustomer.value.trim() == "" || FromCustomer.value.trim() == "0")
                && (ToCustomer.value.trim() != "")) {
                alert("From Customer/Branch/Supplier should be selected");
                return false;
            }
            else if ((ToCustomer.value.trim() == null || ToCustomer.value.trim() == "" || ToCustomer.value.trim() == "0")
                && (FromCustomer.value.trim() != "")) {
                alert("To Customer/Branch/Supplier should be selected");
                return false;
            }
            else {
                return ValidateOutstandingDate(txtFromDate);
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
        List Of Pending Bills
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="crPendingBills" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromCustomer" runat="server" Text="From Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                                ID="cboFromCustomer" SkinID="DropDownListNormal" TabIndex="1" OnSelectedIndexChanged="ddlChangedEvent">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToCustomer" runat="server" Text="To Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                                ID="cboToCustomer" SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlChangedEvent">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="3" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="4" SkinID="DropDownListNormal">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCustomerInfo" runat="server">
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
                <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                    TabIndex="5" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="Div1" runat="server">
                <uc1:CrystalReport runat="server" ID="crPendingBills" OnUnload="crPendingBills_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
