<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Bank.aspx.cs" Inherits="IMPALWeb.Bank" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character Should be Alphabet or Number";
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
            Bank
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Bank" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataBank"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Bank_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Bank_SelectedIndexChanged" OnRowCommand="GV_Bank_RowCommand"
                                OnRowUpdating="GV_Bank_RowUpdating" SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblBank" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblBankCode" runat="server" Text='<%# Bind("BankCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBankName" runat="server" Text='<%# Bind("BankName") %>' Wrap="False"
                                                SkinID="GridViewTextBox" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SkinID="GridViewLabelError" Display="Dynamic" SetFocusOnError="true"
                                                ID="rvBankName" runat="server" ControlToValidate="txtBankName" ErrorMessage="Please Enter Bank Name"
                                                ValidationGroup="BankEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="BankEditGroup" ControlToValidate="txtBankName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Bank Name"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBankName" SkinID="GridViewTextBox" Width="241px" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SkinID="GridViewLabelError" Display="Dynamic" SetFocusOnError="true"
                                                ID="rvNewBankName" runat="server" ControlToValidate="txtNewBankName" ErrorMessage="Please Enter Bank Name"
                                                ValidationGroup="BankAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="BankAddGroup" ControlToValidate="txtNewBankName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Bank Name"></asp:CustomValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                ValidationGroup="BankEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" SkinID="GridViewButtonFooter" CommandName="Insert"
                                                ValidationGroup="BankAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ObjectDataBank" runat="server" InsertMethod="AddNewBanks"
                    SelectMethod="GetAllBanks" TypeName="IMPALLibrary.Banks" OnInserting="ODSBank_Inserting"
                    UpdateMethod="UpdateBank">
                    <UpdateParameters>
                        <asp:Parameter Name="BankCode" Type="String" />
                        <asp:Parameter Name="BankName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="BankName" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
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
