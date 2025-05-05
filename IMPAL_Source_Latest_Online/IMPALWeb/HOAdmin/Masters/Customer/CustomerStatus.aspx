<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CustomerStatus.aspx.cs"
    Inherits="IMPALWeb.CustomerStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function FnValidate() {
            var ddlBranch = document.getElementById('ctl00_CPHDetails_ddlBranch');
            var ddlCustomer = document.getElementById('ctl00_CPHDetails_ddlCustomer');
            var ddlCustomerStatus = document.getElementById('ctl00_CPHDetails_ddlCustomerStatus');

            if (ddlBranch.value.trim() == "" || ddlBranch.value.trim() == "0" || ddlBranch.value.trim() == null) {
                alert('Please Select the Branch');
                ddlBranch.focus();
                return false;
            }

            if (ddlCustomer.value.trim() == "" || ddlCustomer.value.trim() == null) {
                alert('Please Select the Customer');
                ddlCustomer.focus();
                return false;
            }

            if (ddlCustomerStatus.value.trim() == "" || ddlCustomerStatus.value.trim() == null) {
                alert('Please Select the Customer Status');
                ddlCustomerStatus.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Customer Status
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upCusOSQuery" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCustomer" />
            </Triggers>
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
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode" AutoPostBack="true"
                                            TabIndex="1" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlBranch_OnSelectedIndexChanged" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                            SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
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
                                    Customer Status
                                </div>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label1" Text="Status" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlCustomerStatus" runat="server" SkinID="DropDownListNormal">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="reportButtons">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" OnClientClick="return FnValidate();" />
                    <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
