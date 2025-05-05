<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="TODGeneration.aspx.cs"
    Inherits="IMPALWeb.TODGeneration" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../../Javascript/TODGeneration.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grvItemDetails.ClientID%>', '1000', '400');
        }
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDocumentNumber" />
                <asp:PostBackTrigger ControlID="BtnReport" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle">
                            TOD GENERATION
                        </div>
                        <table class="subFormTable">
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
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Document Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDocumentNumber" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDocumentNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentNumber_OnSelectedIndexChanged"
                                        SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgEditToggle" ImageUrl="../../../images/ifind.png" OnClick="imgEditToggle_Click"
                                        SkinID="ImageButtonSearch" runat="server" Visible="false" Enabled="false" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Document Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtDocumentDate" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Accounting Period"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAccountPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" OnSelectedIndexChanged="ddlAccountingPeriod_SelectedIndexChanged"
                                        AutoPostBack="true" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="DropDownListNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label26" runat="server" SkinID="LabelNormal" Text="Supply Plant"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSupplyPlant" runat="server" SkinID="DropDownListNormal" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Customer"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" runat="server" SkinID="DropDownListNormal"
                                        DataSourceID="ODS_Customer" DataTextField="Customer_Name" DataValueField="Customer_Code"
                                        OnDataBound="ddlCustomer_OnDataBound" OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="SLB Value"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSLBValue" SkinID="DropDownListNormal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSLBValue_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" SkinID="LabelNormal" Text="Market Location"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlMarketLocation" SkinID="DropDownListNormal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMarketLocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>  
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" SkinID="LabelNormal" Text="SLB Type"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSLBType" SkinID="DropDownListNormal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSLBType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Before/After CD"></asp:Label>
                                    <span class="asterix">*</span>
                                </td> 
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlBeforeAfterCD" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlBeforeAfterCD_OnSelectedIndexChanged">
                                        <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Before CD" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="After CD" Value="A"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                                <asp:Panel ID="PnlDateRangeGogo" runat="server">
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="TOD Month"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlTODMonthsGOGO" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                            OnSelectedIndexChanged="ddlTODMonthsGOGO_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </asp:Panel>                                                                                                                                                           
                            </tr>
                            <tr>
                               <td class="label">
                                    <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="TOD Percentage"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlSLBPercentage" SkinID="DropDownListNormal" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" SkinID="LabelNormal" Text="TOD Type"></asp:Label>
                                    <span class="asterix">*</span>
                                </td> 
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlTODtype" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                        <%--<asp:ListItem Text="-- Select --" Value=""></asp:ListItem>--%>
                                        <asp:ListItem Text="WithOut GST" Value="N"></asp:ListItem>
                                        <%--<asp:ListItem Text="With GST" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="Special TOD" Value="S"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <asp:Panel ID="PnlDateRangeOthers" runat="server">
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
                                </asp:Panel>  
                            </tr>
                        </table>
                    </div>
                    <div class="subFormTitle">
                        CUSTOMER INFORMATION
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label11" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
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
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGSTINNo" runat="server" SkinID="LabelNormal" Text="GSTIN No"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGSTINNo" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            DOCUMENT DETAILS
                        </div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScrollFullPage">
                                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        OnRowDataBound="grvItemDetails_RowDataBound" ShowFooter="false" SkinID="GridViewScroll">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Records Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnClick="return EnableAllCheckboxes(this.id);" Checked="true" Enabled="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkSelected" runat="server" onClick="SelectedChange(this)" Checked="true" AutoPostBack="false"
                                                        OnCheckedChanged="ChkSelected_OnCheckedChanged" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Number">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceNumber" runat="server" ReadOnly="true" SkinID="TextBoxDisabled"
                                                        Width="105" Text='<%# Bind("InvoiceNumber") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true" SkinID="TextBoxDisabled"
                                                        Width="100" Text='<%# Bind("InvoiceDate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInvoiceValue" runat="server" ReadOnly="true" SkinID="TextBoxDisabled"
                                                        Width="80" Text='<%# Bind("InvoiceValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalLabel1" runat="server" Text="<b>Total</b>"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="List Value" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtListValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Width="70" Text='<%# Bind("ListValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOD Value" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTODValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Width="70" Text='<%# Bind("TODValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSGSTPercentage" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="50" Text='<%# Bind("SGSTPer") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSGSTValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="70" Text='<%# Bind("SGSTVal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST %" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCGSTPercentage" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="60" Text='<%# Bind("CGSTPer") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST Value" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCGSTValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="70" Text='<%# Bind("CGSTVal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalLabel" runat="server" Text="<b>Total</b>"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST %" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIGSTPercentage" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="60" Text='<%# Bind("IGSTPer") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST Value" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIGSTValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabledSmall"
                                                        Width="70" Text='<%# Bind("IGSTVal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalLabel2" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total TOD Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemTotalTODValue" ReadOnly="true" runat="server" SkinID="TextBoxDisabled"
                                                        Width="90" Text='<%# Bind("ItemTotalTODValue") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTotalTODValue" runat="server" Style="width: 90px !important;" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                                </FooterTemplate>
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
                <input id="hdnStartdate" type="hidden" runat="server" />
                <input id="hdnEnddate" type="hidden" runat="server" />
                <input id="hdnTODNumber" type="hidden" runat="server" />
                <input id="ChkStatus" runat="server" type="hidden" />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnGetDocuments" SkinID="ButtonNormal" runat="server" Text="Get Documents" Width="100px" OnClick="BtnGetDocuments_Click" />
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="BtnReport" SkinID="ButtonNormal" runat="server" Text="Report" Visible="false" OnClick="BtnReport_Click" />
                        <asp:Button ID="btnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Customer" runat="server" SelectMethod="GetAllCustomers"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
