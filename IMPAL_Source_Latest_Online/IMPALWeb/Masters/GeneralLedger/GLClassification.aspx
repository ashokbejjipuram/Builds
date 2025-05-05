<%@ Page Title="GL Classification" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GLClassification.aspx.cs" Inherits="IMPALWeb.GLClassification" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
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
            GL Classification
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_GLClassification" runat="server" AutoGenerateColumns="False"
                                DataSourceID="ObjectDataGLCL" AllowPaging="True" HorizontalAlign="Left" BackColor="White"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_GLClassification_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_GLClassification_SelectedIndexChanged1"
                                OnRowCommand="GV_GLClassification_RowCommand" OnRowUpdating="GV_GLClassification_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblGLCLCode" runat="server" Text='<%# Bind("GLCLCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblGLCLDescription" runat="server" Text='<%# Bind("GLCLDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" Width="241px" MaxLength="40" ID="txtGLCLDescription"
                                                runat="server" Text='<%# Bind("GLCLDescription") %>' Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvGLCLDescription" runat="server" ControlToValidate="txtGLCLDescription"
                                                SkinID="GridViewLabelError" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please Enter GL Classifcation Description"
                                                ValidationGroup="GLCLEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValEditDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="GLCLEditGroup" ControlToValidate="txtGLCLDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewGLCLDescription" MaxLength="40" Width="241px" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewGLCLDesc" Display="Dynamic" SetFocusOnError="true"
                                                SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewGLCLDescription"
                                                ErrorMessage="Please Enter GL Classifcation Description" ValidationGroup="GLCLAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValAddDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="GLCLAddGroup" ControlToValidate="txtNewGLCLDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
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
                                                ValidationGroup="GLCLEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="GLCLAddGroup" />
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
                <asp:ObjectDataSource ID="ObjectDataGLCL" runat="server" InsertMethod="AddNewGLClassifications"
                    SelectMethod="GetAllGLClassifications" TypeName="IMPALLibrary.GLClassifications"
                    OnInserting="ODSGLClassification_Inserting" UpdateMethod="UpdateGLClassification">
                    <UpdateParameters>
                        <asp:Parameter Name="GLCLCode" Type="String" />
                        <asp:Parameter Name="GLCLDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="GLCLDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
