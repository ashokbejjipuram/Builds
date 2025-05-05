<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CustomerOutstanding.aspx.cs"
    Inherits="IMPALWeb.CustomerOutstanding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
//        function Reset() {            
//            var divCustomerInfo = document.getElementById('<%=divCustomerInfo.ClientID%>');
//            var divOSdetails = document.getElementById('<%=divOSdetails.ClientID%>');
//            divCustomerInfo.style.display = 'none';
//            divOSdetails.style.display = 'none';
//            
//            var cmbCustomerName = document.getElementById('<%=cmbCustomerName.ClientID%>');
        //            cmbCustomerName.selectedIndex = 0;
        //            cmbCustomerName.disabled = false;
        //            
        //            return false;
        //        }

        function FnValidate() {
            var cmbCustomerName = document.getElementById('<%=cmbCustomerName.ClientID%>_cmbCustomerName_TextBox');
            if (cmbCustomerName.value.trim() == "" || cmbCustomerName.value.trim() == null) {
                alert('Please Select a Customer');
                return false;
            }
        }

        function preventEnterKeyOnCombo() {
            var AsciiValue = event.keyCode;

            if (AsciiValue == 13) {
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Customer Outstanding Query
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upCusOSQuery" runat="server">
            <ContentTemplate>
                <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                    <tr>
                        <td>
                            <table id="reportFiltersTable1" class="reportFiltersTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                                            TabIndex="1" Enabled="false" SkinID="DropDownListNormal" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <ajaxToolkit:ComboBox ID="cmbCustomerName" runat="server" AutoPostBack="true" SkinID="ComboBoxNormal" OnTextChanged="cmbCustomerName_SelectedIndexChanged" Width="300px"
                                            DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" CaseSensitive="False" ItemInsertLocation="Append" OnKeyDown="javascript:return preventEnterKeyOnCombo();"
                                            OnPaste="javascript:return false;">
                                        </ajaxToolkit:ComboBox>
                                    </td>
                                </tr>
                            </table>
                            <div id="divCustomerInfo" style="display: none" runat="server">
                                <div class="reportFormTitle">
                                    Customer Information
                                </div>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblGSTIN" Text="GSTIN No" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPinCode" Text="Pin Code" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtPinCode" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhone" Text="Phone" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divOSdetails" style="display: none" runat="server">
                                <div class="reportFormTitle">
                                    Outstanding Details
                                </div>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label" colspan="2">
                                            <asp:Label runat="server" ID="lblOsMessage" SkinID="LabelNormal" Style="display: none" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label1" Text="Credit Limit" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCreditLimit" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label Text="Outstanding" SkinID="LabelNormal" runat="server" ID="Label2" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtOutstanding" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label3" Text="Above 180 Days" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAbove180" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label4" Text="91 - 180 Days" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAbove90" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label Text="61 - 90 Days" SkinID="LabelNormal" runat="server" ID="Label5" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAbove60" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label6" Text="31 - 60 Days" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAbove30" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label7" Text="0 - 30 Days" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCurrentBal" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label8" Text="Credit Balance" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCrBal" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportButtons">
            <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
        </div>
    </div>
</asp:Content>
