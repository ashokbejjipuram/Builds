<%@ Page Title="Sales TAX" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BranchSalesTaxForms.aspx.cs" Inherits="IMPALWeb.BranchSalesTaxForms" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            arguments.IsValid = true;
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character Should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                //if (firstchr == " ") {
                //source.innerHTML = "Should not be Empty";
                //arguments.IsValid = false;
                //}
                //else {
                arguments.IsValid = true;
                //}
            }

            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }


        }
    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Branch Sales Tax Form
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesTaxDesc" Text="SalesTax Description" SkinID="LabelNormal"
                                runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpBSTDesc" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="drpBSTDesc_SelectedIndexChanged">
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
                    Branch Sales Tax Form
                </div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" ShowFooter="true"
                        AutoGenerateColumns="False" SkinID="GridViewScroll" OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand" OnDataBound="GV_GLMaster_DataBound">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                                <EditItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" readonly="true" Text='<%# Bind("BranchCode") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchCode")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpBranch" SkinID="DropDownListNormal" runat="server" TabIndex="0">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvdrpBranch" Display="Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="drpBranch" ErrorMessage="Branch should not be null"
                                        ValidationGroup="BSTAddGroup">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SerialNumber" SortExpression="SerialNumber">
                                <FooterTemplate>
                                    <asp:Label ID="lblSrNo" runat="server" Text="" SkinID="GridViewLabel">
                                    </asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Bind("Serialnumber") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier/Dealer">
                                <FooterTemplate>
                                    <asp:RadioButton ID="rbSupplierIndicator" runat="server" GroupName="SalesTaxSupplier"
                                        SkinID="GridViewRadioButton" Text="Supplier" Checked="True" TabIndex="1" />
                                    &nbsp;<br />
                                    <asp:RadioButton ID="rbDealerIndicator" runat="server" GroupName="SalesTaxSupplier"
                                        SkinID="GridViewRadioButton" Text="Dealer" TabIndex="2" />
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierSalesTax" runat="server" Text='<%# Bind("SupplierDealerIndicator") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive/Issue">
                                <FooterTemplate>
                                    <asp:RadioButton ID="rbReceiveIndicator" runat="server" GroupName="SalesTaxReceive"
                                        SkinID="GridViewRadioButton" Text="Receive" Checked="True" TabIndex="3" />
                                    &nbsp;<br />
                                    <asp:RadioButton ID="rbLocalIndicator" runat="server" GroupName="SalesTaxReceive"
                                        SkinID="GridViewRadioButton" Text="Issue" TabIndex="4" />
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiveSalesTax" runat="server" Text='<%# Bind("ReceiveIssueIndicator") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ST Form Name " SortExpression="ST Form Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSTForm" runat="server" Text='<%# Bind("STFormName") %>' SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvSTForm" runat="server" ControlToValidate="txtSTForm"
                                        ErrorMessage="Please enter a valid SalesTax Form " ValidationGroup="BSTEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CustomValidator ID="CustEditBSTAddGroup" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="BSTEditGroup" ControlToValidate="txtSTForm"
                                        ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSTForm" runat="server" Text='<%# Bind("STFormName") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewSTForm" runat="server" Wrap="False" SkinID="GridViewTextBox"
                                        TabIndex="5">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewSTForm" runat="server" ControlToValidate="txtNewSTForm"
                                        ErrorMessage="Please enter a valid SalesTax Form" ValidationGroup="BSTAddGroup"
                                        SkinID="GridViewLabelError" Display="Dynamic" SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CustomValidator ID="CustNewBSTAddGroup" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="BSTAddGroup" ControlToValidate="txtNewSTForm"
                                        ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                </FooterTemplate>
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
                                        ValidationGroup="BSTEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="BSTAddGroup"
                                        SkinID="GridViewButton" TabIndex="6" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', null, null);
        }
    </script>
</asp:Content>
