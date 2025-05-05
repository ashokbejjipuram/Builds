<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ProformaInvoice.aspx.cs"
    Inherits="IMPALWeb.ProformaInvoice" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../App_Themes/Styles/SalesStyles.css" rel="stylesheet" type="text/css" />

    <script src="../../Javascript/ProformaInvoice.js" type="text/javascript"></script>

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function Validate() {

            return true;
        }

        function AmountReceived() {
            var AmountReceived = 0;
        }

        function fnReportBtn() {
            document.getElementById('<%=BtnReport.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSalesInvoiceNumber" />
                <asp:PostBackTrigger ControlID="BtnReport" />
                <asp:PostBackTrigger ControlID="BtnReportOs" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <div class="subFormTitle">
                        PROFORMA INVOICE
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" Style="color: red; font-weight: bold"
                                    SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr align="right" style="display: none">
                            <td colspan="6" style="float: right; margin-right: 50px !important">
                                <asp:CheckBox ID="chkActive" runat="server" Text="Inactive" OnClick="HideButton();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Proforma Invoice #" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSalesInvoiceNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlSalesInvoiceNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceNumber_SelectedIndexChanged"
                                    SkinID="DropDownListNormal">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/iFind.png" SkinID="ImageButtonSearch"
                                    runat="server" Visible="false" style="display:none" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="ProformaInvoice Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSalesInvoiceDate" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" Enabled="false" runat="server" DataSourceID="ODS_AllBranch"
                                    DataTextField="BranchName" DataValueField="BranchCode" SkinID="DropDownListDisabled"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnStateCode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Transaction Type" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTransactionType" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCustomerName" runat="server" OnSelectedIndexChanged="ddlCustomerName_OnSelectedIndexChanged"
                                    AutoPostBack="True" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnCustOSLSStatus" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSalesReqNumber" runat="server" Text="Sales Request #" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSalesReqNumber" runat="server" SkinID="DropDownListNormal" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%--To Consider Customer Sales Request this part has been hidden --%>
                            <td class="label" style="display: none">
                                <asp:Label ID="lblLRTransfer" runat="server" Text="LR Transfer" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols" style="display: none">
                                <asp:RadioButtonList runat="server" ID="rdLRTransfer" RepeatDirection="Horizontal"
                                    SkinID="RadioButtonNormal">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSalesMan" runat="server" Text="Sales Man" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSalesMan" runat="server" SkinID="DropDownListNormal" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCashDiscount" runat="server" Text="Cash Discount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCashDiscount" runat="server" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTotalValue" runat="server" Text="Total Value" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtTotalValue" Enabled="false" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCustomerPONo" runat="server" Text="Customer PO No" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerPONo" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCustomerPODate" runat="server" Text="Customer PO Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerPODate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return CheckValidDate(this.id, true, 'Customer PO Date');"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="ceCustomerPODate" PopupButtonID="imgCustomerPODate"
                                    Format="dd/MM/yyyy" runat="server" TargetControlID="txtCustomerPODate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblRefDocument" runat="server" Text="Reference Document" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRefDocument" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label20" runat="server" Text="Courier Charges" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCourierCharges" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"
                                    onkeyup="return CurrencyDecimalOnly(this.id, event);"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label22" runat="server" Text="Insurance Charges %" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInsuranceCharges" runat="server" SkinID="TextBoxNormal" onkeypress="return IntegerValueOnly();"
                                    onblur="return CheckForValidPeriod(this.id, 'Insurance Charges');" MaxLength="3"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblVIndicator" runat="server" Text="Indicator" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlVindicator" runat="server" SkinID="DropDownListNormal">
                                    <%--<asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                    <asp:ListItem Text="NON GST" Value="N"></asp:ListItem>--%>
                                    <asp:ListItem Text="GST" Value="V"></asp:ListItem>
                                    <%--<asp:ListItem Text="GST - E1" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="GST - STU" Value="S"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        CUSTOMER INFORMATION
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Customer Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtHdnCustTownCode" Visible="false" runat="server" SkinID="TextBoxNormal"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblCustomerCreditLimit" runat="server" SkinID="LabelNormal" Text="Credit Limit"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerCreditLimit" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCustomerOutStanding" runat="server" SkinID="LabelNormal" Text="Out Standing"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerOutStanding" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCanBill" runat="server" SkinID="LabelNormal" Text="Can bill upto"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCanBillUpTo" runat="server" Enabled="false" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        SHIPPING ADDRESS
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label31" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingName" runat="server" SkinID="TextBoxNormalBig"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label32" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingAddress1" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label33" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingAddress2" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label34" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingAddress4" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label35" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingGSTIN" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label36" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtShippingLocation" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblShippingState" runat="server" SkinID="LabelNormal" Text="State"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlShippingState" runat="server" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle" style="display: none">
                        CARRIER INFORMATION
                    </div>
                    <table class="subFormTable" style="display: none">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label8" runat="server" Text="LR Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label9" runat="server" Text="LR Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return CheckValidDate(this.id, false, 'LR Date');"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgLRDate" runat="server"
                                    Format="dd/MM/yyyy" TargetControlID="txtLRDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label10" runat="server" Text="Carrier" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label11" runat="server" Text="Case Marking" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCaseMarking" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label" runat="server" Text="No. Of Cases" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNoOfCases" runat="server" SkinID="TextBoxNormal" onkeypress="return IntegerValueOnly();"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label12" runat="server" Text="Weight (UOM)" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtWeight" runat="server" SkinID="TextBoxNormal" onkeypress="return checkNum();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label16" runat="server" Text="Freight Indicator" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlFreightIndicator" runat="server" DataSourceID="ODS_FreightIndicator"
                                    DataTextField="Desc" DataValueField="Code" SkinID="DropDownListNormal" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label17" runat="server" Text="Freight Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFreightAmount" runat="server" MaxLength="12" SkinID="TextBoxNormal"
                                    onkeypress="return CurrencyNumberOnly();" onkeyup="return CurrencyDecimalOnly(this.id, event);"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label28" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" value="" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelReceipt" runat="server" CssClass="subFormPanelGrayInvisible">
                        <div class="subFormTitle">
                            Receipt Details
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" Text="Mode of Receipt" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlModeOfREceipt" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlModeOfREceipt_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cash" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Cheque" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Draft" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Pay order" Value="P"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label21" runat="server" Text="Cheque/Draft Number" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeDraftNo" runat="server" Enabled="false" SkinID="TextBoxNormal"
                                        onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label23" runat="server" Text="Cheque/Draft Date" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeDraftDt" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        Enabled="false" onblur="return CheckValidDate(this.id, false, 'Cheque/Draft Date');"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgChequeDraftDt"
                                        runat="server" Format="dd/MM/yyyy" TargetControlID="txtChequeDraftDt" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label24" runat="server" Text="Local/Outstation" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlLocalOutstation" runat="server" SkinID="DropDownListNormal">
                                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Outstation" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label25" runat="server" Text="Bank" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label26" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankBranch" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label27" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCashBillCustomer" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label29" runat="server" Text="Town" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCashBillCustomerTown" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label"></td>
                                <td class="inputcontrols"></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSalesInvoiceNumber" />
                <asp:AsyncPostBackTrigger ControlID="ddlSalesReqNumber" />
            </Triggers>
            <ContentTemplate>
                <div id="divItemDetails" runat="server">
                    <%--<div class="subFormTitle">
                        ITEM DETAILS</div>--%>
                    <table>
                        <tr>
                            <td>
                                <div class="subFormTitle">
                                    ITEM DETAILS
                                </div>
                            </td>
                            <td style="display: none">
                                <asp:Panel ID="PanelOsLsFilter" runat="server" CssClass="subFormPanelGrayInvisible">
                                    <table cellpadding="5" cellspacing="10">
                                        <tr>
                                            <td class="label" style="font-weight: bold">
                                                <asp:Label ID="lblOsLsFilter" runat="server" Text="OS LS Indicator" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList ID="ddlOsLsFilter" runat="server" Enabled="false" SkinID="DropDownListNormal">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="display: none">
                                <asp:Panel ID="PanelTaxFilter" runat="server" CssClass="subFormPanelGrayInvisible">
                                    <table cellpadding="5" cellspacing="10">
                                        <tr>
                                            <td class="label" style="font-weight: bold">
                                                <asp:Label ID="lblTaxFilter" runat="server" Text="Tax %" SkinID="LabelNormal"></asp:Label>
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:DropDownList ID="ddlTaxFilter" runat="server" Enabled="false" SkinID="DropDownListNormal">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="display: none">
                                <asp:Label ID="lblMessage1" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                                <asp:Label ID="lblPackingQuantity" Text="" runat="server" SkinID="Error"></asp:Label>
                                <asp:Label ID="lblDepotName" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_OnRowDataBound"
                            SkinID="GridViewTransaction" OnRowDeleting="grvItemDetails_RowDeleting">
                            <EmptyDataTemplate>
                                No Stock / SLB for the Items
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSNo" runat="server" SkinID="GridViewTextBoxSmall" Text='<%# Container.DataItemIndex + 1 %>'
                                            Enabled="false"> </asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter"
                                            Style="width: 40px !important;" OnClientClick="return SalesInvoiceValidation(1);" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="GridViewDropDownList"
                                            OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="lblSupplier" runat="server" Enabled="false" SkinID="GridViewTextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Part #">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlItemCode" runat="server" AutoPostBack="True" Visible="false"
                                            OnSelectedIndexChanged="ddlItemCode_OnSelectedIndexChanged" SkinID="GridViewDropDownList">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                            SkinID="GridViewButton" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OS/LS">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOSLS" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req.Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" SkinID="GridViewTextBoxSmall" onkeypress="return IntegerValueOnly();"
                                            OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtCanOrderQty" runat="server" SkinID="GridViewTextBoxSmall" Enabled="false" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLB">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSLB" runat="server" SkinID="GridViewDropDownList" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSLB_OnSelectedIndexChanged" Enabled="false">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSLB" Visible="false" runat="server" SkinID="GridViewTextBox"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch List Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBranchListPrice" runat="server" SkinID="gridviewTextBoxSmall"
                                            Enabled="false" Style="width: 80px !important;"></asp:TextBox>
                                        <asp:TextBox ID="txthCostPrice" runat="server" SkinID="gridviewTextBoxSmall" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLB/Net Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSLBNetValue" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"
                                            Style="width: 80px !important;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDiscount" runat="server" SkinID="gridviewTextBoxSmall" OnTextChanged="txtDiscount_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Tax %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSalesTax" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="txtItemSaleValue" runat="server" />
                                        <asp:HiddenField ID="txtHdnReqOrderQty" runat="server" />
                                        <asp:HiddenField ID="txtProductGroupCode" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross Profit %" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrossProfit" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"
                                            Style="width: 80px !important;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemCode" runat="server" Enabled="false" SkinID="GridViewTextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                    <input id="hdnRowCnt" type="hidden" runat="server" />
                    <asp:TextBox ID="txthdSlab" runat="server" type="hidden" Visible="false"></asp:TextBox>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                        <asp:Button ID="BtnReportOs" SkinID="ButtonNormal" runat="server" Text="OS Details"
                            OnClick="BtnReportOs_Click" />
                        <asp:Button ID="BtnReport" SkinID="ButtonNormalBig" runat="server" Text="Generate Report" OnClick="BtnReport_Click" OnClientClick="javaScript:return fnReportBtn()" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
        <UC:CrystalReport runat="server" ID="cryProformaInvoiceReprint" OnUnload="cryProformaInvoiceReprint_Unload" />
    </div>
    <input id="hdnScreenMode" type="hidden" runat="server" />
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Masters.Suppliers" DataObjectTypeName="IMPALLibrary.Masters.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:XmlDataSource ID="ODS_FreightIndicator" runat="server" DataFile="~/XML/SalesInvoice.xml"
        XPath="/Root/SalesInvoice/FreightIndicator"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_Indicator" runat="server" DataFile="~/XML/SalesInvoice.xml"
        XPath="/Root/SalesInvoice/Indicator"></asp:XmlDataSource>
</asp:Content>
