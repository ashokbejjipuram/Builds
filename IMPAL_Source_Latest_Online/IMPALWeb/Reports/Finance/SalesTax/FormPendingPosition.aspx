<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPendingPosition.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.SalesTax.FormPendingPosition" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            if (validateacc(ddlAccPeriod) == false)
                return false;
            else {
                var ddlBranchName = document.getElementById('<%=ddlBranchName.ClientID%>');
                if (ddlBranchName.value == "") {
                    alert("Select Branch Name!");
                    ddlBranchName.focus();
                    return false;
                }
            }
        }
    </script>

    <div class="reportFormTitle">
        Form Pending Position
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccPeriod" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblNumYears" Text="Number of Years" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlNumYears" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                        <asp:ListItem Text="2" Value="2" />
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="4" Value="4" />
                        <asp:ListItem Text="5" Value="5" />
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBranchName" runat="server" Text="Branch Name" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranchName" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" SkinID="ButtonViewReport" TabIndex="3" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc:CrystalReport ID="rptCrystal" runat="server" OnUnload="rptCrystal_Unload" ReportName="FormPendingPos" />
    </div>
</asp:Content>
