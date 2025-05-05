<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Depot.aspx.cs" Inherits="IMPALWeb.Depot" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(Id, Desc) {
            var TxtGLAcctCode = Id.value;
            if (TxtGLAcctCode != "") {
                firstchr = TxtGLAcctCode.substring(0, 1);
                if (isspecialchar(firstchr)) {
                    alert("First character of " + Desc + " field should be Alphabet or Number");
                    Id.value = "";
                    Id.focus();
                    return false;
                }
                else if (firstchr == " ") {
                alert("First character of " + Desc + " field should not be blank");
                    Id.value = "";
                    Id.focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Depot</div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView SkinID="GridViewTransaction" AutoGenerateColumns="False" AllowPaging="True"
                                HorizontalAlign="Left" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                PageSize="20" ShowFooter="True" CellPadding="3" CaptionAlign="Left" ID="gvDepot"
                                runat="server" OnPageIndexChanging="gvDepot_PageIndexChanging" OnRowCancelingEdit="gvDepot_RowCancelingEdit"
                                OnRowCommand="gvDepot_RowCommand" OnRowEditing="gvDepot_RowEditing" OnRowUpdating="gvDepot_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code" SortExpression="DepotCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" SkinID="GridViewLabel" Text='<%# Bind("DepotCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDepot" runat="server" CausesValidation="True" MaxLength="2" SkinID="GridViewTextBoxSmall"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDepot"
                                                ErrorMessage="Please Enter DepotCode" ID="reqDepot" SkinID="GridViewLabelError"
                                                runat="server" ValidationGroup="BTAddGroup"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDepot"
                                                ErrorMessage="Please enter 2 Digit Numeric Depot Code" ID="regDepot" SkinID="GridViewLabelError"
                                                runat="server" ValidationExpression="^(\d{2})" ValidationGroup="BTAddGroup"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Description" SortExpression="DepotShortName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShort" runat="server" SkinID="GridViewLabel" Text='<%# Bind("DepotShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox MaxLength="6" CausesValidation="true" ID="txtEditShort" Text='<%# Bind("DepotShortName") %>'
                                                 onblur="validateFields(this, 'Short Description');" ValidationGroup="BTEditGroup" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtEditShort"
                                                ErrorMessage="Enter Short Description" ID="reqShort" SkinID="GridViewLabelError"
                                                runat="server" ValidationGroup="BTEditGroup"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox MaxLength="6" CausesValidation="true" ID="txtAddShort" runat="server"
                                                onblur="validateFields(this, 'Short Description');"  SkinID="GridViewTextBox"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtAddShort"
                                                ErrorMessage="Enter Short Description" ID="reqAddShort" SkinID="GridViewLabelError"
                                                runat="server" ValidationGroup="BTAddGroup"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Long Description" SortExpression="DepotLongName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLong" runat="server" Text='<%# Bind("DepotLongName") %>' SkinID="GridViewLabel"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox CausesValidation="true" ValidationGroup="BTEditGroup" ID="txtEditLong"
                                                 onblur="validateFields(this, 'Long Description');" Text='<%# Bind("DepotLongName") %>' runat="server" SkinID="GridViewTextBoxBig"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtEditLong"
                                                ErrorMessage="Enter Long Description" ID="reqLong" SkinID="GridViewLabelError"
                                                runat="server" ValidationGroup="BTEditGroup"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox CausesValidation="true" ValidationGroup="BTAddGroup" ID="txtAddlong"
                                              onblur="validateFields(this, 'Long Description');"    runat="server" SkinID="GridViewTextBoxBig"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtAddlong"
                                                ErrorMessage="Enter Long Description" ID="reqAddLong" SkinID="GridViewLabelError"
                                                runat="server" ValidationGroup="BTAddGroup"></asp:RequiredFieldValidator>
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
                                                ValidationGroup="BTEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" CommandName="Insert"
                                                ValidationGroup="BTAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblDepot" runat="server" Text="No rows returned" SkinID="GridViewLabelEmptyRow"></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
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
