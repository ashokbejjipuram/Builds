<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Consolidated-Branch_Line.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.OOOH.Consolidated_Branch_Line" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript">
        function validatenull(drp) {
            var drp = document.getElementById('<%=ddmonthyear.ClientID%>');
            return validatemonthyear(drp);
        }

    </script>

    <div class="reportFormTitle">
        Consolidated-Branch/Line
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" runat="server" Text="Branch Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddbranchcode" runat="server" SkinID="DropDownListNormal" TabIndex="1"
                        AutoPostBack="true" OnSelectedIndexChanged="ddbranchcode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lbllinecode" SkinID="LabelNormal" Text="Line Code" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlinecode" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblmonthyear" SkinID="LabelNormal" Text="Month Year" runat="server"></asp:Label>
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
        <asp:ObjectDataSource ID="ObjectDataBranchcode" runat="server" SelectMethod="GetAllBranch"
            TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crbrline" OnUnload="crbrline_Unload" />
    </div>
</asp:Content>
