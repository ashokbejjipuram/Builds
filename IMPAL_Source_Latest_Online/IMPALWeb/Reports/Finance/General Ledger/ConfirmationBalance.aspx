<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ConfirmationBalance.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.ConfirmationBalance" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function Validate() {
            var dateason = document.getElementById('<%=txtDate.ClientID %>');
            var fromCustomer = document.getElementById('<%=ddlFromCustomer.ClientID %>_ddlFromCustomer_TextBox');
            var toCustomer = document.getElementById('<%=ddlToCustomer.ClientID %>_ddlToCustomer_TextBox');
            if ((fromCustomer.value != "") && (toCustomer.value == "")) {
                alert("To name should not be null.");
                return false;
            }
            else {
                return ValidateOutstandingDate(dateason);
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
        Confirmation Balance
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crconfirmationbalance" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="FiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCustomerSupplier" runat="server" Text="Customer/Supplier" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlCustomerSupplier" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                                                OnSelectedIndexChanged="ddlCustomerSupplier_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromCustomer" runat="server" Text="From Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <%--<asp:DropDownList ID="ddlFromCustomer" runat="server" SkinID="ComboBoxNormal"
                                    TabIndex="2" AutoPostBack="true" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                    OnSelectedIndexChanged="ddlFromCustomer_SelectedIndexChanged1">
                                </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlFromCustomer" runat="server" TabIndex="2" SkinID="DropDownListNormal" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlFromCustomer_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToCustomer" runat="server" Text="To Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <%--<ajaxToolkit:ComboBox ID="ddlToCustomer" runat="server" SkinID="ComboBoxNormal" TabIndex="2"
                                    AutoPostBack="true" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                    OnSelectedIndexChanged="ddlFromCustomer_SelectedIndexChanged1">
                                </ajaxToolkit:ComboBox>--%>
                                            <asp:DropDownList ID="ddlToCustomer" runat="server" TabIndex="3" SkinID="DropDownListNormal" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlFromCustomer_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDate" runat="server" Text="Date As On" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" TabIndex="4" />
                                            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" TargetControlID="txtDate"
                                                Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divCustomerAddress" visible="false" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
                                    <table class="reportFiltersTable">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblcustcode" runat="server" Text="Customer Code" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtcustcode" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lbladdress1" runat="server" Text="Address1" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtaddress1" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lbladdress2" runat="server" Text="Address2" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtaddress2" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lbladdress3" runat="server" Text="Address3" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtaddress3" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lbladdress4" runat="server" Text="Address4" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtaddress4" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lbllocation" runat="server" Text="Location" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtlocation" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                    Enabled="false" />
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
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport runat="server" ID="crconfirmationbalance" OnUnload="crconfirmationbalance_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
