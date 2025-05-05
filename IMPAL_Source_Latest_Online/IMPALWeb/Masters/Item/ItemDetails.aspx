<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ItemDetails.aspx.cs"
    Inherits="IMPALWeb.Masters.Item.ItemDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function StkItemSelectedHandler(source, eventArgs) 
		{
            document.getElementById("<%= txtSupplierPartNumber.ClientID %>").value = eventArgs.get_text();
            document.getElementById("<%= hddItemCode.ClientID %>").value = eventArgs.get_value();
            document.getElementById("<%= btnAutoComple.ClientID %>").click()
        }
    </script>

    <div>
        <asp:UpdatePanel ID="updateItemDetails" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    Item Details</div>
                <div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierPartNumber" SkinID="LabelNormal" runat="server" Text="Supplier Part #"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSupplierPartNumber" SkinID="LabelNormal" runat="server"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomers" FirstRowSelected="true"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" SkinID="AutoCompleteNormal"
                                    OnClientItemSelected="StkItemSelectedHandler" CompletionSetCount="10" TargetControlID="txtSupplierPartNumber"
                                    ID="AutoCompleteExtender1" runat="server">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="PnlItemDetails" runat="server">
                    <asp:FormView ID="ItemDetailFormView" runat="server" OnItemCreated="ItemDetailFormView_ItemCreated"
                        OnDataBound="ItemDetailFormView_DataBound" OnPageIndexChanging="ItemDetailFormView_PageIndexChanging">
                        <HeaderStyle ForeColor="white" BackColor="Blue" />
                        <ItemTemplate>
                            <table id="tblHeader" class="subFormTable">
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            Item</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblItemCode" runat="server" SkinID="LabelNormal" Text="Item Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtItemCode" ReadOnly="true" runat="server" Text='<%# Eval("Item_Code") %>'
                                            SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblSupplierLine" runat="server" SkinID="LabelNormal" Text="Supplier Line"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpSupplierLine" AutoPostBack="true" runat="server" SkinID="DropDownListNormalBig"
                                            OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldSupplierLine" ValidationGroup="validate"
                                            runat="server" ControlToValidate="drpSupplierLine" SetFocusOnError="true" ErrorMessage="Supplier Line is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPartNumber" runat="server" SkinID="LabelNormal" Text="Supplier part #"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPartNumber" onblur="return ValidateFirstCharacter(this.id, 'Supplier part.');"
                                            runat="server" Text='<%# Eval("Supplier_Part_Number") %>' SkinID="TextBoxNormalBig">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPartNumber" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtPartNumber" SetFocusOnError="true" ErrorMessage="Supplier part #"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblShortName" runat="server" SkinID="LabelNormal" Text="Short desc."></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtShortName" onblur="return ValidateFirstCharacter(this.id, 'Short desc.');"
                                            runat="server" Text='<%# Eval("Item_Short_Description") %>' SkinID="TextBoxNormalBig">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldShortName" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtShortName" SetFocusOnError="true" ErrorMessage="Short desc. is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblLongDesc" runat="server" SkinID="LabelNormal" Text="Long description"></asp:Label><span
                                                class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtLongDesc" onblur="return ValidateFirstCharacter(this.id, 'Long description');"
                                                runat="server" Text='<%# Eval("Item_Long_Description") %>' SkinID="TextBoxNormalBig">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldLongDesc" InitialValue="" ValidationGroup="validate"
                                                runat="server" ControlToValidate="txtLongDesc" SetFocusOnError="true" ErrorMessage="Short desc. is required"
                                                SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CLASSIFICATION</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblHSNCode" runat="server" SkinID="LabelNormal" Text="HSN Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtHSNCode" runat="server" Text='<%# Eval("Item_Type_Code") %>'
                                         SkinID="TextBoxNormalBig">
                                        </asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblProductGrp" runat="server" SkinID="LabelNormal" Text="Product group"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpProductGrp" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldProductGrp" ValidationGroup="validate"
                                            runat="server" ControlToValidate="drpProductGrp" SetFocusOnError="true" ErrorMessage="Item type is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppSegment" runat="server" SkinID="LabelNormal" Text="Application segment"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpAppSegment" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAppSegment" ValidationGroup="validate"
                                            runat="server" ControlToValidate="drpAppSegment" SetFocusOnError="true" ErrorMessage="Application segment is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblVehType" runat="server" SkinID="LabelNormal" Text="Vehicle type"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpVehType" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldVehType" ValidationGroup="validate"
                                            runat="server" ControlToValidate="drpVehType" SetFocusOnError="true" ErrorMessage="Vehicle type is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAbcInd" runat="server" SkinID="LabelNormal" Text="ABC indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpAbcInd" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblFsnInd" runat="server" SkinID="LabelNormal" Text="FSN indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpFsnInd" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            DISCOUNTS
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPurchaseDis" runat="server" SkinID="LabelNormal" Text="Purchase discount"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPurchaseDis" runat="server" onkeypress="enterNumberOnly();" onblur="return CheckForNumberRange(this.id, 'Purchase discount','100');"
                                            Text='<%# Eval("Purchase_Discount") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPurchaseDis" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtPurchaseDis" SetFocusOnError="true" ErrorMessage="Purchase discount is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblEdDis" runat="server" SkinID="LabelNormal" Text="ED discount"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtEdDis" onkeypress="enterNumberOnly();" onblur="return CheckForNumberRange(this.id, 'ED discount','100');"
                                            runat="server" Text='<%# Eval("Excise_Duty_Discount") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldEdDis" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtEdDis" SetFocusOnError="true" ErrorMessage="ED discount is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            EXCISE DUTY</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblInd" runat="server" SkinID="LabelNormal" Text="Indicator"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButton ID="RdoIndPercent" Checked="true" GroupName="ExDuty" runat="server"
                                            Text="Percent" />
                                        <asp:RadioButton ID="RdoIndAmount" GroupName="ExDuty" runat="server" Text="Amount" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblEdValue" runat="server" SkinID="LabelNormal" Text="ED value"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtEdValue" onkeypress="enterNumberOnly();" MaxLength="12" runat="server"
                                            Text='<%# Eval("Excise_Duty_Value") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldEdValue" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtEdValue" SetFocusOnError="true" ErrorMessage="ED value is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            UNIT OF MEASUREMENT</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblUOM" runat="server" SkinID="LabelNormal" Text="UOM"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtUOM" onkeypress="return AlphaNumericOnly();" MaxLength="6" runat="server"
                                            Text='<%# Eval("Unit_of_Measurement") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldUOM" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtUOM" SetFocusOnError="true" ErrorMessage="UOM is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPackQty" runat="server" SkinID="LabelNormal" Text="Packing Qty"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPackQty" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Packing_Quantity") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPackQty" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtPackQty" SetFocusOnError="true" ErrorMessage="Packing Qty is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            RATE</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRateInd" runat="server" SkinID="LabelNormal" Text="Indicator"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButton ID="rdoRateIndYes" Checked="true" GroupName="RateInd" runat="server"
                                            Text="Yes" />
                                        <asp:RadioButton ID="rdoRateIndNo" GroupName="RateInd" runat="server" Text="No" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblValue" runat="server" SkinID="LabelNormal" Text="Value"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtValue" onkeypress="enterNumberOnly();" MaxLength="12" runat="server"
                                            Text='<%# Eval("Rate") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValue" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtValue" SetFocusOnError="true" ErrorMessage="value is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CONTROLS
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMinQnt" runat="server" SkinID="LabelNormal" Text="Minimum order quantity"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtMinQnt" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Minimum_Order_Quantity") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblMaxQnt" runat="server" SkinID="LabelNormal" Text="Max order qty"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtMaxQnt" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Maximum_Order_Quantity") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblEconomicQnt" runat="server" SkinID="LabelNormal" Text="Economic batch qty"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtEconomicQnt" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Economic_Batch_Quantity") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblSaftyStock" runat="server" SkinID="LabelNormal" Text="Safety stock"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSaftyStock" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Safety_stock") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMinLead" runat="server" SkinID="LabelNormal" Text="Min. Lead time"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtMinLead" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Minimum_Lead_Time") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblMaxLead" runat="server" SkinID="LabelNormal" Text="Max. Lead"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtMaxLead" onkeypress="enterNumberOnly();" MaxLength="4" runat="server"
                                            Text='<%# Eval("Maximum_Lead_Time") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpStatus" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldStatus" ValidationGroup="validate" runat="server"
                                            ControlToValidate="drpStatus" SetFocusOnError="true" ErrorMessage="value is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </asp:Panel>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="validate" Text="Submit"
                            SkinID="ButtonNormal" Visible="false" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Generate Report" Visible="false" SkinID="ButtonViewReport"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:Button ID="btnAutoComple" runat="server" Style="display: none;" OnClick="btnAutoComple_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hddItemCode" runat="server" />
</asp:Content>
