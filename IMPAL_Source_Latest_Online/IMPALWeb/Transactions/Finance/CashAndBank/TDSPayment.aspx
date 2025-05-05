<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="TDSPayment.aspx.cs"
    Inherits="IMPALWeb.Finance.TDSPayment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/FinanceTDSPayment.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        TDS PAYMENT</div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" DataSourceID="ODS_AllBranch"
                                    DataTextField="BranchName" Enabled="false" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDocumentNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlDocumentNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlDocumentNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDocumentDate" runat="server" Text="Document Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDocumentDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlAccountingPeriod_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAccountingPeriod" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblVendor" runat="server" Text="Vendor" SkinID="LabelNormal"></asp:Label>
                                <span id="Span6" runat="server" class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlVendorCode" AutoPostBack="true" runat="server" SkinID="DropDownListNormal"
                                    DataSourceID="ODS_Vendor" DataTextField="Customer_Name" DataValueField="Customer_Code"
                                    OnSelectedIndexChanged="ddlVendorCode_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblVendorCode" runat="server" SkinID="LabelNormal" Text="Vendor Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtVendorCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblVendorName" runat="server" Text="Vendor Name" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtVendorName" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblVendorLocation" runat="server" Text="Vendor Location" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtVendorLocation" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="GSTIN Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGSTINNumber" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="TDS Amount"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    MaxLength="11" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblNarration" runat="server" Text="Narration" SkinID="LabelNormal"></asp:Label>
                                <span id="Span5" runat="server" class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNarration" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        CHEQUE/DRAFT DETAILS</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChartOfAccount" runat="server" Text="Chart of Account" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="True"></asp:TextBox>
                                <uc1:chartaccount runat="server" defaultbranch="true" id="BankAccNo"
                                    onsearchimageclicked="ucChartAccount_SearchImageClicked" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Mode Of Payment"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlModeOfPayment" runat="server" SkinID="DropDownListNormal">
                                    <asp:ListItem Text="Cash" Value="CA"></asp:ListItem>
                                    <asp:ListItem Text="Cheque / Draft" Value="CH"></asp:ListItem>
                                    <asp:ListItem Text="NEFT / RTGS" Value="DR"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Cheque Number"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeDate" onblur="return CheckChequeDate(this.id, true,'Cheque Date');"
                                    runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtChequeDate" OnClientShown="CheckToday" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                    onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="divItemDetails" runat="server">
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS</div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table runat="server" id="tblBalanceAmount">
                            <tr>
                                <td class="subFormTitle">
                                    <asp:Label ID="Label23" runat="server" Text="Total Balance Amount : "></asp:Label>
                                    <asp:TextBox CssClass="subFormTitle" ID="txtTotalBalanceAmount" ReadOnly="true" runat="server"
                                        Text=""></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="gridViewScrollFullPage">
                            <%--<div>
                                <asp:CheckBox ID="ChkHeader" Text="Select All" CssClass="labelSubTitle" runat="server" />
                                AutoPostBack="true" OnCheckedChanged="ChkHeader_OnCheckedChanged" />
                            </div>--%>
                            <asp:GridView ID="grvTDSPaymentDetails" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" SkinID="GridViewTransaction" OnRowDataBound="grvTDSPaymentDetails_RowDataBound">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Records Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelected" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSelected_OnCheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'
                                                Style="width: 30px !important;" Enabled="false" SkinID="GridViewTextBoxSmall"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Number">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBookingNumber" runat="server" ReadOnly="true" Text='<%# Bind("DocumentNumber") %>'
                                                SkinID="TextBoxDisabled" style="width:110px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBookingDate" runat="server" ReadOnly="true" Text='<%# Bind("DocumentDate") %>'
                                                SkinID="TextBoxDisabled" style="width:75px"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. Number">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReferenceNumber" runat="server" ReadOnly="true" Text='<%# Bind("InvoiceNumber") %>'
                                                SkinID="TextBoxDisabled"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReferenceDate" runat="server" ReadOnly="true" Text='<%# Bind("InvoiceDate") %>'
                                                SkinID="TextBoxDisabled" style="width:70px"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBookingAmount" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                Text='<%# Bind("InvoiceValue") %>' style="width:100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TDS Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTDSAmount" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                Text='<%# Bind("TDSAmount") %>' style="width:80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPaidAmount" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                Text='<%# Bind("PaidAmount") %>' style="width:80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPaymentAmount" runat="server" SkinID="TextBoxNormal" Text='<%# Bind("PaymentAmount") %>'
                                                onkeypress="return CurrencyNumberOnly();" style="width:110px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" ReadOnly="true" runat="server" SkinID="GridViewTextBoxBig"
                                                Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
            <input id="hdnRowCnt" type="hidden" runat="server" />
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
            <asp:AsyncPostBackTrigger ControlID="ddlVendorCode" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Vendor" runat="server" SelectMethod="GetAllVendors"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
