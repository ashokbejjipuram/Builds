<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="IMPALWeb.Security.UserRoleMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<asp:UpdatePanel ID="upRoles" runat="server">
  <ContentTemplate>     
 
    <asp:GridView ID="gvUserRole" runat="server" AutoGenerateColumns="False" 
          DataSourceID="odUsers" AllowPaging="True"   
          HorizontalAlign="Left" BackColor="White" 
          BorderStyle="None" BorderWidth="1px" 
          CellPadding="3" CaptionAlign="Left" 
          ondatabinding="gvUserRole_DataBinding"   
          onselectedindexchanged="gvUserRole_SelectedIndexChanged" 
          onrowcommand="gvUserRole_RowCommand" 
          onrowupdating="gvUserRole_RowUpdating" 
          SkinID="GridView" DataKeyNames="UserID"  ShowFooter="false">
    
          <Columns>
           
         
            <asp:TemplateField   HeaderText="User ID" Visible="False" >
               
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Branch Name" >
                 <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtUserName" runat="server" 
                        Text='<%# Bind("UserName") %>' Wrap="False"  >
                        </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvUserName" runat="server" 
                        ControlToValidate="txtUserName" ErrorMessage="Please enter a valid User Name"
                         validationgroup="UserEditGroup">
                     </asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            
         <asp:TemplateField   HeaderText="Branch Code" >
               
                <ItemTemplate>
                    <asp:Label ID="lblBranchCode" runat="server" Text='<%# Bind("BranchCode") %>'></asp:Label>
                </ItemTemplate>
                 
                
            </asp:TemplateField>
        <asp:TemplateField HeaderText="User Role" >
            <ItemTemplate>
             <asp:Label ID="lblUserRole" runat="server" Text='<%# Bind("RoleName") %>'>
               
             </asp:Label>
             </ItemTemplate>
                
             <EditItemTemplate>
                    <asp:DropDownList ID="ddUserRole" runat="server" 
                            DataSourceID="odRoles" DataTextField="RoleName" 
                            DataValueField="RoleID"  SelectedValue='<%#Bind("RoleID")%>'>
                     </asp:DropDownList>
               </EditItemTemplate>    
            </asp:TemplateField>    
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="btEdit" runat="server"   CausesValidation="False"  CommandName="Edit">
                   
                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png"/>
                         
                         </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btUpdate" runat="server"   CausesValidation="True"  CommandName="Update" 
                     validationgroup="UserGroup">
                         <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png"/>
                         </asp:LinkButton>
                    
                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"  >
                         <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png"/>
                        </asp:LinkButton>
                </EditItemTemplate>
                 
            </asp:TemplateField>
            
             
        </Columns>
          
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
           
    </asp:GridView>
     <asp:ObjectDataSource ID="odRoles" runat="server"  
           SelectMethod="GetAllRoles" TypeName="IMPALLibrary.Roles" > 
    </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="odUsers" runat="server"   
            SelectMethod="GetAllUsersDetails" TypeName="IMPALLibrary.Users" 
            UpdateMethod="UpdateUserDetails"  > 
         <UpdateParameters>
             <asp:Parameter Name="UserID" Type="Int16" />
             <asp:Parameter Name="UserName" Type="String" />
             <asp:Parameter Name="RoleID" Type ="String" />
         </UpdateParameters>
        
    </asp:ObjectDataSource>
                    
     </ContentTemplate>     

 </asp:UpdatePanel> 
</asp:Content>
