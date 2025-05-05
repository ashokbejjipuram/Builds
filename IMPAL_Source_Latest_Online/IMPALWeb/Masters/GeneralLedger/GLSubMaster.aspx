<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLSubMaster.aspx.cs"
    Inherits="IMPALWeb.Masters.GLSubMaster" Title="" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
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
                source.innerHTML = "First character Should be Alphabet or Number";
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
        <asp:ObjectDataSource ID="ObjectDSGLSubMaster" runat="server" InsertMethod="AddNewGLSubMaster"
            SelectMethod="GetAllGlSubGroup" TypeName="IMPALLibrary.GLSubMasters" UpdateMethod="UpdateGLSubGroup"
            OnInserting="ObjectDSGLSubMaster_Inserting">
            <UpdateParameters>
                <asp:Parameter Name="GLMasterCode" Type="String" />
                <asp:Parameter Name="GLSubMasterCode" Type="String" />
                <asp:Parameter Name="GLSubMasterDesc" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="GLMasterCode" Type="String" />
                <asp:Parameter Name="GLSubMasterCode" Type="String" />
                <asp:Parameter Name="GLSubMasterDesc" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <%--<asp:ObjectDataSource ID="ObjectDataGLMain" runat="server" SelectMethod="GetAllGLMasters"
            TypeName="IMPALLibrary.GLMasters" OnInserting="ObjectDataGLMain_Inserting"></asp:ObjectDataSource>--%>
        <asp:ObjectDataSource ID="ObjectDataGLMain" runat="server" SelectMethod="GetAllGLMasters"
            TypeName="IMPALLibrary.GLMasters" OnInserting="ObjectDataGLMain_Inserting"></asp:ObjectDataSource>
        <div class="subFormTitle">
            GL Sub Master</div>
        <div class="gridViewFullPage">
            <asp:GridView ID="GV_GLSubMaster" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDSGLSubMaster"
                SkinID="GridView" OnDataBinding="GV_GLSubMaster_DataBinding" OnRowCommand="GV_GLSubMaster_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="GL Code" SortExpression="GLMainCode">
                        <ItemTemplate>
                            <asp:Label ID="lblGLMainCode" runat="server" Text='<%# Bind("GLMasterCode") %>' SkinID="GridViewLabel"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Desc" SortExpression="GLMainDesc">
                        <FooterTemplate>
                            <asp:DropDownList ID="ddGLMainGroup" runat="server" DataSourceID="ObjectDataGLMain"
                                DataTextField="Description" DataValueField="GLMasterCode" SkinID="GridViewDropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddGLMainGroup" Display="Dynamic" SetFocusOnError="true"
                                SkinID="GridViewLabelError" runat="server" ControlToValidate="ddGLMainGroup"
                                ErrorMessage="Enter GL-Submaster Desc" ValidationGroup="GLSubMasterAddGroup">
                            </asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblGLMainDesc" runat="server" Text='<%# Bind("GLMasterDesc") %>' SkinID="GridViewLabel">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL SubCode" SortExpression="GLSubCode">
                        <FooterTemplate>
                            <asp:TextBox ID="txtGLSubCode" runat="server" MaxLength="4" Text='<%# Bind("GLSubMasterCode") %>'
                                Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
                            <br />
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtGLSubCode"
                                ErrorMessage="GL SubCode must be Numeric" ID="regtxtGLSubCode" runat="server" SetFocusOnError="true"
                                ValidationGroup="GLSubMasterAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rvtxtGLSubCode" Display="Dynamic" SetFocusOnError="true"
                                SkinID="GridViewLabelError" runat="server" ControlToValidate="txtGLSubCode" ErrorMessage="Please enter GL-Submaster code"
                                ValidationGroup="GLSubMasterAddGroup">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CusttxtGLSubCode" SkinID="GridViewLabelError" runat="server"
                                Display="Dynamic" ValidationGroup="GLSubMasterAddGroup" ControlToValidate="txtGLSubCode"
                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblGLSubCode" runat="server" Text='<%# Bind("GLSubMasterCode") %>'
                                SkinID="GridViewLabel">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Sub Description" SortExpression="GLSubDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGLSubDescription" runat="server" Text='<%# Bind("GLSubMasterDesc") %>'
                                Wrap="False" SkinID="GridViewTextBoxBig"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvGLSubDescription" Display="Dynamic" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtGLSubDescription" ErrorMessage="Please enter description"
                                ValidationGroup="GLSubEditGroup" SkinID="GridViewLabelError">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CusttxtGLSubDescription" SkinID="GridViewLabelError" runat="server"
                                Display="Dynamic" ValidationGroup="GLSubAddGroup" ControlToValidate="txtGLSubDescription"
                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewSubDescription" runat="server" Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvtxtNewSubDescription" Display="Dynamic" SetFocusOnError="true"
                                SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewSubDescription"
                                ErrorMessage="Please enter description" ValidationGroup="GLSubMasterAddGroup">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CusttxtNewSubDescription" SkinID="GridViewLabelError" runat="server"
                                Display="Dynamic" ValidationGroup="GLSubMasterAddGroup" ControlToValidate="txtNewSubDescription"
                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                        </FooterTemplate>
                        <FooterStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="lblGLMainDescription" runat="server" Text='<%# Bind("GLSubMasterDesc") %>'
                                SkinID="GridViewLabel"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                ValidationGroup="GLSubMasterEditGroup">
                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                            </asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="GLSubMasterAddGroup"
                                SkinID="GridViewButtonFooter" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                    SkinID="ButtonViewReport" />
            </div>
        </div>
    </div>
</asp:Content>
