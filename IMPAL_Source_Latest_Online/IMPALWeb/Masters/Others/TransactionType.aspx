<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="TransactionType.aspx.cs" Inherits="IMPALWeb.Masters.Others.TransactionType" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        //To validate First character should not be symbols
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

        } 

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Transaction Type Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ObjectDSTranType" runat="server" OnInserting="ObjectDSTranType_Inserting"
                                InsertMethod="AddNewTranTypeMaster" SelectMethod="GetAllTranTypes" TypeName="IMPALLibrary.TranTypeMasters"
                                UpdateMethod="UpdateTranTypeMaster" OnUpdating="ObjectDSTranType_Updating">
                                <UpdateParameters>
                                    <asp:Parameter Name="TranTypeCode" Type="String" />
                                    <asp:Parameter Name="TranTypeDesc" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="TranTypeCode" Type="String" />
                                    <asp:Parameter Name="TranTypeDesc" Type="String" />
                                    <asp:Parameter Name="TransTypeModuleCode" Type="String" />
                                    <asp:Parameter Name="ParameterNumber" Type="String" />
                                    <asp:Parameter Name="AffectSales" Type="String" />
                                    <asp:Parameter Name="Indicator" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSParamList" runat="server" SelectMethod="GetParameterList"
                                TypeName="IMPALLibrary.TranTypeMasters"></asp:ObjectDataSource>
                            <asp:XmlDataSource ID="xmlDSModule" runat="server" DataFile="~/XML/TranTypeModule.xml"
                                XPath="/Root/TRANTYPE/MODULE"></asp:XmlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdTranType" runat="server" AllowPaging="True" ShowFooter="True"
                                AutoGenerateColumns="False" SkinID="GridViewScroll" DataSourceID="ObjectDSTranType"
                                OnRowCommand="grdTranType_RowCommand" OnRowUpdating="grdTranType_RowUpdating">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Transaction Type Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranTypeCode" runat="server" Text='<%#Bind("TranTypeCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTranTypeCode" MaxLength="3" runat="server" Text='<%#Bind("TranTypeCode")%>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldTranTypeCode" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtTranTypeCode" SetFocusOnError="true"
                                                ErrorMessage="Transaction code should not be null"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:CustomValidator ID="CustValCode" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="validate" ControlToValidate="txtTranTypeCode"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Type Desc">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTranEditDesc" runat="server" Text='<%# Bind("TranTypeDesc") %>'
                                                Wrap="False" Width="241px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldTranDesc" ValidationGroup="validateEdit"
                                                runat="server" ForeColor="Red" ControlToValidate="txtTranEditDesc" SetFocusOnError="true"
                                                ErrorMessage="Description should not be null"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:CustomValidator ID="CustValTransTypeDesc" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="validateEdit" ControlToValidate="txtTranEditDesc"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranDesc" runat="server" Text='<%#Bind("TranTypeDesc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTranDesc" runat="server" Text='<%#Bind("TranTypeDesc")%>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldTranDesc" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtTranDesc" SetFocusOnError="true"
                                                ErrorMessage="Description should not be null"></asp:RequiredFieldValidator>
                                                <br />
                                            <asp:CustomValidator ID="CustValTransTypeDesc1" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="validate" ControlToValidate="txtTranDesc"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true"></asp:CustomValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModule" runat="server" Text='<%#Bind("TransTypeModuleCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlModule" runat="server" DataSourceID="xmlDSModule" DataTextField="Desc"
                                                DataValueField="Code">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldModule" ValidationGroup="validate" runat="server"
                                                ForeColor="Red" ControlToValidate="ddlModule" SetFocusOnError="true" ErrorMessage="Module should not be null"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parameter Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParamNumber" runat="server" Text='<%#Bind("ParameterNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlParamNumber" runat="server" DataSourceID="objDSParamList"
                                                DataTextField="ParameterTypeName" DataValueField="ParameterTypeCode">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldParamNumber" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlParamNumber" SetFocusOnError="true"
                                                ErrorMessage="Parameter should not be null" InitialValue="0"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Affect Sales Fig.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAffSales" runat="server" Text='<%#Bind("AffectSales")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:RadioButton ID="rbAffSalesYes" runat="server" GroupName="AffSalesFig" Text="Yes" />
                                            &nbsp;<br />
                                            <asp:RadioButton ID="rbAffSalesNo" runat="server" GroupName="AffSalesFig" Text="No"
                                                Checked="True" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indicator">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndicator" runat="server" Text='<%#Bind("Indicator")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:RadioButton ID="rbIndiRevenue" runat="server" GroupName="GroupIndicator" Text="Revenue"
                                                Checked="True" />
                                            &nbsp;<br />
                                            <asp:RadioButton ID="rbIndiExpense" runat="server" GroupName="GroupIndicator" Text="Expense" />
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
                                                ValidationGroup="validateEdit">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="validate" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
