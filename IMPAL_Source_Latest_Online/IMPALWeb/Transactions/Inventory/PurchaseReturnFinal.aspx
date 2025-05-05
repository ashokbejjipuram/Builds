<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="PurchaseReturnFinal.aspx.cs"
    Inherits="IMPALWeb.PurchaseReturnFinal" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="<%# Page.ResolveClientUrl("~/App_Themes/ImapalMainTheme/jquery-ui.css") %>"
        rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .ui-datepicker {
            font-size: 64%;
        }
    </style>

    <script src="../../Javascript/PurchaseReturn.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', '1000', '400');
        }

        function StatusChange(e) {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            var today = dd + '/' + mm + '/' + yyyy;

            var ddlStatusvalue = document.getElementById('<%=ddlStatus.ClientID%>').value;
            var BtnSubmit = document.getElementById('<%=BtnSubmit.ClientID%>');
            var stPrRtDate = document.getElementById('<%=txtPurchaseReturnDate.ClientID%>').value;
            if (ddlStatusvalue == "Active") {
                BtnSubmit.disabled = true;
            }
            else if ((ddlStatusvalue == "Inactive") && (stPrRtDate == today)) {
                BtnSubmit.disabled = false;
            }
        }

        function fnReportBtn() {
            document.getElementById('<%=BtnReport.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server" style="width: 100%">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtPurchaseReturnNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                <asp:PostBackTrigger ControlID="BtnReport"></asp:PostBackTrigger>
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle subFormTitleExtender250">
                            PURCHASE RETURN - APPROVAL
                        </div>
                        <asp:Panel ID="pnlSTEntry" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Enabled="false" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                            DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Purchase Return Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPurchaseReturnNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlPurchaseReturnNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ODS_PurchaseReturn" DataTextField="itemdesc" DataValueField="itemcode"
                                            OnSelectedIndexChanged="ddlPurchaseReturnNumber_OnSelectedIndexChanged" SkinID="dropdownlistnormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="imagebuttonsearch" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Purchase Return Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtPurchaseReturnDate" runat="server" SkinID="TextBoxCalendarExtenderDisabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="false" Enabled="false" SkinID="DropDownListDisabled"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" Enabled="false" SkinID="DropDownListDisabled"
                                            AutoPostBack="true" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                            DataValueField="SupplierCode" OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplyPlant" runat="server" Enabled="false" SkinID="DropDownListDisabled"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSupplyPlant_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRefDocumentNumber" runat="server" Text="Reference Document Number"
                                            SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefDocumentNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblRefDocumentDate" runat="server" Text="Reference Date" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefDocumentDate" runat="server" Enabled="false" SkinID="TextBoxCalendarExtenderDisabled"
                                            onblur="return CheckValidDate(this.id, true, 'Customer PO Date');"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceRefDocumentDate" PopupButtonID="imgCustomerPODate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtRefDocumentDate" />
                                    </td>
                                    <asp:Panel ID="pnlStatus" Visible="false" runat="server">
                                        <td class="label">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                                onchange="javaScript:return StatusChange(this);">
                                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </asp:Panel>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="subFormTitle">
                            CUSTOMER INFORMATION
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress" SkinID="TextBoxMultilineDisableFiveColBig" runat="server"
                                        TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </div>
                    <div id="divItemDetails" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div class="subFormTitle">
                                        ITEM DETAILS
                                    </div>
                                </td>
                                <td width="10">&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" Enabled="false" OnRowDataBound="grvItemDetails_RowDataBound"
                                        OnRowDeleting="grvItemDetails_OnRowDeleting" SkinID="GridViewScroll" PageSize="1500">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                            <asp:TemplateField HeaderText="Supplier Part #">
                                                <ItemTemplate>
                                                    <div class="itemResetHolder">
                                                        <div class="itemReset">
                                                            <asp:DropDownList ID="ddlSupplierPartNo" runat="server" AutoPostBack="True" Visible="false"
                                                                OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged" SkinID="GridViewDropDownList">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                        </div>
                                                        <div class="itemResetButton">
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                                SkinID="GridViewButton" Visible="false" />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add Row" Visible="false" SkinID="GridViewButtonFooter" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabledBig">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" SkinID="TextBoxDisabled" runat="server" Enabled="false" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAvailableQuantity" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost Price/Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCstPricePerQty" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCostPrice" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGSTPercentage" Enabled="false" runat="server" onkeypress="return IntegerValueOnly();"
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="false" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                                <input id="hdnRowCnt" type="hidden" runat="server" />
                                <input id="hdnOSLSindicator" type="hidden" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Approve" Style="background-color: Green; color: White" OnClick="BtnSubmit_Click" SkinID="ButtonNormal" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnReject" SkinID="ButtonNormal" Style="background-color: Red; color: White" runat="server" Text="Reject" OnClick="BtnReject_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" SkinID="ButtonNormal" />
                            <asp:Button ID="BtnReport" runat="server" Text="Report" Visible="false" OnClick="BtnReport_Click" SkinID="ButtonNormal" OnClientClick="javaScript:return fnReportBtn()" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
        <UC:CrystalReport runat="server" ID="cryPurchaseReturnReprint" OnUnload="cryPurchaseReturnReprint_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranches"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_PurchaseReturn" runat="server" SelectMethod="GetPurchaseReturnEntries"
        TypeName="IMPALLibrary.Transactions.StockTransferTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/StockTranReq.xml"
        XPath="/Root/PurchaseReturnEntry/TransactionType"></asp:XmlDataSource>
</asp:Content>
