<%@ Page Title="Sales Man Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SalesMan.aspx.cs" Inherits="IMPALWeb.SaleMan" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Sales Man
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesMan" Text="SalesMan Description" SkinID="LabelNormal" runat="server"></asp:Label>
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
                    Sales Man Master</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" ShowFooter="true"
                        AutoGenerateColumns="False" SkinID="GridViewScroll" OnPageIndexChanging="GV_GLMaster_PageIndexChanging"
                        OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit" OnRowCreated="GV_GLMaster_RowCreated"
                        OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                        OnRowCommand="GV_GLMaster_RowCommand" OnDataBound="GV_GLMaster_DataBound" OnRowDataBound="GV_GLMaster_RowDataBound">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblSMCode" runat="server" Text='<%# Bind("SalesManCode") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewSMCode" runat="server" Wrap="False" ReadOnly="true" SkinID="GridViewTextBox">                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvtxtNewSMCode" runat="server" ControlToValidate="txtNewSMCode"
                                        ErrorMessage="SM Code should not be null" ValidationGroup="STAddGroup" SetFocusOnError="true"
                                        Text=".">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ExtenderSMCode" TargetControlID="rvtxtNewSMCode"
                                        PopupPosition="BottomLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <EditItemTemplate>
                                    <asp:Label ID="txtName" runat="server" Text='<%# Bind("SalesManName") %>' SkinID="GridViewTextBox"
                                        ReadOnly="true">
                                    </asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("SalesManName") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="drpSName" SkinID="DropDownListNormal" runat="server" OnSelectedIndexChanged="drpSName_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" SortExpression="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewDesignation" runat="server" Wrap="False" ReadOnly="true" SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("Branch") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtBranch" runat="server" Text='<%# Bind("Branch") %>' ReadOnly="true"
                                        SkinID="GridViewTextBox"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvtxtBranch" runat="server" ControlToValidate="txtBranch"
                                        ErrorMessage="Branch should not be null" ValidationGroup="STAddGroup" SetFocusOnError="true"
                                        Text=".">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ExtenderBranch" TargetControlID="rvtxtBranch"
                                        PopupPosition="BottomLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("StartDate") %>' SkinID="GridViewTextBox" onblur="return checkDateForValidDate(this.id);">
                                    </asp:TextBox>
                                    <%--<asp:ImageButton ID="ImgbtnClearDt1" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImgbtnClearDt"
                                        runat="server" PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                                        OnClientShown="CheckToday" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewStartDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" onblur="return checkDateForValidDate(this.id);"></asp:TextBox>
                                    <%--<asp:ImageButton ID="ImgbtnClearDt2" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgbtnClearDt"
                                        runat="server" PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtNewStartDate"
                                        OnClientShown="CheckToday" />
                                 <%--   <asp:RequiredFieldValidator ID="rvtxtNewStartDate" runat="server" ControlToValidate="txtNewStartDate"
                                        ErrorMessage="StartDate should not be null" ValidationGroup="STAddGroup" SetFocusOnError="true"
                                        Text=".">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ExtenderSDate" TargetControlID="rvtxtNewStartDate"
                                        PopupPosition="BottomLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("EndDate") %>' SkinID="GridViewTextBox" onblur="return checkDateForValidDate(this.id);">   </asp:TextBox>
                                    <%--   <asp:ImageButton ID="ImgbtnClearDt3" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgbtnClearDt"
                                        runat="server" PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                        OnClientShown="CheckToday" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewEndDate" runat="server" Wrap="False" SkinID="GridViewTextBox" onblur="return checkDateForValidDate(this.id);">
                                    </asp:TextBox>
                                    <%--<asp:ImageButton ID="ImgbtnClearDt4" ImageUrl="~/Images/Calendar.png" runat="server"
                                        SkinID="ImageButtonCalendar" />--%>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImgbtnClearDt1"
                                        runat="server" PopupPosition="TopRight" Format="dd/MM/yyyy" TargetControlID="txtNewEndDate"
                                        OnClientShown="CheckToday" />
                                   <%-- <asp:RequiredFieldValidator ID="rvtxtNewEndDate" runat="server" ControlToValidate="txtNewEndDate"
                                        ErrorMessage="EndDate should not be null" ValidationGroup="STAddGroup" SetFocusOnError="true"
                                        Text=".">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ExtenderEDate" TargetControlID="rvtxtNewEndDate"
                                        PopupPosition="BottomLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditStatus"  runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <%--<ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" SkinID="GridViewLabel" Text='<%#Bind("Status")%>'>
                                    </asp:Label>
                                </ItemTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BranchCode" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrCode" hidden="true" runat="server" Text='<%#Bind("BrCode")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblBrCode1" hidden="true" runat="server" Text='' SkinID="GridViewLabel"></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', 900, 500);
        }
    </script>
</asp:Content>
