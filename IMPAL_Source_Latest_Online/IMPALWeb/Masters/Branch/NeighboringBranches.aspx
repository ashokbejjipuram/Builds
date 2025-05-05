<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="NeighboringBranches.aspx.cs"
    Inherits="IMPALWeb.Masters.Branch.NeighboringBranches" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/DirectPOcommon.js" language="javascript" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div class="subFormTitle">
                        BRANCH DETAILS
                    </div>
                    <div id="divItemDetails" runat="server">
                        <table class="subFormTable">
                            <tr>
                                <td class="labelColSpan2">
                                    <asp:Label ID="lblBranchName" SkinID="LabelNormal" runat="server" Text="Branch Name"></asp:Label>
                                </td>
                                <td class="inputcontrolsColSpan2">
                                    <asp:DropDownList ID="ddlBranchName" SkinID="DropDownListDisabled" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            ITEM DETAILS</div>
                        <%--  <div class="gridViewScroll">--%>
                        <asp:GridView ID="grdNeighboringBranch" runat="server" SkinID="GridViewTransaction"
                            AutoGenerateColumns="False" PageSize="25" OnPageIndexChanging="grdNeighboringBranch_PageIndexChanging"
                            AllowPaging="true">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="0px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn" Value='<%# Bind("Neighboring_Branch_Code") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Neighbour Branch">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNeighboring" SkinID="GridViewTextBox" ReadOnly="true" Text='<%# Bind("Neighboring_Branch_Name") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPriority" SkinID="GridViewTextBox" Style="text-align: right"
                                            Text='<%# Bind("Priority") %>' runat="server" MaxLength="4" />
                                        <asp:CompareValidator ID="cmpPriorityValidator" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtPriority" Operator="DataTypeCheck" ValidationGroup="BtnSubmit"
                                            Type="Integer" Display="None" ErrorMessage="Priority allows only numeric digits" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="cmpPriorityValidator"
                                            PopupPosition="Left" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="CustomPriorityValidation" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtPriority" ValidationGroup="BtnSubmit" ClientValidationFunction="funPriorityMaxlimit"
                                            ErrorMessage="Priority should not be greater than 99." Display="None">
                                        </asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CustomPriorityValidation"
                                            PopupPosition="Left" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="CustomPriorityDuplicationValidation" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtPriority" ValidationGroup="BtnSubmit" ClientValidationFunction="funPriorityDuplication"
                                            ErrorMessage="Priority should not be duplicate." Display="None">
                                        </asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="CustomPriorityDuplicationValidation"
                                            PopupPosition="Left" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Freight %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFreight" SkinID="GridViewTextBox" Style="text-align: right" Text='<%# Bind("Freight_Percent") %>'
                                            runat="server" MaxLength="8" />
                                        <asp:CompareValidator ID="cmpFreightValidator" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtFreight" Operator="DataTypeCheck" ValidationGroup="BtnSubmit"
                                            Type="Double" Display="None" ErrorMessage="Freight allows only numeric digits" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="cmpFreightValidator"
                                            PopupPosition="Right" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CustomValidator ID="CustomMaxlimitFreightValidation" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtFreight" ValidationGroup="BtnSubmit" ClientValidationFunction="funMaxlimit"
                                            ErrorMessage="Freight should not max limit 1000." Display="None">
                                        </asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="CustomMaxlimitFreightValidation"
                                            PopupPosition="Right" runat="server">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%-- </div>--%>
                        <div class="transactionButtons">
                            <div class="transactionButtonsHolder">
                                <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormalBig"
                                    OnClientClick="return funPriorityDuplication();" CausesValidation="true" Text="Submit"
                                    OnClick="BtnSubmit_Click" />
                                <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                                    SkinID="ButtonNormalBig" Text="Reset" OnClick="btnReset_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                                    OnClick="btnReport_Click" />
                            </div>
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="labelColSpan2" align="center" colspan="4">
                                    <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelColSpan2">
                                </td>
                                <td class="inputcontrolsColSpan2">
                                </td>
                                <td class="labelColSpan2">
                                </td>
                                <td class="inputcontrolsColSpan2">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
