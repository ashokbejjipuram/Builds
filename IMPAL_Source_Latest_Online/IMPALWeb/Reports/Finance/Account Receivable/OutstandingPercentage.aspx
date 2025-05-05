<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="OutstandingPercentage.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.OutstandingPercentage" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateBranch() {

            var fromBranch = document.getElementById('<%=ddlFromBranch.ClientID %>');
            var toBranch = document.getElementById('<%=ddlToBranch.ClientID %>');
            if (fromBranch.value.trim() == null || fromBranch.value.trim() == "" || fromBranch.value.trim() == "0") {
                alert("From Branch should not be null");
                return false;
            }
            else if (toBranch.value.trim() == null || toBranch.value.trim() == "" || toBranch.value.trim() == "0") {
                alert("To Branch should not be null");
                return false;
            }
            else if (fromBranch.value.trim() > toBranch.value.trim()) {
                alert("To Branch should be Greater");
                return false;
            }
        } </script>

    <div class="reportFormTitle  reportFormTitleExtender350">
        Outstanding Percentage (Branch)
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromBranch" runat="server" Text="From Branch" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlFromBranch" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblToBranch" runat="server" Text="To Branch" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToBranch" runat="server" TabIndex="2" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="3" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return validateBranch();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crOutPercentage" runat="server" OnUnload="crOutPercentage_Unload" />
    </div>
</asp:Content>
