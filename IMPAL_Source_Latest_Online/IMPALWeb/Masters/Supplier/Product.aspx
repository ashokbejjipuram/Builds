<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="IMPALWeb.Product" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
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
            Product
        </div>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Product" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSP"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_Product_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_Product_SelectedIndexChanged" OnRowCommand="GV_Product_RowCommand"
                                OnRowUpdating="GV_Product_RowUpdating" SkinID="GridViewTransaction" PageSize="20">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabelEmptyRow" Text="No Data present"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblSupplierCode" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList CausesValidation="true" SkinID="GridViewDropDownList" ID="ddlSupplierCode"
                                                runat="server" DataSourceID="ODSProduct" DataTextField="SupplierName" DataValueField="SupplierCode"
                                                Width="185px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator InitialValue="0" runat="server" ID="reqSupplier" SkinID="GridViewLabelError"
                                                ControlToValidate="ddlSupplierCode" Display="Dynamic" ErrorMessage="Please select supplier"
                                                SetFocusOnError="true" ValidationGroup="SPAddGroup"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Code">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblSPCode" runat="server" Text='<%# Bind("SupplierProductCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox CausesValidation="true" SkinID="GridViewTextBox" MaxLength="2" ID="txtNewSPCode"
                                                runat="server" Wrap="False" Width="79px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator SkinID="GridViewLabelError" Display="Dynamic" SetFocusOnError="true"
                                                ID="rvNewSPCode" runat="server" ControlToValidate="txtNewSPCode" ErrorMessage="Please Enter Product Code"
                                                ValidationGroup="SPAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtNewSPCode"
                                                ErrorMessage="Please enter 2 Digit Numeric Product Code" ID="regProduct" SkinID="GridViewLabelError"
                                                runat="server" ValidationExpression="^(\d{2})" ValidationGroup="BTAddGroup"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblShortName" runat="server" Text='<%# Bind("SupplierProductShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" MaxLength="6" ID="txtShortName" runat="server"
                                                Text='<%# Bind("SupplierProductShortName") %>' Wrap="False" Width="100px"></asp:TextBox>
                                                <span>*</span>
                                            <br />
                                            <asp:RequiredFieldValidator ID="reqShortName" runat="server" SkinID="GridViewLabelError"
                                                ControlToValidate="txtShortName" Display="Dynamic" ErrorMessage="Enter Short Name"
                                                SetFocusOnError="true" ValidationGroup="SPEditGroup"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustValShortName" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SPEditGroup" ControlToValidate="txtShortName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Short Name should not be null"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" ID="txtNewShortName" MaxLength="6" runat="server"
                                                Wrap="False" Width="100px"></asp:TextBox>
                                            <br />
                                            <%-- ValidationExpression="^[A-Za-z0-9]{1}?[A-Za-z0-9 _!@#$%^&*_-=+]*$"--%>
                                            <asp:CustomValidator ID="CustValShortNameFooter" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SPAddGroup" ControlToValidate="txtNewShortName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rvNewShortName" runat="server"
                                                ControlToValidate="txtNewShortName" ErrorMessage="Please Enter Short Name" ValidationGroup="SPAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label SkinID="GridViewLabel" ID="lblLongName" runat="server" Text='<%# Bind("SupplierProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLongName" SkinID="GridViewTextBox" runat="server" Text='<%# Bind("SupplierProductName") %>'
                                                Wrap="False" Width="200px"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValEditProdName" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SPEditGroup" ControlToValidate="txtLongName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvLongName" runat="server" ControlToValidate="txtLongName"
                                                ErrorMessage="Please Enter Product Name" SetFocusOnError="true" Display="Dynamic"
                                                ValidationGroup="SPEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox SkinID="GridViewTextBox" ID="txtNewLongName" runat="server" Wrap="False"
                                                Width="200px"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustValAddProdName" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SPAddGroup" ControlToValidate="txtNewLongName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rvNewLongName" runat="server" ControlToValidate="txtNewLongName"
                                                Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please enter a Product Name"
                                                ValidationGroup="SPAddGroup">
                                            </asp:RequiredFieldValidator>
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
                                                ValidationGroup="SPEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" SkinID="GridViewButtonFooter" CausesValidation="true" runat="server"
                                                Text="Add" CommandName="Insert" ValidationGroup="SPAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ObjectDataSP" runat="server" SelectMethod="GetAllProducts"
                    TypeName="IMPALLibrary.Products" OnInserting="ODSSP_Inserting" InsertMethod="AddNewProducts"
                    OnUpdating="ODSSP_Updating" UpdateMethod="UpdateProducts">
                    <UpdateParameters>
                        <asp:Parameter Name="SupplierCode" Type="String" />
                        <asp:Parameter Name="SupplierProductCode" Type="String" />
                        <asp:Parameter Name="SupplierProductShortName" Type="String" />
                        <asp:Parameter Name="SupplierProductName" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SupplierCode" Type="String" />
                        <asp:Parameter Name="SupplierProductCode" Type="String" />
                        <asp:Parameter Name="SupplierProductShortName" Type="String" />
                        <asp:Parameter Name="SupplierProductName" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSProduct" runat="server" SelectMethod="GetAllSuppliers"
                    TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
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
