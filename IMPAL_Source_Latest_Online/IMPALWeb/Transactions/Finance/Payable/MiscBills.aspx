<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="MiscBills.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.MiscBills" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/FinanceMiscBills.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        MISC. BILLS</div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
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
                                <asp:Label ID="lblInvoiceNumber" runat="server" Text="Document Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlInvoiceNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAccountingPeriod" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Supplier Line" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierLine" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlSupplierLine_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Supplier Name" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSupplierName" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Supplier Place" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSupplierPlace" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDocumentNumber" runat="server" Text="Reference Document Number"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDocumentNumber" runat="server" SkinID="TextBoxNormal"
                                    MaxLength="20"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDocumentDate" runat="server" Text="Reference Document Date"
                                    SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDocumentDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return checkDateLessthanSysDate(this.id, 'Reference Document Date');"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceRequDocDate" PopupButtonID="imgRequDocDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtReferenceDocumentDate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        TOTALS</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Invoice Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Invoice Amount');"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Excise Duty Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtExciseDutyAmount" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Excise Duty Amount');"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label6" runat="server" Text="Sale Tax Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSaleTaxAmount" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Sale Tax Amount');"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label7" runat="server" Text="Other Charges" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOtherCharges" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Other Charges');"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label8" runat="server" Text="Other Deductions" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOtherDeductions" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Other Deductions');"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label9" runat="server" Text="Advances" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAdvances" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onblur="return CheckLessthanZero(this.id, 'Advances');"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS</div>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvMiscDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        SkinID="GridViewTransaction">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'
                                        Style="width: 30px !important;" Enabled="false" SkinID="GridViewTextBoxSmall"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter"
                                        Style="width: 40px !important;" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chart Of Account">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        Enabled="false"> </asp:TextBox>
                                    <uc1:ChartAccount runat="server" DefaultBranch="true" ID="BankAccNo" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Description") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUnit" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Unit_of_Measurement") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" MaxLength="200" SkinID="GridViewTextBox"
                                        onkeypress="return CurrencyNumberOnly();" onblur="return CheckGreaterthanZero(this.id, 'Quantity');"
                                        Style="width: 60px !important;" Text='<%# Bind("Quantity") %>' TabIndex="0"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" MaxLength="20" SkinID="GridViewTextBox"
                                        onkeypress="return CurrencyNumberOnly();" onblur="return CheckGreaterthanZero(this.id, 'Rate');"
                                        Style="width: 80px !important;" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("Discount") %>' Style="width: 80px !important;"
                                        onkeypress="return CurrencyNumberOnly();" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNetPrice" runat="server" Text='<%# Bind("Net_Price") %>' Style="width: 80px !important;"
                                        SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%-- <asp:Panel ID="Panel1" runat="server">
                        <div id="idTotal" align="right" style="width: 999px; float: left !important; margin-right: 10px !important;">
                            Total :
                            <asp:TextBox ID="lblTotalDebitAmount" runat="server" SkinID="GridViewTextBox" Text="0"
                                Style="width: 80px !important;" Enabled="false"></asp:TextBox>
                            <asp:TextBox ID="lblTotalCreditAmount" runat="server" SkinID="GridViewTextBox" Text="0"
                                Style="width: 80px !important;" Enabled="false"></asp:TextBox>
                        </div>
                    </asp:Panel>--%>
                </div>
                <br />
                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <input id="hdnRowCnt" type="hidden" runat="server" />
                <asp:TextBox ID="txthdSlab" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlInvoiceNumber" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
