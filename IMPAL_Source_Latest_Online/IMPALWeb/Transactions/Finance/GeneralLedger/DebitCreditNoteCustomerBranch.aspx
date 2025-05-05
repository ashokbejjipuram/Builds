<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DebitCreditNoteCustomerBranch.aspx.cs" Inherits="IMPALWeb.Transactions.Finance.General_Ledger.DebitCreditNoteCustomerBranch" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="cntDebitCreditNoteCustBranch" ContentPlaceHolderID="CPHDetails"
    runat="server">

    <script src="../../../Javascript/DebitCreditNoteCustomerBranch.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divDebitCreditAdvice" runat="server">
                    <div class="subFormTitle subFormTitleExtender300">
                        DEBIT/CREDIT Note - Customer/Branch
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDocumentNumber" MaxLength="5" onkeypress="return IntegerValueOnly()"
                                    runat="server" SkinID="TextBoxNormal" Enabled="false" OnTextChanged="txtDocumentNumber_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
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
                                    <input id="hdnBranchCode" type="hidden" runat="server" />
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
                                <td class="label">
                                    <asp:Label ID="lblSuppCustBranchInd" runat="server" Text="Customer / Branch indicator"
                                        SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSuppCustBranchInd" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlSuppCustBranchInd_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSuppCustBranch" runat="server" Text="Customer / Branch" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSuppCustBranch" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlSuppCustBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <input id="hdninterStateStatus" type="hidden" runat="server" />
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
                                    <%--<asp:ImageButton ID="imgRequDocDate" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
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
                    <asp:FormView ID="FormDetailCustomer" DefaultMode="ReadOnly" runat="server">
                        <ItemTemplate>
                            <div id="divItemDetails" runat="server">
                                <div class="subFormTitle">
                                    CUSTOMER INFORMATION
                                </div>
                                <table class="subFormTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("Customer_Code") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblAddress1" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Eval("address1") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblAddress2" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress2" runat="server" Text='<%# Eval("address2") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress3" runat="server" SkinID="LabelNormal" Text="Address3"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress3" runat="server" Text='<%# Eval("address3") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblAddress4" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress4" runat="server" Text='<%# Eval("address4") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblLocation" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtLocation" runat="server" Text='<%# Eval("Location") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <td class="label">
                                        <asp:Label ID="lblGSTINNo" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtGSTINNo" runat="server" Text='<%# Eval("GSTIN") %>' SkinID="TextBoxDisabled"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                    <div class="subFormTitle">
                        Item Details
                    </div>
                    <asp:Panel ID="pnlView" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div class="gridViewScrollFullPage">
                                        <asp:GridView ID="grdview" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                            ShowFooter="false" SkinID="GridViewScroll" PageSize="1500" OnRowDataBound="grdview_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="Serial_Number" HeaderText="Sr.No" />
                                                <asp:BoundField DataField="Item_Code" HeaderText="ItemCode" />
                                                <asp:BoundField DataField="Return_Document_Quantity" HeaderText="Rtn Qnt" />
                                                <asp:BoundField DataField="Return_Actual_Quantity" HeaderText="RtnActual Qnt" />
                                                <asp:BoundField DataField="Value" HeaderText="Value" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                <asp:BoundField DataField="Chart_OF_Account_Code" HeaderText="ChartAcc Code" />
                                                <asp:BoundField DataField="SGST_Code" HeaderText="SGSTCode" />
                                                <asp:BoundField DataField="SGST_Percent" HeaderText="SGSTPer" />
                                                <asp:BoundField DataField="SGST_Amount" HeaderText="SGSTAmt" />
                                                <asp:BoundField DataField="CGST_Code" HeaderText="CGSTCode" />
                                                <asp:BoundField DataField="CGST_Percent" HeaderText="CGSTPer" />
                                                <asp:BoundField DataField="CGST_Amount" HeaderText="CGSTAmt" />
                                                <asp:BoundField DataField="IGST_Code" HeaderText="IGSTCode" />
                                                <asp:BoundField DataField="IGST_Percent" HeaderText="IGSTPer" />
                                                <asp:BoundField DataField="IGST_Amount" HeaderText="IGSTAmt" />
                                                <asp:BoundField DataField="UTGST_Code" HeaderText="UTGSTCode" />
                                                <asp:BoundField DataField="UTGST_Percent" HeaderText="UTGSTPer" />
                                                <asp:BoundField DataField="UTGST_Amount" HeaderText="UTGSTAmt" />
                                                <asp:BoundField DataField="Part_Number" HeaderText="PartNumber" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCA" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div class="gridViewScrollFullPage">
                                        <asp:GridView ID="grdCA" runat="server" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="False"
                                            SkinID="GridViewScroll" PageSize="1500" OnRowCreated="grdCA_RowCreated">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNumber" Text='<%# Bind("RowNumber") %>' runat="server"> </asp:Label>
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
                                                        <asp:TextBox ID="txtGrdValue" SkinID="GridViewTextBox" onkeypress="return CurrencyNumberOnly();" runat="server" AutoPostBack="true"
                                                            OnTextChanged="txtGrdValue_TextChanged" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGrdRemarks" SkinID="GridViewTextBox" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item code">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemCode" SkinID="GridViewTextBox" ReadOnly="true" runat="server"></asp:TextBox>
                                                        <User:ItemCodePartNumber ID="UserPartNumber" Mode="3" SupplierType="1" Disable="false"
                                                            runat="server" OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturnQuantity" Text="0" SkinID="GridViewTextBoxSmall" onkeypress="return IntegerValueOnly();"
                                                            runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SGST Code">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlSGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList" Width="60"
                                                            OnSelectedIndexChanged="ddlSGSTCode_SelectedIndexChanged">
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
                                                        <asp:DropDownList ID="ddlUTGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList" Width="60"
                                                            OnSelectedIndexChanged="ddlUTGSTCode_SelectedIndexChanged">
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
                                                        <asp:DropDownList ID="ddlCGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList" Width="60"
                                                            OnSelectedIndexChanged="ddlCGSTCode_SelectedIndexChanged">
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
                                                        <asp:DropDownList ID="ddlIGSTCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList" Width="60"
                                                            OnSelectedIndexChanged="ddlIGSTCode_SelectedIndexChanged">
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
                                                <asp:TemplateField HeaderText="Supp. Part #">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPartNumber" ReadOnly="true" SkinID="GridViewTextBoxHidden" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlDA" runat="server">
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lbltotalAmt" SkinID="LabelNormal" Text="Total Value" runat="server"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtTotalAmount" SkinID="TextBoxDisabled" runat="server" ReadOnly></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCollectAmount" SkinID="LabelNormal" Text="Collected Amount" runat="server"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCollectedAmount" SkinID="TextBoxDisabled" runat="server" ReadOnly></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblChartAccount" SkinID="LabelNormal" Text="Chart Of Account" runat="server"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChartOtAccount" SkinID="TextBoxDisabled" Width="150px" runat="server"
                                        ReadOnly></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <div class="gridViewScrollFullPage">
                                        <asp:GridView ID="grdDA" SkinID="GridViewScroll" runat="server" AllowPaging="true"
                                            ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="grdDA_RowDataBound">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelected" onClick="SelectedChange(this)" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part number">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPartNumber" Width="90px" Text='<%# Bind("supplier_part_number") %>'
                                                            SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                                        <asp:HiddenField ID="txtSerialNumber" Value='<%# Bind("Serial_Number") %>'
                                                            runat="server"></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Text='<%# Bind("Item_Quantity","{0:0.0000}") %>' SkinID="TextBoxDisabledSmall"
                                                            ReadOnly="true" onkeypress="return IntegerValueOnly();" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGrdValue" Width="90px" Text='<%# Bind("value","{0:0.0000}") %>'
                                                            SkinID="TextBoxDisabled" ReadOnly="true" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturnquantity" onblur="CalculateTax(this,1);" onkeypress="return IntegerValueOnly();"
                                                            Text='<%# Bind("return_quantity","{0:0.0000}") %>' SkinID="GridViewTextBoxSmall"
                                                            runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return value">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturnvalue" Width="90px" Text="0" onblur="CalculateTax(this,2);"
                                                            onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBox" runat="server"
                                                            Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SGST">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSGST" Text='<%# Bind("SGSTSalesPer","{0:0.0000}") %>' Width="60px"
                                                            onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBoxSmall" onblur="CalAdjustValue(this);"
                                                            runat="server" Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CGST">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCGST" Text='<%# Bind("CGSTSalesPer","{0:0.0000}") %>' Width="60px"
                                                            onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBoxSmall" onblur="CalAdjustValue(this);"
                                                            runat="server" Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IGST">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIGST" Text='<%# Bind("IGSTSalesPer","{0:0.0000}") %>' Width="60px"
                                                            onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBoxSmall" onblur="CalAdjustValue(this);"
                                                            runat="server" Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UTGST">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUTGST" Text='<%# Bind("UTGSTSalesPer","{0:0.0000}") %>' Width="60px"
                                                            onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBoxSmall" onblur="CalAdjustValue(this);"
                                                            runat="server" Enabled="false" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                        <asp:HiddenField ID="txtActualQuantity" Value='<%# Bind("return_quantity","{0:0.0000}") %>'
                                                            runat="server"></asp:HiddenField>
                                                        <asp:HiddenField ID="txtCouponCharges" Value='<%# Bind("CouponCharges","{0:0.0000}") %>'
                                                            runat="server"></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnItemDetails" runat="server" Text="Item Details" OnClientClick="return ValidateDebitCreditFields();"
                                SkinID="ButtonNormal" OnClick="btnItemDetails_Click" />
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClientClick="return checkAmount(2);"
                                SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="hdnChart" type="hidden" runat="server" />
                    <input id="hdnTransType" type="hidden" runat="server" />
                    <input id="hdnpath" type="hidden" runat="server" />
                    <input id="hddItemCode" type="hidden" runat="server" />
                    <input id="hddrefdate" type="hidden" runat="server" />
                    <input id="hddrefStatus" type="hidden" runat="server" />
                    <input id="hddreturnQntCA" type="hidden" runat="server" />
                    <input id="hdntxtRoundoff" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
