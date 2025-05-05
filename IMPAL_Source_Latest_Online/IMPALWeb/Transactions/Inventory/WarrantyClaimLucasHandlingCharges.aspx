<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WarrantyClaimLucasHandlingCharges.aspx.cs"
    Inherits="IMPALWeb.WarrantyClaimLucasHandlingCharges" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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

    <script src="../../Javascript/WarrantyClaimLucasHandlingCharges.js" type="text/javascript"></script>

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
            var stPrRtDate = document.getElementById('<%=txtWarrantyClaimDate.ClientID%>').value;
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
                <asp:AsyncPostBackTrigger ControlID="txtHandlingChargesNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                <asp:PostBackTrigger ControlID="BtnReport" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle subFormTitleExtender350">
                            WARRANTY CLAIM - LUCAS - HANDLING CHARGES
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
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Handling Charges Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtHandlingChargesNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlHandlingChargesNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ODS_HandlingChargesNumbers" DataTextField="itemdesc" DataValueField="itemcode"
                                            OnSelectedIndexChanged="ddlHandlingChargesNumber_OnSelectedIndexChanged" SkinID="dropdownlistnormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="imagebuttonsearch" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Handling Charges Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtHandlingChargesDate" runat="server" SkinID="TextBoxCalendarExtenderDisabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Warranty Claim Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlWarrantyClaimNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ODS_WarrantyClaim" DataTextField="itemdesc" DataValueField="itemcode"
                                            OnSelectedIndexChanged="ddlWarrantyClaimNumber_OnSelectedIndexChanged" SkinID="dropdownlistnormal">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Warranty Claim Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtWarrantyClaimDate" runat="server" SkinID="TextBoxCalendarExtenderDisabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" SkinID="DropDownListNormal"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListNormal"
                                            AutoPostBack="true" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                            DataValueField="SupplierCode" OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplyPlant" runat="server" SkinID="DropDownListNormal"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSupplyPlant_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label25" runat="server" SkinID="LabelNormal" Text="Handling Charges"
                                            MaxLength="12"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtHandlingCharges" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="display: none">
                                        <asp:Label ID="lblRefDocumentNumber" runat="server" Text="Reference Document Number"
                                            SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols" style="display: none">
                                        <asp:TextBox ID="txtRefDocumentNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblJobCardNumber" runat="server" Text="Job Card Number"
                                            SkinID="LabelNormal"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtJobCardNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                    <td class="label" style="display: none">
                                        <asp:Label ID="lblRefDocumentDate" runat="server" Text="Reference Date" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols" style="display: none">
                                        <asp:TextBox ID="txtRefDocumentDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return CheckValidDate(this.id, true, 'Reference Date');"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceRefDocumentDate" PopupButtonID="imgCustomerPODate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtRefDocumentDate" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblJobCardDate" runat="server" Text="Job Card Date" SkinID="LabelNormal"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtJobCardDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return CheckValidDate(this.id, true, 'Job Card Date');"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceJobCardDate" PopupButtonID="imgJobCardDate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtJobCardDate" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblShipTo" runat="server" Text="Ship To" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtShipTo" runat="server" SkinID="TextBoxMultilineNormalFiveColBig" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlStatus" Visible="false" runat="server">
                                    <tr>
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
                                    </tr>
                                </asp:Panel>
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
                                    <asp:TextBox ID="txtAddress" SkinID="TextBoxMultilineNormalFiveColBig" runat="server"
                                        TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
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
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_RowDataBound"
                                        SkinID="GridViewScroll" PageSize="1500" Enabled="false">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                            <asp:TemplateField HeaderText="Supplier Part #">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabledBig">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" SkinID="TextBoxDisabled" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Qty" Visible="false">
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
                            <asp:Button ID="BtnReport" runat="server" Text="Report" OnClick="BtnReport_Click"
                                SkinID="ButtonNormal" OnClientClick="javaScript:return fnReportBtn()" />
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"
                                SkinID="ButtonNormal" />
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
        <UC:CrystalReport runat="server" ID="cryHandlingChargesReprint" OnUnload="cryHandlingChargesReprint_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersLucas"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranches"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_WarrantyClaim" runat="server" SelectMethod="GetWarrantyClaimEntriesHandlingPending"
        TypeName="IMPALLibrary.Transactions.StockTransferTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HandlingChargesNumbers" runat="server" SelectMethod="GetHandlingChargesEntries"
        TypeName="IMPALLibrary.Transactions.StockTransferTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/StockTranReq.xml"
        XPath="/Root/WarrantyClaimHandlingChargesEntry/TransactionType"></asp:XmlDataSource>
</asp:Content>
