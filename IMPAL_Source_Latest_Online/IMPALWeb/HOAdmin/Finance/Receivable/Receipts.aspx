<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Receipts.aspx.cs"
    Inherits="IMPALWeb.HOAdmin.Finance.Receivable.Receipts" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        RECEIPTS</div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upHOReceipts" runat="server">
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