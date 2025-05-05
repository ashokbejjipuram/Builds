<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StateMaster.aspx.cs" Inherits="IMPALWeb.StateMaster" %>

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
            State
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_StateMaster" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataStateMaster"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_StateMaster_DataBinding"
                                ShowFooter="True" OnRowCommand="GV_StateMaster_RowCommand" SkinID="GridView"
                                PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblState" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblStateCode" runat="server" Text='<%# Bind("StateCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStateName" runat="server" Text='<%# Bind("StateName") %>' Wrap="False"
                                                SkinID="GridViewTextBox" MaxLength="40" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvStateName" runat="server" ControlToValidate="txtStateName"
                                                SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                ValidationGroup="StateMasterEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="StateMasterEditGroup" ControlToValidate="txtStateName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="40" Width="241px" ID="txtStateNameNew"
                                                runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="StateMasterAddGroup" ControlToValidate="txtStateNameNew"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvNewStateName" runat="server" ControlToValidate="txtStateNameNew"
                                                SetFocusOnError="true" Display="Dynamic" SkinID="GridViewLabelError" ErrorMessage="Please Enter Description"
                                                ValidationGroup="StateMasterAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblStateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Zone">
                                        <EditItemTemplate>
                                            <asp:DropDownList SkinID="GridViewDropDownList" ID="ddlZone" runat="server" SelectedValue='<%#Bind("ZoneCode")%>'
                                                DataSourceID="ODSZoneName" DataTextField="ZoneName" DataValueField="ZoneCode">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvZone" runat="server" ControlToValidate="ddlZone"
                                                SetFocusOnError="true" SkinID="GridViewLabelError" Display="Dynamic" ErrorMessage="Please Select Zone"
                                                InitialValue="0" ValidationGroup="StateMasterEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlZoneName" SkinID="GridViewDropDownListFooter" runat="server"
                                                DataSourceID="ODSZoneName" DataTextField="ZoneName" DataValueField="ZoneCode">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvZoneAdd" runat="server" ControlToValidate="ddlZoneName"
                                                SetFocusOnError="true" Display="Dynamic" SkinID="GridViewLabelError" ErrorMessage="Please Select Zone"
                                                InitialValue="0" ValidationGroup="StateMasterAddGroup"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblZoneName" runat="server" Text='<%# Bind("ZoneName") %>'>
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
                                                ValidationGroup="StateMasterEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="StateMasterAddGroup" />
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
                <asp:ObjectDataSource ID="ODSZoneName" runat="server" SelectMethod="GetAllZones"
                    TypeName="IMPALLibrary.Zones"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataStateMaster" runat="server" InsertMethod="AddNewState"
                    SelectMethod="GetAllStates" TypeName="IMPALLibrary.StateMasters" OnInserting="ObjectDataStateMaster_Inserting"
                    UpdateMethod="UpdateState" OnUpdating="ObjectDataStateMaster_Updating">
                    <UpdateParameters>
                        <asp:Parameter Name="StateCode" Type="String" />
                        <asp:Parameter Name="StateName" Type="String" />
                        <asp:Parameter Name="ZoneCode" Type="String" />
                        <asp:Parameter Name="ZoneName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="StateCode" Type="String" />
                        <asp:Parameter Name="StateName" Type="String" />
                        <asp:Parameter Name="ZoneCode" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
