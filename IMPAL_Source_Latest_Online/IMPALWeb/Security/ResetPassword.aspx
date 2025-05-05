<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="IMPALWeb.Security.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
 
    <asp:UpdatePanel ID="UPChangePassword" runat="server">
  <ContentTemplate> 
  <div id="main" runat="server">
  <div class="subFormTitle" >Reset Password</div>
 
  <table class="subFormTable">
   <tr>
   
      <td class= "labelSubTitle">
        <asp:Label ID="lblUserName" runat="server" Text="Branch Name:" 
              SkinID="LabelNormal"    ></asp:Label>
      </td>
      <td  >
          <asp:DropDownList ID="ddUserList" runat="server" SkinID="DropDownListNormal" 
              DataSourceID="odsUsers" DataTextField="UserName" DataValueField="BranchCode">
          </asp:DropDownList> 
          <asp:ObjectDataSource ID="odsUsers" runat="server" 
              SelectMethod="GetAllUsersDetails" TypeName="IMPALLibrary.Users">
          </asp:ObjectDataSource>
      </td>
      </tr>
      
       <tr>
      <td class="label">
        <asp:Label ID="lblNewPassword" runat="server" Text="Enter New Password:" 
              SkinID= "LabelNormal"  ></asp:Label>
         <span class="asterix">*</span>
      </td>
      <td class="inputcontrols">
        <asp:TextBox ID="txtNewPassword" runat="server" SkinID="TextBoxNormal" 
              TextMode="Password" ></asp:TextBox>
          <br />
          <asp:RequiredFieldValidator ID="RFVNewPassword" runat="server" 
              ControlToValidate="txtNewPassword" 
              ErrorMessage="Please Enter New Password" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
      </td>
      </tr>
           <tr>
      <td class="label">
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" 
              SkinID="LabelNormal"    ></asp:Label>
         <span class="asterix">*</span>
      </td>
      <td class="inputcontrols">
        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" SkinID="TextBoxNormal" ></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RFVConfirmPassword" runat="server" 
              ControlToValidate="txtConfirmPassword" 
              ErrorMessage="Please Enter Confirm Password" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
              
        <asp:CompareValidator ID="CVChangePassword" ControlToCompare="txtNewPassword" 
              ControlToValidate="txtConfirmPassword" runat="server" 
              Text="New Password and Confirm Password are Not Matching" 
              ValidationGroup="ChangePassword"></asp:CompareValidator>
        </td>
        </tr>
          <tr>
        <td  class="inputcontrols">
        <asp:Label ID="lblSucessMessage"  runat="server" Text=""></asp:Label>
        </td>
        </tr>
        </table>
        
        <div class="transactionButtons">
      
        <div class="transactionButtonsHolder">
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"
                SkinID="ButtonNormal" ValidationGroup="ChangePassword" />
        </div>
        
  
    </div>
  
  </div>
 </ContentTemplate>   
  
  </asp:UpdatePanel>
</asp:Content>

