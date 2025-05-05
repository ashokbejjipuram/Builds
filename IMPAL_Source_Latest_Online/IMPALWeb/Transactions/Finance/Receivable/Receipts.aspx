<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Receipts.aspx.cs"
    Inherits="IMPALWeb.Receipts" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/ReceivableReceipts.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnReportBtn() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlReceiptNumber" />
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnReport" />
                <asp:PostBackTrigger ControlID="btnReportExcel" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle">
                            RECEIPTS
                        </div>
                        <table id="reportFiltersTable" class="subFormTable" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label5" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" DataSourceID="ODS_AllBranch"
                                        DataTextField="BranchName" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Receipt Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtReceiptNumber" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                                    <asp:DropDownList ID="ddlReceiptNumber" runat="server" AutoPostBack="true" DataSourceID="ODS_FYRecReceiptNumber"
                                        DataTextField="ItemDesc" DataValueField="ItemCode" OnSelectedIndexChanged="ddlReceiptNumber_OnSelectedIndexChanged"
                                        SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgEditToggle" ImageUrl="../../../images/ifind.png" OnClick="imgEditToggle_Click"
                                        SkinID="ImageButtonSearch" runat="server" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Receipt Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtReceiptDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Accounting Period"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAccountPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" OnSelectedIndexChanged="ddlAccountingPeriod_SelectedIndexChanged" AutoPostBack="true" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Customer"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" runat="server" SkinID="DropDownListNormalBig"
                                        DataSourceID="ODS_Customer" DataTextField="Customer_Name" DataValueField="Customer_Code"
                                        OnDataBound="ddlCustomer_OnDataBound" OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Mode Of Receipt"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlModeOfReceipt" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlModeOfReceipt_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Amount"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true" onpaste="return false;" ondragstart="return false;" ondrop="return false;"
                                        MaxLength="11" onkeypress="return CurrencyNumberOnly();" OnTextChanged="txtAmount_OnTextChanged"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label24" runat="server" SkinID="LabelNormal" Text="HO NEFT Details"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlHoRefNo" runat="server" SkinID="DropDownListNormalBig"
                                        OnSelectedIndexChanged="ddlHoRefNo_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Temporary Recpt. Number"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtTempRecptNumber" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        MaxLength="10" onkeypress="return AlphaNumericWithSlash();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="Temporary Recpt. Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtTempRecptDate" onblur="return CheckTempRecptDate(this.id, true,'Temporary Recpt. Date');"
                                        runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtTempRecptDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label25" runat="server" SkinID="LabelNormal" Text="Advance Amount"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAdvanceAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        MaxLength="11" onkeypress="return CurrencyNumberOnly();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                </td>
                                <td class="label" runat="server" id="advChqslipDetls1">
                                    <asp:Label ID="Label26" runat="server" SkinID="LabelNormal" Text="Advance ChequeSlip #"></asp:Label>
                                </td>
                                <td class="inputcontrols" runat="server" id="advChqslipDetls2">
                                    <asp:TextBox ID="txtAdvanceChequeSlipNo" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CHEQUE/DRAFT DETAILS
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Cheque Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnly();" MaxLength="6" OnTextChanged="txtChequeNumber_OnTextChanged"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeDate" onblur="return CheckChequeDate(this.id, true,'Cheque Date');"
                                        runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtChequeDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Local / Out Station"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols" colspan="3">
                                    <asp:DropDownList ID="ddlLocalOrOutStation" runat="server" SkinID="DropDownListNormal">
                                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Out Station" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="subFormTitle">
                        CUSTOMER INFORMATION
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Customer Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Address 1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="Address 2"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Address 3"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Address 4"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        OUTSTANDING DETAILS
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="Label27" Text="Credit Limit" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCreditLimit" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                            </td>
                            <td class="label">
                                <asp:Label Text="Outstanding" SkinID="LabelNormal" runat="server" ID="Label28" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOutstanding" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                            </td>
                            <td class="label">
                                <asp:Label runat="server" ID="Label29" Text="Above 180 Days" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAbove180" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                <asp:Label runat="server" ID="lblAbove180" SkinID="LabelNormal" style="font-size: 13px; color:red; font-weight:bold; font-family:Arial" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="Label30" Text="91 - 180 Days" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAbove90" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                <asp:Label runat="server" ID="lblAbove90" SkinID="LabelNormal" style="font-size: 13px; color:red; font-weight:bold; font-family:Arial" />
                            </td>
                            <td class="label">
                                <asp:Label Text="61 - 90 Days" SkinID="LabelNormal" runat="server" ID="Label31" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAbove60" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                <asp:Label runat="server" ID="lblAbove60" SkinID="LabelNormal" style="font-size: 13px; color:red; font-weight:bold; font-family:Arial" />
                            </td>
                            <td class="label">
                                <asp:Label runat="server" ID="Label32" Text="31 - 60 Days" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAbove30" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                <asp:Label runat="server" ID="lblAbove30" SkinID="LabelNormal" style="font-size: 13px; color:red; font-weight:bold; font-family:Arial" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="Label33" Text="0 - 30 Days" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCurrentBal" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                <asp:Label runat="server" ID="lblCurrentBal" SkinID="LabelNormal" style="font-size: 13px; color:red; font-weight:bold; font-family:Arial" />
                            </td>
                            <td class="label">
                                <asp:Label runat="server" ID="Label34" Text="Credit Balance" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCrBal" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />                                
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            DOCUMENT DETAILS
                        </div>
                        <div>
                            <table class="subFormTable" style="width: 700px" id="tblDocDetails" runat="server">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label21" runat="server" SkinID="LabelNormal" Text="From Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtFromDate" onblur="return CheckValidDate(this.id, true,'From Date');"
                                            runat="server" ReadOnly="false" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label22" runat="server" SkinID="LabelNormal" Text="To Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtToDate" onblur="return CheckValidDate(this.id, true,'To Date');"
                                            runat="server" ReadOnly="false" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:Button ID="BtnGetDocuments" SkinID="ButtonNormal" runat="server" Text="Get Documents"
                                            Width="100px" OnClick="BtnGetDocuments_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 100%; margin-bottom: 5px" runat="server" id="tblBalanceAmount">
                                    <tr>
                                        <td class="subFormTitle">
                                            <asp:Label ID="Label23" runat="server" Text="Total Balance Amount : "></asp:Label>
                                            <asp:TextBox CssClass="subFormTitle" ID="txtTotalBalanceAmount" ReadOnly="true" runat="server"
                                                Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="gridViewScrollFullPage">
                                    <div>
                                        <asp:CheckBox ID="ChkHeader" Text="Select All" CssClass="labelSubTitle" runat="server"
                                            AutoPostBack="true" OnCheckedChanged="ChkHeader_OnCheckedChanged" />
                                    </div>
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        OnRowDataBound="grvItemDetails_RowDataBound" SkinID="GridViewScroll">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Records Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSelected" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSelected_OnCheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceType" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("ReferenceType") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Document Number">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceDocNumber" runat="server" ReadOnly="true" Text='<%# Bind("ReferenceDocumentNumber") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference Document Number1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReferenceDocNumber1" runat="server" ReadOnly="true" Text='<%# Bind("ReferenceDocumentNumber1") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDocumentDate" runat="server" ReadOnly="true" Text='<%# Bind("DocumentDate") %>'
                                                        SkinID="TextBoxDisabled"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document / Balance Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDocumentValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("DocumentValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collection Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCollectionAmount" CausesValidation="true" contentEditable="true"
                                                        runat="server" onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBox"
                                                        Text='<%# Bind("CollectionAmount") %>' onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalanceAmount" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Text='<%# Bind("BalanceAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Indicator">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlPaymentIndicator" runat="server" SkinID="DropDownListNormal"
                                                        SelectedValue='<%# Bind ("PaymentIndicator") %>'>
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="CD DISPUTE" Value="CD DISPUTE"></asp:ListItem>
                                                        <asp:ListItem Text="WARRANTY" Value="WARRANTY"></asp:ListItem>
                                                        <asp:ListItem Text="TOD" Value="TOD"></asp:ListItem>
                                                        <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden"></asp:TextBox>
                <input id="hdnRowCnt" type="hidden" runat="server" />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReport" SkinID="ButtonNormal" runat="server" Text="Report" OnClick="btnReport_Click" OnClientClick="javascript:return fnReportBtn();" />
                        <asp:Button ID="btnReportExcel" SkinID="ButtonNormal" runat="server" Text="Excel" OnClick="btnReportExcel_Click" OnClientClick="javascript:return fnReportBtn();" />
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
        <UC:CrystalReport runat="server" ID="crReceipt" OnUnload="crReceipt_Unload" ReportName="FYRecReceipt" />
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_FYRecReceiptNumber" runat="server" SelectMethod="GetReceiptsList"
        TypeName="IMPALLibrary.ReceiptTransactions" DataObjectTypeName="IMPALLibrary.ReceiptTransactions">
        <SelectParameters>
            <asp:ControlParameter Name="BranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Customer" runat="server" SelectMethod="GetAllCustomersForReceipts"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
