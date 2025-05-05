<%@ Page Title="Budget Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BudgetMaster.aspx.cs" Inherits="IMPALWeb.BudgetMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="UC" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <!-- Template for Full Page GridView -->
    <!--
    <div id="DivOuter">
        <div class="subFormTitle">GL Master</div>        
        <div class="gridViewScrollFullPage">
            -- Gridview goes here ---
        </div>  
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
            </div>
        </div>
    </div>
    -->
    <div id="DivOuter">
        <div class="subFormTitle">
            Budget Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGlGroupDesc" Text="Budget Year" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpBY" SkinID="DropDownListNormal" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="drpBY_SelectedIndexChanged">
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
                        AutoGenerateColumns="False" SkinID="GridViewScroll" OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="BudgetYear" SortExpression="BudgetYear">
                                <EditItemTemplate>
                                    <asp:Label ID="lblBudgetYear" runat="server" Text='<%# Bind("Budget_Year") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                    <%--<asp:TextBox ID="txtBudgetYear" runat="server" Text='<%# Bind("Budget_Year")%>' SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rvBudgetYear" runat="server" ControlToValidate="txtBudgetYear"
                                                    ErrorMessage="Please enter a valid BudgetYear" ValidationGroup="GLMasterEditGroup"
                                                    SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>--%>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetYear" runat="server" Text='<%# Bind("Budget_Year")%>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewBudgetYear" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNewBudgetYear" runat="server" ControlToValidate="txtNewBudgetYear"
                                        ErrorMessage="Please enter a valid BudgetYear" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ChartofAccountCode" SortExpression="ChartofAccountCode">
                                <EditItemTemplate>
                                    <asp:Label ID="lblChartOfAccountCode" runat="server" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        SkinID="GridViewLabel"></asp:Label>
                                    <%--<asp:TextBox ID="txtChartOfAccountCode" runat="server" Text='<%#Bind("Chart_of_Account_Code") %>'
                                                    SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rvChartOfAccountCode" runat="server" ControlToValidate="txtChartOfAccountCode"
                                                    ErrorMessage="Please enter a valid ChartOfAccountCode" ValidationGroup="GLMasterEditGroup"
                                                    SkinID="GridViewLabelError">
                                                </asp:RequiredFieldValidator>--%>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChartOfAccountCode" runat="server" Text='<%# Bind("Chart_of_Account_Code") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewChartOfAccountCode" runat="server" Wrap="False" ReadOnly="true"
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNewChartOfAccountCode" runat="server" ControlToValidate="txtNewChartOfAccountCode"
                                        ErrorMessage="Please enter a valid ChartOfAccountCode" SkinID="GridViewLabelError"
                                        ValidationGroup="GLMasterAddGroup">
                                    </asp:RequiredFieldValidator>
                                    <UC:ChartAccount ID="UserChart" runat="server" OnSearchImageClicked="UserChart_SearchImageClicked" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BudgetAmount" SortExpression="BudgetAmount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBudgetAmount" runat="server" Text='<%# Bind("Budget_Amount") %>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvBudgetAmount" runat="server" ControlToValidate="txtBudgetAmount"
                                        ErrorMessage="Please enter a valid BudgetAmount" ValidationGroup="GLMasterEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetAmount" runat="server" Text='<%# Bind("Budget_Amount") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewBudgetAmount" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNewBudgetAmount" runat="server" ControlToValidate="txtNewBudgetAmount"
                                        ErrorMessage="Please enter a valid BudgetAmount" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ApprovedAmount" SortExpression="ApprovedAmount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtApprovedAmount" runat="server" Text='<%# Bind("Approved_Amount") %>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvApprovedAmount" runat="server" ControlToValidate="txtApprovedAmount"
                                        ErrorMessage="Please enter a valid ApprovedAmount" ValidationGroup="GLMasterEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedAmount" runat="server" Text='<%# Bind("Approved_Amount") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewApprovedAmount" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNewApprovedAmount" runat="server" ControlToValidate="txtNewApprovedAmount"
                                        ErrorMessage="Please enter a valid Approved Amount" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActualAmount" SortExpression="ActualAmount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActualAmount" runat="server" Text='<%# Bind("Actual_Amount") %>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvActualAmount" runat="server" ControlToValidate="txtActualAmount"
                                        ErrorMessage="Please enter a valid ActualAmount" ValidationGroup="GLMasterEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActualAmount" runat="server" Text='<%# Bind("Actual_Amount") %>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewActualAmount" runat="server" Wrap="False" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rvNewActualAmount" runat="server" ControlToValidate="txtNewActualAmount"
                                        ErrorMessage="Please enter a valid Actual Amount" ValidationGroup="GLMasterAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
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
                                        ValidationGroup="GLMasterEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="GLMasterAddGroup"
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
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', 850, 400);
        }
    </script>
</asp:Content>
