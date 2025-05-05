<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="TownMaster.aspx.cs" Inherits="IMPALWeb.TownMaster" %>

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
            Town</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Town" runat="server" AutoGenerateColumns="False" DataSourceID="ODSTown"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Town_DataBinding"
                                ShowFooter="True" OnRowCommand="GV_Town_RowCommand" SkinID="GridView" PageSize="100">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblClassification" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%# Bind("Towncode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtName" SkinID="GridViewTextBox" MaxLength="40" runat="server"
                                                Text='<%# Bind("TownName") %>' Wrap="False" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="GridViewLabelError" SetFocusOnError="true"
                                                ID="rvName" runat="server" ControlToValidate="txtName" ErrorMessage="Please Enter Name"
                                                ValidationGroup="TownEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="TownEditGroup" ControlToValidate="txtName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Name"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" Width="241px" MaxLength="40" ID="txtNewName"
                                                runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewName" Display="Dynamic" SkinID="GridViewLabelError"
                                                SetFocusOnError="true" runat="server" ControlToValidate="txtNewName" ErrorMessage="Please Enter Name"
                                                ValidationGroup="TownAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="TownAddGroup" ControlToValidate="txtNewName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Name"></asp:CustomValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" SkinID="GridViewLabel" runat="server" Text='<%# Bind("TownName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch">
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlTBranch" SkinID="GridViewDropDownListFooter" runat="server"
                                                DataSourceID="ObjectDataTBranch" DataTextField="BranchName" DataValueField="BranchCode">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvBranch" runat="server" ControlToValidate="ddlTBranch"
                                                SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Select Branch"
                                                InitialValue="0" ValidationGroup="TownAddGroup"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranchName" SkinID="GridViewLabel" runat="server" Text='<%# Bind("BrName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                ValidationGroup="TownEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="TownAddGroup" />
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
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report" style="display:none"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ODSTown" runat="server" SelectMethod="GetAllTowns" TypeName="IMPALLibrary.Towns"
                    InsertMethod="AddNewTowns" OnInserting="ODSTown_Inserting" OnUpdating="ODSTown_Updating"
                    UpdateMethod="UpdateTown">
                    <UpdateParameters>
                        <asp:Parameter Name="Towncode" Type="String" />
                        <asp:Parameter Name="TownName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="TownName" Type="String" />
                        <asp:Parameter Name="BrCode" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataTBranch" runat="server" SelectMethod="GetAllBranch"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
