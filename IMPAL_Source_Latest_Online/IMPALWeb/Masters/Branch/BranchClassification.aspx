<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BranchClassification.aspx.cs" Inherits="IMPALWeb.BranchClassification" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

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
        <div class="subFormTitle">
            Classification
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_BRClassification" runat="server" AutoGenerateColumns="False"
                                DataSourceID="ObjectDataBRCL" AllowPaging="True" HorizontalAlign="Left" BackColor="White"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_BRClassification_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_BRClassification_SelectedIndexChanged"
                                OnRowCommand="GV_BRClassification_RowCommand" OnRowUpdating="GV_BRClassification_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblClassification" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblBRCLCode" runat="server" Text='<%# Bind("ClassificationCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="1" SkinID="GridViewTextBox" ID="txtNewBRCLCode" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValNewcODE" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="BRCLAddGroup" ControlToValidate="txtNewBRCLCode"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewBRCLCode" runat="server" ControlToValidate="txtNewBRCLCode" ErrorMessage="Please Enter Branch Classification Code"
                                                ValidationGroup="BRCLAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBRCLDescription" SkinID="GridViewLabel" runat="server" Text='<%# Bind("ClassificationDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="40" SkinID="GridViewTextBox" ID="txtNewBRCLDescription" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="BRCLAddGroup" ControlToValidate="txtNewBRCLDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvNewBRCLDesc" runat="server" Display="Dynamic" SetFocusOnError="true"
                                                SkinID="GridViewLabelError" ControlToValidate="txtNewBRCLDescription" ErrorMessage="Please Enter Description"
                                                ValidationGroup="BRCLAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outstanding Limit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBROSLimit" HtmlEncode="False" DataFormatString="{0:c}" runat="server"
                                                Text='<%# Eval("OutstandingLimit", "{0:0.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox MaxLength="12" SkinID="GridViewTextBox" ID="txtBROSLimit" runat="server"
                                                Text='<%# Bind("OutstandingLimit", "{0:#.00}") %>' Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvBROSLimit" runat="server" ControlToValidate="txtBROSLimit" ErrorMessage="Please Enter Outstanding Limit"
                                                ValidationGroup="BRCLEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtBROSLimit"
                                                ErrorMessage="Outstanding Limit must be Numeric" ID="regMaxValEdit" runat="server"
                                                SetFocusOnError="true" ValidationGroup="BROSLimitEditGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="12" ID="txtNewBROSLimit" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewBROSLimit" runat="server" ControlToValidate="txtNewBROSLimit" ErrorMessage="Please Enter Outstanding Limit"
                                                ValidationGroup="BRCLAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewBROSLimit"
                                                ErrorMessage="Outstanding Limit must be Numeric" ID="regMaxValAdd" runat="server"
                                                SetFocusOnError="true" ValidationGroup="BRCLAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Outstanding Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBROSDays" SkinID="GridViewLabel" runat="server" Text='<%# Bind("OutstandingDays") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBROSDays" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("OutstandingDays") %>'
                                                MaxLength="3" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="GridViewLabelError" SetFocusOnError="true"
                                                ID="rvBROSDays" runat="server" ControlToValidate="txtBROSDays" ErrorMessage="Please Enter Outstanding Days"
                                                ValidationGroup="BRCLEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtBROSDays"
                                                ErrorMessage="Outstanding Days must be Numeric" ID="regMaxValEdit1" runat="server"
                                                SetFocusOnError="true" ValidationGroup="BRCLAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="3" SkinID="GridViewTextBox" ID="txtNewBROSDays" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewBROSDays" runat="server" ControlToValidate="txtNewBROSDays" ErrorMessage="Please Enter Outstanding Days"
                                                ValidationGroup="BRCLAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewBROSDays"
                                                ErrorMessage="Outstanding Days must be Numeric" ID="regOutlimitAdd" runat="server"
                                                SetFocusOnError="true" ValidationGroup="BRCLAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*$"></asp:RegularExpressionValidator>
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
                                                ValidationGroup="BRCLEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button SkinID="GridViewButtonFooter" ID="btAdd" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="BRCLAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ObjectDataBRCL" runat="server" InsertMethod="AddNewBRClassifications"
                    SelectMethod="GetAllBRClassifications" TypeName="IMPALLibrary.BranchClassifications"
                    OnInserting="ODSBRClassification_Inserting" UpdateMethod="UpdateBRClassifications">
                    <UpdateParameters>
                        <asp:Parameter Name="ClassificationCode" Type="String" />
                        <asp:Parameter Name="ClassificationDescription" Type="String" />
                        <asp:Parameter Name="OutstandingLimit" Type="String" />
                        <asp:Parameter Name="OutstandingDays" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ClassificationCode" Type="String" />
                        <asp:Parameter Name="ClassificationDescription" Type="String" />
                        <asp:Parameter Name="OutstandingLimit" Type="String" />
                        <asp:Parameter Name="OutstandingDays" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
