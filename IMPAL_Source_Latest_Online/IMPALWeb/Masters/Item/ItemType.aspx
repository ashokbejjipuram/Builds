<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ItemType.aspx.cs" Inherits="IMPALWeb.ItemType" %>

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
            Item Type
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_ItemType" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataIT"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_ItemType_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_ItemType_SelectedIndexChanged" OnRowCommand="GV_ItemType_RowCommand"
                                OnRowUpdating="GV_ItemType_RowUpdating" SkinID="GridView" PageSize="10">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabel" Text="No Rows Returned"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%# Bind("ItemTypeCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblDescription" runat="server" Text='<%# Bind("ItemTypeDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemTypeDescription" SkinID="GridViewTextBox" runat="server"
                                                Text='<%# Bind("ItemTypeDescription") %>' Wrap="False" Width="241px"></asp:TextBox>
                                                <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="rvItemTypeDescription" runat="server"
                                                ControlToValidate="txtItemTypeDescription" SkinID="GridViewLabelError" ErrorMessage="Please Enter Item Type Description"
                                                SetFocusOnError="true" ValidationGroup="ItemTypeEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValEditDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="ItemTypeEditGroup" ControlToValidate="txtItemTypeDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox Width="241px" SkinID="GridViewTextBox" ID="txtNewItemTypeDescription"
                                                runat="server" Wrap="False"></asp:TextBox>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rvNewItemTypeDesc" runat="server" ControlToValidate="txtNewItemTypeDescription"
                                                SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Enter Item Type Description"
                                                SetFocusOnError="true" ValidationGroup="ItemTypeAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValAddDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="ItemTypeAddGroup" ControlToValidate="txtNewItemTypeDescription"
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
                                                ValidationGroup="ItemTypeEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button SkinID="GridViewButtonFooter" ID="btAdd" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="ItemTypeAddGroup" />
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
                <asp:ObjectDataSource ID="ObjectDataIT" runat="server" InsertMethod="AddNewItemType"
                    SelectMethod="GetAllItemTypes" TypeName="IMPALLibrary.ItemTypes" OnInserting="ODSGV_ItemType_Inserting"
                    UpdateMethod="UpdateItemType">
                    <UpdateParameters>
                        <asp:Parameter Name="ItemTypeCode" Type="String" />
                        <asp:Parameter Name="ItemTypeDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ItemTypeDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
