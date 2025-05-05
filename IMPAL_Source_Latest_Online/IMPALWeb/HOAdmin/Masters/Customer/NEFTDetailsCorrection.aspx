<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="NEFTDetailsCorrection.aspx.cs"
    Inherits="IMPALWeb.NEFTDetailsCorrection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function FnValidate(i) {
            var ddlCustomer = document.getElementById('ctl00_CPHDetails_ddlCustomer');
            var txtBankRefNo = document.getElementById('ctl00_CPHDetails_txtBankRefNo');

            if (txtBankRefNo.value.trim() == "" || txtBankRefNo.value.trim() == null) {
                alert('Please Enter the Bank / NEFT Ref. No.');
                txtBankRefNo.focus();
                return false;
            }

            if (i != '1') {
                if (ddlCustomer.value.trim() == "0" || ddlCustomer.value.trim() == "") {
                    alert('Please Select the Customer');
                    ddlCustomer.focus();
                    return false;
                }
            }
        }
    </script>

    <div class="reportFormTitle">
        NEFT Details Correction
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upCusOSQuery" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtBankRefNo" />
            </Triggers>
            <ContentTemplate>
                <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                    <tr>
                        <td>
                            <table id="reportFiltersTable1" class="reportFiltersTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBankRefNo" runat="server" Text="Bank / NEFT Ref. No." SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankRefNo" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" SkinID="GridViewButton" OnClientClick="return FnValidate('1');" />
                                    </td>
                                </tr>
                            </table>
                            <div id="divNEFTDetails" style="display: none" runat="server">
                                <div class="reportFormTitle">
                                    NEFT Details
                                </div>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                                                TabIndex="1" Enabled =" false" SkinID="DropDownListDisabled" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblNEFTDate" Text="NEFT Date" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtNEFTDate" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblAmount" Text="NEFT Amount" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRemarks" Text="Remarks" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" Width="400" Height="50" />
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Correct Customer" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                                SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblCustomerCode" Text="Correct Customer Code" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="reportButtons">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" OnClientClick="return FnValidate('2');" />
                    <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
