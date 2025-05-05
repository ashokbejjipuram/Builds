<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JournalVoucherHO.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.GeneralLedger.JournalVoucherHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/JournalVoucher.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        JOURNAL VOUCHER
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblJVNumber" runat="server" Text="JV number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtJVNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlJVNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlJVNumber_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblJVDate" runat="server" Text="JV Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtJVDate" runat="server" Enabled="false" SkinID="TextBoxDisabled" onchange="return CheckTransDate(this.id)"></asp:TextBox>
                                <asp:DropDownList ID="ddlJVDate" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlJVDate_SelectedIndexChanged">
                                </asp:DropDownList>
                                <ajaxToolkit:CalendarExtender ID="calTransactionDate" runat="server" EnableViewState="true"
                                    Format="dd/MM/yyyy" TargetControlID="txtJVDate">
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
                                <asp:Label ID="lblReferenceDocumentType" runat="server" Text="Reference Document Type"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDocumentType" runat="server" SkinID="TextBoxDisabled"
                                    Text="JVR" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDocumentDate" runat="server" Text="Reference Document Date"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDocumentDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return CheckValidDateJV(this.id, true, 'Reference Document Date');"></asp:TextBox>
                                <%--<asp:ImageButton ID="imgRequDocDate" ImageUrl="~/Images/Calendar.png" runat="server"
                                    SkinID="ImageButtonCalendar" />--%>
                                <ajaxToolkit:CalendarExtender ID="ceRequDocDate" PopupButtonID="imgRequDocDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtReferenceDocumentDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDocumentNumber" runat="server" Text="Reference Document Number"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDocumentNumber" runat="server" SkinID="TextBoxNormal"
                                    MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                <input id="hdnBranch" type="hidden" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblNarration" runat="server" Text="Narration" maxlength="200" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNarration" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblNoofTransactions" runat="server" Text="No of Transactions" SkinID="LabelNormal"></asp:Label>
                                <span id="idNarr" runat="server" class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNoofTransactions" runat="server" SkinID="TextBoxNormal" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnTransaction" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    TRANSACTION DETAILS
                </div>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvTransactionDetails" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" ShowFooter="true" SkinID="GridViewTransaction" OnRowDataBound="grvTransactionDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'
                                        Style="width: 30px !important;" Enabled="false" SkinID="GridViewTextBoxSmall"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chart Of Account">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        Enabled="false"> </asp:TextBox>
                                    <uc1:ChartAccount runat="server" DefaultBranch="true" ID="BankAccNo" Filter="" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                                    <%--<asp:RequiredFieldValidator ID="ReqtxtChartOfAccount" SetFocusOnError="true" ValidationGroup="BtnSubmit"
                                        ControlToValidate="txtChartOfAccount" runat="server" ErrorMessage="Chart Of Account is required."
                                        Display="None"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="ReqtxtChartOfAccount"
                                        PopupPosition="Right" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" SkinID="TextBoxDisabled" Text='<%# Bind("Description") %>'
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" SkinID="GridViewTextBox"
                                        Style="width: 60px !important;" Text='<%# Bind("Remarks") %>' TabIndex="0"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.Doc Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlRefDocType" runat="server" SkinID="GridViewDropDownList"
                                        Style="width: 80px !important;">
                                        <asp:ListItem Text="JVR" Value="JVR"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.Doc Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRefDocNumber" runat="server" MaxLength="20" SkinID="GridViewTextBox"
                                        Style="width: 80px !important;" Text='<%# Bind("Ref_Doc_Number") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.Doc Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReqDocDate" runat="server" Text='<%# Bind("Ref_Doc_Date") %>'
                                        Style="width: 80px !important;" SkinID="TextBoxCalendarExtenderNormal" onblur="return checkDateForRefDocDate(this.id);"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceReqDocDate" PopupButtonID="imgReqDocDate" Format="dd/MM/yyyy"
                                        runat="server" TargetControlID="txtReqDocDate" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr/Cr">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDrCr" runat="server" SkinID="GridViewDropDownListEscape"
                                        Style="width: 70px !important;">
                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtDrCr" runat="server" SkinID="GridViewTextBox" Style="width: 30px !important;"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalLabel" runat="server" Text="<b>Total</b>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDebitAmount" runat="server" Style="width: 80px !important;" SkinID="GridviewTextBox"
                                        Text='<%# Bind("Debit_Amount") %>' onkeypress="return CurrencyNumberOnly();"
                                        onkeyup="return CurrencyDecimalOnly(this.id, event);" onblur="return CurrencyRoundOffOnly(this.id, event);"
                                        Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalDebitAmount" runat="server" Style="width: 80px !important;" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCreditAmount" runat="server" Style="width: 80px !important;"
                                        SkinID="GridviewTextBox" Text='<%# Bind("Credit_Amount") %>' onkeypress="return CurrencyNumberOnly();"
                                        onkeyup="return CurrencyDecimalOnly(this.id, event);" onblur="return CurrencyRoundOffOnly(this.id, event);"
                                        Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalCreditAmount" runat="server" Style="width: 80px !important;" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <input id="hdnRowCnt" type="hidden" runat="server" />
                <asp:TextBox ID="txthdSlab" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnTransaction" runat="server" ValidationGroup="btnTransaction" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Transaction" OnClick="btnTransaction_Click" />
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                        <%--<asp:Button ID="btnReport" ValidationGroup="btnReport" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Report" />--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlJVNumber" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
