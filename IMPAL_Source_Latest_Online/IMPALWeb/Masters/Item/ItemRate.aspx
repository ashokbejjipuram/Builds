<%@ Page Title="Item Rate Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ItemRate.aspx.cs" Inherits="IMPALWeb.ItemRate" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validate_itemcode(source, arguments) {
            var drpGLClassification = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpSupplierLine").value;
            firstchr = drpGLClassification.substring(0, 1, drpGLClassification);
            if (drpGLClassification == "" || drpGLClassification == null) {
                source.innerHTML = "SupplierLine should not be null";
                arguments.IsValid = false;
            }
        }
        function validate_Branch(source, arguments) {
            var drpGLClassification = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl17_drpBranch").value;
            firstchr = drpGLClassification.substring(0, 1, drpGLClassification);
            if (drpGLClassification == "0" || drpGLClassification == null) {
                source.innerHTML = "Branch should not be null";
                arguments.IsValid = false;
            }
        }

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Item Rate Master
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplier" Text="Supplier Line" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpItemRate" SkinID="DropDownListNormal" AutoPostBack="true"
                                TabIndex="1" runat="server" OnSelectedIndexChanged="drpItemRate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                    </tr>
                </table>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" ShowFooter="true"
                        AutoGenerateColumns="False" SkinID="GridView" OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand">
                        <%--<EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabelEmptyRow" Text="No Results Found"></asp:Label>
                        </EmptyDataTemplate> --%>
                        <Columns>
                            <asp:TemplateField HeaderText="SupplierLine" SortExpression="SupplierLine">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSupplierLine" runat="server" Text='<%# Bind("SupplierName") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierLine" runat="server" Text='<%# Bind("SupplierName")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpSupplierLine" SkinID="DropDownListNormal" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" Text='<%#Bind("ItemCode") %>' SkinID="GridViewLabel"
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditItemCode" ReadOnly="true" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewItemCode" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    <User:ItemCodePartNumber ID="user" runat="server" Mode="2" Disable="false" OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvtxtNewItemCode" Display="Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="txtNewItemCode"
                                        ErrorMessage="Please Enter Item Code" ValidationGroup="IRAddGroup">
                                    </asp:RequiredFieldValidator>
                                    <%-- <br />
                                    <asp:CustomValidator ID="CusttxtNewItemCode" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="IRAddGroup" ControlToValidate="txtNewItemCode"
                                        ClientValidationFunction="validate_itemcode" SetFocusOnError="true"></asp:CustomValidator>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SuppPartNumber" SortExpression="SuppPartNumber">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPartNumber" runat="server" Text='<%# Bind("Partnumber") %>' SkinID="GridViewTextBox" ReadOnly="true">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvPartNumber" runat="server" ControlToValidate="txtPartNumber"
                                        ErrorMessage="Please enter a valid PartNumber" ValidationGroup="IREditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# Bind("Partnumber") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewPartNumber" runat="server" Wrap="False" SkinID="GridViewTextBox"
                                        ReadOnly="true">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvtxtNewPartNumber" runat="server" ControlToValidate="txtNewPartNumber"
                                        ErrorMessage="Please enter a valid PartNumber" ValidationGroup="IRAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                                <EditItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" readonly="true" Text='<%# Bind("BranchName") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpBranch" SkinID="DropDownListNormal" runat="server" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rvdrpBranch" Display="Dynamic" SetFocusOnError="true"
                                        SkinID="GridViewLabelError" runat="server" ControlToValidate="drpBranch" ErrorMessage="Branch should not be null"
                                        ValidationGroup="IRAddGroup">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CustomValidator ID="CustdrpBranch" SkinID="GridViewLabelError" runat="server"
                                        Display="Dynamic" ValidationGroup="IRAddGroup" ControlToValidate="drpBranch"
                                        ClientValidationFunction="validate_Branch" SetFocusOnError="true"></asp:CustomValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NetAmount" SortExpression="NetAmount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNetAmount" runat="server" Text='<%# Bind("NetPrice") %>' SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNetAmount" runat="server" ControlToValidate="txtNetAmount"
                                        ErrorMessage="Please enter a valid NetAmount" ValidationGroup="IREditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# Bind("NetPrice") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewNetAmount" runat="server" Wrap="False" SkinID="GridViewTextBox"> </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                        ID="rvNewNetAmount" runat="server" ControlToValidate="txtNewNetAmount" ErrorMessage="Please Enter Amount"
                                        ValidationGroup="IRAddGroup">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewNetAmount"
                                        ErrorMessage="Amount must be Numeric" ID="regMaxValAdd" runat="server" SetFocusOnError="true"
                                        ValidationGroup="IRAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <%--            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    ValidationGroup="IREditGroup" SkinID="GridViewLinkButton">
                                                    <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                                </asp:LinkButton>--%>
                                    <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        ValidationGroup="IREditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%--  <asp:LinkButton ID="btAdd" runat="server" Text="Add" CommandName="Insert" SkinID="GridViewButton" />--%>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" SkinID="GridViewButtonFooter" CommandName="Insert"
                                        ValidationGroup="IRAddGroup" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            Visible="true" OnClick="btnReport_Click" />
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
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', 750, 370);
        }
    </script>
</asp:Content>
