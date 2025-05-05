<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierDetails.aspx.cs"
    MasterPageFile="~/Main.Master" Inherits="IMPALWeb.Masters.Supplier.SupplierDetails" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div>
        <asp:UpdatePanel ID="updateSupplier" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    Supplier Details</div>
                <div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierCode" SkinID="LabelNormal" runat="server" Text="Supplier Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="drpSupplierCode" SkinID="DropDownListNormalBig" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpSupplierCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="PnlSupplier" runat="server">
                    <asp:FormView ID="SupplierFormView" runat="server" OnDataBound="SupplierFormView_DataBound">
                        <HeaderStyle />
                        <ItemTemplate>
                            <table id="tblHeader" class="subFormTable">
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            SUPPLIER</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCode" MaxLength="3" onkeypress="return IntegerValueOnly()" runat="server"
                                            Text='<%# Eval("Supplier_Code") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldcode" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtCode" SetFocusOnError="true" ErrorMessage="Supplier Code is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtName" onblur="return ValidateFirstCharacter(this.id, 'Name.');"
                                            runat="server" Text='<%# Eval("Supplier_Name") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldName" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtName" SetFocusOnError="true" ErrorMessage="Supplier Name is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblType" runat="server" SkinID="LabelNormal" Text="Supplier Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpSupplierType" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldType" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="drpSupplierType" SetFocusOnError="true" ErrorMessage="Supplier Type is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblInsurance" runat="server" SkinID="LabelNormal" Text="Insurance"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButton ID="RdoInsYes" Checked="true" GroupName="Insurance" runat="server"
                                            Text="Yes" />
                                        <asp:RadioButton ID="RdoInsNo" GroupName="Insurance" runat="server" Text="No" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            COMMUNICATION DETAILS</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAddress" runat="server" SkinID="LabelNormal" Text="Address"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAdd" onblur="return ValidateFirstCharacter(this.id, 'Address.');"
                                            runat="server" Text='<%# Eval("Address") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAdd" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtAdd" SetFocusOnError="true" ErrorMessage="Address is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPinCode" runat="server" SkinID="LabelNormal" Text="PinCode"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPinCode" onblur="return ValidateFirstCharacter(this.id, 'PinCode.');"
                                            runat="server" Text='<%# Eval("Pincode") %>' SkinID="TextBoxNormalBig" MaxLength="6"
                                            onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPhone" onblur="return ValidateFirstCharacter(this.id, 'Phone.');"
                                            runat="server" Text='<%# Eval("Phone") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblFax" runat="server" SkinID="LabelNormal" Text="Fax"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtFax" onblur="return ValidateFirstCharacter(this.id, 'Fax.');"
                                            runat="server" Text='<%# Eval("Fax") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblTelex" runat="server" SkinID="LabelNormal" Text="Telex"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtTelex" onblur="return ValidateFirstCharacter(this.id, 'Telex.');"
                                            runat="server" Text='<%# Eval("Telex") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblEmail" runat="server" SkinID="LabelNormal" Text="Email"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtEmail" onblur="return ValidateFirstCharacter(this.id, 'Email.');"
                                            runat="server" Text='<%# Eval("Email") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblContactPerson" runat="server" SkinID="LabelNormal" Text="Contact Person"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtContactPerson" onblur="return ValidateFirstCharacter(this.id, 'Contact Person.');"
                                            runat="server" Text='<%# Eval("Contact_Person") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblContactDesg" runat="server" SkinID="LabelNormal" Text="Contact Designation"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtContactDesignation" onblur="return ValidateFirstCharacter(this.id, 'Contact Designation.');"
                                            runat="server" Text='<%# Eval("Contact_Designation") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CARRIER INFORMATION</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPrefCarrier" runat="server" SkinID="LabelNormal" Text="Preferred Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPreferredCarrier" onblur="return ValidateFirstCharacter(this.id, 'Preferred Carrier.');"
                                            runat="server" Text='<%# Eval("Carrier") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblDesination" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDestination" onblur="return ValidateFirstCharacter(this.id, 'Destination.');"
                                            runat="server" Text='<%# Eval("Destination") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CLASSIFICATION</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAdPayMent" runat="server" SkinID="LabelNormal" Text="Adjust payment"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:CheckBox ID="chkAdPayMent" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblBMS" runat="server" SkinID="LabelNormal" Text="BMS Company"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:CheckBox ID="chkBMS" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAlphaCode" runat="server" SkinID="LabelNormal" Text="Alpha code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAlphaCode" runat="server" onblur="return ValidateFirstCharacter(this.id, 'Alpha code.');"
                                            Text='<%# Eval("Alpha_Code") %>' SkinID="TextBoxNormalBig" MaxLength="1"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblChartAcc" runat="server" SkinID="LabelNormal" Text="Chart of Account"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtChartAc" runat="server" Text='<%# Eval("Chart_of_Account_Code") %>'
                                            SkinID="TextBoxNormalBig" Enabled="false"></asp:TextBox>
                                        <User:ChartAccount ID="UserChartAccount" runat="server" OnSearchImageClicked="Customer_SearchImageClicked" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblStockValue" runat="server" SkinID="LabelNormal" Text="Optimum stock value "></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtStockValue" runat="server" onblur="return ValidateFirstCharacter(this.id, 'Optimum stock value.');"
                                            Text='<%# Eval("Optimum_Stock_value") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblAutoLock" runat="server" SkinID="LabelNormal" Text="Auto Locking"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="drpAutoLock" runat="server" SkinID="DropDownListNormalSmall">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            SALES TAX</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSales" runat="server" SkinID="LabelNormal" Text="Local sales tax"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSales" onblur="return ValidateFirstCharacter(this.id, 'Local sales tax.');"
                                            runat="server" Text='<%# Eval("Local_Sales_Tax_Number") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCentraltax" runat="server" SkinID="LabelNormal" Text="Central sales tax"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCentraltax" onblur="return ValidateFirstCharacter(this.id, 'Central sales tax.');"
                                            runat="server" Text='<%# Eval("Central_Sales_Tax_Number") %>' SkinID="TextBoxNormalBig"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </asp:Panel>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="validate" Text="Submit"
                            SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
