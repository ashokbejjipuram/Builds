<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CustomerLineInvoiceSales.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.CustomerLineInvoiceSales" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var ddlFromLine = document.getElementById('<%=ddlFromLine.ClientID %>');
            var ddlToLine = document.getElementById('<%=ddlToLine.ClientID %>');
            if (ValidateDates(txtFromDate, txtToDate) == false)
                return false;
            if (ValidateLineCode(ddlFromLine, ddlToLine) == false)
                return false;


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
        Customer/Line/Invoice Sales
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crCustLineInvSales" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable2" class="reportFiltersTable" runat="server">
                                    <tr>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="cboCustomer" runat="server" AutoPostBack="true"
                                                TabIndex="1" DropDownStyle="DropDownList" OnSelectedIndexChanged="cboCustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblFromLine" runat="server" Text="From Line"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="ddlFromLine" runat="server" TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblToLine" runat="server" Text="To Line"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="ddlToLine" runat="server" TabIndex="3">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblFromDate" runat="server" Text="From Date"></asp:Label><span
                                                class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtFromDate" runat="server"
                                                TabIndex="4"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" runat="server"
                                                TargetControlID="txtFromDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblToDate" runat="server" Text="To Date"></asp:Label><span
                                                class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" ID="txtToDate" runat="server"
                                                TabIndex="5"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblReportType" runat="server" Text="Report Type"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="ddlReportType" runat="server" TabIndex="6"
                                                OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div id="CustomerInfo" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
                                    <div class="reportFilters">
                                        <table id="reportFiltersTable1" class="reportFiltersTable" runat="server">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblCustCode" runat="server" Text="Customer Code"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtCustCode" runat="server" TabIndex="8"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblAddress1" runat="server" Text="Address1"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtAddress1" runat="server" ReadOnly="True"
                                                        TabIndex="9"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblAddress2" runat="server" Text="Address2"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtAddress2" runat="server" ReadOnly="True"
                                                        TabIndex="10"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblAddress3" runat="server" Text="Address3"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtAddress3" runat="server" ReadOnly="True"
                                                        TabIndex="11"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblAddress4" runat="server" Text="Address4"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtAddress4" runat="server" ReadOnly="True"
                                                        TabIndex="12"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label SkinID="LabelNormal" ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox SkinID="TextBoxNormal" ID="txtLocation" runat="server" ReadOnly="True"
                                                        TabIndex="13"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                    TabIndex="7" SkinID="ButtonViewReport" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport runat="server" ID="crCustLineInvSales" OnUnload="crCustLineInvSales_Unload" ReportName="CustLineInvSales" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
