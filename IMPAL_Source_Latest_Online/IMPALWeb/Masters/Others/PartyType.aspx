<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="PartyType.aspx.cs" Inherits="IMPALWeb.Masters.Others.PartyType" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Party Type Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ODSPartyType" runat="server" 
                                oninserting="ODSPartyType_Inserting" InsertMethod="AddNewPartyTypeMaster" 
                                SelectMethod="GetAllPartyTypes" TypeName="IMPALLibrary.PartyTypeMaster" 
                                UpdateMethod="UpdatePartyTypeMaster">
                                <UpdateParameters>
                                    <asp:Parameter Name="PartyTypeCode" Type="String" />
                                    <asp:Parameter Name="PartyTypeDesc" Type="String" />
                                    <asp:Parameter Name="PartyTypeDbAccount" Type="String" />
                                    <asp:Parameter Name="PartyTypeCrAccount" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="PartyTypeCode" Type="String" />
                                    <asp:Parameter Name="PartyTypeDesc" Type="String" />
                                    <asp:Parameter Name="PartyTypeDbAccount" Type="String" />
                                    <asp:Parameter Name="PartyTypeCrAccount" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdPartyType" runat="server" AllowPaging="True" ShowFooter="True"
                                AutoGenerateColumns="False" SkinID="GridViewScroll" 
                                DataSourceID="ODSPartyType" onrowcommand="grdPartyType_RowCommand">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText = "Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" runat="server" Text='<%#Bind("PartyTypeCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPartyCode" runat="server" Text='<%#Bind("PartyTypeCode") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldPartyCode" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtPartyCode" SetFocusOnError="true"
                                                ErrorMessage="Party Type code should not be null" ></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="lblPartyEditDesc" runat="server" Text='<%# Bind("PartyTypeDesc") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldPartyDesc" ValidationGroup="validateEdit"
                                                runat="server" ForeColor="Red" ControlToValidate="lblPartyEditDesc" SetFocusOnError="true"
                                                ErrorMessage="Description should not be null" ></asp:RequiredFieldValidator>  
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyDesc" runat="server" Text='<%#Bind("PartyTypeDesc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPartyDesc" runat="server" Text='<%#Bind("PartyTypeDesc")%>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldPartyDesc" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtPartyDesc" SetFocusOnError="true"
                                                ErrorMessage="Description should not be null" ></asp:RequiredFieldValidator>                                            
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "Debit Chart of Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAccount" runat="server" Text='<%#Bind("PartyTypeDbAccount")%>' ></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDebitAccount" runat="server" Text='<%#Bind("PartyTypeDbAccount")%>' Enabled="false" ></asp:TextBox>
                                            <br />
                                            <Account:ChartAccount ID="ucDebitAccount" runat="server" OnSearchImageClicked="ucDebitChartAccount_SearchImageClicked" />
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldDebitAccount" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtDebitAccount" SetFocusOnError="true"
                                                ErrorMessage="Debit chart of account should be selected" ></asp:RequiredFieldValidator>                                            
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "Credit Chart of Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAccount" runat="server" Text='<%#Bind("PartyTypeCrAccount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCreditAccount" runat="server" Text='<%#Bind("PartyTypeCrAccount")%>' Enabled="false"></asp:TextBox>
                                            <br />
                                            <Account:ChartAccount ID="ucCreditAccount" runat="server" OnSearchImageClicked="ucCreditChartAccount_SearchImageClicked" />
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldCreditAccount" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtCreditAccount" SetFocusOnError="true"
                                                ErrorMessage="Credit chart of account should be selected"></asp:RequiredFieldValidator>                                            
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
                                                ValidationGroup="validateEdit">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="validate" />
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
