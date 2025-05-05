<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ReceiptsCancellation.aspx.cs"
    Inherits="IMPALWeb.ReceiptsCancellation" EnableEventValidation="false" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/ReceivableReceiptsCancellation.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlReceiptNumber" />
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle">
                            RECEIPTS CANCELLATION
                        </div>
                        <table id="reportFiltersTable" class="subFormTable" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch"
                                        DataTextField="BranchName" SkinID="DropDownListNormal" DataValueField="BranchCode">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Receipt Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtReceiptNumber" runat="server" SkinID="TextBoxNormal" ReadOnly="false" MaxLength="14"></asp:TextBox>
                                    <asp:DropDownList ID="ddlReceiptNumber" runat="server" AutoPostBack="true" DataTextField="ItemDesc" DataValueField="ItemCode" 
                                        OnSelectedIndexChanged="ddlReceiptNumber_OnSelectedIndexChanged" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgEditToggle" ImageUrl="../../../images/ifind.png" OnClick="imgEditToggle_Click"
                                        SkinID="ImageButtonSearch" runat="server" OnClientClick="return ValidateDocNo()" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Receipt Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtReceiptDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Accounting Period"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAccountPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Customer"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" runat="server" SkinID="DropDownListDisabledBig"
                                        DataSourceID="ODS_Customer" DataTextField="Customer_Name" DataValueField="Customer_Code">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Mode Of Receipt"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlModeOfReceipt" runat="server" SkinID="DropDownListDisabled">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Amount"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxDisabled" contentEditable="true"
                                        MaxLength="11" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Temporary Recpt. Number"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtTempRecptNumber" runat="server" SkinID="TextBoxDisabled" contentEditable="true"
                                        MaxLength="10" onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                </td>
                                <td class="label" style="display: none">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="Temporary Recpt. Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols" style="display: none">
                                    <asp:TextBox ID="txtTempRecptDate" onblur="return CheckTempRecptDate(this.id, true,'Temporary Recpt. Date');"
                                        runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="Reason to Cancel"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols" colspan="3">
                                    <asp:DropDownList ID="ddlReason" runat="server" SkinID="DropDownListNormal">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Wrong OS / Aging Adjusted" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Wrong Dealer Adjusted" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="tblNEFTremarks" class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label21" runat="server" SkinID="LabelNormal" Text="NEFT Details"></asp:Label>
                                </td>
                                <td class="inputcontrols" colspan="20">
                                    <asp:TextBox ID="txtNEFTremarks" runat="server" SkinID="TextBoxDisabledBig" contentEditable="true" TextMode="MultiLine" Height="80px" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CHEQUE/DRAFT DETAILS
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Cheque Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxDisabled" contentEditable="true"
                                        onkeypress="return AlphaNumericOnly();" MaxLength="6"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeDate" onblur="return CheckChequeDate(this.id, true,'Cheque Date');"
                                        runat="server" SkinID="TextBoxCalendarExtenderDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxDisabled" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxDisabled" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Local / Out Station"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols" colspan="3">
                                    <asp:DropDownList ID="ddlLocalOrOutStation" runat="server" SkinID="DropDownListDisabled">
                                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Out Station" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="subFormTitle">
                        CUSTOMER INFORMATION
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCode" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Address 1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address 2"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Address 3"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Address 4"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            DOCUMENT DETAILS
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 100%; margin-bottom: 5px" runat="server" id="tblBalanceAmount">
                                    <tr>
                                        <td class="subFormTitle">
                                            <asp:Label ID="Label23" runat="server" Text="Total Balance Amount : "></asp:Label>
                                            <asp:TextBox CssClass="subFormTitle" ID="txtTotalBalanceAmount" ReadOnly="true" runat="server"
                                                Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="gridViewScrollFullPage">
                                    <div>
                                        <asp:CheckBox ID="ChkHeader" Text="Select All" CssClass="labelSubTitle" runat="server" />
                                    </div>
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" SkinID="GridViewScroll">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Records Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSelected" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceType" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("ReferenceType") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Document Number">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceDocNumber" runat="server" ReadOnly="true" Text='<%# Bind("ReferenceDocumentNumber") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Document Number1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceDocNumber1" runat="server" ReadOnly="true" Text='<%# Bind("ReferenceDocumentNumber1") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDocumentDate" runat="server" ReadOnly="true" Text='<%# Bind("DocumentDate") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document / Balance Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDocumentValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("DocumentValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collection Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCollectionAmount" CausesValidation="true" contentEditable="true"
                                                        runat="server" onkeypress="return CurrencyNumberOnly();" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("CollectionAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalanceAmount" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("BalanceAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Indicator">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlPaymentIndicator" runat="server" SkinID="DropDownListDisabled"
                                                        SelectedValue='<%# Bind ("PaymentIndicator") %>'>
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="CD DISPUTE" Value="CD DISPUTE"></asp:ListItem>
                                                        <asp:ListItem Text="WARRANTY" Value="WARRANTY"></asp:ListItem>
                                                        <asp:ListItem Text="TOD" Value="TOD"></asp:ListItem>
                                                        <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <input id="hdnAdvanceAmount" type="hidden" runat="server" />
                <input id="hdnAdvanceChequSlipNo" type="hidden" runat="server" />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnCancel" SkinID="ButtonNormalBig" runat="server" Text="Cancel Receipt" OnClick="BtnCancel_Click" Style="background-color: Red; color: whitesmoke; font-weight: bold; width: 100px" />
                        <asp:Button ID="btnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Customer" runat="server" SelectMethod="GetAllCustomersForReceipts"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
