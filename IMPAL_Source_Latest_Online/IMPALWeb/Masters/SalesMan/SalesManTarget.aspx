<%@ Page Title="Sales Man Target Master" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="SalesManTarget.aspx.cs" Inherits="IMPALWeb.SalesManTarget" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Sales Man Target Master
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesMan" Text="Sales Man" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpSalesMan1" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="drpSalesMan1_SelectedIndexChanged">
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
                    <asp:GridView ID="GV_GLMaster" runat="server" AutoGenerateColumns="False" SkinID="GridViewScroll"
                        OnPageIndexChanging="GV_GLMaster_PageIndexChanging" OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit"
                        OnRowCreated="GV_GLMaster_RowCreated" OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <%---- for Sales Man Details ----%>
                            <asp:TemplateField HeaderText="SalesMan Code " SortExpression="SalesManCode">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSalesManCode" runat="server" Text='<%# Bind("SalesManCode") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesManCode" runat="server" Text='<%# Bind("SalesManCode")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblNewSalesManCode" runat="server" SkinID="GridViewLabel">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SalesMan Name" SortExpression="SalesManCode">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSalesManName" runat="server" Text='<%# Bind("SalesManName") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesManName" runat="server" Text='<%# Bind("SalesManName")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpSalesMan" SkinID="DropDownListNormal" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="drpSalesMan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%---- Customer Information -- --%>
                            <asp:TemplateField HeaderText="Customer Code " SortExpression="CustomerCode">
                                <EditItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Bind("CustomerCode") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Bind("CustomerCode")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblNewCustomerCode" runat="server" SkinID="GridViewLabel">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="CusstomerName">
                                <EditItemTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpCustomer" SkinID="DropDownListNormal" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%---- for Supplier Line Details--%>
                            <asp:TemplateField HeaderText="Supplier Line" SortExpression="SupplierLine">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSupplierLine" runat="server" Text='<%# Bind("SupplierLinecode") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierLine" runat="server" Text='<%# Bind("SupplierLinecode")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblNewSupplierLine" runat="server" SkinID="GridViewLabel">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Description" SortExpression="SupplierLine">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SupplierName")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpSupplierLine" SkinID="DropDownListNormal" AutoPostBack="true"
                                        runat="server" OnSelectedIndexChanged="drpSupplierLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year" SortExpression="Year">
                                <EditItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text='<%# Bind("Year") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text='<%# Bind("Year")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewYear" runat="server" Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Amount" SortExpression="TargetAmount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTargetAmt" runat="server" Text='<%# Bind("TargetAmt") %>' SkinID="GridViewLabel">
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTargetAmt" runat="server" Text='<%# Bind("TargetAmt")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewTagetAmt" runat="server" Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Amount" SortExpression="ActualAmount">
                                <EditItemTemplate>
                                    <asp:Label ID="lblActualAmt" runat="server" Text='<%# Bind("Actual") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActualAmt" runat="server" Text='<%# Bind("Actual")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewActualAmt" runat="server" Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses" SortExpression="Expenses">
                                <EditItemTemplate>
                                    <asp:Label ID="lblExpenses" runat="server" Text='<%# Bind("Expenses") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenses" runat="server" Text='<%# Bind("Expenses")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewExpenses" runat="server" Wrap="False" SkinID="GridViewTextBox"></asp:TextBox>
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
                                    <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        ValidationGroup="SalesManEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="btAdd" runat="server" Text="Add" CommandName="Insert" SkinID="GridViewButton" />
                                    <%--<asp:Button ID="btAdd" runat="server" CommandName="Insert" 
                                        onclick="btAdd_Click" Text="Add" />--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
