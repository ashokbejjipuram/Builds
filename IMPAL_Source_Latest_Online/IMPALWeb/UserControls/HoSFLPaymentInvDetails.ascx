<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoSFLPaymentInvDetails.ascx.cs"
    Inherits="IMPALWeb.UserControls.HoSFLPaymentInvDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div class="modalIconHolder">
    <asp:Button ID="btnEdit" runat="server" Style="width: 30px !important;" Text="Edit"
        SkinID="ButtonNormal" OnClientClick="javascript:showHOSFLPaymentChild(this.id);"
        OnClick="btnEdit_Click"></asp:Button>
    <asp:Button ID="imgPopupHidden" Style="display: none" runat="server" />
    <ajax:ModalPopupExtender ID="MPEHOSFLPayment" runat="server" TargetControlID="imgPopupHidden"
        PopupControlID="divPopup" DropShadow="false" BackgroundCssClass="modalPopupBackground">
    </ajax:ModalPopupExtender>
</div>
<div id="divPopup" class="divPopup" runat="server" style="display: none;width:870px;height:500px;overflow-y: auto;">
    <div class="modalImageExit">
        <asp:ImageButton ID="imgBtnPopupExit" runat="server" SkinID="ImageButtonSearchPopup"
            ImageUrl="~/images/iCloseGray.png" OnClick="imgBtnPopupExit_Click" />
    </div>
    <div class="modalExtenderTitle">
        DOCUMENT DETAILS</div>
    <table id="tablediv" runat="server" class="modalExtenderTable">
        <tr>
            <td class="label">
                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxNormalSmall" Enabled="false"></asp:TextBox>
            </td>
            <td class="label">
                <asp:Label ID="lblBranchTotal" runat="server" Text="Branch Total" SkinID="LabelNormal"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtBranchTotal" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
            </td>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Adj.Total" SkinID="LabelNormal"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtAdjTotal" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
            </td>
            <td class="label">
                <asp:Label ID="lblCDTotal" runat="server" Text="CD.Total" SkinID="LabelNormal"></asp:Label>
            </td>
            <td class="inputcontrols">
                <asp:TextBox ID="txtCDTotal" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                <asp:HiddenField ID="hdnCnt" runat="server" />
                <asp:HiddenField ID="hdnInvoiceNumbers" runat="server" />
            </td>
        </tr>
    </table>
    <div id="divItemDetails" runat="server">
        <div id="idTrans" runat="server" class="subFormTitle">
            ITEM DETAILS</div>
        <div id="idGrid" runat="server">
            <asp:GridView ID="grvHOSFLPaymentInvoiceDetails" runat="server" AutoGenerateColumns="False"
                AllowPaging="false" SkinID="GridViewTransaction">
                <EmptyDataTemplate>
                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelected" runat="server" Style="width: 30px !important;" SkinID="GridViewTextBoxBigSmall"
                                OnClick="CalculateTotalHO(this.id);" Checked='<%# Bind("Selected") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Indicator">
                        <ItemTemplate>
                            <asp:TextBox ID="txtIndicator" runat="server" Style="width: 30px !important;" SkinID="GridViewTextBox"
                                Text='<%# Bind("Indicator") %>' Enabled="false"> </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InvoiceNo#">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("InvoiceNo") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Style="width: 90px !important;" SkinID="GridViewTextBox" Text='<%# Bind("InvoiceDate") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Invoice Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("InvoiceAmount") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CD Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCDAmount" runat="server" Style="width: 100px !important;" SkinID="GridViewTextBox" Text='<%# Bind("CDAmount") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Inv.Amt - CD.Amt">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInvoiceValue" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("InvoiceValue") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CD Days">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCDDays" runat="server" Style="width: 60px !important;" SkinID="GridViewTextBox" Text='<%# Bind("CDDays") %>'
                                Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <%--<div>
                <asp:Button ID="Button1" runat="server" SkinID="ButtonNormal" Text="Submit" OnClientClick="return PassValuesBack();" />
                <asp:HiddenField ID="hdnInvoiceNo" runat="server" />
                <asp:HiddenField ID="hdnBranch" runat="server" />
                <asp:HiddenField ID="hdnSupplier" runat="server" />
            </div>--%>
        </div>
    </div>
    <div class="modalExtenderButtons">
        <div class="modalExtenderButtonsHolder">
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormalPopup"
                OnClick="BtnSubmit_Click" />
        </div>
    </div>
</div>
