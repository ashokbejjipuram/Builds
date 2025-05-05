<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ApplicationSegment.aspx.cs" Inherits="IMPALWeb.ApplicationSegment" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (source.id == "ctl00_CPHDetails_GV_ApplnSegment_ctl22_Custvalshortname" && TxtGLAcctCode.length < 3) {
                source.innerHTML = "Code Should be 3 digit";
                arguments.IsValid = false;
            }
            else if (isspecialchar(firstchr)) {
                source.innerHTML = "First character Should be Alphabet or Number";
                arguments.IsValid = false;

            }
            else if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            } 
            else {
                arguments.IsValid = true;
            }

        } 

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Application Segment</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_ApplnSegment" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataASeg"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_ApplnSegment_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_ApplnSegment_SelectedIndexChanged"
                                OnRowCommand="GV_ApplnSegment_RowCommand" OnRowUpdating="GV_ApplnSegment_RowUpdating"
                                SkinID="GridView" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" Text="No Data returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblApplnCode" runat="server" Text='<%# Bind("ApplicationSegmentCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="3" SkinID="GridViewTextBox" ID="txtNewApplnCode" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="Custvalshortname" SkinID="GridViewLabelError" runat="server"
                                                Display="dynamic" ValidationGroup="ApplnAddGroup" ControlToValidate="txtnewapplncode"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblApplnDescription" runat="server" Text='<%# Bind("ApplnSegmentDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="40" ID="txtApplnDescription" runat="server"
                                                Text='<%# Bind("ApplnSegmentDescription") %>' Wrap="False" Width="241px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator runat="server" ID="reqEditDesc" SkinID="GridViewLabelError"
                                                ControlToValidate="txtApplnDescription" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                SetFocusOnError="true" ValidationGroup="ApplnEditGroup"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValEditDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="ApplnEditGroup" ControlToValidate="txtApplnDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="40" ID="txtNewApplnDescription"
                                                Wrap="False" Width="241px" runat="server"></asp:TextBox>
                                           
                                            <br />
                                            <asp:RequiredFieldValidator runat="server" ID="reqAddDesc" SkinID="GridViewLabelError"
                                                ControlToValidate="txtNewApplnDescription" Display="Dynamic" ErrorMessage="Please Enter Description"
                                                SetFocusOnError="true" ValidationGroup="ApplnAddGroup"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValAddDesc" runat="server" SkinID="GridViewLabelError"
                                                Display="Dynamic" ValidationGroup="ApplnAddGroup" ControlToValidate="txtNewApplnDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"></asp:CustomValidator>
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
                                                ValidationGroup="ApplnEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button SkinID="GridViewButtonFooter" ID="btAdd" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="ApplnAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ObjectDataASeg" runat="server" InsertMethod="AddNewApplicationSegment"
                    SelectMethod="GetAllApplicationSegments" TypeName="IMPALLibrary.ApplicationSegments"
                    OnInserting="ODSAppln_Inserting" UpdateMethod="UpdateItemType">
                    <UpdateParameters>
                        <asp:Parameter Name="ApplicationSegmentCode" Type="String" />
                        <asp:Parameter Name="ApplnSegmentDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ApplnSegmentDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
