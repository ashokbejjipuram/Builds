<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLAccMaster.aspx.cs"
    Inherits="IMPALWeb.GLAccMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

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


        function validate(source, arguments) {
            var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_GLSubMaster_ctl27_txtGLAccCode").value; //Id.value;
            firstchr = TxtGLAcctCode.substring(0, 1);
            //            if (TxtGLAcctCode != "") {
            //                firstchr = TxtGLAcctCode.substring(0, 1);
            //                if (isspecialchar(firstchr)) {
            //                    alert("First character Should be Alphabet or Number");
            //                    Id.value = "";
            //                    Id.focus();
            //                    return false;
            //                }
            //                else
            if (firstchr == " ") {
                source.innerHTML = "First character Should not be blank";
                arguments.IsValid = false;
            }
            else {
                return true;
            }
        }
        //}

        //        function validate(source, arguments) {
        //            var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_GLSubMaster_ctl27_txtGLAccCode").value;
        //            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
        //            if (TxtGLAcctCode == "" || TxtGLAcctCode == null) {
        //                source.innerHTML = "Code should not be null";
        //                arguments.IsValid = false;
        //            }
        //            else if (TxtGLAcctCode.length != 7) {
        //                source.innerHTML = "Length of Account Code must be 7";
        //                arguments.IsValid = false;

        //            }
        //            else if (isspecialchar(firstchr)) {
        //                source.innerHTML = "First character of code should be alphabet or number";
        //                arguments.IsValid = false;

        //            }
        //            else {
        //                return true;
        //            }
        //        }
        function validate_desc(source, arguments) {
            var ddGLMainGroup = document.getElementById("ctl00_CPHDetails_GV_GLSubMaster_ctl27_ddGLMainGroup").value;
            firstchr = ddGLMainGroup.substring(0, 1, ddGLMainGroup);
            if (ddGLMainGroup == "" || ddGLMainGroup == null) {
                source.innerHTML = "Code should not be null";
                arguments.IsValid = false;
            }
        }
    </script>

    <div>
        <asp:UpdatePanel ID="updGlAccmaster" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ObjectDSGLSubMaster" runat="server" InsertMethod="AddNewGLAccMaster"
                                SelectMethod="GetAllGlAccGroup" TypeName="IMPALLibrary.GLAccMasters" UpdateMethod="UpdateGLAccGroup"
                                OnInserting="ObjectDSGLSubMaster_Inserting">
                                <UpdateParameters>
                                    <asp:Parameter Name="GLMasterCode" Type="String" />
                                    <asp:Parameter Name="GLMasterDesc" Type="String" />
                                    <asp:Parameter Name="GLSubMasterCode" Type="String" />
                                    <asp:Parameter Name="GLSubMasterDesc" Type="String" />
                                    <asp:Parameter Name="GLAccMasterCode" Type="String" />
                                    <asp:Parameter Name="GLAccMasterDesc" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="GLMasterCode" Type="String" />
                                    <asp:Parameter Name="GLMasterDesc" Type="String" />
                                    <asp:Parameter Name="GLSubMasterCode" Type="String" />
                                    <asp:Parameter Name="GLSubMasterDesc" Type="String" />
                                    <asp:Parameter Name="GLAccMasterCode" Type="String" />
                                    <asp:Parameter Name="GLAccMasterDesc" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ODSGLSub" runat="server" SelectMethod="GetAllGlAccSubGroup"
                                TypeName="IMPALLibrary.GLAccMasters">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="" Name="glmainCode" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <table>
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            GL Account Master</div>
                                        <asp:GridView ID="GV_GLSubMaster" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDSGLSubMaster"
                                            AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                            CaptionAlign="Left" SkinID="GridView" PageSize="25" ShowFooter="True" OnDataBinding="GV_GLSubMaster_DataBinding"
                                            OnRowCommand="GV_GLSubMaster_RowCommand" OnRowDataBound="GV_GLSubMaster_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="GL Code" SortExpression="GLMainCode">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGLMainCode" runat="server" Text='<%#Bind("GLMasterCode")%>' Wrap="False"
                                                            Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLMainCode" runat="server" Text='<%#Bind("GLMasterCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GL Desc" SortExpression="GLMainDesc">
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddGLMainGroup" runat="server" DataSourceID="ObjectDataGLMain"
                                                            DataTextField="Description" DataValueField="GLMasterCode" OnSelectedIndexChanged="ddGLMainGroup_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="241px">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="ReqddGLMainGroup" runat="server" ControlToValidate="ddGLMainGroup"
                                                            ErrorMessage="Desc should not be null" Display="Dynamic" ValidationGroup="GLSubMasterAddGroup">
                                                        </asp:RequiredFieldValidator>
                                                        <br />
                                                        <%--<asp:CustomValidator ID="CustddGLMainGroup" SkinID="GridViewLabelError" runat="server"
                                                            Display="Dynamic" ValidationGroup="GLSubMasterAddGroup" ControlToValidate="ddGLMainGroup"
                                                            ClientValidationFunction="validate_desc" SetFocusOnError="true"></asp:CustomValidator>--%>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLMainDesc" runat="server" Text='<%# Bind("GLMasterDesc") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GL SubCode" SortExpression="GLSubCode">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGLSubCode" runat="server" Text='<%#Bind("GLSubMasterCode")%>'
                                                            Wrap="False" Width="90px" Enabled="false"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLSubCode" runat="server" Text='<%#Bind("GLSubMasterCode") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GL Sub Description" SortExpression="GLSubDescription">
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddGLSubDescription" runat="server" DataSourceID="ODSGLSub"
                                                            AutoPostBack="true" DataTextField="GLSubMasterDesc" DataValueField="GLSubMasterCode"
                                                            OnDataBound="ddGLSubDescription_DataBound" OnSelectedIndexChanged="ddGLSubDescription_SelectedIndexChanged"
                                                            Width="241px">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLSubDesc" runat="server" Text='<%#Bind("GLSubMasterDesc") %>'>        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GL AccCode" SortExpression="GLAccCode">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGLAccCode" runat="server" MaxLength="7" Text='<%# Bind("GLSubMasterCode") %>'
                                                            Wrap="False" Width="90px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqtxtGLAccCode" runat="server" ControlToValidate="txtGLAccCode"
                                                            ErrorMessage="Code should not be null" Display="Dynamic" ValidationGroup="GLSubMasterAddGroup">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RetxtGLAccCode1" runat="server" ControlToValidate="txtGLAccCode"
                                                            ErrorMessage="Code should be 7 digit" Display="Dynamic" ValidationExpression="^\w{0,7}$"
                                                            ValidationGroup="GLSubMasterAddGroup"></asp:RegularExpressionValidator>
                                                        <asp:CustomValidator ID="CusttxtGLAccCode" SkinID="GridViewLabelError" runat="server"
                                                            Display="Dynamic" ValidationGroup="GLSubMasterAddGroup" ControlToValidate="txtGLAccCode"
                                                            ClientValidationFunction="validate" SetFocusOnError="true"></asp:CustomValidator>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLAccCode" runat="server" Text='<%# Bind("GLAccMasterCode") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GL Acc Description" SortExpression="GLAccDescription">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGLAccDescription" runat="server" Text='<%# Bind("GLAccMasterDesc") %>'
                                                            Wrap="False" Width="241px"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rvGLAccDescription" runat="server" ControlToValidate="txtGLAccDescription"
                                                            ErrorMessage=" First character for Description field should be alphabet or number only"
                                                            ValidationGroup="GLAccEditGroup">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:CustomValidator ID="CustEditGLACCDesc" SkinID="GridViewLabelError" runat="server"
                                                            Display="Dynamic" ValidationGroup="GLAccEditGroup" ControlToValidate="txtGLAccDescription"
                                                            ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewAccDescription" runat="server" Wrap="False" Width="250px"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rvNewAccDesc" runat="server" ControlToValidate="txtNewAccDescription"
                                                            ErrorMessage="Please enter a valid GL Acc Description" ValidationGroup="GLSubMasterAddGroup">
                                                        </asp:RequiredFieldValidator>
                                                        <br />
                                                        <%--<asp:RegularExpressionValidator ID="RetxtNewAccDesc" runat="server" ControlToValidate="txtNewAccDescription"
                                                            ErrorMessage="First character of description should be alphabet or number" Display="Dynamic"
                                                            ValidationExpression="^\w{0,}$" ValidationGroup="GLSubMasterAddGroup"></asp:RegularExpressionValidator>--%>
                                                        <asp:CustomValidator ID="CustNewGLACCDesc" SkinID="GridViewLabelError" runat="server"
                                                            Display="Dynamic" ValidationGroup="GLSubMasterAddGroup" ControlToValidate="txtNewAccDescription"
                                                            ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                                    </FooterTemplate>
                                                    <FooterStyle Wrap="False" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLAccDescription" runat="server" Text='<%# Bind("GLAccMasterDesc") %>'></asp:Label>
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
                                                            ValidationGroup="GLSubMasterEditGroup">
                                                            <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                            <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btAdd" runat="server" Text="Add" SkinID="GridViewButtonFooter" CommandName="Insert"
                                                            ValidationGroup="GLSubMasterAddGroup" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <asp:ObjectDataSource ID="ObjectDataGLMain" runat="server" SelectMethod="GetAllGLMasters"
                                TypeName="IMPALLibrary.GLMasters" OnInserting="ObjectDataGLMain_Inserting"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
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
</asp:Content>
