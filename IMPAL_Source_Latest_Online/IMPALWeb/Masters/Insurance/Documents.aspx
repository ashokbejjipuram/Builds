<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Documents.aspx.cs" Inherits="IMPALWeb.Documents" %>

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

        } 

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Documents
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Documents" runat="server" AutoGenerateColumns="False" DataSourceID="ODSDocuments"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Documents_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Documents_SelectedIndexChanged"
                                OnRowCommand="GV_Documents_RowCommand" OnRowUpdating="GV_Documents_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabelEmptyRow" Text="No records to Display"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                                MaxLength="40" Wrap="False" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvDescription" runat="server" ControlToValidate="txtDescription"
                                                Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter Description"
                                                ValidationGroup="DescEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValShortDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="DescEditGroup" ControlToValidate="txtDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDescription" Width="241px" MaxLength="40" runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rvNewDescription" runat="server"
                                                ControlToValidate="txtNewDescription" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="DescAddGroup" ControlToValidate="txtNewDescription"
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
                                                ValidationGroup="DescEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" SkinID="GridViewButtonFooter" Text="Add" CommandName="Insert"
                                                ValidationGroup="DescAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ODSDocuments" runat="server" InsertMethod="AddNewDocuments"
                    SelectMethod="GetAllDocuments" TypeName="IMPALLibrary.Documents" OnInserting="ODSdocuments_Inserting"
                    UpdateMethod="UpdateBank">
                    <UpdateParameters>
                        <asp:Parameter Name="Code" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Description" Type="String" />
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
