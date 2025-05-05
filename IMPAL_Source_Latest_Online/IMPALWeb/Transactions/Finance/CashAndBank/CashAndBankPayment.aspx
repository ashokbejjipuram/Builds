<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashAndBankPayment.aspx.cs" Inherits="IMPALWeb.Finance.CashAndBankPayment" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<asp:Content ID="cntCashAndBankPayment" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/CashAndBankPayment.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function fnReportBtn() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
            <ContentTemplate>
                <div id="divCashAndBankPayment" runat="server">
                    <div class="subFormTitle">
                        CASH & BANK - PAYMENT
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
                                <asp:TextBox ID="txtTransactionAmount" runat="server" SkinID="TextBoxNormal" onpaste="return false;" ondragstart="return false;" ondrop="return false;" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
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
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            Cash & Bank Details
                        </div>
                        <div class="gridViewScrollFullPage">
                            <asp:GridView ID="grvCashAndBankPayment" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                SkinID="GridViewTransaction" OnRowDataBound="gvResults_RowDataBound" OnRowDeleting="gvResults_RowDeleting">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chart of Account">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrdChartOfAccount" runat="server" ReadOnly="true" Text='<%# Bind("Chart_of_Account_Code") %>'
                                                SkinID="TextBoxDisabledBig" Enabled="false"></asp:TextBox>
                                            <uc1:ChartAccount runat="server" ID="COA" DefaultBranch="true" OnSearchImageClicked="COA_SearchImageClicked" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnadd" Text="Add Row" OnClientClick="return GridValidate(1);" SkinID="GridViewButtonFooter"
                                                runat="server" OnClick="btnadd_Click" />
                                        </FooterTemplate>
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
                                            <asp:TextBox ID="txtGrdAmount" onkeypress="return CurrencyNumberOnly();" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                                Text='<%# Bind("Amount","{0:0.00}") %>' SkinID="GridViewTextBox" onchange="return calculateTotal()"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTotalAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrdRemarks" runat="server" Text='<%# Bind("Remarks") %>' SkinID="GridViewTextBoxBig"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="transactionButtons">
                            <div class="transactionButtonsHolder">
                                <asp:Button ID="btnTransactionDetails" runat="server" Text="Transaction Details" SkinID="ButtonNormalBig"
                                    OnClick="btnTransactionDetails_Click" OnClientClick="javascript:return ValidateTransactionFields();" />
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" OnClick="BtnSubmit_Click"
                                    OnClientClick="javascript: return GridValidate(2);" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" SkinID="ButtonNormal" OnClick="btnReport_Click"
                                    OnClientClick="javaScript:return fnReportBtn()" />
                            </div>
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="RowNum" type="hidden" value="0" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
            <UC:CrystalReport runat="server" ID="crCashAndBank" OnUnload="crCashAndBank_Unload" />
        </div>
    </div>
</asp:Content>
