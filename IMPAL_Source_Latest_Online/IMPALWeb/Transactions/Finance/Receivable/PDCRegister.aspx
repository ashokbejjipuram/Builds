<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="PDCRegister.aspx.cs"
    Inherits="IMPALWeb.PDCRegister" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <script src="../../../Javascript/PDCRegister.js" type="text/javascript"></script>
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle">
                            PDC Register
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
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Accounting Period"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAccountPeriod" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" Enabled="false" SkinID="DropDownListNormal">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Customer"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlCustomer" AutoPostBack="true" runat="server" SkinID="DropDownListNormalBig"
                                        DataSourceID="ODS_Customer" DataTextField="Customer_Name" DataValueField="Customer_Code"
                                        OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgEditToggle" ImageUrl="../../../images/ifind.png" OnClick="imgEditToggle_Click"
                                        SkinID="ImageButtonSearch" runat="server" />
                                </td>
                                <td class="label" style="display: none">
                                    <asp:Label ID="Label6" runat="server" SkinID="LabelNormal" Text="Mode Of Receipt"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols" style="display: none">
                                    <asp:DropDownList ID="ddlModeOfReceipt" runat="server" SkinID="DropDownListNormal" AutoPostBack="false" Enabled="false">
                                    </asp:DropDownList>
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
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnly();" MaxLength="6" AutoPostBack="true" OnTextChanged="txtChequeNumber_OnTextChanged"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" SkinID="LabelNormal" Text="Cheque Date"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeDate" onblur="return CheckChequeDate(this.id, true,'Cheque Date');"
                                        runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtChequeDate" OnClientShown="CheckToday" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label7" runat="server" SkinID="LabelNormal" Text="Cheque Amount"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtChequeAmount" runat="server" SkinID="TextBoxNormal" contentEditable="true" MaxLength="11"
                                        onpaste="return false;" ondragstart="return false;" ondrop="return false;" onkeypress="return CurrencyNumberOnly();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label19" runat="server" SkinID="LabelNormal" Text="Bank"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label20" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtBranch" runat="server" SkinID="TextBoxNormal" contentEditable="true"
                                        onkeypress="return AlphaNumericOnlyWithSpace();"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Cleared / UnCleared"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlClearedStatus" runat="server" SkinID="DropDownListNormal">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Cleared" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="UnCleared" Value="U"></asp:ListItem>
                                        <asp:ListItem Text="Returned to Dealer" Value="R"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" SkinID="LabelNormal" Text="Local / Out Station"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlLocalOrOutStation" runat="server" SkinID="DropDownListNormal">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Out Station" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label21" runat="server" SkinID="LabelNormal" Text="Remarks"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtremarks" runat="server" SkinID="TextBoxNormalBig" contentEditable="true"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
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
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="BtnUpdate" SkinID="ButtonNormal" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
                        <asp:Button ID="btnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                    </div>
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_Customer" runat="server" SelectMethod="GetAllCustomersForReceipts"
        TypeName="IMPALLibrary.Masters.Customers" DataObjectTypeName="IMPALLibrary.Masters.Customers">
        <SelectParameters>
            <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
