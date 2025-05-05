<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashDiscountFinal.aspx.cs" Inherits="IMPALWeb.Transactions.Finance.GeneralLedger.CashDiscountFinal" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/CashDiscount.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function fnReportBtn() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            window.document.forms[0].target = '_blank';
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divCashDiscount" runat="server">
                    <div class="subFormTitle">
                        Cash Discount
                    </div>
                    <asp:Panel ID="pnlHeader" runat="server">
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlDocumentNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlDocumentNumber_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgButtonQuery" ImageUrl="~/Images/ifind.png" runat="server"
                                        SkinID="ImageButtonSearch" OnClick="imgButtonQuery_Click" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblAccountPeriod" runat="server" Text="Accounting period" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlAccountPeriod" runat="server" AutoPostBack="True" SkinID="DropDownListDisabled"
                                        OnSelectedIndexChanged="ddlAccountPeriod_SelectedIndexChanged" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblDocumentDate" runat="server" Text="Document date" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDocumentDate" ReadOnly="true" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBranchCode" runat="server" Text="Branch code" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBranchCode" ReadOnly="true" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblSuppCustBranch" runat="server" Text="Customer " SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSuppCust" runat="server" Enabled="false" SkinID="DropDownListDisabled">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblValue" runat="server" Text="Value" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtValue" Text="0" ReadOnly="true" Enabled="false" SkinID="TextBoxDisabled" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCustomerInd" runat="server" Text="Customer Indication" SkinID="LabelNormal"></asp:Label><span
                                        class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlCustomerInd" runat="server" AutoPostBack="false" Enabled="false" SkinID="DropDownListDisabled">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:FormView ID="FormDetailCustomer" DefaultMode="ReadOnly" runat="server">
                        <ItemTemplate>
                            <div id="divItemDetails" runat="server">
                                <div class="subFormTitle">
                                    CUSTOMER INFORMATION
                                </div>
                                <table class="subFormTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("Customer_Code") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblAddress1" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Eval("address1") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress2" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress2" runat="server" Text='<%# Eval("address2") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblAddress3" runat="server" SkinID="LabelNormal" Text="Address3"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress3" runat="server" Text='<%# Eval("address3") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress4" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress4" runat="server" Text='<%# Eval("address4") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblLocation" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtLocation" runat="server" Text='<%# Eval("Location") %>' SkinID="TextBoxDisabled"
                                                ReadOnly="true"></asp:TextBox>
                                            <input id="hdnCustStatus" type="hidden" runat="server" value='<%# Eval("Status") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                    <div class="subFormTitle subFormTitleExtender300">
                        Cash Discount Details
                    </div>
                    <table>
                        <tr>
                            <td>
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grdCD" SkinID="GridViewScroll" runat="server" ShowFooter="true" AutoGenerateColumns="False"
                                        OnRowDataBound="grdCD_RowDataBound" AllowPaging="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selected">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelected" onClick="SelectedChangeFinal(this)" runat="server" />
                                                    <input id="ChkStatus" runat="server" type="hidden" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Number">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInVoiceNo" Width="90px" Text='<%# Bind("Reference_Document_Number") %>'
                                                        SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceDate" Text='<%# Bind("Reference_Document_Date") %>' SkinID="TextBoxDisabled"
                                                        ReadOnly="true" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOrderValue" Text='<%# Bind("Value") %>' SkinID="TextBoxDisabled"
                                                        ReadOnly="true" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount Collected">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmtCollected" Text='<%# Bind("Collection_Amount") %>' SkinID="TextBoxDisabled"
                                                        ReadOnly="true" runat="server" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Days">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNoOfDays" Text='<%# Bind("Days") %>' SkinID="TextBoxDisabledSmall"
                                                        ReadOnly="true" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CD%">
                                                <ItemTemplate>
                                                    <select id="drpCdPer" runat="server" onchange="CalculateCD(this);">
                                                    </select>
                                                    <asp:TextBox ID="txtCdPer" runat="server" Text='<%# Bind("CD_Per") %>' Enabled="false" SkinID="TextBoxDisabledSmall"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CD Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCDValue" Width="60px" Text='<%# Bind("Adjustment_Value") %>' SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                                    <input id="hdnCDValue" type="hidden" value="0" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delay in despatch(days)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDelayDays" Text='<%# Bind("Delay_Days") %>' onkeypress="return CurrencyNumberOnly();" SkinID="GridViewTextBoxSmall"
                                                        onblur="LoadCDPer(this);" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="grdview" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                    ShowFooter="false" SkinID="GridViewScroll" OnRowDataBound="grdview_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="invoice_number" HeaderText="Invoice Number" />
                                        <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" />
                                        <asp:BoundField DataField="order_value" HeaderText="Order Value" />
                                        <asp:BoundField DataField="collected_Amount" HeaderText="Amount Collected" />
                                        <asp:BoundField DataField="Number_of_days" HeaderText="No of Days" />
                                        <asp:BoundField DataField="cd_percent" HeaderText="CD%" />
                                        <asp:BoundField DataField="cd_value" HeaderText="CD Value" />
                                        <asp:BoundField DataField="delay_days" HeaderText="Delay in despatch(days)" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Approve" Style="background-color: Green; color: White" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnReject" SkinID="ButtonNormal" Style="background-color: Red; color: White" runat="server" Text="Reject" OnClick="BtnReject_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" OnClick="btnReport_Click" OnClientClick="javaScript:return fnReportBtn()" />
                        </div>
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                    <input id="hdnpath" type="hidden" runat="server" />
                    <input id="hdnStartdate" type="hidden" runat="server" />
                    <input id="hdnEnddate" type="hidden" runat="server" />
                    <input id="hdnValue" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
            <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
            <UC:CrystalReport runat="server" ID="cryCashDiscountReprint" OnUnload="cryCashDiscountReprint_Unload" />
        </div>
    </div>
</asp:Content>
