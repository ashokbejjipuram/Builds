<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Customer.aspx.cs" Inherits="IMPALWeb.Masters.Customer.Customer" %>

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

        function CurrencyNumberOnly() {
            var AsciiValue = event.keyCode;

            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
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

                if (phone.value.length != 10) {
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
            var CustName = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtName');
            var AlphaCode = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtAlphacode');

            if (CustName.value != "") {
                AlphaCode.value = CustName.value.trim().substr(0, 1).toUpperCase();
                CustName.value = CustName.value.trim().toUpperCase();
            }
            else {
                AlphaCode.value = "";
            }
        }

        function CreditLimitChange() {
            if (document.getElementById('ctl00_CPHDetails_BtnSubmit').value == "Update") {
                var OldCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_hdnCreditLimit').value;
                var NewCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtCreditLimit').value;
                var ValiditityInd = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlValidityIndicator');

                if (parseFloat(NewCrLimit) >= 1000000 && parseFloat(OldCrLimit) < parseFloat(NewCrLimit)) {
                    alert('Credit Limit Change of Rs. 10 Lakhs and Above will be Updated by only HO Admin Department. Please Contact HO.');
                    return false;
                }

                if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit))
                    ValiditityInd.disabled = false;
                else
                    ValiditityInd.disabled = true;
            }
            else
                ValiditityInd.disabled = true;
        }

        function CreditLimitValidity() {
            if (document.getElementById('ctl00_CPHDetails_BtnSubmit').value == "Update") {
                var DueDate = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtCrlimitDueDate');
                var OldCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_hdnCreditLimit').value;
                var NewCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtCreditLimit').value;
                var ValiditityInd = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlValidityIndicator').value;

                if (parseFloat(NewCrLimit) >= 1000000 && parseFloat(OldCrLimit) < parseFloat(NewCrLimit)) {
                    alert('Credit Limit Change of Rs. 10 Lakhs and Above will be Updated by only HO Admin Department. Please Contact HO.');
                    return false;
                }

                if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd == "T")
                    DueDate.disabled = false;
                else
                    DueDate.disabled = true;
            }
            else
                ValiditityInd.disabled = true;
        }

        function CheckValidDateFields() {
            var CustClassifiation = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlCustClassifiation');
            var CustSegment = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlCustSegment');
            var LocalOrOutStation = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlLocalOrOutStation');
            var OldCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_hdnCreditLimit').value;
            var NewCrLimit = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtCreditLimit').value;
            var ValiditityInd = document.getElementById('ctl00_CPHDetails_CustomerFormView_ddlValidityIndicator');
            var DueDate = document.getElementById('ctl00_CPHDetails_CustomerFormView_txtCrlimitDueDate');

            if (CustClassifiation.value.trim() == "") {
                alert('Please Select Customer Classifiation');
                CustClassifiation.focus();
                return false;
            }

            if (CustSegment.value.trim() == "") {
                alert('Please Select Customer Segment');
                CustSegment.focus();
                return false;
            }

            if (LocalOrOutStation.value.trim() == "") {
                alert('Please Select Customer Town Location');
                LocalOrOutStation.focus();
                return false;
            }

            if (parseFloat(NewCrLimit) >= 1000000 && parseFloat(OldCrLimit) < parseFloat(NewCrLimit)) {
                alert('Credit Limit Change of Rs. 10 Lakhs and Above will be Updated by only HO Admin Department. Please Contact HO.');
                return false;
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "") {
                alert('Please Select Validity Indicator for Credit Limit Change');
                ValiditityInd.focus();
                return false;
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T") {
                if (DueDate.value.trim() == "") {
                    alert('Please Select Validity Date for Credit Limit Change');
                    DueDate.focus();
                    return false;
                }

                if (fnIsDate(DueDate.value.trim()) == false) {
                    DueDate.focus();
                    return false;
                }
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T" && DueDate.value.trim() != "") {
                var oSysDate = new Date();
                var oDate = DueDate.value.trim().split("/");

                var oCurDateFormatted = (oSysDate.getMonth() + 1) + '/' + oSysDate.getDate() + '/' + oSysDate.getFullYear();
                var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

                if (new Date(oCurDateFormatted) > new Date(oDateFormatted)) {
                    alert("Validity Date should be greater than or equal to System Date");
                    DueDate.value = "";
                    DueDate.focus();
                    return false;
                }
            }
        }
    </script>

    <div>
        <asp:UpdatePanel ID="UpdateCustomer" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    CUSTOMER Details
                </div>
                <div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="drpCustomerCode" SkinID="DropDownListNormal" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpCustomerCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="PnlCustomer" runat="server">
                    <asp:FormView ID="CustomerFormView" runat="server" OnItemCreated="CustomerFormView_ItemCreated"
                        OnDataBound="CustomerFormView_DataBound">
                        <HeaderStyle ForeColor="white" BackColor="Blue" />
                        <ItemTemplate>
                            <table id="tblHeader" class="subFormTable">
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CUSTOMER
                                        </div>
                                    </td>
                                    <td width="170px">&nbsp;</td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Date of Creation"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDateOfCreation" runat="server" Text='<%# Eval("Date_Of_Creation") %>' SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCode" ReadOnly="true" runat="server" Text='<%# Eval("Customer_Code") %>'
                                            SkinID="TextBoxDisabled"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Customer_Name") %>' SkinID="TextBoxNormal" onblur="return ValidateAlphaCode();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldCustomer" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtName" SetFocusOnError="true" ErrorMessage="<br/>Customer Name is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldBranch" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlBranch" SetFocusOnError="true" ErrorMessage="<br/>Branch Name is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCustType" runat="server" SkinID="LabelNormal" Text="Customer Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustType" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldCustType" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlCustType" SetFocusOnError="true" ErrorMessage="<br/>Customer Type is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Customer State"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustomerState" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnStateCode" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldOutStation" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlCustomerState" SetFocusOnError="true"
                                            ErrorMessage="<br/>State is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Customer Classification"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustClassifiation" runat="server" SkinID="DropDownListNormal">
                                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="WHOLESALERS" Value="WHOLESALERS" />
                                            <asp:ListItem Text="SEMI WHOLESALER" Value="SEMI WHOLESALER" />
                                            <asp:ListItem Text="RETAILERS" Value="RETAILERS" />
                                            <asp:ListItem Text="RETAIL CUM GARAGE" Value="RETAIL CUM GARAGE" />
                                            <asp:ListItem Text="AUTHORIZED VEHICLE SERVICE CENTRE (MASS/TASS/LEYLAND/TATA ETC)" Value="AUTHORIZED VEHICLE SERVICE CENTRE (MASS/TASS/LEYLAND/TATA ETC)" />
                                            <asp:ListItem Text="AUTHORIZED SPARES SERVICE CENTRE (TRW/WABCO/LUCAS/BRAKES INDIA ETC)" Value="AUTHORIZED SPARES SERVICE CENTRE (TRW/WABCO/LUCAS/BRAKES INDIA ETC)" />
                                            <asp:ListItem Text="MULTI CAR/TRUCK SERVICE CENTRE" Value="MULTI CAR/TRUCK SERVICE CENTRE" />
                                            <asp:ListItem Text="FLEET OWNER" Value="FLEET OWNER" />																													
                                            <asp:ListItem Text="LUBRICANTS AND GREASE DEALER" Value="LUBRICANTS AND GREASE DEALER" />
                                            <asp:ListItem Text="STU/GOVT. DEPARTMENT" Value="STU/GOVT. DEPARTMENT" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlCustClassifiation" SetFocusOnError="true"
                                            ErrorMessage="<br/>Customer Classifiation is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>                                    
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Customer Segment"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols" colspan="3">
                                        <asp:DropDownList ID="ddlCustSegment" runat="server" SkinID="DropDownListNormal">
                                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="ACCESSORIES" Value="ACCESSORIES" />
                                            <asp:ListItem Text="2/3 WHEELERS" Value="2/3 WHEELERS" />
                                            <asp:ListItem Text="PC/UV" Value="PC/UV" />
                                            <asp:ListItem Text="SCV/LCV/HCV" Value="SCV/LCV/HCV" />
                                            <asp:ListItem Text="LCV/SCV" Value="LCV/SCV" />
                                            <asp:ListItem Text="MULTI SEGMENT" Value="MULTI SEGMENT" />
                                            <asp:ListItem Text="FARM TRACTOR" Value="FARM TRACTOR" />
                                            <asp:ListItem Text="HARDWARE" Value="HARDWARE" />
                                            <asp:ListItem Text="INDUSTRIAL" Value="INDUSTRIAL" />
                                            <asp:ListItem Text="LUBRICANTS AND GREASE" Value="LUBRICANTS AND GREASE" />
                                            <asp:ListItem Text="PASSENGER CARS/UTILITY VEHICLES" Value="PASSENGER CARS/UTILITY VEHICLES" />
                                            <asp:ListItem Text="OFF HIGHWAY" Value="OFF HIGHWAY" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlCustSegment" SetFocusOnError="true"
                                            ErrorMessage="<br/>Customer Segment is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="lblAddOne" runat="server" SkinID="LabelNormal" Text="Address 1"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAddOne" runat="server" Text='<%# Eval("address1") %>' SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAdd" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtAddOne" SetFocusOnError="true" ErrorMessage="<br/>Address is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblAddTwo" runat="server" SkinID="LabelNormal" Text="Address 2"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAddTwo" runat="server" Text='<%# Eval("address2") %>' SkinID="TextBoxNormal"></asp:TextBox>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldLocation" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtLocation" SetFocusOnError="true" ErrorMessage="<br/>Location is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPinCode" runat="server" SkinID="LabelNormal" Text="PinCode"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPinCode" runat="server" Text='<%# Eval("Pincode") %>' SkinID="TextBoxNormal"
                                            MaxLength="6" onkeypress="return IntegerValueOnly();" onblur="return ValidatePinCode(1);" onpaste="return ValidatePinCode(2);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPinCode" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtPinCode" SetFocusOnError="true" ErrorMessage="<br/>Pin Code is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
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
                                            runat="server" ControlToValidate="ddlTown" SetFocusOnError="true" ErrorMessage="<br/>Town is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Town Location"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlLocalOrOutStation" runat="server" SkinID="DropDownListNormal">
                                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                            <asp:ListItem Text="Out Station" Value="O"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldLocalOutStation" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlLocalOrOutStation" SetFocusOnError="true"
                                            ErrorMessage="<br/>Local/OutStation is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("Phone") %>' SkinID="TextBoxNormal"
                                            MaxLength="10" onkeypress="return IntegerValueOnly();" onblur="return ValidatePhoneNo(1);" onpaste="return ValidatePhoneNo(2);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPhone" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtPhone" SetFocusOnError="true" ErrorMessage="<br/>Phone Number is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblTelex" runat="server" SkinID="LabelNormal" Text="Telex"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtTelex" runat="server" Text='<%# Eval("Telex") %>' SkinID="TextBoxNormal"></asp:TextBox>
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
                                        <asp:Label ID="lblContactDesg" runat="server" SkinID="LabelNormal" Text="Contact Mobile No."></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtContactDesignation" runat="server" Text='<%# Eval("Contact_Person_Mobile") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblAlphaCode" runat="server" SkinID="LabelNormal" Text="Alpha Code"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAlphacode" runat="server" Text='<%# Eval("Alpha_Code") %>' SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldAlpha" ValidationGroup="validate" runat="server"
                                            ControlToValidate="txtAlphacode" SetFocusOnError="true" ErrorMessage="<br/>Alpha Code is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSalesman" runat="server" SkinID="LabelNormal" Text="Salesman"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSalesman" runat="server" SkinID="GridViewDropDownList" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSalesman_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldSalesman" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlSalesman" SetFocusOnError="true" ErrorMessage="<br/>Salesman is required"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblSalesmanCode" runat="server" SkinID="LabelNormal" Text="Salesman Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSalesmanCode" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CARRIER
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPrefCarrier" runat="server" SkinID="LabelNormal" Text="Preferred Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPreferredCarrier" runat="server" Text='<%# Eval("Carrier") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblDesination" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDestination" runat="server" Text='<%# Eval("Destination") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
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
                                        <asp:TextBox ID="txtGSTIN" runat="server" Text='<%# Eval("Local_Sales_Tax_Number") %>'
                                            SkinID="TextBoxNormal" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldGSTIN" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtGSTIN" SetFocusOnError="true" ErrorMessage="<br/><b>GSTIN is required. Please fill as UNREGISTERED in case of no GSTIN</b>"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCentralSales" runat="server" SkinID="LabelNormal" Text="PAN Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCentralST" runat="server" Text='<%# Eval("Central_Sales_Tax_Number") %>'
                                            SkinID="TextBoxNormal" MaxLength="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldPAN" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="txtCentralST" SetFocusOnError="true" ErrorMessage="<br/><b>PAN is required. Please fill as UNREGISTERED in case of no PAN</b>"
                                            SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CREDIT LIMIT
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCreditLimit" runat="server" SkinID="LabelNormal" Text="Credit Limit"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCreditLimit" runat="server" Text='<%# Eval("Credit_Limit") %>'
                                            SkinID="TextBoxNormal" Enabled="true" onkeypress="return CurrencyNumberOnly();" onchange="return CreditLimitChange()"></asp:TextBox>
                                        <asp:HiddenField ID="hdnCreditLimit" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCreditInd" runat="server" SkinID="LabelNormal" Text="Credit Limit Indicator"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCrLimitIndicator" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldCrLimitIndicator" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlCrLimitIndicator" SetFocusOnError="true"
                                            ErrorMessage="<br/>Credit Limit Indicator is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Validity Indicator"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlValidityIndicator" runat="server" SkinID="DropDownListNormal" Enabled="false" onchange="return CreditLimitValidity()">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="T">Temporary</asp:ListItem>
                                            <asp:ListItem Value="P">Permanent</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Validity Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCrlimitDueDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" Enabled="false"
                                            onchange="return CheckValidDateCrLimit();"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceCrlimitDueDate" PopupButtonID="imgCrlimitDueDate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtCrlimitDueDate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCollectionDay" runat="server" SkinID="LabelNormal" Text="Collection Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCollectiondays" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblOutAmt" runat="server" SkinID="LabelNormal" Text="Outstanding Amount"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtOutstandingamount" runat="server" Text='<%# Eval("Outstanding_Amount") %>'
                                            SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblChartAcc" runat="server" SkinID="LabelNormal" Text="Chart of Account"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtChartAc" runat="server" Text='<%# Eval("Chart_of_Account_Code") %>'
                                            SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <User:ChartAccount ID="UserChartAccount" runat="server" OnSearchImageClicked="Customer_SearchImageClicked" Visible="false" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status "></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlStatus" runat="server" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldStatus" InitialValue="" ValidationGroup="validate"
                                            runat="server" ControlToValidate="ddlStatus" SetFocusOnError="true" ErrorMessage="<br/>Status is required"
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
                                        <asp:TextBox ID="txtBankName" runat="server" Text='<%# Eval("customer_bank_name") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblBranchName" runat="server" SkinID="LabelNormal" Text="Branch Name"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Eval("customer_bank_branch") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBankAdd" runat="server" SkinID="LabelNormal" Text="IFSC Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtIFSCcode" runat="server" Text='<%# Eval("IFSC_Code") %>'
                                            SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblTinNum" runat="server" SkinID="LabelNormal" Text="Tin Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtTinNumber" runat="server" Text='<%# Eval("TinNumber") %>' SkinID="TextBoxNormal"></asp:TextBox>
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
                        <asp:Button ID="btnReport" runat="server" Visible="false" Text="Generate Report" SkinID="ButtonViewReport"
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
