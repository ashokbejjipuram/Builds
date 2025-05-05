<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="OpeningBalance.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.OpeningBalance" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
    function validate() {
        var accountingperiod1 = document.getElementById('<%=ddlAccountingPeriod.ClientID %>');
        var branch = document.getElementById('<%=ddlBranch.ClientID %>');
        if (accountingperiod1.value == -1 ) {
            alert("Accounting period should not be null.");
            return false;
        }
        else if (branch.value.trim() == null || branch.value.trim() == "") {
            alert("Branch should not be null");
            return false;
        }
        else {
            return true;
        }
    }

    </script>

    <div class="reportFormTitle">
        Opening Balance
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccountingPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccountingPeriod" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="2" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return validate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="cropeningbalance" OnUnload="cropeningbalance_Unload" />
    </div>
</asp:Content>
