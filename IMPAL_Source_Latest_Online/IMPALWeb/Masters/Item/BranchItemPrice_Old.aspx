<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchItemPrice_Old.aspx.cs"
    Inherits="IMPALWeb.BranchItemPrice_Old" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <asp:UpdatePanel ID="updateGrd" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    Branch & ItemPrice</div>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierLine" SkinID="LabelNormal" Text="Supplier Line" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpSupplierLine" runat="server" SkinID="DropDownListNormal"
                                AutoPostBack="true" OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnSupplierLine" runat="server" />
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
                                Branch & ItemPrice</div>
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
                                                <asp:TextBox ID="txtEditItemCode" ReadOnly="true" runat="server" Text='<%# Bind("item_code") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewItemCode" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="true"></asp:TextBox>
                                                <User:ItemCodePartNumber ID="user" runat="server" Mode="2" Disable="false" OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked" />
                                                <asp:RequiredFieldValidator ID="rvItemCode" runat="server" SetFocusOnError="true"
                                                    Text="." ControlToValidate="txtNewItemCode" ErrorMessage="Please enter a valid ItemCode"
                                                    ValidationGroup="GLMasterAddGroup">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtenderItemCode" TargetControlID="rvItemCode"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartNo" Text='<%#  Bind("PartNo") %>' SkinID="GridViewLabel" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditPartNo" runat="server" ReadOnly="true" Text='<%# Bind("PartNo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewPartNo" Enabled="false" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranch" Text='<%#  Bind("Branch_Code") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBranch" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Code") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpBranch" SkinID="GridViewDropDownList" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ListPrice">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllistPrice" Text='<%#  Bind("listPrice") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditlistPrice" runat="server" Text='<%# Bind("listPrice") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvEditlistPrice" runat="server" ControlToValidate="txtEditlistPrice"
                                                    ErrorMessage="Please enter a valid listPrice" ValidationGroup="GLMasterEditGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtendeEditlistPrice" TargetControlID="rvEditlistPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtEditlistPrice"
                                                    ErrorMessage="listPrice must be Numeric" ID="regMaxlp" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterEditGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewlistPrice" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvNewlistPrice" runat="server" ControlToValidate="txtNewlistPrice"
                                                    ErrorMessage="Please enter a valid listPrice" ValidationGroup="GLMasterAddGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtendeNewlistPrice" TargetControlID="rvNewlistPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewlistPrice"
                                                    ErrorMessage="listPrice must be Numeric" ID="regMaxlp1" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CostPrice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCostPrice" Text='<%#  Bind("costPrice") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditCostPrice" runat="server" Text='<%# Bind("costPrice") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvEditCostPrice" runat="server" ControlToValidate="txtEditCostPrice"
                                                    ErrorMessage="Please enter a valid CostPrice" ValidationGroup="GLMasterEditGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtenderEditCostPrice" TargetControlID="rvEditCostPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtEditCostPrice"
                                                    ErrorMessage="CostPrice must be Numeric" ID="regMaxcp" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterEditGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewCostPrice" runat="server" SkinID="GridViewTextBoxFooter" ReadOnly="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvCostPrice" runat="server" ControlToValidate="txtNewCostPrice"
                                                    ErrorMessage="Please enter a valid CostPrice" ValidationGroup="GLMasterAddGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtenderCostPrice" TargetControlID="rvCostPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewCostPrice"
                                                    ErrorMessage="CostPrice must be Numeric" ID="regMaxcp1" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SellingPrice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSellingPrice" Text='<%#  Bind("sellingPrice") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditSellingPrice" runat="server" Text='<%# Bind("sellingPrice") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvEditSellingPrice" runat="server" ControlToValidate="txtEditSellingPrice"
                                                    ErrorMessage="Please enter a valid SellingPrice" ValidationGroup="GLMasterEditGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtenderEditSellingPrice" TargetControlID="rvEditSellingPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtEditSellingPrice"
                                                    ErrorMessage="Selling Price must be Numeric" ID="regMaxsp" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterEditGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewSellingPrice" runat="server" SkinID="GridViewTextBoxFooter"
                                                    ReadOnly="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvSellingPrice" runat="server" ControlToValidate="txtNewSellingPrice"
                                                    ErrorMessage="Please enter a valid SellingPrice" ValidationGroup="GLMasterAddGroup"
                                                    SetFocusOnError="true" Text=".">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:ValidatorCalloutExtender ID="ExtenderSellingPrice" TargetControlID="rvSellingPrice"
                                                    PopupPosition="BottomLeft" runat="server">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewSellingPrice"
                                                    ErrorMessage="Selling Price must be Numeric" ID="regMaxsp1" runat="server" SetFocusOnError="true"
                                                    ValidationGroup="GLMasterAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
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
                                                    ValidationGroup="GLMasterEditGroup">
                                                    <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                    <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btAdd" runat="server" Text="Add" ValidationGroup="GLMasterAddGroup"
                                                    CommandName="Insert" />
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
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', 1024, 370);
        }
    </script>

</asp:Content>
