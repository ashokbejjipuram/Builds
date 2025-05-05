<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StatementOfAccountA4.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.StatementOfAccountA4" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlCusType = document.getElementById('<%=ddlCustomerType.ClientID%>');
            var Customer = document.getElementById('<%=ddlCustomer.ClientID%>_ddlCustomer_TextBox');
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');

            if ((Customer.value.trim() == null || FromCustomer.value.trim() == "" || FromCustomer.value.trim() == "0")) {
                alert("Customer/Branch/Supplier should be selected");
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
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        Statement Of Account - A4
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
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
                                            <asp:Label ID="lblCustomerType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlCustomerType" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                                                OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblMonthYear" runat="server" Text="Month & Year" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlMonthYear" runat="server" TabIndex="4" SkinID="DropDownListNormal"
                                                AutoPostBack="false" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                                ID="ddlCustomer" SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlChangedEvent">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblReportSubType" runat="server" Text="SOA Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList runat="server" DropDownStyle="DropDownList" ID="ddlReportSubType" SkinID="DropDownListNormal">
                                                <asp:ListItem Value="A">--All--</asp:ListItem>
                                                <asp:ListItem Value="O">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="N">Nil Outstanding</asp:ListItem>
                                                <asp:ListItem Value="C">Credit Balance</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" runat="server" Text="Report" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="7" SkinID="DropDownListNormal" Enabled="false">
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
                    OnClientClick="javaScript:return Validate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport runat="server" ID="crStatementOfAccountA4" OnUnload="crStatementOfAccountA4_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
