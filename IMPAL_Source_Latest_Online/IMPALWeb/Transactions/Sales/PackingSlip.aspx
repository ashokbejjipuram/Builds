<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="PackingSlip.aspx.cs"
    Inherits="IMPALWeb.PackingSlip" EnableEventValidation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/PackingSlip.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSalesInvoiceNumber" />
                <asp:AsyncPostBackTrigger ControlID="imgEditToggle" />
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div id="DivHeader" runat="server">
                        <div class="subFormTitle">
                            PACKING SLIP
                        </div>
                        <asp:Panel ID="PackingPanel" runat="server">
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" Text="Sales Invoice #" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSalesInvoiceNumber" runat="server" Enabled="True" SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:DropDownList ID="ddlSalesInvoiceNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceNumber_SelectedIndexChanged"
                                            SkinID="DropDownListNormal">
                                            <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgEditToggle" ImageUrl="../../images/iFind.png" SkinID="ImageButtonSearch"
                                            runat="server" OnClick="imgEditToggle_Click" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" Text="SalesInvoice Date" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSalesInvoiceDate" runat="server" Enabled="False" SkinID="TextBoxDisabled"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" Enabled="False" runat="server" DataSourceID="ODS_AllBranch"
                                            DataTextField="BranchName" SkinID="DropDownListDisabled" DataValueField="BranchCode">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" Text="Transaction Type" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTransactionType" runat="server" Enabled="False" SkinID="DropDownListNormal"
                                            AutoPostBack="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" Enabled="False" SkinID="DropDownListNormal">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblSalesMan" runat="server" Text="Sales Man" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlSalesMan" runat="server" SkinID="DropDownListNormal" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCashDiscount" runat="server" Text="Cash Discount" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCashDiscount" runat="server" SkinID="DropDownListNormal"
                                            Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblTotalValue" runat="server" Text="Total Value" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtTotalValue" Enabled="False" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle">
                                CUSTOMER INFORMATION
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Customer Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Address1"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="Address2"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle">
                                CARRIER INFORMATION
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="LR Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label13" runat="server" SkinID="LabelNormal" Text="LR Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                            onblur="return checkDateForLRDate(this.id);"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalExtLRDate" PopupButtonID="ImgLRDate" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtLRDate" OnClientShown="CheckToday" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label14" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericWithSlashAndSpace();"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label15" runat="server" SkinID="LabelNormal" Text="Marking Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtMarkingNumber" runat="server" SkinID="TextBoxNormal" onkeypress="return AlphaNumericWithSlashAndSpace();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label16" runat="server" SkinID="LabelNormal" Text="Weight"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtWeight" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Number of cases"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtNoOfCases" runat="server" SkinID="TextBoxNormal" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">                            
                            <asp:Button ID="BtnReport" SkinID="ButtonNormal" runat="server" Text="Report"
                                OnClick="BtnReport_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
            <UC:CrystalReport runat="server" ID="crPackingSlip" ReportName="PackingSlip" />
        </div>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
