<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CollectionAging.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.CollectionAging" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');
            var ddlFromCustomer = document.getElementById('<%=ddlFromCustomer.ClientID%>');
            var ddlToCustomer = document.getElementById('<%=ddlToCustomer.ClientID%>');
            var txtFromDate = document.getElementById('<%=txtDate.ClientID%>');

            if (ddlFromCustomer.value == null || ddlFromCustomer.value == "")
                return true;
            else
                return fnValidateFinance(ddlBranch, ddlFromCustomer, ddlToCustomer, txtFromDate);
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
        Collection Details
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crCollectionAging_Report" />
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
                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label><span
                                                id="astBranch" class="asterix" runat="server">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromCustomer" runat="server" Text="From Customer" SkinID="LabelNormal"></asp:Label><span
                                                id="astFromCust" runat="server" class="asterix" visible="false">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="ddlFromCustomer" runat="server"
                                                AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlFromCustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblToCustomer" runat="server" Text="To Customer" SkinID="LabelNormal"></asp:Label><span
                                                id="astToCust" runat="server" visible="false" class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" ID="ddlToCustomer" runat="server" AutoPostBack="true"
                                                TabIndex="3" OnSelectedIndexChanged="ddlToCustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label><span
                                                id="astDate" runat="server" class="asterix" visible="false">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" TabIndex="4"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calDate" Format="dd/MM/yyyy" TargetControlID="txtDate"
                                                runat="server">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="6" SkinID="DropDownListNormal">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div id="CustInfo" visible="false" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
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
                                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxNormal" ReadOnly="True"
                                                    TextMode="MultiLine" Height="78px"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAddress2" runat="server" Text="Address2" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblAddress3" runat="server" Text="Address3" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxNormal" Height="78px"
                                                    ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAddress4" runat="server" Text="Address4" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxNormal" Height="62px"
                                                    ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblLocation" runat="server" Text="Location" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" ReadOnly="True"></asp:TextBox>
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
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="7" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crCollectionAging_Report" runat="server" OnUnload="crCollectionAging_Report_Unload" ReportName="CollectionAging_Report" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
