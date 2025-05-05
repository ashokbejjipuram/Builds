<%@ Page Title="Sales Man Target Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ChartOfAccountMaster.aspx.cs" Inherits="IMPALWeb.ChartOfAccountMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript" >
    function validate_glclass(source, arguments) {
        var drpGLClassification = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLClassification").value;
        firstchr = drpGLClassification.substring(0, 1, drpGLClassification);
        if (drpGLClassification == "-1" || drpGLClassification == null) {
            source.innerHTML = "Classification should not be null";
            arguments.IsValid = false;
        }
    }
    function validate_glgroup(source, arguments) {
        var drpGLGroup = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLGroup").value;
        firstchr = drpGLGroup.substring(0, 1, drpGLGroup);
        schar = drpGLGroup.substring(1, 2, drpGLGroup);
        if (drpGLGroup == "-1" || drpGLGroup == null) {
            source.innerHTML = "GLGroup should not be null";
            arguments.IsValid = false;
        }
    }
    function validate_glmain(source, arguments) {
        var drpGLMain = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLMain").value;
        firstchr = drpGLMain.substring(0, 1, drpGLMain);
        if (drpGLMain == "" || drpGLMain == null) {
            source.innerHTML = "GLMain should not be null";
            arguments.IsValid = false;
        }
    }
    function validate_glsub(source, arguments) {
        var drpGLSub = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLSub").value;
        firstchr = drpGLSub.substring(0, 1, drpGLSub);
        if (drpGLSub == "" || drpGLSub == null) {
            source.innerHTML = "GLSub should not be null";
            arguments.IsValid = false;
        }
    }
    function validate_glaccount(source, arguments) {
        var drpGLAccount = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLAccount").value;
        firstchr = drpGLAccount.substring(0, 1, drpGLAccount);
        if (drpGLAccount == "-1" || drpGLAccount == null) {
            source.innerHTML = "GL Account should not be null";
            arguments.IsValid = false;
        }
    }
    function validate_branch(source, arguments) {
        var drpGLBranch = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpGLBranch").value;
        firstchr = drpGLBranch.substring(0, 1, drpGLBranch);
        if (drpGLBranch == "" || drpGLBranch == null) {
            source.innerHTML = "Branch should not be null";
            arguments.IsValid = false;
        }
    }
    
