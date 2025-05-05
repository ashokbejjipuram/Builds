<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DebitCreditNoteSupplier.aspx.cs" Inherits="IMPALWeb.Transactions.Finance.General_Ledger.DebitCreditNoteSupplier" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="cntDebitCreditNoteSupplier" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/DebitCreditNoteSupplier.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divDebitCreditNoteSupplier" runat="server">
                    <div class="subFormTitle subFormTitleExtender300">
                        DEBIT/CREDIT NOTE - Supplier
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDocumentNumber" MaxLength="5" onkeypress="return IntegerValueOnly()"
                                    runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlDocumentNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlDocumentNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgButtonQuery" ImageUrl="~/Images/ifind.png" runat="server"
                                    SkinID="ImageButtonSearch" OnClick="imgButtonQuery_Click" />
                            </td>
                            <asp:Panel ID="pnldocument" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblAccountPeriod" runat="server" Text="Accounting period" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlAccountPeriod" OnSelectedIndexChanged="ddlAccountPeriod_SelectedIndexChanged"
                                        runat="server" AutoPostBack="True" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAccountPeriod" MaxLength="5" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblDocumentDate" runat="server" Text="Document date" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDocumentDate" runat="server" SkinID="TextBoxDisabled" onchange="return CheckTransDate(this.id)"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calTransactionDate" runat="server" EnableViewState="true"
                                        Format="dd/MM/yyyy" TargetControlID="txtDocumentDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </asp:Panel>
                        </tr>
                        <asp:Panel ID="pnldebitCredit" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBranchCode" runat="server" Text="Branch code" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBranchCode" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblDebitCreditNote" runat="server" Text="Debit/Credit Note" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlDebitCreditNote" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDebitCreditNote_SelectedIndexChanged"
                                        SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                </td>
                                <td class="inputcontrols" style="display: none">
                                    <asp:TextBox ID="txtSupplierInd" ReadOnly="true" Text="Supplier" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblSuppCustBranch" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label26" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSupplyPlant" runat="server" SkinID="DropDownListNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplyPlant_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <input id="hdnOsLsIndicator" type="hidden" runat="server" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblTransactionType" runat="server" Text="Transaction type" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblReferenceDocNUmber" runat="server" Text="Reference document number"
                                        SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtReferenceDocNumber" onblur="GetDocumentDate();" runat="server"
                                        SkinID="TextBoxNormal"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRefDocumnetDate" runat="server" Text="Reference document date"
                                        SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRefDocumnetDate" onblur="return CheckValidDate(this.id, true,'Reference document date');"
                                        runat="server" Enabled="true" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceRequDocDate" Format="dd/MM/yyyy" runat="server"
                                        TargetControlID="txtRefDocumnetDate" />
                                </td>

                                <td class="label">
                                    <asp:Label ID="lblValue" runat="server" Text="TurnOver Value" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtValue" onkeypress="return CurrencyNumberOnly();" runat="server"
                                        SkinID="TextBoxNormal" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="GST Value" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtGSTValue" onkeypress="return CurrencyNumberOnly();" runat="server"
                                        SkinID="TextBoxNormal" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            TRANSACTION DETAILS
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="div1">
                                    <div class="gridViewScrollFullPage">
                                        <asp:GridView ID="grdview" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                            ShowFooter="false" SkinID="GridViewScroll" PageSize="1500" OnRowDataBound="grdview_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="Serial_Number" HeaderText="Sr.No" />
                                                <asp:BoundField DataField="Chart_of_Account_Code" HeaderText="ChartAcc Code" />
                                                <asp:BoundField DataField="Amount" HeaderText="Value" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                <asp:BoundField DataField="SGST_Code" HeaderText="SGSTCode" />
                                                <asp:BoundField DataField="SGST_Per" HeaderText="SGSTPer" />
                                                <asp:BoundField DataField="SGST_Amt" HeaderText="SGSTAmt" />
                                                <asp:BoundField DataField="CGST_Code" HeaderText="CGSTCode" />
                                                <asp:BoundField DataField="CGST_Per" HeaderText="CGSTPer" />
                                                <asp:BoundField DataField="CGST_Amt" HeaderText="CGSTAmt" />
                                                <asp:BoundField DataField="IGST_Code" HeaderText="IGSTCode" />
                                                <asp:BoundField DataField="IGST_Per" HeaderText="IGSTPer" />
                                                <asp:BoundField DataField="IGST_Amt" HeaderText="IGSTAmt" />
                                                <asp:BoundField DataField="UTGST_Code" HeaderText="UTGSTCode" />
                                                <asp:BoundField DataField="UTGST_Per" HeaderText="UTGSTPer" />
                                                <asp:BoundField DataField="UTGST_Amt" HeaderText="UTGSTAmt" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="div2">
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grdCA" runat="server" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="False"
                                        SkinID="GridViewScroll" PageSize="1500" OnRowCreated="grdCA_RowCreated" OnRowDeleting="grdCA_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chart Of Account">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGrdChartAccount" SkinID="GridViewTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                                                    <User:ChartAccount ID="UserChartAccount" DefaultBranch="true" runat="server" OnSearchImageClicked="DebitCreditAdvice_SearchImageClicked" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnadd" Text="Add Row" OnClientClick="return checkAmount(1);" SkinID="GridViewButtonFooter"
                                                        runat="server" OnClick="btnadd_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGrdValue" SkinID="GridViewTextBox" onkeypress="return CurrencyNumberOnly();"
                                                        runat="server" AutoPostBack="true" OnTextChanged="txtGrdValue_TextChanged" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGrdRemarks" SkinID="GridViewTextBox" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST Code">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                        Width="60" OnSelectedIndexChanged="ddlSGSTCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST %">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSGSTPer" Text='<%# Bind("SGSTPer") %>' onkeypress="return IntegerValueOnly();"
                                                        SkinID="GridViewTextBoxSmall" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSGSTAmt" Text='<%# Bind("SGSTAmt") %>' onkeypress="return IntegerValueWithDot();"
                                                        SkinID="GridViewTextBoxSmall" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UTGST Code">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlUTGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                        Width="60" OnSelectedIndexChanged="ddlUTGSTCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UTGST %">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUTGSTPer" Text='<%# Bind("UTGSTPer") %>' onkeypress="return IntegerValueOnly();"
                                                        SkinID="GridViewTextBoxSmall" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UTGST Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUTGSTAmt" Text='<%# Bind("UTGSTAmt") %>' onkeypress="return IntegerValueWithDot();"
                                                        SkinID="GridViewTextBoxSmall" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST Code">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlCGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                        Width="60" OnSelectedIndexChanged="ddlCGSTCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST %">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCGSTPer" Text='<%# Bind("CGSTPer") %>' onkeypress="return IntegerValueOnly();"
                                                        SkinID="GridViewTextBoxSmall" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCGSTAmt" Text='<%# Bind("CGSTAmt") %>' onkeypress="return IntegerValueWithDot();"
                                                        SkinID="GridViewTextBoxSmall" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST Code">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlIGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                        Width="60" OnSelectedIndexChanged="ddlIGSTCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST %">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIGSTPer" Text='<%# Bind("IGSTPer") %>' onkeypress="return IntegerValueOnly();"
                                                        SkinID="GridViewTextBoxSmall" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIGSTAmt" Text='<%# Bind("IGSTAmt") %>' onkeypress="return IntegerValueWithDot();"
                                                        SkinID="GridViewTextBoxSmall" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
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
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnDetails" runat="server" Text="Item Details" OnClientClick="return ValidateDebitCreditFields();"
                                SkinID="ButtonNormal" OnClick="btnDetails_Click" />
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClientClick="return checkAmount(2);"
                                SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="hdnChart" type="hidden" runat="server" />
                    <input id="hdnTransType" type="hidden" runat="server" />
                    <input id="hdnpath" type="hidden" runat="server" />
                    <input id="hdnRoundoff" type="hidden" runat="server" />
                    <input id="hdnDebitCreditNote" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
