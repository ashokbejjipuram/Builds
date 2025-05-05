<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StatementOfAccount.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.StatementOfAccount" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {

            var ddlCusType = document.getElementById('<%=ddlCustomerType.ClientID%>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
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
            else if (ddlCusType.value == "Customer") {
                return ValidateDates(txtFromDate, txtToDate);
            }
            else
            { return true; }
        }
        
        function Reset() {

            var today = GetTodaysDate();
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txttoDate = document.getElementById('<%=txtToDate.ClientID%>');
            txtFromDate.value = today;
            txttoDate.value = today;

            var cbCustomerName = document.getElementById('<%=cboFromCustomer.ClientID%>');
            cbCustomerName.getElementsByTagName('input')[0].value = '';
            cbCustomerName.getElementsByTagName('input')[1].value = '-1';

            var cbCustomerName = document.getElementById('<%=cboToCustomer.ClientID%>');
            cbCustomerName.getElementsByTagName('input')[0].value = '';
            cbCustomerName.getElementsByTagName('input')[1].value = '-1';

            var ddlReportType = document.getElementById('<%=ddlReportType.ClientID%>');
            ddlReportType.options[0].selected = true;
            var divCustomerInfo = document.getElementById('<%=divCustomerInfo.ClientID%>');
            divCustomerInfo.style.display = 'none';
            return false;
        }
    </script>

    <div class="reportFormTitle">
        Statement Of Account
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td>
                    <table class="reportFiltersTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCustomerType" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
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
                                <asp:Label ID="lblFromCustomer" runat="server" Text="From Branch/Customer/Supplier"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                    ID="cboFromCustomer" SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlChangedEvent">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToCustomer" runat="server" Text="To Branch/Customer/Supplier" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                    ID="cboToCustomer" SkinID="DropDownListNormal" TabIndex="3" OnSelectedIndexChanged="ddlChangedEvent">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:8px"><td></td></tr>
                        <div id="divDate" style="display: none" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFromdate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="5"
                                        runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="6" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </div>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="7" SkinID="DropDownListNormal"
                                    AutoPostBack="True">
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
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" TabIndex="8" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return Validate();" OnClick="btnReport_Click" />
            <%-- <asp:Button ID="btnReset" Text="Reset" runat="server" TabIndex="9" SkinID="ButtonViewReport"
                                    OnClientClick="javaScript:return Reset();" onclick="btnReset_Click" />--%>
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crStatementOfAccount" OnUnload="crStatementOfAccount_Unload" />
    </div>
</asp:Content>
