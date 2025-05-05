<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CostToCostInward.aspx.cs"
    Inherits="IMPALWeb.CostToCostInward" Title="Stock Transfer Receipt" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/CostToCostInward.js" type="text/javascript"></script>

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
                <asp:AsyncPostBackTrigger ControlID="ddlCostToCostInwardNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle">
                            COST TO COST INWARD
                        </div>
                        <asp:Panel ID="STDNReceiptPanel" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                        <span class="asterix">*</span>
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
                                        <asp:TextBox ID="txtCostToCostInwardNumber" runat="server" SkinID="TextBoxDisabled"
                                            Enabled="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlCostToCostInwardNumber" runat="server" AutoPostBack="true"
                                            DataSourceID="ODS_STOCKTRANSRECEIPT" DataTextField="ItemDesc" DataValueField="ItemCode"
                                            OnSelectedIndexChanged="ddlCostToCostInwardNumber_OnSelectedIndexChanged"
                                            SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle_Click"
                                            SkinID="ImageButtonSearch" runat="server" />
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
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="false" SkinID="DropDownListNormalBig"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="From Branch"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlFromBranch" Enabled="true" runat="server" DataSourceID="ODS_BranchCostToCost"
                                            DataTextField="BranchName" SkinID="DropDownListNormal" DataValueField="BranchCode" AutoPostBack="true" OnSelectedIndexChanged="ddlFromBranch_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Invoice Value"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInvoiceValue" runat="server" MaxLength="12" SkinID="TextBoxNormal"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSGSTValue" runat="server" SkinID="LabelNormal" Text="SGST"></asp:Label>
                                        <asp:Label ID="lblUTGSTValue" runat="server" SkinID="LabelNormal" Text="UTGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                        <asp:TextBox ID="txtUTGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label29" runat="server" SkinID="LabelNormal" Text="CGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label30" runat="server" SkinID="LabelNormal" Text="IGST"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtIGSTValue" runat="server" SkinID="TextBoxNormal" MaxLength="12"
                                            onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Ref Stock Trans No."></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCCWHNo" runat="server" contentEditable="true" SkinID="TextBoxNormal"
                                            onkeypress="return AlphaNumericWithSlash();" onblur="return CheckReferenceNo(this.ID,this.value);"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Ref Stock Trans Dt"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefStockTransfeDate" onblur="return CheckValidDate(this.id, true,'Reference Stock Transfer Date');"
                                            runat="server" SkinID="TextBoxNormal" Width="100" AutoPostBack="false"></asp:TextBox>
                                        <ajaxtoolkit:calendarextender id="calExtRefStockTransfeDate" runat="server" enableviewstate="true"
                                            format="dd/MM/yyyy" targetcontrolid="txtRefStockTransfeDate">
                                                    </ajaxtoolkit:calendarextender>
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
                                        onchange="javaScript:return Validate(this);"></asp:TextBox>
                                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy"
                                        targetcontrolid="txtLRDate" />
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
                                        onchange="javaScript:return Validate(this);"></asp:TextBox>
                                    <ajaxtoolkit:calendarextender id="CalendarExtender3" runat="server" format="dd/MM/yyyy"
                                        targetcontrolid="txtRoadPermitDate" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div id="divItemDetails" runat="server">
                    <div style="display: none;">
                        <asp:TextBox runat="server" ID="hiddenTextBox1" />
                        <ajaxtoolkit:calendarextender id="hiddenTextBoxCE" runat="server" targetcontrolid="hiddenTextBox1" />
                    </div>
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
                                                <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListNormalBig"
                                                    AutoPostBack="true" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                                    DataValueField="SupplierCode" OnDataBound="ddlSupplierName_OnDataBound" OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="ButtonAdd" runat="server" Text="Add Row" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Part #">
                                            <ItemTemplate>
                                                <div class="itemResetHolder">
                                                    <div class="itemReset">
                                                        <asp:DropDownList ID="ddlSupplierPartNo" runat="server" AutoPostBack="True" Visible="false"
                                                            OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged" SkinID="GridViewDropDownList">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                    </div>
                                                    <div class="itemResetButton">
                                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                            SkinID="GridViewButton" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabled" Width="145">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemLocation" contentEditable="true" runat="server" SkinID="GridViewTextBox" Width="110">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Original Rcpt Dt" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOriginalReceiptDate" onblur="return CheckOrginalreciptdate(this.id, true,'Original Receipt Date');"
                                                    runat="server" SkinID="GridViewTextBox" Width="100" AutoPostBack="false"></asp:TextBox>
                                                <ajaxtoolkit:calendarextender id="calExtOriginalReceiptDate" runat="server" enableviewstate="true"
                                                    format="dd/MM/yyyy" targetcontrolid="txtOriginalReceiptDate">
                                                    </ajaxtoolkit:calendarextender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Price/Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCstPricePerQty" contentEditable="true" runat="server" Text=""
                                                    onkeypress="return CurrencyNumberOnly();" SkinID="TextBoxNormal" Width="80"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReceivedQuantity" runat="server" SkinID="TextBoxNormal" Width="80" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Cost Pr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalCostPrice" Enabled="false" runat="server" Width="80" SkinID="TextBoxDisabledSmall"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OS/LS Ind">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOSLSIndicator" Enabled="false" runat="server" Width="60" SkinID="GridViewTextBox"></asp:TextBox>
                                                <asp:DropDownList ID="ddlOSLSIndicator" runat="server" Width="60" AutoPostBack="false">
                                                    <asp:ListItem Text="OS" Value="O"></asp:ListItem>
                                                    <asp:ListItem Text="LS" Value="L"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGSTPercentage" runat="server" SkinID="TextBoxNormalSmall" Enabled="true" Width="50" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                                <asp:HiddenField ID="hdnTaxGroupCode" runat="server"></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supp. Invoice Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvoiceNumber" contentEditable="true" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supp. Invoice Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvoiceDate" contentEditable="true" runat="server" onblur="return CheckOrginalreciptdate(this.id, true,'Invoice Date');"
                                                    SkinID="GridViewTextBox" Width="100"></asp:TextBox>
                                                <ajaxtoolkit:calendarextender id="calExtInvoiceDate" runat="server" enableviewstate="true"
                                                    format="dd/MM/yyyy" targetcontrolid="txtInvoiceDate">
                                                    </ajaxtoolkit:calendarextender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                            <input id="hdnRowCnt" type="hidden" runat="server" />
                            <input id="hdnFooterCostPrice" type="hidden" runat="server" />
                            <input id="hdnFooterTaxValue" type="hidden" runat="server" />
                            <input id="hdnSelItemCode" type="hidden" runat="server" />
                            <input id="hdninterStateStatus" type="hidden" runat="server" />
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
    <asp:ObjectDataSource ID="ODS_BranchCostToCost" runat="server" SelectMethod="GetCostToCostSuppliers"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_STOCKTRANSRECEIPT" runat="server" SelectMethod="GetCostToCostEntries"
        TypeName="IMPALLibrary.Transactions.StockTransferReceiptTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferReceiptTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/InwardEntry.xml"
        XPath="/Root/CostToCostInward/TransactionType"></asp:XmlDataSource>
</asp:Content>
