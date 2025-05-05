<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="VendorBooking.aspx.cs"
    Inherits="IMPALWeb.Finance.VendorBooking" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/VendorBooking.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div class="subFormTitle">
                    VENDOR BOOKING
                </div>
                <table class="subFormTable">
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
                            <asp:TextBox ID="txtDocumentDate" runat="server" SkinID="TextBoxDisabled" onchange="return CheckTransDate(this.id)"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calTransactionDate" runat="server" EnableViewState="true"
                                Format="dd/MM/yyyy" TargetControlID="txtDocumentDate">
                            </ajaxToolkit:CalendarExtender>
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
                            <asp:Label ID="lblReferenceDocumentNumber" runat="server" Text="Reference Document Number"
                                SkinID="LabelNormal"></asp:Label><span id="Span3" runat="server" class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtReferenceDocumentNumber" runat="server" SkinID="TextBoxNormal"
                                MaxLength="25" OnTextChanged="txtReferenceDocumentNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceDocumentDate" runat="server" Text="Reference Document Date"
                                SkinID="LabelNormal"></asp:Label><span id="Span4" runat="server" class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtReferenceDocumentDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                onblur="return checkDateForRefDocDate(this.id);"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceRequDocDate" PopupButtonID="imgRequDocDate" Format="dd/MM/yyyy"
                                runat="server" TargetControlID="txtReferenceDocumentDate" />
                        </td>
                        <td class="label">
                            <asp:Label ID="lblInvoiceAmount" runat="server" SkinID="LabelNormal" Text="Invoice Amount" onchange="javascript:return fnCalculateTDS();"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                MaxLength="11" onkeypress="return CurrencyNumberOnly();" onchange="javascript:return fnCalculateTDS();"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="GST Amount"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtGSTAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                MaxLength="11" onkeypress="return CurrencyNumberOnly();" onchange="javascript:return fnCalculateTDS();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTDStype" runat="server" Text="TDS Type" SkinID="LabelNormal"></asp:Label>
                            <span id="Span2" runat="server" class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlTDStype" runat="server" SkinID="DropDownListNormalBig" onchange="javascript:return fnCalculateTDS();">
                            </asp:DropDownList>
                            <input id="hdnTDStype" type="hidden" runat="server" />
                        </td>
                        <td class="label">
                            <asp:Label ID="lblTDSAmount" runat="server" SkinID="LabelNormal" Text="TDS Amount"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtTDSAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false" MaxLength="11" 
                                onpaste="return false;" ondragstart="return false;" ondrop="return false;" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="lblNarration" runat="server" Text="Narration" SkinID="LabelNormal"></asp:Label>
                            <span id="Span5" runat="server" class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtNarration" runat="server" SkinID="TextBoxMultilineNormalFiveColBig"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="label" style="display:none">
                            <asp:Label ID="lblRCMStatus" runat="server" Text="RCM Status" SkinID="LabelNormal"></asp:Label>
                            <span id="Span1" runat="server" class="asterix">*</span>
                        </td>
                        <td class="inputcontrols" style="display:none">
                            <asp:DropDownList ID="ddlRCMStatus" runat="server" SkinID="DropDownListNormal">
                                <%--<asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>--%>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
					<tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Payment Branch"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlPaymentBranch" runat="server" SkinID="DropDownListNormal" DataTextField="BranchName" DataValueField="BranchCode">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Payment Due Date" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtPaymentDueDate" runat="server" SkinID="TextBoxNormal" onchange="return CheckPaymentDueDate(this.id)"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calPaymentDueDate" runat="server" EnableViewState="true"
                                Format="dd/MM/yyyy" TargetControlID="txtPaymentDueDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divVendorInfo" style="display: none" runat="server">
                <div class="reportFormTitle">
                    Vendor Information
                </div>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVendorCode" runat="server" SkinID="LabelNormal" Text="Vendor Code"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtVendorCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="lblVendorName" runat="server" Text="Vendor Name" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtVendorName" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Address 1"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address 2"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Address 3"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="label">
                            <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Address 4"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVendorLocation" runat="server" Text="Location" SkinID="LabelNormal"></asp:Label>
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
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Phone" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>        
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS
                </div>
                <div id="idGrid" runat="server">
                    <asp:GridView ID="grvVendorBookingDetails" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" SkinID="GridViewTransaction" OnRowDeleting="grvVendorBookingDetails_OnRowDeleting" OnRowCreated="grvVendorBookingDetails_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'
                                        Style="width: 30px !important;" Enabled="false" SkinID="GridViewTextBoxSmall"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Row" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chart Of Account">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChartOfAccount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        Enabled="false" Width="150px"> </asp:TextBox>
                                    <uc1:ChartAccount runat="server" DefaultBranch="true" ID="ChartAccount1" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
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
                                        Style="width: 100px !important;" Text='<%# Bind("Remarks") %>' TabIndex="0"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Style="width: 80px !important;" SkinID="GridviewTextBox"
                                        Text='<%# Bind("Amount") %>' onkeypress="return CurrencyNumberOnly();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                        onkeyup="return CurrencyDecimalOnly(this.id, event);" onblur="return CurrencyRoundOffOnly(this.id, event);"
                                        Enabled="false" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
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
                                    <asp:TextBox ID="txtSGSTPer" Text='<%# Bind("SGST_Per") %>' onkeypress="return IntegerValueOnly();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SGST Amt">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSGSTAmt" Text='<%# Bind("SGST_Amt") %>' onkeypress="return IntegerValueWithDot();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
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
                                    <asp:TextBox ID="txtUTGSTPer" Text='<%# Bind("UTGST_Per") %>' onkeypress="return IntegerValueOnly();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UTGST Amt">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUTGSTAmt" Text='<%# Bind("UTGST_Amt") %>' onkeypress="return IntegerValueWithDot();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
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
                                    <asp:TextBox ID="txtCGSTPer" Text='<%# Bind("CGST_Per") %>' onkeypress="return IntegerValueOnly();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGST Amt">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCGSTAmt" Text='<%# Bind("CGST_Amt") %>' onkeypress="return IntegerValueWithDot();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
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
                                    <asp:TextBox ID="txtIGSTPer" Text='<%# Bind("IGST_Per") %>' onkeypress="return IntegerValueOnly();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IGST Amt">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIGSTAmt" Text='<%# Bind("IGST_Amt") %>' onkeypress="return IntegerValueWithDot();"
                                        SkinID="GridViewTextBox" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                <input id="hdnRowCnt" type="hidden" runat="server" />
                <input id="hdnTotalAmount" type="hidden" runat="server" />
                <input id="hdnTotalTaxAmount" type="hidden" runat="server" />
                <input id="hdnOSLSstatus" type="hidden" runat="server" />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal" CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false" SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDocumentNumber" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Vendor" runat="server" SelectMethod="GetAllVendors"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
