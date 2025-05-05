<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Zone.aspx.cs" Inherits="IMPALWeb.Zone" %>

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
            Zone
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Zone" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3"
                                DataSourceID="ObjectDataZone" HorizontalAlign="Left" OnDataBinding="GV_Zone_DataBinding"
                                OnRowCommand="GV_Zone_RowCommand" OnRowUpdating="GV_Zone_RowUpdating" OnSelectedIndexChanged="GV_Zone_SelectedIndexChanged"
                                PageSize="20" ShowFooter="True" SkinID="GridView">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblZone" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblZoneCode" runat="server" Text='<%# Bind("ZoneCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblZoneName" runat="server" Text='<%# Bind("ZoneName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtZoneName" runat="server" Text='<%# Bind("ZoneName") %>' Width="241px"
                                                SkinID="GridViewTextBox" MaxLength="40" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvZoneName" runat="server" ControlToValidate="txtZoneName"
                                                SetFocusOnError="true" Display="Dynamic" SkinID="GridViewLabelError" ErrorMessage="Please Enter Zone Name"
                                                ValidationGroup="ZoneNameEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="ZoneNameEditGroup" ControlToValidate="txtZoneName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" ID="txtNewZoneName" Width="241px" MaxLength="40"
                                                runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewZoneName" runat="server" ControlToValidate="txtNewZoneName"
                                                Display="Dynamic" SkinID="GridViewLabelError" SetFocusOnError="true" ErrorMessage="Please Enter Zone Name"
                                                ValidationGroup="ZoneNameAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="ZoneNameAddGroup" ControlToValidate="txtNewZoneName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                ValidationGroup="ZoneNameEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" CommandName="Insert"
                                                Text="Add" ValidationGroup="ZoneNameAddGroup" />
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
                <asp:ObjectDataSource ID="ObjectDataZone" runat="server" InsertMethod="AddNewZone"
                    SelectMethod="GetAllZones" TypeName="IMPALLibrary.Zones" OnInserting="ODSZone_Inserting"
                    UpdateMethod="UpdateZone">
                    <UpdateParameters>
                        <asp:Parameter Name="ZoneCode" Type="String" />
                        <asp:Parameter Name="ZoneName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ZoneName" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
