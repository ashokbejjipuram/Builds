<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashAndBankCancellation.aspx.cs" Inherits="IMPALWeb.Finance.CashAndBankCancellation" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<asp:Content ID="cntCashAndBankCancellation" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/CashAndBankCancellation.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="divCashAndBankCancellation" runat="server">
                    <div class="subFormTitle subFormTitleExtender250">
                        CASH & BANK - CANCELLATION
                    </div>
                    <table id="reportFiltersTable" class="subFormTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch"
                                    DataTextField="BranchName" DataValueField="BranchCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTransactionNumber" runat="server" Text="Transaction Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionNumber" runat="server" SkinID="TextBoxNormal" ReadOnly="false" MaxLength="14"></asp:TextBox>
                                <asp:DropDownList ID="ddlTransactionNumber" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" OnClientClick="return ValidateDocNo()" />
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
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAccountingPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPayment" runat="server" Text="Payment Type" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlPayment" runat="server" SkinID="DropDownListDisabled" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionAmount" runat="server" Text="Transaction Amount" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChartOfAccount" runat="server" Text="Chart of Account" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="TextBoxDisabledBig" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCashCheque" runat="server" Text="Cash/Cheque" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCashCheque" runat="server" AutoPostBack="True" SkinID="DropDownListDisabled" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="Reason to Cancel"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols" colspan="3">
                                <asp:TextBox ID="txtReason" runat="server" SkinID="TextBoxNormalBig" TextMode="MultiLine" Height="40px" Width="200px"></asp:TextBox>
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
                                <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChequeDate" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBank" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankBranch" runat="server" SkinID="LabelNormal" Text="Bank Branch"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBankBranch" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblLocalOutstation" runat="server" SkinID="LabelNormal" Text="Local/Outstation"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlLocalOutstation" runat="server" SkinID="DropDownListDisabled" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDate" runat="server" SkinID="LabelNormal" Text="Reference Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            Cash & Bank Details
                        </div>
                        <div class="gridViewScrollFullPage">
                            <asp:GridView ID="grvCashAndBankCancellation" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                SkinID="GridViewTransaction" OnRowDataBound="gvResults_RowDataBound">
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
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="transactionButtons">
                            <div class="transactionButtonsHolder">
                                <asp:Button ID="BtnCancel" SkinID="ButtonNormalBig" runat="server" Text="Cancel" OnClick="BtnCancel_Click" OnClientClick="return funCBCancellationSubmit();" Style="background-color: Red; color: whitesmoke; font-weight: bold; width: 100px" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="RowNum" type="hidden" value="0" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
