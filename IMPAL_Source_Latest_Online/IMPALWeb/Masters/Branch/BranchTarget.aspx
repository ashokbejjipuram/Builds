<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BranchTarget.aspx.cs" Inherits="IMPALWeb.BranchTarget" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Target</div>
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gv_Target" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                HorizontalAlign="Left" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" CaptionAlign="Left" SkinID="GridView" PageSize="20"
                                OnPageIndexChanging="gv_Target_PageIndexChanging" OnRowCancelingEdit="gv_Target_RowCancelingEdit"
                                OnRowCommand="gv_Target_RowCommand" OnRowDeleting="gv_Target_RowDeleting" OnRowEditing="gv_Target_RowEditing"
                                OnRowUpdating="gv_Target_RowUpdating" OnRowDataBound="gv_Target_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Line Code" SortExpression="SupplierCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLineCode" runat="server" SkinID="GridViewLabel" Text='<%# Bind("SupplierCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlLineCode" runat="server" DataSourceID="ODSSupplier" DataTextField="SupplierCode"
                                                DataValueField="SupplierCode" SkinID="GridViewDropDownListFooter">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvLineCode" ValidationGroup="BTAddGroup" runat="server"
                                                InitialValue="" ErrorMessage="Line Code Should not be null" ControlToValidate="ddlLineCode"
                                                SetFocusOnError="true" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch" SortExpression="BranchName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" SkinID="GridViewLabel" Text='<%# Bind("BranchName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODSBranch" DataTextField="BranchName"
                                                DataValueField="BranchCode" SkinID="GridViewDropDownList">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvBranch" ValidationGroup="BTAddGroup" runat="server"
                                                InitialValue="0" ErrorMessage="Please select Branch" ControlToValidate="ddlBranch"
                                                SetFocusOnError="true" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" SortExpression="Budget Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" SkinID="GridViewLabel" Text='<%# Bind("Year") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtYear" runat="server" SkinID="GridViewTextBox"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="rvNewYear" Display="Dynamic" runat="server" ControlToValidate="txtYear"
                                                SetFocusOnError="true" ErrorMessage="Year Required" ValidationGroup="BTAddGroup"
                                                SkinID="GridViewLabelError">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtYear" ErrorMessage="Year must be between 1900 and 2100"
                                                ID="regyear" runat="server" SetFocusOnError="true" ValidationGroup="BTAddGroup"
                                                SkinID="GridViewLabelError" ValidationExpression="^(19|20)\d{2}$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TargetAmount" SortExpression="Target Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTarget" runat="server" SkinID="GridViewLabel" Text='<%# Bind("TargetAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ValidationGroup="BTEditGroup" ID="txtTargetAmount" Text='<%# Bind("TargetAmount") %>'
                                                runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewTarget" Display="Dynamic" runat="server" ControlToValidate="txtTargetAmount"
                                                SkinID="GridViewLabelError" ErrorMessage="Target Amount Required" ValidationGroup="BTEditGroup">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTargetAmount"
                                                ErrorMessage="Target amount must be numeric" ID="regTarget" runat="server" SetFocusOnError="true"
                                                ValidationGroup="BTEditGroup" SkinID="GridViewLabelError" ValidationExpression="^(\d{0,9})(.\d{2})?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTarget" runat="server" SkinID="GridViewTextBox"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTarget" ErrorMessage="Target amount must be numeric"
                                                ID="regTarget" runat="server" SetFocusOnError="true" ValidationGroup="BTAddGroup"
                                                SkinID="GridViewLabelError" ValidationExpression="^(\d{0,9})(.\d{2})?$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rvNewTarget" Display="Dynamic" runat="server" ControlToValidate="txtTarget"
                                                SkinID="GridViewLabelError" ErrorMessage="Target Amount Required" ValidationGroup="BTAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Amount" SortExpression="ActualAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActual" runat="server" Enabled="False" SkinID="GridViewLabel" Text='<%# Bind("ActualAmount") %>'></asp:Label>
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
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabelEmptyRow" Text="No Data Found"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ObjectDataSource ID="ODSSupplier" runat="server" SelectMethod="GetAllLine" TypeName="IMPALLibrary.BranchTargets">
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ODSBranch" runat="server" SelectMethod="GetAllBranch" TypeName="IMPALLibrary.Branches">
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
