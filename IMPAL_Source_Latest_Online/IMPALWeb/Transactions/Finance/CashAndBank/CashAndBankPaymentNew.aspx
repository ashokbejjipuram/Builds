<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashAndBankPaymentNew.aspx.cs" Inherits="IMPALWeb.Finance.CashAndBankPaymentNew" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<asp:Content ID="cntCashAndBankPaymentNew" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/CashAndBankPaymentNew.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function fnReportBtn() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divCashAndBankPaymentNew" runat="server">
                    <div class="subFormTitle">
                        CASH & BANK - PAYMENT NEW
                    </div>
                    <table id="reportFiltersTable" class="subFormTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionNumber" runat="server" Text="Transaction Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlTransactionNumber" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionDate" runat="server" SkinID="TextBoxDisabled" onchange="return CheckTransDate(this.id)"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTransactionDate" runat="server" EnableViewState="true"
                                    Format="dd/MM/yyyy" TargetControlID="txtTransactionDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlAccountingPeriod_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAccountingPeriod" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPayment" runat="server" Text="Payment Type" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlPayment" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionAmount" runat="server" Text="Transaction Amount" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionAmount" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChartOfAccount" runat="server" Text="Chart of Account" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="True"></asp:TextBox>
                                <uc1:ChartAccount runat="server" DefaultBranch="true" ID="BankAccNo" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCashCheque" runat="server" Text="Cash/Cheque" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCashCheque" runat="server" AutoPostBack="True" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNoOfTransactions" runat="server" Text="No of Transactions" SkinID="LabelNormal"></asp:Label>
                                <span id="idspan" class="asterix" runat="server">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNoOfTransactions" runat="server" SkinID="TextBoxNormal" MaxLength="3"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        CHEQUE/DRAFT DETAILS
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChequeNumber" runat="server" SkinID="LabelNormal" Text="Cheque Number"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnly();" MaxLength="6"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChequeDate" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtChequeDate" OnClientShown="CheckToday" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBank" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankBranch" runat="server" SkinID="LabelNormal" Text="Bank Branch"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBankBranch" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblLocalOutstation" runat="server" SkinID="LabelNormal" Text="Local/Outstation"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlLocalOutstation" runat="server" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDate" runat="server" SkinID="LabelNormal" Text="Reference Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDate" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calExtReferenceDate" runat="server" EnableViewState="true"
                                    Format="dd/MM/yyyy" TargetControlID="txtReferenceDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetailsExcel" runat="server">
                        <div class="subFormTitle">
                            File Upload
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:FileUpload runat="server" ID="btnFileUpload" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadExcel" runat="server" Text="Upload File" SkinID="ButtonNormal"
                                        OnClick="btnUploadExcel_Click" OnClientClick="javascript: return ValidateTransactionFields(1);" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label" colspan="4">
                                <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                                <asp:HiddenField runat="server" ID="HdnExcelTotalValue" />
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            Cash & Bank Details
                        </div>
                        <div class="gridViewScrollFullPage">
                            <asp:GridView ID="grvCashAndBankPaymentNew" runat="server" AutoGenerateColumns="False"
                                SkinID="GridViewTransaction" OnRowDataBound="gvResults_RowDataBound" OnRowDeleting="gvResults_RowDeleting">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Serial_Number" HeaderText="S.No" />
                                    <asp:TemplateField HeaderText="Chart of Account">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChartOfAccount" runat="server" ReadOnly="true" Text='<%# Bind("Chart_of_Account_Code") %>'
                                                SkinID="GridViewTextBoxBig"></asp:TextBox>
                                            <uc1:ChartAccount runat="server" ID="COA" DefaultBranch="true" OnSearchImageClicked="COA_SearchImageClicked" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indicator">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlIndicator" runat="server" SkinID="GridViewDropDownList"
                                                Style="width: 80px !important;">
                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" onpaste="return false;" onkeypress="return CurrencyNumberOnly();" onchange="return CalcTotalValue();" runat="server"
                                                Text='<%# Bind("Amount","{0:0.00}") %>' SkinID="GridViewTextBox" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' SkinID="GridViewTextBoxBig"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <input id="hdnTotalAmount" type="hidden" runat="server" />
                        <div class="transactionButtons">
                            <div class="transactionButtonsHolder">
                                <asp:Button ID="btnReportTransaction" runat="server" Text="Transaction Details" SkinID="ButtonNormalBig"
                                    OnClick="btnReportTransaction_Click" OnClientClick="javascript: return ValidateTransactionFields(2);" />
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" OnClick="BtnSubmit_Click"
                                    OnClientClick="javascript: return GridValidate();" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" SkinID="ButtonNormal" OnClick="btnReport_Click" OnClientClick="javaScript:return fnReportBtn()" />
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScroll">
                                </div>
                                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                                <input id="hdnRowCnt" type="hidden" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="RowNum" type="hidden" value="0" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnReport" />
                <asp:PostBackTrigger ControlID="btnUploadExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
            <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
            <UC:CrystalReport runat="server" ID="crCashAndBank" OnUnload="crCashAndBank_Unload" />
        </div>
    </div>
</asp:Content>
