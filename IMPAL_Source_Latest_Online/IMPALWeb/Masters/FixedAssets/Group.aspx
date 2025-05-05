<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Group.aspx.cs" Inherits="IMPALWeb.Group" %>

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

        } 

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Group
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Group" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataGroup"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Group_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Group_SelectedIndexChanged" OnRowCommand="GV_Group_RowCommand"
                                OnRowUpdating="GV_Group_RowUpdating" SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" Text="No Records to Display" SkinID="GridViewLabel"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%# Bind("GLCLCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblDescription" runat="server" Text='<%# Bind("GLCLDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" ID="txtDescription" runat="server" Text='<%# Bind("GLCLDescription") %>'
                                                MaxLength="40" Wrap="False" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic"
                                                ID="rvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please Enter Description"
                                                ValidationGroup="FAEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="FAEditGroup" ControlToValidate="txtDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="40" SkinID="GridViewTextBox" Width="241px" ID="txtNewDescription"
                                                runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="Dynamic" SkinID="GridViewLabelError"
                                                ID="rvNewDescription" runat="server" ControlToValidate="txtNewDescription" ErrorMessage="Please Enter Description"
                                                ValidationGroup="FAAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="FAAddGroup" ControlToValidate="txtNewDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
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
                                                ValidationGroup="FAEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="FAAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ObjectDataGroup" runat="server" InsertMethod="AddNewGLFixedGroups"
                    SelectMethod="GetAllGLFixedGroups" TypeName="IMPALLibrary.GLFixedAssetGroups"
                    OnInserting="ODSGroup_Inserting" UpdateMethod="UpdateGLFixedGroup">
                    <UpdateParameters>
                        <asp:Parameter Name="GLCLCode" Type="String" />
                        <asp:Parameter Name="GLCLDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="GLCLDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
