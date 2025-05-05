<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Vendor.aspx.cs" Inherits="IMPALWeb.Masters.Vendor.Vendor" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function IntegerValueOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function ValidatePhoneNo(i) {
            var phone = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtPhone');

            if (i == 1) {
                if (isNaN(phone.value)) {
                    phone.value = '';
                    return false;
                }

                if (phone.value.length != 13) {
                    alert('Phone Number length Should be 10 digits');
                    return false;
                }
            }

            if (i == 2) {
                if (isNaN(phone.value)) {
                    phone.value = '';
                    return false;
                }
            }
        }

        function ValidatePinCode(i) {
            var pincode = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtPinCode');

            if (i == 1) {
                if (isNaN(pincode.value)) {
                    pincode.value = '';
                    return false;
                }

                if (pincode.value.length != 6) {
                    alert('PinCode length Should be 6 digits');
                    return false;
                }
            }

            if (i == 2) {
                if (isNaN(pincode.value)) {
                    pincode.value = '';
                    return false;
                }
            }
        }

        function ValidateAlphaCode() {
            var VendName = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtName');
            var AlphaCode = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtAlphacode');

            if (VendName.value != "") {
                AlphaCode.value = VendName.value.substr(0, 1).toUpperCase();
                VendName.value = VendName.value.toUpperCase();
            }
            else {
                AlphaCode.value = "";
            }
        }
    </script>
    <div>
        <asp:UpdatePanel ID="UpdateVendor" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    Vendor Details
                </div>
                <div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblVendor" SkinID="LabelNormal" runat="server" Text="Vendor"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="drpVendorCode" SkinID="DropDownListNormal" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpVendorCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="PnlVendor" runat="server">
                    <asp:FormView ID="VendorFormView" runat="server" OnItemCreated="VendorFormView_ItemCreated"
                        OnDataBound="VendorFormView_DataBound">
                        <HeaderStyle ForeColor="white" BackColor="Blue" />
                        <ItemTemplate>
                            <table id="tblHeader" class="subFormTable">
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            Vendor
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCode" ReadOnly="true" runat="server" Text='<%# Eval("Vendor_Code") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Vendor_Name") %>' SkinID="TextBoxNormal" onblur="return ValidateAlphaCode();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldVendor" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtName" SetFocusOnError="true" ErrorMessage="<br/>Vendor Name is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldBranch" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlBranch" SetFocusOnError="true" ErrorMessage="Branch Name is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Vendor Location"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols" colspan="3">
                                        <asp:DropDownList ID="ddlLocalOrOutStation" runat="server" SkinID="DropDownListNormal"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLocalOrOutStation_SelectedIndexChanged">
                                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                            <asp:ListItem Text="Out Station" Value="O"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldLocalOutStation" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlLocalOrOutStation" SetFocusOnError="true"
                                            ErrorMessage="Local/OutStation is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Local/Outstation State"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols" colspan="3">
                                        <asp:DropDownList ID="ddlLocalOutStaionList" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnStateCode" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldOutStation" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlLocalOutStaionList" SetFocusOnError="true"
                                            ErrorMessage="OutStation State is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            COMMUNICATION
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAddOne" runat="server" SkinID="LabelNormal" Text="Address 1"></asp:Label><span
                                            class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAdd1" runat="server" Text='<%# Eval("address1") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAdd" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtAdd1" SetFocusOnError="true" ErrorMessage="<br/>Address is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblAddTwo" runat="server" SkinID="LabelNormal" Text="Address 2"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAdd2" runat="server" Text='<%# Eval("address2") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAdd3" runat="server" SkinID="LabelNormal" Text="Address 3"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAdd3" runat="server" Text='<%# Eval("address3") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblAdd4" runat="server" SkinID="LabelNormal" Text="Address 4"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAdd4" runat="server" Text='<%# Eval("address4") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblLocation" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLocation" runat="server" Text='<%# Eval("location") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPinCode" runat="server" SkinID="LabelNormal" Text="PinCode"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPinCode" runat="server" Text='<%# Eval("Pincode") %>' SkinID="TextBoxNormal"
                                            MaxLength="6" onkeypress="return IntegerValueOnly();" onblur="return ValidatePinCode(1);" onpaste="return ValidatePinCode(2);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblTown" runat="server" SkinID="LabelNormal" Text="Town"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTown" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldTown" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlTown" SetFocusOnError="true" ErrorMessage="Town is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("Phone") %>' SkinID="TextBoxNormal"
                                            MaxLength="13" onkeypress="return IntegerValueOnly();" onblur="return ValidatePhoneNo(1);" onpaste="return ValidatePhoneNo(2);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblEmail" runat="server" SkinID="LabelNormal" Text="Email"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblContactPerson" runat="server" SkinID="LabelNormal" Text="Contact Person"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtContactPerson" runat="server" Text='<%# Eval("Contact_Person") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAlphaCode" runat="server" SkinID="LabelNormal" Text="Alpha Code"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAlphacode" runat="server" Text='<%# Eval("Vendor_Alpha_Code") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAlpha" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtAlphacode" SetFocusOnError="true" ErrorMessage="<br/>Alpha Code is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            SALES TAX
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblLocalTax" runat="server" SkinID="LabelNormal" Text="GSTIN Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtGSTIN" runat="server" Text='<%# Eval("GSTIN") %>'
                                            SkinID="TextBoxNormal" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblChartAcc" runat="server" SkinID="LabelNormal" Text="Chart of Account"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtChartAc" runat="server" Text='<%# Eval("Chart_of_Account_Code") %>'
                                            SkinID="TextBoxDisabled" Enabled="False"></asp:TextBox>
                                        <User:ChartAccount ID="UserChartAccount" runat="server" Visible="false" OnSearchImageClicked="Vendor_SearchImageClicked" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status "></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlStatus" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldStatus" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlStatus" SetFocusOnError="true" ErrorMessage="Status is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            BANK INFORMATION
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBankName" runat="server" SkinID="LabelNormal" Text="Bank Name"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtBankName" runat="server" Text='<%# Eval("Vendor_bank_name") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblBranchName" runat="server" SkinID="LabelNormal" Text="Branch Name"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Eval("Vendor_bank_branch") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBankAdd" runat="server" SkinID="LabelNormal" Text="Bank Address"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtBankAddress" runat="server" Text='<%# Eval("Vendor_bank_address") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
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
                        <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" Visible="false"
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
