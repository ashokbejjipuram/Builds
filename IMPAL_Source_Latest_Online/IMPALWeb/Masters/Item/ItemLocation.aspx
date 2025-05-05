<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ItemLocation.aspx.cs"
    Inherits="IMPALWeb.ItemLocation" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <asp:UpdatePanel ID="updateGrd" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    ITEM LOCATION</div>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierLine" SkinID="LabelNormal" Text="Supplier Line" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpSupplierLine" runat="server" SkinID="DropDownListNormal" DropDownStyle="DropDownList"
                             AutoPostBack="True" OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            <asp:Label ID="lblSupplierPartNo" SkinID="LabelNormal" Text="Supplier Part Number"
                                runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:TextBox ID="txtSupplierPartNo" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                        </td>
                        <td class="transactionButtonsHolder">
                            <asp:Button ID="btnList" runat="server" Text="List" SkinID="ButtonNormal" OnClick="btnList_Click" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HdnSuppLine" runat="server" />
                <table>
                    <tr>
                        <td>
                            <div class="subFormTitle">
                                ITEM LOCATION DETAILS</div>
                            <div class="gridViewScrollFullPage">
                                <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" SkinID="GridViewScroll"
                                    OnRowCreated="grvItemDetails_RowCreated" OnPageIndexChanging="grvItemDetails_PageIndexChanging"
                                    OnRowCancelingEdit="grvItemDetails_RowCancelingEdit" OnRowEditing="grvItemDetails_RowEditing"
                                    OnRowUpdating="grvItemDetails_RowUpdating" OnRowCommand="grvItemDetails_RowCommand">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCode" Text='<%#  Bind("item_code") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditItemCode" SkinID="GridViewTextBox" ReadOnly="true" runat="server"
                                                    Text='<%# Bind("item_code") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtItemCode" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <User:ItemCodePartNumber ID="user" runat="server" Mode="2" Disable="false" OnSearchImageClicked="user_SearchImageClicked" />
                                                <br></br>
                                                <asp:RequiredFieldValidator ID="rvItemCode" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="txtItemCode" ErrorMessage="Please enter a valid ItemCode"
                                                    ValidationGroup="GLMasterAddGroup" SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supp. Part #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuppPartNo" Text='<%#  Bind("part_no") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditSuppPartNo" SkinID="GridViewTextBox" ReadOnly="true" runat="server"
                                                    Text='<%# Bind("part_no") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtSuppPartNo" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="true"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" Text='<%#  Bind("Location_Code") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditLocation" SkinID="GridViewTextBox" ReadOnly="true" runat="server" Text='<%# Bind("Location_Code") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtLocation" SkinID="GridViewTextBox" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranch" Text='<%#  Bind("Branch_Code") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBranch" SkinID="GridViewTextBox" runat="server" Text='<%# Bind("Branch_Code") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpBranch" SkinID="GridViewDropDownListFooter" runat="server">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aisle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAisle" Text='<%#  Bind("Aisle") %>' SkinID="GridViewLabel" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditAisle" SkinID="GridViewTextBox" runat="server" Text='<%# Bind("Aisle") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAisle" onblur="return ValidateFirstCharacter(this.id, 'Aisle');"
                                                    MaxLength="3" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <br></br>
                                                <asp:RequiredFieldValidator ID="rvAisle" runat="server" ControlToValidate="txtAisle"
                                                    ErrorMessage="Please enter a valid Aisle" ValidationGroup="GLMasterAddGroup"
                                                    SetFocusOnError="true" SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRow" Text='<%#  Bind("Row") %>' SkinID="GridViewLabel" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditRow" SkinID="GridViewTextBox" runat="server" Text='<%# Bind("Row") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRow" onblur="return ValidateFirstCharacter(this.id, 'Row');"
                                                    MaxLength="3" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <br></br>
                                                <asp:RequiredFieldValidator ID="rvRow" runat="server" ControlToValidate="txtRow"
                                                    ErrorMessage="Please enter a valid Row" ValidationGroup="GLMasterAddGroup" SetFocusOnError="true"
                                                    SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bin">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBin" Text='<%#  Bind("Bin") %>' SkinID="GridViewLabel" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBin" SkinID="GridViewTextBox" runat="server" Text='<%# Bind("Bin") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBin" onblur="return ValidateFirstCharacter(this.id, 'Bin');"
                                                    MaxLength="3" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <br></br>
                                                <asp:RequiredFieldValidator ID="rvBin" runat="server" ControlToValidate="txtBin"
                                                    ErrorMessage="Please enter a valid Bin" ValidationGroup="GLMasterAddGroup" SetFocusOnError="true"
                                                    SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock on Hand">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance_Quantity" Text='<%#  Bind("Balance_Quantity") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBalance_Quantity" SkinID="TextBoxDisabled" runat="server" ReadOnly="true"
                                                    Text='<%# Bind("Balance_Quantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBalance_Quantity" onkeypress="enterNumberOnly()" runat="server"
                                                    SkinID="TextBoxDisabled" MaxLength="6" Text="0" Enabled="false"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max. Stock qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaximum_Quantity" Text='<%#  Bind("Maximum_Quantity") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditMaximum_Quantity" SkinID="TextBoxDisabled" runat="server" ReadOnly="true"
                                                    Text='<%# Bind("Maximum_Quantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMaximum_Quantity" onkeypress="enterNumberOnly()" runat="server"
                                                    SkinID="TextBoxDisabled" MaxLength="6" Text="999999" Enabled="false"></asp:TextBox>
                                                <br></br>
                                                <asp:RequiredFieldValidator ID="rvMaximum_Quantity" runat="server" ControlToValidate="txtMaximum_Quantity"
                                                    ErrorMessage="Please enter a valid Maximum Quantity" ValidationGroup="GLMasterAddGroup"
                                                    SetFocusOnError="true" SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>
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
                                                    ValidationGroup="AMEditGroup" OnClientClick="return validateLocation(this.id)">
                                                    <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                    <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" runat="server" Text="Add" ValidationGroup="GLMasterAddGroup"
                                                    CommandName="Insert" OnClientClick="return validateLocation(this.id)" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" Visible="false" />
                        <asp:Button SkinID="ButtonViewReport" ID="btnReset" runat="server" Text="Reset"
                            OnClick="btnReset_Click" />
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
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', 1024, 370);
        }

        function validateLocation(id) {
            var txtAisle = document.getElementById(id.replace("btAdd", "txtAisle"));
            var txtRow = document.getElementById(id.replace("btAdd", "txtRow"));
            var txtBin = document.getElementById(id.replace("btAdd", "txtBin"));

            if (txtAisle.value.trim().toUpperCase() == "LOC") {
                alert("Invalid Aisle.");
                txtAisle.focus();
                return false;
            }

            if (txtRow.value.trim().toUpperCase() == "LOC") {
                alert("Invalid Row.");
                txtRow.focus();
                return false;
            }

            if (txtBin.value.trim().toUpperCase() == "LOC") {
                alert("Invalid Bin.");
                txtBin.focus();
                return false;
            }
        }
    </script>

</asp:Content>
