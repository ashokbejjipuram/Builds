<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Consolidated-Line_Branch.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.OOOH.Consolidated_Line_Branch" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">   
        function validatenull(drp) {
            var drp = document.getElementById('<%=ddmonthyear.ClientID%>');
            return validatemonthyear(drp);
        }
    </script>

    <div class="reportFormTitle">
        Consolidated-Line/Branch
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lbllinecode" SkinID="LabelNormal" Text="Line Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlinecode" runat="server" AutoPostBack="True" DataSourceID="ODsupplier"
                        DataTextField="SupplierName" SkinID="DropDownListNormal" TabIndex="1" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblbranchcode" SkinID="LabelNormal" Text="Branch Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddbranchcode" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblmonthyear" Text="Month Year" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddmonthyear" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="4" OnClientClick="javaScript:return validatenull();" />
        </div>
        <asp:ObjectDataSource ID="ODsupplier" runat="server" SelectMethod="GetSupplierline"
            TypeName="IMPALLibrary.Suppliers">
            <SelectParameters>
                <asp:ControlParameter Name="strBranchcode" ControlID="ddbranchcode" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crlinebranch" OnUnload="crlinebranch_Unload" />
    </div>
</asp:Content>
