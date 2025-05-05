<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JournalVoucherFinal.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.GeneralLedger.JournalVoucherFinal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/JournalVoucher.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle subFormTitleExtender250">
                        JOURNAL VOUCHER - APPROVAL
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
                                <asp:TextBox ID="txtJVDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListDisabled"
                                    OnSelectedIndexChanged="ddlAccountingPeriod_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAccountingPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
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
                                <asp:TextBox ID="txtReferenceDocumentDate" runat="server" SkinID="TextBoxCalendarExtenderDisabled"
                                    onblur="return CheckValidDate(this.id, true, 'Reference Document Date');" Enabled="false"></asp:TextBox>
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
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    TRANSACTION DETAILS
                </div>
                <asp:Panel ID="PanelMessage" runat="server" Visible="false">
                    <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal"></asp:Label>
                </asp:Panel>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvTransactionDetails" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" ShowFooter="true" SkinID="GridViewTransaction" OnRowDataBound="grvTransactionDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <%--<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" onClick="SelectedChangeAll(this.id)" runat="server" />
                                </HeaderTemplate>--%>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelected" onClick="SelectedChangeCheckBox(this)" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'
                                        Style="width: 30px !important;" Enabled="false" SkinID="TextBoxDisabledSmall"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chart Of Account">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="TextBoxDisabled" Style="width: 150px" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        Enabled="false"> </asp:TextBox>
                                    <%--<asp:Panel runat="server" ID="PanelCOA">--%>
                                    <uc1:ChartAccount runat="server" DefaultBranch="true" ID="BankAccNo" Filter="GSTOPTIONS" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                                    <%--</asp:Panel>--%>
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
                                        Style="width: 250px !important;" Text='<%# Bind("Remarks") %>' TabIndex="0"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.Doc Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlRefDocType" runat="server" SkinID="DropDownListDisabled"
                                        Style="width: 80px !important;" Enabled="false">
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
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalLabel" runat="server" Text="<b>Total</b>"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDebitAmount" runat="server" Style="width: 80px !important;" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Debit_Amount") %>' onkeypress="return CurrencyNumberOnly();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                        onkeyup="return CurrencyDecimalOnlyFinal(this.id, event);" onblur="return CurrencyRoundOffOnly(this.id, event);"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotalDebitAmount" runat="server" Style="width: 80px !important;" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCreditAmount" runat="server" Style="width: 80px !important;" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Credit_Amount") %>' onkeypress="return CurrencyNumberOnly();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                        onkeyup="return CurrencyDecimalOnlyFinal(this.id, event);" onblur="return CurrencyRoundOffOnly(this.id, event);"></asp:TextBox>
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
                <input id="ChkStatus" runat="server" type="hidden" />
                <asp:TextBox ID="txthdSlab" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Approve" Style="background-color: Green; color: White" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnReject" SkinID="ButtonNormal" Style="background-color: Red; color: White" runat="server" Text="Reject" OnClick="BtnReject_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
