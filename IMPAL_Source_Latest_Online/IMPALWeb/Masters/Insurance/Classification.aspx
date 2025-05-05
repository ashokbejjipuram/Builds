<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Classification.aspx.cs" Inherits="IMPALWeb.Classification" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
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

    <div id="DivOuter">
        <div class="subFormTitle">
            Classification
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Classification" runat="server" AutoGenerateColumns="False" DataSourceID="ODSClassification"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Classification_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Classification_SelectedIndexChanged"
                                OnRowCommand="GV_Classification_RowCommand" OnRowUpdating="GV_Classification_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblClassification" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                                MaxLength="40" SkinID="GridViewTextBox" Wrap="False" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="DescEditGroup" ControlToValidate="txtDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvDescription" runat="server" ControlToValidate="txtDescription"
                                                SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                ValidationGroup="DescEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDescription" MaxLength="40" SkinID="GridViewTextBox" runat="server"
                                                Width="241px" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="DescAddGroup" ControlToValidate="txtNewDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvNewDescription" runat="server" ControlToValidate="txtNewDescription"
                                                SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min. Value">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblMinValue" runat="server" Text='<%# Bind("MinimumValue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMinValue" runat="server" Text='<%# Bind("MinimumValue") %>' Wrap="False"
                                                SkinID="GridViewTextBox" MaxLength="12" Width="198px"></asp:TextBox>
                                            <br />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMinValue"
                                                ErrorMessage="Minimum Value must be numeric" ID="regMinVal" runat="server" SetFocusOnError="true"
                                                ValidationGroup="DescEditGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="rvMinValue" runat="server" ControlToValidate="txtMinValue"
                                                ErrorMessage="Please Enter Minimum Value" ValidationGroup="DescEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="12" ID="txtNewMinValue" runat="server"
                                                Wrap="False" Width="198px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="rvNewMinValue" runat="server" ControlToValidate="txtNewMinValue"
                                                SetFocusOnError="true" ErrorMessage="Please Enter Minimum Value" ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewMinValue"
                                                ErrorMessage="Minimum Value must be numeric" ID="regMinValAdd" runat="server"
                                                SetFocusOnError="true" ValidationGroup="DescAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max. Value">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblMaxValue" runat="server" Text='<%# Bind("MaximumValue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMaxValue" runat="server" Text='<%# Bind("MaximumValue") %>' Wrap="False"
                                                SkinID="GridViewTextBox" Width="198px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="rvMaxValue"
                                                runat="server" ControlToValidate="txtMaxValue" ErrorMessage="Please Enter Maximum Value"
                                                ValidationGroup="DescEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMaxValue"
                                                ErrorMessage="Maximum Value must be numeric" ID="regMaxValEdit" runat="server"
                                                SetFocusOnError="true" ValidationGroup="DescEditGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            <asp:CompareValidator ControlToValidate="txtMaxValue" SetFocusOnError="true" ErrorMessage="Max value must be greater than Min Value"
                                                runat="server" ID="compMaxValEdit" ControlToCompare="txtMinValue" Display="Dynamic"
                                                Type="Double" Operator="GreaterThan" ValidationGroup="DescEditGroup"></asp:CompareValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewMaxValue" Width="198px" SkinID="GridViewTextBox" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="rvNewMaxValue"
                                                runat="server" ControlToValidate="txtNewMaxValue" ErrorMessage="Please Enter Maximum Value"
                                                ValidationGroup="DescAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewMaxValue"
                                                ErrorMessage="Maximum Value must be numeric" ID="regMaxValAdd" runat="server"
                                                SetFocusOnError="true" ValidationGroup="DescAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            <asp:CompareValidator ControlToValidate="txtNewMaxValue" SetFocusOnError="true" ErrorMessage="Max value must be greater than Min Value"
                                                runat="server" ID="compMaxValAdd" ControlToCompare="txtNewMinValue" Display="Dynamic"
                                                Type="Double" Operator="GreaterThan" ValidationGroup="DescAddGroup"></asp:CompareValidator>
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
                                                ValidationGroup="DescEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="DescAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ODSClassification" runat="server" InsertMethod="AddNewClassification"
                    SelectMethod="GetAllClassifications" TypeName="IMPALLibrary.Classifications"
                    OnInserting="ODSClassification_Inserting" UpdateMethod="UpdateClassification">
                    <UpdateParameters>
                        <asp:Parameter Name="Code" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="MinimumValue" Type="Double" />
                        <asp:Parameter Name="MaximumValue" Type="Double" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="MinimumValue" Type="Double" />
                        <asp:Parameter Name="MaximumValue" Type="Double" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" /></center>
            </div>
        </div>
    </div>
</asp:Content>
