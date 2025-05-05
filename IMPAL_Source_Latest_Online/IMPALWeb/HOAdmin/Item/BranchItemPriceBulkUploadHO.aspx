<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchItemPriceBulkUploadHO.aspx.cs"
    Inherits="IMPALWeb.HOAdmin.Item.BranchItemPriceBulkUploadHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" src="../../Javascript/BranchItemPriceBulkUploadHO.js" type="text/javascript"></script>

    <div class="reportFormTitle reportFormTitleExtender300">
        Branch Item Price - Bulk Upload - HO
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:CheckBox ID="chkZone" runat="server" Checked="true" Text="" Style="display: none" />
                    <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                        TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                        Enabled="false" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkState" runat="server" Text="" Style="display: none" />
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                        AutoPostBack="True" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkBranch" runat="server" Text="" Style="display: none" />
                    <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        TabIndex="3" Enabled="false" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <br />
        <div>
            <table class="reportFiltersTable" runat="server">
                <tr id="tbluploadFile">
                    <td class="label">
                        <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:FileUpload runat="server" ID="btnFileUpload" />
                    </td>
                    <td>
                        <asp:Button ID="btnUploadExcel" runat="server" Text="Upload File" SkinID="ButtonNormal"
                            OnClick="btnUploadExcel_Click" OnClientClick="javascript: return ValidateFile();" />
                    </td>
                </tr>
                <tr>
                    <td class="label" colspan="4">
                        <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                        <asp:HiddenField runat="server" ID="HdnExcelTotalValue" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdnRowCount" runat="server" />
        <asp:HiddenField ID="hdnMissingPartNos" runat="server" />
        <div class="reportButtons">
            <asp:Button SkinID="ButtonNormal" ID="btnSubmit" runat="server" Text="Submit"
                TabIndex="3" OnClientClick="javaScript:return fnValidateSubmit();" Style="width: 80px" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormal"
                OnClick="btnReset_Click" Text="Reset" />
        </div>
    </div>
</asp:Content>
