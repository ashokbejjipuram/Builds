<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="StockTransferEntryFinal.aspx.cs"
    Inherits="IMPALWeb.StockTransferEntryFinal" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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

    <script src="../../Javascript/StockTransfer.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', '1000', '400');
        }

        function Validate(e) {
            var prefix = "ctl00_CPHDetails_";
            var txtLRDate = document.getElementById('<%=txtLRDate.ClientID%>');
            var txtRPDate = document.getElementById('<%=txtRoadPermitDate.ClientID%>');
            if (e.id = prefix + "txtLRDate") {
                return ValidateTranDate(txtLRDate, "Disabled");
            }
            else {
                return ValidateTranDate(txtRPDate, "Disabled");
            }
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
            var stTrfrDate = document.getElementById('<%=txtStockTransferDate.ClientID%>').value;
            if (ddlStatusvalue == "Active") {
                BtnSubmit.disabled = true;
            }
            else if ((ddlStatusvalue == "Inactive") && (stTrfrDate == today)) {
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
                <asp:AsyncPostBackTrigger ControlID="txtStockTransferNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                <asp:PostBackTrigger ControlID="BtnReport" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle subFormTitleExtender300">
                            STOCK TRANSFER ENTRY - APPROVAL
                        </div>
                        <asp:Panel ID="pnlSTEntry" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                            DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Stock Transfer Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtStockTransferNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlStockTransferNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ods_stocktransentry" DataTextField="itemdesc" DataValueField="itemcode"
                                            OnSelectedIndexChanged="ddlStockTransferNumber_OnSelectedIndexChanged" SkinID="dropdownlistnormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="imagebuttonsearch" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Stock Transfer Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtStockTransferDate" runat="server" SkinID="TextBoxDisabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" Enabled="false" AutoPostBack="false" SkinID="DropDownListDisabled"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="To Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlToBranch" Enabled="false" runat="server" DataSourceID="ODS_AllBranch"
                                            DataTextField="BranchName" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Reference / CCWH Number"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCCWHNo" runat="server" SkinID="TextBoxDisabled" onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Reference / CCWH Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefStockTransfeDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
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
                        <!-- Section 3 - CARRIER INFORMATION -->
                        <div class="subFormTitle">
                            CARRIER INFORMATION
                        </div>
                        <asp:Panel ID="pnlCI" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="LR Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="LR Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRDate" runat="server" Enabled="false" SkinID="TextBoxDisabled"
                                            onchange="javaScript:return Validate(this);"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCarrier" runat="server" Enabled="false" SkinID="TextBoxDisabled" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDestination" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Road Permit Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRoadPermitNo" runat="server" Enabled="false" SkinID="TextBoxDisabled" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Road Permit Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRoadPermitDate" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            ITEM DETAILS
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_RowDataBound"
                                        OnRowDeleting="grvItemDetails_OnRowDeleting" SkinID="GridViewScroll">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                            <asp:TemplateField HeaderText="Supplier Line">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" SkinID="TextBoxDisabledBig"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListDisabledBig"
                                                        AutoPostBack="true" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                                        DataValueField="SupplierCode" OnDataBound="ddlSupplierName_OnDataBound" OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add Row" OnClick="ButtonAdd_Click"
                                                        SkinID="GridViewButtonFooter" Visible="false" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Part #">
                                                <ItemTemplate>
                                                    <div class="itemResetHolder">
                                                        <div class="itemReset">
                                                            <asp:DropDownList ID="ddlSupplierPartNo" runat="server" AutoPostBack="True" Visible="false"
                                                                OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged" SkinID="DropDownListDisabled">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtSupplierPartNo" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                                        </div>
                                                        <div class="itemResetButton" style="display: none">
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                                SkinID="GridViewButton" Visible="false" />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabledBig">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAvailableQuantity" runat="server" Enabled="false" SkinID="TextBoxDisabled"
                                                        onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transfer Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" SkinID="TextBoxDisabled" Enabled="false" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost Price/Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCstPricePerQty" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCostPrice" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGSTPercentage" Enabled="false" runat="server" SkinID="TextBoxDisabled" Width="35px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGSTValue" Enabled="false" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="false" ButtonType="Button" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                                <input id="hdnRowCnt" type="hidden" runat="server" />
                                <input id="hdnFooterCostPrice" type="hidden" runat="server" />
                                <input id="hdnSelItemCode" type="hidden" runat="server" />
                                <input id="hdnRemarks" type="hidden" runat="server" />
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
        <uc:crystalreport runat="server" id="crySTDNinvoiceReprint" onunload="crySTDNinvoiceReprint_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranches"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_STOCKTRANSENTRY" runat="server" SelectMethod="GetSTDNDespatchEntries"
        TypeName="IMPALLibrary.Transactions.StockTransferTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/StockTransferEntry/TransactionType"></asp:XmlDataSource>
</asp:Content>
