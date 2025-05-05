<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditListforTransaction.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.EditListforTransaction" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            var ddltrancode = document.getElementById('<%=ddltrancode.ClientID%>');
            if (ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate) == false)
                return false;
            if (ValidateTransactionCode(ddltrancode) == false)
                return false;
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Edit List For Transaction Type Code</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
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
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1" />
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2" />
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblTransactionCode" runat="server" Text="Transaction Code" SkinID="LabelNormal" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
                        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
                    </asp:ObjectDataSource>
                    <asp:DropDownList ID="ddltrancode" runat="server" TabIndex="3" DataSourceID="ODtrancode"
                        DataTextField="ttdescription" DataValueField="ttcode" SkinID="DropDownListNormal" />
                    <asp:ObjectDataSource ID="ODtrancode" runat="server" SelectMethod="GetTransactionCode"
                        TypeName="IMPALLibrary.InventoryReports">
                        <SelectParameters>
                            <asp:ControlParameter Name="strBranch" ControlID="ddlBranch" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hidToDate" runat="server" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                OnClientClick="javaScript:return fnValidate();" TabIndex="4" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crListTransaction" runat="server" OnUnload="crListTransaction_Unload" ReportName="impal_editlistfortran" />
    </div>
</asp:Content>
