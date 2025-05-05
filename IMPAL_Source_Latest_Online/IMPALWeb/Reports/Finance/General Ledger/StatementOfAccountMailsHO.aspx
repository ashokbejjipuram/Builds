<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StatementOfAccountMailsHO.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.StatementOfAccountMailsHO" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {

            var ddlCusType = document.getElementById('<%=ddlCustomerType.ClientID%>');
            var FromCustomer = document.getElementById('<%=cboFromCustomer.ClientID%>_cboFromCustomer_TextBox');
            var ToCustomer = document.getElementById('<%=cboToCustomer.ClientID%>_cboToCustomer_TextBox');
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');

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

            if (ddlMonthYear.value.trim() == null || ddlMonthYear.value.trim() == "" || ddlMonthYear.value.trim() == "0") {
                alert("Month Year should be selected");
                ddlMonthYear.focus();
                return false;
            }
            else { return true; }
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        Statement Of Account - A4
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCustomerType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlCustomerType" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblMonthYear" runat="server" Text="Month & Year" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlMonthYear" runat="server" TabIndex="4" SkinID="DropDownListNormal" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromCustomer" runat="server" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" DropDownStyle="DropDownList" ID="cboFromCustomer" SkinID="DropDownListNormal" Enabled="false" TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToCustomer" runat="server" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" DropDownStyle="DropDownList" ID="cboToCustomer" SkinID="DropDownListNormal" Enabled="false" TabIndex="3">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" runat="server" Text="Report" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" runat="server" Enabled="false" TabIndex="7" SkinID="DropDownListNormal">
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
                <asp:Button ID="btnReport" Text="Generate Report" runat="server" TabIndex="8" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return validate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnSendMails" Text="Send Mails" runat="server" TabIndex="8" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnSendMails_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Visible="false" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport runat="server" ID="crStatementOfAccountMailsHO" OnUnload="crStatementOfAccountMailsHO_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