</script>
    <div id="DivOuter">
        <div class="subFormTitle">
           Chart Of Account
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesMan" Text="Classification" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpClassificationGrp" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="drpClassificationGrp_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label"></td>
                        <td class="inputcontrols"></td>
                        <td class="label"></td>
                        <td class="inputcontrols"></td>
                    </tr>
                    </table>
                    
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AutoGenerateColumns="False" SkinID="GridView"
                        OnPageIndexChanging="GV_GLMaster_PageIndexChanging" OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit"
                        OnRowCreated="GV_GLMaster_RowCreated" OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                        <%-- for Sales Man Details --%>
                        <asp:TemplateField HeaderText="GL Classification " SortExpression="GLClassification">
                                <EditItemTemplate>
                                    <asp:Label ID="lblGLClassification" runat="server" Text='<%# Bind("GL_Classification_Description") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGLClassification" runat="server" Text='<%# Bind("GL_Classification_Description")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpGLClassification" SkinID="DropDownListNormalBig" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="drpGLClassification_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                     <asp:RequiredFieldValidator ID="ReqdrpGLClassification" runat="server" ControlToValidate="drpGLClassification"
                                                            ErrorMessage="GL Classification should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                        </asp:RequiredFieldValidator>
                                     <br />
                                    <asp:CustomValidator ID="CustdrpGLClassification" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLClassification"
                                        ClientValidationFunction="validate_glclass" SetFocusOnError="true"></asp:CustomValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                         <%--GL GROUP--%>
                        <asp:TemplateField HeaderText="GL Group " SortExpression="GLGroup">
                            <EditItemTemplate>
                                <asp:Label ID="lblGLGroup" runat="server" Text='<%# Bind("Gl_Group_Description") %>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGLGroup" runat="server" Text='<%# Bind("Gl_Group_Description")%>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drpGLGroup" SkinID="DropDownListNormalBig" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="drpGLGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="ReqdrpGLGroup" runat="server" ControlToValidate="drpGLGroup"
                                                    ErrorMessage="GL Group should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                 <asp:CustomValidator ID="CustdrpGLGroup" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLGroup"
                                        ClientValidationFunction="validate_glgroup" SetFocusOnError="true"></asp:CustomValidator>                                                
                                                                                        
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <%--GL MAIN--%>
                        <asp:TemplateField HeaderText="GL Main" SortExpression="GLMain">
                            <EditItemTemplate>
                                <asp:Label ID="lblGLMain" runat="server" Text='<%# Bind("Gl_Main_Description") %>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGLMain" runat="server" Text='<%# Bind("Gl_Main_Description")%>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drpGLMain" SkinID="DropDownListNormalBig" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="drpGLMain_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                 <asp:RequiredFieldValidator ID="ReqdrpGLMain" runat="server" ControlToValidate="drpGLMain"
                                                    ErrorMessage="GL Main should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                 <br />
                                <asp:CustomValidator ID="CustdrpGLMain" SkinID="GridViewLabelError" runat="server"
                                    Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLMain"
                                    ClientValidationFunction="validate_glmain" SetFocusOnError="true"></asp:CustomValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%--GL SUB--%>
                        <asp:TemplateField HeaderText="GL Sub" SortExpression="GLSub">
                            <EditItemTemplate>
                                <asp:Label ID="lblGLSub" runat="server" Text='<%# Bind("Gl_Sub_Description") %>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGLSub" runat="server" Text='<%# Bind("Gl_Sub_Description")%>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drpGLSub" SkinID="DropDownListNormalBig" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="drpGLSub_SelectedIndexChanged">
                                </asp:DropDownList>
                                 <br />
                                 <asp:RequiredFieldValidator ID="ReqdrpGLSub" runat="server" ControlToValidate="drpGLSub"
                                                    ErrorMessage="GL Sub should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                 <br />
                                <asp:CustomValidator ID="CustdrpGLSub" SkinID="GridViewLabelError" runat="server"
                                    Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLSub"
                                    ClientValidationFunction="validate_glsub" SetFocusOnError="true"></asp:CustomValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                     
                        <%--Wait for Testing--%>
                        
                        
                        <%--GL Account--%>
                        <asp:TemplateField HeaderText="GL Account" SortExpression="GLAccount">
                            <EditItemTemplate>
                                <asp:Label ID="lblGLAccount" runat="server" Text='<%# Bind("GL_Account_Code") %>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGLAccount" runat="server" Text='<%# Bind("GL_Account_Code")%>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drpGLAccount" SkinID="DropDownListNormalBig" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="drpGLAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                                 <br />
                                 <asp:RequiredFieldValidator ID="ReqdrpGLAccount" runat="server" ControlToValidate="drpGLAccount"
                                                    ErrorMessage="GL Sub should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                 <br />
                                <asp:CustomValidator ID="CustdrpGLAccount" SkinID="GridViewLabelError" runat="server"
                                    Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLAccount"
                                    ClientValidationFunction="validate_glaccount" SetFocusOnError="true"></asp:CustomValidator>
                                    
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="GL Branch" SortExpression="GLAccount">
                            <EditItemTemplate>
                                <asp:Label ID="lblGLBranch" runat="server" Text='<%# Bind("Branch_Code") %>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGLBranch" runat="server" Text='<%# Bind("Branch_Code")%>'
                                    SkinID="GridViewLabel">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="drpGLBranch" SkinID="DropDownListNormalBig" AutoPostBack="false"
                                    runat="server" >
                                </asp:DropDownList>
                                <br />
                                 <asp:RequiredFieldValidator ID="ReqdrpGLBranch" runat="server" ControlToValidate="drpGLBranch"
                                                    ErrorMessage="Branch should not be null" Display="Dynamic" ValidationGroup="CAMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                 <br />
                                <asp:CustomValidator ID="CustdrpGLBranch" SkinID="GridViewLabelError" runat="server"
                                    Display="Dynamic" ValidationGroup="CAMasterAddGroup" ControlToValidate="drpGLBranch"
                                    ClientValidationFunction="validate_branch" SetFocusOnError="true"></asp:CustomValidator>
                                
                            </FooterTemplate>
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
                                        ValidationGroup="CAMasterEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%--<asp:LinkButton ID="btAdd" runat="server" Text="Add" CommandName="Insert" SkinID="GridViewButton" />--%>
                                    <%--<asp:Button ID="btAdd" runat="server" CommandName="Insert" Text="Add" SkinID ="GridViewButtonFooter"  />--%>
                                      <asp:Button ID="btAdd" runat="server" Text="Add" SkinID="GridViewButtonFooter" CommandName="Insert"
                                                            ValidationGroup="CAMasterAddGroup" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            
            <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', 975, 475);
        }
    </script>
</asp:Content>
