<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashAndBankPaymentExcel.aspx.cs" Inherits="IMPALWeb.HOAdmin.Finance.CashAndBankPaymentExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        CASH & BANK - PAYMENT</div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upHOCBPayment" runat="server">
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
                                            Enabled="false" SkinID="DropDownListNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_IndexChanged" />
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>