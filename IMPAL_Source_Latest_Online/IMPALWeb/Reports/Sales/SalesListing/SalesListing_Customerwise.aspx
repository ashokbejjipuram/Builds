<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesListing_Customerwise.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.SalesListing_Customer_wise_" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var ddlFromLine = document.getElementById('<%=ddlFromLine.ClientID%>');
            var ddlToLine = document.getElementById('<%=ddlToLine.ClientID%>');
            if (ddlFromLine.value != "" && ddlFromLine.value != null) {
                if (ddlToLine.value == "0" || ddlToLine.value == null) {
                    alert("To Line should not be null");
                    ddlToLine.focus();
                    return false;
                }
                else {
                    return true;
                }
            }
            else
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
        Sales Listing(Customer wise)
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crSalesListing_Custwise" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable2" class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label><%--<span class="asterix">*</span>--%>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="cboCustomer" runat="server" DropDownStyle="DropDownList"
                                                AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="cboCustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromLine" runat="server" Text="From Line" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlFromLine" runat="server" TabIndex="2" SkinID="DropDownListNormal"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFromLine_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToLine" runat="server" Text="To Line" SkinID="LabelNormal"></asp:Label><span
                                                id="astToLine" runat="server" visible="false" class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlToLine" runat="server" TabIndex="3" SkinID="DropDownListNormal">
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
                                                TabIndex="4"></asp:TextBox>
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
                                                TabIndex="5"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                runat="server">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                                <div id="CustInfo" visible="false" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
                                    <%--<div class="reportFilters">--%>
                                    <table id="reportFiltersTable1" class="reportFiltersTable">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblCustCode" runat="server" Text="Customer Code" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtCustCode" runat="server" SkinID="TextBoxNormal" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAddress1" runat="server" Text="Address1" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAddress2" runat="server" Text="Address2" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblAddress3" runat="server" Text="Address3" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAddress4" runat="server" Text="Address4" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblLocation" runat="server" Text="Location" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</div>--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="6" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crSalesListing_Custwise" runat="server" OnUnload="crSalesListing_Custwise_Unload" ReportName="SalesListing_Custwise" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
