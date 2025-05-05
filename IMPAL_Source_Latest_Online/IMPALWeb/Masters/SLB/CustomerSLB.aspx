<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CustomerSLB.aspx.cs" Inherits="IMPALWeb.CustomerSLB" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "Value should not null, Please select items";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }

        } 

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Customer SLB</div>
        <asp:UpdatePanel ID="UpdPnlCustomerSLB" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_SLBGroup" runat="server" AutoGenerateColumns="False" DataSourceID="ODSSLB"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_SLBGroup_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_SLBGroup_SelectedIndexChanged" OnRowCommand="GV_SLBGroup_RowCommand"
                                OnRowUpdating="GV_SLBGroup_RowUpdating" SkinID="GridView" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer Code" SortExpression="CustCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("Custcode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCustCode" runat="server" Text='<%# Bind("Custcode") %>' Wrap="False"
                                                ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewCustCode" runat="server" Text='<%#Bind("Custcode")%>' Wrap="False"
                                                            Width="80px" ReadOnly="true"></asp:TextBox>
                                        </FooterTemplate>                                                            
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CustomerName" SortExpression="CustomerName">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCustomerName" runat="server" Text='<%# Bind("CustName") %>' Wrap="False"
                                                Width="241px"></asp:TextBox>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rvCustomerName" runat="server" ControlToValidate="txtCustomerName"
                                                ErrorMessage="Enter a valid Name" ValidationGroup="SLBEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" DataSourceID="ODSCustomer"
                                             DataTextField="CustomerName" DataValueField="CustomerCode" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="241px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CustomValidator ID="CustValName" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SLBAddGroup" ControlToValidate="ddlCustomerName"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Name should not be null"></asp:CustomValidator>
                                            <br />                                                
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewddlCustomerName" runat="server" ControlToValidate="ddlCustomerName" ErrorMessage="Name should not be null"
                                                ValidationGroup="SLBAddGroup">
                                            </asp:RequiredFieldValidator>
                                            
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustName") %>' readonly="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supp.Line Code" SortExpression="SuppCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuppCode" runat="server" Text='<%# Bind("SupplierLineCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSuppCode" runat="server" Text='<%# Bind("SupplierLineCode") %>'
                                                Wrap="False" Width="241px" ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewSuppCode" runat="server" Text='<%# Bind("SupplierLineCode") %>'
                                                Width="80px" Wrap="False" ReadOnly="true"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSuppName" runat="server" Text='<%# Bind("SupplierLine") %>' Wrap="False"
                                                Width="241px" ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODSSuppLine" DataTextField="SupplierShortName"
                                                DataValueField="SupplierLineCode" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="241px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CustomValidator ID="CustValSName" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SLBAddGroup" ControlToValidate="ddlSupplier"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Name should not be null"></asp:CustomValidator>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewddlSupplierName" runat="server" ControlToValidate="ddlSupplier" ErrorMessage="Name should not be null"
                                                ValidationGroup="SLBAddGroup">
                                            </asp:RequiredFieldValidator>
                                            
                                            
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%#Bind("SupplierLine") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SLB Code" SortExpression="SLBCode">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSLBCode" runat="server" Text='<%# Bind("SLBCode") %>' Wrap="False"
                                                Width="241px" ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                          <asp:TextBox ID="txtNewSLBCode" runat="server" Text='<%# Bind("SLBCode") %>' Wrap="False"
                                                Width="80px" ReadOnly="true"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSLBCode" runat="server" Text='<%# Bind("SLBCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SLB Desc" SortExpression="SLBDesc">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSLBDesc" runat="server" Text='<%# Bind("SLBDesc") %>' Wrap="False"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlSLB" runat="server" DataSourceID="ODSSLBMaster" DataTextField="SLB_Description"
                                                DataValueField="SLB_Code" OnSelectedIndexChanged="ddlSLB_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="241px">
                                            </asp:DropDownList>
                                            <br />
                                             <asp:CustomValidator ID="Custslb" SkinID="GridViewLabelError" runat="server"
                                                Display="Dynamic" ValidationGroup="SLBAddGroup" ControlToValidate="ddlSLB"
                                                ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Name should not be null"></asp:CustomValidator>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewSlb" runat="server" ControlToValidate="ddlSLB" ErrorMessage="Name should not be null"
                                                ValidationGroup="SLBAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSLBDesc" runat="server" Text='<%#Bind("SLBDesc") %>'></asp:Label>
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
                                                ValidationGroup="SLBEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="SLBAddGroup" />
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
                            OnClick="btnReport_Click" Visible="false" />
                        <asp:Button SkinID="ButtonViewReport" ID="btnReset" runat="server" Text="Reset"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ODSCustomer" runat="server" SelectMethod="GetCustomerList"
                    TypeName="IMPALLibrary.CustomerNames">  
                    <SelectParameters>
                    <asp:Parameter Type="String" Name ="strBranchCode" />
                    </SelectParameters>                                    
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSSuppLine" runat="server" SelectMethod="GetSupplierLineList"
                    TypeName="IMPALLibrary.SupplierLineNames"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSSLBMaster" runat="server" SelectMethod="GetSLBNameList"
                    TypeName="IMPALLibrary.SLBNames"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSSLB" runat="server" SelectMethod="GetSlbGroup" TypeName="IMPALLibrary.CustomerSLBS"
                    OnUpdating="ODSSLB_Updating" UpdateMethod="UpdateSLBGroup" OnInserting="ODSSLB_Inserting"
                    InsertMethod="AddNewSLBGroup">
                    <SelectParameters>
                        <asp:Parameter Name="strBranchCode" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Custcode" Type="String" />
                        <asp:Parameter Name="CustName" Type="String" />
                        <asp:Parameter Name="SupplierLineCode" Type="String" />
                        <asp:Parameter Name="SupplierLine" Type="String" />
                        <asp:Parameter Name="SLBCode" Type="String" />
                        <asp:Parameter Name="SLBDesc" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="CustomerCode" Type="String" />
                        <asp:Parameter Name="SupplierLineCode" Type="String" />
                        <asp:Parameter Name="SLBCode" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
