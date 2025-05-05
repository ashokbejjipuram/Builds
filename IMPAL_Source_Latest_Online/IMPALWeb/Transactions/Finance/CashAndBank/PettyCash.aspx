<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PettyCash.aspx.cs"
    Inherits="IMPALWeb.Finance.Fin_PettyCash" %>

<%@ Register TagPrefix="uc1" TagName="ChartAccount" Src="~/UserControls/ChartAccount.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

<script src="../../../Javascript/PettyCash.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divPettyCash" runat="server">
                    <div class="subFormTitle">
                        PETTY CASH
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPettyCashNumber" runat="server" Text="Petty Cash Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtPettyCashNumber" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>                    
                                <asp:DropDownList ID="ddlPettyCashNumber" SkinID="DropDownListNormal" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPettyCashNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPettyCashDate" runat="server" SkinID="LabelNormal" Text="Petty Cash Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtPettyCashDate" SkinID="TextBoxDisabled" runat="server"></asp:TextBox>
                            </td>
                            <td class="label">
                                &nbsp;
                            </td>
                            <td class="inputcontrols">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDescription_Name" runat="server" SkinID="LabelNormal" Text="Description / Name"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDescription_Name" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionAmount" runat="server" SkinID="LabelNormal" Text="Transaction Amount"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTransactionAmount" runat="server" onkeypress="return CurrencyNumberOnly();"
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblNoOfTransactions" SkinID="LabelNormal" runat="server" Text="No Of Transactions"></asp:Label>
                                <span class="asterix" runat="server" id="ImgNoOfTransaction">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNoOfTransactions" runat="server" onkeypress="return CurrencyNumberOnly();"
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                &nbsp;
                            </td>
                            <td class="inputcontrols">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            TRANSACTION DETAILS</div>
                        <div class="gridViewScrollFullPage">
                            <asp:GridView ID="grdPettyTransaction" runat="server" AutoGenerateColumns="false"
                                SkinID="GridViewScroll" OnRowDataBound="grdPettyTransaction_RowDataBound">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" SkinID="GridViewLabel" runat="server">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtS_No" ReadOnly="true" runat="server" SkinID="GridViewTextBoxSmall"
                                                Text='<%# Container.DataItemIndex + 1 %>'> </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chart Of Account">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChartOfAccount" runat="server" ReadOnly="true" SkinID="GridViewTextBoxBig"
                                                Text='<%# Bind("Chart_of_Account_Code") %>'> </asp:TextBox>
                                            <uc1:ChartAccount runat="server" ID="BankAccNo" DefaultBranch="true" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" onkeypress="return CurrencyNumberOnly();" Enabled="true"
                                                SkinID="GridViewTextBox" Text='<%# Bind("Amount") %>'> </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Enabled="true" SkinID="GridViewTextBoxBig" Text='<%# Bind("Remarks") %>'> </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="transactionButtons">
                            <div class="transactionButtonsHolder">
                                <asp:Button ID="btnTransactionDetails" runat="server" ValidationGroup="btnTransactionDetails"
                                    OnClientClick="return NoofValidate();" SkinID="ButtonNormalBig" CausesValidation="true"
                                    Text="Transaction Details" OnClick="btnTransactionDetails_Click" />
                                <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" OnClientClick="return Validate();"
                                    SkinID="ButtonNormal" CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                                <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                                    SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                                <asp:Button ID="btnReport" ValidationGroup="btnReport" runat="server" CausesValidation="false"
                                    SkinID="ButtonNormal" Text="Report" OnClick="btnReport_Click" />
                            </div>
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="gridViewScroll">
                                    </div>
                                    <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                                    <input id="hdnRowCnt" type="hidden" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
