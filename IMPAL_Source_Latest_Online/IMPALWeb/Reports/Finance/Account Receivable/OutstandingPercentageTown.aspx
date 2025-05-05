<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="OutstandingPercentageTown.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.OutstandingPercentageTown" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">        function Validatenull() {

            var drpfromtown = document.getElementById('<%=ddlFromTown.ClientID %>');
            var drptotown = document.getElementById('<%=ddlToTown.ClientID %>');
            if (drpfromtown.value.trim() > drptotown.value.trim()) {
                alert("To Town should be greater");
                return false;
            }
            else {
                return true;
            }


        } </script>

    <div class="reportFormTitle">
        Outstanding Percentage Town
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblfromtown" runat="server" Text="From Town" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlFromTown" runat="server" TabIndex="2" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lbltotown" runat="server" Text="To Town" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToTown" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="4" OnClientClick="javaScript:return Validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="croutstandingtown" OnUnload="croutstandingtown_Unload" />
    </div>
</asp:Content>
