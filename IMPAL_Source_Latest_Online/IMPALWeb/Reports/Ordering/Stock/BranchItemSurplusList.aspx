<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchItemSurplusList.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Stock.Branch_Item_Surplus_List" MasterPageFile="~/Main.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle reportFormTitleExtender350">
        Zone/Branch - Surplus Stock</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                 <td class="label">
                    <asp:Label ID="lblZone" runat="server" SkinID="LabelNormal" Text="Zone"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged"
                        SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2"
                        AutoPostBack="True" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                     OnSelectedIndexChanged="ddlBranch_OnSelectedIndexChanged" TabIndex="3" 
                     AutoPostBack="True" SkinID="DropDownListNormal" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplier" Text="Supplier" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" DataTextField="SupplierName" DataValueField="SupplierCode"
                        OnSelectedIndexChanged="ddlSupplier_OnSelectedIndexChanged" TabIndex="4" AutoPostBack="True"
                        SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblSuppPartno" Text="Supp.PartNo" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                     <asp:TextBox ID="txtSupplierPartNumber" SkinID="TextBoxNormalPopup" runat="server" AutoPostBack="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAutoComple" runat="server" Style="display: none;" OnClick="btnAutoComple_Click" />
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" TabIndex="6" SkinID="ButtonViewReport" />
        </div>
    </div>
</asp:Content>
