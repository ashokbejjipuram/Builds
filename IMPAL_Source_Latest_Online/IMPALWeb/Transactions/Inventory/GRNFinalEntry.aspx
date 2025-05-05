<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="GRNFinalEntry.aspx.cs"
    Inherits="IMPALWeb.GRNFinalEntry" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <style type="text/css">
        .ui-datepicker
        {
            font-size: 64%;
        }
    </style>

    <script src="../../Javascript/InwardAndGRN.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {           
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', '1000', '400');
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlInwardNumber" />
            </Triggers>
            <ContentTemplate>
                <div id="divGRN" runat="server">
                    <div class="subFormTitle subFormTitleExtender250">
                        GRN FINAL ENTRY - APPROVAL</div>
                    <asp:Panel ID="GRNPanel" runat="server">
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" SkinID="DropDownListDisabled"
                                        DataSourceID="ODS_AllBranch" DataTextField="BranchName" DataValueField="BranchCode"
                                        OnSelectedIndexChanged="ddlBranch_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Inward Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlInwardNumber" runat="server" SkinID="DropDownListNormal"
                                        AutoPostBack="true" DataSourceID="ODS_GRNENTRY" DataTextField="ItemDesc" DataValueField="ItemCode"
                                        OnSelectedIndexChanged="ddlInwardNumber_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Inward Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtInwardDate" SkinID="TextBoxDisabled" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlTransactionType" runat="server" SkinID="DropDownListDisabledBig"
                                        AutoPostBack="false" DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListDisabledBig"
                                        DataSourceID="ODS_Suppliers" DataTextField="SupplierName" DataValueField="SupplierCode"
                                        OnDataBound="ddlSupplierName_OnDataBound" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Package Status"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlPackageStatus" runat="server" SkinID="DropDownListDisabled"
                                        DataSourceID="ODS_PackageStatus" DataTextField="Desc" DataValueField="Code" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Package Open Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPackageOpenDate" SkinID="TextBoxDisabled" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label15" SkinID="LabelNormal" runat="server" Text="LR Transfer"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlLRTransfer" SkinID="DropDownListDisabled" Enabled="false" runat="server" DataSourceID="ODS_LRTransfer"
                                        DataTextField="Desc" DataValueField="Code">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Purchase Tax"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPurchaseTax" SkinID="TextBoxDisabled" runat="server" Enabled="false"
                                        onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trRoadPermitDetails" runat="server">
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Road Permit Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRoadPermitNo" runat="server" SkinID="TextBoxNormal" Enabled="true" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="Road Permit Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRoadPermitDate" runat="server" SkinID="TextBoxNormal" Enabled="true"
                                        onblur="return CheckValidDate(this.id, true, 'Road Permit Date');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgRoadPermitDt"
                                        runat="server" Format="dd/MM/yyyy" TargetControlID="txtRoadPermitDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="Remarks"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRemarks" SkinID="TextBoxMultilineDisableFiveColBig" Enabled="false" runat="server"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle subFormTitleExtender300">
                            CHECKPOST CLEARING INFORMATION</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label21" runat="server" SkinID="LabelNormal" Text="Invoice Number"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" SkinID="TextBoxNormal" Enabled="true" onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                    <input id="hdnInvoiceNo" type="hidden" runat="server" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="Invoice Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="TextBoxNormal" Enabled="true"
                                        onblur="return checkDateForInvoice(this.id, 2);"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalExtInvoiceDate" PopupButtonID="ImgInvoiceDate"
                                        runat="server" Format="dd/MM/yyyy" TargetControlID="txtInvoiceDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Clearing Agent No."></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtClearingAgentNo" MaxLength="9" SkinID="TextBoxNormal" Enabled="true" runat="server"
                                        onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Check Post Name"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCheckPostName" SkinID="TextBoxNormal" Enabled="true" runat="server"
                                        onkeypress="return AlphaNumericWithSlashAndSpace();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" Text="Clearence Date" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtClearenceDate" runat="server" SkinID="TextBoxNormal" Enabled="true"
                                        onblur="return CheckValidDate(this.id, true, 'Clearence Date');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgbtnClearDt"
                                        runat="server" Format="dd/MM/yyyy" TargetControlID="txtClearenceDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Clearance Amount"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtClearenceAmount" SkinID="TextBoxNormal" Enabled="true" runat="server"
                                        onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Warehouse No."></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtWareHouseNo" runat="server" MaxLength="9" SkinID="TextBoxNormal" Enabled="true" 
                                    onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Warehouse Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtWarehouseDate" runat="server" SkinID="TextBoxNormal" Enabled="true"
                                        onblur="javaScript:return CheckValidDate(this.id, 'Ware House');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CeInwardDate" PopupButtonID="ImgDcDate" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txtWarehouseDate" OnClientShown="CheckToday" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="subFormTitle">
                        ITEM DETAILS</div>
                </div>
                <div id="divItemDetails" runat="server">
                    <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="gridViewScrollFullPage">
                                <asp:GridView ID="grvItemDetails" runat="server" ShowFooter="False" AutoGenerateColumns="False"
                                    OnRowDataBound="grvItemDetails_RowDataBound" SkinID="GridViewScroll" AllowPaging="false">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblEmptySearch" SkinID="GridViewLabel" runat="server">No Results Found</asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <%-- <asp:BoundField DataField="SNo" HeaderText="S.No" />--%>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSNo" SkinID="TextBoxDisabledSmall" Enabled="false" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Part #">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSupplierPartNo" SkinID="TextBoxDisabled" Enabled="false" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPOQuantity" SkinID="TextBoxDisabled" Enabled="false" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRcvdQty" SkinID="TextBoxDisabled" Enabled="false" runat="server"
                                                    onkeypress="return CurrencyNumberOnly();"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inward Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInwardQty" SkinID="TextBoxDisabled" Enabled="false" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accepted Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAcceptedQty" SkinID="TextBoxNormal" Enabled="true" runat="server" MaxLength="10"
                                                    onkeypress="return IntegerValueOnly();"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indicator">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtIndicator" SkinID="TextBoxDisabled" Enabled="false" runat="server"
                                                    Text=""></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shortage">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtShortage" SkinID="TextBoxDisabled" Enabled="false" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Location">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemLocation" SkinID="TextBoxNormal" Enabled="true" runat="server"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Handling Charges">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHandlingCharges" SkinID="TextBoxNormal" Enabled="true" runat="server" onkeypress="return CurrencyNumberOnly();"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" style="background-color:Green; color:White" runat="server" Text="Approve" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnReject" SkinID="ButtonNormal" style="background-color:Red; color:White" runat="server" Text="Reject" OnClick="BtnReject_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                            <asp:Button ID="btnCancel" SkinID="ButtonNormal" runat="server" Text="Cancel" OnClick="BtnCancel_Click" Visible="false" />
                        </div>
                    </div>
                    <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                    <input id="hdnRowCnt" type="hidden" runat="server" />
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="hdnOSLSvalue" type="hidden" runat="server" />
                    <input id="hdnInwardvalue" type="hidden" runat="server" />
                    <input id="hdnHdrTaxvalue" type="hidden" runat="server" />
                    <input id="hdnInsvalue" type="hidden" runat="server" />
                    <input id="hdnDiscvalue" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_GRNENTRY" runat="server" SelectMethod="GetInwardEntriesForManager"
        TypeName="IMPALLibrary.Transactions.GRNTransactions" DataObjectTypeName="IMPALLibrary.Transactions.GRNTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/InwardEntry/TransactionType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_PackageStatus" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/GRNEntry/PackageStatus"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_LRTransfer" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/GRNEntry/LRTransfer"></asp:XmlDataSource>
</asp:Content>
