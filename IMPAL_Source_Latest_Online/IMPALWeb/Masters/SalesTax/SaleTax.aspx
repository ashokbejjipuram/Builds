<%@ Page Title="Sales TAX" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SaleTax.aspx.cs" Inherits="IMPALWeb.SaleTax" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

<script type="text/javascript">

    function validateFields(source, arguments) {

        //var TxtGLAcctCode = document.getElementById("ctl00_CPHDetails_GV_GLMaster_ctl27_txtNewDescription").value;

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
            Sales Tax
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesTaxDesc" Text="SalesTax Description" SkinID="LabelNormal"
                                runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpSTDesc" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="drpSTDesc_SelectedIndexChanged">
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
                <div class="subFormTitle">
                    Sales Tax Master</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" AutoGenerateColumns="False" 
                    BackColor="White" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3"
                           HorizontalAlign="Left" ShowFooter="True" SkinID="GridView" PageSize="25" 
                         OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand" OnDataBound="GV_GLMaster_DataBound">
                        
                        
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblSTCode" runat="server" Text='<%# Bind("SalesTaxCode") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("SalesTaxDescription") %>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:CustomValidator ID="CustEditGLCode" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="STEditGroup" ControlToValidate="txtDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                   <br /> 
                                    <asp:RequiredFieldValidator ID="rvSTDesc" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="Please enter a valid SalesTax Description" ValidationGroup="STEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("SalesTaxDescription") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewDescription" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:CustomValidator ID="CustNewGLCode" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="STAddGroup" ControlToValidate="txtNewDescription"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                   <br /> 
                                   
                                    <asp:RequiredFieldValidator ID="rvNewDesc" runat="server" ControlToValidate="txtNewDescription"
                                        ErrorMessage="Please enter a valid SalesTax Description" ValidationGroup="STAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                   
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SalesTax">
                                <FooterTemplate>
                                    <asp:RadioButton ID="rbCentralIndicator" runat="server" GroupName="SalesTax" SkinID="GridViewRadioButton"
                                        Text="Central" Checked="True" />
                                    &nbsp;<br />
                                    <asp:RadioButton ID="rbLocalIndicator" runat="server" GroupName="SalesTax" SkinID="GridViewRadioButton"
                                        Text="Local" />
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesTax" runat="server" Text='<%# Bind("SalesTaxIndicator") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage" SortExpression="Percentage">
                                <%-- <EditItemTemplate>
                                        <asp:TextBox ID="txtPercentage" runat="server" Text='<%# Bind("SalesTaxPercentage") %>'
                                            SkinID="GridViewTextBox">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            ErrorMessage="Please enter a valid SalesTax Percentage" ValidationGroup="STEditGroup"
                                            SkinID="GridViewLabelError">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblPercentage" runat="server" Text='<%# Bind("SalesTaxPercentage") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewPercentage" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                 <br /> 
                                    <asp:RequiredFieldValidator ID="rvNewPercentage" runat="server" ControlToValidate="txtNewPercentage"
                                        ErrorMessage="Please enter a valid SalesTax Percentage" ValidationGroup="STAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                  <br />  
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewPercentage"
                                    ErrorMessage="Code must be Numeric" ID="regtxtNewPercentage" runat="server"
                                    SetFocusOnError="true" ValidationGroup="STAddGroup" SkinID="GridViewLabelError"
                                    ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calculation Indicator">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddCalInd" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCalInd" runat="server" Text='<%# Bind("SalesTaxReference") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SalesTax Type">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlST" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblST" runat="server" Text='<%# Bind("SalesTaxType") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        ValidationGroup="STEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="STAddGroup"
                                        SkinID="GridViewButton" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
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
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', null, null);
        }
    </script>
</asp:Content>
