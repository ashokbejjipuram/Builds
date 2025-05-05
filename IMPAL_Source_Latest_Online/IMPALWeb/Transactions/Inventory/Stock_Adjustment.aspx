<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Stock_Adjustment.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.Stock_Adjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="PartNoDetails" Src="~/UserControls/ItemCodePartNumber.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <script src="../../Javascript/DirectPOcommon.js" type="text/javascript"></script>
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    STOCK ADJUSTMENT</div>
                <table class="subFormTable">
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblTagnumber" runat="server" SkinID="LabelNormal" Text="Tag Number"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtTagnumber" TabIndex="1" SkinID="TextBoxNormalBig" onkeypress="return IntegerValueOnlyWithSearch();"
                                runat="server" MaxLength="5" onclick="return IntegerValueOnly()"></asp:TextBox>
                            <asp:DropDownList ID="ddlTagnumber" TabIndex="2" runat="server" AutoPostBack="True"
                                SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlTagnumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:ImageButton ID="ImgButtonQuery" runat="server" alt="Query" CausesValidation="true"
                                ImageUrl="~/images/ifind.png" OnClick="ImgButtonQuery_Click" OnClientClick="return funValidateTagNumber();"
                                SkinID="ImageButtonSearch" TabIndex="3" ValidationGroup="btnSearch" />
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblTagdate" runat="server" SkinID="LabelNormal" Text="Tag Date"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtTagDate" ReadOnly="true" SkinID="TextBoxDisabledBig" TabIndex="-1"
                                runat="server" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblItemcode" SkinID="LabelNormal" runat="server" Text="Item Code"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtItemcode" SkinID="TextBoxDisabledBig" ReadOnly="true" TabIndex="-1"
                                runat="server" MaxLength="20"></asp:TextBox>
                            <uc1:PartNoDetails runat="server" ID="UCPartDeails" Mode="4" Disable="True" />
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblComputerBalance" SkinID="LabelNormal" runat="server" Text="Computer Balance"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtComputerBalance" SkinID="TextBoxDisabledBig" ReadOnly="true"
                                TabIndex="-1" runat="server" MaxLength="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblEntrydate" SkinID="LabelNormal" runat="server" Text="Entry Date"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtEntrydate" SkinID="TextBoxDisabledBig" ReadOnly="true" TabIndex="6"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblPhysicalVerificationBy" SkinID="LabelNormal" runat="server" Text="Physical Verification By"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtPhysicalVerificationBy" SkinID="TextBoxNormalBig" TabIndex="4"
                                runat="server" MaxLength="40"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblPhysicalBalanceDate" TabIndex="8" SkinID="LabelNormal" runat="server"
                                Text="Physical Balance Date"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtPhysicalBalanceDate" onmousedown="TriggerCalender('ImgPhysicalBalanceDate');"
                                contentEditable="false" SkinID="TextBoxNormalBig" TabIndex="5" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImgPhysicalBalanceDate" runat="server" alt="Calendar" Height="18"
                                ImageUrl="~/Images/Calendar.png" TabIndex="6" Width="18" />
                            <ajaxToolkit:CalendarExtender ID="PhysicalBalanceDateExtender" runat="server" EnableViewState="true"
                                Format="dd/MM/yyyy" PopupButtonID="ImgPhysicalBalanceDate" PopupPosition="BottomLeft"
                                TargetControlID="txtPhysicalBalanceDate" OnClientDateSelectionChanged="CheckValidFutureDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblApprovedBy" SkinID="LabelNormal" runat="server" Text="Approved By"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtApprovedBy" SkinID="TextBoxNormalBig" TabIndex="7" runat="server"
                                MaxLength="40"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblPhysicalBalance" SkinID="LabelNormal" runat="server" Text="Physical Balance"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtPhysicalBalance" TabIndex="8" onkeypress="return IntegerValueOnly();"
                                SkinID="TextBoxNormalBig" runat="server" MaxLength="5"></asp:TextBox>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblRemarks" SkinID="LabelNormal" runat="server" Text="Remarks"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:DropDownList ID="ddlRemarks" SkinID="DropDownListNormalBig" TabIndex="13" runat="server"
                                AutoPostBack="True">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="Short Supply" Text="Short Supply"></asp:ListItem>
                                <asp:ListItem Value="Excess Supply" Text="Excess Supply"></asp:ListItem>
                                <asp:ListItem Value="Inventory discrepancy" Text="Inventory discrepancy"></asp:ListItem>
                                <asp:ListItem Value="Warranty claims" Text="Warranty claims"></asp:ListItem>
                                <asp:ListItem Value="FOC" Text="FOC"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblOS_LS_Indicator" SkinID="LabelNormal" runat="server" Text="OS/LS Indicator"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:DropDownList ID="ddlOS_LS_Indicator" Enabled="false" TabIndex="14" runat="server"
                                SkinID="DropDownListNormalBig">
                                <asp:ListItem Value="O" Text="OS"></asp:ListItem>
                                <asp:ListItem Value="L" Text="LS"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblSuppPartNo" SkinID="LabelNormal" runat="server" Text="Supp. Part #"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtSuppPartNo" TabIndex="15" ReadOnly="true" SkinID="TextBoxDisabledBig"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblLocation" SkinID="LabelNormal" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtLocation" TabIndex="9" SkinID="TextBoxNormalBig" runat="server"
                                MaxLength="12"></asp:TextBox>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblWHRefNo_Reason" SkinID="LabelNormal" runat="server" Text="WH Ref.No./Reason"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtWHRefNo_Reason" TabIndex="10" SkinID="TextBoxNormalBig" runat="server"
                                MaxLength="100"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblInvoiceNumber" SkinID="LabelNormal" runat="server" Text="Invoice Number"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtInvoiceNumber" TabIndex="9" SkinID="TextBoxNormalBig" runat="server"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="labelColSpan2">
                            <asp:Label ID="lblInvoiceDate" SkinID="LabelNormal" runat="server" Text="Invoice Date"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtInvoiceDate" TabIndex="10" SkinID="TextBoxNormalBig" runat="server"
                                MaxLength="100"></asp:TextBox>
                            <asp:ImageButton ID="ImgInvoiceDate" runat="server" alt="Calendar" Height="18"
                                ImageUrl="~/Images/Calendar.png" TabIndex="6" Width="18" />
                            <ajaxToolkit:CalendarExtender ID="InvoiceDateExtender" runat="server" EnableViewState="true"
                                Format="dd/MM/yyyy" PopupButtonID="ImgInvoiceDate" PopupPosition="BottomLeft"
                                TargetControlID="txtInvoiceDate" OnClientDateSelectionChanged="CheckValidFutureDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:HiddenField ID="HdnBranchCode" runat="server" />
                        </td>
                    </tr>
                    <td class="labelColSpan2" colspan="2">
                        <asp:Label ID="lblError" runat="server" SkinID="LabelNormal" ForeColor="Red"></asp:Label>
                    </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" TabIndex="11" runat="server" CausesValidation="false"
                            SkinID="ButtonNormalBig" Text="Submit" OnClientClick="return funStockAdjustmentBtnSubmit();"
                            OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" TabIndex="12" runat="server" CausesValidation="false" SkinID="ButtonNormalBig"
                            Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
                <input id="hdnAccountingPeriod" type="hidden" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
