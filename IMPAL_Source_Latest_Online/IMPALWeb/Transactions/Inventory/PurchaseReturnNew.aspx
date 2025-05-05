<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="PurchaseReturnNew.aspx.cs"
    Inherits="IMPALWeb.PurchaseReturnNew" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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

    <script src="../../Javascript/PurchaseReturnNew.js" type="text/javascript"></script>

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

            if ((ddlStatusvalue == "Inactive") && (stPrRtDate == today)) {
                BtnSubmit.style.display = "inline";
                BtnSubmit.disabled = false;
            }
            else {
                BtnSubmit.style.display = "none";
                BtnSubmit.disabled = true;
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
                        <div class="subFormTitle">
                            PURCHASE RETURN - EDP
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
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="false" SkinID="DropDownListNormal"
                                            DataSourceID="ODS_Transactions" DataTextField="Desc" DataValueField="Code">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Inward Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInwardNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:DropDownList ID="ddlInwardNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInwardNumber_OnSelectedIndexChanged"
                                            SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle1" ImageUrl="../../images/ifind.png" OnClick="imgEditToggle1_Click"
                                            SkinID="ImageButtonSearch" runat="server" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Inward Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtInwardDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRefDocumentNumber" runat="server" Text="Invoice Number"
                                            SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefDocumentNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblRefDocumentDate" runat="server" Text="Invoice Date" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtRefDocumentDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label28" runat="server" SkinID="LabelNormal" Text="Received Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtReceivedDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListDisabled" DataSourceID="ODS_Suppliers"
                                            DataTextField="SupplierName" DataValueField="SupplierCode" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSupplyPlant" runat="server" SkinID="DropDownListDisabledBig" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label21" runat="server" Text="Reason For Return" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlRemarks" runat="server" SkinID="DropDownListNormal">
                                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Warranty Claim Material" Value="Warranty Claim Material"></asp:ListItem>
                                            <asp:ListItem Text="Excess Supplies / Without Ordered Material" Value="Excess Supplies / Without Ordered Material"></asp:ListItem>
                                            <asp:ListItem Text="Pre-fitment Claim Material" Value="Pre-fitment Claim Material"></asp:ListItem>
                                            <asp:ListItem Text="Material Received from Service Centre" Value="Material Received from Service Centre"></asp:ListItem>
                                            <asp:ListItem Text="Materials returned to Factory (As per HO Instruction)" Value="Materials returned to Factory (As per HO Instruction)"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:Panel ID="pnlStatus" Visible="false" runat="server">
                                        <td class="label">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                                onchange="javaScript:return StatusChange(this);">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                                <%--<asp:ListItem Text="Active" Value="Active"></asp:ListItem>--%>
                                                <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </asp:Panel>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="subFormTitle">
                            DELIVERY CHALLAN
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="DC Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDCNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="DC Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDcDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label"></td>
                                <td class="inputcontrols"></td>
                            </tr>
                        </table>
                        <!-- Section 3 - CARRIER INFORMATION -->
                        <div class="subFormTitle">
                            CARRIER INFORMATION
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="LR Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="LR Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="Place of Despatch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPlaceOfDespatch" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Weight"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtWeight" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Number of cases"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtNoOfCases" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trRoadPermitDetails" runat="server">
                                <td class="label">
                                    <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Road Permit Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRoadPermitNo" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Road Permit Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtRoadPermitDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label"></td>
                                <td class="inputcontrols"></td>
                            </tr>
                        </table>
                        <!-- Section 4 - Freight DETAILS -->
                        <div class="subFormTitle">
                            FREIGHT DETAILS
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="Freight Amount"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFreightAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label23" runat="server" SkinID="LabelNormal" Text="Freight Tax"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFreightTax" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label24" runat="server" SkinID="LabelNormal" Text="Insurance"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtInsurance" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label25" runat="server" SkinID="LabelNormal" Text="Postal Charges"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPostalCharges" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label26" runat="server" SkinID="LabelNormal" Text="Coupon Charges"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCouponCharges" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label"></td>
                                <td class="inputcontrols"></td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            SUPPLIER INFORMATION
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress" SkinID="TextBoxMultilineDisableFiveColBig" runat="server"
                                        TextMode="MultiLine" Enabled="false" Height="90px" Width="180px"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
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
                                <td><b><font size="2">Total Purchase Return Value: </font></b><asp:Label ID="lblTotalPurchaseReturnVal" runat="server" Width="100" SkinID="LabelNormal" Style="font-size:20px; font-weight:bold;"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_RowDataBound" SkinID="GridViewScroll" PageSize="1500">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" onClick="SelectedChangeAll(this.id)" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelected" onClick="SelectedChangeCheckBox(this)" runat="server" />
                                                    <asp:HiddenField ID="hdnInwardSno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />
                                            <asp:TemplateField HeaderText="Supplier Part #">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" SkinID="TextBoxDisabledBig">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Short Descr.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemDescription" Enabled="false" runat="server" SkinID="TextBoxDisabledBig">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GRN Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInwardQuantity" runat="server" Enabled="false" SkinID="TextBoxDisabled" Width="75px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAvailableQuantity" runat="server" Enabled="false" SkinID="TextBoxDisabled" Width="75px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" SkinID="GridViewTextBox" onkeypress="return IntegerValueOnly();" OnChange="return IntegerValueOnly();" Width="75px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost Price/Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCstPricePerQty" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled" Width="75px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCostPrice" Enabled="false" runat="server" Text="" SkinID="TextBoxDisabled" Width="75px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGSTPercentage" Enabled="false" runat="server" SkinID="TextBoxDisabled" Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <input id="hdnRowCnt" type="hidden" runat="server" />
                                <input id="hdnOSLSindicator" type="hidden" runat="server" />
                                <input id="ChkStatus" runat="server" type="hidden" />
                                <input id="hdnFooterCostPrice" type="hidden" runat="server" />
                                <input id="hdnFooterTaxPrice" type="hidden" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnReport" runat="server" Text="Report" OnClick="BtnReport_Click" SkinID="ButtonNormal" OnClientClick="javaScript:return fnReportBtn()" />
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" SkinID="ButtonNormal" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                        </div>
                    </div>
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
    <asp:ObjectDataSource ID="ODS_PurchaseReturn" runat="server" SelectMethod="GetPurchaseReturnEntriesEDP"
        TypeName="IMPALLibrary.Transactions.StockTransferTransactions" DataObjectTypeName="IMPALLibrary.Transactions.StockTransferTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_Transactions" runat="server" DataFile="~/XML/StockTranReq.xml"
        XPath="/Root/PurchaseReturnEntry/TransactionType"></asp:XmlDataSource>
</asp:Content>
