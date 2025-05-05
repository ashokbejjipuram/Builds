<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ClearingAgent.aspx.cs" Inherits="IMPALWeb.Masters.Others.ClearingAgent" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Clearing Agent Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ODSClearingAgent" runat="server" InsertMethod="AddNewClearingAgentMaster"
                                OnInserting="ODSClearingAgent_Inserting" SelectMethod="GetAllClearingAgent" TypeName="IMPALLibrary.ClearingAgentMaster"
                                UpdateMethod="UpdateClearingAgentMaster">
                                <UpdateParameters>
                                    <asp:Parameter Name="BranchCode" Type="String" />
                                    <asp:Parameter Name="AgentCode" Type="String" />
                                    <asp:Parameter Name="AgentName" Type="String" />
                                    <asp:Parameter Name="Address" Type="String" />
                                    <asp:Parameter Name="Phone" Type="String" />
                                    <asp:Parameter Name="Fax" Type="String" />
                                    <asp:Parameter Name="EMail" Type="String" />
                                    <asp:Parameter Name="ContactPerson" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="BranchCode" Type="String" />
                                    <asp:Parameter Name="AgentCode" Type="String" />
                                    <asp:Parameter Name="AgentName" Type="String" />
                                    <asp:Parameter Name="Address" Type="String" />
                                    <asp:Parameter Name="Phone" Type="String" />
                                    <asp:Parameter Name="Fax" Type="String" />
                                    <asp:Parameter Name="EMail" Type="String" />
                                    <asp:Parameter Name="ContactPerson" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSBranchList" runat="server" SelectMethod="GetBranchList"
                                TypeName="IMPALLibrary.ClearingAgentMaster"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdClearingAgent" runat="server" AllowPaging="True" ShowFooter="True"
                                AutoGenerateColumns="False" SkinID="GridViewScroll" DataSourceID="ODSClearingAgent"
                                OnRowCommand="grdClearingAgent_RowCommand">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgentCode" runat="server" Text='<%#Bind("AgentCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAgentCode" runat="server" Text='<%#Bind("AgentCode") %>'></asp:TextBox>
                                        </FooterTemplate>
--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgentName" runat="server" Text='<%#Bind("AgentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditAgentName" runat="server" Text='<%#Bind("AgentName") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldEditAgentName" ValidationGroup="EditValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtEditAgentName" SetFocusOnError="true"
                                                ErrorMessage="Agent Name should not be null"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAgentName" runat="server" Text='<%#Bind("AgentName") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldAddAgentName" ValidationGroup="InsertValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtAgentName" SetFocusOnError="true"
                                                ErrorMessage="Agent Name should not be null"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch of operation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranchCode" runat="server" Text='<%#Bind("BranchCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEditBranch" runat="server" DataSourceID="objDSBranchList"
                                                DataTextField="BrName" DataValueField="BrCode" SelectedValue='<%#Bind("BranchCode")%>'>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldEditBranch" ValidationGroup="EditValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlEditBranch" SetFocusOnError="true"
                                                ErrorMessage="Branch should not be null" InitialValue="0"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="objDSBranchList" DataTextField="BrName"
                                                DataValueField="BrCode">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldAddBranch" ValidationGroup="InsertValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlBranch" SetFocusOnError="true"
                                                ErrorMessage="Branch should not be null" InitialValue="0"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%#Bind("Address")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditAddress" runat="server" Text='<%#Bind("Address") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldEditAddress" ValidationGroup="EditValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtEditAddress" SetFocusOnError="true"
                                                ErrorMessage="Address should not be null"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAddress" runat="server" Text='<%#Bind("Address") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldAddAddress" ValidationGroup="InsertValidate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtAddress" SetFocusOnError="true"
                                                ErrorMessage="Address should not be null"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("Phone")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditPhone" runat="server" Text='<%#Bind("Phone") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPhone" runat="server" Text='<%#Bind("Phone") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server" Text='<%#Bind("Fax")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditFax" runat="server" Text='<%#Bind("Fax") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFax" runat="server" Text='<%#Bind("Fax") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="E-Mail">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server" Text='<%#Bind("EMail")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditEMail" runat="server" Text='<%#Bind("EMail") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEMail" runat="server" Text='<%#Bind("EMail") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" runat="server" Text='<%#Bind("ContactPerson")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditContactPerson" runat="server" Text='<%#Bind("ContactPerson") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtContactPerson" runat="server" Text='<%#Bind("ContactPerson") %>'></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditRemarks" runat="server" Text='<%#Bind("Remarks") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Bind("Remarks") %>'></asp:TextBox>
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
                                                ValidationGroup="EditValidate">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="InsertValidate" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
