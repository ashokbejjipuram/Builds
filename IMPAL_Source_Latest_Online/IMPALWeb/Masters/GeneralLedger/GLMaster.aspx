<%@ Page Title="GL Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GLMaster.aspx.cs" Inherits="IMPALWeb.GLMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <!-- Template for Full Page GridView -->
    <!--
    <div id="DivOuter">
        <div class="subFormTitle">GL Master</div>        
        <div class="gridViewScrollFullPage">
            -- Gridview goes here ---
        </div>  
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
            </div>
        </div>
    </div>
    -->
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
            GL Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGlGroupDesc" Text="GlGroup Description" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpGlGroupDesc" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="drpGlGroupDesc_SelectedIndexChanged">
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
                    GL Master Details</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" ShowFooter="true"
                        AutoGenerateColumns="False" SkinID="GridView" OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="GLMasterCode" SortExpression="GLMasterCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLMasterCode" runat="server" Text='<%# Bind("GLMasterCode") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                        SkinID="GridViewTextBoxBig">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvGLMasterDesc" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="Please enter a valid GL Master Description" ValidationGroup="GLMasterEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewDescription" runat="server" Wrap="False" SkinID="GridViewTextBoxBig">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewDesc" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ControlToValidate="txtNewDescription"
                                        ErrorMessage="Enter a valid GL Master Description" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                     <asp:CustomValidator ID="CusttxtNewDescription" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="GLMasterAddGroup" ControlToValidate="txtNewDescription"
                                            ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GroupDescription">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddGLGroup" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                    
                                     <asp:RequiredFieldValidator ID="rvGLGroup" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ControlToValidate="ddGLGroup"
                                        ErrorMessage="Enter a valid GL Description" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                     <asp:CustomValidator ID="CusttxtNewGLGroup" SkinID="GridViewLabelError" runat="server"
                                            Display="Dynamic" ValidationGroup="GLMasterAddGroup" ControlToValidate="ddGLGroup"
                                            ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>  
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGLGroupDesc" runat="server" Text='<%# Bind("GroupDescription") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BalanceIndicator">
                                <FooterTemplate>
                                    <asp:RadioButton ID="rbCreditIndicator" runat="server" GroupName="BalanceIndicator"
                                        Text="Credit" SkinID="GridViewRadioButton" Checked="True" />
                                    &nbsp;<br />
                                    <asp:RadioButton ID="rbdebitIndicator" runat="server" GroupName="BalanceIndicator"
                                        Text="Debit" skinID="GridViewRadioButton" />
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBalanceIndicator" runat="server" Text='<%# Bind("BalanceIndicator") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        ValidationGroup="GLMasterEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewButton" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', null, null);
        }
    </script>
</asp:Content>
