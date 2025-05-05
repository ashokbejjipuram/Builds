<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ItemLocationNew.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.ItemLocationNew" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript" src="../../JavaScript/xlsx.full.min.js"></script>
    <script src="../../Javascript/ItemLocation.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="subFormTitle">
                    ITEM LOCATION - NEW
                </div>
                <asp:Panel ID="InwardPanel" runat="server">
                    <table class="subFormTable" width="500">
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
                                <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                    DataSourceID="ODS_Suppliers" DataTextField="SupplierName" DataValueField="SupplierCode"
                                    OnSelectedIndexChanged="ddlSupplierName_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Part Number"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                                <asp:DropDownList ID="ddlSupplierPartNo" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                    SkinID="ButtonNormal" />
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Location"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" MaxLength="12"></asp:TextBox>
                                <input id="hdnViewState" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Add" OnClick="BtnSubmit_Click"
                            SkinID="ButtonNormal" OnClientClick="return FieldValidationSubmit();" />
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click"
                            SkinID="ButtonNormal" OnClientClick="return FieldValidationSubmit();" />
                        <asp:Button ID="BtnReport" runat="server" Text="Report" SkinID="ButtonNormal"
                            OnClientClick="return FieldValidationReport();" OnClick ="BtnReport_Click" />
                        <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" SkinID="ButtonNormal" />
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersGST"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
