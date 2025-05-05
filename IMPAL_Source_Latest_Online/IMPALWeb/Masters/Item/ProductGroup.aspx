<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ProductGroup.aspx.cs" Inherits="IMPALWeb.ProductGroup" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
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
            Product Group
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_ProductGroup" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataPG"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_ProductGroup_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_ProductGroup_SelectedIndexChanged"
                                OnRowCommand="GV_ProductGroup_RowCommand" OnRowUpdating="GV_ProductGroup_RowUpdating"
                                SkinID="GridView" PageSize="10">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabel" Text="No Rows Returned"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%#Bind("ProductGroupCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblDescription" runat="server" Text='<%# Bind("ProductGroupDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPGDescription" runat="server" Text='<%# Bind("ProductGroupDescription") %>'
                                                Width="241px" MaxLength="40" Wrap="False"></asp:TextBox>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rvPGDescription" runat="server" ControlToValidate="txtPGDescription"
                                                Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter ProductGroup Description"
                                                SkinID="GridViewLabelError" ValidationGroup="PGEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValEditDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="PGEditGroup" ControlToValidate="txtPGDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox Width="241px" MaxLength="40" ID="txtNewPGDescription" runat="server"
                                                Wrap="False"></asp:TextBox>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rvNewPGDesc" runat="server" ControlToValidate="txtNewPGDescription"
                                                Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter ProductGroup Description"
                                                SkinID="GridViewLabelError" ValidationGroup="PGAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValAddDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="PGAddGroup" ControlToValidate="txtNewPGDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
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
                                                ValidationGroup="PGEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="PGAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ObjectDataPG" runat="server" InsertMethod="AddNewProductGroup"
                    SelectMethod="GetAllProductGroups" TypeName="IMPALLibrary.ProductGroups" OnInserting="ODSGV_ProductGroup_Inserting"
                    UpdateMethod="UpdateProductGroup">
                    <UpdateParameters>
                        <asp:Parameter Name="ProductGroupCode" Type="String" />
                        <asp:Parameter Name="ProductGroupDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ProductGroupDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
