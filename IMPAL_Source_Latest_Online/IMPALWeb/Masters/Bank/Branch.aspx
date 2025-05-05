<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Branch.aspx.cs"
    Inherits="IMPALWeb.Masters.Bank.Branch" %>

<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">

        function validateFields_BrName(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewName").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }

        function validateFields_acno(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewAccNo").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }

        function validateFields_address(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewAddress").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }
        function validateFields_cno(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewPhone").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }
        function validateFields_phone(source, arguments) {

            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewFax").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }
        function validateFields_cnp(source, arguments) {
            
            var TxtGLAcctCode = arguments.Value;
            //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_Branch_ctl12_txtNewContactPerson").value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }
        }
    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Bank Branch</div>
        <asp:UpdatePanel ID="updPanelBranch" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBank" Text="Bank" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlBank" SkinID="DropDownListNormal" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                    </tr>
                </table>
                <div class="subFormTitle">
                    Bank Branch Details</div>
                <div id="divItemDetails" runat="server">
                    <div class="gridViewScrollFullPage">
                        <asp:GridView ID="GV_Branch" runat="server" AllowPaging="true" ShowFooter="true"
                            AutoGenerateColumns="False" SkinID="GridViewScroll" OnPageIndexChanging="GV_Branch_PageIndexChanging"
                            OnRowCancelingEdit="GV_Branch_RowCancelingEdit" OnRowCreated="GV_Branch_RowCreated"
                            OnRowEditing="GV_Branch_RowEditing" OnRowUpdating="GV_Branch_RowUpdating" OnRowCommand="GV_Branch_RowCommand">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Bank Name" SortExpression="Bank_Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("Bank_Name") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlBankName" runat="server" SkinID="GridViewDropDownList">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewBank" Text="." runat="server" ControlToValidate="ddlBankName"
                                            ErrorMessage="Bank Name should not be empty" ValidationGroup="BranchAddGroup"
                                            SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="rvNewBank"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Branch_Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Branch_Name") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvBranchName" Text="." runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Please enter a valid Name" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE1" TargetControlID="rvBranchName" PopupPosition="BottomLeft"
                                            runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Branch_Name") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewName" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewDesc" Text="." runat="server" ControlToValidate="txtNewName"
                                            ErrorMessage="Please enter a valid Name" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="rvNewDesc"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <br />
                                        <asp:CustomValidator ID="CustNewBank" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewName"
                                            ClientValidationFunction="validateFields_BrName" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Bank_Branch_Code") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Number">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Bind("Account_Number") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvAccNo" runat="server" Text="." ControlToValidate="txtAccNo"
                                            ErrorMessage="Please enter a Account Number" ValidationGroup="BranchEditGroup"
                                            SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE2" TargetControlID="rvAccNo" PopupPosition="TopLeft"
                                            runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewAccNo" runat="server" Text='<%# Bind("Account_Number") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewAccNo" Text="." runat="server" ControlToValidate="txtNewAccNo"
                                            ErrorMessage="Please enter a Account Number" ValidationGroup="BranchAddGroup"
                                            SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE3" TargetControlID="rvNewAccNo" PopupPosition="TopLeft"
                                            runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="CusttxtNewAccNo" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewAccNo"
                                            ClientValidationFunction="validateFields_acno" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountNo" runat="server" Text='<%# Bind("Account_Number") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewAddress" runat="server" Text='<%# Bind("Address") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewtxtNewAddress" Text="." runat="server" ControlToValidate="txtNewAddress"
                                            ErrorMessage="Please enter a Address" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE4" TargetControlID="rvNewtxtNewAddress"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <br />
                                        <asp:CustomValidator ID="CusttxtNewAddress" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewAddress"
                                            ClientValidationFunction="validateFields_address" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewtxtNewPhone" Text="." runat="server" ControlToValidate="txtNewPhone"
                                            ErrorMessage="Please enter a Phone Number" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE5" TargetControlID="rvNewtxtNewPhone"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <br />
                                        <asp:CustomValidator ID="CusttxtNewPhone" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewPhone"
                                            ClientValidationFunction="validateFields_cno" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fax">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFax" runat="server" Text='<%# Bind("Fax") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewFax" runat="server" Text='<%# Bind("Fax") %>' SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewtxtNewFax" Text="." runat="server" ControlToValidate="txtNewFax"
                                            ErrorMessage="Please enter a FAX" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE6" TargetControlID="rvNewtxtNewFax"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <br />
                                        <asp:CustomValidator ID="CusttxtNewFax" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewFax"
                                            ClientValidationFunction="validateFields_phone" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax") %>' SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtContactPerson" runat="server" Text='<%# Bind("Contact_Person") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewContactPerson" runat="server" Text='<%# Bind("Contact_Person") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewtxtNewContactPerson" Text="." runat="server"
                                            ControlToValidate="txtNewContactPerson" ErrorMessage="Please enter a Contact Person"
                                            ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="VCE7" TargetControlID="rvNewtxtNewContactPerson"
                                            PopupPosition="TopLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <br />
                                        <asp:CustomValidator ID="CusttxtNewContactPerson" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="BranchAddGroup" ControlToValidate="txtNewContactPerson"
                                            ClientValidationFunction="validateFields_cnp" SetFocusOnError="true"></asp:CustomValidator>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactPerson" runat="server" Text='<%# Bind("Contact_Person") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chart of Account">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtChartOfAccount" Enabled="false" runat="server" Text='<%# Bind("Chart_of_Account_Code") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <uc1:ChartAccount runat="server" ID="CAEditBranch" OnSearchImageClicked="CAEditBranch_SearchImageClicked" />
                                        <asp:RequiredFieldValidator ID="ReqtxtChartOfAccount" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                            ControlToValidate="txtChartOfAccount" runat="server" ErrorMessage="Chart Of Account is required."
                                            Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="ReqtxtChartOfAccount"
                                            PopupPosition="Right" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewChartOfAccount" Enabled="false" runat="server" Text='<%# Bind("Chart_of_Account_Code") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <uc1:ChartAccount runat="server" ID="CABranch" OnSearchImageClicked="CABranch_SearchImageClicked" />
                                        <asp:RequiredFieldValidator ID="ReqtxtNewChartOfAccount" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                            ControlToValidate="txtNewChartOfAccount" runat="server" Text="." ErrorMessage="Chart Of Account is required."></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="ReqtxtNewChartOfAccount"
                                            PopupPosition="BottomLeft" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblChartOfAccount" runat="server" Text='<%# Bind("Chart_of_Account_Code") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            SkinID="GridViewLinkButton">
                                            <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            ValidationGroup="BranchEditGroup" SkinID="GridViewLinkButton">
                                            <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            SkinID="GridViewLinkButton">
                                            <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="BranchAddGroup"
                                            SkinID="GridViewButton" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_Branch.ClientID%>', null, null);
        }
    </script>
</asp:Content>
