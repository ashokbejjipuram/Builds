<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Location.aspx.cs" Inherits="IMPALWeb.Location" %>

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
            Location
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Location" runat="server" AutoGenerateColumns="False" DataSourceID="ODSLocation"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Location_DataBinding"
                                ShowFooter="True" OnRowCommand="GV_Location_RowCommand" SkinID="GridView"
                                PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblClassification" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblCode" runat="server" Text='<%# Bind("FALocationcode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch">
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlLBranch" runat="server" DataSourceID="ObjectDataLBranch"
                                                SkinID="GridViewDropDownListFooter" DataTextField="BranchName" DataValueField="BranchCode">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="reqBranch" runat="server" SetFocusOnError="true"
                                                Display="Dynamic" ErrorMessage="Please Select Branch" ControlToValidate="ddlLBranch"
                                                SkinID="GridViewLabelError" InitialValue="0" ValidationGroup="LocationAddGroup"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblBranchName" runat="server" Text='<%# Bind("BrName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("FALocationName") %>' MaxLength="40"
                                                SkinID="GridViewTextBox" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="LocationEditGroup" ControlToValidate="txtName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="GridViewLabelError" ID="rvName"
                                                runat="server" ControlToValidate="txtName" ErrorMessage="Please Enter Description"
                                                SetFocusOnError="true" ValidationGroup="LocationEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="40" SkinID="GridViewTextBox" Width="241px" ID="txtNewName"
                                                runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="LocationAddGroup" ControlToValidate="txtNewName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvNewName" runat="server" ControlToValidate="txtNewName"
                                                SetFocusOnError="true" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                SkinID="GridViewLabelError" ValidationGroup="LocationAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("FALocationName") %>'></asp:Label>
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
                                                ValidationGroup="LocationEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="LocationAddGroup" />
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
                <asp:ObjectDataSource ID="ODSLocation" runat="server" SelectMethod="GetAllLocations"
                    TypeName="IMPALLibrary.Locations" InsertMethod="AddNewLocations" OnInserting="ODSLocation_Inserting"
                    OnUpdating="ODSLocation_Updating" UpdateMethod="UpdateLocation">
                    <UpdateParameters>
                        <asp:Parameter Name="FALocationcode" Type="String" />
                        <asp:Parameter Name="FALocationName" Type="String" />
                        <asp:Parameter Name="BrName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="FALocationName" Type="String" />
                        <asp:Parameter Name="BrCode" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataLBranch" runat="server" SelectMethod="GetAllBranch"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
