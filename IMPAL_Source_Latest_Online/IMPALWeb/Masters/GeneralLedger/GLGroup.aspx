<%@ Page Title="GL Group Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GLGroup.aspx.cs" Inherits="IMPALWeb.GLGroup" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
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
    <table>
        <tr>
            <td>
             
                <div class="subFormTitle">
                    GL Group</div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGlClassification" SkinID="LabelNormal" runat="server" Text="Gl Classification"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="drpGLClassification" AutoPostBack="true" runat="server" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="drpGLClassification_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                </td>
                                <td class="inputcontrols">
                                </td>
                                <td class="label">
                                </td>
                                <td class="inputcontrols">
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            GL Group Details</div>
                        <asp:GridView ID="GV_GLGroup" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3"
                            HorizontalAlign="Left" ShowFooter="True" SkinID="GridView" OnRowCancelingEdit="GV_GLGroup_RowCancelingEdit"
                            OnRowCommand="GV_GLGroup_RowCommand" OnRowCreated="GV_GLGroup_RowCreated" OnRowEditing="GV_GLGroup_RowEditing"
                            OnRowUpdating="GV_GLGroup_RowUpdating" OnPageIndexChanging="GV_GLGroup_PageIndexChanging">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="GL Code" SortExpression="GLCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGLCode" runat="server" Text='<%# Bind("GLCode") %>'></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:TextBox ID="txtNewGLCode" runat="server" Wrap="False"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewGLCode" Display = "Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewGLCode"
                                            ErrorMessage="enter a valid GLCode " ValidationGroup="GLAddGroup">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                        <asp:CustomValidator ID="CustNewGLCode" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="GLAddGroup" ControlToValidate="txtNewGLCode"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                        
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                            Width="241px" Wrap="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvGLDescription" runat="server" ControlToValidate="txtDescription"
                                            ErrorMessage="Please enter a valid GL Group Description" ValidationGroup="GLEditGroup">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewDescription" runat="server" Wrap="False"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvNewDesc" Display = "Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewDescription"
                                            ErrorMessage="Please enter a valid GL Group Description" ValidationGroup="GLAddGroup">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                         <%--<asp:RequiredFieldValidator ID="rvNewGLCLDesc" Display="Dynamic" SetFocusOnError="true"
                                                SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewGLCLDescription"
                                                ErrorMessage="Please Enter GL Classifcation Description" ValidationGroup="GLCLAddGroup">
                                            </asp:RequiredFieldValidator>--%>
                                            
                                            <asp:CustomValidator ID="CustNewDescription" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="GLAddGroup" ControlToValidate="txtNewDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                                
                                        
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GL Classification" SortExpression="ClassificationDesc">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGLClassification" runat="server"> </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rvddlGLClassification" Display = "Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="ddlGLClassification"
                                            ErrorMessage="Please enter a valid GL Classification " ValidationGroup="GLAddGroup">
                                        </asp:RequiredFieldValidator>
                                        <br />    
                                         <asp:CustomValidator ID="CustNewddlGLClassification" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="GLAddGroup" ControlToValidate="ddlGLClassification"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>                                        
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGLClassification" runat="server" Text='<%# Bind("ClassificationDesc") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                            <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                            ValidationGroup="GLEditGroup">
                                            <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                            <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btAdd" runat="server" CommandName="Insert" Text="Add" ValidationGroup="GLAddGroup" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div class="transactionButtons">
        <div class="transactionButtonsHolder">
            <asp:Button ID="btnReport" runat="server" SkinID="ButtonViewReport" Text="Generate Report"
                OnClick="btnReport_Click" />
        </div>
    </div>
</asp:Content>
