<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClassificationDocuments.aspx.cs" Inherits="IMPALWeb.ClassificationDocuments" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle subFormTitleExtender250">
            Classification Documents</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Documents" runat="server" AutoGenerateColumns="False" DataSourceID="ODSDocuments1"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Documents_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Documents_SelectedIndexChanged"
                                OnRowCommand="GV_Documents_RowCommand" OnRowUpdating="GV_Documents_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("classificationCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCLCode" runat="server" Text='<%#Bind("classificationCode")%>'
                                                Wrap="False" Width="90px" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvtxtCLCode" runat="server" ControlToValidate="txtCLCode"
                                                ErrorMessage="Code Required" ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Classification" SortExpression="Classification">
                                        <%--<EditItemTemplate>
                    <asp:TextBox ID="txtClassifiction" runat="server" 
                        Text='<%# Bind("classification") %>' Wrap="False" Width="241px"></asp:TextBox>
               
                    <asp:RequiredFieldValidator ID="rvtxtClassifiction" runat="server" 
                        ControlToValidate="txtClassifiction" ErrorMessage="Enter a valid Classification"
                         validationgroup="DescEditGroup">
                     </asp:RequiredFieldValidator>
               
                </EditItemTemplate>--%>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlclassification" runat="server" DataSourceID="ODSclassification"
                                                DataTextField="Description" DataValueField="Code" OnSelectedIndexChanged="ddlclassification_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblclassification" runat="server" Text='<%# Bind("classification") %>'
                                                readonly="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc.Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocCode" runat="server" Text='<%# Bind("documentCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDocCode" runat="server" Text='<%#Bind("documentCode")%>' Wrap="False"
                                                Width="90px" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvtxtDocCode" runat="server" ControlToValidate="txtDocCode"
                                                ErrorMessage="Code Required" ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documents">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("documents") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("documents") %>' Wrap="False"
                                                Width="241px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvDescription" runat="server" ControlToValidate="txtDescription"
                                                ErrorMessage="Please enter a valid Description" ValidationGroup="DescEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDescription" runat="server" AutoPostBack="true" DataSourceID="ODSDocments"
                                                DataTextField="Description" DataValueField="Code" OnSelectedIndexChanged="ddlDescription_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlDescription" runat="server" DataSourceID="ODSDocments" DataTextField="Description"
                                                DataValueField="Code" OnSelectedIndexChanged="ddlDescription_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="50px" SelectedValue='<%#Bind("status")%>'>
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="50px">
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png"/>
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
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="DescAddGroup" />
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
                <asp:ObjectDataSource ID="ODSclassification" runat="server" SelectMethod="GetAllClassifications"
                    TypeName="IMPALLibrary.Classifications"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSDocments" runat="server" SelectMethod="GetAllDocuments"
                    TypeName="IMPALLibrary.Documents"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSDocuments1" runat="server" InsertMethod="AddNewClassificationDocuments"
                    SelectMethod="GetAllClassificationDocuments" TypeName="IMPALLibrary.ClassificationDocuments"
                    OnInserting="ODSdocuments1_Inserting" UpdateMethod="UpdateClassificationDocuments">
                    <UpdateParameters>
                        <asp:Parameter Name="classificationCode" Type="String" />
                        <asp:Parameter Name="classification" Type="String" />
                        <asp:Parameter Name="documentCode" Type="String" />
                        <asp:Parameter Name="documents" Type="String" />
                        <asp:Parameter Name="status" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="classificationCode" Type="String" />
                        <asp:Parameter Name="classification" Type="String" />
                        <asp:Parameter Name="documentCode" Type="String" />
                        <asp:Parameter Name="documents" Type="String" />
                        <asp:Parameter Name="status" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
