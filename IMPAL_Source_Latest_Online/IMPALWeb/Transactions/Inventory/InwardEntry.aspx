<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="InwardEntry.aspx.cs"
    Inherits="IMPALWeb.InwardEntry" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/InwardAndGRN.js" type="text/javascript"></script>

    <script type="text/javascript">

        function funSetFocusOnQty(QtyID) {
            //alert(QtyID);
            //debugger;
            //alert(document.getElementById('div1').scrollLeft);
            //document.getElementById('div1').scrollLeft = 500;
            document.getElementById(QtyID).focus();
            return;
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlInwardNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                <asp:AsyncPostBackTrigger ControlID="ddlOSIndicator" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle">
                            MANUAL INWARD ENTRY
                        </div>
                        <asp:Panel ID="InwardPanel" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" DataSourceID="ODS_AllBranch"
                                            DataTextField="BranchName" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Inward Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInwardNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlInwardNumber" runat="server" AutoPostBack="true" DataSourceID="ODS_INWARDENTRY"
                                            DataTextField="ItemDesc" DataValueField="ItemCode" OnSelectedIndexChanged="ddlInwardNumber_OnSelectedIndexChanged"
                                            SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="ImageButtonSearch" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Inward Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInwardDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code" OnSelectedIndexChanged="ddlTransactionType_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                                            DataSourceID="ODS_Suppliers" DataTextField="SupplierName" DataValueField="SupplierCode" OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblSupplierGSTIN" runat="server" SkinID="LabelNormal" Text="Supplier State Code/GSTIN"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSupplierGSTIN" runat="server" SkinID="TextBoxNormal" MaxLength="15" onkeypress="return AlphaNumericOnlyGSTIN();"
                                            onpaste="return false;" AutoPostBack="true" OnTextChanged="txtSupplierGSTIN_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label26" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplyPlant" runat="server" SkinID="DropDownListNormal"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSupplyPlant_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2" style="vertical-align: top">
                                        <span style="font-family: @Arial Unicode MS; font-size: 13px; color: red; font-weight: bold;">(Refer Supplier Invoice to Enter State Code or GSTIN)</span>

                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle">
                                INVOICE
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label21" runat="server" SkinID="LabelNormal" Text="Invoice Number"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInvoiceNo" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericWithSlashDash();" MaxLength="16"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="Invoice Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return checkDateForInvoice(this.id, 1);"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalExtInvoiceDate" PopupButtonID="ImgInvoiceDate"
                                            runat="server" Format="dd/MM/yyyy" TargetControlID="txtInvoiceDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label23" runat="server" SkinID="LabelNormal" Text="Received Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtReceivedDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return checkDateForReceivedDate(this.id);"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalExtReceivedDate" PopupButtonID="ImgReceivedDate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtReceivedDate" OnClientShown="CheckToday" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label25" runat="server" SkinID="LabelNormal" Text="Invoice Value"
                                            MaxLength="12"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInvoiceValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label27" runat="server" SkinID="LabelNormal" Text="OS/LS Indicator"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlOSIndicator" runat="server" DataSourceID="ODS_OSIndicator"
                                            DataTextField="Desc" DataValueField="Code" SkinID="DropDownListNormal" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label30" runat="server" SkinID="LabelNormal" Text="IGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtIGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSGSTValue" runat="server" SkinID="LabelNormal" Text="SGST"></asp:Label>
                                        <asp:Label ID="lblUTGSTValue" runat="server" SkinID="LabelNormal" Text="UTGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return AutoFillCGSTSGST(this.id);"></asp:TextBox>
                                        <asp:TextBox ID="txtUTGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return return AutoFillCGSTSGST(this.id);"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label29" runat="server" SkinID="LabelNormal" Text="CGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return AutoFillCGSTSGST(this.id);"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label24" runat="server" SkinID="LabelNormal" Text="TCS Value"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtTCSValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="label" style="display: none">
                                        <asp:Label ID="Label28" runat="server" SkinID="LabelNormal" Text="Reason for Return"></asp:Label>
                                    </td>
                                    <td class="inputcontrols" style="display: none">
                                        <asp:DropDownList ID="ddlReasonForReturn" runat="server" DataSourceID="ODS_ReasonForReturn"
                                            DataTextField="Desc" DataValueField="Code" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label"></td>
                                    <td class="inputcontrols"></td>
                                </tr>
                            </table>
                            <!-- Section 2 - DELIVERY CHALLAN -->
                            <div class="subFormTitle">
                                DELIVERY CHALLAN
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="DC Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDCNumber" runat="server" SkinID="TextBoxNormal" MaxLength="20"
                                            onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="DC Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDcDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return checkDateForDCDate(this.id);"></asp:TextBox>
                                        <%--<asp:ImageButton ID="ImgDcDate" ImageUrl="~/Images/Calendar.png" runat="server" SkinID="ImageButtonCalendar" />--%>
                                        <ajaxToolkit:CalendarExtender ID="CalExtDcDate" PopupButtonID="ImgDcDate" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtDcDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label"></td>
                                    <td class="inputcontrols"></td>
                                </tr>
                            </table>
                            <!-- Section 3 - CARRIER INFORMATION -->
                            <div class="subFormTitle">
                                CARRIER INFORMATION
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="LR Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="LR Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return checkDateForLRDate(this.id);"></asp:TextBox>
                                        <%--<asp:ImageButton ID="ImgLRDate" ImageUrl="~/Images/Calendar.png" runat="server" SkinID="ImageButtonCalendar" />--%>
                                        <ajaxToolkit:CalendarExtender ID="CalExtLRDate" PopupButtonID="ImgLRDate" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtLRDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericWithSlashAndSpace();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Place of Despatch"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPlaceOfDespatch" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericWithSlashAndSpace();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Weight"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtWeight" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Number of cases"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtNoOfCases" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trRoadPermitDetails" runat="server">
                                    <td class="label">
                                        <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Road Permit Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRoadPermitNo" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Road Permit Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRoadPermitDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return CheckValidDate(this.id, true, 'Road Permit Date');"></asp:TextBox>
                                        <%--<asp:ImageButton ID="ImgRoadPermitDt" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgRoadPermitDt"
                                            runat="server" Format="dd/MM/yyyy" TargetControlID="txtRoadPermitDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label"></td>
                                    <td class="inputcontrols"></td>
                                </tr>
                            </table>
                            <!-- Section 4 - Freight DETAILS -->
                            <div class="subFormTitle">
                                FREIGHT DETAILS
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Freight Amount"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtFreightAmount" runat="server" SkinID="TextBoxNormal" MaxLength="12" onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"
                                            onblur="return CurrencyValidateForNegative(this.id, event);" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label31" runat="server" SkinID="LabelNormal" Text="Freight Tax"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtFreightTax" runat="server" SkinID="TextBoxNormal" MaxLength="12" onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"
                                            onblur="return CurrencyValidateForNegative(this.id, event);" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Insurance"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInsurance" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"
                                            onblur="return CurrencyValidateForNegative(this.id, event);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Postal Charges"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPostalCharges" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"
                                            onblur="return CurrencyValidateForNegative(this.id, event);"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Coupon Charges"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCouponCharges" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"
                                            onblur="return CurrencyValidateForNegative(this.id, event);"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!-- Section 5 - INVOICE -->
                    </div>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            ITEM DETAILS
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="div1" style="overflow: auto; width: 1400px; margin: 0; padding: 0;">
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_RowDataBound"
                                        OnRowDeleting="grvItemDetails_OnRowDeleting" SkinID="GridViewScroll" AllowPaging="false">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                            <asp:TemplateField HeaderText="CCWH No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCWHNo" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlCCWHNo" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCCWHNo_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add Row" OnClick="ButtonAdd_Click"
                                                        SkinID="GridViewButtonFooter" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Part #">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSupplierPartNo" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlItemCode" runat="server" SkinID="GridViewDropDownList" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlItemCode_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPOQuantity" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRcvdQty" runat="server" Enabled="false" onkeypress="return IntegerValueOnly();"
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                    <asp:HiddenField ID="txtExistingQty" runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalanceQty" Enabled="false" runat="server" SkinID="TextBoxDisabled"
                                                        Text=""></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQty" runat="server" onkeypress="return IntegerValueOnly();" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="List Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtListPrice" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CostPrice/Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCstPricePerQty" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total CostPrice">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCostPrice" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coupon">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCoupon" onkeypress="return CurrencyNumberOnly();" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="List Less Discount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtListLessDiscount" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Location">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemLocation" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax %">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTaxPercentage" ReadOnly="false" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberwithOneDotOnly(this.id);"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemTaxValue" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCode" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                                    <asp:TextBox ID="txtSerialNo" runat="server" SkinID="TextBoxDisabledSmall" Visible="false" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtPONumber" runat="server" SkinID="TextBoxDisabledSmall" Visible="false" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                                <input id="hdnRowCnt" type="hidden" runat="server" />
                                <input id="hdnFooterCostPrice" type="hidden" runat="server" />
                                <input id="hdnFooterTaxPrice" type="hidden" runat="server" />
                                <input id="hdnFooterCoupon" type="hidden" runat="server" />
                                <input id="hdnSelItemCode" type="hidden" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"
                                SkinID="ButtonNormal" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click"
                                SkinID="ButtonNormal" Visible="false" />
                        </div>
                    </div>
                </div>
                <input id="hdnScreenMode" type="hidden" runat="server" />
                <input id="hdnDocStatus" type="hidden" runat="server" />
                <input id="hdnSecondSales" type="hidden" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersManualGRN"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_INWARDENTRY" runat="server" SelectMethod="GetInwardEntriesList"
        TypeName="IMPALLibrary.Transactions.InwardTransactions" DataObjectTypeName="IMPALLibrary.Transactions.InwardTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_SupplyDepot" runat="server" SelectMethod="GetSupplierDepot"
        TypeName="IMPALLibrary.Transactions.InwardTransactions" DataObjectTypeName="IMPALLibrary.Transactions.InwardTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
            <asp:ControlParameter Name="SupplierCode" ControlID="ddlSupplierName" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/InwardEntry/TransactionType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_OSIndicator" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/InwardEntry/OSIndicator"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_ReasonForReturn" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/InwardEntry/ReasonForReturn"></asp:XmlDataSource>
</asp:Content>
