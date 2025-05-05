<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="StockTransferReceiptOnline.aspx.cs"
    Inherits="IMPALWeb.StockTransferReceiptOnline" Title="Stock Transfer Receipt" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/StockTransferReceipt.js" type="text/javascript"></script>

    <link href="<%# Page.ResolveClientUrl("~/App_Themes/ImapalMainTheme/jquery-ui.css") %>"
        rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .ui-datepicker {
            font-size: 64%;
        }
    </style>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', '1000', '480');
        }

        function CheckReferenceNo(ID, RefNo) {
            //alert(RefNo);
            PageMethods.CheckRefNo(RefNo, ifNotExists, ifExists);
        }

        function ifExists() {
            alert("Branch STDN Number already exists");
        }

        function ifNotExists() {
        }

        function TriggerJqueryCalender(txtClientId) {
            $('#' + txtClientId).datepicker(
                {
                    dateFormat: 'dd/mm/yy',
                    maxDate: 0,
                    numberOfMonths: 1,
                    onSelect: function (selected) {
                        var strArr = txtClientId.split('_');
                        var StockTansRefDtClientID = strArr[0] + '_' + strArr[1] + '_' + strArr[2] + '_' + strArr[3] + '_txtRefStockTransfeDate';
                        var OriginalRcptDtClientID = strArr[0] + '_' + strArr[1] + '_' + strArr[2] + '_' + strArr[3] + '_txtOriginalReceiptDate';
                        document.getElementById(txtClientId).focus();
                        if (txtClientId == OriginalRcptDtClientID) {
                            if (IsHigherDate(document.getElementById(txtClientId).value, document.getElementById(StockTansRefDtClientID).value)) {
                                alert('Original Receipt Date(' + document.getElementById(txtClientId).value + ') should be less than the Reference Stock Transfer Date.');
                                document.getElementById(txtClientId).value = "";
                                document.getElementById(txtClientId).focus();
                                return false;
                            }
                        }
                    }
                }
            );
        }

        function IsHigherDate(Date1, Date2) {
            var strDateArr1 = Date1.split('/');
            var strDateArr2 = Date2.split('/');

            var dtDate1 = new Date();
            var dtDate2 = new Date();
            dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
            dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

            //alert(dtDate1);
            //alert(dtDate2);
            //alert('dtDate1 is greater than dtDate2');

            if (dtDate1 >= dtDate2)
                return true;
            else
                return false;
        }
    </script>

    <div id="DivTop" runat="server" style="width: 100%">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSTDNNumberOnline" />
                <asp:AsyncPostBackTrigger ControlID="ddlStockTransferReceiptNumber" />
                <asp:PostBackTrigger ControlID="ddlBranch" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle subFormTitleExtender350">
                            VIEW STOCK TRANSFER RECEIPT - ONLINE
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTransNumber" runat="server" Text="STDN From Branch Number" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSTDNNumberOnline" runat="server" SkinID="DropDownListNormal"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSTDNNumberOnline_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <input id="hdnSTDNDate" type="hidden" runat="server" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="STDN From Branch Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtStockTransferFromDate" runat="server" SkinID="TextBoxDisabled"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle subFormTitleExtender350">
                            STOCK TRANSFER RECEIPT - ONLINE
                        </div>
                        <asp:Panel ID="STDNReceiptPanel" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                            SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Stock Transfer Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtStockTransferReceiptNumber" runat="server" SkinID="TextBoxDisabled"
                                            Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlStockTransferReceiptNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ODS_STOCKTRANSRECEIPT" DataTextField="ItemDesc" DataValueField="ItemCode"
                                            OnSelectedIndexChanged="ddlStockTransferReceiptNumber_OnSelectedIndexChanged"
                                            SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="ImageButtonSearch" runat="server" Visible="false" />
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
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="false" SkinID="DropDownListDisabled"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="From Branch"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlFromBranch" Enabled="true" runat="server" DataSourceID="ODS_AllBranch"
                                            DataTextField="BranchName" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Invoice Value"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInvoiceValue" runat="server" MaxLength="12" SkinID="TextBoxDisabled"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="IGST Value"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtIGSTValue" runat="server" MaxLength="9" SkinID="TextBoxDisabled"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Warehouse No."></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtWareHouseNo" runat="server" MaxLength="9" Enabled="false" SkinID="TextBoxDisabled"
                                            onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Warehouse Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtWarehouseDate" runat="server" Enabled="false" SkinID="TextBoxCalendarExtenderDisabled"
                                            onblur="javaScript:return Validate(this.id, 'Ware House');"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CeInwardDate" PopupButtonID="ImgDcDate" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtWarehouseDate" OnClientShown="CheckToday" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="subFormTitle">
                        CARRIER INFORMATION
                    </div>
                    <asp:Panel ID="pnlCI" runat="server">
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="LR Number"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="LR Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        onblur="javaScript:return Validate(this.id, 'LR');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtLRDate" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
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
                                    <asp:TextBox ID="txtRoadPermitNo" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Road Permit Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRoadPermitDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        onblur="javaScript:return Validate(this.id, 'Road Permit');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtRoadPermitDate" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div id="divItemDetails" runat="server">
                    <div style="display: none;">
                        <asp:TextBox runat="server" ID="hiddenTextBox1" />
                        <ajaxToolkit:CalendarExtender ID="hiddenTextBoxCE" runat="server" TargetControlID="hiddenTextBox1" />
                    </div>
                    <div class="subFormTitle">
                        ITEM DETAILS
                    </div>
                    <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="gridViewScrollFullPage">
                                <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_RowDataBound" SkinID="GridViewScroll">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                        <asp:TemplateField HeaderText="Supplier Line">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" SkinID="TextBoxDisabledBig" Width="110"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Part #">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabled" Width="145"> </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Orig Rcpt Dt" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOriginalReceiptDate" runat="server" SkinID="GridViewTextBox" Width="80" AutoPostBack="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="List Price" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtListPrice" runat="server" SkinID="GridViewTextBox" Width="80" AutoPostBack="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Price/Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCstPricePerQty" contentEditable="true" runat="server" SkinID="TextBoxNormal" Width="80"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReceivedQuantity" runat="server" SkinID="TextBoxNormal" Width="80" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accepted Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAcceptedQuantity" runat="server" SkinID="TextBoxNormal" Width="80" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemLocation" contentEditable="true" runat="server" SkinID="GridViewTextBox" Width="110" MaxLength="10"> </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tot Cost Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalCostPrice" Enabled="false" runat="server" Width="80" SkinID="TextBoxDisabledSmall"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OS/LS">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOSLSIndicator" Enabled="false" runat="server" Width="30" SkinID="GridViewTextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGSTPercentage" runat="server" SkinID="TextBoxNormalSmall" Enabled="true" Width="50" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                                <asp:HiddenField ID="hdnTaxGroupCode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supp. Inv #" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvoiceNumber" contentEditable="true" runat="server" SkinID="GridViewTextBox" Width="100"></asp:TextBox>
                                                <asp:TextBox ID="hdnConsInwardNo" Visible="false" runat="server" />
                                                <asp:TextBox ID="hdnConsSerialNo" Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supp. Inv Dt" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvoiceDate" contentEditable="true" runat="server" onblur="return CheckOrginalreciptdate(this.id, true,'Invoice Date');"
                                                    SkinID="GridViewTextBox" Width="100"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="calExtInvoiceDate" runat="server" EnableViewState="true"
                                                    Format="dd/MM/yyyy" TargetControlID="txtInvoiceDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                            <input id="hdnRowCnt" type="hidden" runat="server" />
                            <input id="hdnFooterCostPrice" type="hidden" runat="server" />
                            <input id="hdnFooterTaxValue" type="hidden" runat="server" />
                            <input id="hdnSelItemCode" type="hidden" runat="server" />
                            <input id="hdninterStateStatus" type="hidden" runat="server" />
                            <input id="hdnReceivedStatus" type="hidden" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" SkinID="ButtonNormal" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                    </div>
                </div>
                <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranches"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_STOCKTRANSRECEIPT" runat="server" SelectMethod="GetSTDNReceiptEntries"
        TypeName="IMPALLibrary.Transactions.StockTransferReceiptTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferReceiptTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/StockTransferReceipt/TransactionType"></asp:XmlDataSource>
</asp:Content>
