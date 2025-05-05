<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="IMPALWeb.Security.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
 <asp:UpdatePanel ID="upRoles" runat="server">
  <ContentTemplate>     
 
    <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" 
          DataSourceID="odRoles" AllowPaging="True"   
          HorizontalAlign="Left" BackColor="White" 
          BorderStyle="None" BorderWidth="1px" 
          CellPadding="3" CaptionAlign="Left" 
          ondatabinding="gvRoles_DataBinding"  ShowFooter="True" 
          onselectedindexchanged="gvRoles_SelectedIndexChanged" 
          onrowcommand="gvRoles_RowCommand" 
          onrowupdating="gvRoles_RowUpdating" 
          SkinID="GridView" DataKeyNames="RoleID">
    
          <Columns>
           
         
         <asp:TemplateField   HeaderText="Role ID" Visible="False" >
               
                <ItemTemplate>
                    <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("RoleID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
         <asp:TemplateField HeaderText="Role Name" >
                 <ItemTemplate>
                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtRoleName" runat="server" 
                        Text='<%# Bind("RoleName") %>' Wrap="False" Width="241px"></asp:TextBox>
                     
               
                    <asp:RequiredFieldValidator ID="rvRoleName" runat="server" 
                        ControlToValidate="txtRoleName" ErrorMessage="Please enter a valid Role Name"
                         validationgroup="RoleEditGroup">
                     </asp:RequiredFieldValidator>
               
                </EditItemTemplate>
                <FooterTemplate   >
                    <asp:TextBox ID="txtNewRoleName" runat="server" Wrap="False"></asp:TextBox>

               
                    <asp:RequiredFieldValidator ID="rvNewRoleName" runat="server" 
                        ControlToValidate="txtNewRoleName" ErrorMessage="Please enter a valid Role Name" 
                         validationgroup="RoleAddGroup" >
                        </asp:RequiredFieldValidator>
               
                </FooterTemplate>
                <FooterStyle Wrap="False" />
 
            </asp:TemplateField>
            
         <asp:TemplateField   HeaderText="Role Code" >
               
                <ItemTemplate>
                    <asp:Label ID="lblRoleCode" runat="server" Text='<%# Bind("RoleCode") %>'></asp:Label>
                </ItemTemplate>
                 <FooterTemplate   >
                    <asp:TextBox ID="txtNewRoleCode" runat="server" Wrap="False" MaxLength="4"></asp:TextBox>

               
                    <asp:RequiredFieldValidator ID="rvNewRoleCode" runat="server" 
                        ControlToValidate="txtNewRoleCode" ErrorMessage="Please enter a valid Role Code" 
                         validationgroup="RoleAddGroup" >
                        </asp:RequiredFieldValidator>
               
                </FooterTemplate>
                <FooterStyle Wrap="False" />
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Active" Visible="False" >
           
                
                <ItemTemplate>
                    <asp:Label ID="lblActive" runat="server" Text='<%# Bind("Active") %>'>
                   
                </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            
             
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="btEdit" runat="server"   CausesValidation="False"  CommandName="Edit">
                   
                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png"/>
                         
                         </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btUpdate" runat="server"   CausesValidation="True"  CommandName="Update" 
                     validationgroup="RoleGroup">
                         <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png"/>
                         </asp:LinkButton>
                    
                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"  >
                         <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png"/>
                        </asp:LinkButton>
                </EditItemTemplate>
                
                   <FooterTemplate>
                       <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert"  ValidationGroup="RoleGroup"/>
                 </FooterTemplate>
            </asp:TemplateField>
            
             
        </Columns>
          
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            
            
           
    </asp:GridView>
    
     <asp:ObjectDataSource ID="odRoles" runat="server"  InsertMethod="AddNewRole" 
                        SelectMethod="GetAllRoles" TypeName="IMPALLibrary.Roles" 
                        oninserting="odRoles_Inserting" 
                        UpdateMethod="UpdateRole"  > 
         <UpdateParameters>
             <asp:Parameter Name="RoleID" Type="Int16" />
             <asp:Parameter Name="RoleName" Type="String" />
             <asp:Parameter Name="Active" Type ="Boolean" />
         </UpdateParameters>
         <InsertParameters>
              <asp:Parameter Name="RoleName" Type="String" />
             <asp:Parameter Name="RoleCode" Type="String" />
           
         </InsertParameters>
    </asp:ObjectDataSource>
                    
     </ContentTemplate>     

 </asp:UpdatePanel> 
</asp:Content>
