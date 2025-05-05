<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="IMPALWeb.Security.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <asp:UpdatePanel ID="UPChangePassword" runat="server">
        <ContentTemplate>
            <div class="subFormTitle">
                Change Password</div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblUserName" runat="server" Text="Branch Name:" SkinID="LabelNormal"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtUserName" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOldPassword" runat="server" Text="Enter Old Password:" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtOldPassword" runat="server" SkinID="TextBoxNormal" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RFVOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            ErrorMessage="Please Enter Old Password" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CVOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            ErrorMessage="Old Password is not matching, Please Enter a Valid Password" Display="Dynamic"
                            OnServerValidate="VarifyPassword" ValidationGroup="ChangePassword"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNewPassword" runat="server" Text="Enter New Password:" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtNewPassword" runat="server" SkinID="TextBoxNormal" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RFVNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ErrorMessage="Please Enter New Password" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                        <asp:CompareValidator Type="String" Operator="NotEqual" ID="CVNewPassword" ControlToCompare="txtOldPassword"
                            ControlToValidate="txtNewPassword" runat="server" Text="New Password and Old Password are same, Please Enter a Different Password"
                            ValidationGroup="ChangePassword"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Please Enter Confirm Password" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CVChangePassword" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
                            runat="server" Text="New Password and Confirm Password are Not Matching" ValidationGroup="ChangePassword"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="inputcontrols">
                        <asp:Label ID="lblSucessMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"
                        SkinID="ButtonNormal" ValidationGroup="ChangePassword" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
