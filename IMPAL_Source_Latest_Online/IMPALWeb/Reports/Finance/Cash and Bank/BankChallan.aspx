<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BankChallan.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.BankChallan" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlReportType = document.getElementById('<%=ddlReportType.ClientID%>');
            var ddlBank = document.getElementById('<%=ddlBank.ClientID%>');
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');
            var ddlAccountingPeriod = document.getElementById('<%=ddlAccountingPeriod.ClientID%>');
            var ddlChallanNo = document.getElementById('<%=ddlChallanNo.ClientID %>');
            if (ddlReportType.value == "Report") {
                if (ValidateBank(ddlBank, ddlBranch, ddlAccountingPeriod) == false) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                if (ValidateChallan(ddlChallanNo) == false)
                    return false;
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
        Bank Challan
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crBankChallan" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <table id="reportFiltersTable" runat="server">
                    <tr>
                        <td>
                            <div class="reportFilters">
                                <table id="reportFiltersTable2" class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" AutoPostBack="true" TabIndex="1" ID="ddlReportType"
                                                runat="server" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label SkinID="LabelNormal" ID="lblChallan" runat="server" Text="Challan Number"></asp:Label><span
                                                class="asterix" id="idSpan" runat="server">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList SkinID="DropDownListNormal" TabIndex="2" ID="ddlChallanNo" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="BankInfo" runat="server">
                                <div class="reportFilters">
                                    <table id="reportFiltersTable1" class="reportFiltersTable">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Accounting Period"></asp:Label>
                                                <span class="asterix">*</span>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="label">
                                                <asp:Label SkinID="LabelNormal" ID="lblBank" runat="server" Text="Bank"></asp:Label><span
                                                    class="asterix">*</span>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList SkinID="DropDownListNormal" AutoPostBack="true" TabIndex="3" ID="ddlBank"
                                                    runat="server" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="label">
                                                <asp:Label SkinID="LabelNormal" ID="lblBranch" runat="server" Text="Branch"></asp:Label><span
                                                    class="asterix">*</span>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList SkinID="DropDownListNormal" TabIndex="4" ID="ddlBranch" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="label">
                                                <asp:Label SkinID="LabelNormal" ID="lblLocalOutstatn" runat="server" Text="Local/Outstation"></asp:Label><span
                                                    class="asterix">*</span>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList SkinID="DropDownListNormal" TabIndex="5" ID="ddlLocalOutstatn"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label SkinID="LabelNormal" ID="lblTransfer" runat="server" Text="Transfer"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:CheckBox ID="chkTransfer" runat="server" TabIndex="6" />
                                            </td>
                                            <td class="label">
                                                <asp:Label SkinID="LabelNormal" ID="lblTownWiseGroup" runat="server" Text="Townwise Grouping"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:CheckBox ID="chkGrouping" runat="server" TabIndex="7" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();"
                    Text="Generate Report" SkinID="ButtonViewReport" TabIndex="11" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport runat="server" ID="crBankChallan" OnUnload="crBankChallan_Unload" ReportName="BankChallan" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
