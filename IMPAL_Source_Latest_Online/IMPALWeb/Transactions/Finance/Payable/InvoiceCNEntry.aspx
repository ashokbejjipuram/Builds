<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="InvoiceCNEntry.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.InvoiceCNEntry" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/FinanceInvoiceCN.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="subFormTitle">
                        INVOICE / CN ENTRY</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceDate" runat="server" Text="Ho REF. Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtHOREFNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                    DataValueField="BranchCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Invoice Number" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceNumber" runat="server" AutoPostBack="true" SkinID="TextBoxNormal" OnTextChanged="txtInvoiceNumber_TextChanged"
                                    onkeypress="return AlphaNumericOnly()" onBlur="return ChkScreenMode();"></asp:TextBox>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Invoice Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" OnTextChanged="txtInvoiceDate_TextChanged"
                                onblur="return checkInvoiceDate(this.id);"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceInvoiceDate" PopupButtonID="imgInvoiceDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtInvoiceDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Invoice Amount" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPaymentStatus" runat="server" Text="Payment Status" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlPaymentStatus" runat="server" SkinID="DropDownListNormal">
                                    <asp:ListItem Selected="True" Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="Stop Payment" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Partial Payment" Value="P"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDocumentDate" runat="server" Text="Indicator" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlIndicator" runat="server" SkinID="DropDownListNormal">
                                    <asp:ListItem Selected="True" Text="PO" Value="PO"></asp:ListItem>
                                    <asp:ListItem Text="CN" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="DA" Value="DA"></asp:ListItem>
                                    <asp:ListItem Text="CA" Value="CA"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="font-weight: bold"">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                        CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                        SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                </div>
            </div>
            <input id="hdnScreenMode" type="hidden" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersInvoiceCN"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranches"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
</asp:Content>
